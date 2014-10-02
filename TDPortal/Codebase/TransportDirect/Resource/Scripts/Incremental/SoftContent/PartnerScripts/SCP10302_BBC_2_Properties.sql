-- ***********************************************
-- NAME 		: SCP10302_BBC_2_Properties.sql
-- DESCRIPTION 		: Script to add specific properties for Theme BBC
-- AUTHOR		: Mitesh Modi
-- DATE			: 26 Mar 2008 18:00:00
-- ************************************************

-----------------------------------------------------------------------
-- Properties 
-----------------------------------------------------------------------

USE [PermanentPortal]
GO

DECLARE @ThemeId INT
SET @ThemeId = 3

-- Icons on homepages

IF not exists (select top 1 * from properties where pName = 'LinkToWebsiteAvailable' and ThemeId = @ThemeId)
BEGIN
	insert into properties values ('LinkToWebsiteAvailable', 'True', '<DEFAULT>', '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'True'
	where pname = 'LinkToWebsiteAvailable' and ThemeId = @ThemeId
END


IF not exists (select top 1 * from properties where pName = 'ToolbarDownloadAvailable' and ThemeId = @ThemeId)
BEGIN
	insert into properties values ('ToolbarDownloadAvailable', 'True', '<DEFAULT>', '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'True'
	where pname = 'ToolbarDownloadAvailable' and ThemeId = @ThemeId
END


IF not exists (select top 1 * from properties where pName = 'DigitalTVInfoAvailable' and ThemeId = @ThemeId)
BEGIN
	insert into properties values ('DigitalTVInfoAvailable', 'False', '<DEFAULT>', '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'False'
	where pname = 'DigitalTVInfoAvailable' and ThemeId = @ThemeId
END


IF not exists (select top 1 * from properties where pName = 'EnvironmentalBenefitsCalculator.Available' and ThemeId = @ThemeId)
BEGIN
	insert into properties values ('EnvironmentalBenefitsCalculator.Available', 'False', 'Web', 'UserPortal', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'False'
	where pname = 'EnvironmentalBenefitsCalculator.Available' and ThemeId = @ThemeId
END


-- Tabs in header
IF not exists (select top 1 * from properties where pName = 'LoginRegisterImageButtonAvailable' and ThemeId = @ThemeId)
BEGIN
	insert into properties values ('LoginRegisterImageButtonAvailable', 'True', '<DEFAULT>', '<DEFAULT>', 0, @ThemeId)
END
ELSE
BEGIN
	update properties 
	set pvalue = 'True'
	where pname = 'LoginRegisterImageButtonAvailable' and ThemeId = @ThemeId
END



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

SET @ScriptNumber = 10302
SET @ScriptDesc = 'Properties added for theme BBC'


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
