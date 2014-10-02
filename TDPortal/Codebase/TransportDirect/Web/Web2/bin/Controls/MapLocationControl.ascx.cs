// *********************************************** 
// NAME                 : MapLocationControl.ascx.cs 
// AUTHOR               : Atos Origin
// DATE CREATED         : 03/03/2004
// DESCRIPTION			:
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapLocationControl.ascx.cs-arc  $
//
//   Rev 1.4   Sep 14 2009 11:17:06   apatel
//Stop Information related changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.3   Jan 14 2009 11:52:34   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:22:00   mturner
//Drop3 from Dev Factory
// 
//  Rev Devfactory Feb 04 2008 08:39:00 apatel
//  CCN 0427: Changed the layout of the aspx page 
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.0   Nov 08 2007 13:16:26   mturner
//Initial revision.
//
//   Rev 1.42   Apr 27 2007 11:11:42   mmodi
//Added code to handle a null Car park object
//Resolution for 4396: Car Park: Selecting Car park from Map does not show Costs amount in Tickets/Costs
//
//   Rev 1.41   Apr 25 2007 13:24:08   mmodi
//Exposed SetupCarParkPlanning to allow access to user of the control
//Resolution for 4396: Car Park: Selecting Car park from Map does not show Costs amount in Tickets/Costs
//
//   Rev 1.40   Oct 06 2006 15:38:48   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.39.1.10   Oct 03 2006 11:24:18   tmollart
//Added calls to SetupCarParkPlanning for Public and Private Via locations.
//Resolution for 4211: Car Parking: Journey can be planned where From and To are same car park
//
//   Rev 1.39.1.9   Sep 29 2006 12:43:42   mmodi
//Updated SetupCarParkPlanning to set the locality of the car park
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4204: Car Parking: Non-ambiguous location requires two submits when planning journey
//
//   Rev 1.39.1.8   Sep 21 2006 16:57:24   mmodi
//Updated to set car park location to use different entrance and exit coordinates
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4182: Car Parking: Entrance and Exit coordinates not used in journey planning
//
//   Rev 1.39.1.7   Sep 19 2006 17:36:02   tmollart
//Null reference check added.
//Resolution for 4193: Error on Map when click 'i' without selecting new location
//
//   Rev 1.39.1.6   Sep 18 2006 17:23:00   tmollart
//Modified method calls to retrieve a car park from the catalogue.
//Resolution for 4190: Thread Safety Issue on Car Park Catalogue
//
//   Rev 1.39.1.5   Sep 08 2006 14:45:20   esevern
//Removed call to CarParkCatalogue.LoadData. Now only loads data when specific car park selected.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.39.1.4   Sep 01 2006 17:05:10   mmodi
//Amended code to prevent crash when planing a journey with car park - interim fix
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.39.1.3   Aug 21 2006 14:10:06   esevern
//Added check for and processing if car park should be used as planning point for the journey
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.39.1.2   Aug 16 2006 11:40:38   esevern
//Added setting of car parking data to TDLocation if planning a journey
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.39.1.1   Aug 16 2006 11:16:24   esevern
//Added check in OK button handler for selected location being a car park. If so, obtains the reference number for the car park and sets it in InputPageState for later use (on CarParkInformation page etc)
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.39.1.0   Aug 14 2006 10:47:44   esevern
//added FindCarParkInput
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.39   Apr 12 2006 13:13:22   esevern
//Added extra check to the event handler for the 'Plan a journey' button click, (when user elects to use a resolved 'Find on Map' location for journey planning), to prevent a new, empty destination NaPTANs array being created. 
//Resolution for 3872: DN093 Find a Bus: Unable to plan a journey when Find on Map used
//
//   Rev 1.38   Mar 09 2006 15:58:52   RBroddle
//Del 8.1 Code fix exercise - Vantive 4054610 - fix to ensure station info is always displayed.
//
//   Rev 1.37   Feb 23 2006 16:12:46   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.36   Feb 17 2006 11:57:22   halkatib
//Added fixes for IR3573
//
//   Rev 1.35   Feb 10 2006 12:24:46   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.34   Dec 06 2005 14:24:06   pcross
//Added logic to show just the text part of the control. Required for Visit Planner.
//Resolution for 3278: Visit Planner: The maps panel on the journey results page is missing 'select new location' and 'i' buttons
//
//   Rev 1.33.1.0   Dec 22 2005 11:15:52   tmollart
//Removed calls to now redudant SaveCurrentFindaMode.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.33   Nov 14 2005 20:36:46   RHopkins
//Changed clientside JavaScript that handles visibility of map manipulation controls.
//Resolution for 3017: UEE: Location Maps - "Select new location" JavaScript error
//
//   Rev 1.32   Nov 11 2005 13:48:54   rgreenwood
//IR2986 fixed information button tool tip
//Resolution for 2986: UEE: Location Maps - "Select new location" button shown on new line
//
//   Rev 1.31   Nov 10 2005 17:48:48   asinclair
//Merge for 2638
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.30   Nov 04 2005 11:45:22   ralonso
//Manual merge from stream2816
//
//   Rev 1.29   Nov 01 2005 15:11:34   build
//Automatically merged from branch for stream2638
//
//   Rev 1.28.2.0   Oct 29 2005 11:37:30   asinclair
//Added methods to support Map button click from the three VisitPlannerLocation controls
//Resolution for 2638: DEL 8 Stream: Visit Planner

