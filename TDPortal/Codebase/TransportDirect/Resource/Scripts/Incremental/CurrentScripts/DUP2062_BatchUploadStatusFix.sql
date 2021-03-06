
-- ***********************************************
-- NAME 		: DUP2062_BatchUploadStatusFix.sql
-- DESCRIPTION 		: Fix batches stuck in upload state
-- AUTHOR		: Phil Scott
-- DATE			: 22 July 2013
-- ************************************************



USE [BatchJourneyPlanner]
GO
/****** Object:  StoredProcedure [dbo].[UpdateQueuedAndPercentComplete]    Script Date: 07/22/2013 10:45:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[UpdateQueuedAndPercentComplete]
AS
BEGIN
	-- Queued Position
	UPDATE BatchRequestSummary
	SET QueuedPosition = (1 + (SELECT COUNT(*)
		FROM BatchRequestSummary brs
		WHERE brs.BatchStatusId = 1
		AND brs.BatchId < brs1.BatchId))
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
	
	-- Tidy any batches stuck uploading
	UPDATE  s
	SET s.BatchStatusId  = 4,
        s.ProcessorId = null,
        s.CompletedDateTime = GETDATE()
	FROM [BatchJourneyPlanner].[dbo].[BatchRequestSummary] S
    WHERE s.CompletedDateTime is null
	and s.ProcessorId = 'Uploading'
	and DATEDIFF(MINUTE,s.QueuedDateTime,GETDATE()) > 30
	and (SELECT COUNT(*) 
			FROM [BatchJourneyPlanner].[dbo].[BatchRequestDetail] d
			WHERE d.BatchId = s.BatchId) < 1

		
END 
GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2062
SET @ScriptDesc = 'DUP2062_BatchUploadStatusFix.sql'

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
