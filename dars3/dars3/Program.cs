using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace dars3;

internal class Program
{
    // 1. Bot tokeningizni shu yerga qo'ying
    private static ITelegramBotClient botClient = new TelegramBotClient("8337917588:AAFlW0q3fGfaHhKXClYbAKsXuMijlmPzHJU");

    // 2. FAYL YO'LI (FILE PATH) - O'zingiz yaratgan fayl manzilini SHU YERGA qo'ying:
    // DIQQAT: Tirnoqdan oldin @ belgisini qoldiring!
    private const string UsersFilePath = @"D:\Coding\DotNet\TortinchiModul\dars3\dars3\Bazza.txt";

    // 3. HttpClient: Bitta obyekt orqali tezkor ishlash
    private static readonly HttpClient httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(10) };

    private static Dictionary<long, string> userDirections = new Dictionary<long, string>();

    static async Task Main(string[] args)
    {
        Console.WriteLine("Bot ishga tushdi...");
        using var cts = new CancellationTokenSource();

        // Botni ulaymiz
        botClient.StartReceiving(
            HandleUpdateAsync,
            HandlePollingErrorAsync,
            new ReceiverOptions { AllowedUpdates = Array.Empty<UpdateType>() },
            cts.Token
        );

        await Task.Delay(-1); // Dastur o'chib qolmasligi uchun
    }

    static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        if (update.Message is not { } message || message.Text is not { } messageText) return;

        long chatId = message.Chat.Id;

        // Foydalanuvchini faylga saqlash funksiyasini chaqiramiz
        await SaveUserToFile(message.From);

        ReplyKeyboardMarkup replyKeyboardMarkup = new(new[]
        {
            new KeyboardButton[] { "🇺🇿 UZ -> 🇬🇧 ENG", "🇬🇧 ENG -> 🇺🇿 UZ" },
        })
        { ResizeKeyboard = true };

        if (messageText == "/start")
        {
            await botClient.SendMessage(chatId, "Salom! Tarjima yo'nalishini tanlang:", replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);
            return;
        }

        if (messageText == "🇺🇿 UZ -> 🇬🇧 ENG")
        {
            userDirections[chatId] = "uz/en";
            await botClient.SendMessage(chatId, "So'zni o'zbekcha yozing, men uni inglizchaga o'giraman.", cancellationToken: cancellationToken);
            return;
        }
        else if (messageText == "🇬🇧 ENG -> 🇺🇿 UZ")
        {
            userDirections[chatId] = "en/uz";
            await botClient.SendMessage(chatId, "So'zni inglizcha yozing, men uni o'zbekchaga o'giraman.", cancellationToken: cancellationToken);
            return;
        }

        // Tarjima jarayoni
        if (userDirections.ContainsKey(chatId))
        {
            await botClient.SendChatAction(chatId, ChatAction.Typing, cancellationToken: cancellationToken);

            string[] langs = userDirections[chatId].Split('/');
            string translatedText = await TranslateAsync(messageText, langs[0], langs[1]);

            await botClient.SendMessage(chatId, translatedText, cancellationToken: cancellationToken);
        }
        else
        {
            await botClient.SendMessage(chatId, "Iltimos, avval tarjima yo'nalishini tanlang!", replyMarkup: replyKeyboardMarkup, cancellationToken: cancellationToken);
        }
    }

    static async Task<string> TranslateAsync(string text, string from, string to)
    {
        try
        {
            string url = $"https://lingva.ml/api/v1/{from}/{to}/{Uri.EscapeDataString(text)}";
            var response = await httpClient.GetStringAsync(url);
            var json = JObject.Parse(response);
            return json["translation"]?.ToString() ?? "Tarjima topilmadi.";
        }
        catch (Exception ex)
        {
            Console.WriteLine("API xatosi: " + ex.Message);
            return "Kechirasiz, tarjima xizmati vaqtincha ishlamayapti.";
        }
    }

    static async Task SaveUserToFile(User? user)
    {
        if (user == null) return;
        try
        {
            // Papka mavjudligini tekshirish va yaratish
            string? directory = Path.GetDirectoryName(UsersFilePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            List<User> users = new List<User>();

            if (System.IO.File.Exists(UsersFilePath))
            {
                string existingData = await System.IO.File.ReadAllTextAsync(UsersFilePath);
                users = JsonConvert.DeserializeObject<List<User>>(existingData) ?? new List<User>();
            }

            if (!users.Exists(u => u.Id == user.Id))
            {
                users.Add(user);
                string json = JsonConvert.SerializeObject(users, Formatting.Indented);
                await System.IO.File.WriteAllTextAsync(UsersFilePath, json);
                Console.WriteLine($"[LOG]: Yangi foydalanuvchi bazaga qo'shildi: {user.FirstName}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("[XATO]: Faylga yozishda muammo: " + ex.Message);
        }
    }

    static Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        Console.WriteLine("Telegram xatosi: " + exception.Message);
        return Task.CompletedTask;
    }
}