using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task03
{
    /// <summary>
    /// Класс DynamicArray<T> реализует массив с запасом
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DynamicArray<T> : IEnumerable<T>
    {
        private T[] data;                           //массив с данными
        public int Length { get; private set; }     //длина массива
        public int Capacity { get; private set; }   //ёмкость массива

        /// <summary>
        /// Конструктор по умолчанию инициализирует пустой массив с ёмкостью, равной 8.
        /// </summary>
        public DynamicArray() : this(8) { }

        /// <summary>
        /// Конструктор с параметром int cap инициализирует пустой массив с ёмкостью, равной cap.
        /// </summary>
        /// <param name="cap"></param>
        public DynamicArray(int cap)
        {
            if (cap < 1)
                throw new ArgumentOutOfRangeException("Ёмкость массива должна быть натуральным числом.");
            else
            {
                Capacity = cap;
                Length = 0;
                data = new T[Capacity];
            }
        }

        /// <summary>
        /// Конструктор с параметром IEnumerable<T> e инициализирует массив ёмкостью, равной ближайщей степени 2, 
        /// большей чем количество элементов коллекции е, и копирует все элементы из е в массив. 
        /// </summary>
        /// <param name="e"></param>
        public DynamicArray(IEnumerable<T> e)
        {
            Length = e.Count();
            Capacity = 1;
            int i = 1;
            while (Capacity < Length)
                Capacity *= 2;
            data = new T[Capacity];
            i = 0;
            foreach (T t in e)
                data[i++] = t;
        }

        /// <summary>
        /// Метод Add добавляет элемент в конец массива. Ёмкость увеличивается в 2 раза при необходимости.
        /// </summary>
        /// <param name="elem"></param>
        public void Add(T elem)
        {
            if (Length == Capacity)
            {
                //увеличиваем ёмкость если достигли предела
                Capacity *= 2;
                T[] newdata = new T[Capacity];
                for (int i = 0; i < Length; i++)
                    newdata[i] = data[i];
                newdata[Length++] = elem;
                data = newdata;
                return;
            }
            else
                data[Length++] = elem;
        }

        /// <summary>
        /// Метод AddRange добавляет содержимое коллекции е в массив.
        /// Ёмкость удваивается пока этого не будет достаточно.
        /// </summary>
        /// <param name="e"></param>
        public void AddRange(IEnumerable<T> e)
        {
            while (Capacity <= Length + e.Count())
                Capacity *= 2;
            if(Capacity != data.Length)
            {
                T[] newdata = new T[Capacity];
                for (int i = 0; i < Length; i++)
                    newdata[i] = data[i];
                data = newdata;
            }
            foreach (T t in e)
                Add(t);
        }

        /// <summary>
        /// Метод Insert вставляет элемент elem в позицию index
        /// При нехватке места в массиве ёмкость удваивается
        /// При попытки вставить элемент за границу массива выбрасывается исключение IndexOutOfRangeException
        /// </summary>
        /// <param name="elem"></param>
        /// <param name="index"></param>
        public void Insert(T elem, int index)
        {
            if (index > Length)
                throw new IndexOutOfRangeException("Индекс вне диапазона.");
            else if (Length != Capacity)
            {
                int i;
                for (i = Length; i > index; i--)
                    data[i] = data[i - 1];
                data[i] = elem;
                Length++;
            }
            else
            {
                //увеличение ёмкости при необходимости
                Capacity *= 2;
                T[] newdata = new T[Capacity];
                for (int i = 0; i < Length; i++)
                    newdata[i] = data[i];
                data = newdata;
                Insert(elem, index);
            }
            return;
        }

        /// <summary>
        /// Метод Remove удаляет элемент elem из массива. Возвращает true, если элемент был удалён, false - в противном случае.
        /// </summary>
        /// <param name="elem"></param>
        /// <returns></returns>
        public bool Remove(T elem)
        {
            int index = 0;
            while (index < Length && !data[index].Equals(elem))
                index++;
            if (index < Length)
            {
                for (int i = index; i < Length - 1; i++)
                    data[i] = data[i + 1];
                Length--;
                return true;
            }
            else
                return false;

        }

        /// <summary>
        /// Перегрузка индексатора
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                if (index < Length)
                    return data[index];
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
            }
            set
            {
                if (index < Length)
                    data[index] = value;
                else
                    throw new IndexOutOfRangeException("Индекс вне диапазона.");
            }
        }

        /// <summary>
        /// Получение перечислителя
        /// </summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new DynamicArrayEnumerator<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
