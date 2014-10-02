-- ***********************************************
-- NAME           : DUP2030_UPdatePromLinksSchedule_IncludeRideLondon.sql
-- DESCRIPTION    : Script to modify sheduled Promo links in overrides table to include Ride London info
--					until 05/08/2013 
-- DATE           : 08/07/2013
-- Author         : R Broddle
-- ***********************************************

USE [Content]
GO

--Delete August chunk as we are replacing it with 2 seperate bits
Delete from dbo.tblContentOverride 
where ControlName = 'TDAdditionalInformationHtmlPlaceholderDefinition'
and PropertyName = '/Channels/TransportDirect/Home'
and StartDate =  '2013-08-01' and EndDate = '2013-08-31'
GO 



-------------------------------------------------------------------------------------------------------------------------------------------------
--  FEBRUARY 2013  FEBRUARY 2013  FEBRUARY 2013  FEBRUARY 2013  FEBRUARY 2013  FEBRUARY 2013  FEBRUARY 2013  FEBRUARY 2013  FEBRUARY 2013  
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
'
 
,'2013-02-01','2013-02-28'


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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
'
,
'2013-02-01','2013-02-28'


-------------------------------------------------------------------------------------------------------------------------------------------------
--  MARCH 2013  MARCH 2013  MARCH 2013  MARCH 2013  MARCH 2013  MARCH 2013  MARCH 2013  MARCH 2013  
-------------------------------------------------------------------------------------------------------------------------------------------------

EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>





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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>





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
,'2013-03-01','2013-03-31'

EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>





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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>





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
,'2013-03-01','2013-03-31'


-------------------------------------------------------------------------------------------------------------------------------------------------
--  APRIL 2013  APRIL 2013  APRIL 2013  APRIL 2013  APRIL 2013  APRIL 2013  APRIL 2013  APRIL 2013  
-------------------------------------------------------------------------------------------------------------------------------------------------

EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
,'2013-04-01','2013-04-30'

EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
,'2013-04-01','2013-04-30'


-------------------------------------------------------------------------------------------------------------------------------------------------
--  MAY 2013  MAY 2013  MAY 2013  MAY 2013  MAY 2013  MAY 2013  MAY 2013  MAY 2013  MAY 2013  MAY 2013  
-------------------------------------------------------------------------------------------------------------------------------------------------

EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
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
<div class="Column3Header">
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
'
, 
'

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
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
<div class="Column3Header">
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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

'
,'2013-05-01','2013-05-31'


EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
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
<div class="Column3Header">
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
'
, 
'

<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
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
<div class="Column3Header">
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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

'
,'2013-05-01','2013-05-31'




-------------------------------------------------------------------------------------------------------------------------------------------------
--  JUNE 2013  JUNE 2013  JUNE 2013  JUNE 2013  JUNE 2013  JUNE 2013  JUNE 2013  JUNE 2013  JUNE 2013  
-------------------------------------------------------------------------------------------------------------------------------------------------

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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Where''s my train?</a>
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
        <td class="txtseven">Need easy access to bus or train times when you''re out and about?  Visit our  
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how.<br/><br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Where''s my train?</a>
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
        <td class="txtseven">Need easy access to bus or train times when you''re out and about?  Visit our  
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how.<br/><br/>
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
,'2013-06-01','2013-06-30'

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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Where''s my train?</a>
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
        <td class="txtseven">Need easy access to bus or train times when you''re out and about?  Visit our  
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how.<br/><br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Where''s my train?</a>
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
        <td class="txtseven">Need easy access to bus or train times when you''re out and about?  Visit our  
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how.<br/><br/>
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
,'2013-06-01','2013-06-30'




-------------------------------------------------------------------------------------------------------------------------------------------------
--  JULY 2013  JULY 2013  JULY 2013  JULY 2013  JULY 2013  JULY 2013  JULY 2013  JULY 2013  JULY 2013  
-------------------------------------------------------------------------------------------------------------------------------------------------
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/TravelNews.aspx">Tailored travel news</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoLT" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For comprehensive travel news in your region, by road or rail, go to our <a href="/Web2/LiveTravel/TravelNews.aspx">Live travel news</a> page
        
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
	<DIV class="txtsevenbbl">Prudential RideLondon</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<br>
	<DIV class="txtsevenbbl">cycle race, 3/4 August</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>
<div class="Column3Content">
  <table id="tableRHrideLondon" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center" width="26">
        </td>
        <td class="txtseven">
        Our car planner now reflects road closures during this event and journeys to or from closed roads will not work. See <a target="_child" href="http://www.tfl.gov.uk/gettingaround/27647.aspx">RideLondon<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" /></a> for full details of planned closures and diversions.
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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


