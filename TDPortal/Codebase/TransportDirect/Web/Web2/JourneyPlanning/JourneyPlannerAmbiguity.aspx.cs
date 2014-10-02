// *********************************************** 
// NAME                 : JourneyPlannerAmbiguity.aspx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 22/09/2003 
// DESCRIPTION  : Template for the Journey Planner Ambiguity page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/JourneyPlannerAmbiguity.aspx.cs-arc  $ 
//
//   Rev 1.15   Jan 30 2013 13:47:54   mmodi
//Fixed showing advanced options on ambiguity page
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.14   Jan 20 2013 16:27:22   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.13   Jan 09 2013 14:15:58   mmodi
//Send to find accessible stops page if required for Accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.12   Dec 11 2012 17:28:06   mmodi
//Display accessible option on ambiguity page
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.11   Dec 05 2012 13:38:22   mmodi
//Display locations not accessible error
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.10   Aug 28 2012 10:21:48   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.9   Mar 14 2011 15:12:08   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.8   Oct 29 2010 09:52:38   rbroddle
//Removed explicit wire up to Page_Init as AutoEventWireUp=true for this page so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.7   Jan 19 2010 13:21:02   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.6   Dec 09 2009 11:32:48   mmodi
//Fixed issue of Map being shown unexpectedly on a postback
//
//   Rev 1.5   Dec 02 2009 11:51:22   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Jun 26 2008 14:04:26   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.3   Apr 08 2008 13:52:14   apatel
//set pageoptionscontrol visibility in preference controls
//Resolution for 4846: Preference control dont retain preferences selected
//
//   Rev 1.2   Mar 31 2008 13:24:56   mturner
//Drop3 from Dev Factory
//
//
//   Rev DevFactory   Feb 04 2008 15:15:44   aahmed
//Added code to handle  button events from preferences controls
//white labelling
//
//   Rev 1.0   Nov 08 2007 13:30:06   mturner
//Initial revision.
//
//   Rev 1.131   Jun 07 2007 15:18:44   mmodi
//Added code to handle Submit event from preferences controls
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.130   Mar 15 2007 16:57:34   rbroddle
//Vantive 3694492 Fix - Commented out initialization of errors collection in UpdateOrigin and UpdateDestination methods to prevent problem if location changed with date validation error outstanding.
//Resolution for 2549: Changing the destination location on the ambiguity screen causes 'Error has occured' message
//
//   Rev 1.129   Apr 27 2006 11:20:54   mtillett
//Prevent calendar button dislpay on ambiguity page after next or back buttons clicked
//Resolution for 3510: Apps: Calendr Control problems on input/ambiguity screen
//
//   Rev 1.128   Apr 05 2006 15:24:54   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.127   Mar 24 2006 17:21:28   kjosling
//Automatically merged for stream 0023
//
//   Rev 1.126.1.0   Mar 14 2006 11:52:08   kjosling
//Redirects to journey results if requested
//Resolution for 23: DEL 8.1 Workstream - Journey Results - Phase 1
//
//   Rev 1.126   Feb 23 2006 19:39:10   AViitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.125   Feb 10 2006 15:08:40   build
//Automatically merged from branch for stream3180
//
//   Rev 1.124.1.2   Dec 22 2005 11:21:34   tmollart
//Removed calls to now redudant SaveCurrentFindaMode.
//Removed reference to OldJourneyParameters where applicable.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.124.1.1   Dec 12 2005 16:56:54   tmollart
//Removed erronous code that causes an itinerary to be reset under certain critieria.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.124.1.0   Nov 29 2005 15:44:22   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.124   Nov 28 2005 11:34:18   tmollart
//Changed transition event code used when user presses Back button.
//Resolution for 3209: UEE Hompage:  After using the Homepage miniplanner and reaching ambiguity page, pressing "back" returns you to the Hompage when it should not
//
//   Rev 1.123   Nov 21 2005 17:53:30   RGriffith
//IR3162 Resolution - Fix of Back Button navigation when coming from the homepage
//
//   Rev 1.122   Nov 15 2005 20:50:30   rgreenwood
//IR2990 Wired up help button
//
//   Rev 1.121   Nov 11 2005 15:06:12   NMoorhouse
//Hide dateControl when in Extend Journey mode
//Resolution for 2987: UEE Post Build Issue: Extend Journey Invalid Date Error
//
//   Rev 1.120   Nov 09 2005 16:15:06   mguney
//Merge for stream 2818.
//
//   Rev 1.119   Nov 03 2005 17:06:52   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.118.1.7   Nov 02 2005 14:35:30   mtillett
//Fix minor layout bugds for UUE changed
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.118.1.6   Oct 27 2005 14:01:20   NMoorhouse
//TD93 - UEE Input Pages, Date Control element CUT
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.118.1.5   Oct 25 2005 20:11:16   RGriffith
//TD089 ES020 Image Button Replacement
//
//   Rev 1.118.1.4   Oct 25 2005 17:43:16   NMoorhouse
//TD93 - Input Pages, Date Control updates
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.118.1.3   Oct 13 2005 12:02:52   mtillett
//Remove of the Help buttons and basic layout changes for UEE work
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.118.1.2   Oct 12 2005 13:03:22   mtillett
//Fix build error due to changes to the HelpCustomControl
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.118.1.1   Oct 12 2005 12:50:14   mtillett
//Updates to advanced options control to remove help and move hide button to single place in FindPageOptionsControl
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.118.1.0   Oct 12 2005 10:54:38   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.119   Oct 11 2005 10:50:24   RGriffith
//Replacing the image button with HTML button
//
//   Rev 1.118   Sep 29 2005 12:53:20   build
//Automatically merged from branch for stream2673
//
//   Rev 1.117.1.1   Sep 09 2005 14:18:24   Schand
//DN079 UEE Enter Key.
//Updates for UEE.
//Resolution for 2756: DEL 8 Stream: Add default key functionality on Input pages to Next button
//
//   Rev 1.117.1.0   Sep 07 2005 13:12:46   rgreenwood
//DN079 UEE ES015 24 Hour Help Button removal
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.117   Aug 16 2005 14:56:32   RWilby
//Merge for stream2556
//
//   Rev 1.116   Jun 30 2005 15:49:56   pcross
//Handling sentence with format "I have a large diesel engined car".
//Note that this is not handled properly as far as placement of list boxes on data entry (on JourneyPlannerInput page) - only on showing the sentence as text in a label.
//Resolution for 2367: DEL 7 Welsh text missing
//
//   Rev 1.115   May 19 2005 16:48:50   ralavi
//Changes as the result of running FXCop and also ensuring that in door to door both via PT and car text boxes remain visible when selecting new location button on one of them.
//
//   Rev 1.114   May 10 2005 17:53:30   Ralavi
//Using a new helper class for validating fuel consumption and fuel costs
//
//   Rev 1.113   Apr 26 2005 09:20:42   Ralavi
//Ensuring the mode for avoidRoad and useRoads are set to readonly when in ambiguity page and passing value from journey parameters to the control for useRoads.
//
//   Rev 1.112   Apr 22 2005 14:39:14   tmollart
//User preferences are NO LONGER reloaded when the preferences panel for Car and PT are made visible.
//Resolution for 2251: Door-to-door Logged In With Do Not Use Motorways Selected Cannot Unselect
//
//   Rev 1.111   Apr 20 2005 13:39:52   Ralavi
//Fixing fuel consumption and fuel cost related IRs 2006, 2009 and 2215
//
//   Rev 1.110   Apr 15 2005 12:48:18   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.109   Apr 13 2005 12:03:34   Ralavi
//Added code for validating fuel cost and fuel consumption
//
//   Rev 1.108   Apr 06 2005 12:15:52   Ralavi
//Passing extra string "engined" for car costing text
//
//   Rev 1.107   Apr 05 2005 11:55:22   Ralavi
//Removing references to preConvertedFuelConsumption
//
//   Rev 1.106   Apr 01 2005 13:26:52   Ralavi
//changes to convert fuel consumption to correct values
//
//   Rev 1.105   Mar 24 2005 16:08:48   RAlavi
//fix to ensure submit button works correctly.
//
//   Rev 1.104   Mar 24 2005 15:12:42   RAlavi
//Modified functionality of PT via map in door to door to be consistent with car
//
//   Rev 1.103   Mar 23 2005 17:30:50   rscott
//Help text added
//
//   Rev 1.102   Mar 23 2005 11:21:02   RAlavi
//Modifications to back button for car costing
//
//   Rev 1.101   Mar 22 2005 19:00:54   esevern
//only display car details information if it deviates from the default values
//
//   Rev 1.100   Mar 18 2005 11:16:04   RAlavi
//Latest car costing changes
//
//   Rev 1.99   Feb 24 2005 11:37:12   PNorell
//Updated for favourite details.
//
//   Rev 1.98   Sep 13 2004 17:48:34   jmorrissey
//IR1527 - updated displaying of error messages and added validation for 'overlapping' locations
//
//   Rev 1.97   Aug 24 2004 18:10:10   RPhilpott
//StationType is now passed into LocationSearchHelper (but always Undetermined for multimodal requests).
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.96   Aug 17 2004 16:48:16   RHopkins
//IRs 1290 & 1324.  Corrected tests that determine whether to output error messages for standard Date/time panel and for Extension stopover time panel.
//
//   Rev 1.95   Aug 17 2004 09:23:14   COwczarek
//Save the current Find A mode and journey parameters at the
//point of commencing a new journey plan by calling
//SaveCurrentFindAMode method.
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.94   Jul 27 2004 17:05:20   esevern
//changed date validation to use FindDateValidation
//
//   Rev 1.93   Jun 23 2004 16:54:04   COwczarek
//Additional date validation for planning journey extensions
//Resolution for 1044: Add date validation to extend journey functionality
//
//   Rev 1.92   Jun 09 2004 11:13:28   esevern
//hides stopover control if not extend in progress
//
//   Rev 1.91   Jun 04 2004 09:44:52   RPhilpott
//Ensure that default location type is picked up from DataServices, not random hard-coding.
//
//   Rev 1.90   Jun 01 2004 10:00:40   jbroome
//IR972
//Allow Partial Postcode searching directly from Journey Planner.
//Previously, this was only allowed via the location maps.
//
//   Rev 1.89   May 26 2004 10:20:14   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.88   May 21 2004 13:56:56   asinclair
//Further fix for IR 869
//
//   Rev 1.87   Apr 28 2004 16:20:14   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.86   Apr 27 2004 14:10:18   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.85   Apr 15 2004 13:40:28   COwczarek
//Remove redundant code / add comments
//Resolution for 746: Date validation for return journeys behaves strangely
//
//   Rev 1.84   Apr 13 2004 12:18:02   ESevern
//DEL5.2 QA Changes
//Resolution for 710: Calendar Control on Ambiguity Page covers footer
//
//   Rev 1.83   Apr 08 2004 14:44:48   COwczarek
//The Populate method of the TriStateLocationControl now
//accepts location type (e.g. origin, destination, via). Pass this
//value to specify the purpose of the control.
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.82   Apr 06 2004 16:36:52   COwczarek
//If return date < outward date then allow user to modify either
//dates and not just the return date.
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.81   Apr 06 2004 12:30:20   COwczarek
//Error text for an ambiguous date is now displayed above appropriate date
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.80   Apr 02 2004 17:03:36   COwczarek
//Changes for DEL 5.2 QA
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.79   Mar 26 2004 16:55:34   COwczarek
//QA changes for DEL 5.2
//Resolution for 665: Del 5.2 BBC QA changes
//
//   Rev 1.78   Mar 26 2004 12:10:50   ESevern
//DEL5.2 QA changes - resolution for IR665
//
//   Rev 1.77   Mar 26 2004 11:21:38   COwczarek
//Back button handling now reinstates original journey parameters and map searches from this page now search using location and search objects in input page state so as to allow user to cancel  location selected from map. Also changes to the way panels for travel preferences and advanced route options are displayed for DEL 5.2.
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location
//
//   Rev 1.76   Mar 22 2004 13:35:54   COwczarek
//Change naming of control ids
//
//   Rev 1.75   Mar 22 2004 10:34:32   ESevern
//DEL5.2 added additional help buttons to advanced route option via
//
//   Rev 1.74   Mar 19 2004 16:14:34   COwczarek
//Initialise error message string in UpdateErrorMessage method
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.73   Mar 16 2004 10:15:36   ESevern
//added extra help label for invalid date(s)
//
//   Rev 1.72   Mar 12 2004 15:07:34   ESevern
//DEL5.2 error message handling for date ambiguity
//
//   Rev 1.71   Mar 10 2004 11:03:06   COwczarek
//Add comments
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.70   Mar 03 2004 15:51:48   COwczarek
//Add functionality to navigate through a hierarchic search using next/back buttons.
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.69   Feb 16 2004 14:27:46   esevern
//DEL5.2: added storing fave journey name and check box selection in viewstate to fix Redirect removing journey name
//
//   Rev 1.68   Jan 21 2004 11:33:18   PNorell
//Update to 5.2
//
//   Rev 1.67   Jan 08 2004 16:18:34   passuied
//displayed Default locationtype when map LocationSearch input text is empty, NoMatch otherwise
//Resolution for 581: Map button on JP Page produces unnecessary error message
//
//   Rev 1.66   Dec 18 2003 11:20:02   passuied
//minor changes
//
//   Rev 1.65   Dec 18 2003 11:05:54   passuied
//html fixes
//Resolution for 557: JP Ambiguity: Formatting
//
//   Rev 1.64   Dec 17 2003 14:44:02   passuied
//tidied up
//
//   Rev 1.63   Dec 15 2003 12:41:46   kcheung
//Del 5.1 Journey Planner Location Map Updates after link testing on dev build 22.
//
//   Rev 1.62   Dec 13 2003 14:05:36   passuied
//fix for location controls
//
//   Rev 1.61   Dec 12 2003 20:08:10   passuied
//hides help when location from / to is valid
//
//   Rev 1.60   Dec 12 2003 17:26:26   JHaydock
//DEL 5.1 DfT formatting changes
//
//   Rev 1.59   Dec 10 2003 16:16:18   JHaydock
//Update for display of walking amibguity details
//
//   Rev 1.58   Dec 10 2003 10:58:56   JHaydock
//Adjustment so that Ambiguity page only displays sections that have changed
//
//   Rev 1.57   Dec 04 2003 13:09:38   passuied
//final version for del 5.1
//
//   Rev 1.56   Dec 01 2003 17:09:06   passuied
//added security in case user clicks many times on Find Journey(s) and there is a Naptan error. Avoids Null Exception error
//
//   Rev 1.55   Nov 27 2003 17:54:14   passuied
//changed calls to SetUpLocationSearch so it specifies if the location accepts the postcodes
//Resolution for 428: Public Via shouldn't accept postcodes
//
//   Rev 1.54   Nov 26 2003 11:36:32   passuied
//retrieved channel language to pass to ValidateAndRun
//Resolution for 397: Wrong language passed to JW
//
//   Rev 1.53   Nov 18 2003 11:49:12   passuied
//missing comments
//
//   Rev 1.52   Nov 18 2003 10:44:18   passuied
//changes to hopefully pass code review
//
//   Rev 1.51   Nov 13 2003 18:03:24   passuied
//fixed Back button of JourneyLocationMap was updating journeyParameter searches and locations.
//Instead, leave it in the same state as it was before going on the map page
//Resolution for 180: Maps Page: Back button
//
//   Rev 1.50   Nov 13 2003 12:39:56   passuied
//No change.
//Resolution for 76: Help Text on Ambiguity Page
//
//   Rev 1.49   Nov 13 2003 11:50:46   passuied
//fix for error messages display
//Resolution for 105: Journey planner hp – Ambibuity error message change
//
//   Rev 1.48   Nov 13 2003 10:48:26   passuied
//fix for invalid return date if ealier than now but after outward
//Resolution for 153: Ambiguity Page: Error handling
//
//   Rev 1.47   Nov 06 2003 12:25:50   passuied
//visual discrepancies (font size and help display)
//
//   Rev 1.46   Nov 05 2003 15:21:24   passuied
//minor changes
//
//   Rev 1.45   Nov 05 2003 13:23:54   passuied
//fixed various problems with font size
//
//   Rev 1.44   Nov 04 2003 16:35:32   passuied
//added units in Label Walking time
//
//   Rev 1.43   Nov 04 2003 15:10:14   passuied
//Changes : Whenever a travel detail changes on Input page, display Travel details panel on Ambiguity page
//Whenever a route options changes (but not empty) on Input page, display route options panel on ambiguity page
//
//   Rev 1.42   Nov 04 2003 11:20:02   passuied
//added string
//
//   Rev 1.41   Nov 03 2003 18:16:42   passuied
//fixed message display in Ambiguity page
//
//   Rev 1.40   Oct 31 2003 15:26:22   passuied
//visual fixes
//
//   Rev 1.39   Oct 30 2003 15:03:26   passuied
//fixed button ImageUrl bug
//
//   Rev 1.38   Oct 30 2003 11:22:22   passuied
//fixed bug with old journey/new journey name that can be empty (default or old journey instead)
//
//   Rev 1.37   Oct 29 2003 12:33:38   passuied
//BCN002 implementation. Removed alternate locations
//
//   Rev 1.36   Oct 28 2003 16:54:00   passuied
//changes for visual discrepancy
//
//   Rev 1.35   Oct 28 2003 10:39:12   passuied
//changes after fxcop
//
//   Rev 1.34   Oct 27 2003 14:27:30   passuied
//build 2 integration in Input pages debugged
//
//   Rev 1.33   Oct 24 2003 15:36:06   passuied
//fixed problem with build2 stuff
//
//   Rev 1.32   Oct 24 2003 11:42:38   passuied
//Build 2 for ambiguity page first working version
//
//   Rev 1.31   Oct 22 2003 17:21:50   passuied
//bug fix: When find journey, test if location selected is w +. If not get locationDetails
//
//   Rev 1.30   Oct 22 2003 13:54:16   passuied
//bug fix on location expand 2
//
//   Rev 1.29   Oct 22 2003 12:20:20   RPhilpott
//Improve CJP error handling
//
//   Rev 1.28   Oct 20 2003 16:22:54   passuied
//added boolean to indicates if user wants to save details
//
//   Rev 1.27   Oct 20 2003 12:21:22   passuied
//corrected red border display
//
//   Rev 1.26   Oct 20 2003 10:53:06   passuied
//Changes after fxcop
//
//   Rev 1.25   Oct 17 2003 15:40:56   passuied
//First working version for build2
//
//   Rev 1.24   Oct 14 2003 12:48:10   passuied
//Implemented Refresh Naptans and Toid functionality
//
//   Rev 1.23   Oct 13 2003 12:25:34   passuied
//made walkingTime/Speed highlighted
//
//   Rev 1.22   Oct 10 2003 14:25:04   passuied
//PageTitle from Resourcemanager
//
//   Rev 1.21   Oct 10 2003 12:40:58   passuied
//Put back the ShowPublicTransport and ShowCar labels
//
//   Rev 1.20   Oct 09 2003 17:36:24   passuied
//Implemented the HasNoNaptan error. Allows to reselect Walking speed and time if required
//
//   Rev 1.19   Oct 09 2003 16:48:46   passuied
//Changes in Html
//
//   Rev 1.18   Oct 09 2003 11:15:54   passuied
//corrected bug with map event handlers (was not working!)
//
//   Rev 1.17   Oct 08 2003 10:35:42   passuied
//fixed bugs with location expand and indexes
//
//   Rev 1.16   Oct 03 2003 16:59:40   passuied
//session reference bug fixed
//
//   Rev 1.15   Oct 01 2003 14:51:38   passuied
//final viewstate bug fix!!!
//
//   Rev 1.14   Sep 30 2003 17:30:54   passuied
//handled the no results from cjp and return to input page case
//
//   Rev 1.13   Sep 30 2003 15:23:02   PNorell
//Added support for ensuring only one "window" open on a web page at the same time.
//Fixed numerous click bug in the Help control.
//Fixed numerous language issues with the help control.
//Updated the journey planner input pages to contain the updated code for ensuring one window.
//Updated the wait page and took out the debug logging.
//
//   Rev 1.12   Sep 29 2003 16:14:28   passuied
//updated
//
//   Rev 1.11   Sep 26 2003 20:05:54   abork
//INTEGRATED HTML UPDATE
//
//   Rev 1.10   Sep 26 2003 11:43:36   kcheung
//Added null check for validation errors in PopulatePage otherwise page just dies
//
//   Rev 1.9   Sep 25 2003 18:06:32   PNorell
//Ensured everything is linked up together.
//Fixed various small bugs.
//
//   Rev 1.8   Sep 25 2003 16:50:44   passuied
//Corrected index problems for ambiguity and implemented back to journey Input page
//
//   Rev 1.7   Sep 25 2003 14:00:22   passuied
//last working version (LocationSearch fixed)
//
//   Rev 1.6   Sep 25 2003 09:32:06   passuied
//latest working version
//
//   Rev 1.5   Sep 24 2003 15:03:30   passuied
//last working version

