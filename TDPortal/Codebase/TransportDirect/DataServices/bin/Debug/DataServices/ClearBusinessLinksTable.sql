--delete data in Business Links Table
-- Delete temp tables if they exist
use PermanentPortal
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BusinessLinkTemplates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
DELETE FROM BusinessLinkTemplates
GO