using System;
using System.Collections.Generic;
using CSharpPraktikum.Common;
using CSharpPraktikum.Tasks.Level1;
using CSharpPraktikum.Tasks.Level2;
using CSharpPraktikum.Tasks.Level3; // <--- ОБЯЗАТЕЛЬНО ДЛЯ 3-ГО УРОВНЯ

namespace CSharpPraktikum.Menu;

public class MenuRunner
{
    private readonly List<ITask> _level1Tasks = new();
    private readonly List<ITask> _level2Tasks = new();
    private readonly List<ITask> _level3Tasks = new(); // <--- ДОБАВЛЕН СПИСОК 3 УРОВНЯ

    public MenuRunner()
    {
        // === УРОВЕНЬ 1 ===
        _level1Tasks.Add(new Task01_HelloWorld());
        _level1Tasks.Add(new Task02_PerPrimTip());
        _level1Tasks.Add(new Task03_Calculator());
        _level1Tasks.Add(new Task04_Chetnost());
        _level1Tasks.Add(new Task05_MultiplicationTable());
        _level1Tasks.Add(new Task06_DigitSum());
        _level1Tasks.Add(new Task07_Reverse());
        _level1Tasks.Add(new Task08_FizzBuzz());
        _level1Tasks.Add(new Task09_MassivMinMaxSumAvg());
        _level1Tasks.Add(new Task10_Sorting());
        _level1Tasks.Add(new Task11_Matrix());
        _level1Tasks.Add(new Task12_Palindrom());
        _level1Tasks.Add(new Task13_Dictionary());
        _level1Tasks.Add(new Task14_Age());

        // === УРОВЕНЬ 2 ===
        _level2Tasks.Add(new Task16_StringExtensions());
        _level2Tasks.Add(new Task17_Concat());
        _level2Tasks.Add(new Task18_TryParseVsTryCatch());
        _level2Tasks.Add(new Task19_Stek());
        _level2Tasks.Add(new Task20_SwapMaxIComparable());
        _level2Tasks.Add(new Task21_enum());
        _level2Tasks.Add(new Task22_Params());
        _level2Tasks.Add(new Task23_Nullable());
        _level2Tasks.Add(new Task24_oop());
        _level2Tasks.Add(new Task25_InterfaceIShape());
        _level2Tasks.Add(new Task26_PatternStrategy());
        _level2Tasks.Add(new Task27_IComparableIComparerTforStudent());
        _level2Tasks.Add(new Task28_EqualsGetHashCode());
        _level2Tasks.Add(new Task29_FabricPattern());
        _level2Tasks.Add(new Task30_BankAccount());
        _level2Tasks.Add(new Task31_IDisposable());
        _level2Tasks.Add(new Task32_temperatura());
        _level2Tasks.Add(new Task33_EmployeesLinq());
        _level2Tasks.Add(new Task34_GrupirSlov());

        // === УРОВЕНЬ 3 (Названия взяты с вашего скриншота) ===
        _level3Tasks.Add(new Task36_Duplicates());
        _level3Tasks.Add(new Task37_FibonacciMemo());
        _level3Tasks.Add(new Task38_ListVsLinkedList());
        _level3Tasks.Add(new Task39_YieldReturn());
        _level3Tasks.Add(new Task40_AsyncExceptions());
        _level3Tasks.Add(new Task41_TaskWhenAll());
        _level3Tasks.Add(new Task42_Cancellation());
        _level3Tasks.Add(new Task43_ExceptionOrder()); // Исправил двойное имя
        _level3Tasks.Add(new Task44_TodoJson());       // Исправил GrupirSlov на TodoJson
        _level3Tasks.Add(new Task45_Library());        // Исправил GrupirSlov на Library

        // Задания по Базе Данных (они тоже лежат в Level3)
        _level3Tasks.Add(new Task28_CSharpSql());
        _level3Tasks.Add(new Task29_CSharpInsert());
        _level3Tasks.Add(new Task30_CSharpReport());
    }

