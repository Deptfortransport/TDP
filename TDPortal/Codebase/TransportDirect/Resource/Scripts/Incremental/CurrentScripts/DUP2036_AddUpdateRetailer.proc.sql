-- ***********************************************
-- NAME           : DUP2036_AddUpdateRetailer.proc.sql
-- DESCRIPTION    : AddUpdateRetailer stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddUpdateRetailer]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE AddUpdateRetailer
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
ALTER PROCEDURE [dbo].[AddUpdateRetailer]
	@RetailerId [char](20),
	@Name [char](50),
	@WebsiteURL [char](300),
	@HandoffURL [char](300),
	@DisplayURL [char](100),
	@PhoneNumber [char](50),
	@PhoneNumberDisplay [char](100),
	@AllowsMTH [char](1),
	@IconURL [char](300),
	@SmallIconUrl [char](300),
	@ResourceKey [char](100)
AS
BEGIN
	IF NOT EXISTS (SELECT * 
				     FROM [dbo].[Retailers] 
					WHERE [RetailerId] = @RetailerId) 			   
		BEGIN
			INSERT INTO [dbo].[Retailers] (
				RetailerId, 
				Name, 
				WebsiteURL, 
				HandoffURL, 
				DisplayURL, 
				PhoneNumber, 
				PhoneNumberDisplay, 
				AllowsMTH,
				IconURL,
				SmallIconUrl,
				ResourceKey)
			VALUES (
				@RetailerId, 
				@Name, 
				@WebsiteURL, 
				@HandoffURL, 
				@DisplayURL, 
				@PhoneNumber, 
				@PhoneNumberDisplay, 
				@AllowsMTH,
				@IconURL,
				@SmallIconUrl,
				@ResourceKey)
		END
	ELSE
		BEGIN
			UPDATE [dbo].[Retailers]
			   SET [Name] = @Name,
				   [WebsiteURL] = @WebsiteURL,
				   [HandoffURL] = @HandoffURL,
				   [DisplayURL] = @DisplayURL,
				   [PhoneNumber] = @PhoneNumber,
				   [PhoneNumberDisplay] = @PhoneNumberDisplay,
				   [AllowsMTH] = @AllowsMTH,
				   [IconURL] = @IconURL,
				   [SmallIconUrl] = @SmallIconUrl,
				   [ResourceKey] = @ResourceKey
			 WHERE [RetailerID] = @RetailerId
		END	

END
GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2036, 'AddUpdateRetailer stored procedure'

GO