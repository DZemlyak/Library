using System;
using Library.Catalog;

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
            while (!int.TryParse(temp, out numberOfIssue)) {
                Console.Write("\nНеверный номер выпуска!\n");
                Console.Write("Номер выпуска: ");
                temp = Console.ReadLine();
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
            while (!int.TryParse(temp, out creationYear)) {
                Console.WriteLine("\nНеверный год выпуска!\n");
                Console.Write("Год выпуска: ");
                temp = Console.ReadLine();
            }
        }
    }
}
