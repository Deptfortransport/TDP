-- ***********************************************
-- NAME 		: DUP2002_BatchJourneyPlannerMaxLines_Property.sql
-- DESCRIPTION 	: Script to update BatchJourneyPlannerMaxLines Property
-- AUTHOR		: David Lane
-- DATE			: 14 Mar 2013
-- ************************************************

--************************** NOTE ***********************************
-- THIS ONLY NEEDS TO GO TO DR TEST BUT WILL NOT HARM PRODUCTION
--*******************************************************************

USE [PermanentPortal]
GO


DECLARE @AID NVARCHAR(30)
DECLARE @GID NVARCHAR(30)
SET @AID = 'Web'
SET @GID = 'UserPortal'

-- PT and car logging
IF exists (select top 1 * from properties where pName = 'BatchJourneyPlannerMaxLines' and AID = @AID and GID = @GID and PartnerId = 0 and ThemeId = 1)
BEGIN
	UPDATE properties SET pValue = '500' WHERE pName = 'BatchJourneyPlannerMaxLines' and AID = @AID and GID = @GID and PartnerId = 0 and ThemeId = 1
END
ELSE
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerMaxLines', '500', @AID, @GID, 0, 1)
END
GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2002
SET @ScriptDesc = 'BatchJourneyPlannerMaxLines property'

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
