CREATE FUNCTION isBoss (@emplid INT)
RETURNS BIT

AS 

BEGIN
	RETURN(
		SELECT CAST(CASE 
						WHEN (EXISTS(SELECT EmployeeID
									FROM Northwind.Employees
									WHERE ReportsTo = @emplid)) THEN 1 
						ELSE 0 
					END 
					AS BIT)
		)		
END;
