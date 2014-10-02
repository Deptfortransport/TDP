// *********************************************** 
// NAME                 : TicketRetailersHelper.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 22/02/2005
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/TicketRetailersHelper.cs-arc  $
//
//   Rev 1.3   Apr 29 2008 13:43:14   jfrank
//Fix for find cheaper rail fares - retailer order and selection.  See IR 4902 for further detail.
//Resolution for 4902: Find Cheaper Rail Fares Retailer handoff
//
//   Rev 1.2   Mar 31 2008 12:59:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:30   mturner
//Initial revision.
//
//   Rev 1.20   Apr 26 2006 12:17:06   RPhilpott
//Manual merge of stream 35
//Resolution for 3946: DD075: Find Cheaper: Ticket Retailers page displayed with missing details
//
//   Rev 1.19   Mar 14 2006 10:31:36   RGriffith
//Manual merge for Stream3353
//
//   Rev 1.18   Feb 23 2006 19:16:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.17.2.0   Feb 22 2006 12:07:36   RGriffith
//Changes made for multiple asynchronous ticket/costing. Movement of methods into SessionManager.PricingRetailOptionsState
//
//   Rev 1.17.1.1   Jan 30 2006 12:21:06   mdambrine
//add TDCultureInfo is now in the common namespace
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.17.1.0   Jan 10 2006 15:17:46   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.17   Nov 15 2005 08:52:28   jgeorge
//Moved check to see if cost based buy option changed.
//Resolution for 2995: DN040: Incorrect retailer and ticket information after using Back button
//Resolution for 2996: DN040: spurious Wait Page when selecting "Amend" in SBT
//Resolution for 3030: DN040: Switching between options on results page invokes wait page
//
//   Rev 1.16   Nov 09 2005 12:31:56   build
//Automatically merged from branch for stream2818
//
//   Rev 1.15.1.3   Nov 08 2005 11:34:24   mguney
//The select buttons in TicketRetailers.aspx were not functioning.
//Came accross to this problem during the integration testing. JG changed GetPricingRetailOptionsState method to fix the ticket retailer selection bug but it still has problems. When back button is clicked, the page doesn't display the previous state.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.15.1.2   Nov 04 2005 15:50:50   RWilby
//Updated GetPricingRetailOptionsState method
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.15.1.1   Oct 29 2005 16:18:32   RPhilpott
//Updated requestId
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.15.1.0   Oct 14 2005 15:28:12   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.15   May 12 2005 11:37:10   tmollart
//Modified BuildSelectedTickets method to use correct fare price.
//Resolution for 2478: PT costing - fare wrong if min fare > undiscounted
//
//   Rev 1.14   May 09 2005 17:49:08   jgeorge
//Changed to compare Ticket objects using Equals method rather than == operator
//
//   Rev 1.13   May 04 2005 17:28:30   jgeorge
//Reinstated code to use minimum fares where applicable to cost based search results.
//Resolution for 2422: PT - Minimum Fares not being applied in Find a Fare
//
//   Rev 1.12   May 03 2005 15:19:42   jgeorge
//Fix to ensure discount cards are taken into account on tickets/retailers page,.
//
//   Rev 1.11   Apr 28 2005 14:20:24   jgeorge
//Change in way the cost based search results are handled when creating PricingRetailOptionsState/TicketRetailerInfo objects.
//Resolution for 2309: PT - Train - Destination wrong on rail ticket description.
//
//   Rev 1.10   Apr 19 2005 11:37:12   jgeorge
//Updated to cope with minimum fares
//Resolution for 2071: PT - Train - Minimum train fares not flagged
//
//   Rev 1.9   Apr 16 2005 12:01:22   jgeorge
//Updated discount card handling for cost based search, and added support for minimum fares.
//Resolution for 2071: PT: Minimum train fares not flagged
//Resolution for 2074: PT: Discount cards not displayed correctly on Ticket Retailers page after cost based search
//
//   Rev 1.8   Apr 11 2005 11:11:06   jgeorge
//Corrections for handling find a fare results
//Resolution for 2073: PT: Ticket Retailers page does not work correctly with Find a Fare
//
//   Rev 1.7   Apr 09 2005 13:21:54   jgeorge
//Bug fixes
//
//   Rev 1.6   Mar 31 2005 16:26:26   jgeorge
//Bug fix
//
//   Rev 1.5   Mar 30 2005 15:19:14   jgeorge
//Integration with cost based search, and updates to ticket retailer handoff
//
//   Rev 1.4   Mar 22 2005 09:20:32   jgeorge
//FxCop updates
//
//   Rev 1.3   Mar 15 2005 08:43:24   tmollart
//Removed reference to obsolete property.
//
//   Rev 1.2   Mar 08 2005 16:28:56   jgeorge
//Added first try at cost based search code.
//
//   Rev 1.1   Mar 04 2005 09:21:10   jgeorge
//Transferred methods from TicketRetailers and TicketRetailerHandoff pages
//
//   Rev 1.0   Feb 22 2005 17:43:32   jgeorge
//Initial revision.
 
