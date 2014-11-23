using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Library.Catalog
{
    [Serializable]
    public class LibraryCollection<T> : ICollection<T> where T: CatalogItem
    {
        private const int Size = 10; 
        private object[] _objects = new object[Size];

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public IEnumerator<T> GetEnumerator() {
            return new LibraryCollectionEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new LibraryCollectionEnumerator<T>(this);
        }

        public void Add(T item)
        {
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

        public void Clear() {
            _objects = new object[Size];
            Count = 0;
        }

        public bool Contains(T item) {
            return _objects.Any(o => o == item);
        }

        public void CopyTo(T[] array, int arrayIndex) {
            foreach (var o in _objects) {
                array[arrayIndex] = (T) o;
            }
        }

        public bool Remove(T item) {
            for (int i = 0; i < Count; i++) {
                if (((T)_objects[i]).Id != item.Id) continue;
                RemoveAt(i);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index) {
            if (Count == 0) return;
            if (index < 0 && index >= Count) return;
            var temp = _objects[Count - 1];
            _objects[Count - 1] = _objects[index];
            _objects[index] = temp;
            Count--;
        }

        public int IndexOf(CatalogItem item) {
            for (int i = 0; i < Count; i++) {
                if (((T) _objects[i]).Id == item.Id) {
                    return i;
                }
            }
            return -1;
        }

        public T this[int i] {
            get { return (T) _objects[i]; }
            set { _objects[i] = value; }
        }
    }
}
