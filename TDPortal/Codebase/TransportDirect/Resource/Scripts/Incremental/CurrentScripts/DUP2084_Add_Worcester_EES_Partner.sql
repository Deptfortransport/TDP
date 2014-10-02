-- ***********************************************
-- AUTHOR      	: Phil Scott
-- Title        : DUP2084_Add_Worcester_EES_Partner
-- DESCRIPTION 	: Adds WorcesterCC to EES
-- SOURCE 	: TDP Apps Support
-- ************************************************

-- ************************************************
-- ******************* IMPORTANT ******************
-- Please update the @PartnerPassword value to be the
-- PRODUCTION encrupted password key, this involves
-- un-commenting the appropriate line below.
-- ************************************************

use permanentportal
go
-- "EESTID","Desc"
-- Password: W0rc35t3r# 
-- PROD: crypt@ZAGLMQOKOWAPvAxM/rDy298LdyrIshh2EBQTHy9V/Q8=
-- DEV:  crypt@6qURPb8dNgS3EuTEGZe1Tj0HsGJONSgPNEPuJQ1hcqo=


declare @partnerid int
declare @hostname varchar(25)
declare @Partnername varchar(25)
declare @Channel varchar(25)
declare @PartnerPassword varchar(100)


set @partnerid = 146
set @hostname = 'Worcester'
set @Partnername = 'Worcester'
set @Channel = 'EES' 

-- DEV
set @PartnerPassword = '6qURPb8dNgS3EuTEGZe1Tj0HsGJONSgPNEPuJQ1hcqo='
-- PROD
-- set @PartnerPassword = 'ZAGLMQOKOWAPvAxM/rDy298LdyrIshh2EBQTHy9V/Q8='

IF EXISTS (SELECT * FROM [PartnerAllowedServices] WHERE PartnerId = @partnerid)
BEGIN
	DELETE FROM [PartnerAllowedServices]
	WHERE PartnerId = @partnerid
END

IF EXISTS (SELECT * FROM [Partner] WHERE PartnerId = @partnerid)
BEGIN
	DELETE FROM [Partner]
	WHERE PartnerId = @partnerid
END

insert into Partner values (@partnerid, @hostname, @Partnername, @Channel, @PartnerPassword)

-- 1,CodeHandler_V1
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,1)
-- 2,DepartureBoard_V1
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,2)
-- 3,FindNearest_V1
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,3)
-- 4,JourneyPlanner_V1
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,4)
-- 5,JourneyPlannerSynchronous_V1
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,5)
-- 6,TaxiInformation_V1
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,6)
-- 7,TestWebService
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,7)
-- 8,TravelNews_V1
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,8)
-- 9,OpenJourneyPlanner_V1
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,9)
-- 10,
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,10)
-- 11,
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,11)
-- 12,
	insert into PartnerAllowedServices (partnerid, EESTID )
	values (@partnerid,12)

go

-- Update M01 database as well
-- Use Reporting
-- insert into Partner values (146, 'Worcester', 'Worcester', 'EES',null)
-- go




----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2084
SET @ScriptDesc = 'DUP2084_Add_Worcester_EES_Partner'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc)
  END
GO








