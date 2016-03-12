SELECT ContactName
FROM Northwind.Customers
WHERE NOT EXISTS(SELECT COUNT(CustomerID) 
		FROM Northwind.Orders 
		WHERE Customers.CustomerID = Orders.CustomerID
		GROUP BY CustomerID)