'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/TravelNews.aspx">Tailored travel news</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoLT" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For comprehensive travel news in your region, by road or rail, go to our <a href="/Web2/LiveTravel/TravelNews.aspx">Live travel news</a> page
        
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
	<DIV class="txtsevenbbl">Prudential RideLondon</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<br>
	<DIV class="txtsevenbbl">cycle race, 3/4 August</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>
<div class="Column3Content">
  <table id="tableRHrideLondon" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center" width="26">
        </td>
        <td class="txtseven">
        Our car planner now reflects road closures during this event and journeys to or from closed roads will not work. See <a target="_child" href="http://www.tfl.gov.uk/gettingaround/27647.aspx">RideLondon<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" /></a> for full details of planned closures and diversions.
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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

'
,'2013-07-01','2013-07-31'

EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/TravelNews.aspx">Tailored travel news</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoLT" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For comprehensive travel news in your region, by road or rail, go to our <a href="/Web2/LiveTravel/TravelNews.aspx">Live travel news</a> page
        
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
	<DIV class="txtsevenbbl">Prudential RideLondon</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<br>
	<DIV class="txtsevenbbl">cycle race, 3/4 August</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>
<div class="Column3Content">
  <table id="tableRHrideLondon" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center" width="26">
        </td>
        <td class="txtseven">
        Our car planner now reflects road closures during this event and journeys to or from closed roads will not work. See <a target="_child" href="http://www.tfl.gov.uk/gettingaround/27647.aspx">RideLondon<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" /></a> for full details of planned closures and diversions.
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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


'
,
'
<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/TravelNews.aspx">Tailored travel news</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoLT" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For comprehensive travel news in your region, by road or rail, go to our <a href="/Web2/LiveTravel/TravelNews.aspx">Live travel news</a> page
        
		</td>
      </tr>
    </tbody>
  </table>
</div>

<div class="Column3Header">
	<DIV class="txtsevenbbl">Prudential RideLondon</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<br>
	<DIV class="txtsevenbbl">cycle race, 3/4 August</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div><div class="Column3Content">
  <table id="tableRHrideLondon" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center" width="26">
        </td>
        <td class="txtseven">
        Our car planner now reflects road closures during this event and journeys to or from closed roads will not work. See <a target="_child" href="http://www.tfl.gov.uk/gettingaround/27647.aspx">RideLondon<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" /></a> for full details of planned closures and diversions.
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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

'
,'2013-07-01','2013-07-31'




-------------------------------------------------------------------------------------------------------------------------------------------------
--  01 - 04 AUGUST 2013 
-------------------------------------------------------------------------------------------------------------------------------------------------


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
	<DIV class="txtsevenbbl">Prudential RideLondon</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<br>
	<DIV class="txtsevenbbl">cycle race, 3/4 August</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>
<div class="Column3Content">
  <table id="tableRHrideLondon" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center" width="26">
        </td>
        <td class="txtseven">
        Our car planner now reflects road closures during this event and journeys to or from closed roads will not work. See <a target="_child" href="http://www.tfl.gov.uk/gettingaround/27647.aspx">RideLondon<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" /></a> for full details of planned closures and diversions.
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
	<DIV class="txtsevenbbl">Prudential RideLondon</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<br>
	<DIV class="txtsevenbbl">cycle race, 3/4 August</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<div class="Column3Content">
  <table id="tableRHrideLondon" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center" width="26">
        </td>
        <td class="txtseven">
        Our car planner now reflects road closures during this event and journeys to or from closed roads will not work. See <a target="_child" href="http://www.tfl.gov.uk/gettingaround/27647.aspx">RideLondon<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" /></a> for full details of planned closures and diversions.
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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


'
,'2013-08-01','2013-08-04'

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
	<DIV class="txtsevenbbl">Prudential RideLondon</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<br>
	<DIV class="txtsevenbbl">cycle race, 3/4 August</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<div class="Column3Content">
  <table id="tableRHrideLondon" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center" width="26">
        </td>
        <td class="txtseven">
        Our car planner now reflects road closures during this event and journeys to or from closed roads will not work. See <a target="_child" href="http://www.tfl.gov.uk/gettingaround/27647.aspx">RideLondon<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" /></a> for full details of planned closures and diversions.
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
	<DIV class="txtsevenbbl">Prudential RideLondon</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
	<br>
	<DIV class="txtsevenbbl">cycle race, 3/4 August</DIV><!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<div class="Column3Content">
  <table id="tableRHrideLondon" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center" width="26">
        </td>
        <td class="txtseven">
        Our car planner now reflects road closures during this event and journeys to or from closed roads will not work. See <a target="_child" href="http://www.tfl.gov.uk/gettingaround/27647.aspx">RideLondon<img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" title="(opens in new window)" /></a> for full details of planned closures and diversions.
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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