//   Rev 1.28.1.1   Nov 01 2005 14:20:58   rgreenwood
//TD089 ES020 Removed HTML tablecell references and replaced with button reference
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.28.1.0   Oct 19 2005 14:57:48   rhopkins
//TD089 ES020 Image Button Replacement - Replace ScriptableImageButtons and ordinary ImageButtons
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.28   May 09 2005 13:29:38   jbroome
//Added call to SaveCurrentFindAMode() before clearing out journey parameters on click events of Travel To This Location and Travel From This Location.
//Also ensure session parameters and info is cleared out when viewing full itinerary view.
//Resolution for 2433: PT - Screenflow problem: Different consecutive journey planning breaks Plan journey from this location in Map page
//
//   Rev 1.27   Apr 28 2005 20:21:30   pcross
//IR2368. When using Maps screen to plan journey on startup, ensure journey params object is initialised.
//Resolution for 2368: Del 7 - Error when planing a journey from Location Maps
//
//   Rev 1.26   Mar 24 2005 14:48:26   RAlavi
//Added code to display the appropriate map for the public transport
//
//   Rev 1.25   Sep 22 2004 11:53:06   jbroome
//IR 1597 - Clear out any itinerary results when planning journey from map.
//
//   Rev 1.24   Sep 20 2004 15:24:36   passuied
//minor error. forgot to use FromFindAInput in conjonction with FromJourneyInput during recent changes. 
//Resolution for 1605: Location maps do not populate quick planner correctly
//
//   Rev 1.23   Sep 17 2004 15:14:00   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.22   Sep 15 2004 09:24:14   jbroome
//Complete re-design of Map tools navigation. Removal of unnecessary stages in screen flow.
//
//   Rev 1.21   Jun 09 2004 16:31:00   passuied
//changes to display correct instructions in FindStationMap
//
//   Rev 1.20   Jun 07 2004 15:25:30   jbroome
//ExtendJourney - added support for TDItineraryManager.
//
//   Rev 1.19   May 26 2004 11:28:20   jbroome
//Fix for IR914 / IR826. Map Control, Find Information flow.
//
//   Rev 1.18   Apr 30 2004 13:27:14   jbroome
//DEL 5.4 Merge
//JavaScript Map Control
//
//   Rev 1.17   Apr 06 2004 12:54:24   CHosegood
//Del 5.2 Map QA changes.
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.16   Apr 05 2004 18:01:52   CHosegood
//Del 5.2 map QA fixes
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.15   Apr 05 2004 17:13:14   CHosegood
//Del 5.2 map QA fixes
//
//   Rev 1.14   Apr 01 2004 18:08:46   CHosegood
//Now goes back to the ambiguity page/input page as appropriate
//Resolution for 687: Current location button on map page does not return to ambiguity page
//
//   Rev 1.13   Mar 23 2004 11:53:54   CHosegood
//Del 5.2 BBC QA changes
//Resolution for 665: Del 5.2 BBC QA changes
//
//   Rev 1.12   Mar 19 2004 16:01:14   COwczarek
//Set the map control's associated location object to that in the page state and not from journey parameters
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.11   Mar 16 2004 16:38:38   CHosegood
//Del 5.2 map changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.10   Mar 15 2004 18:18:00   CHosegood
//Del 5.2 Map Changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.9   Mar 14 2004 19:12:24   CHosegood
//DEL 5.2 Changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.8   Mar 12 2004 16:25:54   COwczarek
//Initialse map location search object in page state correctly when ok button clicked
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.7   Mar 12 2004 15:13:54   CHosegood
//Added labelOptions2
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.6   Mar 11 2004 18:14:50   PNorell
//Changed the support for using as much as possible in the MapState rather than InputPageState.
//
//   Rev 1.5   Mar 10 2004 19:02:10   PNorell
//Updated for Map state.
//
//   Rev 1.4   Mar 10 2004 18:31:24   PNorell
//Updated stub to actually work.
//
//   Rev 1.3   Mar 10 2004 18:23:10   PNorell
//Added stub method for if the journey is return journey or not.
//
//   Rev 1.2   Mar 10 2004 17:25:12   CHosegood
//Added ModeChangedEventHandler, ModeChangedEventArgs, LocationSelectedEventHandler, LocationSelectedEventArgs from MapSelectionControl and output attribute
//
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.1   Mar 08 2004 19:55:14   CHosegood
//Fixed HTML formatting, added PVCS header and now using InputPageState.MapLocation instead of JourneyViewState.Location
//Resolution for 633: Del 5.2 Map Changes

