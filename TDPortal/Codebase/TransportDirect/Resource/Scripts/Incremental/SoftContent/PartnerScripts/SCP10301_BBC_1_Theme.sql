-- ***********************************************
-- NAME 		: SCP10301_BBC_1_Theme.sql
-- DESCRIPTION 		: Script to add a new Theme for a Partner - BBC
-- AUTHOR		: Mitesh Modi
-- DATE			: 26 Mar 2008 18:00:00
-- ************************************************

-- ************************************************
-- WARNING: PLEASE UPDATE THE @Domain value to be that for SITEST/BBP/ACP
-- ************************************************

-----------------------------------------------------------------------
-- Theme 
-----------------------------------------------------------------------

USE [Content]
GO

DECLARE @ThemeId INT,
	@ThemeName varchar(100),
	@Domain1 varchar (1000),
	@Domain2 varchar (1000),
	@Domain3 varchar (1000),
	@Domain4 varchar (1000)

SET @ThemeId = 3
SET @ThemeName = 'BBC'
-- set the most common domains
SET @Domain1 = 'BBC'
SET @Domain2 = 'bbc.transportdirect.info'
SET @Domain3 = 'bbc.sitest.transportdirect.info'
SET @Domain4 = 'bbc.test.transportdirect.info'

-- Add the theme

IF NOT EXISTS (SELECT TOP 1 * FROM tblTheme 
WHERE ThemeId = @ThemeId and Domain = @Domain1 
)
BEGIN
	INSERT INTO tblTheme VALUES (@ThemeId, @ThemeName, @Domain1)
END
ELSE
BEGIN
	UPDATE tblTheme 
	SET [Name] = @ThemeName, [Domain] = @Domain1
	WHERE ThemeId = @ThemeId and Domain = @Domain1
END


-----
IF NOT EXISTS (SELECT TOP 1 * FROM tblTheme 
WHERE ThemeId = @ThemeId and Domain = @Domain2)
BEGIN
	INSERT INTO tblTheme VALUES (@ThemeId, @ThemeName, @Domain2)
END
ELSE
BEGIN
	UPDATE tblTheme 
	SET [Name] = @ThemeName, [Domain] = @Domain2
	WHERE ThemeId = @ThemeId and Domain = @Domain2
END


-----
IF NOT EXISTS (SELECT TOP 1 * FROM tblTheme 
WHERE ThemeId = @ThemeId and Domain = @Domain3 )
BEGIN
	INSERT INTO tblTheme VALUES (@ThemeId, @ThemeName, @Domain3)
END
ELSE
BEGIN
	UPDATE tblTheme 
	SET [Name] = @ThemeName, [Domain] = @Domain3
	WHERE ThemeId = @ThemeId and Domain = @Domain3
END


-----
IF NOT EXISTS (SELECT TOP 1 * FROM tblTheme 
WHERE ThemeId = @ThemeId and Domain = @Domain4 )
BEGIN
	INSERT INTO tblTheme VALUES (@ThemeId, @ThemeName, @Domain4)
END
ELSE
BEGIN
	UPDATE tblTheme 
	SET [Name] = @ThemeName, [Domain] = @Domain4
	WHERE ThemeId = @ThemeId and Domain = @Domain4
END
GO

-----


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10301
SET @ScriptDesc = 'New theme added for BBC'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.2  $'

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
