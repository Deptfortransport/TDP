-- ***********************************************
-- NAME 	: PopulateTransientPortalDataBaseTables.sql
-- DESCRIPTION 	: Populates Transient Portal database tables
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/Data/PopulateTransientPortalDataBaseTables.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:41:12   mturner
--Initial revision.

use [TransientPortal]
GO

DELETE FROM TrafficAndTravelNewsSeverity

INSERT INTO TrafficAndTravelNewsSeverity
(ID, Description)
SELECT
1, 'Very Severe'
UNION
SELECT
2, 'Severe'
UNION
SELECT
3, 'Medium'
UNION
SELECT
4, 'Slight'
UNION
SELECT
5, 'Very Slight'