'
,'2013-08-01','2013-08-04'



-------------------------------------------------------------------------------------------------------------------------------------------------
--  05 - 31 AUGUST 2013 
-------------------------------------------------------------------------------------------------------------------------------------------------


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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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


'
,'2013-08-05','2013-08-31'

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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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


'
,'2013-08-05','2013-08-31'






-------------------------------------------------------------------------------------------------------------------------------------------------
--  SEPTEMBER 2013  SEPTEMBER 2013  SEPTEMBER 2013  SEPTEMBER 2013  SEPTEMBER 2013  SEPTEMBER 2013
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>


'
,'2013-09-01','2013-09-30'


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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
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
        <td class="txtseven">Stay updated about Transport Direct by connecting with us on Facebook and Twitter.
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>


'
,'2013-09-01','2013-09-30'





-------------------------------------------------------------------------------------------------------------------------------------------------
--  OCTOBER 2013  OCTOBER 2013  OCTOBER 2013  OCTOBER 2013  OCTOBER 2013  OCTOBER 2013  OCTOBER 2013  
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
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
,'2013-10-01','2013-10-31'






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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
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
,'2013-10-01','2013-10-31'



-------------------------------------------------------------------------------------------------------------------------------------------------
--  NOVEMBER 2013  NOVEMBER 2013  NOVEMBER 2013  NOVEMBER 2013  NOVEMBER 2013  NOVEMBER 2013  
-------------------------------------------------------------------------------------------------------------------------------------------------
EXEC AddContentOverride 
'home', 1, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
,'2013-11-01','2013-11-30'



EXEC AddContentOverride 
'home', 3, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/LiveTravel/Home.aspx">Traffic jams or train delays?</a>
</div>
<!-- Don''t remove spaces -->&#160;&#160;&#160;</div>
<div class="Column3Content">
  <table id="tableRHInfoMobile" cellspacing="0" cellpadding="2" width="100%"
  border="0">
    <tbody>
      <tr>
        <td class="txtseven" valign="top" align="center"
        width="26">
        </td>
        <td class="txtseven">
        For travel news that''s relevant to your journey, whether by car or public transport, try our <a href="/Web2/LiveTravel/Home.aspx">Live travel news</a> page.<br/>
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
,'2013-11-01','2013-11-30'



-------------------------------------------------------------------------------------------------------------------------------------------------
--  DECEMBER 2013  DECEMBER 2013  DECEMBER 2013  DECEMBER 2013  DECEMBER 2013  DECEMBER 2013  
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
         		Get directions to a Park & Ride site near you. <br\>
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of nearby car parks, including prices, number of spaces and opening times.
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>




<div class="Column3Header">
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
         		Get directions to a Park & Ride site near you. <br\>
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of nearby car parks, including prices, number of spaces and opening times.
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>




<div class="Column3Header">
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
'
,'2013-12-01','2013-12-25'


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
         		Get directions to a Park & Ride site near you. <br\>
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of nearby car parks, including prices, number of spaces and opening times.
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>




<div class="Column3Header">
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
         		Get directions to a Park & Ride site near you. <br\>
					<a href="/Web2/JourneyPlanning/ParkAndRideInput.aspx">Plan to park & ride</a>
					<br\>
                Alternatively, 
                <a href="/Web2/JourneyPlanning/FindNearestLandingPage.aspx?ft=cp&id=PROMxms">Find a car park</a>
                 has details of nearby car parks, including prices, number of spaces and opening times.
				</td>
			</tr>
		</tbody>
	</table>
</div>


<div class="Column3Header">
<div class="txtsevenbbl">
 <a class="Column3HeaderLink" href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Travel news on your Mobile</a>
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
        <td class="txtseven">See our 
        <a href="/Web2/TDOnTheMove/TDOnTheMove.aspx?cacheparam=0">Mobile/PDA</a> page to find out how to access real-time road and rail travel on your handheld device.<br/><br/>
		</td>
      </tr>
    </tbody>
  </table>
</div>




<div class="Column3Header">
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
'
,'2013-12-01','2013-12-25'




----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 2030
SET @ScriptDesc = 'DUP2030_UPdatePromLinksSchedule_IncludeRideLondon.sql'

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

