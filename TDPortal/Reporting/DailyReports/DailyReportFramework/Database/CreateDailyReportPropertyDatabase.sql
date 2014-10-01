-- ***********************************************
-- NAME 	: CreateDailyReportPropertyDatabase.sql
-- DESCRIPTION 	: Creates Reporting database, users & roles
-- ************************************************
--
-- 15/07/04 - JPS - Adjust Email lengths (value column) and add mail recipients
--
-- $Log:   P:/TDPortal/archives/Reporting/CreateDailyReportPropertyDatabase.sql-arc  $
--
--   Rev 1.1   Mar 06 2008 10:14:16   pscott
--No change.
--
--   Rev 1.0   Jun 19 2006 16:14:24   PScott
--Initial revision.
--
--   Rev 1.0   Jun 19 2006 16:09:48   PScott
--Initial revision.
--
--   Rev 1.0   Jun 19 2006 16:01:48   PScott
--Initial revision.

USE [master]

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'ReportProperties')
	DROP DATABASE [ReportProperties]
GO

CREATE DATABASE [ReportProperties]
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'ReportProperties', N'torn page detection', N'true'
GO

exec sp_dboption N'ReportProperties', N'autoshrink', N'true'
GO

exec sp_dboption N'ReportProperties', N'ANSI null default', N'true'
GO

exec sp_dboption N'ReportProperties', N'ANSI nulls', N'true'
GO

exec sp_dboption N'ReportProperties', N'ANSI warnings', N'true'
GO

exec sp_dboption N'ReportProperties', N'auto create statistics', N'true'
GO

exec sp_dboption N'ReportProperties', N'auto update statistics', N'true'
GO

if( ( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) ) or ( (@@microsoftversion / power(2, 24) = 7) and (@@microsoftversion & 0xffff >= 1082) ) )
	exec sp_dboption N'ReportProperties', N'db chaining', N'false'
GO


EXEC sp_configure 'remote proc trans', '1'
GO

RECONFIGURE WITH OVERRIDE
GO


USE [ReportProperties]
GO

-- CREATE Permission access for TNG Service account

-- DECLARE @TNGUser nvarchar(100)
-- SELECT @TNGUser=MISServerName + '\-service-tng' from master.dbo.Environment

-- if not exists (select * from dbo.sysusers where name = N'TNGUser' and uid < 16382)
--	EXEC sp_grantdbaccess @TNGUser, N'-service-tng'
-- GO

-- exec sp_addrolemember N'db_owner', N'-service-tng'
-- GO


if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReportProperties]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ReportProperties]
GO

-- create properties table
CREATE TABLE [dbo].[ReportProperties] (
	[reportNumber] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[propertykey] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[value] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS) ON [PRIMARY]
GO

GO

 CREATE  INDEX [IX_ReportProperties] ON [dbo].[ReportProperties]([reportNumber], [propertyKey]) ON [PRIMARY]
GO


ALTER TABLE[dbo].[ReportProperties] ADD CONSTRAINT
	PK_properties PRIMARY KEY CLUSTERED 
	(
	reportNumber,
	propertyKey
	) ON [PRIMARY]

GO


