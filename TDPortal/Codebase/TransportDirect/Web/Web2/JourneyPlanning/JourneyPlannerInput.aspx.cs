// *********************************************** 
// NAME                 : JourneyPlannerInput.aspx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 22/09/2003 
// DESCRIPTION  : Journey Planner Input page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyPlannerInput.aspx.cs-arc  $ 
//
//   Rev 1.45   Mar 22 2013 10:49:18   dlane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.44   Feb 13 2013 17:06:54   dlane
//Don't check controls that are not even created due to arriving here having been redirected from the landing page
//Resolution for 5889: Accessible page landing issue
//
//   Rev 1.43   Jan 30 2013 13:47:56   mmodi
//Fixed showing advanced options on ambiguity page
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.42   Jan 24 2013 15:45:30   mmodi
//Minor update to load user preferences
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.41   Jan 17 2013 09:46:08   mmodi
//Updates to D2D advanced options for better js and non-js behaviour
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.40   Jan 15 2013 15:42:08   mmodi
//Page landing updates for accessible locations
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.39   Jan 15 2013 14:10:48   dlane
//Accessiblity updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.38   Jan 10 2013 16:16:14   DLane
//Door to door input options
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.37   Dec 11 2012 17:28:10   mmodi
//Display accessible option on ambiguity page
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.36   Dec 11 2012 14:00:06   mmodi
//Load favourite journey accessible options 
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.35   Dec 10 2012 15:46:46   mmodi
//Added accessible options landing parameters
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.34   Dec 05 2012 13:37:38   mmodi
//Updated setting accessible parameters
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.33   Nov 16 2012 14:00:42   DLane
//Setting walk parameters in the journey request
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.32   Nov 15 2012 16:20:18   mmodi
//Null check for accessible options property switch
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.31   Nov 15 2012 14:07:14   dlane
//Addition of accessibility options to journey plan input and user profile
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.30   Oct 30 2012 11:13:30   mmodi
//Hide advanced options when favourite journey loaded with no via location
//Resolution for 5852: Gaz - Favourite journeys are not displayed
//
//   Rev 1.29   Oct 04 2012 14:36:04   mmodi
//Updated comments and removed commented out code
//Resolution for 5857: Gaz - Code review updates
//
//   Rev 1.28   Sep 27 2012 14:46:26   mmodi
//Display favourite journey in new location suggest control
//Resolution for 5852: Gaz - Favourite journeys are not displayed
//
//   Rev 1.27   Sep 04 2012 11:17:02   mmodi
//Updated to handle landing page auto plan with the new auto-suggest location control
//Resolution for 5837: Gaz - Page landing autoplan links fail on Cycle input page
//
//   Rev 1.26   Aug 28 2012 10:21:54   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.25   Jul 28 2011 16:20:34   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.24   Mar 14 2011 15:12:10   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.23   Jul 29 2010 16:13:52   mmodi
//Changes to page layout and styles to be exactly consistent for all input pages in the Portal
//Resolution for 4760: IE7-find a car route check boxes
//
//   Rev 1.22   May 13 2010 13:05:28   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.21   Mar 26 2010 12:03:58   MTurner
//Added code to clear journey results from session if you are coming directly from another planner.
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.20   Feb 17 2010 15:13:56   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.19   Jan 29 2010 14:45:32   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.18   Jan 19 2010 13:21:04   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.17   Dec 14 2009 11:06:14   apatel
//stop the map showing when new location button click after amend
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.16   Dec 09 2009 11:34:00   mmodi
//When Clear button is clicked, reset the map
//
//   Rev 1.15   Dec 04 2009 08:49:00   apatel
//input page mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.14   Dec 03 2009 16:00:58   apatel
//input page mapping enhancement related changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.13   Dec 02 2009 11:54:12   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.12   Nov 30 2009 09:58:14   apatel
//input page find on map workflow changed
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.11   Nov 18 2009 11:42:12   apatel
//Added oneusekey for findonmap button click to move on to findmapinput page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.10   Nov 18 2009 11:20:40   apatel
//Updated for Journey input planner mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 10 2009 11:30:16   apatel
//Find Input pages mapping enhancement changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Feb 02 2009 17:14:06   mmodi
//Populate the Routing Guide flags
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.7   Dec 19 2008 15:07:26   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.6   Dec 08 2008 15:49:28   pscott
//SCR5199 - When logged in a users saved  preferences are overwriting later selected journey options. Preferences should only be read on start of new journey
//Resolution for 5199: Preferences (Set & Default) overwrite selected journey parameters
//
//   Rev 1.5   May 01 2008 17:23:24   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.4   Apr 08 2008 14:38:34   scraddock
//Advanced Text Not Hidden
//Resolution for 4847: Advanced Text Not Hidden
//
// rev devfactory 8 apr sbarker
// Advanced text hidden when advanced pressed, or in ambiguity mode
//
//   Rev 1.3   Apr 07 2008 11:28:20   scraddock
//See main check-in comment
//
//   Rev 1.2   Mar 31 2008 13:24:58   mturner
//Drop3 from Dev Factory
//
// REV DevFactory Apr 07 sbarker
// Fix to the journeyPlanner variable being null.
//
//   Rev DevFactory   Feb 04 2008 13:15:44   aahmed
//Added code to handle  button events from preferences controls
//white labelling and removed next button in advance mode and all other buttons according to CCN
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//  Rev DevFactory Feb 01 2008 09:42:05 sbarker 
//  revision problem with reading parent node when reading resx file for title text
//  changed page.id to pageName in LanguageHandler.cs and return an empty string. This is OK, since
//  the string is used to look up a value in a resx file.
//
//   Rev 1.0   Nov 08 2007 13:30:08   mturner
//Initial revision.
//
//   Rev 1.185   Sep 03 2007 15:25:22   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.184   Jun 07 2007 15:10:42   mmodi
//Added a new Next submit button
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.183   Feb 28 2007 14:56:56   jfrank
//CCN0366 - Enhancement to enable Page Landing from Word 2003 and to ignore session timeouts for Page Landing due to future usage by National Trust.
//Resolution for 4356: Word 2003 and National Trust Landing Page links
//
//   Rev 1.182   Oct 06 2006 16:48:16   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.181.1.0   Sep 22 2006 14:12:40   mmodi
//Added NewLocationClick events to allow journeyparameters "return locations" to be cleared which will hold Return car park location
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4196: Car Parking: New location button retains Car Park location
//
//   Rev 1.181   Aug 02 2006 14:39:20   tmollart
//Modified handling of paramters saving for car options to ensure they are saved on all post back events. Ensures that data retained in all circumstances.
//Resolution for 3520: Apps: Avoid/Use Roads data not saving to session
//
//   Rev 1.180   Apr 26 2006 15:36:20   asinclair
//Removed clearRoadEntered() as this code has now been put onto the control to allow it to be used elsewhere
//Resolution for 3974: DN068 Extend: 'Clear page' does not clear all values on Extend input page
//
//   Rev 1.179   Apr 20 2006 15:56:20   tmollart
//Removed DisableLocailityQuery method calls.
//Resolution for 3840: DN068 Replan:  JavaScript - No results returned when replanning a Car journey using find a car
//
//   Rev 1.178   Apr 05 2006 15:25:38   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.177   Mar 30 2006 10:37:24   halkatib
//Made page check if coming from landing page before initialising the journey paramters. Initialisation is not required on the page since the landing page does this already. When this happens twice in a landing page call the journeyparametes set by the landing are changed.
//
//   Rev 1.176   Mar 24 2006 17:03:26   halkatib
//Applied fixes to landing page functionalitt post phase 3 merge
//
//   Rev 1.175   Mar 23 2006 17:29:46   halkatib
//Removed unnecessary ambiguity search in submit method
//
//   Rev 1.174   Mar 22 2006 17:30:28   halkatib
//Changes due to Merge of stream3152 Landing Page phase 3
//
//   Rev 1.173   Mar 17 2006 17:01:26   RPhilpott
//Ensure all PT mode check-boxes are initially ticked if PT selected.
//Resolution for 3599: Del 8.1 Door to Door - Advanced Options transport types unchecked
//
//   Rev 1.172   Mar 14 2006 11:12:30   asinclair
//Manula merge of stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.171   Mar 10 2006 12:45:10   pscott
//SCR3510
//Close Calendar Control when going to Ambiguity page
//SCR 3592 - Mode Preferences
//
//   Rev 1.170   Feb 23 2006 19:36:40   AViitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.169   Feb 10 2006 18:20:36   kjosling
//Fixed merge
//
//   Rev 1.168   Feb 10 2006 12:24:54   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.167   Dec 07 2005 16:13:56   RWilby
//Added logic to
//a)Clear the ItineraryManager when redirected from VisitPlanner
//b)Only redirect to JourneySummary.aspx if the ItineraryManager is not of type VisitPlannerItineraryManager.
//Resolution for 3316: Home page: Server Error in '/Web' Application Home page to door-to-door after Visit planner
//
//   Rev 1.166.1.1   Dec 12 2005 17:00:00   tmollart
//Modified to use new initialise parameters method on session manager. Removed any code that redirects users to existing results.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.166.1.0   Nov 29 2005 16:07:40   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.166   Nov 24 2005 14:53:06   pcross
//Update after code review
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.165   Nov 23 2005 17:16:36   mtillett
//Remove obsolete code as part of the code review
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.164   Nov 22 2005 13:50:36   RGriffith
//IR3161 Resolution - Preventing via locations being validated before being loaded and update to TransportTypes behaviour for the first time it loads 
//Resolution for 3161: Home page: Advanced button from door-to-door miniplanner
//
//   Rev 1.163   Nov 22 2005 10:51:30   NMoorhouse
//Removed obsolete local date control update methods (code which has been moved into Adapter classes)
//Resolution for 3069: DN77 - Ambiguity pages not holding values
//
//   Rev 1.162   Nov 21 2005 18:31:14   RGriffith
//IR3041 Resolution - Correction of merge error to ensure Find On Map button appears
//
//   Rev 1.161   Nov 18 2005 14:29:48   RGriffith
//Fix to ensure TransportTypes check boxes are checked in accoradance to the PublicTransport required check box
//
//   Rev 1.160   Nov 17 2005 13:22:04   pcross
//Manual merge of stream2880
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.159   Nov 15 2005 20:52:44   rgreenwood
//IR2990 Wired up Help button
//Resolution for 2990: UEE Post Build Enhancement: Add Help Pages to Input Pages
//
//   Rev 1.158   Nov 15 2005 16:16:26   RGriffith
//Resolution for IR3041 - Public Transport "Find on Map" button was dissappearing once pressed and page was returned to.
//
//   Rev 1.157   Nov 10 2005 14:32:54   NMoorhouse
//TD093 UEE Input Pages - Soft Content
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.156   Nov 09 2005 16:35:50   mguney
//Merge for stream 2818.
//
//   Rev 1.155.1.4   Nov 16 2005 11:14:42   NMoorhouse
//UEE Home Page - Added Public & Private links (back on) to the page
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.155.1.3   Nov 14 2005 13:13:48   pcross
//Fix to ensure link from Plan a Journey More... from Homepage always goes to Door to Door (not a FindA page)
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.155.1.2   Nov 10 2005 16:44:44   RGriffith
//Bug fix to show Hide button when not showing the Advanced Options button.
//
//   Rev 1.155.1.1   Nov 09 2005 14:51:24   RGriffith
//Added check for SessionOneUse Key from PlanAJourneyControl to set journeyParameters.PublicModes
//
//   Rev 1.155.1.0   Nov 07 2005 19:18:36   schand
//Merged stream2880 with the trunk for FindAPlaceControl
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.155   Nov 03 2005 15:15:24   mtillett
//stream2816 changes merged
//
//   Rev 1.154   Nov 02 2005 17:31:26   halkatib
//Merged changes from stream2877
//Resolution for 2937: Stream 2877 (Landing Page - Phase 2) Merge
//
//   Rev 1.153   Nov 01 2005 10:07:44   tolomolaiye
//Merge for stream 2638 (Visit Planner)
//Resolution for 2929: Visit Planner Merge Activity
//
//   Rev 1.148.3.1   Oct 26 2005 19:10:06   asinclair
//Added comments
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.148.3.0   Oct 19 2005 17:46:52   asinclair
//Now using TransportTypesControl
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.151   Sep 29 2005 17:18:18   halkatib
//Merged Landing page code
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.150   Sep 29 2005 10:43:50   schand
//Merged stream 2673 back into trunk
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.149   Sep 26 2005 09:18:28   MTurner
//Resolution for IR 2776 - Amended handling of TabSection to prevent exceptions being thrown if this has not already been set by another page.
//
//   Rev 1.148   Aug 16 2005 14:55:32   jgeorge
//Automatically merged from branch for stream2556
//
//   Rev 1.147.1.0   Jun 29 2005 10:44:02   jbroome
//Added AccessibilityLinkForInputControl
//Resolution for 2556: DEL 8 Stream: Accessibility Links
//
//   Rev 1.147   May 19 2005 16:51:02   ralavi
//Changes as the result of running FXCop.
//
//   Rev 1.146   May 10 2005 17:54:32   Ralavi
//Using a helper class to validate fuel consumption and fuel costs.
//
//   Rev 1.145   May 04 2005 15:23:42   Ralavi
//Replacing two error messages for each avoid/use roads by one error message for both.
//
//   Rev 1.144   Apr 29 2005 17:17:36   Ralavi
//Removed two lines which pass avoidRoads and UseRoads to journeyParameters in WriteToSession method because this was overwriting the values in journey parameters when accessing favourite journeys.
//
//   Rev 1.143   Apr 28 2005 14:52:46   rscott
//Fix for IR2348 added.
//
//   Rev 1.142   Apr 28 2005 14:25:42   rscott
//Changes to fix IR2360
//
//   Rev 1.141   Apr 26 2005 09:22:38   Ralavi
//Passing values from journeyParameters to the control for AvoidRoads and UseRoads and vice versa in order to fix the problems  with AvoidRoad and UseRoads
//
//   Rev 1.140   Apr 22 2005 14:43:34   tmollart
//Changed the following:
//- Modified PageLoad so header is not displayed twice (CMO).
//- Added resetDone flag to tell when journey params have been reset.
//- Made load of user preferences conditional on resetDone flag.
//- Remove surplus calls to RefreshPage - now called once in PreRender event handler.
//- Removed calls that loaded use preferences when preferences panels were made visible as this overwrote user changes. No done once when params reset.
//- Code to load user preferences in PreRender refactored into a new method called LoadUserPreferences.
//
//Resolution for 2251: Door-to-door Logged In With Do Not Use Motorways Selected Cannot Unselect
//
//   Rev 1.139   Apr 20 2005 16:07:22   COwczarek
//Fix to ensure correct header displayed when redirecting from home page.
//Resolution for 2079: PT - Extend journey does not work with PT cost based searches
//
//   Rev 1.138   Apr 20 2005 13:44:44   Ralavi
//Fixing IRs for fuel consumption and fuel cost related IRs 2006, 2009 and 2215.
//
//   Rev 1.137   Apr 15 2005 15:18:42   rscott
//Code added for IR1980
//
//   Rev 1.136   Apr 15 2005 12:48:22   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.135   Apr 14 2005 12:01:44   pcross
//IR2180. Updated skip links for changed DEL7 screen
//
//   Rev 1.134   Apr 13 2005 12:06:48   Ralavi
//Added code for validating fuel consumption and costs
//
//   Rev 1.133   Apr 05 2005 10:04:38   Ralavi
//Fixes to ensure that fuel consumption is converted correctly and avoid Motorway, Tolls and Ferries are saved as favourite journey
//
//   Rev 1.132   Apr 01 2005 13:27:42   Ralavi
//Changes to correctly convert fuel consumption
//
//   Rev 1.131   Mar 30 2005 17:07:44   RAlavi
//Car costing changes
//
//   Rev 1.130   Mar 23 2005 11:21:40   RAlavi
//Modifications for clear button for car costing
//
//   Rev 1.129   Mar 23 2005 09:37:44   rscott
//Help text added 
//
//   Rev 1.128   Mar 18 2005 11:17:34   RAlavi
//Fixing door to door problems for car costing
//
//   Rev 1.127   Mar 10 2005 11:34:10   PNorell
//Fixed bug with expand preferences buttons.
//
//   Rev 1.126   Mar 08 2005 09:31:08   RAlavi
//Checked in after adding code for saving preferences
//
//   Rev 1.124   Feb 23 2005 16:37:26   RAlaviload
//Checked in after integrating car costing controls
//
//   Rev 1.123   Oct 15 2004 12:39:16   jgeorge
//Changed to take account of new JourneyPlanStateData and changes to existing JourneyPlanControlData.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.122   Sep 21 2004 17:28:46   jbroome
//IR 1596 - Set ExtendEndOfItinerary value in TDJourneyParameters when searching for extension.
//
//   Rev 1.121   Sep 17 2004 12:12:44   jbroome
//IR 1591 - Extend Journey Usability Changes
//
//   Rev 1.120   Sep 09 2004 13:38:06   RHopkins
//IR1543 Added Extend journey help.
//IR1559 Go straight back to Initial Results when undoing an Extension if the User has not seen the Extend Journey Summary page.
//
//   Rev 1.119   Sep 06 2004 11:35:18   passuied
//Undid changes to change displayed imageButtons TravelDetails and Route options in extended journey case.
//Resolution for 1379: Driving and via PT are within wrong sections in Extend's  Find transport from/to page
//
//   Rev 1.118   Sep 02 2004 15:56:56   passuied
//Modified Load Preferences to be able to load eitherway if the data is stored as object or string  and modified Save Preferences (when needed) to save data as objects (not as strings)
//
//   Rev 1.117   Aug 24 2004 18:10:18   RPhilpott
//StationType is now passed into LocationSearchHelper (but always Undetermined for multimodal requests).
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.116   Aug 20 2004 16:26:32   passuied
//In ambiguitySearch() method, set the newly created LocationSearch properties : InputText, searchType, and fuzzy to what the user has typed (from JourneyParameters)
//Resolution for 1240: Gazetteer default on JP input page set to address postcode
//
//   Rev 1.115   Aug 19 2004 20:37:38   RHopkins
//IR1102 Change the layout of the buttons so that when in Extend mode the "Back" button can say "Back to journey summary".
//
//   Rev 1.114   Aug 17 2004 09:24:02   COwczarek
//Save the current Find A mode and journey parameters at the
//point of commencing a new journey plan by calling
//SaveCurrentFindAMode method.
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.113   Aug 02 2004 13:38:38   COwczarek
//Replace hardcoded key values with constants defined in ProfileKeys class.
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.112   Jul 29 2004 11:52:10   passuied
//Replace use of CloseSingleWindows with TDPage static method CloseAllSingleWindows
//
//   Rev 1.111   Jul 16 2004 12:36:02   RHopkins
//IR1101 Removed help button from "From" and "To" location boxes if the respective location is fixed (e.g. an Extend is in progress)
//
//   Rev 1.110   Jul 14 2004 13:00:32   passuied
//Changes in SessionManager with impact in Web for Del 6.1
//Compiles
//
//   Rev 1.109   Jul 14 2004 12:42:04   CHosegood
//Added invisible links
//Resolution for 1167: Add invisible links at top of page to page sections [WAI 3.1.11]
//
//   Rev 1.108   Jul 12 2004 18:56:20   JHaydock
//DEL 5.4.7 Merge: IR 1089
//
//   Rev 1.107   Jun 29 2004 11:32:10   jgeorge
//Updated code to check for valid journey results
//
//   Rev 1.106   Jun 28 2004 20:49:02   JHaydock
//JourneyPlannerInput clear page and back buttons for extend journey
//
//   Rev 1.105   Jun 24 2004 14:47:04   jgeorge
//Changed to use ChangeJourneyParametersType of TDSessionManager
//
//   Rev 1.104   Jun 23 2004 16:56:26   COwczarek
//Changes for performing date validation on journey extensions.
//Journey plan runner will be called to ignore date validation for
//journey extensions.
//Resolution for 1044: Add date validation to extend journey functionality
//
//   Rev 1.103   Jun 10 2004 16:44:10   esevern
//Added alternative button images for public and car travel options when extending journey
//
//   Rev 1.102   Jun 09 2004 17:04:40   esevern
//removed setting of amendstopover title text as this is now done in the control itself
//
//   Rev 1.101   Jun 08 2004 09:56:30   esevern
//removed reset origin/destination selection - corrects error where locations were lost when performing extension journey searches
//
//   Rev 1.100   Jun 04 2004 14:38:42   RPhilpott
//Use DataServices to get default location type instead of hard-coding.
//
//   Rev 1.99   Jun 04 2004 13:42:54   RHopkins
//Added call to AmendStopoverControl.UpdateRequestedTimes() so that times are updated when stopover control is used.
//
//   Rev 1.98   Jun 01 2004 10:00:44   jbroome
//IR972
//Allow Partial Postcode searching directly from Journey Planner.
//Previously, this was only allowed via the location maps.
//
//   Rev 1.97   May 26 2004 12:45:44   RHopkins
//The code that redirects to Summary page if valid Results exist has been extended to also check for presence of Itinerary.
//
//   Rev 1.96   May 26 2004 10:20:08   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.95   May 25 2004 11:05:30   ESevern
//Extend journey additions
//
//   Rev 1.94   Apr 28 2004 16:20:16   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.93   Apr 27 2004 13:55:42   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.92   Apr 19 2004 15:28:12   ESevern
//Now refreshes location details if location is valid but walk time/speed has changed
//Resolution for 696: Nearest naptans not re-evaluated when walking parameters changed
//
//   Rev 1.91   Apr 08 2004 14:46:18   COwczarek
//The Populate method of the BiStateLocationControl now
//accepts location type (e.g. origin, destination, via). Pass this
//value to specify the purpose of the control.
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.90   Apr 06 2004 15:18:58   ESevern
//corrections to previous fix - page refresh was causing load favourites to fail
//
//   Rev 1.89   Apr 05 2004 15:32:36   ESevern
//Added logged in event when registered for correct display of login/favourite controls
//Resolution for 719: Login/Register link visibility error
//
//   Rev 1.88   Apr 05 2004 14:40:56   ESevern
//hides pre logged in control whenever page refreshed 
//
//   Rev 1.87   Mar 26 2004 12:10:54   ESevern
//DEL5.2 QA changes - resolution for IR665
//
//   Rev 1.86   Mar 26 2004 11:26:26   COwczarek
//Journey parameters are saved prior to moving to abiguity page to allow them to be reinstated if user cancels changes made during ambiguity resolution.
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location
//
//   Rev 1.85   Mar 19 2004 16:23:28   COwczarek
//After loading a favourite journey, if loaded journey contains advanced route options then update RouteOptionsChanged collection to indicate they have been "modified" so that they appear visible on the ambiguity page
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.84   Mar 18 2004 15:26:04   AWindley
//DEL5.2 Modifications to label text following Resolution 649 changes
//
//   Rev 1.83   Mar 17 2004 14:55:12   ESevern
//DEL5.2 - additional help buttons for public and car advanced route options
//
//   Rev 1.82   Mar 16 2004 18:27:40   COwczarek
//Replace location select control for all locations (to, from, public
//via, private via) with new bi state location control. This new
//control encapsulates the location select control and location
//display control. It displays locations resolved from the map
//using the location display control. Unresolved locations or
//those resolved not using the map are displayed using the
//location select control.
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.81   Mar 12 2004 13:30:26   COwczarek
//Fix the previous check in corrupted by PVCS
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.80   Mar 10 2004 12:55:46   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.79   Mar 03 2004 15:53:34   COwczarek
//Don't show map button for public via location input
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.78   Mar 02 2004 16:34:32   asinclair
//Fixed Netscape HTML issues for Del 5.2
//
//   Rev 1.77   Feb 24 2004 09:56:50   esevern
//DEL 5.2 format changes (login/register less prominent, travel preferences title removed)
//
//   Rev 1.76   Feb 20 2004 11:30:44   esevern
//DEL5.2 changes relating to journey planner input page format
//
//   Rev 1.75   Feb 16 2004 14:27:50   esevern
//DEL5.2: added storing fave journey name and check box selection in viewstate to fix Redirect removing journey name
//
//   Rev 1.74   Feb 13 2004 13:29:24   esevern
//DEL 5.2 seperation of login and register
//
//   Rev 1.73   Jan 23 2004 16:03:26   PNorell
//Favourite journey updates.
//
//   Rev 1.72   Jan 21 2004 11:33:08   PNorell
//Update to 5.2
//
//   Rev 1.71   Jan 08 2004 16:18:42   passuied
//displayed Default locationtype when map LocationSearch input text is empty, NoMatch otherwise
//Resolution for 581: Map button on JP Page produces unnecessary error message
//
//   Rev 1.70   Jan 05 2004 13:34:26   passuied
//reset search and location before loading favourite journey
//Resolution for 565: Retrieving a saved journey does not clear the ambiguity fields
//
//   Rev 1.69   Dec 18 2003 12:24:54   JHaydock
//Formatting changes for DEL 5.1
//
//   Rev 1.68   Dec 16 2003 15:18:30   JHaydock
//Unit test bugs fixed
//
//   Rev 1.67   Dec 15 2003 12:41:50   kcheung
//Del 5.1 Journey Planner Location Map Updates after link testing on dev build 22.
//
//   Rev 1.66   Dec 12 2003 17:26:30   JHaydock
//DEL 5.1 DfT formatting changes
//
//   Rev 1.65   Dec 10 2003 10:58:52   JHaydock
//Adjustment so that Ambiguity page only displays sections that have changed
//
//   Rev 1.64   Dec 04 2003 13:09:34   passuied
//final version for del 5.1
//
//   Rev 1.63   Dec 01 2003 10:10:54   passuied
//fixed problem with loading of PrivateAlgorithm type preference
//Resolution for 454: One preference not being correctly saved
//
//   Rev 1.62   Nov 27 2003 17:54:20   passuied
//changed calls to SetUpLocationSearch so it specifies if the location accepts the postcodes
//Resolution for 428: Public Via shouldn't accept postcodes
//
//   Rev 1.61   Nov 26 2003 18:43:54   PNorell
//Fixed for IR317
//
//   Rev 1.60   Nov 26 2003 11:36:36   passuied
//retrieved channel language to pass to ValidateAndRun
//Resolution for 397: Wrong language passed to JW
//
//   Rev 1.59   Nov 26 2003 11:20:30   asinclair
//Changes made to Alt text strings
//
//   Rev 1.58   Nov 18 2003 16:22:38   passuied
//Fixed bug : saved details are only restored when user logs in or when page is cleared.
//
//Otherwise, keep taking what's in the journeyParameters.
//
//Also, LoadSaveTravelDetails updates JourneyParameters, not controls on page (they are refreshed automatically with refresh page)
//Resolution for 252: Walking time reverts to default when journey amended
//
//   Rev 1.57   Nov 18 2003 11:49:08   passuied
//missing comments
//
//   Rev 1.56   Nov 18 2003 10:44:14   passuied
//changes to hopefully pass code review
//
//   Rev 1.55   Nov 17 2003 17:36:46   passuied
//changes after HTML validation
//
//   Rev 1.54   Nov 16 2003 13:00:48   hahad
//Change Title Tag so that it reads value from asp:literal rather then value being hardcoded within the page
//
//   Rev 1.53   Nov 15 2003 16:17:40   PNorell
//Updated to close all ISingleWindows when clear button is pressed.
//
//   Rev 1.52   Nov 07 2003 12:19:30   esevern
//added check for travel preference values being set as may have say fares or travel news prefs but no travel detail preferences
//
//   Rev 1.51   Nov 06 2003 13:08:18   passuied
//removed error display
//
//   Rev 1.50   Nov 05 2003 17:12:40   passuied
//fixed Clear Page action... Wasn't clearing everything
//
//   Rev 1.49   Nov 05 2003 16:52:58   passuied
//fixed TravelDetails and RouteOptions buttons display
//
//   Rev 1.48   Nov 04 2003 16:16:30   passuied
//fixed update of checkboxes on page. were not reset when Clear Page
//
//   Rev 1.47   Nov 04 2003 15:10:18   passuied
//Changes : Whenever a travel detail changes on Input page, display Travel details panel on Ambiguity page
//Whenever a route options changes (but not empty) on Input page, display route options panel on ambiguity page
//
//   Rev 1.46   Nov 03 2003 15:43:14   passuied
//implemented anchorage for help and input page
//
//   Rev 1.45   Oct 31 2003 16:41:18   passuied
//big bug fixed for favourite journeys. Was saving New JourneyName when wasn't saving favourite!
//
//   Rev 1.44   Oct 30 2003 11:22:18   passuied
//fixed bug with old journey/new journey name that can be empty (default or old journey instead)
//
//   Rev 1.43   Oct 29 2003 14:24:40   passuied
//show/hide help and label bug fix
//
//   Rev 1.42   Oct 29 2003 14:07:14   passuied
//fixed Load favourite bug
//
//   Rev 1.41   Oct 29 2003 12:33:42   passuied
//BCN002 implementation. Removed alternate locations
//
//   Rev 1.40   Oct 28 2003 10:39:10   passuied
//changes after fxcop
//
//   Rev 1.39   Oct 27 2003 16:13:26   passuied
//Build2 integration tidied up and optimised... still working
//
//   Rev 1.38   Oct 27 2003 14:27:34   passuied
//build 2 integration in Input pages debugged
//
//   Rev 1.37   Oct 24 2003 18:08:52   passuied
//First complete working version of Build2 Input
//
//   Rev 1.36   Oct 24 2003 16:02:16   passuied
//Build2 max reached implementation before major changes
//
//   Rev 1.35   Oct 24 2003 11:42:32   passuied
//Build 2 for ambiguity page first working version
//
//   Rev 1.34   Oct 23 2003 15:27:48   esevern
//added check for journey names not being empty
//
//   Rev 1.33   Oct 23 2003 15:22:00   esevern
//added setting of old/new journey names if ambiguity, corrected call to save journey, set journeyparameters.savedetails' when saving travel preferences
//
//   Rev 1.32   Oct 22 2003 15:44:14   passuied
//minor changes (No return & imageUrl)
//
//   Rev 1.31   Oct 22 2003 12:20:22   RPhilpott
//Improve CJP error handling
//
//   Rev 1.30   Oct 21 2003 15:30:42   esevern
//added setting of old and new favourite journey names in journey parameters
//
//   Rev 1.29   Oct 17 2003 14:01:06   esevern
//amended submit_click to only save journeys if no ambiguity
//
//   Rev 1.28   Oct 17 2003 10:24:28   esevern
//completed user preference additions
//
//   Rev 1.27   Oct 10 2003 11:15:26   passuied
//change : if !PublicModesRequired, No Public modes!
//
//   Rev 1.26   Oct 10 2003 11:04:46   esevern
//interim checkin for hand over
//
//   Rev 1.25   Oct 08 2003 15:26:02   PNorell
//Removed server map path.
//
//   Rev 1.24   Oct 03 2003 16:59:34   passuied
//session reference bug fixed
//
//   Rev 1.23   Oct 02 2003 16:54:06   passuied
//updated	
//
//   Rev 1.22   Oct 01 2003 13:14:50   passuied
//bug fix
//
//   Rev 1.21   Sep 30 2003 17:30:26   passuied
//handled the no results from cjp and return to input page case
//
//   Rev 1.20   Sep 30 2003 15:23:04   PNorell
//Added support for ensuring only one "window" open on a web page at the same time.
//Fixed numerous click bug in the Help control.
//Fixed numerous language issues with the help control.
//Updated the journey planner input pages to contain the updated code for ensuring one window.
//Updated the wait page and took out the debug logging.
//
//   Rev 1.19   Sep 29 2003 16:14:32   passuied
//updated
//
//   Rev 1.18   Sep 26 2003 20:05:56   abork
//INTEGRATED HTML UPDATE
//
//   Rev 1.17   Sep 26 2003 11:10:22   passuied
//implemented hide/show button text
//
//   Rev 1.16   Sep 25 2003 18:36:32   passuied
//Added missing headers

