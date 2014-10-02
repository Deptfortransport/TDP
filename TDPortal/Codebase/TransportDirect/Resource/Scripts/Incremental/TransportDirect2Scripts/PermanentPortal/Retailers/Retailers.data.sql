-- ***********************************************
-- NAME           : 
-- DESCRIPTION    : Retailers data
-- AUTHOR         : Mitesh Modi
-- DATE           : 26 Jun 2013
-- ***********************************************
USE [PermanentPortal]
GO

-----------------------------------
-- The configuration property "Retail.Retailers.ShowTestRetailers.Switch" is used to display the test retailers
-----------------------------------

-- Tidy up first, helps keep retailer tables clean,
-- and ensures this script contains complete control of the retailers in use
DELETE FROM [dbo].[RetailerLookup]
DELETE FROM [dbo].[Retailers]

----------------------------------------------------------------------
----------------------------------------------------------------------
-- Add Retailers 
----------------------------------------------------------------------
----------------------------------------------------------------------
-- EXEC AddUpdateRetailer 
-- @RetailerId, @Name, @WebsiteURL, @HandoffURL, @DisplayURL, 
-- @PhoneNumber, @PhoneNumberDisplay, @AllowsMTH, 
-- @IconURL, @SmallIconUrl, @ResourceKey

-----------------------------------
-- TDP retailer sites
-----------------------------------
EXEC AddUpdateRetailer 'FGW', 'First Great Western', 'http://www.firstgreatwestern.co.uk/home/index.php ', NULL, 'www.firstgreatwestern.co.uk', '08457 000 125', '08457 000 125', 'N', '/Web/images/gifs/SoftContent/FGW.gif',NULL,NULL
EXEC AddUpdateRetailer 'GONORTHEAST', 'GONORTHEAST', NULL, NULL, NULL, 'N/A', 'N/A', 'N', NULL,NULL,NULL
--EXEC AddUpdateRetailer 'MTT', 'MyTrainTicket', 'http://www.mytrainticket.co.uk', 'http://ticketing.mytrainticket.co.uk/transportdirect/', 'http://www.mytrainticket.co.uk', '0845 1280 480', '0845 1280 480', 'N', '/Web/images/gifs/SoftContent/mtt_lg.gif', '/Web/images/gifs/SoftContent/mtt_sm.gif',NULL
EXEC AddUpdateRetailer 'NEX', 'National Express', 'http://www.nationalexpress.com', 'http://www.nationalexpress.com/tdlive.cfm', 'www.nationalexpress.com', '08705 80 80 80', '08705 80 80 80', 'N', '/Web/images/gifs/SoftContent/NationalExpress.gif', '/Web/images/gifs/SoftContent/NationalExpress_small.gif',NULL
EXEC AddUpdateRetailer 'NEXEC', 'East Coast', 'http://www.eastcoast.co.uk', 'http://tickets.eastcoast.co.uk/ec/en/landing/TDP', 'http://www.eastcoast.co.uk/', '08457225225', '08457225225', 'N', '/Web/images/gifs/SoftContent/eastcoast_lg.gif', '/Web/images/gifs/SoftContent/eastcoast_sm.gif',NULL
EXEC AddUpdateRetailer 'ONE', 'Greater Anglia', NULL, NULL, NULL, '0845 600 7245', '0845 600 7245', 'N', NULL,NULL,NULL
EXEC AddUpdateRetailer 'SCL', 'Scottish Citylink', 'http://www.citylink.co.uk', 'http://www.citylinkonlinesales.co.uk/CitylinkWeb?redirect=TransportDirectController&mn=1&op=4', 'www.citylink.co.uk', '0871 266 3333', '0871 266 3333', 'N', '/Web/images/gifs/SoftContent/citylink.gif', '/Web/images/gifs/SoftContent/citylink_small.gif',NULL
EXEC AddUpdateRetailer 'SCLOff', 'Scottish Citylink', 'http://www.citylink.co.uk', NULL, 'www.citylink.co.uk', '0871 266 3333', '0871 266 3333', 'N', '/Web/images/gifs/SoftContent/citylink.gif',NULL,NULL
EXEC AddUpdateRetailer 'SCOTRAIL', 'ScotRail', 'http://www.firstgroup.com/scotrail/index.php', NULL, 'www.firstgroup.com/scotrail', '08457 55 00 33 ', '08457 55 00 33 ', 'N', '/Web/images/gifs/SoftContent/Firstscotrail.gif',NULL,NULL
EXEC AddUpdateRetailer 'TPE', 'TransPennine Express', 'http://www.tpexpress.co.uk', NULL, 'www.tpexpress.co.uk', '0845 678 6974', '0845 678 6974', 'N', '/Web/images/gifs/SoftContent/transpennine.gif',NULL,NULL
EXEC AddUpdateRetailer 'TRAINLINE', 'The Trainline.com', 'http://www.thetrainline.com', 'http://www.thetrainline.com/buytickets/datapassedin.aspx', 'www.thetrainline.com', '08704 11 11 11', '08704 11 11 11', 'N', '/Web/images/gifs/SoftContent/TheTrainLine.gif', '/Web/images/gifs/SoftContent/TheTrainline_small.gif',NULL
EXEC AddUpdateRetailer 'VIRGIN', 'Virgin Trains Call Centre', NULL, NULL, NULL, '0871 977 4222', '0871 977 4222', 'N', NULL,NULL,NULL
EXEC AddUpdateRetailer 'VIRGIN-B', 'Virgin Trains Business Express', NULL, NULL, NULL, '0845 600 61 62', '0845 600 61 62', 'N', NULL,NULL,NULL

