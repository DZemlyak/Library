using System;
using System.Collections.Generic;
using System.Linq;
using Library.Catalog.Model;
using Library.Catalog.Repository;
using CatalogController = System.Collections.Generic.List<Library.Catalog.Model.CatalogItem>;

namespace Library.Client
{
    public static class LibraryMenuControl
    {
        private static CatalogController _catalog;
        private static readonly RepositoryHelper _repository = new RepositoryHelper();

        public static void StartLibrary() {
            _catalog = _repository.LoadCatalog();
            var menu = new Dictionary<string, Action<CatalogController>> {
                {"Добавить", Add},
                {"Изменить", Update},
                {"Удалить", Delete},
                {"Найти", Find},
                {"Напечатать товары", PrintItems},
            };
            Menu(_catalog, menu);
        }

        #region Пункты меню

        private static void Add(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try {
                        var book = LibraryObjectsCreator.CreateBook();
                        _repository.BookRepository.Create(ref book);
                        catalog.Add(book);
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
                        _repository.MagazineRepository.Create(ref magazine);
                        catalog.Add(magazine);
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

        private static void Update(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try {
                        var id = GetId();
                        var book = catalog.Find(f => f.Id == id) as Book;
                        if(book == null) throw new InvalidCastException("Такой товар не найден.");
                        LibraryObjectsCreator.CreateBook(book);
                        _repository.BookRepository.Update(book);
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
                        var id = GetId();
                        var magazine = catalog.Find(f => f.Id == id) as Magazine;
                        if (magazine == null) throw new InvalidCastException("Такой товар не найден.");
                        LibraryObjectsCreator.CreateMagazine(magazine);
                        _repository.MagazineRepository.Update(magazine);
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

        private static void Delete(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try
                    {
                        var id = GetId();
                        var book = catalog.Find(f => f.Id == id) as Book;
                        if (book == null) throw new InvalidCastException("Такой товар не найден.");
                        _repository.BookRepository.Delete(id);
                        catalog.Remove(book);
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
                        var id = GetId();
                        var magazine = catalog.Find(f => f.Id == id) as Magazine;
                        if (magazine == null) throw new InvalidCastException("Такой товар не найден.");
                        _repository.MagazineRepository.Delete(id);
                        catalog.Remove(magazine);
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

        private static void Find(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try
                    {
                        var id = GetId();
                        var book = catalog.Find(f => f.Id == id) as Book;
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
                        var id = GetId();
                        var magazine = catalog.Find(f => f.Id == id) as Magazine;
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

        private static void PrintItems(CatalogController catalog)
        {
            var menu = new Dictionary<string, Action<CatalogController>> {
                { "Книга", i => {
                    try
                    {
                        if (catalog.Count(w => w.GetType() == typeof (Book)) == 0)
                            throw new Exception("Таких товаров нет.");
                        Console.WriteLine("*** Книги ***");
                        Console.WriteLine("-------------------------------");
                        foreach (var item in catalog
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
                        if (catalog.Count(w => w.GetType() == typeof(Magazine)) == 0)
                            throw new Exception("Таких товаров нет.");
                        Console.WriteLine("*** Журналы ***");
                        Console.WriteLine("-------------------------------");
                        foreach (var item in catalog
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
                        if (!catalog.Any())
                            throw new Exception("Таких товаров нет.");
                        Console.WriteLine("*** Все товары ***");
                        Console.WriteLine("-------------------------------");
                        foreach (var item in catalog
                            .Where(w => w.GetType() == typeof(Magazine)).Cast<Magazine>()) {
                                Console.WriteLine(item);
                        }
                        foreach (var item in catalog
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
