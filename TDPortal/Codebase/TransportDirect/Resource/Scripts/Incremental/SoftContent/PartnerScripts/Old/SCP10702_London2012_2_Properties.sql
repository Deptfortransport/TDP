-- ***********************************************
-- NAME 		: SCP10702_London2012_2_Properties.sql
-- DESCRIPTION 		: Script to add specific properties for Theme London2012
-- AUTHOR		: Mitesh Modi
-- DATE			: 02 Sep 2008
-- ************************************************

-----------------------------------------------------------------------
-- Properties 
-----------------------------------------------------------------------

USE [PermanentPortal]
GO

DECLARE @ThemeId INT
SET @ThemeId = 8


-- Show the Powered by control
IF not exists (select top 1 * from properties where pName = 'PoweredByControl.ShowPoweredBy' and ThemeId = @ThemeId)
BEGIN
	insert into properties values ('PoweredByControl.ShowPoweredBy', 'True', '<DEFAULT>', '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'True'
	where pname = 'PoweredByControl.ShowPoweredBy' and ThemeId = @ThemeId
END


-- Show Menu link headings
IF not exists (select top 1 * from properties where pName = 'ExpandableMenu.MiniHomePages.ShowHeading' and ThemeId = @ThemeId)
BEGIN
	insert into properties values ('ExpandableMenu.MiniHomePages.ShowHeading', 'True', '<DEFAULT>', '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'True'
	where pname = 'ExpandableMenu.MiniHomePages.ShowHeading' and ThemeId = @ThemeId
END

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10702
SET @ScriptDesc = 'Properties added for theme London2012'


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
