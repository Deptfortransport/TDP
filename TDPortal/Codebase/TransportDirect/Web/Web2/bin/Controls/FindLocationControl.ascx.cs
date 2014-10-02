// *************************************************************************** 
// NAME                 : FindLocationControl.ascx
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 08/07/2004 
// DESCRIPTION			: Control allows user to view or specify the origin or
// destination on all Find a Input pages (except on Find a flight)
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindLocationControl.ascx.cs-arc  $
//
//   Rev 1.14   Dec 21 2011 11:02:24   MTurner
//Removed group locations from Gaz results on searches performed on FindA pages for Car, Cycle and Map.
//Resolution for 5776: Remove Exchange Groups from Gaz in Find a Map, Car and Cycle
//
//   Rev 1.13   Apr 23 2010 16:17:16   mmodi
//Method to populate the scriptable drop down list in FindPlaceDropDownControl
//Resolution for 5521: TD Extra - Drop Down List Change - CCN0575
//
//   Rev 1.12   Mar 01 2010 16:38:26   mmodi
//Set location unspecified for Cycle input page
//Resolution for 5425: Cycle Planner - Back button on ambiguity loses selected search type
//
//   Rev 1.11   Feb 11 2010 14:35:14   rbroddle
//Updated for International Planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Feb 11 2010 08:53:30   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.9   Dec 03 2009 15:59:20   apatel
//input page mapping enhancement related changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Dec 02 2009 11:51:16   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Nov 02 2009 17:49:50   mmodi
//Updated for Find map page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Sep 21 2009 14:57:24   mmodi
//Updated for Environmental Benefits Calculator (EBC)
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//   Rev 1.5   Oct 14 2008 12:05:58   mmodi
//Manual merge for stream5014
//
//   Rev 1.4   Jun 26 2008 14:04:12   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.3.1.0   Jul 28 2008 13:12:38   mmodi
//Added handling for Cycle planner
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   May 01 2008 16:32:44   mmodi
//No change.
//Resolution for 4923: Control alignments: Find a train
//
//   Rev 1.2   Mar 31 2008 13:20:42   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 05 2008 13:00:00   mmodi
//Added method to set the drop down list to the current location
//
//   Rev 1.0   Nov 08 2007 13:14:20   mturner
//Initial revision.
//
//   Rev 1.52   Nov 14 2006 10:03:46   rbroddle
//Merge for stream4220
//
//   Rev 1.51.1.2   Nov 12 2006 13:46:28   tmollart
//Modified logic for visibility of Find Nearest Button.
//Resolution for 4220: Rail Search by Price
//
//   Rev 1.51.1.1   Nov 09 2006 17:37:06   tmollart
//Updated code commenting.
//
//   Rev 1.51.1.0   Nov 07 2006 11:38:40   tmollart
//Updated functionality for Rail Search By Price. Also added new property to add a space between the to/from controls to make up for the difference when the Find Nearest button is not displayed.
//
//   Rev 1.51   Oct 06 2006 14:30:44   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.50.1.1   Aug 14 2006 17:08:58   esevern
//amended refresh tri state control method to hide 'Find on map' button when on FindCarParkInput page 
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.50.1.0   Aug 14 2006 10:32:20   esevern
//findCarParkInput page changes
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.50   May 02 2006 11:58:28   mtillett
//Add test for the AmendMode property so that the editable park and ride drop down list is displayed
//Resolution for 4038: DN058 Park and Ride Phase 2 - Amend from location by map causes drop-down list of P&R locations to disappear
//
//   Rev 1.49   Apr 18 2006 16:04:54   esevern
//Added property to access ParkAndRideSelectionControl
//Resolution for 3803: DN058 Park and Ride Phase 2 - Amend from journey results page does not retain values
//
//   Rev 1.48   Apr 12 2006 16:18:32   esevern
//Added check for ParkAndRideInput page id when refreshing TriStateLocationControl so that correct DataServiceType of FindCarLocationDrop is used.
//Resolution for 3799: DN058 Park and Ride Phase 2 - No Station/airport From location option available
//
//   Rev 1.47   Apr 11 2006 10:53:08   esevern
//added check for FindBusInput page in SetLocationUnspecified, so that correct DataServiceType is used when populating the TriStateLocationControl
//Resolution for 3865: DN093 Find a Bus:  Gazetteer option changes from All stops to CTS when back button is pressed on ambiguity page
//
//   Rev 1.46   Apr 05 2006 15:23:36   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.45   Mar 23 2006 17:58:36   build
//Automatically merged from branch for stream0025
//
//   Rev 1.44.1.3   Mar 16 2006 12:43:02   halkatib
//changes for park and ride phase 2 to get selected location.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.44.1.2   Mar 16 2006 11:58:42   halkatib
//Ammended the logic for displaying the park and ride selection control.
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.44.1.1   Mar 14 2006 09:45:32   halkatib
//Added a check on the tristatecontrolvisible  property to check if the search type of the control is set to Park and ride. 
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.44.1.0   Mar 08 2006 14:32:18   tolomolaiye
//Changes for Park and Ride Phase II
//Resolution for 25: DEL 8.1 Workstream - Park and Ride - Phase 2
//
//   Rev 1.44   Feb 23 2006 19:16:40   build
//Automatically merged from branch for stream3129
//
//   Rev 1.43.1.0   Jan 10 2006 15:24:48   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.43   Nov 04 2005 13:35:18   NMoorhouse
//Manual merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.42   Nov 02 2005 14:45:46   jgeorge
//Changed to alter control type instead of creating new type object.
//Resolution for 2935: Del 7.2: Expanding Train Preferences prevents user planning journey
//
//   Rev 1.41.2.1   Oct 24 2005 21:22:54   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.41.2.0   Oct 04 2005 16:29:38   mtillett
//Update the display of the location controls
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.42   Oct 04 2005 15:57:40   mtillett
//Update location control to meet requirements of TD093
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.41   Mar 01 2005 18:34:28   tmollart
//Removed ResetDropDown call from SetLocationUnspecifiedMethod as this was causing problems in FindTrunkInput.
//Added seperate ResetLocationDropDown method.
//
//   Rev 1.40   Feb 25 2005 10:34:58   tmollart
//Updated Search method and SetHelpText method to take into account FindFare.
//
//   Rev 1.39   Feb 15 2005 16:12:58   tmollart
//Updated SetLocationUnspecified method so that it calls the reset method in the place control.
//
//   Rev 1.38   Jan 17 2005 15:11:56   tmollart
//Del 7 - Modified TriStateControlVisible  property to control if placeControl is visible.
//
//   Rev 1.37   Nov 23 2004 11:39:30   SWillcock
//Added help text for IR 1769
//Resolution for 1769: Station/airport - Help page in 'Find nearest station/airport" page is blank after selecting new location
//
//   Rev 1.36   Nov 02 2004 16:53:14   passuied
//Cleaning the code!
//
//   Rev 1.35   Nov 02 2004 11:40:54   passuied
//fixed various display bugs
//
//   Rev 1.34   Nov 01 2004 18:04:56   passuied
//Changes for FindPlace new functionality
//
//   Rev 1.33   Sep 11 2004 12:55:14   jmorrissey
//IR1567 - fix for More help pages not being set
//
//   Rev 1.32   Sep 09 2004 17:19:58   jmorrissey
////IR1541 - moved state settings from constructor to page load
//
//   Rev 1.31   Sep 08 2004 16:34:52   jmorrissey
//Updated setting of help text for FindCarInput when the page is in AmendMode.
//
//   Rev 1.30   Sep 07 2004 11:21:22   jgeorge
//Enabled Partial Postcode searching where full postcode searching is already available.
//Resolution for 1535: Find a - Partial postcode does not work for any of find a functionalities
//
//   Rev 1.29   Sep 03 2004 15:17:22   RPhilpott
//Move setting of station type out of SetUpResources so that it happens even in a postback.
//
//   Rev 1.28   Sep 02 2004 10:15:08   jmorrissey
//SetUpResources now called once on initial PageLoad and then always on PreRender so that help text and alt tags are set after any click events may have changed the page mode. 
//
//   Rev 1.27   Sep 01 2004 12:04:32   esevern
//now hides help button when find a car on ambiguity and location is valid
//
//   Rev 1.26   Aug 27 2004 17:02:50   RPhilpott
//Correct interaction between control hierarchy and LocationSearchHelper.
//Resolution for 1329: Both train and coach stops can be selected on a Find A Train or Find A Coach page
//
//   Rev 1.25   Aug 26 2004 16:48:52   COwczarek
//Don't display instruction label if currently showing tip to enter different location name
//Resolution for 1421: Find a ambiguity pages (QA)
//
//   Rev 1.24   Aug 26 2004 12:05:06   esevern
//added setting of ambiguous help label text for Find a trunk
//
//   Rev 1.23   Aug 26 2004 10:15:24   passuied
//Adding a extra DataSet in DataServices to have a different Search Type check box list in FindCarInput page
//Resolution for 1441: Find A Car : Add extra station/Airport search type in location selection
//
//   Rev 1.22   Aug 25 2004 16:37:20   esevern
//added setting of alternative help text and more url on find car page when result is ambiguous - fix for IR1299
//
//   Rev 1.21   Aug 13 2004 14:09:04   jmorrissey
//Updated RefreshTristateControl method so that for Findatrain and Findacoach the search type is not set to MainStationAirport  if a valid location has already been found. 
//
//
//   Rev 1.20   Aug 12 2004 12:00:40   jmorrissey
//Updates to help text
//
//   Rev 1.19   Aug 12 2004 10:55:14   esevern
//added alt text
//
//   Rev 1.18   Aug 02 2004 15:37:24   passuied
//working verson of FindCarInput page + changes to some adapters, controls...
//
//   Rev 1.17   Aug 02 2004 15:07:54   jmorrissey
//Updated SetHelpLabel method
//
//   Rev 1.16   Jul 30 2004 14:19:02   esevern
//added check for trunk page on RefreshTristateControl so that when location control populated, map button is shown
//
//   Rev 1.15   Jul 29 2004 16:45:48   jmorrissey
//Moved the SetControlVisibility method to the Pre_Render event rather than Page_Load, so that all events have been handled before the visibility of controls is decided.
//
//   Rev 1.14   Jul 28 2004 16:57:30   jmorrissey
//Added 'MoreUrls' for the HelpLabels
//
//   Rev 1.13   Jul 27 2004 14:03:10   passuied
//FindStation Del6.1 :Finalised version 
//
//   Rev 1.12   Jul 26 2004 20:23:54   passuied
//Changes to implement AmendSeach Functionality. Created and Amend mode in the tristate to enable the display of a valid location inside the locationUnspecified control.
//We send this mode when a one use session key has been set by a click on AmendSearch button.
//Also tweak in toFromLocationControl to display the to and from location/station controls correctly
//
//   Rev 1.11   Jul 23 2004 17:39:32   passuied
//FindStation 6.1. Labels and text updates
//
//   Rev 1.10   Jul 22 2004 18:06:00   passuied
//Integration between pages and move of code to location service
//
//   Rev 1.9   Jul 22 2004 10:13:00   COwczarek
//SetLocationUnspecified now clears location search object
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.8   Jul 21 2004 15:26:36   passuied
//Changes to implement the New Location Button func for all different controls
//
//   Rev 1.7   Jul 21 2004 14:21:52   COwczarek
//Add SetLocationUnspecified method. Remove DisplayMode property
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.6   Jul 21 2004 11:58:52   passuied
//Changes so the Refresh is done in the OnPreRender
//
//   Rev 1.5   Jul 21 2004 10:51:22   passuied
//Re work for integration with FindStation del6.1. Working. Needs work on resources
//
//   Rev 1.4   Jul 16 2004 14:50:46   COwczarek
//Complete implementation of layout
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.3   Jul 15 2004 15:02:04   jmorrissey
//Changed how the help label's text is displayed
//
//   Rev 1.2   Jul 14 2004 11:34:56   jmorrissey
//Coding complete. FxCop run. Needs help label text added to langStrings and integration testing with the various Find a pages.
//
//   Rev 1.1   Jul 13 2004 09:58:18   jmorrissey
//Interim version for use in development of new Find a pages
//
//   Rev 1.0   Jul 08 2004 17:19:20   jmorrissey
//Initial revision.


