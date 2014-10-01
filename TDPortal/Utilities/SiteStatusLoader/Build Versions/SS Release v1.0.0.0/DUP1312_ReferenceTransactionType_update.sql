-- ***********************************************
-- NAME 		: DUP1312_ReferenceTransactionType_update.sql
-- DESCRIPTION 		: Script to add table for ReferenceTransactionType records
-- AUTHOR		: Mitesh Modi
-- DATE			: 13 Mar 2009
-- ************************************************

USE [Reporting]
GO

----------------------------------------------------------------
-- Create SiteStatusLoaderEvent
----------------------------------------------------------------

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[ReferenceTransactionType]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN

	if exists (select * from [dbo].[ReferenceTransactionType] where RTTCode in ('SLA01', 'SLA02', 'SLA03', 'SLA04', 'SLA05', 'SLA06', 'SLA07', 'SLA08', 'SLA09', 'SLA10', 'SLA11', 'SLA12', 'SLA13', 'SLA14'))
	BEGIN
		
		DELETE FROM [dbo].[ReferenceTransactionType]
		WHERE RTTCode in ('SLA01', 'SLA02', 'SLA03', 'SLA04', 'SLA05', 'SLA06','SLA07', 'SLA08', 'SLA09', 'SLA10', 'SLA11', 'SLA12', 'SLA13', 'SLA14')

	END

	if exists (select * from [dbo].[ReferenceTransactionType] where RTTCode in ('KPI01', 'KPI02', 'KPI03', 'KPI04', 'KPI05', 'KPI06', 'KPI07', 'KPI08', 'KPI09', 'KPI10'))
	BEGIN
		
		DELETE FROM [dbo].[ReferenceTransactionType]
		WHERE RTTCode in ('KPI01', 'KPI02', 'KPI03', 'KPI04', 'KPI05', 'KPI06', 'KPI07', 'KPI08', 'KPI09', 'KPI10')

	END

	DECLARE @id TINYINT


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA01', 'Complex Query 1 - Find-a-Train', 0, 1, 7000, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA02', 'Complex Query 2 - Find-a-Car', 0, 1, 7000, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA03', 'Simple Query 1 - Find-a-Flight', 0, 1, 4000, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA04', 'Simple Query 2 - Location Map', 0, 1, 5000, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA05', 'Simple Query 3 - Address Gaz', 0, 1, 5000, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA06', 'Simple Query 4 - Travel News', 0, 1, 4000, 95, 90)

	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA07', 'Simple Query 5 - Home Page', 0, 1, 11000, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA08', 'Journey Planning Sub-Home Page', 0, 1, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA09', 'Maps Sub-Home Page', 0, 1, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA10', 'Live Travel Sub-Home Page', 0, 1, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA11', 'Mobile Home Page', 0, 1, 0, 95, 90)

	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA12', 'Web Service - Journey Planner', 0, 1, 0, 95, 90)

	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA13', 'Web Service - Departure Board', 0, 1, 0, 95, 90)

	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'SLA14', 'Web Service - Find Nearest', 0, 1, 0, 95, 90)

	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'KPI01', 'City-to-City', 0, 0, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'KPI02', 'Find-a-Cycle', 0, 0, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'KPI03', 'Find-a-Coach (NCSD)', 0, 0, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'KPI04', 'Direct - NCSD', 0, 0, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'KPI05', 'MRMD', 0, 0, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'KPI06', 'Direct - MRMD', 0, 0, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'KPI07', 'LSEEAEM', 0, 0, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'KPI08', 'Direct - LSEEAEM', 0, 0, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'KPI09', 'NE', 0, 0, 0, 95, 90)


	SET @id = (SELECT MAX(RTTID) FROM [ReferenceTransactionType]) + 1

	INSERT INTO [dbo].[ReferenceTransactionType] (RTTID, RTTCode, RTTDescription, RTTCreditInclude, RTTSLAInclude, RTTMaxMsDuration, RTTTarget, RTTThreshold)
	VALUES (@id, 'KPI10', 'Direct - NE', 0, 0, 0, 95, 90)
END
GO





----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------

USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 1312
SET @ScriptDesc = 'Add ReferenceTransactionType for new injections'

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