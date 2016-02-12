using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Point
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        //конструкторы
        public Point() : this(0, 0) { }
        public Point(Point p) : this(p.X, p.Y) { }
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        //сеттер
        public void set(int x, int y)
        {
            X = x;
            Y = y;
        }

        //переопределение Equals GetHashCode ToString
        public override bool Equals(object obj)
        {
            Point p = obj as Point;
            if ((object)p == null)
                return false;
            else
                return X == p.X && Y == p.Y;
        }

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(X);
            sb.Append(Y);
            return int.Parse(sb.ToString());
        }

        public override string ToString()
        {
            return String.Format("({0},{1})",X,Y);
        }

        //перегрузка операторов сравнения
        static public bool operator == (Point a, Point b)
        {
            return a.Equals(b);
        }

        static public bool operator != (Point a, Point b)
        {
            return !a.Equals(b);
        }
    }
}
