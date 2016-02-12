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
            int a, b, c;

            //создаём 2 экземпляра
            Triangle t1 = new Triangle();
            Triangle t2 = new Triangle(5, 4, 8);

            //вывод на экран
            Console.WriteLine("Первый треугольник: {0}", t1);
            Console.WriteLine("  Площадь - {0:0.00}", t1.GetArea());
            Console.WriteLine("  Периметр - {0:0.00}", t1.GetPerimeter());
            Console.WriteLine("Второй треугольник: {0}", t2);
            Console.WriteLine("  Площадь - {0:0.00}", t2.GetArea());
            Console.WriteLine("  Периметр - {0:0.00}", t2.GetPerimeter());
            Console.WriteLine();

            //предлагаем пользователю изменить один из треугольников
            Console.WriteLine("Введите новые длины сторон для первого треугольника:");
            Console.Write("  Введите сторону а:\n    ");
            while (int.TryParse(Console.ReadLine(), out a) == false || a < 0)
                Console.Write("    Неверный ввод. Введите целое число: ");
            Console.Write("  Введите сторону b:\n    ");
            while (int.TryParse(Console.ReadLine(), out b) == false || b < 0)
                Console.Write("    Неверный ввод. Введите целое число: ");
            Console.Write("  Введите сторону c:\n    ");
            while (int.TryParse(Console.ReadLine(), out c) == false || c < 0)
                Console.Write("    Неверный ввод. Введите неотрицательное целое число: ");
            t1.SetData(a, b, c);

            Console.WriteLine("Изменённый первый круг: {0}", t1);
            Console.WriteLine("  Площадь - {0:0.00}", t1.GetArea());
            Console.WriteLine("  Площадь - {0:0.00}", t1.GetPerimeter());

            //сравним треугольник введённый пользователем, со вторым
            if (t1 == t2)
                Console.WriteLine("Первый и второй треугольники равны.");
            else
                Console.WriteLine("Первый и второй треугольники неравны.");
            Console.ReadKey();

        }
    }
}
