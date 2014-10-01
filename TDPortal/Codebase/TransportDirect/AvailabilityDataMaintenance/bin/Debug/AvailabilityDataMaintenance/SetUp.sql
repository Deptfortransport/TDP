-- *********************************************** 
-- NAME			: SetUp.sql
-- AUTHOR		: James Broome
-- DATE CREATED	: 26/01/2005
-- DESCRIPTION	: Database set up script for NUnit testing
-- ************************************************ 
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AvailabilityDataMaintenance/bin/Debug/AvailabilityDataMaintenance/SetUp.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:18:54   mturner
--Initial revision.



USE ProductAvailability


-- Delete temp tables if they exist
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[temp_UnavailableProducts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[temp_UnavailableProducts]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[temp_ProductProfile]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[temp_ProductProfile]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[temp_AvailabilityHistory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[temp_AvailabilityHistory]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[temp_TicketCategory]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[temp_TicketCategory]
GO


-- Backup data so we dont lose any data after tests have been run

select * into temp_UnavailableProducts from UnavailableProducts
go
select * into temp_ProductProfile from ProductProfile
go
select * into temp_AvailabilityHistory from AvailabilityHistory
go
select * into temp_TicketCategory from TicketCategory
go


-- Remove any test records that may not have been deleted

DELETE FROM UnavailableProducts 

DELETE FROM ProductProfile

DELETE FROM AvailabilityHistory

DELETE TicketCategory 
WHERE TicketCode = 'TEST'

-- Insert test data

INSERT INTO TicketCategory
VALUES ('TEST', 1)

INSERT INTO AvailabilityHistory
VALUES (GETDATE(), 'TEST', 'TEST', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), GETDATE(), 110))), 'TEST', 1)
INSERT INTO AvailabilityHistory
VALUES (GETDATE(), 'TEST', 'TEST', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()-1), 110))), 'TEST', 1)
INSERT INTO AvailabilityHistory
VALUES (GETDATE(), 'TEST', 'TEST', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()-2), 110))), 'TEST', 1)
INSERT INTO AvailabilityHistory
VALUES (GETDATE(), 'TEST', 'TEST', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()-3), 110))), 'TEST', 1)
INSERT INTO AvailabilityHistory
VALUES (GETDATE(), 'TEST', 'TEST', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()+1), 110))), 'TEST', 1)
INSERT INTO AvailabilityHistory
VALUES (GETDATE(), 'TEST', 'TEST', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()+2), 110))), 'TEST', 1)
INSERT INTO AvailabilityHistory
VALUES (GETDATE(), 'TEST', 'TEST', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()+3), 110))), 'TEST', 1)
INSERT INTO AvailabilityHistory
VALUES (GETDATE(), 'TEST', 'TEST', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()+4), 110))), 'TEST', 1)
INSERT INTO AvailabilityHistory
VALUES (GETDATE(), 'TEST', 'TEST', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()+5), 110))), 'TEST', 1)
INSERT INTO AvailabilityHistory
VALUES (GETDATE(), 'TEST', 'TEST', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()+6), 110))), 'TEST', 1)

INSERT INTO ProductProfile
VALUES ('TEST1', 'TEST1', 'TEST1', 1, 1, 0, 4)

INSERT INTO ProductProfile
VALUES ('TEST2', 'TEST2', 'TEST2', 2, 2, 0, 4)
INSERT INTO ProductProfile
VALUES ('TEST2', 'TEST2', 'TEST2', 2, 1, 0, 3)
INSERT INTO ProductProfile
VALUES ('TEST2', 'TEST2', 'TEST2', 1, 2, 0, 3)
INSERT INTO ProductProfile
VALUES ('TEST2', 'TEST2', 'TEST2', 1, 1, 0, 2)
INSERT INTO ProductProfile
VALUES ('TEST2', 'DEFAULT', 'DEFAULT', 2, 2, 0, 4)
INSERT INTO ProductProfile
VALUES ('TEST2', 'DEFAULT', 'DEFAULT', 2, 1, 0, 4)
INSERT INTO ProductProfile
VALUES ('TEST2', 'DEFAULT', 'DEFAULT', 1, 2, 0, 4)

INSERT INTO UnavailableProducts
VALUES ('TEST1', 'TEST1', 'TEST1', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), GETDATE(), 110))), NULL )
INSERT INTO UnavailableProducts
VALUES ('TEST1', 'TEST1', 'TEST1', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()+1), 110))), NULL)
INSERT INTO UnavailableProducts
VALUES ('TEST1', 'TEST1', 'TEST1', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()+2), 110))), (CONVERT( VARCHAR(10), (GETDATE()+3), 110)))
INSERT INTO UnavailableProducts
VALUES ('TEST1', 'TEST1', 'TEST1', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()+3), 110))), (CONVERT( VARCHAR(10), (GETDATE()+3), 110)))
INSERT INTO UnavailableProducts
VALUES ('TEST1', 'TEST1', 'TEST1', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()+4), 110))), NULL)
INSERT INTO UnavailableProducts
VALUES ('TEST1', 'TEST1', 'TEST1', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()-1), 110))), (CONVERT( VARCHAR(10), (GETDATE()+2), 110)))
INSERT INTO UnavailableProducts
VALUES ('TEST1', 'TEST1', 'TEST1', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()-2), 110))), (CONVERT( VARCHAR(10), (GETDATE()+2), 110)))
INSERT INTO UnavailableProducts
VALUES ('TEST1', 'TEST1', 'TEST1', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()-3), 110))), (CONVERT( VARCHAR(10), (GETDATE()-1), 110)))
INSERT INTO UnavailableProducts
VALUES ('TEST1', 'TEST1', 'TEST1', 'TEST', CONVERT (DATETIME, (CONVERT( VARCHAR(10), (GETDATE()-4), 110))), (CONVERT( VARCHAR(10), GETDATE(), 110)))
GO