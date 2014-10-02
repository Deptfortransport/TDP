-- ***********************************************
-- AUTHOR      	: Charles Roberts
-- NAME 	: MDS004_AvailabilityMatrix_Update.sql
-- DESCRIPTION 	: Updates AvailabilityMatrix data
-- SOURCE 	: TDP Development Team
-- Additional Steps Required: After running this script perform a 
--                            Data Gateway import for the latest 'Availability Product Profiles’
--                            feed (WNQ457) to update the Availability Matrix data.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS004_AvailabilityMatrix_Update.sql-arc  $
--
--	 Rev 1.14 Apr 09 2014 CPietruski
--Updates in respect of the National Rail fares revision on 18 May 2014 from MDS... 
--
--	 Rev 1.13 Nov 28 2013 CPietruski
--Updates in respect of the National Rail fares revision on 02 January 2014 from MDS... 
--
--	 Rev 1.12 Aug 20 2013 RBroddle 
--Updates in respect of the National Rail fares revision on 08 September 2013 from MDS... 
--ALSO MOVED TICKETCATEGORY UPDATES FROM MDS007 INTO THIS SCRIPT TO FIX FK_CATEGORY_TICKETCATEGORY ISSUES WE HAVE ALWAYS HAD WITH THIS
--
--   Rev 1.11   Jan 07 2013 11:29:38   RBroddle
--Amended to make it more obvious that "Availability Product Profiles" feed (WNQ457) needs to be ran after running this script in to update the Availability Matrix data !!
--Resolution for 5880: Latest Rail data update from MDS
--
--   Rev 1.10   Sep 02 2011 14:26:30   nrankin
--Amended to make it more obvious that "Availability Product Profiles" feed (WNQ457) needs to be ran after running this script in to update the Availability Matrix data !!
--
--   Rev 1.9   Aug 24 2011 15:53:10   nrankin
--Latest Updates from MDS - Ticket Route Restrictions / Categorised Hashes
--Resolution for 5730: Ticket Route Restrictions / Categorised Hashes (MDS)
--
--   Rev 1.8   Dec 08 2010 14:44:18   rbroddle
--Latest updates from MDS
--
--   Rev 1.7   Aug 26 2010 11:25:08   rbroddle
--updates  from Chris P - changes 3217624 and 3217931
--
--   Rev 1.6   Apr 16 2010 16:17:16   rbroddle
--Removed TicketCategory updates as this is done in MDS007
--
--   Rev 1.5   Aug 28 2009 11:05:08   nrankin
--Updated with latest TicketCategory and Calendar data from MDS.
--
--   Rev 1.4   May 01 2009 12:32:18   nrankin
--AMENDED - Updated TicketCategory and Category - May 1st 09
--
--   Rev 1.3   May 01 2009 12:22:46   nrankin
--Updated TicketCategory and Category - May 1st 09
--
--   Rev 1.2   Jan 07 2009 17:57:12   jroberts
--Added stored procedures 
--INFAddUpdateTicketCategory
--and
--INFAddUpdateCalendar
--
--   Rev 1.1   Jan 07 2009 16:27:10   mmodi
--Updated from production
--
--   Rev 1.0   Nov 08 2007 12:42:26   mturner
--Initial revision.
--
--   Rev 1.2   Feb 23 2006 15:15:48   DMistry
--Reinstated delete of ProductProfile and UnavailableProduct. Changed truncate statements to delete.
--
--   Rev 1.1   Feb 21 2006 17:17:28   croberts
--Removed truncation of ProductProfile table and UnavailableProduct.
--
--   Rev 1.0   Feb 21 2006 14:15:38   croberts
--Initial revision.

use ProductAvailability


-- **********************************************************************
-- PROCEDURE INFAddUpdateCalendar
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'INFAddUpdateCalendar'
IF NOT EXISTS(SELECT 1 
                FROM INFORMATION_SCHEMA.ROUTINES
               WHERE ROUTINE_NAME = @ObjectName
                 AND ROUTINE_TYPE = N'PROCEDURE')
BEGIN
    EXEC ('CREATE PROCEDURE [dbo].INFAddUpdateCalendar AS BEGIN SELECT 1 END')

    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Used By', @value=N'TDP.Infrastructure' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName

END
GO


GO
-- **********************************************************************
-- PROCEDURE INFAddUpdateCalendar
-- Description: 
-- 
-- **********************************************************************
ALTER PROCEDURE dbo.INFAddUpdateCalendar
(
    @Date       datetime,
    @DayTypeId  int
) AS
BEGIN
    SET NOCOUNT ON
    IF EXISTS(SELECT 1
                FROM dbo.Calendar
               WHERE [Date] = @Date)
        BEGIN
          UPDATE Date
             SET DayTypeId = @DayTypeId
           WHERE [Date] = @Date
           PRINT 'Updated Calendar date ' + Convert(varchar, @Date, 103) + ' DayTypeId  ' + Cast(@DayTypeId as varchar(10))

        END
    ELSE
        BEGIN
            INSERT INTO dbo.Calendar
            (
                [Date],
                DayTypeId
            )
            VALUES
            (
                @Date,
                @DayTypeId
            )
            PRINT 'Inserted  Calendar date ' + Convert(varchar, @Date, 103) + ' DayTypeId  ' + Cast(@DayTypeId as varchar(10))
           
        END
    --END IF    
END
GO


-- **********************************************************************
-- PROCEDURE INFAddUpdateTicketCategory
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'INFAddUpdateTicketCategory'
IF NOT EXISTS(SELECT 1 
                FROM INFORMATION_SCHEMA.ROUTINES
               WHERE ROUTINE_NAME = @ObjectName
                 AND ROUTINE_TYPE = N'PROCEDURE')
BEGIN
    EXEC ('CREATE PROCEDURE [dbo].INFAddUpdateTicketCategory AS BEGIN SELECT 1 END')

    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Used By', @value=N'TDP.Infrastructure' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName

END
GO


GO
-- **********************************************************************
-- PROCEDURE INFAddUpdateTicketCategory
-- Description: 
-- 
-- **********************************************************************
ALTER PROCEDURE dbo.INFAddUpdateTicketCategory
(
    @TicketCode    varchar(10),
    @CategoryId    int
) AS
BEGIN
    SET NOCOUNT ON
    IF EXISTS(SELECT 1
                FROM dbo.TicketCategory
               WHERE TicketCode = @TicketCode)
        BEGIN
          UPDATE dbo.TicketCategory
             SET CategoryId = @CategoryId
           WHERE TicketCode = @TicketCode
           PRINT 'Updated TicketCategory TicketCode ' + @TicketCode
        END
    ELSE
        BEGIN
            INSERT INTO dbo.TicketCategory
            (
                TicketCode,
                CategoryId
            )
            VALUES
            (
                @TicketCode,
                @CategoryId
            )
            PRINT 'Inserted TicketCategory TicketCode ' + @TicketCode
        END
    --END IF    
