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
            Employee person; //сотрудник

            //переменные для считывания значений
            string surname, name, middlename, position;
            int bday, bmonth, byear, rday, rmonth, ryear;

            //ввод значений
            Console.WriteLine("Введите информацию о сотруднике:");

            //ФИО
            Console.Write("  Введите фамилию: ");
            surname = Console.ReadLine();
            Console.Write("  Введите имя: ");
            name = Console.ReadLine();
            Console.Write("  Введите отчество: ");
            middlename = Console.ReadLine();

            //Дата рождения
            Console.WriteLine("  Введите дату рождения:");
            Console.Write("    Год: ");
            while (int.TryParse(Console.ReadLine(), out byear) == false || byear > DateTime.Now.Year || byear < 1900)
                Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1900 до {0}: ", DateTime.Now.Year);

            Console.Write("    Месяц: ");
            if (byear == DateTime.Now.Year)
                while (int.TryParse(Console.ReadLine(), out bmonth) == false || bmonth > DateTime.Now.Month)
                    Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до {0}: ", DateTime.Now.Month);
            else
                while (int.TryParse(Console.ReadLine(), out bmonth) == false || bmonth > 12)
                    Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 12: ");

            Console.Write("    День: ");
            if (bmonth == 2)
            {
                if (byear % 4 == 0 && byear % 100 != 0 || byear % 400 == 0)
                    while (int.TryParse(Console.ReadLine(), out bday) == false || bday > 29)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 29: ");
                else
                    while (int.TryParse(Console.ReadLine(), out bday) == false || bday > 28)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 28: ");
            }
            else
            {
                if (bmonth == 4 || bmonth == 6 || bmonth == 9 || bmonth == 11)
                    while (int.TryParse(Console.ReadLine(), out bday) == false || bday > 30)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 30: ");
                else
                    while (int.TryParse(Console.ReadLine(), out bday) == false || bday > 31)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 31: ");
            }

            //должность
            Console.Write("  Введите должность: ");
            position = Console.ReadLine();

            //дата найма
            Console.WriteLine("  Введите дату найма на эту должность:");
            Console.Write("    Год: ");
            while (int.TryParse(Console.ReadLine(), out ryear) == false || ryear > DateTime.Now.Year || ryear < byear + 18)
                Console.Write("    Неверный ввод. Введите целое число в диапазоне от {0} до {1}: ", byear + 18, DateTime.Now.Year);

            Console.Write("    Месяц: ");
            if (byear == DateTime.Now.Year)
                while (int.TryParse(Console.ReadLine(), out rmonth) == false || rmonth > DateTime.Now.Month)
                    Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до {0}: ", DateTime.Now.Month);
            else
                while (int.TryParse(Console.ReadLine(), out rmonth) == false || rmonth > 12)
                    Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 12: ");

            Console.Write("    День: ");
            if (rmonth == 2)
            {
                if (ryear % 4 == 0 && ryear % 100 != 0 || ryear % 400 == 0)
                    while (int.TryParse(Console.ReadLine(), out rday) == false || rday > 29)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 29: ");
                else
                    while (int.TryParse(Console.ReadLine(), out rday) == false || rday > 28)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 28: ");
            }
            else
            {
                if (rmonth == 4 || rmonth == 6 || rmonth == 9 || rmonth == 11)
                    while (int.TryParse(Console.ReadLine(), out rday) == false || rday > 30)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 30: ");
                else
                    while (int.TryParse(Console.ReadLine(), out rday) == false || rday > 31)
                        Console.Write("    Неверный ввод. Введите целое число в диапазоне от 1 до 31: ");
            }

            //создание сотрудника
            person = new Employee(surname, name, middlename, new DateTime(byear, bmonth, bday), position, new DateTime(ryear, rmonth, rday));

            //вывод на консоль
            Console.WriteLine(person);
            Console.ReadKey();

        }
    }
}
