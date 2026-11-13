using System;
using System.Collections.Generic;
using System.Linq;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task36_Duplicates : ITask
{
    public string Title => "Дубликаты в List (HashSet + GroupBy)";
    public string Description => "Найти дубликаты в списке чисел двумя способами.";
    public string[] Requirements => new[]
    {
        "Способ 1: через HashSet<int> — проверять Contains",
        "Способ 2: через LINQ GroupBy(x => x).Where(g => g.Count() > 1)",
        "Оба способа оформить отдельными методами",
        "Продемонстрировать на списке с повторяющимися значениями"
    };

    public void Run()
    {
        List<int> numbers = new List<int> { 1, 2, 3, 2, 4, 5, 5, 6, 1, 7, 8, 9, 3 };

        Console.WriteLine("Исходный список: " + string.Join(", ", numbers));

        var duplicates1 = FindDuplicatesHashSet(numbers);
        var duplicates2 = FindDuplicatesLinq(numbers);

        Console.WriteLine($"Способ 1 (HashSet): {string.Join(", ", duplicates1)}");
        Console.WriteLine($"Способ 2 (LINQ):    {string.Join(", ", duplicates2)}");
    }


    private List<int> FindDuplicatesHashSet(List<int> list)
    {
        HashSet<int> seen = new HashSet<int>();
        HashSet<int> duplicates = new HashSet<int>();

        foreach (int num in list)
        {
          
            if (!seen.Add(num))
            {
                duplicates.Add(num);
            }
        }
        return duplicates.ToList();
    }

    private List<int> FindDuplicatesLinq(List<int> list)
    {
        return list
            .GroupBy(x => x)                 
            .Where(g => g.Count() > 1)       
            .Select(g => g.Key)              
            .ToList();
    }
}