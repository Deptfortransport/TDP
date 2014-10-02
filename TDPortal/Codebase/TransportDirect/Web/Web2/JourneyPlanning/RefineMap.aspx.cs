// *********************************************** 
// NAME                 : RefineMap.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 30/01/2006
// DESCRIPTION			: A new page as part of the new Extension, Replan and Adjust pages. 
//                      Displays the Maps of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/RefineMap.aspx.cs-arc  $
//
//   Rev 1.8   Mar 29 2010 16:40:38   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.7   Dec 01 2009 11:50:30   mmodi
//Corrected problem of showing both maps when only an outward journey exists
//
//   Rev 1.6   Nov 29 2009 12:46:26   mmodi
//Updated map initialise to display the show journey buttons, and ensure only  one map is shown
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Nov 23 2009 10:35:44   mmodi
//Updated to use new mapping
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Jan 09 2009 13:36:30   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Nov 20 2008 14:41:04   pscott
//IR 5178 Change to ensure initial map display details are saved so they are available for printer friendly
//Resolution for 5178: Incorrect Journey shown on printable map when extending
//
//   Rev 1.2   Mar 31 2008 13:25:40   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Feb 08 2008 12:36:00 apatel
//  CCN 0427 Added left hand menu on the page
//
//   Rev 1.0   Nov 08 2007 13:31:14   mturner
//Initial revision.
//
//   Rev 1.7   Mar 24 2006 12:48:56   NMoorhouse
//Removed 'help' button from top of page
//Resolution for 3667: DN068 Replan:  Server Error when clicking Back button from replan results Map page Help
//
//   Rev 1.6   Mar 14 2006 11:43:20   NMoorhouse
//Post stream3353 merge: added new HeadElementControl, HeaderControls and reference new ResourceManager (also, added missing skiplink)
//
//   Rev 1.5   Mar 10 2006 12:28:06   pcross
//Updated to get "Add extension to journey" to work
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Mar 10 2006 09:51:26   NMoorhouse
//Fix a problem display maps of adjusted journeys
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Mar 06 2006 18:17:34   NMoorhouse
//Updated following FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Feb 14 2006 16:25:44   NMoorhouse
//Linking up of pages
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 09 2006 19:00:42   NMoorhouse
//Updates to make RefineMap screen consistent
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 09 2006 16:21:52   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Feb 07 2006 18:47:28   NMoorhouse
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//


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
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for RefineMap.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RefineMap : TDPage
    {
        #region Private members

        // HTML Controls

		// TD Web Controls
		protected TransportDirect.UserPortal.Web.Controls.JourneyBuilderControl addExtensionControl;

		private ITDSessionManager tdSessionManager;
		private TDItineraryManager itineraryManager;

		/// <summary>
		/// True if the Itinerary exists, containing the Initial journey and zero or more extensions
		/// </summary>
		private bool itineraryExists;

		/// <summary>
		/// True if an extension to an Itinerary is in the process of being planned and has not yet been added to the Itinerary
		/// </summary>
		private bool extendInProgress;

		// State of results
		/// <summary>
		///  True if there is an outward trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool outwardExists;

		/// <summary>
		///  True if there is a return trip for the current selection (Journey, Itinerary or Extension)
		/// </summary>
		private bool returnExists;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor for the Page
		/// </summary>
		public RefineMap() : base()
		{
			// Set page Id
			pageId = PageId.RefineMap;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
        }

        #endregion

        #region Event Handlers
        /// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			tdSessionManager = TDSessionManager.Current;
			itineraryManager = TDItineraryManager.Current;

			// Initialise text and button properties
			PopulateControls();	
		
			// See the one use keys to see if a selected leg has already been selected
			if (TDSessionManager.Current.GetOneUseKey(SessionKey.MapOutwardSelectedLeg) != null)
			{
				string temp = TDSessionManager.Current.GetOneUseKey(SessionKey.MapOutwardSelectedLeg);
                mapJourneyControlOutward.ShowSelectedLeg = Convert.ToInt32(temp, TDCultureInfo.CurrentCulture.NumberFormat);
				this.ScrollManager.RestPageAtElement(mapJourneyControlOutward.FirstElementId);
			}
			if (TDSessionManager.Current.GetOneUseKey(SessionKey.MapReturnSelectedLeg) != null)
			{
				string temp = TDSessionManager.Current.GetOneUseKey(SessionKey.MapReturnSelectedLeg);
				mapJourneyControlReturn.ShowSelectedLeg = Convert.ToInt32(temp, TDCultureInfo.CurrentCulture.NumberFormat);
				this.ScrollManager.RestPageAtElement(mapJourneyControlReturn.FirstElementId);
			}

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextRefineMap);
            expandableMenuControl.AddExpandedCategory("Related links");

		}

		/// <summary>
		/// Handles the page prerender event. This performs any last-minute updates to the controls
		/// that are displayed to the user
		/// </summary>
		protected override void OnPreRender(EventArgs e)
		{
			SetControlVisibility();

            SetPrintableControl();

			SetupSkipLinksAndScreenReaderText();

			base.OnPreRender(e);
		}

		/// <summary>
		/// Handles back button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backButton_Click(object sender, EventArgs e)
		{
			// Navigate back to appropriate page
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineDetailsBack;
		}

        /// <summary>
        /// Button click event when the Outward journey button is clicked on the map journey control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowOutwardJourneyButton_Click(object sender, EventArgs e)
        {
            // Hide the return journey map and show the outward journey map
            outwardPanel.Visible = true;
            returnPanel.Visible = false;

            // Commit selected view to session to ensure if they choose a different journey then
            // outward maps continue to be shown
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            viewState.OutwardMapSelected = true;
            viewState.ReturnMapSelected = false;

            // If the user has selected to show map on the details page, update the selected view
            if (viewState.ReturnShowMap)
            {
                viewState.OutwardShowMap = true;
                viewState.ReturnShowMap = false;
            }
        }

        /// <summary>
        /// Button click event when the Return journey button is clicked on the map journey control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShowReturnJourneyButton_Click(object sender, EventArgs e)
        {
            // Hide the outward journey map and show the return journey map
            outwardPanel.Visible = false;
            returnPanel.Visible = true;

            // Commit selected view to session to ensure if they choose a different journey then
            // return maps continue to be shown
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            viewState.OutwardMapSelected = false;
            viewState.ReturnMapSelected = true;

            // If the user has selected to show map on the details page, update the selected view
            if (viewState.OutwardShowMap)
            {
                viewState.OutwardShowMap = false;
                viewState.ReturnShowMap = true;
            }
        }

		#endregion

		#region Private Methods

		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
            this.PageTitle = GetResource("RefineMap.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			// Get correct resource strings for labels
			labelTitle.Text = GetResource("RefineMap.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("RefineMap.labelIntroductoryText.Text");

			// Get correct resource strings for buttons
			backButton.Text = GetResource("RefineMap.backButton.Text");

			addExtensionControl.ItineraryManager = itineraryManager;
		}

		/// <summary>
		/// Establish what mode the Itinerary Manager is in and whether we have any Return results
		/// </summary>
		private void DetermineStateOfResults()
		{
			itineraryExists = (itineraryManager.Length > 0);
			extendInProgress = itineraryManager.ExtendInProgress;
			bool showItinerary = (itineraryExists && !extendInProgress);

			if ( showItinerary )
			{
				outwardExists = (itineraryManager.OutwardLength > 0);
				returnExists = (itineraryManager.ReturnLength > 0);
			}
			else
			{
				//check for normal result
				ITDJourneyResult result = tdSessionManager.JourneyResult;
				if(result != null) 
				{
					outwardExists = ((result.OutwardPublicJourneyCount + result.OutwardRoadJourneyCount) > 0) && result.IsValid;
					returnExists = ((result.ReturnPublicJourneyCount + result.ReturnRoadJourneyCount) > 0) && result.IsValid;
				}
			}
		}

		/// <summary>
		/// Determines which controls should be visible
		/// </summary>
		private void SetControlVisibility()
		{
			DetermineStateOfResults();

			//Only show add extension button if an extend is in progress
			addExtensionControl.Visible = itineraryManager.ExtendInProgress;

            outwardPanel.Visible = false;
            returnPanel.Visible = false;

			if (outwardExists)
			{
                outwardPanel.Visible = true;
				ShowMapControl(mapJourneyControlOutward, true);
			}
			
			if (returnExists)
			{
                returnPanel.Visible = true;
				ShowMapControl(mapJourneyControlReturn, false);
			}
			
            // Only allow outward OR return map to be shown, not both because the new 
            // AJAX maps only work for one map on the screen
            if (outwardExists && returnExists)
            {
                // Check view state to determine which is the initial map to show
                TDJourneyViewState viewState = itineraryManager.JourneyViewState;

                // First time page is entered, set to show outward map
                if (!Page.IsPostBack)
                {
                    viewState.OutwardMapSelected = true;
                    viewState.ReturnMapSelected = false;
                }

                // Outward map should be shown in preference to return map
                outwardPanel.Visible = true;
                returnPanel.Visible = false;

                // Check which map was selected from Details page
                if (viewState.OutwardShowMap)
                {
                    // Enter here to prevent return map being set if both outward and return flags are true
                }
                else if ((viewState.ReturnShowMap) || (viewState.ReturnMapSelected))
                {
                    outwardPanel.Visible = false;
                    returnPanel.Visible = true;
                }

                // Commit selected view back to this pages viewstate
                viewState.OutwardMapSelected = outwardPanel.Visible;
                viewState.ReturnMapSelected = returnPanel.Visible;
            }
		}

		/// <summary>
		/// Handles the visual bits associated with showing the map control - ie makes it visible and updates the buttons
		/// </summary>
		/// <param name="?"></param>
		private void ShowMapControl(MapJourneyControl journeyMapControl, bool outward)
		{
			journeyMapControl.Visible = true;
            journeyMapControl.Initialise(outward, true, (outwardExists && returnExists));
		}

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{

			// Setup gif resource for images (1 invisible image for all skip links)
			string skipLinkImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.ImageUrl = skipLinkImageUrl;
			imageMainContentSkipLink1.AlternateText = GetResource("RefineDetails.imageMainContentSkipLink.AlternateText");

		}

        /// <summary>
        /// Sets up the printable control with the querystring params needed
        /// </summary>
        private void SetPrintableControl()
        {
            // Add the javascript to set the map viewstate on client side
            PrintableButtonHelper printHelper = null;

            if ((outwardExists) && (returnExists))
            {
                // Initialise for both outward and return maps
                printHelper = new PrintableButtonHelper(
                    mapJourneyControlOutward.MapId,
                    mapJourneyControlReturn.MapId,
                    mapJourneyControlOutward.MapSymbolsSelectId,
                    mapJourneyControlReturn.MapSymbolsSelectId,
                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId);
            }
            else if (outwardExists)
            {
                // Initialise only for outward map
                printHelper = new PrintableButtonHelper(
                    mapJourneyControlOutward.MapId,
                    string.Empty,
                    mapJourneyControlOutward.MapSymbolsSelectId,
                    string.Empty,
                    mapJourneyControlOutward.MapJourneyDisplayDetailsDropDownId,
                    string.Empty);
            }
            else if (returnExists)
            {
                // Initialise only for return map
                printHelper = new PrintableButtonHelper(
                    string.Empty,
                    mapJourneyControlReturn.MapId,
                    string.Empty,
                    mapJourneyControlReturn.MapSymbolsSelectId,
                    string.Empty,
                    mapJourneyControlReturn.MapJourneyDisplayDetailsDropDownId);
            }

            if (printHelper != null)
            {
                printerFriendlyControl.PrintButton.OnClientClick = printHelper.GetClientScript();
            }
        }

		#endregion

		#region Web Form Designer generated code
		/// <summary>
		/// Web Form Designer generated code
		/// </summary>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			ExtraWiringEvents();
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
		/// Sets up the necessary button event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{
			this.backButton.Click += new EventHandler(this.backButton_Click);

            mapJourneyControlOutward.ShowReturnJourneyButton.Click += new EventHandler(ShowReturnJourneyButton_Click);
            mapJourneyControlReturn.ShowOutwardJourneyButton.Click += new EventHandler(ShowOutwardJourneyButton_Click);
        }
		#endregion
	}
}