-- ***********************************************
-- NAME           : DUP2093_Properties_DepartureBoardService.sql
-- DESCRIPTION    : DepartureBoardService properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 27 Nov 2013
-- ***********************************************
USE [PermanentPortal]
GO

-- ************************************************************************************************
-- UPDATE THE DepartureBoardWebService URL
-- ************************************************************************************************

DECLARE @DepartureBoardWebServiceURL varchar(500)
SET @DepartureBoardWebServiceURL = 'http://localhost/TDPWebServices/DepartureBoardWebService/DepartureBoards.asmx'

-- DEV DepartureBoardWebService url: 'http://localhost/TDPWebServices/DepartureBoardWebService/DepartureBoards.asmx'
-- SITEST/BBP/ACP DepartureBoardWebService url: ''

------------------------------------------------
-- 'DepartureBoardService' properties
------------------------------------------------

DECLARE @AID varchar(50) = 'Web'
DECLARE @GID varchar(50) = 'UserPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- DepartureBoardWebService web service
EXEC AddUpdateProperty 'DepartureBoardService.DepartureBoardWebService.URL', @DepartureBoardWebServiceURL, @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoardService.DepartureBoardWebService.TimeoutMillisecs', '30000', @AID, @GID, @PartnerID, @ThemeID

-- Switch for using the new NRE LDBWebService or the existing NRE Enquiry Ports
EXEC AddUpdateProperty 'DepartureBoardService.RTTIManager.UseLDBWebService.Switch', 'true', @AID, @GID, @PartnerID, @ThemeID

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2093, 'DepartureBoardService properties data'