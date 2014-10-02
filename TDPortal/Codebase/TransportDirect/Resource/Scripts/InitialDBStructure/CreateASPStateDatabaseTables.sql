-- ***********************************************
-- NAME 	: CreateASPStateDatabaseTables.sql
-- DESCRIPTION 	: Creates the ASPState database tables, 
--		: including sprocs, functions and triggers
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/InitialDBStructure/CreateASPStateDatabaseTables.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:18   mturner
--Initial revision.

USE ASPState
GO

SET QUOTED_IDENTIFIER OFF
GO
SET ANSI_NULLS ON
GO

CREATE PROCEDURE DropTempTables
AS
    IF EXISTS (SELECT * FROM ASPState..sysobjects WHERE name = 'ASPStateTempSessions' AND type = 'U') BEGIN
        DROP TABLE ASPState..ASPStateTempSessions
    END

    IF EXISTS (SELECT * FROM ASPState..sysobjects WHERE name = 'ASPStateTempApplications' AND type = 'U') BEGIN
        DROP TABLE ASPState..ASPStateTempApplications
    END
	-- Include DeferredData table
    IF EXISTS (SELECT * FROM ASPState..sysobjects WHERE name = 'DeferredData' AND type = 'U') BEGIN
        DROP TABLE ASPState..DeferredData
    END

    RETURN 0
GO

CREATE PROCEDURE CreateTempTables
AS
    /*
     * Note that we cannot create user-defined data types in
     * ASPState because sp_addtype must be run in the context
     * of the current database, and we cannot switch to
     * ASPState from a stored procedure.
     */

    CREATE TABLE ASPState..ASPStateTempSessions (
        SessionId           CHAR(32)        NOT NULL PRIMARY KEY,
        Created             DATETIME        NOT NULL DEFAULT GETDATE(),
        Expires             DATETIME        NOT NULL,
        LockDate            DATETIME        NOT NULL,
        LockCookie          INT             NOT NULL,
        Timeout             INT             NOT NULL,
        Locked              BIT             NOT NULL,
        SessionItemShort    VARBINARY(7000) NULL,
        SessionItemLong     IMAGE           NULL,
    )

    CREATE TABLE ASPState..ASPStateTempApplications (
        AppId               INT             NOT NULL IDENTITY PRIMARY KEY,
        AppName             CHAR(280)       NOT NULL,
    )

    CREATE NONCLUSTERED INDEX Index_AppName ON ASPState..ASPStateTempApplications(AppName)

    -- Addditional table added to store deferred data
    CREATE TABLE ASPState..DeferredData (
        SessionId           CHAR(32)        NOT NULL,
        KeyID				CHAR(48)	    NOT NULL,
        SessionItemLong     IMAGE           NULL,
    )

	-- Add a compound primary key on SessionID & KeyID
	ALTER TABLE ASPState..DeferredData ADD 
	CONSTRAINT PK__DeferredData__0F975522 PRIMARY KEY  CLUSTERED 
	(
		SessionId,
		KeyID
	) 

	-- Add Foreign key constraint to enable Session data in DeferredData
	-- to be automatically deleted when it is removed from ASPStateTempSessions
    ALTER TABLE ASPState..DeferredData ADD
	CONSTRAINT FK_DeferredData_ASPStateTempSessions FOREIGN KEY
	(
		SessionId
	) REFERENCES ASPState..ASPStateTempSessions (
		SessionId
	) ON DELETE CASCADE

	-- Add table-level constraint
    ALTER TABLE ASPState..DeferredData ADD
	CONSTRAINT U_DeferredData UNIQUE NONCLUSTERED
	(
		KeyID,
		SessionId
	) 
	
	

    RETURN 0
GO

CREATE PROCEDURE ResetData
AS
    EXECUTE DropTempTables
    EXECUTE CreateTempTables
    RETURN 0
GO

EXECUTE sp_addtype tSessionId, 'CHAR(32)',  'NOT NULL'
GO

EXECUTE sp_addtype tAppName, 'VARCHAR(280)', 'NOT NULL'
GO

EXECUTE sp_addtype tSessionItemShort, 'VARBINARY(7000)'
GO

EXECUTE sp_addtype tSessionItemLong, 'IMAGE'
GO

EXECUTE sp_addtype tTextPtr, 'VARBINARY(16)'
GO

CREATE PROCEDURE TempGetAppID
    @appName    tAppName,
    @appId      INT OUTPUT