END
GO



DELETE FROM UnavailableProducts
GO

DELETE FROM ProductProfile
GO

DELETE FROM TicketCategory
GO

DELETE FROM Category
GO

DELETE FROM Calendar
GO

DELETE FROM DayType
GO


DBCC CHECKIDENT ('dbo.UnavailableProducts', RESEED, 0)
DBCC CHECKIDENT ('dbo.category', RESEED, 0)
DBCC CHECKIDENT ('dbo.ProductProfile', RESEED, 0)
DBCC CHECKIDENT ('dbo.DayType', RESEED, 0)

set identity_insert Category on
insert Category(CategoryId,CategoryDesc) values(1,'DEFAULT')
insert Category(CategoryId,CategoryDesc) values(2,'Advance14')
insert Category(CategoryId,CategoryDesc) values(3,'Advance3')
insert Category(CategoryId,CategoryDesc) values(4,'Advance7')
insert Category(CategoryId,CategoryDesc) values(5,'AdvanceCheapest')
insert Category(CategoryId,CategoryDesc) values(6,'AdvanceGeneral')
insert Category(CategoryId,CategoryDesc) values(7,'AdvanceMedium')
insert Category(CategoryId,CategoryDesc) values(8,'APEX')
insert Category(CategoryId,CategoryDesc) values(9,'WalkOn')
insert Category(CategoryId,CategoryDesc) values(10,'1stAdvCheapest')
insert Category(CategoryId,CategoryDesc) values(11,'1stAdvGeneral')
insert Category(CategoryId,CategoryDesc) values(12,'1stAdvMedium')
insert Category(CategoryId,CategoryDesc) values(13,'1stAdvBusiness')
insert Category(CategoryId,CategoryDesc) values(14,'StdAdvBusiness')
set identity_insert Category off
GO


set identity_insert DayType on
insert DayType(DayTypeId,DayTypeDesc) values(1,'DEFAULT')
insert DayType(DayTypeId,DayTypeDesc) values(2,'Weekday')
insert DayType(DayTypeId,DayTypeDesc) values(3,'WE')
insert DayType(DayTypeId,DayTypeDesc) values(4,'BH')
insert DayType(DayTypeId,DayTypeDesc) values(5,'D23')
insert DayType(DayTypeId,DayTypeDesc) values(6,'D25')
insert DayType(DayTypeId,DayTypeDesc) values(7,'D26')
set identity_insert DayType off
GO


