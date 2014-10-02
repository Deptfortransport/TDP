-- *************************************************************************************
-- NAME 		: SC10001_TransportDirect_Content_1_MiniHome_PlanAJourney.sql
-- DESCRIPTION  : Updates to Plan a journey homepage information panel
-- AUTHOR		: xxxx
-- DATE			: 16 Apr 2008
-- *************************************************************************************

USE [Content]
GO

DECLARE @GroupId int,
	@ThemeId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'journeyplanning_home')
SET @ThemeId = 1

-- Add the html text
EXEC AddtblContent
@ThemeId, @GroupId, 'PlanAJourneyInformationHtmlPlaceholderDefinition', '/Channels/TransportDirect/JourneyPlanning/Home'
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
    <br/>
    <h3>Plan me a journey door to door...</h3><img alt="Image showing the quick planner icons on Transport Direct"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomeJourneyPlannerBlueBackgroundSmall.gif"
    border="0" align="middle" />
    <br/>
    <p/>
    <p> 
       <img style="PADDING-RIGHT: 10px" alt="An image showing a summary of journey results"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/JourneyMiniHomePageDetail.jpg"
    align="left" border="0" /> The simplest way to plan your journey is to use the door-to-door journey planner above. This searches for up to five journey options - by joined-up public transport or by car. 
       Just enter your origin and destination and the results will show step-by-step directions, 
       including any connections you need to make, station details, 
       interchange times or driving instructions if you have selected the car journey.
    </p>
    <br/>

    <p>
    <br clear="left" />
    <img style="PADDING-RIGHT: 10px"
    alt="An image showing a summary of journey results"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/JourneyMiniHomePageMap.jpg"
    align="left" border="0" />
    By clicking the ''Maps'' or ''show map'' button you can see a map of your entire journey, or you can look at the individual sections by clicking the map button next to that leg.</p>
    <br clear="left" />
    <br/>
    
    <p>&#160;</p>
    <p>By clicking "Tickets/Costs", you can check the prices and 
    availability of rail and coach tickets and buy your tickets from partner retailer sites.</p>
    <br/>
    <br/>
    
    <p>&#160;</p>
    <h3>I know how I want to travel...</h3>
    <p>&#160;</p>
    <p>Perhaps you have already decided what form of transport to
    use for the main part of your journey. 
    
    <br/>In that case use our single mode planners to  
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
    <br/>
    <br/>
    <img alt="Image showing the quick planner icons on Transport Direct"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyQuickPlanners5.JPG"
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
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/findCycleDetails.gif"
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
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyBookmark.JPG"
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
    
     
     <img alt="Screenshot showing the social network sites"
     src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/SocialNetworks.jpg"
     align="left" border="0" /><p>Simply plan the journey, click the link in the left hand menu and once 
     you log in to the relevant social network site a link will be posted there 
     allowing your friends to see the journey you''ve planned. So if you''re 
     planning a party and you want people to know how to get there or your''e 
     planning to visit friends or family and want to share your travel plans you 
     now have more ways than ever to do it. Don''t forget if you are a registered 
     user you still have the option to email your plans directly to your friends too.</p> 
     
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
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/JourneyMiniHomePageDetail.jpg"
    align="left" border="0" /> Y ffordd symlaf o gynllunio eich siwrnai yw defnyddio’r cynllunydd siwrnai drws-i-ddrws aml-foddol uchod.  Mae hwn yn chwilio am hyd at bump o opsiynau teithio – drwy ddefnyddio dulliau cludiant cyhoeddus cydgysylltiedig neu gar. Mewnbynnwch eich man cychwyn a’ch man gorffen a bydd y canlyniadau yn dangos cyfarwyddiadau cam-wrth-gam, yn cynnwys unrhyw newidiadau fydd angen ichi eu gwneud, manylion am orsafoedd, amseroedd cyfnewid neu gyfarwyddiadau i yrwyr os ydych wedi dewis y siwrnai mewn car.
    </p>
    <br/>

    <p>  <br clear="left" />
    <img style="PADDING-RIGHT: 10px"
    alt="An image showing a summary of journey results"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/JourneyMiniHomePageMap.jpg"
    align="left" border="0" />Drwy glicio ar y botwm ‘Mapiau’ neu ‘Dangos map’ gallwch weld map o’ch siwrnai gyfan, neu gallwch edrych ar ddarnau unigol ohoni drwy glicio ar fotwm y map ger y darn hwnnw.</p>
    <br clear="left" />
    <br/>
    
    <p>&#160;</p>
    <p>Drwy glicio ar ""Tocynnau/Costau"", gallwch ddarganfod prisiau ac argaeledd tocynnau trên a bysiau moethus a phrynu eich tocynnau o wefannau partneriaid manwerthu.</p>
    <br/>
    <br/>
    
    <p>&#160;</p>
    <h3>I know how I want to travel...</h3>
    <p>&#160;</p>
    <p>Efallai eich bod eisoes wedi penderfynu pa ddull o gludiant i’w ddefnyddio ar gyfer prif ran eich taith. 
    
    <br/>Os felly defnyddiwch ein cynllunwyr un modd  
    <a href="/Web2/JourneyPlanning/FindTrainInput.aspx">Canfyddwch drên</a>, 
    <a href="/Web2/JourneyPlanning/FindFlightInput.aspx">Canfyddwch ehediad</a>, 
    <a href="/Web2/JourneyPlanning/FindCarInput.aspx">Canfyddwch lwybr car</a>, 
    <a href="/Web2/JourneyPlanning/FindCoachInput.aspx">Canfyddwch fws moethus</a>&#160;or 
    <a href="/Web2/JourneyPlanning/FindBusInput.aspx">neu Canfyddwch fws.</a>&#160;Bydd y rhain yn rhestru siwrneiau ar gyfer y math hwnnw o gludiant yn unig</p>
    <br/>
    <br/>
    <img alt="Image showing the quick planner icons on Transport Direct"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyQuickPlanners5.JPG"
    border="0" />
    <p>&#160;</p>
    <p>Drwy ddefnyddio’r cynllunydd ‘Dod o hyd i docynnau trên rhatach’ neu ‘Canfyddwch drên’ gallwch chwilio am docyn trên, yn ôl amser neu yn ôl pris. Mae chwilio yn ôl pris yn caniatáu ichi ddewis o amrediad o brisiau cyn ichi weld manylion y daith sy’n berthnasol i’r pris tocyn hwnnw.</p>
    <p>&#160;</p>
    <p>Gallwch ddefnyddio’r cynllunydd siwrnai <a href="/Web2/JourneyPlanning/FindTrunkInput.aspx">dinas i ddinas</a> i gymharu siwrneiau trên, awyren, bws moethus a char rhwng dwy ddinas neu rhwng dwy dref ym Mhrydain.</p>
    <p>&#160;</p>
    <p>Os ydych wedi penderfynu teithio mewn car, gallwch ddarganfod y maes parcio agosaf at eich man cychwyn neu eich man gorffen drwy glicio ar ‘Gyrru i faes parcio’ a dilyn y cyfarwyddiadau ar y sgrîn.</p>
    <p>&#160;</p>
    <h3>Plan me a cycle route&#8230;</h3>
    <p>&#160;</p>
    <p>Mae cynllunydd ‘Canfod Llwybr Beicio’ Transport Direct wedi ei fwriadu i gynllunio siwrneiau beicio lleol mewn ardaloedd trefol. Gallwch ddewis y daith gyflymaf, y daith dawelaf neu daith hamdden a fydd yn cynnwys cymaint o lastiroedd ag sydd bosibl. Bydd manylion y daith yn cael eu harddangos ynghyd â graff a fydd yn dangos graddiant y daith.</p>
    <p>&#160;</p>
    <br class="clearboth" />
    <img style="PADDING-RIGHT: 10px"
    alt="An image showing a Cycle Planner details page"
    title="An image showing a Cycle Planner details page"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/findCycleDetails.gif"
    align="left" border="0" />
    <br clear="left" />
    <p>Mae’r cynllunydd beicio yn cwmpasu sawl ardal o Brydain Fawr ac rydym yn ychwanegu mwy o ardaloedd yn gyson. Ewch i’n hadran <a href="/Web2/Help/HelpCycle.aspx#A16.2">COA</a>i weld a yw’r cynllunydd beicio yn cynnwys eich ardal chi.</p>
    <p>&#160;</p>
    <br class="clearboth" />
    <h3>Amending my chosen journey</h3>
    <p>&#160;</p>
    <p>Unwaith y byddwch wedi darganfod eich dewis siwrnai, gallwch addasu eich taith:.</p>
    <table class="txtseven" cellspacing="0" cellpadding="2"
    border="0">
      <tbody>
        <tr>
          <td>
            <img alt="Image representing the action of adding to the overall journey"
            src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyExtend.JPG"
            border="0" />
          </td>
          <td>Ychwanegwch siwrnai gysylltiol – darganfyddwch brif ran eich taith ac yna estynnwch y cynllun i ddangos sut i deithio o’r orsaf at eich stepen drws drwy ddefnyddio car neu gludiant cyhoeddus.</td>
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
          <td>Addaswch yr amseroedd o fewn eich siwrnai – caniatewch fwy o amser mewn mannau lle mae’n rhaid ichi wneud newidiadau ac ailgynlluniwch eich siwrnai yn unol â hynny</td>
        </tr>
	<tr>
          <td>
	    <img alt="Image of the &#8216;Amend date and time&#8217; feature"
            src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyAmendJourney.gif"
            border="0" />
          </td>
          <td>Newidiwch eich man cychwyn a/neu eich man gorffen a/neu ddyddiad ac amser eich taith drwy glicio ar yr eicon hwn neu ar y botwm ""Diwygio"" ar frig y dudalen.  Gallech hefyd ddefnyddio’r nodwedd ""Newid dyddiad ac amser"" ar waelod y dudalen</td>
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
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/HomePlanAJourneyBookmark.JPG"
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
    <p>Ar ôl cynllunio eich siwrnai mae nifer o ddolennau yn y ddewislen ar y chwith sy’n caniatáu i chi rannu eich siwrnai â ffrindiau gan ddefnyddio rhwydweithiau cymdeithasol gwahanol.</p>
     <p>&#160;</p>
     <br clear="left" />
    
     <img alt="Screenshot showing the social network sites"
    src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/SocialNetworks.jpg"
    align="left" border="0" />
    <p>Y cwbl sydd angen ichi ei wneud yw cynllunio’r daith, clicio ar y ddolen yn y ddewislen ar y chwith, ac unwaith y byddwch yn mewngofnodi i’r safle rhwydweithio cymdeithasol perthnasol bydd dolen yn cael ei phostio yno gan ganiatáu i’ch ffrindiau weld y daith ydych chi wedi ei chynllunio.  Felly, os ydych yn cynllunio parti ac eisiau i bobl wybod sut i gyrraedd yno, neu os ydych yn bwriadu ymweld â ffrindiau neu deulu ac eisiau rhannu eich cynlluniau teithio mae gennych fwy o ffyrdd nag erioed o’r blaen o wneud hynny.  Peidiwch ag anghofio, os ydych yn ddefnyddiwr cofrestredig mae gennych hefyd yr opsiwn i e-bostio eich cynlluniau yn syth i’ch ffrindiau.</p>
  </div>
</div>
'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 9998
SET @ScriptDesc = 'Updates to Plan a journey homepage information panel'

DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.14  $'

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

