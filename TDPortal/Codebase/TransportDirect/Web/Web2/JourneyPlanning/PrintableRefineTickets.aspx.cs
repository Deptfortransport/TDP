// *********************************************** 
// NAME                 : PrintableRefineTickets.ascx.cs 
// AUTHOR               : Rob Griffith
// DATE CREATED         : 30/01/2006
// DESCRIPTION			: A new page as part of the new Extension, Replan and Adjust pages. 
//                      Displays the full ticket cost details of the planned journey.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/PrintableRefineTickets.aspx.cs-arc  $
//
//   Rev 1.3   Jan 08 2009 11:12:14   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:25:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:31:00   mturner
//Initial revision.
//
//   Rev 1.8   May 02 2006 16:44:14   jbroome
//Fix for IR 4046
//Resolution for 4046: DN068: Netscape/Firefox/IE display issues on the Replan Tickets/Costs page
//
//   Rev 1.7   Mar 17 2006 15:32:40   RGriffith
//Extend, adjust, and replan code review suggestions
//
//   Rev 1.6   Mar 14 2006 11:39:56   RGriffith
//Addition of HeaderControl and HeadElementControl as well as inclusion of new Resource namespace
//
//   Rev 1.5   Mar 13 2006 11:57:50   RGriffith
//Changes to make Car Fuel Costs drop down printer friendly
//
//   Rev 1.4   Mar 10 2006 16:39:44   RGriffith
//Changes to bring in line with RefineTickets.aspx
//
//   Rev 1.3   Mar 08 2006 16:52:04   RGriffith
//FxCop suggested changes and explicitly setting the TicketsCostsControl as printer friendly
//
//   Rev 1.2   Mar 06 2006 19:56:18   RGriffith
//Changes for Tickets and Costs
//
//   Rev 1.1   Feb 15 2006 17:32:54   RGriffith
//Intermediate version
//
//   Rev 1.0   Feb 14 2006 11:32:16   RGriffith
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
	/// Summary description for PrintableRefineTickets.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class PrintableRefineTickets : TDPrintablePage
	{
		// HTML Controls
		
		// TD Web Controls
		protected TransportDirect.UserPortal.Web.Controls.JourneyTicketCostsControl outwardJourneyTicketCostsControl;
		protected TransportDirect.UserPortal.Web.Controls.JourneyTicketCostsControl returnJourneyTicketCostsControl;

		// Private variables
		private AsyncCallStatus priceStatus;
		private PricingRetailOptionsState[] options;
		private bool multiplePublicJourneysExist;

		/// <summary>
		/// Constructor for the Page
		/// </summary>
		public PrintableRefineTickets() : base()
		{
			// Set page Id
			pageId = PageId.PrintableRefineTickets;

			// Initialise Resource Manager
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#region Event Handlers
		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Initialise text and button properties
			PopulateControls();

			// Set up the Journey Tickets Costs Control (and required data)
			InitialiseJourneyTicketCostsControls();
		}
		#endregion

		#region Private Methods
		/// <summary>
		/// Method to initialise text and button properties
		/// </summary>
		private void PopulateControls()
		{
			// Get correct resource strings for labels
			labelTitle.Text = GetResource("RefineTickets.labelTitle.Text");
			labelOutward.Text = GetResource("RefineTickets.labelOutward.Text");
			labelReturn.Text = GetResource("RefineTickets.labelReturn.Text");

			// Set up printer friendly labels
			labelPrinterFriendly.Text = GetResource("StaticPrinterFriendly.labelPrinterFriendly");
			labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");
			labelDateTitle.Text = GetResource("StaticPrinterFriendly.labelDateTitle");
			labelDate.Text = TDDateTime.Now.ToString("G");
			labelUsername.Visible = TDSessionManager.Current.Authenticated;
			labelUsernameTitle.Visible = TDSessionManager.Current.Authenticated;

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

			// Set up the JourneyTicketCosts controls to be printer friendly
			outwardJourneyTicketCostsControl.PrinterFriendly = true;
			returnJourneyTicketCostsControl.PrinterFriendly = true;

			// Call Itinerary Manager method to get PricingRetailOptionsState variables for all appropriate journeys
			options = itineraryManager.GetItineraryPricing();

			// ### Code to set up JourneyTicketsCostsControl pricing and journey details ###
			// ### for Adjust mode (ie:IMMode.None) - uses session manager not itinerary manager ###
			if (useSessionManager)
			{
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
			}
				// ### for Replan mode (ie:IMMode.Replan) ###
			else if (itineraryManagerMode == ItineraryManagerMode.Replan)
			{
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
			}
				// ### for Extend mode ###
			else
			{
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
			}
			// ### End: Code to set up JourneyTicketsCostsControl pricing and journey details ###

			// Flag to determine if AmendSaveSendControl should be displayed
			bool containsPublicJourney = false;
			multiplePublicJourneysExist = false;

			// ### Code to determine if AmendSaveSendControl is to be displayed ###
			if (outwardJourneyTicketCostsControl.JourneyArray != null)
			{
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
			}
			// ### End: Code to determine if AmendSaveSendControl is to be displayed ###

			// ### Code to determine start and end locations of journey for display ###
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
				labelReturnLocations.Text = returnStart + GetResource("RefineTickets.LocationSeperator.Text") + returnEnd;
				returnTicketsPanel.Visible = true;
			}
			// ### End: Code to determine start and end locations of journey for display ###

			if (containsPublicJourney && ( (useSessionManager && ((sessionManager.PricingRetailOptions == null) || (sessionManager.PricingRetailOptions.JourneyItinerary == null)
				|| !sessionManager.PricingRetailOptions.JourneyItinerary.FaresInitialised) )
				|| (!useSessionManager && !itineraryManager.PricingDataComplete) ) )
			{
				// Calculate Fares
				CalculateFares(options);

				// If calculations are still in progress - redirect to the Wait Page
				if (priceStatus == AsyncCallStatus.InProgress)
				{
					sessionManager.Session[SessionKey.Transferred] = false;
					sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.WaitingRefresh;
				}
				else
				{
					// else nsure that the PricingRetailOptionsState is reloaded from deferred storage.
					sessionManager.ClearDeferredData();
					itineraryManager = sessionManager.ItineraryManager;
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
