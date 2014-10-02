// *********************************************** 
// NAME			: PricingUnitMergePolicy.cs
// AUTHOR		: Alistair Caunt
// DATE CREATED	: 05/10/03
// DESCRIPTION	: Implementation of the PricingUnitMergePolicy class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/PricingUnitMergePolicy.cs-arc  $
//
//   Rev 1.6   Apr 09 2010 10:29:18   mmodi
//Allow setting up a pricing unit for a RailReplacementBus with Walk leg scenario
//Resolution for 5500: Fares - RF 019 fares for journeys involving rail replacement bus stops
//
//   Rev 1.5   Feb 18 2009 18:15:52   mmodi
//Make use of the Unit routing guide compliant flag
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.4   Feb 16 2009 12:18:46   rbroddle
//Updated "AreRailMatchesByNLC" to reflect fact that we are now pricing the itinerary twice.
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.3   Feb 11 2009 15:28:32   mmodi
//Updated PricingUnit to hold multiple routing guide section Ids
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//Resolution for 5233: Routeing Guide - Reading to Leads - Seperate fares shown for the same routing guide section
//
//   Rev 1.2   Feb 02 2009 18:46:16   rbroddle
//Added method AreRailMatchesByNLC
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.1   Feb 02 2009 16:55:36   mmodi
//Use the routing guide section to influence the merge
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:36:52   mturner
//Initial revision.
//
//   Rev 1.21   Jan 18 2006 18:16:34   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.20   Jan 17 2006 17:59:36   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//

