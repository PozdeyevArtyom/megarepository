SELECT DISTINCT SUBSTRING(LastName, 1, 1) AS 'letter'
FROM Northwind.Employees
ORDER BY 'letter'