-- ***********************************************
-- NAME 		: DUP2015_ServiceDetailRequestOperatorCodes_Properties_DataNotification.sql
-- DESCRIPTION 	: Add Service Detail Request Operator Codes data notification to properties table
-- AUTHOR		: Rich Broddle
-- DATE			: 19 Mar 2013
-- ************************************************

USE [TransientPortal]
GO

-- This script adds a new change notification "table" to the 
-- "Journey" change notification group

DECLARE @changeNotifGroup varchar(100) = 'Journey'
DECLARE @changeNotifTable varchar(100) = 'ServiceDetailRequestOperatorCodes'
DECLARE @changeNotifDatabase varchar(100) = 'TransientPortalDB'

-- ************************************************
-- Change notification table
EXEC [AddChangeNotificationTable] @changeNotifTable, 0


USE [PermanentPortal]

-- ************************************************
-- Change notification properties for database and table

EXEC [AddDataNotificationDatabase] @changeNotifGroup, @changeNotifDatabase
EXEC [AddDataNotificationTable] @changeNotifGroup, @changeNotifTable


-- ************************************************
-- Change notification group list update
DECLARE @AID VARCHAR(20), 
		@GID VARCHAR(20)

-- For Default
SET @AID = '<DEFAULT>'
SET @GID = '<DEFAULT>'

EXEC [AddDataNotificationGroup] @changeNotifGroup, @AID, @GID

-- For Web
SET @AID = 'Web'
SET @GID = 'UserPortal'

EXEC [AddDataNotificationGroup] @changeNotifGroup, @AID, @GID

-- ************************************************

GO


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2015
SET @ScriptDesc = 'Add Service Detail Request Operator Codes data notification to properties table'

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
-------------------------------------------