-- ***********************************************
-- NAME 	: CreatePermanentPortalDatabase.sql
-- DESCRIPTION 	: Creates the PermanementPortal database 
--		: including users and roles.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreatePermanentPortalDatabase.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:20   mturner
--Initial revision.

USE [master]

IF EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = N'PermanentPortal')
	DROP DATABASE [PermanentPortal]
GO

CREATE DATABASE [PermanentPortal]
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO

exec sp_dboption N'PermanentPortal', N'torn page detection', N'true'
GO

exec sp_dboption N'PermanentPortal', N'autoshrink', N'true'
GO

exec sp_dboption N'PermanentPortal', N'ANSI null default', N'true'
GO

exec sp_dboption N'PermanentPortal', N'ANSI nulls', N'true'
GO

exec sp_dboption N'PermanentPortal', N'ANSI warnings', N'true'
GO

exec sp_dboption N'PermanentPortal', N'auto create statistics', N'true'
GO

exec sp_dboption N'PermanentPortal', N'auto update statistics', N'true'
GO

if( ( (@@microsoftversion / power(2, 24) = 8) and (@@microsoftversion & 0xffff >= 724) ) or ( (@@microsoftversion / power(2, 24) = 7) and (@@microsoftversion & 0xffff >= 1082) ) )
	exec sp_dboption N'PermanentPortal', N'db chaining', N'false'
GO

USE [PermanentPortal]
GO

EXEC sp_configure 'remote proc trans', '1'
GO

RECONFIGURE WITH OVERRIDE
GO

-- CREATE access for domain user ASPUSER

DECLARE @ASPUser nvarchar(100)
SELECT @ASPUser=DMZDomainName + '\ASPUSER' from master.dbo.Environment

if not exists (select * from dbo.sysusers where name = N'ASPUSER' and uid < 16382)
	EXEC sp_grantdbaccess @ASPUser, N'ASPUSER'
GO

exec sp_addrolemember N'db_owner', N'ASPUSER'
GO


---------------------------------------------------------------------------------------------
-- IMPORTANT: Report Server must exist before this script will work.
---------------------------------------------------------------------------------------------

CREATE TABLE #LinkedServers
(
	SRV_NAME varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS,
	SRV_PROVIDERNAME varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS,
	SRV_PRODUCT varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS,
	SRV_DATASOURCE varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS,
	SRV_PROVIDERSTRING varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS,
	SRV_LOCATION varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS,
	SRV_CAT varchar(500) COLLATE SQL_Latin1_General_CP1_CI_AS
)

INSERT INTO #LinkedServers
EXEC sp_linkedservers

IF EXISTS (SELECT SRV_NAME FROM #LinkedServers WHERE SRV_NAME = 'ReportServer')
	EXEC sp_dropserver @server='ReportServer'

DROP TABLE #LinkedServers

DECLARE @MISServerName nvarchar(100)
SELECT @MISServerName=MISServerName from master.dbo.Environment

DECLARE @ProviderString nvarchar(100)
SET @ProviderString = 'DRIVER={SQL Server};SERVER=' + @MISServerName + ';Trusted_Connection=Yes'

EXEC sp_addlinkedserver
	@server = 'ReportServer',
	@provider = 'MSDASQL',
	@srvproduct = '',
	@provstr = @ProviderString

EXEC sp_serveroption 'ReportServer', 'collation name', 'SQL_Latin1_General_CP1_CI_AS'

EXEC sp_serveroption 'ReportServer', 'rpc', 'TRUE'

EXEC sp_serveroption 'ReportServer', 'rpc out', 'TRUE'
GO