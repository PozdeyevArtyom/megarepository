using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CustOrdersDetailDataSet
    { 
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public int Discount { get; set; }
        public double ExtendedPrice { get; set; }

        public CustOrdersDetailDataSet(string productname, double unitprice, int quantity, int discount, 
            double extendedprice)
        {
            ProductName = productname;
            UnitPrice = unitprice;
            Quantity = quantity;
            Discount = discount;
            ExtendedPrice = extendedprice;
        }
    }
}
