/****** Object:  Database AdditionalData    Script Date: 06/10/2003 12:05:59 ******/
IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'AdditionalData')
	DROP DATABASE [AdditionalData]
GO

CREATE DATABASE [AdditionalData]  ON (NAME = N'AdditionalData', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\Data\AdditionalData_Data.mdf' , SIZE = 1713, FILEGROWTH = 100) LOG ON (NAME = N'AdditionalData_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL\Data\AdditionalData_Log.ldf' , SIZE = 135, FILEGROWTH = 10%)
 COLLATE Latin1_General_CI_AS
GO

exec sp_dboption N'AdditionalData', N'autoclose', N'false'
GO

exec sp_dboption N'AdditionalData', N'bulkcopy', N'false'
GO

exec sp_dboption N'AdditionalData', N'trunc. log', N'true'
GO

exec sp_dboption N'AdditionalData', N'torn page detection', N'true'
GO

exec sp_dboption N'AdditionalData', N'read only', N'false'
GO

exec sp_dboption N'AdditionalData', N'dbo use', N'false'
GO

exec sp_dboption N'AdditionalData', N'single', N'false'
GO

exec sp_dboption N'AdditionalData', N'autoshrink', N'true'
GO

exec sp_dboption N'AdditionalData', N'ANSI null default', N'false'
GO

exec sp_dboption N'AdditionalData', N'recursive triggers', N'false'
GO

exec sp_dboption N'AdditionalData', N'ANSI nulls', N'false'
GO

exec sp_dboption N'AdditionalData', N'concat null yields null', N'false'
GO

exec sp_dboption N'AdditionalData', N'cursor close on commit', N'false'
GO

exec sp_dboption N'AdditionalData', N'default to local cursor', N'false'
GO

exec sp_dboption N'AdditionalData', N'quoted identifier', N'false'
GO

exec sp_dboption N'AdditionalData', N'ANSI warnings', N'false'
GO

exec sp_dboption N'AdditionalData', N'auto create statistics', N'true'
GO

exec sp_dboption N'AdditionalData', N'auto update statistics', N'true'
GO

use [AdditionalData]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ITNTOID Categories_ITNTOID Data]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ITNTOID Categories] DROP CONSTRAINT FK_ITNTOID Categories_ITNTOID Data
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ITNTOID CategoryCodes_ITNTOID Sources]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ITNTOID CategoryCodes] DROP CONSTRAINT FK_ITNTOID CategoryCodes_ITNTOID Sources
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NPTG Categories_NPTG Data]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NPTG Categories] DROP CONSTRAINT FK_NPTG Categories_NPTG Data
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NPTG CategoryCodes_NPTG Sources]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NPTG CategoryCodes] DROP CONSTRAINT FK_NPTG CategoryCodes_NPTG Sources
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NaPTAN Categories_NaPTAN Data]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NaPTAN Categories] DROP CONSTRAINT FK_NaPTAN Categories_NaPTAN Data
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NaPTAN CategoryCodes_NaPTAN Sources]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NaPTAN CategoryCodes] DROP CONSTRAINT FK_NaPTAN CategoryCodes_NaPTAN Sources
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ITNTOID Categories_ITNTOID CategoryCodes]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ITNTOID Categories] DROP CONSTRAINT FK_ITNTOID Categories_ITNTOID CategoryCodes
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ITNTOID TypeCodes_ITNTOID CategoryCodes]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ITNTOID TypeCodes] DROP CONSTRAINT FK_ITNTOID TypeCodes_ITNTOID CategoryCodes
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NPTG Categories_NPTG CategoryCodes]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NPTG Categories] DROP CONSTRAINT FK_NPTG Categories_NPTG CategoryCodes
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NPTG TypeCodes_NPTG CategoryCodes]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NPTG TypeCodes] DROP CONSTRAINT FK_NPTG TypeCodes_NPTG CategoryCodes
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NaPTAN Categories_NaPTAN CategoryCodes]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NaPTAN Categories] DROP CONSTRAINT FK_NaPTAN Categories_NaPTAN CategoryCodes
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NaPTAN TypeCodes_NaPTAN CategoryCodes]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NaPTAN TypeCodes] DROP CONSTRAINT FK_NaPTAN TypeCodes_NaPTAN CategoryCodes
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ITNTOID Types_ITNTOID Categories]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ITNTOID Types] DROP CONSTRAINT FK_ITNTOID Types_ITNTOID Categories
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ITNTOID Types_ITNTOID TypeCodes]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ITNTOID Types] DROP CONSTRAINT FK_ITNTOID Types_ITNTOID TypeCodes
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NPTG Types_NPTG Categories]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NPTG Types] DROP CONSTRAINT FK_NPTG Types_NPTG Categories
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NPTG Types_NPTG TypeCodes]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NPTG Types] DROP CONSTRAINT FK_NPTG Types_NPTG TypeCodes
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NaPTAN Types_NaPTAN Categories]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NaPTAN Types] DROP CONSTRAINT FK_NaPTAN Types_NaPTAN Categories
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NaPTAN Types_NaPTAN TypeCodes]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NaPTAN Types] DROP CONSTRAINT FK_NaPTAN Types_NaPTAN TypeCodes
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_ITNTOID Items_ITNTOID Types]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[ITNTOID Items] DROP CONSTRAINT FK_ITNTOID Items_ITNTOID Types
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NPTG Items_NPTG Types]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NPTG Items] DROP CONSTRAINT FK_NPTG Items_NPTG Types
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FK_NaPTAN Items_NaPTAN Types]') and OBJECTPROPERTY(id, N'IsForeignKey') = 1)
ALTER TABLE [dbo].[NaPTAN Items] DROP CONSTRAINT FK_NaPTAN Items_NaPTAN Types
GO

