-- ***********************************************
-- NAME 		: SCP10504_BusinessLink_4_Content.sql
-- DESCRIPTION 	: Script to add specific content for a Theme - BusinessLink
-- AUTHOR		: Phil Scott
-- DATE			: 18 MAR 2008 12:15:00
-- ************************************************

-----------------------------------------------------
-- SOFT CONTENT
-----------------------------------------------------

USE [Content]
GO

DECLARE @ThemeId INT
SET @ThemeId = 6

----------------------------------------------------------
-- Header text
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.headerHomepageLink.AlternateText', 'BusinessLink', 'BusinessLink'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.TDSmallBannerImage.AlternateText', 'BusinessLink', 'BusinessLink'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.defaultActionButton.AlternateText', 'BusinessLink', 'BusinessLink'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'PrintableHeaderControl.transportDirectLogoImg.AlternateText', 'BusinessLink - Public services all in one place', 'BusinessLink - Public services all in one place'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'PrintableHeaderControl.connectingPeopleImg.AlternateText', 'Provided by Transport Direct', 'Provided by Transport Direct'


----------------------------------------------------------
-- Header tab buttons
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.planAJourneyImageButton.AlternateText', 'Plan a journey', 'Cynlluniwch siwrnai'


EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.planAJourneyImageButton.ImageUrl', '/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/en/PlanAJourneyUnSelectedEn.gif', '/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/cy/PlanAJourneyUnSelectedCy.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HeaderControl.planAJourneySelectedImageButton.ImageUrl', '/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/en/PlanAJourneySelectedEn.gif', '/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/cy/PlanAJourneySelectedCy.gif'
GO






----------------------------------------------------------
-- Footer links
----------------------------------------------------------
DECLARE @ThemeId INT
SET @ThemeId = 6
EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'FooterControl1.ContactUsLinkButton', 'Contact Transport Direct', 'Cysylltu â Transport Direct '

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'FooterControl1.AboutLinkButton', 'About Transport Direct', 'Amdanom Transport Direct '

----------------------------------------------------------
-- Contact us page
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'FeedbackInitialPage.ContactUsLabel', 'Contact Transport Direct', 'cy Contact Transport Direct'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'FeedbackInitialPage.labelTitle.Text', 'Send Transport Direct your feedback', 'cy Send Transport Direct your feedback'


EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HomeTipsTools.imageLinkToUs.AlternateText','Add Transport Direct to your website','Add Transport Direct to your website'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HomeTipsTools.lblLinkToUs','Add Transport Direct <br />to your website','Add Transport Direct <br />to your website'

EXEC AddtblContent
@ThemeId, 1, 'Tools', 'BusinessLinks.BusinessLinksHeader.Text','Add Transport Direct <br />to your website','Add Transport Direct <br />to your website'



EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HomeTipsTools.imageFeedbackSkipLink.AlternateText', 'Skip to Send Transport Direct your feedback', 'cy Skip to Send Transport Direct your feedback'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'HomeTipsTools.lblFeedback', 'Send Transport Direct <br /> your feedback', 'cy Send Transport Direct <br />your feedback'

----------------------------------------------------------
-- About us page
----------------------------------------------------------



EXEC AddtblContent
@ThemeId, 19, 'TitleText', '/Channels/TransportDirect/About/AboutUs',
'<div>
    <h1>
        About Transport Direct
    </h1>
</div>'
,
'<div>
    <h1>
        Amdanom Transport Direct
    </h1>
</div>'

----------------------------------------------------------
-- HomePage - Tips and Tools panel
----------------------------------------------------------

EXEC AddtblContent
@ThemeId, 15, 'TDTipsHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', 
'<div class="Column2Header">
<h2><a class="Column2HeaderLink" title="Go to Tips and tools page" href="/Web2/Tools/Home.aspx"><span class="txtsevenbwl">Tips and tools</span></a></h2>
<a class="txtsevenbwrlink" title="Go to Tips and tools page" href="/Web2/Tools/Home.aspx">More... </a>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </div>
<div></div>
<div class="clearboth"></div><div class="Column2Content2">
<table class="TipsToolsTable" cellspacing="5">
<tbody>
<tr>
<td class="TipsToolsIconPadding"><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">
<img title="Check journey CO2" height="30" alt="Check CO2 emissions" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/Co2_30x30.gif" width="30" /></a></td>
<td><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">Check CO2 emissions</a></td></tr>
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/BusinessLinks.aspx"><img title="Business Links" height="30" alt="Business Links" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/BusinessLinks.aspx">Add Transport Direct to your website for free</a></td></tr>  

<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/about/RelatedSites.aspx"><img title="Related Sites" height="30" alt="Related Links" src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/relatedlinksSmallicon.gif" width="30" /></a></td>  <td><a href="/Web2/about/RelatedSites.aspx">Related Sites</a></td></tr>  


<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/help/NewHelp.aspx"><img title="Frequently Asked Questions" height="30" alt="FAQs" src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/faqSmallicon.gif" width="30" /></a></td>  <td><a href="/Web2/Help/NewHelp.aspx">Frequently Asked Questions</a></td></tr>  


</tbody></table></div>', 
'<div class="Column2Header">
<h2><a class="Column2HeaderLink" title="Ewch i''r dudalen Awgrymiadau a thedynnau" href="/Web2/Tools/Home.aspx"><span class="txtsevenbwl">Awgrymiadau a theclynnau</span></a></h2>
<a class="txtsevenbwrlink" title="Ewch i''r dudalen Awgrymiadau a thedynnau" href="/Web2/Tools/Home.aspx">Mwy... </a><!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </div>
<div></div>
<div class="clearboth"></div><div class="Column2Content2">
<table class="TipsToolsTable" cellspacing="5">
<tbody>
<tr>
<td class="TipsToolsIconPadding"><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx"><img title="Mesur CO2 y siwrnai" height="30" alt="Mesur CO2 y siwrnai" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/cy/Co2_30x30.gif" width="30" /></a></td>
<td><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">Mesur CO2 y siwrnai</a></td></tr>
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/BusinessLinks.aspx"><img title="Cysylltiadau Busnes" height="30" alt="Cysylltiadau Busnes" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/BusinessLinks.aspx">Ychwanegwch Transport Direct at eich gwefan am ddim</a></td></tr>  
<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/Tools/BusinessLinks.aspx"><img title="Cysylltiadau Busnes" height="30" alt="Cysylltiadau Busnes" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/en/WebsiteLinkB70.gif" width="30" /></a></td>  <td><a href="/Web2/Tools/BusinessLinks.aspx">Ychwanegwch Transport Direct at eich gwefan am ddim</a></td></tr> 


<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/about/RelatedSites.aspx"><img title="Related Sites" height="30" alt="Related Links" src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/relatedlinksSmallicon.gif" width="30" /></a></td>  <td><a href="/Web2/about/RelatedSites.aspx">Related Sites</a></td></tr>  



<tr>  <td class="TipsToolsIconPadding"><a href="/Web2/help/NewHelp.aspx"><img title="Frequently Asked Questions" height="30" alt="FAQs" src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/faqSmallicon.gif" width="30" /></a></td>  <td><a href="/Web2/Help/NewHelp.aspx">Frequently Asked Questions</a></td></tr>  

 
</tbody></table></div>'


GO




------------------------------------------------------------------------
-- Plan a Journey Home page information panel
------------------------------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 6


