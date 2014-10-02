-- ***********************************************
-- NAME 		: DUP2025_UpdateLocationInformationFurtherDetailsURL.sql
-- DESCRIPTION 	: Script to Update Location Information Further Details URL
-- AUTHOR		: Phil Scott
-- DATE			: 20/05/2013
-- ************************************************

--************************** NOTE ***********************************
----********************************************************************

USE [PermanentPortal]
GO

---------------------------------------------------------------------
-- LOGGING PROPERTIES

UPDATE properties 
set pValue = 'http://www.nationalrail.co.uk/stations/{0}/details.html'
where pName like 'locationinformation.furtherdetailsurl'


GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2025
SET @ScriptDesc = 'Update Location Information Further Details URL'

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