AS
    SELECT @appId = AppId
    FROM ASPState..ASPStateTempApplications
    WHERE AppName = @appName

    IF @appId IS NULL BEGIN
        INSERT ASPState..ASPStateTempApplications
            (AppName)
        VALUES
            (@appName)

        SELECT @appId = AppId
        FROM ASPState..ASPStateTempApplications
        WHERE AppName = @appName
    END

    RETURN 0
GO

CREATE PROCEDURE TempGetStateItem
    @id         tSessionId,
    @itemShort  tSessionItemShort OUTPUT,
    @locked     BIT OUTPUT,
    @lockDate   DATETIME OUTPUT,
    @lockCookie INT OUTPUT
AS
    DECLARE @textptr AS tTextPtr
    DECLARE @length AS INT
    DECLARE @now as DATETIME
    SET @now = GETDATE()

    UPDATE ASPState..ASPStateTempSessions
    SET Expires = DATEADD(n, Timeout, @now),
        @locked = Locked,
        @lockDate = LockDate,
        @lockCookie = LockCookie,
        @itemShort = CASE @locked
            WHEN 0 THEN SessionItemShort
            ELSE NULL
            END,
        @textptr = CASE @locked
            WHEN 0 THEN TEXTPTR(SessionItemLong)
            ELSE NULL
            END,
        @length = CASE @locked
            WHEN 0 THEN DATALENGTH(SessionItemLong)
            ELSE NULL
            END
    WHERE SessionId = @id
    IF @length IS NOT NULL BEGIN
        READTEXT ASPState..ASPStateTempSessions.SessionItemLong @textptr 0 @length
    END

    RETURN 0
GO

-- Create SPROC to enable retrieval of deferred data
CREATE PROCEDURE TempGetDeferredData
    @id         tSessionId,
    @key	char(48)
AS
    DECLARE @textptr AS tTextPtr
    DECLARE @length AS INT

    SELECT
        @textptr = TEXTPTR(SessionItemLong),
        @length = DATALENGTH(SessionItemLong)
    FROM AspState..DeferredData
    WHERE SessionID = @id
    IF @length IS NOT NULL BEGIN
        READTEXT ASPState..DeferredData.SessionItemLong @textptr 0 @length
    END

    RETURN 0
GO


CREATE PROCEDURE TempGetStateItemExclusive
    @id         tSessionId,
    @itemShort  tSessionItemShort OUTPUT,
    @locked     BIT OUTPUT,
    @lockDate   DATETIME OUTPUT,
    @lockCookie INT OUTPUT
AS
    DECLARE @textptr AS tTextPtr
    DECLARE @length AS INT
    DECLARE @now as DATETIME

    SET @now = GETDATE()
    UPDATE ASPState..ASPStateTempSessions
    SET Expires = DATEADD(n, Timeout, @now),
        @lockDate = LockDate = CASE Locked
            WHEN 0 THEN @now
            ELSE LockDate
            END,
        @lockCookie = LockCookie = CASE Locked
            WHEN 0 THEN LockCookie + 1
            ELSE LockCookie
            END,
        @itemShort = CASE Locked
            WHEN 0 THEN SessionItemShort
            ELSE NULL
            END,
        @textptr = CASE Locked
            WHEN 0 THEN TEXTPTR(SessionItemLong)
            ELSE NULL
            END,
        @length = CASE Locked
            WHEN 0 THEN DATALENGTH(SessionItemLong)
            ELSE NULL
            END,
        @locked = Locked,
        Locked = 1
    WHERE SessionId = @id
    IF @length IS NOT NULL BEGIN
        READTEXT ASPState..ASPStateTempSessions.SessionItemLong @textptr 0 @length
    END

    RETURN 0
GO

CREATE PROCEDURE TempReleaseStateItemExclusive
    @id         tSessionId,
    @lockCookie INT
AS
    UPDATE ASPState..ASPStateTempSessions
    SET Expires = DATEADD(n, Timeout, GETDATE()),
        Locked = 0
    WHERE SessionId = @id AND LockCookie = @lockCookie

    RETURN 0
GO


CREATE PROCEDURE TempInsertStateItemShort
    @id         tSessionId,
    @itemShort  tSessionItemShort,
    @timeout    INT
