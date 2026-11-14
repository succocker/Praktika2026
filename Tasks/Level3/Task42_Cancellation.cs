using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task42_Cancellation : ITask
{
    public string Title => "CancellationToken — отмена операции";
    public string Description => "Реализовать долгую операцию с поддержкой отмены.";
    public string[] Requirements => new[]
    {
        "async Task LongOperationAsync(CancellationToken token) — цикл 10 раз по 500 мс",
        "В Main создать CancellationTokenSource, запустить операцию, отменить через 2 секунды",
        "Обработать OperationCanceledException",
        "Вывести «операция отменена»"
    };

    public void Run()
    {
        RunAsync().GetAwaiter().GetResult();
    }

    private async Task RunAsync()
    {
        Console.WriteLine("Запускаем долгую операцию (всего 10 шагов по 0.5 сек = 5 сек)...");
        Console.WriteLine("Операция будет отменена через 2 секунды.\n");


        using (CancellationTokenSource cts = new CancellationTokenSource())
        {

            cts.CancelAfter(2000);

            try
            {
                
                await LongOperationAsync(cts.Token);
                Console.WriteLine("Операция успешно завершена!");
            }
            catch (OperationCanceledException)
            {
                
                Console.WriteLine("\n[ОПЕРАЦИЯ ОТМЕНЕНА] Пользователь прервал скачивание.");
            }
        } 
    }

    private async Task LongOperationAsync(CancellationToken token)
    {
        for (int i = 1; i <= 10; i++)
        {
           
            token.ThrowIfCancellationRequested();

            Console.WriteLine($"Шаг {i}/10 выполнен...");

            await Task.Delay(500, token);
        }
    }
}
