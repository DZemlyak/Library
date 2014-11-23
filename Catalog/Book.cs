using System;

namespace Library.Catalog
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
            return string.Format("Книга\n") + base.ToString() + string.Format("Автор: {0}\n", Author);
        }
    }
}
