-- ***********************************************
-- NAME 	: CreateReportingExtractDB.sql
-- DESCRIPTION 	: Creates Reporting database Extract for given month, users & roles
-- ************************************************
-- $Log:   P:/TDPortal/archives/Reporting/CreateReportingExtractDB.sql-arc  $
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

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'ReportingExtract')
	DROP DATABASE [ReportingExtract]
GO

CREATE DATABASE [ReportingExtract]
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'ReportingExtract', N'torn page detection', N'true'
GO

exec sp_dboption N'ReportingExtract', N'autoshrink', N'true'
GO

exec sp_dboption N'ReportingExtract', N'ANSI null default', N'true'
GO

exec sp_dboption N'ReportingExtract', N'ANSI nulls', N'true'
GO

exec sp_dboption N'ReportingExtract', N'ANSI warnings', N'true'
GO

exec sp_dboption N'ReportingExtract', N'auto create statistics', N'true'
GO

exec sp_dboption N'ReportingExtract', N'auto update statistics', N'true'
GO

if( ( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) ) or ( (@@microsoftversion / power(2, 24) = 7) and (@@microsoftversion & 0xffff >= 1082) ) )
	exec sp_dboption N'ReportingExtract', N'db chaining', N'false'
GO


EXEC sp_configure 'remote proc trans', '1'
GO

RECONFIGURE WITH OVERRIDE
GO


USE [ReportingExtract]
GO

