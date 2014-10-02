-- ***********************************************
-- NAME 		: DUP2018_BatchQueuedPosition.sql
-- DESCRIPTION 	: Fix batch queued position
-- AUTHOR		: David Lane
-- DATE			: 2 April 2013
-- ************************************************

USE BatchJourneyPlanner
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
END 
GO

ALTER PROCEDURE [dbo].[AddBatchSummary]
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
	SELECT @QueuedPosition = 1 + COUNT(*)
	FROM BatchRequestSummary
	WHERE BatchStatusId = 1
	
	INSERT INTO BatchRequestSummary	(UserId, QueuedDateTime, BatchStatusId, ProcessorId, [ReportParameters.IncludeJourneyStatistics], [ReportParameters.IncludeJourneyDetails], [ReportParameters.IncludePublicTransport], [ReportParameters.IncludeCar], [ReportParameters.IncludeCycle], [ReportParameters.ConvertToRtf], QueuedPosition)
	VALUES (@UserId, GETDATE(), 1, 'Uploading', 0, 0, 0, 0, 0, 0, @QueuedPosition) 
	
	SELECT @@IDENTITY
END
GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2018
SET @ScriptDesc = 'CCN 0648c - Batch queued position'

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
