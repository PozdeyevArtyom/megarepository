/*Провести сравнительный анализ скорости работы классов String и StringBuilder для операции сложения строк.*/

using System;
using System.Diagnostics;
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
            //для анализа скорости работы был использован класс Stopwatch из пространства имён System.Diagnostics
            Stopwatch t = new Stopwatch();  
            string str = "";
            StringBuilder sb = new StringBuilder();
            int N = 100;

            
            t.Start();                  //включаем секундомер
            for (int i = 0; i < N; i++)
                str += "*";             //выполняем сложение
            t.Stop();                   //останавливаем секундомер
            Console.WriteLine("Время работы класса String:        {0}", t.Elapsed);

            t.Reset();                  //сбрасываем секундомер
            t.Start();                  //включаем секундомер
            for (int i = 0; i < N; i++)
                sb.Append("*");         //выполняем сложение
            t.Stop();                   //останавливаем секундомер
            Console.WriteLine("Время работы класса StringBuilder: {0}", t.Elapsed);
            Console.ReadKey();
        }
    }
}
