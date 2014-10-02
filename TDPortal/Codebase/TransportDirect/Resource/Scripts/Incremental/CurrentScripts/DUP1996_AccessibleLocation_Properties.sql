-- ***********************************************
-- NAME 		: DUP1996_AccessibleLocation_Properties.sql
-- DESCRIPTION 	: Accessibility permanent portal - properties and dropdownlists
-- AUTHOR		: Mitesh Modi
-- DATE			: 12 Feb 2013
-- ************************************************

USE [PermanentPortal]
GO

-- TIDY UP (ok to delete as this is first script to add AccessibleOptions properties)
DELETE FROM [properties]
WHERE pName like 'AccessibleOptions.%'

DECLARE @AID_W varchar(50) = 'Web'
DECLARE @AID_RM varchar(50) = 'TDRemotingHost'
DECLARE @AID_DS varchar(50) = 'DataServices'
DECLARE @GID_UP varchar(50) = 'UserPortal'
DECLARE @GID_RM varchar(50) = 'TDRemotingHost'


-- *****************************************************************
-- Accessible options switch
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'AccessibleOptions.Visible.Switch' 
	AND AID = @AID_W AND GID = @GID_UP AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('AccessibleOptions.Visible.Switch', 
	'true', @AID_W, @GID_UP, 0, 1)
END

-- Accessible journey parameters 
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'AccessibleOptions.WalkingSpeed.MetresPerMinute' 
	AND AID = @AID_W AND GID = @GID_UP AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('AccessibleOptions.WalkingSpeed.MetresPerMinute', 
	'70', @AID_W, @GID_UP, 0, 1)
END

IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'AccessibleOptions.WalkingDistance.Metres' 
	AND AID = @AID_W AND GID = @GID_UP AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('AccessibleOptions.WalkingDistance.Metres', 
	'2000', @AID_W, @GID_UP, 0, 1)
END

-- Accessible journey request parameters - olympic/accessible request
-- (sets flag to allow CJP to use the single traveline planner for accessible journeys)
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'AccessibleOptions.AccessibleRequest.Olympic' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('AccessibleOptions.AccessibleRequest.Olympic', 
	'true', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.AccessibleRequest.Olympic', 
	'true', @AID_RM, @GID_RM, 0, 1)
END

-- Accessible journey request parameters - dont force coach
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'AccessibleOptions.AdminAreaCode.London' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('AccessibleOptions.AdminAreaCode.London', 
	'82', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.AdminAreaCode.London', 
	'82', @AID_RM, @GID_RM, 0, 1)
END

IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'AccessibleOptions.DontForceCoach.OriginDestinationLondon' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('AccessibleOptions.DontForceCoach.OriginDestinationLondon', 
	'true', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.DontForceCoach.OriginDestinationLondon', 
	'true', @AID_RM, @GID_RM, 0, 1)
END

IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'AccessibleOptions.DontForceCoach.FewerChanges' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('AccessibleOptions.DontForceCoach.FewerChanges', 
	'false', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.DontForceCoach.FewerChanges', 
	'false', @AID_RM, @GID_RM, 0, 1)
END

-- Accessible journey request parameters - remove awkward overnight
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'AccessibleOptions.RemoveAwkwardOvernight' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('AccessibleOptions.RemoveAwkwardOvernight', 
	'false', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.RemoveAwkwardOvernight', 
	'false', @AID_RM, @GID_RM, 0, 1)
END


-- Accessible location check
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName like 'AccessibleOptions.LocationAccessible.%' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	
	INSERT INTO properties values ('AccessibleOptions.LocationAccessible.AdminAreaCheck.Switch', 
	'false', @AID_W, @GID_UP, 0, 1)
	
	INSERT INTO properties values ('AccessibleOptions.LocationAccessible.AccessibleCacheCheck.Switch', 
	'true', @AID_W, @GID_UP, 0, 1)
	
	INSERT INTO properties values ('AccessibleOptions.LocationAccessible.AccessibleGISQueryCheck.Switch', 
	'true', @AID_W, @GID_UP, 0, 1)

END

-- Accessible location stop - show locality switch
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName like 'AccessibleOptions.TransportTypes.%' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	
	INSERT INTO properties values ('AccessibleOptions.TransportTypes.Locality.Switch', 
	'true', @AID_W, @GID_UP, 0, 1)

END

