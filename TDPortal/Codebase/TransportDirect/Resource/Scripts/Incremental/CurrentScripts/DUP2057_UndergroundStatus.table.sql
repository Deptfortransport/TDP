-- ***********************************************
-- NAME           : DUP2057_UndergroundStatus.table.sql
-- DESCRIPTION    : UndergroundStatus table
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

-- Drop existing table (OK to drop, this is first time table is being created in TDP)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UndergroundStatus]') AND type in (N'U'))
	DROP TABLE [dbo].[UndergroundStatus]
GO

---- Create table
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
		[StatusCssClass] [varchar](30) NOT NULL
	)

END
GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2057, 'UndergroundStatus table'