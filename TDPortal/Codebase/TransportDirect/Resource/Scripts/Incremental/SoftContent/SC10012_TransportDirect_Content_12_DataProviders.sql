-- ***********************************************
-- NAME 		: SC10012_TransportDirect_Content_12_DataProviders.sql
-- DESCRIPTION 	: Script to add Data Providers content
-- AUTHOR		: xxxx
-- DATE			: 20 May 2008 15:00:00
-- ************************************************


USE [Content]
GO

DECLARE @GroupId int
SET @GroupId = (SELECT GroupId FROM tblGroup WHERE [Name] = 'staticwithoutprint')

EXEC AddtblContent
1, @GroupId, 'TitleText', '/Channels/TransportDirect/About/DataProviders',
'<div>
    <h1>
        Data Providers
    </h1>
</div>'
,
'<div>
    <h1>
        Darparwyr data
    </h1>
</div>'


EXEC AddtblContent
1, @GroupId, 'Body Text', '/Channels/TransportDirect/About/DataProviders'
,'
<div id="primcontent">
<div id="contentarea">
<div id="hdtypethree">
<h2>Data providers</h2></div>
<p>The following organisations supply Transport Direct with information:</p>
<p>&nbsp;</p>
<p><img alt="Traveline Logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/Traveline.gif" border="0" /></p>
<p>&nbsp;</p>
<p><strong></strong></p>
<p><strong></strong></p>
<h3>traveline</h3>
<p>traveline provides us with all our local transport information for buses, tram, light rail and ferries. It is a partnership of Passenger Transport Executives (PTEs), local authorities, bus operators and others. It has been formed by fostering partnerships in each region of the country, and these partnerships have made arrangements to run traveline in their particular areas. traveline collects and organises all of the timetable information, provides systems to find answers regarding travel and arranges for travel query phone calls to be answered. </p>
<p>&nbsp;</p>
<p>The 11 traveline regions are:</p>
<ul id="lister">
<li>East Anglia </li>
<li>East Midlands </li>
<li>London </li>
<li>North&nbsp;East </li>
<li>North&nbsp;West </li>
<li>Scotland </li>
<li>South East </li>
<li>South West </li>
<li>Wales </li>
<li>West Midlands&nbsp; </li>
<li>Yorkshire</li></ul>
<p>&nbsp;</p>
<h3>Local Authorities</h3>
<p>To provide door-to-door public transport journeys, we rely on information about the name and location of each of the 300,000 bus stops throughout Britain from all 141 local authorities. They also provide this information to traveline. In addition to bus stop timetable information, we currently have live bus departure information from certain local authorities which is delivered from systems supplied by ACIS and INIT.</p><br />
<h3>Transport for London</h3>
<p>Transport for London (TfL) provides us with the tube and London bus network maps. It is the integrated body responsible for the capital''s transport system. Its role is to implement the Mayor''s Transport Strategy for London and manage the transport services across the capital for which the Mayor has responsibility. </p>
<p><br /><strong></strong></p>
<p><strong><img alt="Point X logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/PointX.jpg" border="0" /></strong></p>
<h3>Point X</h3>
<p>PointX provide location information about a large range of landscape features &#8211; including attractions and facilities - which we can include on our maps and which you can use as the start and end locations when planning your journeys. PointX&#8217;s national Points of Interest database provides essential information for businesses delivering location-based services as well as central government, local authorities, emergency services and the commercial sector.&nbsp;&nbsp; </p>
<div style="FONT-SIZE: 0.9em; FONT-FAMILY: Verdana, Arial, helvetica, Sans-Serif"><br />
<p>PointX © includes data licensed from the following third parties: PointX © Database Right/Copyright, Thomson Directories Limited © Copyright, Market Location © Database Right/Copyright and Ordnance Survey © Crown Copyright and/or Database Right. All rights reserved. Licence 100034829.</p></div>
<p>&nbsp;</p>
<p><br /><img alt="TheTrainline.com logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/TheTrainLine.gif" border="0" /></p>
<p><strong></strong></p>
<h3>thetrainline.com</h3>
<p>TheTrainline is the leading supplier of UK train travel products, providing fast and easy access to timetables, fares, reservations and tickets through its Internet site and contact centre operations in the UK train travel sector.<br />Trainline.com Ltd, Trainline Rail Enquiry Service Ltd, Trainline Short Breaks Ltd, and Qjump Ltd are wholly owned subsidiaries of Trainline Holdings Ltd, which is owned by travel groups, Virgin Group (80.5%), National Express Group plc (14%) and Stagecoach Holdings plc (5.5%).<br /></p><br />
<p><img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/eastcoast_lg.gif" alt="East Coast" border="0" /><br /></p>
<p></p>
<h3>East Coast</h3>
<p>East Coast is one of our retail partners. They are a provider of rail travel along the East Coast Main Line serving some of the UK''s major commercial centres from London to Scotland. Through their interactive website, www.eastcoast.co.uk, you can check timetables, view live train running information, buy rail tickets for anywhere in Great Britain and sign up to receive all the latest East Coast news by email.<br /><br /></p>
<p><br /><strong><img alt="Traintaxi logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/TrainTaxi.gif" border="0" /></strong></p>
<h3>traintaxi</h3>
<p>traintaxi provides us with information about taxi and private hire firms serving all the train, tram, metro and underground stations in Britain. </p>
<div style="FONT-SIZE: 0.9em; FONT-FAMILY: Verdana, Arial, helvetica, Sans-Serif"><br />
<p>&#169; Traintaxi Limited, 2003. The compilation of taxi and private hire data used within the Transport Direct portal is the copyright of Traintaxi Limited and is also protected by database right. The Traintaxi name and the Traintaxi logo are trademarks of Traintaxi Limited. Both the data and the trademarks are used by Transport Direct under licence.</p></div>
<p><br /><strong></strong></p>
<p><strong></strong></p>
<p><br /><strong><img alt="Ordance Survey logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/ordnance.jpg" border="0" /></strong></p>
<h3>Ordnance Survey</h3>
<p>Ordnance Survey provides both digital data and traditional mapping to underpin our applications to find locations, plan journeys and display for journeys. Their products and services link people and businesses to Britain''s diverse landscape. They offer a wide range of products and services from traditional walking maps and road maps to the large-scale maps and digital products that now make up the largest part of their business.</p>
<p>&nbsp;</p>
<p><br /><strong></strong></p>
<p><strong><img alt="Association of Train Operating Companies logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/ATOCresized.gif" border="0" />&nbsp;&nbsp;&nbsp;&nbsp; <img alt="National Rail &#13;&#10;&#13;&#10;Enquires logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/NRESresized.gif" border="0" />&nbsp; </strong></p>
<h3>Rail Settlement Plan (RSP)/ Association of Train Operating Companies (ATOC)/ National Rail Enquiries&nbsp;</h3>
<p>ATOC is an incorporated association owned by its members. It was set up by the Train Operating Companies formed during the privatisation of the railways under the Railways Act 1993. ATOC&#8217;s website is the official site of the passenger rail industry.</p>
<p>&nbsp;</p>
<p>National Rail Enquiries is an ATOC company, wholly owned by train operators, providing timetable, fare and real-time train movement information for passengers. National Rail Enquiries provides us with live train departure information for all mainland National Rail stations and current service information as part of our live travel news offering.</p>
<p>&nbsp;</p>
<p>Rail Settlement Plan (RSP) is an ATOC company, wholly owned by train operators, providing distribution and settlement services in support of ticketing and information services for the National Rail network.</p>
<p>&nbsp;</p>
<p>RSP provides us with information (updated daily) about timetables and fares for the whole Network.<br /></p>
<p><br /><strong></strong></p>
<p><strong></strong></p>
<p><strong><img alt="National Express logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/NationalExpress.gif" border="0" /></strong></p>
<h3>National Express</h3>
<p>National Express is one of our retail partners. They run Britain''s largest scheduled coach network, carrying more than 16 million passengers each year to around 1200 destinations throughout England, Scotland and Wales. They are Britain''s largest operator of scheduled coaches to airports, and the UK''s largest operator of scheduled services to Europe.</p>
<p><br /><strong></strong></p>
<p><strong><img alt="Scottish Citylink logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/citylink.gif" border="0" /></strong></p>
<h3>Scottish Citylink</h3>
<p>Scottish Citylink&nbsp;is one of our retail partners. It is Scotland&#8217;s leading provider of express coach services. Each year over 3 million passengers travel with&nbsp;Scottish Citylink&nbsp;to more than 200 destinations in a fleet of blue and yellow coaches.</p>
<p>&nbsp;</p>
<p><img alt="OAG logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/OAG_Corporate.gif" border="0" /></p>
<p><strong></strong></p>
<h3>OAG Worldwide</h3>
<p>OAG Worldwide provides us with our domestic air schedules.&nbsp; A renowned global content management company, OAG specialises in the collection and distribution of travel and transport information. OAG''s database holds flights from the world''s scheduled airlines, making it a leading source of global flight information.&nbsp;&nbsp; </p>
<div style="FONT-SIZE: 0.9em; FONT-FAMILY: Verdana, Arial, helvetica, Sans-Serif"><br />
<p>The database rights and copyright in the air travel information provided by Transport Direct belong to OAG Worldwide Limited.</p>
<p>&nbsp;</p>
<p>OAG Worldwide Limited has used its best efforts in collecting and preparing material for inclusion in Transport Direct, but cannot warrant that the information available on Transport Direct is complete or accurate and does not assume and hereby disclaims, liability to any person for any loss or damage caused by errors or omissions in the OAG data whether such errors or omissions result from negligence, accident or any other cause.</p>
<p>&nbsp;</p>
<p><img alt="Transport Scotland Logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/TSlogo.jpg" border="0" /></p>
<p></p></div>
<p><strong></strong></p>
<h3>Transport Scotland and the Scottish Roads Traffic Database</h3>
<p>Transport Scotland and the Scottish Roads Traffic Database provide historic road traffic information for Scotland which enables us to provide intelligent road journey planning that takes expected traffic levels into account at different times of the day. Both systems have been established by the Scottish Executive through its Traffic Controller Unit. Transport Scotland aims to ensure that best use is made of the existing Scottish trunk road network and to improve the safety and efficiency of that network through the provision of up to the minute traffic information. The Scottish Roads Traffic Database holds all historical Scottish trunk road traffic data and some local roads data. </p>
<p><br /><strong><img alt="Traffic Wales logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/TrafficWales4colour.gif" border="0" /></strong></p>
<h3>Traffic-Wales</h3>
<p>Traffic-Wales is the traffic information centre in Wales operated by the Welsh Assembly Government. It provides road traffic information for routes across Wales and enables us to provide intelligent road journey planning taking into account expected traffic levels for different times of the day, times of year and during events. Traffic Wales information is collated from two Welsh Assembly Government traffic management centres, which provide network management capability for the core networks in North and South Wales.</p>
<p><br /><strong></strong></p>
<p><strong></strong></p>
<table>
<tbody>
<tr>
<td colspan="2"><img alt="Highways Agency logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/highwaysagency.gif" border="0" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
<tr>
<td>&nbsp;<img alt="Trafficmaster logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/trafficmaster.gif" border="0" /></td>
<td>&nbsp;&nbsp;&nbsp;&nbsp;<img alt="ITIS Holdings logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/itisholdings.gif" border="0" /></td></tr></tbody></table>
<h3>Highways Agency</h3>
<p>The Highways Agency provides historic road traffic information for motorways and trunk roads in England which enables us to provide intelligent road journey planning that takes into account expected journey times at different times of the day. The Highways Agency is responsible for managing and maintaining the strategic road network which is the single largest Government asset comprising 9,380km / 5,863 miles of motorways and trunk roads. The Agency takes data from different&nbsp;sources including the private companies of Trafficmaster and ITIS. </p>
<p>&nbsp;</p>
<p>Trafficmaster has a network of cameras installed on the trunk road network which provides real time journey speed information to road users and historical information to the Agency.</p>
<p>&nbsp;</p>
<p>ITIS obtain journey speed information from vehicles fitted with GPS tracking systems on the UK road network which they use to provide information to road users.&nbsp; They provide historical journey speed data to the Agency for its core road network.&nbsp;&nbsp;&nbsp; </p>
<p>&nbsp;</p>
<p><img alt="Landmark logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/Landmark.jpg" border="0" /></p>
<p><strong></strong></p>
<h3>Landmark</h3>
<p>Landmark provides us with detailed and comprehensive information on car parks within the UK.<br /><br /></p>
<p>Landmark is Britain''s leading supplier of quality land and property search information. Providing digital mapping, planning and environmental risk information, Landmark also delivers comprehensive geographic solutions to a wide range of government and private sector clients.</p>
<p>&nbsp;</p>
<p><img alt="Trafficlink logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/Trafficlink.JPG" border="0" /></p>
<h3>Trafficlink</h3>
<p>Trafficlink is the UK&#8217;s leading provider of real-time traffic and travel information to the Government, BBC, UK independent radio, Teletext, the AA, ITV, and the UK telematics industry.</p><br />
<p>Operating from seven regional offices across the UK, Trafficlink aggregates congestion and incident information through a wide variety of information sources including Police and Traffic Control Rooms, numerous CCTV cameras, local authorities, satellite speed sensors, motorists and motorway variable message signs. These extensive sources, monitored by the UK&#8217;s largest team of traffic and travel analysts, means that Trafficlink&#8217;s information not only details the cause of incidents, but their effect and likely duration.</p>
<p>&nbsp;</p>
<h3>We would also like to acknowledge the following organisations which all provide us with information</h3>
<p>The Automobile Association<br />ACIS<br />Aldwark Bridge Limited<br />Argyll &amp; Bute Council<br />BAA<br />Batheaston Toll Bridge<br />Bournemouth to Swanage<br />Brighton &amp; Hove Borough Council<br />Bristol City Council<br />C Toms &amp; Sons<br />Caledonian MacBrayne Limited<br />Cardiff City Council<br />Cartford Bridge<br />Castle Drive<br />Clifton Suspension Bridge Trust<br />College Road Toll<br />Conwy Borough Council<br />Cromarty Ferry Company<br />Cumbria County Council<br />Derby City Council<br />Dunham Bridge Toll<br />Eling Toll Bridge<br />Erskine Toll Bridge<br />Forth Estuary Transport Authority<br />Frithsden Road Toll<br />Gloucestershire County Council<br />Great Orme Toll (Happy Valley)<br />Gwynedd County Council<br />Hamsterley Forest Drive<br />Harlyn Road Toll<br />Highland Regional Council<br />Humber Bridge<br />Itchen Toll Bridge<br />Kewstoke Toll Gate<br />Kimmeridge Bay Toll<br />King Harry Ferry<br />Kingsland Toll Bridge Shrewsbury<br />Lancashire County Council<br />Le Crossing Company Limited<br />Leicester City Council<br />Llanddwyn Beach Toll<br />London Borough of Greenwich<br />Mersey Tunnels<br />Middlesbrough Transporter Bridge<br />Midland Expressway Limited<br />North Yorkshire Moors National Park<br />Northern Ireland Executive<br />Northlink Ferries Limited<br />Nottingham City Council<br />Orkney Ferries Limited<br />Penmaenpool Toll Bridge<br />Pentland Ferries Limited<br />Petrol Retailers Association<br />Philips Limited Ferry<br />Porlock Hill Toll<br />RAC<br />Red Funnel Ferries Limited<br />Reedham Ferry<br />Ringshall Drive Toll<br />River Cleddau Toll Bridge<br />Rixton &amp; Warburton Toll Bridge<br />Saddler Street Durham<br />Severn Bridge Toll<br />Shetland Islands Council<br />Skye Ferry<br />South Hams District Council<br />Stagecoach Holdings plc<br />Surrey County Council<br />Sustrans<br />Swinford Toll Bridge<br />Tamar Bridge &amp; Torpoint Ferry<br />Tay Road Bridge<br />Three Lochs Forest Drive<br />Tyne &amp; Wear PTE<br />Western Ferries<br />Whitchurch Toll Bridge<br />Whitney on Wye Toll Bridge<br />Wightlink<br />Worthy Toll Road<br /></p>
<p>&nbsp;</p>
<div></div>
</div></div>
<div></div>
'

