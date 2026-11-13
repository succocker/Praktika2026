using System;
using System.Collections.Generic;
using System.Diagnostics;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;


public class Task37_FibonacciMemo : ITask
{
    public string Title => "Мемоизация Фибоначчи";
    public string Description => "Реализовать мемоизированную функцию Фибоначчи и сравнить скорость с обычной рекурсией.";
    public string[] Requirements => new[]
    {
        "Обычная рекурсивная версия (для сравнения)",
        "Мемоизированная версия с Dictionary<int, long>",
        "Замерить время обеих на N = 30-35 через Stopwatch",
        "Вывести оба времени и разницу"
    };

    public void Run()
    {
        int n = 35;
        Console.WriteLine($"Вычисляем Фибоначчи для N = {n}...\n");

        // Обычная рек
        Stopwatch sw1 = Stopwatch.StartNew();
        long result1 = FibNaive(n);
        sw1.Stop();
        Console.WriteLine($"Обычная рекурсия:      {result1} (за {sw1.ElapsedMilliseconds} мс)");

        // Мемоиз
        Stopwatch sw2 = Stopwatch.StartNew();
        long result2 = FibMemo(n, new Dictionary<int, long>());
        sw2.Stop();
        Console.WriteLine($"Мемоизация (Dictionary): {result2} (за {sw2.ElapsedMilliseconds} мс)");

        Console.WriteLine($"\nРезультаты совпадают: {result1 == result2}");
    }


    private long FibNaive(int n)
    {
        if (n <= 1) return n;
        return FibNaive(n - 1) + FibNaive(n - 2);
    }

    private long FibMemo(int n, Dictionary<int, long> memo)
    {
        if (n <= 1) return n;

     
        if (memo.ContainsKey(n))
        {
            return memo[n];
        }

  
        long result = FibMemo(n - 1, memo) + FibMemo(n - 2, memo);
        memo[n] = result;

        return result;
    }
}