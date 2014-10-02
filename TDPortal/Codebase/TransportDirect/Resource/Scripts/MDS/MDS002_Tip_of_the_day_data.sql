-- ***********************************************
-- AUTHOR      	: Chris Rees
-- NAME 	: MDS002_Tip_of_the_day_data.sql
-- DESCRIPTION 	: Updates the tip of the day data
-- SOURCE 	: Manual Data Service
-- Version	: $Revision:   1.0  $
-- Additional Steps Required : Run updateCMSentries.exe
-- ************************************************
--$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Resource/DEL8_2/MDS/MDS002_Tip_of_the_day_data.sql-arc  $
--
--   Rev 1.0   Nov 08 2007 12:42:24   mturner
--Initial revision.
--
--   Rev 1.10   May 30 2007 09:26:34   sangle
--Fixed single quotes problem
--
--   Rev 1.9   May 25 2007 11:46:06   sangle
--Added Tip 107
--
--   Rev 1.8   May 23 2007 15:30:04   sangle
--Added Tips 105 and 106.
--
--   Rev 1.7   May 03 2006 16:19:52   DMistry
--Escaping single quote characters, for example from 'O'r to 'O''r
-- 
--
--   Rev 1.6   Apr 29 2006 15:51:06   abork
--Updated from TT05101 DEL 8_1 Tips of the day_RECEIVED.xls
-- - slight change to the existing text as first updated tip could not be found, re-used the closest match (tip 3)
--Resolution for 3961: DN102 Wait Pages:  Tip of the day refers to button that does no exist
--Resolution for 4000: DN102 Wait Pages:  Tip of the day refers to Quickplanner tab
--
--   Rev 1.5   Mar 03 2006 16:39:44   CRees
--Update Extend Journey tip to read Extend this Journey, to match current UI.
--
--   Rev 1.4   Feb 02 2006 15:08:14   CRees
--Added automatic CMS update code to end. No tip changes.
--
--   Rev 1.3   Jan 24 2006 12:18:06   scraddock
--Updated with new tips for Del 8 from document TT03604 supplied by LFR team 24/1, also correction of tip 103. Tested on NFH.
--
--   Rev 1.2   Jan 17 2006 13:47:52   CRees
--Updated with new tips for Del 8 and amendments to existing tips to reflect new page layouts and buttons. Some Welsh amendments (more to follow).
--
--   Rev 1.1   Jan 10 2006 13:14:46   CRees
--Revised script to include all wait page tip of the day properties, and to reflect correct headers for tips.
--
--   Rev 1.0   Jan 09 2006 11:51:12   scraddock
--Initial revision.

-- REPLACE WITH INSERT FROM HERE... (SEE BELOW FOR END)

-- Source: tipsource060116.txt + tipsource060116cy.txt
-- Set up Properties

USE PermanentPortal
GO

DECLARE @Text VARCHAR(2000),
	@Name VARCHAR(2000)

