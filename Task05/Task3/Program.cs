using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            int n;
            int x, y, r1, r2;
            Point p1, p2;

            //выбор фигуры
            Console.WriteLine("Выберите фигуру, которую вы хотите создать: ");
            Console.WriteLine("1 | Линия");
            Console.WriteLine("2 | Окружность");
            Console.WriteLine("3 | Прямоугольник");
            Console.WriteLine("4 | Круг");
            Console.WriteLine("5 | Кольцо");
            while (!int.TryParse(Console.ReadLine(), out n) || n > 5)
                Console.Write("Неверный ввод. Введите целое число от 1 до 5: ");
            switch(n)
            {
                case 1:
                    //ввод линии
                    Line l;
                    Console.WriteLine("\n\nВы выбрали линию. Введите 2 точки:");

                    Console.Write("  Введите координату x первой точки: ");
                    while (!int.TryParse(Console.ReadLine(), out x))
                        Console.Write("  Неверный ввод. Введите целое число: ");
                    Console.Write("  Введите координату y первой точки: ");
                    while (!int.TryParse(Console.ReadLine(), out y))
                        Console.Write("  Неверный ввод. Введите целое число: ");
                    p1 = new Point(x, y);

                    Console.Write("  Введите координату х второй точки: ");
                    while (!int.TryParse(Console.ReadLine(), out x))
                        Console.Write("  Неверный ввод. Введите целое число: ");
                    Console.Write("  Введите координату у второй точки: ");
                    while (!int.TryParse(Console.ReadLine(), out y))
                        Console.Write("  Неверный ввод. Введите целое число: ");
                    p2 = new Point(x, y);

                    l = new Line(p1, p2);
                    Console.WriteLine("\nВаша линия: {0}\n", l);
                    Console.ReadKey();
                    break;
                case 2:
                    //ввод окружности
                    Circle c;
                    Console.WriteLine("\n\nВы выбрали окружность. Введите центр и радиус:");

                    Console.Write("  Введите координату x центра: ");
                    while (!int.TryParse(Console.ReadLine(), out x))
                        Console.Write("  Неверный ввод. Введите целой число: ");
                    Console.Write("  Введите координату y центра: ");
                    while (!int.TryParse(Console.ReadLine(), out y))
                        Console.Write("  Неверный ввод. Введите целое число: ");
                    p1 = new Point(x, y);
                    Console.Write("  Введите радиус: ");
                    while (!int.TryParse(Console.ReadLine(), out r1) || r1 < 0)
                        Console.Write("  Неверный ввод. Введите натуральное число: ");

                    c = new Circle(p1, r1);
                    Console.WriteLine("Ваша окружность: {0}",c);
                    Console.ReadKey();
                    break;
                case 3:
                    //ввод прямоугольника
                    Rectangle r;
                    Console.WriteLine("\n\nВы выбрали прямоугольник. Введите 2 точки:");

                    Console.Write("  Введите координату x первой точки: ");
                    while (!int.TryParse(Console.ReadLine(), out x))
                        Console.Write("  Неверный ввод. Введите целой число: ");
                    Console.Write("  Введите координату y первой точки: ");
                    while (!int.TryParse(Console.ReadLine(), out y))
                        Console.Write("  Неверный ввод. Введите целое число: ");
                    p1 = new Point(x, y);

                    Console.Write("  Введите координату х второй точки: ");
                    while (!int.TryParse(Console.ReadLine(), out x))
                        Console.Write("  Неверный ввод. Введите целое число: ");
                    Console.Write("  Введите координату у второй точки: ");
                    while (!int.TryParse(Console.ReadLine(), out y))
                        Console.Write("  Неверный ввод. Введите целое число: ");
                    p2 = new Point(x, y);

                    r = new Rectangle(p1, p2);
                    Console.WriteLine("\nВаш прямоугольник: {0}\n", r);
                    Console.ReadKey();
                    break;
                case 4:
                    //ввод круга
                    Round round;
                    Console.WriteLine("\n\nВы выбрали круг. Введите центр и радиус:");

                    Console.Write("  Введите координату x центра: ");
                    while (!int.TryParse(Console.ReadLine(), out x))
                        Console.Write("  Неверный ввод. Введите целой число: ");
                    Console.Write("  Введите координату y центра: ");
                    while (!int.TryParse(Console.ReadLine(), out y))
                        Console.Write("  Неверный ввод. Введите целое число: ");
                    p1 = new Point(x, y);
                    Console.Write("  Введите радиус: ");
                    while (!int.TryParse(Console.ReadLine(), out r1) || r1 < 0)
                        Console.Write("  Неверный ввод. Введите натуральное число: ");

                    round = new Round(p1, r1);
                    Console.WriteLine("Ваш круг: {0}", round);
                    Console.ReadKey();
                    break;
                case 5:
                    //ввод кольца
                    Ring ring;
                    Console.WriteLine("\n\nВы выбрали кольцо. Введите центр и диаметры: ");

                    Console.Write("  Введите координату x центра: ");
                    while (!int.TryParse(Console.ReadLine(), out x))
                        Console.Write("  Неверный ввод. Введите целой число: ");
                    Console.Write("  Введите координату y центра: ");
                    while (!int.TryParse(Console.ReadLine(), out y))
                        Console.Write("  Неверный ввод. Введите целое число: ");
                    Console.Write("  Введите внутренний радиус: ");
                    while (!int.TryParse(Console.ReadLine(), out r1) || r1 < 0)
                        Console.Write("  Неверный ввод. Введите натуральное число: ");
                    Console.Write("  Введите внутренний радиус: ");
                    while (!int.TryParse(Console.ReadLine(), out r2) || r2 <= r1)
                        Console.Write("  Неверный ввод. Введите натуральное число больше {0}: ", r1);

                    ring = new Ring(x, y, r1, r2);
                    Console.WriteLine("Ваше кольцо: {0}", ring);
                    Console.ReadKey();
                    break;
            }
        }
    }
}
