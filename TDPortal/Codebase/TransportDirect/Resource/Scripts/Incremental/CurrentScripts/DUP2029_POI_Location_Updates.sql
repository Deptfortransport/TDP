-- ***********************************************
-- NAME 		: DUP2029_POI_Location_Updates.sql
-- DESCRIPTION 	: Adds POI properties
-- AUTHOR		: David Lane
-- DATE			: 04/07/2013
-- ************************************************


USE PermanentPortal

DECLARE @AID NVARCHAR(50)
DECLARE @GID NVARCHAR(50)

-- Coordinate properties
SET @GID = 'UserPortal'
SET @AID = 'Web'

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'LocationService.Cache.SearchLocationsShow.POILimit.Count' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('LocationService.Cache.SearchLocationsShow.POILimit.Count', '5', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '5'
	WHERE pName = 'LocationService.Cache.SearchLocationsShow.POILimit.Count' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'ScriptRepository.LocationSuggest.Script.Name.PTViaDrop' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('ScriptRepository.LocationSuggest.Script.Name.PTViaDrop', 'Location_NoGroupsNoLocalitiesNoPOIs_', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = 'Location_NoGroupsNoLocalitiesNoPOIs_'
	WHERE pName = 'ScriptRepository.LocationSuggest.Script.Name.PTViaDrop' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END



USE [AtosAdditionalData]
GO

-- Create stored proc if needed
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetPoiLocation]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE GetPoiLocation 	
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO
 
-- Create stored proc
ALTER PROCEDURE [dbo].[GetPoiLocation]
	@searchstring varchar(100)
AS
BEGIN
	
		SELECT TOP(1) 
			 [DATASETID]
			,[ParentID]
			,[Name]
			,[DisplayName]
			,[Type]
			,[Naptan]
			,[LocalityID]
			,[Easting]
			,[Northing]
			,[NearestTOID]
			,[NearestPointE]
			,[NearestPointN]
			,[AdminAreaID]
			,[DistrictID]
		FROM [GAZ].[gazadmin].[TDLocations]
		WHERE DisplayName = @searchstring
		AND [Type] = 'POI'
END
GO

	
-- Grant permissions - will only work for relevant users as only they
-- will exist
GRANT  EXECUTE  ON [dbo].[GetPoiLocation]  TO [BBPTDPSIW\aspuser]
GRANT  EXECUTE  ON [dbo].[GetPoiLocation]  TO [BBPTDPW\aspuser]
GRANT  EXECUTE  ON [dbo].[GetPoiLocation]  TO [ACPTDPW\aspuser]
GRANT  EXECUTE  ON [dbo].[GetPoiLocation]  TO [BBPTDPSIS\aspuser]
GRANT  EXECUTE  ON [dbo].[GetPoiLocation]  TO [BBPTDPS\aspuser]
GRANT  EXECUTE  ON [dbo].[GetPoiLocation]  TO [ACPTDPS\aspuser]
GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2029
SET @ScriptDesc = 'POI locations updates'

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