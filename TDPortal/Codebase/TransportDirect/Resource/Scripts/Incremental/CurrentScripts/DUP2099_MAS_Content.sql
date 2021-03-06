-- ***********************************************
-- NAME 		: DUP2099_MAS_Content.sql
-- DESCRIPTION 	: MAS content update for infographic
-- AUTHOR		: David Lane
-- DATE			: 04 March 2013
-- ************************************************

USE [Content]
GO
------------------------------------------------

-- Infographic page title
EXEC AddtblContent 1, 1, 'langStrings', 'Infographic.AppendPageTitle',
'Money Advice Service | ',
'Money Advice Service | '

-- Infographic page content
EXEC AddtblContent 1, 1, 'langStrings', 'Infographic.PageContent',
'<p> View the <a href="Infographic.aspx?accessible=true"><i>text alternative for the commuter money saving infographic</i></a></p><p><a class="publish2_link" target="_blank" href="https://www.moneyadviceservice.org.uk?utm_source=publish2&utm_medium=referral&utm_campaign=www.gov.uk"><img src="https://www.moneyadviceservice.org.uk/assets/logo_en.png" alt="Money Advice Service" border="0" /></a></p><p><img alt="Cut down on travel costs by investigating alternative routes into to work" class="infoimage" src="https://www.moneyadviceservice.org.uk/images/travelcosts.jpg" /></p><p>All information accurate at time of publication</p><p>This article is provided by the <a class="publish2_link" target="_blank" href="https://www.moneyadviceservice.org.uk/?utm_source=publish2&utm_medium=referral&utm_campaign=www.gov.uk">Money Advice Service</a>.</p><img id="publish2_tracking" src="http://www.publish2.com/static/tracking?distributor_id=4668&account_type=google_analytics&account_id=UA-4205932-1&format=json&partner_name=Department_of_Transport&story_id=8985544&delivery=api&story_title=Transport+Infographic&partner_feed_id=2130&distributor_feed_id=2259&content_type=story&usage=web_full&path=%2Finfographic%2Ftransportinfographic%3Fpublish2_story_id%3D8985544%26publish2_byline%3DNone%26publish2_categories%3Dtransportinfo%26publish2_partner_name%3DDepartment_of_Transport%26publish2_partner_id%3D4985&export_id=None&partner_id=4985&nativead_feed_id=None" width="1" height="1" />',
'<p> View the <a href="Infographic.aspx?accessible=true"><i>text alternative for the commuter money saving infographic</i></a></p><p><a class="publish2_link" target="_blank" href="https://www.moneyadviceservice.org.uk?utm_source=publish2&utm_medium=referral&utm_campaign=www.gov.uk"><img src="https://www.moneyadviceservice.org.uk/assets/logo_en.png" alt="Money Advice Service" border="0" /></a></p><p><img alt="Cut down on travel costs by investigating alternative routes into to work" class="infoimage" src="https://www.moneyadviceservice.org.uk/images/travelcosts.jpg" /></p><p>All information accurate at time of publication</p><p>This article is provided by the <a class="publish2_link" target="_blank" href="https://www.moneyadviceservice.org.uk/?utm_source=publish2&utm_medium=referral&utm_campaign=www.gov.uk">Money Advice Service</a>.</p><img id="publish2_tracking" src="http://www.publish2.com/static/tracking?distributor_id=4668&account_type=google_analytics&account_id=UA-4205932-1&format=json&partner_name=Department_of_Transport&story_id=8985544&delivery=api&story_title=Transport+Infographic&partner_feed_id=2130&distributor_feed_id=2259&content_type=story&usage=web_full&path=%2Finfographic%2Ftransportinfographic%3Fpublish2_story_id%3D8985544%26publish2_byline%3DNone%26publish2_categories%3Dtransportinfo%26publish2_partner_name%3DDepartment_of_Transport%26publish2_partner_id%3D4985&export_id=None&partner_id=4985&nativead_feed_id=None" width="1" height="1" />'

