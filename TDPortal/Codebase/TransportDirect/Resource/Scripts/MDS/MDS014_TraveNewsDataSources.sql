-- **********************************************************************
-- $Workfile:   MDS014_TraveNewsDataSources.sql  $
-- AUTHOR       : John Frank
-- DATE CREATED : 22/01/2009
-- REVISION     : $Revision:   1.2  $
-- DESCRIPTION  : Update script for TravelNewsDataSources
-- **********************************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS014_TraveNewsDataSources.sql-arc  $
--
--   Rev 1.3   Sep 16 2013 13:53:00   cpietruski
--Updated as per list supplied by inrix via MDS
--
--   Rev 1.2   Feb 22 2010 17:04:14   rhopkins
--New trusted source for Travel News
--Resolution for 5409: New MDS trusted sources file
--
--   Rev 1.1   Jan 22 2009 16:05:02   jfrank
--Updated as requested by MDS
--
--   Rev 1.0   Sep 16 2008 14:40:02   jroberts
--Initial revision.
--

USE TransientPortal
GO
DELETE dbo.TravelNewsDataSources
GO
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (2)', 'Government Agency (2)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (5)', 'Eyewitness (5)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (6)', 'Eyewitness (6)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (7)', 'Eyewitness (7)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (10)', 'Operator (10)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (11)', 'Operator (11)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Emergency Services (12)', 'Emergency Services (12)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Local Authority (13)', 'Local Authority (13)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (14)', 'Operator (14)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Camera (15)', 'Camera (15)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Camera (16)', 'Camera (16)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Emergency Services (17)', 'Emergency Services (17)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Local Authority (19)', 'Local Authority (19)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Utility Company (22)', 'Utility Company (22)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (23)', 'Government Agency (23)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (25)', 'Operator (25)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Emergency Services (26)', 'Emergency Services (26)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (27)', 'Government Agency (27)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (28)', 'Eyewitness (28)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Local Authority (31)', 'Local Authority (31)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (34)', 'Government Agency (34)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (36)', 'Operator (36)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (38)', 'Eyewitness (38)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (39)', 'Eyewitness (39)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Emergency Services (40)', 'Emergency Services (40)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (45)', 'Operator (45)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (47)', 'Government Agency (47)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (50)', 'Eyewitness (50)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (57)', 'Operator (57)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (58)', 'Operator (58)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (59)', 'Operator (59)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (60)', 'Operator (60)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (62)', 'Operator (62)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Utility Company (63)', 'Utility Company (63)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (64)', 'Eyewitness (64)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (66)', 'Operator (66)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Local Authority (67)', 'Local Authority (67)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (68)', 'Eyewitness (68)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Sensor (69)', 'Sensor (69)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Local Authority (70)', 'Local Authority (70)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (71)', 'Government Agency (71)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (72)', 'Government Agency (72)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (73)', 'Government Agency (73)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (74)', 'Government Agency (74)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (75)', 'Government Agency (75)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Camera (76)', 'Camera (76)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Sensor (77)', 'Sensor (77)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (78)', 'Eyewitness (78)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (79)', 'Eyewitness (79)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Sensor (80)', 'Sensor (80)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (81)', 'Eyewitness (81)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (82)', 'Government Agency (82)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (83)', 'Eyewitness (83)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (84)', 'Eyewitness (84)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (86)', 'Government Agency (86)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (88)', 'Operator (88)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Local Authority (90)', 'Local Authority (90)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (91)', 'Eyewitness (91)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Emergency Services (92)', 'Emergency Services (92)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (93)', 'Government Agency (93)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Local Authority (94)', 'Local Authority (94)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (95)', 'Government Agency (95)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (96)', 'Government Agency (96)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (97)', 'Government Agency (97)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (100)', 'Eyewitness (100)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (102)', 'Government Agency (102)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (103)', 'Government Agency (103)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (105)', 'Government Agency (105)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (106)', 'Eyewitness (106)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Government Agency (107)', 'Government Agency (107)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Eyewitness (108)', 'Eyewitness (108)', 1)
INSERT INTO dbo.TravelNewsDataSources (DataSourceId, DataSourceName, Trusted) VALUES ('Operator (109)', 'Operator (109)', 1)



----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.3  $',8),7)

IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 014 and VersionNumber = @@value)
BEGIN
UPDATE dbo.MDSChangeCatalogue
SET
ChangeDate = getdate(),
Summary = 'MDS014_TravelNewsDataSources.sql'
WHERE ScriptNumber = 014 AND VersionNumber = @@value
END
ELSE
BEGIN
INSERT INTO dbo.MDSChangeCatalogue
(
ScriptNumber,
VersionNumber,
Summary
)
VALUES
(
014,
@@value,
'MDS014_TravelNewsDataSources.sql'
)
END
GO