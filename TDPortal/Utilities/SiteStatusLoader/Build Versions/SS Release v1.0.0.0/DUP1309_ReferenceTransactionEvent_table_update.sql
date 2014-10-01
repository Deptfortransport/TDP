-- ***********************************************
-- NAME 		: DUP1309_ReferenceTransactionEvent_table_update.sql
-- DESCRIPTION 	: Script to update the ReferenceTransactionEvent table, 
--              : removing the ID column, updating the Primary Key, and reordering columns
-- AUTHOR		: Mitesh Modi
-- DATE			: 23 Mar 2009
-- ************************************************

USE [ReportStagingDB]
GO

-- ************************************************************************************************
-- THIS SCRIPT MUST BE AMENDED TO ASSIGN PERMISSIONS TO THE CORRECT USER
-- ************************************************************************************************



-- Create new table which has the correct columns in the order required, with primary key
-- Copy all data into the new table, 
-- Drop the old table
-- Rename the new table
--		There shouldnt be any errors (all data rows will be unique), but if there are not sure what we do.



-- Delete temp tables if they exist
IF EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[temp_ReferenceTransactionEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[temp_ReferenceTransactionEvent]
GO


-- Create the new table
CREATE TABLE [dbo].[temp_ReferenceTransactionEvent] (
	[Submitted] [datetime] NOT NULL ,
	[EventType] [varchar] (50) NOT NULL ,
	[ServiceLevelAgreement] [bit] NULL ,
	[SessionId] [varchar] (50) NULL ,
	[TimeLogged] [datetime] NULL ,
	[Successful] [bit] NULL 
) ON [PRIMARY]
GO


-- Create the primary key on the new table
ALTER TABLE [dbo].[temp_ReferenceTransactionEvent] WITH NOCHECK ADD 
	CONSTRAINT [PK_temp_ReferenceTransactionEvent] PRIMARY KEY  NONCLUSTERED 
	(
		[Submitted],
		[EventType]
	)  ON [PRIMARY] 
GO


-- Copy the data from the original table into the new table
IF EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReferenceTransactionEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

    INSERT INTO [dbo].[temp_ReferenceTransactionEvent]
              ( [Submitted],[EventType],[ServiceLevelAgreement],[SessionId],[TimeLogged],[Successful] )
         SELECT [Submitted],[EventType],[ServiceLevelAgreement],[SessionId],[TimeLogged],[Successful]
           FROM [dbo].[ReferenceTransactionEvent]
GO


-- Drop the original table
IF EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReferenceTransactionEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	DROP TABLE [dbo].[ReferenceTransactionEvent]
GO


-- Rename the temp table to be the original name, and the primary key
IF EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[temp_ReferenceTransactionEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)

    EXEC sp_rename 'temp_ReferenceTransactionEvent', 'ReferenceTransactionEvent'

    EXEC sp_rename 'PK_temp_ReferenceTransactionEvent','PK_ReferenceTransactionEvent','OBJECT'

GO



-- Add permissions to the appropriate user
GRANT SELECT, INSERT, UPDATE, DELETE ON [dbo].[ReferenceTransactionEvent] TO [??????] 
-- Should be ASPUSER_S, and will require all permissions

GO


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 1309
SET @ScriptDesc = 'Update ReferenceTransactionEvent table'

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