-- This script provides stored procedures that enable journey processing event data to be viewed at various stages of the transfer process.
-- This can be useful for debugging or determining the expected results of running the TransferJourneyProcessingEvents stored procedure.
-- Note that the @Date variable defined below must be changed manually prior to running this script.

USE [PermanentPortal]

DECLARE @Date varchar(10)
DECLARE @MsWindow int
SET @Date = '2003-11-22'
SET @MsWindow = 10000

SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON

DELETE FROM ReportServer.Reporting.dbo.JourneyProcessingEvents
WHERE CONVERT(varchar(10), JPEDate, 121) = @Date

DECLARE @JourneyPlanRequestId varchar(50)
DECLARE @Submitted datetime
DECLARE @TimeLogged datetime
DECLARE @MsDuration int
DECLARE @RefTransaction bit

DECLARE @StoreJourneyPlanRequestId varchar(50)
DECLARE @StoreWindowStart datetime
DECLARE @StoreWindowEnd datetime
DECLARE @StoreMaxMsDuration int
DECLARE @StoreRefTransaction bit
DECLARE @OrigWindowStart datetime
DECLARE @SumMaxMsDuration int

CREATE TABLE #Windows
(
	JourneyPlanRequestId varchar(50),
	WindowStart datetime,
	WindowEnd datetime,
	SumMaxMsDuration int,
	RefTransaction bit
)

select * from JourneyWebRequestEvent

SELECT
	JPRQE.JourneyPlanRequestId,
	JWRE1.Submitted,
	JWRE1.TimeLogged,
	DATEDIFF(millisecond, JWRE1.Submitted, JWRE1.TimeLogged) AS MsDuration,
	JWRE1.RefTransaction
FROM JourneyWebRequestEvent JWRE1
INNER JOIN JourneyPlanRequestEvent JPRQE ON LEFT(JWRE1.JourneyWebRequestId, 24) = JPRQE.JourneyPlanRequestId
WHERE CONVERT(varchar(10), JWRE1.TimeLogged, 121) = @Date
ORDER BY
	JPRQE.JourneyPlanRequestId,
	JWRE1.Submitted



DECLARE LocalCursor CURSOR FORWARD_ONLY READ_ONLY FOR
SELECT
	JPRQE.JourneyPlanRequestId,
	JWRE1.Submitted,
	JWRE1.TimeLogged,
	DATEDIFF(millisecond, JWRE1.Submitted, JWRE1.TimeLogged) AS MsDuration,
	JWRE1.RefTransaction
FROM JourneyWebRequestEvent JWRE1
INNER JOIN JourneyPlanRequestEvent JPRQE ON LEFT(JWRE1.JourneyWebRequestId, 24) = JPRQE.JourneyPlanRequestId
WHERE CONVERT(varchar(10), JWRE1.TimeLogged, 121) = @Date
ORDER BY
	JPRQE.JourneyPlanRequestId,
	JWRE1.Submitted

OPEN LocalCursor

SET @StoreJourneyPlanRequestId = ''
SET @StoreWindowStart = CAST(0 AS datetime)
SET @StoreWindowEnd = CAST(0 AS datetime)
SET @StoreMaxMsDuration = 0
SET @StoreRefTransaction = 0
SET @OrigWindowStart = 0
SET @SumMaxMsDuration = 0

FETCH NEXT FROM LocalCursor INTO @JourneyPlanRequestId, @Submitted, @TimeLogged, @MsDuration, @RefTransaction

WHILE @@Fetch_Status <> -1
BEGIN
	IF @@Fetch_Status <> -2
	BEGIN
		IF @JourneyPlanRequestId <> @StoreJourneyPlanRequestId
		BEGIN
			SET @StoreJourneyPlanRequestId = @JourneyPlanRequestId
			SET @StoreWindowStart = @Submitted
			SET @StoreWindowEnd = @TimeLogged
			SET @StoreMaxMsDuration = @MsDuration
			SET @StoreRefTransaction = @RefTransaction
			SET @OrigWindowStart = @Submitted
			SET @SumMaxMsDuration = 0
