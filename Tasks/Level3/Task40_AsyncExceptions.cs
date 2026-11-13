using System;
using System.Threading.Tasks;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task40_AsyncExceptions : ITask
{
    public string Title => "async/await + обработка исключений";
    public string Description => "Асинхронный метод с эмуляцией скачивания и обработкой ошибок.";
    public string[] Requirements => new[]
    {
        "async Task<string> DownloadAsync(string url) — внутри Task.Delay(1000)",
        "Имитировать ошибку: если url пустой — бросить ArgumentException",
        "Вызвать через await внутри try/catch",
        "Продемонстрировать успешный и ошибочный вызов"
    };

    public void Run()
    {

        RunAsync().GetAwaiter().GetResult();
    }

    private async Task RunAsync()
    {
        Console.WriteLine("--- Успешный вызов ---");
        try
        {
            string result = await DownloadAsync("https://example.com/data");
            Console.WriteLine($"Результат: {result}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }

        Console.WriteLine("\n--- Ошибочный вызов (пустой url) ---");
        try
        {
            string result = await DownloadAsync("");
            Console.WriteLine($"Результат: {result}");
        }
        catch (ArgumentException ex)
        {
            
            Console.WriteLine($"Поймано исключение: {ex.Message}");
        }
    }

 
    private async Task<string> DownloadAsync(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
        {
            throw new ArgumentException("URL не может быть пустым!");
        }

        Console.WriteLine($"Начинаем скачивание с {url}...");
        await Task.Delay(1000);

        Console.WriteLine("Скачивание завершено.");
        return "Данные успешно загружены!";
    }
}