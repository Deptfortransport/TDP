// *********************************************** 
// NAME                 : ExtendJourneyInput.aspx.cs
// AUTHOR               : Andrew Sinclair
// DATE CREATED         : 04/01/2006
// DESCRIPTION			: ExtendJourneyInput page, allowing users to 
// enter information to request an extension to a journey. 
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/ExtendJourneyInput.aspx.cs-arc  $
//
//   Rev 1.17   Mar 14 2011 15:12:14   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.16   Apr 12 2010 16:58:26   pghumra
//Changed to avoid page chrashing when clear page and amend buttons are clicked
//Resolution for 5504: CODE FIX - NEW - Del 10.x - Page crash when using modify in flight
//
//   Rev 1.15   Mar 29 2010 16:39:22   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.14   Jan 19 2010 13:20:56   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.13   Dec 11 2009 09:39:36   apatel
//ExtendedJourneyInput mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.12   Dec 10 2009 12:41:38   apatel
//Make find on map button not to show on page
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.11   Dec 04 2009 11:17:26   apatel
//Walkit control update to put walkit logo
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.10   Sep 24 2009 08:43:38   apatel
//updates for CCN FI88 - Connecting Journeys using modify
//Resolution for 5324: FI88 - Connecting Journeys using modify
//
//   Rev 1.9   Jan 08 2009 14:57:16   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.8   Nov 14 2008 11:56:14   pscott
//Post regression test changes (start reqd as well as end)
//Resolution for 5098: Partial Postcodes when extend journey
//
//   Rev 1.7   Oct 17 2008 11:55:26   build
//Automatically merged from branch for stream0093
//
//   Rev 1.6.1.0   Aug 07 2008 09:37:12   pscott
//SCR 5098  allow partial postcodes
//Resolution for 5098: Partial Postcodes when extend journey
//
//   Rev 1.6   Jul 09 2008 16:00:28   rbroddle
//Added Alt Text for imageExtendJourney
//Resolution for 5016: WAI WCAG level A compliance faults - Missing Alt text
//
//   Rev 1.5   Jul 02 2008 09:29:36   rbroddle
//Added imageExtendJourney.AlternateText 
//Resolution for 5016: WAI WCAG level A compliance faults - Missing Alt text
//
//   Rev 1.4   Jun 26 2008 14:04:24   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.3   Apr 30 2008 15:45:14   mmodi
//Fixed problem with summary being shown after clicking back on modify journey. Also, order of buttons and enablement of buttons sorted on modify journey screens.
//Resolution for 4911: When a journey is modified in any way, the default results tab is the summary tab
//
//   Rev 1.2   Mar 31 2008 13:24:16   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Feb 23 2008 17:25:00 apatel
//  Advance Option Controls modify to show preference control correctly. 
//
//   Rev 1.0   Nov 08 2007 13:29:14   mturner
//Initial revision.
//
//   Rev 1.24   May 04 2006 12:26:34   asinclair
//Set errors after it has been populated if in Amend mode
//Resolution for 3962: DN068: Extend: Ambiguiy / Error messages on Extend Input page
//
//   Rev 1.23   May 03 2006 10:33:16   asinclair
//Now using the ResetCar method for the Car via locations
//Resolution for 3962: DN068: Extend: Ambiguiy / Error messages on Extend Input page
//
//   Rev 1.22   Apr 29 2006 16:20:30   asinclair
//Corrected issues with Back and New Location buttons. Clear button no longer displayed when page is in Ambiguity mode
//Resolution for 3962: DN068: Extend: Ambiguiy / Error messages on Extend Input page
//
//   Rev 1.21   Apr 29 2006 11:51:14   asinclair
//Added check to see if page is in Amend mode
//Resolution for 3984: DN068: Previously resolved extension locations are treated as ambiguous on amend
//
//   Rev 1.20   Apr 28 2006 12:40:34   asinclair
//Added ErrorDisplay Control  and fixed Clear button issues
//Resolution for 3962: DN068: Extend: Ambiguiy / Error messages on Extend Input page
//Resolution for 3974: DN068 Extend: 'Clear page' does not clear all values on Extend input page
//
//   Rev 1.19   Apr 21 2006 14:50:16   asinclair
//The destinationLocationControl_MapClick now uses the correct location
//Resolution for 3968: DN068 Extend: 'Find on map' not working for extend from end location
//
//   Rev 1.18   Apr 19 2006 11:41:48   asinclair
//Added code to correct set the display of the Car details when selecting 'Back'
//Resolution for 3744: DN068 Extend: Problems with 'via' fields on Extend input page
//
//   Rev 1.17   Apr 18 2006 09:49:10   asinclair
//Added missing label
//
//   Rev 1.16   Apr 10 2006 11:47:56   asinclair
//Correctly set the Page to go to when CJP can not return results
//Resolution for 3700: DN068 Extend: Journey options not searched for when adding a second extension
//Resolution for 3767: DN068 Extend: Inescapable loop on extend input page
//Resolution for 3827: DN068 Extend: Stuck on input page when planning 2nd extension
//Resolution for 3832: DN068 Extend: Selecting Amend on extension results options page shows Resolved extend input screen
//
//   Rev 1.15   Apr 05 2006 15:24:38   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.14   Apr 01 2006 17:44:10   asinclair
//Work in progress for IR 3744
//Resolution for 3744: DN068 Extend: Problems with 'via' fields on Extend input page
//
//   Rev 1.13   Mar 31 2006 16:59:46   halkatib
//Added setters for the PublicRequired and PrivateRequired properties in order to set the public/private required check boxes. 
//Added code to check the status of public/private required within journey parameters and assign the corresponding value to properties.
//
//   Rev 1.12   Mar 31 2006 16:12:30   RGriffith
//Addition of Default Click and Resetting of Fares
//
//   Rev 1.11   Mar 31 2006 12:50:40   asinclair
//FxCop fixes and fix to not display save preferences message
//Resolution for 3691: DN068 Extend: Remove option to save user preferences on Extend Input
//
//   Rev 1.10   Mar 29 2006 17:11:08   rgreenwood
//IR3743 - Added labelAdvanced
//
//   Rev 1.9   Mar 29 2006 11:03:30   tmollart
//Modified back button code so that it takes user back to the correct place.
//Resolution for 3699: DN068 Extend: Clicking 'Back' when adding a second extension removes first extension
//
//   Rev 1.8   Mar 21 2006 12:48:46   asinclair
//Updated with code review comments
//
//   Rev 1.7   Mar 20 2006 10:29:52   asinclair
//Added information link for disabled travellers
//
//   Rev 1.6   Mar 15 2006 10:15:06   asinclair
//Manual Merge for stream3353
//
//   Rev 1.5.1.1   Mar 14 2006 20:23:06   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.5.1.0   Mar 14 2006 19:50:10   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.5   Mar 10 2006 09:39:06   asinclair
//Commented code and removed duplicated lines
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 06 2006 11:14:06   asinclair
//Added functionality to allow Map click and also the planing of car only journeys
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 03 2006 16:02:28   pcross
//Skip links / layout changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 22 2006 17:59:16   asinclair
//Updated to fix issues with Back and Clear buttons and HTML formatting
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 21 2006 09:46:36   asinclair
//Work in progress check in
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 13 2006 14:37:44   asinclair
//Initial revision.
 
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
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;

using TransportDirect.UserPortal.JourneyControl;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;

