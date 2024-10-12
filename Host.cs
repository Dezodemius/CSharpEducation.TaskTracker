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
    public Action<ITelegramBotClient, Update> OnMessage;
    private TelegramBotClient Task_bot;
    private bool AllTasksFlag = false;
    Main.Copy.Task[] AllTasks;
    Main.Copy.Task[] MyTasks;
    public Host()
    {
      Task_bot = new TelegramBotClient("7940487255:AAGToj4yup9tZ61PUc7o-RNpc02nDNtIhuA");
    }

        public void Start()
        {
            logger.Info("Старт приложения");
            Task_bot.StartReceiving(UpdateHandler, ErrorHandler);
            Console.WriteLine("Бот запущен");
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
      //if(update.Type == UpdateType.Message)
      //{
      //  string Time = DateTime.Now.ToShortTimeString();
      //  var message = update.Message;
      //  if(message.Text.ToLower() == "/menu")
      //  {
      //    await Task_bot.SendTextMessageAsync(
      //    chatId: message.Chat,
      //    text: "Главное меню",
      //    replyMarkup: kb.Menu);
      //    return;
      //  }
      //  else if (message.Text.ToLower() == "/start")
      //  {
      //    await Task_bot.SendTextMessageAsync(
      //    chatId: message.Chat,
      //    text: "А я все думал когда же ты появишься ☺\nПиши /menu <- или тыкни сюда",
      //    replyMarkup: kb.Menu);
      //    return;
      //  }
      //  else if(message.Text.ToLower() == "/help")
      //  {
      //    await Task_bot.SendTextMessageAsync(
      //    chatId: message.Chat.Id,
      //    text: "Бог в помощь напиши /menu <- или тыкни",
      //    cancellationToken: cancellationToken);
      //    return;
      //  }
      //  else
      //  {
      //    await Task_bot.SendTextMessageAsync(
      //    chatId: message.Chat.Id,
      //    text: "Я тебя не понять \u263a\nЧитай описание",
      //    cancellationToken: cancellationToken);
      //    await SendStartMessage(message.Chat.Id, cancellationToken);
      //  }
      //  //if (!message.Text.StartsWith("/"))
      //  //{

      //  //}
      //}
      //if (update.Type == UpdateType.CallbackQuery)
      //{
      //  // Тут идет обработка всех нажатий на кнопки, тут никаких особых доп условий не надо, тк у каждой кнопки своя ссылка
      //  var callbackQuery = update.CallbackQuery;
      //  //var userRole = await DataBaseMethods.GetUserRole(callbackQuery.Message.Chat.Id);
      //  //long userTgId;
      //  //try
      //  //{
      //  //  userTgId = Convert.ToInt64(callbackQuery.Data);
      //  //}
      //  //catch
      //  //{
      //  //  userTgId = 0;
      //  //}
      //  //var checkUserCallback = await DataBaseMethods.GetUserRole(userTgId);
      //  if (callbackQuery.Data == "AllTask")
      //  {
      //    SendAllTaskMessage(update.CallbackQuery, cancellationToken);
      //  }
      //  else if (callbackQuery.Data == "MyTask")
      //  {
      //    SendMyTaskMessage(update.CallbackQuery, cancellationToken);
      //  }
      //  else if (int.TryParse(callbackQuery.Data, out int AllTasksResult)&& AllTasksFlag && AllTasks != null)
      //  {
      //    await Task_bot.SendTextMessageAsync(
      //    chatId: update.Message.Chat.Id,
      //    text: $"\nId: - {AllTasks[AllTasksResult].Id}\nDescription: - {AllTasks[AllTasksResult].Description}\nStartDate: - {AllTasks[AllTasksResult].StartDate}\nEndDate: - {AllTasks[AllTasksResult].EndDate}\nAppUserId: - {AllTasks[AllTasksResult].AppUserId}\nComments: - {AllTasks[AllTasksResult].Comments}\nResponsibles: -{AllTasks[AllTasksResult].Responsibles}\n",
      //    cancellationToken: cancellationToken);
      //    //await SendStartMessage(message.Chat.Id, cancellationToken);
      //  }
      //  else if (int.TryParse(callbackQuery.Data, out int MyTasksResult) && !AllTasksFlag && MyTasks != null)
      //  {
      //    await Task_bot.SendTextMessageAsync(
      //    chatId: update.Message.Chat.Id,
      //    text: $"\nId: - {MyTasks[AllTasksResult].Id}\nDescription: - {MyTasks[AllTasksResult].Description}\nStartDate: - {MyTasks[AllTasksResult].StartDate}\nEndDate: - {MyTasks[AllTasksResult].EndDate}\nAppUserId: - {MyTasks[AllTasksResult].AppUserId}\nComments: - {MyTasks[AllTasksResult].Comments}\nResponsibles: -{MyTasks[AllTasksResult].Responsibles}\n",
      //    cancellationToken: cancellationToken);
      //  }
      //  //var hadler = update switch
      //  //{
      //  //  { Message: { } message } => MessageTextHandler(message, cancellationToken),
      //  //  { CallbackQuery: { } query } => ChoiceCallbackQueryHandler(query, cancellationToken),
      //  //  { CallbackQuery: { } query1 } => OpportunitiesTaskMessage(query1, cancellationToken),
      //  //  _ => throw new NotImplementedException()
      //  //};
      //  //await hadler;
      //}
    }
    
    /// <summary>
    /// Начально меню
    /// </summary>
    /// <param name="chatId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    private async System.Threading.Tasks.Task SendStartMessage(long chatId, CancellationToken cancellationToken)
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
    private async System.Threading.Tasks.Task SendAllTaskMessage(CallbackQuery query, CancellationToken cancellationToken)
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
    private async System.Threading.Tasks.Task SendMyTaskMessage(CallbackQuery query, CancellationToken cancellationToken)
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
    private async System.Threading.Tasks.Task ErrorHandler(ITelegramBotClient client, Exception exception, CancellationToken token)
    {
      Console.WriteLine("Ошибка: " + exception.Message);
      await System.Threading.Tasks.Task.CompletedTask;
    }
  }
}
