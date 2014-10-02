-- ***********************************************
-- AUTHOR      : Phil Scott
-- NAME : MDS0011_journeycallpartner.sql
-- DESCRIPTION : Updates hostname of journey call partner
-- SOURCE : Manual Data Service
-- Version: $Revision:   1.0  $
-- Additional Steps Required : IIS Reset Webservers
-- ************************************************
-- ************************************************
--//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS0011_journeycallpartner.sql-arc  $
--
--   Rev 1.0   May 20 2008 08:37:02   Pscott
--Initial revision.
--
--   Rev 1.0   May 20 2008 08:10:24   pscott
--Initial revision.


USE Reporting

update Partner
set hostname = 'journeycall'
where partnerid = 6

go



USE PermanentPortal

update Partner
set hostname = 'journeycall'
where partnerid = 6

go



----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.0  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 011 and VersionNumber = @@value)
BEGIN
UPDATE dbo.MDSChangeCatalogue
SET
ChangeDate = getdate(),
Summary = 'Updates the journeycall hostname in partner table'
WHERE ScriptNumber = 011 AND VersionNumber = @@value
END
ELSE
BEGIN
INSERT INTO dbo.MDSChangeCatalogue
(
ScriptNumber,
VersionNumber,
Summary
)
VALUES
(
011,
@@value,
'Updates the journeycall hostname in partner table'
)
END