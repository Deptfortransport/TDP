-- ***********************************************
-- NAME 		: DUP2008_JourneyNoteFilter_DataImport_Properties.sql
-- DESCRIPTION 	: Script to update data import properties for journey note filter
-- AUTHOR		: Mitesh Modi
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
WHERE  pName LIKE 'datagateway.sqlimport.JourneyNoteFilterData%'

-- AID and GID
DECLARE @AID varchar(50) = ''
DECLARE @GID varchar(50) = 'DataGateway'

-- Accessible Operator Data
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.JourneyNoteFilterData.database', @AID, @GID, 0, 1, N'TransientPortalDB')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.JourneyNoteFilterData.feedname', @AID, @GID, 0, 1, N'loe147')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.JourneyNoteFilterData.Name', @AID, @GID, 0, 1, N'JourneyNoteFilterData')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.JourneyNoteFilterData.schemea', @AID, @GID, 0, 1, @gatewayPath + N'\JourneyNoteFilter.xsd')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.JourneyNoteFilterData.sqlcommandtimeout', @AID, @GID, 0, 1, N'3000')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.JourneyNoteFilterData.storedprocedure', @AID, @GID, 0, 1, N'ImportJourneyNoteFilterData')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.JourneyNoteFilterData.xmlnamespace', @AID, @GID, 0, 1, N'http://www.transportdirect.info/JourneyNoteFilter')
INSERT INTO [dbo].[properties] ([pName], [AID], [GID], [PartnerId], [ThemeId], [pValue]) VALUES (N'datagateway.sqlimport.JourneyNoteFilterData.xmlnamespacexsi', @AID, @GID, 0, 1, N'http://www.w3.org/2001/XMLSchema-instance')

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2008
SET @ScriptDesc = 'Script to update data import properties for journey note filter'

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