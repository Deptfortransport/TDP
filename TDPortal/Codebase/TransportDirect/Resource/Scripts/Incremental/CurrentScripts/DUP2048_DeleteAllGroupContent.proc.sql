-- ***********************************************
-- NAME           : DUP2048_DeleteAllGroupContent.proc.sql
-- DESCRIPTION    : DeleteAllGroupContent stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeleteAllGroupContent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE DeleteAllGroupContent
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to DeleteAllGroupContent
-- =============================================
ALTER PROCEDURE [dbo].[DeleteAllGroupContent]
	@Group varchar(100)
AS
BEGIN

	-- Deletes all content for the Group specified, 
	-- but does not delete the group

	DECLARE @GroupId int
   
    --Look up the GroupId:
    SELECT @GroupId = GroupId 
      FROM [dbo].tblGroup
     WHERE [Name] = @Group

	IF @GroupId IS NOT NULL
    	BEGIN

			-- Delete from ContentOverride
			IF EXISTS (SELECT * FROM [dbo].tblContentOverride 
					   WHERE tblContentOverride.GroupId = @GroupId )
			BEGIN
				DELETE FROM [dbo].tblContentOverride
					  WHERE tblContentOverride.GroupId = @GroupId
			END

			-- Delete from Content
			IF EXISTS (SELECT * FROM [dbo].tblContent 
					   WHERE tblContent.GroupId = @GroupId )
			BEGIN
				DELETE FROM [dbo].tblContent
				      WHERE tblContent.GroupId = @GroupId
			END

		END
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2048, 'DeleteAllGroupContent stored procedure'

GO