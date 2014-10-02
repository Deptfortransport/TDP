-- ***********************************************
-- NAME           : DUP2095_Properties_DepartureBoardWebService_EventLogging.sql
-- DESCRIPTION    : DepartureBoardWebService EventLogging properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 27 Nov 2013
-- ***********************************************
USE [PermanentPortal]
GO

-- ************************************************************************************************
-- UPDATE THE Logging.Event.Operational.TraceLevel TO APPROPRIATE ENVIRONMENT LEVEL
-- ************************************************************************************************

------------------------------------------------
-- 'DepartureBoardWebService EventLogging' properties
------------------------------------------------

DECLARE @AID varchar(50) = 'DepartureBoardWebService'
DECLARE @GID varchar(50) = 'UserPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- Publishers
EXEC AddUpdateProperty 'Logging.Publisher.Default',	'FILE1', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Publisher.Console', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Custom', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Email', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.EventLog', '', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.File', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Queue', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID

-- Publisher Configurations
EXEC AddUpdateProperty 'Logging.Publisher.File.FILE1.Directory', 'D:\TDPortal', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.File.FILE1.Rotation', '20000', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'Logging.Publisher.Queue.QUEUE1.Delivery', 'Recoverable', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Queue.QUEUE1.Path', '.\Private$\TDPrimaryQueue', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Publisher.Queue.QUEUE1.Priority', 'Normal', @AID, @GID, @PartnerID, @ThemeID


-- Event Logging Level and Publisher mapping
EXEC AddUpdateProperty 'Logging.Event.Operational.TraceLevel', 'Verbose', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Verbose.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Info.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Warning.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Operational.Error.Publishers', 'FILE1', @AID, @GID, @PartnerID, @ThemeID

-- Custom Events
EXEC AddUpdateProperty 'Logging.Event.Custom.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom', 'RTTI RTTIInternal', @AID, @GID, @PartnerID, @ThemeID

-- RTTI event
EXEC AddUpdateProperty 'Logging.Event.Custom.RTTI.Assembly', 'td.reportdataprovider.tdpcustomevents', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RTTI.Name', 'RTTIEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RTTI.Publishers', 'FILE1 QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RTTI.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

-- RTTIInternal event
EXEC AddUpdateProperty 'Logging.Event.Custom.RTTIInternal.Assembly', 'td.reportdataprovider.tdpcustomevents', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RTTIInternal.Name', 'RTTIInternalEvent', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RTTIInternal.Publishers', 'FILE1 QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'Logging.Event.Custom.RTTIInternal.Trace', 'On', @AID, @GID, @PartnerID, @ThemeID

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2095, 'DepartureBoardWebService EventLogging properties data'