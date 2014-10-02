-- ***********************************************
-- NAME 		: DUP1991_StopInformation_AccessibleLink.sql
-- DESCRIPTION 	: Script to update Stop information accessible link
-- AUTHOR		: Mitesh Modi
-- DATE			: 01 Feb 2013
-- ************************************************

USE [PermanentPortal]
GO

DECLARE @AID_W varchar(50) = 'Web'
DECLARE @GID_UP varchar(50) = 'UserPortal'

-- Accessible find nearest stops - search configuration
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'locationinformation.accessibilityurl')
BEGIN
	INSERT INTO properties values ('locationinformation.accessibilityurl', 
	'http://www.nationalrail.co.uk/stations/sjp/{0}/stationOverview.xhtml', @AID_W, @GID_UP, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = 'http://www.nationalrail.co.uk/stations/sjp/{0}/stationOverview.xhtml'
	WHERE pName = 'locationinformation.accessibilityurl'
END

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 1991
SET @ScriptDesc = 'Stop information accessible link update'

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