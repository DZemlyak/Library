using System;
using System.Xml.Serialization;

namespace Catalog
{
    [Serializable]
    public abstract class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreationYear { get; set; }
        private const int Min_ID = 100000;
        private const int Max_ID = 1000000;

        protected CatalogItem(string name, int creationYear) {
            var rand = new Random(DateTime.Now.Millisecond);
            Id = rand.Next(Min_ID, Max_ID);
            Name = name;
            CreationYear = creationYear;
        }
        protected CatalogItem() { }
    }
}
