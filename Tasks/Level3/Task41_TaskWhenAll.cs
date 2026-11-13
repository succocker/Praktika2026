using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;
>
public class Task41_TaskWhenAll : ITask
{
    public string Title => "Task.WhenAll — параллельно 10 задач";
    public string Description => "Запустить 10 задач параллельно и сравнить с последовательным выполнением.";
    public string[] Requirements => new[]
    {
        "Task<int> WorkAsync(int id) — Task.Delay(500), возвращает id",
        "Последовательный запуск 10 задач в цикле с await",
        "Параллельный запуск через Task.WhenAll",
        "Замерить время обоих вариантов через Stopwatch",
        "Вывести оба времени и объяснить разницу"
    };

    public void Run()
    {
      
        RunAsync().GetAwaiter().GetResult();
    }

    private async Task RunAsync()
    {
        int taskCount = 10;
        Console.WriteLine($"Запускаем {taskCount} задач (каждая занимает 500 мс)...\n");

   
        Stopwatch sw1 = Stopwatch.StartNew();
        List<int> sequentialResults = new List<int>();

        for (int i = 1; i <= taskCount; i++)
        {
      
            int result = await WorkAsync(i);
            sequentialResults.Add(result);
        }
        sw1.Stop();
        Console.WriteLine($"Последовательное выполнение: {sw1.ElapsedMilliseconds} мс");

        //параллельное выполнение
        Stopwatch sw2 = Stopwatch.StartNew();

        List<Task<int>> tasks = new List<Task<int>>();
        for (int i = 1; i <= taskCount; i++)
        {
            tasks.Add(WorkAsync(i));
        }

        int[] parallelResults = await Task.WhenAll(tasks);

        sw2.Stop();
        Console.WriteLine($"Параллельное выполнение:     {sw2.ElapsedMilliseconds} мс");

        Console.WriteLine($"\nРезультаты одинаковые: {sequentialResults.SequenceEqual(parallelResults)}");

        // объяснение:
        // при последовательном выполнении каждая задача ждет 500 мс.
        // итого: 10 задач * 500 мс = 5000 мс (5 секунд).
        // При параллельном выполнении все 10 задач запускаются одновременно
        // и ждут 500 мс вместе. Итого: ~500 мс.
    }

    // имитация долгой работы (например, запрос к серверу)
    private async Task<int> WorkAsync(int id)
    {
        await Task.Delay(500);
        return id;
    }
}