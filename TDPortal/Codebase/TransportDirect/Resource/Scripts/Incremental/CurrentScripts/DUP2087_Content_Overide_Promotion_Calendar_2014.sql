-- ***********************************************
-- NAME           : DUP2087_Content_Overide_Promotion_Calendar_2014.sql
-- DESCRIPTION    : Script tocreate 2014 sheduled Promo links in overrides table
--					until 31/12/2014 
-- DATE           : 03/12/2013
-- Author         : P Scott
-- ***********************************************

USE [Content]
GO

--Delete Old expired Content Overrides prior to Dec2013
Delete from dbo.tblContentOverride 
where StartDate <=  '2013-12-01' and EndDate <= '2013-12-31'
GO 

-------------------------------------------------------------------------------------------------------------------------------------------------
--  DECEMBER 2013 DECEMBER 2013  DECEMBER 2013  DECEMBER 2013 DECEMBER 2013 DECEMBER 2013  
-------------------------------------------------------------------------------------------------------------------------------------------------
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">Get ready for winter  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">
    <img title="Met Office winter advice" alt="Met Office winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/getreadyforwinter.JPG" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Christmas shopping? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving...
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
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

'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">Get ready for winter  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">
    <img title="Met Office winter advice" alt="Met Office winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/getreadyforwinter.JPG" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Christmas shopping? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving...
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
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

'
,'2013-12-01','2013-12-31'


EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">Get ready for winter  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">
    <img title="Met Office winter advice" alt="Met Office winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/getreadyforwinter.JPG" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Christmas shopping? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving...
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
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

'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">Get ready for winter  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">
    <img title="Met Office winter advice" alt="Met Office winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/getreadyforwinter.JPG" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Christmas shopping? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving...
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
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

'
,'2013-12-01','2013-12-31'


-------------------------------------------------------------------------------------------------------------------------------------------------
--  JANUARY 2014  JANUARY 2014  JANUARY 2014  JANUARY 2014  JANUARY 2014  JANUARY 2014  JANUARY 2014  JANUARY 2014  JANUARY 2014  JANUARY 2014  
-------------------------------------------------------------------------------------------------------------------------------------------------
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">Winter Driving advice  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">
    <img title="Highways Agency winter advice" alt="Highways Agency winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/green-TD.gif" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
',
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">Winter Driving advice  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">
    <img title="Highways Agency winter advice" alt="Highways Agency winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/green-TD.gif" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
'
,'2014-01-01','2014-01-31'
GO
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">Winter Driving advice  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">
    <img title="Highways Agency winter advice" alt="Highways Agency winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/green-TD.gif" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
',
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">Winter Driving advice  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">
    <img title="Highways Agency winter advice" alt="Highways Agency winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/green-TD.gif" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
'
,'2014-01-01','2014-01-31'




-------------------------------------------------------------------------------------------------------------------------------------------------
--  FEBRUARY 2014  FEBRUARY 2014  FEBRUARY 2014  FEBRUARY 2014  FEBRUARY 2014  FEBRUARY 2014  FEBRUARY 2014  FEBRUARY 2014  FEBRUARY 2014  
-------------------------------------------------------------------------------------------------------------------------------------------------
GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Now in tablet form</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		A scaled down version of our journey planner is now available on mobile devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">Winter Driving advice  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">
    <img title="Highways Agency winter advice" alt="Highways Agency winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/green-TD.gif" /></a></td>  
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


'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Now in tablet form</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		A scaled down version of our journey planner is now available on mobile devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">Winter Driving advice  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">
    <img title="Highways Agency winter advice" alt="Highways Agency winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/green-TD.gif" /></a></td>  
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

'
,'2014-02-01','2014-02-28'
GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Now in tablet form</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		A scaled down version of our journey planner is now available on mobile devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">Winter Driving advice  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">
    <img title="Highways Agency winter advice" alt="Highways Agency winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/green-TD.gif" /></a></td>  
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


'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Now in tablet form</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		A scaled down version of our journey planner is now available on mobile devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">Winter Driving advice  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.highways.gov.uk/traffic-information/seasonal-advice/make-time-for-winter/">
    <img title="Highways Agency winter advice" alt="Highways Agency winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/green-TD.gif" /></a></td>  
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

'
,'2014-02-01','2014-02-28'


-------------------------------------------------------------------------------------------------------------------------------------------------
--  MARCH 2014  MARCH 2014  MARCH 2014  MARCH 2014  MARCH 2014  MARCH 2014  MARCH 2014  MARCH 2014  
-------------------------------------------------------------------------------------------------------------------------------------------------
GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">My other car is a bus</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHbus" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/bus_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Looking for a journey that only uses buses? Try our 
        <a href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">Find a bus</a> planner.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter  
