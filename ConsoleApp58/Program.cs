using ConsoleApp55;
using System;

namespace ConsoleApp58;

class Program
{
    static async Task Main(string[] args)
    {
        const string token = "6719531096:AAGHUFOcuu5g-JDh0FT1ecif6yLAYswyAXM";

        TelegramPost telegramPost = new TelegramPost(token);
        await  telegramPost.PostHandle();
    }
}