using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Task03
{
    class Program
    {
        //делегат для проверки некоторого условия
        public delegate bool Condition(int i);

        //метод сравнения
        static public bool IsPositive(int i)
        {
            return i > 0;
        }

        //метод поиска положительных элементов массива array
        static public IEnumerable<int> FindPositiveElements(int[] array)
        {
            var list = new List<int>();
            foreach (int i in array)
                if (i > 0)
                    list.Add(i);
            return list;
        }

        //метод поиска элементов массива array, удовлетворяющих условию cond
        static public IEnumerable<int> FindByCondition(int[] array, Condition cond)
        {
            var list = new List<int>();
            foreach (int i in array)
                if (cond(i))
                    list.Add(i);
            return list;
        }        

        //метод поиска положительных элементов массива array используюя LINQ-выражения
        static public IEnumerable<int> FindWithLinq(int[] array)
        {
            var list = from item in array
                       where item > 0
                       select item;
            return list.ToArray();
        }

        static void Main(string[] args)
        {
            //инициализация массива для проверки
            Random r1 = new Random();
            int[] arr = new int[500];
            for (int i = 0; i < 500; i++)
                arr[i] = r1.Next(-50, 50);

            //массив для хранения результатов
            long[] result = new long[25];

            //секундомер
            Stopwatch stopwatch = new Stopwatch();

            Condition cond1 = IsPositive;                           //обычный делегат
            Condition cond2 = delegate (int i) { return i > 0; };   //анонимный делегат
            Condition cond3 = (i) => i > 0;                         //лямбда-выражение
            IEnumerable<int> e;                                     //коллекция для получения результата

            //подсчёт
            for (int i = 0; i < 25; i++)
            {
                stopwatch.Start();
                e = FindPositiveElements(arr);
                stopwatch.Stop();
                result[i] = stopwatch.ElapsedTicks;
            }
            Array.Sort(result);
            Console.WriteLine("  поиск напрямую - {0}", result[12]);

            for (int i = 0; i < 25; i++)
            {
                stopwatch.Start();
                e = FindByCondition(arr, cond1);
                stopwatch.Stop();
                result[i] = stopwatch.ElapsedTicks;
            }
            Array.Sort(result);
            Console.WriteLine("  передача условия через делегат - {0}", result[12]);

            for (int i = 0; i < 25; i++)
            {
                stopwatch.Start();
                e = FindByCondition(arr, cond2);
                stopwatch.Stop();
                result[i] = stopwatch.ElapsedTicks;
            }
            Array.Sort(result);
            Console.WriteLine("  передача условия через делегат в виде анонимного метода - {0}", result[12]);

            for (int i = 0; i < 25; i++)
            {
                stopwatch.Start();
                e = FindByCondition(arr, cond3);
                stopwatch.Stop();
                result[i] = stopwatch.ElapsedTicks;
            }
            Array.Sort(result);
            Console.WriteLine("  передача условия в виде лямбда выражения - {0}", result[12]);

            for (int i = 0; i < 25; i++)
            {
                stopwatch.Start();
                e = FindWithLinq(arr);
                stopwatch.Stop();
                result[i] = stopwatch.ElapsedTicks;
            }
            Array.Sort(result);
            Console.WriteLine("  LINQ-выражения - {0}", result[12]);

            Console.ReadKey();
        }
    }
}
