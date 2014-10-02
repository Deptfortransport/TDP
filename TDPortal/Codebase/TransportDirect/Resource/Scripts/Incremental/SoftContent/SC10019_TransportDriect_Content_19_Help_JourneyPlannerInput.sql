-- ***********************************************
-- NAME           : SC10019_TransportDriect_Content_19_Help_JourneyPlannerInput.sql
-- DESCRIPTION    : Updates to Help text for Journey Planner Input page
-- AUTHOR         : Mitesh Modi
-- DATE           : 09 Jan 2012
-- ***********************************************

USE [Content] 
GO

DECLARE 
	@Group varchar(30),
	@GroupId int,
	@ThemeId int

SET @ThemeId = 1

-- Delete existing content (seems to be duplicates so tidy up so only one entry for page and printerfriendly)
SET @Group = 'helpfulljp'

EXEC DeleteContent 
@ThemeId, @Group,
'HelpBodyText',
'/Channels/TransportDirect/Help/HelpJourneyPlannerInput'

EXEC DeleteContent 
@ThemeId, @Group,
'HelpBodyText',
'/Channels/TransportDirect/Printer/HelpJourneyPlannerInput'

SET @Group = 'helpfulljprinter'

EXEC DeleteContent 
@ThemeId, @Group,
'HelpBodyText',
'/Channels/TransportDirect/Help/HelpJourneyPlannerInput'

EXEC DeleteContent 
@ThemeId, @Group,
'HelpBodyText',
'/Channels/TransportDirect/Printer/HelpJourneyPlannerInput'


-- Help Journey Planner Input - Page url configuration
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'ContentDatabase')

EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Help_HelpJourneyPlannerInput', 'Channel'
,'/Channels/TransportDirect/Help/HelpJourneyPlannerInput'
,'/Channels/TransportDirect/Help/HelpJourneyPlannerInput'

EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Help_HelpJourneyPlannerInput', 'Page'
,'/Web2/helpfulljp.aspx'
,'/Web2/helpfulljp.aspx'

EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Help_HelpJourneyPlannerInput', 'QueryString'
,'helpfulljp'
,'helpfulljp'


EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Printer_HelpJourneyPlannerInput', 'Channel'
,'/Channels/TransportDirect/Printer/HelpJourneyPlannerInput'
,'/Channels/TransportDirect/Printer/HelpJourneyPlannerInput'

EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Printer_HelpJourneyPlannerInput', 'Page'
,'/Web2/helpfulljp.aspx'
,'/Web2/helpfulljp.aspx'

EXEC AddtblContent
@ThemeId, @GroupId, '_Web2_Printer_HelpJourneyPlannerInput', 'QueryString'
,'helpfulljp'
,'helpfulljp'

-- Help Journey Planner Input - Help content
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'helpfulljp')

EXEC AddtblContent
@ThemeId, @GroupId, 'HelpBodyText', '/Channels/TransportDirect/Help/HelpJourneyPlannerInput'
,
'<h3>Selecting locations to travel from and to</h3>
<div class="helpTextPad1">
  <h5>1. Type the location names in the boxes</h5>
  <br />
  <p>Transport direct uses javascript to help predict your likely intended 
  origin and destination based on what you type (note, if you have 
  javascript disabled then this feature will not work).  You can enter 
  either a location e.g. a town, district or village name, or the name 
  of a station or airport and Transport Direct will generate a list of 
  possible options as you type.  Select the appropriate options from the 
  lists and move on to the next step.  Transport Direct will also recognise 
  postcodes.  If you are searching for a specific address, then entering 
  the house number followed by a space and then the postcode, is often enough to find it.</p>
  <p>If you don’t find what you are looking for in the list, then you can click 
  the ''more options'' button to the right hand side of the input box.  
  Selecting this option removes the prediction but does allow you access to 
  more possible origins and destinations such as facilities / attractions and 
  other points of interest.  See point 2 below for more information about this.</p>
  <p>When using the ''more options'' function it is best to type in the full 
  location name so that you get the fewest ''similar matches'' returned to you. 
  Punctuation and use of capital letters are not important.</p>
  <p>If you are not sure how to spell the name of the location, you
  can tick the ''Unsure of spelling'' box so that the journey planner
  will also search for locations that sound similar to the one you
  type in.</p>
  <p>If you do not know the full location name, type in as much as
  you know and put an asterisk * after the letters. 
  <br /></p>
  <div class="helpTextPad3">
    <p>e.g. If you selected ''Station/airport'' and typed in ''Kin*''
    you would get all the stations and airports in Britain that
    start with the letters Kin - ''Kinsbrace'', ''Kingham'',
    ''Kings Cross Thameslink'', ''Kings Cross'' etc</p>
  </div>
  <br />
  <h5>2. Select the type of locations</h5>
  <br />
  <p>This will inform the journey planner whether you are looking
  for an address, a postcode, a station or an attraction
   etc.</p>
  <p>It is important that you select the appropriate type of
  location. For example, if you are looking for a railway
  station, but you select the ''Address/postcode'' category, the
  journey planner will not be able to find it.</p>
  <p>The categories are described below:</p>
  <ul>
    <li>
      <div class="helpTextPad2">
      <strong>''Address/postcode'':</strong>If you select this, you
      can type in part or all of an address and/or a postcode, e.g.
      ''3 Burleigh Road'', ''3 Burleigh Road, Stretford'', ''Burleigh
      Road, Stretford, Manchester'', ''3 Burleigh Road, M32 0PF'',
      ''M32 0PF''. If you don''t know the postcode include as
      much of the address as possible.</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Town/district/village'':</strong>If you select this,
      you can type in the name of a city, town, borough, district,
      area, suburb, village or hamlet, e.g. ''Manchester'',
      ''Maidenhead'', ''Anfield'', ''Hockley'', ''Chelsea''</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Facility/attraction'':</strong>If you select this,
      you can type in the name of an attraction or facility,
      including: hotels, schools, universities, hospitals,
      surgeries, sports grounds, theatres, cinemas, tourist
      attractions, museums, government buildings and police
      stations, e.g. ''Edinburgh Castle'', ''Queen''s Head Hotel'',
      ''British Museum'', ''Arsenal Football Club''</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Station/airport'':</strong> If you select this,
      you can type in the name of a railway station, an underground
      station, a light rail station, a coach station, an airport or
      a ferry terminal. You may also type in the name of a
      town and choose to travel from any of the stations in this
      town,e.g. ''London'', Derby'', ''Newcastle'', ''Gatwick''.</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''All stops'':</strong>If you select this, you can type
      in the name of a bus stop, an underground stop, a metro stop,
      a tram stop, or a railway station, e.g. ''Trafalgar Square'',
      ''Whitley Bay'', ''Piccadilly Circus''.</div>
    </li>
  </ul>
  <br />
</div>
<div class="helpTextPad1">
  <h5>3. Find the location on a map (optional)</h5>
  <br />
  <p>You may click on the ''Find on map'' button to find the location
  on a map.</p>
  <p>Once you have found the location on the map, you will have the
  option to continue planning the journey (by clicking ''Next'' on
  that page). This will return you to the current page.</p>
</div>
<h3>Selecting outward and return journey dates and times</h3>
<div class="helpTextPad1">
  <h4>Selecting outward and return journey dates</h4>
  <h5>1. Select the dates you would like to leave/return on</h5>
  <br />
  <p>Select a day from the ''drop-down'' list, then select a month/year</p>
  <p>or</p>
  <p>Click the calendar and select a date from it</p>
  <p>If you are planning a journey that is only one way, choose
    ''No return'' in the month/year ''drop-down'' list.  If
    you would like to plan a return journey but are not sure when
    you are returning, select ''Open return'' in the month/year
    ''drop-down'' list</p>
  <br />
  <h5>2. Choose how you want to specify the times</h5>
  <br />
  <p>You have the choice of selecting the time you want to leave
    the location or the time you want to arrive at the
    destination</p>
  <p>Choose ''Leaving at'' to select the earliest time you want to
    leave the location</p>
  <p>or</p>
    <p>Choose ''Arriving by'' to select the latest time you want to
    arrive at the destination</p>
  <br />
  <h5>3. Select the times you would like to travel</h5>
  <br />
  <p>Select the time you want to either leave at or arrive by</p>
  <p>Select the hours from the ''drop-down'' list</p>
  <p>Select the minutes from the ''drop-down'' list (the journey
    planner uses a 24 hour clock e.g. 5:00pm = 17:00)</p>
