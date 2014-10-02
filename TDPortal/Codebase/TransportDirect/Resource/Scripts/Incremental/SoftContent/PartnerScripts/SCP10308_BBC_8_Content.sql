-- *************************************************************************************
-- NAME 		: SCP10307_BBC_7_Accessibility.sql
-- DESCRIPTION 	: Script to add specific content for a Theme - BBC
-- AUTHOR		: David Lane
-- DATE         : 27/10/2011
-- *************************************************************************************

-- ************************************************
-- NOTE: Currently only affects the home page right hand panel
-- ************************************************


USE [Content]
GO
DECLARE @GroupId int
DECLARE @ThemeId INT
SET @ThemeId = 3
SET @GroupId = 15 
---------------------------------------------------------------
-- Home Page - Right hand info panel
---------------------------------------------------------------


-- Right hand items 1 and 2
EXEC AddtblContent
@ThemeId, @GroupId, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home'
, 
'<div class="Column3Header">
<div class="txtsevenbbl">
  Follow Us
</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoCO2" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">Get the latest travel news alerts and find out about Transport Direct developments by connecting with us on Facebook and Twitter.
		<br/>
		<a target="_child" href="http://www.facebook.com/home.php/#!/174434289241225">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/facebook_logo_16.gif" alt="(opens in new window)" />
			Find us on Facebook
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>
		<br/>
		<a target="_child" href="http://www.twitter.com/tdinfo">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter_bird_16_blue.gif" alt="(opens in new window)" />
			Follow us on Twitter
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Free webmaster tools</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					Our pdf helpsheets give simple instructions on how to add Transport Direct features to your website.  Download the full set or try 
					<a target="_child" href="/Web2/Downloads/1a - link to Transport Direct with pre-set destination using postcode.pdf">
						Helpsheet 1a 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>.  
					<br/>
					<br/>
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						Transport Direct helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">On your bike? / </div>  
<tr>
<div class="txtsevenbbl">Going Somewhere solo?</div>  
</table>
</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Why not go by bike? our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> now covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>'
, 
'<div class="Column3Header">
<div class="txtsevenbbl">
  Follow Us
</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoCO2" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">Get the latest travel news alerts and find out about Transport Direct developments by connecting with us on Facebook and Twitter.
		<br/>
		<a target="_child" href="http://www.facebook.com/home.php/#!/174434289241225">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/facebook_logo_16.gif" alt="(opens in new window)" />
			Find us on Facebook
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>
		<br/>
		<a target="_child" href="http://www.twitter.com/tdinfo">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter_bird_16_blue.gif" alt="(opens in new window)" />
			Follow us on Twitter
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Free webmaster tools</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					Our pdf helpsheets give simple instructions on how to add Transport Direct features to your website.  Download the full set or try 
					<a target="_child" href="/Web2/Downloads/1a - link to Transport Direct with pre-set destination using postcode.pdf">
						Helpsheet 1a 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>.  
					<br/>
					<br/>
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						Transport Direct helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">On your bike? / </div>  
<tr>
<div class="txtsevenbbl">Going Somewhere solo?</div>  
</table>
</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Why not go by bike? our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> now covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>'


-- Right hand item 3
EXEC AddtblContent
@ThemeID, @GroupId, 'TDNewInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home',
'',''










GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10308
SET @ScriptDesc = 'Updates to BBC home page right hand panel content'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.2  $'

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

