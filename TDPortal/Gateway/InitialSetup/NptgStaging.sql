USE master
GO

/****** Object:  Database NPTG    Script Date: 09/10/2003 12:18:52 ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'NPTG_Staging')
	DROP DATABASE [NPTG_Staging]
GO

CREATE DATABASE [NPTG_Staging]  ON (NAME = N'NPTG_Staging', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL\Data\NPTG_Staging_data.mdf' , SIZE = 15, FILEGROWTH = 10%) LOG ON (NAME = N'NPTG_Staging_log', FILENAME = N'E:\Program Files\Microsoft SQL Server\MSSQL\Data\NPTG_Staging_log.LDF' , FILEGROWTH = 10%)
 COLLATE Latin1_General_CI_AS
GO

exec sp_dboption N'NPTG_Staging', N'autoclose', N'true'
GO

exec sp_dboption N'NPTG_Staging', N'bulkcopy', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'trunc. log', N'true'
GO

exec sp_dboption N'NPTG_Staging', N'torn page detection', N'true'
GO

exec sp_dboption N'NPTG_Staging', N'read only', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'dbo use', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'single', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'autoshrink', N'true'
GO

exec sp_dboption N'NPTG_Staging', N'ANSI null default', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'recursive triggers', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'ANSI nulls', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'concat null yields null', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'cursor close on commit', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'default to local cursor', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'quoted identifier', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'ANSI warnings', N'false'
GO

exec sp_dboption N'NPTG_Staging', N'auto create statistics', N'true'
GO

exec sp_dboption N'NPTG_Staging', N'auto update statistics', N'true'
GO

use [NPTG_Staging]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Localities_Districts]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Localities] DROP CONSTRAINT FK_Localities_Districts
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Admin Areas_Traveline Regions]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Admin Areas] DROP CONSTRAINT [FK_Admin Areas_Traveline Regions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AREPs_Traveline Regions]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AREPs] DROP CONSTRAINT [FK_AREPs_Traveline Regions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_AREPs_Traveline Regions1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[AREPs] DROP CONSTRAINT [FK_AREPs_Traveline Regions1]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Call Centre_Traveline Regions]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Call Centres] DROP CONSTRAINT [FK_Call Centre_Traveline Regions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Unsupported_Traveline Regions]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Unsupported] DROP CONSTRAINT [FK_Unsupported_Traveline Regions]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Admin Areas_Call Centre]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Admin Areas] DROP CONSTRAINT [FK_Admin Areas_Call Centre]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Localities_Admin Areas]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Localities] DROP CONSTRAINT [FK_Localities_Admin Areas]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Alternate Names_Localities]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Alternate Names] DROP CONSTRAINT [FK_Alternate Names_Localities]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Alternate Names_Localities1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Alternate Names] DROP CONSTRAINT [FK_Alternate Names_Localities1]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Exchange Points_Localities]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Exchange Points] DROP CONSTRAINT [FK_Exchange Points_Localities]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Hierarchy_Localities]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Hierarchy] DROP CONSTRAINT FK_Hierarchy_Localities
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_Hierarchy_Localities1]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[Hierarchy] DROP CONSTRAINT FK_Hierarchy_Localities1
GO

/****** Object:  User Defined Function dbo.GetCapability    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetCapability]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetCapability]
GO

/****** Object:  User Defined Function dbo.GetJWVersion    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetJWVersion]') and xtype in (N'FN', N'IF', N'TF'))
drop function [dbo].[GetJWVersion]
GO

/****** Object:  Stored Procedure dbo.GetExchangePoints    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetExchangePoints]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetExchangePoints]
GO

/****** Object:  Stored Procedure dbo.GetURL    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetURL]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetURL]
GO

/****** Object:  Stored Procedure dbo.GetAREPs    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetAREPs]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetAREPs]
GO

/****** Object:  Table [dbo].[Alternate Names]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Alternate Names]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Alternate Names]
GO

/****** Object:  Table [dbo].[Exchange Points]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Exchange Points]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Exchange Points]
GO

/****** Object:  Table [dbo].[Hierarchy]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Hierarchy]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Hierarchy]
GO

/****** Object:  Table [dbo].[Localities]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Localities]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Localities]
GO

/****** Object:  Table [dbo].[Admin Areas]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Admin Areas]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Admin Areas]
GO

/****** Object:  Table [dbo].[AREPs]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AREPs]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[AREPs]
GO

/****** Object:  Table [dbo].[Call Centres]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Call Centres]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Call Centres]
GO

/****** Object:  Table [dbo].[Unsupported]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Unsupported]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Unsupported]
GO

/****** Object:  Table [dbo].[Districts]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Districts]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Districts]
GO

/****** Object:  Table [dbo].[Traveline Regions]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Traveline Regions]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Traveline Regions]
GO

/****** Object:  Table [dbo].[Trusted Clients]    Script Date: 09/10/2003 12:18:53 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Trusted Clients]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Trusted Clients]
GO

/****** Object:  Table [dbo].[Districts]    Script Date: 09/10/2003 12:18:53 ******/
CREATE TABLE [dbo].[Districts] (
	[District ID] [int] NOT NULL ,
	[District Name] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Traveline Regions]    Script Date: 09/10/2003 12:18:54 ******/