-----------------------------------
-- TDP Mobile retailers
-----------------------------------
--EXEC AddUpdateRetailer 'DMT', 'DirectManagedTransport', 'http://www.firstgroup.com', 'http://www.firstgroupgamestravel.com/direct-coaching', '', '+448449212012', '0844 921 2012', 'N', '', '', 'Retailers.Retailer.Coach.DirectManagedTransport'
--EXEC AddUpdateRetailer 'NR', 'NationalRail', 'http://www.nationalrail.co.uk/', 'http://tickets.nationalrailgamestravel.co.uk/sjp/sjplanding.aspx', '', '+448446932898', '0844 693 2898', 'N', '', '', 'Retailers.Retailer.Rail.NationalRail'
--EXEC AddUpdateRetailer 'CC', 'CityCruises', 'http://www.citycruises.co.uk', 'http://www.citycruisesgamestravel.co.uk', '', '+442077400400', '0207 7400 400', 'N', '', '', 'Retailers.Retailer.Ferry.CityCruises'
--EXEC AddUpdateRetailer 'TC', 'ThamesClipper', 'http://www.thamesclippers.com', 'https://booking.thamesclippers.com/gamestravel', '', '+442070012200', '0207 001 2200', 'N', '', '', 'Retailers.Retailer.Ferry.ThamesClipper'


-- Test retailer sites (these must be prefixed with TEST and Property[Retail.Retailers.ShowTestRetailers.Switch] in configuration be true to display retailers)
--EXEC AddUpdateRetailer 'TEST_DMT', 'DirectManagedTransport', 'http://www.firstgroup.com', 'http://dmt.spideronline.co.uk/direct-coaching', '', '', '', 'N', '', '', 'Retailers.Retailer.Coach.DirectManagedTransport.Test'
--EXEC AddUpdateRetailer 'TEST_NR', 'NationalRail', 'http://www.nationalrail.co.uk/', 'http://tickets.nationalrailgamestravel.co.uk', '', '', '', 'N', '', '', 'Retailers.Retailer.Rail.NationalRail.Test'
--EXEC AddUpdateRetailer 'TEST_WBT', 'WebTIS', 'http://tickets.redspottedhanky.com/', 'http://uat-tickets.redspottedhanky.com/sjp/sjplanding.aspx', '', '', '', 'N', '', '', 'Retailers.Retailer.Rail.WebTIS.Test'
--EXEC AddUpdateRetailer 'TEST_CC', 'CityCruises', 'http://www.citycruises.co.uk', 'http://citycruisescloud.cloudapp.net', '', '', '', 'N', '', '', 'Retailers.Retailer.Ferry.CityCruises.Test'
--EXEC AddUpdateRetailer 'TEST_TC', 'ThamesClipper', 'http://www.thamesclippers.com', 'https://booking.thamesclippers.com/gamestravel', '', '', '', 'N', '', '', 'Retailers.Retailer.Ferry.ThamesClipper.Test'


