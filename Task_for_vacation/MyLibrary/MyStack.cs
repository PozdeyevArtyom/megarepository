using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public abstract class MyStack<T> : ICollection<T>, IEnumerable<T>, ICloneable, IDisposable
    {
        public int Count { get; protected set; }
        public bool IsReadOnly { get; }
        protected bool disposed = false;

        //перегрузка индексатора (для простоты внутренней работы)
        protected abstract T this[int index] { get; set; }

        //метод Add добавляет элемент на вершину стека (этот метод должен был быть определён из за интерфейса ICollection)
        public abstract void Add(T item);

        //метод Clear очищает стек
        public abstract void Clear();

        //метод Clone создаёт новый объект являющийся копией старого
        public abstract object Clone();

        //метод Contains проверят содержит ли стек указанный элемент
        public abstract bool Contains(T item);

        //метод CopyTo копирует стек в массив начиная с указанного индекса массива
        public abstract void CopyTo(T[] array, int arrayIndex);

        //метод Peek возвращает вершину стека
        public abstract T Peek();

        //метод Pop извлекает вершину стека
        public abstract T Pop();

        //метод Push добавляет элемент в стек
        public abstract void Push(T elem);

        //метод Remove не работает (этот метод должен был быть определён из за интерфейса ICollection)
        //для удаления элементов используется метод Pop
        public abstract bool Remove(T item);

        //получение перечеслителя
        public IEnumerator<T> GetEnumerator()
        {
            return new MyStackEnumerator<T>(this);
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

        ~MyStack()
        {
            Dispose(false);
        }



        //переопределение стандартных методов

        public override bool Equals(object obj)
        {
            MyStack<T> s = obj as MyStack<T>;
            if (s == null || Count != s.Count)
                return false;
            else
            {
                bool b = true;
                int i = 0;
                while (i < Count && b == true)
                {
                    if (!this[i].Equals(s[i]))
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
            for (int i = 0; i < Count; i++)
            {
                sb.Append(this[i]);
                sb.Append(" ");
            }

            return sb.ToString().Substring(0, sb.ToString().Length - 1);
        }
    }
}
