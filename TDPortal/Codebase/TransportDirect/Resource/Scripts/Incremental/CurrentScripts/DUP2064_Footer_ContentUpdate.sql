-- ***********************************************
-- NAME 		: DUP2064_Footer_ContentUpdate.sql
-- DESCRIPTION 	: Footer content update
-- AUTHOR		: Mitesh Modi
-- DATE			: 06 Aug 2013
-- ************************************************

USE [Content]
GO
------------------------------------------------

-- Mobile site link in footer
EXEC AddtblContent 1, 1, 'langStrings', 'FooterControl1.MobileSite',
	'Mobile',
	'Mobile'


-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2064, 'Footer content update'

GO