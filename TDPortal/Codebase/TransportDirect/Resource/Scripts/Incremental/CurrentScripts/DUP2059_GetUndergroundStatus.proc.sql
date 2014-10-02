-- ***********************************************
-- NAME           : DUP2059_GetUndergroundStatus.proc.sql
-- DESCRIPTION    : GetUndergroundStatus stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [TransientPortal]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetUndergroundStatus]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE GetUndergroundStatus
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to GetUndergroundStatus
-- =============================================
ALTER PROCEDURE [dbo].[GetUndergroundStatus]
AS
BEGIN

	 SELECT [LineStatusId],
			[LineStatusDetails],
			[LineId],
			[LineName],
			[StatusId],
			[StatusDescription],
			[StatusIsActive],
			[StatusCssClass] 
	   FROM	[dbo].[UndergroundStatus]
   ORDER BY [LineName]

END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2059, 'GetUndergroundStatus stored procedure'

GO