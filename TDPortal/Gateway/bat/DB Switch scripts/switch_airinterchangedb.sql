-- ***********************************************
-- NAME 	: switch_airinterchangedb.sql
-- DESCRIPTION 	: Switches the names of the AirInterchange
--		: live and staging databases.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/build/DataGatewayApps/Gateway/bat/DB Switch scripts/switch_airinterchangedb.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 11:53:36   mturner
--Initial revision.
--
--   Rev 1.0   Mar 12 2007 12:21:24   scraddock
--Initial revision.

USE master
GO

-- Check that the databases exist. If they don't we don't progress any further
IF DB_ID('AirInterchange') IS  NULL 
	BEGIN
        RAISERROR (N'Unable to perform switch. Database AirInterchange does not exist', 16, 127) 
	END

IF DB_ID('AirInterchange_Staging') IS  NULL 
	BEGIN
        RAISERROR (N'Unable to perform switch. Database AirInterchange_Staging does not exist', 16, 127)
	END

-- Update the access rights to the AirInterchange databases to allow for the databases to be renamed
ALTER database AirInterchange SET SINGLE_USER WITH ROLLBACK IMMEDIATE
ALTER database AirInterchange_Staging SET SINGLE_USER WITH ROLLBACK IMMEDIATE

-- Switch the database names
IF (@@ERROR = 0 )
	BEGIN
		Print 'Renaming AirInterchange_Staging as AirInterchange_Temp'
		EXEC sp_renamedb AirInterchange_Staging, AirInterchange_Temp
		Print 'Renaming AirInterchange as AirInterchange_Staging'
		EXEC sp_renamedb AirInterchange, AirInterchange_Staging
		Print 'Renaming AirInterchange_Temp as AirInterchange'
		EXEC sp_renamedb AirInterchange_Temp, AirInterchange
	END
GO

-- Update the access rights to the AirInterchange databasess to allow for normal access
IF (@@ERROR = 0 ) 
	BEGIN
		ALTER database AirInterchange SET MULTI_USER
		ALTER database AirInterchange_Staging SET MULTI_USER
	END