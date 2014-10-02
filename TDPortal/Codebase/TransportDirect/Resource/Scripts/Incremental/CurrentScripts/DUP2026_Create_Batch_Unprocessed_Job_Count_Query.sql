-- ***********************************************
-- NAME 		: DUP2026_Create_Batch_Unprocessed_Job_Count_Query.sql
-- DESCRIPTION 	        : Script to Create   Stored Proc and Agent Job to count Batch Unprocessed Jobs
--                      : and write to event log if unprocessed count goes over Fifty, thus enabling alerts to be generated / callouts invoked.
-- AUTHOR		: Phil Scott
-- DATE			: 11/06/2013
-- ************************************************

--************************** NOTE ***********************************
--    ENSURE permissions on the agent job are changed according to environment by remming out correct lines
--
--********************************************************************

USE [BatchJourneyPlanner]
GO

/****** Object:  StoredProcedure [dbo].[BatchUnprocessedQueueTest]    Script Date: 06/10/2013 15:48:21 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[BatchUnprocessedQueueTest]

AS
BEGIN

	SET NOCOUNT ON;
	DECLARE @msg VARCHAR(100)


	SET NOCOUNT ON;
    	IF (( select COUNT(*) 
    	from [BatchJourneyPlanner].[dbo].[BatchRequestDetail]
    	where CompletedDateTime is null 
    	and  BatchDetailStatusId is null ) >= 1)
	BEGIN
	    IF ((select datediff(minute, MIN(SubmittedDateTime),GETDATE()) 
        	from [BatchJourneyPlanner].[dbo].[BatchRequestDetail]
        	Where CompletedDateTime is null 
        	and SubmittedDateTime is not null
        	) >5)
	    BEGIN
	        IF (
	        (select datediff(minute, MAX(completedDateTime),GETDATE()) 
                 from [BatchJourneyPlanner].[dbo].[BatchRequestDetail])
                 > 20)        
                BEGIN
            	  set @msg = 'Warning -  Batch system not processing unprocessed batches'
         	  EXEC xp_logevent 505050, @msg,  Warning 
                END
            END
        END
    
     RETURN 0
     END

GO


--   **************************************************************************************

USE [msdb]
GO

/****** Object:  Job [BatchUnprocessedQueueTest]    Script Date: 06/10/2013 15:47:38 ******/
BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0
/****** Object:  JobCategory [[Uncategorized (Local)]]]    Script Date: 06/10/2013 15:47:38 ******/
IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'[Uncategorized (Local)]' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'[Uncategorized (Local)]'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'BatchUnprocessedQueueTest', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'Batch system queue length test - writes to event log if unprocessed batches are not being pocessed.', 
		@category_name=N'[Uncategorized (Local)]', 
		@owner_login_name=N'BBPTDPSIS\siadmin', @job_id = @jobId OUTPUT
--		@owner_login_name=N'BBPTDPS\siadmin', @job_id = @jobId OUTPUT
--		@owner_login_name=N'ACPTDPS\siadmin', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [run test]    Script Date: 06/10/2013 15:47:38 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'run test', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'EXEC BatchUnprocessedQueueTest', 
		@database_name=N'BatchJourneyPlanner', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'run batch pending check every 5 mins', 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=4, 
		@freq_subday_interval=5, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20130524, 
		@active_end_date=99991231, 
		@active_start_time=0, 
		@active_end_time=235959, 
		@schedule_uid=N'71fa8849-ca50-422d-bebf-acebfd971095'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

GO






----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2026
SET @ScriptDesc = 'Create Batch Unprocessed Jobs Count Query'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc)
  END
GO