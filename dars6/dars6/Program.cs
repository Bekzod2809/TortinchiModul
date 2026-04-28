using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

// 1. Botni sozlash
var botClient = new TelegramBotClient("8699204401:AAGOuknuNT7f6Uh-86hAvnLirjCuoeMD6FA");
using HttpClient http = new();

using var cts = new CancellationTokenSource();

// 2. ReceiverOptions (Xatolikni oldini olish uchun)
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>() // Hamma xabarlarni qabul qilish
};

Console.WriteLine("Bot ishga tushdi...");

// 3. StartReceiving (Parametr nomlari to'g'rilangan)
botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    errorHandler: HandlePollingErrorAsync, // 'pollingErrorHandler' emas, shunchaki 'errorHandler'
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

Console.ReadLine();

// Xabarlarni qayta ishlash
async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Faqat matnli xabarlarni filtrlash
    if (update.Message is not { Text: { } messageText } message) return;

    long chatId = message.Chat.Id;
    string? user = message.Chat.FirstName;

    try
    {
        // API orqali ob-havoni olish
        string weather = await http.GetStringAsync($"https://wttr.in/{messageText}?format=3", cancellationToken);

        // Log faylga yozish
        string log = $"[{DateTime.Now}] User: {user}, Shahar: {messageText}, Natija: {weather.Trim()}\n";
        await File.AppendAllTextAsync("weather_logs.txt", log, cancellationToken);

        // JAVOB YUBORISH (Kutubxonaning yangi versiyasi uchun)
        await botClient.SendMessage(
            chatId: chatId,
            text: $"Natija: {weather}",
            cancellationToken: cancellationToken
        );
    }
    catch
    {
        await botClient.SendMessage(chatId, "Xatolik! Shahar nomini inglizcha kiriting.", cancellationToken: cancellationToken);
    }
}

// Xatoliklarni qayta ishlash
Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    Console.WriteLine("Xatolik yuz berdi: " + exception.Message);
    return Task.CompletedTask;
}