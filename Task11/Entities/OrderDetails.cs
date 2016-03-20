using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class OrderDetails
    {
        public Product product { get; set; }
        public double UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
        
        public OrderDetails(Product prod, double unitprice, int quantity, double discount)
        {
            product = prod;
            UnitPrice = unitprice;
            Quantity = quantity;
            Discount = discount;
        }

        public override bool Equals(object obj)
        {
            OrderDetails details = obj as OrderDetails;
            if (details == null)
                return false;
            else
                return product.Equals(details.product)  && UnitPrice == details.UnitPrice
                    && Quantity == details.Quantity && Discount == details.Discount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("  Идентификатор продукта: {0}\n", product.ID);
            sb.AppendFormat("  Наименование продукта: {0}\n", product.Name);
            sb.AppendFormat("  Цена за единицу товара: {0}\n", UnitPrice);
            sb.AppendFormat("  Количество: {0}\n", Quantity);
            sb.AppendFormat("  Скидка: {0}\n", Discount);
            return sb.ToString();
        }
    }
}
