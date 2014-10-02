-- ***********************************************
-- NAME 		: DUP2090_AccessibleLinks_ExternalLinks_Update.sql
-- DESCRIPTION 	: External links updated for accessible information for transport modes
-- AUTHOR		: Mitesh Modi
-- DATE			: 02 Jan 2014
-- ************************************************

USE [TransientPortal]
GO

EXEC AddExternalLink 'AccessibilityInformation.Air', 'https://www.gov.uk/transport-disabled/planes', '', 'DPTAC Going by air', ''
EXEC AddExternalLink 'AccessibilityInformation.Bus', 'https://www.gov.uk/transport-disabled/cars-buses-and-coaches', '', 'DPTAC Buses and coaches', ''
EXEC AddExternalLink 'AccessibilityInformation.Car', 'https://www.gov.uk/transport-disabled/cars-buses-and-coaches', '', 'DPTAC Motoring', ''
EXEC AddExternalLink 'AccessibilityInformation.Coach', 'https://www.gov.uk/transport-disabled/cars-buses-and-coaches', '', 'DPTAC Buses and coaches', ''
EXEC AddExternalLink 'AccessibilityInformation.Drt', 'https://www.gov.uk/transport-disabled/cars-buses-and-coaches', '', 'DPTAC Buses and coaches', ''
EXEC AddExternalLink 'AccessibilityInformation.Ferry', 'https://www.gov.uk/transport-disabled', '', 'DPTAC Ferries', ''
EXEC AddExternalLink 'AccessibilityInformation.Metro', 'https://www.gov.uk/transport-disabled/trains', '', 'DPTAC Light rapid transport systems', ''
EXEC AddExternalLink 'AccessibilityInformation.Rail', 'https://www.gov.uk/transport-disabled/trains', '', 'DPTAC Going by rail', ''
EXEC AddExternalLink 'AccessibilityInformation.RailReplacementBus', 'https://www.gov.uk/transport-disabled/cars-buses-and-coaches', '', 'DPTAC Buses and coaches', ''
EXEC AddExternalLink 'AccessibilityInformation.Taxi', 'https://www.gov.uk/transport-disabled/taxis-and-minicabs', '', 'DPTAC Taxis and private hire', ''
EXEC AddExternalLink 'AccessibilityInformation.Telecabine', 'http://www.tfl.gov.uk/gettingaround/transportaccessibility/23849.aspx', '', 'TFL Telecabine accessibility information', ''
EXEC AddExternalLink 'AccessibilityInformation.Tram', 'https://www.gov.uk/transport-disabled/trains', '', 'DPTAC Light rapid transport systems', ''
EXEC AddExternalLink 'AccessibilityInformation.Underground', 'https://www.gov.uk/transport-disabled/trains', '', 'DPTAC Underground and subway systems', ''

EXEC AddExternalLink 'AccessibilityInformation.BeforeTravel', 'https://www.gov.uk/transport-disabled', '', 'DPTAC Before you travel', ''

EXEC AddExternalLink 'DisabilityInformation', 'https://www.gov.uk/transport-disabled', '', 'Information for travellers with disabilities', ''

GO  

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2090
SET @ScriptDesc = 'External links updated for accessible information for transport modes'

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