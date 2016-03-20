using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    /// <summary>
    /// Класс Order хранит в себе информацию о заказах
    /// </summary>
    public class Order
    {
        public int ID { get; set; } = 0;
        public string CustomerID { get; set; } = "";
        public int EmployeeID { get; set; } = 0;
        public DateTime OrderDate { get; set; } = DateTime.MinValue;
        public DateTime RequiredDate { get; set; } = DateTime.MinValue;
        public DateTime ShippedDate { get; set; } = DateTime.MinValue;
        public int ShipVia { get; set; } = 1;
        public double Freight { get; set; } = 0;
        public string ShipName { get; set; } = "";
        public string ShipAddress { get; set; } = "";
        public string ShipCity { get; set; } = "";
        public string ShipRegion { get; set; } = "";
        public string ShipPostalCode { get; set; } = "";
        public string ShipCountry { get; set; } = "";
        public List<OrderDetails> Details { get; set; } = new List<OrderDetails>();
        public OrderStatus Status = OrderStatus.NEW;

        public Order() { }

        public Order(int id, string customerid, int employeeid, DateTime orderdate, DateTime requireddate, 
            DateTime shippeddate, int shipvia, double freight, string shipname, string shipaddress, string shipcity, 
            string shiparegion, string shippostalcode, string shipcountry, List<OrderDetails> details)
        {
            ID = id;
            CustomerID = customerid;
            EmployeeID = employeeid;
            OrderDate = orderdate;
            RequiredDate = requireddate;
            ShippedDate = shippeddate;
            ShipVia = shipvia;
            Freight = freight;
            ShipName = shipname;
            ShipAddress = shipaddress;
            ShipCity = shipcity;
            ShipRegion = shiparegion;
            ShipPostalCode = shippostalcode;
            ShipCountry = shipcountry;
            Details = details;
            if (orderdate.Equals(DateTime.MinValue))
                Status = OrderStatus.NEW;
            else if (ShippedDate.Equals(DateTime.MinValue))
                Status = OrderStatus.NOTSHIPPED;
            else
                Status = OrderStatus.SHIPPED;
        }

        public override int GetHashCode()
        {
            return ID;
        }
        public override bool Equals(object obj)
        {
            Order order = obj as Order;
            if (order == null)
                return false;
            else
            {
                bool b = true;
                foreach (OrderDetails d in Details)
                    b &= order.Details.Contains(d);
                return ID == order.ID
                    && CustomerID == order.CustomerID
                    && EmployeeID == order.EmployeeID
                    && OrderDate == order.OrderDate
                    && RequiredDate == order.RequiredDate
                    && ShippedDate == order.ShippedDate
                    && ShipVia == order.ShipVia
                    && Freight == order.Freight
                    && ShipName == order.ShipName
                    && ShipAddress == order.ShipAddress
                    && ShipCity == order.ShipCity
                    && ShipRegion == order.ShipRegion
                    && ShipPostalCode == order.ShipPostalCode
                    && ShipCountry == order.ShipCountry
                    && b;
            }
        }
    }
}
