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
            char[] hello = { 'H', 'e', 'l', 'l', 'o', ',' };

            //инициализация экземпляров MyString разными способами
            MyString ex1 = new MyString(hello);
            MyString ex2 = new MyString(" world");
            MyString ex3 = "!!!";

            //вывод на экран
            Console.WriteLine("ex1 = {0}", ex1);
            Console.WriteLine("ex2 = {0}", ex2);
            Console.WriteLine("ex3 = {0}", ex3);

            //конкатенация
            MyString res = ex1 + ex2 + ex3;
            Console.WriteLine("ex1 + ex2 + ex3 = {0}", res);            

            //обращение к элементу по индексу
            int i;
            Console.Write("Введите номер символа который вы хотите изменить (отсчёт с нуля): ");
            while (int.TryParse(Console.ReadLine(), out i) == false || i < 0 && i > res.Length)
                Console.Write("Неверный ввод. Введите неотрицательное целое число меньше {0}: ", res.Length);
            Console.Write("Введите символ, на который вы хотите заменить: ");
            char c;
            while (char.TryParse(Console.ReadLine(), out c) == false)
                Console.Write("Неверный ввод. Введите один символ: ");
            res[i] = c;
            Console.WriteLine(res);
            Console.ReadKey();
        }
    }
}