EXEC AddtblContent
@ThemeId, 18, 'PlanAJourneyInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/JourneyPlanning/Home'
-- ENGLISH
,
'<div class="MinihomeHyperlinksInfoContent">
  <div class="MinihomeHyperlinksInfoHeader">
  <div class="txtsevenbbl">
    <h2>A quick guide to the Journey Planners</h2>
  </div>
  <!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</div>
  <div class="MinihomeSoftContent">
    <p>
    <br clear="left" />
    You can plan a journey in a number of different ways to suit
    your needs.</p>
    <br />
    <h3>Plan me a journey door to door...</h3><img alt="Image showing the quick planner icons on Transport Direct"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeJourneyPlannerBlueBackgroundSmall.gif"
    border="0" align="middle" />
    
    <br />
    <p> 
       <img style="PADDING-RIGHT: 10px" alt="An image showing a summary of journey results"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/JourneyMiniHomePageDetail.jpg"
    align="left" border="0" /> The simplest way to plan your journey is to use the door-to-door journey planner above. This searches for up to five journey options - by joined-up public transport or by car. 
       Just enter your origin and destination and the results will show step-by-step directions, 
       including any connections you need to make, station details, 
       interchange times or driving instructions if you have selected the car journey.
    </p>
    <br />

    <p>  <br clear="left" />
    <img style="PADDING-RIGHT: 10px"
    alt="An image showing a map of journey results"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/JourneyMiniHomePageMap.jpg"
    align="left" border="0" />
    By clicking the ''Maps'' or ''show map'' button you can see a map of your entire journey, or you can look at the individual sections by clicking the map button next to that leg.</p>
    <br clear="left" />
   
    
    <p>&#160;</p>
    <p>By clicking "Tickets/Costs", you can check the prices and 
    availability of rail and coach tickets and buy your tickets from partner retailer sites.</p>
    <br />
    
    
    <p>&#160;</p>
    <h3>I know how I want to travel...</h3>
    <p>&#160;</p>
    <p>Perhaps you have already decided what form of transport to
    use for the main part of your journey. 
    
    <br />In that case use our single mode planners to  
    <a href="/Web2/JourneyPlanning/FindTrainInput.aspx">Find a
    train</a>, 
    <a href="/Web2/JourneyPlanning/FindFlightInput.aspx">Find a
    flight</a>, 
    <a href="/Web2/JourneyPlanning/FindCarInput.aspx">Find a car
    route</a>, 
    <a href="/Web2/JourneyPlanning/FindCoachInput.aspx">Find a
    coach</a>&#160;or 
    <a href="/Web2/JourneyPlanning/FindBusInput.aspx">Find a
    bus.</a>&#160;These will list journeys for just that
    type of transport.</p>
    <br />
    
    <img alt="Image showing the quick planner icons on Transport Direct"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomePlanAJourneyQuickPlanners5.JPG"
    border="0" />
    <p>&#160;</p>
    <p>By using the ''Find cheaper rail fares'' or ''Find a train'' planner you can search by price for a rail ticket. 
    A search by price allows you to choose from a range of fares before you see the journey details relevant to that ticket price.</p>
    <p>&#160;</p>
    <p>You can also use the <a href="/Web2/JourneyPlanning/FindTrunkInput.aspx">city to city</a> journey planner to quickly 
       compare train, plane, coach and car journeys between two cities or towns in Britain.</p>
    <p>&#160;</p>
    <p>If you chose to complete your journey by car, you can find the nearest car park to your origin or destination 
       by clicking ''Drive to a car park'' and following the instructions on the screen.</p>
    <p>&#160;</p>
    <h3>Plan me a cycle route&#8230;</h3>
    <p>&#160;</p>
    <p>Transport Direct''s ''Find a cycle route'' planner is designed to plan local cycle journeys within 
    urban areas. You can choose from the quickest journey, one that takes the quietest route, or a 
    recreational journey which will take as many green places as possible. Details of the journey will be displayed along with the graph showing the gradient of the journey.</p>
    <p>&#160;</p>
    <br class="clearboth" />
    <img style="PADDING-RIGHT: 10px"
    alt="An image showing a Cycle Planner details page"
    title="An image showing a Cycle Planner details page"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/findCycleDetails.gif"
    align="left" border="0" />
    <br clear="left" />
    <p>The cycle planner covers many areas of Great Britain and we are regularly adding more areas.
    Please visit our <a href="/Web2/Help/HelpCycle.aspx#A16.2">FAQs</a> to see if the cycle planner covers your area.</p>

    <p>&#160;</p>
    <br class="clearboth" />
    <h3>Amending my chosen journey</h3>
    <p>&#160;</p>
    <p>Once you have found a journey you want, you can modify your journey:.</p>
    <table class="txtseven" cellspacing="0" cellpadding="2"
    border="0">
      <tbody>
        <tr>
          <td>
            <img alt="Image representing the action of adding to the overall journey"
            src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyExtend.JPG"
            border="0" />
          </td>
          <td>Add a connecting journey - find the main part of your journey and then
          extend the plan to show how to get from the station to your door by car or public transport.</td>
        </tr>
        <tr>
          <td>
            <img alt="Image representing the action of changing the journey plan"
            src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyReplace.JPG"
            border="0" />
          </td>
          <td>Combine public transport and car - replace a section of your journey
          that uses public transport with a car journey.</td>
        </tr>
        <tr>
          <td>
            <img alt="Image representing the action of adjusting the timings of a journey"
            src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyAdjust.JPG"
            border="0" />
          </td>
          <td>Adjust the timings within your journey - allow more time at the places
          where you have to change transport and re-plan your journey accordingly.</td>
        </tr>
	<tr>
          <td>
	    <img alt="Image of the &#8216;Amend date and time&#8217; feature"
            src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyAmendJourney.gif"
            border="0" />
          </td>
          <td>Change your origin and/or destination and/or your travel date and time by 
          clicking this icon or the "Amend" button at the top of the page. You could
          also use the "Amend date and time" feature, at the bottom of the page.</td>
        </tr>
      </tbody>
    </table>
    <p>&#160;</p>
    <h3>Save my journey request</h3>
    <p>&#160;</p>
    <p>Once you have found a journey, you can save it as a bookmark
    ("favourite") in your browser...</p>
    <p>&#160;</p>
    <img alt="Image showing feature to bookmark a journey"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomePlanAJourneyBookmark.JPG"
    border="0" />
    <p>&#160;</p>
    <p>...and retrieve it at a future time from the "Favourites"
    menu in your browser.</p>
    <p>&#160;</p>
    <img alt="Screenshot showing the browser favourites menu bar"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyFavourites.jpg"
    border="0" />

    <p>&#160;</p>
    <h3>Or Share your journey</h3>
    <p>&#160;</p>
    <p>After you plan your journey there are a number of links in the left hand menu which allow you 
     to share your journey with friends using different social networks.</p>
     <p>&#160;</p>
     <br clear="left" />
     
     <p><img alt="Screenshot showing the social network sites"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/SocialNetworks.jpg"
    align="left" border="0" />Simply plan the journey, click the link in the left hand menu and once 
     you log in to the relevant social network site a link will be posted there 
     allowing your friends to see the journey you''ve planned. So if you''re 
     planning a party and you want people to know how to get there or your''e 
     planning to visit friends or family and want to share your travel plans you 
     now have more ways than ever to do it. Don''t forget if you are a registered 
     user you still have the option to email your plans directly to your friends too. </p>

  </div>
</div>
'
-- WELSH
,
'<div class="MinihomeHyperlinksInfoContent">
  <div class="MinihomeHyperlinksInfoHeader">
  <div class="txtsevenbbl">
    <h2>Arweiniad cyflym i’r cynllunwyr siwrneion</h2>
  </div>
  <!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&#160;&#160;&#160;&#160;&#160;&#160;&#160;&#160;</div>
  <div class="MinihomeSoftContent">
    <p>
    <br clear="left" />
    Gallwch gynllunio taith mewn nifer o wahanol ffyrdd i gyd-fynd â’ch anghenion.</p>
    <br/>
    <h3>Plan me a journey door to door...</h3><img alt="Image showing the quick planner icons on Transport Direct"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeJourneyPlannerBlueBackgroundSmall.gif"
    border="0" align="middle" />
    <br/>
    <br/>
    <p> 
       <img style="PADDING-RIGHT: 10px" alt="An image showing a summary of journey results"
    src="/Web2/App_Themes/BusinessLinks/images/gifs/SoftContent/JourneyMiniHomePageDetail.jpg"
    align="left" border="0" /> Y ffordd symlaf o gynllunio eich siwrnai yw defnyddio’r cynllunydd siwrnai drws-i-ddrws aml-foddol uchod.  Mae hwn yn chwilio am hyd at bump o 