using System;
using System.Collections;
using System.Resources;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// Functionality used by the Ticket Retailers pages
	/// </summary>
	public sealed class TicketRetailersHelper
	{
		private TDResourceManager resourceManager;

		public const string MoneyFormat = "&pound;{0:N}";

		private string resourceAdultFareFull;
		private string resourceAdultFareDiscounted;
		private string resourceChildFareFull;
		private string resourceChildFareDiscounted;
		private string resourceChildFareAgesFull;
		private string resourceChildFareAgesDiscounted;

		#region Constructor

		/// <summary>
		/// Constructor which loads the resources from the given resource manager
		/// </summary>
		/// <param name="resources"></param>
		public TicketRetailersHelper(TDResourceManager resources)
		{
			this.resourceManager = resources;
			LoadResources();
		}

		/// <summary>
		/// Default constructor. Does not load any resources. If this constructor is used, 
		/// calls should not be made to the BuildFaresList function
		/// </summary>
		public TicketRetailersHelper()
		{ }

		#endregion

		#region Public methods

		/// <summary>
		/// Constructs the list of fares to display. This includes up to four entries
		/// for full and discounted, adult and child fares
		/// </summary>
		/// <param name="rowTicket"></param>
		/// <returns></returns>
		public ArrayList BuildFaresList(SelectedTicket rowTicket, Discounts discounts)
		{
			ArrayList faresList = new ArrayList(4);

			bool displayDiscountedFares = DisplayDiscountedFares(rowTicket.Mode, discounts.RailDiscount.Length != 0, discounts.CoachDiscount.Length != 0);

			// Full adult fare
			if (!float.IsNaN(rowTicket.Ticket.AdultFare))
				AddFareToList(faresList, resourceAdultFareFull, string.Format( TDCultureInfo.InvariantCulture, MoneyFormat, rowTicket.Ticket.AdultFare ) );

			// Discounted adult fare
			if (displayDiscountedFares && (!float.IsNaN(rowTicket.Ticket.DiscountedAdultFare)))
				AddFareToList(faresList, resourceAdultFareDiscounted, string.Format( TDCultureInfo.InvariantCulture, MoneyFormat, rowTicket.Ticket.DiscountedAdultFare ) );

			// Full child fare
			if (!float.IsNaN(rowTicket.Ticket.ChildFare))
				AddFareToList(faresList, (rowTicket.Ticket.MaxChildAge == 0) ? resourceChildFareFull : string.Format(TDCultureInfo.InvariantCulture, resourceChildFareAgesFull, rowTicket.Ticket.MinChildAge, rowTicket.Ticket.MaxChildAge), 
					string.Format( TDCultureInfo.InvariantCulture, MoneyFormat, rowTicket.Ticket.ChildFare ) );

			// Discounted adult fare
			if (displayDiscountedFares && (!float.IsNaN(rowTicket.Ticket.DiscountedChildFare)))
				AddFareToList(faresList, (rowTicket.Ticket.MaxChildAge == 0) ? resourceChildFareDiscounted : string.Format( TDCultureInfo.InvariantCulture, resourceChildFareAgesDiscounted, rowTicket.Ticket.MinChildAge, rowTicket.Ticket.MaxChildAge), 
					string.Format( TDCultureInfo.InvariantCulture, MoneyFormat, rowTicket.Ticket.DiscountedChildFare ) );

			return faresList;
		}

		/// <summary>
		/// Determines whether or not discounted fares should be displayed for 
		/// tickets using the selected mode. This is determined by checking to see
		/// if any discount cards corresponding to that mode are being used.
		/// </summary>
		/// <param name="mode"></param>
		/// <returns></returns>
		public static bool DisplayDiscountedFares(TransportDirect.JourneyPlanning.CJPInterface.ModeType mode, bool hasRailCard, bool hasCoachCard)
		{
			if (!(hasRailCard || hasCoachCard))
				return false;
			else
				switch (mode)
				{
					case TransportDirect.JourneyPlanning.CJPInterface.ModeType.Coach:
						return hasCoachCard;
					case TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail:
					case TransportDirect.JourneyPlanning.CJPInterface.ModeType.RailReplacementBus:
						return hasRailCard;
					default:
						return false;
				}
		}

		/// <summary>
		/// Returns true if the current search type is cost based
		/// </summary>
		/// <returns></returns>
		public static bool IsCostBasedSearch
		{
			get 
			{ 
				ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
				return FindInputAdapter.IsCostBasedSearchMode( sessionManager.FindAMode );
			}
		}

		/// <summary>
		/// Returns the current PricingRetailOptionsState object, creating it first if necessary.
		/// TicketRetailerInfo properties are also updated if necessary.
		/// </summary>
		/// <returns></returns>
		public PricingRetailOptionsState GetPricingRetailOptionsState()
		{
			PricingRetailOptionsState currentOptions = TDItineraryManager.Current.PricingRetailOptions;

			if (currentOptions == null)
				return GetPricingRetailOptionsState(FindFareBuyOption.None);
			else
				return GetPricingRetailOptionsState(currentOptions.SelectedCostBasedBuyOption);
		}

		/// <summary>
		/// Returns the current PricingRetailOptionsState object, creating it first if necessary.
		/// TicketRetailerInfo properties are also updated if necessary.
		/// </summary>
		/// <returns></returns>
		public PricingRetailOptionsState GetPricingRetailOptionsState(PricingRetailOptionsState currentOptions, Journey outwardJourney, Journey returnJourney)
		{
			if (currentOptions == null)
				return GetPricingRetailOptionsState(currentOptions, FindFareBuyOption.None, outwardJourney, returnJourney);
			else
				return GetPricingRetailOptionsState(currentOptions, currentOptions.SelectedCostBasedBuyOption, outwardJourney, returnJourney);
		}

		/// <summary>
		/// Returns the current PricingRetailOptionsState object, creating it first if necessary
		/// </summary>
		/// <param name="ensureTicketRetailerInfoPopulated">Whether or not to populate the 
		/// TicketRetailerInfo properties they are not present/out of date necessary</param>
		/// <returns></returns>
		public PricingRetailOptionsState GetPricingRetailOptionsState(FindFareBuyOption costBasedBuyOption)
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			PricingRetailOptionsState options = itineraryManager.PricingRetailOptions;

			// Initialise the session with pricing and retail user options if this
			// if the first time the user has visited pricing and ticket retailing pages
			// during the session
			if (options == null) 
			{
				// First visit to page during session so create session object
				options = new PricingRetailOptionsState();
				itineraryManager.PricingRetailOptions = options;
			} 

			if (options.HasProcessedFaresJourneyChanged)
			{
				ITDJourneyResult journeyResult = itineraryManager.JourneyResult;
				TDJourneyViewState journeyViewState = itineraryManager.JourneyViewState;

				PublicJourney outwardJourney = null;
				PublicJourney returnJourney = null;

				if ((journeyResult != null) && (journeyViewState != null))
				{
					// Get selected outbound public journey
					if( journeyViewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
					{
						outwardJourney = journeyResult.OutwardPublicJourney(journeyViewState.SelectedOutwardJourneyID);
					}
					else if( journeyViewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended )
					{
						outwardJourney = journeyResult.AmendedOutwardPublicJourney;
					}

					// Get selected return public journey
					if( journeyViewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
					{
						returnJourney = journeyResult.ReturnPublicJourney(journeyViewState.SelectedReturnJourneyID);
					}
					else if( journeyViewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended )
					{
						returnJourney = journeyResult.AmendedReturnPublicJourney;
					}
				}

				options.CreateItinerary(costBasedBuyOption, outwardJourney, returnJourney);
			}

			if	(IsCostBasedSearch)
			{
				options.SelectedCostBasedBuyOption = costBasedBuyOption;
				SetSelectedTicket(options);
			}

			if (!options.JourneyItinerary.RetailersInitialised)
			{
				options.JourneyItinerary.FindRetailers();

                //Added to initialise retailer info for Cost based search.
                if (options.HasProcessedTicketRetailerInfoJourneyChanged && IsCostBasedSearch)
                {
                    PopulateTicketRetailerInfo(options);
                }
			}

            if (options.HasProcessedTicketRetailerInfoJourneyChanged && !IsCostBasedSearch)
			{
				PopulateTicketRetailerInfo(options);
			}

			return options;
		}

		/// <summary>
		/// Returns the current PricingRetailOptionsState object, creating it first if necessary
		/// </summary>
		/// <param name="ensureTicketRetailerInfoPopulated">Whether or not to populate the 
		/// TicketRetailerInfo properties they are not present/out of date necessary</param>
		/// <returns></returns>
		public PricingRetailOptionsState GetPricingRetailOptionsState(PricingRetailOptionsState options, FindFareBuyOption costBasedBuyOption, Journey outwardJourney, Journey returnJourney)
		{
			// Initialise the session with pricing and retail user options if this
			// if the first time the user has visited pricing and ticket retailing pages
			// during the session
			if (options == null) 
			{
				// First visit to page during session so create session object
				options = new PricingRetailOptionsState();
			} 

			options.GetPricingRetailOptionsState(costBasedBuyOption, outwardJourney, returnJourney);

			return options;
		}

		/// <summary>
		/// Generates the TicketRetailerInfo objects for time based searches
		/// </summary>
		/// <param name="options"></param>
		public static void PopulateTicketRetailerInfo(PricingRetailOptionsState options)
		{
			options.PopulateTicketRetailerInfo();
		}

		#endregion

		#region Private methods


		private void SetSelectedTicket(PricingRetailOptionsState options)
		{
			options.SetSelectedTicket();
		}

		/// <summary>
		/// Adds the given fare information to an array list.
		/// </summary>
		/// <param name="theList"></param>
		/// <param name="name"></param>
		/// <param name="price"></param>
		private static void AddFareToList(ArrayList theList, string name, string price)
		{
			theList.Add( new string[] { name, price });
		}

		/// <summary>
		/// Loads the resources from the given resource manager
		/// </summary>
		private void LoadResources()
		{
			resourceAdultFareFull = resourceManager.GetString( "FareDescription.AdultFare.Full", TDCultureInfo.CurrentUICulture);
			resourceAdultFareDiscounted = resourceManager.GetString( "FareDescription.AdultFare.Discounted", TDCultureInfo.CurrentUICulture);
			resourceChildFareFull = resourceManager.GetString( "FareDescription.ChildFare.Full", TDCultureInfo.CurrentUICulture);
			resourceChildFareDiscounted = resourceManager.GetString( "FareDescription.ChildFare.Discounted", TDCultureInfo.CurrentUICulture);
			resourceChildFareAgesFull = resourceManager.GetString( "FareDescription.ChildFare.Ages.Full", TDCultureInfo.CurrentUICulture);
			resourceChildFareAgesDiscounted = resourceManager.GetString( "FareDescription.ChildFare.Ages.Discounted", TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Builds an array of selected ticket objects for the pricing units supplied.
		/// </summary>
		/// <param name="options"></param>
		/// <param name="pricingUnits"></param>
		/// <returns></returns>
		private static SelectedTicket[] BuildSelectedTickets(PricingRetailOptionsState options, IList pricingUnits, Journey journey)
		{
			return options.BuildSelectedTickets(pricingUnits, journey);
		}

		#endregion
	}
}
