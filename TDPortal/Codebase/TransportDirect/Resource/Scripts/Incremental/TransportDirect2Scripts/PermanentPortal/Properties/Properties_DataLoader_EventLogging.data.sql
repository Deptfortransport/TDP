-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : DataLoader properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

------------------------------------------------
-- 'DataLoader EventLogging' properties
------------------------------------------------

DECLARE @AID varchar(50) = 'DataLoader'
DECLARE @GID varchar(50) = 'DataGateway'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- Publishers
EXEC AddUpdateProperty 'Logging.Publisher.Default',	'FILE1', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Publisher.Console', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Custom', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Email', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.EventLog', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.File', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Queue', '', @AID, @GID, @PartnerID, @ThemeID

-- Publisher Configurations
EXEC AddUpdateProperty 'Logging.Publisher.File.FILE1.Directory', 'D:\TDPortal', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.File.FILE1.Rotation', '20000', @AID, @GID, @PartnerID, @ThemeID

-- Event Logging Level and Publisher mapping
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel', 'Verbose', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Verbose.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Info.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Warning.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Error.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID

-- Custom Events
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace', 'Off', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom', '', @AID, @GID, @PartnerID, @ThemeID

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'EventLogging properties data'