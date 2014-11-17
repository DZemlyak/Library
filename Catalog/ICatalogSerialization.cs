using System.IO;

namespace Catalog
{
    interface ICatalogSerialization
    {
        void SerializeCatalog(Catalog catalog, string path, FileMode fm);
        Catalog DeserializeCatalog(string path, FileMode fm);
    }

}
