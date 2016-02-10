using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{
    public class MyLinkedList<T> : MyList<T>
    {
        private Node<T> first;     //ссылка на первый элемент списка

        //конструкторы
        public MyLinkedList()
        {
            first = null;
            Count = 0;
        }

        public MyLinkedList(MyList<T> List)
        {
            Count = List.Count;
            first = null;
            if (Count != 0)
            {
                first = new Node<T>(List[0]);
                Node<T> n = first;
                for (int i = 1; i < Count; i++)
                {
                    n.next = new Node<T>(List[i]);
                    n.next.prev = n;
                    n = n.next;
                }

            }

        }

        //определение методов предка

        //метод добавления нового элемента
        public override void Add(T elem)
        {
            if (first == null)
            {
                first = new Node<T>(elem);
                Count = 1;
            }
            else
            {
                Node<T> n = first;
                while (n.next != null)
                    n = n.next;
                n.next = new Node<T>(elem);
                n.next.prev = n;
                Count++;
            }
        }

        //метод очистки списка
        public override void Clear()
        {
            Count = 0;
            first = null;
        }

        //метод, возвращающий копию объекта
        public override object Clone()
        {
            return new MyLinkedList<T>(this);
        }

        //метод проверки принадлежности элемента списку
        public override bool Contains(T elem)
        {
            if (first == null)
                return false;
            else
            {
                Node<T> p = first;
                while (p != null && !elem.Equals(p.data))
                    p = p.next;
                if (p == null)
                    return false;
                else
                    return true;
            }
        }

        //метод копирования списка в массив
        public override void CopyTo(T[] array, int index)
        {
            Node<T> p = first;
            for (int i = index; i < index + Count; i++)
                if (i < array.Length)
                {
                    array[i] = p.data;
                    p = p.next;
                }
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
        }

        //метод Dispose
        protected override void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                    first = null;
                disposed = true;
            }
        }
        //метод поиска элемента
        public override int Find(T elem)
        {
            if (first == null)
                return -1;
            else
            {
                int i = 0;
                Node<T> n = first;
                while (n != null && !n.data.Equals(elem))
                {
                    n = n.next;
                    i++;
                }
                if (n == null)
                    //возвращает -1 в случае неуспеха
                    return -1;
                else
                    //возвращает его индекс в случае успеха
                    return i;
            }
        }

        //метод, возвращающий элемент с указанным индексом
        public override T Get(int index)
        {
            if (index < Count)
                return this[index];
            else
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
        }

        //метод вставки элемента на определённую позицию в списке
        public override void Insert(T elem, int index)
        {
            index--;
            if (first == null)
            {
                first = new Node<T>(elem);
                Count++;
            }
            else
            {
                int i = 0;
                Node<T> n = first;
                while (n.next != null && i != index)
                {
                    n = n.next;
                    i++;
                }
                if (i == index)
                {
                    Node<T> p = new Node<T>(elem);
                    p.prev = n;
                    p.next = n.next;
                    n.next.prev = p;
                    n.next = p;
                }
                else
                {
                    n.next = new Node<T>(elem);
                    n.next.prev = n;
                }
                Count++;
            }
        }

        //метод удаления элемента
        public override bool Remove(T elem)
        {
            if (first == null)
                return false;
            else
            {
                Node<T> n = first;
                while (n.next != null && !n.next.data.Equals(elem))
                    n = n.next;
                if (n.next == null)
                    return false;
                else
                {
                    n.next.next.prev = n;
                    n.next = n.next.next;
                    Count--;
                    return true;
                }
            }
        }

        //перегрузка индексатора
        public override T this[int index]
        {
            get
            {
                if (index < Count)
                {
                    Node<T> n = first;
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
                    Node<T> n = first;
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
