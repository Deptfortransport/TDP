-- ***********************************************
-- NAME           : DUP2049_GetContent.proc.sql
-- DESCRIPTION    : GetContent stored procedure
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
GO

if not exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[GetContent]') and OBJECTPROPERTY(id, N'IsProcedure') = 1)
BEGIN
    EXEC ('
        CREATE PROCEDURE GetContent
        AS
        BEGIN 
            SET NOCOUNT ON 
        END
    ')

END
GO

-- =============================================
-- Stored procedure to GetContent (used by TDPMobile)
-- =============================================
ALTER PROCEDURE [dbo].[GetContent] 
(
    @Group    varchar(100),
    @Language char(5)
)
AS
BEGIN

	-- Parse language into 5 char that the main get content is expecting
	IF @Language NOT IN ('en-GB', 'cy-GB')
	BEGIN
		IF @Language = 'en'
			SET @Language = 'en-GB'
		ELSE IF @Language = 'cy'
			SET @Language = 'cy-GB'
	END
	
	-- Get Default theme, hard coded same as GetDefaultThemeId
	DECLARE @ThemeId varchar(100) = 'TransportDirect'

	-- Call main get content script
	EXEC [dbo].[sprGetContent] @ThemeId, @Group, @Language

END

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2049, 'GetContent stored procedure'

GO