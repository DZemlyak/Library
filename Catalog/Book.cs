using System;

namespace Catalog
{
    [Serializable]
    
    public class Book : CatalogItem
    {
        public string Author { get; set; }

        public Book(string name, int date, string author) 
            : base(name, date) {
                Author = author;
        }
        public Book() { }
    }
}