using System;
using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.Presentation.InteractiveMapping;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Web.Support;
using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Summary description for MapLocationControl.
	/// </summary>
	public partial  class MapLocationControl : TDUserControl
	{
		#region protected page attributes




		protected MapFindInformationLocationControl mapFindInformationLocationControl;
		protected MapPlanJourneyLocationControl mapPlanJourneyLocationControl;
		protected MapSelectLocationControl mapSelectLocationControl;

		protected ICarParkCatalogue carParkCatalogue;
		public const string PARKING_EXIT = "Exit";
		public const string PARKING_ENTRANCE = "Entrance";
		public const string PARKING_MAP = "Map";

		#endregion

		#region private page attributes

		private TDMap map;

		/// <summary>
		/// Keeps track of the page the control is used on
		/// </summary>
		private PageId pageId = PageId.Empty;

		private bool outputMap = false;
		private bool stationMap = false;
		private bool usesReturnJourney = false;
		private bool showTextOnly = false;
		private bool carParkSelected = false;

		#endregion

		#region Public Events
		/// <summary>
		/// 
		/// </summary>
		public event LocationSelectedEventHandler LocationSelectedEvent;

		/// <summary>
		/// Event for mode-changed 
		/// </summary>
		public event ModeChangedEventHandler ModeChangeEvent;

		/// <summary>
		/// Event for information requested (Is this needed?)
		/// </summary>
		public event EventHandler InformationRequestedEvent;
		#endregion

		#region Public Properties
		/// <summary>
		/// 
		/// </summary>
		public TDMap Map 
		{
			get 
			{
				return map;
			}
			set 
			{
				map = value; 
			}
		}


		/// <summary>
		/// Get/Set
		/// True if this MapLocationControl is for an output map otherwise
		/// it is false (i.e. input map)
		/// </summary>
		public bool OutputMap 
		{
			get  { return outputMap; }
			set 
			{
				outputMap = value;
				this.mapFindInformationLocationControl.OutputMap = value;
				//this.mapPlanJourneyLocationControl.OutputMap = value;
				this.mapSelectLocationControl.OutputMap = value;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		public bool StationMap
		{
			get
			{
				return stationMap;
			}

			set
			{
				stationMap = value;
				OutputMap = value;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		public MapSelectLocationControl SelectLocationControl 
		{
			get { return this.mapSelectLocationControl; }
		}


		/// <summary>
		/// Get/Set property to set the page id that this control belongs to.
		/// </summary>
		public PageId CurrentPageId
		{
			get { return pageId; }
			set { pageId = value; }
		}


		/// <summary>
		/// Sets/Gets if the control should be handling outward or inward journey.
		/// </summary>
		public bool UsesReturnJourney
		{
			get  { return usesReturnJourney; }
			set 
			{
				usesReturnJourney = value;
				mapFindInformationLocationControl.UsesReturnJourney = value;
				mapPlanJourneyLocationControl.UsesReturnJourney = value;
				mapSelectLocationControl.UsesReturnJourney = value;
			}
		}

		/// <summary>
		/// Sets/Gets if the control should show just the label text (no interaction to re-plan map location)
		/// </summary>
		public bool ShowTextOnly
		{
			get  { return showTextOnly; }
			set 
			{
				showTextOnly = value;
				
				if (showTextOnly)
				{
					buttonPlanAJourney.Visible = false;
					buttonFindInformation.Visible = false;
					buttonSelectLocation.Visible = false;
					labelPlanAJourney.Visible = false;
					labelFindInformation.Visible = false;
					labelSelectLocation.Visible = false;
					labelOptions.Visible = true;
					labelOptions2.Visible = true;
					mapPlanJourneyLocationControl.Visible = false;
					mapFindInformationLocationControl.Visible = false;
					mapSelectLocationControl.Visible = false;
				}
				else
				{
					buttonPlanAJourney.Visible = true;
					buttonFindInformation.Visible = true;
					buttonSelectLocation.Visible = true;
					labelPlanAJourney.Visible = true;
					labelFindInformation.Visible = true;
					labelSelectLocation.Visible = true;
					labelOptions.Visible = true;
					labelOptions2.Visible = true;
					mapPlanJourneyLocationControl.Visible = true;
					mapFindInformationLocationControl.Visible = true;
					mapSelectLocationControl.Visible = true;
				}
			}
		}

		/// <summary>
		/// Gets the mapstate to be used by the control
		/// </summary>
		public JourneyMapState MapState
		{
			get 
			{ 
				if( UsesReturnJourney )
				{
					return TDSessionManager.Current.ReturnJourneyMapState;
				}
				return TDSessionManager.Current.JourneyMapState;
			}
		}

		/// <summary>
		/// Gets the string id for the hidden field, used to 
		/// store the JourneyMapState client-side.
		/// </summary>
		public string GetHiddenInputId
		{
			get
			{
				return this.ClientID + "_hdnJourneyMapState";
			}
		}

		/// <summary>
		/// String used populate hdnJourneyMapState hidden control
		/// Used in synchronising client and server states due to use of JavaScript
		/// </summary>
		public string GetJourneyMapState
		{
			get 
			{
				return TDSessionManager.Current.JourneyMapState.State.ToString(); 
			}
		}
		#endregion

		#region Button event handler

		/// <summary>
		/// Event handler for click event of the Plan Journey button when coming 
		/// from Travel To.
		/// (or Travel To button of the child MapPlanJourneyLocationControl)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PlanJourneyTravelTo_Click(object sender, EventArgs e)
		{        

			// If there is a valid results set, reset the parameters and pagestate, and 
			// and invalidate the results.
			if ( ((TDItineraryManager.Current.JourneyResult != null) && (TDItineraryManager.Current.JourneyResult.IsValid))
				|| (TDItineraryManager.Current.FullItinerarySelected))
			{
				TDItineraryManager.Current.ResetItinerary();
				TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();
				TDSessionManager.Current.InputPageState.Initialise();
				TDSessionManager.Current.JourneyResult.IsValid = false;
			} 

			// If there have been no journey planner parameters set up then do so now
			if (TDSessionManager.Current.JourneyParameters == null)
			{
				TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();				
			}

			// If its a car park selected, obtain the car park data and add to location
			if(TDSessionManager.Current.InputPageState.CarParkReference != string.Empty)
			{
				SetupCarParkPlanning(false);
			}

			TDSessionManager.Current.JourneyParameters.DestinationLocation 
				= TDSessionManager.Current.InputPageState.MapLocation;

			TDSessionManager.Current.JourneyParameters.Destination 
				= TDSessionManager.Current.InputPageState.MapLocationSearch;

			if ( TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromJourneyInput
				&& TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromFindAInput) 
			{
				// Check to see if Destination is valid
				if(TDSessionManager.Current.JourneyParameters.OriginLocation.Status == TDLocationStatus.Valid)
				{
					TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.FindJourneys;
				}
				else
				{
					TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.TravelTo;
					TDSessionManager.Current.InputPageState.MapLocationSearch = new LocationSearch();
					TDSessionManager.Current.InputPageState.MapLocation = new TDLocation();
					TDSessionManager.Current.InputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.Default;
				}
			}

			if (TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.FromFindAInput 
				&& (TDSessionManager.Current.FindAMode != FindAMode.Bus)) 
			{
				TDSessionManager.Current.JourneyParameters.DestinationLocation.NaPTANs = new TDNaptan[0];
			}
			//Reset the Journey Map State
			MapState.Initialise();

			if ( TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count == 0 )
				TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId.JourneyPlannerInput );

			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.LocationMapBack;
		}

		/// <summary>
		/// Event handler for click event of the Plan Journey button 
		/// when coming from Via Location.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PlanJourneyTravelVia_Click(object sender, EventArgs e)
		{        
			TDJourneyParametersMulti tdJourneyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;


			// If its a car park selected, obtain the car park data and add to location
			if(TDSessionManager.Current.InputPageState.CarParkReference != string.Empty)
			{
				SetupCarParkPlanning(true);
			}

			if (tdJourneyParameters == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("MapPlanJourneyLocationControl.TravelVia_Click requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}

			tdJourneyParameters.PrivateViaLocation = TDSessionManager.Current.InputPageState.MapLocation;

			tdJourneyParameters.PrivateVia = TDSessionManager.Current.InputPageState.MapLocationSearch;

			if ( TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromJourneyInput
				&& TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromFindAInput) 
			{
				// Check to see if Destination is valid
				if(tdJourneyParameters.PrivateViaLocation.Status == TDLocationStatus.Valid)
				{
					TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.FindJourneys;
				}
				else
				{
					TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.TravelTo;
					TDSessionManager.Current.InputPageState.MapLocationSearch = new LocationSearch();
					TDSessionManager.Current.InputPageState.MapLocation = new TDLocation();
					TDSessionManager.Current.InputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.Default;
				}
			}

			//Reset the Journey Map State
			MapState.Initialise();

			if ( TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count == 0 )
				TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId.JourneyPlannerInput );

			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.LocationMapBack;
		}
		
		
		/// <summary>
		/// Event handler for click event of the Plan Journey button 
		/// when coming from Via Location.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PlanJourneyTravelViaPt_Click(object sender, EventArgs e)
		{        
			TDJourneyParametersMulti tdJourneyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;

			if (tdJourneyParameters == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("MapPlanJourneyLocationControl.TravelVia_Click requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}

			// If its a car park selected, obtain the car park data and add to location
			if(TDSessionManager.Current.InputPageState.CarParkReference != string.Empty)
			{
				SetupCarParkPlanning(true);
			}

			tdJourneyParameters.PublicViaLocation = TDSessionManager.Current.InputPageState.MapLocation;

			tdJourneyParameters.PublicVia = TDSessionManager.Current.InputPageState.MapLocationSearch;

			if ( TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromJourneyInput
				&& TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromFindAInput) 
			{
				// Check to see if Destination is valid
				if(tdJourneyParameters.PublicViaLocation.Status == TDLocationStatus.Valid)
				{
					TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.FindJourneys;
				}
				else
				{
					TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.TravelTo;
					TDSessionManager.Current.InputPageState.MapLocationSearch = new LocationSearch();
					TDSessionManager.Current.InputPageState.MapLocation = new TDLocation();
					TDSessionManager.Current.InputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.Default;
				}
			}

			//Reset the Journey Map State
			MapState.Initialise();

			if ( TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count == 0 )
				TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId.JourneyPlannerInput );

			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.LocationMapBack;
		}

		/// <summary>
		/// Event handler for click event of the Plan Journey button when coming 
		/// from Travel From.
		/// (or Travel From button of the child MapPlanJourneyLocationControl)
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PlanJourneyTravelFrom_Click(object sender, EventArgs e)
		{

			// If there is a valid results set, reset the parameters and pagestate, and 
			// and invalidate the results.
			if ( ((TDItineraryManager.Current.JourneyResult != null) && (TDItineraryManager.Current.JourneyResult.IsValid))
				|| (TDItineraryManager.Current.FullItinerarySelected))
			{
				TDItineraryManager.Current.ResetItinerary();
				TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();				
				TDSessionManager.Current.InputPageState.Initialise();
				TDSessionManager.Current.JourneyResult.IsValid = false;
			} 

			// If there have been no journey planner parameters set up then do so now
			if (TDSessionManager.Current.JourneyParameters == null)
			{
				TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();				
			}

			// If its a car park selected, obtain the car park data and add to location
			if(TDSessionManager.Current.InputPageState.CarParkReference != string.Empty)
			{
				SetupCarParkPlanning(true);
			}
			
			TDSessionManager.Current.JourneyParameters.OriginLocation
				= TDSessionManager.Current.InputPageState.MapLocation;

			TDSessionManager.Current.JourneyParameters.Origin
				= TDSessionManager.Current.InputPageState.MapLocationSearch;

			if ( TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromJourneyInput
				&& TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromFindAInput) 
			{
				// Check to see if Destination is valid
				if(TDSessionManager.Current.JourneyParameters.DestinationLocation.Status == TDLocationStatus.Valid)
				{
					TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.FindJourneys;
				}
				else
				{
					TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.TravelTo;
					TDSessionManager.Current.InputPageState.MapLocationSearch = new LocationSearch();
					TDSessionManager.Current.InputPageState.MapLocation = new TDLocation();
					TDSessionManager.Current.InputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.Default;
				}
			}

			if (TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.FromFindAInput && 
				(TDSessionManager.Current.FindAMode != FindAMode.Bus)) 
			{
				TDSessionManager.Current.JourneyParameters.DestinationLocation.NaPTANs = new TDNaptan[0];
			}
			//Reset the Journey Map State
			MapState.Initialise();

			if ( TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count == 0 )
				TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId.JourneyPlannerInput );

			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.LocationMapBack;

		}
		/// <summary>
		/// Event handler for click event of the Plan Journey button when coming 
		/// from VisitPlannerOriginLocation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PlanJourneyVisitOrigin_Click(object sender, EventArgs e)
		{

			TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

			// If there have been no journey planner parameters set up then do so now
			if (parameters == null)
			{
				parameters = new TDJourneyParametersVisitPlan();				
			}

			parameters.SetLocation(0,TDSessionManager.Current.InputPageState.MapLocation);
			
			parameters.SetLocationSearch(0,TDSessionManager.Current.InputPageState.MapLocationSearch);



			//Reset the Journey Map State
			MapState.Initialise();

			if ( TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count == 0 )
				TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId.JourneyPlannerInput );

			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.LocationMapBack;

		}

		/// <summary>
		/// Event handler for click event of the Plan Journey button when coming 
		/// from VisitPlannerLocation1
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PlanJourneyVisitLocation1_Click(object sender, EventArgs e)
		{

			TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

			// If there have been no journey planner parameters set up then do so now
			if (parameters == null)
			{
				parameters = new TDJourneyParametersVisitPlan();				
			}
					
			parameters.SetLocation(1,TDSessionManager.Current.InputPageState.MapLocation);
			
			parameters.SetLocationSearch(1,TDSessionManager.Current.InputPageState.MapLocationSearch);



			//Reset the Journey Map State
			MapState.Initialise();

			if ( TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count == 0 )
				TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId.JourneyPlannerInput );

			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.LocationMapBack;

		}

		/// <summary>
		/// Event handler for click event of the Plan Journey button when coming 
		/// from VisitPlannerLocation2.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PlanJourneyVisitLocation2_Click(object sender, EventArgs e)
		{

			TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

			// If there have been no journey planner parameters set up then do so now
			if (parameters == null)
			{
				parameters = new TDJourneyParametersVisitPlan();				
			}
					
			parameters.SetLocation(2,TDSessionManager.Current.InputPageState.MapLocation);
			
			parameters.SetLocationSearch(2,TDSessionManager.Current.InputPageState.MapLocationSearch);



			//Reset the Journey Map State
			MapState.Initialise();

			if ( TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count == 0 )
				TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( PageId.JourneyPlannerInput );

			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			sessionManager.FormShift[SessionKey.TransitionEvent] =
				TransitionEvent.LocationMapBack;

		}
		#endregion

		
		/// <summary>
		/// Sets up the necessary event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{
			SessionManager.InputPageState pageState = TDSessionManager.Current.InputPageState;

			// Main buttons
			if ( pageState.MapType == CurrentLocationType.From ) 
			{
				this.buttonPlanAJourney.Click += new EventHandler(this.PlanJourneyTravelFrom_Click);
			} 
			else if ( pageState.MapType == CurrentLocationType.To) 
			{
				this.buttonPlanAJourney.Click += new EventHandler(this.PlanJourneyTravelTo_Click);
			} 
			else if ( pageState.MapType == CurrentLocationType.PrivateVia )
			{
				this.buttonPlanAJourney.Click += new EventHandler(this.PlanJourneyTravelVia_Click);
			} 
			else if ( pageState.MapType == CurrentLocationType.PublicVia )
			{
				this.buttonPlanAJourney.Click += new EventHandler(this.PlanJourneyTravelViaPt_Click);
			}
			else if ( pageState.MapType == CurrentLocationType.VisitPlannerOrigin)
			{
				this.buttonPlanAJourney.Click += new EventHandler(this.PlanJourneyVisitOrigin_Click);
			}

			else if ( pageState.MapType == CurrentLocationType.VisitPlannerVisitPlace1)
			{
				this.buttonPlanAJourney.Click += new EventHandler(this.PlanJourneyVisitLocation1_Click);
			}

			else if ( pageState.MapType == CurrentLocationType.VisitPlannerVisitPlace2)
			{
				this.buttonPlanAJourney.Click += new EventHandler(this.PlanJourneyVisitLocation2_Click);
			}
			else
			{
				this.buttonPlanAJourney.Click += new EventHandler( this.buttonPlanAJourney_Click );
			}
			this.buttonFindInformation.Click += new EventHandler(this.buttonFindInformation_Click);
			this.buttonSelectLocation.Click += new EventHandler(this.buttonSelectLocation_Click);

			//MapPlanJourneyLocationControl events
			this.mapPlanJourneyLocationControl.ButtonPlanCancel.Click += new EventHandler( this.buttonCancel_Click );
			this.mapPlanJourneyLocationControl.ButtonTravelFromLocation.Click += new EventHandler( this.PlanJourneyTravelFrom_Click );
			this.mapPlanJourneyLocationControl.ButtonTravelToLocation.Click += new EventHandler( this.PlanJourneyTravelTo_Click );

			//MapFindInformationLocationControl events
			this.mapFindInformationLocationControl.ButtonCancel.Click += new EventHandler( this.buttonCancel_Click );
			this.mapFindInformationLocationControl.ButtonSelectNewLocation.Click += new EventHandler( this.buttonSelectLocation_Click );

			//MapSelectLocationControl events
			this.mapSelectLocationControl.ButtonCancel.Click += new EventHandler( this.buttonCancel_Click );
			this.mapSelectLocationControl.ButtonSelectOK.Click += new EventHandler ( this.buttonSelectOK_Click );
		}


		/// <summary>
		/// Sets the label text for the control according to whether it
		/// is a public or private journey.
		/// </summary>
		/// <returns></returns>
		private void SetDefaultText() 
		{

			if ( outputMap && !stationMap ) 
			{
				if ( UsesReturnJourney ) //Is a return journey?
				{
					//This is the return journey
					labelOptions.Text = Global.tdResourceManager.GetString(  "MapLocationControl.labelOptions.Text.OutputPublic"  , TDCultureInfo.CurrentUICulture) + "<br/>";
					labelOptions2.Text = Global.tdResourceManager.GetString(  "MapLocationControl.labelOptions2.Text.OutputPublic"  , TDCultureInfo.CurrentUICulture);
				} 
				else 
				{
					//This is the outward journey
					labelOptions.Text = Global.tdResourceManager.GetString(  "MapLocationControl.labelOptions.Text.OutputPublic"  , TDCultureInfo.CurrentUICulture) + "<br/>";
					labelOptions2.Text = Global.tdResourceManager.GetString(  "MapLocationControl.labelOptions2.Text.OutputPublic"  , TDCultureInfo.CurrentUICulture);
				}
			} 
			else 
			{
				labelOptions.Text = Global.tdResourceManager.GetString(  "MapLocationControl.labelOptions.Text"  , TDCultureInfo.CurrentUICulture);
				labelOptions2.Text = string.Empty;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e ) 
		{
			this.DataBind();
			if ( MapState.State == StateEnum.Default )
			{
				// Display the buttons not the labels
                SetControlVisible(headerButtonRow, true);
               
				SetControlVisible(headerTextRow, false);

				SetControlVisible(labelOptions, true);
				SetControlVisible(labelOptions2, true);
				SetDefaultText();

				//If this is an output map do not display the plan a journey tab
				SetControlVisible(buttonPlanAJourney, !outputMap);

				SetControlVisible(buttonFindInformation, true);
				SetControlVisible(buttonSelectLocation, true);

				SetControlVisible(mapPlanJourneyLocationControl, false);
				SetControlVisible(mapFindInformationLocationControl, false);
				SetControlVisible(mapSelectLocationControl, false);
			}
			else if ( MapState.State == StateEnum.Plan )
			{
				// Display the labels not the buttons
                SetControlVisible(headerButtonRow, false);
              
				SetControlVisible(headerTextRow, true);

				SetControlVisible(labelOptions, false);
				SetControlVisible(labelOptions2, false);

				//If this is an output map do not display the plan a journey tab
				//This case should never happen
				SetControlVisible(labelPlanAJourney, !outputMap);

				SetControlVisible(labelFindInformation, false);
				SetControlVisible(labelSelectLocation, false);

				SetControlVisible(mapPlanJourneyLocationControl, true);
				SetControlVisible(mapFindInformationLocationControl, false);
				SetControlVisible(mapSelectLocationControl, false);
			} 
			else if ( MapState.State == StateEnum.FindInformation )
			{
				// Display the labels not the buttons
                SetControlVisible(headerButtonRow, false);
               
				SetControlVisible(headerTextRow, true);

				SetControlVisible(labelOptions, false);
				SetControlVisible(labelOptions2, false);

				SetControlVisible(labelPlanAJourney, false);
				SetControlVisible(labelFindInformation, true);
				SetControlVisible(labelSelectLocation, false);

				SetControlVisible(mapPlanJourneyLocationControl, false);
				SetControlVisible(mapFindInformationLocationControl, true);
				SetControlVisible(mapSelectLocationControl, false);
			}
			else if ( (MapState.State == StateEnum.Select ) 
				|| MapState.State == StateEnum.Select_Option )
			{
				// Display the labels not the buttons
                SetControlVisible(headerButtonRow, false);
                
				SetControlVisible(headerTextRow, true);

				SetControlVisible(labelOptions, false);
				SetControlVisible(labelOptions2, false);

				SetControlVisible(labelPlanAJourney, false);
				SetControlVisible(labelFindInformation, false);
				SetControlVisible(labelSelectLocation, true);

				SetControlVisible(mapPlanJourneyLocationControl, false);
				SetControlVisible(mapFindInformationLocationControl, false);
				SetControlVisible(mapSelectLocationControl, true);
			}

			// Make sure that client display is in sync with server state.
			// Inconsistencies can arise due to use of JavaScript on client.
			AlignClientWithServer();
			//Check for Javascript presence and register client script methods if appropriate
			EnableScriptableObjects();
			
			//If the select mode is chosen and the map is at a zoom level that allows
			//for selection of location then fire a event requesting a change to
			//the map mode.
			if ( ( (MapState.State == StateEnum.Select)
				||(MapState.State == StateEnum.Select_Option) ) 
				&& ( MapState.SelectEnabled == true )  ) 
			{

				ModeChangedEventArgs eventArgs = new ModeChangedEventArgs( TransportDirect.Presentation.InteractiveMapping.Map.ClickModeType.Query );
				// Fire the appropriate event defined in the event declaration
				// Add new MapMode to the event args
				ModeChangeEvent( this, eventArgs );
			}
			else 
			{
				ModeChangedEventArgs eventArgs = new ModeChangedEventArgs( TransportDirect.Presentation.InteractiveMapping.Map.ClickModeType.ZoomIn );
				// Fire the appropriate event defined in the event declaration
				// Add new MapMode to the event args
				ModeChangeEvent( this, eventArgs );
			}

			base.OnPreRender(e);
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			carParkCatalogue = 
				(ICarParkCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarParkCatalogue];

			labelPlanAJourney.Text =
				Global.tdResourceManager.GetString( "MapLocationControl.labelPlanAJourney.Text"  , TDCultureInfo.CurrentUICulture)
				+ "&nbsp;";
			labelFindInformation.Text =
				Global.tdResourceManager.GetString(  "MapLocationControl.labelFindInformation.Text" , TDCultureInfo.CurrentUICulture)
				+ "&nbsp;";;
			labelSelectLocation.Text =
				Global.tdResourceManager.GetString(  "MapLocationControl.labelSelectLocation.Text" , TDCultureInfo.CurrentUICulture)
				+ "&nbsp;";;

			buttonPlanAJourney.ToolTip = GetResource("MapLocationControl.buttonPlanAJourney.AlternateText");
			buttonPlanAJourney.Text = GetResource("MapLocationControl.buttonPlanAJourney.Text");

			buttonFindInformation.ToolTip = GetResource("MapLocationControl.buttonFindInformation.ToolTip");
			buttonFindInformation.Text = GetResource("MapLocationControl.buttonFindInformation.Text");

			buttonSelectLocation.ToolTip = GetResource("MapLocationControl.buttonSelectLocation.AlternateText");
			buttonSelectLocation.Text = GetResource("MapLocationControl.buttonSelectLocation.Text");

			if (Page.IsPostBack) 
			{
				// Update server state with client state.
				// Inconsistencies can arise due to use of JavaScript on client.
				AlignServerWithClient();
			}
		}


		#region Web Form Designer generated code
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{
			ExtraWiringEvents();
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		/// <summary>
		/// Populates the MapLocation in the session with the CarParking object. 
		/// Assigns the GridReference and TOIDs to use for the location.
		/// </summary>
		/// <param name="origin">boolean true if the car park is the journey origin location</param>
		public void SetupCarParkPlanning(bool origin) 
		{
			//Load car park data for car park
			ICarParkCatalogue carParkCatalogue = (ICarParkCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarParkCatalogue];
			string carParkRef = TDSessionManager.Current.InputPageState.CarParkReference;

			// access the car park data
			CarPark carPark = carParkCatalogue.GetCarPark(carParkRef);				
			
			// set the car park
			TDLocation theLocation = TDSessionManager.Current.InputPageState.MapLocation;

			if (carPark != null)
			{
				theLocation.CarParking = carPark;

				// poulate the grid reference, address to match, and toids
				theLocation = SetUpGridRefenceAndToids(theLocation, carPark, origin);

				// set the location locality, prevents ambiguity page being displayed in journey planning
				IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
				theLocation.Locality = gisQuery.FindNearestLocality(
					theLocation.GridReference.Easting, theLocation.GridReference.Northing);

				// Because the car park can use different entrance and exit coordinates
				// need to set up to return to the same location but using the appropriate coordinates
				TDLocation theReturnLocation = new TDLocation(carParkRef, theLocation.Description, !origin);			           			

				if (origin)
					TDSessionManager.Current.JourneyParameters.ReturnDestinationLocation = theReturnLocation;
				else
					TDSessionManager.Current.JourneyParameters.ReturnOriginLocation = theReturnLocation;
			}
			else
			{
				// Log car park was not found in the Car park catalogue
				Logger.Write (new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Warning, 
					"Unable to find Car park in the CarParkCatalogue, ID: " + carParkRef + " " + theLocation.Description ));

				// Attempt to populate as much information as we can to carry on planning the journey
				// to the click point. This will mean the Car park cost is not shown in the Tickets/costs page
				// because the car park object was not found.
				theLocation.PopulateToids();
				
				// set the location locality
				IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
				theLocation.Locality = gisQuery.FindNearestLocality(
					theLocation.GridReference.Easting, theLocation.GridReference.Northing);
			}

			TDSessionManager.Current.InputPageState.MapLocation = theLocation;
		}

		/// <summary>
		/// Checks the planning point of the car park to determine which grid
		/// reference should be used for journey planning. If the car park 
		/// should be used as a planning point, the map grid reference of 
		/// the centre of the car park should be used. If the car park is 
		/// not a planning point, and there is a street name for the entrance
		/// (if the car park is the journey destination), or the exit (if
		/// the car park is the journey origin), then use the grid reference
		/// for the street. If no street name is available, use road name
		/// search ...
		/// </summary>
		/// <param name="theLocation"></param>
		/// <param name="carPark"></param>
		/// <param name="origin"></param>
		/// <returns></returns>
		private TDLocation SetUpGridRefenceAndToids (TDLocation theLocation, CarPark carPark, bool origin)
		{	
			// check the access point
			CarParkAccessPoint[] pointsList = carPark.AccessPoints;

			for(int i=0; i<pointsList.Length; i++)
			{
				CarParkAccessPoint accessPoint = pointsList[i];
				string geocodeType = accessPoint.GeocodeType;

				if(carPark.PlanningPoint)
				{
					// if its a planning point and a map access point
					if( string.Compare(geocodeType,PARKING_MAP, true) == 0)
					{
						theLocation.GridReference = accessPoint.GridReference;
						theLocation.Toid = carPark.GetMapToids();
					}
				}
				else 
				{
					// car park is the origin so use the exit co-ordinates if there are any
					if(accessPoint.StreetName != null) 
					{
						if(origin)
						{
							// get the exit street name to use for name search
							if (string.Compare(geocodeType, PARKING_EXIT, true) == 0)
							{
								theLocation.AddressToMatch = accessPoint.StreetName;
								theLocation.GridReference = accessPoint.GridReference;
								theLocation.Toid = carPark.GetExitToids();
							}
						}
						else
						{
							// get the entrance street name to use for name search
							if (string.Compare(geocodeType, PARKING_ENTRANCE, true) == 0)
							{
								theLocation.AddressToMatch = accessPoint.StreetName;
								theLocation.GridReference = accessPoint.GridReference;
								theLocation.Toid = carPark.GetEntranceToids();
							}
						}
					}
					else
					{
						if(origin)
						{
							// use the exit grid reference
							if (string.Compare(geocodeType, PARKING_EXIT, true) == 0)
							{
								theLocation.GridReference = accessPoint.GridReference;	
								theLocation.Toid = carPark.GetExitToids();
							}
						}
						else 
						{
							// use the entrance grid reference
							if (string.Compare(geocodeType, PARKING_ENTRANCE, true) == 0)
							{
								theLocation.GridReference = accessPoint.GridReference;
								theLocation.Toid = carPark.GetEntranceToids();
							}
						}
					}
				}
			}
		
			return theLocation;
		}

		#region Event Handlers
		/// <summary>
		/// Click event for the PlanAJourney button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPlanAJourney_Click(object sender, EventArgs e)
		{
			MapState.State = StateEnum.Plan;
		}

		/// <summary>
		/// Click event for the SelectLocation button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSelectLocation_Click(object sender, EventArgs e)
		{
			MapState.State = StateEnum.Select;
		}


		/// <summary>
		/// Click event for the Cancel button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonCancel_Click(object sender, EventArgs e)
		{
			MapState.State =  StateEnum.Default;
		}


		/// <summary>
		/// Click event for the OK button of the MapSelectLocationControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonSelectOK_Click(object sender, EventArgs e)
		{
			MapState.State = StateEnum.Default;
			MapState.Location = mapSelectLocationControl.SelectedLocation;
			MapState.LocationSearch = new LocationSearch();
			MapState.LocationSearch.InputText = MapState.Location.Description;
			MapState.LocationSearch.SearchType = SearchType.Map;

			TDSessionManager.Current.InputPageState.MapLocation = MapState.Location;
			TDSessionManager.Current.InputPageState.MapLocationSearch = MapState.LocationSearch;

			// Get the x,y coordinate of the selected location.
			string dropDownListValue = mapSelectLocationControl.SelectedItem.Value;

			// if the selected location is a car park, obtain the car park reference
			// for later use 
			if(mapSelectLocationControl.SelectedItemIsCarPark())
			{
				TDSessionManager.Current.InputPageState.CarParkReference = 
					mapSelectLocationControl.SelectedCarParkReference;
			}
			else
			{
				TDSessionManager.Current.InputPageState.CarParkReference = 
					string.Empty;
			}

			// Split the value to retrive the x,y coord and location name
			string[] split = dropDownListValue.Split( MapSelectLocationControl.SEPARATOR );
			string xString = split[ MapSelectLocationControl.DROPD_X ];
			string yString = split[ MapSelectLocationControl.DROPD_Y ];

			// Cast to double first (as the coordinates may have decimal points)
			// e.g. x and y for ITN.
			double xDouble = Convert.ToDouble(xString, TDCultureInfo.CurrentUICulture.NumberFormat);
			double yDouble = Convert.ToDouble(yString, TDCultureInfo.CurrentUICulture.NumberFormat);

			// Now cast the double into an int
			int xValue = Convert.ToInt32(xDouble, TDCultureInfo.CurrentUICulture.NumberFormat);
			int yValue = Convert.ToInt32(yDouble, TDCultureInfo.CurrentUICulture.NumberFormat);

			OnLocationSelected(xValue, yValue, mapSelectLocationControl.SelectedItem.Text);
		}

		/// <summary>
		/// Click event for the FindInformation button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonFindInformation_Click(object sender, EventArgs e) 
		{
			if (outputMap)
			{
				if ((MapState.Location.Description == null) || (MapState.Location.Description.Length == 0))
				{
					MapState.State = StateEnum.FindInformation;
					return;
				}
			}

			if( MapState.Location.Status != TDLocationStatus.Valid )
			{
				MapState.Location = TDSessionManager.Current.InputPageState.MapLocation;
			}

			// Fetch naptan (or possible empty naptan)
			if( MapState.Location.Status == TDLocationStatus.Valid
				&& MapState.Location != null)
			{

				if(MapState.Location.NaPTANs.Length > 0 )
				{
					TDSessionManager.Current.InputPageState.AdditionalDataLocation
						= MapState.Location.NaPTANs[0].Naptan;

					#region Vantive 4054610 fix to ensure station info is always displayed
					//Search through NaPTANs - if there's one with an exact match 
					//on Grid Reference then that's the actual one selected so use it
					//instead of just whatever is at element zero! 
					//NB there are also changes in MapSelectLocationControl.SelectedLocation property
					int collIndex = 0;
					foreach (TDNaptan currentNaptan in MapState.Location.NaPTANs)
					{
						if ((currentNaptan.GridReference.Easting == MapState.Location.GridReference.Easting)
							&& (currentNaptan.GridReference.Northing == MapState.Location.GridReference.Northing))
						{
							TDSessionManager.Current.InputPageState.AdditionalDataLocation
								= MapState.Location.NaPTANs[collIndex].Naptan;
							break;
						}
						collIndex++;
					}
					#endregion

				} 
				// its there are car parks
				if(MapState.Location.CarParkReferences != null && MapState.Location.CarParkReferences.Length > 0)
				{
					
					TDSessionManager.Current.InputPageState.CarParkReference =
						MapState.Location.CarParkReferences[0];

					carParkSelected = true;
				}

				TDSessionManager.Current.InputPageState.AdditionalDataDescription
					= MapState.Location.Description;
			}
			else 
			{
				TDSessionManager.Current.InputPageState.AdditionalDataLocation
					= string.Empty;
			}

			MapState.State = StateEnum.Default;

			// This is how we 'return'
			TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push( pageId );

			// Fire event for anyone interested
			OnInformationRequested();
			
			if(carParkSelected)
			{
				// Show the information page for the selected car park.
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.CarParkInformation;
			}
			else 
			{
                //CCN526 changes
                SetStopInformation(TDSessionManager.Current.InputPageState.AdditionalDataLocation);
				// Show the information page for the selected location.
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.StopInformation;
			}
		}

		#endregion

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
		/// Fires the information requested event
		/// </summary>
		private void OnInformationRequested()
		{
			if( InformationRequestedEvent != null )
			{
				// Fire the appropriate event defined in the event declaration
				// Add new MapMode to the event args
				InformationRequestedEvent( this, EventArgs.Empty );
			}
		}

		/// <summary>
		/// Fires the Location Selected event
		/// </summary>
		private void OnLocationSelected(int x, int y, string name)
		{
			if( LocationSelectedEvent != null )
			{
				LocationSelectedEventArgs eventArgs = new LocationSelectedEventArgs(x, y, name);

				// Fire the appropriate event defined in the event declaration
				// Add new MapMode to the event args
				LocationSelectedEvent( this,  eventArgs );
			}
		}

		#region JourneyMapState tracking methods
		///<summary>
		/// The EnableClientScript property of a scriptable control is set so that they
		/// output an action attribute when appropriate.
		/// If JavaScript is enabled then appropriate script blocks are added to the page.
		///</summary>
		protected void EnableScriptableObjects()
		{
			// Determine if JavaScript is supported
			bool javaScriptSupported = ((TDPage)Page).IsJavascriptEnabled;
			string javaScriptDom = ((TDPage)Page).JavascriptDom;
			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
			bool locationUnresolved = ((MapState.Location.Description == null) || (MapState.Location.Description.Length == 0));

			if (javaScriptSupported)
			{
				if(TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.FindJourneys || 
					TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.TravelBoth || TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.TravelFrom
					|| TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.TravelTo)
				{
					//imageButtonPlanAJourney.EnableClientScript = true;
					buttonPlanAJourney.EnableClientScript = true;

				}

			}

			// Register Client scripts, add script reference, ensure control is present.
			// Else, make sure client script is disabled
			//buttonPlanAJourney.EnableClientScript = (javaScriptSupported && (TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromJourneyInput && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromFindAInput));
			buttonFindInformation.EnableClientScript = (javaScriptSupported && outputMap && locationUnresolved);
			buttonSelectLocation.EnableClientScript = javaScriptSupported;

			if (buttonPlanAJourney.EnableClientScript)
				Page.ClientScript.RegisterStartupScript(typeof(MapLocationControl), buttonPlanAJourney.ScriptName, scriptRepository.GetScript(buttonPlanAJourney.ScriptName, javaScriptDom));
			if (buttonFindInformation.EnableClientScript)
                Page.ClientScript.RegisterStartupScript(typeof(MapLocationControl), buttonFindInformation.ScriptName, scriptRepository.GetScript(buttonFindInformation.ScriptName, javaScriptDom));
			if (buttonSelectLocation.EnableClientScript)
                Page.ClientScript.RegisterStartupScript(typeof(MapLocationControl), buttonSelectLocation.ScriptName, scriptRepository.GetScript(buttonSelectLocation.ScriptName, javaScriptDom));
		}

		///<summary>
		/// The client may have changed things through JavaScript so need to update server state.
		///</summary>
		private void AlignServerWithClient()
		{
			if (Request.Params[this.ClientID + "_hdnJourneyMapState"] != null)
			{
				MapState.State = (StateEnum) Enum.Parse(typeof(StateEnum), Request.Params[this.ClientID + "_hdnJourneyMapState"], true);
				if ( ( (MapState.State == StateEnum.Select)
					||(MapState.State == StateEnum.Select_Option) ) 
					&& ( MapState.SelectEnabled == true )  ) 
				{
					ModeChangedEventArgs eventArgs = new ModeChangedEventArgs( TransportDirect.Presentation.InteractiveMapping.Map.ClickModeType.Query );
					// Fire the appropriate event defined in the event declaration
					// Add new MapMode to the event args
					ModeChangeEvent( this, eventArgs );
				}
				else 
				{
					ModeChangedEventArgs eventArgs = new ModeChangedEventArgs( TransportDirect.Presentation.InteractiveMapping.Map.ClickModeType.ZoomIn );
					// Fire the appropriate event defined in the event declaration
					// Add new MapMode to the event args
					ModeChangeEvent( this, eventArgs );
				}
			}
		}
		
		///<summary>
		/// The client and server need to be kept in sync.
		///</summary>
		private void AlignClientWithServer()
		{
			// Update the Action properties according to control ID

			buttonPlanAJourney.Action = "return Plan('"+this.ClientID+"');";

			// Determine which view to display for SelectLocation control.
			string selectEnabled = MapState.SelectEnabled.ToString();
			buttonSelectLocation.Action = "return SelectLocation('"+this.ClientID+"', "+selectEnabled.ToLower()+");";

			// Determine which view to display for FindInformation control.
			string locationUnresolved = (outputMap && ((MapState.Location.Description == null) || (MapState.Location.Description.Length == 0))) ? "true" : "false";
			buttonFindInformation.Action = "return FindInformation('"+this.ClientID+"', "+locationUnresolved.ToLower()+");";
		}
		#endregion


		/// <summary>
		/// Sets the visibility of the control using clientside attributes
		/// </summary>
		/// <param name="thisControl"></param>
		/// <param name="visible"></param>
		private void SetControlVisible(Control thisControl, bool visible)
		{
			if (thisControl is WebControl)
			{
				if (visible)
				{
					((WebControl)thisControl).Attributes.Remove("style");
				}
				else
				{
					((WebControl)thisControl).Attributes.Add("style", "display:none");
				}
			}
			else if (thisControl is HtmlControl)
			{
				if (visible)
				{
					((HtmlControl)thisControl).Attributes.Remove("style");
				}
				else
				{
					((HtmlControl)thisControl).Attributes.Add("style", "display:none");
				}
			}
			else if (thisControl is TDUserControl) 
			{
				if (visible)
				{
					((TDUserControl)thisControl).Attributes.Remove("style");
				}
				else
				{
					((TDUserControl)thisControl).Attributes.Add("style", "display:none");
				}
			}
		}
	}


	/// <summary>
	/// Delegate - Event Handler if selected mode changes
	/// </summary>
	public delegate void ModeChangedEventHandler(object sender, ModeChangedEventArgs e);

	/// <summary>
	/// Delegate - Event Handler if location selected
	/// </summary>
	public delegate void LocationSelectedEventHandler(object sender, LocationSelectedEventArgs e);

	/// <summary>
	/// 
	/// </summary>
	public class ModeChangedEventArgs : EventArgs
	{
		private TransportDirect.Presentation.InteractiveMapping.Map.ClickModeType mode;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="mode">Map Mode.</param>
		public ModeChangedEventArgs(TransportDirect.Presentation.InteractiveMapping.Map.ClickModeType mode)
		{
			this.mode = mode;
		}

		/// <summary>
		/// Get property to retrieve the map mode.
		/// </summary>
		public TransportDirect.Presentation.InteractiveMapping.Map.ClickModeType MapMode
		{
			get
			{
				return mode;
			}
		}
	}


	/// <summary>
	/// 
	/// </summary>
	public class LocationSelectedEventArgs : EventArgs
	{
		private int x;
		private int y;
		private string name;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="x">x</param>
		/// <param name="x">y</param>
		public LocationSelectedEventArgs(int x, int y, string name)
		{
			this.x = x;
			this.y = y;
			this.name = name;
		}

		/// <summary>
		/// Get property to retrieve x coordinate
		/// </summary>
		public int CoordinateX
		{
			get
			{
				return x;
			}
		}

		/// <summary>
		/// Get property to retrieve y coordinate
		/// </summary>
		public int CoordinateY
		{
			get
			{
				return y;
			}
		}

		/// <summary>
		/// Get property to retrieve the location name.
		/// </summary>
		public string LocationName
		{
			get
			{
				return name;
			}
		}
	}
}