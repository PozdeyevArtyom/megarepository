using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            string str1 = "Hello, world!!!";
            string str2 = "754";
            string str3 = "-356";
            string str4 = "4.5";
            string str5 = "3456ty331";

            Console.WriteLine("Строка {0} является положительным целым числом - {1}", str1, str1.IsPositiveInteger());
            Console.WriteLine("Строка {0} является положительным целым числом - {1}", str2, str2.IsPositiveInteger());
            Console.WriteLine("Строка {0} является положительным целым числом - {1}", str3, str3.IsPositiveInteger());
            Console.WriteLine("Строка {0} является положительным целым числом - {1}", str4, str4.IsPositiveInteger());
            Console.WriteLine("Строка {0} является положительным целым числом - {1}", str5, str5.IsPositiveInteger());
            Console.ReadKey();
        }
    }
}
