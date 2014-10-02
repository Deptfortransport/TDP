-- ***********************************************
-- NAME           : DUP2052_Districts.table.sql
-- DESCRIPTION    : Districts table
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

-- Drop existing table (OK to drop, this is first time table is being created in TDP)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Districts]') AND type in (N'U'))
	DROP TABLE [dbo].[Districts]
GO

---- Create table
IF NOT EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[Districts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN

	CREATE TABLE [dbo].[Districts] (
		[DistrictCode]           VARCHAR (50) NOT NULL,
		[DistrictName]           VARCHAR (50) NULL,
		[DistrictLang]           VARCHAR (50) NULL,
		[AdministrativeAreaCode] VARCHAR (50) NULL,
		[CreationDateTime]       VARCHAR (50) NULL,
		[ModificationDateTime]   VARCHAR (50) NULL,
		[RevisionNumber]         VARCHAR (50) NULL,
		[Modification]           VARCHAR (50) NULL
	);

END
GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2052, 'Districts table'