using System;

namespace Library.Catalog
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
            return string.Format("Журнал\n") + base.ToString() + string.Format("Номер выпуска: {0}\n", NumberOfIssue);
        }
    }
}
