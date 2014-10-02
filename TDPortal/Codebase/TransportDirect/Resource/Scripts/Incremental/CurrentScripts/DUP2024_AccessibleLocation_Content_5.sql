-- ***********************************************
-- NAME 		: DUP2024_AccessibleLocation_Content_5.sql
-- DESCRIPTION 	: Accessibility options - content
-- AUTHOR		: Phil Scott
-- DATE			: 01 May 13
-- ************************************************

USE [Content]
GO


DECLARE @GroupId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'journeyplanning_findnearestaccessiblestop')

DECLARE @ThemeId int
SET @ThemeId = 1

EXEC AddtblContent
@ThemeId, @GroupId, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/JourneyPlanning/FindNearestAccessibleStop', 
'<div class="Column3Header">
	<div class="txtsevenbbl">
		Accessibility Information
	</div>
	<!-- Don''t remove spaces -->&nbsp;&nbsp;
</div>
<div class="Column3Content">
	<table width="100%" cellspacing="0" cellpadding="2" border="0" id="tableAccessibilityInformation">
		<tbody>
			<tr>
				<td class="txtseven">
					The Transport Direct accessible journey planner will plan journeys on a limited
					network where we have identified that the stations/stops and services are step free
					and/or have staff assistance available. The definition of these options can be found 
					by hovering over the information buttons or by checking 
					our <a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx">FAQ</a>
					<br />
				</td>
			</tr>
			<tr>
				<td class="txtseven">
					<br />
					If you are unable to use private transport, you may wish to book an accessible taxi, or make use of community transport. Details of taxi firms that offer accessible cabs can be found on our Station Information pages, and the Community Transport Online website offers information about local schemes at: <a target="_child" href="http://www.ctonline.org.uk" title="Click to view in a new browser window">http://www.ctonline.org.uk<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
					<br />
				</td>
			</tr>
		</tbody>
	</table>
</div>',
'<div class="Column3Header">
	<div class="txtsevenbbl">
		Accessibility Information
	</div>
	<!-- Don''t remove spaces -->&nbsp;&nbsp;
</div>
<div class="Column3Content">
	<table width="100%" cellspacing="0" cellpadding="2" border="0" id="tableAccessibilityInformation">
		<tbody>
			<tr>
				<td class="txtseven">
					The Transport Direct accessible journey planner will plan journeys on a limited
					network where we have identified that the stations/stops and services are step free
					and/or have staff assistance available. The definition of these options can be found 
					by hovering over the information buttons or by checking 
					our <a href="/Web2/Help/HelpAccessibleJourneyPlanning.aspx">FAQ</a>
					<br />
				</td>
			</tr>
			<tr>
				<td class="txtseven">
					<br />
					If you are unable to use private transport, you may wish to book an accessible taxi, or make use of community transport. Details of taxi firms that offer accessible cabs can be found on our Station Information pages, and the Community Transport Online website offers information about local schemes at: <a target="_child" href="http://www.ctonline.org.uk" title="Click to view in a new browser window">http://www.ctonline.org.uk<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
					<br />
				</td>
			</tr>



		</tbody>
	</table>
</div>'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2024
SET @ScriptDesc = 'Accessibility options content'

IF EXISTS (SELECT * FROM [dbo].[ChangeCatalogue] WHERE ScriptNumber = @ScriptNumber)
  BEGIN
    UPDATE [dbo].[ChangeCatalogue]
    SET ChangeDate = getDate(), Summary = @ScriptDesc
    WHERE ScriptNumber = @ScriptNumber
  END
ELSE
  BEGIN
    INSERT INTO [dbo].[ChangeCatalogue] (ScriptNumber, ChangeDate, Summary)
    VALUES (@ScriptNumber, getDate(), @ScriptDesc)
  END
GO