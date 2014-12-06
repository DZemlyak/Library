using System.Collections.Generic;
using Library.Catalog.Model;

namespace Library.Catalog.Repository
{
    public class RepositoryHelper
    {
        public readonly BookRepository BookRepository = new BookRepository();
        public readonly MagazineRepository MagazineRepository = new MagazineRepository();
        
        public List<CatalogItem> LoadCatalog()
        {
            var list = new List<CatalogItem>();
            list.AddRange(BookRepository.GetItemsList());
            list.AddRange(MagazineRepository.GetItemsList());
            return list;
        }

    }
}
