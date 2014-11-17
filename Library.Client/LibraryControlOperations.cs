using System;
using System.Collections.Generic;
using System.Linq;
using Catalog;
using CatalogController = Catalog.Catalog;

namespace Library.Client
{
    class LibraryControl
    {
        public LibraryControl(CatalogController catalog) {
            var menu = new Dictionary<string, Action<CatalogController>>
            {
                {"Добавить", Add},
                {"Изменить", Update},
                {"Удалить", Delete},
                {"Найти", Find},
                {"Напечатать товары", PrintItems}
            };
            Menu(catalog, menu);
        }

        #region Пункты меню

        private static void Add(CatalogController catalog)
        {

            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try {
                        var book = LibraryObjectsCreator.CreateBook();
                        catalog.Add(book);
                        Console.WriteLine("Книга добавлена.");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
                { "Журнал", i => {
                    try {
                        var magazine = LibraryObjectsCreator.CreateMagazine();
                        catalog.Add(magazine);
                        Console.WriteLine("Журнал добавлен.");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
            };
            Menu(catalog, menu);
        }

        private static void Update(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try
                    {
                        var book = catalog.FindItem(GetId()) as Book;
                        if(book == null) throw new InvalidCastException("Такой товар не найден.");
                        LibraryObjectsCreator.CreateBook(book);
                        catalog.Update(book);
                        Console.WriteLine("Данные о книге обновлены.");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
                { "Журнал", i => {
                    try {
                        var magazine = catalog.FindItem(GetId()) as Magazine;
                        if (magazine == null) throw new InvalidCastException("Такой товар не найден.");
                        LibraryObjectsCreator.CreateMagazine(magazine);
                        catalog.Update(magazine);
                        Console.WriteLine("Данные о журнале обновлены.");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
            };
            Menu(catalog, menu);
        }

        private static void Delete(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try {
                        var book = catalog.FindItem(GetId()) as Book;
                        if (book == null) throw new InvalidCastException("Такой товар не найден.");
                        catalog.Delete(book);
                        Console.WriteLine("Книга удалена.");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
                { "Журнал", i => {
                    try {
                        var magazine = catalog.FindItem(GetId()) as Magazine;
                        if (magazine == null) throw new InvalidCastException("Такой товар не найден.");
                        catalog.Delete(magazine);
                        Console.WriteLine("Журнал удален.");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
            };
            Menu(catalog, menu);
        }

        private static void Find(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try {
                        var book = catalog.FindItem(GetId()) as Book;
                        if (book == null) throw new InvalidCastException("Такой товар не найден.");
                        PrintItem(book);
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
                { "Журнал", i => {
                    try {
                        var magazine = catalog.FindItem(GetId()) as Magazine;
                        if (magazine == null) throw new InvalidCastException("Такой товар не найден.");
                        PrintItem(magazine);
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
            };
            Menu(catalog, menu);
        }

        private static void PrintItems(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try
                    {
                        if (catalog.CatalogItems.Count(w => w.GetType() == typeof (Book)) == 0)
                            throw new Exception("Таких товаров нет.");
                        foreach (var item in catalog.CatalogItems
                            .Where(w => w.GetType() == typeof(Book)).Cast<Book>()) {
                            PrintItem(item);
                        }
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
                { "Журнал", i => {
                    try {
                        if (catalog.CatalogItems.Count(w => w.GetType() == typeof(Magazine)) == 0)
                            throw new Exception("Таких товаров нет.");
                        foreach (var item in catalog.CatalogItems
                            .Where(w => w.GetType() == typeof(Magazine)).Cast<Magazine>()) {
                            PrintItem(item);
                        }
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
                { "Все товары", i => {
                    try {
                        if (!catalog.CatalogItems.Any())
                            throw new Exception("Таких товаров нет.");

                        foreach (var item in catalog.CatalogItems
                            .Where(w => w.GetType() == typeof(Magazine)).Cast<Magazine>()) {
                            PrintItem(item);
                        }

                        foreach (var item in catalog.CatalogItems
                            .Where(w => w.GetType() == typeof(Book)).Cast<Book>()) {
                            PrintItem(item);
                        }
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
            };
            Menu(catalog, menu);
        }

        #endregion

        private static void Menu(CatalogController catalog, Dictionary<string, Action<CatalogController>> menu)
        {
            var position = 1;
            do
            {
                Console.CursorVisible = false;
                Console.Clear();
                PrintMenu(menu, position);
                Console.SetCursorPosition(0, Console.WindowHeight - 2);
                Console.WriteLine("Enter - выбор пункта меню. ESC - назад/выход.");
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow && position != 1) position--;
                if (key.Key == ConsoleKey.DownArrow && position != menu.Count) position++;
                if (key.Key == ConsoleKey.Escape) break;
                if (key.Key != ConsoleKey.Enter) continue;
                Console.Clear();
                Console.CursorVisible = true;
                menu.ElementAt(position - 1).Value(catalog);
            }
            while (true);
        }
        
        private static void PrintMenu(Dictionary<string, Action<CatalogController>> menu, int position)
        {
            var i = 1;
            Console.WriteLine("\t\t*** Каталог товаров ***");
            foreach (var m in menu) {
                Console.Write(i == position ? "> " : " ");
                Console.WriteLine(m.Key);
                i++;
            }
        }

        private static void PrintItem(Book book) {
            Console.WriteLine("Книга:");
            Console.WriteLine("ID: {0}\nНазвание: {1}\nГод выпуска: {2}\nАвтор: {3}\n",
                book.Id, book.Name, book.CreationYear, book.Author);
        }

        private static void PrintItem(Magazine magazine) {
            Console.WriteLine("Журнал:");
            Console.WriteLine("ID: {0}\nНазвание: {1}\nГод выпуска: {2}\nНомер выпуска: {3}\n",
                magazine.Id, magazine.Name, magazine.CreationYear, magazine.NumberOfIssue);
        }
        
        public static int GetId() {
            Console.Write("Введите ID товара: ");
            var res = Console.ReadLine();
            int id;
            if (!int.TryParse(res, out id)) {
                throw new ArgumentException("Неверно введен ID.");
            }
            return id;
        }
    }
}
