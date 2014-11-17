using System;
using System.Linq;

namespace Catalog
{
    [Serializable]
    public class Catalog
    {
        private readonly LibraryCollection<CatalogItem> _catalog;

        public int Length { get { return _catalog.Count; } }
        
        public Catalog() {
            _catalog = new LibraryCollection<CatalogItem>();
        }

        public LibraryCollection<CatalogItem> CatalogItems { get { return _catalog; } }

        public void Add(CatalogItem item) {
            _catalog.Add(item);
        }

        public void Delete(CatalogItem item) {
            _catalog.Remove(item);
        }

        public void Update(CatalogItem item)
        {
            var index = _catalog.IndexOf(item);
            if (index != -1) {
                _catalog[index] = item;
            }
        }

        public CatalogItem FindItem(CatalogItem item) {
            var index = _catalog.IndexOf(item);
            return index != -1 ? _catalog[index] : null;
        }

        public CatalogItem FindItem(int id) {
            return _catalog.FirstOrDefault(f => f.Id == id);
        }

        public object this[int index] {
            get { return _catalog[index]; }
            set { _catalog[index] = (CatalogItem) value; }
        }
    }
}
