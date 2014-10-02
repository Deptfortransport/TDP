// *********************************************** 
// NAME                 : JourneyFares.aspx.cs
// AUTHOR               : SchlumbergerSema
// DATE CREATED         : 05/09/2003
// DESCRIPTION			: Journey Fares page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyFares.aspx.cs-arc  $
//
//   Rev 1.19   Jan 23 2013 17:21:44   mmodi
//Hide buy fares button for accessible journeys
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.18   Mar 29 2010 16:40:32   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.17   Mar 26 2010 11:40:08   mmodi
//Populate CJP user flag to allow fares debugging info to be shown
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.16   Mar 22 2010 15:54:58   apatel
//Added null check for adding customised intellitracker parameters
//Resolution for 5473: Error message when click Buy tickets on mix PT/Car return journey
//
//   Rev 1.15   Mar 19 2010 10:50:40   apatel
//Added custom related links for city to city result pages
//Resolution for 5468: Related link in city to city incorrect
//
//   Rev 1.14   Mar 05 2010 10:35:42   apatel
//update for intellitracker customised tag parameters
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.13   Mar 04 2010 15:50:08   apatel
//Added Tracking parameters for Selected Ticket
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.12   Mar 04 2010 14:50:22   apatel
//Updated to add customised intellitracker parameters
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.11   Jan 20 2010 12:13:10   apatel
//Updated to resolve the issue with Ratail hand-off child ticket error
//Resolution for 5380: Retail hand-off child ticket error
//
//   Rev 1.10   Dec 16 2009 09:04:12   apatel
//made cheaper fair link not to display when there are no fares.
//Resolution for 5348: Rail Fares - Fares and Tickets
//
//   Rev 1.9   Nov 15 2009 18:17:44   mmodi
//Updated styles to make results pages look consistent
//
//   Rev 1.8   Oct 21 2009 11:18:28   PScott
//Add social bookmark links to Summary,maps, and fares screen
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.7   Feb 26 2009 10:50:48   apatel
//Revert back changes made to short out the Fares/Session overwrite problem
//Resolution for 5228: Fares/Session Overwrite Problem
//
//   Rev 1.6   Feb 02 2009 17:39:42   mmodi
//Populate Routing Guide properties
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.5   Jan 29 2009 14:06:34   pscott
//IR 5228 Session wrie change to prevent loss of fares info
//Resolution for 5228: Fares/Session Overwrite Problem
//
//   Rev 1.4   Jul 14 2008 14:59:02   mmodi
//Passed in ModeTypes to the pricing Itinerary to ensure correct journey is selected
//Resolution for 5060: City to city: Fares are shown for the wrong journey
//
//   Rev 1.3   May 07 2008 14:50:24   apatel
//made tiltle "Journey Found For" to look in table view when findamode is flight.
//
//   Rev 1.2   Mar 31 2008 13:24:52   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Mar 28 2008 11:33:00 apatel
//  added styles for the park and ride page through code.
//
//  Rev DevFactory Mar 02 2008 15:21:00 apatel
//  setup help button at the top right
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//Updated to pass mode type to FindSummaryResultControl selected on 
//JourneyOverview page in City to City journeys. ModeType value obtained from Session.FindPageState.ModeType
//
//   Rev 1.3   Nov 29 2007 15:10:36   mturner
//Removed redundant CLSCompliant Tag
//
//   Rev 1.2   Nov 29 2007 15:01:02   mturner
//Changes to remove comiler warnings caused by the merge of Del 9.8 into the .Net2 codebase
//
//   Rev 1.2   Nov 29 2007 13:07:54   mturner
//Declared as partial class to make Del 9.8 cde .Net2 compliant
//
//   Rev 1.1   Nov 29 2007 11:08:36   mturner
//Updated for Del 9.8
//
//   Rev 1.125   Oct 16 2007 14:03:58   mmodi
//Amended to generate a fare request ID for the Fare call being made
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.124   Apr 18 2007 14:51:00   dsawe
//added to fix the single fares link issue
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//Resolution for 4388: Del 9.5 LZS Operator link showing on Rail Station description screens
//
//   Rev 1.123   Mar 06 2007 13:43:38   build
//Automatically merged from branch for stream4358
//
//   Rev 1.122.1.0   Mar 02 2007 14:52:54   asinclair
//Added OtherFaresClicked method
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.122   Dec 08 2006 17:16:30   mmodi
//Corrected Summary CO2 control issue when there are no cars
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.121   Dec 07 2006 14:37:28   build
//Automatically merged from branch for stream4240
//
//   Rev 1.120.1.1   Dec 04 2006 14:03:52   dsawe
//added total fuel cost for return journey control when Page.IsPostback
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.120.1.0   Dec 04 2006 13:44:12   dsawe
//setup the fuel cost for calculation of CO2 when Page.ISPostback
//Resolution for 4240: CO2 Emissions
//
//   Rev 1.120   Oct 06 2006 16:47:04   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.119.1.0   Oct 02 2006 16:15:38   mmodi
//Populated roadjourney for CarParkCosts control
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4162: Car Parking: Navigation error when returning from information page
//
//   Rev 1.119   Jun 14 2006 12:04:28   esevern
//Code fix for vantive 3918704 - Enable buttons when no outward journey is returned but a return journey is. Changed code to remove assumption that there is always an outward journey.
//Resolution for 3686: Buttons disabled when return journey returned and outward not
//
//   Rev 1.118   May 18 2006 10:22:04   rphilpott
//Correct location handling for Find Cheaper on City-to-City reverse leg.
//Resolution for 4086: Find Cheaper on City-to-City -- locations reversed.
//
//   Rev 1.117   May 17 2006 15:03:34   rphilpott
//Reverse handling of outward and return locations on Find Cheaper of return area of Two Singles tickets.
//Resolution for 4085: DD075: Illogical handling of inward singles on Find Cheaper
//
//   Rev 1.116   May 04 2006 12:01:54   RPhilpott
//Force manual prerender of AmendFaresControl before setting overrideItineraryType, or prerender will now reset it back to teh default value.
//Resolution for 4071: Del 8.1: Return fares displayed on costs page but Amend fares tab shows single
//
//   Rev 1.115   May 03 2006 15:49:18   RPhilpott
//Find Cheaper on "Two Singles" needs return date (like "Return").
//Resolution for 4067: DD075: Handling of Open Return fares in Find Cheaper
//
//   Rev 1.114   May 03 2006 14:31:22   RPhilpott
//Do not pass return date/time for Open Return Find Cheaper request.
//Resolution for 4067: DD075: Handling of Open Return fares in Find Cheaper
//
//   Rev 1.113   May 03 2006 11:39:20   RPhilpott
//Move PricingRetailOptionsState to partition-specific deferred storage.
//Resolution for 4005: DD075: Discount card entered in Find Cheaper retained if switch back to search by time
//Resolution for 4040: DD075: City-to-city shows return fares if change mode and causes an error
//
//   Rev 1.112   May 02 2006 18:07:20   RPhilpott
//Use correct time-based, not cost-based, value of overrideItineraryType when deciding Find Cheaper ticket type.
//Resolution for 4028: DD075: Single fares displayed if find cheaper after selecting to view open returns
//
//   Rev 1.111   Apr 28 2006 16:58:14   RPhilpott
//1)  Change Find Cheaper link to HyperlinkPostBackControl.
//
//2)  Use return journey details when linking to Find Cheaper from the return fares panel. 
//Resolution for 4037: DD075: Find Cheaper: Cheaper hyperlink should use new style
//Resolution for 4043: DD075: Find cheaper for return journeys involving stations  where exchange and fare grouping are inconsistent
//
//   Rev 1.110   Apr 26 2006 16:53:36   esevern
//Call to SetupCarCosts now done on page postback after call to DetermineStateOfResults so that booleans checked later are setup correctly
//Resolution for 3893: DN058: Park+Ride: Ambiguity yellow crash screen if ' all costs' selected on car results screen
//
//   Rev 1.109   Apr 26 2006 12:15:06   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.108   Apr 05 2006 15:42:56   build
//Automatically merged from branch for stream0030
//
//   Rev 1.107.2.0   Mar 29 2006 11:31:46   RGriffith
//Wait Page Ehancement changes
//Resolution for 33: DEL 8.1 Workstream: Wait Pages
//
//   Rev 1.107   Mar 14 2006 10:27:46   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.106   Feb 23 2006 18:39:28   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.105   Feb 10 2006 10:47:58   jmcallister
//Manual Merge of Homepage 2. IR3180
//
//   Rev 1.104   Feb 01 2006 17:06:16   mguney
//CreatePricingRetailOptions method changed to load discounts from profile only if there is an authenticated user and if there is no discount cards selected from the user interface.
//Resolution for 3534: Del 8: Unable to apply fare discount when user logged in
//
//   Rev 1.103   Dec 13 2005 11:22:22   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.102   Nov 28 2005 14:36:10   NMoorhouse
//Set Cheaper Fares button to look like a hyperlink
//Resolution for 3138: UEE (CG): "Cheaper Fares" link is button, not link
//
//   Rev 1.101   Nov 18 2005 09:10:24   mguney
//Session manager's and itinerary manager's PricingRetailOptions are assigned when needed because some details are lost after extend.
//Resolution for 2991: DN040: SBP Pricing extended journey
//
//   Rev 1.100   Nov 17 2005 16:19:26   jgeorge
//Only recalculate fares if a transition hasn't been requested by other code on the page.
//Resolution for 2996: DN040: spurious Wait Page when selecting "Amend" in SBT
//
//   Rev 1.98   Nov 16 2005 12:59:30   RGriffith
//Resolution for IR3047 - Fix to allow Fuel Costs Drop down button to work
//Resolution for 3047: Del8: Find a Car - Ticket/Cost functionality fails to display All Costs details
//
//   Rev 1.97   Nov 15 2005 08:53:42   jgeorge
//Updated to code to force deferred data to be saved, cleared and reloaded correctly when pricing itineraries.
//Resolution for 2995: DN040: Incorrect retailer and ticket information after using Back button
//Resolution for 2996: DN040: spurious Wait Page when selecting "Amend" in SBT
//Resolution for 3030: DN040: Switching between options on results page invokes wait page
//
//   Rev 1.96   Nov 11 2005 16:02:04   mguney
//The FaresInitialised property check omitted in the  PricedItineraryAvailable method so that the JourneyFaresControl is shown even though no fares are returned. In that case it will display "No fare information available" message.
//Resolution for 2997: DN040: incorrect SBT behaviour when no coach tickets found
//Resolution for 2999: DN040: SBT error handling when wait page times out
//
//   Rev 1.95   Nov 10 2005 10:17:20   jgeorge
//Changed to cast to ExtendItineraryManager when checking if extend is in progress.
//
//   Rev 1.94   Nov 09 2005 15:12:06   mguney
//Merge for stream 2818.
//
//   Rev 1.93   Nov 07 2005 14:36:40   ralonso
//Fixed problem with tdbuttons
//
//   Rev 1.92   Nov 04 2005 12:22:16   rgreenwood
//Manual merge of  2816 HTML Buttons changes into trunk
//
//   Rev 1.91   Nov 02 2005 10:37:20   tmollart
//Modifications for stream of 2638. 
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.90   Sep 29 2005 12:52:48   build
//Automatically merged from branch for stream2673
//
//   Rev 1.89.1.0   Sep 07 2005 13:14:20   rgreenwood
//DN079 UEE ES015 24 Hour Help Button removal
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.89   Aug 26 2005 09:20:08   RWilby
//Fix for IR2691. Changed page to inherit from TDPage instead of TDPrintablePage.
//Resolution for 2691: DN059 - No link to Accessibility information on tickets and costs page
//
//   Rev 1.88   Aug 16 2005 14:38:20   RWilby
//Merge for stream2556
//
//   Rev 1.87   Aug 03 2005 21:48:10   asinclair
//check in for 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Charge for return journeys
//
//   Rev 1.86   Jul 25 2005 11:47:56   jgeorge
//Added additional call to CalculateCarCosts method.
//Resolution for 2616: Car costs displayed incorrectly
//
//   Rev 1.85   Jun 10 2005 16:31:56   rgreenwood
//IR 2544: Created CalculateCarCosts() method to ensure correct initialisation of car costs, and added a page transition that takes users back to the JourneySummary page if they re-select the full itinerary
//
//   Rev 1.84   May 20 2005 11:19:00   tmollart
//Changed the following:
// - Modified code to remove duplicate running of methods to display car costs.
// - Added event handler to detect when user changes journey to refresh the car costs.
// - Changed code that displays the overall total to take into account when we need to decimalise the total.
//Resolution for 2300: Del 7 - Car Costing - Display and rounding of Fuel and Journey costs
//Resolution for 2498: Total Costs Bar does not adapt according to costs displayed
//
//   Rev 1.83   May 12 2005 18:41:38   rgreenwood
//IR 2498: Added some more specific formatting to the DisplayCarCosts() method.
//
//   Rev 1.82   May 10 2005 17:30:58   rgreenwood
//IR 2300: Amended DisplayCarCosts() to handle multiple combinations of rounded/unrounded costs,  and enhanced outward/return cost type dropdown button click events to force calculation of costs at the right time.
//Resolution for 2300: Del 7 - Car Costing - Display and rounding of Fuel and Journey costs
//
//   Rev 1.81   May 09 2005 17:49:38   jgeorge
//Changed to compare Ticket objects using Equals method rather than == operator
//
//   Rev 1.80   May 06 2005 10:37:40   jgeorge
//Check for full itinerary before examining full journey
//Resolution for 2444: Ticket Purchase .NET Error Clicking On Trainline.com Buy Button
//
//   Rev 1.79   May 04 2005 13:50:20   jgeorge
//Added code to ensure that Amend Fares tab is only visible when a PT journey is selected.
//Resolution for 2394: DEL 7 Car Costing - Amend Fares
//
//   Rev 1.78   Apr 27 2005 16:52:32   rgeraghty
//Updated preRender event handler to only re-create the itinerary if the journey selection has changed
//Resolution for 2340: PT - UI issue with Door-to-door and discount cards
//
//   Rev 1.77   Apr 25 2005 17:04:54   jgeorge
//Updates to make car cost controls work correctly with extended journey
//Resolution for 2297: Del 7 - Car Costing - Zero cost for car journey which is part of multi-mode extended journey
//
//   Rev 1.76   Apr 25 2005 12:27:02   esevern
//added check for existance of return journey to UpdateCarCostsDisplay
//
//   Rev 1.75   Apr 25 2005 12:19:18   rgeraghty
//Prevent buy tickets button being displayed if an extension is in progress
//
//   Rev 1.74   Apr 25 2005 11:51:36   jgeorge
//Updated DisplayFindCheaperLink() method to take extend journey into account.
//Resolution for 2299: Del 7 - PT- Find Cheaper not displayed for City to city with extension
//
//   Rev 1.73   Apr 25 2005 10:20:22   esevern
//corrected check for existance of return journey in display of car costs as it was prevent the calculation of running costs
//
//   Rev 1.72   Apr 24 2005 16:58:42   rgeraghty
//FullItinerarySelected check added before calling DisplayCarCosts, also variable tidy up/correction in DisplayFares and DisplayCarCosts
//Resolution for 2295: Del 7 - PT - Server error selecting full extended journey from tickets/costs page
//
//   Rev 1.71   Apr 22 2005 18:09:08   esevern
//amendments to allow changes to car cost display (selection between fuel and all costs) to be reflected in both outward and return car journeys when only one drop list value changed
//Resolution for 2236: Del 7 - Car Costing - Fuel Costs/Total Costs toggles
//
//   Rev 1.70   Apr 18 2005 15:12:40   pcross
//Updated skip links logic to cope with possibility of screen showing mixed outward and return results of car and public transport
//
//   Rev 1.69   Apr 18 2005 14:32:28   rgeraghty
//updated so both control types (fares and car costing) can be displayed simultaneously for door-to-door
//Resolution for 2122: Unable to view tickets/costs for car and public transport at same time
//
//   Rev 1.68   Apr 14 2005 15:47:04   COwczarek
//Set mode to Find A Fare when user invokes find cheaper.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.67   Apr 12 2005 18:41:12   pcross
//Changed the way that the skip links image URL is accessed (now from langStrings, not HTML)
//
//   Rev 1.66   Apr 12 2005 14:38:26   pcross
//Corrected langStrings call to use GetResource for Skip Links work
//
//   Rev 1.65   Apr 12 2005 11:41:04   rgeraghty
//FindCheaperFares logic extended
//Resolution for 2052: PT: Find Cheaper link displays empty FindFareDateSelection page for return journeys
//Resolution for 2082: PT: Find Cheaper link not specifying which ticket type to use for cost based search
//
//   Rev 1.64   Apr 11 2005 13:43:32   COwczarek
//Save Find A mode after invoking cost search for "find cheaper"
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.63   Apr 07 2005 19:53:06   pcross
//Further skip link changes
//
//   Rev 1.62   Apr 07 2005 14:20:12   rgreenwood
//IR 1997: Now hide the TotalCostsControl if only a single car journey is being displayed.
//Resolution for 1997: Del 7 - Display of 'Other costs' in Door-to-door
//
//   Rev 1.61   Apr 07 2005 09:43:52   pcross
//Added skip links
//
//   Rev 1.60   Apr 05 2005 09:46:12   esevern
//FXCop changes for car costing controls
//
//   Rev 1.59   Mar 31 2005 11:52:32   rgeraghty
//Updates made due to Fx Cop changes applied to AmendFaresControl
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.58   Mar 30 2005 09:50:16   esevern
//corrected setting of total value when user switches between fuel and total costs
//
//   Rev 1.57   Mar 29 2005 16:08:20   esevern
//amended car cost journey total to be displayed as pounds and pence
//
//   Rev 1.56   Mar 29 2005 15:54:24   rgeraghty
//Switch added for cost based search and changes made to visibility of pages controls
//Resolution for 1925: DEV Code Review: Journey Fares
//Resolution for 1972: Front End Switch for Find A Fare/City to City Cost Based Search
//
//   Rev 1.55   Mar 15 2005 18:36:32   esevern
//corrected setting of total journey value when outward only
//
//   Rev 1.54   Mar 15 2005 17:41:26   esevern
//display of fares and car costing now mutually exclusive
//
//   Rev 1.53   Mar 14 2005 12:25:36   esevern
//added display of car cost control to pageload
//
//   Rev 1.52   Mar 10 2005 16:51:48   COwczarek
//Method call to invoke cost search runner changed.
//Note some code commented out temporarily to enable build to work.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.51   Mar 10 2005 16:35:08   esevern
//Car Costing - added setting of total journey cost 
//
//   Rev 1.50   Mar 08 2005 14:20:18   COwczarek
//AmendSaveSendControl now uses uppercase naming convention for AmendFaresControl property.
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.49   Mar 04 2005 15:49:48   esevern
//added car costing control
//
//   Rev 1.48   Mar 04 2005 09:25:38   rgeraghty
//Temporarily removed AmendFares references
//
//   Rev 1.47   Mar 03 2005 18:03:56   rgeraghty
//FxCop code changes
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.46   Mar 01 2005 16:29:20   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.45   Mar 01 2005 15:14:40   rgeraghty
//Work in progress
//
//   Rev 1.44   Oct 28 2004 09:46:56   jbroome
//IR1676 Removed now-redundant extra call to displayFares() in PageLoad method. This was added to solved problem of Help button event handlers, which was solved by another IR.
//
//   Rev 1.43   Oct 19 2004 12:31:54   jbroome
//IR 1676 - Ensure correct display of fares when viewing a journey extension for the first time.
//
//   Rev 1.42   Oct 08 2004 16:27:54   passuied
//Added security checks
//Resolution for 1698: Extended Journey: Exception when selectin Fares for Flight segment
//
//   Rev 1.41   Oct 07 2004 12:50:46   jbroome
//IR 1676 - Set ItineraryViewChanged flag when journey option changes in results tables.
//
//   Rev 1.40   Sep 21 2004 17:08:34   passuied
//call display fares in page_load to be able to keep events after postback
//Resolution for 1436: Outward journey help button on fares page perfoms no action
//
//   Rev 1.39   Sep 20 2004 16:45:24   COwczarek
//Page title now displayed using JourneyPlannerOutputTitleControl.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary
//
//   Rev 1.38   Sep 19 2004 15:04:20   jbroome
//IR1391 - visibility of Add Extension to Journey button if incomplete results returned.
//
//   Rev 1.37   Sep 17 2004 12:12:30   jbroome
//IR 1591 - Extend Journey Usability Changes
//
//   Rev 1.36   Aug 31 2004 15:06:42   RHopkins
//IR1440 Rationalised the code for determining the state of the result data.  This improved code has been duplicated across all of the results pages to reduce the burden of future maintenance.
//
//   Rev 1.35   Aug 27 2004 14:30:32   RHopkins
//IR1448 The Outward summary table now has the appropriate class attribute set when in Extended Journey mode.
//
//   Rev 1.34   Aug 23 2004 11:08:40   jgeorge
//IR1319
//
//   Rev 1.33   Aug 10 2004 15:45:02   JHaydock
//Update of display of correct header control for help pages.
//
//   Rev 1.32   Aug 03 2004 16:47:24   RHopkins
//Removed spurious "using" reference.
//
//   Rev 1.31   Jul 23 2004 12:21:02   jgeorge
//Find a... updates
//
//   Rev 1.30   Jul 22 2004 14:15:36   RHopkins
//IR1113 The "Amend date/time" anchor link now varies its text depending upon whether the AmendSaveSend control is displaying "Amend date and time" or "Amend stopover time" or not displaying either.
//
//   Rev 1.29   Jul 01 2004 16:16:00   RHopkins
//Corrected handling of Fares for Extended Journeys
//
//   Rev 1.28   Jun 23 2004 19:20:14   RHopkins
//Partial correction to display of fares for Itinerary, but still no Return fares.
//
//   Rev 1.27   Jun 23 2004 15:54:28   esevern
//added redirect to summary page when extension added
//
//   Rev 1.26   Jun 23 2004 11:44:06   esevern
//corrected display of fares for itinerary segments, corrected return itinerary table display
//
//   Rev 1.25   Jun 17 2004 21:23:42   RHopkins
//Corrected handling of Itinerary journeys
//
//   Rev 1.24   Jun 17 2004 16:44:04   CHosegood
//Modified so that PageLoad completes successfully.
//
//   Rev 1.23   Jun 14 2004 18:25:40   esevern
//added extend journey funcationality
//
//   Rev 1.22   Apr 28 2004 16:20:10   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.21   Apr 02 2004 10:16:28   AWindley
//DEL 5.2 QA Changes: Resolution for 692
//
//   Rev 1.20   Mar 01 2004 13:46:58   asinclair
//Removed Printer hyperlink as not needed in Del 5.2
//
//   Rev 1.19   Nov 27 2003 16:30:08   CHosegood
//Retailers and Fares pages now have identical behaviour for single, matching & non-matching returns.
//Resolution for 307: Return retailers control appears when single journey selected
//
//   Rev 1.18   Nov 21 2003 16:17:02   CHosegood
//Added code doco
//
//   Rev 1.17   Nov 17 2003 15:59:44   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.16   Nov 17 2003 11:48:30   CHosegood
//Now hides discounts if no pricing units are selected
//
//   Rev 1.15   Nov 16 2003 16:31:38   CHosegood
//Now uses OutputNaviagationControl instead of individual buttons for each jp page
//
//   Rev 1.14   Nov 14 2003 17:04:48   COwczarek
//SCR#186 Saved discount preferences not being applied
//
//   Rev 1.13   Nov 14 2003 11:35:06   COwczarek
//Changes to improve (lessen) the frequency with which fares are recalculated when user navigates pages
//
//   Rev 1.12   Nov 11 2003 11:09:40   CHosegood
//No change.
//
//   Rev 1.11   Nov 10 2003 17:16:54   CHosegood
//Logic to determine if fares need to be recalculated is moved into the session object.
//
//   Rev 1.10   Nov 10 2003 11:55:36   CHosegood
//Changed codebehind ns
//
//   Rev 1.9   Nov 07 2003 16:43:02   CHosegood
//Now in templates ns
//
//   Rev 1.8   Nov 06 2003 18:01:48   CHosegood
//Printable version
//
//   Rev 1.7   Nov 05 2003 15:36:24   CHosegood
//If you entered this page from the ticket retailers it would exception.  This is now fixed
//
//   Rev 1.6   Nov 05 2003 12:52:18   CHosegood
//Use loose dtd as told by the development guide
//
//   Rev 1.5   Nov 03 2003 16:29:54   CHosegood
//Intermediate checkin for integration
//
//   Rev 1.4   Oct 31 2003 09:28:56   CHosegood
//Check in for build/integration with TicketRetailers.  What's the worst that can happen!
//
//   Rev 1.3   Oct 28 2003 13:09:00   COwczarek
//Hold processed journeys in separate properties for fares and retail handoff
//
//   Rev 1.2   Oct 27 2003 20:27:14   CHosegood
//Intermediate Version
//
//   Rev 1.1   Oct 17 2003 16:41:32   CHosegood
//Initial Version
//
#region using
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

