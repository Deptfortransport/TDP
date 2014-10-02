#region Header and version history
// *********************************************** 
// NAME                 : JourneyDetailsSegmentsControl.ascx.cs 
// AUTHOR               : James Broome
// DATE CREATED         : 16/06/2004
// DESCRIPTION          : A custom control to display
// details of a given journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyDetailsSegmentsControl.ascx.cs-arc  $ 
//
//   Rev 1.46   Mar 21 2013 10:13:06   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.45   Mar 19 2013 12:05:28   mmodi
//Updates to accessible icons and display of debug info from PublicJourneyDetail
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.44   Feb 11 2013 11:15:44   mmodi
//Dont display walkit link for accessible journey start end naptan to naptan leg
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.43   Feb 11 2013 10:01:12   mmodi
//Do not display walkit link for accessible journey naptan to naptan leg
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.42   Feb 05 2013 13:21:12   mmodi
//Show Walk Interchange in map journey leg dropdown for accessible journey when required
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.41   Jan 29 2013 14:12:52   DLane
//Update to interchange presentation
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.40   Jan 20 2013 16:26:58   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.39   Dec 05 2012 13:46:38   mmodi
//Display accessible features, and improves layout for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.38   Oct 11 2012 14:05:52   mmodi
//Updated cjp user output information styling
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.37   Nov 08 2010 08:48:28   apatel
//Updated to remove CJP additional information for Trunk Exchange Point and interchange time
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.36   Oct 27 2010 11:16:36   apatel
//Updated to add Error handling for CJP power user additional information 
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.35   Oct 26 2010 14:30:24   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.34   Sep 14 2010 09:49:16   apatel
//Updated to correct the walkit link for return journey
//Resolution for 5603: Return Journey - Walkit Links Wrong
//
//   Rev 1.33   May 04 2010 11:11:16   apatel
//Revert the changes made for printable vertical line between nodes
//Resolution for 5502: Printer friendly page issue with vertical journey node lines
//
//   Rev 1.31   Apr 07 2010 16:06:48   mmodi
//Updated/added alternate text to map and walkit links for accessability
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.30   Mar 10 2010 14:07:48   mturner
//Updated to not display the WalkIT control if you are in the middle of the Modify Journey process.
//Resolution for 5446: Walkit link on Modify pages causes page to display incorrectly
//
//   Rev 1.29   Feb 26 2010 15:54:44   mmodi
//Correctly set Arrival boards link for international journey using the previous detail's arrival link
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.28   Feb 26 2010 11:04:16   apatel
//Updated for XHTML Compliance
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.27   Feb 26 2010 09:51:22   apatel
//Updated for International Planner to show checkin and exit time for Rail and Coach. Also, Updated to show international stop location as links if links available for them.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.26   Feb 25 2010 16:20:56   pghumra
//Code fix applied to resolve issue with date not being displayed on journey details section in the door to door planner when date of travel is different to requested date
//Resolution for 5413: CODE FIX - NEW - DEL 10.x - Issue with seasonal information change from Del 10.8
//
//   Rev 1.25   Feb 21 2010 23:22:52   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.24   Feb 17 2010 15:13:34   apatel
//International planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.23   Feb 16 2010 11:15:44   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.22   Feb 12 2010 11:13:32   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.21   Jan 07 2010 13:38:58   apatel
//Updated for Walkit control on printer friendly page
//Resolution for 5357: Printer friendly page issue of header and walkit links
//
//   Rev 1.20   Dec 08 2009 15:59:02   apatel
//Walkit link code
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.19   Dec 04 2009 11:16:54   apatel
//Walkit control update to put walkit logo
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.18   Nov 15 2009 11:05:18   mmodi
//Updated to add map button javascript to zoom the map to the selected leg
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.17   Nov 11 2009 16:42:52   pghumra
//Changes for CCN0538 Page Land on Walkit.com
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.16   Oct 23 2009 09:03:14   apatel
//Seasonal page link and next day journeys code changes for Day trip planner
//
//   Rev 1.15   Oct 15 2009 13:37:54   apatel
//Seasonal page link and next day journeys changes
//
//   Rev 1.14   Sep 14 2009 10:55:28   apatel
//Stop Information page changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.13   Feb 02 2009 17:07:40   mmodi
//Display routing guide compliant flag when logged in as a CJP user
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.12   Nov 20 2008 10:15:14   pscott
//Change image url for arrivals for air
//
//   Rev 1.11   Oct 24 2008 13:53:32   jfrank
//Updated for XHTML compliance
//Resolution for 5146: WAI AAA copmpliance work (CCN 474)
//
//   Rev 1.10   Oct 22 2008 12:03:44   pscott
//SCR5148 Add arrival Boards Icon
//Resolution for 5148: Add Arrivals Icon to journey Summary Page
//
//   Rev 1.9   Oct 13 2008 16:41:44   build
//Automatically merged from branch for stream5014
//
//   Rev 1.8.2.1   Sep 26 2008 13:44:04   jfrank
//Amended to make XHTML transitional
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.8.2.0   Aug 27 2008 14:16:02   pscott
//UK:3104684 - apply latest departure board changes to cycle planner stream
//
//   Rev 1.8.1.0   Aug 06 2008 09:43:06   pscott
//SCR 5093 - visibility of map icon when modify /adjust side by side comparison is shown 
//
//   Rev 1.8   Jul 29 2008 11:14:40   pscott
//replace map button by image and hyperlink
//
//   Rev 1.7   Jul 24 2008 16:22:18   mmodi
//Corrected server error when no departure board url for an airport
//
//   Rev 1.6   Jul 23 2008 15:11:54   mmodi
//Departure and arrivals board links are not shown when adjusting a journey
//Resolution for 5085: Departure board link should not be shown on the Modify Adjust journey page
//
//   Rev 1.5   Jul 11 2008 10:59:54   pscott
//fix flight arrival board url
//
//   Rev 1.4   Jul 08 2008 10:55:46   pscott
//DFt post review changes to departureboard visibiliy mods
//
//   Rev 1.3   Jun 27 2008 12:12:28   pscott
//5011 - Departure board visibility changes
//
//   Rev 1.2   Mar 31 2008 13:21:18   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory  Mar 24 2007 18:00:00   mmodi
//Check searchtype when displaying car park location at start/end of journey
//
//   Rev 1.0   Nov 08 2007 13:15:18   mturner
//Initial revision.
//
//   Rev 1.36   Oct 11 2007 14:45:50   asinclair
//Added code to allow the Journey Number to be displayed
//Resolution for 4513: 9.8 - Journey Leg Numbering
//
//   Rev 1.35   Nov 09 2006 12:30:40   mmodi
//Removed Car Park opening times note
//Resolution for 4248: Del 9.1: Remove Opening time note on Car Park Information page
//
//   Rev 1.34   Oct 06 2006 14:40:26   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.33.1.4   Sep 26 2006 16:40:06   esevern
//Amendments to display opening times note correctly when extending journey
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4164: Car Parking: Car park note is not displayed in Details view
//
//   Rev 1.33.1.3   Sep 26 2006 12:25:36   esevern
//Added car parking opening times note label. Added setting of LegInstructionsControl.ShowInGrid property so that opening times note will only be displayed for the leg instructions if the journey is being shown in a table
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4164: Car Parking: Car park note is not displayed in Details view
//
//   Rev 1.33.1.2   Sep 21 2006 17:01:58   esevern
//Amendment to CarParking changes - was incorrectly displaying a blank car park information page for no car park locations (the start/end location of an intermediate leg)
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4163: Car Parking: Car park is not shown as a link in Details view
//
//   Rev 1.33.1.1   Sep 20 2006 17:01:12   esevern
//Added setting of car park reference in location hyperlink command name if start/end location is a car park
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4163: Car Parking: Car park is not shown as a link in Details view
//
//   Rev 1.33.1.0   Sep 05 2006 15:29:34   esevern
//changes to info available methods to show car park as a hyperlink if its the start/end location
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.33   Apr 19 2006 12:00:34   RGriffith
//IR3938 Fix: Text labels appeared incorrect on Mac (Netscape 7.2)
//Resolution for 3938: DN068 Replan, Adjust, Extend: Diagram instruction text unclear on Mac/Netscape
//
//   Rev 1.32   Apr 05 2006 15:24:16   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.31   Mar 31 2006 10:40:38   NMoorhouse
//fix problem with walking leg times appearing
//Resolution for 3714: DN068 Replan: Replacing a walk leg preceding a bus service produces odd journey times
//
//   Rev 1.30   Mar 30 2006 10:55:52   NMoorhouse
//Ensure we're not display 'None' date/times for (footer) arrivals
//Resolution for 3714: DN068 Replan: Replacing a walk leg preceding a bus service produces odd journey times
//
//   Rev 1.29   Mar 28 2006 17:33:10   NMoorhouse
//Do not display 'None' date/times
//Resolution for 3714: DN068 Replan: Replacing a walk leg preceding a bus service produces odd journey times
//
//   Rev 1.28   Mar 20 2006 16:49:54   NMoorhouse
//Provent the mode detail icons (train and car) from being clickable on the printer friendly pages
//Resolution for 3644: clcikable icon on printer friendly page
//
//   Rev 1.27   Mar 20 2006 16:34:20   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.26   Mar 14 2006 19:49:58   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.25   Mar 13 2006 16:54:46   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21.2.4   Mar 08 2006 16:24:28   tmollart
//Changes for time rounding errors.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21.2.3   Mar 08 2006 09:19:08   pcross
//Removed adjust earlier / later buttons
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21.2.2   Feb 24 2006 14:34:22   NMoorhouse
//Changes to support the addition of new page to display CarDetails
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21.2.1   Feb 10 2006 15:35:26   RGriffith
//Removal of ArriverEarlier / ArriveLater buttons
//
//   Rev 1.21.2.0   Jan 26 2006 20:39:26   rhopkins
//Changed to use Journey instead of PublicJourney and JourneyLeg instead of PublicJourneyDetails.  This allows proper mode-agnostic handling.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24   Mar 09 2006 15:49:26   pscott
//SCR 3512
//Remove map buttons when in Journey Adjust
//
//   Rev 1.23   Feb 23 2006 16:12:00   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.22   Feb 17 2006 11:57:10   halkatib
//Added fixes for IR3573
//
//   Rev 1.21   Nov 24 2005 12:51:28   rhopkins
//Revert search for imageButtonMode so that it looks for an ImageButton, not a TDButton, so that the event handler gets attached.  (imageButtonMode is an icon and cannot be turned into a TDButton.)
//Resolution for 3095: VisitPlanner  - Train hyperlink on journey summary page does not display intermediate stops
//
//   Rev 1.20   Nov 15 2005 16:15:12   RGriffith
//Resolution for IR3058 - Tooltip incorrectly set
//
//   Rev 1.19   Nov 09 2005 16:58:48   jgeorge
//Manual merge for stream2818 (Search by Price)
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.18   Nov 09 2005 15:17:56   rgreenwood
//TD089 ES020 code review actions
//
//   Rev 1.16   Nov 01 2005 15:11:36   build
//Automatically merged from branch for stream2638
//
//   Rev 1.15.1.0   Sep 30 2005 13:47:54   jbroome
//Amended to ensure correct alignment of columns
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.15   Aug 31 2005 12:00:24   RPhilpott
//Don't show frequency of service as a range if min and max frequencies are the same.
//Resolution for 2741: DN062: frequency of freq-based services
//
//   Rev 1.14   Aug 19 2005 14:07:24   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.13.1.12   Aug 16 2005 15:12:32   RPhilpott
//FxCop fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.11   Aug 12 2005 18:28:16   RPhilpott
//Remove interchange icon for "missing" short walks.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.10   Aug 11 2005 10:05:56   RPhilpott
//Error if interchange at start of final leg.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.9   Aug 09 2005 18:45:48   RPhilpott
//Add support for "missing" short walks omitted by CJP -- insert new "interchange" pseudo-leg.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.8   Jul 28 2005 16:04:44   RPhilpott
//Service Details changes. 
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.7   Jul 25 2005 18:12:34   rgreenwood
//DD073 Map Details: Added MapButtonVisible() method, removed MapButtonVisible property
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.6   Jul 25 2005 15:40:44   pcross
//Handle multiple network map links
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.5   Jul 25 2005 15:05:26   RWilby
//Added GetDisplayNotes methods
//
//   Rev 1.13.1.4   Jul 22 2005 20:03:16   RPhilpott
//Changes to link to new ServiceDetails page, and to render location names as links to Info pages.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.3   Jul 19 2005 18:42:18   pcross
//Removed some resource fetching that is no longer needed now LegInstructionsControl is used.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.2   Jul 19 2005 18:17:00   pcross
//Added Operator Links
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.1   Jul 19 2005 10:59:36   pcross
//Addded Network Map Links control
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13.1.0   Jul 11 2005 11:14:30   rgreenwood
//DN062: Added VehicleFeaturesControl
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.13   Apr 26 2005 13:18:30   pcross
//IR2192. Corrections to extended journey handling
//
//   Rev 1.12   Apr 22 2005 16:03:44   pcross
//IR2192. Raises event when map button is pressed instead of opening a new page. Then the host control can react accordingly.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.11   Oct 08 2004 15:37:26   RPhilpott
//Check for continuous legs instead of just for Mode.Walk, to handle taxis correctly.
//Resolution for 1697: Taxi legs not being treated as continuous legs
//
//   Rev 1.10   Oct 08 2004 14:12:52   RPhilpott
//Allow for possibility of no check-in or check-out legs around a flight leg (to cater for Scottish "internal" flights in door-to-door).
//Resolution for 1694: Exception when looking at details of "internal" Scottish flights
//
//   Rev 1.9   Sep 30 2004 12:33:26   RGeraghty
//Changes made to pick up new image alternate text
//
//   Rev 1.8   Sep 21 2004 17:11:08   RPhilpott
//Cater for empty naptan fields returned by CJP/travelines by returning an empty string to client code in this case.
//Resolution for 1614: Exception if no Naptan returned by CJP
//
//   Rev 1.7   Aug 09 2004 16:15:08   jbroome
//IR 1258 - Ensure consistency of rounded time values.
//
//   Rev 1.6   Jul 26 2004 17:27:54   rgreenwood
//IR 1112 added "driveFromText" variable name to label text assignments.
//
//   Rev 1.5   Jul 12 2004 15:10:20   jbroome
//Actioned Extend Journey code review comments.
//
//   Rev 1.4   Jul 01 2004 16:47:14   jmorrissey
//Updated because the check in time and exit time for a flight are no longer separate journey legs
//
//   Rev 1.3   Jun 23 2004 15:34:06   JHaydock
//Updates/Corrections for JourneyDetails page
//
//   Rev 1.2   Jun 17 2004 17:22:44   jbroome
//Corrected all HTML formatting issues. Store correct journey from itinerary when clicking Map button.
//
//   Rev 1.1   Jun 16 2004 09:25:36   jbroome
//Control created from existing functionality / HTML in JourneyDetailsControl. Can now be used to show multiple journeys.

#endregion

#region Using Statements

using System;
using System.Collections;
using System.Data;
using System.Text;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Logger = System.Diagnostics.Trace;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Web.Support;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.JourneyPlanRunner;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.Web.Adapters;
using System.Web.UI;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.AdditionalDataModule;
using TransportDirect.UserPortal.LocationInformationService;
using System.Collections.Generic;


