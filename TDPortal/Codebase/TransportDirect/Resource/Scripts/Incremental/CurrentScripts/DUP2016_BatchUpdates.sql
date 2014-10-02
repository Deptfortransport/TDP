-- ***********************************************
-- NAME 		: DUP2016_BatchUpdates.sql
-- DESCRIPTION 	: Updates for CCN0648c - Super batch
-- AUTHOR		: David Lane
-- DATE			: 22 Mar 2013
-- ************************************************

USE BatchJourneyPlanner
GO

-- Zip field
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BatchRequestSummary' AND COLUMN_NAME = 'ZipDownload')
BEGIN
ALTER TABLE BatchRequestSummary
ADD ZipDownload VARBINARY(MAX) NULL
END
GO

-- Percentage complete
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BatchRequestSummary' AND COLUMN_NAME = 'PercentageComplete')
BEGIN
ALTER TABLE BatchRequestSummary
ADD PercentageComplete INT NULL
END
GO

-- Queued position
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'BatchRequestSummary' AND COLUMN_NAME = 'QueuedPosition')
BEGIN
ALTER TABLE BatchRequestSummary
ADD QueuedPosition INT NULL
END
GO

-- User's upload limit
IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'RegisteredUser' AND COLUMN_NAME = 'UploadLimit')
BEGIN
ALTER TABLE RegisteredUser
ADD UploadLimit INT NOT NULL DEFAULT 0
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBatchRequestsForUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetBatchRequestsForUser]
GO
-- GetBatchRequestsForUser proc
CREATE PROCEDURE [dbo].[GetBatchRequestsForUser]
(
	@EmailAddress nvarchar(250),
	@Page int,
	@SortColumn nvarchar(100),
	@SortDirection nvarchar(5)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Get the user id
	DECLARE @UserId int
	SELECT @UserId = UserId
	FROM RegisteredUser
	WHERE @EmailAddress = EmailAddress
	
	-- Paging vars (20 per page)
	DECLARE @StartRow int
	DECLARE @EndRow int
	SET @StartRow = 20 * (@Page - 1) + 1
	SET @EndRow = @StartRow + 19
	
	DECLARE @Total int
	SELECT @Total = COUNT(*)
		FROM BatchRequestSummary
		WHERE UserId = @UserId;
	
	-- Get the user's batch requests
	WITH Results AS
	(SELECT BatchJourneyPlanner.dbo.PadBatchId(CONVERT(nvarchar(20), [BatchId])) AS BatchId
		,CASE WHEN [CompletedDateTime] = NULL THEN '' ELSE CONVERT(nvarchar(10), [CompletedDateTime], 104) END AS CompletedDateTime
		,CONVERT(nvarchar(10), [QueuedDateTime], 104) AS QueuedDateTime
		,[NumberOfRequests]
		,CASE WHEN brs.BatchStatusId != 3 THEN '' ELSE [NumberOfSuccessfulResults] END AS NumberOfSuccessfulResults
		,CASE WHEN brs.BatchStatusId != 3 THEN '' ELSE [NumberOfPartialSuccesses] END AS NumberOfPartialSuccesses
		,CASE WHEN brs.BatchStatusId != 3 THEN '' ELSE [NumberOfInvalidRequests] END AS NumberOfInvalidRequests
		,CASE WHEN brs.BatchStatusId != 3 THEN '' ELSE [NumberOfUnsuccessfulRequests] END AS NumberOfUnsuccessfulRequests
		,[BatchStatusDescription]
		,[ReportParameters.IncludePublicTransport]
		,[ReportParameters.IncludeCar]
		,[ReportParameters.IncludeCycle]
		,ROW_NUMBER() OVER
			(ORDER BY
				CASE WHEN @SortColumn = 'BatchId' AND @SortDirection = 'asc' THEN BatchId END ASC,
				CASE WHEN @SortColumn = 'BatchId' AND @SortDirection = 'desc' THEN BatchId END DESC,
				CASE WHEN @SortColumn = 'Submitted' AND @SortDirection = 'asc' THEN QueuedDateTime END ASC,
				CASE WHEN @SortColumn = 'Submitted' AND @SortDirection = 'desc' THEN QueuedDateTime END DESC,
				CASE WHEN @SortColumn = 'PublicTransport' AND @SortDirection = 'asc' THEN [ReportParameters.IncludePublicTransport] END ASC,
				CASE WHEN @SortColumn = 'PublicTransport' AND @SortDirection = 'desc' THEN [ReportParameters.IncludePublicTransport] END DESC,
				CASE WHEN @SortColumn = 'Car' AND @SortDirection = 'asc' THEN [ReportParameters.IncludeCar] END ASC,
				CASE WHEN @SortColumn = 'Car' AND @SortDirection = 'desc' THEN [ReportParameters.IncludeCar] END DESC,
				CASE WHEN @SortColumn = 'Cycle' AND @SortDirection = 'asc' THEN [ReportParameters.IncludeCycle] END ASC,
				CASE WHEN @SortColumn = 'Cycle' AND @SortDirection = 'desc' THEN [ReportParameters.IncludeCycle] END DESC,
				CASE WHEN @SortColumn = 'NumberRequests' AND @SortDirection = 'asc' THEN NumberOfRequests END ASC,
				CASE WHEN @SortColumn = 'NumberRequests' AND @SortDirection = 'desc' THEN NumberOfRequests END DESC,
				CASE WHEN @SortColumn = 'NumberResults' AND @SortDirection = 'asc' THEN NumberOfSuccessfulResults END ASC,
				CASE WHEN @SortColumn = 'NumberResults' AND @SortDirection = 'desc' THEN NumberOfSuccessfulResults END DESC,
				CASE WHEN @SortColumn = 'NumberPartials' AND @SortDirection = 'asc' THEN NumberOfPartialSuccesses END ASC,
				CASE WHEN @SortColumn = 'NumberPartials' AND @SortDirection = 'desc' THEN NumberOfPartialSuccesses END DESC,
				CASE WHEN @SortColumn = 'ValidationErrors' AND @SortDirection = 'asc' THEN NumberOfInvalidRequests END ASC,
				CASE WHEN @SortColumn = 'ValidationErrors' AND @SortDirection = 'desc' THEN NumberOfInvalidRequests END DESC,
				CASE WHEN @SortColumn = 'NoResults' AND @SortDirection = 'asc' THEN NumberOfUnsuccessfulRequests END ASC,
				CASE WHEN @SortColumn = 'NoResults' AND @SortDirection = 'desc' THEN NumberOfUnsuccessfulRequests END DESC,
				CASE WHEN @SortColumn = 'DateComplete' AND @SortDirection = 'asc' THEN CompletedDateTime END ASC,
				CASE WHEN @SortColumn = 'DateComplete' AND @SortDirection = 'desc' THEN CompletedDateTime END DESC,
				CASE WHEN @SortColumn = 'Status' AND @SortDirection = 'asc' THEN BatchStatusDescription END ASC,
				CASE WHEN @SortColumn = 'Status' AND @SortDirection = 'desc' THEN BatchStatusDescription END DESC)
		AS RowNumber
		,[PercentageComplete]
		,[QueuedPosition]
	FROM [BatchJourneyPlanner].[dbo].[BatchRequestSummary] brs
	INNER JOIN [BatchJourneyPlanner].[dbo].[BatchStatus] bs
	ON brs.BatchStatusId = bs.BatchStatusId
	WHERE UserId = @UserId)

	SELECT * FROM Results
	WHERE RowNumber >= @StartRow
	AND RowNumber <= @EndRow
	ORDER BY RowNumber ASC
	
	RETURN @Total	
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBatchRequestDetailsForProcessing]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE [dbo].[GetBatchRequestDetailsForProcessing]
END
GO
-- GetBatchRequestDetailsForProcessing proc
CREATE PROCEDURE [dbo].[GetBatchRequestDetailsForProcessing]
(
	@ProcessorId nvarchar(50),
	@NumberRequests int,
	@BatchMaxLineLimit int,
	@BatchMinLineLimit int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Check for a batch this processor is already doing
	DECLARE @BatchId int
	DECLARE @Date datetime
	SET @Date = GETDATE()
	
	IF (NOT EXISTS (SELECT BatchId FROM BatchRequestSummary WHERE ProcessorId = @ProcessorId))
	BEGIN
		-- Get a new batch if there is one available
		IF (EXISTS (SELECT BatchId FROM BatchRequestSummary WHERE ProcessorId IS NULL AND BatchStatusId = 1 AND NumberOfRequests <= @BatchMaxLineLimit AND NumberOfRequests >= @BatchMinLineLimit))
		BEGIN
			SELECT @BatchId = BatchId 
				FROM BatchRequestSummary 
				WHERE ProcessorId IS NULL 
				AND BatchStatusId = 1
				AND NumberOfRequests <= @BatchMaxLineLimit
				AND NumberOfRequests >= @BatchMinLineLimit
				ORDER BY QueuedDateTime DESC
				
			UPDATE BatchRequestSummary
				SET ProcessorId = @ProcessorId,
				BatchStatusId = 2,
				PercentageComplete = 0
				WHERE BatchId = @BatchId
		END
		ELSE
		BEGIN
			-- No requests available
			RETURN 0
		END
	END
	ELSE
	BEGIN
		SELECT @BatchId = BatchId
		FROM BatchRequestSummary
		WHERE ProcessorId = @ProcessorId
	END

	-- See if the batch is complete
	DECLARE @Count int
	SELECT @Count = COUNT(*)
	FROM BatchRequestDetail
	WHERE (BatchDetailStatusId IS NULL -- Pending
	OR BatchDetailStatusId = 1) -- in progress
	AND BatchId = @BatchId
		
	IF (@Count = 0)
	BEGIN
		-- Allow service to generate zip
		RETURN @BatchId
	END
	ELSE
	BEGIN
		-- Check if batch has stalled
		SELECT @Count = COUNT(*)
		FROM BatchRequestDetail
		WHERE BatchDetailStatusId IS NULL
		AND BatchId = @BatchId
		
		IF (@Count = 0)
		BEGIN
			-- resset the stalled records
			UPDATE BatchRequestDetail
			SET BatchDetailStatusId = NULL
			WHERE BatchDetailStatusId = 1
			AND BatchId = @BatchId
		END
	END

	-- Get detail records and preferences
	SELECT TOP (@NumberRequests) 
		bd.RequestId,									--0
		bs.[ReportParameters.IncludeJourneyStatistics],	--1
		bs.[ReportParameters.IncludeJourneyDetails],	--2
		bs.[ReportParameters.IncludePublicTransport],	--3
		bs.[ReportParameters.IncludeCar],				--4
		bs.[ReportParameters.IncludeCycle],				--5
		bs.[ReportParameters.ConvertToRtf],				--6
		bd.UserSuppliedUniqueId,						--7
		bd.[JourneyParameters.OriginType],				--8
		bd.[JourneyParameters.Origin],					--9
		bd.[JourneyParameters.DestinationType],			--10
		bd.[JourneyParameters.Destination],				--11
		bd.[JourneyParameters.OutwardDate],				--12
		bd.[JourneyParameters.OutwardTime],				--13
		bd.[JourneyParameters.OutwardArrDep],			--14
		bd.[JourneyParameters.ReturnDate],				--15
		bd.[JourneyParameters.ReturnTime],				--16
		bd.[JourneyParameters.ReturnArrDep]				--17
	FROM BatchRequestSummary bs
	INNER JOIN BatchRequestDetail bd
		ON bs.BatchId = bd.BatchId
	WHERE bd.BatchId = @BatchId
	AND bd.BatchDetailStatusId IS NULL -- Pending
	ORDER BY bd.RequestId ASC
	
	-- Update the status and submitted date
	UPDATE BatchRequestDetail
		SET BatchDetailStatusId = 1, -- Submitted
			SubmittedDateTime = @Date
	WHERE RequestId IN (SELECT TOP (@NumberRequests) RequestId
						FROM BatchRequestDetail bd
						WHERE bd.BatchId = @BatchId
						AND bd.BatchDetailStatusId IS NULL
						ORDER BY bd.RequestId ASC)
	
	RETURN 0
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetZip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetZip]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


