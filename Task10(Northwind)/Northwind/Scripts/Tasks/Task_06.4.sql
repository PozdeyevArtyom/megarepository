SELECT 
	a.ContactName as 'Person',
	'Type' = 'Customer',
	a.City
FROM Northwind.Customers as a, Northwind.Employees as b 
WHERE a.City = b.City

	UNION

SELECT 
	b.LastName as 'Person',
	'Type' = 'Seller',
	a.City
FROM Northwind.Customers as a, Northwind.Employees as b 
WHERE a.City = b.City


