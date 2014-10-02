
-- ***********************************************
-- NAME 		: DUP0619_AddTravelNewsPritableFooter Content.sql
-- DESCRIPTION 		: AddTravelNewsPritableFooter Content
-- AUTHOR		: Phil Scott
-- Date 		: 12-May-2008
-- ************************************************

-------------------------------------------------------------------------
-- Properties
-------------------------------------------------------------------------

USE [Content]
GO


-----------------------------------------------------------
-- updating PropertyName HomeDefault.lblFindCarPark
-----------------------------------------------------------



EXEC dbo.[AddtblContent]
	1,
	1,
	'langStrings',
	'PrintableTravelNews.labelDateTitle',
	'This page was viewed:',
	'Gwelwyd y dudalen hon ar:'

GO

EXEC dbo.[AddtblContent]
	1,
	1,
	'langStrings',
	'PrintableTravelNews.labelUsernameTitle',
	'Username:',
	'Enw''r defnyddiwr:'

GO


-------------------------------------------------------------------------
-- CHANGE LOG
-------------------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 619
SET @ScriptDesc = 'AddTravelNewsPritableFooter Content'

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