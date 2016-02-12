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
            int spaces; //количество пробелов от левого края экрана до центра треугольника
            int N;      //высота треугольнника
            int i, j;   //счётчики

            //ввод зачения
            Console.Write("Введите натуральное число: ");
            while (int.TryParse(Console.ReadLine(), out N) == false || N <= 0)
                Console.Write("Неверный ввод. Введите натуральное число: ");
            spaces = N - 1;

            //рисование
            for (i = 1; i <= 2 * N - 1; i = i + 2)
            {
                for (j = 0; j < spaces; j++)
                    Console.Write(" ");
                for (j = 1; j <= i; j++)
                    Console.Write("*");
                spaces--;
                Console.WriteLine();
            }

            Console.ReadKey();
        }
    }
}
