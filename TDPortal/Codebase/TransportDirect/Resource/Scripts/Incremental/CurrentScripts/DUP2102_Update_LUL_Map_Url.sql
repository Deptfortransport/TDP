-- ***********************************************
-- AUTHOR      	: Rich Broddle
-- Title        : DUP2102_Update_LUL_Map_Url
-- DESCRIPTION 	: Updates London Underground map URL displayed on portal 
-- SOURCE 	: TDP Apps Support
-- ************************************************


use TransientPortal
go

UPDATE [TransientPortal].[dbo].[ExternalLinks]
	SET	[URL] = 'http://www.tfl.gov.uk/maps/track/tube'
	,[TestURL] = 'http://www.tfl.gov.uk/maps/track/tube'
 WHERE Id in ('NetworkMapLink.02', 'NetworkMapLink.LondonUnderground')
GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2102
SET @ScriptDesc = 'DUP2102_Update_LUL_Map_Url'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc)
  END
GO








