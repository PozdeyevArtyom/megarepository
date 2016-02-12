using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            int N,i,j;  //объявление переменных

            //ввод значения
            Console.Write("Введите натуральное число: ");
            while (int.TryParse(Console.ReadLine(), out N) == false || N <= 0)
                Console.Write("Неверный ввод. Введите натуральное число: ");

            //рисование
            for (i = 1; i <= N; i++)
            {
                for (j = 1; j <= i; j++)
                    Console.Write("*");
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