-- --------------------------------
if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_GazetteerEvents_GazetteerType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[GazetteerEvents] DROP CONSTRAINT FK_GazetteerEvents_GazetteerType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_JourneyPlanLocationEvents_JourneyAdminAreaType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[JourneyPlanLocationEvents] DROP CONSTRAINT FK_JourneyPlanLocationEvents_JourneyAdminAreaType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_JourneyPlanModeEvents_JourneyModeType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[JourneyPlanModeEvents] DROP CONSTRAINT FK_JourneyPlanModeEvents_JourneyModeType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_JourneyPlanLocationEvents_JourneyPlanResponseType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[JourneyPlanLocationEvents] DROP CONSTRAINT FK_JourneyPlanLocationEvents_JourneyPlanResponseType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_JourneyPlanLocationEvents_JourneyPrepositionType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[JourneyPlanLocationEvents] DROP CONSTRAINT FK_JourneyPlanLocationEvents_JourneyPrepositionType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_JourneyAdminAreaType_JourneyRegionType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[JourneyAdminAreaType] DROP CONSTRAINT FK_JourneyAdminAreaType_JourneyRegionType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_JourneyWebRequestEvents_JourneyRegionType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[JourneyWebRequestEvents] DROP CONSTRAINT FK_JourneyWebRequestEvents_JourneyRegionType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_MapEvents_MapCommandType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[MapEvents] DROP CONSTRAINT FK_MapEvents_MapCommandType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_MapEvents_MapDisplayType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[MapEvents] DROP CONSTRAINT FK_MapEvents_MapDisplayType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_PageEntryEvents_PageEntryType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[PageEntryEvents] DROP CONSTRAINT FK_PageEntryEvents_PageEntryType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_ReferenceTransactionEvents_ReferenceTransactionType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[ReferenceTransactionEvents] DROP CONSTRAINT FK_ReferenceTransactionEvents_ReferenceTransactionType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_RetailerHandoffEvents_RetailerHandoffType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[RetailerHandoffEvents] DROP CONSTRAINT FK_RetailerHandoffEvents_RetailerHandoffType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[FK_UserPreferenceSaveEvents_UserPreferenceType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [ReportingExtract].[dbo].[UserPreferenceSaveEvents] DROP CONSTRAINT FK_UserPreferenceSaveEvents_UserPreferenceType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[CapacityBands]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[CapacityBands]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[GazetteerEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[GazetteerEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[GazetteerType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[GazetteerType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[JourneyAdminAreaType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[JourneyAdminAreaType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[JourneyModeType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[JourneyModeType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[JourneyPlanLocationEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[JourneyPlanLocationEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[JourneyPlanModeEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[JourneyPlanModeEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[JourneyPlanResponseType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[JourneyPlanResponseType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[JourneyPrepositionType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[JourneyPrepositionType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[JourneyProcessingEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[JourneyProcessingEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[JourneyRegionType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[JourneyRegionType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[JourneyWebRequestEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[JourneyWebRequestEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[LoginEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[LoginEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[MapCommandType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[MapCommandType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[MapDisplayType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[MapDisplayType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[MapEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[MapEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[OperationalEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[OperationalEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[PageEntryEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[PageEntryEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[PageEntryType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[PageEntryType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[ReferenceTransactionEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[ReferenceTransactionEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[ReferenceTransactionType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[ReferenceTransactionType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[ResponseServiceCreditBands]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[ResponseServiceCreditBands]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[RetailerHandoffEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[RetailerHandoffEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[RetailerHandoffType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[RetailerHandoffType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[RoadPlanEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[RoadPlanEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[DataGatewayEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[DataGatewayEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[SessionEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[SessionEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[UserPreferenceSaveEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[UserPreferenceSaveEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[UserPreferenceType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[UserPreferenceType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[WeekDays]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[WeekDays]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[ReportingExtract].[dbo].[WorkloadEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [ReportingExtract].[dbo].[WorkloadEvents]
GO

CREATE TABLE [ReportingExtract].[dbo].[CapacityBands] (
	[CBNumber] [tinyint] NOT NULL ,
	[CBMinRequests] [int] NOT NULL ,
	[CBMaxRequests] [int] NOT NULL ,
	[CBSurgePercentage] [decimal](8, 5) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[GazetteerEvents] (
	[GEDate] [datetime] NOT NULL ,
	[GEHour] [tinyint] NOT NULL ,
	[GEHourQuarter] [tinyint] NOT NULL ,
	[GEWeekDay] [tinyint] NOT NULL ,
	[GEGTID] [tinyint] NOT NULL ,
	[GELoggedOn] [bit] NOT NULL ,
	[GECount] [int] NOT NULL ,
	[GEAvMsDuration] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[GazetteerType] (
	[GTID] [tinyint] NOT NULL ,
	[GTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[GTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[JourneyAdminAreaType] (
	[JAATID] [smallint] NOT NULL ,
	[JAATJRTID] [tinyint] NOT NULL ,
	[JAATCode] [varchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JAATDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[JourneyModeType] (
	[JMTID] [tinyint] NOT NULL ,
	[JMTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JMTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[JourneyPlanLocationEvents] (
	[JPLEDate] [datetime] NOT NULL ,
	[JPLEHour] [tinyint] NOT NULL ,
	[JPLEHourQuarter] [tinyint] NOT NULL ,
	[JPLEWeekDay] [tinyint] NOT NULL ,
	[JPLEJPTID] [tinyint] NOT NULL ,
	[JPLEJAATID] [smallint] NOT NULL ,
	[JPLEJPRTID] [tinyint] NOT NULL ,
	[JPLELoggedOn] [bit] NOT NULL ,
	[JPLECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[JourneyPlanModeEvents] (
	[JPMEDate] [datetime] NOT NULL ,
	[JPMEHour] [tinyint] NOT NULL ,
	[JPMEHourQuarter] [tinyint] NOT NULL ,
	[JPMEWeekDay] [tinyint] NOT NULL ,
	[JPMEJMTID] [tinyint] NOT NULL ,
	[JPMELoggedOn] [bit] NOT NULL ,
	[JPMECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[JourneyPlanResponseType] (
	[JPRTID] [tinyint] NOT NULL ,
	[JPRTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JPRTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[JourneyPrepositionType] (
	[JPTID] [tinyint] NOT NULL ,
	[JPTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JPTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[JourneyProcessingEvents] (
	[JPEDate] [datetime] NOT NULL ,
	[JPEHour] [tinyint] NOT NULL ,
	[JPEHourQuarter] [tinyint] NOT NULL ,
	[JPEWeekDay] [tinyint] NOT NULL ,
	[JPERefTransaction] [bit] NOT NULL ,
	[JPEAvMsProcessingDuration] [int] NOT NULL ,
	[JPEAvMsWaitingDuration] [int] NOT NULL ,
	[JPECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[JourneyRegionType] (
	[JRTID] [tinyint] NOT NULL ,
	[JRTCode] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JRTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JRTMaxMsDuration] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[JourneyWebRequestEvents] (
	[JWREDate] [datetime] NOT NULL ,
	[JWREHour] [tinyint] NOT NULL ,
	[JWREHourQuarter] [tinyint] NOT NULL ,
	[JWREWeekDay] [tinyint] NOT NULL ,
	[JWREJRTID] [tinyint] NOT NULL ,
	[JWRESuccessful] [bit] NOT NULL ,
	[JWREAvMsDuration] [int] NOT NULL ,
	[JWRECount] [int] NOT NULL ,
	[JWREJWRTID] [tinyint] NOT NULL
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[LoginEvents] (
	[LEDate] [datetime] NOT NULL ,
	[LEHour] [tinyint] NOT NULL ,
	[LEHourQuarter] [tinyint] NOT NULL ,
	[LEWeekDay] [tinyint] NOT NULL ,
	[LECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[MapCommandType] (
	[MCTID] [tinyint] NOT NULL ,
	[MCTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[MCTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[MapDisplayType] (
	[MDTID] [tinyint] NOT NULL ,
	[MDTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[MDTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[MapEvents] (
	[MEDate] [datetime] NOT NULL ,
	[MEHour] [tinyint] NOT NULL ,
	[MEHourQuarter] [tinyint] NOT NULL ,
	[MEWeekDay] [tinyint] NOT NULL ,
	[MEMDTID] [tinyint] NOT NULL ,
	[MEMCTID] [tinyint] NOT NULL ,
	[MELoggedOn] [bit] NOT NULL ,
	[MEAvMsDuration] [int] NOT NULL ,
	[MECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[OperationalEvents] (
	[OEID] [int] NOT NULL ,
	[OESessionID] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[OEMessage] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[OEMachineName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[OEAssemblyName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[OEMethodName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[OETypeName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[OELevel] [varchar] (50) NULL ,
	[OECategory] [varchar] (50) NULL ,
	[OETarget] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[OETimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[PageEntryEvents] (
	[PEEDate] [datetime] NOT NULL ,
	[PEEHour] [tinyint] NOT NULL ,
	[PEEHourQuarter] [tinyint] NOT NULL ,
	[PEEWeekDay] [tinyint] NOT NULL ,
	[PEEPETID] [int] NOT NULL ,
	[PEELoggedOn] [bit] NOT NULL ,
	[PEECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[PageEntryType] (
	[PETID] [int] NOT NULL ,
	[PETCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PETDescription] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[ReferenceTransactionEvents] (
	[RTEDate] [datetime] NOT NULL ,
	[RTEHour] [tinyint] NOT NULL ,
	[RTEMinute] [tinyint] NOT NULL ,
	[RTEWeekDay] [tinyint] NOT NULL ,
	[RTERTTID] [tinyint] NOT NULL ,
	[RTESuccessful] [bit] NOT NULL ,
	[RTEOverran] [bit] NOT NULL ,
	[RTEMsDuration] [int] NOT NULL ,
	[RTEMachineName] [varchar] (50)
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[ReferenceTransactionType] (
	[RTTID] [tinyint] NOT NULL ,
	[RTTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[RTTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[RTTCreditInclude] [bit] NOT NULL ,
	[RTTSLAInclude] [bit] NOT NULL ,
	[RTTMaxMsDuration] [int] NOT NULL ,
	[RTTTarget] [decimal](8, 5) NOT NULL ,
	[RTTThreshold] [decimal](8, 5) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[ResponseServiceCreditBands] (
	[RSCBStart] [decimal](8, 5) NOT NULL ,
	[RSCBEnd] [decimal](8, 5) NOT NULL ,
	[RSCBPoints] [smallint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[RetailerHandoffEvents] (
	[RHEDate] [datetime] NOT NULL ,
	[RHEHour] [tinyint] NOT NULL ,
	[RHEHourQuarter] [tinyint] NOT NULL ,
	[RHEWeekDay] [tinyint] NOT NULL ,
	[RHERHTID] [tinyint] NOT NULL ,
	[RHELoggedOn] [bit] NOT NULL ,
	[RHECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[RetailerHandoffType] (
	[RHTID] [tinyint] NOT NULL ,
	[RHTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[RHTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[RHTOnlineRetailer] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[DataGatewayEvents] (
	[DGEID] [bigint] NOT NULL ,
	[DGEFeedId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DGESessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DGEFileName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DGETimeStarted] [datetime] NULL,
	[DGETimeFinished] [datetime] NULL,
	[DGESuccessFlag] [bit] NULL ,
	[DGEErrorCode] [int] NULL,
	[DGEUserLoggedOn] [bit] NULL ,
	[DGETimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[RoadPlanEvents] (
	[RPEDate] [datetime] NOT NULL ,
	[RPEHour] [tinyint] NOT NULL ,
	[RPEHourQuarter] [tinyint] NOT NULL ,
	[RPEWeekDay] [tinyint] NOT NULL ,
	[RPECongestedRoute] [bit] NOT NULL ,
	[RPELoggedOn] [bit] NOT NULL ,
	[RPECount] [int] NOT NULL ,
	[RPEAvMsDuration] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[SessionEvents] (
	[SEDate] [datetime] NOT NULL ,
	[SECount] [int] NOT NULL , 
	[SEHour] [int] NOT NULL ,
	[SEPartnerID] [int] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[UserPreferenceSaveEvents] (
	[UPSEDate] [datetime] NOT NULL ,
	[UPSEHour] [tinyint] NOT NULL ,
	[UPSEHourQuarter] [tinyint] NOT NULL ,
	[UPSEWeekDay] [tinyint] NOT NULL ,
	[UPSEUPTID] [tinyint] NOT NULL ,
	[UPSECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[UserPreferenceType] (
	[UPTID] [tinyint] NOT NULL ,
	[UPTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[UPTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[WeekDays] (
	[WDID] [tinyint] NOT NULL ,
	[WDShortName] [varchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[WDFullName] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [ReportingExtract].[dbo].[WorkloadEvents] (
	[WEDate] [datetime] NOT NULL ,
	[WEHour] [tinyint] NOT NULL ,
	[WEMinute] [tinyint] NOT NULL ,
	[WEWeekDay] [tinyint] NOT NULL ,
	[WECount] [int] NOT NULL ,
	[PartnerId] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [ReportingExtract].[dbo].[CapacityBands] WITH NOCHECK ADD 
	CONSTRAINT [PK_CapacityBands] PRIMARY KEY  CLUSTERED 
	(
		[CBNumber]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[GazetteerType] WITH NOCHECK ADD 
	CONSTRAINT [PK_GazetteerType] PRIMARY KEY  CLUSTERED 
	(
		[GTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[JourneyAdminAreaType] WITH NOCHECK ADD 
	CONSTRAINT [PK_JourneyAdminAreaType] PRIMARY KEY  CLUSTERED 
	(
		[JAATID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[JourneyModeType] WITH NOCHECK ADD 
	CONSTRAINT [PK_JourneyModeType] PRIMARY KEY  CLUSTERED 
	(
		[JMTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[JourneyPlanResponseType] WITH NOCHECK ADD 
	CONSTRAINT [PK_JourneyPlanResponseType] PRIMARY KEY  CLUSTERED 
	(
		[JPRTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[JourneyPrepositionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_JourneyPrepositionType] PRIMARY KEY  CLUSTERED 
	(
		[JPTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[JourneyRegionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_JourneyRegionType] PRIMARY KEY  CLUSTERED 
	(
		[JRTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[MapCommandType] WITH NOCHECK ADD 
	CONSTRAINT [PK_MapCommandType] PRIMARY KEY  CLUSTERED 
	(
		[MCTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[MapDisplayType] WITH NOCHECK ADD 
	CONSTRAINT [PK_MapDisplayType] PRIMARY KEY  CLUSTERED 
	(
		[MDTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[OperationalEvents] WITH NOCHECK ADD 
	CONSTRAINT [PK_OperationalEvents] PRIMARY KEY  CLUSTERED 
	(
		[OEID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[PageEntryType] WITH NOCHECK ADD 
	CONSTRAINT [PK_PageEntryType] PRIMARY KEY  CLUSTERED 
	(
		[PETID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[ReferenceTransactionEvents] WITH NOCHECK ADD 
	CONSTRAINT [PK_ReferenceTransactionEvents] PRIMARY KEY  CLUSTERED 
	(
		[RTEDate],
		[RTEHour],
		[RTEMinute],
		[RTERTTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[ReferenceTransactionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_ReferenceTransactionType] PRIMARY KEY  CLUSTERED 
	(
		[RTTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[RetailerHandoffType] WITH NOCHECK ADD 
	CONSTRAINT [PK_RetailerHandoffType] PRIMARY KEY  CLUSTERED 
	(
		[RHTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[SessionEvents] WITH NOCHECK ADD 
	CONSTRAINT [PK_SessionEvents] PRIMARY KEY  CLUSTERED 
	(
		[SEDate]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[UserPreferenceType] WITH NOCHECK ADD 
	CONSTRAINT [PK_UserPreferenceType] PRIMARY KEY  CLUSTERED 
	(
		[UPTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [ReportingExtract].[dbo].[WeekDays] WITH NOCHECK ADD 
	CONSTRAINT [PK_WeekDays] PRIMARY KEY  CLUSTERED 
	(
		[WDID]
	)  ON [PRIMARY] 
GO

 CREATE  INDEX [IX_GazetteerEvents] ON [ReportingExtract].[dbo].[GazetteerEvents]([GEDate], [GEHour], [GEHourQuarter], [GEGTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_JourneyPlanLocationEvents] ON [ReportingExtract].[dbo].[JourneyPlanLocationEvents]([JPLEDate], [JPLEHour], [JPLEHourQuarter], [JPLEJPTID], [JPLEJAATID], [JPLEJPRTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_JourneyPlanModeEvents] ON [ReportingExtract].[dbo].[JourneyPlanModeEvents]([JPMEDate], [JPMEHour], [JPMEHourQuarter], [JPMEJMTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_JourneyProcessingEvents] ON [ReportingExtract].[dbo].[JourneyProcessingEvents]([JPEDate], [JPEHour], [JPEHourQuarter]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_JourneyWebRequestEvents] ON [ReportingExtract].[dbo].[JourneyWebRequestEvents]([JWREDate], [JWREHour], [JWREHourQuarter], [JWREJRTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_LoginEvents] ON [ReportingExtract].[dbo].[LoginEvents]([LEDate], [LEHour], [LEHourQuarter]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_MapEvents] ON [ReportingExtract].[dbo].[MapEvents]([MEDate], [MEHour], [MEHourQuarter], [MEMDTID], [MEMCTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_PageEntryEvents] ON [ReportingExtract].[dbo].[PageEntryEvents]([PEEDate], [PEEHour], [PEEHourQuarter], [PEEPETID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_RetailerHandoffEvents] ON [ReportingExtract].[dbo].[RetailerHandoffEvents]([RHEDate], [RHEHour], [RHEHourQuarter], [RHERHTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_RoadPlanEvents] ON [ReportingExtract].[dbo].[RoadPlanEvents]([RPEDate], [RPEHour], [RPEHourQuarter]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_UserPreferenceSaveEvents] ON [ReportingExtract].[dbo].[UserPreferenceSaveEvents]([UPSEDate], [UPSEHour], [UPSEHourQuarter], [UPSEUPTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_WorkloadEvents] ON [ReportingExtract].[dbo].[WorkloadEvents]([WEDate], [WEHour], [WEMinute]) ON [PRIMARY]
GO

ALTER TABLE [ReportingExtract].[dbo].[GazetteerEvents] ADD 
	CONSTRAINT [FK_GazetteerEvents_GazetteerType] FOREIGN KEY 
	(
		[GEGTID]
	) REFERENCES [ReportingExtract].[dbo].[GazetteerType] (
		[GTID]
	)
GO

ALTER TABLE [ReportingExtract].[dbo].[JourneyAdminAreaType] ADD 
	CONSTRAINT [FK_JourneyAdminAreaType_JourneyRegionType] FOREIGN KEY 
	(
		[JAATJRTID]
	) REFERENCES [ReportingExtract].[dbo].[JourneyRegionType] (
		[JRTID]
	)
GO

ALTER TABLE [ReportingExtract].[dbo].[JourneyPlanLocationEvents] ADD 
	CONSTRAINT [FK_JourneyPlanLocationEvents_JourneyAdminAreaType] FOREIGN KEY 
	(
		[JPLEJAATID]
	) REFERENCES [ReportingExtract].[dbo].[JourneyAdminAreaType] (
		[JAATID]
	),
	CONSTRAINT [FK_JourneyPlanLocationEvents_JourneyPlanResponseType] FOREIGN KEY 
	(
		[JPLEJPRTID]
	) REFERENCES [ReportingExtract].[dbo].[JourneyPlanResponseType] (
		[JPRTID]
	),
	CONSTRAINT [FK_JourneyPlanLocationEvents_JourneyPrepositionType] FOREIGN KEY 
	(
		[JPLEJPTID]
	) REFERENCES [ReportingExtract].[dbo].[JourneyPrepositionType] (
		[JPTID]
	)
GO

ALTER TABLE [ReportingExtract].[dbo].[JourneyPlanModeEvents] ADD 
	CONSTRAINT [FK_JourneyPlanModeEvents_JourneyModeType] FOREIGN KEY 
	(
		[JPMEJMTID]
	) REFERENCES [ReportingExtract].[dbo].[JourneyModeType] (
		[JMTID]
	)
GO

ALTER TABLE [ReportingExtract].[dbo].[JourneyWebRequestEvents] ADD 
	CONSTRAINT [FK_JourneyWebRequestEvents_JourneyRegionType] FOREIGN KEY 
	(
		[JWREJRTID]
	) REFERENCES [ReportingExtract].[dbo].[JourneyRegionType] (
		[JRTID]
	)
GO

ALTER TABLE [ReportingExtract].[dbo].[MapEvents] ADD 
	CONSTRAINT [FK_MapEvents_MapCommandType] FOREIGN KEY 
	(
		[MEMCTID]
	) REFERENCES [ReportingExtract].[dbo].[MapCommandType] (
		[MCTID]
	),
	CONSTRAINT [FK_MapEvents_MapDisplayType] FOREIGN KEY 
	(
		[MEMDTID]
	) REFERENCES [ReportingExtract].[dbo].[MapDisplayType] (
		[MDTID]
	)
GO

ALTER TABLE [ReportingExtract].[dbo].[PageEntryEvents] ADD 
	CONSTRAINT [FK_PageEntryEvents_PageEntryType] FOREIGN KEY 
	(
		[PEEPETID]
	) REFERENCES [ReportingExtract].[dbo].[PageEntryType] (
		[PETID]
	)
GO

ALTER TABLE [ReportingExtract].[dbo].[ReferenceTransactionEvents] ADD 
	CONSTRAINT [FK_ReferenceTransactionEvents_ReferenceTransactionType] FOREIGN KEY 
	(
		[RTERTTID]
	) REFERENCES [ReportingExtract].[dbo].[ReferenceTransactionType] (
		[RTTID]
	)
GO

ALTER TABLE [ReportingExtract].[dbo].[RetailerHandoffEvents] ADD 
	CONSTRAINT [FK_RetailerHandoffEvents_RetailerHandoffType] FOREIGN KEY 
	(
		[RHERHTID]
	) REFERENCES [ReportingExtract].[dbo].[RetailerHandoffType] (
		[RHTID]
	)
GO

ALTER TABLE [ReportingExtract].[dbo].[UserPreferenceSaveEvents] ADD 
	CONSTRAINT [FK_UserPreferenceSaveEvents_UserPreferenceType] FOREIGN KEY 
	(
		[UPSEUPTID]
	) REFERENCES [ReportingExtract].[dbo].[UserPreferenceType] (
		[UPTID]
	)
GO

---------------------------------------------------------------------------------------------

-- now copy from reporting database for current month
------------------------------------------------------

use ReportingExtract
------------------------------------


DECLARE @ExtMon int
DECLARE @ExtYr int
SET @ExtMon = 02
SET @ExtYr = 2004

-------------------------------------------------------------------------------
-------------------------------------------------------------------------------
delete from [ReportingExtract].[dbo].[GazetteerEvents]
delete from [ReportingExtract].[dbo].[OperationalEvents]
delete from [ReportingExtract].[dbo].[RoadPlanEvents]
delete from [ReportingExtract].[dbo].[RetailerHandoffEvents]
delete from [ReportingExtract].[dbo].[MapEvents]
delete from [ReportingExtract].[dbo].[WorkloadEvents]
delete from [ReportingExtract].[dbo].[JourneyWebRequestEvents]
delete from [ReportingExtract].[dbo].[JourneyProcessingEvents]
delete from [ReportingExtract].[dbo].[JourneyPlanModeEvents]
delete from [ReportingExtract].[dbo].[JourneyPlanLocationEvents]
delete from [ReportingExtract].[dbo].DataGatewayEvents
delete from [ReportingExtract].[dbo].[UserPreferenceSaveEvents]
delete from [ReportingExtract].[dbo].[SessionEvents]
delete from [ReportingExtract].[dbo].[LoginEvents]
delete from [ReportingExtract].[dbo].[PageEntryEvents]
delete from [ReportingExtract].[dbo].[ReferenceTransactionEvents]

delete from [ReportingExtract].[dbo].[ReferenceTransactionType]
delete from [ReportingExtract].[dbo].[PageEntryType]
delete from [ReportingExtract].[dbo].[UserPreferenceType]
delete from [ReportingExtract].[dbo].[GazetteerType]
delete from [ReportingExtract].[dbo].[CapacityBands]
delete from [ReportingExtract].[dbo].[JourneyPlanResponseType]
delete from [ReportingExtract].[dbo].[JourneyPrepositionType]
delete from [ReportingExtract].[dbo].[JourneyRegionType]
delete from [ReportingExtract].[dbo].[MapCommandType]
delete from [ReportingExtract].[dbo].[MapDisplayType]
delete from [ReportingExtract].[dbo].[ResponseServiceCreditBands]
delete from [ReportingExtract].[dbo].[RetailerHandoffType]
delete from [ReportingExtract].[dbo].[JourneyAdminAreaType]
delete from [ReportingExtract].[dbo].[JourneyModeType]
delete from [ReportingExtract].[dbo].[WeekDays]



--------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[GazetteerType] 
select * from [Reporting].[dbo].[GazetteerType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[UserPreferenceType] 
select * from [Reporting].[dbo].[UserPreferenceType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[PageEntryType] 
select * from [Reporting].[dbo].[PageEntryType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[ReferenceTransactionType] 
select * from [Reporting].[dbo].[ReferenceTransactionType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[CapacityBands]
select * from [Reporting].[dbo].[CapacityBands]
----------------------------------------------------
Insert into [ReportingExtract].[dbo].[JourneyPlanResponseType]
select * from [Reporting].[dbo].[JourneyPlanResponseType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[JourneyPrepositionType] 
select * from [Reporting].[dbo].[JourneyPrepositionType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[JourneyRegionType]
select * from [Reporting].[dbo].[JourneyRegionType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[MapCommandType] 
select * from [Reporting].[dbo].[MapCommandType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[MapDisplayType] 
select * from [Reporting].[dbo].[MapDisplayType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[ResponseServiceCreditBands] 
select * from [Reporting].[dbo].[ResponseServiceCreditBands]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[RetailerHandoffType] 
select * from [Reporting].[dbo].[RetailerHandoffType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[JourneyAdminAreaType] 
select * from [Reporting].[dbo].[JourneyAdminAreaType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[JourneyModeType] 
select * from [Reporting].[dbo].[JourneyModeType]
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[WeekDays] 
select * from [Reporting].[dbo].[WeekDays]



Insert into [ReportingExtract].[dbo].[GazetteerEvents] 
select * from [Reporting].[dbo].[GazetteerEvents]
where month([Reporting].[dbo].[GazetteerEvents].GEDate) = @ExtMon
and year([Reporting].[dbo].[GazetteerEvents].GEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[UserPreferenceSaveEvents]
select * from [Reporting].[dbo].[UserPreferenceSaveEvents]
where month([Reporting].[dbo].[UserPreferenceSaveEvents].UPSEDate) = @ExtMon
and year([Reporting].[dbo].[UserPreferenceSaveEvents].UPSEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[SessionEvents]
select * from [Reporting].[dbo].[SessionEvents]
where month([Reporting].[dbo].[SessionEvents].SEDate) = @ExtMon
and year([Reporting].[dbo].[SessionEvents].SEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[LoginEvents] 
select * from [Reporting].[dbo].[LoginEvents]
where month([Reporting].[dbo].[LoginEvents].LEDate) = @ExtMon
and year([Reporting].[dbo].[LoginEvents].LEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[PageEntryEvents] 
select * from [Reporting].[dbo].[PageEntryEvents]
where month([Reporting].[dbo].[PageEntryEvents].PEEDate) = @ExtMon
and year([Reporting].[dbo].[PageEntryEvents].PEEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[ReferenceTransactionEvents]
select * from [Reporting].[dbo].[ReferenceTransactionEvents]
where month([Reporting].[dbo].[ReferenceTransactionEvents].RTEDate) = @ExtMon
and year([Reporting].[dbo].[ReferenceTransactionEvents].RTEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[DataGatewayEvents]
select * from [Reporting].[dbo].[DataGatewayEvents]
where month([Reporting].[dbo].[DataGatewayEvents].DGETimeLogged) = @ExtMon
and year([Reporting].[dbo].[DataGatewayEvents].DGETimeLogged) = @ExtYr
------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[JourneyPlanLocationEvents] 
select * from [Reporting].[dbo].[JourneyPlanLocationEvents]
where month([Reporting].[dbo].[JourneyPlanLocationEvents].JPLEDate) = @ExtMon
and year([Reporting].[dbo].[JourneyPlanLocationEvents].JPLEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[WorkloadEvents] 
select * from [Reporting].[dbo].[WorkloadEvents]
where month([Reporting].[dbo].[WorkloadEvents].WEDate) = @ExtMon
and year([Reporting].[dbo].[WorkloadEvents].WEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[JourneyPlanModeEvents] 
select * from [Reporting].[dbo].[JourneyPlanModeEvents]
where month([Reporting].[dbo].[JourneyPlanModeEvents].JPMEDate) = @ExtMon
and year([Reporting].[dbo].[JourneyPlanModeEvents].JPMEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[JourneyProcessingEvents] 
select * from [Reporting].[dbo].[JourneyProcessingEvents]
where month([Reporting].[dbo].[JourneyProcessingEvents].JPEDate) = @ExtMon
and year([Reporting].[dbo].[JourneyProcessingEvents].JPEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[JourneyWebRequestEvents] 
select * from [Reporting].[dbo].[JourneyWebRequestEvents]
where month([Reporting].[dbo].[JourneyWebRequestEvents].JWREDate) = @ExtMon
and year([Reporting].[dbo].[JourneyWebRequestEvents].JWREDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[MapEvents] 
select * from [Reporting].[dbo].[MapEvents]
where month([Reporting].[dbo].[MapEvents].MEDate) = @ExtMon
and year([Reporting].[dbo].[MapEvents].MEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[OperationalEvents]
select * from [Reporting].[dbo].[OperationalEvents]
where month([Reporting].[dbo].[OperationalEvents].OETimeLogged) = @ExtMon
and year([Reporting].[dbo].[OperationalEvents].OETimeLogged) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[RetailerHandoffEvents]
select * from [Reporting].[dbo].[RetailerHandoffEvents]
where month([Reporting].[dbo].[RetailerHandoffEvents].RHEDate) = @ExtMon
and year([Reporting].[dbo].[RetailerHandoffEvents].RHEDate) = @ExtYr
-------------------------------------------------------------------------------
Insert into [ReportingExtract].[dbo].[RoadPlanEvents] 
select * from [Reporting].[dbo].[RoadPlanEvents]
where month([Reporting].[dbo].[RoadPlanEvents].RPEDate) = @ExtMon
and year([Reporting].[dbo].[RoadPlanEvents].RPEDate) = @ExtYr
-------------------------------------------------------------------------------





