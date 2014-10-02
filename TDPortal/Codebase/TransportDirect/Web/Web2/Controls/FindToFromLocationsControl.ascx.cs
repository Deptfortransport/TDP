// *************************************************************************** 
// NAME                 : FindToFromLocationsControl.ascx
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 08/07/2004 
// DESCRIPTION			: This control is a container for the FindLocationControl
// FindStationDisplayControl instances on 'Find a' pages
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindToFromLocationsControl.ascx.cs-arc  $
//
//   Rev 1.7   Jun 17 2010 12:21:34   apatel
//remove the filter controls set for auto suggest
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.6   Jun 16 2010 10:21:10   apatel
//Updated to implement auto-suggest functionality
//
//   Rev 1.5   Apr 23 2010 16:18:14   mmodi
//Public method to allow the scriptable drop down list to be used in the child control
//Resolution for 5521: TD Extra - Drop Down List Change - CCN0575
//
//   Rev 1.4   Oct 13 2008 16:41:40   build
//Automatically merged from branch for stream5014
//
//   Rev 1.3.1.0   Sep 02 2008 11:22:44   mmodi
//Overloaded RefreshControl method to enable favourite journeys to be shown on cycle input page
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 08 2008 11:41:06   mmodi
//Changes made to multiple files to fix the find a car park feature of the city-to-city trunk mode. Testing performed to ensure that the trunk car park features has not broken the drive to car park mode.
//Resolution for 4954: Include 'Drive to Car Park' functionality in City to City
//
//   Rev 1.2   Mar 31 2008 13:20:54   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 05 2008 13:00:00   mmodi
//Added method to set the location drop down lists to amendable mode
//
//   Rev 1.0   Nov 08 2007 13:14:34   mturner
//Initial revision.
//
//   Rev 1.23   Oct 06 2006 14:31:54   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.22.1.1   Aug 14 2006 16:59:40   esevern
//corrected to only display one location control for car park input
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.22.1.0   Aug 14 2006 10:44:44   esevern
//Added FindCarParkInput page to control visibility checks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.22   Feb 23 2006 19:16:44   build
//Automatically merged from branch for stream3129
//
//   Rev 1.21.1.0   Jan 10 2006 15:25:00   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.21   Nov 17 2005 11:26:20   pcross
//Manual merge of stream2880
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.19.1.0   Nov 04 2005 15:28:04   schand
//Added public void RefreshControl() for FindAPlaceControl
//Resolution for 2880: DEL 7.3 Stream UEE work on Homepage
//
//   Rev 1.20   Nov 10 2005 14:27:14   NMoorhouse
//TD093 UEE Input Pages - Soft Content
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.19   Feb 15 2005 16:16:20   tmollart
//Added SetLocationsUnspecified method which can be called at this level to reset the location controls.
//
//   Rev 1.18   Nov 18 2004 15:44:38   passuied
//Changes to enable display of stations even when Search.SearchType was overwritten in FindPlaceDD population. Use Location.SearchType instead
//Resolution for 1748: City to city amend search - Selected From location is reverted back to default when new location is selected in To section or vice versa
//
//   Rev 1.17   Nov 03 2004 12:54:06   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.16   Nov 02 2004 16:53:12   passuied
//Cleaning the code!
//
//   Rev 1.15   Nov 02 2004 11:40:52   passuied
//fixed various display bugs
//
//   Rev 1.14   Nov 01 2004 18:04:10   passuied
//Changes for FindPlace new functionality
//
//   Rev 1.13   Aug 27 2004 17:03:14   RPhilpott
//Correct interaction between control hierarchy and LocationSearchHelper.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.12   Aug 13 2004 14:11:16   jmorrissey
//Updated SetControlVisibility method so that the toLocationControl visibility is set correctly
//
//   Rev 1.11   Jul 27 2004 14:03:12   passuied
//FindStation Del6.1 :Finalised version 
//
//   Rev 1.10   Jul 26 2004 20:23:52   passuied
//Changes to implement AmendSeach Functionality. Created and Amend mode in the tristate to enable the display of a valid location inside the locationUnspecified control.
//We send this mode when a one use session key has been set by a click on AmendSearch button.
//Also tweak in toFromLocationControl to display the to and from location/station controls correctly
//
//   Rev 1.9   Jul 22 2004 18:06:00   passuied
//Integration between pages and move of code to location service
//
//   Rev 1.8   Jul 22 2004 10:26:06   COwczarek
//Fix condition for display "to" location control when not on station input page
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.7   Jul 21 2004 15:26:36   passuied
//Changes to implement the New Location Button func for all different controls
//
//   Rev 1.6   Jul 21 2004 11:58:50   passuied
//Changes so the Refresh is done in the OnPreRender
//
//   Rev 1.5   Jul 21 2004 10:51:10   passuied
//Re work for integration with FindStation del6.1. Working. Needs work on resources
//
//   Rev 1.4   Jul 16 2004 14:51:36   COwczarek
//Complete implementation. Remove redundant code
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.3   Jul 14 2004 17:25:48   jmorrissey
//Fixed warnings generated when doing build of this file
//
//   Rev 1.2   Jul 14 2004 14:16:14   jmorrissey
//Coding complete. FxCop run. Needs integration testing with the various Find a pages.
//
//   Rev 1.1   Jul 13 2004 09:59:04   jmorrissey
//Interim version for use in development of new Find a pages
//
//   Rev 1.0   Jul 08 2004 17:17:48   jmorrissey
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	#region .Net namespaces

	using System;using TransportDirect.Common.ResourceManager;
	using System.Collections;
	using System.Data;	
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	#endregion

	#region Transport Direct namespaces

	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.UserPortal.LocationService;	
	using TransportDirect.Common.ServiceDiscovery;	
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.Common;

	#endregion	

	/// <summary>
	///	Summary description for FindToFromLocationsControl.
	/// </summary>
	public partial  class FindToFromLocationsControl : TDUserControl
	{

		#region Controls

		protected TransportDirect.UserPortal.Web.Controls.FindLocationControl fromLocationControl;
		protected TransportDirect.UserPortal.Web.Controls.FindLocationControl toLocationControl;
		protected TransportDirect.UserPortal.Web.Controls.FindStationsDisplayControl fromStationsControl;
		protected TransportDirect.UserPortal.Web.Controls.FindStationsDisplayControl toStationsControl;

		#endregion


		public event EventHandler NewLocationFrom;
		public event EventHandler NewLocationTo;

		#region PageLoad
		/// <summary>
		/// Page Load method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

			fromLocationControl.LocationType = CurrentLocationType.From;
			toLocationControl.LocationType = CurrentLocationType.To;
			fromStationsControl.LocationType = CurrentLocationType.From;
			toStationsControl.LocationType = CurrentLocationType.To;

		}

		/// <summary>
		/// Overriden OnPreRender method
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender (EventArgs e)
		{
			//set which controls should be visible 
			SetControlVisibility();
			
			base.OnPreRender(e);

		}


		#endregion

		#region Controls Visibilty 
		
		/// <summary>
		/// set controls visible according to whether a nearest station has been found and 
		/// whether it is a To or From control
		/// </summary>
		private void SetControlVisibility()
		{	
			bool fromFound;
			bool tofound;

			if(this.PageId == PageId.FindCarParkInput)
			{
				fromFound = NearestCarParkFound(true);  
				tofound = NearestCarParkFound(false);

				toLocationControl.Visible = tofound &&  
					((PageId == Common.PageId.FindCarParkInput && fromFound));
			}
			else 
			{
				fromFound = NearestStationFound(true); 
				tofound = NearestStationFound(false);

				toLocationControl.Visible = !tofound &&  
					((PageId == Common.PageId.FindStationInput && fromFound) 
					|| (PageId != Common.PageId.FindStationInput));

			}
			fromStationsControl.Visible = fromFound;  
			fromLocationControl.Visible = !fromFound; 
			toStationsControl.Visible = tofound;  
		
			// hide StationTypes check list for To location at all time
			toLocationControl.StationTypesCheckListVisible = false;
			// show StationTypes check list for From location when in FindStation Mode
			fromLocationControl.StationTypesCheckListVisible = 
				(TDSessionManager.Current.FindPageState.Mode == FindAMode.Station) // in StationMode we are able to select the StationType
				// don't display it if in ambiguous mode. 
				// if destination is valid, means half of the journey has been chosen. No need for station type anymore.
				&& (OriginLocation.Status == TDLocationStatus.Unspecified 
				&& DestinationLocation.Status != TDLocationStatus.Valid
					|| fromLocationControl.AmendMode); // display if in amendMode 

			// DirectionLabel Visibility (from/to)
			fromLocationControl.DirectionLabelVisible = DirectionLabelVisible;
			toLocationControl.DirectionLabelVisible = DirectionLabelVisible;
		}

		#endregion


		#region Visibility properties
		/// <summary>
		/// Show Direction label (from/to) in location controls
		/// if ! FindStationInput page, always
		/// if FindStationInput, never except if one of the location is valid.
		/// </summary>
		private bool DirectionLabelVisible
		{
			get
			{
				return (PageId != Common.PageId.FindStationInput)
					|| (PageId == Common.PageId.FindStationInput && !fromLocationControl.AmendMode)// don't display direction if in amend mode
					|| (PageId != Common.PageId.FindCarParkInput)
					|| (PageId != Common.PageId.FindCarParkInput && !fromLocationControl.AmendMode); 
			}
		}
		#endregion

		#region private methods

		/// <summary>
		/// returns whether a nearest station has been found
		/// </summary>
		/// <returns></returns>
		private bool NearestStationFound(bool from)
		{
			//return value, default to false
			bool nearestStationFound = false;

			//check for origin
			if (from)
			{
				// We use this check on the FindXXXInput pages and FindStationInput page
				// But on FindStation we dont need and we don't have the FindNearest SearchType
				// So don't check if FindNearest when in FindStationInput page
				if ((OriginLocation.SearchType == SearchType.FindNearest || PageId == Common.PageId.FindStationInput) 
					&& (OriginLocation.Status == TDLocationStatus.Valid) && !fromLocationControl.AmendMode)
				{
					nearestStationFound =  true;
				}
			}
			else 
			{
				// see above for condition
				if ((DestinationLocation.SearchType == SearchType.FindNearest || PageId == Common.PageId.FindStationInput) 
					&& (DestinationLocation.Status == TDLocationStatus.Valid) && !fromLocationControl.AmendMode)
				{
					nearestStationFound =  true;
				}							
			}

			//return evaluated boolean
			return nearestStationFound;				

		}

		/// <summary>
		/// returns whether a nearest car park has been found
		/// </summary>
		/// <returns></returns>
		private bool NearestCarParkFound(bool from)
		{
			//return value, default to false
			bool nearestCarParkFound = false;

			//check for origin
			if (from)
			{
				if ((OriginLocation.SearchType == SearchType.FindNearest
					|| PageId == Common.PageId.FindCarParkInput) 
					&& (OriginLocation.Status == TDLocationStatus.Valid) 
					&& !fromLocationControl.AmendMode)
				{
					nearestCarParkFound =  true;
				}
			}
			else 
			{
				// see above for condition
				if ((DestinationLocation.SearchType == SearchType.FindNearest 
					|| PageId == Common.PageId.FindCarParkInput) 
					&& (DestinationLocation.Status == TDLocationStatus.Valid) 
					&& !fromLocationControl.AmendMode)
				{
					nearestCarParkFound =  true;
				}							
			}
			return nearestCarParkFound;				
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Search Method. Calls back the appropriate method from the controls.
		/// </summary>
		public void Search()
		{
			
			fromLocationControl.Search();

			// only search destination if control is visible
			if (toLocationControl.Visible)
			{
				toLocationControl.Search();
			}
		}

		/// <summary>
		/// Sets the to/from locations as unspecified. Updates the place control
		/// drop down to its original value as well.
		/// </summary>
		public void SetLocationsUnspecified()
		{
			FromLocationControl.SetLocationUnspecified();
			ToLocationControl.SetLocationUnspecified();
		}

        /// <summary>
        /// Sets the to/from (drop down) locations as amendable, retaining selected location value.
        /// </summary>
        public void SetLocationsAmendable()
        {
            FromLocationControl.TheLocation.Status = TDLocationStatus.Unspecified;
            ToLocationControl.TheLocation.Status = TDLocationStatus.Unspecified;

            FromLocationControl.SetLocationDropDown();
            ToLocationControl.SetLocationDropDown();
        }
        
        /// <summary>
        /// Sets the from (drop down) location as amendable, retaining selected location value.
        /// </summary>
        public void SetFromLocationAmendable()
        {
            FromLocationControl.TheLocation.Status = TDLocationStatus.Unspecified;
            FromLocationControl.SetLocationDropDown();
        }

        /// <summary>
        /// Sets this input control to use the scriptable dropdown lists. This is used to dynamically 
        /// update the To input dropdown control based on the value selected in the From control.
        /// Note: This method does not force the display of the drop down list, this logic is maintained elsewhere
        /// </summary>
        /// <param name="restrictLocations">ArrayList string of ids in the dropdown to restrict/not show</param>
        public void SetScriptableDropdownList(ArrayList restrictLocationsFrom, ArrayList restrictLocationsTo)
        {
            // Only want to update the To control based on the From control
            FromLocationControl.SetDropDownScriptable(ToLocationControl.DropDownScriptableClientID, true, this.PageId, restrictLocationsFrom);
            ToLocationControl.SetDropDownScriptable(FromLocationControl.DropDownScriptableClientID, false, this.PageId, restrictLocationsTo);
        }

		/// <summary>
		/// This function Refreshes the triStateControl and call tristateLocationControl.Populate() method
		/// </summary>
		public void RefreshControl()
		{
			FromLocationControl.RefreshTristateControl(true); 
		}

        /// <summary>
        /// This function Refreshes the triStateControl and call tristateLocationControl.Populate() method
        /// for both the From and To controls
        /// </summary>
        /// <param name="checkInput">true or false for the control to check the input</param>
        public void RefreshControl(bool checkInput)
        {
            FromLocationControl.RefreshTristateControl(checkInput);
            ToLocationControl.RefreshTristateControl(checkInput);
        }

		#endregion
		
		#region Properties

		/// <summary>
		/// Read-write property. TDLocation session information for the origin. 
		/// </summary>
		public TDLocation OriginLocation
		{
			get
			{
				return fromLocationControl.TheLocation;
			}
			set
			{
				fromLocationControl.TheLocation = value;
				fromStationsControl.Location = value;
			}
		}

		/// <summary>
		/// Read-write property. TDLocation session information for the destination.  
		/// </summary>
		public TDLocation DestinationLocation
		{
			get
			{
				return toLocationControl.TheLocation;
			}
			set
			{
				toLocationControl.TheLocation = value;
				toStationsControl.Location = value;
			}
		}

		/// <summary>
		/// Read-write property. LocationSearch session information for the origin.
		/// </summary>
		public LocationSearch OriginSearch
		{
			get
			{
				return fromLocationControl.TheSearch;
			}
			set
			{
				fromLocationControl.TheSearch = value;
			}
		}

		/// <summary>
		/// Read-write property. LocationSearch session information for the destination.
		/// </summary>
		public LocationSearch DestinationSearch
		{
			get
			{
				return toLocationControl.TheSearch;
			}
			set
			{
				toLocationControl.TheSearch = value;
			}
		}

		/// <summary>
		/// Read-write property. ControlType session information for the origin.
		/// </summary>
		public TDJourneyParameters.LocationSelectControlType OriginControlType
		{
			get
			{
				return fromLocationControl.LocationControlType;
			}
			set
			{
				fromLocationControl.LocationControlType = value;
			}
		}

		/// <summary>
		/// Read-write property. ControlType session information for the destination.
		/// </summary>
		public TDJourneyParameters.LocationSelectControlType DestinationControlType
		{
			get
			{
				return toLocationControl.LocationControlType;
			}
			set
			{
				toLocationControl.LocationControlType= value;
			}
		}


		/// <summary>
		/// Read only property returning the origin FindLocationControl
		/// </summary>
		public FindLocationControl FromLocationControl 
		{
			get 
			{
				return fromLocationControl;
			}
		}

		/// <summary>
		/// Read only property returning the destination FindLocationControl
		/// </summary>
		public FindLocationControl ToLocationControl 
		{
			get 
			{
				return toLocationControl;
			}
		}

		/// <summary>
		/// Read only property returning the origin FindStationsDisplayControl
		/// </summary>
		public FindStationsDisplayControl FromStationsControl 
		{
			get 
			{
				return fromStationsControl;
			}
		}

		/// <summary>
		/// Read only property returning the destination FindStationsDisplayControl
		/// </summary>
		public FindStationsDisplayControl ToStationsControl 
		{
			get 
			{
				return toStationsControl;
			}
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			fromStationsControl.NewLocationClick += new EventHandler(OnFromNewLocation);
			toStationsControl.NewLocationClick += new EventHandler(OnToNewLocation);
			fromLocationControl.NewLocation += new EventHandler(OnFromNewLocation);
			toLocationControl.NewLocation += new EventHandler(OnToNewLocation);
			fromLocationControl.SelectedLocation += new EventHandler (OnFromSelectedLocation);
			toLocationControl.SelectedLocation += new EventHandler (OnToSelectedLocation);

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
		
		#region Event Handlers
		private void OnFromNewLocation (object sender, EventArgs e)
		{
			if (PageId == Common.PageId.FindTrunkInput)
			{

				if ( TDSessionManager.Current.FindAMode == FindAMode.TrunkStation)
				{
					TDSessionManager.Current.FindStationPageState.LocationType = FindStationPageState.CurrentLocationType.From;
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
					return;
				}

				// Force the page to do !Postback. Otherwise Place list doesn't get populated
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;


			}

			

			
			OriginLocation = new TDLocation();
			OriginSearch = new LocationSearch();
			OriginControlType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);

			// Raise Event to indicate to the page the location From has changed
			if (NewLocationFrom != null)
				NewLocationFrom(sender, e);
		

		}

		private void OnToNewLocation (object sender, EventArgs e)
		{
			if (PageId == Common.PageId.FindTrunkInput)
			{

				if ( TDSessionManager.Current.FindAMode == FindAMode.TrunkStation)
				{
					TDSessionManager.Current.FindStationPageState.LocationType = FindStationPageState.CurrentLocationType.To;
					TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
					return;
				}

				// Force the page to do !Postback. Otherwise Place list doesn't get populated
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindTrunkInputDefault;

			}

			DestinationLocation = new TDLocation();
			DestinationSearch = new LocationSearch();
			DestinationControlType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);

			
			// Raise Event to indicate to the page the location From has changed
			if (NewLocationTo != null)
				NewLocationTo (sender, e);
		}

		/// <summary>
		/// Event Handler for SelectedLocation event of fromLocationControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFromSelectedLocation (object sender, EventArgs e)
		{
			// Raise event for page
			if (NewLocationFrom != null)
				NewLocationFrom (sender, e);
		}

		/// <summary>
		/// Event Handler for SelectedLocation event of toLocationControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnToSelectedLocation (object sender, EventArgs e)
		{
			// Raise event for page
			if (NewLocationTo != null)
				NewLocationTo (sender, e);
		}
		#endregion

	}


}
