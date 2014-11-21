using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Catalog;

namespace Library.Client
{
    static class CatalogFileControl
    {
        private static string _path;
        private static readonly Dictionary<int, string> _catalogs 
            = new Dictionary<int, string>();

        /// <summary>
        /// Загружает один из файлов каталога
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="catalogName"></param>
        /// <returns></returns>
        public static Catalog.Catalog LoadCatalog(ICatalogSerialization serializer, out string catalogName)
        {
            _path = ListCatalogs();

            try {
                catalogName = _path;
                return serializer.DeserializeCatalog(_path, FileMode.Open);
            }
            catch (Exception) {
                Console.WriteLine("Каталог не может быть загружен.");
            }

            Console.ReadKey();
            _path = catalogName = "NewCatalog.xml";
            return new Catalog.Catalog();
        }

        /// <summary>
        /// Сохраняет или перезаписывает файл каталога
        /// </summary>
        /// <param name="serializer"></param>
        /// <param name="catalog"></param>
        public static void SaveCatalog(ICatalogSerialization serializer, Catalog.Catalog catalog)
        {
            var res = String.Empty;
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
                    Save(serializer, catalog, _path);
                    return;
                }
                _path = GetNewPath();
                Save(serializer, catalog, _path);
                return;
            }
            _path = GetNewPath();
            Save(serializer, catalog, _path);
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
        /// <param name="path"></param>
        private static void Save(ICatalogSerialization serializer, Catalog.Catalog catalog, string path)
        {
            try {
                serializer.SerializeCatalog(catalog, _path, FileMode.Create);
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
