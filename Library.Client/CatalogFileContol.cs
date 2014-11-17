using System;
using System.IO;
using Catalog;

namespace Library.Client
{
    static class CatalogFileContol
    {
        private const string Path = "catalog.xml";

        public static Catalog.Catalog FindCatalog()
        {
            if (!File.Exists(Path)) return new Catalog.Catalog();
            Console.Write("Найден файл каталога. Загрузить? ");
            var res = Console.ReadLine();
            if (res != null && res.ToLower() == "y") {
                return new CatalogXmlSerialization().DeserializeCatalog(Path, FileMode.Open);
            }
            Console.WriteLine("Каталог не загружен. Нажмите, чтобы продолжить...");
            Console.ReadKey();
            return new Catalog.Catalog();
        }

        public static void SaveCatalog(Catalog.Catalog catalog)
        {
            Console.Clear();
            Console.WriteLine("Сохранение каталога.");
            if (!File.Exists(Path)) {
                new CatalogXmlSerialization().SerializeCatalog(catalog, Path, FileMode.Create);
            }
            else {
                Console.Write("Файл каталога найден. Перезаписать? ");
                var res = Console.ReadLine();
                if (res != null && res.ToLower() == "y") {
                    new CatalogXmlSerialization().SerializeCatalog(catalog, Path, FileMode.Create);
                    Console.WriteLine("Каталог сохранен. Нажмите, чтобы продолжить...");
                    return;
                }
                Console.WriteLine("Каталог не сохранен. Нажмите, чтобы продолжить...");
                Console.ReadKey();
            }
        }
    }
}
