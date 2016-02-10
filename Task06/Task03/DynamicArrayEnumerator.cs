using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task03
{
    public class DynamicArrayEnumerator<T> : IEnumerator<T>
    {
        private DynamicArray<T> Collection;
        private int curIndex;
        private T curElem;

        public DynamicArrayEnumerator(DynamicArray<T> arr)
        {
            Collection = arr;
            curIndex = -1;
            curElem = default(T);
        }

        object IEnumerator.Current
        {
            get { return Current; }
        }

        public T Current
        {
            get { return curElem; }
        }

        void IDisposable.Dispose() { }

        public bool MoveNext()
        {
            if (++curIndex >= Collection.Length)
                return false;
            else
                curElem = Collection[curIndex];
            return true;
        }

        public void Reset()
        {
            curIndex = -1;
        }
    }
}
