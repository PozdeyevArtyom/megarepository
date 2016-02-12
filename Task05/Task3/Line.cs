using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Line : Figure
    {
        private Point P1;
        private Point P2;
        
        //коэффициенты уравнения прямой
        public int A { get; private set; }
        public int B { get; private set; }
        public int C { get; private set; }

        //конструктор
        public Line(Point p1, Point p2)
        {
            P1 = p1;
            P2 = p2;
            A = p1.Y - p2.Y;
            B = p2.X - p1.X;
            C = P1.X * P2.Y - P2.X * P1.Y;
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
            A = p1.Y - p2.Y;
            B = p2.X - p1.X;
            C = P1.X * P2.Y - P2.X * P1.Y;
        }
        public override void draw()
        {
            //реализаци draw()
        }

        //переопределение Equals GetHashCode ToString
        public override bool Equals(object obj)
        {
            Line l = obj as Line;
            if ((object)l == null)
                return false;
            else
                return (double) A / l.A == (double)B / l.B && (double)B / l.B == (double)C / l.C && (double)A / l.A == (double)C / l.C;
        }

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(A);
            sb.Append(B);
            sb.Append(C);
            return int.Parse(sb.ToString());
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (A != 0) sb.AppendFormat("{0}x",A);
            if (B > 0) sb.AppendFormat("+{0}y",B);
            if (B < 0) sb.AppendFormat("{0}y", B);
            if (C > 0) sb.AppendFormat("+{0}", C);
            if (C < 0) sb.Append(C);
            sb.Append("=0");
            if (A == 0 && B > 0) return sb.ToString().Substring(1, sb.Length - 1);
            else return sb.ToString();
        }

        //перегрузка операторов сравнения
        static public bool operator ==(Line a, Line b)
        {
            return a.Equals(b);
        }
        static public bool operator !=(Line a, Line b)
        {
            return a.Equals(b);
        }
    }
}
