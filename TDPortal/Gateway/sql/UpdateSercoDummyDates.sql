-- *******************************************************
-- NAME 	: SercoDummyCurrentDates.sql
-- DESCRIPTION 	: updates the dates of reportedDateTime, 
-- startDatetime and lastModifiedDatatime in the TransientPortal
-- database to be the current date and time. This is to replace 
-- the dummy dates set in the SercoDummy.xml file
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/build/DataGatewayApps/Gateway/sql/UpdateSercoDummyDates.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 11:53:46   mturner
--Initial revision.

USE [TransientPortal]

GO

UPDATE TravelNews1 
SET TravelNews1.ReportedDateTime = CURRENT_TIMESTAMP,
TravelNews1.StartDateTime = CURRENT_TIMESTAMP, 
TravelNews1.LastModifiedDateTime = CURRENT_TIMESTAMP

UPDATE TravelNews2
SET TravelNews2.ReportedDateTime = CURRENT_TIMESTAMP,
TravelNews2.StartDateTime = CURRENT_TIMESTAMP, 
TravelNews2.LastModifiedDateTime = CURRENT_TIMESTAMP


