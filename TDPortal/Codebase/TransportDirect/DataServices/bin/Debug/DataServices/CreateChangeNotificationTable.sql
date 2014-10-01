Use PermanentPortal
Go

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