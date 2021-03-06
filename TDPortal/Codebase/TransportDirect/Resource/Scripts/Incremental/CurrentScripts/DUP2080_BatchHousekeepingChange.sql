-- ***********************************************
-- NAME           : DUP2080_BatchHousekeepingChange.sql
-- DESCRIPTION    : Script to update batch housekeeping 
-- AUTHOR         : David Lane
-- DATE           : 18 Sep 2013
-- ***********************************************


-- Add new property for inactivity window in batch
USE PermanentPortal
GO

IF EXISTS (SELECT 1 FROM properties WHERE pName = 'BatchInactivityErrorWindowDays')
BEGIN
	UPDATE properties SET pValue = '10' WHERE pName = 'BatchInactivityErrorWindowDays'
END
ELSE
BEGIN
	INSERT INTO properties
	VALUES ('BatchInactivityErrorWindowDays', '10', 'BatchJourneyPlannerService', 'UserPortal', 0, 1)
END
GO


IF EXISTS (SELECT 1 FROM properties WHERE pName = 'BatchNotStartedErrorWindowDays')
BEGIN
	UPDATE properties SET pValue = '90' WHERE pName = 'BatchNotStartedErrorWindowDays'
END
ELSE
BEGIN
	INSERT INTO properties
	VALUES ('BatchNotStartedErrorWindowDays', '90', 'BatchJourneyPlannerService', 'UserPortal', 0, 1)
END
GO


USE [BatchJourneyPlanner]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[DeleteBatchDetails]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Get the expiry age
	DECLARE @Days INT
	SELECT @Days = pValue
	FROM PermanentPortal..properties
	WHERE pName = 'BatchProcessingOldResultExpiryDays'
	
	-- Remove old completed batches
	DECLARE @Date datetime
	SELECT @Date = DATEADD(DAY, -@Days, GETDATE())
	 
	DELETE FROM BatchRequestDetail
	WHERE BatchId IN (SELECT BatchId
						FROM BatchRequestSummary
						WHERE CompletedDateTime < @Date)
						
	DELETE FROM BatchRequestSummary
	WHERE CompletedDateTime < @Date

	-- Set to errored batches that have started but shown no activity in the last 'n' days
	SELECT @Days = pValue
	FROM PermanentPortal..properties
	WHERE pName = 'BatchInactivityErrorWindowDays'
	
	SELECT @Date = DATEADD(DAY, -@Days, GETDATE())
	
	DECLARE @CompleteDate datetime
	SELECT @CompleteDate = GETDATE()
	
	-- Pick up in progress batches with no activity
	UPDATE BatchRequestSummary
	SET BatchStatusId = 4, -- errored
	CompletedDateTime = @CompleteDate
	WHERE BatchId IN (SELECT s.BatchId
						FROM BatchRequestSummary s
						INNER JOIN BatchRequestDetail d
						ON s.BatchId = d.BatchId
						WHERE s.StartDateTime IS NOT NULL
						AND s.CompletedDateTime IS NULL
						GROUP BY s.BatchId
						HAVING MAX(d.CompletedDateTime) < @Date)

	-- Set to errored if the batch has not started for 'n' days
	SELECT @Days = pValue
	FROM PermanentPortal..properties
	WHERE pName = 'BatchNotStartedErrorWindowDays'
	
	SELECT @Date = DATEADD(DAY, -@Days, GETDATE())
	
	UPDATE BatchRequestSummary
	SET BatchStatusId = 4, -- errored
	CompletedDateTime = @CompleteDate,
	StartDateTime = @CompleteDate
	WHERE QueuedDateTime < @Date
	AND StartDateTime IS NULL
END
GO

-- ensure housekeeping is turned on
EXEC msdb..sp_update_job @job_name = 'BatchHousekeeping', @enabled = 1
GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2080
SET @ScriptDesc = 'Script to update batch housekeeping'

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
