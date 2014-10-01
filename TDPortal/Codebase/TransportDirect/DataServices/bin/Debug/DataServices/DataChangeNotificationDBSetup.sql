IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'U' AND NAME = 'ChangeNotification')
	BEGIN
		DROP TABLE ChangeNotification
		CREATE TABLE ChangeNotification ([Version] int, [Table] varchar(100))
	END
ELSE
	BEGIN
		CREATE TABLE ChangeNotification ([Version] int, [Table] varchar(100))
	END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'P' AND NAME = 'GetChangeTable')
	DROP PROCEDURE GetChangeTable
GO
	
	CREATE PROCEDURE GetChangeTable AS
	SELECT [Table], Version FROM ChangeNotification
	ORDER BY [Table]
GO
