using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public Product(int id)
        {
            ID = id;
        }
        public Product(int id, string name)
        {
            ID = id;
            Name = name;
        }

        public override int GetHashCode()
        {
            return ID;
        }

        public override bool Equals(object obj)
        {
            Product product = obj as Product;
            if (product == null)
                return false;
            else
                return ID == product.ID && Name == product.Name;
        }
    }
}
