--Populate the Business links table
use PermanentPortal
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BusinessLinkTemplates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	
	
	INSERT INTO BusinessLinkTemplates(BusinessLinkTemplatesId, NameResourceId, ImageUrl,
	HTMLScript)
	VALUES(0, 'Test1', 'TestURL1', 'Test HTML1')
	GO
	
	INSERT INTO BusinessLinkTemplates(BusinessLinkTemplatesId, NameResourceId, ImageUrl,
	HTMLScript)
	VALUES(1, 'Test2', 'TestURL2', 'Test HTML2')
	GO
	
	INSERT INTO BusinessLinkTemplates(BusinessLinkTemplatesId, NameResourceId, ImageUrl,
	HTMLScript)
	VALUES(2, 'Test3', 'TestURL3', 'Test HTML3')
	GO