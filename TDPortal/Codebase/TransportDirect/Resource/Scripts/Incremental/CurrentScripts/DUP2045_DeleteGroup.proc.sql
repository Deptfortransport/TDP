-- ***********************************************
-- NAME           : DUP2045_DeleteGroup.proc.sql
-- DESCRIPTION    : DeleteGroup stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[DeleteGroup]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE DeleteGroup
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to DeleteGroup
-- =============================================
ALTER PROCEDURE [dbo].[DeleteGroup]
(
	@Group varchar(100)
)
AS
BEGIN

	DECLARE @GroupId int

	--Look up the GroupId:
    SELECT @GroupId = GroupId 
      FROM [dbo].tblGroup
     WHERE [Name] = @Group

	IF @GroupId IS NOT NULL
		BEGIN
			-- Check if there is any Content using this group
			IF EXISTS (SELECT * 
						 FROM [dbo].tblContent
						WHERE GroupId = @GroupId)
				BEGIN
					RAISERROR ('Content exists for group %s, unable to delete group', 16, 1, @Group)
					RETURN 0
				END

			-- OK to delete group
			DELETE FROM [dbo].tblGroup
				  WHERE [Name] = @Group

		END

END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2045, 'DeleteGroup stored procedure'

GO