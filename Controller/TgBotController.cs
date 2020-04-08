using AddTextToImageTgBot.Model;
using AddTextToImageTgBot.Servces;
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace AddTextToImageTgBot.Controller
{

    public class TgBotController
    {
        Token token = new Token();

      

        private static readonly TelegramBotClient bot = new TelegramBotClient("1286739413:AAFIYjlAwKqD2irRw5bK4FKhPyPGvjvBbbM");

        public TgBotController()
        {
            var send = new SendMessage(bot);

            bot.OnMessage += Bot_OnMessage;
            bot.OnMessage += send.StartPoint;
            bot.OnMessage += send.SendMessageToUser;
            bot.OnMessage += send.SendImageToChanel;

            bot.StartReceiving();




            Console.ReadLine();
            //  bot.StopReceiving();
            Console.WriteLine("Hello World!");
        }

        public static void Bot_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            if (e.Message.Text == "check")
            {
                var markup = new ReplyKeyboardMarkup(new[] {
                        new KeyboardButton("User Verification"),
                        new KeyboardButton("Card Verification"),
                   });
                markup.OneTimeKeyboard = true;
                bot.SendTextMessageAsync(e.Message.Chat.Id, " Tanlang ", replyMarkup: markup);
            }
        }




    }

}
