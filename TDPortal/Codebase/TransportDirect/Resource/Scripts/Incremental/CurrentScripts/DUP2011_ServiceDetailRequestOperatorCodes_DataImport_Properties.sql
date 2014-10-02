-- ***********************************************
-- NAME 		: DUP2008_ServiceDetailRequestOperatorCodes_DataImport_Properties.sql
-- DESCRIPTION 	: Script to update data import properties for operator code translation data
-- AUTHOR		: Rich Broddle
-- DATE			: 19 Mar 2013
-- ************************************************


-- ****************** IMPORTANT **************************
-- Please change the @gatewayPath value path to be as required for 
-- the environment
-- *******************************************************

USE [PermanentPortal]
GO

DECLARE @gatewayPath varchar(30) = 'C:\Gateway\xml'

-- Clear all existing properties
DELETE FROM [dbo].[properties]
WHERE  pName LIKE 'datagateway.sqlimport.ServiceDetailRequestOperatorCodesData%'

-- AID and GID
DECLARE @AID varchar(50) = ''
DECLARE @GID varchar(50) = 'DataGateway'

-- Service Detail Request Operator Codes Data Operator Data
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.ServiceDetailRequestOperatorCodesData.database', @AID, @GID, 0, 1, N'TransientPortalDB')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.ServiceDetailRequestOperatorCodesData.feedname', @AID, @GID, 0, 1, N'ert199')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.ServiceDetailRequestOperatorCodesData.Name', @AID, @GID, 0, 1, N'ServiceDetailRequestOperatorCodesData')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.ServiceDetailRequestOperatorCodesData.schemea', @AID, @GID, 0, 1, @gatewayPath + N'\ServiceDetailRequestOperatorCodes.xsd')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.ServiceDetailRequestOperatorCodesData.sqlcommandtimeout', @AID, @GID, 0, 1, N'3000')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.ServiceDetailRequestOperatorCodesData.storedprocedure', @AID, @GID, 0, 1, N'ImportServiceDetailRequestOperatorCodesData')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.ServiceDetailRequestOperatorCodesData.xmlnamespace', @AID, @GID, 0, 1, N'http://www.transportdirect.info/ServiceDetailRequestOperatorCodes')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.ServiceDetailRequestOperatorCodesData.xmlnamespacexsi', @AID, @GID, 0, 1, N'http://www.w3.org/2001/XMLSchema-instance')

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2011
SET @ScriptDesc = 'Script to update data import properties for operator code translation data'

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