using AddTextToImageTgBot.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace AddTextToImageTgBot.Servces
{
    public class SendMessage
    {
        static TelegramBotClient _botClient;

        IAddText _addText = new AddText();

        ManualResetEvent mre = new ManualResetEvent(false);

        Person person = new Person();
        long userId;
        public SendMessage(TelegramBotClient bot)
        {
            _botClient = bot;
        }

        public void StartPoint(object sender, MessageEventArgs e)
        {

            if (e.Message.Text == "/Start" || e.Message.Text == "/start")
            {
                Console.WriteLine("asdasdasdsadas");
                var markup = new ReplyKeyboardMarkup(new[] {
                        new KeyboardButton("Boshlash"),
                    });
                markup.OneTimeKeyboard = true;

                _botClient.SendTextMessageAsync(e.Message.Chat.Id, "Assalomu alaykum! \n FURBO ga xush kelibsiz. \n" +
                    "Rasmingiz <a href=\"https://t.me/FurboBusiness\">FurboDevelopment</a> kanaliga yuboriladi.\n" +
                    "Savol va Takliflar uchun Furbo admini bilan bog'laning : @FurboDevelopment",
                    replyMarkup: markup,
                    parseMode:ParseMode.Html);

            }

        }
        public void SendMessageToUser(object sender, MessageEventArgs e)
        {
            if (e.Message.Text == "Boshlash")
            {
                //var bmp = Bitmap.FromFile("checkWhite.jpg");

                // var first_photo = "https://miro.medium.com/max/1200/1*mk1-6aYaf_Bes1E3Imhc0A.jpeg";
                // _botClient.SendPhotoAsync(e.Message.Chat.Id, first_photo, " 1 ");
                int count = 1;
                SendChoiceImageForChoice("photo1.jpg", count, e);
                count++;
                SendChoiceImageForChoice("photo2.jpg", count,e);

               
                var markup = new ReplyKeyboardMarkup(new[] {
                    new KeyboardButton("Birinchi rasm"),
                    new KeyboardButton("Ikkinchi rasm")
                });
                markup.OneTimeKeyboard = true;

                _botClient.SendTextMessageAsync(e.Message.Chat.Id, " Orqada Fonni Tanlang ", replyMarkup: markup);

/*
                var myValueText = Path.Combine(Directory.GetCurrentDirectory(), "Moon.jpg");

                var fts = new InputOnlineFile(new MemoryStream(File.ReadAllBytes(myValueText)));

                _botClient.SendPhotoAsync(e.Message.Chat.Id, fts, "My Text");
*/
            }
        }

        public async void SendImageToChanel(object sender, MessageEventArgs e)
        {
            int statusColor; 
            if (e.Message.Text == "Birinchi rasm")
            {
                userId = e.Message.Chat.Id;
                statusColor = 1;
                var question_title = "Ismingizni kiriting";
                var chat_id = e.Message.Chat.Id;
                int counter = 1;
                await SendMessageToGetUserNameAsync(chat_id, question_title,true);

                _addText.AddText("photo1.jpg",115,"sendPhot.jpg",person.Name,statusColor);

                SendSelectedImage("sendPhot.jpg", counter, e);
            }
            if (e.Message.Text == "Ikkinchi rasm")
            {
                userId = e.Message.Chat.Id;
                statusColor = 2;
                var question_title = "Ismingizni kiriting";
                var chat_id = e.Message.Chat.Id;
                int counter = 1;
                await SendMessageToGetUserNameAsync(chat_id, question_title, true);

                _addText.AddText("photo2.jpg", 115, "sendPhot2.jpg", person.Name, statusColor);

                SendSelectedImage("sendPhot2.jpg", counter, e);
            }
           
        }

        // send message to user for asking name 
        public async Task SendMessageToGetUserNameAsync(long chatID, string message, bool shouldAwait = false)
        {

            await _botClient.SendTextMessageAsync(chatID, message, ParseMode.Html);
            if (shouldAwait)
            {
                _botClient.OnMessage += GetUserName;
            }
            mre.WaitOne();

            _botClient.OnMessage -= GetUserName;
        }

        private void GetUserName(object sender, MessageEventArgs e)
        {
            person.Name = e.Message.Text;

            Console.WriteLine(e.Message.Text);
            if (userId == e.Message.Chat.Id)
            {
                //meetingModel.
                mre.Set();
                mre.Reset(); //we dont need the reset becouse we have only one question
            }
        }

        private void SendSelectedImage(string imageName, int counter, MessageEventArgs e)
        {
            var myValueText = Path.Combine(Directory.GetCurrentDirectory(), imageName);

            var fts = new InputOnlineFile(new MemoryStream(File.ReadAllBytes(myValueText)));

            //_botClient.SendTextMessageAsync("@FurboRasmlar", "worjs for text");
            _botClient.SendPhotoAsync("@FurboRasmlar", fts, $"Rasmlar qo'shish va tahrirlash bo'yicha taklif va mulohozalaringiz bo'lsa admin bilan bog'laning : @FurboDevelopment \n #{person.Name} \n Isimli rasm yaratish uchun telegram bot : @TgAvaBot");
            _botClient.SendTextMessageAsync(e.Message.Chat.Id, $"Rasmingiz <a href=\"https://t.me/FurboRasmlar\" >FurboRasmlar</a> kanaliga  #{person.Name} hashtegi ostida yuborildi.\n" +
                $"\n FurboDevlopment tarmog'ining boshqa qiziqarli (videolar, rasmlar ) <a href=\"https://t.me/FurboBusiness\" > kanaliga </a> o'tishingiz mumkin" 
            ,parseMode: ParseMode.Html );

 
            // _botClient.SendTextMessageAsync(e.Message.Chat.Id,)
        }
        private void SendChoiceImageForChoice(string imageName, int counter, MessageEventArgs e)
        {
            var myValueText = Path.Combine(Directory.GetCurrentDirectory(), imageName);

            var fts = new InputOnlineFile(new MemoryStream(File.ReadAllBytes(myValueText)));

            //_botClient.SendTextMessageAsync("@FurboRasmlar", "worjs for text");
            _botClient.SendPhotoAsync(e.Message.Chat.Id, fts, $"Rasmlar qo'shish va tahrirlash bo'yicha taklif va mulohozalaringiz bo'lsa admin bilan bog'laning : @FurboDevelopment \n #{counter}");
        }


    }
    
}
