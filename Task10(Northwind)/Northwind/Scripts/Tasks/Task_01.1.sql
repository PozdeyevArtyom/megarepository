DECLARE @DATE DATETIME
SET @DATE = CONVERT(DATETIME, '19980506', 101)
SELECT OrderID, ShippedDate, ShipVia 
FROM Northwind.Orders 
WHERE ShippedDate >= @DATE AND ShipVia >= 2
/*В результаты запроса не попали строки со значениями NULL потому что сравнение с NULL вернуло unknown*/

/*Data Source=(localdb)\\ProjectsV13;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False*/
select ProductID, UnitPrice, Quantity, Discount from Northwind.[Order Details] where OrderID = 10412
SELECT * FROM Northwind.Customers

SELECT * FROM Northwind.Products WHERE ProductID = 68

SELECT * FROM Northwind.Employees

SELECT * FROM Northwind.Orders /*WHERE OrderID = 10345*/ ORDER BY OrderID DESC 

SELECT * FROM Northwind.[Order Details] WHERE OrderID = 15079 ORDER BY OrderID DESC

INSERT INTO Northwind.Orders (CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress,
 ShipCity, ShipRegion, ShipPostalCode, ShipCountry) VALUES ('ALFKI', 1, CONVERT(DATETIME, '20160304', 101), 
 CONVERT(DATETIME, '20160506', 101), CONVERT(DATETIME, '20160405', 101), 3, 37.5, 'Titanik', 'Pushkin Street', 'Las-Vegas', 'DC', '123456',
 'Egypt')

 INSERT INTO Northwind.[Order Details] (ProductID, UnitPrice, Quantity, Discount) VALUES (1, 3456.43, 3, 0.5)

 DELETE FROM Northwind.Orders WHERE OrderID = 15078

 update Northwind.Orders set OrderDate = CONVERT(DATETIME, '20160304', 101) where OrderID = 12085

 select OrderID, CustomerID, EmployeeID, OrderDate, RequiredDate, ShippedDate, ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, 
 ShipPostalCode, ShipCountry from Northwind.Orders where OrderID = 10412

 insert into Northwind.[Order Details] (ProductID, UnitPrice, Quantity, Discount) values(2, 325.35, 15, 0.1)

 select OrderID from Northwind.Orders ORDER BY OrderID DESC

 EXECUTE CustOrderHist 'RATTC';

 select CustomerID from Northwind.Customers WHERE CustomerID = 'Hello world!!!'
 
 EXECUTE CustOrdersDetail 10345;

