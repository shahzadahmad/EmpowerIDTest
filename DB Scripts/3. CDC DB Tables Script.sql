-- Implement CDC to capture specific changes to the data in the SQL database and write them out to another database

EXEC sys.sp_cdc_enable_db
GO

EXEC sys.sp_cdc_enable_table
@source_schema = N'dbo',
@source_name = N'Categories',
@role_name = NULL
GO

EXEC sys.sp_cdc_enable_table
@source_schema = N'dbo',
@source_name = N'Products',
@role_name = NULL
GO

EXEC sys.sp_cdc_enable_table
@source_schema = N'dbo',
@source_name = N'Orders',
@role_name = NULL
GO

EXEC sys.sp_cdc_enable_table
@source_schema = N'dbo',
@source_name = N'Orders_Products',
@role_name = NULL
GO