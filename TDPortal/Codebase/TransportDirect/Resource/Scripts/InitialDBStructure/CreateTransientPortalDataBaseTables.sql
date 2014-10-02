-- ***********************************************
-- NAME 	: CreateTransientPortalDatabaseTables.sql
-- DESCRIPTION 	: Creates the Transient Portal database tables, 
--		: including sprocs and functions.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreateTransientPortalDataBaseTables.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:24   mturner
--Initial revision.

use [TransientPortal]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_TrafficAndTravelNewsRegion_TrafficAndTravelNews]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[TrafficAndTravelNewsRegion] DROP CONSTRAINT FK_TrafficAndTravelNewsRegion_TrafficAndTravelNews
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_TrafficAndTravelNews_TrafficAndTravelNewsSeverity]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[TrafficAndTravelNews] DROP CONSTRAINT FK_TrafficAndTravelNews_TrafficAndTravelNewsSeverity
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TrafficAndTravelNews]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TrafficAndTravelNews]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TrafficAndTravelNewsRegion]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TrafficAndTravelNewsRegion]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[TrafficAndTravelNewsSeverity]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[TrafficAndTravelNewsSeverity]
GO

CREATE TABLE [dbo].[TrafficAndTravelNews] (
	[UID] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[SeverityLevel] [tinyint] NOT NULL ,
	[PublicTransportOperator] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[ModeOfTransport] [varchar] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Location] [varchar] (100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[IncidentType] [varchar] (60) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[HeadlineText] [varchar] (150) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[DetailText] [varchar] (200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[IncidentStatus] [varchar] (1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Easting] [decimal](18, 6) NOT NULL ,
	[Northing] [decimal](18, 6) NOT NULL ,
	[ReportedDateTime] [datetime] NOT NULL ,
	[StartDateTime] [datetime] NOT NULL ,
	[LastModifiedDateTime] [datetime] NOT NULL ,
	[ClearedDateTime] [datetime] NULL ,
	[ExpiryDateTime] [datetime] NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TrafficAndTravelNewsRegion] (
	[UID] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[RegionName] [varchar] (25) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[TrafficAndTravelNewsSeverity] (
	[ID] [tinyint] NOT NULL ,
	[Description] [varchar] (15) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TrafficAndTravelNews] WITH NOCHECK ADD 
	CONSTRAINT [PK_TrafficAndTravelNews] PRIMARY KEY  CLUSTERED 
	(
		[UID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TrafficAndTravelNewsSeverity] WITH NOCHECK ADD 
	CONSTRAINT [PK_TrafficAndTravelNewsSeverity] PRIMARY KEY  CLUSTERED 
	(
		[ID]
	)  ON [PRIMARY] 
GO

 CREATE  UNIQUE  CLUSTERED  INDEX [IX_TrafficAndTravelNewsRegion] ON [dbo].[TrafficAndTravelNewsRegion]([UID], [RegionName]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[TrafficAndTravelNewsSeverity] ADD 
	CONSTRAINT [IX_TrafficAndTravelNewsSeverity] UNIQUE  NONCLUSTERED 
	(
		[Description]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[TrafficAndTravelNews] ADD 
	CONSTRAINT [FK_TrafficAndTravelNews_TrafficAndTravelNewsSeverity] FOREIGN KEY 
	(
		[SeverityLevel]
	) REFERENCES [dbo].[TrafficAndTravelNewsSeverity] (
		[ID]
	)
GO

ALTER TABLE [dbo].[TrafficAndTravelNewsRegion] ADD 
	CONSTRAINT [FK_TrafficAndTravelNewsRegion_TrafficAndTravelNews] FOREIGN KEY 
	(
		[UID]
	) REFERENCES [dbo].[TrafficAndTravelNews] (
		[UID]
	)
GO

---------------------------------------------------------------------------------------------

CREATE FUNCTION fn_TrafficAndTravelNewsRegion()
RETURNS @TrafficAndTravelNewsRegion TABLE
(
	UID varchar(25) COLLATE SQL_Latin1_General_CP1_CI_AS,
	Regions varchar(200) COLLATE SQL_Latin1_General_CP1_CI_AS
)
AS
BEGIN
	DECLARE @UID varchar(25)
	DECLARE @RegionName varchar(25)
	DECLARE @LastUID varchar(25)
	DECLARE @Regions varchar(200)
	
	DECLARE LocalCursor CURSOR FORWARD_ONLY READ_ONLY FOR
	SELECT
		UID,
		RegionName
	FROM TrafficAndTravelNewsRegion
	ORDER BY UID, RegionName
	
	OPEN LocalCursor
	
	SET @LastUID = ''
	FETCH NEXT FROM LocalCursor INTO @UID, @RegionName
	
	WHILE @@Fetch_Status <> -1
	BEGIN
		IF @@Fetch_Status <> -2
		BEGIN
			IF @UID = @LastUID
				SET @Regions = @Regions + ', ' + @RegionName
			ELSE
				SET @Regions = @RegionName
		END
		
		SET @LastUID = @UID
		
		FETCH NEXT FROM LocalCursor INTO @UID, @RegionName
		
		IF @@Fetch_Status <> -1 AND @@Fetch_Status <> -2
		BEGIN
			IF @UID <> @LastUID
			BEGIN
				INSERT INTO @TrafficAndTravelNewsRegion
				(
					UID,
					Regions
				)
				VALUES
				(
					@LastUID,
					@Regions
				)
				
				SET @Regions = ''
			END
		END
		ELSE IF LEN(@Regions) > 0
		BEGIN
			INSERT INTO @TrafficAndTravelNewsRegion
			(
				UID,
				Regions
			)
			VALUES
			(
				@LastUID,
				@Regions
			)
			
			SET @Regions = ''
		END
	END
	
	CLOSE LocalCursor
	DEALLOCATE LocalCursor
	
	RETURN
END
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.ImportTravelNews
	@XML text
AS
SET NOCOUNT ON

DECLARE @DocID int
DECLARE @Version varchar(20)

EXEC sp_xml_preparedocument @DocID OUTPUT, @XML

SET @Version = NULL

SELECT TOP 1
	@Version = Version
FROM
OPENXML (@DocID, '/TrafficAndTravelNewsData', 2)
WITH
(
	Version varchar(50) '@version'
)

IF @Version IS NULL OR @Version <> 'IF00902'
BEGIN
	EXEC sp_xml_removedocument @DocID
	RAISERROR('The stored procedure ImportTravelNews did not recognise the given XML file version.', 11, 1)
END
ELSE
BEGIN
	DELETE FROM TrafficAndTravelNewsRegion
	DELETE FROM TrafficAndTravelNews
	
	INSERT INTO TrafficAndTravelNews
	(
		UID,
		SeverityLevel,
		PublicTransportOperator,
		ModeOfTransport,
		Location,
		IncidentType,
		HeadlineText,
		DetailText,
		IncidentStatus,
		Easting,
		Northing,
		ReportedDateTime,
		StartDateTime,
		LastModifiedDateTime,
		ClearedDateTime,
		ExpiryDateTime
	)
	SELECT
		X.UID,
		TATNS.ID,
		X.PublicTransportOperator,
		X.ModeOfTransport,
		X.Location,
		X.IncidentType,
		X.HeadlineText,
		X.DetailText,
		X.IncidentStatus,
		X.Easting,
		X.Northing,
		X.ReportedDateTime,
		X.StartDateTime,
		X.LastModifiedDateTime,
		X.ClearedDateTime,
		X.ExpiryDateTime
	FROM
	OPENXML (@DocID, '/TrafficAndTravelNewsData/Incident', 2)
	WITH
	(
		UID varchar(25) '@uid',
		SeverityLevel varchar(15) '@severity',
		PublicTransportOperator varchar(100) '@publicTransportOperator',
		ModeOfTransport varchar(50) '@modeOfTransport',
		Location varchar(100) '@location',
		IncidentType varchar(60) '@incidentType',
		HeadlineText varchar(150) '@headlineText',
		DetailText varchar(200) '@detailText',
		IncidentStatus varchar(1) '@incidentStatus',
		Easting decimal(18, 6) '@easting',
		Northing decimal(18, 6) '@northing',
		ReportedDateTime datetime '@reportedDatetime',
		StartDateTime datetime '@startDatetime',
		LastModifiedDateTime datetime '@lastModifiedDatetime',
		ClearedDateTime datetime '@clearedDatetime',
		ExpiryDateTime datetime '@expiryDatetime'
	) X
	INNER JOIN TrafficAndTravelNewsSeverity TATNS ON X.SeverityLevel = TATNS.Description
	
	INSERT INTO TrafficAndTravelNewsRegion
	(
		UID,
		RegionName
	)
	SELECT
		UID,
		RegionName
	FROM
	OPENXML (@DocID, '/TrafficAndTravelNewsData/Incident/Region', 2)
	WITH
	(
		UID varchar(25) '@uid',
		RegionName varchar(50) '@regionName'
	)
	
	EXEC sp_xml_removedocument @DocID
END
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TravelNewsAll
	@OrderByClause varchar(200) = ''
AS
SET NOCOUNT ON

DECLARE @SQL nvarchar(4000)

SET @SQL = 
N'SELECT
	TATN.UID,
	TATNS.ID SeverityLevelID,
	TATNS.Description SeverityLevelDescription,
	TATN.PublicTransportOperator,
	TATN.ModeOfTransport,
	TATN.Location,
	TATN.IncidentType,
	TATNR.Regions,
	TATN.HeadlineText,
	TATN.DetailText,
	TATN.IncidentStatus,
	TATN.Easting,
	TATN.Northing,
	TATN.ReportedDateTime,
	TATN.StartDateTime,
	TATN.LastModifiedDateTime,
	TATN.ClearedDateTime,
	TATN.ExpiryDateTime
FROM TrafficAndTravelNews TATN
INNER JOIN TrafficAndTravelNewsSeverity TATNS ON TATN.SeverityLevel = TATNS.ID
INNER JOIN fn_TrafficAndTravelNewsRegion() TATNR ON TATN.UID = TATNR.UID
'

IF @OrderByClause <> ''
	SET @SQL = @SQL + N'ORDER BY ' + @OrderByClause

EXEC sp_executesql @SQL
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TravelNewsGrid
	@OrderByClause varchar(200) = '',
	@ModeOfTransport varchar(50) = 'All',
	@Region varchar(25) = 'All',
	@SeverityLevel varchar(15) = 'All',
	@FullDetails bit = 0
AS
SET NOCOUNT ON

DECLARE @SQL nvarchar(4000)
DECLARE @Where nvarchar(1000)

SET @SQL = 
N'SELECT
	TATN.IncidentType Incident,
	TATNR.Regions + '' ('' + TATN.Location + '')'' [Regions / Specifics],
	TATNS.Description Severity,
	TATN.StartDateTime Occurred,
	TATN.ModeOfTransport [Mode Affected],
	ISNULL(TATN.PublicTransportOperator, ''-'') Operator,
'

IF @FullDetails = 1
	SET @SQL = @SQL + N'	TATN.DetailText Details,'
ELSE
	SET @SQL = @SQL + N'	TATN.HeadlineText Details,'

SET @SQL = @SQL + N'
	TATN.LastModifiedDateTime [Last Updated]
FROM TrafficAndTravelNews TATN
INNER JOIN TrafficAndTravelNewsSeverity TATNS ON TATN.SeverityLevel = TATNS.ID
'

IF @Region <> 'All'
	SET @SQL = @SQL + N'INNER JOIN TrafficAndTravelNewsRegion TATNR2 ON TATN.UID = TATNR2.UID
INNER JOIN fn_TrafficAndTravelNewsRegion() TATNR ON TATNR2.UID = TATNR.UID
'
ELSE
	SET @SQL = @SQL + N'INNER JOIN fn_TrafficAndTravelNewsRegion() TATNR ON TATN.UID = TATNR.UID
'

SET @Where = ''

IF @ModeOfTransport <> 'All'
	IF @ModeOfTransport = 'Public Transport'
		IF @Where = ''
			SET @Where = N'WHERE TATN.ModeOfTransport <> ''Road''
'
		ELSE
			SET @Where = @Where + N'AND TATN.ModeOfTransport <> ''Road''
'
	ELSE
		IF @Where = ''
			SET @Where = N'WHERE TATN.ModeOfTransport = ''' + @ModeOfTransport + N'''
'
		ELSE
			SET @Where = @Where + N'AND TATN.ModeOfTransport = ''' + @ModeOfTransport + N'''
'

IF @Region <> 'All'
	IF @Where = ''
		SET @Where = N'WHERE TATNR2.RegionName = ''' + @Region + N'''
'
	ELSE
		SET @Where = @Where + N'AND TATNR2.RegionName = ''' + @Region + N'''
'

IF @SeverityLevel <> 'All'
	IF @Where = ''
		SET @Where = N'WHERE TATNS.Description = ''' + @SeverityLevel + N'''
'
	ELSE
		SET @Where = @Where + N'AND TATNS.Description = ''' + @SeverityLevel + N'''
'

SET @SQL = @SQL + @Where

IF @OrderByClause <> ''
	SET @SQL = @SQL + N'ORDER BY ' + @OrderByClause

EXEC sp_executesql @SQL
GO

---------------------------------------------------------------------------------------------

CREATE PROCEDURE dbo.TravelNewsHeadlines
AS
SET NOCOUNT ON

SELECT DISTINCT
	HeadlineText
FROM TrafficAndTravelNews
WHERE SeverityLevel = 
(
	SELECT
		MIN(SeverityLevel)
	FROM TrafficAndTravelNews
)
GO
