using System;

namespace Library.Catalog
{
    [Serializable]
    public abstract class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CreationYear { get; set; }

        protected CatalogItem(string name, int creationYear) {
            Id = IdGenerator.GetId();
            Name = name;
            CreationYear = creationYear;
            IdGenerator.IncrementId();
        }
        protected CatalogItem() { }

        public override string ToString()
        {
            return string.Format("ID: {0}\nНазвание: {1}\nГод выпуска: {2}\n",
                Id, Name, CreationYear);
        }
    }
}
