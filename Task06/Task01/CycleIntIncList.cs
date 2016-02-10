using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    /// <summary>
    /// Класс CycleIntIncList реализует кольцевой двусвязный список с элементами 1, 2, 3, ..., N
    /// и минимальным функционалом, требуемым для выполнения задания.
    /// </summary>
    public class CycleIntIncList
    {
        private Node node;                      //поле node указывает на некоторый элемент списка
                                                //его значение изменяется в ходе работы программы

        public int Count { get; private set; }  //количество элементов в списке

        /// <summary>
        /// Конструктор по умолчанию инициализирует список с двумя элементами - 1, 2.
        /// </summary>
        public CycleIntIncList() : this(2) { }

        /// <summary>
        /// Конструктор с параметром инициализирует список длины n
        /// </summary>
        /// <param name="n"></param>
        public CycleIntIncList(int n)
        {
            if (n <= 1)
                throw new ArgumentOutOfRangeException("Длина списка дожна быть больше единицы.");
            else
            {
                Count = n;
                node = new Node(1);
                Node p = node;
                for (int i = 1; i < n; i++)
                {
                    p.next = new Node(i + 1);
                    p.next.prev = p;
                    p = p.next;
                }
                p.next = node;
                node.prev = p;
                node = node.prev;
            }
        }

        /// <summary>
        /// Метод Remove() удаляет элемент на который указывает node
        /// после удаления node будет указывать на элемент стоящий перед удалённым 
        /// </summary>
        public void Remove()
        {
            node = node.prev;
            node.next.next.prev = node;
            node.next = node.next.next;
            Count--;
        }

        /// <summary>
        /// Метод Start() начинает удалять каждый второй элемент в списке, пока в списке не останется 1 элемент
        /// </summary>
        public void Start()
        {
            while (Count > 1)
            {
                node = node.next.next;
                Remove();
            }
        }

        /// <summary>
        /// Метод Peek() возвращает значение, лежащее в node
        /// </summary>
        public int Peek()
        {
            return node.data;
        }
    }
}
