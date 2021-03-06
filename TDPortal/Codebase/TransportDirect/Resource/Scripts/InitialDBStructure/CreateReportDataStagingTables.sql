-- ***********************************************
-- NAME 	: CreateReportDataStagingTables.sql
-- DESCRIPTION 	: Creates the ReportDataStaging tables, indexes, constraints,
--		: foreign keys, sprocs and functions.
-- ************************************************
-- $Log$

USE [PermanentPortal]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ReportStagingDataAudit_ReportStagingDataAuditType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ReportStagingDataAudit] DROP CONSTRAINT FK_ReportStagingDataAudit_ReportStagingDataAuditType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ReportStagingDataAudit_ReportStagingDataType]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ReportStagingDataAudit] DROP CONSTRAINT FK_ReportStagingDataAudit_ReportStagingDataType
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddGazetteerEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddGazetteerEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddJourneyPlanRequestEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddJourneyPlanRequestEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddJourneyPlanRequestVerboseEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddJourneyPlanRequestVerboseEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddJourneyPlanResultsEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddJourneyPlanResultsEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddJourneyPlanResultsVerboseEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddJourneyPlanResultsVerboseEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddJourneyWebRequestEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddJourneyWebRequestEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddLocationRequestEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddLocationRequestEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddLoginEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddLoginEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddMapEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddMapEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddOperationalEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddOperationalEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddPageEntryEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddPageEntryEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddReferenceTransactionEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddReferenceTransactionEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddRetailerHandoffEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddRetailerHandoffEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddDataGatewayEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddDatagatewayEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddUserPreferenceSaveEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddUserPreferenceSaveEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddWorkloadEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[AddWorkloadEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetLatestImported]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetLatestImported]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateAuditEvent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[UpdateAuditEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GazetteerEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[GazetteerEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyPlanRequestEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyPlanRequestEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyPlanRequestVerboseEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyPlanRequestVerboseEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyPlanResultsEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyPlanResultsEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyPlanResultsVerboseEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyPlanResultsVerboseEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[JourneyWebRequestEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[JourneyWebRequestEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[LocationRequestEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[LocationRequestEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[LoginEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[LoginEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[MapEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[MapEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[OperationalEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[OperationalEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[PageEntryEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[PageEntryEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReferenceTransactionEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ReferenceTransactionEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReportStagingDataAudit]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ReportStagingDataAudit]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReportStagingDataAuditType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ReportStagingDataAuditType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReportStagingDataType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ReportStagingDataType]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RetailerHandoffEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[RetailerHandoffEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DataGatewayEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DataGatewayEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UserPreferenceSaveEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[UserPreferenceSaveEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[WorkloadEvent]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[WorkloadEvent]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeleteImportedStagingData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeleteImportedStagingData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeleteStagingData]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[DeleteStagingData]
GO

CREATE TABLE [dbo].[GazetteerEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[EventCategory] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserLoggedOn] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyPlanRequestEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[JourneyPlanRequestId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Air] [bit] NULL ,
	[Bus] [bit] NULL ,
	[Car] [bit] NULL ,
	[Coach] [bit] NULL ,
	[Cycle] [bit] NULL ,
	[Drt] [bit] NULL ,
	[Ferry] [bit] NULL ,
	[Metro] [bit] NULL ,
	[Rail] [bit] NULL ,
	[Taxi] [bit] NULL ,
	[Tram] [bit] NULL ,
	[Underground] [bit] NULL ,
	[Walk] [bit] NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserLoggedOn] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyPlanRequestVerboseEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[JourneyPlanRequestId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[JourneyRequestData] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserLoggedOn] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyPlanResultsEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[JourneyPlanRequestId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ResponseCategory] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserLoggedOn] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyPlanResultsVerboseEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[JourneyPlanRequestId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[JourneyResultsData] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserLoggedOn] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[JourneyWebRequestEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[JourneyWebRequestId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Submitted] [datetime] NULL ,
	[RegionCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Success] [bit] NULL ,
	[RefTransaction] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LocationRequestEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[JourneyPlanRequestId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[PrepositionCategory] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[AdminAreaCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[RegionCode] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[LoginEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserLoggedOn] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[MapEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[CommandCategory] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Submitted] [datetime] NULL ,
	[DisplayCategory] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserLoggedOn] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[OperationalEvent] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Message] [varchar] (500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[MachineName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[AssemblyName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[MethodName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TypeName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Level] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Category] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[Target] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[PageEntryEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[Page] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserLoggedOn] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReferenceTransactionEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[EventType] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ServiceLevelAgreement] [bit] NULL ,
	[Submitted] [datetime] NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TimeLogged] [datetime] NULL ,
	[Successful] [bit] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReportStagingDataAudit] (
	[RSDAID] [int] IDENTITY (1, 1) NOT NULL ,
	[RSDTID] [int] NOT NULL ,
	[RSDATID] [int] NOT NULL ,
	[Event] [datetime] NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReportStagingDataAuditType] (
	[RSDATID] [int] NOT NULL ,
	[RSDATName] [varchar] (30) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[ReportStagingDataType] (
	[RSDTID] [int] NOT NULL ,
	[RSDTName] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RetailerHandoffEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[RetailerId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[UserLoggedOn] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[UserPreferenceSaveEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[EventCategory] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[DataGatewayEvent] (
	[Id] [bigint] IDENTITY (1, 1) NOT NULL ,
	[FeedId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[SessionId] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[FileName] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TimeStarted] [datetime] NULL,
	[TimeFinished] [datetime] NULL,
	[SuccessFlag] [bit] NULL ,
	[ErrorCode] [int] NULL,
	[UserLoggedOn] [bit] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[WorkloadEvent] (
	[Id] [int] IDENTITY (1, 1) NOT NULL ,
	[Requested] [datetime] NULL ,
	[TimeLogged] [datetime] NULL 
) ON [PRIMARY]
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

CREATE PROCEDURE AddGazetteerEvent
(
	@EventCategory varchar(50),
	@SessionId varchar(50),
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into GazetteerEvent Table'

    Insert into GazetteerEvent
    ( 
	EventCategory,
	SessionId,
	UserLoggedOn,
	TimeLogged
    )

    Values
    (
	@EventCategory,
	@SessionId,	
	@UserLoggedOn,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddJourneyPlanRequestEvent
(
	@JourneyPlanRequestId varchar(50),
	@Air bit,
	@Bus bit,
	@Car bit,
	@Coach bit,
	@Cycle bit,
	@Drt bit,
	@Ferry bit,
	@Metro bit,
	@Rail bit,
	@Taxi bit,
	@Tram bit,
	@Underground bit,
	@Walk bit,
	@SessionId varchar(50),
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into JourneyPlanRequestEvent Table'

    Insert into JourneyPlanRequestEvent
    ( 
	JourneyPlanRequestId,
	Air,
	Bus,
	Car,
	Coach,
	Cycle,
	Drt,
	Ferry,
	Metro,
	Rail,
	Taxi,
	Tram,
	Underground,
	Walk,
	SessionId,
	UserLoggedOn,
	TimeLogged	
    )

    Values
    (
	@JourneyPlanRequestId,
	@Air,
	@Bus,
	@Car,
	@Coach,
	@Cycle,
	@Drt,
	@Ferry,
	@Metro,
	@Rail,
	@Taxi,
	@Tram,
	@Underground,
	@Walk,
	@SessionId,
	@UserLoggedOn,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddJourneyPlanRequestVerboseEvent
(
	@JourneyPlanRequestId varchar(50),
	@JourneyRequestData varchar(4000),
	@SessionId varchar(50),
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into JourneyPlanRequestVerboseEvent Table'

    Insert into JourneyPlanRequestVerboseEvent
    ( 
	JourneyPlanRequestId,
	JourneyRequestData,
	SessionId,
	UserLoggedOn,
	TimeLogged
    )

    Values
    (
	@JourneyPlanRequestId,
	@JourneyRequestData,
	@SessionId,
	@UserLoggedOn,
	@TimeLogged	
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddJourneyPlanResultsEvent
(
	@JourneyPlanRequestId varchar(50),
	@ResponseCategory varchar(50),
	@SessionId varchar(50),
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into JourneyPlanResultsEvent Table'

    Insert into JourneyPlanResultsEvent
    ( 
	JourneyPlanRequestId,
	ResponseCategory,
	SessionId,
	UserLoggedOn,
	TimeLogged
    )

    Values
    (
	@JourneyPlanRequestId,
	@ResponseCategory,
	@SessionId,
	@UserLoggedOn,
	@TimeLogged	
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddJourneyPlanResultsVerboseEvent
(
	@JourneyPlanRequestId varchar(50),
	@JourneyResultsData varchar(4000),
	@SessionId varchar(50),
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into JourneyPlanResultsVerboseEvent Table'

    Insert into JourneyPlanResultsVerboseEvent
    ( 
	JourneyPlanRequestId,
	JourneyResultsData,
	SessionId,
	UserLoggedOn,
	TimeLogged
    )

    Values
    (
	@JourneyPlanRequestId,
	@JourneyResultsData,
	@SessionId,
	@UserLoggedOn,
	@TimeLogged	
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddJourneyWebRequestEvent
(
	@JourneyWebRequestId varchar(50),
	@SessionId varchar(50),
	@Submitted datetime,
	@RegionCode varchar(50),
	@Success bit,
	@RefTransaction bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into JourneyWebRequestEvent Table'

    Insert into JourneyWebRequestEvent
    (
	JourneyWebRequestId,
	SessionId,
	Submitted,
	RegionCode,
	Success,
	RefTransaction,
	TimeLogged
    )
    Values
    (
	@JourneyWebRequestId,
	@SessionId,
	@Submitted,
	@RegionCode,
	@Success,
	@RefTransaction,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddLocationRequestEvent
(
	@JourneyPlanRequestId varchar(50),
	@PrepositionCategory varchar(50),
	@AdminAreaCode varchar(50),
	@RegionCode varchar(50),
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into LocationRequestEvent Table'

    Insert into LocationRequestEvent
    (
	JourneyPlanRequestId,
	PrepositionCategory,
	AdminAreaCode,
	RegionCode,
	TimeLogged
    )
    Values
    (
	@JourneyPlanRequestId,
	@PrepositionCategory,
	@AdminAreaCode,
	@RegionCode,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddLoginEvent
(
	@SessionId varchar(50),
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into LoginEvent Table'

    Insert into LoginEvent
    ( 
	SessionId,
	UserLoggedOn,
	TimeLogged
    )

    Values
    (
	@SessionId,
	@UserLoggedOn,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddMapEvent
(
	@CommandCategory varchar(50),
	@Submitted datetime,
	@DisplayCategory varchar(50),
	@SessionId varchar(50),
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into MapEvent Table'

    Insert into MapEvent
    ( 
	CommandCategory,
	Submitted,
	DisplayCategory,
	SessionId,
	UserLoggedOn,
	TimeLogged
    )

    Values
    (
	@CommandCategory,
	@Submitted,
	@DisplayCategory,
	@SessionId,
	@UserLoggedOn,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddOperationalEvent
(
	@SessionId varchar(50),
	@Message varchar(500),
	@MachineName varchar(50),
	@AssemblyName varchar(50),
	@MethodName varchar(50),
	@TypeName varchar(50),
	@Level varchar(50),
	@Category varchar(50),
	@Target varchar(50),
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsertOperationalEvent AS nvarchar(256)
    set @localized_string_UnableToInsertOperationalEvent = 'Unable to Insert a new record into OperationalEvent Table'

    Insert into OperationalEvent
    (
	SessionId,
	Message,
	MachineName,
	AssemblyName,
	MethodName,
	TypeName,
	Level,
	Category,
	Target,
	TimeLogged
    )
    Values
    (
	@SessionId,
	@Message,
	@MachineName,
	@AssemblyName,
	@MethodName,
	@TypeName,
	@Level,
	@Category,
	@Target,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsertOperationalEvent, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddPageEntryEvent
(
	@Page varchar(50),
	@SessionId varchar(50),
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into PageEntryEvent Table'

    Insert into PageEntryEvent
    ( 
	Page,
	SessionId,
	UserLoggedOn,
	TimeLogged
    )

    Values
    (
	@Page,
	@SessionId,
	@UserLoggedOn,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddReferenceTransactionEvent
(
	@EventType varchar(50),
	@ServiceLevelAgreement bit,
	@Submitted datetime,
	@SessionId varchar(50),
	@TimeLogged datetime,
	@Successful bit
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into ReferenceTransactionEvent Table'

    Insert into ReferenceTransactionEvent
    ( 
	EventType,
	ServiceLevelAgreement,
	Submitted,
	SessionId,
	TimeLogged,
	Successful
    )
    Values
    (
	@EventType,
	@ServiceLevelAgreement,
	@Submitted,
	@SessionId,
	@TimeLogged,
	@Successful
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddRetailerHandoffEvent
(
	@RetailerId varchar(50),
	@SessionId varchar(50),
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into RetailerHandoffEvent Table'

    Insert into RetailerHandoffEvent
    ( 
	RetailerId,
	SessionId,
	UserLoggedOn,
	TimeLogged
    )

    Values
    (
	@RetailerId,
	@SessionId,
	@UserLoggedOn,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddDataGatewayEvent
(
	@FeedId varchar(50),
	@SessionId varchar(50),
	@FileName varchar (100),
	@TimeStarted datetime,
	@TimeFinished datetime,
	@SuccessFlag bit,
	@ErrorCode int,
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into DataGatewayEvent Table'

    Insert into DataGatewayEvent
    ( 
	FeedId,
	SessionId,
	FileName,
	TimeStarted,
	TimeFinished,
	SuccessFlag,
	ErrorCode,
	UserLoggedOn,
	TimeLogged
    )

    Values
    (
	@FeedId,
	@SessionId,
	@FileName,
	@TimeStarted,
	@TimeFinished,
	@SuccessFlag,
	@ErrorCode,
	@UserLoggedOn,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO

CREATE PROCEDURE AddUserPreferenceSaveEvent
(
	@EventCategory varchar(50),
	@SessionId varchar(50),
	@UserLoggedOn bit,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsert AS nvarchar(256)
    set @localized_string_UnableToInsert = 'Unable to Insert a new record into RetailerHandoffEvent Table'

    Insert into UserPreferenceSaveEvent
    ( 
	EventCategory,
	SessionId,
	TimeLogged
    )

    Values
    (
	@EventCategory,
	@SessionId,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsert, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO



CREATE PROCEDURE AddWorkloadEvent
(
	@Requested datetime,
	@TimeLogged datetime
)
As
    set nocount off

    declare @localized_string_UnableToInsertWorkloadEvent AS nvarchar(256)
    set @localized_string_UnableToInsertWorkloadEvent = 'Unable to Insert a new record into WorkloadEvent Table'

    Insert into WorkloadEvent
    ( 
	Requested,
	TimeLogged
    )

    Values
    (
	@Requested,
	@TimeLogged
    )

    if @@error <> 0
    Begin
        raiserror (@localized_string_UnableToInsertWorkloadEvent, 1,1)
        return -1
    end
    else
    begin
        return @@rowcount
    End
GO


CREATE PROCEDURE GetLatestImported
(
	@DataName varchar(50)
)
As

	declare @RSDTID as int
    set @RSDTID = (Select RSDTID from ReportStagingDataType where RSDTName = @DataName)

Select 
	Max(RSDA.Event) as AuditDate
From 
	ReportStagingDataAudit RSDA
Where
	(RSDA.RSDATID =
	(SELECT RSDATID
	 FROM ReportStagingDataAuditType
	 WHERE RSDATName = 'LatestImported'))
	AND
	(RSDA.RSDTID = @RSDTID)

GO


CREATE PROCEDURE UpdateAuditEvent
(
	@DataName varchar(50),
	@Date datetime,
	@AuditType varchar(50)
)
As
    set nocount off

    declare @RSDTID as int
    set @RSDTID = (Select RSDTID from ReportStagingDataType where RSDTName = @DataName)

    declare @RSDATID as int
    set @RSDATID = (Select RSDATID from ReportStagingDataAuditType where RSDATName = @AuditType)
	
	Update ReportStagingDataAudit
		Set Event = @Date
		Where (RSDTID = @RSDTID) AND (RSDATID = @RSDATID)
	
GO


CREATE PROCEDURE DeleteImportedStagingData
	@StagingTableName varchar(30),
	@LatestImported DATETIME
AS

DECLARE @sqlStr VARCHAR (300)

SET @sqlStr    = 'DELETE FROM ' + @StagingTableName + ' WHERE CONVERT( DATETIME, CONVERT( VARCHAR(10), TimeLogged, 110 ) ) <= CONVERT( DATETIME, ''' + CONVERT( VARCHAR(10), @LatestImported, 110 ) + ''')'
 
EXEC( @sqlStr )

GO


CREATE PROCEDURE DeleteStagingData
	@StagingTableName varchar(30)
AS

DECLARE @SQL nvarchar(200)

SET @SQL=N'
DELETE FROM ' + @StagingTableName

EXEC sp_executesql @SQL

GO

---------------------------------------------------------------------------------------------
SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

CREATE PROCEDURE dbo.TransferInitialise
	@ConnectionString varchar(255)
AS
EXEC sp_dropserver @server='ReportServer'

EXEC sp_addlinkedserver
	@server = 'ReportServer',
	@provider = 'MSDASQL',
	@srvproduct = '',
	@provstr = @ConnectionString

EXEC sp_serveroption 'ReportServer', 'rpc', 'TRUE'

EXEC sp_serveroption 'ReportServer', 'rpc out', 'TRUE'

EXEC ReportServer.Reporting.dbo.TransferInitialise
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferComplete
AS
EXEC ReportServer.Reporting.dbo.TransferComplete
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferDataGatewayEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.DataGatewayEvents
WHERE CONVERT(varchar(10), DGETimeLogged, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.DataGatewayEvents
(
	DGEID,
	DGEFeedID,
	DGESessionID,
	DGEFileName,
	DGETimeStarted,
	DGETimeFinished,
	DGESuccessFlag,
	DGEErrorCode,
	DGEUserLoggedOn,
	DGETimeLogged
)
SELECT
	[Id],
	FeedId,
	SessionId,
	[FileName],
	TimeStarted,
	TimeFinished,
	SuccessFlag,
	ErrorCode,
	UserLoggedOn,
	TimeLogged
FROM DataGatewayEvent
WHERE CONVERT(varchar(10), TimeLogged, 121) = @Date
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferGazetteerEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.GazetteerEvents
WHERE CONVERT(varchar(10), GEDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.GazetteerEvents
(
	GEDate,
	GEHour,
	GEHourQuarter,
	GEGTID,
	GEWeekDay,
	GELoggedOn,
	GECount
)
SELECT
	CAST(CONVERT(varchar(10), GE.TimeLogged, 121) AS datetime) AS GEDate,
	DATEPART(hour, GE.TimeLogged) AS GEHour,
	CAST(DATEPART(minute, GE.TimeLogged) / 15 AS smallint) AS GEHourQuarter,
	GT.GTID AS GEGTID,
	DATEPART(weekday, GE.TimeLogged) AS GEWeekDay,
	GE.UserLoggedOn AS GELoggedOn,
	COUNT(*) AS GECount
FROM GazetteerEvent GE
LEFT OUTER JOIN ReportServer.Reporting.dbo.GazetteerType GT ON GE.EventCategory = GT.GTCode
WHERE CONVERT(varchar(10), GE.TimeLogged, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), GE.TimeLogged, 121) AS datetime),
	DATEPART(hour, GE.TimeLogged),
	CAST(DATEPART(minute, GE.TimeLogged) / 15 AS smallint),
	DATEPART(weekday, GE.TimeLogged),
	GT.GTID,
	GE.UserLoggedOn
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferJourneyPlanLocationEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.JourneyPlanLocationEvents
WHERE CONVERT(varchar(10), JPLEDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.JourneyPlanLocationEvents
(
	JPLEDate,
	JPLEHour,
	JPLEHourQuarter,
	JPLEWeekDay,
	JPLEJPTID,
	JPLEJAATID,
	JPLEJPRTID,
	JPLELoggedOn,
	JPLECount
)
SELECT 
	CAST(CONVERT(varchar(10), JPRQE.TimeLogged, 121) AS datetime) AS JPLEDate,
	DATEPART(hour, JPRQE.TimeLogged) AS JPLEHour,
	CAST(DATEPART(minute, JPRQE.TimeLogged) / 15 AS smallint) AS JPLEHourQuarter,
	DATEPART(weekday, JPRQE.TimeLogged) AS JPLEWeekDay,
	JPT.JPTID AS JPLEJPTID,
	JAAT.JAATID AS JPLEJAATID,
	JPRT.JPRTID AS JPLEJPRTID,
	JPRQE.UserLoggedOn AS JPLELoggedOn,
	COUNT(*) AS JPLECount
FROM JourneyPlanRequestEvent JPRQE
INNER JOIN LocationRequestEvent LRE ON JPRQE.JourneyPlanRequestId = LRE.JourneyPlanRequestId
INNER JOIN JourneyPlanResultsEvent JPRSE ON JPRQE.JourneyPlanRequestId = JPRSE.JourneyPlanRequestId
LEFT OUTER JOIN ReportServer.Reporting.dbo.JourneyPrepositionType JPT ON LRE.PrepositionCategory = JPT.JPTCode
LEFT OUTER JOIN ReportServer.Reporting.dbo.JourneyAdminAreaType JAAT ON LRE.AdminAreaCode = JAAT.JAATCode
LEFT OUTER JOIN ReportServer.Reporting.dbo.JourneyPlanResponseType JPRT ON JPRSE.ResponseCategory = JPRT.JPRTCode
WHERE CONVERT(varchar(10), JPRQE.TimeLogged, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), JPRQE.TimeLogged, 121) AS datetime),
	DATEPART(hour, JPRQE.TimeLogged),
	CAST(DATEPART(minute, JPRQE.TimeLogged) / 15 AS smallint),
	DATEPART(weekday, JPRQE.TimeLogged),
	JPT.JPTID,
	JAAT.JAATID,
	JPRT.JPRTID,
	JPRQE.UserLoggedOn
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferJourneyPlanModeEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.JourneyPlanModeEvents
WHERE CONVERT(varchar(10), JPMEDate, 121) = @Date

DECLARE @SQLBase nvarchar(4000)
DECLARE @SQL nvarchar(4000)
DECLARE @JMTID smallint
DECLARE @JMTDescription varchar(30)

SET @SQLBase = N'
INSERT INTO ReportServer.Reporting.dbo.JourneyPlanModeEvents
(
	JPMEDate,
	JPMEHour,
	JPMEHourQuarter,
	JPMEWeekDay,
	JPMEJMTID,
	JPMELoggedOn,
	JPMECount
)
SELECT
	CAST(CONVERT(varchar(10), TimeLogged, 121) AS datetime) AS JPMEDate,
	DATEPART(hour, TimeLogged) AS JPMEHour,
	CAST(DATEPART(minute, TimeLogged) / 15 AS smallint) AS JPMEHourQuarter,
	DATEPART(weekday, TimeLogged) AS JPMEWeekDay,
	[@JMTID] AS JPMEJMTID,
	UserLoggedOn AS JPMELoggedOn,
	SUM(CAST([@JMTDescription] AS smallint)) AS JPMECount
FROM JourneyPlanRequestEvent
WHERE CONVERT(varchar(10), TimeLogged, 121) = ''[@Date]''
GROUP BY
	CAST(CONVERT(varchar(10), TimeLogged, 121) AS datetime),
	DATEPART(hour, TimeLogged),
	CAST(DATEPART(minute, TimeLogged) / 15 AS smallint),
	DATEPART(weekday, TimeLogged),
	UserLoggedOn
HAVING SUM(CAST([@JMTDescription] AS smallint)) > 0
'

DECLARE LocalCursor CURSOR FORWARD_ONLY READ_ONLY FOR
SELECT JMTID, JMTDescription
FROM ReportServer.Reporting.dbo.JourneyModeType

OPEN LocalCursor

FETCH NEXT FROM LocalCursor INTO @JMTID, @JMTDescription

WHILE @@Fetch_Status <> -1
BEGIN
	IF @@Fetch_Status <> -2
	BEGIN
		SET @SQL = @SQLBase
		SET @SQL = REPLACE(@SQL, '[@Date]', @Date)
		SET @SQL = REPLACE(@SQL, '[@JMTID]', CAST(@JMTID AS varchar(10)))
		SET @SQL = REPLACE(@SQL, '[@JMTDescription]', @JMTDescription)
		EXEC sp_ExecuteSQL @SQL
	END
	FETCH NEXT FROM LocalCursor INTO @JMTID, @JMTDescription
END

CLOSE LocalCursor
DEALLOCATE LocalCursor
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferJourneyProcessingEvents
	@Date varchar(10),
	@MsWindow int
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.JourneyProcessingEvents
WHERE CONVERT(varchar(10), JPEDate, 121) = @Date

DECLARE @JourneyPlanRequestId varchar(50)
DECLARE @Submitted datetime
DECLARE @TimeLogged datetime
DECLARE @MsDuration int
DECLARE @RefTransaction bit

DECLARE @StoreJourneyPlanRequestId varchar(50)
DECLARE @StoreWindowStart datetime
DECLARE @StoreWindowEnd datetime
DECLARE @StoreMaxMsDuration int
DECLARE @StoreRefTransaction bit
DECLARE @OrigWindowStart datetime
DECLARE @SumMaxMsDuration int

CREATE TABLE #Windows
(
	JourneyPlanRequestId varchar(50),
	WindowStart datetime,
	WindowEnd datetime,
	SumMaxMsDuration int,
	RefTransaction bit
)

DECLARE LocalCursor CURSOR FORWARD_ONLY READ_ONLY FOR
SELECT
	JPRQE.JourneyPlanRequestId,
	JWRE1.Submitted,
	JWRE1.TimeLogged,
	DATEDIFF(millisecond, JWRE1.Submitted, JWRE1.TimeLogged) AS MsDuration,
	JWRE1.RefTransaction
FROM JourneyWebRequestEvent JWRE1
INNER JOIN JourneyPlanRequestEvent JPRQE ON LEFT(JWRE1.JourneyWebRequestId, 24) = JPRQE.JourneyPlanRequestId
WHERE CONVERT(varchar(10), JWRE1.TimeLogged, 121) = @Date
ORDER BY
	JPRQE.JourneyPlanRequestId,
	JWRE1.Submitted

OPEN LocalCursor

SET @StoreJourneyPlanRequestId = ''
SET @StoreWindowStart = CAST(0 AS datetime)
SET @StoreWindowEnd = CAST(0 AS datetime)
SET @StoreMaxMsDuration = 0
SET @StoreRefTransaction = 0
SET @OrigWindowStart = 0
SET @SumMaxMsDuration = 0

FETCH NEXT FROM LocalCursor INTO @JourneyPlanRequestId, @Submitted, @TimeLogged, @MsDuration, @RefTransaction

WHILE @@Fetch_Status <> -1
BEGIN
	IF @@Fetch_Status <> -2
	BEGIN
		IF @JourneyPlanRequestId <> @StoreJourneyPlanRequestId
		BEGIN
			SET @StoreJourneyPlanRequestId = @JourneyPlanRequestId
			SET @StoreWindowStart = @Submitted
			SET @StoreWindowEnd = @TimeLogged
			SET @StoreMaxMsDuration = @MsDuration
			SET @StoreRefTransaction = @RefTransaction
			SET @OrigWindowStart = @Submitted
			SET @SumMaxMsDuration = 0
		END
		
		FETCH NEXT FROM LocalCursor INTO @JourneyPlanRequestId, @Submitted, @TimeLogged, @MsDuration, @RefTransaction
		
		IF @@Fetch_Status = -1 OR @@Fetch_Status = -2
		BEGIN
			INSERT INTO #Windows
			(
				JourneyPlanRequestId,
				WindowStart,
				WindowEnd,
				SumMaxMsDuration,
				RefTransaction
			)
			VALUES
			(
				@StoreJourneyPlanRequestId,
				@OrigWindowStart,
				@StoreWindowEnd,
				@SumMaxMsDuration + @StoreMaxMsDuration,
				@StoreRefTransaction
			)
		END
		ELSE IF @JourneyPlanRequestId <> @StoreJourneyPlanRequestId
		BEGIN
			INSERT INTO #Windows
			(
				JourneyPlanRequestId,
				WindowStart,
				WindowEnd,
				SumMaxMsDuration,
				RefTransaction
			)
			VALUES
			(
				@StoreJourneyPlanRequestId,
				@OrigWindowStart,
				@StoreWindowEnd,
				@SumMaxMsDuration + @StoreMaxMsDuration,
				@StoreRefTransaction
			)
		END
		ELSE
		BEGIN
			IF DATEDIFF(millisecond, @StoreWindowStart, @Submitted) BETWEEN 0 AND @MsWindow
			BEGIN
				IF @StoreWindowEnd < @TimeLogged
					SET @StoreWindowEnd = @TimeLogged
				
				IF @StoreMaxMsDuration < @MsDuration
					SET @StoreMaxMsDuration = @MsDuration
				
				IF @StoreRefTransaction < @RefTransaction
					SET @StoreRefTransaction = @RefTransaction
			END
			
			IF @Submitted > @StoreWindowEnd
			BEGIN
				SET @SumMaxMsDuration = @SumMaxMsDuration + @StoreMaxMsDuration
				
				SET @StoreJourneyPlanRequestId = @JourneyPlanRequestId
				SET @StoreWindowStart = @Submitted
				SET @StoreWindowEnd = @TimeLogged
				SET @StoreMaxMsDuration = @MsDuration
				SET @StoreRefTransaction = @RefTransaction
			END
		END
	END
END

CLOSE LocalCursor
DEALLOCATE LocalCursor

INSERT INTO ReportServer.Reporting.dbo.JourneyProcessingEvents
(
	JPEDate,
	JPEHour,
	JPEHourQuarter,
	JPEWeekDay,
	JPERefTransaction,
	JPEAvMsProcessingDuration,
	JPEAvMsWaitingDuration,
	JPECount
)
SELECT
	CAST(CONVERT(varchar(10), JPRQE.TimeLogged, 121) AS datetime) AS JPEDate,
	DATEPART(hour, JPRQE.TimeLogged) AS JPEHour,
	CAST(DATEPART(minute, JPRQE.TimeLogged) / 15 AS smallint) AS JPEHourQuarter,
	DATEPART(weekday, JPRQE.TimeLogged) AS JPEWeekDay,
	WIN.RefTransaction AS JPERefTransaction,
	AVG(ISNULL(CAST(DATEDIFF(millisecond, JPRQE.TimeLogged, JPRSE.TimeLogged) AS decimal(18, 0)), 0)) AS JPEAvMsProcessingDuration,
	AVG(ISNULL(CAST(WIN.SumMaxMsDuration AS decimal(18, 0)), 0)) AS JPEAvMsWaitingDuration,
	COUNT(JPRQE.TimeLogged) AS JPECount
FROM JourneyPlanRequestEvent JPRQE
INNER JOIN JourneyPlanResultsEvent JPRSE ON JPRQE.JourneyPlanRequestId = JPRSE.JourneyPlanRequestId
INNER JOIN #Windows WIN ON JPRQE.JourneyPlanRequestId = WIN.JourneyPlanRequestId
WHERE CONVERT(varchar(10), JPRQE.TimeLogged, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), JPRQE.TimeLogged, 121) AS datetime),
	DATEPART(hour, JPRQE.TimeLogged),
	CAST(DATEPART(minute, JPRQE.TimeLogged) / 15 AS smallint),
	DATEPART(weekday, JPRQE.TimeLogged),
	WIN.RefTransaction

DROP TABLE #Windows
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferJourneyWebRequestEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.JourneyWebRequestEvents
WHERE CONVERT(varchar(10), JWREDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.JourneyWebRequestEvents
(
	JWREDate,
	JWREHour,
	JWREHourQuarter,
	JWREWeekDay,
	JWREJRTID,
	JWRESuccessful,
	JWREAvMsDuration,
	JWRECount
)
SELECT
	CAST(CONVERT(varchar(10), JWRE.Submitted, 121) AS datetime) AS JWREDate,
	DATEPART(hour, JWRE.Submitted) AS JWREHour,
	CAST(DATEPART(minute, JWRE.Submitted) / 15 AS smallint) AS JWREHourQuarter,
	DATEPART(weekday, JWRE.Submitted) AS JWREWeekDay,
	JRT.JRTID AS JWREJRTID,
	JWRE.Success AS JWRESuccessful,
	AVG(CAST(DATEDIFF(millisecond, JWRE.Submitted, JWRE.TimeLogged) AS decimal(18, 0))) AS JWREAvMsDuration,
	COUNT(*) AS JWRECount
FROM JourneyWebRequestEvent JWRE
LEFT OUTER JOIN ReportServer.Reporting.dbo.JourneyRegionType JRT ON JWRE.RegionCode = JRT.JRTCode
WHERE CONVERT(varchar(10), JWRE.Submitted, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), JWRE.Submitted, 121) AS datetime),
	DATEPART(hour, JWRE.Submitted),
	CAST(DATEPART(minute, JWRE.Submitted) / 15 AS smallint),
	DATEPART(weekday, JWRE.Submitted),
	JRT.JRTID,
	JWRE.Success
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferLoginEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.LoginEvents
WHERE CONVERT(varchar(10), LEDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.LoginEvents
(
	LEDate,
	LEHour,
	LEHourQuarter,
	LEWeekDay,
	LECount
)
SELECT
	CAST(CONVERT(varchar(10), TimeLogged, 121) AS datetime) AS LEDate,
	DATEPART(hour, TimeLogged) AS LEHour,
	CAST(DATEPART(minute, TimeLogged) / 15 AS smallint) AS LEHourQuarter,
	DATEPART(weekday, TimeLogged) AS LEWeekDay,
	COUNT(*) AS LECount
FROM LoginEvent
WHERE CONVERT(varchar(10), TimeLogged, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), TimeLogged, 121) AS datetime),
	DATEPART(hour, TimeLogged),
	CAST(DATEPART(minute, TimeLogged) / 15 AS smallint),
	DATEPART(weekday, TimeLogged)
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferMapEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.MapEvents
WHERE CONVERT(varchar(10), MEDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.MapEvents
(
	MEDate,
	MEHour,
	MEHourQuarter,
	MEWeekDay,
	MEMDTID,
	MEMCTID,
	MELoggedOn,
	MEAvMsDuration,
	MECount
)
SELECT
	CAST(CONVERT(varchar(10), ME.Submitted, 121) AS datetime) AS MEDate,
	DATEPART(hour, ME.Submitted) AS MEHour,
	CAST(DATEPART(minute, ME.Submitted) / 15 AS smallint) AS MEHourQuarter,
	DATEPART(weekday, ME.Submitted) AS MEWeekDay,
	MDT.MDTID AS MEMDTID,
	MCT.MCTID AS MEMCTID,
	ME.UserLoggedOn AS MELoggedOn,
	AVG(CAST(DATEDIFF(millisecond, ME.Submitted, ME.TimeLogged) AS decimal(18, 0))) AS MEAvMsDuration,
	COUNT(*) AS MECount
FROM MapEvent ME
LEFT OUTER JOIN ReportServer.Reporting.dbo.MapDisplayType MDT ON ME.DisplayCategory = MDT.MDTCode
LEFT OUTER JOIN ReportServer.Reporting.dbo.MapCommandType MCT ON ME.CommandCategory = MCT.MCTCode
WHERE CONVERT(varchar(10), ME.Submitted, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), ME.Submitted, 121) AS datetime),
	DATEPART(hour, ME.Submitted),
	CAST(DATEPART(minute, ME.Submitted) / 15 AS smallint),
	DATEPART(weekday, ME.Submitted),
	MDT.MDTID,
	MCT.MCTID,
	ME.UserLoggedOn
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferOperationalEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.OperationalEvents
WHERE CONVERT(varchar(10), OETimeLogged, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.OperationalEvents
(
	OEID,
	OESessionID,
	OEMessage,
	OEMachineName,
	OEAssemblyName,
	OEMethodName,
	OETypeName,
	OELevel,
	OECategory,
	OETarget,
	OETimeLogged
)
SELECT
	[ID],
	SessionID,
	Message,
	MachineName,
	AssemblyName,
	MethodName,
	TypeName,
	[Level],
	Category,
	Target,
	TimeLogged
FROM OperationalEvent
WHERE CONVERT(varchar(10), TimeLogged, 121) = @Date
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferPageEntryEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.PageEntryEvents
WHERE CONVERT(varchar(10), PEEDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.PageEntryEvents
(
	PEEDate,
	PEEHour,
	PEEHourQuarter,
	PEEWeekDay,
	PEEPETID,
	PEELoggedOn,
	PEECount
)
SELECT
	CAST(CONVERT(varchar(10), PEE.TimeLogged, 121) AS datetime) AS PEEDate,
	DATEPART(hour, PEE.TimeLogged) AS PEEHour,
	CAST(DATEPART(minute, PEE.TimeLogged) / 15 AS smallint) AS PEEHourQuarter,
	DATEPART(weekday, PEE.TimeLogged) AS PEEWeekDay,
	PET.PETID AS PEEPETID,
	PEE.UserLoggedOn AS PEELoggedOn,
	COUNT(*) AS PEECount
FROM PageEntryEvent PEE
LEFT OUTER JOIN ReportServer.Reporting.dbo.PageEntryType PET ON PEE.Page = PET.PETCode
WHERE CONVERT(varchar(10), PEE.TimeLogged, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), PEE.TimeLogged, 121) AS datetime),
	DATEPART(hour, PEE.TimeLogged),
	CAST(DATEPART(minute, PEE.TimeLogged) / 15 AS smallint),
	DATEPART(weekday, PEE.TimeLogged),
	PET.PETID,
	PEE.UserLoggedOn
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferReferenceTransactionEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.ReferenceTransactionEvents
WHERE CONVERT(varchar(10), RTEDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.ReferenceTransactionEvents
(
	RTEDate,
	RTEHour,
	RTEMinute,
	RTEWeekDay,
	RTERTTID,
	RTESuccessful,
	RTEOverran,
	RTEMsDuration
)
SELECT
	CAST(CONVERT(varchar(10), RTE.Submitted, 121) AS datetime) AS RTEDate,
	DATEPART(hour, RTE.Submitted) AS RTEHour,
	DATEPART(minute, RTE.Submitted) AS RTEMinute,
	DATEPART(weekday, RTE.Submitted) AS RTEWeekDay,
	RTT.RTTID AS RTERTTID,
	RTE.Successful AS RTESuccessful,
	CASE WHEN DATEDIFF(millisecond, RTE.Submitted, RTE.TimeLogged) <= RTT.RTTMaxMsDuration THEN 0 ELSE 1 END AS RTEOverran,
	DATEDIFF(millisecond, RTE.Submitted, RTE.TimeLogged) AS RTEMsDuration
FROM ReferenceTransactionEvent RTE
LEFT OUTER JOIN ReportServer.Reporting.dbo.ReferenceTransactionType RTT ON RTE.EventType = RTT.RTTCode
WHERE CONVERT(varchar(10), RTE.Submitted, 121) = @Date
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferRetailerHandoffEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.RetailerHandoffEvents
WHERE CONVERT(varchar(10), RHEDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.RetailerHandoffEvents
(
	RHEDate,
	RHEHour,
	RHEHourQuarter,
	RHEWeekDay,
	RHERHTID,
	RHELoggedOn,
	RHECount
)
SELECT 
	CAST(CONVERT(varchar(10), RHE.TimeLogged, 121) AS datetime) AS RHEDate,
	DATEPART(hour, RHE.TimeLogged) AS RHEHour,
	CAST(DATEPART(minute, RHE.TimeLogged) / 15 AS smallint) AS RHEHourQuarter,
	DATEPART(weekday, RHE.TimeLogged) AS RHEWeekDay,
	RHT.RHTID AS RHERHTID,
	RHE.UserLoggedOn AS RHELoggedOn,
	COUNT(*) AS RHECount
FROM RetailerHandoffEvent RHE
LEFT OUTER JOIN ReportServer.Reporting.dbo.RetailerHandoffType RHT ON RHE.RetailerId = RHT.RHTCode
WHERE CONVERT(varchar(10), RHE.TimeLogged, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), RHE.TimeLogged, 121) AS datetime),
	DATEPART(hour, RHE.TimeLogged),
	CAST(DATEPART(minute, RHE.TimeLogged) / 15 AS smallint),
	DATEPART(weekday, RHE.TimeLogged),
	RHT.RHTID,
	RHE.UserLoggedOn
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferRoadPlanEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.RoadPlanEvents
WHERE CONVERT(varchar(10), RPEDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.RoadPlanEvents
(
	RPEDate,
	RPEHour,
	RPEHourQuarter,
	RPEWeekDay,
	RPECongestedRoute,
	RPELoggedOn,
	RPECount
)
SELECT 
	CAST(CONVERT(varchar(10), JPRE.TimeLogged, 121) AS datetime) AS RPEDate,
	DATEPART(hour, JPRE.TimeLogged) AS RPEHour,
	CAST(DATEPART(minute, JPRE.TimeLogged) / 15 AS smallint) AS RPEHourQuarter,
	DATEPART(weekday, JPRE.TimeLogged) AS RPEWeekDay,
	CR.CongestedRoute AS RPECongestedRoute,
	JPRE.UserLoggedOn AS RPELoggedOn,
	COUNT(*) AS RPECount
FROM JourneyPlanRequestEvent JPRE
CROSS JOIN
(
	SELECT 0 AS CongestedRoute
	UNION
	SELECT 1 AS CongestedRoute
) AS CR
WHERE CONVERT(varchar(10), JPRE.TimeLogged, 121) = @Date
AND JPRE.Car = 1
GROUP BY
	CAST(CONVERT(varchar(10), JPRE.TimeLogged, 121) AS datetime),
	DATEPART(hour, JPRE.TimeLogged),
	CAST(DATEPART(minute, JPRE.TimeLogged) / 15 AS smallint),
	DATEPART(weekday, JPRE.TimeLogged),
	CR.CongestedRoute,
	JPRE.UserLoggedOn
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferSessionEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.SessionEvents
WHERE CONVERT(varchar(10), SEDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.SessionEvents
(
	SEDate,
	SECount
)
SELECT
	CAST(@Date AS datetime) AS SEDate,
	COUNT(*) AS SECount
FROM
(
	SELECT DISTINCT
		PEE.SessionId
	FROM PageEntryEvent PEE
	WHERE CONVERT(varchar(10), PEE.TimeLogged, 121) = @Date
) SE
HAVING COUNT(*) > 0
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferUserPreferenceSaveEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.UserPreferenceSaveEvents
WHERE CONVERT(varchar(10), UPSEDate, 121) = @Date

INSERT INTO ReportServer.Reporting.dbo.UserPreferenceSaveEvents
(
	UPSEDate,
	UPSEHour,
	UPSEHourQuarter,
	UPSEWeekDay,
	UPSEUPTID,
	UPSECount
)
SELECT 
	CAST(CONVERT(varchar(10), UPSE.TimeLogged, 121) AS datetime) AS UPSEDate,
	DATEPART(hour, UPSE.TimeLogged) AS UPSEHour,
	CAST(DATEPART(minute, UPSE.TimeLogged) / 15 AS smallint) AS UPSEHourQuarter,
	DATEPART(weekday, UPSE.TimeLogged) AS UPSEWeekDay,
	UPT.UPTID AS UPSEUPTID,
	COUNT(*) AS UPSECount
FROM UserPreferenceSaveEvent UPSE
LEFT OUTER JOIN ReportServer.Reporting.dbo.UserPreferenceType UPT ON UPSE.EventCategory = UPT.UPTCode
WHERE CONVERT(varchar(10), UPSE.TimeLogged, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), UPSE.TimeLogged, 121) AS datetime),
	DATEPART(hour, UPSE.TimeLogged),
	CAST(DATEPART(minute, UPSE.TimeLogged) / 15 AS smallint),
	DATEPART(weekday, UPSE.TimeLogged),
	UPT.UPTID
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TransferWorkloadEvents
	@Date varchar(10)
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.WorkloadEvents
WHERE CONVERT(varchar(10), WEDate, 121) = @Date

-- NB Requested is used in the following command, rather than TimeLogged,
-- because Requested is the time the event actually occurred (as logged in the IIS Web Log)
-- where as TimeLogged is the time that the Web Log Reader logged the data.

INSERT INTO ReportServer.Reporting.dbo.WorkloadEvents
(
	WEDate,
	WEHour,
	WEMinute,
	WEWeekDay,
	WECount
)
SELECT
	CAST(CONVERT(varchar(10), WE.Requested, 121) AS datetime) AS WEDate,
	DATEPART(hour, WE.Requested) AS WEHour,
	DATEPART(minute, WE.Requested) AS WEMinute,
	DATEPART(weekday, WE.Requested) AS WEWeekDay,
	COUNT(*) AS WECount
FROM WorkloadEvent WE
WHERE CONVERT(varchar(10), WE.Requested, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), WE.Requested, 121) AS datetime),
	DATEPART(hour, WE.Requested),
	DATEPART(minute, WE.Requested),
	DATEPART(weekday, WE.Requested)
GO

---------------------------------------------------------------------------------------------

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO
