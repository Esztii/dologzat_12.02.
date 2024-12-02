using dologzat_12._02_;
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        List<Book> books = GenerateRandomBooks();
        int kezdetiKeszletSzam = GetTotalStockCount(books);

        int eladottKonyvekSzama = 0;
        int elfogyottKeszletszam = 0;
        int bevetel = 0;

        for (int i = 0; i < 100; i++)
        {
            Book book = SelectRandomBook(books);
            if (book.Stock > 0)
            {
                book.Stock--;
                bevetel += book.Price;
                eladottKonyvekSzama++;
            }
            else
            {
                if (new Random().Next(2) == 0)
                {
                    book.Stock = new Random().Next(1, 11);
                }
                else
                {
                    books.Remove(book);
                    elfogyottKeszletszam++;
                }
            }
        }

        int jelenlegiKeszletSzam = GetTotalStockCount(books);

        Console.WriteLine($"Összes eladott könyv: {eladottKonyvekSzama}");
        Console.WriteLine($"Nagykerből elfogyott könyvek: {elfogyottKeszletszam}");
        Console.WriteLine($"Kezdeti készlet: {kezdetiKeszletSzam} db");
        Console.WriteLine($"Jelenlegi készlet: {jelenlegiKeszletSzam} db");
        Console.WriteLine($"Készlet változás: {jelenlegiKeszletSzam - kezdetiKeszletSzam} db");
        Console.WriteLine($"Bruttó bevétel: {bevetel} Ft");
    }

    private static List<Book> GenerateRandomBooks()
    {
        List<Book> books = new List<Book>();
        for (int i = 0; i < 15; i++)
        {
            string title = $"Cím {i + 1}";
            List<Author> authors = GenerateRandomAuthors();
            long isbn = GenerateUniqueISBN(books);
            string language = new Random().NextDouble() < 0.8 ? "magyar" : "angol";
            int publicationYear = new Random().Next(2007, DateTime.Now.Year + 1);
            int price = new Random().Next(1000, 10001) / 100 * 100;
            books.Add(new Book(isbn, authors, title, publicationYear, language, price));
        }
        return books;
    }

    private static List<Author> GenerateRandomAuthors()
    {
        int authorCount = new Random().Next(1, 4);
        List<Author> authors = new List<Author>();
        for (int i = 0; i < authorCount; i++)
        {
            authors.Add(new Author($"Szerző{i + 1} Vezetéknév"));
        }
        return authors;
    }

    private static long GenerateUniqueISBN(List<Book> books)
    {
        long isbn;
        Random random = new Random();
        do
        {
            isbn = random.Next(1000000000, 10000000000);
        } 
        while (books.Exists(book => book.ISBN == isbn));
        return isbn;
    }

    private static Book SelectRandomBook(List<Book> books)
    {
        return books[new Random().Next(books.Count)];
    }

    private static int GetTotalStockCount(List<Book> books)
    {
        int totalStock = 0;
        foreach (var book in books)
        {
            totalStock += book.Stock;
        }
        return totalStock;
    }
}
