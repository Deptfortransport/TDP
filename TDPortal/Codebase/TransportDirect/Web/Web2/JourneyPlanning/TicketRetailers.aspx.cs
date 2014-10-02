// *********************************************** 
// NAME                 : TicketRetailers.aspx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 08.10.03 
// DESCRIPTION			: Page displaying ticket retailers 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/TicketRetailers.aspx.cs-arc  $
//
//   Rev 1.4   Mar 29 2010 16:40:40   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.3   Mar 22 2010 15:56:24   apatel
//updated UpdateFaresHeader method to check if the outward journey is not public and set headerd to come from return journey in such case.
//Resolution for 5473: Error message when click Buy tickets on mix PT/Car return journey
//
//   Rev 1.2   Mar 31 2008 13:25:46   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:48   mturner
//Initial revision.
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.61   Jun 21 2006 17:16:10   rphilpott
//Clear out retailer lists stored in session when selected ticket changes. Also fix minor out/return bug. 
//Resolution for 4123: Retailer Handoff -- selection of wrong retailer
//
//   Rev 1.60   Apr 29 2006 19:39:00   RPhilpott
//Store retailer list to avoid affects of randomisation.
//Resolution for 4036: DD075: Mismatch of retailer selected in Find Cheaper
//
//   Rev 1.59   Feb 24 2006 10:17:34   rgreenwood
//stream3129: Manual merge changes
//
//   Rev 1.58   Feb 10 2006 11:12:52   aviitanen
//Manual merge for Homepage phase 2 (stream3180)
//
//   Rev 1.57   Nov 30 2005 17:58:02   rhopkins
//Corrections to the button alignments
//Resolution for 3216: UEE: Javascript disabled - Printer friendly button uses flat style
//Resolution for 3242: UEE: Netscape - buttons overlap with journey summary on results page
//
//   Rev 1.56   Nov 24 2005 12:15:02   mguney
//ReturnJourneyDate property is set when there is a combined return journey so that TicketMatrix control can display the appropriate dates.
//Resolution for 3140: DN040 (CG): Ticket retailers page date label
//
//   Rev 1.55   Nov 10 2005 17:09:52   rgreenwood
//TD089 ES020 Changed TD button naming convention
//
//   Rev 1.54   Nov 03 2005 16:03:30   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.53.1.0   Oct 12 2005 15:58:56   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.53   Apr 28 2005 14:22:30   jgeorge
//Updates to way cost based results are handled, and correction to code which populates header block to get journey results from the TDItineraryManager rather than TDSessionManager
//Resolution for 2309: PT - Train - Destination wrong on rail ticket description.
//
//   Rev 1.52   Apr 19 2005 16:31:24   pcross
//Correction to skip links after unit testing
//
//   Rev 1.51   Apr 16 2005 12:06:42   jgeorge
//Update to avoid errors when user has already done one cost based search and is now viewing tickets for a second set of search results.
//Resolution for 2074: PT: Discount cards not displayed correctly on Ticket Retailers page after cost based search
//
//   Rev 1.50   Apr 13 2005 19:29:06   pcross
//Extra logic to only show ticket retailers skip links when a ticket has definitely been selected
//
//   Rev 1.49   Apr 13 2005 15:20:32   pcross
//No longer using GetResource as we need details from the langStrings file and the default for this page is the FaresAndTickets resource file.
//
//   Rev 1.48   Apr 12 2005 18:46:28   pcross
//Changed the way that the skip links image URL is accessed (now from langStrings, not HTML)
//
//   Rev 1.47   Apr 12 2005 14:41:06   pcross
//Corrected langStrings call to use GetResource for Skip Links work
//
//   Rev 1.46   Apr 11 2005 11:10:38   jgeorge
//Corrections for handling find a fare results
//Resolution for 2073: PT: Ticket Retailers page does not work correctly with Find a Fare
//
//   Rev 1.45   Apr 04 2005 18:05:54   pcross
//Updated skip links
//
//   Rev 1.44   Mar 30 2005 15:42:08   jgeorge
//Integration with cost based search
//
//   Rev 1.43   Mar 29 2005 13:40:42   rgeraghty
//Added code to back button for ensuring correct tickets are selected on the journey fares page
//
//   Rev 1.42   Mar 24 2005 14:57:24   pcross
//Added skip links
//
//   Rev 1.41   Mar 22 2005 08:53:32   jgeorge
//FxCop changes
//
//   Rev 1.40   Mar 08 2005 16:56:58   jgeorge
//Updates after QA	
//
//   Rev 1.39   Mar 04 2005 09:32:52   jgeorge
//Updated after redesign
//
//   Rev 1.38   Feb 22 2005 17:29:38   jgeorge
//Interim check-in
//
//   Rev 1.37   Feb 16 2005 16:39:48   jgeorge
//Interim check in
//
//   Rev 1.36   Sep 20 2004 16:45:18   COwczarek
//Page title now displayed using JourneyPlannerOutputTitleControl.
//Resolution for 1505: Extend Journey interface in relation to the Outward itinerary
//
//   Rev 1.35   Sep 19 2004 15:04:22   jbroome
//IR1391 - visibility of Add Extension to Journey button if incomplete results returned.
//
//   Rev 1.34   Sep 17 2004 12:12:52   jbroome
//IR 1591 - Extend Journey Usability Changes
//
//   Rev 1.33   Aug 31 2004 15:06:52   RHopkins
//IR1440 Rationalised the code for determining the state of the result data.  This improved code has been duplicated across all of the results pages to reduce the burden of future maintenance.
//
//   Rev 1.32   Aug 27 2004 14:37:22   COwczarek
//Fixes to show correct header control
//Resolution for 1430: Journey summary page, once ticket retailer is selected, the next actions header becomes 'Find a ...'
//
//   Rev 1.31   Aug 23 2004 11:08:36   jgeorge
//IR1319
//
//   Rev 1.30   Aug 10 2004 15:45:04   JHaydock
//Update of display of correct header control for help pages.
//
//   Rev 1.29   Jul 23 2004 12:21:46   jgeorge
//Find a... updates
//
//   Rev 1.28   Jul 22 2004 14:22:00   RHopkins
//IR1113 The "Amend date/time" anchor link now varies its text depending upon whether the AmendSaveSend control is displaying "Amend date and time" or "Amend stopover time" or not displaying either.
//
//   Rev 1.27   Jul 02 2004 15:07:30   RHopkins
//Corrected to work with ItineraryManager (including printable pages)
//
//   Rev 1.26   Jun 22 2004 13:39:30   jmorrissey
//Now redisplays page correctly when changing between Itinerary/Extension/Normal.
//
//   Rev 1.24   Jun 17 2004 21:24:52   RHopkins
//Corrected handling of Itinerary journeys
//
//   Rev 1.23   Jun 14 2004 18:25:42   esevern
//added extend journey funcationality
//
//   Rev 1.22   Apr 28 2004 16:20:26   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.21   Apr 02 2004 10:16:34   AWindley
//DEL 5.2 QA Changes: Resolution for 692
//
//   Rev 1.20   Mar 01 2004 13:47:06   asinclair
//Removed Printer hyperlink as not needed in Del 5.2
//
//   Rev 1.19   Nov 21 2003 11:43:02   COwczarek
//No change.
//Resolution for 295: Internationalisation issues with Ticket Retailers page
//
//   Rev 1.18   Nov 18 2003 16:00:10   COwczarek
//SCR#247 : Complete adding comments to existing code and add $Log: for PVCS history

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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Page displaying ticket retailers
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class TicketRetailers : TDPage
	{
		#region Control declaration

		protected System.Web.UI.WebControls.Button forcePostback;


		protected HeaderControl headerControl;
		protected AmendSaveSendControl AmendSaveSendControl;
		protected JourneyChangeSearchControl theJourneyChangeSearchControl;
		protected JourneyFareHeadingControl journeyFareHeadingControl;
		protected TicketMatrixControl outwardTickets;
		protected RetailerMatrixControl outwardRetailers;
		protected TicketMatrixControl inwardTickets;
		protected RetailerMatrixControl inwardRetailers;
		
		#endregion

		#region Private members

		private TicketRetailersHelper helper;
		private bool outwardExists;
		private bool returnExists;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor. Sets PageId and LocalResourceManager
		/// </summary>
		public TicketRetailers() : base()
		{
			LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
			pageId = PageId.TicketRetailers;

		}
        
		#endregion
		
		#region Event handlers

		/// <summary>
		/// Page Init event handler. Sets up additional event handlers
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			outwardTickets.SelectedRetailUnitChanged += new EventHandler(TicketMatrixControl_SelectedRetailUnitChanged);
			outwardTickets.PeopleTravellingControl.PeopleNumbersChanged += new EventHandler(PeopleTravellingControl_PeopleNumbersChanged);

			inwardTickets.SelectedRetailUnitChanged += new EventHandler(TicketMatrixControl_SelectedRetailUnitChanged);
			inwardTickets.PeopleTravellingControl.PeopleNumbersChanged += new EventHandler(PeopleTravellingControl_PeopleNumbersChanged);

			outwardRetailers.RetailerSelected += new RetailerSelectedEventHandler(retailerMatrix_RetailerSelected);
			inwardRetailers.RetailerSelected += new RetailerSelectedEventHandler(retailerMatrix_RetailerSelected);
		}

		/// <summary>
		/// Page load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event parameters</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			helper = new TicketRetailersHelper(resourceManager);

            
			ITDSessionManager sessionManager = TDSessionManager.Current;

			if (!Page.IsPostBack && TicketRetailersHelper.IsCostBasedSearch)
			{
				// If coming from the summary page, we may need to update ticketretailerinfo
				// Get the current PricingRetailOptionsState
				if (sessionManager.GetOneUseKey(SessionKey.FindFareBuySingleOrReturn) != null)
					helper.GetPricingRetailOptionsState( FindFareBuyOption.SingleOrReturn );
				else if (sessionManager.GetOneUseKey(SessionKey.FindFareBuyOutwardSingle) != null)
					helper.GetPricingRetailOptionsState( FindFareBuyOption.OutwardSingle );
				else if (sessionManager.GetOneUseKey(SessionKey.FindFareBuyReturnSingle) != null)
					helper.GetPricingRetailOptionsState( FindFareBuyOption.ReturnSingle );
				else if (sessionManager.GetOneUseKey(SessionKey.FindFareBuyBothSingle) != null)
					helper.GetPricingRetailOptionsState( FindFareBuyOption.BothSingle );
			}

            this.PageTitle = GetResource("JourneyPlanner.TicketRetailersHandOffPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			SetupControls();
			InitialiseStaticControls();
			UpdateTicketMatrixControls();

            //Added for white labelling:
            ConfigureLeftMenu("TicketRetailers.clientLink.BookmarkTitle", "TicketRetailers.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);

            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextTicketRetailers);
            expandableMenuControl.AddExpandedCategory("Related links");
            
		}

		/// <summary>
		/// Pre-render event handler. This method is responsible for displaying retailers
		/// for a new journey selection. It is only at this point in the page processing
		/// that we know if a new journey selection has been made. This is because event 
		/// handlers of the journey selection control will have been called by the time this
		/// method is called and will have updated the necessary session data.
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, System.EventArgs e) 
		{
			UpdateFaresHeader();

			PricingRetailOptionsState options = helper.GetPricingRetailOptionsState();
			UpdateTicketMatrixControls();

			UpdatePeopleTravellingControl();
			
			SetupSkipLinksAndScreenReaderText();

		}

		/// <summary>
		/// Handles the user changing the selected RetailUnit from outward and return
		/// ticket matrix controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void TicketMatrixControl_SelectedRetailUnitChanged(object sender, System.EventArgs e)
		{
			// Identify whether we are dealing with the outward or return control
			PricingRetailOptionsState options = helper.GetPricingRetailOptionsState();

			if (sender == outwardTickets)
			{
				options.SelectedOutwardRetailUnit = outwardTickets.SelectedRetailUnit;
			}
			else
			{
				options.SelectedReturnRetailUnit = inwardTickets.SelectedRetailUnit;
			}

			// clear out retailer lists stored in session to force RetailerMatrixControl to reload them ...

			InputPageState pageState = TDSessionManager.Current.InputPageState;

			pageState.OutwardOnlineRetailers  = null;
			pageState.OutwardOfflineRetailers = null;
			pageState.ReturnOnlineRetailers   = null;
			pageState.ReturnOfflineRetailers  = null;
		}

		/// <summary>
		/// Handles user changing the number of people travelling
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void PeopleTravellingControl_PeopleNumbersChanged(object sender, System.EventArgs e)
		{
			PricingRetailOptionsState options = helper.GetPricingRetailOptionsState();
			PeopleTravellingControl theSender = sender as PeopleTravellingControl;
			if (theSender.Visible)
			{
				options.AdultPassengers = theSender.Adults;
				options.ChildPassengers = theSender.Children;
			}
		}

		/// <summary>
		/// Handles the retailer selected event for both retailer list controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void retailerMatrix_RetailerSelected(object sender, RetailerSelectedEventArgs e)
		{
			if (ActivePeopleTravellingControl.IsValid)
			{
				PricingRetailOptionsState options = helper.GetPricingRetailOptionsState();
				options.LastRetailerSelectionIsForReturn = sender.Equals(inwardRetailers);
				options.LastRetailerSelection = e.SelectedRetailer;

				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RetailerHandoff;
			}
		}

        /// <summary>
        /// Handler for the back button. User is directed back to either the
        /// time based Tickets/Costs page or the cost based ...?
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (TicketRetailersHelper.IsCostBasedSearch)
            {
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneySummary;
            }
            else
            {
                PricingRetailOptionsState options = TDItineraryManager.Current.PricingRetailOptions;
                options.LeaveTicketDisplay = true; //this flag is used by the JourneyFares page for re-displaying the tickets selected
                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyFares;
            }
        }

		#endregion 

		#region Private Methods

		/// <summary>
		/// Loads data into the matrix controls and sets visibilities.
		/// </summary>
		private void UpdateTicketMatrixControls()
		{
			PricingRetailOptionsState options = helper.GetPricingRetailOptionsState();
			outwardExists = (options.OutwardTicketRetailerInfo != null) && (options.OutwardTicketRetailerInfo.RetailUnits.Length != 0);
			returnExists = (options.ReturnTicketRetailerInfo != null) && (options.ReturnTicketRetailerInfo.RetailUnits.Length != 0);

			if (outwardExists)
			{
				panelOutward.Visible = true;
				outwardTickets.Data = options.OutwardTicketRetailerInfo;
				outwardTickets.Discounts = options.Discounts;
				outwardTickets.ShowPeopleTravellingControl = true;
				outwardTickets.SelectedRetailUnit = options.SelectedOutwardRetailUnit;

				outwardRetailerPanel.Visible = options.SelectedOutwardRetailUnit != null;
				outwardRetailers.RetailUnit = options.SelectedOutwardRetailUnit;
				outwardRetailers.IsReturn = false;
			}
			else
			{
				panelOutward.Visible = false;
			}

			if (returnExists)
			{
				panelInward.Visible = true;
				inwardTickets.Data = options.ReturnTicketRetailerInfo;
				inwardTickets.Discounts = options.Discounts;
				inwardTickets.ShowPeopleTravellingControl = !panelOutward.Visible;
				inwardTickets.SelectedRetailUnit = options.SelectedReturnRetailUnit;

				inwardRetailerPanel.Visible = options.SelectedReturnRetailUnit != null;
				inwardRetailers.RetailUnit = options.SelectedReturnRetailUnit;
				inwardRetailers.IsReturn = true;
			}
			else
			{
				panelInward.Visible = false;
			}

		}

		/// <summary>
		/// Updates the current PeopleTravellingControl from the session data
		/// </summary>
		private void UpdatePeopleTravellingControl()
		{
			PricingRetailOptionsState options = helper.GetPricingRetailOptionsState();
			PeopleTravellingControl control = ActivePeopleTravellingControl;

			if (control != null)
			{
				control.Adults = options.AdultPassengers;
				control.Children = options.ChildPassengers;
			}
		}

		/// <summary>
		/// Returns the current PeopleTravellingControl. Only one of those from the outward and inward
		/// matrix controls will be visible
		/// </summary>
		private PeopleTravellingControl ActivePeopleTravellingControl
		{
			get 
			{
				if (panelOutward.Visible)
					return outwardTickets.PeopleTravellingControl;
				else
					return inwardTickets.PeopleTravellingControl;
			}
		}

		/// <summary>
		/// Initialises those page controls that require initialisation only on initial page load.
		/// It should not be necessary to call this method on post back requests.
		/// </summary>
		private void InitialiseStaticControls()
		{
			// Initialise static labels, hypertext text and image button Urls 
			// from Resource Mangager
			outwardTicketNotes.Text = GetResource("TicketRetailers.TicketNotes");
			outwardRetailerNotes.Text = GetResource("TicketRetailers.RetailerNotes");
			inwardRetailerNotes.Text = GetResource("TicketRetailers.RetailerNotes");
		}

		/// <summary>
		/// Set the visibility and data sources for the controls
		/// </summary>
		private void SetupControls()
		{
			AmendSaveSendControl.Initialise( this.pageId );

            if (TicketRetailersHelper.IsCostBasedSearch)
            {
                SetUpStepsControl();

                panelFindFareSteps.Visible = true;
            }
            else
            {
                panelFindFareSteps.Visible = false;
            }

            // Back button
            this.theJourneyChangeSearchControl.GenericBackButtonVisible = true;
		}

        /// <summary>
        /// Sets up the mode for the FindFareStepsConrol
        /// </summary>
        private void SetUpStepsControl()
        {
            findFareStepsControl.Visible = true;
            findFareStepsControl.Step = FindFareStepsControl.FindFareStep.FindFareStep4;
            findFareStepsControl.SessionManager = TDSessionManager.Current;
            findFareStepsControl.PageState = TDSessionManager.Current.FindPageState;
        }

		/// <summary>
		/// Populates the journey details from the session
		/// </summary>
		private void UpdateFaresHeader()
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			
			// Get the current journey
			PublicJourney journey = itineraryManager.JourneyResult.OutwardPublicJourney(itineraryManager.JourneyViewState.SelectedOutwardJourneyID);

            if (journey != null) // Check if journey is not null - May be outward journey is private not public
            {
                journeyFareHeadingControl.OriginLocation = journey.Details[0].LegStart.Location.Description;
                journeyFareHeadingControl.DestinationLocation = journey.Details[journey.Details.Length - 1].LegEnd.Location.Description;
            }
            else // we not got public outward journey check if we can get fares for return journey
            {
                journey = itineraryManager.JourneyResult.ReturnPublicJourney(itineraryManager.JourneyViewState.SelectedReturnJourneyID);
                if (journey != null) // Check if journey is not null
                {
                    journeyFareHeadingControl.OriginLocation = journey.Details[0].LegStart.Location.Description;
                    journeyFareHeadingControl.DestinationLocation = journey.Details[journey.Details.Length - 1].LegEnd.Location.Description;
                }
            }

			IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			PricingRetailOptionsState pricingOptions = helper.GetPricingRetailOptionsState();

            if (pricingOptions != null)
            {
                journeyFareHeadingControl.CoachCardName = ds.GetText(DataServiceType.DiscountCoachCardDrop, pricingOptions.Discounts.CoachDiscount);
                journeyFareHeadingControl.RailCardName = ds.GetText(DataServiceType.DiscountRailCardDrop, pricingOptions.Discounts.RailDiscount);
            }
		}

		/// <summary>
		/// Sets the text for the skip to links (for screenreader browsers).
		/// Handles visibility of links according to status of screen (eg whether return journeys exist)
		/// </summary>
		private void SetupSkipLinksAndScreenReaderText()
		{

			// Setup gif resource for images (1 invisible image for all skip links)
			string SkipLinkImageUrl = Global.tdResourceManager.GetString("SkipLinks.InvisibleImage.ImageUrl",TDCultureInfo.CurrentUICulture);
			imageMainContentSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageOutwardJourneyTicketsSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageOutwardJourneyRetailersSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageReturnJourneyTicketsSkipLink1.ImageUrl = SkipLinkImageUrl;
			imageReturnJourneyRetailersSkipLink1.ImageUrl = SkipLinkImageUrl;

			imageMainContentSkipLink1.AlternateText = 
				Global.tdResourceManager.GetString("TicketRetailers.imageMainContentSkipLink.AlternateText",TDCultureInfo.CurrentUICulture);

			// Show skip links only if appropriate results shown (outward, return journeys)
			PricingRetailOptionsState options = helper.GetPricingRetailOptionsState();
			if (outwardExists)
			{

				// 1st check if this is a return journey (ie return selected in original travel request, and fare covers outward and inward journeys)
				if(options.OutwardTicketRetailerInfo.ItineraryType == ItineraryType.Return)
				{
					// Return journey type uses outward panel but the text needs to refer to "return", not "single (outward)"
					panelOutwardJourneyTicketsSkipLink1.Visible=true;
					imageOutwardJourneyTicketsSkipLink1.AlternateText = 
						Global.tdResourceManager.GetString("TicketRetailers.imageReturnJourneyTicketsSkipLink1.AlternateText",TDCultureInfo.CurrentUICulture);

					// Only have retailers skip links when the retailers panels are shown (retail units selected)
					if(options.SelectedOutwardRetailUnit != null)
					{
						panelOutwardJourneyRetailersSkipLink1.Visible=true;
						imageOutwardJourneyRetailersSkipLink1.AlternateText = 
							Global.tdResourceManager.GetString("TicketRetailers.imageReturnJourneyRetailersSkipLink1.AlternateText",TDCultureInfo.CurrentUICulture);
					}

					//Setting this will let the control display outward and return dates together for a combined return journey.
					if (options.JourneyItinerary.ReturnJourney != null)
					{
						outwardTickets.ReturnJourneyDate = 
							((PublicJourney)options.JourneyItinerary.ReturnJourney).JourneyDate;
					}
				}
				else
				{
					// Set "skip to outward" type text
					panelOutwardJourneyTicketsSkipLink1.Visible=true;
					imageOutwardJourneyTicketsSkipLink1.AlternateText = 
						Global.tdResourceManager.GetString("TicketRetailers.imageOutwardJourneyTicketsSkipLink1.AlternateText",TDCultureInfo.CurrentUICulture);

					// Only have retailers skip links when the retailers panels are shown (retail units selected)
					if(options.SelectedOutwardRetailUnit != null)
					{
						panelOutwardJourneyRetailersSkipLink1.Visible=true;
						imageOutwardJourneyRetailersSkipLink1.AlternateText = 
							Global.tdResourceManager.GetString("TicketRetailers.imageOutwardJourneyRetailersSkipLink1.AlternateText",TDCultureInfo.CurrentUICulture);
					}
				}
			}

			if (returnExists)
			{
				// Set "skip to return" type text
				panelReturnJourneyTicketsSkipLink1.Visible=true;
				imageReturnJourneyTicketsSkipLink1.AlternateText = 
					Global.tdResourceManager.GetString("TicketRetailers.imageReturnJourneyTicketsSkipLink1.AlternateText",TDCultureInfo.CurrentUICulture);

				// Only have retailers skip links when the retailers panels are shown (retail units selected)
				if(options.SelectedReturnRetailUnit != null)
				{
					panelReturnJourneyRetailersSkipLink1.Visible=true;
					imageReturnJourneyRetailersSkipLink1.AlternateText = 
						Global.tdResourceManager.GetString("TicketRetailers.imageReturnJourneyRetailersSkipLink1.AlternateText",TDCultureInfo.CurrentUICulture);
				}
			}

		}

		#endregion Private Methods        

		#region Web Form Designer generated code

		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraEventWireUp();
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

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			this.theJourneyChangeSearchControl.GenericBackButton.Click += new EventHandler(this.buttonBack_Click);
		}
		#endregion

        

    }
}
