-- ***********************************************
-- NAME           : DUP2046_DeleteContentOverride.proc.sql
-- DESCRIPTION    : DeleteContentOverride stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeleteContentOverride]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE DeleteContentOverride
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to DeleteContentOverride
-- =============================================
ALTER PROCEDURE [dbo].[DeleteContentOverride]
(
	@ThemeId int,
	@Group varchar(100),
	@ControlName varchar(60),
	@PropertyName varchar(100)
)
AS
BEGIN

	DECLARE @GroupId int
   
    --Look up the GroupId:
    SELECT @GroupId = GroupId 
      FROM [dbo].tblGroup
     WHERE [Name] = @Group

    --Check the group:
    IF @GroupId IS NULL
    	BEGIN
    		RAISERROR ('%s is an invalid group', 16, 1, @Group)
			RETURN 0
    	END

	IF EXISTS (SELECT * FROM [dbo].tblContentOverride 
					   WHERE tblContentOverride.PropertyName = @PropertyName 
						 AND tblContentOverride.ControlName = @ControlName 
						 AND tblContentOverride.ThemeId = @ThemeId 
						 AND tblContentOverride.GroupId = @GroupId )
		BEGIN
			DELETE FROM [dbo].tblContentOverride
				  WHERE tblContentOverride.PropertyName = @PropertyName 
				    AND tblContentOverride.ControlName = @ControlName 
					AND tblContentOverride.ThemeId = @ThemeId 
					AND tblContentOverride.GroupId = @GroupId
		END
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2046, 'DeleteContentOverride stored procedure'

GO