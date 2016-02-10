using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class MyLinkedQueue<T> : MyQueue<T>
    {
        private Node<T> Head;   //начало
        private Node<T> Tail;   //конец

        //конструкторы
        public MyLinkedQueue()
        {
            Head = null;
            Tail = null;
        }

        public MyLinkedQueue(MyQueue<T> Queue)
        {
            Head = null;
            Tail = null;
            Count = Queue.Count;
            T[] data = new T[Count];
            Queue.CopyTo(data, 0);
            Head = new Node<T>(data[0]);
            Node<T> n = Head;
            for (int i = 1; i < Count; i++)
            {
                n.next = new Node<T>(data[i]);
                n.next.prev = n;
                n = n.next;
            }
            Tail = n;
        }

        //метод Add вызывает метод Enqueue
        public override void Add(T item)
        {
            Enqueue(item);
        }

        //метод очистки очереди
        public override void Clear()
        {
            Count = 0;
            Head = null;
            Tail = null;
        }

        public override object Clone()
        {
            return new MyLinkedQueue<T>(this);
        }

        //метод проверки принадлежности элемента очереди
        public override bool Contains(T item)
        {
            Node<T> n = Head;
            while (n != Tail && !n.data.Equals(item))
                n = n.next;
            if (n == Tail)
                return false;
            else
                return true;
        }

        //метод копирования очереди в массив
        public override void CopyTo(T[] array, int index)
        {
            Node<T> n = Head;
            for (int i = index; i < index + Count; i++)
                if (i < array.Length)
                {
                    array[i] = n.data;
                    n = n.next;
                }
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
        }

        //метод удаления элемента из очереди
        public override T Dequeue()
        {
            if (Head == null)
                throw new InvalidOperationException("Очередь пуста.");
            else
            {
                T result = Head.data;
                if (Head.next == null)
                {
                    Head = null;
                    Tail = null;
                }
                else
                {
                    Head.next.prev = null;
                    Head = Head.next;
                }
                Count--;
                return result;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Head = null;
                    Tail = null;
                }
                disposed = true;
            }
        }

        //метод добавления элемента в начало очереди
        public override void Enqueue(T elem)
        {
            if (Head == null)
            {
                Head = new Node<T>(elem);
                Tail = Head;
                Count++;
            }
            else
            {
                Tail.next = new Node<T>(elem);
                Tail.next.prev = Tail;
                Tail = Tail.next;
                Count++;
            }
        }

        //метод, возвращающий первый элемент
        public override T Peek()
        {
            if (Head == null)
                throw new InvalidOperationException("Очередь пуста.");
            else
                return Head.data;
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
                {
                    Node<T> n = Head;
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
                    Node<T> n = Head;
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
