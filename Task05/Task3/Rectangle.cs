using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Rectangle : Figure
    {
        public Point P1;
        public Point P2;

        public Rectangle(Point p1, Point p2)
        {
            P1 = new Point(p1);
            P2 = new Point(p2);
        }

        public Point getP1()
        {
            return P1;
        }

        public Point getP2()
        {
            return P2;
        }

        public void set(Point p1, Point p2)
        {
            P1.set(p1.X, p1.Y);
            P2.set(p2.X, p2.Y);
        }

        public override void draw()
        {
            //реализация draw()
        }

        public override bool Equals(object obj)
        {
            Rectangle r = obj as Rectangle;
            if ((object)r == null)
                return false;
            else
                return P1 == r.P1 && P2 == r.P2;
        }

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(P1.GetHashCode());
            sb.Append(P2.GetHashCode());
            return int.Parse(sb.ToString());
        }

        public override string ToString()
        {
            return String.Format("{0};{1};{2};{3}", P1, new Point(P1.X, P2.Y), P2, new Point(P2.X, P1.Y));
        }
    }
}
