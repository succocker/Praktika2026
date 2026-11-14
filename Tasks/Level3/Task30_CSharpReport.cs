using System;
using Microsoft.Data.SqlClient;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task30_CSharpReport : ITask
{
    public string Title => "30. C# Отчёт (JOIN)";
    public string Description => "Вывод отчета по сотрудникам и их отделам.";
    public string[] Requirements => new[] { "Использовать JOIN", "Красивый текстовый вывод" };

    public void Run()
    {
        string connStr = Environment.GetEnvironmentVariable("PRACTICE_DB_CONNECTION")
                         ?? throw new Exception("Переменная PRACTICE_DB_CONNECTION не найдена!");
        try
        {
            using var connection = new SqlConnection(connStr);
            connection.Open();

            string query = @"
                SELECT e.EmployeeName, d.DepartmentName, e.Salary 
                FROM Employees e
                INNER JOIN Departments d ON e.DepartmentId = d.DepartmentId
                ORDER BY d.DepartmentName, e.Salary DESC";

            using var cmd = new SqlCommand(query, connection);
            using var reader = cmd.ExecuteReader();

            Console.WriteLine("==================================================");
            Console.WriteLine("ОТЧЕТ ПО СОТРУДНИКАМ");
            Console.WriteLine("==================================================");
            Console.WriteLine($"{"Сотрудник",-20} | {"Отдел",-15} | {"Зарплата",10}");
            Console.WriteLine(new string('-', 52));

            while (reader.Read())
            {
                string name = reader.GetString(0);
                string dept = reader.GetString(1);
                decimal salary = reader.GetDecimal(2);

                Console.WriteLine($"{name,-20} | {dept,-15} | {salary,10:F2}");
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine($"Ошибка SQL: {ex.Message}");
        }
    }
}