using TransportDirect.Common.DatabaseInfrastructure;  // Only to use the SQL Format helper
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.TimeBasedPriceRunner;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

using Logger = System.Diagnostics.Trace;

#endregion
namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>s
	/// Page displaying fares for journeys.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class JourneyFares : TDPage
	{   
		#region Instance members
		protected System.Web.UI.HtmlControls.HtmlGenericControl hyperlinkAmendSaveSend;

		protected System.Web.UI.WebControls.Image hyperlinkImageAmendDateTime;
		protected System.Web.UI.WebControls.Label hyperlinkAmendDateTime;
		protected System.Web.UI.WebControls.Label labelFaresOutwardJourney;
		protected System.Web.UI.WebControls.Label labelFaresOutwardDisplayNumber;
		protected System.Web.UI.WebControls.Label labelFaresReturnDisplayNumber;
		protected System.Web.UI.WebControls.Label labelFaresReturnJourney;
		protected TransportDirect.UserPortal.Web.Controls.AmendSaveSendControl AmendSaveSendControl;
		protected TransportDirect.UserPortal.Web.Controls.FindSummaryResultControl findSummaryResultTableControlOutward;
		protected TransportDirect.UserPortal.Web.Controls.FindSummaryResultControl findSummaryResultTableControlReturn;
		protected TransportDirect.UserPortal.Web.Controls.JourneyChangeSearchControl theJourneyChangeSearchControl;
		protected TransportDirect.UserPortal.Web.Controls.OutputNavigationControl theOutputNavigationControl;
		protected TransportDirect.UserPortal.Web.Controls.SummaryResultTableControl summaryResultTableControlOutward;
		protected TransportDirect.UserPortal.Web.Controls.SummaryResultTableControl summaryResultTableControlReturn;
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyFaresControl OutboundJourneyFaresControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyFaresControl ReturnJourneyFaresControl;
		protected TransportDirect.UserPortal.Web.Controls.CarJourneyCostsControl outwardCarJourneyCostsControl;
		protected TransportDirect.UserPortal.Web.Controls.CarJourneyCostsControl returnCarJourneyCostsControl;
		protected TransportDirect.UserPortal.Web.Controls.ResultsFootnotesControl footnotesControl;
		
		private ITDSessionManager sessionManager;
		private TDItineraryManager itineraryManager;
		private PlannerOutputAdapter outputAdapter;

		// State of results
		/// <summary>
		///  True if there is an outward trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool outwardExists;

		/// <summary>
		///  True if there is a return trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool returnExists;

		/// <summary>
		/// True if the Itinerary exists, containing the Initial journey and zero or more extensions
		/// </summary>
		private bool itineraryExists;

		/// <summary>
		/// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
		/// </summary>
		private bool extendInProgress;

		/// <summary>
		/// True if the Itinerary exists and there are no extensions in the process of being planned
		/// </summary>
		private bool showItinerary ;

		/// <summary>
		/// True if the results have been planned using FindA
		/// </summary>
		private bool showFindA;
		private bool returnArriveBefore;		
		private bool outwardArriveBefore;

		/// <summary>
		/// Used to identify when a rail or discount change occurs
		/// </summary>
		private bool discountsChanged;

        /// <summary>
        /// Tracking Control helper class
        /// </summary>
        private TrackingControlHelper trackingHelper;

		#endregion


		#region Constructor
		/// <summary>
		/// Constructor - sets the page Id
		/// </summary>
		public JourneyFares() : base()
		{
			pageId = PageId.JourneyFares;
		}

		#endregion

		#region Private Methods
		/// <summary>
		/// Initialises static page controls
		/// </summary>
		private void InitialiseStaticControls()
		{
			// Initialise static labels, hypertext text and image button Urls 
			// from Resourcing Mangager.

			// Initialise static hyperlink text
			hyperlinkReturnJourneys.Text = GetResource("JourneyPlanner.hyperLinkReturnJourneys.Text");

			hyperlinkOutwardJourneys.Text = GetResource("JourneyPlanner.hyperLinkOutwardJourneys.Text");

			hyperlinkImageReturnJourneys.ImageUrl = GetResource("JourneyPlanner.hyperLinkImageReturnJourneys");
            
			hyperlinkImageOutwardJourneys.ImageUrl = GetResource("JourneyPlanner.hyperLinkImageOutwardJourneys");
           
			buttonRetailers.Text = GetResource("JourneyFares.buttonRetailers.Text");		
		}

		/// <summary>
		/// Creates a PricingRetailOptionsState object and initialises it.
		/// This includes creating a new Itinerary object.
		/// </summary>
		/// <returns>A new PricingRetailOptionsState object</returns>
		private AsyncCallStatus CreatePricingRetailOptions() 
		{
			AsyncCallStatus priceStatus = AsyncCallStatus.CompletedOK;
			
			// See if there is a PricingRetailOptionsState already. 
			PricingRetailOptionsState options = itineraryManager.PricingRetailOptions;
			
			if (options == null)
			{
				options = new PricingRetailOptionsState();
				itineraryManager.PricingRetailOptions = options;
				sessionManager.PricingRetailOptions   = options;				
			}

			if ( !TDItineraryManager.Current.FullItinerarySelected )
			{
				//MG 01/02/2006
				//Load discounts from profile only if there is an authenticated user and
				//if there is no discount cards selected from the user interface.
				if(sessionManager.Authenticated
					&& (options.Discounts.RailDiscount.Length == 0)
					&& (options.Discounts.CoachDiscount.Length == 0))
				
				{
					options.LoadDiscountsFromProfile( sessionManager.CurrentUser.UserProfile );
				}

				if (options.HasProcessedRetailersJourneyChanged || options.JourneyItinerary == null)
				{
                    // City to city filters journeys based on modeType. Must pass in the modeType to ensure
                    // the correct journey is selected for pricing
                    TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
                    if (TDSessionManager.Current.FindPageState != null)
                        modeTypes = TDSessionManager.Current.FindPageState.ModeType;

					options.JourneyItinerary = ItineraryAdapter.CreateItinerary(itineraryManager.JourneyResult, itineraryManager.JourneyViewState, modeTypes);
					options.InitOverrideItineraryType(options.JourneyItinerary.Type);
				}

				// Set up an async call state in the session manager for the call
				AsyncCallState callState = new TimeBasedFaresSearchState();

				// Determine refresh interval and resource string for the wait page
				callState.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.TicketsAndCosts"]);
				callState.WaitPageMessageResourceFile = "langStrings";
				callState.WaitPageMessageResourceId = "WaitPageMessage.TicketsAndCosts";

				callState.AmbiguityPage = this.PageId;
				callState.DestinationPage = this.PageId;
				callState.ErrorPage = this.PageId;
				sessionManager.AsyncCallState = callState;

				//As we have just calculated pricing information there cannot be
				//any new discounts to apply so set to false.
				options.ApplyNewDiscounts = false;

				options.SetProcessedJourneys();

				sessionManager.SaveData();

				// persist updated options to deferred storage
				CJPSessionInfo sessionInfo = sessionManager.GetSessionInformation();
				TDSessionSerializer serializer = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);
				serializer.SerializeSessionObjectAndSave(sessionInfo.SessionId, sessionManager.Partition, TDSessionManager.KeyPricingRetailOptions, options);

				//Calculate pricing information for currently selected journey(s)
				
				ITimeBasedPriceSupplier priceSupplier = (ITimeBasedPriceSupplier) TDServiceDiscovery.Current[ServiceDiscoveryKey.TimeBasedPriceSupplier];

				#region Fare request ID
				// Pass the journey request id on to the Fares call so that we can tie together a journey request
				// and fares request when extracting logs using the Application Support logs viewer tool
				int journeyRequestID = sessionManager.JourneyResult.JourneyReferenceNumber;
				int lastSequenceNumber = sessionManager.JourneyResult.LastReferenceSequence;
				int lastFareIncrement = sessionManager.JourneyResult.LastFareRequestNumber;

				string requestID = SqlHelper.FormatRef(journeyRequestID)
					+ lastSequenceNumber.ToString("-0000")
					+ lastFareIncrement.ToString("-00");	// FareCall will then add "01", "02"..., for each call made to ensure id is unique
				
				// save the last FareRequest number used
				sessionManager.JourneyResult.LastFareRequestNumber = lastFareIncrement + 1;
				#endregion

				priceStatus = priceSupplier.PriceItinerary(options.JourneyItinerary, options.Discounts, requestID);

				sessionManager.AsyncCallState.Status = priceStatus;
			}


			return priceStatus;
		}

		/// <summary>
		/// Determines whether a "Find Cheaper" link should be displayed
		/// </summary>
		/// <returns>true/false</returns>
		private bool DisplayFindCheaperLink(TransportDirect.JourneyPlanning.CJPInterface.ModeType[] transportModes)
		{
			FindAMode mode = GetCurrentFindAMode();
			
			if (mode == FindAMode.Train)	
			{
				return FindInputAdapter.IsFindCheaperAvailable(TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail);
			}
			else if (mode == FindAMode.Coach)	
			{
				return FindInputAdapter.IsFindCheaperAvailable(TransportDirect.JourneyPlanning.CJPInterface.ModeType.Coach);
			}
			else if (mode == FindAMode.Trunk)	
			{
				if	(transportModes.Length > 0)
				{
					TransportDirect.JourneyPlanning.CJPInterface.ModeType singleMode = transportModes[0];

					for	(int i = 1; i < transportModes.Length; i++)
					{
						if	(transportModes[i] != singleMode)
						{
							return false;
						}
					}
				
					return FindInputAdapter.IsFindCheaperAvailable(singleMode);
				}
			}

			return false;			
		}

		/// <summary>
		/// Determines the Find-A context in which this page is being used
		/// </summary>
		/// <returns>Current mode</returns>
		private FindAMode GetCurrentFindAMode()
		{
			ExtendItineraryManager itineraryManager = TDSessionManager.Current.ItineraryManager as ExtendItineraryManager;
			FindAMode mode = FindAMode.None;

			if (itineraryManager != null)
			{
				// We're in an itinerary. We will only show the button if the original
				// find a mode was trunk, and the original journey is selected
				if ((!itineraryManager.ExtendInProgress) && (!itineraryManager.FullItinerarySelected))
				{
					mode = itineraryManager.SpecificFindAMode(itineraryManager.SelectedItinerarySegment);
				}
			}
			else if (TDSessionManager.Current.FindPageState != null)
			{
				mode = TDSessionManager.Current.FindPageState.Mode;
			}
			
			return mode;
		}
	

		/// <summary>
		/// Calculate car journey costs if a road journey is available
		/// </summary>
		private void CalculateCarCosts()
		{
			// Determine whether or not to show the outward control, and if so, set it up
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			
			bool outwardIsRoad = (viewState != null) && 
				(viewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested);
			bool returnIsRoad = (viewState != null) && 
				(viewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested);

			//If outward journey is by road, set control visiblity and also set up the properties
			//on the car cost controls.
			if (outwardIsRoad)
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CalculateCosts();

			if (returnIsRoad)
				returnCarJourneyCostsControl.ItemisedCarCostsControl.CalculateCosts();
		}

		/// <summary>
		/// Sets up all of the properties required for the car costing controls.
		/// Should be called from PageLoad to ensure that everything is in place
		/// prior to processing taking place.
		/// </summary>
		private void SetupCarCosts()
		{
			bool outwardIsRoad = !outputAdapter.OutwardIsPublic;
			bool inwardIsRoad = !outputAdapter.InwardIsPublic && returnExists;

			//If outward journey is by road, set control visiblity and also set up the properties
			//on the car cost controls.
			if (outwardIsRoad)
			{
				outwardCarJourneyCostsControl.Visible = true;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.IsOutward = true;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.OtherCostsControl.IsOutward = true;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CarParkCostsControl.IsOutward = true;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.ShowRunning = TDItineraryManager.Current.JourneyViewState.ShowRunning;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.ReturnExists = inwardIsRoad;

				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CarRoadJourney = itineraryManager.JourneyResult.OutwardRoadJourney();
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.OtherCostsControl.CarRoadJourney = itineraryManager.JourneyResult.OutwardRoadJourney();
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CarParkCostsControl.CarRoadJourney = itineraryManager.JourneyResult.OutwardRoadJourney();

				outwardCarJourneyCostsControl.TotalCarCostsControl.Visible = false;

				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CalculateCosts();
			}
			else
			{
				outwardCarJourneyCostsControl.Visible = false;
				itineraryManager.JourneyViewState.CongestionCostAdded = false;
				itineraryManager.JourneyViewState.CongestionChargeAdded = false;
			}

			//If return journey is by road, set control visiblity and also set up the properties
			//on the car cost controls.
			if (inwardIsRoad)
			{
				returnCarJourneyCostsControl.Visible = true;
				returnCarJourneyCostsControl.ItemisedCarCostsControl.IsOutward = false;
				returnCarJourneyCostsControl.ItemisedCarCostsControl.ShowRunning = TDItineraryManager.Current.JourneyViewState.ShowRunning;
				returnCarJourneyCostsControl.ItemisedCarCostsControl.ReturnExists = true;			
				
				returnCarJourneyCostsControl.TotalCarCostsControl.Visible = outwardIsRoad;
				
				returnCarJourneyCostsControl.ItemisedCarCostsControl.CarRoadJourney = itineraryManager.JourneyResult.ReturnRoadJourney();				
				returnCarJourneyCostsControl.ItemisedCarCostsControl.OtherCostsControl.CarRoadJourney =	itineraryManager.JourneyResult.ReturnRoadJourney();
				returnCarJourneyCostsControl.ItemisedCarCostsControl.CarParkCostsControl.CarRoadJourney = itineraryManager.JourneyResult.ReturnRoadJourney();
				
				returnCarJourneyCostsControl.ItemisedCarCostsControl.CalculateCosts();
			}
			else
			{
				itineraryManager.JourneyViewState.CongestionCostAdded = false;
				itineraryManager.JourneyViewState.CongestionChargeAdded = false;
				outwardCarJourneyCostsControl.ItemisedCarCostsControl.CalculateCosts();

			}
		}

		/// <summary>
		/// Performs actions related to displaying car cost totals if the
		/// return journey is by road.
		/// </summary>
		private void DisplayCarCostTotals()
		{
			//Determine ir return journey is by road.
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			if (!outputAdapter.InwardIsPublic && returnExists)
			{
				// set the total cost for the entire journey = fuel, running (optional), and tolls, outward + return
				// This needs to be done every time due to the user being able to select
				// different journeys on the summaryresult control which causes a postback
				// Other changes to car costs displayed are handled by the CostPageTitle event handler				
				if (returnCarJourneyCostsControl.TotalCarCostsControl.Visible)
				{
					decimal cost = outwardCarJourneyCostsControl.ItemisedCarCostsControl.TotalCost + 
						returnCarJourneyCostsControl.ItemisedCarCostsControl.TotalCost;

					//If either of the itemised car cost controls is set to display a decimalised total
					//then the overall total must be decimalised as well. Otherwise it must be 
					//rounded to the nearest whole number.
					if (outwardCarJourneyCostsControl.ItemisedCarCostsControl.DecimaliseTotalCost ||
						returnCarJourneyCostsControl.ItemisedCarCostsControl.DecimaliseTotalCost)
					{	
						// If we meed to show a decimalised total cost then (not sure if this is the
						// best way to do it but) get the substring of the total price for both the
						// pounds and the pence.
						string temp = String.Format(TDCultureInfo.CurrentUICulture, "{0:C}", cost);

						//Perform quick safety check as an empty string will cause an excpetion.
						if (temp.Length > 0)
						{
							returnCarJourneyCostsControl.TotalCarCostsControl.PoundsLabel.Text = temp.Substring(0,temp.Length - 3);
							returnCarJourneyCostsControl.TotalCarCostsControl.PenceLabel.Text = temp.Substring(temp.Length - 3,3);
						}
					}
					else
					{
						returnCarJourneyCostsControl.TotalCarCostsControl.PoundsLabel.Text = 
							TDCultureInfo.CurrentUICulture.NumberFormat.CurrencySymbol + Decimal.Round(cost, 0);
					}
				}
			}
			else 
			{
				returnCarJourneyCostsControl.Visible = false;				
				returnCarJourneyCostsControl.ItemisedCarCostsControl.ReturnExists = false;
			}
		}

		/// <summary>
		/// Returns true if the supplied PricingRetailOptionsState is not null, and if it has
		/// a JourneyItinerary that has been priced.
		/// </summary>
		/// <param name="options"></param>
		private bool PricedItineraryAvailable(PricingRetailOptionsState options)
		{
			return (options != null && options.JourneyItinerary != null);
		}		

		/// <summary>
		/// Creates a new ItineraryAdapter for the current ininerary and is
		/// responsible for initialising the outward and return fares controllers
		/// </summary>
		private void DisplayFares() 
		{
			itineraryManager.PricingRetailOptions = sessionManager.PricingRetailOptions;
			PricingRetailOptionsState options = itineraryManager.PricingRetailOptions;

			if (PricedItineraryAvailable(options))
			{
				//default visibility of 'buy' button
				buttonRetailers.Visible = false;

                if (outputAdapter.OutwardIsPublic || outputAdapter.InwardIsPublic)
                {
                    ItineraryAdapter adapter = new ItineraryAdapter(options.JourneyItinerary);
                    adapter.OverrideItineraryType = options.OverrideItineraryType;

                    bool isAccessibleJourney = false;

                    #region Check for accessible journey

                    // Do not display the retailer buttons or the select fares radios if this is an accessible journey
                    if (options.JourneyItinerary != null && options.JourneyItinerary.OutwardJourney != null
                        && options.JourneyItinerary.OutwardJourney is PublicJourney)
                    {
                        PublicJourney pj = (PublicJourney)options.JourneyItinerary.OutwardJourney;

                        if (pj.AccessibleJourney)
                            isAccessibleJourney = true;
                    }
                    else if (options.JourneyItinerary != null && options.JourneyItinerary.ReturnJourney != null
                        && options.JourneyItinerary.ReturnJourney is PublicJourney)
                    {
                        PublicJourney pj = (PublicJourney)options.JourneyItinerary.ReturnJourney;

                        if (pj.AccessibleJourney)
                            isAccessibleJourney = false;
                    }

                    #endregion

                    if (outputAdapter.OutwardIsPublic)
                    {
                        OutboundJourneyFaresControl.Visible = true;
                        OutboundJourneyFaresControl.ItineraryAdapter = adapter;
                        OutboundJourneyFaresControl.ShowChildFares = options.ShowChildFares;
                        OutboundJourneyFaresControl.PrinterFriendly = false;
                        OutboundJourneyFaresControl.InTableMode = options.ShowOutboundFaresInTableFormat;
                        OutboundJourneyFaresControl.IsForReturn = false;
                        OutboundJourneyFaresControl.HideTicketSelection = isAccessibleJourney;
                        OutboundJourneyFaresControl.FullItinerarySelected = TDItineraryManager.Current.FullItinerarySelected;
                        if (adapter.DoesJourneyContainFares(false))
                        {
                            OutboundJourneyFaresControl.ShowFindCheaper(DisplayFindCheaperLink(options.JourneyItinerary.OutwardModes));
                        }
                        else
                        {
                            OutboundJourneyFaresControl.ShowFindCheaper(false);
                        }

                        if (options.Discounts != null)
                        {
                            OutboundJourneyFaresControl.RailDiscount = options.Discounts.RailDiscount;
                            OutboundJourneyFaresControl.CoachDiscount = options.Discounts.CoachDiscount;
                        }

                        OutboundJourneyFaresControl.CJPUser = IsCJPUser();
                    }
                    else
                    {
                        OutboundJourneyFaresControl.Visible = false;
                    }

                    if (outputAdapter.InwardIsPublic)
                    {
                        ReturnJourneyFaresControl.Visible = true;
                        ReturnJourneyFaresControl.ItineraryAdapter = adapter;
                        ReturnJourneyFaresControl.ShowChildFares = options.ShowChildFares;
                        ReturnJourneyFaresControl.PrinterFriendly = false;
                        ReturnJourneyFaresControl.InTableMode = options.ShowReturnFaresInTableFormat;
                        ReturnJourneyFaresControl.IsForReturn = true;
                        ReturnJourneyFaresControl.HideTicketSelection = isAccessibleJourney;
                        ReturnJourneyFaresControl.FullItinerarySelected = TDItineraryManager.Current.FullItinerarySelected;
                        if (adapter.DoesJourneyContainFares(true))
                        {
                            ReturnJourneyFaresControl.ShowFindCheaper(DisplayFindCheaperLink(options.JourneyItinerary.ReturnModes));
                        }
                        else
                        {
                            ReturnJourneyFaresControl.ShowFindCheaper(false);
                        }

                        if (options.Discounts != null)
                        {
                            ReturnJourneyFaresControl.RailDiscount = options.Discounts.RailDiscount;
                            ReturnJourneyFaresControl.CoachDiscount = options.Discounts.CoachDiscount;
                        }

                        ReturnJourneyFaresControl.CJPUser = IsCJPUser();
                    }
                    else
                    {
                        ReturnJourneyFaresControl.Visible = false;
                    }

                    //hide or show the buy ticket button dependant on whether the user can actually select a ticket
                    //don't allow ticket to be selected if an extension is in progress
                    if (!TDItineraryManager.Current.ExtendInProgress)
                    {
                        if (OutboundJourneyFaresControl.Visible || ReturnJourneyFaresControl.Visible)
                        {
                            bool isOutwardOrReturnJourneyPriced = (adapter.IsJourneyPriced(false) || adapter.IsJourneyPriced(true));
                            bool journeyContainsFares = (adapter.DoesJourneyContainFares(false) || adapter.DoesJourneyContainFares(true));

                            buttonRetailers.Visible = (isOutwardOrReturnJourneyPriced && journeyContainsFares);

                            // Do not display the retailer buttons if this is an accessible journey
                            if (isAccessibleJourney)
                                buttonRetailers.Visible = false;
                        }
                    }
                }
                else
                {
                    OutboundJourneyFaresControl.Visible = false;
                    ReturnJourneyFaresControl.Visible = false;
                }
			}
			else
			{
				OutboundJourneyFaresControl.Visible = false;
				ReturnJourneyFaresControl.Visible = false;
			}
		}

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// Handles visibility of links according to status of screen (eg whether return journeys exist)
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{
			// Setup gif resource for images (1 invisible image for all skip links)
			string SkipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageJourneyButtonsSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageJourneyButtonsSkipLink2.ImageUrl = SkipLinkImageUrl;
			imageJourneyButtonsSkipLink3.ImageUrl = SkipLinkImageUrl;
			imageOutwardJourneyPTSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageReturnJourneyPTSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageFindTransportToStartLocationSkipLink.ImageUrl = SkipLinkImageUrl;
			imageFindTransportFromEndLocationSkipLink.ImageUrl = SkipLinkImageUrl;

			imageMainContentSkipLink1.AlternateText = GetResource("JourneyFares.imageMainContentSkipLink.AlternateText");

			string journeyButtonsSkipLinkText = GetResource("JourneyFares.imageJourneyButtonsSkipLink.AlternateText");

			imageJourneyButtonsSkipLink1.AlternateText = journeyButtonsSkipLinkText;
			imageJourneyButtonsSkipLink3.AlternateText = journeyButtonsSkipLinkText;

			// Show different skip links depending on status of search (car vs public transport & outward, return)
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;
			bool isOutwardPublic = (viewState != null) && (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal || viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended);
			bool isReturnPublic = (viewState != null) && (viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal || viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended);

			if (outwardExists)
			{
				if (itineraryExists)	// In Extend a mode
				{
					panelFindTransportToStartLocationSkipLink.Visible = true;
					imageFindTransportToStartLocationSkipLink.AlternateText = GetResource("JourneyFares.imageFindTransportToStartLocationSkipLink.AlternateText");

					panelFindTransportFromEndLocationSkipLink.Visible = true;
					imageFindTransportFromEndLocationSkipLink.AlternateText = 
						GetResource("JourneyFares.imageFindTransportFromEndLocationSkipLink.AlternateText");
				}
				else
				{
					panelFindTransportToStartLocationSkipLink.Visible = false;
					panelFindTransportFromEndLocationSkipLink.Visible = false;
				}
			}

			if (returnExists)	// Have return - need link from end of outward back to Journey buttons
			{
				panelJourneyButtonsSkipLink2.Visible=true;
				imageJourneyButtonsSkipLink2.AlternateText = journeyButtonsSkipLinkText;
			}

			if (outwardExists)
			{
				if (isOutwardPublic)
				{
					// Show outward public transport skip links
					panelOutwardJourneyPTSkipLink1.Visible = true;
					imageOutwardJourneyPTSkipLink1.AlternateText = 
						GetResource("JourneyFares.imageOutwardJourneyPTSkipLink1.AlternateText");
				}
				if (!isOutwardPublic)
				{
					// No specific car links to show
				}
			}

			if (returnExists)
			{
				if (isReturnPublic)
				{
					// Show return public transport skip links
					panelReturnJourneyPTSkipLink1.Visible = true;
					imageReturnJourneyPTSkipLink1.AlternateText = 
						GetResource("JourneyFares.imageReturnJourneyPTSkipLink1.AlternateText");
				}
				if (!isReturnPublic)
				{
					// No specific car links to show
				}
			}
		}

		#endregion

		#region Methods to control visiblities of controls
		/// <summary>
		/// Sets the visibilities of the "outward" components.
		/// </summary>
		/// <param name="visible">True if outward components should be visible
		/// and false if outward components should not be visible.</param>
		//Added as part of fix for Vantive 3918704
		private void SetOutwardVisible(bool visible)
		{
			outwardPanel.Visible = visible;
			summaryResultTableControlOutward.Visible = visible;
			hyperlinkOutwardJourneys.Visible = visible;
			hyperlinkImageOutwardJourneys.Visible = visible;
		}

		
		/// <summary>
		/// Sets the visibilities of the "Return" components.
		/// </summary>
		/// <param name="visible">True if return components should be visible
		/// and false if return components should not be visible.</param>
		private void SetReturnVisible(bool visible)
		{
			returnPanel.Visible = visible;
			summaryResultTableControlReturn.Visible = visible;
			hyperlinkReturnJourneys.Visible = visible;
			hyperlinkImageReturnJourneys.Visible = visible;
		}

		#endregion
        
		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
            ExtraWiringEvents();
			InitializeComponent();
            
			base.OnInit(e);
		}

        /// <summary>
        /// 
        /// </summary>
        private void ExtraWiringEvents()
        {
            socialBookMarkLinkControl.EmailLinkButton.Click += new EventHandler(EmailLink_Click);
        }

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            if (TDSessionManager.Current.FindAMode == FindAMode.Trunk)
                this.theJourneyChangeSearchControl.GenericBackButton.Click += new EventHandler(this.buttonBackJourneyOverview_Click);
		}
		
		#endregion

		#region private methods

		/// <summary>
		/// Transfers user to part way through the Find A Fare process
		/// For return journeys where the itinerary type is single only the outbound/return section
		/// of the journey will be passed to the costSearchRunner as a single journey. 
		/// For all other cases the entire journey is used 
		/// </summary>
		/// <param name="isReturn">True if link clicked was for the return fares</param>
		private void FindCheaperFares(bool isReturn)
		{
			PricingRetailOptionsState timeBasedOptions = TDItineraryManager.Current.PricingRetailOptions;			
			ITDSessionManager sessionManager = TDSessionManager.Current;

			FindAMode findAMode = GetCurrentFindAMode();

			// we need to get this before the partition is swapped time-based -> cost-based 
			ItineraryType overrideItineraryType = timeBasedOptions.OverrideItineraryType;
			ItineraryType itineraryType = timeBasedOptions.JourneyItinerary.Type;

			//temporarily store the journey parameters from the session
			TDJourneyParameters oldParams = TDItineraryManager.Current.JourneyParameters;

			//InitialiseJourneyParametersPageStates forces new journey paramaters			
			sessionManager.InitialiseJourneyParameters(FindAMode.Fare);

			//current session journey parameters will now be a costSearchParams object
			CostSearchParams costSearchParams = (CostSearchParams) sessionManager.JourneyParameters;
			
			Itinerary itinerary = timeBasedOptions.JourneyItinerary;

			TDLocation originLocation = null;
			TDLocation destinationLocation = null;

			// If city-to-city, just use the original locations (from city-to-city database).
			//	Cannot do this for find-a-train, because original locations may be (eg) London (Any Rail),
			//  which could generate dozens of FBO requests, so instead use the actual start and end
			//  locations for this specific journey result and let the FBO deal with fare groups.
			
			if	(findAMode == FindAMode.Trunk)
			{
				originLocation		= oldParams.OriginLocation;
				destinationLocation = oldParams.DestinationLocation;
			}
			else
			{
				PublicJourneyDetail[] firstJourneyDetails;
				PublicJourneyDetail[] lastJourneyDetails;
				
				if	(!isReturn)
				{
					firstJourneyDetails = ((PublicJourney)itinerary.OutwardJourney).Details; 
					lastJourneyDetails  = ((PublicJourney)itinerary.OutwardJourney).Details; 
					originLocation		= firstJourneyDetails[0].LegStart.Location;	
					destinationLocation = lastJourneyDetails[lastJourneyDetails.Length - 1].LegEnd.Location;												
				}
				else
				{
					firstJourneyDetails = ((PublicJourney)itinerary.ReturnJourney).Details; 
					lastJourneyDetails  = ((PublicJourney)itinerary.ReturnJourney).Details; 
					originLocation		= lastJourneyDetails[lastJourneyDetails.Length - 1].LegEnd.Location;												
					destinationLocation = firstJourneyDetails[0].LegStart.Location;	
				}

				originLocation.Status		= TDLocationStatus.Valid;							
				destinationLocation.Status	= TDLocationStatus.Valid;

				string[] names = GetLocationNamesFromFares(itinerary, isReturn);
				
				if	(names.Length == 2)
				{
					if	(names[0].Length > 0)
					{
						originLocation.Description = names[0];
					}

					if	(names[1].Length > 0)
					{
						destinationLocation.Description = names[1];
					}
				}
			}

			//populate the properties of costSearchParams
			costSearchParams.OriginLocation		 = originLocation;
			costSearchParams.DestinationLocation = destinationLocation;

			// set no flexibility on the dates
			costSearchParams.OutwardFlexibilityDays = 0;
			costSearchParams.InwardFlexibilityDays  = 0;

			// include only "cheaper-enabled" travel modes in the cost based search
			
			bool cheaperRail  =	bool.Parse(Properties.Current["FindCheaperAvailable.Rail"]);
			bool cheaperCoach =	bool.Parse(Properties.Current["FindCheaperAvailable.Coach"]);
			bool cheaperAir   =	bool.Parse(Properties.Current["FindCheaperAvailable.Air"]);
	
			int modeCount = 0;

			if	(cheaperRail) 
			{
				modeCount++;
			}
			
			if	(cheaperCoach) 
			{
				modeCount++;
			}

			if	(cheaperAir) 
			{
				modeCount++;
			}

			if	(modeCount == 0) 
			{
				Logger.Write(new OperationalEvent (TDEventCategory.Business, TDTraceLevel.Error, "No modes found for Find Cheaper"));
				return;
			}
			
			TicketTravelMode[] travelModeParams = new TicketTravelMode[modeCount];

			modeCount = 0;

			if	(cheaperRail)
			{
				travelModeParams[modeCount++] = TicketTravelMode.Rail;
			}
			
			if	(cheaperCoach)
			{
				travelModeParams[modeCount++] = TicketTravelMode.Coach;
			}

			if	(cheaperAir)
			{
				travelModeParams[modeCount++] = TicketTravelMode.Air;
			}
			
			costSearchParams.TravelModesParams = travelModeParams;

			//find what discounts are to be applied
			costSearchParams.CoachDiscountedCard = timeBasedOptions.Discounts.CoachDiscount;
			costSearchParams.RailDiscountedCard  = timeBasedOptions.Discounts.RailDiscount;

			//set the outward departure time details to be used by default
			TDDateTime outwardDepartureTime = ((PublicJourney)itinerary.OutwardJourney).Details[0].LegStart.DepartureDateTime;												
			costSearchParams.OutwardDayOfMonth = outwardDepartureTime.ToString("dd");
			costSearchParams.OutwardMonthYear= outwardDepartureTime.ToString("MM") + "/" + outwardDepartureTime.ToString("yyyy");	

			//set return properties to empty by default
			costSearchParams.IsReturnRequired = false;
			costSearchParams.ReturnDayOfMonth = string.Empty;
			costSearchParams.ReturnMonthYear = TransportDirect.Common.ReturnType.NoReturn.ToString();

            // Set routing guide flags
            costSearchParams.RoutingGuideInfluenced = bool.Parse(Properties.Current["RoutingGuide.FindATrainCost.RoutingGuideInfluenced"]);
            costSearchParams.RoutingGuideCompliantJourneysOnly = bool.Parse(Properties.Current["RoutingGuide.FindATrainCost.RoutingGuideCompliantJourneysOnly"]);

			FindCostBasedPageState pageState = (FindCostBasedPageState)sessionManager.FindPageState;

			pageState.ShowChild = timeBasedOptions.ShowChildFares;

			if (itinerary.ReturnJourney != null)			// return journey
			{
				costSearchParams.IsReturnRequired = true;
				
				if (itineraryType == ItineraryType.Single)	//unmatched return - treat as a single journey
				{
					if (overrideItineraryType == ItineraryType.Return)		//return fares selected
					{
						pageState.SelectedTicketType = TicketType.OpenReturn;
					}
					else	
					{
						pageState.SelectedTicketType = TicketType.Singles;
					}
				}
				else
				{
					if	(overrideItineraryType == ItineraryType.Return)
					{
						pageState.SelectedTicketType = TicketType.Return;
					}
					else	
					{
						pageState.SelectedTicketType = TicketType.Singles;
					}
				}
			}
			else	// single journey - hence no return dates are required
			{				
				if (overrideItineraryType == ItineraryType.Return)
				{
					pageState.SelectedTicketType = TicketType.OpenReturn;
				}
				else
				{
					pageState.SelectedTicketType = TicketType.Single;
				}
			}

			if	(pageState.SelectedTicketType == TicketType.Return || pageState.SelectedTicketType == TicketType.Singles)
			{
				TDDateTime returnDepartureTime = ((PublicJourney)itinerary.ReturnJourney).Details[0].LegStart.DepartureDateTime;
				costSearchParams.ReturnDayOfMonth = returnDepartureTime.ToString("dd");
				costSearchParams.ReturnMonthYear= returnDepartureTime.ToString("MM") + "/" + returnDepartureTime.ToString("yyyy");	
			}

			//Invoke the cost search runner.			
			FindFareInputAdapter adapter = new FindFareInputAdapter(pageState, sessionManager);

			// Determine refresh interval and resource string for the wait page
			int refreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.FindFare.CheapFaresLink"]);
			string resourceFilename = "langStrings";
			string resourceId = "WaitPageMessage.FindFare.CheapFaresLink";

			adapter.InitialiseAsyncCallStateForFaresSearch(refreshInterval, resourceFilename, resourceId);

			sessionManager.AsyncCallState.AmbiguityPage		= PageId.JourneyFares;
			sessionManager.AsyncCallState.ErrorPage			= PageId.JourneyFares;
			sessionManager.AsyncCallState.DestinationPage   = PageId.FindFareDateSelection;
			
			//Holds returned search wait state.
			AsyncCallStatus searchWaitState;
			searchWaitState = adapter.InvokeValidateAndRunFares();
			
			//Handle the returned statuses 
			switch (searchWaitState)
			{
				case AsyncCallStatus.InProgress:
				{
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;
					break;
				}

				case AsyncCallStatus.CompletedOK:
				{
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindFareDateSelectionDefault;
					break;
				}

				//should never get a validationerror as this check is performed in the FindA page
				//therefore log an operational event if it occurs here
				case AsyncCallStatus.ValidationError:
				{
					//log operational event			
					string errorMessage = "Validation error returned from CostSearchRunner when none expected.";
							
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, errorMessage);

					Logger.Write(operationalEvent);
					
					//reset journey parameters
					sessionManager.InitialiseJourneyParameters(FindAMode.Trunk);

					break;
				}
			}		
		}

		/// <summary>
		/// Obtains the names of origin and destination from the fares. 
		/// This allows us to use fare group names in the Find Cheaper 
		/// output in preference to the specific stations from the journey 
		/// results. Because this is only used for fares obtained in 
		/// Find-a-Train, we can assume that the whole outbound itinerary 
		/// has been costed as a single pricing unit. In the unexpected 
		/// case of more than one unit, or if the unit is not priced, 
		/// the method will return an empty array. 
		/// </summary>
		/// <param name="itinerary">the itinerary</param>
		/// <param name="isReturn">true if for return fares</param>
		/// <returns>origin and destination station names</returns>
		private string[] GetLocationNamesFromFares(Itinerary itinerary, bool isReturn)
		{
			string originName	   = string.Empty;
			string destinationName = string.Empty;
			
			if	(!isReturn && itinerary.OutwardUnits.Count != 1)
			{
				return new string[0];
			}

			if	(isReturn && itinerary.ReturnUnits.Count != 1)
			{
				return new string[0];
			}

			PricingUnit pu;
			
			if	(isReturn)
			{
				pu = ((PricingUnit) itinerary.ReturnUnits[0]);
			}
			else
			{
				pu = ((PricingUnit) itinerary.OutwardUnits[0]);
			}

			if	(pu.SingleFares.Tickets.Count > 0)
			{
				if	(!isReturn)
				{
					originName		= ((Ticket)pu.SingleFares.Tickets[0]).TicketRailFareData.OriginName;				
					destinationName = ((Ticket)pu.SingleFares.Tickets[0]).TicketRailFareData.DestinationName;				
				}
				else
				{
					originName		= ((Ticket)pu.SingleFares.Tickets[0]).TicketRailFareData.DestinationName;				
					destinationName = ((Ticket)pu.SingleFares.Tickets[0]).TicketRailFareData.OriginName;				
				}
			}
			else if (pu.ReturnFares.Tickets.Count > 0)
			{
				if	(!isReturn)
				{
					originName		= ((Ticket)pu.ReturnFares.Tickets[0]).TicketRailFareData.OriginName;				
					destinationName = ((Ticket)pu.ReturnFares.Tickets[0]).TicketRailFareData.DestinationName;				
				}
				else
				{
					originName		= ((Ticket)pu.ReturnFares.Tickets[0]).TicketRailFareData.DestinationName;				
					destinationName = ((Ticket)pu.ReturnFares.Tickets[0]).TicketRailFareData.OriginName;				
				}
			}
			else
			{
				return new string[0];
			}

			return new string[] { originName, destinationName };
		}


		/// <summary>
		/// Retrieves the tickets to be selected from the session data
		/// </summary>
		private void GetSelectedTickets()
		{
			PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;			

			// check itinerary adapter - if we're displaying car costs. rather than fares, it will be null
			if(OutboundJourneyFaresControl.ItineraryAdapter != null) 
			{
				foreach (PricingUnit pu in OutboundJourneyFaresControl.ItineraryAdapter.OutwardPricingUnits)
				{
					OutboundJourneyFaresControl.SetSelectedTicket(pu,options.GetSelectedTicket(pu));
				}
			}
						
			if(ReturnJourneyFaresControl.ItineraryAdapter != null) 
			{
				foreach (PricingUnit pu in ReturnJourneyFaresControl.ItineraryAdapter.ReturnPricingUnits)
				{
					ReturnJourneyFaresControl.SetSelectedTicket(pu,options.GetSelectedTicket(pu));
				}
			}
		}

		/// <summary>
		/// Stores the user's ticket selection in the session
		/// </summary>
		private void SetTicketDetails()
		{
			//need to update the session with the user selection
			PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;

			// check itinerary adapter - if we're displaying car costs. rather than fares, it will be null
			if(OutboundJourneyFaresControl.ItineraryAdapter != null) 
			{
				foreach (PricingUnit pu in OutboundJourneyFaresControl.ItineraryAdapter.OutwardPricingUnits)
				{
					FareDetailsTableSegmentControl fareDetailsTableSegmentControl = OutboundJourneyFaresControl.FaresTable(pu);				
					if (fareDetailsTableSegmentControl != null)					
						options.SetSelectedTicket(pu, fareDetailsTableSegmentControl.SelectedTicket);									
				}
			}
				
			if(ReturnJourneyFaresControl.ItineraryAdapter != null) 
			{
				foreach (PricingUnit pu in ReturnJourneyFaresControl.ItineraryAdapter.ReturnPricingUnits)
				{
					FareDetailsTableSegmentControl fareDetailsTableSegmentControl = ReturnJourneyFaresControl.FaresTable(pu);
					if (fareDetailsTableSegmentControl != null)
						options.SetSelectedTicket(pu, fareDetailsTableSegmentControl.SelectedTicket);
				}
			}
		}


		/// <summary>
		/// Set the selected list item in each drop down list to the ones specified
		/// in the session state
		/// </summary>
		private void SetAmendFareControl(PricingRetailOptionsState options)
		{		
			if (options != null)
			{
				Discounts discounts = options.Discounts;
					
				if (discounts != null)
				{
					AmendSaveSendControl.AmendFaresControl.RailCard = discounts.RailDiscount;
					AmendSaveSendControl.AmendFaresControl.CoachCard = discounts.CoachDiscount;
				}

				AmendSaveSendControl.AmendFaresControl.ShowChildFares = options.ShowChildFares;
				AmendSaveSendControl.AmendFaresControl.ShowItineraryType = options.OverrideItineraryType;
			}
		}


		/// <summary>
		/// Determines whether there is at least one ticket selected on the page
		/// </summary>
		private bool AreTicketsSelected()
		{
			bool ticketsSelected = false;

			// check itinerary adapter - if we're displaying car costs. rather than fares, it will be null
			if(OutboundJourneyFaresControl.ItineraryAdapter != null) 
			{
				foreach (PricingUnit pu in OutboundJourneyFaresControl.ItineraryAdapter.OutwardPricingUnits)
				{
					FareDetailsTableSegmentControl fareDetailsTableSegmentControl = OutboundJourneyFaresControl.FaresTable(pu);				
					if (fareDetailsTableSegmentControl != null)		
					{
						if (fareDetailsTableSegmentControl.SelectedTicket != null && 
							!fareDetailsTableSegmentControl.SelectedTicket.Equals( PricingRetailOptionsState.NoTicket ))
							ticketsSelected=true;

                        if (fareDetailsTableSegmentControl.SelectedTicket != null)
                        {
                            // Add Tracking Parameter for selected outward ticket
                            trackingHelper.AddTrackingParameter(OutboundJourneyFaresControl, fareDetailsTableSegmentControl.SelectedTicket.ShortCode);
                        }
					}					
				}
			}

			if(ReturnJourneyFaresControl.ItineraryAdapter != null) 
			{
				foreach (PricingUnit pu in ReturnJourneyFaresControl.ItineraryAdapter.ReturnPricingUnits)
				{
					FareDetailsTableSegmentControl fareDetailsTableSegmentControl = ReturnJourneyFaresControl.FaresTable(pu);
					if (fareDetailsTableSegmentControl != null)
					{
						if (fareDetailsTableSegmentControl.SelectedTicket != null && 
							!fareDetailsTableSegmentControl.SelectedTicket.Equals(PricingRetailOptionsState.NoTicket) )
							ticketsSelected=true;

                        if (fareDetailsTableSegmentControl.SelectedTicket != null)
                        {
                            // Add Tracking Parameter for selected return ticket
                            trackingHelper.AddTrackingParameter(ReturnJourneyFaresControl, fareDetailsTableSegmentControl.SelectedTicket.ShortCode);
                        }
					}				
				}
			}
			return ticketsSelected;
		}

        /// <summary>
        /// Sets the back button visibility when in Trunk mode
        /// </summary>
        private void SetJourneyOverviewBackButton()
        {
            if (TDSessionManager.Current.FindAMode == FindAMode.Trunk)
                theJourneyChangeSearchControl.GenericBackButtonVisible = true;
        }

        #region Private helper method - CJP User status

        /// <summary>
        /// Method which returns true if user is a higher-level (e.g. CJP) user 
        /// </summary>
        private bool IsCJPUser()
        {
            bool userIsLoggedOn = TDSessionManager.Current.Authenticated;

            // Get the user's type
            int userType = userIsLoggedOn ? (int)TDSessionManager.Current.CurrentUser.UserType : (int)TDUserType.Standard;

            return (userType > 0);
        }

        #endregion

		#endregion

		#region Event Handlers

		/// <summary>
		/// Handler for the Load event. Wires up control events
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Init(object sender, EventArgs e)
		{
			// Set up page level session and itinerary references
			sessionManager = TDSessionManager.Current;
			itineraryManager = sessionManager.ItineraryManager;

            // Set up page level tracking control helper reference
            trackingHelper = new TrackingControlHelper();

			this.buttonRetailers.Click += new EventHandler(this.buttonRetailers_Click);
            this.OutboundJourneyFaresControl.ButtonRetailers.Click += new EventHandler(this.buttonRetailers_Click);
            
			this.AmendSaveSendControl.AmendFaresControl.OKButton.Click += new EventHandler(AmendFares_Click);
			this.AmendSaveSendControl.AmendFaresControl.DropDownListRailCard.SelectedIndexChanged += new EventHandler(AmendFaresControl_DiscountRailCardChanged);
			this.AmendSaveSendControl.AmendFaresControl.DropDownListCoachCard.SelectedIndexChanged += new EventHandler(AmendFaresControl_DiscountCoachCardChanged);

			this.OutboundJourneyFaresControl.ViewButton.Click += new EventHandler(this.OutboundChangeView_Click);		
			this.OutboundJourneyFaresControl.FindCheaperLink.link_Clicked += new EventHandler(this.OutwardFindCheaper_Click);					
			this.OutboundJourneyFaresControl.InfoButtonClicked +=new EventHandler(this.InfoButtonClicked);
			this.OutboundJourneyFaresControl.OtherFaresClicked +=new EventHandler(OutboundJourneyFaresControl_OtherFaresClicked);

			this.ReturnJourneyFaresControl.ViewButton.Click += new EventHandler(this.ReturnChangeView_Click);		
			this.ReturnJourneyFaresControl.FindCheaperLink.link_Clicked += new EventHandler(this.InwardFindCheaper_Click);									
			this.ReturnJourneyFaresControl.InfoButtonClicked +=new EventHandler(this.InfoButtonClicked);

			this.ReturnJourneyFaresControl.OtherFaresClicked += new EventHandler(OutboundJourneyFaresControl_OtherFaresClicked);

			this.outwardCarJourneyCostsControl.ItemisedCarCostsControl.CostPageTitleControl.OKButton.Click += new EventHandler(CostPageTitle_OutwardOKButtonClick);
			this.returnCarJourneyCostsControl.ItemisedCarCostsControl.CostPageTitleControl.OKButton.Click += new EventHandler(CostPageTitle_ReturnOKButtonClick);

			//Event handlders for re-computing car costs when the user changes the current journey.
			this.summaryResultTableControlOutward.SelectionChanged += new SelectionChangedEventHandler(this.SummaryItineraryControl_Click);
			this.summaryResultTableControlReturn.SelectionChanged += new SelectionChangedEventHandler(this.SummaryItineraryControl_Click);

            this.findSummaryResultTableControlOutward.SelectionChanged += new SelectionChangedEventHandler(this.FindSummaryResultTableControl_Click);
            this.findSummaryResultTableControlReturn.SelectionChanged += new SelectionChangedEventHandler(this.FindSummaryResultTableControl_Click);

			this.OutboundJourneyFaresControl.ViewOtherFareLink.link_Clicked +=new EventHandler(ViewOtherFareLink_link_Clicked);
			this.ReturnJourneyFaresControl.ViewOtherFareLink.link_Clicked += new EventHandler(ViewOtherFareLink_link_Clicked);

           
		}

        

		/// <summary>
		/// Page load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event Parameter</param>
		private void Page_Load(object sender, System.EventArgs e)
		{
			outputAdapter = new PlannerOutputAdapter(sessionManager);

			sessionManager.JourneyViewState.CongestionCostAdded = false;
			sessionManager.JourneyViewState.CongestionChargeAdded = false;
			TDItineraryManager.Current.JourneyViewState.ShowRunning = false;

            //Added css to headElementControl for park and ride page
            if (sessionManager.FindAMode == FindAMode.ParkAndRide)
                headElementControl.Stylesheets += ",JourneyFaresParkAndRide.aspx.css";

            this.PageTitle = GetResource("JourneyPlanner.JourneyFaresPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            InitialiseStaticControls();

            if (sessionManager.FindAMode == FindAMode.Flight)
                journeysSearchedControl.IsTableView = true;

            // CCN 0427 setting help button
            theJourneyChangeSearchControl.HelpLabel = "helpJourneyFaresLabelControl";
            theJourneyChangeSearchControl.HelpCustomControl.HelpLabelControl = helpJourneyFaresLabelControl;
            theJourneyChangeSearchControl.HelpCustomControl.HelpLabel = "helpJourneyFaresLabelControl";


            headElementControl.ImageSource = GetModeImageSource();
            headElementControl.Desc = journeysSearchedControl.ToString();

            socialBookMarkLinkControl.BookmarkDescription = journeysSearchedControl.ToString();
            socialBookMarkLinkControl.EmailLink.NavigateUrl = Request.Url.AbsoluteUri + "#JourneyOptions";





            if (!Page.IsPostBack)
			{				
				DetermineStateOfResults();
				SetupControls();

				PricingRetailOptionsState options = itineraryManager.PricingRetailOptions;

				// If the options are already present, check to see if user has just logged in
				if (options != null)
				{
					options.CheckForNewUserLogin();
				}

				SetupCarCosts();
			}
			else 
			{
				DetermineStateOfResults();
                SetupControls();

				//Set up the required properties on the car cost controls. This is a change in functionality
				//as prior to IR2300 the set up and calculations were done in one method. This approach allows
				//us to set up the required properties on page load and the total can be calculated in pre-render
				//which avoids running all of the code twice.
				SetupCarCosts();

				/// setup the fuel cost for calculation of CO2
				if (itineraryManager.JourneyResult.OutwardRoadJourneyCount > 0)
					outwardCarJourneyCostsControl.ItemisedCarCostsControl.SummaryC02EmissionsControl.FuelCost = itineraryManager.JourneyResult.OutwardRoadJourney().TotalFuelCost;

				if (itineraryManager.JourneyResult.ReturnRoadJourneyCount > 0)
					returnCarJourneyCostsControl.ItemisedCarCostsControl.SummaryC02EmissionsControl.FuelCost = itineraryManager.JourneyResult.ReturnRoadJourney().TotalFuelCost;

				//display journey fares/car cost controls
				DisplayFares();				
			}
			// Initialise the output navigation control.
			this.theOutputNavigationControl.Initialise(pageId);
            
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            if (sessionManager.FindAMode == FindAMode.Trunk)
            {
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyFaresFindTrunkInput);
            }
            else
            {
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyFares);
            }
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		/// <summary>
		/// Event handler for when an info button is clicked on the tabledegementcontrol
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event Parameter</param>
		private void InfoButtonClicked(object sender, EventArgs e)
		{
			// redirect to Ticket Upgrade page				
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketUpgrade;
		}

		/// <summary>
		/// PreRender event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event Parameter</param>
		private void Page_PreRender(object sender, System.EventArgs e) 
		{
			DetermineStateOfResults();

			// If the screen is switching in either direction between the display of normal 
			// results and the display of the Itinerary then the controls must be reset
            // Or if we're in Trunk mode, because we're showing a filtered set of results 
            // which must be reloaded
			if (itineraryManager.ItineraryManagerModeChanged)
			{
				SetupControls();
			}

			// Initialise the session with pricing and retail user options
			PricingRetailOptionsState options = itineraryManager.PricingRetailOptions;

			TDJourneyViewState journeyViewState = itineraryManager.JourneyViewState;
			ITDJourneyResult journeyResult = itineraryManager.JourneyResult;			

			if ( !itineraryManager.FullItinerarySelected )
			{
				AmendSaveSendControl.manualPreRender();
				AmendSaveSendControl.AmendFaresControl.manualPreRender();

                PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(TDSessionManager.Current);

                plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);
                				
				//display journey fares controls
				DisplayFares();
			
				//Call to display the car cost totals. This method will decide if they
				//need to be displayed or not.
				DisplayCarCostTotals();

				GetSelectedTickets();
				SetAmendFareControl(options);
				SetupSkipLinksAndScreenReaderText();
			}
		}

		/// <summary>
		/// Override the OnPreRender method of the base class. Ensures the selected tickets are stored in the session.
		/// before any redirection takes place on the base class. 
		/// Putting the save code in the PreRender event handler won't work as it would not be called.
		/// This is because the redirect on TDPage would take place before that event is raised.
		/// </summary>
		/// <param name="e">Event arguments</param>
		protected override void OnPreRender(EventArgs e)
		{
			PricingRetailOptionsState options = itineraryManager.PricingRetailOptions;

			if (!IsReentrant)
			{				
				//The ticket selection in the session is only updated in the session if the 'LeaveTicketDisplay' flag hasn't been set
				//The LeaveTicketDisplay flag gets set on the TicketRetailers page and is used
				//to ensure that the current ticket selection is retained when the user returns to the journey fares page
				//This prevents the default ticket selections from being displayed
				if(options !=null)
				{
					if (!options.LeaveTicketDisplay) 
						SetTicketDetails(); 					
					else
						options.LeaveTicketDisplay= false; //reset the flag
				}
			}

			// Only attempt to recalculate the fares if there is no transition waiting to be actioned.
			// Otherwise, we'll recalculate them and potentially overwite the previous transition.
			if (sessionManager.FormShift[SessionKey.TransitionEvent] == TransitionEvent.Default)
			{
				// The next section recalculates the fares if necessary. We don't want to do this if
				// a button was clicked that is resulting in a redirect.

				if ( !itineraryManager.FullItinerarySelected )
				{
					// If journey selection has been changed for either outward or return journeys 
					// or the discount options have changed then: 
					// a) create a new itinerary if the selected journey has changed
					// b) if only the discount options have changed then don't create the itinerary, 
					// just re-price it - this prevents the pricing units being unnecessarily re-calculated
					if ( options == null || options.HasProcessedFaresJourneyChanged || options.JourneyItinerary == null)
					{
						AsyncCallStatus status = CreatePricingRetailOptions();
					
						if (status == AsyncCallStatus.InProgress)
						{
							// The call is asynchronous. Redirect to the wait page.
							sessionManager.Session[SessionKey.Transferred] = false;
							sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;
						}
						else
						{
							// The call was synchronous. Ensure that the PricingRetailOptionsState is reloaded
							// from deferred storage.
							sessionManager.ClearDeferredData();
							itineraryManager = sessionManager.ItineraryManager;
						}
					}

				}
			}

			
			base.OnPreRender(e);
		}

		/// <summary>
		/// Changes the display of the fares information between table and
		/// diagram mode for the outbound control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OutboundChangeView_Click(object sender, EventArgs e)
		{
			//need to update the session with the user selection

			TDItineraryManager.Current.PricingRetailOptions.ShowOutboundFaresInTableFormat = 
				!TDItineraryManager.Current.PricingRetailOptions.ShowOutboundFaresInTableFormat;
		}

		/// <summary>
		/// Changes the display of the fares information between table and
		/// diagram mode for the return control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ReturnChangeView_Click(object sender, EventArgs e)
		{			
			//need to update the session with the user selection
			PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;

			if (options != null)
			{
				options.ShowReturnFaresInTableFormat = !options.ShowReturnFaresInTableFormat;	
			}		
		}


		/// <summary>
		/// Links to 'Find a Fare process'
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void InwardFindCheaper_Click(object sender, EventArgs e)
		{			
			FindCheaperFares(true);			
		}

		/// <summary>
		/// Links to 'Find a Fare process'
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OutwardFindCheaper_Click(object sender, EventArgs e)
		{			
			FindCheaperFares(false);			
		}


		/// <summary>
		/// Stores the user's selections in the session
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AmendFares_Click(object sender, EventArgs e)
		{
			//need to update the session with the user selection
			PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;

			if (options != null)
			{
				options.Discounts.RailDiscount = AmendSaveSendControl.AmendFaresControl.RailCard;
				options.Discounts.CoachDiscount = AmendSaveSendControl.AmendFaresControl.CoachCard;
		
				options.ShowChildFares = AmendSaveSendControl.AmendFaresControl.ShowChildFares;
				options.OverrideItineraryType =AmendSaveSendControl.AmendFaresControl.ShowItineraryType;

                if (options.ShowChildFares)
                {
                    if (options.AdultPassengers <= 1 && options.ChildPassengers == 0)
                    {
                        options.AdultPassengers = 0;
                        options.ChildPassengers = 1;
                    }

                }
                else
                {
                    if (options.AdultPassengers == 0 && options.ChildPassengers <= 1)
                    {
                        options.AdultPassengers = 1;
                        options.ChildPassengers = 0;
                    }
                }

				options.SetProcessedJourneys();

				//only set apply new discounts if the rail or coach discounts have changed
				if (discountsChanged)
				{
					options.ApplyNewDiscounts = true;
					discountsChanged = false;
				}
			}

		}

		//Handle the discount rail card changed event.
		private void AmendFaresControl_DiscountRailCardChanged(object sender, EventArgs e)
		{
			discountsChanged = true;
		}

		//Handle the discount coach card changed event.
		private void AmendFaresControl_DiscountCoachCardChanged(object sender, EventArgs e)
		{
			discountsChanged = true;
		}


		override protected void OnUnload(EventArgs e)
		{
            base.OnUnload(e);
		}



        private string GetModeImageSource()
        {
            string modeimage = string.Empty;

            switch (sessionManager.FindAMode)
            {
                case FindAMode.Bus:
                    modeimage = GetResource("HomePlanAJourney.imageFindBus.ImageUrl");
                    break;
                case FindAMode.Car:
                    modeimage = GetResource("HomeDefault.imageFindCar.ImageUrl");
                    break;
                case FindAMode.CarPark:
                    modeimage = GetResource("HomeDefault.imageFindCarPark.ImageUrl");
                    break;
                case FindAMode.Coach:
                    modeimage = GetResource("HomeDefault.imageFindCoach.ImageUrl");
                    break;
                case FindAMode.Flight:
                    modeimage = GetResource("HomeDefault.imageFindFlight.ImageUrl");
                    break;
                case FindAMode.RailCost:
                case FindAMode.Train:
                    modeimage = GetResource("HomeDefault.imageFindTrain.ImageUrl");
                    break;
                default:
                    modeimage = GetResource("PlanAJourneyControl.imageDoorToDoor.ImageUrl");
                    break;

            }

            return modeimage;
        }

        /// <summary>
        /// Event handler that responds to the Social Link control's email link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailLink_Click(object sender, EventArgs e)
        {
            if (AmendSaveSendControl.IsLoggedIn())
            {
                AmendSaveSendControl.SetActiveTab(AmendPanelMode.SendEmailNormal);
            }
            else
            {
                AmendSaveSendControl.SetActiveTab(AmendPanelMode.SendEmailLogin);
            }
            AmendSaveSendControl.Focus();


            string amendSaveSendControlFocusScript = @"<script>  function ScrollView() { var el = document.getElementById('" + AmendSaveSendControl.SendEmailTabButton.ClientID
                                              + @"'); if (el != null){ el.scrollIntoView(); el.focus();}} window.onload = ScrollView;</script>";

            this.ClientScript.RegisterClientScriptBlock(this.GetType(), "CtrlFocus", amendSaveSendControlFocusScript);

        }






		/// <summary>
		/// Stores the selected tickets in the session and redirects display to the ticket retailers page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonRetailers_Click(object sender, EventArgs e)
		{			
			// Add Tracking Parameter when buy button click
            trackingHelper.AddTrackingParameter(this.pageId.ToString(), "buttonRetailers", TrackingControlHelper.CLICK);
						
			//only set to error if no tickets selected across both outward and return
			bool noTicketsSelected = !AreTicketsSelected();

			if (noTicketsSelected)
			{
				OutboundJourneyFaresControl.InErrorMode = true;
				ReturnJourneyFaresControl.InErrorMode = true;
			}
			else
				//redirect page to Ticket Retailers and validation				
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoTicketRetailers;
		}

		/// <summary>
		/// Handler for the OK button click event on the CostPageTitleControl, in CarJourneyCostsControl.
		/// Event is handled here in order to correctly set the total cost displayed for the whole 
		/// journey (inc. return portion if this exists).  If the 'total costs' option has
		/// been selected, running costs should be included in the calculation.  The visiblity of the 
		/// running and fuel cost controls is appropriate event handler in CarJourneyItemisedCostControl.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CostPageTitle_OutwardOKButtonClick(object sender, EventArgs e)
		{
			// Update the stuff for the return control
			TDItineraryManager.Current.JourneyViewState.ShowRunning = outwardCarJourneyCostsControl.ItemisedCarCostsControl.CostPageTitleControl.ShowCostTypeControl.ShowSelectedCost == 1;

			//Update showRunning property in CarJourneyItemisedCost control
			outwardCarJourneyCostsControl.ItemisedCarCostsControl.ShowRunning = (outwardCarJourneyCostsControl.ItemisedCarCostsControl.CostPageTitleControl.ShowCostTypeControl.ShowSelectedCost == 1);
			returnCarJourneyCostsControl.ItemisedCarCostsControl.ShowRunning = (outwardCarJourneyCostsControl.ItemisedCarCostsControl.CostPageTitleControl.ShowCostTypeControl.ShowSelectedCost == 1);

			//Call calculate costs method of control here
			CalculateCarCosts();
		}

		/// <summary>
		/// Handler for the OK button click event on the CostPageTitleControl, in CarJourneyCostsControl.
		/// Event is handled here in order to correctly set the total cost displayed for the whole 
		/// journey (inc. return portion if this exists).  If the 'total costs' option has
		/// been selected, running costs should be included in the calculation.  The visiblity of the 
		/// running and fuel cost controls is appropriate event handler in CarJourneyItemisedCostControl.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CostPageTitle_ReturnOKButtonClick(object sender, EventArgs e)
		{
			// set all the stuff for the return control
			TDItineraryManager.Current.JourneyViewState.ShowRunning = returnCarJourneyCostsControl.ItemisedCarCostsControl.CostPageTitleControl.ShowCostTypeControl.ShowSelectedCost == 1;

			//Update showRunning property in CarJourneyItemisedCost control
			outwardCarJourneyCostsControl.ItemisedCarCostsControl.ShowRunning = (returnCarJourneyCostsControl.ItemisedCarCostsControl.CostPageTitleControl.ShowCostTypeControl.ShowSelectedCost == 1);
			returnCarJourneyCostsControl.ItemisedCarCostsControl.ShowRunning = (returnCarJourneyCostsControl.ItemisedCarCostsControl.CostPageTitleControl.ShowCostTypeControl.ShowSelectedCost == 1);

			CalculateCarCosts();
		}

		/// <summary>
		/// Handler for when the user changes the current selected journey on either
		/// the main summary control or (when extending a journey) the itinerary
		/// control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void SummaryItineraryControl_Click(object sender, EventArgs e)
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			SetupCarCosts();
			
			CalculateCarCosts();
			//IR2544 Check if the user is trying to view the full itinerary.
			//If yes, move them back to the JourneySummary page.

			if (itineraryManager.FullItinerarySelected)
			{
				sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneySummary;
			}
		}

        /// <summary>
        /// Handler for find summary result table control selection changed event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindSummaryResultTableControl_Click(object sender, EventArgs e)
        {
            // do nothing
        }

        /// <summary>
        /// Handle button click to redirect user to journey overview page 
        /// </summary>
        /// <param name="sender">Originator of event</param>
        /// <param name="e">Event parameters</param>
        protected void buttonBackJourneyOverview_Click(object sender, EventArgs e)
        {
            ITDSessionManager sessionManager = TDSessionManager.Current;
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyOverview;
        }
		#endregion

		#region Extend Journey 
		/// <summary>
		/// Establish what mode the Itinerary Manager is in and whether we have any Return results
		/// </summary>
		private void DetermineStateOfResults()
		{
			itineraryManager = TDItineraryManager.Current;

			itineraryExists = (itineraryManager.Length > 0);
			extendInProgress = itineraryManager.ExtendInProgress;
			showItinerary = (itineraryExists && !extendInProgress);
			showFindA = (!showItinerary && (sessionManager.IsFindAMode));

			if ( showItinerary )
			{
				outwardExists = (itineraryManager.OutwardLength > 0);
				returnExists = (itineraryManager.ReturnLength > 0);
			}
			else
			{
				//check for normal result
				ITDJourneyResult result = sessionManager.JourneyResult;
				if(result != null) 
				{
					outwardExists = ((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid;
					returnExists = ((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid;

					// Get time types for journey.
					outwardArriveBefore = sessionManager.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = sessionManager.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}
		}

		/// <summary>
		/// Set the visibility and data sources for the controls
		/// </summary>
		private void SetupControls()
		{
			// a) 1st display: extend journey button visible. Journey results shown.
			// b) 2nd display: user pressed extend journey (itineraryExists = true). Itinerary shown.
			// c) 3rd display: user entered extension details and obtained search results - undo visible (extendInProgress = true). Extension results shown.
			// d) 4th display: user has added extension results to itinerary - all extension buttons visible (next button pressed). Itinerary shown.

			#region Initialise controls

			if ( showItinerary )
			{
				if (outwardExists)
				{
					summaryOutwardTable.Attributes.Add("class", "jpsumout");
				}

				if (returnExists)
				{
					// The itinerary table for return extensions has no radio buttons on right hand
					// side of table, hence use appropriate styles to not render cell with blue background
					returnPanel.CssClass = "boxtypeeighteenitineraryreturn";
					faresReturnTable.CssClass = "jpsumrtnitinerary";
				}
			}
			else if (showFindA)
			{
                TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
                if (TDSessionManager.Current.FindPageState != null)
                    modeTypes = TDSessionManager.Current.FindPageState.ModeType;

				if (outwardExists)
				{
					findSummaryResultTableControlOutward.Initialise(false, true, outwardArriveBefore, modeTypes);
					summaryOutwardTable.Attributes.Add("class", "jpsumoutfinda");
				}
				
				if (returnExists)
				{
					returnPanel.CssClass = "boxtypeeighteen";
					faresReturnTable.CssClass = "jpsumrtnfinda";
					findSummaryResultTableControlReturn.Initialise(false, false, returnArriveBefore, modeTypes);
				}
			}
			else 
			{
				if (outwardExists)
				{
					summaryResultTableControlOutward.Initialise(false, true, outwardArriveBefore);
					summaryOutwardTable.Attributes.Add("class", "jpsumout");
				}
				
				if (returnExists)
				{
					returnPanel.CssClass = "boxtypeeighteen";
					faresReturnTable.CssClass = "jpsumrtn";
					summaryResultTableControlReturn.Initialise(false, false, returnArriveBefore);
				}
			}

			AmendSaveSendControl.Initialise( this.pageId);

			this.theOutputNavigationControl.Initialise(pageId);

			// Set up correct mode for footnotes control
			if ( sessionManager.IsFindAMode )
			{
				if ( FindFareInputAdapter.IsCostBasedSearchMode(sessionManager.FindAMode) )
				{
					footnotesControl.CostBasedResults = true;
				}
				footnotesControl.Mode = sessionManager.FindAMode;
			}

			#endregion Initialise controls

           

			#region Set visibility of controls


			// Hide footnotes if no journey results
			if (!outwardExists && !returnExists)
				footnotesControl.Visible = false;

			if (outwardExists)
			{
				summaryResultTableControlOutward.Visible = !showFindA;
				findSummaryResultTableControlOutward.Visible = showFindA;
			}

			if(returnExists)
			{
				summaryResultTableControlReturn.Visible = !showFindA;
				findSummaryResultTableControlReturn.Visible = showFindA;
			}

			//Added as part of fix for Vantive 3918704
			if (!outwardExists)
			{
				// Outward results DO NOT exist. Set visibility of all return controls to false.
				SetOutwardVisible(false);
			}

			if (!returnExists)
			{
				// Return results DO NOT exist. Set visibility of all return controls to false.
				SetReturnVisible(false);
			}

			sessionManager.InputPageState.MapUrlOutward = String.Empty;
			sessionManager.InputPageState.MapUrlReturn  = String.Empty;

            SetJourneyOverviewBackButton();

			#endregion Set visibility of controls
		}
		#endregion


		private void OutboundJourneyFaresControl_OtherFaresClicked(object sender, EventArgs e)
		{

			PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;

			if(options.OverrideItineraryType == ItineraryType.Single)
			{
				options.OverrideItineraryType = ItineraryType.Return;

			}
			else
			{
				options.OverrideItineraryType = ItineraryType.Single;
 
			}
			
		}

		private void ViewOtherFareLink_link_Clicked(object sender, EventArgs e)
		{
			PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;

			if(options.OverrideItineraryType == ItineraryType.Single)
			{
				options.OverrideItineraryType = ItineraryType.Return;

			}
			else
			{
				options.OverrideItineraryType = ItineraryType.Single;
 
			}

		}
	}
}



