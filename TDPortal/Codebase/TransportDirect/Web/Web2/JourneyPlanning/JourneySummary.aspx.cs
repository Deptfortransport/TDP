// *********************************************** 
// NAME                 : JourneySummary.aspx.cs
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 05/09/2003
// DESCRIPTION			: Journey Summary page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneySummary.aspx.cs-arc  $
//
//   Rev 1.13   Aug 17 2012 10:55:42   dlane
//Cycle walk links
//Resolution for 5827: CCN Cycle Walk links
//
//   Rev 1.12   Apr 22 2010 11:52:40   pghumra
//Added comments following code review
//Resolution for 5459: Update soft content for Intellitacker tracking URL in tag
//
//   Rev 1.11   Mar 30 2010 10:18:24   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.10   Mar 19 2010 10:50:42   apatel
//Added custom related links for city to city result pages
//Resolution for 5468: Related link in city to city incorrect
//
//   Rev 1.9   Mar 05 2010 10:17:06   apatel
//added event handlers for findresulttablecontrols
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.8   Mar 04 2010 17:03:30   pghumra
//Updated tracking functionality
//Resolution for 5402: Add Intellitracker tag to all TDP web pages
//
//   Rev 1.7   Nov 15 2009 18:17:52   mmodi
//Updated styles to make results pages look consistent
//
//   Rev 1.6   Oct 21 2009 11:18:20   PScott
//Add social bookmark links to Summary,maps, and fares screen
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.5   Oct 15 2008 17:15:40   mmodi
//Updated for cycle planner help
//
//   Rev 1.4   Oct 13 2008 16:44:30   build
//Automatically merged from branch for stream5014
//
//   Rev 1.3.1.2   Oct 13 2008 10:38:54   mmodi
//Updated page id for cycle mode
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.1   Sep 15 2008 11:12:04   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.0   Jun 20 2008 14:28:48   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.0   Jun 20 2008 14:16:00   mmodi
//Updated to detect cycle journey
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 07 2008 14:50:26   apatel
//made tiltle "Journey Found For" to look in table view when findamode is flight.
//
//   Rev 1.2   Mar 31 2008 13:25:00   mturner
//Drop3 from Dev Factory
//
//
//  Rev Devfactory Mar 28 2008 08:40:00 apatel
//  added styles for the park and ride page and car route page through code.
//
//  Rev Devfactory Jan 30 2008 14:23:51 aahmed
//  Modified to set PageOptionsControls inside the blue boxes. New PageOptionsControl added to page which
//  will be display/hide when hide/advance button will be clicked.
//
//   Rev 1.0   Nov 08 2007 13:30:14   mturner
//Initial revision.
//
//   Rev DevFactory Jan 20 2008 19:00:00 dgath
//CCN0382b City to City enhancements:
//Updated to pass mode type to FindSummaryResultControl selected on 
//JourneyOverview page in City to City journeys. ModeType value obtained from Session.FindPageState.ModeType
//
//   Rev 1.119   Jun 14 2006 12:04:36   esevern
//Code fix for vantive 3918704 - Enable buttons when no outward journey is returned but a return journey is. Changed code to remove assumption that there is always an outward journey.
//Resolution for 3686: Buttons disabled when return journey returned and outward not
//
//   Rev 1.118   Apr 11 2006 15:30:50   mdambrine
//Adding helpBusDetails specific pages for find-a-bus
//Resolution for 3874: DN093 Find a Bus: Journey results Help text contains reference to Car journeys
//
//   Rev 1.117   Mar 14 2006 18:48:36   NMoorhouse
//fix of stream3353 merge problem
//
//   Rev 1.116   Mar 13 2006 18:33:02   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.111.1.3   Mar 13 2006 16:08:12   tmollart
//Removed references to SummaryItineraryTableControl.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.111.1.2   Mar 09 2006 10:33:50   asinclair
//Removed access to Extend
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.111.1.1   Feb 27 2006 15:24:58   pcross
//Removed Refine Journey button - now on OutputNavigationControl
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.111.1.0   Feb 21 2006 09:58:16   asinclair
//Added 'Refine journey' button to allow access to Refins page.  This is still work in progress as this will not be the correct way to access this page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.115   Feb 23 2006 18:38:14   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.114   Feb 10 2006 12:24:58   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.113   Dec 13 2005 11:20:46   asinclair
//Merge for stream3143
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.112   Dec 13 2005 09:50:04   esevern
//added alt text for amend date time down arrow image link
//Resolution for 3330: Del 8: JourneySummary WAI changes
//
//   Rev 1.111   Nov 30 2005 17:57:54   rhopkins
//Corrections to the button alignments
//Resolution for 3216: UEE: Javascript disabled - Printer friendly button uses flat style
//Resolution for 3242: UEE: Netscape - buttons overlap with journey summary on results page
//
//   Rev 1.110.2.1   Dec 12 2005 17:00:44   tmollart
//Removed code to reinstate journey results.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.110.2.0   Nov 29 2005 16:10:18   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.110   Nov 15 2005 16:33:08   mguney
//Null check applied for pageState.SelectedTravelDate in SetupControls method. This property is set to null after extend journey.
//Resolution for 3072: DN040: SBP Unhandled error when New Search clicked after Extend
//
//   Rev 1.109   Nov 03 2005 16:02:04   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.108.1.2   Oct 24 2005 14:03:18   RGriffith
//Changes to accomodate AmendViewControl changes
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.108.1.1   Oct 12 2005 19:55:58   rgreenwood
//TD089 ES020 Fixed naming conventions and event failure
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.108.1.0   Oct 12 2005 15:06:54   ralonso
//TD089 ES020 image button replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.108   Sep 29 2005 15:32:36   rgeraghty
//Merge code for UEE phase 1
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.107.1.2   Sep 22 2005 10:58:50   rgreenwood
//DN079 TD088 Code Review actions
//Resolution for 2771: DEL 8 Stream: PageEntry reporting for Extend functionality
//
//   Rev 1.107.1.1   Sep 13 2005 09:51:00   rgreenwood
//DN079 UEE TD088 JourneyExtension Tracking: Added logging for Add Extension (submit) button
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.107.1.0   Sep 07 2005 13:12:00   rgreenwood
//DN079 UEE ES015 24 Hour Help Button removal
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.107   Aug 19 2005 11:40:40   asinclair
//Check in for merge of 2572
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.105.2.1   Aug 09 2005 19:45:30   asinclair
//Now using arraylist when constructing ErrorMessages
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.105.2.0   Jul 22 2005 18:30:06   asinclair
//Now users ErrorDisplayControl
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//   Rev 1.106   Aug 16 2005 14:55:36   jgeorge
//Automatically merged from branch for stream2556
//
//   Rev 1.105.1.0   Jun 29 2005 10:42:34   jbroome
//Replace footnotes labels with ResultsFootnotesControl
//Resolution for 2556: DEL 8 Stream: Accessibility Links
//
//   Rev 1.105   May 04 2005 14:11:50   rhopkins
//Added additional note for FindAFare (Train)
//
//   Rev 1.104   Apr 20 2005 14:48:36   rgeraghty
//DisplayCostSearchMessages method added
//Resolution for 2193: PT - Messages returned by cost search back end will not be displayed
//
//   Rev 1.103   Apr 20 2005 12:10:42   COwczarek
//Itinerary manager instance variable reassigned in pre-render
//event handler since partition may have been switched by user.
//PlannerOutputAdapter instantiated when needed to ensure it
//uses itinerary manager from correct partition.
//Resolution for 2079: PT - Extend journey does not work with PT cost based searches
//
//   Rev 1.102   Apr 15 2005 14:15:18   rhopkins
//When displaying Instruction text and Help, use TicketType of selected TravelDate, rather than TicketType originally requested by User.
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.101   Apr 15 2005 12:48:28   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.100   Apr 13 2005 15:45:42   tmollart
//Modified SetupControls method so page states are casted as FindCostPage state instead of FindFarePageState. This means compatibility with FindTrunkCostBasedPageStates as well.
//Resolution for 2136: PT: Portal error page displayed when attempting to view journeys for a city to city cost based journey
//
//   Rev 1.99   Apr 12 2005 18:19:30   pcross
//Changed the way that the skip links image URL is accessed (now from langStrings, not HTML)
//
//   Rev 1.98   Apr 11 2005 08:35:18   rgeraghty
//Back button functionality added for find fare results
//Resolution for 2058: PT: Back button missing from the Find Fare Results (Summary) Page
//
//   Rev 1.97   Apr 06 2005 12:17:12   pcross
//Added Extend Journey skip links
//
//   Rev 1.96   Apr 05 2005 16:26:18   rhopkins
//Use methods in PlannerOutputAdapter to initialise AmendSaveSendControl
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.95   Apr 04 2005 20:32:20   rhopkins
//Corrected handling of AmendViewControl change of Session Partition
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.94   Apr 01 2005 19:51:34   rhopkins
//Changes to allow display of FindFare results and partition switching.
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.93   Mar 21 2005 16:08:32   pcross
//Tweaked order of skip links
//
//   Rev 1.92   Mar 18 2005 12:23:40   pcross
//Skip links and screenreader text
//
//   Rev 1.91   Mar 10 2005 11:36:00   rhopkins
//Show different Help for FindFare Singles and non-Singles
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.90   Mar 09 2005 19:57:48   rhopkins
//Added FindFare controls for "Buy tickets" buttons and selected ticket label. Also moved and reworked Help button control.
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.89   Mar 01 2005 16:29:36   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.88   Feb 25 2005 19:49:34   rhopkins
//Removed the Print button because one is now included in the JourneyChangeSearchControl.
//Added FindFareGotoTicketRetailerControl.
//Resolution for 1932: DEV Code Review: FAF Output Pages
//
//   Rev 1.87   Nov 03 2004 12:54:22   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.86   Oct 21 2004 17:41:02   jgeorge
//Added flight footnote to Trunk results
//Resolution for 1721: Change footnote on Find Flight results page
//
//   Rev 1.85   Oct 21 2004 17:06:54   jgeorge
//Updated format of message
//Resolution for 1721: Change footnote on Find Flight results page
//
//   Rev 1.84   Oct 21 2004 11:05:20   jgeorge
//Changed footnote for Find Flight results
//Resolution for 1721: Change footnote on Find Flight results page
//
//   Rev 1.83   Sep 27 2004 11:34:32   RGeraghty
//Help text change for Find A Flight
//
//   Rev 1.82   Sep 22 2004 15:51:56   esevern
//check for number of maximum journey extensions reached before setting extend journey info text
//
//   Rev 1.81   Sep 21 2004 16:38:34   esevern
//IR1581 - amended outward and return journey label for find a car (there will only be one journey)
//
//   Rev 1.80   Sep 20 2004 16:45:28   COwczarek
//Page title now displayed using JourneyPlannerOutputTitleControl.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary
//
//   Rev 1.79   Sep 19 2004 15:04:22   jbroome
//IR1391 - visibility of Add Extension to Journey button if incomplete results returned.
//
//   Rev 1.78   Sep 17 2004 17:43:54   esevern
//outward journey text different if in find a car mode
//
//   Rev 1.77   Sep 17 2004 12:12:50   jbroome
//IR 1591 - Extend Journey Usability Changes
//
//   Rev 1.76   Sep 14 2004 15:42:06   esevern
//added find coach/train/car/variety summary help label text and more urls: IR1305
//
//   Rev 1.75   Sep 10 2004 11:24:58   passuied
//Added functionality to hide notes in summary page if in FindA Car mode
//
//   Rev 1.74   Sep 09 2004 15:21:12   RHopkins
//IR1543 Added Extend journey help.
//
//   Rev 1.73   Aug 31 2004 15:06:46   RHopkins
//IR1440 Rationalised the code for determining the state of the result data.  This improved code has been duplicated across all of the results pages to reduce the burden of future maintenance.
//
//   Rev 1.72   Aug 19 2004 13:31:34   COwczarek
//Find A mode was determined on page load but could change
//before pre-render. Get value directly when required rather than
//using an instance variable.
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.71   Aug 10 2004 15:45:04   JHaydock
//Update of display of correct header control for help pages.
//
//   Rev 1.70   Aug 10 2004 14:46:56   COwczarek
//Use FindAMode method to determine current Find A mode
//Resolution for 1202: Implement FindTrainInput and FindCoachInput pages
//
//   Rev 1.69   Jul 27 2004 11:20:26   RHopkins
//Put back the footer code that I deleted by mistake.
//
//   Rev 1.68   Jul 22 2004 15:32:20   jgeorge
//Updates for Find a...
//
//   Rev 1.67   Jul 22 2004 14:18:48   RHopkins
//IR1113 The "Amend date/time" anchor link now varies its text depending upon whether the AmendSaveSend control is displaying "Amend date and time" or "Amend stopover time" or not displaying either.
//
//   Rev 1.66   Jun 28 2004 11:59:18   esevern
//corrected initialisation of AmendSaveSendControl (fix for AmbiguityPage back button ArrayIndex error)
//
//   Rev 1.65   Jun 17 2004 14:43:08   RHopkins
//Corrected date display on summary controls
//
//   Rev 1.64   Jun 16 2004 19:40:10   RHopkins
//Corrected display of date/time text above summary controls
//
//   Rev 1.63   Jun 15 2004 12:23:20   jgeorge
//Changed footnote - IR1004
//
//   Rev 1.62   Jun 10 2004 15:59:42   RHopkins
//Change to formatting of Return SummaryItineraryTable
//
//   Rev 1.61   Jun 09 2004 17:33:00   RHopkins
//Style changes to Return Summary Itinerary Table
//
//   Rev 1.60   Jun 08 2004 15:47:26   RHopkins
//Corrections to labels
//
//   Rev 1.59   Jun 07 2004 16:24:04   RHopkins
//Corrected Control state handling for changes in Itinerary Manager Mode.
//
//   Rev 1.58   Jun 04 2004 19:15:54   RHopkins
//Improvements to User Control rendering
//
//   Rev 1.57   Jun 02 2004 12:10:24   RHopkins
//Modify logic for determining whether to display Summary Itinerary Table or Summary Results Table
//
//   Rev 1.56   May 27 2004 19:09:18   ESevern
//summary control display changes
//
//   Rev 1.55   Apr 02 2004 10:16:32   AWindley
//DEL 5.2 QA Changes: Resolution for 692
//
//   Rev 1.54   Mar 12 2004 19:40:30   AWindley
//DEL5.2 Results Summary page changes
//
//
//   Rev 1.53   Mar 01 2004 13:47:02   asinclair
//Removed Printer hyperlink as not needed in Del 5.2
//
//   Rev 1.52   Jan 14 2004 12:05:04   RPhilpott
//Clear MapUrlOutward and MapUrlReturn from session in initialisation so that they are not erroneously picked by "send to a friend" facility.
//Resolution for 598: Wrong map attached to emails "sent to a friend".
//
//   Rev 1.51   Dec 09 2003 13:59:56   kcheung
//Fixed so that journey summary message does not force a line break causing the title to appear incorrectly.,
//
//   Rev 1.50   Nov 24 2003 16:05:54   kcheung
//Fixed so that if result is not valid then correctly handled.
//
//   Rev 1.49   Nov 19 2003 10:09:54   kcheung
//Used count property to determine if return exists or not.
//Resolution for 132: Determing whether Journey is return or not
//
//   Rev 1.48   Nov 17 2003 16:16:42   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.47   Nov 13 2003 12:47:46   kcheung
//UPdated to use the output navigation control
//Resolution for 149: Streamline code for result summary buttons
//
//   Rev 1.46   Nov 07 2003 11:29:24   PNorell
//Fixed n*mespace comment confusing NAnt.
//
//   Rev 1.45   Nov 06 2003 17:02:40   kcheung
//Updated so that error messages are cleared once they have been displayed.
//
//   Rev 1.44   Nov 05 2003 11:51:36   kcheung
//Added Header summary for Page Load
//
//   Rev 1.43   Nov 05 2003 10:25:22   kcheung
//Added : back to times as requested in QA
//
//   Rev 1.42   Nov 04 2003 13:53:56   kcheung
//Updated n*mespace to Web.Templates
//
//   Rev 1.41   Nov 03 2003 16:57:10   kcheung
//Updated so that Fares and Ticket Retailers are greyed-out if the user has selected a car journey as requeseted in the QA
//
//   Rev 1.40   Oct 28 2003 11:39:46   kcheung
//Fixed for QA
//
//   Rev 1.39   Oct 28 2003 10:19:34   CHosegood
//Added Fares button event handler
//
//   Rev 1.38   Oct 27 2003 18:42:18   COwczarek
//Add event handler to redirect to ticket retailers page
//
//   Rev 1.37   Oct 23 2003 16:31:08   esevern
//added check for journey result not null
//
//   Rev 1.36   Oct 20 2003 18:22:38   kcheung
//Fixed for FXCOP
//
//   Rev 1.35   Oct 20 2003 17:25:18   kcheung
//Cosmetic corrections for the benefit of FXCOP
//
//   Rev 1.34   Oct 17 2003 16:38:12   kcheung
//Fixed for FXCop comments
//
//   Rev 1.33   Oct 15 2003 13:13:18   kcheung
//Fixed html
//
//   Rev 1.32   Oct 15 2003 12:53:44   kcheung
//Fixed html to show the down arrow button
//
//   Rev 1.31   Oct 14 2003 10:55:30   kcheung
//Fixed helpbox for output to helpboxoutput
//
//   Rev 1.30   Oct 13 2003 10:58:00   kcheung
//Fixed alt texts
//
//   Rev 1.29   Oct 13 2003 09:41:22   PNorell
//Fixed alt texts for the image buttons.
//
//   Rev 1.28   Oct 10 2003 17:45:04   kcheung
//Updated alt text
//
//   Rev 1.27   Oct 09 2003 12:20:34   kcheung
//Page Title reads from langstrings.. all uppercase html tags updated to lowercase
//
//   Rev 1.26   Oct 09 2003 10:21:20   PNorell
//Fixed so messages are always shown, not just only when no journeys are found.
//
//   Rev 1.25   Oct 07 2003 17:50:46   kcheung
//Fixed bank holiday bug where it was reading the outward selected journey leg only.
//
//   Rev 1.24   Oct 03 2003 16:45:20   PNorell
//Updated error message on page.
//Updated index out of bounds.
//
//   Rev 1.23   Oct 03 2003 14:21:12   kcheung
//Uncommented call to InputPageState.Initialise()
//
//   Rev 1.22   Oct 01 2003 16:11:36   kcheung
//Added commented out version of InputPageState.Initialise
//
//   Rev 1.21   Oct 01 2003 12:54:02   kcheung
//Updated bank holiday tests
//
//   Rev 1.20   Sep 30 2003 17:47:50   kcheung
//Updated bank holiday test code
//
//   Rev 1.19   Sep 29 2003 11:37:22   kcheung
//Fixed printable links
//
//   Rev 1.18   Sep 29 2003 09:13:48   kcheung
//Removed commented out code for Bank Holiday test
//
//   Rev 1.17   Sep 26 2003 16:55:34   kcheung
//Fixed styling.. added printer format hyperlink
//
//   Rev 1.16   Sep 26 2003 16:24:56   kcheung
//Fixed bank holiday bug!
//
//   Rev 1.15   Sep 26 2003 14:50:04   PNorell
//Fixed hyperlink within the page.
//
//   Rev 1.14   Sep 26 2003 12:00:46   kcheung
//Fixed return css bug
//
//   Rev 1.13   Sep 25 2003 11:46:22   PNorell
//Fixed the compare control to have footer and small bugs in the handling of the adjusted journey.
//
//   Rev 1.12   Sep 24 2003 16:16:06   PNorell
//Updated for html integration.
//
//   Rev 1.11   Sep 23 2003 19:09:32   kcheung
//Updated
//
//   Rev 1.10   Sep 22 2003 20:56:08   kcheung
//Added cms stuff
//
//   Rev 1.9   Sep 22 2003 17:20:46   PNorell
//Integrated help controls and associated resources.
//Fixed bug in event handling in JourneyDetails.
//
//   Rev 1.8   Sep 21 2003 14:35:40   kcheung
//Added Initialise call to AmendSaveSend Control
//
//   Rev 1.7   Sep 18 2003 18:52:22   kcheung
//Added Event handlers to amend/new journey
//
//   Rev 1.6   Sep 18 2003 18:30:34   kcheung
//Bank holiday added
//
//   Rev 1.5   Sep 18 2003 15:55:54   kcheung
//Updated handlers so that they don't clear FormShift
//
//   Rev 1.4   Sep 18 2003 11:04:08   PNorell
//Fixed reference to use interface instead of concrete implimentation.
//
//   Rev 1.3   Sep 18 2003 09:56:50   jcotton
//Changes for intitial screenflow integration work
//
//   Rev 1.2   Sep 16 2003 18:07:12   kcheung
//Updated
//
//   Rev 1.1   Sep 10 2003 15:25:34   kcheung
//Updated to inherit from TDPage
//
//   Rev 1.0   Sep 09 2003 15:32:30   kcheung
//Initial Revision
//
//   Rev 1.3   Sep 09 2003 15:17:24   kcheung
//Updated spelling
//
//   Rev 1.2   Sep 08 2003 16:53:06   kcheung
//Updated
//
//   Rev 1.1   Sep 08 2003 15:08:18   kcheung
//working page without any HTML formatting

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.CostSearch;

