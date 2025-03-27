SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
    CREATE OR ALTER PROCEDURE dbo.GetItemsForListing
        @minDate DATETIME = '1970.01.01', 
        @maxDate DATETIME = '2050.12.31'
    AS
    BEGIN
        -- This prevents SQL Server from returning the number of rows affected by the query.
        SET NOCOUNT ON;
    
        -- Main query to fetch items for listing
        SELECT 
            item.[Name],        -- Item name
            item.[Description], -- Item description
            item.Notes,         -- Item notes
            item.isActive,      -- Whether the item is active
            item.isDeleted,     -- Whether the item is deleted
            cat.[Name] AS CategoryName  -- Category name for the item
        FROM dbo.Items item
        LEFT JOIN dbo.Categories cat
            ON item.CategoryId = cat.Id
        WHERE 
            (@minDate IS NULL OR item.CreatedDate >= @minDate)  -- Filter by minDate if provided
            AND (@maxDate IS NULL OR item.CreatedDate <= @maxDate)  -- Filter by maxDate if provided
    END
GO