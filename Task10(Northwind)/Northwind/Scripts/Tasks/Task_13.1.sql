USE Northwind;

GO
CREATE PROCEDURE GreatestOrders
	@year int,
	@top int
AS

	CREATE TABLE #temp(EmployeeID INT, MaxPrice MONEY);

	INSERT INTO #temp 
		SELECT
			Orders.EmployeeID AS 'EmpID',
			MAX(([Order Details].UnitPrice - [Order Details].UnitPrice * ([Order Details].Discount * 0.01)) * [Order Details].Quantity) AS 'MaxPrice'
		FROM Northwind.Orders
		LEFT OUTER JOIN Northwind.[Order Details] ON Orders.OrderID = [Order Details].OrderID
		WHERE YEAR(Orders.OrderDate) = @year
		GROUP BY Orders.EmployeeID

	SELECT TOP(@top)
		Employees.FirstName AS 'First Name',
		Employees.LastName AS 'Last Name',
		#temp.MaxPrice AS 'Max Price'
	FROM Northwind.Employees
	INNER JOIN #temp ON Employees.EmployeeID = #temp.EmployeeID
	ORDER BY 'Max Price' DESC

	DROP TABLE #temp
GO