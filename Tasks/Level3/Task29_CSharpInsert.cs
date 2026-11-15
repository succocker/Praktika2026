using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task29_CSharpInsert : ITask
{
    public string Title => "29. C# + SQL: параметризованный INSERT";
    public string Description => "Добавление сотрудника в базу с защитой от SQL-инъекций.";
    public string[] Requirements => new[] { "Параметризованный запрос", "SELECT для подтверждения" };

    public void Run()
    {
        RunAsync().GetAwaiter().GetResult();
    }

    private async Task RunAsync()
    {
        string connStr = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION")
                         ?? throw new Exception("Переменная PRACTICE_DB_CONNECTION не найдена!");
        try
        {
            using var connection = new SqlConnection(connStr);
            await connection.OpenAsync();

            string insertQuery = @"
                INSERT INTO Employees (EmployeeName, Salary, HireDate, DepartmentId) 
                VALUES (@Name, @Salary, @HireDate, @DeptId)";

            using var cmd = new SqlCommand(insertQuery, connection);

            
            cmd.Parameters.AddWithValue("@Name", "Иван Тестов");
            cmd.Parameters.AddWithValue("@Salary", 60000m);
            cmd.Parameters.AddWithValue("@HireDate", DateTime.Today);
            cmd.Parameters.AddWithValue("@DeptId", 1);

            int rows = await cmd.ExecuteNonQueryAsync();
            Console.WriteLine($"Успешно добавлено строк: {rows}");


            using var checkCmd = new SqlCommand("SELECT EmployeeName, Salary FROM Employees WHERE EmployeeName = 'Иван Тестов'", connection);
            using var reader = await checkCmd.ExecuteReaderAsync();

            Console.WriteLine("\nПроверка (новый сотрудник):");
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