-- GetZip proc
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetZip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetZip]
GO

CREATE PROCEDURE [dbo].[GetZip]
(
	@BatchId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT TOP 1 ZipDownload FROM BatchRequestSummary WHERE BatchId = @BatchId
END
GO

-- SetZip proc
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SetZip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SetZip]
GO

CREATE PROCEDURE [dbo].[SetZip]
(
	@BatchId int,
	@ZipFile varbinary(max)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE BatchRequestSummary
	SET ZipDownload = @ZipFile	
	WHERE BatchId = @BatchId
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserUploadLimit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetUserUploadLimit]
GO

CREATE PROCEDURE dbo.GetUserUploadLimit
	(@EmailAddress NVARCHAR(250))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT UploadLimit
	FROM RegisteredUser
	WHERE EmailAddress = @EmailAddress
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[AddBatchSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[AddBatchSummary]
GO

CREATE PROCEDURE dbo.AddBatchSummary
	(@EmailAddress NVARCHAR(250))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @UserId INT
	SELECT @UserId = UserId
	FROM RegisteredUser
	WHERE EmailAddress = @EmailAddress
	
	DECLARE @QueuedPosition INT
	SELECT @QueuedPosition = COUNT(*)
	FROM BatchRequestSummary
	WHERE BatchStatusId = 1
	
	INSERT INTO BatchRequestSummary	(UserId, QueuedDateTime, BatchStatusId, ProcessorId, [ReportParameters.IncludeJourneyStatistics], [ReportParameters.IncludeJourneyDetails], [ReportParameters.IncludePublicTransport], [ReportParameters.IncludeCar], [ReportParameters.IncludeCycle], [ReportParameters.ConvertToRtf], QueuedPosition)
	VALUES (@UserId, GETDATE(), 1, 'Uploading', 0, 0, 0, 0, 0, 0, @QueuedPosition) 
	
	SELECT @@IDENTITY
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateBatchSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateBatchSummary]
GO

