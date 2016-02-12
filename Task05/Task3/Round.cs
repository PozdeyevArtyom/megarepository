using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class Round : Circle
    {
        private int Color { get; set; }

        public Round(Point center, int radius) : base(center.X, center.Y, radius) { }
        public Round(Circle c) : base(c) { }
        public Round(int x, int y, int radius) : base(x,y,radius) { }
        public override void draw()
        {
            //переопределение draw()
        }
    }
}
