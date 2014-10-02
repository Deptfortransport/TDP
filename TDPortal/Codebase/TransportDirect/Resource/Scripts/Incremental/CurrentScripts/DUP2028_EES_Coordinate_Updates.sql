-- ***********************************************
-- NAME 		: DUP2028_EES_Coordinate_Updates.sql
-- DESCRIPTION 	        : Adds coordinate upper bounds
-- AUTHOR		: David Lane
-- DATE			: 11/06/2013
-- ************************************************


USE PermanentPortal

DECLARE @AID NVARCHAR(50)
DECLARE @GID NVARCHAR(50)

-- Coordinate properties
SET @GID = 'TDRemotingHost'
SET @AID = 'TDRemotingHost'

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'Coordinate.Validation.Easting.Max' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('Coordinate.Validation.Easting.Max', '800000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '800000'
	WHERE pName = 'Coordinate.Validation.Easting.Max' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'Coordinate.Validation.Northing.Max' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('Coordinate.Validation.Northing.Max', '1350000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '1350000'
	WHERE pName = 'Coordinate.Validation.Northing.Max' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

SET @GID = 'UserPortal'
SET @AID = 'BatchJourneyPlannerService'

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'Coordinate.Validation.Easting.Max' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('Coordinate.Validation.Easting.Max', '800000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '800000'
	WHERE pName = 'Coordinate.Validation.Easting.Max' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'Coordinate.Validation.Northing.Max' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('Coordinate.Validation.Northing.Max', '1350000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '1350000'
	WHERE pName = 'Coordinate.Validation.Northing.Max' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

SET @AID = 'EnhancedExposedServices'

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'Coordinate.Validation.Easting.Max' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('Coordinate.Validation.Easting.Max', '800000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '800000'
	WHERE pName = 'Coordinate.Validation.Easting.Max' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'Coordinate.Validation.Northing.Max' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('Coordinate.Validation.Northing.Max', '1350000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '1350000'
	WHERE pName = 'Coordinate.Validation.Northing.Max' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

SET @AID = 'Web'

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'Coordinate.Validation.Easting.Max' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('Coordinate.Validation.Easting.Max', '800000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '800000'
	WHERE pName = 'Coordinate.Validation.Easting.Max' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'Coordinate.Validation.Northing.Max' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('Coordinate.Validation.Northing.Max', '1350000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '1350000'
	WHERE pName = 'Coordinate.Validation.Northing.Max' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2028
SET @ScriptDesc = 'EES coordinate updates'

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