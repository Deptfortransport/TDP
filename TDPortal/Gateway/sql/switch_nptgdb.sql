-- ***********************************************
-- NAME 	: switch_additionaldatadb.sql
-- DESCRIPTION 	: Switches the names of the NPTG
--		: live and staging databases.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/build/DataGatewayApps/Gateway/sql/switch_nptgdb.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 11:53:46   mturner
--Initial revision.

USE master
GO

-- Check that the databases exist. If they don't we don't progress any further
IF DB_ID('NPTG') IS  NULL 
BEGIN
        RAISERROR (N'Unable to perform switch. Database NPTG does not exist', 16, 127) 

END
IF DB_ID('NPTG_Staging') IS  NULL 
BEGIN
        RAISERROR (N'Unable to perform switch. Database NPTG_Staging does not exist', 16, 127)

END

-- Update the access rights to the NPTG databases to allow for the databases to be renamed

ALTER database NPTG SET SINGLE_USER WITH ROLLBACK IMMEDIATE
ALTER database NPTG_Staging SET SINGLE_USER WITH ROLLBACK IMMEDIATE

-- Switch the database names

IF (@@ERROR = 0 )
BEGIN	
	Print 'Renaming NPTG_Staging as NPTG_Temp'
	EXEC sp_renamedb NPTG_Staging, NPTG_Temp
	Print 'Renaming NPTG as NPTG_Staging'
	EXEC sp_renamedb NPTG, NPTG_Staging
	Print 'Renaming NPTG_Temp as NPTG'
	EXEC sp_renamedb NPTG_Temp, NPTG

END
GO

-- Update the access rights to the NPTG databasess to allow for normal access

IF (@@ERROR = 0 ) 
BEGIN
	ALTER database NPTG SET MULTI_USER
	ALTER database NPTG_Staging SET MULTI_USER
END

