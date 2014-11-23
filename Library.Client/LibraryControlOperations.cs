using System;
using System.Collections.Generic;
using System.Linq;
using Library.Catalog;
using CatalogController = Library.Catalog.Catalog;

namespace Library.Client
{
    class LibraryMenuControl
    {
        private readonly ICatalogSerialization _serializer;

        private delegate void OnChangeSaver(ICatalogSerialization serializer, Catalog.Catalog catalog);
        private event OnChangeSaver Saver = LibraryFileControl.SaveAsync;

        public LibraryMenuControl(CatalogController catalog, ICatalogSerialization serializer) {
            _serializer = serializer;
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

        private void Add(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try {
                        var book = LibraryObjectsCreator.CreateBook();
                        catalog.Add(book);
                        Saver(_serializer, catalog);
                        Console.WriteLine("\nКнига добавлена.");
                        Console.WriteLine("\nНажмите любую клавишу.");
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
                        Saver(_serializer, catalog);
                        Console.WriteLine("\nЖурнал добавлен.");
                        Console.WriteLine("\nНажмите любую клавишу.");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
            };
            Menu(catalog, menu);
        }

        private void Update(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try
                    {
                        var book = catalog.FindItem(GetId()) as Book;
                        if(book == null) throw new InvalidCastException("Такой товар не найден.");
                        LibraryObjectsCreator.CreateBook(book);
                        catalog.Update(book);
                        Saver(_serializer, catalog);
                        Console.WriteLine("\nДанные о книге обновлены.");
                        Console.WriteLine("\nНажмите любую клавишу.");
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
                        Saver(_serializer, catalog);
                        Console.WriteLine("\nДанные о журнале обновлены.");
                        Console.WriteLine("\nНажмите любую клавишу.");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
            };
            Menu(catalog, menu);
        }

        private void Delete(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try {
                        var book = catalog.FindItem(GetId()) as Book;
                        if (book == null) throw new InvalidCastException("Такой товар не найден.");
                        catalog.Delete(book);
                        Saver(_serializer, catalog);
                        Console.WriteLine("\nКнига удалена.");
                        Console.WriteLine("\nНажмите любую клавишу.");
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
                        Saver(_serializer, catalog);
                        Console.WriteLine("\nЖурнал удален.");
                        Console.WriteLine("\nНажмите любую клавишу.");
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.ReadKey();
                }},
            };
            Menu(catalog, menu);
        }

        private void Find(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try {
                        var book = catalog.FindItem(GetId()) as Book;
                        if (book == null) throw new InvalidCastException("Такой товар не найден.");
                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.WriteLine(book);
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("\nНажмите любую клавишу.");
                    Console.ReadKey();
                }},
                { "Журнал", i => {
                    try {
                        var magazine = catalog.FindItem(GetId()) as Magazine;
                        if (magazine == null) throw new InvalidCastException("Такой товар не найден.");
                        Console.CursorVisible = false;
                        Console.Clear();
                        Console.WriteLine(magazine);
                    }
                    catch (Exception ex) {
                        Console.WriteLine(ex.Message);
                    }
                    Console.WriteLine("\nНажмите любую клавишу.");
                    Console.ReadKey();
                }},
            };
            Menu(catalog, menu);
        }

        private void PrintItems(CatalogController catalog)
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
                    Console.WriteLine("\nНажмите любую клавишу.");
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
                    Console.WriteLine("\nНажмите любую клавишу.");
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
                    Console.WriteLine("\nНажмите любую клавишу.");
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
            Console.WriteLine("*** Меню ***");
            Console.WriteLine("-------------------------------");
            foreach (var m in menu) {
                Console.Write(i == position ? " -> " : "    ");
                Console.WriteLine(m.Key);
                i++;
            }
            Console.SetCursorPosition(0, Console.WindowHeight - 3);
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
