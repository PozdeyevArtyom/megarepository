SELECT 
	(SELECT DISTINCT LastName FROM Northwind.Employees WHERE(Employees.EmployeeID = Orders.EmployeeID)) +
	(SELECT DISTINCT FirstName FROM Northwind.Employees WHERE(Employees.EmployeeID = Orders.EmployeeID)) AS 'Name',
	EmployeeID AS 'Seller',
	COUNT(EmployeeID) AS 'Amout' FROM Northwind.Orders
GROUP BY EmployeeID
