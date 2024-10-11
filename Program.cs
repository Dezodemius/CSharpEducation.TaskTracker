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

namespace Main
{
  internal class Program 
  {

    static void Main(string[] args)
    {
      Host OTask_Manager_bot = new Host();
      OTask_Manager_bot.Start();
      //OTask_Manager_bot.OnMessage += OnMessage;
      Console.ReadLine();
    }
  }
}