exec dbo.INFAddUpdateTicketCategory 'XP2',9
exec dbo.INFAddUpdateTicketCategory 'XP1',9
exec dbo.INFAddUpdateTicketCategory 'XFR',11
exec dbo.INFAddUpdateTicketCategory 'XF',11
exec dbo.INFAddUpdateTicketCategory 'XD2',13
exec dbo.INFAddUpdateTicketCategory 'XD1',13
exec dbo.INFAddUpdateTicketCategory 'XC2',6
exec dbo.INFAddUpdateTicketCategory 'XC1',6
exec dbo.INFAddUpdateTicketCategory 'XB2',6
exec dbo.INFAddUpdateTicketCategory 'XB1',6
exec dbo.INFAddUpdateTicketCategory 'WKR',9
exec dbo.INFAddUpdateTicketCategory 'WHS',6
exec dbo.INFAddUpdateTicketCategory 'WGS',6
exec dbo.INFAddUpdateTicketCategory 'WFS',6
exec dbo.INFAddUpdateTicketCategory 'WFR',11
exec dbo.INFAddUpdateTicketCategory 'WDS',5
exec dbo.INFAddUpdateTicketCategory 'WCS',5
exec dbo.INFAddUpdateTicketCategory 'WBS',7
exec dbo.INFAddUpdateTicketCategory 'WAS',7
exec dbo.INFAddUpdateTicketCategory 'VCS',5
exec dbo.INFAddUpdateTicketCategory 'VBS',7
exec dbo.INFAddUpdateTicketCategory 'VBR',9
exec dbo.INFAddUpdateTicketCategory 'VAS',7
exec dbo.INFAddUpdateTicketCategory 'VA2',6
exec dbo.INFAddUpdateTicketCategory 'VA1',6
exec dbo.INFAddUpdateTicketCategory 'TB2',7
exec dbo.INFAddUpdateTicketCategory 'TB1',12
exec dbo.INFAddUpdateTicketCategory 'TA2',6
exec dbo.INFAddUpdateTicketCategory 'TA1',11
exec dbo.INFAddUpdateTicketCategory 'SVS',9
exec dbo.INFAddUpdateTicketCategory 'SVR',9
exec dbo.INFAddUpdateTicketCategory 'SSS',9
exec dbo.INFAddUpdateTicketCategory 'SSR',9
exec dbo.INFAddUpdateTicketCategory 'SPG',9
exec dbo.INFAddUpdateTicketCategory 'SOS',9
exec dbo.INFAddUpdateTicketCategory 'SOR',9
exec dbo.INFAddUpdateTicketCategory 'SES',6
exec dbo.INFAddUpdateTicketCategory 'SER',6
exec dbo.INFAddUpdateTicketCategory 'SDS',9
exec dbo.INFAddUpdateTicketCategory 'SDR',9
exec dbo.INFAddUpdateTicketCategory 'SAS',6
exec dbo.INFAddUpdateTicketCategory 'SAR',5
exec dbo.INFAddUpdateTicketCategory 'SAL',5
exec dbo.INFAddUpdateTicketCategory 'SAK',7
exec dbo.INFAddUpdateTicketCategory 'SAJ',6
exec dbo.INFAddUpdateTicketCategory 'SAI',5
exec dbo.INFAddUpdateTicketCategory 'SAH',7
exec dbo.INFAddUpdateTicketCategory 'SAG',6
exec dbo.INFAddUpdateTicketCategory 'SAD',6
exec dbo.INFAddUpdateTicketCategory 'SAC',6
exec dbo.INFAddUpdateTicketCategory 'OS4',6
exec dbo.INFAddUpdateTicketCategory 'OS3',6
exec dbo.INFAddUpdateTicketCategory 'OS2',7
exec dbo.INFAddUpdateTicketCategory 'OS1',5
exec dbo.INFAddUpdateTicketCategory 'OPS',9
exec dbo.INFAddUpdateTicketCategory 'OPR',9
exec dbo.INFAddUpdateTicketCategory 'OGS',11
exec dbo.INFAddUpdateTicketCategory 'OF4',11
exec dbo.INFAddUpdateTicketCategory 'OF3',11
exec dbo.INFAddUpdateTicketCategory 'OF2',12
exec dbo.INFAddUpdateTicketCategory 'OF1',10
exec dbo.INFAddUpdateTicketCategory 'OES',11
exec dbo.INFAddUpdateTicketCategory 'ODS',12
exec dbo.INFAddUpdateTicketCategory 'OCS',10
exec dbo.INFAddUpdateTicketCategory 'OBS',10
exec dbo.INFAddUpdateTicketCategory 'NWD',6
exec dbo.INFAddUpdateTicketCategory 'NAR',9
exec dbo.INFAddUpdateTicketCategory 'MXR',4
exec dbo.INFAddUpdateTicketCategory 'MWA',6
exec dbo.INFAddUpdateTicketCategory 'LNO',9
exec dbo.INFAddUpdateTicketCategory 'LFR',11
exec dbo.INFAddUpdateTicketCategory 'LEX',6
exec dbo.INFAddUpdateTicketCategory 'HTT',11
exec dbo.INFAddUpdateTicketCategory 'HTS',5
exec dbo.INFAddUpdateTicketCategory 'HTA',7
exec dbo.INFAddUpdateTicketCategory 'HT4',6
exec dbo.INFAddUpdateTicketCategory 'HT2',6
exec dbo.INFAddUpdateTicketCategory 'HT1',12
exec dbo.INFAddUpdateTicketCategory 'GFS',9
exec dbo.INFAddUpdateTicketCategory 'GFR',9
exec dbo.INFAddUpdateTicketCategory 'GE1',9
exec dbo.INFAddUpdateTicketCategory 'FXS',8
exec dbo.INFAddUpdateTicketCategory 'FXR',8
exec dbo.INFAddUpdateTicketCategory 'FSS',9
exec dbo.INFAddUpdateTicketCategory 'FSR',9
exec dbo.INFAddUpdateTicketCategory 'FPP',9
exec dbo.INFAddUpdateTicketCategory 'FOS',9
exec dbo.INFAddUpdateTicketCategory 'FOR',9
exec dbo.INFAddUpdateTicketCategory 'FFS',9
exec dbo.INFAddUpdateTicketCategory 'FFS',9
exec dbo.INFAddUpdateTicketCategory 'FDS',9
exec dbo.INFAddUpdateTicketCategory 'FDR',9
exec dbo.INFAddUpdateTicketCategory 'FCS',10
exec dbo.INFAddUpdateTicketCategory 'FCR',9
exec dbo.INFAddUpdateTicketCategory 'FBS',12
exec dbo.INFAddUpdateTicketCategory 'FAS',11
exec dbo.INFAddUpdateTicketCategory 'FAR',11
exec dbo.INFAddUpdateTicketCategory 'DXR',6
exec dbo.INFAddUpdateTicketCategory 'DHS',11
exec dbo.INFAddUpdateTicketCategory 'DHS',11
exec dbo.INFAddUpdateTicketCategory 'DGS',11
exec dbo.INFAddUpdateTicketCategory 'DFS',11
exec dbo.INFAddUpdateTicketCategory 'DES',10
exec dbo.INFAddUpdateTicketCategory 'DDS',10
exec dbo.INFAddUpdateTicketCategory 'DCS',10
exec dbo.INFAddUpdateTicketCategory 'DBS',12
exec dbo.INFAddUpdateTicketCategory 'DAS',12
exec dbo.INFAddUpdateTicketCategory 'CDS',9
exec dbo.INFAddUpdateTicketCategory 'CDR',9
exec dbo.INFAddUpdateTicketCategory 'CBP',6
exec dbo.INFAddUpdateTicketCategory 'C7S',5
exec dbo.INFAddUpdateTicketCategory 'C7R',4
exec dbo.INFAddUpdateTicketCategory 'C3S',7
exec dbo.INFAddUpdateTicketCategory 'C1S',6
exec dbo.INFAddUpdateTicketCategory 'C1B',6
exec dbo.INFAddUpdateTicketCategory 'BVR',9
exec dbo.INFAddUpdateTicketCategory 'BUS',6
exec dbo.INFAddUpdateTicketCategory 'BTS',7
exec dbo.INFAddUpdateTicketCategory 'BSS',5
exec dbo.INFAddUpdateTicketCategory 'BRS',5
exec dbo.INFAddUpdateTicketCategory 'BGO',11
exec dbo.INFAddUpdateTicketCategory 'BFS',9
exec dbo.INFAddUpdateTicketCategory 'BFR',9
exec dbo.INFAddUpdateTicketCategory 'BFO',11
exec dbo.INFAddUpdateTicketCategory 'BFA',13
exec dbo.INFAddUpdateTicketCategory 'BCO',7
exec dbo.INFAddUpdateTicketCategory 'BBO',6
exec dbo.INFAddUpdateTicketCategory 'BAO',6
exec dbo.INFAddUpdateTicketCategory 'AXS',8
exec dbo.INFAddUpdateTicketCategory 'AXR',8
exec dbo.INFAddUpdateTicketCategory 'AVR',6
exec dbo.INFAddUpdateTicketCategory 'ASV',6
exec dbo.INFAddUpdateTicketCategory '2IS',6
exec dbo.INFAddUpdateTicketCategory '2IC',5
exec dbo.INFAddUpdateTicketCategory '2HS',6
exec dbo.INFAddUpdateTicketCategory '2HC',6
exec dbo.INFAddUpdateTicketCategory '2GS',6
exec dbo.INFAddUpdateTicketCategory '2GC',6
exec dbo.INFAddUpdateTicketCategory '2FS',7
exec dbo.INFAddUpdateTicketCategory '2FC',6
exec dbo.INFAddUpdateTicketCategory '2ES',7
exec dbo.INFAddUpdateTicketCategory '2EC',6
exec dbo.INFAddUpdateTicketCategory '2DS',5
exec dbo.INFAddUpdateTicketCategory '2DC',6
exec dbo.INFAddUpdateTicketCategory '2CS',5
exec dbo.INFAddUpdateTicketCategory '2CC',6
exec dbo.INFAddUpdateTicketCategory '2BC',6
exec dbo.INFAddUpdateTicketCategory '2AC',6
exec dbo.INFAddUpdateTicketCategory '1ES',11
exec dbo.INFAddUpdateTicketCategory '1EC',11
exec dbo.INFAddUpdateTicketCategory '1DS',11
exec dbo.INFAddUpdateTicketCategory '1DC',11
exec dbo.INFAddUpdateTicketCategory '1CS',12
exec dbo.INFAddUpdateTicketCategory '1CC',11
exec dbo.INFAddUpdateTicketCategory '1BS',10
exec dbo.INFAddUpdateTicketCategory '1BC',11
exec dbo.INFAddUpdateTicketCategory '1AS',10
exec dbo.INFAddUpdateTicketCategory '1AC',11
exec dbo.INFAddUpdateTicketCategory 'F1V',11
exec dbo.INFAddUpdateTicketCategory 'F3V',12
exec dbo.INFAddUpdateTicketCategory 'F7V',10
exec dbo.INFAddUpdateTicketCategory 'GDS',9
exec dbo.INFAddUpdateTicketCategory 'GDR',9
exec dbo.INFAddUpdateTicketCategory 'PDR',9
exec dbo.INFAddUpdateTicketCategory 'PDS',9
exec dbo.INFAddUpdateTicketCategory 'WES',5
exec dbo.INFAddUpdateTicketCategory 'SCO',9
exec dbo.INFAddUpdateTicketCategory 'CPR',9
exec dbo.INFAddUpdateTicketCategory 'PSR',9
exec dbo.INFAddUpdateTicketCategory 'NBA',7
exec dbo.INFAddUpdateTicketCategory 'NCA',6
exec dbo.INFAddUpdateTicketCategory 'NDA',7
exec dbo.INFAddUpdateTicketCategory 'NEA',7
exec dbo.INFAddUpdateTicketCategory 'C1R',9
exec dbo.INFAddUpdateTicketCategory 'C2R',9
exec dbo.INFAddUpdateTicketCategory 'FCD',9
exec dbo.INFAddUpdateTicketCategory 'GCR',7
exec dbo.INFAddUpdateTicketCategory 'GCS',7
exec dbo.INFAddUpdateTicketCategory 'GCT',5
exec dbo.INFAddUpdateTicketCategory 'GCU',5
exec dbo.INFAddUpdateTicketCategory 'GVR',9
exec dbo.INFAddUpdateTicketCategory 'VBT',9
exec dbo.INFAddUpdateTicketCategory 'GUS',9
exec dbo.INFAddUpdateTicketCategory 'GUR',9
exec dbo.INFAddUpdateTicketCategory 'GTS',9
exec dbo.INFAddUpdateTicketCategory 'GTR',9
exec dbo.INFAddUpdateTicketCategory 'SAV',5
exec dbo.INFAddUpdateTicketCategory 'SBV',7
exec dbo.INFAddUpdateTicketCategory 'SCV',6
exec dbo.INFAddUpdateTicketCategory 'BXS',6
exec dbo.INFAddUpdateTicketCategory '1AF',11
exec dbo.INFAddUpdateTicketCategory '1BF',12
exec dbo.INFAddUpdateTicketCategory '1CF',12
exec dbo.INFAddUpdateTicketCategory '1DF',10
exec dbo.INFAddUpdateTicketCategory '1EF',10
exec dbo.INFAddUpdateTicketCategory '2AF',6
exec dbo.INFAddUpdateTicketCategory '2BF',7
exec dbo.INFAddUpdateTicketCategory '2CF',7
exec dbo.INFAddUpdateTicketCategory '2DF',5
exec dbo.INFAddUpdateTicketCategory '2EF',5
exec dbo.INFAddUpdateTicketCategory 'HSS',9
exec dbo.INFAddUpdateTicketCategory 'FA1',6
exec dbo.INFAddUpdateTicketCategory 'DRR',9
exec dbo.INFAddUpdateTicketCategory 'DRI',9
exec dbo.INFAddUpdateTicketCategory '1FS',11
exec dbo.INFAddUpdateTicketCategory '2JS',6
exec dbo.INFAddUpdateTicketCategory 'ECD',9
exec dbo.INFAddUpdateTicketCategory 'MWS',9
exec dbo.INFAddUpdateTicketCategory 'OF5',11
exec dbo.INFAddUpdateTicketCategory 'OF6',11
exec dbo.INFAddUpdateTicketCategory 'OS5',6
exec dbo.INFAddUpdateTicketCategory 'OS6',6
exec dbo.INFAddUpdateTicketCategory 'MAF',11
exec dbo.INFAddUpdateTicketCategory 'MAS',6
exec dbo.INFAddUpdateTicketCategory 'MBF',11
exec dbo.INFAddUpdateTicketCategory 'MBS',6
exec dbo.INFAddUpdateTicketCategory 'MCF',12
exec dbo.INFAddUpdateTicketCategory 'MCS',7
exec dbo.INFAddUpdateTicketCategory 'MDF',12
exec dbo.INFAddUpdateTicketCategory 'MDS',7
exec dbo.INFAddUpdateTicketCategory 'MEF',10
exec dbo.INFAddUpdateTicketCategory 'MES',5
exec dbo.INFAddUpdateTicketCategory 'S1A',5
exec dbo.INFAddUpdateTicketCategory 'S2A',7
exec dbo.INFAddUpdateTicketCategory 'S3A',7
exec dbo.INFAddUpdateTicketCategory 'S4A',6
exec dbo.INFAddUpdateTicketCategory 'F1A',10
exec dbo.INFAddUpdateTicketCategory 'F2A',12
exec dbo.INFAddUpdateTicketCategory 'F3A',12
exec dbo.INFAddUpdateTicketCategory 'F4A',11
exec dbo.INFAddUpdateTicketCategory 'BPS',5
exec dbo.INFAddUpdateTicketCategory 'BVS',9
exec dbo.INFAddUpdateTicketCategory 'PMR',9
exec dbo.INFAddUpdateTicketCategory 'EGF',9
exec dbo.INFAddUpdateTicketCategory 'CBA',9
exec dbo.INFAddUpdateTicketCategory 'OEO',9
exec dbo.INFAddUpdateTicketCategory 'SOP',9
exec dbo.INFAddUpdateTicketCategory 'FSO',9
exec dbo.INFAddUpdateTicketCategory 'SWS',9
exec dbo.INFAddUpdateTicketCategory 'SRR',9
exec dbo.INFAddUpdateTicketCategory 'VDS',5
exec dbo.INFAddUpdateTicketCategory 'VES',5
exec dbo.INFAddUpdateTicketCategory 'GPR',9
exec dbo.INFAddUpdateTicketCategory 'GOR',9
exec dbo.INFAddUpdateTicketCategory 'OPD',9
exec dbo.INFAddUpdateTicketCategory 'AW1',11
exec dbo.INFAddUpdateTicketCategory 'SPA',11
exec dbo.INFAddUpdateTicketCategory 'SPB',12
exec dbo.INFAddUpdateTicketCategory 'BHO',10
exec dbo.INFAddUpdateTicketCategory 'BYS',6
exec dbo.INFAddUpdateTicketCategory 'BZS',6
exec dbo.INFAddUpdateTicketCategory 'OHS',11
exec dbo.INFAddUpdateTicketCategory 'OJS',11
exec dbo.INFAddUpdateTicketCategory 'AF1',10
exec dbo.INFAddUpdateTicketCategory 'AG2',6
exec dbo.INFAddUpdateTicketCategory 'AH2',6
exec dbo.INFAddUpdateTicketCategory 'AI2',7
exec dbo.INFAddUpdateTicketCategory 'AJ2',5
exec dbo.INFAddUpdateTicketCategory 'AK2',5
exec dbo.INFAddUpdateTicketCategory 'AA1',11
exec dbo.INFAddUpdateTicketCategory 'AB1',11
exec dbo.INFAddUpdateTicketCategory 'AD1',12
exec dbo.INFAddUpdateTicketCategory 'AE1',10
exec dbo.INFAddUpdateTicketCategory 'ADT',9
exec dbo.INFAddUpdateTicketCategory 'ODT',9
exec dbo.INFAddUpdateTicketCategory 'WRE',9
exec dbo.INFAddUpdateTicketCategory 'FDT',9
exec dbo.INFAddUpdateTicketCategory 'STO',9
exec dbo.INFAddUpdateTicketCategory 'WDT',7
exec dbo.INFAddUpdateTicketCategory 'DTP',9
exec dbo.INFAddUpdateTicketCategory 'FTP',9
exec dbo.INFAddUpdateTicketCategory 'OTF',9
exec dbo.INFAddUpdateTicketCategory '2JC',6
exec dbo.INFAddUpdateTicketCategory '1HS',11
exec dbo.INFAddUpdateTicketCategory 'I1H',11
exec dbo.INFAddUpdateTicketCategory 'SPC',6
exec dbo.INFAddUpdateTicketCategory 'SPI',7
exec dbo.INFAddUpdateTicketCategory 'SPE',12
exec dbo.INFAddUpdateTicketCategory 'FSX',9
exec dbo.INFAddUpdateTicketCategory 'FRX',9
exec dbo.INFAddUpdateTicketCategory 'FTX',9
exec dbo.INFAddUpdateTicketCategory 'G1S',9
exec dbo.INFAddUpdateTicketCategory 'G1R',9
exec dbo.INFAddUpdateTicketCategory 'G2S',9
exec dbo.INFAddUpdateTicketCategory 'G2R',9
exec dbo.INFAddUpdateTicketCategory 'LFB',12
exec dbo.INFAddUpdateTicketCategory 'MFS',6
exec dbo.INFAddUpdateTicketCategory 'MGS',6
exec dbo.INFAddUpdateTicketCategory 'MHS',6
exec dbo.INFAddUpdateTicketCategory 'MFF',11
exec dbo.INFAddUpdateTicketCategory 'MGF',11
exec dbo.INFAddUpdateTicketCategory 'EGS',9
exec dbo.INFAddUpdateTicketCategory 'FAV',10
exec dbo.INFAddUpdateTicketCategory 'FBV',12
exec dbo.INFAddUpdateTicketCategory 'FDV',11
exec dbo.INFAddUpdateTicketCategory 'UFA',11
exec dbo.INFAddUpdateTicketCategory 'UFB',11
exec dbo.INFAddUpdateTicketCategory 'UFC',12
exec dbo.INFAddUpdateTicketCategory 'UFD',12
exec dbo.INFAddUpdateTicketCategory 'UFE',12
exec dbo.INFAddUpdateTicketCategory 'UFF',10
exec dbo.INFAddUpdateTicketCategory 'UFG',10
exec dbo.INFAddUpdateTicketCategory 'USA',6
exec dbo.INFAddUpdateTicketCategory 'USB',6
exec dbo.INFAddUpdateTicketCategory 'USC',7
exec dbo.INFAddUpdateTicketCategory 'USD',7
exec dbo.INFAddUpdateTicketCategory 'USE',7
exec dbo.INFAddUpdateTicketCategory 'USF',5
exec dbo.INFAddUpdateTicketCategory 'USG',5
exec dbo.INFAddUpdateTicketCategory '1SO',9
exec dbo.INFAddUpdateTicketCategory 'DJS',11
exec dbo.INFAddUpdateTicketCategory 'DKS',11
exec dbo.INFAddUpdateTicketCategory 'DLS',11
exec dbo.INFAddUpdateTicketCategory 'WJS',7
exec dbo.INFAddUpdateTicketCategory 'WKS',7
exec dbo.INFAddUpdateTicketCategory 'WLS',7
exec dbo.INFAddUpdateTicketCategory 'POP',9
exec dbo.INFAddUpdateTicketCategory 'PAP',9
exec dbo.INFAddUpdateTicketCategory 'SOT',9
exec dbo.INFAddUpdateTicketCategory 'BOS',5
exec dbo.INFAddUpdateTicketCategory 'GC3',11
exec dbo.INFAddUpdateTicketCategory 'GC4',11
exec dbo.INFAddUpdateTicketCategory 'GC5',12
exec dbo.INFAddUpdateTicketCategory 'GC6',12
exec dbo.INFAddUpdateTicketCategory 'XQ1',9
exec dbo.INFAddUpdateTicketCategory 'XQ2',9
exec dbo.INFAddUpdateTicketCategory '1AB',10
exec dbo.INFAddUpdateTicketCategory '1BB',12
exec dbo.INFAddUpdateTicketCategory '1CB',11
exec dbo.INFAddUpdateTicketCategory '1SS',9
exec dbo.INFAddUpdateTicketCategory 'MSV',9
exec dbo.INFAddUpdateTicketCategory 'OAS',10
exec dbo.INFAddUpdateTicketCategory '1BA',9
exec dbo.INFAddUpdateTicketCategory 'F0R',9
exec dbo.INFAddUpdateTicketCategory 'IDO',12
exec dbo.INFAddUpdateTicketCategory 'F55',11
exec dbo.INFAddUpdateTicketCategory 'F56',11
exec dbo.INFAddUpdateTicketCategory 'S55',9
exec dbo.INFAddUpdateTicketCategory '55F',9
exec dbo.INFAddUpdateTicketCategory '55S',9
exec dbo.INFAddUpdateTicketCategory '2FB',7
exec dbo.INFAddUpdateTicketCategory '2GB',7
exec dbo.INFAddUpdateTicketCategory 'CBB',9
exec dbo.INFAddUpdateTicketCategory 'F5A',11
exec dbo.INFAddUpdateTicketCategory 'S5A',6
exec dbo.INFAddUpdateTicketCategory '1ST',9
exec dbo.INFAddUpdateTicketCategory 'STP',9
exec dbo.INFAddUpdateTicketCategory 'NOR',9
exec dbo.INFAddUpdateTicketCategory 'GCA',10
exec dbo.INFAddUpdateTicketCategory 'GCB',5
exec dbo.INFAddUpdateTicketCategory 'GCH',7
exec dbo.INFAddUpdateTicketCategory 'CMR',9
exec dbo.INFAddUpdateTicketCategory '55W',9
exec dbo.INFAddUpdateTicketCategory 'C2S',6
exec dbo.INFAddUpdateTicketCategory 'C4S',7
exec dbo.INFAddUpdateTicketCategory 'F2V',6
exec dbo.INFAddUpdateTicketCategory 'F4V',7
exec dbo.INFAddUpdateTicketCategory 'SAQ',5
exec dbo.INFAddUpdateTicketCategory 'GCE',12
exec dbo.INFAddUpdateTicketCategory 'GC7',10
exec dbo.INFAddUpdateTicketCategory 'GC8',10
exec dbo.INFAddUpdateTicketCategory 'GCD',6
exec dbo.INFAddUpdateTicketCategory 'GCG',6
exec dbo.INFAddUpdateTicketCategory 'GCQ',6
exec dbo.INFAddUpdateTicketCategory 'GCJ',7
exec dbo.INFAddUpdateTicketCategory 'GCV',5
exec dbo.INFAddUpdateTicketCategory 'OPF',9
exec dbo.INFAddUpdateTicketCategory 'SOF',9
exec dbo.INFAddUpdateTicketCategory 'SOA',9
exec dbo.INFAddUpdateTicketCategory 'SOB',9
exec dbo.INFAddUpdateTicketCategory 'WTC',9
exec dbo.INFAddUpdateTicketCategory 'XS5',14
exec dbo.INFAddUpdateTicketCategory 'XS6',14
exec dbo.INFAddUpdateTicketCategory 'EPT',9
exec dbo.INFAddUpdateTicketCategory 'JBA',11
exec dbo.INFAddUpdateTicketCategory 'JBB',6
exec dbo.INFAddUpdateTicketCategory 'JBC',9
exec dbo.INFAddUpdateTicketCategory 'JBD',9
exec dbo.INFAddUpdateTicketCategory 'SMG',9
exec dbo.INFAddUpdateTicketCategory 'VER',9
exec dbo.INFAddUpdateTicketCategory '55P',9
exec dbo.INFAddUpdateTicketCategory 'DG1',5
exec dbo.INFAddUpdateTicketCategory 'DG2',7
exec dbo.INFAddUpdateTicketCategory 'DG3',6
exec dbo.INFAddUpdateTicketCategory 'NN2',9
exec dbo.INFAddUpdateTicketCategory 'GM6',9
exec dbo.INFAddUpdateTicketCategory 'UFH',11
exec dbo.INFAddUpdateTicketCategory 'USH',14
exec dbo.INFAddUpdateTicketCategory '1PS',9
exec dbo.INFAddUpdateTicketCategory 'EMB',14
exec dbo.INFAddUpdateTicketCategory 'WK2',9
exec dbo.INFAddUpdateTicketCategory 'WK1',9
exec dbo.INFAddUpdateTicketCategory 'OSB',14
exec dbo.INFAddUpdateTicketCategory '2FF',7
exec dbo.INFAddUpdateTicketCategory '2GF',5
exec dbo.INFAddUpdateTicketCategory '2HF',5

