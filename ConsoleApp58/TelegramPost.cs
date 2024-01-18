using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleApp55
{
    public class TelegramPost
    {
        private string Token { get; set; }
        private string ChannelName { get; set; }
        private string PostText { get; set; }

        public TelegramPost(string token)
        {
            this.Token = token;
        }

        public async Task PostHandle()
        {
            var botClient = new TelegramBotClient(this.Token);

            using CancellationTokenSource cts = new();

            ReceiverOptions receiverOptions = new()
            {
                AllowedUpdates = Array.Empty<UpdateType>()
            };

            botClient.StartReceiving(
                updateHandler: HandleUpdateAsync,
                pollingErrorHandler: HandlePollingErrorAsync,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();

            Console.WriteLine($"Start listening for @{me.Username}");
            Console.ReadLine();

            cts.Cancel();




        }

        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message is not { } message || message.Text is not { } messageText)
                return;

            var chatId = update.Message.Chat.Id;
           /* var message = update.Message;*/



            /*     if (message.Type == MessageType.Sticker)
                 {
                     await botClient.SendStickerAsync(
        chatId: chatId,
        sticker: InputFile.FromFileId(message.Sticker!.FileId),
        cancellationToken: cancellationToken);
                 }
                 else if (message.Type == MessageType.Photo)
                 {
                     await botClient.SendPhotoAsync(
        chatId: chatId,
        photo: InputFile.FromFileId(message.Photo.Last().FileId),
        cancellationToken: cancellationToken);
                 }
                 else if (message.Type == MessageType.Video)
                 {
                     await botClient.SendVideoAsync(
        chatId: chatId,
        video: InputFile.FromFileId(message.Video!.FileId),
        cancellationToken: cancellationToken);
                 }*/

            if (message.Text == "pool")
            {
                await botClient.SendPollAsync(
   chatId: "@djdbbkbwdbqbw",
   question: "Did you ever hear the tragedy of Darth Plagueis The Wise?",
   options: new[]
   {
        "Yes for the hundredth time!",
        "No, who`s that?"
   },
   cancellationToken: cancellationToken);
            }else if (messageText == "/video")
            {
                var fileStreamName = new FileStream(@"C:\Users\Hp\Videos\sevgi.mp4", FileMode.Open);
     
                await botClient.SendVideoAsync(
    chatId: chatId,
    video: InputFile.FromStream(fileStreamName),
    thumbnail: InputFile.FromUri("https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg"),
    supportsStreaming: true,
    cancellationToken: cancellationToken);



            }







        }

        private async Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            Console.WriteLine(errorMessage);
            await Task.CompletedTask;
        }
    }
}
