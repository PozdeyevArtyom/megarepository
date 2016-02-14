using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    class Program
    {
        //делгат Compare
        public delegate int Compare(string str1, string str2);

        //метод ByLength сравнивает 2 строки 
        //возвращает 1, если длина str1 больше str2
        //возвращает -1, если длина str1 меньше str2
        //возращает 0, если строки равны
        public static int ByLength(string str, string str2)
        {
            if (str.Length > str2.Length)
                return 1;
            else if (str.Length == str2.Length)
                return 0;
            else
                return -1;
        }

        //метод ByLength сравнивает 2 равные по длине строки 
        //возвращает 1, если str1 располагается раньше str2 в алфавитном порядке
        //возвращает -1, если str1 располагается позже str2 в алфавитном порядке
        //возращает 0, если строки равны
        public static int ByAlphabet(string str1, string str2)
        {
            int diff = ByLength(str1, str2);
            if (diff != 0)
                return diff;
            else if (str1.CompareTo(str2) > 0)
                return 1;
            else
                return -1;
        }

        //метод Sort сортирует массив строк str в указанном порядке sorttype
        public static void Sort(string[] str, Compare sorttype)
        {
            int i, j, min;
            string buff;
            for (i = 0; i < str.Length - 1; i++)
            {
                min = i;
                for (j = i + 1; j < str.Length; j++)
                    if (sorttype(str[min], str[j]) == 1)
                        min = j;
                if (min != i)
                {
                    buff = str[min];
                    str[min] = str[i];
                    str[i] = buff;
                }
            }
        }

        static void Main(string[] args)
        {
            //инициализируем делегаты
            Compare compareByLength = ByLength;
            Compare compareByAlph = ByAlphabet;

            //задаём массив строк
            string[] Arr = { "qweqw", "cccad", "bbbb", "qqqqq", "adsfhjkm", "abbb", "ccccc", "adsfhjkl", "aaa" };

            //выводим его на экран
            Console.WriteLine("Изначальный массив строк: ");
            foreach (string str in Arr)
                Console.WriteLine(str);

            Sort(Arr, compareByLength);
            Sort(Arr, compareByAlph);

            Console.WriteLine("\nОтсортированный массив строк: ");
            foreach (string str in Arr)
                Console.WriteLine(str);

            Console.ReadKey();
        }
    }
}
