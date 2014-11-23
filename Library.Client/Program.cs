using Library.Catalog;

namespace Library.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var serializer = new CatalogXmlSerialization();
            var catalog = LibraryFileControl.LoadCatalogAsync(serializer).Result;
            IdGenerator.SetId(catalog);
            new LibraryMenuControl(catalog, serializer);
            LibraryFileControl.SaveCatalog(serializer, catalog);
        }
    }
}
