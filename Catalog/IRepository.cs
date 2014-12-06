using System.Collections.Generic;
using Library.Catalog.Model;

namespace Library.Catalog
{
    interface IRepository <T> where T : CatalogItem
    {
        List<T> GetItemsList();
        //T GetItem(int id);
        void Create(ref T item);
        void Update(T item);
        void Delete(int id);
    }
}
