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
using NLog;

namespace Main
{
    internal class Host
    {
        private readonly Logger logger = LogManager.GetCurrentClassLogger();
        public Action<ITelegramBotClient, Update> OnMessage;
        private TelegramBotClient Task_bot;
        private bool AllTasksFlag = false;
        Tasks[] AllTasks;
        Tasks[] MyTasks;
        public Host()
        {
            Task_bot = new TelegramBotClient("7416815072:AAGlhMnfVjHh8s4L2k63cXIEt7OTAA4if9Q");
        }

        public void Start()
        {
            logger.Info("Старт приложения");
            Task_bot.StartReceiving(UpdateHandler, ErrorHandler);
            Console.WriteLine("Бот запущен");
        }


        private async Task UpdateHandler(ITelegramBotClient client, Update update, CancellationToken cancellationToken)
        {
            try
            {
                if (update.Type == UpdateType.Message)
                {
                    string time = DateTime.Now.ToShortTimeString();
                    var message = update.Message;

                    logger.Info($"Получено сообщение от пользователя {message.Chat.Id} в {time}. Текст: {message.Text}");

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
                            text: "А я все думал, когда же ты появишься ☺\nПиши /menu или тыкни сюда",
                            replyMarkup: kb.Menu);
                        return;
                    }
                    else if (message.Text.ToLower() == "/help")
                    {
                        await Task_bot.SendTextMessageAsync(
                            chatId: message.Chat.Id,
                            text: "Бог в помощь! Напиши /menu или тыкни сюда",
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
                    var callbackQuery = update.CallbackQuery;
                    logger.Info($"Обработка нажатия кнопки от пользователя {callbackQuery.Message.Chat.Id}. CallbackData: {callbackQuery.Data}");

                    if (callbackQuery.Data == "AllTask")
                    {
                        SendAllTaskMessage(update.CallbackQuery, cancellationToken);
                    }
                    else if (callbackQuery.Data == "MyTask")
                    {
                        SendMyTaskMessage(update.CallbackQuery, cancellationToken);
                    }
                    else if (int.TryParse(callbackQuery.Data, out int allTasksResult) && AllTasksFlag && AllTasks != null)
                    {
                        await Task_bot.SendTextMessageAsync(
                            chatId: update.Message.Chat.Id,
                            text: $"\nId: - {AllTasks[allTasksResult].Id}\nDescription: - {AllTasks[allTasksResult].Description}\nStartDate: - {AllTasks[allTasksResult].StartDate}\nEndDate: - {AllTasks[allTasksResult].EndDate}\nAppUserId: - {AllTasks[allTasksResult].AppUserId}\nComments: - {AllTasks[allTasksResult].Comments}\nResponsibles: -{AllTasks[allTasksResult].Responsibles}\n",
                            cancellationToken: cancellationToken);
                    }
                    else if (int.TryParse(callbackQuery.Data, out int myTasksResult) && !AllTasksFlag && MyTasks != null)
                    {
                        await Task_bot.SendTextMessageAsync(
                            chatId: update.Message.Chat.Id,
                            text: $"\nId: - {MyTasks[myTasksResult].Id}\nDescription: - {MyTasks[myTasksResult].Description}\nStartDate: - {MyTasks[myTasksResult].StartDate}\nEndDate: - {MyTasks[myTasksResult].EndDate}\nAppUserId: - {MyTasks[myTasksResult].AppUserId}\nComments: - {MyTasks[myTasksResult].Comments}\nResponsibles: -{MyTasks[myTasksResult].Responsibles}\n",
                            cancellationToken: cancellationToken);
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.Error($"Поймано исключение ArgumentNullException: {ex.Message}\n{ex.StackTrace}");
            }
            catch (InvalidOperationException ex)
            {
                logger.Error($"Поймано исключение InvalidOperationException: {ex.Message}\n{ex.StackTrace}");
            }
            catch (Exception ex)
            {
                logger.Error($"Общее исключение: {ex.Message}\n{ex.StackTrace}");
                await Task_bot.SendTextMessageAsync(
                    chatId: update.Message?.Chat.Id ?? 0,
                    text: "Произошла ошибка. Пожалуйста, попробуйте позже.",
                    cancellationToken: cancellationToken);
            }
        }

        /// <summary>
        /// Начально меню
        /// </summary>
        /// <param name="chatId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task SendStartMessage(long chatId, CancellationToken cancellationToken)
        {
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
        private async Task SendAllTaskMessage(CallbackQuery query, CancellationToken cancellationToken)
        {
            InlineKeyboardMarkup inlineKeyboardMarkup;
            AllTasks = ApplicationContext.GetAllTask();

            InlineKeyboardMarkup inline = kb.GetInlineKeyboard(AllTasks);

            await Task_bot.SendTextMessageAsync(
              chatId: query.Message.Chat.Id,
              text: "Задачи для всех\nВыберете интересующую вас",
              replyMarkup: inline,
              cancellationToken: cancellationToken);
        }
        private async Task SendMyTaskMessage(CallbackQuery query, CancellationToken cancellationToken)
        {
            InlineKeyboardMarkup inlineKeyboardMarkup;
            MyTasks = ApplicationContext.GetMyTask();

            InlineKeyboardMarkup inline = kb.GetInlineKeyboard(MyTasks);
            //InlineKeyboardMarkup inline = new InlineKeyboardMarkup([
            //  [InlineKeyboardButton.WithCallbackData($"Задача {0}: {tasks[0].Description}", $"TaskId{0}")],
            //  [InlineKeyboardButton.WithCallbackData($"Задача {1}: {tasks[1].Description}", $"TaskId{1}")],
            //  [InlineKeyboardButton.WithCallbackData($"Задача {2}: {tasks[2].Description}", $"TaskId{2}")]
            //  ]);

            await Task_bot.SendTextMessageAsync(
              chatId: query.Message.Chat.Id,
              text: "Мои задачи\nВыберете интересующую вас",
              replyMarkup: inline,
              cancellationToken: cancellationToken);
        }


        /// <summary>
        /// Error Handler отслеживание ошибок как я понял 
        /// </summary>
        /// <param name="client"></param>
        /// <param name="exception"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
        {
            Console.WriteLine("Ошибка: " + exception.Message);
            await Task.CompletedTask;
        }
    }
}
