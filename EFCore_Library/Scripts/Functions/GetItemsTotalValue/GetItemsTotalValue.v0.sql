CREATE OR ALTER FUNCTION dbo.GetItemsTotalValue ( @IsActive BIT = true ) 
RETURNS TABLE 
AS 
RETURN ( SELECT Id, [Name], [Description], Quantity, PurchasePrice, Quantity * PurchasePrice as TotalValue 
         FROM Items 
         WHERE IsActive = @IsActive )