SELECT OrderID, ShippedDate  = 
CASE 
WHEN ShippedDate IS NULL THEN 'Not shipped'
END
FROM Northwind.Orders