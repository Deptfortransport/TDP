-- ***********************************************
-- NAME           : SC10020_TransportDriect_Content_20_Help_FindNearestAccessibleStop.sql
-- DESCRIPTION    : Updates to Help text for Find Nearest Accessible Stop page
-- AUTHOR         : Mitesh Modi
-- DATE           : 09 Jan 2012
-- ***********************************************

USE [Content] 
GO

DECLARE 
	@GroupId int,
	@ThemeId int

SET @ThemeId = 1

-- Find Nearest Accessible Stop - Page url configuration
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'ContentDatabase')

EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Help_HelpFindNearestAccessibleStop', 'Channel'
,'/Channels/TransportDirect/Help/HelpFindNearestAccessibleStop'
,'/Channels/TransportDirect/Help/HelpFindNearestAccessibleStop'

EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Help_HelpFindNearestAccessibleStop', 'Page'
,'/Web2/helpfulljp.aspx'
,'/Web2/helpfulljp.aspx'

EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Help_HelpFindNearestAccessibleStop', 'QueryString'
,'helpfulljp'
,'helpfulljp'


EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Printer_HelpFindNearestAccessibleStop', 'Channel'
,'/Channels/TransportDirect/Printer/HelpFindNearestAccessibleStop'
,'/Channels/TransportDirect/Printer/HelpFindNearestAccessibleStop'

EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Printer_HelpFindNearestAccessibleStop', 'Page'
,'/Web2/helpfulljp.aspx'
,'/Web2/helpfulljp.aspx'

EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Printer_HelpFindNearestAccessibleStop', 'QueryString'
,'helpfulljp'
,'helpfulljp'

-- Find Nearest Accessible Stop - Help content
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'helpfulljp')

EXEC AddtblContent
@ThemeId, @GroupId, 'HelpBodyText', '/Channels/TransportDirect/Help/HelpFindNearestAccessibleStop'
,'<p>
You have been shown this screen because your selected ''from'' and /or ''to'' points for your journey are not able to deliver the accessible journey characteristics you have requested. This page will show you places near your selected points that do meet the accessible journey characteristics. 
You need to select a point each of the ''from'', ''to'' and ''via'' points that you have been shown. The journey will then be planned using new points. 
If no points are shown then there are no places near your chosen point that match your selected stop point types and your chosen accessible journey requirements. In this case you may need to amend the type of stop or return to the input screen to amend your accessibility requirements. 
Once you have selected your points please click ''next'' to plan your journey.
</p>
<br />
<p>
<strong>View on map</strong>
<br />
You can see the stops and locations on a map by clicking ''View on Map'' for each of the ''from'', ''to'' and via stops list. 
Once you have a map displayed this can be removed by clicking on ''hide''. 
</p>
<br />
<p>
<strong>Choose the types of stop point you would like to use</strong>
<br />
Untick the types of transport stop or locations that you are not interested in using. However, at least one type must remain ticked.
For a town/district/village the journey planner will search for journeys that start from the centre of the selected location.
</p>'
,
'<p>
You have been shown this screen because your selected ''from'' and /or ''to'' points for your journey are not able to deliver the accessible journey characteristics you have requested. This page will show you places near your selected points that do meet the accessible journey characteristics. 
You need to select a point each of the ''from'', ''to'' and ''via'' points that you have been shown. The journey will then be planned using new points. 
If no points are shown then there are no places near your chosen point that match your selected stop point types and your chosen accessible journey requirements. In this case you may need to amend the type of stop or return to the input screen to amend your accessibility requirements. 
Once you have selected your points please click ''next'' to plan your journey.
</p>
<br />
<p>
<strong>View on map</strong>
<br />
You can see the stops and locations on a map by clicking ''View on Map'' for each of the ''from'', ''to'' and via stops list. 
Once you have a map displayed this can be removed by clicking on ''hide''. 
</p>
<br />
<p>
<strong>Choose the types of stop point you would like to use</strong>
<br />
Untick the types of transport stop or locations that you are not interested in using. However, at least one type must remain ticked.
For a town/district/village the journey planner will search for journeys that start from the centre of the selected location.
</p>'


-- Find Nearest Accessible Stop - Help content - Printer
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'helpfulljprinter')

EXEC AddtblContent
@ThemeId, @GroupId, 'HelpBodyText', '/Channels/TransportDirect/Printer/HelpFindNearestAccessibleStop'
,'<p>
You have been shown this screen because your selected ''from'' and /or ''to'' points for your journey are not able to deliver the accessible journey characteristics you have requested. This page will show you places near your selected points that do meet the accessible journey characteristics. 
You need to select a point each of the ''from'', ''to'' and ''via'' points that you have been shown. The journey will then be planned using new points. 
If no points are shown then there are no places near your chosen point that match your selected stop point types and your chosen accessible journey requirements. In this case you may need to amend the type of stop or return to the input screen to amend your accessibility requirements. 
Once you have selected your points please click ''next'' to plan your journey.
</p>
<br />
<p>
<strong>View on map</strong>
<br />
You can see the stops and locations on a map by clicking ''View on Map'' for each of the ''from'', ''to'' and via stops list. 
Once you have a map displayed this can be removed by clicking on ''hide''. 
</p>
<br />
<p>
<strong>Choose the types of stop point you would like to use</strong>
<br />
Untick the types of transport stop or locations that you are not interested in using. However, at least one type must remain ticked.
For a town/district/village the journey planner will search for journeys that start from the centre of the selected location.
</p>'
,
'<p>
You have been shown this screen because your selected ''from'' and /or ''to'' points for your journey are not able to deliver the accessible journey characteristics you have requested. This page will show you places near your selected points that do meet the accessible journey characteristics. 
You need to select a point each of the ''from'', ''to'' and ''via'' points that you have been shown. The journey will then be planned using new points. 
If no points are shown then there are no places near your chosen point that match your selected stop point types and your chosen accessible journey requirements. In this case you may need to amend the type of stop or return to the input screen to amend your accessibility requirements. 
Once you have selected your points please click ''next'' to plan your journey.
</p>
<br />
<p>
<strong>View on map</strong>
<br />
You can see the stops and locations on a map by clicking ''View on Map'' for each of the ''from'', ''to'' and via stops list. 
Once you have a map displayed this can be removed by clicking on ''hide''. 
</p>
<br />
<p>
<strong>Choose the types of stop point you would like to use</strong>
<br />
Untick the types of transport stop or locations that you are not interested in using. However, at least one type must remain ticked.
For a town/district/village the journey planner will search for journeys that start from the centre of the selected location.
</p>'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10020
SET @ScriptDesc = 'Updates to Help text for Find Nearest Accessible Stop page'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.0  $'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc, VersionInfo = @VersionInfo
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary, VersionInfo)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc, @VersionInfo)
  END
GO