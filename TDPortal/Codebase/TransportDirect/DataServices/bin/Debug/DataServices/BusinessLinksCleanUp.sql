--copy items back to Business links table from temp table.
use PermanentPortal
GO

if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[temp_BusinessLinksTemplates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	if exists (select * from dbo.sysobjects where id = object_id(N'[dbo].[BusinessLinkTemplates]') and OBJECTPROPERTY(id, N'IsUserTable') = 1)
	BEGIN
		DELETE  FROM BusinessLinkTemplates
		
		
		INSERT INTO BusinessLinkTemplates(BusinessLinkTemplatesId, NameResourceId, ImageUrl,HTMLScript)
		SELECT BusinessLinkTemplatesId, NameResourceId, ImageUrl, HTMLScript FROM temp_BusinessLinksTemplates
		
		
		DROP TABLE temp_BusinessLinksTemplates
		
	END
GO