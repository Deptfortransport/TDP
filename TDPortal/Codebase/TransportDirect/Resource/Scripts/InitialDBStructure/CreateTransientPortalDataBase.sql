-- ***********************************************
-- NAME 	: CreateTransientPortalDatabase.sql
-- DESCRIPTION 	: Creates the Transient Portal database, 
--		: including users and roles.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreateTransientPortalDataBase.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:22   mturner
--Initial revision.

USE master
GO

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'TransientPortal')
	DROP DATABASE [TransientPortal]
GO

CREATE DATABASE [TransientPortal]
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'TransientPortal', N'torn page detection', N'true'
GO

exec sp_dboption N'TransientPortal', N'autoshrink', N'true'
GO

exec sp_dboption N'TransientPortal', N'ANSI null default', N'true'
GO

exec sp_dboption N'TransientPortal', N'ANSI nulls', N'true'
GO

exec sp_dboption N'TransientPortal', N'ANSI warnings', N'true'
GO

exec sp_dboption N'TransientPortal', N'auto create statistics', N'true'
GO

exec sp_dboption N'TransientPortal', N'auto update statistics', N'true'
GO

if( ( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) ) or ( (@@microsoftversion / power(2, 24) = 7) and (@@microsoftversion & 0xffff >= 1082) ) )
	exec sp_dboption N'TransientPortal', N'db chaining', N'false'
GO

use [TransientPortal]
GO

-- CREATE access for domain user ASPUSER

DECLARE @ASPUser nvarchar(100)
SELECT @ASPUser=DMZDomainName + '\ASPUSER' from master.dbo.Environment

if not exists (select * from dbo.sysusers where name = N'ASPUSER' and uid < 16382)
	EXEC sp_grantdbaccess @ASPUser, N'ASPUSER'
GO

exec sp_addrolemember N'db_owner', N'ASPUSER'
GO