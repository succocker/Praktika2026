using System;
using Microsoft.Data.SqlClient;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task29_CSharpInsert : ITask
{
    public string Title => "29. C# INSERT в БД";
    public string Description => "Добавление нового отдела в базу данных через C#.";
    public string[] Requirements => new[] { "Использовать параметризованный запрос", "Обработать SqlException" };

    public void Run()
    {
        string connStr = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION")
                         ?? throw new Exception("Переменная PRACTICE_DB_CONNECTION не найдена!");
        try
        {
            using var connection = new SqlConnection(connStr);
            connection.Open();

    
            string query = "INSERT INTO Departments (DepartmentName) VALUES (@Name)";
            using var cmd = new SqlCommand(query, connection);

            
            cmd.Parameters.AddWithValue("@Name", "Новый отдел (из C#)");

   
            int rowsAffected = cmd.ExecuteNonQuery();

            Console.WriteLine($"Успешно! Добавлено строк: {rowsAffected}");
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL: {ex.Message}");
        }
    }
}