</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport Direct news and tips.
		<br/>
		</a>
		<br/>
		<a target="_child" href="http://www.twitter.com/tdinfo">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/ twitter_bird_16_blue.gif" alt="(opens in new window)" />
			Transport Direct on Twitter (@tdinfo)
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
	

<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="http://m.transportdirect.info">Mobile journey planner</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif"
          border="0" />
        </td>
        <td class="txtseven">Need directions on your smartphone? Try our new mobile-optimised planner 
        <a href="http://m.transportdirect.info">m.transportdirect.info</a>.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
	
'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">My other car is a bus</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHbus" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/bus_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Looking for a journey that only uses buses? Try our 
        <a href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">Find a bus</a> planner.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter  
</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport Direct news and tips.
		<br/>
		</a>
		<br/>
		<a target="_child" href="http://www.twitter.com/tdinfo">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/ twitter_bird_16_blue.gif" alt="(opens in new window)" />
			Transport Direct on Twitter (@tdinfo)
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
	

<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="http://m.transportdirect.info">Mobile journey planner</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif"
          border="0" />
        </td>
        <td class="txtseven">Need directions on your smartphone? Try our new mobile-optimised planner 
        <a href="http://m.transportdirect.info">m.transportdirect.info</a>.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

'
,'2014-03-01','2014-03-31'
GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">My other car is a bus</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHbus" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/bus_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Looking for a journey that only uses buses? Try our 
        <a href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">Find a bus</a> planner.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter  
</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport Direct news and tips.
		<br/>
		</a>
		<br/>
		<a target="_child" href="http://www.twitter.com/tdinfo">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/ twitter_bird_16_blue.gif" alt="(opens in new window)" />
			Transport Direct on Twitter (@tdinfo)
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
	

<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="http://m.transportdirect.info">Mobile journey planner</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif"
          border="0" />
        </td>
        <td class="txtseven">Need directions on your smartphone? Try our new mobile-optimised planner 
        <a href="http://m.transportdirect.info">m.transportdirect.info</a>.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
	
'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">My other car is a bus</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHbus" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/bus_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Looking for a journey that only uses buses? Try our 
        <a href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">Find a bus</a> planner.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter  
</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport Direct news and tips.
		<br/>
		</a>
		<br/>
		<a target="_child" href="http://www.twitter.com/tdinfo">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/ twitter_bird_16_blue.gif" alt="(opens in new window)" />
			Transport Direct on Twitter (@tdinfo)
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
	

<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="http://m.transportdirect.info">Mobile journey planner</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif"
          border="0" />
        </td>
        <td class="txtseven">Need directions on your smartphone? Try our new mobile-optimised planner 
        <a href="http://m.transportdirect.info">m.transportdirect.info</a>.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

'
,'2014-03-01','2014-03-31'


-------------------------------------------------------------------------------------------------------------------------------------------------
--  APRIL 2014  APRIL 2014  APRIL 2014  APRIL 2014  APRIL 2014  APRIL 2014  APRIL 2014  APRIL 2014  
-------------------------------------------------------------------------------------------------------------------------------------------------
GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
						<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainInput.aspx?cacheparam=0">Bookmark your train times</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTBM" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Got a rail route you use regularly? Why not add it to your Internet Explorer ''favourites''. Plan your journey with  
        <a href="/Web2/JourneyPlanning/FindTrainInput.aspx?cacheparam=0">Find a train</a>, then click the link on the left-hand column.<br/><br/>
	</td>
      </tr>
    </tbody>
  </table>
</div>

'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
						<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainInput.aspx?cacheparam=0">Bookmark your train times</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTBM" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Got a rail route you use regularly? Why not add it to your Internet Explorer ''favourites''. Plan your journey with  
        <a href="/Web2/JourneyPlanning/FindTrainInput.aspx?cacheparam=0">Find a train</a>, then click the link on the left-hand column.<br/><br/>
	</td>
      </tr>
    </tbody>
  </table>
</div>

'
,'2014-04-01','2014-04-30'
GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
						<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainInput.aspx?cacheparam=0">Bookmark your train times</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTBM" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Got a rail route you use regularly? Why not add it to your Internet Explorer ''favourites''. Plan your journey with  
        <a href="/Web2/JourneyPlanning/FindTrainInput.aspx?cacheparam=0">Find a train</a>, then click the link on the left-hand column.<br/><br/>
	</td>
      </tr>
    </tbody>
  </table>
</div>

