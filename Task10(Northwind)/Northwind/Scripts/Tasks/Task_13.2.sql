USE Northwind;

GO
CREATE PROCEDURE ShippedOrdersDiff
	@days INT
AS

	SELECT
		OrderID,
		OrderDate,
		ShippedDate,
		DATEDIFF(DAY,OrderDate,ShippedDate) AS 'ShippedDelay',
		@days AS 'SpecifiedDelay'
	FROM Northwind.Orders
	WHERE DATEDIFF(DAY,OrderDate,ShippedDate) IS NULL OR DATEDIFF(DAY,OrderDate,ShippedDate) > @days 
	ORDER BY 'ShippedDelay' DESC
GO