</div>
<h3>Advanced options</h3>
<div class="helpTextPad1">
  <h4>Selecting accessible journey options</h4>
  <p>Choose the type of accessible journey you are interested in being shown. 
  Transport Direct currently offers three options to choose from:</p>
  <ul>
    <li><div class="helpTextPad2">''I need a step free journey''</div></li>
    <li><div class="helpTextPad2">''I need a step free journey with staff assistance''</div></li>
	<li><div class="helpTextPad2">''I need a journey with staff assistance''</div></li>
  </ul>
  <br />
  <p>You can find more details about what each of these options means by either 
  hovering over the info button for the option or by following the link to the relevant FAQ section.</p>
  <p>If you pick an accessibility option you can also tick to request a journey with 
  the fewest changes the journey planner can find for your chosen journey.</p>

  <h4>Selecting types of transport</h4>
  <p><strong>Choose the types of transport you would like to use</strong>
  </p>
  <p>Untick the types of transport that you are not interested in
    using. However, at least one type must remain ticked.</p>
  <p>The journey planner will search for journeys that involve
    the 
    <strong>ticked</strong> types of public transport. 
    However, it may not necessarily find journeys that use all the
    types that are selected. This is because the journey
    planner uses 
    <strong>all</strong> the criteria you enter and searches for the
    most suitable journeys only.</p>
</div>
<h3>Public Transport journey details</h3>
<div class="helpTextPad1">
	<p>
	  <strong>Changes (refers to changing from one vehicle to
	  another)</strong>
	</p>
</div>
<div class="helpTextPad1">
  <p>
    <strong>Find (journeys with unlimited, few or no
    changes)</strong>
  </p>
  <p>Choose how much you want the journey planner to limit its
  search to journeys that involve a few changes or no
  changes. Select:
  <br /></p>
  <ul>
    <li>''All journeys (I don''t mind changing)'' to find journeys
    that best fit your required travel times, regardless of the
    number of changes required</li>
    <li>''Journeys with a limited number of changes'' to find only
    those journeys requiring a small number of changes</li>
    <li>''Journeys with no changes'' to find journeys with no
    changes. This could limit the number of journey options
    found</li>
  </ul>
  <br />
  <p>
    <strong>Speed (of changes)</strong>
  </p>
  <p>Choose how quickly you think you can make these changes. 
  Select:</p>
  <ul>
    <li>''Fast'' if you can make the changes quickly</li>
    <li>''Average'' if you can make the changes at an average
    pace</li>
    <li>''Slow'' if you think you will be making the changes at a
    slower than average pace </li>
  </ul>
  <p>For example, you should choose ''Slow'' if you are unfamiliar
  with the stations, require assistance or are travelling with
  luggage.</p>
</div>
<div class="helpTextPad1">
  <h4>Walking</h4>
  <p>Some journeys will require you to walk in order to get from a
  location to a stop (or vice versa), or from one stop to another
  stop. </p>
  <br />
  <p>
    <strong>Speed (of walking)</strong>
  </p>
  <p>Choose your walking speed. Select:</p>
  <ul>
    <li>''Fast'' if you tend to walk quickly</li>
    <li>''Average'' if you walk at an average pace</li>
    <li>''Slow'' if you think you will be walking at a slower
    than average pace</li>
  </ul>
  <br />
  <p>For example you should choose ''Slow'' if you are travelling
  with heavy luggage or if you have a mobility problem.</p>
  <br />
  <p>
    <strong>Time (max.)</strong>
  </p>
  <p>Choose the maximum length of time you are prepared to spend
  walking between points in your journey from the ''drop-down''
  list:</p>
  <ul>
    <li>''5'' minutes </li>
    <li>''10'' minutes</li>
    <li>''15'' minutes</li>
    <li>''20'' minutes </li>
    <li>''25'' minutes</li>
    <li>''30'' minutes</li>
  </ul>
  <p>Please note that setting a low maximum walking speed may limit
  the number of journeys that can be found. If possible, it
  is recommended you choose 30 minutes.</p>
</div>
<div class="helpTextPad1">
	<h4>Journey options</h4>
	<p><strong>Travelling via a location</strong>
	</p>
  <p>Choose where to travel via by typing in the location and
  selecting a location type. For more details on how to do
  this, see ''Selecting locations to travel from and to'' at the
  beginning of the Help page. You may also use a map to find
  the location.</p>
</div>
<h3>Car journey details</h3>
<div class="helpTextPad1">
    <p><strong>Find (quickest, shortest, most fuel economic or
      cheapest overall) journeys</strong>
	</p>
  <p>Choose how the journey planner will select the best driving
  route. Select:</p>
	<ul>
  <li>
    <div class="helpTextPad3">
    <strong>''Quickest''</strong>if you would like a route with the
    shortest driving time</div>
  </li>
  <li>
    <div class="helpTextPad3">
    <strong>''Shortest''</strong>if you would like a route with the
    shortest distance</div>
  </li>
  <li>
    <div class="helpTextPad3">
    <strong>''Most fuel economic''</strong>if you would like a route
    with the lowest fuel consumption</div>
  </li>
  <li>
    <div class="helpTextPad3">
    <strong>''Cheapest overall''</strong>if you would like the
    cheapest route which takes all aspects of the journey into
    account.</div>
  </li>
	</ul>
</div>
<div class="helpTextPad1">
    <p><strong>Speed (maximum driving speed)</strong>
    </p>
</div>
<div class="helpTextPad1">
  <p>Choose the maximum speed you are willing to drive from the
  ''drop-down'' list. Journey times will be based on this
  speed, but will also take into account the legal speed limits on
  the various roads in the proposed route. You can select
  from:</p>
  <ul>
    <li>
      <div>''Max. legal speed on all roads''
      <br /></div>
    </li>
    <li>
      <div>''60 mph'' 
      <br /></div>
    </li>
    <li>
      <div>''50 mph''  
      <br /></div>
    </li>
    <li>
      <div>''40 mph''</div>
    </li>
  </ul>
</div>
<div class="helpTextPad1">
  <p><strong>Do not use motorways</strong>
    </p>
  <p>Tick the box if you do 
  <strong>not</strong> want to drive on motorways.</p>
</div>
<div class="helpTextPad1">
	<h4>Car details</h4>
	<p>Choose the size of your car and whether it is a petrol or diesel
	engined car.</p>
	<p>For fuel consumption you can either select ''Average for my type
	of car'' or specify the amount of fuel your car consumes by entering
	the number of miles per gallon or litres per 100 kilometres.</p>
	<p>For fuel cost you can either select the current average cost of
	fuel or specify the cost you pay per litre.</p>
</div>
<div class="helpTextPad1">
	<h4>Journey options </h4>
	<p><strong>Avoid tolls, ferries, motorways</strong>
    </p>
	<p>If you would prefer your journey to avoid tolls/ ferries/
  motorways, tick the boxes and, where possible, the journey
  planner will avoid them so that they are not included in your
  journey plan.</p>
</div>
<div class="helpTextPad1">
    <p><strong>Avoid roads with restricted access</strong></p>
	<p>The Journey Planner will always attempt to take account of
  roads with restricted access (for example, where a road is
  pedestrian-only during shopping hours) on the days and times on
  which the restriction applies. Sometimes information about
  exactly when these restrictions apply is not available &#8211;
  tick this box if you would like the Journey Planner to avoid
  roads for which this is the case.</p>
</div>
<div class="helpTextPad1">
    <p><strong>Avoid roads</strong></p>
    <p>Type in the numbers of up to 6 roads you would like the
  journey planner to avoid when planning your journey.  
  Type in 1 road per box. e.g. ''M1'', ''A23'', ''B4132''.</p>
</div>
<div class="helpTextPad1">
    <p><strong>To use roads</strong></p>
    <p>Type in the numbers of up to 6 roads you would like the
  journey planner to use when planning your journey. Type in 1 road
  per box. e.g. ''M1'', ''A23'', ''B4132''.</p>
</div>
<div class="helpTextPad1">
    <p><strong>Travelling via a location by car</strong></p>
	<p>Choose where to travel via by typing in the location and
  selecting a location type. For more details on how to do
  this, see ''Selecting locations to travel from and to'' at the
  beginning of the Help page. You may also use a map to find
  the location.</p>
</div>
<h3>Once you have completed the page, click ''Next''.</h3>
<div class="helpTextPad1">
  <p>At this point the journey planner will search for the
  locations you have typed in. If there is more than one location
  similar to the location you typed in, you will need to choose
  from a list of possible matches.</p>
</div>'


