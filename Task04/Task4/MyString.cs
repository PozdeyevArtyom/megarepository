using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    public class MyString : IComparable
    {
        private char[] c;                       //массив, хранящий строку
        public int Length { get; private set; } //длина строки

        //конструкторы
        public MyString(char[] s)
        {
            Length = s.Length;
            c = new char[Length];
            for (int i = 0; i < Length; i++)
                c[i] = s[i];
        }
        public MyString(string s)
        {
            c = s.ToArray();
            Length = s.Length;
        }
        public MyString(int l)
        {
            Length = l;
            c = new char[l];
        }

        //перегрузка индексатора
        public char this[int index]
        {
            get { return c[index]; }
            set { c[index] = value; }
        }

        //перегрузка методов Equals GetHashCode и ToString
        public override bool Equals(object obj)
        {
            MyString S = obj as MyString;
            if (S == null || Length != S.Length)
                return false;
            else
            {
                for (int i = 0; i < Length; i++)
                    if (c[i] != S.c[i])
                        return false;
                return true;
            }
        }

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in c)
                sb.Append((int)ch);
            return int.Parse(sb.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (char ch in c)
                sb.Append(ch);
            return sb.ToString();
        }

        //переопределение CompareTo
        public int CompareTo(object obj)
        {
            MyString S = obj as MyString;
            if (S == null)
                throw new ArgumentException("Сравниваемый объект не является экземпляром MyString.");
            else
            {
                for (int i = 0; i < Math.Min(Length, S.Length); i++)
                {
                    if (c[i] < S.c[i]) return -1;
                    if (c[i] > S.c[i]) return 1;
                }
                if (Length < S.Length) return -1;
                if (Length > S.Length) return 1;
                return 0;
            }
        }

        //перегрузка операторов сравнения
        public static bool operator == (MyString a, MyString b)
        {
            return a.Equals(b);
        }

        public static bool operator != (MyString a, MyString b)
        {
            return !a.Equals(b);
        }
        
        //перегрузка конкатенации
        public static MyString operator + (MyString a, MyString b)
        {
            char[] c = new char[a.Length + b.Length];
            for (int i = 0; i < a.Length; i++)
                c[i] = a[i];
            for (int i = 0; i < b.Length; i++)
                c[i + a.Length] = b[i];
            return new MyString(c);
        }
        
        //приведение string к MyString
        public static implicit operator MyString (string source)
        {
            return new MyString(source);
        }        
    }
}