Insert Into ReportProperties Values('0', 'DefaultInjector', 'INJ01')
Insert Into ReportProperties Values('0', 'BackupInjector', 'INJ03')
Insert Into ReportProperties Values('0', 'CurrentCapacityBand', '4')
Insert Into ReportProperties Values('0', 'DefaultReportTime', '09:00:00')
Insert Into ReportProperties Values('0', 'MailAddress', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net')
Insert Into ReportProperties Values('0', 'NumberOfReports', '27')
Insert Into ReportProperties Values('0', 'RDILastRun', 'Reset19/06/2013')
Insert Into ReportProperties Values('0', 'smtpServer', '156.150.218.77')
Insert Into ReportProperties Values('0', 'TravelineInjector', 'INJ02')
Insert Into ReportProperties Values('1', 'FilePath', 'C:\DailySurgeReport.txt')
Insert Into ReportProperties Values('1', 'Frequency', 'D')
Insert Into ReportProperties Values('1', 'LastRan', '12/03/2009')
Insert Into ReportProperties Values('1', 'MailRecipient', 'tdpsupport@atos.net;BBPCapacityPlanning@atos.net;Craig.stocken@atos.net')
Insert Into ReportProperties Values('1', 'Title', 'Daily Surge Report')
Insert Into ReportProperties Values('2', 'FilePath', 'C:\DailyMapActivityReport %YY-MM-DD%.txt')
Insert Into ReportProperties Values('2', 'Frequency', 'D')
Insert Into ReportProperties Values('2', 'LastRan', '12/03/2009')
Insert Into ReportProperties Values('2', 'MailRecipient', 'john.scott@atos.net;tdpsupport@atos.net')
Insert Into ReportProperties Values('2', 'Title', 'Daily Map Activity Report %YY-MM-DD%')
Insert Into ReportProperties Values('3', 'FilePath', 'C:\MonthlyTransactionalMixReport %YY-MM%.csv')
Insert Into ReportProperties Values('3', 'Frequency', 'M')
Insert Into ReportProperties Values('3', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('3', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('3', 'Title', 'Monthly Transactional Mix Report %YY-MM%')
Insert Into ReportProperties Values('4', 'FilePath', 'C:\DFTWeeklyReport %YY-MM-DD%.csv')
Insert Into ReportProperties Values('4', 'Frequency', 'W')
Insert Into ReportProperties Values('4', 'LastRan', '09/03/2009')
Insert Into ReportProperties Values('4', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;BBPCapacityPlanning@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('4', 'Title', 'DFT Weekly Report %YY-MM-DD%')
Insert Into ReportProperties Values('5', 'FilePath', 'C:\MonthlyAvailabilityReport %YY-MM%.csv')
Insert Into ReportProperties Values('5', 'Frequency', 'M')
Insert Into ReportProperties Values('5', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('5', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('5', 'Title', 'Monthly Availability Report %YY-MM%')
Insert Into ReportProperties Values('6', 'FilePath', 'C:\MonthlySurgeReport %YY-MM%.csv')
Insert Into ReportProperties Values('6', 'Frequency', 'M')
Insert Into ReportProperties Values('6', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('6', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('6', 'Title', 'Monthly Surge Report %YY-MM%')
Insert Into ReportProperties Values('7', 'FilePath', 'C:\MonthlyMappingAvailabilityReport %YY-MM%.csv')
Insert Into ReportProperties Values('7', 'Frequency', 'M')
Insert Into ReportProperties Values('7', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('7', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('7', 'Title', 'Monthly Mapping Availability Report %YY-MM%')
Insert Into ReportProperties Values('8', 'FilePath', 'C:\MonthlyTicketingReport %YY-MM%.csv')
Insert Into ReportProperties Values('8', 'Frequency', 'M')
Insert Into ReportProperties Values('8', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('8', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('8', 'Title', 'Monthly Ticketing Report %YY-MM%')
Insert Into ReportProperties Values('9', 'FilePath', 'C:\MonthlyAverageResponseReport %YY-MM%.csv')
Insert Into ReportProperties Values('9', 'Frequency', 'M')
Insert Into ReportProperties Values('9', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('9', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('9', 'Title', 'Monthly Average Response Report %YY-MM%')
Insert Into ReportProperties Values('10', 'FilePath', 'C:\MonthlyTransactionPerformanceReport %YY-MM%.csv')
Insert Into ReportProperties Values('10', 'Frequency', 'M')
Insert Into ReportProperties Values('10', 'LastRan', '01/04/2009')
Insert Into ReportProperties Values('10', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net')
Insert Into ReportProperties Values('10', 'Title', 'Monthly Transaction Performance Report %YY-MM%')
Insert Into ReportProperties Values('11', 'FilePath', 'C:\MonthlyWebPageAndMapUsage %YY-MM%.csv')
Insert Into ReportProperties Values('11', 'Frequency', 'M')
Insert Into ReportProperties Values('11', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('11', 'MailRecipient', 'tdpsupport@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('11', 'Title', 'Monthly Web Page And Map Usage Report %YY-MM%')
Insert Into ReportProperties Values('12', 'FilePath', 'C:\MonthlyMapTransacationsReport %YY-MM%.csv')
Insert Into ReportProperties Values('12', 'Frequency', 'M')
Insert Into ReportProperties Values('12', 'LastRan', '23/11/2008')
Insert Into ReportProperties Values('12', 'MailRecipient', 'tdpsupport@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('12', 'Title', 'Monthly Map Transactions Report %YY-MM%')
Insert Into ReportProperties Values('13', 'FilePath', 'C:\MonthlyUserFeedbackReport %YY-MM%.csv')
Insert Into ReportProperties Values('13', 'Frequency', 'M')
Insert Into ReportProperties Values('13', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('13', 'MailRecipient', 'tdpsupport@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('13', 'Title', 'MonthlyUserFeedbackReturn %YY-MM%')
Insert Into ReportProperties Values('14', 'FilePath', 'C:\MonthlyWeightedTransactionReport %YY-MM%.csv')
Insert Into ReportProperties Values('14', 'Frequency', 'M')
Insert Into ReportProperties Values('14', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('14', 'MailRecipient', 'john.scott@atos.net')
Insert Into ReportProperties Values('14', 'Title', 'Monthly Weighted Transaction Report %YY-MM%')
Insert Into ReportProperties Values('15', 'FilePath', 'C:\DailyWeightedTransactionReport %YY-MM-DD%.csv')
Insert Into ReportProperties Values('15', 'Frequency', 'D')
Insert Into ReportProperties Values('15', 'LastRan', '12/03/2009')
Insert Into ReportProperties Values('15', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net')
Insert Into ReportProperties Values('15', 'Title', 'Daily Weighted Transaction Report %YY-MM-DD%')
Insert Into ReportProperties Values('16', 'FilePath', 'C:\NewRefTransExtract %YY-MM-DD%.csv')
Insert Into ReportProperties Values('16', 'Frequency', 'D')
Insert Into ReportProperties Values('16', 'LastRan', '26/11/2008')
Insert Into ReportProperties Values('16', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net')
Insert Into ReportProperties Values('16', 'Title', 'New Ref Trans Extract %YY-MM-DD%')
Insert Into ReportProperties Values('17', 'FilePath', 'C:\WeekTicketingReport %YY-MM-DD%.csv')
Insert Into ReportProperties Values('17', 'Frequency', 'W')
Insert Into ReportProperties Values('17', 'LastRan', '12/03/2009')
Insert Into ReportProperties Values('17', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('17', 'Title', 'Weekly Ticketing Report %YY-MM-DD%')
Insert Into ReportProperties Values('18', 'FilePath', 'C:\DFTWeeklyReport %YY-MM-DD%.csv')
Insert Into ReportProperties Values('18', 'Frequency', 'D')
Insert Into ReportProperties Values('18', 'LastRan', '01/01/2005')
Insert Into ReportProperties Values('18', 'MailRecipient', 'john.scott@atos.net;tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('18', 'Title', 'DFT Weekly Report %YY-MM-DD%')
Insert Into ReportProperties Values('19', 'FilePath', 'C:\ %YY-MM-DD%')
Insert Into ReportProperties Values('19', 'Frequency', 'W')
Insert Into ReportProperties Values('19', 'LastRan', '01/01/2005')
Insert Into ReportProperties Values('19', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('19', 'PartnerID', '-1')
Insert Into ReportProperties Values('19', 'Title', 'Partner Weekly Report %YY-MM-DD%')
Insert Into ReportProperties Values('20', 'FilePath', 'C:\DailyTLFailures %YY-MM-DD%.csv')
Insert Into ReportProperties Values('20', 'Frequency', 'D')
Insert Into ReportProperties Values('20', 'LastRan', '')
Insert Into ReportProperties Values('20', 'MailRecipient', 'tdpsupport@atos.net;tdportal.operations@dft.gsi.gov.uk;mike.gibson@dft.gsi.gov.uk;chris.sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('20', 'Title', 'Daily Traveline Failures %YY-MM-DD%')
Insert Into ReportProperties Values('21', 'FilePath', 'C:\WeeklyBBCReport %YY-MM-DD%.csv')
Insert Into ReportProperties Values('21', 'Frequency', 'W')
Insert Into ReportProperties Values('21', 'LastRan', '01/01/2005')
Insert Into ReportProperties Values('21', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net')
Insert Into ReportProperties Values('21', 'PartnerID', '2')
Insert Into ReportProperties Values('21', 'Title', 'BBC Weekly Report %YY-MM-DD%')
Insert Into ReportProperties Values('22', 'FilePath', 'C:\WeeklyDirectGovReport %YY-MM-DD%.csv')
Insert Into ReportProperties Values('22', 'Frequency', 'W')
Insert Into ReportProperties Values('22', 'LastRan', '01/01/2005')
Insert Into ReportProperties Values('22', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net')
Insert Into ReportProperties Values('22', 'PartnerID', '4')
Insert Into ReportProperties Values('22', 'Title', 'DirectGov Weekly Report %YY-MM-DD%')
Insert Into ReportProperties Values('23', 'FilePath', 'C:\WeeklyVisitBritainReport %YY-MM-DD%.csv')
Insert Into ReportProperties Values('23', 'Frequency', 'W')
Insert Into ReportProperties Values('23', 'LastRan', '01/01/2005')
Insert Into ReportProperties Values('23', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net')
Insert Into ReportProperties Values('23', 'PartnerID', '1')
Insert Into ReportProperties Values('23', 'Title', 'Visit Britain Weekly Report %YY-MM-DD%')
Insert Into ReportProperties Values('24', 'FilePath', 'C:\WeeklyPageLandingReport %YY-MM-DD%.csv')
Insert Into ReportProperties Values('24', 'Frequency', 'W')
Insert Into ReportProperties Values('24', 'LastRan', '01/01/2005')
Insert Into ReportProperties Values('24', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('24', 'Title', 'Page Landing Weekly Report %YY-MM-DD%')
Insert Into ReportProperties Values('25', 'FilePath', 'C:\EnhancedExposedServiceReport %YY-MM-DD%.csv')
Insert Into ReportProperties Values('25', 'Frequency', 'D')
Insert Into ReportProperties Values('25', 'LastRan', '12/03/2009')
Insert Into ReportProperties Values('25', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('25', 'Title', 'Enhanced Exposed Services Report %YY-MM-DD%')
Insert Into ReportProperties Values('26', 'FilePath', 'C:\DftMonthlyReport %YY-MM%.csv')
Insert Into ReportProperties Values('26', 'Frequency', 'M')
Insert Into ReportProperties Values('26', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('26', 'MailRecipient', 'tdpsupport@atos.net;chris.sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('26', 'Title', 'DfT Monthly Report %YY-MM%')
Insert Into ReportProperties Values('27', 'FilePath', 'C:\DfTWeeklyCombinedReport %YY-MM-DD%.csv')
Insert Into ReportProperties Values('27', 'Frequency', 'W')
Insert Into ReportProperties Values('27', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('27', 'MailRecipient', 'tdpsupport@atos.net;Chris.Sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('27', 'Title', 'DfT Weekly Combined Report %YY-MM-DD%')
Insert Into ReportProperties Values('28', 'FilePath', 'C:\DfTWeeklySummary %YY-MM-DD%.csv')
Insert Into ReportProperties Values('28', 'Frequency', 'W')
Insert Into ReportProperties Values('28', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('28', 'MailRecipient', 'tdpsupport@atos.net;chris.sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('28', 'Title', 'DfT Weekly Summary %YY-MM-DD%')
Insert Into ReportProperties Values('29', 'FilePath', 'C:\DfTMonthlySummary %YY-MM%.csv')
Insert Into ReportProperties Values('29', 'Frequency', 'M')
Insert Into ReportProperties Values('29', 'LastRan', '18/09/2008')
Insert Into ReportProperties Values('29', 'MailRecipient', 'tdpsupport@atos.net;chris.sanders@atos.net;Craig.stocken@atos.net;luftul.amin@atos.net')
Insert Into ReportProperties Values('29', 'Title', 'DfT Monthly Summary %YY-MM%')