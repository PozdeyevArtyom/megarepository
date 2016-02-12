using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int a, b;   //а - длина прямоугольника, b - его ширина

            //ввод данных
            Console.WriteLine("Введите стороны прямоугольника:");
            Console.Write("  Введите длину: ");
            a = int.Parse(Console.ReadLine());
            Console.Write("  Введите ширину: ");
            b = int.Parse(Console.ReadLine());

            //подсчёт площади, если это возможно, иначе сообщаем об ошибке
            if (a > 0 && b > 0)
                Console.WriteLine("Площадь введённого прямоугольника равна {0}.", a * b);
            else
                Console.WriteLine("Ошибка. Неверные данные.");

            Console.ReadKey();
        }
    }
}
