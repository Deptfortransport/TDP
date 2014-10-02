//********************************************************************************
//NAME         : RetailBusinessObjectsFacade.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RetailBusinessObjectsFacade.cs-arc  $
//
//   Rev 1.16   Dec 05 2012 13:59:56   mmodi
//Removed code warnings
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.15   Oct 04 2012 13:34:26   mmodi
//Added logging for fare availability checks
//Resolution for 5856: Fares - Sleeper fare ticket shown when it is not available
//
//   Rev 1.14   Oct 05 2010 10:45:58   apatel
//Updated to include a check for day return ticket's validity for the return journey in search by price
//Resolution for 5614: Rail Search By Price - Invalid day return tickets shown
//
//   Rev 1.13   Jul 30 2010 09:20:40   mmodi
//In search by price, only include routeing guide validation if there is at least one train with the fare route code. 
//Resolution for 5295: Fares - SbP - Journey planned is not to original station selected
//
//   Rev 1.12   Jun 03 2010 11:07:22   RBroddle
//Increased size of FAGFOutputLength buffer by 50% as was not sufficient for some fares returns where a discount card was used.
//Resolution for 5543: Euston - Glasgow Central out 11:30 rtn 10:40 with disabled railcard - no fares shown.
//
//   Rev 1.11   Jun 03 2010 09:36:52   mmodi
//Updated to perform RBO GL call and RBO MR call for Search By Price requests
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.10   May 06 2010 13:38:38   mmodi
//Moved restrict fares logging into RestrictFaresMR so can see which fares are dropped for each journey being validated
//Resolution for 5528: Fares - Route Chesterfield fares displayed incorrectly
//
//   Rev 1.9   Apr 20 2010 15:38:04   mmodi
//Added more logging for dropped fares
//Resolution for 5509: Fares - RF 035 Find a Train - Sleeper Services Not Offering Full Range of Fares
//
//   Rev 1.8   Apr 15 2010 15:35:36   mmodi
//Fixed fare availability (reservations) logic to correctly mark Sleeper fares as available when the rvbo says they are, and similar for seated sleeper fares.
//Resolution for 5509: Fares - RF 035 Find a Train - Sleeper Services Not Offering Full Range of Fares
//
//   Rev 1.7   Mar 26 2010 11:34:48   mmodi
//Populate the actual NLC values for Cost search for debugging to be shown in the output. Does not affect the cost search logic.
//Resolution for 5295: Search by Price - Journey planned is not to original station selected
//
//   Rev 1.6   Feb 26 2009 15:03:38   mmodi
//Separate calls to Restrict MR for fares where route codes are not known by the routing guide obtained train journey
//Resolution for 5239: Routeing Guide - FCC Routed Travelcards
//
//   Rev 1.5   Feb 18 2009 18:19:40   mmodi
//Updated to use new RBO method MR, and changes following implementation RBO interface 0202
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.4   Feb 13 2009 17:26:50   rbroddle
//doubled value of FAGFOutputLength variable from 8192 16384 
//Resolution for 5250: Certain rail fare requests return ZPBO error
//
//   Rev 1.3   Feb 09 2009 15:02:18   mmodi
//Updated with comment following ZPBO travelcards ticket type fix
//Resolution for 5237: Routeing Guide - Anytime Day Travelcard for a Return jouney
//
//   Rev 1.2   Feb 02 2009 18:57:10   rbroddle
//Amended  ProcessTravelcardTickets to avoid Travelcards showing as spurious returns
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.1   Jan 11 2009 17:36:14   mmodi
//Updated processing to allow ZPBO to be used
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:22   mturner
//Initial revision.
//
//   Rev 1.70   Jun 04 2007 11:42:22   asinclair
//Corrected the if statement logic in CheapestAdvancedGroupFare
//Resolution for 4431: 9.6 - When selecting a Discount fare for grouped tickets, the orignal fare is lost
//
//   Rev 1.69   May 31 2007 14:54:26   asinclair
//Changed CheapestAdvanceCheapestAdvancedGroupFare() to work correct with discounted fares
//Resolution for 4431: 9.6 - When selecting a Discount fare for grouped tickets, the orignal fare is lost
//
//   Rev 1.68   Apr 23 2007 14:58:52   asinclair
//Added an extra GA call.  This is to ensure that Single fares are not validated with the return train, as this causes special TOC single fares to be removed for the outward leg if a different TOC is selected for the return leg.
//
//   Rev 1.67   Mar 06 2007 21:31:12   asinclair
//Added the private static string for Non-grouped fares
//
//   Rev 1.66   Mar 06 2007 13:43:52   build
//Automatically merged from branch for stream4358
//
//   Rev 1.65.1.0   Mar 02 2007 14:46:30   asinclair
//Added CheapestAdvancedFareGroup method, and code to set a flag after the RBO call returns no fares
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.65   Jan 11 2006 17:18:42   RPhilpott
//1) Check for group stations if initial FBO call returns no fares.
//
//2) If no fares found at all, don't attempt to call SBO MT.
//Resolution for 3444: SBT Rail Pricing - London -> Hertford East
//
//   Rev 1.64   Dec 15 2005 20:44:38   RPhilpott
//Changes to correctly handle avilability requests with and without railcards.
//Resolution for 3373: Incorrect display of availablility with railcards
//
//   Rev 1.63   Dec 08 2005 19:40:04   RPhilpott
//Correct logic error in seats/berths reservation logic.
//Resolution for 3106: DN039 - NRS - Apex Single Tickets on Sleepers
//
//   Rev 1.62   Dec 08 2005 16:35:18   RPhilpott
//Treat overbook places as being available.
//Resolution for 3352: DN040: Overbooked places should be considered available
//
//   Rev 1.61   Dec 05 2005 18:26:52   RPhilpott
//Changes to ensure that RE GD call is made if connecting TOC's need to be checked post-timetable call.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.60   Dec 02 2005 18:13:22   RPhilpott
//Handling of non-reservable services with mandatory reservation tickets.
//Resolution for 3277: DN040: NRS - mandatory tickets on non-reservable trains
//
//   Rev 1.59   Dec 02 2005 12:37:36   RPhilpott
//Handling of "no inventory" conditions.
//Resolution for 3202: DN040: Specific handling of no inventory condition.
//
//   Rev 1.58   Dec 02 2005 11:21:16   jgeorge
//Correction to availability checking for trains with sleeper and seat availability
//Resolution for 3106: DN039 - NRS - Apex Single Tickets on Sleepers
//
//   Rev 1.57   Nov 25 2005 19:22:54   RPhilpott
//Change in handling of "train not found by NRS" to set fare as unavailable. 
//Resolution for 3202: DN040: Specific handling of no inventory condition.
//
//   Rev 1.56   Nov 25 2005 17:14:26   RPhilpott
//Fix Find-A-Fare case where we find individual and group tickets from same station.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.55   Nov 24 2005 18:22:48   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.54   Nov 23 2005 15:53:10   RPhilpott
//Fix incorrect availability reporting of inward single and return fares by excluding irrelevant fares from all processing (returns) and from NRS queries (inward singles on outward legs). 
//Resolution for 3101: DN039 - NRS - Single Fares in Return Journeys
//
//   Rev 1.53   Nov 22 2005 17:29:18   RPhilpott
//Correct further error in origin/destination NLCs.
//Resolution for 3135: DN040: (CG) Fare availability inconsistent between SBT and SBP
//
//   Rev 1.52   Nov 18 2005 19:45:54   RPhilpott
//Correct handling of journey origin/destination NLC codes.
//Resolution for 3135: DN040: (CG) Fare availability inconsistent between SBT and SBP
//
//   Rev 1.51   Nov 16 2005 12:16:34   RPhilpott
//Minor changes to RVBO and SBO calls.
//Resolution for 3073: DN040: NRS errors on individual trains cause single RVBO error
//Resolution for 3081: DN040: Displayable supplements handling
//
//   Rev 1.50   Nov 15 2005 19:45:08   RPhilpott
//Filter fares to remove non-displayable tickets before doing anything else with them.
//Resolution for 3037: DN040: User responsiveness of SBP fare requests
//
//   Rev 1.49   Nov 15 2005 18:10:24   RPhilpott
//Correct handling in cases where NRS or SBO errors occur in availability checking.
//Resolution for 3018: DN040: No train or coach fares returned in SITest
//
//   Rev 1.48   Nov 11 2005 17:37:10   RWilby
//Updated for NRS compliance enhancement
//Resolution for 3003: NRS enhancement for non-compulsory reservations
//
//   Rev 1.47   Nov 09 2005 12:31:50   build
//Automatically merged from branch for stream2818
//
//   Rev 1.46.1.0   Nov 02 2005 17:48:46   rhopkins
//Added CheckAvailability logic for checking multiple Fares in one request.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.46   Jul 21 2005 12:08:44   RPhilpott
//Correct error handling when FBO returns critical error.
//Resolution for 2602: Del 7 PT costing - FBO error causes UI crash
//
//   Rev 1.45   May 13 2005 14:57:56   RPhilpott
//Add extra logging and defensive coding for Application initialisation problem.
//Resolution for 2511: PT costing - RBO initialisation failure
//
//   Rev 1.44   Apr 29 2005 20:46:54   RPhilpott
//Correct handling of availability checking for return journeys from pricing time-based searches. 
//Resolution for 2342: Del 7 - PT - Door to Door planner does not respond to unavailable ticket as expected
//
//   Rev 1.43   Apr 28 2005 18:09:56   RPhilpott
//Work in progress ...
//Resolution for 2342: Del 7 - PT - Door to Door planner does not respond to unavailable ticket as expected
//
//   Rev 1.42   Apr 28 2005 18:05:34   RPhilpott
//Split noPlacesAvaialble flag into singles and returns.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.41   Apr 28 2005 15:52:00   RPhilpott
//Add "NoPlacesAvailable" property to PricingResultDto to indicate that a time-based fare request has found valid fares but no places are available.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.40   Apr 25 2005 21:13:06   RPhilpott
//Change LBO lookups to get all groups to which a location belongs, and remove associated redundant code.
//Resolution for 2328: PT - fares between Three Bridges and Victoria
//
//   Rev 1.39   Apr 20 2005 10:32:24   RPhilpott
//Add more thorough and robust error handling.
//Resolution for 2247: PT: error handling by Retail Business Objects
//
//   Rev 1.38   Apr 13 2005 19:59:44   RPhilpott
//Corrections to RBO GC call
//Resolution for 2171: PT: incorrect parameters used when selecing journeys
//
//   Rev 1.37   Apr 13 2005 13:59:38   RPhilpott
//Returning of NRS errors.
//Resolution for 2072: PT: NRS error messages.
//
//   Rev 1.36   Apr 08 2005 18:55:04   RPhilpott
//Corrections to restrictions handling.
//
//   Rev 1.35   Apr 08 2005 12:22:34   RPhilpott
//Fix overwrite of request destination.
//
//   Rev 1.34   Apr 07 2005 20:52:32   RPhilpott
//Corrections to Supplement and Availability checking.
//
//   Rev 1.33   Apr 07 2005 19:01:40   RPhilpott
//Better logging on fares calculation.
//
//   Rev 1.32   Apr 06 2005 11:11:44   RPhilpott
//Availability correction.
//
//   Rev 1.31   Apr 03 2005 18:21:40   RPhilpott
//Unit test corrections
//
//   Rev 1.30   Apr 03 2005 18:01:06   RPhilpott
//Corrections to setting of Ticket JourneyTypes.
//
//   Rev 1.29   Mar 31 2005 18:44:24   RPhilpott
//Changes to handling of RVBO calls
//
//   Rev 1.28   Mar 22 2005 16:09:16   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.27   Mar 03 2005 19:46:32   RPhilpott
//Work in progress
//
//   Rev 1.26   Feb 10 2005 17:38:18   RScott
//Updated to include Reservation and Supplement Business Objects (RVBO, SBO)
//
//   Rev 1.25   Sep 03 2004 10:54:02   passuied
//Fixed problem created by fix for IR1392. Delete higher price tickets only if the lower price is not a discount!
//Resolution for 1487: Discounted Fares are not being displayed - DEL 6
//
//   Rev 1.24   Aug 20 2004 16:03:48   asinclair
//Removed extra loop that was not needed
//
//   Rev 1.23   Aug 20 2004 09:28:36   asinclair
//Fix for IR 1392
//
//   Rev 1.22   Jun 17 2004 13:33:12   passuied
//changes for del6:
//Inserted calls to GL and GM functions to restrict fares.
//Changes in RestrictFares design to respect Open-Close Principle
//
//   Rev 1.21   Jun 09 2004 13:29:10   asinclair
//Started coding for calling passing locations.  Not complete
//
//   Rev 1.20   Apr 15 2004 18:40:04   CHosegood
//refactoring
//Resolution for 663: Rail fares not being displayed
//
//   Rev 1.19   Mar 15 2004 10:14:26   CHosegood
//Performance fix
//Resolution for 621: Are too many RBO GB calls taking place?
//
//   Rev 1.18   Jan 13 2004 13:17:04   CHosegood
//Refactored LBO transform logic into LookupTransform class
//Resolution for 585: Pricing does not obtain all valid fares for stations within a group.
//
//   Rev 1.17   Jan 12 2004 17:15:12   CHosegood
//Changed trace level from info to verbose
//Resolution for 585: Pricing does not obtain all valid fares for stations within a group.
//
//   Rev 1.16   Jan 09 2004 16:44:30   CHosegood
//Added LookupLocationGroupRequest to project and Facade now determines fares for (where applicable)
//station -> station
//station -> station group
//station group -> station
//station group -> station group
//Resolution for 585: Pricing does not obtain all valid fares for stations within a group.
//
//   Rev 1.15   Jan 07 2004 11:49:00   CHosegood
//Now adds valid undiscounted fares to returned set before checking each one for discounted restrictions.
//Resolution for 462: Valid fares are being marked as invalid
//
//   Rev 1.14   Nov 18 2003 16:29:44   CHosegood
//TicketDto now requires Route code in constructor.
//Resolution for 188: Only one ticket of each type incorrectly assumed
//
//   Rev 1.13   Nov 13 2003 17:56:10   CHosegood
//Does not perform GB calls if no discount has been supplied
//
//   Rev 1.12   Nov 13 2003 11:19:12   CHosegood
//Now logs TDException caught in GetFares before rethrowing
//
//   Rev 1.11   Oct 29 2003 11:54:48   CHosegood
//Commented out GL call
//
//   Rev 1.10   Oct 28 2003 20:05:08   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.9   Oct 23 2003 10:15:32   CHosegood
//Commented out LBO and now returns TicketCode instead of TicketName in TicketDto
//
//   Rev 1.8   Oct 22 2003 15:34:36   CHosegood
//Commented out the GL call
//
//   Rev 1.7   Oct 17 2003 12:50:34   geaton
//Use correct Interface property.
//
//   Rev 1.6   Oct 17 2003 10:19:24   CHosegood
//Now a remote object
//
//   Rev 1.5   Oct 16 2003 13:59:14   CHosegood
//Removed Console.WriteLine statements
//
//   Rev 1.4   Oct 16 2003 12:53:30   CHosegood
//Now correctly returns ticket type
//
//   Rev 1.3   Oct 15 2003 20:13:08   CHosegood
//Now populates the result object
//
//   Rev 1.2   Oct 14 2003 12:26:38   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.1   Oct 13 2003 15:30:26   CHosegood
//Initial attempt at business logic
//
//   Rev 1.0   Oct 08 2003 11:46:42   CHosegood
//Initial Revision

