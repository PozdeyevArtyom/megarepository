using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task1
{
    public class Round
    {
        //координаты центра
        public int X { get; private set; }
        public int Y { get; private set; }
        //радиус
        public int R { get; private set; }

        //конструкторы
        public Round() : this (0,0,1) { }

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

        //метод GetCircleLength
        public double GetCircleLength()
        {
            return 2 * Math.PI * R;
        }

        //метод GetArea
        public double GetArea()
        {
            return Math.PI * R * R;
        }

        //переопределение методов ToString, Equals, GetHashCode
        public override string ToString()
        {
            return String.Format("Центр - ({0},{1}), радиус - {2}",X,Y,R);
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
        static public bool operator == (Round a, Round b)
        {
            return a.Equals(b);
        }

        static public bool operator != (Round a, Round b)
        {
            return !a.Equals(b);
        }
    }
}
