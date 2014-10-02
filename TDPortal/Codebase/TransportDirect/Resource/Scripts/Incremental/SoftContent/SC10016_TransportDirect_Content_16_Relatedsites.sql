-- *************************************************************************************
-- NAME 		: SC10016_TransportDirect_Content_16_Relatedsites.sql
-- DESCRIPTION  	: Updates to Related Sites content
-- AUTHOR		: xxxxxxxxxxxxx
-- *************************************************************************************

USE [Content]
GO

-------------------------------------------------------------
-- Related sites
-------------------------------------------------------------

EXEC AddtblContent
1, 20, 'TitleText', '/Channels/TransportDirect/About/RelatedSites',
'<table>
	<tr>
        <td><img src="/Web2/App_Themes/TransportDirect/images/gifs/softcontent/relatedlinkslargeicon.gif" alt=" " width="70px" height="36px" /></td>
        <td><h1>Related sites</h1></td>
	</tr>
</table>'
,
'<table>
	<tr>
		<td><img src="/Web2/App_Themes/TransportDirect/images/gifs/softcontent/relatedlinkslargeicon.gif" alt=" " width="70px" height="36px" /></td>
		<td><h1>Safleoedd cysylltiedig</h1></td>
	</tr>
</table>'


EXEC AddtblContent
1, 20, 'Body Text', '/Channels/TransportDirect/About/RelatedSites', 
'<div id="primcontent">
	<div id="contentarea">
		<table id="rsviewa" summary="Related sites list" cellspacing="0">
			<tbody>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="NationalTransport" name="NationalTransport"></a>
							<h2>National transport</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">National Rail</td>
					<td class="rscoltwoa"><a href="http://www.nationalrail.co.uk/" target="_blank">www.nationalrail.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Guide to UK national rail services, including timetables 
						and real time travel information.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">National Express</td>
					<td class="rscoltwoa"><a href="http://www.nationalexpress.com/" target="_blank">www.nationalexpress.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Homepage of the&nbsp;UK''s largest scheduled coach company. 
						Timetable and fare information also available.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Scottish Citylink</td>
					<td class="rscoltwoa"><a href="http://www.citylink.co.uk/" target="_blank">www.citylink.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Scotland''s leading provider of express coach services.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Translink Online - Northern Ireland</td>
					<td class="rscoltwoa"><a href="http://www.translink.co.uk/" target="_blank">www.translink.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">The main public transport provider for Northern Ireland.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Sustrans</td>
					<td class="rscoltwoa"><a href="http://www.sustrans.org.uk/" target="_blank">www.sustrans.org.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Homepage of Sustrans, the sustainable transport charity, 
						working on practical projects to encourage people to walk, cycle and use public 
						transport. Contains information on the National Cycle Network.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">National Trails</td>
					<td class="rscoltwoa"><a href="http://www.nationaltrail.co.uk/" target="_blank">www.nationaltrail.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Provides details of over 2500 miles (4000km) of the 
						nation&#8217;s favourite walking, riding and cycling trails.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">BAA</td>
					<td class="rscoltwoa"><a href="http://www.baa.co.uk/" target="_blank">www.baa.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">UK flight arrivals, flight timetables, and booking service.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">PLUSBUS</td>
					<td class="rscoltwoa"><a href="http://www.plusbus.info/" target="_blank">www.plusbus.info <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Integrated train and bus ticketing is now available across 
						Britain, view this site for details.</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div align="right"><a name="LocalPublicTransport"></a><a class="jpt" href="/Web2/About/RelatedSites.aspx#logoTop">Top 
								of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="LocalPublicTransport"></a>
							<h2>Local public transport</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">traveline</td>
					<td class="rscoltwoa"><a href="http://traveline.info/" target="_blank">traveline.info <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Covers all travel by air, rail, coach, bus, ferry, metro 
						and tram within the UK. Searchable index to timetables, fares, maps, ticket 
						types, and passenger facilities.</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div class="jpt" align="right"><a name="Motoring"></a><a href="/Web2/About/RelatedSites.aspx#logoTop">Top 
								of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="Motoring"></a>
							<h2>Motoring</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Traffic England</td>
					<td class="rscoltwoa"><a href="http://www.trafficengland.com/" target="_blank">www.trafficengland.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Traffic England is a service, provided on behalf of the 
						Highways Agency, displaying real-time traffic information for England''s 
						motorways and A-roads.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Traffic Wales</td>
					<td class="rscoltwoa"><a href="http://www.traffic-wales.com/" target="_blank">www.traffic-wales.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Maintains travel information for the motorways and trunk 
						roads of Wales, providing network status, traffic maps, and live traffic 
						information.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Traffic Scotland</td>
					<td class="rscoltwoa"><a href="http://www.trafficscotland.org/" target="_blank">www.trafficscotland.org <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Travel information for Scotland''s strategic road network.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">AA</td>
					<td class="rscoltwoa"><a href="http://www.theaa.com/" target="_blank">www.theaa.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Motoring organisation providing breakdown cover, car 
						insurance, road travel information and other travel and motoring products and 
						services.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">RAC</td>
					<td class="rscoltwoa"><a href="http://www.rac.co.uk/" target="_blank">www.rac.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Motoring organisation providing breakdown cover, car 
						insurance, road travel information and other travel and motoring products and 
						services.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">TfL Congestion Charging</td>
					<td class="rscoltwoa"><a href="http://www.tfl.gov.uk/roadusers/congestioncharging/" target="_blank">www.tfl.gov.uk/roadusers/congestioncharging <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Information about the London Congestion Charging scheme. 
						Allows on-line payment.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Park And Ride</td>
					<td class="rscoltwoa"><a href="/Web2/JourneyPlanning/ParkAndRide.aspx">www.transportdirect.info  
							</a></td>
					<td class="rscolthreea">Information about Park and Ride schemes around Britain.
					</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div class="jpt" align="right"><a name="MotoringCosts"></a><a href="/Web2/About/RelatedSites.aspx#logoTop">Top 
								of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="MotoringCosts"></a>
							<h2>Motoring costs</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">AA car costs</td>
					<td class="rscoltwoa"><a href="http://www.theaa.com/motoring_advice/running_costs/index.html" target="_blank">www.theaa.com/motoring_advice/<br />
							running_costs <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></td>
					<td class="rscolthreea">Information about car running costs for petrol and diesel 
						cars.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">RAC car costs</td>
					<td class="rscoltwoa"><a href="http://www.emmerson-hill.co.uk/documents/RAC_Illustrative_Motoring_costs_April_2013.pdf" target="_blank">
							www.emmerson-hill.co.uk/documents/RAC_Illustrative_Motoring_costs_April_2013.pdf <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /></a></td>
					<td class="rscolthreea">Information about car running costs for petrol and diesel 
						cars.</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div class="jpt" align="right"><a name="CarSharing"></a><a href="/Web2/About/RelatedSites.aspx#logoTop">Top 
								of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="CarSharing"></a>
							<h2>Car Sharing</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Liftshare</td>
					<td class="rscoltwoa"><a href="http://www.liftshare.com/" target="_blank">www.liftshare.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Comprehensive national car sharing database. Free 
						registration.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">National CarShare</td>
					<td class="rscoltwoa"><a href="http://www.nationalcarshare.co.uk/" target="_blank">www.nationalcarshare.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">National car sharing database. Free registration.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">ShareAcar</td>
					<td class="rscoltwoa"><a href="http://www.shareacar.com/" target="_blank">www.shareacar.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">National car sharing database. Annual fee.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Freewheelers</td>
					<td class="rscoltwoa"><a href="http://www.freewheelers.com/" target="_blank">www.freewheelers.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">National and international journey database. Free 
						registration.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Go Car Share</td>
					<td class="rscoltwoa"><a href="http://www.goCarShare.com/" target="_blank">www.goCarShare.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Car sharing via Facebook.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Carpooling</td>
					<td class="rscoltwoa"><a href="http://www.carpooling.co.uk/" target="_blank">www.carpooling.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">National and international car sharing database. Free registration.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Carplus</td>
					<td class="rscoltwoa"><a href="http://www.carplus.org.uk/" target="_blank">www.carplus.org.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Supporting the development of affordable, accessible and low-carbon car-sharing clubs and ride-sharing services.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Bla Bla Car</td>
					<td class="rscoltwoa"><a href="http://www.blablacar.com/" target="_blank">www.blablacar.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">European car sharing site.</td>
				</tr>

				<tr>
					<td align="right" colspan="3">
						<div class="jpt" align="right"><a name="Government"></a><a href="/Web2/About/RelatedSites.aspx#logoTop">Top 
								of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="Government"></a>
							<h2>Government</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Department for Transport</td>
					<td class="rscoltwoa"><a href="http://www.dft.gov.uk/" target="_blank">www.dft.gov.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Homepage of the Government Department responsible for 
						Transport.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">       
					<td class="rscolonea">GOV.UK</td>       
					<td class="rscoltwoa"><a href="https://www.gov.uk" target="_blank">www.gov.uk    <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" />
					          </a></td>
					<td class="rscolthreea">Interactive portal to latest and widest range of public
					    service information including links to other UK Government sites.</td>      
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Scottish Government</td>
					<td class="rscoltwoa"><a href="http://www.scotland.gov.uk/Home/" target="_blank">www.scotland.gov.uk/Home <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Scottish politics, reports, consultations and government 
						office publications from the devolved government.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Welsh Government</td>
					<td class="rscoltwoa"><a href="http://www.wales.gov.uk/" target="_blank">www.wales.gov.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Homepage of the Welsh Government.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Northern Ireland Executive</td>
					<td class="rscoltwoa"><a href="http://www.northernireland.gov.uk/" target="_blank">www.northernireland.gov.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Homepage of the Northern Ireland Executive.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Transport Scotland</td>
					<td class="rscoltwoa"><a href="http://www.transportscotland.gov.uk/" target="_blank">www.transportscotland.gov.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Transport Scotland is the new national transport agency for 
						Scotland.&nbsp;Its purpose is to help deliver the vision of the Scottish 
						Government for transport, with a focus on the national rail and road networks.
					</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div align="right"><a class="jpt" href="/Web2/About/RelatedSites.aspx#logoTop">Top of 
								page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="TouristInfo"></a>
							<h2>Tourist Information</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Visit Britain</td>
					<td class="rscoltwoa"><a href="http://www.visitbritain.com/" target="_blank">www.visitbritain.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">The official site of the British Tourist Authority. Whether 
						you need inspiration for your trip or practical travel advice.</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div class="jpt" align="right"><a name="Motoring"></a><a href="/Web2/About/RelatedSites.aspx#logoTop">Top 
								of page <img alt="Go to top of page" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
			</tbody>
		</table>
	</div>
