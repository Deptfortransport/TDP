use permanentportal
GO

-- Use dummy partner ID 999 for testing so existing data not disturbed


CREATE PROC dbo.SetupDataSetForPartner999 AS

-- Insert data required to test DataServices for a dummy partner
INSERT INTO Partner (PartnerId, HostName, PartnerName, Channel) VALUES (999, 'TestPartner', 'TestPartner', 'TestPartner')
SET IDENTITY_INSERT DataSet ON
INSERT INTO DataSet (DataSetId, DataSet, PartnerId) VALUES (999, 'WalkingSpeedDrop', 999)
SET IDENTITY_INSERT DataSet OFF
INSERT INTO DropDownLists (DataSet, ResourceID, ItemValue, IsSelected, SortOrder, PartnerId) VALUES ('WalkingSpeedDrop', 'Normal', 80, 1, 1, 999)
INSERT INTO DropDownLists (DataSet, ResourceID, ItemValue, IsSelected, SortOrder, PartnerId) VALUES ('WalkingSpeedDrop', 'SnailPace', 20, 0, 2, 999)
INSERT INTO DropDownLists (DataSet, ResourceID, ItemValue, IsSelected, SortOrder, PartnerId) VALUES ('WalkingSpeedDrop', 'Speedy', 150, 0, 3, 999)

GO


-- TidyUp
CREATE PROCEDURE DataSetForPartner999TidyUp
AS
DELETE FROM DropDownLists WHERE PartnerId = 999
DELETE FROM DataSet WHERE PartnerId = 999
DELETE FROM Partner WHERE PartnerId = 999

DROP PROCEDURE SetupDataSetForPartner999
DROP PROCEDURE DataSetForPartner999TidyUp

GO

