-- ***********************************************
-- AUTHOR      	: Tim Mollart
-- NAME 	: MDS006_FindTrainPromoText.sql
-- DESCRIPTION 	: Updates the promotional panel on the find train 
--                and find train cost pages.
-- ************************************************
--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS006_FindTrainPromoText.sql-arc  $   
--
--   Rev 1.0   Nov 08 2007 12:42:26   mturner
--Initial revision.
--
--   Rev 1.3   Nov 23 2006 11:47:08   mturner
--Updated to resolve Firefox display issue
--
--   Rev 1.2   Nov 16 2006 14:16:52   tmollart
--Updated the hyper link location for TD Mobile.
--Resolution for 4220: Rail Search by Price
--
--   Rev 1.1   Nov 15 2006 17:22:42   tmollart
--Fixed error in Where clause of the update statement.
--

USE TransientPortal

-- Text Definitions                                                                                                                        

DECLARE @textEN VARCHAR(8000)
DECLARE @textCY VARCHAR(8000)

SET @textEN = '<div class="Column3Header"><div class="txtsevenbbl">Get the latest travel info on your phone</div>&nbsp;&nbsp;</div><div class="Column3Content"><table cellSpacing="0" cellPadding="2" width="100%" border="0" class="txtseven"><tr><td>You can get our <a href="http://www.transportdirect.info/TransportDirect/en/TDOnTheMove/TDOnTheMove.aspx">Transport Direct Mobile</a> service on your mobile phone or PDA for free. You can search for next train departures and arrivals, check whether a train is running on time and look up times for tomorrow.</td></tr></table></div>'

SET @textCY = @textEN


-- EN Updates
UPDATE CmsTextEntries SET text = @textEN WHERE Channel = '/Channels/TransportDirect/en/JourneyPlanning' AND Posting LIKE '%FindTrain%' AND PlaceHolder = 'TDFindTrainPromoHtmlPlaceholderDefinition'

-- CY Updates
UPDATE CmsTextEntries SET text = @textCY WHERE Channel = '/Channels/TransportDirect/cy/JourneyPlanning' AND Posting LIKE '%FindTrain%' AND PlaceHolder = 'TDFindTrainPromoHtmlPlaceholderDefinition'


----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.0  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 006 and VersionNumber = @@value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Updated Find Train (Cost) Promotional CMS Placeholder'
	WHERE ScriptNumber = 006 AND VersionNumber = @@value
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
		006,
		@@value,
		'Updated Find Train (Cost) Promotional CMS Placeholder'
	)
END


----------------
-- UPDATE CMS --
----------------

begin
	raiserror('CMS UPDATE REQUIRED',16,1) with LOG
end