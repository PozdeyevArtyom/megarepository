using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using System.Data.SqlClient;
using System.Data.SqlTypes;

namespace DALFiles
{
    public class DBDAL : IDAL
    {
        //строка подключения
        public SqlConnectionStringBuilder ConnectionSB { get; set; }

        /// <summary>
        /// Конструктор инициализирует строку подключения к базе данных
        /// </summary>
        public DBDAL()
        {
            ConnectionSB = new SqlConnectionStringBuilder
            {
                DataSource = "(localdb)\\ProjectsV13",
                InitialCatalog = "Northwind",
                IntegratedSecurity = true,
                ConnectTimeout = 30,
                Encrypt = false,
                TrustServerCertificate = true,
                ApplicationIntent = ApplicationIntent.ReadWrite,
                MultiSubnetFailover = false
            };
        }

        /// <summary>
        /// Метод GetAll() возвращает все записи из таблицы Orders
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Order> GetAll()
        {
            List<Order> result = new List<Order>();
            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                //запрос
                SqlCommand ocommand = new SqlCommand("select OrderID, CustomerID, EmployeeID, OrderDate, "
                    + "RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, "
                    + "ShipPostalCode, ShipCountry from Northwind.Orders");
                ocommand.Connection = connection;

                //открываем подключения
                connection.Open();

                using (SqlDataReader reader = ocommand.ExecuteReader())
                {
                    //считываем до конца
                    while (reader.Read())
                    {
                        Order order = new Order();
                        order.ID = reader.GetInt32(0);
                        order.CustomerID = reader.GetString(1);
                        order.EmployeeID = reader.GetInt32(2);
                        order.RequiredDate = reader.GetDateTime(4);
                        try
                        {
                            order.ShippedDate = reader.GetDateTime(5);
                            order.Status = OrderStatus.SHIPPED;
                        }
                        catch (SqlNullValueException)
                        {
                            order.ShippedDate = DateTime.MinValue;
                            order.Status = OrderStatus.NOTSHIPPED;
                        }
                        try
                        {
                            order.OrderDate = reader.GetDateTime(3);
                        }
                        catch (SqlNullValueException)
                        {
                            order.OrderDate = DateTime.MinValue;
                            order.Status = OrderStatus.NEW;
                        }
                        order.ShipVia = reader.GetInt32(6);
                        order.Freight = reader.GetSqlMoney(7).ToDouble();
                        order.ShipName = reader.GetString(8);
                        order.ShipAddress = reader.GetString(9);
                        order.ShipCity = reader.GetString(10);
                        try
                        {
                            order.ShipRegion = reader.GetString(11);
                        }
                        catch (SqlNullValueException)
                        {
                            order.ShipRegion = "";
                        }
                        try
                        {
                            order.ShipPostalCode = reader.GetString(12);
                        }
                        catch (SqlNullValueException)
                        {
                            order.ShipPostalCode = "";
                        }
                        order.ShipCountry = reader.GetString(13);
                        result.Add(order);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Метод GetById(id) возвращает запись из таблицы Orders с номером id, а также с дополнительными сведениями
        /// из таблиц Order Details и Products.
        /// Если в метод передать несуществющий идентификатор заказа, он вернёт null.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Order GetById(int id)
        {
            List<OrderDetails> details = new List<OrderDetails>();

            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                //запрос в таблицу Orders
                SqlCommand ocommand = new SqlCommand("select OrderID, CustomerID, EmployeeID, OrderDate, RequiredDate, "
                + "ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, "
                + "ShipPostalCode, ShipCountry from Northwind.Orders where OrderID = @oid");
                ocommand.Connection = connection;
                ocommand.Parameters.AddWithValue("@oid", id);

                //запрос в таблицу Order Details
                SqlCommand odcommand = new SqlCommand("select ProductID, UnitPrice, Quantity, Discount "
                    + "from Northwind.[Order Details] where OrderID = @oid");
                odcommand.Parameters.AddWithValue("@oid", id);
                odcommand.Connection = connection;


                connection.Open();

                using (SqlDataReader odreader = odcommand.ExecuteReader())
                {
                    //считываем результаты запроса в таблицу Order Details
                    if (odreader.HasRows)
                    {
                        while (odreader.Read())
                            details.Add(new OrderDetails(
                                new Product(odreader.GetInt32(0)),
                                odreader.GetSqlMoney(1).ToDouble(),
                                odreader.GetInt16(2),
                                odreader.GetFloat(3)));                 
                    }
                    else
                        return null;
                }

                for (int i = 0; i < details.Count; i++)
                {
                    //запрос в таблицу Products 
                    SqlCommand pcommand = new SqlCommand("select ProductID, ProductName "
                        + "from Northwind.Products where ProductID = @pid");
                    pcommand.Parameters.AddWithValue("@pid", details[i].product.ID);
                    pcommand.Connection = connection;

                    using (SqlDataReader preader = pcommand.ExecuteReader())
                    {
                        //получаем имя продукта из таблицы Products
                        preader.Read();
                        details[i].product.Name = preader.GetString(1);
                    }
                }   

                using (SqlDataReader reader = ocommand.ExecuteReader())
                {
                    //получаем результаты запроса в таблицу Orders
                    if(reader.Read())
                    {
                        Order order = new Order();
                        order.ID = reader.GetInt32(0);
                        order.CustomerID = reader.GetString(1);
                        order.EmployeeID = reader.GetInt32(2);
                        order.RequiredDate = reader.GetDateTime(4);
                        try
                        {
                            order.ShippedDate = reader.GetDateTime(5);
                            order.Status = OrderStatus.SHIPPED;
                        }
                        catch (SqlNullValueException)
                        {
                            order.ShippedDate = DateTime.MinValue;
                            order.Status = OrderStatus.NOTSHIPPED;
                        }
                        try
                        {
                            order.OrderDate = reader.GetDateTime(3);
                        }
                        catch (SqlNullValueException)
                        {
                            order.OrderDate = DateTime.MinValue;
                            order.Status = OrderStatus.NEW;
                        }
                        order.ShipVia = reader.GetInt32(6);
                        order.Freight = reader.GetSqlMoney(7).ToDouble();
                        order.ShipName = reader.GetString(8);
                        order.ShipAddress = reader.GetString(9);
                        order.ShipCity = reader.GetString(10);
                        try
                        {
                            order.ShipRegion = reader.GetString(11);
                        }
                        catch (SqlNullValueException)
                        {
                            order.ShipRegion = "";
                        }
                        try
                        {
                            order.ShipPostalCode = reader.GetString(12);
                        }
                        catch (SqlNullValueException)
                        {
                            order.ShipPostalCode = "";
                        }
                        order.ShipCountry = reader.GetString(13);
                        order.Details.AddRange(details);
                        return order;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Метод CheckCustomerID(id) проверяет идентификатор покупателя на верность
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckCustomerID(string id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                SqlCommand command = new SqlCommand("select CustomerID from Northwind.Customers where CustomerID = @id");
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                    return reader.Read();
            }
        }

        /// <summary>
        /// Метод CheckEmployeeID(id) проверяет идентификатор продавца на верность
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckEmployeeID(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                SqlCommand command = new SqlCommand("select EmployeeID from Northwind.Employees where EmployeeID = @id");
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                    return reader.Read();
            }
        }

        /// <summary>
        /// Метод CheckOrderID(id) проверяет идентификатор заказа на верность
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool CheckOrderID(int id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                SqlCommand command = new SqlCommand("select OrderID from Northwind.Orders where OrderID = @id");
                command.Connection = connection;
                command.Parameters.AddWithValue("@id", id);
                connection.Open();
                using (SqlDataReader reader = command.ExecuteReader())
                    return reader.Read();
            }
        }

        /// <summary>
        /// Метод CheckProductsID(details) проверяет идентификаторы продуктов на верность.
        /// На вход передаётся коллекция OrderDetails
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public bool CheckProductsID(IEnumerable<OrderDetails> details)
        {
            bool b = true;
            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                connection.Open();
                foreach (OrderDetails d in details)
                {
                    SqlCommand command = new SqlCommand("select ProductID from Northwind.Products where ProductID = @id");
                    command.Connection = connection;
                    command.Parameters.AddWithValue("@id", d.product.ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                        b &= reader.Read();
                }
            }
            return b;
        }

        /// <summary>
        /// Метод Add добавляет информацию о заказе order в базу
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        public bool Add(Order order)
        {
            if (order.CustomerID.Length > 5 || !CheckCustomerID(order.CustomerID))
                throw new ArgumentException("Недопустимое значение параметра!", "CustomerID");
            if (!CheckEmployeeID(order.EmployeeID))
                throw new ArgumentException("Недопустимое значение параметра!", "EmployeeID");
            if (!CheckProductsID(order.Details))
                throw new ArgumentException("Недопустимое значение параметра!", "ProductID");

            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                //вставка в таблицу Orders
                SqlCommand ocommand = new SqlCommand("insert into Northwind.Orders (CustomerID, EmployeeID, "
                    + "OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, "
                    + "ShipRegion, ShipPostalCode, ShipCountry) values(@cid, @eid, @od, @rd, @sd, @sv, @fr, "
                    + "@sn, @sa, @scity, @sr, @spc, @scountry)");
                ocommand.Connection = connection;

                ocommand.Parameters.AddWithValue("@cid", order.CustomerID);
                ocommand.Parameters.AddWithValue("@eid", order.EmployeeID);

                switch (order.Status)
                {
                    case OrderStatus.NEW:
                        ocommand.Parameters.AddWithValue("@od", DBNull.Value);
                        ocommand.Parameters.AddWithValue("@sd", DBNull.Value);
                        break;
                    case OrderStatus.NOTSHIPPED:
                        ocommand.Parameters.AddWithValue("@sd", DBNull.Value);
                        break;
                    case OrderStatus.SHIPPED:
                        ocommand.Parameters.AddWithValue("@od", order.OrderDate);
                        ocommand.Parameters.AddWithValue("@sd", order.ShippedDate);
                        break;
                }
                ocommand.Parameters.AddWithValue("@rd", order.RequiredDate);
                ocommand.Parameters.AddWithValue("@sv", order.ShipVia);
                ocommand.Parameters.AddWithValue("@fr", order.Freight);
                ocommand.Parameters.AddWithValue("@sn", order.ShipName);
                ocommand.Parameters.AddWithValue("@sa", order.ShipAddress);
                ocommand.Parameters.AddWithValue("@scity", order.ShipCity);

                if(order.ShipRegion != "")
                    ocommand.Parameters.AddWithValue("@sr", order.ShipRegion);
                else
                    ocommand.Parameters.AddWithValue("@sr", DBNull.Value);

                if (order.ShipPostalCode != "")
                    ocommand.Parameters.AddWithValue("@spc", order.ShipPostalCode);
                else
                    ocommand.Parameters.AddWithValue("@spc", DBNull.Value);

                ocommand.Parameters.AddWithValue("@scountry", order.ShipCountry);
                ocommand.Connection = connection;

                List<SqlCommand> detailcommands = new List<SqlCommand>();
                foreach(OrderDetails d in order.Details)
                {
                    //вставка в таблицу Order Details
                    SqlCommand odcommand = new SqlCommand("insert into Northwind.[Order Details] (OrderID, "
                        + "ProductID, UnitPrice, Quantity, Discount) values(@oid, @pid, @up, @q, @d)");
                    odcommand.Parameters.AddWithValue("@pid", d.product.ID);
                    odcommand.Parameters.AddWithValue("@up", d.UnitPrice);
                    odcommand.Parameters.AddWithValue("@q", d.Quantity);
                    odcommand.Parameters.AddWithValue("@d", d.Discount);
                    odcommand.Connection = connection;
                    detailcommands.Add(odcommand);
                }
                connection.Open();
                if (ocommand.ExecuteNonQuery() == 1)
                {
                    bool b = true;
                    //получаем идентификатор только что вставленного заказа
                    using (SqlDataReader r =
                        (new SqlCommand("select OrderID from Northwind.Orders ORDER BY OrderID DESC", 
                        connection)).ExecuteReader())
                    {
                        r.Read();
                        int oid = r.GetInt32(0);
                        foreach (SqlCommand command in detailcommands)
                            command.Parameters.AddWithValue("@oid", oid);
                    }
                    foreach (SqlCommand command in detailcommands)
                        b &= command.ExecuteNonQuery() == 1;
                    return b;
                }
                else
                    return false;
            }
        }

        /// <summary>
        /// Метод SendToWork(orderid, orderdate) устанавливает дату заказа с идентификатором orderid равной orderdate
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="orderdate"></param>
        /// <returns></returns>
        public bool SendToWork(int orderid, DateTime orderdate)
        {
            if (!CheckOrderID(orderid))
                throw new ArgumentException("Недопустимое значение параметра!", "OrderID");

            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                SqlCommand command = new SqlCommand("update Northwind.Orders set OrderDate = @od where OrderID = @oid",
                    connection);
                command.Parameters.AddWithValue("@oid", orderid);
                command.Parameters.AddWithValue("@od", orderdate);
                connection.Open();
                return command.ExecuteNonQuery() == 1;
            }
        }

        /// <summary>
        /// Метод MarkAsDone(int orderid, DateTime shippedDate) устанавливает дату отправки заказа с идентификатором 
        /// orderid равной shippedDate
        /// </summary>
        /// <param name="orderid"></param>
        /// <param name="shippedDate"></param>
        /// <returns></returns>
        public bool MarkAsDone(int orderid, DateTime shippedDate)
        {
            if (!CheckOrderID(orderid))
                throw new ArgumentException("Недопустимое значение параметра!", "OrderID");

            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                SqlCommand command = new SqlCommand("update Northwind.Orders set ShippedDate = @sd where OrderID = @oid",
                    connection);
                command.Parameters.AddWithValue("@oid", orderid);
                command.Parameters.AddWithValue("@sd", shippedDate);
                connection.Open();
                return command.ExecuteNonQuery() == 1;
            }
        }


