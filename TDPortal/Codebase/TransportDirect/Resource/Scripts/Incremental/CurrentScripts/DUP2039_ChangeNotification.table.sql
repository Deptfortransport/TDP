-- ***********************************************
-- NAME           : DUP2039_ChangeNotification.table.sql
-- DESCRIPTION    : ChangeNotification table
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
GO

-- Drop existing table (OK to drop, this is first time table is being created in TDP)
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ChangeNotification]') AND type in (N'U'))
	DROP TABLE [dbo].[ChangeNotification]
GO

---- Create table
IF NOT EXISTS (select * from dbo.sysobjects where id = object_id(N'[dbo].[ChangeNotification]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
BEGIN
	CREATE TABLE [dbo].[ChangeNotification]
	(
		[Version] [int] NOT NULL,
		[Table] [varchar](100) NOT NULL,
		CONSTRAINT [PK_ChangeNotification] PRIMARY KEY CLUSTERED 
		(
			[Table] ASC
		)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2039, 'ChangeNotification table'

GO