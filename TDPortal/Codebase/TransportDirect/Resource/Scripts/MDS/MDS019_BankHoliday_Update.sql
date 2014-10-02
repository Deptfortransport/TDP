-- ***********************************************
-- AUTHOR   : Richard Hopkins
-- NAME 	: MDS019_BankHoliday_Update.sql
-- DESCRIPTION 	: Updates Bank Holiday data
-- SOURCE 	: TDP Development Team
-- Version	: 1.8
-- Additional Steps Required: 
-- ************************************************
--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS019_BankHoliday_Update.sql-arc  $
--
--   Rev 1.8	03/02/2014 - R Broddle - Updated with 2014 data
--
--   13/06/2013 - R Broddle - Updated to correct GZ:I5294361 - 
--   Scottish Bank Holiday Showing 6 Aug - Should be 5 Aug
--
--   Rev 1.6   Jan 07 2013 13:16:18   rbroddle
--Latest Bank Holiday Data from MDS
--Resolution for 5881: Latest Bank Holiday Data from MDS
--
--   Rev 1.5   Dec 01 2011 14:39:02   nrankin
--Bank Holiday Data table Update from PUSS
--Resolution for 5770: Bank Holiday Data table Update from PUSS
--
--   Rev 1.4   Jan 13 2011 16:06:08   PScott
--no november 11 Bank Holiday.
--
--   Rev 1.3   Jan 13 2011 15:43:10   PScott
--add extra days for 2011
--
--   Rev 1.2   Oct 18 2010 16:01:40   RBroddle
--Added 2011 Bank Holidays
--
--   Rev 1.1   Oct 13 2010 13:00:00   scraddock
--Moved to correct script name and updated accordingly
--
--   Rev 1.0   Oct 13 2010 12:57:40   scraddock
--Initial revision.
--
--   Rev 1.0   Feb 24 2010 17:37:24   rhopkins
--Initial revision.
--

use PermanentPortal


delete from dbo.BankHolidays 
GO

-- Add Bank holiday data 
insert into dbo.BankHolidays values ('2013-Jan-01 00:00:00',3)
insert into dbo.BankHolidays values ('2013-Mar-29 00:00:00',3)
insert into dbo.BankHolidays values ('2013-Apr-01 00:00:00',3)
insert into dbo.BankHolidays values ('2013-May-06 00:00:00',3)
insert into dbo.BankHolidays values ('2013-May-27 00:00:00',3)
insert into dbo.BankHolidays values ('2013-Aug-05 00:00:00',2)
insert into dbo.BankHolidays values ('2013-Aug-26 00:00:00',3)
insert into dbo.BankHolidays values ('2013-Nov-30 00:00:00',2)
insert into dbo.BankHolidays values ('2013-Dec-25 00:00:00',3)
insert into dbo.BankHolidays values ('2013-Dec-26 00:00:00',3)
insert into dbo.BankHolidays values ('2014-Jan-01 00:00:00',3)
insert into dbo.BankHolidays values ('2014-Jan-02 00:00:00',2)
insert into dbo.BankHolidays values ('2014-Apr-18 00:00:00',3)
insert into dbo.BankHolidays values ('2014-Apr-21 00:00:00',1)
insert into dbo.BankHolidays values ('2014-May-05 00:00:00',3)
insert into dbo.BankHolidays values ('2014-May-26 00:00:00',3)
insert into dbo.BankHolidays values ('2014-Aug-04 00:00:00',2)
insert into dbo.BankHolidays values ('2014-Aug-25 00:00:00',1)
insert into dbo.BankHolidays values ('2014-Dec-25 00:00:00',3)
insert into dbo.BankHolidays values ('2014-Dec-26 00:00:00',3)

GO

----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = 1.8


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 019 and VersionNumber = @@value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Updates for Bank Holidays'
	WHERE ScriptNumber = 019 AND VersionNumber = @@value
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
		019,
		@@value,
		'Updates for Bank Holidays'
	)
END
