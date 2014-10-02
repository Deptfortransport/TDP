-- ****************************************************************
-- NAME 		: DUP2013_ServiceDetailRequestOperatorCodes_Properties.sql
-- DESCRIPTION 	: Creates switch for CCNxxx operator code translation workaround enhancment
-- AUTHOR		: Rich Broddle
-- DATE			: 19/03/2013
-- ****************************************************************

--on/off switch

USE [PermanentPortal]
GO

IF not exists ( select top 1 * from properties 
	where pName = 'StopInformation.ShowServices.FilterOperatorCodes' AND AID = 'Web')
BEGIN
	insert into properties values ('StopInformation.ShowServices.FilterOperatorCodes','True',
				'Web', 'UserPortal', 0, 1)
END
ELSE
BEGIN
	update properties set pvalue = 'True' 
		where pname = 'StopInformation.ShowServices.FilterOperatorCodes' AND AID = 'Web'
END

IF not exists ( select top 1 * from properties 
	where pName = 'StopInformation.ShowServices.FilterOperatorCodes' AND AID = 'EnhancedExposedServices')
BEGIN
	insert into properties values ('StopInformation.ShowServices.FilterOperatorCodes','True',
				'EnhancedExposedServices', 'UserPortal', 0, 1)
END
ELSE
BEGIN
	update properties set pvalue = 'True' 
		where pname = 'StopInformation.ShowServices.FilterOperatorCodes' AND AID = 'EnhancedExposedServices'
END


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2013
SET @ScriptDesc = 'Create switch for CCNxxx operator code translation workaround enhancment'

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