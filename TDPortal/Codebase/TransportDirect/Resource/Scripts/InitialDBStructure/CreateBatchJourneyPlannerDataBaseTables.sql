USE [BatchJourneyPlanner]
GO

---------
-- TABLES
---------

/****** Object:  Table [dbo].[UserStatus]    Script Date: 01/13/2012 09:30:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserStatus](
	[UserStatusId] [int] NOT NULL,
	[UserStatusDescription] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserStatus] PRIMARY KEY CLUSTERED 
(
	[UserStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[RegisteredUser]    Script Date: 01/12/2012 16:00:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[RegisteredUser](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [nvarchar](250) NOT NULL,
	[UserStatusId] [int] NOT NULL,
	[IncludeStatistics] [bit] NOT NULL,
	[IncludeDetails] [bit] NOT NULL,
	[PublicTransport] [bit] NOT NULL,
	[Car] [bit] NOT NULL,
	[Cycle] [bit] NOT NULL,
	[ResultsAsRtf] [bit] NOT NULL,
	[StatusChanged] [datetime],
 CONSTRAINT [PK_RegisteredUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[RegisteredUser] ADD  CONSTRAINT [DF_RegisteredUser_UserStatusId]  DEFAULT ((0)) FOR [UserStatusId]
GO

ALTER TABLE [dbo].[RegisteredUser] ADD  CONSTRAINT [DF_RegisteredUser_IncludeStatistics]  DEFAULT ((0)) FOR [IncludeStatistics]
GO

ALTER TABLE [dbo].[RegisteredUser] ADD  CONSTRAINT [DF_RegisteredUser_IncludeDetails]  DEFAULT ((0)) FOR [IncludeDetails]
GO

ALTER TABLE [dbo].[RegisteredUser] ADD  CONSTRAINT [DF_RegisteredUser_PublicTransport]  DEFAULT ((0)) FOR [PublicTransport]
GO

ALTER TABLE [dbo].[RegisteredUser] ADD  CONSTRAINT [DF_RegisteredUser_Car]  DEFAULT ((0)) FOR [Car]
GO

ALTER TABLE [dbo].[RegisteredUser] ADD  CONSTRAINT [DF_RegisteredUser_Cycle]  DEFAULT ((0)) FOR [Cycle]
GO

ALTER TABLE [dbo].[RegisteredUser] ADD  CONSTRAINT [DF_RegisteredUser_ResultsAsRtf]  DEFAULT ((0)) FOR [ResultsAsRtf]
GO

ALTER TABLE [dbo].[RegisteredUser] WITH CHECK ADD CONSTRAINT [FK_RegisteredUser_UserStatus] FOREIGN KEY([UserStatusId])
REFERENCES [dbo].[UserStatus] ([UserStatusId])
GO

ALTER TABLE [dbo].[RegisteredUser] CHECK CONSTRAINT [FK_RegisteredUser_UserStatus] 
GO


/****** Object:  Table [dbo].[BatchStatus]    Script Date: 01/13/2012 09:34:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BatchStatus](
	[BatchStatusId] [int] NOT NULL,
	[BatchStatusDescription] [nvarchar](50) NOT NULL
 CONSTRAINT [PK_BatchStatus] PRIMARY KEY CLUSTERED 
(
	[BatchStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
ON [PRIMARY])

GO



/****** Object:  Table [dbo].[BatchRequestSummary]    Script Date: 01/12/2012 16:00:43 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BatchRequestSummary](
	[BatchId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[QueuedDateTime] [datetime] NOT NULL,
	[CompletedDateTime] [datetime] NULL,
	[DetailsDeletedDateTime] [datetime] NULL,
	[LastResultDownloadRequestDateTime] [datetime] NULL,
	[NumberOfRequests] [int] NULL,
	[NumberOfSuccessfulResults] [int] NULL,
	[NumberOfInvalidRequests] [int] NULL,
	[NumberOfUnsuccessfulRequests] [int] NULL,
	[NumberOfResultsDownloadAttempts] [int] NULL,
	[BatchStatusId] [int] NOT NULL,
	[ProcessorId] [nvarchar](50) NULL,
	[ReportParameters.IncludeJourneyStatistics] [bit] NOT NULL,
	[ReportParameters.IncludeJourneyDetails] [bit] NOT NULL,
	[ReportParameters.IncludePublicTransport] [bit] NOT NULL,
	[ReportParameters.IncludeCar] [bit] NOT NULL,
	[ReportParameters.IncludeCycle] [bit] NOT NULL,
	[ReportParameters.ConvertToRtf] [bit] NOT NULL,
 CONSTRAINT [PK_BatchRequestSummary] PRIMARY KEY CLUSTERED 
(
	[BatchId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
ON [PRIMARY])


GO

ALTER TABLE [dbo].[BatchRequestSummary]  WITH CHECK ADD  CONSTRAINT [FK_BatchRequestSummary_RegisteredUser] FOREIGN KEY([UserId])
REFERENCES [dbo].[RegisteredUser] ([UserId])
GO

ALTER TABLE [dbo].[BatchRequestSummary] CHECK CONSTRAINT [FK_BatchRequestSummary_RegisteredUser]
GO

ALTER TABLE [dbo].[BatchRequestSummary]  WITH CHECK ADD  CONSTRAINT [FK_BatchRequestSummary_BatchStatus] FOREIGN KEY([BatchStatusId])
REFERENCES [dbo].[BatchStatus] ([BatchStatusId])
GO

ALTER TABLE [dbo].[BatchRequestSummary] CHECK CONSTRAINT [FK_BatchRequestSummary_BatchStatus] 
GO


/****** Object:  Table [dbo].[BatchDetailStatus]    Script Date: 01/13/2012 09:37:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[BatchDetailStatus](
	[BatchDetailStatusId] [int] NOT NULL,
	[BatchDetailStatusDescription] [nvarchar](50) NOT NULL
 CONSTRAINT [PK_BatchDetailStatus] PRIMARY KEY CLUSTERED 
(
	[BatchDetailStatusId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) 
ON [PRIMARY])

GO


/****** Object:  Table [dbo].[BatchRequestDetail]    Script Date: 01/12/2012 16:01:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[BatchRequestDetail](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[BatchId] [int] NOT NULL,
	[BatchDetailStatusId] [int] NULL,
	[UserSuppliedUniqueId] [nvarchar](100) NULL,
	[JourneyParameters.OriginType] [char](1) NULL,
	[JourneyParameters.Origin] [nvarchar](100) NULL,
	[JourneyParameters.DestinationType] [char](1) NULL,
	[JourneyParameters.Destination] [nvarchar](100) NULL,
	[JourneyParameters.OutwardDate] [date] NULL,
	[JourneyParameters.OutwardTime] [time](4) NULL,
	[JourneyParameters.OutwardArrDep] [char](1) NULL,
	[JourneyParameters.ReturnDate] [date] NULL,
	[JourneyParameters.ReturnTime] [time](4) NULL,
	[JourneyParameters.ReturnArrDep] [char](1) NULL,
	[ErrorMessages] [nvarchar](700) NULL,
	[PublicJourneyResultSummary] [nvarchar](250) NULL,
	[PublicJourneyResultDetails] [xml] NULL,
	[CarJourneyResultSummary] [nvarchar](250) NULL,
	[CarJourneyResultDetails] [xml] NULL,
	[CycleJourneyResultSummary] [nvarchar](250) NULL,
	[CycleJourneyResultDetails] [xml] NULL,
	[QueuedDateTime] [datetime] NULL,
	[SubmittedDateTime] [datetime] NULL,
	[CompletedDateTime] [datetime] NULL,
	[PublicOutwardJourneyStatus] [int] NULL,
	[CarOutwardJourneyStatus] [int] NULL,
	[CycleOutwardJourneyStatus] [int] NULL,
	[PublicReturnJourneyStatus] [int] NULL,
	[CarReturnJourneyStatus] [int] NULL,
	[CycleReturnJourneyStatus] [int] NULL,
 CONSTRAINT [PK_BatchRequestDetail] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[BatchRequestDetail]  WITH CHECK ADD  CONSTRAINT [FK_BatchRequestDetail_BatchRequestSummary] FOREIGN KEY([BatchId])
REFERENCES [dbo].[BatchRequestSummary] ([BatchId])
GO

ALTER TABLE [dbo].[BatchRequestDetail] CHECK CONSTRAINT [FK_BatchRequestDetail_BatchRequestSummary]
GO

ALTER TABLE [dbo].[BatchRequestDetail]  WITH CHECK ADD  CONSTRAINT [FK_BatchRequestDetail_BatchDetailsStatus] FOREIGN KEY([BatchDetailStatusId])
REFERENCES [dbo].[BatchDetailStatus] ([BatchDetailStatusId])
GO

ALTER TABLE [dbo].[BatchRequestDetail] CHECK CONSTRAINT [FK_BatchRequestDetail_BatchDetailsStatus] 
GO


--------------------
-- LOOKUP TABLE DATA
--------------------

INSERT INTO [dbo].[UserStatus]
VALUES (1, 'Pending'),
		(2, 'Active'),
		(3, 'Suspended')

GO

INSERT INTO [dbo].[BatchStatus]
VALUES (1, 'Queued'),
		(2, 'In Progress'),
		(3, 'Complete'),
		(4, 'Errored')

GO

INSERT INTO [dbo].[BatchDetailStatus]
VALUES (1, 'Submitted'),
		(2, 'Complete'),
		(3, 'Errored'),
		(4, 'ValidationError')

GO

------------
-- FUNCTIONS
------------
CREATE FUNCTION PadBatchId (@BatchId nvarchar(20))
RETURNS nvarchar(20)
AS
BEGIN
	-- Pad the batch id up to 6 characters by prepending zeroes
	WHILE (LEN(@BatchId) < 6)
	BEGIN
		SELECT @BatchId = '0' + @BatchId
	END

	-- Return the result of the function
	RETURN @BatchId
END
GO

--------------------
-- STORED PROCEDURES
--------------------

CREATE PROCEDURE dbo.AddNewUser
(
	@EmailAddress nvarchar(250)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Create the user record
	INSERT INTO [BatchJourneyPlanner].[dbo].[RegisteredUser]
			   ([EmailAddress]
			   ,[UserStatusId]
			   ,[IncludeStatistics]
			   ,[IncludeDetails]
			   ,[PublicTransport]
			   ,[Car]
			   ,[Cycle]
			   ,[ResultsAsRtf]
			   ,[StatusChanged])
		 VALUES
			   (@EmailAddress
			   ,1
			   ,0
			   ,0
			   ,0
			   ,0
			   ,0
			   ,0
			   ,GETDATE())

    -- Return the user id
	SELECT @@IDENTITY
END
GO


CREATE PROCEDURE dbo.GetUserStatus
(
	@EmailAddress nvarchar(250)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DECLARE @return int
	
	-- Return the user's status, return "Unkown" if the user is not known to batch
	IF (EXISTS (SELECT * FROM RegisteredUser WHERE EmailAddress = @EmailAddress))
	BEGIN
		SELECT @return = UserStatusId
		FROM RegisteredUser
		WHERE EmailAddress = @EmailAddress
	END
	ELSE
		SELECT @return = 0

	RETURN @return
END
GO


CREATE PROCEDURE dbo.SetUserStatus
(
	@EmailAddress nvarchar(250),
	@StatusId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Change the user's status
	UPDATE RegisteredUser
		SET UserStatusId = @StatusId
		WHERE EmailAddress = @EmailAddress

	RETURN 0
END
GO


CREATE PROCEDURE dbo.AddBatchRequest
(
	@EmailAddress nvarchar(250),
	@IncludeJourneyStatistics bit,
	@IncludeJourneyDetails bit,
	@IncludePublicTransport bit,
	@IncludeCar bit,
	@IncludeCycle bit,
	@ConvertToRtf bit,
	@BatchDetails xml
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
	
	-- Add a record to batch request
	DECLARE @Date datetime
	SELECT @Date = GETDATE()
	
	INSERT INTO [BatchJourneyPlanner].[dbo].[BatchRequestSummary]
           ([UserId]
           ,[QueuedDateTime]
           ,[BatchStatusId]
           ,[ReportParameters.IncludeJourneyStatistics]
           ,[ReportParameters.IncludeJourneyDetails]
           ,[ReportParameters.IncludePublicTransport]
           ,[ReportParameters.IncludeCar]
           ,[ReportParameters.IncludeCycle]
           ,[ReportParameters.ConvertToRtf])
     VALUES
           (@UserId
           ,@Date
           ,1 -- Pending
           ,@IncludeJourneyStatistics
           ,@IncludeJourneyDetails
           ,@IncludePublicTransport
           ,@IncludeCar
           ,@IncludeCycle
           ,@ConvertToRtf)	
	
	DECLARE @BatchId int
	SELECT @BatchId = @@IDENTITY
	
	-- Add the batch detail records
	DECLARE @userSuppliedUniqueId nvarchar(100)
	DECLARE @originType char(1)
	DECLARE @origin nvarchar(100)
	DECLARE @destinationType char(1)
	DECLARE @destination nvarchar(100)
	DECLARE @outwardDate nvarchar(6)
	DECLARE @outwardTimeAsString nvarchar(4)
	DECLARE @outwardArrDep char(1)
	DECLARE @returnDate nvarchar(6)
	DECLARE @returnTimeAsString nvarchar(4)
	DECLARE @returnArrDep char(1)
	DECLARE @detailStatus char(1)
	
	DECLARE Cursor_Details CURSOR STATIC FORWARD_ONLY READ_ONLY FOR 
	SELECT   
		tab.col.value('UserSuppliedUniqueId[1]','NVARCHAR(100)'),
		tab.col.value('OriginType[1]','CHAR(1)'),
		tab.col.value('Origin[1]','NVARCHAR(100)'),
		tab.col.value('DestinationType[1]','CHAR(1)'),
		tab.col.value('Destination[1]','NVARCHAR(100)'),
		tab.col.value('OutwardDate[1]','NVARCHAR(6)'),
		tab.col.value('OutwardTime[1]','NVARCHAR(4)'),
		tab.col.value('OutwardArrDep[1]','CHAR(1)'),
		tab.col.value('ReturnDate[1]','NVARCHAR(6)'),
		tab.col.value('ReturnTime[1]','NVARCHAR(4)'),
		tab.col.value('ReturnArrDep[1]','CHAR(1)')
	FROM @BatchDetails.nodes('//DetailRecord') tab(col) 
	
	OPEN Cursor_Details
	FETCH NEXT FROM Cursor_Details INTO
		@userSuppliedUniqueId,
		@originType,
		@origin,
		@destinationType,
		@destination,
		@outwardDate,
		@outwardTimeAsString,
		@outwardArrDep,
		@returnDate,
		@returnTimeAsString,
		@returnArrDep
		
	DECLARE @lines int
	SELECT @lines = 0
	DECLARE @invalidLines int
	SELECT @invalidLines = 0
	DECLARE @badRows int
	SELECT @badRows = 0
	
	WHILE 0 = @@FETCH_STATUS
	BEGIN
		-- Insert the detail row
		SELECT @lines = @lines + 1
		
		-- if necessary prepend a zero to the time
		IF (LEN(@outwardTimeAsString) = 3)
			SET @outwardTimeAsString = '0' + @outwardTimeAsString
			
		-- Convert outward time 
		SET @outwardTimeAsString = LEFT(@outwardTimeAsString, 2) + ':' + RIGHT(@outwardTimeAsString, 2)
		
		-- Catch errors
		BEGIN TRY		
			-- check for return journey
			IF (LEN(@returnDate) > 0 AND LEN(@returnTimeAsString) > 0 AND LEN(@returnArrDep) > 0)
			BEGIN
				-- if necessary prepend a zero to the time
				IF (LEN(@returnTimeAsString) = 3)
					SET @returnTimeAsString = '0' + @returnTimeAsString

				-- Convert return time 
				SET @returnTimeAsString = LEFT(@returnTimeAsString, 2) + ':' + RIGHT(@returnTimeAsString, 2)
				
				INSERT INTO BatchRequestDetail
				   ([BatchId]
				   ,[BatchDetailStatusId]
				   ,[UserSuppliedUniqueId]
				   ,[JourneyParameters.OriginType]
				   ,[JourneyParameters.Origin]
				   ,[JourneyParameters.DestinationType]
				   ,[JourneyParameters.Destination]
				   ,[JourneyParameters.OutwardDate]
				   ,[JourneyParameters.OutwardTime]
				   ,[JourneyParameters.OutwardArrDep]
				   ,[JourneyParameters.ReturnDate]
				   ,[JourneyParameters.ReturnTime]
				   ,[JourneyParameters.ReturnArrDep]
				   ,[SubmittedDateTime])
				VALUES
				   (@BatchId
				   ,@detailStatus
				   ,@userSuppliedUniqueId
				   ,@originType
				   ,@origin
				   ,@destinationType
				   ,@destination
				   ,@outwardDate
				   ,@outwardTimeAsString
				   ,@outwardArrDep
				   ,@returnDate
				   ,@returnTimeAsString
				   ,@returnArrDep
				   ,@Date)
			END
			ELSE
			BEGIN
				INSERT INTO BatchRequestDetail
				   ([BatchId]
				   ,[BatchDetailStatusId]
				   ,[UserSuppliedUniqueId]
				   ,[JourneyParameters.OriginType]
				   ,[JourneyParameters.Origin]
				   ,[JourneyParameters.DestinationType]
				   ,[JourneyParameters.Destination]
				   ,[JourneyParameters.OutwardDate]
				   ,[JourneyParameters.OutwardTime]
				   ,[JourneyParameters.OutwardArrDep]
				   ,[JourneyParameters.ReturnDate]
				   ,[JourneyParameters.ReturnTime]
				   ,[JourneyParameters.ReturnArrDep]
				   ,[SubmittedDateTime])
				VALUES
				   (@BatchId
				   ,NULL
				   ,@userSuppliedUniqueId
				   ,@originType
				   ,@origin
				   ,@destinationType
				   ,@destination
				   ,@outwardDate
				   ,@outwardTimeAsString
				   ,@outwardArrDep
				   ,null
				   ,null
				   ,null
				   ,@Date)
			END
		END TRY
		BEGIN CATCH
			SELECT @badRows = @badRows + 1
		END CATCH

		FETCH NEXT FROM Cursor_Details INTO
			@userSuppliedUniqueId,
			@originType,
			@origin,
			@destinationType,
			@destination,
			@outwardDate,
			@outwardTimeAsString,
			@outwardArrDep,
			@returnDate,
			@returnTimeAsString,
			@returnArrDep
	END
	
	CLOSE Cursor_Details
	DEALLOCATE Cursor_Details

	-- Update the summary with the number of lines.
	UPDATE BatchRequestSummary
		SET NumberOfRequests = @lines,
			NumberOfInvalidRequests = @badRows
		WHERE BatchId = @BatchId
		
	-- return the batch id and queued position
	DECLARE @PaddedBatchId nvarchar(50)
	SELECT @PaddedBatchId = BatchJourneyPlanner.dbo.PadBatchId(@BatchId)
	
	DECLARE @Position int
	SELECT @Position = COUNT(*) + 1
		FROM BatchRequestSummary
		WHERE BatchId < @BatchId
		AND BatchStatusId < 3 -- ie queued or in progress
		
	DECLARE @ReturnPosition nvarchar(20)
	SELECT @ReturnPosition = CONVERT(nvarchar(20), @Position)
	SELECT @ReturnPosition = @ReturnPosition + 
		CASE
			WHEN LEN(@ReturnPosition) > 1 AND (LEFT(RIGHT(@ReturnPosition, 2), 1) = '1') THEN '<sup>th</sup>'
			WHEN RIGHT(@Position, 1) = '1' THEN '<sup>st</sup>'
			WHEN RIGHT(@Position, 1) = '2' THEN '<sup>nd</sup>'
			WHEN RIGHT(@Position, 1) = '3' THEN '<sup>rd</sup>'
			ELSE  '<sup>th</sup>'
		END
		
	SELECT @PaddedBatchId, @ReturnPosition
	RETURN 0
END
GO


CREATE PROCEDURE dbo.GetBatchRequestsForUser
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
				CASE WHEN @SortColumn = 'ValidationErrors' AND @SortDirection = 'asc' THEN NumberOfInvalidRequests END ASC,
				CASE WHEN @SortColumn = 'ValidationErrors' AND @SortDirection = 'desc' THEN NumberOfInvalidRequests END DESC,
				CASE WHEN @SortColumn = 'NoResults' AND @SortDirection = 'asc' THEN NumberOfUnsuccessfulRequests END ASC,
				CASE WHEN @SortColumn = 'NoResults' AND @SortDirection = 'desc' THEN NumberOfUnsuccessfulRequests END DESC,
				CASE WHEN @SortColumn = 'DateComplete' AND @SortDirection = 'asc' THEN CompletedDateTime END ASC,
				CASE WHEN @SortColumn = 'DateComplete' AND @SortDirection = 'desc' THEN CompletedDateTime END DESC,
				CASE WHEN @SortColumn = 'Status' AND @SortDirection = 'asc' THEN BatchStatusDescription END ASC,
				CASE WHEN @SortColumn = 'Status' AND @SortDirection = 'desc' THEN BatchStatusDescription END DESC)
		AS RowNumber
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


CREATE PROCEDURE dbo.SetUserPreferences
(
	@EmailAddress nvarchar(250),
	@Statistics bit,
	@Details bit,
	@PublicTransport bit,
	@Car bit,
	@Cycle bit,
	@Rtf bit
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
	
	UPDATE RegisteredUser
		SET IncludeStatistics = @Statistics,
		IncludeDetails = @Details,
		PublicTransport = @PublicTransport,
		Car = @Car,
		Cycle = @Cycle,
		ResultsAsRtf = @Rtf
	WHERE UserId = @UserId

	RETURN 0	
END
GO


CREATE PROCEDURE dbo.GetUserPreferences
(
	@EmailAddress nvarchar(250)
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
	
	SELECT IncludeStatistics,
		IncludeDetails,
		PublicTransport,
		Car,
		Cycle,
		ResultsAsRtf
	FROM RegisteredUser

	RETURN 0	
END
GO


CREATE PROCEDURE [dbo].[GetBatchSummary]
(
	@BatchId int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT DetailsDeletedDateTime,
		BatchStatusId,
		[ReportParameters.IncludeJourneyStatistics],
		[ReportParameters.IncludeJourneyDetails],
		[ReportParameters.IncludePublicTransport],
		[ReportParameters.IncludeCar],
		[ReportParameters.IncludeCycle],
		[ReportParameters.ConvertToRtf]
		,QueuedDateTime
		,CompletedDateTime
		,NumberOfRequests
		,NumberOfSuccessfulResults
		,NumberOfUnsuccessfulRequests
	FROM BatchRequestSummary
	WHERE BatchId = @BatchId;
END
GO

CREATE PROCEDURE [dbo].[GetBatchRequestDetailsForProcessing]
(
	@ProcessorId nvarchar(50),
	@NumberRequests int
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
		IF (EXISTS (SELECT BatchId FROM BatchRequestSummary WHERE ProcessorId IS NULL AND BatchStatusId = 1))
		BEGIN
			SELECT @BatchId = BatchId 
				FROM BatchRequestSummary 
				WHERE ProcessorId IS NULL 
				AND BatchStatusId = 1
				ORDER BY QueuedDateTime DESC
				
			UPDATE BatchRequestSummary
				SET ProcessorId = @ProcessorId,
				BatchStatusId = 2
				WHERE BatchId = @BatchId
		END
		ELSE
		BEGIN
			-- No requests available
			RETURN 1
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
		-- Finish off this batch and update the summary record
		SET @Date = GETDATE()
		
		UPDATE BatchRequestSummary
			SET CompletedDateTime = @Date,
			NumberOfSuccessfulResults = (SELECT COUNT(*) FROM BatchRequestDetail WHERE BatchDetailStatusId = 2 AND BatchId = @BatchId),
			NumberOfUnsuccessfulRequests = (SELECT COUNT(*) FROM BatchRequestDetail WHERE BatchDetailStatusId = 3 AND BatchId = @BatchId),
			BatchStatusId = 3,
			ProcessorId = null
		WHERE BatchId = @BatchId
		
		-- Get a new batch if there is one available
		IF (EXISTS (SELECT BatchId FROM BatchRequestSummary WHERE ProcessorId IS NULL AND BatchStatusId = 1))
		BEGIN
			SELECT @BatchId = BatchId 
				FROM BatchRequestSummary 
				WHERE ProcessorId IS NULL 
				AND BatchStatusId = 1
				ORDER BY QueuedDateTime DESC
				
			UPDATE BatchRequestSummary
				SET ProcessorId = @ProcessorId,
				BatchStatusId = 2
				WHERE BatchId = @BatchId
		END
		ELSE
		BEGIN
			-- No requests available
			RETURN 1
		END
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
		bd.RequestId,
		bs.[ReportParameters.IncludeJourneyStatistics],
		bs.[ReportParameters.IncludeJourneyDetails],
		bs.[ReportParameters.IncludePublicTransport],
		bs.[ReportParameters.IncludeCar],
		bs.[ReportParameters.IncludeCycle],
		bs.[ReportParameters.ConvertToRtf],
		bd.UserSuppliedUniqueId,
		bd.[JourneyParameters.OriginType],
		bd.[JourneyParameters.Origin],
		bd.[JourneyParameters.DestinationType],
		bd.[JourneyParameters.Destination],
		bd.[JourneyParameters.OutwardDate],
		bd.[JourneyParameters.OutwardTime],
		bd.[JourneyParameters.OutwardArrDep],
		bd.[JourneyParameters.ReturnDate],
		bd.[JourneyParameters.ReturnTime],
		bd.[JourneyParameters.ReturnArrDep]
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

CREATE PROCEDURE dbo.UpdateBatchRequestDetail
(
	@BatchDetailId int,
	@PublicJourneyResultSummary nvarchar(250),
	@PublicJourneyResultDetails xml,
	@PublicOutwardJourneyStatus int,
	@PublicReturnJourneyStatus int,
	@PublicJourneyErrorMessage nvarchar(200),
	@CarResultSummary nvarchar(250),
	@CarOutwardStatus int,
	@CarReturnStatus int,
	@CarErrorMessage nvarchar(200),
	@CycleResultSummary nvarchar(250),
	@CycleOutwardStatus int,
	@CycleReturnStatus int,
	@CycleErrorMessage nvarchar(200)
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
	
	DECLARE @ErrorMessage nvarchar(700)
	SELECT @ErrorMessage = ''
	
	IF (LEN(@PublicJourneyErrorMessage) > 0)
	BEGIN
		SELECT @ErrorMessage = @ErrorMessage + @PublicJourneyErrorMessage + ' '
		SET @DetailStatusId = 3 -- errored
	END
	
	IF (LEN(@CarErrorMessage) > 0)
	BEGIN
		SELECT @ErrorMessage = @ErrorMessage + @CarErrorMessage + ' '
		SET @DetailStatusId = 3 -- errored
	END
		
	IF (LEN(@CycleErrorMessage) > 0)
	BEGIN
		SELECT @ErrorMessage = @ErrorMessage + @CycleErrorMessage
		SET @DetailStatusId = 3 -- errored
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
		ErrorMessages = @ErrorMessage,
		BatchDetailStatusId = @DetailStatusId
	WHERE RequestId = @BatchDetailId

END
GO


CREATE PROCEDURE dbo.GetBatchResults
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
	FROM [BatchJourneyPlanner].[dbo].[BatchRequestDetail]
	WHERE BatchId = @BatchId
	
END
GO


CREATE PROCEDURE dbo.GetUsers
(
	@Page int,
	@SortColumn nvarchar(100),
	@SortDirection nvarchar(5)
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Paging (20 per page)
	DECLARE @StartRow int
	DECLARE @EndRow int
	SET @StartRow = 20 * (@Page - 1) + 1
	SET @EndRow = @StartRow + 19
	
	DECLARE @Total int
	SELECT @Total = COUNT(*)
		FROM RegisteredUser;
	
	-- Output the results
	WITH Results AS
	(SELECT [EmailAddress]
		,brs.QueuedDateTime
		,us.UserStatusDescription
		,ru.StatusChanged
		,ROW_NUMBER() OVER 
			(ORDER BY
				CASE WHEN @SortColumn = 'User' AND @SortDirection = 'asc' THEN EmailAddress END ASC,
				CASE WHEN @SortColumn = 'User' AND @SortDirection = 'desc' THEN EmailAddress END DESC,
				CASE WHEN @SortColumn = 'StatusChange' AND @SortDirection = 'asc' THEN ru.StatusChanged END ASC,
				CASE WHEN @SortColumn = 'StatusChange' AND @SortDirection = 'desc' THEN ru.StatusChanged END DESC,
				CASE WHEN @SortColumn = 'LastFileUpload' AND @SortDirection = 'asc' THEN brs.QueuedDateTime END ASC,
				CASE WHEN @SortColumn = 'LastFileUpload' AND @SortDirection = 'desc' THEN brs.QueuedDateTime END DESC,
				CASE WHEN @SortColumn = 'Status' AND @SortDirection = 'asc' THEN us.UserStatusDescription END ASC,
				CASE WHEN @SortColumn = 'Status' AND @SortDirection = 'desc' THEN us.UserStatusDescription END DESC
			) 
			AS RowNumber
	FROM [BatchJourneyPlanner].[dbo].[RegisteredUser] ru
	INNER JOIN [BatchJourneyPlanner].[dbo].[UserStatus] us
	ON ru.UserStatusId = us.UserStatusId
	LEFT JOIN [BatchJourneyPlanner].[dbo].[BatchRequestSummary] brs
	ON ru.UserId = brs.UserId
	AND brs.QueuedDateTime = (SELECT MAX(brs2.QueuedDateTime)
						FROM BatchJourneyPlanner.dbo.BatchRequestSummary brs2
						WHERE brs2.UserId = brs.UserId))

	SELECT * FROM Results
	WHERE RowNumber >= @StartRow
	AND RowNumber <= @EndRow
	ORDER BY RowNumber ASC
	
	RETURN @Total
END
GO


CREATE PROCEDURE dbo.DeleteBatchDetails
(
	@Days int
)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Remove old completed batches
	DECLARE @Date datetime
	SELECT @Date = DATEADD(DAY, -@Days, GETDATE())
	 
	DELETE FROM BatchRequestDetail
	WHERE BatchId IN (SELECT BatchId
						FROM BatchRequestSummary
						WHERE CompletedDateTime < @Date)
						
	DELETE FROM BatchRequestSummary
	WHERE CompletedDateTime < @Date

	-- Remove old failed batches
	SELECT @Date = DATEADD(DAY, -10, GETDATE())
		
	DELETE FROM BatchRequestDetail
	WHERE BatchId IN (SELECT BatchId
						FROM BatchRequestSummary
						WHERE QueuedDateTime < @Date)
						
	DELETE FROM BatchRequestSummary
	WHERE QueuedDateTime < @Date

END
GO


CREATE PROCEDURE dbo.RemoveDeletedUsers
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	-- Get a list of deleted TD users
	DECLARE DeletedUsersCursor CURSOR FOR
		SELECT EmailAddress 
		FROM RegisteredUser
		WHERE EmailAddress NOT IN (SELECT UserID
									FROM TDUserInfo..ProfileData)
	
	DECLARE @EmailAddress nvarchar(250)
	DECLARE @UserId int
	
	OPEN DeletedUsersCursor
	
	FETCH NEXT FROM DeletedUsersCursor
	INTO @EmailAddress
	
	WHILE @@FETCH_STATUS = 0
	BEGIN
		-- Get the user id
		SELECT @UserId = UserId
		FROM RegisteredUser
		WHERE EmailAddress = @EmailAddress
	
		-- Delete detail records
		DELETE FROM BatchRequestDetail
		WHERE BatchId IN (SELECT BatchId
						FROM BatchRequestSummary
						WHERE UserId = @UserId)
		
		-- Delete summary records
		DELETE FROM BatchRequestSummary
		WHERE UserId = @UserId
		
		-- Delete user record
		DELETE FROM RegisteredUser
		WHERE UserId = @UserId
		
		FETCH NEXT FROM DeletedUsersCursor
		INTO @EmailAddress
	END

	CLOSE DeletedUsersCursor
	DEALLOCATE DeletedUsersCursor

END
GO
