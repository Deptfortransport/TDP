-- ***********************************************
-- NAME 		: DUP2073_Batch_Properties.sql
-- DESCRIPTION 	: Batch properties updates to avoid initialisation errors
-- AUTHOR		: David Lane
-- DATE			: 21 Aug 2013
-- ************************************************

USE [PermanentPortal]
GO

DECLARE @AID_BATCH varchar(50) = 'BatchJourneyPlannerService'
DECLARE @GID_UPortal varchar(50) = 'UserPortal'

IF EXISTS (SELECT 1 FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.db' AND AID = @AID_BATCH)
BEGIN
	UPDATE properties SET pValue = 'DefaultDB' WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.db' AND AID = @AID_BATCH
END
ELSE
BEGIN
	INSERT INTO [properties] VALUES (
	'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.db', 'DefaultDB', @AID_BATCH, @GID_UPortal, 0, 1)
END

IF EXISTS (SELECT 1 FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.query' AND AID = @AID_BATCH)
BEGIN
	UPDATE properties SET pValue = 'SELECT KeyName, Value, Category, TicketGroup, Data1 FROM CategorisedHashes WHERE DataSet = ''DisplayableRailTickets''' WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.query' AND AID = @AID_BATCH
END
ELSE
BEGIN
	INSERT INTO [properties] VALUES (
	'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.query', 'SELECT KeyName, Value, Category, TicketGroup, Data1 FROM CategorisedHashes WHERE DataSet = ''DisplayableRailTickets''', @AID_BATCH, @GID_UPortal, 0, 1)
END

IF EXISTS (SELECT 1 FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.type' AND AID = @AID_BATCH)
BEGIN
	UPDATE properties SET pValue = '6' WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.query' AND AID = @AID_BATCH
END
ELSE
BEGIN
	INSERT INTO [properties] VALUES (
	'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.type', '6', @AID_BATCH, @GID_UPortal, 0, 1)
END

IF EXISTS (SELECT 1 FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableSupplements.db' AND AID = @AID_BATCH)
BEGIN
	UPDATE properties SET pValue = 'DefaultDB' WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableSupplements.db' AND AID = @AID_BATCH
END
ELSE
BEGIN
	INSERT INTO [properties] VALUES (
	'TransportDirect.UserPortal.DataServices.DisplayableSupplements.db', 'DefaultDB', @AID_BATCH, @GID_UPortal, 0, 1)
END

IF EXISTS (SELECT 1 FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableSupplements.query' AND AID = @AID_BATCH)
BEGIN
	UPDATE properties SET pValue = 'SELECT listitem FROM Lists WHERE dataset = ''DisplayableSupplements'' ' WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableSupplements.query' AND AID = @AID_BATCH
END
ELSE
BEGIN
	INSERT INTO [properties] VALUES (
	'TransportDirect.UserPortal.DataServices.DisplayableSupplements.query', 'SELECT listitem FROM Lists WHERE dataset = ''DisplayableSupplements'' ', @AID_BATCH, @GID_UPortal, 0, 1)
END

IF EXISTS (SELECT 1 FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableSupplements.type' AND AID = @AID_BATCH)
BEGIN
	UPDATE properties SET pValue = '1' WHERE pName = 'TransportDirect.UserPortal.DataServices.DisplayableSupplements.type' AND AID = @AID_BATCH
END
ELSE
BEGIN
	INSERT INTO [properties] VALUES (
	'TransportDirect.UserPortal.DataServices.DisplayableSupplements.type', '1', @AID_BATCH, @GID_UPortal, 0, 1)
END

IF EXISTS (SELECT 1 FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.FindABusCheck.db' AND AID = @AID_BATCH)
BEGIN
	UPDATE properties SET pValue = 'DefaultDB' WHERE pName = 'TransportDirect.UserPortal.DataServices.FindABusCheck.db' AND AID = @AID_BATCH
END
ELSE
BEGIN
	INSERT INTO [properties] VALUES (
	'TransportDirect.UserPortal.DataServices.FindABusCheck.db', 'DefaultDB', @AID_BATCH, @GID_UPortal, 0, 1)
END

IF EXISTS (SELECT 1 FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.FindABusCheck.query' AND AID = @AID_BATCH)
BEGIN
	UPDATE properties SET pValue = 'SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''FindABusCheck'' AND PartnerId = 0 And ThemeId = 1 ORDER BY SortOrder' WHERE pName = 'TransportDirect.UserPortal.DataServices.FindABusCheck.query' AND AID = @AID_BATCH
END
ELSE
BEGIN
	INSERT INTO [properties] VALUES (
	'TransportDirect.UserPortal.DataServices.FindABusCheck.query', 'SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''FindABusCheck'' AND PartnerId = 0 And ThemeId = 1 ORDER BY SortOrder', @AID_BATCH, @GID_UPortal, 0, 1)
END

IF EXISTS (SELECT 1 FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.FindABusCheck.type' AND AID = @AID_BATCH)
BEGIN
	UPDATE properties SET pValue = '3' WHERE pName = 'TransportDirect.UserPortal.DataServices.FindABusCheck.type' AND AID = @AID_BATCH
END
ELSE
BEGIN
	INSERT INTO [properties] VALUES (
	'TransportDirect.UserPortal.DataServices.FindABusCheck.type', '3', @AID_BATCH, @GID_UPortal, 0, 1)
END

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2073, 'Update Batch properties'