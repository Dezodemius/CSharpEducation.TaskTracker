using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Polling;
using System.Threading;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Telegram.Bot.Types.ReplyMarkups;
using System.ComponentModel;
using Main.Copy;
using Telegram.Bot.Types.Enums;
using Main.DataBase;
using NLog;


namespace Main
{
    internal class Host
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        private Dictionary<long, bool> waitingForTaskDescription = new Dictionary<long, bool>();
        public Action<ITelegramBotClient, Update> OnMessage;
        private TelegramBotClient Task_bot;
        private bool AllTasksFlag = false;
        //private List<Main.Copy.Task> MyTask;
        private List<Main.Copy.Task> AllTasks;
        private List<Main.Copy.Task> MyTasks;
        public Host()
        {
            Task_bot = new TelegramBotClient("7940487255:AAGToj4yup9tZ61PUc7o-RNpc02nDNtIhuA");
        }

        public void Start()
        {
            logger.Info("Старт приложения");
            Task_bot.StartReceiving(UpdateHandler, ErrorHandler);
        }

        /// <summary>
        /// Метод закоментирован потому что не работает.
        /// Кто-нибудь почините.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="update"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update.Type == UpdateType.Message)
                {
                    //string Time = DateTime.Now.ToShortTimeString();
                    var message = update.Message;

                    if (waitingForTaskDescription.ContainsKey(message.Chat.Id))
                    {
                        string description = message.Text;

                        logger.Info($"Получено описание задачи: {description}");

                        // Проверка на пустое описание
                        if (string.IsNullOrWhiteSpace(description))
                        {
                            await Task_bot.SendTextMessageAsync(
                                chatId: message.Chat.Id,
                                text: "Описание задачи не может быть пустым. Пожалуйста, введите описание задачи.");
                            return;
                        }

                        // Создайте новый объект задачи
                        var newTask = new Main.Copy.Task
                        {
                            Description = description ?? throw new ArgumentNullException(nameof(description)),
                            StartDate = DateTime.Now.ToUniversalTime(),
                            EndDate = DateTime.Now.AddDays(1).ToUniversalTime(), // Задача заканчивается через день
                            AppUserId = message.Chat.Id.ToString() // Приведение к строке, если необходимо
                        };

                        logger.Info("Создание новой задачи в базе данных");
                        DataBaseMethods.getInstance().CreateTask(newTask);

                        await Task_bot.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Задача добавлена!");

                        // Удалите пользователя из состояния ожидания
                        // Выход из метода, если задача была добавлена
                        waitingForTaskDescription.Remove(message.Chat.Id);
                        return;
                    }
                    if (message.Text.ToLower() == "/menu")
                    {
                        await Task_bot.SendTextMessageAsync(
                        chatId: message.Chat,
                        text: "Главное меню",
                        replyMarkup: kb.Menu);
                        return;
                    }
                    else if (message.Text.ToLower() == "/start")
                    {
                        await Task_bot.SendTextMessageAsync(
                        chatId: message.Chat,
                        text: "А я все думал когда же ты появишься ☺\nПиши /menu <- или тыкни сюда",
                        replyMarkup: kb.Menu);
                        return;
                    }
                    else if (message.Text.ToLower() == "/newtask")
                    {
                        await StartWaitingForTaskDescription(message.Chat.Id);
                        return;
                    }
                    else if (message.Text.ToLower() == "/help")
                    {
                        await Task_bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Бог в помощь напиши /menu <- или тыкни",
                        cancellationToken: cancellationToken);
                        return;
                    }
                    else
                    {
                        await Task_bot.SendTextMessageAsync(
                        chatId: message.Chat.Id,
                        text: "Я тебя не понять \u263a\nЧитай описание",
                        cancellationToken: cancellationToken);
                        await SendStartMessage(message.Chat.Id, cancellationToken);
                    }

                }
                if (update.Type == UpdateType.CallbackQuery)
                {
                    //Обработка кнопок.
                    var callbackQuery = update.CallbackQuery;

                    if (callbackQuery.Data == "AllTask")
                    {
                        await SendAllTaskMessage(callbackQuery, cancellationToken);
                    }
                    if (callbackQuery.Data == "MyTask")
                    {
                        await SendMyTaskMessage(callbackQuery, cancellationToken);
                    }
                    if (callbackQuery.Data == "CreateTask")
                    {
                        await StartWaitingForTaskDescription(callbackQuery.Message.Chat.Id); // Запускаем ожидание описания задачи
                        return;
                    }

                    else if (int.TryParse(callbackQuery.Data, out int AllTasksResult) && AllTasksFlag && AllTasks != null)
                    {
                        await Task_bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: $"\nId: - {AllTasks[AllTasksResult].Id}\nDescription: - {AllTasks[AllTasksResult].Description}\nStartDate: - {AllTasks[AllTasksResult].StartDate}\nEndDate: - {AllTasks[AllTasksResult].EndDate}\nAppUserId: - {AllTasks[AllTasksResult].AppUserId}\n",
                        cancellationToken: cancellationToken);
                    }
                    else if (int.TryParse(callbackQuery.Data, out int MyTasksResult) && !AllTasksFlag && MyTasks != null)
                    {
                        await Task_bot.SendTextMessageAsync(
                        chatId: callbackQuery.Message.Chat.Id,
                        text: $"\nId: - {MyTasks[MyTasksResult].Id}\nDescription: - {MyTasks[MyTasksResult].Description}\nStartDate: - {MyTasks[MyTasksResult].StartDate}\nEndDate: - {MyTasks[MyTasksResult].EndDate}\nAppUserId: - {MyTasks[MyTasksResult].AppUserId}\n",
                        cancellationToken: cancellationToken);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error($"Ошибка с update методом: {ex.Message}\n{ex.InnerException?.Message}\n{ex.StackTrace}");
            }
        }

        /// <summary>
        /// Начально меню
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task SendStartMessage(long chatId, CancellationToken cancellationToken)
        {
            logger.Info($"Отправка начального сообщения пользователю с chatId: {chatId}");
            await Task_bot.SendTextMessageAsync(
              chatId: chatId,
              text: "Выбери опцию",
              replyMarkup: kb.Menu,
              cancellationToken: cancellationToken);
        }

        /// <summary>
        /// Выводит список задач для выбора и просмотра информации интересующей задачи 
        /// </summary>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task SendAllTaskMessage(CallbackQuery query, CancellationToken cancellationToken)
        {
            logger.Info($"Отправка всех задач пользователю с chatId: {query.Message.Chat.Id}");

            var dbMethods = DataBaseMethods.getInstance();
            AllTasks = dbMethods.GetAllTasks();
            if (AllTasks == null || AllTasks.Count == 0)
            {
                await Task_bot.SendTextMessageAsync(
                        chatId: query.Message.Chat.Id,
                        text: "Нет доступных задач.",
                        cancellationToken: cancellationToken);
            }

            InlineKeyboardMarkup inline = kb.GetInlineKeyboard(AllTasks.ToArray());

            await Task_bot.SendTextMessageAsync(
                  chatId: query.Message.Chat.Id,
                  text: "Задачи для всех\nВыберете интересующую вас",
                  replyMarkup: inline,
                  cancellationToken: cancellationToken);
        }

        private async System.Threading.Tasks.Task SendMyTaskMessage(CallbackQuery query, CancellationToken cancellationToken)
        {
            logger.Info($"Отправка моих задач пользователю с chatId: {query.Message.Chat.Id}");

            var dbMethods = DataBaseMethods.getInstance();

            // Создайте экземпляр ApplicationContext

            MyTasks = dbMethods.GetMyTask(query.From.Id.ToString()).ToList(); // Передайте идентификатор пользователя


            InlineKeyboardMarkup inline = kb.GetInlineKeyboard(MyTasks.ToArray());

            await Task_bot.SendTextMessageAsync(
              chatId: query.Message.Chat.Id,
              text: "Мои задачи\nВыберете интересующую вас",
              replyMarkup: inline,
              cancellationToken: cancellationToken);
        }

        private async System.Threading.Tasks.Task StartWaitingForTaskDescription(long chatId)
        {
            if (!waitingForTaskDescription.ContainsKey(chatId))
            {
                waitingForTaskDescription[chatId] = true;
                await Task_bot.SendTextMessageAsync(
                    chatId: chatId,
                    text: "Пожалуйста, введите описание задачи.");
            }
        }
        /// <summary>
        /// Отслеживание ошибок.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="exception"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            logger.Error($"Ошибка: {exception.Message}");
            Console.WriteLine("Ошибка: " + exception.Message);
            await System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
