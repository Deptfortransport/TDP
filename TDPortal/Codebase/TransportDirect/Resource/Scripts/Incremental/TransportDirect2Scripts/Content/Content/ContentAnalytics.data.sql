-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : Content Analytics tag data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
Go

------------------------------------------------
-- Analytics and Adverts content, all added to the group 'Analytics'
------------------------------------------------

DECLARE @ThemeId int = 1
DECLARE @Group varchar(100) = 'TDPAnalytics'
DECLARE @Collection varchar(100) = 'Default' -- This is set the PageId."Default" page, allows custom tags for pages
DECLARE @CultEn varchar(2) = 'en'
DECLARE @CultCy varchar(2) = 'cy'

-- Tidy up first, helps keep content table clean,
-- and ensures this script contains complete control of content for this Group
EXEC DeleteAllGroupContent @Group

-- Example format for Analytics tag content:
-- group, language, collection(PageId), resourceKey, value

-- DON'T FORGET TO REPLACE ALL ' WITH '' IN THE TAG


-- Live analytics tag
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Analytics.Tag.Host', 
'<script src="//tags.transportdirect.info/e/cognesiaservice.min.js"></script>'

EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Analytics.Tag.Tracker', 
'<script type="text/javascript">
          iCognesia.Init("1896", "tags.transportdirect.info");
          iCognesia.ClickMapOn();
</script>'


-- Live adverts tag
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Adverts.Tag.Service', 
''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Adverts.Tag.Placeholders', 
''

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'Content Google Analytics tag data'

GO