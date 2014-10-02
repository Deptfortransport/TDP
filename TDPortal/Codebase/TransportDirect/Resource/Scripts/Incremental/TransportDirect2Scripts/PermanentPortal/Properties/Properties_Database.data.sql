-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : ConnectionString properties data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

------------------------------------------------
-- ConnectionString properties
------------------------------------------------

DECLARE @AID varchar(50) = '<Default>'
DECLARE @GID varchar(50) = 'TDPortal'
DECLARE @PartnerID int = 0
DECLARE @ThemeID int = 1

EXEC AddUpdateProperty 'DefaultDB', 'Data Source=localhost;Initial Catalog=PermanentPortal;Pooling=True;Timeout=30;User id=SJP_User;Password=!password!1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'TransientPortalDB', 'Data Source=localhost;Initial Catalog=TransientPortal;Pooling=True;Timeout=30;User id=SJP_User;Password=!password!1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'GazetteerDB', 'Data Source=localhost;Initial Catalog=TDPGazetteer;Pooling=True;Timeout=30;User id=SJP_User;Password=!password!1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ContentDB', 'Data Source=localhost;Initial Catalog=Content;Pooling=True;Timeout=30;User id=SJP_User;Password=!password!1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AirDataProviderDB', 'Data Source=localhost;Initial Catalog=AirRouteMatrix;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AdditionalDataDB', 'data source=DBM; initial catalog=AdditionalData; user id=steve; password=password; persist security info=False', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'AtosAdditionalDataDB', 'Data Source=localhost;Initial Catalog=AtosAdditionalData;Timeout=30;User id=SJP_User;Password=!password!1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'ReportStagingDB', 'Data Source=MIS;Initial Catalog=TDPReportStaging;Timeout=30;User id=SJP_User;Password=!password!1', @AID, @GID, @PartnerID, @ThemeID
EXEC AddUpdateProperty 'CommandControlDB','Data Source=MIS;Initial Catalog=CommandAndControl;Pooling=True;Timeout=30;User id=SJP_User;Password=!password!1', @AID, @GID, @PartnerID, @ThemeID

GO


-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'ConnectionString properties data'