opsiynau teithio – drwy ddefnyddio dulliau cludiant cyhoeddus cydgysylltiedig neu gar. Mewnbynnwch eich man cychwyn a’ch man gorffen a bydd y 
canlyniadau yn dangos cyfarwyddiadau cam-wrth-gam, yn cynnwys unrhyw newidiadau fydd angen ichi eu gwneud, manylion am orsafoedd, amseroedd 
cyfnewid neu gyfarwyddiadau i yrwyr os ydych wedi dewis y siwrnai mewn car.
    </p>
    <br/>

    <p>  <br clear="left" />
    <img style="PADDING-RIGHT: 10px"
    alt="An image showing a map of journey results"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/JourneyMiniHomePageMap.jpg"
    align="left" border="0" />
    Drwy glicio ar y botwm ‘Mapiau’ neu ‘Dangos map’ gallwch weld map o’ch siwrnai gyfan, neu gallwch edrych ar ddarnau unigol ohoni drwy glicio ar 
fotwm y map ger y darn hwnnw.</p>
    <br clear="left" />
    
    
    <p>&#160;</p>
    <p>Drwy glicio ar ""Tocynnau/Costau"", gallwch ddarganfod prisiau ac argaeledd tocynnau trên a bysiau moethus a phrynu eich tocynnau o wefannau 
partneriaid manwerthu.</p>
    <br/>
    <br/>
    
    <p>&#160;</p>
    <h3>I know how I want to travel...</h3>
    <p>&#160;</p>
    <p>Efallai eich bod eisoes wedi penderfynu pa ddull o gludiant i’w ddefnyddio ar gyfer prif ran eich taith 
    
    <br/>Os felly defnyddiwch ein cynllunwyr un modd: 
    <a href="/Web2/JourneyPlanning/FindTrainInput.aspx">Canfyddwch drên</a>, 
    <a href="/Web2/JourneyPlanning/FindFlightInput.aspx">Canfyddwch ehediad</a>, 
    <a href="/Web2/JourneyPlanning/FindCarInput.aspx">anfyddwch lwybr car</a>, 
    <a href="/Web2/JourneyPlanning/FindCoachInput.aspx">Canfyddwch fws moethus</a>&#160;or 
    <a href="/Web2/JourneyPlanning/FindBusInput.aspx">neu Canfyddwch fws.</a>
    &#160;Bydd y rhain yn rhestru siwrneiau ar gyfer y math hwnnw o gludiant yn unig.</p>
    <br/>
    <br/>
    <img alt="Image showing the quick planner icons on Transport Direct"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomePlanAJourneyQuickPlanners5.JPG"
    border="0" />
    <p>&#160;</p>
    <p>BDrwy ddefnyddio’r cynllunydd ‘Dod o hyd i docynnau trên rhatach’ neu ‘Canfyddwch drên’ gallwch chwilio am docyn trên, yn ôl amser neu yn ôl 
pris. Mae chwilio yn ôl pris yn caniatáu ichi ddewis o amrediad o brisiau cyn ichi weld manylion y daith sy’n berthnasol i’r pris tocyn hwnnw.</p>
    <p>&#160;</p>
    <p>Gallwch ddefnyddio’r cynllunydd siwrnai <a href="/Web2/JourneyPlanning/FindTrunkInput.aspx">dinas i ddinas</a> i gymharu siwrneiau trên, awyren, bws moethus a char rhwng dwy ddinas neu rhwng dwy dref 
ym Mhrydain.</p>
    <p>&#160;</p>
    <p>Os ydych wedi penderfynu teithio mewn car, gallwch ddarganfod y maes parcio agosaf at eich man cychwyn neu eich man gorffen drwy glicio ar 
‘Gyrru i faes parcio’ a dilyn y cyfarwyddiadau ar y sgrîn.</p>
    <p>&#160;</p>
    <h3>Plan me a cycle route&#8230;</h3>
    <p>&#160;</p>
    <p>Mae cynllunydd ‘Canfod Llwybr Beicio’ Transport Direct wedi ei fwriadu i gynllunio siwrneiau beicio lleol mewn ardaloedd trefol. Gallwch ddewis 
y daith gyflymaf, y daith dawelaf neu daith hamdden a fydd yn cynnwys cymaint o lastiroedd ag sydd bosibl. Bydd manylion y daith yn cael eu 
harddangos ynghyd â graff a fydd yn dangos graddiant y daith.</p>
    <p>&#160;</p>
    <br class="clearboth" />
    <img style="PADDING-RIGHT: 10px"
    alt="An image showing a Cycle Planner details page"
    title="An image showing a Cycle Planner details page"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/findCycleDetails.gif"
    align="left" border="0" />
    <br clear="left" />
    <p>Mae’r cynllunydd beicio yn cwmpasu sawl ardal o Brydain Fawr ac rydym yn ychwanegu mwy o ardaloedd yn gyson.  Ewch i’n hadran<a href="/Web2/Help/HelpCycle.aspx#A16.2">COA</a> i weld a yw’r 
cynllunydd beicio yn cynnwys eich ardal chi.</p>

    <p>&#160;</p>
    <br class="clearboth" />
    <h3>Amending my chosen journey</h3>
    <p>&#160;</p>
    <p>nwaith y byddwch wedi darganfod eich dewis siwrnai, gallwch addasu eich taith:.</p>
    <table class="txtseven" cellspacing="0" cellpadding="2"
    border="0">
      <tbody>
        <tr>
          <td>
            <img alt="Image representing the action of adding to the overall journey"
            src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyExtend.JPG"
            border="0" />
          </td>
          <td>Ychwanegwch siwrnai gysylltiol – darganfyddwch brif ran eich taith ac yna estynnwch y cynllun i ddangos sut i deithio o’r orsaf at eich stepen 
drws drwy ddefnyddio car neu gludiant cyhoeddus.</td>
        </tr>
        <tr>
          <td>
            <img alt="Image representing the action of changing the journey plan"
            src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyReplace.JPG"
            border="0" />
          </td>
          <td>Cyfunwch gludiant cyhoeddus a char – rhowch siwrnai car yn lle rhan o’r siwrnai sy’n defnyddio cludiant cyhoeddus.</td>
        </tr>
        <tr>
          <td>
            <img alt="Image representing the action of adjusting the timings of a journey"
            src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyAdjust.JPG"
            border="0" />
          </td>
          <td>Addaswch yr amseroedd o fewn eich siwrnai – caniatewch fwy o amser mewn mannau lle mae’n rhaid ichi wneud newidiadau ac ailgynlluniwch eich 
siwrnai yn unol â hynny</td>
        </tr>
	<tr>
          <td>
	    <img alt="Image of the &#8216;Amend date and time&#8217; feature"
            src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyAmendJourney.gif"
            border="0" />
          </td>
          <td>Newidiwch eich man cychwyn a/neu eich man gorffen a/neu ddyddiad ac amser eich taith drwy glicio ar yr eicon hwn neu ar y botwm ""Diwygio"" ar 
frig y dudalen.  Gallech hefyd ddefnyddio’r nodwedd ""Newid dyddiad ac amser"" ar waelod y dudalen</td>
        </tr>
      </tbody>
    </table>
    <p>&#160;</p>
    <h3>Cadw fy nghais ar gyfer siwrnai</h3>
    <p>&#160;</p>
    <p>Once you have found a journey, you can save it as a bookmark
    ("favourite") in your browser...</p>
    <p>&#160;</p>
    <img alt="Image showing feature to bookmark a journey"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomePlanAJourneyBookmark.JPG"
    border="0" />
    <p>&#160;</p>
    <p>...and retrieve it at a future time from the "Favourites"
    menu in your browser.</p>
    <p>&#160;</p>
    <img alt="Screenshot showing the browser favourites menu bar"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyFavourites.jpg"
    border="0" />

    <p>&#160;</p>
    <h3>Neu rhannwch eich siwrnai</h3>
    <p>&#160;</p>
    <p>Ar ôl cynllunio eich siwrnai mae nifer o ddolennau yn y ddewislen ar y chwith sy’n caniatáu i chi rannu eich siwrnai â ffrindiau gan ddefnyddio 
