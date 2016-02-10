using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    //абстрактный класс MyList описывает структуру данных список
    public abstract class MyList<T> : ICollection<T>, IEnumerable<T>, IDisposable, ICloneable
    {
        public int Count { get; protected set; }        //длина списка
        public bool IsReadOnly { get; }
        protected bool disposed = false;                // Для определения избыточных вызовов

        //метод Add добавляет элемент в конец списка
        public abstract void Add(T elem);

        //метод Clear очищает список
        public abstract void Clear();

        //метод Clone создаёт новый объект являющийся копией старого
        public abstract object Clone();

        //метод Contains проверят содержит ли список указанный элемент
        public abstract bool Contains(T elem);

        //метод CopyTo копирует список в массив начиная с указанного индекса массива
        public abstract void CopyTo(T[] array, int index);

        //метод Find возвращает индекс элемента elem
        public abstract int Find(T elem);

        //метод Get возвращает элемент с указанным индексом
        public abstract T Get(int index);

        //перегрузка индексатора
        public abstract T this[int index] { get; set; }

        //метод Insert вставляет новый элемент в список на определённую позицию
        public abstract void Insert(T elem, int index);

        //метод Remove удаляет элемент из списка
        public abstract bool Remove(T elem);

        //получение перечислятеля
        public IEnumerator<T> GetEnumerator()
        {
            return new MyListEnumerator<T>(this);
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

        ~MyList()
        {
            Dispose(false);
        }

        //переопределение стандартных методов
        public override bool Equals(object obj)
        {
            MyList<T> l = obj as MyList<T>;
            if (l == null || Count != l.Count)
                return false;
            else
            {
                bool b = true;
                int i = 0;
                while (i < Count && b == true)
                {
                    if (!this[i].Equals(l[i]))
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
            return sb.ToString().Substring(0, sb.ToString().Length);
        }
    }
}
