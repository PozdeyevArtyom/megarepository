using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            //считываем из файла текст
            StreamReader file = new StreamReader("text.txt", Encoding.Default);
            string text = file.ReadToEnd();

            //выводим текст на экран
            Console.WriteLine("Текст:");
            Console.WriteLine(text);
            Console.WriteLine();

            //делим текст на предложения
            string[] sentences = text.Split('.');

            //удаляем лишние пробелы
            for (int i = 0; i < sentences.Length; i++)
                sentences[i] = sentences[i].Trim();

            //поделим предложения на слова и запишем их в список
            //список использовать удобнее, потому что мы не знаем кол-во слов в тексте,
            //а также у списка есть метод FindAll(), который находит все вхождения элемента в список
            List<string> words = new List<string>();

            foreach (string s1 in sentences)
            {
                string[] word = s1.Split(' ');
                foreach (string s2 in word)
                    words.Add(s2.Trim().ToLower());
            }

            //ввод слов для проверки их частоты вхождения
            Console.WriteLine("Введите слова, частоту которых вы хотите проверить, через пробел");
            string[] wordsfortest = Console.ReadLine().Split(' ');
            for (int i = 0; i < wordsfortest.Length; i++)
                wordsfortest[i] = wordsfortest[i].Trim();

            //поиск вхождений и подсчёт частоты
            foreach(string t in wordsfortest)
            {
                if (t.Length > 0)   //проверка того, что пользователь случайно не поставил 2 пробела между словами
                {
                    int count = words.FindAll(
                        delegate (string str)
                        {
                            return t.ToLower().Equals(str);
                        }).Count;
                    Console.WriteLine("Слово {0} имеет частоту вхождения {1:P1}", t, (double)count / words.Count);
                }
            }

            Console.ReadKey();
        }
    }
}
