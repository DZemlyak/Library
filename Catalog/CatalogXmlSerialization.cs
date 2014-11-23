using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Xml.Serialization;

namespace Library.Catalog
{
    public class CatalogXmlSerialization : ICatalogSerialization
    {
        public void SerializeCatalog(Catalog catalog, string path, FileMode fm) {
            using (var fs = new FileStream(path, fm)) {
                var types = GetSubclassTypesOfCatalog();
                var serializer = new XmlSerializer(typeof(Catalog), types);
                serializer.Serialize(fs, catalog);
            }
        }

        public Catalog DeserializeCatalog(string path, FileMode fm)
        {
            Catalog catalog;
            using (var fs = new FileStream(path, fm)) {
                var types = GetSubclassTypesOfCatalog();
                var serializer = new XmlSerializer(typeof(Catalog), types);
                catalog = (Catalog) serializer.Deserialize(fs);
            }
            return catalog;
        }

        private static Type[] GetSubclassTypesOfCatalog() {
            var type = typeof(CatalogItem);
            return Assembly.GetAssembly(type).GetTypes().Where(w => w.IsSubclassOf(type)).ToArray();
        }
    }
}