, -- WELSH
'<h3>Dewis lleoliadau i deithio ohonynt ac iddynt</h3>
<div class="helpTextPad1">
  <h5>1. Teipiwch enwau''r lleoliad yn y blychau</h5>
  <br />
  <p>Mae''n well teipio enw''r lleoliad yn llawn fel y bydd y nifer
  lleiaf o ''gyfatebiaethau tebyg'' yn cael eu hanfon yn &#244;l
  atoch. Nid yw atalnodi a defnyddio prif lythrennau yn bwysig.</p>
  <p>Os nad ydych yn sicr sut i sillafu enw''r lleoliad, gallwch
  dicio''r blwch ''Ansicr o sillafiad'' fel y bydd y Cynlluniwr
  Siwrnai hefyd yn chwilio am leoliadau sy''n swnio''n debyg i''r un
  yr ydych wedi ei deipio.</p>
  <p>Os nad ydych yn gwybod enw llawn y lleoliad, teipiwch gymaint
  ag a wyddoch a rhowch * ar &#244;l y llythrennau. 
  <br /></p>
  <div class="helpTextPad3">
    <p>e.e. Os bu i chi ddewis ''Gorsaf/maes awyr'' a theipio ''Kin*''
    byddech yn cael yr holl orsafoedd a''r meysydd awyr ym Mhrydain
    sy''n dechrau gyda''r llythrennau Kin - ''Kinsbrace'', ''Kingham'',
    ''King''s Cross Thameslink'', ''King''s Cross'' ac ati.</p>
  </div> 
  <br />
  <h5>2. Dewiswch y math o leoliadau</h5>
  <br />
  <p>Bydd hyn yn hysbysu''r Cynlluniwr Siwrnai a ydych yn chwilio am
  gyfeiriad, c&#244;d post, gorsaf neu atyniad... ac ati.</p>
  <p>Mae''n bwysig eich bod yn dewis y math priodol o leoliad. Er
  enghraifft, os ydych yn chwilio am orsaf rheilffordd, ond yn
  dewis y categori ''Cyfeiriad/c&#244;d post'', ni fydd y Cynlluniwr
  Siwrnai yn gallu dod o hyd iddo.</p>
  <p>Disgrifir y categor&#180;au isod:</p>
  <ul>
    <li>
      <div class="helpTextPad2">
      <strong>''Cyfeiriad/cod post'':</strong>Os dewiswch hwn,
      gallwch deipio rhan neu''r cyfan o gyfeiriad a/neu god post,
      e.e. ''3 Burleigh Road'', ''3 Burleigh Road, Stretford'',
      ''Burleigh Road, Stretford, Manchester'', ''3 Burleigh Road, M32
      0PF'', ''M32 0PF''. Os nad ydych yn gwybod y cod post cynhwyswch
      gymaint o''r cyfeiriad '' phosibl.</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Tref/rhanbarth/pentref'':</strong>Os dewiswch hwn,
      gallwch deipio enw dinas, tref, bwrdeistref, rhanbarth,
      ardal, maestref, pentref neu bentref bychan, e.e.
      ''Manchester'', ''Maidenhead'', ''Anfield'', ''Hockley'',
      ''Chelsea''</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Cyfleuster/atyniad'':</strong>Os dewiswch hwn,
      gallwch deipio enw''r atyniad neu''r cyfleuster, gan gynnwys:
      gwestai, ysgolion, prifysgolion, ysbytai, meddygfeydd,
      meysydd chwaraeon, theatrau, sinem''u, atyniadau twristaidd,
      amgueddfeydd, adeiladau''r llywodraeth a gorsafoedd yr heddlu,
      e.e. ''Edinburgh Castle'', ''Queen''s Head Hotel'', ''British
      Museum'', ''Arsenal Football Club''</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Gorsaf/Maes awyr'':</strong> Os dewiswch hyn,
      gallwch deipio enw gorsaf rheilffordd, gorsaf tanddaearol,
      gorsaf rheilffordd ysgafn, gorsaf bysiau moethus, maes awyr
      neu derfynnell fferi. Gallwch hefyd deipio enw tref a dewis
      teithio o unrhyw rai o''r gorsafoedd yn y dref hon, e.e.
      ''Llundain'', ''Derby'', ''Newcastle'', ''Gatwick''.</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Pob arhosfan'':</strong>Os dewiswch hwn, gallwch
      deipio enw arhosfan bws, stop tanddaearol, stop metro neu
      stop tram neu orsaf rheilffordd e.e. Trafalgar Square,
      Whitley Bay, Piccadilly Circus.</div>
    </li>
  </ul>
  <br />
</div>
<div class="helpTextPad1">
  <h5>3. Canfyddwch y lleoliad ar fap (dewisol)</h5>
  <br />
  <p>Gallwch glicio ar y botwm ''Canfyddwch ar y map'' i ganfod
  lleoliad ar fap.</p>
  <p>Wedi i chi ddod o hyd i''r lleoliad ar y map, bydd gennych y
  dewis o barhau i gynllunio''r siwrnai (drwy glicio ar ''Nesa'' ar y
  dudalen honno). Bydd hyn yn dod '' chi yn &#244;l i''r dudalen
  gyfredol.</p>
</div>
<h3>Dewis dyddiadau ac amserau siwrneion allan a dychwel</h3>
<div class="helpTextPad1">
  <h5>1. Dewiswch y dyddiadau yr hoffech adael/dychwelyd arnynt</h5>
  <br />
  <p>Dewiswch ddyddiad o''r rhestr a ollyngir i lawr ac yna dewiswch fis/blwyddyn</p>
  <p>neu</p>
  <p>Cliciwch ar ''Calendr'' a dewiswch ddyddiad ohono</p>
  <p>Os ydych yn cynllunio siwrnai sydd yn unffordd yn unig,
    dewiswch ''Dim dychwelyd'' yn y rhestr o''r misoedd/blwyddyn a
    ollyngir i lawr. Os hoffech gynllunio siwrnai ddychwel ond nad
    ydych yn sicr pryd yr ydych yn dychwelyd, dewiswch ''Tocyn
    dychwel agored'' yn y rhestr o''r misoedd/blwyddyn a ollyngir i
    lawr</p>
  <br />  
  <h5>2. Dewiswch sut y dymunwch nodi''r amserau</h5>
  <br />
  <p>Mae gennych ddewis o ddethol yr amser y dymunwch adael y
    lleoliad arno neu''r amser y dymunwch gyrraedd y gyrchfan erbyn
    drwy</p>
  <p>Ddewis ''Gadael am'' i ddewis yr amser cynharaf y dymunwch
    adael y lleoliad</p>
  <p>neu</p>
  <p>Dewis ''Cyrraedd erbyn'' i ddewis yr amser diweddaraf y
    dymunwch gyrraedd yn y gyrchfan</p>
  <br />
  <h5>3. Dewiswch yr amserau yr hoffech deithio</h5>
  <br />
  <p>Dewiswch yr amser y dymunwch naill ai adael neu gyrraedd</p>
  <p>Dewiswch yr oriau o''r rhestr a ollyngir i lawr</p>
  <p>Dewiswch y munudau o''r rhestr a ollyngir i lawr (mae''r
    Cynlluniwr Siwrnai yn defnyddio cloc 24 awr e.e. 5.00 p.m. =
    17.00)</p>
