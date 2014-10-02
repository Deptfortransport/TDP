-- ***********************************************
-- AUTHOR      	: Phil Scott
-- Title        : DUP2103_Add_Norfolk_EES_Partner
-- DESCRIPTION 	: Adds Norfolk to EES
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



-- Password:N0rfolk#
-- PROD: crypt@apqw2niZ5k35S0hoZqx1Fbh1w3ZAollXgZ1PfNkMjTs=
-- DEV:  crypt@B4mdZPFTfT9bp41RbYWoWIZ+37h35roMj4Kt+QwgQqg=

declare @partnerid int
declare @hostname varchar(25)
declare @Partnername varchar(25)
declare @Channel varchar(25)
declare @PartnerPassword varchar(100)


set @partnerid = 150
set @hostname = 'Norfolk'
set @Partnername = 'Norfolk'
set @Channel = 'EES' 

-- DEV
set @PartnerPassword = 'B4mdZPFTfT9bp41RbYWoWIZ+37h35roMj4Kt+QwgQqg='
-- PROD
-- set @PartnerPassword = 'apqw2niZ5k35S0hoZqx1Fbh1w3ZAollXgZ1PfNkMjTs='

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



-- Password:S0uth3nd#
-- PROD: crypt@qvcpcLE/Vk8WyjZOiLIAvoO74bbO9BU0oOMSIg6h3EA=
-- DEV:  crypt@9YFXTug8ymKdLgh9vbnjgTe9v8HEvPXgjM03jBJzOT0=

set @partnerid = 151
set @hostname = 'Southend'
set @Partnername = 'Southend'
set @Channel = 'EES'


-- DEV
set @PartnerPassword = '9YFXTug8ymKdLgh9vbnjgTe9v8HEvPXgjM03jBJzOT0='
-- PROD
-- set @PartnerPassword = 'qvcpcLE/Vk8WyjZOiLIAvoO74bbO9BU0oOMSIg6h3EA='

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
-- insert into Partner values (150, 'Norfolk', 'Norfolk', 'EES',null)
-- insert into Partner values (151, 'Southend', 'Southend', 'EES',null)
-- go




----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2103
SET @ScriptDesc = 'DUP2103_Add_Norfolk_EES_Partner'

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








