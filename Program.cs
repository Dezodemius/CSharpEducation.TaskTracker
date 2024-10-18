using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;
using System.Configuration;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;
using Main.DataBase;
using Microsoft.Extensions.Logging;
using NLog;


namespace Main
{
    internal class Program
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            logger.Info("Запуск приложения...");
            Host OTask_Manager_bot = new Host();
            OTask_Manager_bot.Start();
            //OTask_Manager_bot.OnMessage += OnMessage;
            logger.Info("Бот запущен и ожидает обновлений.");
            Console.ReadLine();
        }
    }
}


