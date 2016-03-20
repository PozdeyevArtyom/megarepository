using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;

namespace DALFiles
{
    public interface IDAL
    {
        IEnumerable<Order> GetAll();
        Order GetById(int id);
        bool Add(Order order);
        bool SendToWork(int orderid, DateTime orderdate);
        bool MarkAsDone(int orderid, DateTime shippedDate);
    }
}