using System;
using System.Collections;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.CoachFares;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Describes the various policies for when PricingUnits can be created or added to one another.
	/// </summary>
	[Serializable]
	public sealed class PricingUnitMergePolicy
	{

		private const string nxCodeProperty  = "PricingRetail.Domain.PricingUnitMergePolicy.NXCode";
		private const string sclCodeProperty = "PricingRetail.Domain.PricingUnitMergePolicy.SCLCode";
        private const string ttboRegionProperty = "PricingRetail.Domain.PricingUnitMergePolicy.TTBORegion";

        /// <summary>
        /// Constructor is private to prevent instantiation
        /// </summary>
        private PricingUnitMergePolicy() 
        {
        }

		/// <summary>
		/// Policy method to determine if we can create a new PricingUnit from a JourneyDetail
		/// </summary>
		/// <param name="detail"></param>
		/// <returns></returns>
		public static bool WillAccept(PublicJourneyDetail detail)
		{
			bool willAccept = false;

			// We can create a PricingUnit if
			// a) the mode is train
			if (IsRailMode(detail.Mode)) 
			{
				willAccept = true;
			}
			// or, b) the mode is coach
			else if (detail.Mode == ModeType.Coach) 
			{
				// and the operator code belongs to NX or SCL

				ICoachOperatorLookup lookup = (ICoachOperatorLookup)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];
			
				CoachOperator op = lookup.GetOperatorDetails(detail.Services[0].OperatorCode);
	
				if (op != null)
				{
					string nxCode	= Properties.Current[nxCodeProperty];
					string sclCode	= Properties.Current[sclCodeProperty];
					
					if	(op.OperatorCode.Equals(nxCode) || op.OperatorCode.Equals(sclCode))
					{
						willAccept = true;
					}
				}
			}

			return willAccept;
		}

		/// <summary>
		/// Policy method to determine if we can add a new JourneyDetail to an existing PricingUnit.
        /// </summary>
        /// <param name="unit">Pricing unit to add to</param>
        /// <param name="mergableIndices">CJP identified indexes which are ok to Merge (they are routing guide compliant)</param>
        /// <param name="journeyDetails">PublcJourneyDetails for the journey</param>
        /// <param name="detailIndex">Index of the PublicJourneyDetail to check for merge</param>
        /// <param name="routingGuideCompliant">Is the detail being checked Routing Guide compliant</param>
        /// <returns>If the PublicJourneyDetail may be merged to the pricing unit</returns>
		public static bool CanMerge(
			PricingUnit unit,
			int[] mergableIndices,
			int detailIndex,
			PublicJourneyDetail[] journeyDetails,
            RoutingGuideSection routingGuideSection)
		{
			PublicJourneyDetail detail = journeyDetails[detailIndex];
			bool last = (detailIndex == journeyDetails.Length - 1);
			bool canMerge = false;
			bool matchingIndex = false;
            bool routingGuideCompliant = routingGuideSection.Compliant;
            bool sameRoutingGuideSection = unit.ContainsRoutingGuideSectionId(routingGuideSection.Id);
            bool unitRoutingGuideCompliant = unit.RoutingGuideCompliant;

            // determine if the index of the detail is one of these identified by the CJP as belonging to the PricingUnit
			for (int i=0;i<mergableIndices.Length;++i) 
			{
				if (mergableIndices[i] == detailIndex) 
				{
					matchingIndex = true;
					break;
				}
			}

			// We can merge a JourneyDetail into a PricingUnit if:
			// a) the index of the JourneyDetail has been identified by the CJP, 
            //      AND it is routing guide compliant
            //      AND they are for the same routing guide section
			if (matchingIndex && routingGuideCompliant && sameRoutingGuideSection)
			{
				canMerge = true;
			}
			// b) the PricingUnit is for Rail 
			else if (IsRailMode(unit.Mode))
			{
				// and (i) and the JourneyDetail is for rail, 
                //          AND it is routing guide compliant
                //          AND they are in the same routing guide section
                if ((IsRailMode(detail.Mode)) && routingGuideCompliant && sameRoutingGuideSection)
				{
                    // Shouldn't enter here because the MatchingIndex check will have picked up
                    // rail legs in the same routing guide section
					canMerge = true;
				}
				// or (ii) JourneyDetail is for underground or walking and it isn't the last leg of the journey, 
				else if ((detail.Mode == ModeType.Underground || detail.Mode == ModeType.Walk) && last == false && unitRoutingGuideCompliant)
                {
                    #region Merge Walk/Underground leg for a rail->walk/underground/walk->rail scenario
                    // SWillcock - change for Vantive 3377643 / IR 1762
					// if there are more rail / underground JourneyDetails after this JourneyDetail
					// and no intervening steps that are not walking or underground
					// then this can merge otherwise not
					for(int i = (detailIndex + 1); i < journeyDetails.Length; i++)
					{
						if	(IsRailMode(journeyDetails[i].Mode))
						{
							canMerge = true;
							break;
						}

						if	(!(journeyDetails[i].Mode == ModeType.Underground || journeyDetails[i].Mode == ModeType.Walk))
						{
							canMerge = false;
							break;
						}
                    }
                    #endregion
                }
                // or (iii) JourneyDetail is for Rail but is for another routing guide section, and directly preceeding
                //          it there is a Walk/Underground preceeded by a Train, then ok to merge.
                //          This satisfies being able to merge the second train back into a 
                //          Rail -> walk/underground/walk -> Rail scenario which is also tackled by (ii) above
                //          which merges the middle legs
                else if ((IsRailMode(detail.Mode)) && routingGuideCompliant && !sameRoutingGuideSection)
                {
                    #region Merge 2nd Rail leg for a rail->walk/underground/walk->rail scenario
                    for (int i = (detailIndex - 1); i >= 0; i--)
                    {
                        // If this is Rail mode and the previous leg mode is not rail. This stops two
                        // consecutive rail legs which are NOT part of the same routing guide being merged.
                        if ((IsRailMode(journeyDetails[i].Mode)) && (i != (detailIndex - 1)))
                        {
                            canMerge = true;
                            
                            // Because this is a differemt RGS, associate this RGS Id with the Pricing unit
                            unit.AddRoutingGuideSectionId(routingGuideSection.Id);

                            break;
                        }
                            // The previous leg to this one is Rail so can't merge
                        else if (IsRailMode(journeyDetails[i].Mode))
                        {
                            canMerge = false;
                            break;
                        }

                        if (!(journeyDetails[i].Mode == ModeType.Underground || journeyDetails[i].Mode == ModeType.Walk))
                        {
                            canMerge = false;
                            break;
                        }
                    }
                    #endregion
                }
                // or (iv) JourneyDetail is for a Walk leg, and directly preceeded it there is a 
                //         RailReplacementBus leg, and the walk is the last leg, and the Walk and RRB were
                //         provided by the TTBO, then ok to merge.
                //         This satisfies being able to plan fares for a RailReplacementBus -> Walk journey
                //         which (may temporarily) replace a Rail station to Rail station journey, which is now
                //         a Rail station (RRB to) -> Bus stop (Walk to) -> Rail station
                else if ((detail.Mode == ModeType.Walk) && (unit.Mode == ModeType.RailReplacementBus) && last && unitRoutingGuideCompliant)
                {
                    #region Merge Walk leg into a RailReplacementBus for railreplacementbus->walk scenario

                    #region TTBO region value
                    string ttboRegion = Properties.Current[ttboRegionProperty];
                    
                    // Set default value if property missing
                    if (string.IsNullOrEmpty(ttboRegion))
                    {
                        ttboRegion = "TT";
                    }
                    #endregion

                    // Only check if Walk is from TTBO
                    if (detail.Region == ttboRegion)
                    {
                        // Sanity check there is a preceeding detail
                        if ((journeyDetails.Length > 1) && (detailIndex >= 1))
                        {
                            // If this is Walk mode supplied by TTBO, and the previous leg mode is RailReplacementBus 
                            // also supplied by TTBO, then OK to merge. Any other combination, should not merge
                            // as there is no assurance that its a valid fare combination. Can safely assume if the 
                            // TTBO passed the walk leg, then they're ok with it and can fare it.
                            if (journeyDetails[detailIndex - 1].Mode == ModeType.RailReplacementBus)
                            {
                                if (journeyDetails[detailIndex - 1].Region == ttboRegion)
                                {
                                    canMerge = true;
                                }
                            }
                        }
                    }

                    #endregion
                }
			}
			// or c) they are both of mode Coach
			else if (unit.Mode == ModeType.Coach && detail.Mode == ModeType.Coach) 
			{
				ICoachOperatorLookup lookup = (ICoachOperatorLookup)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];
			
				CoachOperator op1 = lookup.GetOperatorDetails(unit.OperatorCode);
				CoachOperator op2 = lookup.GetOperatorDetails(detail.Services[0].OperatorCode);

				// and both operators are the same (in practice, means both are NX or both are SCL)  
				
				if	(op1 != null && op2 != null)
				{
					if	(op1.OperatorCode.Equals(op2.OperatorCode))
					{
						canMerge = true;
					}
				}
			}
			return canMerge;
		}

        /// <summary>
        /// Returns true if the supplied travel mode is regarded as a rail mode with respect
        /// to either a journey leg or a pricing unit
        /// </summary>
        /// <param name="mode">Mode of journey leg or pricing unit</param>
        /// <returns>True if the supplied mode is regarded as a rail mode, false otherwise</returns>
        public static bool IsRailMode(ModeType mode) 
        {
            return mode == ModeType.Rail || mode == ModeType.RailReplacementBus;
        }

		/// <summary>
		/// Policy method to determine if two pricing units are matching
		/// </summary>
		/// <param name="u1"></param>
		/// <param name="u2"></param>
		/// <returns></returns>
		public static bool AreMatching(PricingUnit u1, PricingUnit u2)
		{
			bool matching = false;

			// Determine the start and end locations of the two pricing units
			TDLocation u1Start = ((PublicJourneyDetail)u1.OutboundLegs[0]).LegStart.Location;
			TDLocation u1End = ((PublicJourneyDetail)u1.OutboundLegs[u1.OutboundLegs.Count-1]).LegEnd.Location;
			TDLocation u2Start = ((PublicJourneyDetail)u2.OutboundLegs[0]).LegStart.Location;
			TDLocation u2End= ((PublicJourneyDetail)u2.OutboundLegs[u2.OutboundLegs.Count-1]).LegEnd.Location;

			// For two pricing units to be matching they must 
			// a) have oppposite start and end locations
			
			if (u1Start.IsMatchingNaPTANGroup(u2End) && u1End.IsMatchingNaPTANGroup(u2Start))
			{	
				// and, b) they have the same mode
                if ( u1.Mode == u2.Mode
                    || (u1.Mode == ModeType.Rail && u2.Mode == ModeType.RailReplacementBus)
                    || (u1.Mode == ModeType.RailReplacementBus && u2.Mode == ModeType.Rail) ) 
				{
					// and i) they are both Rail (rail replacement)
					if (IsRailMode(u1.Mode)) 
					{
						matching = true;
					}
					// or ii) they are by coach and both operators are the same 
					//   (in practice, means both are NX or both are SCL)  
					
					else 
					{
						ICoachOperatorLookup lookup = (ICoachOperatorLookup)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];
			
						CoachOperator op1 = lookup.GetOperatorDetails(u1.OperatorCode);
						CoachOperator op2 = lookup.GetOperatorDetails(u2.OperatorCode);
						
						if	(op1 != null && op2 != null)
						{
							if (op1.OperatorCode.Equals(op2.OperatorCode)) 
							{
								matching = true;
							}
						}
					}
				}
			}
			return matching;
		}


        /// <summary>
        /// Policy method to determine if two rail pricing units have tickets with matching NLC's
        /// </summary>
        /// <param name="u1"></param>
        /// <param name="u2"></param>
        /// <returns></returns>
        public static bool AreRailMatchesByNLC(PricingUnit u1, PricingUnit u2)
        {
            bool matching = false;

            if ((u1.ReturnFares != null) && (u2.SingleFares != null) && IsRailMode(u1.Mode) && IsRailMode(u2.Mode))
            {
                //Check each outbound return ticket against all inbound single tickets - if outbound return origin NLC 
                //matches ANY of the inbound single dest NLC's, AND the outbound return dest NLC matches ANY of the  
                //inbound single origin NLC's, then that outbound return ticket can be considered matching and by
                //extension the two pricing units can be considered to match as far as location is concerend.  In this
                //case we can skip the usual check on NaPTAN groups.
                bool outOriginMatchesInDest = false;
                bool outDestMatchesInOrigin = false;

                String outReturnOriginNLC, outReturnDestNLC, inSingleOriginNLC, inSingleDestNLC;

                //Check each outbound return NLC against the list of inbound singles
                foreach (Domain.Ticket outRtnTicket in u1.ReturnFares.Tickets)
                {
                    outDestMatchesInOrigin = false;
                    outOriginMatchesInDest = false;
                    //Use ...NLcActual properties returned by ZPBO if they are populated as they are more likely to be a match
                    outReturnDestNLC = (!String.IsNullOrEmpty(outRtnTicket.TicketRailFareData.DestinationNlcActual) ? outRtnTicket.TicketRailFareData.DestinationNlcActual : outRtnTicket.TicketRailFareData.DestinationNlc);
                    outReturnOriginNLC = (!String.IsNullOrEmpty(outRtnTicket.TicketRailFareData.OriginNlcActual) ? outRtnTicket.TicketRailFareData.OriginNlcActual : outRtnTicket.TicketRailFareData.OriginNlc);
                    //now check it against all the singles to see if it's a match
                    foreach (Domain.Ticket inSingleTicket in u2.SingleFares.Tickets)
                    {
                        //Use ...NLcActual properties returned by ZPBO if they are populated as they are more likely to be a match
                        inSingleOriginNLC = (!String.IsNullOrEmpty(inSingleTicket.TicketRailFareData.OriginNlcActual) ? inSingleTicket.TicketRailFareData.OriginNlcActual : inSingleTicket.TicketRailFareData.OriginNlc);
                        inSingleDestNLC = (!String.IsNullOrEmpty(inSingleTicket.TicketRailFareData.DestinationNlcActual) ? inSingleTicket.TicketRailFareData.DestinationNlcActual : inSingleTicket.TicketRailFareData.DestinationNlc);

                        //Flag if inbound single origin matches the outbound return dest NLC 
                        if (inSingleOriginNLC == outReturnDestNLC) { outDestMatchesInOrigin = true; }
                        //Flag if inbound single dest NLC matches the outbound return origin NLC
                        if (inSingleDestNLC == outReturnOriginNLC) { outOriginMatchesInDest = true; }
                    }
                    //If matches for both origin and dest were found then we have a matching return ticket - flag
                    //the ticket as matching & set  "faresMatchOnNLCs" flag so we can treat the whole PU as a match
                    if (outOriginMatchesInDest && outDestMatchesInOrigin)
                    {
                        outRtnTicket.MatchingNLCreturn = true;
                        u1.MatchedReturnOnNLC = true;
                        u2.MatchedReturnOnNLC = true;
                        matching = true;
                    }
                }
            }
            return matching;
        }


		/// <summary>
		/// Indicates if the merged in JourneyDetail should override the operator code of the PricingUnit
		/// Typically this isn't the case, but if the new JourneyDetail is NX then we do.
		/// </summary>
		/// <param name="unit"></param>
		/// <param name="detail"></param>
		/// <returns></returns>
		public static bool OverrideOperatorCode(PricingUnit unit, PublicJourneyDetail detail)
		{
			bool overRide = false;
			
			// If we're merging with a NX leg and the current PricingUnit has a non-NX code, we adopt the NX code
			
			if (unit.Mode == ModeType.Coach)			
			{
				ICoachOperatorLookup lookup = (ICoachOperatorLookup)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];
			
				CoachOperator op1 = lookup.GetOperatorDetails(detail.Services[0].OperatorCode);
				CoachOperator op2 = lookup.GetOperatorDetails(unit.OperatorCode);
	
				if (op1 != null && op2 != null)
				{
					string nxCode	= Properties.Current[nxCodeProperty];
					
					if	(op1.OperatorCode.Equals(nxCode) && !op2.OperatorCode.Equals(nxCode))
					{
						overRide = true;
					}
				}
			}
			return overRide;
		}
	}
}
