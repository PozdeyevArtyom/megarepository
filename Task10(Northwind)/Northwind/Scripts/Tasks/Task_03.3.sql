SELECT CustomerID, Country 
FROM Northwind.Customers 
WHERE SUBSTRING(Country, 1, 1) >= 'b' AND SUBSTRING(Country, 1, 1) <= 'g' 
ORDER BY Country