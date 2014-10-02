USE [PermanentPortal]
GO

DECLARE @AID_DEFAULT varchar(50) = '<DEFAULT>'
DECLARE @AID_Web varchar(50) = 'Web'
DECLARE @AID_RHost varchar(50) = 'TDRemotingHost'
DECLARE @AID_PHost varchar(50) = 'TDPlannerHost'
DECLARE @AID_ES varchar(50) = 'ExposedServices'
DECLARE @AID_EES varchar(50) = 'EnhancedExposedServices'
DECLARE @AID_ER varchar(50) = 'EventReceiver'
DECLARE @AID_DG varchar(50) = ''
DECLARE @AID_CCS varchar(50) = 'CoordinateConvertorService'
DECLARE @AID_TTF varchar(50) = 'TicketTypeFeed'
DECLARE @AID_TWS varchar(50) = 'TransactionWebService'
DECLARE @AID_USE varchar(50) = 'UserSurveyExport'
DECLARE @AID_BATCH varchar(50) = 'BatchJourneyPlannerService'

DECLARE @GID_DEFAULT varchar(50) = '<DEFAULT>'
DECLARE @GID_UPortal varchar(50) = 'UserPortal'
DECLARE @GID_RHost varchar(50) = 'TDRemotingHost'
DECLARE @GID_PHost varchar(50) = 'TDPlannerHost'
DECLARE @GID_RDP varchar(50) = 'ReportDataProvider'
DECLARE @GID_DG varchar(50) = 'DataGateway'

-------------------------------------------------------------
DELETE FROM [dbo].[properties]
WHERE  pName in (
		'DefaultDB',
		'EsriDB',
		'ASPStateDB',
		'ReportDataDB',
		'TransientPortalDB',
		'UserInfoDB',
        'AirDataProviderDB',
		'ReportStagingDB',
		'ProductAvailabilityDB',
		'PlacesDB',
		'AtosAdditionalDataDB',
        'CarParksDB',
        'GAZ_StagingDB',
        'InternationalDataDB',
        'SJPGazetteerDB',
        'SJPTransientPortalDB',
        'BatchJourneyPlannerDB',
        'BatchJourneyPlannerDBLongTimeout')