-- WaitPageTextStartEn
SET @Name = 'WaitPageTextStartEn'
SET @Text = '<span style="font-weight:normal">We are always seeking to improve our service. If you cannot find what you want, please tell us by clicking <b>Contact us</b><br><br><br><div style="background-color:#ccccff;border:1px solid #330099;padding:5px 5px 5px 5px;"><b>Tip of the Day</b><br><br>'
IF EXISTS (SELECT * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
BEGIN
	UPDATE properties	
	SET  pValue = @Text
	WHERE pName = @Name
	AND AID = 'Web'
	AND GID = 'UserPortal'
	AND PartnerId = 0
END
ELSE
BEGIN
	if not exists (select * FROM properties where pName = @Name AND AID = 'Web' AND GID = 'UserPortal' AND PartnerId=0)

		INSERT INTO properties 
		VALUES (@Name, @Text, 'Web', 'UserPortal', 0)
END


-- WaitPageTextStartCy
SET @Name = 'WaitPageTextStartCy'
SET @Text = '<span style="font-weight:normal">Rydym bob amser yn ceisio gwella ein gwasanaeth. Os na allwch ddod o hyd i’r hyn yr ydych yn chwilio amdano, dywedwch wrthym drwy glicio ar <b>Cysylltwch â ni</b><br><br><br><div style="background-color:#ccccff;border:1px solid #330099;padding:5px 5px 5px 5px;"><b>Awgrym y Dydd</b><br><br>'

IF EXISTS (SELECT * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
BEGIN
	UPDATE properties	
	SET  pValue = @Text
	WHERE pName = @Name
	AND AID = 'Web'
	AND GID = 'UserPortal'
	AND PartnerID = 0
END
ELSE
BEGIN
	if not exists (select * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
		INSERT INTO properties 
		VALUES (@Name, @Text, 'Web', 'UserPortal', 0)
END

-- WaitPageTextEndEn
SET @Name = 'WaitPageTextEndEn'
SET @Text = '</div></span>'
IF EXISTS (SELECT * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
BEGIN
	UPDATE properties	
	SET  pValue = @Text
	WHERE pName = @Name
	AND AID = 'Web'
	AND GID = 'UserPortal'
	AND PartnerID = 0

END
ELSE
BEGIN
	if not exists (select * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
		INSERT INTO properties 
		VALUES (@Name, @Text, 'Web', 'UserPortal', 0)
END

-- WaitPageTextEndCy
SET @Name = 'WaitPageTextEndCy'
SET @Text = '</div></span>'
IF EXISTS (SELECT * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
BEGIN
	UPDATE properties	
	SET  pValue = @Text
	WHERE pName = @Name
	AND AID = 'Web'
	AND GID = 'UserPortal'
	AND PartnerID = 0
END
ELSE
BEGIN
	if not exists (select * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
		INSERT INTO properties 
		VALUES (@Name, @Text, 'Web', 'UserPortal', 0)
END

-- WaitPageTipChannel.en
SET @Name = 'WaitPageTipChannel.en'
SET @Text = '/Channels/TransportDirect/en/JourneyPlanning'
if not exists (select * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
	INSERT INTO properties 
	VALUES (@Name, @Text, 'Web', 'UserPortal', 0)

-- WaitPageTipChannel.cy
SET @Name = 'WaitPageTipChannel.cy'
SET @Text = '/Channels/TransportDirect/cy/JourneyPlanning'
if not exists (select * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
	INSERT INTO properties 
	VALUES (@Name, @Text, 'Web', 'UserPortal', 0)

-- WaitPageTipPosting
SET @Name = 'WaitPageTipPosting'
SET @Text = 'WaitPage'
if not exists (select * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
	INSERT INTO properties 
	VALUES (@Name, @Text, 'Web', 'UserPortal', 0)

-- WaitPageTipPlaceHolder
SET @Name = 'WaitPageTipPlaceHolder'
SET @Text = 'MessageDefinition'
if not exists (select * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
	INSERT INTO properties 
	VALUES (@Name, @Text, 'Web', 'UserPortal', 0)

-- CurrentWaitPageTipID
SET @Name = 'CurrentWaitPageTipID'
SET @Text = '0'
if not exists (select * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
	INSERT INTO properties 
	VALUES (@Name, @Text, 'Web', 'UserPortal', 0)

-- WaitPageTipMethod
SET @Name = 'WaitPageTipMethod'
SET @Text = '1'
if not exists (select * FROM properties where pName = @Name AND AID='Web' AND GID='UserPortal' AND PartnerId=0)
	INSERT INTO properties 
	VALUES (@Name, @Text, 'Web', 'UserPortal', 0)

GO



USE TransientPortal
GO

DECLARE @enText VARCHAR(2000),
	@cyText VARCHAR(2000),
	@TipID Int


-- Tip 1
SET @TipId = 1
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 2
SET @TipId = 2
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau eich siwrnai yn gyflym yn yr adran <b>''Newid dyddiad/amser''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 3
SET @TipId = 3
SET @enText = 'Find the nearest station or airport to a location. Click the <b>''Find a place''<b> tab and then click on the <b>''Find nearest stations and airports''<b> icon or link.'
SET @cyText = 'Canfyddwch yr orsaf neu''r maes awyr agosaf i leoliad. Cliciwch ar y tab  <b>''Canfyddwch le''<b> ac yna cliciwch ar yr eicon neu ddolen <b>''Dod o hyd i''r orsaf/maes awyr agosaf''<b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 4
SET @TipId = 4
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 5
SET @TipId = 5
SET @enText = 'Avoid particular roads when planning a car journey. Click <b>''Advanced options''</b> on the input page.'
SET @cyText = 'Osgowch ffyrdd penodol wrth gynllunio siwrnai car. Cliciwch ar <b>''Dewisiadau mwy cymhleth''</b> ar y dudalen mewnbynnu.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 6
SET @TipId = 6
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau eich siwrnai yn gyflym yn yr adran <b>''Newid dyddiad/amser''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 7
SET @TipId = 7
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 8
SET @TipId = 8
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 9
SET @TipId = 9
SET @enText = 'View ticket prices for journeys by train or coach by clicking <b>''Tickets/costs''</b>.'
SET @cyText = 'Edrychwch ar brisiau''r tocynnau ar gyfer siwrneion mewn tr&ecirc;n neu fws moethus trwy glicio ar <b>''Tocynnau/Costau''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 10
SET @TipId = 10
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau eich siwrnai yn gyflym yn yr adran <b>''Newid dyddiad/amser''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 11
SET @TipId = 11
SET @enText = 'Save your favourite journeys and travel preferences. Click the <b>''Save as a favourite journey''</b> tab.'
SET @cyText = 'Cadwch eich hoff siwrneion a''ch hoffterau teithio. Cliciwch y tab <b>''Cadwch fel hoff siwrnai''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 12
SET @TipId = 12
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 13
SET @TipId = 13
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 14
SET @TipId = 14
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau eich siwrnai yn gyflym yn yr adran <b>''Newid dyddiad/amser''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 15
SET @TipId = 15
SET @enText = 'Travelling by train? Find out what stations you will be stopping at. Click <b>''Details''</b> then click on the word <b>''Train''</b> under the train symbol.'
SET @cyText = 'Teithio ar y tr&ecirc;n?  Canfyddwch am hygyrchedd, cyfleusterau neu barcio mewn gorsaf reilffordd.  Cliciwch ar <b>''Manylion''</b> ac yna cliciwch ar enw gorsaf.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 16
SET @TipId = 16
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 17
SET @TipId = 17
SET @enText = 'Find your nearest station. Click the <b>''Find a place''<b> tab and then click on the <b>''Find nearest stations and airports''</b> link near the top of the screen.'
SET @cyText = 'Canfyddwch eich gorsaf agosaf. Cliciwch ar y tab <b>''Canfyddwch le''<b> ac yna cliciwch ar yr eicon neu ddolen <b>''Dod o hyd i''r orsaf/maes awyr agosaf''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 18
SET @TipId = 18
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau eich siwrnai yn gyflym yn yr adran <b>''Newid dyddiad/amser''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 19
SET @TipId = 19
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 20
SET @TipId = 20
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 21
SET @TipId = 21
SET @enText = 'See maps for any stage of your journey. Click <b>''Maps''</b>.'
SET @cyText = 'Gweler mapiau am unrhyw gam o''ch siwrnai.  Cliciwch ar <b> ''Mapiau''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 22
SET @TipId = 22
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 23
SET @TipId = 23
SET @enText = 'Get an estimate of the cost of travelling by car. Click <b>''Tickets/costs''</b> when you get a car option.'
SET @cyText = 'Chwiliwch am amcangyfrif o gost gyrru eich car. Cliciwch ar <b>''Tocynnau/Costau''</b> pan gewch ddewis yn ymwneud &acirc; char.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 24
SET @TipId = 24
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 25
SET @TipId = 25
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 26
SET @TipId = 26
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 27
SET @TipId = 27
SET @enText = 'Get a good quality printout. Click <b>''Printer friendly''</b> before printing as normal.'
SET @cyText = 'Gofalwch eich bod yn cael allbrint o ansawdd da. Cliciwch ar <b>''Hawdd ei argraffu''</b> cyn argraffu fel arfer.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 28
SET @TipId = 28
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 29
SET @TipId = 29
SET @enText = 'Get live departure times on your PDA or mobile phone for rail stations and some bus/coach stops. Click the <b>''Tips and tools''</b> tab.'
SET @cyText = 'Chwiliwch am amserau ymadael byw ar eich PDA neu ff&ocirc;n symudol ar gyfer gorsafoedd rheilffordd a rhai arosfannau bysiau/bysiau moethus. Cliciwch y tab <b>''Awgrymiadau a theclynnau''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 30
SET @TipId = 30
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 31
SET @TipId = 31
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 32
SET @TipId = 32
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 33
SET @TipId = 33
SET @enText = 'Get live travel news for road or public transport on your PDA or mobile phone. Click the <b>''Tips and tools''</b> tab.'
SET @cyText = 'Chwiliwch am newyddion teithio byw ar gyfer cludiant ar y ffordd neu gludiant cyhoeddus ar eich PDA neu eich ff&ocirc;n symudol. Cliciwch y tab <b>''Awgrymiadau a theclynnau''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 34
SET @TipId = 34
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 35
SET @TipId = 35
SET @enText = 'Travelling by train? Find out about what on-board facilities will be available. Click <b>''Details''</b>.'
SET @cyText = 'Teithio ar y tr&ecirc;n?  Canfyddwch am hygyrchedd, cyfleusterau neu barcio mewn gorsaf reilffordd.  Cliciwch ar <b>''Manylion''</b> ac yna cliciwch ar enw gorsaf.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 36
SET @TipId = 36
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 37
SET @TipId = 37
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 38
SET @TipId = 38
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 39
SET @TipId = 39
SET @enText = 'Want to compare your car and public transport options for the same journey? Use the <b>''Door to door''</b> planner.'
SET @cyText = 'Hoffech chi gymharu eich dewisiadau car a chludiant cyhoeddus ar gyfer yr un siwrnai? Defnyddiwch y cynlluniwr <b>''Drws-i-ddrws''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 40
SET @TipId = 40
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 41
SET @TipId = 41
SET @enText = 'Email travel information to others. Click the <b>''Send to a friend''</b> tab.'
SET @cyText = 'Ebostiwch wybodaeth am deithio i eraill. Cliciwch y tab <b>''Anfonwch at ffrind''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 42
SET @TipId = 42
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 43
SET @TipId = 43
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 44
SET @TipId = 44
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 45
SET @TipId = 45
SET @enText = 'See live departure boards for all 2500 national rail stations. Click the <b>''Live travel''</b> tab.'
SET @cyText = 'Gwelwch y byrddau ymadael byw ar gyfer pob un o''r 2500 o orsafoedd rheilffordd cenedlaethol. Cliciwch y tab <b>''Teithio byw''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 46
SET @TipId = 46
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 47
SET @TipId = 47
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 48
SET @TipId = 48
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 49
SET @TipId = 49
SET @enText = 'Choose to use only certain types of transport in the Door-to-door journey planner. Click <b>''Advanced options''</b> on the input page.'
SET @cyText = 'Dewiswch ddefnyddio rhai mathau o gludiant yn unig yn y cynlluniwr siwrneion ''Drws-i-ddrws''. Cliciwch ar <b>''Dewisiadau mwy cymhleth''</b> ar y dudalen mewnbynnu.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 50
SET @TipId = 50
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 51
SET @TipId = 51
SET @enText = 'Car routes and times account for predicted traffic levels at different times of the day.'
SET @cyText = 'Mae llwybrau ac amserau teithio mewn car yn cyfrif am y lefelau traffig a ragfynegir ar wahanol adegau o''r dydd.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 52
SET @TipId = 52
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 53
SET @TipId = 53
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 54
SET @TipId = 54
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 55
SET @TipId = 55
SET @enText = 'Register on Transport Direct and benefit from more features. Click <b>''Register''</b>.'
SET @cyText = 'Cofrestrwch ar Transport Direct i elwa ar fwy o nodweddion. Cliciwch ar <b>''Cofrestrwch''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 56
SET @TipId = 56
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 57
SET @TipId = 57
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 58
SET @TipId = 58
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 59
SET @TipId = 59
SET @enText = 'Plan a journey to attractions or facilities (public buildings) such as ''Beaumaris Castle'', ''Big Ben'', or ''Royal County Hotel''. Select <b>''Attraction/facility''</b> on the input page.'
SET @cyText = 'Cynlluniwch siwrnai i atyniadau neu gyfleusterau (adeiladau cyhoeddus) fel ''Castell Biwmaris'', ''Big Ben'' neu ''Gwesty''r Royal County''. Dewiswch <b>''Atyniad/cyfleuster''</b> ar y dudalen mewnbynnu.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 60
SET @TipId = 60
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 61
SET @TipId = 61
SET @enText = 'Travelling by train? Find out about station accessibility, facilities or car parking at a rail station. Click <b>''Details''</b> and then click a station name.'
SET @cyText = 'Teithio ar y tr&ecirc;n?  Canfyddwch am hygyrchedd, cyfleusterau neu barcio mewn gorsaf reilffordd.  Cliciwch ar <b>''Manylion''</b> ac yna cliciwch ar enw gorsaf.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 62
SET @TipId = 62
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 63
SET @TipId = 63
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 64
SET @TipId = 64
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 65
SET @TipId = 65
SET @enText = 'Allow extra time for connections in the Door-to-door planner. Click <b>''Advanced options''</b> on the input page.'
SET @cyText = 'Caniatewch amser ychwanegol ar gyfer cysylltiadau yn y Cynlluniwr o ddrws-i-ddrws.  Cliciwch <b>''Dewisiadau mwy cymhleth''</B> ar y dudalen mewnbynnu.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 66
SET @TipId = 66
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 67
SET @TipId = 67
SET @enText = 'Compare travel options between towns and cities by coach, rail and air with the <b>''City to city''</b> quick planner.'
SET @cyText = 'Cymharwch ddewisiadau teithio rhwng trefi a dinasoedd drwy fws moethus, rheilffordd ac awyr gyda''r cynlluniwr cyflym <b>''Dinas-i-ddinas''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 68
SET @TipId = 68
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 69
SET @TipId = 69
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 70
SET @TipId = 70
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 71
SET @TipId = 71
SET @enText = 'Get taxi rank information or mini cab phone numbers on your PDA or mobile phone. Click the <b>''Mobile''</b> tab.'
SET @cyText = 'Chwiliwch am wybodaeth am ranciau tacsi neu rifau ff&ocirc;n cabiau mini ar eich PDA neu ff&ocirc;n symudol. Cliciwch y tab <b>''Symudol''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 72
SET @TipId = 72
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 73
SET @TipId = 73
SET @enText = 'Plan journeys with local public transport, such as buses or trams. Click the <b>Door-to-door</b> tab.'
SET @cyText = 'Cynlluniwch siwrneion gyda chludiant cyhoeddus lleol, fel bysiau neu dramiau. Cliciwch y tab <b>Drws-i-ddrws</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 74
SET @TipId = 74
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 75
SET @TipId = 75
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 76
SET @TipId = 76
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 77
SET @TipId = 77
SET @enText = 'If you want a car route that avoids ferries or tolls, Click <b>''Advanced options''</b> on the input page.'
SET @cyText = 'Os hoffech gael llwybr i deithio arno mewn car sy''n osgoi ffer&iuml;au neu dollau, cliciwch ar <b>''Dewisiadau mwy cymhleth''</b> ar y dudalen mewnbynnu.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 78
SET @TipId = 78
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 79
SET @TipId = 79
SET @enText = 'Click <b>''Tickets/costs''</b> to buy train and coach tickets or view car costs.'
SET @cyText = 'Cliciwch ar <b>''Tocynnau/Costau''</b> ar y dudalen nesaf i brynu tocynnau tr&ecirc;n a bysiau moethus neu i edrych ar gostau ceir.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 80
SET @TipId = 80
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 81
SET @TipId = 81
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 82
SET @TipId = 82
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 83
SET @TipId = 83
SET @enText = 'Find out which taxis or private hire cars serve a station by clicking the station name on the <b>''Details''</b> page of your journey plan.'
SET @cyText = 'Darganfyddwch pa dacsis neu geir hurio preifat sy''n gwasanaethu gorsaf drwy glicio ar enw''r orsaf ar dudalen <b>''Manylion''</b> cynllun eich siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 84
SET @TipId = 84
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 85
SET @TipId = 85
SET @enText = 'See a map of road incidents in your area. Click the <b>''Live travel''</b> tab.'
SET @cyText = 'Edrychwch ar fap o bethau sydd wedi digwydd ar y ffordd yn eich ardal. Cliciwch ar y tab <b>''Teithio byw''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 86
SET @TipId = 86
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 87
SET @TipId = 87
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 88
SET @TipId = 88
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 89
SET @TipId = 89
SET @enText = 'Choose particular roads to use when planning a car journey. Click <b>''Advanced options''</b> on the input page.'
SET @cyText = 'Dewiswch ffyrdd penodol i''w defnyddio wrth gynllunio siwrnai car. Cliciwch ar <b>''Dewisiadau mwy cymhleth''</b> ar y dudalen mewnbynnu.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 90
SET @TipId = 90
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 91
SET @TipId = 91
SET @enText = 'Get details of Park and Ride schemes across the country. Click the link on the <b>''Find a car route''</b> page.'
SET @cyText = 'Ewch i gael manylion am gynlluniau Parcio a Theithio ar draws y wlad. Cliciwch y ddolen ar y dudalen <b>''Canfyddwch lwybr car''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 92
SET @TipId = 92
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 93
SET @TipId = 93
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 94
SET @TipId = 94
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 95
SET @TipId = 95
SET @enText = 'Download the new Transport Direct <b>''Toolbar''</b> for easy access to the main pages on the portal.'
SET @cyText = 'Lawrlwythwch <b>''Bar Offer''</b> newydd Transport Direct i gael mynediad rhwydd i''r prif dudalennau ar y porth.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 96
SET @TipId = 96
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 97
SET @TipId = 97
SET @enText = 'Plan a day trip to your choice of two destinations and then back to where you started with <b>''Day trip planner''</b>.'
SET @cyText = 'Cynlluniwch daith dydd i''ch dewis o ddau gyrchfan ac yna yn &ocirc;l i ble y bu i chi ddechrau gyda''r <b>''Cynllunydd teithiau dydd''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 98
SET @TipId = 98
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 99
SET @TipId = 99
SET @enText = 'Click <b>''Details''</b> to see the full list of times, directions and changes for each journey.'
SET @cyText = 'Cliciwch ar <b>''Manylion''</b> i weld y rhestr lawn o amserau, cyfarwyddiadau a newidiadau ar gyfer pob siwrnai.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 100
SET @TipId = 100
SET @enText = 'From the results page, click <b>''Modify journey''<b> to see how you might tailor the journey to suit your exact needs.'
SET @cyText = 'O''r dudalen canlyniadau, cliciwch ar <b>''Diwygiwch y siwrnai''<b> i weld sut y gallech deilwrio''r siwrnai i weddu i''ch union'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 101
SET @TipId = 101
SET @enText = '<b>Compare the cost</b> of travel options between towns and cities by coach and train with the <b>''City to city''</b> quick planner.'
SET @cyText = '<b>Cymharwch gostau</b> dewisiadau teithio rhwng trefi a dinasoedd mewn bws moethus a thr&ecirc;n gyda''r cynlluniwr cyflym <b>''Dinas-i-ddinas''</b>.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 102
SET @TipId = 102
SET @enText = 'You can quickly amend journey times in the <b>''Amend date and time''</b> section.'
SET @cyText = 'Gallwch ddiwygio amserau siwrneion yn gyflym yn yr adran <b> ''Newid ddyddiad/amser''.</b>'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 103
SET @TipId = 103
SET @enText = 'Click <b>''Bookmark this journey for the future''</b> to save a journey request to your browser favourites.'
SET @cyText = 'Cliciwch <b>''Rhowch nod llyfr ar y siwrnai hon ar gyfer y dyfodol''</b> i gadw cais am siwrnai i ffefrynnau eich porwr.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END


-- Tip 104
SET @TipId = 104
SET @enText = 'Get a free journey searchbox that gives people directions to where you are.  Click the link in <b>''Tips and tools''<b>, on the homepage.'
SET @cyText = 'Cymerwch flwch chwilio am siwrnai am ddim sy''n rhoi cyfarwyddiadau i bobl i ble rydych chi. Cliciwch y ddolen yn <b>''Awgrymiadau a theclynnau''<b>, ar y dudalen gartref.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 105
SET @TipId = 105
SET @enText = 'You can plan journeys for the current calendar month and the next two months.'
SET @cyText = 'Gallwch gynllunio siwrneion ar gyfer y mis calendr hwn a''r ddau fis nesaf.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END


-- Tip 106
SET @TipId = 106
SET @enText = 'Transport Direct will search for the fastest route using the parameters you have selected.'
SET @cyText = 'Bydd Transport Direct yn chwilio am y llwybr gyflymaf gan ddefnyddio''ch paramedrau dewisol.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END

-- Tip 107
SET @TipId = 107
SET @enText = 'From the Find a Place page, click ''Find nearest car park'' and see a list of car parks which you can then plan a journey to.'
SET @cyText = 'From the Find a Place page, click ''Find nearest car park'' and see a list of car parks which you can then plan a journey to.'
IF EXISTS (SELECT * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
BEGIN
	UPDATE WaitPageMessageTips
	SET  WaitPageTipTextEn = @enText, WaitPageTipTextCy = @cyText
	WHERE WaitPageTipID = @TipID
END
ELSE
BEGIN
	if not exists (select * FROM WaitPageMessageTips where WaitPageTipID = @TipID)
		INSERT INTO WaitPageMessageTips
		VALUES (@TipID, @enText, @cyText)
END



GO

-- ------------------------------------------------------------------------------
-- Set up Properties

USE PermanentPortal
GO

DECLARE @Text VARCHAR(2000),
	@Name VARCHAR(2000)

-- MaxWaitPageTipID
SET @Name = 'MaxWaitPageTipID'
SET @Text = '104'
if not exists (select * FROM properties where pName = @Name AND AID = 'Web' AND GID = 'Userportal' AND PartnerID=0)
	INSERT INTO properties 
	VALUES (@Name, @Text, 'Web', 'UserPortal', 0)
ELSE 
	UPDATE properties
	SET pValue = @Text
	WHERE pName = @Name AND AID = 'Web' AND GID = 'UserPortal' AND PartnerID=0

GO


-- ... END INSERT HERE

----------------
-- Change Log --
----------------

USE PermanentPortal

Declare @@value decimal(7,3)
Select @@value = left(right('$Revision:   1.0  $',8),7)


IF EXISTS (SELECT * FROM dbo.MDSChangeCatalogue WHERE ScriptNumber = 002 and VersionNumber = @@value)
BEGIN
	UPDATE dbo.MDSChangeCatalogue
	SET
		ChangeDate = getdate(),
		Summary = 'Updates the tip of the day data'
	WHERE ScriptNumber = 002 AND VersionNumber = @@value
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
		002,
		@@value,
		'Updates the tip of the day data'
	)
END


----------------
-- UPDATE CMS --
----------------

begin
	raiserror('CMS UPDATE REQUIRED',16,1) with LOG
end
