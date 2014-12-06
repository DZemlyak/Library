using System;

namespace Library.Catalog.Model
{
    [Serializable]
    public abstract class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationYear { get; set; }

        protected CatalogItem(string name, DateTime creationYear) {
            Name = name;
            CreationYear = creationYear;
        }
        protected CatalogItem() { }

        public override string ToString()
        {
            return string.Format("ID: {0}\nНазвание: {1}\nГод выпуска: {2} г.\n",
                Id, Name, CreationYear.Year);
        }
    }
}