INSERT INTO [properties] VALUES (
'AirDataProviderDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=AirRouteMatrix;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'ASPStateDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=ASPState;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'AtosAdditionalDataDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=AtosAdditionalData;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'BatchJourneyPlannerDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=BatchJourneyPlanner;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'BatchJourneyPlannerDBLongTimeout', 'Data Source=.\SQLEXPRESS;Initial Catalog=BatchJourneyPlanner;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'CarParksDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=CarParks;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'DefaultDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=PermanentPortal;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'EsriDB', 'Data Source=DBM;Initial Catalog=transientGIS;Pooling=False;Timeout=30;User id=steve;Password=password', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'InternationalDataDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=InternationalData;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'PlacesDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=PermanentPortal;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'ProductAvailabilityDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=ProductAvailability;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'ReportDataDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=Reporting;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'ReportStagingDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=ReportStagingDB;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'TransientPortalDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=TransientPortal;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)
INSERT INTO [properties] VALUES (
'UserInfoDB', 'Data Source=.\SQLEXPRESS;Initial Catalog=TDUserInfo;Pooling=False;Timeout=30;User id=SJP_User;Password=!password!1', @AID_DEFAULT, @GID_DEFAULT, 0, 1)


UPDATE [dbo].[properties]
SET pValue = 'data source=DBM; initial catalog=AdditionalData; user id=steve; password=password; persist security info=False'
WHERE  pValue = 'data source=SID01; initial catalog=AdditionalData; user id=steve; password=password; persist security info=False'

UPDATE [dbo].[properties]
SET pValue = 'data source=DBM; initial catalog=AdditionalData; user id=steve; password=password; persist security info=False'
WHERE  pValue = 'Server=D03;Initial Catalog=AdditionalData;Trusted_Connection=true;'
-------------------------------------------------------------


UPDATE [dbo].[properties]
SET pValue = 'UserFeedbackEvent'
WHERE  pName = 'Logging.Event.Custom.USERFEEDBACK.Name'

-------------------------------------------------------------
-- Batch

UPDATE [dbo].[properties]
SET pValue = 'false'
WHERE  pName = 'BatchJourneyPlannerTestMode'
AND AID = @AID_BATCH

INSERT INTO [properties] VALUES (
'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.db', 'DefaultDB', @AID_BATCH, @GID_UPortal, 0, 1)

INSERT INTO [properties] VALUES (
'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.query', 'SELECT KeyName, Value, Category, TicketGroup, Data1 FROM CategorisedHashes WHERE DataSet = ''DisplayableRailTickets''', @AID_BATCH, @GID_UPortal, 0, 1)

INSERT INTO [properties] VALUES (
'TransportDirect.UserPortal.DataServices.DisplayableRailTickets.type', '6', @AID_BATCH, @GID_UPortal, 0, 1)

INSERT INTO [properties] VALUES (
'TransportDirect.UserPortal.DataServices.DisplayableSupplements.db', 'DefaultDB', @AID_BATCH, @GID_UPortal, 0, 1)

INSERT INTO [properties] VALUES (
'TransportDirect.UserPortal.DataServices.DisplayableSupplements.query', 'SELECT listitem FROM Lists WHERE dataset = ''DisplayableSupplements'' ', @AID_BATCH, @GID_UPortal, 0, 1)

INSERT INTO [properties] VALUES (
'TransportDirect.UserPortal.DataServices.DisplayableSupplements.type', '1', @AID_BATCH, @GID_UPortal, 0, 1)

INSERT INTO [properties] VALUES (
'TransportDirect.UserPortal.DataServices.FindABusCheck.db', 'DefaultDB', @AID_BATCH, @GID_UPortal, 0, 1)

INSERT INTO [properties] VALUES (
'TransportDirect.UserPortal.DataServices.FindABusCheck.query', 'SELECT ResourceID, ItemValue, IsSelected FROM DropDownLists WHERE DataSet = ''FindABusCheck'' AND PartnerId = 0 And ThemeId = 1 ORDER BY SortOrder', @AID_BATCH, @GID_UPortal, 0, 1)

INSERT INTO [properties] VALUES (
'TransportDirect.UserPortal.DataServices.FindABusCheck.type', '3', @AID_BATCH, @GID_UPortal, 0, 1)

-- GATEWAY
DELETE FROM [PermanentPortal].[dbo].[FTP_CONFIGURATION]
WHERE DATA_FEED = 'ert199'or DATA_FEED = 'loe147'

INSERT INTO [PermanentPortal].[dbo].[FTP_CONFIGURATION] VALUES (
1, 'ert199', 'Localhost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/ert199', './ert199', '*.zip', 0, 1, '2000-01-01 00:00:00.000', '', 1)

INSERT INTO [PermanentPortal].[dbo].[FTP_CONFIGURATION] VALUES (
1, 'loe147', 'Localhost', 'TDP28Nov', 'sI1732#3-', 'D:/Gateway/dat/Incoming/loe147', './loe147', '*.zip', 0, 1, '2000-01-01 00:00:00.000', '', 1)


DELETE FROM [PermanentPortal].[dbo].[IMPORT_CONFIGURATION]
WHERE DATA_FEED = 'ert199' or DATA_FEED = 'loe147'

INSERT INTO [PermanentPortal].[dbo].[IMPORT_CONFIGURATION] VALUES (
'ert199', 'TransportDirect.UserPortal.JourneyControl.ServiceDetailRequestOperatorCodesDataImporter', 'td.userportal.journeycontrol.dll,', '', '', '', 'D:/Gateway/dat/Processing/ert199')

INSERT INTO [PermanentPortal].[dbo].[IMPORT_CONFIGURATION] VALUES (
'loe147', 'TransportDirect.UserPortal.JourneyControl.JourneyNoteFilterDataImporter', 'td.userportal.journeycontrol.dll', '', '', '', 'D:/Gateway/dat/Processing/loe147')


UPDATE [dbo].[properties]
SET pValue = 'D:\Gateway\xml\JourneyNoteFilter.xsd'
WHERE  pName = 'datagateway.sqlimport.JourneyNoteFilterData.schemea'


GO

