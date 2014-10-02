-- ***********************************************
-- NAME           : DUP2047_DeleteContent.proc.sql
-- DESCRIPTION    : DeleteContent stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
GO

-- Drop the old delete stored procedure
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeletetblContent]') AND type in (N'P', N'PC'))
	DROP PROCEDURE [dbo].[DeletetblContent]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeleteContent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE DeleteContent
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to DeleteContent
-- =============================================
ALTER PROCEDURE [dbo].[DeleteContent]
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

	IF EXISTS (SELECT * FROM [dbo].tblContent 
					   WHERE tblContent.PropertyName = @PropertyName 
						 AND tblContent.ControlName = @ControlName 
						 AND tblContent.ThemeId = @ThemeId 
						 AND tblContent.GroupId = @GroupId )
		BEGIN
			DELETE FROM [dbo].tblContent
				  WHERE tblContent.PropertyName = @PropertyName 
						 AND tblContent.ControlName = @ControlName 
						 AND tblContent.ThemeId = @ThemeId 
						 AND tblContent.GroupId = @GroupId
				  
			EXEC DeleteContentOverride @ThemeId, @Group, @ControlName, @PropertyName
		END
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2047, 'DeleteContent stored procedure'

GO