AS

    DECLARE @now as DATETIME
    SET @now = GETDATE()

    INSERT ASPState..ASPStateTempSessions
        (SessionId,
         SessionItemShort,
         Timeout,
         Expires,
         Locked,
         LockDate,
         LockCookie)
    VALUES
        (@id,
         @itemShort,
         @timeout,
         DATEADD(n, @timeout, @now),
         0,
         @now,
         1)

    RETURN 0
GO

CREATE PROCEDURE TempInsertStateItemLong
    @id         tSessionId,
    @itemLong   tSessionItemLong,
    @timeout    INT
AS
    DECLARE @now as DATETIME
    SET @now = GETDATE()

    INSERT ASPState..ASPStateTempSessions
        (SessionId,
         SessionItemLong,
         Timeout,
         Expires,
         Locked,
         LockDate,
         LockCookie)
    VALUES
        (@id,
         @itemLong,
         @timeout,
         DATEADD(n, @timeout, @now),
         0,
         @now,
         1)

    RETURN 0
GO

-- Create SPROC to enable storing of deferred data
CREATE PROCEDURE TempInsertDeferredData
    @id         tSessionId,
    @key	char(48),
    @itemLong   tSessionItemLong

AS
    INSERT AspState..DeferredData
        (SessionId,
	 KeyID,	
         SessionItemLong)
    VALUES
        (@id,
         @key,
         @itemLong)

    RETURN 0
GO

CREATE PROCEDURE TempUpdateStateItemShort
    @id         tSessionId,
    @itemShort  tSessionItemShort,
    @timeout    INT,
    @lockCookie INT
AS
    UPDATE ASPState..ASPStateTempSessions
    SET Expires = DATEADD(n, Timeout, GETDATE()),
        SessionItemShort = @itemShort,
        Timeout = @timeout,
        Locked = 0
    WHERE SessionId = @id AND LockCookie = @lockCookie

    RETURN 0
GO

CREATE PROCEDURE TempUpdateStateItemShortNullLong
    @id         tSessionId,
    @itemShort  tSessionItemShort,
    @timeout    INT,
    @lockCookie INT
AS
    UPDATE ASPState..ASPStateTempSessions
    SET Expires = DATEADD(n, Timeout, GETDATE()),
        SessionItemShort = @itemShort,
        SessionItemLong = NULL,
        Timeout = @timeout,
        Locked = 0
    WHERE SessionId = @id AND LockCookie = @lockCookie

    RETURN 0
GO

CREATE PROCEDURE TempUpdateStateItemLong
    @id         tSessionId,
    @itemLong   tSessionItemLong,
    @timeout    INT,
    @lockCookie INT
AS
    UPDATE ASPState..ASPStateTempSessions
    SET Expires = DATEADD(n, Timeout, GETDATE()),
        SessionItemLong = @itemLong,
        Timeout = @timeout,
        Locked = 0
    WHERE SessionId = @id AND LockCookie = @lockCookie

    RETURN 0
GO

-- Create SPROC to enable update of deferred data
CREATE PROCEDURE TempUpdateDeferredData
    @id         tSessionId,
    @key	char(48),
    @itemLong   tSessionItemLong
AS
    UPDATE ASPState..DeferredData
    SET SessionItemLong = @itemLong
    WHERE SessionId = @id AND KeyID = @key

    RETURN 0
GO

CREATE PROCEDURE TempUpdateStateItemLongNullShort
    @id         tSessionId,
    @itemLong   tSessionItemLong,
    @timeout    INT,
    @lockCookie INT
AS
    UPDATE ASPState..ASPStateTempSessions
    SET Expires = DATEADD(n, Timeout, GETDATE()),
        SessionItemLong = @itemLong,
        SessionItemShort = NULL,
        Timeout = @timeout,
        Locked = 0
    WHERE SessionId = @id AND LockCookie = @lockCookie

    RETURN 0
GO

CREATE PROCEDURE TempRemoveStateItem
    @id     tSessionId,
    @lockCookie INT
AS
    DELETE ASPState..ASPStateTempSessions
    WHERE SessionId = @id AND LockCookie = @lockCookie
    RETURN 0
GO

CREATE PROCEDURE TempResetTimeout
    @id     tSessionId
AS
    UPDATE ASPState..ASPStateTempSessions
    SET Expires = DATEADD(n, Timeout, GETDATE())
    WHERE SessionId = @id
    RETURN 0
GO

CREATE PROCEDURE DeleteExpiredSessions
AS
    DECLARE @now DATETIME
    SET @now = GETDATE()

    DELETE ASPState..ASPStateTempSessions
    WHERE Expires < @now

    RETURN 0
