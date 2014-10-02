-- ***********************************************
-- AUTHOR      	: John Frank
-- NAME 	: MDS0001_RailTicketTypes.sql
-- DESCRIPTION 	: Updates the list of displayable rail tickets
-- SOURCE 	: Manual Data Service
-- Additional Steps Required : IIS Reset Webservers
-- ************************************************
--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS001_RailTicketTypes.sql-arc  $
--
--	 Rev 1.30 Apr 09 Cpietruski - Updates in respect of the National Rail fares revision on 18 May 2014 from MDS
--
--	 Rev 1.29 Nov 28 2013 Cpietruski - Updates in respect of the National Rail fares revision on 02 January 2014 from MDS
--
--	 Rev 1.28 Aug 20 2013 RBroddle - Updates in respect of the National Rail fares revision on 08 September 2013 from MDS
--
--   Rev 1.27   Jan 07 2013 11:28:42   RBroddle
--Latest rail data - went in with changes C1297547 BBP / C1331034 ACP Aug. 2012
--Resolution for 5880: Latest Rail data update from MDS
--
--   Rev 1.26   Aug 10 2012 13:12:26   RBroddle
--Rail ticket data updates from MDS
--
--   Rev 1.25   Apr 30 2012 13:30:20   nrankin
--Latest update to Tickect Route Restrictions / Cat Hashes / Ticket Types delivered by PUSS team.
--Resolution for 5806: Ticket Route Restrictions / Categorised Hashes (MDS)
--
--   Rev 1.24   Dec 01 2011 13:30:24   nrankin
--Latest update to Tickect Route Restrictions / Categorised Hashes / Ticket Types delivered by PUSS team.
--
--Resolution for 5769: Update to Tickect Route Restrictions / Cat Hashes / Ticket Types
--
--   Rev 1.23   Aug 24 2011 15:53:10   nrankin
--Latest Updates from MDS - Ticket Route Restrictions / Categorised Hashes
--Resolution for 5730: Ticket Route Restrictions / Categorised Hashes (MDS)
--
--   Rev 1.22   May 09 2011 15:36:24   PScott
--Latest MDS changes
--
--   Rev 1.20   Dec 08 2010 14:33:08   rbroddle
--Latest updates from MDS
--
--   Rev 1.19   Oct 05 2010 12:05:00   apatel
--Updated INFAddUpdateCategorisedHashes stored proc to include extra information for the dataset. Also updated  DisplayableRailTickets dataset to include extra information as true/false string to confirm if the ticket is same day return.
--Resolution for 5614: Rail Search By Price - Invalid day return tickets shown
--
--   Rev 1.18   Aug 26 2010 11:24:00   rbroddle
--updates  from Chris P - changes 3217624 and 3217931
--
--   Rev 1.17   Aug 05 2010 15:26:04   nrankin
--Change UK:C2888148 - CatergorisedHashes and TicketCategory update
--
--   Rev 1.16   Apr 16 2010 16:25:26   jroberts
--Change UK: C2760577
--
--   Rev 1.15   Dec 18 2009 15:04:16   PScott
--Change C2343356 - MDS changes 18/12/2009
--
--   Rev 1.14   Oct 05 2009 10:02:10   scraddock
--Updated by Chris P for USD UK:5497549 to remove ticket type XG2
--
--   Rev 1.13   Sep 07 2009 11:22:46   nrankin
--Amended for Chris P - MGF was descibed as "2nd Advance" - it should be "1st Advance"
--
--   Rev 1.12   Aug 28 2009 11:04:12   nrankin
--Updated with latest CatergorisedHashes data from MDS
--
--   Rev 1.11   May 01 2009 12:23:46   nrankin
--Updated RailTicketTypes - May 1st 09
--
--   Rev 1.10   Jan 30 2009 12:00:50   mmodi
--Corrected Unknowns with actual flexibility
--
--   Rev 1.9   Jan 30 2009 11:44:12   mmodi
--Update empty flexibiliy values to be Unknown
--Resolution for 5210: CCN487 - ZPBO Implementation workstream
--
--   Rev 1.8   Jan 30 2009 10:53:16   mmodi
--Updated with ZPBO related data from MDS
--Resolution for 5210: CCN487 - ZPBO Implementation workstream
--
--   Rev 1.7   Jan 12 2009 09:08:30   mmodi
--Added Travelcard ticket types (temporary)
--Resolution for 5210: CCN487 - ZPBO Implementation workstream
--
--   Rev 1.6   Dec 01 2008 11:45:22   jroberts
--Change UK:C1320227
--Load new categorised hashes file (for new ticket types)
--
--   Rev 1.5   Sep 18 2008 15:25:32   rbroddle
--Updated with latest data from Peter Arnold 18-09-08
--
--   Rev 1.4   Aug 05 2008 14:48:32   nrankin
--slight mistake in 1.3 (whoops)
--
--Updated MDS data 05/08/08
--
--   Rev 1.3   Aug 05 2008 14:40:04   nrankin
--Upadated with latest MDS data
--
--   Rev 1.2   May 08 2008 16:47:58   rbroddle
--updated for MDS update 08/05/08
--
--   Rev 1.1   May 02 2008 15:13:14   rbroddle
--Updated from "CatergorisedHashes 20080418.csv" provided by MDS team.
--
--   Rev 1.0   Nov 08 2007 12:42:24   mturner
--Initial revision.
--
--   Rev 1.12   Aug 22 2007 15:39:38   nrankin
--Script ran into BBP for testing
--
--   Rev 1.11   May 17 2007 16:54:34   rbroddle
--Corrected for extra "TicketGroup" column
--
--   Rev 1.10   May 17 2007 16:25:38   rbroddle
--Upadted with latest MDS data
--
--   Rev 1.9   Jan 03 2007 15:05:48   rbroddle
--Updated with new list from MDS
--
--   Rev 1.8   Dec 29 2006 18:23:54   rbroddle
--Corrected fault in December MDS data.
--
--   Rev 1.7   Dec 29 2006 15:50:06   rbroddle
--Updated with latest MDS data - Dec 06
--
--   Rev 1.6   Nov 28 2006 17:07:26   rbroddle
--Updated with Latest MDS data - November 06
--
--   Rev 1.5   Jun 01 2006 14:55:08   RBroddle
--Ticket Types updates recieved from MDS team 01/06/06
--
--   Rev 1.4   Jan 30 2006 12:38:38   JFrank
--Update provide by MDS 30/01/2006
--
--   Rev 1.3   Dec 16 2005 14:10:46   JFrank
--New ticket types supplied by MDS
--
--   Rev 1.2   Dec 12 2005 15:52:08   JFrank
--Updated Header
--
--   Rev 1.1   Dec 12 2005 15:43:16   JFrank
--Amended change control update statement.
--
--   Rev 1.0   Dec 12 2005 15:26:54   JFrank
--Initial revision.

