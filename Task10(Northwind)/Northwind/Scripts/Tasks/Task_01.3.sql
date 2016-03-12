DECLARE @DATE DATETIME
SET @DATE = CONVERT(DATETIME, '19980506', 101)
SELECT OrderID AS 'Order Number', 'Shipped Date'  =
CASE 
WHEN ShippedDate IS NULL THEN 'Not shipped'
ELSE CAST(ShippedDate AS NVARCHAR)
END 
FROM Northwind.Orders 
WHERE ShippedDate >= @DATE OR ShippedDate IS NULL