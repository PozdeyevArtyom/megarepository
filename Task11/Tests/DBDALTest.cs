using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DALFiles;
using Entities;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class DBDALTest
    {
        public Order CreateTestOrder()
        {
            Order order = new Order();
            order.CustomerID = "BOTTM";
            order.EmployeeID = 7;
            order.OrderDate = new DateTime(2016, 3, 2);
            order.RequiredDate = new DateTime(2016, 3, 22);
            order.ShippedDate = new DateTime(2016, 3, 15);
            order.ShipVia = 3;
            order.Freight = 24.12;
            order.ShipName = "Bottom-Dollar Markets";
            order.ShipAddress = "23 Tsawassen Blvd.";
            order.ShipCity = "Tsawassen";
            order.ShipRegion = "BC";
            order.ShipPostalCode = "T2F 8M4";
            order.ShipCountry = "Canada";
            order.Details.Add(new OrderDetails(new Product(40, "Boston Crab Meat"), 356.57, 42, 0.3));
            order.Status = OrderStatus.SHIPPED;

            return order;
        }

        [TestMethod]
        public void ConstructorTest()
        {
            DBDAL dbdal = new DBDAL();
            Assert.IsNotNull(dbdal.ConnectionSB);
        }

        [TestMethod]
        public void GetAllTest()
        {
            DBDAL dbdal = new DBDAL();
            Order[] allorders = dbdal.GetAll().ToArray();
            Assert.AreNotEqual(allorders.Length, 0);
        }

        [TestMethod]
        public void GetExistingOrderByIdTest()
        {
            DBDAL dbdal = new DBDAL();
            Order order = dbdal.GetById(11048);
            Order actualorder = new Order();
            actualorder.ID = 11048;
            actualorder.CustomerID = "BOTTM";
            actualorder.EmployeeID = 7;
            actualorder.OrderDate = new DateTime(1998, 4, 24);
            actualorder.RequiredDate = new DateTime(1998, 5, 22);
            actualorder.ShippedDate = new DateTime(1998, 4, 30);
            actualorder.ShipVia = 3;
            actualorder.Freight = 24.12;
            actualorder.ShipName = "Bottom-Dollar Markets";
            actualorder.ShipAddress = "23 Tsawassen Blvd.";
            actualorder.ShipCity = "Tsawassen";
            actualorder.ShipRegion = "BC";
            actualorder.ShipPostalCode = "T2F 8M4";
            actualorder.ShipCountry = "Canada";
            actualorder.Details.Add(new OrderDetails(new Product(68, "Scottish Longbreads"), 12.50, 42, 0));
            actualorder.Status = OrderStatus.SHIPPED;
            Assert.AreEqual(order, actualorder);
        }

        [TestMethod]
        public void GetNotExistingOrderByIdTest()
        {
            DBDAL dbdal = new DBDAL();
            Order order = dbdal.GetById(-345);
            Assert.IsNull(order);
        }

        [TestMethod]
        public void AddNewOrderWithIncorrectCustomerIDTest()
        {
            DBDAL dbdal = new DBDAL();
            Order order = CreateTestOrder();
            order.CustomerID = "Hello world!!!";
            try
            {
                dbdal.Add(order);
                Assert.Fail("Не было выброшено ниодного исключения.");
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "Недопустимое значение параметра!");
            }
        }

        [TestMethod]
        public void AddNewOrderWithIncorrectEmployeeIDTest()
        {
            DBDAL dbdal = new DBDAL();
            Order order = CreateTestOrder();
            order.EmployeeID = -18;
            try
            {
                dbdal.Add(order);
                Assert.Fail("Не было выброшено ниодного исключения.");
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "Недопустимое значение параметра!");
            }
        }
        [TestMethod]
        public void AddNewOrderWithIncorrectProductIDTest()
        {
            DBDAL dbdal = new DBDAL();
            Order order = CreateTestOrder();
            order.Details.Add(new OrderDetails(new Product(-97, "Scottish Longbreads"), 12.50, 42, 0));
            try
            {
                dbdal.Add(order);
                Assert.Fail("Не было выброшено ниодного исключения.");
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "Недопустимое значение параметра!");
            }
        }

        [TestMethod]
        public void AddTest()
        {
            DBDAL dbdal = new DBDAL();
            Order order = CreateTestOrder();
            int before = dbdal.GetAll().Count();
            dbdal.Add(order);
            int after = dbdal.GetAll().Count();
            Assert.AreEqual(before+1,after);
        }

        [TestMethod]
        public void SendToWorkWithIncorrectOrderIdTest()
        {
            DBDAL dbdal = new DBDAL();
            try
            {
                dbdal.SendToWork(-28, DateTime.Now);
                Assert.Fail("Не было выброшено ниодного исключения.");
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "Недопустимое значение параметра!");
            }
        }

        [TestMethod]
        public void SendToWorkTest()
        {
            DBDAL dbdal = new DBDAL();
            Assert.AreEqual(dbdal.SendToWork(10345, DateTime.Now), true);
        }

        [TestMethod]
        public void MarkAsDoneWithIncorrectOrderIdTest()
        {
            DBDAL dbdal = new DBDAL();
            try
            {
                dbdal.MarkAsDone(-57, DateTime.Now);
                Assert.Fail("Не было выброшено ниодного исключения.");
            }
            catch (ArgumentException e)
            {
                StringAssert.Contains(e.Message, "Недопустимое значение параметра!");
            }
        }

        [TestMethod]
        public void CustOrderHistIncorrectInputTest()
        {
            DBDAL dbdal = new DBDAL();
            Assert.AreEqual(dbdal.CustOrderHist("Hello, world!").Count(), 0);
        }

        [TestMethod]
        public void CustOrderHistTest()
        {
            DBDAL dbdal = new DBDAL();
            Assert.AreNotEqual(dbdal.CustOrderHist("RATTC").Count(), 0);
        }

        [TestMethod]
        public void CustOrdersDetailIncorrectInputTest()
        {
            DBDAL dbdal = new DBDAL();
            Assert.AreEqual(dbdal.CustOrdersDetail(-34).Count(), 0);
        }

        [TestMethod]
        public void CustOrdersDetailTest()
        {
            DBDAL dbdal = new DBDAL();
            Assert.AreNotEqual(dbdal.CustOrdersDetail(10345).Count(), 0);
        }
    }
}