/****** Object:  Stored Procedure dbo.GetGenericITNTOID    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetGenericITNTOID]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetGenericITNTOID]
GO

/****** Object:  Stored Procedure dbo.GetGenericNPTG    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetGenericNPTG]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetGenericNPTG]
GO

/****** Object:  Stored Procedure dbo.GetGenericNaPTAN    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetGenericNaPTAN]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetGenericNaPTAN]
GO

/****** Object:  Stored Procedure dbo.ITNTOIDGetByValue    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ITNTOIDGetByValue]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ITNTOIDGetByValue]
GO

/****** Object:  Stored Procedure dbo.NPTGGetByValue    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NPTGGetByValue]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[NPTGGetByValue]
GO

/****** Object:  Stored Procedure dbo.NaPTANGetByPdsKey    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NaPTANGetByPdsKey]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[NaPTANGetByPdsKey]
GO

/****** Object:  Stored Procedure dbo.NaPTANGetByValue    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NaPTANGetByValue]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[NaPTANGetByValue]
GO

/****** Object:  Stored Procedure dbo.GenericGetUsedCategoryNamesByPDSKey    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GenericGetUsedCategoryNamesByPDSKey]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GenericGetUsedCategoryNamesByPDSKey]
GO

/****** Object:  Stored Procedure dbo.ITNTOIDGetUsedCategoryNamesByPDSKey    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ITNTOIDGetUsedCategoryNamesByPDSKey]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[ITNTOIDGetUsedCategoryNamesByPDSKey]
GO

/****** Object:  Stored Procedure dbo.NPTGGetUsedCategoryNamesByPDSKey    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NPTGGetUsedCategoryNamesByPDSKey]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[NPTGGetUsedCategoryNamesByPDSKey]
GO

/****** Object:  Stored Procedure dbo.NaPTANGetUsedCategoryNamesByPDSKey    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NaPTANGetUsedCategoryNamesByPDSKey]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[NaPTANGetUsedCategoryNamesByPDSKey]
GO

/****** Object:  Stored Procedure dbo.GetSourceCategoryTypeDetails    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetSourceCategoryTypeDetails]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetSourceCategoryTypeDetails]
GO

/****** Object:  Stored Procedure dbo.GetGeneric    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetGeneric]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
drop procedure [dbo].[GetGeneric]
GO

/****** Object:  Table [dbo].[ITNTOID Items]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ITNTOID Items]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ITNTOID Items]
GO

/****** Object:  Table [dbo].[NPTG Items]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NPTG Items]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NPTG Items]
GO

/****** Object:  Table [dbo].[NaPTAN Items]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NaPTAN Items]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NaPTAN Items]
GO

/****** Object:  Table [dbo].[ITNTOID Types]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ITNTOID Types]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ITNTOID Types]
GO

/****** Object:  Table [dbo].[NPTG Types]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NPTG Types]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NPTG Types]
GO

/****** Object:  Table [dbo].[NaPTAN Types]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NaPTAN Types]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NaPTAN Types]
GO

/****** Object:  Table [dbo].[ITNTOID Categories]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ITNTOID Categories]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ITNTOID Categories]
GO

/****** Object:  Table [dbo].[ITNTOID TypeCodes]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ITNTOID TypeCodes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ITNTOID TypeCodes]
GO

/****** Object:  Table [dbo].[NPTG Categories]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NPTG Categories]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NPTG Categories]
GO

/****** Object:  Table [dbo].[NPTG TypeCodes]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NPTG TypeCodes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NPTG TypeCodes]
GO

/****** Object:  Table [dbo].[NaPTAN Categories]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NaPTAN Categories]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NaPTAN Categories]
GO

/****** Object:  Table [dbo].[NaPTAN TypeCodes]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NaPTAN TypeCodes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NaPTAN TypeCodes]
GO

/****** Object:  Table [dbo].[ITNTOID CategoryCodes]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ITNTOID CategoryCodes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ITNTOID CategoryCodes]
GO

/****** Object:  Table [dbo].[NPTG CategoryCodes]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NPTG CategoryCodes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NPTG CategoryCodes]
GO

/****** Object:  Table [dbo].[NaPTAN CategoryCodes]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NaPTAN CategoryCodes]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NaPTAN CategoryCodes]
GO

/****** Object:  Table [dbo].[ITNTOID Data]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ITNTOID Data]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ITNTOID Data]
GO

/****** Object:  Table [dbo].[ITNTOID Sources]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ITNTOID Sources]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[ITNTOID Sources]
GO

/****** Object:  Table [dbo].[NPTG Data]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NPTG Data]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NPTG Data]
GO

/****** Object:  Table [dbo].[NPTG Sources]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NPTG Sources]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NPTG Sources]
GO

/****** Object:  Table [dbo].[NaPTAN Data]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NaPTAN Data]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NaPTAN Data]
GO

/****** Object:  Table [dbo].[NaPTAN Sources]    Script Date: 06/10/2003 12:06:07 ******/
if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[NaPTAN Sources]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[NaPTAN Sources]
GO

/****** Object:  Table [dbo].[ITNTOID Data]    Script Date: 06/10/2003 12:06:12 ******/
CREATE TABLE [dbo].[ITNTOID Data] (
	[PDS Key] [varchar] (12) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ITNTOID Sources]    Script Date: 06/10/2003 12:06:14 ******/
CREATE TABLE [dbo].[ITNTOID Sources] (
	[Source ID] [int] NOT NULL ,
	[Name] [varchar] (24) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NPTG Data]    Script Date: 06/10/2003 12:06:14 ******/
CREATE TABLE [dbo].[NPTG Data] (
	[PDS Key] [varchar] (12) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NPTG Sources]    Script Date: 06/10/2003 12:06:15 ******/
CREATE TABLE [dbo].[NPTG Sources] (
	[Source ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [varchar] (24) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NaPTAN Data]    Script Date: 06/10/2003 12:06:15 ******/
CREATE TABLE [dbo].[NaPTAN Data] (
	[PDS Key] [varchar] (12) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NaPTAN Sources]    Script Date: 06/10/2003 12:06:15 ******/
CREATE TABLE [dbo].[NaPTAN Sources] (
	[Source ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Name] [varchar] (24) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ITNTOID CategoryCodes]    Script Date: 06/10/2003 12:06:15 ******/
CREATE TABLE [dbo].[ITNTOID CategoryCodes] (
	[Category Code] [int] NOT NULL ,
	[Source ID] [int] NOT NULL ,
	[Name] [varchar] (24) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Display Order] [smallint] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NPTG CategoryCodes]    Script Date: 06/10/2003 12:06:16 ******/
