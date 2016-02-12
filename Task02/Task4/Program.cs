/*Элемент двумерного массива считается стоящим на чётной позиции, если сумма номеров его позиций по обеим размерностям
является чётным числом. Определить сумму элементов массива, стоящих на чётных позициях.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            //размерности массива
            const int dim1 = 3;
            const int dim2 = 3;

            int[,] m = new int[dim1, dim2]; //массив
            int i, j;                       //счётчики
            int sum=0;                      //сумма


            Random r = new Random();
            Console.WriteLine("Ваша матрица:");
            for (i = 0; i < dim1; i++)
            {
                for (j = 0; j < dim2; j++)
                {
                    m[i, j] = r.Next(-50, 50);          //заполнение случайными элементами
                    Console.Write("{0,4}", m[i, j]);    //вывод на экран
                    if ((i + j) % 2 == 0)
                        sum += m[i, j];                 //подсчёт суммы
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nСумма элементов, стоящих на чётной позиции равна {0}", sum);
            Console.ReadKey();
        }
    }
}
