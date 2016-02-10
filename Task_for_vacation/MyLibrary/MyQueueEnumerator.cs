using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class MyQueueEnumerator<T> : IEnumerator<T>
    {
        private T[] Collection;
        private int curIndex;
        private T curElem;

        public MyQueueEnumerator(MyQueue<T> q)
        {
            Collection = new T[q.Count];
            q.CopyTo(Collection, 0);
            curIndex = -1;
            curElem = default(T);
        }

        public T Current
        {
            get { return curElem; }
        }

        object IEnumerator.Current
        {
            get { return Current; }
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
