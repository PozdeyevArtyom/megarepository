SELECT ContactName, Country 
FROM Northwind.Customers 
WHERE Country NOT IN ('USA', 'Canada') 
ORDER BY ContactName