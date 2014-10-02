-- ***********************************************
-- NAME           : DUP2044_AddGroup.proc.sql
-- DESCRIPTION    : AddGroup stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[AddGroup]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE AddGroup
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to AddGroup
-- =============================================
ALTER PROCEDURE [dbo].[AddGroup]
(
	@Group varchar(100)
)
AS
BEGIN

	DECLARE @GroupId int

	IF NOT EXISTS (SELECT GroupId 
				     FROM tblGroup 
					WHERE [Name] = @Group)
		BEGIN
			SET @GroupId = (SELECT ISNULL(Max(GroupId),0) + 1 FROM tblGroup)

			INSERT INTO tblGroup(GroupId, [Name]) 
				 VALUES (@GroupId, @Group)
		END
END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2044, 'AddGroup stored procedure'

GO