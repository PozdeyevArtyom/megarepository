SELECT FirstName
FROM Northwind.Employees 
WHERE (SELECT COUNT(EmployeeID) 
		FROM Northwind.Orders 
		WHERE Employees.EmployeeID = Orders.EmployeeID
		GROUP BY EmployeeID ) > 150
