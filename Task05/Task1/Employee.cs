using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Employee : User
    {
        public string Position { get; private set; }        //должность
        public DateTime RecruitDate { get; private set; }   //дата найма
        private int Experience                              //стаж
        {
            get
            {
                if (RecruitDate >= DateTime.Now)
                    return DateTime.Now.Year - RecruitDate.Year;
                else
                    return DateTime.Now.Year - RecruitDate.Year - 1;
            }
        }

        //конструкторы
        public Employee(string surname, string name, string midname, DateTime birthdate, string pos, DateTime recdate)
        {
            Surname = surname;
            Name = name;
            Middlename = midname;
            Birthdate = birthdate;
            if (DateTime.Now.Month > birthdate.Month || DateTime.Now.Month == birthdate.Month && DateTime.Now.Day >= birthdate.Day)
                Age = DateTime.Now.Year - birthdate.Year;
            else
                Age = DateTime.Now.Year - birthdate.Year - 1;
            Position = pos;
            RecruitDate = recdate;
        }

        public Employee(string surname, string name, string midname, DateTime birthdate, string pos) :
            this(surname, name, midname, birthdate, pos, DateTime.Now) { }

        public Employee(User u, string pos, DateTime recdate) :
            this (u.Surname, u.Name, u.Middlename, u.Birthdate, pos, recdate) { }

        public Employee(User u, string pos) : this(u, pos, DateTime.Now) { }

        //переопределение методов Equals ToString GetHashCode
        public override bool Equals(object obj)
        {
            Employee E = obj as Employee;
            if (E == null)
                return false;
            else
                return Surname == E.Surname && Name == E.Name && Middlename == E.Middlename &&
                    Birthdate == E.Birthdate && Position == E.Position && RecruitDate == E.RecruitDate;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return String.Format("Ф.И.О. - {0} {1} {2}\nДата рождения - {3:00}.{4:00}.{5}, {6} лет\nДолжность - {7}, стаж - {8} лет",
                Surname, Name, Middlename, Birthdate.Day, Birthdate.Month, Birthdate.Year, Age, Position, Experience);
        }

        //переопределение операторов сравнения
        public static bool operator ==(Employee a, Employee b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Employee a, Employee b)
        {
            return !a.Equals(b);
        }
    }
}