GO

exec INFAddUpdateCalendar 'Jan 01 2013 12:00AM',4
exec INFAddUpdateCalendar 'Jan 05 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jan 06 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jan 12 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jan 13 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jan 19 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jan 20 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jan 26 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jan 27 2013 12:00AM',3
exec INFAddUpdateCalendar 'Feb 02 2013 12:00AM',3
exec INFAddUpdateCalendar 'Feb 03 2013 12:00AM',3
exec INFAddUpdateCalendar 'Feb 09 2013 12:00AM',3
exec INFAddUpdateCalendar 'Feb 10 2013 12:00AM',3
exec INFAddUpdateCalendar 'Feb 16 2013 12:00AM',3
exec INFAddUpdateCalendar 'Feb 17 2013 12:00AM',3
exec INFAddUpdateCalendar 'Feb 23 2013 12:00AM',3
exec INFAddUpdateCalendar 'Feb 24 2013 12:00AM',3
exec INFAddUpdateCalendar 'Mar 02 2013 12:00AM',3
exec INFAddUpdateCalendar 'Mar 03 2013 12:00AM',3
exec INFAddUpdateCalendar 'Mar 09 2013 12:00AM',3
exec INFAddUpdateCalendar 'Mar 10 2013 12:00AM',3
exec INFAddUpdateCalendar 'Mar 16 2013 12:00AM',3
exec INFAddUpdateCalendar 'Mar 17 2013 12:00AM',3
exec INFAddUpdateCalendar 'Mar 23 2013 12:00AM',3
exec INFAddUpdateCalendar 'Mar 24 2013 12:00AM',3
exec INFAddUpdateCalendar 'Mar 29 2013 12:00AM',4
exec INFAddUpdateCalendar 'Mar 30 2013 12:00AM',3
exec INFAddUpdateCalendar 'Mar 31 2013 12:00AM',3
exec INFAddUpdateCalendar 'Apr 01 2013 12:00AM',4
exec INFAddUpdateCalendar 'Apr 06 2013 12:00AM',3
exec INFAddUpdateCalendar 'Apr 07 2013 12:00AM',3
exec INFAddUpdateCalendar 'Apr 13 2013 12:00AM',3
exec INFAddUpdateCalendar 'Apr 14 2013 12:00AM',3
exec INFAddUpdateCalendar 'Apr 20 2013 12:00AM',3
exec INFAddUpdateCalendar 'Apr 21 2013 12:00AM',3
exec INFAddUpdateCalendar 'Apr 27 2013 12:00AM',3
exec INFAddUpdateCalendar 'Apr 28 2013 12:00AM',3
exec INFAddUpdateCalendar 'May 04 2013 12:00AM',3
exec INFAddUpdateCalendar 'May 05 2013 12:00AM',3
exec INFAddUpdateCalendar 'May 06 2013 12:00AM',4
exec INFAddUpdateCalendar 'May 11 2013 12:00AM',3
exec INFAddUpdateCalendar 'May 12 2013 12:00AM',3
exec INFAddUpdateCalendar 'May 18 2013 12:00AM',3
exec INFAddUpdateCalendar 'May 19 2013 12:00AM',3
exec INFAddUpdateCalendar 'May 25 2013 12:00AM',3
exec INFAddUpdateCalendar 'May 26 2013 12:00AM',3
exec INFAddUpdateCalendar 'May 27 2013 12:00AM',4
exec INFAddUpdateCalendar 'Jun 01 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jun 02 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jun 08 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jun 09 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jun 15 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jun 16 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jun 22 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jun 23 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jun 29 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jun 30 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jul 06 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jul 07 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jul 13 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jul 14 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jul 20 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jul 21 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jul 27 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jul 28 2013 12:00AM',3
exec INFAddUpdateCalendar 'Aug 03 2013 12:00AM',3
exec INFAddUpdateCalendar 'Aug 04 2013 12:00AM',3
exec INFAddUpdateCalendar 'Aug 10 2013 12:00AM',3
exec INFAddUpdateCalendar 'Aug 11 2013 12:00AM',3
exec INFAddUpdateCalendar 'Aug 17 2013 12:00AM',3
exec INFAddUpdateCalendar 'Aug 18 2013 12:00AM',3
exec INFAddUpdateCalendar 'Aug 24 2013 12:00AM',3
exec INFAddUpdateCalendar 'Aug 25 2013 12:00AM',3
exec INFAddUpdateCalendar 'Aug 26 2013 12:00AM',4
exec INFAddUpdateCalendar 'Aug 31 2013 12:00AM',3
exec INFAddUpdateCalendar 'Sep 01 2013 12:00AM',3
exec INFAddUpdateCalendar 'Sep 07 2013 12:00AM',3
exec INFAddUpdateCalendar 'Sep 08 2013 12:00AM',3
exec INFAddUpdateCalendar 'Sep 14 2013 12:00AM',3
exec INFAddUpdateCalendar 'Sep 15 2013 12:00AM',3
exec INFAddUpdateCalendar 'Sep 21 2013 12:00AM',3
exec INFAddUpdateCalendar 'Sep 22 2013 12:00AM',3
exec INFAddUpdateCalendar 'Sep 28 2013 12:00AM',3
exec INFAddUpdateCalendar 'Sep 29 2013 12:00AM',3
exec INFAddUpdateCalendar 'Oct 05 2013 12:00AM',3
exec INFAddUpdateCalendar 'Oct 06 2013 12:00AM',3
exec INFAddUpdateCalendar 'Oct 12 2013 12:00AM',3
exec INFAddUpdateCalendar 'Oct 13 2013 12:00AM',3
exec INFAddUpdateCalendar 'Oct 19 2013 12:00AM',3
exec INFAddUpdateCalendar 'Oct 20 2013 12:00AM',3
exec INFAddUpdateCalendar 'Oct 26 2013 12:00AM',3
exec INFAddUpdateCalendar 'Oct 27 2013 12:00AM',3
exec INFAddUpdateCalendar 'Nov 02 2013 12:00AM',3
exec INFAddUpdateCalendar 'Nov 03 2013 12:00AM',3
exec INFAddUpdateCalendar 'Nov 09 2013 12:00AM',3
exec INFAddUpdateCalendar 'Nov 10 2013 12:00AM',3
exec INFAddUpdateCalendar 'Nov 16 2013 12:00AM',3
exec INFAddUpdateCalendar 'Nov 17 2013 12:00AM',3
exec INFAddUpdateCalendar 'Nov 23 2013 12:00AM',3
exec INFAddUpdateCalendar 'Nov 24 2013 12:00AM',3
exec INFAddUpdateCalendar 'Nov 30 2013 12:00AM',3
exec INFAddUpdateCalendar 'Dec 01 2013 12:00AM',3
exec INFAddUpdateCalendar 'Dec 07 2013 12:00AM',3
exec INFAddUpdateCalendar 'Dec 08 2013 12:00AM',3
exec INFAddUpdateCalendar 'Dec 14 2013 12:00AM',3
exec INFAddUpdateCalendar 'Dec 15 2013 12:00AM',3
exec INFAddUpdateCalendar 'Dec 21 2013 12:00AM',3
exec INFAddUpdateCalendar 'Dec 22 2013 12:00AM',3
exec INFAddUpdateCalendar 'Dec 23 2013 12:00AM',5
exec INFAddUpdateCalendar 'Dec 24 2013 12:00AM',5
exec INFAddUpdateCalendar 'Dec 25 2013 12:00AM',6
exec INFAddUpdateCalendar 'Dec 26 2013 12:00AM',7
exec INFAddUpdateCalendar 'Dec 28 2013 12:00AM',3
exec INFAddUpdateCalendar 'Dec 29 2013 12:00AM',3
exec INFAddUpdateCalendar 'Jan 01 2014 12:00AM',4
exec INFAddUpdateCalendar 'Jan 04 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jan 05 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jan 11 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jan 12 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jan 18 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jan 19 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jan 25 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jan 26 2014 12:00AM',3
exec INFAddUpdateCalendar 'Feb 01 2014 12:00AM',3
exec INFAddUpdateCalendar 'Feb 02 2014 12:00AM',3
exec INFAddUpdateCalendar 'Feb 08 2014 12:00AM',3
exec INFAddUpdateCalendar 'Feb 09 2014 12:00AM',3
exec INFAddUpdateCalendar 'Feb 15 2014 12:00AM',3
exec INFAddUpdateCalendar 'Feb 16 2014 12:00AM',3
exec INFAddUpdateCalendar 'Feb 22 2014 12:00AM',3
exec INFAddUpdateCalendar 'Feb 23 2014 12:00AM',3
exec INFAddUpdateCalendar 'Mar 01 2014 12:00AM',3
exec INFAddUpdateCalendar 'Mar 02 2014 12:00AM',3
exec INFAddUpdateCalendar 'Mar 08 2014 12:00AM',3
exec INFAddUpdateCalendar 'Mar 09 2014 12:00AM',3
exec INFAddUpdateCalendar 'Mar 15 2014 12:00AM',3
exec INFAddUpdateCalendar 'Mar 16 2014 12:00AM',3
exec INFAddUpdateCalendar 'Mar 22 2014 12:00AM',3
exec INFAddUpdateCalendar 'Mar 23 2014 12:00AM',3
exec INFAddUpdateCalendar 'Mar 29 2014 12:00AM',3
exec INFAddUpdateCalendar 'Mar 30 2014 12:00AM',3
exec INFAddUpdateCalendar 'Apr 05 2014 12:00AM',3
exec INFAddUpdateCalendar 'Apr 06 2014 12:00AM',3
exec INFAddUpdateCalendar 'Apr 12 2014 12:00AM',3
exec INFAddUpdateCalendar 'Apr 13 2014 12:00AM',3
exec INFAddUpdateCalendar 'Apr 18 2014 12:00AM',4
exec INFAddUpdateCalendar 'Apr 19 2014 12:00AM',3
exec INFAddUpdateCalendar 'Apr 20 2014 12:00AM',3
exec INFAddUpdateCalendar 'Apr 21 2014 12:00AM',4
exec INFAddUpdateCalendar 'Apr 26 2014 12:00AM',3
exec INFAddUpdateCalendar 'Apr 27 2014 12:00AM',3
exec INFAddUpdateCalendar 'May 03 2014 12:00AM',3
exec INFAddUpdateCalendar 'May 04 2014 12:00AM',3
exec INFAddUpdateCalendar 'May 05 2014 12:00AM',4
exec INFAddUpdateCalendar 'May 10 2014 12:00AM',3
exec INFAddUpdateCalendar 'May 11 2014 12:00AM',3
exec INFAddUpdateCalendar 'May 17 2014 12:00AM',3
exec INFAddUpdateCalendar 'May 18 2014 12:00AM',3
exec INFAddUpdateCalendar 'May 24 2014 12:00AM',3
exec INFAddUpdateCalendar 'May 25 2014 12:00AM',3
exec INFAddUpdateCalendar 'May 26 2014 12:00AM',4
exec INFAddUpdateCalendar 'May 31 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jun 01 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jun 07 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jun 08 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jun 14 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jun 15 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jun 21 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jun 22 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jun 28 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jun 29 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jul 05 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jul 06 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jul 12 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jul 13 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jul 19 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jul 20 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jul 26 2014 12:00AM',3
exec INFAddUpdateCalendar 'Jul 27 2014 12:00AM',3
exec INFAddUpdateCalendar 'Aug 02 2014 12:00AM',3
exec INFAddUpdateCalendar 'Aug 03 2014 12:00AM',3
exec INFAddUpdateCalendar 'Aug 09 2014 12:00AM',3
exec INFAddUpdateCalendar 'Aug 10 2014 12:00AM',3
exec INFAddUpdateCalendar 'Aug 16 2014 12:00AM',3
exec INFAddUpdateCalendar 'Aug 17 2014 12:00AM',3
exec INFAddUpdateCalendar 'Aug 23 2014 12:00AM',3
exec INFAddUpdateCalendar 'Aug 24 2014 12:00AM',3
exec INFAddUpdateCalendar 'Aug 25 2014 12:00AM',4
exec INFAddUpdateCalendar 'Aug 30 2014 12:00AM',3
exec INFAddUpdateCalendar 'Aug 31 2014 12:00AM',3
exec INFAddUpdateCalendar 'Sep 06 2014 12:00AM',3
exec INFAddUpdateCalendar 'Sep 07 2014 12:00AM',3
exec INFAddUpdateCalendar 'Sep 13 2014 12:00AM',3
exec INFAddUpdateCalendar 'Sep 14 2014 12:00AM',3
exec INFAddUpdateCalendar 'Sep 20 2014 12:00AM',3
exec INFAddUpdateCalendar 'Sep 21 2014 12:00AM',3
exec INFAddUpdateCalendar 'Sep 27 2014 12:00AM',3
exec INFAddUpdateCalendar 'Sep 28 2014 12:00AM',3
exec INFAddUpdateCalendar 'Oct 04 2014 12:00AM',3
exec INFAddUpdateCalendar 'Oct 05 2014 12:00AM',3
exec INFAddUpdateCalendar 'Oct 11 2014 12:00AM',3
exec INFAddUpdateCalendar 'Oct 12 2014 12:00AM',3
exec INFAddUpdateCalendar 'Oct 18 2014 12:00AM',3
exec INFAddUpdateCalendar 'Oct 19 2014 12:00AM',3
exec INFAddUpdateCalendar 'Oct 25 2014 12:00AM',3
exec INFAddUpdateCalendar 'Oct 26 2014 12:00AM',3
exec INFAddUpdateCalendar 'Nov 01 2014 12:00AM',3
exec INFAddUpdateCalendar 'Nov 02 2014 12:00AM',3
exec INFAddUpdateCalendar 'Nov 08 2014 12:00AM',3
exec INFAddUpdateCalendar 'Nov 09 2014 12:00AM',3
exec INFAddUpdateCalendar 'Nov 15 2014 12:00AM',3
exec INFAddUpdateCalendar 'Nov 16 2014 12:00AM',3
exec INFAddUpdateCalendar 'Nov 22 2014 12:00AM',3
exec INFAddUpdateCalendar 'Nov 23 2014 12:00AM',3
exec INFAddUpdateCalendar 'Nov 29 2014 12:00AM',3
exec INFAddUpdateCalendar 'Nov 30 2014 12:00AM',3
exec INFAddUpdateCalendar 'Dec 06 2014 12:00AM',3
exec INFAddUpdateCalendar 'Dec 07 2014 12:00AM',3
exec INFAddUpdateCalendar 'Dec 13 2014 12:00AM',3
exec INFAddUpdateCalendar 'Dec 14 2014 12:00AM',3
exec INFAddUpdateCalendar 'Dec 20 2014 12:00AM',3
exec INFAddUpdateCalendar 'Dec 21 2014 12:00AM',3


GO

----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3) = 1.14


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 004 and VersionNumber = @@value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Updates in respect of the National Rail fares revision on 18 May 2014'
	WHERE ScriptNumber = 004 AND VersionNumber = @@value
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
		004,
		@@value,
		'Updates in respect of the National Rail fares revision on 18 May 2014'
	)
END
