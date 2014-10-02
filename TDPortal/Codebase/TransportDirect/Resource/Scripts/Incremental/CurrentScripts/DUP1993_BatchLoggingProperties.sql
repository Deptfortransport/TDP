-- ***********************************************
-- NAME 		: DUP1993_BatchLoggingProperties.sql
-- DESCRIPTION 	: Script to add Batch logging properties
-- AUTHOR		: David Lane
-- DATE			: 05 Feb 2013
-- ************************************************

--************************** NOTE ***********************************
-- Update publishers i.e. 'FILE1' according to SITEST/BBP/ACP
----********************************************************************

USE [PermanentPortal]
GO

---------------------------------------------------------------------
-- LOGGING PROPERTIES

DECLARE @AID NVARCHAR(30)
DECLARE @GID NVARCHAR(30)
SET @AID = 'BatchJourneyPlannerService'
SET @GID = 'UserPortal'

-- PT and car logging
IF exists (select top 1 * from properties where pName LIKE 'Logging.Event.Custom.JOURNEYREQUEST.%' and AID = @AID and GID = @GID and PartnerId = 0 and ThemeId = 1)
BEGIN
	DELETE FROM properties WHERE pName LIKE 'Logging.Event.Custom.JOURNEYREQUEST.%' and AID = @AID and GID = @GID and PartnerId = 0 and ThemeId = 1
END
BEGIN
	insert into properties values ('Logging.Event.Custom.JOURNEYREQUEST.Assembly', 'td.userportal.journeycontrol', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.JOURNEYREQUEST.Name', 'JourneyPlanRequestEvent', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.JOURNEYREQUEST.Publishers', 'FILE1 QUEUE1', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.JOURNEYREQUEST.Trace', 'On', @AID, @GID, 0, 1)
END

IF exists (select top 1 * from properties where pName LIKE 'Logging.Event.Custom.JOURNEYRESULTS.%' and AID = @AID and GID = @GID and PartnerId = 0 and ThemeId = 1)
BEGIN
	DELETE FROM properties WHERE pName LIKE 'Logging.Event.Custom.JOURNEYRESULTS.%' and AID = @AID and GID = @GID and PartnerId = 0 and ThemeId = 1
END
BEGIN
	insert into properties values ('Logging.Event.Custom.JOURNEYRESULTS.Assembly', 'td.userportal.journeycontrol', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.JOURNEYRESULTS.Name', 'JourneyPlanResultsEvent', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.JOURNEYRESULTS.Publishers', 'FILE1 QUEUE1', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.JOURNEYRESULTS.Trace', 'On', @AID, @GID, 0, 1)
END

-- cycle logging
IF exists (select top 1 * from properties where pName LIKE 'Logging.Event.Custom.CYCLEPLANNERREQUEST.%' and AID = @AID and GID = @GID and PartnerId = 0 and ThemeId = 1)
BEGIN
	DELETE FROM properties WHERE pName LIKE 'Logging.Event.Custom.CYCLEPLANNERREQUEST.%' and AID = @AID and GID = @GID and PartnerId = 0 and ThemeId = 1
END
BEGIN
	insert into properties values ('Logging.Event.Custom.CYCLEPLANNERREQUEST.Assembly', 'td.userportal.cycleplannercontrol', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.CYCLEPLANNERREQUEST.Name', 'CyclePlannerRequestEvent', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.CYCLEPLANNERREQUEST.Publishers', 'FILE1 QUEUE1', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.CYCLEPLANNERREQUEST.Trace', 'On', @AID, @GID, 0, 1)
END

IF exists (select top 1 * from properties where pName LIKE 'Logging.Event.Custom.CYCLEPLANNERRESULT.%' and AID = @AID and GID = @GID and PartnerId = 0 and ThemeId = 1)
BEGIN
	DELETE FROM properties WHERE pName LIKE 'Logging.Event.Custom.CYCLEPLANNERRESULT.%' and AID = @AID and GID = @GID and PartnerId = 0 and ThemeId = 1
END
BEGIN
	insert into properties values ('Logging.Event.Custom.CYCLEPLANNERRESULT.Assembly', 'td.userportal.cycleplannercontrol', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.CYCLEPLANNERRESULT.Name', 'CyclePlannerResultEvent', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.CYCLEPLANNERRESULT.Publishers', 'FILE1 QUEUE1', @AID, @GID, 0, 1)
	insert into properties values ('Logging.Event.Custom.CYCLEPLANNERRESULT.Trace', 'On', @AID, @GID, 0, 1)
END


-- Add the custom event to the defined list
IF not exists (select top 1 * from properties where pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID and pValue LIKE '%JOURNEYREQUEST%')
BEGIN
	UPDATE properties
	SET pValue = (SELECT pValue + ' JOURNEYREQUEST' FROM [PermanentPortal].[dbo].[properties]
				  WHERE pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID)
	WHERE pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID
END

IF not exists (select top 1 * from properties where pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID and pValue LIKE '%JOURNEYRESULTS%')
BEGIN
	UPDATE properties
	SET pValue = (SELECT pValue + ' JOURNEYRESULTS' FROM [PermanentPortal].[dbo].[properties]
				  WHERE pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID)
	WHERE pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID
END

IF not exists (select top 1 * from properties where pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID and pValue LIKE '%CYCLEPLANNERREQUEST%')
BEGIN
	UPDATE properties
	SET pValue = (SELECT pValue + ' CYCLEPLANNERREQUEST' FROM [PermanentPortal].[dbo].[properties]
				  WHERE pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID)
	WHERE pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID
END

IF not exists (select top 1 * from properties where pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID and pValue LIKE '%CYCLEPLANNERRESULT%')
BEGIN
	UPDATE properties
	SET pValue = (SELECT pValue + ' CYCLEPLANNERRESULT' FROM [PermanentPortal].[dbo].[properties]
				  WHERE pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID)
	WHERE pName = 'Logging.Event.Custom' and AID = @AID and GID = @GID
END

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 1993
SET @ScriptDesc = 'Batch logging properties'

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
