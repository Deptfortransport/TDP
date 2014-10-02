-- ***********************************************
-- NAME           : DUP2042_GetChangeTable.proc.sql
-- DESCRIPTION    : GetChangeTable stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetChangeTable]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE GetChangeTable
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to GetChangeTable
-- =============================================
ALTER PROCEDURE [dbo].[GetChangeTable]
	
AS
BEGIN
	  SELECT [Table], [Version] 
	    FROM [dbo].[ChangeNotification]
	ORDER BY [Table]
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2042, 'GetChangeTable stored procedure'

GO