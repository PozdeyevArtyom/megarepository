using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    //абстрактный класс MyQueue описывает структуру данных очередь
    public abstract class MyQueue<T> : ICollection<T>, IEnumerable<T>, ICloneable, IDisposable
    {
        public int Count { get; protected set; }
        public bool IsReadOnly { get; }
        protected bool disposed = false;

        //перегрузка индексатора (для простоты внутренней работы)
        protected abstract T this[int index] { get; set; }

        //метод Add добавляет элемент в начало очереди (этот метод должен был быть определён из за интерфейса ICollection)
        public abstract void Add(T item);

        //метод Clear очищает очередь
        public abstract void Clear();

        //метод Clone создаёт новый объект являющийся копией старого
        public abstract object Clone();

        //метод Contains проверят содержит ли очередь указанный элемент
        public abstract bool Contains(T item);

        //метод CopyTo копирует очередь в массив начиная с указанного индекса массива
        public abstract void CopyTo(T[] array, int arrayIndex);

        //метод Dequeue возвращает первый элемент очереди и удаляет его
        public abstract T Dequeue();

        //метод Enqueue добавляет элемент в начало очереди 
        public abstract void Enqueue(T elem);

        //метод Peek возвращает первый элемент очереди
        public abstract T Peek();

        //метод Remove не работает (этот метод должен был быть определён из за интерфейса ICollection)
        //для удаления элементов используется метод Dequeue
        public abstract bool Remove(T item);

        //получение перечеслителя
        public IEnumerator<T> GetEnumerator()
        {
            return new MyQueueEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //метод Dispose
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Dispose(bool disposing);

        ~MyQueue()
        {
            Dispose(false);
        }

        //переопределение стандартных методов

        public override bool Equals(object obj)
        {
            MyQueue<T> q = obj as MyQueue<T>;
            if (q == null || Count != q.Count)
                return false;
            else
            {
                T[] arr1 = new T[Count];
                T[] arr2 = new T[Count];
                CopyTo(arr1, 0);
                q.CopyTo(arr2, 0);
                bool b = true;
                int i = 0;
                while (i < Count && b == true)
                {
                    if (!arr1[i].Equals(arr2[i]))
                        b = false;
                    i++;
                }
                return b;
            }
        }

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < Count; i++)
                sb.Append(this[i]);
            return int.Parse(sb.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            T[] arr = new T[Count];
            CopyTo(arr, 0);
            for (int i = 0; i < Count; i++)
            {
                sb.Append(arr[i]);
                sb.Append(" ");
            }

            return sb.ToString().Substring(0, sb.ToString().Length - 1);
        }
    }
}
