-- ***********************************************
-- NAME           : DUP2022_BatchJourneyPlanner_StoredProc_Update.sql
-- DESCRIPTION    : Script to update GetBatchResults stored proc to sort by batch request id
-- AUTHOR         : Mitesh Modi
-- DATE           : 17 Apr 2013
-- ***********************************************

USE [BatchJourneyPlanner]
GO

-- Create stored proc if needed (it should exist)
if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetBatchResults]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE GetBatchResults
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- GetBatchResults proc
ALTER PROCEDURE [dbo].[GetBatchResults]
(
	@BatchId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Output the results
	SELECT [UserSuppliedUniqueId]			-- 0
		,[ErrorMessages]					-- 1
		,[PublicJourneyResultSummary]		-- 2
		,[PublicJourneyResultDetails]		-- 3
		,[CarJourneyResultSummary]			-- 4
		,[CarJourneyResultDetails]			-- 5
		,[CycleJourneyResultSummary]		-- 6
		,[CycleJourneyResultDetails]		-- 7
		,[PublicOutwardJourneyStatus]		-- 8
		,[PublicReturnJourneyStatus]		-- 9
		,[CarOutwardJourneyStatus]			-- 10
		,[CarReturnJourneyStatus]			-- 11
		,[CycleOutwardJourneyStatus]		-- 12
		,[CycleReturnJourneyStatus]			-- 13
		,[JourneyParameters.Origin]			-- 14
		,[JourneyParameters.Destination]	-- 15
		,[JourneyParameters.OutwardDate]	-- 16
		,[JourneyParameters.OutwardTime]	-- 17
		,[JourneyParameters.OutwardArrDep]  -- 18
		,[JourneyParameters.ReturnDate]		-- 19
		,[JourneyParameters.ReturnTime]		-- 20
		,[JourneyParameters.ReturnArrDep]	-- 21
		,[BatchDetailStatusId]				-- 22
		,[JourneyParameters.OriginType]		-- 23
		,[JourneyParameters.DestinationType]-- 24
		,[PublicOutwardErrorMessage]		-- 25
		,[CycleOutwardErrorMessage]			-- 26
		,[CarOutwardErrorMessage]			-- 27
		,[PublicReturnErrorMessage]			-- 28
		,[CycleReturnErrorMessage]			-- 29
		,[CarReturnErrorMessage]			-- 30
	FROM [BatchJourneyPlanner].[dbo].[BatchRequestDetail]
	WHERE BatchId = @BatchId
	ORDER BY RequestId ASC
	
END
GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2022
SET @ScriptDesc = 'Script to update GetBatchResults stored proc to sort by batch request id'

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