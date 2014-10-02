-- ***********************************************
-- NAME           : Properties_EventReceiver.data.sql
-- DESCRIPTION    : TDP EventReceiver properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 01 Aug 2013
-- ***********************************************
USE [PermanentPortal]
GO

------------------------------------------------
-- 'EventReceiver' properties
------------------------------------------------

DECLARE @AID varchar(50) = 'TDPEventReceiver'
DECLARE @GID varchar(50) = 'TDReporting'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- Property Service
EXEC AddUpdateProperty 'propertyservice.version', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'propertyservice.refreshrate', '300000', @AID, @GID, @PartnerID, @ThemeID

-- Message Queues to monitor for events
EXEC AddUpdateProperty 'EventReceiver.Queue', 'QUEUE1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'EventReceiver.Queue.QUEUE1.Path', '.\Private$\TDSecondaryQueue', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'EventReceiver.TimeBeforeRecovery.Millisecs', '60000', @AID, @GID, @PartnerID, @ThemeID

GO