USE PermanentPortal
GO
-- **********************************************************************
-- PROCEDURE INFAddUpdateCategorisedHashes
-- **********************************************************************
DECLARE @ObjectName AS varchar(128)
SET @ObjectName = N'INFAddUpdateCategorisedHashes'
IF NOT EXISTS(SELECT 1 
                FROM INFORMATION_SCHEMA.ROUTINES
               WHERE ROUTINE_NAME = @ObjectName
                 AND ROUTINE_TYPE = N'PROCEDURE')
BEGIN
    EXEC ('CREATE PROCEDURE [dbo].INFAddUpdateCategorisedHashes AS BEGIN SELECT 1 END')

    EXEC sp_addextendedproperty @name=N'Design', @value=N'SD00?? ?????' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Owner', @value=N'TDP' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName
    EXEC sp_addextendedproperty @name=N'Used By', @value=N'TDP.Infrastructure' ,@level0type=N'user', @level0name=N'dbo', @level1type=N'PROCEDURE', @level1name=@ObjectName

END
GO


GO
-- **********************************************************************
-- PROCEDURE INFAddUpdateCategorisedHashes
-- Description: 
-- 
-- **********************************************************************
ALTER PROCEDURE dbo.INFAddUpdateCategorisedHashes
(
    @DataSet      varchar(200),
    @KeyName      varchar(200),
    @Value        varchar(200),
    @Category     varchar(200),
    @TicketGroup  varchar(200),
    @Data1		  varchar(50)
) AS
BEGIN
    SET NOCOUNT ON
    IF EXISTS(SELECT 1
                FROM dbo.CategorisedHashes
               WHERE DataSet = @DataSet
                 AND KeyName = @KeyName)
        BEGIN
          UPDATE dbo.CategorisedHashes
             SET Value = @Value,
                 Category = @Category,
                 TicketGroup  = @TicketGroup,
                 Data1 = @Data1
           WHERE DataSet   = @DataSet
             AND KeyName   = @KeyName
           PRINT 'Updated INFAddUpdateCategorisedHashes KeyName ' + @KeyName
        END
    ELSE
        BEGIN
            INSERT INTO dbo.CategorisedHashes 
            (
                DataSet, 
                KeyName, 
                Value, 
                Category, 
                TicketGroup,
                Data1
            )
            VALUES
            (
                @DataSet, 
                @KeyName, 
                @Value, 
                @Category, 
                @TicketGroup,
                @Data1
            )
            PRINT 'Inserted INFAddUpdateCategorisedHashes KeyName ' + @KeyName
        END
    --END IF    
