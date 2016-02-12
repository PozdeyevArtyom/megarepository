using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    public class Round
    {
        //координаты центра
        public int X { get; protected set; }
        public int Y { get; protected set; }
        //радиус
        public int R { get; protected set; }

        //конструкторы
        public Round() : this(0, 0, 1) { }

        public Round(Round r) : this(r.X, r.Y, r.R) { }

        public Round(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
        }


        //метод SetData позволяет изменять значения полей круга
        public void SetData(int x, int y, int r)
        {
            X = x;
            Y = y;
            R = r;
        }

        //метод GetCircleLength возвращает длину окружности
        public double GetCircleLength()
        {
            return 2 * Math.PI * R;
        }

        //метод GetArea возвращает площадь круга
        public double GetArea()
        {
            return Math.PI * R * R;
        }

        //переопределение методов ToString, Equals, GetHashCode
        public override string ToString()
        {
            return String.Format("Центр - ({0},{1}), радиус - {2}", X, Y, R);
        }

        public override bool Equals(object obj)
        {

            if ((object)(obj as Round) == null)
                return false;
            else
                return R == (obj as Round).R;
        }

        public override int GetHashCode()
        {
            return R;
        }

        //перегрузка операторов сравнения
        static public bool operator ==(Round a, Round b)
        {
            return a.Equals(b);
        }

        static public bool operator !=(Round a, Round b)
        {
            return !a.Equals(b);
        }
    }
}