</div>
<h3>Dewisiadau mwy cymhleth</h3>
<div class="helpTextPad1">
  <h4>Selecting accessible journey options</h4>
  <p>Choose the type of accessible journey you are interested in being shown. 
  Transport Direct currently offers three options to choose from:</p>
  <ul>
    <li><div class="helpTextPad2">''I need a step free journey''</div></li>
    <li><div class="helpTextPad2">''I need a step free journey with staff assistance''</div></li>
	<li><div class="helpTextPad2">''I need a journey with staff assistance''</div></li>
  </ul>
  <br />
  <p>You can find more details about what each of these options means by either 
  hovering over the info button for the option or by following the link to the relevant FAQ section.</p>
  <p>If you pick an accessibility option you can also tick to request a journey with 
  the fewest changes the journey planner can find for your chosen journey.</p>

  <h4>Dewis mathau o gludiant</h4>
  <p><strong>Dewiswch y mathau o gludiant y dymunwch eu defnyddio</strong>
  </p>
    <p>Dadgliciwch y mathau o gludiant nad oes gennych ddiddordeb
    yn eu defnyddio. Ond rhaid sicrhau bod un math o leiaf yn dal
    wedi ei glicio.</p>
    <p>Bydd y Cynlluniwr Siwrnai yn chwilio am siwrneion sy''n
    ymwneud ''r mathau o gludiant cyhoeddus a diciwyd. Ond mae''n
    bosibl na fydd o anghenraid yn darganfod siwrneion sy''n
    defnyddio''r holl fathau sy''n cael eu dewis. Mae hyn oherwydd
    bod y Cynlluniwr Siwrnai yn defnyddio''r holl feini prawf yr
    ydych wedi eu nodi ac yn chwilio am y siwrneion mwyaf addas yn
    unig.</p>
 </div>
 <h3>Manylion siwrnai cludiant cyhoeddus</h3>
 <div class="helpTextPad1">
	<p>
	  <strong>Newidiadau a ffafrir (mae''n cyfeirio at newid o un cerbyd
    i''r llall)	</strong>
	</p>
 </div>
 <div class="helpTextPad1">
    <p>
      <strong>Canfyddwch (siwrneion gyda newidiadau niferus,
      ychydig o newidiadau neu ddim newidiadau)</strong>
    </p>
    <p>Dewiswch faint y dymunwch i''r Cynlluniwr Siwrnai gyfyngu ar
    ei chwiliad i siwrneion sy''n ymwneud ag ychydig o newidiadau
    neu ddim newidiadau o gwbl. Dewiswch: 
    <br /></p>
    <ul>
      <li>''Pob siwrnai (does dim bwys gennyf newid)'' i ddod o hyd i
      siwrneion sy''n gweddu i''ch amserau teithio angenrheidiol,
      waeth bynnag faint o newidiadau sy''n angenrheidiol</li>
      <li>''Siwrneion gyda nifer cyfyngedig o newidiadau'' i
      ddarganfod y siwrneion hynny sydd angen nifer fechan o
      newidiadau yn unig</li>
      <li>''Siwrneion gyda dim newidiadau'' i ddod o hyd i siwrneion
      gyda dim newidiadau. Gallai hyn gyfyngu ar nifer yr opsiynau
      o siwrneion a ganfyddir</li>
    </ul>
    <br />
    <p>
      <strong>Cyflymder (newidiadau)</strong>
    </p>
    <p>
      <strong>Dewiswch pa mor gyflym y credwch y gallwch wneud y
      newidiadau hyn. Dewiswch:</strong>
    </p>
    <ul>
      <li>''Cyflym'' os gallwch wneud y newidiadau yn gyflym</li>
      <li>''Cyfartaledd'' os gallwch wneud y newidiadau ar gyflymder
      cyffredin</li>
      <li>''Araf'' os credwch y byddwch yn gwneud y newidiadau ar
      gyflymder arafach na chyffredin</li>
    </ul>
    <p>Er enghraifft dylech ddewis ''Araf'' os ydych yn anghyfarwydd
    ''r gorsafoedd, os oes arnoch angen cymorth neu os ydych yn
    teithio gyda bagiau.</p>
    <br />
 </div>
 <div class="helpTextPad1">
  <h4>Cerdded</h4>
     <p>Bydd rhai siwrneion yn ei gwneud yn ofynnol i chi gerdded er
    mwyn mynd o leoliad i arhosfan (neu i''r gwrthwyneb), neu o un
    arhosfan i arhosfan arall.</p>
    <br />
    <p>
      <strong>Cyflymder (cerdded)</strong>
    </p>
    <p>Dewiswch eich cyflymder cerdded. Dewiswch:</p>
    <ul>
      <li>''Cyflym'' os tueddwch i gerdded yn gyflym</li>
      <li>''Cyfartaledd'' os ydych yn cerdded ar gyflymder
      cyffredin</li>
      <li>''Araf'' os credwch y byddwch yn cerdded ar gyflymder
      arafach na''r cyffredin</li>
    </ul>
    <br />
    <p>Er enghraifft dylech ddewis ''Araf'' os ydych yn teithio gyda
    bagiau trymion neu os oes gennych broblem gyda symud.</p>
    <br />
    <p>
      <strong>Amser (uchafswm)</strong>
    </p>
    <p>Dewiswch uchafswm hyd yr amser yr ydych yn barod i''w dreulio
    yn cerdded rhwng pwyntiau yn eich siwrnai o''r rhestr a ollyngir
    i lawr:</p>
    <ul>
      <li>''5'' munud</li>
      <li>''10'' munud</li>
      <li>''15'' munud</li>
      <li>''20'' munud</li>
      <li>''25'' munud</li>
      <li>''30'' munud</li>
    </ul>
    <p>Nodwch y gall gosod uchafswm cyflymder cerdded isel gyfyngu
    ar nifer y siwrneion y gellir dod o hyd iddynt. Os yn bosibl,
    argymhellir eich bod yn dewis 30 munud.</p>
 </div>
 <div class="helpTextPad1">
	<h4>Dewisiadau ar ran siwrneion</h4>
	<p><strong>Teithio drwy leoliad</strong>
	</p>
    <p>Dewiswch ble i deithio drwyddo drwy deipio''r lleoliad a
    dewis math o leoliad. I gael mwy o fanylion am sut i
    wneud hyn, gweler ''Dewis lleoliadau i deithio ohonynt ac
    iddynt'' ar ddechrau''r dudalen Help. Gallwch hefyd
    ddefnyddio map i ddod o hyd i''r lleoliad.</p>
 </div>
 <h3>Manylion siwrnai car</h3>
 <div class="helpTextPad1">
  <p><strong>Canfyddwch y siwrneion (cyflymaf, byrraf, y rhai
        mwyaf economaidd o ran tanwydd, neu rhataf yn
        gyffredinol)</strong>
  </p>
  <p>Dewiswch sut y bydd y Cynlluniwr Siwrnai yn dewis y llwybr
    gyrru gorau. Dewiswch:</p>
	<ul>
      <li>
        <div class="helpTextPad3">
		''Cyflymaf'' os hoffech lwybr gyda''r amser gyrru
        byrraf</div>
      </li>
      <li>
        <div class="helpTextPad3">
		''Byrraf'' os hoffech lwybr gyda''r pellter byrraf</div>
      </li>
      <li>
        <div class="helpTextPad3">
		''Mwyaf economaidd o ran tanwydd'' os hoffech lwybr sy''n
        defnyddio''r lleiaf o danwydd</div>
      </li>
      <li>
        <div class="helpTextPad3">
		''Rhataf yn gyffredinol'' os hoffech gael y llwybr
        rhataf sy''n cymryd holl agweddau''r siwrnai i
        ystyriaeth.</div>
      </li>
	</ul>
</div>
<div class="helpTextPad1">
    <p><strong>Cyflymder (uchafswm y cyflymder gyrru)</strong>
    </p>
</div>
<div class="helpTextPad1">
    <p>Dewiswch y cyflymder uchaf yr ydych yn barod i''w yrru o''r
    rhestr a ollyngir i lawr. Seilir amseroedd siwrneion ar y
    cyflymder hwn, ond byddant hefyd yn cymryd i ystyriaeth y
    ffiniau cyfreithiol ar gyflymder ar y gwahanol ffyrdd yn y
    llwybr arfaethedig. Gallwch ddewis o blith y canlynol:</p>
    <ul>
      <li>
        <div>''Uchafswm cyflymder cyfreithiol ar bob ffordd'' 
        <br /></div>
      </li>
      <li>
        <div>''60 mya''&#160;
        <br /></div>
      </li>
      <li>
        <div>''50 mya''&#160;&#160;
        <br /></div>
      </li>
      <li>
        <div>''40 mya''</div>
      </li>
    </ul>
  </div>
<div class="helpTextPad1">
  <p><strong>Peidiwch &#226; defnyddio traffyrdd</strong>
   </p>
  <p>Tick the box if you do not want to drive on motorways.</p>
 </div>
 <div class="helpTextPad1">
  <h4>Manylion y car</h4>
  <p>Dewiswch faint eich car a ph''run ai a oes ganddo injan betrol
  neu dd&#238;sl.</p>
  <p>Ar gyfer defnydd o danwydd gallwch ddewis ''Cyfartaledd ar
  gyfer fy math i o gar'' neu nodwch faint o danwydd y mae eich car
  yn ei ddefnyddio drwy roi nifer y milltiroedd y galwyn neu litr
  ar gyfer pob 100km.</p>
  <p>Ar gyfer cost tanwydd gallwch naill ai ddewis cost cyfartalog
  cyfredol y tanwydd neu nodi''r gost a dalwch ar gyfer pob
  litr.</p>
 </div>
 <div class="helpTextPad1">
  <h4>Dewisiadau o ran siwrneion</h4>
  <p><strong>Osgoi tollau, ffer&#239;au, traffyrdd</strong>
  </p>
    <p>Os byddai''n well gennych i''ch siwrnai osgoi tollau /
    ffer&#239;au / traffyrdd, cliciwch y blychau a lle bo hynny''n
    bosibl, bydd y Cynlluniwr Siwrnai yn ei osgoi fel nad ydynt yn
    cael eu cynnwys yng nghynllun eich siwrnai.</p>
  </div>
  <div class="helpTextPad1">
    <p><strong>Osgoi ffyrdd</strong></p>
    <p>Teipiwch rifau hyd at 6 o ffyrdd yr hoffech i''r Cynlluniwr
    Siwrnai eu hosgoi wrth gynllunio eich siwrnai. Teipiwch 1
    ffordd ym mhob blwch e.e. ''M1'', ''A23'', ''B4132''.</p>
  </div>
  <div class="helpTextPad1">
    <p><strong>I ddefnyddio ffyrdd</strong></p>
	<p>Teipiwch rifau hyd at 6 o ffyrdd yr hoffech i''r Cynlluniwr
    Siwrnai eu defnyddio wrth gynllunio eich siwrnai.Teipiwch 1
    ffordd ym mhob blwch e.e. ''M1'', ''A23'', ''B4132''.</p>
  </div>
  <div class="helpTextPad1">
    <p><strong>Teithio drwy leoliad mewn car</strong></p>
	<p>Dewiswch ble i deithio drwyddo drwy deipio''r lleoliad a
    dewis math o leoliad. I gael mwy o fanylion am sut i wneud hyn,
    gweler ''Dewis lleoliadau i deithio ohonynt ac iddynt'' ar
    ddechrau''r dudalen Help. Gallwch hefyd ddefnyddio map i ddod o
    hyd i''r lleoliad.</p>
  </div>
