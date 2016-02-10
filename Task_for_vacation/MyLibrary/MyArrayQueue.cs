using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    //класс MyArrayQueue реализует структуру данных очередь с помощью массива (циклический буфер)
    public class MyArrayQueue<T> : MyQueue<T>
    {
        private T[] data;       //массив данных
        public int Capacity { get; private set; }  //ёмкость массива
        private int head;       //начало очереди
        private int tail;       //конец очереди
        private bool full;      //флаг отображающий заполненность очереди


        //конструкторы
        public MyArrayQueue() : this(10) { }

        public MyArrayQueue(int cap)
        {
            if(cap > 0)
            {
                Count = 0;
                Capacity = cap;
                head = 0;
                tail = 0;
                full = false;
                data = new T[cap];
            }
            else
                throw new ArgumentOutOfRangeException("Capacity", "Вместимость очереди должна быть натуральным числом.");
        }

        public MyArrayQueue(MyQueue<T> Queue)
        {
            Count = Queue.Count;
            MyArrayQueue<T> q = Queue as MyArrayQueue<T>;
            head = 0;
            if (q != null)
            {
                Capacity = q.Capacity;
                tail = Count % Capacity;
                full = q.full;
            }
            else
            {
                Capacity = ((Count / 10) + 1) * 10;
                tail = Count % Capacity;
                full = (Count != 0) && (head == tail);
            }
            data = new T[Capacity];
            q.CopyTo(data, 0);

        }

        public MyArrayQueue(MyLinkedQueue<T> q)
        {
        }

        //определение абстрактных методов

        //метод Add вызывает метод Enqueue
        public override void Add(T item)
        {
            Enqueue(item);
        }

        //метод очистки очереди
        public override void Clear()
        {
            Capacity = 10;
            Count = 0;
            head = 0; tail = 0;
            full = false;
            data = new T[Capacity];
        }

        public override object Clone()
        {
            return new MyArrayQueue<T>(this);
        }

        //метод проверки принадлежности элемента очереди
        public override bool Contains(T item)
        {
            return data.Contains(item);
        }

        //метод копирования очереди в массив
        public override void CopyTo(T[] array, int index)
        {
            int p = head;
            for (int i = index; i < index + Count; i++)
                if (i < array.Length)
                {
                    array[i] = data[p];
                    p = (p + 1) % Capacity;
                }
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
        }

        //метод удаления элемента из очереди
        public override T Dequeue()
        {
            if (head == tail && !full)
                throw new InvalidOperationException("Очередь пуста.");
            else
            {
                if (full == true) full = false;
                T result = data[head];
                head = (head + 1) % Capacity;
                Count--;
                return result;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    data = null;
                disposed = true;
            }
        }

        //метод добавления элемента в начало очереди
        public override void Enqueue(T elem)
        {
            if (full)
            {
                //уведичение ёмкости при необходимости
                Capacity += 10;
                T[] newdata = new T[Capacity];
                newdata[0] = data[head];
                head = (head + 1) % (Capacity - 10);
                int i = 1;
                while (head != tail)
                {
                    newdata[i] = data[head];
                    head = (head + 1) % (Capacity - 10);
                    i++;
                }
                head = 0;
                newdata[Count++] = elem;
                tail = Count;
                data = newdata;
            }
            else
            {
                data[tail] = elem;
                tail = (tail + 1) % Capacity;
                Count++;
                if (head == tail) full = true;
            }
        }

        //метод, возвращающий первый элемент
        public override T Peek()
        {
            if (head == tail && !full)
                throw new InvalidOperationException("Очередь пуста.");
            else
                return data[head];
        }

        //неработающий метод, определён из-за интерфейса ICollection
        //для удаления элементов используется метод Dequeue
        public override bool Remove(T item)
        {
            return false;
        }

        protected override T this[int index]
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