CREATE TABLE [dbo].[Traveline Regions] (
	[Traveline Region ID] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Region Name] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Primary URL] [varchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Secondary URL] [varchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Tertiary URL] [varchar] (200) COLLATE Latin1_General_CI_AS NOT NULL ,
	[JW Version] [varchar] (50) COLLATE Latin1_General_CI_AS NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Trusted Clients]    Script Date: 09/10/2003 12:18:54 ******/
CREATE TABLE [dbo].[Trusted Clients] (
	[System Name] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[First IP] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Last IP] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Date of Last Change] [datetime] NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[AREPs]    Script Date: 09/10/2003 12:18:54 ******/
CREATE TABLE [dbo].[AREPs] (
	[From Region] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[To Region] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[NaPTAN] [varchar] (12) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Date of Last Change] [datetime] NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Easting] [int] NOT NULL ,
	[Northing] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Call Centres]    Script Date: 09/10/2003 12:18:54 ******/
CREATE TABLE [dbo].[Call Centres] (
	[Call Centre ID] [int] NOT NULL ,
	[Traveline Region ID] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Call Centre Name] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Call Centre Hours] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Transfer Phone no] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Power User Phone no] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Notes] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Unsupported]    Script Date: 09/10/2003 12:18:54 ******/
CREATE TABLE [dbo].[Unsupported] (
	[Traveline Region ID] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Capability] [int] NOT NULL ,
	[Date of Last Change] [datetime] NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Admin Areas]    Script Date: 09/10/2003 12:18:54 ******/
CREATE TABLE [dbo].[Admin Areas] (
	[Admin Area ID] [int] NOT NULL ,
	[Admin Area Name] [varchar] (30) COLLATE Latin1_General_CI_AS NOT NULL ,
	[ATCO Code] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Traveline Region ID] [varchar] (100) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Country] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Call Centre ID] [int] NOT NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Localities]    Script Date: 09/10/2003 12:18:55 ******/
CREATE TABLE [dbo].[Localities] (
	[National Gazetteer ID] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Locality Name] [varchar] (255) COLLATE Latin1_General_CI_AS NOT NULL ,
	[District ID] [int] NOT NULL ,
	[Admin Area ID] [int] NOT NULL ,
	[Locality Type] [varchar] (50) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Easting] [int] NOT NULL ,
	[Northing] [int] NOT NULL ,
	[Date of Last Change] [datetime] NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Language] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Alternate Names]    Script Date: 09/10/2003 12:18:55 ******/
CREATE TABLE [dbo].[Alternate Names] (
	[Primary ID] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Alternate ID] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Date of Last Change] [datetime] NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Exchange Points]    Script Date: 09/10/2003 12:18:55 ******/
CREATE TABLE [dbo].[Exchange Points] (
	[Exchange Point ID] [varchar] (8) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Associated NG ID] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Date of Last Change] [datetime] NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[NaPTAN] [varchar] (12) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Hierarchy]    Script Date: 09/10/2003 12:18:55 ******/