END
GO

-- Delete all rows
TRUNCATE TABLE dbo.CategorisedHashes
GO
-- Insert new rows
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XP2', '1st Park & Go', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XP1', '1st Park & Go', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XFR', 'First Daypex Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XF', 'First Daypex', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XD2', 'East Coast Executive Day Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XD1', 'East Coast Executive Day Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XC2', 'East Coast Standard Business Package', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XC1', 'East Coast Standard Business Package', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XB2', 'East Coast Standard Business Day Package', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XB1', 'East Coast Standard Business Day Package', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WKR', 'The Weekender', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WHS', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WGS', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WFS', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WFR', 'Weekend Day First', 'NoFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WDS', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WCS', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WBS', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WAS', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'VCS', 'Advance', 'NoFlexibility', 'VTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'VBS', 'Advance', 'NoFlexibility', 'VTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'VBR', 'Virgin Business Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'VAS', 'Advance', 'NoFlexibility', 'VTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'VA2', 'Advance', 'NoFlexibility', 'SCSAdvOther', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'VA1', 'Advance', 'NoFlexibility', 'SCSAdvOther', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'TB2', 'Advance', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'TB1', '1st Advance', 'NoFlexibility', 'TPFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'TA2', 'Advance', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'TA1', '1st Advance', 'NoFlexibility', 'TPFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SVS', 'Off-Peak Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SVR', 'Off-Peak Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SSS', 'Super Off-Peak Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SSR', 'Super Off-Peak Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SPG', 'Standard Plus Parking', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SOS', 'Anytime Single', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SOR', 'Anytime Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SES', 'Super Economy Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SER', 'Super Economy Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SDS', 'Anytime Day Single', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SDR', 'Anytime Day Return', 'FullyFlexible', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAS', 'Advance', 'NoFlexibility', 'XXSAdvOther', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAR', 'Advance Sleeper Solo (with TV)', 'NoFlexibility', 'GWSAdvLeisureSGL', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAL', 'Advance Sleeper Solo (with TV)', 'NoFlexibility', 'GWSAdvLeisureSGL', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAK', 'Advance Sleeper Solo (with TV)', 'NoFlexibility', 'GWSAdvLeisureSGL', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAJ', 'Advance Sleeper Solo (with TV)', 'NoFlexibility', 'GWSAdvLeisureSGL', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAI', 'Advance Sleeper Solo (no TV)', 'NoFlexibility', 'GWSAdvLeisureSGL', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAH', 'Advance Sleeper Solo (no TV)', 'NoFlexibility', 'GWSAdvLeisureSGL', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAG', 'Advance Sleeper Solo (no TV)', 'NoFlexibility', 'GWSAdvLeisureSGL', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAD', 'Std Sleeper Open Solo (no TV)', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAC', 'Std Sleeper Open Solo (with TV)', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OS4', 'Advance', 'NoFlexibility', 'LESAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OS3', 'Advance', 'NoFlexibility', 'LESAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OS2', 'Advance', 'NoFlexibility', 'LESAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OS1', 'Advance', 'NoFlexibility', 'LESAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OPS', 'Super Off-Peak Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OPR', 'Super Off-Peak Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OGS', '1st Advance', 'NoFlexibility', 'GRFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OF4', '1st Advance', 'NoFlexibility', 'LEFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OF3', '1st Advance', 'NoFlexibility', 'LEFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OF2', '1st Advance', 'NoFlexibility', 'LEFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OF1', '1st Advance', 'NoFlexibility', 'LEFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OES', '1st Advance', 'NoFlexibility', 'GRFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'ODS', '1st Advance', 'NoFlexibility', 'GRFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OCS', '1st Advance', 'NoFlexibility', 'GRFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OBS', '1st Advance', 'NoFlexibility', 'GRFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'NWD', 'Advance', 'NoFlexibility', 'NTSAdvLeisure', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'NAR', 'Network Away Break', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MXR', 'Airport Apex Return', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MWA', 'East Midlands Trains Weekender', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'LNO', 'Super Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'LFR', 'Leisure First Return', 'NoFlexibility', 'XXFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'LEX', 'Flexi Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'HTT', '1st Advance', 'NoFlexibility', 'HTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'HTS', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'HTA', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'HT4', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'HT2', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'HT1', '1st Advance', 'NoFlexibility', 'HTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GFS', 'Anytime First Single', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GFR', 'Anytime First Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GE1', 'Off-Peak Day First Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FXS', '1st Advance', 'NoFlexibility', 'XXFAdvOther', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FXR', '1st Advance Return', 'NoFlexibility', 'XXFAdvOther', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FSS', 'Off-Peak First Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FSR', 'Off-Peak First Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FPP', 'First Plus Parking', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FOS', 'Anytime First Single', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FOR', 'Anytime First Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FFS', 'Family Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FDS', 'Anytime Day First Single', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FDR', 'Anytime Day First Return', 'FullyFlexible', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FCS', '1st Advance', 'NoFlexibility', 'VTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FCR', 'Off-Peak Day First Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FBS', '1st Advance', 'NoFlexibility', 'VTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FAS', '1st Advance', 'NoFlexibility', 'VTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FAR', 'First Advance', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DXR', 'Daypex Return', 'NoFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DHS', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DGS', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DFS', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DES', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DDS', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DCS', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DBS', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DAS', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'CDS', 'Off-Peak Day Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'CDR', 'Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'CBP', 'Clubman+CR Only', 'NoFlexibility', 'CHSAdvBusiness', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'C7S', 'Advance', 'NoFlexibility', 'LMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'C7R', 'Advance Return', 'NoFlexibility', 'LMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'C3S', 'Advance', 'NoFlexibility', 'LMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'C1S', 'Advance', 'NoFlexibility', 'LMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'C1B', 'Advance Return', 'NoFlexibility', 'LMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BVR', 'Off-Peak Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BUS', 'Advance', 'NoFlexibility', 'GRSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BTS', 'Advance', 'NoFlexibility', 'GRSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BSS', 'Advance', 'NoFlexibility', 'GRSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BRS', 'Advance', 'NoFlexibility', 'GRSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BGO', '1st Advance', 'NoFlexibility', 'VTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BFS', 'Off-Peak First Single', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BFR', 'Off-Peak First Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BFO', '1st Advance', 'NoFlexibility', 'VTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BFA', 'Business Advance 1st Single', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BCO', 'Advance', 'NoFlexibility', 'VTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BBO', 'Advance', 'NoFlexibility', 'VTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BAO', 'Advance', 'NoFlexibility', 'VTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AXS', 'Advance', 'NoFlexibility', 'XXSAdvOther', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AXR', 'Advance Return', 'NoFlexibility', 'XXSAdvOther', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AVR', 'Advance Return', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'ASV', 'Business Advance', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2IS', 'Advance', 'NoFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2IC', 'Advance', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2HS', 'Advance', 'NoFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2HC', 'Advance', 'NoFlexibility', 'CHSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2GS', 'Advance', 'NoFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2GC', 'Advance', 'NoFlexibility', 'CHSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2FS', 'Advance', 'NoFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2FC', 'Advance', 'NoFlexibility', 'CHSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2ES', 'Advance', 'NoFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2EC', 'Advance', 'NoFlexibility', 'CHSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2DS', 'Advance', 'NoFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2DC', 'Advance', 'NoFlexibility', 'CHSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2CS', 'Advance', 'NoFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2CC', 'Advance', 'NoFlexibility', 'CHSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2BC', 'Advance', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2AC', 'Advance', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1ES', '1st Advance', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1EC', '1st Advance', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1DS', '1st Advance', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1DC', '1st Advance', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1CS', '1st Advance', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1CC', '1st Advance', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1BS', '1st Advance', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1BC', '1st Advance', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1AS', '1st Advance', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1AC', '1st Advance', 'NoFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F1V', '1st Advance', 'NoFlexibility', 'LMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F3V', '1st Advance', 'NoFlexibility', 'LMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F7V', '1st Advance', 'NoFlexibility', 'LMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GDS', 'Super Off-Peak Day Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GDR', 'Super Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'PDR', 'Super Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'PDS', 'Super Off-Peak Day Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WES', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SCO', 'Super Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'CPR', 'Cheap Park & Ride', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'PSR', 'Standard Park & Ride', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'NBA', 'Advance', 'NoFlexibility', 'AWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'NCA', 'Advance', 'NoFlexibility', 'AWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'NDA', 'Advance', 'NoFlexibility', 'AWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'NEA', 'Advance', 'NoFlexibility', 'AWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'C1R', 'Super Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'C2R', 'Group Super Off-Peak', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FCD', 'Off-Peak Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCR', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCS', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCT', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCU', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GVR', 'Off-Peak Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'VBT', 'Virgin Business Standard', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GUS', 'Anytime First Single', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GUR', 'Anytime First Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GTS', 'Anytime Single', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GTR', 'Anytime Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAV', 'Advance', 'NoFlexibility', 'SNSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SBV', 'Advance', 'NoFlexibility', 'SNSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SCV', 'Advance', 'NoFlexibility', 'SNSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BXS', 'Advance', 'NoFlexibility', 'GRSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1AF', '1st Advance', 'NoFlexibility', 'TPFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1BF', '1st Advance', 'NoFlexibility', 'TPFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1CF', '1st Advance', 'NoFlexibility', 'TPFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1DF', '1st Advance', 'NoFlexibility', 'TPFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1EF', '1st Advance', 'NoFlexibility', 'TPFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2AF', 'Advance', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2BF', 'Advance', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2CF', 'Advance', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2DF', 'Advance', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2EF', 'Advance', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'HSS', 'Hull Trains Standard Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FA1', 'TransPennine Express Airport Getaway 1 Adult', 'LimitedFlexibility', 'TPSAdvBusiness', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DRR', 'Dales Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DRI', 'Dalesrail Cheap Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1FS', '1st Advance', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2JS', 'Advance', 'NoFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'ECD', 'Evening Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MWS', 'Midweek Mover', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OF5', '1st Advance', 'NoFlexibility', 'LEFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OF6', '1st Advance', 'NoFlexibility', 'LEFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OS5', 'Advance', 'NoFlexibility', 'LESAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OS6', 'Advance', 'NoFlexibility', 'LESAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MAF', '1st Advance', 'NoFlexibility', 'XCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MAS', 'Advance', 'NoFlexibility', 'XCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MBF', '1st Advance', 'NoFlexibility', 'XCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MBS', 'Advance', 'NoFlexibility', 'XCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MCF', '1st Advance', 'NoFlexibility', 'XCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MCS', 'Advance', 'NoFlexibility', 'XCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MDF', '1st Advance', 'NoFlexibility', 'XCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MDS', 'Advance', 'NoFlexibility', 'XCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MEF', '1st Advance', 'NoFlexibility', 'XCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MES', 'Advance', 'NoFlexibility', 'XCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'S1A', 'Advance', 'NoFlexibility', 'SWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'S2A', 'Advance', 'NoFlexibility', 'SWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'S3A', 'Advance', 'NoFlexibility', 'SWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'S4A', 'Advance', 'NoFlexibility', 'SWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F1A', '1st Advance', 'NoFlexibility', 'SWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F2A', '1st Advance', 'NoFlexibility', 'SWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F3A', '1st Advance', 'NoFlexibility', 'SWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F4A', '1st Advance', 'NoFlexibility', 'SWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BPS', 'Advance', 'NoFlexibility', 'GRSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BVS', 'Off-Peak Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'PMR', 'Super Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'EGF', 'Super Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'CBA', 'Super Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OEO', 'Oxford Evening Out', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SOP', 'Super Off-Peak Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FSO', 'Super Off-Peak Day First Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SWS', 'Super Off-Peak Day Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SRR', 'Super Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'VDS', 'Advance', 'NoFlexibility', 'VTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'VES', 'Advance', 'NoFlexibility', 'VTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GPR', 'Anytime Day Return', 'FullyFlexible', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GOR', 'Anytime Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OPD', 'Super Off-Peak Day Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AW1', '1st Advance', 'NoFlexibility', 'AWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SPA', 'Sleeper 1st Single', 'LimitedFlexibility', 'SCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SPB', 'Sleeper 1st Single', 'LimitedFlexibility', 'SCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BHO', 'Advance 1st', 'NoFlexibility', 'VTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BYS', 'Advance', 'NoFlexibility', 'GRSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BZS', 'Advance', 'NoFlexibility', 'GRSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OHS', 'Advance 1st', 'NoFlexibility', 'GRFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OJS', 'Advance 1st', 'NoFlexibility', 'GRFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AF1', 'Airport Advance 1st', 'NoFlexibility', 'TPFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AG2', 'Airport Advance Standard', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AH2', 'Airport Advance Standard', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AI2', 'Airport Advance Standard', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AJ2', 'Airport Advance Standard', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AK2', 'Airport Advance Standard', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AA1', 'Airport Advance 1st', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AB1', 'Airport Advance 1st', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AD1', 'Airport Advance 1st', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'AE1', 'Airport Advance 1st', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'ADT', 'Anytime Day Travelcard', 'FullyFlexible', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'ODT', 'Off-Peak Travelcard', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WRE', 'Super Off-Peak Day Travelcard', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FDT', 'Anytime Day Travelcard First', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'STO', 'Super Off-Peak Day Travelcard', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WDT', 'Super Off-Peak Day Travelcard', 'LimitedFlexibility', 'VTSAdvLeisure', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DTP', 'Off-Peak Day Travelcard Plus', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FTP', 'Off-Peak Day Travelcard Plus First', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OTF', 'Off-Peak Day Travelcard First', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2JC', 'Advance', 'NoFlexibility', 'CHSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1HS', '1st Advance', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'I1H', '1st Advance Breakfast', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SPC', 'Sleeper Standard Single', 'LimitedFlexibility', 'SCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SPI', 'Sleeper Standard Single', 'LimitedFlexibility', 'SCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SPE', 'Sleeper 1st Single', 'LimitedFlexibility', 'SCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FSX', 'Off-Peak Day First Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FRX', 'Off-Peak Day First Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FTX', 'Off-Peak Day Travelcard First', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'G1S', 'Off-Peak First Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'G1R', 'Off-Peak First Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'G2S', 'Off-Peak Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'G2R', 'Off-Peak Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'LFB', '1st Advance', 'NoFlexibility', 'VTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MFS', 'Advance', 'NoFlexibility', 'XCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MGS', 'Advance', 'NoFlexibility', 'XCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MHS', 'Advance', 'NoFlexibility', 'XCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MFF', '1st Advance', 'NoFlexibility', 'XCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MGF', '1st Advance', 'NoFlexibility', 'XCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'EGS', 'Super Off-Peak Day Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FAV', '1st Advance', 'NoFlexibility', 'SNFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FBV', '1st Advance', 'NoFlexibility', 'SNFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'FDV', '1st Advance', 'NoFlexibility', 'SNFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'UFA', '1st Advance', 'NoFlexibility', 'HTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'UFB', '1st Advance', 'NoFlexibility', 'HTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'UFC', '1st Advance', 'NoFlexibility', 'HTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'UFD', '1st Advance', 'NoFlexibility', 'HTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'UFE', '1st Advance', 'NoFlexibility', 'HTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'UFF', '1st Advance', 'NoFlexibility', 'HTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'UFG', '1st Advance', 'NoFlexibility', 'HTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'USA', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'USB', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'USC', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'USD', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'USE', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'USF', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'USG', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1SO', 'Super Off-Peak First Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DJS', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DKS', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DLS', '1st Advance', 'NoFlexibility', 'GWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WJS', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WKS', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WLS', 'Advance', 'NoFlexibility', 'GWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'POP', 'Pay as you Go Off-Peak (Oyster Smartcard)', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'PAP', 'Pay as you Go Peak (Oyster Smartcard)', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SOT', 'Super Off-Peak Day Travelcard', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'BOS', 'Advance', 'NoFlexibility', 'GRSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GC3', '1st Advance', 'NoFlexibility', 'GCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GC4', '1st Advance', 'NoFlexibility', 'GCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GC5', '1st Advance', 'NoFlexibility', 'GCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GC6', '1st Advance', 'NoFlexibility', 'GCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XQ1', '1st Park, 2 Dine & Go', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XQ2', '1st Park, 2 Dine & Go', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1AB', '1st Advance', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1BB', '1st Advance', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1CB', '1st Advance', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1SS', 'Super Off-Peak First Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'MSV', 'Merseyrail Day Saver', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OAS', '1st Advance', 'NoFlexibility', 'GRFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1BA', 'Business Anytime Plus', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F0R', 'Business Anytime', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'IDO', '1st London Day Out', 'NoFlexibility', 'EMFAdvLeisure', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F55', 'Club 55 Premier (1st, age 55 and over)', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F56', 'Club 55 Premier (1st, Age 55 and over, Senior Railcard Holder)', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'S55', 'Club 55 Standard (Age 55 and over)', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '55F', 'Club 55 First (Age 55 and over)', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '55S', 'Club 55 Standard (Age 55 and over)', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2FB', 'Advance', 'LimitedFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2GB', 'Advance', 'LimitedFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'CBB', 'Super Offpeak Single', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F5A', '1st Advance', 'NoFlexibility', 'SWFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'S5A', 'Advance', 'NoFlexibility', 'SWSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1ST', 'Super Off-Peak Day Travelcard First', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'STP', 'Super Off-Peak Day Travelcard', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'NOR', 'Northern Rail Only Day Ranger', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCA', '1st Advance', 'NoFlexibility', 'GCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCB', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCH', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'CMR', 'Off-Peak Cracker', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '55W', 'Club 55 Standard - Not Fridays (Age 55 and over)', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'C2S', 'Advance', 'NoFlexibility', 'LMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'C4S', 'Advance', 'NoFlexibility', 'LMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F2V', '1st Advance', 'NoFlexibility', 'LMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'F4V', '1st Advance', 'NoFlexibility', 'LMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SAQ', 'Advance Sleeper Solo (no TV)', 'NoFlexibility', 'GWSAdvLeisureSGL', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCE', '1st Advance', 'NoFlexibility', 'GCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GC7', '1st Advance', 'NoFlexibility', 'GCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GC8', '1st Advance', 'NoFlexibility', 'GCFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCD', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCG', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCQ', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCJ', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GCV', 'Advance', 'NoFlexibility', 'GCSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OPF', 'Off-Peak Day First Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SOF', 'Super Off-Peak Day First Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SOA', 'Super Off-Peak Day Single', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SOB', 'Super Off-Peak Day Return', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WTC', 'Super Off-Peak Day Travelcard', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XS5', 'Scottish Executive Package', 'LimitedFlexibility', 'GRSAdvBusiness', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'XS6', 'Scottish Executive Package', 'LimitedFlexibility', 'GRSAdvBusiness', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'EPT', 'Evening Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'JBA', 'Jubilee 1st Advance Return', 'NoFlexibility', 'EMFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'JBB', 'Jubilee Advance Return', 'NoFlexibility', 'EMSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'JBC', 'Jubilee Anytime First Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'JBD', 'Jubilee Anytime Return', 'FullyFlexible', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'SMG', 'Skegness Promotion', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'VER', 'Valley Evening Return', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '55P', 'Club 55 Standard Any Day (Age 55 and over)', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DG1', 'Advance', 'NoFlexibility', 'NTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DG2', 'Advance', 'NoFlexibility', 'NTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'DG3', 'Advance', 'NoFlexibility', 'NTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'NN2', 'Daytripper', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'GM6', 'Greater Manchester Wayfarer', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'UFH', '1st Advance', 'NoFlexibility', 'HTFAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'USH', 'Advance', 'NoFlexibility', 'HTSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '1PS', 'Cardiff Day', 'LimitedFlexibility', 'NoGroup', 'true'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'EMB', 'Business Standard', 'LimitedFlexibility', 'EMSAdvBusiness', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WK2', 'Weekender', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'WK1', 'Weekender First', 'LimitedFlexibility', 'NoGroup', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', 'OSB', 'Greater Anglia Business', 'LimitedFlexibility', 'LESAdvBusiness', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2FF', 'Advance', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2GF', 'Advance', 'NoFlexibility', 'TPSAdvLeisure', 'false'
EXEC INFAddUpdateCategorisedHashes 'DisplayableRailTickets', '2HF', 'Advance', 'NoFlexibility', 'TPSAdvLeisure', 'false'



GO




----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3) = 1.30


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 001 and VersionNumber = @@value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Updates in respect of the National Rail fares revision on 18 May 2014'
	WHERE ScriptNumber = 001 AND VersionNumber = @@value
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
		001,
		@@value,
		'Updates in respect of the National Rail fares revision on 18 May 2014'
	)
END