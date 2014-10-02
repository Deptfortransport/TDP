-- ***********************************************
-- AUTHOR      : Phil Scott
-- NAME : MDS0010_AddWrexhamAndShropshire.sql
-- DESCRIPTION : Adds new operator WS for Wrexham and Shropshire 
-- SOURCE : Manual Data Service
-- Version: $Revision:   1.0  $
-- Additional Steps Required : 
-- ************************************************
--//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS010_AddWrexhamAndShropshire.sql-arc  $
--
--   Rev 1.0   Apr 10 2008 14:41:24   pscott
--Initial revision.
--
--   Rev 1.0   Apr 10 2008 16:39:24   pscott
--Initial revision.

USE TransientPortal

INSERT INTO ExternalLinks (Id,URL,TestURL,Valid,Description)
select 'OperatorLink.WS',
'http://www.nationalrail.co.uk/companies/?atocCode=ws',
'http://www.nationalrail.co.uk/companies/?atocCode=WS',
1,
'Operator website'





----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.0  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 010 and VersionNumber = @@value)
BEGIN
UPDATE dbo.MDSChangeCatalogue
SET
ChangeDate = getdate(),
Summary = 'Adds new operator WS for Wrexham and Shropshire '
WHERE ScriptNumber = 009 AND VersionNumber = @@value
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
010,
@@value,
'Adds new operator WS for Wrexham and Shropshire '
)
END