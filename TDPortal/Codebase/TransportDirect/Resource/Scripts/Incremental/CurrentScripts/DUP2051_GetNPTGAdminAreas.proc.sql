-- ***********************************************
-- NAME           : DUP2051_GetNPTGAdminAreas.proc.sql
-- DESCRIPTION    : GetNPTGAdminAreas stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetNPTGAdminAreas]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE GetNPTGAdminAreas
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to GetNPTGAdminAreas
-- =============================================
ALTER PROCEDURE [dbo].[GetNPTGAdminAreas]
AS
	SELECT [AdministrativeAreaCode]
		  ,[AreaName]
		  ,[Country]
		  ,[RegionCode]
	 FROM [dbo].[AdminAreas]
	 WHERE [Country] != 'GRE'
RETURN 0

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2051, 'GetNPTGAdminAreas stored procedure'

GO