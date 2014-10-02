-- ***********************************************
-- NAME 		: DUP1995_GISQueryEvent_Properties_Update.sql
-- DESCRIPTION 	: Script to update GISQueryEvent logging type flags
-- AUTHOR		: Mitesh Modi
-- DATE			: 11 Feb 2013
-- ************************************************

USE [PermanentPortal]
GO


-- Flags to log specfic GISQueryEvent types
IF exists (select top 1 * from properties where pName like 'Logging.Event.Custom.GISQUERY.EventTypeSwitch.%' and ThemeId = 1)
BEGIN
	DELETE FROM properties WHERE pName like 'Logging.Event.Custom.GISQUERY.EventTypeSwitch.%' and ThemeId = 1
END
BEGIN
	insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestStops', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestLocality', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestITN', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestITNs', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestPointOnTOID', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestCarParks', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestAccessibleStops', 'True', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestAccessibleLocalities', 'True', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindNearestStopsAndITNs', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindExchangePointsInRadius', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindStopsInRadius', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindStopsInGroupForStops', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.FindStopsInfoForStops', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.IsPointsInCycleDataArea', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.IsPointsInWalkDataArea', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.IsPointInAccessibleLocation', 'True', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.GetExchangeInfoForNaptan', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.GetNPTGInfoForNaPTAN', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.GetLocalityInfoForNatGazID', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.GetStreetsFromPostcode', 'False', 'Web', 'UserPortal', 0, 1)
    insert into properties values ('Logging.Event.Custom.GISQUERY.EventTypeSwitch.GetDistancesForTOIDs', 'False', 'Web', 'UserPortal', 0, 1)
END

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 1995
SET @ScriptDesc = 'GISQueryEvent properties update'

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