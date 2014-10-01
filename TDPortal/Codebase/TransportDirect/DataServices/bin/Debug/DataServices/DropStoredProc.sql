USE PermanentPortal
GO

IF EXISTS (SELECT Count(1) FROM sysobjects WHERE xtype = 'P' AND NAME = 'GetChangeTable')
	DROP PROCEDURE GetChangeTable

GO