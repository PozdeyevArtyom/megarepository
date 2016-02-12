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
            int x, y, ir, or;

            //создаём 2 экземпляра
            Ring c1 = new Ring();
            Ring c2 = new Ring(5, 3, 4, 8);

            //вывод на экран
            Console.WriteLine("Первое кольцо: {0}", c1);
            Console.WriteLine("  Площадь - {0:0.00}", c1.GetArea());
            Console.WriteLine("  Длина границ - {0:0.00}", c1.GetSumLength());
            Console.WriteLine("Второй кольцо: {0}", c2);
            Console.WriteLine("  Площадь - {0:0.00}", c2.GetArea());
            Console.WriteLine("  Длина окружности - {0:0.00}", c2.GetSumLength());
            Console.WriteLine();

            //предлагаем пользователю изменить один из кругов
            Console.WriteLine("Введите новые значения для первого круга:");
            Console.Write("  Введите координату х:\n    ");
            while (int.TryParse(Console.ReadLine(), out x) == false)
                Console.Write("    Неверный ввод. Введите целое число: ");
            Console.Write("  Введите координату y:\n    ");
            while (int.TryParse(Console.ReadLine(), out y) == false)
                Console.Write("    Неверный ввод. Введите целое число: ");
            Console.Write("  Введите внутренний радиус:\n    ");
            while (int.TryParse(Console.ReadLine(), out ir) == false || ir < 0)
                Console.Write("    Неверный ввод. Введите неотрицательное целое число: ");
            Console.Write("  Введите внешний радиус:\n    ");
            while (int.TryParse(Console.ReadLine(), out or) == false || or < ir)
                Console.Write("    Неверный ввод. Введите целое число, больше {0}: ", ir);
            c1.SetRing(x, y, ir, or);

            Console.WriteLine("Изменённое первое кольцо: {0}", c1);
            Console.WriteLine("  Площадь - {0:0.00}", c1.GetArea());
            Console.WriteLine("  Длина окружности - {0:0.00}", c1.GetSumLength());

            //сравним круг введённый пользователем, со вторым
            if (c1 == c2)
                Console.WriteLine("Первое и второе кольцо равны.");
            else
                Console.WriteLine("Первое и второе кольцо неравны.");
            Console.ReadKey();

        }
    }
}
