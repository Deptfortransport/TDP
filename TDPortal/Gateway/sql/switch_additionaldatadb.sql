-- ***********************************************
-- NAME 	: switch_additionaldatadb.sql
-- DESCRIPTION 	: Switches the names of the AdditionalData
--		: live and staging databases.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/build/DataGatewayApps/Gateway/sql/switch_additionaldatadb.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 11:53:46   mturner
--Initial revision.

USE master
GO

-- Check that the databases exist. If they don't we don't progress any further
IF DB_ID('AdditionalData') IS  NULL 
BEGIN
        RAISERROR (N'Unable to perform switch. Database AdditionalData does not exist', 16, 127) 

END
IF DB_ID('AdditionalData_Staging') IS  NULL 
BEGIN
        RAISERROR (N'Unable to perform switch. Database AdditionalData_Staging does not exist', 16, 127)

END

-- Update the access rights to the AdditionalData databases to allow for the databases to be renamed

ALTER database AdditionalData SET SINGLE_USER WITH ROLLBACK IMMEDIATE
ALTER database AdditionalData_Staging SET SINGLE_USER WITH ROLLBACK IMMEDIATE

-- Switch the database names

IF (@@ERROR = 0 )
BEGIN	
	Print 'Renaming AdditionalData_Staging as AdditionalData_Temp'
	EXEC sp_renamedb AdditionalData_Staging, AdditionalData_Temp
	Print 'Renaming AdditionalData as AdditionalData_Staging'
	EXEC sp_renamedb AdditionalData, AdditionalData_Staging
	Print 'Renaming AdditionalData_Temp as AdditionalData'
	EXEC sp_renamedb AdditionalData_Temp, AdditionalData

END
GO

-- Update the access rights to the AdditionalData databasess to allow for normal access

IF (@@ERROR = 0 ) 
BEGIN
	ALTER database AdditionalData SET MULTI_USER
	ALTER database AdditionalData_Staging SET MULTI_USER
END




