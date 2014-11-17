using System;
using Catalog;

namespace Library.Client
{
    static class LibraryObjectsCreator
    {
        public static Book CreateBook(Book book = null)
        {
            string name;
            int year;
            GetGeneralData(out name, out year);
            Console.Write("Автора: ");
            var author = Console.ReadLine();

            if (book == null) return new Book(name, year, author);
            book.Name = name;
            book.CreationYear = year;
            book.Author = author;
            return book;
        }

        public static Magazine CreateMagazine(Magazine magazine = null)
        {
            string name;
            int year, numberOfIssue;
            GetGeneralData(out name, out year);
            Console.Write("Номер выпуска: ");
            var temp = Console.ReadLine();
            if (!int.TryParse(temp, out numberOfIssue)) {
                throw new ArgumentException("Неверный номер выпуска!");
            }

            if (magazine == null) return new Magazine(name, year, numberOfIssue);
            magazine.Name = name;
            magazine.CreationYear = year;
            magazine.NumberOfIssue = numberOfIssue;
            return magazine;
        }

        private static void GetGeneralData(out string name, out int creationYear)
        {
            Console.WriteLine("Ведите данные.");
            Console.Write("Название: ");
            name = Console.ReadLine();
            Console.Write("Год выпуска: ");
            var temp = Console.ReadLine();
            if (!int.TryParse(temp, out creationYear)) {
                throw new ArgumentException("Неверный год выпуска!");
            }
        }
    }
}
