using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Task02
{
    class Program
    {
        static void Main(string[] args)
        {
            Person John = new Person("Джон");
            Person Bill = new Person("Билл");
            Person Hugo = new Person("Хьюго");
            Person Jack = new Person("Джек");

            Person.Came(John);
            Person.Came(Bill);
            Person.Came(Hugo);
            Person.Left(Bill);
            Person.Came(Jack);
            Person.Left(John);
            Person.Left(Hugo);
            Person.Left(Jack);

            Console.ReadKey();
        }
    }
}
