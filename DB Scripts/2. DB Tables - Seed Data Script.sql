-- Seed Data into dbo.[Categories] table.
INSERT INTO DBO.Categories 
VALUES
	('Mobile Phone'),
	('Game Console'),
	('Household Furniture'),
	('Home Appliances'),
	('Clothing');

GO

-- Seed Data into dbo.[Products] table.
INSERT INTO DBO.Products 
VALUES
	('Apple',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Mobile Phone'),
	  200,
	  null,
	  null,
	  GETDATE()),
	('Samsung',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Mobile Phone'),
	  250,
	  null,
	  null,
	  GETDATE()),
	('OPPO',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Mobile Phone'),
	  150,
	  null,
	  null,
	  GETDATE()),

	('Playstation',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Game Console'),
	  300,
	  null,
	  null,
	  GETDATE()),
	('Nintendo DS',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Game Console'),
	  500,
	  null,
	  null,
	  GETDATE()),
	('GameCube',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Game Console'),
	  600,
	  null,
	  null,
	  GETDATE()),

	('Bed',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Household Furniture'),
	  500,
	  null,
	  null,
	  GETDATE()),
	('Chairs',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Household Furniture'),
	  100,
	  null,
	  null,
	  GETDATE()),
	('Table',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Household Furniture'),
	  150,
	  null,
	  null,
	  GETDATE()),

	('Refrigerator',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Home Appliances'),
	  1000,
	  null,
	  null,
	  GETDATE()),
	('Blender',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Home Appliances'),
	  300,
	  null,
	  null,
	  GETDATE()),
	('Microwave',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Home Appliances'),
	  400,
	  null,
	  null,
	  GETDATE()),
	
	('Under Armour',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Clothing'),
	  200,
	  null,
	  null,
	  GETDATE()),
	('Wrangler',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Clothing'),
	  350,
	  null,
	  null,
	  GETDATE()),
	('Champion',
	  (SELECT category_id from dbo.Categories WHERE category_name like 'Clothing'),
	  200,
	  null,
	  null,
	  GETDATE());
	
GO		
	
-- Seed Data into dbo.[Orders] table.	
INSERT INTO DBO.Orders 
VALUES
	(GETDATE(),'Louis Bond'),
	(GETDATE(),'Chris Dave'),
	(GETDATE(),'James Herry'),
	(GETDATE(),'Robert Hawk'),
	(GETDATE(),'David Mehgin');

GO

-- Seed Data into dbo.[Orders_Products] table.	
INSERT INTO DBO.Orders_Products
VALUES
		-- Seed Data for customer 'Louis Bond'
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'Louis Bond'),
			(SELECT product_id from dbo.Products where product_name like 'Samsung')
		),
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'Louis Bond'),
			(SELECT product_id from dbo.Products where product_name like 'Champion')
		),
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'Louis Bond'),
			(SELECT product_id from dbo.Products where product_name like 'Table')
		),

		-- Seed Data for customer 'Chris Dave'
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'Chris Dave'),
			(SELECT product_id from dbo.Products where product_name like 'Microwave')
		),
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'Chris Dave'),
			(SELECT product_id from dbo.Products where product_name like 'OPPO')
		),

		-- Seed Data for customer 'James Herry'
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'James Herry'),
			(SELECT product_id from dbo.Products where product_name like 'Bed')
		),
		
		-- Seed Data for customer 'Robert Hawk'
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'Robert Hawk'),
			(SELECT product_id from dbo.Products where product_name like 'GameCube')
		),
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'Robert Hawk'),
			(SELECT product_id from dbo.Products where product_name like 'Under Armour')
		),
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'Robert Hawk'),
			(SELECT product_id from dbo.Products where product_name like 'Samsung')
		),
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'Robert Hawk'),
			(SELECT product_id from dbo.Products where product_name like 'Chairs')
		),

		-- Seed Data for customer 'David Mehgin'
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'David Mehgin'),
			(SELECT product_id from dbo.Products where product_name like 'Playstation')
		),
		(
			(SELECT order_id from dbo.Orders WHERE customer_name like 'David Mehgin'),
			(SELECT product_id from dbo.Products where product_name like 'Wrangler')
		);

GO