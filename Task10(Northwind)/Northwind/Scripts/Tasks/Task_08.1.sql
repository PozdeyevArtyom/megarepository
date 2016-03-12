SELECT
	Customers.ContactName AS 'Name',
	Count(Orders.OrderID) AS 'Count'
FROM Northwind.Customers
LEFT OUTER JOIN Northwind.Orders ON Customers.CustomerID = Orders.CustomerID
GROUP BY Customers.ContactName