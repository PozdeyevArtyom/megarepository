using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Circle : Figure
    {
        protected Point Center;
        public int Radius { get; private set; }

        //конструкторы
        protected Circle() { }
        public Circle(Point center, int radius) : this(center.X, center.Y, radius) { }
        public Circle(Circle c) : this(c.Center.X, c.Center.Y, c.Radius) { }
        public Circle(int x, int y, int radius)
        {
            Center = new Point(x, y);
            Radius = radius;
        }

        public Point getCenter()
        {
            return Center;
        }

        public void set(Point center, int radius)
        {
            Center.set(center.X,center.Y);
            Radius = radius;
        }

        public void set(int x, int y, int radius)
        {
            Center.set(x, y);
            Radius = radius;
        }

        public override void draw()
        {
            //реализация draw()
        }

        //переопределение Equals GetHashCode ToString
        public override bool Equals(object obj)
        {
            Circle c = obj as Circle;
            if ((object)c == null)
                return false;
            else
                return Radius == c.Radius;
        }

        public override int GetHashCode()
        {
            return Radius;
        }

        public override string ToString()
        {
            return String.Format("Центр - {0}, радиус - {1}", Center, Radius);
        }

        //перегрузка операторов сравнения
        static public bool operator == (Circle a, Circle b)
        {
            return a.Equals(b);
        }

        static public bool operator != (Circle a, Circle b)
        {
            return !a.Equals(b);
        }
    }
}