GO

SET QUOTED_IDENTIFIER ON 
GO
SET ANSI_NULLS OFF 
GO


CREATE PROCEDURE usp_GetAppID
    @appName    tAppName
AS
    select AppId
    FROM ASPState..ASPStateTempApplications
    WHERE AppName = @appName

    RETURN 0
GO


/****** Object:  Stored Procedure dbo.usp_SaveDeferredData    Script Date: 15/09/2003 15:52:44 ******/
CREATE PROCEDURE dbo.usp_SaveDeferredData ( @SessionID varchar(32), 
					   						@KeyID char(48), 
					    					@SessionItem tSessionItemLong )
AS

/*
***********************************************************************************************************
   NAME 	: usp_SaveDeferredData
   AUTHOR 	: James Cotton
   DATE CREATED : 10-Sept-2003
   DESCRIPTION 	: TD Session Manager Component.

	Note: @SessionID must be a varchar for the LIKE X + % to work.
	          Char will pad and stop any match happening.

***********************************************************************************************************
*/

DECLARE @TempSessionID tSessionId

-- Test for an entry in ASPStateTempSessions.SessionID Like the Session ID passed in @SessionID
IF EXISTS (SELECT SessionID FROM ASPState..ASPStateTempSessions ats WHERE ats.SessionID = @SessionID )

