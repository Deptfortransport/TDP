// *********************************************** 
// NAME                 : FindCarInput.aspx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/07/2004 
// DESCRIPTION  : Input Page for FindA Car
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindCarInput.aspx.cs-arc  $ 
//
//   Rev 1.26   Mar 22 2013 10:49:06   DLane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.25   Sep 04 2012 11:16:58   mmodi
//Updated to handle landing page auto plan with the new auto-suggest location control
//Resolution for 5837: Gaz - Page landing autoplan links fail on Cycle input page
//
//   Rev 1.24   Aug 28 2012 10:21:30   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.23   Jul 28 2011 16:19:44   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.22   Mar 14 2011 15:12:10   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.21   Oct 28 2010 14:47:22   RBroddle
//Removed explicit wire up to Page_PreRender & Page_Init as AutoEventWireUp=true for this control so they were firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.20   May 13 2010 13:05:14   mmodi
//Added code call to clear the printable map session information 
//Resolution for 5535: Printable maps session logic improvement
//
//   Rev 1.19   May 11 2010 09:30:24   apatel
//Resolve the issue with locations not auto populated when coming from drive to car park
//Resolution for 5529: Issue with plan a journey with find nearest
//
//   Rev 1.18   Mar 26 2010 11:55:16   MTurner
//Added code to clear journey results from session if you are coming directly from another planner.
//
//Resolution for 5481: Session issue when going from FAT to D2D using the left hand menu
//
//   Rev 1.17   Jan 29 2010 14:45:24   mmodi
//Updated to reset CycleResult to correct error when planning journey after coming directly from Cycle journey details
//Resolution for 5388: Cycle Planner - Server error when planning Door to door after a cycle journey
//
//   Rev 1.16   Jan 19 2010 13:20:58   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.15   Dec 14 2009 11:06:12   apatel
//stop the map showing when new location button click after amend
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.14   Dec 09 2009 11:33:54   mmodi
//When Clear button is clicked, reset the map
//
//   Rev 1.13   Dec 03 2009 16:00:56   apatel
//input page mapping enhancement related changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.12   Dec 02 2009 11:51:18   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.11   Nov 30 2009 10:19:40   apatel
//mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.10   Nov 30 2009 09:58:08   apatel
//input page find on map workflow changed
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 18 2009 11:42:10   apatel
//Added oneusekey for findonmap button click to move on to findmapinput page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Nov 18 2009 11:20:38   apatel
//Updated for Journey input planner mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Nov 10 2009 11:30:06   apatel
//Find Input pages mapping enhancement changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Jan 30 2009 10:44:10   apatel
//Search Engine Optimasation changes - CCN 624
//Resolution for 5229: Search Engin Optimisation Changes -  CCN624
//
//   Rev 1.5   May 02 2008 14:34:04   mmodi
//Formatting improved.
//Resolution for 4929: Control alignments: Find car
//
//   Rev 1.4   May 01 2008 17:24:12   mmodi
//Updated to display session timeout error
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.3   Apr 08 2008 15:55:50   scraddock
//Advanced Text Not Hidden
//Resolution for 4847: Advanced Text Not Hidden
//
// rev devfactory 8 apr sbarker
// Advanced text hidden when advanced pressed, or in ambiguity mode
//
//   Rev 1.2   Mar 31 2008 13:24:20   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Jan 30 2008 10:37:00 dsawe
//  Modified to set PageOptionsControls inside the blue boxes. New PageOptionsControl added to page which
//  will be display/hide when hide/advance button will be clicked.
//
//DEVFACTORY FEB 21 2008 sbarker
//Page icon added
//
//DEVFACTORY   JAN 08 2008 12:30:00   psheldrake
//Changes to conform to CCN426 : Car Park Usability Updates
//
//   Rev 1.0   Nov 08 2007 13:29:20   mturner
//Initial revision.
//
//   Rev 1.66   Sep 03 2007 15:24:44   pscott
//CCN407 IR 4490
//title and key word changes for Google natural search
//
//   Rev 1.65   Jun 05 2007 15:14:28   mmodi
//Added code for the PreferencesOptionsControl Click event
//Resolution for 4409: Minor Portal Changes
//
//   Rev 1.64   Apr 25 2007 13:41:40   mmodi
//Reset alternate Return locations to prevent scenario where both From and To are car parks
//Resolution for 4394: Car Park: Using Browser back button causes journey planning issue
//
//   Rev 1.63   Oct 06 2006 16:37:10   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.62.1.2   Sep 28 2006 11:29:24   mmodi
//Updated to prevent car park location being editable when Amend selected
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4207: Car Parking: Car park location editable when Amend selected
//
//   Rev 1.62.1.1   Sep 21 2006 16:41:38   mmodi
//Updated to handle different different Car park entrance and exit coordinates
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4182: Car Parking: Entrance and Exit coordinates not used in journey planning
//
//   Rev 1.62.1.0   Sep 05 2006 11:28:18   mmodi
//Added processing for CarParks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.62   May 03 2006 14:34:14   mtillett
//Reset the Park and Ride scheme property of the destination location object to null for find a car
//Resolution for 4063: DN058 Park and Ride Phase 2:  Error displayed on Find a Car Maps results page after switching from Park and Ride planner
//
//   Rev 1.61   May 03 2006 11:01:30   mtillett
//Add comments to fix
//Resolution for 4039: DN058 Park and Ride Phase 2 - Find a car route has P&R sites drop-down box in To field
//
//   Rev 1.60   May 03 2006 09:24:42   mtillett
//Code added to Park and Ride and Find A Car input pages to reset the destiniation information when the SearchType is incorrect on load of page and back button. 
//Resolution for 4039: DN058 Park and Ride Phase 2 - Find a car route has P&R sites drop-down box in To field
//
//   Rev 1.59   Apr 28 2006 12:30:56   asinclair
//Removed the ClearRoadEntered method as it is now on the journeyoptionscontrol
//Resolution for 3974: DN068 Extend: 'Clear page' does not clear all values on Extend input page
//
//   Rev 1.58   Apr 25 2006 11:28:12   COwczarek
//Change logic in submitRequest method to set ambiguity mode
//to false if auto plan landing request
//Resolution for 3943: DN077 Landing Page: Dates not displayed on Ambiguity screen
//
//   Rev 1.57   Apr 24 2006 10:31:46   pscott
//IR3927/3510
//
//   Rev 1.56   Apr 13 2006 11:05:02   COwczarek
//Call ResetLandingPageSessionParameters in Unload event
//handler rather than PreRender event handler. This ensures
//parameters are reset even if redirect occurs due to autoplan
//being set.
//Resolution for 3902: Landing Page: Using Find A Car with autoplan set then clicking amend throws exception
//
//   Rev 1.55   Apr 12 2006 12:42:54   COwczarek
//Rearrange logic in Page_PreRender so that UpdateDateControl is not called on initial load if request is a landing page request.
//Resolution for 3773: Landing Page: Return date error when no return date or time specified
//
//   Rev 1.54   Mar 30 2006 10:37:40   halkatib
//Made page check if coming from landing page before initialising the journey paramters. Initialisation is not required on the page since the landing page does this already. When this happens twice in a landing page call the journeyparametes set by the landing are changed.
//
//   Rev 1.53   Mar 27 2006 10:23:40   kjosling
//Merged stream 0023 - Journey Results
//
//   Rev 1.52   Mar 22 2006 17:30:02   halkatib
//Changes due to Merge of stream3152 Landing Page phase 3
//
//   Rev 1.51   Mar 10 2006 12:41:18   pscott
//SCR3510
//Close Calendar Control when going to Ambiguity page
//
//   Rev 1.50   Feb 23 2006 19:12:32   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.49   Feb 10 2006 11:09:46   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.48   Jan 12 2006 18:16:08   RPhilpott
//Reset TDItineraryManager to default (mode "None") in page initialisation to allow for case where we are coming from VisitPlanner.
//Resolution for 3450: DEL 8: Server error when returning to Quickplanner results from Visit Planner input
//
//   Rev 1.47   Nov 25 2005 13:47:04   NMoorhouse
//Moved the setting on HelpUrl into the Page_Load (as they were being reset to null on post back)
//Resolution for 2990: UEE Post Build Enhancement: Add Help Pages to Input Pages
//
//   Rev 1.46   Nov 17 2005 17:11:14   NMoorhouse
//Update Input Page Help Page URLs
//Resolution for 2990: UEE Post Build Enhancement: Add Help Pages to Input Pages
//
//   Rev 1.45   Nov 10 2005 14:32:40   NMoorhouse
//TD093 UEE Input Pages - Soft Content
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.44   Nov 09 2005 17:20:04   mguney
//Merge for stream 2818.
//
//   Rev 1.43   Nov 03 2005 18:00:06   NMoorhouse
//Manual Merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.42   Nov 02 2005 14:47:22   jgeorge
//Undid previous change, as this was not complete and caused an additional bug. Fix for the original IR was made in FindLocationControl and FindViaLocationControl.
//Resolution for 2935: Del 7.2: Expanding Train Preferences prevents user planning journey
//
//   Rev 1.41   Oct 11 2005 17:04:36   kjosling
//Fixed problem switching to preferences. 
//Resolution for 2842: DN79 UEE Stage 1:  "No Options found" message displayed when opening the Journey Details section after pressing Back button on ambiguity page
//
//   Rev 1.40   Oct 10 2005 14:05:32   schand
//Fix for IR2849. Removed lable 'labelLeftHeading' from FindCarInput.aspx and FindCarInput.aspx.cs page.
//Resolution for 2849: Del 7.2 - Find a car - unnecessary header above suggestion links
//
//   Rev 1.39.1.2   Oct 25 2005 20:07:58   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.39.1.1   Oct 12 2005 12:50:12   mtillett
//Updates to advanced options control to remove help and move hide button to single place in FindPageOptionsControl
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.39.1.0   Oct 04 2005 16:36:12   mtillett
//Update the page usage of the FindToFromLocationControl and HelpButtonControl. Removed Back button
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.39   Sep 29 2005 10:42:52   schand
//Merged stream 2673 back into trunk
//Resolution for 2673: DEL 8 stream: 24 hour help button removal
//
//   Rev 1.38   Sep 27 2005 11:10:58   asinclair
//Merge for 2596
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.37.1.2   Aug 25 2005 16:29:44   NMoorhouse
//Adding of suggestion link control
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.37.1.1   Aug 18 2005 11:20:42   rgeraghty
//Changes made for FxCop
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.37.1.0   Aug 12 2005 13:29:36   NMoorhouse
//DN058 Park And Ride - Added link to Park And Ride page
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.37   May 13 2005 14:54:52   passuied
//Fix so when clicking submit on page and in amend mode, the amend mode is reset to false and all locations are refreshed, in case there is still ambiguity on the page
//Resolution for 2468: Location is described as not found even though the ambiguity page has displayed a resolution for the location
//
//   Rev 1.36   May 10 2005 17:52:10   Ralavi
//Using a new class for validating fuel consumption and fuel cost
//
//   Rev 1.35   May 10 2005 15:13:54   rgeraghty
//Updated preferencesControl_Clear to set the visibility of panelDivHider on the preferences control. Removed code which was always setting the preferences to be visible.
//Resolution for 2469: Find A Car : new bug when clicking Clear Page button
//
//   Rev 1.34   May 04 2005 15:22:40   Ralavi
//Replacing two error message for avoid/use roads by one error message.
//
//   Rev 1.33   Apr 28 2005 17:47:16   rgeraghty
//Update to preferencescontrol_back to ensure the panelDivHider on the car preferences control is visible
//Resolution for 2222: Door To Door Location Ambiguity Page Blue Box Display Formatting
//
//   Rev 1.32   Apr 28 2005 16:46:34   Ralavi
//Remove passing values from journeyParameters.AvoidRoadsList and UseRoadsList in PreRender section
//
//   Rev 1.31   Apr 27 2005 10:20:34   COwczarek
//Fix compiler warnings
//
//   Rev 1.30   Apr 26 2005 09:18:44   Ralavi
//Passing values from journey parameters to the control in prerender section to fix problems with avoidRoad and useRoad
//
//   Rev 1.29   Apr 20 2005 13:35:34   Ralavi
//Fixing fuel cost and fuel consumption IRs 2115, 2116, 2009 and 2006 
//
//   Rev 1.28   Apr 15 2005 12:48:06   COwczarek
//Changes to allow PT cost based searches to work with extend journey functionality.
//Resolution for 2079: PT Extend journey does not work with PT cost based searches
//
//   Rev 1.27   Apr 13 2005 12:01:18   Ralavi
//Added code to validate fuel consumption and fuel cost
//
//   Rev 1.26   Apr 05 2005 10:06:18   Ralavi
//To ensure fuelConsumption is converted and passed correctly
//
//   Rev 1.25   Apr 01 2005 13:32:16   Ralavi
//Changes for correct fuel consumption conversion
//
//   Rev 1.24   Mar 23 2005 17:00:50   RAlavi
//Passed the values for fuel consumption and fuel cost to Journey parameter when in default state.
//
//   Rev 1.23   Mar 23 2005 15:51:34   rscott
//Added help text reference for carpreferences control
//
//   Rev 1.22   Mar 23 2005 11:19:42   RAlavi
//Modifications for car costing
//
//   Rev 1.21   Mar 21 2005 09:17:38   rscott
//NoWrap added to header div to prevent TDOnTheMove tab image button from wrapping.
//
//   Rev 1.20   Mar 16 2005 11:33:10   RAlavi
//Passing fuel consumption and fuel cost values
//
//   Rev 1.19   Mar 08 2005 16:25:46   bflenk
//TimeOut functionality implemented in TDPage.cs, removed from this file - IR1720
//
//   Rev 1.18   Mar 08 2005 15:44:18   RAlavi
//Car Costing changes
//
//   Rev 1.17   Mar 04 2005 16:33:14   RAlavi
//Modifying problem with clear button for car costing
//
//   Rev 1.16   Mar 02 2005 15:22:56   RAlavi
//Added code for ambiguity pages
//
//   Rev 1.15   Feb 23 2005 17:26:54   RAlavi
//After modifications relating to car costing
//
//   Rev 1.13   Jan 28 2005 18:46:02   ralavi
//Updated for car costing
//
//   Rev 1.12   Nov 19 2004 11:19:30   asinclair
//Fix for IR1720 Vantive 3487114
//
//   Rev 1.11   Oct 15 2004 12:39:04   jgeorge
//Changed to take account of new JourneyPlanStateData and changes to existing JourneyPlanControlData.
//Resolution for 1713: Results are incorrect after planning several journeys
//
//   Rev 1.10   Sep 08 2004 16:38:10   jmorrissey
//IR 1417 - add check in Page Load to see if page is in amend mode and, if true, set the location controls to editable
//
//   Rev 1.9   Aug 27 2004 10:43:20   passuied
//added back button at top of page
//
//   Rev 1.8   Aug 24 2004 11:32:38   passuied
//Call UpdateDateControl only on Postback
//
//   Rev 1.7   Aug 19 2004 15:39:20   passuied
//Added new type in MapMode called FromFindAInput, used by FindTrunkInput and FindCarInput when calling the map. The map page and controls behave exactly as with enum MapMode.FromJourneyInput except that it shows the FindA header in the first case and the JourneyPlanner one in the latter. 
//Also checks if MapMode.FromFindAInput and if not wipe the FindAMode (CreateInstance(FindAMode.None)).
//
//   Rev 1.6   Aug 19 2004 13:26:50   COwczarek
//Reset Find A session data when page is loaded for the first 
//time and there is an itinerary or an extension is in progress.
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.5   Aug 17 2004 13:46:36   esevern
//added initialisation of date control to initial request setup
//
//   Rev 1.4   Aug 17 2004 09:19:54   COwczarek
//Prior to commencing journey planning, reset the itinerary if one
//exists since it is not currently possible to extend using a Find A
//function. At this point save the current Find A mode and journey
//parameters to record what was used to plan the journey.
//Resolution for 1306: Find a Train extends a journey incorrectly.
//
//   Rev 1.3   Aug 04 2004 14:25:56   passuied
//minor appearance fixes
//
//   Rev 1.2   Aug 04 2004 11:29:06   passuied
//changes for New Search implementation. Use of the GetOneKey() method
//
//   Rev 1.1   Aug 02 2004 15:37:28   passuied
//working verson of FindCarInput page + changes to some adapters, controls...
//
//   Rev 1.0   Jul 29 2004 15:10:08   passuied
//Initial Revision

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;


