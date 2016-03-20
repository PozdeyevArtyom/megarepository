using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class CustOrderHistDataSet
    {
        public string ProductName { get; set; }
        public int Total { get; set; }

        public CustOrderHistDataSet(string productname, int total)
        {
            ProductName = productname;
            Total = total;
        }
    }
}
