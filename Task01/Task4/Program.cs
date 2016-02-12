using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static class Triangle
        {
            //метод draw рисует треугольник высотой N, с некоторым отступом от левого края
            static public void draw(int N, int spaces)
            {
                int i, j;   //счётчики

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
            }
        }

        static void Main(string[] args)
        {
            int i;      //счётчик
            int N;      //высота нижнего треугольника

            //ввод значения
            Console.Write("Введите натуральное число: ");
            while (int.TryParse(Console.ReadLine(), out N) == false || N <= 0)
                Console.Write("Неверный ввод. Введите натуральное число: ");

            //рисование
            for (i = 1; i <= N; i++)
                Triangle.draw(i, N-1);  //N-1 - расстояние от левого края экрана до центра "ёлочки"

            Console.ReadKey();
        }
    }
}
