-- ***********************************************
-- AUTHOR      	: Test
-- NAME 	: MDS000 - Test.sql
-- DESCRIPTION 	: Test
-- SOURCE 	: Apps Support
-- Version	: $Revision:   1.0  $
-- Additional Steps Required : None
-- ************************************************
--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS000 - Test.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:24   mturner
--Initial revision.
--
--   Rev 1.7   Dec 12 2005 15:42:20   JFrank
--Ammend Change control update statement.
--
--   Rev 1.6   Dec 12 2005 15:04:30   scraddock
--Added declare stqtementyn4et
--
--   Rev 1.5   Dec 12 2005 14:58:18   scraddock
--Corrected syntax error
--
--   Rev 1.4   Dec 12 2005 14:56:38   scraddock
--Added @@value to automatically update the change catalogue footer script
--
--   Rev 1.3   Dec 12 2005 14:41:46   scraddock
--Added trailing '$' symbol back in as it is NEEDED


----------------
-- Change Log --
----------------
USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.0  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 000 and VersionNumber = @@value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Test'
	WHERE ScriptNumber = 000 AND VersionNumber = @@value
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
		000,
		@@value,
		'Test'
	)
END
