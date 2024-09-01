using Main.Copy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace Main
{
  public class kb
  {
    public static InlineKeyboardMarkup Menu = new(new[]
    {
            new []
            {
                InlineKeyboardButton.WithCallbackData(text: "Все задачи", callbackData: "AllTask"),//Диалоги
                InlineKeyboardButton.WithCallbackData(text: "Мои задачи", callbackData: "MyTask"),//Профиль
            },
            //new []
            //{
            //    InlineKeyboardButton.WithCallbackData(text: "Регистрация", callbackData: "register"),
            //},
        });
    /// <summary>
    /// Формирование задач в кнопки
    /// </summary>
    /// <param name="tasksArray"></param>
    /// <returns></returns>
    public static InlineKeyboardButton[][] GetInlineKeyboard(Tasks[] tasksArray)
    {
      var keyboardInline = new InlineKeyboardButton[tasksArray.Length][];
      var keyboardButtons = new InlineKeyboardButton[tasksArray.Length];
      for (var i = 0; i < tasksArray.Length; i++)
      {
        keyboardButtons[i] = new InlineKeyboardButton($"Задача: {i} {tasksArray[i].Description}");
        keyboardButtons[i].CallbackData = $"{i}";
      }
      for (var j = 1; j <= tasksArray.Length; j++)
      {
        keyboardInline[j - 1] = keyboardButtons.Take(1).ToArray();
        keyboardButtons = keyboardButtons.Skip(1).ToArray();
      }

      return keyboardInline;
    }
  }
}
