-- ***********************************************
-- NAME 		: DUP2019_UpdateTrapezeRegionsParameter.sql
-- DESCRIPTION 	        : update of journeyControl.notes.TrapezeRegions property to reflect current regions
-- AUTHOR		: Phil Scott
-- DATE			: 8 Apr 2013
-- ************************************************

USE [PermanentPortal]
GO

BEGIN
	
	UPDATE properties
	SET pValue = 'S,Y,NW,W'
	where pName like 'journeyControl.notes.TrapezeRegions'
	
END

GO  

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2020
SET @ScriptDesc = 'update of journeyControl.notes.TrapezeRegions property to reflect current regions'

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