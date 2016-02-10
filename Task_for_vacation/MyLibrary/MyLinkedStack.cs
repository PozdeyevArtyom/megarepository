using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class MyLinkedStack<T> : MyStack<T>
    {
        Node<T> Top;

        public MyLinkedStack()
        {
            Top = null;
        }

        public MyLinkedStack(MyStack<T> s)
        {
            Count = s.Count;
            T[] data = new T[Count];
            s.CopyTo(data, 0);
            Node<T> n = new Node<T>(data[0]);
            for (int i = 1; i < Count; i++)
            {
                n.next = new Node<T>(data[i]);
                n.next.prev = n;
                n = n.next;
            }
            Top = n;
        }

        //метод Add вызывает метод Enqueue
        public override void Add(T item)
        {
            Push(item);
        }

        //метод очистки стека
        public override void Clear()
        {
            Count = 0;
            Top = null;
        }

        //метод копирования объекта
        public override object Clone()
        {
            return new MyLinkedStack<T>(this);
        }
        //метод проверки наличия элемента в стеке
        public override bool Contains(T item)
        {
            Node<T> n = Top;
            while (n != null && !n.data.Equals(item))
                n = n.prev;
            if (n == null)
                return false;
            else
                return true;
        }

        //метод копирования cтека в массив
        public override void CopyTo(T[] array, int index)
        {
            Node<T> n = Top;
            while (n.prev != null)
                n = n.prev;
            for (int i = index; i < index + Count; i++)
                if (i < array.Length)
                {
                    array[i] = n.data;
                    n = n.next;
                }
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    Top = null;
                disposed = true;
            }
        }

        public override T Peek()
        {
            if (Top == null)
                throw new InvalidOperationException("Стек пуст.");
            return Top.data;
        }

        public override T Pop()
        {
            if (Top == null)
                throw new InvalidOperationException("Стек пуст.");
            else
            {
                T result = Top.data;
                Top = Top.prev;
                Top.next = null;
                Count--;
                return result;
            }
        }

        public override void Push(T elem)
        {
            if (Top == null)
                Top = new Node<T>(elem);
            else
            {
                Top.next = new Node<T>(elem);
                Top.next.prev = Top;
                Top = Top.next;
            }
            Count++;
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
                {
                    Node<T> n = Top;
                    while (n.prev != null)
                        n = n.prev;
                    for (int i = 0; i < index; i++)
                        n = n.next;
                    return n.data;
                }
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
            }
            set
            {
                if (index < Count)
                {
                    Node<T> n = Top;
                    while (n.prev != null)
                        n = n.prev;
                    for (int i = 0; i < index; i++)
                        n = n.next;
                    n.data = value;
                }
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
            }
        }
    }
}
