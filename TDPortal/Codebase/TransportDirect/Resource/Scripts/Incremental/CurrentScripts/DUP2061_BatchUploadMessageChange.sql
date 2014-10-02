-- ***********************************************
-- NAME 		: DUP2061_BatchUploadMessageChange.sql
-- DESCRIPTION 	        : batch upload message - remove duplicate
-- AUTHOR		: Mitesh Modi
-- DATE			: 11 Mar 13
-- ************************************************

USE [Content]
GO

-- Accessible radio options
EXEC AddtblContent
	1, 1, 'langStrings', 'BatchJourneyPlanner.UploadSuccess',
	'<p>&nbsp;</p>Request {0} has been successfully uploaded to the Batch Journey Planner and placed {1} in the queue to be processed.',
	'<p>&nbsp;</p>Mae cais {0} wedi cael ei lwytho i fyny yn llwyddiannus i''r Cynllunydd Amldeithiau ac wedi ei osod {1} yn y ciw i''w brosesu.'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2061
SET @ScriptDesc = 'batch upload message - remove duplicate'

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