rhwydweithiau cymdeithasol gwahanol.</p>
     <p>&#160;</p>
     <br clear="left" />
    
     <p><img alt="Screenshot showing the social network sites"
    src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/SocialNetworks.jpg"
    align="left" border="0" />Y cwbl sydd angen ichi ei wneud yw cynllunio’r daith, clicio ar y ddolen yn y ddewislen ar y chwith, ac unwaith y byddwch yn mewngofnodi i’r 
safle rhwydweithio cymdeithasol perthnasol bydd dolen yn cael ei phostio yno gan ganiatáu i’ch ffrindiau weld y daith ydych chi wedi ei 
chynllunio.  Felly, os ydych yn cynllunio parti ac eisiau i bobl wybod sut i gyrraedd yno, neu os ydych yn bwriadu ymweld â ffrindiau neu deulu ac 
eisiau rhannu eich cynlluniau teithio mae gennych fwy o ffyrdd nag erioed o’r blaen o wneud hynny.  Peidiwch ag anghofio, os ydych yn ddefnyddiwr 
cofrestredig mae gennych hefyd yr opsiwn i e-bostio eich cynlluniau yn syth i’ch ffrindiau.</p>

  </div>
</div>
'





GO


------------------------------------------------------------------------
-- Find A Place Home page information panel
------------------------------------------------------------------------



DECLARE @GroupId int,
	@ThemeId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'maps_home')
SET @ThemeId = 6

-- Add the html text
EXEC AddtblContent
@ThemeId, @GroupId, 'FindAPlaceInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Maps/Home'
-- ENGLISH
,
'<div class="MinihomeHyperlinksInfoContent">
<div class="MinihomeHyperlinksInfoHeader">
<div class="txtsevenbbl">
<h2>Finding the place you want</h2>
</div>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>

<div class="MinihomeSoftContent">
<h3>Find me a map...</h3>
<br />

<p>You can <a href="/Web2/Maps/FindMapInput.aspx">find a map</a> of a place,
city, attraction, station, stop, address or postcode.</p>

<p>&nbsp;</p><img style="PADDING-LEFT: 10px" alt=
"Image of a map showing a specific location" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceLocationMap1.JPG"
align="right" border="0" />

<p>You can view the local transport stops and stations in the area and local amenities
such as hotels, attractions and public facilities.</p><br clear="right" />

<p>&nbsp;</p>

<h3>Now plan me a journey there...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 70px; PADDING-LEFT: 10px" alt=
"Screenshot of a Transport Direct map and the buttons from which to plan a journey" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceMapPlan1.JPG"
align="right" border="0" />You can then choose to plan a journey to or from the location
shown on the map.</p><br clear="right" />

<p>&nbsp;</p>

<h3>Where''s the nearest transport?</h3>

<p>&nbsp;</p>

<p>Alternatively, you may wish to <a href=
"/Web2/JourneyPlanning/FindAStationInput.aspx">find the nearest stations &amp;
airports</a> to a location.</p><br />
<img alt=
"Screenshot of the output from the Find nearest station/airport feature on Transport Direct"
src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceNearest1.JPG"
align="right" border="0" />

<p>You can view them as a list, in order of their distance from your
location...</p><br clear="right" />

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 10px" alt="Image of map showing numbered locations" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceNearestMap1.JPG"
align="left" border="0" />...or you can view them on a map.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Now plan me a journey there...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-LEFT: 10px" alt=
"Screenshot of the results from the Find nearest station/airport feature on Transport Direct"
src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceStationJourneyResults.JPG"
align="right" border="0" />The closest station/airport is already selected. Once you have selected 
the station/airport required, click Travel from or Travel to. Then repeat for the other journey 
leg and click next.<br />
<br />
You will then be able to ''Find journeys between stations/airports'' from the selection you
have made.
<br />
You may not always get journeys to or from all of your selected stations but the results
returned will be the best journeys available between the chosen origin(s) and
destination(s).</p><br clear="right" />

<p>&nbsp;</p>

<h3>Where''s the nearest car park?</h3>

<p>&nbsp;</p>

<p>Similarly, you may wish to <a href="/Web2/JourneyPlanning/FindCarParkInput.aspx">find
the nearest car parks</a> to a location.</p><br />
<img alt=
"Screenshot of the output from the Find nearest car park feature on Transport Direct"
src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceCarParkResults.JPG"
align="right" border="0" />

<p>You can view them as a list, in order of their distance from your location, the total number 
of spaces, and if they have disabled spaces...</p><br clear="right" />

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 10px" alt="Image of map showing available car parks" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceCarParkMap.JPG"
align="left" border="0" />...or you can view them on a map.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Now plan me a journey there...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-LEFT: 10px" alt=
"Screenshot of the output from the Find nearest car park feature on Transport Direct"
src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceCarParkJourneyResults.JPG"
align="right" border="0" />The closest car park is already selected. Once you have selected
the option required, click Drive from or Drive to.<br />

You will then be able to ''Plan a car route'' from/to the car park selected.</p>
<br clear="right" />

<p>&nbsp;</p>

<h3>What will the roads be like?</h3>

<p>&nbsp;</p>

<p>If you are planning to visit a place, you may want to know what the traffic will be
like when you travel there.</p><br />

<p><img style="PADDING-LEFT: 10px" alt=
"Image of a map showing predicted traffic levels using different colours to represent estimated congestion"
src=
"/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeFindAPlaceTrafficMap1.JPG"
align="right" border="0" />Our <a href="/Web2/Maps/TrafficMaps.aspx">traffic maps</a> are
based on recorded traffic levels over the last few years and display an accurate
prediction of the likely traffic levels at your precise time of travel.</p>
<br clear="right" />

<p>&nbsp;</p>

<h3>I need a tube map...</h3>

<p>&nbsp;</p>

<p>Finally, you may just need a map showing part of the rail network, bus routes or a map
of the London Underground. Our <a href="/Web2/Maps/NetworkMaps.aspx">transport network
maps</a> page links you to a range of sites containing these maps.</p>
</div>
</div>
'
-- WELSH
,
'<div class="MinihomeHyperlinksInfoContent">
<div class="MinihomeHyperlinksInfoHeader">
<div class="txtsevenbbl">
<h2>Dywedwch fwy wrthyf</h2>
</div>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>

<div class="MinihomeSoftContent">
<h3>Darganfyddwch fap i mi...</h3><br />


<p>Gallwch <a href="/Web2/Maps/FindMapInput.aspx">ddarganfyddwch map</a> o le, dinas, atyniad, gorsaf, arhosfan, 
cyfeiriad neu god post.</p>

<p>&nbsp;</p><img style="PADDING-LEFT: 10px" alt=
"Delwedd o fap yn dangos lleoliad penodol" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceLocationMap1.JPG"
align="right" border="0" />

<p>Gallwch weld yr arosfannau a’r gorsafoedd cludiant lleol yn yr ardal a mwynderau lleol fel gwestai, 
atyniadau a chyfleusterau cyhoeddus.</p><br clear="right" />

<p>&nbsp;</p>

<h3>Yn awr cynlluniwch siwrnai i mi yno...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 70px; PADDING-LEFT: 10px" alt=
"Delwedd sgrîn o fap Transport Direct a’r botymau y bwriadwch gynllunio siwrneion ohonynt" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceMapPlan1.JPG"
align="right" border="0" />Yna gallwch ddewis cynllunio siwrnai i neu o’r lleoliad a ddangosir ar y map.</p><br clear="right" />

<p>&nbsp;</p>

<h3>Ble mae’r cludiant agosaf?</h3>

<p>&nbsp;</p>

<p>Neu efallai y dymunwch <a href=
"/Web2/JourneyPlanning/FindAStationInput.aspx">dod o hyd i''r gorsafoedd a''r meysydd awyr agosaf</a> at leoliad.</p><br />
<img alt=
"Delwedd sgrîn o’r allbwn o’r nodwedd Dod ohyd i''r orsaf/maes awyr agosaf ar Transport Direct"
src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceNearest1.JPG"
align="right" border="0" />

