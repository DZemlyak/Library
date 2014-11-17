using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Catalog
{
    [Serializable]
    public class LibraryCollection<T> : ICollection<T> where T: CatalogItem
    {
        private const int Size = 10; 
        private object[] _objects = new object[Size];

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public IEnumerator<T> GetEnumerator()
        {
            return new LibraryCollectionEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new LibraryCollectionEnumerator<T>(this);
        }

        public void Add(T item)
        {
            if (Count != 0) {
                for (int i = 0; i < Count; i++) {
                    var catalogItem = _objects[i] as CatalogItem;
                    if (catalogItem != null && catalogItem.Id == item.Id)
                        throw new ArgumentException("Товар с таким ID уже существует. Повторите добавление товара.");
                }
            }
 
            if (Count >= _objects.Length) {
                var temp = new object[_objects.Length + Size];
                for (int i = 0; i < Count; i++) {
                    temp[i] = _objects[i];
                }
                _objects = temp;
            }
            _objects[Count] = item;
            Count++;
        }

        public void Clear()
        {
            _objects = new object[Size];
            Count = 0;
        }

        public bool Contains(T item)
        {
            return _objects.Any(o => o == item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach (var o in _objects) {
                array[arrayIndex] = (T) o;
            }
        }

        public bool Remove(T item)
        {
            for (int i = 0; i < _objects.Length; i++) {
                if (((T)_objects[i]).Id != item.Id) continue;;
                RemoveAt(i);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (index < 0 && index >= Count) return;
            for (int i = index; i < Count; i++) {
                _objects[i] = _objects[i + 1];
            }
            Count--;
        }

        public int IndexOf(CatalogItem item)
        {
            for (int i = 0; i < _objects.Length; i++) {
                if (((T) _objects[i]).Id == item.Id) {
                    return i;
                }
            }
            return -1;
        }

        public T this[int i]
        {
            get { return (T) _objects[i]; }
            set { _objects[i] = value; }
        }
    }

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