CREATE PROCEDURE dbo.UpdateBatchSummary
	(@BatchId int,
	@UploadedOK bit,
	@NumberofRequests int,
	@NumberInvalidRequests int,
	@IncStatistics bit,
	@IncDetails bit,
	@IncPT bit,
	@IncCar bit,
	@IncCycle bit,
	@ConvertToRtf bit)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE BatchRequestSummary
	SET BatchStatusId = CASE WHEN @UploadedOK = 0 THEN 4 ELSE 1 END,
	NumberOfRequests = @NumberofRequests,
	NumberOfInvalidRequests = @NumberInvalidRequests,
	[ReportParameters.IncludeJourneyStatistics] = @IncStatistics,
	[ReportParameters.IncludeJourneyDetails] = @IncDetails,
	[ReportParameters.IncludePublicTransport] = @IncPT,
	[ReportParameters.IncludeCar] = @IncCar,
	[ReportParameters.IncludeCycle] = @IncCycle,
	[ReportParameters.ConvertToRtf] = @ConvertToRtf,
	ProcessorId = null
	WHERE BatchId = @BatchId
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateQueuedAndPercentComplete]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateQueuedAndPercentComplete]
GO

CREATE PROCEDURE dbo.UpdateQueuedAndPercentComplete
AS
BEGIN
	-- Queued Position
	UPDATE BatchRequestSummary
	SET QueuedPosition = (SELECT COUNT(*)
		FROM BatchRequestSummary brs
		WHERE brs.BatchStatusId = 1
		AND brs.BatchId < brs1.BatchId)
	FROM BatchRequestSummary brs1
	WHERE brs1.BatchStatusId = 1
	
	-- Percent complete
	UPDATE BatchRequestSummary
	SET PercentageComplete = (SELECT FLOOR((100 * COUNT(*)) / brs.NumberOfRequests)
		FROM BatchRequestDetail brd
		WHERE brs.BatchId = brd.BatchId
		AND brd.BatchDetailStatusId IS NOT NULL
		AND brd.BatchDetailStatusId <> 1)
	FROM BatchRequestSummary brs
	WHERE brs.BatchStatusId = 2
