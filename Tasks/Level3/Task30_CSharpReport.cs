using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task30_CSharpReport : ITask
{
    public string Title => "30. C# + SQL: мини-отчёт по отделам";
    public string Description => "Отчёт по отделам: количество сотрудников и средняя зарплата.";
    public string[] Requirements => new[] { "LEFT JOIN", "GROUP BY", "Форматированный вывод" };

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

            string query = @"
                SELECT 
                    d.DepartmentName, 
                    COUNT(e.EmployeeId) AS EmpCount, 
                    AVG(e.Salary) AS AvgSalary 
                FROM Departments d 
                LEFT JOIN Employees e ON d.DepartmentId = e.DepartmentId 
                GROUP BY d.DepartmentName";

            using var cmd = new SqlCommand(query, connection);
            using var reader = await cmd.ExecuteReaderAsync();

            Console.WriteLine("==================================================");
            Console.WriteLine("ОТЧЕТ ПО ОТДЕЛАМ");
            Console.WriteLine("==================================================");
            Console.WriteLine($"{"Отдел",-15} | {"Сотрудников",12} | {"Средняя ЗП",12}");
            Console.WriteLine(new string('-', 46));

            while (await reader.ReadAsync())
            {
                string dept = reader.GetString(0);
                int count = reader.GetInt32(1);

  
                string avgStr = reader.IsDBNull(2)
                    ? "N/A"
                    : reader.GetDecimal(2).ToString("F2");

                Console.WriteLine($"{dept,-15} | {count,12} | {avgStr,12}");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL: {ex.Message}");
        }
    }
}
