-- ****************************************************************
-- NAME 		: DUP2014_ServiceDetailRequestOperatorCodes_Database_Objects.sql
-- DESCRIPTION 	: Creates table and stored procedure for CCNxxx operator code translation workaround enhancment
-- AUTHOR		: Rich Broddle
-- DATE			: 19/03/2013
-- ****************************************************************

USE [TransientPortal]
GO

/****** Object:  Table [dbo].[ServiceDetailRequestOperatorCodes]    Script Date: 03/19/2013 13:50:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ServiceDetailRequestOperatorCodes]') AND type in (N'U'))
DROP TABLE [dbo].[ServiceDetailRequestOperatorCodes]
GO

USE [TransientPortal]
GO

/****** Object:  Table [dbo].[ServiceDetailRequestOperatorCodes]    Script Date: 03/19/2013 13:50:36 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ServiceDetailRequestOperatorCodes](
	[OperatorCode] [varchar](10) NOT NULL,
	[RequestOperatorCode] [varchar](10) NOT NULL
 CONSTRAINT [PK_ServiceDetailRequestOperatorCodes] PRIMARY KEY CLUSTERED 
(
	[OperatorCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON, FILLFACTOR = 80) ON [PRIMARY]
) ON [PRIMARY]

GO



-----------------------------------------------
-- GetServiceDetailRequestOperatorCodes Stored Procedure
-----------------------------------------------

IF NOT EXISTS (   SELECT * FROM INFORMATION_SCHEMA.ROUTINES
              WHERE SPECIFIC_CATALOG = 'TransientPortal' 
              AND ROUTINE_NAME = 'GetServiceDetailRequestOperatorCodes' )
	BEGIN
		EXEC ('CREATE PROCEDURE [dbo].[GetServiceDetailRequestOperatorCodes] AS BEGIN SET NOCOUNT ON END')
	END
GO

ALTER PROCEDURE [dbo].[GetServiceDetailRequestOperatorCodes]
AS
BEGIN
	SELECT [OperatorCode],
		[RequestOperatorCode] 
	FROM 	[ServiceDetailRequestOperatorCodes]
	ORDER BY [OperatorCode]

END
GO

-- Grant permissions - will only work for relevant users as only they
-- will exist
GRANT  EXECUTE  ON [dbo].[GetServiceDetailRequestOperatorCodes]  TO [BBPTDPSIW\aspuser]
GRANT  EXECUTE  ON [dbo].[GetServiceDetailRequestOperatorCodes]  TO [BBPTDPW\aspuser]
GRANT  EXECUTE  ON [dbo].[GetServiceDetailRequestOperatorCodes]  TO [ACPTDPW\aspuser]
GRANT  EXECUTE  ON [dbo].[GetServiceDetailRequestOperatorCodes]  TO [BBPTDPSIS\aspuser]
GRANT  EXECUTE  ON [dbo].[GetServiceDetailRequestOperatorCodes]  TO [BBPTDPS\aspuser]
GRANT  EXECUTE  ON [dbo].[GetServiceDetailRequestOperatorCodes]  TO [ACPTDPS\aspuser]
GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2014
SET @ScriptDesc = 'Create table and stored procedure for CCNxxx operator code translation workaround enhancment'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc)
  END
GO