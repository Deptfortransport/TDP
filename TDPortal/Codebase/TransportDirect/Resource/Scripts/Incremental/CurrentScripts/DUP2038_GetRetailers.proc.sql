-- ***********************************************
-- NAME           : DUP2038_GetRetailers.proc.sql
-- DESCRIPTION    : GetRetailers stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetRetailers]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE GetRetailers
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure GetRetailers
-- =============================================
ALTER PROCEDURE [dbo].[GetRetailers]
AS
BEGIN
	SELECT [RetailerId],
		   [Name],
		   [WebsiteURL],
		   [HandoffURL],
		   [DisplayURL],
		   [PhoneNumber],
		   [PhoneNumberDisplay],
		   [AllowsMTH],
		   [IconURL],
		   [SmallIconUrl],
		   [ResourceKey]
	  FROM [dbo].[Retailers]
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2038, 'GetRetailers stored procedure'

GO