</div>'
,
'<div id="primcontent">
	<div id="contentarea">
		<table id="rsview" cellspacing="0">
			<tbody>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="Top" name="Top"></a>
							<h2>Cludiant cenedlaethol</h2>
						</div>
					</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">National Rail</td>
					<td class="rscoltwoa"><a href="http://www.nationalrail.co.uk/" target="_blank">www.nationalrail.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Canllaw i wasanaethau rheilffyrdd cenedlaethol y DG, gan 
						gynnwys amserlenni a gwybodaeth am deithio mewn amser real.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">National Express</td>
					<td class="rscoltwoa"><a href="http://www.nationalexpress.com/" target="_blank">www.nationalexpress.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Tudalen gartref cwmni bysiau moethus rheolaidd mwyaf y 
						DG.&nbsp; Gellir cael gwybodaeth am amserlenni a phris tocynnau hefyd.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Scottish Citylink</td>
					<td class="rscoltwoa"><a href="http://www.citylink.co.uk/" target="_blank">www.citylink.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Prif ddarparwr yr Alban o wasanaethau bysiau moethus 
						cyflym.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Translink Online - Northern Ireland</td>
					<td class="rscoltwoa"><a href="http://www.translink.co.uk/" target="_blank">www.translink.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Y prif ddarparwr cludiant cyhoeddus ar gyfer Gogledd 
						Iwerddon.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Sustrans</td>
					<td class="rscoltwoa"><a href="http://www.sustrans.org.uk/" target="_blank">www.sustrans.org.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Tudalen gartref Sustrans, yr elusen cludiant cynaladwy, 
						sy&#8217;n gweithio ar brosiectau ymarferol i annog pobl i gerdded, seiclo a 
						defnyddio cludiant cyhoeddus.&nbsp; Mae hefyd yn cynnwys gwybodaeth am y 
						Rhwydwaith Seiclo Genedlaethol.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">National Trails</td>
					<td class="rscoltwoa"><a href="http://www.nationaltrail.co.uk/" target="_blank">www.nationaltrail.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Mae&#8217;n darparu manylion am dros 2500 milltir (4000km) 
						o hoff lwybrau cerdded, merlota a seiclo&#8217;r genedl..</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">BAA</td>
					<td class="rscoltwoa"><a href="http://www.baa.co.uk/" target="_blank">www.baa.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Awyrennau sy&#8217;n hedfan i mewn i&#8217;r DG, amserlenni 
						hedfan a gwasanaeth archebu.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">PLUSBUS</td>
					<td class="rscoltwoa"><a href="http://www.plusbus.info/" target="_blank">www.plusbus.info <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">cy Integrated train and bus ticketing is now available 
						across Britain, view this site for details.</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div align="right"><a name="LocalPublicTransport"></a><a class="jpt" href="/Web2/About/RelatedSites.aspx#logoTop">Yn 
								&#244;l i&#8217;r brig <img alt="Yn &#269;l i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="LocalPublicTransport"></a>
							<h2>Cludiant cyhoeddus lleol</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Traveline</td>
					<td class="rscoltwoa"><a href="http://traveline.info/" target="_blank">traveline.info <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Mae&#8217;n ymdrin &#226; phob math o deithio drwy&#8217;r 
						awyr, ar y rheilffyrdd, bysiau moethus, bysiau, fferi, metro a thramiau o fewn 
						y DG.&nbsp; Mynegai y gellir ei chwilio o amserlenni, prisiau tocynnau, mapiau, 
						mathau o docynnau a chyfleusterau i deithwyr.</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div align="right"><a name="Motoring"></a><a class="jpt" href="/Web2/About/RelatedSites.aspx#logoTop">Yn 
								&#244;l i&#8217;r brig <img alt="Yn &#269;l i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="Motoring"></a>
							<h2>Moduro</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Traffic England</td>
					<td class="rscoltwoa"><a href="http://www.trafficengland.com" target="_blank">www.trafficengland.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Mae Traffic England yn wasanaeth a ddarperir ar ran 
						Asiantaeth y Priffyddd, sy&#8217;n arddangos gwybodaeth am draffig mewn amser 
						real ar gyfer traffyrdd a ffyrdd A Lloegr.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Traffic Wales</td>
					<td class="rscoltwoa"><a href="http://www.traffic-wales.com/" target="_blank">www.traffic-wales.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Mae&#8217;n cynnal gwybodaeth am deithio ar gyfer y 
						traffyrdd a&#8217;r cefnffyrdd yng Nghymru gan ddarparu gwybodaeth am statws y 
						rhwydwaith, mapiau trafnidiaeth a gwybodaeth fyw am drafnidiaeth.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Traffic Scotland</td>
					<td class="rscoltwoa"><a href="http://www.trafficscotland.org/" target="_blank">www.trafficscotland.org <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Gwybodaeth am deithio ar gyfer rhwydwaith ffyrdd strategol 
						yr Alban.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">AA</td>
					<td class="rscoltwoa"><a href="http://www.theaa.com/" target="_blank">www.theaa.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Corff morduro sy&#8217;n darparu gwarchodaeth rhag torri i 
						lawr, yswiriant ceir, gwybodaeth am deithio ar y ffyrdd a chynhyrchion a 
						gwasanaethau eraill yn ymwneud &#226; theithio a moduro.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">RAC</td>
					<td class="rscoltwoa"><a href="http://www.rac.co.uk/" target="_blank">www.rac.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Corff morduro sy&#8217;n darparu gwarchodaeth rhag torri i 
						lawr, yswiriant ceir, gwybodaeth am deithio ar y ffyrdd a chynhyrchion a 
						gwasanaethau eraill yn ymwneud &#226; theithio a moduro.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">TfL Congestion Charging</td>
					<td class="rscoltwoa"><a href="http://www.tfl.gov.uk/roadusers/congestioncharging/" target="_blank">www.tfl.gov.uk/roadusers/congestioncharging <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Gwybodaeth am gynllun Codi T&#226;l Gorlawnder 
						Llundain.&nbsp; Caniateir talu ar-lein.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Parcio a Theithio</td>
					<td class="rscoltwoa"><a href="/Web2/JourneyPlanning/ParkAndRide.aspx">www.transportdirect.info  
							</a></td>
					<td class="rscolthreea">Gwybodaeth am gynlluniau Parcio a Theithio o amgylch 
						Prydain.
					</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div align="right"><a name="MotoringCosts"></a><a class="jpt" href="/Web2/About/RelatedSites.aspx#logoTop">Yn 
								&#244;l i&#8217;r brig <img alt="Yn &#269;l i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="MotoringCosts"></a>
							<h2>Costau moduro</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">AA car costs</td>
					<td class="rscoltwoa"><a href="http://www.theaa.com/motoring_advice/running_costs/index.html" target="_blank">www.theaa.com/motoring_advice/<br />
							running_costs <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></td>
					<td class="rscolthreea">Gwybodaeth am gostau rhedeg car ar gyfer ceir petrol a 
						disel.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">RAC car costs</td>
					<td class="rscoltwoa"><a href="http://www.emmerson-hill.co.uk/documents/RAC_Illustrative_Motoring_costs_April_2013.pdf">
						www.emmerson-hill.co.uk/documents/RAC_Illustrative_Motoring_costs_April_2013.pdf <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /></a></td>
					<td class="rscolthreea">Gwybodaeth am gostau rhedeg car ar gyfer ceir petrol a 
						disel.</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div class="jpt" align="right"><a name="CarSharing"></a><a href="/Web2/About/RelatedSites.aspx#logoTop">Yn 
								&#244;l i&#8217;r brig <img alt="Yn &#269;l i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="CarSharing"></a>
							<h2>Rhannu ceir</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Liftshare</td>
					<td class="rscoltwoa"><a href="http://www.liftshare.com/" target="_blank">www.liftshare.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Cronfa ddata rhannu ceir cenedlaethol cynhwysfawr. 
						Cofrestru am ddim.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">National CarShare</td>
					<td class="rscoltwoa"><a href="http://www.nationalcarshare.co.uk/" target="_blank">www.nationalcarshare.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Cronfa ddata rhannu ceir cenedlaethol. Cofrestru am ddim.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">ShareAcar</td>
					<td class="rscoltwoa"><a href="http://www.shareacar.com/" target="_blank">www.shareacar.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Cronfa ddata rhannu ceir cenedlaethol. Ffi blynyddol.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Freewheelers</td>
					<td class="rscoltwoa"><a href="http://www.freewheelers.com/" target="_blank">www.freewheelers.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Cronfa ddata siwrneion cenedlaethol a rhyngwladol. 
						Cofrestru am ddim.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Go Car Share</td>
					<td class="rscoltwoa"><a href="http://www.goCarShare.com/" target="_blank">www.goCarShare.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Car sharing via Facebook.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Carpooling</td>
					<td class="rscoltwoa"><a href="http://www.carpooling.co.uk/" target="_blank">www.carpooling.co.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">National and international car sharing database. Free registration.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Carplus</td>
					<td class="rscoltwoa"><a href="http://www.carplus.org.uk/" target="_blank">www.carplus.org.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">Supporting the development of affordable, accessible and low-carbon car-sharing clubs and ride-sharing services.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Bla Bla Car</td>
					<td class="rscoltwoa"><a href="http://www.blablacar.com/" target="_blank">www.blablacar.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">European car sharing site.</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div class="jpt" align="right"><a name="Government"></a><a href="/Web2/About/RelatedSites.aspx#logoTop">Yn 
								&#244;l i&#8217;r brig <img alt="Yn &#269;l i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="Government"></a>
							<h2>Llywodraeth</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Department for Transport</td>
					<td class="rscoltwoa"><a href="http://www.dft.gov.uk/" target="_blank">www.dft.gov.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Tudalen gartref Adran y Llywodraeth sy&#8217;n gyfrifol am 
						Gludiant.&nbsp;&nbsp;&nbsp;
					</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">       
					<td class="rscolonea">GOV.UK</td>       
					<td class="rscoltwoa"><a href="https://www.gov.uk" target="_blank">www.gov.uk    <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" />
					          </a></td>
					<td class="rscolthreea">Porth rhyngweithiol i&#8217;r ystod diweddaraf ac ehangaf o 
						wybodaeth y gwasanaeth cyhoeddus gan gynnwys dolennau i safleoedd eraill 
						Llywodraeth y DU.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Scottish Government</td>
					<td class="rscoltwoa"><a href="http://www.scotland.gov.uk/Home/" target="_blank">www.scotland.gov.uk/Home <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Gwleidyddiaeth, adroddiadau, ymgynghoriadau a chyhoeddiadau 
						Swyddfa Llywodraeth yr Alban oddi wrth y llywodraeth ddatganoledig.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Welsh Government</td>
					<td class="rscoltwoa"><a href="http://www.wales.gov.uk/" target="_blank">www.wales.gov.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Tudalen gartref Cynulliad Cenedlaethol Cymru.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Northern Ireland Executive</td>
					<td class="rscoltwoa"><a href="http://www.northernireland.gov.uk/" target="_blank">www.northernireland.gov.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Tudalen gartref Gweithrediaeth Gogledd Iwerddon.</td>
				</tr>
				<tr>
					<td colspan="3">&nbsp;</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Transport Scotland</td>
					<td class="rscoltwoa"><a href="http://www.transportscotland.gov.uk/" target="_blank">www.transportscotland.gov.uk <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(yn agor ffenestr newydd)" /> 
							</a></td>
					<td class="rscolthreea">Transport Scotland yw&#8217;r asiantaeth cludiant 
						cenedlaethol newydd ar gyfer yr Alban.&nbsp;Ei bwrpas yw helpu i gyflwyno 
						gweledigaeth Gweithrediaeth yr Alban ar gyfer cludiant, gan ganolbwyntio ar y 
						rhwydweithiau rheilffyrdd a ffyrdd cenedlaethol.
					</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div align="right"><a class="jpt" href="/Web2/About/RelatedSites.aspx#logoTop">Yn 
								&#244;l i&#8217;r brig <img alt="Yn &#269;l i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
				<tr>
					<td colspan="3">
						<div class="hdtypethree"><a id="TouristInfo"></a>
							<h2>cy- Tourist Information</h2>
						</div>
					</td>
				</tr>
				<tr class="rsviewcontentrow">
					<td class="rscolonea">Visit Britain</td>
					<td class="rscoltwoa"><a href="http://www.visitbritain.com/" target="_blank">www.visitbritain.com <img src="/Web2/App_Themes/TransportDirect/images/gifs/newwindow.gif" alt="(opens in new window)" /> 
							</a></td>
					<td class="rscolthreea">cy - The official site of the British Tourist Authority. Whether 
						you need inspiration for your trip or practical travel advice.</td>
				</tr>
				<tr>
					<td align="right" colspan="3">
						<div align="right"><a name="MotoringCosts"></a><a class="jpt" href="/Web2/About/RelatedSites.aspx#logoTop">Yn 
								&#244;l i&#8217;r brig <img alt="Yn &#269;l i&#8217;r brig" src="/Web2/App_Themes/TransportDirect/images/gifs/SoftContent/uarrow_icon_slim.gif"
									border="0" /></a></div>
					</td>
				</tr>
			</tbody>
		</table>
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

SET @ScriptNumber = 10016

SET @ScriptDesc = 'Updates to Related sites'


DECLARE @VersionInfo VARCHAR(24)
SET @VersionInfo = '$Revision:   1.21  $'

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

