using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Triangle
    {
        //стороны треугольника
        public int A { get; private set; }
        public int B { get; private set; }
        public int C { get; private set; }

        //конструкторы
        public Triangle() : this(1,1,1) { }

        public Triangle(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        //метод SetData позволяет изменять значения полей круга
        public void SetData(int a, int b, int c)
        {
            A = a;
            B = b;
            C = c;
        }

        //метод GetPerimeter возвращает периметр треугольника
        public int GetPerimeter()
        {
            return A + B + C;
        }

        //метод GetArea возвращает площадь треугольника
        public double GetArea()
        {
            double p = (double) GetPerimeter() / 2;
            return Math.Pow(p * (p - A) * (p - B) * (p - C), 0.5);
        }

        //переопределение методов Equals, GetHashCode, ToString
        public override bool Equals(object obj)
        {
            Triangle T = obj as Triangle;
            if (T == null)
                return false;
            else
            {
                int[] m1 = new int[3];
                m1[0] = A;
                m1[1] = B;
                m1[2] = C;
                int[] m2 = new int[3];
                m2[0] = T.A;
                m2[1] = T.B;
                m2[2] = T.C;
                Array.Sort(m1);
                Array.Sort(m2);
                return m1[0]==m2[0] && m1[1] == m2[1] && m1[2] == m2[2];
            }                
        }

        public override int GetHashCode()
        {
            int[] m = new int[3];
            m[0] = A;
            m[1] = B;
            m[2] = C;
            Array.Sort(m);
            return int.Parse(String.Format("{0}{1}{2}",m[0],m[1],m[2]));
        }

        public override string ToString()
        {
            return String.Format("a={0}, b={1}, c={2}",A,B,C);
        }

        //перегрузка операторов сравнения
        public static bool operator == (Triangle a, Triangle b)
        {
            return a.Equals(b);
        }

        public static bool operator != (Triangle a, Triangle b)
        {
            return !a.Equals(b);
        }
    }
}
