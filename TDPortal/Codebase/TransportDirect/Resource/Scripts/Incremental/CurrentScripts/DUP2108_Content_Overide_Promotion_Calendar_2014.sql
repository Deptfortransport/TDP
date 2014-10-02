-- ***********************************************
-- NAME           : DUP2108_Content_Overide_Promotion_Calendar_2014.sql
-- DESCRIPTION    : Script to create 2014 sheduled Promo links in overrides table
--					until 31/12/2014 
-- DATE           : 02/09/2014
-- Author         : D Lane
-- ***********************************************

USE [Content]
GO

--Delete Content Overrides prior to Sep2014 as this superceeds all others
Delete from dbo.tblContentOverride 
where StartDate <=  '2014-09-01' and EndDate <= '2014-09-30'
GO 

----------------------------------------------------------------------------------------------------
---------------------------------------------
--  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  MAY 2014  
----------------------------------------------------------------------------------------------------
---------------------------------------------

GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
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
         		A scaled down version of our journey planner is now available on mobile 
devices -
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
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" 
title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now 
offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To 
Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Free webmaster tools</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free 
Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					Our pdf helpsheets give simple instructions on how to add 
Transport Direct features to your website.  Download the full set or try 
					<a target="_child" href="/Web2/Downloads/1a - link to 
Transport Direct with pre-set destination using postcode.pdf">
						Helpsheet 1a 
						<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>.  
					<br/>
					<br/>
					<a target="_child" 
href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						Transport Direct helpsheets (zip file) 
						<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
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
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
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
         		A scaled down version of our journey planner is now available on mobile 
devices -
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
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" 
title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now 
offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To 
Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Free webmaster tools</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free 
Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					Our pdf helpsheets give simple instructions on how to add 
Transport Direct features to your website.  Download the full set or try 
					<a target="_child" href="/Web2/Downloads/1a - link to 
Transport Direct with pre-set destination using postcode.pdf">
						Helpsheet 1a 
						<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>.  
					<br/>
					<br/>
					<a target="_child" 
href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						Transport Direct helpsheets (zip file) 
						<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
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
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
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
         		A scaled down version of our journey planner is now available on mobile 
devices -
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
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" 
title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now 
offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To 
Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Free webmaster tools</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free 
Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					Our pdf helpsheets give simple instructions on how to add 
Transport Direct features to your website.  Download the full set or try 
					<a target="_child" href="/Web2/Downloads/1a - link to 
Transport Direct with pre-set destination using postcode.pdf">
						Helpsheet 1a 
						<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>.  
					<br/>
					<br/>
					<a target="_child" 
href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						Transport Direct helpsheets (zip file) 
						<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
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
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
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
         		A scaled down version of our journey planner is now available on mobile 
devices -
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
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" alt="Accessibility Options" 
title="Accessibility Options" />
				</td> 
				<td class="txtseven">
         		Need a step-free route or assistance boarding/alighting vehicles? We now 
offer accessible public transport options in our
         			<a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To 
Door Planner</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Free webmaster tools</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70_small.gif" alt="Free 
Tools" title="Free Tools" />
				</td> 
				<td class="txtseven">
					Our pdf helpsheets give simple instructions on how to add 
Transport Direct features to your website.  Download the full set or try 
					<a target="_child" href="/Web2/Downloads/1a - link to 
Transport Direct with pre-set destination using postcode.pdf">
						Helpsheet 1a 
						<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>.  
					<br/>
					<br/>
					<a target="_child" 
href="/Web2/Downloads/Transport_Direct_Helpsheets.zip">
						Transport Direct helpsheets (zip file) 
						<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					</a>
				</td>
			</tr>
		</tbody>
	</table>
</div>
'
,'2014-05-01','2014-05-31'


----------------------------------------------------------------------------------------------------
---------------------------------------------
--  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014  JUNE 2014 
 
----------------------------------------------------------------------------------------------------
---------------------------------------------



GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">Glasgow 2014 and Tour De France</div>  <!-- Don''t remove spaces --
>&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					The <a target="_child" 
href="http://letour.yorkshire.com/road-closures">Tour de France Grand Départ</a> 
					 website has details of planned road closures during the race 
(5-7 July).
					<br/>
					Commonwealth Games travel information can be found at 
Traveline’s 
					<a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle 
planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?
cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Walk this way</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/walk.gif" alt="Walkit" 
title="Walkit" />
				</td> 
				<td class="txtseven">
					Follow the Walkit link in your
						<a href="/Web2/JourneyPlanning/JPLandingPage.aspx?
&id=walkpromo">public transport journey</a> 
						for a map and detailed walking directions.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">Glasgow 2014 and Tour De France</div>  <!-- Don''t remove spaces --
>&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					The <a target="_child" 
href="http://letour.yorkshire.com/road-closures">Tour de France Grand Départ</a> 
					 website has details of planned road closures during the race 
(5-7 July).
					<br/>
					Commonwealth Games travel information can be found at 
Traveline’s 
					<a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle 
planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?
cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Walk this way</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/walk.gif" alt="Walkit" 
title="Walkit" />
				</td> 
				<td class="txtseven">
					Follow the Walkit link in your
						<a href="/Web2/JourneyPlanning/JPLandingPage.aspx?
&id=walkpromo">public transport journey</a> 
						for a map and detailed walking directions.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,'2014-06-01','2014-06-30'



GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">Glasgow 2014 and Tour De France</div>  <!-- Don''t remove spaces --
>&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					The <a target="_child" 
href="http://letour.yorkshire.com/road-closures">Tour de France Grand Départ</a> 
					 website has details of planned road closures during the race 
(5-7 July).
					<br/>
					Commonwealth Games travel information can be found at 
Traveline’s 
					<a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle 
planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?
cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Walk this way</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/walk.gif" alt="Walkit" 
title="Walkit" />
				</td> 
				<td class="txtseven">
					Follow the Walkit link in your
						<a href="/Web2/JourneyPlanning/JPLandingPage.aspx?
&id=walkpromo">public transport journey</a> 
						for a map and detailed walking directions.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">Glasgow 2014 and Tour De France</div>  <!-- Don''t remove spaces --
>&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					The <a target="_child" 
href="http://letour.yorkshire.com/road-closures">Tour de France Grand Départ</a> 
					 website has details of planned road closures during the race 
(5-7 July).
					<br/>
					Commonwealth Games travel information can be found at 
Traveline’s 
					<a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle 
planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?
cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Walk this way</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/walk.gif" alt="Walkit" 
title="Walkit" />
				</td> 
				<td class="txtseven">
					Follow the Walkit link in your
						<a href="/Web2/JourneyPlanning/JPLandingPage.aspx?
&id=walkpromo">public transport journey</a> 
						for a map and detailed walking directions.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,'2014-06-01','2014-06-30'



----------------------------------------------------------------------------------------------------
---------------------------------------------
--  JULY 1 -7  2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  
----------------------------------------------------------------------------------------------------
---------------------------------------------
GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Tour de France, 5-7 July</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		Details of planned road closures and diversions are on the <a target="_child" 
href="http://letour.yorkshire.com/road-closures">Tour de France Grand Départ</a>  website.

				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>




<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Get your wings</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"  title="Flight 
Planner" alt="Flight Planner" />
				</td> 
				<td class="txtseven">
					
					Our <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?
cacheparam=0">Find a flight</a>
	                planner covers domestic air travel within England, Scotland and Wales.        
  
	                <br/>
	                <br/>    
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Tour de France, 5-7 July</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		Details of planned road closures and diversions are on the <a target="_child" 
href="http://letour.yorkshire.com/road-closures">Tour de France Grand Départ</a>  website.

				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>




<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Get your wings</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"  title="Flight 
Planner" alt="Flight Planner" />
				</td> 
				<td class="txtseven">
					
					Our <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?
