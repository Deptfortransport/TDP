-- ***********************************************
-- NAME           : DUP2076_DepartureBoardLinks_Update.sql
-- DESCRIPTION    : DepartureBoard external links update
-- AUTHOR         : Mitesh Modi
-- DATE           : 29 Aug 2013
-- ***********************************************

USE [TransientPortal]
GO

-----------------------------------------------------------------------------------------
-- Bus Departure Boards
-----------------------------------------------------------------------------------------

-- Tidyup bus departure boards
DELETE FROM ExternalLinks 
WHERE Id like 'DepartureBoard.Bus.%'

-- Add links
EXEC [AddExternalLink] 'DepartureBoard.Bus.1', 'http://www.nextbuses.mobi', 'http://www.nextbuses.mobi', 'Departure Board - NextBuses', 'http://www.nextbuses.mobi'

-- Old links, retained for reference:
--EXEC [AddExternalLink] 'DepartureBoard.Bus.1', 'http://www.lancashire.gov.uk/corporate/web/view.asp?siteid=4404&amp;pageid=19915&amp;e=e', 'http://www.lancashire.gov.uk/corporate/web/view.asp?siteid=4404&amp;pageid=19915&amp;e=e', 'Departure Board - Transport for Lancashire', 'http://www.lancashire.gov.uk/corporate/web/view.asp?siteid=4404&amp;pageid=19915&amp;e=e'
--EXEC [AddExternalLink] 'DepartureBoard.Bus.2', 'http://www.acis.uk.com/ACIS-Live.aspx', 'http://www.acis.uk.com/ACIS-Live.aspx', 'Departure Board - Acis Live', 'http://www.acis.uk.com/ACIS-Live.aspx'
--EXEC [AddExternalLink] 'DepartureBoard.Bus.3', 'http://www.star-trak.co.uk/', 'http://www.star-trak.co.uk/', 'Departure Board - Star Trak', 'http://www.star-trak.co.uk/'
--EXEC [AddExternalLink] 'DepartureBoard.Bus.4', 'http://www.citytransport.org.uk/eMerge.html', 'http://www.citytransport.org.uk/eMerge.html', 'Departure Board - City Transport', 'http://www.citytransport.org.uk/eMerge.html'
--EXEC [AddExternalLink] 'DepartureBoard.Bus.5', 'http://www.help2travel.co.uk/mattisse/bus-realtime.htm', 'http://www.help2travel.co.uk/mattisse/bus-realtime.htm', 'Departure Board - Help2Travel', 'http://www.help2travel.co.uk/mattisse/bus-realtime'

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2076, 'DepartureBoard external links update'

GO