using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Template for the Journey Planner Ambiguity page
    /// </summary>
    public partial class JourneyPlannerAmbiguity : TDPage
    {
        #region Private members

        #region Controls declaration

        protected System.Web.UI.WebControls.Label labelRouteOptionsTitlelabelCarOptionsTitle;
		
        #endregion

        #region User controls declaration

		protected TransportDirect.UserPortal.Web.Controls.FindCarPreferencesControl carPreferencesControl;
		protected TransportDirect.UserPortal.Web.Controls.FindCarJourneyOptionsControl journeyOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.FindPageOptionsControl pageOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.FindViaLocationControl viaLocationControl;
		protected TransportDirect.UserPortal.Web.Controls.FindLeaveReturnDatesControl dateControl;
        protected TransportDirect.UserPortal.Web.Controls.FavouriteLoadOptionsControl FavouriteLoadOptions;

        #endregion

        #region Private variables
        private DataServices.DataServices populator;
        private TDJourneyParametersMulti journeyParameters;
        private InputPageState inputPageState;

		/// <summary>
		/// Helper class responsible for common methods to non-Find A pages
		/// </summary>
		private LeaveReturnDatesControlAdapter inputDateAdapter;

        private ValidationError errors;

        // Declaration of search/location object members
        private LocationSearch originSearch;
        private TDLocation originLocation;
        private LocationSearch destinationSearch;
        private TDLocation destinationLocation;
        private LocationSearch privateViaSearch;
        private TDLocation privateViaLocation;
        private LocationSearch publicViaSearch;
        private TDLocation publicViaLocation;
        private LocationSelectControlType originType;
        private LocationSelectControlType destinationType;
        private LocationSelectControlType privateViaType;
        private LocationSelectControlType publicViaType;

		#endregion

        protected System.Web.UI.WebControls.Literal Literal3;
        protected System.Web.UI.WebControls.Panel panelNoReturn;
        protected System.Web.UI.WebControls.Label labelNoReturn;
        protected AmendStopoverControl stopoverControl;
		protected TransportDirect.UserPortal.Web.Controls.PtPreferencesControl ptPreferencesControl;
		protected TransportDirect.UserPortal.Web.Controls.PtJourneyChangesOptionsControl journeyChangesOptionsControl;
		protected TransportDirect.UserPortal.Web.Controls.PtWalkingSpeedOptionsControl walkingSpeedOptionsControl;

        protected HeaderControl headerControl;
        
		// car costing string constants
		private const string FuelConsumptionKey = "FindCarPreferencesControl.FuelConsumption";
		private const string SpecificConsumptionKey = "FindCarPreferencesControl.SpecificConsumption";
		private const string PencePerLitreKey = "FindCarPreferencesControl.PencePerLitre";
		private const string FuelCostKey = "FindCarPreferencesControl.FuelCost";
		private const string IHaveaKey = "FindCarPreferencesControl.IHaveA";
		private const string SizedKey = "FindCarPreferencesControl.Sized";
		private const string CarKey = "FindCarPreferencesControl.Car";
		private const string EnginedKey = "JourneyPlannerAmbiguity.Car";
		private const string FormatCarTypeSentenceKey = "FindCarPreferencesControl.FormatCarTypeSentence";

        private static readonly object BackEventKey = new object();

        #endregion
        
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public JourneyPlannerAmbiguity()
        {
            pageId = PageId.JourneyPlannerAmbiguity;
            populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
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
			dateControl.LeaveDateControl.DateChanged += new EventHandler(dateControlLeaveDateControl_DateChanged);
			dateControl.ReturnDateControl.DateChanged += new EventHandler(dateControlReturnDateControl_DateChanged);
			carPreferencesControl.FuelConsumptionTextChanged += new EventHandler(preferencesControl_FuelConsumptionTextChanged);
			carPreferencesControl.FuelCostTextChanged += new EventHandler(preferencesControl_FuelCostTextChanged);
			carPreferencesControl.SpecificFuelUseUnitChanged += new EventHandler(preferencesControl_FuelUseUnitChanged);

            // Locations
            originLocationControl.MapLocationClick += new EventHandler(MapFromClick);
            originLocationControl.NewLocationClick += new EventHandler(NewLocationFromClick);

            destinationLocationControl.MapLocationClick += new EventHandler(MapToClick);
            destinationLocationControl.NewLocationClick += new EventHandler(NewLocationToClick);

            ptPreferencesControl.LocationControlVia.MapClick += new EventHandler(MapViaPTClick);
            ptPreferencesControl.LocationControlVia.NewLocation += new EventHandler(NewLocationPTViaClick);

            carPreferencesControl.JourneyOptionsControl.LocationControl.MapLocationClick += new EventHandler(MapViaCarClick);
            carPreferencesControl.JourneyOptionsControl.LocationControl.NewLocationClick += new EventHandler(NewLocationCarViaClick);
            
			carPreferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.CarSizeDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.FuelTypeDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.FuelCostOptionMode = GenericDisplayMode.ReadOnly;
			
            ptPreferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode= GenericDisplayMode.ReadOnly;
			ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode= GenericDisplayMode.ReadOnly;
			ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.ReadOnly;
			ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.ReadOnly;
            carPreferencesControl.JourneyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.ReadOnly;
            carPreferencesControl.JourneyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.ReadOnly;

            accessibleOptionsControl.DisplayMode = GenericDisplayMode.ReadOnly;
		}

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            #region Initialisation
            
            journeyOptionsControl = carPreferencesControl.JourneyOptionsControl;
            pageOptionsControl = carPreferencesControl.PageOptionsControl;

            inputDateAdapter = new LeaveReturnDatesControlAdapter();

            journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            inputPageState = TDSessionManager.Current.InputPageState;
            errors = TDSessionManager.Current.ValidationError;

            LoadSessionVariables();
                        
            originType = journeyParameters.OriginType;
            destinationType = journeyParameters.DestinationType;
            publicViaType = journeyParameters.PublicViaType;
            privateViaType = journeyParameters.PrivateViaType;

            journeyOptionsControl.AvoidRoadsList = journeyParameters.AvoidRoadsList;
            journeyOptionsControl.UseRoadsList = journeyParameters.UseRoadsList;

            #endregion

            if (Page.IsPostBack)
            {
                if (TDItineraryManager.Current.ExtendInProgress)
                {
                    stopoverControl.UpdateRequestedTimes();
                }
                else
                {
                    //Save Journey Dates to Session
                    inputDateAdapter.UpdateJourneyDates(dateControl, true, journeyParameters, TDSessionManager.Current.ValidationError);
                }

                if (carPreferencesControl.FuelConsumptionOption == true)
                {
                    journeyParameters.FuelConsumptionOption = carPreferencesControl.FuelConsumptionOption;
                    carPreferencesControl.FuelConsumptionValid = true;
                    journeyParameters.FuelConsumptionValid = carPreferencesControl.FuelConsumptionValid;
                }
                else
                {
                    journeyParameters.FuelConsumptionUnit = carPreferencesControl.FuelConsumptionUnit;
                }

                if (carPreferencesControl.FuelCostOption == true)
                {
                    journeyParameters.FuelCostOption = carPreferencesControl.FuelCostOption;
                    carPreferencesControl.FuelCostValid = true;
                    journeyParameters.FuelCostValid = carPreferencesControl.FuelCostValid;
                }

                CarCostingFuelValidationHelper FuelValidation = new CarCostingFuelValidationHelper();
                //Fuel consumption and fuel cost validation entered by the user
                FuelValidation.AmbiguityFuelValidation(journeyParameters, carPreferencesControl);
                if (carPreferencesControl.FuelCostValue != "")
                {
                    journeyParameters.FuelCostEntered = carPreferencesControl.FuelCostValue;
                }
            }
            else
            {
                DisplayPanels();
            }

            PopulatePage();

            carPreferencesControl.PreferencesVisible = inputPageState.CarOptionsVisible;
            ptPreferencesControl.PreferencesVisible = inputPageState.PublicTransportOptionsVisible;

            // Set visiblity of back/clear buttons
            pageOptionsControl.AllowBack = false;
            pageOptionsControl.AllowClear = true;
            ptPreferencesControl.LocationControlVia.PageOptionsControl.AllowNext = true;
            ptPreferencesControl.WalkingSpeedOptionsControl.PageOptionsControl.AllowNext = false;
            carPreferencesControl.JourneyOptionsControl.PageOptionsControl.AllowBack = false;
            carPreferencesControl.JourneyOptionsControl.PageOptionsControl.AllowClear = true;

            carPreferencesControl.AllowSavePreferences = false;

            SetupResources();

            inputPageState.JourneyInputReturnStack.Clear();

            // DN079 UEE
            // Adding client side script for user navigation (when user hit enter, it should take the default action)
            UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

            //Added for white labelling:
            ConfigureLeftMenu("JourneyPlannerInput.clientLink.BookmarkTitle", "JourneyPlannerInput.clientLink.LinkText", null, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyPlannerAmbiguity);
            expandableMenuControl.AddExpandedCategory("Related links");
        }

        /// <summary>
        /// OnPreRender event handler 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {

            //control click events may have changed what error message should be showing
            //so check it here
            UpdateErrorMessages();

            // Refresh all values on the page.
            carPreferencesControl.DrivingSpeed = journeyParameters.DrivingSpeed;
            carPreferencesControl.FindJourneyType = journeyParameters.PrivateAlgorithmType;
            carPreferencesControl.CarSize = journeyParameters.CarSize;
            carPreferencesControl.FuelType = journeyParameters.CarFuelType;
            carPreferencesControl.FuelConsumptionOption = journeyParameters.FuelConsumptionOption;
            carPreferencesControl.FuelCostOption = journeyParameters.FuelCostOption;
            carPreferencesControl.FuelConsumptionUnit = journeyParameters.FuelConsumptionUnit;
            carPreferencesControl.DoNotUseMotorways = journeyParameters.DoNotUseMotorways;

            journeyOptionsControl.AvoidMotorways = journeyParameters.AvoidMotorWays;
            journeyOptionsControl.AvoidFerries = journeyParameters.AvoidFerries;
            journeyOptionsControl.AvoidTolls = journeyParameters.AvoidTolls;
            journeyOptionsControl.BanLimitedAccess = journeyParameters.BanUnknownLimitedAccess;
            journeyOptionsControl.AvoidRoadsList = journeyParameters.AvoidRoadsList;
            journeyOptionsControl.UseRoadsList = journeyParameters.UseRoadsList;
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = journeyParameters.WalkingSpeed;
            ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration = journeyParameters.MaxWalkingTime;
            ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed = journeyParameters.InterchangeSpeed;
            ptPreferencesControl.JourneyChangesOptionsControl.Changes = journeyParameters.PublicAlgorithmType;

            pageOptionsControltop.AllowBack = false;

            // White Labeling - this section sets options for pageOptionsControltop.
            bool showPreferences;

            if (carPreferencesControl.AmbiguityMode)
            {
                showPreferences = (carPreferencesControl.JourneyOptionsControl.Visible);
            }
            else
            {
                showPreferences = carPreferencesControl.PreferencesVisible;
            }
            
            pageOptionsControltop.AllowBack = false;
            pageOptionsControltop.AllowShowAdvancedOptions = !carPreferencesControl.AmbiguityMode && !carPreferencesControl.PreferencesVisible;

            if (showPreferences)
            {
                pageOptionsControltop.AllowHideAdvancedOptions = !carPreferencesControl.AmbiguityMode;
            }

            if (carPreferencesControl.AmbiguityMode)
            {
                pageOptionsControltop.Visible = true;
            }

            SetupMap();

            base.OnPreRender(e);
        }
        
        /// <summary>
        /// OnUnload event handler 
        /// </summary>
        /// <param name="e">event arguments</param>
        override protected void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
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
            // DN079 UEE
            // Event Handler for default action button
            this.headerControl.DefaultActionEvent += new EventHandler(this.SubmitClick);

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
            pageOptionsControl = carPreferencesControl.PageOptionsControl;
            
            pageOptionsControl.Submit += new EventHandler(SubmitClick);
            carPreferencesControl.PreferencesOptionsControl.Submit += new EventHandler(SubmitClick);
            ptPreferencesControl.PreferencesOptionsControl.Submit += new EventHandler(SubmitClick);
            ptPreferencesControl.LocationControlVia.PageOptionsControl.Submit += new EventHandler(SubmitClick);
            carPreferencesControl.JourneyOptionsControl.PageOptionsControl.Submit += new EventHandler(SubmitClick);
            carPreferencesControl.PageOptionsControl.Back += new EventHandler(this.CommandBottomBackClick);
            
            pageOptionsControl.AllowClear = false;
                        
            pageOptionsControltop.Submit += new EventHandler(SubmitClick);
            pageOptionsControltop.Back += new EventHandler(commandBack_Click);
            
            this.commandBack.Click += new EventHandler(commandBack_Click);
        }

        #endregion

        #region Public property

        public PtPreferencesControl PreferencesControl
        {
            get { return ptPreferencesControl; }
        }

        public FindCarPreferencesControl CarPreferencesControl
        {
            get { return carPreferencesControl; }
        }
        #endregion

        #region Event handlers

        /// <summary>
        /// Find Journey Click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SubmitClick(object sender, EventArgs e)
        {
            dateControl.CalendarClose();

            ITDSessionManager sessionManager = TDSessionManager.Current;

            #region Search Locations

            // Origin

            originLocationControl.Validate(journeyParameters, true, true, true, StationType.Undetermined);

            journeyParameters.Origin = originLocationControl.Search;
            journeyParameters.OriginLocation = originLocationControl.Location;

            // Destination 

            destinationLocationControl.Validate(journeyParameters, true, true, true, StationType.Undetermined);

            journeyParameters.Destination = destinationLocationControl.Search;
            journeyParameters.DestinationLocation = destinationLocationControl.Location;

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

            // Validate request and submit, if not valid then this page is redisplayed showing the errors
            JourneyPlannerInputAdapter journeyPlannerInputAdapter = new JourneyPlannerInputAdapter();
            bool valid = journeyPlannerInputAdapter.ValidateAndSearch(AmendStopoverControl.ValidateDates(), this.pageId,
                TransitionEvent.JourneyPlannerAmbiguityFind, TransitionEvent.JourneyPlannerInputErrors);
                        
            errors = sessionManager.ValidationError;

            if (!valid)
            {
                DisplayPanels();
                UpdateErrorMessages();
                inputDateAdapter.UpdateDateControl(
                    dateControl, true, journeyParameters, TDSessionManager.Current.ValidationError);
                PopulateStopOverControl();
            }
        }

        #region Back events

        /// <summary>
        /// Event Handler for back button
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">args</param>
        private void commandBack_Click(object sender, EventArgs e)
        {
            CommandBottomBackClick(sender, e);
        }

        /// <summary>
        /// Back button click event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommandBottomBackClick(object sender, EventArgs e)
        {
            dateControl.CalendarClose();

            //Check to see if there is a previous page waiting on the stack (i.e. user did not come in
            //from JourneyPlannerInput.aspx)
            if (inputPageState.JourneyInputReturnStack.Count != 0)
            {
                TransitionEvent lastPage = (TransitionEvent)inputPageState.JourneyInputReturnStack.Pop();
                //If the user is returing to the previous journey results, re-validate them
                if (lastPage == TransitionEvent.FindAInputRedirectToResults)
                {
                    TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = lastPage;
                    return;
                }
            }
            // Test if there are any ambiguous locations not currently
            // displaying their highest drilldown level

            if (IsAtHighestLevel())
            {
                // Reinstate all journey parameters that may have changed during
                // ambiguity resolution and return to previous page
                TDSessionManager.Current.AmbiguityResolution.ReinstateJourneyParameters();
                // The user may have come from the home page but we still want to return them to the main journey
                // planner input page. Prior to this we have added JourneyPlannerInput to the page stack so this
                // transition event will return the user to the appropriate place.
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerAmbiguityBack;


                carPreferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.Normal;
                carPreferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.Normal;
                carPreferencesControl.CarSizeDisplayMode = GenericDisplayMode.Normal;
                carPreferencesControl.FuelTypeDisplayMode = GenericDisplayMode.Normal;
                carPreferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.Normal;
                carPreferencesControl.FuelCostOptionMode = GenericDisplayMode.Normal;
                carPreferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.Normal;

                journeyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.Normal;
                journeyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.Normal;

                ptPreferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.Normal;
                ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.Normal;
                ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.Normal;
                ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.Normal;

                accessibleOptionsControl.DisplayMode = GenericDisplayMode.Normal;
            }
            else
            {

                // Decrement the drilldown level of any ambiguous locations
                // that are not yet at their highest level
                journeyParameters.Origin.DecrementLevel();
                journeyParameters.Destination.DecrementLevel();
                journeyParameters.PublicVia.DecrementLevel();
                journeyParameters.PrivateVia.DecrementLevel();

                LoadSessionVariables();
                InitialiseLocationControls();
            }
        }

        /// <summary>
        /// Handles the click event of the command buttons and raises the appropriate event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCommandButtonClick(object sender, EventArgs e)
        {
            EventHandler theDelegate = null;

            if (sender.Equals(commandBack))
                theDelegate = this.Events[BackEventKey] as EventHandler;

            if (theDelegate != null)
                theDelegate(this, EventArgs.Empty);

        }

        /// <summary>
        /// Occurs when the Back button is clicked
        /// </summary>
        public event EventHandler Back
        {
            add { this.Events.AddHandler(BackEventKey, value); }
            remove { this.Events.RemoveHandler(BackEventKey, value); }
        }

        #endregion

        #region Location events

        /// <summary>
        /// Creates new location and search objects and associates them with this object and 
        /// the origin location and search objects held in the session's journey parameters.
        /// Updates the origin control to accept input for a new location
        /// </summary>
        /// <param name="sender">Event originator</param>
        /// <param name="e">Event parameters</param>
        private void NewLocationFromClick(object sender, EventArgs e)
        {
            // Set local search and location to be the location controls values
            originSearch = originLocationControl.Search;
            originLocation = originLocationControl.Location;

            journeyParameters.Origin = originSearch;
            journeyParameters.OriginLocation = originLocation;

            originType = new LocationSelectControlType(ControlType.NewLocation);
            originSearch.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);

            journeyParameters.OriginType = originType;
        }

        /// <summary>
        /// Creates new location and search objects and associates them with this object and 
        /// the desitnation location and search objects held in the session's journey parameters.
        /// Updates the destination control to accept input for a new location
        /// </summary>
        /// <param name="sender">Event originator</param>
        /// <param name="e">Event parameters</param>
        private void NewLocationToClick(object sender, EventArgs e)
        {
            // Set local search and location to be the location controls values
            destinationSearch = destinationLocationControl.Search;
            destinationLocation = destinationLocationControl.Location;

            journeyParameters.Destination = destinationSearch;
            journeyParameters.DestinationLocation = destinationLocation;

            destinationType = new LocationSelectControlType(ControlType.NewLocation);
            destinationSearch.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);

            journeyParameters.DestinationType = destinationType;
        }

        /// <summary>
        /// Creates new location and search objects and associates them with this object and 
        /// the public via location and search objects held in the session's journey parameters.
        /// Updates the public via control to accept input for a new location
        /// </summary>
        /// <param name="sender">Event originator</param>
        /// <param name="e">Event parameters</param>
        private void NewLocationPTViaClick(object sender, EventArgs e)
        {
            // Set local search and location to be the location controls values
            publicViaSearch = ptPreferencesControl.LocationControlVia.Search;
            publicViaLocation = ptPreferencesControl.LocationControlVia.Location;

            journeyParameters.PublicVia = publicViaSearch;
            journeyParameters.PublicViaLocation = publicViaLocation;
            
            publicViaType = new LocationSelectControlType(ControlType.NewLocation);
            publicViaSearch.SearchType = GetDefaultSearchType(DataServiceType.PTViaDrop);

            journeyParameters.PublicViaType = publicViaType;
        }

        /// <summary>
        /// Creates new location and search objects and associates them with this object and 
        /// the private via location and search objects held in the session's journey parameters.
        /// Updates the private via control to accept input for a new location
        /// </summary>
        /// <param name="sender">Event originator</param>
        /// <param name="e">Event parameters</param>
        private void NewLocationCarViaClick(object sender, EventArgs e)
        {
            // Set local search and location to be the location controls values
            privateViaSearch = carPreferencesControl.JourneyOptionsControl.LocationControl.Search;
            privateViaLocation = carPreferencesControl.JourneyOptionsControl.LocationControl.Location;

            journeyParameters.PrivateVia = privateViaSearch;
            journeyParameters.PrivateViaLocation = privateViaLocation;
            
            privateViaType = new LocationSelectControlType(ControlType.NewLocation);
            privateViaSearch.SearchType = GetDefaultSearchType(DataServiceType.CarViaDrop);

            journeyParameters.PrivateViaType = privateViaType;
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
        
        #region Journey preference changed events

        private void preferencesControl_FuelConsumptionTextChanged(object sender, EventArgs e)
        {
            journeyParameters.FuelConsumptionEntered = carPreferencesControl.FuelConsumptionValue;
        }
        private void preferencesControl_FuelCostTextChanged(object sender, EventArgs e)
        {
            journeyParameters.FuelCostEntered = carPreferencesControl.FuelCostValue;
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

        #endregion

        #endregion

        #region Map handler Methods

        /// <summary>
        /// Origin Map click event handler
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
                mapInputControl.Visible = true;
            }
        }

        /// <summary>
        /// Destination Map click event handler
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
                mapInputControl.Visible = true;
            }

        }

        /// <summary>
        /// Via Car Map click event handler
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
                mapInputControl.Visible = true;
            }
        }

        /// <summary>
        /// Via Car Map click event handler
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
                mapInputControl.Visible = true;
            }
        }


        #endregion				

        #endregion

        #region Private methods

        #region Journey parameter session methods

        /// <summary>
        /// white labelling
        /// </summary>
        private void LoadSessionVariables()
        {
            if (journeyParameters == null)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
                throw new TDException("JourneyPlannerAmbiguity page requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
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

        #endregion

        #region Control initialise methods
        
        /// <summary>
        /// Populate controls in page
        /// </summary>
        private void PopulatePage()
        {
			// set the Page title for appropriate language
            journeyOptionsControl = carPreferencesControl.JourneyOptionsControl;
			
			if(!TDItineraryManager.Current.ExtendInProgress)
			{
				inputDateAdapter.UpdateDateControl(dateControl, true, journeyParameters, TDSessionManager.Current.ValidationError);
			}
			else
			{
				dateControl.Visible = false;
			}

            InitialiseLocationControls();

            PopulateStopOverControl();

            PopulateAccessibleOptionsControl();

            #region Population of labels
            labelOptions.Text = GetResource("JourneyPlannerAmbiguity.labelOptions");
			labelShow.Text =
                Global.tdResourceManager.GetString("JourneyPlannerAmbiguity.labelShow", TDCultureInfo.CurrentUICulture);

            labelOriginTitle.Text = 
                Global.tdResourceManager.GetString("JourneyPlannerAmbiguity.labelOriginTitle", TDCultureInfo.CurrentUICulture);
            labelDestinationTitle.Text =
                Global.tdResourceManager.GetString("JourneyPlannerAmbiguity.labelDestinationTitle", TDCultureInfo.CurrentUICulture);

            #region Population of Find Public tranport/Car label

            // displays 1 single label stating if we're trying to find public transports AND/OR car journeys
            string transports = string.Empty;
            
            if (journeyParameters.PublicRequired)
                transports += Global.tdResourceManager.GetString("JourneyPlanner.PublicTransportLowerCase", TDCultureInfo.CurrentUICulture);
            
            transports += (journeyParameters.PrivateRequired && journeyParameters.PublicRequired)? 
                " " + Global.tdResourceManager.GetString("JourneyPlanner.AndLowerCase", TDCultureInfo.CurrentUICulture) + " " : string.Empty;
        
            if (journeyParameters.PrivateRequired)
                transports += Global.tdResourceManager.GetString("JourneyPlanner.CarLowerCase", TDCultureInfo.CurrentUICulture);
            
            labelShow.Text = string.Format(labelShow.Text, transports);

			#endregion

			PopulateCarDetailsDisplay();

            // Go through all selected public modes and add them to the table to display them
            int currentColumn = 0;
            int maxColumns = 2;
            TableRow row = new TableRow();
            foreach (ModeType mode in journeyParameters.PublicModes)
            {
                if (row.Cells.Count == maxColumns)
                {
                    //tablePublicTransport.Rows.Add(row);
                    currentColumn = 0;
                    row = new TableRow();
                }
                switch (mode)
                {
                    case ModeType.Air:
                    {
                        TableCell cell = new TableCell();
                        Label label = new Label();
                        label.Text = Global.tdResourceManager.GetString(
                            "JourneyPlannerAmbiguity.labelAir",
                            TDCultureInfo.CurrentUICulture);
                        cell.Controls.Add(label);
                        row.Cells.Add(cell);
                    
                        currentColumn++;
                        break;
                    }
                    case ModeType.Bus:
                    {
                        TableCell cell = new TableCell();
                        Label label = new Label();
                        label.Text = Global.tdResourceManager.GetString(
                            "JourneyPlannerAmbiguity.labelBus",
                            TDCultureInfo.CurrentUICulture);
                        cell.Controls.Add(label);
                        row.Cells.Add(cell);
                    
                        currentColumn++;
                        break;
                    }
                    case ModeType.Ferry:
                    {
                        TableCell cell = new TableCell();
                        Label label = new Label();
                        label.Text = Global.tdResourceManager.GetString(
                            "JourneyPlannerAmbiguity.labelFerry",
                            TDCultureInfo.CurrentUICulture);
                        cell.Controls.Add(label);
                        row.Cells.Add(cell);
                    
                        currentColumn++;
                        break;
                    }
                    case ModeType.Metro:
                    {
                        TableCell cell = new TableCell();
                        Label label = new Label();
                        label.Text = Global.tdResourceManager.GetString(
                            "JourneyPlannerAmbiguity.labelTube",
                            TDCultureInfo.CurrentUICulture);
                        cell.Controls.Add(label);
                        row.Cells.Add(cell);
                    
                        currentColumn++;                        
                        break;
                    }
                    case ModeType.Rail:
                    {
                        TableCell cell = new TableCell();
                        Label label = new Label();
                        label.Text = Global.tdResourceManager.GetString(
                            "JourneyPlannerAmbiguity.labelTrain",
                            TDCultureInfo.CurrentUICulture);
                        cell.Controls.Add(label);
                        row.Cells.Add(cell);
                    
                        currentColumn++;
                        break;
                    }
                    case ModeType.Telecabine:
                    {
                        TableCell cell = new TableCell();
                        Label label = new Label();
                        label.Text = Global.tdResourceManager.GetString(
                            "JourneyPlannerAmbiguity.labelTelecabine",
                            TDCultureInfo.CurrentUICulture);
                        cell.Controls.Add(label);
                        row.Cells.Add(cell);

                        currentColumn++;
                        break;
                    }
                    case ModeType.Tram:
                    {
                        TableCell cell = new TableCell();
                        Label label = new Label();
                        label.Text = Global.tdResourceManager.GetString(
                            "JourneyPlannerAmbiguity.labelTram",
                            TDCultureInfo.CurrentUICulture);
                        cell.Controls.Add(label);
                        row.Cells.Add(cell);
                    
                        currentColumn++;
                        break;
                    }
                }

            }

            #endregion
        }

        /// <summary>
        /// Initialises the origin, destination and via location controls
        /// </summary>
        private void InitialiseLocationControls()
        {
            // Force update of status to ambiguous as the location search only sets it to ambiguous
            // if the search has location choices - but we want to force ambiguous so location
            // control renders in ambigous mode if required
            if (!Page.IsPostBack)
            {
                if (originLocation.Status == TDLocationStatus.Unspecified)
                    originLocation.Status = TDLocationStatus.Ambiguous;
                
                if (destinationLocation.Status == TDLocationStatus.Unspecified)
                    destinationLocation.Status = TDLocationStatus.Ambiguous;
                
                if (inputPageState.PublicTransportOptionsVisible 
                    && publicViaLocation.Status == TDLocationStatus.Unspecified
                    && !string.IsNullOrEmpty(publicViaSearch.InputText))
                    publicViaLocation.Status = TDLocationStatus.Ambiguous;
                
                if (inputPageState.CarOptionsVisible
                    && privateViaLocation.Status == TDLocationStatus.Unspecified
                    && !string.IsNullOrEmpty(privateViaSearch.InputText))
                    privateViaLocation.Status = TDLocationStatus.Ambiguous;
            }

            originLocationControl.Initialise(originLocation, originSearch, DataServiceType.FromToDrop, true, true, false, true, true, true, false, true, false);
            destinationLocationControl.Initialise(destinationLocation, destinationSearch, DataServiceType.FromToDrop, true, true, false, true, true, true, false, true, false);
            ptPreferencesControl.LocationControlVia.Initialise(publicViaLocation, publicViaSearch, DataServiceType.PTViaDrop, true, true, false, true, false, true, false, true, false);
            carPreferencesControl.JourneyOptionsControl.LocationControl.Initialise(privateViaLocation, privateViaSearch, DataServiceType.CarViaDrop, true, true, false, true, true, true, false, true, false);

            // Show the (auto suggest) via location and hide the standard via control
            ptPreferencesControl.ShowLocationControlAutoSuggest = true;
            carPreferencesControl.JourneyOptionsControl.ShowLocationControlAutoSuggest = true;
        }

		private void PopulateCarDetailsDisplay() 
		{
			// check for non-default values for car details
		
			bool fuelCostChanged = false;

			if( journeyParameters.FuelCostEntered != string.Empty
				&& !journeyParameters.FuelCostOption) // check if the fuel cost option is true (ie default) 
			{
				fuelCostChanged = true;
			}
			
			// default value may be set for this so check for that as well
			bool fuelConsumptionChanged = false;

			

			if(journeyParameters.FuelConsumptionEntered != string.Empty 
				&& !journeyParameters.FuelConsumptionOption) // check if the fuel consumption option is true (ie default) 
			{
					fuelConsumptionChanged = true;
					
					
			}

			bool fuelTypeChanged = !journeyParameters.CarFuelType.Equals(
				populator.GetDefaultListControlValue(DataServiceType.ListFuelTypeDrop)); //petrol is default

			bool carSizeChanged = !journeyParameters.CarSize.Equals(
				populator.GetDefaultListControlValue(DataServiceType.ListCarSizeDrop)); //medium is default
				
			string langResource = "DataServices.FuelConsumptionUnitDrop.Option" 
				+ journeyParameters.FuelConsumptionUnit.ToString();

			carPreferencesControl.FuelConsumptionValid = journeyParameters.FuelConsumptionValid;
			carPreferencesControl.FuelCostValid = journeyParameters.FuelCostValid;
				
            // set display fuel consumption text
            if (((fuelConsumptionChanged)) ||
                ((carPreferencesControl.FuelConsumptionOption == false) && (carPreferencesControl.FuelConsumptionValue != string.Empty)))
            {
				
                if (journeyParameters.FuelConsumptionValid == true)
                {
                   
                    carPreferencesControl.FuelConsumptionValid = true;

                    carPreferencesControl.FuelConsumptionLabel.Text = GetResource(FuelConsumptionKey) 
                        + " " + 
                        journeyParameters.FuelConsumptionEntered 
                        + " " + 
                        GetResource(langResource); 
                    carPreferencesControl.PanelCarDetails.Visible = true;
                }
                else
                {
                    carPreferencesControl.FuelConsumptionOption = journeyParameters.FuelConsumptionOption;
                    carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
                }
            }
			

            // set display fuel cost text
            if ((fuelCostChanged)||
                ((carPreferencesControl.FuelCostOption == false) && (carPreferencesControl.FuelCostValue != string.Empty)))
            {
                if (journeyParameters.FuelCostValid == true)
                {
                    carPreferencesControl.FuelCostLabel.Text = GetResource(FuelCostKey) 
                        + " " + 
                        journeyParameters.FuelCostEntered
                        + " " + 
                        GetResource(PencePerLitreKey); 
                    carPreferencesControl.PanelCarDetails.Visible = true;
                }
                else
                {
                    carPreferencesControl.FuelCostOption = journeyParameters.FuelCostOption;
                    carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;
                }

            }
			

			// set display fuelt type and car size text
			if(fuelTypeChanged || carSizeChanged) 
			{
				// This sentence to be shown on a label is a concatenation of 5 parts to get something like:
				// "I have a medium sized diesel engined car".
				// The Welsh version of the sentence has a different order of parts so we have a resource key for
				// the sentence that receives the parts and orders them accordingly.
							
				// Set up the array of sentence parts to be ordered.
				string[] CarTypeSentenceParts = new string[4];
				CarTypeSentenceParts[0] = journeyParameters.CarSize;
				CarTypeSentenceParts[1] = GetResource(SizedKey);
				CarTypeSentenceParts[2] = journeyParameters.CarFuelType;	// Note: this should say petrol engined, not just petrol
				CarTypeSentenceParts[3] = GetResource(CarKey);
				
				carPreferencesControl.CarDetailsLabel.Text = GetResource(IHaveaKey) + " " + String.Format(GetResource(FormatCarTypeSentenceKey), CarTypeSentenceParts);
				
				carPreferencesControl.CarDetailsLabel.Visible = true;
				carPreferencesControl.PanelCarDetails.Visible = true;
			}
			//if none of the car details have changed, hide the cardetails panel
			if(! fuelTypeChanged && !carSizeChanged && ! fuelCostChanged && !fuelConsumptionChanged) 
			{
				carPreferencesControl.PanelCarDetails.Visible = false;
			}
		}

        /// <summary>
        /// Methods that hides/shows different sections depending on user changes/ambiguities
        /// </summary>
        private void DisplayPanels()
        {
            // display panels
            bool displayTravelPublicTransport = inputPageState.TravelOptionsChanged.Contains(TravelOptionsChangedEnum.PublicTransport) && IsValidModes;
			journeyOptionsControl = carPreferencesControl.JourneyOptionsControl;
            
			ptPreferencesControl.PreferencesVisible = inputPageState.PublicTransportOptionsVisible;
			carPreferencesControl.PreferencesVisible = inputPageState.CarOptionsVisible;
			
			//Added for car costing
			carPreferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.CarSizeDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.FuelTypeDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.FuelCostOptionMode = GenericDisplayMode.ReadOnly;
			ptPreferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.ReadOnly;
			ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.ReadOnly;
			ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.ReadOnly;
			ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.JourneyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.ReadOnly;
			carPreferencesControl.JourneyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.ReadOnly;
        }

        /// <summary>
        /// Update error messages displayed on page according to the validation errors
        /// </summary>
        private void UpdateErrorMessages()
        {

			//"Then click Next" label 
			bool showNextText = false;  

			//error message 
			labelErrorMessages.Text = string.Empty;

			//ensure we are dealing with current error
			errors = TDSessionManager.Current.ValidationError;

            // If transport modes are invalid, then this is the only message displayed
            // by this page. Since transport selection cannot be corrected on this page,
            // the user should not be encouraged to correct other ambiguities since
            // clicking "back" cancels any changes made.

            if(!IsValidModes) 
            {
                labelErrorMessages.Text = Global.tdResourceManager.GetString(
                    "ValidateAndRun.ChooseAnOption", TDCultureInfo.CurrentUICulture);

            } 
            else if (errors.Contains(ValidationErrorID.ExtendFromEndOutwardInPast)) 
            {
                labelErrorMessages.Text = Global.tdResourceManager.GetString(
                    "ValidateAndRun.ExtendFromEndOutwardInPast", TDCultureInfo.CurrentUICulture);
            
            }
            else if (errors.Contains(ValidationErrorID.ExtendFromEndReturnInPast)) 
            {
                labelErrorMessages.Text = Global.tdResourceManager.GetString(
                    "ValidateAndRun.ExtendFromEndReturnInPast", TDCultureInfo.CurrentUICulture);
            
            }
            else if (errors.Contains(ValidationErrorID.ExtendToStartOutwardInPast)) 
            {
                labelErrorMessages.Text = Global.tdResourceManager.GetString(
                    "ValidateAndRun.ExtendToStartOutwardInPast", TDCultureInfo.CurrentUICulture);
            
            }
            else if (errors.Contains(ValidationErrorID.ExtendToStartReturnInPast)) 
            {
                labelErrorMessages.Text = Global.tdResourceManager.GetString(
                    "ValidateAndRun.ExtendToStartReturnInPast", TDCultureInfo.CurrentUICulture);
            
            }
            else if(TDSessionManager.Current.ValidationError.MessageIDs.Count > 0)
            {
				//if error exists then the "Show Next" text is needed
				showNextText = true;     
             
                // Display "select from list..." if origin or destination locations are
                // ambiguous
                if(AreLocationsAmbiguous) 
                {
                    // If there are no ambiguouse search results, then display location unspecified error
                    if ((originSearch != null && originSearch.CurrentLevel < 0) &&
                     (destinationSearch != null && destinationSearch.CurrentLevel < 0) &&
                     (publicViaSearch != null && publicViaSearch.CurrentLevel < 0) &&
                     (privateViaSearch != null && privateViaSearch.CurrentLevel < 0))
                    {
                        labelErrorMessages.Text += Global.tdResourceManager.GetString(
                            "ValidateAndRun.SelectLocation", TDCultureInfo.CurrentUICulture) + " ";
                    }
                    // Otherwise display both select from list and type a new location. 
                    // No need to do any deeper if checks here
                    else
                    {
                        labelErrorMessages.Text = Global.tdResourceManager.GetString(
                            "ValidateAndRun.SelectFromList", TDCultureInfo.CurrentUICulture) + " ";
                        labelErrorMessages.Text += Global.tdResourceManager.GetString(
                            "ValidateAndRun.SelectLocation", TDCultureInfo.CurrentUICulture) + " ";
                    }
                }

                // Display "Select/type in a new location" if origin or destination locations
                // have not been specified or cannot be resolved
                if(AreLocationsUnspecified)
                {
                    string message = Global.tdResourceManager.GetString(
                        "ValidateAndRun.SelectLocation", TDCultureInfo.CurrentUICulture);
                    
                    if (!labelErrorMessages.Text.Contains(message))
                        labelErrorMessages.Text += message + " ";
                }

				// IR1527 - Display "The origin and destination locations you have chosen overlap..." 
				//if origin and destination locations overlap i.e. share the same naptans 				
				if(AreOriginAndDestinationLocationsOverlapping(errors)) 
				{
					labelErrorMessages.Text = Global.tdResourceManager.GetString(
						"ValidateAndRun.DoorToDoorOriginAndDestinationOverlap", TDCultureInfo.CurrentUICulture) + " ";
					showNextText = false;					
				}

				// IR1527 - Display "The origin and via locations you have chosen overlap..." 
				//if origin and via locations overlap i.e. share the same naptans 				
				if(AreOriginAndViaLocationsOverlapping(errors)) 
				{
					labelErrorMessages.Text = Global.tdResourceManager.GetString(
						"ValidateAndRun.DoorToDoorOriginAndViaOverlap", TDCultureInfo.CurrentUICulture) + " ";	
					showNextText = false;					
				}

				// IR1527 - Display "The destination and via locations you have chosen overlap..." 
				//if destination and via locations overlap i.e. share the same naptans 				
				if(AreDestinationAndViaLocationsOverlapping(errors)) 
				{
					labelErrorMessages.Text = Global.tdResourceManager.GetString(
						"ValidateAndRun.DoorToDoorDestinationAndViaOverlap", TDCultureInfo.CurrentUICulture) + " ";	
					showNextText = false;					
				}

                // Display the locations are not accessible error message. 
                // The user should have been sent to the Find Nearest Accessible Stop page, unless there were 
                // other validation errors, so only show the message if there are no other errors - as 
                // this is only shown where the page flow fails!
                if (AreLocationsNotAccessible)
                {
                    if (errors.ErrorIDs != null && errors.ErrorIDs.Length == 1)
                    {
                        ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.ValidationError);
                        showNextText = false;
                    }
                }

                if (TDItineraryManager.Current.ExtendInProgress) 
                {

                    stopOverErrorMessageLabel.Text = string.Empty;

					if ( FindDateValidation.OutwardDateInPast || FindDateValidation.ReturnDateInPast || FindDateValidation.AreDatesPast)
					{
                        stopOverErrorMessageLabel.Text = Global.tdResourceManager.GetString(
                            "ValidateAndRun.StopOverTimeIntoPast",
                            TDCultureInfo.CurrentUICulture) + " ";
                    }

                    if (FindDateValidation.IsExtensionReturnOverlap) 
                    {
                        stopOverErrorMessageLabel.Text += Global.tdResourceManager.GetString(
                            "ValidateAndRun.ExtensionReturnOverlap",
                            TDCultureInfo.CurrentUICulture);
                    }
                }

                // Add "then click next" but only if there was error text displayed
                if ((labelErrorMessages.Text.Length != 0) && (showNextText))
                {
                    labelErrorMessages.Text += Global.tdResourceManager.GetString(
                        "ValidateAndRun.ClickNext",
                        TDCultureInfo.CurrentUICulture);
                }

            }

        }

        /// <summary>
        /// Intialises the stop over control depending on whether an extend is in
        /// progress and if so, whether it should be displayed as readonly, editable or
        /// highlighting erroneous times.
        /// </summary>
        private void PopulateStopOverControl() 
        {

            if(!TDItineraryManager.Current.ExtendInProgress) 
            {
                panelStopover.Visible = false;
            } 
            else 
            {
                if (FindDateValidation.IsExtensionReturnOverlap || FindDateValidation.IsOutwardAndReturnExtensionStartInPast) 
                {
                    this.stopoverControl.Initialise(this.PageId,false,true,true);
                } 
                else if (FindDateValidation.OutwardExtensionToStartInPast) 
                {
                    this.stopoverControl.Initialise(this.PageId,false,true,false);
                } 
                else if (FindDateValidation.ReturnExtensionToStartInPast) 
                {
                    this.stopoverControl.Initialise(this.PageId,false,false,true);
                } 
                else 
                {
                    this.stopoverControl.Initialise(this.PageId,true,false,false);
                }
            }

        }

        /// <summary>
        /// Initialises the accessible options control
        /// </summary>
        private void PopulateAccessibleOptionsControl()
        {
            accessibleOptionsControl.Initialise(
                journeyParameters.RequireStepFreeAccess,
                journeyParameters.RequireSpecialAssistance,
                journeyParameters.RequireFewerInterchanges);
        }

        #endregion

        #region Valid data Checks

        /// <summary>
        /// True if any location (origin, destination, public via, private via) is
        /// unspecified
        /// </summary>
        private bool AreLocationsUnspecified
        {
            get
            {
                return
                    errors.Contains(ValidationErrorID.OriginLocationInvalid) ||
                    errors.Contains(ValidationErrorID.OriginLocationInvalidAndOtherErrors) ||
                    errors.Contains(ValidationErrorID.DestinationLocationInvalid) ||
                    errors.Contains(ValidationErrorID.DestinationLocationInvalidAndOtherErrors) ||
                    errors.Contains(ValidationErrorID.PrivateViaLocationInvalid) ||
                    errors.Contains(ValidationErrorID.PrivateViaLocationInvalidAndOtherErrors) ||
                    errors.Contains(ValidationErrorID.PublicViaLocationInvalid) ||
                    errors.Contains(ValidationErrorID.PublicViaLocationInvalidAndOtherErrors);
            }
        }

        /// <summary>
        /// True if any location (origin, destination, public via, private via) is
        /// an ambiguous state
        /// </summary>
        private bool AreLocationsAmbiguous
        {
            get
            {
                return
                    errors.Contains(ValidationErrorID.AmbiguousOriginLocation) ||
                    errors.Contains(ValidationErrorID.AmbiguousDestinationLocation) ||
                    errors.Contains(ValidationErrorID.AmbiguousPublicViaLocation) ||
                    errors.Contains(ValidationErrorID.AmbiguousPrivateViaLocation);
            }
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// erroneous results for any location (to, from, public via or private via).
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicate erroneous results for any location, false otherwise</returns>
        public virtual bool AreOriginAndDestinationLocationsOverlapping(ValidationError errors)
        {
            return
                errors.Contains(ValidationErrorID.OriginAndDestinationOverlap);
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// erroneous results for any location (to, from, public via or private via).
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicate erroneous results for any location, false otherwise</returns>
        public virtual bool AreOriginAndViaLocationsOverlapping(ValidationError errors)
        {
            return
                errors.Contains(ValidationErrorID.OriginAndViaOverlap);
        }

        /// <summary>
        /// Returns true if supplied ValidationError object contains errors indicating
        /// erroneous results for any location (to, from, public via or private via).
        /// </summary>
        /// <param name="errors">errors to search</param>
        /// <returns>True if errors indicate erroneous results for any location, false otherwise</returns>
        public virtual bool AreDestinationAndViaLocationsOverlapping(ValidationError errors)
        {
            return
                errors.Contains(ValidationErrorID.DestinationAndViaOverlap);
        }

        /// <summary>
        /// True if any location (origin, destination, public via) are 
        /// not accessible
        /// </summary>
        private bool AreLocationsNotAccessible
        {
            get
            {
                return
                    errors.Contains(ValidationErrorID.OriginLocationNotAccessible) ||
                    errors.Contains(ValidationErrorID.DestinationLocationNotAccessible) ||
                    errors.Contains(ValidationErrorID.PublicViaLocationNotAccessible);
            }
        }

        /// <summary>
        /// Returns false if no modes selected
        /// </summary>
        private bool IsValidModes
        {
            get
            {
                if (errors.Contains(ValidationErrorID.NoModesSelected))
                    return false;
                else
                    return true;
            }
        }

        /// <summary>
        /// gets if no naptan error is raised
        /// </summary>
        private bool IsValidNaptan
        {
            get
            {
                if (errors.Contains(ValidationErrorID.OriginLocationHasNoNaptan)
                    || errors.Contains(ValidationErrorID.DestinationLocationHasNoNaptan))
                    return false;
                else
                    return true;
            }
        }

        #endregion

        #region Map methods

        /// <summary>
        /// Sets up a map for the location search and type of map location mode
        /// If map location mode is via map gets initialised with start, via and end mode.
        /// By default map gets initialised with start and end mode.
        /// </summary>
        /// <param name="locationSearch">Location search</param>
        /// <param name="mapLocationMode">Map location mode</param>
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
        
        /// <summary>
        /// Loads text and image resources
        /// </summary>
        private void SetupResources()
        {
            // Location input screen reader text
            originLocationControl.LocationInputDescription.Text = GetResource("originSelect.labelSRSelect");
            originLocationControl.LocationTypeDescription.Text = GetResource("originSelect.labelSRLocation");

            destinationLocationControl.LocationInputDescription.Text = GetResource("destinationSelect.labelSRSelect");
            destinationLocationControl.LocationTypeDescription.Text = GetResource("destinationSelect.labelSRLocation");
            
            helpbutton.HelpUrl = GetResource("JourneyPlannerAmbiguity.HelpAmbiguityUrl");
            commandBack.Text = GetResource("FindPageOptionsControl.Back.Text");
        }

        /// <summary>
        /// Tests whether all ambiguous locations on the page are currently
        /// at their highest drillable level.
        /// </summary>
        /// <returns>False if their is any ambiguous location and it is not at
        /// the highest level, otherwise true</returns>
        private bool IsAtHighestLevel()
        {
            return !(
            (journeyParameters.OriginLocation.Status == TDLocationStatus.Ambiguous &&
                journeyParameters.Origin.CurrentLevel > 0) ||
            (journeyParameters.DestinationLocation.Status == TDLocationStatus.Ambiguous &&
                journeyParameters.Destination.CurrentLevel > 0) ||
            (journeyParameters.PrivateViaLocation.Status == TDLocationStatus.Ambiguous &&
                journeyParameters.PrivateVia.CurrentLevel > 0) ||
            (journeyParameters.PublicViaLocation.Status == TDLocationStatus.Ambiguous &&
                journeyParameters.PublicVia.CurrentLevel > 0));
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

        #endregion

        #region UNUSED METHODS

        // FOLLOWING METHODS SHOULD BE DELETED WHEN CONFIRMED THEY ARE ABSOLUTELY NOT USED

        ///// <summary>
        ///// Set up a new search using the Map Location objects (search and location)
        ///// </summary>
        ///// <param name="inputText">Input text</param>
        ///// <param name="searchType">Search type</param>
        ///// <param name="fuzzy">Fuzzy search</param>
        ///// <param name="acceptsPostcode">location can accept postcodes</param>
        ///// <param name="acceptsPartPostcode">location can accept partial postcodes</param>
        //private void MapSearch(string inputText, SearchType searchType, bool fuzzy, bool acceptsPostcode, bool acceptsPartPostcode)
        //{

        //    inputPageState.MapLocationSearch.ClearAll();
        //    inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
        //    inputPageState.MapLocationSearch.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);

        //    LocationSearch thisSearch = inputPageState.MapLocationSearch;
        //    TDLocation thisLocation = inputPageState.MapLocation;

        //    LocationSearchHelper.SetupLocationParameters(
        //        inputText,
        //        searchType,
        //        fuzzy,
        //        0,
        //        journeyParameters.MaxWalkingTime,
        //        journeyParameters.WalkingSpeed,
        //        ref thisSearch,
        //        ref thisLocation,// check that it is passed as reference
        //        acceptsPostcode,
        //        acceptsPartPostcode,
        //        StationType.Undetermined
        //        );

        //    inputPageState.MapLocationSearch = thisSearch;
        //    inputPageState.MapLocation = thisLocation;
        //}

        #endregion
    }
}
