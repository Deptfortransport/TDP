-- ***********************************************
-- NAME           : DUP2004_JourneyNoteFilter_Tables.sql
-- DESCRIPTION    : Script to create journey note filter tables
-- AUTHOR         : Mitesh Modi
-- DATE           : 19 Mar 2013
-- ***********************************************

USE [TransientPortal]
GO

-- **************************************
-- [JourneyNoteFilter]

-- Drop existing table (OK to drop, this is first time table is being created in TDP)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[JourneyNoteFilter]') AND type in (N'U'))
	DROP TABLE [dbo].[JourneyNoteFilter]
GO

---- Create table
IF NOT EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyNoteFilter]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE [dbo].[JourneyNoteFilter](
		[Mode] [varchar](20) NOT NULL,
		[Region] [varchar](100) NOT NULL,
		[AccessibleOnly] [bit] NULL,
		[FilterText] [varchar](250) NULL
	) ON [PRIMARY]
END
GO

-- No primary key required on this table

-- **************************************
GO


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2004
SET @ScriptDesc = 'Script to create journey note filter tables'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc)
  END
GO