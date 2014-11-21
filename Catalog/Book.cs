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

        public override string ToString()
        {
            return string.Format("Книга\nID: {0}\nНазвание: {1}\nГод выпуска: {2}\nАвтор: {3}\n", 
                Id, Name, CreationYear, Author);
        }
    }
}
