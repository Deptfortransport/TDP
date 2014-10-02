-- ***********************************************
-- NAME           : DUP2033_UpdateChangeCatalogue.proc.sql
-- DESCRIPTION    : UpdateChangeCatalogue stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[UpdateChangeCatalogue]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE UpdateChangeCatalogue
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to add or update the change catalogue
-- =============================================
ALTER PROCEDURE [dbo].[UpdateChangeCatalogue]
	@ScriptNumber INT,
	@ScriptDesc VARCHAR(200)
AS

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


-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2033, 'UpdateChangeCatalogue stored procedure'