-- ***********************************************
-- NAME           : DUP2050_AdminAreas.table.sql
-- DESCRIPTION    : AdminAreas table
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

-- Drop existing table (OK to drop, this is first time table is being created in TDP)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AdminAreas]') AND type in (N'U'))
	DROP TABLE [dbo].[AdminAreas]
GO

---- Create table
IF NOT EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[AdminAreas]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN

	CREATE TABLE [dbo].[AdminAreas] (
		[AdministrativeAreaCode]      VARCHAR (50) NOT NULL,
		[AtcoAreaCode]                VARCHAR (50) NULL,
		[AreaName]                    VARCHAR (50) NULL,
		[AreaNameLang]                VARCHAR (50) NULL,
		[ShortName]                   VARCHAR (50) NULL,
		[ShortNameLang]               VARCHAR (50) NULL,
		[Country]                     VARCHAR (50) NULL,
		[RegionCode]                  VARCHAR (50) NULL,
		[MaximumLengthForShortNames ] VARCHAR (50) NULL,
		[National]                    VARCHAR (50) NULL,
		[ContactEmail]                VARCHAR (50) NULL,
		[ContactTelephone]            VARCHAR (50) NULL,
		[CreationDateTime]            VARCHAR (50) NULL,
		[ModificationDateTime]        VARCHAR (50) NULL,
		[RevisionNumber]              VARCHAR (50) NULL,
		[Modification]                VARCHAR (50) NULL
	);

END
GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2050, 'AdminAreas table'