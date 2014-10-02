-- *************************************************************************************
-- NAME 		: SCP10907_CycleRoutes_Cycle_7_Accessibility.sql
-- DESCRIPTION  : Updates to Accessibility content
-- AUTHOR		: Amit Patel
-- DATE         : 20 Dec 2010
-- *************************************************************************************

-- **** IMPORTANT ****
-- ENSURE THE CHANGE CATALOGUE @ScriptNumber VALUE IS UPDATED WHEN COPYING TO INCREMENTAL UPDATES FOLDER



USE [Content]
GO

DECLARE @ThemeId INT
SET @ThemeId = 201

-------------------------------------------------------------
-- Accessibility Content
-------------------------------------------------------------
EXEC AddtblContent
@ThemeId, 20, 'TitleText', '/Channels/TransportDirect/About/Accessibility',
'<div>
    <h1>
        Accessibility
    </h1>
</div>'
,
'<div>
    <h1>
        Hygyrchedd
    </h1>
</div>'


EXEC AddtblContent
@ThemeId, 20, 'Body Text', '/Channels/TransportDirect/About/Accessibility', 
'<div id="primcontent">
<div id="contentarea">
<div id="hdtypethree">
<h2>Accessibility</h2></div>
<p>Transport Direct is committed to providing usable and accessible information. We believe that testing the Transport Direct portal with users of various abilities and disabilities is the best way to improve the accessibility of the portal.</p>
<p>&nbsp;</p>
<p>Through our ongoing Usability and Accessibility testing programmes we ask testers to perform typical tasks on the portal with the objective of finding out if they experience any difficulties. The findings from these tests are used to make design enhancements to the portal. For example</p>
<p>&nbsp;</p>
<ul class="lister">
<li>We have designed the pages on the site so that as far as possible they do not require horizontal scrollbars when viewed at a resolution of 1024 x 768 pixels </li>
<li>All images have an alternative text description so that they can be understood by users of screen readers </li>
<li>Our journey itineraries can be presented in tables as well as using a graphical time line </li></ul>
<p></p>
<p></p>
<p></p>
<p></p>
<p>&nbsp;</p>
<p>We also welcome <a href="/Web2/ContactUs/FeedbackPage.aspx">feedback</a>, particularly from users with any disabilities, about any specific problems they have encountered in using the Transport Direct portal. </p>
<p></p>
<p></p>
<p></p>
<p>&nbsp;</p>
<p>This portal currently conforms to Level AA of the Web Accessibility Guidelines (WAI) produced by the World Wide Web Consortium (W3C).</p>
<p>&nbsp;</p>
<a title="Explanation of Level AA Conformance" href="http://www.w3.org/WAI/WCAG1A-Conformance"><img height="32" alt="Level AA &#13;&#10;&#13;&#10;&#13;&#10;conformance icon, &#13;&#10; W3C-WAI Web Content Accessibility Guidelines 1.0" src="http://www.w3.org/WAI/wcag1AA" width="88" /></a> 
<p></p>
<p>&nbsp;</p>
<h3>Additional tips for screen reader users</h3>
<p>&nbsp;</p>
<p>This section outlines some tips that could be useful to people who use the site with screen reading software.</p>
<p>&nbsp;</p>
<p><strong>Overview of navigation </strong></p>
<p>There are several ways to navigate to features within the site. </p>
<p>&nbsp;</p>
<p>The main navigation consists of five main links located towards the top of the page: &#8216;Home&#8217;; &#8216;Plan a journey&#8217;; &#8216;Find a place&#8217;; &#8216;Live travel&#8217;; and &#8216;Tips and tools&#8217;.&nbsp;Clicking on the &#8216;Home&#8217; link will open the home page.&nbsp;Clicking on any of the other links will open an overview page containing a set of sub-links, each linking to one of several related functions.&nbsp; For example, clicking on the &#8216;Plan a journey&#8217; link will open a page containing an overview of Transport Direct''s journey planning features and links to each individual journey planner, such as &#8216;Find a train&#8217; or &#8216;Find a car route&#8217;.&nbsp;Overview pages also contain links to other relevant features.&nbsp;For example, there is a link to &#8216;Live travel news&#8217; on the &#8216;Plan a journey&#8217; overview page.</p>
<p>&nbsp;</p>
<p>These main links are present on every page (excluding printable pages and the ticket purchase page).&nbsp;They are also repeated in the left hand menu on the homepage and on the overview/menu pages.&nbsp;On the homepage, if you have scripting enabled, clicking on one of these links will reveal further links to all the functions within that category.&nbsp;If you have scripting disabled, all the links will be shown at the same time (including the five main links).</p>
<p>&nbsp;</p>
<p>There is a further set of links at the foot of every page, including links to help, contact details and accessibility information.</p>
<p>&nbsp;</p>
<p>There are also quick links on the homepage to functions such as &#8216;Find a train&#8217; or &#8216;Find a place&#8217;.</p>
<p>&nbsp;</p>
<p><strong>Skip links </strong></p>
<p>Throughout the site, there are special links that allow you to skip past sections of the page.&nbsp;This can be useful when pages contain repeated content and links, such as the main navigation links.</p>
<p>&nbsp;</p>
<p><strong>Overview of planning a journey</strong></p>
<p>This section outlines the main steps involved with finding a journey.</p>
<p>&nbsp;</p>
<p>To find a journey, you will typically need to follow this process:</p>
<p>&nbsp;</p>
<p>Step 1:&nbsp;Choose the type of planner you need, such as &#8216;Find a train&#8217;.&nbsp; Alternatively, there is a section on the homepage which acts as the first page of the door-to-door journey planner.</p>
<p>&nbsp;</p>
<p>Step 2: In first page of the chosen planner, you will need to enter and/or select the locations you want to travel from and to.&nbsp; You may also need to enter further information, such as the date and time of travel.&nbsp; You must then click &#8216;Next&#8217;.</p>
<p>&nbsp;</p>
<p>Step 3: If one or more of the locations are ambiguous, you will be presented with a list of choices.&nbsp; Choose the option which matches your preferred location.&nbsp; This page will also indicate if there are any problems with the journey request, such as inconsistent or missing times and dates.&nbsp; Click "Next" to continue.</p>
<p>&nbsp;</p>
<p>Step 4: The system will then search for appropriate journey options.&nbsp; While it is doing this, the "Searching" page will automatically refresh every few seconds.</p>
<p>&nbsp;</p>
<p>Step 5: The next page to open will show you a list of journey options - both for outward and return, if you have requested a return journey.&nbsp; These are shown in rows on a table.&nbsp; At the end of each row is a button to select the option.&nbsp; The first option is selected by default. Before the table is a set of action buttons called &#8216;Details&#8217;, &#8216;Maps&#8217;, &#8216;Tickets/Costs&#8217; and &#8216;Modify this journey&#8217;.&nbsp; These allow you to get further information about whichever journey option you have selected.</p>
<p>&nbsp;</p>
<p>If you wish to view details for a journey, such as directions, interchanges and fares, you must first select the journey by clicking the button at the end of its row and then navigate back to the appropriate action button.</p>
<p>&nbsp;</p>
<p>Please note that selecting a journey option will refresh the page.</p>
<p>&nbsp;</p>
<p>When you have opened the &#8216;Details&#8217; page of a journey, we would recommend that you click on the &#8216;Show in table&#8217; link.&nbsp; This displays the details in a text-only format rather than as pictures and text.</p>
<p>&nbsp;</p>
<p>There are several variations of journey planner, such as &#8216;Compare door-to-door journeys&#8217;, &#8216;Find a flight&#8217; and &#8216;Find a coach&#8217;.&nbsp; They all work in a similar way, except for the &#8216;Day trip planner&#8217;, in which you can choose options for each of the three stages of your journey.&nbsp; These are shown in three tables, one for each of the stages of the journey.&nbsp; You should choose an option from each of the three tables by selecting the button that appears before each option.&nbsp; Each journey option shows a list of the types of transport involved for that option.&nbsp; For example a journey that requires you to take a train, then a bus and a walk, might be described as &#8216;Train and Bus and Walk&#8217;.</p>
<p>&nbsp;</p>
<p>After these three tables is a drop-down menu list that allows you to choose to get further details about the combination of journey options you have selected.&nbsp; We recommend you select the details as a table, and click &#8216;OK&#8217;.</p>
<p>&nbsp;</p>
<p><strong>Ways to improve readability</strong></p>
<p>You may wish to improve the readability of the pages by changing your browser settings.</p>
<p>&nbsp;</p>
<p>For example: </p>
<ul class="lister">
<li>increase the browser font size </li>
<li>switch off images </li>
<li>change the background and text colours </li></ul>
<p></p>
<p>&nbsp;</p>
<p><strong>Accessibility of transport</strong></p>
<p>Travellers who have disabilities or mobility issues will be able to get information about the accessibility of different types of transport when planning journeys.&nbsp; Transport Direct has links on journey planning pages as well as on the journey results pages.</p><br />
<p>The links below will open other websites to show information on the accessibility of transport:</p><br />
<p><u><font color="#0000ff">
<a href="http://www.nationalrail.co.uk/passenger_services/disabled_passengers/" target="_blank" >Maps of railway stations with access for people with reduced mobility <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a><br />
<a href="http://www.direct.gov.uk/en/DisabledPeople/MotoringAndTransport/index.htm" target="_blank">Door-to-door:&nbsp; A travel guide for disabled people <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a><br />
<a href="http://www.direct.gov.uk/en/Dl1/Directories/DG_6000229" target="_blank">In and around London <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a><br />
<a href="http://www.direct.gov.uk/en/TravelAndTransport/Publictransport/index.htm" target="_blank" >Useful Organisations <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a><br /><br /></font></u></p>
<p>&nbsp;</p>
<div></div></div></div>
<div></div>'
,

