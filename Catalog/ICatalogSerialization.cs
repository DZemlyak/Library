using System.IO;

namespace Library.Catalog
{
    public interface ICatalogSerialization
    {
        void SerializeCatalog(Catalog catalog, string path, FileMode fm);
        Catalog DeserializeCatalog(string path, FileMode fm);
    }

}