CREATE TABLE [dbo].[NPTG CategoryCodes] (
	[Category Code] [int] IDENTITY (1, 1) NOT NULL ,
	[Source Id] [int] NOT NULL ,
	[Name] [varchar] (24) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Display Order] [smallint] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NaPTAN CategoryCodes]    Script Date: 06/10/2003 12:06:16 ******/
CREATE TABLE [dbo].[NaPTAN CategoryCodes] (
	[Category Code] [int] IDENTITY (1, 1) NOT NULL ,
	[Source ID] [int] NOT NULL ,
	[Name] [varchar] (48) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Display Order] [smallint] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ITNTOID Categories]    Script Date: 06/10/2003 12:06:16 ******/
CREATE TABLE [dbo].[ITNTOID Categories] (
	[Category ID] [int] NOT NULL ,
	[PDS Key] [varchar] (12) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Category Code] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ITNTOID TypeCodes]    Script Date: 06/10/2003 12:06:16 ******/
CREATE TABLE [dbo].[ITNTOID TypeCodes] (
	[Type Code] [int] NOT NULL ,
	[Category Code] [int] NOT NULL ,
	[Name] [varchar] (24) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Logical Type] [varchar] (24) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Display Order] [smallint] NOT NULL ,
	[Display Flag] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NPTG Categories]    Script Date: 06/10/2003 12:06:17 ******/
CREATE TABLE [dbo].[NPTG Categories] (
	[Category ID] [int] IDENTITY (1, 1) NOT NULL ,
	[PDS Key] [varchar] (12) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Category Code] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NPTG TypeCodes]    Script Date: 06/10/2003 12:06:17 ******/
CREATE TABLE [dbo].[NPTG TypeCodes] (
	[Type Code] [int] IDENTITY (1, 1) NOT NULL ,
	[Category Code] [int] NOT NULL ,
	[Name] [varchar] (24) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Logical Type] [varchar] (24) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Display Order] [smallint] NOT NULL ,
	[Display Flag] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NaPTAN Categories]    Script Date: 06/10/2003 12:06:18 ******/
CREATE TABLE [dbo].[NaPTAN Categories] (
	[Category ID] [int] NOT NULL ,
	[PDS Key] [varchar] (12) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Category Code] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NaPTAN TypeCodes]    Script Date: 06/10/2003 12:06:18 ******/
CREATE TABLE [dbo].[NaPTAN TypeCodes] (
	[Type Code] [int] IDENTITY (1, 1) NOT NULL ,
	[Category Code] [int] NOT NULL ,
	[Name] [varchar] (48) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Logical Type] [varchar] (24) COLLATE Latin1_General_CI_AS NOT NULL ,
	[Display Order] [smallint] NOT NULL ,
	[Display Flag] [tinyint] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ITNTOID Types]    Script Date: 06/10/2003 12:06:18 ******/
CREATE TABLE [dbo].[ITNTOID Types] (
	[Type ID] [int] NOT NULL ,
	[Category ID] [int] NOT NULL ,
	[Type Code] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NPTG Types]    Script Date: 06/10/2003 12:06:18 ******/
CREATE TABLE [dbo].[NPTG Types] (
	[Type ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Category ID] [int] NOT NULL ,
	[Type Code] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NaPTAN Types]    Script Date: 06/10/2003 12:06:19 ******/
CREATE TABLE [dbo].[NaPTAN Types] (
	[Type ID] [int] NOT NULL ,
	[Category ID] [int] NOT NULL ,
	[Type Code] [int] NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ITNTOID Items]    Script Date: 06/10/2003 12:06:19 ******/
CREATE TABLE [dbo].[ITNTOID Items] (
	[Item ID] [int] NOT NULL ,
	[Type ID] [int] NOT NULL ,
	[Value] [varchar] (256) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NPTG Items]    Script Date: 06/10/2003 12:06:19 ******/
