-- ***********************************************
-- NAME           : DUP2094_Properties_DepartureBoardWebService.sql
-- DESCRIPTION    : DepartureBoardWebService properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 27 Nov 2013
-- ***********************************************
USE [PermanentPortal]
GO

-- ************************************************************************************************
-- UPDATE THE LDBWebService URL
-- UPDATE THE Enquiry Ports sockets RD service
-- ************************************************************************************************

DECLARE @LDBWebServiceURL varchar(500) = 'https://staging.livedepartureboards.co.uk/ldbws/ldb5.asmx'
-- Test/Statging LDBWebService url: 'https://staging.livedepartureboards.co.uk/ldbws/ldb5.asmx'
-- Live LDBWebService url: 'https://realtime.nationalrail.co.uk/LDBWS/ldb5.asmx'

------------------------------------------------
-- 'DepartureBoardWebService' properties
------------------------------------------------

DECLARE @AID varchar(50) = 'DepartureBoardWebService'
DECLARE @GID varchar(50) = 'UserPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

-- Property Service
EXEC AddUpdateProperty 'propertyservice.version', '1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'propertyservice.refreshrate', '300000', @AID, @GID, @PartnerID, @ThemeID

------------------------------------------------
-- LDB web service
EXEC AddUpdateProperty 'DepartureBoards.LDBWebService.URL', @LDBWebServiceURL, @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.LDBWebService.ServiceBindingName', 'LDBServiceSoap', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DepartureBoards.LDBWebService.Service.AccessToken', '5694fdf5-63d9-47b4-a223-f98801cd773d', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.LDBWebService.Service.NumberOfRows.Default', '10', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.LDBWebService.Service.NumberOfRows.Max', '20', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.LDBWebService.Service.Duration.Minutes.Default', '120', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.LDBWebService.Service.Duration.Minutes.Max', '120', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.LDBWebService.Service.TimeOffset.Minutes.Default', '0', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.LDBWebService.Service.TimeOffset.Minutes.Max', '119', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.LDBWebService.Service.TimeOffset.Minutes.Min', '0', @AID, @GID, @PartnerID, @ThemeID

------------------------------------------------

------------------------------------------------
-- Enquiry Ports sockets RD service
-- Live server ip: '141.196.28.219'
-- Live server port: '9130'
EXEC AddUpdateProperty 'DepartureBoards.EnquiryPorts.Service.SocketServer', '141.196.28.219', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.EnquiryPorts.Service.SocketPort', '9130', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DepartureBoards.EnquiryPorts.Service.RetryCount', '3', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.EnquiryPorts.Service.MilliSecondWaitTime', '1000', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'DepartureBoards.EnquiryPorts.Service.NetworkStreamTimeout', '30000', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DepartureBoards.EnquiryPorts.Service.DefaultDuration.Minutes', '240', @AID, @GID, @PartnerID, @ThemeID

-- Enquiry Ports sockets template xml
EXEC AddUpdateProperty 'DepartureBoards.EnquiryPorts.Service.RequestTemplate.StationRequestByCRS', 
'<?xml version="1.0"?>
<Eport xmlns:xsd="http://www.w3.org/2001/XMLSchema"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" msgId="0"
ts="------" version="1.0"
xmlns="http://www.thales-is.com/rtti/EnquriyPorts/v6/rttiEPTSchema.xsd">

  <StationReq>
    <ByCRS crs="---" />
  </StationReq>
</Eport>
', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DepartureBoards.EnquiryPorts.Service.RequestTemplate.TripRequestByCRS', 
'<?xml version="1.0"?>
<Eport xmlns:xsd="http://www.w3.org/2001/XMLSchema"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" msgId="0"
ts="-----" version="1.0"
xmlns="http://www.thales-is.com/rtti/EnquriyPorts/v6/rttiEPTSchema.xsd">

  <TripReq>
    <ByCRS main="---" interest="-----" time="-----" secondary="---"
    dur="120" />
  </TripReq>
</Eport>', @AID, @GID, @PartnerID, @ThemeID

EXEC AddUpdateProperty 'DepartureBoards.EnquiryPorts.Service.RequestTemplate.TrainRequestByRID', 
'<?xml version="1.0"?>
<Eport xmlns:xsd="http://www.w3.org/2001/XMLSchema"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" msgId="0"
ts="-----" version="1.0"
xmlns="http://www.thales-is.com/rtti/EnquriyPorts/v6/rttiEPTSchema.xsd">

  <TrainReq>
    <ByRID rid="-----" />
  </TrainReq>
</Eport>', @AID, @GID, @PartnerID, @ThemeID
------------------------------------------------


GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2094, 'DepartureBoardWebService properties data'