<p>Gallwch edrych arnynt fel rhestr, yn nhrefn eu pellter o’ch lleoliad...</p><br clear="right" />

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 10px" alt="Delwedd o fap yn dangos lleoliadau wedi eu rhifo" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceNearestMap1.JPG"
align="left" border="0" />...neu gallwch eu gweld ar fap.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Yn awr cynlluniwch siwrnai i mi yno...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-LEFT: 10px" alt=
"Llun sgrîn o’r allbwn o’r nodwedd Dod o hyd i''r orsaf/maes awyr agosaf ar Transport Direct"
src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceStationJourneyResults.JPG"
align="right" border="0" />Mae’r orsaf/maes awyr agosaf wedi ei dicio yn barod. Wedi i chi ddewis yr 
opsiwn/opsiynau angenrheidiol, cliciwch ar Teithio o neu Teithio i. Yna ailadroddwch hyn ar gyfer yr 
adran arall o’r siwrnai a chliciwch nesaf.<br />

Yna byddwch yn gallu ‘Darganfyddwch siwrneion rhwng gorsafoedd/meysydd awyr’ o’r dewis yr ydych wedi ei wneud.<br />

Efallai na fydd wastad yn bosibl i chi gael siwrneion sy''n gadael neu''n cyrraedd pob gorsaf a ddewiswyd gennych ond 
byddwn yn dychwelyd manylion y siwrneion gorau sydd ar gael rhwng y man(nau) cychwyn a''r cyrchfan(nau) a ddewiswyd.</p><br clear="right" />

<p>&nbsp;</p>

<h3>Ble mae’r maes parcio agosaf?</h3>

<p>&nbsp;</p>

<p>Yn yr un modd, mae’n bosibl y byddwch yn dymuno <a href="/Web2/JourneyPlanning/FindCarParkInput.aspx">darganfod 
y meysydd parcio agosaf</a> i leoliad.</p><br />
<img alt=
"Llun sgrîn o’r allbwn o’r nodwedd Darganfyddwch y maes parcio agosaf ar Transport Direct"
src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceCarParkResults.JPG"
align="right" border="0" />

<p>Gallwch eu gweld fel rhestr, yn nhrefn eu pellter o’ch lleoliad chi...</p><br clear="right" />

<p>&nbsp;</p>

<p><img style="PADDING-RIGHT: 10px" alt="Delwedd o fap yn dangos y meysydd parcio sydd ar gael" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceCarParkMap.JPG"
align="left" border="0" />...neu gallwch eu gweld ar fap.</p><br clear="left" />

<p>&nbsp;</p>

<h3>Yn awr cynlluniwch siwrnai i mi yno...</h3>

<p>&nbsp;</p>

<p><img style="PADDING-LEFT: 10px" alt=
"Llun sgrîn o’r allbwn o’r nodwedd Darganfyddwch y maes parcio agosaf ar Transport Direct"
src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceCarParkJourneyResults.JPG"
align="right" border="0" />Mae’r maes parcio agosaf wedi ei ddewis yn barod. Wedi i chi ddewis yr 
opsiwn angenrheidiol, cliciwch ar Gyrrwch o neu Gyrrwch i<br />

Yna byddwch yn gallu ‘Cynlluniwch lwybr car’ o/i’r maes parcio a ddewiswyd.</p>
<br clear="right" />

<p>&nbsp;</p>

<h3>Sut rai fydd y ffyrdd?</h3>

<p>&nbsp;</p>

<p>Os ydych chi’n cynllunio ymweld â lle, efallai y byddwch yn dymuno gwybod sut fydd y traffig pan 
deithiwch yno.</p><br />

<p><img style="PADDING-LEFT: 10px" alt=
"Delwedd o fap yn dangos y lefelau traffig a ragfynegir gan ddefnyddio lliwiau gwahanol i gynrychioli amcangyfrif o’r tagfeydd"
src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeFindAPlaceTrafficMap1.JPG"
align="right" border="0" />Seilir ein <a href="/Web2/Maps/TrafficMaps.aspx">mapiau traffig</a> ar lefelau traffig a gofnodwyd dros
 yr ychydig flynyddoedd diwethaf ac maent yn dangos rhagfynegiad cywir o’r lefelau traffig tebygol ar yr union adeg y byddwch yn 
teithio.</p>
<br clear="right" />

<p>&nbsp;</p>

<h3>Mae arnaf angen map o’r trenau tanddaearol...</h3>

<p>&nbsp;</p>

<p>Yn olaf, efallai y bydd arnoch angen map yn dangos rhan o rwydwaith y rheilffordd, y llwybrau bysiau neu fap o drenau 
tanddaearol Llundain. Mae ein tudalen  <a href="/Web2/Maps/NetworkMaps.aspx">mapiau’r rhwydwaith cludiant</a> 
yn eich cysylltu ag ystod o safleoedd yn cynnwys y mapiau hyn.</p>
</div>
</div>
'

GO






------------------------------------------------------------------------
-- Live Travel Home page information panel
------------------------------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 6


EXEC AddtblContent
@ThemeId, 16, 'HomeTravelInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/LiveTravel/Home', 
'<div class="MinihomeHyperlinksInfoContent">  <div class="MinihomeHyperlinksInfoHeader">  <div class="txtsevenbbl"><h2>Finding your news</h2></div><!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </div>  <div class="MinihomeSoftContent">  <h3>Are there any incidents that could affect my journey?</h3>  <p>&nbsp;</p><img style="PADDING-LEFT: 30px" alt="Image of a map showing incident symbols" src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeLiveTravelNewsIncidents.JPG" align="right" border="0"/>   <p>You can check for any incidents on the roads or the public transport network that may affect your journey, either on a map or as a list, on our <a href="/Web2/LiveTravel/TravelNews.aspx">Live Travel news</a> page.</p><br/>  <p>&nbsp;</p>  <p><img style="PADDING-RIGHT: 40px" alt="Screenshot of the map with additional hover-over info" src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeLiveTravelNewsIncidentPopup1.JPG" align="left" border="0"/></p>  <p>Hover-over the incident symbols on the map to find further details. <br/><br/>We also show future planned roadworks and public transport engineering works.</p><br/>  <p>&nbsp;</p>  <h3>When is the next train? </h3>  <p>&nbsp;</p>  <p>Find out when the next train or bus leaves, or whether there are any current delays to your service, by visiting our <a href="/Web2/LiveTravel/DepartureBoards.aspx">departure boards</a> page.</p></div></div>',
'<div class="MinihomeHyperlinksInfoContent">  <div class="MinihomeHyperlinksInfoHeader">  <div class="txtsevenbbl"><h2>Dywedwch fwy wrthyf...</h2><!-- New heading should read "Finding your news" replacing "Tell me more" --></div><!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </div>  <div class="MinihomeSoftContent">  <h3>Oes yna unrhyw ddigwyddiadau a allai effeithio ar fy siwrnai?</h3>  <p>&nbsp;</p><img style="PADDING-LEFT: 30px" alt="Delwedd o fap yn dangos symbolau digwyddiadau" src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeLiveTravelNewsIncidents.JPG" align="right" border="0"/>   <p>Gallwch weld a oes unrhyw ddigwyddiadau ar y ffyrdd neu''r rhwydwaith cludiant cyhoeddus a all effeithio ar eich siwrnai, naill ai ar fap neu fel rhestr, ar ein tudalen <a href="/Web2/LiveTravel/TravelNews.aspx">Newyddion teithio byw</a>.</p><br/>  <p>&nbsp;</p>  <p><img style="PADDING-RIGHT: 40px" alt="Delwedd sgrîn o fap gyda gwybodaeth ychwanegol i hofran drosto" src="/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeLiveTravelNewsIncidentPopup2.JPG" align="left" border="0"/></p>  <p>Arhoswch uwchben y symbolau digwyddiadau ar y map i ddarganfod manylion pellach. <br/><br/>Rydym hefyd yn dangos gwaith ffyrdd sy''n cael eu cynllunio yn y dyfodol a gwaith peirianyddol yn ymwneud â chludiant cyhoeddus.</p><br/>  <p>&nbsp;</p>  <h3>Pryd mae''r trên nesaf? </h3>  <p>&nbsp;</p>  <p>Darnganfyddwch pryd mae''r trên neu''r bws nesaf yn gadael, neu a oes unrhyw oedi i''ch gwasanaeth chi ar hyn o bryd drwy ymweld â''n tudalen <a href="/Web2/LiveTravel/DepartureBoards.aspx">byrddau cyrraedd a chychwyn</a>.</p></div></div>'

