-- ***********************************************
-- NAME           : DUP1992_AccessibleLocation_DataImport_Procs.sql
-- DESCRIPTION    : Script to add accessible location data import procedures
-- AUTHOR         : Mitesh Modi
-- DATE           : 05 Feb 2013
-- ***********************************************

-- ************************************************************************************************
-- THIS SCRIPT MUST BE AMENDED TO ASSIGN PERMISSIONS TO THE CORRECT USER.
-- SEE BOTTOM OF SCRIPT.
-- ************************************************************************************************

USE [AtosAdditionalData]
GO

-- *****************************************
-- Create function needed by import procedure
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetAccesibleStopType]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
	EXEC ('
        CREATE FUNCTION [dbo].[GetAccesibleStopType]
        ()
		RETURNS VARCHAR(20)
		AS
		BEGIN
			DECLARE @StopType VARCHAR(20)
			RETURN @StopType
		END
    ')
GO

-- Update function
ALTER FUNCTION [dbo].[GetAccesibleStopType]
(
	@StopNaPTAN VARCHAR(20), 
	@StopOperator VARCHAR(4)
)
RETURNS VARCHAR(20)
AS
BEGIN
	DECLARE @StopType VARCHAR(20)

	-- Default to Bus
	SELECT @StopType = 'Bus'

	IF (LEFT(@StopNaPTAN,4) = '9100')
		SELECT @StopType = 'Rail'

	IF (LEFT(@StopNaPTAN,4) = '9200')
		SELECT @StopType = 'Air'

	IF (LEFT(@StopNaPTAN,4) = '9000')
		SELECT @StopType = 'Coach'

	IF (LEFT(@StopNaPTAN,4) = '9400')
	BEGIN
		SELECT @StopType = 'Tram'

		IF(@StopOperator = 'LU')
			SELECT @StopType = 'Underground'

		IF(@StopOperator = 'DL')
			SELECT @StopType = 'DLR'

	END

	IF (LEFT(@StopNaPTAN,4) = '9300')
		SELECT @StopType = 'Ferry'

	RETURN @StopType

END

GO

-- *****************************************
-- Create stored proc if needed
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ImportAccessibleLocationsData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE ImportAccessibleLocationsData
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- Update stored proc
ALTER PROCEDURE [dbo].[ImportAccessibleLocationsData]
(
	@XML text
)
AS
BEGIN
    -- ImportAccessibleLocations strored proc to insert accessible locations data
	SET NOCOUNT ON

	DECLARE @DocID int, @XMLPathData varchar(100)

	-- Loading xml document 
	EXEC sp_xml_preparedocument @DocID OUTPUT, @XML
	-- setting the node 
	SET @XMLPathData =  '/AccessibleLocationsData/AccessibleLocation'

	-- starting trasaction 
	BEGIN TRANSACTION

		-- Delete from accessible locations table
		DELETE FROM [dbo].[AccessibleLocations]

		-- Insert accessible locations data
		INSERT INTO [dbo].[AccessibleLocations](
				[StopNaptan]
			   ,[StopName] 
			   ,[StopAreaNaPTAN]
			   ,[StopOperator]
			   ,[WheelchairAccess]
			   ,[AssistanceService]
			   ,[WEFDate]
			   ,[WEUDate]
			   ,[MOFRStartTime]
			   ,[MOFREndTime]
			   ,[SatStartTime]
			   ,[SatEndTime]
			   ,[SunStartTime]
			   ,[SunEndTime]
			   ,[StopCountry]
			   ,[AdministrativeAreaCode]
			   ,[NPTGDistrictCode]
			   ,[StopType])
		SELECT
				X.[StopNaPTAN]
			   ,X.[StopName] 
			   ,X.[StopAreaNaPTAN]
			   ,X.[StopOperator]
			   ,CASE WHEN X.[WheelchairAccessBool] = 'TRUE' THEN 1 ELSE 0 END
			   ,CASE WHEN X.[AssistanceServiceBool] = 'TRUE' THEN 1 ELSE 0 END
			   ,CAST ((X.[WEFDate]) AS DATE)
			   ,CAST ((X.[WEUDate]) AS DATE)
			   ,CAST ((X.[MOFRStartTime]) AS TIME)
			   ,CAST ((X.[MOFREndTime]) AS TIME)
			   ,CAST ((X.[SatStartTime]) AS TIME)
			   ,CAST ((X.[SatEndTime]) AS TIME)
			   ,CAST ((X.[SunStartTime]) AS TIME)
			   ,CAST ((X.[SunEndTime]) AS TIME)
			   ,''
			   ,-1
			   ,-1
			   ,[dbo].[GetAccesibleStopType](X.[StopNaPTAN],X.[StopOperator])
		FROM
		OPENXML (@DocID, @XMLPathData, 2)
		WITH
		(
			   [StopNaPTAN] [nvarchar](20),
			   [StopName] [nvarchar](150),
			   [StopAreaNaPTAN] [nvarchar](20),
			   [StopOperator] [nvarchar](4),
			   [WheelchairAccessBool] [nvarchar](5),
			   [AssistanceServiceBool] [nvarchar](5),
			   [WEFDate] [nvarchar](6),
			   [WEUDate] [nvarchar](6),
			   [MOFRStartTime] [nvarchar](6),
			   [MOFREndTime] [nvarchar](6),
			   [SatStartTime] [nvarchar](6),
			   [SatEndTime] [nvarchar](6),
			   [SunStartTime] [nvarchar](6),
			   [SunEndTime] [nvarchar](6)
		) X 

	-- Removing xml doc from memorry
	EXEC sp_xml_removedocument @DocID
	
	IF @@ERROR<>0
		ROLLBACK TRANSACTION
	ELSE
	BEGIN
		COMMIT TRANSACTION		
		
		UPDATE ChangeNotification
		SET Version = Version + 1
		WHERE [Table] = 'LocationAccessibleCache'
	END
	
END
GO


-- *****************************************
-- Create stored proc if needed
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ImportAccessibleAdminAreasData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE ImportAccessibleAdminAreasData 	
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- Update stored proc
ALTER PROCEDURE [dbo].[ImportAccessibleAdminAreasData]
(
	@XML text
)
AS
BEGIN
    -- ImportAccessibleLocations strored proc to return all accessible locations
	SET NOCOUNT ON

	DECLARE @DocID int, @XMLPathData varchar(100)

	-- Loading xml document 
	EXEC sp_xml_preparedocument @DocID OUTPUT, @XML
	-- setting the node 
	SET @XMLPathData =  '/AccessibleAdminAreasData/AccessibleAdminArea'

	-- starting trasaction 
	BEGIN TRANSACTION

		-- Delete from SJPGNATAdminAreas table
		DELETE FROM [dbo].[AccessibleAdminAreas]

		-- Insert GNAT AdminAreas data
		INSERT INTO [dbo].[AccessibleAdminAreas](
				[AdministrativeAreaCode]
			   ,[DistrictCode]
			   ,[StepFree]
			   ,[Assistance])
		SELECT
				X.[AdministrativeAreaCode]
			   ,X.[DistrictCode] 
			   ,CASE WHEN X.[StepFree] = 'True' THEN 1 ELSE 0 END
			   ,CASE WHEN X.[Assistance] = 'True' THEN 1 ELSE 0 END
		FROM
		OPENXML (@DocID, @XMLPathData, 2)
		WITH
		(
			   [AdministrativeAreaCode] [nvarchar](8),
			   [DistrictCode] [nvarchar](8),
			   [StepFree] [nvarchar](5),
			   [Assistance] [nvarchar](5)
		) X 

	COMMIT TRANSACTION 

	-- Removing xml doc from memorry
	EXEC sp_xml_removedocument @DocID

END
GO
	
-- *****************************************
-- Grant permissions - will only work for relevant users as only they
-- will exist
GRANT  EXECUTE  ON [dbo].[GetAccesibleStopType]  TO [BBPTDPSIS\ASPUSER_S]
GRANT  EXECUTE  ON [dbo].[GetAccesibleStopType]  TO [BBPTDPS\ASPUSER_S]
GRANT  EXECUTE  ON [dbo].[GetAccesibleStopType]  TO [ACPTDPS\ASPUSER_S]

GRANT  EXECUTE  ON [dbo].[ImportAccessibleLocationsData]  TO [BBPTDPSIS\ASPUSER_S]
GRANT  EXECUTE  ON [dbo].[ImportAccessibleLocationsData]  TO [BBPTDPS\ASPUSER_S]
GRANT  EXECUTE  ON [dbo].[ImportAccessibleLocationsData]  TO [ACPTDPS\ASPUSER_S]

GRANT  EXECUTE  ON [dbo].[ImportAccessibleAdminAreasData]  TO [BBPTDPSIS\ASPUSER_S]
GRANT  EXECUTE  ON [dbo].[ImportAccessibleAdminAreasData]  TO [BBPTDPS\ASPUSER_S]
GRANT  EXECUTE  ON [dbo].[ImportAccessibleAdminAreasData]  TO [ACPTDPS\ASPUSER_S]

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 1992
SET @ScriptDesc = 'Script to add accessible location data import procedures'

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