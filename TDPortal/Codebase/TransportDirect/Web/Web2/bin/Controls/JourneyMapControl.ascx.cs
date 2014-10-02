// *********************************************** 
// NAME                 : JourneyMapControl.ascx.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 21/08/2003 
// DESCRIPTION			: A custom user control to
// display a map for a public transport journey.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyMapControl.ascx.cs-arc  $
//
//   Rev 1.13   Oct 12 2009 09:11:38   apatel
//EBC Map page and printer friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.13   Oct 12 2009 08:40:04   apatel
//EBC Printer Friendly page related changes
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.12   Nov 24 2008 13:53:20   mmodi
//Updated to add ferry and toll icons for cycle journeys
//Resolution for 5176: Cycle Planner - Maps with Ferry legs do not include Ferry symbol
//
//   Rev 1.11   Nov 12 2008 13:40:30   mturner
//Changed to show different MapKey for cycle journeys.
//Resolution for 5161: Cycle Planner - Map Key shows default values
//
//   Rev 1.10   Nov 06 2008 10:40:20   mturner
//Updated to work for cycle journeys containing U-Turns
//
//   Rev 1.9   Oct 28 2008 17:06:32   mmodi
//Add Via location label for a cycle journey
//
//   Rev 1.8   Oct 27 2008 16:01:22   mmodi
//Removed comments
//
//   Rev 1.7   Oct 14 2008 16:09:02   mmodi
//Corrected
//
//   Rev 1.6   Oct 14 2008 14:02:58   mmodi
//Manual merge for stream5014
//
//   Rev 1.5   Jul 09 2008 14:53:44   mturner
//Fix for IR5046 - Door to Door Maps Produce Error
//
//   Rev 1.4   Jun 18 2008 16:11:44   dgath
//fixed ITP issues
//Resolution for 5025: ITP: Workstream
//
//   Rev 1.3.1.6   Oct 09 2008 14:41:24   mmodi
//Updated zooming to a direction to use a coordinate rather than the toids
//Resolution for 5140: Cycle Planner - 'Server Error' page is displayed when user selects 'Map of outward journey' dropdown containing series of tiles
//
//   Rev 1.3.1.5   Sep 18 2008 11:38:02   mmodi
//Do not sure Map directions button on Cycle Details page
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.4   Sep 16 2008 10:58:10   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.3   Sep 08 2008 15:49:58   mmodi
//Updated following change to Cycle journey maps processing
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.2   Aug 22 2008 10:32:42   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.1   Jun 20 2008 15:24:42   mmodi
//Updated journey count
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.0   Jun 18 2008 14:30:30   mmodi
//Updated for cycle journeys
//
//   Rev 1.3   Apr 15 2008 11:42:08   mmodi
//Removed unused variable
//
//   Rev 1.2   Mar 31 2008 13:21:36   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 20 2008 14:00:00   mmodi
//Updated to populate results with modetypes value for city to city journeys
//
//   Rev DevFactory   Feb 06 2008 17:00:00   mmodi
//If the start/end location is a car park, then display the car park name rather than the location description
//
//  Rev DevFactory Feb 05 2008 14:39:00 apatel
//  Changed layout of the controls. MapZoomControl and maplocationcontrol removed.
//
//   Rev 1.0   Nov 08 2007 13:15:42   mturner
//Initial revision.
//
//   Rev 1.151   Jul 13 2007 15:42:52   jfrank
//Fix for Park and Ride mapping issue for entire journey maps.
//Resolution for 4464: Park and Ride map of entire journey should include the locality
//
//   Rev 1.150   May 11 2007 14:56:16   nrankin
//Code change to remove Gazetteer postfixes from location name on maps
//Resolution for 4406: Gaz post fixes should not be displayed on maps
//
//   Rev 1.149   Jan 08 2007 15:50:58   jfrank
//Any <modes> have been changed to Main <modes>, this will break previous pseudo naptans CCN's.  This change means this will not occur.
//Resolution for 4333: Gaz Improvement Workshop - Actions 6, 9, 17 - Gaz Config and Naptan Config Changes
//
//   Rev 1.148   Dec 28 2006 10:55:48   mturner
//Resolution for IR4326 - Psedo Naptan locations still being displayed on Maps of car journeys
//
//   Rev 1.147   Oct 06 2006 14:48:36   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.146.1.3   Sep 29 2006 15:41:20   esevern
//Added check for car parking functionality switched on before toggling car park layer visibility
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4191: Car Parking: Configurable switch should be available to display/hide car parking functionality
//
//   Rev 1.146.1.2   Sep 29 2006 11:12:50   mmodi
//Amended code to plot Car Park start/end point for both PT and Car journey
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4209: Car Parking: Start/End point on journey map needs to be displayed using Map coordinate
//
//   Rev 1.146.1.1   Sep 22 2006 14:47:26   mmodi
//Added code to plot the end point on the map for a Car park journey
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4198: Car Parking: End point on Map for return car journey incorrect
//
//   Rev 1.146.1.0   Sep 20 2006 11:47:26   mmodi
//Added code to display car parks icon on map
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4170: Car Parking: Car park symbol not shown on PT journey map
//
//   Rev 1.146   Aug 07 2006 15:18:06   tmollart
//Code modified to refresh the map at the correct point so transport symbols are displayed.
//Resolution for 4144: Transport stop map symbols not displayed on first view of map
//
//   Rev 1.145   Jun 01 2006 08:44:46   mmodi
//IR4105: Added code to repopulate Select New Location dropdown list (when user returns back from Help page)
//Resolution for 4105: Del 8.2 - Select new location map feature dropdown values are lost
//
//   Rev 1.144   Apr 26 2006 16:00:20   mtillett
//Correct logic to display park and ride symbols for outward and return journeys and zoom to envelope for park and ride.
//Resolution for 3995: DN058 Park & Ride Phase 2: envelope size incorrect
//
//   Rev 1.143   Apr 26 2006 13:34:36   halkatib
//Fix for IR3991: Added flag to allow control to detect whether a VisitPlanner journey result contained any errors. 
//
//   Rev 1.142   Apr 25 2006 11:18:46   rwilby
//Updated control to add Start/End/Via Points to map using same logic for outward and return journeys 
//Resolution for 3966: DN068: map symbols for return portion of extended PT journey
//
//   Rev 1.141   Apr 21 2006 12:44:14   RGriffith
//IR3953 - Additional call to ZoomRoadRoute in the park and ride map update section
//Resolution for 3953: DN058 Park and Ride Phase 2: Map shown at wrong zoom level
//
//   Rev 1.140   Apr 20 2006 11:47:52   mdambrine
//n extra check is being done if an extention is in process. if it is, it skips the assignment of the first journey leg in the list.
//Resolution for 3854: DN068 Extend: extend car journey on car journey gives map of entire journey instead of single leg
//
//   Rev 1.139   Apr 10 2006 17:31:56   mdambrine
//an extra check at the end of the prerender method to check if the journeyDropDown object is null. If it is then just follow the ShowSelectedMapLeg method instead of the ShowSelectedCarMapLeg that always default to the entire journey.
//Resolution for 3854: DN068 Extend: extend car journey on car journey gives map of entire journey instead of single leg
//
//   Rev 1.138   Apr 04 2006 11:55:42   rwilby
//Merge stream0034. Fixed issue identified during the Map symbol update work.
//
//   Rev 1.137   Mar 24 2006 10:30:18   tmollart
//Added methods needed for stream0025.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.136   Mar 23 2006 17:41:14   tmollart
//Manual merge of stream 0025.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.135   Mar 13 2006 17:32:30   NMoorhouse
//Manual merge of stream3353 -> trunk
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.134   Mar 08 2006 16:03:12   CRees
//Fix for IR 3597 / Vantive 4187482.
//
//   Rev 1.133   Feb 23 2006 16:12:18   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.132   Jan 18 2006 18:57:32   jbroome
//Removed labelVisitPlanner help to avoid duplicate text
//
//   Rev 1.131.1.0   Feb 17 2006 14:30:20   NMoorhouse
//Updates (by Richard Hopkins) to support Replan Maps
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.131   Dec 06 2005 14:27:32   pcross
//Changed the way the property works that controls whether the map location control can be interacted with or not. Before it was just made invisible - now it is visible but we can just see the text
//Resolution for 3278: Visit Planner: The maps panel on the journey results page is missing 'select new location' and 'i' buttons
//
//   Rev 1.130   Nov 28 2005 15:21:18   jbroome
//Added new intialise method for Visit plan on Map key control
//Resolution for 3222: Visit Planner: Purple triangles should be in the map key for stopover locations
//
//   Rev 1.129   Nov 21 2005 17:28:48   asinclair
//Moved code from PageLoad to OnPreRender
//Resolution for 3083: Visit Planner - Map drop down not set correctly after viewing Help
//
//   Rev 1.128   Nov 11 2005 16:20:18   tolomolaiye
//Added text for help label
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.127   Nov 04 2005 15:43:18   ralonso
//Manual merge of stream2816
//
//   Rev 1.126   Nov 01 2005 15:11:38   build
//Automatically merged from branch for stream2638
//   Rev 1.125.2.1   Oct 25 2005 14:52:48   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.125.2.0   Oct 24 2005 17:04:36   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.125.1.3   Oct 29 2005 15:46:08   jbroome
//Allowed setting visibility of title area
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.125.1.2   Oct 18 2005 13:26:34   jbroome
//Show/hide MapLocationControl
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.125.1.1   Oct 14 2005 09:40:36   jbroome
//Replaced checks for SelectedItinerarySegment with FullItinerarySelected
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.125.1.0   Sep 21 2005 17:23:08   jbroome
//Updated for Visit Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.125   Aug 19 2005 14:08:32   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.124.1.1   Aug 09 2005 18:44:02   rgreenwood
//DD073 Map Details: Made ShowSelectedLegMap handle single leg journeys more robustly
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.124.1.0   Aug 04 2005 15:07:20   rgreenwood
//DD073 Map Details: Updated all method calls to MapHelper Initialise... methods so they now use the new overloads for Map Details changes. Also fixed per-leg map scaling issues and dropdown entries.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.124   May 26 2005 13:47:08   rgreenwood
//IR2535: Declared and instantiated mapHelper instance variable
//Resolution for 2535: Extend Journey - "Back To Main Results" button exception
//
//   Rev 1.123   May 10 2005 17:41:08   rhopkins
//FxCop corrections
//
//   Rev 1.122   May 09 2005 16:19:54   asinclair
//Added codes to display UK map if toid incorrect IR 2321
//
//   Rev 1.121   May 06 2005 13:58:28   asinclair
//Fix for IR 2439
//
//   Rev 1.120   May 04 2005 15:10:14   asinclair
//Added code to display toll and ferry icons on maps for car journeys
//
//   Rev 1.119   Apr 28 2005 16:28:50   pcross
//IR2369.
//Resolution for 2369: View map leg from details table on journey details page fails for extended journey
//
//   Rev 1.118   Apr 27 2005 16:28:32   pcross
//IR2355. Handle scenario where there is a single leg in a journey and therefore no associated leg breakdown
//Resolution for 2355: View map dropdown for single leg journey
//
//   Rev 1.117   Apr 26 2005 13:19:40   pcross
//IR2192. Corrections to extended journey handling
//
//   Rev 1.116   Apr 26 2005 10:15:52   pcross
//IR2192. Draw intermediate nodes for extended journeys.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.115   Apr 25 2005 19:24:58   asinclair
//Fix for IR 1983
//
//   Rev 1.114   Apr 22 2005 16:11:44   pcross
//IR2192. Control now allows a property to be set so that when rendered the dropdown of journey legs is updated and the map shown zooms to the selected leg.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.113   Apr 12 2005 10:57:44   bflenk
//Work in Progress - IR 1986
//
//   Rev 1.112   Apr 08 2005 16:09:10   rhopkins
//Changes to MapHelper methods for testing for presence of public/private results
//
//   Rev 1.111   Mar 24 2005 19:59:40   asinclair
//Removed strings used in debuging
//
//   Rev 1.110   Mar 18 2005 15:24:14   asinclair
//Updated for Del 7 Car Costing
//
//   Rev 1.109   Mar 01 2005 15:43:18   asinclair
//Updated for Del 7 Car Costing
//
//   Rev 1.108   Nov 23 2004 17:34:36   SWillcock
//Modified logic for displaying journey numbers after 'Outward journey' and 'return journey' text
//Resolution for 1781: Remove Journey Numbers from Details and Map Display in Quick Planners
//
//   Rev 1.107   Nov 09 2004 11:24:08   jgeorge
//Added use of new ZoomToAllAddedRoutes method of TransportDirect.Presentation.InteractiveMapping.Map object to zoom itinerary/multiple leg maps.
//
//   Rev 1.106   Nov 04 2004 13:00:08   passuied
//Added ability to clear Iconselection (out and ret) in Input page State when scale is too low
//Resolution for 1732: POI keys appear on printable map page although the scale is too low.
//
//   Rev 1.105   Nov 04 2004 11:10:08   JHaydock
//Performance enhancement - use of BLOBs for road route journeys
//
//   Rev 1.104   Sep 30 2004 12:12:56   rhopkins
//IR1648 Use ReturnOriginLocation and ReturnDestinationLocation when outputting Return Car segments of Extended Journeys.
//
//   Rev 1.103   Sep 27 2004 16:07:40   passuied
//Used the JourneyRequest.ReturnOriginLocation and JourneyRequest.ReturnDestinationLocation to fill return locations.
//
//   Rev 1.102   Sep 22 2004 17:26:02   esevern
//hide key control help button if the journey is not car
//
//   Rev 1.101   Sep 21 2004 16:31:44   esevern
//IR1581 - removed display of selected journey number for find a car (there will only be one journey)
//
//   Rev 1.100   Sep 20 2004 16:00:22   asinclair
//Further fix for IR 1592
//
//   Rev 1.99   Sep 20 2004 15:20:14   asinclair
//IR 1592 - Travel mode overlays
//
//   Rev 1.98   Sep 20 2004 12:22:02   jbroome
//IR 1578 and 1533. Ensuring Maps are displayed correctly (returning from More Help... and Find A return journeys)
//
//   Rev 1.97   Sep 17 2004 17:33:42   esevern
//partial fix for IR1581 - checked in for handover 
//
//   Rev 1.96   Sep 17 2004 15:13:52   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.95   Sep 15 2004 15:47:42   RHopkins
//IR1489 The existing method drawItineraryJourneys() is now invoked at the end of the UpdateMap() method when displaying a Full Itinerary.
//
//   Rev 1.94   Sep 04 2004 09:47:06   jbroome
//IR 1493 Call correct initialisation of MapKeyControl for FindACar mode.
//
//   Rev 1.93   Aug 05 2004 14:49:22   COwczarek
//Use IsFindAMode to determine how to initialise map
//Resolution for 1202: Implement FindTrainInput and FindCoachInput pages
//
//   Rev 1.92   Jul 20 2004 12:08:34   CHosegood
//Now sets 'adjusted' box from session state.
//
//   Rev 1.91   Jul 12 2004 19:54:42   JHaydock
//DEL 5.4.7 Merge: IR 1132
//
//   Rev 1.90   Jul 12 2004 15:07:20   jbroome
//Actioned Extend Journey code review comments.
//
//   Rev 1.89   Jun 22 2004 13:36:56   jbroome
//Ensured map is updated only when required. (using ItineraryManagerModeChanged)
//
//   Rev 1.88   Jun 18 2004 14:54:22   jbroome
//Fixed minor Itinerary error
//
//   Rev 1.87   Jun 17 2004 17:23:50   jbroome
//Updated retrieval of JourneyViewState and JourneyResult.
//
//   Rev 1.86   Jun 08 2004 17:19:06   jbroome
//Correct display of Start/Via/End points when displaying Return Itinerary journeys.
//
//   Rev 1.85   Jun 08 2004 14:38:42   RPhilpott
//Add further support for only displaying key for specific modes. 
//
//   Rev 1.84   Jun 07 2004 15:23:32   jbroome
//ExtendJourney - added support for TDItineraryManager and displaying multiple journeys.
//
//   Rev 1.83   May 18 2004 13:26:54   jbroome
//IR861 Resolving issue of Zoom Control levels and Map Symbols.
//
//   Rev 1.82   Apr 16 2004 10:07:56   asinclair
//Added label for Map symbols
//
//   Rev 1.81   Apr 14 2004 11:08:06   CHosegood
//If this is a private only journey or private journey selected on summary page before viewing maps then the UpdateRoadJourneyControls method is called OnPreRender
//Resolution for 737: Show directions button on Car Summary - Maps screen
//
//   Rev 1.80   Apr 06 2004 11:54:16   asinclair
//Added code to save the map view state on a 'More' help click
//
//   Rev 1.79   Apr 05 2004 17:12:38   CHosegood
//Del 5.2 map QA fixes
//
//   Rev 1.78   Apr 01 2004 10:37:22   CHosegood
//Help button now opens the correct help label
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.77   Mar 31 2004 13:48:56   CHosegood
//Del 5.2 map qa fixes
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.76   Mar 23 2004 15:08:54   asinclair
//Renamed Help Labels
//
//   Rev 1.75   Mar 22 2004 11:48:36   CHosegood
//Del 5.2 BBC QA changes
//Resolution for 665: Del 5.2 BBC QA changes
//
//   Rev 1.74   Mar 16 2004 16:38:42   CHosegood
//Del 5.2 map changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.73   Mar 16 2004 09:45:00   PNorell
//Updated to remember positions.
//
//   Rev 1.72   Mar 15 2004 19:49:44   pnorell
//Updated to keep location information between information location page and the normal page.
//Currently simpleminded and only works for outward.
//Not marked ready for build.
//
//   Rev 1.71   Mar 15 2004 18:18:04   CHosegood
//Del 5.2 Map Changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.70   Mar 12 2004 17:20:42   CHosegood
//Del5.2 Map changes::Check-in for integration
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.69   Jan 13 2004 19:52:42   RPhilpott
//Make compatible with changes in CarJourneyDetailsTableControl
//Resolution for 549: Send to a Friend email eight various problems
//Resolution for 589: Mileage/road description mismatch
//
//   Rev 1.68   Dec 17 2003 15:24:06   kcheung
//Minor updates required to map logging changes.
//
//   Rev 1.67   Dec 15 2003 16:14:20   kcheung
//Fixed printer friendly map name when original view is clicked.
//
//   Rev 1.66   Dec 02 2003 13:18:00   kcheung
//Fixed so that if both routes is selected on the screen map page, the printable map page will not display the directions table.
//Removed call to ZoomFull for car journey because latest td.interactivemapping dll address the problem where this had to be called first.
//
//   Rev 1.65   Dec 01 2003 15:16:18   kcheung
//Added label for unadjusted route selection.
//Resolution for 460: In the traffic map for a car journey, the adjusted route should be default
//
//   Rev 1.64   Nov 27 2003 19:31:30   asinclair
//Added code to switch alt tags
//
//   Rev 1.63   Nov 26 2003 18:11:26   kcheung
//Rounding bug fix.
//
//   Rev 1.62   Nov 26 2003 16:26:28   passuied
//refresh map only once on page
//Resolution for 381: The 'Previous View' Map functionality is not working
//
//   Rev 1.61   Nov 19 2003 11:58:02   kcheung
//To use new view state and tdjourney result properties
//Resolution for 132: Determing whether Journey is return or not
//Resolution for 136: Properties of JourneyViewState to determine the selected outward and return journeys
//
//   Rev 1.60   Nov 14 2003 13:04:40   kcheung
//Incorporated zoom minus and zoom plus buttons.
//Resolution for 173: The map zoom control, comprising 13 zoom level links, is missing an indication of zoom direction
//
//   Rev 1.59   Nov 12 2003 19:40:48   PNorell
//Updated for 'previous view' bug fix from ESRI.
//
//   Rev 1.58   Nov 06 2003 12:41:28   kcheung
//Start/End points appear for public journeys
//
//   Rev 1.57   Nov 06 2003 11:39:14   kcheung
//Fixed printing for Netscape
//
//   Rev 1.56   Nov 05 2003 16:27:04   kcheung
//Fixed bug where stations and pointxs are being displayed when they shouldn't be.
//
//   Rev 1.55   Nov 05 2003 13:59:08   kcheung
//Added summary header. Updated Header Text.
//
//   Rev 1.54   Nov 05 2003 11:16:44   kcheung
//Fixed bug where selected map text in session is not updated if you select another journey on the map page.
//
//   Rev 1.53   Oct 31 2003 11:01:36   passuied
//Resolution of SCR 44 and 45.
//Refresh of POI on map everytime action is done on map and scale is close enough to display something.
//Resolution for 44: pois overlaid on the output map are not changed on zoom
//Resolution for 45: point of interest category changed but existing items on map are not cleared
//
//   Rev 1.52   Oct 28 2003 14:50:22   kcheung
//Fixed so that printer friendly map gets the correct map name being shown when entering the page.
//
//   Rev 1.51   Oct 27 2003 14:24:44   kcheung
//Added extra exception catch on UpdateMap
//
//   Rev 1.50   Oct 23 2003 13:47:44   kcheung
//Fixed checking of duplicates in the drop down list.
//
//   Rev 1.49   Oct 22 2003 19:23:18   passuied
//changes for printable output map page
//
//   Rev 1.48   Oct 22 2003 11:10:58   kcheung
//Fixed for FXCOP
//
//   Rev 1.47   Oct 22 2003 10:25:06   kcheung
//Fixed the ZoomToEnvelope bug where it was throwing an exception when the envelope was too small.
//
//   Rev 1.46   Oct 21 2003 16:18:50   kcheung
//Updates applied after updates to FXCOP changes in MapToolsControl
//
//   Rev 1.45   Oct 21 2003 12:29:50   kcheung
//Fixed so that start/end points for return are correctly displayed.
//
//   Rev 1.44   Oct 20 2003 11:47:22   kcheung
//Cosmetic corrections to comply with FXCOP
//
//   Rev 1.43   Oct 16 2003 17:53:28   kcheung
//Fixed so that Original view resets the drop down box
//
//   Rev 1.42   Oct 15 2003 16:33:04   kcheung
//Added code to reset route before a new one is added onto the map
//
//   Rev 1.41   Oct 15 2003 13:30:00   PNorell
//Updates to get the correct journey time to show.
//
//   Rev 1.40   Oct 14 2003 19:12:16   kcheung
//Fixed so that the scale is retrieved from the MapControl
//
//   Rev 1.39   Oct 14 2003 18:52:18   kcheung
//Fixed "i" and select enabling after testing with real data.
//
//   Rev 1.38   Oct 13 2003 18:07:38   kcheung
//Fixes
//
//   Rev 1.37   Oct 13 2003 12:43:02   kcheung
//Fixed ALT text
//
//   Rev 1.36   Oct 10 2003 15:14:04   kcheung
//Fixed AddStartEndPoint bug
//
//   Rev 1.35   Oct 10 2003 12:51:50   kcheung
//Alternative help icon added for car journeys.. start/end points added..
//
//   Rev 1.34   Oct 10 2003 11:08:10   PNorell
//Small "fix" to to start the map when ZoomToRoadRoute does not do what it is supposed to.
//
//   Rev 1.33   Oct 09 2003 19:49:18   PNorell
//Corrected Zooming.
//
//   Rev 1.32   Oct 09 2003 18:29:52   kcheung
//Updated to ensure that Map does not display any initial icons and the Map ServerName and ServiceName properties are loaded from Properties service.
//
//   Rev 1.31   Oct 09 2003 13:52:26   kcheung
//Added code to push page id onto stack before transferring to the the location information.
//
//   Rev 1.30   Oct 08 2003 18:11:24   kcheung
//Added SeperatorVisible property which determines if the blue line seperator is visible or not on the Map Tools Control.
//
//   Rev 1.29   Oct 08 2003 17:17:36   PNorell
//Updated to not throw an exception if the journey results does not contain a journey.
//
//   Rev 1.28   Oct 08 2003 13:12:56   kcheung
//Added Map Exception handling code.
//
//   Rev 1.27   Oct 06 2003 15:54:20   kcheung
//Fixed MapToolsControl and MapToolsAcceptedDataControl. JourneyPlannerLocationMap now working properly.
//
//   Rev 1.26   Oct 03 2003 14:53:08   kcheung
//Removed redundant zooming code and oveview map working
//
//   Rev 1.25   Oct 03 2003 11:26:32   passuied
//latest working of printable maps
//
//   Rev 1.24   Oct 02 2003 17:00:30   kcheung
//Updated to write selected map name, url, overview url and scale to session for printer friendly maps.
//
//   Rev 1.23   Oct 02 2003 14:40:32   kcheung
//Updated to get working with del 5 build 2 dll
//
//   Rev 1.22   Oct 02 2003 11:30:02   passuied
//implemented iconSelection storage in sessionmanager
//
//   Rev 1.21   Sep 30 2003 15:27:20   kcheung
//Fixed all HTML
//
//   Rev 1.20   Sep 30 2003 14:23:16   kcheung
//Integrated all HTML stuff
//
//   Rev 1.19   Sep 26 2003 13:06:14   kcheung
//Updated Initialise method of MapToolsControl
//
//   Rev 1.18   Sep 26 2003 12:31:38   kcheung
//Removed selected initialisation - already done in UpdateMap
//
//   Rev 1.17   Sep 26 2003 12:24:40   kcheung
//Fixed Select appearing bug
//
//   Rev 1.16   Sep 26 2003 09:22:08   kcheung
//Fixed initialisation bug for Map Tools Control
//
//   Rev 1.15   Sep 23 2003 17:28:46   kcheung
//Updated
//
//   Rev 1.14   Sep 23 2003 17:11:06   kcheung
//Updated
//
//   Rev 1.13   Sep 22 2003 19:09:50   PNorell
//Added help button to the map control.
//
//   Rev 1.12   Sep 22 2003 18:01:14   kcheung
//Updated
//
//   Rev 1.11   Sep 22 2003 16:54:14   kcheung
//Updated
//
//   Rev 1.10   Sep 21 2003 17:55:50   kcheung
//Updated
//
//   Rev 1.9   Sep 19 2003 20:09:16   kcheung
//Updated
//
//   Rev 1.8   Sep 19 2003 12:22:54   kcheung
//Updated initialise method calls for MapToolsControl
//
//   Rev 1.7   Sep 18 2003 18:05:16   kcheung
//Updated Map_Click to sort the list
//
//   Rev 1.6   Sep 18 2003 16:48:20   kcheung
//Population of drop down from Data Services
//
//   Rev 1.5   Sep 18 2003 12:41:48   kcheung
//Updated
//
//   Rev 1.4   Sep 18 2003 10:42:52   PNorell
//Fixed reference to use interface instead of concrete implimentation.
//
//   Rev 1.3   Sep 17 2003 17:01:20   kcheung
//Updated
//
//   Rev 1.2   Sep 16 2003 18:07:06   kcheung
//Updated
//
//   Rev 1.1   Sep 16 2003 11:17:02   kcheung
//Updated
//
//   Rev 1.0   Sep 15 2003 16:54:06   kcheung
//Initial Revision
//
//   Rev 1.7   Sep 15 2003 16:08:16   kcheung
//Updated
//
//   Rev 1.6   Sep 03 2003 14:37:58   jcotton
//Addition of code for hiding and showing 'To', 'From', 'via', and 'alternate' buttons on the MapToolsControl.
//
//   Rev 1.5   Sep 03 2003 11:27:22   kcheung
//No update.
//
//   Rev 1.4   Aug 27 2003 11:23:18   kcheung
//Updated - working verson
//
//   Rev 1.3   Aug 22 2003 16:14:44   kcheung
//Updated
//
//   Rev 1.2   Aug 22 2003 10:27:48   kcheung
//Updated
//
//   Rev 1.1   Aug 21 2003 14:30:10   kcheung
//Update
//
//   Rev 1.0   Aug 21 2003 11:55:12   kcheung
//Initial Revision