CREATE TABLE [dbo].[NPTG Items] (
	[Item ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Type ID] [int] NOT NULL ,
	[Value] [varchar] (256) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[NaPTAN Items]    Script Date: 06/10/2003 12:06:20 ******/
CREATE TABLE [dbo].[NaPTAN Items] (
	[Item ID] [int] IDENTITY (1, 1) NOT NULL ,
	[Type ID] [int] NOT NULL ,
	[Value] [varchar] (256) COLLATE Latin1_General_CI_AS NOT NULL 
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ITNTOID Data] WITH NOCHECK ADD 
	CONSTRAINT [PK_ITNTOID Data] PRIMARY KEY  CLUSTERED 
	(
		[PDS Key]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ITNTOID Sources] WITH NOCHECK ADD 
	CONSTRAINT [PK_ITNTOID Sources] PRIMARY KEY  CLUSTERED 
	(
		[Source ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NPTG Data] WITH NOCHECK ADD 
	CONSTRAINT [PK_NPTG Data] PRIMARY KEY  CLUSTERED 
	(
		[PDS Key]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NPTG Sources] WITH NOCHECK ADD 
	CONSTRAINT [PK_NPTG Sources] PRIMARY KEY  CLUSTERED 
	(
		[Source ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NaPTAN Data] WITH NOCHECK ADD 
	CONSTRAINT [PK_NaPTAN Data] PRIMARY KEY  CLUSTERED 
	(
		[PDS Key]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NaPTAN Sources] WITH NOCHECK ADD 
	CONSTRAINT [PK_Sources] PRIMARY KEY  CLUSTERED 
	(
		[Source ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ITNTOID CategoryCodes] WITH NOCHECK ADD 
	CONSTRAINT [PK_ITNTOID CategoryCodes] PRIMARY KEY  CLUSTERED 
	(
		[Category Code]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NPTG CategoryCodes] WITH NOCHECK ADD 
	CONSTRAINT [PK_NPTG CategoryCodes] PRIMARY KEY  CLUSTERED 
	(
		[Category Code]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NaPTAN CategoryCodes] WITH NOCHECK ADD 
	CONSTRAINT [PK_NaPTAN CategoryCodes] PRIMARY KEY  CLUSTERED 
	(
		[Category Code]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ITNTOID Categories] WITH NOCHECK ADD 
	CONSTRAINT [PK_ITNTOID Categories] PRIMARY KEY  CLUSTERED 
	(
		[Category ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ITNTOID TypeCodes] WITH NOCHECK ADD 
	CONSTRAINT [PK_ITNTOID TypeCodes] PRIMARY KEY  CLUSTERED 
	(
		[Type Code]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NPTG Categories] WITH NOCHECK ADD 
	CONSTRAINT [PK_NPTG Categories] PRIMARY KEY  CLUSTERED 
	(
		[Category ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NPTG TypeCodes] WITH NOCHECK ADD 
	CONSTRAINT [PK_NPTG TypeCodes] PRIMARY KEY  CLUSTERED 
	(
		[Type Code]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NaPTAN Categories] WITH NOCHECK ADD 
	CONSTRAINT [PK_NaPTAN Categories] PRIMARY KEY  CLUSTERED 
	(
		[Category ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NaPTAN TypeCodes] WITH NOCHECK ADD 
	CONSTRAINT [PK_NaPTAN TypeCodes] PRIMARY KEY  CLUSTERED 
	(
		[Type Code]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[ITNTOID Types] WITH NOCHECK ADD 
	CONSTRAINT [PK_ITNTOID Types] PRIMARY KEY  CLUSTERED 
	(
		[Type ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NPTG Types] WITH NOCHECK ADD 
	CONSTRAINT [PK_NPTG Types] PRIMARY KEY  CLUSTERED 
	(
		[Type ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NaPTAN Types] WITH NOCHECK ADD 
	CONSTRAINT [PK_NaPTAN Types] PRIMARY KEY  CLUSTERED 
	(
		[Type ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

 CREATE  CLUSTERED  INDEX [IX_ITNTOID Items_1] ON [dbo].[ITNTOID Items]([Type ID]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  CLUSTERED  INDEX [IX_NPTG Items_1] ON [dbo].[NPTG Items]([Type ID]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  CLUSTERED  INDEX [IX_NaPTAN Items_1] ON [dbo].[NaPTAN Items]([Type ID]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

ALTER TABLE [dbo].[ITNTOID Items] WITH NOCHECK ADD 
	CONSTRAINT [PK_ITNTOID Items] PRIMARY KEY  NONCLUSTERED 
	(
		[Item ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NPTG Items] WITH NOCHECK ADD 
	CONSTRAINT [PK_NPTG Items] PRIMARY KEY  NONCLUSTERED 
	(
		[Item ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

ALTER TABLE [dbo].[NaPTAN Items] WITH NOCHECK ADD 
	CONSTRAINT [PK_NaPTAN Items] PRIMARY KEY  NONCLUSTERED 
	(
		[Item ID]
	) WITH  FILLFACTOR = 10  ON [PRIMARY] 
GO

 CREATE  INDEX [IX_ITNTOID Sources] ON [dbo].[ITNTOID Sources]([Name]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NPTG Sources] ON [dbo].[NPTG Sources]([Name]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NaPTAN Sources] ON [dbo].[NaPTAN Sources]([Name]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_ITNTOID CategoryCodes] ON [dbo].[ITNTOID CategoryCodes]([Source ID]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_ITNTOID CategoryCodes_1] ON [dbo].[ITNTOID CategoryCodes]([Name]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NPTG CategoryCodes] ON [dbo].[NPTG CategoryCodes]([Source Id]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NPTG CategoryCodes_1] ON [dbo].[NPTG CategoryCodes]([Name]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NaPTAN CategoryCodes] ON [dbo].[NaPTAN CategoryCodes]([Source ID]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NaPTAN CategoryCodes_1] ON [dbo].[NaPTAN CategoryCodes]([Name]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_ITNTOID Categories] ON [dbo].[ITNTOID Categories]([PDS Key]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_ITNTOID Categories_1] ON [dbo].[ITNTOID Categories]([Category Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [ITNTOID Categories14] ON [dbo].[ITNTOID Categories]([PDS Key], [Category Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_ITNTOID TypeCodes] ON [dbo].[ITNTOID TypeCodes]([Category Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_ITNTOID TypeCodes_1] ON [dbo].[ITNTOID TypeCodes]([Name]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NPTG Categories] ON [dbo].[NPTG Categories]([PDS Key]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NPTG Categories_1] ON [dbo].[NPTG Categories]([Category Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [NPTG Categories14] ON [dbo].[NPTG Categories]([PDS Key], [Category Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NPTG TypeCodes] ON [dbo].[NPTG TypeCodes]([Category Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NPTG TypeCodes_1] ON [dbo].[NPTG TypeCodes]([Name]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NaPTAN Categories] ON [dbo].[NaPTAN Categories]([PDS Key]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NaPTAN Categories_1] ON [dbo].[NaPTAN Categories]([Category Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [NaPTAN Categories14] ON [dbo].[NaPTAN Categories]([PDS Key], [Category Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NaPTAN TypeCodes] ON [dbo].[NaPTAN TypeCodes]([Category Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NaPTAN TypeCodes_1] ON [dbo].[NaPTAN TypeCodes]([Name]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_ITNTOID Types] ON [dbo].[ITNTOID Types]([Category ID]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_ITNTOID Types_1] ON [dbo].[ITNTOID Types]([Type Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [ITNTOID Types10] ON [dbo].[ITNTOID Types]([Category ID], [Type Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NPTG Types] ON [dbo].[NPTG Types]([Category ID]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NPTG Types_1] ON [dbo].[NPTG Types]([Type Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [NPTG Types10] ON [dbo].[NPTG Types]([Category ID], [Type Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NaPTAN Types] ON [dbo].[NaPTAN Types]([Category ID]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NaPTAN Types_1] ON [dbo].[NaPTAN Types]([Type Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [NaPTAN Types10] ON [dbo].[NaPTAN Types]([Category ID], [Type Code]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_ITNTOID Items] ON [dbo].[ITNTOID Items]([Value]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NPTG Items] ON [dbo].[NPTG Items]([Value]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

 CREATE  INDEX [IX_NaPTAN Items] ON [dbo].[NaPTAN Items]([Value]) WITH  FILLFACTOR = 10 ON [PRIMARY]
GO

ALTER TABLE [dbo].[ITNTOID CategoryCodes] ADD 
	CONSTRAINT [FK_ITNTOID CategoryCodes_ITNTOID Sources] FOREIGN KEY 
	(
		[Source ID]
	) REFERENCES [dbo].[ITNTOID Sources] (
		[Source ID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[NPTG CategoryCodes] ADD 
	CONSTRAINT [FK_NPTG CategoryCodes_NPTG Sources] FOREIGN KEY 
	(
		[Source Id]
	) REFERENCES [dbo].[NPTG Sources] (
		[Source ID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[NaPTAN CategoryCodes] ADD 
	CONSTRAINT [FK_NaPTAN CategoryCodes_NaPTAN Sources] FOREIGN KEY 
	(
		[Source ID]
	) REFERENCES [dbo].[NaPTAN Sources] (
		[Source ID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[ITNTOID Categories] ADD 
	CONSTRAINT [FK_ITNTOID Categories_ITNTOID CategoryCodes] FOREIGN KEY 
	(
		[Category Code]
	) REFERENCES [dbo].[ITNTOID CategoryCodes] (
		[Category Code]
	) ON DELETE CASCADE ,
	CONSTRAINT [FK_ITNTOID Categories_ITNTOID Data] FOREIGN KEY 
	(
		[PDS Key]
	) REFERENCES [dbo].[ITNTOID Data] (
		[PDS Key]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[ITNTOID TypeCodes] ADD 
	CONSTRAINT [FK_ITNTOID TypeCodes_ITNTOID CategoryCodes] FOREIGN KEY 
	(
		[Category Code]
	) REFERENCES [dbo].[ITNTOID CategoryCodes] (
		[Category Code]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[NPTG Categories] ADD 
	CONSTRAINT [FK_NPTG Categories_NPTG CategoryCodes] FOREIGN KEY 
	(
		[Category Code]
	) REFERENCES [dbo].[NPTG CategoryCodes] (
		[Category Code]
	) ON DELETE CASCADE ,
	CONSTRAINT [FK_NPTG Categories_NPTG Data] FOREIGN KEY 
	(
		[PDS Key]
	) REFERENCES [dbo].[NPTG Data] (
		[PDS Key]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[NPTG TypeCodes] ADD 
	CONSTRAINT [FK_NPTG TypeCodes_NPTG CategoryCodes] FOREIGN KEY 
	(
		[Category Code]
	) REFERENCES [dbo].[NPTG CategoryCodes] (
		[Category Code]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[NaPTAN Categories] ADD 
	CONSTRAINT [FK_NaPTAN Categories_NaPTAN CategoryCodes] FOREIGN KEY 
	(
		[Category Code]
	) REFERENCES [dbo].[NaPTAN CategoryCodes] (
		[Category Code]
	) ON DELETE CASCADE ,
	CONSTRAINT [FK_NaPTAN Categories_NaPTAN Data] FOREIGN KEY 
	(
		[PDS Key]
	) REFERENCES [dbo].[NaPTAN Data] (
		[PDS Key]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[NaPTAN TypeCodes] ADD 
	CONSTRAINT [FK_NaPTAN TypeCodes_NaPTAN CategoryCodes] FOREIGN KEY 
	(
		[Category Code]
	) REFERENCES [dbo].[NaPTAN CategoryCodes] (
		[Category Code]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[ITNTOID Types] ADD 
	CONSTRAINT [FK_ITNTOID Types_ITNTOID Categories] FOREIGN KEY 
	(
		[Category ID]
	) REFERENCES [dbo].[ITNTOID Categories] (
		[Category ID]
	) ON DELETE CASCADE ,
	CONSTRAINT [FK_ITNTOID Types_ITNTOID TypeCodes] FOREIGN KEY 
	(
		[Type Code]
	) REFERENCES [dbo].[ITNTOID TypeCodes] (
		[Type Code]
	)
GO

ALTER TABLE [dbo].[NPTG Types] ADD 
	CONSTRAINT [FK_NPTG Types_NPTG Categories] FOREIGN KEY 
	(
		[Category ID]
	) REFERENCES [dbo].[NPTG Categories] (
		[Category ID]
	) ON DELETE CASCADE ,
	CONSTRAINT [FK_NPTG Types_NPTG TypeCodes] FOREIGN KEY 
	(
		[Type Code]
	) REFERENCES [dbo].[NPTG TypeCodes] (
		[Type Code]
	)
GO

ALTER TABLE [dbo].[NaPTAN Types] ADD 
	CONSTRAINT [FK_NaPTAN Types_NaPTAN Categories] FOREIGN KEY 
	(
		[Category ID]
	) REFERENCES [dbo].[NaPTAN Categories] (
		[Category ID]
	) ON DELETE CASCADE ,
	CONSTRAINT [FK_NaPTAN Types_NaPTAN TypeCodes] FOREIGN KEY 
	(
		[Type Code]
	) REFERENCES [dbo].[NaPTAN TypeCodes] (
		[Type Code]
	)
GO

ALTER TABLE [dbo].[ITNTOID Items] ADD 
	CONSTRAINT [FK_ITNTOID Items_ITNTOID Types] FOREIGN KEY 
	(
		[Type ID]
	) REFERENCES [dbo].[ITNTOID Types] (
		[Type ID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[NPTG Items] ADD 
	CONSTRAINT [FK_NPTG Items_NPTG Types] FOREIGN KEY 
	(
		[Type ID]
	) REFERENCES [dbo].[NPTG Types] (
		[Type ID]
	) ON DELETE CASCADE 
GO

ALTER TABLE [dbo].[NaPTAN Items] ADD 
	CONSTRAINT [FK_NaPTAN Items_NaPTAN Types] FOREIGN KEY 
	(
		[Type ID]
	) REFERENCES [dbo].[NaPTAN Types] (
		[Type ID]
	) ON DELETE CASCADE 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.GetGeneric    Script Date: 06/10/2003 12:06:20 ******/

/****** Object:  Stored Procedure dbo.GetGeneric    Script Date: 18/09/2003 19:15:20 ******/

/****** Object:  Stored Procedure dbo.GetGeneric    Script Date: 18/09/2003 18:58:03 ******/

/****** Object:  Stored Procedure dbo.GetGeneric    Script Date: 29/08/2003 11:13:37 ******/

/****** Object:  Stored Procedure dbo.GetGeneric    Script Date: 29/08/2003 09:03:56 ******/

/****** Object:  Stored Procedure dbo.GetGeneric    Script Date: 28/08/2003 19:30:29 ******/
CREATE      PROCEDURE [dbo].[GetGeneric]
(
@pds integer,
@PDSKey varchar(12),
@SourceName varchar(12),
@CategoryName varchar(24),
@TypeName varchar(24),
@Value varchar(256),
@DisplayFlag tinyint
)
AS
BEGIN
--SET NOCOUNT ON
-- Call a less generic sp according to pds
  IF 1=@pds
    	BEGIN
	      --An execution plan is built for normal usage, where PDSKey has a value and @Value=%  
	      -- this does not work too well so use the same sp code but let a differnt execution plan be built
	      if @Value='%'
		Exec GetGenericNaPTAN @PDSKey, @SourceName, @CategoryName, @TypeName, @Value, @DisplayFlag
	      else
		Exec NaPTANGetByValue @PDSKey, @SourceName, @CategoryName, @TypeName, @Value, @DisplayFlag
	END
  ELSE IF 2 = @pds
    	BEGIN
	      --An execution plan is built for normal usage, where PDSKey has a value and @Value=%  
	      -- this does not work too well so use the same sp code but let a differnt execution plan be built
	      if @Value='%'
		Exec GetGenericNPTG @PDSKey, @SourceName, @CategoryName, @TypeName, @Value, @DisplayFlag
	      else
		Exec NPTGGetByValue @PDSKey, @SourceName, @CategoryName, @TypeName, @Value, @DisplayFlag
	END
  ELSE IF 3 = @pds
    	BEGIN
	      --An execution plan is built for normal usage, where PDSKey has a value and @Value=%  
	      -- this does not work too well so use the same sp code but let a differnt execution plan be built
	      if @Value='%'
		Exec GetGenericITNTOID @PDSKey, @SourceName, @CategoryName, @TypeName, @Value, @DisplayFlag
	      else
		Exec ITNTOIDGetByValue @PDSKey, @SourceName, @CategoryName, @TypeName, @Value, @DisplayFlag
	END
  ELSE
      Return 1
--SET NOCOUNT OFF
END


GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.GetSourceCategoryTypeDetails    Script Date: 06/10/2003 12:06:20 ******/
CREATE PROCEDURE dbo.GetSourceCategoryTypeDetails
(
@pds integer
)
AS 
BEGIN
IF 1=@pds
Select S.Name as [Source Name],
CC.Name as [Category Name],CC.[Display Order] as [Category Display Order],
TC.Name as [Type Name],TC.[Logical Type],
TC.[Display Order] as [Type Display Order],
TC.[Display Flag] as [Type Display Flag]
FROM [NaPTAN Sources] S,[NaPTAN CategoryCodes] CC, [NaPTAN TypeCodes] TC
Where S.[Source ID] = CC.[Source ID] AND CC.[Category Code] = TC.[Category Code]
ORDER by S.Name,CC.[Display Order],TC.[Display Order]
ELSE IF 2=@pds
Select S.Name as [Source Name],
CC.Name as [Category Name],CC.[Display Order] as [Category Display Order],
TC.Name as [Type Name],TC.[Logical Type],
TC.[Display Order] as [Type Display Order],
TC.[Display Flag] as [Type Display Flag]
FROM [NPTG Sources] S,[NPTG CategoryCodes] CC, [NPTG TypeCodes] TC
Where S.[Source ID] = CC.[Source ID] AND CC.[Category Code] = TC.[Category Code]
ORDER by S.Name,CC.[Display Order],TC.[Display Order]
ELSE IF 3=@pds
Select S.Name as [Source Name],
CC.Name as [Category Name],CC.[Display Order] as [Category Display Order],
TC.Name as [Type Name],TC.[Logical Type],
TC.[Display Order] as [Type Display Order],
TC.[Display Flag] as [Type Display Flag]
FROM [ITNTOID Sources] S,[ITNTOID CategoryCodes] CC, [ITNTOID TypeCodes] TC
Where S.[Source ID] = CC.[Source ID] AND CC.[Category Code] = TC.[Category Code]
ORDER by S.Name,CC.[Display Order],TC.[Display Order]
ELSE
Return 1
END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.ITNTOIDGetUsedCategoryNamesByPDSKey    Script Date: 06/10/2003 12:06:20 ******/
CREATE PROCEDURE [dbo].[ITNTOIDGetUsedCategoryNamesByPDSKey] 
(
@PDSKey varchar(12)
)
AS
BEGIN
Select Distinct CC.Name from
[ITNTOID Categories] C,
[ITNTOID CategoryCodes] CC
Where
CC.[Category Code] = C.[Category Code]
AND C.[PDS Key] = @PDSKey
END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.NPTGGetUsedCategoryNamesByPDSKey    Script Date: 06/10/2003 12:06:20 ******/
CREATE PROCEDURE [dbo].[NPTGGetUsedCategoryNamesByPDSKey] 
(
@PDSKey varchar(12)
)
AS
BEGIN
Select Distinct CC.Name from
[NPTG Categories] C,
[NPTG CategoryCodes] CC
Where
CC.[Category Code] = C.[Category Code]
AND C.[PDS Key] = @PDSKey
END

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.NaPTANGetUsedCategoryNamesByPDSKey    Script Date: 06/10/2003 12:06:20 ******/

/****** Object:  Stored Procedure dbo.NaPTANGetUsedCategoryNamesByPDSKey    Script Date: 03/10/2003 19:32:15 ******/
CREATE  PROCEDURE [dbo].[NaPTANGetUsedCategoryNamesByPDSKey]
(
@PDSKey varchar(12)
)
AS
BEGIN
Select Distinct CC.Name from
[NaPTAN Categories] C,
[NaPTAN CategoryCodes] CC
Where
CC.[Category Code] = C.[Category Code]
AND C.[PDS Key] = @PDSKey
END

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.GenericGetUsedCategoryNamesByPDSKey    Script Date: 06/10/2003 12:06:20 ******/
CREATE PROCEDURE [dbo].[GenericGetUsedCategoryNamesByPDSKey]
(
@pds integer,
@PDSKey varchar(12)
)
AS
BEGIN
-- Call a less generic sp according to pds
  IF 1=@pds
    	BEGIN
    		Exec NaPTANGetUsedCategoryNamesByPDSKey @PDSKey
	END
  ELSE IF 2 = @pds
    	BEGIN
    		Exec NPTGGetUsedCategoryNamesByPDSKey @PDSKey
	END
  ELSE IF 3 = @pds
    	BEGIN
    		Exec ITNTOIDGetUsedCategoryNamesByPDSKey @PDSKey
	END
  ELSE
      Return 1
END

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.GetGenericITNTOID    Script Date: 06/10/2003 12:06:20 ******/
CREATE PROCEDURE dbo.[GetGenericITNTOID]
(
@PDSKey varchar(12),
@SourceName varchar(12),
@CategoryName varchar(24),
@TypeName varchar(24),
@Value varchar(256),
@DisplayFlag tinyint
)
AS
BEGIN
Select C.[PDS Key],CC.Name as Category,C.[Category Id],TC.Name as Type,TC.[Logical Type],I.Value
from [ITNTOID Sources] S,[ITNTOID Categories] C, [ITNTOID CategoryCodes] CC, [ITNTOID Types] T, 
[ITNTOID TypeCodes] TC, [ITNTOID Items] I
Where 
S.[Source ID] = CC.[Source ID]
AND CC.[Category Code] = C.[Category Code]
AND CC.[Category Code] = TC.[Category Code]
AND C.[Category ID] = T.[Category ID]
AND T.[Type Code] = TC.[Type Code]
AND I.[Type ID] = T.[Type ID]
AND S.Name like @SourceName
AND CC.Name like @CategoryName
AND TC.Name like @TypeName
AND C.[PDS Key] like  @PDSKey
AND I.Value like @Value
AND TC.[Display Flag] = @DisplayFlag
ORDER BY C.[PDS Key],CC.[Display Order],C.[Category ID],TC.[Type Code],TC.[Display Order],T.[Type ID]

END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.GetGenericNPTG    Script Date: 06/10/2003 12:06:20 ******/
CREATE PROCEDURE dbo.[GetGenericNPTG]
(
@PDSKey varchar(12),
@SourceName varchar(12),
@CategoryName varchar(24),
@TypeName varchar(24),
@Value varchar(256),
@DisplayFlag tinyint
)
AS
BEGIN
Select C.[PDS Key],CC.Name as Category,C.[Category Id],TC.Name as Type,TC.[Logical Type],I.Value
from [NPTG Sources] S,[NPTG Categories] C, [NPTG CategoryCodes] CC, [NPTG Types] T, 
[NPTG TypeCodes] TC, [NPTG Items] I
Where 
S.[Source ID] = CC.[Source ID]
AND CC.[Category Code] = C.[Category Code]
AND CC.[Category Code] = TC.[Category Code]
AND C.[Category ID] = T.[Category ID]
AND T.[Type Code] = TC.[Type Code]
AND I.[Type ID] = T.[Type ID]
AND S.Name like @SourceName
AND CC.Name like @CategoryName
AND TC.Name like @TypeName
AND C.[PDS Key] like  @PDSKey
AND I.Value like @Value
AND TC.[Display Flag] = @DisplayFlag
ORDER BY C.[PDS Key],CC.[Display Order],C.[Category ID],TC.[Type Code],TC.[Display Order],T.[Type ID]

END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.GetGenericNaPTAN    Script Date: 06/10/2003 12:06:20 ******/

/****** Object:  Stored Procedure dbo.GetGenericNaPTAN    Script Date: 04/09/2003 15:51:41 ******/

/****** Object:  Stored Procedure dbo.GetGenericNaPTAN    Script Date: 29/08/2003 11:14:05 ******/
CREATE   PROCEDURE dbo.[GetGenericNaPTAN]
(
@PDSKey varchar(12),
@SourceName varchar(12),
@CategoryName varchar(24),
@TypeName varchar(24),
@Value varchar(256),
@DisplayFlag tinyint
)
AS
BEGIN
Select C.[PDS Key],CC.Name as Category,C.[Category Id],TC.Name as Type,TC.[Logical Type],I.Value
from 
[NaPTAN Sources] S WITH (NOLOCK),
[NaPTAN Categories] C WITH (NOLOCK), 
[NaPTAN CategoryCodes] CC WITH (NOLOCK), 
[NaPTAN Types] T WITH (NOLOCK), 
[NaPTAN TypeCodes] TC WITH (NOLOCK),
[NaPTAN Items] I WITH (NOLOCK)
Where 
S.[Source ID] = CC.[Source ID]
AND CC.[Category Code] = C.[Category Code]
AND CC.[Category Code] = TC.[Category Code]
AND C.[Category ID] = T.[Category ID]
AND T.[Type Code] = TC.[Type Code]
AND I.[Type ID] = T.[Type ID]
AND S.Name like @SourceName
AND CC.Name like @CategoryName
AND TC.Name like @TypeName
AND C.[PDS Key] like  @PDSKey
AND I.Value like @Value
AND TC.[Display Flag] = @DisplayFlag
ORDER BY C.[PDS Key],CC.[Display Order],C.[Category ID],TC.[Type Code],TC.[Display Order],T.[Type ID]


END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.ITNTOIDGetByValue    Script Date: 06/10/2003 12:06:20 ******/
CREATE  PROCEDURE dbo.ITNTOIDGetByValue
(
@PDSKey varchar(12),
@SourceName varchar(12),
@CategoryName varchar(24),
@TypeName varchar(24),
@Value varchar(256),
@DisplayFlag tinyint
)
AS
BEGIN
Select C.[PDS Key],CC.Name as Category,C.[Category Id],TC.Name as Type,TC.[Logical Type],I.Value
from [ITNTOID Sources] S,[ITNTOID Categories] C, [ITNTOID CategoryCodes] CC, [ITNTOID Types] T, 
[ITNTOID TypeCodes] TC, [ITNTOID Items] I
Where 
S.[Source ID] = CC.[Source ID]
AND CC.[Category Code] = C.[Category Code]
AND CC.[Category Code] = TC.[Category Code]
AND C.[Category ID] = T.[Category ID]
AND T.[Type Code] = TC.[Type Code]
AND I.[Type ID] = T.[Type ID]
AND S.Name like @SourceName
AND CC.Name like @CategoryName
AND TC.Name like @TypeName
AND C.[PDS Key] like  @PDSKey
AND I.Value like @Value
AND TC.[Display Flag] = @DisplayFlag
ORDER BY C.[PDS Key],CC.[Display Order],C.[Category ID],TC.[Type Code],TC.[Display Order],T.[Type ID]

END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.NPTGGetByValue    Script Date: 06/10/2003 12:06:20 ******/
CREATE  PROCEDURE dbo.NPTGGetByValue
(
@PDSKey varchar(12),
@SourceName varchar(12),
@CategoryName varchar(24),
@TypeName varchar(24),
@Value varchar(256),
@DisplayFlag tinyint
)
AS
BEGIN
Select C.[PDS Key],CC.Name as Category,C.[Category Id],TC.Name as Type,TC.[Logical Type],I.Value
from [NPTG Sources] S,[NPTG Categories] C, [NPTG CategoryCodes] CC, [NPTG Types] T, 
[NPTG TypeCodes] TC, [NPTG Items] I
Where 
S.[Source ID] = CC.[Source ID]
AND CC.[Category Code] = C.[Category Code]
AND CC.[Category Code] = TC.[Category Code]
AND C.[Category ID] = T.[Category ID]
AND T.[Type Code] = TC.[Type Code]
AND I.[Type ID] = T.[Type ID]
AND S.Name like @SourceName
AND CC.Name like @CategoryName
AND TC.Name like @TypeName
AND C.[PDS Key] like  @PDSKey
AND I.Value like @Value
AND TC.[Display Flag] = @DisplayFlag
ORDER BY C.[PDS Key],CC.[Display Order],C.[Category ID],TC.[Type Code],TC.[Display Order],T.[Type ID]

END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.NaPTANGetByPdsKey    Script Date: 06/10/2003 12:06:20 ******/
CREATE PROCEDURE dbo.NaPTANGetByPdsKey 
(
@PDSKey varchar(12)
)
AS
BEGIN
Select C.[PDS Key],CC.Name as Category,TC.Name as Type,TC.[Logical Type],I.Value
from [NaPTAN Sources] S,[NaPTAN Categories] C, [NaPTAN CategoryCodes] CC, [NaPTAN Types] T, 
[NaPTAN TypeCodes] TC, [NaPTAN Items] I
Where 
S.[Source ID] = CC.[Source ID]
AND CC.[Category Code] = C.[Category Code]
AND CC.[Category Code] = TC.[Category Code]
AND C.[Category ID] = T.[Category ID]
AND T.[Type Code] = TC.[Type Code]
AND I.[Type ID] = T.[Type ID]
AND C.[PDS Key] like  @PDSKey
ORDER BY C.[PDS Key],S.[Source ID],C.[Category Code],CC.[Display Order],TC.[Type Code],TC.[Display Order],T.[Type ID]

END

GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS OFF 
GO

/****** Object:  Stored Procedure dbo.NaPTANGetByValue    Script Date: 06/10/2003 12:06:20 ******/
CREATE  PROCEDURE dbo.NaPTANGetByValue
(
@PDSKey varchar(12),
@SourceName varchar(12),
@CategoryName varchar(24),
@TypeName varchar(24),
@Value varchar(256),
@DisplayFlag tinyint
)
AS
BEGIN
Select C.[PDS Key],CC.Name as Category,C.[Category Id],TC.Name as Type,TC.[Logical Type],I.Value
from [NaPTAN Sources] S,[NaPTAN Categories] C, [NaPTAN CategoryCodes] CC, [NaPTAN Types] T, 
[NaPTAN TypeCodes] TC, [NaPTAN Items] I
Where 
S.[Source ID] = CC.[Source ID]
AND CC.[Category Code] = C.[Category Code]
AND CC.[Category Code] = TC.[Category Code]
AND C.[Category ID] = T.[Category ID]
AND T.[Type Code] = TC.[Type Code]
AND I.[Type ID] = T.[Type ID]
AND S.Name like @SourceName
AND CC.Name like @CategoryName
AND TC.Name like @TypeName
AND C.[PDS Key] like  @PDSKey
AND I.Value like @Value
AND TC.[Display Flag] = @DisplayFlag
ORDER BY C.[PDS Key],CC.[Display Order],C.[Category ID],TC.[Type Code],TC.[Display Order],T.[Type ID]

END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO

