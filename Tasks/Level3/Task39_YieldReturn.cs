using System;
using System.Collections.Generic;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task39_YieldReturn : ITask
{
    public string Title => "yield return — генератор чётных чисел";
    public string Description => "Реализовать генератор чётных чисел через yield return и показать ленивость.";
    public string[] Requirements => new[]
    {
        "Метод IEnumerable<int> GetEvenNumbers(int max) — возвращает чётные числа до max",
        "Реализовать именно через yield return, без промежуточного списка",
        "Продемонстрировать работу в foreach",
        "Дополнительно: показать ленивость (вывести сообщение внутри генератора)"
    };

    public void Run()
    {
        Console.WriteLine("--- Демонстрация ленивости (yield return) ---");
        Console.WriteLine("Запускаем foreach...\n");


        foreach (int number in GetEvenNumbers(10))
        {
            Console.WriteLine($"[Получено из генератора]: {number}");


            if (number == 4)
            {
                Console.WriteLine("Прерываем цикл на 4...");
                break;
            }
        }

        Console.WriteLine("\n--- Демонстрация полного прохода ---");
        foreach (int number in GetEvenNumbers(6))
        {
            Console.WriteLine($"Число: {number}");
        }
    }

  
    private IEnumerable<int> GetEvenNumbers(int max)
    {
        Console.WriteLine("  [Генератор]: Начинаю работу!");

        for (int i = 0; i <= max; i++)
        {
            if (i % 2 == 0)
            {
                Console.WriteLine($"  [Генератор]: Подготовил число {i}");
                yield return i;
            }
        }

        Console.WriteLine("  [Генератор]: Закончил работу!");
    }
}