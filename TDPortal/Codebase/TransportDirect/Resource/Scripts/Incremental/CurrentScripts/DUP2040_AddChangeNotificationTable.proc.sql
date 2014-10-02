-- ***********************************************
-- NAME           : DUP2040_AddChangeNotificationTable.proc.sql
-- DESCRIPTION    : AddChangeNotificationTable stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddChangeNotificationTable]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE AddChangeNotificationTable
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to add change notification table
-- =============================================
ALTER PROCEDURE [dbo].[AddChangeNotificationTable]
	@table varchar(100) 
AS
BEGIN

	-- Add tables that didn't previously exist
	IF NOT EXISTS (SELECT * FROM [dbo].[ChangeNotification]
						   WHERE [table] = @table)
		BEGIN
			INSERT INTO [dbo].[ChangeNotification] ([table], [version])
				 VALUES (@table, 1)
		END
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2040, 'AddChangeNotificationTable stored procedure'

GO