using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace dologzat_12._02_
{
    public class Book
    {
        public long ISBN { get; private set; }
        public List<Author> Authors { get; private set; }
        public string Title { get; private set; }
        public int PublicationYear { get; private set; }
        public string Language { get; private set; }
        public int Stock { get; set; }
        public int Price { get; private set; }
        public Book(long isbn, List<Author> authors, string title, int publicationYear, string language, int price)
        {
            if (authors == null || authors.Count < 1 || authors.Count > 3) throw new ArgumentException("nem megfelelő a szerzők száma");
            if (title.Length < 3 || title.Length > 64) throw new ArgumentException("az érték 3 vagy 64 karakterhosszúság között lehet");
            if (publicationYear < 2007 || publicationYear > DateTime.Now.Year) throw new ArgumentException("az év 2007 vagy a jelenlegi év lehet");
            if (language != "angol" && language != "német" && language != "magyar") throw new ArgumentException("a nyelv csak angol, csak német, vagy csak magyar lehet");
            if (price < 1000 || price > 10000 || price % 100 != 0) throw new ArgumentException("az érték 1000 és 10000 között lehet");
            ISBN = isbn;
            Authors = authors;
            Title = title;
            PublicationYear = publicationYear;
            Language = language;
            Stock = 0;
            Price = price;
        }
        public Book(string title, string authorName) : this(new Random().Next(1000000000, 1000000000),
            new List<Author> { new Author(authorName) },
            title,
            2024,
            "magyar",
            4500)
        { }
        public override string ToString()
        {
            string authorsString = Authors.Count == 1 ? "szerző:" : "szerzők:";
            string stockString = Stock == 0 ? "beszerzés alatt" : $"{Stock} db";
            return $"ISBN: {ISBN}\n" + $"{authorsString} " +
            $"{string.Join(", ", Authors)}\n"
                + $"cím: {Title}\n"
                + $"kiadás éve: {PublicationYear}\n"
                + $"nyelv: {Language}\n"
                + $"készlet: {stockString}\n"
                + $"ár: {Price} Ft";
        }
    }
    public class Author
    {
        public string FirstName { get; set; }
        public string LastName { get; }
        public Guid Id { get; }
    }
    public Author(string fullName)
    {
        var names = fullName.Split(' ');
        if (names.Length != 2) throw new ArgumentException("a név egy vezetéknévből és egy keresztnévből kell állnia");
        if (names[0].Length < 3 || names[0].Length > 32 || names[1].Length < 3 || names[1].Length > 32) throw new ArgumentException("nem megfelelő hosszúságú");
        
        FirstName = names[0];
        LastName = names[1];
        Id = Guid.NewGuid();
    }
    public override string ToString()
    {
        return $"{FirstName} {LastName} (ID: {Id})";
    }

   



}
