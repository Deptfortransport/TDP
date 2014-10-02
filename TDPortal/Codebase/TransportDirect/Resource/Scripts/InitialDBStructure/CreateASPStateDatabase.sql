-- ***********************************************
-- NAME 	: CreateASPStateDatabase.sql
-- DESCRIPTION 	: Creates the ASPState database, 
--		: including users and roles.
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreateASPStateDatabase.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:18   mturner
--Initial revision.

USE master
GO

/* Drop the database containing our sprocs */
IF DB_ID('ASPState') IS NOT NULL BEGIN
    DROP DATABASE ASPState
END
GO

/* Drop the obsolete startup enabler */
DECLARE @PROCID int
SET @PROCID = OBJECT_ID('EnableASPStateStartup')
IF @PROCID IS NOT NULL AND OBJECTPROPERTY(@PROCID, 'IsProcedure') = 1 BEGIN
    DROP PROCEDURE EnableASPStateStartup
END
GO

/* Drop the obsolete startup disabler */
DECLARE @PROCID int
SET @PROCID = OBJECT_ID('DisableASPStateStartup')
IF @PROCID IS NOT NULL AND OBJECTPROPERTY(@PROCID, 'IsProcedure') = 1 BEGIN
    DROP PROCEDURE DisableASPStateStartup
END
GO

/* Drop the ASPState_DeleteExpiredSessions_Job */
DECLARE @JobID BINARY(16)
SELECT @JobID = job_id
FROM   msdb.dbo.sysjobs
WHERE (name = N'ASPState_Job_DeleteExpiredSessions')
IF (@JobID IS NOT NULL)
BEGIN
    -- Check if the job is a multi-server job
    IF (EXISTS (SELECT  *
              FROM    msdb.dbo.sysjobservers
              WHERE   (job_id = @JobID) AND (server_id <> 0)))
    BEGIN
        -- There is, so abort the script
        RAISERROR (N'Unable to import job ''ASPState_Job_DeleteExpiredSessions'' since there is already a multi-server job with this name.', 16, 1)
    END
    ELSE
        -- Delete the [local] job
        EXECUTE msdb.dbo.sp_delete_job @job_name = N'ASPState_Job_DeleteExpiredSessions'
END

USE master
GO

/* Create and populate the ASPState database */
CREATE DATABASE ASPState
GO

USE [ASPState]
GO

-- CREATE access for domain user ASPUSER

DECLARE @ASPUser nvarchar(100)
SELECT @ASPUser=DMZDomainName + '\ASPUSER' from master.dbo.Environment

if not exists (select * from dbo.sysusers where name = N'ASPUSER' and uid < 16382)
	EXEC sp_grantdbaccess @ASPUser, N'ASPUSER'
GO

exec sp_addrolemember N'db_owner', N'ASPUSER'
GO
