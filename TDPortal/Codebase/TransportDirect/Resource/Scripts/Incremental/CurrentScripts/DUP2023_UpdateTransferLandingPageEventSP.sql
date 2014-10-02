-- ***********************************************
-- NAME 		: DUP2023_UpdateTransferLandingPageEventSP.sql
-- DESCRIPTION 	        : update TransferLandingPageEvent.sql SP
-- AUTHOR		: Phil Scott
-- DATE			: 18 Apr 2013
-- ************************************************


USE [ReportStagingDB]
GO

/****** Object:  StoredProcedure [dbo].[JPSTransferLandingPageEvents]    Script Date: 04/18/2013 13:34:25 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO


ALTER PROCEDURE [dbo].[TransferLandingPageEvents] (@Date varchar(10))
AS
SET NOCOUNT ON
SET DATEFIRST 1
SET XACT_ABORT ON


;WITH MissingPartners AS
(
    SELECT DISTINCT
           LPE.LPPCode
      FROM dbo.LandingPageEvent LPE
     WHERE LPE.LPPCode IS NOT NULL
       AND LPE.LPPCode > ''
       AND NOT EXISTS(SELECT NULL
                        FROM ReportServer.Reporting.dbo.LandingPagePartner LPP
                        WHERE LPE.LPPCode = LPP.LPPCode)
),
MissingPartnersWithID AS
(
    SELECT ROW_NUMBER() OVER (ORDER BY LPPCode) RowNum,
           LPPCode
     FROM MissingPartners 
),
MissingPartnersWithCorrectID AS
(
    SELECT RowNum + (SELECT MAX(LPPID) 
                       FROM ReportServer.Reporting.dbo.LandingPagePartner
                      WHERE LPPID NOT IN(999,1000,1001))
                  + CASE 
                        WHEN RowNum + (SELECT MAX(LPPID) 
                                         FROM ReportServer.Reporting.dbo.LandingPagePartner
                                        WHERE LPPID NOT IN(999,1000,1001)) >= 999 AND (SELECT MAX(LPPID) 
                                                  FROM ReportServer.Reporting.dbo.LandingPagePartner
                                                 WHERE LPPID NOT IN(999,1000,1001)) < 999
                        THEN 3
                        ELSE 0
                    END RowNum,
           LPPCode
     FROM MissingPartnersWithId
)
INSERT INTO ReportServer.Reporting.[dbo].[LandingPagePartner]
(
    [LPPID],
    [LPPCode],
    [LPPDescription]
)
SELECT RowNum,
       LPPCode,
       LPPCode
  FROM MissingPartnersWithCorrectID 


	DELETE FROM ReportServer.Reporting.dbo.LandingPageEvents
	WHERE LPEDate >= @Date and LPEDate < DATEADD(dd,1,@date)


	INSERT INTO ReportServer.Reporting.dbo.LandingPageEvents
	(
		LPEDate,
		LPEHour,
		LPEHourQuarter,
		LPEWeekDay,
		LPECount,
		LPELPPID,
		LPELPSID
	)
	SELECT
		CAST(CONVERT(varchar(10), LPE.TimeLogged, 121) AS datetime) AS LPEDATE,
		DATEPART(hour, LPE.TimeLogged) AS LPEHour,
		CAST(DATEPART(minute, LPE.TimeLogged) / 15 AS smallint) AS LPEHourQuater,
		DATEPART(weekday, LPE.TimeLogged) AS LPEWeekDay,
		COUNT(*) AS LPECOUNT,
		LPP.LPPID AS LPELPPID,
		LPS.LPSID AS LPELPSID
	FROM
		LandingPageEvent LPE 
			LEFT OUTER JOIN ReportServer.Reporting.dbo.LandingPagePartner LPP ON LPE.LPPCODE = LPP.LPPCODE
			LEFT OUTER JOIN ReportServer.Reporting.dbo.LandingPageService LPS ON LPE.LPSCODE = LPS.LPSCODE
WHERE TimeLogged >= @Date and TimeLogged < DATEADD(dd,1,@date)
and (LPE.LPPCODE in (select LPPCODE from ReportServer.Reporting.dbo.LandingPagePartner))

	GROUP BY
		CAST(CONVERT(varchar(10), LPE.TimeLogged, 121) AS datetime),
		DATEPART(hour, LPE.TimeLogged),
		CAST(DATEPART(minute, LPE.TimeLogged) / 15 AS smallint),
		DATEPART(weekday, LPE.TimeLogged),
		LPP.LPPID,
		LPS.LPSID

	INSERT INTO ReportServer.Reporting.dbo.LandingPageEvents
	(
		LPEDate,
		LPEHour,
		LPEHourQuarter,
		LPEWeekDay,
		LPECount,
		LPELPPID,
		LPELPSID
	)
	SELECT
		CAST(CONVERT(varchar(10), LPE.TimeLogged, 121) AS datetime) AS LPEDATE,
		DATEPART(hour, LPE.TimeLogged) AS LPEHour,
		CAST(DATEPART(minute, LPE.TimeLogged) / 15 AS smallint) AS LPEHourQuater,
		DATEPART(weekday, LPE.TimeLogged) AS LPEWeekDay,
		COUNT(*) AS LPECOUNT,
		999 AS LPELPPID,
		LPS.LPSID AS LPELPSID
	FROM
		LandingPageEvent LPE 
			LEFT OUTER JOIN ReportServer.Reporting.dbo.LandingPagePartner LPP ON LPE.LPPCODE = LPP.LPPCODE
			LEFT OUTER JOIN ReportServer.Reporting.dbo.LandingPageService LPS ON LPE.LPSCODE = LPS.LPSCODE
WHERE TimeLogged >= @Date and TimeLogged < DATEADD(dd,1,@date)
and not (LPE.LPPCODE in (select LPPCODE from ReportServer.Reporting.dbo.LandingPagePartner))

	GROUP BY
		CAST(CONVERT(varchar(10), LPE.TimeLogged, 121) AS datetime),
		DATEPART(hour, LPE.TimeLogged),
		CAST(DATEPART(minute, LPE.TimeLogged) / 15 AS smallint),
		DATEPART(weekday, LPE.TimeLogged),
		LPP.LPPID,
		LPS.LPSID



GO



----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2023
SET @ScriptDesc = 'update TransferLandingPageEvent.sql SP'

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