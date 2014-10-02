-- *********************************************************************
-- NAME 	: GetLatestTravelNewsFileName.sql
-- DESCRIPTION : Looks up the most recently imported travel news data file name
--
-- *********************************************************************


use PermanentPortal

SELECT     DATA_FEED_FILENAME
FROM         FTP_CONFIGURATION
WHERE     (DATA_FEED = 'afc743')

go