    public void Run()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== Практикум по C# ===");
            Console.WriteLine("1. Уровень 1 — Базовый (задания 1–15)");
            Console.WriteLine("2. Уровень 2 — Средний (задания 16–35)");
            Console.WriteLine("3. Уровень 3 — Продвинутый (задания 36–50)");
            Console.WriteLine("4. Выход");
            Console.Write("\nВыберите уровень: ");

            string? input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    ShowLevel1Menu();
                    break;
                case "2":
                    ShowLevel2Menu();
                    break;
                case "3":
                    ShowLevel3Menu(); // <--- ДОБАВЛЕН ВЫЗОВ МЕНЮ 3 УРОВНЯ
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Неверный ввод. Нажмите Enter...");
                    Console.ReadLine();
                    break;
            }
        }
    }

    private void ShowLevel1Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Уровень 1: Базовый ---");

            for (int i = 0; i < _level1Tasks.Count; i++)
            {
                Console.WriteLine(_level1Tasks[i].Title);
            }

            Console.WriteLine("0. Назад");
            Console.Write("\nВыберите задание: ");

            string? input = Console.ReadLine();

            if (input == "0") return;

            if (int.TryParse(input, out int taskNumber) && taskNumber >= 1 && taskNumber <= _level1Tasks.Count)
            {
                RunTask(_level1Tasks[taskNumber - 1]);
            }
            else
            {
                Console.WriteLine("Неверный номер задания. Нажмите Enter...");
                Console.ReadLine();
            }
        }
    }

    private void ShowLevel2Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Уровень 2: Средний ---");

            for (int i = 0; i < _level2Tasks.Count; i++)
            {
                Console.WriteLine($"{i + 16}. {_level2Tasks[i].Title}");
            }

            Console.WriteLine("0. Назад");
            Console.Write("\nВыберите задание: ");

            string? input = Console.ReadLine();

            if (input == "0") return;

            if (int.TryParse(input, out int taskNumber)
                && taskNumber >= 16
                && taskNumber <= 15 + _level2Tasks.Count)
            {
                RunTask(_level2Tasks[taskNumber - 16]);
            }
            else
            {
                Console.WriteLine("Неверный номер задания. Нажмите Enter...");
                Console.ReadLine();
            }
        }
    }

    // <--- ДОБАВЛЕН МЕТОД ДЛЯ 3 УРОВНЯ --->
    private void ShowLevel3Menu()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--- Уровень 3: Продвинутый ---");

            for (int i = 0; i < _level3Tasks.Count; i++)
            {
                // Выводим номера, начиная с 36
                Console.WriteLine($"{i + 36}. {_level3Tasks[i].Title}");
            }

            Console.WriteLine("0. Назад");
            Console.Write("\nВыберите задание: ");

            string? input = Console.ReadLine();

            if (input == "0") return;

            // Проверка на диапазон 36...50
            if (int.TryParse(input, out int taskNumber)
                && taskNumber >= 36
                && taskNumber <= 35 + _level3Tasks.Count)
            {
                RunTask(_level3Tasks[taskNumber - 36]);
            }
            else
            {
                Console.WriteLine("Неверный номер задания. Нажмите Enter...");
                Console.ReadLine();
            }
        }
    }

    private void RunTask(ITask task)
    {
        Console.Clear();
        Console.WriteLine(new string('=', 50));
        Console.WriteLine(task.Title);
        Console.WriteLine(new string('-', 50));
        Console.WriteLine("Описание:");
        Console.WriteLine($"  {task.Description}");

        Console.WriteLine("\nТребования:");
        foreach (var req in task.Requirements)
        {
            Console.WriteLine($"  - {req}");
        }
        Console.WriteLine(new string('-', 50));

        Console.WriteLine("[запуск решения...]\n");

        task.Run();

        Console.WriteLine("\nНажмите Enter для возврата в меню...");
        Console.ReadLine();
    }
}
