using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyLibrary
{

    //класс Node описывает ячейку со значением и ссылками на соседние элементы
    public class Node<T>
    {
        public T data;
        public Node<T> next;
        public Node<T> prev;

        public Node(T d)
        {
            data = d;
            next = null;
            prev = null;
        }
    }
}
