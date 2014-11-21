using System;

namespace Catalog
{
    [Serializable]
    public class Magazine : CatalogItem
    {
        public int NumberOfIssue { get; set; }

        public Magazine(string name, int date, int numberOfIssue) 
            : base(name, date) {
            NumberOfIssue = numberOfIssue;
        }
        public Magazine() { }

        public override string ToString()
        {
            return string.Format("Журнал\nID: {0}\nНазвание: {1}\nГод выпуска: {2}\nНомер выпуска: {3}\n",
                Id, Name, CreationYear, NumberOfIssue);
        }
    }
}
