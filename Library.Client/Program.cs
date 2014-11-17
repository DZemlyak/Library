namespace Library.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var catalog = CatalogFileContol.FindCatalog();
            new LibraryControl(catalog);
            CatalogFileContol.SaveCatalog(catalog);
        }
    }
}
