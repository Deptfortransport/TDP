-- ***********************************************
-- NAME 		: SC10006_TransportDirect_Content_6_Home_TipsToolsPanel.sql
-- DESCRIPTION 	: Script to add the list items in the Tips and Tools panel
-- AUTHOR		: Mitesh Modi
-- DATE			: 16 Apr 2008 15:00:00
-- ************************************************


USE [Content]
GO


DECLARE @GroupId int,
	@ThemeId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'home')


-- Tips and Tools panel on homepage
EXEC AddtblContent
1, @GroupId, 'TDTipsHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home'
, '<div class="Column2Header">  
<h2><a class="Column2HeaderLink" title="Go to Tips and tools page" href="/Web2/Tools/Home.aspx"><span class="txtsevenbwl">Tips and tools</span></a></h2>
<a class="txtsevenbwrlink" title="Go to Tips and tools page" href="/Web2/Tools/Home.aspx">More... </a>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
</div>  
<div></div>  
<div class="Column2Content2">  
<table class="TipsToolsTable" cellspacing="5">  
<tbody>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx"><img title="Batch Journey Planner" alt="Batch Journey Planner" src="/Web2/App_Themes/TransportDirect/images/gifs/Misc/Batch_sm.gif" /></a></td> <td><a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">Batch Journey Planner</a></td></tr>
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx"><img title="Check journey CO2" height="30" alt="Check CO2 emissions" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/Co2_30x30.gif" width="30" /></a></td>  <td><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">Check CO2 emissions</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/BusinessLinks.aspx"><img title="Business Links" height="30" alt="Business Links" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/BusinessLinks.aspx">Add Transport Direct to your website for free</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/ToolbarDownload.aspx"><img title="Toolbar download" height="30" alt="Toolbar download" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/Toolbox30.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/ToolbarDownload.aspx">Speed up your travel searches with our free toolbar</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/TDOnTheMove/TDOnTheMove.aspx"><img title="Services available on your mobile" height="30" alt="Services available on your mobile" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif" width="30" /></a></td>  <td><a href="/Web2/TDOnTheMove/TDOnTheMove.aspx">Get live departures on your mobile</a></td></tr>
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/JourneyPlanning/FindEBCInput.aspx"><img title="Freight Grants - Environmental Benefits Calculator" alt="Freight Grants - Environmental Benefits Calculator" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/EnvironmentalBenefitsCalculator_small.gif" /></a></td> <td><a href="/Web2/JourneyPlanning/FindEBCInput.aspx">Freight Grants - Environmental Benefits Calculator</a></td></tr>
</tbody></table></div>'

, '<div class="Column2Header">  
<h2><a class="Column2HeaderLink" title="Ewch i''r dudalen Awgrymiadau a thedynnau" href="/Web2/Tools/Home.aspx"><span class="txtsevenbwl">Awgrymiadau a theclynnau</span></a></h2>
<a class="txtsevenbwrlink" title="Ewch i''r dudalen Awgrymiadau a thedynnau" href="/Web2/Tools/Home.aspx">Mwy... </a>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
</div>  
<div></div>  
<div class="Column2Content2">  
<table class="TipsToolsTable" cellspacing="5">  <tbody>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx"><img title="B" alt="Freight Grants - Environmental Benefits Calculator" src="/Web2/App_Themes/TransportDirect/images/gifs/Misc/Batch_sm.gif" /></a></td> <td><a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">Batch Journey Planner</a></td></tr>
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx"><img title="Mesur CO2 y siwrnai" height="30" alt="Mesur CO2 y siwrnai" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/cy/Co2_30x30.gif" width="30" /></a></td>  <td><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">Mesur CO2 y siwrnai</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/BusinessLinks.aspx"><img title="Cysylltiadau Busnes" height="30" alt="Cysylltiadau Busnes" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/BusinessLinks.aspx">Ychwanegwch Transport Direct at eich gwefan am ddim</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/ToolbarDownload.aspx"><img title="Lawrlwythwch y Bar Offer" height="30" alt="Lawrlwythwch y Bar Offer" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/Toolbox30.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/ToolbarDownload.aspx">Cyflymwch eich chwiliadau teithio gyda''n bar offer am ddim</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/TDOnTheMove/TDOnTheMove.aspx"><img title="Gwasanaethau sydd ar gael ar eich ff&#244;n symudol" height="30" alt="Gwasanaethau sydd ar gael ar eich ff&#244;n symudol" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif" width="30" /></a></td>  <td><a href="/Web2/TDOnTheMove/TDOnTheMove.aspx">Derbyniwch ymadawiadau byw ar eich ff&#244;n symudol</a></td></tr>
</tbody></table></div>'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10006
SET @ScriptDesc = 'Script to add Homepage Tips and Tools panel list items'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.12  $'

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
