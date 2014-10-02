// *********************************************** 
// NAME			: ItineraryAdapter.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 09.10.03
// DESCRIPTION	: Wraps an Itinerary object to provide easier access
// to fares and retailer details for presentation purposes.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/ItineraryAdapter.cs-arc  $
//
//   Rev 1.5   Feb 26 2009 17:27:46   mmodi
//New method to check for travelcards across pricing units
//Resolution for 5266: ZPBO - Multiple Travelcards when journey has multiple legs
//
//   Rev 1.4   Feb 02 2009 17:05:28   mmodi
//Method to determine if the break of journey message should be shown based on the journey Routing Guide compliance flag
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.3   Jul 14 2008 14:59:58   mmodi
//Uses modeTypes when selecting the journey for a pricing CreateItinerary
//Resolution for 5060: City to city: Fares are shown for the wrong journey
//
//   Rev 1.2   Mar 31 2008 12:59:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:22   mturner
//Initial revision.
//
//   Rev 1.36   Jun 29 2007 19:08:20   asinclair
//Fix for USD 1085365
//
//   Rev 1.35   Apr 18 2007 08:59:24   asinclair
//Changed determineReturnPricingUnits() to always return itinerary.ReturnUnits.  This fixes a number of issues with the display of return journeys for the integration of Local Zonal Serivces and Improved Rail fares.
//
//   Rev 1.34   Mar 29 2007 21:54:44   asinclair
//Changed GetReturnPricedJourney method for ImprovedRail Fares and LocalZonalServices
//
//   Rev 1.33   Mar 06 2007 13:43:52   build
//Automatically merged from branch for stream4358
//
//   Rev 1.32.1.0   Mar 02 2007 10:40:46   asinclair
//Added two new methods- OtherFaresAvailable and NoThroughFares
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.32   Feb 23 2006 19:16:10   build
//Automatically merged from branch for stream3129
//
//   Rev 1.31.1.0   Jan 10 2006 15:17:40   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.31   Nov 09 2005 12:31:56   build
//Automatically merged from branch for stream2818
//
//   Rev 1.30.1.0   Oct 29 2005 14:50:08   RPhilpott
//Get rid of compiler warnings.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.30   Apr 30 2005 13:44:12   jgeorge
//Modified to take into account all tickets being unavailable when checking for presence of tickets
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.29   Apr 28 2005 14:19:24   jgeorge
//Added overloaded CreateItinerary method to create an itinerary and force it to be of type return
//Resolution for 2309: PT - Train - Destination wrong on rail ticket description.
//
//   Rev 1.28   Mar 30 2005 12:00:34   rgeraghty
//Updated CheckForTickets method
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.27   Mar 23 2005 14:32:14   jgeorge
//Removed unused method
//
//   Rev 1.26   Mar 22 2005 17:26:58   rgeraghty
//Removed redundant code and updated comments
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.25   Mar 22 2005 09:20:32   jgeorge
//FxCop updates
//
//   Rev 1.24   Mar 14 2005 14:27:58   rgeraghty
//Amended DoesJourneyContainFares method
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.23   Mar 01 2005 14:51:24   rgeraghty
//DoesJourneyContainFares method added
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.22   Feb 28 2005 14:32:28   jgeorge
//Added new FilterTickets method. The old one should be removed when dependent code has been removed.
//
//   Rev 1.21   Feb 22 2005 18:01:18   jgeorge
//Removed obsolete methods
//
//   Rev 1.20   Feb 11 2005 11:26:56   rgeraghty
//CheckForMissingFare method added to tidy up code
//
//   Rev 1.19   Feb 08 2005 11:39:24   rgeraghty
//Work in progress - version checked in for build to compile
//
//   Rev 1.18   Jan 28 2005 14:53:20   rgeraghty
//Corrected GetPricedJourney method
//
//   Rev 1.17   Jan 26 2005 13:50:30   rgeraghty
//Updated GetOutwardPricedJourney and GetReturnPricedJourney methods. Added IsFareMissing method
//
//   Rev 1.16   Jan 24 2005 17:18:04   jgeorge
//Added method to build TicketRetailerInfo object.
//
//   Rev 1.15   Dec 23 2004 12:04:18   jgeorge
//Added methods to retrieve PricedJourneySegment array for an itinerary.
//
//   Rev 1.14   Jun 17 2004 21:22:18   RHopkins
//Modified handling to cope with Itinerary
//
//   Rev 1.13   Jun 04 2004 10:54:58   acaunt
//GetTicketsByFlexibility renamed as FilterTickets. Can now also filter out child tickets if required.
//
//   Rev 1.12   Nov 18 2003 16:00:02   COwczarek
//SCR#247 : Complete adding comments to existing code and add $Log: for PVCS history