namespace TransportDirect.UserPortal.Web.Controls
{

	#region Using statements
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Collections;
    using System.Collections.Generic;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.HtmlControls;
	using System.Web.UI.WebControls;

	using TransportDirect.Common;
	using TransportDirect.Common.DatabaseInfrastructure;
	using TransportDirect.Common.Logging;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.Presentation.InteractiveMapping;
    using TransportDirect.UserPortal.CyclePlannerControl;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.Web.Support;

	using Logger = System.Diagnostics.Trace;
	using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
	using MapPoint = TransportDirect.Presentation.InteractiveMapping.Point;

	#endregion

	/// <summary>
	///	A custom user control to display a map for a public transport journey.
	/// </summary>

	public partial class JourneyMapControl : TDUserControl
    {
        #region Member variables

        #region Labels
        #endregion

        #region Images
        #endregion


        #region Web-User controls

        protected MapControl theMapControl;
		protected MapKeyControl theMapKeyControl;
		
		protected MapJourneyDisplayDetailsControl theMapJourneyDisplayDetailsControl;
		protected MapLocationIconsSelectControl theMapLocationIconsSelectControl;
		protected MapLocationControl theMapLocationControl;
		protected TransportDirect.UserPortal.Web.Controls.HelpLabelControl journeyMapControlHelpLabel;

        
		#endregion

		protected System.Web.UI.WebControls.Panel carJourneyDetails;

		#region Variables to control state of this control

		/// <summary>Used for output: indicates if the map should render for outward journey or return journey.</summary>
		private bool outward = false;

		/// <summary>Indicates if the CarDetailsTableControl should be visible or not. (Saved in ViewState)</summary>
		private bool directionsVisible = false;

        /// <summary> Indicates if the cycle directions control in parent page should be visibile or not </summary>
        private bool directionsCycleVisible = false;

		/// <summary>Indicates if naptan was found. Used to control visibility of the "select" and "i" button</summary>
		private bool naptanFound = false;
		/// <summary>Indicates if naptan is in range. Used to control visibility of the "select" and "i" button</summary>
		private bool naptanInRange = false;

		private PageId callingPageId = PageId.Empty;

		// flag indicating if map needs to be refreshed
		private bool mapToRefresh = false;
		//flag indicating if map has been updated
		private bool mapUpdated = false;

		// flag used in determining where to obtain journey info - 
		// has user extended journey?
		private bool usingItinerary = false;
		private bool itinerarySegmentSelected = false;

		// flag used to determine whether to re-select the correct journey 
		// segment in the drop down list (i.e. after returning from Help)
		private bool retainSelectedSegmentIndex = false;

		// Constants used when adding location points to the map.
		private const int STARTPOINT = 1;
		private const int ENDPOINT = 2;
		private const int VIAPOINT = 3;

		//Used to get the previous journey type for map symbols
		private bool previousPublic = false;
	
		private int selectedLeg;

		// Flag to specify whether the MapLocationControl 
		// control part of this control should be visible.
		private bool showMapLocationControlTextOnly = false;
		
		// Flag to specify whether to show the title
		// area and associated labels.
		private bool displayTitle = true;

		//Flag to identify whether there is an CJP error 
		//in a VisitPlanner Journey result.
		private bool isError = false;

		#endregion

		protected System.Web.UI.WebControls.Literal literalMapKeyDivBegin;
		protected System.Web.UI.WebControls.Literal literalMapKeyDivEnd;

		MapHelper mapHelper = new MapHelper();

		public event EventHandler InformationRequestedEvent;
		public event EventHandler MoreHelpEvent;

		private const string TOID_PREFIX = "JourneyControl.ToidPrefix";
		private const string MAP_ZOOM = "JourneyDetailsCarSection.Scale";

		private string JourneyDropDown;

		private string railPostFix = string.Empty;
		private string coachPostFix = string.Empty;
		private string railcoachPostFix = string.Empty;

       

		IPropertyProvider properties = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

        TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;

        #endregion

        #region Page Load & OnPreRender methods

        /// <summary>
		/// Page Load method
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//verify that no error has been found
			if (!isError)
			{
                if (outward)
                {
                    labelOptions.Text = Global.tdResourceManager.GetString("MapLocationControl.labelOptions.Text.OutputPublic", TDCultureInfo.CurrentUICulture);
                }
                else
                {
                    labelOptions.Text = Global.tdResourceManager.GetString("MapLocationControl.labelOptions.Text.OutputPublic", TDCultureInfo.CurrentUICulture) ;
                }

				this.usingItinerary = ((TDItineraryManager.Current.Length > 0) 
					&& (!TDItineraryManager.Current.ExtendInProgress));
				this.itinerarySegmentSelected = ((usingItinerary) && (!TDItineraryManager.Current.FullItinerarySelected));

				railPostFix = properties[ "Gazetteerpostfix.rail" ];
				coachPostFix = properties[ "Gazetteerpostfix.coach" ];
				railcoachPostFix = properties[ "Gazetteerpostfix.railcoach" ];

                // Set the modes used in the display to retrieve journeys from result object, 
                // set by city to city otherwise will be null
                if (TDSessionManager.Current.FindPageState != null)
                    modeTypes = TDSessionManager.Current.FindPageState.ModeType;

				//If this is the first viewing of the page and not a "post back"
                if ((!IsPostBack))
				{
					//We have not been to this page yet so set the previous journey index
					//to -1
					if ( outward )
						TDSessionManager.Current.InputPageState.PreviousOutwardJourney = -1;
					else
						TDSessionManager.Current.InputPageState.PreviousReturnJourney = -1;

					

					#region Initialise labels and drop downs
					// Initialise labels from Resource manager
					
					labelCurrentLocation.Text = Global.tdResourceManager.GetString(
						"JourneyMapControl.labelCurrentLocation.Text", TDCultureInfo.CurrentUICulture);

					labelMaps.Text = Global.tdResourceManager.GetString(
						"JourneyMapControl.labelMaps.Text", TDCultureInfo.CurrentUICulture);

					labelTotalDistance.Text = Global.tdResourceManager.GetString(
						"JourneyMapControl.labelTotalDistance", TDCultureInfo.CurrentUICulture);

					labelTotalDuration.Text = Global.tdResourceManager.GetString(
						"JourneyMapControl.labelTotalDuration", TDCultureInfo.CurrentUICulture);

                    labelOverviewMap.Text = Global.tdResourceManager.GetString(
                        "JourneyMapControl.labelOverviewMap", TDCultureInfo.CurrentUICulture);

					if(TDSessionManager.Current.FindAMode == FindAMode.Car) 
					{
						labelMapsCar.Visible = false;
						HelpControlMapKey.Visible = true;
					}
					else 
					{
						labelMapsCar.Visible = true;
						labelMapsCar.Text = "(" + Global.tdResourceManager.GetString(
							"JourneyMapControl.labelCar", TDCultureInfo.CurrentUICulture) + ")";
						HelpControlMapKey.Visible = false;
					}

					// By Default, Show Directions is showing.
					buttonDirections.Text = Global.tdResourceManager.GetString(
						"JourneyMapControl.ButtonDirectionsShow.Text", TDCultureInfo.CurrentUICulture);

                    buttonDirectionsCycle.Text = Global.tdResourceManager.GetString(
                        "JourneyMapControl.ButtonDirectionsShow.Text", TDCultureInfo.CurrentUICulture);

					#endregion

				
					// Initialise Web User Controls
                    imageSummaryMap.AlternateText = Global.tdResourceManager.GetString(
                        "JourneyMapControl.imageSummaryMap.AlternateText", TDCultureInfo.CurrentUICulture);
					
					labelMapSymbols.Text = Global.tdResourceManager.GetString(
						"panelMapLocationSelect.labelMapSymbols", TDCultureInfo.CurrentUICulture);

					labelKey.Text = Global.tdResourceManager.GetString(
						"MapKeyControl.labelKey", TDCultureInfo.CurrentUICulture);

                    // Set the disclaimer text (should only be set up for a white label partner)
                    labelMapSymbolsDisclaimer.Text = Global.tdResourceManager.GetString(
                        "panelMapLocationSelect.labelMapSymbolsDisclaimer", TDCultureInfo.CurrentUICulture);

					// Initialise the selected map text for the printer friendly
					// map to the string stating map of the full journey.
					ResetSelectedMapTextInSession();

                    SetPrinterDirectionsVisible(false);

					

					
					if (mapHelper.PublicOutwardJourney)
						this.previousPublic = true;

				}
			}
		}

