using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALFiles;
using Entities;

namespace BLL
{
    public static class Logic
    {
        public static IDAL Data = new DBDAL();

        public static IEnumerable<Order> GetAll()
        {
            return Data.GetAll();
        }

        public static Order GetByID(int id)
        {
            return Data.GetById(id);
        }

        public static int AddOrderNoDetails(Order o)
        {
            return Data.AddNoDetails(o);
        }

        public static bool AddDetails(int orderid, OrderDetails details)
        {
            return Data.AddDetails(orderid, details);
        }
    }
}
