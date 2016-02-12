using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    //абстрактный класс Entity описывает любой движущийся объект
    public abstract class Entity
    {
        protected int speed;            //скорость передвижения
        protected int CurI;             //текущая координата i
        protected int CurJ;             //текущая координата j

        //методы передвижения
        public void moveUp() { }
        public void moveDown() { }
        public void moveLeft() { }
        public void moveRight() { }
    }
}
