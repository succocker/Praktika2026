using System;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;
public class Task43_ExceptionOrder : ITask
{
    public string Title => "Порядок catch-блоков";
    public string Description => "Продемонстрировать порядок обработки исключений.";
    public string[] Requirements => new[]
    {
        "Метод, который может бросить FormatException, DivideByZeroException или CustomException",
        "Несколько catch-блоков в правильном порядке (от частного к общему)",
        "Продемонстрировать все три сценария",
        "В конце — finally с выводом «завершено»",
        "Объяснить в комментарии, почему порядок catch важен"
    };

    public void Run()
    {
        // formatException
        Console.WriteLine("--- Сценарий 1: FormatException ---");
        ExecuteScenario("abc");

        // DivideByZeroException
        Console.WriteLine("\n--- Сценарий 2: DivideByZeroException ---");
        ExecuteScenario("0");

        // Кастомное исключение
        Console.WriteLine("\n--- Сценарий 3: CustomException ---");
        ExecuteScenario("100");
    }

    private void ExecuteScenario(string input)
    {
        try
        {
            int number = int.Parse(input);

            if (number == 0)
            {
            
                int result = 10 / number;
            }
            else if (number > 50)
            {
            
                throw new CustomException("Число слишком большое (больше 50)!");
            }

            Console.WriteLine($"Успешно обработано число: {number}");
        }
   
        catch (FormatException ex)
        {
            Console.WriteLine($"Поймано FormatException: {ex.Message}");
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Поймано DivideByZeroException: {ex.Message}");
        }
        catch (CustomException ex)
        {
            Console.WriteLine($"Поймано CustomException: {ex.Message}");
        }
        catch (Exception ex) 
        {
            Console.WriteLine($"Поймано неизвестное исключение: {ex.Message}");
        }
        finally
        {
  
            Console.WriteLine("  [finally]: Завершено.");
        }
    }
}


public class CustomException : Exception
{
    public CustomException(string message) : base(message) { }
}