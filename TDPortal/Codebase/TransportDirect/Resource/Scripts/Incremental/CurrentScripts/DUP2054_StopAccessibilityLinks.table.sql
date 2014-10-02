-- ***********************************************
-- NAME           : DUP2054_StopAccessibilityLinks.table.sql
-- DESCRIPTION    : StopAccessibilityLinks table
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

-- Drop existing table (OK to drop, this is first time table is being created in TDP)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[StopAccessibilityLinks]') AND type in (N'U'))
	DROP TABLE [dbo].[StopAccessibilityLinks]
GO

---- Create table
IF NOT EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[StopAccessibilityLinks]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN

	CREATE TABLE [dbo].[StopAccessibilityLinks]
	(
		[StopNaPTAN] NVARCHAR (20) NOT NULL,
		[StopName] NVARCHAR (100) NULL,
		[StopOperator] [char](10) NOT NULL,
		[LinkUrl] NVARCHAR (300) NOT NULL,
		[WEFDate] DATE NOT NULL,
		[WEUDate] DATE NOT NULL
	)

END
GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2054, 'StopAccessibilityLinks table'