        /// <summary>
        /// Метод CustOrderHist(CustomerID) вызывает хранимую процедуру CustOrderHist
        /// </summary>
        /// <param name="CustomerID"></param>
        /// <returns></returns>
        public IEnumerable<CustOrderHistDataSet> CustOrderHist(string CustomerID)
        {
            List<CustOrderHistDataSet> result = new List<CustOrderHistDataSet>();
            if (CustomerID.Length > 5 || !CheckCustomerID(CustomerID))
                return result;

            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                SqlCommand command = new SqlCommand("execute CustOrderHist @cid", connection);
                command.Parameters.AddWithValue("@cid", CustomerID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                    while(reader.Read())
                        result.Add(new CustOrderHistDataSet(reader.GetString(0), reader.GetInt32(1)));
            }

            return result;
        }

        /// <summary>
        /// Метод CustOrdersDetail(OrderID) вызывает хранимую процедуру CustOrdersDetail
        /// </summary>
        /// <param name="OrderID"></param>
        /// <returns></returns>
        public IEnumerable<CustOrdersDetailDataSet> CustOrdersDetail(int OrderID)
        {
            List<CustOrdersDetailDataSet> result = new List<CustOrdersDetailDataSet>();
            if(!CheckOrderID(OrderID))
                return result;

            using (SqlConnection connection = new SqlConnection(ConnectionSB.ConnectionString))
            {
                SqlCommand command = new SqlCommand("execute CustOrdersDetail @oid", connection);
                command.Parameters.AddWithValue("@oid", OrderID);
                connection.Open();

                using (SqlDataReader reader = command.ExecuteReader())
                    while (reader.Read())
                        result.Add(new CustOrdersDetailDataSet(
                            reader.GetString(0), 
                            reader.GetSqlMoney(1).ToDouble(),
                            reader.GetInt16(2),
                            reader.GetInt32(3),
                            reader.GetSqlMoney(4).ToDouble()
                            ));
            }
            return result;
        }
    }
}