cacheparam=0">Find a flight</a>
	                planner covers domestic air travel within England, Scotland and Wales.        
  
	                <br/>
	                <br/>    
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,'2014-07-01','2014-07-07'

GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Tour de France, 5-7 July</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		Details of planned road closures and diversions are on the <a target="_child" 
href="http://letour.yorkshire.com/road-closures">Tour de France Grand Départ</a>  website.

				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>




<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Get your wings</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"  title="Flight 
Planner" alt="Flight Planner" />
				</td> 
				<td class="txtseven">
					
					Our <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?
cacheparam=0">Find a flight</a>
	                planner covers domestic air travel within England, Scotland and Wales.        
  
	                <br/>
	                <br/>    
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,
'
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<div class="txtsevenbbl">Tour de France, 5-7 July</div>  
</table>
</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
         		Details of planned road closures and diversions are on the <a target="_child" 
href="http://letour.yorkshire.com/road-closures">Tour de France Grand Départ</a>  website.

				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>




<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Get your wings</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"  title="Flight 
Planner" alt="Flight Planner" />
				</td> 
				<td class="txtseven">
					
					Our <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?
cacheparam=0">Find a flight</a>
	                planner covers domestic air travel within England, Scotland and Wales.        
  
	                <br/>
	                <br/>    
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,'2014-07-01','2014-07-07'


----------------------------------------------------------------------------------------------------
---------------------------------------------
--  JULY 8 - 31  2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  JULY 2014  
----------------------------------------------------------------------------------------------------
---------------------------------------------
GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle 
planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?
cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Get your wings</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"  title="Flight 
Planner" alt="Flight Planner" />
				</td> 
				<td class="txtseven">
					
					Our <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?
cacheparam=0">Find a flight</a>
	                planner covers domestic air travel within England, Scotland and Wales.        
  
	                <br/>
	                <br/>    
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>

<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle 
planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?
cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Get your wings</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"  title="Flight 
Planner" alt="Flight Planner" />
				</td> 
				<td class="txtseven">
					
					Our <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?
cacheparam=0">Find a flight</a>
	                planner covers domestic air travel within England, Scotland and Wales.        
  
	                <br/>
	                <br/>    
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,'2014-07-08','2014-07-31'

GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>




<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle 
planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?
cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Get your wings</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"  title="Flight 
Planner" alt="Flight Planner" />
				</td> 
				<td class="txtseven">
					
					Our <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?
cacheparam=0">Find a flight</a>
	                planner covers domestic air travel within England, Scotland and Wales.        
  
	                <br/>
	                <br/>    
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>




<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<div class="txtsevenbbl">Back in the saddle</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/Cycle_small.gif" alt="Cycle 
planner" title="Cycle planner" />
				</td> 
				<td class="txtseven">
					Need a route by bike? Our
					<a href="/web2/JourneyPlanning/FindCycleInput.aspx?
cacheparam=0">Cycle Planner</a> covers the whole of England.  
					<br/>
				</td>
			</tr>
		</tbody>
	</table>
</div>

<div class="Column3Header">
<div class="txtsevenbbl">Get your wings</div>  <!-- Don''t remove spaces --
>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</div>
<div class="Column3Content">
	<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" width="26">
					<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/JourneyPlanning/en/air_sm.gif"  title="Flight 
Planner" alt="Flight Planner" />
				</td> 
				<td class="txtseven">
					
					Our <a href="/Web2/JourneyPlanning/FindFlightInput.aspx?
cacheparam=0">Find a flight</a>
	                planner covers domestic air travel within England, Scotland and Wales.        
  
	                <br/>
	                <br/>    
				</td>
			</tr>
		</tbody>
	</table>
</div>
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,'2014-07-08','2014-07-31'




----------------------------------------------------------------------------------------------------
---------------------------------------------
--  AUGUST 2014 1-3
----------------------------------------------------------------------------------------------------
---------------------------------------------