CREATE TABLE [dbo].[Hierarchy] (
	[Parent ID] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Child ID] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Date of Last Change] [datetime] NULL ,
	[Date of Issue] [datetime] NOT NULL ,
	[Issue Version] [varchar] (10) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Districts] WITH NOCHECK ADD 
	CONSTRAINT [PK_Districts] PRIMARY KEY  CLUSTERED 
	(
		[District ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Traveline Regions] WITH NOCHECK ADD 
	CONSTRAINT [PK_Traveline Regions] PRIMARY KEY  CLUSTERED 
	(
		[Traveline Region ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Trusted Clients] WITH NOCHECK ADD 
	CONSTRAINT [PK_Trusted Clients] PRIMARY KEY  CLUSTERED 
	(
		[System Name]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[AREPs] WITH NOCHECK ADD 
	CONSTRAINT [PK_AREPs] PRIMARY KEY  CLUSTERED 
	(
		[From Region],
		[To Region],
		[NaPTAN]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Call Centres] WITH NOCHECK ADD 
	CONSTRAINT [PK_Call Centre] PRIMARY KEY  CLUSTERED 
	(
		[Call Centre ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Unsupported] WITH NOCHECK ADD 
	CONSTRAINT [PK_Unsupported] PRIMARY KEY  CLUSTERED 
	(
		[Traveline Region ID],
		[Capability]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Admin Areas] WITH NOCHECK ADD 
	CONSTRAINT [PK_Admin Areas] PRIMARY KEY  CLUSTERED 
	(
		[Admin Area ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Localities] WITH NOCHECK ADD 
	CONSTRAINT [PK_Localities] PRIMARY KEY  CLUSTERED 
	(
		[National Gazetteer ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Alternate Names] WITH NOCHECK ADD 
	CONSTRAINT [PK_Alternate Names] PRIMARY KEY  CLUSTERED 
	(
		[Primary ID],
		[Alternate ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Exchange Points] WITH NOCHECK ADD 
	CONSTRAINT [PK_Exchange Points] PRIMARY KEY  CLUSTERED 
	(
		[Exchange Point ID],
		[Associated NG ID]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[Hierarchy] WITH NOCHECK ADD 
	CONSTRAINT [PK_Hierarchy] PRIMARY KEY  CLUSTERED 
	(
		[Parent ID],
		[Child ID]
	)  ON [PRIMARY] 
GO

 CREATE  INDEX [IX_Admin Areas] ON [dbo].[Admin Areas]([Traveline Region ID]) ON [PRIMARY]
GO

 CREATE  INDEX [IX_Localities] ON [dbo].[Localities]([Admin Area ID]) ON [PRIMARY]
GO

ALTER TABLE [dbo].[AREPs] ADD 
	CONSTRAINT [FK_AREPs_Traveline Regions] FOREIGN KEY 
	(
		[From Region]
	) REFERENCES [dbo].[Traveline Regions] (
		[Traveline Region ID]
	),
	CONSTRAINT [FK_AREPs_Traveline Regions1] FOREIGN KEY 
	(
		[To Region]
	) REFERENCES [dbo].[Traveline Regions] (
		[Traveline Region ID]
	)
GO

ALTER TABLE [dbo].[Call Centres] ADD 
	CONSTRAINT [FK_Call Centre_Traveline Regions] FOREIGN KEY 
	(
		[Traveline Region ID]
	) REFERENCES [dbo].[Traveline Regions] (
		[Traveline Region ID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[Unsupported] ADD 
	CONSTRAINT [FK_Unsupported_Traveline Regions] FOREIGN KEY 
	(
		[Traveline Region ID]
	) REFERENCES [dbo].[Traveline Regions] (
		[Traveline Region ID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[Admin Areas] ADD 
	CONSTRAINT [FK_Admin Areas_Call Centre] FOREIGN KEY 
	(
		[Call Centre ID]
	) REFERENCES [dbo].[Call Centres] (
		[Call Centre ID]
	),
	CONSTRAINT [FK_Admin Areas_Traveline Regions] FOREIGN KEY 
	(
		[Traveline Region ID]
	) REFERENCES [dbo].[Traveline Regions] (
		[Traveline Region ID]
	)
GO

ALTER TABLE [dbo].[Localities] ADD 
	CONSTRAINT [FK_Localities_Admin Areas] FOREIGN KEY 
	(
		[Admin Area ID]
	) REFERENCES [dbo].[Admin Areas] (
		[Admin Area ID]
	),
	CONSTRAINT [FK_Localities_Districts] FOREIGN KEY 
	(
		[District ID]
	) REFERENCES [dbo].[Districts] (
		[District ID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[Alternate Names] ADD 
	CONSTRAINT [FK_Alternate Names_Localities] FOREIGN KEY 
	(
		[Primary ID]
	) REFERENCES [dbo].[Localities] (
		[National Gazetteer ID]
	),
	CONSTRAINT [FK_Alternate Names_Localities1] FOREIGN KEY 
	(
		[Alternate ID]
	) REFERENCES [dbo].[Localities] (
		[National Gazetteer ID]
	)
GO

ALTER TABLE [dbo].[Exchange Points] ADD 
	CONSTRAINT [FK_Exchange Points_Localities] FOREIGN KEY 
	(
		[Associated NG ID]
	) REFERENCES [dbo].[Localities] (
		[National Gazetteer ID]
	)
GO

ALTER TABLE [dbo].[Hierarchy] ADD 
	CONSTRAINT [FK_Hierarchy_Localities] FOREIGN KEY 
	(
		[Parent ID]
	) REFERENCES [dbo].[Localities] (
		[National Gazetteer ID]
	),
	CONSTRAINT [FK_Hierarchy_Localities1] FOREIGN KEY 
	(
		[Child ID]
	) REFERENCES [dbo].[Localities] (
		[National Gazetteer ID]
	)
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.GetAREPs    Script Date: 09/10/2003 12:18:55 ******/
CREATE PROCEDURE dbo.GetAREPs

	(
		@FromRegion varchar(100),
		@ToRegion varchar(100)
	)

AS
	/* SET NOCOUNT ON */
Select NaPTAN,Easting,Northing From AREPs Where [From Region] = @FromRegion AND [To Region] = @ToRegion
	
RETURN
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.GetURL    Script Date: 09/10/2003 12:18:55 ******/

/****** Object:  Stored Procedure dbo.GetURL    Script Date: 09/09/2003 15:26:48 ******/
CREATE  PROCEDURE dbo.GetURL
--Return result set of (Primary URL, Traveline Regions ID) from Traveline Rgions table
	(
		@NationalGazetteerID varchar(10)
	)

AS
	/* SET NOCOUNT ON */
Select A.[Admin Area ID], T.[Primary URL], T.[Traveline Region ID]
From [Traveline Regions] T, [Admin Areas] A,  Localities L
Where T.[Traveline Region ID] = A.[Traveline Region ID]
AND L.[Admin Area Id] = A.[Admin Area Id]
AND L.[National Gazetteer ID] =  @NationalGazetteerID
	
RETURN 


GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  Stored Procedure dbo.GetExchangePoints    Script Date: 09/10/2003 12:18:55 ******/
CREATE PROCEDURE dbo.GetExchangePoints
-- Return cursor with all NaPTAN values
(
	@AssociatedNGID varchar(10)
)

AS
	/* SET NOCOUNT ON */
Select NaPTAN From [Exchange Points] Where [Associated NG ID] = @AssociatedNGID	
	
RETURN 

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  User Defined Function dbo.GetJWVersion    Script Date: 09/10/2003 12:18:56 ******/
CREATE FUNCTION dbo.GetJWVersion
	(
	@TravelineRegionID varchar(100)
	)
RETURNS varchar(50)
AS
BEGIN
Declare @JWVersion char(50)

Set @JWVersion =
(Select [JW Version]
FROM 
  [Traveline Regions]
WHERE
  [Traveline Region ID] = @TravelineRegionID)
  
RETURN @JWVersion
END

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS ON 
GO

/****** Object:  User Defined Function dbo.GetCapability    Script Date: 09/10/2003 12:18:56 ******/
CREATE FUNCTION dbo.GetCapability
	(
	@TravelineRegionID varchar(100),
	@Capability int
	)
RETURNS tinyint
AS
BEGIN

Declare @CapabilityExists tinyint
IF EXISTS (Select * from Unsupported Where [Traveline Region ID] = @TravelineRegionID AND Capability = @Capability)
	SET @CapabilityExists = 1
ELSE
	SET @CapabilityExists = 0
	
RETURN @CapabilityExists
END

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

