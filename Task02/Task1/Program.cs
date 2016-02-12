/*Написать программу которая генерирует случайным образом элементы массива (число элементов массива и их тип определяются
разработчиком), оппеделяет максимальное и минимальное значения, сортирует массив и выводит полученный результат на экран.

Примечание: LINQ запросы и готовые функции языка использовать запрещается*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a;                //массив
            int i,j;                //счётчики
            int min = 0, max = 0;   //индексы минимального и максимального элемента

            //ввод длины массива
            Console.Write("Введите количество элементов в массиве: ");
            while (int.TryParse(Console.ReadLine(), out i) == false || i <= 0)
                Console.Write("Неверное значение, введите натуральное число: ");

            a = new int[i];
            Random r = new Random();
            Console.Write("Ваш массив:\n  ");
            for (i = 0; i < a.Length; i++)
            {
                a[i] = r.Next(-50, 50);         //заполнение массива случайными элементами
                Console.Write("{0} ", a[i]);    //вывод массива на экран
            }
            Console.WriteLine();

            //поиск минимального элемента
            for (i = 1; i < a.Length; i++)
                if (a[min] > a[i])
                    min = i;
            //поиск максимального элемента
            for (i = 1; i < a.Length; i++)
                if (a[max] < a[i])
                    max = i;

            Console.WriteLine("Минимальный элемент массива находится на {0}-ой позиции и равен {1}.", min, a[min]);
            Console.WriteLine("Максимальный элемент массива находится на {0}-ой позиции и равен {1}.", max, a[max]);

            //сортировка обменом
            for (i = 0; i < a.Length-1; i++)
            {
                min = i;
                for (j = i + 1; j < a.Length; j++)
                    if (a[j] < a[min])
                        min = j;
                if (min != i) 
                {
                    int b = a[i];
                    a[i] = a[min];
                    a[min] = b;
                }
            }

            //вывод отсортированного массива на экран
            Console.Write("Отсортированный массив:\n  ");
            for (i = 0; i < a.Length; i++)       
                Console.Write("{0} ", a[i]);

            Console.ReadKey();
        }
    }
}
