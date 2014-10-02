// *********************************************** 
// NAME			: PricingUnit.cs
// AUTHOR		: 
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the PricingUnit class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/PricingUnit.cs-arc  $
//
//   Rev 1.5   Apr 09 2010 10:28:46   mmodi
//Allow setting up a pricing unit for a RailReplacementBus with Walk leg scenario
//Resolution for 5500: Fares - RF 019 fares for journeys involving rail replacement bus stops
//
//   Rev 1.4   Feb 18 2009 18:15:26   mmodi
//Hold a routing guide compliant flag
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.3   Feb 11 2009 15:28:30   mmodi
//Updated PricingUnit to hold multiple routing guide section Ids
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//Resolution for 5233: Routeing Guide - Reading to Leads - Seperate fares shown for the same routing guide section
//
//   Rev 1.2   Feb 02 2009 18:41:10   rbroddle
//New property MatchedReturnOnNLC & method  RailMatchesByNLC
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.1   Feb 02 2009 16:52:52   mmodi
//Added Routing guide section id for this pricing unit, and pass the routing guide section in to the merge policy
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:36:52   mturner
//Initial revision.
//
//   Rev 1.27   May 25 2007 16:22:14   build
//Automatically merged from branch for stream4401
//
//   Rev 1.26.1.1   May 10 2007 16:50:00   mmodi
//Added public methods to set single and return fares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.26.1.0   May 09 2007 14:43:42   mmodi
//Added CoachFaresReturnComponent flag
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.26   Mar 06 2007 13:43:46   build
//Automatically merged from branch for stream4358
//
//   Rev 1.25.1.0   Mar 02 2007 11:09:34   asinclair
//Added NoThroughFares bool
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.25   Mar 22 2006 20:27:52   rhopkins
//Minor FxCop fixes.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24   Jan 18 2006 18:16:32   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.23   Nov 09 2005 12:31:40   build
//Automatically merged from branch for stream2818
//
//   Rev 1.22.2.1   Oct 28 2005 14:56:38   RPhilpott
//Remove Price() method.
//
//   Rev 1.22.2.0   Oct 22 2005 14:28:10   mguney
//public PricingUnit(PublicJourneyDetail detail) constructor added.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.22   Apr 30 2005 13:47:56   jgeorge
//Updated commenting
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.

