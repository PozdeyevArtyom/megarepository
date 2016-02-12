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
            int x,y,r;

            //создаём 2 экземпляра
            Round c1 = new Round();
            Round c2 = new Round(5, 4, 8);

            //вывод на экран
            Console.WriteLine("Первый круг: {0}", c1);
            Console.WriteLine("  Площадь - {0:0.00}", c1.GetArea());
            Console.WriteLine("  Длина окружности - {0:0.00}", c1.GetCircleLength());
            Console.WriteLine("Второй круг: {0}", c2);
            Console.WriteLine("  Площадь - {0:0.00}", c2.GetArea());
            Console.WriteLine("  Длина окружности - {0:0.00}", c2.GetCircleLength());
            Console.WriteLine();

            //предлагаем пользователю изменить один из кругов
            Console.WriteLine("Введите новые значения для первого круга:");
            Console.Write("  Введите координату х:\n    ");
            while (int.TryParse(Console.ReadLine(), out x) == false)
                Console.Write("    Неверный ввод. Введите целое число: ");
            Console.Write("  Введите координату y:\n    ");
            while (int.TryParse(Console.ReadLine(), out y) == false)
                Console.Write("    Неверный ввод. Введите целое число: ");
            Console.Write("  Введите координату радиус:\n    ");
            while (int.TryParse(Console.ReadLine(), out r) == false || r < 0)
                Console.Write("    Неверный ввод. Введите неотрицательное целое число: ");
            c1.SetData(x, y, r);

            Console.WriteLine("Изменённый первый круг: {0}", c1);
            Console.WriteLine("  Площадь - {0:0.00}", c1.GetArea());
            Console.WriteLine("  Длина окружности - {0:0.00}", c1.GetCircleLength());

            //сравним круг введённый пользователем, со вторым
            if (c1==c2)
                Console.WriteLine("Первый и второй круги равны.");
            else
                Console.WriteLine("Первый и второй круги неравны.");
            Console.ReadKey();
        }
    }
}
