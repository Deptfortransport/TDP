-- ***********************************************
-- NAME 	: switch_gazdb.sql
-- DESCRIPTION 	: Switches the names of the GAZ
--		: live and staging databases.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/build/DataGatewayApps/Gateway/bat/DB Switch scripts/switch_GAZdb.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 11:53:38   mturner
--Initial revision.
--
--   Rev 1.0   Mar 12 2007 12:21:26   scraddock
--Initial revision.

USE master
GO

-- Check that the databases exist. If they don't we don't progress any further
IF DB_ID('GAZ') IS  NULL 
BEGIN
        RAISERROR (N'Unable to perform switch. Database GAZ does not exist', 16, 127) 

END
IF DB_ID('GAZ_Staging') IS  NULL 
BEGIN
        RAISERROR (N'Unable to perform switch. Database GAZ_Staging does not exist', 16, 127)

END

-- Update the access rights to the GAZ databases to allow for the databases to be renamed

ALTER database GAZ SET SINGLE_USER WITH ROLLBACK IMMEDIATE
ALTER database GAZ_Staging SET SINGLE_USER WITH ROLLBACK IMMEDIATE

-- Switch the database names

IF (@@ERROR = 0 )
BEGIN	
	Print 'Renaming GAZ_Staging as GAZ_Temp'
	EXEC sp_renamedb GAZ_Staging, GAZ_Temp
	Print 'Renaming GAZ as GAZ_Staging'
	EXEC sp_renamedb GAZ, GAZ_Staging
	Print 'Renaming GAZ_Temp as GAZ'
	EXEC sp_renamedb GAZ_Temp, GAZ

END
GO

-- Update the access rights to the GAZ databasess to allow for normal access

IF (@@ERROR = 0 ) 
BEGIN
	ALTER database GAZ SET MULTI_USER
	ALTER database GAZ_Staging SET MULTI_USER
END

go

sp_defaultdb 'gazadmin','GAZ'

go
