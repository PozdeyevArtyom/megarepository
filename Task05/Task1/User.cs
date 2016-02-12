using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class User
    {
        public string Surname { get; protected set; }         //фамилия
        public string Name { get; protected set; }            //имя
        public string Middlename { get; protected set; }      //отчество
        public DateTime Birthdate { get; protected set; }     //дата рождения
        public int Age { get; protected set; }                //возраст

        //конструктор

        protected User() { }
        public User(string surname, string name, string midname, DateTime birthdate)
        {
            Surname = surname;
            Name = name;
            Middlename = midname;
            Birthdate = birthdate;
            if (DateTime.Now.Month > birthdate.Month || DateTime.Now.Month == birthdate.Month && DateTime.Now.Day >= birthdate.Day)
                Age = DateTime.Now.Year - birthdate.Year;
            else
                Age = DateTime.Now.Year - birthdate.Year - 1;
        }

        //метод SetData позволяет менять данные о пользователе
        public void SetData(string s, string n, string m, DateTime b)
        {
            Surname = s;
            Name = n;
            Middlename = m;
            Birthdate = b;
            if (DateTime.Now.Month > b.Month || DateTime.Now.Month == b.Month && DateTime.Now.Day >= b.Day)
                Age = DateTime.Now.Year - b.Year;
            else
                Age = DateTime.Now.Year - b.Year - 1;
        }

        //переопределение методов Equals ToString GetHashCode
        public override bool Equals(object obj)
        {
            User U = obj as User;
            if (U == null)
                return false;
            else
            {
                return Surname == U.Surname && Name == U.Name &&
                    Middlename == U.Middlename && Birthdate == U.Birthdate;
            }
        }

        public override string ToString()
        {
            return String.Format("Ф.И.О. - {0} {1} {2}\nДата рождения - {3:00}.{4:00}.{5}, {6} лет.",
                Surname, Name, Middlename, Birthdate.Day, Birthdate.Month, Birthdate.Year, Age);
        }

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in Surname)
                sb.Append((int)c);
            foreach (char c in Name)
                sb.Append((int)c);
            foreach (char c in Middlename)
                sb.Append((int)c);
            sb.Append(Birthdate.Day);
            return int.Parse(sb.ToString());
        }

        //переопределение операторов сравнения
        public static bool operator ==(User a, User b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(User a, User b)
        {
            return !a.Equals(b);
        }
    }
}
