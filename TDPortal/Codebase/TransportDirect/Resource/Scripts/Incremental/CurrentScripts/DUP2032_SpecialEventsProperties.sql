-- ***********************************************
-- NAME 		: DUP2032_SpecialEventsProperties.sql
-- DESCRIPTION 	: Special Events - properties
-- AUTHOR		: Rich Broddle
-- DATE			: 09 July 2013
-- ************************************************

USE [PermanentPortal]
GO

-- TIDY UP (ok to delete as this is first script to add SpecialEvents properties)
DELETE FROM [properties]
WHERE pName like 'TransportDirect.UserPortal.DataServices.SpecialEvents.%'

-- Insert properties for special events data service
INSERT INTO [PermanentPortal].[dbo].[properties] ([pName],[pValue],[AID],[GID],[PartnerId],[ThemeId])
VALUES ('TransportDirect.UserPortal.DataServices.SpecialEvents.db','TransientPortalDB','DataServices','UserPortal',0,1)

INSERT INTO [PermanentPortal].[dbo].[properties] ([pName],[pValue],[AID],[GID],[PartnerId],[ThemeId])
VALUES ('TransportDirect.UserPortal.DataServices.SpecialEvents.query',
'SELECT EventDate,MessageText FROM SpecialEvents ORDER BY EventDate','DataServices','UserPortal',0,1)

INSERT INTO [PermanentPortal].[dbo].[properties] ([pName],[pValue],[AID],[GID],[PartnerId],[ThemeId])
VALUES ('TransportDirect.UserPortal.DataServices.SpecialEvents.type','7','DataServices','UserPortal',0,1)

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2032
SET @ScriptDesc = 'special events properties'

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