using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using TransportDirect.CommonWeb.Helpers;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Journey Planner Input page
	/// </summary>
	/// 
	public partial class JourneyPlannerInput : TDPage
	{
		#region  Instance members

        protected System.Web.UI.WebControls.CheckBoxList checklistModesPublicTransport;
		protected System.Web.UI.WebControls.Label labelPublicModesNote;
		protected System.Web.UI.WebControls.CheckBox saveTravelBox;
		protected System.Web.UI.WebControls.Label labelPublicModesTitle;
		protected System.Web.UI.WebControls.Panel panelRouteOptionsLabel;
		protected System.Web.UI.WebControls.PlaceHolder holderTravelPreferencesFavourites;
      
		protected TransportDirect.UserPortal.Web.Controls.FavouriteLoadOptionsControl FavouriteLoadOptions;
		protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl dateControl;

        // Advanced options
        protected TransportDirect.UserPortal.Web.Controls.AccessibleOptionsControl accessibleOptionsControl;
        protected TransportDirect.UserPortal.Web.Controls.D2DTransportTypesControl transportTypesControl;
        protected TransportDirect.UserPortal.Web.Controls.D2DPTPreferencesControl ptPreferencesControl;
        protected TransportDirect.UserPortal.Web.Controls.D2DCarPreferencesControl carPreferencesControl;
        protected TransportDirect.UserPortal.Web.Controls.D2DCarJourneyOptionsControl journeyOptionsControl;
		

		protected HeaderControl headerControl;

		// private variables
		private ControlPopulator populator;
		private TDJourneyParametersMulti journeyParameters;
		private InputPageState inputPageState;

		/// <summary>
		/// Helper class responsible for common methods to non-Find A pages
		/// </summary>
		private LeaveReturnDatesControlAdapter inputDateAdapter;

		/// <summary>
		/// Helper class for Landing Page functionality
		/// </summary>
		private LandingPageHelper landingPageHelper = new LandingPageHelper();
        
		// Declaration of search/location object members
		private LocationSearch originSearch;
		private LocationSearch destinationSearch;
		private LocationSearch privateViaSearch;
		private LocationSearch publicViaSearch;
        
		private TDLocation destinationLocation;
		private TDLocation originLocation;
		private TDLocation privateViaLocation;
		private TDLocation publicViaLocation;

		private LocationSelectControlType privateViaType;
		private LocationSelectControlType publicViaType;
        	
		
        // Indicates if the location controls should display the "more options" visible
        private bool showLocationMoreOptionsExpanded = false;

		// Set if the parameters from the journeyOptionsControl have already been saved.
		private bool journeyOptionsSaved = false;

		#endregion	
		
		#region Constructor
		/// <summary>
		/// 
		/// </summary>
		public JourneyPlannerInput()
		{
			pageId = PageId.JourneyPlannerInput;
			// Get DataServices from Service Discovery
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}

		#endregion

        #region Page_Init, Page_Load, OnPreRender, OnUnload

        /// <summary>
        /// Performs page initialisation including event wiring.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            journeyOptionsControl = carPreferencesControl.JourneyOptionsControl;

            carPreferencesControl.JourneyTypeChanged += new EventHandler(preferencesControl_JourneyTypeChanged);
            carPreferencesControl.SpeedChanged += new EventHandler(preferencesControl_SpeedChanged);
            carPreferencesControl.CarSizeChanged += new EventHandler(preferencesControl_CarSizeChanged);
            carPreferencesControl.FuelTypeChanged += new EventHandler(preferencesControl_FuelTypeChanged);
            carPreferencesControl.ConsumptionOptionChanged += new EventHandler(preferencesControl_FuelConsumptionOptionChanged);
            carPreferencesControl.SpecificFuelUseUnitChanged += new EventHandler(preferencesControl_FuelUseUnitChanged);
            carPreferencesControl.FuelCostOptionChanged += new EventHandler(preferencesControl_FuelCostOptionChanged);

            carPreferencesControl.FuelConsumptionTextChanged += new EventHandler(preferencesControl_FuelConsumptionTextChanged);
            carPreferencesControl.FuelCostTextChanged += new EventHandler(preferencesControl_FuelCostTextChanged);

            journeyOptionsControl.AvoidMotorwaysChanged += new EventHandler(preferencesControl_AvoidMotorwaysChanged);
            journeyOptionsControl.AvoidTollsChanged += new EventHandler(preferencesControl_AvoidTollsChanged);
            journeyOptionsControl.AvoidFerriesChanged += new EventHandler(preferencesControl_AvoidFerriesChanged);
            journeyOptionsControl.BanLimitedAccessChanged += new EventHandler(preferencesControl_BanLimitedAccessChanged);

            carPreferencesControl.DoNotUseMotorwayChanged += new EventHandler(preferencesControl_DoNotUseMotorwaysChanged);
            ptPreferencesControl.JourneyChangesOptionsControl.ChangesOptionChanged += new EventHandler(preferencesControl_PtJourneyChanges);
            ptPreferencesControl.JourneyChangesOptionsControl.ChangeSpeedOptionChanged += new EventHandler(preferencesControl_PtChangesSpeed);
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDurationOptionChanged += new EventHandler(preferencesControl_PtWalkDuration);
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeedOptionChanged += new EventHandler(preferencesControl_PtWalkSpeed);

            FavouriteLoadOptions.FavouriteLoggedIn.LoadFavourite += new FavouriteLoggedInControl.LoadFavouriteEventHandler(LoadFavourite);
        }

        /// <summary>
		/// Page_PreRender
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
        }

        /// <summary>
		/// Page_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;

			// Used for JPLanding from Word 2003
			// If the session id in the url doesn't match the current session id.
			if (Request.QueryString["SID"] != null && TDSessionManager.Current.Session.SessionID != Request.QueryString["SID"])
			{
				//Call method to copy the data from the url's session to the current session.
				TDSessionSerializer tdSS= new TDSessionSerializer();
				tdSS.UpdateToDeferredSession(TDSessionManager.Current.Session.SessionID, Request.QueryString["SID"]);
			}

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            if (!Page.IsPostBack)
            {
                //Clear ItineraryManager when redirected from VisitPlanner
                if (TDItineraryManager.Current is VisitPlannerItineraryManager || TDItineraryManager.Current is ReplanItineraryManager)
                    TDItineraryManager.Current.NewSearch();

                #region Clear cache of journey data
                ClearCacheHelper helper = new ClearCacheHelper();

                // Force clear of any printable information if added by the journey result page
                helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

                // Fix for IR5481 Session issue when going from FAT to D2D using the left hand menu
                if (TDSessionManager.Current.FindAMode != FindAMode.None || TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.None)
                {
                    // We have come directly from another planner so clear results from session.
                    helper.ClearJourneyResultCache();
                }
                #endregion
            }

            bool isFromCarParks = IsFromCarParks();

            bool loadUserPreferences = true;
			// Do not load user preferences or reset journey parameters if coming from landing page, 
            // since the journey parameters  have already been initialised. 
            // Or if arrived from Find a Car park, or International planner
			if ((TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ])
                || (isFromCarParks)
                || (TDSessionManager.Current.GetOneUseKey(SessionKey.InternationalPlannerInput) != null))
			{
                loadUserPreferences = false;
			}
			else
			{
                loadUserPreferences = sessionManager.InitialiseJourneyParameters(FindAMode.None);
			}

			// This is known because FindAMode.None always gives us Multi
			journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
			inputPageState = sessionManager.InputPageState;

			sessionManager.InputPageState.JourneyInputReturnStack.Clear();

			// Getting search and location objects from session
			LoadSessionVariables();
                        
			#region Plan A Journey Control Functionality
			// Code used to detect a OneUse session key from the Plan A Journey control to determine
			// whether public modes transport values are required to be checked or unchecked.
			if (TDSessionManager.Current.GetOneUseKey(SessionKey.PublicModesRequired) != null)
			{
				// Initialise the Via parameters so they are not marked as 
				// invalid when coming from the Homepage.
				InitViaParameters();
				
				if (TDSessionManager.Current.GetOneUseKey(SessionKey.PublicModesRequired) == "true")
				{
					// Populate Transport Types checkBoxes
					populator.LoadListControl(DataServiceType.PublicTransportsCheck, transportTypesControl.ModesPublicTransport);
				}
				else
				{
					// UnPopulate Transport Types checkBoxes
					transportTypesControl.PublicModes = new ModeType[0];
				}

				// Set the transportModes values in session
				journeyParameters.PublicModes = transportTypesControl.PublicModes;
			}
			else if	(journeyParameters.PublicModes.Length == 0)
			{
				// if the one-use key hasn't been set, ensure that all 
				//  PT modes are ticked if the list is currently empty 
				populator.LoadListControl(DataServiceType.PublicTransportsCheck, transportTypesControl.ModesPublicTransport);
                journeyParameters.PublicModes = transportTypesControl.PublicModes;
			}
            // Check if location controls should display with expanded options
            if (TDSessionManager.Current.GetOneUseKey(SessionKey.ExpandOptionsRequired) != null)
            {
                if (TDSessionManager.Current.GetOneUseKey(SessionKey.ExpandOptionsRequired) == "true")
                {
                    showLocationMoreOptionsExpanded = true;
                }
            }
			#endregion

            InitialiseLocationControls();

			publicViaType = journeyParameters.PublicViaType;
			privateViaType = journeyParameters.PrivateViaType;

            if (!Page.IsPostBack && loadUserPreferences)
			{
				LoadUserPreferences();
			}

			// if first time page displayed, or accessed through a redirection
			if (Page.IsPostBack)
			{
				journeyParameters.DefaultValues = false;

				WriteToSession();
                
                journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;
					
				InitViaParameters();
				CarCostingFuelValidationHelper FuelValidation = new CarCostingFuelValidationHelper();
				//Validate fuel consumption entered by the user
				FuelValidation.FuelConsumptionValidation(journeyParameters, carPreferencesControl);
				//Validate fuel cost entered by the user
				FuelValidation.FuelCostValidation(journeyParameters, carPreferencesControl);
			}

            SetupResources();

			InitialiseControls();
							
			//DN079 UEE
			//Adding client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);  

			#region Landing Page Functionality

			//Check if we need to initiate a search due to Landing Page Mode
			if ( TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ] )
			{				
				//Check Auto Plan Flag to see if automatic submit should be performed
				//make sure that both the origin and data are provided so that autoplan will return a result
				if (TDSessionManager.Current.Session[ SessionKey.LandingPageAutoPlan ] && TDSessionManager.Current.Session[ SessionKey.LandingPageBothDataNotNull ])
				{
					//reset session parameters
					landingPageHelper.ResetLandingPageSessionParameters();

                    // Update location control resolve flag as this is a landing page request so don't want to validate,
                    // the landing page will have validated
                    if (originLocation != null && originLocation.Status == TDLocationStatus.Valid)
                    {
                        originLocationControl.ResolveLocation = false;
                    }
                    if (destinationLocation != null && destinationLocation.Status == TDLocationStatus.Valid)
                    {
                        destinationLocationControl.ResolveLocation = false;
                    }

                    SubmitRequest(loadUserPreferences);
				}	

				//reset landing page session parameters
				landingPageHelper.ResetLandingPageSessionParameters();	
			}
			#endregion	

            #region Car Parks Functionality
            if (isFromCarParks)
            {
                // Clear any car park related flags
                TDSessionManager.Current.FindCarParkPageState.IsFromDoorToDoor = false;
                TDSessionManager.Current.FindCarParkPageState.CurrentFindMode = FindCarParkPageState.FindCarParkMode.Default;
                TDSessionManager.Current.FindCarParkPageState.CarParkFindMode = FindCarParkPageState.FindCarParkMode.Default;

                // Update location control resolve flag as this is from car parks so don't want to validate,
                // assume car parks page will have validated
                if (originLocation != null && originLocation.Status == TDLocationStatus.Valid)
                {
                    originLocationControl.ResolveLocation = false;
                }
                if (destinationLocation != null && destinationLocation.Status == TDLocationStatus.Valid)
                {
                    destinationLocationControl.ResolveLocation = false;
                }

                SubmitRequest(loadUserPreferences);
            }
            #endregion

            // Setup navigation menus
            ConfigureLeftMenu("JourneyPlannerInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyPlannerInput);
            expandableMenuControl.AddExpandedCategory("Related links");

            // Register javascript
            TDPage thePage = (TDPage)Page;
            ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
            thePage.ClientScript.RegisterStartupScript(this.GetType(), "JourneyPlannerInput", repository.GetScript("JourneyPlannerInput", thePage.JavascriptDom));
            thePage.ClientScript.RegisterStartupScript(this.GetType(), "JQueryQtip", repository.GetScript("JQueryQtip", thePage.JavascriptDom));
        }

        /// <summary>
        /// OnPreRender event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {

            // If this page is a post back then the following journey options properties 
            //should be saved to journey paremters.
            // NOTE: This cannot be done on the WriteToSession method called from page load as
            // at that point in time the JourneyOptionsControl will not have loaded and
            // these values will not be available.
            // NOTE: This is a catch all to ensure that the paramters are saved when the
            // user for example goes to the To Location map. If the parameters have already
            // been saved by Submitting the journey they wont get saved again here.
            if (Page.IsPostBack && !journeyOptionsSaved)
            {
                SaveJourneyOptionsToJourneyParameters();
                SaveAccessibleJourneyOptionsToJourneyParameters();
            }

            // Refresh all values on the page.
            RefreshPage();

            // If the page is shown initially and you are logged in
            // all the values should be fetched from your preferences.
            SetupSkipLinksAndScreenReaderText();

            carPreferencesControl.FuelConsumptionOption = journeyParameters.FuelConsumptionOption;
            carPreferencesControl.FuelCostOption = journeyParameters.FuelCostOption;
            carPreferencesControl.DrivingSpeed = journeyParameters.DrivingSpeed;
            carPreferencesControl.FindJourneyType = journeyParameters.PrivateAlgorithmType;
            carPreferencesControl.CarSize = journeyParameters.CarSize;
            carPreferencesControl.FuelType = journeyParameters.CarFuelType;
            carPreferencesControl.FuelConsumptionUnit = journeyParameters.FuelConsumptionUnit;

            SetupMap();

            SetupNavigationButtons();

            base.OnPreRender(e);

            //Hide the advanced text if required. Note that this must be done
            //after the base call of the method, since it is in there that
            //the panel text is initially populated.
            if (transportTypesControl.Visible)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }
        }

        /// <summary>
        /// OnUnload event handler 
        /// </summary>
        /// <param name="e">event arguments</param>
        override protected void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
            dateControl.CalendarClose();

        }

        #endregion

        #region Web Form Designer generated code

        override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ExtraEventWireUp();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}

        /// <summary>
        /// Extra event subscription
        /// </summary>
        private void ExtraEventWireUp()
        {
            journeyOptionsControl = carPreferencesControl.JourneyOptionsControl;

            // Locations
            originLocationControl.MapLocationClick += new EventHandler(MapFromClick);
            originLocationControl.NewLocationClick += new EventHandler(NewLocationFromClick);
            
            destinationLocationControl.MapLocationClick += new EventHandler(MapToClick);
            destinationLocationControl.NewLocationClick += new EventHandler(NewLocationToClick);

            ptPreferencesControl.LocationControlVia.MapClick += new EventHandler(MapViaPTClick);
            ptPreferencesControl.LocationControlVia.NewLocation += new EventHandler(NewLocationPTViaClick);

            carPreferencesControl.JourneyOptionsControl.LocationControl.MapLocationClick += new EventHandler(MapViaCarClick);
            carPreferencesControl.JourneyOptionsControl.LocationControl.NewLocationClick += new EventHandler(NewLocationCarViaClick);

            // Submit
            pageOptionsControlBottom.Next += new EventHandler(SubmitClick);
            pageOptionsControlBottom.Clear += new EventHandler(CommandClearClick);
            
            // Save
            pageOptionsControlBottom.Save += new EventHandler(pageOptionsControlBottom_Save);

            // Dates
            dateControl.LeaveDateControl.DateChanged += new EventHandler(dateControlLeaveDateControl_DateChanged);
            dateControl.ReturnDateControl.DateChanged += new EventHandler(dateControlReturnDateControl_DateChanged);

            // DN079 UEE
            // Event Handler for default action button			
            headerControl.DefaultActionEvent += new EventHandler(this.SubmitClick);

            transportTypesControl.ModesPublicTransport.SelectedIndexChanged += new System.EventHandler(this.ModesPublicTransportSelectedIndexChanged);
        }

		#endregion

        #region Properties

        #region Public properties

        public D2DPTPreferencesControl PreferencesControl
        {
            get { return ptPreferencesControl; }
        }

        public D2DCarPreferencesControl CarPreferencesControl
        {
            get { return carPreferencesControl; }
        }

        #endregion

        #region Get/Set Properties for Control Selection values

        /// <summary>
        /// Gets/Sets Private required
        /// </summary>
        private bool PrivateRequired
        {
            get
            {
                return checkBoxCarRoute.Checked;
            }
            set
            {
                checkBoxCarRoute.Checked = value;
            }
        }

        /// <summary>
        /// Gets/Sets Public required
        /// </summary>
        private bool PublicRequired
        {
            get
            {
                return checkBoxPublicTransport.Checked;
            }
            set
            {
                checkBoxPublicTransport.Checked = value;
            }
        }

        /// <summary>
        /// Gets/Sets walking speed
        /// </summary>
        private int WalkingSpeed
        {
            get
            {
                return ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed;
            }

            set
            {
                ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = value;
            }

        }

        /// <summary>
        /// Gets/Sets max walking time
        /// </summary>
        private int MaxWalkingTime
        {
            get
            {
                return ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration;
            }

            set
            {
                ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration = value;
            }
        }

        /// <summary>
        /// Gets/Sets driving speed
        /// </summary>
        private int DrivingSpeed
        {
            get
            {
                return carPreferencesControl.DrivingSpeed;
            }
            set
            {
                carPreferencesControl.DrivingSpeed = value;
            }
        }

        /// <summary>
        /// Gets/sets interchange speed
        /// </summary>
        private int InterchangeSpeed
        {
            get
            {
                return ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed;
            }
            set
            {
                ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed = value;
            }
        }

        /// <summary>
        /// Gets/sets if wants to avoid motorways
        /// </summary>
        private bool AvoidMotorways
        {
            get
            {
                return journeyOptionsControl.AvoidMotorways;
            }

            set
            {
                journeyOptionsControl.AvoidMotorways = value;
            }
        }

        /// <summary>
        /// Gets/sets if wants to avoid tolls
        /// </summary>
        private bool AvoidTolls
        {
            get
            {
                return journeyOptionsControl.AvoidTolls;
            }

            set
            {
                journeyOptionsControl.AvoidTolls = value;
            }
        }

        /// <summary>
        /// Gets/sets if wants to avoid ferries
        /// </summary>
        private bool AvoidFerries
        {
            get
            {
                return journeyOptionsControl.AvoidFerries;
            }

            set
            {
                journeyOptionsControl.AvoidFerries = value;
            }
        }

        /// <summary>
        /// Gets/sets if wants to avoid unnamed limited access restrictions
        /// </summary>
        private bool BanLimitedAccess
        {
            get
            {
                return journeyOptionsControl.BanLimitedAccess;
            }

            set
            {
                journeyOptionsControl.BanLimitedAccess = value;
            }
        }

        /// <summary>
        /// Gets/sets if wants to avoid motorways
        /// </summary>
        private bool DoNotUseMotorways
        {
            get
            {
                return carPreferencesControl.DoNotUseMotorways;
            }

            set
            {
                carPreferencesControl.DoNotUseMotorways = value;
            }
        }

        /// <summary>
        /// Gets/sets Public algorithm type
        /// </summary>
        private PublicAlgorithmType PublicAlgorithmType
        {
            get
            {
                return ptPreferencesControl.JourneyChangesOptionsControl.Changes;
            }
            set
            {
                ptPreferencesControl.JourneyChangesOptionsControl.Changes = value;

            }
        }

        /// <summary>
        /// Gets/Sets Private algorithm type
        /// </summary>
        private PrivateAlgorithmType PrivateAlgorithmType
        {
            get
            {
                return carPreferencesControl.FindJourneyType;

            }
            set
            {
                carPreferencesControl.FindJourneyType = value;

            }
        }

        /// <summary>
        /// Gets/Sets car size
        /// </summary>
        private string CarSize
        {
            get
            {
                return carPreferencesControl.CarSize;

            }
            set
            {
                carPreferencesControl.CarSize = value;

            }
        }

        /// <summary>
        /// Gets/Sets fuel Type
        /// </summary>
        private string FuelType
        {
            get
            {
                return carPreferencesControl.FuelType;

            }
            set
            {
                carPreferencesControl.FuelType = value;

            }
        }

        /// <summary>
        /// Gets/Sets Fuel consumption unit
        private int FuelConsumptionUnit
        {
            get
            {
                return carPreferencesControl.FuelConsumptionUnit;

            }
            set
            {
                carPreferencesControl.FuelConsumptionUnit = value;

            }
        }

        #endregion

        #endregion
        
		#region Events handlers

        /// <summary>
        /// Find Journey Click event handler
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        private void SubmitClick(object sender, EventArgs e)
        {
            SubmitRequest(true);
        }

        /// <summary>
        /// Clear button event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandClearClick(object sender, EventArgs e)
        {
            // Close all ISingleWindows
            TDPage.CloseAllSingleWindows(this);

            // Hide map
            pnlMap.Visible = false;
            mapInputControl.Visible = false;

            // Reset location controls
            originLocationControl.Reset();
            destinationLocationControl.Reset();
            ptPreferencesControl.LocationControlVia.Reset();
            carPreferencesControl.JourneyOptionsControl.LocationControl.Reset(SearchType.Locality);

            // Reset parameters and journey options
            TDItineraryManager itinerary = TDItineraryManager.Current;
            journeyParameters.AvoidMotorWays = false;
            journeyParameters.AvoidFerries = false;
            journeyParameters.AvoidTolls = false;
            journeyParameters.BanUnknownLimitedAccess = false;

            carPreferencesControl.InputConsumptionText = null;
            carPreferencesControl.InputCostText = null;

            journeyParameters.PrivateViaType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
            journeyParameters.PrivateVia = new LocationSearch();
            journeyParameters.PrivateVia.SearchType = SearchType.Locality;

            journeyParameters.DoNotUseMotorways = false;

            string defaultItemValue = populator.GetDefaultListControlValue(DataServiceType.DrivingFindDrop);
            journeyParameters.PrivateAlgorithmType = (PrivateAlgorithmType)Enum.Parse(typeof(PrivateAlgorithmType), defaultItemValue);

            journeyParameters.DrivingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.DrivingMaxSpeedDrop), TDCultureInfo.CurrentCulture);
            journeyParameters.PrivateRequired = true;
            journeyParameters.PublicRequired = false;
            journeyParameters.PublicModes = new ModeType[0];
            journeyParameters.CarSize = populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop);
            journeyParameters.CarFuelType = populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop);
            journeyParameters.FuelConsumptionUnit = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.UnitsDrop), TDCultureInfo.CurrentCulture);

            string defaultPtItemValue = populator.GetDefaultListControlValue(DataServiceType.ChangesFindDrop);
            journeyParameters.PublicAlgorithmType = (PublicAlgorithmType)Enum.Parse(typeof(PublicAlgorithmType), defaultPtItemValue);
            journeyParameters.InterchangeSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.ChangesSpeedDrop), TDCultureInfo.CurrentCulture);
            journeyParameters.WalkingSpeed = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.WalkingSpeedDrop), TDCultureInfo.CurrentCulture);
            journeyParameters.MaxWalkingTime = Convert.ToInt32(populator.GetDefaultListControlValue(DataServiceType.WalkingMaxTimeDrop), TDCultureInfo.CurrentCulture);
            journeyParameters.FuelConsumptionOption = true;
            journeyParameters.FuelCostOption = true;

            carPreferencesControl.InputConsumptionText = null;
            carPreferencesControl.InputCostText = null;
            carPreferencesControl.JourneyOptionsControl.ClearRoads();

            carPreferencesControl.CarSizeDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelTypeDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelCostOptionMode = GenericDisplayMode.Normal;

            ptPreferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.Normal;
            ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.Normal;
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.Normal;
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.Normal;

            ptPreferencesControl.JourneyChangesOptionsControl.Changes = journeyParameters.PublicAlgorithmType;
            ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed = journeyParameters.InterchangeSpeed;
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration = journeyParameters.MaxWalkingTime;
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = journeyParameters.WalkingSpeed;

            carPreferencesControl.DrivingSpeed = journeyParameters.DrivingSpeed;
            carPreferencesControl.FindJourneyType = journeyParameters.PrivateAlgorithmType;
            carPreferencesControl.CarSize = journeyParameters.CarSize;
            carPreferencesControl.FuelType = journeyParameters.CarFuelType;
            carPreferencesControl.FuelConsumptionOption = journeyParameters.FuelConsumptionOption;
            carPreferencesControl.FuelCostOption = journeyParameters.FuelCostOption;
            carPreferencesControl.FuelConsumptionUnit = journeyParameters.FuelConsumptionUnit;
            carPreferencesControl.DoNotUseMotorways = journeyParameters.DoNotUseMotorways;
            carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
            carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;

            journeyOptionsControl.AvoidMotorways = journeyParameters.AvoidMotorWays;
            journeyOptionsControl.AvoidFerries = journeyParameters.AvoidFerries;
            journeyOptionsControl.AvoidTolls = journeyParameters.AvoidTolls;
            journeyOptionsControl.AvoidRoadsList = journeyParameters.AvoidRoadsList;
            journeyOptionsControl.BanLimitedAccess = journeyParameters.BanUnknownLimitedAccess;
            journeyOptionsControl.UseRoadsList = journeyParameters.UseRoadsList;

            accessibleOptionsControl.Reset();
            transportTypesControl.Reset();
            ptPreferencesControl.JourneyChangesOptionsControl.Reset();
            ptPreferencesControl.WalkingSpeedOptionsControl.Reset();
            carPreferencesControl.Reset();

            journeyParameters.PrivateRequired = true;
            journeyParameters.PublicRequired = true;
            
            journeyParameters.Initialise();
            inputPageState.Initialise();

            // After resetting JourneyParameters, the local variables need to be reloaded
            LoadSessionVariables();

            // Reset the Map locations to show on the map (use Origin as it should have been reset by now)
            inputPageState.MapLocation = journeyParameters.OriginLocation;
            inputPageState.MapLocationSearch = journeyParameters.Origin;

            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
            FavouriteLoadOptions.SaveDisplay.Visible = false;

            LoadUserPreferences();
        }

        #region Location events

        /// <summary>
        /// Event handler for origin location new click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewLocationFromClick(object sender, EventArgs e)
        {
            // Set local search and location to be the location controls values
            originSearch = originLocationControl.Search;
            originLocation = originLocationControl.Location;

            journeyParameters.Origin = originSearch;
            journeyParameters.OriginLocation = originLocation;

            // If a car park was being planned from (Exit coords), we will have populated the return 
            // location (because it would use Entrance coords), therefore need to clear it
            if (journeyParameters.ReturnDestinationLocation != null)
            {
                journeyParameters.ReturnDestinationLocation.ClearAll();
                journeyParameters.ReturnDestinationLocation = null;
            }

            // If the map control is visible, set it to focus in on the destination, if that is valid
            if (mapInputControl.Visible)
            {
                if (destinationLocation.Status == TDLocationStatus.Valid)
                {
                    inputPageState.MapLocationSearch = destinationSearch;
                    inputPageState.MapLocation = destinationLocation;
                }
            }
        }

        /// <summary>
        /// Event handler for destination location new click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewLocationToClick(object sender, EventArgs e)
        {
            // Set local search and location to be the location controls values
            destinationSearch = destinationLocationControl.Search;
            destinationLocation = destinationLocationControl.Location;

            journeyParameters.Destination = destinationSearch;
            journeyParameters.DestinationLocation = destinationLocation;

            // If a car park was being planned to (Entrance coords), we will have populated the return 
            // location (because it would use Exit coords), therefore need to clear it
            if (journeyParameters.ReturnOriginLocation != null)
            {
                journeyParameters.ReturnOriginLocation.ClearAll();
                journeyParameters.ReturnOriginLocation = null;
            }

            // If the map control is visible, set it to focus in on the origin, if that is valid
            if (mapInputControl.Visible)
            {
                if (originLocation.Status == TDLocationStatus.Valid)
                {
                    inputPageState.MapLocationSearch = originSearch;
                    inputPageState.MapLocation = originLocation;
                }
            }
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "via" location.
        /// The journey parameters are updated with the "via" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void NewLocationPTViaClick(object sender, EventArgs e)
        {
            // Set local search and location to be the location controls values
            publicViaSearch = ptPreferencesControl.LocationControlVia.Search;
            publicViaLocation = ptPreferencesControl.LocationControlVia.Location;

            journeyParameters.PublicVia = publicViaSearch;
            journeyParameters.PublicViaLocation = publicViaLocation;
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "via" location.
        /// The journey parameters are updated with the "via" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void NewLocationCarViaClick(object sender, EventArgs e)
        {
            // Set local search and location to be the location controls values
            privateViaSearch = carPreferencesControl.JourneyOptionsControl.LocationControl.Search;
            privateViaLocation = carPreferencesControl.JourneyOptionsControl.LocationControl.Location;

            journeyParameters.PrivateVia = privateViaSearch;
            journeyParameters.PrivateViaLocation = privateViaLocation;
        }

        #endregion

        #region Date events

        /// <summary>
        /// Event handler called when date selected from calendar control. The journey parameters for the outward
        /// date are updated with the calendar date selection.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void dateControlLeaveDateControl_DateChanged(object sender, EventArgs e)
        {
            journeyParameters.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
            journeyParameters.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
        }

        /// <summary>
        /// Event handler called when date selected from calendar control. The journey parameters for the return
        /// date are updated with the calendar date selection.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void dateControlReturnDateControl_DateChanged(object sender, EventArgs e)
        {
            journeyParameters.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
            journeyParameters.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
        }

        #endregion

        #region Journey preferences events

        /// <summary>
        /// Event handler for the Save user preferences button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageOptionsControlBottom_Save(object sender, EventArgs e)
        {
            SaveUserPreferences();
        }

        #region Journey preference changed events

        /// <summary>
        /// Handler for PublicAlgorithmType changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_PtJourneyChanges(object sender, EventArgs e)
        {
            journeyParameters.PublicAlgorithmType = ptPreferencesControl.JourneyChangesOptionsControl.Changes;
        }

        /// <summary>
        /// Handler for InterchangeSpeed changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_PtChangesSpeed(object sender, EventArgs e)
        {
            journeyParameters.InterchangeSpeed = ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed;
        }

        /// <summary>
        /// Handler for MaxWalkingTime changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_PtWalkDuration(object sender, EventArgs e)
        {
            journeyParameters.MaxWalkingTime = ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration;
        }

        /// <summary>
        /// Handler for WalkingSpeed changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_PtWalkSpeed(object sender, EventArgs e)
        {
            journeyParameters.WalkingSpeed = ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed;
        }

        /// <summary>
        /// Handler for JourneyType changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_JourneyTypeChanged(object sender, EventArgs e)
        {
            journeyParameters.PrivateAlgorithmType = carPreferencesControl.FindJourneyType;
        }

        /// <summary>
        /// Handler for Driving speed changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_SpeedChanged(object sender, EventArgs e)
        {
            journeyParameters.DrivingSpeed = carPreferencesControl.DrivingSpeed;
        }

        /// <summary>
        /// Handler for car size changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_CarSizeChanged(object sender, EventArgs e)
        {
            journeyParameters.CarSize = carPreferencesControl.CarSize;
        }
        
        /// <summary>
        /// Handler for fuel type changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_FuelTypeChanged(object sender, EventArgs e)
        {
            journeyParameters.CarFuelType = carPreferencesControl.FuelType;
        }

        /// <summary>
        /// Handler for consumption option changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_FuelConsumptionOptionChanged(object sender, EventArgs e)
        {
            journeyParameters.FuelConsumptionOption = carPreferencesControl.FuelConsumptionOption;
        }

        /// <summary>
        /// Handler for fuel use unit changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_FuelUseUnitChanged(object sender, EventArgs e)
        {
            journeyParameters.FuelConsumptionUnit = carPreferencesControl.FuelConsumptionUnit;
        }

        /// <summary>
        /// Handler for fuel cost option changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_FuelCostOptionChanged(object sender, EventArgs e)
        {
            journeyParameters.FuelCostOption = carPreferencesControl.FuelCostOption;
        }
        
        /// <summary>
        /// Event handler for FuelConsumptionTextChanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_FuelConsumptionTextChanged(object sender, EventArgs e)
        {
            journeyParameters.FuelConsumptionEntered = carPreferencesControl.FuelConsumptionValue;
            //journeyParameters.PreConvertedFuelConsumption = carPreferencesControl.FuelConsumptionText;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_FuelCostTextChanged(object sender, EventArgs e)
        {
            journeyParameters.FuelCostEntered = carPreferencesControl.FuelCostValue;
        }

        /// <summary>
        /// Handler for Avoid Motorways changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_AvoidMotorwaysChanged(object sender, EventArgs e)
        {
            journeyParameters.AvoidMotorWays = journeyOptionsControl.AvoidMotorways;
        }

        /// <summary>
        /// Handler for DoNotUseMotorways changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_DoNotUseMotorwaysChanged(object sender, EventArgs e)
        {
            journeyParameters.DoNotUseMotorways = carPreferencesControl.DoNotUseMotorways;
        }

        /// <summary>
        /// Handler for Avoid Tolls changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_AvoidTollsChanged(object sender, EventArgs e)
        {
            journeyParameters.AvoidTolls = journeyOptionsControl.AvoidTolls;
        }

        /// <summary>
        /// Handler for Ban Limited Access changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_BanLimitedAccessChanged(object sender, EventArgs e)
        {
            journeyParameters.BanUnknownLimitedAccess = journeyOptionsControl.BanLimitedAccess;
        }

        /// <summary>
        /// Handler for Avoid Ferries changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_AvoidFerriesChanged(object sender, EventArgs e)
        {
            journeyParameters.AvoidFerries = journeyOptionsControl.AvoidFerries;
        }
        
        /// <summary>
        /// Event handler for when something changes in the panel traveldetails
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModesPublicTransportSelectedIndexChanged(object sender, System.EventArgs e)
        {
            // When event triggered, write info in session : Travel details have changed and should be displayed		
            if (!inputPageState.TravelOptionsChanged.Contains(TravelOptionsChangedEnum.PublicTransport))
                inputPageState.TravelOptionsChanged.Add(TravelOptionsChangedEnum.PublicTransport);
        }

        #endregion

        #endregion

        #region Map Event Handlers

		/// <summary>
		/// Map from click event hadler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
        private void MapFromClick(object sender, EventArgs e)
        {
            bool shiftForm = false;
            inputPageState.MapType = CurrentLocationType.From;
            inputPageState.MapMode = CurrentMapMode.FromJourneyInput;

            #region Resolve location for map

            // Reset map search and location
            inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
            inputPageState.MapLocationSearch.ClearAll();
            inputPageState.MapLocationSearch.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);

            // Validate selected location and save to parameters ready for input/map use
            originLocationControl.Validate(journeyParameters, true, true, true, StationType.Undetermined);

            originSearch = originLocationControl.Search;
            originLocation = originLocationControl.Location;
            journeyParameters.Origin = originSearch;
            journeyParameters.OriginLocation = originLocation;
            inputPageState.MapLocationSearch = originSearch;
            inputPageState.MapLocation = originLocation;

            // If no input text, display default map
            if (inputPageState.MapLocationSearch.InputText.Length == 0)
            {
                inputPageState.MapLocationControlType.Type = ControlType.Default;
            }
            // If valid location, display the location on map
            else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
            {
                // Ensure search type is set to map to allow map/location controls to handle if needed
                originSearch.SearchType = SearchType.Map;
            }
            // Otherwise, ambiguous location, send to next page to resolve
            else
            {
                inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                shiftForm = true;
            }

            #endregion

            if (shiftForm)
            {
                inputPageState.JourneyInputReturnStack.Push(pageId);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked			
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                pnlMap.Visible = true;
                mapInputControl.Visible = true;
            }
        }
        
		/// <summary>
		/// Map to click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapToClick(object sender, EventArgs e)
		{
            bool shiftForm = false;
			inputPageState.MapType = CurrentLocationType.To;
			inputPageState.MapMode = CurrentMapMode.FromJourneyInput;

            #region Resolve location for map

            // Reset map search and location
            inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
            inputPageState.MapLocationSearch.ClearAll();
            inputPageState.MapLocationSearch.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);

            // Validate selected location and save to parameters ready for input/map use
            destinationLocationControl.Validate(journeyParameters, true, true, true, StationType.Undetermined);

            destinationSearch = destinationLocationControl.Search;
            destinationLocation = destinationLocationControl.Location;
            journeyParameters.Destination = destinationSearch;
            journeyParameters.DestinationLocation = destinationLocation;
            inputPageState.MapLocationSearch = destinationSearch;
            inputPageState.MapLocation = destinationLocation;

            // If no input text, display default map
            if (inputPageState.MapLocationSearch.InputText.Length == 0)
            {
                inputPageState.MapLocationControlType.Type = ControlType.Default;
            }
            // If valid location, display the location on map
            else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
            {
                // Ensure search type is set to map to allow map/location controls to handle if needed
                destinationSearch.SearchType = SearchType.Map;
            }
            // Otherwise, ambiguous location, send to next page to resolve
            else
            {
                inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                shiftForm = true;
            }

            #endregion

            if (shiftForm)
            {
                inputPageState.JourneyInputReturnStack.Push(pageId);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked			
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                pnlMap.Visible = true;
                mapInputControl.Visible = true;
            }
		}

		/// <summary>
		/// Map via road click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapViaCarClick(object sender, EventArgs e)
		{
            bool shiftForm = false;
			inputPageState.MapType = CurrentLocationType.PrivateVia;
			inputPageState.MapMode = CurrentMapMode.FromJourneyInput;

            #region Resolve location for map

            // Reset map search and location
            inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
            inputPageState.MapLocationSearch.ClearAll();
            inputPageState.MapLocationSearch.SearchType = GetDefaultSearchType(DataServiceType.CarViaDrop);

            // Validate selected location and save to parameters ready for input/map use
            carPreferencesControl.JourneyOptionsControl.LocationControl.Validate(journeyParameters, true, true, true, StationType.Undetermined);

            privateViaSearch = carPreferencesControl.JourneyOptionsControl.LocationControl.Search;
            privateViaLocation = carPreferencesControl.JourneyOptionsControl.LocationControl.Location;
            journeyParameters.PrivateVia = privateViaSearch;
            journeyParameters.PrivateViaLocation = privateViaLocation;
            inputPageState.MapLocationSearch = privateViaSearch;
            inputPageState.MapLocation = privateViaLocation;

            // If no input text, display default map
            if (inputPageState.MapLocationSearch.InputText.Length == 0)
            {
                inputPageState.MapLocationControlType.Type = ControlType.Default;
            }
            // If valid location, display the location on map
            else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
            {
                // Ensure search type is set to map to allow map/location controls to handle if needed
                privateViaSearch.SearchType = SearchType.Map;
            }
            // Otherwise, ambiguous location, send to next page to resolve
            else
            {
                inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                shiftForm = true;
            }

            #endregion

            if (shiftForm)
            {
                inputPageState.JourneyInputReturnStack.Push(pageId);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked			
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                pnlMap.Visible = true;
                mapInputControl.Visible = true;
            }
		}

		/// <summary>
		/// Map via road click event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void MapViaPTClick(object sender, EventArgs e)
		{
            bool shiftForm = false;
			inputPageState.MapType = CurrentLocationType.PublicVia;
			inputPageState.MapMode = CurrentMapMode.FromJourneyInput;
			
            #region Resolve location for map

            // Reset map search and location
            inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
            inputPageState.MapLocationSearch.ClearAll();
            inputPageState.MapLocationSearch.SearchType = GetDefaultSearchType(DataServiceType.PTViaDrop);

            // Validate selected location and save to parameters ready for input/map use
            ptPreferencesControl.LocationControlVia.Validate(journeyParameters, false, false, false, StationType.UndeterminedNoGroup);

            publicViaSearch = ptPreferencesControl.LocationControlVia.Search;
            publicViaLocation = ptPreferencesControl.LocationControlVia.Location;
            journeyParameters.PublicVia = publicViaSearch;
            journeyParameters.PublicViaLocation = publicViaLocation;
            inputPageState.MapLocationSearch = publicViaSearch;
            inputPageState.MapLocation = publicViaLocation;

            // If no input text, display default map
            if (inputPageState.MapLocationSearch.InputText.Length == 0)
            {
                inputPageState.MapLocationControlType.Type = ControlType.Default;
            }
            // If valid location, display the location on map
            else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
            {
                // Ensure search type is set to map to allow map/location controls to handle if needed
                publicViaSearch.SearchType = SearchType.Map;
            }
            // Otherwise, ambiguous location, send to next page to resolve
            else
            {
                inputPageState.MapLocationControlType.Type = ControlType.NoMatch;
                shiftForm = true;
            }

            #endregion

            if (shiftForm)
            {
                inputPageState.JourneyInputReturnStack.Push(pageId);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                pnlMap.Visible = true;
                mapInputControl.Visible = true;
            }
		}

		#endregion

		#endregion

        #region Private Methods

        #region Journey parameter session methods

        /// <summary>
        /// Loads page location and search objects from journey parameters
        /// </summary>
        private void LoadSessionVariables()
        {
            //This is to fix the journey planner being null.
            //Note that we don't need to check that journeyPlanner 
            //is the correct type. It always will be, else it'll be
            //null.
            if (journeyParameters == null)
            {
                journeyParameters = new TDJourneyParametersMulti();
                TDSessionManager.Current.JourneyParameters = journeyParameters;
            }

            originSearch = journeyParameters.Origin;
            originLocation = journeyParameters.OriginLocation;
            destinationSearch = journeyParameters.Destination;
            destinationLocation = journeyParameters.DestinationLocation;
            
            privateViaSearch = journeyParameters.PrivateVia;
            privateViaLocation = journeyParameters.PrivateViaLocation;
            publicViaSearch = journeyParameters.PublicVia;
            publicViaLocation = journeyParameters.PublicViaLocation;
        }

        /// <summary>
        /// Writes user input data to the journey paramters object
        /// </summary>
        private void WriteToSession()
        {
            if (inputDateAdapter == null)
            {
                inputDateAdapter = new LeaveReturnDatesControlAdapter();
            }

            // Save Journey Dates to session
            inputDateAdapter.UpdateJourneyDates(dateControl, false, journeyParameters, TDSessionManager.Current.ValidationError);

            // Save Journey preferences to session
            journeyParameters.PrivateRequired = PrivateRequired;
            journeyParameters.PublicRequired = PublicRequired;

            if (PublicRequired)
            {
                journeyParameters.PublicModes = transportTypesControl.PublicModes;
            }
            else
            {
                journeyParameters.PublicModes = new ModeType[0];
            }

            // Public
            journeyParameters.WalkingSpeed = ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed;
            journeyParameters.MaxWalkingTime = ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration;
            journeyParameters.InterchangeSpeed = ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed;
            journeyParameters.PublicAlgorithmType = ptPreferencesControl.JourneyChangesOptionsControl.Changes;

            // Car
            journeyParameters.DrivingSpeed = carPreferencesControl.DrivingSpeed;
            journeyParameters.PrivateAlgorithmType = carPreferencesControl.FindJourneyType;

            journeyParameters.DoNotUseMotorways = carPreferencesControl.DoNotUseMotorways;

            journeyParameters.CarSize = carPreferencesControl.CarSize;
            journeyParameters.CarFuelType = carPreferencesControl.FuelType;

            journeyParameters.FuelConsumptionOption = carPreferencesControl.FuelConsumptionOption;
            journeyParameters.FuelConsumptionEntered = carPreferencesControl.FuelConsumptionValue;

            journeyParameters.FuelConsumptionUnit = carPreferencesControl.FuelConsumptionUnit;

            journeyParameters.FuelCostOption = carPreferencesControl.FuelCostOption;
            journeyParameters.FuelCostEntered = carPreferencesControl.FuelCostValue;

            // Accessible
            SaveAccessibleJourneyOptionsToJourneyParameters();
        }

        /// <summary>
        /// Saves the values from the journey options control to the
        /// journey parameters object.
        /// </summary>
        private void SaveJourneyOptionsToJourneyParameters()
        {
            // Save the values from the journey options control into the journey parameters
            journeyParameters.AvoidMotorWays = journeyOptionsControl.AvoidMotorways;
            journeyParameters.AvoidFerries = journeyOptionsControl.AvoidFerries;
            journeyParameters.AvoidTolls = journeyOptionsControl.AvoidTolls;
            journeyParameters.BanUnknownLimitedAccess = journeyOptionsControl.BanLimitedAccess;
            journeyParameters.AvoidRoadsList = journeyOptionsControl.AvoidRoadsList;
            journeyParameters.UseRoadsList = journeyOptionsControl.UseRoadsList;
        }

        /// <summary>
        /// Saves the accessible values from the accessible journey options control 
        /// to the journey parameters object
        /// </summary>
        private void SaveAccessibleJourneyOptionsToJourneyParameters()
        {
            if (ShowAccessibleOptions())
            {
                switch (accessibleOptionsControl.SelectedAccessibilityOption)
                {
                    case AccessibleOptionsType.Wheelchair:
                        journeyParameters.RequireStepFreeAccess = true;
                        journeyParameters.RequireSpecialAssistance = false;
                        break;
                    case AccessibleOptionsType.WheelchairAndAssistance:
                        journeyParameters.RequireStepFreeAccess = true;
                        journeyParameters.RequireSpecialAssistance = true;
                        break;
                    case AccessibleOptionsType.Assistance:
                        journeyParameters.RequireStepFreeAccess = false;
                        journeyParameters.RequireSpecialAssistance = true;
                        break;
                    case AccessibleOptionsType.NoRequirement:
                    default:
                        journeyParameters.RequireStepFreeAccess = false;
                        journeyParameters.RequireSpecialAssistance = false;
                        break;
                }

                if (accessibleOptionsControl.SelectedAccessibilityOption != AccessibleOptionsType.NoRequirement)
                    journeyParameters.RequireFewerInterchanges = accessibleOptionsControl.FewestChanges.Checked;
                else
                    journeyParameters.RequireFewerInterchanges = false;

                // Only update the accessible walk values if the user has not changed the walk defaults
                if ((journeyParameters.WalkingSpeed == int.Parse(populator.GetDefaultListControlValue(DataServiceType.WalkingSpeedDrop)))
                    && (journeyParameters.MaxWalkingTime == int.Parse(populator.GetDefaultListControlValue(DataServiceType.WalkingMaxTimeDrop))))
                {
                    journeyParameters.AccessibleWalkDistance = int.Parse(Properties.Current["AccessibleOptions.WalkingDistance.Metres"]);
                    journeyParameters.AccessibleWalkSpeed = int.Parse(Properties.Current["AccessibleOptions.WalkingSpeed.MetresPerMinute"]);
                }
                else
                {
                    journeyParameters.AccessibleWalkDistance = 0;
                    journeyParameters.AccessibleWalkSpeed = 0;
                }
            }
        }

        /// <summary>
        /// Initialise PT and Car journey params values
        /// </summary>
        public void InitViaParameters()
        {
            // Car via params
            journeyParameters.PrivateViaType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
            journeyParameters.PrivateVia = new LocationSearch();
            journeyParameters.PrivateVia.SearchType = SearchType.Locality;

            // PT via params
            journeyParameters.PublicViaType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
            journeyParameters.PublicVia = new LocationSearch();
        }

        #endregion

        #region Control initialise methods

        /// <summary>
        /// Initialises the origin, destination and via location controls
        /// </summary>
        private void InitialiseLocationControls()
        {
            // Populate always, the location control deals with any changed locations or not
            originLocationControl.Initialise(originLocation, originSearch, DataServiceType.FromToDrop, true, false, false, true, true, true, false, false, showLocationMoreOptionsExpanded);
            destinationLocationControl.Initialise(destinationLocation, destinationSearch, DataServiceType.FromToDrop, true, false, false, true, true, true, false, false, showLocationMoreOptionsExpanded);
            ptPreferencesControl.LocationControlVia.Initialise(publicViaLocation, publicViaSearch, DataServiceType.PTViaDrop, true, false, false, true, false, true, false, false, showLocationMoreOptionsExpanded);
            carPreferencesControl.JourneyOptionsControl.LocationControl.Initialise(privateViaLocation, privateViaSearch, DataServiceType.CarViaDrop, true, false, false, true, true, true, false, false, showLocationMoreOptionsExpanded);
        }

        /// <summary>
        /// Initialises controls used on page with journey parameters
        /// </summary>
        public void InitialiseControls()
        {
            transportTypesControl.Visible = true;
            
            carPreferencesControl.DrivingSpeed = journeyParameters.DrivingSpeed;
            carPreferencesControl.FindJourneyType = journeyParameters.PrivateAlgorithmType;
            carPreferencesControl.DoNotUseMotorways = journeyParameters.DoNotUseMotorways;

            carPreferencesControl.CarSize = journeyParameters.CarSize;
            carPreferencesControl.FuelType = journeyParameters.CarFuelType;

            carPreferencesControl.FuelConsumptionOption = journeyParameters.FuelConsumptionOption;
            if (carPreferencesControl.FuelConsumptionText != "")
            {
                carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
            }
            carPreferencesControl.FuelConsumptionUnit = journeyParameters.FuelConsumptionUnit;

            carPreferencesControl.FuelCostOption = journeyParameters.FuelCostOption;
            if (carPreferencesControl.FuelCostText != "")
            {
                carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;
            }

            carPreferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.CarSizeDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelTypeDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelCostOptionMode = GenericDisplayMode.Normal;
            carPreferencesControl.JourneyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.JourneyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.Normal;
            ptPreferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.Normal;
            ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.Normal;
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.Normal;
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.Normal;

            ptPreferencesControl.JourneyChangesOptionsControl.Changes = journeyParameters.PublicAlgorithmType;
            ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed = journeyParameters.InterchangeSpeed;
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration = journeyParameters.MaxWalkingTime;
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = journeyParameters.WalkingSpeed;

            // Show the (auto suggest) via location and hide the standard via control
            ptPreferencesControl.ShowLocationControlAutoSuggest = true;
            carPreferencesControl.JourneyOptionsControl.ShowLocationControlAutoSuggest = true;

            panelBackTop.Visible = false;

            #region Accessibility options

            RefreshAccessibleOptions();

            accessibleOptionsControl.Visible = ShowAccessibleOptions();
            accessibleOptionsControl.DisplayMode = GenericDisplayMode.Normal;

            #endregion
        }

        /// <summary>
        /// Refresh all controls in the page using journey parameters
        /// </summary>
        private void RefreshPage()
        {
            accessibleOptionsControl.Visible = true;

            #region Resource controls population
            // populate lists
            populator.LoadListControl(DataServiceType.PublicTransportsCheck, transportTypesControl.ModesPublicTransport);

            PrivateRequired = journeyParameters.PrivateRequired;
            PublicRequired = journeyParameters.PublicRequired;

            #endregion

            #region date control
            if (inputDateAdapter == null)
            {
                inputDateAdapter = new LeaveReturnDatesControlAdapter();
            }

            inputDateAdapter.UpdateDateControl(dateControl, false, journeyParameters, TDSessionManager.Current.ValidationError);
            #endregion

            PrivateRequired = journeyParameters.PrivateRequired;
            PublicRequired = journeyParameters.PublicRequired;

            ptPreferencesControl.PreferencesVisible = true;
            carPreferencesControl.PreferencesVisible = true;

            carPreferencesControl.InputConsumptionText = null;
            carPreferencesControl.InputCostText = null;

            carPreferencesControl.CarSizeDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelTypeDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.Normal;
            carPreferencesControl.FuelCostOptionMode = GenericDisplayMode.Normal;
            journeyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.Normal;
            journeyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.Normal;

            #region Populating the Input Fields

            publicViaType = journeyParameters.PublicViaType;
            privateViaType = journeyParameters.PrivateViaType;

            // avoidSelect.Current = journeyParameters.AvoidRoads;
            #endregion

            transportTypesControl.PublicModes = journeyParameters.PublicModes;
            
            #region Accessibility options

            RefreshAccessibleOptions();

            #endregion

            // if defaultValues false populate with journeyParameters
            if (!journeyParameters.DefaultValues)
            {
                #region Favourite journey additions
                // check if user logged on 
                if (TDSessionManager.Current.Authenticated)
                {
                    //show logged in controls when page refreshed/reloaded after ambiguity etc.
                    FavouriteLoadOptions.LoggedInDisplay();
                    panelFavouriteLoadOptions.Visible = true;
                }
                else
                {
                    panelFavouriteLoadOptions.Visible = true;
                }
                #endregion

                #region Public transport preferences

                ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = journeyParameters.WalkingSpeed;
                ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration = journeyParameters.MaxWalkingTime;
                ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed = journeyParameters.InterchangeSpeed;
                ptPreferencesControl.JourneyChangesOptionsControl.Changes = journeyParameters.PublicAlgorithmType;

                #endregion

                #region Car preferences
                carPreferencesControl.DrivingSpeed = journeyParameters.DrivingSpeed;
                carPreferencesControl.FindJourneyType = journeyParameters.PrivateAlgorithmType;

                carPreferencesControl.DoNotUseMotorways = journeyParameters.DoNotUseMotorways;

                carPreferencesControl.CarSize = journeyParameters.CarSize;
                carPreferencesControl.FuelType = journeyParameters.CarFuelType;

                carPreferencesControl.FuelConsumptionOption = journeyParameters.FuelConsumptionOption;
                if (carPreferencesControl.FuelConsumptionText != null)
                {
                    carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
                }

                carPreferencesControl.FuelConsumptionUnit = journeyParameters.FuelConsumptionUnit;

                carPreferencesControl.FuelCostOption = journeyParameters.FuelCostOption;
                if (carPreferencesControl.FuelCostText != null)
                {
                    carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;
                }

                journeyOptionsControl.AvoidMotorways = journeyParameters.AvoidMotorWays;
                journeyOptionsControl.AvoidFerries = journeyParameters.AvoidFerries;
                journeyOptionsControl.AvoidTolls = journeyParameters.AvoidTolls;
                journeyOptionsControl.BanLimitedAccess = journeyParameters.BanUnknownLimitedAccess;

                journeyOptionsControl.AvoidRoadsList = journeyParameters.AvoidRoadsList;
                journeyOptionsControl.UseRoadsList = journeyParameters.UseRoadsList;

                #endregion
            }
        }

        /// <summary>
        /// Updates the accessible options control with journeyParameters selected values
        /// </summary>
        private void RefreshAccessibleOptions()
        {
            if (ShowAccessibleOptions())
            {
                accessibleOptionsControl.Initialise(
                    journeyParameters.RequireStepFreeAccess,
                    journeyParameters.RequireSpecialAssistance,
                    journeyParameters.RequireFewerInterchanges);
            }
        }

        #endregion

        #region User preference and journey methods

        /// <summary>
        /// Saves user preferences
        /// </summary>
        private void SaveUserPreferences()
        {
            #region User Preferences additions - save travel details

            if (ptPreferencesControl.SavePreferences)
            {
                UserPreferencesHelper.SavePublicTransportPreferences(journeyParameters);
            }

            if (carPreferencesControl.SavePreferences)
            {
                UserPreferencesHelper.SaveCarPreferences(journeyParameters);
            }

            if (accessibleOptionsControl.SavePreferences)
            {
                UserPreferencesHelper.SaveAccessibilityPreferences(journeyParameters);
            }

            if (carPreferencesControl.SavePreferences || ptPreferencesControl.SavePreferences || accessibleOptionsControl.SavePreferences)
            {
                TDSessionManager.Current.CurrentUser.JourneyPreferences.Update();
            }

            #endregion
        }

        /// <summary>
        /// Loads user preferences
        /// </summary>
        private void LoadUserPreferences()
        {
            // Only load user prefernces if user is logged on
            if (TDSessionManager.Current.Authenticated)
            {
                bool ptPrefs = UserPreferencesHelper.LoadPublicTransportPreferences(journeyParameters);
                bool carPrefs = UserPreferencesHelper.LoadCarPreferences(journeyParameters);
                bool accPrefs = UserPreferencesHelper.LoadAccessiblityPreferences(journeyParameters);

                if (ptPrefs)
                {
                    ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = journeyParameters.WalkingSpeed;
                    ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration = journeyParameters.MaxWalkingTime;
                    ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed = journeyParameters.InterchangeSpeed;
                    ptPreferencesControl.JourneyChangesOptionsControl.Changes = journeyParameters.PublicAlgorithmType;
                }

                if (carPrefs)
                {
                    carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
                    carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;
                    carPreferencesControl.DoNotUseMotorways = journeyParameters.DoNotUseMotorways;

                    journeyOptionsControl.AvoidMotorways = journeyParameters.AvoidMotorWays;
                    journeyOptionsControl.AvoidFerries = journeyParameters.AvoidFerries;
                    journeyOptionsControl.BanLimitedAccess = journeyParameters.BanUnknownLimitedAccess;
                    journeyOptionsControl.AvoidTolls = journeyParameters.AvoidTolls;

                    journeyOptionsControl.AvoidRoadsList = journeyParameters.AvoidRoadsList;
                    journeyOptionsControl.UseRoadsList = journeyParameters.UseRoadsList;
                }

                if (accPrefs)
                {
                    RefreshAccessibleOptions();
                }
            }
        }

        /// <summary>
        /// Retrieves the User's selected FavouriteJourney using the Favourite GUID held in the
        /// list of users journeys.  JourneyParameter data is populated from the selected 
        /// Favourite journey.  This will be used to populate page elements when page refresh called
        /// </summary>
        /// <param name="sender">object</param>
        /// <param name="e">Controls.FavouriteLoggedInControl.LoadFavouriteEventArgs</param>
        public void LoadFavourite(object sender, Controls.FavouriteLoggedInControl.LoadFavouriteEventArgs e)
        {
            FavouriteJourney favouriteJourney = e.FavouriteJourney;
            FavouriteJourneyHelper.LoadFavouriteJourney(favouriteJourney);

            // Save the journey options and then set a variable so that the
            // pre-render method doesnt bother to save them again, 
            // otherwise it could override the parameters set by loading the favourite journey
            journeyOptionsSaved = true;

            // Ensure all variables used as 'ref' are updated as well
            LoadSessionVariables();

            // Ensure location controls show the loaded favourite journey locations
            InitialiseLocationControls();
        }

        #endregion

        #region Map methods

        /// <summary>
        /// Sets up a map for the location search and type of map location mode
        /// If map location mode is via map gets initialised with start, via and end mode.
        /// By default map gets initialised with start and end mode.
        /// </summary>
        private void SetupMap()
        {
            MapLocationPoint[] locationsToShow = GetMapLocationPoints();

            LocationSearch locationSearch = inputPageState.MapLocationSearch;
            TDLocation location = inputPageState.MapLocation;

            SearchType searchType = SearchType.Map;

            if (locationSearch != null)
            {
                searchType = locationSearch.SearchType;
            }

            if (location != null)
            {
                if (location.GridReference.IsValid)
                {
                    mapInputControl.MapCenter = location.GridReference;
                }
            }

            if (carPreferencesControl.PreferencesVisible)
            {
                MapLocationMode[] mapModes = new MapLocationMode[3] { MapLocationMode.Start, MapLocationMode.Via, MapLocationMode.End };
                mapInputControl.Initialise(searchType, locationsToShow, mapModes);
            }
            else
            {
                mapInputControl.Initialise(searchType, locationsToShow);
            }

            if (!mapInputControl.Visible && TDSessionManager.Current.GetOneUseKey(SessionKey.JourneyPlannerInputToMap) != null)
            {
                pnlMap.Visible = true;
                mapInputControl.Visible = true;
            }
        }

        /// <summary>
        /// Returns the location points to show on map
        /// </summary>
        /// <returns></returns>
        private MapLocationPoint[] GetMapLocationPoints()
        {
            MapHelper mapHelper = new MapHelper();

            List<MapLocationPoint> mapLocationPoints = new List<MapLocationPoint>();

            MapLocationPoint origin = mapHelper.GetMapLocationPoint(originLocation, MapLocationSymbolType.Start, true, false);

            MapLocationPoint destination = mapHelper.GetMapLocationPoint(destinationLocation, MapLocationSymbolType.End, true, false);

            MapLocationPoint publicVia = mapHelper.GetMapLocationPoint(publicViaLocation, MapLocationSymbolType.Via, true, false);

            MapLocationPoint privateVia = mapHelper.GetMapLocationPoint(privateViaLocation, MapLocationSymbolType.Via, true, false);

            if (origin.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(origin);
            }

            if (destination.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(destination);
            }

            if (publicVia.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(publicVia);
            }

            if (privateVia.MapLocationOSGR.IsValid)
            {
                mapLocationPoints.Add(privateVia);
            }

            return mapLocationPoints.ToArray();
        }

        #endregion

        #region Submit

        /// <summary>
        /// Validates inputs and submits the journey request
        /// </summary>
        private void SubmitRequest(bool loadUserPreferences)
        {
            // Save the journey options and then set a variable so that the
            // pre-render method doesnt bother to save them again.
            SaveJourneyOptionsToJourneyParameters();
            SaveAccessibleJourneyOptionsToJourneyParameters();
            journeyOptionsSaved = true;

            #region Origin and Destination locations

            // Validate the location controls, this calls the location search

            // Origin
            originLocationControl.Validate(journeyParameters, true, true, true, StationType.Undetermined);

            journeyParameters.Origin = originLocationControl.Search;
            journeyParameters.OriginLocation = originLocationControl.Location;
            

            // Destination 
            destinationLocationControl.Validate(journeyParameters, true, true, true, StationType.Undetermined);

            journeyParameters.Destination = destinationLocationControl.Search;
            journeyParameters.DestinationLocation = destinationLocationControl.Location;
            
            #endregion

            #region Via locations

            // PT Via

            // No group locations, or postcodes allowed for public via
            ptPreferencesControl.LocationControlVia.Validate(journeyParameters, false, false, false, StationType.UndeterminedNoGroup);

            journeyParameters.PublicVia = ptPreferencesControl.LocationControlVia.Search;
            journeyParameters.PublicViaLocation = ptPreferencesControl.LocationControlVia.Location;

            // Car Via 

            carPreferencesControl.JourneyOptionsControl.LocationControl.Validate(journeyParameters, true, true, true, StationType.Undetermined);

            journeyParameters.PrivateVia = carPreferencesControl.JourneyOptionsControl.LocationControl.Search;
            journeyParameters.PrivateViaLocation = carPreferencesControl.JourneyOptionsControl.LocationControl.Location;

            #endregion

            UpdateAdvancedOptionsSelected(loadUserPreferences);

            // Save journey parameters that may change during ambiguity resolution
            // so that the may be reinstated if the changes are cancelled
            TDSessionManager.Current.AmbiguityResolution = new AmbiguityResolutionState();
            TDSessionManager.Current.AmbiguityResolution.SaveJourneyParameters();

            // Set up the routing guide flags
            journeyParameters.RoutingGuideInfluenced = bool.Parse(Properties.Current["RoutingGuide.DoorToDoor.RoutingGuideInfluenced"]);
            journeyParameters.RoutingGuideCompliantJourneysOnly = bool.Parse(Properties.Current["RoutingGuide.DoorToDoor.RoutingGuideCompliantJourneysOnly"]);

            // Reset it to false by default, then check if should be true 
            journeyParameters.SaveDetails = false;

            SaveUserPreferences();

            // refactoring for stream2880 
            JourneyPlannerInputAdapter journeyPlannerInputAdapter = new JourneyPlannerInputAdapter();
            journeyPlannerInputAdapter.ValidateAndSearch(AmendStopoverControl.ValidateDates(), this.pageId);
        }

        #endregion

        /// <summary>
        /// Loads text and image resources
        /// </summary>
        private void SetupResources()
        {
            PageTitle = GetResource("JourneyPlannerInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            imageJourneyPlanner.ImageUrl = GetResource("PlanAJourneyControl.imageDoorToDoor.ImageUrl");
            imageJourneyPlanner.AlternateText = " ";

            labelOriginTitle.Text = Global.tdResourceManager.GetString(
                "originSelect.labelLocationTitle", TDCultureInfo.CurrentUICulture);

            labelDestinationTitle.Text = Global.tdResourceManager.GetString(
                "destinationSelect.labelLocationTitle", TDCultureInfo.CurrentUICulture);

            // Location input screen reader text
            originLocationControl.LocationInputDescription.Text = Global.tdResourceManager.GetString(
                "originSelect.labelSRLocation", TDCultureInfo.CurrentUICulture);
            originLocationControl.LocationTypeDescription.Text = Global.tdResourceManager.GetString(
                "originSelect.labelSRSelect", TDCultureInfo.CurrentUICulture);

            destinationLocationControl.LocationInputDescription.Text = Global.tdResourceManager.GetString(
                "destinationSelect.labelSRLocation", TDCultureInfo.CurrentUICulture);
            destinationLocationControl.LocationTypeDescription.Text = Global.tdResourceManager.GetString(
                "destinationSelect.labelSRSelect", TDCultureInfo.CurrentUICulture);
            
            labelShow.Text = GetResource("PlanAJourneyControl.labelShow.Text");
            checkBoxPublicTransport.Text = GetResource("PlanAJourneyControl.checkboxPublicTransport.Text");

            checkBoxCarRoute.Text = GetResource("PlanAJourneyControl.checkboxCarRoute.Text");

            labelAdvanced.Text = GetResource(TDResourceManager.VISIT_PLANNER_RM, "VisitPlannerInput.AdvancedOptions");

            //Wire up help button
            Helpbuttoncontrol1.HelpUrl = GetResource("JourneyPlannerInput.HelpPageUrl");
        }

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// Handles visibility of links according to status of screen (eg whether return journeys exist)
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{
			// Setup gif resource for images (1 invisible image for all skip links)
			string SkipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imagePTJourneyDetailsSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageCarJourneyDetailsSkipLink1.ImageUrl = SkipLinkImageUrl;
			
			// Only show the skip links when the related sections are expanded
			if (ptPreferencesControl.PreferencesVisible)
			{
				panelPTJourneyDetailsSkipLink1.Visible = true;
				imagePTJourneyDetailsSkipLink1.AlternateText = 
					GetResource("JourneyPlannerInput.imagePTJourneyDetailsSkipLink1.AlternateText");
			}
			else
				panelPTJourneyDetailsSkipLink1.Visible = false;

			if (carPreferencesControl.PreferencesVisible)
			{
				panelCarJourneyDetailsSkipLink1.Visible = true;
				imageCarJourneyDetailsSkipLink1.AlternateText = 
					GetResource("JourneyPlannerInput.imageCarJourneyDetailsSkipLink1.AlternateText");
			}
			else
				panelCarJourneyDetailsSkipLink1.Visible = false;
		}

        /// <summary>
        /// Sets up the page optiosn next, clear, save buttons
        /// </summary>
        private void SetupNavigationButtons()
        {
            // Page options at bottom of page
            pageOptionsControlBottom.AllowBack = false;
            pageOptionsControlBottom.AllowSave = TDSessionManager.Current.Authenticated;
            pageOptionsControlBottom.AllowClear = true;
            pageOptionsControlBottom.AllowNext = true;
        }

        /// <summary>
        /// Checks if arrived to this planner from Find a car park
        /// </summary>
        /// <returns></returns>
        private bool IsFromCarParks()
        {
            // We can check if arrvied from car parks because car park results will have set the return page
            bool isFromCarParks = false;
            
            Stack returnStack = TDSessionManager.Current.InputPageState.JourneyInputReturnStack;

            if (returnStack.Count != 0)
            {
                PageId returnPageId = (PageId)returnStack.Pop();

                if ((returnPageId == PageId.FindCarParkResults) ||
                    (returnPageId == PageId.FindCarParkMap))
                {
                    isFromCarParks = true;
                }
            }

            return isFromCarParks;
        }

        /// <summary>
        /// Gets the default search type
        /// </summary>
        /// <param name="listType"></param>
        /// <returns></returns>
        private SearchType GetDefaultSearchType(DataServiceType listType)
        {
            DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
            string defaultItemValue = ds.GetDefaultListControlValue(listType);
            return (SearchType)(Enum.Parse(typeof(SearchType), defaultItemValue));
        }

        /// <summary>
        /// Updates the input page state with flags to indicate if advanced options were selected.
        /// Allows (ambiguity page) to display advanced option controls if required
        /// </summary>
        private void UpdateAdvancedOptionsSelected(bool loadUserPreferences)
        {
            inputPageState.PublicTransportTypesVisible = false;
            inputPageState.PublicTransportOptionsVisible = false;
            inputPageState.CarOptionsVisible = false;

            if (loadUserPreferences)
            {
                // Transport Types
                if (transportTypesControl.IsOptionSelected)
                {
                    inputPageState.PublicTransportTypesVisible = true;
                }

                // PT preferences
                if (ptPreferencesControl.IsOptionSelected)
                {
                    inputPageState.PublicTransportOptionsVisible = true;
                }

                // Car preferences
                if (carPreferencesControl.IsOptionSelected)
                {
                    inputPageState.CarOptionsVisible = true;
                }
            }
        }

        /// <summary>
        /// Determines if Accessible Options should be shown or not
        /// </summary>
        /// <returns></returns>
        private bool ShowAccessibleOptions()
        {
            bool show = false;

            if (!bool.TryParse(Properties.Current["AccessibleOptions.Visible.Switch"], out show))
            {
                return false;
            }

            return show;
        }

		#endregion
    }
}