</div>
<h3>Wedi i chi gwblhau''r blychau sydd ar &#244;l ar y dudalen hon,
dylech glicio ar ''Nesa''.</h3>
<div class="helpTextPad1">
  <p>Yma bydd y Cynlluniwr Siwrnai yn chwilio am y lleoliad yr
  ydych wedi ei deipio. Os oes mwy nag un lleoliad yn debyg i''r
  lleoliad y bu i chi ei deipio, bydd angen i chi ddewis o restr o
  gyfatebiaethau posibl.</p>
</div>'


-- Help Journey Planner Input - Help content - Printer
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'helpfulljp')

EXEC AddtblContent
@ThemeId, @GroupId, 'HelpBodyText', '/Channels/TransportDirect/Printer/HelpJourneyPlannerInput'
,'<h3>Selecting locations to travel from and to</h3>
<div class="helpTextPad1">
  <h5>1. Type the location names in the boxes</h5>
  <br />
  <p>Transport direct uses javascript to help predict your likely intended 
  origin and destination based on what you type (note, if you have javascript 
  disabled then this feature will not work).  You can enter either a location 
  e.g. a town, district or village name, or the name of a station or airport 
  and Transport Direct will generate a list of possible options as you type.  
  Select the appropriate options from the lists and move on to the next step.  
  Transport Direct will also recognise postcodes.  If you are searching for a 
  specific address, then entering the house number followed by a space and then 
  the postcode, is often enough to find it.</p>
  <p>If you don’t find what you are looking for in the list, then you can click 
  the ''more options'' button to the right hand side of the input box.  
  Selecting this option removes the prediction but does allow you access to 
  more possible origins and destinations such as facilities / attractions and 
  other points of interest.  See point 2 below for more information about this.</p>
  <p>When using the ''more options'' function it is best to type in the full 
  location name so that you get the fewest ''similar matches'' returned to you. 
  Punctuation and use of capital letters are not important.</p>
  <p>If you are not sure how to spell the name of the location, you
  can tick the ''Unsure of spelling'' box so that the journey planner
  will also search for locations that sound similar to the one you
  type in.</p>
  <p>If you do not know the full location name, type in as much as
  you know and put an asterisk * after the letters. 
  <br /></p>
  <div class="helpTextPad3">
    <p>e.g. If you selected ''Station/airport'' and typed in ''Kin*''
    you would get all the stations and airports in Britain that
    start with the letters Kin - ''Kinsbrace'', ''Kingham'',
    ''Kings Cross Thameslink'', ''Kings Cross'' etc</p>
  </div>
  <br />
  <h5>2. Select the type of locations</h5>
  <br />
  <p>This will inform the journey planner whether you are looking
  for an address, a postcode, a station or an attraction
   etc.</p>
  <p>It is important that you select the appropriate type of
  location. For example, if you are looking for a railway
  station, but you select the ''Address/postcode'' category, the
  journey planner will not be able to find it.</p>
  <p>The categories are described below:</p>
  <ul>
    <li>
      <div class="helpTextPad2">
      <strong>''Address/postcode'':</strong>If you select this, you
      can type in part or all of an address and/or a postcode, e.g.
      ''3 Burleigh Road'', ''3 Burleigh Road, Stretford'', ''Burleigh
      Road, Stretford, Manchester'', ''3 Burleigh Road, M32 0PF'',
      ''M32 0PF''. If you don''t know the postcode include as
      much of the address as possible.</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Town/district/village'':</strong>If you select this,
      you can type in the name of a city, town, borough, district,
      area, suburb, village or hamlet, e.g. ''Manchester'',
      ''Maidenhead'', ''Anfield'', ''Hockley'', ''Chelsea''</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Facility/attraction'':</strong>If you select this,
      you can type in the name of an attraction or facility,
      including: hotels, schools, universities, hospitals,
      surgeries, sports grounds, theatres, cinemas, tourist
      attractions, museums, government buildings and police
      stations, e.g. ''Edinburgh Castle'', ''Queen''s Head Hotel'',
      ''British Museum'', ''Arsenal Football Club''</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Station/airport'':</strong> If you select this,
      you can type in the name of a railway station, an underground
      station, a light rail station, a coach station, an airport or
      a ferry terminal. You may also type in the name of a
      town and choose to travel from any of the stations in this
      town,e.g. ''London'', Derby'', ''Newcastle'', ''Gatwick''.</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''All stops'':</strong>If you select this, you can type
      in the name of a bus stop, an underground stop, a metro stop,
      a tram stop, or a railway station, e.g. ''Trafalgar Square'',
      ''Whitley Bay'', ''Piccadilly Circus''.</div>
    </li>
  </ul>
  <br />
</div>
<div class="helpTextPad1">
  <h5>3. Find the location on a map (optional)</h5>
  <br />
  <p>You may click on the ''Find on map'' button to find the location
  on a map.</p>
  <p>Once you have found the location on the map, you will have the
  option to continue planning the journey (by clicking ''Next'' on
  that page). This will return you to the current page.</p>
</div>
<h3>Selecting outward and return journey dates and times</h3>
<div class="helpTextPad1">
  <h4>Selecting outward and return journey dates</h4>
  <h5>1. Select the dates you would like to leave/return on</h5>
  <br />
  <p>Select a day from the ''drop-down'' list, then select a month/year</p>
  <p>or</p>
  <p>Click the calendar and select a date from it</p>
  <p>If you are planning a journey that is only one way, choose
    ''No return'' in the month/year ''drop-down'' list.  If
    you would like to plan a return journey but are not sure when
    you are returning, select ''Open return'' in the month/year
    ''drop-down'' list</p>
  <br />
  <h5>2. Choose how you want to specify the times</h5>
  <br />
  <p>You have the choice of selecting the time you want to leave
    the location or the time you want to arrive at the
    destination</p>
  <p>Choose ''Leaving at'' to select the earliest time you want to
    leave the location</p>
  <p>or</p>
    <p>Choose ''Arriving by'' to select the latest time you want to
    arrive at the destination</p>
  <br />
  <h5>3. Select the times you would like to travel</h5>
  <br />
  <p>Select the time you want to either leave at or arrive by</p>
  <p>Select the hours from the ''drop-down'' list</p>
  <p>Select the minutes from the ''drop-down'' list (the journey
    planner uses a 24 hour clock e.g. 5:00pm = 17:00)</p>
</div>
<h3>Advanced options</h3>
<div class="helpTextPad1">
  <h4>Selecting accessible journey options</h4>
  <p>Choose the type of accessible journey you are interested in being shown. 
  Transport Direct currently offers three options to choose from:</p>
  <ul>
    <li><div class="helpTextPad2">''I need a step free journey''</div></li>
    <li><div class="helpTextPad2">''I need a step free journey with staff assistance''</div></li>
	<li><div class="helpTextPad2">''I need a journey with staff assistance''</div></li>
  </ul>
  <br />
  <p>You can find more details about what each of these options means by either 
  hovering over the info button for the option or by following the link to the relevant FAQ section.</p>
  <p>If you pick an accessibility option you can also tick to request a journey with 
  the fewest changes the journey planner can find for your chosen journey.</p>

  <h4>Selecting types of transport</h4>
  <p><strong>Choose the types of transport you would like to use</strong>
  </p>
  <p>Untick the types of transport that you are not interested in
    using. However, at least one type must remain ticked.</p>
  <p>The journey planner will search for journeys that involve
    the 
    <strong>ticked</strong> types of public transport. 
    However, it may not necessarily find journeys that use all the
    types that are selected. This is because the journey
    planner uses 
    <strong>all</strong> the criteria you enter and searches for the
    most suitable journeys only.</p>
</div>
<h3>Public Transport journey details</h3>
<div class="helpTextPad1">
	<p>
	  <strong>Changes (refers to changing from one vehicle to
	  another)</strong>
	</p>
