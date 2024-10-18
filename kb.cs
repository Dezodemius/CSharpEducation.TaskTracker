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
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "Создать задачу", callbackData: "CreateTask"),
                InlineKeyboardButton.WithCallbackData(text: "Удалить задачу", callbackData: "RemoveTask"),
                
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "Поиск пользователя", callbackData: "FindAppUser"),
                InlineKeyboardButton.WithCallbackData(text: "Поиск всех задач пользователя", callbackData: "FindAllAppUserTasks"),
                InlineKeyboardButton.WithCallbackData(text: "Поиск всех комментариев по задаче", callbackData: "FindAllCommentsFromTask"),
            },
             new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "Добавить комментарий", callbackData: "CreateComment"),
                InlineKeyboardButton.WithCallbackData(text: "Удалить комментирий", callbackData: "RemoveComment"),
            },
             new[]
            {
                InlineKeyboardButton.WithCallbackData(text: "Удалить пользователя", callbackData: "RemoveAppUser"),
                InlineKeyboardButton.WithCallbackData(text: "Вывод задачи", callbackData: "FindAppUserTaskById"),
            },
        });
        /// <summary>
        /// Формирование задач в кнопки
        /// </summary>
        /// <param name="tasksArray"></param>
        /// <returns></returns>
        public static InlineKeyboardButton[][] GetInlineKeyboard(Main.Copy.Task[] tasksArray)
        {
            var keyboardInline = new List<InlineKeyboardButton[]>();

            for (var i = 0; i < tasksArray.Length; i++)
            {

                var row = new List<InlineKeyboardButton>();

                // Первая кнопка
                var shortDescription1 = tasksArray[i].Description.Length > 20
                    ? tasksArray[i].Description.Substring(0, 17) + "..."
                    : tasksArray[i].Description;

                row.Add(InlineKeyboardButton.WithCallbackData($"Задача: {shortDescription1}", $"{i}"));

                // Проверяем, есть ли вторая кнопка
                if (i + 1 < tasksArray.Length)
                {
                    var shortDescription2 = tasksArray[i + 1].Description.Length > 20
                        ? tasksArray[i + 1].Description.Substring(0, 17) + "..."
                        : tasksArray[i + 1].Description;

                    row.Add(InlineKeyboardButton.WithCallbackData($"Задача: {shortDescription2}", $"{i + 1}"));
                }

                // Добавляем строку кнопок в клавиатуру
                keyboardInline.Add(row.ToArray());
                //var button = InlineKeyboardButton.WithCallbackData($"Задача: {tasksArray[i].Id} {tasksArray[i].Description}", $"{i}");
                //// Проверяем, если строка еще не создана или уже полная
                //if (i % 1 == 0) // Измените 1 на желаемое количество кнопок в строке
                //{
                //    keyboardInline.Add(new InlineKeyboardButton[] { button });
                //}
                //else
                //{
                //    keyboardInline[keyboardInline.Count - 1] = keyboardInline[keyboardInline.Count - 1].Concat(new[] { button }).ToArray();
                //}
            }

            //var keyboardInline = new InlineKeyboardButton[tasksArray.Length][];
            //var keyboardButtons = new InlineKeyboardButton[tasksArray.Length];
            //for (var i = 0; i < tasksArray.Length; i++)
            //{
            //    keyboardButtons[i] = new InlineKeyboardButton($"Задача: {i} {tasksArray[i].Description}");
            //    keyboardButtons[i].CallbackData = $"{i}";
            //}
            //for (var j = 1; j <= tasksArray.Length; j++)
            //{
            //    keyboardInline[j - 1] = keyboardButtons.Take(1).ToArray();
            //    keyboardButtons = keyboardButtons.Skip(1).ToArray();
            //}

            return keyboardInline.ToArray();
        }
    }
}