		/// <summary>
		/// OnPreRender method - refreshes controls and calls the
		/// base OnPreRender.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			usingItinerary = ((TDItineraryManager.Current.Length > 0) 
				&& (!TDItineraryManager.Current.ExtendInProgress));
			itinerarySegmentSelected = ((usingItinerary) && (!TDItineraryManager.Current.FullItinerarySelected));
			
			SetControlVisibilities();

			buttonDirections.Visible = DirectionsButtonVisible;// directionsVisible;
            buttonDirectionsCycle.Visible = DirectionsCycleButtonVisible; //directionsCycleVisible        

            //We need to find out if it is a car route map, then we restrict the 
			//transport symbols that are displayed by default

			if (mapHelper.PrivateOutwardJourney || mapHelper.PrivateReturnJourney)
			{

                if ((!IsPostBack) && TDSessionManager.Current.GetOneUseKey(SessionKey.IndirectLocationPostBack) == null)
				{
					theMapLocationIconsSelectControl.TransportSectionCar();
				}
				
				else if(TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack) != null )
				{
					theMapLocationIconsSelectControl.TransportSectionCar();
					mapToRefresh = true;
				}
			}

			if( TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack ) != null && Visible )
			{
				// Get map data
				// Inject it
				try
				{	
					// Need to select the right data
					theMapControl.Map.InjectViewState(TDSessionManager.Current.StoredMapViewState[ outward ? TDSessionManager.OUTWARDMAP : TDSessionManager.RETURNMAP] );
					// set flag so that selected segment in drop down is repopulated.
					this.retainSelectedSegmentIndex = true;
				}
				catch( MapExceptionGeneral exc)
				{
					Logger.Write( new OperationalEvent( TDEventCategory.ThirdParty, TDTraceLevel.Warning,exc.Message +"\nStacktrace:\n"+exc.StackTrace));
				}
				
			}
				

			// Need to update map if have added/removed items from Itinerary, 
			// but not if it has already been updated ( if selected index has 
			// changed then UpdateMapDisplay is called from parent page).
			if (TDItineraryManager.Current.ItineraryManagerModeChanged && !mapUpdated)
			{
				Journey[] aJourneys = getCurrentJourneys();
				UpdateMap(aJourneys);
				mapToRefresh = true;
			}

			if (theMapJourneyDisplayDetailsControl.DropDownListJourneySegment.SelectedIndex == -1)
			{
				// Dropdown can lose items if back button is pressed therefore look for empty dropdown and refresh if so
			}

			//boolean representing if the journey display details control needs to be updated.
			TDJourneyViewState viewstate = TDItineraryManager.Current.JourneyViewState;

			if (TDItineraryManager.Current.FullItinerarySelected )
			{
				// Check if we are dealing with Visit Planner results - has own key
				if (TDSessionManager.Current.ItineraryMode == ItineraryManagerMode.VisitPlanner)
					theMapKeyControl.InitialiseVisitPlan(false, mapHelper.HasJourneyGreyedOutMode(outward));
				else
					theMapKeyControl.InitialiseMixed(false, mapHelper.HasJourneyGreyedOutMode(outward));
				theMapJourneyDisplayDetailsControl.UsingItinerary = true;
			}
			else
			{
				theMapJourneyDisplayDetailsControl.UsingItinerary = usingItinerary;
				
				if ( outward ) 
				{
					//This is the outward journey so set the state of the journey details control
					theMapJourneyDisplayDetailsControl.PublicJourney = mapHelper.PublicOutwardJourney;
					
					if	(!TDSessionManager.Current.IsFindAMode)
					{
						if ( mapHelper.PublicOutwardJourney )
						{
                            theMapKeyControl.InitialisePublic( false, mapHelper.HasJourneyGreyedOutMode(outward) );
                            HelpControlMapKey.Visible = false;
						}
						else
						{
                            theMapKeyControl.InitialisePrivate(false, false);
                            HelpControlMapKey.Visible = true;
						}
					}
					else
					{
						if	(TDSessionManager.Current.FindPageState.Mode == FindAMode.Car) 
						{
							theMapKeyControl.InitialisePrivate ( false, false ) ;
							HelpControlMapKey.Visible = true;
						}
                        else if (TDSessionManager.Current.FindPageState.Mode == FindAMode.Cycle)
                        {
                            theMapKeyControl.InitialiseCycle(false);
                            HelpControlMapKey.Visible = false;
                        }
                        else if (TDSessionManager.Current.FindPageState.Mode == FindAMode.EnvironmentalBenefits)
                        {
                             theMapKeyControl.InitialiseEBC(false);
							    
                        }
                        else
                        {
                            theMapKeyControl.InitialiseSpecificModes(mapHelper.FindUsedModes(true, usingItinerary), false, mapHelper.HasJourneyGreyedOutMode(outward));
                            HelpControlMapKey.Visible = false;
                        }
					}

					if ( TDSessionManager.Current.InputPageState.PreviousOutwardJourney != viewstate.SelectedOutwardJourney || usingItinerary) 
					{
						TDSessionManager.Current.InputPageState.PreviousOutwardJourney = viewstate.SelectedOutwardJourney;
					}
				}
				else 
				{
					//This is the return journey so set the state of the journey details control
					theMapJourneyDisplayDetailsControl.PublicJourney = mapHelper.PublicReturnJourney;
					
					if	(!TDSessionManager.Current.IsFindAMode)
					{
						if ( mapHelper.PublicReturnJourney )
						{
							theMapKeyControl.InitialisePublic( false, mapHelper.HasJourneyGreyedOutMode(outward) );
							HelpControlMapKey.Visible = false;
						}
						else
						{
							theMapKeyControl.InitialisePrivate( false, false );
							HelpControlMapKey.Visible = true;
						}
					}
					else
					{
						if	(TDSessionManager.Current.FindPageState.Mode == FindAMode.Car) 
						{
							theMapKeyControl.InitialisePrivate ( false, false ) ;
							HelpControlMapKey.Visible = true;
						}
						else if(TDSessionManager.Current.FindPageState.Mode == FindAMode.Cycle)
                        {
                            theMapKeyControl.InitialiseCycle(false);
                            HelpControlMapKey.Visible = false;
                        }
                        else
						{
							theMapKeyControl.InitialiseSpecificModes(mapHelper.FindUsedModes(false, usingItinerary), false, mapHelper.HasJourneyGreyedOutMode(outward));
							HelpControlMapKey.Visible = false;
						}
					}

					if ( TDSessionManager.Current.InputPageState.PreviousReturnJourney != viewstate.SelectedReturnJourney || usingItinerary) 
					{
						TDSessionManager.Current.InputPageState.PreviousReturnJourney = viewstate.SelectedReturnJourney;
					}
				}
			}

			// Determine to see if it is necessary to update the drop down list of available maps.
			// Only need to update the list if the selected journey has changed.

                // clear the drop down list and repopulate
				if (usingItinerary && !itinerarySegmentSelected)
				{
					theMapJourneyDisplayDetailsControl.PopulateJourneySegments ( outward , usingItinerary , TDItineraryManager.Current.Length );
				}
				else
				{
					theMapJourneyDisplayDetailsControl.PopulateJourneySegments ( outward , usingItinerary , 1 );
				}
            

			// Determine whether we need to select the correct segment in the drop down, 
			// e.g. if returning from More Help...
			if (retainSelectedSegmentIndex)
			{
				theMapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex = 
					outward ?	TDSessionManager.Current.JourneyMapState.SelectedJourneySegment
					:	TDSessionManager.Current.ReturnJourneyMapState.SelectedJourneySegment;
			}

			if ( this.labelSelectedLocation.Text.Equals( string.Empty ) )
			{
				this.labelCurrentLocation.Visible = false;
				this.labelSelectedLocation.Visible = false;
			} 
			else 
			{
				this.labelCurrentLocation.Visible = true;
				this.labelSelectedLocation.Visible = true;
			}

            if ((!IsPostBack) 
				&& !usingItinerary
				&& (TDSessionManager.Current.GetOneUseKey( SessionKey.IndirectLocationPostBack ) == null )
				&& !TDItineraryManager.Current.ExtendInProgress)
			{
				//If this is a private journey update the map control to
				//show adjusted road data
				Journey[] aJourneys = getCurrentJourneys();
                bool isRoad = aJourneys[0] is JourneyControl.RoadJourney;
                if (isRoad)
				{
					UpdateMap( aJourneys[0] );
					UpdateRoadJourneyControls();
				}
			}
			
			// If we need the map to show a selected leg then the SelectedLeg property will have been set.
			// Leg 1 equates to a dropdown index of 1 (and 2=2, etc)
			 if (selectedLeg > 0)
			{
				if(hasPublic() || journeyDropDown == null)
				{
					// Shows the selected leg and sets the equivalent dropdown value
					ShowSelectedMapLeg(selectedLeg);
				}
				else
				{
					// Shows the selected direction and sets the equivalent dropdown value
					ShowSelectedCarMapLeg(journeyDropDown);
				}
			}

			
			if ( theMapLocationIconsSelectControl.IsEnabled )
				IconsSelectRefresh();
			else
				ResetStopPointXVisible();


			if (mapToRefresh)
				theMapControl.Map.Refresh();

			base.OnPreRender(e);
		}

		#endregion

		#region Initialisation method

		/// <summary>
		/// Initialises this control - used for output.
		/// </summary>
		/// <param name="outward">If true, map for outward journey is rendered
		/// otherwise map for return journey is rendered.</param>
		public void Initialise(bool outward, PageId callingPageId)
		{
			this.outward = outward;
			this.callingPageId = callingPageId;
		}

		#endregion

		#region Methods to update controls

		/// <summary>
		/// Sets the visibility of controls and sets dynamic text
		/// depending on what the current selected item is.
		/// </summary>
		public void SetControlVisibilities()
		{
			SharedControlVisibility(false);
			PublicTransportControlVisibility( false );
			CarControlVisibility( false );
			mapTitleArea.Visible = displayTitle;
            //Environment benefits control visibility
            EBCControlVisibility();

           // Get TDJourneyViewState and TDJourneyResult from TDSessionManager
			TDJourneyViewState viewstate = TDItineraryManager.Current.JourneyViewState;
			ITDJourneyResult journeyResult = TDItineraryManager.Current.JourneyResult;
            ITDCyclePlannerResult cycleResult = TDSessionManager.Current.CycleResult;
			
			if (usingItinerary && !itinerarySegmentSelected)
			{
				// Map is showing whole itinerary i.e. multiple journeys
				UpdateItineraryControls();
			}
			else
			{
				if(outward)
				{			
					// Required to render the map for the outward journey.
                    int numberOfJourneys = 0;
                    if (journeyResult != null)
                    {
                        numberOfJourneys += journeyResult.OutwardPublicJourneyCount;
                        numberOfJourneys += journeyResult.OutwardRoadJourneyCount;
                    }
                    if (cycleResult != null)
                        numberOfJourneys += cycleResult.OutwardCycleJourneyCount;

                    if (numberOfJourneys < 1)
                        return;

					// Find out what type the selected outward journey is.
					if( viewstate.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
					{	
						int journeyIndex = viewstate.SelectedOutwardJourneyID;

						// Update controls that are common to both public and car journeys.
						UpdateCommonControls(journeyResult.OutwardPublicJourney(journeyIndex));

						// A public journey was selected. Call method to render
						// controls for public journey
						UpdatePublicJourneyControls();
					}
					else if( viewstate.SelectedOutwardJourneyType == TDJourneyType.PublicAmended )
					{
						// Update controls that are common to both public and car journeys.
						UpdateCommonControls(journeyResult.AmendedOutwardPublicJourney);

						// A public journey was selected. Call method to render
						// controls for public journey
						UpdatePublicJourneyControls();
					}
                    else if (viewstate.SelectedOutwardJourneyType == TDJourneyType.Cycle)
                    {
                        // Update controls that are common to all journeys
                        UpdateCommonControls(cycleResult.OutwardCycleJourney());

                        // A cycle journey was selected. call method to render controls for cycle journey
                        UpdateCycleJourneyControls();
                    }
					else
					{
						// Update controls that are common to both public and car journeys.
						UpdateCommonControls
							(journeyResult.OutwardRoadJourney());


						// A road journey was selected. Call method to render
						// controls for road journey.
						UpdateRoadJourneyControls();
					}
				}
				else
				{
					// Required to render the map for the return journey.
                    int numberOfJourneys = 0;
                    if (journeyResult != null)
                    {
                        numberOfJourneys += journeyResult.ReturnPublicJourneyCount;
                        numberOfJourneys += journeyResult.ReturnRoadJourneyCount;
                    }
                    if (cycleResult != null)
                        numberOfJourneys += cycleResult.ReturnCycleJourneyCount;

                    if (numberOfJourneys < 1)
                        return;
				
					// Find out what type the selected outward journey is.
					if( viewstate.SelectedReturnJourneyType == TDJourneyType.PublicOriginal)
					{	
						int journeyIndex = viewstate.SelectedReturnJourneyID;

						// Update controls that are common to both public and car journeys.
						UpdateCommonControls(journeyResult.ReturnPublicJourney(journeyIndex));

						// A public journey was selected. Call method to render
						// controls for public journey
						UpdatePublicJourneyControls();
					
					}
					else if(viewstate.SelectedReturnJourneyType == TDJourneyType.PublicAmended )
					{
						// Update controls that are common to both public and car journeys.
						UpdateCommonControls(journeyResult.AmendedReturnPublicJourney);

						// A public journey was selected. Call method to render
						// controls for public journey
						UpdatePublicJourneyControls();
					}
                    else if (viewstate.SelectedReturnJourneyType == TDJourneyType.Cycle)
                    {
                        // Update controls that are common to all journeys
                        UpdateCommonControls(cycleResult.ReturnCycleJourney());

                        // A cycle journey was selected. call method to render controls for cycle journey
                        UpdateCycleJourneyControls();
                    }
					else
					{
						// Update controls that are common to both public and car journeys.
						UpdateCommonControls(journeyResult.ReturnRoadJourney());

						// A road journey was selected. Call method to render
						// controls for road journey.
						UpdateRoadJourneyControls();
					}
				}
			}
		}

		/// <summary>
		/// Updates controls that are common to both public and road journeys.
		/// </summary>
		/// <param name="journey">Journey that is being rendered.</param>
		public void UpdateCommonControls(Journey journey)
		{

			// Get TDJourneyResult and TDViewState from TDSessionManager
			ITDJourneyResult result = TDItineraryManager.Current.JourneyResult;
            ITDCyclePlannerResult cycleResult = TDSessionManager.Current.CycleResult;
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;

			JourneySummaryLine summaryLine;

			if (usingItinerary)
			{
				labelDisplayNumber.Text = outward ? TDItineraryManager.Current.OutwardDisplayNumber.ToString() 
					: TDItineraryManager.Current.ReturnDisplayNumber.ToString();
			}
			else
			{
				// Get the selected public journey
				if(outward)
				{
					int selectedIndex = viewState.SelectedOutwardJourney;
					bool arriveBefore = viewState.JourneyLeavingTimeSearchType;
                    summaryLine = (TDSessionManager.Current.FindAMode == FindAMode.Cycle) ?
                        cycleResult.OutwardJourneySummary(arriveBefore, modeTypes)[selectedIndex] :
                        result.OutwardJourneySummary(arriveBefore, modeTypes)[selectedIndex];
				}
				else
				{
					int selectedIndex = viewState.SelectedReturnJourney;
					bool arriveBefore = viewState.JourneyReturningTimeSearchType;
                    summaryLine = (TDSessionManager.Current.FindAMode == FindAMode.Cycle) ?
                        cycleResult.ReturnJourneySummary(arriveBefore, modeTypes)[selectedIndex] :
                        result.ReturnJourneySummary(arriveBefore, modeTypes)[selectedIndex];
				}

				// Set dynamic text labels
				if(TDSessionManager.Current.FindAMode != FindAMode.None)
				{
					labelDisplayNumber.Text = string.Empty;
				}
				else 
				{
					labelDisplayNumber.Text = summaryLine.DisplayNumber;
				}
			}

			if(outward)
				labelJourney.Text = Global.tdResourceManager.GetString(
					"JourneyMapControl.labelOutwardJourney", TDCultureInfo.CurrentUICulture);
			else
				labelJourney.Text = Global.tdResourceManager.GetString(
					"JourneyMapControl.labelReturnJourney", TDCultureInfo.CurrentUICulture);

			// Update the Map Control to display map of the public journey.
			// Needs to called only if the page is not a postback otherwise
			// the button handlers will update the map approriately.

            if ((!IsPostBack) && TDSessionManager.Current.GetOneUseKey(SessionKey.IndirectLocationPostBack) == null)
			{
				UpdateMap(journey);

				// Ensure that no points are visible on the map
				// Reset the PointX and Stops on the map.
				ResetStopPointXVisible();
			}
		}

		#endregion

		#region Method to reset Stop and PointX on the map

		/// <summary>
		/// Resets the Stops and PointX hashtable so that no icons
		/// are displayed on the map.
		/// </summary>
		private void ResetStopPointXVisible()
		{
			try
			{
				bool change = false;
				// Ensure that the no keys are visible on the map.
				// Create a new StopsVisible Hashtable
				Hashtable stopsVisible = new Hashtable();
				Hashtable pointXVisible = new Hashtable();
				
				foreach(string stopKey in theMapControl.Map.StopsVisible.Keys)
				{
					if( (bool)theMapControl.Map.StopsVisible[stopKey] )
					{
						stopsVisible[stopKey] = false;
						change = true;
					}
				}

				foreach(string pointXKey in theMapControl.Map.PointXVisible.Keys)
				{
					if( (bool)theMapControl.Map.PointXVisible[pointXKey] )
					{
						pointXVisible[pointXKey] = false;
						change = true;
					}
				}
				if( change )
				{
					theMapControl.Map.StopsVisible = stopsVisible;
					theMapControl.Map.PointXVisible = pointXVisible;
					mapToRefresh = true;
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

			// Reset icon selection for printable page
			if (outward)
				TDSessionManager.Current.InputPageState.ResetIconSelectionOutward();
			else
				TDSessionManager.Current.InputPageState.ResetIconSelectionReturn();

		}

		#endregion

        #region Update controls and visibility

        /// <summary>
		/// Updates all controls to render if showing full itinerary (multiple journeys).
		/// </summary>
		public void UpdateItineraryControls()
		{
			SharedControlVisibility(true);
			PublicTransportControlVisibility(false);
			CarControlVisibility(false);

            // Road and Cycle Controls
			buttonDirections.Visible = false;
            buttonDirectionsCycle.Visible = false;
			labelTotalDistance.Visible = false;
			labelTotalMiles.Visible = false;
			labelTotalDuration.Visible = false;
			labelTotalTime.Visible = false;
            travelInfoDiv.Visible = false;

			if(outward)
				labelJourney.Text = Global.tdResourceManager.GetString(
					"JourneyMapControl.labelOutwardFullItinerary", TDCultureInfo.CurrentUICulture);
			else
				labelJourney.Text = Global.tdResourceManager.GetString(
					"JourneyMapControl.labelReturnFullItinerary", TDCultureInfo.CurrentUICulture);
			
			labelDisplayNumber.Text = "";

			// Update the Map Control to display map of the whole itinerary.
			// Needs to called only if the page is not a postback otherwise
			// the button handlers will update the map approriately.
            if ((!IsPostBack) && TDSessionManager.Current.GetOneUseKey(SessionKey.IndirectLocationPostBack) == null)
			{
				Journey[] aJourneys = getCurrentJourneys();
				UpdateMap(aJourneys);
				// Ensure that no points are visible on the map
				// Reset the PointX and Stops on the map.
				ResetStopPointXVisible();
			}
		}

		/// <summary>
		/// Updates all controls to render for the selected public journey.
		/// </summary>
		public void UpdatePublicJourneyControls()
		{
			SharedControlVisibility(true);
			PublicTransportControlVisibility(true);
			CarControlVisibility(false);
		}

		/// <summary>
		/// Method to set visibility of common controls.
		/// </summary>
		private void SharedControlVisibility( bool visible )
		{
			// Set visibility of all controls that are common to 
			// both public journey and car journeys
			labelJourney.Visible = visible;
            if ((TDSessionManager.Current.FindAMode == FindAMode.Car) ||
                (TDSessionManager.Current.FindAMode == FindAMode.Cycle))
			{
				labelDisplayNumber.Visible = false;
			}
			else
			{
				labelDisplayNumber.Visible = visible;
			}
			theMapControl.Visible = visible;
			
		}

		/// <summary>
		/// Method to set visibility of public journey controls.
		/// </summary>
		private void PublicTransportControlVisibility(bool visible)
		{

			if (usingItinerary)
				journeySegmentHelpCustomControl.HelpLabel = journeyMapControlItineraryHelpLabel.ID;
			else
			{
				if ( visible )
					journeySegmentHelpCustomControl.HelpLabel = journeyMapControlPublicHelpLabel.ID;
				else
					journeySegmentHelpCustomControl.HelpLabel = journeyMapControlPrivateHelpLabel.ID;
			}
		}

		/// <summary>
		/// Method to set visibility of car journey controls.
		/// </summary>
		private void CarControlVisibility(bool visible)
		{
			// Set visibilty of all controls that are applicable to road
			// journeys to false

			if(TDSessionManager.Current.FindAMode == FindAMode.Car) 
			{
				labelMapsCar.Visible = false;
			}
			else 
			{
				labelMapsCar.Visible = visible;
			}

			labelTotalDistance.Visible = visible;
			labelTotalMiles.Visible = visible;
			labelTotalDuration.Visible = visible;
			labelTotalTime.Visible = visible;
            travelInfoDiv.Visible = visible;

			if (usingItinerary)
				journeySegmentHelpCustomControl.HelpLabel = journeyMapControlItineraryHelpLabel.ID;
			else
			{
				if ( visible )
					journeySegmentHelpCustomControl.HelpLabel = journeyMapControlPrivateHelpLabel.ID;
				else
					journeySegmentHelpCustomControl.HelpLabel = journeyMapControlPublicHelpLabel.ID;
			}
		}

        /// <summary>
		/// Method to set visibility of EBC journey controls.
		/// </summary>
        private void EBCControlVisibility()
        {
            if (TDSessionManager.Current.FindAMode == FindAMode.EnvironmentalBenefits)
            {
                mapTitleArea.Visible = false;
                travelInfoDiv.Visible = false;
            }
        }

		/// <summary>
		/// Update controls that are applicable to road journeys.
		/// </summary>
		/// <param name="journey">Journey being rendered.</param>
		public void UpdateRoadJourneyControls()
		{
			SharedControlVisibility(true);
			PublicTransportControlVisibility( false );
			CarControlVisibility(true);
            EBCControlVisibility();

			// ----------------------------------------------------------

			// Get the total time and duration for the selected car journey.
			// First, determine which journey has been selected

			RoadJourney selectedRoadJourney;
			ITDJourneyResult journeyResult = TDItineraryManager.Current.JourneyResult;

			if(outward)
				selectedRoadJourney = journeyResult.OutwardRoadJourney();
			else
				selectedRoadJourney = journeyResult.ReturnRoadJourney();

			long distance = selectedRoadJourney.TotalDistance;

			// Convert the distance to miles using the conversion factor
			
			// Retrieve the conversion factor from the Properties Service.
			double conversionFactor =
				Convert.ToDouble(Properties.Current["Web.Controls.MileageConverter"], TDCultureInfo.CurrentCulture.NumberFormat);

			double result = (double)distance/ conversionFactor;

			// Only want the string to have 1 decimal place - chop off everything
			// after that.

			labelTotalMiles.Text = result.ToString("F1", TDCultureInfo.CurrentCulture.NumberFormat)
				+ " " + Global.tdResourceManager.GetString("JourneyMapControl.labelMiles", TDCultureInfo.CurrentUICulture);

			// Now get the duration
			double time = (double)selectedRoadJourney.TotalDuration  / 60.0; // in minutes
			time = Round(time);
			// Convert to hours and minutes for display
			long hours = (long)time / 60;
			long minutes = (long)time % 60;

			labelTotalTime.Text = hours.ToString(TDCultureInfo.CurrentUICulture.NumberFormat)
				+ " " + Global.tdResourceManager.GetString("JourneyMapControl.labelHours", TDCultureInfo.CurrentUICulture)
				+ " " + minutes.ToString(TDCultureInfo.CurrentUICulture.NumberFormat)
				+ " " + Global.tdResourceManager.GetString("JourneyMapControl.labelMinutes", TDCultureInfo.CurrentUICulture);

		}

        /// <summary>
        /// Update controls that are applicable to cycle journeys.
        /// </summary>
        public void UpdateCycleJourneyControls()
        {
            SharedControlVisibility(true);
            PublicTransportControlVisibility(false);
            CarControlVisibility(false);

            #region Set Distance and Time labels
            // Display the div containing the labels
            travelInfoDiv.Visible = true;
            travelInfoLabelsDiv.Visible = true && DirectionsCycleButtonVisible; // Ensures parent page controls visibility
            labelTotalDistance.Visible = true;
            labelTotalMiles.Visible = true;
            labelTotalDuration.Visible = true;
            labelTotalTime.Visible = true;
            buttonDirectionsCycle.Visible = true && DirectionsCycleButtonVisible; // Ensures parent page controls visibility

            // Get the total time and duration for the selected cycle journey.
            CycleJourney selectedCycleJourney;
            ITDCyclePlannerResult journeyResult = TDSessionManager.Current.CycleResult;

            if (outward)
                selectedCycleJourney = journeyResult.OutwardCycleJourney();
            else
                selectedCycleJourney = journeyResult.ReturnCycleJourney();

            int distance = selectedCycleJourney.TotalDistance;

            // Convert the distance to miles using the conversion factor
            string resultMileage = MeasurementConversion.Convert((double)distance, ConversionType.MetresToMileage);
            if (string.IsNullOrEmpty(resultMileage))
                resultMileage = "0";

            double result = Convert.ToDouble(resultMileage);

            // Only want the string to have 1 decimal place - chop off everything
            // after that.
            labelTotalMiles.Text = result.ToString("F1", TDCultureInfo.CurrentCulture.NumberFormat)
                + " " + Global.tdResourceManager.GetString("JourneyMapControl.labelMiles", TDCultureInfo.CurrentUICulture);

            // Now get the duration
            double time = (double)selectedCycleJourney.TotalDuration / 60.0; // in minutes
            time = Round(time);

            // Convert to hours and minutes for display
            long hours = (long)time / 60;
            long minutes = (long)time % 60;

            labelTotalTime.Text = hours.ToString(TDCultureInfo.CurrentUICulture.NumberFormat)
                + " " + Global.tdResourceManager.GetString("JourneyMapControl.labelHours", TDCultureInfo.CurrentUICulture)
                + " " + minutes.ToString(TDCultureInfo.CurrentUICulture.NumberFormat)
                + " " + Global.tdResourceManager.GetString("JourneyMapControl.labelMinutes", TDCultureInfo.CurrentUICulture);

            #endregion
        }

		/// <summary>
		/// Returns a boolean value indicating whether a journey has a public transport element to it.
		/// Actually looks at the whole itinerary (if there are several journeys) and if any public transport
		/// element exists then we need to treat whole journey as a public transport journey.
		/// </summary>
		/// <returns></returns>
		private bool hasPublic()
		{
			// Get the journeys in the itinerary (or session if no itinerary)
			Journey[] aJourneys = getCurrentJourneys();
			bool foundPublic = false;
			// Examine each journey to see if it is public. As soon as we have a public journey element
			// we can stop and return that info.
			foreach (JourneyControl.Journey journey in aJourneys)
			{
				if (journey is JourneyControl.PublicJourney)
				{
					foundPublic = true;
					break;
				}
			}
			return foundPublic;
        }

        #endregion


        #region Method to update the display of the map component.

        /// <summary>
		/// Updates the display of the Map.
		/// </summary>
		private void UpdateMap(Journey journey)
		{
			// Need to convert journey into array for UpdateMap
			Journey[] aJourneys = new Journey[1];
			aJourneys[0] = journey;
			UpdateMap(aJourneys);
		}

		/// <summary>
		/// Updates the display of the Map.
		/// </summary>
		private void UpdateMap(Journey[] aJourneys)
		{
			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			ITDJourneyRequest request;
			JourneyLeg[] journeyLegs;
			JourneyControl.PublicJourney publicJourney;
			JourneyControl.RoadJourney roadJourney;
            CyclePlannerControl.CycleJourney cycleJourney;
			RoadJourneyDetail currentRoadDetail;
            
			int noOfJourneys = aJourneys.Length;
			int startEasting = 0;
			int startNorthing = 0;
			int endEasting = 0;
			int endNorthing = 0;
			string startDescription = string.Empty;
			string endDescription = string.Empty;
			int startType = 0;
			int endType = 0;
			MapPoint iconMapPoint;
			Double xcord;
			Double ycord;

			// Reset the route displayed on the map
			ClearMapRoutes();


			try
			{
				theMapControl.Map.ClearAddedSymbols();

				for (int i=0; i<noOfJourneys; i++)
                {
                    #region Determine start/end coordinates and description
                    journeyLegs = aJourneys[i].JourneyLegs;

					if (!usingItinerary || itinerarySegmentSelected)
                        SetPrinterDirectionsVisible(false);

					if	(journeyLegs[0].LegStart.Location.GridReference != null 
						&& journeyLegs[0].LegStart.Location.GridReference.Easting > 0 
						&& journeyLegs[0].LegStart.Location.GridReference.Northing > 0)
					{	
						startEasting = journeyLegs[0].LegStart.Location.GridReference.Easting;
						startNorthing = journeyLegs[0].LegStart.Location.GridReference.Northing;
						startDescription = journeyLegs[0].LegStart.Location.Description;
					}
					else
					{
						if (sessionManager.ItineraryMode == ItineraryManagerMode.Replan)
						{
							if (i == 0)
							{
								if (outward)
								{
									startEasting = ((ReplanItineraryManager)itineraryManager).OriginalRequest.OriginLocation.GridReference.Easting;
									startNorthing = ((ReplanItineraryManager)itineraryManager).OriginalRequest.OriginLocation.GridReference.Northing;
									startDescription = ((ReplanItineraryManager)itineraryManager).OriginalRequest.OriginLocation.Description;
								}
								else
								{
									startEasting = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnOriginLocation.GridReference.Easting;
									startNorthing = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnOriginLocation.GridReference.Northing;
									startDescription = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnOriginLocation.Description;
								}
							}
							else
							{
								startEasting = 0;
								startNorthing = 0;
								startDescription = journeyLegs[0].LegStart.Location.Description;
							}
						}
						else
						{
							request = itineraryManager.JourneyRequest;

							if (outward)
							{
								startEasting = request.OriginLocation.GridReference.Easting;
								startNorthing = request.OriginLocation.GridReference.Northing;
								startDescription = request.OriginLocation.Description;
							}
							else
							{
								startEasting = request.ReturnOriginLocation.GridReference.Easting;
								startNorthing = request.ReturnOriginLocation.GridReference.Northing;
								startDescription = request.ReturnOriginLocation.Description;
							}
						}
					}

					if	(journeyLegs[journeyLegs.Length - 1].LegEnd.Location.GridReference != null 
						&& journeyLegs[journeyLegs.Length - 1].LegEnd.Location.GridReference.Easting > 0 
						&& journeyLegs[journeyLegs.Length - 1].LegEnd.Location.GridReference.Northing > 0)
					{	
						endEasting = journeyLegs[journeyLegs.Length - 1].LegEnd.Location.GridReference.Easting;
						endNorthing = journeyLegs[journeyLegs.Length - 1].LegEnd.Location.GridReference.Northing;
						endDescription = journeyLegs[journeyLegs.Length - 1].LegEnd.Location.Description;
					}
					else
					{
						if (sessionManager.ItineraryMode == ItineraryManagerMode.Replan)
						{
							if (i == (noOfJourneys - 1))
							{
								if (outward)
								{
									endEasting = ((ReplanItineraryManager)itineraryManager).OriginalRequest.DestinationLocation.GridReference.Easting;
									endNorthing = ((ReplanItineraryManager)itineraryManager).OriginalRequest.DestinationLocation.GridReference.Northing;
									endDescription = ((ReplanItineraryManager)itineraryManager).OriginalRequest.DestinationLocation.Description;
								}
								else
								{
									endEasting = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnDestinationLocation.GridReference.Easting;
									endNorthing = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnDestinationLocation.GridReference.Northing;
									endDescription = ((ReplanItineraryManager)itineraryManager).OriginalRequest.ReturnDestinationLocation.Description;
								}
							}
							else
							{
								endEasting = 0;
								endNorthing = 0;
								endDescription = journeyLegs[journeyLegs.Length - 1].LegEnd.Location.Description;
							}
						}
						else
						{
							request = itineraryManager.JourneyRequest;

							if (outward)
							{
								endEasting = request.DestinationLocation.GridReference.Easting;
								endNorthing = request.DestinationLocation.GridReference.Northing;
								endDescription = request.DestinationLocation.Description;
							}
							else
							{
								endEasting = request.ReturnDestinationLocation.GridReference.Easting;
								endNorthing = request.ReturnDestinationLocation.GridReference.Northing;
								endDescription = request.ReturnDestinationLocation.Description;
							}
						}
                    }
                    #endregion

                    if (aJourneys[i] is JourneyControl.PublicJourney)
                    {
                        #region Add PT Journey to map
                        publicJourney = (JourneyControl.PublicJourney)aJourneys[i];

						// Add the route to the map.
						theMapControl.Map.AddPTRoute( Session.SessionID, publicJourney.RouteNum );

						// Add location points to map.

						JourneyLeg journeyLegFirst = publicJourney.JourneyLegs[0];
						JourneyLeg journeyLegLast = publicJourney.JourneyLegs[publicJourney.JourneyLegs.Length - 1];

						//Car Park outward and return journey
						if ((journeyLegLast.LegEnd.Location.CarParking != null) 
							||(journeyLegFirst.LegStart.Location.CarParking != null) )
						{
							// Car Park journey can use Map, Entrance, or Exit coordinates to 
							// plan a journey From/To. We always want to display the Map coordinate 
							// as the Start/End point on the map

							// Add location points to map, for start or via
							startType = (i==0) ? STARTPOINT : VIAPOINT;

							// Scenario where user has planned From a Car Park
							if (journeyLegFirst.LegStart.Location.CarParking != null)
							{
								CarPark carPark = journeyLegFirst.LegStart.Location.CarParking;
								OSGridReference gridreference = new OSGridReference();
								gridreference = carPark.GetMapGridReference();
								AddStartEndViaPoint( gridreference.Easting, gridreference.Northing, startDescription, startType );
							}
							else
							{
								AddStartEndViaPoint( startEasting, startNorthing, startDescription, startType );
							}

							//Add location points to map, for end or via
							endType = (i==(noOfJourneys-1)) ? ENDPOINT : VIAPOINT;

							//Scenario where user has planned To a Car Park
							if (journeyLegLast.LegEnd.Location.CarParking != null) 
                            {
								CarPark carPark = journeyLegLast.LegEnd.Location.CarParking;
								OSGridReference gridreference = new OSGridReference();
								gridreference = carPark.GetMapGridReference();
								AddStartEndViaPoint( gridreference.Easting, gridreference.Northing, endDescription, endType );
							}
							else
							{
								AddStartEndViaPoint( endEasting, endNorthing, endDescription, endType );
							}

							if (noOfJourneys == 1)
								theMapControl.Map.ZoomPTRoute( Session.SessionID, publicJourney.RouteNum );
						}
						else
						{
							//IR3966:Add Start/End/Via Point to map using same logic for outward and return journeys 
							startType = (i==0) ? STARTPOINT : VIAPOINT;
					
							AddStartEndViaPoint( startEasting,startNorthing,startDescription,startType );
						
							//IR3966:Add Start/End/Via Point to map using same logic for outward and return journeys 
							endType = (i==(noOfJourneys-1)) ? ENDPOINT : VIAPOINT;
		
							AddStartEndViaPoint( endEasting,endNorthing,endDescription,endType );

							if (noOfJourneys ==1)
								theMapControl.Map.ZoomPTRoute( Session.SessionID, publicJourney.RouteNum );
						}
                        #endregion
                    }
                    else if (aJourneys[i] is CyclePlannerControl.CycleJourney)
                    {
                        #region Add Cycle Journey to map

                        cycleJourney = (CyclePlannerControl.CycleJourney)aJourneys[i];

                        // Update the map to display the selected cycle journey
                        theMapControl.Map.AddCycleRoute(Session.SessionID, cycleJourney.RouteNum);

                        // Add the start and end point text
                        startType = (i == 0) ? STARTPOINT : VIAPOINT;
                        AddStartEndViaPoint(startEasting, startNorthing, startDescription, startType);

                        endType = (i == (noOfJourneys - 1)) ? ENDPOINT : VIAPOINT;
                        AddStartEndViaPoint(endEasting, endNorthing, endDescription, endType);

                        // Add the via location point if there is one
                        if (cycleJourney.RequestedViaLocation != null)
                        {
                            TDLocation viaLocation = cycleJourney.RequestedViaLocation;
                            AddStartEndViaPoint(viaLocation.GridReference.Easting,
                                viaLocation.GridReference.Northing,
                                viaLocation.Description,
                                VIAPOINT);
                        }

                        #region Add Ferrys and Toll icons

                        CycleJourneyDetail currentCycleDetail;

                        //This displays the Toll and Ferry Icons on the map
                        for (int a = 0; a < cycleJourney.Details.Length; a++)
                        {
                            currentCycleDetail = cycleJourney.Details[a];

                            //Only display if FerryExit/Entry or Toll Entry
                            if (currentCycleDetail.DisplayFerryIcon || currentCycleDetail.DisplayTollIcon)
                            {
                                string ITNToid = currentCycleDetail.NodeToid;

                                if (!string.IsNullOrEmpty(ITNToid))
                                {
                                    string toidPrefix = Properties.Current[TOID_PREFIX];

                                    if (toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
                                    {
                                        ITNToid = ITNToid.Substring(toidPrefix.Length);
                                    }

                                    // Get the coordinates
                                    iconMapPoint = theMapControl.Map.FindITNNodePoint(ITNToid);

                                    // Ferry or Toll icon
                                    string image = (currentCycleDetail.DisplayFerryIcon) ? "FERRY" : "TOLL";

                                    if (iconMapPoint != null)
                                    {
                                        //Add the F or T symbol to the map
                                        theMapControl.Map.AddSymbolPoint(iconMapPoint.X, iconMapPoint.Y, image, string.Empty);
                                    }
                                }
                            }
                        }

                        #endregion

                        // Zoom to ensure the map starts and displays the full cycle route
                        if (noOfJourneys == 1)
                            theMapControl.Map.ZoomCycleRoute();

                        #endregion
					}
					else
                    {
                        #region Add Road Journey to map
                        roadJourney = (JourneyControl.RoadJourney)aJourneys[i];

						//Expand the routes for use
						SqlHelper sqlHelper = new SqlHelper();
						sqlHelper.ConnOpen(SqlHelperDatabase.EsriDB);
						Hashtable htParameters = new Hashtable(2);
						htParameters.Add("@SessionID", Session.SessionID);
						htParameters.Add("@RouteNum", roadJourney.RouteNum);
						sqlHelper.Execute("usp_ExpandRoutes", htParameters);

						// Update the map to show display the selected road journey -
						theMapControl.Map.AddRoadRoute(Session.SessionID, roadJourney.RouteNum);

						JourneyLeg journeyLeg = roadJourney.JourneyLegs[0];
				
						//Park and Ride outward journey
						if (journeyLeg.LegEnd.Location.ParkAndRideScheme != null)
						{
							// Add location points to map, for start or via
							startType = (i==0) ? STARTPOINT : VIAPOINT;
							AddStartEndViaPoint( startEasting,startNorthing,startDescription,startType );

							//get CarPark details for journey TOID
							ParkAndRideInfo parkAndRideInfo = journeyLeg.LegEnd.Location.ParkAndRideScheme;
							journeyLeg.LegEnd.Location.CarPark = parkAndRideInfo.MatchCarPark(roadJourney.Details[roadJourney.Details.Length - 1].Toid);

							if (journeyLeg.LegEnd.Location.CarPark != null)
							{
								//Add location points to map, for end or via
								endType = (i==(noOfJourneys-1)) ? ENDPOINT : VIAPOINT;
								AddStartEndViaPoint(journeyLeg.LegEnd.Location.CarPark.GridReference.Easting, journeyLeg.LegEnd.Location.CarPark.GridReference.Northing, journeyLeg.LegEnd.Location.CarPark.CarParkName,endType );
							}
							if (noOfJourneys == 1)
								theMapControl.Map.ZoomRoadRoute( Session.SessionID, roadJourney.RouteNum );

							drawParkAndRideJourney(journeyLeg.LegStart.Location, journeyLeg.LegEnd.Location, roadJourney.RequestedViaLocation);
						}
							//Park and Ride return journey
						else if (journeyLeg.LegStart.Location.ParkAndRideScheme != null)
						{
							//get CarPark details for journey TOID
							ParkAndRideInfo parkAndRideInfo = journeyLeg.LegStart.Location.ParkAndRideScheme;
							journeyLeg.LegStart.Location.CarPark = parkAndRideInfo.MatchCarPark(roadJourney.Details[0].Toid);

							// Add location points to map, for start or via
							startType = (i==0) ? STARTPOINT : VIAPOINT;
							AddStartEndViaPoint( journeyLeg.LegStart.Location.CarPark.GridReference.Easting,journeyLeg.LegStart.Location.CarPark.GridReference.Northing,journeyLeg.LegStart.Location.CarPark.CarParkName,startType );

							if (journeyLeg.LegStart.Location.CarPark != null)
							{
								//Add location points to map, for end or via
								endType = (i==(noOfJourneys-1)) ? ENDPOINT : VIAPOINT;
								AddStartEndViaPoint(journeyLeg.LegEnd.Location.GridReference.Easting, journeyLeg.LegEnd.Location.GridReference.Northing, journeyLeg.LegEnd.Location.Description,endType );
							}

							if (noOfJourneys == 1)
								theMapControl.Map.ZoomRoadRoute( Session.SessionID, roadJourney.RouteNum );

							drawParkAndRideJourney(journeyLeg.LegStart.Location, journeyLeg.LegEnd.Location, roadJourney.RequestedViaLocation);
						}
							//Car Park outward or return journey
						else if ((journeyLeg.LegEnd.Location.CarParking != null) 
							|| (journeyLeg.LegStart.Location.CarParking != null) )
						{
							// Car Park journey can use Map, Entrance, or Exit coordinates to 
							// plan a journey From/To. We always want to display the Map coordinate 
							// as the Start/End point on the map

							// Add location points to map, for start or via
							startType = (i==0) ? STARTPOINT : VIAPOINT;

							//Scenario where user has planned From a Car Park
							if (journeyLeg.LegStart.Location.CarParking != null)
							{
								CarPark carPark = journeyLeg.LegStart.Location.CarParking;
								OSGridReference gridreference = new OSGridReference();
								gridreference = carPark.GetMapGridReference();

                                startDescription = FindCarParkHelper.GetCarParkName(carPark);

								AddStartEndViaPoint( gridreference.Easting, gridreference.Northing, startDescription, startType );
							}
							else
							{
								AddStartEndViaPoint( startEasting, startNorthing, startDescription, startType );
							}

							//Add location points to map, for end or via
							endType = (i==(noOfJourneys-1)) ? ENDPOINT : VIAPOINT;

							//Scenario where user has planned To a Car Park
							if (journeyLeg.LegEnd.Location.CarParking != null)
							{
								CarPark carPark = journeyLeg.LegEnd.Location.CarParking;
								OSGridReference gridreference = new OSGridReference();
								gridreference = carPark.GetMapGridReference();

                                endDescription = FindCarParkHelper.GetCarParkName(carPark);

								AddStartEndViaPoint( gridreference.Easting, gridreference.Northing, endDescription, endType );
							}
							else
							{
								AddStartEndViaPoint( endEasting, endNorthing, endDescription, endType );
							}
					
							if (noOfJourneys == 1)
								theMapControl.Map.ZoomRoadRoute( Session.SessionID, roadJourney.RouteNum );

						}
						else
						{
							// Add location points to map.
							//IR3966:Add Start/End/Via Point to map using same logic for outward and return journeys 
							startType = (i==0) ? STARTPOINT : VIAPOINT;
							AddStartEndViaPoint( startEasting,startNorthing,startDescription,startType );
							
							//IR3966:Add Start/End/Via Point to map using same logic for outward and return journeys 
							endType = (i==(noOfJourneys-1)) ? ENDPOINT : VIAPOINT;
							AddStartEndViaPoint( endEasting,endNorthing,endDescription,endType );

							if (noOfJourneys == 1)
								theMapControl.Map.ZoomRoadRoute( Session.SessionID, roadJourney.RouteNum );
						}

						//theMapControl.Map.ClearAddedSymbols();
						
						//This displays the Toll and Ferry Icons on the map
						for ( int a = 0; a < roadJourney.Details.Length; a++ )
						{
							currentRoadDetail = roadJourney.Details[a];
							//Only display if FerryExit/Entry or Toll Entry
							if (currentRoadDetail.displayFerryIcon == true || currentRoadDetail.displayTollIcon == true) 
							{
								string ITNToid = currentRoadDetail.nodeToid;

								string toidPrefix =  Properties.Current[TOID_PREFIX];

								if	(toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
								{
									ITNToid = ITNToid.Substring(toidPrefix.Length);
								}

								iconMapPoint = theMapControl.Map.FindITNNodePoint(ITNToid);

								xcord = iconMapPoint.X;
								ycord = iconMapPoint.Y;

								string Image = string.Empty;

								//Ferry Entry or Exit
								if(currentRoadDetail.displayFerryIcon == true)
								{
									Image = "FERRY";
								}
								else
									//Toll Entry
								{
									Image = "TOLL";
								}
								
								//Add the F or T symbol to the map
								theMapControl.Map.AddSymbolPoint(xcord, ycord, Image, string.Empty);
							}
                        }
                        #endregion
                    }

				} //foreach journey...

				// Zoom to multiple journey area
				if (usingItinerary && !itinerarySegmentSelected && (noOfJourneys > 1))
				{
					drawItineraryJourneys();
				}
				else if (noOfJourneys > 1)
				{
					theMapControl.Map.ZoomToAllAddedRoutes();
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
			catch(RouteInvalidException rie)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);
				
				Logger.Write(operationalEvent);
			}
			catch(NoPreviousExtentException npee)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

				Logger.Write(operationalEvent);
			}
			catch(MapExceptionGeneral meg)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);
				
				Logger.Write(operationalEvent);
			}
			// Show intermediate nodes if journey is a public journey or an extended journey with full itinerary selected
			if((outward && mapHelper.PublicOutwardJourney) || (!outward && mapHelper.PublicReturnJourney) || (usingItinerary && !itinerarySegmentSelected))
			{
				mapControl.Map.ShowIntermediateNodesOnMap(outward);
				this.mapToRefresh = true;
			}

			this.mapToRefresh = true;
		}

		/// <summary>
		/// Method to clear the map routes
		/// </summary>
		private void ClearMapRoutes()
		{
			try
			{
				theMapControl.Map.ClearPTRoutes();
				theMapControl.Map.ClearRoadRoutes();
                theMapControl.Map.ClearCycleRoute();
				theMapControl.Map.ClearStartEndPoints();
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
			catch(RouteInvalidException rie)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);
				
				Logger.Write(operationalEvent);
			}
			catch(MapExceptionGeneral meg)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);
			
				Logger.Write(operationalEvent);
			}
		}

		#endregion	
	
		#region Method to add start/end point for road journeys on the map

		/// <summary>
		/// Adds start/end/via points on the map for given coordinates.
		/// </summary>
		/// <param name="easting">Easting of location</param>
		/// <param name="northing">Northing of location</param>
		/// <param name="description">Description of location</param>
		private void AddStartEndViaPoint(double easting, double northing, string description, int type)
		{
			// Strip out any sub strings denoting pseudo locations
			System.Text.StringBuilder strName = new System.Text.StringBuilder(description);
			strName.Replace(railPostFix, "");
			strName.Replace(coachPostFix, "");
			strName.Replace(railcoachPostFix, "");
			description = strName.ToString();
			try
			{
				switch (type)
				{
					case STARTPOINT :
						if (easting > 0)
							theMapControl.Map.AddStartPoint(easting, northing, description);
						break;
					case ENDPOINT :
						if (easting > 0)
							theMapControl.Map.AddEndPoint(easting, northing, description);
						break;
					case VIAPOINT :
						if (easting > 0)
							theMapControl.Map.AddViaPoint(easting, northing, description);
						break;
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

		}

		#endregion

		#region ViewState

		/// <summary>
		/// Overrides base view state to load custom viewstate
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
					outward = (bool)(myState[1]);
				if (myState[2] != null)
					directionsVisible = (bool)myState[2];
				if (myState[3] != null)
					callingPageId = (PageId)myState[3];
				if (myState[4] != null)
					naptanFound = (bool)myState[4];
				if (myState[5] != null)
					naptanInRange = (bool)myState[5];
				if (myState[6] != null)
					previousPublic = (bool)myState[6];
                if (myState[7] != null)
                    directionsCycleVisible = (bool)myState[7];
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

            object[] allStates = new object[8];
			allStates[0] = baseState;
			allStates[1] = outward;
			allStates[2] = directionsVisible;
			allStates[3] = callingPageId;
			allStates[4] = naptanFound;
			allStates[5] = naptanInRange;
			allStates[6] = previousPublic;
            allStates[7] = directionsCycleVisible;

			return allStates;
		}

		#endregion

        #region ExtraWiringEvents
        /// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraWiringEvents()
		{
			
			//This ensures that the Map viewstate is saved for the 'More' help click
			this.helpLabelMapToolsOutward.MoreHelpEvent += new EventHandler( this.OnMoreHelpEvent);
			this.helpLabelMapToolsReturn.MoreHelpEvent += new EventHandler( this.OnMoreHelpEvent);
			this.journeyMapControlPublicHelpLabel.MoreHelpEvent += new EventHandler (this.OnMoreHelpEvent);
			this.journeyMapControlPrivateHelpLabel.MoreHelpEvent += new EventHandler (this.OnMoreHelpEvent);
			this.journeyMapControlItineraryHelpLabel.MoreHelpEvent += new EventHandler (this.OnMoreHelpEvent);
           
			
			// Add a handler for the OnMapChangedEvent.
			theMapControl.Map.OnMapChangedEvent += new MapChangedEventHandler(this.Map_Changed);

			// Add a handler for the map Exception event
			theMapControl.Map.OnMapExceptionEvent += new MapExceptionEventHandler(this.Map_Exception);

			theMapJourneyDisplayDetailsControl.ButtonShow.Click += new EventHandler ( this.btnOK_Click );

			buttonDirections.Click += new EventHandler(this.imageButtonDirections_Click);
            buttonDirectionsCycle.Click += new EventHandler(buttonDirectionsCycle_Click);
        }

        #endregion

        #region Designer generated code

        /// <summary>
		/// On Init Method - wires up all events
		/// </summary>
		override protected void OnInit(EventArgs e)
		{

			ExtraWiringEvents();

			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);

		}

		/// <summary>
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}

		#endregion

		#region Handler for OK button on the Icons Select Control

		/// <summary>
		/// Button handler to handle the event fired by the OK button in the
		/// Icons Select control
		/// </summary>
		private void IconsSelectRefresh()
		{
			try
			{
				bool change = false;
				// Get all the selected hashtable codes for Stops
				ArrayList selectedStopKeys =
					theMapLocationIconsSelectControl.GetSelectedStopKeys();

				// Create a new StopsVisible Hashtable
				Hashtable stopsVisible = new Hashtable();
			
				// Iterate through all available keys and set boolean accordindly
				foreach(string stopkey in theMapControl.Map.StopsVisible.Keys)
				{
					if( ((bool)theMapControl.Map.StopsVisible[stopkey]) != selectedStopKeys.Contains(stopkey) )
					{
						change = true;
					}
					stopsVisible.Add(stopkey, selectedStopKeys.Contains(stopkey));
				}

				//Get all the selected hashtable codes for PointX
				ArrayList selectedPointXKeys =
					theMapLocationIconsSelectControl.GetSelectedPointXKeys();

				// Create a new PointX Hashtable
				Hashtable pointXVisible = new Hashtable();

				// Iternate through all available keys and set boolean accordingly.
				foreach(string pointXKey in theMapControl.Map.PointXVisible.Keys)
				{
					if( ((bool) theMapControl.Map.PointXVisible[pointXKey] ) !=	   selectedPointXKeys.Contains(pointXKey) )
					{
						change = true;
					}
					pointXVisible.Add(pointXKey, selectedPointXKeys.Contains(pointXKey));
				}
		

				if(FindCarParkHelper.CarParkingAvailable)
				{
					// if the visibility has changed, re-toggle the car park layer
					if( theMapControl.Map.CarParkLayerVisible != theMapLocationIconsSelectControl.CarParksSelected)
					{
						theMapControl.Map.ToggleLayerVisibility(theMapControl.Map.CarParkLayerIndex);
						change = true;
					}
				}

				// Only refresh map if it has changed.
				if( change )
				{

					// Reset and update the "PointX" hashtable
					theMapControl.Map.StopsVisible = stopsVisible;
					theMapControl.Map.PointXVisible = pointXVisible;

					// Refresh the map
					if (theMapLocationIconsSelectControl.IsEnabled)
						mapToRefresh = true;
					
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

			// Update iconSelection for Printer Friendly pages
			bool [][] iconSelection;
			if (outward)
			{
				iconSelection = TDSessionManager.Current.InputPageState.IconSelectionOutward;
			}
			else
			{
				iconSelection = TDSessionManager.Current.InputPageState.IconSelectionReturn;
			}
			theMapLocationIconsSelectControl.UpdateIconSelection(ref iconSelection);
		}

		#endregion

		#region Handler for map exception

		/// <summary>
		/// Handler for the map exception event.
		/// </summary>
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

		#endregion

		#region Handler for Map Changed event

		/// <summary>
		/// Map Changed Event Handler
		/// </summary>
		private void Map_Changed(object sender, MapChangedEventArgs e)
		{
            // Update the image url of the summary map
            imageSummaryMap.ImageUrl = e.OvURL;
			
			theMapControl.ScaleChange( e.MapScale, e.NaptanInRange );

			// Check to see if it is necessary to update the Location Select control
			// and Map Tools Control (as the scale determines if the controls should
			// be enabled/disabled.
			naptanInRange = e.NaptanInRange;
			theMapLocationIconsSelectControl.EnableOKButton( naptanInRange );

			if( outward )
			{
				TDSessionManager.Current.JourneyMapState.SelectEnabled = naptanInRange;

				// Update the map URLS and scales in InputPageState for printer friendly maps

                if (TDSessionManager.Current.FindAMode != FindAMode.EnvironmentalBenefits || !IsPostBack)
                {
                    
                    TDSessionManager.Current.InputPageState.MapUrlOutward =
                        theMapControl.Map.ImageUrl;

                    TDSessionManager.Current.InputPageState.OverviewMapUrlOutward =
                        e.OvURL;

                    TDSessionManager.Current.InputPageState.MapScaleOutward =
                        e.MapScale;
                    
                }
                                
			}
			else
			{
				TDSessionManager.Current.ReturnJourneyMapState.SelectEnabled = naptanInRange;

                if (TDSessionManager.Current.FindAMode != FindAMode.EnvironmentalBenefits || !IsPostBack)
                {
                    TDSessionManager.Current.InputPageState.MapUrlReturn =
                        theMapControl.Map.ImageUrl;

                    TDSessionManager.Current.InputPageState.OverviewMapUrlReturn =
                        e.OvURL;

                    TDSessionManager.Current.InputPageState.MapScaleReturn =
                        e.MapScale;
                }
                
			}

			
		}

		#endregion

		#region Handler for Map Selection Control events
		/// <summary>
		/// Event handler for the map mode changed event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void MapModeChanged(object sender, ModeChangedEventArgs e)
		{
			// Set the map mode to the mode in the event args
			this.theMapControl.Map.ClickMode = e.MapMode;
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

			// Update the label on the page that shows what the current selected location is.
			AddAdditionalIconsOnMap();

			//Set the current location of the control to be the location name
			//of the event
			this.labelSelectedLocation.Text = e.LocationName;
		}

		/// <summary>
		/// Method to add additional icons on to the map.
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

			// Strip out any sub strings denoting pseudo locations
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
		/// Private method firing the public InformationRequestedEvent event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnInformationRequested(object sender, EventArgs e )
		{
			// Fire event here
			if( InformationRequestedEvent != null )
			{
				InformationRequestedEvent( this, e);
			}
		}

		/// <summary>
		/// Private method firing the public MoreHelpEvent event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnMoreHelpEvent(object sender, EventArgs e )
		{
			// Fire event here
			if( MoreHelpEvent != null )
			{
				MoreHelpEvent( this, e);
			}
		}

		#endregion

		#region Previous View/Original View/Show Info/OK View button handlers

		/// <summary>
		/// OK Button handler for the Map Tools Control. This handler will update
		/// the mapping component depending on which map has been selected from
		/// the drop down list.
		/// </summary>
		private void btnOK_Click(object sender, EventArgs e)
		{
			ShowSelectedMapLeg();
		}

		#endregion

        #region Draw public/cycle/private journeys on the map

        /// <summary>
		/// Draw a public journey on the map.
		/// If not displaying itinerary, i.e. not showing multiple journeys, then 
		/// just use the selected index from the drop down. 
		/// </summary>
		/// <param name="journey"></param>
		private void drawPublicJourney( Journey journey ) 
		{
			ListItem selectedJourneySegment = theMapJourneyDisplayDetailsControl.SelectedJourneySegment;
			int selectedJourneySegmentIndex;
			
			if (selectedJourneySegment == null || selectedJourneySegment.Value == null || selectedJourneySegment.Value == string.Empty)
				selectedJourneySegmentIndex = 0;
			else
				selectedJourneySegmentIndex = int.Parse(selectedJourneySegment.Value);

			drawPublicJourney( journey, selectedJourneySegmentIndex);
		}

		/// <summary>
		/// Draw a public journey on the map
		/// </summary>
		/// <param name="journey"></param>
		private void drawPublicJourney( Journey journey, int selectedJourneyLeg) 
		{
			JourneyControl.PublicJourney publicJourney = (JourneyControl.PublicJourney)journey;

			if(selectedJourneyLeg == 0)
			{
				try
				{
					// "View Entire Map option" has been selected
					theMapControl.Map.ZoomPTRoute( Session.SessionID, publicJourney.RouteNum );
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
				catch(RouteInvalidException rie)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);
				
					Logger.Write(operationalEvent);
				}
				catch(NoPreviousExtentException npee)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

					Logger.Write(operationalEvent);
				}
				catch(MapExceptionGeneral meg)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);
					
					Logger.Write(operationalEvent);
				}
			}
			else
			{

				OSGridReference[] osgr = publicJourney.Details[ selectedJourneyLeg - 1 ].Geometry;
				if( osgr.Length == 0 )
				{
					return;
				}	
				
				// Zoom the map to the correct route leg coords
				// create the envelope to contain the all points on the map. 
				createMapEnvelope(osgr);

				// Write the selected journey leg "name" to the session for
				// printer friendly print page.
				if (outward)
					TDSessionManager.Current.InputPageState.MapViewTypeOutward =
						theMapJourneyDisplayDetailsControl.SelectedJourneySegment.Text;
				else
					TDSessionManager.Current.InputPageState.MapViewTypeReturn =
						theMapJourneyDisplayDetailsControl.SelectedJourneySegment.Text;
			}
		}


		/// <summary>
		/// Draw a private journey on the map.
		/// If not displaying itinerary, i.e. not showing multiple journeys, then 
		/// just use the selected index from the drop down. 
		/// </summary>
		/// <param name="journey"></param>
		private void drawPrivateJourney( Journey journey ) 
		{
			//drawPrivateJourney( journey, theMapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex);
			if(theMapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex != 0)
			{
				int JourneyIndex = Convert.ToInt32(theMapJourneyDisplayDetailsControl.SelectedJourneyValue);

				drawPrivateJourney( journey, JourneyIndex);
			}
			else
			{
				drawPrivateJourney( journey, theMapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex);
			}
		}

		/// <summary>
		/// Draw a private journey on the map
		/// </summary>
		/// <param name="journey"></param>
		private void drawPrivateJourney( Journey journey, int selectedJourneyLeg ) 
		{

			// Journey is a road journey
			RoadJourney roadJourney = (RoadJourney)journey;

			// Add the road data for the selected traffic adjustment level
			// to the map
			// If using Itinerary then can only ever show Adjusted route.
			if (!usingItinerary || itinerarySegmentSelected )
			{
				AddRoadJourneyDataToMap();
			}


			//If the Map of the full journey has been selected.
			if(selectedJourneyLeg == 0 && roadJourney.DestinationLocation.ParkAndRideScheme == null)
			{
				try
				{
					// "Map of entire journey" option has been selected.
					theMapControl.Map.ZoomRoadRoute(Session.SessionID, roadJourney.RouteNum);
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
				catch(RouteInvalidException rie)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);
				
					Logger.Write(operationalEvent);
				}
				catch(NoPreviousExtentException npee)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

					Logger.Write(operationalEvent);
				}
				catch(MapExceptionGeneral meg)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);
					
					Logger.Write(operationalEvent);
				}
			}
			//If the Map of the full journey has been selected and the journey is a Park and Ride Journey - fix for IR4464
			//When a user selects the Map of entire journey option the locality should be included on the map
			else if (selectedJourneyLeg == 0 && roadJourney.DestinationLocation.ParkAndRideScheme != null)
			{
				CarParkInfo parkAndRideCarPark = null;

				if (roadJourney.DestinationLocation.CarPark == null)
				{
					string[] carParkToid = {roadJourney.Details[roadJourney.Details.Length-1].Toid[roadJourney.Details[roadJourney.Details.Length-1].Toid.Length-1]};
					parkAndRideCarPark = roadJourney.DestinationLocation.ParkAndRideScheme.MatchCarPark(carParkToid);
				}

				if (parkAndRideCarPark != null)
				{
					OSGridReference[] osGridArray = {roadJourney.OriginLocation.GridReference,
														parkAndRideCarPark.GridReference,
														roadJourney.DestinationLocation.ParkAndRideScheme.SchemeGridReference,
														((roadJourney.RequestedViaLocation !=null) ? roadJourney.RequestedViaLocation.GridReference : null)
													};

					if(osGridArray.Length == 0)
						return;

					//create the map envelope using the osgrids that are provided. 
					createMapEnvelope(osGridArray);
				}
				else
				{
					OSGridReference[] osGridArray = {roadJourney.OriginLocation.GridReference,
														((roadJourney.DestinationLocation.CarPark!=null) ? roadJourney.DestinationLocation.CarPark.GridReference : null),
														roadJourney.DestinationLocation.ParkAndRideScheme.SchemeGridReference,
														((roadJourney.RequestedViaLocation!=null) ? roadJourney.RequestedViaLocation.GridReference : null)
													};

					if(osGridArray.Length == 0)
						return;

					//create the map envelope using the osgrids that are provided. 
					createMapEnvelope(osGridArray);
				}

			}


			int lastitem = roadJourney.Details.Length+2;
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			ITDJourneyRequest request = viewState.OriginalJourneyRequest;
								
			int easting = 0;
			int northing = 0;
			//if the beginning or the end of the journey has been selected.			
			if ( (selectedJourneyLeg == 1) || (selectedJourneyLeg == lastitem) )
			{				
				// "START" or "END" option has been selected.
				// Get the coordinates of the start OR end location from the request object
	
				if(outward)
				{
					easting = selectedJourneyLeg == 1 ?
						request.OriginLocation.GridReference.Easting:
						request.DestinationLocation.GridReference.Easting;

					northing = selectedJourneyLeg == 1 ?
						request.OriginLocation.GridReference.Northing:
						request.DestinationLocation.GridReference.Northing;
				}
				else
				{
					easting =	selectedJourneyLeg == 1 ?
						request.ReturnOriginLocation.GridReference.Easting: 
						request.ReturnDestinationLocation.GridReference.Easting;

					northing =	selectedJourneyLeg == 1 ?
						request.ReturnOriginLocation.GridReference.Northing :
						request.ReturnDestinationLocation.GridReference.Northing;
				}

				// Call the map component to zoom to the specified location.
				// If the current map scale is less than 80000, then zoom to the
				// location with the current map scale. If the map scale is currently
				// greater than 80000, then zoom to the 80000.
				// NOTE: The 80000 valus is read from the Properties service and
				// may therefore change.

				string cutOffProperty = Properties.Current["InteractiveMapping.Map.RoadScaleCutOff"];

				int zoomLevelCutOff = Convert.ToInt32(cutOffProperty, TDCultureInfo.CurrentUICulture.NumberFormat);

				try
				{		
					// Find out what the current scale is
					int currentScale = theMapControl.Map.Scale;
					//TODO - pull from database. No longer using - set to 4000 to match other leg maps
					int scaleToZoom = currentScale < zoomLevelCutOff ? currentScale : zoomLevelCutOff;

					theMapControl.Map.ZoomToPoint(easting, northing, 4000);
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
				catch(ScaleZeroOrNegativeException szone)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

					Logger.Write(operationalEvent);
				}
				catch(EnvelopeZeroOrNegativeException ezone)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + ezone.Message);

					Logger.Write(operationalEvent);
				}
				catch(ScaleOutOfRangeException soore)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

					Logger.Write(operationalEvent);
				}
				catch(RouteInvalidException rie)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);
				
					Logger.Write(operationalEvent);
				}
				catch(NoPreviousExtentException npee)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

					Logger.Write(operationalEvent);
				}
				catch(MapExceptionGeneral meg)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);
					
					Logger.Write(operationalEvent);
				}

			}

			else if(selectedJourneyLeg != 0)
			{
				//it must be a section leg that need to be displayed
				RoadJourneyDetail currentDetail = roadJourney.Details[selectedJourneyLeg-2];
				
				if (currentDetail == roadJourney.Details[0])
				{
					//It is the second step on the directions table, which also happens
					//to have the same map as the origin location.
					if (outward)
					{
						easting = request.OriginLocation.GridReference.Easting;
						northing = request.OriginLocation.GridReference.Northing;
					}
					else
					{
						easting = request.DestinationLocation.GridReference.Easting;
						northing = request.DestinationLocation.GridReference.Northing;
					}

					theMapControl.Map.ZoomToPoint(easting, northing, 4000);

				}

				else
				{
					RoadJourneyDetail previousDetail = roadJourney.Details[selectedJourneyLeg -3];

					if (previousDetail.IsStopOver  && !currentDetail.IsStopOver)
					{
						//If the previous section was a StopoverSection, but the current one isn't
						//then it is the first drive section after a Stopover, so
						//we need to use the ITNNode method to display the map.
						string toidPrefix =  Properties.Current[TOID_PREFIX];
					
						string ITNToid = previousDetail.nodeToid;

						if	(toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
						{
							ITNToid = ITNToid.Substring(toidPrefix.Length);
						}

						theMapControl.Map.ZoomToITNNode(ITNToid, 4000);

					}

						//If the current RoadJourneyDetail is not a StopoverSection
					else if (!currentDetail.IsStopOver)
					{
						string FirstToid = string.Empty;
						string LastToid = string.Empty;					
		
						RoadJourneyDetailMapInfo mapInfo = new RoadJourneyDetailMapInfo(previousDetail.RoadJourneyDetailMapInfo.lastToid, currentDetail.RoadJourneyDetailMapInfo.firstToid);
						FirstToid = mapInfo.firstToid;
						LastToid = mapInfo.lastToid;

						string toidPrefix =  Properties.Current[TOID_PREFIX];


						if	(toidPrefix == null) 
						{
							toidPrefix = string.Empty;
						}

						if	(toidPrefix.Length > 0 && FirstToid.StartsWith(toidPrefix))
						{
							FirstToid = FirstToid.Substring(toidPrefix.Length);
						}

						if	(toidPrefix.Length > 0 && LastToid.StartsWith(toidPrefix))
						{
							LastToid = LastToid.Substring(toidPrefix.Length);
						}

						theMapControl.Map.ZoomToJunction(LastToid, FirstToid, 4000);
					}

					else

					{
						//It is a StopoverSection.  We need to call the ITNNode method to display the map
						string toidPrefix =  Properties.Current[TOID_PREFIX];
					
						string ITNToid = currentDetail.nodeToid;

						if	(toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
						{
							ITNToid = ITNToid.Substring(toidPrefix.Length);
						}
						
						//
						try
						{
							//The map will zoom to the ITNNode TOID.
							theMapControl.Map.ZoomToITNNode(ITNToid, 4000);
						}
						catch
						{
							//The toid cannot be found or is invalid, therefore display a map of the UK.
							theMapControl.Map.ZoomFull();

							Logger.Write(new OperationalEvent(  TDEventCategory.Infrastructure,
								TDTraceLevel.Info, String.Format("The Node TOID was not found at TransportDirect.Presentation.InteractiveMapping.Map.ParseRoadNodeTOIDResponse for " + currentDetail.nodeToid)));
						}
					}

				}
			}

			// Write the selected journey leg "name" to the session for
			// printer friendly print page.
			if (outward)
				TDSessionManager.Current.InputPageState.MapViewTypeOutward =
					theMapJourneyDisplayDetailsControl.SelectedJourneySegment.Text;
			else
				TDSessionManager.Current.InputPageState.MapViewTypeReturn =
					theMapJourneyDisplayDetailsControl.SelectedJourneySegment.Text;
		}

        #region Draw cycle journey

        /// <summary>
        /// Draw a cycle journey on the map.
        /// If not displaying itinerary, i.e. not showing multiple journeys, then 
        /// just use the selected index from the drop down. 
        /// </summary>
        /// <param name="journey"></param>
        private void drawCycleJourney(Journey journey)
        {
            if (theMapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex != 0)
            {
                int JourneyIndex = Convert.ToInt32(theMapJourneyDisplayDetailsControl.SelectedJourneyValue);

                drawCycleJourney(journey, JourneyIndex);
            }
            else
            {
                drawCycleJourney(journey, theMapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex);
            }
        }

        /// <summary>
        /// Draw a cycle journey on the map
        /// </summary>
        /// <param name="journey"></param>
        private void drawCycleJourney(Journey journey, int selectedJourneyLeg)
        {
            // Journey is a cycle journey
            CycleJourney cycleJourney = (CycleJourney)journey;

            //If the Map of the full journey has been selected.
            if (selectedJourneyLeg == 0)
            {
                try
                {
                    // "Map of entire journey" option has been selected.
                    theMapControl.Map.ZoomCycleRoute();
                }
                #region Catch exception
                catch (PropertiesNotSetException pnse)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

                    Logger.Write(operationalEvent);
                }
                catch (MapNotStartedException mnse)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);

                    Logger.Write(operationalEvent);
                }
                catch (ScaleOutOfRangeException soore)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

                    Logger.Write(operationalEvent);
                }
                catch (ScaleZeroOrNegativeException szone)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

                    Logger.Write(operationalEvent);
                }
                catch (RouteInvalidException rie)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);

                    Logger.Write(operationalEvent);
                }
                catch (NoPreviousExtentException npee)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

                    Logger.Write(operationalEvent);
                }
                catch (MapExceptionGeneral meg)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);

                    Logger.Write(operationalEvent);
                }
                #endregion
            }

            int lastitem = cycleJourney.Details.Length + 2;
            //TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            //ITDJourneyRequest request = viewState.OriginalJourneyRequest;
            ITDCyclePlannerRequest request = TDSessionManager.Current.CycleRequest;

            #region Set zoom to scale value

            // Call the map component to zoom to the specified location.
            // If the current map scale is less than 4000, then zoom to the
            // location with the current map scale. If the map scale is currently
            // greater than 4000, then zoom to the 4000.
            // NOTE: This value may change as it is read from the Properties service
            string cutOffProperty = Properties.Current["CyclePlanner.InteractiveMapping.Map.CycleScaleCutOff"];

            int zoomLevelCutOff = Convert.ToInt32(cutOffProperty, TDCultureInfo.CurrentUICulture.NumberFormat);

            // Find out what the current scale is
            int currentScale = theMapControl.Map.Scale;

            int scaleToZoom = currentScale < zoomLevelCutOff ? currentScale : zoomLevelCutOff;

            #endregion

            int easting = 0;
            int northing = 0;
            //if the beginning or the end of the journey has been selected.			
            if ((selectedJourneyLeg == 1) || (selectedJourneyLeg == lastitem))
            {
                // "START" or "END" option has been selected.
                // Get the coordinates of the start OR end location from the request object

                if (outward)
                {
                    easting = selectedJourneyLeg == 1 ?
                        request.OriginLocation.GridReference.Easting :
                        request.DestinationLocation.GridReference.Easting;

                    northing = selectedJourneyLeg == 1 ?
                        request.OriginLocation.GridReference.Northing :
                        request.DestinationLocation.GridReference.Northing;
                }
                else
                {
                    easting = selectedJourneyLeg == 1 ?
                        request.DestinationLocation.GridReference.Easting :
                        request.OriginLocation.GridReference.Easting;

                    northing = selectedJourneyLeg == 1 ?
                        request.DestinationLocation.GridReference.Northing :
                        request.OriginLocation.GridReference.Northing;
                }

                try
                {
                    theMapControl.Map.ZoomToPoint(easting, northing, scaleToZoom);
                }
                #region Catch exception
                catch (PropertiesNotSetException pnse)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

                    Logger.Write(operationalEvent);
                }
                catch (MapNotStartedException mnse)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);

                    Logger.Write(operationalEvent);
                }
                catch (ScaleZeroOrNegativeException szone)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

                    Logger.Write(operationalEvent);
                }
                catch (EnvelopeZeroOrNegativeException ezone)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + ezone.Message);

                    Logger.Write(operationalEvent);
                }
                catch (ScaleOutOfRangeException soore)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

                    Logger.Write(operationalEvent);
                }
                catch (RouteInvalidException rie)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);

                    Logger.Write(operationalEvent);
                }
                catch (NoPreviousExtentException npee)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

                    Logger.Write(operationalEvent);
                }
                catch (MapExceptionGeneral meg)
                {
                    // Log the exception
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);

                    Logger.Write(operationalEvent);
                }
                #endregion

            }
            //section leg that needs to be displayed
            else if (selectedJourneyLeg != 0)
            {
                CycleJourneyDetail currentDetail = cycleJourney.Details[selectedJourneyLeg - 2];

                if (currentDetail == cycleJourney.Details[0])
                {
                    //It is the second step on the directions table, which also happens
                    //to have the same map as the origin location.
                    if (outward)
                    {
                        easting = request.OriginLocation.GridReference.Easting;
                        northing = request.OriginLocation.GridReference.Northing;
                    }
                    else
                    {
                        easting = request.DestinationLocation.GridReference.Easting;
                        northing = request.DestinationLocation.GridReference.Northing;
                    }

                    theMapControl.Map.ZoomToPoint(easting, northing, scaleToZoom);
                }

                else
                {
                    CycleJourneyDetail previousDetail = cycleJourney.Details[selectedJourneyLeg - 3];

                    if (previousDetail.StopoverSection && !currentDetail.StopoverSection)
                    {
                        //If the previous section was a StopoverSection, but the current one isn't
                        //then it is the first drive section after a Stopover, so
                        //we need to use the ITNNode method to display the map.
                        string toidPrefix = Properties.Current[TOID_PREFIX];
                        
                        // Fix to allow Zoom to UTurn instruction
                        string ITNToid = string.Empty;
                        if (previousDetail.NodeToid != null && previousDetail.NodeToid.Length != 0)
                        {
                            ITNToid = previousDetail.NodeToid;

                            if (toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
                            {
                                ITNToid = ITNToid.Substring(toidPrefix.Length);
                            }

                            theMapControl.Map.ZoomToITNNode(ITNToid, scaleToZoom);
                        }
                        else
                        {
                            // This is a U-Turn so we don't have a Node
                            // Use the OSGR of the section start instead.
                            theMapControl.Map.ZoomToPoint(System.Convert.ToDouble(currentDetail.StartOSGR.Easting),
                                System.Convert.ToDouble(currentDetail.StartOSGR.Northing), scaleToZoom);
                        }
                    }

                    //If the current CycleJourneyDetail is not a StopoverSection
                    else if (!currentDetail.StopoverSection)
                    {
                        // Variables to hold OSGR found
                        OSGridReference intersectionOSGR = null;
                        OSGridReference firstOSGR = null;

                        GetIntersectionCoordinate(previousDetail.Geometry, currentDetail.Geometry,
                            out intersectionOSGR, out firstOSGR);

                        try
                        {
                            if (intersectionOSGR != null)
                            {
                                theMapControl.Map.ZoomToPoint(intersectionOSGR.Easting, intersectionOSGR.Northing, scaleToZoom);
                            }
                            else
                            {
                                theMapControl.Map.ZoomToPoint(firstOSGR.Easting, firstOSGR.Northing, scaleToZoom);
                            }
                        }
                        catch (Exception ex)
                        {
                            //No OSGR could be found or an exception trying to zoom, therefore just display the full cycle journey.
                            theMapControl.Map.ZoomCycleRoute();

                            Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                                TDTraceLevel.Info,
                                "Cycle route map - Unable to zoom to point for a cycle direction, at intersection of toids: "
                                + previousDetail.RoadJourneyDetailMapInfo.lastToid
                                + ", "
                                + currentDetail.RoadJourneyDetailMapInfo.firstToid
                                + ". Exception: "
                                + ex.Message));
                        }
                    }
                    //It is a StopoverSection.  We need to call the ITNNode method to display the map
                    else
                    {
                        string toidPrefix = Properties.Current[TOID_PREFIX];

                                               
                        // Fix to allow Zoom to a via instruction immediately before a U-Turn
                        string ITNToid = string.Empty;
                        if (currentDetail.NodeToid != null && currentDetail.NodeToid.Length != 0)
                        {
                            ITNToid = currentDetail.NodeToid;

                            if (toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
                            {
                                ITNToid = ITNToid.Substring(toidPrefix.Length);
                            }

                            theMapControl.Map.ZoomToITNNode(ITNToid, scaleToZoom);

                            if (toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
                            {
                                ITNToid = ITNToid.Substring(toidPrefix.Length);
                            }

                            try
                            {
                                //The map will zoom to the ITNNode TOID.
                                theMapControl.Map.ZoomToITNNode(ITNToid, scaleToZoom);
                            }
                            catch
                            {
                                //The toid cannot be found or is invalid, therefore just display the full cycle journey.
                                theMapControl.Map.ZoomCycleRoute();

                                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                                    TDTraceLevel.Info, String.Format("The Node TOID was not found at TransportDirect.Presentation.InteractiveMapping.Map.ParseCycleNodeTOIDResponse for " + currentDetail.NodeToid)));
                            }
                        }
                        else
                        {
                            try
                            {
                                // We don't have a Node as this is a Point on a link being used as a via.
                                //
                                // Instead try to find the last point of the previous section as this should 
                                // be the same Point as this Via. This is found in the last value in the array 
                                // of geometry objects.
                                //
                                // We can then zoom to that Point.
                                OSGridReference previousEndPoint = (OSGridReference)previousDetail.Geometry[previousDetail.Geometry.Count - 1].GetValue(previousDetail.Geometry[previousDetail.Geometry.Count - 1].Length - 1);

                                theMapControl.Map.ZoomToPoint(System.Convert.ToDouble(previousEndPoint.Easting),
                                    System.Convert.ToDouble(previousEndPoint.Northing), scaleToZoom);
                            }
                            catch
                            {
                                //There was a problem with the geometry, therefore just display the full cycle journey.
                                theMapControl.Map.ZoomCycleRoute();

                                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                                    TDTraceLevel.Info, String.Format("The Geometry was not found at TransportDirect.Presentation.InteractiveMapping.Map.ParseCycleNodeTOIDResponse")));
                            }
                        }
                    }
                }
            }

            // Write the selected journey leg "name" to the session for
            // use by printer friendly page, if needed.
            if (outward)
                TDSessionManager.Current.InputPageState.MapViewTypeOutward =
                    theMapJourneyDisplayDetailsControl.SelectedJourneySegment.Text;
            else
                TDSessionManager.Current.InputPageState.MapViewTypeReturn =
                    theMapJourneyDisplayDetailsControl.SelectedJourneySegment.Text;
        }

        /// <summary>
        /// Gets the the first OSGR found which matches in the two geometry arrays (the intersectionOSGR).
        /// Also sets the first OSGR coordinate found in the currentGeometry array.
        /// </summary>
        /// <param name="inteserctionOSGR"></param>
        /// <param name="firstOSGR"></param>
        private void GetIntersectionCoordinate(Dictionary<int, OSGridReference[]> previousGeometry, Dictionary<int, OSGridReference[]> currentGeometry,
            out OSGridReference intersectionOSGR, out OSGridReference firstOSGR)
        {
            bool intersectionOSGRFound = false;
            intersectionOSGR = null;
            firstOSGR = null;

            // Loop through the two OSGR arrays and find the coordinate which is in both.
            // This will indicate the point where the two sections intersect/join.
            foreach (KeyValuePair<int, OSGridReference[]> keyvaluePreviousGeometry in previousGeometry)
            {
                if (!intersectionOSGRFound)
                {
                    foreach (OSGridReference previousOSGR in keyvaluePreviousGeometry.Value)
                    {
                        if (!intersectionOSGRFound)
                        {
                            foreach (KeyValuePair<int, OSGridReference[]> keyvalueCurrentGeometry in currentGeometry)
                            {
                                if (!intersectionOSGRFound)
                                {
                                    foreach (OSGridReference currentOSGR in keyvalueCurrentGeometry.Value)
                                    {
                                        // Fall back is to use the first OSGR of currentDetail
                                        if (firstOSGR == null)
                                            firstOSGR = currentOSGR;

                                        // Check if there is a matching OSGR
                                        if ((currentOSGR.Easting == previousOSGR.Easting)
                                            &&
                                            (currentOSGR.Northing == previousOSGR.Northing))
                                        {
                                            intersectionOSGR = currentOSGR;
                                            intersectionOSGRFound = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                    break;
                            }
                        }
                        else
                            break;
                    }
                }
                else
                    break;
            }
        }

        #endregion

		/// <summary>
		/// Draw journeys from a full itinerary on a map
		/// </summary>
		private void drawItineraryJourneys() 
		{
			Journey [] aJourneys = getCurrentJourneys();

			ListItem selectedJourneySegment = theMapJourneyDisplayDetailsControl.SelectedJourneySegment;
			int selectedJourneyLeg;
			
			if (selectedJourneySegment == null || selectedJourneySegment.Value == null || selectedJourneySegment.Value == string.Empty)
				selectedJourneyLeg = 0;
			else
			{ // changed for IR3597
				try 
				{
					selectedJourneyLeg = int.Parse(selectedJourneySegment.Value);
				}
				catch (System.FormatException sfe)
				{
					// dummy string created to remove code warning, as we don't want or need to capture the error.
					String deadstr = sfe.Message;
                    selectedJourneyLeg = 0;
				}
			}// end IR3597
			
			TDItineraryManager itinerary = TDItineraryManager.Current;
			JourneyControl.PublicJourney publicJourney = null;
			JourneyControl.RoadJourney roadJourney = null;

			if(selectedJourneyLeg == 0)
			{
				try
				{
					theMapControl.Map.ZoomToAllAddedRoutes();
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
				catch(ScaleZeroOrNegativeException szone)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

					Logger.Write(operationalEvent);
				}
				catch(EnvelopeZeroOrNegativeException ezone)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + ezone.Message);

					Logger.Write(operationalEvent);
				}
				catch(ScaleOutOfRangeException soore)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

					Logger.Write(operationalEvent);
				}
				catch(RouteInvalidException rie)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);
					
					Logger.Write(operationalEvent);
				}
				catch(NoPreviousExtentException npee)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

					Logger.Write(operationalEvent);
				}
				catch(MapExceptionGeneral meg)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);
						
					Logger.Write(operationalEvent);
				}
				// Write the selected journey leg "name" to the session for
				// printer friendly print page.

				if (theMapJourneyDisplayDetailsControl.SelectedJourneySegment != null)
				{
					if (outward)
						TDSessionManager.Current.InputPageState.MapViewTypeOutward =
							theMapJourneyDisplayDetailsControl.SelectedJourneySegment.Text;
					else
						TDSessionManager.Current.InputPageState.MapViewTypeReturn =
							theMapJourneyDisplayDetailsControl.SelectedJourneySegment.Text;
				}
		
			}
			else //selectedJourneyLeg == 0
			{
				// Need to determine which segment to be displayed - 
				// could be from any of the itinerary journeys.
				// Then call drawPublicJourey or drawPrivateJourney as usual.
				int total = 0;
				int index = 0;

				// Itinerary Manager stores Return journeys in same order as Outward journeys.
				// This code loops forward through aJourneys array if Outward, but loops 
				// backwards through the array if dealing with Return journeys.
				for (int i = outward ? 0 : (aJourneys.Length-1); outward ? i<aJourneys.Length : i >= 0; i = i + (outward ? 1 : (-1)))
				{
					if(aJourneys[i] is JourneyControl.PublicJourney)
					{
						publicJourney = (JourneyControl.PublicJourney)aJourneys[i];
						total += publicJourney.Details.Length;
						if (selectedJourneyLeg <= total)
						{
							// The segment belongs to this journey.
							index = (publicJourney.Details.Length + selectedJourneyLeg - total);
							drawPublicJourney( publicJourney, index);
							return;
						}
					}
					else
					{
						//Journey is a road journey.
						roadJourney = (JourneyControl.RoadJourney)aJourneys[i];
						total += 1;
						if (selectedJourneyLeg <= total)
						{
							// The segment belongs to this journey.
							drawPrivateJourney( roadJourney, 0);
							return;
						}
					}
				}
			}
		}

		/// <summary>
		/// Method to draw the symbol represtenting the Park and Ride destination and to 
		/// Create a map envelope around the entire journey. 
		/// </summary>
		/// <param name="originLocation">used to provide the grid reference of the origin</param>
		/// <param name="destinationLocation">used to provide the grid reference of the destination</param>
		/// <param name="privateViaLocation">used to provide the grid reference of the via point</param>
		private void drawParkAndRideJourney(TDLocation originLocation, TDLocation destinationLocation, TDLocation privateViaLocation)
		{
			if (outward)
			{
				//Origin journey: create the symbol for the destination of the park and ride scheme. 
				theMapControl.Map.AddSymbolPoint(
					destinationLocation.ParkAndRideScheme.SchemeGridReference.Easting,
					destinationLocation.ParkAndRideScheme.SchemeGridReference.Northing,
					"CIRCLE",
					destinationLocation.ParkAndRideScheme.Location.ToString()
					);

				OSGridReference[] osGridArray  = {originLocation.GridReference,
													 destinationLocation.CarPark.GridReference,
													 destinationLocation.ParkAndRideScheme.SchemeGridReference,
													 ((privateViaLocation !=null) ? privateViaLocation.GridReference : null)
												 };
				if(osGridArray.Length == 0)
					return;
				//create the map envelope using the osgrids that are provided. 
				createMapEnvelope(osGridArray);
			}
			else
			{
				//Return journey: create the symbol for the origin of the park and ride scheme. 
				theMapControl.Map.AddSymbolPoint(
					originLocation.ParkAndRideScheme.SchemeGridReference.Easting,
					originLocation.ParkAndRideScheme.SchemeGridReference.Northing,
					"CIRCLE",
					originLocation.ParkAndRideScheme.Location.ToString()
					);

				OSGridReference[] osGridArray  = {destinationLocation.GridReference,
													 originLocation.CarPark.GridReference,
													 originLocation.ParkAndRideScheme.SchemeGridReference,
													 ((privateViaLocation !=null) ? privateViaLocation.GridReference : null)
												 };
				if(osGridArray.Length == 0)
					return;
				//create the map envelope using the osgrids that are provided. 
				createMapEnvelope(osGridArray);
			}
		}

		/// <summary>
		/// Method uses and array of osgrid reference to determine the max and min eastings and northings
		/// it then passes this information to the zoom envelopme method to create a map that includes all
		/// the listed points. 
		/// </summary>
		/// <param name="osgr">Array of osgr's used to determine the outer boundaries of the journey map</param>
		private void createMapEnvelope(OSGridReference[] osgr)
		{

			double minEasting = double.MaxValue;
			double maxEasting = double.MinValue;
			double minNorthing = double.MaxValue;
			double maxNorthing = double.MinValue;

			for(int i = 0; i < osgr.Length; i++)
			{
				if (osgr[i] != null)
				{
					//compare Easting to current min and max
					if (osgr[i].Easting != -1)
					{
						minEasting = Math.Min( osgr[i].Easting, minEasting );
						maxEasting = Math.Max( osgr[i].Easting, maxEasting );
					}
					//compare Northing to current min and max
					if (osgr[i].Northing != -1)
					{
						minNorthing = Math.Min( osgr[i].Northing, minNorthing );
						maxNorthing = Math.Max( osgr[i].Northing, maxNorthing );
					}				
				}
			}

			double eastingPadding = Math.Max( (maxEasting - minEasting) / 20, 300 - (maxEasting - minEasting) / 2);
			minEasting = minEasting - eastingPadding;
			maxEasting = maxEasting + eastingPadding;

			double northingPadding = Math.Max( (maxNorthing - minNorthing) / 20, 300 - (maxNorthing - minNorthing) / 2);
			minNorthing = minNorthing - northingPadding;
			maxNorthing = maxNorthing + northingPadding;

			try
			{
				theMapControl.Map.ZoomToEnvelope( minEasting,minNorthing, maxEasting, maxNorthing );
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
			catch(ScaleZeroOrNegativeException szone)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

				Logger.Write(operationalEvent);
			}
			catch(EnvelopeZeroOrNegativeException ezone)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + ezone.Message);

				Logger.Write(operationalEvent);
			}
			catch(ScaleOutOfRangeException soore)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

				Logger.Write(operationalEvent);
			}
			catch(RouteInvalidException rie)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);
				
				Logger.Write(operationalEvent);
			}
			catch(NoPreviousExtentException npee)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

				Logger.Write(operationalEvent);
			}
			catch(MapExceptionGeneral meg)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);
					
				Logger.Write(operationalEvent);
			}

		}

		#endregion

		#region Handler for "Adjusted route" drop down list OK button (Car Journeys)

		/// <summary>
		/// Handles the "Adjusted route" drop down for Car Journeys.
		/// </summary>
		private void AddRoadJourneyDataToMap()
		{
			ITDJourneyResult result =TDItineraryManager.Current.JourneyResult;
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;

			try
			{
				// Clear any existing routes on the map
				theMapControl.Map.ClearPTRoutes();
				theMapControl.Map.ClearRoadRoutes();
                theMapControl.Map.ClearCycleRoute();

                SetPrinterDirectionsVisible(directionsVisible);

				if(outward)
				{
					// Update the Mapping component on what should be displayed so
					// that on the next OnPreRender the map is approriately refreshed.
					RoadJourney journey = result.OutwardRoadJourney();

					//Expand the routes for use
					SqlHelper sqlHelper = new SqlHelper();
					sqlHelper.ConnOpen(SqlHelperDatabase.EsriDB);
					Hashtable htParameters = new Hashtable(2);
					htParameters.Add("@SessionID", Session.SessionID);
					htParameters.Add("@RouteNum", journey.RouteNum);
					sqlHelper.Execute("usp_ExpandRoutes", htParameters);

					theMapControl.Map.AddRoadRoute(Session.SessionID, journey.RouteNum);
					theMapControl.Map.ZoomRoadRoute(Session.SessionID, journey.RouteNum);
				}
				else
				{
					RoadJourney journey = result.ReturnRoadJourney();

					//Expand the routes for use
					SqlHelper sqlHelper = new SqlHelper();
					sqlHelper.ConnOpen(SqlHelperDatabase.EsriDB);
					Hashtable htParameters = new Hashtable(2);
					htParameters.Add("@SessionID", Session.SessionID);
					htParameters.Add("@RouteNum", journey.RouteNum);
					sqlHelper.Execute("usp_ExpandRoutes", htParameters);

					theMapControl.Map.AddRoadRoute(Session.SessionID, journey.RouteNum);
					theMapControl.Map.ZoomRoadRoute(Session.SessionID, journey.RouteNum);
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
			catch(ScaleOutOfRangeException soore)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

				Logger.Write(operationalEvent);
			}
			catch(RouteInvalidException rie)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);
				
				Logger.Write(operationalEvent);
			}
			catch(NoPreviousExtentException npee)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

				Logger.Write(operationalEvent);
			}
			catch(MapExceptionGeneral meg)
			{
				// Log the exception
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + meg.Message);
			
				Logger.Write(operationalEvent);
			}

		}

		#endregion
		
		#region Handler for Directions Button

		/// <summary>
		/// Handles the Directions button in the CarDetailsButton to toggle
		/// whether the directions should be shown or not.
		/// </summary>
		private void imageButtonDirections_Click(object sender, EventArgs e)
		{
			// Check to see if directions are currently showing
			if(directionsVisible)
			{
				// Set directionsVisible to false so when the OnPreRender executes
				// it will not render the carJourneyDetailsControl
				directionsVisible = false;
				

				//Update the text of the image to the 'Show' version
				buttonDirections.Text = Global.tdResourceManager.GetString(
					"JourneyMapControl.ButtonDirectionsShow.Text", TDCultureInfo.CurrentUICulture);
			}
			else
			{
				// Set directionsVisible to true so when the OnPreRender executes
				// it will render the carJourneyDetailsControl
				directionsVisible = true;


				//Update the text of the image to the 'Hide' version
				buttonDirections.Text = Global.tdResourceManager.GetString(
					"JourneyMapControl.ButtonDirectionsHide.Text", TDCultureInfo.CurrentUICulture);
			}

			// saves if user selected show directions/not in session for print page
			SetPrinterDirectionsVisible(directionsVisible);
		}

        /// <summary>
        /// Handles the Cycle Directions button to toggle
        /// whether the directions should be shown or not.
        /// </summary>
        private void buttonDirectionsCycle_Click(object sender, EventArgs e)
        {
            // Check to see if directions are currently showing
            if (directionsCycleVisible)
            {
                // Set directionsVisible to false so when the OnPreRender in parent page executes
                // it will not render the cycle directions control
                directionsCycleVisible = false;

                //Update the text of the image to the 'Show' version
                buttonDirectionsCycle.Text = Global.tdResourceManager.GetString(
                    "JourneyMapControl.ButtonDirectionsShow.Text", TDCultureInfo.CurrentUICulture);
            }
            else
            {
                // Set directionsVisible to false so when the OnPreRender in parent page executes
                // it will render the cycle directions control
                directionsCycleVisible = true;

                //Update the text of the image to the 'Hide' version
                buttonDirectionsCycle.Text = Global.tdResourceManager.GetString(
                    "JourneyMapControl.ButtonDirectionsHide.Text", TDCultureInfo.CurrentUICulture);
            }

            // saves if user selected show directions/not in session for print page
            SetPrinterDirectionsVisible(directionsCycleVisible);
        }

		#endregion

		#region Handlers for the "Zoom" buttons on the MapZoomControl
		/// <summary>
		/// Handler for the Zoom Event.
		/// </summary>
		/// <param name="sender">Sender</param>
		/// <param name="e">Event arguments</param>
		private void ZoomMap(object sender, ZoomLevelEventArgs e)
		{
			if(e.ZoomLevel > 0)
				this.theMapControl.Map.SetScale(e.ZoomLevel);
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

		#endregion

		#region Method to set-up variables in session for printer friendly map page.
		/// <summary>
        /// Determines what the car/cycle directions flag in the session
		/// should be set to.  This is used for the printer friendly page.
		/// If the directions are visible on screen, they should be visible
		/// on the printer friendly page.  If the directions are not visible
		/// on screen, then it should not be visible on the printer friendly
		/// page.
		/// Only gets called when not using the Itinerary.
		/// </summary>
        private void SetPrinterDirectionsVisible(bool visible)
		{				
			// saves if user selected show directions/not in session for print page
			TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
			if (viewState != null)
			{
				if(outward)
					viewState.OutwardShowDirections = visible;
				else
					viewState.ReturnShowDirections = visible;
			}
		}

		/// <summary>
		/// Resets the text of the selected map to the default. This
		/// text is used in the Printer Friendly page.
		/// </summary>
		private void ResetSelectedMapTextInSession()
		{
			// Initialise the selected map text for the printer friendly
			// map to the string stating map of the full journey.

			string textToWrite = Global.tdResourceManager.GetString(
				"DataServices.MapsForThisJourneyDrop.FullJourney", TDCultureInfo.CurrentUICulture);

			if (outward)
			{
				TDSessionManager.Current.InputPageState.MapViewTypeOutward = textToWrite;
				
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
			else
			{
				TDSessionManager.Current.InputPageState.MapViewTypeReturn = textToWrite;
			
				if(TDSessionManager.Current.InputPageState.IconSelectionReturn != null)
				{
					/// Clear the selected icons in the session.
					for( int i=0;
						i < TDSessionManager.Current.InputPageState.IconSelectionReturn.Length;
						i ++ )
					{
						for( int j=0;
							j < TDSessionManager.Current.InputPageState.IconSelectionReturn[i].Length;
							j ++)
						{
							TDSessionManager.Current.InputPageState.IconSelectionReturn[i][j] = false;
						}
					}
				}
			}
		}

		#endregion

		#region Method to call when required to update the map

		/// <summary>
		/// If user has extended the journey then the journeys that make up the 
		/// Itinerary will be returned. If not then the single journey from 
		/// the SesssionManager will be returned.
		/// </summary>
		/// <returns>Array of Journey objects</returns>
		private Journey[] getCurrentJourneys() 
		{
			TDItineraryManager itinerary = TDItineraryManager.Current;
			ITDSessionManager sessionManager = TDSessionManager.Current;

			if (sessionManager.ItineraryMode != ItineraryManagerMode.None)
			{
				if (outward)
				{
					return itinerary.OutwardJourneyItinerary;
				}
				else
				{
					return itinerary.ReturnJourneyItinerary;
				}
			}
			else
			{
				TDJourneyViewState viewState = sessionManager.JourneyViewState;
				ITDJourneyResult result = sessionManager.JourneyResult;
                ITDCyclePlannerResult cycleResult = sessionManager.CycleResult;
				Journey journey = null;

				bool arriveBefore = outward ? viewState.JourneyLeavingTimeSearchType : viewState.JourneyReturningTimeSearchType;
                int selectedJourney = outward ? viewState.SelectedOutwardJourney : viewState.SelectedReturnJourney;
                
                JourneySummaryLine[] summaryLine;

                // get the summary line from the session Cycle result or Journey result
                if (sessionManager.FindAMode == FindAMode.Cycle)
                {
                    summaryLine = outward ? cycleResult.OutwardJourneySummary(arriveBefore, modeTypes) : cycleResult.ReturnJourneySummary(arriveBefore, modeTypes);
                }
                else
                {
                    summaryLine = outward ? result.OutwardJourneySummary(arriveBefore, modeTypes) : result.ReturnJourneySummary(arriveBefore, modeTypes);
                }

                if ((TDSessionManager.Current.FindAMode == FindAMode.Trunk || TDSessionManager.Current.FindAMode == FindAMode.TrunkStation) && TDSessionManager.Current.FindPageState.ITPJourney)
                {
                    List<JourneySummaryLine> itpjourneys = new List<JourneySummaryLine>();

                    foreach (JourneySummaryLine jsl in summaryLine)
                    {
                        ArrayList journeyModes = new ArrayList();
                        journeyModes.AddRange(jsl.Modes);

                        bool coachmode = ((journeyModes.Contains(ModeType.Coach)) || (journeyModes.Contains(ModeType.Bus)));
                        bool trainmode = ((journeyModes.Contains(ModeType.Rail))
                                   ||
                                 (journeyModes.Contains(ModeType.RailReplacementBus))
                                   ||
                                 (journeyModes.Contains(ModeType.Metro))
                                   ||
                                 (journeyModes.Contains(ModeType.Tram))
                                   ||
                                 (journeyModes.Contains(ModeType.Underground)));

                        bool airmode = journeyModes.Contains(ModeType.Air);

                        if (airmode && (coachmode || trainmode))
                            itpjourneys.Add(jsl);
                        if (coachmode && trainmode && !airmode)
                            itpjourneys.Add(jsl);


                    }

                    summaryLine = itpjourneys.ToArray();
                }
                
				JourneySummaryLine summary = summaryLine[selectedJourney];

				if(outward)
				{
					if (summary.Type == TDJourneyType.PublicOriginal)
						journey = result.OutwardPublicJourney(summary.JourneyIndex);
					else if (summary.Type == TDJourneyType.PublicAmended)
						journey = result.AmendedOutwardPublicJourney;
                    else if (summary.Type == TDJourneyType.Cycle)
                        journey = cycleResult.OutwardCycleJourney(summary.JourneyIndex);
					else
						journey = result.OutwardRoadJourney();
				}
				else
				{
					if (summary.Type == TDJourneyType.PublicOriginal)
						journey = result.ReturnPublicJourney(summary.JourneyIndex);
					else if (summary.Type == TDJourneyType.PublicAmended)
						journey = result.AmendedReturnPublicJourney;
                    else if (summary.Type == TDJourneyType.Cycle)
                        journey = cycleResult.ReturnCycleJourney(summary.JourneyIndex);
					else
						journey = result.ReturnRoadJourney();
				}

				return new Journey[] {journey};
			}

		}

		/// <summary>
		/// Updates the map component to display the selected journey.
		/// </summary>
		public void UpdateMapDisplay()
		{
			itinerarySegmentSelected = ((usingItinerary) && (!TDItineraryManager.Current.FullItinerarySelected));

			theMapJourneyDisplayDetailsControl.ClearSelection();

			Journey[] aJourneys = getCurrentJourneys();

			//check for a public journey when the previous was a private journey, to display the default public 
			//transport map symbols
			if (((mapHelper.PublicOutwardJourney) || (mapHelper.PublicReturnJourney)) && previousPublic == false)
			{
				theMapLocationIconsSelectControl.CheckTransportSection();
				this.previousPublic = true;
			}	

			//check for private outward journey.  Sets map symbols to car defaults
			if (outward && mapHelper.PrivateOutwardJourney)
			{
				theMapLocationIconsSelectControl.TransportSectionCar();
				previousPublic = false;
			}
				
			//check for private return journey.  Sets map symbols to car defaults
			if (!outward && mapHelper.PrivateReturnJourney)
			{
				theMapLocationIconsSelectControl.TransportSectionCar();
				previousPublic = false;
			}

			// Call update map with the journey to update the map
			UpdateMap(aJourneys);
			// Set flag so that UpdateMap is not called again on preRender.
			this.mapUpdated = true;

			// Reset the selected map in session (used for printer friendly)
			ResetSelectedMapTextInSession();

		}

		#endregion
	
		#region Rounding method

		/// <summary>
		/// Rounds the given double to the nearest int.
		/// If double is 0.5, then rounds up.
		/// Using this instead of Math.Round because Math.Round
		/// ALWAYS returns the even number when rounding a .5 -
		/// this is not behaviour we want.
		/// </summary>
		/// <param name="valueToRound">Value to round.</param>
		/// <returns>Nearest integer</returns>
		private static int Round(double valueToRound)
		{
			// Get the decimal point
			double valueFloored = Math.Floor(valueToRound);
			double remain = valueToRound - valueFloored;

			if(remain >= 0.5)
				return (int)Math.Ceiling(valueToRound);
			else
				return (int)Math.Floor(valueToRound);
		}

		#endregion

		#region Public methods
		
		/// <summary>
		///  Updates the map view state held in the session manager with the current
		///  state of the map. This can then be retrieved in necessary.
		///  E.g. after an IndirectLocationPostBack (returning from Help page)
		/// </summary>
		public void SaveMapViewState()
		{
			// Store away the map viewstate
			object o = theMapControl.Map.ExtractViewState();
			TDSessionManager.Current.StoredMapViewState[ this.outward ? TDSessionManager.OUTWARDMAP : TDSessionManager.RETURNMAP ] = o;
		}

		/// <summary>
		/// Allows calling code to select a new leg in the dropdown combo box and then
		/// updates the map accordingly.
		/// </summary>
		private void ShowSelectedMapLeg(int LegNumber)
		{
			// Set the newly selected leg in the dropdown combo box.
			// This value will then be referenced in the private ShowSelectedMapLeg method so the
			// correct leg is picked up and shown
			if (theMapJourneyDisplayDetailsControl.DropDownListJourneySegment.Items.Count > 1)
			{
				theMapJourneyDisplayDetailsControl.SelectedJourneyValue = LegNumber.ToString();
				ShowSelectedMapLeg();

			}
			else
			{
				theMapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex = LegNumber;
				ShowSelectedMapLeg();
			}
		}

		/// <summary>
		/// Allows calling code to select a new leg in the dropdown combo box and then
		/// updates the map accordingly.
		/// </summary>
		public void ShowSelectedCarMapLeg(string journeyDropDown)
		{
			// Set the newly selected leg in the dropdown combo box.
			// This value will then be referenced in the private ShowSelectedMapLeg method so the
			// correct leg is picked up and shown
			theMapJourneyDisplayDetailsControl.SelectedJourneyValue = journeyDropDown;
			ShowSelectedMapLeg();
		}

		/// <summary>
		/// Draws the map according to the leg selected in the dropdown combo box
		/// </summary>
		private void ShowSelectedMapLeg()
		{

			if(theMapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex > 0)
			{
				int JourneyValue = theMapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex;

				int JourneySectionNumber = Convert.ToInt32(JourneyValue);
				TDSessionManager.Current.JourneyMapState.SelectedJourneySegment
					= JourneySectionNumber;
			}

			// Get the selected journey
			Journey journey;
			
			if (usingItinerary && !itinerarySegmentSelected)
			{
				// Showing whole itinerary i.e. multiple journeys on map.
				drawItineraryJourneys();	
			}
			else
			{
				if(outward)
				{
                    journey = mapHelper.FindRelevantJourney(outward);

                    if (TDSessionManager.Current.JourneyMapState.SelectedJourneySegment <= 0)
					{
						TDSessionManager.Current.JourneyMapState.SelectedJourneySegment= 0;
					}
				}
				else
				{
                    journey = mapHelper.FindRelevantJourney(outward);

					// Retain selected index, incase we need to re-set it.
					TDSessionManager.Current.ReturnJourneyMapState.SelectedJourneySegment 
						= theMapJourneyDisplayDetailsControl.SelectedJourneySegmentIndex;
				}

				// Update the mapping component.
				if(journey is JourneyControl.PublicJourney)
				{
					drawPublicJourney( journey );
				}
                else if (journey is CyclePlannerControl.CycleJourney)
                {
                    drawCycleJourney(journey);
                }
				else
				{
					drawPrivateJourney( journey );
				} 
			}
		}
        
        #endregion

        #region Public properties
		/// <summary>
		/// Get/Set property to get/set visibility of the directions label.
		/// </summary>
		public bool DirectionsButtonVisible
		{
			get
			{
				return buttonDirections.Visible;
			}
			set
			{
				buttonDirections.Visible = value;
			}
		}

		public bool DirectionsVisible
		{
			get
			{
				return directionsVisible;
			}
			set
			{
				directionsVisible= value;
			}
		}

        /// <summary>
        /// Get/Set property to get/set visibility of the directions label.
        /// </summary>
        public bool DirectionsCycleButtonVisible
        {
            get
            {
                return buttonDirectionsCycle.Visible;
            }
            set
            {
                buttonDirectionsCycle.Visible = value;
            }
        }

        /// <summary>
        /// Get/Set property to get/set visibility of the cycle directions labels
        /// </summary>
        public bool DirectionsCycleVisible
        {
            get
            {
                return directionsCycleVisible;
            }
            set
            {
                directionsCycleVisible = value;
            }
        }

        /// <summary>
        /// Read/Write Property. Gets/sets width of the map
        /// </summary>
        public Unit MapWidth
        {
            get
            {
                return theMapControl.Map.Width;
            }
            set
            {
                theMapControl.Map.Width = value;
            }
        }

        /// <summary>
        /// Read/Write Property. Gets/sets height of the map
        /// </summary>
        public Unit MapHeight
        {
            get
            {
                return theMapControl.Map.Height;
            }
            set
            {
                theMapControl.Map.Height = value;
            }
        }

		public MapControl mapControl
		{
			get
			{
				return theMapControl;
			}
			set
			{
				mapControl = theMapControl;
			}
		}

		public MapJourneyDisplayDetailsControl JourneyDisplayDetailsControl
		{
			get
			{
				return theMapJourneyDisplayDetailsControl;
			}
			set
			{
				JourneyDisplayDetailsControl = theMapJourneyDisplayDetailsControl;
			}
		}	
	
		public string firstElementID
		{
			get
			{
				return this.labelMaps.ClientID;
			}
			set
			{
				firstElementID = value;
			}
		}

		public int SelectedLeg
		{
			get
			{
				return selectedLeg;
			}
			set
			{
				selectedLeg = value;
			}
		}

		public string journeyDropDown
		{
			get
			{
				return JourneyDropDown;
			}
			set
			{
				JourneyDropDown = value;
			}
		}

        public bool OverviewVisible
        {
            get { return mapOverviewContainer.Visible; }
            set { mapOverviewContainer.Visible = value; }
        }

        
		
        /// <summary>
		/// Read/write property, used to specify whether 
		/// the MapLocationControl part of this control 
		/// should be fully available or just show text.
        /// </summary>
		public bool ShowMapLocationControlTextOnly
		{
			get 
			{
				return showMapLocationControlTextOnly;
			}
			set 
			{
				showMapLocationControlTextOnly = value;
			}
		}

		/// <summary>
		/// Read/write property used to specify
		/// whether to show or hide the title area
		/// </summary>
		public bool DisplayTitle 
		{
			get { return displayTitle; }
			set { displayTitle = value; }
		}

		/// <summary>
		/// Public write only property to flag whether there is a 
		/// CJP error in a VisitPlanner journey result.
		/// </summary>		
		public bool IsError
		{
			get {return isError;}
			set { isError = value; }
        }

        #endregion
    }
}