using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{

	/// <summary>
	/// Class representing a PricingUnit entity
	/// </summary>
	[Serializable]
	public class PricingUnit
    {
        #region Private members 

        private ModeType mode;
		private string operatorCode;
		
		/// <summary>
		/// True if the unit has return legs
		/// </summary>
		private bool matchingReturn;

		/// <summary>
		/// True if there are no fares returned by the RBO
		/// </summary>
		private bool noThroughFares;

		/// <summary>
		/// True if the returnFares are NX return component fares. This will be used to indicate 
		/// whether the Outward Journey and Return Journey return component fares can be combined
		/// </summary>
		private bool coachFaresReturnComponent;

		/// <summary>
		/// True if the unit includes LU legs
		/// </summary>
		private bool includesUnderground;

        /// <summary>
        /// True if the unit includes a RailReplacementBus with a Walk leg
        /// </summary>
        private bool includesRailReplacementBusWalk = false;
		
		/// <summary>
		/// The outbound legs that comprise the PricingUnit
		/// </summary>
		private ArrayList outboundLeg = new ArrayList();

		/// <summary>
		/// The inbound legs that comprise the PricingUnit. These will only be available if the
		/// whole itinerary is a matched return and the unit is an Outbound unit.
		/// </summary>
		private ArrayList inboundLeg = new ArrayList();
		
		/// <summary>
		/// The single fares associated with the PricingUnit
		/// </summary>
		private PricingResult singleFares;

		/// <summary>
		/// The return fares associated with the PricingUnit
		/// </summary>
		private PricingResult returnFares;

		/// <summary>
		/// The Retailers associated with the PricingUnit
		/// </summary>
		private ArrayList retailers;
		
		/// <summary>
		/// Journey legs identified by the CJP as belonging to this PricingUnit regardless of other business rules
		/// if this set is empty then the standard business rules apply as usual
		/// </summary>
		private int[] mergableIndices = new int[0];

		private ArrayList errorResourceIds = new ArrayList();

        /// <summary>
        /// The routing guide section id(s) if a routing guide section was used in creating this pricing unit
        /// </summary>
        private ArrayList routingGuideSectionIds = new ArrayList();

        /// <summary>
        /// Whether this unit is constructed with a routeing guide compliant section
        /// </summary>
        private bool routingGuideCompliant = true;

        /// <summary>
        /// True if the pricing unit matches because of NLC codes only
        /// </summary>
        private bool matchedReturnOnNLC;


        #endregion

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="detail"></param>
        public PricingUnit()
        {
        }

        /// <summary>
		/// Creates a PricingUnit from a JourneyDetail if appropriate.
		/// </summary>
		/// <param name="detail"></param>
		public PricingUnit(PublicJourneyDetail detail)
		{
			// Check to confirm that we can create a pricing out of this detail
			if (!PricingUnitMergePolicy.WillAccept(detail)) 
			{
				throw new TDException("Attempt to create a PricingUnit for a Journey that can not be priced", false, TDExceptionIdentifier.PRHAttemptToCreateInvalidPricingUnit);
			}
			this.mode = detail.Mode;
			this.operatorCode = detail.Services[0].OperatorCode;
			outboundLeg.Add(detail);			
		}

		#region Properties

		/// <summary>
		/// Read only property for the mode associated with the PricingUnit.
        /// Write provided for RailReplacementBus scenario
		/// </summary>
		public ModeType Mode
		{
			get {return mode;}
            set { mode = value; }
		}

		/// <summary>
		/// Read only property for operator associated with the PricingUnit.
        /// Write provided for RailReplacementBus scenario
		/// </summary>
		public string OperatorCode
		{
			get {return operatorCode;}
            set { operatorCode = value; }
		}

		/// <summary>
		/// Read only property for single prices result
		/// </summary>
		public PricingResult SingleFares
		{
			get {return singleFares;}
		}

		/// <summary>
		/// Read only property for return prices result
		/// </summary>
		public PricingResult ReturnFares
		{
			get {return returnFares;}
		}

		/// <summary>
		/// Read only property for including an underground leg
		/// </summary>
		public bool IncludesUnderground
		{
			get {return includesUnderground;}
		}

        /// <summary>
        /// Read only property for including a RailReplacementBus with a Walk leg.
        /// Write provided for RailReplacementBus scenario
        /// </summary>
        public bool IncludesRailReplacementBusWalk
        {
            get { return includesRailReplacementBusWalk; }
            set { includesRailReplacementBusWalk = value; }
        }

		/// <summary>
		/// Modifiable property for matching return
		/// </summary>
		public bool MatchingReturn
		{
			get {return matchingReturn;}
			set {matchingReturn = value;}
		}


		/// <summary>
		/// Read/Write property for noThroughFares
		/// If true, indicates no fares where returned by the RBO
		/// </summary>
		public bool NoThroughFares
		{
			get {return noThroughFares;}
			set {noThroughFares = value;}
		}

		/// <summary>
		/// Read/Write property for coachFaresReturnComponent
		/// If true, indicates this PricingUnits returnFares are NX return component fares
		/// </summary>
		public bool CoachFaresReturnComponent
		{
			get {return coachFaresReturnComponent;}
			set {coachFaresReturnComponent = value;}
		}

		/// <summary>
		/// Read only property returning outbound legs which make up this pricing unit
		/// </summary>
		public ArrayList OutboundLegs
		{
			get {return outboundLeg;}
		}

		/// <summary>
		/// Read only property returning inbound legs which make up this pricing unit
		/// </summary>
		public ArrayList InboundLegs
		{
			get {return inboundLeg;}
		}

		/// <summary>
		/// Read only property returning retailers for this pricing unit
		/// </summary>
		public ArrayList Retailers
		{
			get {return retailers;}
		}

        /// <summary>
        /// Modifiable property to flag if a pricing unit matches by NLC codes only.
        /// </summary>
        public bool MatchedReturnOnNLC
        {
            get { return matchedReturnOnNLC; }
            set { matchedReturnOnNLC = value; }
        }

        /// <summary>
        /// Read only property returning the routing guide section ids added for this pricing unit
        /// </summary>
        public ArrayList RoutingGuideSectionIds
        {
            get { return routingGuideSectionIds; }
        }

        /// <summary>
        /// Read/write property set if pricing unit applies to compliant routing guide section
        /// </summary>
        public bool RoutingGuideCompliant
        {
            get { return routingGuideCompliant; }
            set { routingGuideCompliant = value; }
        }

		#endregion

		#region Public Static Methods
		
		public static bool WillAccept(PublicJourneyDetail detail)
		{
			return PricingUnitMergePolicy.WillAccept(detail);
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// If applicable adds an extra JourneyDetail to a PricingUnit
		/// </summary>
		/// <param name="detail"></param>
		/// <returns></returns>
		public bool Merge(int detailIndex, PublicJourneyDetail[] journeyDetails, RoutingGuideSection routingGuideSection)		
		{
			PublicJourneyDetail detail = journeyDetails[detailIndex];

            mergableIndices = routingGuideSection.Legs;

            // Determine if we can add the JourneyDetail and act as appropriate
			if (PricingUnitMergePolicy.CanMerge(this, mergableIndices, detailIndex, journeyDetails, routingGuideSection))
			{
				// If we're including an underground journey then track this
				if (detail.Mode == ModeType.Underground) 
				{
					includesUnderground = true;
				}
                // If we're including a walk leg with a railreplacementbus, then track this
                else if ((detail.Mode == ModeType.Walk) && (this.Mode == ModeType.RailReplacementBus))
                {
                    includesRailReplacementBusWalk = true;
                }

				// Check to see if the operator code should be overridden 
				if (PricingUnitMergePolicy.OverrideOperatorCode(this, detail))
				{
					this.operatorCode = detail.Services[0].OperatorCode;
				}
				outboundLeg.Add(detail);
				return true;
			} 
			else 
			{
				return false;
			}

		}

		/// <summary>
		/// Checks to see if two PricingUnits are matching.
		/// This is true only if
		/// - they are of the same mode
		/// - they have the same operator
		// /- the origin of one is the same as the destination of the other
		/// </summary>
		/// <param name="candidate"></param>
		/// <returns></returns>
		public bool Matches(PricingUnit candidate)
		{
			return PricingUnitMergePolicy.AreMatching(this, candidate);
		}

        /// <summary>
        /// Checks to see if two PricingUnits are rail and matching by NLC codes returned by fares requests.
        /// This is true if outbound return origin NLC matches ANY of the inbound single dest NLC's, 
        /// AND the outbound return dest NLC matches ANY of the  
        /// inbound single origin NLC's.
        /// </summary>
        /// <param name="candidate"></param>
        /// <returns></returns>
        public bool RailMatchesByNLC(PricingUnit candidate)
        {
            return PricingUnitMergePolicy.AreRailMatchesByNLC(this, candidate);
        }

        /// <summary>
		/// Adds the PublicJourneyDetail to the outboudoutbound PricingUnit.
        /// Provided for RailReplacementBus scenario
		/// </summary>
		public void AddOutwardLeg(PublicJourneyDetail detail)
		{
            if (detail != null)
            {
                outboundLeg.Add(detail);
            }
		}

		/// <summary>
		/// Adds the information from a return PricingUnit to an outbound PricingUnit.
		/// The laziness below assumes that these are read only lists.
		/// </summary>
		/// <param name="returnUnit"></param>
		public void AddReturnUnit(PricingUnit returnUnit)
		{
			inboundLeg = returnUnit.outboundLeg;
		}

		/// <summary>
		/// Find the Retailers associated with the PricingUnit
		/// </summary>
		/// <param name="retailerCatalogue"></param>
		public void FindRetailers(RetailerCatalogue retailerCatalogue)
		{
			retailers = (ArrayList)(retailerCatalogue.FindRetailers(operatorCode, mode));
            if (retailers == null) 
            {
                string message = "No retailers found, operator code=" + operatorCode +", mode=" + mode;
                OperationalEvent oe = new OperationalEvent(
                    TDEventCategory.Business,
                    TDTraceLevel.Warning,
                    message);
                Logger.Write ( oe );
            }
		}

		/// <summary>
		/// Searches through the tickets in the pricing unit to find the one
		/// that matches the given ticket. If found, the ticket from the pricing unit is
		/// returned, otherwise the supplied ticket is returned unchanged.
		/// This is to ensure that any changes to the ticket that do not alter the fact
		/// that it is the "same" (for example, changes to discount fares) are reflected on the
		/// TicketRetailerInfo objects.
		/// </summary>
		/// <param name="selectedTicket"></param>
		/// <returns></returns>
		public Ticket FindSelectedTicket(Ticket selectedTicket)
		{
			// Need to loop through the tickets in the pricing unit and return the one
			// that matches
			if ( this.SingleFares != null )
			{
				foreach (Ticket t in this.SingleFares.Tickets)
				{
					if ( selectedTicket.Equals( t ) )
					{
						return t;
					}
				}
			}

			if ( this.ReturnFares != null )
			{
				foreach (Ticket t in this.ReturnFares.Tickets)
				{
					if ( selectedTicket.Equals( t ) )
					{
						return t;
					}
				}
			}

			return selectedTicket;
		}

		/// <summary>
		/// Set the Fares associated with the PricingUnit.
		/// </summary>
		/// <param name="singleFares"></param>
		/// <param name="returnFares"></param>
		public void SetFares(PricingResult singleFares, PricingResult returnFares)
		{
			this.singleFares = singleFares;
			this.returnFares = returnFares;
		}

		/// <summary>
		/// Set the Single Fares associated with the PricingUnit.
		/// </summary>
		/// <param name="singleFares"></param>
		public void SetFaresSingle(PricingResult singleFares)
		{
			this.singleFares = singleFares;
		}

		/// <summary>
		/// Set the Return Fares associated with the PricingUnit.
		/// </summary>
		/// <param name="returnFares"></param>
		public void SetFaresReturn(PricingResult returnFares)
		{
			this.returnFares = returnFares;
		}


		/// <summary>
		/// Resource ids for text of message(s) to be displayed
		/// to user as a result of any errors during processing
		/// </summary>
		public string[] ErrorResourceIds
		{
			get { return (string[])(this.errorResourceIds.ToArray(typeof(string))); } 
		}


		/// <summary>
		/// Add resource id for an error msg, but only if this
		/// one is not already present in the msg array ...
		/// </summary>
		public void AddErrorMessage(string resourceId) 
		{
			foreach (string rid in errorResourceIds)
			{
				if	(rid.Equals(resourceId))
				{
					return;
				}
			}

			errorResourceIds.Add(resourceId);
		}

        /// <summary>
        /// Adds the provided Routing Guide Section Id to this pricing unit
        /// </summary>
        /// <param name="routingGuideSectionId"></param>
        public void AddRoutingGuideSectionId(int routingGuideSectionId)
        {
            if (!ContainsRoutingGuideSectionId(routingGuideSectionId))
            {
                routingGuideSectionIds.Add(routingGuideSectionId);
            }
        }

        /// <summary>
        /// Returns true if the specified Routing Guide Section Id is used by 
        /// this Pricing Unit. (Assumes caller has previously added Ids to this pricing unit)
        /// </summary>
        /// <param name="routingGuideSectionId"></param>
        /// <returns></returns>
        public bool ContainsRoutingGuideSectionId(int routingGuideSectionId)
        {
            if (routingGuideSectionIds.Contains(routingGuideSectionId))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

		#endregion
	
		#region Private Methods

		#endregion
	}
}