using Logger = System.Diagnostics.Trace;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.ScreenFlow;


namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Journey Summary page
	/// </summary>
	public partial class JourneySummary : TDPage
	{

		// Labels

		// Help controls

		// Web User Controls
		protected ResultsTableTitleControl resultsTableTitleControlOutward;
		protected ResultsTableTitleControl resultsTableTitleControlReturn;
		protected SummaryResultTableControl summaryResultTableControlOutward;
		protected SummaryResultTableControl summaryResultTableControlReturn;
        protected JourneysSearchedForControl theJourneysSearchForControl;
		protected AmendSaveSendControl amendSaveSendControl;
		protected HeaderControl headerControl;
		protected FindSummaryResultControl findSummaryResultTableControlOutward;
		protected FindSummaryResultControl findSummaryResultTableControlReturn;
		protected FindFareSelectedTicketLabelControl findFareSelectedTicketLabelControl;
		protected FindFareGotoTicketRetailerControl findFareGotoTicketRetailerControl;
		protected ResultsFootnotesControl footnotesControl;
		
		protected OutputNavigationControl theOutputNavigationControl;
		protected JourneyChangeSearchControl theJourneyChangeSearchControl;
		protected ErrorDisplayControl errorDisplayControl;

		protected System.Web.UI.WebControls.Label hyperLinkAmendDateTime;
		protected System.Web.UI.WebControls.Image hyperLinkImageAmendDateTime;

		protected System.Web.UI.HtmlControls.HtmlGenericControl hyperlinkAmendSaveSend;

		//TD Buttons	
		private ITDSessionManager tdSessionManager;
		private TDItineraryManager itineraryManager;

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

		/// <summary>
		/// Constructor - sets the page id
		/// </summary>
		public JourneySummary() : base()
		{
            pageId = PageId.JourneySummary;
		}

        /// <summary>
        /// Method to handle the selection changed event raised by the radio button selected in the 
        /// SummaryResultTableControl user control
        /// 
        /// NOTE: Although no implementation is required for this method currently, the empty method is required 
        /// to ensure said event is subscribed to and the fact that the user selected an alternative radio button
        /// is recorded and subsequently sent to Inellitracker for tracking purposes
        /// </summary>
        private void summaryResultTableControlOutward_SelectionChanged(object sender, EventArgs e)
        {
            // not implemented
        }

        /// <summary>
        /// Method to handle the selection changed event raised by the radio button selected in the 
        /// SummaryResultTableControl user control
        /// 
        /// NOTE: Although no implementation is required for this method currently, the empty method is required 
        /// to ensure said event is subscribed to and the fact that the user selected an alternative radio button
        /// is recorded and subsequently sent to Inellitracker for tracking purposes
        /// </summary>
        private void summaryResultTableControlReturn_SelectionChanged(object sender, EventArgs e)
        {
            // not implemented
        }

        /// <summary>
        /// Page Init event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            outwardCycleWalkLinks.Outward = true;
            returnCycleWalkLinks.Outward = false;
        }

		/// <summary>
		/// Page Load Method. Sets-up the page.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{

            tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;

            // Override the PageId if we're in cycle mode
            if (tdSessionManager.FindAMode == FindAMode.Cycle)
            {
                pageId = PageId.CycleJourneySummary;
            }

            //Added css to headElementControl for park and ride page
            if (tdSessionManager.FindAMode == FindAMode.ParkAndRide)
                headElementControl.Stylesheets += ",JourneySummaryParkAndRide.aspx.css";

            //Added css to headElementControl for car route page
            if (tdSessionManager.FindAMode == FindAMode.Car )
                headElementControl.Stylesheets += ",JourneySummaryCar.aspx.css";

            //Added css to headElementControl for cycle journey page
            if (tdSessionManager.FindAMode == FindAMode.Cycle)
                headElementControl.Stylesheets += ",CycleJourneyDetails.aspx.css";

			#region Set up text and images

            this.PageTitle = GetResource("JourneyPlanner.JourneySummaryPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
		           
            // Ensure title is specific to the modeTypes asked for
            // e.g. Summary of Rail journey options
            if (tdSessionManager.FindPageState != null)
                JourneyPlannerOutputTitleControl1.ModeTypes = tdSessionManager.FindPageState.ModeType;
            			
			#endregion Set up text and images

            SetupControls();

			SetHelpText();

            if (tdSessionManager.FindAMode == FindAMode.Flight)
                theJourneysSearchedForControl1.IsTableView = true;

			#region Result Errors

			errorMessagePanel.Visible = false;
			errorDisplayControl.Visible = false;
			

			if ( !showItinerary )
			{
				ITDJourneyResult res = tdSessionManager.JourneyResult;
				if( res != null )
				{
					ArrayList errorsList = new ArrayList();
					
					foreach( CJPMessage mess in res.CJPMessages )
					{
						if(mess.Type == ErrorsType.Warning)
						{
							errorDisplayControl.Type = ErrorsDisplayType.Warning;
						}

						string text = mess.MessageText;
						if( text == null || text.Length == 0 )
						{
							text = GetResource(mess.MessageResourceId);
						}
						errorsList.Add( text );
					}

					errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));
					
					errorDisplayControl.ReferenceNumber = tdSessionManager.JourneyResult.JourneyReferenceNumber.ToString();
					
					if (errorDisplayControl.ErrorStrings.Length >0)
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

            headElementControl.ImageSource = GetModeImageSource();
            headElementControl.Desc = theJourneysSearchedForControl1.ToString();

            socialBookMarkLinkControl.BookmarkDescription = theJourneysSearchedForControl1.ToString();
            socialBookMarkLinkControl.EmailLink.NavigateUrl = Request.Url.AbsoluteUri + "#JourneyOptions";


            #region CCN 0427 left hand navigation changes
            // Information column set up
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            #region Add Related Link context based on mode
            if ((tdSessionManager.FindAMode == FindAMode.Car) || (tdSessionManager.FindAMode == FindAMode.CarPark))
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneySummaryFindCarRoute);
            else if (tdSessionManager.FindAMode == FindAMode.Trunk)
            {
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneySummaryFindTrunkInput);
            }
            else
                expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneySummary);
            #endregion

            expandableMenuControl.AddExpandedCategory("Related links");
        
        }
            #endregion
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
															
				foreach(CostSearchError error in errors)
				{
					string errorText;
					//append to the error message label
					errorText = this.GetResource(error.ResourceID);
					errorDisplayControl.ReferenceNumber = tdSessionManager.JourneyResult.JourneyReferenceNumber.ToString();
					errorDisplayControl.Type = ErrorsDisplayType.Error;

					errorsList.Add( errorText );
				}				

				errorDisplayControl.ErrorStrings = (string[])errorsList.ToArray(typeof(string));

				pageState.SearchResult.ClearErrors();
			}
		}

		/// <summary>
		/// Sets the help label moreUrl for the summary page,
		/// based on which type of result is being displayed.
		/// </summary>
		private void SetHelpText()
		{
			if ( itineraryExists )
			{
				// Show help for Extend Journey Results

				if ( extendInProgress )
				{
					// Show help for Extension Results
					SetHelpTextProperties("journeySummaryHelpLabelExtendExtension");
				}
				else
				{
					if (itineraryManager.Length == 1)
					{
						// Show help for initial Extend Journey page
						SetHelpTextProperties("journeySummaryHelpLabelExtendInitial");
					}
					else
					{
						// Show help for Extension added to Itinerary
						SetHelpTextProperties("journeySummaryHelpLabelExtendAdded");
					}
				}
			}
			else 
			{
				// Show help for non-extended Results

				if (tdSessionManager.IsFindAMode || tdSessionManager.FindAMode == FindAMode.Bus) 
				{
					// Show help for FindA Results

					FindAMode mode = tdSessionManager.FindAMode;

					switch(mode) 
					{
                        case FindAMode.Cycle:
                            // Show the help page instead of the inline help
                            SetHelpPageProperties("JourneySummary.HelpPageUrl.Cycle");
                            break;
						case FindAMode.Fare:
						case FindAMode.TrunkCostBased:
							try
							{
								FindCostBasedPageState pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
								if (pageState.SelectedTravelDate.TravelDate.TicketType == TicketType.Singles)
								{
									SetHelpTextProperties("journeySummaryHelpLabelFindFareSingles");
								}
								else
								{
									SetHelpTextProperties("journeySummaryHelpLabelFindFare");
								}
							}
							catch
							{
								// If we get here then the FindPageState is not of the correct type, which of course should never happen...
								SetHelpTextProperties("journeySummaryHelpLabelFindFare");
							}
							break;
						case FindAMode.Flight:
							SetHelpTextProperties("journeySummaryHelpLabelFlight");
							break;
						case FindAMode.Coach:
							SetHelpTextProperties("journeySummaryHelpLabelCoach");
							break;
						case FindAMode.Train:
							SetHelpTextProperties("journeySummaryHelpLabelTrain");
							break;
						case FindAMode.TrunkStation:
						case FindAMode.Trunk:
							SetHelpTextProperties("journeySummaryHelpLabelVariety");
							break;
						case FindAMode.Car:
							SetHelpTextProperties("journeySummaryHelpLabelCar");
							break;
						case FindAMode.Bus:
							SetHelpTextProperties("journeySummaryHelpLabelBus");
							break;
						default:
							SetHelpTextProperties("journeySummaryHelpLabel");
							break;
					}

				}
				else 
				{
					// Show help for Door-to-door Results
					SetHelpTextProperties("journeySummaryHelpLabel");
				}
			}
		}


		/// <summary>
		/// Sets the help label text and moreUrl for the supplied Help resource
		/// </summary>
		/// <param name="helpResource">Resource string for the Help text and also as the root for the resource for the "More" URL</param>
		private void SetHelpTextProperties(string helpResource) 
		{
			journeySummaryPageHelpLabel.Text = GetResource(helpResource);
			journeySummaryPageHelpLabel.MoreHelpUrl = GetResource(helpResource + ".moreURL");
		}

        /// <summary>
        /// Sets the help page url for the supplied Help resource
        /// </summary>
        /// <param name="helpPageUrlResource"></param>
        private void SetHelpPageProperties(string helpPageUrlResource)
        {
            // Ensure the help label isn't set
            theJourneyChangeSearchControl.HelpLabel = string.Empty;

            // Display the help button which takes user to the Help page
            theJourneyChangeSearchControl.HelpUrl = GetResource(helpPageUrlResource);
        }

		/// <summary>
		/// OnPreRender - updates the state of the controls.
		/// </summary>
		protected override void OnPreRender(System.EventArgs e)
		{

			itineraryManager = TDItineraryManager.Current;

			// If the screen is switching in either direction between the display of normal results and
			// the display of the Itinerary then the controls must be reset
			if (itineraryManager.ItineraryManagerModeChanged)
			{
				SetupControls();
			}

			SetupSkipLinksAndScreenReaderText();

			// Prerender setup for the AmendSaveSend control and its child controls

			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

			plannerOutputAdapter.AmendSaveSendControlPreRender(amendSaveSendControl);

			plannerOutputAdapter.PopulateResultsTableTitles(resultsTableTitleControlOutward, resultsTableTitleControlReturn);


			// Call base
			base.OnPreRender(e);
		}

		override protected void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
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
                //check for cycle result
                PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);
                outwardExists = plannerOutputAdapter.CycleExists(true);
                returnExists = plannerOutputAdapter.CycleExists(false);

                //check for normal result
				ITDJourneyResult result = tdSessionManager.JourneyResult;
				if(result != null) 
				{
					outwardExists = (((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid) || outwardExists;
					returnExists = (((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid) || returnExists;

					// Get time types for journey.
					outwardArriveBefore = tdSessionManager.JourneyViewState.JourneyLeavingTimeSearchType;
					returnArriveBefore = tdSessionManager.JourneyViewState.JourneyReturningTimeSearchType;
				}
			}
		}

		/// <summary>
		/// Set the visibility and data sources for the controls
		/// </summary>
		private void SetupControls()
		{
			DetermineStateOfResults();

			#region Initialise controls

            if (showFindA)
			{
                ModeType[] modeTypes = tdSessionManager.FindPageState.ModeType;
                
                if (outwardExists)
				{
					findSummaryResultTableControlOutward.Initialise(false, true, outwardArriveBefore, modeTypes);
				}
				
				if (returnExists)
				{
					findSummaryResultTableControlReturn.Initialise(false, false, returnArriveBefore, modeTypes);
				}
			}
			else 
			{
				if (outwardExists)
				{
					summaryResultTableControlOutward.Initialise(false, true, outwardArriveBefore);
				}
				
				if (returnExists)
				{
					summaryResultTableControlReturn.Initialise(false, false, returnArriveBefore);
				}
			}

			// Setup the AmendSaveSend control and its child controls

			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

			plannerOutputAdapter.AmendSaveSendControlPageLoad(amendSaveSendControl, this.pageId);

			this.theOutputNavigationControl.Initialise(pageId);

			// Set up correct mode for footnotes control
			if ( tdSessionManager.IsFindAMode )
			{
				if ( FindFareInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode) )
				{
					footnotesControl.CostBasedResults = true;
				}
				footnotesControl.Mode = tdSessionManager.FindAMode;
			}

			#endregion Initialise controls

			#region Set visibility of controls

            summaryOutwardTable.Attributes.Add("class", showFindA ? "jpsumoutfinda" : "jpsumout");
            summaryReturnTable.CssClass = (showFindA ? "jpsumrtnfinda" : "jpsumrtn");
            returnPanel.CssClass = "boxtypeeighteen";

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

				if (FindInputAdapter.IsCostBasedSearchMode(tdSessionManager.FindAMode))
				{
					// Set up controls that are visible in FindAFare mode
					SetFindFareVisible(true);
					findFareSelectedTicketLabelControl.PageState = tdSessionManager.FindPageState;
					findFareGotoTicketRetailerControl.PageState = tdSessionManager.FindPageState;

					FindCostBasedPageState pageState = (FindCostBasedPageState)tdSessionManager.FindPageState;
					if ((pageState.SelectedTravelDate != null) &&
						(pageState.SelectedTravelDate.TravelDate.TicketType == TicketType.Singles))
					{
						labelInstructions.Text = GetResource("JourneySummary.labelInstructions.FindAFareSingles");
					}
					else
					{
						labelInstructions.Text = GetResource("JourneySummary.labelInstructions.FindAFare");
					}
                    
                    labelInstructions2.Text = "<br/>" + GetResource("JourneySummary.labelInstructions2.FindAFare");

                    SetUpStepsControl();
				}
				else
				{
					panelInstructions.Visible = false;

					SetFindFareVisible(false);
				}

			if (!outwardExists && returnExists)
			{
				// Outward results DO NOT exist. Set visibility of all outward controls to false,
				// but only if there is a return journey.
				SetOutwardVisible(false);			
			}

			if (!returnExists)
			{
				// Return results DO NOT exist. Set visibility of all return controls to false.
				SetReturnVisible(false);
			}

			tdSessionManager.InputPageState.MapUrlOutward = String.Empty;
			tdSessionManager.InputPageState.MapUrlReturn  = String.Empty;

            SetJourneyOverviewBackButton();

			#endregion Set visibility of controls
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
			imageOutwardJourneysSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageReturnJourneysSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageFindTransportToStartLocationSkipLink.ImageUrl = SkipLinkImageUrl;
			imageFindTransportFromEndLocationSkipLink.ImageUrl = SkipLinkImageUrl;
			imageJourneyButtonsSkipLink2.ImageUrl = SkipLinkImageUrl;

			imageMainContentSkipLink1.AlternateText = 
				GetResource("JourneySummary.imageMainContentSkipLink.AlternateText");

			string journeyButtonsSkipLinkText = GetResource("JourneySummary.imageJourneyButtonsSkipLink.AlternateText");

			imageJourneyButtonsSkipLink1.AlternateText = journeyButtonsSkipLinkText;
			imageJourneyButtonsSkipLink2.AlternateText = journeyButtonsSkipLinkText;

			labelJourneyOptionsTableDescription.Text =	GetResource("JourneySummary.labelJourneyOptionsTableDescription.Text");


			// Only show the link to outward journeys portion of the screen if we have outward journeys on screen
			if (outwardExists)
			{
				panelOutwardJourneysSkipLink1.Visible = true;
				imageOutwardJourneysSkipLink1.AlternateText = 
					GetResource("JourneySummary.imageOutwardJourneysSkipLink1.AlternateText");

				if (itineraryExists)
				{
					panelFindTransportToStartLocationSkipLink.Visible = true;
					imageFindTransportToStartLocationSkipLink.AlternateText = 
						GetResource("JourneySummary.imageFindTransportToStartLocationSkipLink.AlternateText");

					panelFindTransportFromEndLocationSkipLink.Visible = true;
					imageFindTransportFromEndLocationSkipLink.AlternateText = 
						GetResource("JourneySummary.imageFindTransportFromEndLocationSkipLink.AlternateText");
				}
				else
				{
					panelFindTransportToStartLocationSkipLink.Visible = false;
					panelFindTransportFromEndLocationSkipLink.Visible = false;
				}
			}

			// Only show the link to return journeys portion of the screen if we have return journeys on screen
			if (returnExists)
			{
				panelReturnJourneysSkipLink1.Visible = true;
				imageReturnJourneysSkipLink1.AlternateText = 
					GetResource("JourneySummary.imageReturnJourneysSkipLink1.AlternateText");

			}
		}

		/// <summary>
		/// Sets the visibilities of the "Return" components.
		/// </summary>
		/// <param name="visible">True if return components should be visible
		/// and false if return components should not be visible.</param>
		//Added as part of vantive fix for situations when there is a return
		//result but no outward
		private void SetOutwardVisible(bool visible)
		{
			
			outwardPanel.Visible = visible;
			resultsTableTitleControlOutward.Visible = visible;
			summaryResultTableControlOutward.Visible = visible;
			findSummaryResultTableControlOutward.Visible = visible;
		}

		/// <summary>
		/// Sets the visibilities of the "Return" components.
		/// </summary>
		/// <param name="visible">True if return components should be visible
		/// and false if return components should not be visible.</param>
		private void SetReturnVisible(bool visible)
		{
			
			returnPanel.Visible = visible;
			resultsTableTitleControlReturn.Visible = visible;
			summaryResultTableControlReturn.Visible = visible;
			findSummaryResultTableControlReturn.Visible = visible;
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

            labelInstructions2.Visible = findFare; // hidden by default as the label may not be used by other modes for this page

            theJourneyChangeSearchControl.GenericBackButtonVisible = findFare;
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
            if (TDSessionManager.Current.FindAMode == FindAMode.Trunk)
                theJourneyChangeSearchControl.GenericBackButtonVisible = true;
        }

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//

			amendSaveSendControl.AmendViewControl.SubmitButton.Click += new EventHandler(AmendViewControl_Click);
//			this.commandSubmit.Click += new EventHandler(this.commandSubmit_Click);
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
            if (TDSessionManager.Current.FindAMode == FindAMode.Trunk)
                this.theJourneyChangeSearchControl.GenericBackButton.Click += new EventHandler(this.buttonBackJourneyOverview_Click);
            else
                this.theJourneyChangeSearchControl.GenericBackButton.Click +=new EventHandler(this.findFareBackButton_Click);
		}

		#endregion Web Form Designer generated code

		#region Event Handlers

		/// <summary>
		/// Event handler that is fired when the "OK" button is clicked on the amendViewControl.
		/// This will switch Session partitions and display Summary page with the appropriate results.
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void AmendViewControl_Click(object sender, EventArgs e)
		{
			PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter(tdSessionManager);

			plannerOutputAdapter.ViewPartitionResults(amendSaveSendControl.AmendViewControl.PartitionSelected);
		}

		/// <summary>
		/// Event handler to take user to Find Fare Ticket selection pages
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		protected void findFareBackButton_Click(object sender, EventArgs e)
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
        /// 
        /// </summary>
        private void ExtraWiringEvents()
        {

           socialBookMarkLinkControl.EmailLinkButton.Click += new EventHandler(EmailLink_Click);

           this.summaryResultTableControlOutward.SelectionChanged += new SelectionChangedEventHandler(summaryResultTableControlOutward_SelectionChanged);
           this.summaryResultTableControlReturn.SelectionChanged += new SelectionChangedEventHandler(summaryResultTableControlReturn_SelectionChanged);

           this.findSummaryResultTableControlOutward.SelectionChanged += new SelectionChangedEventHandler(summaryResultTableControlOutward_SelectionChanged);
           this.findSummaryResultTableControlReturn.SelectionChanged += new SelectionChangedEventHandler(summaryResultTableControlReturn_SelectionChanged);

        }





      


		#endregion Event Handlers

	}
}
