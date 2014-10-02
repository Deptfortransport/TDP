-- ***********************************************
-- NAME           : DUP2035_AddUpdateProperty.proc.sql
-- DESCRIPTION    : AddUpdateProperty stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddUpdateProperty]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE AddUpdateProperty
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to add or update a property
-- =============================================
ALTER PROCEDURE [dbo].[AddUpdateProperty]
	@pName varchar(255), 
	@pValue varchar(2000),
	@AID varchar(50),
	@GID varchar(50),
	@PartnerId int,
	@ThemeId int
AS
	IF not exists (select top 1 * from properties 
								 where pName = @pName 
								   and AID = @AID 
								   and GID = @GID
								   and PartnerId = @PartnerId
								   and ThemeId = @ThemeId)
	BEGIN
		insert into properties values (
			@pName, 
			@pValue, 
			@AID, 
			@GID,
			@PartnerId,
			@ThemeId)
	END
	ELSE
	BEGIN
		update properties 
		set pValue = @pValue
		where pName = @pName
		  and AID = @AID
		  and GID = @GID
		  and PartnerId = @PartnerId
		  and ThemeId = @ThemeId
	END	

RETURN 0

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2035, 'AddUpdateProperty stored procedure'

GO