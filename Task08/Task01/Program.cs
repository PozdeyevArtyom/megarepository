using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] intArr = new int[10];
            double[] doubleArr = new double[10];

            //создаём случайные массивы
            Random r1 = new Random();
            Random r2 = new Random();
            for (int i = 0; i < 10; i++)
            {
                intArr[i] = r1.Next(-10, 10);
                doubleArr[i] = r2.NextDouble() * 20 - 10;
            }

            //вызываем метод расширения
            Console.WriteLine("Случайный целочисленный массив: ");
            foreach (int i in intArr)
                Console.Write("{0} ", i);
            Console.WriteLine("\nСумма его элементов: {0}\n", intArr.Sum());

            Console.WriteLine("Случайный массив чисел с плавающей точкой: ");
            foreach (double i in doubleArr)
                Console.Write("{0:0.00} ", i);
            Console.WriteLine("\nСумма его элементов: {0:0.00}", doubleArr.Sum());

            Console.ReadKey();
        }
    }
}
