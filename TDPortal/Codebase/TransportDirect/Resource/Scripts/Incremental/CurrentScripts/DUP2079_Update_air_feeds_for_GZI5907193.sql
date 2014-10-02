
-- ****************************************************************
-- NAME 		: DUP2079_Update_air_feeds_for_GZI5907193
-- DESCRIPTION 	: updates feed details for far265 and ijm369 to resolve GZI5907193
-- AUTHOR		: Steve Craddock
-- DATE			: 17/09/03/2013
-- ****************************************************************

use PermanentPortal
go

update dbo.IMPORT_CONFIGURATION
set 
IMPORT_CLASS = 'TransportDirect.Datagateway.Framework.CommandLineImporter',
CLASS_ARCHIVE = 'D:/Gateway/bin/td.datagateway.framework.dll',
IMPORT_UTILITY = 'D:/Gateway/bat/FAR265.bat',
PARAMETERS1 = '',
PARAMETERS2 = ''
where DATA_FEED = 'far265' 
go

update dbo.IMPORT_CONFIGURATION
set 
IMPORT_CLASS = 'TransportDirect.Datagateway.Framework.CommandLineImporter',
CLASS_ARCHIVE = 'D:/Gateway/bin/td.datagateway.framework.dll',
IMPORT_UTILITY = 'D:/Gateway/bat/IJM369.bat',
PARAMETERS1 = '',
PARAMETERS2 = ''
where DATA_FEED = 'ijm369' 
go

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2079
SET @ScriptDesc = 'updates feed details for far265 and ijm369 to resolve GZI5907193'

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