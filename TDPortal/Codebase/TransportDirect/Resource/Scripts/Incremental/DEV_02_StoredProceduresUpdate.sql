-- **********************************************
-- **********************************************
-- ONLY RUN THIS SCRIPT IN DEV
-- **********************************************
-- **********************************************

USE [ReportStagingDB]
GO

-- **********************************************
-- **********************************************

ALTER PROCEDURE [dbo].[TransferInitialise]
	@ConnectionString varchar(255)
AS
BEGIN

	--EXEC sp_dropserver @server='ReportServer'

	--EXEC sp_addlinkedserver
	--	@server = 'ReportServer',
	--	@provider = 'MSDASQL',
	--	@srvproduct = '',
	--	@provstr = @ConnectionString

	--EXEC sp_serveroption 'ReportServer', 'rpc', 'TRUE'

	--EXEC sp_serveroption 'ReportServer', 'rpc out', 'TRUE'

	--EXEC ReportServer.Reporting.dbo.TransferInitialise
	EXEC Reporting.dbo.TransferInitialise
END
GO

-- **********************************************
-- **********************************************

ALTER PROCEDURE [dbo].[TransferComplete]
AS
BEGIN
	--EXEC ReportServer.Reporting.dbo.TransferComplete
	EXEC Reporting.dbo.TransferComplete	
END
GO

-- **********************************************
-- **********************************************


USE [TransientPortal]
GO

ALTER  PROCEDURE [dbo].[GetZonalOperatorLinks]
AS
BEGIN 

DECLARE @date SMALLDATETIME

SET @date = CONVERT(char(8), GetDate(), 112)

--SELECT RegionId, OperatorCode, ModeId, URL FROM ZonalOperatorLinks
SELECT TOP 10 RegionId, OperatorCode, ModeId, URL FROM ZonalOperatorLinks
INNER JOIN ExternalLinks ON ZonalOperatorLinks.[OPeratorLinkId] = ExternalLinks.[Id]
WHERE 
(
	(ExternalLinks.[EndDate] >= @date AND ExternalLinks.[StartDate] IS NULL)
OR 
	(ExternalLinks.[StartDate] IS NULL AND ExternalLinks.[EndDate] IS NULL) 
OR 
	(ExternalLinks.[StartDate] <= @date AND ExternalLinks.[EndDate] IS NULL)
OR
	(ExternalLinks.[StartDate] <= @date AND ExternalLinks.[EndDate] >= @date)
)

END
GO

-- **********************************************
-- **********************************************

ALTER  PROCEDURE [dbo].[GetZonalOperatorFaresLinks]
AS
BEGIN

DECLARE @date SMALLDATETIME

SET @date = CONVERT(char(8), GetDate(), 112)

--SELECT RegionId, OperatorCode, ModeId, URL FROM ZonalOperatorLinks
SELECT TOP 10 RegionId, OperatorCode, ModeId, URL FROM ZonalOperatorLinks
INNER JOIN ExternalLinks ON ZonalOperatorLinks.[OPeratorFaresLinkId] = ExternalLinks.[Id]
WHERE 
(
	(ExternalLinks.[EndDate] >= @date AND ExternalLinks.[StartDate] IS NULL)
OR 
	(ExternalLinks.[StartDate] IS NULL AND ExternalLinks.[EndDate] IS NULL) 
OR 
	(ExternalLinks.[StartDate] <= @date AND ExternalLinks.[EndDate] IS NULL)
OR
	(ExternalLinks.[StartDate] <= @date AND ExternalLinks.[EndDate] >= @date)
)

END
GO

-- **********************************************
-- **********************************************