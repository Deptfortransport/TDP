-- ***********************************************
-- NAME 	: CreateReportingDatabase.sql
-- DESCRIPTION 	: Creates Reporting database, users & roles
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreateReportingDatabase.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:22   mturner
--Initial revision.

USE [master]

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'Reporting')
	DROP DATABASE [Reporting]
GO

CREATE DATABASE [Reporting]
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'Reporting', N'torn page detection', N'true'
GO

exec sp_dboption N'Reporting', N'autoshrink', N'true'
GO

exec sp_dboption N'Reporting', N'ANSI null default', N'true'
GO

exec sp_dboption N'Reporting', N'ANSI nulls', N'true'
GO

exec sp_dboption N'Reporting', N'ANSI warnings', N'true'
GO

exec sp_dboption N'Reporting', N'auto create statistics', N'true'
GO

exec sp_dboption N'Reporting', N'auto update statistics', N'true'
GO

if( ( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) ) or ( (@@microsoftversion / power(2, 24) = 7) and (@@microsoftversion & 0xffff >= 1082) ) )
	exec sp_dboption N'Reporting', N'db chaining', N'false'
GO


EXEC sp_configure 'remote proc trans', '1'
GO

RECONFIGURE WITH OVERRIDE
GO


USE [Reporting]
GO

-- CREATE Permission access for TNG Service account

DECLARE @TNGUser nvarchar(100)
SELECT @TNGUser=MISServerName + '\-service-tng' from master.dbo.Environment

if not exists (select * from dbo.sysusers where name = N'TNGUser' and uid < 16382)
	EXEC sp_grantdbaccess @TNGUser, N'-service-tng'
GO

exec sp_addrolemember N'db_owner', N'-service-tng'
GO