using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task01
{
    class Program
    {
        static void Main(string[] args)
        {
            int N;                  //количество людей в круге
            CycleIntIncList list;   //кольцевой список содержащий номера игроков

            //ввод количества людей
            Console.Write("Введите количество людей в круге: ");
            while (!int.TryParse(Console.ReadLine(), out N) || N < 1)
                Console.WriteLine("Ошибка: неверный ввод.\nВведите натуральное число больше единицы");

            //создание списка
            list = new CycleIntIncList(N);

            //удаление каждого второго
            list.Start();

            //вывод на экран результата
            Console.Write("Номер оставшегося игрока {0}", list.Peek());
            
            Console.ReadKey();            
        }
    }
}
