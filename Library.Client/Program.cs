using Catalog;

namespace Library.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string catalogName;        
            var catalog = CatalogFileControl.LoadCatalog(new CatalogXmlSerialization(), out catalogName);
            new LibraryControl(catalog, catalogName);
            CatalogFileControl.SaveCatalog(new CatalogXmlSerialization(), catalog);
        }
    }
}
