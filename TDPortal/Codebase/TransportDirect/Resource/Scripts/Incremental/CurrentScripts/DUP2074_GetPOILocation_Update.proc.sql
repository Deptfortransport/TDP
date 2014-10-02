-- ***********************************************
-- NAME           : DUP2074_GetPOILocation_Update.proc.sql
-- DESCRIPTION    : GetPoiLocation stored procedure update
-- AUTHOR         : Mitesh Modi
-- DATE           : 22 Aug 2013
-- ***********************************************
USE [AtosAdditionalData]
GO

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

-- =============================================
-- Stored procedure to GetPoiLocation
-- =============================================
ALTER PROCEDURE [dbo].[GetPoiLocation]
	@poi varchar(100)
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
		WHERE [DATASETID] = @poi
		OR [DisplayName] = @poi
		AND [Type] = 'POI'
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2074, 'GetPoiLocation stored procedure update'

GO