#region .Net namespaces

using System;
using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Data;
using System.Drawing;
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
using TransportDirect.UserPortal.Web; 
using TransportDirect.Common;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;

#endregion

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Summary description for FindLocationControl.
	/// </summary>	
	public partial  class FindLocationControl : TDUserControl
	{
		#region Controls
		protected TriStateLocationControl2 tristateLocationControl;
		protected FindPlaceDropDownControl placeControl;
		protected ParkAndRideSelectionControl parkAndRideControl;
		#endregion

		#region private variables

		private FindStationPageState stationPageState;
		private FindPageState pageState;

		//private field for holding location type of this control
		private CurrentLocationType locationType;
		//private field for holding station type of this control
		private StationType stationType;
		//private field for holding LocationSearch session information for this control
		private LocationSearch theSearch;
		//private field for holding TDLocation session information for this control
		private TDLocation theLocation;
		//private field for holding LocationSelectControlType session information for this control
		private TDJourneyParameters.LocationSelectControlType locationControlType;		
		//populator to load the strings for the check box list
		private DataServices.DataServices populator;

		private bool acceptPostcodes = true;
		private bool acceptPartialPostcodes = true;

		//langStrings keys 
		private const string RES_FROM = "FindLocationControl.directionLabelFrom";
		private const string RES_TRAVELFROM = "FindLocationControl.directionLabelTravelFrom";
		private const string RES_TO = "FindLocationControl.directionLabelTo";
		private const string RES_TRAVELTO = "FindLocationControl.directionLabelTravelTo";
        private const string RES_VIA = "FindLocationControl.directionLabelVia";
        private const string RES_TRAVELVIA = "FindLocationControl.directionLabelTravelVia";
	
		//array of events raised by this control
		private ArrayList eventsRaised = new ArrayList();


		#endregion

		#region Constructor/Page_Load/Pre_Render

		/// <summary>
		/// default constructor
		/// </summary>
		protected FindLocationControl()
		{
			//instance of DataServices class
			populator = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];	
		}		

		/// <summary>
		/// Page Load method
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//IR1541 - moved state settings from constructor to page load
			stationPageState = TDSessionManager.Current.FindStationPageState;
			pageState = TDSessionManager.Current.FindPageState;				

			if (Visible)
			{	
				//Get resource strings
				SetText();

				//sets the gazetteer mode filter 
				SetStationType();
								
				// Put user code to initialize the page here
				if (!IsPostBack)
				{

					//set up item text for the StationTypesCheckList
					if(this.PageId != PageId.FindCarParkInput)
						LoadStationTypesCheckList();					
		
					//do not refresh the tristate location control
					RefreshTristateControl(false);
				}
				else
				{
					//refresh the tristate location control
					RefreshTristateControl(true);
				}
			}
		}
		
		protected override void OnPreRender(EventArgs e)
		{	
			//set controls visible according to which Find a page this control is on
			SetControlVisibility();

            //Get resource strings
            SetText();	
            			            
			if (Page.IsPostBack)
			{
				// if the Input text is empty it means, we had a new location
				// So, there is no need to check the inputs as we don't want to 
				// update the session variables
				if (theSearch.InputText.Length ==0)
					RefreshTristateControl(false);
				else
					RefreshTristateControl(true);
			}

			base.OnPreRender(e);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Search Method. Use to search a location and call back tristatecontrol Search method
		/// However in FindTrunkInput page, it retrieves the location from FindPlaceDropDownControl
		/// and sends an event to indicate to the page, the location needs to be updated in sessionManager
		/// </summary>
		public void Search()
		{
			if (PageId != Common.PageId.FindTrunkInput 
                && PageId != Common.PageId.FindFareInput 
                && theSearch.SearchType != SearchType.ParkAndRide
                && PageId != PageId.FindInternationalInput)
				tristateLocationControl.Search(true);
			else
			{
				// Only try to get location if location not valid
				if (theLocation.Status != TDLocationStatus.Valid)
				{
					if (theSearch.SearchType == SearchType.ParkAndRide)
					{
						theLocation = parkAndRideControl.Location;
					}
					else
					{
						theLocation = placeControl.Location;
					}

					// Send event to indicate to the page to update journeyParams
					if (theLocation != null)
					{
						if (SelectedLocation != null)
							SelectedLocation(this, EventArgs.Empty);
					}
				}
			}
		}

		/// <summary>
		/// Sets the control back into the default state that allows a location to be entered by user.
		/// Now updated to reset the drop down list of places back to its initial settting.
		/// </summary>
		public void SetLocationUnspecified() 
		{
			theLocation.Status = TDLocationStatus.Unspecified;
			locationControlType.Type = TDJourneyParameters.ControlType.Default;
			theSearch.ClearSearch();

            // Ensure correct list of location search types are loaded
			if(PageId == PageId.FindBusInput) 
			{
				tristateLocationControl.Populate(DataServiceType.FromToDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true,  acceptPostcodes, acceptPartialPostcodes, stationType, false);
			}
            else if (PageId == PageId.FindCycleInput)
            {
                tristateLocationControl.Populate(DataServiceType.FindCycleLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true, acceptPostcodes, acceptPartialPostcodes, stationType, false);
            }
            else
            {
                tristateLocationControl.Populate(DataServiceType.FindStationLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true, acceptPostcodes, acceptPartialPostcodes, stationType, false);
            }
		}

        /// <summary>
        /// Sets the place control drop down to amendable with the selected location displayed
        /// </summary>
        public void SetLocationDropDown()
        {
            placeControl.SetLocationDropDown();
        }

		//Resets the place control drop down back to the first entry. This is
		//typically the help text advising a user on how to select a location.
		public void ResetPlaceControlDropDown()
		{
			placeControl.ResetLocationDropDown();
		}

        /// <summary>
        /// Method which populates the scriptable dropdown control values. These values indicate the 
        /// associated "other location" dropdown control to update using client javascript.
        /// e.g. Used on Find International Input to dynamically update the valid from - to route combinations
        /// </summary>
        /// <param name="targetControlName">The target dropdown control to update</param>
        /// <param name="updateTargetControl">Whether the target control should be updated</param>
        /// <param name="pageId">PageId determines which javascript to attach to the dropdown list</param>
        /// <param name="restrictLocations">ArrayList string of ids in the dropdown to restrict/not show</param>
        public void SetDropDownScriptable(string targetControlName, bool updateTargetControl, PageId pageId, ArrayList restrictLocations)
        {
            placeControl.TargetControlName = targetControlName;
            placeControl.UpdateTargetControl = updateTargetControl;
            placeControl.DropDownScriptablePageId = pageId;
            placeControl.Restrict(restrictLocations);
            placeControl.UseScriptableList = true;
            placeControl.UseJavaScript = true;
        }

		#endregion

		#region Control Appearance 
		
		/// <summary>
		/// set controls visible according to which Find a page this control is on
		/// </summary>
		private void SetControlVisibility()
		{				
			if(theSearch.SearchType == SearchType.ParkAndRide)
			{
				if (TriStateControlVisible && !AmendMode)
				{
					tristateLocationControl.Visible = true;
					placeControl.Visible = false;
					parkAndRideControl.Visible = false;
				}
				else
				{
					tristateLocationControl.Visible = false;
					placeControl.Visible = false;
					parkAndRideControl.Visible = true;
				}
			}
			else
			{
				if (TriStateControlVisible)
				{
					tristateLocationControl.Visible = true;
					placeControl.Visible = false;
					parkAndRideControl.Visible = false;
				}
				else
				{
					parkAndRideControl.Visible = false;
					placeControl.Visible = true;
					tristateLocationControl.Visible = false;
				}
			}

			//decide whether to show the findNearestButton		
			findNearestButton.Visible = FindNearestButtonVisible;	
			
			//decide whether to show the stationTypesCheckList					
			stationTypeLabel.Visible = StationTypesCheckListVisible;			
			stationTypesCheckList.Visible = StationTypesCheckListVisible;

			// decide wether to show direction label
			directionLabel.Visible = DirectionLabelVisible;
		}

		/// <summary>
		///  sets various text according to which page contains this control
		/// </summary>
		private void SetText()
		{

			//sets the directionLabel text
			SetDirectionLabelText();

            // screen reader text
            //setting the tristatelocation control text for screen reader

            string tristatescreenreadertext = string.Empty;
            switch (locationType)
            {

                case CurrentLocationType.From:

                    tristatescreenreadertext = Global.tdResourceManager.GetString("originSelect.labelSRLocation", TDCultureInfo.CurrentUICulture);
                    break;

                case CurrentLocationType.To:

                    tristatescreenreadertext = Global.tdResourceManager.GetString("destinationSelect.labelSRLocation", TDCultureInfo.CurrentUICulture);
                    break;

                case CurrentLocationType.PrivateVia:

                    tristatescreenreadertext = Global.tdResourceManager.GetString("viaSelect.labelSRLocation", TDCultureInfo.CurrentUICulture);
                    break;

                default:

                    tristatescreenreadertext = GetResource("FindStationInput.labelSRLocation");
                    break;
            }
            if (PageId != Common.PageId.FindStationInput && PageId != Common.PageId.FindCarParkInput)
            {
                tristateLocationControl.LocationUnspecifiedControl.TypeInstruction.Text = tristatescreenreadertext;
            }
            else
            {
                tristateLocationControl.LocationUnspecifiedControl.TypeInstruction.Text = GetResource("FindStationInput.labelSRLocation");
            }
           
			//evaluate the containing page
			switch (this.PageId)
			{

					//Find a train input page
				case TransportDirect.Common.PageId.FindTrainInput :
				case TransportDirect.Common.PageId.FindTrainCostInput:

					stationTypeLabel.Text = String.Empty;
					findNearestButton.Text = GetResource("FindLocationControl.findNearestTrainButtonText");
					break;

					//Find a coach input page
				case TransportDirect.Common.PageId.FindCoachInput :

					stationTypeLabel.Text = String.Empty;
					findNearestButton.Text = GetResource("FindLocationControl.findNearestCoachButtonText");

					break;	

					//Find a car journey input page
				case TransportDirect.Common.PageId.FindCarInput :

					stationTypeLabel.Text = String.Empty;
					break;

					//Find a station input page
				case TransportDirect.Common.PageId.FindStationInput :

					stationTypeLabel.Text = Global.tdResourceManager.GetString("FindLocationControl.stationTypeLabel",TDCultureInfo.CurrentUICulture);
					break;

					//Find a trunk road input page
				case TransportDirect.Common.PageId.FindTrunkInput :

					stationTypeLabel.Text = String.Empty;
					break;				

                case TransportDirect.Common.PageId.FindEBCInput:
                    stationTypeLabel.Text = String.Empty;
                    break;

					//default, set text to blank
				default :

					stationTypeLabel.Text = String.Empty;
					break;
			}

		}
		
		/// <summary>
		///  sets gazetteer mode filter and postcode flags, depending on id of hosting page
		/// </summary>
		private void SetStationType()
		{
			switch (this.PageId)
			{
				case TransportDirect.Common.PageId.FindTrainInput :
				case TransportDirect.Common.PageId.FindTrainCostInput:
					acceptPostcodes = false;
					acceptPartialPostcodes = false;
					stationType = StationType.Rail;
					break;

				case TransportDirect.Common.PageId.FindCoachInput :
					acceptPostcodes = false;
					acceptPartialPostcodes = false;
					stationType = StationType.Coach;
					break;

                case TransportDirect.Common.PageId.FindEBCInput:
				case TransportDirect.Common.PageId.FindStationInput :
				case TransportDirect.Common.PageId.FindTrunkInput :
                case TransportDirect.Common.PageId.FindInternationalInput:
					acceptPostcodes = true;
					acceptPartialPostcodes = true;
					stationType = StationType.Undetermined;
					break;

                case TransportDirect.Common.PageId.FindCarInput:
                case TransportDirect.Common.PageId.FindCarParkInput:
                case TransportDirect.Common.PageId.FindCycleInput:
                case TransportDirect.Common.PageId.FindMapInput:
                    acceptPostcodes = true;
                    acceptPartialPostcodes = true;
                    stationType = StationType.UndeterminedNoGroup;
                    break;

				default :
					acceptPostcodes = true;
					acceptPartialPostcodes = true;
					stationType = StationType.Undetermined;
					break;
			}
		}

		/// <summary>
		/// sets the direction label text according to the LocationType property of this control
		/// </summary>
		private void SetDirectionLabelText()
		{
			string resFrom = string.Empty;
			string resTo = string.Empty;
            string resVia = string.Empty;

			if (PageId == Common.PageId.FindStationInput
				|| PageId == Common.PageId.FindCarParkInput)
			{
				resFrom = RES_TRAVELFROM;
				resTo = RES_TRAVELTO;
                resVia = RES_VIA;
			}
			else
			{
				resFrom = RES_FROM;
				resTo = RES_TO;
                resVia = RES_VIA;
			}

			//evaluate the location type
			switch (locationType)
			{

				case CurrentLocationType.None:

					directionLabel.Text = String.Empty;
					break;

				case CurrentLocationType.From:

					directionLabel.Text = Global.tdResourceManager.GetString(resFrom, TDCultureInfo.CurrentUICulture);
					break;

				case CurrentLocationType.To:

					directionLabel.Text = Global.tdResourceManager.GetString(resTo, TDCultureInfo.CurrentUICulture);
					break;

                case CurrentLocationType.PrivateVia:

                    directionLabel.Text = Global.tdResourceManager.GetString(resVia, TDCultureInfo.CurrentUICulture);
                    break;

				default:

					directionLabel.Text = String.Empty;
					break;
			}	

		}	
		
		/// <summary>
		/// 
		/// </summary>
		private void LoadStationTypesCheckList()
		{
			// populate lists
			populator.LoadListControl(DataServiceType.StationTypesCheck, stationTypesCheckList);
		}

	
		/// <summary>
		/// Refresh the tristate control
		/// </summary>
		/// <param name="checkInput">indicates if location control checks if input has changed/not</param>
		public void RefreshTristateControl(bool checkInput)
		{

			//evaluate the containing page
			switch (this.PageId)
			{

				case TransportDirect.Common.PageId.FindTrainInput :
				case TransportDirect.Common.PageId.FindTrainCostInput:

					//set the search type to MainStationAirport so that a main station gazetteer search is invoked
					//but only if a valid location is not being displayed already
					if (theLocation.Status != TDLocationStatus.Valid)
					{
						theSearch.SearchType = SearchType.MainStationAirport;
					}

					tristateLocationControl.Populate(DataServiceType.FindStationLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true, acceptPostcodes, acceptPartialPostcodes, stationType, checkInput);
					break;

				case TransportDirect.Common.PageId.FindCoachInput :
					//set the search type to MainStationAirport so that a main station gazetteer search is invoked
					//but only if a valid location is not being displayed already
					if (theLocation.Status != TDLocationStatus.Valid)
					{
						theSearch.SearchType = SearchType.MainStationAirport;
					}

					tristateLocationControl.Populate(DataServiceType.FindStationLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true, acceptPostcodes, acceptPartialPostcodes, stationType, checkInput);
					break;

				case TransportDirect.Common.PageId.FindTrunkInput :
					// display 'Map' image button in locationselectcontrol
					tristateLocationControl.Populate(DataServiceType.FindStationLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, false,  acceptPostcodes, acceptPartialPostcodes, stationType, checkInput);
					break;

				case TransportDirect.Common.PageId.FindCarParkInput :
					// hide 'Map' image button in locationselectcontrol
					tristateLocationControl.Populate(DataServiceType.FindCarLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true,  acceptPostcodes, acceptPartialPostcodes, stationType, checkInput);
					break;

				case TransportDirect.Common.PageId.FindCarInput :
				case TransportDirect.Common.PageId.ParkAndRideInput :
					// display 'Map' image button in locationselectcontrol
                    tristateLocationControl.Populate(DataServiceType.FindCarLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, false, acceptPostcodes, acceptPartialPostcodes, stationType, checkInput && theSearch.SearchType != SearchType.Map);
					break;

				case TransportDirect.Common.PageId.FindBusInput :
					// display 'Map' image button in locationselectcontrol
                    tristateLocationControl.Populate(DataServiceType.FromToDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, false, acceptPostcodes, acceptPartialPostcodes, stationType, checkInput && theSearch.SearchType != SearchType.Map);
					break;

                case TransportDirect.Common.PageId.FindCycleInput:
                    // display 'Map' button in locationselectcontrol
                    tristateLocationControl.Populate(DataServiceType.FindCycleLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, false, acceptPostcodes, acceptPartialPostcodes, stationType, checkInput && theSearch.SearchType != SearchType.Map);
                    break;

                case TransportDirect.Common.PageId.FindEBCInput:
                    // hide 'Map' button in locationselectcontrol
                    tristateLocationControl.Populate(DataServiceType.FindEBCLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true, acceptPostcodes, acceptPartialPostcodes, stationType, checkInput);
                    break;

                case TransportDirect.Common.PageId.FindMapInput:
                    // hide 'Map' button in locationselectcontrol
                    tristateLocationControl.Populate(DataServiceType.LocationTypeDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true, acceptPostcodes, acceptPartialPostcodes, stationType, checkInput);
                    break;
                
				default:
					tristateLocationControl.Populate(DataServiceType.FindStationLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true,  acceptPostcodes, acceptPartialPostcodes, stationType, checkInput);
					break;
			}			
		}

		#endregion

		#region Control status
		/// <summary>
		/// returns an array of the station types selected on the stationTypesCheckList
		/// </summary>
		/// <returns></returns>
		public StationType[] StationTypesSelected
		{
			get
			{

				//array to return
				ArrayList selectedTypesArray = new ArrayList();			
			
				bool allUnchecked = true;

				//add selected items to the array
				for (int i =  0; i < stationTypesCheckList.Items.Count; i++)
				{				
					if (stationTypesCheckList.Items[i].Selected)
					{
						allUnchecked = false;
						try
						{
							selectedTypesArray.Add(
								(StationType)Enum.Parse(typeof(StationType), stationTypesCheckList.Items[i].Value, true));
						}
						catch (ArgumentException)
						{

							Logger.Write(new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, "Error in method StationTypeSelected(). Unable to parse the values of the Checkbox list items."));
							throw new TDException("Error in method StationTypeSelected(). Unable to parse the values of the Checkbox list items.",
								true, TDExceptionIdentifier.BTCBadStationTypeEnumParsing);
						}
					}
				}

				// if all checkboxes are unchecked. Recheck Everything
				if (allUnchecked)
				{
					return new StationType[]{StationType.Airport, StationType.Coach, StationType.Rail};
				}

				//return the array
				return (StationType[])selectedTypesArray.ToArray(typeof(StationType));
			}

			set
			{
				// first uncheck all
				foreach (ListItem item in stationTypesCheckList.Items)
				{
					item.Selected = false;
				}

				StationType[] types = (StationType[])value;

				// go through all stationtypes in the value and select the ones in the list
				foreach(StationType type in types)
				{
					foreach( ListItem item in stationTypesCheckList.Items)
					{
						if (type.ToString() == item.Value)
						{
							item.Selected = true;
							break;
						}

					}
				}

			}
		}

		#endregion

		#region Properties


		/// <summary>
		/// Read only property returning the ParkAndRideSelectionControl
		/// </summary>
		public ParkAndRideSelectionControl ParkAndRideSelection 
		{
			get 
			{
				return parkAndRideControl;
			}
		}

		/// <summary>
		/// Read only propety that decides whether the tristate control should be visible or not
		/// </summary>
		public bool TriStateControlVisible
		{
			get
			{
				// TriState shouldn't be visible when in trunk Input page or when the search type is Park and ride
				// but it should be visible when location is valid!
				return ((this.PageId != PageId.FindTrunkInput && this.PageId != PageId.FindFareInput && theSearch.SearchType != SearchType.ParkAndRide && this.PageId != PageId.FindInternationalInput)
					|| theLocation.Status == TDLocationStatus.Valid);
			}
		}

		/// <summary>
		/// Read Only property that controls whether the findNearestButton is visible 
		/// </summary>
		public bool FindNearestButtonVisible
		{
			get
			{
				return ( 
					this.PageId == TransportDirect.Common.PageId.FindTrainInput || 
					this.PageId == TransportDirect.Common.PageId.FindCoachInput) && (theLocation.Status != TDLocationStatus.Valid);
			}
		}

		/// <summary>
		/// Read Only property that controls whether the findNearestButton is visible 
		/// </summary>
		public bool InstructionLabelVisible
		{
			get
			{
				return (locationControlType.Type != TDJourneyParameters.ControlType.NoMatch);
			}
			
		}

		/// <summary>
		/// Sets the Amend mode in the tristate control. See triStateControl for explanations.
		/// </summary>
		public bool AmendMode
		{
			get
			{
				return tristateLocationControl.AmendMode;
			}
			set
			{
				tristateLocationControl.AmendMode = value;
			}
		}
		/// <summary>
		/// Read Only property that controls whether the stationTypesCheckList is visible 
		/// </summary>
		public bool StationTypesCheckListVisible
		{
			get
			{
				return stationTypesCheckList.Visible;
			}

			set
			{
				stationTypesCheckList.Visible = value;
				stationTypeLabel.Visible = value;
			}
			
		}

		/// <summary>
		/// Read only property that controls whether the direction label is visible.
		/// </summary>
		public bool DirectionLabelVisible
		{
			get
			{
				return directionLabel.Visible;
			}

			set
			{
				directionLabel.Visible = value;
			}
			
		}


		/// <summary>
		/// property holds the location type
		/// this determines the label used for the direction label e.g. To or From
		/// </summary>
		public CurrentLocationType LocationType
		{
			get
			{
				return locationType;
			}
			set
			{
				locationType = value;
				placeControl.LocationType = value;
			}
		}

		/// <summary>
		/// property holds the LocationSearch object used to populate the tristateLocationControl		
		/// </summary>
		public LocationSearch TheSearch
		{
			get
			{
				return theSearch;
			}
			set
			{
				theSearch = value;	
				placeControl.Search = value;
			}
		}

		/// <summary>
		/// property holds the TDLocation object used to populate the tristateLocationControl	
		/// </summary>
		public TDLocation TheLocation
		{
			get
			{
				return theLocation;
			}
			set
			{
				theLocation = value;
				placeControl.Location = value;
			}
		}

		/// <summary>
		/// property holds the LocationSelectControlType object used to populate the tristateLocationControl	
		/// </summary>
		public TDJourneyParameters.LocationSelectControlType LocationControlType			
		{
			get
			{
				return locationControlType;
			}
			set
			{
				locationControlType = value;				
			}
		}

		/// <summary>
		/// Read only property returning the TriStateLocationControl2
		/// </summary>
		public TriStateLocationControl2 TriLocationControl 
		{
			get 
			{
				return tristateLocationControl;
			}
		}

		/// <summary>
		/// Read omly. Makes space table row visible or not.
		/// </summary>
		public bool SpaceTableVisible
		{
			set { spaceTable.Visible = value; }
		}

        /// <summary>
        /// Read only. Returns the full id of the scriptable dropdown list, as it will be written out in the
        /// client HTML.
        /// To be used when determining the control id to pass into the method SetDropDownScriptable(..) when 
        /// the scriptable drop down is used, e.g. on the Find International Input page
        /// </summary>
        public string DropDownScriptableClientID
        {
            get { return placeControl.DropDownScriptableClientID; }
        }

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//

			tristateLocationControl.NewLocation += new EventHandler(OnNewLocation);

			ExtraEventWireUp();
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			findNearestButton.Click += new EventHandler(this.OnFindNearestClick);
		}
		#endregion

		#region Public Events

		public event EventHandler NewLocation;
		public event EventHandler SelectedLocation;

		//keys
		private static readonly object FindNearestClickEventKey = new object();		

		/// <summary>
		/// Event that will be raised when the findNearestButton button is clicked.
		/// Event is then dealt with in the containing page
		/// </summary>
		public event EventHandler FindNearestClick
		{
			add { this.Events.AddHandler(FindNearestClickEventKey, value); }
			remove { this.Events.RemoveHandler(FindNearestClickEventKey, value); }
		}

		#endregion

		#region Event Handlers
		/// <summary>
		/// Handle click event for the findNearestButton
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnFindNearestClick(object sender, EventArgs e)
		{

			EventHandler theDelegate = (EventHandler)this.Events[FindNearestClickEventKey];
			if (theDelegate != null)
				theDelegate(this, e);
			eventsRaised.Add(FindNearestClickEventKey);
		
		}

		private void OnNewLocation (object sender, EventArgs e)
		{
			if (NewLocation != null)
				NewLocation(sender, e);
		}

		#endregion
	}
}