GO


------------------------------------------------------------------------
-- Tips and tools Home page information panel
------------------------------------------------------------------------


DECLARE @ThemeId INT
SET @ThemeId = 6

EXEC AddtblContent
@ThemeId, 14, 'TipsToolsInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Tools/Home',
'<div class="MinihomeHyperlinksInfoContent">  
<div class="MinihomeHyperlinksInfoHeader">  
<div class="txtsevenbbl"><h2>Helping you get the most from Transport Direct</h2></div>
<!-- Following spaces help   formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice   wrapping when ramping up text size -->
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
</div>  
<div class="MinihomeSoftContent" summary="Tips and tools content">  
<h3>Compare CO2 emissions for your journey... </h3>  
<p>&nbsp;</p>

<p>You can now compare the CO2 emissions of the four main modes of transport (Car, Rail,
Bus/coach, and Plane) for your journey.</p><br />
<img alt="Screen shot of CO2 Emissions page" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeTipsToolsEmissions2.jpg"
align="right" border="0" />
<p>

You can do a simple emissions comparison by inputting a distance in miles or
kilometres. TD will tell you how much CO2 would be emitted if you travelled that distance
by car, train, bus, or plane.</p><br clear="right" />
<p>&nbsp;</p>
<p><img alt="Screen shot of CO2 Emissions for your journey" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeTipsToolsEmissionsJourney2.jpg" 
align="left" border="0"/>Or look at the emissions for your journey by different modes of 
transport by following the links from your journey details page.</p><br clear="left" />

<p>&nbsp;</p>
<p>Go to the <a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">CO2 emissions</a> page to find out.</p>  <p>&nbsp;</p>  
<p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><p>&nbsp;</p><br />
<h3>I use a screen-reader...</h3>  <p>&nbsp;</p>  <p>We aim to provide an equivalent experience for screen-reader users as well as users who do not need this technology. In order for you to best understand how the site works and how to get the most out of the journey planning features, visit the 
<a href="/Web2/About/Accessibility.aspx">Accessibility</a> section. </p><br /></div></div>'


-- WELSH
,
'<div class="MinihomeHyperlinksInfoContent">
<div class="MinihomeHyperlinksInfoHeader">
<div class="txtsevenbbl">
<h2>Dywedwch fwy wrthyf...</h2>
<!-- New heading text should read "Helping you get the most from Transport Direct" replacing "Tell me more" -->
</div>
<!-- Following spaces help formatting in 2 ways: 1. Give outer div real content to give height; 2. Add a padding to ensure nice wrapping when ramping up text size -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
</div>

<div class="MinihomeSoftContent" summary="Tips and tools content">
<h3>Cymharwch allyriadau CO2 ar gyfer eich siwrnai...</h3>

<p>&nbsp;</p>

<p>Nawr fe allwch gymharu allyriadau CO2 y pedwar prif fath o drafnidiaeth (Car,
Tr&ecirc;n, Bws/coets, ac Awyren) ar gyfer eich siwrnai.</p><br />
&nbsp;

<p><img alt="Llun sgrin o dudalen Allyriadau CO2" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeTipsToolsEmissions2.jpg"
align="right" border="0" />Gallwch gymharu allyriadau yn syml drwy nodi pellter mewn
milltiroedd neu gilometrau. Bydd Transport Direct yn dweud wrthych faint o CO2 fyddai''n
cael ei ollwng pe byddech chi''n teithio''r pellter hwnnw mewn car, tr&ecirc;n, bws, neu
awyren.</p><br clear="right" />

<p>&nbsp;</p>

<p><img alt="Llun sgrin o Allyriadau CO2 ar gyfer eich siwrnai" src=
"/Web2/App_Themes/BusinessLink/images/gifs/SoftContent/HomeTipsToolsEmissionsJourney2.jpg"
align="left" border="0" />Neu edrychwch ar yr allyriadau ar gyfer eich siwrnai yn &ocirc;l
gwahanol fathau o drafnidiaeth drwy ddilyn y cysylltau o dudalen manylion eich
siwrnai.</p><br clear="left" />

<p>&nbsp;</p>

<p>Ewch i''r dudalen <a href=
"/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">allyriadau CO2</a> i gael
gwybod.</p>

<p>&nbsp;</p>

<h3>Rwy&rsquo;n defnyddio darllenwr sgr&icirc;n...</h3>

<p>&nbsp;</p>

<p>Anelwn at ddarparu profiad cyfwerth ar gyfer defnyddwyr darllenwyr sgr&icirc;n yn
ogystal &acirc; defnyddwyr nad oes arnynt angen y dechnoleg hon. Er mwyn i chi ddeall
orau sut mae''r safle yn gweithio a sut i gael y gorau o''r nodweddion cynllunio siwrnai,
ymwelwch &acirc;"r adran <a href=
"/TransportDirect/cy/About/Accessibility.aspx">Hygyrchedd</a> yn y tudalennau Amdanom
ni.</p>
</div>
</div>
'

GO


---------------------------------------------------------------
-- Home Page - Right hand info panel
---------------------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 6

-- Second Third items
EXEC AddtblContent
@ThemeId, 15, 'TDAdditionalInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home', '<div class="Column3Header">
<div class="txtsevenbbl">Useful links for Business Users</div>
<div class="clearboth" ></div>
</div>
<div class="Column3Content">
<table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
<tbody>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/TrafficRadioIcon.gif" alt="Traffic Radio News"/>
</td>
<td class="txtseven" valign="top">
<a target="_blank" href="http://www.trafficradio.org.uk" >Traffic Radio News</a>
</td></tr>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/HighwaysAgencyIcon.gif" alt="Regional Traffic RSS Information"/>
</td>
<td class="txtseven" valign="top">
<a target="_blank" href="http://www.highways.gov.uk/traffic/11278.aspx" >Regional Traffic RSS Information</a>
</td></tr>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/FreightBestPractice.gif" alt="Freight Best Practice"/>
</td>
<td class="txtseven" valign="top">
<a target="_blank" href="http://www.freightbestpractice.org.uk" >Freight Best Practice</a>
</td></tr>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/HighwaysAgencyIcon.gif" alt="Truck Stop Guide"/>
</td>
<td class="txtseven" valign="top">
<a target="_blank" href="http://www.highways.gov.uk/knowledge/13659.aspx" >Truck Stop Guide</a>
</td></tr>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/HighwaysAgencyIcon.gif" alt="Abnormal Loads"/>
</td>
<td class="txtseven" valign="top">
<a target="_blank" href="http://www.esdal.com" >Abnormal Loads Notification</a>
</td></tr>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/MetOfficeIcon.gif" alt="Latest Weather"/>
</td>
<td class="txtseven" valign="top">
<a target="_blank" href="http://www.metoffice.gov.uk" >Latest Weather</a>
</td></tr>
</tbody></table></div>
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
<div class="txtsevenbbl">Act on C02</div>
<div class="clearboth" ></div>
</div>
<div class="Column3Content">
<table id="Table2" cellspacing="0" cellpadding="2" width="100%" border="0">
<tbody>
<tr>
<td class="VertAlignTop">
<img src="/Web2/App_Themes/BusinessLink/Images/gifs/JourneyPlanning/en/trspt_c02.gif" alt="Act on CO2"/>
</td>
<td class="txtseven" valign="top">
Compare your CO2 and choose the route with the lowest emissions for your journey. On the journey results page click the 
<a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx" >"Check CO2"</a> button.
<br/><br/>
</td></tr></tbody></table></div>'	

