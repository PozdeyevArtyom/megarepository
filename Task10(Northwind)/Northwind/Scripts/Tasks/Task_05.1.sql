SELECT CONVERT(money, ROUND(SUM((UnitPrice - UnitPrice * (Discount * 0.01)) * Quantity), 2), 1) AS 'Total'
FROM Northwind.[Order Details]