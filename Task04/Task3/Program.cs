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
            User person; //пользователь

            //переменные для считывания значений
            string surname, name, middlename;
            int day, month, year;

            //ввод значений
            Console.WriteLine("Введите пользователя:");
            Console.Write("  Введите фамилию: ");
            surname = Console.ReadLine();
            Console.Write("  Введите имя: ");
            name = Console.ReadLine();
            Console.Write("  Введите отчество: ");
            middlename = Console.ReadLine();

            Console.WriteLine("  Введите дату рождения:");
            Console.Write("    Год: ");
            while (int.TryParse(Console.ReadLine(), out year) == false || year > DateTime.Now.Year || year < 1900)
                Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1900 до {0}: ", DateTime.Now.Year);

            Console.Write("    Месяц: ");
            if (year == DateTime.Now.Year)
                while (int.TryParse(Console.ReadLine(), out month) == false || month > DateTime.Now.Month)
                    Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до {0}: ", DateTime.Now.Month);
            else
                while (int.TryParse(Console.ReadLine(), out month) == false || month > 12)
                    Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 12: ");

            Console.Write("    День: ");
            if (month == 2)
            {
                if (year % 4 == 0 && year % 100 != 0 || year % 400 == 0)
                    while (int.TryParse(Console.ReadLine(), out day) == false || day > 29)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 29: ");
                else
                    while (int.TryParse(Console.ReadLine(), out day) == false || day > 28)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 28: ");
            }
            else
            {
                if (month == 4 || month == 6 || month == 9 || month == 11)
                    while (int.TryParse(Console.ReadLine(), out day) == false || day > 30)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 30: ");
                else
                    while (int.TryParse(Console.ReadLine(), out day) == false || day > 31)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 31: ");
            }

            //создание пользователя
            person = new User(surname, name, middlename, new DateTime(year, month, day));

            //вывод на консоль
            Console.WriteLine(person);
            Console.ReadKey();
        }
    }
}
