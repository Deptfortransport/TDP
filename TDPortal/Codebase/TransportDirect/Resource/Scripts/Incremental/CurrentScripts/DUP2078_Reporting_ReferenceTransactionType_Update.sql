-- ***********************************************
-- NAME 		: DUP2078_Reporting_ReferenceTransactionType_Update.sql
-- DESCRIPTION 	: Update ReferenceTransactionType in Reporting database
-- AUTHOR		: Mitesh Modi
-- DATE			: 02 Sep 2013
-- ************************************************

USE [Reporting]
GO

DECLARE @id int 

IF NOT EXISTS (SELECT * FROM [dbo].[ReferenceTransactionType] WHERE RTTCode = 'KPI17')
BEGIN
	SET @id = (SELECT MAX(RTTID) + 1 FROM [dbo].[ReferenceTransactionType])
	INSERT INTO [dbo].[ReferenceTransactionType] VALUES (
	@id, 'KPI17', 'Mobile Home Page 2013', 0, 0, 0, 95.00000, 90.00000, NULL, 0, 10)
END
ELSE 
BEGIN 
	SET @id = (SELECT RTTID FROM [dbo].[ReferenceTransactionType] WHERE RTTCode = 'KPI17')
	UPDATE [dbo].[ReferenceTransactionType] 
	SET 
	   [RTTDescription] = 'Mobile Home Page 2013'
      ,[RTTCreditInclude] = 0
      ,[RTTSLAInclude]= 0
      ,[RTTMaxMsDuration] = 0
      ,[RTTTarget] = 95.00000
      ,[RTTThreshold] = 90.00000
      ,[RTTChannel] = NULL
      ,[RTTAmberMsDuration] = 0 
      ,[RTTInjectionFrequency] = 10
	WHERE RTTID = @id
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2078, 'Update ReferenceTransactionType in Reporting database'