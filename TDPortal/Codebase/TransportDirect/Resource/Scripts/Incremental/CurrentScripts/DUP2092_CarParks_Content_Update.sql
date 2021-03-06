-- ***********************************************
-- NAME 		: DUP2092_CarParks_Content_Update.sql
-- DESCRIPTION 	: Car Parks content update for error no parks found
-- AUTHOR		: Mitesh Modi
-- DATE			: 13 Jan 2014
-- ************************************************

USE [Content]
GO
------------------------------------------------

EXEC AddContent 1, 'General', 'en', 'langStrings', 'FindCarParkResult.ErrorMessage.CarParkMode',
'Sorry we are currently unable to obtain any car parks using the details you have  entered. <a href="{0}" target="_blank"><b>Please click here to see a comprehensive list of our sources of car park information (PDF)</b></a>.
<br /><br />If, having checked this list, you believe there are public car parks that should have been found by this search please help us to investigate this further by completing a feedback form (do this by clicking "Contact us" at the bottom of the page).
'

EXEC AddContent 1, 'General', 'cy', 'langStrings', 'FindCarParkResult.ErrorMessage.CarParkMode',
'Mae’n ddrwg gennym, ni allwn ddarganfod unrhyw feysydd parcio ar hyn o bryd gan ddefnyddio’r manylion a nodwyd gennych. <a href="{0}" target="_blank"><b>Cliciwch yma i weld rhestr gynhwysfawr o’n ffynonellau gwybodaeth meysydd parcio (PDF)</b></a>.
<br /><br />Ar ôl darllen y rhestr hon, os gredwch y dylai fod y chwiliad hwn wedi darganfod meysydd parcio cyhoeddus, helpwch ni i ymchwilio ymhellach i hyn drwy lanw ffurflen adborth (gwnewch hyn drwy glicio "Cysylltu â ni" ar waelod y dudalen).
'

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2092, 'Car Parks content update for error no parks found'

GO