----------------------------------------------------------------------
----------------------------------------------------------------------
-- Add Retailer Lookup
----------------------------------------------------------------------
----------------------------------------------------------------------
-- EXEC AddUpdateRetailerLookup @OperatorCode, @Mode, @RetailerId, @PartnerId, @ThemeId

-----------------------------------
-- TDP retailer sites
-----------------------------------
EXEC AddUpdateRetailerLookup 'AB','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'AL','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'ASCL','Coach','SCL',0,1
EXEC AddUpdateRetailerLookup 'CL','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'FK','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'FL','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'GD','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'HH','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'JL','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'NB','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'NG','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'NONE','Rail','FGW',0,1
--EXEC AddUpdateRetailerLookup 'NONE','Rail','MTT',0,1
EXEC AddUpdateRetailerLookup 'NONE','Rail','NEXEC',0,1
EXEC AddUpdateRetailerLookup 'NONE','Rail','ONE',0,1
EXEC AddUpdateRetailerLookup 'NONE','Rail','SCOTRAIL',0,1
EXEC AddUpdateRetailerLookup 'NONE','Rail','TPE',0,1
EXEC AddUpdateRetailerLookup 'NONE','Rail','TRAINLINE',0,1
EXEC AddUpdateRetailerLookup 'NONE','Rail','VIRGIN',0,1
EXEC AddUpdateRetailerLookup 'NONE','Rail','VIRGIN-B',0,1
EXEC AddUpdateRetailerLookup 'nrc-238','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'nrc-9','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'nrc-99028','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'NX','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'NXP','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'RA','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'SC','Coach','SCL',0,1
EXEC AddUpdateRetailerLookup 'SCL','Coach','SCL',0,1
EXEC AddUpdateRetailerLookup 'SH','Coach','NEX',0,1
EXEC AddUpdateRetailerLookup 'SP','Coach','NEX',0,1

-----------------------------------
-- TDP Mobile retailers
-----------------------------------
--EXEC AddUpdateRetailerLookup 'FOLY', 'Coach', 'DMT', 0, 1
--EXEC AddUpdateRetailerLookup '5364', 'Coach', 'DMT', 0, 1
--EXEC AddUpdateRetailerLookup 'NONE', 'Rail',  'NR', 0, 1
--EXEC AddUpdateRetailerLookup 'CCR', 'Ferry', 'CC', 0, 1
--EXEC AddUpdateRetailerLookup 'THC', 'Ferry', 'TC', 0, 1
--EXEC AddUpdateRetailerLookup 'TRS', 'Ferry', 'CC', 0, 1

--EXEC AddUpdateRetailerLookup 'FOLY', 'Coach', 'TEST_DMT', 0, 1
--EXEC AddUpdateRetailerLookup '5364', 'Coach', 'TEST_DMT', 0, 1
--EXEC AddUpdateRetailerLookup 'NONE', 'Rail',  'TEST_NR', 0, 1
--EXEC AddUpdateRetailerLookup 'NONE', 'Rail',  'TEST_WBT', 0, 1
--EXEC AddUpdateRetailerLookup 'CCR', 'Ferry', 'TEST_CC', 0, 1
--EXEC AddUpdateRetailerLookup 'THC', 'Ferry', 'TEST_TC', 0, 1
--EXEC AddUpdateRetailerLookup 'TRS', 'Ferry', 'TEST_CC', 0, 1

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 9999, 'Retailers data'