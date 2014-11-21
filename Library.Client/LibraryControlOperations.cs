using System;
using System.Collections.Generic;
using System.Linq;
using Catalog;
using CatalogController = Catalog.Catalog;

namespace Library.Client
{
    class LibraryControl
    {
        private static string _currentCatalog;

        public LibraryControl(CatalogController catalog, string currentCatalog) {
            _currentCatalog = currentCatalog;
            var menu = new Dictionary<string, Action<CatalogController>> {
                {"Добавить", Add},
                {"Изменить", Update},
                {"Удалить", Delete},
                {"Найти", Find},
                {"Напечатать товары", PrintItems},
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
                        Console.WriteLine(book);
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
                        Console.WriteLine(magazine);
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
                        Console.WriteLine("*** Книги ***");
                        Console.WriteLine("-------------------------------");
                        foreach (var item in catalog.CatalogItems
                            .Where(w => w.GetType() == typeof(Book)).Cast<Book>()) {
                                Console.WriteLine(item);
                        }
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("\nLEFT - назад.");
                    Console.ReadKey();
                }},
                { "Журнал", i => {
                    try {
                        if (catalog.CatalogItems.Count(w => w.GetType() == typeof(Magazine)) == 0)
                            throw new Exception("Таких товаров нет.");
                        Console.WriteLine("*** Журналы ***");
                        Console.WriteLine("-------------------------------");
                        foreach (var item in catalog.CatalogItems
                            .Where(w => w.GetType() == typeof(Magazine)).Cast<Magazine>()) {
                                Console.WriteLine(item);
                        }
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("\nLEFT - назад.");
                    Console.ReadKey();
                }},
                { "Все товары", i => {
                    try {
                        if (!catalog.CatalogItems.Any())
                            throw new Exception("Таких товаров нет.");
                        Console.WriteLine("*** Все товары ***");
                        Console.WriteLine("-------------------------------");
                        foreach (var item in catalog.CatalogItems
                            .Where(w => w.GetType() == typeof(Magazine)).Cast<Magazine>()) {
                                Console.WriteLine(item);
                        }
                        foreach (var item in catalog.CatalogItems
                            .Where(w => w.GetType() == typeof(Book)).Cast<Book>()) {
                                Console.WriteLine(item);
                        }
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("\nLEFT - назад.");
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
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow) {
                    position--;
                    if (position == 0)
                        position = menu.Count;
                }
                if (key.Key == ConsoleKey.DownArrow) {
                    if (position == menu.Count)
                        position = 0;
                    position++;
                }
                if (key.Key == ConsoleKey.LeftArrow) break;
                if (key.Key != ConsoleKey.Enter) continue;
                Console.Clear();
                Console.CursorVisible = true;
                menu.ElementAt(position - 1).Value(catalog);
            } while (true);
        }
        
        private static void PrintMenu(Dictionary<string, Action<CatalogController>> menu, int position)
        {
            var i = 1;
            Console.WriteLine("*** Меню ***\tКаталог: " + _currentCatalog);
            Console.WriteLine("-------------------------------");
            foreach (var m in menu) {
                Console.Write(i == position ? " -> " : "    ");
                Console.WriteLine(m.Key);
                i++;
            }
            Console.SetCursorPosition(0, Console.WindowHeight - 2);
            Console.WriteLine("UP/DOWN - вверх/вниз. LEFT - назад/выход. ENTER - выбор.");
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
