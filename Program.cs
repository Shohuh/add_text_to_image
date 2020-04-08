using AddTextToImageTgBot.Controller;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace AddTextToImageTgBot
{
    class Program
    {
        private static readonly TelegramBotClient bot = new TelegramBotClient("1286739413:AAFIYjlAwKqD2irRw5bK4FKhPyPGvjvBbbM");
      
        static void Main(string[] args)
        {
            TgBotController tg = new TgBotController();
           
        }
       

    }
}
