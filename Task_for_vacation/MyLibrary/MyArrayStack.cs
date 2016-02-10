using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class MyArrayStack<T> : MyStack<T>
    {
        private T[] data;
        public int Capacity { get; private set; }

        public MyArrayStack() : this(10) { }

        public MyArrayStack(int cap)
        {
            if (cap > 0)
            {
                Count = 0;
                Capacity = cap;
                data = new T[Capacity];
            }
            else
                throw new ArgumentOutOfRangeException("Capacity", "Вместимость стека должна быть натуральным числом.");
        }

        public MyArrayStack(MyStack<T> Stack)
        {
            Count = Stack.Count;
            MyArrayStack<T> s = Stack as MyArrayStack<T>;
            if (s != null)
                Capacity = s.Capacity;
            else
                Capacity = (Count / 10 + 1) * 10;
            data = new T[Capacity];
            s.CopyTo(data, 0);
        }

        //метод Add вызывает метод Push
        public override void Add(T item)
        {
            Push(item);
        }

        //метод очистки стека
        public override void Clear()
        {
            Capacity = 10;
            Count = 0;
            data = new T[Capacity];
        }

        //метод копирования стека
        public override object Clone()
        {
            return new MyArrayStack<T>(this);
        }

        //метод проверки наличия элемента в стеке
        public override bool Contains(T item)
        {
            return data.Contains(item);
        }

        //метод копирования очереди в массив
        public override void CopyTo(T[] array, int index)
        {
            for (int i = index; i < index + Count; i++)
                if (i < array.Length)
                    array[i] = data[i - index];
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
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

        public override T Peek()
        {
            if (Count == 0)
                throw new InvalidOperationException("Стек пуст.");
            else
                return data[Count - 1];
        }

        public override T Pop()
        {
            if (Count == 0)
                throw new InvalidOperationException("Стек пуст.");
            else
                return data[--Count];
        }

        public override void Push(T elem)
        {
            if (Count == Capacity)
            {
                Capacity += 10;
                T[] newdata = new T[Capacity];
                for (int i = 0; i < Count; i++)
                    newdata[i] = data[i];
                data = newdata;
            }
            data[Count++] = elem;
        }

        //неработающий метод, определён из-за интерфейса ICollection
        //для удаления элементов используется метод Pop
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