-- Accessible is accessible stop - configuration
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName like 'AccessibleOptions.IsPointAccessible.%' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	
	INSERT INTO properties values ('AccessibleOptions.IsPointAccessible.Stops.SearchDistance.Metres', 
	'1400', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.IsPointAccessible.Stops.SearchDistance.Metres', 
	'1400', @AID_RM, @GID_RM, 0, 1)
	
	INSERT INTO properties values ('AccessibleOptions.IsPointAccessible.Localities.SearchDistance.Metres', 
	'1400', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.IsPointAccessible.Localities.SearchDistance.Metres', 
	'1400', @AID_RM, @GID_RM, 0, 1)
	
END

-- Accessible find nearest stops - search configuration
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName like 'AccessibleOptions.FindNearestLocations.%' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	
	INSERT INTO properties values ('AccessibleOptions.FindNearestLocations.Stops.SearchDistance.Metres', 
	'20000', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.FindNearestLocations.Stops.SearchDistance.Metres', 
	'20000', @AID_RM, @GID_RM, 0, 1)
	
	INSERT INTO properties values ('AccessibleOptions.FindNearestLocations.Localities.SearchDistance.Metres', 
	'20000', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.FindNearestLocations.Localities.SearchDistance.Metres', 
	'20000', @AID_RM, @GID_RM, 0, 1)
	
	INSERT INTO properties values ('AccessibleOptions.FindNearestLocations.Stops.Count.Max', 
	'10', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.FindNearestLocations.Stops.Count.Max', 
	'10', @AID_RM, @GID_RM, 0, 1)
	
	INSERT INTO properties values ('AccessibleOptions.FindNearestLocations.Localities.Count.Max', 
	'3', @AID_W, @GID_UP, 0, 1)
	INSERT INTO properties values ('AccessibleOptions.FindNearestLocations.Localities.Count.Max', 
	'3', @AID_RM, @GID_RM, 0, 1)
END


-- Accessible features display
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'AccessibleOptions.JourneyDetails.AccessibleFeatures.SuppressForNaPTANs' 
	AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	-- Suppress accessibility icons for stop naptans with following prefix, comma seperated list
	INSERT INTO properties values ('AccessibleOptions.JourneyDetails.AccessibleFeatures.SuppressForNaPTANs', 
	'8100', @AID_W, @GID_UP, 0, 1)
END


-- **********************************************************************
-- ******************** DataServices, UserPortal ************************
-- Accessible options control radio properties
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.AccessibleOptionsRadio.db' 
	AND AID = @AID_DS AND GID = @GID_UP AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('TransportDirect.UserPortal.DataServices.AccessibleOptionsRadio.db', 
	'DefaultDB', @AID_DS, @GID_UP, 0, 1)
END

IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.AccessibleOptionsRadio.query' 
	AND AID = @AID_DS AND GID = @GID_UP AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('TransportDirect.UserPortal.DataServices.AccessibleOptionsRadio.query', 
	'SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''AccessibleOptionsRadio'' AND PartnerId = 0 And ThemeId = 1 ORDER BY SortOrder', @AID_DS, @GID_UP, 0, 1)
END

IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.AccessibleOptionsRadio.type' 
	AND AID = @AID_DS AND GID = @GID_UP AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('TransportDirect.UserPortal.DataServices.AccessibleOptionsRadio.type', 
	'3', @AID_DS, @GID_UP, 0, 1)
END


-- **********************************************************************
-- ******************** DataServices, UserPortal ************************
-- Accessible transport types
IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.AccessibleTransportTypes.db' 
	AND AID = @AID_DS AND GID = @GID_UP AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('TransportDirect.UserPortal.DataServices.AccessibleTransportTypes.db', 
	'DefaultDB', @AID_DS, @GID_UP, 0, 1)
END

IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.AccessibleTransportTypes.query' 
	AND AID = @AID_DS AND GID = @GID_UP AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('TransportDirect.UserPortal.DataServices.AccessibleTransportTypes.query', 
	'SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''AccessibleTransportTypes'' AND PartnerId = 0 And ThemeId = 1 ORDER BY SortOrder', @AID_DS, @GID_UP, 0, 1)
END

IF NOT EXISTS (SELECT TOP 1 * FROM properties WHERE pName = 'TransportDirect.UserPortal.DataServices.AccessibleTransportTypes.type' 
	AND AID = @AID_DS AND GID = @GID_UP AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties values ('TransportDirect.UserPortal.DataServices.AccessibleTransportTypes.type', 
	'3', @AID_DS, @GID_UP, 0, 1)
END

GO  

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 1996
SET @ScriptDesc = 'Acccessibility options properties'

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