using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task4
{
    //класс Monster, наследованный от Entity
    abstract public class Monster : Entity
    {
        public int damage;          //урон монстра
                
        public void attack(Player p) { }    //метод attack вызывается при сближении игрока и монстра
    }
}