'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
						<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainInput.aspx?cacheparam=0">Bookmark your train times</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTBM" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Got a rail route you use regularly? Why not add it to your Internet Explorer ''favourites''. Plan your journey with  
        <a href="/Web2/JourneyPlanning/FindTrainInput.aspx?cacheparam=0">Find a train</a>, then click the link on the left-hand column.<br/><br/>
	</td>
      </tr>
    </tbody>
  </table>
</div>

'
,'2014-04-01','2014-04-30'

-------------------------------------------------------------------------------------------------------------------------------------------------
--  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  
-------------------------------------------------------------------------------------------------------------------------------------------------

GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">Two wheels good</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Back in  the saddle? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Now in tablet form</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		A scaled down version of our journey planner is now available on mobile devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
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


'
,
'

<div class="Column3Header">
<div class="txtsevenbbl">Two wheels good</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Back in  the saddle? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Now in tablet form</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		A scaled down version of our journey planner is now available on mobile devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
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
'
,'2014-05-01','2014-05-31'

GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">Two wheels good</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Back in  the saddle? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Now in tablet form</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		A scaled down version of our journey planner is now available on mobile devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
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


'
,
'

<div class="Column3Header">
<div class="txtsevenbbl">Two wheels good</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Back in  the saddle? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Now in tablet form</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		A scaled down version of our journey planner is now available on mobile devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
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
'
,'2014-05-01','2014-05-31'




-------------------------------------------------------------------------------------------------------------------------------------------------
--  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  
-------------------------------------------------------------------------------------------------------------------------------------------------

GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
						<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">Walk this way</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/walk.gif" alt="Walkit" title="Walkit" />
				</td> 
				<td class="txtseven">
					Follow the Walkit link in your
						<a href="/Web2/JourneyPlanning/JPLandingPage.aspx?&id=walkpromo">public transport journey</a> 
						for a map and detailed walking directions.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Don''t want to miss a thing?</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Visit
		<a target="_child" href="http://www.twitter.com/tdinfo">
			www.twitter.com/tdinfo
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  for traveltips and the latest news about Transport Direct.
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindFlightInput.aspx?cacheparam=0">Plane Sailing</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHflight" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Our 
        <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?cacheparam=0">Find a flight</a> planner covers domestic air travel within England, Scotland and Wales.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

'
,
'

<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
						<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">Walk this way</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/walk.gif" alt="Walkit" title="Walkit" />
				</td> 
				<td class="txtseven">
					Follow the Walkit link in your
						<a href="/Web2/JourneyPlanning/JPLandingPage.aspx?&id=walkpromo">public transport journey</a> 
						for a map and detailed walking directions.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Don''t want to miss a thing?</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Visit
		<a target="_child" href="http://www.twitter.com/tdinfo">
			www.twitter.com/tdinfo
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  for traveltips and the latest news about Transport Direct.
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindFlightInput.aspx?cacheparam=0">Plane Sailing</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHflight" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Our 
        <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?cacheparam=0">Find a flight</a> planner covers domestic air travel within England, Scotland and Wales.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

'
,'2014-06-01','2014-06-30'


GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
						<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">Walk this way</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/walk.gif" alt="Walkit" title="Walkit" />
				</td> 
				<td class="txtseven">
					Follow the Walkit link in your
						<a href="/Web2/JourneyPlanning/JPLandingPage.aspx?&id=walkpromo">public transport journey</a> 
						for a map and detailed walking directions.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Don''t want to miss a thing?</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Visit
		<a target="_child" href="http://www.twitter.com/tdinfo">
			www.twitter.com/tdinfo
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  for traveltips and the latest news about Transport Direct.
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindFlightInput.aspx?cacheparam=0">Plane Sailing</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHflight" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Our 
        <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?cacheparam=0">Find a flight</a> planner covers domestic air travel within England, Scotland and Wales.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

'
,
'

<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
						<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">Walk this way</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/walk.gif" alt="Walkit" title="Walkit" />
				</td> 
				<td class="txtseven">
					Follow the Walkit link in your
						<a href="/Web2/JourneyPlanning/JPLandingPage.aspx?&id=walkpromo">public transport journey</a> 
						for a map and detailed walking directions.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Don''t want to miss a thing?</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Visit
		<a target="_child" href="http://www.twitter.com/tdinfo">
			www.twitter.com/tdinfo
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  for traveltips and the latest news about Transport Direct.
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindFlightInput.aspx?cacheparam=0">Plane Sailing</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHflight" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Our 
        <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?cacheparam=0">Find a flight</a> planner covers domestic air travel within England, Scotland and Wales.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

'
,'2014-06-01','2014-06-30'