, '<div class="Column3Header"><div class="txtsevenbbl">Useful links for Business Users</div>
<div class="clearboth" ></div>
</div>
<div class="Column3Content">
<table id="Table1" cellspacing="0" cellpadding="2" width="100%" border="0">
<tbody>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/TrafficRadioIcon.gif" alt="Traffic Radio News"/>
</td>
<td class="txtseven" valign="top">
<a href="http://www.trafficradio.org.uk" >Traffic Radio News</a>
</td></tr>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/HighwaysAgencyIcon.gif" alt="Regional Traffic RSS Information"/>
</td>
<td class="txtseven" valign="top">
<a href="http://www.highways.gov.uk/traffic/11278.aspx" >Regional Traffic RSS Information</a>
</td></tr>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/BlankIcon.gif" alt="Freight Best Practice"/>
</td>
<td class="txtseven" valign="top">
<a href="http://www.freightbestpractice.org.uk" >Freight Best Practice</a>
</td></tr>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/HighwaysAgencyIcon.gif" alt="Truck Stop Guide"/>
</td>
<td class="txtseven" valign="top">
<a href="http://www.highways.gov.uk/knowledge/13659.aspx" >Truck Stop Guide</a>
</td></tr>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/HighwaysAgencyIcon.gif" alt="Abnormal Loads"/>
</td>
<td class="txtseven" valign="top">
<a href="http://www.esdal.com" >Abnormal Loads Notification</a>
</td></tr>

<tr>
<td class="VertAlignTop">
<img src="/Web2/app_themes/BusinessLink/images/gifs/partner/MetOfficeIcon.gif" alt="Latest Weather"/>
</td>
<td class="txtseven" valign="top">
<a href="http://www.metoffice.gov.uk" >Latest Weather</a>
</td></tr>
</tbody></table></div>

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
<div class="txtsevenbbl">Act on C02</div>
<div class="clearboth" ></div>
</div>
<div class="Column3Content">
<table id="Table2" cellspacing="0" cellpadding="2" width="100%" border="0">
<tbody>
<tr>
<td class="VertAlignTop">
<img src="/Web2/App_Themes/BusinessLink/Images/gifs/JourneyPlanning/en/trspt_c02.gif" alt="Act on CO2"/>
</td>
<td class="txtseven" valign="top">
Compare your CO2 and choose the route with the lowest emissions for your journey. On the journey results page click the 
<a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx" >"Check CO2"</a> button.
<br/><br/>


</td></tr></tbody></table></div>'	




-- Last item

EXEC AddtblContent
@ThemeId, 15, 'TDNewInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/Home',
'',''

GO


---------------------------------------------------------
-- Find a train input
---------------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 6

EXEC AddtblContent
@ThemeId, 50, 'TDFindTrainPromoHtmlPlaceholderDefinition', '/Channels/TransportDirect/JourneyPlanning/FindTrainInput', 
'<div class="Column3Header"><div class="txtsevenbbl">You can get <A href="http://www.direct.gov.uk/en/Hl1/Help/YourQuestions/DG_069492" >travel information</a> on BusinessLink mobile</div>&nbsp;&nbsp;</div>
<div class="Column3Content">
<table cellSpacing="0" cellPadding="2" width="100%" border="0" class="txtseven"><tr><td>
You can access travel information through the <A href="http://www.direct.gov.uk/en/Hl1/Help/YourQuestions/DG_069492" >BusinessLink mobile service</A>. You can search for next train departures and arrivals, check whether a train is running on time and look up times for tomorrow.
</td></tr></table></div>',

 '<div class="Column3Header"><div class="txtsevenbbl">You can get <A href="http://www.direct.gov.uk/en/Hl1/Help/YourQuestions/DG_069492" >travel information</A> on BusinessLink mobile</div>&nbsp;&nbsp;</div>
<div class="Column3Content">
<table cellSpacing="0" cellPadding="2" width="100%" border="0" class="txtseven"><tr><td>
You can access travel information through the <A href="http://www.direct.gov.uk/en/Hl1/Help/YourQuestions/DG_069492" >BusinessLink mobile service</A>. You can search for next train departures and arrivals, check whether a train is running on time and look up times for tomorrow.
</td></tr></table></div>'


-- Second Third items
EXEC AddtblContent
@ThemeId, 51, 'TDFindTrainPromoHtmlPlaceholderDefinition', '/Channels/TransportDirect/JourneyPlanning/FindTrainCostInput', 
'<div class="Column3Header"><div class="txtsevenbbl">You can get <A href="http://www.direct.gov.uk/en/Hl1/Help/YourQuestions/DG_069492" >travel information</A> on BusinessLink mobile</div>&nbsp;&nbsp;</div>
<div class="Column3Content">
<table cellSpacing="0" cellPadding="2" width="100%" border="0" class="txtseven"><tr><td>
You can access travel information through the <A href="http://www.direct.gov.uk/en/Hl1/Help/YourQuestions/DG_069492" >BusinessLink mobile service</A>. You can search for next train departures and arrivals, check whether a train is running on time and look up times for tomorrow.
</td></tr></table></div>',

 '<div class="Column3Header"><div class="txtsevenbbl">You can get <A href="http://www.direct.gov.uk/en/Hl1/Help/YourQuestions/DG_069492" >travel information</A> on BusinessLink mobile</div>&nbsp;&nbsp;</div>
<div class="Column3Content">
<table cellSpacing="0" cellPadding="2" width="100%" border="0" class="txtseven"><tr><td>
You can access travel information through the <A href="http://www.direct.gov.uk/en/Hl1/Help/YourQuestions/DG_069492" >BusinessLink mobile service</A>. You can search for next train departures and arrivals, check whether a train is running on time and look up times for tomorrow.
</td></tr></table></div>'


----------------------------------------------------------------
-- Sitemap
----------------------------------------------------------------


-- Live travel
EXEC AddtblContent
@ThemeId, 43, 'LiveTravelBody', '/Channels/TransportDirect/SiteMap/SiteMap', 
'
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">Check Journey CO2</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/ContactUs/FeedbackPage.aspx">Provide feedback</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/RelatedSites.aspx">Related sites</a><br/>
<ul id="lister">
<li><a href="/Web2/About/RelatedSites.aspx#NationalTransport">National transport</a><br/></li>
<li><a href="/Web2/About/RelatedSites.aspx#LocalPublicTransport">Local public transport</a> </li>
<li><a href="/Web2/About/RelatedSites.aspx#Motoring">Motoring</a> </li>
<li><a href="/Web2/About/RelatedSites.aspx#MotoringCosts">Motoring Costs</a> </li>
<li><a href="/Web2/About/RelatedSites.aspx#CarSharing">Car Sharing</a> </li>
<li><a href="/Web2/About/RelatedSites.aspx#Government">Government</a> </li></ul></div>
<div class="smcSiteMapLink"><a href="/Web2/Help/NewHelp.aspx">Frequently Asked Questions</a><br/></div>
', 

'
<div class="smcSiteMapLink"><a href="/Web2/JourneyPlanning/JourneyEmissionsCompare.aspx">Check Journey CO2</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/ContactUs/FeedbackPage.aspx">Rhowch adborth</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/RelatedSites.aspx">Safleoedd cysylltiedig</a><br/>
<ul id="lister">
<li><a href="/Web2/About/RelatedSites.aspx#NationalTransport">Cludiant cenedlaethol</a><br/></li>
<li><a href="/Web2/About/RelatedSites.aspx#LocalPublicTransport">Cludiant cyhoeddus lleol</a> </li>
<li><a href="/Web2/About/RelatedSites.aspx#Motoring">Moduro</a> </li>
<li><a href="/Web2/About/RelatedSites.aspx#MotoringCosts">Costau moduro</a> </li>
<li><a href="/Web2/About/RelatedSites.aspx#CarSharing">Rhannu ceir</a> </li>
<li><a href="/Web2/About/RelatedSites.aspx#Government">Llywodraeth</a> </li></ul></div>
<div class="smcSiteMapLink"><a href="/Web2/Help/NewHelp.aspx">Cwestiynau a Ofynnir yn Aml</a><br/></div>
'

-- TDOnTheMove title
EXEC AddtblContent
@ThemeId, 43, 'TDOnTheMoveTitle', '/Channels/TransportDirect/SiteMap/SiteMap',
'<h3>About Transport Direct</h3>',
'<h3>Amdanom Transport Direct</h3>'

