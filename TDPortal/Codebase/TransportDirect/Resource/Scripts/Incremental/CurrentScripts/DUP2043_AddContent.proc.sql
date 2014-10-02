-- ***********************************************
-- NAME           : DUP2043_AddContent.proc.sql
-- DESCRIPTION    : AddContent stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddContent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE AddContent
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to AddContent
-- =============================================
ALTER PROCEDURE [dbo].[AddContent]
(
	@ThemeId int,
	@Group varchar(100),
	@Language char(2),
	@ControlName varchar(60),
	@PropertyName varchar(100),
	@ContentValue text
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

	IF EXISTS (SELECT * FROM [dbo].tblContent 
					   WHERE tblContent.PropertyName = @PropertyName 
						 AND tblContent.ControlName = @ControlName 
						 AND tblContent.ThemeId = @ThemeId 
						 AND tblContent.GroupId = @GroupId )			 
		BEGIN
		
			-- English
			IF (@Language = 'en')
				BEGIN
					UPDATE [dbo].tblContent
					   SET [Value-En] = @ContentValue
					 WHERE tblContent.PropertyName = @propertyName 
					   AND tblContent.ControlName = @ControlName 
					   AND tblContent.ThemeId = @ThemeId 
					   AND tblContent.GroupId = @GroupId	 
				END
			
			-- Welsh
			IF (@Language = 'cy')
				BEGIN
					UPDATE [dbo].tblContent
					   SET [Value-Cy] = @ContentValue
					 WHERE tblContent.PropertyName = @propertyName 
					   AND tblContent.ControlName = @ControlName 
					   AND tblContent.ThemeId = @ThemeId 
					   AND tblContent.GroupId = @GroupId 
				END	
		END
	ELSE
	   BEGIN
			-- Doesnt exist so update both english and welsh as the same
			INSERT INTO [dbo].tblContent (ThemeId, GroupId, ControlName, PropertyName, [Value-En], [Value-Cy])
        	VALUES (@ThemeId, @GroupId, @ControlName, @PropertyName, @ContentValue, @ContentValue)
	   END

END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2043, 'AddContent stored procedure'

GO