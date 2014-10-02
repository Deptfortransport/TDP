// *********************************************** 
// NAME				 : JourneyDetails.aspx.cs
// AUTHOR			   : Kenny Cheung
// DATE CREATED		 : 05/09/2003
// DESCRIPTION		  : Journey Details page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyDetails.aspx.cs-arc  $
//
//   Rev 1.54   Feb 08 2013 09:29:46   mmodi
//Check for null accessible prefs for flight journeys
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.53   Dec 05 2012 13:39:14   mmodi
//Display accessible journey errors
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.52   Aug 17 2012 10:55:34   dlane
//Cycle walk links
//Resolution for 5827: CCN Cycle Walk links
//
//   Rev 1.51   Oct 04 2011 15:44:38   mmodi
//Updated to set the road journey allow replan flag when a replan fails (due to validation and hence not submitted to cjp manager)
//Resolution for 5748: Real Time in Car - Replan is button displayed when no journeys can be planned
//
//   Rev 1.50   Sep 21 2011 09:53:24   mmodi
//Corrected to show last car journey planned when no more routes using the replan to avoid incidents
//Resolution for 5739: Real Time In Car - Failed journey Replan does not display last journey
//
//   Rev 1.49   Sep 06 2011 12:13:48   apatel
//Updated for Real Time Car following code review
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.48   Sep 02 2011 10:22:12   apatel
//Real time car changes
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.47   Sep 01 2011 10:44:46   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.46   Aug 01 2011 14:51:08   apatel
//Updated to add DaysToDeparture Intellitracker tags
//Resolution for 5718: DaysToDeparture tracking tag not get sent to Intellitracker
//
//   Rev 1.45   Jun 10 2011 14:05:18   PScott
//Add logic for Open Returns and Anytime tags
//Resolution for 5703: Intellitracker date and time tags
//
//   Rev 1.44   Jun 09 2011 10:55:36   PScott
//IR 5703 Intellitracker date and time tags
//
//Resolution for 5703: Intellitracker date and time tags
//
//   Rev 1.43   Apr 08 2010 13:23:10   apatel
//Updated to resolve html validator issues related to intellitracker
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.42   Mar 29 2010 16:39:48   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.41   Mar 19 2010 10:50:36   apatel
//Added custom related links for city to city result pages
//Resolution for 5468: Related link in city to city incorrect
//
//   Rev 1.40   Mar 05 2010 10:00:36   apatel
//Updated for customised intellitracker tags
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.39   Mar 04 2010 15:38:18   mmodi
//Updated emissions control flag to show units switch dropdown
//Resolution for 5433: TD Extra - The miles/km control displayed the bottom of the page on the CO2 comparison page.
//
//   Rev 1.38   Mar 04 2010 14:50:20   apatel
//Updated to add customised intellitracker parameters
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.37   Mar 04 2010 11:20:32   mmodi
//Corrected showing of Map button and CO2 button for non-international planner mode
//Resolution for 5430: TD Extra - No show map button on the car journey details page
//
//   Rev 1.36   Feb 25 2010 17:51:16   mmodi
//Don't show Help button for international mode
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.35   Feb 23 2010 15:30:18   mmodi
//Updates for displaying the Emissions control
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.34   Feb 17 2010 15:13:52   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.33   Feb 17 2010 09:08:42   RBroddle
//International planner corrections
//
//   Rev 1.32   Feb 16 2010 11:16:10   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.31   Feb 16 2010 10:33:30   RBroddle
//Added JourneyEmissionsCompareControl for display for international journeys
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.30   Feb 12 2010 12:55:38   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.29   Feb 12 2010 11:14:02   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.28   Jan 05 2010 15:12:44   apatel
//updated DisplayBankHolidayMessage method
//Resolution for 5354: Holiday error message - Journey Details page issue
//
//   Rev 1.27   Dec 18 2009 15:28:56   apatel
//removed for the return journey the check to see if the returnmap is already visible
//Resolution for 5351: JourneyDetails - Retrun map and printerfriendly page issue
//
//   Rev 1.26   Dec 02 2009 12:19:14   mmodi
//Updated to display map direction number link on Car journey details table
//
//   Rev 1.25   Nov 29 2009 12:43:38   mmodi
//Updated map initialise to hide the show journey buttons, and ensure only  one map is shown
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.24   Nov 20 2009 09:27:58   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.23   Nov 16 2009 15:30:12   mmodi
//Updated details table view for mapping
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.22   Nov 15 2009 18:17:36   mmodi
//Updated styles to make results pages look consistent
//
//   Rev 1.21   Nov 15 2009 11:09:52   mmodi
//Updated to pass map control details through to the journey detail segments control
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.20   Nov 11 2009 21:11:18   mmodi
//Updated to use new MapJourneyControl
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.19   Nov 11 2009 16:43:14   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.18   Oct 28 2009 15:30:54   apatel
//Seasonal page link change 
//
//   Rev 1.17   Oct 15 2009 13:38:06   apatel
//Seasonal page link and next day journeys changes
//
//   Rev 1.16   Oct 01 2009 16:25:18   apatel
//Updates for Social Bookmark links
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.15   Sep 30 2009 11:20:12   apatel
//Social Bookmarking changes - updated link for email
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.14   Sep 30 2009 09:06:50   apatel
//CCN 530 Social Bookmarking code changes
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.13   Sep 28 2009 08:40:00   PScott
//CCN 530 SCR 5305  add social bookmarkcontrol
//
//   Rev 1.12   Sep 15 2009 14:36:50   apatel
//back button functionality for stop information page
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.11   Nov 12 2008 08:49:34   pscott
//SCR 5121 CCN471
//Further error message changes 
//
//   Rev 1.10   Nov 05 2008 12:24:48   pscott
//add message mod for cases where only PT
//
//   Rev 1.9   Oct 14 2008 14:35:12   mmodi
//Manual merge for stream5014
//
//   Rev 1.8   Oct 07 2008 15:39:10   PScott
//scr 5121 CCN 471 USD c1182414
//Link to find nearest for PT journeys that fail journey request
//
//   Rev 1.7   Jul 02 2008 17:47:42   rbroddle
//Corrected display of skip links depending on journey type (pt/car) shown
//Resolution for 4891: Summary of "Find Flight" options on results screen looks cluttered
//Resolution for 5016: WAI WCAG level A compliance faults - Missing Alt text
//
//   Rev 1.6   Jun 20 2008 13:31:26   pscott
//SCR 5026 USD 2720245 - errormessages and screen display incorrect when outward journey has failed but the return journey hasn't.   Converse of this also applies.
//
//   Rev 1.5.1.3   Oct 07 2008 11:46:44   jfrank
//Update for XHTML compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.5.1.1   Sep 19 2008 09:52:48   mmodi
//Hide Outward panel, navigation, and title when no journeys found
//Resolution for 5116: Inappropriate Details Text and Header bar when no results
//
//   Rev 1.5.1.0   Sep 15 2008 09:31:48   jfrank
//Changed code which determines whether the buttons for out and return should say 'show map' or 'hide map'.
//Resolution for 5078: Return journey "Show Map" button text can change to "Hide Map" incorrectly
//
//   Rev 1.5   May 07 2008 14:50:22   apatel
//made tiltle "Journey Found For" to look in table view when findamode is flight.
//
//   Rev 1.4   Apr 07 2008 16:08:30   scraddock
//Plan to Park and Ride results map: symbols crash into details section.
//Resolution for 4840: Plan to Park and Ride results map: symbols crash into details section.
//
// Rev DevFactory Apr 7 2008 sbarker
// Removed the possibility of duplicate stylesheets
//
//   Rev 1.3   Apr 04 2008 09:00:06   apatel
//modified page load event to display errors when there is no journey result
//
//   Rev 1.2   Mar 31 2008 13:24:46   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Mar 27 2008 15:40:00 apatel
//  added styles for the park and ride page through code.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//Updated to pass mode type to FindSummaryResultControl selected on 
//JourneyOverview page in City to City journeys. ModeType value obtained from Session.FindPageState.ModeType
//
//   Rev 1.2   Nov 29 2007 15:09:48   mturner
//Changes to remove compiler warnings after Del 9.8 merge.
//
//   Rev 1.1   Nov 29 2007 11:08:38   mturner
//Updated for Del 9.8
//
//   Rev 1.128   Oct 17 2007 13:30:38   pscott
//UK:1361875  IR 4515  change instances of Km to km
//
//   Rev 1.127   Sep 07 2007 18:52:16   asinclair
//Removed Check CO2 button
//
//   Rev 1.126   Jun 28 2007 14:29:34   mmodi
//Code added to pass journey parameters in to the CarAllDetailsControl
//Resolution for 4458: DEL 9.7 - Car journey details
//
//   Rev 1.125   May 22 2007 11:20:36   mmodi
//Enabled Emissions button when a Car journey is selected
//Resolution for 4420: CO2: Display CO2 button on Car details page
//
//   Rev 1.124   Mar 06 2007 12:30:06   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.123.1.0   Feb 16 2007 11:20:32   mmodi
//Added Compare Emissions button
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.123   Nov 08 2006 11:07:26   mmodi
//Added event to save the MapViewState
//Resolution for 4225: Car Parking: Return from Car Park Information page shows the wrong journey map
//
//   Rev 1.122   Oct 06 2006 16:39:58   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.121.1.0   Sep 30 2006 12:40:04   mmodi
//Removed events for if a map button is clicked on the Car journey details table view to resolve IR. Map buttons are not shown in Car details view therefore this has no adverse effect.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4205: Car Parking: Navigation issue for Car Park results with map displayed
//
//   Rev 1.121   Jun 14 2006 12:04:20   esevern
//Code fix for vantive 3918704 - Enable buttons when no outward journey is returned but a return journey is. Changed code to remove assumption that there is always an outward journey.
//Resolution for 3686: Buttons disabled when return journey returned and outward not
//
//   Rev 1.120   Mar 28 2006 09:56:48   pcross
//Removed adjust return button. Re-pop hyperlinks on postback
//Resolution for 3728: Adjust button Journey Details
//
//   Rev 1.119   Mar 20 2006 18:13:42   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.118   Mar 14 2006 19:50:12   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.115   Feb 23 2006 18:03:40   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.114   Feb 10 2006 10:48:02   jmcallister
//Manual Merge of Homepage 2. IR3180
//
//   Rev 1.113   Dec 13 2005 11:23:34   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.112.4.1   Mar 13 2006 16:07:58   tmollart
//Removed references to SummaryItineraryTableControl.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.112.4.0   Mar 09 2006 08:44:54   asinclair
//Removed all access to Extend Functionality
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.112.1.0   Nov 23 2005 11:25:52   jgeorge
//Added client links functionality
//Resolution for 3144: DEL 8 stream: Client Links Development
//
//   Rev 1.112   Nov 21 2005 17:26:42   ralonso
//Add button fixed to read Add extension to journey
//Resolution for 3163: UEE: Button text incorrect on Extensions results screen - "Add"
//
//   Rev 1.111   Nov 15 2005 11:07:04   AViitanen
//Changed to display text in Adjust button
//Resolution for 3045: UEE: Door to Door - Untitled button for Adjust journey
//
//   Rev 1.110   Nov 03 2005 16:17:12   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.109.1.7   Nov 02 2005 16:00:24   RGriffith
//Removed Tool Tips from TDButtons
//
//   Rev 1.109.1.6   Oct 25 2005 14:37:10   ralonso
//Changes to accomodate AmendViewControl changes
//
//   Rev 1.109.1.3   Oct 21 2005 16:11:18   ralonso
//TD089 ES020 image button replacement
//
//   Rev 1.109.1.2   Oct 12 2005 14:12:20   ralonso
//TD089 ES020 image button replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.109.1.1   Oct 11 2005 12:22:12   rhopkins
//TD089 ES020 image button replacement.  Correction for Printer Friendly Button.
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.109.1.0   Oct 11 2005 12:03:18   ralonso
//TD089 ES020 image button replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.109   Sep 29 2005 10:43:18   schand
//Merged stream 2673 back into trunk
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.108   Sep 26 2005 15:04:56   CRees
//Reinstated some lines relating to journey units on primary print button deleted during removal of second print button in stream 2556
//
//   Rev 1.107   Aug 17 2005 13:56:32   RWilby
//Merge Fix for stream2556
//
//   Rev 1.106   Aug 16 2005 14:38:06   RWilby
//Merge for stream2556
//
//   Rev 1.105   Aug 04 2005 13:06:32   asinclair
//Clear array list in Page_Load
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Charge for return journeys
//
//   Rev 1.104   Aug 03 2005 21:08:12   asinclair
//fix for 2639
//Resolution for 2639: DEl 7: Car Costing: Do not display Congestion Carge for return journeys
//
//   Rev 1.103   May 23 2005 17:20:04   rgreenwood
//IR2500: Added FindAFare back button
//Resolution for 2500: PT - Back Button Missing in Find-a-Fare
//
//   Rev 1.102   May 20 2005 09:43:12   rgeraghty
//Replaced printer hyperlinks with printerfriendlypagebuttoncontrol
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.101   May 03 2005 16:05:30   pcross
//IR2400. Return journey skip link text was dependent on outward journey property.
//Resolution for 2400: Journey Details 'skip to return' button text dependent on outward button state
//
//   Rev 1.100   Apr 28 2005 12:33:16   pcross
//Removed SetPrinterMapVisible (to be run inline instead) as it now applies to screen as well as printer.
//Associated with Richard Hopkins' last change to show maps in printer page
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.99   Apr 28 2005 12:02:06   pcross
//Minor sip link updates
//
//   Rev 1.98   Apr 27 2005 20:23:46   rhopkins
//Corrected handling of showing / hiding maps.
//Resolution for 2361: Maps on Details page not always shown when requested
//
//   Rev 1.97   Apr 26 2005 13:20:36   pcross
//IR2192. Corrections to extended journey handling
//
//   Rev 1.96   Apr 25 2005 19:25:30   asinclair
//Fix for IR 1983
//
//   Rev 1.95   Apr 22 2005 16:17:14   pcross
//IR2192. Screen now allows maps to be viewed for public transport journeys as well as car journeys.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.94   Apr 20 2005 12:10:40   COwczarek
//Itinerary manager instance variable reassigned in pre-render
//event handler since partition may have been switched by user.
//PlannerOutputAdapter instantiated when needed to ensure it
//uses itinerary manager from correct partition.
//Resolution for 2079: PT - Extend journey does not work with PT cost based searches
//
//   Rev 1.93   Apr 15 2005 13:00:04   pcross
//Skip links correction
//
//   Rev 1.92   Apr 12 2005 18:34:32   pcross
//Changed the way that the skip links image URL is accessed (now from langStrings, not HTML)
//
//   Rev 1.91   Apr 12 2005 14:43:22   rgeraghty
//OnPreRender code changed to call InitialiseJourneyComponents
//Resolution for 2101: Details page not displaying selected result when moving through the result list
//
//   Rev 1.90   Apr 12 2005 12:48:42   pcross
//Skip link corrections plus new links for Extend a Journey mode
//
//   Rev 1.89   Apr 12 2005 10:59:20   bflenk
//Work in Progress - IR 1986
//
//   Rev 1.88   Apr 08 2005 16:00:34   rhopkins
//Added FindFareSelectedTicketLabelControl and AmendViewControl
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.87   Apr 05 2005 14:38:26   asinclair
//Setting the PrinterFriendly page's URL params in OnPreRender
//
//   Rev 1.86   Mar 30 2005 19:01:58   asinclair
//Fixed error with Map drop down selection
//
//   Rev 1.85   Mar 21 2005 16:05:20   pcross
//Added screenreader and alt text
//
//   Rev 1.84   Mar 18 2005 15:11:04   asinclair
//Updated to display car section maps
//
//   Rev 1.83   Mar 01 2005 16:29:12   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.82   Mar 01 2005 15:30:54   asinclair
//Updated for Del 7 Car Costing
//
//   Rev 1.81   Jan 20 2005 10:22:40   asinclair
//Work in progress - Del 7 Car costing
//
//   Rev 1.80   Nov 23 2004 17:33:22   SWillcock
//Modified logic for displaying journey numbers after 'Outward journey' and 'return journey' text
//Resolution for 1781: Remove Journey Numbers from Details and Map Display in Quick Planners
//
//   Rev 1.79   Sep 28 2004 11:38:52   esevern
//removed commented out code
//
//   Rev 1.78   Sep 21 2004 16:38:30   esevern
//IR1581 - amended outward and return journey label for find a car (there will only be one journey)
//
//   Rev 1.77   Sep 21 2004 11:09:50   passuied
//Added extra check for FindA Mode in imageButtonAdjustJourneyDetails out and ret visibility setting
//Resolution for 1610: FindA : Remove Adjust journey button for results details
//
//   Rev 1.76   Sep 20 2004 16:45:22   COwczarek
//Page title now displayed using JourneyPlannerOutputTitleControl.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary
//
//   Rev 1.75   Sep 19 2004 15:04:18   jbroome
//IR1391 - visibility of Add Extension to Journey button if incomplete results returned.
//
//   Rev 1.74   Sep 17 2004 15:14:24   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.73   Aug 31 2004 15:06:40   RHopkins
//IR1440 Rationalised the code for determining the state of the result data.  This improved code has been duplicated across all of the results pages to reduce the burden of future maintenance.
//
//   Rev 1.72   Aug 26 2004 12:53:20   RHopkins
//Corrected some visibility/initialisation errors that were spotted whilst fixing IR1443.  They weren't causing any operational problems but could have made future maintenance more difficult because the code was more confusing than necessary.
//
//   Rev 1.71   Aug 23 2004 11:08:42   jgeorge
//IR1319
//
//   Rev 1.70   Aug 17 2004 11:15:56   rgreenwood
//IR1286 - Check TDItineraryManager for Journey Data
//
//   Rev 1.69   Aug 10 2004 15:45:00   JHaydock
//Update of display of correct header control for help pages.
//
//   Rev 1.68   Aug 10 2004 11:59:04   RHopkins
//IR1291  Hyperlink for "return" anchor is now only displayed when return results are present.
//
//   Rev 1.67   Aug 06 2004 18:02:40   RHopkins
//IR1141 The flag that indicates whether we are displaying details in a table or in a diagram is now stored in the control's viewstate so that the appropriate state can be restored after the User uses the Back button.  This enables the buttons contained in the controls to trap their click events correctly.
//
//   Rev 1.66   Jul 30 2004 11:44:16   RHopkins
//IR1113 The "Amend date/time" anchor link now varies its text depending upon whether the AmendSaveSend control is displaying "Amend date and time" or "Amend stopover time" or not displaying either.
//
//   Rev 1.65   Jul 22 2004 14:16:12   jgeorge
//Updates for Find a...
//
//   Rev 1.64   Jun 23 2004 15:34:14   JHaydock
//Updates/Corrections for JourneyDetails page
//
//   Rev 1.63   Jun 22 2004 12:20:18   JHaydock
//FindMap page done. Corrections to printable map controls and pages. Various updates to Find pages.
//
//   Rev 1.62   Jun 16 2004 16:42:36   COwczarek
//No longer pass selected journey to initialise methods of summary controls
//Resolution for 867: Add extend journey functionality to summary and details pages
//
//   Rev 1.61   Jun 16 2004 15:17:46   COwczarek
//Initial implementation of extend journey functionality
//Resolution for 867: Add extend journey functionality to summary and details pages
//
//   Rev 1.60   Apr 02 2004 10:16:24   AWindley
//DEL 5.2 QA Changes: Resolution for 692
//
//   Rev 1.59   Mar 23 2004 14:04:36   CHosegood
//Del 5.2 QA Journey Details changes.
//Resolution for 668: Del 5.2 QA Journey Details changes.
//
//   Rev 1.58   Mar 09 2004 18:35:40   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.57   Mar 01 2004 13:46:54   asinclair
//Removed Printer hyperlink as not needed in Del 5.2
//
//   Rev 1.56   Feb 19 2004 17:36:08   COwczarek
//Changes for DEL 5.2 display format
//Resolution for 629: Frequency based Journeys
//
//   Rev 1.55   Jan 14 2004 12:05:02   RPhilpott
//Clear MapUrlOutward and MapUrlReturn from session in initialisation so that they are not erroneously picked by "send to a friend" facility.
//Resolution for 598: Wrong map attached to emails "sent to a friend".
//
//   Rev 1.54   Dec 04 2003 18:37:58   kcheung
//Del 5.1 updates
//
//   Rev 1.53   Dec 01 2003 14:52:30   kcheung
//Added label for unadjusted selection.
//Resolution for 460: In the traffic map for a car journey, the adjusted route should be default
//
//   Rev 1.52   Nov 26 2003 10:41:00   kcheung
//Added extra button - Adjust Journey Details greyed out.
//Resolution for 388: QA: JP Result Details Page button not greyed out
//
//   Rev 1.51   Nov 25 2003 18:25:22   COwczarek
//SCR#145: CSS style does not appear correctly in Mozilla browsers
//Resolution for 145: CSS style does not appear correctly in Mozilla browsers
//
//   Rev 1.50   Nov 21 2003 09:45:34   kcheung
//Updated for the return requested but no outward journey found case.
//
//   Rev 1.49   Nov 20 2003 09:35:58   kcheung
//Fixed return car not appearing due to property switch,
//
//   Rev 1.48   Nov 19 2003 12:49:40   kcheung
//Updated to use properties of journey result and viewstate
//Resolution for 132: Determing whether Journey is return or not
//Resolution for 136: Properties of JourneyViewState to determine the selected outward and return journeys
//
//   Rev 1.47   Nov 17 2003 15:57:04   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.46   Nov 13 2003 12:44:00   kcheung
//Updated to use the output navigation control
//Resolution for 149: Streamline code for result summary buttons
//
//   Rev 1.45   Nov 07 2003 11:29:16   PNorell
//Fixed n*mespace comment confusing NAnt.
//
//   Rev 1.44   Nov 05 2003 13:07:24   kcheung
//Added summary headers
//
//   Rev 1.43   Nov 05 2003 10:46:08   kcheung
//Inserted : as requested in QA
//
//   Rev 1.42   Nov 04 2003 13:53:46   kcheung
//Updated n*mespace to Web.Templates
//
//   Rev 1.41   Nov 03 2003 17:14:00   kcheung
//Updated so that ticket retailers and fares is disabled if cars is selected (as requested in QA)
//
//   Rev 1.40   Oct 28 2003 14:10:00   kcheung
//Cosmetic fixes for QA
//
//   Rev 1.39   Oct 28 2003 10:19:26   CHosegood
//Added Fares button event handler
//
//   Rev 1.38   Oct 27 2003 18:40:28   COwczarek
//Add event handler to redirect to ticket retailers page
//
//   Rev 1.37   Oct 20 2003 12:51:12   kcheung
//Renamed variables to comply with FXCOP
//
//   Rev 1.36   Oct 16 2003 11:21:22   kcheung
//Fixed so that help for the summary does not appear - wireframes were incorrect
//
//   Rev 1.35   Oct 15 2003 19:12:28   kcheung
//Fixed help label for car summary so that it appears properly on the details page rather than inside the DIV
//
//   Rev 1.34   Oct 15 2003 14:40:04   kcheung
//Removed redundant <p> tags
//
//   Rev 1.33   Oct 15 2003 13:25:06   kcheung
//Fixed HTML
//
//   Rev 1.32   Oct 15 2003 13:13:14   kcheung
//Fixed html
//
//   Rev 1.31   Oct 14 2003 18:59:48   kcheung
//Fixed so that return journey link is visible only if return journeys exist
//
//   Rev 1.30   Oct 13 2003 10:58:18   kcheung
//Fixed alt texts
//
//   Rev 1.29   Oct 10 2003 17:45:00   kcheung
//Updated alt text
//
//   Rev 1.28   Oct 10 2003 10:04:34   kcheung
//Fixed so page title is read from langstrings
//
//   Rev 1.27   Oct 08 2003 13:22:06   kcheung
//Fixed labelCarAdjusted label appearing for Unadjusted bug
//
//   Rev 1.26   Oct 08 2003 10:37:06   PNorell
//Removed exceptions and errors when no journey results where found.
//
//   Rev 1.25   Oct 08 2003 10:18:04   PNorell
//Removed exception throwing if a result contains no journeys.
//
//   Rev 1.24   Oct 03 2003 14:39:30   PNorell
//Fixed null reference exception if session was empty.
//
//   Rev 1.23   Oct 03 2003 14:20:18   kcheung
//Uncommented call to InputPageState.Initialise()
//
//   Rev 1.22   Oct 01 2003 16:13:10   kcheung
//Added commented out code to call Initialise of InputPageState
//
//   Rev 1.21   Sep 29 2003 16:25:46   kcheung
//Updated html styling.. integrated with printer friendly
//
//   Rev 1.20   Sep 26 2003 15:00:44   PNorell
//Minor HTML integration.
//
//   Rev 1.19   Sep 26 2003 11:53:26   kcheung
//Fixed no return css bug
//
//   Rev 1.18   Sep 25 2003 18:06:32   PNorell
//Ensured everything is linked up together.
//Fixed various small bugs.
//
//   Rev 1.17   Sep 25 2003 13:05:20   kcheung
//Integrated HTML for Car Details Control
//
//   Rev 1.16   Sep 25 2003 11:46:16   PNorell
//Fixed the compare control to have footer and small bugs in the handling of the adjusted journey.
//
//   Rev 1.15   Sep 22 2003 18:57:30   PNorell
//Updated all transition events and page ids and interaction events.
//
//   Rev 1.14   Sep 22 2003 17:20:44   PNorell
//Integrated help controls and associated resources.
//Fixed bug in event handling in JourneyDetails.
//
//   Rev 1.13   Sep 21 2003 17:55:54   kcheung
//Updated
//
//   Rev 1.12   Sep 21 2003 14:46:34   kcheung
//Added initialise call for AmendSaveSend control
//
//   Rev 1.11   Sep 19 2003 19:58:10   PNorell
//Updated all journey details screens.
//Support for Adjusted journeys added and Validate And Run.
//
//   Rev 1.10   Sep 19 2003 09:20:58   kcheung
//Minor tweaks
//
//   Rev 1.9   Sep 18 2003 19:28:56   kcheung
//Updated buttons to drop down lists.
//
//   Rev 1.8   Sep 18 2003 18:53:46   kcheung
//Added event handlers for New/Amend
//
//   Rev 1.7   Sep 18 2003 16:00:06   kcheung
//Remove Formshift clear stuff
//
//   Rev 1.6   Sep 18 2003 11:04:12   PNorell
//Fixed reference to use interface instead of concrete implimentation.
//
//   Rev 1.5   Sep 18 2003 09:56:46   jcotton
//Changes for intitial screenflow integration work
//
//   Rev 1.4   Sep 15 2003 16:08:22   kcheung
//Updated
//
//   Rev 1.3   Sep 11 2003 17:14:34   kcheung
//Updated
//
//   Rev 1.2   Sep 11 2003 10:59:40   kcheung
//Updated
//
//   Rev 1.1   Sep 10 2003 15:25:30   kcheung
//Updated to inherit from TDPage
//
//   Rev 1.0   Sep 09 2003 15:32:22   kcheung
//Initial Revision
//
//   Rev 1.0   Sep 09 2003 15:16:50   kcheung
//Initial Revision

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Journey Details page
	/// </summary>
	public partial class JourneyDetails : TDPage
	{
		private const string summarySelected = "-1";
		
		// Web-user controls
		protected ResultsTableTitleControl resultsTableTitleControlOutward;
		protected ResultsTableTitleControl resultsTableTitleControlReturn;
		protected JourneysSearchedForControl journeysSearchedControl;
		protected SummaryResultTableControl summaryResultTableControlOutward;
		protected JourneyDetailsControl journeyDetailsControlOutward;
		protected SummaryResultTableControl summaryResultTableControlReturn;
		protected JourneyDetailsControl journeyDetailsControlReturn;
		protected AmendSaveSendControl amendSaveSendControl;
		protected JourneyDetailsTableControl journeyDetailsTableControlOutward;
		protected JourneyDetailsTableControl journeyDetailsTableControlReturn;
		protected FindSummaryResultControl findSummaryResultTableControlOutward;
		protected FindSummaryResultControl findSummaryResultTableControlReturn;
		protected HeaderControl headerControl;
		protected CarAllDetailsControl carAllDetailsControlOutward;
		protected CarAllDetailsControl carAllDetailsControlReturn;
		protected MapJourneyDisplayDetailsControl theMapJourneyDisplayDetailsControl;
		protected JourneyChangeSearchControl JourneyChangeSearchControl1;
		protected FindFareSelectedTicketLabelControl findFareSelectedTicketLabelControl;
		protected FindFareGotoTicketRetailerControl findFareGotoTicketRetailerControl;
		protected ResultsFootnotesControl footnotesControl;

        
		protected System.Web.UI.WebControls.ImageButton imageButtonAdjustedDetailsOutward;
		protected System.Web.UI.WebControls.ImageButton imageButtonUnadjustedDetailsOutward;
		protected System.Web.UI.WebControls.ImageButton imageButtonAdjustedDetailsReturn;
		protected System.Web.UI.WebControls.Label labelCarDirectionsUnadjustedReturn;
		protected System.Web.UI.WebControls.ImageButton imageButtonUnadjustedDetailsReturn;

		protected System.Web.UI.HtmlControls.HtmlGenericControl hyperlinkAmendSaveSend;
        protected OutputNavigationControl theOutputNavigationControl;

		protected System.Web.UI.WebControls.DropDownList dropDownListAdjustedDetailsOutward;
		protected System.Web.UI.WebControls.ImageButton imageButtonOKOutward;
		protected System.Web.UI.WebControls.Label labelCarAdjustedOutward;
		protected System.Web.UI.WebControls.Label labelCarUnadjustedOutward;
		protected System.Web.UI.WebControls.DropDownList dropDownListAdjustedDetailsReturn;
		protected System.Web.UI.WebControls.ImageButton imageButtonOKReturn;
		protected System.Web.UI.WebControls.Label labelCarAdjustedReturn;
		protected System.Web.UI.WebControls.Label labelCarUnadjustedReturn;
			
		protected TDJourneyViewState journeyViewState;
		private ITDSessionManager tdSessionManager;
		private TDItineraryManager itineraryManager;

        private TrackingControlHelper trackingHelper;

        private RoadJourneyHelper roadJourneyHelper;

		// State of results
		/// <summary>
		///  True if there is an outward trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool outwardExists = false;

		/// <summary>
		///  True if there is a return trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool returnExists = false;

		/// <summary>
		/// True if the Itinerary exists, containing the Initial journey and zero or more extensions
		/// </summary>
		private bool itineraryExists = false;

		/// <summary>
		/// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
		/// </summary>
		private bool extendInProgress = false;

		/// <summary>
		/// True if the Itinerary exists and there are no extensions in the process of being planned
		/// </summary>
		private bool showItinerary = false;

		/// <summary>
		/// True if the results have been planned using FindA
		/// </summary>
		private bool showFindA = false;

		private bool returnArriveBefore = false;
		private bool outwardArriveBefore = false;

		private const string TOID_PREFIX = "JourneyControl.ToidPrefix";
		protected System.Web.UI.WebControls.Label hyperLinkAmendDateTime;
		protected TDImage hyperLinkImageAmendDateTime;
		private const string MAP_ZOOM = "JourneyDetailsCarSection.Scale";

		/// <summary>
		/// Constructor - sets the page Id
		/// </summary>
		public JourneyDetails() : base()
		{
			pageId = PageId.JourneyDetails;
		}


		#region ViewState Code
		/// <summary>
		/// Loads the ViewState
		/// </summary>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					TDItineraryManager.Current.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode = (bool)myState[1];
				if (myState[2] != null)
					TDItineraryManager.Current.JourneyViewState.ShowReturnJourneyDetailsDiagramMode = (bool)myState[2];
			}
		}
	
		/// <summary>
		/// Overrides the base SaveViewState to customise viewstate behaviour.
		/// </summary>
		/// <returns>The ViewState object to be saved.</returns>
		protected override object SaveViewState()
		{ 
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();
		
			object[] allStates = new object[3];
			allStates[0] = baseState;
			allStates[1] = TDItineraryManager.Current.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode;
			allStates[2] = TDItineraryManager.Current.JourneyViewState.ShowReturnJourneyDetailsDiagramMode;

			return allStates;
		}
		#endregion ViewState Code


		#region Initialisation, Page_Load and OnPreRender
		/// <summary>
		/// Page Load Method
		/// </summary>
		private void Page_Load(object sender, System.EventArgs e)
		{
			tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;

            // Add Intellitracker tags for road replan journey clicked
            if (tdSessionManager.JourneyViewState.OutwardRoadReplanned)
            {
                AddReplanAvoidClosuresParam(false);
            }

            if (tdSessionManager.JourneyViewState.ReturnRoadReplanned)
            {
                AddReplanAvoidClosuresParam(true);
            }

            // Reset the journey view state fields
            tdSessionManager.JourneyViewState.OutwardRoadReplanned = false;
            tdSessionManager.JourneyViewState.ReturnRoadReplanned = false;

			tdSessionManager.JourneyViewState.CongestionChargeAdded = false;
			tdSessionManager.JourneyViewState.VisitedCongestionCompany.Clear();

			Initialise();

			InitialiseJourneyComponents();

            SetupControls();

            if (tdSessionManager.FindAMode == FindAMode.Flight)
                journeysSearchedControl.IsTableView = true;
            			
			// Initialise hyperlinks
			hyperLinkReturnJourneys.Text = GetResource("JourneyPlanner.hyperLinkReturnJourneys.Text");
			hyperLinkImageReturnJourneys.ImageUrl = GetResource("JourneyPlanner.hyperLinkImageReturnJourneys");
			hyperLinkImageReturnJourneys.AlternateText = GetResource("JourneyPlanner.hyperLinkImageReturnJourneys.AlternateText");
			hyperLinkOutwardJourneys.Text = GetResource("JourneyPlanner.hyperLinkOutwardJourneys.Text");
			hyperLinkImageOutwardJourneys.ImageUrl = GetResource("JourneyPlanner.hyperLinkImageOutwardJourneys");
			hyperLinkImageOutwardJourneys.AlternateText = GetResource("JourneyPlanner.hyperLinkImageOutwardJourneys.AlternateText");

            if (tdSessionManager.FindPageState != null)
                JourneyPlannerOutputTitleControl1.ModeTypes = tdSessionManager.FindPageState.ModeType;

            #region Result Errors

            errorMessagePanel.Visible = false;
            errorDisplayControl.Visible = false;


            if (!showItinerary)
            {
                ITDJourneyResult res = tdSessionManager.JourneyResult;
                ITDJourneyRequest req = tdSessionManager.JourneyRequest;
                if (res != null)
                {
                    ArrayList errorsList = new ArrayList();

                    foreach (CJPMessage mess in res.CJPMessages)
                    {
                        if (mess.Type == ErrorsType.Warning)
                        {
                            errorDisplayControl.Type = ErrorsDisplayType.Warning;
                        }

                        // Handle specific link display message
                        if (mess.MessageResourceId == JourneyControlConstants.CJPPartialReturnAmend
                         || mess.MessageResourceId == JourneyControlConstants.JourneyWebNoResults)
                        {
                            LandingPageHelper lpHelper = new LandingPageHelper();

                            // Load the current message that we are going to modify
                            string message = GetResource(mess.MessageResourceId);

                            #region Update Feedback page url in message
                            
                            // Replace the targetUrl place holder in the extended message with the actual URL
                            int pos = message.IndexOf("targetUrlContactPage");
                            // but only modify current message if we have a placeholder
                            if (pos > 0)
                            {
                                // Find the url for the feedback page 
                                string targetUrl = lpHelper.GetLandingPageUrl(PageId.FeedbackPage);

                                StringBuilder modifiedMessage = new StringBuilder();

                                modifiedMessage.Append(message, 0, pos);
                                modifiedMessage.Append(targetUrl);
                                modifiedMessage.Append(message, (pos + 20), message.Length - (pos + 20));

                                message = modifiedMessage.ToString();
                            }

                            #endregion

                            #region Add Find nearest station message if needed

                            //Check if the request and origin was for a station. If they were then don't add additional text
                            if (!(tdSessionManager.JourneyParameters.OriginLocation.SearchType == LocationService.SearchType.MainStationAirport
                                  && tdSessionManager.JourneyRequest.DestinationLocation.SearchType == LocationService.SearchType.MainStationAirport))
                            {
                                // find the extra bit of the message
                                string findNearestMessage = GetResource(JourneyControlConstants.CJPPartialReturnAmendFindNearest);
                                
                                // replace the targetUrl place holder in the extended message with the actual URL
                                pos = findNearestMessage.IndexOf("targetUrlFindNearest");
                                // but only modify current message if we have a placeholder
                                if (pos > 0)
                                {
                                    // Find the url for the FindNearestLandingPage 
                                    string targetUrl = lpHelper.GetLandingPageUrl(PageId.FindStationInput);
                                    
                                    StringBuilder modifiedMessage = new StringBuilder();

                                    modifiedMessage.Append(message);

                                    modifiedMessage.Append(findNearestMessage, 0, pos);
                                    modifiedMessage.Append(targetUrl);
                                    modifiedMessage.Append(findNearestMessage, (pos + 20), findNearestMessage.Length - (pos + 20));

                                    message = modifiedMessage.ToString();
                                }
                            }

                            #endregion

                            #region Add Accessible journey message if needed

                            //Check if the request and origin was for a station. If they were then don't add additional text
                            if (req != null && req.AccessiblePreferences != null && req.AccessiblePreferences.Accessible)
                            {
                                // Get accessible message
                                string accessibleMessage = GetResource(JourneyControlConstants.AccessibleJourneyNoResults);

                                if (!string.IsNullOrEmpty(accessibleMessage))
                                {
                                    message += accessibleMessage;
                                }
                            }

                            #endregion

                            #region Add Bank holiday message

                            string holidayMessage = DisplayBankHolidayMessage();

                            if (!string.IsNullOrEmpty(holidayMessage))
                            {
                                message += holidayMessage;
                            }

                            #endregion

                            mess.MessageText = message;
                        }

                        // If specific message not set, get from resource
                        string text = mess.MessageText;
                        if (text == null || text.Length == 0)
                        {
                            text = GetResource(mess.MessageResourceId);
                        }
                        errorsList.Add(text); 
                    }

                    errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));

                    errorDisplayControl.ReferenceNumber = tdSessionManager.JourneyResult.JourneyReferenceNumber.ToString();

                    if (errorDisplayControl.ErrorStrings.Length > 0)
                    {
                        errorMessagePanel.Visible = true;
                        errorDisplayControl.Visible = true;
                    }

                    // Clear the error messages in the result
                    res.ClearMessages();
                }

                // Show any Cost search result messages
                DisplayCostSearchMessages();   
            }

            #endregion Result Errors

            //Track user planned dates for journey
            TDJourneyParameters journeyParams = tdSessionManager.JourneyParameters;

            if (journeyParams != null && !IsPostBack)
            {
                try
                {
                    // Add Tracking Parameter
                    trackingHelper.AddTrackingParameter(this.pageId.ToString(), "OutwardDate", string.Format("{0}{1}", journeyParams.OutwardDayOfMonth, journeyParams.OutwardMonthYear.Replace("/","")));
                    if (journeyParams.OutwardAnyTime)
                    {
                        trackingHelper.AddTrackingParameter(this.pageId.ToString(), "OutwardTime", "AnyTime");
                    }
                    else
                    {
                        trackingHelper.AddTrackingParameter(this.pageId.ToString(), "OutwardTime", string.Format("{0}{1}", journeyParams.OutwardHour, journeyParams.OutwardMinute));
                    }
                    
                    AddDaysToDepartTrackingParam(journeyParams.OutwardDayOfMonth, journeyParams.OutwardMonthYear, false);
                    

                    if (tdSessionManager.JourneyRequest.IsReturnRequired || journeyParams.ReturnMonthYear == "OpenReturn")
                    {
                        // Add Tracking Parameter
                        trackingHelper.AddTrackingParameter(this.pageId.ToString(), "ReturnDate", string.Format("{0}{1}", journeyParams.ReturnDayOfMonth, journeyParams.ReturnMonthYear.Replace("/", "")));
                        if (journeyParams.ReturnAnyTime || journeyParams.ReturnMonthYear == "OpenReturn")
                        {
                            trackingHelper.AddTrackingParameter(this.pageId.ToString(), "ReturnTime", "AnyTime");
                        }
                        else
                        {
                            trackingHelper.AddTrackingParameter(this.pageId.ToString(), "ReturnTime", string.Format("{0}{1}", journeyParams.ReturnHour, journeyParams.ReturnMinute));
                        }

                        AddDaysToDepartTrackingParam(journeyParams.ReturnDayOfMonth, journeyParams.ReturnMonthYear, true);
                    }
                }
                catch (Exception ex)
                {
                    string message = "TrackingControlHelper Exception: " + ex.StackTrace;
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                                TDTraceLevel.Error, message);
                    Logger.Write(oe);
                }

            }


            headElementControl.ImageSource = GetModeImageSource();
            headElementControl.Desc = journeysSearchedControl.ToString();

            socialBookMarkLinkControl.BookmarkDescription = journeysSearchedControl.ToString();
            socialBookMarkLinkControl.EmailLink.NavigateUrl = Request.Url.AbsoluteUri + "#JourneyOptions";

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            #region Add Related Link context based on mode
            if ((tdSessionManager.FindAMode == FindAMode.Car) || (tdSessionManager.FindAMode == FindAMode.CarPark))
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyDetailsFindCarRoute);
            else if (tdSessionManager.FindAMode == FindAMode.Trunk)
            {
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyDetailsFindTrunkInput);
            }
            else
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyDetails);
            #endregion
            
            expandableMenuControl.AddExpandedCategory("Related links");
		}

        /// <summary>
        /// Adds a tracking parameter to pass outward days to depart or return days to depart for intellitracker
        /// </summary>
        /// <param name="day"></param>
        /// <param name="monthYear"></param>
        /// <param name="isReturn"></param>
        private void AddDaysToDepartTrackingParam(string day, string monthYear, bool isReturn)
        {
            try
            {
                string trackingParamKey = isReturn ? "ReturnDaysToDepart" : "OutwardDaysToDepart";

                string dateString = string.Format("{0}/{1}", day, monthYear);


                DateTime journeyDate = DateTime.MinValue;

                DateTime.TryParseExact(dateString, "dd/MM/yyyy", new CultureInfo("en-GB"), DateTimeStyles.None, out journeyDate);

                if (journeyDate != DateTime.MinValue)
                {

                    int daysToDepart = journeyDate.Subtract(DateTime.Now.Date).Days;

                    trackingHelper.AddTrackingParameter(this.pageId.ToString(), trackingParamKey, daysToDepart.ToString());
                }
            }
            catch (Exception ex)
            {
                string message = "TrackingControlHelper Exception: " + ex.StackTrace;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                            TDTraceLevel.Error, message);
                Logger.Write(oe);
            }

        }

        /// <summary>
        /// Adds intellitracker tag when a user clicks the link to re-plan car journey avoiding closed roads.
        /// </summary>
        /// <param name="isReturn">True if the tag needs adding for return journey, false otherwise</param>
        private void AddReplanAvoidClosuresParam(bool isReturn)
        {
            try
            {
                
                string trackingParamKey = isReturn ? "ReturnReplanCarAvoidClosures" : "OutwardReplanCarAvoidClosures";

                trackingHelper.AddTrackingParameter(this.pageId.ToString(), trackingParamKey, TrackingControlHelper.CLICK);
                
                
            }
            catch (Exception ex)
            {
                string message = "TrackingControlHelper Exception: " + ex.StackTrace;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                            TDTraceLevel.Error, message);
                Logger.Write(oe);
            }
        }

        /// <summary>
        /// Page Init Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Init(object sender, EventArgs e)
        {
            outwardCycleWalkLinks.Outward = true;
            returnCycleWalkLinks.Outward = false;
        }

        /// <summary>
        /// Page PreRender Event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_PreRender(object sender, EventArgs e)
        {
            SetupPageControlsForInternational();
        }

        private void SetupPageControlsForInternational()
        {
            if (tdSessionManager.FindAMode == FindAMode.International)
            {
                amendSaveSendControl.Visible = false;
                theOutputNavigationControl.Visible = false;
                
            }
        }

        private string DisplayBankHolidayMessage()
        {
            string message = string.Empty;

            IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            bool englishHolidayOutward = false;

            bool scotlandHolidayOutward = false;

            bool englishHolidayReturn = false;

            bool scotlandHolidayReturn = false;

            TDDateTime outwardDatetime = tdSessionManager.JourneyRequest.OutwardDateTime[0];

            englishHolidayOutward = ds.IsHoliday(DataServiceType.BankHolidays, new TDDateTime(outwardDatetime.Year, outwardDatetime.Month, outwardDatetime.Day) , DataServiceCountries.EnglandWales);

            scotlandHolidayOutward = ds.IsHoliday(DataServiceType.BankHolidays, new TDDateTime(outwardDatetime.Year, outwardDatetime.Month, outwardDatetime.Day), DataServiceCountries.Scotland);

            if (tdSessionManager.JourneyRequest.IsReturnRequired)
            {
                if (tdSessionManager.JourneyRequest.ReturnDateTime != null)
                {
                    TDDateTime returnDatetime = tdSessionManager.JourneyRequest.ReturnDateTime[0];

                    if (tdSessionManager.JourneyRequest.ReturnDateTime.Length > 0)
                    {
                        englishHolidayReturn = ds.IsHoliday(DataServiceType.BankHolidays, new TDDateTime(returnDatetime.Year, returnDatetime.Month, returnDatetime.Day), DataServiceCountries.EnglandWales);

                        scotlandHolidayReturn = ds.IsHoliday(DataServiceType.BankHolidays, new TDDateTime(returnDatetime.Year, returnDatetime.Month, returnDatetime.Day), DataServiceCountries.Scotland);
                    }
                }


            }

            bool englishHoliday = englishHolidayOutward | englishHolidayReturn;

            bool scotlandHoliday = scotlandHolidayOutward | scotlandHolidayReturn;

            if (englishHoliday || scotlandHoliday)
            {
                if (englishHoliday && scotlandHoliday)
                {
                    message = GetResource("JourneyPlannerOutput.Holiday");
                }
                else
                {
                    message = GetResource(string.Format("JourneyPlannerOutput.Holiday.{0}", englishHoliday ? "EnglandWales" : "Scotland"));
                }
            }

            return message;
        }


        private string GetModeImageSource()
        {
            string modeimage = string.Empty;

            switch (tdSessionManager.FindAMode)
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
		/// Initialisation Method
		/// </summary>
		private void Initialise()
		{
			// Initialise static labels, hypertext text and image button Urls 
			// from Resourcing Mangager.
			// Initialise static labels text

            this.PageTitle = GetResource("JourneyPlanner.JourneyDetailsPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            //Added css to headElementControl for park and ride page
            if (tdSessionManager.FindAMode == FindAMode.ParkAndRide && !headElementControl.Stylesheets.ToLower().Contains("journeydetailsparkandride.aspx.css"))
            {
                headElementControl.Stylesheets += ",JourneyDetailsParkAndRide.aspx.css";
            }

            //Added css to headElementControl for international page
            if (tdSessionManager.FindAMode == FindAMode.International && !headElementControl.Stylesheets.ToLower().Contains("journeydetailsinternational.aspx.css"))
            {
                headElementControl.Stylesheets += ",JourneyDetailsInternational.aspx.css";
            }

			InitialiseStaticLabels();

            if (!IsPostBack)
            {
                tdSessionManager.InputPageState.MapUrlOutward = String.Empty;
                tdSessionManager.InputPageState.MapUrlReturn = String.Empty;
            }

            // Reset ShowCO2 flag when not in International planner mode, as don't want to show for othe planners
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            if (viewState.ShowCO2 && (tdSessionManager.FindAMode != FindAMode.International))
            {
                viewState.ShowCO2 = false;
            }

		}

		/// <summary>
		/// Establish what mode the Itinerary Manager is in and whether we have any Return results
		/// </summary>
		private void DetermineStateOfResults()
		{
			itineraryExists = (itineraryManager.Length > 0);
			extendInProgress = itineraryManager.ExtendInProgress;
			showItinerary = (itineraryExists && !extendInProgress);
			showFindA = (!showItinerary && (tdSessionManager.IsFindAMode));

			if ( showItinerary )
			{
				outwardExists = (itineraryManager.OutwardLength > 0);
				returnExists = (itineraryManager.ReturnLength > 0);
			}
			else
			{
				//check for normal result
				ITDJourneyResult result = tdSessionManager.JourneyResult;
				if(result != null) 
				{
					outwardExists = ((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid;
					returnExists = ((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid;

					// Get time types for journey.
					outwardArriveBefore = tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = tdSessionManager.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}
		}

        /// <summary>
        /// Displays messages returned from the cost search
        /// </summary>
        private void DisplayCostSearchMessages()
        {
            if (FindInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode))
            {
                FindCostBasedPageState pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
                CostSearchError[] errors = pageState.SearchResult.GetErrors();
                if (errors.Length > 0)
                {
                    errorMessagePanel.Visible = true;
                    errorDisplayControl.Visible = true;
                }

                ArrayList errorsList = new ArrayList();

                foreach (CostSearchError error in errors)
                {
                    string errorText;
                    //append to the error message label
                    errorText = this.GetResource(error.ResourceID);
                    errorDisplayControl.ReferenceNumber = tdSessionManager.JourneyResult.JourneyReferenceNumber.ToString();
                    errorDisplayControl.Type = ErrorsDisplayType.Error;

                    errorsList.Add(errorText);
                }

                errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));

                pageState.SearchResult.ClearErrors();
            }
        }

		/// <summary>
		/// Initialises page components based on whether the page should be displaying
		/// journey details for a journey or journey extensions and also whether a return
		/// journey has been planned.
		/// </summary>
		private void InitialiseJourneyComponents()
		{
			DetermineStateOfResults();

			//Moved here for DEL 7
			carAllDetailsControlOutward.Printable = true;
			carAllDetailsControlReturn.Printable = true;			

			// Display different text if the itinerary summary is selected otherwise
			// display the selected journey number
			if (itineraryManager.FullItinerarySelected)
			{
				labelDetailsOutwardJourney.Text = GetResource("JourneyDetails.OutwardSummaryText");

				labelDetailsReturnJourney.Text = GetResource("JourneyDetails.ReturnSummaryText");
			} 
			else 
			{
				labelDetailsOutwardJourney.Text = GetResource("JourneyDetails.labelDetailsOutwardJourney.Text");

				labelDetailsReturnJourney.Text = GetResource("JourneyDetails.labelDetailsReturnJourney.Text");

				if(tdSessionManager.FindAMode == FindAMode.None)
				{
					labelDetailsOutwardJourney.Text += " " + itineraryManager.OutwardDisplayNumber.ToString();
					labelDetailsReturnJourney.Text += " " + itineraryManager.ReturnDisplayNumber.ToString();
				}
			}


			ITDJourneyResult journeyResult = itineraryManager.JourneyResult;
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

			if (showFindA)
			{
                TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
                if (TDSessionManager.Current.FindPageState != null)
                    modeTypes = tdSessionManager.FindPageState.ModeType;

				if (outwardExists)
				{
                    findSummaryResultTableControlOutward.Initialise(false, true, viewState.JourneyLeavingTimeSearchType, modeTypes);
				}

				if (returnExists)
				{
					findSummaryResultTableControlReturn.Initialise(false, false, viewState.JourneyReturningTimeSearchType, modeTypes);
				}
			}
			else
			{
				// Initialise those controls required if there are no extensions or
				// an extend is in progress
				if (outwardExists)
				{
					summaryResultTableControlOutward.Initialise(false, true, viewState.JourneyLeavingTimeSearchType);
				}

				if (returnExists)
				{
					summaryResultTableControlReturn.Initialise(false, false, viewState.JourneyReturningTimeSearchType);
				}
			}

			// Setup the AmendSaveSend control and its child controls
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

			plannerOutputAdapter.AmendSaveSendControlPageLoad(amendSaveSendControl, this.pageId);

            bool showTravelNewsIncidents = true;
            
            if(!bool.TryParse(Properties.Current["TravelNews.Toids.ProcessJourney.Switch"],out showTravelNewsIncidents))
            {
                showTravelNewsIncidents = true;
            }
            
            

			// Initialise those controls required for displaying an outward journey or extension
			if (outwardExists)
			{
				if (!itineraryManager.FullItinerarySelected)
				{
					// An individual journey is selected

					// Determine the journey details object to use in initialising the
					// details controls

					PublicJourney outPJ = null;
		
					if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
					{
						// the original journey has been selected
						outPJ = journeyResult.OutwardPublicJourney(
							viewState.SelectedOutwardJourneyID);
					}
					else if( viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended )
					{
						// the amended journey has been selected
						outPJ = journeyResult.AmendedOutwardPublicJourney;
					} 
					else if (viewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested) 
					{
						TDJourneyParametersMulti journeyParams = tdSessionManager.JourneyParameters as TDJourneyParametersMulti;

                        RoadJourney roadJourney = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();

                        if (!roadJourney.JourneyMatchedForTravelNewsIncidents)
                        {
                            roadJourneyHelper.ProcessRoadJourneyForTravelNewsIncidents(roadJourney);
                        }
                        
						// private road journey has been selected
                        carAllDetailsControlOutward.Initialise(true, journeyParams, showTravelNewsIncidents, 
                            roadJourney.HasClosure && roadJourney.AllowReplan);

                        // Set up to add the links to zoom to direction on the map
                        if (viewState.OutwardShowMap)
                        {
                            carAllDetailsControlOutward.SetMapProperties(true, mapJourneyControlOutward.MapId,
                                mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId, 
                                mapJourneyControlOutward.FirstElementId, Session.SessionID);
                        }
                    }

					if (outPJ != null) 
					{
						if (viewState.ShowOutwardJourneyDetailsDiagramMode)  
						{
                            journeyDetailsControlOutward.SetMapProperties(
                                mapJourneyControlOutward.MapId,
                                mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                                mapJourneyControlOutward.FirstElementId, Session.SessionID);
							journeyDetailsControlOutward.Initialise(outPJ, true, false,false,true, itineraryManager.JourneyRequest,tdSessionManager.FindAMode);
                            journeyDetailsControlOutward.MyPageId = pageId;
						} 
						else 
						{
                            journeyDetailsTableControlOutward.SetMapProperties(
                                mapJourneyControlOutward.MapId,
                                mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                                mapJourneyControlOutward.FirstElementId, Session.SessionID);
                            journeyDetailsTableControlOutward.Initialise(outPJ, true, tdSessionManager.FindAMode); 
                            journeyDetailsTableControlOutward.MyPageId = pageId;
						}
					}
				} 
				else 
				{
					// Full journey summary selected

					// Intialise details controls with no details object - the full
					// summary will be shown
					if (viewState.ShowOutwardJourneyDetailsDiagramMode) 
					{
						journeyDetailsControlOutward.Initialise(true, false,false,true, tdSessionManager.FindAMode);
						journeyDetailsControlOutward.MyPageId = pageId;	  
					} 
					else 
					{
                        journeyDetailsTableControlOutward.Initialise(true, tdSessionManager.FindAMode); 
                        journeyDetailsTableControlOutward.MyPageId = pageId;
					}
				}

			}

			// Initialise those controls required for displaying a return journey or extension
			if (returnExists)
			{
				if (!itineraryManager.FullItinerarySelected)
				{
					// An individual journey is selected

					// Determine the journey details object to use in initialising the
					// details controls

					PublicJourney retPJ = null;

					if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
					{
						// the original journey has been selected
						retPJ = journeyResult.ReturnPublicJourney(
							viewState.SelectedReturnJourneyID);
					}
					else if( viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended )
					{
						// the amended journey has been selected
						retPJ = journeyResult.AmendedReturnPublicJourney;
					}
					else if (viewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested) 
					{
						TDJourneyParametersMulti journeyParams = tdSessionManager.JourneyParameters as TDJourneyParametersMulti;

                        RoadJourney roadJourney = TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();

                        if (!roadJourney.JourneyMatchedForTravelNewsIncidents)
                        {
                            roadJourneyHelper.ProcessRoadJourneyForTravelNewsIncidents(roadJourney);
                        }

						// private road journey has been selected
                        carAllDetailsControlReturn.Initialise(false, journeyParams, showTravelNewsIncidents, 
                            roadJourney.HasClosure && roadJourney.AllowReplan);

                        // Set up to add the links to zoom to direction on the map
                        if (viewState.ReturnShowMap)
                        {
                            carAllDetailsControlReturn.SetMapProperties(true, mapJourneyControlReturn.MapId,
                                mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId, 
                                mapJourneyControlReturn.FirstElementId, Session.SessionID);
                        }
                                                
					}

					if (retPJ != null) 
					{
						if (viewState.ShowReturnJourneyDetailsDiagramMode) 
						{
                            journeyDetailsControlReturn.Initialise(retPJ, false, false, false, true, itineraryManager.JourneyRequest,tdSessionManager.FindAMode);
                            journeyDetailsControlReturn.SetMapProperties(
                                mapJourneyControlReturn.MapId,
                                mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId,
                                mapJourneyControlReturn.FirstElementId, Session.SessionID);
							journeyDetailsControlReturn.MyPageId = pageId;
						} 
						else 
						{
                            journeyDetailsTableControlReturn.SetMapProperties(
                                mapJourneyControlReturn.MapId,
                                mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId,
                                mapJourneyControlReturn.FirstElementId, Session.SessionID);
                            journeyDetailsTableControlReturn.Initialise(retPJ, false, tdSessionManager.FindAMode); 
                            journeyDetailsTableControlReturn.MyPageId = pageId;			
						}
					}

				} 
				else 
				{
					// Full journey summary selected

					// Intialise details controls with no details object - the full
					// summary will be shown
					if (viewState.ShowReturnJourneyDetailsDiagramMode) 
					{
						journeyDetailsControlReturn.Initialise(false, false,false,true,tdSessionManager.FindAMode);			
						journeyDetailsControlReturn.MyPageId = pageId;
					} 
					else 
					{
                        journeyDetailsTableControlReturn.Initialise(false, tdSessionManager.FindAMode); 
                        journeyDetailsTableControlReturn.MyPageId = pageId;
					}
				}

			}

			theOutputNavigationControl.Initialise(pageId);

            #region Setup emissions control

            //Setup CO2 control for TD Extra
            ITDSessionManager sessionManager = TDSessionManager.Current;

            sessionManager.JourneyEmissionsPageState.IsInternationalJourney = true;

            // Determine where we get selected journey from Journey Result or Itinerary Manager
            // and then set the Compare emissions control flags.
            // The Compare emissions control will then populate distances and calculate emissions
            // from the session
            if (((sessionManager.JourneyResult != null) && (sessionManager.JourneyResult.IsValid))
                || ((sessionManager.CycleResult != null) && (sessionManager.CycleResult.IsValid)))
                journeyEmissionsCompareControlOutward.UseSessionManager = true;
            else
                journeyEmissionsCompareControlOutward.UseItineraryManager = true;

            // Set the mode for the Compare emissions control
            journeyEmissionsCompareControlOutward.JourneyEmissionsCompareMode = JourneyEmissionsCompareMode.JourneyCompare;
            journeyEmissionsCompareControlOutward.ShowHeaderUnitsDropdown = false;
            journeyEmissionsCompareControlOutward.NonPrintable = true;

            #endregion
        }

		/// <summary>
		/// OnPreRender method - overrides base and updates the visiblity
		/// of controls depending on which should be rendered. Calls base OnPreRender
		/// as the final step.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;
			itineraryManager = TDItineraryManager.Current;

			//Checks to set the params to pass for units for Printer Pages
			string  printerUnits = tdSessionManager.InputPageState.Units.ToString();
			string url = GetResource("JourneyPlanner.UrlPrintableJourneyDetails");

			// IR 2802 - reinstated following lines deleted during removal of second print button in stream 2556
			if (printerUnits == "kms")
			{
				JourneyChangeSearchControl1.PrinterFriendlyPageButton.UrlParams = "Units=kms";
			}
			else
			{
				JourneyChangeSearchControl1.PrinterFriendlyPageButton.UrlParams = "Units=miles";
			}
			// IR 2802 end

            SetPrintableControl();

			InitialiseJourneyComponents();

			showItinerary =  itineraryManager.Length > 0 && !itineraryManager.ExtendInProgress;
			SetControlVisibility();

			SetupSkipLinksAndScreenReaderText();

			// Prerender setup for the AmendSaveSend control and its child controls
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);
            
			plannerOutputAdapter.AmendSaveSendControlPreRender(amendSaveSendControl);

			plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);

			if (viewState.OutwardShowMap && (mapJourneyControlOutward.Visible != true) )
			{
				ShowMapControl(true);
			}
			if (viewState.ReturnShowMap)
			{
				ShowMapControl(false);
			}

            base.OnPreRender(e);
		}

		override protected void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
		}

		#endregion Initialisation, Page_Load and OnPreRender

		#region Methods to control visiblities of controls

        /// <summary>
        /// Method to populate controls during Page_Load
        /// </summary>
        private void SetupControls()
        {
            if (!itineraryExists && FindInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode))
            {
                // Set up controls that are visible in FindAFare mode
                SetUpStepsControl();
            }

            if (mapJourneyControlOutward.Visible || journeyDetailsControlReturn.Visible)
            {
                helpLabelJourneyDetails.Text = GetResource("helpLabelJourneyDetailsMap");
            }
            else
            {
                helpLabelJourneyDetails.Text = GetResource("helpLabelJourneyDetails");
            }

            // Set up the help button
            if (tdSessionManager.FindAMode == FindAMode.International)
            {
                // Don't show the help button
                JourneyChangeSearchControl1.HelpLabel = string.Empty;
                JourneyChangeSearchControl1.HelpUrl = string.Empty;
            }
            else
            {
                JourneyChangeSearchControl1.HelpLabel = "helpLabelJourneyDetails";
            }
        }

		/// <summary>
		/// Determines which controls should be visible
		/// </summary>
		private void SetControlVisibility()
		{
			ITDJourneyResult journeyResult = itineraryManager.JourneyResult;
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

            // Hide Title, Navigation control, and footnotes if no journey results
            if (!outwardExists && !returnExists)
            {
                JourneyPlannerOutputTitleControl1.Visible = false;
                theOutputNavigationControl.Visible = false;
                footnotesControl.Visible = false;
            }


			summaryOutwardTable.Attributes.Add("class", showFindA ? "jpsumoutfinda" : "jpsumout");

			// Are there any outward journeys or extensions?
			if (outwardExists)
			{
				outwardPanel.Visible = true;

				summaryResultTableControlOutward.Visible = !(showItinerary || showFindA);
				findSummaryResultTableControlOutward.Visible = showFindA;

                if (itineraryManager.FullItinerarySelected) 
				{
					// Full itinerary journey selected - treat as a public journey
                    OutwardPublicJourneyVisible(true, viewState.SelectedOutwardJourneyType);
					OutwardCarJourneyVisible(false);
				}
				else
				{
					// Full journey itinerary not selected

					// Determine if the selected outward journey is public or road
					bool isPublic = (viewState != null) && (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal || viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended);
                    OutwardPublicJourneyVisible(isPublic, viewState.SelectedOutwardJourneyType);
					OutwardCarJourneyVisible(!isPublic);
				}
			}
			else
			{
                
				outwardPanel.Visible = false;
                OutwardCarJourneyVisible(false);
                outwardDetailPanel.Visible = false;
				hyperLinkImageOutwardJourneys.Visible = false;
				hyperLinkOutwardJourneys.Visible = false;
			}

			// Are there any return journeys or extensions?
			if (returnExists)
			{
				returnPanel.Visible = true;
				hyperLinkTagReturnJourneys.Visible = true;
		
				summaryResultTableControlReturn.Visible = !(showItinerary || showFindA);
				findSummaryResultTableControlReturn.Visible = showFindA;

				// The itinerary table for return extensions has no radio buttons on right hand
				// side of table, hence use appropriate styles to not render cell with blue background 
				if (findSummaryResultTableControlReturn.Visible)
				{
					summaryReturnPanel.CssClass = "boxtypeeighteen";
					summaryReturnTable.CssClass = "jpsumrtnfinda";
				}
				else
				{
					summaryReturnPanel.CssClass = "boxtypeeighteen";
					summaryReturnTable.CssClass = "jpsumrtn";
				}

				if (itineraryManager.FullItinerarySelected)
				{
					// Full journey itinerary selected - treate as a public journey
					ReturnPublicJourneyVisible(true , viewState.SelectedReturnJourneyType);
					ReturnCarJourneyVisible(false);
				} 
				else
				{
					// Full journey itinerary not selected

					// Determine if the selected return journey is public or road
					bool isPublic = viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal || viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended;

					ReturnPublicJourneyVisible(isPublic , viewState.SelectedReturnJourneyType);
					ReturnCarJourneyVisible(!isPublic);
				} 
			} 
			else 
			{
				returnPanel.Visible = false;
				hyperLinkTagReturnJourneys.Visible = false;
			}

			if (!itineraryExists && FindInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode))
			{
				// Set up controls that are visible in FindAFare mode
				SetFindFareVisible(true);
				findFareSelectedTicketLabelControl.PageState = tdSessionManager.FindPageState;
				findFareGotoTicketRetailerControl.PageState = tdSessionManager.FindPageState;
			}
			else
			{
				SetFindFareVisible(false);
			}

            SetJourneyOverviewBackButton();

            if (TDSessionManager.Current.IsStopInformationMode)
                JourneyChangeSearchControl1.GenericBackButtonVisible = true;

			// Set up correct mode for footnotes control
			if ( tdSessionManager.IsFindAMode )
			{
				if ( FindFareInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode) )
				{
					footnotesControl.CostBasedResults = true;
				}
				footnotesControl.Mode = tdSessionManager.FindAMode;
			}

            if (!this.IsJavascriptEnabled)
            {
                buttonShowMap.Visible = false;
                mapJourneyControlOutward.Visible = false;
                mapJourneyControlReturn.Visible = false;
            }
		}

		/// <summary>
		/// Sets the visibility of controls depending on whether we are displaying FindFare results
		/// </summary>
		/// <param name="findFare">True if we are displaying FindFare results</param>
		private void SetFindFareVisible(bool findFare)
		{
            selectedTicketRow.Visible = findFare;
			selectedTicketCell.Visible = findFare;
			findFareSelectedTicketLabelControl.Visible = findFare;
			findFareGotoTicketRetailerControl.Visible = findFare;

            panelFindFareSteps.Visible = findFare;

            JourneyChangeSearchControl1.GenericBackButtonVisible = findFare;
		}

        /// <summary>
        /// Sets up the mode for the FindFareStepsConrol
        /// </summary>
        private void SetUpStepsControl()
        {
            findFareStepsControl.Visible = true;
            findFareStepsControl.Step = FindFareStepsControl.FindFareStep.FindFareStep3;
            findFareStepsControl.SessionManager = TDSessionManager.Current;
            findFareStepsControl.PageState = TDSessionManager.Current.FindPageState;
        }

        /// <summary>
        /// Sets the back button visibility when in Trunk mode
        /// </summary>
        private void SetJourneyOverviewBackButton()
        {
            if (TDSessionManager.Current.FindAMode == FindAMode.Trunk || TDSessionManager.Current.FindAMode == FindAMode.International)
                JourneyChangeSearchControl1.GenericBackButtonVisible = true;
        }

		/// <summary>
		/// Sets the visibility of outward public journey controls.
		/// </summary>
		/// <param name="visible">True if controls should be visible, false otherwise.</param>
		/// <param name="type">Type of the public journey being rendered.</param>
		private void OutwardPublicJourneyVisible(bool visible, TDJourneyType type)
		{
			ITDJourneyResult journeyResult = itineraryManager.JourneyResult;
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;

            //JourneyEmissions control added for TD Extra -
            //Toggle JourneyEmissions / details / table control visibility NB only outward is considered 
            //at this stage as TD Extra has no returns and CO2 control only appears for TD Extra
            outwardEmissionsPanel.Visible = viewState.ShowCO2;
            outwardDetailPanel.Visible = !viewState.ShowCO2;

			// A public original journey can be adjusted only for non-amended journeys
			// with an interchange count of 0 and if in diagram mode and an extension is
			// not being shown
			int outwardInterchangeCount = 0;

			// Only get the interchange count if journey is not an extension 
			if (!itineraryExists )
			{
				PublicJourney journey = journeyResult.OutwardPublicJourney(
					viewState.SelectedOutwardJourneyID);
				if(journey != null)
				{
					outwardInterchangeCount = 
						TDJourneyResult.GetInterchangeCount(journey);
				}
			}

			// Journey Details control is visible if diagram mode
            journeyDetailsControlOutward.Visible = visible && viewState.ShowOutwardJourneyDetailsDiagramMode && outwardDetailPanel.Visible;

			// Journey Details table control is visible if not diagram mode.
            journeyDetailsTableControlOutward.Visible = visible && !viewState.ShowOutwardJourneyDetailsDiagramMode && outwardDetailPanel.Visible;

            buttonShowTableOutward.Visible = visible && outwardDetailPanel.Visible &&
				(type == TDJourneyType.PublicOriginal || type == TDJourneyType.PublicAmended || type == TDJourneyType.Itinerary); 

			if (viewState.ShowOutwardJourneyDetailsDiagramMode) 
			{
				buttonShowTableOutward.Text = GetResource("JourneyDetailsControl.buttonShowTable.Text");
			} 
			else 
			{
				buttonShowTableOutward.Text = GetResource("JourneyDetailsControl.buttonShowDiagram.Text");
			}

            // CO2 button only shown for International planner
            if (visible)
            {
                buttonCheckCO2Outward.Visible = (tdSessionManager.FindAMode == FindAMode.International);
            }

            // Map button shown for all planners except International planner
            buttonShowMap.Visible = (tdSessionManager.FindAMode != FindAMode.International);

		}

		/// <summary>
		/// Sets the visibility of return public journey controls.
		/// </summary>
		/// <param name="visible">True if controls should be visible, false otherwise.</param>
		/// <param name="type">Type of the public journey being rendered.</param>
		private void ReturnPublicJourneyVisible(bool visible, TDJourneyType type)
		{
			ITDJourneyResult journeyResult = itineraryManager.JourneyResult;
			TDJourneyViewState viewState = itineraryManager.JourneyViewState;
			
			// A public original journey can be adjusted only for non-amended journeys
			// with an interchange count of 0 and if in diagram mode and an extension is
			// not being shown
			int returnInterchangeCount = 0;

			// Only get the interchange count if journey is not an extension 
			if (!itineraryExists)
			{			
				PublicJourney journey = journeyResult.ReturnPublicJourney(
					viewState.SelectedReturnJourneyID);
				if(journey != null)
				{
					returnInterchangeCount = 
						TDJourneyResult.GetInterchangeCount(journey);
				}
			}

			// Journey Details Control is visible if diagram mode
			journeyDetailsControlReturn.Visible = visible && viewState.ShowReturnJourneyDetailsDiagramMode;
		
			// Journey Details table control is visible if table mode
			journeyDetailsTableControlReturn.Visible = visible && !viewState.ShowReturnJourneyDetailsDiagramMode;

			buttonShowTableReturn.Visible = visible &&
				(type == TDJourneyType.PublicOriginal || type == TDJourneyType.PublicAmended || type == TDJourneyType.Itinerary); 

			if (viewState.ShowReturnJourneyDetailsDiagramMode) 
			{
				buttonShowTableReturn.Text = GetResource("JourneyDetailsControl.buttonShowTable.Text");
			} 
			else 
			{
				buttonShowTableReturn.Text = GetResource("JourneyDetailsControl.buttonShowDiagram.Text");
			}

            // CO2 button only shown for International planner, but currently not supported for return journey
            if (visible)
            {
                buttonCheckCO2Return.Visible = false;
            }

            // Map button shown for all planners except International planner
            buttonShowMapReturn.Visible = (tdSessionManager.FindAMode != FindAMode.International);
		}

		/// <summary>
		/// Sets the visiblity of all outward car journey controls.
		/// </summary>
		/// <param name="visible">True if visible, false if not visible.</param>
		private void OutwardCarJourneyVisible(bool visible)
		{
            TDJourneyViewState viewState = itineraryManager.JourneyViewState;

            //JourneyEmissions control added for TD Extra -
            //Toggle JourneyEmissions / details / table control visibility NB only outward is considered 
            //at this stage as TD Extra has no returns and CO2 control only appears for TD Extra
            outwardEmissionsPanel.Visible = viewState.ShowCO2;
            outwardDetailPanel.Visible = !viewState.ShowCO2;

			carAllDetailsControlOutward.Visible = visible;
			if(tdSessionManager.FindAMode == FindAMode.Car) 
			{
				labelCarOutward.Visible = false;
			}
			else 
			{
				labelCarOutward.Visible = visible;
			}

            // Always hide CO2 button for car
            if (visible)
            {
                buttonCheckCO2Outward.Visible = false;
            }

            // Map button shown for all planners except International planner
            buttonShowMap.Visible = (tdSessionManager.FindAMode != FindAMode.International);
		}

		/// <summary>
		/// Sets the visibility of all return car journey controls.
		/// </summary>
		/// <param name="visible">True if visible, false if not visible.</param>
		private void ReturnCarJourneyVisible(bool visible)
		{
			carAllDetailsControlReturn.Visible = visible;
			if(tdSessionManager.FindAMode == FindAMode.Car) 
			{
				labelCarReturn.Visible = false;
			}
			else 
			{
				labelCarReturn.Visible = visible;
			}

            // Always hide CO2 button for car
            if (visible)
            {
                buttonCheckCO2Return.Visible = false;
            }

            // Map button shown for all planners except International planner
            buttonShowMapReturn.Visible = (tdSessionManager.FindAMode != FindAMode.International);
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
			imageOutwardJourneyCarSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageReturnJourneyCarSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageReturnJourneyPTSkipLink2.ImageUrl = SkipLinkImageUrl;
			imageReturnJourneyCarSkipLink2.ImageUrl = SkipLinkImageUrl;
			imageFindTransportToStartLocationSkipLink.ImageUrl = SkipLinkImageUrl;
			imageFindTransportFromEndLocationSkipLink.ImageUrl = SkipLinkImageUrl;

			imageMainContentSkipLink1.AlternateText = GetResource("JourneyDetails.imageMainContentSkipLink.AlternateText");

			string journeyButtonsSkipLinkText = GetResource("JourneyDetails.imageJourneyButtonsSkipLink.AlternateText");

			imageJourneyButtonsSkipLink1.AlternateText = journeyButtonsSkipLinkText;
			imageJourneyButtonsSkipLink3.AlternateText = journeyButtonsSkipLinkText;

			// Show different skip links depending on status of search (car vs public transport & outward, return)
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			bool isOutwardPublic = (viewState != null) && (viewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal || viewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended);
			bool isReturnPublic = (viewState != null) && (viewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal || viewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended);
			
			if (outwardExists)
			{
				if (itineraryExists)	// In Extend a mode
				{
					panelFindTransportToStartLocationSkipLink.Visible = true;
					imageFindTransportToStartLocationSkipLink.AlternateText = 
						GetResource("JourneyDetails.imageFindTransportToStartLocationSkipLink.AlternateText");

					panelFindTransportFromEndLocationSkipLink.Visible = true;
					imageFindTransportFromEndLocationSkipLink.AlternateText = 
						GetResource("JourneyDetails.imageFindTransportFromEndLocationSkipLink.AlternateText");
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
                    panelOutwardJourneyCarSkipLink1.Visible = false;
					
					// There are 2 skip link descriptions depending on whether the button
					// is show in table or show in diagram
					string altText;
					if (viewState.ShowOutwardJourneyDetailsDiagramMode)
						altText = GetResource("JourneyDetails.imageOutwardJourneyPTSkipLink1.AlternateText");
					else
						altText = GetResource("JourneyDetails.imageOutwardJourneyPTSkipLink1.AlternateText2");

					imageOutwardJourneyPTSkipLink1.AlternateText = altText;

				}
				if (!isOutwardPublic)
				{
					// Show outward car skip links
					panelOutwardJourneyCarSkipLink1.Visible = true;
                    panelOutwardJourneyPTSkipLink1.Visible = false;
					imageOutwardJourneyCarSkipLink1.AlternateText = GetResource("JourneyDetails.imageOutwardJourneyCarSkipLink1.AlternateText");

                    //Fix for XHTML compliance
                    imageOutwardJourneyPTSkipLink1.AlternateText = " ";
				}
			}

			if (returnExists)
			{
				if (isReturnPublic)
				{
					// Show return public transport skip links
					panelReturnJourneyPTSkipLink1.Visible = true;
					panelReturnJourneyPTSkipLink2.Visible = true;
                    panelReturnJourneyCarSkipLink1.Visible = false;
                    panelReturnJourneyCarSkipLink2.Visible = false;
					
					// There are 2 skip link descriptions depending on whether the button
					// is show in table or show in diagram
					string altText;
					if (viewState.ShowReturnJourneyDetailsDiagramMode)
						altText = GetResource("JourneyDetails.imageReturnJourneyPTSkipLink1.AlternateText");
					else
						altText = GetResource("JourneyDetails.imageReturnJourneyPTSkipLink1.AlternateText2");

					imageReturnJourneyPTSkipLink1.AlternateText = altText;
					imageReturnJourneyPTSkipLink2.AlternateText = altText;
					
				}
				if (!isReturnPublic)
				{
					// Show return car transport skip links
					panelReturnJourneyCarSkipLink1.Visible = true;
					imageReturnJourneyCarSkipLink1.AlternateText = GetResource("JourneyDetails.imageReturnJourneyCarSkipLink1.AlternateText");

					panelReturnJourneyCarSkipLink2.Visible = true;
					imageReturnJourneyCarSkipLink2.AlternateText = GetResource("JourneyDetails.imageReturnJourneyCarSkipLink1.AlternateText");

                    panelReturnJourneyPTSkipLink1.Visible = false;
                    panelReturnJourneyPTSkipLink2.Visible = false;

                }
			}
		}

        /// <summary>
        /// Sets up the printable control with the querystring params needed
        /// </summary>
        private void SetPrintableControl()
        {
            // Add the javascript to set the map viewstate on client side
            PrintableButtonHelper printHelper = null;

            if ((outwardExists) && (returnExists))
            {
                // Initialise for both outward and return maps
                printHelper = new PrintableButtonHelper(
                    mapJourneyControlOutward.MapId,
                    mapJourneyControlReturn.MapId,
                    mapJourneyControlOutward.MapSymbolsSelectId,
                    mapJourneyControlReturn.MapSymbolsSelectId,
                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId);
            }
            else if (outwardExists)
            {
                // Initialise only for outward map
                printHelper = new PrintableButtonHelper(
                    mapJourneyControlOutward.MapId,
                    string.Empty,
                    mapJourneyControlOutward.MapSymbolsSelectId,
                    string.Empty,
                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                    string.Empty);
            }
            else if (returnExists)
            {
                // Initialise only for return map
                printHelper = new PrintableButtonHelper(
                    string.Empty,
                    mapJourneyControlReturn.MapId,
                    string.Empty,
                    mapJourneyControlReturn.MapSymbolsSelectId,
                    string.Empty,
                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId);
            }

            if (printHelper != null)
            {
                // Only attach if maps are visible
                if (mapJourneyControlOutward.Visible || mapJourneyControlReturn.Visible)
                {
                    JourneyChangeSearchControl1.PrinterFriendlyPageButton.PrintButton.OnClientClick = printHelper.GetClientScript();
                }
            }
        }

		#endregion


		#region Methods to set dynamic label text

		/// <summary>
		/// Constructs the text string for the Outward Journeys label.
		/// </summary>
		private string GetOutwardJourneyLabelText()
		{
			// Get the boilerplate label strings from Resourcing Manager
			string forString = GetResource("JourneyPlanner.labelFor");
			string anyString = GetResource("JourneyPlanner.labelAnyTime");

			TDDateTime outwardLeavingTime;
			string leavingTimeDate = String.Empty;

			if ( showItinerary )
			{
				if (itineraryManager.OutwardMultipleDates)
				{
					// There are multiple dates in the outward Itinerary, so we can't say anything useful in the label
					return String.Empty;
				}
				else
				{
					// Get the outward leaving time from TDItineraryManager
					outwardLeavingTime = itineraryManager.OutwardDepartDateTime();
					leavingTimeDate = outwardLeavingTime.ToString("ddd dd MMM yy");

					// return the constructed string for travelling at a specific date
					return String.Format("{0} {1}", forString, leavingTimeDate);
				}
			}
			else
			{
				if (tdSessionManager.JourneyViewState.OriginalJourneyRequest != null)
				{
					TDJourneyViewState viewState = tdSessionManager.JourneyViewState;

					// Get the outward leaving time from TDSessionManager
					outwardLeavingTime = viewState.OriginalJourneyRequest.OutwardDateTime[0];
					leavingTimeDate = outwardLeavingTime.ToString("ddd dd MMM yy");

					if (viewState.OriginalJourneyRequest.OutwardAnyTime)
					{
						// return the constructed string for travelling at any time
						return String.Format("{0} {1} {2}", forString, leavingTimeDate, anyString);
					}
					else
					{
						string leavingTimeTime = outwardLeavingTime.ToString("HH:mm");
						string outwardSearchType = String.Empty;

						if(tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType)
							outwardSearchType = GetResource("JourneyPlanner.labelArrivingBefore");
						else
							outwardSearchType = GetResource("JourneyPlanner.labelLeavingAfter");

						// return the constructed string for travelling at a specific date and time
						return String.Format("{0} {1} {2} {3}", forString, leavingTimeDate, outwardSearchType, leavingTimeTime);
					}
				}
				else
				{
					return String.Empty;
				}
			}
		}

		/// <summary>
		/// Constructs the text string for the Return Journeys label.
		/// </summary>
		private string GetReturnJourneyLabelText()
		{
			// Get the boilerplate label strings from Resourcing Manager
			string forString = GetResource("JourneyPlanner.labelFor");
			string anyString = GetResource("JourneyPlanner.labelAnyTime");

			TDDateTime returnLeavingTime;
			string leavingTimeDate = String.Empty;

			if ( showItinerary )
			{
				if (itineraryManager.ReturnMultipleDates)
				{
					// There are multiple dates in the return Itinerary, so we can't say anything useful in the label
					return String.Empty;
				}
				else
				{
					// Get the return leaving time from TDItineraryManager
					returnLeavingTime = itineraryManager.ReturnDepartDateTime();
					leavingTimeDate = returnLeavingTime.ToString("ddd dd MMM yy");

					// return the constructed string for travelling at a specific date
					return String.Format("{0} {1}", forString, leavingTimeDate);
				}
			}
			else
			{
				if (tdSessionManager.JourneyViewState.OriginalJourneyRequest != null)
				{
					TDJourneyViewState viewState = tdSessionManager.JourneyViewState;

					// Get the return leaving time from TDSessionManager
					returnLeavingTime = viewState.OriginalJourneyRequest.ReturnDateTime[0];
					leavingTimeDate = returnLeavingTime.ToString("ddd dd MMM yy");

					if (viewState.OriginalJourneyRequest.ReturnAnyTime)
					{
						// return the constructed string for travelling at any time
						return String.Format("{0} {1} {2}", forString, leavingTimeDate, anyString);
					}
					else
					{
						string leavingTimeTime = returnLeavingTime.ToString("HH:mm");
						string returnSearchType = String.Empty;

						if(tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType)
							returnSearchType = GetResource("JourneyPlanner.labelArrivingBefore");
						else
							returnSearchType = GetResource("JourneyPlanner.labelLeavingAfter");

						// return the constructed string for travelling at a specific date and time
						return String.Format("{0} {1} {2} {3}", forString, leavingTimeDate, returnSearchType, leavingTimeTime);
					}
				}
				else
				{
					return String.Empty;
				}
			}
		}

		#endregion Methods to set dynamic label text


		#region Methods to set static label text

		/// <summary>
		/// Method to intiialise the static labels that are on the page.
		/// </summary>
		private void InitialiseStaticLabels()
		{
			// -----
			//if find a don't show this
			if(tdSessionManager.FindAMode == FindAMode.Car) 
			{
				labelCarOutward.Text = "(" + GetResource("JourneyDetails.labelDetailsCar") + ")";

				labelCarReturn.Text = "(" + GetResource("JourneyDetails.labelDetailsCar") + ")";
			}
			else 
			{
				labelCarOutward.Text = "(" + GetResource("JourneyDetails.labelDetailsCar") + ")";

				labelCarReturn.Text = "(" + GetResource("JourneyDetails.labelDetailsCar") + ")";
			}
           
            
            // Set up a generic journey map control reference that can represent outward or return and see if it is public transport or car

            if (mapJourneyControlOutward.Visible)
            {
                buttonShowMap.Text = GetResource("HideMap.Text");
            }
            else
            {
                buttonShowMap.Text = GetResource("ShowMap.Text");
            }

            if (mapJourneyControlReturn.Visible)
            {
                buttonShowMapReturn.Text = GetResource("HideMap.Text");
            }
            else
            {
                buttonShowMapReturn.Text = GetResource("ShowMap.Text");
            }

            buttonCheckCO2Outward.Text = GetResource("JourneyDetails.CheckCO2.Text");
		}
		#endregion

        /// <summary>
        /// Displays error message when validation fails for replanning a road journey to avoid closure or blockages
        /// </summary>
        private void ShowReplanAvoidClosureError()
        {
            if (tdSessionManager.ValidationError.Contains(ValidationErrorID.RouteAffectedByClosuresErrors))
            {
                string closureError = Global.tdResourceManager.GetString("ValidateAndRun.RouteAffectedByClosures", TDCultureInfo.CurrentUICulture);

                errorDisplayControl.ErrorStrings = new string[] { closureError };

                errorDisplayControl.ReferenceNumber = tdSessionManager.JourneyResult.JourneyReferenceNumber.ToString();

                if (errorDisplayControl.ErrorStrings.Length > 0)
                {
                    errorMessagePanel.Visible = true;
                    errorDisplayControl.Visible = true;
                }

            }
        }

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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{	
			//this.Load += new System.EventHandler(this.Page_Load);
            trackingHelper = new TrackingControlHelper();

            roadJourneyHelper = new RoadJourneyHelper();
		}

		/// <summary>
		/// 
		/// </summary>
		private void ExtraWiringEvents() 
		{
			
			this.buttonShowMap.Click += new EventHandler(this.buttonShowMap_Click);
			this.buttonShowMapReturn.Click += new EventHandler(this.buttonShowMapReturn_Click);
			this.buttonShowTableOutward.Click += new EventHandler(this.buttonShowTableOutward_Click);
			this.buttonShowTableReturn.Click += new EventHandler(this.buttonShowTableReturn_Click);

            this.buttonCheckCO2Outward.Click += new EventHandler(this.buttonCheckCO2_Click);
           
            if(TDSessionManager.Current.IsStopInformationMode)
                this.JourneyChangeSearchControl1.GenericBackButton.Click += new EventHandler(this.buttonStopInformationBack_Click);
            else if (TDSessionManager.Current.FindAMode == FindAMode.Trunk || TDSessionManager.Current.FindAMode == FindAMode.International)
                this.JourneyChangeSearchControl1.GenericBackButton.Click += new EventHandler(this.buttonBackJourneyOverview_Click);
            else
                this.JourneyChangeSearchControl1.GenericBackButton.Click += new EventHandler(this.buttonFindFareBack_Click);
            			
			// Subscribe to selection changed event of the Summary 
			// table controls so that the map can be updated when it fires.
			summaryResultTableControlOutward.SelectionChanged +=
				new SelectionChangedEventHandler(this.OutwardSelectionChanged);

			summaryResultTableControlReturn.SelectionChanged +=
				new SelectionChangedEventHandler(this.ReturnSelectionChanged);
			
			findSummaryResultTableControlOutward.SelectionChanged += 
				new SelectionChangedEventHandler(this.OutwardSelectionChanged);

			findSummaryResultTableControlReturn.SelectionChanged += 
				new SelectionChangedEventHandler(this.ReturnSelectionChanged);

			// Events that are fired when map button is clicked on segment control (or table equivalent)
			journeyDetailsControlOutward.MapButtonClicked += new MapButtonClickEventHandler(this.journeyDetailsControlOutward_MapButtonClicked);
			journeyDetailsControlReturn.MapButtonClicked += new MapButtonClickEventHandler(this.journeyDetailsControlReturn_MapButtonClicked);
			journeyDetailsTableControlOutward.MapButtonClicked += new MapButtonClickEventHandler(this.journeyDetailsControlOutward_MapButtonClicked);
			journeyDetailsTableControlReturn.MapButtonClicked += new MapButtonClickEventHandler(this.journeyDetailsControlReturn_MapButtonClicked);

			amendSaveSendControl.AmendViewControl.SubmitButton.Click += new EventHandler(AmendViewControl_Click);

            socialBookMarkLinkControl.EmailLinkButton.Click += new EventHandler(EmailLink_Click);

            carAllDetailsControlOutward.ReplanAvoidClosures += new EventHandler(carAllDetailsControlOutward_ReplanAvoidClosures);
            carAllDetailsControlReturn.ReplanAvoidClosures += new EventHandler(carAllDetailsControlReturn_ReplanAvoidClosures);            
		}
             
              
        

		#endregion


		#region Event handling
		/// <summary>
		/// Handles the button click to toggle the display format of outward details 
		/// between tabular and graphical formats
		/// </summary>
		/// <param name="sender">Event originator</param>
		/// <param name="e">Event parameters</param>
		private void buttonShowTableOutward_Click(object sender, EventArgs e)
		{
			bool showDiagramMode = itineraryManager.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode;
			itineraryManager.JourneyViewState.ShowOutwardJourneyDetailsDiagramMode = !showDiagramMode;
            

            if (showDiagramMode)
            {
                // Add Tracking Parameter when button click to show table view
                trackingHelper.AddTrackingParameter(buttonShowTableOutward, TrackingControlHelper.CLICK);
            }
            else
            {
                // Add Tracking Parameter when button clik to show diagram view 
                trackingHelper.AddTrackingParameter(this.pageId.ToString(), "buttonShowDiagramOutward", TrackingControlHelper.CLICK);
            }
		}

		/// <summary>
		/// Handles the button click to toggle the display format of return details 
		/// between tabular and graphical formats
		/// </summary>
		/// <param name="sender">Event originator</param>
		/// <param name="e">Event parameters</param>
		private void buttonShowTableReturn_Click(object sender, EventArgs e)
		{
			bool showDiagramMode = itineraryManager.JourneyViewState.ShowReturnJourneyDetailsDiagramMode;
			itineraryManager.JourneyViewState.ShowReturnJourneyDetailsDiagramMode = !showDiagramMode;

            
            if (showDiagramMode)
            {
                // Add Tracking Parameter when button click to show table view
                trackingHelper.AddTrackingParameter(buttonShowTableReturn, TrackingControlHelper.CLICK);
            }
            else
            {
                // Add Tracking Parameter when button clik to show diagram view 
                trackingHelper.AddTrackingParameter(this.pageId.ToString(), "buttonShowDiagramReturn", TrackingControlHelper.CLICK);

            }
		}

		/// <summary>
		/// Event handler that is fired when the "OK" button is clicked on the amendViewControl.
		/// This will switch Session partitions and display Summary page with the appropriate results.
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void AmendViewControl_Click(object sender,EventArgs e)
		{
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

			plannerOutputAdapter.ViewPartitionResults(amendSaveSendControl.AmendViewControl.PartitionSelected);
		}

		/// <summary>
		/// Handler for the Show map button.
		/// Show the whole outward journey in a map control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonShowMap_Click(object sender, EventArgs e)
		{
            if (!mapJourneyControlOutward.Visible)
            {
                // Add Tracking Parameter
                trackingHelper.AddTrackingParameter(this.pageId.ToString(), "buttonShowMapOutward", TrackingControlHelper.CLICK);

                ShowMapControl(true);
                HideMapControl(false);

                this.ScrollManager.RestPageAtElement(mapJourneyControlOutward.FirstElementId);
            }
            else
            {
                // Add Tracking Parameter
                trackingHelper.AddTrackingParameter(this.pageId.ToString(), "buttonHideMapOutward", TrackingControlHelper.CLICK);

                HideMapControl(true);
            }
		}

		/// <summary>
		/// Handler for the Show map button.
		/// Show the whole return journey in a map control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonShowMapReturn_Click(object sender, EventArgs e)
		{
            if (!mapJourneyControlReturn.Visible)
            {
                //Add Tracking Parameter
                trackingHelper.AddTrackingParameter(this.pageId.ToString(), "buttonShowMapReturn", TrackingControlHelper.CLICK);

                ShowMapControl(false);
                HideMapControl(true);

                this.ScrollManager.RestPageAtElement(mapJourneyControlReturn.FirstElementId);
            }
            else
            {
                // Add Tracking Parameter
                trackingHelper.AddTrackingParameter(this.pageId.ToString(), "buttonHideMapOutward", TrackingControlHelper.CLICK);

                HideMapControl(false);
            }
		}

        /// <summary>
        /// Handler for the Check CO2 button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCheckCO2_Click(object sender, EventArgs e)
        {
            ITDSessionManager sessionManager = TDSessionManager.Current;
            if (sessionManager.FindAMode != FindAMode.International)
            {
                //Do nothing - at present this button should only show for international planner
            }
            else
            {
                //Toggle JourneyEmissions / details / table control visibility NB only outward is considered 
                //at this stage as TD Extra has no returns and CO2 control only appears for TD Extra
                TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
                viewState.ShowCO2 = true;
            }
		}
                 

		/// <summary>
		/// Handles the visual bits associated with showing the map control - ie makes it visible and updates the buttons
		/// </summary>
		/// <param name="?"></param>
		private void ShowMapControl(bool outward)
		{
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;

			if(outward)
			{
                // Initialise map
                mapJourneyControlOutward.Visible = true;
                mapJourneyControlOutward.Initialise(true, true, false);

				viewState.OutwardShowMap = true;
                				
				buttonShowMap.Text = GetResource("HideMap.Text");

				tdSessionManager.InputPageState.PreviousOutwardJourney = -1;
			}
			else
			{
                // Initialise map
                mapJourneyControlReturn.Visible = true;
                mapJourneyControlReturn.Initialise(false, true, false);

				viewState.ReturnShowMap = true;
				
				buttonShowMapReturn.Text = GetResource("HideMap.Text");

				tdSessionManager.InputPageState.PreviousReturnJourney = -1;
			}
		}

		/// <summary>
		/// Handles the visual bits associated with showing the map control - ie makes it visible and updates the buttons
		/// </summary>
		/// <param name="?"></param>
		private void HideMapControl(bool outward)
		{
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;

			if(outward)
			{
				viewState.OutwardShowMap = false;

                mapJourneyControlOutward.Visible = false;

				buttonShowMap.Text = GetResource("ShowMap.Text");

                // Force the directions map link to be hidden
                carAllDetailsControlOutward.carjourneyDetailsTableControl.IsRowMapLinkVisible = false;
			}
			else
			{
				viewState.ReturnShowMap = false;

				mapJourneyControlReturn.Visible = false;

				buttonShowMapReturn.Text = GetResource("ShowMap.Text");

                // Force the directions map link to be hidden
                carAllDetailsControlReturn.carjourneyDetailsTableControl.IsRowMapLinkVisible = false;
			}
		}

		/// <summary>
		/// Handler for map button being pressed on the segment control (control used for public journeys)
		/// When pressed, load the map with the journey details for the selected leg only.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void journeyDetailsControlOutward_MapButtonClicked(object sender, MapButtonClickEventArgs e)
		{
			// If map control not visible then show the control and initialise it
			if (mapJourneyControlOutward.Visible !=true)
			{
				ShowMapControl(true);
                HideMapControl(false);
			}

			// Now show the selected leg
			int selectedLeg = e.LegIndex;
			mapJourneyControlOutward.ShowSelectedLeg = selectedLeg;

			this.ScrollManager.RestPageAtElement(mapJourneyControlOutward.FirstElementId);
		}

		/// <summary>
		/// Handler for map button being pressed on the segment control (control used for public journeys)
		/// When pressed, load the map with the journey details for the selected leg only.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void journeyDetailsControlReturn_MapButtonClicked(object sender, MapButtonClickEventArgs e)
		{
			// If map control not visible then show the control and initialise it
			if (mapJourneyControlReturn.Visible !=true)
			{
				ShowMapControl(false);
                HideMapControl(true);
			}

			// Now show the selected leg
			int selectedLeg = e.LegIndex;
			mapJourneyControlReturn.ShowSelectedLeg = selectedLeg;

			this.ScrollManager.RestPageAtElement(mapJourneyControlReturn.FirstElementId);
		}
        		
		/// <summary>
		/// Handler for the selection changed event from the the Outward Journey Summary.
		/// </summary>
		private void OutwardSelectionChanged(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Handler for the selection changed event from the Return Journey Summary.
		/// </summary>
		private void ReturnSelectionChanged(object sender, EventArgs e)
		{			
		}

		/// <summary>
		/// Handler for the map button click event from Outward segments control or details table control
		/// </summary>
		private void OnMapButtonOutwardClick(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// Handler for the map button click event from Return segments control or details table control
		/// </summary>
		private void OnMapButtonReturnClick(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// IR 2500 Event handler to for Back Button for Find A Fare navigation 
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void buttonFindFareBack_Click(object sender, EventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			sessionManager.Session[SessionKey.Transferred] = false;
			sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneySummaryTicketSelectionBack;        
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

        /// <summary>
        /// Handle button click to redirect user to stop information page 
        /// </summary>
        /// <param name="sender">Originator of event</param>
        /// <param name="e">Event parameters</param>
        protected void buttonStopInformationBack_Click(object sender, EventArgs e)
        {
            ITDSessionManager sessionManager = TDSessionManager.Current;
            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.StopInformation;
        }

		/// <summary>
		/// Event handler that responds to changes in outward and return maps.  
		/// Changes are then saved in session data so the information 
		/// is available to the printer friendly page.
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">e</param>
		private void Map_OnMapChangedEvent(object sender, MapChangedEventArgs e)
		{
		}

        /// <summary>
        /// Event handler that responds to the Social Link control's email link
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EmailLink_Click(object sender, EventArgs e)
        {
            if (amendSaveSendControl.IsLoggedIn())
            {
                amendSaveSendControl.SetActiveTab(AmendPanelMode.SendEmailNormal);
            }
            else
            {
                amendSaveSendControl.SetActiveTab(AmendPanelMode.SendEmailLogin);
            }
            amendSaveSendControl.Focus();

            
            string amendSaveSendControlFocusScript = @"<script>  function ScrollView() { var el = document.getElementById('" + amendSaveSendControl.SendEmailTabButton.ClientID
                                              + @"'); if (el != null){ el.scrollIntoView(); el.focus();}} window.onload = ScrollView;</script>";

            this.ClientScript.RegisterClientScriptBlock(this.GetType(),"CtrlFocus", amendSaveSendControlFocusScript);

        }

        /// <summary>
        /// Handler for replan avoid closures event 
        /// to replan the return road journey avoiding closures/blockages affecting the journey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carAllDetailsControlReturn_ReplanAvoidClosures(object sender, EventArgs e)
        {
            TDJourneyViewState viewState = itineraryManager.JourneyViewState;
            viewState.ReturnRoadReplanned = true;

            // only avoid the toids with closure/blockages
            bool success = roadJourneyHelper.ReplanRoadJourneyToAvoidClosures(false, true);
            if (!success)
            {
                ShowReplanAvoidClosureError();

                // Ensure the Replan button is no longer shown in the car details control
                RoadJourney roadJourney = TDItineraryManager.Current.JourneyResult.ReturnRoadJourney();
                roadJourney.AllowReplan = false;

                ScrollManager.RestPageAtElement(headerControl.ClientID);
            }
        }

        /// <summary>
        /// Handler for replan avoid closures event 
        /// to replan the outward road journey avoiding closures/blockages affecting the journey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void carAllDetailsControlOutward_ReplanAvoidClosures(object sender, EventArgs e)
        {
            TDJourneyViewState viewState = itineraryManager.JourneyViewState;
            viewState.OutwardRoadReplanned = true;

            // only avoid the toids with closure/blockages
            bool success = roadJourneyHelper.ReplanRoadJourneyToAvoidClosures(true, true);
            if (!success)
            {
                ShowReplanAvoidClosureError();

                // Ensure the Replan button is no longer shown in the car details control
                RoadJourney roadJourney = TDItineraryManager.Current.JourneyResult.OutwardRoadJourney();
                roadJourney.AllowReplan = false;

                ScrollManager.RestPageAtElement(headerControl.ClientID);
            }
        }

		#endregion
	}

}