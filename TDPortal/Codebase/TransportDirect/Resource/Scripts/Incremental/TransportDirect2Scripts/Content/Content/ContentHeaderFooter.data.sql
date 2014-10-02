-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : Content HeaderFooter data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
Go

------------------------------------------------
-- General content, all added to the group 'HeaderFooter'
------------------------------------------------

DECLARE @ThemeId int = 1
DECLARE @Group varchar(100) = 'TDPHeaderFooter'
DECLARE @Collection varchar(100) = 'General'
DECLARE @CultEn varchar(2) = 'en'
DECLARE @CultCy varchar(2) = 'cy'

-- Tidy up first, helps keep content table clean,
-- and ensures this script contains complete control of content for this Group
EXEC DeleteAllGroupContent @Group

------------------------------------------------------------------------------------------------------------------
-- Header
------------------------------------------------------------------------------------------------------------------

-- Sub-section 1 - English
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Header.PrimaryContainer.Html',
''
-- Sub-section 1 - Welsh
EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'Header.PrimaryContainer.Html',
''

-- Sub-section 2 - English
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Header.SecondaryContainer.Html',
''
-- Sub-section 2 - Welsh
EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'Header.SecondaryContainer.Html',
''

------------------------------------------------------------------------------------------------------------------
-- Footer
------------------------------------------------------------------------------------------------------------------

-- English
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Footer.Html',
'
<div class="footer">
	<h2 class="screenReaderOnly">Footer menu</h2>
    <div class="footerTop">
        <div id="colsWrap">
            <div class="colLinks">
                <ul>
					<li><a href="/TDPWeb/Pages/TravelNews.aspx">Travel News</a> </li>
                    <li><a href="/TDPMobile/Default.aspx">Mobile site</a> </li>
				</ul>
            </div>
        </div>
    </div>
    <div class="footerBottom">
		<div class="footerBottomL">
			<div class="footerSocial">
				<span>Follow Us On:</span>
				<ul>
					<li><a class="external" href="http://en-gb.facebook.com/pages/Transport-Direct-Journey-Planner/174434289241225">Facebook</a></li>
				</ul>
			</div>
		</div>
	</div>
</div>
'

-- Welsh
EXEC AddContent @ThemeId, @Group, @CultCy, @Collection, 'Footer.Html',
'
<div class="footer">
	<h2 class="screenReaderOnly">Footer menu</h2>
    <div class="footerTop">
        <div id="colsWrap">
            <div class="colLinks">
                <ul>
					<li><a href="/TDPWeb/Pages/TravelNews.aspx">Travel News</a> </li>
                    <li><a href="/TDPMobile/Default.aspx">Mobile site</a> </li>
				</ul>
            </div>
        </div>
    </div>
    <div class="footerBottom">
		<div class="footerBottomL">
			<div class="footerSocial">
				<span>Follow Us On:</span>
				<ul>
					<li><a class="external" href="http://en-gb.facebook.com/pages/Transport-Direct-Journey-Planner/174434289241225">Facebook</a></li>
				</ul>
			</div>
		</div>
	</div>
</div>
'
GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'Content HeaderFooter data'

GO