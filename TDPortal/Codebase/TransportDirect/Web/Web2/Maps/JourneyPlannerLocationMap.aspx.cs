// *********************************************** 
// NAME                 : JourneyPlannerLocationMap.aspx.cs
// AUTHOR               : Patrick Assuied / Kenny Cheung
// DATE CREATED         : 02/10/2003
// DESCRIPTION			: Journey Planner Map page.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Maps/JourneyPlannerLocationMap.aspx.cs-arc  $
//
//   Rev 1.11   Oct 30 2009 11:50:52   apatel
//Stop information changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.10   Jan 30 2009 10:44:26   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.9   Jan 14 2009 11:52:38   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.8   Oct 24 2008 15:13:10   mmodi
//Manual merge of stream5014
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//Resolution for 5151: Cycle Planner - Gazetteer (i.e. choose a new type of location) shown in dropdown is 'Town/ district/village' instead of 'Station/Airport' on 'Find a Map' page.
//
//   Rev 1.7   Jun 26 2008 14:04:46   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.6.1.0   Jul 28 2008 13:14:18   mmodi
//Updates for Cycle planner find on map button
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.6   May 23 2008 11:53:42   pscott
//SCR 4996   UK:2634563
//Ensure language is checked correctly so that map not lost on change f journey
//
//   Rev 1.5   May 06 2008 09:38:46   apatel
//cc
//
//   Rev 1.4   May 01 2008 17:18:16   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.3   Apr 02 2008 13:02:26   apatel
//moved new location button from tristatelocation control to top of the page as back button
//
//   Rev 1.2   Mar 31 2008 13:26:04   mturner
//Drop3 from Dev Factory
//
// Rev DevFactory Mar 17 2008 15:15:00 dgath
// Added control HelpLocationControl2 for display on results page  
//
//  Rev DevFactory Feb 28 2008 22:08:00 apatel
//  set the text for the mab symbol label. added newsearch button.
//
//  Rev Devfactory Feb 04 2008 08:56:00 apatel
//  CCN - 0427 Changed the layout of the page, added left hand menu, added right hand menu, removed the zoom controls and 
//  made the page to show left hand menu when map not displaying. Also, Changed map to stretch to the page.
//
//   Rev 1.0   Nov 08 2007 13:30:12   mturner
//Initial revision.
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//   Rev 1.170   Sep 03 2007 15:25:30   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.169   May 11 2007 14:53:30   nrankin
//Code change to remove Gazetteer postfixes from location name on maps
//Resolution for 4406: Gaz post fixes should not be displayed on maps
//
//   Rev 1.168   Apr 25 2007 13:22:02   mmodi
//Called SetupCarParkPlanning when location selected is a car park
//Resolution for 4396: Car Park: Selecting Car park from Map does not show Costs amount in Tickets/Costs
//
//   Rev 1.167   Jan 08 2007 15:49:56   jfrank
//Any <modes> have been changed to Main <modes>, this will break previous pseudo naptans CCN's.  This change means this will not occur.
//Resolution for 4333: Gaz Improvement Workshop - Actions 6, 9, 17 - Gaz Config and Naptan Config Changes
//
//   Rev 1.166   Oct 09 2006 11:37:58   tmollart
//Manual merge of strem 4143.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.165   Sep 05 2006 14:08:30   mturner
//Added code to remove sub strings denoting pseudo locations e.g "Any Rail" from the labels displayed on maps.
//
//   Rev 1.164   Jul 26 2006 15:30:26   MModi
//Amended HasLanguageChanged to handle when entry is from Homepage Find a place miniplanner
//Resolution for 4142: Maps - Navigation error when selecting Welsh on the map results page
//
//   Rev 1.163   Jun 15 2006 10:27:06   mmodi
//Added page language changed check when intialising map to prevent map being cleared
//Resolution for 4117: Maps - Navigation error when selecting Welsh on the map results page
//
//   Rev 1.162   Jun 01 2006 08:38:38   mmodi
//IR4105: Added code to repopulate Select New Location dropdown list (when user returns back from Help page)
//Resolution for 4105: Del 8.2 - Select new location map feature dropdown values are lost
//
//   Rev 1.161   Apr 12 2006 13:11:04   esevern
//Added extra check when updating the From and To locations (when user elects to use a resolved 'Find on Map' location for journey planning), to prevent a new, empty destination NaPTANs array being created. 
//Resolution for 3872: DN093 Find a Bus: Unable to plan a journey when Find on Map used
//
//   Rev 1.160   Apr 05 2006 15:25:26   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.159   Apr 04 2006 14:17:56   build
//Automatically merged from stream 0034
//
//   Rev 1.158.1.0   Mar 29 2006 17:50:42   RWilby
//Fixed issue identified during the Map symbol update work.
//Resolution for 3715: Map Symbols
//
//   Rev 1.158   Feb 23 2006 19:37:42   AViitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.157   Feb 21 2006 11:44:04   aviitanen
//Merge from stream0009 
//
//   Rev 1.156   Feb 10 2006 15:08:46   build
//Automatically merged from branch for stream3180
//
//   Rev 1.155.1.1   Dec 08 2005 17:08:18   RGriffith
//Removal of TabSection references
//
//   Rev 1.155.1.0   Nov 30 2005 15:08:04   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.155   Nov 23 2005 19:13:30   RGriffith
//Code review suggestions for stream2880
//
//   Rev 1.154   Nov 17 2005 12:20:18   pcross
//Manual merge of stream2880
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.151.1.0   Nov 07 2005 19:10:18   schand
//Merged stream2880 (FindAPlaceControl)changes to this trunk
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.153   Nov 14 2005 18:27:16   RGriffith
//IR2985 Fix - Added hiding of PrinterFriendly button until results are found.
//
//   Rev 1.152   Nov 10 2005 17:51:38   asinclair
//Correct merge for VP
//
//   Rev 1.151   Nov 03 2005 16:10:42   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.150.1.1   Nov 02 2005 11:51:32   rgreenwood
//TD089 ES020 Corrections to Back button.
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.150.1.0   Oct 25 2005 19:11:52   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.151   Oct 07 2005 16:33:24   RGriffith
//Replacing the image button with HTML button
//
//   Rev 1.150   Sep 29 2005 10:44:20   schand
//Merged stream 2673 back into trunk
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.149   Sep 26 2005 09:19:30   MTurner
//Resolution for IR 2776 - Amended handling of TabSection to prevent exceptions being thrown if this has not already been set by another page.
//
//   Rev 1.148   May 20 2005 11:58:02   ralavi
//Changes made to ensure that only one header is displayed when selecting back button from a journey planner map.
//
//   Rev 1.147   Apr 15 2005 12:48:26   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.146   Nov 03 2004 12:54:18   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.145   Sep 20 2004 16:47:10   passuied
//minor change
//
//   Rev 1.144   Sep 15 2004 09:35:42   jbroome
//IR 1549 - Find a Variety of transport journeys after using map to select location.
//
//   Rev 1.143   Sep 01 2004 10:30:08   passuied
//added code to populate tristate control with new Car SearchTypes DataSets for Car mode
//Resolution for 1441: Find A Car : Add extra station/Airport search type in location selection
//
//   Rev 1.142   Aug 19 2004 15:39:18   passuied
//Added new type in MapMode called FromFindAInput, used by FindTrunkInput and FindCarInput when calling the map. The map page and controls behave exactly as with enum MapMode.FromJourneyInput except that it shows the FindA header in the first case and the JourneyPlanner one in the latter. 
//Also checks if MapMode.FromFindAInput and if not wipe the FindAMode (CreateInstance(FindAMode.None)).
//
//   Rev 1.141   Aug 16 2004 16:06:34   RHopkins
//IR1257  Store the current "resolved" state of the location in the page's viewstate so that the User can use the browser's back button to enter a new location.
//
//   Rev 1.140   Aug 11 2004 11:03:54   jbroome
//IR 1144 - Help boxes are closed when Next button is clicked, or location resolved to ensure incorrect Help info is not displayed.
//
//   Rev 1.139   Aug 03 2004 11:55:38   COwczarek
//Use new IsFindAMode property
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.138   Aug 02 2004 18:11:48   esevern
//added check for findpagestate when refreshing location control - if in find a mode, only display limited number of location options 
//
//   Rev 1.137   Jul 23 2004 11:48:32   passuied
//Changes to add GetResource Method in TDPage and TDUserControl to ease access to resources. Also removal of local GetResouce in controls and pages
//
//   Rev 1.136   Jul 23 2004 11:33:22   CHosegood
//Adjusted drop down fix.
//
//   Rev 1.135   Jul 12 2004 14:00:38   jbroome
//InjectViewState no longer causes an error - removed comment.
//(Extend Journey code review)
//
//   Rev 1.134   Jun 04 2004 09:44:58   RPhilpott
//Ensure that default location type is picked up from DataServices, not random hard-coding.
//
//   Rev 1.133   May 26 2004 10:20:00   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.132   May 18 2004 13:27:54   jbroome
//IR861 Resolving issue of Zoom Control levels and Map Symbols.
//
//   Rev 1.131   May 05 2004 16:50:10   AWindley
//Resolution for IR688: Correct browser window title is now maintained following return from location information page.
//
//   Rev 1.130   Apr 30 2004 13:10:24   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.129   Apr 28 2004 16:51:52   ESevern
//added help labels/help text for DEL5.4 additional copy
//Resolution for 835: Additional help copy text provided by DfT for DEL5.4
//
//   Rev 1.128   Apr 16 2004 10:08:32   asinclair
//Added label for Map symbols
//
//   Rev 1.127   Apr 08 2004 14:47:54   COwczarek
//The Populate method of the TriStateLocationControl now
//accepts location type (e.g. origin, destination, via). Pass this
//value to specify the purpose of the control.
//Resolution for 695: DEL 5.2 QA Changes - Ambiguity page
//
//   Rev 1.126   Apr 07 2004 15:47:10   CHosegood
//Now displaying/removing map symbols correctly
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.125   Apr 06 2004 14:11:24   COwczarek
//Add event handler for "new location" button click
//Resolution for 682: New location button on location maps / traffic maps page performs no action
//
//   Rev 1.124   Apr 05 2004 15:56:20   PNorell
//Fixed map-zoom events to be less of them.
//Fixed IR674
//
//   Rev 1.123   Apr 05 2004 09:58:48   ESevern
//DEL5.2 QA changes - error messages put back in
//
//   Rev 1.122   Apr 01 2004 19:12:04   PNorell
//Updated for keeping the location and other things where they should after going to help page and/or location information.
//
//   Rev 1.121   Mar 31 2004 10:22:44   PNorell
//Fixed help-labels to keep the map-state.
//
//   Rev 1.120   Mar 26 2004 11:28:14   COwczarek
//Fix to return user to previous page and not journey planner input page if the back button is clicked
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location
//
//   Rev 1.119   Mar 19 2004 16:25:12   COwczarek
//Remove redundant code
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.118   Mar 18 2004 10:14:22   asinclair
//Removed error messages no longer required for Del 5.2
//
//   Rev 1.117   Mar 16 2004 09:25:30   PNorell
//Support for outward and return map added.
//
//   Rev 1.116   Mar 15 2004 19:41:34   pnorell
//Checkin with change pending answer from ESRI what is up.
//
//   Rev 1.115   Mar 15 2004 16:26:06   CHosegood
//del 5.2 map changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.114   Mar 12 2004 16:18:18   COwczarek
//Set initial scale of map for locations previously selected from map. Initialise search type for location search to be of type Map
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.113   Mar 11 2004 17:00:06   PNorell
//Updated for new map-handling.
//
//   Rev 1.112   Mar 10 2004 17:41:30   COwczarek
//Set help label id
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.111   Mar 10 2004 15:53:28   PNorell
//Updated for IR498 to be included in 5.2
//
//   Rev 1.110   Mar 10 2004 09:37:00   COwczarek
//Add comments
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.109   Mar 09 2004 10:36:28   CHosegood
//Removed refrence to JourneyMapState.Location
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.108   Mar 08 2004 17:17:10   COwczarek
//Remove handling of redundant "header click" event. Now performed in header controls.
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.107   Mar 08 2004 16:06:56   CHosegood
//Added the pageID to the mapLoctionControl
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.106   Mar 08 2004 15:27:32   CHosegood
//Removed bottom journey planner banner and replace continune button with next
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.105   Mar 04 2004 09:40:04   asinclair
//Added Help Icon/Label for Map Icons box for Del 5.2
//
//   Rev 1.104   Mar 03 2004 15:56:12   COwczarek
//Add next/back button functionality to allow navigation of hierarchic search for ambiguous locations
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.103   Mar 01 2004 15:30:56   CHosegood
//DEL5.2 Changes.  Checked in for Integration and not yet complete
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.102   Jan 21 2004 11:32:58   PNorell
//Update to 5.2
//
//   Rev 1.101   Dec 19 2003 12:24:56   passuied
//lsdfjl
//
//   Rev 1.100   Dec 19 2003 09:47:18   kcheung
//Minor/Cosmetic code review updates.
//
//   Rev 1.99   Dec 17 2003 15:24:24   kcheung
//Minor updates required to map logging changes.
//
//   Rev 1.98   Dec 16 2003 12:26:12   kcheung
//Switch AddAdditionaIconsOnMap call so that Previous View works IF map location is valid.
//
//   Rev 1.97   Dec 16 2003 10:23:18   kcheung
//Zoom level > 0 check before calling setscale.
//
//   Rev 1.96   Dec 15 2003 16:37:52   kcheung
//Alt text for back button
//
//   Rev 1.95   Dec 15 2003 15:02:28   kcheung
//Removed setting of mapToRefresh to TRUE when setscale is called.
//
//   Rev 1.94   Dec 15 2003 14:43:20   kcheung
//Tided up code
//
//   Rev 1.93   Dec 15 2003 13:25:32   passuied
//fix for little problem with location searchtype
//
//   Rev 1.92   Dec 15 2003 12:41:40   kcheung
//Del 5.1 Journey Planner Location Map Updates after link testing on dev build 22.
//
//   Rev 1.91   Dec 13 2003 14:05:44   passuied
//fix for location controls
//
//   Rev 1.90   Dec 12 2003 18:54:38   kcheung
//Del 5.1
//
//   Rev 1.89   Dec 12 2003 18:08:04   kcheung
//Del 5.1 Updates
//
//   Rev 1.88   Dec 12 2003 14:36:50   kcheung
//Journey Planner Location Map 5.1 Updates
//
//   Rev 1.87   Dec 11 2003 10:21:46   kcheung
//Journey Planner Location Map Del 5.1 Update
//
//   Rev 1.86   Dec 10 2003 11:57:46   PNorell
//Updates for DEL5.1
//
//   Rev 1.85   Dec 04 2003 13:09:48   passuied
//final version for del 5.1
//
//   Rev 1.84   Dec 01 2003 16:28:46   kcheung
//Removed footer, then re-inserted it  - Welsh correctly translate now.  Bizarre.
//Resolution for 328: Welsh button selected but some language still in English
//
//   Rev 1.83   Nov 28 2003 16:31:52   passuied
//various changes in the initialisation of the map location when using map outside JP
//Resolution for 452: Session variables problem between traffic map and Location map
//
//   Rev 1.82   Nov 27 2003 17:54:26   passuied
//changed calls to SetUpLocationSearch so it specifies if the location accepts the postcodes
//Resolution for 428: Public Via shouldn't accept postcodes
//
//   Rev 1.81   Nov 27 2003 17:25:32   asinclair
//Updated
//
//   Rev 1.80   Nov 26 2003 19:47:42   passuied
//added handler for HeaderClick event that clear the JourneyReturnInputStack when triggered.
//So back button is not displayed next time there is a click on map button in header.
//Resolution for 419: Back button displayed when it shouldn't
//
//   Rev 1.79   Nov 26 2003 16:25:30   passuied
//Refresh the map only once on the page
//Resolution for 381: The 'Previous View' Map functionality is not working
//
//   Rev 1.78   Nov 25 2003 18:25:40   COwczarek
//SCR#145: CSS style does not appear correctly in Mozilla browsers
//Resolution for 145: CSS style does not appear correctly in Mozilla browsers
//
//   Rev 1.77   Nov 18 2003 12:56:04   passuied
//comments added for code review
//
//   Rev 1.76   Nov 17 2003 16:49:16   kcheung
//Uncommented code that catches MapExceptionGeneral.
//
//   Rev 1.75   Nov 17 2003 11:22:42   passuied
//Added error message when location unspecified or ambiguous
//Resolution for 202: Map Display partial postcode
//Resolution for 226: Error messges for ambiguity on map pages
//
//   Rev 1.74   Nov 14 2003 12:55:08   kcheung
//Added zoom plus and zoom minus buttons.
//Resolution for 173: The map zoom control, comprising 13 zoom level links, is missing an indication of zoom direction
//
//   Rev 1.73   Nov 13 2003 18:03:18   passuied
//fixed Back button of JourneyLocationMap was updating journeyParameter searches and locations.
//Instead, leave it in the same state as it was before going on the map page
//Resolution for 180: Maps Page: Back button
//
//   Rev 1.72   Nov 11 2003 17:58:38   PNorell
//Updated to conform to the visuals.
//
//   Rev 1.71   Nov 07 2003 16:48:50   kcheung
//Fixed bug where "Back" was not visible if coming back from Location Information.
//
//   Rev 1.70   Nov 07 2003 13:39:08   kcheung
//Added random number at the end of road list values to ensure duplicates do not exist.
//
//   Rev 1.69   Nov 07 2003 11:29:20   PNorell
//Fixed n*mespace comment confusing NAnt.
//
//   Rev 1.68   Nov 06 2003 12:26:30   kcheung
//Fixed alternate text for map and summary map
//
//   Rev 1.67   Nov 06 2003 11:39:22   kcheung
//Fixed printing for Netscape
//
//   Rev 1.66   Nov 05 2003 13:22:22   kcheung
//Added check for null Journey Results before setting the IsValid flag
//
//   Rev 1.65   Nov 04 2003 15:37:38   kcheung
//Set the IsValid to false
//
//   Rev 1.64   Nov 04 2003 15:28:08   kcheung
//Uncommented out Initialise call to InputPageState
//
//   Rev 1.63   Nov 04 2003 13:53:54   kcheung
//Updated n*mespace to Web.Templates
//
//   Rev 1.62   Oct 31 2003 11:01:48   passuied
//Resolution of SCR 44 and 45.
//Refresh of POI on map everytime action is done on map and scale is close enough to display something.
//Resolution for 44: pois overlaid on the output map are not changed on zoom
//Resolution for 45: point of interest category changed but existing items on map are not cleared
//
//   Rev 1.61   Oct 30 2003 14:27:36   passuied
//fixed font size and added Location label
//
//   Rev 1.60   Oct 29 2003 17:13:36   passuied
//fixed pb with location ok button on map page
//
//   Rev 1.59   Oct 23 2003 12:43:04   kcheung
//Fixed so that road names are correctly read from ITN instead of ROADS.  Fixed duplicate names appearing on the drop down list.
//
//   Rev 1.58   Oct 22 2003 10:37:30   kcheung
//Fixed bug to clear the location drop down list before zoom map full is called.
//
//   Rev 1.57   Oct 21 2003 16:18:54   kcheung
//Updates applied after updates to FXCOP changes in MapToolsControl
//
//   Rev 1.56   Oct 21 2003 15:44:50   passuied
//corrected naptan problem.
//
//   Rev 1.55   Oct 21 2003 14:18:10   passuied
//changes after fxcop
//
//   Rev 1.54   Oct 20 2003 17:55:04   passuied
//renamed control names with pascal case
//
//   Rev 1.53   Oct 15 2003 16:59:34   kcheung
//Fixed so that GisQuery is returned from ServiceDiscovery
//
//   Rev 1.52   Oct 15 2003 11:48:30   passuied
//fixed pb when valid location selected by map and go back to map (location was unspecified so the map of that location was not displayed)
//
//   Rev 1.51   Oct 13 2003 18:07:44   kcheung
//Fixes
//
//   Rev 1.50   Oct 13 2003 17:37:38   kcheung
//Wrapped UpdateMapPoints into if so that it is not called multiple times
//
//   Rev 1.49   Oct 10 2003 16:24:36   passuied
//pageTitle from Resources, PrinterFormat AlternateText
//
//   Rev 1.48   Oct 10 2003 09:27:08   kcheung
//Fixed bug to ensure start/end/via points are correctly shown on the map with descriptions next to them.
//
//   Rev 1.47   Oct 09 2003 18:29:58   kcheung
//Updated to ensure that Map does not display any initial icons and the Map ServerName and ServiceName properties are loaded from Properties service.
//
//   Rev 1.46   Oct 09 2003 14:17:42   kcheung
//Removed redundant i variable
//
//   Rev 1.45   Oct 09 2003 13:52:28   kcheung
//Added code to push page id onto stack before transferring to the the location information.
//
//   Rev 1.44   Oct 09 2003 12:58:04   kcheung
//Added line to push page id onto stack before transferring to location information page.
//
//   Rev 1.43   Oct 09 2003 10:45:04   passuied
//corrected bug with publicVia
//
//   Rev 1.42   Oct 08 2003 18:11:30   kcheung
//Added SeperatorVisible property which determines if the blue line seperator is visible or not on the Map Tools Control.
//
//   Rev 1.41   Oct 08 2003 13:13:02   kcheung
//Added Map Exception handling code.
//
//   Rev 1.40   Oct 07 2003 16:52:20   passuied
//corrected so we can select travel from and then travel to either from map or locationService
//
//   Rev 1.39   Oct 07 2003 16:15:58   kcheung
//Added check to see if necessary to update the map because a location has now become valid.
//
//   Rev 1.38   Oct 07 2003 15:30:24   kcheung
//Fixed display and population of Alternate Locations
//
//   Rev 1.37   Oct 07 2003 14:08:06   kcheung
//Updated to corrently redirect when finding journeys
//
//   Rev 1.36   Oct 06 2003 17:34:20   kcheung
//Removed naptan null reference bug
//
//   Rev 1.35   Oct 06 2003 17:21:00   kcheung
//Removed null reference exception bug in CreateLocation()
//
//   Rev 1.34   Oct 06 2003 15:54:34   kcheung
//Fixed MapToolsControl and MapToolsAcceptedDataControl. JourneyPlannerLocationMap now working properly.
//
//   Rev 1.33   Oct 03 2003 16:59:36   passuied
//session reference bug fixed
//
//   Rev 1.32   Oct 03 2003 16:51:00   kcheung
//Drop down list now populates after selection.
//
//   Rev 1.31   Oct 03 2003 14:53:12   kcheung
//Removed redundant zooming code and oveview map working
//
//   Rev 1.30   Oct 03 2003 13:18:48   kcheung
//Location Icon Selection working
//
//   Rev 1.29   Oct 02 2003 18:11:26   passuied
//added strings for printablemapinput
//
//   Rev 1.28   Oct 02 2003 17:00:34   kcheung
//Updated to write selected map name, url, overview url and scale to session for printer friendly maps.
//
//   Rev 1.27   Oct 02 2003 14:40:36   kcheung
//Updated to get working with del 5 build 2 dll
//
//   Rev 1.26   Oct 02 2003 10:10:56   kcheung
//Updated for Richards map redirect stuff