'<div id="primcontent">
<div id="contentarea">
<div id="hdtypethree">
<div align="right"><a id="jpt" href="#"></a></div>
<h2>Hygyrchedd</h2></div>
<p>Mae Transport Direct yn ymrwymedig i ddarparu gwybodaeth ddefnyddiol a hygyrch. Credwn mai profi porth Transport Direct gyda defnyddwyr o wahanol alluoedd ac anableddau yw''r ffordd orau i wella hygyrchedd y porth.</p>
<p>&nbsp;</p>
<p>Drwy ein rhaglenni profi parhaus Defnyddioldeb a Hygyrchedd rydym yn gofyn i''r profwyr berfformio tasgau nodweddiadol ar y porth gyda''r nod o gael gwybod os ydynt yn profi unrhyw anawsterau. Defnyddir y darganfyddiadau o''r profion hyn i wneud gwelliannau cynllunio i''r porth. Er enghraifft</p>
<p>&nbsp;</p>
<ul class="lister">
<li>Rydym wedi cynllunio''r tudalennau ar y safle hyn hyd eithaf ein gallu fel nad ydynt angen bariau rholio llorweddol wrth iddynt gael eu gweld ar gydraniad o 1024 x 768 picsel </li>
<li>Mae gan y delweddau i gyd ddisgrifiad testun amgen fel bod modd i ddefnyddwyr darllenwyr sgr&#238;n eu deall </li>
<li>Gellir cyflwyno ein teithlyfrau mewn tablau ynghyd &#226; llinell amser graffigol</li></ul>
<p></p>
<p></p>
<p></p>
<p></p>
<p></p>
<p></p>
<p></p>
<p>&nbsp;</p>
<p><a href="/Web2/ContactUs/FeedbackPage.aspx">Rydym hefyd yn croesawu atborth</a>, yn arbennig oddi wrth ddefnyddwyr gydag unrhyw anableddau, ynglyn &#226; phroblemau penodol y daethant ar eu traws tra''n defnyddio porth Transport Direct. </p>
<p></p>
<p></p>
<p></p>
<p>&nbsp;</p>
<p>Ar hyn o bryd mae''r porth hwn yn cydymffurfio &#226; Lefel A, Canllawiau Hygyrchedd y We (WAI) a gynhyrchwyd gan Gonsortiwm y We Fyd-Eang (W3C).</p>
<p>&nbsp;</p><a title="Eglurhad o Gydymffurfiad Lefel A" href="http://www.w3.org/WAI/WCAG1A-Conformance"><img height="32" alt="Eicon &#13;&#10;&#13;&#10;&#13;&#10;&#13;&#10;cydymffurfiad Lefel A, &#13;&#10;          W3C-WAI Web Content &#13;&#10;&#13;&#10;Accessibility &#13;&#10;Guidelines 1.0" src="http://www.w3.org/WAI/wcag1A" width="88" /></a> 
<p></p>
<p>&nbsp;</p>
<p>Lle fo''n bosibl mae''n cydymffurfio &#226; Lefel AA.</p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<h3>Awgrymiadau ychwanegol ar gyfer defnyddwyr darllenwyr sgr&#238;n</h3>
<p>&nbsp;</p>
<p>Mae&#8217;r adran hon yn amlinellu rhai awgrymiadau a allai fod yn ddefnyddiol i bobl sy&#8217;n defnyddio&#8217;r safle gyda meddalwedd darllen sgr&#238;n.</p>
<p>&nbsp;</p>
<p><strong>Gorolwg o&#8217;r ffordd o gwmpas</strong></p>
<p>Mae nifer o ffyrdd o symud o gwmpas i nodweddion o fewn y safle. </p>
<p>&nbsp;</p>
<p>Mae&#8217;r brif ffordd o symud o gwmpas yn cynnwys pump o brif ddolennau a leolir tuag at frig y dudalen: &#8216;Cartref&#8217;, &#8216;Cynlluniwch siwrnai&#8217;, &#8216;Canfyddwch le&#8217;, &#8216;Teithio byw&#8217; ac &#8216;Awgrymiadau a theclynnau&#8217;.&nbsp;Bydd clicio ar y ddolen &#8216;Cartref&#8217; yn agor y dudalen gartref.&nbsp;Bydd clicio ar unrhyw rai o&#8217;r dolennau eraill yn agor tudalen gorolwg yn cynnwys set o is-ddolennau, gyda phob un yn cysylltu ag un o nifer o swyddogaethau cysylltiedig.&nbsp; Er enghraifft, bydd clicio ar ddolen &#8216;Cynlluniwch siwrnai&#8217; yn agor tudalen yn cynnwys gorolwg o nodweddion cynllunio siwrnai Transport Direct a dolennau i bob cynlluniwr siwrnai unigol, fel &#8216;Canfyddwch dr&#234;n&#8217; neu &#8216;Canfyddwch lwybr car&#8217;.&nbsp;Mae tudalennau gorolwg hefyd yn cynnwys dolennau i nodweddion perthnasol eraill.&nbsp;Er enghraifft, mae dolen i &#8216;Newyddion teithio byw&#8217; ar dudalen gorolwg &#8216;Cynlluniwch siwrnai&#8217;.</p>
<p>&nbsp;</p>
<p>Mae&#8217;r prif ddolennau hyn yn bresennol ar bob tudalen (heb gynnwys tudalennau y gellir eu hargraffu a&#8217;r dudalen prynu tocynnau).&nbsp;Maen nhw''n cael eu hailadrodd hefyd yn y ddewislen ar y chwith ar y dudalen gartref ac ar y tudalennau trosolwg/dewislen.&nbsp;Ar y dudalen gartref, os yw sgriptio wedi ei alluogi, bydd clicio ar un o&#8217;r dolennau hyn yn datgelu mwy o ddolennau i&#8217;r holl swyddogaethau o fewn y categori hwnnw.&nbsp;Os yw sgriptio wedi ei analluogi, bydd yr holl ddolennau yn cael eu dangos yr un pryd (gan gynnwys y pump o brif ddolennau).</p>
<p>&nbsp;</p>
<p>Mae set arall o ddolennau ar droed pob tudalen, gan gynnwys dolennau i help, manylion cyswllt a gwybodaeth am hygyrchedd.</p>
<p>&nbsp;</p>
<p>Ceir hefyd ddolennau cyflym ar y dudalen gartref i swyddogaethau fel &#8216;Canfyddwch dr&#234;n&#8217; neu &#8216;Canfyddwch le&#8217;.</p>
<p>&nbsp;</p>
<p><strong>Dolennau neidio</strong></p>
<p>Drwy gydol y safle, ceir dolennau arbennig sy&#8217;n caniat&#225;u i chi neidio heibio adrannau o&#8217;r dudalen.&nbsp;Gall hyn fod yn ddefnyddiol pan fo tudalennau yn cynnwys deunydd a dolennau sy&#8217;n cael eu hailadrodd, fel y prif ddolennau symud o gwmpas.</p>
<p>&nbsp;</p>
<p><strong>Gorolwg o gynllunio siwrnai</strong></p>
<p>Mae&#8217;r adran hon yn amlinellu&#8217;r prif gamau sy&#8217;n ymwneud &#226; darganfod siwrnai.</p>
<p>&nbsp;</p>
<p>I ddarganfod siwrnai, bydd angen i chi ddilyn y broses hon fel arfer:</p>
<p>&nbsp;</p>
<p>Cam 1:&nbsp;Dewiswch y math o gynlluniwr sydd arnoch ei angen, fel &#8216;Canfyddwch dr&#234;n&#8217;.&nbsp; Neu, mae adran ar y dudalen gartref sy&#8217;n gweithredu fel tudalen gyntaf y cynlluniwr siwrnai o ddrws-i-ddrws.</p>
<p>&nbsp;</p>
<p>Cam 2: Yn nhudalen gyntaf y cynlluniwr a ddewiswyd, bydd angen i chi gofnodi a/neu ddethol y lleoliadau y dymunwch deithio ohonynt ac iddynt.&nbsp; Efallai y bydd angen i chi roi gwybodaeth bellach hefyd, fel y dyddiad ac amser teithio.&nbsp; Rhaid i chi wedyn glicio ar &#8216;Nesaf&#8217;.</p>
<p>&nbsp;</p>
<p>Cam 3: Os oes un neu fwy o&#8217;r lleoliadau yn amwys, rhoddir rhestr o ddewisiadau i chi.&nbsp; Dewiswch yr opsiwn sy&#8217;n cyfateb i&#8217;r lleoliad sydd orau gennych.&nbsp; Bydd y dudalen hon hefyd yn nodi a oes unrhyw broblemau gyda&#8217;r cais am siwrnai, fel amserau a dyddiadau anghyson neu rai sydd ar goll.&nbsp; Cliciwch ar &#8216;Nesaf&#8217; i barhau.</p>
<p>&nbsp;</p>
<p>Cam 4: Bydd y system wedyn yn chwilio am ddewisiadau siwrnai priodol.&nbsp; Tra bo&#8217;n gwneud hyn, bydd y dudalen &#8216;Chwilio&#8217; yn adnewyddu ei hun yn awtomatig bob ychydig eiliadau.</p>
<p>&nbsp;</p>
<p>Cam 5: Bydd y dudalen nesaf i&#8217;w hagor yn dangos rhestr o ddewisiadau siwrnai &#8211; ar gyfer siwrnai allanol a siwrnai ddychwel, os ydych wedi gofyn am siwrnai ddychwel.&nbsp; Dangosir y rhain mewn rhesi ar dabl.&nbsp; Ar ben pob rhes ceir botwm i ddewis yr opsiwn.&nbsp; Dewisir yr opsiwn cyntaf yn ddiofyn. O flaen y tabl ceir set o fotymau gweithredu o&#8217;r enw &#8216;Manylion&#8217;, &#8216;Mapiau&#8217;, Tocynnau/costau&#8217; a &#8216;Diwygiwch y siwrnai hon&#8217;.&nbsp; Mae&#8217;r rhain yn caniat&#225;u i chi gael gwybodaeth bellach am ba bynnag ddewisiadau siwrnai yr ydych wedi eu dethol.</p>
<p>&nbsp;</p>
<p>Os dymunwch edrych ar fanylion am siwrnai, fel cyfarwyddiadau, rhyngnewidiadau, a phrisiau tocynnau, rhaid i chi yn gyntaf ddewis y siwrnai drwy glicio ar y botwm ar ben y rhes briodol ac yna canfod eich ffordd yn &#244;l i&#8217;r botwm gweithredu priodol.</p>
<p>&nbsp;</p>
<p>Nodwch y bydd opsiwn dewis siwrnai yn adnewyddu&#8217;r dudalen.</p>
<p>&nbsp;</p>
<p>Pan ydych wedi agor tudalen &#8216;Manylion&#8217; siwrnai, byddwn yn argymell eich bod yn clicio ar y ddolen &#8216;Dangos mewn tabl&#8217;.&nbsp; Mae hon yn arddangos y manylion mewn fformat testun yn unig yn hytrach nag fel lluniau a thestun.</p>
<p>&nbsp;</p>
<p>Ceir nifer o amrywiadau ar gynllunwyr siwrneion, fel &#8216;Cymharu siwrneion o ddrws-i-ddrws&#8217;, &#8216;Canfyddwch ehediad&#8217; a &#8216;Canfyddwch fws moethus&#8217;.&nbsp; Maent i gyd yn gweithio yn debyg, ac eithrio ar gyfer y &#8216;Cynllunydd teithiau dydd&#8217; lle gallwch ddewis opsiynau ar gyfer pob un o dri cham eich siwrnai.&nbsp; Dangosir y rhain mewn tri thabl, un ar gyfer pob un o gamau&#8217;r siwrnai.&nbsp; Dylech ddewis opsiwn o bob un o&#8217;r tri thabl drwy ddewis y botwm sy&#8217;n ymddangos o flaen pob opsiwn.&nbsp; Mae pob opsiwn siwrnai yn dangos rhestr o fathau o gludiant sy&#8217;n ymwneud &#226;&#8217;r opsiwn hwnnw.&nbsp; Er enghraifft gallai siwrnai sy&#8217;n ei gwneud hi&#8217;n ofynnol i chi gymryd tr&#234;n, yna bws a cherdded, gael ei disgrifio fel &#8216;Tr&#234;n a Bws a Cherdded&#8217;.</p>
<p>&nbsp;</p>
<p>Ar &#244;l y tri thabl hwn ceir rhestr dewislen a ollyngir i lawr sy&#8217;n caniat&#225;u i chi ddewis cael manylion pellach am y cyfuniad o ddewisiadau o siwrneion yr ydych wedi eu dethol.&nbsp; Argymhellwn eich bod yn dewis y manylion fel tabl, a chlicio ar &#8216;Iawn&#8217;.</p>
<p>&nbsp;</p>
<p><strong>Dulliau o wneud y cyfan yn fwy darllenadwy</strong></p>
<p>Efallai y dymunwch wneud y tudalennau yn fwy darllenadwy drwy newid setiadau eich porwr.</p>
<p>&nbsp;</p>
<p>Er enghraifft: </p>
<ul class="lister">
<li>cynyddu maint ffont y porwr </li>
<li>diffodd delweddau </li>
<li>newid y cefndir a lliwiau&#8217;r testun</li></ul>
<p></p>
<p>&nbsp;</p>
<p><strong>Hygyrchedd cludiant</strong></p>
<p>Bydd teithwyr sydd ag anableddau neu broblemau gyda symud yn gallu cael gwybodaeth am hygyrchedd gwahanol fathau o gludiant wrth gynllunio eu siwrneion. Mae gan Transport Direct ddolennau ar dudalennau cynllunio siwrneion yn ogystal ag ar y tudalennau canlyniadau siwrneion.</p>
<p>&nbsp;</p>
<p>Bydd y dolennau isod yn agor gwefannau eraill i ddangos gwybodaeth ynghylch hygyrchedd cludiant:</p><br />
<p><u><font color="#0000ff">
<a href="http://www.nationalrail.co.uk/passenger_services/disabled_passengers/" target="_blank" >Mapiau o orsafoedd rheilffordd gyda mynediad i bobl gyda phroblemau symud <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a><br />
<a href="http://www.direct.gov.uk/en/DisabledPeople/MotoringAndTransport/index.htm" target="_blank" >Drws-i-ddrws: Canllaw teithio i bobl anabl <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a><br />
<a href="http://www.direct.gov.uk/en/Dl1/Directories/DG_6000229" target="_blank" >Yn ac o amgylch Llundain <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a><br />
<a href="http://www.direct.gov.uk/en/TravelAndTransport/Publictransport/index.htm" target="_blank" >Cyrff a Mudiadau Defnyddiol <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a><br /></font></u></p>
<p>&nbsp;</p>
<p>&nbsp;</p></div></div>
<div></div>
<div></div>
<div></div>'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10907
SET @ScriptDesc = 'Updates to Accessibility content'


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

