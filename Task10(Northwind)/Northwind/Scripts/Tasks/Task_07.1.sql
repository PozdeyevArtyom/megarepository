SELECT 
	Employees.LastName AS 'LastName', 
	Territories.TerritoryDescription AS 'TerritoryDescription'
FROM Northwind.Employees 
INNER JOIN Northwind.Territories ON Territories.RegionID = 2


