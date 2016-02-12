/*Написать программу, которая определяет сумму неотрицательных элементов в одномерном массиве. 
Число элементов в массиве и их тип определяется разработчиком.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] a;
            int i;
            int sum=0;

            Console.Write("Введите количество элементов в массиве: ");
            while (int.TryParse(Console.ReadLine(), out i) == false || i <= 0)
                Console.Write("Неверное значение, введите натуральное число: ");

            a = new int[i];
            Random r = new Random();
            Console.Write("Ваш массив:\n  ");
            for (i = 0; i < a.Length; i++)
            {
                a[i] = r.Next(-50, 50);
                Console.Write("{0} ", a[i]);
                if (a[i] >= 0)
                    sum += a[i];
            }
            Console.WriteLine("\nСумма неотрицательных элементов равна {0}", sum);
            Console.ReadKey();
        }
    }
}