</div>
<div class="helpTextPad1">
  <p>
    <strong>Find (journeys with unlimited, few or no
    changes)</strong>
  </p>
  <p>Choose how much you want the journey planner to limit its
  search to journeys that involve a few changes or no
  changes. Select:
  <br /></p>
  <ul>
    <li>''All journeys (I don''t mind changing)'' to find journeys
    that best fit your required travel times, regardless of the
    number of changes required</li>
    <li>''Journeys with a limited number of changes'' to find only
    those journeys requiring a small number of changes</li>
    <li>''Journeys with no changes'' to find journeys with no
    changes. This could limit the number of journey options
    found</li>
  </ul>
  <br />
  <p>
    <strong>Speed (of changes)</strong>
  </p>
  <p>Choose how quickly you think you can make these changes. 
  Select:</p>
  <ul>
    <li>''Fast'' if you can make the changes quickly</li>
    <li>''Average'' if you can make the changes at an average
    pace</li>
    <li>''Slow'' if you think you will be making the changes at a
    slower than average pace </li>
  </ul>
  <p>For example, you should choose ''Slow'' if you are unfamiliar
  with the stations, require assistance or are travelling with
  luggage.</p>
</div>
<div class="helpTextPad1">
  <h4>Walking</h4>
  <p>Some journeys will require you to walk in order to get from a
  location to a stop (or vice versa), or from one stop to another
  stop. </p>
  <br />
  <p>
    <strong>Speed (of walking)</strong>
  </p>
  <p>Choose your walking speed. Select:</p>
  <ul>
    <li>''Fast'' if you tend to walk quickly</li>
    <li>''Average'' if you walk at an average pace</li>
    <li>''Slow'' if you think you will be walking at a slower
    than average pace</li>
  </ul>
  <br />
  <p>For example you should choose ''Slow'' if you are travelling
  with heavy luggage or if you have a mobility problem.</p>
  <br />
  <p>
    <strong>Time (max.)</strong>
  </p>
  <p>Choose the maximum length of time you are prepared to spend
  walking between points in your journey from the ''drop-down''
  list:</p>
  <ul>
    <li>''5'' minutes </li>
    <li>''10'' minutes</li>
    <li>''15'' minutes</li>
    <li>''20'' minutes </li>
    <li>''25'' minutes</li>
    <li>''30'' minutes</li>
  </ul>
  <p>Please note that setting a low maximum walking speed may limit
  the number of journeys that can be found. If possible, it
  is recommended you choose 30 minutes.</p>
</div>
<div class="helpTextPad1">
	<h4>Journey options</h4>
	<p><strong>Travelling via a location</strong>
	</p>
  <p>Choose where to travel via by typing in the location and
  selecting a location type. For more details on how to do
  this, see ''Selecting locations to travel from and to'' at the
  beginning of the Help page. You may also use a map to find
  the location.</p>
</div>
<h3>Car journey details</h3>
<div class="helpTextPad1">
    <p><strong>Find (quickest, shortest, most fuel economic or
      cheapest overall) journeys</strong>
	</p>
  <p>Choose how the journey planner will select the best driving
  route. Select:</p>
	<ul>
  <li>
    <div class="helpTextPad3">
    <strong>''Quickest''</strong>if you would like a route with the
    shortest driving time</div>
  </li>
  <li>
    <div class="helpTextPad3">
    <strong>''Shortest''</strong>if you would like a route with the
    shortest distance</div>
  </li>
  <li>
    <div class="helpTextPad3">
    <strong>''Most fuel economic''</strong>if you would like a route
    with the lowest fuel consumption</div>
  </li>
  <li>
    <div class="helpTextPad3">
    <strong>''Cheapest overall''</strong>if you would like the
    cheapest route which takes all aspects of the journey into
    account.</div>
  </li>
	</ul>
</div>
<div class="helpTextPad1">
    <p><strong>Speed (maximum driving speed)</strong>
    </p>
</div>
<div class="helpTextPad1">
  <p>Choose the maximum speed you are willing to drive from the
  ''drop-down'' list. Journey times will be based on this
  speed, but will also take into account the legal speed limits on
  the various roads in the proposed route. You can select
  from:</p>
  <ul>
    <li>
      <div>''Max. legal speed on all roads''
      <br /></div>
    </li>
    <li>
      <div>''60 mph'' 
      <br /></div>
    </li>
    <li>
      <div>''50 mph''  
      <br /></div>
    </li>
    <li>
      <div>''40 mph''</div>
    </li>
  </ul>
</div>
<div class="helpTextPad1">
  <p><strong>Do not use motorways</strong>
    </p>
  <p>Tick the box if you do 
  <strong>not</strong> want to drive on motorways.</p>
</div>
<div class="helpTextPad1">
	<h4>Car details</h4>
	<p>Choose the size of your car and whether it is a petrol or diesel
	engined car.</p>
	<p>For fuel consumption you can either select ''Average for my type
	of car'' or specify the amount of fuel your car consumes by entering
	the number of miles per gallon or litres per 100 kilometres.</p>
	<p>For fuel cost you can either select the current average cost of
	fuel or specify the cost you pay per litre.</p>
</div>
<div class="helpTextPad1">
	<h4>Journey options </h4>
	<p><strong>Avoid tolls, ferries, motorways</strong>
    </p>
	<p>If you would prefer your journey to avoid tolls/ ferries/
  motorways, tick the boxes and, where possible, the journey
  planner will avoid them so that they are not included in your
  journey plan.</p>
</div>
<div class="helpTextPad1">
    <p><strong>Avoid roads with restricted access</strong></p>
	<p>The Journey Planner will always attempt to take account of
  roads with restricted access (for example, where a road is
  pedestrian-only during shopping hours) on the days and times on
  which the restriction applies. Sometimes information about
  exactly when these restrictions apply is not available &#8211;
  tick this box if you would like the Journey Planner to avoid
  roads for which this is the case.</p>
</div>
<div class="helpTextPad1">
    <p><strong>Avoid roads</strong></p>
    <p>Type in the numbers of up to 6 roads you would like the
  journey planner to avoid when planning your journey.  
  Type in 1 road per box. e.g. ''M1'', ''A23'', ''B4132''.</p>
</div>
<div class="helpTextPad1">
    <p><strong>To use roads</strong></p>
    <p>Type in the numbers of up to 6 roads you would like the
  journey planner to use when planning your journey. Type in 1 road
  per box. e.g. ''M1'', ''A23'', ''B4132''.</p>
</div>
<div class="helpTextPad1">
    <p><strong>Travelling via a location by car</strong></p>
	<p>Choose where to travel via by typing in the location and
  selecting a location type. For more details on how to do
  this, see ''Selecting locations to travel from and to'' at the
  beginning of the Help page. You may also use a map to find
  the location.</p>
</div>
<h3>Once you have completed the page, click ''Next''.</h3>
<div class="helpTextPad1">
  <p>At this point the journey planner will search for the
  locations you have typed in. If there is more than one location
  similar to the location you typed in, you will need to choose
  from a list of possible matches.</p>
</div>'

