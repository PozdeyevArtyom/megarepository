using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task02
{
    /// <summary>
    /// Класс Person реализует класс сотрудника с полем именем и методами прихода в офис, ухода из офиса,
    /// а также сообщения приветствия и прощания
    /// </summary>
    public class Person
    {
        public string Name { get; private set; }        

        /// <summary>
        /// Конструктор по умолчанию создаёт экземпляр Person'a с именем name
        /// </summary>
        /// <param name="name"></param>
        public Person(string name)
        {
            Name = name;
        }

        //события прихода и ухода
        public delegate void MessageOnCame(Person p, DateTime d);
        public delegate void MessageOnLeft(Person p);
        public static event MessageOnCame OnCame;
        public static event MessageOnLeft OnLeft;

        /// <summary>
        /// Статический метод Came(Person arrivedperson) сообщает о приходе arrivedperson
        /// а также подписывает методы Greet и SayGoodbye на события OnCame и OnLeft
        /// </summary>
        /// <param name="p"></param>
        public static void Came(Person arrivedperson)
        {
            //сообщаем о приходе
            Console.WriteLine("[На работу пришёл {0}]", arrivedperson);

            //если в офисе кто-то есть вызываем их методы приветствия
            if (OnCame != null) OnCame(arrivedperson, DateTime.Now);

            //подписываемся на событие
            OnCame += arrivedperson.Greet;
            OnLeft += arrivedperson.SayGoodbye;
            Console.WriteLine();
        }

        /// <summary>
        /// Статический метод Left(Person arrivedperson) сообщает об уходе arrivedperson
        /// а также отписывает методы Greet и SayGoodbye от событий OnCame и OnLeft
        /// </summary>
        /// <param name="arrivedperson"></param>
        public static void Left(Person arrivedperson)
        {
            //сообщаем об уходе
            Console.WriteLine("[{0} ушёл домой]", arrivedperson);

            //отписываемся от событияD
            OnCame -= arrivedperson.Greet;
            OnLeft -= arrivedperson.SayGoodbye;

            //если в офисе кто-нибудь остался вызываем их методы прощания
            if (OnLeft != null) OnLeft(arrivedperson);
            Console.WriteLine();
        }

        /// <summary>
        /// Метод Greet(Person anotherPerson, DateTime time) выводит на экран приветствие anotherPerson от имени вызывающего экземпляра.
        /// Приветствие отличается в зависимости от времени.
        /// </summary>
        /// <param name="anotherPerson"></param>
        /// <param name="time"></param>
        public void Greet(Person anotherPerson, DateTime time)
        {
            if (time.Hour < 12)
                Console.WriteLine("'Доброе утро, {0}!', - сказал {1}", anotherPerson, this);
            else if (time.Hour < 17)
                Console.WriteLine("'Добрый день, {0}!', - сказал {1}", anotherPerson, this);
            else
                Console.WriteLine("'Добрый вечер, {0}!', - сказал {1}", anotherPerson, this);
        }

        /// <summary>
        /// Метод SayGoodbye(Person anotherPerson) выводит на экран прощание с anotherPerson от имени вызывающего экземпляра.
        /// </summary>
        /// <param name="anotherPerson"></param>
        public void SayGoodbye(Person anotherPerson)
        {
            Console.WriteLine("'До свидания, {0}!', - сказал {1}", anotherPerson, this);
        }

        /// <summary>
        /// Переопределение метода ToString()
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }
    }
}