-------------------------------------------------------------------------------------------------------------------------------------------------
--  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  
-------------------------------------------------------------------------------------------------------------------------------------------------
GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Two wheels good</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Back in  the saddle? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
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

'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Two wheels good</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Back in  the saddle? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
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

'
,'2014-07-01','2014-07-31'

GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Two wheels good</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Back in  the saddle? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
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

'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Two wheels good</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Back in  the saddle? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?cacheparam=0">Cycle Planner</a> covers the whole of England.  
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

'
,'2014-07-01','2014-07-31'








-------------------------------------------------------------------------------------------------------------------------------------------------
--  AUGUST 2014 
-------------------------------------------------------------------------------------------------------------------------------------------------

GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">

<a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Train tickets for tight budgets</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>



'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">

<a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Train tickets for tight budgets</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>

'
,'2014-08-01','2014-08-31'
GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">

<a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Train tickets for tight budgets</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>



'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">

<a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Train tickets for tight budgets</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>

'
,'2014-08-01','2014-08-31'





-------------------------------------------------------------------------------------------------------------------------------------------------
--  SEPTEMBER 2014  SEPTEMBER 2014  SEPTEMBER 2014  SEPTEMBER 2014  SEPTEMBER 2014  SEPTEMBER 2014
-------------------------------------------------------------------------------------------------------------------------------------------------

EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">On the buses?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHbus" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/bus_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Looking for a journey that only uses buses and coaches? Try our 
        <a href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">Find a bus</a> planner.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="http://m.transportdirect.info">Mobile journey planner</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif"
          border="0" />
        </td>
        <td class="txtseven">Need directions on your smartphone? Try our new mobile-optimised planner 
        <a href="http://m.transportdirect.info">m.transportdirect.info</a>.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>



'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">On the buses?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHbus" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/bus_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Looking for a journey that only uses buses and coaches? Try our 
        <a href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">Find a bus</a> planner.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="http://m.transportdirect.info">Mobile journey planner</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif"
          border="0" />
        </td>
        <td class="txtseven">Need directions on your smartphone? Try our new mobile-optimised planner 
        <a href="http://m.transportdirect.info">m.transportdirect.info</a>.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>


'
,'2014-09-01','2014-09-30'
GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">On the buses?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHbus" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/bus_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Looking for a journey that only uses buses and coaches? Try our 
        <a href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">Find a bus</a> planner.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="http://m.transportdirect.info">Mobile journey planner</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif"
          border="0" />
        </td>
        <td class="txtseven">Need directions on your smartphone? Try our new mobile-optimised planner 
        <a href="http://m.transportdirect.info">m.transportdirect.info</a>.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>



'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">On the buses?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHbus" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/bus_sm.gif"
          border="0" />
        </td>
        <td class="txtseven">Looking for a journey that only uses buses and coaches? Try our 
        <a href="/Web2/JourneyPlanning/FindBusInput.aspx?cacheparam=0">Find a bus</a> planner.
        <br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="http://m.transportdirect.info">Mobile journey planner</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/phone.gif"
          border="0" />
        </td>
        <td class="txtseven">Need directions on your smartphone? Try our new mobile-optimised planner 
        <a href="http://m.transportdirect.info">m.transportdirect.info</a>.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Link with us</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					It''s easy to create customised links to Transport Direct with your destination, or other journey details, pre-set. Send them in an email or put them on your website.</br>To find out how, download Our 
					<a target="_child" href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						pdf helpsheets (zip file) 
						<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>


'
,'2014-09-01','2014-09-30'

GO







-------------------------------------------------------------------------------------------------------------------------------------------------
--  OCTOBER 2014  OCTOBER 2014  OCTOBER 2014  OCTOBER 2014  OCTOBER 2014  OCTOBER 2014  OCTOBER 2014  
-------------------------------------------------------------------------------------------------------------------------------------------------

EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Christmas travel by train?</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Christmas travel by train?</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

'
,'2014-10-01','2014-10-31'
GO 

EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Christmas travel by train?</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Christmas travel by train?</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx?cacheparam=0">Office move? </a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>


<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" valign="top">
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/batch_sm.gif" alt="Batch Journey Planners" title="Batch Journey Planner" border="0" />
				</td> 
				<td class="txtseven">
					Our  
					<a href="/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx">
						Batch Journey Planner
						
					</a>
can produce statistics and printable directions for a large number of journeys. Ideal for travel planners and it''s free to use.  
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Where''s your app?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		We don''t have an app but our new mobile site is optimised for most devices -
         			<a href="http://m.transportdirect.info">m.transportdirect.info</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

'
,'2014-10-01','2014-10-31'
GO 