,'<h3>Dewis lleoliadau i deithio ohonynt ac iddynt</h3>
<div class="helpTextPad1">
  <h5>1. Teipiwch enwau''r lleoliad yn y blychau</h5>
  <br />
  <p>Mae''n well teipio enw''r lleoliad yn llawn fel y bydd y nifer
  lleiaf o ''gyfatebiaethau tebyg'' yn cael eu hanfon yn &#244;l
  atoch. Nid yw atalnodi a defnyddio prif lythrennau yn bwysig.</p>
  <p>Os nad ydych yn sicr sut i sillafu enw''r lleoliad, gallwch
  dicio''r blwch ''Ansicr o sillafiad'' fel y bydd y Cynlluniwr
  Siwrnai hefyd yn chwilio am leoliadau sy''n swnio''n debyg i''r un
  yr ydych wedi ei deipio.</p>
  <p>Os nad ydych yn gwybod enw llawn y lleoliad, teipiwch gymaint
  ag a wyddoch a rhowch * ar &#244;l y llythrennau. 
  <br /></p>
  <div class="helpTextPad3">
    <p>e.e. Os bu i chi ddewis ''Gorsaf/maes awyr'' a theipio ''Kin*''
    byddech yn cael yr holl orsafoedd a''r meysydd awyr ym Mhrydain
    sy''n dechrau gyda''r llythrennau Kin - ''Kinsbrace'', ''Kingham'',
    ''King''s Cross Thameslink'', ''King''s Cross'' ac ati.</p>
  </div> 
  <br />
  <h5>2. Dewiswch y math o leoliadau</h5>
  <br />
  <p>Bydd hyn yn hysbysu''r Cynlluniwr Siwrnai a ydych yn chwilio am
  gyfeiriad, c&#244;d post, gorsaf neu atyniad... ac ati.</p>
  <p>Mae''n bwysig eich bod yn dewis y math priodol o leoliad. Er
  enghraifft, os ydych yn chwilio am orsaf rheilffordd, ond yn
  dewis y categori ''Cyfeiriad/c&#244;d post'', ni fydd y Cynlluniwr
  Siwrnai yn gallu dod o hyd iddo.</p>
  <p>Disgrifir y categor&#180;au isod:</p>
  <ul>
    <li>
      <div class="helpTextPad2">
      <strong>''Cyfeiriad/cod post'':</strong>Os dewiswch hwn,
      gallwch deipio rhan neu''r cyfan o gyfeiriad a/neu god post,
      e.e. ''3 Burleigh Road'', ''3 Burleigh Road, Stretford'',
      ''Burleigh Road, Stretford, Manchester'', ''3 Burleigh Road, M32
      0PF'', ''M32 0PF''. Os nad ydych yn gwybod y cod post cynhwyswch
      gymaint o''r cyfeiriad '' phosibl.</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Tref/rhanbarth/pentref'':</strong>Os dewiswch hwn,
      gallwch deipio enw dinas, tref, bwrdeistref, rhanbarth,
      ardal, maestref, pentref neu bentref bychan, e.e.
      ''Manchester'', ''Maidenhead'', ''Anfield'', ''Hockley'',
      ''Chelsea''</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Cyfleuster/atyniad'':</strong>Os dewiswch hwn,
      gallwch deipio enw''r atyniad neu''r cyfleuster, gan gynnwys:
      gwestai, ysgolion, prifysgolion, ysbytai, meddygfeydd,
      meysydd chwaraeon, theatrau, sinem''u, atyniadau twristaidd,
      amgueddfeydd, adeiladau''r llywodraeth a gorsafoedd yr heddlu,
      e.e. ''Edinburgh Castle'', ''Queen''s Head Hotel'', ''British
      Museum'', ''Arsenal Football Club''</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Gorsaf/Maes awyr'':</strong> Os dewiswch hyn,
      gallwch deipio enw gorsaf rheilffordd, gorsaf tanddaearol,
      gorsaf rheilffordd ysgafn, gorsaf bysiau moethus, maes awyr
      neu derfynnell fferi. Gallwch hefyd deipio enw tref a dewis
      teithio o unrhyw rai o''r gorsafoedd yn y dref hon, e.e.
      ''Llundain'', ''Derby'', ''Newcastle'', ''Gatwick''.</div>
    </li>
    <li>
      <div class="helpTextPad2">
      <strong>''Pob arhosfan'':</strong>Os dewiswch hwn, gallwch
      deipio enw arhosfan bws, stop tanddaearol, stop metro neu
      stop tram neu orsaf rheilffordd e.e. Trafalgar Square,
      Whitley Bay, Piccadilly Circus.</div>
    </li>
  </ul>
  <br />
</div>
<div class="helpTextPad1">
  <h5>3. Canfyddwch y lleoliad ar fap (dewisol)</h5>
  <br />
  <p>Gallwch glicio ar y botwm ''Canfyddwch ar y map'' i ganfod
  lleoliad ar fap.</p>
  <p>Wedi i chi ddod o hyd i''r lleoliad ar y map, bydd gennych y
  dewis o barhau i gynllunio''r siwrnai (drwy glicio ar ''Nesa'' ar y
  dudalen honno). Bydd hyn yn dod '' chi yn &#244;l i''r dudalen
  gyfredol.</p>
</div>
<h3>Dewis dyddiadau ac amserau siwrneion allan a dychwel</h3>
<div class="helpTextPad1">
  <h5>1. Dewiswch y dyddiadau yr hoffech adael/dychwelyd arnynt</h5>
  <br />
  <p>Dewiswch ddyddiad o''r rhestr a ollyngir i lawr ac yna dewiswch fis/blwyddyn</p>
  <p>neu</p>
  <p>Cliciwch ar ''Calendr'' a dewiswch ddyddiad ohono</p>
  <p>Os ydych yn cynllunio siwrnai sydd yn unffordd yn unig,
    dewiswch ''Dim dychwelyd'' yn y rhestr o''r misoedd/blwyddyn a
    ollyngir i lawr. Os hoffech gynllunio siwrnai ddychwel ond nad
    ydych yn sicr pryd yr ydych yn dychwelyd, dewiswch ''Tocyn
    dychwel agored'' yn y rhestr o''r misoedd/blwyddyn a ollyngir i
    lawr</p>
  <br />  
  <h5>2. Dewiswch sut y dymunwch nodi''r amserau</h5>
  <br />
  <p>Mae gennych ddewis o ddethol yr amser y dymunwch adael y
    lleoliad arno neu''r amser y dymunwch gyrraedd y gyrchfan erbyn
    drwy</p>
  <p>Ddewis ''Gadael am'' i ddewis yr amser cynharaf y dymunwch
    adael y lleoliad</p>
  <p>neu</p>
  <p>Dewis ''Cyrraedd erbyn'' i ddewis yr amser diweddaraf y
    dymunwch gyrraedd yn y gyrchfan</p>
  <br />
  <h5>3. Dewiswch yr amserau yr hoffech deithio</h5>
  <br />
  <p>Dewiswch yr amser y dymunwch naill ai adael neu gyrraedd</p>
  <p>Dewiswch yr oriau o''r rhestr a ollyngir i lawr</p>
  <p>Dewiswch y munudau o''r rhestr a ollyngir i lawr (mae''r
    Cynlluniwr Siwrnai yn defnyddio cloc 24 awr e.e. 5.00 p.m. =
    17.00)</p>