BEGIN

	-- Test for required Table
	IF EXISTS (SELECT * 
		     FROM INFORMATION_SCHEMA.TABLES 
	      	    WHERE TABLE_NAME = 'DeferredData')
		
	BEGIN -- Table exists so we append or update
	
		SELECT @TempSessionID = SessionID FROM ASPState..ASPStateTempSessions WHERE SessionID = @SessionID
		 
		-- Test for an existing entry
		IF EXISTS (SELECT SessionID FROM DeferredData 
	        	    WHERE SessionID = @TempSessionID 
			      AND KeyID = @KeyID)
	
		BEGIN -- UPDATE an existing entry
	
			UPDATE DeferredData
			   SET SessionItemLong = @SessionItem
	        	 WHERE SessionID = @TempSessionID 
			   AND KeyID = @KeyID
	
		END
		ELSE
		BEGIN -- APPEND a new record
	
			INSERT INTO ASPState..DeferredData (SessionID, KeyID, SessionItemLong)
			     VALUES(@TempSessionID, @KeyID, @SessionItem)
	
		END
	END
	ELSE
	BEGIN -- Table does not exists so we build it
		
		-- TODO: Log the fact that the table was missing
	
		-- Re-create the table 
		CREATE TABLE [DeferredData] (	[SessionId] [char] (32) COLLATE Latin1_General_CI_AS NOT NULL ,
						[KeyID] [char] (48) COLLATE Latin1_General_CI_AS NOT NULL ,
						[SessionItemLong] [image] NULL ,
						PRIMARY KEY  CLUSTERED ( [SessionId] )  ON [PRIMARY] ,
						CONSTRAINT [U_DeferredData] UNIQUE  NONCLUSTERED ( [KeyID], [SessionId] )  ON [PRIMARY] ,
						CONSTRAINT [FK_DeferredData_ASPStateTempSessions] FOREIGN KEY ( [SessionId] ) 
							REFERENCES [ASPStateTempSessions] ( [SessionId] ) ON DELETE CASCADE 
					    ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
	
		-- Now try again
		EXEC ASPState..usp_SaveDeferredData @SessionID, @KeyID, @SessionItem
	
	END
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

/****** Object:  Stored Procedure dbo.usp_GetDeferredData    Script Date: 15/09/2003 15:51:56 ******/
CREATE PROCEDURE dbo.usp_GetDeferredData ( @SessionID varchar(32), 
					   					   @KeyID char(48) )
AS
    
/* 
***********************************************************************************************************
   NAME 	: usp_GetDeferredData
   AUTHOR 	: James Cotton
   DATE CREATED : 10-Sept-2003
   DESCRIPTION 	: TD Session Manager Component.

	Note: @SessionID must be a varchar for the LIKE X + % to work.
	          Char will pad and stop any match happening.

***********************************************************************************************************
*/

DECLARE @textptr AS tTextPtr
DECLARE @length AS INT
DECLARE @TempSessionID tSessionId

-- Test for an entry in ASPState..ASPStateTempSessions.SessionID Like the Session ID passed in @SessionID
IF EXISTS (SELECT SessionID FROM ASPState..ASPStateTempSessions ats WHERE ats.SessionID = @SessionID)
BEGIN
	
    SELECT @TempSessionID = SessionID FROM ASPState..ASPStateTempSessions WHERE SessionID = @SessionID

    SELECT
        @textptr = TEXTPTR(SessionItemLong),
        @length = DATALENGTH(SessionItemLong)
    FROM AspState..DeferredData
    WHERE SessionID = @TempSessionID
          AND KeyID = @KeyID
   
     IF @length IS NOT NULL 
	BEGIN
	        READTEXT ASPState..DeferredData.SessionItemLong @textptr 0 @length
	END

    RETURN 0

END
ELSE
BEGIN
    RETURN -1
END
GO

SET QUOTED_IDENTIFIER OFF 
GO
SET ANSI_NULLS ON 
GO


EXECUTE CreateTempTables
GO


/****** Object:  Trigger dbo.DeleteEsriMapData    Script Date: 05/09/2003 12:04:12 ******/
CREATE TRIGGER DeleteEsriMapData ON [ASPState].[dbo].[ASPStateTempSessions] 
FOR DELETE 
AS

DELETE FROM [MASTERMAP].[mapadmin].[PT_ROUTES]
WHERE [MASTERMAP].[mapadmin].[PT_ROUTES].[SESSIONID] IN (SELECT SESSIONID FROM DELETED)

DELETE FROM [MASTERMAP].[mapadmin].[RD_ROUTES]
WHERE [MASTERMAP].[mapadmin].[RD_ROUTES].[SESSIONID] IN (SELECT SESSIONID FROM DELETED)

GO


/* Create the startup procedure */
USE master
GO

/* Create the job to delete expired sessions */
BEGIN TRANSACTION
    DECLARE @JobID BINARY(16)
    DECLARE @ReturnCode INT
    SELECT @ReturnCode = 0

    -- Add job category
    IF (SELECT COUNT(*) FROM msdb.dbo.syscategories WHERE name = N'[Uncategorized (Local)]') < 1
        EXECUTE msdb.dbo.sp_add_category @name = N'[Uncategorized (Local)]'

    -- Add the job
    EXECUTE @ReturnCode = msdb.dbo.sp_add_job
            @job_id = @JobID OUTPUT,
            @job_name = N'ASPState_Job_DeleteExpiredSessions',
            @owner_login_name = NULL,
            @description = N'Deletes expired sessions from the session state database.',
            @category_name = N'[Uncategorized (Local)]',
            @enabled = 1,
            @notify_level_email = 0,
            @notify_level_page = 0,
            @notify_level_netsend = 0,
            @notify_level_eventlog = 0,
            @delete_level= 0

    IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

    -- Add the job steps
    EXECUTE @ReturnCode = msdb.dbo.sp_add_jobstep
            @job_id = @JobID,
            @step_id = 1,
            @step_name = N'ASPState_JobStep_DeleteExpiredSessions',
            @command = N'EXECUTE DeleteExpiredSessions',
            @database_name = N'ASPState',
            @server = N'',
            @database_user_name = N'',
            @subsystem = N'TSQL',
            @cmdexec_success_code = 0,
            @flags = 0,
            @retry_attempts = 0,
            @retry_interval = 1,
            @output_file_name = N'',
            @on_success_step_id = 0,
            @on_success_action = 1,
            @on_fail_step_id = 0,
            @on_fail_action = 2

    IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

    EXECUTE @ReturnCode = msdb.dbo.sp_update_job @job_id = @JobID, @start_step_id = 1
    IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

    -- Add the job schedules
    EXECUTE @ReturnCode = msdb.dbo.sp_add_jobschedule
            @job_id = @JobID,
            @name = N'ASPState_JobSchedule_DeleteExpiredSessions',
            @enabled = 1,
            @freq_type = 4,
            @active_start_date = 20001016,
            @active_start_time = 0,
            @freq_interval = 1,
            @freq_subday_type = 4,
            @freq_subday_interval = 1,
            @freq_relative_interval = 0,
            @freq_recurrence_factor = 0,
            @active_end_date = 99991231,
            @active_end_time = 235959

    IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

    -- Add the Target Servers
    EXECUTE @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @JobID, @server_name = N'(local)'
    IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

    COMMIT TRANSACTION
    GOTO   EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:
GO
