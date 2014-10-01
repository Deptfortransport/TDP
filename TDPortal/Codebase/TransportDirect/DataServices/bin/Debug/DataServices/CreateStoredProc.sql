Use PermanentPortal
GO

IF EXISTS (SELECT * FROM sysobjects WHERE xtype = 'P' AND NAME = 'GetChangeTable')
	DROP PROCEDURE GetChangeTable
GO
	
	CREATE PROCEDURE GetChangeTable AS
	SELECT [Table], Version FROM ChangeNotification
	ORDER BY [Table]
GO
