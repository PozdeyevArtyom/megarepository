SELECT YEAR(OrderDate) AS 'Year', COUNT(YEAR(OrderDate)) AS 'Total' 
FROM Northwind.Orders
GROUP BY YEAR(OrderDate)