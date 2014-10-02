-- ***********************************************
-- NAME           : DUP2034_Retailers.table.sql
-- DESCRIPTION    : Retailers table update
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Retailers' AND COLUMN_NAME = 'PhoneNumberDisplay')
BEGIN
	ALTER TABLE Retailers
	ADD PhoneNumberDisplay [char](100) NULL
END
GO

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Retailers' AND COLUMN_NAME = 'ResourceKey')
BEGIN
	ALTER TABLE Retailers
	ADD ResourceKey [char](100) NULL
END
GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2034, 'Retailers table update'