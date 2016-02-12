using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Ring
    {
        public Round Inner { get; private set; }    //внутренний круг
        public Round Outer { get; private set; }    //внешний круг

        //конструкторы
        public Ring() : this(0,0,1,2) { }

        public Ring(int x, int y, int innerrad, int outerrad)
        {
            Inner = new Round(x, y, innerrad);
            Outer = new Round(x, y, outerrad);
        }

        public Ring(Round inner, Round outer)
        {
            Inner = new Round(inner);
            Outer = new Round(outer);
        }

        public void SetRing(int x, int y, int innerrad, int outerrad)
        {
            Inner.SetData(x, y, innerrad);
            Outer.SetData(x, y, outerrad);
        }

        //метод GetSumLength() возвращает суммарную длину границ кольца
        public double GetSumLength()
        {
            return Inner.GetCircleLength() + Outer.GetCircleLength();
        }

        //метод GetArea возвращает площадь кольца
        public double GetArea()
        {
            return Outer.GetArea() - Inner.GetArea();
        }

        //переопределение методов ToString, Equals, GetHashCode
        public override bool Equals(object obj)
        {
            Ring ring = obj as Ring;
            if ((object)ring == null)
                return false;
            else
                return Inner.R == ring.Inner.R && Outer.R == ring.Outer.R;
        }

        public override int GetHashCode()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Inner.R);
            sb.Append(Outer.R);
            return int.Parse(sb.ToString());
        }

        public override string ToString()
        {
            return String.Format("Центр - ({0};{1}), внутренний радиус - {2}, внешний радиус - {3}.",
                Inner.X,Inner.Y,Inner.R,Outer.R);
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