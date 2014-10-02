-- ***********************************************
-- AUTHOR      	: Mark Turner
-- NAME 	: MDS003_HomepageText.sql
-- DESCRIPTION 	: Updates the homepage text.
-- SOURCE 	: TDP Apps Support
-- Version	: $Revision:   1.47  $
-- ************************************************
--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS003_HomepageText.sql-arc  $   
--
-- 1.48 MModi 12/02/2014 
-- Added Flooding seasonal message
--
--   Rev 1.47   May 01 2013 08:46:08   PScott
--IR 5929  Cookies Message and Accessibility FAQ changes
--Resolution for 5929: Cookies Message and Accessibility FAQ changes
--
--   Rev 1.46   Apr 30 2013 16:33:18   PScott
--Temporary message for First buses in South essex
--
--   Rev 1.45   Jan 04 2013 13:25:54   nrankin
--Updated Seasonal Notice Board info for 2013
--
--MDS003_HomepageText.sql 
--Resolution for 5879: Updated Seasonal Notice Board info for 2013
--
--   Rev 1.44   Aug 23 2012 15:13:28   PScott
--Cookies policy
--Resolution for 5829: HomePage/SpecialNoticeBoard Cookies Policy
--
--   Rev 1.43   May 09 2012 11:27:48   nrankin
--Amend Hompage message to remove West midlands forn MRMD and add it to South West for correct 'region' alerting 
--
--   Rev 1.42   Apr 02 2012 11:33:50   nrankin
--Amendment - Updated Seasonal Notice Board info for 2012
--Resolution for 5800: Amendment - Updated Seasonal Notice Board info for 2012
--
--   Rev 1.41   Mar 02 2012 15:06:14   nrankin
--Amendment - Updated Seasonal Notice Board info for 2012
--Resolution for 5796: Amendment - Updated Seasonal Notice Board info for 2012
--
--   Rev 1.40   Feb 28 2012 12:10:26   PScott
--UKC4792266 - remove metrolink message
--
--   Rev 1.39   Feb 24 2012 15:40:44   PScott
--Emergency change C4789219
--
--   Rev 1.38   Jan 05 2012 13:08:16   nrankin
--Updated "AUTO" Seasonal Notice Board info for 2012 has been supplied by the DFT.
--Resolution for 5778: Updated Seasonal Notice Board info for 2012
--
--   Rev 1.37   Jan 05 2012 12:56:56   nrankin
--Updated "AUTO" Seasonal Notice Board info for 2012 has been supplied by the DFT.
--
--Resolution for 5778: Updated Seasonal Notice Board info for 2012
--
--   Rev 1.36   Dec 13 2011 12:56:24   nrankin
--Remove SW soft content from home page text
--Resolution for 5774: Remove SW soft content from home page text
--
--   Rev 1.35   Sep 16 2011 09:44:20   DLane
--WAI additional changes
--Resolution for 5738: WAI additional changes
--
--   Rev 1.34   Aug 09 2011 11:47:44   DLane
--WAI fixes
--Resolution for 5722: Fixes to WAI changes
--
--   Rev 1.33   Mar 08 2011 16:17:10   rbroddle
--Updated for Scotland inclusion in MRMD
--
--   Rev 1.32   Feb 15 2011 10:36:58   nrankin
--Slight amend to SNBT for 2011.
--Resolution for 5672: Seasonal Notice Board Timetable for 2011
--
--   Rev 1.31   Feb 03 2011 14:30:56   nrankin
--Seasonal Notice Board Timetable for 2011
--Resolution for 5672: Seasonal Notice Board Timetable for 2011
--
--   Rev 1.30   Oct 28 2010 15:59:24   nrankin
--Update Seasonal Message - TFL Xmas Info. Amend Seasonal Message Board so to link to new Christmas info (and PDF's) from the 29th October rather than the 24th November (C3455293). 
--
--
--Resolution for 5626: Update Seasonal Message - TFL Xmas Info
--
--   Rev 1.29   Jul 27 2010 10:53:56   nrankin
--XHTML compliance updates for seasonal
--
--   Rev 1.28   Jun 15 2010 16:51:58   PScott
--Added RTTI outage message on 15/6/10
--
--   Rev 1.27   May 27 2010 13:05:40   RHopkins
--Add NRS outage message
--Resolution for 5540: NRS outage alert 29/05/2010 - 30/05/2010
--
--   Rev 1.26   May 11 2010 10:15:56   nrankin
--Seasonal Notice Board - Auto updates until 5th Jan 2011.
--
--   Rev 1.25   May 04 2010 09:23:18   nrankin
--4th May  Amendment
--
--   Rev 1.24   Apr 06 2010 09:45:10   nrankin
--Amend Seasonal Notice board (Thursday 6th April) as per Timetable
--
--   Rev 1.23   Mar 31 2010 13:48:20   nrankin
--Amend Seasonal Notice board (Thursday 1st April) as per Timetable
--
--   Rev 1.22   Mar 22 2010 16:23:42   pghumra
--Added line break after each homepage article for right hand menu in order to display properly with linearised tables.
--Resolution for 5475: CODEFIX - NEW - Del 10.x - Issue with linearised home page
--
--   Rev 1.21   Jan 27 2010 09:59:54   nrankin
--Add warning about Easter weekend (2nd to 5th April) - 1st Feb 10
--
--   Rev 1.20   Jan 20 2010 17:06:26   mmodi
--XHTML compliance updates
--
--   Rev 1.19   Jan 08 2010 13:38:34   nrankin
--Removed Seasonal Notice Board link for 5th January 2010 as per agreed process
--
--   Rev 1.18   Dec 14 2009 14:15:56   nrankin
--C2359425
--
--   Rev 1.17   Nov 24 2009 16:09:40   nrankin
--Seasonal Notice Board link added to homepage message
--
--   Rev 1.16   Sep 29 2009 15:27:06   nrankin
--Add warning about Christmas period - 29th Septmber 09
--
--   Rev 1.15   Aug 07 2009 10:10:50   nrankin
--amended
--
--   Rev 1.14   Aug 03 2009 11:32:46   nrankin
--August bank hol message amends (Tuesday 4 August). Remove Scotland BH text - slight amend
--
--   Rev 1.13   Aug 03 2009 11:01:42   nrankin
--August bank hol message amends (Tuesday 4 August). Remove Scotland BH text
--
--   Rev 1.12   Jul 24 2009 16:18:14   nrankin
--August bank hol message amends (Fr1 31st July). Link to seasonal notice board
--
--   Rev 1.11   Jun 24 2009 12:00:44   pscott
--August bank hol message adjustment (Scotland running normally msg)
--
--   Rev 1.10   Jun 01 2009 09:34:36   jfrank
--Removed link from August Bank Holiday message.
--
--   Rev 1.9   May 19 2009 15:19:46   nrankin
--Script to remove link and message for Seasonal Notice Board (26th May). Note : 2nd June text already added - just marked as not displaying!
--
--   Rev 1.8   Apr 29 2009 13:21:34   nrankin
--Seasonal Notice Board - Change wording to remove reference to 4th May
--
--   Rev 1.7   Apr 28 2009 10:15:50   jfrank
--Update alert message for SELMA
--
--   Rev 1.6   Apr 07 2009 13:57:58   nrankin
--Seasonal Notice Board - Change wording to remove reference to Easter on the 14th April
--
--   Rev 1.5   Mar 25 2009 13:54:44   nrankin
--AMENDS -Update to amend Seasonl Notice Board link to be applied on the 6th April.
--
--   Rev 1.4   Mar 25 2009 13:43:32   nrankin
--Update to amend Seasonl Notice Board link to be applied on the 6th April.
--
--   Rev 1.3   Dec 10 2008 15:47:50   rbroddle
--Added Seasonal Noticeboard bit back in as this needs to be in one script - otherwise we lose rows when one or other MDS script is run!!!!
--
--   Rev 1.2   Oct 31 2008 11:02:26   sangle
--Commented out the seasonal section (3) as this functionality has been relocated to MDS015.
--
--   Rev 1.1   Oct 30 2008 15:31:36   mturner
--Manually merged because of failure in automatic merge
--
--   Rev 1.0.1.0   Oct 16 2008 17:24:28   mturner
--Updated to make messages XHTML compliant
--Resolution for 5146: WAI AAA copmpliance work (CCN 474)
--
--   Rev 1.0   Nov 08 2007 12:42:26   mturner
--Initial revision.
--
--   Rev 1.16   Sep 13 2007 13:36:58   nrankin
---- Additional Steps Required : Update C02 RHM text (for DEL9.7) CCN409
--
--Remove the folowing text....
-- " on the 'Details' button and then"
-- " which is just below your list of options "
--
--   Rev 1.15   Jun 13 2007 14:11:16   nrankin
--IR4410
--
--Yet another amend by DfT for RHM Homepage
--
--Change "selected" to "select"
--
--Resolution for 4220: Rail Search by Price
--Resolution for 4410: Add CO2 Icon and Home Page right hand menu text changes
--
--   Rev 1.14   Jun 07 2007 10:20:06   nrankin
--IR 4410 - Additonal changes requested by Kirsty Gibson to right hand menu
--Resolution for 4410: Add CO2 Icon and Home Page right hand menu text changes
--
--   Rev 1.13   Jun 01 2007 09:22:38   nrankin
--IR4410 - CCN391
--
--   Rev 1.12   May 24 2007 16:22:50   nrankin
--IR4410
--
--Right Hand menu text changes (Car Park amends and C02 addition - removal of  "On a Budget")
--Resolution for 4410: Add CO2 Icon and Home Page right hand menu text changes
--
--   Rev 1.12   May 24 2007 14:26:58   NRankin
--Update to car parks text / removal of On A Budget / C02 Emissions addition required for del 9.6
--
--
--   Rev 1.11   Oct 18 2006 14:26:58   jfrank
--Update to car parks text required for del 9.0
--
--   Rev 1.10   Oct 10 2006 14:07:22   jfrank
--Soft content change to homepage for DEL 9.0.
--Resolution for 4219: Find a Car Park - Soft content changes for the homepage
--
--   Rev 1.9   May 02 2006 16:40:14   CRees
--Welsh text added for 8.1, removed references to Inspirational Places, changed Door-to-door reference to Plan a journey.
--
--   Rev 1.8   Apr 26 2006 17:03:52   CRees
--Update to Free Tools section, following Vantive 4252862. This version of the message should be applied to Del 8.1 and above.
--
--   Rev 1.7   Apr 11 2006 13:39:22   CRees
--Updates to Homepage requested by Kirsty Gibson. Removes Welsh from Green RH panel, as no translation available. This is as requested by KG.
--
--   Rev 1.6   Feb 17 2006 15:17:44   scraddock
--corrected typo in sql
--
--   Rev 1.5   Feb 17 2006 15:05:26   scraddock
--Update promotional panel welsh text from TT038 del8
--
--   Rev 1.4   Feb 06 2006 10:34:06   scraddock
--Updated welsh text to translate what's new? - Beth sy'n newydd?
--
--   Rev 1.3   Feb 02 2006 15:10:28   CRees
--Added auto CMS update code, and removed Tips and Tools section - in DUP script instead. DUPLICATE CHECK-IN (Previous check-in branched to 1.2.1.0)
--
--   Rev 1.2   Jan 24 2006 14:54:14   pcross
--Entered translation for "Plan a day trip to two destinations with our new Day trip planner, on the Door-to-door page."
--
--   Rev 1.1   Jan 24 2006 12:36:20   scraddock
--Partial update for additional welsh from doc TT03604.
--
--   Rev 1.0   Jan 17 2006 16:22:34   AViitanen
--Initial revision.



USE PermanentPortal

SET NOEXEC OFF
SET ANSI_WARNINGS ON
SET XACT_ABORT ON
SET IMPLICIT_TRANSACTIONS OFF
SET NOCOUNT ON

BEGIN TRAN
GO

-- ***************************************************
-- *********SEASONAL NOTICEBOARD TEMPLATES ***********
-- ***************************************************
-- WITHOUT LINK: 	'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">TEXT GOES HERE</td></tr>',
-- WITH LINK:		'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">TEXT GOES HERE</a></td></tr>',
-- ***************************************************



-- =======================================================
-- Synchronization Script for: [dbo].[HomePageMessage]
-- =======================================================
Print 'Synchronization Script for: [dbo].[HomePageMessage]'

EXEC sp_executesql N'
ALTER TABLE [dbo].[HomePageMessage] NOCHECK CONSTRAINT ALL
ALTER TABLE [dbo].[HomePageMessage] DISABLE TRIGGER ALL'

UPDATE HomePageMessage
SET valueFR = NULL
GO

UPDATE HomePageMessage
SET valueEN = NULL
GO

UPDATE HomePageMessage
SET valueCY = NULL
GO

ALTER TABLE HomePageMessage
    ALTER COLUMN valueFR varchar(1)
GO

ALTER TABLE HomePageMessage
    ALTER COLUMN valueEN varchar(3900)
GO

ALTER TABLE HomePageMessage
    ALTER COLUMN valueCY varchar(3900)
GO

DELETE FROM [dbo].[HomepageMessage]



INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(0, N'Header', 1, 1, NULL, NULL, 
N'<div class="Column3Header"><div class="txtsevenbbl">Latest...</div><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div><div class="Column3Content"><table cellspacing="0" cellpadding="2" width="100%" border="0"><tbody>', 
N'<div class="Column3Header"><div class="txtsevenbbl">Latest...</div><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div><div class="Column3Content"><table cellspacing="0" cellpadding="2" width="100%" border="0"><tbody>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(1, N'RTTI Outage', 1, 0, NULL, NULL, 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Our link to real time train information is undergoing routine maintenance this evening between 23:00 and 00:00. Real time departure board information usually provided via our mobile service and station information pages will not be available during this time.<br/></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Our link to real time train information is undergoing routine maintenance this evening between 23:00 and 00:00. Real time departure board information usually provided via our mobile service and station information pages will not be available during this time<br/></td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(2, N'Flooding', 1, 1, '2014-04-01 23:59:59', '2014-04-02 23:59:59',
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21"/></td><td class="txtsevenb">Known changes to rail services in response to flooding are on our <a href="/web2/SeasonalNoticeBoard.aspx">seasonal page.</a><br /></td></tr>',
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21"/></td><td class="txtsevenb">Known changes to rail services in response to flooding are on our <a href="/web2/SeasonalNoticeBoard.aspx">seasonal page.</a><br /></td></tr>',
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(3, N'Special Notice Board', 1, 1, NULL, NULL, 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21"/></td><td class="txtseven"> Transport Direct uses cookies.<a href="/web2/SpecialNoticeBoard.aspx"> For more information on our cookies, or how to opt out, click here.</a> Your continued use of this site is taken as acceptance of this.<br /></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21"/></td><td class="txtseven"> Transport Direct uses cookies.<a href="/web2/SpecialNoticeBoard.aspx"> For more information on our cookies, or how to opt out, click here.</a> Your continued use of this site is taken as acceptance of this.<br /></td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES
(4, N'South Essex', 1, 1, '2013-04-30 00:00:00.000', '2013-05-02 00:00:00.000', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">South Essex: We are missing First buses from 6 May.  Service changes are <a href="http://www.firstgroup.com/ukbus/essex/travel_news/service_updates/?item=8011&conf=0" target="_child">here</a> and timetables <a href="http://www.firstgroup.com/ukbus/essex/journey_planning/timetables/index.php?operator=8&page=1&redirect=no" target="_child">here</a>.  We will rectify this issue as soon as possible.',
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">South Essex: We are missing First buses from 6 May.  Service changes are <a href="http://www.firstgroup.com/ukbus/essex/travel_news/service_updates/?item=8011&conf=0" target="_child">here</a> and timetables <a href="http://www.firstgroup.com/ukbus/essex/journey_planning/timetables/index.php?operator=8&page=1&redirect=no" target="_child">here</a>.  We will rectify this issue as soon as possible.', 
N'')

--INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
--(5, N'Seasonal', 1, 0, '2014-02-03 00:00:00', '2014-03-02 23:59:59', 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Public transport information for the Easter weekend (18th April - 21st April) may not be correct at present. Please re-check nearer the time.</td></tr>', 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Public transport information for the Easter weekend (18th April - 21st April) may not be correct at present. Please re-check nearer the time.</td></tr>', 
--N'')

--INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
--(6, N'Seasonal', 1, 0, '2014-03-03 00:00:00', '2014-03-17 23:59:59', 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Public transport information for the Easter and May bank holidays (18th April - 21st April, 5th &nbsp; 26th May) may not be correct at present. Please re-check nearer the time.</td></tr>', 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Public transport information for the Easter and May bank holidays (18th April - 21st April, 5th &nbsp; 26th May) may not be correct at present. Please re-check nearer the time.</td></tr>', 
--N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(7, N'Seasonal', 1, 0, '2014-03-18 00:00:00', '2014-04-21 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for information about changes to public transport services for the Easter weekend (18th April - 21st April).</a><br/>Public transport information for the May bank holiday (5th &nbsp; 26th May), may not be correct at present. Please re-check nearer the time.</td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for information about changes to public transport services for the Easter weekend (18th April - 21st April).</a><br/>Public transport information for the May bank holiday (5th &nbsp; 26th May), may not be correct at present. Please re-check nearer the time.</td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(8, N'Seasonal', 1, 0, '2014-04-22 00:00:00', '2014-05-05 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for information about changes to public transport services for the May bank holidays (5th &nbsp; 26th May).</a></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for information about changes to public transport services for the May bank holidays (5th &nbsp; 26th May).</a></td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(9, N'Seasonal', 1, 0, '2014-05-06 00:00:00', '2014-05-26 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for information about changes to public transport services for the spring bank holiday (26th May).</a></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for information about changes to public transport services for the spring bank holiday (26th May).</a></td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(10, N'Seasonal', 1, 0, '2014-06-02 00:00:00', '2014-07-03 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Public transport information for the August bank holidays on the 4th (Scotland only) and 25th, may not be correct at present. Please re-check nearer the time</td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Public transport information for the August bank holidays on the 4th (Scotland only) and 25th, may not be correct at present. Please re-check nearer the time</td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(11, N'Seasonal', 1, 0, '2014-07-04 00:00:00', '2014-07-24 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Services will run normally throughout Scotland on the Scottish bank holiday, Monday 4th August.<br/>Public transport information for bank holiday Monday 25th August may not be correct at present. Please re-check nearer the time.</td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Services will run normally throughout Scotland on the Scottish bank holiday, Monday 4th August.<br/>Public transport information for bank holiday Monday 25th August may not be correct at present. Please re-check nearer the time.</td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(12, N'Seasonal', 1, 0, '2014-07-25 00:00:00', '2014-08-04 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Services will run normally throughout Scotland on the Scottish bank holiday, Monday 4th August.<br/><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for information about changes to public transport services for bank holiday Monday 25th August.</a></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Services will run normally throughout Scotland on the Scottish bank holiday, Monday 4th August.<br/><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for information about changes to public transport services for bank holiday Monday 25th August.</a></td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(13, N'Seasonal', 1, 0, '2014-08-05 00:00:00', '2014-08-25 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for information about changes to public transport services for the bank holiday Monday 25th August. </a></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for information about changes to public transport services for the bank holiday Monday 25th August.</a></td></tr>', 
N'')

--INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
--(13, N'EastAnglia', 1, 0, NULL, NULL, 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in East Anglia.  We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in East Anglia.  We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
--N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(14, N'Seasonal', 1, 0, '2014-09-01 00:00:00', '2014-09-30 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Services will run normally throughout Scotland on the Scottish bank holiday, Sunday 30th November.</td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Services will run normally throughout Scotland on the Scottish bank holiday, Sunday 30th November.</td></tr>', 
N'')

--INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
--(14, N'EastMidlands', 1, 0, NULL, NULL, 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in the East Midlands including Derby and Nottingham.  We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in the East Midlands including Derby and Nottingham.  We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
--N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(15, N'Seasonal', 1, 0, '2014-10-01 00:00:00', '2014-11-24 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Public transport information for the Christmas and New Year period may not be correct at present. Please re-check nearer the time.<br/>Services will run normally throughout Scotland on the Scottish bank holiday, Sunday 30th November.</td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven">Public transport information for the Christmas and New Year period may not be correct at present. Please re-check nearer the time.<br/>Services will run normally throughout Scotland on the Scottish bank holiday, Sunday 30th November.</td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(16, N'Seasonal', 1, 0, '2014-11-25 00:00:00', '2014-11-30 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for the latest information on changes to public transport services over the Christmas and New Year holiday period.</a><br/>Services will run normally throughout Scotland on the Scottish bank holiday Sunday 30th November.</td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for the latest information on changes to public transport services over the Christmas and New Year holiday period.</a><br/>Services will run normally throughout Scotland on the Scottish bank holiday Sunday 30th November.</td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(17, N'Seasonal', 1, 0, '2014-12-01 00:00:00', '2014-01-02 23:59:59', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for the latest information on changes to public transport services over the Christmas and New Year holiday period.</a></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtseven"><a href="/web2/SeasonalNoticeBoard.aspx">Please click here for the latest information on changes to public transport services over the Christmas and New Year holiday period.</a></td></tr>', 
N'')

--INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
--(18, N'SouthWest', 1, 0, NULL, NULL, 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in the following areas:-<br>  &bull;  South West England including Hampshire<br> We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in the following areas:-<br>  &bull;  South West England including Hampshire<br> We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
--N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(19, N'SouthEast', 1, 0, NULL, NULL, 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in the following areas:-<br>  &bull;  London<br>  &bull;  South East of England including the Isle of Wight<br>  &bull;  South West England including Hampshire<br>   &bull;  East Midlands including Derby and Nottingham<br>  &bull;  East Anglia<br>  &bull;  West Midlands<br> We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in the following areas:-<br>  &bull;  London<br>  &bull;  South East of England including the Isle of Wight<br>   &bull;  East Midlands including Derby and Nottingham<br>  &bull;  East Anglia<br>  &bull;  West Midlands<br> We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(20, N'MRMD', 1, 0, NULL, NULL, 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in the following areas:-<br>  &bull;  Yorkshire<br>   &bull;  Wales<br>  &bull;  Scotland<br>  &bull;  North West of England including Liverpool and Manchester<br> We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in the following areas:-<br>  &bull;  Yorkshire<br>   &bull;  Wales<br>  &bull;  North West of England including Liverpool and Manchester<br> We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
N'')

--INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
--(20, N'Scotland', 1, 0, NULL, NULL, 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in Scotland.  We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
--N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in Scotland.  We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
--N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(21, N'AllRegion', 1, 0, NULL, NULL, 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are working to fix a problem affecting local public transport information in our Door to Door planner.  If you require train times, driving directions or domestic flight schedules, please use the appropriate ‘Find a…’ planner<br/></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are working to fix a problem affecting local public transport information in our Door to Door planner.  If you require train times, driving directions or domestic flight schedules, please use the appropriate ‘Find a…’ planner<br/></td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(22, N'NorthEast', 1, 0, '2010-12-24 03:59:48', '2010-12-24 04:47:32', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in the North East of England and Cumbria / Lake District.  We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about local public transport services in the North East of England and Cumbria / Lake District.  We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(23, N'NCSD', 1, 0, NULL, NULL, 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about long-distance coach journeys (using either the Door-to-Door planner or ''Find a Coach''). We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
N'<tr><td class="VertAlignTop"><img class="imageAttention" src="/Web2/App_Themes/{Theme_Name}/images/gifs/exclamation.gif" alt=" " border="0" width="26" height="21" /></td><td class="txtsevenbred">We are currently unable to provide information about long-distance coach journeys (using either the Door-to-Door planner or ''Find a Coach''). We are working to fix this problem, which will be resolved shortly.<br/></td></tr>', 
N'')

INSERT INTO [dbo].[HomePageMessage] ([SeqNo], [Description], [Changed], [Display], [DisplayFrom], [DisplayUntil], [valueEN], [valueCY], [valueFR]) VALUES 
(30, N'Footer', 1, 1, NULL, NULL, 
N'</tbody></table></div>', 
N'</tbody></table></div>', 
N'')



EXEC sp_executesql N'
ALTER TABLE [dbo].[HomePageMessage] CHECK CONSTRAINT ALL
ALTER TABLE [dbo].[HomePageMessage] ENABLE TRIGGER ALL'


COMMIT
GO

SET NOEXEC OFF
GO







----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @value decimal(7,3)
Select @value = 1.480


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 003 and VersionNumber = @value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Added Flooding seasonal message'
	WHERE ScriptNumber = 003 AND VersionNumber = @value
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
		003,
		@value,
		'Added Flooding seasonal message'
	)
END
