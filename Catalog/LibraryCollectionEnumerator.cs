using System.Collections;
using System.Collections.Generic;

namespace Library.Catalog
{
    class LibraryCollectionEnumerator<T> : IEnumerator<T> where T : CatalogItem
    {
        private readonly LibraryCollection<T> _collection;
        private int _index;

        public LibraryCollectionEnumerator(LibraryCollection<T> libraryCollection) {
            _collection = libraryCollection;
            _index = -1;
        }

        public void Dispose() { }

        public bool MoveNext() {
            if (++_index >= _collection.Count) {
                return false;
            }
            Current = _collection[_index];
            return true;
        }

        public void Reset() { _index = -1; }

        public T Current { get; private set; }

        object IEnumerator.Current {
            get { return Current; }
        }
    }
}
