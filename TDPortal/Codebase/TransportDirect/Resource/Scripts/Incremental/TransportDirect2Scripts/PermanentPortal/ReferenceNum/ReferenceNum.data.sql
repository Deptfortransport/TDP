-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : ReferenceNum data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

IF NOT EXISTS (SELECT * FROM ReferenceNum)
	INSERT INTO ReferenceNum VALUES (0)


-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'ReferenceNum data'