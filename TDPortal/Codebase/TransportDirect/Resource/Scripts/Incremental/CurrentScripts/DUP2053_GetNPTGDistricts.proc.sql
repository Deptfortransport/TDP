-- ***********************************************
-- NAME           : DUP2053_GetNPTGDistricts.proc.sql
-- DESCRIPTION    : GetNPTGDistricts stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetNPTGDistricts]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE GetNPTGDistricts
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to GetNPTGDistricts
-- =============================================
ALTER PROCEDURE [dbo].[GetNPTGDistricts]
AS
	SELECT [DistrictCode]
      ,[DistrictName]
      ,[AdministrativeAreaCode]
	FROM [dbo].[Districts]
RETURN 0

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2053, 'GetNPTGDistricts stored procedure'

GO