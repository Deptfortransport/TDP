-- ***********************************************
-- AUTHOR      	: Rich Broddle
-- Title        : DUP2101_Add_MX_DATA_TRUST_EES_Partner
-- DESCRIPTION 	: Adds MX Data Trust to EES
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

declare @partnerid int
declare @hostname varchar(25)
declare @Partnername varchar(25)
declare @Channel varchar(25)
declare @PartnerPassword varchar(100)


set @partnerid = 149
set @hostname = 'MXData'
set @Partnername = 'MXData'
set @Channel = 'EES' 

-- "EESTID","Desc"
-- Password: MXD4t4#
-- PROD: crypt@NtHDsTDZobyfyA6YqiNNQA==
-- DEV:  crypt@nZ1X7BDMBd169fbbnXLL0A==
--
-- PROD
-- set @PartnerPassword = 'NtHDsTDZobyfyA6YqiNNQA=='
-- DEV
 set @PartnerPassword = 'nZ1X7BDMBd169fbbnXLL0A=='

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
-- insert into Partner values (149, 'MXData', 'MXData', 'EES',null)
-- go




----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2101
SET @ScriptDesc = 'DUP2101_Add_MX_DATA_TRUST_EES_Partner'

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