-- TDOnTheMove body
EXEC AddtblContent
@ThemeId, 43, 'TDOnTheMoveBody', '/Channels/TransportDirect/SiteMap/SiteMap',
'<div class="smcSiteMapLink"><a href="/Web2/About/AboutUs.aspx">About Transport Direct</a><br/>
<ul class="lister">
<li><a href="/Web2/About/AboutUs.aspx#EnablingIntelligentTravel">Enabling intelligent travel</a><br/></li>
<li><a href="/Web2/About/AboutUs.aspx#WhoOperates">Who operates Transport Direct? </a></li>
<li><a href="/Web2/About/AboutUs.aspx#WhoBuilds">Who builds this site?</a> </li>
</ul></div>
<div class="smcSiteMapLink"><a href="/Web2/About/Accessibility.aspx">Accessibility</a> <br/></div>
<div class="smcSiteMapLink"><a href="/Web2/ContactUs/Details.aspx">Contact details</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/DataProviders.aspx">Data providers</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/PrivacyPolicy.aspx">Privacy policy</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/TermsConditions.aspx">Terms &amp; conditions</a><br/></div>
', 

'<div class="smcSiteMapLink"><a href="/Web2/About/AboutUs.aspx">Amdanom Transport Direct</a><br/>
<ul class="lister">
<li><a href="/Web2/About/AboutUs.aspx#EnablingIntelligentTravel">Galluogi teithio deallus</a> </li>
<li><a href="/Web2/About/AboutUs.aspx#WhoOperates">Pwy sy''n gweithredu Transport Direct? </a></li>
<li><a href="/Web2/About/AboutUs.aspx#WhoBuilds">Pwy sy''n adeiladu a datblygu''r safle hwn?</a> </li>
</ul></div>
<div class="smcSiteMapLink"><a href="/Web2/About/Accessibility.aspx">Hygyrchedd</a> <br/></div>
<div class="smcSiteMapLink"><a href="/Web2/ContactUs/Details.aspx">Manylion cyswllt</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/DataProviders.aspx">Darparwyr data</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/PrivacyPolicy.aspx">Polisi preifatrwydd</a><br/></div>
<div class="smcSiteMapLink"><a href="/Web2/About/TermsConditions.aspx">Amodau a thelerau</a><br/></div>
'


EXEC AddtblContent
@ThemeId, 43, 'sitemapFooterNote', '/Channels/TransportDirect/SiteMap/SiteMap',
' ',
' '

GO

----------------------------------------------------------------
-- Tips and Tools home - Mobile Demonstrator Icon link
----------------------------------------------------------------



----------------------------------------------------------------
-- Ambiguity page
----------------------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 6

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'FindCarParkInput.labelNote.Ambiguous', 'Select an option from each list below. Then click "Next".', 'cy Select an option from each list below. Then click "Next".'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'FindStationInput.labelNote.StationMode.NoValid', 'Select an option from each list below. Then click "Next".', 'cy Select an option from each list below. Then click "Next".'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'ValidateAndRun.SelectFromList', 'Select an option from each list below.', 'cy Select an option from each list below. Then click "Next".'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'VisitPlannerInput.Instructional.Ambiguity', 'Select an option from each list below. Then click "Next".', 'cy Select an option from each list below. Then click "Next".'

GO


----------------------------------------------------------------
-- Amend tab images
----------------------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 6

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendCarDetailsTabSelected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendCarDetailsTabSelected.gif', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendCarDetailsTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendCarDetailsTabUnselected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendCarDetailsTabUnselected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendCarDetailsTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendDayTabSelected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendDateTabSelected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendDateTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendDayTabUnselected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendDateTabUnselected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendDateTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendTabSelected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendTabSelected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendTabUnselected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendTabUnselected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendViewTabSelected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendViewTabSelected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendViewTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonAmendViewTabUnSelected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendViewTabUnSelected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendViewTabUnSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonCostSearchDateTabSelected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendDateTabSelected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendDateTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonCostSearchDateTabUnselected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendDateTabUnselected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendDateTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonFareTabSelected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendFareDetailsTabSelected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendFareDetailsTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonFareTabUnselected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendFareDetailsTabUnselected.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendFareDetailsTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonStopoverTabSelected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendStopoverBlue.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendStopoverBlue.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonStopoverTabUnselected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/AmendStopoverGrey.gif','/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/AmendStopoverGrey.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSaveTabSelected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/SaveTabSelected.gif', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/SaveTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSaveTabUnselected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/SaveTabUnselected.gif', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/SaveTabUnselected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSendTabSelected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/SendTabSelected.gif', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/SendTabSelected.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSend.ImageButtonSendTabUnselected', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/SendTabUnselected.gif', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/cy/SendTabUnselected.gif'

----------------------------------------------------------------
-- Back to top arrow
----------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'DepartureBoards.TopOfPage.HyperlinkImage', '/Web2/App_Themes/BusinessLink/images/gifs/Partner/back_to_top_icon.jpg', '/Web2/App_Themes/BusinessLink/images/gifs/JourneyPlanning/en/uarrow_icon_slim.gif'

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'DepartureBoards.TopOfPage.Text', '', ''


GO


----------------------------------------------------------------
-- Powered by Transport Direct content
----------------------------------------------------------------

DECLARE @ThemeId INT
SET @ThemeId = 6

EXEC AddtblContent
@ThemeId, 1, 'langstrings', 'PoweredByControl.HTMLContent',
'<div class="Column3PoweredBy">
<div class="Column3Header Column3HeaderPoweredBy">
<div class="txtsevenbbl">
Provided by
<!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
</div>
</div>
<div class="Column3Content Column3ContentPoweredBy">
<table>
<tr>
<td>
<a href="/Web2/About/AboutUs.aspx">
<img src="/Web2/App_Themes/BusinessLink/images/gifs/Partner/td_logo_4whbg.gif" alt="Provided by Transport Direct" />
</a>
</td>
</tr>
</table>
</div>
</div>',

'<div class="Column3PoweredBy">
<div class="Column3Header Column3HeaderPoweredBy">
<div class="txtsevenbbl">
Provided by
<!-- Don''t remove spaces -->&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
</div>
</div>
<div class="Column3Content Column3ContentPoweredBy">
<table>
<tr>
<td>
<a href="/Web2/About/AboutUs.aspx">
<img src="/Web2/App_Themes/BusinessLink/images/gifs/Partner/td_logo_4whbg.gif" alt="Provided by Transport Direct" />
</a>
</td>
</tr>
</table>
</div>
</div>'

EXEC AddtblContent
@ThemeId, 1, 'langstrings', 'PoweredByControl.HTMLContent.LogoOnly',
'<div class="PoweredByLogo">
<a href="/Web2/About/AboutUs.aspx">
<img src="/Web2/App_Themes/BusinessLink/images/gifs/ProvidedBy1a.gif" alt="Provided by Transport Direct" />
</a>
</div>',

'<div class="PoweredByLogo">
<a href="/Web2/About/AboutUs.aspx">
<img src="/Web2/App_Themes/BusinessLink/images/gifs/ProvidedBy1a.gif" alt="Provided by Transport Direct" />
</a>
</div>'

----------------------------------------------------------------
-- Wait page
----------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 32, 'MessageDefinition', '/Channels/TransportDirect/JourneyPlanning/WaitPage',
'We are always seeking to improve our service. If you cannot find what you want, please tell Transport Direct by clicking <b>Contact Transport Direct</b>',
'cy We are always seeking to improve our service. If you cannot find what you want, please tell Transport Direct by clicking <b>Contact Transport Direct</b>'


EXEC dbo.[AddtblContent]
@ThemeId, 1, 'langStrings', 'WaitPage.TitleTipOfTheDay',
'<b>Tip of the Day</b>',
'cy <b>Tip of the Day</b>'


----------------------------------------------------------------
-- Send to friend text 
----------------------------------------------------------------

EXEC AddtblContent
@ThemeId, 1, 'langStrings', 'AmendSaveSendLoginControl.labelLoginRegisterNote',
'This feature is currently not used within this service.',
'cy This feature is currently not used within this service.'


GO




----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10504
SET @ScriptDesc = 'Content added for theme BusinessLink'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.27  $'

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
