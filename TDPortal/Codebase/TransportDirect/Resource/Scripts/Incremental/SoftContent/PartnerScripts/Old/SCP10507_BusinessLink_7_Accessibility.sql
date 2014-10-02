-- *************************************************************************************
-- NAME 		: SCP10507_BusinessLink_7_Accessibility.sql
-- DESCRIPTION  : Updates to Accessibility content
-- AUTHOR		: Amit Patel
-- DATE         :
-- *************************************************************************************

-- ************************************************
-- NOTE: The script is added just for consistancy purpose
--	   : Custom accessibility content added for cycle white planner label sites.
--     : To make the numbering of the partner scripts same, this script is added
--	   : The script is empty as partner got no custom Accessibility content
-- ************************************************


USE [Content]
GO

DECLARE @ThemeId INT
SET @ThemeId = 6

-------------------------------------------------------------
-- Accessibility Content
-------------------------------------------------------------

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10507
SET @ScriptDesc = 'Updates to Accessibility content'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.1  $'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc, VersionInfo = @VersionInfo
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary, VersionInfo)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc, @VersionInfo)
  END
GO