using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Web.Support;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.SuggestionLinkService;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using System.Collections.Generic;
using TransportDirect.CommonWeb.Helpers;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Input Page for FindA Car
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class FindCarInput : TDPage
    {
        #region Private members

        protected System.Web.UI.WebControls.Panel panelBackTop;
		protected FindLeaveReturnDatesControl dateControl;
		protected FindCarPreferencesControl preferencesControl;
		protected FindCarJourneyOptionsControl journeyOptionsControl;
		protected ExpandableMenuControl expandableMenuControl; 
		protected HeaderControl headerControl;

		/// <summary>
		/// Holds user's current page state for this page
		/// </summary>
		private FindCarPageState pageState;

		/// <summary>
		/// Hold user's current journey parameters for car only journey
		/// </summary>
		private TDJourneyParametersMulti journeyParams;

		/// <summary>
		/// Holds user's map current page state.
		/// </summary>
		private InputPageState inputPageState;

		/// <summary>
		/// Helper class responsible for common methods to Find A pages
		/// </summary>
		private FindCarInputAdapter findInputAdapter;

		/// <summary>
		/// Helper class for Landing Page functionality
		/// </summary>
		private LandingPageHelper landingPageHelper = new LandingPageHelper();

		private const string RES_FROMTOTITLE = "FindCarInput.labelFindCarNote";
		private const string RES_PAGETITLE = "FindCarInput.labelFindCarTitle";

        private static readonly object BackEventKey = new object();

        #endregion

        #region Constructor, Page Load, Init, PreRender

        /// <summary>
		/// Default Constructor
		/// </summary>
		public FindCarInput()
		{
			pageId = PageId.FindCarInput;
		}

		/// <summary>
		/// Page Load event handler
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
        {
            LoadResources();

            #region Display Session Errors
            // Check if there are any errors in the InputPageState and display
            ErrorMessageAdapter.UpdateErrorDisplayControl(panelErrorDisplayControl, errorDisplayControl, TDSessionManager.Current.InputPageState.InputSessionErrors);

            // Clear the error messages
            TDSessionManager.Current.InputPageState.InputSessionErrors = null;
            #endregion

            if (Page.IsPostBack)
			{
				// The date controls don't raise events to say they have updated, so the params for these are
				// always update here
				updateOtherControls();

				CarCostingFuelValidationHelper FuelValidation = new CarCostingFuelValidationHelper();
				// Validate fuel cost entered by the user
				FuelValidation.FuelCostValidation(journeyParams, preferencesControl);
				// Validate fuel consumption eneterd by the user
				FuelValidation.FuelConsumptionValidation(journeyParams, preferencesControl);
            }
			else
			{
				InitialRequestSetup();
			}

			if (pageState.AmendMode)
			{
				journeyParams.DestinationType.Type = ControlType.Default;
				journeyParams.OriginType.Type = ControlType.Default;
				journeyParams.PrivateViaType.Type = ControlType.Default;
			}

            SetupLocationControls();
            
            findInputAdapter.InitPreferencesControl(preferencesControl, journeyOptionsControl);
            findInputAdapter.InitPreferencesDisplayMode(preferencesControl, journeyOptionsControl);
            
			if (!IsPostBack)
			{
				preferencesControl.DrivingSpeed = journeyParams.DrivingSpeed;
				preferencesControl.FindJourneyType = journeyParams.PrivateAlgorithmType;
				preferencesControl.CarSize = journeyParams.CarSize;
				preferencesControl.FuelType = journeyParams.CarFuelType;
				
				preferencesControl.FuelConsumptionUnit = journeyParams.FuelConsumptionUnit;
				preferencesControl.DoNotUseMotorways = journeyParams.DoNotUseMotorways;
				preferencesControl.FuelConsumptionValue = journeyParams.FuelConsumptionEntered;
				
				preferencesControl.FuelCostValue = journeyParams.FuelCostEntered;
                preferencesControl.FuelConsumptionOption = journeyParams.FuelConsumptionOption;
                preferencesControl.FuelCostOption = journeyParams.FuelCostOption;
			
				journeyOptionsControl.AvoidMotorways = journeyParams.AvoidMotorWays;
				journeyOptionsControl.AvoidFerries = journeyParams.AvoidFerries;
                journeyOptionsControl.AvoidTolls = journeyParams.AvoidTolls;
                journeyOptionsControl.BanLimitedAccess = journeyParams.BanUnknownLimitedAccess;
				journeyOptionsControl.AvoidRoadsList = journeyParams.AvoidRoadsList;
				journeyOptionsControl.UseRoadsList = journeyParams.UseRoadsList;
			}
		
			//Set help text for the Car preferences control (preferencesControl)
			TDResourceManager rm = Global.tdResourceManager;

			expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

			// Adding client side script for user navigation (when user hit enter, it should take the default action)
			UserExperienceEnhancementHelper.AddClientForUserNavigationDefaultAction(this.Page);
			
			if (TDSessionManager.Current.FindPageState.AmbiguityMode)
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindCarInput.HelpAmbiguityUrl");
			}
			else
			{
				Helpbuttoncontrol1.HelpUrl = GetResource("FindCarInput.HelpPageUrl");
			}

			#region Landing Page Functionality
			//Check if we need to initiate an automatic search due to Landing Page Autoplan Mode			
			if (TDSessionManager.Current.Session[ SessionKey.LandingPageAutoPlan ])
			{
                // Update location control resolve flag as this is a landing page request so don't want to validate,
                // the landing page will have validated
                if (journeyParams.OriginLocation != null && journeyParams.OriginLocation.Status == TDLocationStatus.Valid)
                {
                    originLocationControl.ResolveLocation = false;
                }
                if (journeyParams.DestinationLocation != null && journeyParams.DestinationLocation.Status == TDLocationStatus.Valid)
                {
                    destinationLocationControl.ResolveLocation = false;
                }
		
				SubmitRequest();
			}			
			#endregion

            // Page option buttons (next, clear page, advanced options)
            SetupPageOptionsControl(preferencesControl.PreferencesVisible);

            //Added for white labelling:
            ConfigureLeftMenu("FindCarInput.clientLink.BookmarkTitle", "Home.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindCarInput);
            expandableMenuControl.AddExpandedCategory("Related links");

            commandBack.Text = GetResource("FindCarInput.CommandBack.Text");

		}

		/// <summary>
		/// Page Unload event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Unload(object sender, System.EventArgs e)
		{
			//reset landing page session parameters
			if (TDSessionManager.Current.Session[ SessionKey.LandingPageCheck ])
			{
				landingPageHelper.ResetLandingPageSessionParameters();
			}
		}

		/// <summary>
		/// Performs page initialisation including event wiring.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Init(object sender, EventArgs e)
		{
			journeyOptionsControl = preferencesControl.JourneyOptionsControl;
			
			preferencesControl.JourneyTypeChanged += new EventHandler(preferencesControl_JourneyTypeChanged);
			preferencesControl.SpeedChanged += new EventHandler(preferencesControl_SpeedChanged);
			preferencesControl.CarSizeChanged += new EventHandler(preferencesControl_CarSizeChanged);
			preferencesControl.FuelTypeChanged += new EventHandler(preferencesControl_FuelTypeChanged);
			preferencesControl.ConsumptionOptionChanged += new EventHandler(preferencesControl_FuelConsumptionOptionChanged);
			preferencesControl.SpecificFuelUseUnitChanged += new EventHandler(preferencesControl_FuelUseUnitChanged);
			preferencesControl.FuelCostOptionChanged += new EventHandler(preferencesControl_FuelCostOptionChanged);
			preferencesControl.PreferencesVisibleChanged += new EventHandler(preferencesControl_PreferencesVisibleChanged);
			preferencesControl.FuelConsumptionTextChanged += new EventHandler(preferencesControl_FuelConsumptionTextChanged);
			preferencesControl.FuelCostTextChanged += new EventHandler(preferencesControl_FuelCostTextChanged);

			journeyOptionsControl.AvoidMotorwaysChanged += new EventHandler(preferencesControl_AvoidMotorwaysChanged);
			journeyOptionsControl.AvoidTollsChanged += new EventHandler(preferencesControl_AvoidTollsChanged);
            journeyOptionsControl.AvoidFerriesChanged += new EventHandler(preferencesControl_AvoidFerriesChanged);
            journeyOptionsControl.BanLimitedAccessChanged += new EventHandler(preferencesControl_BanLimitedAccessChanged);
			preferencesControl.DoNotUseMotorwayChanged += new EventHandler(preferencesControl_DoNotUseMotorwaysChanged);

            commandBack.Click += new EventHandler(preferencesControl_Back);

            preferencesControl.PageOptionsControl.Clear += new EventHandler(preferencesControl_Clear);
			preferencesControl.PageOptionsControl.Submit += new EventHandler(preferencesControl_Submit);
			preferencesControl.PreferencesOptionsControl.Submit += new EventHandler(preferencesControl_Submit);

			pageOptionsControltop.Clear += new EventHandler(preferencesControl_Clear);
            pageOptionsControltop.Submit += new EventHandler(preferencesControl_Submit);
            pageOptionsControltop.ShowAdvancedOptions += new EventHandler(pageOptionsControltop_ShowAdvancedOptions);
            pageOptionsControltop.HideAdvancedOptions += new EventHandler(pageOptionsControltop_HideAdvancedOptions);
            
            // Locations
            originLocationControl.MapLocationClick += new EventHandler(MapFromClick);
            originLocationControl.NewLocationClick += new EventHandler(NewLocationFromClick);

            destinationLocationControl.MapLocationClick += new EventHandler(MapToClick);
            destinationLocationControl.NewLocationClick += new EventHandler(NewLocationToClick);

            preferencesControl.JourneyOptionsControl.LocationControl.MapLocationClick += new EventHandler(MapViaCarClick);
            preferencesControl.JourneyOptionsControl.LocationControl.NewLocationClick += new EventHandler(NewLocationCarViaClick);

			dateControl.LeaveDateControl.DateChanged += new EventHandler(dateControlLeaveDateControl_DateChanged);
			dateControl.ReturnDateControl.DateChanged += new EventHandler(dateControlReturnDateControl_DateChanged);

			// Event Handler for default action button
			headerControl.DefaultActionEvent +=  new EventHandler(preferencesControl_Submit);

           
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, EventArgs e) 
		{
			// don't want to update the date when not postback...
			// otherwise double initial population
			if (Page.IsPostBack)
			{
				findInputAdapter.UpdateDateControl(dateControl);
			}

			findInputAdapter.UpdateErrorMessages(
				panelErrorMessage, labelErrorMessages, TDSessionManager.Current.ValidationError);

            commandBack.Visible = preferencesControl.AmbiguityMode;

			// hide from to title in ambiguity mode!
			labelFromToTitle.Visible = !pageState.AmbiguityMode;
			
			preferencesControl.FuelConsumptionOption = journeyParams.FuelConsumptionOption;
            preferencesControl.FuelCostOption = journeyParams.FuelCostOption;
            preferencesControl.FuelConsumptionValid = journeyParams.FuelConsumptionValid;
            preferencesControl.FuelCostValid = journeyParams.FuelCostValid;
            preferencesControl.FuelType = journeyParams.CarFuelType;
            preferencesControl.DrivingSpeed = journeyParams.DrivingSpeed;
            preferencesControl.FindJourneyType = journeyParams.PrivateAlgorithmType;
            preferencesControl.CarSize = journeyParams.CarSize;
            preferencesControl.FuelConsumptionUnit = journeyParams.FuelConsumptionUnit;
            journeyOptionsControl.AvoidRoadsList = journeyParams.AvoidRoadsList;  //re-instated jps
            journeyOptionsControl.UseRoadsList = journeyParams.UseRoadsList; //re-instated jps
            
            //Hide the advanced text if required:
            if (preferencesControl.PreferencesVisible || pageState.AmbiguityMode)
            {
                BlankPanelText(TDPageInformationHtmlPlaceHolderDefinition);
            }

            SetupMap();
		}

		#endregion

		#region Private Methods

        /// <summary>
        /// Load resources
        /// </summary>
		private void LoadResources()
		{
			PageTitle = GetResource("FindCarInput.AppendPageTitle")
				+ GetResource("JourneyPlanner.DefaultPageTitle");	
			labelFindPageTitle.Text = GetResource(RES_PAGETITLE);
			labelFromToTitle.Text = GetResource(RES_FROMTOTITLE);
            imageFindACar.ImageUrl = GetResource("HomeDefault.imageFindCar.ImageUrl");
            imageFindACar.AlternateText = " ";

            labelOriginTitle.Text = GetResource("originSelect.labelLocationTitle");
            labelDestinationTitle.Text = GetResource("destinationSelect.labelLocationTitle");
		}

		/// <summary>
		/// Updates session data with control values for those controls that do not raise events
		/// to signal that user has changed values (i.e. the date controls)
		/// </summary>
		private void updateOtherControls() 
		{
			pageState = (FindCarPageState)TDSessionManager.Current.FindPageState;
			journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;            
			inputPageState = TDSessionManager.Current.InputPageState;
			findInputAdapter = new FindCarInputAdapter(journeyParams,pageState, inputPageState);
			journeyParams.FuelConsumptionEntered = preferencesControl.FuelConsumptionValue;
            journeyParams.AvoidRoadsList = journeyOptionsControl.AvoidRoadsList;
            journeyParams.UseRoadsList = journeyOptionsControl.UseRoadsList;
			journeyParams.FuelCostEntered = preferencesControl.FuelCostValue;
			
			findInputAdapter.UpdateJourneyDates(dateControl);
		}

		/// <summary>
		/// Performs initialisation when page is loaded for the first time. This includes delegating to
		/// session manager by calling InitialiseJourneyParametersPageStates to create a new session data
		/// if necesssary (i.e. journey parameters and page state objects). If session data contains
		/// journey results then the user is redirected to the journey summary page.
		/// </summary>
		private void InitialRequestSetup() 
		{

			ITDSessionManager sessionManager = TDSessionManager.Current;
			sessionManager.ItineraryMode = ItineraryManagerMode.None;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

            #region Clear cache of journey data
            // if an extension is in progress, cancel it
            sessionManager.ItineraryManager.CancelExtension();

            ClearCacheHelper helper = new ClearCacheHelper();

            // Force clear of any printable information if added by the journey result page
            helper.ClearPrintableResultCache(TDSessionPartition.TimeBased);

            // Fix for IR5481 Session issue when going from between different planners using the left hand menu
            if (sessionManager.FindAMode != FindAMode.Car && sessionManager.FindAMode != FindAMode.CarPark)
            {
                // We have come directly from another planner so clear results from session.
                helper.ClearJourneyResultCache();
            }
            #endregion

            // Next, Initialise JourneyParameters and PageStates if needed. 
			// Will return true, if reset has been performed.
			bool resetDone;
			bool carParkLocation = false;
			bool originLocation = false;
			string carParkRef = "";
			
			//No reset is required if coming from landing page, since the journey parameters 
			//have already been initialised.
            if (TDSessionManager.Current.Session[SessionKey.LandingPageCheck] || (TDSessionManager.Current.GetOneUseKey(SessionKey.JourneyPlannerInputToMap) != null))
			{
				resetDone = false;
			}
			else
			{
				// If we've come from FindCarParkResults then need to retrieve CarPark details
				// before it is reset
				Stack returnStack = TDSessionManager.Current.InputPageState.JourneyInputReturnStack;
				PageId returnPageId;
				if(returnStack.Count != 0)
				{
					returnPageId = (PageId)returnStack.Pop();
							
					if ((returnPageId == PageId.FindCarParkResults) ||
						(returnPageId == PageId.FindCarParkMap))
					{
						carParkLocation = true;
						carParkRef = TDSessionManager.Current.InputPageState.CarParkReference;
                        
                        // Clear the reference to avoid any further usage (e.g. Find on map)
                        TDSessionManager.Current.InputPageState.CarParkReference = string.Empty;

						// Track if car park is the origin or destination location
						if (TDSessionManager.Current.FindCarParkPageState.LocationType == FindCarParkPageState.CurrentLocationType.To)
							originLocation = false;
						else
							originLocation = true;

						// Clear page stack (should be already, but just to make sure!)
						returnStack.Clear();
					}
				}

				resetDone = sessionManager.InitialiseJourneyParameters(FindAMode.Car);
			}
			pageState = (FindCarPageState)TDSessionManager.Current.FindPageState;
			journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;            
			inputPageState = TDSessionManager.Current.InputPageState;
			findInputAdapter = new FindCarInputAdapter(journeyParams,pageState, inputPageState);
            
			//reset the destiniation if previous data not find a car (e.g. park and ride)
			if (journeyParams.Destination.SearchType == SearchType.ParkAndRide)
			{
				journeyParams.DestinationLocation.ClearAll();
				journeyParams.DestinationLocation.ParkAndRideScheme = null;
				journeyParams.Destination.ClearAll();
				journeyParams.Destination.SearchType = SearchType.Locality;
			}

			if (resetDone)            
			{
				findInputAdapter.InitJourneyParameters();
			}


            // set up car park location
			if (carParkLocation)
			{
				if (originLocation)
			    {
                    // Get the original search location, and pass this in as the description
                    string description = sessionManager.FindCarParkPageState.LocationFrom.Description;

                    // Create the new location
                    journeyParams.OriginLocation = new TDLocation(carParkRef, description, originLocation);
					journeyParams.OriginLocation.Status = TDLocationStatus.Valid;
					journeyParams.Origin = new LocationSearch();
					journeyParams.Origin.SearchType = journeyParams.OriginLocation.SearchType;
					// Set location to fixed and valid in order for the control to appear resolved. 
					journeyParams.Origin.LocationFixed = true;
					
					// Ensure field is populated if we return to page by selecting Amend
					journeyParams.Origin.InputText = journeyParams.OriginLocation.Description;

                    journeyParams.DestinationLocation.ClearAll();
                    journeyParams.Destination.ClearAll();
                    journeyParams.Destination.SearchType = SearchType.Locality;

					// In case a return journey is planned, because Car park entrance and exit coordinates
					// may be different, we need to populate the ReturnDestinationLocation
                    journeyParams.ReturnDestinationLocation = new TDLocation(carParkRef, TDSessionManager.Current.JourneyParameters.OriginLocation.Description, !originLocation);
					journeyParams.ReturnDestinationLocation.Status = TDLocationStatus.Valid;
					journeyParams.ReturnOriginLocation = null;
				}
				else
				{
                    // Get the original search location, and pass this in as the description
                    string description = sessionManager.FindCarParkPageState.LocationTo.Description;

                    // Create the new location
                    journeyParams.DestinationLocation = new TDLocation(carParkRef, description, originLocation);
					journeyParams.DestinationLocation.Status = TDLocationStatus.Valid;
					journeyParams.Destination = new LocationSearch();
					journeyParams.Destination.SearchType = journeyParams.DestinationLocation.SearchType;
					// Set location to fixed and valid in order for the control to appear resolved. 
					journeyParams.Destination.LocationFixed = true;
					
					// Ensure field is populated if we return to page by selecting Amend
					journeyParams.Destination.InputText = journeyParams.DestinationLocation.Description;

					// Reset origin to prevent scenario where from and to are both car parks if we are not in drive to mode
                    if(sessionManager.FindCarParkPageState.CurrentFindMode != FindCarParkPageState.FindCarParkMode.DriveTo)
                    {
                        journeyParams.OriginLocation.ClearAll();
					    journeyParams.Origin.ClearAll();
					    journeyParams.Origin.SearchType = SearchType.Locality;
                    }
					
					// In case a return journey is planned, because Car park entrance and exit coordinates
					// may be different, we need to populate the ReturnOriginLocation
					journeyParams.ReturnOriginLocation = new TDLocation(carParkRef, TDSessionManager.Current.JourneyParameters.DestinationLocation.Description, !originLocation);
					journeyParams.ReturnOriginLocation.Status = TDLocationStatus.Valid;
					journeyParams.ReturnDestinationLocation = null;					
				}
			}

			// initialise the date control from session (required as displaying help forces redirect)
			findInputAdapter.InitDateControl(dateControl);
		}

        /// <summary>
        /// Initialises and setsup the Location controls (origin, destination, via)
        /// </summary>
        private void SetupLocationControls()
        {
            findInputAdapter.InitLocationsControl(originLocationControl, destinationLocationControl);
            findInputAdapter.InitViaLocationsControl(journeyOptionsControl, true);

            if (Page.IsPostBack)
            {
                // If page is posting back, then check if the location controls need to display the 
                // location gazetteer options expanded 
                if (originLocationControl.MoreOptionSelected)
                    originLocationControl.ShowMoreOptionsExpanded = true;

                if (destinationLocationControl.MoreOptionSelected)
                    destinationLocationControl.ShowMoreOptionsExpanded = true;
            }
        }

        /// <summary>
        /// Setsup the page options control buttons
        /// </summary>
        private void SetupPageOptionsControl(bool preferencesVisible)
        {
            // Always set both pageOptionsControls, default to visible 
            pageOptionsControltop.Visible = true;

            pageOptionsControltop.AllowBack = false; // Only ever want to show the Back button at top of page
            pageOptionsControltop.AllowHideAdvancedOptions = false; // Only ever want to show the Hide button at the bottom of preferences control
            pageOptionsControltop.AllowShowAdvancedOptions = (pageState.AmbiguityMode) ? false : true; // Only allow show advanced options in non-ambiguity mode
            pageOptionsControltop.AllowNext = true;
            pageOptionsControltop.AllowClear = true;

            preferencesControl.PageOptionsControl.AllowBack = false;
            preferencesControl.PageOptionsControl.AllowHideAdvancedOptions = (pageState.AmbiguityMode) ? false : true;
            preferencesControl.PageOptionsControl.AllowShowAdvancedOptions = false;
            preferencesControl.PageOptionsControl.AllowNext = true;
            preferencesControl.PageOptionsControl.AllowClear = true;

            // Show only one pageOptionsControl
            pageOptionsControltop.Visible = (pageState.AmbiguityMode) ? true : !preferencesVisible;
        }

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

            if (preferencesControl.PreferencesVisible)
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
                if (!pageState.AmbiguityMode)
                {
                    mapInputControl.Visible = true;
                }
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

            MapLocationPoint origin = mapHelper.GetMapLocationPoint(journeyParams.OriginLocation, MapLocationSymbolType.Start, true, false);

            MapLocationPoint destination = mapHelper.GetMapLocationPoint(journeyParams.DestinationLocation, MapLocationSymbolType.End, true, false);

            MapLocationPoint publicVia = mapHelper.GetMapLocationPoint(journeyParams.PublicViaLocation, MapLocationSymbolType.Via, true, false);

            MapLocationPoint privateVia = mapHelper.GetMapLocationPoint(journeyParams.PrivateViaLocation, MapLocationSymbolType.Via, true, false);

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
        /// Method containing the preferencesControl_Submit method functionality so that it
        /// can be called by the Landing page when AutoPlanning is required. 
        /// </summary>
        private void SubmitRequest()
        {
            if (preferencesControl.SavePreferences)
            {
                findInputAdapter.SaveTravelDetails();
            }
            dateControl.CalendarClose();

            if (!pageState.AmbiguityMode)
            {
                pageState.SaveJourneyParameters(journeyParams);
            }

            #region Origin and Destination locations

            originLocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.Origin = originLocationControl.Search;
            journeyParams.OriginLocation = originLocationControl.Location;

            destinationLocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.Destination = destinationLocationControl.Search;
            journeyParams.DestinationLocation = destinationLocationControl.Location;

            #endregion

            #region Via location

            // Car Via 
            preferencesControl.JourneyOptionsControl.LocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.PrivateVia = preferencesControl.JourneyOptionsControl.LocationControl.Search;
            journeyParams.PrivateViaLocation = preferencesControl.JourneyOptionsControl.LocationControl.Location;

            #endregion

            // Set up the JourneyPlanControlData
            findInputAdapter.InitialiseAsyncCallState();

            // Validate the JourneyParameters
            JourneyPlanRunner.JourneyPlanRunner runner = new JourneyPlanRunner.JourneyPlanRunner(Global.tdResourceManager);

            pageState.AmbiguityMode = !runner.ValidateAndRun(
                TDSessionManager.Current,
                TDSessionManager.Current.JourneyParameters,
                GetChannelLanguage(TDPage.SessionChannelName),
                true);

            if (pageState.AmbiguityMode)
            {
                if (!TDSessionManager.Current.Session[SessionKey.LandingPageAutoPlan])
                {
                    // Indicate the locations when valid/resolved should be shown as fixed when page is in ambiguity 
                    originLocationControl.ShowFixedLocation = true;
                    destinationLocationControl.ShowFixedLocation = true;
                    preferencesControl.JourneyOptionsControl.LocationControl.ShowFixedLocation = true;

                    preferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.ReadOnly;
                    preferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.ReadOnly;
                    dateControl.LeaveDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                    dateControl.ReturnDateControl.DateErrors = TDSessionManager.Current.ValidationError;
                    dateControl.LeaveDateControl.AmbiguityMode = true;
                    dateControl.ReturnDateControl.AmbiguityMode = true;
                    preferencesControl.CarSizeDisplayMode = GenericDisplayMode.ReadOnly;
                    preferencesControl.FuelTypeDisplayMode = GenericDisplayMode.ReadOnly;
                    preferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.ReadOnly;
                    preferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.ReadOnly;
                    preferencesControl.FuelCostOptionMode = GenericDisplayMode.ReadOnly;
                    journeyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.ReadOnly;
                    journeyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.ReadOnly;
                }
                else
                {
                    pageState.AmbiguityMode = false;
                }

                // If ambiguity and preferences are selected, then pageOptionsControl will be the one
                // displayed in the preferences control
                SetupPageOptionsControl(preferencesControl.PreferencesSelected);
            }
            else
            {
                // Extending journeys using a Find A is not currently possible so clear down the itinerary
                TDItineraryManager itineraryManager = TDItineraryManager.Current;
                if (itineraryManager.Length >= 1)
                {
                    itineraryManager.ResetItinerary();
                }

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindAInputOk;

            }

            // IR2468 - adding code to refresh the controls otherwise, the amendMode
            // change is not taken into account until next postback
            if (pageState.AmendMode)
            {
                //IR1417 -  Disable AmendMode when leaving the page
                pageState.AmendMode = false;
            }
        }

        #endregion

        #endregion

        #region Event Handlers

        /// <summary>
        /// Event handler called when next button clicked. The journey plan runner component validates the 
        /// current journey parameters. If invalid then the page is put into ambiguity mode otherwise 
        /// journey planning commences. User preferences are saved before commencing journey planning.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_Submit(object sender, EventArgs e)
        {
            SubmitRequest();
        }

        /// <summary>
        /// Event handler called when back button clicked in ambiguous mode. Decrements the level of
        /// hierarchical location searches (origin, destination and private via). If all locations are at highest 
        /// level then page is reverted to input mode and original input parameters reinstated.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_Back(object sender, EventArgs e)
        {
            dateControl.CalendarClose();

            if (findInputAdapter.IsAtHighestLevel())
            {
                //Check to see if there is a previous page waiting on the stack
                if (inputPageState.JourneyInputReturnStack.Count != 0)
                {
                    TransitionEvent lastPage = (TransitionEvent)inputPageState.JourneyInputReturnStack.Pop();
                    //If the user is returing to the previous journey results, re-validate them
                    if (lastPage == TransitionEvent.FindAInputRedirectToResults)
                    {
                        TDSessionManager.Current.JourneyResult.IsValid = true;
                    }
                    TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = lastPage;
                    return;
                }

                pageState.AmbiguityMode = false;
                pageState.ReinstateJourneyParameters(journeyParams);
                TDSessionManager.Current.ValidationError.Initialise();
                preferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.CarSizeDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.FuelTypeDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.Normal;
                preferencesControl.FuelCostOptionMode = GenericDisplayMode.Normal;
                preferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.Normal;
                dateControl.LeaveDateControl.AmbiguityMode = false;
                dateControl.ReturnDateControl.AmbiguityMode = false;
                dateControl.LeaveDateControl.DateErrors = null;
                dateControl.ReturnDateControl.DateErrors = null;
                journeyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.Normal;
                journeyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.Normal;
                preferencesControl.FuelConsumptionOption = journeyParams.FuelConsumptionOption;
                preferencesControl.FuelCostOption = journeyParams.FuelCostOption;
                preferencesControl.FuelConsumptionValue = journeyParams.FuelConsumptionEntered;
                preferencesControl.FuelCostValue = journeyParams.FuelCostEntered;
                preferencesControl.FuelConsumptionUnit = journeyParams.FuelConsumptionUnit;

                SetupLocationControls();
            }
            else
            {
                // Decrement the drilldown level of any ambiguous locations
                // that are not yet at their highest level
                journeyParams.Origin.DecrementLevel();
                journeyParams.Destination.DecrementLevel();
                journeyParams.PrivateVia.DecrementLevel();
            }

            //IR2222 - ensure the DivHider panel on the carpreferencescontrol is made visible
            preferencesControl.PanelDivHider.Visible = true;

            //reset the destiniation if previous data not find a car (e.g. park and ride)
            if (journeyParams.Destination.SearchType == SearchType.ParkAndRide)
            {
                journeyParams.DestinationLocation.ClearAll();
                journeyParams.DestinationLocation.ParkAndRideScheme = null;
                journeyParams.Destination.ClearAll();
                journeyParams.Destination.SearchType = SearchType.Locality;
            }

            SetupPageOptionsControl(preferencesControl.PreferencesVisible);
        }

        /// <summary>
        /// Occurs when the Back button is clicked
        /// </summary>
        public event EventHandler Back
        {
            add { this.Events.AddHandler(BackEventKey, value); }
            remove { this.Events.RemoveHandler(BackEventKey, value); }
        }

        /// <summary>
        /// Event handler called when clear page button is clicked. Journey parameters are reset
        /// to initial values, page controls updated and the page set to input mode.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void preferencesControl_Clear(object sender, EventArgs e)
        {
            findInputAdapter.InitJourneyParameters();
            pageState.AmbiguityMode = false;

            // Hide advanced options 
            preferencesControl.PreferencesVisible = false;

            // Hide map
            mapInputControl.Visible = false;

            // Reset location controls
            originLocationControl.Reset(SearchType.Locality);
            destinationLocationControl.Reset(SearchType.Locality);
            preferencesControl.JourneyOptionsControl.LocationControl.Reset(SearchType.Locality);

            findInputAdapter.InitLocationsControl(originLocationControl, destinationLocationControl);
            findInputAdapter.InitViaLocationsControl(journeyOptionsControl, true);
            findInputAdapter.InitPreferencesControl(preferencesControl, journeyOptionsControl);
            findInputAdapter.InitPreferencesDisplayMode(preferencesControl, journeyOptionsControl);

            dateControl.LeaveDateControl.DateErrors = null;
            dateControl.LeaveDateControl.AmbiguityMode = false;
            dateControl.ReturnDateControl.DateErrors = null;
            dateControl.ReturnDateControl.AmbiguityMode = false;
            TDSessionManager.Current.ValidationError = null;
            TDPage.CloseAllSingleWindows(Page);

            preferencesControl.PanelDivHider.Visible = true;
            preferencesControl.InputConsumptionText = null;
            preferencesControl.InputCostText = null;
            journeyOptionsControl.ClearRoads();
            //journeyOptionsControl.LocationControlType = journeyParams.PrivateViaType;
            preferencesControl.CarSizeDisplayMode = GenericDisplayMode.Normal;
            preferencesControl.FuelTypeDisplayMode = GenericDisplayMode.Normal;
            preferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.Normal;
            preferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.Normal;
            preferencesControl.FuelCostOptionMode = GenericDisplayMode.Normal;
            //journeyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.Normal;
            //journeyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.Normal;
            preferencesControl.DrivingSpeed = journeyParams.DrivingSpeed;
            preferencesControl.FindJourneyType = journeyParams.PrivateAlgorithmType;
            preferencesControl.CarSize = journeyParams.CarSize;
            preferencesControl.FuelType = journeyParams.CarFuelType;
            preferencesControl.FuelConsumptionOption = journeyParams.FuelConsumptionOption;
            preferencesControl.FuelCostOption = journeyParams.FuelCostOption;
            preferencesControl.FuelConsumptionUnit = journeyParams.FuelConsumptionUnit;
            preferencesControl.DoNotUseMotorways = journeyParams.DoNotUseMotorways;
            preferencesControl.FuelConsumptionValue = journeyParams.FuelConsumptionEntered;
            preferencesControl.FuelCostValue = journeyParams.FuelCostEntered;

            journeyOptionsControl.AvoidMotorways = journeyParams.AvoidMotorWays;
            journeyOptionsControl.AvoidFerries = journeyParams.AvoidFerries;
            journeyOptionsControl.AvoidTolls = journeyParams.AvoidTolls;
            journeyOptionsControl.BanLimitedAccess = journeyParams.BanUnknownLimitedAccess;
            journeyOptionsControl.AvoidRoadsList = journeyParams.AvoidRoadsList;
            journeyOptionsControl.UseRoadsList = journeyParams.UseRoadsList;

            // Reset the Map locations to show on the map (use Origin as it should have been reset by now)
            inputPageState.MapLocation = journeyParams.OriginLocation;
            inputPageState.MapLocationSearch = journeyParams.Origin;

            // Preference options hidden when clear selected
            SetupPageOptionsControl(false);
        }

        #region Preference events

        /// <summary>
		/// Handler for JourneyType changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_JourneyTypeChanged(object sender, EventArgs e)
		{
			journeyParams.PrivateAlgorithmType = preferencesControl.FindJourneyType;
		}

		/// <summary>
		/// Handler for Driving speed changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_SpeedChanged(object sender, EventArgs e)
		{
			journeyParams.DrivingSpeed = preferencesControl.DrivingSpeed;
		}		

		/// <summary>
		/// Handler for car size changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_CarSizeChanged(object sender, EventArgs e)
		{
			journeyParams.CarSize = preferencesControl.CarSize;
		}
		
		/// <summary>
		/// Handler for fuel type changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelTypeChanged(object sender, EventArgs e)
		{
			journeyParams.CarFuelType = preferencesControl.FuelType;
		}

		/// <summary>
		/// Handler for consumption option changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelConsumptionOptionChanged(object sender, EventArgs e)
		{
			journeyParams.FuelConsumptionOption = preferencesControl.FuelConsumptionOption;
			
		}

		/// <summary>
		/// Handler for fuel use unit changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelUseUnitChanged(object sender, EventArgs e)
		{
			journeyParams.FuelConsumptionUnit= preferencesControl.FuelConsumptionUnit;
		}

		/// <summary>
		/// Handler for fuel cost option changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelCostOptionChanged(object sender, EventArgs e)
		{
			journeyParams.FuelCostOption = preferencesControl.FuelCostOption;
		}
		private void preferencesControl_FuelConsumptionTextChanged(object sender, EventArgs e)
		{
			journeyParams.FuelConsumptionEntered = preferencesControl.FuelConsumptionValue;
			//journeyParams.PreConvertedFuelConsumption = preferencesControl.FuelConsumptionText;
		}
		private void preferencesControl_FuelCostTextChanged(object sender, EventArgs e)
		{
			journeyParams.FuelCostEntered = preferencesControl.FuelCostValue;
		}

		/// Handler for Avoid Motorways changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_AvoidMotorwaysChanged(object sender, EventArgs e)
		{
			journeyParams.AvoidMotorWays = journeyOptionsControl.AvoidMotorways;
		}

		/// Handler for DoNotUseMotorways changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_DoNotUseMotorwaysChanged(object sender, EventArgs e)
		{
			journeyParams.DoNotUseMotorways = preferencesControl.DoNotUseMotorways;
		}

		/// Handler for Avoid Tolls changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_AvoidTollsChanged(object sender, EventArgs e)
		{
			journeyParams.AvoidTolls = journeyOptionsControl.AvoidTolls;
		}

        /// Handler for Avoid Ferries changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_AvoidFerriesChanged(object sender, EventArgs e)
        {
            journeyParams.AvoidFerries = journeyOptionsControl.AvoidFerries;
        }

        /// Handler for Avoid Ferries changed event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void preferencesControl_BanLimitedAccessChanged(object sender, EventArgs e)
        {
            journeyParams.BanUnknownLimitedAccess = journeyOptionsControl.BanLimitedAccess;
        }

		/// <summary>
		/// Event handler called when visibility of preferences changed. Updates page state with new value.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		private void preferencesControl_PreferencesVisibleChanged(object sender, EventArgs e) 
		{
			pageState.TravelDetailsVisible = preferencesControl.PreferencesVisible;

            SetupPageOptionsControl(preferencesControl.PreferencesVisible);
		}

        /// <summary>
        /// White Labeling - Added event for pageOptionsControltop which raises when user hides AdvanceOptions.
        /// this will hide advance options. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageOptionsControltop_HideAdvancedOptions(object sender, EventArgs e)
        {
            preferencesControl.PreferencesVisible = false;

            SetupPageOptionsControl(false);
        }

        /// <summary>
        /// White Labeling - Added event for pageOptionsControltop which raises when user clicks AdvanceOptions.
        /// this will show advance options and hide pageOptionsControltop control. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void pageOptionsControltop_ShowAdvancedOptions(object sender, EventArgs e)
        {
            // Showing Advance options
            preferencesControl.PreferencesVisible = true;

            SetupPageOptionsControl(true);
        }

        #endregion

        #region Location events

        /// <summary>
        /// Event handler called when new location button is clicked for the "from" location.
        /// The journey parameters are updated with the "from" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void NewLocationFromClick(object sender, EventArgs e)
        {
            // Set search and location to be the location controls values
            journeyParams.Origin = originLocationControl.Search;
            journeyParams.Origin.SearchType = SearchType.Locality;
            journeyParams.OriginLocation = originLocationControl.Location;

            // If a car park was being planned from (Exit coords), we will have populated the return 
            // location (because it would use Entrance coords), therefore need to clear it
            if (journeyParams.ReturnDestinationLocation != null)
            {
                journeyParams.ReturnDestinationLocation.ClearAll();
                journeyParams.ReturnDestinationLocation = null;
            }

            // If the map control is visible, set it to focus in on the destination, if that is valid
            if (mapInputControl.Visible)
            {
                if (journeyParams.DestinationLocation.Status == TDLocationStatus.Valid)
                {
                    inputPageState.MapLocationSearch = journeyParams.Destination;
                    inputPageState.MapLocation = journeyParams.DestinationLocation;
                }
            }
        }

        /// <summary>
        /// Event handler called when new location button is clicked for the "to" location.
        /// The journey parameters are updated with the "to" location control's new values.
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        private void NewLocationToClick(object sender, EventArgs e)
        {
            // Set search and location to be the location controls values
            journeyParams.Destination = destinationLocationControl.Search;
            journeyParams.Destination.SearchType = SearchType.Locality;
            journeyParams.DestinationLocation = destinationLocationControl.Location;

            // If a car park was being planned to (Entrance coords), we will have populated the return 
            // location (because it would use Exit coords), therefore need to clear it
            if (journeyParams.ReturnOriginLocation != null)
            {
                journeyParams.ReturnOriginLocation.ClearAll();
                journeyParams.ReturnOriginLocation = null;
            }

            // If the map control is visible, set it to focus in on the origin, if that is valid
            if (mapInputControl.Visible)
            {
                if (journeyParams.OriginLocation.Status == TDLocationStatus.Valid)
                {
                    inputPageState.MapLocationSearch = journeyParams.Origin;
                    inputPageState.MapLocation = journeyParams.OriginLocation;
                }
            }
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
            journeyParams.PrivateVia = preferencesControl.JourneyOptionsControl.LocationControl.Search;
            journeyParams.PrivateViaLocation = preferencesControl.JourneyOptionsControl.LocationControl.Location;
            journeyParams.PrivateVia.SearchType = SearchType.Locality;
        }

        #endregion

        #region Map events

        /// <summary>
        /// Event handler for when Find on map is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapFromClick(object sender, EventArgs e)
        {
            bool shiftForm = false;
            inputPageState.MapType = CurrentLocationType.From;
            inputPageState.MapMode = CurrentMapMode.FromFindAInput;

            #region Resolve location for map

            // Reset map search and location
            inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
            inputPageState.MapLocationSearch.ClearAll();

            // Validate selected location and save to parameters ready for input/map use
            originLocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.Origin = originLocationControl.Search;
            journeyParams.OriginLocation = originLocationControl.Location;
            inputPageState.MapLocationSearch = originLocationControl.Search;
            inputPageState.MapLocation = originLocationControl.Location;

            // If no input text, display default map
            if (inputPageState.MapLocationSearch.InputText.Length == 0)
            {
                inputPageState.MapLocationControlType.Type = ControlType.Default;
            }
            // If valid location, display the location on map
            else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
            {
                // Ensure search type is set to map to allow map/location controls to handle if needed
                journeyParams.Origin.SearchType = SearchType.Map;
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
        /// Event handler for when Find on map is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapToClick(object sender, EventArgs e)
        {
            bool shiftForm = false;
            inputPageState.MapType = CurrentLocationType.To;
            inputPageState.MapMode = CurrentMapMode.FromFindAInput;

            #region Resolve location for map

            // Reset map search and location
            inputPageState.MapLocation.Status = TDLocationStatus.Unspecified;
            inputPageState.MapLocationSearch.ClearAll();

            // Validate selected location and save to parameters ready for input/map use
            destinationLocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.Destination = destinationLocationControl.Search;
            journeyParams.DestinationLocation = destinationLocationControl.Location;
            inputPageState.MapLocationSearch = destinationLocationControl.Search;
            inputPageState.MapLocation = destinationLocationControl.Location;

            // If no input text, display default map
            if (inputPageState.MapLocationSearch.InputText.Length == 0)
            {
                inputPageState.MapLocationControlType.Type = ControlType.Default;
            }
            // If valid location, display the location on map
            else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
            {
                // Ensure search type is set to map to allow map/location controls to handle if needed
                journeyParams.Destination.SearchType = SearchType.Map;
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

            // Validate selected location and save to parameters ready for input/map use
            preferencesControl.JourneyOptionsControl.LocationControl.Validate(journeyParams, false, true, true, StationType.UndeterminedNoGroup);

            journeyParams.PrivateVia = preferencesControl.JourneyOptionsControl.LocationControl.Search;
            journeyParams.PrivateViaLocation = preferencesControl.JourneyOptionsControl.LocationControl.Location;
            inputPageState.MapLocationSearch = preferencesControl.JourneyOptionsControl.LocationControl.Search;
            inputPageState.MapLocation = preferencesControl.JourneyOptionsControl.LocationControl.Location;

            // If no input text, display default map
            if (inputPageState.MapLocationSearch.InputText.Length == 0)
            {
                inputPageState.MapLocationControlType.Type = ControlType.Default;
            }
            // If valid location, display the location on map
            else if (inputPageState.MapLocation.Status == TDLocationStatus.Valid)
            {
                // Ensure search type is set to map to allow map/location controls to handle if needed
                journeyParams.PrivateVia.SearchType = SearchType.Map;
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

        #region Date events

        private void dateControlLeaveDateControl_DateChanged(object sender, EventArgs e)
        {
            journeyParams.OutwardDayOfMonth = dateControl.LeaveDateControl.DateControl.Day;
            journeyParams.OutwardMonthYear = dateControl.LeaveDateControl.DateControl.MonthYear;
        }

        private void dateControlReturnDateControl_DateChanged(object sender, EventArgs e)
        {
            journeyParams.ReturnDayOfMonth = dateControl.ReturnDateControl.DateControl.Day;
            journeyParams.ReturnMonthYear = dateControl.ReturnDateControl.DateControl.MonthYear;
        }

		#endregion
		
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
