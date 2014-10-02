-- ***********************************************
-- NAME 	: CreateDataServicesTables.sql
-- DESCRIPTION 	: Creates DataServices tables e.g. drop down lists, dates etc.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreateDataServicesTables.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:20   mturner
--Initial revision.

USE [PermanentPortal]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReferenceNum]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ReferenceNum]
GO

-- Create ReferenceNum table used for Reference-Numbering
 CREATE TABLE ReferenceNum(
	RefID Int NOT NULL default 0
 )
 GO

-- Create TDDataServices Tables
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DropDownLists]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[DropDownLists]
GO


-- Create drop down list table and add wireframe drop down list items
 CREATE TABLE DropDownLists(
	DataSet    VARCHAR(200) NOT NULL,
	ResourceID VARCHAR(200) NOT NULL,
	ItemValue  VARCHAR(200) NOT NULL,
	IsSelected Int  NOT NULL default 0,
	SortOrder Int  NOT NULL default 0
 )
 GO

-- Create bank holiday table
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BankHolidays]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[BankHolidays]
GO

 CREATE TABLE BankHolidays(
	Holiday		Datetime NOT NULL,
	Country		Int 
 )
 GO

-- Create lists table
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Lists]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Lists]
GO

 CREATE TABLE Lists(
	DataSet   VARCHAR(200) NOT NULL,
	ListItem  VARCHAR(200) NOT NULL,
	ItemOrder Int
 )
 GO


-- Create categorised hash table
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[CategorisedHashes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[CategorisedHashes]
GO

 CREATE TABLE CategorisedHashes (
	DataSet   VARCHAR(200) NOT NULL,
	KeyName  VARCHAR(200) NOT NULL,
	Value  VARCHAR(200) NOT NULL,
	Category VARCHAR(200) NOT NULL
 )
 GO


-- Create Retailer Catalogue tables 
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_RetailerLookup_Retailer]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[RetailerLookup] DROP CONSTRAINT FK_RetailerLookup_Retailer
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[RetailerLookup]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[RetailerLookup]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[Retailers]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[Retailers]
GO

CREATE TABLE [dbo].[Retailers] (
	[RetailerId] [char] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Name] [char] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[WebsiteURL] [char] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[HandoffURL] [char] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[PhoneNumber] [char] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[DisplayURL] [char] (50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL ,
	[IconURL] [char] (300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL 
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[RetailerLookup] (
	[OperatorCode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[Mode] [char] (10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL ,
	[RetailerId] [char] (20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Retailers] ADD 
	CONSTRAINT [PK_Retailer] PRIMARY KEY  CLUSTERED 
	(
		[RetailerId]
	)  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[RetailerLookup] ADD 
	CONSTRAINT [PK_RetailerLookup] PRIMARY KEY  CLUSTERED 
	(
		[OperatorCode],
		[Mode],
		[RetailerId]
	)  ON [PRIMARY] ,
	CONSTRAINT [CK_RetailerLookup] CHECK ([Mode] = 'Underground' or ([Mode] = 'Tram' or ([Mode] = 'Rail' or ([Mode] = 'Metro' or ([Mode] = 'Ferry' or ([Mode] = 'Drt' or ([Mode] = 'Bus' or ([Mode] = 'Coach' or [Mode] = 'Air')))))))),
	CONSTRAINT [CK_RetailerLookup_1] CHECK ([Mode] = 'Rail' and [OperatorCode] = 'NONE' or [Mode] <> 'Rail' and [OperatorCode] <> 'NONE')
GO

ALTER TABLE [dbo].[RetailerLookup] ADD 
	CONSTRAINT [FK_RetailerLookup_Retailer] FOREIGN KEY 
	(
		[RetailerId]
	) REFERENCES [dbo].[Retailers] (
		[RetailerId]
	)
GO