END 
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FinishBatch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[FinishBatch]
GO

CREATE PROC FinishBatch
(@BatchId INT)
AS
BEGIN
	-- Finish off this batch and update the summary record
	DECLARE @Date datetime
	SET @Date = GETDATE()
	
	UPDATE BatchRequestSummary
		SET CompletedDateTime = @Date,
		NumberOfSuccessfulResults = (SELECT COUNT(*) FROM BatchRequestDetail WHERE BatchDetailStatusId = 2 AND BatchId = @BatchId),
		NumberOfUnsuccessfulRequests = (SELECT COUNT(*) FROM BatchRequestDetail WHERE BatchDetailStatusId = 3 AND BatchId = @BatchId),
		NumberOfPartialSuccesses = (SELECT COUNT(*) FROM BatchRequestDetail WHERE BatchDetailStatusId = 5 AND BatchId = @BatchId),
		BatchStatusId = 3,
		ProcessorId = null
	WHERE BatchId = @BatchId
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBatchSummary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetBatchSummary]
GO

-- GetBatchSummary proc
CREATE PROCEDURE [dbo].[GetBatchSummary]
(
	@BatchId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- This proc is called when completing a batch by creating a zip so some returns are artificial
	SELECT DetailsDeletedDateTime,
		BatchStatusId,
		[ReportParameters.IncludeJourneyStatistics],
		[ReportParameters.IncludeJourneyDetails],
		[ReportParameters.IncludePublicTransport],
		[ReportParameters.IncludeCar],
		[ReportParameters.IncludeCycle],
		[ReportParameters.ConvertToRtf]
		,QueuedDateTime
		,GETDATE() AS CompletedDateTime
		,NumberOfRequests
		,(SELECT COUNT(*) FROM BatchRequestDetail WHERE BatchDetailStatusId = 2 AND BatchId = @BatchId) AS NumberOfSuccessfulResults
		,(SELECT COUNT(*) FROM BatchRequestDetail WHERE BatchDetailStatusId = 3 AND BatchId = @BatchId) AS NumberOfUnsuccessfulRequests
		,NumberOfInvalidRequests
		,dbo.PadBatchId(BatchId)
		,(SELECT COUNT(*) FROM BatchRequestDetail WHERE BatchDetailStatusId = 5 AND BatchId = @BatchId) AS NumberOfPartialSuccesses
	FROM BatchRequestSummary
	WHERE BatchId = @BatchId;
END
GO

-- Content
USE Content

EXEC AddtblContent
	1, 1, 'langStrings', 'BatchJourneyPlanner.MaxUserFileLength',
	'The file you attempted to upload exceeded your maximum upload file length of {0} rows',
	'The file you attempted to upload exceeded your maximum upload file length of {0} rows'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'BatchJourneyPlanner.MaxDefaultFileLength',
	'The file you attempted to upload exceeded the default maximum upload file length of {0} rows',
	'The file you attempted to upload exceeded the default maximum upload file length of {0} rows'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'BatchJourneyPlanner.MaxJourneyDetailsFileLength',
	'The file you attempted to upload exceeded the maximum upload file length (when requesting journey detail files) of {0} rows',
	'The file you attempted to upload exceeded the maximum upload file length (when requesting journey detail files) of {0} rows'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'BatchJourneyPlanner.MaxNoJourneyDetailsFileLength',
	'The file you attempted to upload exceeded the maximum upload file length of {0} rows',
	'The file you attempted to upload exceeded the maximum upload file length of {0} rows'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'BatchJourneyPlanner.PercentageComplete',
	'Batch {0} is currently {1}% complete',
	'Batch {0} is currently {1}% complete'
	
EXEC AddtblContent
	1, 1, 'langStrings', 'BatchJourneyPlanner.QueuedPosition',
	'Batch {0} is {1} in the queue to be processed',
	'Batch {0} is {1} in the queue to be processed'

EXEC AddtblContent
	1, 1, 'langStrings', 'BatchJourneyPlanner.Uploading',
	'Uploading...',
	'Uploading...'

-- Create the status update job
USE [msdb]
GO

BEGIN TRANSACTION
DECLARE @ReturnCode INT
SELECT @ReturnCode = 0

IF NOT EXISTS (SELECT name FROM msdb.dbo.syscategories WHERE name=N'[Uncategorized (Local)]' AND category_class=1)
BEGIN
EXEC @ReturnCode = msdb.dbo.sp_add_category @class=N'JOB', @type=N'LOCAL', @name=N'[Uncategorized (Local)]'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback

END

DECLARE @jobId BINARY(16)
EXEC @ReturnCode =  msdb.dbo.sp_add_job @job_name=N'BatchStatusUpdates', 
		@enabled=1, 
		@notify_level_eventlog=0, 
		@notify_level_email=0, 
		@notify_level_netsend=0, 
		@notify_level_page=0, 
		@delete_level=0, 
		@description=N'Update the queued and in progress batches with queue position and percentage complete', 
		@category_name=N'[Uncategorized (Local)]', 
		@owner_login_name=N'NFHTDSUPPORT\dlane', @job_id = @jobId OUTPUT
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
/****** Object:  Step [Run sproc]    Script Date: 03/21/2013 11:33:17 ******/
EXEC @ReturnCode = msdb.dbo.sp_add_jobstep @job_id=@jobId, @step_name=N'Run sproc', 
		@step_id=1, 
		@cmdexec_success_code=0, 
		@on_success_action=1, 
		@on_success_step_id=0, 
		@on_fail_action=2, 
		@on_fail_step_id=0, 
		@retry_attempts=0, 
		@retry_interval=0, 
		@os_run_priority=0, @subsystem=N'TSQL', 
		@command=N'EXEC dbo.UpdateQueuedAndPercentComplete', 
		@database_name=N'BatchJourneyPlanner', 
		@flags=0
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_update_job @job_id = @jobId, @start_step_id = 1
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobschedule @job_id=@jobId, @name=N'BatchStatusUpdatesSchedule', 
		@enabled=1, 
		@freq_type=4, 
		@freq_interval=1, 
		@freq_subday_type=4, 
		@freq_subday_interval=5, 
		@freq_relative_interval=0, 
		@freq_recurrence_factor=0, 
		@active_start_date=20130319, 
		@active_end_date=99991231, 
		@active_start_time=0, 
		@active_end_time=235959, 
		@schedule_uid=N'cc4cbd5c-2c8a-4456-bf60-65533448f438'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
EXEC @ReturnCode = msdb.dbo.sp_add_jobserver @job_id = @jobId, @server_name = N'(local)'
IF (@@ERROR <> 0 OR @ReturnCode <> 0) GOTO QuitWithRollback
COMMIT TRANSACTION
GOTO EndSave
QuitWithRollback:
    IF (@@TRANCOUNT > 0) ROLLBACK TRANSACTION
EndSave:

GO

-- Properties
USE PermanentPortal

DECLARE @AID NVARCHAR(50)
DECLARE @GID NVARCHAR(50)

-- Service properties
SET @AID = 'BatchJourneyPlannerService'
SET @GID = 'UserPortal'

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchLandingPage' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchLandingPage', 'http://www.transportdirect.info/web2/JourneyPlanning/JPLandingPage.aspx', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = 'http://www.transportdirect.info/web2/JourneyPlanning/JPLandingPage.aspx'
	WHERE pName = 'BatchLandingPage' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerDBLongTimeout' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerDBLongTimeout', 'Server=.;Initial Catalog=BatchJourneyPlanner;Trusted_Connection=true;Connect Timeout=600;Packet Size=4096;', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = 'Server=.;Initial Catalog=BatchJourneyPlanner;Trusted_Connection=true;Connect Timeout=600;Packet Size=4096;'
	WHERE pName = 'BatchJourneyPlannerDBLongTimeout' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerTestMode' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerTestMode', 'false', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = 'true'
	WHERE pName = 'BatchJourneyPlannerTestMode' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchMaxLinesWithOutputImage' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchMaxLinesWithOutputImage', '500', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '500'
	WHERE pName = 'BatchMaxLinesWithOutputImage' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerFileStorage' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerFileStorage', 'D:\Temp\_BatchDownloads', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = 'D:\Temp\_BatchDownloads'
	WHERE pName = 'BatchJourneyPlannerFileStorage' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

-- transform move
IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerJourneyTransform' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerJourneyTransform', '\RtfJourneyTransform.xslt', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '\RtfJourneyTransform.xslt'
	WHERE pName = 'BatchJourneyPlannerJourneyTransform' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerSummaryTransform' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerSummaryTransform', '\RtfSummaryTransform.xslt', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '\RtfSummaryTransform.xslt'
	WHERE pName = 'BatchJourneyPlannerSummaryTransform' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END


-- website properties
SET @AID = 'Web'

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerDefaultMaxLines' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerDefaultMaxLines', '500', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '500'
	WHERE pName = 'BatchJourneyPlannerDefaultMaxLines' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerJourneyDetailsMaxLines' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerJourneyDetailsMaxLines', '10000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '10000'
	WHERE pName = 'BatchJourneyPlannerJourneyDetailsMaxLines' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerNoJourneyDetailsMaxLines' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerNoJourneyDetailsMaxLines', '50000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '50000'
	WHERE pName = 'BatchJourneyPlannerNoJourneyDetailsMaxLines' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerMaxEasting' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerMaxEasting', '800000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '800000'
	WHERE pName = 'BatchJourneyPlannerMaxEasting' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchJourneyPlannerMaxNorthing' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchJourneyPlannerMaxNorthing', '1350000', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '1350000'
	WHERE pName = 'BatchJourneyPlannerMaxNorthing' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END

IF NOT EXISTS (SELECT * FROM properties WHERE pName = 'BatchMaxLinesWithOutputImage' 	
	AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1)
BEGIN
	INSERT INTO properties VALUES ('BatchMaxLinesWithOutputImage', '500', @AID, @GID, 0, 1)
END
ELSE
BEGIN
	UPDATE properties SET pValue = '500'
	WHERE pName = 'BatchMaxLinesWithOutputImage' AND AID = @AID AND GID = @GID AND PartnerId = 0 AND ThemeId = 1
END


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2016
SET @ScriptDesc = 'CCN 0648c - Super batch'

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
-------------------------------------------
