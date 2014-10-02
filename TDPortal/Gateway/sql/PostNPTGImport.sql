-- ************************************************************
-- NAME 	: PostNPTGImport.sql
-- DESCRIPTION 	: updates NPTG_Staging database after each weekly
-- refresh of data. This is because some test values are still
-- currently required.
-- ************************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/build/DataGatewayApps/Gateway/sql/PostNPTGImport.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 11:53:44   mturner
--Initial revision.

USE [NPTG_Staging]

UPDATE [TRAVELINE REGIONS] SET [Traveline Region Id] = 'EA',[Region Name] = 'East Anglia',
[Primary URL] = 'http://192.12.40.231/scripts/xmlplanner.dll',[Secondary URL] = '',
[Tertiary URL] = '', [JW Version] = '2.1', [Date of Issue] = '1960-01-01 00:00:00',
[Issue Version] = '33'
WHERE [Traveline Region Id] = 'EA'

UPDATE [TRAVELINE REGIONS] SET [Traveline Region Id] = 'EM',[Region Name] = 'East Midlands',
[Primary URL] = 'http://192.12.40.230/scripts/xmlplanner.dll',[Secondary URL] = '',
[Tertiary URL] = '', [JW Version] = '2.1', [Date of Issue] = '1960-01-01 00:00:00',
[Issue Version] = '33'
WHERE [Traveline Region Id] = 'EM'

UPDATE [TRAVELINE REGIONS] SET [Traveline Region Id] = 'L',[Region Name] = 'London',
[Primary URL] = 'http://62.8.192.113:8080/JourneyWeb.cgi',[Secondary URL] = '',
[Tertiary URL] = '', [JW Version] = '2.1', [Date of Issue] = '1960-01-01 00:00:00',
[Issue Version] = '33'
WHERE [Traveline Region Id] = 'L'

UPDATE [TRAVELINE REGIONS] SET [Traveline Region Id] = 'S',[Region Name] = 'Scotland',
[Primary URL] = 'http://80.75.65.37/cgi-bin/jpclient.exe',[Secondary URL] = '',
[Tertiary URL] = '', [JW Version] = '2.1', [Date of Issue] = '1960-01-01 00:00:00',
[Issue Version] = '33'
WHERE [Traveline Region Id] = 'S'

UPDATE [TRAVELINE REGIONS] SET [Traveline Region Id] = 'SE',[Region Name] = 'South East',
[Primary URL] = 'http://62.8.192.113:8080/JourneyWeb.cgi',[Secondary URL] = '',
[Tertiary URL] = '', [JW Version] = '2.1', [Date of Issue] = '1960-01-01 00:00:00',
[Issue Version] = '33'
WHERE [Traveline Region Id] = 'SE'

UPDATE [TRAVELINE REGIONS] SET [Traveline Region Id] = 'SW',[Region Name] = 'South West',
[Primary URL] = 'http://217.45.201.153/jweb/sw',[Secondary URL] = '',
[Tertiary URL] = '', [JW Version] = '2.1', [Date of Issue] = '1960-01-01 00:00:00',
[Issue Version] = '33'
WHERE [Traveline Region Id] = 'SW'

UPDATE [TRAVELINE REGIONS] SET [Traveline Region Id] = 'W',[Region Name] = 'Wales',
[Primary URL] = 'http://217.45.201.153/jweb/wales',[Secondary URL] = '',
[Tertiary URL] = '', [JW Version] = '2.1', [Date of Issue] = '1960-01-01 00:00:00',
[Issue Version] = '33'
WHERE [Traveline Region Id] = 'W'

UPDATE [TRAVELINE REGIONS] SET [Traveline Region Id] = 'Y',[Region Name] = 'Yorkshire',
[Primary URL] = 'http://217.45.201.153/jweb/york',[Secondary URL] = '',
[Tertiary URL] = '', [JW Version] = '2.1', [Date of Issue] = '1960-01-01 00:00:00',
[Issue Version] = '33'
WHERE [Traveline Region Id] = 'Y'

UPDATE [TRAVELINE REGIONS] SET [Traveline Region Id] = 'NW',[Region Name] = 'North West',
[Primary URL] = 'http://217.45.201.153/jweb/nw',[Secondary URL] = '',
[Tertiary URL] = '', [JW Version] = '2.1', [Date of Issue] = '1960-01-01 00:00:00',
[Issue Version] = '33'
WHERE [Traveline Region Id] = 'NW'

GO 

INSERT INTO AREPs VALUES ('EA','EM','05003135','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EA','EM','05003142','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EA','EM','05003334','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EA','EM','0500PT1452','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EA','EM','0500PT617','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EA','EM','0500PT94','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EA','EM','0590PET622','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EA','EM','0590PHS371','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EA','EM','2700LSBB4192','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EA','EM','300000269MA','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EM','EA','05003135','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EM','EA','05003142','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EM','EA','05003334','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EM','EA','0500PT1452','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EM','EA','0500PT617','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EM','EA','0500PT94','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EM','EA','0590PET622','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EM','EA','0590PHS371','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EM','EA','2700LSBB4192','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
INSERT INTO AREPS VALUES ('EM','EA','300000269MA','2003-09-17 00:00:00','2003-09-17 00:00:00','20',0,0)
GO

DELETE FROM UNSUPPORTED
WHERE ([Traveline Region Id] = 'EA' and [Capability] = 201)
GO

DELETE FROM UNSUPPORTED
WHERE ([Traveline Region Id] = 'EM' and [Capability] = 201)
GO