#endregion

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Control to render the segments of a given journey.
	/// </summary>
	public partial  class JourneyDetailsSegmentsControl : TDPrintableUserControl
    {
        #region Private members
        
        #region Controls

        protected VehicleFeaturesControl vehicleFeaturesControl;
        protected AccessibleFeaturesControl legAccessibleFeaturesControl;
        protected AccessibleFeaturesControl locationAccessibleFeaturesControl;
        protected AccessibleInstructionControl accessibleInstructionControl;
		protected NetworkMapLinksControl networkMapLink;
		protected LegInstructionsControl legInstructionsControl;
        protected WalkitLinkControl walkitLink;
        protected HyperLink walkitImageLink;
        protected HyperLink walkitDirectionsLink;
        protected System.Web.UI.WebControls.Image walkitImage;
		protected HyperlinkPostbackControl modeLinkControl;
		protected HyperlinkPostbackControl locationInfoLinkControl;
        protected HyperLink locationInfoLink;
		protected Label locationLabelControl;
		protected HyperlinkPostbackControl alightLocationInfoLinkControl;
		protected Label alightLocationLabelControl;
		protected HyperlinkPostbackControl endLocationInfoLinkControl;
        protected HyperLink endLocationInfoLink;
		protected Label endLocationLabelControl;
		protected Label startCarParkLabel;
		protected Label endCarParkLabel;
        
		protected TableRow interchangeTableRow1;
		protected TableRow interchangeTableRow2;
		protected TableRow interchangeTableRow3;
		protected TableRow interchangeTableRow4;
		protected TableRow interchangeTableRow5;
		protected TableRow interchangeTableRow6;
		protected TableRow interchangeTableRow7;

        #endregion

        // Data for the Repeater.
		private JourneyControl.Journey journey;

		private bool outward = true;
		private bool isEndCarPark = false;
		private bool isStartCarPark = false;
        private SearchType searchType = SearchType.AddressPostCode;
		private ArrayList uniqueNameList; // list of unique car park references
		private bool useNameList;

		private bool compareMode;
		private bool adjustable;
		private bool stationInfo;
		private bool firstJourney;	
		private bool lastJourney;
		private int journeyItineraryIndex = -1;

		private int journeyIconCount = 0;

        // Variables needed to add javascript to the map button click
        private bool addMapJavascript = false;
        private string mapId = "map";
        private string mapJourneyDisplayDetailsDropDownId = "mapdropdown";
        private string scrollToControlId = "mapControl";
        private string sessionId = "session";
        private int journeyRouteNumber = 0;
        private string journeyType = "PT";
        private MapHelper mapHelper = new MapHelper();
        
        private PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter();

        // Used to display accessible journey elements
        private bool showAccessibleFeatures;
        private bool showAccessibleAssistanceInfo;
        private bool showAccessibleStepFreeInfo;

        private bool showAccessibleRail = true;


        private bool isCJPUser = false;

        private ITDJourneyRequest tdjourneyRequest;
        
		// Event to fire when the Map button is pressed
		public event MapButtonClickEventHandler MapButtonClicked;

        protected TDImageButton imageMapButton;
        protected HyperlinkPostbackControl hyperlinkPostbackControlMapButton;

        protected HyperLink hyperlinkDepartureBoard;
        protected HyperLink hyperlinkDepartureBoardLink;
        protected HyperLink hyperlinkArrivalBoard;
        protected HyperLink hyperlinkArrivalBoardLink;

        private const string originNaptanString = "Origin";
		private const string destinationNaptanString = "Destination";

        private FindAMode findAMode = FindAMode.None;

        #region Web holders and image definitions

        private string imageEndNodeUrl = String.Empty;
		private string imageStartNodeUrl = String.Empty;
		private string imageIntermediateNodeUrl = String.Empty;
		private string imageNodeConnectorUrl = String.Empty;

		private string imageCarUrl = String.Empty;
		private string imageAirUrl = String.Empty;
		private string imageBusUrl = String.Empty;
		private string imageCoachUrl = String.Empty;
		private string imageCycleUrl = String.Empty;
		private string imageDrtUrl = String.Empty;
		private string imageFerryUrl = String.Empty;
		private string imageMetroUrl = String.Empty;
		private string imageRailUrl = String.Empty;
		private string imageTaxiUrl = String.Empty;
        private string imageTelecabineUrl = String.Empty;
		private string imageTramUrl = String.Empty;
		private string imageUndergroundUrl = String.Empty;
        private string imageWalkUrl = String.Empty;
        private string imageWalkInterchangeUrl = String.Empty;
		private string imageRailReplacementBusUrl = String.Empty;
        private string imageWalkitUrl = string.Empty;
        private string imageTransferUrl = string.Empty;

		private string imageMapUrl = String.Empty;
        private string imageArrivalBoardUrl = String.Empty;
        private string imageDepartureBoardUrl = String.Empty;
        private string imageArriveEarlierUrl = String.Empty;
		private string imageLeaveLaterUrl = String.Empty;
		private string imageFindTransportFromUrl = String.Empty;
		private string imageFindTransportToUrl = String.Empty;
		private string imageFindTransportFromGreyUrl = String.Empty;
		private string imageFindTransportToGreyUrl = String.Empty;
		private string imageSpacerUrl = String.Empty;

		// Alternate Text for images and links
		private string alternateTextStartNode = String.Empty;
		private string alternateTextEndNode = String.Empty;
		private string alternateTextIntermediateNode = String.Empty;
		private string alternateTextNodeConnector = String.Empty;
		private string toolTipTextLocationLink = String.Empty;
        private string toolTipTextLocationAccessibleLink = String.Empty;
		private string toolTipTextDetailsLink = String.Empty;
		private string toolTipTextInformationButton = String.Empty;
		private string textFindTransportFromButton = String.Empty;
		private string textFindTransportToButton = String.Empty;
		private string textFindTransportFromGreyButton = String.Empty;
		private string textFindTransportToGreyButton = String.Empty;
        private string alternateTextWalkit = string.Empty;
        private string alternateTextWalkitImageLink = string.Empty;
        private string walkitDirectionText = string.Empty;

		private string checkinText = String.Empty;
		private string exitText = String.Empty;
		private string leaveText = String.Empty;
		private string arriveText = String.Empty;
		private string departText = String.Empty;
        private string minsText = String.Empty;
        private string minutesText = String.Empty;
		private string minText = String.Empty;
		private string maxDurationText = String.Empty;
		private string typicalDurationText = String.Empty;
		private string startText = String.Empty;
		private string endText = String.Empty;
		private string hoursText = String.Empty;
		private string hourText = String.Empty;
		private string secondsText = String.Empty;
		private string driveToText = String.Empty;

        #endregion

        #region CJPUserInfo controls
        private CJPUserInfoControl cjpUserLocationNaptanInfo;
        private CJPUserInfoControl cjpUserLocationCoordinateInfo;
        private CJPUserInfoControl cjpUserInfoWalkLength;
        private CJPUserInfoControl cjpUserInfoJourneyLegSource;
        private CJPUserInfoControl cjpUserInfoDisplayNotes;
        private CJPUserInfoControl cjpUserInfoLegDebugInfo;
        #endregion

        #endregion

        #region Initialisation and initialisation properties
        /// <summary>
		/// Initialises the control. This method must be called.
		/// </summary>
		/// <param name="adjustable">Indicates if the control is being rendered in adjust mode.</param>
		/// <param name="compareMode">Indicates if the control is being rendered in compare mode.</param>
		/// <param name="outward">Indicates if the control is being rendered for outward or return journey.</param>
		/// <param name="journey">Journey to render journey for.</param>
		/// <param name="stationInfo">Indicates if station info button should be displayed.</param>
        /// <param name="firstJourney"></param>
        public void Initialise(
			JourneyControl.Journey journey, bool outward, bool compareMode, bool adjustable, 
            bool stationInfo, bool printable, bool firstJourney, bool lastJourney, int journeyItineraryIndex,
            int journeyLegCount, ITDJourneyRequest tdjourneyRequest, FindAMode findAMode,
            bool showAccessibleFeatures, bool showAccessibleAssistanceInfo, bool showAccessibleStepFreeInfo)
		{
			// All stored in internal viewstate so that initialise is called only once
			this.journey = journey;
			this.outward = outward;
			this.compareMode = compareMode;
			this.adjustable = adjustable;
			this.stationInfo = stationInfo;
			this.PrinterFriendly = printable;
			this.firstJourney = firstJourney;
			this.lastJourney = lastJourney;
			this.journeyItineraryIndex = journeyItineraryIndex;	
			this.journeyIconCount = journeyLegCount;
            this.tdjourneyRequest = tdjourneyRequest;
            this.findAMode = findAMode;
            this.showAccessibleFeatures = showAccessibleFeatures;
            this.showAccessibleAssistanceInfo = showAccessibleAssistanceInfo;
            this.showAccessibleStepFreeInfo = showAccessibleStepFreeInfo;
		}


        /// <summary>
        /// Method which sets the values needed to add map javascript to the map buttons
        /// </summary>
        /// <param name="addMapJavascript"></param>
        /// <param name="mapId"></param>
        /// <param name="sessionId"></param>
        public void SetMapProperties(bool addMapJavascript, string mapId, string mapJourneyDisplayDetailsDropDownId,
            string scrollToControlId, string sessionId)
        {
            this.addMapJavascript = addMapJavascript;
            this.mapId = mapId;
            this.mapJourneyDisplayDetailsDropDownId = mapJourneyDisplayDetailsDropDownId;
            this.scrollToControlId = scrollToControlId;
            this.sessionId = sessionId;
            this.journeyType = "PT"; // Should always be PT journey being shown

            if (this.journey != null)
            {
                this.journeyRouteNumber = journey.RouteNum;
            }
        }
        
		private PageId belongingPageId = PageId.Empty;
		/// <summary>
		/// Get/Set property - get or sets the page Id. This should be the page Id
		/// of the page that contains this control.
		/// </summary>
		public PageId MyPageId
		{
			get
			{
				return belongingPageId;
			}

			set
			{
				belongingPageId = value;
			}
		}

		#endregion

		#region Method to update the data

		/// <summary>
		/// Checks TDSessionManager to find the data that should be rendered,
		/// sets it as the datasource to the repeater and binds.
		/// </summary>
		private void UpdateData()
		{
			if( journey == null )
			{
				journeySegmentsRepeater.DataSource = new Object[0];
				journeySegmentsRepeater.DataBind();
				return;
			}
			journeySegmentsRepeater.DataSource = journey.JourneyLegs;
			journeySegmentsRepeater.DataBind();
		}

		#endregion

		#region OnLoad / Page Load / OnPreRender Methods
            
       		/// <summary>
		/// Page Load event
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			#region Image translation and updating

			// Node and node connector image urls
			imageStartNodeUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageStartNodeUrl", TDCultureInfo.CurrentUICulture);

			imageEndNodeUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageEndNodeUrl", TDCultureInfo.CurrentUICulture);
    
			imageIntermediateNodeUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageIntermediateNodePartUrl", TDCultureInfo.CurrentUICulture);
            
			imageNodeConnectorUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageNodeConnectorUrl", TDCultureInfo.CurrentUICulture);

			// Transport Mode Image Urls    
			imageCarUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageCarUrl", TDCultureInfo.CurrentUICulture);

			imageAirUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageAirUrl", TDCultureInfo.CurrentUICulture);

			imageBusUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageBusUrl", TDCultureInfo.CurrentUICulture);

			imageCoachUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageCoachUrl", TDCultureInfo.CurrentUICulture);

			imageCycleUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageCycleUrl", TDCultureInfo.CurrentUICulture);

			imageDrtUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageDrtUrl", TDCultureInfo.CurrentUICulture);
            
			imageFerryUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageFerryUrl", TDCultureInfo.CurrentUICulture);
            
			imageMetroUrl =Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageMetroUrl", TDCultureInfo.CurrentUICulture);
            
			imageRailUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageRailUrl", TDCultureInfo.CurrentUICulture);
            
			imageTaxiUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageTaxiUrl", TDCultureInfo.CurrentUICulture);

            imageTelecabineUrl = Global.tdResourceManager.GetString(
                "JourneyDetailsControl.imageTelecabineUrl", TDCultureInfo.CurrentUICulture);

			imageTramUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageTramUrl", TDCultureInfo.CurrentUICulture);
            
			imageUndergroundUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageUndergroundUrl", TDCultureInfo.CurrentUICulture);
            
			imageWalkUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageWalkUrl", TDCultureInfo.CurrentUICulture);

            imageWalkInterchangeUrl = Global.tdResourceManager.GetString(
                "JourneyDetailsControl.imageWalkInterchangeUrl", TDCultureInfo.CurrentUICulture);

			imageRailReplacementBusUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageRailReplacementBusUrl", TDCultureInfo.CurrentUICulture);

            imageTransferUrl = Global.tdResourceManager.GetString(
                "JourneyDetailsControl.imageTransferUrl", TDCultureInfo.CurrentUICulture);

			// Information, Arrive Earlier, Leave Later Image Urls
            
			imageMapUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageMapUKUrl", TDCultureInfo.CurrentUICulture);

            imageArrivalBoardUrl = Global.tdResourceManager.GetString(
                "JourneyDetailsControl.imageArrivalBoardUrl", TDCultureInfo.CurrentUICulture);

            imageDepartureBoardUrl = Global.tdResourceManager.GetString(
                "JourneyDetailsControl.imageDepartureBoardUrl", TDCultureInfo.CurrentUICulture);
            
            imageArriveEarlierUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageArriveEarlierUrl", TDCultureInfo.CurrentUICulture);

			imageLeaveLaterUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageLeaveLaterUrl", TDCultureInfo.CurrentUICulture);

			imageFindTransportFromUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageFindTransportFromUrl", TDCultureInfo.CurrentUICulture);

			imageFindTransportToUrl = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.imageFindTransportToUrl", TDCultureInfo.CurrentUICulture);		
	
			imageFindTransportFromGreyUrl = Global.tdResourceManager.GetString(
				"JourneyExtensionControl.findTransportFromEndButtonGrey.ImageUrl", TDCultureInfo.CurrentUICulture);

			imageFindTransportToGreyUrl = Global.tdResourceManager.GetString(
				"JourneyExtensionControl.findTransportToStartButtonGrey.ImageUrl", TDCultureInfo.CurrentUICulture);	

			imageSpacerUrl = Global.tdResourceManager.GetString(
				"MapZoomControl.spacer.ImageUrl", TDCultureInfo.CurrentUICulture);

            imageWalkitUrl = Global.tdResourceManager.GetString(
                "JourneyDetailsControl.imageWalkitUrl", TDCultureInfo.CurrentUICulture);

			// Alternate text strings

			alternateTextStartNode = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.alternateTextStartNode", TDCultureInfo.CurrentUICulture);

			alternateTextEndNode = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.alternateTextEndNode", TDCultureInfo.CurrentUICulture);
            
			alternateTextIntermediateNode = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.alternateTextIntermediateNode", TDCultureInfo.CurrentUICulture);
            
			alternateTextNodeConnector = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.alternateTextNodeConnector", TDCultureInfo.CurrentUICulture);

			toolTipTextInformationButton = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.InformationButton.ToolTipText", TDCultureInfo.CurrentUICulture);

			textFindTransportToButton = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.FindTransportTo.Text", TDCultureInfo.CurrentUICulture);

			textFindTransportFromButton = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.FindTransportFrom.Text", TDCultureInfo.CurrentUICulture);

			toolTipTextLocationLink = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.LocationLink.ToolTipText", TDCultureInfo.CurrentUICulture);

            toolTipTextLocationAccessibleLink = Global.tdResourceManager.GetString(
                "JourneyDetailsControl.LocationAccessibleLink.ToolTipText", TDCultureInfo.CurrentUICulture);

			toolTipTextDetailsLink = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.ServiceDetailsLink.ToolTipText", TDCultureInfo.CurrentUICulture);

            alternateTextWalkit = Global.tdResourceManager.GetString(
                "JourneyDetailsControl.alternateTextWalkit", TDCultureInfo.CurrentUICulture);

            alternateTextWalkitImageLink = Global.tdResourceManager.GetString(
                "JourneyDetailsControl.alternateTextWalkitImageLink", TDCultureInfo.CurrentUICulture);
   
			// Labels

			leaveText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.Leave", TDCultureInfo.CurrentUICulture);

			arriveText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.Arrive", TDCultureInfo.CurrentUICulture);

			departText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.Depart", TDCultureInfo.CurrentUICulture);

			checkinText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.Checkin", TDCultureInfo.CurrentUICulture);

			exitText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.Exit", TDCultureInfo.CurrentUICulture);

			minsText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.minutesString", TDCultureInfo.CurrentUICulture);

            minutesText = Global.tdResourceManager.GetString(
                "JourneyDetailsTableControl.minutesString.Long", TDCultureInfo.CurrentUICulture);
                
			minText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.minuteString", TDCultureInfo.CurrentUICulture);
            
			maxDurationText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.maxDuration", TDCultureInfo.CurrentUICulture);
                
			typicalDurationText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.typicalDuration", TDCultureInfo.CurrentUICulture);

			startText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.StartText", TDCultureInfo.CurrentUICulture);

			endText = Global.tdResourceManager.GetString(
				"JourneyDetailsControl.EndText", TDCultureInfo.CurrentUICulture);

			secondsText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.secondsString", TDCultureInfo.CurrentUICulture);

			hoursText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.hoursString", TDCultureInfo.CurrentUICulture);

			hourText = Global.tdResourceManager.GetString(
				"JourneyDetailsTableControl.hourString", TDCultureInfo.CurrentUICulture);

            walkitDirectionText = string.Format("{0} {1}",Global.tdResourceManager.GetString(
                "walkitDrirectionLink.Text", TDCultureInfo.CurrentUICulture),
                GetResource("ExternalLinks.OpensNewWindowImage"));

			#endregion			

            // Set CJP user status flag, to allow any specific processing based on this status to happen
            isCJPUser = IsCJPUser();

            
            // Need to call UpdateData to populate the Repeater. This isn't required
            // when inheriting from Web.UI.Page (and not TDPage).
            UpdateData();
            AddEventHandlers();
            AddMapButtonEventHandlers();
		}

        /// <summary>
        /// Page_PreRender event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, System.EventArgs e)
        {
            // UpdateData needs to be called again because a Button Event Handler
            // may have updated the data. (Can't get away with just having UpdateData
            // here (and not in OnLoad) because during OnLoad, the Repeater is empty
            // for some reason (this behaviour does not occur if the page that this
            // control is in is inherited from Web.UI.Page), therefore, the UpdateData
            // is required to populate the Repeater so that the event handlers can
            // be correctly registered.
            UpdateData();
            AddEventHandlers();
            AddMapButtonEventHandlers();
        }

        #endregion

        #region Event support

        /// <summary>
		/// Method to add the event handlers to add dynamically generated buttons
		/// </summary>
		private void AddEventHandlers()
		{
			for (int i=0; i < journeySegmentsRepeater.Items.Count; i++)
			{
                if	((ImageButton)journeySegmentsRepeater.Items[i].FindControl("imageButtonMode") != null)
				{
					// Add button event handler for the Mode button
					((ImageButton)journeySegmentsRepeater.Items[i].FindControl("imageButtonMode")).Click +=
						new ImageClickEventHandler(this.ServiceDetailsButtonClick);
				}

				if	((HyperlinkPostbackControl)journeySegmentsRepeater.Items[i].FindControl("locationInfoLinkControl") != null)
				{
					// Add button event handler for the Information button
					((HyperlinkPostbackControl)journeySegmentsRepeater.Items[i].FindControl("locationInfoLinkControl")).link_Clicked +=
						new System.EventHandler(this.InformationLinkClick);
				}						

				if	((HyperlinkPostbackControl)journeySegmentsRepeater.Items[i].FindControl("alightLocationInfoLinkControl") != null)
				{
					// Add button event handler for the Information button
					((HyperlinkPostbackControl)journeySegmentsRepeater.Items[i].FindControl("alightLocationInfoLinkControl")).link_Clicked +=
						new System.EventHandler(this.InformationLinkClick);
				}						

				if	((HyperlinkPostbackControl)journeySegmentsRepeater.Items[i].FindControl("modeLinkControl") != null)
				{
					// Add button event handler for the Information button
					((HyperlinkPostbackControl)journeySegmentsRepeater.Items[i].FindControl("modeLinkControl")).link_Clicked +=
						new System.EventHandler(this.ServiceDetailsClick);
				}						
			}	

			// footer is not included in the Items collection, so need to find it separately 
			foreach (Control ctrl in journeySegmentsRepeater.Controls)
			{
				if	(ctrl is RepeaterItem)
				{
					if	(((RepeaterItem)ctrl).ItemType == ListItemType.Footer)
					{
						if  (ctrl.FindControl("endLocationInfoLinkControl") != null)
						{
							// Add button event handler for the Information button
							((HyperlinkPostbackControl)ctrl.FindControl("endLocationInfoLinkControl")).link_Clicked +=
								new System.EventHandler(this.InformationLinkEndClick);
						}
						break;
					}
				}
			}
		}

        /// <summary>
        /// Method to add the Map button event handlers to add dynamically generated buttons
        /// </summary>
        private void AddMapButtonEventHandlers()
        {
            for (int i = 0; i < journeySegmentsRepeater.Items.Count; i++)
            {
                if ((HyperlinkPostbackControl)journeySegmentsRepeater.Items[i].FindControl("hyperlinkPostbackControlMapButton") != null)
                {
                    // Add button event handler for the Map link
                    HyperlinkPostbackControl hyperlinkPostbackControl = ((HyperlinkPostbackControl)journeySegmentsRepeater.Items[i].FindControl("hyperlinkPostbackControlMapButton"));

                    if (addMapJavascript)
                    {
                        // The CommandArgument value is populated with the zoom map value
                        // when the repeater items create event is fired. 
                        // Pick it up here and create the javascript function call
                        string mapZoomValue = hyperlinkPostbackControl.CommandArgument;

                        // This is the index the map journey segment drop down list needs to be 
                        // set to when zooming in to the map for a journey segment
                        int journeySegmentIndex = i + 1;

                        // Add the map javascript (allows zooming to segment on map without page postback)
                        hyperlinkPostbackControl.ClientSideJavascript = GetMapButtonClickJavascript(mapZoomValue, journeySegmentIndex);
                    }
                    else
                    {
                        hyperlinkPostbackControl.link_Clicked += new EventHandler(this.MapButtonClick);
                    }
                }

                if ((TDImageButton)journeySegmentsRepeater.Items[i].FindControl("imageMapButton") != null)
                {
                    TDImageButton tdImageButton = ((TDImageButton)journeySegmentsRepeater.Items[i].FindControl("imageMapButton"));

                    // Add button event handler for the Map button
                    if (addMapJavascript)
                    {
                        // The CommandArgument value is populated with the zoom map value
                        // when the repeater items create event is fired. 
                        // Pick it up here and create the javascript function call
                        string mapZoomValue = tdImageButton.CommandArgument;

                        // This is the index the map journey segment drop down list needs to be 
                        // set to when zooming in to the map for a journey segment
                        int journeySegmentIndex = i + 1;
                                                
                        // Add the map javascript (allows zooming to segment on map without page postback)
                        tdImageButton.OnClientClick = GetMapButtonClickJavascript(mapZoomValue, journeySegmentIndex);
                    }
                    else
                    {
                        // Add a normal postback click
                        tdImageButton.Click += new ImageClickEventHandler(this.MapImageButtonClick);
                    }
                }
            }
        }

		/// <summary>
		/// Set up user controls in the repeater
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void JourneySegmentsRepeaterItemCreated(object sender, RepeaterItemEventArgs e)
		{
            JourneyLeg journeyLeg = null;
            PublicJourneyDetail publicJourneyDetail = null;

			switch (e.Item.ItemType)
			{
				case ListItemType.Item :
				case ListItemType.AlternatingItem:
                    
                    if (e.Item.DataItem is JourneyLeg)
                        journeyLeg = (e.Item.DataItem as JourneyLeg);

                    if (e.Item.DataItem is PublicJourneyDetail)
                        publicJourneyDetail = (e.Item.DataItem as PublicJourneyDetail);

                    #region Vehicle features

                    // Set up vehicle features control
					vehicleFeaturesControl = e.Item.FindControl("vehicleFeaturesControl") as VehicleFeaturesControl;
					vehicleFeaturesControl.Features = (journeyLeg.GetVehicleFeatures());

                    #endregion

                    #region Accessible features

                    // Set up accessible features control
                    legAccessibleFeaturesControl = e.Item.FindControl("legAccessibleFeaturesControl") as AccessibleFeaturesControl;
                    locationAccessibleFeaturesControl = e.Item.FindControl("locationAccessibleFeaturesControl") as AccessibleFeaturesControl;

                    SetupLegAccessibleFeatures(publicJourneyDetail, legAccessibleFeaturesControl, locationAccessibleFeaturesControl, false);

                    #endregion

                    #region Vehicle and accessible features panel 

                    Panel pnlFeatureIcons = e.Item.FindControl("pnlFeatureIcons") as Panel;
                    if ((vehicleFeaturesControl.Features != null && vehicleFeaturesControl.Features.Length > 0)
                        || (legAccessibleFeaturesControl.Features != null && legAccessibleFeaturesControl.Features.Count > 0))
                    {
                        pnlFeatureIcons.Visible = true;
                    }
                    else
                        pnlFeatureIcons.Visible = false;

                    #endregion

                    #region Accessible instructions

                    accessibleInstructionControl = (AccessibleInstructionControl)e.Item.FindControl("accessibleInstructionControl");

                    if (accessibleInstructionControl != null)
                    {
                        // Only show accessible information for first rail leg
                        if (journeyLeg.Mode == ModeType.Rail)
                        {
                            accessibleInstructionControl.Initialise(publicJourneyDetail, 
                                showAccessibleAssistanceInfo && showAccessibleRail, 
                                showAccessibleStepFreeInfo && showAccessibleRail);

                            showAccessibleRail = false;
                        }
                        else
                            accessibleInstructionControl.Initialise(publicJourneyDetail, showAccessibleAssistanceInfo, showAccessibleStepFreeInfo);

                        accessibleInstructionControl.PrinterFriendly = this.PrinterFriendly;
                    }
                                        
                    #endregion

                    #region Network Map link

                    networkMapLink = (NetworkMapLinksControl)e.Item.FindControl("networkMapLink");

                    if ((publicJourneyDetail) == null)
					{
						// This could be a road journey if we have a journey extension. Therefore there would be no
						// public journey services information
						if  (networkMapLink != null)
						{
							networkMapLink.Visible = false;
						}
					}
					else
					{
						// Set up network map links control
						if  (networkMapLink != null)
						{
							// Set properties for the control appropriate to the public journey data in the row
                            networkMapLink.JourneyDetail = publicJourneyDetail;
							networkMapLink.PrinterFriendly = this.PrinterFriendly;
						}
						else
						{
							// This could be a road journey if we have a journey extension. Therefore there would be no
							// public journey services information
							networkMapLink.Visible = false;
						}
                    }

                    #endregion

                    #region Leg instructions

                    // Set up leg instructions control
					legInstructionsControl = (LegInstructionsControl)e.Item.FindControl("legInstructionsControl");
					if  (legInstructionsControl != null)
					{
						legInstructionsControl.ShowingInGrid = false;

						RoadJourney roadJourneyItem = (journey as RoadJourney);
						if (roadJourneyItem != null)
						{
							// Set properties for the control appropriate to displaying a Road journey
							legInstructionsControl.RoadJourney = roadJourneyItem;
						}
                        legInstructionsControl.JournyModeType = findAMode;
						legInstructionsControl.JourneyLeg = journeyLeg;
                        legInstructionsControl.UseWalkInterchange = plannerOutputAdapter.WalkInterchangeRequired(journeyLeg, journey, showAccessibleFeatures);
                        legInstructionsControl.DurationMinsText = GetDuration(journeyLeg);
						legInstructionsControl.PrinterFriendly = this.PrinterFriendly;
                    }

                    #endregion

                    #region Map image/button

                    int selectedJourneyLeg = IndexOf(journeyLeg);

                    // Map image/button handling
                    imageMapButton = (TDImageButton)e.Item.FindControl("imageMapButton");
                    imageMapButton.ImageUrl = MapButtonImageUrl;
                    imageMapButton.AlternateText = MapButtonText;
                    imageMapButton.ToolTip = MapButtonAlternateText;
                    imageMapButton.Visible = MapButtonVisible(journeyLeg);

                    hyperlinkPostbackControlMapButton = (HyperlinkPostbackControl)e.Item.FindControl("hyperlinkPostbackControlMapButton");
                    hyperlinkPostbackControlMapButton.Visible = MapButtonVisible(journeyLeg);
                    hyperlinkPostbackControlMapButton.Text = MapButtonText;
                    hyperlinkPostbackControlMapButton.ToolTipText = MapButtonAlternateText;

                    if (addMapJavascript)
                    {
                        // If adding javascript, get the zoom to map values, and attach to the button.
                        // When the click events are added to the repeater items (in Page_PreRender), 
                        // then this value is used to build up the javascript attached to the image/button.
                        if (publicJourneyDetail != null)
                        {
                            JourneyControl.PublicJourney pj = (JourneyControl.PublicJourney)journey;
                            ListItem listItem = mapHelper.GetListItemForDetail(
                                publicJourneyDetail, selectedJourneyLeg, pj, pj.Details.Length, true);

                            imageMapButton.CommandArgument = listItem.Value;
                            hyperlinkPostbackControlMapButton.CommandArgument = listItem.Value;
                        }
                    }
                    else
                    {
                        imageMapButton.CommandArgument = selectedJourneyLeg.ToString();
                        hyperlinkPostbackControlMapButton.CommandArgument = selectedJourneyLeg.ToString();
                    }
                    
                    #endregion

                    #region Departure board

                    hyperlinkDepartureBoard = (HyperLink)e.Item.FindControl("hyperlinkDepartureBoard");
                    hyperlinkDepartureBoard.Text = DepartureBoardButtonText;
                    hyperlinkDepartureBoard.ToolTip = DepartureBoardButtonText;
                    hyperlinkDepartureBoardLink = (HyperLink)e.Item.FindControl("hyperlinkDepartureBoardLink");
                    hyperlinkDepartureBoardLink.Text = DepartureBoardLinkText;
                    hyperlinkDepartureBoardLink.ToolTip = DepartureBoardButtonText;


                    // Determine if we should display the link, if so, set all of its properties
                    hyperlinkDepartureBoard.Visible = DepartureBoardButtonVisible(selectedJourneyLeg);
                    hyperlinkDepartureBoardLink.Visible = hyperlinkDepartureBoard.Visible;
                    if (hyperlinkDepartureBoard.Visible)
                    {
                        if (findAMode != FindAMode.International)
                        {
                            //check is train or airport
                            if (journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.ContainsNaptansForStationType(StationType.Airport)
                                  || journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.ContainsNaptansForStationType(StationType.AirportNoGroup))
                            {
                                try
                                {
                                    hyperlinkDepartureBoard.ImageUrl = GetResource("JourneyDetailsControl.imageDepartureBoardUrl");
                                    // find air hyperlink here
                                    LocationInformationCatalogue refData = (LocationInformationCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationInformation];
                                    hyperlinkDepartureBoard.NavigateUrl = refData.GetLocationInformation(journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.NaPTANs[0].Naptan).DepartureLink.Url;
                                    hyperlinkDepartureBoardLink.NavigateUrl = hyperlinkDepartureBoard.NavigateUrl;
                                }
                                catch
                                {
                                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing airport URL for naptan: " + journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.NaPTANs[0].Naptan);
                                    Logger.Write(oe);
                                    hyperlinkDepartureBoard.Visible = false;
                                    hyperlinkDepartureBoardLink.Visible = false;
                                }
                            }
                            else
                            {

                                IAdditionalData addData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];
                                string crs = addData.LookupCrsForNaptan(journey.JourneyLegs[selectedJourneyLeg].LegStart.Location.NaPTANs[0].Naptan);

                                try
                                {
                                    hyperlinkDepartureBoard.ImageUrl = GetResource("JourneyDetailsControl.imageDepartureBoardUrl");
                                    hyperlinkDepartureBoard.NavigateUrl =
                                    string.Format(Properties.Current["locationinformation.departureboardurl"], crs);
                                    hyperlinkDepartureBoardLink.NavigateUrl = hyperlinkDepartureBoard.NavigateUrl;
                                }
                                catch
                                {
                                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing property in PropertyService : locationinformation.departureboardurl");
                                    Logger.Write(oe);
                                    throw new TDException("missing property in PropertyService : locationinformation.departureboardurl", true, TDExceptionIdentifier.PSMissingProperty);
                                }
                            }
                        }
                        else
                        {
                            hyperlinkDepartureBoard.ImageUrl = GetResource("JourneyDetailsControl.imageDepartureBoardUrl");
                            hyperlinkDepartureBoard.NavigateUrl = (journeyLeg).LegStart.DepartureURL;
                            hyperlinkDepartureBoardLink.NavigateUrl = hyperlinkDepartureBoard.NavigateUrl;
                            
                        }
                        hyperlinkDepartureBoard.Target = "_blank";
                        hyperlinkDepartureBoardLink.Target = "_blank";
                    }

                    #endregion

                    #region Arrival board
                    if (findAMode == FindAMode.International)
                    {
                        hyperlinkArrivalBoard = (HyperLink)e.Item.FindControl("hyperlinkArrivalBoard");
                        hyperlinkArrivalBoard.Text = ArrivalBoardButtonText;
                        hyperlinkArrivalBoard.ToolTip = ArrivalBoardButtonText;
                        hyperlinkArrivalBoardLink = (HyperLink)e.Item.FindControl("hyperlinkArrivalBoardLink");
                        hyperlinkArrivalBoardLink.Text = ArrivalBoardButtonText;
                        hyperlinkArrivalBoardLink.ToolTip = ArrivalBoardButtonText;


                        // Determine if we should display the link, if so, set all of its properties
                        hyperlinkArrivalBoard.Visible = InternationalArrivalBoardVisible(selectedJourneyLeg);
                        hyperlinkArrivalBoardLink.Visible = hyperlinkArrivalBoard.Visible;
                        if (hyperlinkArrivalBoard.Visible)
                        {
                            // Get the arrival URL from previous journey leg
                            JourneyLeg leg = journey.JourneyLegs[selectedJourneyLeg - 1];
                            hyperlinkArrivalBoard.ImageUrl = GetResource("JourneyDetailsControl.imageArrivalBoardUrl");
                            hyperlinkArrivalBoard.NavigateUrl = leg.LegEnd.ArrivalURL;
                            hyperlinkArrivalBoardLink.NavigateUrl = hyperlinkArrivalBoard.NavigateUrl;


                            hyperlinkArrivalBoard.Target = "_blank";
                            hyperlinkArrivalBoardLink.Target = "_blank";
                        }
                    }

                    #endregion

                    #region Start Location Information link

                    locationInfoLinkControl = (HyperlinkPostbackControl)e.Item.FindControl("locationInfoLinkControl");
                    locationLabelControl = (Label)e.Item.FindControl("locationLabelControl");
                    locationInfoLink = e.Item.FindControl("locationInfoLink") as HyperLink;
                    startCarParkLabel = (Label)e.Item.FindControl("startCarParkLabel");
                    startCarParkLabel.Visible = false;
                    
                    locationInfoLinkControl.CommandName = GetStartNaptan(journeyLeg);
                    locationInfoLinkControl.PrinterFriendly = this.PrinterFriendly;
                                      

                    if	(InfoAvailable(journeyLeg))
					{
                                           
                        if (findAMode == FindAMode.International && (journeyLeg).LegStart.Location.Country.CountryCode != "GB")
                        {
                            locationInfoLink.NavigateUrl = (journeyLeg).LegStart.InformationURL;
                            locationInfoLink.Text = string.Format("{0} {1}", GetStartLocation(journeyLeg), GetResource("ExternalLinks.OpensNewWindowImage"));
                            locationInfoLink.Visible = !PrinterFriendly;
                            locationInfoLinkControl.Visible = false;
                            locationLabelControl.Visible = false;
                        }
                        else
                        {
                            locationInfoLinkControl.Text = GetStartLocation(journeyLeg);

                            if (IsStartCarPark)
                            {
                                string carParkRef = GetStartCarParkRef(journeyLeg);
                                locationInfoLinkControl.CommandName = carParkRef;
                                locationInfoLinkControl.CommandArgument = carParkRef;

                                startCarParkLabel.Text = GetResource("CarParkInformationControl.informationNote");

                                // check whether this car park has already appeared in the results.
                                // if this is the first occurence, display the opening times note

                                if (UseNameList)
                                {

                                    if ((!UniqueNameList.Contains(carParkRef)))
                                    {
                                        UniqueNameList.Add(carParkRef);

                                        if (CurrentIsStartCarPark(journeyLeg))
                                        {
                                            startCarParkLabel.Visible = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (CurrentIsStartCarPark(journeyLeg))
                                    {
                                        startCarParkLabel.Visible = true;
                                    }
                                }

                                // IR4248 - remove the Opening times note
                                startCarParkLabel.Visible = false;
                            }

                            locationInfoLinkControl.ToolTipText = !showAccessibleFeatures ?
                                string.Format(CultureInfo.InvariantCulture, toolTipTextLocationLink, locationInfoLinkControl.Text) :
                                string.Format(CultureInfo.InvariantCulture, toolTipTextLocationAccessibleLink, locationInfoLinkControl.Text);

                            locationInfoLinkControl.Visible = true;
                            locationLabelControl.Visible = false;
                        }
					}
					else
					{
						locationLabelControl.Text = GetStartLocation(journeyLeg);
						locationLabelControl.Visible = true;
						locationInfoLinkControl.Visible = false;
					}

					bool interchangeRequired = InterchangeRequired(IndexOf(journeyLeg));

					alightLocationInfoLinkControl = (HyperlinkPostbackControl)e.Item.FindControl("alightLocationInfoLinkControl");
					alightLocationLabelControl = (Label)e.Item.FindControl("alightLocationLabelControl");

					if	(interchangeRequired)
					{
						alightLocationInfoLinkControl.CommandName = GetPreviousEndNaptan(IndexOf(journeyLeg));
						alightLocationInfoLinkControl.PrinterFriendly = this.PrinterFriendly;

						if	(InfoAvailable(journeyLeg))
						{
							alightLocationInfoLinkControl.Text = GetPreviousEndLocation(IndexOf(journeyLeg));
                            alightLocationInfoLinkControl.ToolTipText = !showAccessibleFeatures ?
                                string.Format(CultureInfo.InvariantCulture, toolTipTextLocationLink, alightLocationInfoLinkControl.Text) :
                                string.Format(CultureInfo.InvariantCulture, toolTipTextLocationAccessibleLink, alightLocationInfoLinkControl.Text);

							alightLocationInfoLinkControl.Visible = true;
							alightLocationLabelControl.Visible = false;
						}
						else
						{
							alightLocationLabelControl.Text = GetPreviousEndLocation(IndexOf(journeyLeg));
							alightLocationLabelControl.Visible = true;
							alightLocationInfoLinkControl.Visible = false;
						}
					}
					else
					{
						alightLocationInfoLinkControl.Visible = false;
						alightLocationLabelControl.Visible = false;
                    }

                    #endregion

                    #region Interchange rows

                    interchangeTableRow1 = (TableRow)e.Item.FindControl("interchangeTableRow1");
					interchangeTableRow1.Visible = interchangeRequired;

					interchangeTableRow2 = (TableRow)e.Item.FindControl("interchangeTableRow2");
					interchangeTableRow2.Visible = interchangeRequired;
					
					interchangeTableRow3 = (TableRow)e.Item.FindControl("interchangeTableRow3");
					interchangeTableRow3.Visible = interchangeRequired;
					
					interchangeTableRow4 = (TableRow)e.Item.FindControl("interchangeTableRow4");
					interchangeTableRow4.Visible = interchangeRequired;

					interchangeTableRow5 = (TableRow)e.Item.FindControl("interchangeTableRow5");
					interchangeTableRow5.Visible = interchangeRequired;
					
					interchangeTableRow6 = (TableRow)e.Item.FindControl("interchangeTableRow6");
					interchangeTableRow6.Visible = interchangeRequired;

                    #endregion

                    #region Mode link

                    modeLinkControl = (HyperlinkPostbackControl)e.Item.FindControl("modeLinkControl");

					modeLinkControl.PrinterFriendly = this.PrinterFriendly;

					if	(HasServiceDetails(journeyLeg))
					{
						modeLinkControl.Text = GetMode(journeyLeg, false);
						modeLinkControl.CommandArgument = GetCommandArgument(journeyLeg);
						modeLinkControl.CommandName = GetCommandName(journeyLeg);
						modeLinkControl.ToolTipText = GetModeLinkText(journeyLeg);
						modeLinkControl.Visible = true;
					}
					else
					{
						modeLinkControl.Visible = false;
                    }

                    #endregion

                    #region Walkit link

                    walkitLink = e.Item.FindControl("walkitLink") as WalkitLinkControl;
                    walkitLink.PrinterFriendly = PrinterFriendly;
                    walkitImageLink = e.Item.FindControl("walkitImageLink") as HyperLink;
                    walkitImage = walkitImageLink.FindControl("walkitImage") as System.Web.UI.WebControls.Image;
                    walkitDirectionsLink = e.Item.FindControl("walkitDirectionsLink") as HyperLink;

                    if (((journeyLeg).Mode == ModeType.Walk)
                        && (!plannerOutputAdapter.WalkInterchangeRequired(journeyLeg, journey, showAccessibleFeatures)))
                    {
                        walkitLink.Initialise(journey, journeyLeg, IndexOf(journeyLeg), TDItineraryManager.Current.JourneyRequest, outward);
                        walkitLink.Visible = walkitLink.IsWalkitLinkAvailable;
                    }
                    else
                    {
                        walkitLink.Visible = false;
                    }

                    if (walkitLink.Visible)
                    {
                        walkitLink.Visible = bool.Parse(Properties.Current["WalkitLinkControl.ShowLink"]);
                    }

                    if (walkitLink.Visible)
                    {
                        // Fix for USD UK:6868765 - WalkIT link appears on details compare page
                        if(PageId == PageId.JourneyAdjust || PageId == PageId.CompareAdjustedJourney)
                        {
                            walkitLink.Visible = false;
                        }
                    }

                    if (walkitLink.Visible)
                    {
                        // Fix to not show for accessible journeys where leg is naptan to naptan
                        if (showAccessibleFeatures && (IsFirstLeg(journeyLeg) || (IsLastLeg(journeyLeg)))
                            && ((journeyLeg.LegStart.Location.NaPTANs.Length > 0 && !journeyLeg.LegStart.Location.NaPTANs[0].Naptan.Equals(originNaptanString) && !journeyLeg.LegStart.Location.NaPTANs[0].Naptan.Equals(destinationNaptanString))
                            && (journeyLeg.LegEnd.Location.NaPTANs.Length > 0 && !journeyLeg.LegEnd.Location.NaPTANs[0].Naptan.Equals(originNaptanString) && !journeyLeg.LegEnd.Location.NaPTANs[0].Naptan.Equals(destinationNaptanString))))
                        {
                            walkitLink.Visible = false;
                        }
                    }

                    if (walkitLink.Visible)
                    {
                        // Map image/button handling, hide because showing the walkit link
                        imageMapButton.Visible = false;
                        hyperlinkPostbackControlMapButton.Visible = false;
                        
                        walkitImageLink.Visible = !PrinterFriendly;
                        walkitImageLink.NavigateUrl = walkitLink.WalkitUrl;
                        walkitImageLink.ToolTip = alternateTextWalkitImageLink;

                        walkitImage.ImageUrl = imageWalkitUrl;
                        walkitImage.AlternateText = alternateTextWalkitImageLink;
                        walkitImage.ToolTip = alternateTextWalkitImageLink;
                        
                        walkitDirectionsLink.NavigateUrl = walkitLink.WalkitUrl;
                        walkitDirectionsLink.Text = walkitDirectionText;
                        walkitDirectionsLink.ToolTip = alternateTextWalkit;

                        walkitDirectionsLink.Visible = bool.Parse(Properties.Current["WalkitLinkControl.ShowWalkitDirectionLink"]) && !PrinterFriendly;
                    }
                    else
                    {
                        walkitImageLink.Visible = false;
                        walkitDirectionsLink.Visible = false;
                    }

                    #endregion

                    #region CJP User Info
                    cjpUserInfoWalkLength = (CJPUserInfoControl)e.Item.FindControl("cjpUserInfoWalkLength");
                    cjpUserLocationNaptanInfo = (CJPUserInfoControl)e.Item.FindControl("cjpUserLocationNaptanInfo");
                    cjpUserLocationCoordinateInfo = (CJPUserInfoControl)e.Item.FindControl("cjpUserLocationCoordinateInfo");
                    cjpUserInfoJourneyLegSource = (CJPUserInfoControl)e.Item.FindControl("cjpUserInfoJourneyLegSource");
                    cjpUserInfoDisplayNotes = (CJPUserInfoControl)e.Item.FindControl("cjpUserInfoDisplayNotes");
                    cjpUserInfoLegDebugInfo = (CJPUserInfoControl)e.Item.FindControl("cjpUserInfoLegDebugInfo");

                    CJPUserInfoHelper journeyInfoHelper = new CJPUserInfoHelper(journey, journeyLeg, IndexOf(journeyLeg));
                    cjpUserInfoWalkLength.Initialise(journeyInfoHelper);
                    cjpUserInfoJourneyLegSource.Initialise(journeyInfoHelper);
                    cjpUserInfoDisplayNotes.Initialise(journeyInfoHelper);
                    cjpUserInfoLegDebugInfo.Initialise(journeyInfoHelper);

                    CJPUserInfoHelper locationInfoHelper = new CJPUserInfoHelper((journeyLeg).LegStart.Location);

                    cjpUserLocationNaptanInfo.Initialise(locationInfoHelper);
                    cjpUserLocationCoordinateInfo.Initialise(locationInfoHelper);
                    #endregion

                    break;

                case ListItemType.Footer:

                    #region End Location Information link, and Arrival board

                    endLocationInfoLinkControl = (HyperlinkPostbackControl)e.Item.FindControl("endLocationInfoLinkControl");
                    endLocationLabelControl = (Label)e.Item.FindControl("endLocationLabelControl");
                    endLocationInfoLink = e.Item.FindControl("endLocationInfoLink") as HyperLink;
                    endCarParkLabel = (Label)e.Item.FindControl("endCarParkLabel");
                    endCarParkLabel.Visible = false;

                    hyperlinkArrivalBoard = (HyperLink)e.Item.FindControl("hyperlinkArrivalBoard");
                    hyperlinkArrivalBoard.Text = ArrivalBoardButtonText;
                    hyperlinkArrivalBoard.ToolTip = ArrivalBoardButtonText;
                    hyperlinkArrivalBoard.Visible = false;
                    hyperlinkArrivalBoardLink = (HyperLink)e.Item.FindControl("hyperlinkArrivalBoardLink");
                    hyperlinkArrivalBoardLink.Text = ArrivalBoardLinkText;
                    hyperlinkArrivalBoardLink.ToolTip = ArrivalBoardButtonText;
                    hyperlinkArrivalBoardLink.Visible = false;
                    hyperlinkArrivalBoard.ImageUrl = GetResource("JourneyDetailsControl.imageArrivalBoardUrl");
                    
                    endLocationInfoLinkControl.PrinterFriendly = this.PrinterFriendly;

                    if (InfoAvailableEndLocation())
                    {
                        if (findAMode == FindAMode.International && (journeyLeg).LegEnd.Location.Country.CountryCode != "GB")
                        {
                            endLocationInfoLink.NavigateUrl = (journeyLeg).LegEnd.InformationURL;
                            endLocationInfoLink.Text = string.Format("{0} {1}", FooterEndLocation, GetResource("ExternalLinks.OpensNewWindowImage"));
                            endLocationInfoLink.Visible = !PrinterFriendly;
                            endLocationInfoLinkControl.Visible = false;
                            endLocationLabelControl.Visible = false;
                        }
                        else
                        {
                            endLocationInfoLinkControl.Text = FooterEndLocation;

                            if (IsEndCarPark)
                            {
                                string carParkRef = GetCarParkRef(IndexOf(journeyLeg));
                                endLocationInfoLinkControl.CommandName = carParkRef;
                                endLocationInfoLinkControl.CommandArgument = carParkRef;

                                endCarParkLabel.Text = GetResource("CarParkInformationControl.informationNote");

                                // check whether this car park has already appeared in the results.
                                // if this is the first occurence, display the opening times note
                                if (UseNameList)
                                {
                                    if (!UniqueNameList.Contains(carParkRef))
                                    {
                                        UniqueNameList.Add(carParkRef);
                                        endCarParkLabel.Visible = true;
                                    }
                                }
                                else
                                {
                                    endCarParkLabel.Visible = true;
                                }

                                // IR4248 - remove the Opening times note
                                endCarParkLabel.Visible = false;
                            }


                            endLocationInfoLinkControl.ToolTipText = !showAccessibleFeatures ?
                                string.Format(CultureInfo.InvariantCulture, toolTipTextLocationLink, endLocationInfoLinkControl.Text) :
                                string.Format(CultureInfo.InvariantCulture, toolTipTextLocationAccessibleLink, endLocationInfoLinkControl.Text);

                            endLocationInfoLinkControl.Visible = true;
                            endLocationLabelControl.Visible = false;
                        }

                        // Determine if we should display the link, if so, set all of its properties
                        selectedJourneyLeg = IndexOf(journeyLeg) - 1;
                        if (selectedJourneyLeg >= 0)
                        {
                            hyperlinkArrivalBoard.Visible = ArrivalBoardButtonVisible(selectedJourneyLeg);
                            if (hyperlinkArrivalBoard.Visible)
                            {
                                //check is train or airport
                                if (journey.JourneyLegs[selectedJourneyLeg].LegEnd.Location.ContainsNaptansForStationType(StationType.Airport)
                                      || journey.JourneyLegs[selectedJourneyLeg].LegEnd.Location.ContainsNaptansForStationType(StationType.AirportNoGroup))
                                {
                                    try
                                    {
                                        hyperlinkArrivalBoard.ImageUrl = ArrivalBoardButtonImageUrl;
                                        // find air hyperlink here
                                        LocationInformationCatalogue refData = (LocationInformationCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.LocationInformation];
                                        hyperlinkArrivalBoard.NavigateUrl = refData.GetLocationInformation(journey.JourneyLegs[selectedJourneyLeg].LegEnd.Location.NaPTANs[0].Naptan).ArrivalLink.Url;
                                        hyperlinkArrivalBoardLink.NavigateUrl = hyperlinkArrivalBoard.NavigateUrl;
                                        hyperlinkArrivalBoardLink.Visible = true;

                                    }
                                    catch
                                    {
                                        OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing airport URL for naptan: " + journey.JourneyLegs[selectedJourneyLeg].LegEnd.Location.NaPTANs[0].Naptan);
                                        Logger.Write(oe);
                                        hyperlinkArrivalBoard.Visible = false;
                                        hyperlinkArrivalBoardLink.Visible = false;
                                    }
                                }
                                else
                                {
                                    IAdditionalData addData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];
                                    string crs = addData.LookupCrsForNaptan(journey.JourneyLegs[selectedJourneyLeg].LegEnd.Location.NaPTANs[0].Naptan);

                                    try
                                    {
                                        hyperlinkArrivalBoard.ImageUrl = ArrivalBoardButtonImageUrl;
                                        
                                        hyperlinkArrivalBoard.NavigateUrl =
                                        string.Format(Properties.Current["locationinformation.arrivalboardurl"], crs);
                                        hyperlinkArrivalBoardLink.NavigateUrl = hyperlinkArrivalBoard.NavigateUrl;
                                        hyperlinkArrivalBoardLink.Visible = true;
                                    }
                                    catch
                                    {
                                        OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "missing property in PropertyService : locationinformation.arrivalboardurl");
                                        Logger.Write(oe);
                                        throw new TDException("missing property in PropertyService : locationinformation.arrivalboardurl", true, TDExceptionIdentifier.PSMissingProperty);
                                    }
                                }
                                hyperlinkArrivalBoard.Target = "_blank";
                                hyperlinkArrivalBoardLink.Target = "_blank";
                            }
                        } // end of arrivalboard visibility check

                        

                    }
                    else
                    {
                        endLocationLabelControl.Text = FooterEndLocation;
                        endLocationLabelControl.Visible = true;
                        endLocationInfoLinkControl.Visible = false;
                        hyperlinkArrivalBoard.Visible = false;
                    }

                    #region CJP User Info
                    cjpUserLocationNaptanInfo = (CJPUserInfoControl)e.Item.FindControl("cjpUserLocationNaptanInfo");
                    cjpUserLocationCoordinateInfo = (CJPUserInfoControl)e.Item.FindControl("cjpUserLocationCoordinateInfo");

                    if (journey != null && journey.JourneyLegs.Length > 0)
                    {
                        CJPUserInfoHelper endlocationInfoHelper = new CJPUserInfoHelper(journey.JourneyLegs[journey.JourneyLegs.Length - 1].LegEnd.Location);

                        cjpUserLocationNaptanInfo.Initialise(endlocationInfoHelper);
                        cjpUserLocationCoordinateInfo.Initialise(endlocationInfoHelper);
                    }
                    #endregion

                    #endregion

                    #region Accessible features

                    // Get last leg
                    if (journey != null)
                    {
                        journeyLeg = journey.JourneyLegs[journey.JourneyLegs.Length - 1];

                        if (journeyLeg is PublicJourneyDetail)
                            publicJourneyDetail = (journeyLeg as PublicJourneyDetail);
                    }

                    // Set up accessible features control
                    locationAccessibleFeaturesControl = e.Item.FindControl("locationAccessibleFeaturesControl") as AccessibleFeaturesControl;

                    SetupLegAccessibleFeatures(publicJourneyDetail, null, locationAccessibleFeaturesControl, true);

                    #endregion
                    
                    break;

				
				default :
					break;
			}	

		}
                
        #endregion

        #region Rendering Code

		// ----------------------------------------------------------------

        /// <summary>
        /// Read Only.
        /// Determines if the cjp info summary div should be visible
        /// </summary>
        public bool IsCJPInfoSummaryAvailable
        {
            get
            {
                return CJPUserInfoHelper.IsCJPInformationAvailableForType(CJPInfoType.TrunkExchangePoint)
                    || CJPUserInfoHelper.IsCJPInformationAvailableForType(CJPInfoType.InterchangeTime);
            }
        }

		/// <summary>
		/// Get Property StartText.
		/// Internationalised text for "start"
		/// </summary>
		public string StartText
		{
			get 
			{
				return startText;
			}
		}

		/// <summary>
		/// Internationalised text for "start"
		/// </summary>
		public string StartTextVisible()
		{
			if (this.firstJourney)
				return string.Empty;
			else
				return "visibility: hidden";
		}

		/// <summary>
		/// Get property EndText.
		/// Internationalised text for "end"
		/// </summary>
		public string EndText
		{
			get
			{
				if (this.lastJourney)
					return endText;
				else
					return "&nbsp;";
			}
		}

		/// <summary>
		/// Public journey object to give information about what legs on this journey
		/// </summary>
		public JourneyControl.Journey Journey
		{
			get { return journey; }
		}


		// ----------------------------------------------------------------

		/// <summary>
		/// Returns the url to the start node image if the current item being rendered
		/// is the first item otherwise returns the url to the intermediate node image.
		/// </summary>
		/// <param name="index">Index of current data item being rendered.</param>
		/// <returns>Url to the image.</returns>
		public string GetNodeImage(int index)
		{
			if( index == 0 ) 
			{
				return imageStartNodeUrl;
			} 
			else 
			{
				int imageNumber = journeyIconCount + index;
				return imageIntermediateNodeUrl + imageNumber + ".gif";
			}
		}

		/// <summary>
		/// Returns valign property of the table cell containing the node image.
		/// "top" if Start Location, "middle" if not.
		/// </summary>
		/// <param name="index">Index of current data item being rendered.</param>
		/// <returns>string of valign property</returns>
		public string GetNodeImageVAlign(int index)
		{
			if( index == 0 ) 
			{
				return "top";
			} 
			else 
			{
				return "middle";
			}
		}

		/// <summary>
		/// Returns the alternative text for start node image if the current item
		/// being rendered is the first item otherwise returns the alternative
		/// text for intermediate node.
		/// </summary>
		/// <param name="index">Index of current item being rendered.</param>
		/// <returns>Alternate Text</returns>
		public string GetNodeImageAlternateText(int index)
		{
			if( index == 0) 
			{
				return alternateTextStartNode;
			} 
			else 
			{
				return alternateTextIntermediateNode;
			}
		}

		/// <summary>
		/// Get property - returns the end node image url.
		/// </summary>
		public string EndNodeImage
		{
			get
			{
				return imageEndNodeUrl;
			}
		}

		/// <summary>
		/// Get property EndNodeImageAlternateText.
		/// The alternate text for the end node image.
		/// </summary>
		public string EndNodeImageAlternateText
		{
			get
			{
				return alternateTextEndNode;
			}
		}

		// ----------------------------------------------------------------
                
		/// <summary>
		/// Get property MapButtonImageUrl.
		/// The image url for the map button.
		/// </summary>
		public string MapButtonImageUrl
		{
			get { return imageMapUrl; }
		}

        /// <summary>
        /// Get property ArrivalBoardButtonImageUrl.
        /// The image url for the ArrivalBoard button.
        /// </summary>
        public string ArrivalBoardButtonImageUrl
        {
            get { return imageArrivalBoardUrl; }
        }
        
        /// <summary>
        /// Get property DepartureBoardButtonImageUrl.
        /// The image url for the DepartureBoard button.
        /// </summary>
        public string DepartureBoardButtonImageUrl
        {
            get { return imageDepartureBoardUrl; }
        }
        
        /// <summary>
		/// Read only property, returning true if end location is car park 
		/// </summary>
		public bool IsEndCarPark
		{
			get
			{
				return isEndCarPark;
			}
		}

		/// <summary>
		/// Read only property, returning true if start location is car park 
		/// </summary>
		public bool IsStartCarPark
		{
			get
			{
				return isStartCarPark;
			}
		}

		/// <summary>
		/// Returns whether the leg start station name should be rendered as a hyperlink
		/// </summary>
		public bool InfoAvailable(JourneyLeg journeyLeg)
		{
			// The location name is rendered as a hyperlink if 
			// the page is not printable AND a naptan exists for 
			// the start location of leg being currently rendered.

			TDNaptan[] naptans = journeyLeg.LegStart.Location.NaPTANs;
			
			bool naptanExists = false;

            if (findAMode != FindAMode.International || journeyLeg.LegStart.Location.Country.CountryCode == "GB")
            {
                if (naptans != null && naptans.Length > 0 && naptans[0].Naptan != null && naptans[0].Naptan.Length > 0)
                {
                    if (!naptans[0].Naptan.Equals(originNaptanString) && !naptans[0].Naptan.Equals(destinationNaptanString))
                    {
                        naptanExists = true;
                    }
                }

                if (journeyLeg.LegStart.Location.CarParking != null)
                {
                    isStartCarPark = true;
                }
            }
            else if(findAMode == FindAMode.International)
            {
                return !string.IsNullOrEmpty(journeyLeg.LegStart.InformationURL) && !PrinterFriendly;
            }

			return ( (!PrinterFriendly && naptanExists)
				|| (!PrinterFriendly && isStartCarPark) ); 
		}

		/// <summary>
		/// Returns whether the leg end station name should be rendered as a hyperlink
		/// </summary>
		public bool InfoAvailableEndLocation()
		{
			// Check for null, as this could cause error on the initial databind.
			if	(journey == null)
			{
				return false;
			}

            // The end location name is rendered as a hyperlink if 
			// the page is not printable AND a naptan exists for 
			// the end location of leg being currently rendered.

			// Get the details for the last leg.            
			JourneyLeg journeyLeg = journey.JourneyLegs[journey.JourneyLegs.Length - 1];

            if (findAMode == FindAMode.International)
            {
                return !string.IsNullOrEmpty(journeyLeg.LegEnd.InformationURL) && !PrinterFriendly;
            }

			TDNaptan[] naptans = journeyLeg.LegEnd.Location.NaPTANs;
			
			bool naptanExists = false;
			
			if	(naptans != null && naptans.Length > 0  && naptans[0].Naptan != null && naptans[0].Naptan.Length > 0)
			{
				if	(!naptans[0].Naptan.Equals(originNaptanString) && !naptans[0].Naptan.Equals(destinationNaptanString)) 
				{
					naptanExists = true;
				}
			}

			if( journeyLeg.LegEnd.Location.CarParking != null )
			{
				isEndCarPark = true;
                searchType = journeyLeg.LegEnd.Location.SearchType;
			}
           
			return ( (!PrinterFriendly && naptanExists)
				|| (!PrinterFriendly && isEndCarPark) ); 

		}

		/// <summary>
		/// Checks the journey leg parameter with the initial journey leg, 
		/// returning true if the current journey leg is the start and
		/// the start location is a car park
		/// </summary>
		/// <param name="detail">JourneyLeg</param>
		/// <returns>bool</returns>
		public bool CurrentIsStartCarPark(JourneyLeg detail)
		{
			JourneyLeg journeyLeg = journey.JourneyLegs[0];
			return (detail == journeyLeg) && (detail.LegStart.Location.CarParking != null);
		}

		/// <summary>
		/// Checks the journey leg parameter with the final journey leg, 
		/// returning true if the current journey leg is the end and
		/// the end location is a car park
		/// </summary>
		/// <param name="detail">JourneyLeg</param>
		/// <returns>bool</returns>
		public bool CurrentIsEndCarPark(JourneyLeg detail)
		{
			JourneyLeg journeyLeg = journey.JourneyLegs[journey.JourneyLegs.Length - 1];
			return (detail == journeyLeg) && (detail.LegEnd.Location.CarParking != null);
		}

		/// <summary>
		/// Get property ArriveEarlierButtonImageUrl.
		/// The image url for the Arrive Earlier button.
		/// </summary>
		public string ArriveEarlierButtonImageUrl
		{
			get { return imageArriveEarlierUrl; }
		}

		/// <summary>
		/// Get property LeaveLaterButtonImageUrl.
		/// URL for Leave Later button image
		/// </summary>
		public string LeaveLaterButtonImageUrl
		{
			get { return imageLeaveLaterUrl; }
		}

		/// <summary>
		/// Returns the formatted string representation of the depart time.
		/// A space is returned if the specified leg is either a frequency
		/// leg or a walking leg
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Depart time string or space (&nbsp;)</returns>
		public string GetDepartDateTime(int index)
		{
			JourneyLeg detail = journey.JourneyLegs[index];

			// Return formatted time string.
			PublicJourneyDetail publicJourneyDetailItem = (detail as PublicJourneyDetail);
			if (publicJourneyDetailItem != null)
			{
				if ((detail is PublicJourneyFrequencyDetail) || (detail is PublicJourneyContinuousDetail))
				{
					if (publicJourneyDetailItem.LegStart.DepartureDateTime.Year == 1 || index != 0)
					{
						return "&nbsp;";
					}
					else
					{
                        return FormatDateTime(publicJourneyDetailItem.LegStart.DepartureDateTime);
					}
						
				}
				else if (detail.Mode == ModeType.Air)
				{
                    return FormatDateTime(publicJourneyDetailItem.FlightDepartDateTime);
				}
				else 
				{
                    return FormatDateTime(publicJourneyDetailItem.LegStart.DepartureDateTime);
				}
			}
			else 
			{
                return FormatDateTime(detail.LegStart.DepartureDateTime);
			}
		}

		/// <summary>
		/// Returns the arrival time of the previous leg.
		/// A space is returned if the leg previous to the one specified
		/// is either a frequency leg or a walking leg
		/// </summary>
		/// <param name="index">Index of current leg being rendered.</param>
		/// <returns>Arrival time of previous leg or space (&nbsp;)</returns>
		public string GetPreviousArrivalDateTime(int index, bool interchangeMode)
		{
			//check if first leg is a flight check in leg
			JourneyLeg detail = journey.JourneyLegs[index];
			
			if (index == 0) 
			{
				if ((detail.Mode == ModeType.Air) && ((detail as PublicJourneyDetail) != null) && (((PublicJourneyDetail)detail).CheckInTime != null))
				{
					return FormatDateTime(((PublicJourneyDetail)detail).CheckInTime);
				}
				else
				{
					return String.Empty;
				}
			}
            
            if ((findAMode == FindAMode.International) && ((detail as PublicJourneyDetail) != null) && (((PublicJourneyDetail)detail).CheckInTime != null))
            {
                return FormatDateTime(((PublicJourneyDetail)detail).CheckInTime);
            }
        
			if	(interchangeMode != InterchangeRequired(index))
			{
				return "&nbsp;";
			}

			detail = journey.JourneyLegs[index - 1];
			
			if (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail)
			{
				return "&nbsp;";
			}
            else 
			{
				return FormatDateTime(detail.LegEnd.ArrivalDateTime);
			}
		}
        
		/// <summary>
		/// Returns the instruction text "Arrive", or "Check-in" for Air if the leg previous to the
		/// one specified is neither a frequency leg or walking leg, otherwise
		/// a space is returned.
		/// </summary>
		/// <param name="index">Index of current leg being rendered.</param>
		/// <returns>Instruction text or space (&nbsp;)</returns>
		public string GetPreviousInstruction(int index, bool interchangeMode) 
		{
        
			//if first journey detail is air then a 'Check-in' label should be displayed
			JourneyLeg detail = journey.JourneyLegs[index];
			
			if ((index == 0) && (detail.Mode == ModeType.Air)
				&& ((detail as PublicJourneyDetail) != null) && (((PublicJourneyDetail)detail).CheckInTime != null))
			{
				return checkinText;
			}
			else if ((index == 0) && (detail.Mode != ModeType.Air || ((detail as PublicJourneyDetail) != null) && (((PublicJourneyDetail)detail).CheckInTime == null)))
			{
				return "&nbsp;";
			}
            else if ((findAMode == FindAMode.International) 
                && ((detail as PublicJourneyDetail) != null) && (((PublicJourneyDetail)detail).CheckInTime != null))
            {
                return checkinText;
            }
            else
            {
                if (interchangeMode != InterchangeRequired(index))
                {
                    return "&nbsp;";
                }

                detail = journey.JourneyLegs[index - 1];

                if (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail)
                {
                    return "&nbsp;";
                }
                else
                {
                    return arriveText;
                }
            }
            
		}

		/// <summary>
		/// Returns the instruction text "Leave" for the first leg
		/// (index is zero). Returns the instruction text "Depart" if the specified 
		/// leg is neither a frequency leg or walking leg, otherwise
		/// a space is returned.
		/// </summary>
		/// <param name="index">Index of current leg being rendered.</param>
		/// <returns>Instruction text or space (&nbsp;)</returns>
		public string GetCurrentInstruction(int index) 
		{			

			JourneyLeg detail = journey.JourneyLegs[index];

			if ((index == 0) && (detail.Mode != ModeType.Air)) 
			{
				if (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail)
				{
					if (detail.LegStart.DepartureDateTime.Year == 1)
					{
						return "&nbsp;";
					}
					else
					{
						return leaveText;
					}
				}
				else
				{
					return leaveText;
				}
			}						  
			else if (detail is PublicJourneyFrequencyDetail || detail is PublicJourneyContinuousDetail) 
			{
				return "&nbsp;";
			}
            else if (findAMode == FindAMode.International)
            {
                detail = journey.JourneyLegs[index - 1];

                if ((((PublicJourneyDetail)detail).ExitTime != null))
                {
                    return exitText;
                }
                else
                {
                    return departText;
                }
            }
            else
            {
                return departText;
            }
		}
		
		/// <summary>
		/// Returns the longest instruction text to be used by a dummy cell in the 
		/// repeater table. This is only to ensure correct cell widths.
		/// </summary>
		/// <returns>Instruction text</returns>
		public string GetLongestInstruction() 
		{
			return checkinText;
		}

		/// <summary>
		/// Returns the text for the transport mode of the specified leg.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Mode string formatted.</returns>
		public string GetMode(JourneyLeg journeyLeg, bool lowerCase)
		{
			string resourceManagerKey = 
                (lowerCase ? "TransportModeLowerCase." : "TransportMode.") + journeyLeg.Mode.ToString();

            // Override if need to display the pseudo walk interchange leg
            if (journeyLeg.Mode == ModeType.Walk && plannerOutputAdapter.WalkInterchangeRequired(journeyLeg, journey, showAccessibleFeatures))
            {
                resourceManagerKey =
                (lowerCase ? "TransportModeLowerCase.WalkInterchange" : "TransportMode.WalkInterchange");
            }
            
			return Global.tdResourceManager.GetString(resourceManagerKey, TDCultureInfo.CurrentUICulture);

		}
		
		/// <summary>
		/// Returns the Duration string.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Formatted duration string.</returns>
		public string GetDuration( JourneyLeg detail )
		{
			if (detail is PublicJourneyFrequencyDetail)
			{
				return String.Empty;
			} 
			else 
			{
				// Get the duration of the current leg (rounded to the nearest minute)
				long durationInSeconds = detail.Duration; // in seconds

				// Get the minutes
				double durationInMinutes = durationInSeconds / 60;

				// Check to see if seconds is less than 30 seconds.
				if( durationInSeconds < 31)
				{
					return "< 30 " + secondsText;
				}
				else
				{
					// Round to the nearest minute
					durationInMinutes = Round(durationInMinutes);

					// Calculate the number of hours in the minute
					int hours = (int)durationInMinutes / 60;

					// Get the minutes (afer the hours has been subracted so always < 60)
					int minutes = (int)durationInMinutes % 60;

					// If greater than 1 hour - retrieve "hours", if 1 or less, retrieve "hour"
					string hourString = hours > 1 ?
					hoursText : hourText;

					// If greater than 1 minute - retrive "minutes", if 1 or less, retrieve "minute"
					string minuteString = minutes > 1 ? minsText : minText;
            
					string formattedString = string.Empty;

					if(hours > 0)
						formattedString += hours + " " + hourString + " ";

					formattedString += minutes + " " + minuteString;

					return formattedString;
				}
			}
		}

		/// <summary>
		/// Returns a formatted string containing mode type, service details
		/// (if present) and duration (if leg is not a frequency leg)
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing mode, service and duration details</returns>
		public string FormatModeDetails(JourneyLeg detail, int index) 
		{
			StringBuilder output = new StringBuilder();


            if (detail.Mode != ModeType.Transfer)
            {
                // if service details are available, mode will be rendered 
			    //  as a hyperlink to ServiceDetails page, otherwise it is 
			    //  to be included here as plain text ...

			    if	(!HasServiceDetails(detail))    
			    {
				    output.Append("<span class=\"txtnine\">" + GetMode(detail, false) + "</span>");
			    }

                string services = GetServices(detail);

                if (services.Length > 0)
                {
                    output.Append("<span class=\"txtnineb\"> " + GetServices(detail) + "</span>");
                }

                if (detail is PublicJourneyFrequencyDetail)
                {
                    PublicJourneyFrequencyDetail frequencyDetail = (PublicJourneyFrequencyDetail)detail;

                    output.Append("<br /><span class=\"txtnine\">" + GetMaxJourneyDurationText(detail) + "<br />");

                    // Only append typical duration if its different to max duration
                    if (frequencyDetail.MaxDuration != frequencyDetail.Duration)
                    {
                        output.Append(GetTypicalDurationText(detail));
                    }
                    
                    output.Append("</span");
                }
                else
                {
                    if (plannerOutputAdapter.WalkInterchangeRequired(detail, journey, showAccessibleFeatures))
                    {
                        output.Append("<br /><span class=\"txtnine\">" + GetResource("JourneyDetailsTableControl.WalkInterchangeTo.Allow") + GetDuration(detail) + "</span>");
                    }
                    else
                    {
                        output.Append("<br /><span class=\"txtnine\">" + GetDuration(detail) + "</span>");
                    }
                }
            }
            else
            {
                output.Append("<span class=\"txtnineb\"> " + GetTransferModeText(detail, index) + "</span>");
            }
			
			return output.ToString();
		}

        /// <summary>
        /// Returns formatted string containing transfer mode text 
        /// </summary>
        /// <param name="detail">Current item being rendered</param>
        /// <returns></returns>
        private string GetTransferModeText(JourneyLeg detail, int index)
        {
            string transferModeResourceString = string.Empty;
            string transferModeText = string.Empty;

            if (index == 0)
            {
                transferModeResourceString = GetResource("JourneyDetailControl.TransferModeText.From");
                if (!string.IsNullOrEmpty(transferModeResourceString))
                {
                    transferModeText = string.Format(transferModeResourceString, detail.LegStart.Location.Description);
                }
            }
            else if (index == journey.JourneyLegs.Length - 1)
            {
                transferModeResourceString = GetResource("JourneyDetailControl.TransferModeText.To");
                if (!string.IsNullOrEmpty(transferModeResourceString))
                {
                    transferModeText = string.Format(transferModeResourceString, detail.LegEnd.Location.Description);
                }
            }
            

            return transferModeText;

        }

		/// <summary>
		/// Returns formatted string containing the maximum duration of
		/// a supplied frequency leg, or empty string if supplied leg is
		/// not a frequency leg.
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing the maximum duration or empty string</returns>
		public string GetMaxJourneyDurationText( JourneyLeg detail ) 
		{
			if (detail is PublicJourneyFrequencyDetail) 
			{
				PublicJourneyFrequencyDetail frequencyDetail = (PublicJourneyFrequencyDetail)detail;

				return maxDurationText + ": " + frequencyDetail.MaxDuration +
					(frequencyDetail.MaxDuration > 1 ? minsText : minText);
			} 
			else 
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Returns formatted string containing the typical duration of
		/// a supplied frequency leg, or empty string if supplied leg is
		/// not a frequency leg.
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing the typical duration or empty string</returns>        
		public string GetTypicalDurationText( JourneyLeg detail ) 
		{
			if (detail is PublicJourneyFrequencyDetail) 
			{
				PublicJourneyFrequencyDetail frequencyDetail = (PublicJourneyFrequencyDetail)detail;

				return typicalDurationText + ": " + frequencyDetail.Duration +
					(frequencyDetail.Duration > 1 ? minsText : minText);
			} 
			else 
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// If the supplied leg is not a rail or Car leg and services details are
		/// present, returns a string containing every service number
		/// delimited by commas. Otherwise an empty string is returned. 
		/// </summary>
		/// <param name="detail">Current item being rendered</param>
		/// <returns>Formatted string containing service numbers or empty string</returns>
		public string GetServices(JourneyLeg detail)
		{
			PublicJourneyDetail publicJourneyDetail = (detail as PublicJourneyDetail);
			if (publicJourneyDetail != null
				&& detail.Mode != ModeType.Rail)
			{
				if (publicJourneyDetail.Services != null
					&& publicJourneyDetail.Services.Length > 0)
				{
					StringBuilder serviceDetailsText = new StringBuilder();
					for (int count=0; count < publicJourneyDetail.Services.Length; count++) 
					{
						serviceDetailsText.Append(publicJourneyDetail.Services[count].ServiceNumber);
						if (count < publicJourneyDetail.Services.Length -1)
						{
							serviceDetailsText.Append(",");
						}
					}
					return serviceDetailsText.ToString();      
				}
				else 
				{
					return String.Empty;
				}
			}
			else 
			{
				return String.Empty;
			}
		}

		/// <summary>
		/// Returns the css class name "bgline" if the index value 
		/// supplied is non-zero, otherwise returns an empty string
		/// </summary>
		/// <param name="index">Current item being rendered</param>
		/// <returns>css class name "bgline" or empty string</returns>
		public string GetBackgroundLineClass(int index) 
		{
			if (index == 0) 
			{
				return String.Empty;
			} 
			else 
			{
				return "bgline";
			}
		}

		/// <summary>
		/// Get property.
		/// The end time for the journey.
		/// </summary>
		public string FooterEndDateTime
		{
			get 
			{
				if( journey == null) 
				{
					return "&nbsp;";
				}
				else
				{
					PublicJourneyDetail publicJourneyDetail = (journey.JourneyLegs[journey.JourneyLegs.Length-1] as PublicJourneyDetail);
					if (publicJourneyDetail != null)
					{
						if (publicJourneyDetail.Mode == ModeType.Air) 
						{
                            return FormatDateTime(publicJourneyDetail.FlightArriveDateTime);
						}
						else if (publicJourneyDetail.LegEnd.ArrivalDateTime.Year == 1)
						{
							return "&nbsp;";
						}
						else
						{
                            return FormatDateTime(publicJourneyDetail.LegEnd.ArrivalDateTime);
						}
					}
					else
					{
                        return FormatDateTime(journey.JourneyLegs[journey.JourneyLegs.Length - 1].LegEnd.ArrivalDateTime);
					}
				}
			}
		}

        /// <summary>
        /// Returns the formatted arrive/depart time.
        /// </summary>
        /// <param name="time">Datetime to format.</param>
        /// <returns>Formatted date time</returns>
        public string FormatDateTime(TDDateTime time)
        {
            int requestedDay = 0;

            // getting requested day for journey planner
            if (TDSessionManager.Current.IsFindAMode && TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest != null)
            {
                if ((outward) && (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime != null))
                {
                    requestedDay = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime[0].Day;
                }
                else if (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime != null) 
                {
                    requestedDay = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime[0].Day;
                }
            }

            if ((TDSessionManager.Current.FindAMode == FindAMode.None) && (TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.VisitPlanner))
            {
                if ((TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.ExtendJourney) && (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest != null))
                {
                    if ((outward) && (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime != null))
                    {
                        requestedDay = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.OutwardDateTime[0].Day;
                    }
                    else if (TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime != null) 
                    {
                        requestedDay = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest.ReturnDateTime[0].Day;
                    }   
                }
                else
                {
                    if ((outward) && (TDSessionManager.Current.InputPageState.OriginalOutwardDateTime != null))
                    {
                        requestedDay = TDSessionManager.Current.InputPageState.OriginalOutwardDateTime.Day;
                    }
                    else if (TDSessionManager.Current.InputPageState.OriginalReturnDateTime != null) 
                    {
                        requestedDay = TDSessionManager.Current.InputPageState.OriginalReturnDateTime.Day;
                    }
                }
            }
                       
            if(requestedDay == 0)
            {
                // keeping the requested day same as actual day
                requestedDay = time.Day;
            }

            if (time.Second >= 30)
                time = time.AddMinutes(1);

            // Check to see if the date is different from the request date.
            // For example, if the user has searched for a journey commencing on
            // a Sunday, but the first available train is on a Monday
            int actualDay = time.Day;
            if ((actualDay != requestedDay))
            {
                // Days are different, return the time with the dates appended
                string date = time.ToString("dd/MM");
                return DisplayFormatAdapter.StandardTimeFormat(time) + "<br />(" + date + ")";
            }
            else
            {
                // Dates are the same, simply return the time.
                return DisplayFormatAdapter.StandardTimeFormat(time);
            }
            
        }
        
		/// <summary>
		/// Get property.
		/// Returns the instruction text "exit" for a flight leg, otherwise
		/// a space is returned.
		/// </summary>
		public string FooterExitText
		{
			get 
			{
				if( journey == null) 
				{
					return "&nbsp;";
				}
				else
				{
					PublicJourneyDetail publicJourneyDetail = (journey.JourneyLegs[journey.JourneyLegs.Length-1] as PublicJourneyDetail);
					if ((publicJourneyDetail != null)
						&& (publicJourneyDetail.Mode == ModeType.Air)
						&& (publicJourneyDetail.ExitTime != null) )
					{
						return exitText;
					}
					else
					{
						return "&nbsp;";
					}
				}
			}
		}

		/// <summary>
		/// Get property.
		/// Returns the formatted string representation of the exit time.
		/// A space is returned if the specified leg is not a flight leg
		/// </summary>
		public string FooterExitDateTime
		{
			get 
			{
				if( journey == null) 
				{
					return "&nbsp;";
				}
				else
				{
					PublicJourneyDetail publicJourneyDetail = (journey.JourneyLegs[journey.JourneyLegs.Length-1] as PublicJourneyDetail);
					if ((publicJourneyDetail != null)
						&& (publicJourneyDetail.Mode == ModeType.Air)
						&& (publicJourneyDetail.ExitTime != null) )
					{
						return DisplayFormatAdapter.StandardTimeFormat(publicJourneyDetail.ExitTime);
					}
					else
					{
						return "&nbsp;";
					}
				}
			}
		}

		/// <summary>
		/// Returns the name of the end location of the previous leg
		/// </summary>
		/// <param name="index">Index data item currently being rendered.</param>
		/// <returns>Name of the end location</returns>
		public string GetPreviousEndLocation(int index)
		{
			if	(index == 0) 
			{
				return string.Empty;
			}
			else
			{
				JourneyLeg detail = journey.JourneyLegs[index  - 1];
				return detail.LegEnd.Location.Description;
			}
		}

		/// <summary>
		/// Returns the naptan of the end location of the previous leg
		/// </summary>
		/// <param name="index">Index data item currently being rendered.</param>
		/// <returns>Naptan of the end location</returns>
		public string GetPreviousEndNaptan(int index)
		{
			
			JourneyLeg detail = null;

			if	(index == 0) 
			{
				return string.Empty;
			}
			else
			{
				detail = journey.JourneyLegs[index  - 1];
			}

			TDNaptan[] naptans = detail.LegEnd.Location.NaPTANs;

			if  (naptans != null && naptans.Length > 0  && naptans[0].Naptan != null && naptans[0].Naptan.Length != 0)
			{
				return naptans[0].Naptan;
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Returns the name of the start location
		/// </summary>
		/// <param name="legDetails">Current data item being rendered.</param>
		/// <returns>Name of the start location</returns>
		public string GetStartLocation(JourneyLeg journeyLeg)
		{
			// Return the start location of the given leg
			return journeyLeg.LegStart.Location.Description;
		}

		/// <summary>
		/// Returns the naptan of the journey leg start location
		/// </summary>
		/// <param name="legDetails">Current data item being rendered.</param>
		/// <returns>Naptan of the start location</returns>
		public string GetStartNaptan(JourneyLeg journeyLeg)
		{
			TDNaptan[] naptans = journeyLeg.LegStart.Location.NaPTANs;

			if  (naptans != null && naptans.Length > 0  && naptans[0].Naptan != null && naptans[0].Naptan.Length != 0)
			{
				return naptans[0].Naptan;
			}
			else
			{
				return string.Empty;
			}
		}

        /// <summary>
        /// Returns the naptan of the journey leg end location
        /// </summary>
        /// <param name="legDetails">Current data item being rendered.</param>
        /// <returns>Naptan of the end location</returns>
        public string GetEndNaptan(JourneyLeg journeyLeg)
        {
            TDNaptan[] naptans = journeyLeg.LegEnd.Location.NaPTANs;

            if (naptans != null && naptans.Length > 0 && naptans[0].Naptan != null && naptans[0].Naptan.Length != 0)
            {
                return naptans[0].Naptan;
            }
            else
            {
                return string.Empty;
            }
        }

		/// <summary>
		/// Get property FooterEndLocation. The end location of the journey.
		/// </summary>
		public string FooterEndLocation
		{
			get 
			{
				if( journey == null ) 
				{
					return string.Empty;
				}

                if ((searchType == SearchType.CarPark)
                    && (journey.JourneyLegs[journey.JourneyLegs.Length - 1].LegEnd.Location.CarParking != null))
                {
                    return FindCarParkHelper.GetCarParkName(journey.JourneyLegs[journey.JourneyLegs.Length - 1].LegEnd.Location.CarParking);
                }
                else
                    return journey.JourneyLegs[journey.JourneyLegs.Length - 1].LegEnd.Location.Description;
			}
		}

        /// <summary>
        /// Returns the naptan of the footer end location
        /// </summary>
        /// <param name="legDetails">Current data item being rendered.</param>
        /// <returns>Naptan of the end location</returns>
        public string FooterEndNaptan
        {
            get
            {
                // Check for null, as this could cause error on the initial databind.
                if (journey == null)
                {
                    return string.Empty;
                }
                // Get the details for the last leg.            
                JourneyLeg journeyLeg = journey.JourneyLegs[journey.JourneyLegs.Length - 1];

                TDNaptan[] naptans = journeyLeg.LegEnd.Location.NaPTANs;

                if (naptans != null && naptans.Length > 0 && !string.IsNullOrEmpty(naptans[0].Naptan))
                {
                    if (!naptans[0].Naptan.Equals(originNaptanString) && !naptans[0].Naptan.Equals(destinationNaptanString))
                    {
                        return naptans[0].Naptan;
                    }
                }

                return string.Empty;
            }
        }
        
		/// <summary>
		/// Get property FooterEndInstruction. Internationalised text for "Arrive"
		/// </summary>
		public string FooterEndInstruction
		{
			get
			{
				if( journey == null) 
				{
					return "&nbsp;";
				}
				else
				{
					if (journey.JourneyLegs[journey.JourneyLegs.Length-1].LegEnd.ArrivalDateTime.Year == 1)
					{
						return "&nbsp;";
					}
					else
					{
						return arriveText;
					}
				}
				
			}
		}

		/// <summary>
		/// Returns the command name that should be associated with the map button.
		/// </summary>
		/// <param name="journeyLeg">Current item being rendered.</param>
		/// <returns>Returns the command name.</returns>
		public string GetCommandName(JourneyLeg journeyLeg)
		{
			return IndexOf(journeyLeg).ToString(TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Returns the command argument that should be associated with the mode type.
		/// </summary>
		/// <param name="journeyLeg">Current item being rendered.</param>
		/// <returns>Returns the command argument</returns>
		public string GetCommandArgument(JourneyLeg journeyLeg)
		{
			return journeyLeg.Mode.ToString();
		}

		/// <summary>
		/// Returns the command name that should be associated with the Find Transport To button.
		/// </summary>
		/// <param name="journeyLeg">Current item being rendered.</param>
		/// <returns>Returns the command name.</returns>
		public string GetFindTransportToCommandName(JourneyLeg journeyLeg)
		{
			return IndexOf(journeyLeg).ToString();
		}

		/// <summary>
		/// Returns the command name that should be associated with the arrive earlier button.
		/// </summary>
		/// <param name="journeyLeg">Current item being rendered.</param>
		/// <returns>Returns the command name.</returns>
		public string GetInfoCommandName(JourneyLeg journeyLeg)
		{
			return IndexOf(journeyLeg).ToString(TDCultureInfo.CurrentCulture);
		}
        
		public string GetSpacerImageUrl()
		{
			return imageSpacerUrl;
		}

		/// <summary>
		/// Returns the image url for the transport mode of the specified
		/// leg.
		/// </summary>
		/// <param name="detail">Current item being rendered.</param>
		/// <returns>Image Url</returns>
		public string GetModeImageUrl(JourneyLeg detail)
		{
			switch(detail.Mode)
			{
				case ModeType.Car :
					return imageCarUrl;

				case ModeType.Air :
					return imageAirUrl;

				case ModeType.Bus :
					return imageBusUrl;

				case ModeType.Coach :
					return imageCoachUrl;

				case ModeType.Cycle :
					return imageCycleUrl;

				case ModeType.Drt :
					return imageDrtUrl;

				case ModeType.Ferry :
					return imageFerryUrl;

				case ModeType.Metro :
					return imageMetroUrl;

				case ModeType.Rail :
					return imageRailUrl;

				case ModeType.Taxi :
					return imageTaxiUrl;

                case ModeType.Telecabine:
                    return imageTelecabineUrl;

				case ModeType.Tram :
					return imageTramUrl;

				case ModeType.Underground :
					return imageUndergroundUrl;

				case ModeType.Walk :
                    if (plannerOutputAdapter.WalkInterchangeRequired(detail, journey, showAccessibleFeatures))
                    {
                        return imageWalkInterchangeUrl;
                    }
                    return imageWalkUrl;

				case ModeType.RailReplacementBus :
					return imageRailReplacementBusUrl;

                case ModeType.Transfer:
                    return imageTransferUrl;
			}

			return String.Empty;
		}

		/// <summary>
		/// Gets the alternate text for the transport mode 
		/// image for the given leg
		/// </summary>
		/// <param name="detail">Current leg being rendered.</param>
		/// <returns>Alternate text</returns>
		public string GetModeImageAlternateText(JourneyLeg detail)
		{
			
			// Note: these are currently all set to empty strings in the resource file.
			//		 This is apparently so that the mode is not read out twice by screen readers 
			//		 (once from the alt text, then again from then label underneath).
			
			switch(detail.Mode)
			{
				case ModeType.Air :
					return Global.tdResourceManager.GetString(
						"TransportMode.Air.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Bus :
					return Global.tdResourceManager.GetString(
						"TransportMode.Bus.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Coach :
					return Global.tdResourceManager.GetString(
						"TransportMode.Coach.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Cycle :
					return Global.tdResourceManager.GetString(
						"TransportMode.Cycle.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Drt :
					return Global.tdResourceManager.GetString(
						"TransportMode.Drt.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Ferry :
					return Global.tdResourceManager.GetString(
						"TransportMode.Ferry.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Metro :
					return Global.tdResourceManager.GetString(
						"TransportMode.Metro.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Rail :
					return Global.tdResourceManager.GetString(
						"TransportMode.Rail.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Taxi :
					return Global.tdResourceManager.GetString(
						"TransportMode.Taxi.ImageAlternateText", TDCultureInfo.CurrentUICulture);

                case ModeType.Telecabine:
                    return Global.tdResourceManager.GetString(
                        "TransportMode.Telecabine.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Tram :
					return Global.tdResourceManager.GetString(
						"TransportMode.Tram.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Underground :
					return Global.tdResourceManager.GetString(
						"TransportMode.Underground.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.Walk :
                    if (plannerOutputAdapter.WalkInterchangeRequired(detail, journey, showAccessibleFeatures))
                    {
                        return Global.tdResourceManager.GetString(
                        "TransportMode.WalkInterchange.ImageAlternateText", TDCultureInfo.CurrentUICulture);
                    }
					return Global.tdResourceManager.GetString(
						"TransportMode.Walk.ImageAlternateText", TDCultureInfo.CurrentUICulture);

				case ModeType.RailReplacementBus :
					return Global.tdResourceManager.GetString(
						"TransportMode.RailReplacementBus.ImageAlternateText", TDCultureInfo.CurrentUICulture);

                case ModeType.Transfer:
                    return Global.tdResourceManager.GetString(
                        "TransportMode.Transfer.ImageAlternateText", TDCultureInfo.CurrentUICulture);
			}

			return String.Empty;
		}

		/// <summary>
		/// Returns the car park reference of the end location of the previous leg
		/// </summary>
		/// <param name="index">Index data item currently being rendered.</param>
		/// <returns>Car park reference of the end location</returns>
		public string GetCarParkRef(int index)
		{
			if	(index == 0) 
			{
				return string.Empty;
			}
			else
			{
				JourneyLeg detail = journey.JourneyLegs[index  - 1];
				if(detail.LegEnd.Location.CarParking != null)
				{
					return detail.LegEnd.Location.CarParking.CarParkReference;
				}
				else
				{
					return string.Empty;
				}
			}
		}

		/// <summary>
		/// Returns the car park reference of the start location
		/// </summary>
		/// <param name="journeyLeg">Current data item being rendered.</param>
		/// <returns>Car park reference of the end location</returns>
		public string GetStartCarParkRef(JourneyLeg journeyLeg)
		{
			if(journeyLeg.LegStart.Location.CarParking != null)
				return journeyLeg.LegStart.Location.CarParking.CarParkReference;	
			else
				return string.Empty;
		}
        
		/// <summary>
		/// Gets the alternate text for the transport mode 
		/// image/text where this is a button or link 
		/// to the service details page.
		/// </summary>
		/// <param name="detail">Current leg being rendered.</param>
		/// <returns>Alternate text</returns>
		public string GetModeLinkText(JourneyLeg detail)
		{
			return string.Format(CultureInfo.InvariantCulture, toolTipTextDetailsLink, GetMode(detail, true));
		}
        
		/// <summary>
		/// Returns the index of the given leg details
		/// </summary>
		/// <param name="legDetails">Current data item being rendered.</param>
		/// <returns>Index of the data</returns>
		private int IndexOf(JourneyLeg detail)
		{
			int index=0;

			for(index=0; index < journey.JourneyLegs.Length; index++)
			{
				if  (journey.JourneyLegs[index] == detail)
				{
					break;
				}
			}

			return index;
		}

		/// <summary>
		/// Returns true if the given legDetail is the first leg of the journey, and false if not.
		/// </summary>
		/// <param name="legDetails">Leg data to check.</param>
		/// <returns>True or false.</returns>
		private bool IsFirstLeg(JourneyLeg detail)
		{
			return journey.JourneyLegs[0] == detail;
		}

        /// <summary>
        /// Returns true if the given legDetail is the last leg of the journey, and false if not.
        /// </summary>
        /// <param name="legDetails">Leg data to check.</param>
        /// <returns>True or false.</returns>
        private bool IsLastLeg(JourneyLeg detail)
        {
            return journey.JourneyLegs[journey.JourneyLegs.Length - 1] == detail;
        }

		/// <summary>
		/// Determines whether an interchange "pseudo-leg" needs to be displayed, 
		/// by checking if the start of the current leg matches the end of the previous one 
		/// - if not, a short walk has been removed by the CJP and so we need an interchange.    
		/// </summary>
		/// <param name="detailIndex">Index of item currently being rendered</param>
		/// <returns>True if interchange pseudo-leg is needed.</returns>
		private bool InterchangeRequired(int detailIndex)
		{			
			if (detailIndex == 0) 
			{
				return false;
			}

			// if either location does not have a valid naptan, can only compare descriptions ...

			if	((journey.JourneyLegs[detailIndex].LegStart.Location.NaPTANs == null) 
				|| (journey.JourneyLegs[detailIndex].LegStart.Location.NaPTANs.Length == 0) 
				|| (journey.JourneyLegs[detailIndex - 1].LegEnd.Location.NaPTANs == null) 
				|| (journey.JourneyLegs[detailIndex - 1].LegEnd.Location.NaPTANs.Length == 0))
			{
				if	(journey.JourneyLegs[detailIndex].LegStart.Location.Description.Equals(
					journey.JourneyLegs[detailIndex - 1].LegEnd.Location.Description))
				{
					return false;
				}
				else
				{
					return true;
				}
			}

			// both locations have a valid naptan, so compare those ...

			if	(journey.JourneyLegs[detailIndex].LegStart.Location.NaPTANs[0].CheckEquals(
				journey.JourneyLegs[detailIndex - 1].LegEnd.Location.NaPTANs[0], false))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
        
        /// <summary>
        /// Returns the notes to display for the journey leg
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
		public string GetDisplayNotes(JourneyLeg detail)
		{
			NotesDisplayAdapter notesDisplayAdapter = new NotesDisplayAdapter();

            return notesDisplayAdapter.GetDisplayableNotes(journey, detail); 
		}

        /// <summary>
        /// If user is logged on as a CJP user, and this detail is routing guide compliant, a compliant
        /// string is returned
        /// </summary>
        /// <param name="detail"></param>
        /// <returns></returns>
        public string GetRoutingGuideStatus(JourneyLeg detail)
        {
            if (isCJPUser)
            {
                PublicJourneyDetail journeyDetailItem = (detail as PublicJourneyDetail);
                if (journeyDetailItem != null)
                {
                    return journeyDetailItem.RoutingGuideCompliant ? 
                        "<span class=\"cjperror\">RGCompliant " + (journeyDetailItem.RoutingGuideSectionIndex + 1) + " </span>" 
                        : string.Empty;
                }
            }
            
            return string.Empty;
        }

        #endregion
		
        #region Method to perform the adjustment routine

		/// <summary>
		/// Executes the routine to adjust the journey.
		/// </summary>
		/// <param name="outward">Indicates if the outward or return journey is
		/// being adjusted.</param>
		/// <param name="selectedRouteNode">The selected route node.</param>
		/// <param name="arriveBefore">Indicates the search type, pass True
		/// if "Arrive Earlier" and False if "Leave Later"</param>
		private void PerformAdjustmentRoutine(int selectedRouteNode, bool arriveBefore)
		{
			ITDJourneyRequest originalJourneyRequest = TDSessionManager.Current.JourneyViewState.OriginalJourneyRequest;

			TDCurrentAdjustState tsj = new TDCurrentAdjustState(originalJourneyRequest);
			if( TDSessionManager.Current.CurrentAdjustState != null || 
				TDSessionManager.Current.CurrentAdjustState.JourneyReferenceSequence != 0)
			{
				tsj.JourneyReferenceSequence = TDSessionManager.Current.CurrentAdjustState.JourneyReferenceSequence;
			}
			// Register the original request in TDCurrentAdjustState
			TDSessionManager.Current.CurrentAdjustState = tsj;
                
			// Register selected route node in TDCurrentAdjustState
			tsj.SelectedRouteNode = (uint)selectedRouteNode;

			// Register the mode in TDCurrentAdjustState
			tsj.SelectedRouteNodeSearchType = arriveBefore;
        
			// Get the current journey result from TDSessionManger.
			ITDJourneyResult result = TDSessionManager.Current.JourneyResult;

			// Set the journey in TDCurrentAdjustState
			tsj.AmendedJourney = (journey as JourneyControl.PublicJourney);

			// Outward/return is already set
			ITDAdjustRoute adjustRoute = new AdjustRoute();
            
			tsj.CurrentAmendmentType = outward ? TDAmendmentType.OutwardJourney : TDAmendmentType.ReturnJourney;

			// Call BuildJourneyRequest passing the TDCurrentAdjustState, to
			// get the adjusted journey request.
			ITDJourneyRequest adjustedJourneyRequest = adjustRoute.BuildJourneyRequest(tsj);

			tsj.AmendedJourneyRequest = adjustedJourneyRequest;

			JourneyPlanRunner.JourneyPlanRunner jpr = new JourneyPlanRunner.JourneyPlanRunner( Global.tdResourceManager );

			int referenceNumber = Convert.ToInt32( result.JourneyReferenceNumber, TDCultureInfo.CurrentCulture.NumberFormat );

			AsyncCallState acs = new JourneyPlanState();
			acs.AmbiguityPage = MyPageId;
			acs.DestinationPage = PageId.CompareAdjustedJourney;
			acs.ErrorPage = MyPageId;
			TDSessionManager.Current.AsyncCallState = acs;

			// Always succeeds validation
			jpr.ValidateAndRun( referenceNumber , tsj.JourneyReferenceSequence, adjustedJourneyRequest, TDSessionManager.Current, TDPage.GetChannelLanguage(TDPage.SessionChannelName) );

			// Write to the session the TransitionEvent
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;
		}

        #endregion

		#region Method to determine Map Button Visibility

		public bool MapButtonVisible(JourneyLeg detail)
		{
			return !(compareMode || PrinterFriendly || detail.HasInvalidCoordinates || PageId==PageId.JourneyAdjust || findAMode == FindAMode.International);
		}
        #endregion

        #region Method to determine DepartureBoard Button Visibility


        /// <summary>
        /// Returns true if DepartureBoard button should be displayed, false otherwise
        /// </summary>
        /// <param name="indexRow"></param>
        /// <returns>DepartureBoard button visibility</returns>
        public bool DepartureBoardButtonVisible(int indexRow)
        {
            bool DB_Visible = false;
            DB_Visible = (
                (journey.JourneyLegs[indexRow].LegStart.Location.ContainsNaptansForStationType(StationType.Rail)
                 || journey.JourneyLegs[indexRow].LegStart.Location.ContainsNaptansForStationType(StationType.Airport)
                 || journey.JourneyLegs[indexRow].LegStart.Location.ContainsNaptansForStationType(StationType.RailNoGroup)
                 || journey.JourneyLegs[indexRow].LegStart.Location.ContainsNaptansForStationType(StationType.AirportNoGroup)
                )
                       && !(PrinterFriendly
                            || journey.JourneyLegs[indexRow].HasInvalidCoordinates
                            || PageId == PageId.JourneyAdjust
                            || PageId == PageId.CompareAdjustedJourney
                           )
                       && (journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Air
                            || journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail
                            || journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.RailReplacementBus
                        )
                       );

            if (findAMode == FindAMode.International && !DB_Visible)
            {
                DB_Visible = ((journey.JourneyLegs[indexRow].Mode == ModeType.Air
                                || journey.JourneyLegs[indexRow].Mode == ModeType.Rail
                                || journey.JourneyLegs[indexRow].Mode == ModeType.Coach) 
                                && !string.IsNullOrEmpty(journey.JourneyLegs[indexRow].LegStart.DepartureURL)
                                && !PrinterFriendly);
            }

            return DB_Visible;

        }
        /// <summary>
        /// Returns true if ArrivalBoard button should be displayed, false otherwise
        /// </summary>
        /// <param name="indexRow"></param>
        /// <returns>ArrivalBoard button visibility</returns>
        public bool ArrivalBoardButtonVisible(int indexRow)
        {
            bool DB_Visible = false;
            if (indexRow <0)
            {
               indexRow = journey.JourneyLegs.Length-1; 
            }
            try
            {
               DB_Visible = (
                    (journey.JourneyLegs[indexRow].LegEnd.Location.ContainsNaptansForStationType(StationType.Rail)
                   || journey.JourneyLegs[indexRow].LegEnd.Location.ContainsNaptansForStationType(StationType.Airport)
                   || journey.JourneyLegs[indexRow].LegEnd.Location.ContainsNaptansForStationType(StationType.RailNoGroup)
                   || journey.JourneyLegs[indexRow].LegEnd.Location.ContainsNaptansForStationType(StationType.AirportNoGroup)
                    )
                           && !(PrinterFriendly
                                || journey.JourneyLegs[indexRow].HasInvalidCoordinates
                                || PageId == PageId.JourneyAdjust
                                || PageId == PageId.CompareAdjustedJourney
                               )
                           && (journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Air
                                || journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail
                                || journey.JourneyLegs[indexRow].Mode == TransportDirect.JourneyPlanning.CJPInterface.ModeType.RailReplacementBus
                            )
                           );
           }
           catch
           {
               DB_Visible = false;
           }
            return DB_Visible;

        }

        /// <summary>
        /// Returns true if ArrivalBoard links should be displayed for international journey, false otherwise
        /// </summary>
        /// <param name="indexRow"></param>
        /// <returns>International ArrivalBoard button visibility</returns>
        public bool InternationalArrivalBoardVisible(int indexRow)
        {
            bool DB_Visible = false;
            if (indexRow == 0)
            {
                return DB_Visible;
            }
            
            try
            {
                // Check the previous leg to the leg being checked to see if there is an arrival URL
                JourneyLeg leg = journey.JourneyLegs[indexRow-1];
                DB_Visible = ((leg.Mode == ModeType.Air 
                                || leg.Mode == ModeType.Coach 
                                || leg.Mode == ModeType.Rail)
                                && (findAMode == FindAMode.International)
                                && (!string.IsNullOrEmpty(leg.LegEnd.ArrivalURL))
                                && !PrinterFriendly
                            );
            }
            catch
            {
                DB_Visible = false;
            }
            return DB_Visible;

        }

		#endregion

        #region Method to get Javscript for map button

        /// <summary>
        /// Method to return the javascript to fire on the Map image/button click. This calls
        /// a javascript function to zoom the map to the selected journey leg
        /// </summary>
        /// <returns></returns>
        private string GetMapButtonClickJavascript(string mapZoomValue, int journeySegmentIndex)
        {
            StringBuilder mapJavascript = new StringBuilder();

            mapJavascript.Append("return zoomJourneyDetailMap(");
            mapJavascript.Append("'" + mapId + "'");
            mapJavascript.Append(", '" + sessionId + "'");
            mapJavascript.Append(", '" + journeyRouteNumber + "'");
            mapJavascript.Append(", '" + journeyType + "'");
            mapJavascript.Append(", '" + mapZoomValue + "'");
            mapJavascript.Append(", '" + mapJourneyDisplayDetailsDropDownId + "'");
            mapJavascript.Append(", " + journeySegmentIndex.ToString() + "");
            mapJavascript.Append(", '" + scrollToControlId + "'");
            mapJavascript.Append(");");

            return mapJavascript.ToString();
        }

        #endregion

        #region Button Click Handlers

        /// <summary>
		/// Event Handler for the information hyperlink.
		/// </summary>
		public void InformationLinkClick(object sender, System.EventArgs e)
		{
			// User has clicked the information hyperlink for a particular location.

			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;

			// This is how we 'return'
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( MyPageId );

			if(IsStartCarPark && !link.CommandArgument.Equals(string.Empty))
			{
				string carParkRef =  link.CommandArgument;
				TDSessionManager.Current.InputPageState.CarParkReference = carParkRef;	

				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = 
					TransitionEvent.FindCarParkResultsInfo; 
			}
			else 
			{
				string naptan =  link.CommandName;

				if( naptan != null && naptan.Length > 0 )
				{
					TDSessionManager.Current.InputPageState.AdditionalDataLocation = naptan;
                    SetStopInformation(naptan);
				}

				//assign the text of the location description to session. 
				TDSessionManager.Current.InputPageState.AdditionalDataDescription = link.Text;

				// Show the information page for the selected location.
				// Write the Transition Event
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
					TransitionEvent.StopInformation;
			}
		}

        
		/// <summary>
		/// Event Handler for the information button.
		/// </summary>
		public void InformationLinkEndClick(object sender, System.EventArgs e)
		{
			// This is how we 'return'
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( MyPageId );

			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;

			if(IsEndCarPark && !link.CommandArgument.Equals(string.Empty))
			{
				string carParkRef =  link.CommandArgument;

				TDSessionManager.Current.InputPageState.CarParkReference = carParkRef;	

				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = 
					TransitionEvent.FindCarParkResultsInfo; 
			}
			else 
			{
			
				// Get naptan for the end node
				int selectedRouteNode = journey.JourneyLegs.Length - 1;

				TDNaptan[] naptan = journey.JourneyLegs[selectedRouteNode].LegEnd.Location.NaPTANs;

				if( naptan != null && naptan.Length > 0 )
				{
					// Which naptan?
					TDSessionManager.Current.InputPageState.AdditionalDataLocation = naptan[0].Naptan;
                    SetStopInformation(naptan[0].Naptan);
				}

				//assign the text of the location description to session. 
				TDSessionManager.Current.InputPageState.AdditionalDataDescription
					= journey.JourneyLegs[selectedRouteNode].LegEnd.Location.Description;

				// Show the information page for the selected location.
				// Write the Transition Event
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
					TransitionEvent.StopInformation;
			}
		}


		/// <summary>
		/// Event Handler for the information button.
		/// </summary>
		public void ServiceDetailsClick(object sender, System.EventArgs e)
		{
			HyperlinkPostbackControl link = (HyperlinkPostbackControl)sender;
			DisplayServiceDetails(Int32.Parse(link.CommandName, TDCultureInfo.CurrentCulture.NumberFormat), link.CommandArgument);
		}

		/// <summary>
		/// Event Handler for the mode imagebutton (linking to ServiceDetails page).
		/// </summary>
		public void ServiceDetailsButtonClick(object sender, ImageClickEventArgs e)
		{
			ImageButton button = (ImageButton)sender;
			DisplayServiceDetails(Int32.Parse(button.CommandName, TDCultureInfo.CurrentCulture.NumberFormat), button.CommandArgument);
		}

        /// <summary>
        /// Seting the naptan information for Stop Information page
        /// </summary>
        /// <param name="naptan"></param>
        private void SetStopInformation(string naptan)
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            inputPageState.StopCode = naptan;
            inputPageState.StopCodeType = TDCodeType.NAPTAN;
            inputPageState.ShowStopInformationPlanJourneyControl = false;
        }

		/// <summary>
		/// Perform a transition to the ServiceDetails page for the selected leg.
		/// </summary>
		private void DisplayServiceDetails(int selectedJourneyLeg, string selectedMode)
		{
			// Write the selected journey leg to TDViewState
			TDJourneyViewState journeyViewState = TDItineraryManager.Current.JourneyViewState;
			journeyViewState.SelectedJourneyLeg = selectedJourneyLeg;
			journeyViewState.SelectedIntermediateItinerarySegment = journeyItineraryIndex;
			journeyViewState.CallingPageID = belongingPageId;
	
			TDSessionManager.Current.Session[SessionKey.JourneyDetailsOutward] = outward;
		
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( MyPageId );

			ModeType selectedModeType = ModeType.Rail;

			if (selectedMode != null)
			{
				// get the mode of the select journey
				selectedModeType = (ModeType)Enum.Parse(typeof(ModeType), selectedMode, true);
			}

			if (selectedModeType == ModeType.Car)
			{
				// Show the car details of the selected journey
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.CarDetails;
			}
			else
			{
				// Show the service details of the selected journey
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] =
					TransitionEvent.ServiceDetails;
			}
		}

        public void MapButtonClick(object sender, EventArgs e)
        {
            HyperlinkPostbackControl button = (HyperlinkPostbackControl)sender;
            int selectedJourneyLeg = Convert.ToInt32(button.CommandArgument, TDCultureInfo.CurrentCulture.NumberFormat);
            ProcessMapButtonClicks(selectedJourneyLeg);
        }



        public void MapImageButtonClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            TDImageButton button = (TDImageButton)sender;
            int selectedJourneyLeg = Convert.ToInt32(button.CommandArgument, TDCultureInfo.CurrentCulture.NumberFormat);
            ProcessMapButtonClicks(selectedJourneyLeg);
        }

        public void ProcessMapButtonClicks(int selectedJourneyLeg)
        {
            // Write the selected journey leg to TDViewState
            TDJourneyViewState journeyViewState = TDItineraryManager.Current.JourneyViewState;
            journeyViewState.SelectedJourneyLeg = selectedJourneyLeg;
            journeyViewState.SelectedIntermediateItinerarySegment = journeyItineraryIndex;
            journeyViewState.CallingPageID = belongingPageId;

            TDSessionManager.Current.Session[SessionKey.JourneyMapOutward] = outward;

            // Raise event so the click can be handled in the journey details form and the map displayed
            // as part of that form
            MapButtonClickEventHandler eventHandler = MapButtonClicked;
            if (eventHandler != null)
                eventHandler(this, new MapButtonClickEventArgs(selectedJourneyLeg));
        }

        #endregion

        #region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
        
		///     Required method for Designer support - do not modify
		///     the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.journeySegmentsRepeater.ItemCreated += new RepeaterItemEventHandler(JourneySegmentsRepeaterItemCreated);
		}
        #endregion

        #region Properties: Alternate Text for buttons
        /// <summary>
        /// Get property MapButtonText. Text for the map button.
        /// </summary>
        public string MapButtonText
        {
            get
            {
                return
                    Global.tdResourceManager.GetString(
                    "JourneyDetailsControl.MapButton.Text", TDCultureInfo.CurrentUICulture);
            }
        }

        /// <summary>
        /// Get property MapButtonAlternateText. Alternate text for the map button.
        /// </summary>
        public string MapButtonAlternateText
        {
            get
            {
                return
                    Global.tdResourceManager.GetString(
                    "JourneyDetailsControl.MapButton.AlternateText", TDCultureInfo.CurrentUICulture);
            }
        }
 
        /// <summary>
        /// Get property DepartureBoardButtonAlternateText. Alternate text for the departure button.
        /// </summary>
        public string DepartureBoardButtonText
        {
            get
            {
                return
                    Global.tdResourceManager.GetString(
                    "JourneyDetailsControl.DepartureBoardButton.Text", TDCultureInfo.CurrentUICulture);
            }
        }
        /// <summary>
        /// Get property ArrivalBoardButtonAlternateText. Alternate text for the Arrival button.
        /// </summary>
        public string ArrivalBoardButtonText
        {
            get
            {
                return Global.tdResourceManager.GetString(
                    "JourneyDetailsControl.ArrivalBoardButton.Text", TDCultureInfo.CurrentUICulture); 
            }
        }

        /// <summary>
        /// Get property DepartureBoardLinkText. 
        /// </summary>
        public string DepartureBoardLinkText
        {
            get
            {
                return
                    Global.tdResourceManager.GetString(
                    "JourneyDetailsControl.DepartureBoardLink.Text", TDCultureInfo.CurrentUICulture);
            }
        }
        /// <summary>
        /// Get property ArrivalBoardLinkText.
        /// </summary>
        public string ArrivalBoardLinkText
        {
            get
            {
                return Global.tdResourceManager.GetString(
                    "JourneyDetailsControl.ArrivalBoardLink.Text", TDCultureInfo.CurrentUICulture);
            }
        }

		/// <summary>
		/// Get property InformationButtonAlternateText. Alternate text for the information button.
		/// </summary>
		public string InformationButtonToolTipText
		{
			get
			{
				return toolTipTextInformationButton;
			}
		}

        #endregion

		#region Journey Properties
		
		/// <summary>
		/// Get property ItineraryExists. Returns true if an itinerary has been populated and false if not
		/// </summary>
		private bool ItineraryExists
		{
			get
			{
				return (TDItineraryManager.Current.Length > 0); 

			}
		}

		/// <summary>
		/// Read/write property returning list of unique car park references
		/// </summary>
		public ArrayList UniqueNameList
		{
			get { return uniqueNameList; }
			set { uniqueNameList = value; }
		}

		public bool UseNameList
		{
			get{ return useNameList; }
			set{ useNameList = value; }
		}
		
		/// <summary>
		/// Checks if current journey detail has additional service details to be dispalyed.
		/// Currently based on detail mode only - returning true for rail or rail replacement bus.
		/// </summary>
		public bool HasServiceDetails(JourneyLeg detail)
		{
            bool internationalCoach = (findAMode== FindAMode.International && detail.Mode == ModeType.Coach);
			return (!compareMode && !this.PrinterFriendly && (detail.Mode == ModeType.Rail || detail.Mode == ModeType.RailReplacementBus || detail.Mode == ModeType.Car || internationalCoach));
		}

		#endregion

        #region Private methods

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

        #region Accessible journey methods

        /// <summary>
        /// Sets up the accessible features for the journey leg, only if it contains a PublicJourneyDetail
        /// </summary>
        private void SetupLegAccessibleFeatures(PublicJourneyDetail publicJourneyDetail,
            AccessibleFeaturesControl legAccessibleFeatures, 
            AccessibleFeaturesControl legLocationAccessibleFeatures,
            bool isForEndLocation)
        {
            bool accessibleFeaturesAvailable = false;
            bool accessibleFeaturesLocationAvailable = false;

            // Accessible Features
            if (showAccessibleFeatures)
            {
                if (publicJourneyDetail != null)
                {
                    List<string> suppressForNaPTANsPrefix = new List<string>();

                    string suppressForNaPTANPrefix = Properties.Current["AccessibleOptions.JourneyDetails.AccessibleFeatures.SuppressForNaPTANs"];

                    if (!string.IsNullOrEmpty(suppressForNaPTANPrefix))
                    {
                        suppressForNaPTANsPrefix.AddRange(suppressForNaPTANPrefix.ToUpper().Split(','));
                    }

                    if (!isForEndLocation)
                    {
                        #region Board location

                        if (ShowAccessibleForNaptan(GetStartNaptan(publicJourneyDetail), suppressForNaPTANsPrefix))
                        {
                            // Accessibility features for location (board)
                            if (legLocationAccessibleFeatures != null)
                                legLocationAccessibleFeatures.Initialise(publicJourneyDetail.BoardAccessibility);
                            accessibleFeaturesLocationAvailable = (publicJourneyDetail.BoardAccessibility.Count > 0);
                        }

                        #endregion

                        #region Leg/Service

                        // Accessibility features for leg/service
                        List<JourneyControl.AccessibilityType> accessibilityFeatures = new List<JourneyControl.AccessibilityType>();


                        if (publicJourneyDetail is PublicJourneyInterchangeDetail)
                        {
                            // Interchange detail can contain additional accessibility details
                            PublicJourneyInterchangeDetail pjid = (PublicJourneyInterchangeDetail)publicJourneyDetail;

                            accessibilityFeatures.AddRange(pjid.InterchangeLegAccessibility);
                        }

                        accessibilityFeatures.AddRange(publicJourneyDetail.ServiceAccessibility);

                        // Filter out any accessible icons that shouldn't be shown
                        accessibilityFeatures = FilterAccessibilityTypes(accessibilityFeatures);

                        if (legAccessibleFeatures != null)
                            legAccessibleFeatures.Initialise(accessibilityFeatures);
                        accessibleFeaturesAvailable = (accessibilityFeatures.Count > 0);

                        #endregion
                    }
                    else
                    {
                        #region Alight location

                        if (ShowAccessibleForNaptan(FooterEndNaptan, suppressForNaPTANsPrefix))
                        {
                            // Accessibility features for location (alight)
                            if (legLocationAccessibleFeatures != null)
                                legLocationAccessibleFeatures.Initialise(publicJourneyDetail.AlightAccessibility);
                            accessibleFeaturesLocationAvailable = (publicJourneyDetail.AlightAccessibility.Count > 0);
                        }

                        #endregion
                    }
                }
            }

            // Display if features found
            if (legLocationAccessibleFeatures != null)
                legLocationAccessibleFeatures.Visible = accessibleFeaturesLocationAvailable;
            if (legAccessibleFeatures != null)
                legAccessibleFeatures.Visible = accessibleFeaturesAvailable;
        }

        /// <summary>
        /// Returns if accessible details can be shown for the supplied naptan.
        /// If the naptan is in the suppress naptans list, false is returned
        /// If the naptan begins with the suppress naptans in list, false is returned
        /// </summary>
        /// <param name="naptan"></param>
        /// <param name="suppressNaPTANsList"></param>
        /// <returns></returns>
        private bool ShowAccessibleForNaptan(string naptan, List<string> suppressNaPTANsList)
        {
            bool show = true;

            if (!string.IsNullOrEmpty(naptan))
            {
                foreach (string suppressNaptan in suppressNaPTANsList)
                {
                    if (suppressNaptan.Equals(naptan, StringComparison.CurrentCultureIgnoreCase))
                    {
                        show = false;
                    }
                    else if (naptan.StartsWith(suppressNaptan, StringComparison.CurrentCultureIgnoreCase))
                    {
                        show = false;
                    }
                }
            }

            return show;
        }

        /// <summary>
        /// Removes any AccessibilityType based on accessiblity flags set
        /// </summary>
        /// <returns></returns>
        private List<JourneyControl.AccessibilityType> FilterAccessibilityTypes(List<JourneyControl.AccessibilityType> accessibilityFeatures)
        {
            if (accessibilityFeatures != null)
            {
                // If flag set to not show any features considered as assistance, remove from list
                if (!AccessibleAssistanceFeaturesRequired())
                {
                    accessibilityFeatures.Remove(JourneyControl.AccessibilityType.ServiceAssistanceBoarding);
                }
            }

            return accessibilityFeatures;
        }

        /// <summary>
        /// Determines whether to show accessible assistance feature icons when displaying the
        /// accessible/assistance details
        /// </summary>
        private bool AccessibleAssistanceFeaturesRequired()
        {
            if (tdjourneyRequest != null)
            {
                if (tdjourneyRequest.AccessiblePreferences != null)
                {
                    if (!tdjourneyRequest.AccessiblePreferences.RequireSpecialAssistance)
                    {
                        // Assistance was not required in the accessible journey request, so don't show
                        return false;
                    }
                }
            }

            // Default to true, show
            return true;
        }

        #endregion

        #endregion
    }

	public class MapButtonClickEventArgs : System.EventArgs
	{
		private int legIndex;

		public MapButtonClickEventArgs(int legIndex) : base()
		{
			this.legIndex = legIndex;
		}

		public int LegIndex
		{
			get { return legIndex; }
		}
	}

    public delegate void MapButtonClickEventHandler(object sender, MapButtonClickEventArgs e);
}
