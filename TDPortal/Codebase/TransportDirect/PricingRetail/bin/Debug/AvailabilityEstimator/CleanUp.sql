-- *********************************************** 
-- NAME			: CleanUp.sql
-- AUTHOR		: James Broome
-- DATE CREATED	: 12/01/2005
-- DESCRIPTION	: Database clean up script AvailabilityEstimator for NUnit tests
-- ************************************************ 

--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/bin/Debug/AvailabilityEstimator/CleanUp.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:36:12   mturner
--Initial revision.

USE ProductAvailability

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

GO

-- Insert original data

insert into TicketCategory 
select TicketCode, CategoryId from temp_TicketCategory
go

insert into AvailabilityHistory 
select RequestDatetime, Mode, Origin, Destination, TravelDatetime, TicketCode, Available from temp_AvailabilityHistory
go

insert into ProductProfile 
select Mode, Origin, Destination, CategoryId, DayTypeId, DayRange, ProbabilityId from temp_ProductProfile
go

insert into UnavailableProducts 
select Mode, Origin, Destination, TravelDate, TicketCode from temp_UnavailableProducts
go

-- drop temp tables
drop table temp_TicketCategory
drop table temp_AvailabilityHistory
drop table temp_ProductProfile
drop table temp_UnavailableProducts
go