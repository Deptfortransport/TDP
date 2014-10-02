-- ***********************************************
-- NAME           : DUP2037_AddUpdateRetailerLookup.proc.sql
-- DESCRIPTION    : AddUpdateRetailerLookup stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddUpdateRetailerLookup]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE AddUpdateRetailerLookup
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to add or update a retailer
-- =============================================
ALTER PROCEDURE [dbo].[AddUpdateRetailerLookup]
	@OperatorCode [char](10),
	@Mode [char](10),
	@RetailerId [char](20),
	@PartnerId [char](20),
	@ThemeId [char](20)
AS
BEGIN
	IF NOT EXISTS (SELECT * 
				     FROM [dbo].[RetailerLookup] 
					WHERE [RetailerId] = @RetailerId 
					  AND [Mode] = @Mode
					  AND [OperatorCode] = @OperatorCode
					  AND [PartnerId] = @PartnerId
					  AND [ThemeId] = @ThemeId) 			   
		BEGIN
			INSERT INTO [dbo].[RetailerLookup] ([OperatorCode], [Mode], [RetailerId], [PartnerId], [ThemeId])
				 VALUES (@OperatorCode, @Mode, @RetailerId, @PartnerId, @ThemeId)
		END
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2037, 'AddUpdateRetailerLookup stored procedure'

GO