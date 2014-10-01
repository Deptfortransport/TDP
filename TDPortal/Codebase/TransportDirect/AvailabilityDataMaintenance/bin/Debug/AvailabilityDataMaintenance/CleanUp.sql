-- *********************************************** 
-- NAME			: CleanUp.sql
-- AUTHOR		: James Broome
-- DATE CREATED	: 26/01/2005
-- DESCRIPTION	: Database clean up script for NUnit testing
-- ************************************************ 
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AvailabilityDataMaintenance/bin/Debug/AvailabilityDataMaintenance/CleanUp.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:18:54   mturner
--Initial revision.

USE ProductAvailability

-- Remove test data

DELETE FROM UnavailableProducts

DELETE FROM ProductProfile

DELETE FROM AvailabilityHistory

DELETE FROM TicketCategory 

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
select Mode, Origin, Destination, TicketCode, OutwardTravelDate, ReturnTravelDate from temp_UnavailableProducts
go

-- drop temp tables
drop table temp_TicketCategory
drop table temp_AvailabilityHistory
drop table temp_ProductProfile
drop table temp_UnavailableProducts
go