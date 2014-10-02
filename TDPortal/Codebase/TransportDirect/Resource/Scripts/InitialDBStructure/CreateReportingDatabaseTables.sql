-- ***********************************************
-- NAME 	: CreateReportingDatabaseTables.sql
-- DESCRIPTION 	: Creates the Reporting database tables, indexes, constraints,
--		: foreign keys, sprocs and functions.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreateReportingDatabaseTables.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:22   mturner
--Initial revision.

use [Reporting]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_GazetteerEvents_GazetteerType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[GazetteerEvents] DROP CONSTRAINT FK_GazetteerEvents_GazetteerType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_JourneyPlanLocationEvents_JourneyAdminAreaType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[JourneyPlanLocationEvents] DROP CONSTRAINT FK_JourneyPlanLocationEvents_JourneyAdminAreaType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_JourneyPlanModeEvents_JourneyModeType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[JourneyPlanModeEvents] DROP CONSTRAINT FK_JourneyPlanModeEvents_JourneyModeType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_JourneyPlanLocationEvents_JourneyPlanResponseType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[JourneyPlanLocationEvents] DROP CONSTRAINT FK_JourneyPlanLocationEvents_JourneyPlanResponseType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_JourneyPlanLocationEvents_JourneyPrepositionType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[JourneyPlanLocationEvents] DROP CONSTRAINT FK_JourneyPlanLocationEvents_JourneyPrepositionType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_JourneyAdminAreaType_JourneyRegionType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[JourneyAdminAreaType] DROP CONSTRAINT FK_JourneyAdminAreaType_JourneyRegionType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_JourneyWebRequestEvents_JourneyRegionType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[JourneyWebRequestEvents] DROP CONSTRAINT FK_JourneyWebRequestEvents_JourneyRegionType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_MapEvents_MapCommandType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[MapEvents] DROP CONSTRAINT FK_MapEvents_MapCommandType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_MapEvents_MapDisplayType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[MapEvents] DROP CONSTRAINT FK_MapEvents_MapDisplayType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_PageEntryEvents_PageEntryType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[PageEntryEvents] DROP CONSTRAINT FK_PageEntryEvents_PageEntryType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ReferenceTransactionEvents_ReferenceTransactionType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ReferenceTransactionEvents] DROP CONSTRAINT FK_ReferenceTransactionEvents_ReferenceTransactionType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_RetailerHandoffEvents_RetailerHandoffType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[RetailerHandoffEvents] DROP CONSTRAINT FK_RetailerHandoffEvents_RetailerHandoffType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_UserPreferenceSaveEvents_UserPreferenceType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[UserPreferenceSaveEvents] DROP CONSTRAINT FK_UserPreferenceSaveEvents_UserPreferenceType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CapacityBands]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[CapacityBands]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GazetteerEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GazetteerEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GazetteerType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GazetteerType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyAdminAreaType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyAdminAreaType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyModeType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyModeType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyPlanLocationEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyPlanLocationEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyPlanModeEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyPlanModeEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyPlanResponseType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyPlanResponseType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyPrepositionType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyPrepositionType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyProcessingEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyProcessingEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyRegionType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyRegionType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyWebRequestEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyWebRequestEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[LoginEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[LoginEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MapCommandType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[MapCommandType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MapDisplayType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[MapDisplayType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MapEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[MapEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OperationalEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[OperationalEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PageEntryEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[PageEntryEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PageEntryType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[PageEntryType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReferenceTransactionEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ReferenceTransactionEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReferenceTransactionType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ReferenceTransactionType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ResponseServiceCreditBands]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ResponseServiceCreditBands]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RetailerHandoffEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[RetailerHandoffEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RetailerHandoffType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[RetailerHandoffType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RoadPlanEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[RoadPlanEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DataGatewayEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DataGatewayEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[SessionEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[SessionEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UserPreferenceSaveEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[UserPreferenceSaveEvents]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UserPreferenceType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[UserPreferenceType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[WeekDays]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[WeekDays]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[WorkloadEvents]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[WorkloadEvents]
GO

CREATE TABLE [dbo].[CapacityBands] (
	[CBNumber] [tinyint] NOT NULL ,
	[CBMinRequests] [int] NOT NULL ,
	[CBMaxRequests] [int] NOT NULL ,
	[CBSurgePercentage] [decimal](8, 5) NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GazetteerEvents] (
	[GEDate] [datetime] NOT NULL ,
	[GEHour] [tinyint] NOT NULL ,
	[GEHourQuarter] [tinyint] NOT NULL ,
	[GEWeekDay] [tinyint] NOT NULL ,
	[GEGTID] [tinyint] NOT NULL ,
	[GELoggedOn] [bit] NOT NULL ,
	[GECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[GazetteerType] (
	[GTID] [tinyint] NOT NULL ,
	[GTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[GTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyAdminAreaType] (
	[JAATID] [smallint] NOT NULL ,
	[JAATJRTID] [tinyint] NOT NULL ,
	[JAATCode] [varchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JAATDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyModeType] (
	[JMTID] [tinyint] NOT NULL ,
	[JMTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JMTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyPlanLocationEvents] (
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

CREATE TABLE [dbo].[JourneyPlanModeEvents] (
	[JPMEDate] [datetime] NOT NULL ,
	[JPMEHour] [tinyint] NOT NULL ,
	[JPMEHourQuarter] [tinyint] NOT NULL ,
	[JPMEWeekDay] [tinyint] NOT NULL ,
	[JPMEJMTID] [tinyint] NOT NULL ,
	[JPMELoggedOn] [bit] NOT NULL ,
	[JPMECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyPlanResponseType] (
	[JPRTID] [tinyint] NOT NULL ,
	[JPRTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JPRTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyPrepositionType] (
	[JPTID] [tinyint] NOT NULL ,
	[JPTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JPTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyProcessingEvents] (
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

CREATE TABLE [dbo].[JourneyRegionType] (
	[JRTID] [tinyint] NOT NULL ,
	[JRTCode] [varchar] (2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JRTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[JRTMaxMsDuration] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyWebRequestEvents] (
	[JWREDate] [datetime] NOT NULL ,
	[JWREHour] [tinyint] NOT NULL ,
	[JWREHourQuarter] [tinyint] NOT NULL ,
	[JWREWeekDay] [tinyint] NOT NULL ,
	[JWREJRTID] [tinyint] NOT NULL ,
	[JWRESuccessful] [bit] NOT NULL ,
	[JWREAvMsDuration] [int] NOT NULL ,
	[JWRECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LoginEvents] (
	[LEDate] [datetime] NOT NULL ,
	[LEHour] [tinyint] NOT NULL ,
	[LEHourQuarter] [tinyint] NOT NULL ,
	[LEWeekDay] [tinyint] NOT NULL ,
	[LECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MapCommandType] (
	[MCTID] [tinyint] NOT NULL ,
	[MCTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[MCTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MapDisplayType] (
	[MDTID] [tinyint] NOT NULL ,
	[MDTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[MDTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MapEvents] (
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

CREATE TABLE [dbo].[OperationalEvents] (
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

CREATE TABLE [dbo].[PageEntryEvents] (
	[PEEDate] [datetime] NOT NULL ,
	[PEEHour] [tinyint] NOT NULL ,
	[PEEHourQuarter] [tinyint] NOT NULL ,
	[PEEWeekDay] [tinyint] NOT NULL ,
	[PEEPETID] [tinyint] NOT NULL ,
	[PEELoggedOn] [bit] NOT NULL ,
	[PEECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PageEntryType] (
	[PETID] [tinyint] NOT NULL ,
	[PETCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[PETDescription] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReferenceTransactionEvents] (
	[RTEDate] [datetime] NOT NULL ,
	[RTEHour] [tinyint] NOT NULL ,
	[RTEMinute] [tinyint] NOT NULL ,
	[RTEWeekDay] [tinyint] NOT NULL ,
	[RTERTTID] [tinyint] NOT NULL ,
	[RTESuccessful] [bit] NOT NULL ,
	[RTEOverran] [bit] NOT NULL ,
	[RTEMsDuration] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReferenceTransactionType] (
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

CREATE TABLE [dbo].[ResponseServiceCreditBands] (
	[RSCBStart] [decimal](8, 5) NOT NULL ,
	[RSCBEnd] [decimal](8, 5) NOT NULL ,
	[RSCBPoints] [smallint] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RetailerHandoffEvents] (
	[RHEDate] [datetime] NOT NULL ,
	[RHEHour] [tinyint] NOT NULL ,
	[RHEHourQuarter] [tinyint] NOT NULL ,
	[RHEWeekDay] [tinyint] NOT NULL ,
	[RHERHTID] [tinyint] NOT NULL ,
	[RHELoggedOn] [bit] NOT NULL ,
	[RHECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RetailerHandoffType] (
	[RHTID] [tinyint] NOT NULL ,
	[RHTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[RHTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DataGatewayEvents] (
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

CREATE TABLE [dbo].[RoadPlanEvents] (
	[RPEDate] [datetime] NOT NULL ,
	[RPEHour] [tinyint] NOT NULL ,
	[RPEHourQuarter] [tinyint] NOT NULL ,
	[RPEWeekDay] [tinyint] NOT NULL ,
	[RPECongestedRoute] [bit] NOT NULL ,
	[RPELoggedOn] [bit] NOT NULL ,
	[RPECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[SessionEvents] (
	[SEDate] [datetime] NOT NULL ,
	[SECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[UserPreferenceSaveEvents] (
	[UPSEDate] [datetime] NOT NULL ,
	[UPSEHour] [tinyint] NOT NULL ,
	[UPSEHourQuarter] [tinyint] NOT NULL ,
	[UPSEWeekDay] [tinyint] NOT NULL ,
	[UPSEUPTID] [tinyint] NOT NULL ,
	[UPSECount] [int] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[UserPreferenceType] (
	[UPTID] [tinyint] NOT NULL ,
	[UPTCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[UPTDescription] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WeekDays] (
	[WDID] [tinyint] NOT NULL ,
	[WDShortName] [varchar] (3) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[WDFullName] [varchar] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkloadEvents] (
	[WEDate] [datetime] NOT NULL ,
	[WEHour] [tinyint] NOT NULL ,
	[WEMinute] [tinyint] NOT NULL ,
	[WEWeekDay] [tinyint] NOT NULL ,
	[WECount] [int] NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CapacityBands] WITH NOCHECK ADD 
	CONSTRAINT [PK_CapacityBands] PRIMARY KEY  CLUSTERED 
	(
		[CBNumber]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[GazetteerType] WITH NOCHECK ADD 
	CONSTRAINT [PK_GazetteerType] PRIMARY KEY  CLUSTERED 
	(
		[GTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[JourneyAdminAreaType] WITH NOCHECK ADD 
	CONSTRAINT [PK_JourneyAdminAreaType] PRIMARY KEY  CLUSTERED 
	(
		[JAATID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[JourneyModeType] WITH NOCHECK ADD 
	CONSTRAINT [PK_JourneyModeType] PRIMARY KEY  CLUSTERED 
	(
		[JMTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[JourneyPlanResponseType] WITH NOCHECK ADD 
	CONSTRAINT [PK_JourneyPlanResponseType] PRIMARY KEY  CLUSTERED 
	(
		[JPRTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[JourneyPrepositionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_JourneyPrepositionType] PRIMARY KEY  CLUSTERED 
	(
		[JPTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[JourneyRegionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_JourneyRegionType] PRIMARY KEY  CLUSTERED 
	(
		[JRTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[MapCommandType] WITH NOCHECK ADD 
	CONSTRAINT [PK_MapCommandType] PRIMARY KEY  CLUSTERED 
	(
		[MCTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[MapDisplayType] WITH NOCHECK ADD 
	CONSTRAINT [PK_MapDisplayType] PRIMARY KEY  CLUSTERED 
	(
		[MDTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[OperationalEvents] WITH NOCHECK ADD 
	CONSTRAINT [PK_OperationalEvents] PRIMARY KEY  CLUSTERED 
	(
		[OEID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[PageEntryType] WITH NOCHECK ADD 
	CONSTRAINT [PK_PageEntryType] PRIMARY KEY  CLUSTERED 
	(
		[PETID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ReferenceTransactionEvents] WITH NOCHECK ADD 
	CONSTRAINT [PK_ReferenceTransactionEvents] PRIMARY KEY  CLUSTERED 
	(
		[RTEDate],
		[RTEHour],
		[RTEMinute],
		[RTERTTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ReferenceTransactionType] WITH NOCHECK ADD 
	CONSTRAINT [PK_ReferenceTransactionType] PRIMARY KEY  CLUSTERED 
	(
		[RTTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RetailerHandoffType] WITH NOCHECK ADD 
	CONSTRAINT [PK_RetailerHandoffType] PRIMARY KEY  CLUSTERED 
	(
		[RHTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[SessionEvents] WITH NOCHECK ADD 
	CONSTRAINT [PK_SessionEvents] PRIMARY KEY  CLUSTERED 
	(
		[SEDate]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[UserPreferenceType] WITH NOCHECK ADD 
	CONSTRAINT [PK_UserPreferenceType] PRIMARY KEY  CLUSTERED 
	(
		[UPTID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[WeekDays] WITH NOCHECK ADD 
	CONSTRAINT [PK_WeekDays] PRIMARY KEY  CLUSTERED 
	(
		[WDID]
	)  ON [PRIMARY] 
GO

 CREATE  INDEX [IX_GazetteerEvents] ON [dbo].[GazetteerEvents]([GEDate], [GEHour], [GEHourQuarter], [GEGTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_JourneyPlanLocationEvents] ON [dbo].[JourneyPlanLocationEvents]([JPLEDate], [JPLEHour], [JPLEHourQuarter], [JPLEJPTID], [JPLEJAATID], [JPLEJPRTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_JourneyPlanModeEvents] ON [dbo].[JourneyPlanModeEvents]([JPMEDate], [JPMEHour], [JPMEHourQuarter], [JPMEJMTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_JourneyProcessingEvents] ON [dbo].[JourneyProcessingEvents]([JPEDate], [JPEHour], [JPEHourQuarter]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_JourneyWebRequestEvents] ON [dbo].[JourneyWebRequestEvents]([JWREDate], [JWREHour], [JWREHourQuarter], [JWREJRTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_LoginEvents] ON [dbo].[LoginEvents]([LEDate], [LEHour], [LEHourQuarter]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_MapEvents] ON [dbo].[MapEvents]([MEDate], [MEHour], [MEHourQuarter], [MEMDTID], [MEMCTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_PageEntryEvents] ON [dbo].[PageEntryEvents]([PEEDate], [PEEHour], [PEEHourQuarter], [PEEPETID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_RetailerHandoffEvents] ON [dbo].[RetailerHandoffEvents]([RHEDate], [RHEHour], [RHEHourQuarter], [RHERHTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_RoadPlanEvents] ON [dbo].[RoadPlanEvents]([RPEDate], [RPEHour], [RPEHourQuarter]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_UserPreferenceSaveEvents] ON [dbo].[UserPreferenceSaveEvents]([UPSEDate], [UPSEHour], [UPSEHourQuarter], [UPSEUPTID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_WorkloadEvents] ON [dbo].[WorkloadEvents]([WEDate], [WEHour], [WEMinute]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[GazetteerEvents] ADD 
	CONSTRAINT [FK_GazetteerEvents_GazetteerType] FOREIGN KEY 
	(
		[GEGTID]
	) REFERENCES [dbo].[GazetteerType] (
		[GTID]
	)
GO

ALTER TABLE [dbo].[JourneyAdminAreaType] ADD 
	CONSTRAINT [FK_JourneyAdminAreaType_JourneyRegionType] FOREIGN KEY 
	(
		[JAATJRTID]
	) REFERENCES [dbo].[JourneyRegionType] (
		[JRTID]
	)
GO

ALTER TABLE [dbo].[JourneyPlanLocationEvents] ADD 
	CONSTRAINT [FK_JourneyPlanLocationEvents_JourneyAdminAreaType] FOREIGN KEY 
	(
		[JPLEJAATID]
	) REFERENCES [dbo].[JourneyAdminAreaType] (
		[JAATID]
	),
	CONSTRAINT [FK_JourneyPlanLocationEvents_JourneyPlanResponseType] FOREIGN KEY 
	(
		[JPLEJPRTID]
	) REFERENCES [dbo].[JourneyPlanResponseType] (
		[JPRTID]
	),
	CONSTRAINT [FK_JourneyPlanLocationEvents_JourneyPrepositionType] FOREIGN KEY 
	(
		[JPLEJPTID]
	) REFERENCES [dbo].[JourneyPrepositionType] (
		[JPTID]
	)
GO

ALTER TABLE [dbo].[JourneyPlanModeEvents] ADD 
	CONSTRAINT [FK_JourneyPlanModeEvents_JourneyModeType] FOREIGN KEY 
	(
		[JPMEJMTID]
	) REFERENCES [dbo].[JourneyModeType] (
		[JMTID]
	)
GO

ALTER TABLE [dbo].[JourneyWebRequestEvents] ADD 
	CONSTRAINT [FK_JourneyWebRequestEvents_JourneyRegionType] FOREIGN KEY 
	(
		[JWREJRTID]
	) REFERENCES [dbo].[JourneyRegionType] (
		[JRTID]
	)
GO

ALTER TABLE [dbo].[MapEvents] ADD 
	CONSTRAINT [FK_MapEvents_MapCommandType] FOREIGN KEY 
	(
		[MEMCTID]
	) REFERENCES [dbo].[MapCommandType] (
		[MCTID]
	),
	CONSTRAINT [FK_MapEvents_MapDisplayType] FOREIGN KEY 
	(
		[MEMDTID]
	) REFERENCES [dbo].[MapDisplayType] (
		[MDTID]
	)
GO

ALTER TABLE [dbo].[PageEntryEvents] ADD 
	CONSTRAINT [FK_PageEntryEvents_PageEntryType] FOREIGN KEY 
	(
		[PEEPETID]
	) REFERENCES [dbo].[PageEntryType] (
		[PETID]
	)
GO

ALTER TABLE [dbo].[ReferenceTransactionEvents] ADD 
	CONSTRAINT [FK_ReferenceTransactionEvents_ReferenceTransactionType] FOREIGN KEY 
	(
		[RTERTTID]
	) REFERENCES [dbo].[ReferenceTransactionType] (
		[RTTID]
	)
GO

ALTER TABLE [dbo].[RetailerHandoffEvents] ADD 
	CONSTRAINT [FK_RetailerHandoffEvents_RetailerHandoffType] FOREIGN KEY 
	(
		[RHERHTID]
	) REFERENCES [dbo].[RetailerHandoffType] (
		[RHTID]
	)
GO

ALTER TABLE [dbo].[UserPreferenceSaveEvents] ADD 
	CONSTRAINT [FK_UserPreferenceSaveEvents_UserPreferenceType] FOREIGN KEY 
	(
		[UPSEUPTID]
	) REFERENCES [dbo].[UserPreferenceType] (
		[UPTID]
	)
GO

---------------------------------------------------------------------------------------------

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE DeleteReportData
(
	@reportdate DATETIME
)
AS

DELETE FROM  GazetteerEvents 			WHERE CONVERT( DATETIME, CONVERT( CHAR(10), GEDATE,   110 ) ) <= @reportdate
DELETE FROM  JourneyPlanLocationEvents  WHERE CONVERT( DATETIME, CONVERT( CHAR(10), JPLEDate, 110 ) ) <= @reportdate
DELETE FROM  JourneyPlanModeEvents  	WHERE CONVERT( DATETIME, CONVERT( CHAR(10), JPMEDate, 110 ) ) <= @reportdate
DELETE FROM  JourneyProcessingEvents  	WHERE CONVERT( DATETIME, CONVERT( CHAR(10), JPEDate,  110 ) ) <= @reportdate
DELETE FROM  JourneyWebRequestEvents  	WHERE CONVERT( DATETIME, CONVERT( CHAR(10), JWREDate, 110 ) ) <= @reportdate
DELETE FROM  LoginEvents  				WHERE CONVERT( DATETIME, CONVERT( CHAR(10), LEDate,   110 ) ) <= @reportdate
DELETE FROM  MapEvents 					WHERE CONVERT( DATETIME, CONVERT( CHAR(10), MEDate,   110 ) ) <= @reportdate
DELETE FROM  OperationalEvents 			WHERE CONVERT( DATETIME, CONVERT( CHAR(10), OETimeLogged, 110 ) ) <= @reportdate
DELETE FROM  PageEntryEvents  			WHERE CONVERT( DATETIME, CONVERT( CHAR(10), PEEDate,  110 ) ) <= @reportdate
DELETE FROM  ReferenceTransactionEvents WHERE CONVERT( DATETIME, CONVERT( CHAR(10), RTEDate,  110 ) ) <= @reportdate
DELETE FROM  RetailerHandoffEvents  	WHERE CONVERT( DATETIME, CONVERT( CHAR(10), RHEDate,  110 ) ) <= @reportdate
DELETE FROM  RoadPlanEvents  			WHERE CONVERT( DATETIME, CONVERT( CHAR(10), RPEDate,  110 ) ) <= @reportdate
DELETE FROM  UserPreferenceSaveEvents  	WHERE CONVERT( DATETIME, CONVERT( CHAR(10), UPSEDate, 110 ) ) <= @reportdate
DELETE FROM  WorkloadEvents  			WHERE CONVERT( DATETIME, CONVERT( CHAR(10), WEDate,   110 ) ) <= @reportdate
DELETE FROM  DataGatewayEvents  		WHERE CONVERT( DATETIME, CONVERT( CHAR(10), DGETimeLogged,   110 ) ) <= @reportdate
DELETE FROM  SessionEvents  			WHERE CONVERT( DATETIME, CONVERT( CHAR(10), SEDate,   110 ) ) <= @reportdate
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferInitialise
AS
IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_GazetteerEvents')
	DROP INDEX [dbo].[GazetteerEvents].[IX_GazetteerEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_JourneyPlanLocationEvents')
	DROP INDEX [dbo].[JourneyPlanLocationEvents].[IX_JourneyPlanLocationEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_JourneyPlanModeEvents')
	DROP INDEX [dbo].[JourneyPlanModeEvents].[IX_JourneyPlanModeEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_JourneyProcessingEvents')
	DROP INDEX [dbo].[JourneyProcessingEvents].[IX_JourneyProcessingEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_JourneyWebRequestEvents')
	DROP INDEX [dbo].[JourneyWebRequestEvents].[IX_JourneyWebRequestEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_LoginEvents')
	DROP INDEX [dbo].[LoginEvents].[IX_LoginEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_MapEvents')
	DROP INDEX [dbo].[MapEvents].[IX_MapEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_PageEntryEvents')
	DROP INDEX [dbo].[PageEntryEvents].[IX_PageEntryEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_RetailerHandoffEvents')
	DROP INDEX [dbo].[RetailerHandoffEvents].[IX_RetailerHandoffEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_RoadPlanEvents')
	DROP INDEX [dbo].[RoadPlanEvents].[IX_RoadPlanEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_UserPreferenceSaveEvents')
	DROP INDEX [dbo].[UserPreferenceSaveEvents].[IX_UserPreferenceSaveEvents]

IF EXISTS (SELECT name FROM sysindexes WHERE name = 'IX_WorkloadEvents')
	DROP INDEX [dbo].[WorkloadEvents].[IX_WorkloadEvents]
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferComplete
AS
CREATE INDEX [IX_GazetteerEvents] ON [dbo].[GazetteerEvents]([GEDate], [GEHour], [GEHourQuarter], [GEGTID]) ON [PRIMARY]
CREATE INDEX [IX_JourneyPlanLocationEvents] ON [dbo].[JourneyPlanLocationEvents]([JPLEDate], [JPLEHour], [JPLEHourQuarter], [JPLEJPTID], [JPLEJAATID], [JPLEJPRTID]) ON [PRIMARY]
CREATE INDEX [IX_JourneyPlanModeEvents] ON [dbo].[JourneyPlanModeEvents]([JPMEDate], [JPMEHour], [JPMEHourQuarter], [JPMEJMTID]) ON [PRIMARY]
CREATE INDEX [IX_JourneyProcessingEvents] ON [dbo].[JourneyProcessingEvents]([JPEDate], [JPEHour], [JPEHourQuarter]) ON [PRIMARY]
CREATE INDEX [IX_JourneyWebRequestEvents] ON [dbo].[JourneyWebRequestEvents]([JWREDate], [JWREHour], [JWREHourQuarter], [JWREJRTID]) ON [PRIMARY]
CREATE INDEX [IX_LoginEvents] ON [dbo].[LoginEvents]([LEDate], [LEHour], [LEHourQuarter]) ON [PRIMARY]
CREATE INDEX [IX_MapEvents] ON [dbo].[MapEvents]([MEDate], [MEHour], [MEHourQuarter], [MEMDTID], [MEMCTID]) ON [PRIMARY]
CREATE INDEX [IX_PageEntryEvents] ON [dbo].[PageEntryEvents]([PEEDate], [PEEHour], [PEEHourQuarter], [PEEPETID]) ON [PRIMARY]
CREATE INDEX [IX_RetailerHandoffEvents] ON [dbo].[RetailerHandoffEvents]([RHEDate], [RHEHour], [RHEHourQuarter], [RHERHTID]) ON [PRIMARY]
CREATE INDEX [IX_RoadPlanEvents] ON [dbo].[RoadPlanEvents]([RPEDate], [RPEHour], [RPEHourQuarter]) ON [PRIMARY]
CREATE INDEX [IX_UserPreferenceSaveEvents] ON [dbo].[UserPreferenceSaveEvents]([UPSEDate], [UPSEHour], [UPSEHourQuarter], [UPSEUPTID]) ON [PRIMARY]
CREATE INDEX [IX_WorkloadEvents] ON [dbo].[WorkloadEvents]([WEDate], [WEHour], [WEMinute]) ON [PRIMARY]
GO

---------------------------------------------------------------------------------------------

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

---------------------------------------------------------------------------------------------
