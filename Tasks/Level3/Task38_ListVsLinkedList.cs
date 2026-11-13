using System;
using System.Collections.Generic;
using System.Diagnostics;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;


public class Task38_ListVsLinkedList : ITask
{
    public string Title => "List vs LinkedList";
    public string Description => "Сравнить вставку в середину List<T> и LinkedList<T>.";
    public string[] Requirements => new[]
    {
        "Создать List<int> и LinkedList<int> по 100 000 элементов",
        "Вставить 1000 элементов в середину каждой коллекции",
        "Замерить время через Stopwatch",
        "Объяснить результат в комментарии"
    };

    public void Run()
    {
        int size = 100_000;
        int insertions = 1000;

        Console.WriteLine($"Создаем коллекции по {size} элементов...\n");

 
        List<int> list = new List<int>();
        for (int i = 0; i < size; i++) list.Add(i);

        LinkedList<int> linkedList = new LinkedList<int>();
        for (int i = 0; i < size; i++) linkedList.AddLast(i);

        Stopwatch sw1 = Stopwatch.StartNew();
        for (int i = 0; i < insertions; i++)
        {
 
            list.Insert(list.Count / 2, -1);
        }
        sw1.Stop();
        Console.WriteLine($"List<T> (вставка в середину):       {sw1.ElapsedMilliseconds} мс");

        Stopwatch sw2 = Stopwatch.StartNew();

   
        var middleNode = GetMiddleNode(linkedList);

        for (int i = 0; i < insertions; i++)
        {
            linkedList.AddAfter(middleNode, -1);
        }
        sw2.Stop();
        Console.WriteLine($"LinkedList<T> (вставка в середину):  {sw2.ElapsedMilliseconds} мс");

        // ВЫВОД (Объяснение):
        // List<T> хранит элементы в непрерывном массиве. При вставке в середину
        // ему приходится сдвигать все элементы вправо, чтобы освободить место.
        // на 100 000 элементов это требует много времени.
        // LinkedList<T> — это цепочка узлов. Чтобы вставить элемент в середину,
        // достаточно поменять ссылки у соседних узлов (это O(1)).
    }

    private LinkedListNode<int> GetMiddleNode(LinkedList<int> list)
    {
        var current = list.First;
        for (int i = 0; i < list.Count / 2; i++)
        {
            current = current.Next;
        }
        return current!;
    }
}