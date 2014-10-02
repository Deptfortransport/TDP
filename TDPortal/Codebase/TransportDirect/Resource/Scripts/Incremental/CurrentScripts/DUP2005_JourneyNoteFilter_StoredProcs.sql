-- ***********************************************
-- NAME           : DUP2005_JourneyNoteFilter_StoredProcs.sql
-- DESCRIPTION    : Script to add journey note filter stored procedures
-- AUTHOR         : Mitesh Modi
-- DATE           : 19 Mar 2013
-- ***********************************************

-- ************************************************************************************************
-- THIS SCRIPT MUST BE AMENDED TO ASSIGN PERMISSIONS TO THE CORRECT USER.
-- SEE BOTTOM OF SCRIPT.
-- ************************************************************************************************

USE [TransientPortal]
GO

-- Create stored proc if needed
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetJourneyNoteFilter]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE GetJourneyNoteFilter 	
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- Update stored proc
ALTER PROCEDURE [dbo].[GetJourneyNoteFilter]
AS
BEGIN
    -- GetJourneyNoteFilter strored proc to return journey note filter
	SELECT 
	   [Mode]
      ,[Region]
      ,[AccessibleOnly]
      ,[FilterText]
	FROM [dbo].[JourneyNoteFilter]
END
GO
	
	
-- Grant permissions - will only work for relevant users as only they
-- will exist
GRANT  EXECUTE  ON [dbo].[GetJourneyNoteFilter]  TO [BBPTDPSIW\aspuser]
GRANT  EXECUTE  ON [dbo].[GetJourneyNoteFilter]  TO [BBPTDPW\aspuser]
GRANT  EXECUTE  ON [dbo].[GetJourneyNoteFilter]  TO [ACPTDPW\aspuser]
GRANT  EXECUTE  ON [dbo].[GetJourneyNoteFilter]  TO [BBPTDPSIS\aspuser]
GRANT  EXECUTE  ON [dbo].[GetJourneyNoteFilter]  TO [BBPTDPS\aspuser]
GRANT  EXECUTE  ON [dbo].[GetJourneyNoteFilter]  TO [ACPTDPS\aspuser]

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2005
SET @ScriptDesc = 'Script to add journey note filter stored procedures'

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