using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task44_TodoJson : ITask
{
    public string Title => "Мини-проект Todo List (JSON)";
    public string Description => "Консольное приложение для списка задач с сохранением в JSON.";
    public string[] Requirements => new[]
    {
        "Классы: TaskItem, TaskRepository, TaskService",
        "CRUD-операции: добавить, вывести все, отметить выполненной, удалить",
        "Сохранение/загрузка через System.Text.Json (файл tasks.json)",
        "Меню с выбором действий",
        "Обработка ошибок (некорректный ввод, отсутствующий файл)"
    };

    public void Run()
    {
     
        string filePath = "tasks.json";
        var repository = new TaskRepository(filePath);
        var service = new TaskService(repository);

        while (true)
        {
            Console.WriteLine("\n--- TODO LIST ---");
            Console.WriteLine("1. Показать задачи");
            Console.WriteLine("2. Добавить задачу");
            Console.WriteLine("3. Отметить выполненной");
            Console.WriteLine("4. Удалить задачу");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");

            string? choice = Console.ReadLine();

            try
            {
                if (choice == "1")
                {
                    service.PrintAllTasks();
                }
                else if (choice == "2")
                {
                    Console.Write("Введите название задачи: ");
                    string? title = Console.ReadLine();
                    service.AddTask(title);
                }
                else if (choice == "3")
                {
                    Console.Write("Введите ID задачи: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                        service.MarkAsDone(id);
                    else
                        Console.WriteLine("Ошибка: ID должен быть числом.");
                }
                else if (choice == "4")
                {
                    Console.Write("Введите ID задачи: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                        service.DeleteTask(id);
                    else
                        Console.WriteLine("Ошибка: ID должен быть числом.");
                }
                else if (choice == "0")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Неизвестная команда.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}


// модель

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public bool IsDone { get; set; }
}


// репоз (Сохранение и загрузка)

public class TaskRepository
{
    private readonly string _filePath;
    private List<TaskItem> _tasks;

    public TaskRepository(string filePath)
    {
        _filePath = filePath;
        _tasks = LoadFromFile();
    }

    public List<TaskItem> GetAll() => _tasks;

    public void Add(TaskItem task)
    {
    
        task.Id = _tasks.Any() ? _tasks.Max(t => t.Id) + 1 : 1;
        _tasks.Add(task);
        SaveToFile();
    }

    public void Remove(int id)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            _tasks.Remove(task);
            SaveToFile();
        }
    }

    public void Update(TaskItem updatedTask)
    {
        var task = _tasks.FirstOrDefault(t => t.Id == updatedTask.Id);
        if (task != null)
        {
            task.Title = updatedTask.Title;
            task.IsDone = updatedTask.IsDone;
            SaveToFile();
        }
    }

    private void SaveToFile()
    {

        string json = JsonSerializer.Serialize(_tasks, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_filePath, json);
    }

    private List<TaskItem> LoadFromFile()
    {
        if (!File.Exists(_filePath)) return new List<TaskItem>();

        try
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<TaskItem>>(json) ?? new List<TaskItem>();
        }
        catch
        {
       
            return new List<TaskItem>();
        }
    }
}


// сервис (Бизнес-логика и вывод)

public class TaskService
{
    private readonly TaskRepository _repository;

    public TaskService(TaskRepository repository)
    {
        _repository = repository;
    }

    public void AddTask(string? title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Название задачи не может быть пустым.");

        _repository.Add(new TaskItem { Title = title, IsDone = false });
        Console.WriteLine("Задача добавлена!");
    }

    public void PrintAllTasks()
    {
        var tasks = _repository.GetAll();
        if (!tasks.Any())
        {
            Console.WriteLine("Список задач пуст.");
            return;
        }

        foreach (var task in tasks)
        {
            string status = task.IsDone ? "[X]" : "[ ]";
            Console.WriteLine($"{task.Id}. {status} {task.Title}");
        }
    }

    public void MarkAsDone(int id)
    {
        var task = _repository.GetAll().FirstOrDefault(t => t.Id == id);
        if (task == null) throw new Exception($"Задача с ID {id} не найдена.");

        task.IsDone = true;
        _repository.Update(task);
        Console.WriteLine("Задача отмечена выполненной!");
    }

    public void DeleteTask(int id)
    {
        _repository.Remove(id);
        Console.WriteLine($"Задача с ID {id} удалена (если существовала).");
    }
}