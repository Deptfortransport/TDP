-- ***********************************************
-- NAME 		: DUP2071_Reporting_GazetteerType_Update.sql
-- DESCRIPTION 	: Update GazetteerTypes in Reporting database
-- AUTHOR		: Mitesh Modi
-- DATE			: 21 Aug 2013
-- ************************************************

USE [Reporting]
GO

DECLARE @id tinyint 

SET @id = (SELECT MAX(GTID) + 1 FROM [dbo].[GazetteerType])

IF NOT EXISTS (SELECT * FROM [dbo].[GazetteerType] WHERE GTCode = 'GazetteerCoordinate')
BEGIN
	INSERT INTO [dbo].[GazetteerType] VALUES (@id, 'GazetteerCoordinate', 'Coordinate')
END


SET @id = (SELECT MAX(GTID) + 1 FROM [dbo].[GazetteerType])

IF NOT EXISTS (SELECT * FROM [dbo].[GazetteerType] WHERE GTCode = 'GazetteerUnknown')
BEGIN
	INSERT INTO [dbo].[GazetteerType] VALUES (@id, 'GazetteerUnknown', 'Unknown')
END


SET @id = (SELECT MAX(GTID) + 1 FROM [dbo].[GazetteerType])

IF NOT EXISTS (SELECT * FROM [dbo].[GazetteerType] WHERE GTCode = 'GazetteerAutoSuggestPointOfInterest')
BEGIN
	INSERT INTO [dbo].[GazetteerType] VALUES (@id, 'GazetteerAutoSuggestPointOfInterest', 'AutoSuggest POI')
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2071, 'Update GazetteerTypes in Reporting database'