CREATE OR ALTER VIEW [dbo].[vwFullItemDetails] 
AS 
SELECT 
    item.Id, item.[Name] AS ItemName, item.[Description] AS ItemDescription , 
    item.IsActive, item.IsDeleted, item.Notes, item.CurrentOrFinalPrice , 
    item.IsOnSale, item.PurchasedDate, item.PurchasePrice, item. Quantity , item.SoldDate, 
    cat.[Name] AS Category, cat.IsActive AS CategoryIsActive , cat.IsDeleted AS CategoryIsDeleted, 
    catDetail.ColorName, catDetail.ColorValue , 
    player.[Name] AS PlayerName, player.[Description] AS PlayerDescription, player.IsActive AS PlayerIsActive, 
    player.IsDeleted AS PlayerIsDeleted , 
    genre.[Name] GenreName, genre.[IsActive] AS GenreIsActive, 
    genre.IsDeleted AS  GenreIsDeleted 
FROM Items item 
    LEFT JOIN Categories cat on item.CategoryId = cat.id 
    LEFT JOIN CategoryDetails catDetail on cat.Id = catDetail.Id 
    LEFT JOIN ItemPlayers ip on item.Id = ip.ItemId 
    LEFT JOIN Players AS player on ip.PlayerId = player.Id 
    LEFT JOIN ItemGenres ig on item.id = ig.ItemId 
    LEFT JOIN Genres genre on ig.GenreId = genre.Id