using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using CJPInterfaceAlias = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
    /// Wraps an Itinerary object to provide easier access
    /// to fares and retailer details for presentation purposes
    /// </summary>
	public class ItineraryAdapter
	{

		#region Private fields

		/// <summary>
		/// The itinerary being wrapped
		/// </summary>
		private Itinerary itinerary;
        
		/// <summary>
		/// The override itinerary type set by the user
		/// </summary>
		private ItineraryType overrideItineraryType;
        
		/// <summary>
		/// An empty list returned by some methods to indicate an empty collection
		/// in preference to returning null
		/// </summary>
		private static readonly IList emptyList = ArrayList.ReadOnly( new ArrayList(0) );

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="itinerary">The itinerary to wrap</param>
		public ItineraryAdapter(Itinerary itinerary)
		{
			this.itinerary = itinerary;
		}

		#endregion

		#region Properties
        
		/// <summary>
		/// Itinerary type specified by user that may be different to the default assigned
		/// to the itinerary when created
		/// </summary>
		public ItineraryType OverrideItineraryType
		{
			get {return overrideItineraryType;}
			set {overrideItineraryType = value;}
		}

		/// <summary>
		/// The itinerary wrapped by this adapter
		/// </summary>
		public Itinerary Adaptee
		{
			get {return itinerary;}
		}

		/// <summary>
		/// Read only property returning the pricing units for the outward journey or
		/// an empty collection if none available
		/// </summary>
		public IList OutwardPricingUnits
		{
			get {return itinerary.OutwardUnits;}
		}

		/// <summary>
		/// Read only property returning a collection of PricingUnit objects representing
		/// the pricing units for the return journey or an empty collection if none available.
		/// If the default itinerary type is "single" then some pricing units may be
		/// returned if available.
		/// If the default itinerary type is "return" and the override type is "single"
		/// then some pricing units may be returned if available.
		/// If the default itinerary type is "return" and the override type is "return"
		/// then an empty collection is returned.
		/// </summary>
		public IList ReturnPricingUnits
		{
			get {return determineReturnPricingUnits();}
		}

		/// <summary>
		/// The start location from the first leg of the itinerary's outward journey
		/// </summary>
		public string OriginLocation
		{
			get 
			{
				PublicJourney outwardJourney = (PublicJourney)itinerary.OutwardJourney;
				PublicJourneyDetail firstSegment = (PublicJourneyDetail)outwardJourney.Details[0];
				return firstSegment.LegStart.Location.Description;
			}
		}

		/// <summary>
		/// The end location from the last leg of the itinerary's outward journey
		/// </summary>
		public string DestinationLocation
		{
			get 
			{
				PublicJourney outwardJourney = (PublicJourney)itinerary.OutwardJourney;
				PublicJourneyDetail lastSegment = (PublicJourneyDetail)outwardJourney.Details[outwardJourney.Details.Length - 1];
				return lastSegment.LegEnd.Location.Description;
			}
		}

		#endregion Properties
        
		#region Public methods

		/// <summary>
		/// Returns a new collection of Ticket objects that contain only the adult or
		/// child tickets (specified by the adultTickets parameteter) from the input
		/// collection
		/// </summary>
		/// <param name="tickets">A collection of tickets objects</param>
		/// <returns>A new collection of Ticket objects or an empty collection if none found
		/// matching the supplied criteria</returns>		
		public static IList FilterTickets(IList tickets, bool adultTickets)
		{
			IList list = new ArrayList();
			foreach ( Ticket ticket in tickets )
			{
				if ( adultTickets && IsAdultTicket( ticket ) )
					list.Add(ticket);
				else if ( !adultTickets && IsChildTicket( ticket ) )
					list.Add(ticket);
			}
			return list;
		}

		/// <summary>
		/// Returns the location name for the origin of the supplied pricing unit
		/// </summary>
		/// <param name="pricingUnit">The pricing unit to return the origin name for</param>
		/// <returns>The location name for the origin</returns>
		public static string FromLocationName(PricingUnit pricingUnit) 
		{
			IList legs = pricingUnit.OutboundLegs;
			return ((PublicJourneyDetail)legs[0]).LegStart.Location.Description;
		}

		/// <summary>
		/// Returns the location name for the destination of the supplied pricing unit
		/// </summary>
		/// <param name="pricingUnit">The pricing unit to return the destination name for</param>
		/// <returns>The location name for the destination</returns>
		public static string ToLocationName(PricingUnit pricingUnit) 
		{
			IList legs = pricingUnit.OutboundLegs;
			return ((PublicJourneyDetail)legs[legs.Count-1]).LegEnd.Location.Description;
		}

        /// <summary>
        /// Creates a new itinerary object based on the supplied journey details. An itinerary
        /// will always be returned but may contain no pricing units if the selected journeys 
        /// have no priceable legs or are private journeys
        /// </summary>
        /// <param name="journey">The set of journeys returned by the CJP</param>
        /// <param name="journeyViewState">The view state indicating the current outbound
        /// (and possibly return) journeys selected by the user</param>
        /// <param name="modeTypes">The modeTypes used to filter journeys displayed to the user, ensures correct 
        /// journey is selected using the viewstate</param>
        /// <returns></returns>
        public static Itinerary CreateItinerary(ITDJourneyResult journey, TDJourneyViewState journeyViewState, TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes)
        {
            return CreateItinerary(journey, journeyViewState, false, modeTypes);
        }

		/// <summary>
		/// Creates a new itinerary object based on the supplied journey details. An itinerary
		/// will always be returned but may contain no pricing units if the selected journeys 
		/// have no priceable legs or are private journeys
		/// </summary>
		/// <param name="journey">The set of journeys returned by the CJP</param>
		/// <param name="journeyViewState">The view state indicating the current outbound
		/// (and possibly return) journeys selected by the user
		/// </param>
		/// <returns>A new itinerary object</returns>
		public static Itinerary CreateItinerary(ITDJourneyResult journey, TDJourneyViewState journeyViewState, bool forceMatchingReturn, TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes) 
		{
			PublicJourney outboundPublicJourney = null;
			PublicJourney returnPublicJourney = null;

			if ((journey != null) && (journeyViewState != null))
			{
				// Get selected outbound public journey
				if( journeyViewState.SelectedOutwardJourneyType == TDJourneyType.PublicOriginal )
                {
                    #region Get Outward journey
                    // If we've been provided modeTypes, then user has been shown a filtered summary of journeys. 
                    // Therefore need to get the correct selected journey from the filtered list
                    if (modeTypes != null)
                    {
                        JourneySummaryLine[] summaryLine = journey.OutwardJourneySummary(journeyViewState.JourneyLeavingTimeSearchType, modeTypes);
                        if ((summaryLine != null) && (summaryLine.Length > 0))
                        {
                            JourneySummaryLine summary = summaryLine[journeyViewState.SelectedOutwardJourney]; // Get the selected summary using index of selected row
                            outboundPublicJourney = journey.OutwardPublicJourney(summary.JourneyIndex); // Get the journey
                        }
                    }
                    else
                    {   // Can assume all journeys are shown to user, and therefore selected journey ID will be correct
                        outboundPublicJourney = journey.OutwardPublicJourney(journeyViewState.SelectedOutwardJourneyID);
                    }
                    #endregion
                }
				else if( journeyViewState.SelectedOutwardJourneyType == TDJourneyType.PublicAmended )
				{
					outboundPublicJourney = journey.AmendedOutwardPublicJourney;
				}

				// Get selected return public journey
				if( journeyViewState.SelectedReturnJourneyType == TDJourneyType.PublicOriginal )
                {
                    #region Get Return journey
                    // If we've been provided modeTypes, then user has been shown a filtered summary of journeys. 
                    // Therefore need to get the correct selected journey from the filtered list
                    if (modeTypes != null)
                    {
                        JourneySummaryLine[] summaryLine = journey.ReturnJourneySummary(journeyViewState.JourneyReturningTimeSearchType, modeTypes);
                        if ((summaryLine != null) && (summaryLine.Length > 0))
                        {
                            JourneySummaryLine summary = summaryLine[journeyViewState.SelectedReturnJourney]; // Get the selected summary using index of selected row
                            returnPublicJourney = journey.ReturnPublicJourney(summary.JourneyIndex); // Get the journey
                        }
                    }
                    else
                    {   // Can assume all journeys are shown to user, and therefore selected journey ID will be correct
                        returnPublicJourney = journey.ReturnPublicJourney(journeyViewState.SelectedReturnJourneyID);
                    }
                    #endregion
                }
				else if( journeyViewState.SelectedReturnJourneyType == TDJourneyType.PublicAmended )
				{
					returnPublicJourney = journey.AmendedReturnPublicJourney;
				}
			}

			// Null journeys will be passed if selected journeys are not public
			return new Itinerary(outboundPublicJourney, returnPublicJourney, forceMatchingReturn);
		}

		/// <summary>
		/// Returns whether a pricedJourneySegment array contains a segment with an unpriced 
		/// pricing unit
		/// </summary>		
		/// <param name="isForReturn">true if the method should check the inward portion
		/// of the journey, false otherwise</param>
		/// <returns>True if a segment with an unpriced pricing unit is found, false otherwise</returns>
		public bool IsFareMissing(bool isForReturn)
		{
			PricedJourneySegment[] pricedJourney = null;

			if (isForReturn) 
			{								
				if (itinerary.ReturnJourney != null)
				{
					pricedJourney = GetReturnPricedJourney();
					return CheckForMissingFare(pricedJourney);
				}
			}
			else
			{				
				if (itinerary.OutwardJourney != null)
				{
					pricedJourney = GetOutwardPricedJourney();
					return CheckForMissingFare(pricedJourney);
				}
			}
			return false;
												
		}


		/// <summary>
		/// Returns true if the itinerary has pricing units for the outward or
		/// return portion as specified. The value is dependent on the current
		/// value of OverrideItineraryType
		/// </summary>
		/// <param name="isForReturn">true if the method should check the inward portion
		/// of the journey, false otherwise</param>
		/// <returns></returns>
		public bool IsJourneyPriced(bool isForReturn)
		{
			IList pricingUnits;
			if (isForReturn)
			{
				if ((itinerary.Type == ItineraryType.Return) && (overrideItineraryType == ItineraryType.Return))
					pricingUnits = OutwardPricingUnits;
				else
					pricingUnits = ReturnPricingUnits;
			}
			else
				pricingUnits = OutwardPricingUnits;

			return (pricingUnits.Count > 0);
		}

        /// <summary>
        /// Returns true if the journey in the itinerary is routing guide compliant.
        /// </summary>
        /// <param name="isForReturn"></param>
        /// <returns></returns>
        public bool IsJourneyRoutingGuideCompliant(bool isForReturn)
        {
            PublicJourney journey = null;
            
            if (isForReturn)
            {
                if (itinerary.ReturnJourney != null)
                {
                    journey = (PublicJourney)itinerary.ReturnJourney;
                }
            }
            else
            {
                if (itinerary.OutwardJourney != null)
                {
                    journey = (PublicJourney)itinerary.OutwardJourney;
                }
            }

            // Only interested in the actual RoutingGuideCompliant value for journeys involving Rail.
            // If the journey only has one leg, then can assume it is compliant
            if ((journey != null) && (journey.JourneyLegs.Length > 1))
            {
                ArrayList modes = new ArrayList(journey.GetUsedModes());

                if (modes.Contains(TransportDirect.JourneyPlanning.CJPInterface.ModeType.Rail))
                {
                    return journey.RoutingGuideCompliantJourney;
                }
            }

            return true;
        }

		/// <summary>
		/// Returns true if the itinerary has pricing units with ticket information for the outward or
		/// return portion as specified. The value is dependent on the current
		/// value of OverrideItineraryType
		/// </summary>
		/// <param name="isForReturn">true if the method should check the inward portion
		/// of the journey, false otherwise</param>
		/// <returns></returns>
		public bool DoesJourneyContainFares(bool isForReturn)
		{
			if (!IsJourneyPriced(isForReturn))
				return false;
			else
			{
				if (isForReturn)
				{
					if ((itinerary.Type == ItineraryType.Return) && (overrideItineraryType == ItineraryType.Return))			
						return CheckForTickets(OutwardPricingUnits,true); //use return fares
					else
						return CheckForTickets(ReturnPricingUnits,false); //use single fares
				}
				else
					if ((itinerary.Type == ItineraryType.Return) && (overrideItineraryType == ItineraryType.Return))			
						return CheckForTickets(OutwardPricingUnits,true); //use return fares
					else
						return CheckForTickets(OutwardPricingUnits,false); //use single fares
			}
		}

		/// <summary>
		/// Returns true if the itinerary has pricing units with ticket information for the outward or
		/// return portion as specified. The value is dependent on the current
		/// value of OverrideItineraryType
		/// </summary>
		/// <returns></returns>
		public bool OtherFaresAvailable()
		{
			if (overrideItineraryType == ItineraryType.Return)			
				return CheckForTickets(ReturnPricingUnits,false);
			else
				return CheckForTickets(OutwardPricingUnits,true); 		
		}


        /// <summary>
        /// Returns true if the itinerary has multiple pricing units, each with a Travelcard
        /// </summary>
        /// <param name="isForReturn">true if the method should check the inward portion
        /// of the journey, false otherwise</param>
        /// <returns></returns>
        public bool DoesJourneyContainMultipleTravelcards(bool isForReturn)
        {
            int travelcardsCount = 0;

            // Travelcards are in the return view, so only show message when return fares shown
            if ((itinerary.Type == ItineraryType.Return) || (overrideItineraryType == ItineraryType.Return))
            {
                IDataServices dataServices = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

                Hashtable travelcardTicketTypes = dataServices.GetHash(DataServiceType.FareTravelcardTicketTypes);

                if (isForReturn)
                {   
                    //Return journey
                    foreach (PricingUnit pu in ReturnPricingUnits)
                    {
                        if (pu.ReturnFares != null)
                        {
                            foreach (Ticket ticket in pu.ReturnFares.Tickets)
                            {
                                if (travelcardTicketTypes.Contains(ticket.ShortCode))
                                {
                                    travelcardsCount++;
                                    break; // Exit this loop and move on to the next PricingUnit
                                }
                            }
                        }
                    }

                }
                else
                {
                    // Outward journey
                    foreach (PricingUnit pu in OutwardPricingUnits)
                    {
                        if (pu.ReturnFares != null)
                        {
                            foreach (Ticket ticket in pu.ReturnFares.Tickets)
                            {
                                if (travelcardTicketTypes.Contains(ticket.ShortCode))
                                {
                                    travelcardsCount++;
                                    break; // Exit this loop and move on to the next PricingUnit
                                }
                            }
                        }
                    }

                }
            }

            return (travelcardsCount > 1);
        }


		/// <summary>
		/// Returns an array of PricedJourneySegment representing the current outward
		/// journey. This array contains an element for every pricing unit, as well as 
		/// an element for every unpriced leg.
		/// </summary>
		/// <returns>PricedJourneySegment array</returns>
		public PricedJourneySegment[] GetOutwardPricedJourney()
		{
			return GetPricedJourney(OutwardPricingUnits, ((PublicJourney)itinerary.OutwardJourney).Details);
		}

		/// <summary>
		/// Returns an array of PricedJourneySegment representing the current inward
		/// journey. This array contains an element for every pricing unit, as well as 
		/// an element for every unpriced leg.
		/// </summary>
		/// <returns>PricedJourneySegment array</returns>
		public PricedJourneySegment[] GetReturnPricedJourney()
		{			
				return GetPricedJourney(ReturnPricingUnits, ((PublicJourney)itinerary.ReturnJourney).Details);
		}

		/// <summary>
		/// Evaluates if through fares are not available
		/// </summary>		
		/// <param name="pricingUnits">A collection of pricing units</param>
		/// <param name="useReturnFares">true if dealing with a matching return</param>
		/// <returns>true if there no through fares available</returns>
		public bool NoThroughFares( bool useReturnFares)
		{
			IList pricingUnits;
		
		
			if (useReturnFares)
			{
				pricingUnits = ReturnPricingUnits;
				if(pricingUnits.Count != 0)
				{
					if(((PricingUnit)pricingUnits[0]).NoThroughFares)
						return true;
				}
				else return false;
			}
			else
			{
				pricingUnits =  OutwardPricingUnits;
				if(pricingUnits.Count != 0)
				if(((PricingUnit)pricingUnits[0]).NoThroughFares)
					return true;
				else return false;
			}
			return false;
		}

        #endregion Public methods
        
        #region Private methods 

		/// <summary>
		/// Generic method used to build an array of PricedJourneySegment from an array
		/// of PricingUnit.
		/// </summary>
		/// <param name="units"></param>
		/// <param name="journey"></param>
		/// <returns></returns>
		private static PricedJourneySegment[] GetPricedJourney(IList units, PublicJourneyDetail[] journey)
		{
			// Initialise results array to max possible length to avoid having to expand it
			ArrayList results = new ArrayList(journey.Length);

			int currentLeg = 0;
			int currentUnit = 0;

			// Go through the pricing units building up a priced journey
			while ( currentUnit < units.Count )
			{
				PricingUnit thisUnit = (PricingUnit)units[currentUnit];
				IList thisUnitLegs = thisUnit.OutboundLegs;

				if (thisUnitLegs[0] == journey[currentLeg])
				{
					results.Add( new PricedJourneySegment(thisUnit, currentLeg == 0, currentLeg + thisUnitLegs.Count == journey.Length) );
					currentLeg += thisUnitLegs.Count;
					currentUnit ++;
				}
				else
				{
					results.Add( new PricedJourneySegment(journey[currentLeg], currentLeg == 0, false));
					currentLeg ++;
				}
			}

			// Take into account any legs at the end that aren't priced
			for (  ; currentLeg < journey.Length; currentLeg ++ )
			{
				results.Add( new PricedJourneySegment(journey[currentLeg], currentLeg == 0, currentLeg == journey.Length - 1));				
			}
			
			return (PricedJourneySegment[])results.ToArray(typeof(PricedJourneySegment));

		}

		/// <summary>
		/// Checks whether an array of pricedJourneySegments contain unpriced pricing units
		/// </summary>
		/// <param name="pricedJourney">array of PricedJourneySegments</param>
		/// <returns>True if an unpriced pricing unit is found, false otherwise</returns>
		private static bool CheckForMissingFare(PricedJourneySegment[] pricedJourney)
		{
			foreach (PricedJourneySegment pjs in pricedJourney)
			{
				if ((pjs.UnitIsPriced == false) && (pjs.Mode != CJPInterfaceAlias.ModeType.Walk) )//check if the pricing unit is not priced
					return true;
			}
			return false;
		}

        
		/// <summary>
		/// Evaluates whether the pricing units contain ticket information
		/// </summary>		
		/// <param name="pricingUnits">A collection of pricing units</param>
		/// <param name="useReturnFares">true if dealing with a matching return</param>
		/// <returns>true if there are tickets contained within the pricing units, false otherwise</returns>
		private static bool CheckForTickets(IList pricingUnits, bool useReturnFares)
		{
			if (useReturnFares)
			{
				foreach (PricingUnit pu in pricingUnits)
				{
					if (pu.ReturnFares !=null)
					{
						IList tickets = pu.ReturnFares.Tickets;
						if (tickets !=null && ( (tickets.Count > 0) || ( pu.ReturnFares.NoPlacesAvailableForReturns ) ) )
							return true;
					}
				}
			}
			else
			{
				foreach (PricingUnit pu in pricingUnits)
				{
					if (pu.SingleFares !=null)
					{
						IList tickets = pu.SingleFares.Tickets;
						if (tickets !=null && ( (tickets.Count > 0) || ( pu.ReturnFares.NoPlacesAvailableForSingles ) ) )
							return true;
					}
				}
			}

			return false;
		}


        /// <summary>
        /// The pricing units for the return journey or an empty collection if none available.
        /// This has been altered to always return the ReturnPricing units due to the integration
        /// of Local Zonal Fares.
        /// </summary>
        /// <returns>A collection of PricingUnit objects</returns>
        private IList determineReturnPricingUnits() 
        {
			return itinerary.ReturnUnits;
        }

		/// <summary>
		/// Indicates if a ticket is suitable for an adult, that is it contains adult fares
		/// </summary>
		/// <param name="ticket"></param>
		/// <returns>true if ticket is suitable for an adult, false otherwise</returns>
		public static bool IsAdultTicket(Ticket ticket)
		{
			return !( float.IsNaN(ticket.AdultFare) && float.IsNaN(ticket.DiscountedAdultFare) );
		}

		/// <summary>
		/// Indicates if a ticket is suitable for a child, ie it contains child fares
		/// </summary>
		/// <param name="ticket"></param>
		/// <returns></returns>
		public static bool IsChildTicket(Ticket ticket)
		{
			return !( float.IsNaN(ticket.ChildFare) && float.IsNaN(ticket.DiscountedChildFare) );
		}

        #endregion Private methods
        
    }
}