GO 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>




<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
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
                <img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" 
alt="Accessibility Options" title="Accessibility Options" />
          </td>
          <td class="txtseven">
             Need a wheelchair-friendly route or help with boarding? Find accessible public transport 
options in our
            <a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
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
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>


'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>




<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
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
                <img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" 
alt="Accessibility Options" title="Accessibility Options" />
          </td>
          <td class="txtseven">
             Need a wheelchair-friendly route or help with boarding? Find accessible public transport 
options in our
            <a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
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
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>


'
,'2014-08-01','2014-08-03'
GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>




<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
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
                <img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" 
alt="Accessibility Options" title="Accessibility Options" />
          </td>
          <td class="txtseven">
             Need a wheelchair-friendly route or help with boarding? Find accessible public transport 
options in our
            <a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
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
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>



'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">Commonwealth Games 2014</div>  <!-- Don''t remove spaces -->&nbsp;</div>




<div class="Column3Content">
	<table id="Table4" border="0" cellpadding="2" cellspacing="0" width="100%">
		<tbody>
			<tr>  
				<td class="txtseven" align="center" valign="top" >
				</td> 
				<td class="txtseven">
					For visitor travel information and bus route changes, please 
see Traveline’s <a target="_child" 
href="http://www.travelinescotland.com/cms/content/News/Games.xhtml">Glasgow 2014 page</a>, which 
will be updated as more details emerge.
					<br/>
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
                <img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" 
alt="Accessibility Options" title="Accessibility Options" />
          </td>
          <td class="txtseven">
             Need a wheelchair-friendly route or help with boarding? Find accessible public transport 
options in our
            <a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
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
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>


'
,'2014-08-01','2014-08-03'












----------------------------------------------------------------------------------------------------
---------------------------------------------
--  AUGUST 2014    4th - 31st
----------------------------------------------------------------------------------------------------
---------------------------------------------

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
                <img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" 
alt="Accessibility Options" title="Accessibility Options" />
          </td>
          <td class="txtseven">
             Need a wheelchair-friendly route or help with boarding? Find accessible public transport 
options in our
            <a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
          </td>
        </tr>
     </tbody>
 </table>
</div>  
    
    
<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter    </div>  <!-- Dont remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
      <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"    border="0">
          <tbody>
              <tr>
                  <td class="txtseven" valign="top">
                  <img title="" alt=" " 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg" border="0" />
                  <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport 
Direct news and tips.<br/></a>
                  <br/>
                  <a target="_child" href="http://www.twitter.com/tdinfo">     <img 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter_bird_16_blue.gif" alt="(opens in new 
window)" />Transport Direct on Twitter (@tdinfo)<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" 
title="(opens in new window)" />
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

    
    
    
    
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
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
                <img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" 
alt="Accessibility Options" title="Accessibility Options" />
          </td>
          <td class="txtseven">
             Need a wheelchair-friendly route or help with boarding? Find accessible public transport 
options in our
            <a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
          </td>
        </tr>
     </tbody>
 </table>
</div>  
    
    
<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter    </div>  <!-- Dont remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
      <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"    border="0">
          <tbody>
              <tr>
                  <td class="txtseven" valign="top">
                  <img title="" alt=" " 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg" border="0" />
                  <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport 
Direct news and tips.<br/></a>
                  <br/>
                  <a target="_child" href="http://www.twitter.com/tdinfo">     <img 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter_bird_16_blue.gif" alt="(opens in new 
window)" />Transport Direct on Twitter (@tdinfo)<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" 
title="(opens in new window)" />
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

    
    
    
    
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>


'

,'2014-08-04','2014-08-31'
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
                <img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" 
alt="Accessibility Options" title="Accessibility Options" />
          </td>
          <td class="txtseven">
             Need a wheelchair-friendly route or help with boarding? Find accessible public transport 
options in our
            <a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
          </td>
        </tr>
     </tbody>
 </table>
