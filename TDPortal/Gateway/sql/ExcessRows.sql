-- *****************************************************************************
-- NAME 	: ExcessRows.sql
-- DESCRIPTION : Used as part of the weekly Naptan and Nptg 
-- import procedure. Called from dft.bat. 
-- Deletes any groupid in the stopsingroups table where instances of groupid > 50
--
-- ****************************************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/build/DataGatewayApps/Gateway/sql/ExcessRows.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 11:53:44   mturner
--Initial revision.

use osmap
go

delete osadmin.stopsingroups where groupid in
(select groupid from osadmin.stopsingroups group by groupid having count(*) > 50)

go