-- Infographic page content
EXEC AddtblContent 1, 1, 'langStrings', 'Infographic.AccessiblePageContent',
'<h2>Could you save money and give yourself a pay rise by changing how you commute?</h2>
<p>How long is your commute? Here are some money saving suggestions.</p>
<h3>Is your commute less than 2 kilometres?</h3>
<p>Save by walking ...</p>
<ul><li>It’s free, no petrol, parking, or bus fare</li>
<li>A 60 kilogram person can burn 100 calories in 30 minutes [Department of Health]</li>
<li>It saves 133.1 grammes per km CO2 emissions by not driving [Society of Motor Manufacturers and Traders]</li></ul>
<h3>Is your commute between 2 and 5 kilometres?</h3>
<p>Save by cycling ...</p>
<ul><li>Save up to 40% off a new bike with the Government cycle to work scheme [Cycle to Work Alliance]</li>
<li>Save at least £368 a year by cancelling your gym membership [BBC News]</li>
<li>An 80 kilogram person can burn up to 650 calories per hour when cycling [NHS]</li></ul>
<h3>Is your commute between 5 and 10 kilometres?</h3>
<p>Save on your bus journey ...</p>
<ul>
<li>Buy an annual season ticket and save approximately £348 a year [National Express WM]</li>
<li>Cycle and save an average £412, including the cost of a new bike [National Express WM]</li>
<li>Get an electric bike and pay just 12 pence each time you charge it [A to B magazine] </li></ul>
<h3>Is your commute over 10 kilometres?</h3>
<h4>If you use your car, make sure you drive economically ...</h4>
<ul>
<li>Save £300 a year just by driving more economically [Energy Saving Trust]</li>
<li>Save up to £2,217 a year – for a 20 mile each way commute – by carsharing [The AA]</li>
<li>"Bike and ride" – save up to £1,200 a year in carpark costs by cycling to the station [London Midland carpark prices]</li></ul>
<h4>If you commute by train, you could halve your train cost ...</h4>
<ul>
<li>Instead of a weekly ticket, get a season ticket loan from work and save approximately 20% [TFL]</li>
<li>Take the bus instead of the train for some or all of your journey and save up to £2,224 a year [The Guardian]</li>
<li>If you work part time or sometimes from home, getting carnet tickets can save up to 20% [My Train Tickets]</li></ul>',
'<h2>Could you save money and give yourself a pay rise by changing how you commute?</h2>
<p>How long is your commute? Here are some money saving suggestions.</p>
<h3>Is your commute less than 2 kilometres?</h3>
<p>Save by walking ...</p>
<ul><li>It’s free, no petrol, parking, or bus fare</li>
<li>A 60 kilogram person can burn 100 calories in 30 minutes [Department of Health]</li>
<li>It saves 133.1 grammes per km CO2 emissions by not driving [Society of Motor Manufacturers and Traders]</li></ul>
<h3>Is your commute between 2 and 5 kilometres?</h3>
<p>Save by cycling ...</p>
<ul><li>Save up to 40% off a new bike with the Government cycle to work scheme [Cycle to Work Alliance]</li>
<li>Save at least £368 a year by cancelling your gym membership [BBC News]</li>
<li>An 80 kilogram person can burn up to 650 calories per hour when cycling [NHS]</li></ul>
<h3>Is your commute between 5 and 10 kilometres?</h3>
<p>Save on your bus journey ...</p>
<ul>
<li>Buy an annual season ticket and save approximately £348 a year [National Express WM]</li>
<li>Cycle and save an average £412, including the cost of a new bike [National Express WM]</li>
<li>Get an electric bike and pay just 12 pence each time you charge it [A to B magazine] </li></ul>
<h3>Is your commute over 10 kilometres?</h3>
<h4>If you use your car, make sure you drive economically ...</h4>
<ul>
<li>Save £300 a year just by driving more economically [Energy Saving Trust]</li>
<li>Save up to £2,217 a year – for a 20 mile each way commute – by carsharing [The AA]</li>
<li>"Bike and ride" – save up to £1,200 a year in carpark costs by cycling to the station [London Midland carpark prices]</li></ul>
<h4>If you commute by train, you could halve your train cost ...</h4>
<ul>
<li>Instead of a weekly ticket, get a season ticket loan from work and save approximately 20% [TFL]</li>
<li>Take the bus instead of the train for some or all of your journey and save up to £2,224 a year [The Guardian]</li>
<li>If you work part time or sometimes from home, getting carnet tickets can save up to 20% [My Train Tickets]</li></ul>'

GO

-- =============================================
-- Update change catalogue
-- =============================================
EXEC [PermanentPortal].[dbo].[UpdateChangeCatalogue] 2099, 'Infographic page content'

GO