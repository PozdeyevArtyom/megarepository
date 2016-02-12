/*Написать программу, которая определяет среднюю длину слова во введённой текстовой строке.
Учесть, что символы пунктуации на длину слова влиять не должны.*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;     //строка для ввода
            string[] words; //массив слов
            double mid = 0;    //средняя длина
            int i;          //счётчик
            char[] seps = { ',', '.', '!', '?', ';' , ':' }; //массив символов пунктуации

            //считываем строку
            Console.Write("Введите строку текста: ");
            str = Console.ReadLine();

            //делим по пробелам с помощью метода Split
            words = str.Split(' ');

            //отбрасываем символы пунктуации с помощью метода Trim
            for (i = 0; i < words.Length; i++)
                words[i] = words[i].Trim(seps);

            //считаем среднюю длину
            for (i = 0; i < words.Length; i++)
                mid += words[i].Length;
            mid /= words.Length;
            Console.WriteLine("Средняя длина слова для данной строки - {0:0.0}", mid);
            Console.ReadKey();
        }
    }
}