#region Using Statements

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.Web.Support;

using Logger = System.Diagnostics.Trace;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.Common.DatabaseInfrastructure.Content;


#endregion

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Input Map page.
	/// </summary>
	public partial class JourneyPlannerLocationMap : TDPage
	{
		#region Images, labels, panels, hyperlinks

		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyPageButton;
        protected System.Web.UI.WebControls.Literal literalSpaceAfter;


		#endregion

		#region Custom web user controls

        protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;


        protected TransportDirect.UserPortal.Web.Controls.MapControl theMapControl;
        protected TransportDirect.UserPortal.Web.Controls.MapDisabledControl disabledMapControl;
        protected TransportDirect.UserPortal.Web.Controls.MapLocationControl mapLocationControl;
        protected TransportDirect.UserPortal.Web.Controls.MapLocationIconsSelectControl mapLocationIconsSelectControl;
        protected TransportDirect.UserPortal.Web.Controls.MapZoomControl theMapZoomControl;

        protected TransportDirect.UserPortal.Web.Controls.TriStateLocationControl2 triStateLocationControl1;


		#endregion

		#region Page variables (stored in internal viewstate)

		// Indicates if the session data for input has been resetted.
		private bool resetted = false;
		
		// Indicates if the location was initially valid.
		// (As the map will have to be refreshed if Map Location was
		// initially invalid and consequently becomes valid).
		private bool locationNotValid = false;

		// Indicates if the map should be refresh at the end of every processing.
		private bool mapToRefresh = false;

		#endregion

		#region Location Search Variables

		// Declaration of search/location object members
		private LocationSearch mapSearch;
		private TDLocation mapLocation;
		private LocationSelectControlType locationControlType;

		#endregion

		#region Constant declarations
		private const string RES_ALT_SUMMAP = "JourneyMapControl.imageSummaryMap.AlternateText";
		private const string RES_URL_BACK = "JourneyPlanner.imageButtonBack.ImageUrl";
		private const string RES_ALT_BACK = "JourneyPlannerLocationMap.imageButtonBack.AlternateText";
		
		private const string RES_URL_NEXT = "JourneyPlannerLocationMap.buttonTopNext.ImageUrl";
		private const string RES_ALT_NEXT = "JourneyPlannerLocationMap.buttonTopNext.AlternateText";
		
		private const string RES_TEXT_LABELPLANJOURNEY = "JourneyPlannerLocationMap.labelPlanJourney.Text";
		private const string RES_TEXT_OR = "JourneyPlannerLocationMap.labelOr.Text";

		private const string RES_TEXT_LABELMAP = "MapToolsControl.labelMap.Text";
		private const string RES_TEXT_LABELMAP_START = "MapToolsControl.labelMap.Start.Text";
		private const string RES_TEXT_LABELMAP_END = "MapToolsControl.labelMap.End.Text";

		private const string RES_TEXT_LABELMAP_VIA = "MapToolsControl.labelMap.Via.Text";
		
		private const string LABEL_START = "MapSelectionControl.labelStart.Text";
		private const string LABEL_END = "MapSelectionControl.labelEnd.Text";
		private const string LABEL_VIA = "MapSelectionControl.labelVia.Text";


		#endregion

		#region Constructor

		/// <summary>
		/// Constructor, sets the PageId and calls base.
		/// </summary>
		public JourneyPlannerLocationMap() : base()
		{
			pageId = PageId.JourneyPlannerLocationMap;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Exposes the Print button control that links to a printer-friendly page.
		/// The Page ID of the printer-friendly page is the current page ID prefixed with "Printable".
		/// If no printer-friendly page is available then the Print button will not be shown.
		/// </summary>
		public TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl PrinterFriendlyPageButton
		{
			get { return printerFriendlyPageButton; }
			set { printerFriendlyPageButton = value; }
		}
		#endregion

		#region Private Methods

		/// <summary>
		/// Loads variables from the session.
		/// </summary>
		private void LoadSessionVariables()
		{
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			mapSearch = pageState.MapLocationSearch;
			mapLocation = pageState.MapLocation;
			locationControlType = pageState.MapLocationControlType;
		}

		/// <summary>
		/// Loads images, strings, etc from resource file.
		/// </summary>
		private void LoadResources()
		{
			PageTitle = GetResource("JourneyPlannerLocationMap.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            labelFindPageTitle.Text = GetResource("HomeFindAPlace.lblFindAPlace");
            
			imageSummaryMap.AlternateText = GetResource( RES_ALT_SUMMAP ); 

			// Determine if the Back button should be visible
			buttonTopBack.Visible = TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count != 0;



			// label overview map
			labelOverviewMap.Text = Global.tdResourceManager.GetString
				("JourneyPlannerLocationMap.labelOverviewMap",TDCultureInfo.CurrentUICulture);

			// Initialise buttons
            buttonTopBack.Text = resourceManager.GetString("JourneyPlannerLocationMap.buttonTopBack.Text");
			buttonTopNext.Text = GetResource("JourneyPlannerLocationMap.buttonTopNext.Text");
			previousLocationButton.Text = GetResource("AmbiguousLocationSelectControl2.backButton.Text");
			resolveLocationButton.Text = GetResource("AmbiguousLocationSelectControl2.nextButton.Text");

            //CCN 0427 Set the text for map symbols label
            labelMapSymbols.Text = GetResource("panelMapLocationSelect.labelMapSymbols");

            //CCN 0427 Set the text for the new search button
            newSearchButton.Text = GetResource("JourneyPlannerLocationMap.newSearchButton.Text");

            // Set the disclaimer text (should only be set up for a white label partner)
            labelMapSymbolsDisclaimer.Text = GetResource("panelMapLocationSelect.labelMapSymbolsDisclaimer");

			// Set the text next to the Select radio button depending on what mode it is
			InputPageState pageState = TDSessionManager.Current.InputPageState;
			bool fromJourneyInput = pageState.MapMode == CurrentMapMode.FromJourneyInput;
			bool fromFindAInput = pageState.MapMode == CurrentMapMode.FromFindAInput;
			bool fromTravelBoth = pageState.MapMode == CurrentMapMode.TravelBoth;

			CurrentMapMode mode = pageState.MapMode;

			if( mode == CurrentMapMode.FromJourneyInput
				|| mode == CurrentMapMode.FromFindAInput)
			{
				// Build the text
				string locationType = String.Empty;
				
				if( pageState.MapType == CurrentLocationType.From )
					locationType = GetResource(LABEL_START);
				else if (pageState.MapType == CurrentLocationType.To )
					locationType = GetResource(LABEL_END);
				else if (pageState.MapType == CurrentLocationType.PrivateVia ||
                    pageState.MapType == CurrentLocationType.PublicVia ||
                    pageState.MapType == CurrentLocationType.CycleVia)
					locationType = GetResource(LABEL_VIA);
			}

			if( (fromJourneyInput || fromFindAInput) && pageState.MapType == CurrentLocationType.From )
			{
				labelMap.Text = GetResource( RES_TEXT_LABELMAP_START );
			}
			else if( (fromJourneyInput || fromFindAInput) && pageState.MapType == CurrentLocationType.To )
			{
				labelMap.Text = GetResource( RES_TEXT_LABELMAP_END );
				
			}
			else if( (fromJourneyInput || fromFindAInput) && ( pageState.MapType == CurrentLocationType.PrivateVia || pageState.MapType == CurrentLocationType.PublicVia || pageState.MapType == CurrentLocationType.CycleVia) )
			{
				labelMap.Text = GetResource( RES_TEXT_LABELMAP_VIA );
			}
			else
			{
				labelMap.Text = GetResource( RES_TEXT_LABELMAP );
			}
		}

		/// <summary>
		/// Sets the initial zoom level of the map.
		/// </summary>
		private void ZoomMapInitial()
		{
			// Find the "relevant location" to determine the initial zoom level.
			TDLocation location = GetLocation;
			
			InputPageState pageState = TDSessionManager.Current.InputPageState;
			TDJourneyParameters parameters = TDSessionManager.Current.JourneyParameters;


			if( location != null && location.Status == TDLocationStatus.Valid )
			{

				locationNotValid = false;

				string scaleString = String.Empty;

				// Now, we have the location, determine what the initial zoom level is.
				switch(pageState.MapLocationSearch.SearchType)
				{
					case SearchType.AddressPostCode :
                    case SearchType.Map :
						
						// Get scale to set from the Properties Service
						scaleString = Properties.Current["GazetteerDefaultScale.AddressPostCode"];

						break;

					case SearchType.AllStationStops :

						// Get scale to set from the Properties Service
						scaleString = Properties.Current["GazetteerDefaultScale.AllStations"];
						break;

					case SearchType.Locality :
						
						// Get scale to set from the Properties Service
						scaleString = Properties.Current["GazetteerDefaultScale.Locality"];
						break;

					case SearchType.MainStationAirport :
						
						// Get scale to set from the Properties Service
						scaleString = Properties.Current["GazetteerDefaultScale.MajorStations"];
						break;

					case SearchType.POI :
						
						// Get scale to set from the Properties Service
						scaleString = Properties.Current["GazetteerDefaultScale.AttractionsFacilities"];
						break;
				}

				int scaleToSet =
					Convert.ToInt32(scaleString, TDCultureInfo.CurrentCulture.NumberFormat);

				try
				{
					if (location.PartPostcode == true) 
					{	// Zoom to the area of the partial postcode
						theMapControl.Map.ZoomToEnvelope(
							location.PartPostcodeMinX, 
							location.PartPostcodeMinY, 
							location.PartPostcodeMaxX, 
							location.PartPostcodeMaxY );
					}
					else
					{
						// Zoom the map.
						theMapControl.Map.ZoomToPoint(
							location.GridReference.Easting,
							location.GridReference.Northing,
							scaleToSet);
					}
					// We zoomed now, so there is no need to redo the zoom/refresh later unless something else changes the layout
					this.mapToRefresh = false;
				}
				catch(PropertiesNotSetException pnse)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

					Logger.Write(operationalEvent);
				}
				catch(MapNotStartedException mnse)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);

					Logger.Write(operationalEvent);
				}
				catch(ScaleOutOfRangeException soore)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

					Logger.Write(operationalEvent);
				}
				catch(ScaleZeroOrNegativeException szone)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

					Logger.Write(operationalEvent);
				}
				catch(NoPreviousExtentException npee)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);
				
					Logger.Write(operationalEvent);
				}
				catch(EnvelopeZeroOrNegativeException ezone)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + ezone.Message);

					Logger.Write(operationalEvent);
				}
				catch(EnvelopeOutOfRangeException eoore)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + eoore.Message);

					Logger.Write(operationalEvent);
				}
				catch(MapExceptionGeneral mge)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mge.Message);

					Logger.Write(operationalEvent);
				}
    		}
		}

		/// <summary>
		/// Get property - Returns the TD Map Location currently in the session
		/// </summary>
		private TDLocation GetLocation
		{	
			get
			{
				TDLocation location = null;

				if(TDSessionManager.Current.InputPageState.MapLocation != null)
					location = TDSessionManager.Current.InputPageState.MapLocation;
		
				return location;
			}
		}

		/// <summary>
		/// Button handler to handle the event fired by the OK button in the
		/// Icons Select control
		/// </summary>
		private void IconsSelectRefresh()
		{
			// Get all the selected hashtable codes for Stops
			ArrayList selectedStopKeys =
				mapLocationIconsSelectControl.GetSelectedStopKeys();

			try
			{
				if( !theMapControl.Map.IsStarted() )
				{
					ZoomMapInitial();
				}
				// Create a new StopsVisible Hashtable
				Hashtable stopsVisible = new Hashtable();

				bool changed = false;
				// Iterate through all available keys and set boolean accordindly
				foreach( string stopkey in theMapControl.Map.StopsVisible.Keys )
				{
					if( (theMapControl.Map.StopsVisible[stopkey] != null && (bool)theMapControl.Map.StopsVisible[stopkey]) != selectedStopKeys.Contains(stopkey) )
					{
						changed = true;
						stopsVisible.Add(stopkey, selectedStopKeys.Contains(stopkey));
					}
				}

				if( changed )
				{
					// Reset and update the "Stops Visible" hashtable
					theMapControl.Map.StopsVisible.Clear();
					theMapControl.Map.StopsVisible = stopsVisible;
				}

				if(FindCarParkHelper.CarParkingAvailable)
				{
					// if the visibility has changed, re-toggle the car park layer
					if( theMapControl.Map.CarParkLayerVisible != mapLocationIconsSelectControl.CarParksSelected)
					{
						theMapControl.Map.ToggleLayerVisibility(theMapControl.Map.CarParkLayerIndex);
					}
				}
				else
				{
					// ensure the car park icons is never displayed
					if( theMapControl.Map.CarParkLayerVisible == true)
					{
						theMapControl.Map.ToggleLayerVisibility(theMapControl.Map.CarParkLayerIndex);
					}
				}

				//Get all the selected hashtable codes for PointX
				ArrayList selectedPointXKeys =
					mapLocationIconsSelectControl.GetSelectedPointXKeys();

				// Create a new PointX Hashtable
				Hashtable pointXVisible = new Hashtable();

				// Reset changed
				changed = false;

				// Iternate through all available keys and set boolean accordingly.
				foreach( string pointXKey in theMapControl.Map.PointXVisible.Keys )
				{
                    if( ( theMapControl.Map.PointXVisible[pointXKey] != null )
                        && ( (bool)theMapControl.Map.PointXVisible[pointXKey] != selectedPointXKeys.Contains(pointXKey) ) )
                    {
                        changed = true;
                        pointXVisible.Add(pointXKey, selectedPointXKeys.Contains(pointXKey));
                    }
				}

				if( changed )
				{
					// Reset and update the "PointX" hashtable
					theMapControl.Map.PointXVisible.Clear();
					theMapControl.Map.PointXVisible = pointXVisible;
				}

				if(FindCarParkHelper.CarParkingAvailable)
				{
					// Refresh the map
					if ( mapLocationIconsSelectControl.IsEnabled && (theMapControl.Map.HasPointXChanged
						|| theMapControl.Map.HasStopsChanged 
						|| theMapControl.Map.HasCarParkChanged)	)
					{
						mapToRefresh = true;
					}
				}
				else 
				{
					// Refresh the map
					if ( mapLocationIconsSelectControl.IsEnabled && (theMapControl.Map.HasPointXChanged
						|| theMapControl.Map.HasStopsChanged)	)
					{
						mapToRefresh = true;
					}
				}
			}
			
			catch(PropertiesNotSetException pnse)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

				Logger.Write(operationalEvent);
			}
			
			catch(MapNotStartedException mnse)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);

				Logger.Write(operationalEvent);
			}
			
			catch(MapExceptionGeneral meg)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);

				Logger.Write(operationalEvent);
			}

			// Update iconSelection for printer friendly maps
			bool [][] iconSelection = TDSessionManager.Current.InputPageState.IconSelectionOutward;	
			mapLocationIconsSelectControl.UpdateIconSelection(ref iconSelection);
		}

        /// <summary>
        /// Refreshes the contents of the location selection control and other controls
        /// on the page that are dependant on the ambiguity resolution of the location
        /// specified.
        /// </summary>
        /// <param name="checkInput">True if form input values should be compared with current session 
        /// state values, updating the session state values if different</param>
        private void RefreshLocationControl(bool checkInput)
		{
            bool locationValid = TDSessionManager.Current.InputPageState.MapLocation.Status == TDLocationStatus.Valid;

            // Set visibility of various control depending on whether
            // location search has been resolved
            panelLocationSelect.Visible = !locationValid;
            labelInstructions.Visible = !locationValid;
            panelMapTools.Visible = locationValid;
            theMapControl.Visible = locationValid;
            //labelZoomLevel.Visible = locationValid;
            //hyperLinkMapKey.Visible = locationValid;
            mapLocationIconsSelectControl.Visible = locationValid;
            disabledMapControl.Visible = !locationValid;
            panelMapLocationSelect.Visible = locationValid;
			labelOverviewMap.Visible = locationValid;
			labelMapSymbols.Visible = locationValid;
			printerFriendlyPageButton.Visible = locationValid;

            // CCN 427 White label changes when map dispalys the location box and titles shouldn't display
            findaMapResults.Visible = locationValid;
            findaMapRow.Visible = !locationValid;
			
			InputPageState pageState = TDSessionManager.Current.InputPageState;
            previousLocationButton.Visible = pageState.JourneyInputReturnStack.Count == 0 && mapSearch.CurrentLevel > 0;

            commandBack.Text = resourceManager.GetString("JourneyPlannerLocationMap.buttonTopBack.Text");
            commandBack.Visible = pageState.MapLocation.Status == TDLocationStatus.Ambiguous;

			if (locationValid)
			{
				labelSelectedLocation.Text = TDSessionManager.Current.InputPageState.MapLocation.Description;

				// set alt text and help text
				if( ((pageState.MapMode == CurrentMapMode.FromJourneyInput) || (pageState.MapMode == CurrentMapMode.FromFindAInput))
					&& pageState.MapType == CurrentLocationType.From )
				{
					//set help label for start
					HelpControlLocation.AlternateText = GetResource("Start.AlternateText");
                    HelpControlLocation2.AlternateText = GetResource("Start.AlternateText");
					
				}
				else if( (pageState.MapMode == CurrentMapMode.FromJourneyInput || pageState.MapMode == CurrentMapMode.FromFindAInput) 
					&& pageState.MapType == CurrentLocationType.To )
				{
					//set help label for destination
					HelpControlLocation.AlternateText = GetResource("Destination.AlternateText");
                    HelpControlLocation2.AlternateText = GetResource("Destination.AlternateText");
					
	
				}
				else if( (pageState.MapMode == CurrentMapMode.FromJourneyInput || pageState.MapMode == CurrentMapMode.FromFindAInput) 
					&& ( pageState.MapType == CurrentLocationType.PrivateVia || pageState.MapType == CurrentLocationType.CycleVia) ) 
				{
					//set help label for via
					HelpControlLocation.AlternateText = GetResource("Via.AlternateText");
                    HelpControlLocation2.AlternateText = GetResource("Via.AlternateText");
                    
				} 
				else 
				{
					HelpControlLocation.AlternateText = GetResource("helpControlLocationSelect.AlternateText");
                    HelpControlLocation2.AlternateText = GetResource("helpControlLocationSelect.AlternateText");
                    
				}
            }
			else
			{

				// Populates the TriState with Possible searchTypes of the location the map has been clicked from.
				DataServices.DataServiceType searchTypes = DataServices.DataServiceType.LocationTypeDrop;
				switch (pageState.MapType)
				{
					case CurrentLocationType.From:
						searchTypes = DataServices.DataServiceType.FromToDrop;
						break;
					case CurrentLocationType.To:
						searchTypes = DataServices.DataServiceType.FromToDrop;
						break;
					case CurrentLocationType.PrivateVia:
						searchTypes = DataServices.DataServiceType.CarViaDrop;
						break;
					case CurrentLocationType.PublicVia:
						searchTypes = DataServices.DataServiceType.PTViaDrop;
						break;
                    case CurrentLocationType.CycleVia:
                        searchTypes = DataServices.DataServiceType.CycleViaLocationDrop;
                        break;
					case CurrentLocationType.Alternate1:
						searchTypes = DataServices.DataServiceType.AltFromToDrop;
						break;
					case CurrentLocationType.Alternate2:
						searchTypes = DataServices.DataServiceType.AltFromToDrop;
						break;
				}

				bool acceptsPostcode;
				if (pageState.MapType == CurrentLocationType.PublicVia)
					acceptsPostcode = false;
				else 
					acceptsPostcode = true;

				// need to populate tri state location control depending on if we're in Find A mode
				// or not (in Find A mode, have reduced number of location options).  
				// Check session for FindPageState - if its not null, we're in Find A mode
				if(TDSessionManager.Current.IsFindAMode)
				{
					switch (TDSessionManager.Current.FindAMode)
					{
						// trunk should accept partial but not whole postcode
						case FindAMode.TrunkStation :
						case FindAMode.Trunk :
							triStateLocationControl1.Populate(DataServiceType.FindStationLocationDrop, 
								pageState.MapType, ref mapSearch, ref mapLocation, ref locationControlType, 
								true, true, true, checkInput);
							break;

						case FindAMode.Car :
							triStateLocationControl1.Populate(DataServiceType.FindCarLocationDrop, 
								pageState.MapType, ref mapSearch, ref mapLocation, ref locationControlType, 
								true, true, true, checkInput);
							break;

                        case FindAMode.Cycle:
                            triStateLocationControl1.Populate(DataServiceType.FindCycleLocationDrop,
                                pageState.MapType, ref mapSearch, ref mapLocation, ref locationControlType,
                                true, true, true, checkInput);
                            break;

						//default (all other find a pages)
						default:

							triStateLocationControl1.Populate(DataServiceType.FindStationLocationDrop, 
								pageState.MapType, ref mapSearch, ref mapLocation, ref locationControlType, 
								true, true, true, checkInput);
							break;
					}
				}
				else {

					triStateLocationControl1.Populate(
					searchTypes,
                    pageState.MapType,
					ref mapSearch,
					ref mapLocation,
					ref locationControlType,
                    true,
					acceptsPostcode,
					acceptsPostcode, // Same rules apply for Partial Postcode in this case	
					checkInput
					);
				}

				labelInstructions.Visible = true;
				labelInstructions.CssClass = "txtsevenb";

				// help label, alt text and map tool help should be set specific to current context 
				// (start, destination, via or generic)
				if( (pageState.MapMode == CurrentMapMode.FromJourneyInput || pageState.MapMode == CurrentMapMode.FromFindAInput) 
						&& pageState.MapType == CurrentLocationType.From )
				{
					//set help label for start
					HelpControlLocation.AlternateText = GetResource("Start.AlternateText");
                    HelpControlLocation2.AlternateText = GetResource("Start.AlternateText");

					if(pageState.MapLocation.Status == TDLocationStatus.Unspecified)
					{
						HelpControlLocation.HelpLabel = "helpLabelStartLocation";
                        HelpControlLocation2.HelpLabel = "helpLabelStartLocation";
                        HelpControlLocation.HelpLabelControl = helpLabelStartLocation;
                        HelpControlLocation2.HelpLabelControl = helpLabelStartLocation;
					}
					if(pageState.MapLocation.Status == TDLocationStatus.Ambiguous)
					{
						HelpControlLocation.HelpLabel = "helpLabelStartAmbiguity";
                        HelpControlLocation2.HelpLabel = "helpLabelStartAmbiguity";
                        HelpControlLocation.HelpLabelControl = helpLabelStartAmbiguity;
                        HelpControlLocation2.HelpLabelControl = helpLabelStartAmbiguity;
					}
                    
				}
				else if( (pageState.MapMode == CurrentMapMode.FromJourneyInput || pageState.MapMode == CurrentMapMode.FromFindAInput) 
							&& pageState.MapType == CurrentLocationType.To )
				{
					//set help label for destination
					HelpControlLocation.AlternateText = GetResource("Destination.AlternateText");
                    HelpControlLocation2.AlternateText = GetResource("Destination.AlternateText");

					if(pageState.MapLocation.Status == TDLocationStatus.Unspecified)
					{
						HelpControlLocation.HelpLabel = "helpLabelDestination";
                        HelpControlLocation2.HelpLabel = "helpLabelDestination";
                        HelpControlLocation.HelpLabelControl = helpLabelDestination;
                        HelpControlLocation2.HelpLabelControl = helpLabelDestination;
		
					}
					if(pageState.MapLocation.Status == TDLocationStatus.Ambiguous)
					{
						HelpControlLocation.HelpLabel = "helpLabelDestinationAmbiguity";
                        HelpControlLocation2.HelpLabel = "helpLabelDestinationAmbiguity";
                        HelpControlLocation.HelpLabelControl = helpLabelDestinationAmbiguity;
                        HelpControlLocation2.HelpLabelControl = helpLabelDestinationAmbiguity;
					}

                   
	
				}
				else if( (pageState.MapMode == CurrentMapMode.FromJourneyInput || pageState.MapMode == CurrentMapMode.FromFindAInput) 
							&& ( pageState.MapType == CurrentLocationType.PrivateVia || pageState.MapType == CurrentLocationType.CycleVia ) ) 
				{
					//set help label for via
					HelpControlLocation.AlternateText = GetResource("Via.AlternateText");
                    HelpControlLocation2.AlternateText = GetResource("Via.AlternateText");

					if(pageState.MapLocation.Status == TDLocationStatus.Unspecified)
					{
						HelpControlLocation.HelpLabel = "helpLabelVia";
                        HelpControlLocation2.HelpLabel = "helpLabelVia";
                        HelpControlLocation.HelpLabelControl = helpLabelVia ;
                        HelpControlLocation2.HelpLabelControl = helpLabelVia;
					}

					if(pageState.MapLocation.Status == TDLocationStatus.Ambiguous)
					{
						HelpControlLocation.HelpLabel = "helpLabelViaAmbiguity";
                        HelpControlLocation2.HelpLabel = "helpLabelViaAmbiguity";
                        HelpControlLocation.HelpLabelControl = helpLabelViaAmbiguity;
                        HelpControlLocation2.HelpLabelControl = helpLabelViaAmbiguity;
					}
                   
				} 
				else 
				{
					HelpControlLocation.AlternateText = GetResource("helpControlLocationSelect.AlternateText");
                    HelpControlLocation2.AlternateText = GetResource("helpControlLocationSelect.AlternateText");
					
					if(pageState.MapLocation.Status == TDLocationStatus.Unspecified)
					{
						HelpControlLocation.HelpLabel = "helpLabelLocations";
                        HelpControlLocation2.HelpLabel = "helpLabelMapTools";
                        HelpControlLocation.HelpLabelControl = helpLabelLocations;
                        HelpControlLocation2.HelpLabelControl = helpLabelMapTools;
					}

					if(pageState.MapLocation.Status == TDLocationStatus.Ambiguous)
					{
						HelpControlLocation.HelpLabel = "helpLabelLocationsAmbig";
                        HelpControlLocation2.HelpLabel = "helpLabelMapTools";
                        HelpControlLocation.HelpLabelControl = helpLabelLocationsAmbig;
                        HelpControlLocation2.HelpLabelControl = helpLabelMapTools;
					}

				 }


				// error message
				if (pageState.MapLocation.Status == TDLocationStatus.Unspecified 
					&& pageState.MapLocationSearch.InputText.Length == 0)
				{
					labelInstructions.Visible = true;
					labelInstructions.CssClass = "txtsevenb";
					labelInstructions.Text = Global.tdResourceManager.GetString(
						"JourneyPlannerLocationMap.ErrorMessage.Prompt",
						TDCultureInfo.CurrentUICulture);
				}
					// if location unspecified and user has typed something
				else if (pageState.MapLocation.Status == TDLocationStatus.Unspecified 
					&& pageState.MapLocationSearch.InputText.Length != 0)
				{
					labelInstructions.Visible = true;
					labelInstructions.CssClass = "txtsevenb";
					labelInstructions.Text = Global.tdResourceManager.GetString(
						"JourneyPlannerLocationMap.ErrorMessage.Prompt",
						TDCultureInfo.CurrentUICulture);
				}
				else if (pageState.MapLocation.Status == TDLocationStatus.Ambiguous) // else if ambiguous
				{
					
					labelInstructions.Visible = true;
					labelInstructions.CssClass = "txtsevenb";
					labelInstructions.Text = Global.tdResourceManager.GetString(
						"JourneyPlannerLocationMap.ErrorMessage.Ambiguous",
						TDCultureInfo.CurrentUICulture);					
				}
			}
		}

		/// <summary>
		/// Method to add additional icons on to the map.
		/// This only adds the white location circle
		/// </summary>
		private void AddAdditionalIconsOnMap()
		{
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			int x = pageState.MapLocation.GridReference.Easting;
			int y = pageState.MapLocation.GridReference.Northing;
			string name = pageState.MapLocation.Description;
		
			// Clear any current locations on the map.
			theMapControl.Map.ClearAddedSymbols();

			// Clear Start/End/Via points.
			theMapControl.Map.ClearStartEndPoints();

			// Strip out any sub strings (read from properties DB) denoting pseudo locations 
			IPropertyProvider properties = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
			
			string railPostFix = properties[ "Gazetteerpostfix.rail" ];
			string coachPostFix = properties[ "Gazetteerpostfix.coach" ];
			string railcoachPostFix = properties[ "Gazetteerpostfix.railcoach" ];

			System.Text.StringBuilder strName = new System.Text.StringBuilder(name);
			strName.Replace(railPostFix, "");
			strName.Replace(coachPostFix, "");
			strName.Replace(railcoachPostFix, "");
			name = strName.ToString();

			// Highlight the point on the map
			theMapControl.Map.AddSymbolPoint((double)x, (double)y, "CIRCLE", name);

			// Set mapToRefresh to TRUE so that the map will be refreshed.
			mapToRefresh = true;
		}

		/// <summary>
		/// Updates the From Location of the Journey Parameters with the selected location.
		/// </summary>
		private void UpdateFromLocation()
		{
			TDJourneyParameters parameters = TDSessionManager.Current.JourneyParameters;
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			// If its a car park selected, call SetUpCarParkPlanning which adds the 
			// CarPark object to InputPageState.MapLocation
			if(TDSessionManager.Current.InputPageState.CarParkReference != string.Empty)
			{
				mapLocationControl.SetupCarParkPlanning(true);
			}

			parameters.Origin = pageState.MapLocationSearch;
			parameters.OriginLocation = pageState.MapLocation;
			
			// Check to see what the map mode is. If the map mode is not
			// FromJourneyPlannerInput, then the map mode must be updated.
			CurrentMapMode mapMode = pageState.MapMode;

			// Update the input text in the location search
			parameters.Origin.InputText = parameters.OriginLocation.Description;

			if (mapMode == CurrentMapMode.FromFindAInput && 
				(TDSessionManager.Current.FindAMode != FindAMode.Bus)) 
				parameters.DestinationLocation.NaPTANs = new TDNaptan[0];

			if( mapMode != CurrentMapMode.FromJourneyInput && mapMode != CurrentMapMode.FromFindAInput)
			{
				// Check to see if Destination is valid
				if(parameters.DestinationLocation.Status == TDLocationStatus.Valid)
				{
					pageState.MapMode = CurrentMapMode.FindJourneys;
				}
				else
				{
					pageState.MapMode = CurrentMapMode.TravelTo;
					pageState.MapLocationSearch = new LocationSearch();
					pageState.MapLocation = new TDLocation();
					pageState.MapLocationControlType.Type = ControlType.Default;
					LoadSessionVariables();
					triStateLocationControl1.Reset();
					RefreshLocationControl(Page.IsPostBack);
				}
			}

		}

		/// <summary>
		/// Updates the To Location of the Journey Parameters with the selected location.
		/// </summary>
		private void UpdateToLocation()
		{
			TDJourneyParameters parameters = TDSessionManager.Current.JourneyParameters;
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			// If its a car park selected, call SetUpCarParkPlanning which adds the 
			// CarPark object to InputPageState.MapLocation
			if(TDSessionManager.Current.InputPageState.CarParkReference != string.Empty)
			{
				mapLocationControl.SetupCarParkPlanning(false);
			}

			parameters.Destination = pageState.MapLocationSearch;
			// Get the location from InputPageState
			parameters.DestinationLocation = pageState.MapLocation;

			// Update the input text in the location search
			parameters.Destination.InputText = parameters.DestinationLocation.Description;

			// Check to see what the map mode is. If the map mode is not
			// FromJourneyPlannerInput, then the map mode must be updated.
			CurrentMapMode mapMode = pageState.MapMode;

			// we don't want to do this if its a find a bus as we need the Destination location naptan
			if (mapMode == CurrentMapMode.FromFindAInput && 
				(TDSessionManager.Current.FindAMode != FindAMode.Bus)) 
				parameters.DestinationLocation.NaPTANs = new TDNaptan[0];

			if( mapMode != CurrentMapMode.FromJourneyInput && mapMode != CurrentMapMode.FromFindAInput)
			{
				// Check to see if Destination is valid
				if(parameters.OriginLocation.Status == TDLocationStatus.Valid)
				{
					pageState.MapMode = CurrentMapMode.FindJourneys;
				}
				else
				{
					pageState.MapMode = CurrentMapMode.TravelFrom;
					pageState.MapLocationSearch = new LocationSearch();
					pageState.MapLocation = new TDLocation();
					pageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.Default;
					LoadSessionVariables();
					triStateLocationControl1.Reset();
					RefreshLocationControl(Page.IsPostBack);
				}
			}
		}

		/// <summary>
		/// Updates the Via Location in TDJourneyParameters
		/// </summary>
		private void UpdateViaLocation()
		{	
			// This only applies to Multi Modal journeys

			TDJourneyParametersMulti parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
			if (parameters == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("JourneyPlannerLocationMap page requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}

			InputPageState pageState = TDSessionManager.Current.InputPageState;

			// Create the location
			if (pageState.MapType == CurrentLocationType.PrivateVia)
			{
				parameters.PrivateVia = pageState.MapLocationSearch;
				parameters.PrivateViaLocation = TDSessionManager.Current.InputPageState.MapLocation;
				parameters.PrivateVia.InputText = parameters.PrivateViaLocation.Description;
			}
            else if (pageState.MapType == CurrentLocationType.CycleVia)
            {
                parameters.CycleVia = pageState.MapLocationSearch;
                parameters.CycleViaLocation = TDSessionManager.Current.InputPageState.MapLocation;
                parameters.CycleVia.InputText = parameters.CycleViaLocation.Description;
            }
			else
			{
				parameters.PublicVia = pageState.MapLocationSearch;
				parameters.PublicViaLocation = TDSessionManager.Current.InputPageState.MapLocation; 
				parameters.PublicVia.InputText = parameters.PublicViaLocation.Description;
			}

			if (pageState.MapMode == CurrentMapMode.FromFindAInput)
				parameters.DestinationLocation.NaPTANs = new TDNaptan[0];

		}

		/// <summary>
		/// Updates the VisitPlannerOrigin Location Journey Parameters with the selected location.
		/// </summary>
		private void UpdateVisitOriginLocation()
		{
			//private TDJourneyParametersVisitPlan parameters;
			TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

			//parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;
			//TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters;
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			parameters.SetLocationSearch(0,pageState.MapLocationSearch);
			parameters.SetLocation(0, pageState.MapLocation);
			//parameters.GetLocationSearch(0).l
			 
			// Check to see what the map mode is. If the map mode is not
			// FromJourneyPlannerInput, then the map mode must be updated.
			CurrentMapMode mapMode = pageState.MapMode;

			// Update the input text in the location search
			//parameters.Origin.InputText = parameters.OriginLocation.Description;

			//parameters.GetLocation(1).Description  
			parameters.GetLocationSearch(0).InputText = parameters.GetLocation(0).Description;


		

			//			if (mapMode == CurrentMapMode.FromFindAInput)
			//				parameters.DestinationLocation.NaPTANs = new TDNaptan[0];

			if( mapMode != CurrentMapMode.FromJourneyInput && mapMode != CurrentMapMode.FromFindAInput)
			{
				// Check to see if Destination is valid
				if(parameters.DestinationLocation.Status == TDLocationStatus.Valid)
				{
					pageState.MapMode = CurrentMapMode.FindJourneys;
				}
				else
				{
					pageState.MapMode = CurrentMapMode.TravelTo;
					pageState.MapLocationSearch = new LocationSearch();
					pageState.MapLocation = new TDLocation();
					pageState.MapLocationControlType.Type = ControlType.Default;
					LoadSessionVariables();
					triStateLocationControl1.Reset();
					RefreshLocationControl(Page.IsPostBack);
				}
			}

		}

		/// <summary>
		/// Updates the VisitPlannerLocation1  Journey Parameters with the selected location.
		/// </summary>
		private void UpdateVisitLocation1()
		{
			//private TDJourneyParametersVisitPlan parameters;
			TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

			//parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;
			//TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters;
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			parameters.SetLocationSearch(1,pageState.MapLocationSearch);
			parameters.SetLocation(1, pageState.MapLocation);
			//parameters.GetLocationSearch(0).l
			 
			// Check to see what the map mode is. If the map mode is not
			// FromJourneyPlannerInput, then the map mode must be updated.
			CurrentMapMode mapMode = pageState.MapMode;

			// Update the input text in the location search
			//parameters.Origin.InputText = parameters.OriginLocation.Description;

			//parameters.GetLocation(1).Description  
			parameters.GetLocationSearch(1).InputText = parameters.GetLocation(1).Description;


		

			//			if (mapMode == CurrentMapMode.FromFindAInput)
			//				parameters.DestinationLocation.NaPTANs = new TDNaptan[0];

			if( mapMode != CurrentMapMode.FromJourneyInput && mapMode != CurrentMapMode.FromFindAInput)
			{
				// Check to see if Destination is valid
				if(parameters.DestinationLocation.Status == TDLocationStatus.Valid)
				{
					pageState.MapMode = CurrentMapMode.FindJourneys;
				}
				else
				{
					pageState.MapMode = CurrentMapMode.TravelTo;
					pageState.MapLocationSearch = new LocationSearch();
					pageState.MapLocation = new TDLocation();
					pageState.MapLocationControlType.Type = ControlType.Default;
					LoadSessionVariables();
					triStateLocationControl1.Reset();
					RefreshLocationControl(Page.IsPostBack);
				}
			}

		}

		/// <summary>
		/// Updates the VisitPlannerLocation2 Location Journey Parameters with the selected location.
		/// </summary>
		private void UpdateVisitLocation2()
		{
			//private TDJourneyParametersVisitPlan parameters;
			TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

			//parameters = sessionManager.JourneyParameters as TDJourneyParametersVisitPlan;
			//TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters;
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			parameters.SetLocationSearch(2,pageState.MapLocationSearch);
			parameters.SetLocation(2, pageState.MapLocation);
			//parameters.GetLocationSearch(0).l
			 
			// Check to see what the map mode is. If the map mode is not
			// FromJourneyPlannerInput, then the map mode must be updated.
			CurrentMapMode mapMode = pageState.MapMode;

			// Update the input text in the location search
			//parameters.Origin.InputText = parameters.OriginLocation.Description;

			//parameters.GetLocation(1).Description  
			parameters.GetLocationSearch(2).InputText = parameters.GetLocation(2).Description;


		

			//			if (mapMode == CurrentMapMode.FromFindAInput)
			//				parameters.DestinationLocation.NaPTANs = new TDNaptan[0];

			if( mapMode != CurrentMapMode.FromJourneyInput && mapMode != CurrentMapMode.FromFindAInput)
			{
				// Check to see if Destination is valid
				if(parameters.DestinationLocation.Status == TDLocationStatus.Valid)
				{
					pageState.MapMode = CurrentMapMode.FindJourneys;
				}
				else
				{
					pageState.MapMode = CurrentMapMode.TravelTo;
					pageState.MapLocationSearch = new LocationSearch();
					pageState.MapLocation = new TDLocation();
					pageState.MapLocationControlType.Type = ControlType.Default;
					LoadSessionVariables();
					triStateLocationControl1.Reset();
					RefreshLocationControl(Page.IsPostBack);
				}
			}

		}

		/// <summary>
		/// Method to reset the session (for input). This is required if the user is
		/// on the journey input screen and clicks on the map tab button on the header.
		/// The session data must be reset if the user clicks on the 'Travel From' or
		/// 'Travel To' buttons.
		/// </summary>
		private void ResetSession()
		{
			// If the user has previously planned a journey from the journey planner
			// then need to clear the session data here, but we need to check if this
			// is the first time this button is being clicked on before we clear the data.

			// First, found what the map mode is as we only want to clear if NOT FromJourneyInput
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			if(pageState.MapMode != CurrentMapMode.FromJourneyInput && pageState.MapMode != CurrentMapMode.FromFindAInput)
			{
				// This means we have transferred to this screen NOT from the journey input screen's
				// map button but by the tab button on the header.

				// Check to see if a reset has already been previously done.
				if(!resetted)
				{
					// Clear input session data
					TDSessionManager.Current.InputPageState.Initialise();
					TDSessionManager.Current.JourneyParameters.Initialise();
					
					if(TDSessionManager.Current.JourneyResult != null)
					{
						// Invalidate the current journey result
						TDSessionManager.Current.JourneyResult.IsValid = false;
					}

					// Update the boolean so that we don't reset the session data again!
					resetted = true;
				}
			}
		}

		/// <summary>
		/// Sets the visibility of the Back button.
		/// </summary>
		private void SetBackButtonVisibility()
		{
           

			if( TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count == 0)
			{
				this.buttonTopBack.Visible = false;
				this.literalSpaceBeforeLocation.Visible = literalSpaceAfterLocation.Visible = false;
			}
			else
			{
				// Initialise button
				buttonTopBack.Text = resourceManager.GetString("JourneyPlannerLocationMap.buttonTopBack.Text");
				this.literalSpaceBeforeLocation.Visible = literalSpaceAfterLocation.Visible = true;
            }
		}

		/// <summary>
		/// Method to (en/dis)able the Find new Map and Continue Journey Plan
		/// buttons.
		/// </summary>
		/// <param name="enable">True to enable, false to disable.</param>
		private void EnablePageButtons(bool enable)
		{
			CurrentMapMode mode = TDSessionManager.Current.InputPageState.MapMode;

			

			// Enable/Disable the "Continue journey plan" buttons (top and bottom)
			buttonTopNext.Visible = enable && (mode == CurrentMapMode.FromJourneyInput || mode == CurrentMapMode.FromFindAInput  || mode == CurrentMapMode.VisitPlannerLocationOrigin 
				|| mode == CurrentMapMode.VisitPlannerLocation1 || mode ==CurrentMapMode.VisitPlannerLocation2);
		}

		/// <summary>
		/// Resets the selected icons in the session to the default. This
		/// text is used in the Printer Friendly page.
		/// </summary>
		private void ResetSelectedMapIconsInSession()
		{
			if(TDSessionManager.Current.InputPageState.IconSelectionOutward != null)
			{
				// Clear the selected icons in the session
				for( int i=0;
					i < TDSessionManager.Current.InputPageState.IconSelectionOutward.Length;
					i ++ )
				{
					for( int j=0;
						j < TDSessionManager.Current.InputPageState.IconSelectionOutward[i].Length;
						j ++)
					{
						TDSessionManager.Current.InputPageState.IconSelectionOutward[i][j] = false;
					}
				}
			}
		}

        /// <summary>
        /// For an ambiguous location search that is drillable, attempts
        /// to decrease the current drill down level by one. If the search is
        /// not ambiguous, or the current level is zero, then the user is
        /// redirected to the previous page.
        /// </summary>
        private void upAmbiguityLevel() {
            if (mapLocation.Status == TDLocationStatus.Ambiguous && mapSearch.CurrentLevel > 0) {
                mapSearch.DecrementLevel();
                RefreshLocationControl(Page.IsPostBack);
            } else {
                // Write the Transition Event
                ITDSessionManager sessionManager = 
                    (ITDSessionManager)TDServiceDiscovery.Current
                    [ServiceDiscoveryKey.SessionManager];

                sessionManager.FormShift[SessionKey.TransitionEvent] =
                    TransitionEvent.LocationMapBack;
            }
        }

        /// <summary>
        /// Performs a search for the currently selected location in an
        /// attempt to resolve it. Refreshes the page with the result.
        /// </summary>
        private void downAmbiguityLevel() {
            triStateLocationControl1.Search(true);
            RefreshLocationControl(Page.IsPostBack);
        }

		/// <summary>
		/// Performs a check to find out if the language has been changed while
		/// on this page, uses the PageLanguage in the InputPageState 
		/// </summary>
		private bool HasLanguageChanged() 
		{
			// Added to prevent map being cleared if language is changed while on this page

			InputPageState pageState = TDSessionManager.Current.InputPageState;
			string previousLanguage = pageState.PageLanguage;
			string currentLanguage = GetPageLanguage();
         			
			// An empty string indicates this page has been accessed or loaded for the first time, 
			// therefore the language has not been changed and is the same as the current language
			if (previousLanguage == string.Empty)
				previousLanguage = currentLanguage;

			if (currentLanguage == previousLanguage)
				return false;
			else 
				return true;
		}

		/// <summary>
		/// Sends back the current page language as a string
		/// </summary>
		private string GetPageLanguage()
		{
	
            Language currentLanguage = CurrentLanguage.Value;
			return currentLanguage.ToString();
		}

		/// <summary>
		/// Saves the current page language to the InputPageState
		/// </summary>
		private void SavePageLanguageToSession()
		{
			string currentLanguage = GetPageLanguage();

			// Update session variable to the current language
			InputPageState pageState = TDSessionManager.Current.InputPageState;
			pageState.PageLanguage = currentLanguage;
		}

		#endregion

		#region ViewState Code

		/// <summary>
		/// Loads the internal viewstate for this page.
		/// </summary>
		/// <param name="savedState">Object containing the saved state.</param>
		protected override void LoadViewState(object savedState) 
		{
			if (savedState != null)
			{
				// Load State from the array of objects that was saved at SavedViewState.
				object[] myState = (object[])savedState;

				if (myState[0] != null)
					base.LoadViewState(myState[0]);
				if (myState[1] != null)
					resetted = (bool)myState[1];
				if (myState[2] != null)
					locationNotValid = (bool)myState[2];
				if (myState[3] != null)
					TDSessionManager.Current.InputPageState.MapLocation.Status = (TDLocationStatus)myState[3];
			}
		}

		/// <summary>
		/// Overrides the base SaveViewState to customise viestate behaviour.
		/// </summary>
		/// <returns>The ViewState object to be saved.</returns>
		protected override object SaveViewState()
		{ 
			// Save State as a cumulative array of objects.
			object baseState = base.SaveViewState();
		
			object[] allStates = new object[4];
			allStates[0] = baseState;
			allStates[1] = resetted;
			allStates[2] = locationNotValid;
			allStates[3] = TDSessionManager.Current.InputPageState.MapLocation.Status;

			return allStates;
		}

		#endregion

		#region OnPreRender method

		/// <summary>
		/// Overrides base OnPreRender. Updates the Map Tools Control
		/// and calls base OnPreRender.
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			// Update the state of the buttons on the page depending on the current location status
			if (TDSessionManager.Current.InputPageState.MapLocation.Status == TDLocationStatus.Valid)
			{
				// Enable the "Find New Map", "Continue Journey Plan", "From", "To" buttons
				EnablePageButtons(true);
			}
			else
			{
				// Status of the map location is not VALID
				// Disable the "Find New Map", "Continue Journey Plan", "From", "To" buttons
				EnablePageButtons(false);
			}		

			// at every postback, if the IconSelection is enabled update POI on map
			if (mapLocationIconsSelectControl.IsEnabled)
				IconsSelectRefresh();
			
			if (mapToRefresh)
				theMapControl.Map.Refresh();

            // setting the label for the tri state location input box
            triStateLocationControl1.LocationUnspecifiedControl.TypeInstruction.Text = GetResource("FindStationInput.labelSRLocation");
           
			base.OnPreRender(e);
		}

		#endregion

		override protected void OnUnload(EventArgs e)
		{
			base.OnUnload(e);
		}

		#region Event Handlers
		/// <summary>
		/// Page Load.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{

            TDSessionManager.Current.Partition = TDSessionPartition.TimeBased;

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

			// Loading Session variables
			LoadSessionVariables();

			InputPageState pageState = TDSessionManager.Current.InputPageState;

			bool fromJourneyInput = pageState.MapMode == CurrentMapMode.FromJourneyInput;
			bool fromFindAInput = pageState.MapMode == CurrentMapMode.FromFindAInput;
			bool fromTravelBoth = pageState.MapMode == CurrentMapMode.TravelBoth;


			bool fromVisitPlanner = false;

            triStateLocationControl1.LocationAmbiguousControl.NewLocationVisible = false;

            imageFindAMap.ImageUrl = GetResource("HomeFindAPlace.imageFindAPlace.ImageUrl");
            imageFindAMap.AlternateText = GetResource("HomeFindAPlace.imageFindAPlace.AlternateText");

			if((pageState.MapMode == CurrentMapMode.VisitPlannerLocationOrigin)|| (pageState.MapMode == CurrentMapMode.VisitPlannerLocation1)
				|| (pageState.MapMode == CurrentMapMode.VisitPlannerLocation2))
			{
				fromVisitPlanner = true;

			}

			// Code previously used to set TabSection (now in HeaderControl) but now just sets
			// required properties when coming from FindAPlaceControl
			// Added for interacting with the FindAPlaceControl
			if (TDSessionManager.Current.GetOneUseKey(SessionKey.FindALocationFromHomePage) != null)
			{
				pageState.MapMode = CurrentMapMode.FindJourneys;
				triStateLocationControl1.Populate(
					DataServices.DataServiceType.FromToDrop,
					pageState.MapType,
					ref mapSearch,
					ref mapLocation,
					ref locationControlType,
					true,
					true,
					true, // Same rules apply for Partial Postcode in this case	
					false);
				downAmbiguityLevel();	
			}


			// If we don't come from FindA page, the FindAMode needs to be turned off
			if ( pageState.MapMode != CurrentMapMode.FromFindAInput)
			{
				TDSessionManager.Current.FindPageState = FindPageState.CreateInstance(FindAMode.None);
			}

			
			mapLocationControl.CurrentPageId = this.pageId;
			mapLocationControl.Map = theMapControl.Map;

            //If this isn't a post back and the previous page has ensured that data has not 
            //been saved and can be reloaded.
			if(!Page.IsPostBack && TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack) == null )
			{
				//Re-initialise the map state(s)
				TDSessionManager.Current.JourneyMapState.Initialise();
				TDSessionManager.Current.ReturnJourneyMapState.Initialise();

				// Check for postback because if first time entering the
				// the page, the mode SHOULD BE travel both or
				// FromJourneyInput unless Force Load is set.
				if((!fromJourneyInput && !fromFindAInput) && !fromTravelBoth && !fromVisitPlanner)
				{
					// Reset to TravelBoth only if the force reload
					if(!pageState.JourneyPlannerLocationMapForceLoad)
					{
						pageState.MapMode = CurrentMapMode.TravelBoth;		
					}
					else
						pageState.JourneyPlannerLocationMapForceLoad = false;
				}

                bool bothLoc = pageState.MapMode == CurrentMapMode.TravelBoth;
                bool fromLoc = bothLoc || pageState.MapMode == CurrentMapMode.TravelFrom;
                bool toLoc = bothLoc || pageState.MapMode == CurrentMapMode.TravelTo;

                // If page first time displayed and not coming from JourneyPlanner,
                // init search, location and indexes in session
                if (bothLoc)
                {
					// Only initialise if the language hasn't changed, this prevents 
					// map found from being reset when we switch between languages
					if (!HasLanguageChanged())
					{
						pageState.MapLocationSearch.ClearAll();
						pageState.MapLocationSearch.SearchType = GetDefaultSearchType(pageState.MapType);		
						pageState.MapLocation.Status = TDLocationStatus.Unspecified;
						pageState.MapLocationControlType.Type = ControlType.Default;
					}
                }
                else if( (fromJourneyInput || fromFindAInput) && pageState.MapLocation.Status == TDLocationStatus.Valid )
                {
                    // Set appropriate location
                    AddAdditionalIconsOnMap();
                }

				

				// Load resources
				LoadResources();

                // Set back-button state
                SetBackButtonVisibility();

				// Reset selected icons in the session
				ResetSelectedMapIconsInSession();

				// Call method to determine if the back button should be displayed.
				

				// Saves the page language to session. Used when checking if the page
				// language has changed, to prevent map from being reset
				SavePageLanguageToSession();

				//Setup the map legend
				//hyperLinkMapKey.Text = Global.tdResourceManager.GetString
				//	("JourneyMapControl.hyperLinkMapKey.Text", TDCultureInfo.CurrentUICulture);
				//hyperLinkMapKey.ToolTip = Global.tdResourceManager.GetString
				//	("JourneyMapControl.hyperLinkMapKey.AlternateText", TDCultureInfo.CurrentUICulture);
				//hyperLinkMapKey.Target = "_blank";

				//Set the URL for the map legend at the current scale if one exists
				MapHelper helper = new MapHelper();
				//hyperLinkMapKey.NavigateUrl = helper.getLegandUrl ( theMapControl.Map.Scale );
			    //hyperLinkMapKey.Visible = !( hyperLinkMapKey.NavigateUrl == string.Empty );

                
               
			}
			else if( TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack ) != null )
			{
				// Get map data
				// Inject it
				try
				{
                    theMapControl.Map.InjectViewState(TDSessionManager.Current.StoredMapViewState[ TDSessionManager.OUTWARDMAP] );
				}
				catch( MapExceptionGeneral exc)
				{
					Logger.Write( new OperationalEvent( TDEventCategory.ThirdParty, TDTraceLevel.Warning,exc.Message +"\nStacktrace:\n"+exc.StackTrace));
				}

				// Repopulate the Select New Location dropdown if it was populated prior to the Pagepostback
				if (mapLocationControl.MapState.State == StateEnum.Select_Option)
				{	
					// Retrieve original point user clicked on the map
					int x = pageState.MapClickPointX;
					int y = pageState.MapClickPointY;
					theMapControl.Map.FireClickEvent(x, y);
				}

				locationNotValid = pageState.MapLocation.Status != TDLocationStatus.Valid;
				// Load resources
				LoadResources();

                // Set back-button state
                SetBackButtonVisibility();
				
				// Set map helper
				MapHelper helper = new MapHelper();
				//hyperLinkMapKey.NavigateUrl = helper.getLegandUrl ( theMapControl.Map.Scale );
				//hyperLinkMapKey.Visible = !( hyperLinkMapKey.NavigateUrl == string.Empty );
			}

			// Refresh the state of this page
			RefreshLocationControl(Page.IsPostBack);
			
			// DN079 UEE
			// Adding client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);

            
            

            #region CCN 0427 left hand navigation changes
            ConfigureLeftMenu("JourneyPlannerLocationMap.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextJourneyPlannerLocationMap);
            expandableMenuControl.AddExpandedCategory("Related links");

            #endregion

            
		}

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (TDSessionManager.Current.IsStopInformationMode)
            {
                newSearchButton.Visible = false;
                buttonTopNext.Visible = false;
                mapLocationControl.Visible = false;
            }
        }


		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
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

		}

		#endregion

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraWiringEvents()
		{

			this.helpLabelMapIcons.MoreHelpEvent += new EventHandler( this.OnMapStore );

			this.helpLabelMapTools.MoreHelpEvent += new EventHandler( this.OnMapStore );
			this.helpLabelMapToolsDestination.MoreHelpEvent += new EventHandler( this.OnMapStore );
			this.helpLabelMapToolsStart.MoreHelpEvent += new EventHandler( this.OnMapStore );
			this.helpLabelMapToolsVia.MoreHelpEvent += new EventHandler( this.OnMapStore );

			
			// Wiring for the Map Selection Control
            mapLocationControl.ModeChangeEvent += new ModeChangedEventHandler ( this.MapModeChanged );
           
            mapLocationControl.LocationSelectedEvent += new LocationSelectedEventHandler( this.LocationSelected );
			mapLocationControl.InformationRequestedEvent += new EventHandler( this.OnMapStore );

			// Add a handler for the OnMapQueryEvent. This event is fired from the
			// Mapping component when it is clicked on.
            theMapControl.Map.OnMapQueryEvent += new MapQueryEventHandler( this.mapLocationControl.SelectLocationControl.MapQuery );

			// Add a handler for the OnMapChangedEvent.
			theMapControl.Map.OnMapChangedEvent += new MapChangedEventHandler(this.Map_Changed);

			// Add a handler for the map Exception event
			theMapControl.Map.OnMapExceptionEvent += new MapExceptionEventHandler(this.Map_Exception);

			triStateLocationControl1.ValidLocation += new EventHandler(OnValidLocation);	
            //triStateLocationControl1.NewLocation += new EventHandler(OnNewLocation);
			triStateLocationControl1.NewSearchType += new EventHandler(OnNewSearchType);

            //CCN 0427 handling events of the newsearch button
            newSearchButton.Click +=new EventHandler(OnNewLocation);

            buttonTopNext.Click += new EventHandler(this.buttonNext_Click);
			buttonTopBack.Click += new EventHandler(this.buttonBack_Click);

            previousLocationButton.Click += new EventHandler(this.previousLocationButton_Click);
            resolveLocationButton.Click += new EventHandler(this.resolveLocationButton_Click);

            commandBack.Click += new EventHandler(OnNewLocation);

			// DN079 UEE
			// Event Handler for default action button
			headerControl.DefaultActionEvent += new EventHandler(DefaultActionClick);  

		}

		/// <summary>
		/// Map exception event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="eventArgs"></param>
		private void Map_Exception(object sender, MapExceptionEventArgs eventArgs)
		{
			// Exception has been thrown by the map

			// Log the event
			OperationalEvent operationalEvent = new OperationalEvent(
				TDEventCategory.ThirdParty, TDTraceLevel.Error,
				"Exception thrown by ESRI mapping component." +
				"Error Code:" + eventArgs.ErrorCode + "Error Description:" + eventArgs.ErrorDescription);
		
			Logger.Write(operationalEvent);
		}

		/// <summary>
		/// Handles the ValidLocation event.  This event is fired when
		/// the map location status becomes valid.
		/// </summary>
		private void OnValidLocation(object sender, EventArgs e)
		{		
			this.mapLocationControl.MapState.Location.Status = TDLocationStatus.Unspecified;
			// Refresh the location control.
			RefreshLocationControl(Page.IsPostBack);
			// Zoom to point -> clears all the overlay
			ZoomMapInitial(); // zoom to point
			// Add back the nessecary overlays
			AddAdditionalIconsOnMap();

			theMapControl.Map.ToggleLayerVisibility(theMapControl.Map.CarParkLayerIndex);
		}

        /// <summary>
        /// Creates new location and search objects and associates them with this object and 
        /// the map location and search objects held in the session's page state object.
        /// Updates the location selection control to accept input for a new location
        /// </summary>
        /// <param name="sender">Event originator</param>
        /// <param name="e">Event parameters</param>
        private void OnNewLocation(object sender, EventArgs e)
        {
            InputPageState pageState = TDSessionManager.Current.InputPageState;

            mapLocation = new TDLocation();
            pageState.MapLocation = mapLocation;
            mapSearch = new LocationSearch();
            pageState.MapLocationSearch = mapSearch;
            locationControlType = new LocationSelectControlType(ControlType.NewLocation);
            pageState.MapLocationControlType = locationControlType;
            mapSearch.SearchType = GetDefaultSearchType(pageState.MapType);		
            RefreshLocationControl(false);

        }

		/// <summary>
		/// Called when the search type is changed via the auto postback
		/// radio buttons. Clicking these can resolve location - if so, 
		/// need to handle accordingly, similar to clicking Next.
		/// </summary>
		/// <param name="sender">Event originator</param>
		/// <param name="e">Event parameters</param>
		private void OnNewSearchType(object sender, EventArgs e)
		{
			InputPageState pageState = TDSessionManager.Current.InputPageState;
			
			// Clicking auto postback radio button has resolved location.
			if (pageState.MapLocation.Status == TDLocationStatus.Valid)
			{
				//Need to clear help boxes as no longer relevant 
				HelpControlLocation.Close();
                HelpControlLocation2.Close();
				//HelpControlMapIcons.Close();
			}
		}

		/// <summary>
		/// Event handler for the map changed event.
		/// </summary>
        private void Map_Changed(object sender, MapChangedEventArgs e)
        {
            // Update the image url of the summary map
            imageSummaryMap.ImageUrl = e.OvURL;

           
            // Change the scale of the map - CCN 0427 calling mapcontrol's ScaleChange method rather then 
            // map zoom control's ScaleChange method;
            theMapControl.ScaleChange( e.MapScale, e.NaptanInRange );

            // Check to see if it is necessary to update the Location Select control
            // and Map Tools control, as the scale determines if the OK buttons
            // should be enabled, disabled.
            // The OK button can be enabled only if naptans are in range.
            mapLocationIconsSelectControl.EnableOKButton(e.NaptanInRange);
            TDSessionManager.Current.JourneyMapState.SelectEnabled = e.NaptanInRange;

			// Update the map URLS and scales in InputPageState for printer friendly maps
			TDSessionManager.Current.InputPageState.MapUrlOutward =
				theMapControl.Map.ImageUrl;

			TDSessionManager.Current.InputPageState.OverviewMapUrlOutward =
				e.OvURL;

			TDSessionManager.Current.InputPageState.MapScaleOutward =
				e.MapScale;

			// Save view state so map is available when printer friendly page is displayed
			TDSessionManager.Current.StoredMapViewState[TDSessionManager.OUTWARDMAP] = theMapControl.Map.ExtractViewState();

            MapHelper helper = new MapHelper();
            //hyperLinkMapKey.NavigateUrl = helper.getLegandUrl ( e.MapScale );
            //hyperLinkMapKey.Visible = !( hyperLinkMapKey.NavigateUrl == string.Empty );
		}

		/// <summary>
		/// Previous view button handler for the Map Tools Control. This handler
		/// will update the mapping component to show the previous view.
		/// </summary>
		private void btnPreviousView_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				// Forward the call to the mapping component.
				theMapControl.Map.ZoomPrevious();
			}
			catch(PropertiesNotSetException pnse)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

				Logger.Write(operationalEvent);
			}
			catch(MapNotStartedException mnse)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);
			
				Logger.Write(operationalEvent);
			}
			catch(ScaleOutOfRangeException soore)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

				Logger.Write(operationalEvent);
			}
			catch(ScaleZeroOrNegativeException szone)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

				Logger.Write(operationalEvent);
			}
			catch(NoPreviousExtentException npee)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

				Logger.Write(operationalEvent);
			}
			catch(MapExceptionGeneral mge)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mge.Message);

				Logger.Write(operationalEvent);
			}

			// Write the Transition Event
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.LocationMapDefault;
		}

		/// <summary>
		/// Event handler for page back buttons
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event parameters</param>
        private void buttonBack_Click(object sender, EventArgs e) {
            upAmbiguityLevel();
		}

		/// <summary>
		/// Handler for the Zoom Event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ZoomMap(object sender, ZoomLevelEventArgs e)
		{
			if(e.ZoomLevel > 0)
				theMapControl.Map.SetScale(e.ZoomLevel);
		}

		/// <summary>
		/// Handler for the zoom in event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ZoomMapIn(object sender, EventArgs e)
		{
			// Get the current map click mode
			Map.ClickModeType currentClickMode = theMapControl.Map.ClickMode;

			// Set the click mode to zoom in
			theMapControl.Map.ClickMode = Map.ClickModeType.ZoomIn;
			
			// Raise the click event
			theMapControl.Map.FireClickEvent();

			// Restore the click mode
			theMapControl.Map.ClickMode = currentClickMode;
		}

		/// <summary>
		/// Handler for the Zoom out event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ZoomMapOut(object sender, EventArgs e)
		{
			// Get the current map click mode
			Map.ClickModeType currentClickMode = theMapControl.Map.ClickMode;

			// Set the click mode to zoom in
			theMapControl.Map.ClickMode = Map.ClickModeType.ZoomOut;
			
			// Raise the click event
			theMapControl.Map.FireClickEvent();

			// Restore the click mode
			theMapControl.Map.ClickMode = currentClickMode;
		}

		/// <summary>
		/// Handler for the Previous View event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void MapPreviousView(object sender, EventArgs e)
		{
			// Call previous view on the map
			theMapControl.Map.ZoomPrevious();
			this.mapToRefresh = false;
		}

		/// <summary>
		/// Handler for the Find New Map event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void MapFindNew(object sender, EventArgs e)
		{
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			// Set status of the map location to unspecified.
			pageState.MapLocation.Status = TDLocationStatus.Unspecified;
			
			this.mapLocationControl.MapState.Location.Status = TDLocationStatus.Unspecified;
			

			pageState.MapLocationSearch.ClearAll();
			pageState.MapLocationControlType.Type = ControlType.Default;

			// Set the transition event to load this page.
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			// Set flag in session to force load to True.
			pageState.JourneyPlannerLocationMapForceLoad = true;

			// Set ForceRedirect to true.
			sessionManager.FormShift[SessionKey.ForceRedirect]= true;
			
			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.GoMap;
		}

		/// <summary>
		/// Event handler for the map mode changed event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void MapModeChanged(object sender, ModeChangedEventArgs e)
		{
            // Set the map mode to the mode in the event args
            theMapControl.Map.ClickMode = e.MapMode;
	}

		/// <summary>
		/// Handler for the location selected event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void LocationSelected( object sender, LocationSelectedEventArgs e )
		{
			// OK button on the map selection control has been clicked.
			// i.e. the location has been selected.

			// Get the x, y and location name from the event args
			int x = e.CoordinateX;
			int y = e.CoordinateY;
			string name = e.LocationName;
			
			// Zoom to point on the map on the current scale
			theMapControl.Map.ZoomToPoint( (double)x, (double)y, theMapControl.Map.Scale);

			// Set the map location as new location.
			InputPageState pageState = TDSessionManager.Current.InputPageState;
			pageState.MapLocation.SearchType = pageState.MapLocationSearch.SearchType;

			this.mapLocationControl.MapState.Location = pageState.MapLocation;

			// Update the label on the page that shows what the current selected location is.
			labelSelectedLocation.Text = TDSessionManager.Current.InputPageState.MapLocation.Description;
			AddAdditionalIconsOnMap();
		}

		/// <summary>
		/// DN079 UEE
		/// Event handler for default action
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void DefaultActionClick(object sender, EventArgs e)
		{
			resolveLocationButton_Click(sender, e); 
		}


		/// <summary>
		/// Event handler for the Continue Journey Plan button.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void buttonNext_Click(object sender, EventArgs e)
		{
			// Set the location depending on what the current map type is
			InputPageState pageState = TDSessionManager.Current.InputPageState;

			switch(pageState.MapType)
			{
				case CurrentLocationType.From :
					UpdateFromLocation();
					break;

				case CurrentLocationType.To :
					UpdateToLocation();
					break;

				case CurrentLocationType.VisitPlannerOrigin:
					UpdateVisitOriginLocation();
					break;	
			
				case CurrentLocationType.VisitPlannerVisitPlace1:
					UpdateVisitLocation1();
					break;

				case CurrentLocationType.VisitPlannerVisitPlace2:
					UpdateVisitLocation2();
					break;

				case CurrentLocationType.PrivateVia :
				case CurrentLocationType.PublicVia :
                case CurrentLocationType.CycleVia:
					UpdateViaLocation();
					break;
			}

            //Reset the Journey Map State
            TDSessionManager.Current.JourneyMapState.Initialise();
            TDSessionManager.Current.ReturnJourneyMapState.Initialise();

			// Write the Transition Event
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.LocationMapBack;
		}

		/// <summary>
		/// Handler the "From This Location" button.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void imageButtonPlanJourneyFrom_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			// Check to see if the session needs resetting.
			ResetSession();

            TDSessionManager.Current.InputPageState.MapType = CurrentLocationType.From;

            //Fire next click event
            buttonNext_Click( sender, e );
		}

		/// <summary>
		/// Handler for the "to this location" button.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e"></param>
		private void imageButtonPlanJourneyTo_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			// Check to see if the session needs resetting.
			ResetSession();

            TDSessionManager.Current.InputPageState.MapType = CurrentLocationType.To;

            //Fire next click event
            buttonNext_Click( sender, e );
		}

        /// <summary>
        /// Handle button click to move up one level in a drillable search
        /// Same behaviour as for clicking page's main back buttons.
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event parameters</param>
        private void previousLocationButton_Click(object sender, EventArgs e) 
        {
            upAmbiguityLevel();
        }

        /// <summary>
        /// Handle button click to move down one level in a drillable search
        /// </summary>
        /// <param name="sender">event originator</param>
        /// <param name="e">event parameters</param>
        private void resolveLocationButton_Click(object sender, EventArgs e) 
        {
            if ( (Page.IsPostBack)
                && (TDSessionManager.Current.InputPageState.MapLocation.Status == TDLocationStatus.Valid) ) 
            {
                //This is to handle the user hitting the browser refresh button.  IR947
                OperationalEvent operationalEvent = new OperationalEvent (TDEventCategory.Infrastructure ,
                    TDTraceLevel.Verbose , "resolveLocationButton_Click : Adding start location to map." );
                Logger.Write(operationalEvent);
                AddAdditionalIconsOnMap();
            }
			//Need to clear help boxes as potentially no longer relevant 
			HelpControlLocation.Close();
            HelpControlLocation2.Close();
			//HelpControlMapIcons.Close();
            downAmbiguityLevel();
        }

		private void OnMapStore(object sender, EventArgs e )
		{
			if( TDTraceSwitch.TraceVerbose )
			{
				Logger.Write( new OperationalEvent( TDEventCategory.Business, TDTraceLevel.Verbose, "Storing map into session") );
			}
			// Store away the map viewstate
			object o = theMapControl.Map.ExtractViewState();

			TDSessionManager.Current.StoredMapViewState[ TDSessionManager.OUTWARDMAP ] = o;
		}

		#endregion

		#region Convenience methods

		/// <summary>
		/// Convenience method for getting the default gazetteer for this map type.
		/// </summary>
		/// <param name="key">The key for the text</param>
		/// <returns>The resource string</returns>
		private SearchType GetDefaultSearchType(CurrentLocationType mapType)
		{
			DataServices.DataServiceType searchTypes = DataServices.DataServiceType.LocationTypeDrop;

			switch (mapType)
			{
				case CurrentLocationType.From:
					searchTypes = DataServices.DataServiceType.FromToDrop;
					break;
				case CurrentLocationType.To:
					searchTypes = DataServices.DataServiceType.FromToDrop;
					break;
				case CurrentLocationType.PrivateVia:
					searchTypes = DataServices.DataServiceType.CarViaDrop;
					break;
				case CurrentLocationType.PublicVia:
					searchTypes = DataServices.DataServiceType.PTViaDrop;
					break;
                case CurrentLocationType.CycleVia:
                    searchTypes = DataServices.DataServiceType.CycleViaLocationDrop;
                    break;
				case CurrentLocationType.Alternate1:
					searchTypes = DataServices.DataServiceType.AltFromToDrop;
					break;
				case CurrentLocationType.Alternate2:
					searchTypes = DataServices.DataServiceType.AltFromToDrop;
					break;
			}

			DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			
			string defaultItemValue = ds.GetDefaultListControlValue(searchTypes);
			return (SearchType) (Enum.Parse(typeof(SearchType), defaultItemValue));
		}


		#endregion

	}
}