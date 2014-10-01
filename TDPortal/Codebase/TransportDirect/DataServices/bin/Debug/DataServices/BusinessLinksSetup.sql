-- Delete temp tables if they exist
use PermanentPortal
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[temp_BusinessLinksTemplates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
drop table [dbo].[temp_BusinessLinksTemplates]
GO

--Create temporary table
SELECT * INTO temp_BusinessLinksTemplates FROM BusinessLinkTemplates
go