-- ***********************************************
-- NAME           : DUP2067_UndergroundStatus.table.sql
-- DESCRIPTION    : UndergroundStatus table
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

---- Create/Alter table
IF NOT EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[UndergroundStatus]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN

	CREATE TABLE [dbo].[UndergroundStatus]
	(
		[LineStatusId] [varchar](10) NOT NULL, 
		[LineStatusDetails] [varchar](300) NULL,
		[LineId] [varchar](10) NOT NULL, 
		[LineName] [varchar](30) NOT NULL, 
		[StatusId] [varchar](10) NOT NULL, 
		[StatusDescription] [varchar](30) NULL, 
		[StatusIsActive] bit NOT NULL, 
		[StatusCssClass] [varchar](30) NOT NULL,
		[LastUpdated] datetime NOT NULL
	)

END
ELSE
BEGIN
	ALTER TABLE [dbo].[UndergroundStatus]
	ADD [LastUpdated] datetime NOT NULL
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2067, 'UndergroundStatus table'