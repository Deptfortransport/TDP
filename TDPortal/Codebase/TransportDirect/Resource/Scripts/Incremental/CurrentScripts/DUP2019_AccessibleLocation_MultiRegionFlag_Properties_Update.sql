-- ***********************************************
-- NAME 		: DUP2019_AccessibleLocation_MultiRegionFlag_Properties_Update.sql
-- DESCRIPTION 	: Accessibility properties update for the Olympic accessible single/multi region flag
-- AUTHOR		: Mitesh Modi
-- DATE			: 28 Mar 2013
-- ************************************************

USE [PermanentPortal]
GO

-- *****************************************************************
-- IMPORTANT - ONLY RUN THIS SCRIPT IF THE INSTRUCTION IN THE BUILD TRACKER 
-- INDICATES MULTI REGION ACCESSIBLE JOURNEY PLANNING SHOULD BE PERFORMED.
-- And please update the true/false as required
-- *****************************************************************

-- Accessible journey request parameters - olympic/accessible request
-- (sets flag to allow CJP to use the single/multi region planner for accessible journeys)
IF EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'AccessibleOptions.AccessibleRequest.Olympic' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	
	-- true = single region accessible planning
	-- false = multi region accessible planning
	UPDATE properties
	SET pValue = 'false'
	WHERE pName = 'AccessibleOptions.AccessibleRequest.Olympic'
	
END

GO  

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2019
SET @ScriptDesc = 'Accessibility properties update for the Olympic accessible single/multi region flag'

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