</div>
<h3>Dewisiadau mwy cymhleth</h3>
<div class="helpTextPad1">
  <h4>Selecting accessible journey options</h4>
  <p>Choose the type of accessible journey you are interested in being shown. 
  Transport Direct currently offers three options to choose from:</p>
  <ul>
    <li><div class="helpTextPad2">''I need a step free journey''</div></li>
    <li><div class="helpTextPad2">''I need a step free journey with staff assistance''</div></li>
	<li><div class="helpTextPad2">''I need a journey with staff assistance''</div></li>
  </ul>
  <br />
  <p>You can find more details about what each of these options means by either 
  hovering over the info button for the option or by following the link to the relevant FAQ section.</p>
  <p>If you pick an accessibility option you can also tick to request a journey with 
  the fewest changes the journey planner can find for your chosen journey.</p>

  <h4>Dewis mathau o gludiant</h4>
  <p><strong>Dewiswch y mathau o gludiant y dymunwch eu defnyddio</strong>
  </p>
    <p>Dadgliciwch y mathau o gludiant nad oes gennych ddiddordeb
    yn eu defnyddio. Ond rhaid sicrhau bod un math o leiaf yn dal
    wedi ei glicio.</p>
    <p>Bydd y Cynlluniwr Siwrnai yn chwilio am siwrneion sy''n
    ymwneud ''r mathau o gludiant cyhoeddus a diciwyd. Ond mae''n
    bosibl na fydd o anghenraid yn darganfod siwrneion sy''n
    defnyddio''r holl fathau sy''n cael eu dewis. Mae hyn oherwydd
    bod y Cynlluniwr Siwrnai yn defnyddio''r holl feini prawf yr
    ydych wedi eu nodi ac yn chwilio am y siwrneion mwyaf addas yn
    unig.</p>
 </div>
 <h3>Manylion siwrnai cludiant cyhoeddus</h3>
 <div class="helpTextPad1">
	<p>
	  <strong>Newidiadau a ffafrir (mae''n cyfeirio at newid o un cerbyd
    i''r llall)	</strong>
	</p>
 </div>
 <div class="helpTextPad1">
    <p>
      <strong>Canfyddwch (siwrneion gyda newidiadau niferus,
      ychydig o newidiadau neu ddim newidiadau)</strong>
    </p>
    <p>Dewiswch faint y dymunwch i''r Cynlluniwr Siwrnai gyfyngu ar
    ei chwiliad i siwrneion sy''n ymwneud ag ychydig o newidiadau
    neu ddim newidiadau o gwbl. Dewiswch: 
    <br /></p>
    <ul>
      <li>''Pob siwrnai (does dim bwys gennyf newid)'' i ddod o hyd i
      siwrneion sy''n gweddu i''ch amserau teithio angenrheidiol,
      waeth bynnag faint o newidiadau sy''n angenrheidiol</li>
      <li>''Siwrneion gyda nifer cyfyngedig o newidiadau'' i
      ddarganfod y siwrneion hynny sydd angen nifer fechan o
      newidiadau yn unig</li>
      <li>''Siwrneion gyda dim newidiadau'' i ddod o hyd i siwrneion
      gyda dim newidiadau. Gallai hyn gyfyngu ar nifer yr opsiynau
      o siwrneion a ganfyddir</li>
    </ul>
    <br />
    <p>
      <strong>Cyflymder (newidiadau)</strong>
    </p>
    <p>
      <strong>Dewiswch pa mor gyflym y credwch y gallwch wneud y
      newidiadau hyn. Dewiswch:</strong>
    </p>
    <ul>
      <li>''Cyflym'' os gallwch wneud y newidiadau yn gyflym</li>
      <li>''Cyfartaledd'' os gallwch wneud y newidiadau ar gyflymder
      cyffredin</li>
      <li>''Araf'' os credwch y byddwch yn gwneud y newidiadau ar
      gyflymder arafach na chyffredin</li>
    </ul>
    <p>Er enghraifft dylech ddewis ''Araf'' os ydych yn anghyfarwydd
    ''r gorsafoedd, os oes arnoch angen cymorth neu os ydych yn
    teithio gyda bagiau.</p>
    <br />
 </div>
 <div class="helpTextPad1">
  <h4>Cerdded</h4>
     <p>Bydd rhai siwrneion yn ei gwneud yn ofynnol i chi gerdded er
    mwyn mynd o leoliad i arhosfan (neu i''r gwrthwyneb), neu o un
    arhosfan i arhosfan arall.</p>
    <br />
    <p>
      <strong>Cyflymder (cerdded)</strong>
    </p>
    <p>Dewiswch eich cyflymder cerdded. Dewiswch:</p>
    <ul>
      <li>''Cyflym'' os tueddwch i gerdded yn gyflym</li>
      <li>''Cyfartaledd'' os ydych yn cerdded ar gyflymder
      cyffredin</li>
      <li>''Araf'' os credwch y byddwch yn cerdded ar gyflymder
      arafach na''r cyffredin</li>
    </ul>
    <br />
    <p>Er enghraifft dylech ddewis ''Araf'' os ydych yn teithio gyda
    bagiau trymion neu os oes gennych broblem gyda symud.</p>
    <br />
    <p>
      <strong>Amser (uchafswm)</strong>
    </p>
    <p>Dewiswch uchafswm hyd yr amser yr ydych yn barod i''w dreulio
    yn cerdded rhwng pwyntiau yn eich siwrnai o''r rhestr a ollyngir
    i lawr:</p>
    <ul>
      <li>''5'' munud</li>
      <li>''10'' munud</li>
      <li>''15'' munud</li>
      <li>''20'' munud</li>
      <li>''25'' munud</li>
      <li>''30'' munud</li>
    </ul>
    <p>Nodwch y gall gosod uchafswm cyflymder cerdded isel gyfyngu
    ar nifer y siwrneion y gellir dod o hyd iddynt. Os yn bosibl,
    argymhellir eich bod yn dewis 30 munud.</p>
 </div>
 <div class="helpTextPad1">
	<h4>Dewisiadau ar ran siwrneion</h4>
	<p><strong>Teithio drwy leoliad</strong>
	</p>
    <p>Dewiswch ble i deithio drwyddo drwy deipio''r lleoliad a
    dewis math o leoliad. I gael mwy o fanylion am sut i
    wneud hyn, gweler ''Dewis lleoliadau i deithio ohonynt ac
    iddynt'' ar ddechrau''r dudalen Help. Gallwch hefyd
    ddefnyddio map i ddod o hyd i''r lleoliad.</p>
 </div>
 <h3>Manylion siwrnai car</h3>
 <div class="helpTextPad1">
  <p><strong>Canfyddwch y siwrneion (cyflymaf, byrraf, y rhai
        mwyaf economaidd o ran tanwydd, neu rhataf yn
        gyffredinol)</strong>
  </p>
  <p>Dewiswch sut y bydd y Cynlluniwr Siwrnai yn dewis y llwybr
    gyrru gorau. Dewiswch:</p>
	<ul>
      <li>
        <div class="helpTextPad3">
		''Cyflymaf'' os hoffech lwybr gyda''r amser gyrru
        byrraf</div>
      </li>
      <li>
        <div class="helpTextPad3">
		''Byrraf'' os hoffech lwybr gyda''r pellter byrraf</div>
      </li>
      <li>
        <div class="helpTextPad3">
		''Mwyaf economaidd o ran tanwydd'' os hoffech lwybr sy''n
        defnyddio''r lleiaf o danwydd</div>
      </li>
      <li>
        <div class="helpTextPad3">
		''Rhataf yn gyffredinol'' os hoffech gael y llwybr
        rhataf sy''n cymryd holl agweddau''r siwrnai i
        ystyriaeth.</div>
      </li>
	</ul>
</div>
<div class="helpTextPad1">
    <p><strong>Cyflymder (uchafswm y cyflymder gyrru)</strong>
    </p>
</div>
<div class="helpTextPad1">
    <p>Dewiswch y cyflymder uchaf yr ydych yn barod i''w yrru o''r
    rhestr a ollyngir i lawr. Seilir amseroedd siwrneion ar y
    cyflymder hwn, ond byddant hefyd yn cymryd i ystyriaeth y
    ffiniau cyfreithiol ar gyflymder ar y gwahanol ffyrdd yn y
    llwybr arfaethedig. Gallwch ddewis o blith y canlynol:</p>
    <ul>
      <li>
        <div>''Uchafswm cyflymder cyfreithiol ar bob ffordd'' 
        <br /></div>
      </li>
      <li>
        <div>''60 mya''&#160;
        <br /></div>
      </li>
      <li>
        <div>''50 mya''&#160;&#160;
        <br /></div>
      </li>
      <li>
        <div>''40 mya''</div>
      </li>
    </ul>
  </div>
<div class="helpTextPad1">
  <p><strong>Peidiwch &#226; defnyddio traffyrdd</strong>
   </p>
  <p>Tick the box if you do not want to drive on motorways.</p>
 </div>
 <div class="helpTextPad1">
  <h4>Manylion y car</h4>
  <p>Dewiswch faint eich car a ph''run ai a oes ganddo injan betrol
  neu dd&#238;sl.</p>
  <p>Ar gyfer defnydd o danwydd gallwch ddewis ''Cyfartaledd ar
  gyfer fy math i o gar'' neu nodwch faint o danwydd y mae eich car
  yn ei ddefnyddio drwy roi nifer y milltiroedd y galwyn neu litr
  ar gyfer pob 100km.</p>
  <p>Ar gyfer cost tanwydd gallwch naill ai ddewis cost cyfartalog
  cyfredol y tanwydd neu nodi''r gost a dalwch ar gyfer pob
  litr.</p>
 </div>
 <div class="helpTextPad1">
  <h4>Dewisiadau o ran siwrneion</h4>
  <p><strong>Osgoi tollau, ffer&#239;au, traffyrdd</strong>
  </p>
    <p>Os byddai''n well gennych i''ch siwrnai osgoi tollau /
    ffer&#239;au / traffyrdd, cliciwch y blychau a lle bo hynny''n
    bosibl, bydd y Cynlluniwr Siwrnai yn ei osgoi fel nad ydynt yn
    cael eu cynnwys yng nghynllun eich siwrnai.</p>
  </div>
  <div class="helpTextPad1">
    <p><strong>Osgoi ffyrdd</strong></p>
    <p>Teipiwch rifau hyd at 6 o ffyrdd yr hoffech i''r Cynlluniwr
    Siwrnai eu hosgoi wrth gynllunio eich siwrnai. Teipiwch 1
    ffordd ym mhob blwch e.e. ''M1'', ''A23'', ''B4132''.</p>
  </div>
  <div class="helpTextPad1">
    <p><strong>I ddefnyddio ffyrdd</strong></p>
	<p>Teipiwch rifau hyd at 6 o ffyrdd yr hoffech i''r Cynlluniwr
    Siwrnai eu defnyddio wrth gynllunio eich siwrnai.Teipiwch 1
    ffordd ym mhob blwch e.e. ''M1'', ''A23'', ''B4132''.</p>
  </div>
  <div class="helpTextPad1">
    <p><strong>Teithio drwy leoliad mewn car</strong></p>
	<p>Dewiswch ble i deithio drwyddo drwy deipio''r lleoliad a
    dewis math o leoliad. I gael mwy o fanylion am sut i wneud hyn,
    gweler ''Dewis lleoliadau i deithio ohonynt ac iddynt'' ar
    ddechrau''r dudalen Help. Gallwch hefyd ddefnyddio map i ddod o
    hyd i''r lleoliad.</p>
  </div>
</div>
<h3>Wedi i chi gwblhau''r blychau sydd ar &#244;l ar y dudalen hon,
dylech glicio ar ''Nesa''.</h3>
<div class="helpTextPad1">
  <p>Yma bydd y Cynlluniwr Siwrnai yn chwilio am y lleoliad yr
  ydych wedi ei deipio. Os oes mwy nag un lleoliad yn debyg i''r
  lleoliad y bu i chi ei deipio, bydd angen i chi ddewis o restr o
  gyfatebiaethau posibl.</p>
</div>'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10019
SET @ScriptDesc = 'Updates to Help text for Journey Planner Input page'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.1  $'

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