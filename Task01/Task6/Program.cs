using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task6
{
    class Program
    {
        static void Main(string[] args)
        {
            //флаги, отвечающие за параметры ввода
            bool bold = false;
            bool italic = false;
            bool underline = false;
            string status = "None";     //строка для вывода параметров вода на консоль
            int i;                      //переменная для изменения параметра

            Console.WriteLine("Любое число, кроме 1, 2, или 3 закончит работу программы.");
            do
            {
                //осуществление диалога с пользователем
                Console.WriteLine("Параметры надписи: {0}\nВведите:", status);
                Console.WriteLine("\t1: bold");
                Console.WriteLine("\t2: italic");
                Console.WriteLine("\t3: underline");

                //ввод числа
                while (int.TryParse(Console.ReadLine(), out i) == false)
                    Console.Write("Неверный ввод. Введите натуральное число: ");

                //изменение соответствующего флага
                switch (i)
                {
                    case 1:
                        bold = !bold;
                        break;
                    case 2:
                        italic = !italic;
                        break;
                    case 3:
                        underline = !underline;
                        break;
                }

                //проверка включённых флагов
                if (bold == false && italic == false && underline == false)
                    status = "None";
                else
                {
                    StringBuilder sb = new StringBuilder();
                    if (bold == true) sb.Append("Bold, ");
                    if (italic == true) sb.Append("Italic, ");
                    if (underline == true) sb.Append("Underline, ");
                    status = sb.ToString().Substring(0, sb.Length - 2);
                }
            } while (i < 4 && i > 0);
        }
    }
}
