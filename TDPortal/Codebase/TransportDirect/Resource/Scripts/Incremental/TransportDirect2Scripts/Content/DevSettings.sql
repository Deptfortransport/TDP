-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : Dev Content data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [Content]
Go
-- =============================================
-- SCRIPT TO UPDATE DEV MACHINE SPECIFIC SETTINGS - OVERWRITES PRODUCTION SETTINGS
-- =============================================

DECLARE @ThemeId int = 1
DECLARE @Group varchar(100) = 'TDPAnalytics'
DECLARE @Collection varchar(100) = 'Default' -- This is set the PageId."Default" page, allows custom tags for pages
DECLARE @CultEn varchar(2) = 'en'
DECLARE @CultFr varchar(2) = 'fr'

-- Live analytics tag
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Analytics.Tag.Host', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Analytics.Tag.Tracker', ''

-- Live adverts tag
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Adverts.Tag.Service', ''
EXEC AddContent @ThemeId, @Group, @CultEn, @Collection, 'Adverts.Tag.Placeholders', ''

GO