using System;
using System.Text;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.DataServices;


namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// The RetailBusinessObjectsController retrieves and validates fares
	/// from the various business objects.
	/// 
	/// This class implements the Facade pattern.
	/// A Facade provides a unified interface to a set of interfaces in a
	/// subsystem. A Facade defines a higher-level interface that makes the
	/// subsystem easier to use.
	/// </summary>
	[CLSCompliant(false)]
	public class RetailBusinessObjectsFacade : MarshalByRefObject
    {
        #region Private static variables
        //private static readonly int REGAOutputLength = 329;   // No longer used following implementation of MR call
        //private static readonly int REGBOutputLength = 3;     // No longer used following implementation of MR call
		private static readonly int REGCOutputLength = 575;
        //private static readonly int REGDOutputLength = 12;    // No longer used following implementation of MR call
		private static readonly int REGLOutputLength = 7701;    // Interface 0202 or above is 7701, otherwise 2076
        //private static readonly int REGMOutputLength = 298;   // Interface 0202 or above is 298, otherwise 76 // No longer used following implementation of MR call
        //private static readonly int REGNOutputLength = 240;   // No longer used following implementation of GL call
		private static readonly int REGROutputLength = 702;     // Interface 0202 or above is 702, otherwise 628
        private static readonly int REMROutputLength = 69696;   // Upto 99 journeys, each at 704 chars = 69696

        private static readonly int FAGFOutputLength = 24576;
		
		private static readonly int SUGAOutputLength = 321;
		private static readonly int SUGDOutputLength = 346;

		private static readonly int RVAVOutputLength = 5681;

        private static readonly string PROPERTY_TIME_BASED_NRS_QUERY = "RetailBusinessObjects.RVBO.TimeBased";
		private static readonly string PROPERTY_COST_BASED_NRS_QUERY = "RetailBusinessObjects.RVBO.CostBased";

		public  static readonly string NRS_UNAVAILABLE		= "CostSearchError.NRSServiceError";
		private static readonly string UNSPECIFIED_BO_ERROR	= "CostSearchError.FaresInternalError";

		private static readonly string NON_GROUPED_TICKETS_IDENTIFIER = "NoGroup";
        #endregion

        #region Constructor
        /// <summary>
		/// Default constructor
		/// </summary>
		public RetailBusinessObjectsFacade() { }

        #endregion

        #region Housekeeping
        /// <summary>
		/// Initiates housekeeping on the FBO Pool.
		/// </summary>
		/// <param name="feedId">Feed id of housekeeping data used.</param>
		/// <returns>True if housekeeping was initiated successfully, otherwise false.</returns>
		public bool InitiateFBOHousekeeping(string feedId)
		{
            return FBOPool.GetFBOPool().InitiateHousekeeping(feedId);
		}

		/// <summary>
		/// Initiates housekeeping on the RBO Pool.
		/// </summary>
		/// <param name="feedId">Feed id of housekeeping data used.</param>
		/// <returns>True if housekeeping was initiated successfully, otherwise false.</returns>
		public bool InitiateRBOHousekeeping(string feedId)
		{
			return RBOPool.GetRBOPool().InitiateHousekeeping(feedId);
		}

		/// <summary>
		/// Initiates housekeeping on the LBO Pool.
		/// </summary>
		/// <param name="feedId">Feed id of housekeeping data used.</param>
		/// <returns>True if housekeeping was initiated successfully, otherwise false.</returns>
		public bool InitiateLBOHousekeeping(string feedId)
		{
			return LBOPool.GetLBOPool().InitiateHousekeeping(feedId);
		}

		/// <summary>
		/// Initiates housekeeping on the RVBO Pool.
		/// </summary>
		/// <param name="feedId">Feed id of housekeeping data used.</param>
		/// <returns>True if housekeeping was initiated successfully, otherwise false.</returns>
		public bool InitiateRVBOHousekeeping(string feedId)
		{
			return RVBOPool.GetRVBOPool().InitiateHousekeeping(feedId);
		}

		/// <summary>
		/// Initiates housekeeping on the SBO Pool.
		/// </summary>
		/// <param name="feedId">Feed id of housekeeping data used.</param>
		/// <returns>True if housekeeping was initiated successfully, otherwise false.</returns>
		public bool InitiateSBOHousekeeping(string feedId)
		{
			return SBOPool.GetSBOPool().InitiateHousekeeping(feedId);
		}

        /// <summary>
        /// Initiates housekeeping on the ZPBO Pool.
        /// </summary>
        /// <param name="feedId">Feed id of housekeeping data used.</param>
        /// <returns>True if housekeeping was initiated successfully, otherwise false.</returns>
        public bool InitiateZPBOHousekeeping(string feedId)
        {
            return ZPBOPool.GetZPBOPool().InitiateHousekeeping(feedId);
        }
        #endregion

        #region Dispose pools
        /// <summary>
		/// Disposes of resources in LBO Pool.
		/// </summary>
		public void DisposeLBOPool()
		{
			LBOPool.GetLBOPool().Dispose(true);
		}

		/// <summary>
		/// Disposes of resources in FBO Pool.
		/// </summary>
		public void DisposeFBOPool()
		{
			FBOPool.GetFBOPool().Dispose(true);
		}

		/// <summary>
		/// Disposes of resources in RBO Pool.
		/// </summary>
		public void DisposeRBOPool()
		{
			RBOPool.GetRBOPool().Dispose(true);
		}

		/// <summary>
		/// Disposes of resources in RVBO Pool.
		/// </summary>
		public void DisposeRVBOPool()
		{
			RVBOPool.GetRVBOPool().Dispose(true);
		}

		/// <summary>
		/// Disposes of resources in SBO Pool.
		/// </summary>
		public void DisposeSBOPool()
		{
			SBOPool.GetSBOPool().Dispose(true);
		}

        /// <summary>
        /// Disposes of resources in ZPBO Pool.
        /// </summary>
        public void DisposeZPBOPool()
        {
            ZPBOPool.GetZPBOPool().Dispose(true);
        }
        #endregion

        #region Private methods

        /// <summary>
		/// Check the availability for multiple fares
		/// </summary>
		/// <param name="request"></param>
		/// <param name="fareAvailabilities"></param>
		/// <param name="costBased"></param>
		/// <param name="rvboResults"></param>
		/// <param name="errors"></param>
		/// <param name="outward"></param>
		private void CheckAvailability(PricingRequestDto request, ref Hashtable fareAvailabilities,
			bool costBased, ArrayList rvboResults, ArrayList errors, bool outward, Hashtable MandatoryReservations, ref bool noTrainsFound)
		{
            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "CheckAvailability for multiple fares"));
            }

			MultipleAvailabilityRequest multipleRvboRequest;
			AvailabilityResult  rvboOutput;
			RailAvailabilityResultDto rvboResult;
			FareAvailabilityCombined currentFareAvailabilityCombined;
			FareAvailability currentFareAvailability;

			RVBOPool rvboPool = RVBOPool.GetRVBOPool();
			BusinessObject rvbo = null;

            #region Do NRS query check

            bool doNrsQuery  = false;
			bool nrsUnavailable = false;

			try
			{
				if	(costBased)
				{
					doNrsQuery = (Properties.Current[PROPERTY_COST_BASED_NRS_QUERY] == "Y");			
				}
				else
				{
					doNrsQuery = (Properties.Current[PROPERTY_TIME_BASED_NRS_QUERY] == "Y");			
				}
			}
			catch 
			{
				//log error, then let all switches default to off
				Trace.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Error,
					"RVBO switches - missing property in Property database"));				
			}

			if	(!doNrsQuery)
			{
                if (TDTraceSwitch.TraceVerbose)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("NRS query switch set to false, RVBO requests have not been done")));
                }

				return;	// nothing to do!
            }

            #endregion

            string[] journeyEndPoints = GetJourneyEndPoints(request.Trains);

			ReturnIndicator previousDirection = ReturnIndicator.Return;

            #region Check if train is reservable

            // check if any part of outward and/or return journey is on a reservable service
			//  if none is, a mandatory reservation ticket cannot be available ...
		
			bool outwardIncludesReservable	= false;
			bool returnIncludesReservable	= false;
			bool returnTrainsPresent		= false;

			foreach (TrainDto train in request.Trains)
			{
				if	(train.ReturnIndicator == ReturnIndicator.Return)
				{
					returnTrainsPresent = true;

					if	(train.Reservability.Length > 0 && train.Reservability != " ")
					{
						returnIncludesReservable = true;
					}
				}
				else
				{
					if	(train.Reservability.Length > 0 && train.Reservability != " ")
					{
						outwardIncludesReservable = true;
					}
				}
            }

            #region Set fare availabilities to false for mandatory reservations if train is not reservable

            // Set fare availabilities to false for any which require mandatory reservations and the train is not reservable
            if	(!outwardIncludesReservable)
			{
				foreach (FareAvailabilityCombined fac in fareAvailabilities.Values)
				{
					MandatoryReservationFlag mrf = (MandatoryReservationFlag)MandatoryReservations[fac.FareWithoutDiscount.FareDto.ShortTicketCode];

					if	(mrf == MandatoryReservationFlag.Required || mrf == MandatoryReservationFlag.OutwardOnly)
					{
						fac.OutwardResultFound	    = false;
						fac.UndiscountedOutwardPlacesAvailable  = false;	
						fac.DiscountedOutwardPlacesAvailable  = false;	
					}
				}
			}

			if	(returnTrainsPresent && !returnIncludesReservable)
			{
				foreach (FareAvailabilityCombined fac in fareAvailabilities.Values)
				{
					MandatoryReservationFlag mrf = (MandatoryReservationFlag)MandatoryReservations[fac.FareWithoutDiscount.FareDto.ShortTicketCode];

					if	(mrf == MandatoryReservationFlag.Required || mrf == MandatoryReservationFlag.ReturnOnly)
					{
						fac.InwardResultFound     = false;
						fac.UndiscountedInwardPlacesAvailable = false;	
						fac.DiscountedInwardPlacesAvailable = false;	
					}
				}
            }
            #endregion

            #endregion

            // The following makes the assumption that a given fare will only have free seat or sleeper
			//  reservation supplements associated with it, never both. The logic breaks down if this is
			//  not true, or if we ever need to deal with chargeable sleeper reservations as well ... 

			try
			{
				int legCount = 0;

                // Array to keep track of fare reservations processed. This is to avoid the FareAvailabilityCombined
                // entries from being double processed if the train allows both sleeper and seat reservations. 
                // The double processing in this case is where ConsolidateResultOfPreviousRequest is called twice,
                // once in this method, and the other in MultipleAvailabilityRequest.BuildRequests(), this sets an 
                // internal class flag outwardResultMissingSomeLegs which ultimately turns an Available fare into Unavailable,
                // which finally means it's not displayed in the output.
                ArrayList faresNotProcessed = null;

				foreach (TrainDto train in request.Trains)
				{
					// Supplement leg count is within direction (out/return) 
					//  so reset it if direction changes. Note that for a 
					//  return journey all outward services will always 
					//	precede all services in return direction ...
					 
					if	(train.ReturnIndicator != previousDirection)
					{
						legCount = 0;
						previousDirection = train.ReturnIndicator;
					}
					
					legCount++;
                    
                    if	((outward && train.ReturnIndicator == ReturnIndicator.Return)
						|| (!outward && train.ReturnIndicator == ReturnIndicator.Outbound))
					{
						continue;
					}

					if	(train.Reservability.Length > 0 && train.Reservability != " ")
					{
						string journeyOriginNlc		 = (train.ReturnIndicator == ReturnIndicator.Outbound ? journeyEndPoints[0] : journeyEndPoints[2]);
						string journeyDestinationNlc = (train.ReturnIndicator == ReturnIndicator.Outbound ? journeyEndPoints[1] : journeyEndPoints[3]);

                        // Reset the fares processed for each train, as we should really validate the fares against all trains?
                        faresNotProcessed = new ArrayList(fareAvailabilities.Keys);

						// check for reservable berths ...
						if	(train.Sleeper.Length > 0 && train.Sleeper != " ")
                        {
                            #region Reservable berths

                            if (TDTraceSwitch.TraceVerbose)
                            {
                                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                    string.Format("Checking for reservable sleeper berths")));
                            }

							multipleRvboRequest = new MultipleAvailabilityRequest(rvboPool.InterfaceVersion, request, journeyOriginNlc, journeyDestinationNlc,
                                ref fareAvailabilities, ref faresNotProcessed,
								train, legCount, true, RVAVOutputLength, outward, MandatoryReservations, ref errors);

							if	(multipleRvboRequest.RequestToProcess)
							{
								foreach (AvailabilityRequest rvboRequest in multipleRvboRequest.Item)
                                {
                                    #region Make availability call

                                    rvbo = rvboPool.GetInstance();
									rvboOutput = new AvailabilityResult(rvbo.Process(rvboRequest));
									rvboPool.Release(ref rvbo);
									rvbo = null;

									if	(rvboOutput.TrainNotFoundByNrs)
									{
										noTrainsFound = true;
										break;
									}

									if	(rvboOutput.NrsServiceError)
									{
										errors.Add(NRS_UNAVAILABLE);
										nrsUnavailable = true;
										break;
									}

									foreach (ProductAvailability pa in rvboOutput.ProductAvailabilityList)
                                    {
                                        #region Update fare availablity list

                                        currentFareAvailabilityCombined = (FareAvailabilityCombined)fareAvailabilities[pa.FareKey];

										if (outward)
											currentFareAvailabilityCombined.OutwardReservableBerths = true;
										else
											currentFareAvailabilityCombined.InwardReservableBerths = true;

										if (pa.Product.Substring(3, 3).Trim().Length > 0)
										{
											currentFareAvailability = currentFareAvailabilityCombined.FareWithDiscount;
										}
										else
										{
											currentFareAvailability = currentFareAvailabilityCombined.FareWithoutDiscount;
										}

										if (currentFareAvailability == null)
										{
											// Matching fare availability store does not exist
											continue;
										}

										if (outward)
										{
											currentFareAvailabilityCombined.OutwardResultFound = true;
											
											if (pa.PlacesAvailable == 0 && pa.OverbookAvailable == 0)
											{
												currentFareAvailability.OutwardBerthsAvailable = false;
											}
										}
										else
										{
											currentFareAvailabilityCombined.InwardResultFound = true;
											
											if (pa.PlacesAvailable == 0 && pa.OverbookAvailable == 0)
											{
												currentFareAvailability.InwardBerthsAvailable = false;
											}
										}

                                        // Keep track that this fare has been processed, in case we next check for reservable seats on the train
                                        //faresProcessed.Add(pa.FareKey);

                                        #region Log

                                        if (TDTraceSwitch.TraceVerbose)
                                        {
                                            FareAvailabilityCombined fac = ((FareAvailabilityCombined)fareAvailabilities[pa.FareKey]);

                                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                                string.Format("Fare product availability processed for farekey[{0}] outBerths[{1}] outSeats[{2}] outResultFound[{3}] inBerths[{4}] inSeats[{5}] inResultFound[{6}] undiscOutPlaceAvail[{7}] undiscInPlaceAvail[{8}] fareWithoutDisc.FareKey[{9}] fareWithoutDisc.OutBerthsAvail[{10}] fareWithoutDisc.OutSeatAvail[{11}] fareWithoutDisc.InBerthAvail[{12}] fareWithoutDisc.InSeatAvail[{13}]",
                                                fac.FareKey,
                                                fac.OutwardReservableBerths,
                                                fac.OutwardReservableSeats,
                                                fac.OutwardResultFound,
                                                fac.InwardReservableBerths,
                                                fac.InwardReservableSeats,
                                                fac.InwardResultFound,
                                                fac.UndiscountedOutwardPlacesAvailable,
                                                fac.UndiscountedInwardPlacesAvailable,
                                                fac.FareWithoutDiscount.FareKey,
                                                fac.FareWithoutDiscount.OutwardBerthsAvailable,
                                                fac.FareWithoutDiscount.OutwardSeatsAvailable,
                                                fac.FareWithoutDiscount.InwardBerthsAvailable,
                                                fac.FareWithoutDiscount.InwardSeatsAvailable
                                                )));
                                        }

                                        #endregion

                                        #region Create availability result

                                        rvboResult = new RailAvailabilityResultDto(pa.Product, pa.PlacesAvailable, pa.OverbookAvailable,
											request.Origin, request.Destination, train.Origin.Departure, train.Uid, train.RetailId);

										rvboResults.Add(rvboResult);

                                        #endregion

                                        #endregion
                                    }

                                    #endregion
                                }

								if	(nrsUnavailable)
								{
									break;
								}
							}
							else
							{
                                if (TDTraceSwitch.TraceVerbose)
                                {
                                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                        string.Format("No reservable sleeper berth check requests were made")));
                                }
								break;
                            }

                            #endregion
                        }

                        // check for reservable seats ...
						if	(train.TrainClass.Length > 0 && train.TrainClass != " ")
                        {
                            #region Reservable seats

                            if (TDTraceSwitch.TraceVerbose)
                            {
                                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                    string.Format("Checking for reservable seats")));
                            }

                            multipleRvboRequest = new MultipleAvailabilityRequest(rvboPool.InterfaceVersion, request, journeyOriginNlc, journeyDestinationNlc,
                                ref fareAvailabilities, ref faresNotProcessed,
								train, legCount, false, RVAVOutputLength, outward, MandatoryReservations, ref errors);
                                                        
							if	(multipleRvboRequest.RequestToProcess)
							{
								foreach (AvailabilityRequest rvboRequest in multipleRvboRequest.Item)
                                {
                                    #region Make availability call

                                    rvbo = rvboPool.GetInstance();
									rvboOutput = new AvailabilityResult(rvbo.Process(rvboRequest));
									rvboPool.Release(ref rvbo);
									rvbo = null;

									if	(rvboOutput.TrainNotFoundByNrs)
									{
										noTrainsFound = true;
										break;
									}

									if	(rvboOutput.NrsServiceError)
									{
										errors.Add(NRS_UNAVAILABLE);
										nrsUnavailable = true;
										break;
									}

                                    foreach (ProductAvailability pa in rvboOutput.ProductAvailabilityList)
                                    {
                                        #region Update fare availablity list

                                        currentFareAvailabilityCombined = (FareAvailabilityCombined)fareAvailabilities[pa.FareKey];

										if (outward)
											currentFareAvailabilityCombined.OutwardReservableSeats = true;
										else
											currentFareAvailabilityCombined.InwardReservableSeats = true;

										if (pa.Product.Substring(3, 3).Trim().Length > 0)
										{
											currentFareAvailability = currentFareAvailabilityCombined.FareWithDiscount;
										}
										else
										{
											currentFareAvailability = currentFareAvailabilityCombined.FareWithoutDiscount;
										}

										if (currentFareAvailability == null)
										{
											// Matching fare availability store does not exist
											continue;
										}

										if (outward)
										{
											currentFareAvailabilityCombined.OutwardResultFound = true;
											
											if (pa.PlacesAvailable == 0 && pa.OverbookAvailable == 0)
											{
												currentFareAvailability.OutwardSeatsAvailable = false;
											}
										}
										else
										{
											currentFareAvailabilityCombined.InwardResultFound = true;
											
											if (pa.PlacesAvailable == 0 && pa.OverbookAvailable == 0)
											{
												currentFareAvailability.InwardSeatsAvailable = false;
											}
										}

                                        // Keep track that this fare has been processed, not really needed as we reset 
                                        // the array for every train but retained in case the logic is changed
                                        //faresProcessed.Add(pa.FareKey);

                                        #region Log

                                        if (TDTraceSwitch.TraceVerbose)
                                        {
                                            FareAvailabilityCombined fac = ((FareAvailabilityCombined)fareAvailabilities[pa.FareKey]);

                                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                                string.Format("Fare product availability processed for farekey[{0}] outBerths[{1}] outSeats[{2}] outResultFound[{3}] inBerths[{4}] inSeats[{5}] inResultFound[{6}] undiscOutPlaceAvail[{7}] undiscInPlaceAvail[{8}] fareWithoutDisc.FareKey[{9}] fareWithoutDisc.OutBerthsAvail[{10}] fareWithoutDisc.OutSeatAvail[{11}] fareWithoutDisc.InBerthAvail[{12}] fareWithoutDisc.InSeatAvail[{13}]",
                                                fac.FareKey,
                                                fac.OutwardReservableBerths,
                                                fac.OutwardReservableSeats,
                                                fac.OutwardResultFound,
                                                fac.InwardReservableBerths,
                                                fac.InwardReservableSeats,
                                                fac.InwardResultFound,
                                                fac.UndiscountedOutwardPlacesAvailable,
                                                fac.UndiscountedInwardPlacesAvailable,
                                                fac.FareWithoutDiscount.FareKey,
                                                fac.FareWithoutDiscount.OutwardBerthsAvailable,
                                                fac.FareWithoutDiscount.OutwardSeatsAvailable,
                                                fac.FareWithoutDiscount.InwardBerthsAvailable,
                                                fac.FareWithoutDiscount.InwardSeatsAvailable
                                                )));
                                        }

                                        #endregion

                                        #region Create availability result
                                        
                                        rvboResult = new RailAvailabilityResultDto(pa.Product, pa.PlacesAvailable, pa.OverbookAvailable,
											request.Origin, request.Destination, train.Origin.Departure, train.Uid, train.RetailId);

										rvboResults.Add(rvboResult);

                                        #endregion

                                        #endregion
                                    }

                                    #endregion
                                }

								if	(nrsUnavailable)
								{
									break;
								}
							}
							else
							{
								break;
                            }

                            #endregion
                        }

                        // Call ConsolidateResultOfPreviousRequest for any fares not processed, i.e. those which didn't have
                        // an availability call made. This needs to be done to ensure fare availabilities are 
                        // put in to their correct unavailable state.
                        foreach (string fareKey in faresNotProcessed)
                        {
                            ((FareAvailabilityCombined)fareAvailabilities[fareKey]).ConsolidateResultOfPreviousRequest(outward);
                        }
					}
				}

				// The results of the request for the last train in the loop need to be interpreted

				// If the availabilities have not been checked because of an NRS failure, 
				//  we miss out this step, which will cause everything to appear to be
				//  available. In the "train not found" case we do perform it, causing 
				//  each fare to stay in its default state (unavailable if a mandatory 
				//  reservation ticket type, available otherwise).
				if	(!nrsUnavailable)
				{
					foreach (string fareKey in fareAvailabilities.Keys)
					{
                        ((FareAvailabilityCombined)fareAvailabilities[fareKey]).ConsolidateResultOfPreviousRequest(outward);
					}
				}
			} 
			catch (TDException tde) 
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Warning, "An error was encountered while calculating fares::", tde ) );
				throw tde; 
			} 
			catch (Exception e) 
			{
				TDException td = new TDException( "Unexpected error encountered while restricting fares", e, true, TDExceptionIdentifier.PRHUnexpectedError );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure,TDTraceLevel.Warning, td.InnerException.Message, e ) );
				throw td;
			}
			finally 
			{
				// Release the RVBO if it is not done already
				if (rvbo != null) 
				{
					rvboPool.Release(ref rvbo);
				}
			}

            return;
		}

		/// <summary>
		/// Check the availability for a single fare
		/// </summary>
		/// <param name="request"></param>
		/// <param name="fareDto"></param>
		/// <param name="supplements"></param>
		/// <param name="costBased"></param>
		/// <param name="rvboResults"></param>
		/// <param name="errors"></param>
		/// <param name="outward"></param>
		/// <returns></returns>
		private bool CheckAvailability(PricingRequestDto request, FareDataDto fareDto, ArrayList supplements, 
			bool costBased, ArrayList rvboResults, ArrayList errors, bool outward, Hashtable mandatoryReservations, ref bool noTrainsFound)
		{
            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "CheckAvailability for single fare"));
            }

			AvailabilityRequest rvboRequest;
			AvailabilityResult  rvboOutput;
			RailAvailabilityResultDto rvboResult;

			bool availabilitySeats  = true;
			bool availabilityBerths = true;
			bool possibilitySeats = false;
			bool possibilityBerths = false;

			RVBOPool rvboPool = RVBOPool.GetRVBOPool();
			BusinessObject rvbo = null;

            #region Do NRS query check

            bool doNrsQuery  = false;

			try
			{
				if	(costBased)
				{
					doNrsQuery = (Properties.Current[PROPERTY_COST_BASED_NRS_QUERY] == "Y");			
				}
				else
				{
					doNrsQuery = (Properties.Current[PROPERTY_TIME_BASED_NRS_QUERY] == "Y");			
				}
			}
			catch 
			{
				//log error, then let all switches default to off
				Trace.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Error,
					"RVBO switches - missing property in Property database"));				
			}

			if	(!doNrsQuery)
			{
                if (TDTraceSwitch.TraceVerbose)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("NRS query switch set to false, RVBO requests have not been done")));
                }

				return true;	// nothing to do!
            }

            #endregion

            #region If fare exist in mandatory reservations, then no availabilit check needed

            MandatoryReservationFlag mandResFlag;

			if	(mandatoryReservations.ContainsKey(fareDto.ShortTicketCode))
			{
				//Get mandatory reservation for ticket type
				mandResFlag = (MandatoryReservationFlag) mandatoryReservations[fareDto.ShortTicketCode];

				//Reservation check not required if:
				if(mandResFlag == MandatoryReservationFlag.NotRequired
					|| (mandResFlag == MandatoryReservationFlag.OutwardOnly && !outward )
					|| mandResFlag == MandatoryReservationFlag.ReturnOnly && outward)
				{
					return true; //No check required
				}
			}
			else
			{
				//if the ticket code cannot be found
				errors.Add(NRS_UNAVAILABLE);
				return true;
            }

            #endregion

            string[] journeyEndPoints = GetJourneyEndPoints(request.Trains);

			// The following makes the assumption that a given fare will only have free seat or sleeper
			//  reservation supplements associated with it, never both. The logic breaks down if this is
			//  not true, or if we ever need to deal with chargeable sleeper reservations as well ... 
			ReturnIndicator previousDirection = ReturnIndicator.Return;

			try
			{
				int legCount = 0;

				foreach (TrainDto train in request.Trains)
				{

					// Supplement leg count is within direction (out/return) 
					//  so reset it if direction changes. Note that for a 
					//  return journey all outward services will always 
					//	precede all services in return direction ...
					 
					if	(train.ReturnIndicator != previousDirection)
					{
						legCount = 0;
						previousDirection = train.ReturnIndicator;
					}
					
					legCount++;

                    if	((outward && train.ReturnIndicator == ReturnIndicator.Return)
						|| (!outward && train.ReturnIndicator == ReturnIndicator.Outbound))
					{
						continue;
					}

                    if	(train.Reservability.Length > 0 && train.Reservability != " ")
					{
						string journeyOriginNlc		 = (train.ReturnIndicator == ReturnIndicator.Outbound ? journeyEndPoints[0] : journeyEndPoints[2]);
						string journeyDestinationNlc = (train.ReturnIndicator == ReturnIndicator.Outbound ? journeyEndPoints[1] : journeyEndPoints[3]);

						// check for reservable berths (but only if we haven't already
						//   found a leg on which this fare is not available) ...

                        if	(availabilityBerths && train.Sleeper.Length > 0 && train.Sleeper != " ")
                        {
                            #region Reservable berths

                            if (TDTraceSwitch.TraceVerbose)
                            {
                                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                    string.Format("Checking for reservable sleeper berths")));
                            }

                            possibilityBerths = true;

							rvboRequest = new AvailabilityRequest(rvboPool.InterfaceVersion, request, fareDto, train, legCount,
								supplements, true, journeyOriginNlc, journeyDestinationNlc, RVAVOutputLength);

							if	(rvboRequest.RequestToProcess)
                            {
                                #region Make availability call

                                rvbo = rvboPool.GetInstance();
								rvboOutput = new AvailabilityResult(rvbo.Process(rvboRequest)); 
								rvboPool.Release(ref rvbo);
								rvbo = null;
								
								if	(rvboOutput.TrainNotFoundByNrs)
								{
									noTrainsFound = true;
									availabilityBerths = false;
								}
								else if	(rvboOutput.NrsServiceError)
								{
									errors.Add(NRS_UNAVAILABLE);
								}
								else
								{
                                    foreach (ProductAvailability pa in rvboOutput.ProductAvailabilityList)
                                    {
                                        #region Create availability result
                                        
                                        rvboResult = new RailAvailabilityResultDto(pa.Product, pa.PlacesAvailable, pa.OverbookAvailable,
											request.Origin, request.Destination, train.Origin.Departure, train.Uid, train.RetailId);
									
										rvboResults.Add(rvboResult);

                                        #endregion
                                    }

									if	(!rvboOutput.AvailabilityExistsForFare)
									{
										availabilityBerths = false;
									}
                                }

                                #endregion
                            }
							else
							{
								availabilityBerths = false;
                            }

                            #endregion
                        }

						// check for reservable seats (but only if we haven't already
						//   found a leg on which this fare is not available) ...

                        if	(availabilitySeats && train.TrainClass.Length > 0 && train.TrainClass != " ")
                        {
                            #region Reservable seats

                            if (TDTraceSwitch.TraceVerbose)
                            {
                                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                    string.Format("Checking for reservable seats")));
                            }

                            possibilitySeats = true;

							rvboRequest = new AvailabilityRequest(rvboPool.InterfaceVersion, request, fareDto, train, legCount,
								supplements, false, journeyOriginNlc, journeyDestinationNlc, RVAVOutputLength);

							if	(rvboRequest.RequestToProcess)
                            {
                                #region Make availability call

                                rvbo = rvboPool.GetInstance();
								rvboOutput = new AvailabilityResult(rvbo.Process(rvboRequest)); 
								rvboPool.Release(ref rvbo);
								rvbo = null;

								if	(rvboOutput.TrainNotFoundByNrs)
								{
									noTrainsFound = true;
									availabilitySeats = false;
								}
								else if	(rvboOutput.NrsServiceError)
								{
									errors.Add(NRS_UNAVAILABLE);
								}
								else
								{
                                    foreach (ProductAvailability pa in rvboOutput.ProductAvailabilityList)
                                    {
                                        #region Create availabiliy result
                                        
                                        rvboResult = new RailAvailabilityResultDto(pa.Product, pa.PlacesAvailable, pa.OverbookAvailable,
											train.Board.Location, train.Alight.Location, train.Board.Departure, train.Uid, train.RetailId);

										rvboResults.Add(rvboResult);

                                        #endregion
                                    }

									if	(!rvboOutput.AvailabilityExistsForFare)
									{
										availabilitySeats = false;
									}
                                }

                                #endregion
                            }
							else
							{
								availabilitySeats = false;
                            }

                            #endregion
                        }
					}
				}
			} 
			catch (TDException tde) 
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Warning, "An error was encountered while calculating fares::", tde ) );
				throw tde; 
			} 
			catch (Exception e) 
			{
				TDException td = new TDException( "Unexpected error encountered while restricting fares", e, true, TDExceptionIdentifier.PRHUnexpectedError );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure,TDTraceLevel.Warning, td.InnerException.Message, e ) );
				throw td;
			}
			finally 
			{
				// Release the RVBO if it is not done already
				if (rvbo != null) 
				{
					rvboPool.Release(ref rvbo);
				}
            }

            #region Log

            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("CheckAvailability for single fare result fareDto[{0}:{1}] available[{2}] ( possibilityBerths[{3}] availabilityBerths[{4}] possibilitySeats[{5}] availabilitySeats[{6}] )",
                    fareDto.ShortTicketCode,
                    fareDto.RouteCode,
                    ((possibilitySeats && availabilitySeats) || (possibilityBerths && availabilityBerths)),
                    possibilitySeats,
                    availabilitySeats,
                    possibilityBerths,
                    availabilityBerths)));
            }

            #endregion

            return ((possibilitySeats && availabilitySeats) || (possibilityBerths && availabilityBerths));
        }

        #region Remove fares

        /// <summary>
		/// Given a set of Fares remove any not considered 
		/// to be displayable within the Portal.
		/// </summary>
		/// <param name="fares">Original set of fares</param>
		/// <returns>Filtered set of fares</returns>
		private ArrayList RemoveUndisplayableFares(JourneyType journeyType, ArrayList fares)
		{
			IDataServices dataServices = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

			Hashtable displayableTickets = dataServices.GetHash(DataServiceType.DisplayableRailTickets);
			
			ArrayList newFareList = new ArrayList(fares.Count);
            int numberOfFares = 0;

			foreach (Fare fare in fares)
			{
				if	(displayableTickets.Contains(fare.TicketType.Code)) 
				{
					// not interested in return tickets if this request is for inward singles ...
					if	(!(journeyType == JourneyType.InwardSingle && fare.TicketType.Type == JourneyType.Return))
					{
						newFareList.Add(fare);
                        numberOfFares++;
						continue;
					}
				}

				if (TDTraceSwitch.TraceVerbose) 
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Dropping non-displayable fare: " + fare.TicketType.Code + "."));
				}
			}

            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Displayable fares: " + numberOfFares + "."));
            }

			return newFareList;
		}

        /// <summary>
        /// Given a set of Fares remove any fares considered to be a Zonal fare which has an 
        /// equivalent Terminal fare.
        /// </summary>
        /// <param name="fares">Original set of fares</param>
        /// <returns>Filtered set of fares</returns>
        private ArrayList RemoveTerminalZoneDuplication(ArrayList fares)
        {
            IDataServices dataServices = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            // Get the NLCs we need to compare against
            Hashtable terminalsHash = dataServices.GetHash(DataServiceType.FareTerminalZoneNLC);
            Hashtable zonesHash = dataServices.GetHash(DataServiceType.FareUndergroundZoneNLC);

            ArrayList newFareList = new ArrayList(fares.Count);
            
            bool keepFare = true;

            foreach (Fare fare in fares)
            {
                keepFare = true;

                // Test we have an Actual NLC (only interested in doing the filtering when ZPBO has been used, only 
                // the ZPBO will populate the Actual NLC property).
                if (!string.IsNullOrEmpty(fare.FareOriginNlcActual))
                {
                    // Is this fare for a Zone NLC we're interested in
                    if ((zonesHash.Contains(fare.FareOriginNlcActual))
                        ||
                        (zonesHash.Contains(fare.FareDestinationNlcActual)))
                    {
                        // Where there is a Terminal NLC fare with a matching TicketType.Code and Route.Code, 
                        // keep the Terminal NLC fare and discard the Zone fare
                        foreach (Fare compareFare in fares)
                        {
                            if ((terminalsHash.Contains(compareFare.FareOriginNlcActual))
                                ||
                                (terminalsHash.Contains(compareFare.FareDestinationNlcActual)))
                            {
                                // Is matching ticket
                                if ((compareFare.TicketType.Code == fare.TicketType.Code)
                                    &&
                                    (compareFare.Route.Code == fare.Route.Code))
                                {
                                    // Its a matching Terminal NLC fare, discard Zone NLC fare
                                    keepFare = false;

                                    if (TDTraceSwitch.TraceVerbose)
                                    {
                                        Trace.Write(new OperationalEvent(
                                            TDEventCategory.Infrastructure, 
                                            TDTraceLevel.Verbose, 
                                            "Dropping terminal-zone fare: " + fare.TicketType.Code 
                                                + " (" + fare.ToString() + " )."));
                                    }

                                    // No need to check this fare anymore 
                                    break;
                                }
                            }

                        } // end foreach
                    }
                }

                if (keepFare)
                {
                    // This fare is ok to be kept
                    newFareList.Add(fare);
                }
            }

            return newFareList;
        }

        /// <summary>
        /// Given a set of Fares remove any Travelcard zone fares if there is a cheaper equivalent 
        /// Travelcard zone fare
        /// </summary>
        /// <param name="fares">Original set of fares</param>
        /// <returns>Filtered set of fares</returns>
        private ArrayList RemoveTravelcardZoneDuplication(ArrayList fares)
        {
            IDataServices dataServices = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            // Get the NLCs we need to compare against
            Hashtable travelcardszoneHash = dataServices.GetHash(DataServiceType.FareTravelcardZoneNLC);
            
            ArrayList newFareList = new ArrayList(fares.Count);

            bool keepFare = true;

            foreach (Fare fare in fares)
            {
                keepFare = true;

                // Test we have an Actual NLC (only interested in doing the filtering when ZPBO has been used, only 
                // the ZPBO will populate the Actual NLC property).
                if (!string.IsNullOrEmpty(fare.FareOriginNlcActual))
                {
                    // Is this fare for a Travelcard NLC we're interested in
                    if ((travelcardszoneHash.Contains(fare.FareOriginNlcActual))
                        ||
                        (travelcardszoneHash.Contains(fare.FareDestinationNlcActual)))
                    {
                        // This is a Travelcard NLC fare, check if there are any cheaper Travelcard NLC
                        // fares of the same TicketType and Route and discard the more expensive one
                        foreach (Fare compareFare in fares)
                        {
                            if ((travelcardszoneHash.Contains(compareFare.FareOriginNlcActual))
                                ||
                                (travelcardszoneHash.Contains(compareFare.FareDestinationNlcActual)))
                            {
                                // Is matching ticket
                                if ((compareFare.TicketType.Code == fare.TicketType.Code)
                                    && (compareFare.Route.Code == fare.Route.Code))
                                {
                                    if ((fare.AdultFare > compareFare.AdultFare)
                                        && (compareFare.RailcardCode.Trim().Length == 0)) // if the cheaper fare is a discount one, don't delete it!
                                    {
                                        keepFare = false;

                                        if (TDTraceSwitch.TraceVerbose)
                                        {
                                            Trace.Write(new OperationalEvent(
                                                TDEventCategory.Infrastructure,
                                                TDTraceLevel.Verbose,
                                                "Dropping travelcard-zone fare: " + fare.TicketType.Code
                                                    + " (" + fare.ToString() + " )."));
                                        }

                                        // No need to check this fare anymore 
                                        break;
                                    }
                                }
                            }

                        } // end foreach
                    }
                }
                
                if (keepFare)
                {
                    // This fare is ok to be kept
                    newFareList.Add(fare);
                }
            }

            return newFareList;
        }

        /// <summary>
        /// Given a set of fares, remove any duplicate fare (TicketType Code and Route Code) and keep 
        /// the cheapest fare
        /// </summary>
        /// <param name="fares"></param>
        /// <returns></returns>
        private ArrayList RemoveDuplicateFares(ArrayList fares)
        {
            #region Keep lowest fare of each Ticket Type Code and Route Code combo
            ArrayList lowestFares = new ArrayList(fares.Count);

            foreach (Fare fareOut in fares)
            {
                bool toKeep = true;

                foreach(Fare fareIn in fares)
                {
                    if ((fareOut.TicketType.Code == fareIn.TicketType.Code)
                        && (fareOut.Route.Code == fareIn.Route.Code))
                    {
                        if (fareOut.AdultFare > fareIn.AdultFare
                            && fareIn.RailcardCode.Trim().Length == 0) // if the cheaper fare is a discount one, don't delete it!
                        {
                            toKeep = false;

                            if (TDTraceSwitch.TraceVerbose)
                            {
                                Trace.Write(new OperationalEvent(
                                    TDEventCategory.Infrastructure,
                                    TDTraceLevel.Verbose,
                                    "Dropping duplicate fare: " + fareOut.TicketType.Code
                                        + " (" + fareOut.ToString() + " )."));
                            }
                        }
                    }
                }

                if (toKeep)
                {
                    lowestFares.Add(fareOut);
                }
            }
            #endregion

            return lowestFares;
        }

        #endregion

        /// <summary>
		/// Finds the NLC codes of the start and end of the outward and return journeys
		/// </summary>
		/// <param name="trains">ArrayList of trains (outward, then return)</param>
		/// <returns>Array of four strings (outward start, end, return start, end</returns>
		private string[] GetJourneyEndPoints(ArrayList trains)
		{
			string outwardJourneyOriginNlc		= string.Empty;
			string outwardJourneyDestinationNlc = string.Empty;

			string returnJourneyOriginNlc		= string.Empty;
			string returnJourneyDestinationNlc	= string.Empty;

			int firstReturnTrainIndex = -1;

			for	(int i = 0; i < trains.Count; i++)
			{
				TrainDto train = (TrainDto)(trains[i]);
				if	(train.ReturnIndicator == ReturnIndicator.Return)
				{
					firstReturnTrainIndex = i;
					break;
				}
			}

			if	(firstReturnTrainIndex > 0)			// both outward & return trains found
			{
				outwardJourneyOriginNlc			= ((TrainDto)(trains[0])).Board.Location.Nlc;
				outwardJourneyDestinationNlc	= ((TrainDto)(trains[firstReturnTrainIndex - 1])).Alight.Location.Nlc;
				returnJourneyOriginNlc			= ((TrainDto)(trains[firstReturnTrainIndex])).Board.Location.Nlc;
				returnJourneyDestinationNlc		= ((TrainDto)(trains[trains.Count - 1])).Alight.Location.Nlc;
			}
			else if (firstReturnTrainIndex == -1)	// no return trains in list
			{
				outwardJourneyOriginNlc			= ((TrainDto)(trains[0])).Board.Location.Nlc;
				outwardJourneyDestinationNlc	= ((TrainDto)(trains[trains.Count - 1])).Alight.Location.Nlc;
			}
			else if (firstReturnTrainIndex == 0)	// no outward trains in list
			{
				returnJourneyOriginNlc			= ((TrainDto)(trains[0])).Board.Location.Nlc;
				returnJourneyDestinationNlc		= ((TrainDto)(trains[trains.Count - 1])).Alight.Location.Nlc;
			}

			return new string[] { outwardJourneyOriginNlc, outwardJourneyDestinationNlc, returnJourneyOriginNlc, returnJourneyDestinationNlc };
        }

        #region Restrict fares

        /// <summary>
		/// Given a set of Fares remove any invalid tickets
		/// </summary>
		/// <param name="rbo"></param>
		/// <param name="InterfaceVersion"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		private Fares RestrictFares(BusinessObject rbo, string InterfaceVersion, Fares fares, PricingRequestDto request) 
		{
			
			if ( TDTraceSwitch.TraceInfo ) 
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Info, 
                    "Restrict fares start: restricting fares for journey " + request.Origin.Nlc + " to " + request.Destination.Nlc + "." ) );
			}

            #region Identify the unique origin/destination NLCs
            // Build up the different Restriction calls that have to be made. 
            // Need a seperate call for each different combination of Fare Origin and Fare Destination
            ArrayList allOriginNLCs = new ArrayList();
            ArrayList allDestinationNLCs = new ArrayList();
            string nlc = string.Empty;

            // Used to determine which fare routes we need to restrict seperately
            ArrayList unknownFareRouteCodes = new ArrayList();

            foreach (Fare fare in fares.Item)
            {
                nlc = fare.FareOriginNlcActual;
                
                if ((!string.IsNullOrEmpty(nlc)) && (!allOriginNLCs.Contains(nlc)))
                {
                    allOriginNLCs.Add(nlc);
                }

                nlc = fare.FareDestinationNlcActual;

                if ((!string.IsNullOrEmpty(nlc)) && (!allDestinationNLCs.Contains(nlc)))
                {
                    allDestinationNLCs.Add(nlc);
                }

                if (!unknownFareRouteCodes.Contains(fare.Route.Code))
                {
                    unknownFareRouteCodes.Add(fare.Route.Code);
                }
            }

            #endregion

            #region Identify the fare route codes the train journeys don't know about
            // Fares with this route need to be restricted not using the routing guide check in the RBO call.
            
            // This is done to overcome an issue with the CJP/TTBO not knowing about all route codes.
            // e.g. for a london journey, the TTBO states First Capital Connect route 00031 is valid. However
            // this TOC also uses FCC route 00028 on fares. If routing guide is used to restrict fares, then
            // all 00028 fares are removed even though they are valid.

            foreach (TrainDto train in request.Trains)
            {
                if ((train.FareRouteCodes != null) && (train.FareRouteCodes.Length > 0))
                {
                    foreach (string trainRouteCode in train.FareRouteCodes)
                    {
                        if (unknownFareRouteCodes.Contains(trainRouteCode))
                        {
                            // Ensures only unknown route codes remain
                            unknownFareRouteCodes.Remove(trainRouteCode);
                        }
                    }
                }
            }

            #endregion

            // All unique fare origins and destinations have been found
            ArrayList faresToRestrict = new ArrayList();
            ArrayList faresToRestrictNonRoutingGuide = new ArrayList();
            ArrayList validFares = new ArrayList();

            // Now go through each combination of fare NLCs and restrict
            foreach (string nlcOrigin in allOriginNLCs)
            {
                foreach (string nlcDestination in allDestinationNLCs)
                {
                    faresToRestrict.Clear();
                    faresToRestrictNonRoutingGuide.Clear();

                    foreach (Fare fare in fares.Item)
                    {
                        if ((fare.FareOriginNlcActual.Equals(nlcOrigin)) && (fare.FareDestinationNlcActual.Equals(nlcDestination)))
                        {
                            // Add to fares to the appropriate check array
                            if (unknownFareRouteCodes.Contains(fare.Route.Code))
                            {
                                faresToRestrictNonRoutingGuide.Add(fare);
                            }
                            else
                            {
                                faresToRestrict.Add(fare);
                            }
                        }
                    }

                    // And remove fare so we don't check again. 
                    //A safety check to ensure we validate ALL fares, see further below
                    foreach (Fare fare in faresToRestrict)
                    {
                        fares.Item.Remove(fare);
                    }

                    foreach (Fare fare in faresToRestrictNonRoutingGuide)
                    {
                        fares.Item.Remove(fare);
                    }

                    #region Call restrict MR

                    if (faresToRestrict.Count > 0)
                    {
                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Trace.Write(new OperationalEvent(
                                TDEventCategory.Infrastructure,
                                TDTraceLevel.Verbose,
                                "Restricting fares using routing guide for Origin NLC[" + nlcOrigin + "] Destination NLC[" + nlcDestination + "]."));
                        }

                        // Call restrictions - use Routing Guide
                        validFares.AddRange(
                            CallRestrictFaresMR(rbo, InterfaceVersion, faresToRestrict, 
                                request, nlcOrigin, nlcDestination, true));
                    }

                    if (faresToRestrictNonRoutingGuide.Count > 0)
                    {
                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Trace.Write(new OperationalEvent(
                                TDEventCategory.Infrastructure,
                                TDTraceLevel.Verbose,
                                "Restricting fares not using routing guide for Origin NLC[" + nlcOrigin + "] Destination NLC[" + nlcDestination + "]."));
                        }

                        // Call restrictions - do not use Routing Guide
                        validFares.AddRange(
                            CallRestrictFaresMR(rbo, InterfaceVersion, faresToRestrictNonRoutingGuide,
                                request, nlcOrigin, nlcDestination, false));
                    }

                    #endregion
                }
            }

            // This should not happen as all fares will have been validated in above loop
            if (fares.Item.Count > 0)
            {
                if (TDTraceSwitch.TraceVerbose)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        "All fares were not passed through to the restrictions object, all remaining fares are now being restricted for journey " + request.Origin.Nlc + " to " + request.Destination.Nlc + "."));
                }

                validFares.AddRange(
                    CallRestrictFaresMR(rbo, InterfaceVersion, fares.Item, request, request.Origin.Nlc, request.Destination.Nlc, true));
            }

            // Update the fares to return to caller
            fares.Item = validFares;

            #region Log output
            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        "Restrict fares end: restriction calls have all been completed for journey " + request.Origin.Nlc + " to " + request.Destination.Nlc + "."));
            }
            #endregion

			return fares;
		}

        /// <summary>
        /// Helper method to call the Restrict Fares GL and MR.
        /// Ensure fares to restrict are all from the same origin NLC and to the same destination NLC
        /// </summary>
        /// <param name="rbo"></param>
        /// <param name="InterfaceVersion"></param>
        /// <param name="faresToRestrict">An arraylist of Fare</param>
        /// <param name="request"></param>
        /// <param name="nlcOrigin"></param>
        /// <param name="nlcDestination"></param>
        /// <returns></returns>
        private ArrayList CallRestrictFaresMR(BusinessObject rbo, string InterfaceVersion, ArrayList faresToRestrict, 
            PricingRequestDto request, string nlcOrigin, string nlcDestination, bool useRoutingGuide)
        {
            IRestrictFares restrictionController;
            BusinessObjectOutput output;

            ValidateRouteList validateRouteList;
            validateRouteList = new ValidateRouteList(InterfaceVersion, REGLOutputLength, request, faresToRestrict,
                nlcOrigin, nlcDestination);
            output = rbo.Process(validateRouteList);
            restrictionController = new RestrictFaresGL(validateRouteList, output);

            // Filters output from all non matching crs with intermediate stops, origin and destination
            output = restrictionController.FilterOutput(request);


            // Takes all the fares found for the train journeys requested, and validates them
            ValidateFaresForJourneyRequest validateFaresForJourney;
            validateFaresForJourney = new ValidateFaresForJourneyRequest(
                InterfaceVersion, REMROutputLength, request, faresToRestrict, output.OutputBody,
                nlcOrigin, nlcDestination, useRoutingGuide);
            output = rbo.Process(validateFaresForJourney);
            restrictionController = new RestrictFaresMR(validateFaresForJourney, output);

            return restrictionController.Restrict(faresToRestrict, null);
        }

		/// <summary>
		/// Given a set of Fares for a route (ie, no trains known yet) 
		///  remove any invalid tickets
		/// </summary>
		/// <param name="rbo"></param>
		/// <param name="InterfaceVersion"></param>
		/// <param name="request"></param>
		/// <returns></returns>
		private Fares RestrictFaresForRoute(BusinessObject rbo, string InterfaceVersion, Fares fares, PricingRequestDto request) 
		{
			BusinessObjectOutput output;

			if (TDTraceSwitch.TraceInfo) 
			{
				Trace.Write(new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Info, "Restricting fares for " + request.Origin.Nlc + " to " + request.Destination.Nlc + "." ));
			}

			if	(fares.Item.Count > 0)
			{
				ValidateFaresForRouteRequest validateFaresForRouteRequest = new ValidateFaresForRouteRequest(InterfaceVersion, REGROutputLength, request, fares);

				output = rbo.Process(validateFaresForRouteRequest);

				if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
				{
					fares.Item.Clear();
					fares.FatalError = true;
				}
				else
				{
					IRestrictFares restrictionController = new RestrictFaresGR(validateFaresForRouteRequest, output);			
					fares.Item = restrictionController.Restrict(fares.Item, null);
				}
			}

			return fares;
        }

        #endregion

        /// <summary>
		/// Calculate all fares (valid & invalid) for a given journey
		/// </summary>
		/// <param name="bo">FBO or ZPBO business object</param>
		/// <param name="InterfaceVersion"></param>
		/// <param name="request"></param>
        /// <param name="useZPBO">Flag to indicate whether the ZPBO business object is used</param>
		/// <returns>Fares</returns>
		private Fares CalculateFares( BusinessObject bo, string InterfaceVersion, PricingRequestDto request, bool useZPBO ) 
		{
			if ( TDTraceSwitch.TraceInfo ) 
			{
				Trace.Write(new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Info, "Calculating fares for " + request.Origin.Nlc + " (" + request.Origin.Crs + ") to " + request.Destination.Nlc + " (" + request.Destination.Crs + ")." ) );
			}

			LocationDto originalRequestOrigin	   = new LocationDto(request.Origin.Crs,	  request.Origin.Nlc);				// save off to restore at end ...
			LocationDto originalRequestDestination = new LocationDto(request.Destination.Crs, request.Destination.Nlc); 

			// Retrieve fares from the Business object (FBO or ZPBO) for the pricing request
			
			Fares fares = RequestFares(bo, InterfaceVersion, request, false, false, 1, useZPBO);

            // Don't need to do additional Group fares as ZPBO does it internally
            if (!useZPBO)
            {
                #region Fare group processing

                // If neither origin nor destination have been overridden with a fare group by the FBO, we
                //  need to check for fares from any fare group(s) that origin or destination belong to.   

                Fares groupFares = null;

                if ((fares.FareOriginNlc.Equals(request.Origin.Nlc)) && (fares.FareDestinationNlc.Equals(request.Destination.Nlc))
                        || (fares.Item.Count == 0))
                {
                    LookupTransform transform = new LookupTransform();

                    // Save origin station & obtain its fare groups
                    LocationDto originStation = request.Origin;
                    LocationDto[] originGroups = transform.GetFareGroupsForStation(originStation.Nlc, request.OutwardDate);

                    // Save destination station & obtain its fare groups
                    LocationDto destinationStation = request.Destination;
                    LocationDto[] destinationGroups = transform.GetFareGroupsForStation(destinationStation.Nlc, request.OutwardDate);

                    // Compute each Origin Station Group -> Destination Station fares

                    foreach (LocationDto originGroup in originGroups)
                    {
                        request.Origin = originGroup;

                        groupFares = RequestFares(bo, InterfaceVersion, request, true, false, 2, useZPBO);

                        if (groupFares.FatalError)
                        {
                            fares.FatalError = true;
                            break;
                        }

                        fares.Item.AddRange(groupFares.Item);

                        // then do fares from this origin group to each destination group ...
                        foreach (LocationDto destinationGroup in destinationGroups)
                        {
                            request.Destination = destinationGroup;

                            groupFares = RequestFares(bo, InterfaceVersion, request, true, true, 3, useZPBO);

                            if (groupFares.FatalError)
                            {
                                fares.FatalError = true;
                                break;
                            }

                            fares.Item.AddRange(groupFares.Item);
                        }
                    }


                    // Compute Origin Station -> each Destination Station Group

                    request.Origin = originStation;

                    foreach (LocationDto destinationGroup in destinationGroups)
                    {
                        request.Destination = destinationGroup;

                        groupFares = RequestFares(bo, InterfaceVersion, request, false, true, 4, useZPBO);

                        if (groupFares.FatalError)
                        {
                            fares.FatalError = true;
                            break;
                        }

                        fares.Item.AddRange(groupFares.Item);
                    }

                }
                else if (fares.FareOriginNlc.Equals(request.Origin.Nlc))
                {
                    // Just compute from each Origin Station Group -> Destination Station

                    LookupTransform transform = new LookupTransform();

                    LocationDto[] originGroups = transform.GetFareGroupsForStation(request.Origin.Nlc, request.OutwardDate);

                    foreach (LocationDto originGroup in originGroups)
                    {
                        request.Origin = originGroup;

                        groupFares = RequestFares(bo, InterfaceVersion, request, true, false, 5, useZPBO);

                        if (groupFares.FatalError)
                        {
                            fares.FatalError = true;
                            break;
                        }

                        fares.Item.AddRange(groupFares.Item);
                    }
                }
                else if (fares.FareDestinationNlc.Equals(request.Destination.Nlc))
                {
                    // Just compute from Origin Station -> each Destination Station

                    LookupTransform transform = new LookupTransform();

                    LocationDto[] destinationGroups = transform.GetFareGroupsForStation(request.Destination.Nlc, request.OutwardDate);

                    foreach (LocationDto destinationGroup in destinationGroups)
                    {
                        request.Destination = destinationGroup;

                        groupFares = RequestFares(bo, InterfaceVersion, request, false, true, 6, useZPBO);

                        if (groupFares.FatalError)
                        {
                            fares.FatalError = true;
                            break;
                        }

                        fares.Item.AddRange(groupFares.Item);
                    }
                }

                //  ... else Group to Group fare computed by original FBO call -- no further processing required.

                #endregion
            }

            request.Origin		= originalRequestOrigin;				// restore original values
			request.Destination = originalRequestDestination;

            // if we have had a Critical or Error level 
			//  failure at any point, it's not safe to 
			//  to return any of the results ...
			
			if	(fares.FatalError)
			{
				fares.Item.Clear();
			}

			return fares;
		}


		/// <summary>
		/// Perform Business Object call to retrieve fares and do verbose logging.
		/// </summary>
		/// <param name="bo">FBO or ZPBO business object</param>
		/// <param name="InterfaceVersion"></param>
		/// <param name="request"></param>
		/// <param name="identifier">for verbose logging only -- identifies which call this is</param>
        /// <param name="useZPBO">Flag to indicate whether the ZPBO business object is used</param>
		/// <returns></returns>
		private Fares RequestFares(BusinessObject bo, string InterfaceVersion, PricingRequestDto request, bool originIsGroup, bool destIsGroup, int identifier, bool useZPBO)
        {
            FareRequest fareRequest = new FareRequest(InterfaceVersion, request, FAGFOutputLength, useZPBO);

            string businessObject = (useZPBO) ? "ZPBO" : "FBO"; // Used only for logging
            
            if (TDTraceSwitch.TraceVerbose)
            {
                string message = "Calling " + businessObject + " (call " + identifier.ToString() + ") for " + request.Origin.Nlc + " to " + request.Destination.Nlc;

                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, message));
            }

            Fares fares = new Fares(bo.Process(fareRequest), request.Origin.Nlc, request.Destination.Nlc, originIsGroup, destIsGroup, request.OutwardDate, useZPBO);

            #region Log output
            if (TDTraceSwitch.TraceVerbose)
            {
                string originNlc;

                if (fares.FareOriginNlc.Length > 0 && !fares.FareOriginNlc.Equals(request.Origin.Nlc))
                {
                    originNlc = fares.FareOriginNlc + " (overridden by " + businessObject + ")";
                }
                else
                {
                    originNlc = request.Origin.Nlc;
                }

                string destinationNlc;

                if (fares.FareDestinationNlc.Length > 0 && !fares.FareDestinationNlc.Equals(request.Destination.Nlc))
                {
                    destinationNlc = fares.FareDestinationNlc + " (overridden by " + businessObject + ")";
                }
                else
                {
                    destinationNlc = request.Destination.Nlc;
                }

                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, businessObject + " found " + fares.Item.Count + " fares (call " + identifier.ToString() + ") for " + originNlc + " to " + destinationNlc));
            }
            #endregion

            return fares;
        }

        /// <summary>
        /// Loops through a set of TicketDto fares, and where a ticket is a Travelcard, 
        /// switches the "Single" fare indicator to be a "Return" fare.
        /// </summary>
        /// <param name="tickets">Original set of fares - an ArrayList of TicketDto objects</param>
        /// <returns>Updated ArrayList of TicketDto objects</returns>
        private ArrayList ProcessTravelcardTickets(JourneyType journeyType, ArrayList tickets)
        {
            IDataServices dataServices = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            Hashtable travelcardTicketTypes = dataServices.GetHash(DataServiceType.FareTravelcardTicketTypes);

            ArrayList newTicketsList = new ArrayList(tickets.Count);

            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Checking for Travelcard tickets. "));
            }

            foreach (TicketDto ticket in tickets)
            {
                if (travelcardTicketTypes.Contains(ticket.TicketCode))
                {
                    // Note: This should no longer happen because the ZPBO/FBO correctly set the ticket.JourneyType to be a "Return" ticket

                    // Its an identified Travelcard ticket, update journey type
                    if (ticket.JourneyType == JourneyType.OutwardSingle)
                    {
                        ticket.JourneyType = JourneyType.Return;

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Updating Travelcard ticket [" + ticket.TicketCode + "] to have a JourneyType.Return. "));
                        }
                    }
                }

                // not interested in return tickets if this request is for inward singles ...
                if (!(journeyType == JourneyType.InwardSingle && ticket.JourneyType == JourneyType.Return))
                {
                    newTicketsList.Add(ticket);
                }
                else
                {
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Dropping Travelcard ticket [" + ticket.TicketCode + "] from the InwardSingle journey fare request."));
                    }
                }
            }

            return newTicketsList;
        }

        /// <summary>
        /// Validates tickets to check if Day Return tickets are valid for the date specified
        /// </summary>
        /// <param name="outwardDate">Outward date</param>
        /// <param name="returnDate">Return date</param>
        /// <param name="tickets">Tickets to validate</param>
        /// <returns>Tickets valid on the journey date specified</returns>
        private ArrayList ProcessTicketsForValidDayReturn(TDDateTime outwardDate, TDDateTime returnDate, ArrayList tickets)
        {
            ArrayList validTicketsResult = new ArrayList(tickets.Count);

            IDataServices dataServices = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

            Hashtable validTicketTypes = dataServices.GetHash(DataServiceType.DisplayableRailTickets);
                        
            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Checking for validity of same day return tickets. "));
            }
            if (outwardDate != null && returnDate != null)
            {
                if (outwardDate.GetDateTime().Date == returnDate.GetDateTime().Date)
                {
                    // Outward data and return date are same so no need to filter out the  same day return tickets
                    // All tickets are valid
                    validTicketsResult = tickets;
                }
                else
                {
                    foreach (TicketDto ticket in tickets)
                    {
                        // Only need to validate the tickets with journey type as return
                        // single tickets doens't need to get validated
                        if (ticket.JourneyType == JourneyType.Return)
                        {
                            // The ticket code should be found in the displayable ticket codes
                            // If it not found don't add tickets to valid ticket collection
                            if (validTicketTypes.Contains(ticket.TicketCode))
                            {
                                bool ticketsValid = true;

                                CategorisedHashData ticketData = (CategorisedHashData)validTicketTypes[ticket.TicketCode];
                                if (ticketData.ExtData != null && ticketData.ExtData.Length > 0)
                                {
                                    if (!string.IsNullOrEmpty(ticketData.ExtData[0]))
                                    {
                                        bool sameDayReturn = false;

                                        if (!bool.TryParse(ticketData.ExtData[0], out sameDayReturn))
                                        {
                                            sameDayReturn = false;
                                        }
                                        ticketsValid = !sameDayReturn;
                                    }
                                }

                                if (ticketsValid)
                                {
                                    validTicketsResult.Add(ticket);
                                }
                            }
                        }
                        else
                        {
                            validTicketsResult.Add(ticket);
                        }

                    }
                }
            }



            return validTicketsResult;
        }

        /// <summary>
        /// Formats a TicketDto object into a string
        /// </summary>
        /// <returns></returns>
        private string TicketDtoToString(PricingMessages.TicketDto ticketDto)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder sbTemp = new StringBuilder(); // used to insert some padding to align the output

            sb.Append("TicketName[").Append(ticketDto.TicketCode).Append("] ");

            sbTemp.Append("TicketType[").Append(ticketDto.JourneyType).Append("] ");
            sb.Append(sbTemp.ToString().PadRight(26, ' '));
            sbTemp = new StringBuilder();

            sb.Append("Route[").Append(ticketDto.RouteCode).Append("] ");
            sb.Append("Origin[").Append(ticketDto.OriginNlc).Append(" ").Append(ticketDto.OriginName).Append("] ");
            sb.Append("Destination[").Append(ticketDto.DestinationNlc).Append(" ").Append(ticketDto.DestinationName).Append("] ");
            sb.Append("OriginActual[").Append(ticketDto.FareOriginNlcActual).Append(" ").Append(" ").Append("] ");
            sb.Append("DestinationActual[").Append(ticketDto.FareDestinationNlcActual).Append(" ").Append(" ").Append("] ");
            //sb.Append("CrossLondon[").Append(route.CrossLondon).Append("] ");
            sb.Append("RestrictionCode[").Append(ticketDto.RestrictionCode).Append("] ");
            //sb.Append("ticketValidityCode[").Append(ticketDto.TicketValidityCode).Append("] ");
            sb.Append("railcardCode[").Append(ticketDto.Railcard).Append("] ");
            sb.Append("adultFare[").Append(ticketDto.AdultFare.ToString().PadLeft(6, ' ')).Append("] ");
            //sb.Append("adultFareError[").Append(ticketDto.AdultFareError).Append("] ");
            //sb.Append("adultFareMinimum[").Append(ticketDto.AdultFareMinimum.ToString().PadLeft(6, ' ')).Append("] ");
            sb.Append("childFare[").Append(ticketDto.ChildFare.ToString().PadLeft(6, ' ')).Append("] ");
            //sb.Append("childFareError[").Append(ChildFareError).Append("] ");
            //sb.Append("childFareMinimum[").Append(ChildFareMinimum.ToString().PadLeft(6, ' ')).Append("] ");

            sbTemp.Append("ticketClass[").Append(ticketDto.TicketClass).Append("] ");
            sb.Append(sbTemp.ToString().PadRight(22, ' '));
            sbTemp = new StringBuilder();

            //sb.Append("tocCode[").Append(ticketDto.TocCode).Append("] ");
            sb.Append("quotaControlled[").Append(ticketDto.QuotaControlled).Append("] ");
            //sb.Append("invalidForTrains[").Append(ticketDto.InvalidForTrains).Append("] ");

            return sb.ToString();
        }

        /// <summary>
        /// Method which returns true if any of the trains in the request have a route 
        /// code which matches the fare route code
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fare"></param>
        /// <returns></returns>
        private bool UseRoutingGuideValidation(PricingRequestDto request, Fare fare)
        {
            bool useRoutingGuide = false;
            
            #region Identify the fare route codes the train journeys don't know about
            // Fares with this route need to be restricted not using the routing guide check in the RBO call.

            // This is done to overcome an issue with the CJP/TTBO not knowing about all route codes.
            // e.g. for a london journey, the TTBO states First Capital Connect route 00031 is valid. However
            // this TOC also uses FCC route 00028 on fares. If routing guide is used to restrict fares, then
            // all 00028 fares are removed even though they are valid.

            foreach (TrainDto train in request.Trains)
            {
                if ((train.FareRouteCodes != null) && (train.FareRouteCodes.Length > 0))
                {
                    foreach (string trainRouteCode in train.FareRouteCodes)
                    {
                        if (trainRouteCode == fare.Route.Code)
                        {
                            useRoutingGuide = true;
                            break;
                        }
                        
                    }
                }
            }

            #endregion
            
            return useRoutingGuide;
        }

        #endregion

        #region Public methods - Search by Time

        /// <summary>
		/// Calculate valid fares for a given journey leg
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>		
		public PricingResultDto GetFaresForSingleJourney(PricingRequestDto request) 
		{
		
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Entry"));

            #region Set up the variables
            PricingResultDto response = null;

			ArrayList validTickets = new ArrayList();
			ArrayList errors = new ArrayList();
			ArrayList rvboResults = new ArrayList(request.Trains.Count);

			Hashtable fareAvailabilities;

			Fares fares;

			RBOPool  rboPool  = RBOPool.GetRBOPool();
            SBOPool  sboPool  = SBOPool.GetSBOPool();
            FBOPool fboPool   = null;
            ZPBOPool zpboPool = null;

			BusinessObject fbo  = null;
			BusinessObject rbo  = null;
            BusinessObject zpbo = null;

			bool noSinglePlacesAvailable = false; 
			bool noReturnPlacesAvailable = false;
			bool noThroughFaresAvailable = false;

			FareAvailabilityCombined currentFareAvailabilityCombined;
			FareAvailability currentFareAvailability;

			bool noTrainsFound = false;

            bool useZPBO = ZPBOPool.UseZPBO();
            
            #endregion

            try 
			{
                // Check if we need to use the FBO or ZPBO and proceed accordingly
                if (useZPBO)
                {
                    #region Call ZPBO
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "ZPBO being used"));

                    zpboPool = ZPBOPool.GetZPBOPool();

                    // Get an instance of the ZPBO
                    zpbo = zpboPool.GetInstance();

                    // Calculate fares
                    fares = CalculateFares(zpbo, zpboPool.InterfaceVersion, request, useZPBO);

                    // Release the ZPBO
                    zpboPool.Release(ref zpbo);
                    #endregion
                }
                else
                {
                    #region Call FBO
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "FBO being used"));

                    fboPool = FBOPool.GetFBOPool();

                    // Get an instance of the FBO
                    fbo = fboPool.GetInstance();

                    // Calculate fares
                    fares = CalculateFares(fbo, fboPool.InterfaceVersion, request, useZPBO);

                    // Release the FBO
                    fboPool.Release(ref fbo);
                    #endregion
                }

				if	(fares.FatalError)
				{
					errors.Add(UNSPECIFIED_BO_ERROR);
                }

                #region Log output
                // If verbose trace print out each of the fares returned
				// before performing restrictions
				if (TDTraceSwitch.TraceVerbose) 
				{
					Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "All fares returned (" + fares.Item.Count + ")") ); 
					
					foreach (Fare fare in fares.Item )
					{
						Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format(Messages.BOFareDetails, fare.ToString()))); 
					}
                }
                #endregion

                fares.Item = RemoveUndisplayableFares(request.JourneyType, fares.Item);

                #region Call RBO
                // Restrict all adult fares using the RBO
				rbo = rboPool.GetInstance();

				// Restrict fares
				fares = RestrictFares( rbo, rboPool.InterfaceVersion, fares, request );

				// Release the rbo
				rboPool.Release(ref rbo);
                #endregion
                                
                fares.Item = RemoveDuplicateFares(fares.Item);

                // Need to Restrict fares BEFORE performing further removes to ensure we don't prematurely
                // remove fares for valid routes (just because they're a duplicate).
                fares.Item = RemoveTerminalZoneDuplication(fares.Item);

                fares.Item = RemoveTravelcardZoneDuplication(fares.Item);
                                
				//If no fares were returned by the RBO, then set noThroughFaresAvailable to true, as this
				//indicates that the combination of route and operators is not fareable
				if	(fares.Item.Count == 0)
				{
					noThroughFaresAvailable = true;
                }

                #region Log output
                // If verbose trace print out each of the fares returned
				// before performing restrictions
				if (TDTraceSwitch.TraceVerbose) 
				{
					Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Following fares are valid(" + fares.Item.Count + ")") ); 
					
					foreach (Fare fare in fares.Item)
					{
						Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format(Messages.BOFareDetails, fare.ToString()))); 
					}
                }
                #endregion

                if	(fares.Item.Count > 0)
                {
                    #region Call SBO
                    BusinessObject sbo  = null;
					sbo	= sboPool.GetInstance();

					//Create TicketTypeCodes array from Fares for MandatoryReservationsRequest parameter
					string[] ticketTypeCodes = new string[fares.Item.Count];
					for(int i = 0;i<fares.Item.Count;i++)
					{
						ticketTypeCodes[i] = (fares.Item[i]as Fare).TicketType.Code;
					}

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("Performing mandatory reservation check for fares")));
                    }

					//Create Mandatory Reservations Request
					MandatoryReservationsRequest mandatoryReservationsRequest = new MandatoryReservationsRequest(sboPool.InterfaceVersion,ticketTypeCodes);
					//Find Mandatory Reservation data for ticket types
					MandatoryReservations mandatoryReservations =  new MandatoryReservations(sbo.Process(mandatoryReservationsRequest),mandatoryReservationsRequest.TicketTypeCodes);
					
					sboPool.Release(ref sbo);
					sbo = null;

					//Flag NRS Unavailable if mandatoryReservations has errors
					if (mandatoryReservations.HasErrors)
					{
						errors.Add(NRS_UNAVAILABLE);
                    }
                    #endregion

                    #region Check fare availability

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("Performing availability check for fares")));
                    }

                    fareAvailabilities = new Hashtable(fares.Item.Count);

					foreach (Fare fare in fares.Item) 
					{
						currentFareAvailability = new FareAvailability(sboPool, request, fare);
					
						if (fareAvailabilities[currentFareAvailability.FareKey] == null)
						{
							fareAvailabilities.Add(currentFareAvailability.FareKey, new FareAvailabilityCombined(currentFareAvailability.FareKey));
						}

						if (currentFareAvailability.IsWithDiscount)
						{
							((FareAvailabilityCombined)fareAvailabilities[currentFareAvailability.FareKey]).FareWithDiscount = currentFareAvailability;
						}
						else
						{
							((FareAvailabilityCombined)fareAvailabilities[currentFareAvailability.FareKey]).FareWithoutDiscount = currentFareAvailability;
						}
					}

					// Check availability for Outward journey
					//Call CheckAvailability - Multiple fare implemention
					CheckAvailability(request, ref fareAvailabilities, false, rvboResults, errors, true, mandatoryReservations.Results, ref noTrainsFound);

					if (request.JourneyType == JourneyType.Return)
					{
						//Call CheckAvailability - Multiple fare implemention
						CheckAvailability(request, ref fareAvailabilities, false, rvboResults, errors, false, mandatoryReservations.Results, ref noTrainsFound);
					}

					foreach (string fareKey in fareAvailabilities.Keys)
					{
						currentFareAvailabilityCombined = (FareAvailabilityCombined)fareAvailabilities[fareKey];

                        #region Log output
                        // If verbose trace print out the availability for the current fare
						if (TDTraceSwitch.TraceVerbose) 
						{
							Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
								string.Format(Messages.BOFareAvailability, fareKey, request.Railcard, currentFareAvailabilityCombined.UndiscountedOutwardPlacesAvailable.ToString(), currentFareAvailabilityCombined.DiscountedOutwardPlacesAvailable.ToString(), currentFareAvailabilityCombined.UndiscountedInwardPlacesAvailable.ToString(), currentFareAvailabilityCombined.DiscountedInwardPlacesAvailable.ToString())));
                        }
                        #endregion

                        bool currentFareAvailable = false;
                                                
						if	(currentFareAvailabilityCombined.UndiscountedInwardPlacesAvailable && currentFareAvailabilityCombined.UndiscountedOutwardPlacesAvailable)	
						{
                            if (currentFareAvailabilityCombined.FareWithoutDiscount != null)
							{
								AddValidTicket(request, currentFareAvailabilityCombined.FareWithoutDiscount, ref validTickets, sboPool);
								currentFareAvailable = true;
							}
                        }

                        #region Discounted fare places available 

                        if	(currentFareAvailabilityCombined.DiscountedInwardPlacesAvailable && currentFareAvailabilityCombined.DiscountedOutwardPlacesAvailable)	
						{
						
							if (currentFareAvailabilityCombined.FareWithDiscount != null)
							{
								if	(!currentFareAvailabilityCombined.UndiscountedInwardPlacesAvailable || !currentFareAvailabilityCombined.UndiscountedOutwardPlacesAvailable)
								{
									// UI logic assumes that we always have an undiscounted ticket,  
									//  so if only discounted available, create a "dummy" undiscounted
									//  ticket without a valid fare ...

									currentFareAvailabilityCombined.FareWithoutDiscount.Ticket.AdultFare = float.NaN;
									currentFareAvailabilityCombined.FareWithoutDiscount.Ticket.ChildFare = float.NaN;
									AddValidTicket(request, currentFareAvailabilityCombined.FareWithoutDiscount, ref validTickets, sboPool);
								}
	
								AddValidTicket(request, currentFareAvailabilityCombined.FareWithDiscount, ref validTickets, sboPool);
								currentFareAvailable = true;
							}
                        }

                        #endregion

                        if	(!currentFareAvailable)
						{
							if (((currentFareAvailabilityCombined.FareWithoutDiscount != null) && (currentFareAvailabilityCombined.FareWithoutDiscount.Ticket.JourneyType == JourneyType.Return))
								|| ((currentFareAvailabilityCombined.FareWithDiscount != null) && (currentFareAvailabilityCombined.FareWithDiscount.Ticket.JourneyType == JourneyType.Return)))
							{
								// since either outward or return legs (or both) fully 
								//  booked, we have at least one return ticket that is 
								//  valid but unavailable ... set flag for UI message

								noReturnPlacesAvailable = true;
							}
							else if	(!currentFareAvailabilityCombined.UndiscountedOutwardPlacesAvailable && !currentFareAvailabilityCombined.DiscountedOutwardPlacesAvailable)	
							{
								// if outward leg is fully booked, we have 
								//  at least one single ticket that is valid
								//  but unavailable ... set flag for UI message

								noSinglePlacesAvailable = true;
							}
						}
					}

					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "noReturn = " + noReturnPlacesAvailable.ToString() + ", noSingles = " +  noSinglePlacesAvailable.ToString()));

                    #endregion
                }

                #region Log output
                if (TDTraceSwitch.TraceVerbose)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Following are the RVBO results"));

                    if (rvboResults.Count == 0)
                    {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
                            string.Format("No RVBO results were found")));
                    }
                    else
                    {
                        foreach (RailAvailabilityResultDto rard in (RailAvailabilityResultDto[])(rvboResults.ToArray(typeof(RailAvailabilityResultDto))))
                        {
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format(Messages.BORailAvailabilityResult, rard.ToString())));
                        }
                    }
                }
                #endregion

                validTickets = CheapestAdvancedGroupFare(validTickets);

                // Set all travel card fares to be a "Return" ticket type
                validTickets = ProcessTravelcardTickets(request.JourneyType, validTickets);

                #region Log output
                if (TDTraceSwitch.TraceVerbose)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
                        string.Format("Following fares are valid({0})",validTickets.Count)));

                    foreach (TicketDto ticket in (TicketDto[])(validTickets.ToArray(typeof(TicketDto))))
                    {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
                            string.Format(Messages.BOFareDetails, TicketDtoToString(ticket))));
                    }
                }
                #endregion

				response = new PricingResultDto(request.OutwardDate, request.ReturnDate, fares.MinimumChildAge, fares.MaximumChildAge, fares.Currency, validTickets, request.JourneyType, noSinglePlacesAvailable, noReturnPlacesAvailable, noThroughFaresAvailable);

				foreach (string errorId in errors)
				{
					response.AddErrorMessage(errorId);
				}

				response.RVBOResults = (RailAvailabilityResultDto[])(rvboResults.ToArray(typeof(RailAvailabilityResultDto)));
            } 
			catch (TDException tde) 
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Warning, "An error was encountered while calculating fares::", tde ) );
				throw tde; 
			} 
			catch (Exception e) 
			{
				TDException td = new TDException( "Unexpected error encountered while restricting fares", e, true, TDExceptionIdentifier.PRHUnexpectedError );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure,TDTraceLevel.Warning, td.InnerException.Message, e ) );
				throw td;
			}
			finally
            {
                #region Release business objects
                // Release the FBO if it is not done already
				if (fbo != null) 
				{
					fboPool.Release (ref fbo);
				}

				// Release the RBO if it is not done already
				if (rbo != null) 
				{
					rboPool.Release(ref rbo);
				}

                // Release the ZPBO if it is not done already
                if (zpbo != null)
                {
                    zpboPool.Release(ref zpbo);
                }
                #endregion
            }
			
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Exit"));

			return response;
        }

        #endregion

        /// <summary>
        /// Removes fares belonging to the same group, leaving only the cheapest in the group
        /// in the validTickets arraylist.
        /// </summary>
        /// <param name="validTickets"></param>
        /// <returns></returns>
        private ArrayList CheapestAdvancedGroupFare(ArrayList validTickets)
		{
            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("CheapestAdvancedGroupFare - removing duplicate fares in groups")));
            }

			//Remove fares belonging to the same group, leaving only the cheapest in the group
			//in the validTickets arraylist.
			IDataServices dataServices = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			Hashtable groupedTickets = dataServices.GetHash(DataServiceType.DisplayableRailTickets);

			ArrayList newFareList = new ArrayList(validTickets.Count);
			Hashtable newFares = new Hashtable();
			Hashtable discountFares = new Hashtable();

            int faresDroppedCount = 0;
				
			foreach (PricingMessages.TicketDto fare in validTickets) 
			{ 
				//Check that 2 or more fares belonging to the same group are in the fares object.
				//If they are then only leave the lowest fare in that group.
				if (((CategorisedHashData)groupedTickets[fare.TicketCode]).Group != NON_GROUPED_TICKETS_IDENTIFIER)
				{
					//Then the ticket is a member of a group.  Collect these tickets, and once complete
					//go through and leave only the lowest of each group.						
					if (newFares.ContainsKey(((CategorisedHashData)groupedTickets[fare.TicketCode]).Group))
					{
						//If the hashtable already has a fare for that group then check the value of it
                        if ((fare.AdultFare < (((PricingMessages.TicketDto)newFares[((CategorisedHashData)groupedTickets[fare.TicketCode]).Group]).AdultFare)) && fare.Railcard == "   ")
                        {
                            #region Log dropping fare
                            // Log we've dropped a Group Duplicate fare
                            PricingMessages.TicketDto oldFare = (PricingMessages.TicketDto)newFares[((CategorisedHashData)groupedTickets[fare.TicketCode]).Group];

                            if (TDTraceSwitch.TraceVerbose)
                            {
                                Trace.Write(new OperationalEvent(
                                    TDEventCategory.Infrastructure,
                                    TDTraceLevel.Verbose,
                                    string.Format("Dropping duplicate fare for group: [{0}] ( {1} ).",
                                        ((CategorisedHashData)groupedTickets[fare.TicketCode]).Group,
                                        TicketDtoToString(oldFare))));
                            }

                            faresDroppedCount++;

                            #endregion

                            // Then remove the old one, and add this one
                            newFares.Remove(((CategorisedHashData)groupedTickets[fare.TicketCode]).Group);
                            newFares.Add(((CategorisedHashData)groupedTickets[fare.TicketCode]).Group, fare);
                        }
                        else
                        {
                            // Fare is dropped
                            #region Log dropping fare
                            if (TDTraceSwitch.TraceVerbose)
                            {
                                Trace.Write(new OperationalEvent(
                                    TDEventCategory.Infrastructure,
                                    TDTraceLevel.Verbose,
                                    string.Format("Dropping duplicate fare for group: [{0}] ( {1} ).",
                                        ((CategorisedHashData)groupedTickets[fare.TicketCode]).Group,
                                        TicketDtoToString(fare))));
                            }

                            faresDroppedCount++;
                            #endregion
                        }

						if (fare.Railcard != "   ")
						{
							if ((discountFares.ContainsKey(((CategorisedHashData)groupedTickets[fare.TicketCode]).Group)))
							{
								if ((fare.AdultFare < (((PricingMessages.TicketDto)discountFares[((CategorisedHashData)groupedTickets[fare.TicketCode]).Group]).AdultFare)) && fare.Railcard != "   ")
								{
                                    #region Log dropping fare
                                    // Log we've dropped a Group Duplicate fare
                                    PricingMessages.TicketDto oldFare = (PricingMessages.TicketDto)discountFares[((CategorisedHashData)groupedTickets[fare.TicketCode]).Group];
                            
                                    if (TDTraceSwitch.TraceVerbose)
                                    {
                                        Trace.Write(new OperationalEvent(
                                            TDEventCategory.Infrastructure,
                                            TDTraceLevel.Verbose,
                                            string.Format("Dropping duplicate fare for group: [{0}] ( {1} ).",
                                                ((CategorisedHashData)groupedTickets[fare.TicketCode]).Group,
                                                TicketDtoToString(oldFare))));
                                    }

                                    faresDroppedCount++;
                                    #endregion

									// Then remove the old one, and add this one
									discountFares.Remove(((CategorisedHashData)groupedTickets[fare.TicketCode]).Group);
									discountFares.Add(((CategorisedHashData)groupedTickets[fare.TicketCode]).Group, fare);
								}
                                else
                                {
                                    // Fare is dropped
                                    #region Log dropping fare
                                    if (TDTraceSwitch.TraceVerbose)
                                    {
                                        Trace.Write(new OperationalEvent(
                                            TDEventCategory.Infrastructure,
                                            TDTraceLevel.Verbose,
                                            string.Format("Dropping duplicate fare for group: [{0}] ( {1} ).",
                                                ((CategorisedHashData)groupedTickets[fare.TicketCode]).Group,
                                                TicketDtoToString(fare))));
                                    }

                                    faresDroppedCount++;
                                    #endregion
                                }

							}
							else
							{
								discountFares.Add(((CategorisedHashData)groupedTickets[fare.TicketCode]).Group, fare);
							}
						}

					}
					else
						//This is the first occurance of this ticket type group we have come across, add it to the hashtable
					{
						if(fare.Railcard == "   ")
						{
							newFares.Add(((CategorisedHashData)groupedTickets[fare.TicketCode]).Group, fare);
						}
									
						else if (fare.Railcard != "   ")
						{
							discountFares.Add(((CategorisedHashData)groupedTickets[fare.TicketCode]).Group, fare);
						}	
			
					}
				}
			
				else
				{
					//Add the ticket as it is not a member of a group.
					newFareList.Add(fare);
				}

			}

            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("Dropped ({0}) duplicate fares for groups", faresDroppedCount)));
            }

			//Now move all the data from the Hashtable to the ArrayList
			foreach(DictionaryEntry ticket in newFares)
			{
				newFareList.Add((PricingMessages.TicketDto)ticket.Value);
			}

			foreach(DictionaryEntry ticket in discountFares)
			{
				newFareList.Add((PricingMessages.TicketDto)ticket.Value);
			}


			validTickets.RemoveRange(0, validTickets.Count);
			foreach (PricingMessages.TicketDto ticket in newFareList)
			{
				validTickets.Add(ticket);

			}

			return validTickets;

        }

        #region Public methods - Search by Price

        /// <summary>
        /// Calculates all fares for a given route
        /// </summary>
        /// <param name="requests"></param>
        /// <returns></returns>
		public PricingResultDto[] GetFaresForRoute(PricingRequestDto[] requests) 
		{
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Entry"));

            #region Set up the variables
            PricingResultDto[] responses = new PricingResultDto[requests.Length];

			RBOPool rboPool = RBOPool.GetRBOPool();
            FBOPool fboPool = null;
            ZPBOPool zpboPool = null;

			BusinessObject fbo = null;
			BusinessObject rbo = null;
            BusinessObject zpbo = null;
			
			Fares fares = null;
			Fares restrictedFares = null;
			TDDateTime previousOutwardDate = DateTime.MinValue;
			LocationDto previousOrigin = null;
			LocationDto previousDestination = null;

            bool useZPBO = ZPBOPool.UseZPBO();

            bool fatalError = false;

            #endregion

            try 
			{
				for (int i = 0; i < requests.Length; i++) 
				{
					// Do this bit for outward dates only 
					//  - assumes requests sorted by origin/destination, 
					//     then by outward date order ...
					
					if	(requests[i].OutwardDate > previousOutwardDate 
							|| requests[i].Origin != previousOrigin
							|| requests[i].Destination != previousDestination)
					{
						previousOutwardDate = requests[i].OutwardDate;
						previousOrigin      = requests[i].Origin;
						previousDestination = requests[i].Destination;

                        // Check if we need to use the FBO or ZPBO and proceed accordingly
                        if (useZPBO)
                        {
                            #region Call ZPBO
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "ZPBO being used"));

                            zpboPool = ZPBOPool.GetZPBOPool();

                            // Get an instance of the ZPBO
                            zpbo = zpboPool.GetInstance();

                            // Calculate fares
                            fares = CalculateFares(zpbo, zpboPool.InterfaceVersion, requests[i], useZPBO);

                            // Release the ZPBO
                            zpboPool.Release(ref zpbo);
                            #endregion
                        }
                        else
                        {
                            #region Call FBO
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "FBO being used"));

                            fboPool = FBOPool.GetFBOPool();

                            // Get an instance of the FBO
                            fbo = fboPool.GetInstance();

                            // Calculate fares
                            fares = CalculateFares(fbo, fboPool.InterfaceVersion, requests[i], useZPBO);

                            // Release the FBO
                            fboPool.Release(ref fbo);
                            #endregion
                        }

						fatalError = fares.FatalError;

                        #region Log output
                        if (TDTraceSwitch.TraceVerbose) 
						{
							Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "All fares returned (" + fares.Item.Count + ")") ); 
							
							foreach (Fare fare in fares.Item)
							{
								Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format(Messages.BOFareDetails, fare.ToString()))); 
							}
                        }
                        #endregion

                        fares.Item = RemoveUndisplayableFares(requests[i].JourneyType, fares.Item);
					}

					// Do this bit for outward and return date permutations (reusing previous
                    //   value of fares, if same outward date and origin/destination locations)

                    #region Call RBO
                    rbo = rboPool.GetInstance();

					restrictedFares = RestrictFaresForRoute(rbo, rboPool.InterfaceVersion, fares, requests[i]);

					if	(restrictedFares.FatalError)
					{
						fatalError = true;
					}

					rboPool.Release(ref rbo);
                    #endregion
                    
                    #region Log output
                    if (TDTraceSwitch.TraceVerbose) 
					{
						Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Following fares are valid" ) ); 
						
						foreach (Fare fare in fares.Item)
						{
							Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format(Messages.BOFareDetails, fare.ToString()))); 
						}
                    }
                    #endregion

                    restrictedFares.Item = RemoveDuplicateFares(restrictedFares.Item);

                    ArrayList validTickets = new ArrayList();

					foreach (Fare fare in restrictedFares.Item) 
					{
						JourneyType type = fare.TicketType.Type;

						// Some juggling required here because the *fare* always has type 
						//  OutwardSingle or Return, but the *ticket* is really for an 
						//  InwardSingle if the request for an InwardSingle ... 

						if	((type == JourneyType.Return) && (requests[i].JourneyType == JourneyType.InwardSingle))
						{
							continue;			// "return" fares for inward journey are irrelevant to us
						}

						if	((type == JourneyType.OutwardSingle) && (requests[i].JourneyType == JourneyType.InwardSingle))
						{
							type = JourneyType.InwardSingle;
						}
						
						TicketDto ticket = new TicketDto(
                            fare.TicketType.Code, fare.Route.Code, fare.AdultFare, fare.ChildFare, 
                            fare.AdultFareMinimum, fare.ChildFareMinimum, fare.TicketType.TicketClass, 
                            fare.RailcardCode, fare.QuotaControlled, type, fare.RestrictionCode,
                            fare.FareOriginNlc, fare.FareDestinationNlc, fare.Origins, fare.Destinations,
                            fare.OriginName, fare.DestinationName, fare.FareOriginNlcActual, fare.FareDestinationNlcActual,
                            fare.RawFareString); 
						
						ticket.IsFromCostSearch = true;

						validTickets.Add(ticket);
					}

                    // Set all travel card fares to be a "Return" ticket type
                    validTickets = ProcessTravelcardTickets(requests[i].JourneyType,validTickets);

                    if (requests[i].JourneyType == JourneyType.Return && requests[i].OutwardDate != null && requests[i].ReturnDate != null)
                    {
                        validTickets = ProcessTicketsForValidDayReturn(requests[i].OutwardDate, requests[i].ReturnDate, validTickets);
                    }

					responses[i] = new PricingResultDto(requests[i].OutwardDate, requests[i].ReturnDate, restrictedFares.MinimumChildAge, restrictedFares.MaximumChildAge, 
										restrictedFares.Currency, validTickets, requests[i].JourneyType, false, false, false);

					if	(fatalError)
					{
						responses[i].AddErrorMessage(UNSPECIFIED_BO_ERROR);
					}
				}
			} 
			catch (TDException tde) 
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Warning, "An error was encountered while calculating fares::", tde ) );
				throw tde; 
			} 
			catch (Exception e ) 
			{
				TDException td = new TDException( "Unexpected error encountered while restricting fares", e, true, TDExceptionIdentifier.PRHUnexpectedError );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure,TDTraceLevel.Warning, td.InnerException.Message, e ) );
				throw td;
			}
			finally
            {
                #region Release business objects
                // Release the FBO if it is not done already
                if (fbo != null) 
				{
					fboPool.Release(ref fbo);
				}

                // Release the RBO if it is not done already
				if (rbo != null) 
				{
					rboPool.Release(ref rbo);
				}

                // Release the ZPBO if it is not done already
                if (zpbo != null)
                {
                    zpboPool.Release(ref zpbo);
                }
                #endregion
			}
			
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Exit"));

			return responses;
		}

        

        /// <summary>
        /// Method to validate the fare and set parameters before a call is made to get journeys for 
        /// the fare which is selected
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fareData"></param>
        /// <returns></returns>
		public TTBOParametersDto[] GetServiceParametersForFare(PricingRequestDto request, FareDataDto[] fareData) 
		{
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Entry"));

			TTBOParametersDto[] ttboDtos = new TTBOParametersDto[fareData.Length];

			RBOPool rboPool = RBOPool.GetRBOPool();
			BusinessObject rbo = null;
			BusinessObjectOutput output;

            bool useZPBO = ZPBOPool.UseZPBO();

			try 
			{
				for (int i = 0; i < fareData.Length; i++)
				{
					bool outward = (i == 0);	// first entry is outward, next is return ...

					ttboDtos[i] = new TTBOParametersDto();

                    #region Restrict GC call

                    rbo = rboPool.GetInstance();
					PreTimetableRequest pttr = new PreTimetableRequest(rboPool.InterfaceVersion, REGCOutputLength, request, fareData[i], outward);
					output = rbo.Process(pttr);

					if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
					{
						ttboDtos[i].AddErrorMessage(UNSPECIFIED_BO_ERROR);
						continue;
					}

					TDDateTime inputDateTime = (outward ? request.OutwardDate : request.ReturnDate);

					ttboDtos[i].PopulateFromGCOutput(output.OutputBody, inputDateTime);
					rboPool.Release(ref rbo);

                    #endregion

                    #region Restrict GN call
                    /* 
                     * No longer need to do the GN call because GC and GL contain the required information
                     * 
                    rbo = rboPool.GetInstance();
					RouteIncludeExcludeRequest rier = new RouteIncludeExcludeRequest(rboPool.InterfaceVersion, REGNOutputLength, request, fareData[i]);
					output = rbo.Process(rier);

					if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
					{
						ttboDtos[i].AddErrorMessage(UNSPECIFIED_BO_ERROR);
						continue;
					}

					ttboDtos[i].PopulateFromGNOutput(output.OutputBody);
					rboPool.Release(ref rbo);
                    */
                    #endregion

                    #region Restrict GL call

                    // Temp fares array
                    FareDataDto fareDto = fareData[i];

                    // Ensure we can reconstruct the Fare object to pass to the BO call
                    if (!string.IsNullOrEmpty(fareDto.RawFareString))
                    {
                        rbo = rboPool.GetInstance();

                        // Build the fare to submit in to the GL call
                        Fare fare = new Fare(fareDto.RawFareString, fareDto.OriginNlc, fareDto.DestinationNlc,
                            fareDto.Origins, fareDto.Destinations, string.Empty, string.Empty, request.OutwardDate, useZPBO);
                        ArrayList fares = new ArrayList(1);
                        fares.Add(fare);

                        ValidateRouteList validateRouteList = new ValidateRouteList(rboPool.InterfaceVersion, REGLOutputLength, request,
                            fares, fareData[i].OriginNlc, fareData[i].DestinationNlc);
                        output = rbo.Process(validateRouteList);

                        if (output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
                        {
                            ttboDtos[i].AddErrorMessage(UNSPECIFIED_BO_ERROR);
                            continue;
                        }

                        ttboDtos[i].PopulateFromGLOutput(output.OutputBody);
                        rboPool.Release(ref rbo);
                    }

                    #endregion
                }
            }
            #region Handle exception and release BO's
            catch (TDException tde) 
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Warning, "An error was encountered while calculating fares::", tde ) );
				throw tde; 
			} 
			catch (Exception e ) 
			{
				TDException td = new TDException( "Unexpected error encountered while restricting fares", e, true, TDExceptionIdentifier.PRHUnexpectedError );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure,TDTraceLevel.Warning, td.InnerException.Message, e ) );
				throw td;
			}
			finally 
			{
				if (rbo != null) 
				{
					rboPool.Release(ref rbo);
				}
            }
            #endregion

            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Exit"));
			
			return ttboDtos;
		}

        /// <summary>
        /// Method to validate the services against the fares where the journeys were found after 
        /// the fare was selected
        /// </summary>
        /// <param name="requests"></param>
        /// <param name="fareData"></param>
        /// <returns></returns>
		public RailServiceValidationResultsDto ValidateServiceDetailsForFareAndJourney(PricingRequestDto[] requests, 
																							FareDataDto[] fareData)
		{
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Entry"));

			RailServiceValidationResultsDto vrDto = new RailServiceValidationResultsDto();

			RBOPool  rboPool  = RBOPool.GetRBOPool();
			SBOPool  sboPool  = SBOPool.GetSBOPool();

			BusinessObject sbo  = null;
			BusinessObject rbo  = null;
			
			BusinessObjectOutput output;
			Supplements supplements;

            bool useZPBO = ZPBOPool.UseZPBO();

			bool noTrainsFound = false;

			try 
			{
				foreach (PricingRequestDto request in requests)
				{
                    
					bool outward = (((TrainDto)request.Trains[0]).ReturnIndicator == ReturnIndicator.Outbound);
					
					int fareDataIndex = (outward ? 0 : 1);  

                    if (TDTraceSwitch.TraceVerbose) 
			        {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
                                string.Format("Validating fare for {0} Journey Index:\t{1}",
                                outward ? "Outward" : "Return",
                                request.JourneyIndex)));
                    }

					JourneyValidity validity = JourneyValidity.ValidFare;		// assume OK until proven otherwise ...

					// doing the RBO GD or RBO MR call is only meaningful if we have 
					//  restriction codes from the earlier GC call to recheck, 
					//  or if we need to check connecting TOC codes 
                    //  or RBO GL call indicated Cross london check, zonal indicator check,
                    //  or RBO GL call provided significant visit CRS locations to check
					
					if	(fareData[fareDataIndex].ConnectingTocCheckRequired
						 || (fareData[fareDataIndex].RestrictionCodesToReapply != null 
						        && fareData[fareDataIndex].RestrictionCodesToReapply.Length > 0
						        && !(fareData[fareDataIndex].RestrictionCodesToReapply.StartsWith("  ")))
                         || (fareData[fareDataIndex].CrossLondonToCheck)
                         || (fareData[fareDataIndex].ZonalIndicatorToCheck)
                         || (fareData[fareDataIndex].VisitCRSToCheck))
                    {
                        #region Restrict GD call

                        /* 
                         * No longer do the GN call because MR call provides additional validation, e.g. cross london checks
                        
                        rbo = rboPool.GetInstance();
						PostTimetableRequest pttr = new PostTimetableRequest(rboPool.InterfaceVersion, REGDOutputLength, request, fareData[fareDataIndex]);
						output = rbo.Process(pttr);
						rboPool.Release(ref rbo);
						rbo = null;

						RestrictFaresGD restrictionController = new RestrictFaresGD(pttr, output);

						if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
						{
							validity = JourneyValidity.InvalidFare;
							vrDto.AddErrorMessage(UNSPECIFIED_BO_ERROR);						
						}
						else
						{
							validity = restrictionController.Restrict(request.Trains);
                        }
                         */
                        
                        #endregion

                        #region Restrict MR call

                        rbo = rboPool.GetInstance();

                        // Build the fare to submit in to the MR call
                        Fare fare = new Fare(fareData[fareDataIndex].RawFareString, fareData[fareDataIndex].OriginNlc, fareData[fareDataIndex].DestinationNlc,
                            fareData[fareDataIndex].Origins, fareData[fareDataIndex].Destinations, string.Empty, string.Empty, request.OutwardDate, useZPBO);
                        ArrayList fares = new ArrayList(1);
                        fares.Add(fare);

                        // There should only be one journey in the request and one fare to validate it against.
                        // Therefore, check if the train contains the fare route code, because if it doesnt then 
                        // we don't want to validate using Routing Guide. This makes it consistent with the 
                        // search by time logic.
                        // This is done because in SBP, journeys are requested for a specified route code, which
                        // can find "valid" journeys but with a different route code - therefore when performing 
                        // the MR validation using Routing Guide, this would mark the service/fare invalid due to
                        // routing guide. 
                        // e.g. Nottingham to Bedford Rail Station provides an SOS 00700 route fare. but when
                        // services are requested, the service contains only 00452 route, yet the fare is valid
                        bool useRoutingGuide = UseRoutingGuideValidation(request, fare);
                        
                        // Takes the fare found and the train journey, and validate them
                        ValidateFaresForJourneyRequest validateFaresForJourney;
                        validateFaresForJourney = new ValidateFaresForJourneyRequest(
                            rboPool.InterfaceVersion, REMROutputLength, request, fares, fareData[fareDataIndex].OutputGL,
                            fare.FareOriginNlcActual, fare.FareDestinationNlcActual, useRoutingGuide);

                        output = rbo.Process(validateFaresForJourney);
                        rboPool.Release(ref rbo);
                        rbo = null;

                        RestrictFaresMR restrictionControllerMR = new RestrictFaresMR(validateFaresForJourney, output);

                        if (output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
                        {
                            validity = JourneyValidity.InvalidFare;
                            vrDto.AddErrorMessage(UNSPECIFIED_BO_ERROR);
                        }
                        else
                        {
                            validity = restrictionControllerMR.Restrict(request.Trains);
                        }

                        #endregion
                    }

					ArrayList displayableSupplements = new ArrayList();

					ArrayList rvboResults = new ArrayList(request.Trains.Count); 

					ArrayList errors = new ArrayList();

					if	(validity != JourneyValidity.InvalidFare)
                    {
                        #region Get supplements

                        sbo	= sboPool.GetInstance();

						SupplementRequest sr = new SupplementRequest(sboPool.InterfaceVersion, request, fareData[fareDataIndex], request.Trains, SUGDOutputLength);

						supplements = new Supplements(sbo.Process(sr));

						sboPool.Release(ref sbo);
						sbo = null;

						sbo	= sboPool.GetInstance();

                        #endregion

                        #region Reservations request

                        //create TicketTypeCodes array
						string[] ticketTypeCodes = new string[fareData.Length];	
						for(int i = 0;i<fareData.Length;i++)
						{
							ticketTypeCodes[i] = fareData[i].ShortTicketCode;
                        }

                        //Create Mandatory Reservations Request
						MandatoryReservationsRequest mandatoryReservationsRequest = new MandatoryReservationsRequest(sboPool.InterfaceVersion,ticketTypeCodes);

						//Find Mandatory Reservation data for ticket types
						MandatoryReservations mandatoryReservations =  new MandatoryReservations(sbo.Process(mandatoryReservationsRequest),mandatoryReservationsRequest.TicketTypeCodes);
						
						sboPool.Release(ref sbo);
						sbo = null;

						//Flag NRS Unavailable if mandatoryReservations has errors
						if (mandatoryReservations.HasErrors)
						{
							errors.Add(NRS_UNAVAILABLE);
						}
						else
						{
							//Call CheckAvailability - individual fare implemention
							bool placesAvailable = CheckAvailability(request, fareData[fareDataIndex], supplements.SupplementList, true, rvboResults, 
																		errors, outward, mandatoryReservations.Results, ref noTrainsFound);
						
							vrDto.AddRailAvailabilityResults(rvboResults);

							foreach (string errorId in errors)
							{
								vrDto.AddErrorMessage(errorId);
							}

							if	(!placesAvailable)  
							{														
								validity = JourneyValidity.NoPlacesAvailable;       
							}
                        }

                        #endregion

                        if	(validity == JourneyValidity.ValidFare				// if there is any leg of the service with no availability,
							|| validity == JourneyValidity.MinimumFareApplies)	//  the ticket is of no use and will not be displayed ...	
                        {
                            #region Add additional supplement information if needed

                            foreach (Supplement supplement in supplements.SupplementList)
							{
								if	((outward && supplement.ReturnDirection) 
									|| (!outward && !supplement.ReturnDirection))
								{
									continue;
								}

								if	(supplement.IsDisplayable)
								{
									SupplementAdditionalDataRequest sadr = new SupplementAdditionalDataRequest(sboPool.InterfaceVersion, supplement,
										request.OutwardDate, SUGAOutputLength);							
									
									sbo = sboPool.GetInstance();
									BusinessObjectOutput sadResponse = sbo.Process(sadr);
									sboPool.Release(ref sbo);
									sbo = null;

									supplement.AddAdditionalData(sadResponse.OutputBody);

									if	(supplement.SupplementClass != request.TicketClass 
										&& (supplement.SupplementType == SupplementType.NonAccomodation || supplement.SupplementType == SupplementType.SeatQuota))
									{
										displayableSupplements.Add(new SupplementDto(supplement.SupplementCode, supplement.Description, supplement.Cost));
									}
								}
                            }

                            #endregion
                        }
					}

					if	(outward)
					{
						vrDto.AddOutwardValidity(request.JourneyIndex, validity, ((SupplementDto[])displayableSupplements.ToArray(typeof(SupplementDto))));
					}
					else
					{
						vrDto.AddReturnValidity(request.JourneyIndex, validity, ((SupplementDto[])displayableSupplements.ToArray(typeof(SupplementDto))));
					}
				}

            }
            #region Handle exception and release BO's
            catch (TDException tde) 
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Warning, "An error was encountered while calculating fares::", tde ) );
				throw tde; 
			} 
			catch (Exception e ) 
			{
				TDException td = new TDException( "Unexpected error encountered while restricting fares", e, true, TDExceptionIdentifier.PRHUnexpectedError );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure,TDTraceLevel.Warning, td.InnerException.Message, e ) );
				throw td;
			}
			finally 
			{
				if (rbo != null) 
				{
					rboPool.Release(ref rbo);
				}

				if (sbo != null) 
				{
					sboPool.Release(ref sbo);
				}
            }
            #endregion

            vrDto.IncludesNoInventoryResults = noTrainsFound;

			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Exit"));

			return vrDto;
        }

        #endregion

        /// <summary>
		/// Adds a valid ticket to the array of valid tickets
		/// </summary>
		/// <param name="currentFareAvailability">The Fare details, either with or without a Discount</param>
		/// <param name="validTickets">ArrayList of valid tickets</param>
		/// <param name="sboPool">Pooled SBO interface</param>
		private void AddValidTicket(PricingRequestDto request, FareAvailability currentFareAvailability, ref ArrayList validTickets, SBOPool sboPool)
		{
			BusinessObject sbo  = null;

			try
			{
				TicketDto ticket = currentFareAvailability.Ticket;

				foreach (Supplement supplement in currentFareAvailability.Supplements.SupplementList)
				{
					if	((ticket.JourneyType == JourneyType.InwardSingle && !supplement.ReturnDirection)  
						|| (ticket.JourneyType == JourneyType.OutwardSingle && supplement.ReturnDirection))  
					{
						continue;
					}

					if	(supplement.IsDisplayable)
					{
						SupplementAdditionalDataRequest sadr = new SupplementAdditionalDataRequest(sboPool.InterfaceVersion, supplement,
							request.OutwardDate, SUGAOutputLength);							
								
						sbo = sboPool.GetInstance();
						BusinessObjectOutput sadResponse = sbo.Process(sadr);
						sboPool.Release(ref sbo);
						sbo = null;

						supplement.AddAdditionalData(sadResponse.OutputBody);

						ticket.AddSupplement(new SupplementDto(supplement.SupplementCode, supplement.Description, supplement.Cost));
					}
				}

				validTickets.Add(ticket);
			}
			catch (TDException tde) 
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Warning, "An error was encountered while calculating fares::", tde ) );
				throw tde; 
			} 
			catch (Exception e) 
			{
				TDException td = new TDException( "Unexpected error encountered while restricting fares", e, true, TDExceptionIdentifier.PRHUnexpectedError );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure,TDTraceLevel.Warning, td.InnerException.Message, e ) );
				throw td;
			}
			finally 
			{
				// Release the SBO if it is not done already
				if (sbo != null) 
				{
					sboPool.Release(ref sbo);
				}
			}
		}
	}
}