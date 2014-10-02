-- ***********************************************
-- NAME 		: DUP2107_BatchNewRegUnavailableMsg.sql
-- DESCRIPTION 	        : batch close down message - no more registrations
-- AUTHOR		: Phil scott
-- DATE			: 30-07-2014
-- ************************************************

USE [Content]
GO

-- Accessible radio options
EXEC AddtblContent
	1, 1, 'langStrings', 'BatchJourneyPlanner.HowRegister.SC',
	'<p>&nbsp;</p>Unfortunately we are not accepting new registrations for the batch journey planner.','<p>&nbsp;</p>Unfortunately we are not accepting new registrations for the batch journey planner.'
	

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2107
SET @ScriptDesc = 'batch close down message - no more registration'

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