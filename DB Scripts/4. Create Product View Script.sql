CREATE VIEW [dbo].[vwProducts]
AS
SELECT        dbo.Products.product_name, dbo.Categories.category_name AS category, FORMAT(dbo.Products.price, '###.##') AS price, dbo.Products.description, CONVERT(VARCHAR, dbo.Products.date_added, 107) AS date_added, 
                         dbo.Products.product_id, dbo.Products.category_id
FROM            dbo.Products INNER JOIN
                         dbo.Categories ON dbo.Products.category_id = dbo.Categories.category_id
GO