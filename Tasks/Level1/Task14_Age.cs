using System;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level1;

public class Task14_Age : ITask
{
    public string Title => "14. Возраст по дате рождения";
    public string Description => "По дате рождения вычислить точный возраст в годах, месяцах и днях.";
    public string[] Requirements => new[]
    {
        "Ввод даты через DateTime.TryParse (или TryParseExact).",
        "Учесть, что день рождения в этом году мог ещё не наступить.",
        "Вывести «X лет, Y месяцев, Z дней».",
        "Некорректная дата — сообщение об ошибке, не падать."
    };

    public void Run()
    {
        Console.WriteLine("Введите вашу дату рождения в формате ДД.ММ.ГГГГ:");
        string? vvod = Console.ReadLine();

        if (!DateTime.TryParse(vvod, out DateTime birthDate))
        {
            Console.WriteLine("Некорректный формат даты.");
            return;
        }
        DateTime today = DateTime.Today;

        if (birthDate > today)
        {
            Console.WriteLine("Ошибка: Дата рождения не может быть в будущем.");
            return;
        }

        int years = today.Year - birthDate.Year;

      
        if (birthDate.Date > today.AddYears(-years))
        {
            years--;
        }

     
        DateTime anniversary = birthDate.AddYears(years);
        int months = 0;
        while (anniversary.AddMonths(months + 1) <= today)
        {
            months++;
        }

        int days = (today - anniversary.AddMonths(months)).Days;

        Console.WriteLine($"\nВаш возраст: {years} лет, {months} месяцев, {days} дней.");
    }
}
