-- *********************************************** 
-- NAME			: SetUp.sql
-- AUTHOR		: James Broome
-- DATE CREATED	: 12/01/2005
-- DESCRIPTION	: Database set up script AvailabilityEstimator for NUnit tests
-- ************************************************ 

--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/bin/Debug/AvailabilityEstimator/SetUp.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:36:12   mturner
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
DELETE UnavailableProducts 
WHERE Origin LIKE 'TEST%'

DELETE ProductProfile 
WHERE Mode LIKE 'TEST%'
OR Origin LIKE 'TEST%'

DELETE ProductProfile 
WHERE DayRange = 2000

DELETE AvailabilityHistory 
WHERE TicketCode LIKE 'TEST%'

DELETE TicketCategory
WHERE TicketCode LIKE 'TEST%'

DELETE Calendar
WHERE Date > '01/01/2000'

-- Set up test reference data
INSERT INTO TicketCategory
VALUES ('TEST1', 2)
INSERT INTO TicketCategory
VALUES ('TEST2', 3)
INSERT INTO TicketCategory
VALUES ('TEST3', 4)

INSERT INTO Calendar
VALUES ('01/01/2010', 2)

-- TestAvailabilityEstimator test data
INSERT INTO ProductProfile
VALUES ('Rail', 'TESTGROUP1', 'TESTGROUP1', 2, 2, NULL, 4)
INSERT INTO ProductProfile
VALUES ('Rail', 'TESTGROUP2', 'TESTGROUP2', 2, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('Rail', 'TESTGROUP3', 'TESTGROUP3', 2, 2, NULL, 4)
INSERT INTO ProductProfile
VALUES ('Rail', 'TESTGROUP4', 'TESTGROUP4', 2, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('Rail', 'TESTGROUP5', 'TESTGROUP5', 2, 2, NULL, 2)
INSERT INTO ProductProfile
VALUES ('Rail', 'TESTGROUP6', 'TESTGROUP6', 2, 2, NULL, 4)

-- TestAvailabilityEstimatorDBHelper test data
INSERT INTO ProductProfile
VALUES ('TEST1', 'TEST', 'TEST', 2, 2, NULL, 4)
INSERT INTO ProductProfile
VALUES ('TEST1', 'TEST', 'TEST', 2, 1, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST1', 'TEST', 'TEST', 1, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST1', 'TEST', 'TEST', 1, 1, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST1', 'DEFAULT', 'DEFAULT', 2, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST1', 'DEFAULT', 'DEFAULT', 2, 1, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST1', 'DEFAULT', 'DEFAULT', 1, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST1', 'DEFAULT', 'DEFAULT', 1, 1, NULL, 3)

INSERT INTO ProductProfile
VALUES ('TEST2', 'TEST', 'TEST', 2, 1, NULL, 4)
INSERT INTO ProductProfile
VALUES ('TEST2', 'TEST', 'TEST', 1, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST2', 'TEST', 'TEST', 1, 1, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST2', 'DEFAULT', 'DEFAULT', 2, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST2', 'DEFAULT', 'DEFAULT', 2, 1, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST2', 'DEFAULT', 'DEFAULT', 1, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST2', 'DEFAULT', 'DEFAULT', 1, 1, NULL, 3)

INSERT INTO ProductProfile
VALUES ('TEST3', 'TEST', 'TEST', 1, 2, NULL, 4)
INSERT INTO ProductProfile
VALUES ('TEST3', 'TEST', 'TEST', 1, 1, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST3', 'DEFAULT', 'DEFAULT', 2, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST3', 'DEFAULT', 'DEFAULT', 2, 1, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST3', 'DEFAULT', 'DEFAULT', 1, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST3', 'DEFAULT', 'DEFAULT', 1, 1, NULL, 3)

INSERT INTO ProductProfile
VALUES ('TEST4', 'TEST', 'TEST', 1, 1, NULL, 4)
INSERT INTO ProductProfile
VALUES ('TEST4', 'DEFAULT', 'DEFAULT', 2, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST4', 'DEFAULT', 'DEFAULT', 2, 1, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST4', 'DEFAULT', 'DEFAULT', 1, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST4', 'DEFAULT', 'DEFAULT', 1, 1, NULL, 3)

INSERT INTO ProductProfile
VALUES ('TEST5', 'DEFAULT', 'DEFAULT', 2, 2, NULL, 4)
INSERT INTO ProductProfile
VALUES ('TEST5', 'DEFAULT', 'DEFAULT', 2, 1, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST5', 'DEFAULT', 'DEFAULT', 1, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST5', 'DEFAULT', 'DEFAULT', 1, 1, NULL, 3)

INSERT INTO ProductProfile
VALUES ('TEST6', 'DEFAULT', 'DEFAULT', 2, 1, NULL, 4)
INSERT INTO ProductProfile
VALUES ('TEST6', 'DEFAULT', 'DEFAULT', 1, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST6', 'DEFAULT', 'DEFAULT', 1, 1, NULL, 3)

INSERT INTO ProductProfile
VALUES ('TEST7', 'DEFAULT', 'DEFAULT', 2, 1, NULL, 4)
INSERT INTO ProductProfile
VALUES ('TEST7', 'DEFAULT', 'DEFAULT', 1, 1, NULL, 3)

INSERT INTO ProductProfile
VALUES ('TEST8', 'DEFAULT', 'DEFAULT', 1, 1, NULL, 4)

INSERT INTO ProductProfile
VALUES ('TEST9', 'DEFAULT', 'DEFAULT', 2, 2, NULL, 4)

INSERT INTO ProductProfile
VALUES ('TEST10', 'DEFAULT', 'DEFAULT', 1, 2, NULL, 4)

INSERT INTO ProductProfile
VALUES ('TEST11', 'DEFAULT', 'DEFAULT', 2, 1, NULL, 4)

INSERT INTO ProductProfile
VALUES ('TEST12', 'TEST', 'TEST', 2, 2, NULL, 3)
INSERT INTO ProductProfile
VALUES ('TEST12', 'TEST', 'TEST', 2, 2, 0, 3)
INSERT INTO ProductProfile
VALUES ('TEST12', 'TEST', 'TEST', 2, 2, 100, 4)
INSERT INTO ProductProfile
VALUES ('TEST12', 'TEST', 'TEST', 2, 2, 100000, 3)

INSERT INTO ProductProfile
VALUES ('TEST13', 'TEST', 'TEST', 2, 2, NULL, 4)
INSERT INTO ProductProfile
VALUES ('TEST13', 'TEST', 'TEST', 2, 2, 100000, 3)

GO