</div>  
    
    
<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter    </div>  <!-- Dont remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
      <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"    border="0">
          <tbody>
              <tr>
                  <td class="txtseven" valign="top">
                  <img title="" alt=" " 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg" border="0" />
                  <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport 
Direct news and tips.<br/></a>
                  <br/>
                  <a target="_child" href="http://www.twitter.com/tdinfo">     <img 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter_bird_16_blue.gif" alt="(opens in new 
window)" />Transport Direct on Twitter (@tdinfo)<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" 
title="(opens in new window)" />
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

    
    
    
    
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
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
                <img src="/Web2/App_Themes/TransportDirect/images/gifs/misc/wheelchair.gif" 
alt="Accessibility Options" title="Accessibility Options" />
          </td>
          <td class="txtseven">
             Need a wheelchair-friendly route or help with boarding? Find accessible public transport 
options in our
            <a href="/Web2/JourneyPlanning/JourneyPlannerInput.aspx"> Door To Door Planner</a>
          </td>
        </tr>
     </tbody>
 </table>
</div>  
    
    
<div class="Column3Header">
<div class="txtsevenbbl">Find us on Twitter    </div>  <!-- Dont remove spaces -->&#160;&#160;</div>
<div class="Column3Content">
      <table id="tableRHInfoTwit" cellspacing="0" cellpadding="2" width="100%"    border="0">
          <tbody>
              <tr>
                  <td class="txtseven" valign="top">
                  <img title="" alt=" " 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter36.jpeg" border="0" />
                  <td class="txtseven">Follow or just visit us on Twitter (@tdinfo) for Transport 
Direct news and tips.<br/></a>
                  <br/>
                  <a target="_child" href="http://www.twitter.com/tdinfo">     <img 
src="/Web2/App_Themes/TransportDirect/images/gifs/misc/twitter_bird_16_blue.gif" alt="(opens in new 
window)" />Transport Direct on Twitter (@tdinfo)<img 
src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" 
title="(opens in new window)" />
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

    
    
    
    
<div class="Column3Header">
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-
tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>


'
,'2014-08-04','2014-08-31'



-------------------------------------------------------------------------------------------------------------------------------------------------
--  SEPTEMBER 2014  SEPTEMBER 2014  SEPTEMBER 2014  SEPTEMBER 2014  SEPTEMBER 2014  SEPTEMBER 2014
-------------------------------------------------------------------------------------------------------------------------------------------------
 
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
     
<div class="Column3Header">  
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
  <tr>
    <div class="txtsevenbbl">Closure of Transport Direct website</div>
</table>
</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven">The Transport Direct website will close on 30 September. Please see our 
        <a target="_blank" href="/Web2/downloads/closedownletter.pdf">closedown letter</a> (PDF) for more details.<br/><br/>
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
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>


'
,
'
     
<div class="Column3Header">  
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
  <tr>
    <div class="txtsevenbbl">Closure of Transport Direct website</div>
</table>
</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven">The Transport Direct website will close on 30 September. Please see our 
        <a target="_blank" href="/Web2/downloads/closedownletter.pdf">closedown letter</a> (PDF) for more details.<br/><br/>
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
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>


'
,'2014-09-01','2014-09-30'
GO 
EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
     
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
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>

'
,
'
     
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
<table id="Table3" border="0" cellpadding="2" cellspacing="0" width="100%">
<tr>
<td>
<div class="txtsevenbbl">Money Advice Service</div>  
</td>
</tr>
</table>
</div>

<div class="Column3ContentMASWidget">
	<iframe class="polling" frameborder="0" src="https://partner-tools.moneyadviceservice.org.uk/en/polls/widgets/7">
	</iframe>
</div>


'
,'2014-09-01','2014-09-30'



GO
----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2108
SET @ScriptDesc = 'DUP2108_Content_Overide_Promotion_Calendar_2014.sql'

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
