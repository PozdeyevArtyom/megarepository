/*Написать программу, которая заменяет все положительные элементы в трёхмерном массиве на нули. 
Число элементов в массиве и их тип определяется разработчиком.*/

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
            //размерности массива
            const int dim1 = 2;
            const int dim2 = 5;
            const int dim3 = 5;

            int[,,] c = new int[dim1, dim2, dim3];  //массив
            int i, j, k;                            //счётчики

            //заполнение случайными значениями и вывод на экран
            Random r = new Random();
            Console.WriteLine("Ваш трёхмерный массив: ");
            for (i = 0; i < dim1; i++) 
            {
                Console.WriteLine("{0}-я матрица:", i);
                for (j = 0; j < dim2; j++) 
                {
                    Console.Write("  ");
                    for (k = 0; k < dim3; k++)
                    {
                        c[i, j, k] = r.Next(-50, 50);
                        Console.Write("{0,4}", c[i, j, k]);
                    }
                    Console.WriteLine();
                }
            }

            //замена положительных элементов на нули и вывод на экран
            Console.WriteLine("\nПреобразованный массив: ");
            for (i = 0; i < dim1; i++)
            {
                Console.WriteLine("{0}-я матрица:", i);
                for (j = 0; j < dim2; j++)
                {
                    Console.Write("  ");
                    for (k = 0; k < dim3; k++)
                    {
                        if (c[i, j, k] > 0)
                            c[i, j, k] = 0;
                        Console.Write("{0,4}", c[i, j, k]);
                    }
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
            
        }
    }
}
