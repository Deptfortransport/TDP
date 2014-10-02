-- *******************************************************
-- NAME 	: CheckOsmapRows.sql
-- DESCRIPTION : Used as part of the weekly Naptan and Nptg import procedure
-- Called from dft,bat. Counts the number of rows in StopsCount and
-- NaptanCheck tables and returns the difference.
--
-- ************************************************
-- $Log:   P:/TDPortal/archives/DotNet2.0Codebase/build/DataGatewayApps/Gateway/sql/CheckOsmapRows.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 11:53:44   mturner
--Initial revision.

use OsMap

-- Create two integer values
	DECLARE @StopsCount int, @NaptanCheck int

	-- Get the number of rows from the first table
	SELECT @StopsCount = (SELECT COUNT(*) FROM OSAdmin.STOPS)	                        
	SELECT @NaptanCheck = (SELECT COUNT(*) FROM OSAdmin.NaptanCheck)							 

    -- Return the difference between the two table rows
	SELECT TotalCount = @StopsCount - @NaptanCheck