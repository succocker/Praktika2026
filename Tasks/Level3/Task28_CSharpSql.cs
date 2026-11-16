using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task28_CSharpSql : ITask
{
    public string Title => "28. C# + SQL: подключение и SELECT";
    public string Description => "Асинхронное подключение к PracticeDB и чтение сотрудников.";
    public string[] Requirements => new[] { "OpenAsync", "SqlDataReader", "Обработка SqlException" };

    public void Run()
    {
      
        RunAsync().GetAwaiter().GetResult();
    }

    private async Task RunAsync()
    {
        string? connStr = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION");
        if (string.IsNullOrEmpty(connStr))
        {
            Console.WriteLine("Ошибка: переменная PRACTICE_DB_CONNECTION не задана!");
            return;
        }
        try
        {
            using var connection = new SqlConnection(connStr);

         
            await connection.OpenAsync();

            using var cmd = new SqlCommand("SELECT EmployeeName, Salary FROM Employees", connection);
            using var reader = await cmd.ExecuteReaderAsync();

            Console.WriteLine("Сотрудники (получено асинхронно):");
            while (await reader.ReadAsync())
            {
                Console.WriteLine($"{reader.GetString(0)}: {reader.GetDecimal(1)} руб.");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL: {ex.Message}");
        }
    }
}
