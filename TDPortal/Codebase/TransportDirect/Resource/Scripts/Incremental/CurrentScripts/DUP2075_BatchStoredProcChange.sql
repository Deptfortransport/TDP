-- ***********************************************
-- NAME           : DUP2075_BatchStoredProcChange.sql
-- DESCRIPTION    : Script to update stored proc for batch to avoid truncation of landing page URL (PT journey summary)
-- AUTHOR         : David Lane
-- DATE           : 29 Aug 2013
-- ***********************************************

USE [BatchJourneyPlanner]
GO
/****** Object:  StoredProcedure [dbo].[UpdateBatchRequestDetail]    Script Date: 08/28/2013 17:56:29 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER OFF
GO

ALTER PROCEDURE [dbo].[UpdateBatchRequestDetail]
(
	@BatchDetailId int,
	@PublicJourneyResultSummary nvarchar(400),
	@PublicJourneyResultDetails xml,
	@PublicOutwardJourneyStatus int,
	@PublicReturnJourneyStatus int,
	@CarResultSummary nvarchar(250),
	@CarOutwardStatus int,
	@CarReturnStatus int,
	@CycleResultSummary nvarchar(400),
	@CycleOutwardStatus int,
	@CycleReturnStatus int,
	@PublicOutwardErrorMessage nvarchar(200),
	@CarOutwardErrorMessage nvarchar(200),
	@CycleOutwardErrorMessage nvarchar(200),
	@PublicReturnErrorMessage nvarchar(200),
	@CarReturnErrorMessage nvarchar(200),
	@CycleReturnErrorMessage nvarchar(200)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Update the detail record
	DECLARE @Date datetime
	SET @Date = GETDATE()
	
	DECLARE @DetailStatusId int
	SET @DetailStatusId = 2 -- complete
	DECLARE @ErrorMessages varchar
	declare @invalidcount as int
	set @invalidcount = 0
	IF ((@PublicOutwardJourneyStatus > 2) OR (@CarOutwardStatus > 2) OR (@CycleOutwardStatus > 2) OR (@PublicReturnJourneyStatus > 2) OR (@CarReturnStatus > 2) OR (@CycleReturnStatus > 2))
	BEGIN
		SET @DetailStatusId = 3 -- errored
	END
	
	IF ((@PublicOutwardJourneyStatus = 4) OR (@CarOutwardStatus = 4) OR (@CycleOutwardStatus = 4 ) OR (@PublicReturnJourneyStatus = 4) OR (@CarReturnStatus = 4) OR (@CycleReturnStatus = 4))
	BEGIN
		SET @DetailStatusId = 4 -- Validation Error
		-- This bit Service side Pre-Validation
		IF ((@PublicOutwardJourneyStatus = 4) and (@CarOutwardStatus = 4) and (@CycleOutwardStatus = 4 ) and (@PublicReturnJourneyStatus = 4) and (@CarReturnStatus = 4) and (@CycleReturnStatus = 4))
		BEGIN
			UPDATE BatchRequestDetail
			SET [ErrorMessages] = @PublicOutwardErrorMessage
			WHERE RequestId = @BatchDetailId
		END
	END

	IF (@DetailStatusId = 3)
	BEGIN
		IF ((@PublicOutwardJourneyStatus = 2) OR (@CycleOutwardStatus = 2) OR (@CarOutwardStatus = 2) OR (@PublicReturnJourneyStatus = 2) OR (@CarReturnStatus = 2) OR (@CycleReturnStatus = 2))
		BEGIN
			SET @DetailStatusId = 5 -- partial success
		END
	END	
	UPDATE BatchRequestDetail
	SET CompletedDateTime = @Date,
		PublicJourneyResultSummary = @PublicJourneyResultSummary,
		PublicJourneyResultDetails = @PublicJourneyResultDetails,
		PublicOutwardJourneyStatus = @PublicOutwardJourneyStatus,
		PublicReturnJourneyStatus = @PublicReturnJourneyStatus,
		CarJourneyResultSummary = @CarResultSummary,
		CarOutwardJourneyStatus = @CarOutwardStatus,
		CarReturnJourneyStatus = @CarReturnStatus,
		CycleJourneyResultSummary = @CycleResultSummary,
		CycleOutwardJourneyStatus = @CycleOutwardStatus,
		CycleReturnJourneyStatus = @CycleReturnStatus,
		BatchDetailStatusId = @DetailStatusId,
		PublicOutwardErrorMessage = @PublicOutwardErrorMessage,
		CarOutwardErrorMessage = @CarOutwardErrorMessage,
		CycleOutwardErrorMessage = @CycleOutwardErrorMessage,
		PublicReturnErrorMessage = @PublicReturnErrorMessage,
		CarReturnErrorMessage = @CarReturnErrorMessage,
		CycleReturnErrorMessage = @CycleReturnErrorMessage
	WHERE RequestId = @BatchDetailId
END
GO


----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2075
SET @ScriptDesc = 'Script to update stored proc for batch to avoid truncation of landing page URL (PT journey summary)'

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
