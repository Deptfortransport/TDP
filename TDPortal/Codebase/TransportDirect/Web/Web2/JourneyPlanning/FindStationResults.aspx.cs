// *********************************************** 
// NAME                 : FindStationResults.aspx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 11/05/2004 
// DESCRIPTION  : Page displaying results of found stations
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/FindStationResults.aspx.cs-arc  $ 
//
//   Rev 1.4   Mar 29 2010 16:39:26   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.3   Dec 17 2008 15:52:04   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:24:38   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Mar 03 2008 14:51:00 apatel
//  Removed top commandNext, commandFromTo and commandFrom buttons
// 
//  Rev DevFactory Mar 02 2008 21:09:00 apatel
//  Configured help button which moved in top right corner.
//
//  Rev DevFactory Feb 06 2008 08:48:00 apatel
//  Changed the layout of controls. moved help button to top right corner.
//
//   Rev 1.0   Nov 08 2007 13:29:38   mturner
//Initial revision.
//
//   Rev 1.27   Feb 23 2006 18:50:24   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.26   Feb 10 2006 15:09:12   build
//Automatically merged from branch for stream3180
//
//   Rev 1.25.2.1   Nov 30 2005 11:49:22   RGriffith
//Update of old way of getting a redirection URL
//
//   Rev 1.25.2.0   Nov 29 2005 18:43:52   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.25   Nov 03 2005 17:02:14   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.24.1.0   Oct 11 2005 12:14:54   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.26   Oct 06 2005 16:43:28   rGriffith
//Replaced printer button hyperlink/image to use the PrinterFriendlyPageButton control
//
//   Rev 1.25   Oct 06 2005 13:43:28   rGriffith
//Updated imagebuttons to become TDButton controls
//
//   Rev 1.24   Mar 14 2005 14:22:32   bflenk
//Functionality that was removed in error restored
//
//   Rev 1.23   Mar 08 2005 16:27:34   bflenk
//TimeOut functionality implemented in TDPage.cs, removed from this file - IR1720
//
//   Rev 1.22   Nov 26 2004 15:29:16   asinclair
//Fix for IR 1720 - Session timeout 
//
//   Rev 1.21   Aug 18 2004 14:19:26   passuied
//Use of non duplicated code to Get transitionevent from FindAMode mode
//
//   Rev 1.20   Aug 13 2004 16:57:08   passuied
//Set visibility of AmendSearch and New Search button in case no results are found
//Resolution for 1312: Find a coach - Amending nearest station displays blank 'Find nearest station' page
//
//   Rev 1.19   Aug 13 2004 14:29:08   passuied
//Changes for FindA station distinct error message
//
//   Rev 1.18   Aug 02 2004 15:36:00   jbroome
//IR 1252 - Tick All Check box retains value after postback.
//
//   Rev 1.17   Jul 29 2004 15:42:50   passuied
//updated Back button redirection for Del6.1
//
//   Rev 1.16   Jul 27 2004 14:03:16   passuied
//FindStation Del6.1 :Finalised version 
//
//   Rev 1.15   Jul 26 2004 20:23:58   passuied
//Changes to implement AmendSeach Functionality. Created and Amend mode in the tristate to enable the display of a valid location inside the locationUnspecified control.
//We send this mode when a one use session key has been set by a click on AmendSearch button.
//Also tweak in toFromLocationControl to display the to and from location/station controls correctly
//
//   Rev 1.14   Jul 23 2004 17:41:58   passuied
//FindStation 6.1. Labels and text updates
//
//   Rev 1.13   Jul 23 2004 11:48:24   passuied
//Changes to add GetResource Method in TDPage and TDUserControl to ease access to resources. Also removal of local GetResouce in controls and pages
//
//   Rev 1.12   Jul 21 2004 10:51:32   passuied
//Re work for integration with FindStation del6.1. Working. Needs work on resources
//
//   Rev 1.11   Jul 14 2004 16:36:26   passuied
//Changes for del6.1. FindFlight functionality working after SessionManager changes.
//
//   Rev 1.10   Jul 14 2004 13:00:28   passuied
//Changes in SessionManager with impact in Web for Del 6.1
//Compiles
//
//   Rev 1.9   Jul 12 2004 14:13:50   passuied
//use of new property Mode of FindPageState base class
//
//   Rev 1.8   Jun 30 2004 15:43:04   passuied
//Cleaning up
//
//   Rev 1.7   Jun 23 2004 15:57:38   passuied
//addition for Results message functionality
//
//   Rev 1.6   Jun 23 2004 11:23:06   passuied
//addition of help for findStation pages
//
//   Rev 1.5   Jun 10 2004 10:19:06   jgeorge
//Updated for changes to FindFlightPageState and TDJourneyParametersFlight
//
//   Rev 1.4   Jun 02 2004 16:40:24   passuied
//working version
//
//   Rev 1.3   May 28 2004 17:51:28   passuied
//update as part of FindStation development
//
//   Rev 1.2   May 21 2004 15:50:00   passuied
//partly working Find station pages and controls. Check in for backup
//
//   Rev 1.1   May 12 2004 17:47:26   passuied
//compiling check in for FindStation pages and related


