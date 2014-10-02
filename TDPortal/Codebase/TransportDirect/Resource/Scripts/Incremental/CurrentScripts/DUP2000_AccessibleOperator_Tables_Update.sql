-- ***********************************************
-- NAME           : DUP2000_AccessibleOperator_Tables_Update.sql
-- DESCRIPTION    : Script to update accessible operator tables, increasing varchar column sizes
-- AUTHOR         : Mitesh Modi
-- DATE           : 11 Mar 2013
-- ***********************************************

USE [TransientPortal]
GO

-- **************************************
-- [AccessibleOperators]

-- Do not drop table will exist, perform a column update
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AccessibleOperators]') AND type in (N'U'))
BEGIN 

	ALTER TABLE [dbo].[AccessibleOperators]
	ALTER COLUMN [OperatorCode] [varchar](50) NOT NULL
	
	ALTER TABLE [dbo].[AccessibleOperators]
	ALTER COLUMN [ServiceNumber] [varchar](150) NOT NULL
	
	ALTER TABLE [dbo].[AccessibleOperators]
	ALTER COLUMN [BookingUrl] [varchar](400) NULL
	
	ALTER TABLE [dbo].[AccessibleOperators]
	ALTER COLUMN [BookingNumber] [varchar](100) NULL
	
END

-- **************************************
GO


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2000
SET @ScriptDesc = 'Script to update accessible operator tables, increasing varchar column sizes'

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