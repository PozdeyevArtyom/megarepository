DECLARE @DATE DATETIME
SET @DATE = CONVERT(DATETIME, '19980506', 101)
SELECT OrderID, ShippedDate, ShipVia 
FROM Northwind.Orders 
WHERE ShippedDate >= @DATE AND ShipVia >= 2
/*В результаты запроса не попали строки со значениями NULL потому что сравнение с NULL вернуло unknown*/