,'
<div id="primcontent">
<div id="contentarea">
<div id="hdtypethree">
<h2>Darparwyr data</h2></div>
<p>Mae''r cyrff canlynol yn rhoi gwybodaeth i Transport Direct:</p>
<p>&nbsp;</p>
<p><img alt="Traveline Logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/Traveline.gif" border="0" /></p>
<p>&nbsp;</p>
<p><strong></strong></p>
<p><strong></strong></p>
<h3>traveline</h3>
<p>Mae traveline yn rhoi ein holl wybodaeth am gludiant lleol i ni ar gyfer bysiau, tramiau, rheilffyrdd ysgafn a ffer&#239;au.&nbsp; Mae''n bartneriaeth o Weithrediaethau Cludiant Teithwyr, (PTE), awdurdodau lleol, gweithredwyr bysiau ac eraill.&nbsp; Fe''i ffurfiwyd drwy feithrin partneriaethau ym mhob rhanbarth o''r wlad, ac mae''r partneriaethau hyn wedi gwneud trefniadau i redeg traveline&nbsp; yn eu hardaloedd arbennig.&nbsp; Mae traveline yn casglu ac yn trefnu''r holl wybodaeth am amserlenni, yn darparu systemau i''w gwneud yn bosibl i gynllunio teithiau ar gludiant cyhoeddus ac yn trefnu i alwadau ff&#244;n a oedd yn ymwneud ag ymholiadau am deithio gael eu hateb.</p>
<p>&nbsp;</p>
<p>11 rhanbarth traveline yw:</p>
<ul id="lister">
<li>East Anglia </li>
<li>Dwyrain Canolbarth Lloegr </li>
<li>Llundain </li>
<li>Gogledd Ddwyrain Lloegr </li>
<li>Gogledd Orllewin Lloegr </li>
<li>Yr Alban </li>
<li>De Ddwyrain Lloegr </li>
<li>De Orllewin Lloegr </li>
<li>Cymru </li>
<li>Gorllewin Canolbarth Lloegr </li>
<li>Swydd Efrog</li></ul>
<p><br />&nbsp;</p>
<h3>Awdurdodau Lleol</h3>
<p>I ddarparu siwrneion cludiant cyhoeddus o ddrws i ddrws, dibynnwn ar wybodaeth am enw a lleoliad pob un o''r 300,000 o arosfannau bysiau drwy Brydain oddi wrth bob un o''r 141 o awdurdodau lleol.&nbsp; Maent yn darparu''r wybodaeth hon hefyd i traveline.&nbsp; Yn ychwanegol at wybodaeth am amserlenni arosfannau bysiau, mae gennym ar hyn o bryd wybodaeth fyw am ymadawiadau bysiau oddi wrth rai awdurdodau lleol a gyflwynir o systemau a gyflenwir gan ACIS ac INIT.</p>
<p>&nbsp;</p>
<h3>Transport for London</h3>
<p>Mae Cwmni Transport for London (TfL) yn darparu mapiau am rwydwaith y bysiau a''r tiwb yn Llundain.&nbsp; Dyma''r corff integredig sy''n gyfrifol am system cludiant y brifddinas.&nbsp; Ei r&#244;l yw gweithredu strategaeth cludiant y Maer ar gyfer Llundain a rheoli''r gwasanaethau cludiant ar draws y brifddinas y mae''r Maer yn gyfrifol amdanynt. </p>
<p><br /><strong></strong></p>
<p><strong><img alt="Logo Point X" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/PointX.jpg" border="0" /></strong></p>
<h3>Point X</h3>
<p>Mae PointX yn darparu gwybodaeth am leoliad amrediad mawr o nodweddion tirlun - gan gynnwys atyniadau a chyfleusterau - y gallwn eu cynnwys ar ein mapiau ac y gallwch eu defnyddio fel lleoliadau dechrau a diwedd wrth gynllunio eich teithiau.&nbsp; Mae databas Mannau o Ddiddordeb cenedlaethol PointX yn darparu gwybodaeth hanfodol ar gyfer busnesau sy''n cyflwyno gwasanaethau sy''n seiliedig ar leoliad yn ogystal &#226; llywodraeth ganolog, awdurdodau lleol, gwasanaethau argyfwng a''r sector masnachol.&nbsp;</p>
<p>&nbsp;</p>
<div style="FONT-SIZE: 0.9em; FONT-FAMILY: Verdana, Arial, helvetica, Sans-Serif">
<p>PointX (c) yn cynnwys data a drwyddedwyd oddi wrth y cyrff trydydd parti canlynol:&nbsp; PointX (c) Hawl Databas/Hawlfraint 2003; Arolwg Ordnans (c) Hawlfraint y Goron a/neu Hawl Databas 2003.&nbsp; Cedwir pob hawl.&nbsp; Rhif trwydded 100034829; Cynhyrchwyd yn gyfan gwbl neu yn rhannol o dan drwydded oddi wrth, ac fe''i seiliwyd yn gyfan gwbl neu yn rhannol ar ddeunydd hawlfraint Thomson Directories Limted.</p></div>
<p>&nbsp;</p>
<p><br /><strong></strong>&nbsp;<img alt="Logo TheTrainline.com" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/TheTrainLine.gif" border="0" /></p>
<p><strong></strong></p>
<h3>thetrainline.com</h3>
<p>TheTrainline yw&#8217;r prif gyflenwr o gynhyrchion teithio ar drenau&#8217;r DU, gan ddarparu mynediad cyflym a rhwydd i amserlenni, tocynnau, archebu ymlaen llaw a thocynnau drwy ei wefan a gweithrediadau canolfannau cyswllt yn sector teithio ar y trenau y DU.<br />Mae Trainline.com Ltd, Trainline Rail Enquiry Service Ltd, Trainline Short Breaks Ltd, a Qjump Ltd i gyd yn is-gwmn&#239;au sy&#8217;n eiddo llwyr i Trainline Holdings Ltd, sy&#8217;n eiddo i grwpiau teithio Virgin Group (80.5%), National Express Group plc (14%) a Stagecoach Holdings plc (5.5%).</p><br />
<p><br /><img src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/eastcoast_lg.gif" alt="East Coast" border="0" /></p>
<p></p>
<h3>East Coast</h3>
<p>Un o&#8217;n partneriaid manwerthu yw East Coast. Mae&#8217;n
darparu teithiau tr&#234;n ar Brif Reilffordd Arfordir y Dwyrain ac
yn gwasanaethu rhai o brif ganolfannau masnachol y DU, o Lundain
i&#8217;r Alban. Drwy eu gwefan ryngweithiol, www.eastcoast.co.uk, gellir gweld amserlenni, a chael gwybodaeth
gyfredol yngl&#375;n ag amserau&#8217;r trenau, prynu tocynnau tr&#234;n
i fynd i unrhyw le ym Mhrydain a chofrestru i dderbyn yr holl
newyddion diweddaraf am East Coast drwy e-bost.<br /></p><br /><br />
<p><br /><strong><img alt="Logo Traintaxi" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/TrainTaxi.gif" border="0" /></strong></p>
<h3>traintaxi</h3>
<p>Mae Traintaxi Limited yn rhoi gwybodaeth i ni am gwmn&#239;au tacsi a hurio preifat sy''n gwasanaethu''r holl orsafoedd trenau, tramiau, trenau metro a thanddaearol ym Mhrydain. </p>
<div style="FONT-SIZE: 0.9em; FONT-FAMILY: Verdana, Arial, helvetica, Sans-Serif"><br />
<p>&#169; Traintaxi Limited, 2003. Mae casglu data am hurio tacsis a hurio preifat a ddefnyddir o fewn porth Transport Direct yn hawlfraint i Traintaxi Limited ac fe''i gwarchodir hefyd gan hawl databas.&nbsp; Mae enw Traintaxi a logo Traintaxi yn nodau masnach Traintaxi Limited.&nbsp; Defnyddir y data a''r nodau masnach gan Transport Direct o dan drwydded.</p></div>
<p><br /><strong></strong></p>
<p><strong></strong></p>
<p><br /><strong><img alt="Logo Arolwg Ordnans" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/ordnance.jpg" border="0" /></strong></p>
<h3>Arolwg Ordnans</h3>
<p>Mae Arolwg Ordnans yn rhoi gwybodaeth am fapio gyda data digidol a mapio traddodiadol i fod yn sail ar gyfer cymwysiadau i ddarganfod lleoliadau, cynllunio teithiau ac arddangosfeydd ar gyfer teithiau.&nbsp; Mae eu cynhyrchion a''u gwasanaethau yn cysylltu pobl a busnesau a thirlun amrywiol Prydain.&nbsp; Cynigiant amrediad eang o gynhyrchion a gwasanaethau o fapiau cerdded traddodiadol a mapiau ffyrdd i''r mapiau ar raddfa fawr a''r cynhyrchion digidol sydd bellach yn ffurfio''r rhan fwyaf o''u busnes.</p>
<p><br /><strong></strong></p>
<p><strong><img alt="Logo Cymdeithas Cwmn&#239;au Gweithredu Trenau" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/ATOCresized.gif" border="0" />&nbsp;&nbsp;&nbsp;&nbsp; <img alt="Logo &#13;&#10;&#13;&#10;Ymholiadau &#13;&#10;Rheilffordd &#13;&#10;&#13;&#10;Cenedlaethol" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/NRESresized.gif" border="0" />&nbsp; </strong><strong></strong></p>
<p><strong></strong>&nbsp;</p>
<h3>Cynllun Setliad Rheilffyrdd (RSP)/Cymdeithas Cwmn&#239;au Gweithredu Trenau (ATOC)/Ymholiadau Rheilffyrdd Cenedlaethol&nbsp;</h3>
<p>Mae ATOC yn gymdeithas gorfforedig sy''n eiddo i''w haelodau.&nbsp; Fe''i sefydlwyd gan y Cwmn&#239;au Gweithredu Trenau a ffurfiwyd yn ystod preifateiddio''r rheilffyrdd o dan Ddeddf Rheilffyrdd 1993.&nbsp; Gwefan ATOC yw safle swyddogol y diwydiant rheilffyrdd i deithwyr.</p>
<p>&nbsp;</p>
<p>Mae Ymholiadau Rheilffyrdd Cenedlaethol yn gwmni ATOC, sy''n eiddo''n gyfan gwbl i weithredwyr trenau, ac mae''n darparu gwybodaeth am amserlenni, prisiau tocynnau a threnau mewn amser real i deithwyr.&nbsp; Mae Ymholiadau Rheilffyrdd Cenedlaethol yn rhoi gwybodaeth i ni am amser ymadael trenau ar gyfer yr holl orsafoedd Rheilffyrdd Cenedlaethol ym Mhrydain a gwybodaeth am wasanaethau cyfredol fel rhan o''r hyn a gynigir gennym ynglyn &#226; newyddion teithio byw.</p>
<p>&nbsp;</p>
<p>Mae Rail Settlement Plan (RSP) yn gwmni ATOC, sy''n eiddo yn gyfan gwbl i weithredwyr trenau, sy''n darparu gwasanaethau dosbarthu a setlo i gefnogi gwasanaethau gwerthu tocynnau a gwybodaeth ar gyfer rhwydwaith Rheilffyrdd Cenedlaethol.</p>
<p>&nbsp;</p>
<p>Mae RSP yn rhoi gwybodaeth i ni (sy''n cael ei diweddaru yn ddyddiol) am amserlenni a phrisiau tocynnau ar gyfer y Rhwydwaith gyfan.<br /></p>
<p><br /><strong></strong></p>
<p><strong></strong></p>
<p><strong><img alt="Logo National Express" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/NationalExpress.gif" border="0" /></strong></p>
<h3>National Express</h3>
<p>Mae National Express yn un o''n partneriaid adwerthu.&nbsp; Maent yn gyfrifol am redeg rhwydwaith bysiau moethus rheolaidd mwyaf Prydain sy''n cludo mwy nag 16 miliwn o deithwyr bob blwyddyn i oddeutu 1200 o gyrchfannau drwy Gymru, Lloegr a''r Alban.&nbsp; Dyma brif weithredwr bysiau moethus rheolaidd Prydain i feysydd awyr a gweithredwr mwyaf y DG o wasanaethau rheolaidd i Ewrop.</p>
<p><br /><strong></strong></p>
<p><strong><img alt="Logo Scottish Citylink" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/citylink.gif" border="0" /></strong></p>
<h3>Scottish Citylink</h3>
<p>Scottish Citylink yw un o''n partneriaid adwerthu. Dyma brif ddarparwr gwasanaethau bysiau cyflym yn yr Alban.&nbsp; Bob blwyddyn mae dros 3 miliwn o deithwyr yn teithio gyda Scottish Citylink i fwy na 200 o gyrchfannau mewn fflyd o fysiau glas a melyn.</p>
<p>&nbsp;</p>
<p><img alt="OAG logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/OAG_Corporate.gif" border="0" /></p>
<p>&nbsp;</p>
<p><strong></strong></p>
<h3>OAG Worldwide</h3>
<p>OAG Worldwide sy''n rhoi amserlenni awyr cartref i ni. Fel cwmni enwog rheoli cynnwys byd-eang, mae OAG yn arbenigo mewn casglu a dosbarthu gwybodaeth teithio a thrafnidiaeth. Mae cronfa ddata OAG yn cadw ehediadau o gwmn&#239;au awyr rhestredig y byd, sy''n ei wneud yn ffynhonnell wybodaeth ehediadau byd-eang.&nbsp;&nbsp; </p>
<div style="FONT-SIZE: 0.9em; FONT-FAMILY: Verdana, Arial, helvetica, Sans-Serif"><br />
<p>Mae hawliau a hawlfraint cronfa ddata yn yr wybodaeth teithio awyr a ddarparir gan Transport Direct yn berchen i OAG Worldwide Limited.</p>
<p>&nbsp;</p>
<p>Defnyddiodd OAG Worldwide Limited eu hymdrechion gorau wrth gasglu a pharatoi deunydd ar gyfer ei gynnwys yn Transport Direct, ond ni allant warantu bod yr wybodaeth sydd ar gael ar Transport Direct yn gyfan gwbl gywir ac nid yw''n honni a thrwy hynny yn gwadu atebolrwydd i unrhyw berson am unrhyw golled neu ddifrod a achoswyd drwy wallau neu esgeulustra yn nata OAC pa un a yw gwallau neu esgeulustra o''r fath yn ganlyniad i esgeulustod, damwain neu unrhyw achos arall.</p>
<p>&nbsp;</p>
<h3><img alt="Transport Scotland Logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/TSlogo.jpg" border="0" /></h3>
<p></p></div>
<p><strong></strong></p>
<h3>Transport Scotland a Database Scottish Roads Traffic</h3>
<p>Mae Transport Scotland a&#8217;r Scottish Roads Traffic Database yn rhoi gwybodaeth hanesyddol am drafnidiaeth ar y ffyrdd ar gyfer yr Alban sy&#8217;n galluogi i ni ddarparu cynlluniau deallus ar gyfer siwrneion ffyrdd sy&#8217;n rhoi ystyriaeth i&#8217;r lefelau trafnidiaeth a ddisgwylir ar wahanol adegau o&#8217;r dydd.&nbsp; Sefydlwyd y ddwy system gan Weithrediaeth yr Alban drwy ei Huned Rheoli Trafnidiaeth.&nbsp; Mae Transport Scotland yn anelu at sicrhau y gwneir y defnydd gorau o rwydwaith bresennol cefnffyrdd yr Alban a gwella ar ddiogelwch ac effeithiolrwydd y rhwydwaith honno drwy ddarparu&#8217;r wybodaeth ddiweddaraf am draffig.&nbsp; Mae&#8217;r Scottish Roads Traffic Database yn cadw&#8217;r holl ddata hanesyddol am drafnidiaeth ar gefnffyrdd yr Alban a rhywfaint o ddata am ffyrdd lleol.</p>
<p><br /><strong><img alt="Logo Traffig Cymru" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/TrafficWales4colour.gif" border="0" /></strong></p>
<h3>Traffic-Wales</h3>
<p>Traffic-Wales yw''r ganolfan gwybodaeth am drafnidiaeth yng Nghymru a weithredir gan Lywodraeth Cynulliad Cymru.&nbsp; Mae''n darparu gwybodaeth am drafnidiaeth ffyrdd ar gyfer llwybrau ar draws Cymru ac yn ein galluogi i ddarparu cynlluniau deallus ar gyfer teithiau ar y ffyrdd gan gymryd lefelau trafnidiaeth ddisgwyliedig i ystyriaeth ar gyfer gwahanol amserau o''r dydd, gwahanol amserau o''r flwyddyn ac yn ystod digwyddiadau.&nbsp; Caiff gwybodaeth Traffic-Wales ei gasglu o ddwy ganolfan rheoli trafnidiaeth Llywodraeth Cynulliad Cymru, sy''n darparu gallu i reoli rhwydwaith ar gyfer y rhwydweithiau craidd yng Ngogledd a De Cymru.</p>
<p><br /><strong></strong></p>
<p><strong></strong></p>
<table>
<tbody>
<tr>
<td colspan="2"><img alt="Logo Awdurdod y Priffyrdd" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/highwaysagency.gif" border="0" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td></tr>
<tr>
<td>&nbsp;<img alt="Logo Trafficmaster" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/trafficmaster.gif" border="0" /></td>
<td>&nbsp;&nbsp;&nbsp;&nbsp;<img alt="Logo ITIS Holdings" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/itisholdings.gif" border="0" /></td></tr></tbody></table>
<h3>Asiantaeth y Priffyrdd</h3>
<p>Mae Asiantaeth y Priffyrdd yn darparu gwybodaeth hanesyddol am drafnidiaeth ffyrdd ar gyfer traffyrdd a chefnffyrdd yn Lloegr sy''n ein galluogi i ddarparu cynlluniau deallus ar gyfer siwrneion ffyrdd sy''n rhoi ystyriaeth i''r amserau y disgwylir i siwrneion eu cymryd ar wahanol adegau o''r dydd.&nbsp; Mae Asiantaeth y Priffyrdd yn gyfrifol am reoli a chynnal rhwydwaith y ffyrdd strategol sef ased unigol fwyaf y Llywodraeth ac sy''n cynnwys 9,380km /5,863 milltir o draffyrdd a chefnffyrdd.&nbsp; Mae''r Asiantaeth yn cymryd data o wahanol ffynonellau gan gynnwys cwmn&#239;au preifat Trafficmaster ac ITIS.&nbsp; </p>
<p>&nbsp;</p>
<p>Mae gan Trafficmaster rwydwaith o gamer&#226;u wedi''u gosod ar rwydwaith y cefnffyrdd sy''n darparu gwybodaeth am gyflymder siwrneion mewn amser real i ddefnyddwyr ffyrdd a gwybodaeth hanesyddol i''r Asiantaeth.</p>
<p>&nbsp;</p>
<p>Mae ITIS yn cael gwybodaeth am gyflymder siwrneion o gerbydau y gosodwyd systemau tracio GPS ynddynt ar rwydwaith ffyrdd y DG y maent yn eu defnyddio i ddarparu gwybodaeth i ddefnyddwyr y ffyrdd.&nbsp; Darparant ddata am gyflymder siwrneion hanesyddol i''r Asiantaeth ar gyfer rhwydwaith eu ffyrdd craidd&nbsp;&nbsp;&nbsp; </p>
<p>&nbsp;</p>
<p><img alt="Landmark logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/Landmark.jpg" border="0" /></p>
<p><strong></strong></p>
<h3>Landmark</h3>
<p>Mae Landmark yn rhoi gwybodaeth fanwl a chynhwysfawr i ni am feysydd parcio yn y DU.<br /><br /></p>
<p>Landmark yw cyflenwr arweiniol Prydain ar gyfer gwybodaeth chwilio tir ac eiddo o ansawdd. Mae''n darparu mapio digidol, gwybodaeth am gynllunio a risg amgylcheddol, a hefyd yn darparu atebion daearyddol cynhwysfawr i ystod eang o gleientiaid llywodraeth a sector preifat.</p>
<p>&nbsp;</p>
<p><img alt="Trafficlink logo" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/Trafficlink.JPG" border="0" /></p>
<h3>Trafficlink</h3>
<p>Trafficlink yw''r prif sefydliad yn y DU sy''n rhoi gwybodaeth amser real am draffig a theithio i''r Llywodraeth, y BBC, radio annibynnol y DU, Teletext, yr AA, ITV, a diwydiant telemateg y DU.</p><br />
<p>Gan weithredu o saith swyddfa ranbarthol ledled y DU, mae Trafficlink yn agregu gwybodaeth am dagfeydd a digwyddiadau drwy ystod eang o ffynonellau gwybodaeth gan gynnwys yr Heddlu ac Ystafelloedd Rheoli Traffig, sawl camera Teledu Cylch Cyfyng, awdurdodau lleol, synwyryddion cyflymder lloeren, modurwyr ac arwyddion negeseuon amrywiol ar draffyrdd. Mae''r ffynonellau helaeth hyn, sy''n cael eu monitro gan d&#238;m mwyaf y DU o ddadansoddwyr traffig a theithio, yn golygu bod gwybodaeth Trafficlink, yn ogystal &#226; disgrifio achos digwyddiadau, yn disgrifio eu heffaith a''u hyd tebygol hefyd.</p><br />
<p>Mae gwybodaeth Trafficlink yn cyrraedd mwy na 40 miliwn o bobl bob wythnos ar draws nifer o wahanol lwyfannau.</p>
<p>&nbsp;</p>
<h3>Hoffem hefyd gydnabod y cyrff canlynol sydd i gyd yn rhoi gwybodaeth i ni.</h3>
<p>The Automobile Association<br />ACIS<br />Aldwark Bridge Limited<br />Argyll &amp; Bute Council<br />BAA<br />Batheaston Toll Bridge<br />Bournemouth to Swanage<br />Brighton &amp; Hove Borough Council<br />Bristol City Council<br />C Toms &amp; Sons<br />Caledonian MacBrayne Limited<br />Cardiff City Council<br />Cartford Bridge<br />Castle Drive<br />Clifton Suspension Bridge Trust<br />College Road Toll<br />Conwy Borough Council<br />Cromarty Ferry Company<br />Cumbria County Council<br />Derby City Council<br />Dunham Bridge Toll<br />Eling Toll Bridge<br />Erskine Toll Bridge<br />Forth Estuary Transport Authority<br />Frithsden Road Toll<br />Gloucestershire County Council<br />Great Orme Toll (Happy Valley)<br />Gwynedd County Council<br />Hamsterley Forest Drive<br />Harlyn Road Toll<br />Highland Regional Council<br />Humber Bridge<br />Itchen Toll Bridge<br />Kewstoke Toll Gate<br />Kimmeridge Bay Toll<br />King Harry Ferry<br />Kingsland Toll Bridge Shrewsbury<br />Lancashire County Council<br />Le Crossing Company Limited<br />Leicester City Council<br />Llanddwyn Beach Toll<br />London Borough of Greenwich<br />Mersey Tunnels<br />Middlesbrough Transporter Bridge<br />Midland Expressway Limited<br />North Yorkshire Moors National Park<br />Northern Ireland Executive<br />Northlink Ferries Limited<br />Nottingham City Council<br />Orkney Ferries Limited<br />Penmaenpool Toll Bridge<br />Pentland Ferries Limited<br />Petrol Retailers Association<br />Philips Limited Ferry<br />Porlock Hill Toll<br />RAC<br />Red Funnel Ferries Limited<br />Reedham Ferry<br />Ringshall Drive Toll<br />River Cleddau Toll Bridge<br />Rixton &amp; Warburton Toll Bridge<br />Saddler Street Durham<br />Severn Bridge Toll<br />Shetland Islands Council<br />Skye Ferry<br />South Hams District Council<br />Stagecoach Holdings plc<br />Surrey County Council<br />Sustrans<br />Swinford Toll Bridge<br />Tamar Bridge &amp; Torpoint Ferry<br />Tay Road Bridge<br />Three Lochs Forest Drive<br />Tyne &amp; Wear PTE<br />Western Ferries<br />Whitchurch Toll Bridge<br />Whitney on Wye Toll Bridge<br />Wightlink<br />Worthy Toll Road<br /></p>
<p>&nbsp;</p>
<p>&nbsp;</p>
<div></div>
</div></div>
<div></div>
'

GO

----------------------------------------------------------------
-- Update change catalogue
----------------------------------------------------------------
USE [PermanentPortal]
GO

DECLARE @ScriptNumber INT
DECLARE @ScriptDesc VARCHAR(200)

SET @ScriptNumber = 10012
SET @ScriptDesc = 'Add Data Providers content'


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
