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
    }
}
