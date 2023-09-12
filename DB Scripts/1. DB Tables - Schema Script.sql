/****** Object:  Table [dbo].[Categories] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Categories](
	[category_id] [int] IDENTITY(1000,1) NOT NULL,
	[category_name] [varchar] (25) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE INDEX [UIX_Categories_CategoryName] ON [dbo].[Categories] ([category_name])
GO

SET ANSI_PADDING OFF
GO

/****** Object:  Table [dbo].[Orders] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Orders](
	[order_id] [int] IDENTITY(1000,1) NOT NULL,
	[order_date] [datetime] DEFAULT GETDATE(),
	[customer_name] [varchar] (25) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[order_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE INDEX [UIX_Orders_CustomerName] ON [dbo].[Orders] ([customer_name])
GO

CREATE NONCLUSTERED INDEX [IX_Orders] ON [dbo].[Orders] ([order_date])
GO


SET ANSI_PADDING OFF
GO


/****** Object:  Table [dbo].[Products] ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Products](
	[product_id] [int] IDENTITY(1000,1) NOT NULL,
	[product_name] [varchar] (25) NOT NULL,
	[category_id] [int] NOT NULL,
	[price] [money] NOT NULL,
	[description] [varchar] (250) NULL,
	[image_url] [varchar] (250) NULL,		
	[date_added] [datetime] DEFAULT GETDATE(),
PRIMARY KEY CLUSTERED 
(
	[product_id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE UNIQUE INDEX [UIX_Products_ProductName] ON [dbo].[Products] ([product_name])
GO

CREATE NONCLUSTERED INDEX [IX_Products] ON [dbo].[Products] ([category_id],[price],[description],[image_url],[date_added] ASC)
GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Products]  WITH CHECK ADD FOREIGN KEY([category_id])
REFERENCES [dbo].[Categories] ([category_id])
GO


/****** Object:  Table [dbo].[Orders_Products]  ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Orders_Products](
	[ID] [int] IDENTITY(1000,1) NOT NULL,
	[order_id] [int] NOT NULL,
	[product_id] [int] NOT NULL	
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_OrdersProducts] ON [dbo].[Orders_Products] ([order_id],[product_id] ASC)
GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Orders_Products]  WITH CHECK ADD FOREIGN KEY([order_id])
REFERENCES [dbo].[Orders] ([order_id])
GO

ALTER TABLE [dbo].[Orders_Products]  WITH CHECK ADD FOREIGN KEY([product_id])
REFERENCES [dbo].[Products] ([product_id])
GO