using System;
using TransportDirect.Common.ResourceManager;
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
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Page displaying results of found stations
	/// </summary>
	public partial class FindStationResults : TDPage
	{
		#region Controls declaration
		protected FindStationResultsLocationControl stationResultsLocationControl;

		#endregion

		#region Resource keys declaration
		private const string RES_TITLE_NOTSTATIONMODE		= "FindStationResult.labelTitle.NotStationMode";
		private const string RES_TITLE_STATIONMODE_NOVALID  = "FindStationResult.labelTitle.StationMode.Valid";
		private const string RES_TITLE_STATIONMODE_VALID	= "FindStationResult.labelTitle.StationMode.Valid"; // same as valid		
		private const string RES_NOTE_NOTSTATIONMODE		= "FindStationResult.labelNote.NotStationMode";
		private const string RES_NOTE_STATIONMODE_NOVALID	= "FindStationResult.labelNote.StationMode.Valid";
		private const string RES_NOTE_STATIONMODE_VALID		= "FindStationResult.labelNote.NotStationMode"; // same as not station mode

		// Redirection url removed from langstrings to be a constant in this file
		private const string HEADER_FINDASTATION_URL_PARAM	= "?NotFindAMode=true";
		
		public const string RES_ERRORMESSAGE_STATIONMODE	= "FindStation.ErrorMessage.StationMode";
		public const string RES_ERRORMESSAGE_FLIGHT			= "FindStation.ErrorMessage.Flight";
		public const string RES_ERRORMESSAGE_TRAINCOACH		= "FindStation.ErrorMessage.TrainCoach";


		
		#endregion

		#region Private variables declaration
		private FindStationPageState stationPageState;
		private FindPageState pageState;


		#endregion

		#region Constructor and Page_Load
		public FindStationResults()
		{
			pageId = PageId.FindStationResults;
		}
		protected void Page_Load(object sender, System.EventArgs e)
		{

			stationPageState = TDSessionManager.Current.FindStationPageState;
			pageState = TDSessionManager.Current.FindPageState;

			if  (Session.IsNewSession)
			{
				// Get the baseChannel URL
				string channelName = getBaseChannelURL(TDPage.SessionChannelName);
				// Get the currentpage URL and add the specific constant required.
				IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];
				PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(PageId.FindStationInput);
				string url = pageTransferDetails.PageUrl + HEADER_FINDASTATION_URL_PARAM;
				// Redirect to the new url
				Response.Redirect(channelName + url);			
			}

			LoadResources();
			SetControlVisibility();


            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuFindAPlace);
            //CCN 0427 Related links
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextFindStationResults);
            expandableMenuControl.AddExpandedCategory("Related links");

		}

        protected void Page_PreRender(object sender, EventArgs e)
        {
            errormsg.Visible = labelMessage.Visible;
        }
		#endregion

		#region Private methods

		/// <summary>
		/// Set visibility of controls in page
		/// </summary>
		private void SetControlVisibility()
		{
            

			// in case there is no station returned or not all searched station types are found, leave only buttons to go back and New Location
			// and display error message
			if (stationPageState.CurrentLocation.NaPTANs.Length == 0
				|| stationPageState.StationTypes.Length == 0
				|| stationPageState.NotAllStationTypesFound)
			{
				
				commandTravelFrom2.Visible = false;
				commandTravelTo2.Visible = false;
				
				commandNext2.Visible = false;
				commandShowMap.Visible = false;
				labelNote.Visible = false;
				// display error message
				labelMessage.Visible = true;

				// New search and Amend button are not displayed when no result found
				commandNewSearch.Visible = false;
				commandAmendSearch.Visible = false;

			}
			else
			{
				switch (pageState.Mode)
				{
					case FindAMode.Station:
						// New search and Amend button are displayed when in Station mode only!
						commandNewSearch.Visible = true;
						commandAmendSearch.Visible = true;
						// if no location defined, show travel from / Travel to
						if (stationPageState.LocationFrom.Status != TDLocationStatus.Valid
							&& stationPageState.LocationTo.Status != TDLocationStatus.Valid)
						{
							
							commandTravelFrom2.Visible = true;
							commandTravelTo2.Visible = true;
							
							commandNext2.Visible = false;

						}
						// if one location defined, Next is displayed
						else
						{
							
							commandTravelFrom2.Visible = false;
							commandTravelTo2.Visible = false;
							
							commandNext2.Visible = true;
						}
						break;
					default:
						
						commandTravelFrom2.Visible = false;
						commandTravelTo2.Visible = false;
						
						commandNext2.Visible = true;
						commandNewSearch.Visible = false;
						commandAmendSearch.Visible = false;
						break;
				}
				
			}
			
		}

		
		/// <summary>
		/// Load page resources
		/// </summary>
		private void LoadResources()
		{
            //CCN 0427 Set up the help button at top right 
            helpIconSelect.HelpLabelControl = stationResultsLocationControl.HelpLabel;
			SetText();
		}

		/// <summary>
		/// Set Text for controls in page
		/// </summary>
		private void SetText()
		{
            this.PageTitle = GetResource("FindStationResults.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			#region Static Setting

			commandBack.Text = 
				GetResource("FindStationMap.commandBack.Text");
			
			commandNext2.Text = 
				GetResource("FindStationMap.commandNext2.Text");
			
			commandTravelFrom2.Text = 
				GetResource("FindStationMap.commandTravelFrom2.Text");

			
			commandTravelTo2.Text = 
				GetResource("FindStationMap.commandTravelTo2.Text");
			commandShowMap.Text = 
				GetResource("FindStationMap.commandShowMap.Text");
			
			commandNewSearch.Text = 
				GetResource("FindStationMap.commandNewSearch.Text");
			commandAmendSearch.Text = 
				GetResource("FindStationMap.commandAmendSearch.Text");

			#endregion

			if (pageState.Mode != FindAMode.Station)
			{
				SetTextNotStationMode();
			}
			else
			{
				SetTextStationMode();
			}

            //CCN 0427 - moved help button to top right corner
            // Setting alternate text for the help button depending on the FindAMode value
            switch (pageState.Mode)
            {
                case FindAMode.Train:
                    helpIconSelect.AlternateText = GetResource("FindStationResultsTrain.AlternateText");
                    break;
                case FindAMode.Flight:
                    //set alt tag for help button
                    helpIconSelect.AlternateText = GetResource("FindStationResultsFlight.AlternateText");
                    break;
                case FindAMode.Coach:
                    //set alt tag for help button
                    helpIconSelect.AlternateText = GetResource("FindStationResultsCoach.AlternateText");
                    break;
                default:
                    //set alt tag for help button
                    helpIconSelect.AlternateText = GetResource("FindStationResults.AlternateText");
                    break;
            }
		}

		

		private void SetTextNotStationMode()
		{

			string sStationType = FindStationHelper.GetStationTypeString();
			string sDirection = FindStationHelper.GetDirectionString();

			// Set Text for TITLE
			labelResultsTableTitle.Text = string.Format(
				GetResource(RES_TITLE_NOTSTATIONMODE), sStationType, sDirection);

			// Set Text for Note
			labelNote.Text = string.Format(
				GetResource(RES_NOTE_NOTSTATIONMODE), sStationType, sDirection);

			switch (pageState.Mode)
			{
				case FindAMode.Flight:
					labelMessage.Text = GetResource(RES_ERRORMESSAGE_FLIGHT);
					break;
				case FindAMode.Coach:
					labelMessage.Text = string.Format(
						GetResource(RES_ERRORMESSAGE_TRAINCOACH), GetResource(FindStationHelper.RES_COACH));
					break;
				case FindAMode.Train:
					labelMessage.Text = string.Format(
						GetResource(RES_ERRORMESSAGE_TRAINCOACH), GetResource(FindStationHelper.RES_RAIL));
					break;

			}

		}

		private void SetTextStationMode()
		{
			string sStationType = FindStationHelper.GetStationTypeString();

			// different if no location are valid or if one is valid

			// if one of them is valid
			if (	stationPageState.LocationFrom.Status == TDLocationStatus.Valid
				||	stationPageState.LocationTo.Status == TDLocationStatus.Valid)
			{
				// get the direction string
				string sDirection = FindStationHelper.GetDirectionString();

				labelResultsTableTitle.Text = string.Format(
					GetResource(RES_TITLE_STATIONMODE_VALID), sStationType, sDirection);
				labelNote.Text = string.Format(
					GetResource(RES_NOTE_STATIONMODE_VALID), sStationType, sDirection);

			}
			else
			{
				labelResultsTableTitle.Text = string.Format(
					GetResource(RES_TITLE_STATIONMODE_NOVALID), sStationType);
				labelNote.Text = GetResource(RES_NOTE_STATIONMODE_NOVALID);
			}

			labelMessage.Text = GetResource(RES_ERRORMESSAGE_STATIONMODE);
	
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			AddExtraWireEvent();
			InitializeComponent();
			base.OnInit(e);
		}

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void AddExtraWireEvent()
		{
			// Setting up the Button Event handlers
			this.commandBack.Click += new EventHandler(this.CommandBackClick);
			
			this.commandShowMap.Click += new EventHandler(this.CommandShowMapClick);
			this.commandNext2.Click += new EventHandler(this.CommandNextClick);
			this.commandTravelFrom2.Click += new EventHandler(this.CommandTravelFromClick);
			this.commandTravelTo2.Click += new EventHandler(this.CommandTravelToClick);
			this.commandNewSearch.Click += new EventHandler(this.CommandNewSearchClick);
			this.commandAmendSearch.Click += new EventHandler(this.CommandAmendSearchClick);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ID = "FindStationResults";

		}
		#endregion

		#region Event handler methods
		/// <summary>
		/// Click event for the ShowMap button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandShowMapClick(object sender, EventArgs e)
		{
			stationPageState.IsShowingHidingMap = true;
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationResultsShowMap;
		}

		/// <summary>
		/// click event for the TravelFrom button
		/// This button is displayed for FindStation Mode. When no location is valid
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandTravelFromClick(object sender, EventArgs e)
		{
				FindStationHelper.ResultsTravelFrom();
		}

		/// <summary>
		/// click event for the TravelTo button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandTravelToClick(object sender, EventArgs e)
		{
			FindStationHelper.ResultsTravelTo();
		}

		/// <summary>
		/// Click event for the next button. 
		/// This button is displayed when in FindXXXInput or FindStation mode
		/// when a location has already been chosen
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandNextClick(object sender, EventArgs e)
		{
			FindStationHelper.ResultsNext();
 		}

		/// <summary>
		/// Click event for the back button
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		private void CommandBackClick(object sender, EventArgs e)
		{
			if (pageState.Mode == FindAMode.Station)
				stationPageState.InstateAmendMode();

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = FindInputAdapter.GetTransitionEventFromMode(pageState.Mode);
		}

		#endregion

		/// <summary>
		/// Click event for the NewSearch button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommandNewSearchClick(object sender, EventArgs e)
		{	
		
			stationPageState.Initialise();
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;

		}
		
		/// <summary>
		/// Click event for the AmendSearch button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CommandAmendSearchClick(object sender, EventArgs e)
		{
			// Clear the searches and locations in session then set the AmendKey in session
			// to indicate to the FindStationInput page not to initialise it
			stationPageState.InstateAmendMode();

			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationInputDefault;
		}
	}
}
