-- ***********************************************
-- NAME 		: DUP2017_BatchConnectionString.sql
-- DESCRIPTION 	: Fix batch connection string
-- AUTHOR		: David Lane
-- DATE			: 28 Mar 2013
-- ************************************************


-------------------------------------------------------------------
---- ENSURE YOU CHANGE THESE PROPERTIES TO SET THE SERVER NAME TO
---- AN APPROPRIATE VALUE FOR THE TARGET ENVIRONMENT (4 PLACES)
-------------------------------------------------------------------


-- Properties
USE PermanentPortal

DECLARE @AID NVARCHAR(50)
DECLARE @GID NVARCHAR(50)

-- Service properties
SET @AID = 'BatchJourneyPlannerService'
SET @GID = 'UserPortal'

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerDBLongTimeout' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerDBLongTimeout', 'Server=.;Initial Catalog=BatchJourneyPlanner;Trusted_Connection=true;Connect Timeout=60;Packet Size=4096;', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = 'Server=.;Initial Catalog=BatchJourneyPlanner;Trusted_Connection=true;Connect Timeout=60;Packet Size=4096;'
	WHERE pName = 'BatchJourneyPlannerDBLongTimeout' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

-- website properties
SET @AID = 'Web'

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerDBLongTimeout' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerDBLongTimeout', 'Server=.;Initial Catalog=BatchJourneyPlanner;Trusted_Connection=true;Connect Timeout=60;Packet Size=4096;', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = 'Server=.;Initial Catalog=BatchJourneyPlanner;Trusted_Connection=true;Connect Timeout=60;Packet Size=4096;'
	WHERE pName = 'BatchJourneyPlannerDBLongTimeout' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2017
SET @ScriptDesc = 'CCN 0648c - Batch connection string fix'

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
-------------------------------------------
