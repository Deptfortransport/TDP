-- ***********************************************
-- NAME 		: SCP10701_London2012_1_Theme.sql
-- DESCRIPTION 		: Script to add a new Theme for a Partner - London2012
-- AUTHOR		: Mitesh Modi
-- DATE			: 02 Sep 2008
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

SET @ThemeId = 8
SET @ThemeName = 'London2012'
-- set the most common domains
SET @Domain1 = 'london2012'
SET @Domain2 = 'london2012.transportdirect.info'
SET @Domain3 = 'london2012.sitest.transportdirect.info'
SET @Domain4 = 'london2012.test.transportdirect.info'

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

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10701
SET @ScriptDesc = 'New theme added for London2012'


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