using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
using ControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for ExtendJourneyInput.
	/// </summary>
	public partial class ExtendJourneyInput : TDPage
	{
		
		
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.TriStateLocationControl2 originLocationControl;
		protected TransportDirect.UserPortal.Web.Controls.TriStateLocationControl2 destinationLocationControl;
		protected TransportDirect.UserPortal.Web.Controls.AmendStopoverControl amendStopoverControl;
		protected TransportDirect.UserPortal.Web.Controls.TransportTypesControl transportTypesControl;
		protected TransportDirect.UserPortal.Web.Controls.PtPreferencesControl ptPreferencesControl;
		protected TransportDirect.UserPortal.Web.Controls.FindCarPreferencesControl carPreferencesControl;
		protected TransportDirect.UserPortal.Web.Controls.FindPageOptionsControl findPageOptionsControl;

		protected TransportDirect.UserPortal.Web.Controls.ItineraryViasControl destinationViasControl;
		protected TransportDirect.UserPortal.Web.Controls.ItineraryViasControl originViasControl;
		protected TransportDirect.UserPortal.Web.Controls.ExtendJourneyLineControl extendJourneyLineControl;
		protected TransportDirect.UserPortal.Web.Controls.ErrorDisplayControl errorDisplayControl;

		private ControlPopulator populator;
		
		private LocationSearch originSearch;
		private TDJourneyParametersMulti journeyParameters;
		private TDLocation originLocation;
		private LocationSearch destinationSearch;
		private TDLocation destinationLocation;

        private TDLocation publicViaLocation;
		private LocationSearch publicViaSearch;
		
		private LocationSelectControlType originLocationSelectControlType;
		private LocationSelectControlType destinationLocationSelectControlType;

		private LocationSelectControlType publicViaType;
		private LocationSearch privateViaSearch;
		private TDLocation privateViaLocation;
		private LocationSelectControlType privateViaType;


		private InputPageState pageState;
		private ValidationError errors;

		private InputAdapter extendInputAdapter = new InputAdapter();
        private List<MapLocationMode> mapModes = new List<MapLocationMode>();

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public ExtendJourneyInput()
		{
			// Set page Id
			pageId = PageId.ExtendJourneyInput;
			
			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;

			// Get DataServices from Service Discovery
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}

		#region Event Handlers

		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			pageState = sessionManager.InputPageState;

			if (pageState.AmendMode)
			{
				destinationLocationControl.AmendMode = true;
			}

			// Reset fares so that they are recalculated after every extension
			if (!Page.IsPostBack)		
			{
				itineraryManager.ResetFares();
			}
			
			//Populate the label and button text for the page
			PopulateControls();

			journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;

			//save certain settings to journeyparameters based on selected values if postback
			if (Page.IsPostBack)
			{
				SaveCurrentSettings();
			}

			//Loads certain session variables from the JourneyParameters
			LoadSessionVariables();

			//Loads certain control values based on what is currently in journeyParameters
			InitialiseControls();

			//If first time page is accessed, then set these car values based on what is in the journeyparameters
			if(!Page.IsPostBack)
			{
				carPreferencesControl.DrivingSpeed = journeyParameters.DrivingSpeed;
				carPreferencesControl.CarSize = journeyParameters.CarSize;
				carPreferencesControl.FuelType = journeyParameters.CarFuelType;

				carPreferencesControl.FuelConsumptionUnit = journeyParameters.FuelConsumptionUnit;
				carPreferencesControl.DoNotUseMotorways = journeyParameters.DoNotUseMotorways;
				carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
				
				carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;
				carPreferencesControl.FuelConsumptionOption = journeyParameters.FuelConsumptionOption;
				carPreferencesControl.FuelCostOption = journeyParameters.FuelCostOption;

				populator.LoadListControl(DataServiceType.PublicTransportsCheck, transportTypesControl.ModesPublicTransport);
			}

			carPreferencesControl.PageOptionsControl.Visible = false;

			journeyParameters.OriginType = new
				TDJourneyParametersMulti.LocationSelectControlType(TDJourneyParametersMulti.ControlType.Default);

			journeyParameters.DestinationType = new
				TDJourneyParametersMulti.LocationSelectControlType(TDJourneyParametersMulti.ControlType.Default);


			//If the PublicModes are null then use the values from 
			if (journeyParameters.PublicModes == null)
			{
				journeyParameters.PublicModes = transportTypesControl.PublicModes;
			}

			transportTypesControl.PublicModes = journeyParameters.PublicModes;

			//If we are extending from the end of the itinerary, then setup the correct Location controls
			if(itineraryManager.ExtendEndOfItinerary)
			{		
				journeyParameters.DestinationType = new
					TDJourneyParametersMulti.LocationSelectControlType(TDJourneyParametersMulti.ControlType.Default);
				destinationLocationSelectControlType = journeyParameters.DestinationType;
				originLocationSelectControlType = journeyParameters.OriginType;


				originLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.From, ref originSearch, ref originLocation, ref originLocationSelectControlType, false, true, true, Page.IsPostBack);
				destinationLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.To, ref destinationSearch, ref destinationLocation, ref destinationLocationSelectControlType, false, true, true, Page.IsPostBack);

				originLocationTitleLabel.Visible = false;
				destinationLocationTitleLabel.Visible = true; 
				
				originLocationControl.Visible = false;

				PopulateViaLocationsControl(originViasControl);
				originViasControl.Visible = true;
				originViasControl.LabelTitle1Text.Text = GetResource("ItineraryViasControl.labelFrom.Text");
				originViasControl.LabelTitle2Text.Text = GetResource("ItineraryViasControl.labelVia.Text");

                mapModes.Add(MapLocationMode.End);

			}
			else
			//We are extending to the start of the journey
			{
				
				journeyParameters.OriginType = new
					TDJourneyParametersMulti.LocationSelectControlType(TDJourneyParametersMulti.ControlType.Default);
				destinationLocationSelectControlType = journeyParameters.DestinationType;
				originLocationSelectControlType = journeyParameters.OriginType;

				originLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.From, ref originSearch, ref originLocation, ref originLocationSelectControlType, false, true, true, Page.IsPostBack);
				destinationLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.To, ref destinationSearch, ref destinationLocation, ref destinationLocationSelectControlType, false, true, true, Page.IsPostBack);
				
				destinationLocationControl.Visible = false;

				PopulateViaLocationsControl(destinationViasControl);
				destinationViasControl.Visible = true;

				destinationViasControl.LabelTitle1Text.Text = GetResource("ItineraryViasControl.labelVia.Text");
				destinationViasControl.LabelTitle2Text.Text = GetResource("ItineraryViasControl.labelTo.Text");

				originLocationTitleLabel.Visible = true;
				destinationLocationTitleLabel.Visible = false;

                mapModes.Add(MapLocationMode.Start);

			}

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextExtendJourneyInput);
            expandableMenuControl.AddExpandedCategory("Related links");
        }

		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
            this.PageTitle = GetResource("ExtendJourneyInput.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

            newJourneyButton.Text = GetResource("ExtensionResultsSummary.newJourneyButton.Text");
			buttonBack.Text = GetResource("ExtendOptionsControl.Back");

			labelShow.Text = GetResource("ExtendJourneyInput.labelShow.Text");
			checkBoxPublicTransport.Text = GetResource("ExtendJourneyInput.checkboxPublicTransport.Text");
			checkBoxCarRoute.Text = GetResource("ExtendJourneyInput.checkboxCarRoute.Text");

			originLocationTitleLabel.Text = GetResource("ExtendJourney.FromLabel");
			destinationLocationTitleLabel.Text = GetResource("ExtendJourney.ToLabel");

			helpButton.HelpUrl = GetResource("ExtendJourneyInput.helpButton.HelpUrl");
			labelAdvanced.Text = GetResource ("ExtendInput.labelAdvanced.Text");
            imageExtendJourney.ImageUrl = GetResource("RefineJourney.extendJourney.ImageURL");
            imageExtendJourney.AlternateText = " ";

		}

		/// <summary>
		/// OnPreRender event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{				
			//Displays the Advanced options or Hide button depending on pageState
			findPageOptionsControl.AllowShowAdvancedOptions = !pageState.AdvancedOptionsVisible;
			findPageOptionsControl.AllowHideAdvancedOptions = pageState.AdvancedOptionsVisible;
			panelAdvancedOptions.Visible = pageState.AdvancedOptionsVisible;
			panelTransportTypes.Visible = pageState.AdvancedOptionsVisible;
			
			carPreferencesControl.PreferencesVisible = pageState.AdvancedOptionsVisible;

			if(pageState.AdvancedOptionsVisible)
			{

				ptPreferencesControl.JourneyChangesOptionsControl.Visible = true;
				ptPreferencesControl.WalkingSpeedOptionsControl.Visible = true;
				ptPreferencesControl.ViaLocationControl.Visible = true;
				ptPreferencesControl.PreferencesVisible = true;
			}

			
			if(pageState.AmbiguityMode)
			{
				labelTitle.Text = GetResource("ExtendJourneyInput.AmbiguityTitle");

				panelTransportTypes.Visible = false;

                findPageOptionsControl.Visible = true;

				findPageOptionsControl.AllowShowAdvancedOptions = false;

				findPageOptionsControl.AllowHideAdvancedOptions = false;
				
				findPageOptionsControl.AllowClear = false;

				PopulateAmendAmbigStopOverControl();

				labelTransport.Text =
					Global.tdResourceManager.GetString("JourneyPlannerAmbiguity.labelShow", TDCultureInfo.CurrentUICulture);

				string transports = string.Empty;
				if (journeyParameters.PublicRequired)
					transports += Global.tdResourceManager.GetString(
						"JourneyPlanner.PublicTransportLowerCase",
						TDCultureInfo.CurrentUICulture);
				transports += (journeyParameters.PrivateRequired && journeyParameters.PublicRequired)? 
					" " +Global.tdResourceManager.GetString(
					"JourneyPlanner.AndLowerCase",
					TDCultureInfo.CurrentUICulture) +" "
					: string.Empty;
            
				if (journeyParameters.PrivateRequired)
					transports += Global.tdResourceManager.GetString(
						"JourneyPlanner.CarLowerCase",
						TDCultureInfo.CurrentUICulture);
				labelTransport.Text = string.Format(labelTransport.Text, transports);

				checkBoxCarRoute.Visible = false;
				checkBoxPublicTransport.Visible = false;
				labelTransport.Visible = true;

				labelShow.Text = GetResource("ExtendJourneyInput.labelShowAmbiguity.Text");

				if (!journeyParameters.PrivateRequired && !journeyParameters.PublicRequired)
				{
					labelShow.Visible = false;
					labelTransport.Visible = false;
					
				}

				if(errorDisplayControl.ErrorStrings.Length > 0)
				{
					panelErrorMessage.Visible = true;
				}
			}
			else
			{
				checkBoxCarRoute.Visible = true;
				checkBoxPublicTransport.Visible = true;
				labelTransport.Visible = false;

				labelIntroductoryText.Text = GetResource("ExtendJourneyInput.IntroductoryText");
				labelTitle.Text = GetResource("ExtendJourneyInput.Title");

				panelErrorMessage.Visible = false;
				findPageOptionsControl.AllowClear = true;
			

			}

			SetupSkipLinksAndScreenReaderText();
			
			ptPreferencesControl.PreferencesOptionsControl.AllowSavePreferences = false;
			carPreferencesControl.PreferencesOptionsControl.AllowSavePreferences = false;
			
			ptPreferencesControl.JourneyChangesOptionsControl.AllowSavePreferences = false;
			carPreferencesControl.AllowSavePreferences = false;

			PrivateRequired = journeyParameters.PrivateRequired;
			PublicRequired = journeyParameters.PublicRequired;

            // screen reader text

            // setting the label for the tri state location input box
            originLocationControl.LocationUnspecifiedControl.TypeInstruction.Text = GetResource("langstrings", "originSelect.labelSRLocation");

            // setting the label for the tri state location input box
            destinationLocationControl.LocationUnspecifiedControl.TypeInstruction.Text = GetResource("langstrings", "destinationSelect.labelSRLocation");

            //originLocationControl.ShowFindOnMap = false;
            //destinationLocationControl.ShowFindOnMap = false;
            //carPreferencesControl.JourneyOptionsControl.TristateLocationControl.ShowFindOnMap = false;
            ptPreferencesControl.ViaLocationControl.TristateLocationControl.ShowFindOnMap = false;

            SetupMap();

			base.OnPreRender(e);


		}

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{
			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink1.AlternateText = GetResource("ExtendJourneyInput.imageMainContentSkipLink.AlternateText");
			imageMainContentSkipLink2.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink2.AlternateText = GetResource("ExtendJourneyInput.imageInputFormSkipLink.AlternateText");

		}


		/// <summary>
		/// Sets certain seesion variables from the parameters
		/// </summary>
		private void LoadSessionVariables()
		{
			originSearch = journeyParameters.Origin;
			originLocation = journeyParameters.OriginLocation;
			
			destinationSearch = journeyParameters.Destination;
			destinationLocation = journeyParameters.DestinationLocation;

			privateViaSearch = journeyParameters.PrivateVia;
			privateViaLocation = journeyParameters.PrivateViaLocation;
			privateViaType = journeyParameters.PrivateViaType;
			publicViaSearch = journeyParameters.PublicVia;
			publicViaLocation = journeyParameters.PublicViaLocation;
	
		}

		/// <summary>
		/// Initialise the Extend Journey Line Control
		/// </summary>
		private void InitialiseJourneyLineControl()
		{
			// Use the ExtendJourneyAdapter to populate the journey line control
			ExtendJourneyAdapter journeyExtension = new ExtendJourneyAdapter();
			journeyExtension.PopulateExtendJourneyLineControl(extendJourneyLineControl);
		}

		/// <summary>
		/// Initialises controls used on page
		/// </summary>
		private void InitialiseControls() 
		{
			InputAdapter inputAdapter = new InputAdapter();

			//Sets the Car Via location and search objects based on what is currently in journeyparameters
			carPreferencesControl.JourneyOptionsControl.TheLocation = journeyParameters.PrivateViaLocation;
			carPreferencesControl.JourneyOptionsControl.TheSearch = journeyParameters.PrivateVia;

            ptPreferencesControl.WalkingSpeedOptionsControl.IsPageOptionsVisible = false;
            ptPreferencesControl.ViaLocationControl.IsPageOptionsVisible = false;
			
			journeyParameters.PublicViaType = new
				TDJourneyParametersMulti.LocationSelectControlType(TDJourneyParametersMulti.ControlType.Default);

			journeyParameters.PrivateViaType = new
				TDJourneyParametersMulti.LocationSelectControlType(TDJourneyParametersMulti.ControlType.Default);

			//Initialises the Public transport preferences control
			inputAdapter.InitialisePTPreferencesControl(ptPreferencesControl);
			//Populates the Public transport preferences control
			inputAdapter.PopulatePTPreferencesControl(ptPreferencesControl, journeyParameters);

			publicViaType = journeyParameters.PublicViaType;
			privateViaType = journeyParameters.PrivateViaType;

			//Sets the Public transport location and search objects based on what is currently in journeyparameters
			ptPreferencesControl.ViaLocationControl.LocationType = CurrentLocationType.PublicVia;
			ptPreferencesControl.ViaLocationControl.TheLocation = journeyParameters.PublicViaLocation;
			ptPreferencesControl.ViaLocationControl.TheSearch = journeyParameters.PublicVia;
			ptPreferencesControl.ViaLocationControl.LocationControlType = journeyParameters.PublicViaType;

			ptPreferencesControl.ViaLocationControl.TristateLocationControl.Populate(DataServiceType.PTViaDrop, CurrentLocationType.PublicVia, ref publicViaSearch, ref publicViaLocation, ref publicViaType, false, false, false, Page.IsPostBack);
			
			carPreferencesControl.JourneyOptionsControl.LocationControlType =  journeyParameters.PrivateViaType;

			carPreferencesControl.JourneyOptionsControl.TristateLocationControl.Populate(DataServiceType.CarViaDrop, CurrentLocationType.PrivateVia, ref privateViaSearch, ref privateViaLocation,ref privateViaType, false, true, false, Page.IsPostBack);

			InitialiseJourneyLineControl();

			if(!IsPostBack)
			{
				PrivateRequired = journeyParameters.PrivateRequired;
				PublicRequired = journeyParameters.PublicRequired;
			}
		}

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			findPageOptionsControl.ShowAdvancedOptions += new EventHandler(Advanced_CommandEventHandler);
			findPageOptionsControl.HideAdvancedOptions += new EventHandler(Advanced_CommandEventHandler);
			headerControl.DefaultActionEvent += new EventHandler(Next_CommandEventHandler);
			findPageOptionsControl.Submit += new EventHandler(Next_CommandEventHandler);
			findPageOptionsControl.Clear += new EventHandler(NewClear_CommandEventHandler);

            // CCN 0427 Handling events of car preference control's page options control
            carPreferencesControl.PageOptionsControl.HideAdvancedOptions += new EventHandler(Advanced_CommandEventHandler);
            carPreferencesControl.PageOptionsControl.Submit += new EventHandler(Next_CommandEventHandler);
            carPreferencesControl.PageOptionsControl.Clear += new EventHandler(NewClear_CommandEventHandler);
			
			transportTypesControl.ModesPublicTransport.SelectedIndexChanged += new EventHandler(ModesPublicTransport_SelectedIndexChanged);
			
			originLocationControl.MapClick += new EventHandler(originLocationControl_MapClick);
			destinationLocationControl.MapClick +=new EventHandler(destinationLocationControl_MapClick);
			ptPreferencesControl.ViaLocationControl.MapClick +=new EventHandler(ViaLocationControl_MapClick);
			carPreferencesControl.JourneyOptionsControl.TristateLocationControl.MapClick +=new EventHandler(TristateLocationControl_MapClick);
			
			originLocationControl.NewLocation += new EventHandler(originLocationControl_NewLocation);
			destinationLocationControl.NewLocation += new EventHandler(destinationLocationControl_NewLocation);

			
			carPreferencesControl.SpeedChanged += new EventHandler(preferencesControl_SpeedChanged);
			carPreferencesControl.CarSizeChanged += new EventHandler(preferencesControl_CarSizeChanged);
			carPreferencesControl.FuelTypeChanged += new EventHandler(preferencesControl_FuelTypeChanged);
			carPreferencesControl.FuelCostOptionChanged += new EventHandler(preferencesControl_FuelCostOptionChanged);
			carPreferencesControl.SpecificFuelUseUnitChanged +=new EventHandler(preferencesControl_FuelUseUnitChanged);
			carPreferencesControl.ConsumptionOptionChanged +=new EventHandler(preferencesControl_FuelConsumptionOptionChanged);
			carPreferencesControl.DoNotUseMotorwayChanged += new EventHandler(preferencesControl_DoNotUseMotorwaysChanged);
			carPreferencesControl.FuelConsumptionTextChanged += new EventHandler(preferencesControl_FuelConsumptionTextChanged);
			carPreferencesControl.FuelCostTextChanged += new EventHandler(preferencesControl_FuelCostTextChanged);
			carPreferencesControl.JourneyTypeChanged += new EventHandler(preferencesControl_JourneyTypeChanged);

			
			ptPreferencesControl.JourneyChangesOptionsControl.ChangesOptionChanged += new EventHandler(preferencesControl_PtJourneyChanges);
			ptPreferencesControl.JourneyChangesOptionsControl.ChangeSpeedOptionChanged += new EventHandler(preferencesControl_PtChangesSpeed);
			ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDurationOptionChanged += new EventHandler(preferencesControl_PtWalkDuration);
			ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeedOptionChanged += new EventHandler(preferencesControl_PtWalkSpeed);

            this.newJourneyButton.Click += new EventHandler(newJourneyButton_Click);
        }

        /// <summary>
        /// Handles new search button click event.
        /// This code is designed to return to the input page the results were derived from and reset 
        /// it to a blank input values ready for a new search.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void newJourneyButton_Click(object sender, EventArgs e)
        {
            RefineHelper refineHelper = new RefineHelper();
            refineHelper.NewJourney();
        }

		/// <summary>
		/// Handles the Back button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void buttonBack_Click(object sender, System.EventArgs e)
		{
			if(!pageState.AmbiguityMode)
			{

				ExtendItineraryManager im = TDSessionManager.Current.ItineraryManager as ExtendItineraryManager;

				// If the user presses back button we always need to remove the last extension.
				im.DeleteLastExtension();

				// Then look at the itinerary. If the user already has an extended journey they
				// should be returned back to their (extended) results. Otherwise they should go
				// back to the refine journey page.
				if (im != null)
				{
					if (im.Length > 1)
					{
						TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ExtendedFullItinerarySummary;
					}
					else
					{
						TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineJourney;
					}
				}
			}
			else
			{
				pageState.AmbiguityMode = false;

				LoadSessionVariables();

				carPreferencesControl.PanelDivHider.Visible=true;
					
				ptPreferencesControl.ViaLocationControl.SetLocationUnspecified();
				carPreferencesControl.JourneyOptionsControl.SetLocationUnspecified();

				//Only set the Origin and Destination locations depending on which bit we are extending.
				if(TDItineraryManager.Current.ExtendEndOfItinerary)
				{
					destinationLocation.Status = TDLocationStatus.Unspecified;
					destinationLocation = new TDLocation();
					journeyParameters.DestinationLocation = destinationLocation;
					destinationLocationSelectControlType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
					destinationSearch.ClearSearch();
					destinationLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.To, ref destinationSearch, ref destinationLocation, ref destinationLocationSelectControlType, false, true, false, Page.IsPostBack);
					
				}
				else
				{
					originLocation.Status = TDLocationStatus.Unspecified;
					originLocation = new TDLocation();
					journeyParameters.OriginLocation = originLocation;
					originLocationSelectControlType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
					originSearch.ClearSearch();
					originLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.From, ref originSearch, ref originLocation, ref originLocationSelectControlType, false, true, false, Page.IsPostBack);
				}					
			}
		}

		/// <summary>
		/// Handles the ShowPreferences event of the FindPageOptionsControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Advanced_CommandEventHandler(object sender, EventArgs e)
		{
			pageState.AdvancedOptionsVisible = !pageState.AdvancedOptionsVisible;
            // CCN 0427 When advance options visible the top page option controls shouldn't be visible.
            findPageOptionsControl.Visible = !pageState.AdvancedOptionsVisible;
            

		}


		/// <summary>
		/// Handles the Next button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Next_CommandEventHandler(object sender, EventArgs e)
		{
			pageState.AmbiguityMode = false;
			pageState.AmendMode = false;
			errors = TDSessionManager.Current.ValidationError;	

			//Get the Public Modes used for this journey
			if( checkBoxPublicTransport.Checked)
			{
				journeyParameters.PublicModes = transportTypesControl.PublicModes;
			}
			else
			{
				journeyParameters.PublicModes = new TransportDirect.JourneyPlanning.CJPInterface.ModeType[0];
			}
			//Number of changes
			journeyParameters.PublicAlgorithmType = ptPreferencesControl.JourneyChangesOptionsControl.Changes;
			//Speed for making changes
			journeyParameters.InterchangeSpeed = ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed;
			//Walking speed
			journeyParameters.WalkingSpeed = ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed;
			//Time to walk
			journeyParameters.MaxWalkingTime = ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration;
			journeyParameters.AvoidMotorWays = carPreferencesControl.JourneyOptionsControl.AvoidMotorways;
			journeyParameters.AvoidFerries = carPreferencesControl.JourneyOptionsControl.AvoidFerries;
			journeyParameters.AvoidTolls = carPreferencesControl.JourneyOptionsControl.AvoidTolls;
            journeyParameters.BanUnknownLimitedAccess = carPreferencesControl.JourneyOptionsControl.BanLimitedAccess;
            journeyParameters.AvoidRoadsList = carPreferencesControl.JourneyOptionsControl.AvoidRoadsList;
            journeyParameters.UseRoadsList = carPreferencesControl.JourneyOptionsControl.UseRoadsList;

			journeyParameters.CarSize =	carPreferencesControl.CarSize;
			journeyParameters.CarFuelType = carPreferencesControl.FuelType;

			if (carPreferencesControl.FuelCostText != null)
			{
				  journeyParameters.FuelCostEntered = carPreferencesControl.FuelCostValue;
			}

			
			journeyParameters.FuelConsumptionOption = carPreferencesControl.FuelConsumptionOption;
			journeyParameters.FuelConsumptionEntered = carPreferencesControl.FuelConsumptionValue;
			
			journeyParameters.FuelConsumptionUnit = carPreferencesControl.FuelConsumptionUnit;

			journeyParameters.FuelCostOption = carPreferencesControl.FuelCostOption;
			journeyParameters.FuelCostEntered = carPreferencesControl.FuelCostValue;

			
			journeyParameters.PrivateRequired =	PrivateRequired;			
			journeyParameters.PublicRequired =	PublicRequired;	

			if (TDItineraryManager.Current.ExtendInProgress)
			{
				amendStopoverControl.UpdateRequestedTimes();
                if (TDItineraryManager.Current.ExtendEndOfItinerary)
                {
                    if (journeyParameters.OriginLocation.RequestPlaceType == TransportDirect.JourneyPlanning.CJPInterface.RequestPlaceType.Locality)
                    {
                        if (journeyParameters.OriginLocation.NaPTANs.Length > 0)
                        {
                            journeyParameters.OriginLocation.RequestPlaceType = TransportDirect.JourneyPlanning.CJPInterface.RequestPlaceType.NaPTAN;

                        }
                    }
                }
                else
                {
                    if (journeyParameters.DestinationLocation.RequestPlaceType == TransportDirect.JourneyPlanning.CJPInterface.RequestPlaceType.Locality)
                    {
                        if (journeyParameters.DestinationLocation.NaPTANs.Length > 0)
                        {
                            journeyParameters.DestinationLocation.RequestPlaceType = TransportDirect.JourneyPlanning.CJPInterface.RequestPlaceType.NaPTAN;

                        }
                    }
                }
			}

			journeyParameters.IsReturnRequired = TDItineraryManager.Current.ReturnExtendPermitted;

			// Save journey parameters that may change during ambiguity resolution
			// so that the may be reinstated if the changes are cancelled
			TDSessionManager.Current.AmbiguityResolution = new AmbiguityResolutionState();
			TDSessionManager.Current.AmbiguityResolution.SaveJourneyParameters();
						
			AsyncCallState acs = new JourneyPlanState();
			// Determine refresh interval and resource string for the wait page
			acs.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.Extend"]);
			acs.WaitPageMessageResourceFile = "RefineJourney";
			acs.WaitPageMessageResourceId = "WaitPageMessage.Extend";

			acs.AmbiguityPage = PageId.ExtendJourneyInput;
			acs.DestinationPage = PageId.ExtensionResultsSummary;
			acs.ErrorPage = PageId.ExtensionResultsSummary;
			TDSessionManager.Current.AsyncCallState = acs;


			//If extending from end, then check destination
			//else extending to start, so check origin
			if(TDItineraryManager.Current.ExtendEndOfItinerary)
			{
				destinationLocationControl.Search(IsValidNaptan);
			}
			else
			{
				originLocationControl.Search(IsValidNaptan);
			}

			//	Only do this if the user has changed the entry
			ptPreferencesControl.ViaLocationControl.Search();
			carPreferencesControl.JourneyOptionsControl.TristateLocationControl.Search(true);	

			ExtendJourneyAdapter extendJourneyAdapter = new ExtendJourneyAdapter();

			//page is in AmbiguityMode if ExtendValidateAndSearch returns false
			pageState.AmbiguityMode = !extendJourneyAdapter.ExtendValidateAndSearch();

			//If not Ambiguity, then invoke the ExtendJourneyInputNext transition event
			//else populate the location controls for Ambiguity
			if (!pageState.AmbiguityMode)
			{
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;// ExtendJourneyInputNext;
			}
			else
				if (pageState.AmbiguityMode) 
			{
				errors = TDSessionManager.Current.ValidationError;
				CarCostingFuelValidationHelper FuelValidation = new CarCostingFuelValidationHelper();
				// Validate fuel cost entered by the user
				FuelValidation.FuelCostValidation(journeyParameters, carPreferencesControl);
				// Validate fuel consumption eneterd by the user
				FuelValidation.FuelConsumptionValidation(journeyParameters, carPreferencesControl);

				carPreferencesControl.TypeJourneyDisplayMode = GenericDisplayMode.ReadOnly;
				carPreferencesControl.SpeedChangeDisplayMode = GenericDisplayMode.ReadOnly;

				carPreferencesControl.CarSizeDisplayMode = GenericDisplayMode.ReadOnly;

				carPreferencesControl.JourneyOptionsControl.AvoidRoadDisplayMode = GenericDisplayMode.ReadOnly;
				carPreferencesControl.JourneyOptionsControl.UseRoadDisplayMode = GenericDisplayMode.ReadOnly;

				ptPreferencesControl.JourneyChangesOptionsControl.ChangesDisplayMode = GenericDisplayMode.ReadOnly;
				ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeedDisplayMode = GenericDisplayMode.ReadOnly;
				ptPreferencesControl.ChangesDisplayMode = GenericDisplayMode.ReadOnly;
				ptPreferencesControl.ChangesSpeedDisplayMode = GenericDisplayMode.ReadOnly;

				ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeedDisplayMode = GenericDisplayMode.ReadOnly;
				ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDurationDisplayMode = GenericDisplayMode.ReadOnly;

				carPreferencesControl.FuelUseUnitDisplayMode = GenericDisplayMode.ReadOnly;
				carPreferencesControl.FuelTypeDisplayMode = GenericDisplayMode.ReadOnly;
				carPreferencesControl.FuelConsumptionOptionMode = GenericDisplayMode.ReadOnly;
				carPreferencesControl.FuelCostOptionMode = GenericDisplayMode.ReadOnly;

				carPreferencesControl.CarSizeDisplayMode = GenericDisplayMode.ReadOnly;

				
				if (TDItineraryManager.Current.ExtendInProgress) 
				{

					labelStopOverErrorMessage.Text = string.Empty;

					if ( FindDateValidation.OutwardDateInPast || FindDateValidation.ReturnDateInPast || FindDateValidation.AreDatesPast)
					{
						labelStopOverErrorMessage.Text = Global.tdResourceManager.GetString(
							"ValidateAndRun.StopOverTimeIntoPast",
							TDCultureInfo.CurrentUICulture) + " ";
					}

					if (FindDateValidation.IsExtensionReturnOverlap) 
					{
						labelStopOverErrorMessage.Text += Global.tdResourceManager.GetString(
							"ValidateAndRun.ExtensionReturnOverlap",
							TDCultureInfo.CurrentUICulture);
					}
				}

				//error message 
				labelWarningMessages.Text = string.Empty;

				if (AreLocationsAmbiguous)
				{
					labelIntroductoryText.Text = Global.tdResourceManager.GetString(
						"ValidateAndRun.SelectFromList", TDCultureInfo.CurrentUICulture) + " ";

				}

				
				// Display "Select/type in a new location" if origin or destination locations
				// have not been specified or cannot be resolved
				if(AreLocationsUnspecified)
				{
					labelIntroductoryText.Text += Global.tdResourceManager.GetString(
						"ValidateAndRun.SelectLocation", TDCultureInfo.CurrentUICulture) + " ";
				}

				extendInputAdapter.PopulateErrorDisplayControl(errorDisplayControl, TDSessionManager.Current.ValidationError);
			}
		

		}



		/// <summary>
		/// Handles the Clear and New search event of the FindPageOptionsControl
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void NewClear_CommandEventHandler(object sender, EventArgs e)
		{
			
			pageState.AmbiguityMode = false;

			ITDSessionManager sessionManager = TDSessionManager.Current;

			journeyParameters = sessionManager.JourneyParameters as TDJourneyParametersMulti;

			journeyParameters = new TDJourneyParametersMulti();

			//Advanced options are not visible
			pageState.AdvancedOptionsVisible = false;

			//LoadSessionVariables();
	        journeyParameters.PublicAlgorithmType = TransportDirect.JourneyPlanning.CJPInterface.PublicAlgorithmType.Default;

			ptPreferencesControl.JourneyChangesOptionsControl.Changes = journeyParameters.PublicAlgorithmType;
			ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed = journeyParameters.InterchangeSpeed;
			ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed = journeyParameters.WalkingSpeed;
			ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration = journeyParameters.MaxWalkingTime;
            
			carPreferencesControl.FindJourneyType = journeyParameters.PrivateAlgorithmType;

			carPreferencesControl.DrivingSpeed = journeyParameters.DrivingSpeed;
			carPreferencesControl.CarSize = journeyParameters.CarSize;
			carPreferencesControl.FuelType = journeyParameters.CarFuelType;

			carPreferencesControl.DoNotUseMotorways = journeyParameters.DoNotUseMotorways;
			carPreferencesControl.FuelConsumptionValue = journeyParameters.FuelConsumptionEntered;
				
			carPreferencesControl.FuelCostValue = journeyParameters.FuelCostEntered;
			carPreferencesControl.FuelConsumptionOption = journeyParameters.FuelConsumptionOption;
			carPreferencesControl.FuelCostOption = journeyParameters.FuelCostOption;
			carPreferencesControl.FuelConsumptionUnit = journeyParameters.FuelConsumptionUnit +1 ;

			carPreferencesControl.JourneyOptionsControl.AvoidTolls = journeyParameters.AvoidTolls;
			carPreferencesControl.JourneyOptionsControl.AvoidFerries = journeyParameters.AvoidFerries;
            carPreferencesControl.JourneyOptionsControl.AvoidMotorways = journeyParameters.AvoidMotorWays;
            carPreferencesControl.JourneyOptionsControl.BanLimitedAccess = journeyParameters.BanUnknownLimitedAccess;

			carPreferencesControl.JourneyOptionsControl.AvoidRoadsList = journeyParameters.AvoidRoadsList;

			//Clear the Avoid and Use roads lists
			carPreferencesControl.JourneyOptionsControl.ClearRoads();

			// Repopulate transportTypesControl and update parameters with default values
			populator.LoadListControl(DataServiceType.PublicTransportsCheck, transportTypesControl.ModesPublicTransport);
			journeyParameters.PublicModes = transportTypesControl.PublicModes;

			amendStopoverControl.Reset();

			//Clear the two Via location controls
			ptPreferencesControl.ViaLocationControl.TristateLocationControl.Reset();
			carPreferencesControl.JourneyOptionsControl.TristateLocationControl.ResetCar();

			ptPreferencesControl.ViaLocationControl.LocationControlType = journeyParameters.PublicViaType;
			ptPreferencesControl.ViaLocationControl.LocationType = CurrentLocationType.PublicVia;
			ptPreferencesControl.ViaLocationControl.TheLocation = journeyParameters.PublicViaLocation;
			ptPreferencesControl.ViaLocationControl.TheSearch = journeyParameters.PublicVia;

			carPreferencesControl.JourneyOptionsControl.LocationControlType = journeyParameters.PrivateViaType;
			carPreferencesControl.JourneyOptionsControl.LocationType = CurrentLocationType.PrivateVia;
			carPreferencesControl.JourneyOptionsControl.TheLocation = journeyParameters.PrivateViaLocation;
			carPreferencesControl.JourneyOptionsControl.TheSearch = journeyParameters.PrivateVia;

			InitialiseControls();
		
			//Clear the Location controls, depending on which end we are extending to/from
            if (TDItineraryManager.Current.ExtendEndOfItinerary)
            {
                destinationLocationControl.Reset();

                destinationLocationControl_NewLocation(null, null);
                sessionManager.JourneyParameters = journeyParameters;

                // Reset the Map locations to show on the map (use Destination as it should have been reset by now)
                pageState.MapLocation = journeyParameters.DestinationLocation;
                pageState.MapLocationSearch = journeyParameters.Destination;

            }
            else
            {
                originLocationControl.Reset();

                originLocationControl_NewLocation(null, null);

                sessionManager.JourneyParameters = journeyParameters;

                // Reset the Map locations to show on the map (use Origin as it should have been reset by now)
                pageState.MapLocation = journeyParameters.OriginLocation;
                pageState.MapLocationSearch = journeyParameters.Origin;

            }
			ptPreferencesControl.JourneyChangesOptionsControl.Changes = journeyParameters.PublicAlgorithmType;
			
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.ExtendJourneyInput;

            findPageOptionsControl.Visible = true;

		}

		/// <summary>
		/// Transport modes selection changed event handler
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">ImageClickEventArgs</param>
		private void ModesPublicTransport_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!pageState.TravelOptionsChanged.Contains(TravelOptionsChangedEnum.PublicTransport))
				pageState.TravelOptionsChanged.Add(TravelOptionsChangedEnum.PublicTransport);
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
		
		
		// <summary>
		/// Handler for fuel type changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelTypeChanged(object sender, EventArgs e)
		{
			journeyParameters.CarFuelType = carPreferencesControl.FuelType;
		}

		// <summary>
		/// Handler for consumption option changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelConsumptionOptionChanged(object sender, EventArgs e)
		{
			journeyParameters.FuelConsumptionOption = carPreferencesControl.FuelConsumptionOption;
			
		}

		// <summary>
		/// Handler for fuel use unit changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelUseUnitChanged(object sender, EventArgs e)
		{
			journeyParameters.FuelConsumptionUnit= carPreferencesControl.FuelConsumptionUnit;
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
		/// Sets the Fuel consumption on the JourneyParameters to be the value entered
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelConsumptionTextChanged(object sender, EventArgs e)
		{
			journeyParameters.FuelConsumptionEntered = carPreferencesControl.FuelConsumptionValue;

		}

		/// <summary>
		/// Sets the Fuel cost on the JourneyParameters to be the value entered
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_FuelCostTextChanged(object sender, EventArgs e)
		{
			journeyParameters.FuelCostEntered = carPreferencesControl.FuelCostValue;
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
		/// Handler for Public journey changes changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_PtJourneyChanges(object sender, EventArgs e)
		{
			journeyParameters.PublicAlgorithmType = ptPreferencesControl.JourneyChangesOptionsControl.Changes;
		}

		/// <summary>
		/// Handler for speed of making public journey changes changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_PtChangesSpeed(object sender, EventArgs e)
		{
			journeyParameters.InterchangeSpeed = ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed;
		}

		/// <summary>
		/// Handler for public transport walk duration changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_PtWalkDuration(object sender, EventArgs e)
		{
			journeyParameters.MaxWalkingTime = ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration ;
		}
		
		/// <summary>
		/// Handler for public transport walking speed changed event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void preferencesControl_PtWalkSpeed(object sender, EventArgs e)
		{
			journeyParameters.WalkingSpeed = ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed;
		}

	

		/// <summary>
		/// Saves current values entered by the user. Useful when doing a postback
		/// </summary>
		private void SaveCurrentSettings() 
		{
			journeyParameters.WalkingSpeed = ptPreferencesControl.WalkingSpeedOptionsControl.WalkingSpeed;
			journeyParameters.MaxWalkingTime =  ptPreferencesControl.WalkingSpeedOptionsControl.WalkingDuration;
			journeyParameters.InterchangeSpeed =  ptPreferencesControl.JourneyChangesOptionsControl.ChangesSpeed;
			journeyParameters.PublicAlgorithmType =  ptPreferencesControl.JourneyChangesOptionsControl.Changes;
			journeyParameters.PublicModes = transportTypesControl.PublicModes;

			//save user preferences if the user is logged in
			if (ptPreferencesControl.SavePreferences)
			{
				UserPreferencesHelper.SavePublicTransportPreferences(journeyParameters);
			}
		}


		/// <summary>
		/// Populates the outward  itinerary locations of the ViaLocationsControl
		/// </summary>
		/// <param name="control"></param>
		public void PopulateViaLocationsControl(ItineraryViasControl control)
		{			
			// Get itinerary data to populate the control
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			
			if (itineraryManager.Length > 0)
			{
				// Get an array of journeys in the itinerary			
				Journey[] outwardJourneys = itineraryManager.OutwardJourneyItinerary;
				// And returns
				Journey[] returnJourneys = itineraryManager.ReturnJourneyItinerary;
			

				// Set up a blank outward journey location array and then populate with the
				// outwardJourneys array
				TDLocation[] outwardLocations = new TDLocation[outwardJourneys.Length + 1];
				int i = 0;

				// Loop around the journeys and create a list of the outward locations
				foreach (Journey outwardJourney in outwardJourneys)
				{
					PublicJourney journey = outwardJourney as PublicJourney;
				

					if (outwardJourney.Type == TDJourneyType.RoadCongested)
					{
						outwardLocations[i] = itineraryManager.SpecificJourneyRequest(i).OriginLocation;

						if (i == outwardJourneys.Length - 1)
							outwardLocations[i + 1] = itineraryManager.SpecificJourneyRequest(i).DestinationLocation;
					}
					else if(journey != null)
					{
						outwardLocations[i] =  ((PublicJourneyDetail)journey.Details[0]).LegStart.Location;

						// Add the final destination to the locations array
						if (i == outwardJourneys.Length - 1)
						{						
							PublicJourneyDetail publicOutwardJourneyDetail =  journey.Details[journey.Details.Length - 1];
							outwardLocations[i + 1] = publicOutwardJourneyDetail.LegEnd.Location;
						}
					}

					i++;
				}

				// Repeat for return journeys if there are any
				// Return journey location array
				TDLocation[] returnLocations = null;
				if (returnJourneys.Length  > 0)
				{
					returnLocations = new TDLocation[returnJourneys.Length + 1];
					i = 0;
				
					foreach (Journey returnJourney in returnJourneys)
					{ 

						PublicJourney journey = returnJourney as PublicJourney;


						if (returnJourney.Type == TDJourneyType.RoadCongested)
						{
							returnLocations[i] = itineraryManager.SpecificJourneyRequest(i).OriginLocation;
				
							if (i == returnJourneys.Length - 1)
								returnLocations[i + 1] = itineraryManager.SpecificJourneyRequest(i).DestinationLocation;
						}
						else if (journey != null)
						{
							returnLocations[i] = ((PublicJourneyDetail)journey.Details[0]).LegStart.Location;
				
							// Add the final destination to the locations array
							if (i == returnJourneys.Length - 1)
							{
								PublicJourney publicReturnJourney = journey; 
								PublicJourneyDetail publicReturnJourneyDetail = journey.Details[publicReturnJourney.Details.Length - 1];
								returnLocations[i + 1] = publicReturnJourneyDetail.LegEnd.Location;
							}
						}
				
						i++;
					}
				}
		

				// Add the itinerary location arrays to the control
				control.OutwardViaLocations = outwardLocations;
				control.ReturnViaLocations = returnLocations;
			}
		}

		/// <summary>
		/// Handler for the origin location Map button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void originLocationControl_MapClick(object sender, EventArgs e)
		{
            bool shiftForm = false;
            pageState.MapType = CurrentLocationType.From;
            pageState.MapMode = CurrentMapMode.FromJourneyInput;

            //pageState.JourneyInputReturnStack.Push(pageId);

            if (journeyParameters.OriginLocation.Status == TDLocationStatus.Unspecified)
            {
                mapSearch(originSearch.InputText, originSearch.SearchType, originSearch.FuzzySearch, true, true);
                if (pageState.MapLocationSearch.InputText.Length == 0)
                    pageState.MapLocationControlType.Type = ControlType.Default;
                else if (pageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    originSearch = pageState.MapLocationSearch;
                    originLocation = pageState.MapLocation;
                    originSearch.SearchType = SearchType.Map;
                    journeyParameters.Origin = originSearch;
                    journeyParameters.OriginLocation = originLocation;
                    originLocationControl.Search(true);
                }
                else
                {
                    shiftForm = true;
                    pageState.MapLocationControlType.Type = ControlType.NoMatch;
                }
            }
            else
            {
                pageState.MapLocationSearch = journeyParameters.Origin;
                pageState.MapLocation = journeyParameters.OriginLocation;
            }

            if (shiftForm)
            {
                pageState.JourneyInputReturnStack.Push(TransitionEvent.ExtendJourneyInput);
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
		/// Handler for the destination location Map button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void destinationLocationControl_MapClick(object sender, EventArgs e)
		{

            bool shiftForm = false;
            pageState.MapType = CurrentLocationType.To;
            pageState.MapMode = CurrentMapMode.FromJourneyInput;
            destinationLocationControl.AmendMode = true;

            //pageState.JourneyInputReturnStack.Push(pageId);

            if (journeyParameters.DestinationLocation.Status == TDLocationStatus.Unspecified)
            {
                mapSearch(destinationSearch.InputText, destinationSearch.SearchType, destinationSearch.FuzzySearch, true, true);
                if (pageState.MapLocationSearch.InputText.Length == 0)
                    pageState.MapLocationControlType.Type = ControlType.Default;
                else if (pageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    destinationSearch = pageState.MapLocationSearch;
                    destinationLocation = pageState.MapLocation;
                    destinationSearch.SearchType = SearchType.Map;
                    journeyParameters.Destination = destinationSearch;
                    journeyParameters.DestinationLocation = destinationLocation;
                }
                else
                {
                    pageState.MapLocationControlType.Type = ControlType.NoMatch;
                    shiftForm = true;
                }
            }
            else
            {
                pageState.MapLocationSearch = journeyParameters.Destination;
                pageState.MapLocation = journeyParameters.DestinationLocation;
            }

            if (shiftForm)
            {
                pageState.JourneyInputReturnStack.Push(TransitionEvent.ExtendJourneyInput);
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
		/// Set up a new search using the map search and location objects
		/// (those in the input page state and not journey parameters) so that
		/// they do not overwrite the journey parameter search and location objects
		/// in case the location selected from the map is cancelled
		/// </summary>
		/// <param name="inputText">Input text</param>
		/// <param name="searchType">Search type</param>
		/// <param name="fuzzy">Fuzzy search</param>
		/// <param name="acceptsPostcode">Location can accept postcodes</param>
		/// <param name="acceptsPartPostcode">Location can accept partial postcodes</param>
		private void mapSearch(string inputText, SearchType searchType, bool fuzzy, bool acceptsPostcode, bool acceptsPartPostcode)
		{
			pageState.MapLocationSearch.ClearAll();
			pageState.MapLocation.Status = TDLocationStatus.Unspecified;
			pageState.MapLocationSearch.SearchType = GetDefaultSearchType(DataServiceType.VisitPlannerLocationSelection);

			LocationSearch thisSearch = pageState.MapLocationSearch;
			TDLocation thisLocation = pageState.MapLocation;

			LocationSearchHelper.SetupLocationParameters(
				inputText,
				searchType,
				fuzzy,
				0,
				journeyParameters.MaxWalkingTime,
				journeyParameters.WalkingSpeed,
				ref thisSearch,
				ref thisLocation,
				acceptsPostcode,
				acceptsPartPostcode,
				StationType.Undetermined
				);

			pageState.MapLocationSearch = thisSearch;
			pageState.MapLocation = thisLocation;
		}

		/// <summary>
		/// Gets the default location search type from DataServices. 
		/// </summary>
		/// <param name="listType">The drop-down type required</param>
		/// <returns>The default search type to be used</returns>
		private SearchType GetDefaultSearchType(DataServiceType listType)
		{
			DataServices.IDataServices ds = (DataServices.IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			string defaultItemValue = ds.GetDefaultListControlValue(listType);
			return (SearchType) (Enum.Parse(typeof(SearchType), defaultItemValue));
		}


		/// <summary>
		/// Handler for the origin location New Location button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void originLocationControl_NewLocation(object sender, EventArgs e)
		{
			originLocation = new TDLocation();
			journeyParameters.OriginLocation = originLocation;
			originSearch = new LocationSearch();
			journeyParameters.Origin = originSearch;
			journeyParameters.OriginType = new LocationSelectControlType(ControlType.NewLocation);
			originSearch.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);
			
			originLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.From, ref originSearch, ref originLocation, ref originLocationSelectControlType, false, true, false, Page.IsPostBack);
			pageState.AmbiguityMode = false;
		}

		/// <summary>
		/// Handler for the destination location New Location button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void destinationLocationControl_NewLocation(object sender, EventArgs e)
		{
			destinationLocation = new TDLocation();
			journeyParameters.DestinationLocation = destinationLocation;
			destinationSearch = new LocationSearch();
			journeyParameters.Destination = destinationSearch;
			journeyParameters.DestinationType = new LocationSelectControlType(ControlType.NewLocation);

			destinationSearch.SearchType = GetDefaultSearchType(DataServiceType.FromToDrop);
			
			destinationLocationControl.Populate(DataServiceType.FromToDrop, CurrentLocationType.To, ref destinationSearch, ref destinationLocation, ref destinationLocationSelectControlType, false, true, false, Page.IsPostBack);
			pageState.AmbiguityMode = false;
		}

		/// <summary>
		/// Handler for the Public Via location Map button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ViaLocationControl_MapClick(object sender, EventArgs e)
		{
			pageState.MapType = CurrentLocationType.PublicVia;
			pageState.MapMode = CurrentMapMode.FromJourneyInput;
			
			if (journeyParameters.PublicViaLocation.Status == TDLocationStatus.Unspecified )
			{
				mapSearch(publicViaSearch.InputText, publicViaSearch.SearchType, publicViaSearch.FuzzySearch, true, true);
				if (pageState.MapLocationSearch.InputText.Length == 0)
					pageState.MapLocationControlType.Type = ControlType.Default;
				else
					pageState.MapLocationControlType.Type = ControlType.NoMatch;
			} 
			else 
			{
				pageState.MapLocationSearch = journeyParameters.PublicVia;
				pageState.MapLocation = journeyParameters.PublicViaLocation;
			}

			pageState.JourneyInputReturnStack.Push(pageId);
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
		}

		/// <summary>
		/// Handler for the Private (Car) location via map button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TristateLocationControl_MapClick(object sender, EventArgs e)
		{
            bool shiftForm = false;

            pageState.MapType = CurrentLocationType.PrivateVia;
            pageState.MapMode = CurrentMapMode.FromJourneyInput;

            if (journeyParameters.PrivateViaLocation.Status == TDLocationStatus.Unspecified)
            {
                mapSearch(privateViaSearch.InputText, privateViaSearch.SearchType, privateViaSearch.FuzzySearch, true, true);
                if (pageState.MapLocationSearch.InputText.Length == 0)
                    pageState.MapLocationControlType.Type = ControlType.Default;
                else if (pageState.MapLocation.Status == TDLocationStatus.Valid)
                {
                    privateViaSearch = pageState.MapLocationSearch;
                    privateViaLocation = pageState.MapLocation;
                    privateViaSearch.SearchType = SearchType.Map;
                    journeyParameters.PrivateVia = privateViaSearch;
                    journeyParameters.PrivateViaLocation = privateViaLocation;
                }
                else
                {
                    pageState.MapLocationControlType.Type = ControlType.NoMatch;
                    shiftForm = true;
                }
            }
            else
            {
                pageState.MapLocationSearch = journeyParameters.PrivateVia;
                pageState.MapLocation = journeyParameters.PrivateViaLocation;
            }

            if (shiftForm)
            {
                pageState.JourneyInputReturnStack.Push(TransitionEvent.ExtendJourneyInput);
                //Set this property to tell the target page we are coming in from the page when findonmap button clicked			
                TDSessionManager.Current.SetOneUseKey(SessionKey.JourneyPlannerInputToMap, string.Empty);
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputToMap;
            }
            else
            {
                mapInputControl.Visible = true;
                mapModes.Add(MapLocationMode.Via);
            }
		}


		/// <summary>
		/// Intialises the stop over control depending on if should be displayed as
		/// readonly, editable or highlighting erroneous times.
		/// </summary>
		private void PopulateAmendAmbigStopOverControl() 
		{

				if (FindDateValidation.IsExtensionReturnOverlap || FindDateValidation.IsOutwardAndReturnExtensionStartInPast) 
				{
					this.amendStopoverControl.Initialise(this.PageId,false,true,true);
				} 
				else if (FindDateValidation.OutwardExtensionToStartInPast) 
				{
					this.amendStopoverControl.Initialise(this.PageId,false,true,false);
				} 
				else if (FindDateValidation.ReturnExtensionToStartInPast) 
				{
					this.amendStopoverControl.Initialise(this.PageId,false,false,true);
				} 
				else 
				{
					this.amendStopoverControl.Initialise(this.PageId,true,false,false);
				}
			
		}

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
		/// gets if no naptan error is raised
		/// </summary>
		private bool IsValidNaptan
		{
			get
			{
				if (errors.Contains (ValidationErrorID.OriginLocationHasNoNaptan)
					|| errors.Contains (ValidationErrorID.DestinationLocationHasNoNaptan))
					return false;
				else
					return true;
			}
		}

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

            LocationSearch locationSearch = pageState.MapLocationSearch;
            TDLocation location = pageState.MapLocation;

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
                if (!mapModes.Contains(MapLocationMode.Via))
                {
                    mapModes.Add(MapLocationMode.Via);
                }
            }
               
            mapInputControl.Initialise(searchType, locationsToShow, mapModes.ToArray());
  
            if (!mapInputControl.Visible && (TDSessionManager.Current.GetOneUseKey(SessionKey.JourneyPlannerInputToMap) != null))
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
            buttonBack.Click += new EventHandler(buttonBack_Click);
		}
		#endregion

		#region Properties

		/// <summary>
		/// Gets/Sets PrivateRequired depending on user's selection
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
		/// Gets/Sets PublicRequired bool depending on user's selection
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

		#endregion
		

	}	
}