-------------------------------------------------------------------------------------------------------------------------------------------------
--  NOVEMBER 2014  NOVEMBER 2014  NOVEMBER 2014  NOVEMBER 2014  NOVEMBER 2014  NOVEMBER 2014  
-------------------------------------------------------------------------------------------------------------------------------------------------

EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Christmas travel by train?</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Can we park this? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving..
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br/>
					<br/>
					Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
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


'
,
'

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Christmas travel by train?</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Can we park this? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving..
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br/>
					<br/>
					Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
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



'
,'2014-11-01','2014-11-30'
GO 


EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Christmas travel by train?</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Can we park this? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving..
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br/>
					<br/>
					Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
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


'
,
'

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Step-free or assistance needed?</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a wheelchair-friendly route or help with boarding? Find accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">

 <a class="Column3HeaderLink" href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Christmas travel by train?</a>

</div>  <!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/train_sm.gif" alt="Door to door" title="Door to door" />
				</td> 
				<td class="txtseven">
					Try our   
					<a href="/Web2/JourneyPlanning/FindTrainCostInput.aspx?cacheparam=0">Find cheaper rail fares</a> tool to find the cheapest train ticket for your journey.
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Can we park this? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving..
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br/>
					<br/>
					Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
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



'
,'2014-11-01','2014-11-30'
GO 


-------------------------------------------------------------------------------------------------------------------------------------------------
-- DECEMBER 2014  DECEMBER 2014  DECEMBER 2014  DECEMBER 2014  DECEMBER 2014  DECEMBER 2014  DECEMBER 2014  DECEMBER 2014  DECEMBER 2014  DECEMBER 2014  
-------------------------------------------------------------------------------------------------------------------------------------------------


EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">Get ready for winter  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">
    <img title="Met Office winter advice" alt="Met Office winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/getreadyforwinter.JPG" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Christmas shopping? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving...
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter  
</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport Direct news and tips.
		<br/>
		</a>
		<br/>
		<a target="_child" href="http://www.twitter.com/tdinfo">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/ twitter_bird_16_blue.gif" alt="(opens in new window)" />
			Transport Direct on Twitter (@tdinfo)
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
	
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

'
,
'

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">Get ready for winter  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">
    <img title="Met Office winter advice" alt="Met Office winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/getreadyforwinter.JPG" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Christmas shopping? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving...
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter  
</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport Direct news and tips.
		<br/>
		</a>
		<br/>
		<a target="_child" href="http://www.twitter.com/tdinfo">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/ twitter_bird_16_blue.gif" alt="(opens in new window)" />
			Transport Direct on Twitter (@tdinfo)
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
	
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

'
,'2014-12-01','2014-12-31'


GO
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">Get ready for winter  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">
    <img title="Met Office winter advice" alt="Met Office winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/getreadyforwinter.JPG" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Christmas shopping? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving...
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter  
</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport Direct news and tips.
		<br/>
		</a>
		<br/>
		<a target="_child" href="http://www.twitter.com/tdinfo">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/ twitter_bird_16_blue.gif" alt="(opens in new window)" />
			Transport Direct on Twitter (@tdinfo)
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
	
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

'
,
'

<div class="Column3Header">
<div class="txtsevenbbl">
  <a class="Column3HeaderLink"
  target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">Get ready for winter  <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoSJP" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
    <tr>
      <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
      <td class="SJPIconPadding"><a target="_child" href="http://www.metoffice.gov.uk/learning/get-ready-for-winter/out-and-about">
    <img title="Met Office winter advice" alt="Met Office winter advice" 
    src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/getreadyforwinter.JPG" /></a></td>  
    </tr>  
    </tbody>
  </table>
</div>

<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Christmas shopping? </div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/parking.gif" alt="Park and ride" title="Park and ride" />
				</td> 
				<td class="txtseven">
         		Park & Ride can be a handy way to avoid city centre driving...
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of car parks close to your destination, including links to info on prices, number of spaces and opening times.
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter  
</div>
<!-- Don''t remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
  <td class="txtseven" valign="top">
          <img title="" alt=" "
          src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg"
          border="0" />
        <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport Direct news and tips.
		<br/>
		</a>
		<br/>
		<a target="_child" href="http://www.twitter.com/tdinfo">
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/ twitter_bird_16_blue.gif" alt="(opens in new window)" />
			Transport Direct on Twitter (@tdinfo)
			<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" />
		</a>  
		<br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>
	
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Accessible Travel Options</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>

'
,'2014-12-01','2014-12-31'
GO
----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2087
SET @ScriptDesc = 'DUP2087_Content_Overide_Promotion_Calendar_2014.sql'

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
