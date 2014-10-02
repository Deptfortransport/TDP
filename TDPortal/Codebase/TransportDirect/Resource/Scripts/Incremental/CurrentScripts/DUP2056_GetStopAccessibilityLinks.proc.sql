-- ***********************************************
-- NAME           : DUP2056_GetStopAccessibilityLinks.proc.sql
-- DESCRIPTION    : GetStopAccessibilityLinks stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetStopAccessibilityLinks]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE GetStopAccessibilityLinks
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to GetStopAccessibilityLinks
-- =============================================
ALTER PROCEDURE [dbo].[GetStopAccessibilityLinks]
	
AS
BEGIN

	SELECT  [StopNaPTAN],
			[StopOperator],
			[LinkUrl],
			[WEFDate],
			[WEUDate]
	  FROM  [dbo].[StopAccessibilityLinks]

END
GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2056, 'GetStopAccessibilityLinks stored procedure'

GO