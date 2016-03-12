SELECT CompanyName 
FROM Northwind.Suppliers
WHERE CompanyName IN (SELECT CompanyName FROM Northwind.Products WHERE UnitsInStock = 0)