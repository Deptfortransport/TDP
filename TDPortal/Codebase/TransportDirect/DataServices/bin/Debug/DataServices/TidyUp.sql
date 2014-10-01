-- Removes the ChangeNotification table and GetChangeTable stored procedure

Use PermanentPortal
Go

IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'U' AND NAME = 'ChangeNotification')
	BEGIN
		DROP TABLE ChangeNotification
	END
GO

IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'P' AND NAME = 'GetChangeTable')
	DROP PROCEDURE GetChangeTable
GO