PRINT '1 ID=' + @JourneyPlanRequestId
+ ' SID=' + @StoreJourneyPlanRequestId
+ ' SRT=' + CONVERT(varchar(50), @Submitted, 120)
+ ' SSRT=' + CONVERT(varchar(50), @StoreWindowStart, 120)
+ ' END=' + CONVERT(varchar(50), @TimeLogged, 120)
+ ' SEND=' + CONVERT(varchar(50), @StoreWindowEnd, 120)
+ ' MAX=' + CAST(@MsDuration AS varchar(50))
+ ' SMAX=' + CAST(@StoreMaxMsDuration AS varchar(50))
+ ' SUM=' + CAST(@SumMaxMsDuration AS varchar(50))
		END
		
		FETCH NEXT FROM LocalCursor INTO @JourneyPlanRequestId, @Submitted, @TimeLogged, @MsDuration, @RefTransaction
		
		IF @@Fetch_Status = -1 OR @@Fetch_Status = -2
		BEGIN
			INSERT INTO #Windows
			(
				JourneyPlanRequestId,
				WindowStart,
				WindowEnd,
				SumMaxMsDuration,
				RefTransaction
			)
			VALUES
			(
				@StoreJourneyPlanRequestId,
				@OrigWindowStart,
				@StoreWindowEnd,
				@SumMaxMsDuration + @StoreMaxMsDuration,
				@StoreRefTransaction
			)
PRINT '2 ID=' + @StoreJourneyPlanRequestId
+ ' SID=' + @StoreJourneyPlanRequestId
+ ' SRT=' + CONVERT(varchar(50), @StoreWindowStart, 120)
+ ' SSRT=' + CONVERT(varchar(50), @StoreWindowStart, 120)
+ ' END=' + CONVERT(varchar(50), @StoreWindowEnd, 120)
+ ' SEND=' + CONVERT(varchar(50), @StoreWindowEnd, 120)
+ ' MAX=' + CAST(@StoreMaxMsDuration AS varchar(50))
+ ' SMAX=' + CAST(@StoreMaxMsDuration AS varchar(50))
+ ' SUM=' + CAST(@SumMaxMsDuration + @StoreMaxMsDuration AS varchar(50))
		END
		ELSE IF @JourneyPlanRequestId <> @StoreJourneyPlanRequestId
		BEGIN
			INSERT INTO #Windows
			(
				JourneyPlanRequestId,
				WindowStart,
				WindowEnd,
				SumMaxMsDuration,
				RefTransaction
			)
			VALUES
			(
				@StoreJourneyPlanRequestId,
				@OrigWindowStart,
				@StoreWindowEnd,
				@SumMaxMsDuration + @StoreMaxMsDuration,
				@StoreRefTransaction
			)
PRINT '3 ID=' + @JourneyPlanRequestId
+ ' SID=' + @StoreJourneyPlanRequestId
+ ' SRT=' + CONVERT(varchar(50), @Submitted, 120)
+ ' SSRT=' + CONVERT(varchar(50), @StoreWindowStart, 120)
+ ' END=' + CONVERT(varchar(50), @TimeLogged, 120)
+ ' SEND=' + CONVERT(varchar(50), @StoreWindowEnd, 120)
+ ' MAX=' + CAST(@MsDuration AS varchar(50))
+ ' SMAX=' + CAST(@StoreMaxMsDuration AS varchar(50))
+ ' SUM=' + CAST(@SumMaxMsDuration + @StoreMaxMsDuration AS varchar(50))
		END
		ELSE
		BEGIN
			IF DATEDIFF(millisecond, @StoreWindowStart, @Submitted) BETWEEN 0 AND @MsWindow
			BEGIN
				IF @StoreWindowEnd < @TimeLogged
					SET @StoreWindowEnd = @TimeLogged
				
				IF @StoreMaxMsDuration < @MsDuration
					SET @StoreMaxMsDuration = @MsDuration
				
				IF @StoreRefTransaction < @RefTransaction
					SET @StoreRefTransaction = @RefTransaction
