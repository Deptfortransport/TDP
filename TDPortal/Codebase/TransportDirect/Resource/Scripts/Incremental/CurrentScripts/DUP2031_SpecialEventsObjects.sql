-- ***********************************************
-- NAME           : DUP2031_SpecialEventsObjects.sql
-- DESCRIPTION    : Script to create SpecialEvents table
-- AUTHOR         : Rich Broddle
-- DATE           : 09 July 2013
-- ***********************************************

USE [TransientPortal]
GO

-- **************************************
-- [SpecialEvents]

-- Drop existing table (OK to drop, this is first time table is being created in TDP)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SpecialEvents]') AND type in (N'U'))
	DROP TABLE [dbo].[SpecialEvents]
GO

---- Create table
IF NOT EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[SpecialEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE [dbo].[SpecialEvents]
	(
	EventDate datetime NOT NULL,
	MessageText varchar(3900) NOT NULL
	)  ON [PRIMARY]
END
GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2031
SET @ScriptDesc = 'Script to create special event tables'

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