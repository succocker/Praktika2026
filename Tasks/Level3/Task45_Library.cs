using System;
using System.Collections.Generic;
using System.Linq;
using CSharpPraktikum.Common;

namespace CSharpPraktikum.Tasks.Level3;

public class Task45_Library : ITask
{
    public string Title => "Мини-проект Библиотека + тесты";
    public string Description => "Классы библиотеки с логикой выдачи книг и лимитами.";
    public string[] Requirements => new[]
    {
        "Классы: Book, Reader, Library",
        "Логика: выдача, возврат, лимит (не больше 3 книг)",
        "Обработка ошибок: книга уже выдана, лимит превышен",
        "Написать 5+ юнит-тестов (xUnit/NUnit)"
    };

    public void Run()
    {
        var library = new Library();
        var book1 = new Book("1984", "Оруэлл");
        var book2 = new Book("Мастер и Маргарита", "Булгаков");
        var reader = new Reader("Иван");

        library.AddBook(book1);
        library.AddBook(book2);

        Console.WriteLine("--- Демонстрация работы библиотеки ---");
        library.BorrowBook(reader, book1);
        Console.WriteLine($"{reader.Name} взял книгу '{book1.Title}'");

        try
        {
            library.BorrowBook(reader, book1);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}


// классы библио

public class Book
{
    public string Title { get; }
    public string Author { get; }
    public bool IsAvailable { get; set; } = true;

    public Book(string title, string author)
    {
        Title = title;
        Author = author;
    }
}

public class Reader
{
    public string Name { get; }
    public List<Book> BorrowedBooks { get; } = new();

    public Reader(string name)
    {
        Name = name;
    }
}

public class Library
{
    private readonly List<Book> _books = new();
    private const int MaxBooksPerReader = 3;

    public void AddBook(Book book) => _books.Add(book);

    public void BorrowBook(Reader reader, Book book)
    {
        if (!_books.Contains(book))
            throw new ArgumentException("Этой книги нет в библиотеке.");

        if (!book.IsAvailable)
            throw new InvalidOperationException($"Книга '{book.Title}' уже выдана.");

        if (reader.BorrowedBooks.Count >= MaxBooksPerReader)
            throw new InvalidOperationException($"Лимит превышен: нельзя брать больше {MaxBooksPerReader} книг.");

        book.IsAvailable = false;
        reader.BorrowedBooks.Add(book);
    }

    public void ReturnBook(Reader reader, Book book)
    {
        if (!reader.BorrowedBooks.Contains(book))
            throw new InvalidOperationException("Этот читатель не брал эту книгу.");

        book.IsAvailable = true;
        reader.BorrowedBooks.Remove(book);
    }
}