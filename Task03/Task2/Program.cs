/*Написать программу, которая удваивает в первой введённой строке все символы,
принадлежащие второй введённой строке

Пример:
    Введите первую строку: написать программу, которая  
    Введите вторую строку: описание  
    Результирующая строка: ннааппииссаать ппроограамму, коотоораая*/

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
            string str1, str2;  //переменные для строк
            int i;              //счётчик

            //считываем обе строки
            Console.Write("Введите первую строку: ");
            str1 = Console.ReadLine();
            Console.Write("Введите вторую строку: ");
            str2 = Console.ReadLine();

            //создаём переменную StringBuilder
            StringBuilder res = new StringBuilder();
            for (i = 0; i < str1.Length; i++)
            {
                res.Append(str1[i]);            //записываем в StringBuilder res каждый символ из первой строки
                if (str2.Contains(str1[i]))
                    res.Append(str1[i]);        //дубилруем его, если он содержится во второй строке
            };

            //выводим на экран
            Console.WriteLine("Результирующая строка: {0}", res);
            Console.ReadKey();
        }
    }
}
