SELECT 
	c1.CustomerID,
	c1.City
FROM Northwind.Customers as c1
WHERE (SELECT COUNT(1) FROM Northwind.Customers as c2 WHERE c2.City = c1.City) > 1