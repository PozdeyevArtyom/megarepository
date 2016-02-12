using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            //определение констант
            const int n = 1000;
            const int divisor1 = 3;
            const int divisor2 = 5;

            int i;          //счётчик
            int sum = 0;    //сумма

            //подсчёт суммы
            for (i = 1; i < n; i++)
                if (i % divisor1 == 0 || i % divisor2 == 0)
                    sum += i;
            Console.WriteLine("Сумма всех натуральных чисел меньше {0},\nкоторые кратны {1}, или {2} равна {3}.",
                                                    n, divisor1, divisor2, sum);

            Console.ReadKey();
        }
    }
}
