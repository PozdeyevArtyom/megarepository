SELECT CustomerID, Country 
FROM Northwind.Customers 
WHERE SUBSTRING(Country, 1, 1) BETWEEN 'b' AND 'g' 
ORDER BY Country