-- ***********************************************
-- NAME 	: CreateFTPServerConfigurationTables.sql
-- DESCRIPTION 	: Creates the FTPServer database tables, indexes, constraints,
--		: foreign keys, sprocs and functions.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreateFTPServerConfigurationTables.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:20   mturner
--Initial revision.


USE [PermanentPortal]
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[FTP_CONFIGURATION]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[FTP_CONFIGURATION]
GO

CREATE TABLE FTP_CONFIGURATION(
	FTP_CLIENT INT ,
	DATA_FEED  VARCHAR(16) NOT NULL,
	IP_ADDRESS VARCHAR(16) NOT NULL,
	USERNAME   VARCHAR(32) NOT NULL,
	PASSWORD   VARCHAR(32) NOT NULL,
	LOCAL_DIR  VARCHAR(128) NOT NULL,
	REMOTE_DIR VARCHAR(128) NOT NULL,
	FILENAME_FILTER VARCHAR(32) NOT NULL,
	MISSING_FEED_COUNTER INT,
	MISSING_FEED_THRESHOLD INT,
	DATA_FEED_DATETIME DATETIME,	
	DATA_FEED_FILENAME VARCHAR(128) NOT NULL,
	REMOVE_FILES Bit 
)
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[IMPORT_CONFIGURATION]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[IMPORT_CONFIGURATION]
GO

CREATE TABLE IMPORT_CONFIGURATION(
	DATA_FEED VARCHAR(16),
	IMPORT_CLASS VARCHAR(128),
	CLASS_ARCHIVE VARCHAR(128),
	IMPORT_UTILITY VARCHAR(128),
	PARAMETERS1 VARCHAR(128),
	PARAMETERS2 VARCHAR(128),
	PROCESSING_DIR VARCHAR(128)	
)
GO

