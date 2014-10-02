// *********************************************** 
// NAME                 : RefineTickets.ascx.cs 
// AUTHOR               : Rob Griffith
// DATE CREATED         : 30/01/2006
// DESCRIPTION			: A new page as part of the new Extension, Replan and Adjust pages. 
//                      Displays the full ticket cost details of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/RefineTickets.aspx.cs-arc  $
//
//   Rev 1.7   Oct 29 2010 11:12:40   rbroddle
//Removed explicit wire up to Page_Load & Page_PreRender as AutoEventWireUp=true for this page so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.6   Mar 29 2010 16:40:38   mmodi
//Added Page Title value
//Resolution for 5487: Del 10.10 - Accessibility Issues from testing
//
//   Rev 1.5   Feb 09 2009 15:20:52   mmodi
//Updated code for Routing Guide Sections to be added to a modified journey
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.4   Feb 02 2009 17:14:54   mmodi
//Show break of journey note for Adjusted journeys
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.3   Jan 08 2009 11:12:14   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.3   Nov 29 2007 14:09:48   mturner
//Remove redundant declarations
//
//   Rev 1.2   Nov 29 2007 13:07:32   mturner
//Declared as partial class to make Del 9.8 code .Net2 complient
//
//   Rev 1.1   Nov 29 2007 11:35:38   mturner
//Updated for Del 9.8
//
//   Rev 1.20   Nov 14 2007 12:56:48   pscott
//4520 - Recalculate fare when mid journey adjustment
//
//   Rev 1.18   Jun 06 2006 18:03:24   rphilpott
//Add manualPrerender() call to AmendFares control to prevent correct initialisation being overwritten by default values.
//Resolution for 4053: DN068: Amend tool when used on Extend pages loses selected values
//
//   Rev 1.17   May 04 2006 12:00:28   RPhilpott
//Restore rev 1.15 changes lost due to PVCS glitch.
//Resolution for 4071: Del 8.1: Return fares displayed on costs page but Amend fares tab shows single
//
//   Rev 1.16   May 02 2006 17:45:20   RPhilpott
//Reset the ItineraryManager on the JourneyBuilderControl after clearing deferred data. 
//Resolution for 4047: DN068 Extend: Error when pricing journey with extension.
//Resolution for 4049: DN068: Blank journey extension added to journey from fares page
//
//   Rev 1.15   May 02 2006 17:15:04   asinclair
//Only Initialise the AmendSaveSend control on first entry to the page
//Resolution for 4053: DN068: Amend tool when used on Extend pages loses selected values
//
//   Rev 1.14   Apr 26 2006 15:58:22   esevern
//Sets visibility of labelInstructionText - should be hidden if the AmendSaveSendControl is not visible
//Resolution for 3999: DN068 Extend: 'Amend fares panel' message should not be shown for car only extension
//
//   Rev 1.13   Apr 26 2006 15:06:44   tmollart
//Changed code so that all changes to dropdowns cause a recalculation of fares.
//Resolution for 3988: DN068: Unable to display return fares for replanned journey
//
//   Rev 1.12   Apr 20 2006 17:52:08   tmollart
//Modified logic to calculate fares.
//Resolution for 3925: DN068 Extend: no result waiting for tickets/costs for bus journeys
//
//   Rev 1.11   Apr 05 2006 15:24:46   esevern
//Manual merge of stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.10   Mar 21 2006 17:35:32   RGriffith
//Addition of JourneyBuilderControl for when an extension is in progress
//
//   Rev 1.9   Mar 17 2006 15:32:40   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.8   Mar 14 2006 11:40:18   RGriffith
//Addition of Skiplinks, HeaderControl and HeadElementControl as well as inclusion of new Resource namespace
//
//   Rev 1.7   Mar 13 2006 11:57:34   RGriffith
//Changes to make Car Fuel Costs drop down printer friendly
//
//   Rev 1.6   Mar 10 2006 16:41:24   RGriffith
//Multiple adjustments including not processing return JourneyTicketsCostsControl if no return journey exists
//
//   Rev 1.5   Mar 08 2006 16:54:34   RGriffith
//Changes to get AmendFares control working and populate return pricing in the correct order
//
//   Rev 1.4   Mar 06 2006 17:27:56   RGriffith
//Changes made for tickets/costs
//
//   Rev 1.3   Mar 01 2006 13:31:36   RGriffith
//Changes for use with Tickets and Costs page
//
//   Rev 1.2   Feb 22 2006 12:09:48   RGriffith
//InterimVersion
//
//   Rev 1.1   Feb 15 2006 16:13:56   RGriffith
//Interim Version
//
//   Rev 1.0   Feb 14 2006 11:30:16   RGriffith
//Initial revision.
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

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.TimeBasedPriceRunner;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for RefineTickets.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class RefineTickets : TDPage
    {
        #region Private members
        // Button controls
		protected TransportDirect.UserPortal.Web.Controls.PrinterFriendlyPageButtonControl printerFriendlyControl;
		
		// TD Web Controls
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyTicketCostsControl outwardJourneyTicketCostsControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyTicketCostsControl returnJourneyTicketCostsControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyBuilderControl addExtensionControl;
		protected TransportDirect.UserPortal.Web.Controls.AmendSaveSendControl AmendSaveSendControl;

		// Private variables
		private AsyncCallStatus priceStatus;
		private bool discountsChanged;
		private PricingRetailOptionsState[] options;
		private bool multiplePublicJourneysExist;
        #endregion

        #region Constructor
        /// <summary>
		/// Constructor for the Page
		/// </summary>
		public RefineTickets() : base()
		{
			// Set page Id
			pageId = PageId.RefineTickets;

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
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Initialise text and button properties
			PopulateControls();

			// Set up the Journey Tickets Costs Control (and required data)
			InitialiseJourneyTicketCostsControls();

            //Added for white labelling:
            ConfigureLeftMenu(expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuPlanAJourney);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextRefineTickets);
            expandableMenuControl.AddExpandedCategory("Related links");

		}

		private void Page_PreRender(object sender, System.EventArgs e)
		{
			// If the AmendSaveSendControl bar is required - call the method to set it up correctly
			if (AmendSaveSendControl.Visible)
			{
				AmendSaveSendControl.manualPreRender();
				AmendSaveSendControl.AmendFaresControl.manualPreRender();

				// Note: Setting up of footnote relies on their being public 
				// transport results - as does the amend fare visibility
				SetFootnote();

				// Set up amend fares control
				SetAmendFareControl();
			}
			else
			{
				// hide the instructional text - its only relevant if amend control visible
				labelIntroductoryText.Visible = false;
			}

			// Only show add extension button if an extend is in progress
			addExtensionControl.Visible = TDItineraryManager.Current.ExtendInProgress;
		}

		/// <summary>
		/// Handles back button click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void backButton_Click(object sender, EventArgs e)
		{
			// Navigate back to summary page
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineDetailsBack;
		}

		/// <summary>
		/// Stores the user's AmendSaveSendControl selections in the session
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void AmendFares_Click(object sender, EventArgs e)
		{
			// Update the session with the user selections
			foreach (PricingRetailOptionsState optionsItem in options)
			{
				optionsItem.Discounts.RailDiscount = AmendSaveSendControl.AmendFaresControl.RailCard;
				optionsItem.Discounts.CoachDiscount = AmendSaveSendControl.AmendFaresControl.CoachCard;
		
				optionsItem.ShowChildFares = AmendSaveSendControl.AmendFaresControl.ShowChildFares;
				optionsItem.OverrideItineraryType = AmendSaveSendControl.AmendFaresControl.ShowItineraryType;		

				optionsItem.SetProcessedJourneys();
			}

			// Only set apply new discounts if the rail or coach discounts have changed
			if (discountsChanged)
			{
				discountsChanged = false;

				CalculateFares(options);
			}
			
			// If calculation is still in process then navigate to the WaitPage
			if (priceStatus == AsyncCallStatus.InProgress)
			{
				TDSessionManager.Current.Session[SessionKey.Transferred] = false;
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;
			}
			// Else navigate to this page to display found results
			else
			{
				TDSessionManager.Current.FormShift[SessionKey.ForceRedirect]= true;
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.RefineTicketsView;
			}
		}

		/// <summary>
		/// Handles any change to the fares options.
		/// </summary>
		/// <param name="sender">Object</param>
		/// <param name="e">Arguments</param>
		private void AmendFaresControl_FaresOptionChanged(object sender, EventArgs e)
		{
			discountsChanged = true;
		}
		
		#endregion

		#region Private Methods
		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
            this.PageTitle = GetResource("RefineTickets.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");

			// Get correct resource strings for labels
			labelTitle.Text = GetResource("RefineTickets.labelTitle.Text");
			labelIntroductoryText.Text = GetResource("RefineTickets.labelIntroductoryText.Text");
			labelOutward.Text = GetResource("RefineTickets.labelOutward.Text");
			labelReturn.Text = GetResource("RefineTickets.labelReturn.Text");

			// Setup Skip Links (1 invisible image for all skip links)
			imageMainContentSkipLink1.ImageUrl = GetResource("SkipLinks.InvisibleImage.ImageUrl");
			imageMainContentSkipLink1.AlternateText = GetResource("RefineDetails.imageMainContentSkipLink.AlternateText");

			// Get correct resource strings for buttons
			backButton.Text = GetResource("RefineTickets.backButton.Text");

			// Set Help Button URL
			helpButton.HelpUrl = GetResource("RefineTickets.helpButton.HelpUrl");

			// Set footnote table to not visible by default - changed at prerender if necessary
			tableFootnotes.Visible = false;

			// Set up the journey builder control
			addExtensionControl.ItineraryManager = TDItineraryManager.Current;
		}

		/// <summary>
		/// Method to initialise the Journey Tickets Costs Controls
		/// </summary>
		private void InitialiseJourneyTicketCostsControls()
		{
			// Set up session and itinerary manager variables
			ITDSessionManager sessionManager = TDSessionManager.Current;
			TDJourneyViewState journeyViewState = sessionManager.JourneyViewState;
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			// Determine ItineraryManager mode
			ItineraryManagerMode itineraryManagerMode = sessionManager.ItineraryMode;
			bool useSessionManager = ((itineraryManagerMode == ItineraryManagerMode.None) || (itineraryManager.ExtendInProgress));
			bool returnRequired = (useSessionManager) ? sessionManager.JourneyRequest.IsReturnRequired : (itineraryManager.ReturnLength > 0);

			// Explicitly set the outward JourneyTicketsCostsControl so it knows it's NOT displaying the return journey details
			outwardJourneyTicketCostsControl.IsReturnJourney = false;
			// Explicitly set the return JourneyTicketsCostsControl so it knows it is displaying the return journey details
			returnJourneyTicketCostsControl.IsReturnJourney = true;

			// Set up the JourneyTicketCosts controls to NOT be printer friendly
			outwardJourneyTicketCostsControl.PrinterFriendly = false;
			returnJourneyTicketCostsControl.PrinterFriendly = false;

			// Call Itinerary Manager method to get PricingRetailOptionsState variables for all appropriate journeys
			options = itineraryManager.GetItineraryPricing();

			// ### Code to set up JourneyTicketsCostsControl pricing and journey details ###
			// ### for Adjust mode (ie:IMMode.None) - uses session manager not itinerary manager ###
			if (useSessionManager)
            {
                #region Adjust
                if (sessionManager.PricingRetailOptions.CalculateFaresOverride == true)
				{
					// Create a new itinerary for the adjusted journey so fares can be calculated
					// for this journey rather than the original
					if (sessionManager.JourneyResult.AmendedOutwardPublicJourney != null 
						|| sessionManager.JourneyResult.AmendedReturnPublicJourney != null)
					{
						// creating the new itinerary sets PricingRetailOptions.JourneyItinerary.FaresInitialised to false 
						// enabling CalculateFares to be called
						Itinerary adjustedJourneyItinerary = new Itinerary(sessionManager.JourneyResult.AmendedOutwardPublicJourney, sessionManager.JourneyResult.AmendedReturnPublicJourney);

						sessionManager.PricingRetailOptions.JourneyItinerary = adjustedJourneyItinerary;
					
					}

					// Reset flag to ensure we don't repeatedly call CalculateFares
					sessionManager.PricingRetailOptions.CalculateFaresOverride = false;
				}
				
                // Set the flag to allow break of journeys message to be shown on the control
                outwardJourneyTicketCostsControl.ShowBreakOfJourneyNote = true;

				// Create new PriceOptionsArrays from sessionManager
				outwardJourneyTicketCostsControl.PriceOptionsArray = new PricingRetailOptionsState[1] {sessionManager.PricingRetailOptions};

				// Set up outward Journey array depending on the type of journey stored in session manager
				if (journeyViewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested)
				{
					outwardJourneyTicketCostsControl.JourneyArray = new Journey[] {sessionManager.JourneyResult.OutwardRoadJourney()};
				}
				else if (journeyViewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal)
				{
					outwardJourneyTicketCostsControl.JourneyArray = new Journey[] {sessionManager.JourneyResult.OutwardPublicJourney(journeyViewState.SelectedOutwardJourneyID)};
				}
				else if (journeyViewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended)
				{
					outwardJourneyTicketCostsControl.JourneyArray = new Journey[] {sessionManager.JourneyResult.AmendedOutwardPublicJourney};
				}
				else
				{
					outwardJourneyTicketCostsControl.JourneyArray = new Journey[] {sessionManager.JourneyResult.OutwardPublicJourney(journeyViewState.SelectedOutwardJourneyID)};
				}

				if (returnRequired)
				{
                    // Set the flag to allow break of journeys message to be shown on the control
                    returnJourneyTicketCostsControl.ShowBreakOfJourneyNote = true;

					// Create new PriceOptionsArrays from sessionManager
					returnJourneyTicketCostsControl.PriceOptionsArray = new PricingRetailOptionsState[1] {sessionManager.PricingRetailOptions};

					// Set up return Journey array depending on the type of journey stored in session manager
					if (journeyViewState.SelectedReturnJourneyType == TDJourneyType.RoadCongested)
					{
						returnJourneyTicketCostsControl.JourneyArray = new Journey[] {sessionManager.JourneyResult.ReturnRoadJourney()};
					}
					else if (journeyViewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal)
					{
						returnJourneyTicketCostsControl.JourneyArray = new Journey[] {sessionManager.JourneyResult.ReturnPublicJourney(journeyViewState.SelectedReturnJourneyID)};
					}
					else if (journeyViewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended)
					{
						returnJourneyTicketCostsControl.JourneyArray = new Journey[] {sessionManager.JourneyResult.AmendedReturnPublicJourney};
					}
					else
					{
						returnJourneyTicketCostsControl.JourneyArray = new Journey[] {sessionManager.JourneyResult.ReturnPublicJourney(journeyViewState.SelectedOutwardJourneyID)};
					}
                }
                #endregion
            }
				// ### for Replan mode (ie:IMMode.Replan) ###
			else if (itineraryManagerMode == ItineraryManagerMode.Replan)
            {
                #region Replan
                // Create new PriceOptionsArrays from itinerary Manager
				outwardJourneyTicketCostsControl.PriceOptionsArray = new PricingRetailOptionsState[itineraryManager.OutwardLength];

				// Copy out the outward parts of the ItineraryManager's pricing options 
				// into the respective individual JourneyTicketCostsControls
				Array.Copy(options, 0, outwardJourneyTicketCostsControl.PriceOptionsArray, 0, itineraryManager.OutwardLength);

				// Set up the JourneyTicketCostsControl journey arrays for outward controls
				outwardJourneyTicketCostsControl.JourneyArray = itineraryManager.OutwardJourneyItinerary;

				if (returnRequired)
				{
					// Create new PriceOptionsArrays from itinerary Manager
					returnJourneyTicketCostsControl.PriceOptionsArray = new PricingRetailOptionsState[itineraryManager.ReturnLength];

					// Copy out the return parts of the ItineraryManager's pricing options 
					// into the respective individual JourneyTicketCostsControls
					Array.Copy(options, itineraryManager.OutwardLength, returnJourneyTicketCostsControl.PriceOptionsArray, 0, itineraryManager.ReturnLength);

					// Set up the JourneyTicketCostsControl journey arrays for return controls
					returnJourneyTicketCostsControl.JourneyArray = itineraryManager.ReturnJourneyItinerary;
                }
                #endregion
            }
				// ### for Extend mode ###
			else
			{
                #region Extend
				// Set up the JourneyTicketCostsControl Pricing arrays for outward controls
				outwardJourneyTicketCostsControl.PriceOptionsArray = options;

				// Set up the JourneyTicketCostsControl journey arrays for outward controls
				outwardJourneyTicketCostsControl.JourneyArray = itineraryManager.OutwardJourneyItinerary;

				if (returnRequired)
				{
					// Set up the JourneyTicketCostsControl Pricing arrays for return controls
					PricingRetailOptionsState[] returnOptions = (PricingRetailOptionsState[])options.Clone();
					Array.Reverse(returnOptions);
					returnJourneyTicketCostsControl.PriceOptionsArray = returnOptions;

					// Set up the JourneyTicketCostsControl journey arrays for return controls
					returnJourneyTicketCostsControl.JourneyArray = itineraryManager.ReturnJourneyItinerary;
                }
                #endregion
            }
			// ### End: Code to set up JourneyTicketsCostsControl pricing and journey details ###

			// Flag to determine if AmendSaveSendControl should be displayed
			bool containsPublicJourney = false;
			multiplePublicJourneysExist = false;

			// ### Code to determine if AmendSaveSendControl is to be displayed ###
			if (outwardJourneyTicketCostsControl.JourneyArray != null)
            {
                #region Show amend
                // If outward Journey contains any public journeys set flag to true
				foreach (Journey item in outwardJourneyTicketCostsControl.JourneyArray)
				{
					if ((item != null)
						&& ((item.Type == TDJourneyType.PublicOriginal) || 
						(item.Type == TDJourneyType.PublicAmended)))
					{
						if (containsPublicJourney)
						{
							multiplePublicJourneysExist = true;
							break;
						}
						containsPublicJourney = true;
					}
				}

				if (returnRequired && (returnJourneyTicketCostsControl.JourneyArray != null) 
					&& (!multiplePublicJourneysExist))
				{
					// If outward Journey doesn't contain any public journeys but return journey does - set flag to true
					foreach (Journey item in returnJourneyTicketCostsControl.JourneyArray)
					{
						if ((item != null)
							&& ((item.Type == TDJourneyType.PublicOriginal) || 
							(item.Type == TDJourneyType.PublicAmended)))
						{
							if (containsPublicJourney)
							{
								multiplePublicJourneysExist = true;
								break;
							}
							containsPublicJourney = true;
						}
					}
				}

				// Set visibility of AmendSaveSendControl
				if (containsPublicJourney)
				{
					AmendSaveSendControl.Visible = true;
				}
				else
				{
					AmendSaveSendControl.Visible = false;
                }
                #endregion
            }
			// ### End: Code to determine if AmendSaveSendControl is to be displayed ###

            // ### Code to determine start and end locations of journey for display ###
            #region Start and End location display
            // Use outward control's JourneyArray to get start and end string values of locations in the journey
			int outwardItineraryLength = outwardJourneyTicketCostsControl.JourneyArray.Length;
			int outwarddLegLength = outwardJourneyTicketCostsControl.JourneyArray[outwardItineraryLength - 1].JourneyLegs.Length;
			string outwardStart = outwardJourneyTicketCostsControl.JourneyArray[0].JourneyLegs[0].LegStart.Location.Description;
			string outwardEnd = outwardJourneyTicketCostsControl.JourneyArray[outwardItineraryLength - 1].JourneyLegs[outwarddLegLength - 1].LegEnd.Location.Description;
			labelOutwardLocations.Text = outwardStart + GetResource("RefineTickets.LocationSeperator.Text") + outwardEnd;
			returnTicketsPanel.Visible = false;

			// If return journey exists - use return control's JourneyArray to get start and end string values of locations in the journey
			if (returnRequired && (returnJourneyTicketCostsControl.JourneyArray.Length > 0)
				&& (returnJourneyTicketCostsControl.JourneyArray[0] != null))
			{
				int returnItineraryLength = returnJourneyTicketCostsControl.JourneyArray.Length;
				int returnLegLength = returnJourneyTicketCostsControl.JourneyArray[returnItineraryLength - 1].JourneyLegs.Length;
				string returnStart = returnJourneyTicketCostsControl.JourneyArray[0].JourneyLegs[0].LegStart.Location.Description;
				string returnEnd = returnJourneyTicketCostsControl.JourneyArray[returnItineraryLength - 1].JourneyLegs[returnLegLength - 1].LegEnd.Location.Description;
				labelReturnLocations.Text = returnStart +  GetResource("RefineTickets.LocationSeperator.Text") + returnEnd;
				returnTicketsPanel.Visible = true;
            }
            #endregion
            // ### End: Code to determine start and end locations of journey for display ###			
			
			TimeBasedFaresSearchState state = sessionManager.AsyncCallState as TimeBasedFaresSearchState;

			if (state == null)
			{
				if (containsPublicJourney 
						&& 
							( 
								(useSessionManager 
									&& 
									(
										
										(sessionManager.PricingRetailOptions == null) 
										|| 
										(sessionManager.PricingRetailOptions.JourneyItinerary == null)
										|| 
										!sessionManager.PricingRetailOptions.JourneyItinerary.FaresInitialised
										
									)
								)
								|| 
								(!useSessionManager && !itineraryManager.PricingDataComplete)
							) 
					)

				{
					// Then we need to start costing and send the user to the wait page
					// if the status goes to in progress.
					CalculateFares(options);
				}
			}

			state = sessionManager.AsyncCallState as TimeBasedFaresSearchState;
			
			if (state != null)
			{
				// If calculations are still in progress - redirect to the Wait Page
				if (priceStatus == AsyncCallStatus.InProgress)
				{
					sessionManager.Session[SessionKey.Transferred] = false;
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;
				}
				else
				{
					// else ensure that the PricingRetailOptionsState is reloaded from deferred storage.
					sessionManager.ClearDeferredData();
					itineraryManager = sessionManager.ItineraryManager;
					addExtensionControl.ItineraryManager = sessionManager.ItineraryManager;
				}
			}
		}


		/// <summary>
		/// Method to trigger the asynchrounous pricing calls
		/// </summary>
		/// <param name="options">Existing PricingRetailOptionsState to be altered with pricing results</param>
		private void CalculateFares(PricingRetailOptionsState[] options)
		{
			// Retrieve SessionManager and CJP Session Info
			ITDSessionManager sessionManager = TDSessionManager.Current;
			CJPSessionInfo sessionInfo = sessionManager.GetSessionInformation();

			// Serialize the pricing info
			TDSessionSerializer serializer = new TDSessionSerializer(sessionInfo.OriginAppDomainFriendlyName);
			serializer.SerializeSessionObjectAndSave(sessionInfo.SessionId, sessionManager.Partition, TDSessionManager.KeyPricingRetailOptionsArray, options);

			// Set up an async call state in the session manager for the call
			AsyncCallState callState = new TimeBasedFaresSearchState();
			// Determine refresh interval and resource string for the wait page
			callState.WaitPageRefreshInterval = Int32.Parse(Properties.Current["WaitPageRefreshSeconds.TicketsAndCosts"]);
			callState.WaitPageMessageResourceFile = "RefineJourney";
			callState.WaitPageMessageResourceId = "WaitPageMessage.TicketsAndCosts";

			callState.AmbiguityPage = this.PageId;
			callState.DestinationPage = this.PageId;
			callState.ErrorPage = this.PageId;
			sessionManager.AsyncCallState = callState;

			// Initialise the price status to be equal 'None'
			priceStatus = AsyncCallStatus.None;

			// Create new TimeBasedPriceSupplier
			ITimeBasedPriceSupplier priceSupplier = (ITimeBasedPriceSupplier) TDServiceDiscovery.Current[ServiceDiscoveryKey.TimeBasedPriceSupplier];

			// If in Adjust mode - force the SessionManager PricingRetailOptionsState to be null so it is recalculated
			if (sessionManager.ItineraryMode == ItineraryManagerMode.None)
			{
				sessionManager.PricingRetailOptions = null;
			}

			// Call the TimeBasedPriceSupplier method to do the pricing of the itinerary
			priceStatus = priceSupplier.PriceItinerary(options.Length);

			// Set the price status to be equal to the AsynCallStatus result
			sessionManager.AsyncCallState.Status = priceStatus;
		}

		/// <summary>
		/// Set up the AmendFares control properly.
		/// </summary>
		private void SetAmendFareControl()
		{		
			// Set up control properties as appropriate
			TDSessionManager.Current.JourneyViewState.SelectedTabIndex = 1;
			AmendSaveSendControl.HideHelpButton = true;
			AmendSaveSendControl.Initialise( this.pageId);

			// If pricing results exist...
			if ((options != null) && (options.Length > 0))
			{
				Discounts discounts = options[0].Discounts;
					
				if (discounts != null)
				{
					// ...set up the discount card properties
					AmendSaveSendControl.AmendFaresControl.RailCard = discounts.RailDiscount;
					AmendSaveSendControl.AmendFaresControl.CoachCard = discounts.CoachDiscount;
				}

				// ... and set up the Adult/Child & Single/Return options
				AmendSaveSendControl.AmendFaresControl.ShowChildFares = options[0].ShowChildFares;
				AmendSaveSendControl.AmendFaresControl.ShowItineraryType = options[0].OverrideItineraryType;
			}
		}

		private void SetFootnote()
		{
			// Only set footnote visible if multiple public journeys exist
			if (multiplePublicJourneysExist)
			{
				// Set footnote table as visible
				tableFootnotes.Visible = true;

				// Set up footnote label strings
				labelFootnote.Text = GetResource("RefineTickets.labelFootnote.Text");
				labelThroughTicketingInfo.Text = GetResource("RefineTickets.labelThroughTicketingInfo.Text");
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
			// Set up back button
			this.backButton.Click += new EventHandler(this.backButton_Click);

			// Set up AmendSaveSendControl buttons
			this.AmendSaveSendControl.AmendFaresControl.OKButton.Click += new EventHandler(AmendFares_Click);
			
			this.AmendSaveSendControl.AmendFaresControl.DropDownListItineraryType.SelectedIndexChanged += new EventHandler(this.AmendFaresControl_FaresOptionChanged);
			this.AmendSaveSendControl.AmendFaresControl.DropDownListRailCard.SelectedIndexChanged += new EventHandler(this.AmendFaresControl_FaresOptionChanged);
			this.AmendSaveSendControl.AmendFaresControl.DropDownListCoachCard.SelectedIndexChanged += new EventHandler(this.AmendFaresControl_FaresOptionChanged);
			this.AmendSaveSendControl.AmendFaresControl.DropDownListAge.SelectedIndexChanged += new EventHandler(this.AmendFaresControl_FaresOptionChanged);
		}
		#endregion
	}
}