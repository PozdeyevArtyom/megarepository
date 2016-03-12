SELECT 
	e1.LastName as 'User Name',
	e2.LastName as 'Boss'
FROM Northwind.Employees as e1
INNER JOIN Northwind.Employees as e2 ON e2.EmployeeID = e1.ReportsTo