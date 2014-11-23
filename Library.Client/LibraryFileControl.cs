using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Library.Catalog;

namespace Library.Client
{
    static class LibraryFileControl
    {
        private static string _path;
        private const string DefaultPath = "newcatalog.xml";

        private static readonly Dictionary<int, string> _catalogs 
            = new Dictionary<int, string>();

        /// <summary>
        /// Загружает один из файлов каталога
        /// </summary>
        /// <param name="serializer"></param>
        /// <returns></returns>
        async public static Task<Catalog.Catalog> LoadCatalogAsync(ICatalogSerialization serializer)
        {
            try {
                _path = ListCatalogs();
                return await Task.Run(() => serializer.DeserializeCatalog(_path, FileMode.Open));
            }
            catch (FileNotFoundException ex) {
                Console.WriteLine(ex.Message);
            }
            catch (Exception) {
                Console.WriteLine("Каталог не может быть загружен.");
            }
            finally {
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
            }
            Console.ReadKey();
            _path = DefaultPath;
            return new Catalog.Catalog();
        }

        /// <summary>
        /// Сохраняет или перезаписывает файл каталога
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="catalog"></param>
        public static void SaveCatalog(ICatalogSerialization serializer, Catalog.Catalog catalog)
        {
            string res;
            Console.Clear();
            Console.CursorVisible = true;
            Console.WriteLine("Сохранение каталога.");

            if (catalog.Length == 0) {
                Console.WriteLine("Пустой каталог сохранен не будет.");
                return;
            }

            if (File.Exists(_path)) {
                Console.Write("Перезаписать текущий каталог {0}? ", _path);
                res = Console.ReadLine();
                if (res != null && res.ToLower() == "y") {
                    Save(serializer, catalog);
                    Console.ReadKey();
                    return;
                }
            }
            Console.Write("Создать новый каталог? ");
            res = Console.ReadLine();
            if (res != null && res.ToLower() == "y") {
                _path = GetNewPath();
                Save(serializer, catalog);
                Console.ReadKey();
                return;
            }
            Console.WriteLine("Каталог не сохранен.");
            Console.ReadKey();
        }

        #region Deserialize Help Methods

        /// <summary>
        /// Реализует меню выбора файла каталога
        /// </summary>
        /// <returns></returns>
        private static string ListCatalogs()
        {
            var position = 1;
            var files = GetCatalogsNames();
            if (files.Length == 0) throw new FileNotFoundException("Каталоги не найдены.");
            do {
                Console.CursorVisible = false;
                PrintCatalogs(files, position);
                _catalogs.Add(_catalogs.Count + 1, String.Empty);
                Console.SetCursorPosition(0, position);
                var key = Console.ReadKey();
                if(key.Key == ConsoleKey.UpArrow && position != 1)
                    position--;
                if(key.Key == ConsoleKey.DownArrow && position != files.Length)
                    position++;
                if (key.Key != ConsoleKey.Enter) continue;
                Console.Clear();
                return _catalogs.ElementAt(position - 1).Value;
            } while (true);
        }

        /// <summary>
        /// Находит файлы каталога и добавляет их в словарь
        /// </summary>
        /// <returns></returns>
        private static string[] GetCatalogsNames()
        {
            var files = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.xml");
            for (int i = 0, k = 1; i < files.Length; i++, k++) {
                files[i] = files[i].Substring(files[i].LastIndexOf('\\') + 1);
                _catalogs.Add(k, files[i]);
            }
            return files;
        }

        /// <summary>
        /// Выводит список найденныых каталогов
        /// </summary>
        /// <param name="files"></param>
        /// <param name="position"></param>
        private static void PrintCatalogs(IEnumerable<string> files, int position)
        {
            var i = 1;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("Найдены каталоги:");
            foreach (var file in files) {
                Console.Write(i == position ? " -> " : "    ");
                Console.WriteLine(file);
                i++;
            }
        }

        #endregion

        #region Serialize Help Methods

        /// <summary>
        /// Метод запускает сериализацию
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="catalog"></param>
        private static void Save(ICatalogSerialization serializer, Catalog.Catalog catalog)
        {
            try {
                serializer.SerializeCatalog(catalog, _path, FileMode.Create);
                Console.WriteLine("Каталог {0} сохранен.", _path);
            }
            catch (Exception) {
                Console.WriteLine("Каталог {0} не сохранен.", _path);
            }
        }

        public async static void SaveAsync(ICatalogSerialization serializer, Catalog.Catalog catalog)
        {
            try {
                await Task.Run(() => serializer.SerializeCatalog(catalog, _path, FileMode.Create));
                Console.WriteLine("Каталог {0} сохранен.", _path);
            }
            catch (Exception) {
                Console.WriteLine("Каталог {0} не сохранен.", _path);
            }
        }

        /// <summary>
        /// Запрашивает новое имя файла для каталога
        /// </summary>
        /// <returns></returns>
        private static string GetNewPath() {
            Console.Write("Введите имя файла для сохранения каталога: ");
            var path = Console.ReadLine();
            while (path != null && !path.Any(char.IsLetter)) {
                Console.WriteLine("Укажите только название файла.");
                Console.Write("Введите имя файла для сохранения каталога: ");
                path = Console.ReadLine();
            }
            return path + ".xml";
        }

        #endregion
    }
}
