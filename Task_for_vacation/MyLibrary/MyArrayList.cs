using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    //класс MyArrayList реализует структуру данных список на основе массива
    public class MyArrayList<T> : MyList<T>
    {
        private T[] data;                               //массив значений
        public int Capacity { get; private set; }       //ёмкость массива (изначально 10, либо задаётся пользователем,
                                                        //в дальнейшем ёмкость увеличивается, если массив полностью заполняетcся)

        //конструторы
        public MyArrayList() : this(10) { }

        public MyArrayList(MyList<T> List)
        {
            Count = List.Count;

            MyArrayList<T> l = List as MyArrayList<T>;
            if (l != null)
                Capacity = l.Capacity;
            else
                Capacity = ((Count / 10) + 1) * 10;

            data = new T[Capacity];
            for (int i = 0; i < Count; i++)
                data[i] = List[i];
        }

        public MyArrayList(int capacity)
        {
            if (capacity > 0)
            {
                Count = 0;
                Capacity = capacity;
                data = new T[Capacity];
            }
            else
                throw new ArgumentOutOfRangeException("Capacity", "Вместимость списка должна быть натуральным числом.");
        }

        //определение методов предка

        //метод добавления нового элемента
        public override void Add(T elem)
        {
            if (Count == Capacity)
            {
                //увеличиваем ёмкость если достигли предела
                Capacity += 10;
                T[] newdata = new T[Capacity];
                for (int i = 0; i < Count; i++)
                    newdata[i] = data[i];
                newdata[Count++] = elem;
                data = newdata;
                return;
            }
            else
                data[Count++] = elem;
        }

        //метод очистки списка
        public override void Clear()
        {
            Capacity = 10;
            Count = 0;
            data = new T[Capacity];
        }

        //метод, возвращающий копию объекта
        public override object Clone()
        {
            return new MyArrayList<T>(this);
        }

        //метод проверки принадлежности элемента списку
        public override bool Contains(T elem)
        {
            return data.Contains(elem);
        }

        //метод копирования списка в массив
        public override void CopyTo(T[] array, int index)
        {
            for (int i = index; i < index + Count; i++)
                if (i < array.Length)
                    array[i] = data[i - index];
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
        }

        //метод Dispose
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    data = null;
                disposed = true;
            }
        }

        //метод поиска элемента
        public override int Find(T elem)
        {
            int i = 0;
            while (i < Count)
            {
                if (data[i].Equals(elem))
                    //возвращает его индекс в случае успеха
                    return i;
                i++;
            }
            //возвращает -1 в случае неуспеха
            return -1;
        }

        //метод, возвращающий элемент с указанным индексом
        public override T Get(int index)
        {
            if (index < Count)
                return data[index];
            else
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
        }

        //метод вставки элемента на определённую позицию в списке
        public override void Insert(T elem, int index)
        {
            if (index >= Count)
                //если указанная позиция больше кол-ва элементов, то новый элемент становится следующим за последним
                Add(elem);
            else if (Count != Capacity)
            {
                int i;
                for (i = Count; i > index; i--)
                    data[i] = data[i - 1];
                data[i] = elem;
                Count++;
            }
            else
            {
                //увеличение ёмкости при необходимости
                Capacity += 10;
                T[] newdata = new T[Capacity];
                for (int i = 0; i < Count; i++)
                    newdata[i] = data[i];
                data = newdata;
                Insert(elem, index);
            }
            return;
        }

        //метод удаления элемента
        public override bool Remove(T elem)
        {
            int index = Find(elem);
            if (index != -1)
            {
                for (int i = index; i < Count - 1; i++)
                    data[i] = data[i + 1];
                Count--;
                return true;
            }
            else
                return false;
        }

        //перегрузка индексатора
        public override T this[int index]
        {
            get
            {
                if (index < Count)
                    return data[index];
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
            }
            set
            {
                if (index < Count)
                    data[index] = value;
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
            }
        }
    }
}
