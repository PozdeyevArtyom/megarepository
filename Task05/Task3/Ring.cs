using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Ring : Figure
    {
        private Circle Inner;
        private Circle Outer;

        //конструкторы
        public Ring(Circle inner, Circle outer)
        {
            Inner = new Circle(inner);
            Outer = new Circle(outer);
        }
        public Ring(int x, int y, int ir, int or)
        {
            Inner = new Circle(x, y, ir);
            Outer = new Circle(x, y, or);
        }
        public Ring(Point c, int ir, int or) : this(c.X, c.Y, ir, or) { }

        public Circle getInner()
        {
            return Inner;
        }

        public Circle getOuter()
        {
            return Outer;
        }

        //сеттер
        public void set(Circle inner, Circle outer)
        {
            Inner.set(inner.getCenter(), inner.Radius);
            Outer.set(outer.getCenter(), outer.Radius);
        }

        public override void draw()
        {
            //реализация draw()
        }

        //переопределение методов ToString, Equals, GetHashCode
        public override bool Equals(object obj)
        {
            Ring ring = obj as Ring;
            if ((object)ring == null)
                return false;
            else
                return Inner.Radius == ring.Inner.Radius && Outer.Radius == ring.Outer.Radius;
        }

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Inner.Radius);
            sb.Append(Outer.Radius);
            return int.Parse(sb.ToString());
        }

        public override string ToString()
        {
            return String.Format("Центр - ({0};{1}), внутренний радиус - {2}, внешний радиус - {3}.",
                Inner.getCenter().X, Inner.getCenter().Y, Inner.Radius, Outer.Radius);
        }

        //перегрузка операторов сравнения
        static public bool operator ==(Ring a, Ring b)
        {
            return a.Equals(b);
        }

        static public bool operator !=(Ring a, Ring b)
        {
            return !a.Equals(b);
        }
    }
}
