// *********************************************** 
// NAME			: Itinerary.cs
// AUTHOR		: Alastair Caunt 
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the Itinerary class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/Itinerary.cs-arc  $
//
//   Rev 1.6   Apr 09 2010 10:27:58   mmodi
//Allow setting up a pricing unit for a RailReplacementBus with Walk leg scenario
//Resolution for 5500: Fares - RF 019 fares for journeys involving rail replacement bus stops
//
//   Rev 1.5   Feb 18 2009 18:34:42   mmodi
//Set the Units routing guide compliant flag
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.4   Feb 11 2009 15:28:30   mmodi
//Updated PricingUnit to hold multiple routing guide section Ids
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//Resolution for 5233: Routeing Guide - Reading to Leads - Seperate fares shown for the same routing guide section
//
//   Rev 1.3   Feb 02 2009 19:14:38   rbroddle
//Made CheckMatching public
//
//   Rev 1.2   Feb 02 2009 18:27:38   rbroddle
//Minor change to CheckMatching method.
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.1   Feb 02 2009 16:51:28   mmodi
//Pass in applicable routing guide section when creating pricing units
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:36:50   mturner
//Initial revision.
//
//   Rev 1.25   Apr 26 2006 12:13:36   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.24.1.0   Mar 29 2006 18:37:10   RPhilpott
//Add property to get modes, to support find-cheaper changes.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.24   Jan 18 2006 18:16:32   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.23   Jan 17 2006 17:56:54   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.22   Nov 09 2005 12:31:40   build
//Automatically merged from branch for stream2818
//
//   Rev 1.21.2.1   Oct 28 2005 14:55:44   RPhilpott
//Remove Price() method (now performed by TimeBasedPriceSupplier).
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.21.2.0   Oct 21 2005 18:15:48   jgeorge
//Initial modifications for search by time. Unfinished.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.21   Apr 28 2005 14:11:48   jgeorge
//Added options to force the itinerary type to return
//Resolution for 2309: PT - Train - Destination wrong on rail ticket description.
//
//   Rev 1.20   Mar 18 2005 09:35:10   jgeorge
//Updated commenting

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	
	/// <summary>
	/// Represents pricing information for a journey (single or return). Will include 
	/// single and return tickets for the given outward and return journey (return is used
	/// interchangably with inward, which can be confusing). Create the itinerary by passing
	/// in the outward and inward journey. This will split the journey up into "Pricing units",
	/// which are sections of the journey for which it should be possible to buy a ticket. Then
	/// call the Price method to look up ticket information for the itinerary and specified
	/// discount card. 
	/// </summary>
    [Serializable()]
    public class Itinerary
	{
		/// <summary>
		/// Atkins references their Legs starting with an index of 0, so we do the same
		/// </summary>
		private const int INDEX_START = 0;
        private const string ttboRegionProperty = "PricingRetail.Domain.PricingUnitMergePolicy.TTBORegion";
		
		private Journey outwardJourney;
		private Journey returnJourney;
        private ItineraryType typeOfItinerary;
		private ArrayList outwardUnits = new ArrayList();
		private ArrayList returnUnits  = new ArrayList();
		private bool retailersInitialised;
        private bool faresInitialised;
		
		/// <summary>
		/// Public constructor. Pricing units for the specified journeys will be
		/// created automatically.
		/// </summary>
		/// <param name="outwardJourney"></param>
		/// <param name="returnJourney"></param>
		public Itinerary(Journey outwardJourney, Journey returnJourney) : this(outwardJourney, returnJourney, false)
		{ }

		/// <summary>
		/// Alternative constructor. If the return journey is present, and both outward and return
		/// journeys break down into the same number of pricing units, then setting forceMatchingReturn
		/// to true will ensure that the itinerary type is set to Return even if the origin/destination
		/// locations do not match. This is a hack for cost based searching, were you can get a return
		/// ticket going from one station and coming back to a different one within the same fare group -
		/// eg London Kings Cross - Cambridge, returning to London Liverpool Street. It is not applicable
		/// to time based searching at time of writing.
		/// </summary>
		/// <param name="outwardJourney"></param>
		/// <param name="returnJourney"></param>
		/// <param name="forceMatchingReturn"></param>
		public Itinerary(Journey outwardJourney, Journey returnJourney, bool forceMatchingReturn)
		{
			this.outwardJourney = outwardJourney;
			this.returnJourney = returnJourney;
			CreatePricingUnits(forceMatchingReturn);
		}

		#region Properties
		
		/// <summary>
		/// Read only property for the outward pricing units
		/// </summary>
		public ArrayList OutwardUnits
		{
			get { return outwardUnits;}
		}

		/// <summary>
		/// Read only property for outward journey
		/// </summary>
		public Journey OutwardJourney
		{
			get { return outwardJourney; }
		}

		/// <summary>
		/// Read only property for the return (inward) journey.
		/// </summary>
		public Journey ReturnJourney
		{
			get { return returnJourney; }
		}	

		/// <summary>
		/// Read only property for the return (inward) pricing units
		/// </summary>
		public ArrayList ReturnUnits
		{
			get {return returnUnits;}
		}

		/// <summary>
		/// Read only property for the travel modes in the outward units 
		/// </summary>
		public ModeType[] OutwardModes
		{
			get 
			{
				ModeType[] modes = new ModeType[outwardUnits.Count]; 
				
				int i = 0;

				foreach (PricingUnit pu in outwardUnits)
				{
					modes[i++] = pu.Mode;
				}
	
				return modes;
			}
		}

		/// <summary>
		/// Read only property for the travel modes in the return units 
		/// </summary>
		public ModeType[] ReturnModes
		{
			get 
			{
				ModeType[] modes = new ModeType[returnUnits.Count]; 
				
				int i = 0;

				foreach (PricingUnit pu in returnUnits)
				{
					modes[i++] = pu.Mode;	
				}
	
				return modes;
			}
		}

        /// <summary>
        /// Read only property for the itinerary type.
        /// </summary>
        public ItineraryType Type
        {
            get {return typeOfItinerary;}
        }
		
        /// <summary>
        /// Read only property returns true if FindRetailers has been called
        /// </summary>
        public bool RetailersInitialised
        {
            get {return retailersInitialised;}
        }
        
        /// <summary>
        /// Indicates if the Itinerary has been priced (read/write).
        /// </summary>
        public bool FaresInitialised
        {
            get { return faresInitialised; }
			set { faresInitialised = value; }
        }
        
		#endregion

		# region Public Methods

		/// <summary>
		/// Associate Retailers with the PricingUnits of an Itinerary
		/// </summary>
		public void FindRetailers()
		{
			RetailerCatalogue retailerCatalogue = (RetailerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.RetailerCatalogue];

			foreach (PricingUnit pricingUnit in outwardUnits) 
			{
				pricingUnit.FindRetailers(retailerCatalogue);
			}

			foreach (PricingUnit pricingUnit in returnUnits)
			{
				pricingUnit.FindRetailers(retailerCatalogue);
			}
			retailersInitialised = true;
		}

		# endregion

		#region Private Methods
		
		/// <summary>
		/// Create the sets of PricingUnits associated with the outward and return journeys.
		/// The unit are first created according to the journey rules and are then update if we discover
		/// that we have matching outward and return journeys.
		/// </summary>
		private void CreatePricingUnits(bool forceMatchingReturn)
		{
			// Create the PricingUnits with the basic information
			outwardUnits = PopulatePricingUnitList(outwardJourney);
			returnUnits = PopulatePricingUnitList(returnJourney);
			
			// Check to see if we have matching outward and return Journeys and process if necessary.
			CheckMatching(forceMatchingReturn);
		}

		/// <summary>
		/// Create a list of PricingUnits for a journey based on the PricingUnit merging rules.
		/// </summary>
		/// <param name="journey"></param>
		/// <returns></returns>
		private ArrayList PopulatePricingUnitList(Journey journey)
		{
			PricingUnit currentUnit = null;
			TransportDirect.UserPortal.JourneyControl.PublicJourney pJourney = journey as TransportDirect.UserPortal.JourneyControl.PublicJourney;
			ArrayList units = new ArrayList();

            // we only create PricingUnits if the journey is by public transport
			if (pJourney != null) 
			{
				// Now create the units
				int index = INDEX_START;

				foreach(PublicJourneyDetail detail in pJourney.Details)
				{
					// If the currentUnit isn't null, attempt to merge in the current details
					// If that isn't possible see if we can create a new pricing unit out of it.
					// If we can merge in the current details then that will happen automatically
					// ( the last parameter in Merge is a flag to indicate if we're adding the last of the journey details within the journey

                    // The Routing guide section for this leg is also used in determing if the current details
                    // can be merged
                    RoutingGuideSection routingGuideSection = pJourney.GetRoutingGuideSection(index);
                    
					if (currentUnit == null || !currentUnit.Merge(index, pJourney.Details, routingGuideSection))
					{
						// We can only create a PricingUnit for certain type of journey, this check to see if we can
						if (PricingUnit.WillAccept(detail)) 
						{
							currentUnit = new PricingUnit(detail);
                            currentUnit.AddRoutingGuideSectionId(routingGuideSection.Id);
                            currentUnit.RoutingGuideCompliant = routingGuideSection.Compliant;
							units.Add(currentUnit);
						}
                        // Scenario where we need to merge a Walk -> RailReplacementBus -> Walk returned from the
                        // TTBO into single pricing unit. The end walk leg is picked up in the Merge, so only need to 
                        // detect the first walk
                        else if ((index == INDEX_START) && (detail.Mode == ModeType.Walk))
                        {
                            #region Allow a start Walk followed by RailReplacementBus scenario to go through

                            #region TTBO region value
                            string ttboRegion = Properties.Current[ttboRegionProperty];

                            // Set default value if property missing
                            if (string.IsNullOrEmpty(ttboRegion))
                            {
                                ttboRegion = "TT";
                            }
                            #endregion

                            if (detail.Region == ttboRegion)
                            {
                                // Check next leg is a RRB from the TTBO
                                if ((pJourney.Details.Length > index + 1))
                                {
                                    PublicJourneyDetail nextDetail = pJourney.Details[index + 1];

                                    if ((nextDetail.Mode == ModeType.RailReplacementBus) && (nextDetail.Region == ttboRegion))
                                    {
                                        // Next leg satisfies the RailReplacementBus scenario, create the unit
                                        // for the detail, and populate essential values with the nextdetail
                                        currentUnit = new PricingUnit();
                                        currentUnit.AddOutwardLeg(detail);
                                        currentUnit.Mode = nextDetail.Mode;
                                        currentUnit.OperatorCode = nextDetail.Services[0].OperatorCode;
                                        currentUnit.IncludesRailReplacementBusWalk = true;
                                        currentUnit.AddRoutingGuideSectionId(pJourney.GetRoutingGuideSection(index + 1).Id);
                                        currentUnit.RoutingGuideCompliant = pJourney.GetRoutingGuideSection(index + 1).Compliant;
                                        units.Add(currentUnit);
                                    }
                                }
                            }
                            else
                            {
                                currentUnit = null;
                            }

                            #endregion
                        }
                        // If we can't create a new PricingUnit out of the current journey leg. Set the current PricingUnit to null
                        // to ensure that we don't add a subsequent journey leg to the last pricingUnit.
                        else
                        {
                            currentUnit = null;
                        }
					} 
					++ index;
				}
			}
			return units;
		}

		/// <summary>
		/// Checks to see if the outward and return Journeys of the Itinerary are matching and if so 
		/// updates the PricingUnits accordingly.
		/// </summary>
        public void CheckMatching(bool forceMatchingReturn)
        {
            // First see if we have the same number of PricingUnits in each direction
            bool matching = (outwardUnits.Count == returnUnits.Count);

            // If we have then check to see if the individual PricingUnits match
            PricingUnit pOut;
            PricingUnit pRet;
            int n = outwardUnits.Count;

			// If matching is true, we need to verify that the outward units do match the return ones
			// However, we bypass this step if forceMatchingReturn is true

            if (matching && !forceMatchingReturn) {
                for (int i=0; i<n; ++i) 
                {
                    pOut = (PricingUnit)outwardUnits[i];
                    pRet = (PricingUnit)returnUnits[n-1-i];
                    matching = matching && (pOut.Matches(pRet) || pOut.RailMatchesByNLC(pRet));
                    if (!matching) 
                    {
                        break;
                    }
                }
            }

            // if we have matching then set the InboundLeg of the outward Units and mark the return Units as MatchingReturn;
            if (matching) 
            {
                for (int i=0; i<n; ++i) 
                {
                    pOut = (PricingUnit)outwardUnits[i];
                    pRet = (PricingUnit)returnUnits[n-1-i];
                    pOut.AddReturnUnit(pRet);
                    pRet.MatchingReturn = true;
                }
            }

            // determine whether the itinerary type is single or return
            if (returnJourney == null) 
            {
                typeOfItinerary = ItineraryType.Single;
            } 
            else 
            {
                if (matching) 
                {
                    typeOfItinerary = ItineraryType.Return;
                } 
                else 
                {
                    typeOfItinerary = ItineraryType.Single;
                }
            }
            
		}
		# endregion
	}
}
