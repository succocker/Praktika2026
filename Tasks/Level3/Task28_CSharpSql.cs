using System;
using Microsoft.Data.SqlClient;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task28_CSharpSql : ITask
{
    public string Title => "28. C# SELECT из БД";
    public string Description => "Подключение к SQL Server и чтение данных.";
    public string[] Requirements => new[] { "Использовать Microsoft.Data.SqlClient", "Обработать SqlException" };
    public void Run()
    {
        string connStr = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION")
                         ?? throw new Exception("Переменная окружения PRACTICE_DB_CONNECTION не найдена!");
        try
        {
            using var connection = new SqlConnection(connStr);
            connection.Open();
            using var cmd = new SqlCommand("SELECT EmployeeName, Salary FROM Employees", connection);
            using var reader = cmd.ExecuteReader();

            Console.WriteLine("Сотрудники:");
            while (reader.Read())
                Console.WriteLine($"{reader.GetString(0)}: {reader.GetDecimal(1)} руб.");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL: {ex.Message}");
        }
    }
}