PRINT '4 ID=' + @JourneyPlanRequestId
+ ' SID=' + @StoreJourneyPlanRequestId
+ ' SRT=' + CONVERT(varchar(50), @Submitted, 120)
+ ' SSRT=' + CONVERT(varchar(50), @StoreWindowStart, 120)
+ ' END=' + CONVERT(varchar(50), @TimeLogged, 120)
+ ' SEND=' + CONVERT(varchar(50), @StoreWindowEnd, 120)
+ ' MAX=' + CAST(@MsDuration AS varchar(50))
+ ' SMAX=' + CAST(@StoreMaxMsDuration AS varchar(50))
+ ' SUM=' + CAST(@SumMaxMsDuration AS varchar(50))
			END
			
			IF @Submitted > @StoreWindowEnd
			BEGIN
				SET @SumMaxMsDuration = @SumMaxMsDuration + @StoreMaxMsDuration
				
				SET @StoreJourneyPlanRequestId = @JourneyPlanRequestId
				SET @StoreWindowStart = @Submitted
				SET @StoreWindowEnd = @TimeLogged
				SET @StoreMaxMsDuration = @MsDuration
				SET @StoreRefTransaction = @RefTransaction
PRINT '5 ID=' + @JourneyPlanRequestId
+ ' SID=' + @StoreJourneyPlanRequestId
+ ' SRT=' + CONVERT(varchar(50), @Submitted, 120)
+ ' SSRT=' + CONVERT(varchar(50), @StoreWindowStart, 120)
+ ' END=' + CONVERT(varchar(50), @TimeLogged, 120)
+ ' SEND=' + CONVERT(varchar(50), @StoreWindowEnd, 120)
+ ' MAX=' + CAST(@MsDuration AS varchar(50))
+ ' SMAX=' + CAST(@StoreMaxMsDuration AS varchar(50))
+ ' SUM=' + CAST(@SumMaxMsDuration AS varchar(50))
			END
		END
	END
END

CLOSE LocalCursor
DEALLOCATE LocalCursor

select * from #Windows

select * from JourneyPlanRequestEvent
select * from JourneyPlanResultsEvent

/*
INSERT INTO ReportServer.Reporting.dbo.JourneyProcessingEvents
(
	JPEDate,
	JPEHour,
	JPEHourQuarter,
	JPEWeekDay,
	JPERefTransaction,
	JPEAvMsProcessingDuration,
	JPEAvMsWaitingDuration,
	JPECount
)
*/
SELECT
	CAST(CONVERT(varchar(10), JPRQE.TimeLogged, 121) AS datetime) AS JPEDate,
	DATEPART(hour, JPRQE.TimeLogged) AS JPEHour,
	CAST(DATEPART(minute, JPRQE.TimeLogged) / 15 AS smallint) AS JPEHourQuarter,
	DATEPART(weekday, JPRQE.TimeLogged) AS JPEWeekDay,
	WIN.RefTransaction AS JPERefTransaction,
	AVG(ISNULL(CAST(DATEDIFF(millisecond, JPRQE.TimeLogged, JPRSE.TimeLogged) AS decimal(18, 0)), 0)) AS JPEAvMsProcessingDuration,
	AVG(ISNULL(CAST(WIN.SumMaxMsDuration AS decimal(18, 0)), 0)) AS JPEAvMsWaitingDuration,
	COUNT(JPRQE.TimeLogged) AS JPECount
FROM JourneyPlanRequestEvent JPRQE
INNER JOIN JourneyPlanResultsEvent JPRSE ON JPRQE.JourneyPlanRequestId = JPRSE.JourneyPlanRequestId
INNER JOIN #Windows WIN ON JPRQE.JourneyPlanRequestId = WIN.JourneyPlanRequestId
WHERE CONVERT(varchar(10), JPRQE.TimeLogged, 121) = @Date
GROUP BY
	CAST(CONVERT(varchar(10), JPRQE.TimeLogged, 121) AS datetime),
	DATEPART(hour, JPRQE.TimeLogged),
	CAST(DATEPART(minute, JPRQE.TimeLogged) / 15 AS smallint),
	DATEPART(weekday, JPRQE.TimeLogged),
	WIN.RefTransaction

DROP TABLE #Windows
