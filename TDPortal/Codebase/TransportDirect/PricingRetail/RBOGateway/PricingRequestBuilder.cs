//********************************************************************************
//NAME         : PricingRequestBuilder.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 15/10/2003
//DESCRIPTION  : Implementation of PricingRequestBuilder class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/RBOGateway/PricingRequestBuilder.cs-arc  $
//
//   Rev 1.5   Sep 28 2012 14:28:44   RBroddle
//Amended AddJourneyLeg to cater for possibility of journey detail origin or destination having no Naptans 
//Resolution for 5851: RF 045 - Search by Price to Heathrow Central Bus Station Hangs on Wait Page then Displays Error
//
//   Rev 1.4   Jun 04 2010 11:03:16   mmodi
//Set Walk leg flag for TrainDto to allow RBO MR call to ignore when setting Routing guide flags for fare route codes
//Resolution for 5547: Fares - Balaston to Wedgewood rail replacement bus journey shows no fares
//
//   Rev 1.3   Jun 03 2010 09:26:22   mmodi
//Updated to allow underground legs to be added to the pricing request trains list, if required by the caller (only true when TTBO region provided the underground legs)
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.2   Apr 09 2010 10:31:14   mmodi
//Allow setting up a fare request for a RailReplacementBus with Walk leg scenario
//
//   Rev 1.1   Feb 18 2009 18:16:56   mmodi
//Populate the fare route codes to be used for fares call
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:37:12   mturner
//Initial revision.
//
//   Rev 1.19   Jan 18 2006 18:16:40   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.18   Nov 23 2005 15:53:08   RPhilpott
//Fix incorrect availability reporting of inward single and return fares by excluding irrelevant fares from all processing (returns) and from NRS queries (inward singles on outward legs). 
//Resolution for 3101: DN039 - NRS - Single Fares in Return Journeys
//
//   Rev 1.17   Nov 17 2005 17:40:24   RPhilpott
//Omit Metro from legs passed to RBO.
//Resolution for 3089: DN040: Manchester Metro on Find-A-Fare
//
//   Rev 1.16   Aug 25 2005 14:41:18   RPhilpott
//Pass Retail Train Id to RVBO in place of UID.
//Resolution for 2710: NRS interface -- retail train id needed
//
//   Rev 1.15   Aug 19 2005 14:05:58   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.14.1.1   Aug 16 2005 11:20:22   RPhilpott
//Get rid of warnings from deprecated methods.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.14.1.0   Jul 08 2005 10:56:18   rgreenwood
//DN062: Class references VehicleFeaturesToDtoConvertor class
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.14   Apr 05 2005 15:23:26   RPhilpott
//Use correct sleeper/seating values
//
//   Rev 1.13   Mar 22 2005 16:08:56   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.12   Mar 01 2005 18:43:16   RPhilpott
//Cost Search Back End for Del 7 - work in progress
//
//   Rev 1.11   Dec 16 2003 17:17:04   COwczarek
//Exclude a walking leg from the creation of a pricing request in the same way as for an underground leg
//Resolution for 376: Pricing of train-walk-train journeys
//
//   Rev 1.10   Oct 30 2003 11:16:24   acaunt
//Reference to TicketClass.FirstClass removed (it is not an option)
//
//   Rev 1.9   Oct 22 2003 11:59:52   acaunt
//Correcting mapping of railcard
//
//   Rev 1.8   Oct 20 2003 18:48:58   acaunt
//Corrected latest version
//
//   Rev 1.6   Oct 20 2003 10:19:26   acaunt
//latest version
//
//   Rev 1.5   Oct 17 2003 20:19:32   acaunt
//Initial Version
//
//   Rev 1.4   Oct 17 2003 12:22:10   acaunt
//Removed ambiguity from TicketClass
//
//   Rev 1.3   Oct 17 2003 12:09:14   acaunt
//No change.
//
//   Rev 1.2   Oct 16 2003 15:45:06   acaunt
//Bug fix
//
//   Rev 1.1   Oct 16 2003 11:51:36   acaunt
//Creation functionality added
//
//   Rev 1.0   Oct 15 2003 16:12:02   acaunt
//Initial Revision

using System;
using System.Collections;
using System.Data;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.RBOGateway
{
	/// <summary>
	/// Class to encapsulate the construction of a PricingRequest object
	/// </summary>
	public class PricingRequestBuilder
	{
		private const string NO_RAILCARD = "   ";
		private const int ADULTS = 1;
		private const int CHILDREN = 1;
		private PricingRequestDto pricingRequest;
		private ArrayList trains = new ArrayList();
		private StopDto previousStop;

		public PricingRequestBuilder()
		{	
		}

		/// <summary>
		/// Initialise a new pricing request
		/// </summary>
		/// <param name="discounts"></param>
		public void CreatePricingRequest(Discounts discounts)
		{
			// Create a PricingRequestDto and set the discounts related fields
			pricingRequest = new PricingRequestDto();
			
			switch (discounts.TicketClass) 
			{
				case TransportDirect.UserPortal.PricingRetail.Domain.TicketClass.Second:
					pricingRequest.TicketClass = 2;
					break;
				default:
					pricingRequest.TicketClass = 9;
					break;
			}
			
			if (discounts.RailDiscount == null || discounts.RailDiscount.Length == 0) 
			{
				pricingRequest.Railcard = NO_RAILCARD;
			} 
			else
			{
				pricingRequest.Railcard = discounts.RailDiscount;
			}
			
			// Explicity set these to null for confidence
			pricingRequest.OutwardDate = null;
			pricingRequest.ReturnDate = null;
			
			// Standard values for number of adults and children. We want one of each.
			pricingRequest.NumberOfAdults = ADULTS;
			pricingRequest.NumberOfChildren = CHILDREN;
		}


		/// <summary>
		/// Initialise a new pricing request
		/// </summary>
		/// <param name="discounts"></param>
		public void CreatePricingRequest(string railcardCode, TDDateTime outwardDate, TDDateTime returnDate)
		{
			// Create a PricingRequestDto and set the discounts related fields
			pricingRequest = new PricingRequestDto();
			
			pricingRequest.TicketClass = 9;		// irrelevant to service validation 
			
			if (railcardCode == null || railcardCode.Length == 0) 
			{
				pricingRequest.Railcard = NO_RAILCARD;
			} 
			else
			{
				pricingRequest.Railcard = railcardCode;
			}
			
			// Explicity set these to null for confidence
			pricingRequest.OutwardDate = outwardDate;
			pricingRequest.ReturnDate = returnDate;
			
			// Standard values for number of adults and children. We want one of each.
			pricingRequest.NumberOfAdults = ADULTS;
			pricingRequest.NumberOfChildren = CHILDREN;
		}



		/// <summary>
		/// Create a new journey leg for the Request based on a JourneyDetail and add it to the 
		/// collection of legs associated with the request
		/// </summary>
		/// <param name="journeyDetail"></param>
		/// <param name="indicator"></param>
        /// <param name="unitIncludesRailReplacementWalk">Flag indicating its ok to fare from the Walk leg 
        /// when this leg connects to a RailReplacementBus leg. Set only to True when Walk+RRB are from the TTBO</param>
        public void AddJourneyLeg(PublicJourneyDetail journeyDetail, ReturnIndicator indicator, 
            bool unitIncludesRailReplacementWalk)
		{
            AddJourneyLeg(journeyDetail, indicator, unitIncludesRailReplacementWalk, false);
		}

        /// <summary>
        /// Create a new journey leg for the Request based on a JourneyDetail and add it to the 
        /// collection of legs associated with the request
        /// </summary>
        /// <param name="journeyDetail"></param>
        /// <param name="indicator"></param>
        /// <param name="unitIncludesUnderground">Flag indicating its ok to fare from the Walk leg 
        /// when this leg connects to a RailReplacementBus leg</param>
        /// <param name="includeUngerground">Flag indicating its ok to fare from the Underground leg when
        /// this leg connects to a Train leg. Set only to True when Underground+Rail are from the TTBO</param>
        public void AddJourneyLeg(PublicJourneyDetail journeyDetail, ReturnIndicator indicator,
            bool unitIncludesRailReplacementWalk, bool unitIncludesUnderground)
        {
                // If this leg is an underground or walking or metro leg we simply set the previous 
                // stop to null so that a new stop is created next time around
                // This way we will get two distinct stops seperated by the underground journey.
                // However, if building the request for a RailReplacementBus/Walk journey, then we want to keep the 
                // walk details as this contains the actual station the fare should be requested for.
                if (journeyDetail.Mode == ModeType.Metro)
                {
                    previousStop = null;
                }
                else if (journeyDetail.Mode == ModeType.Underground && !unitIncludesUnderground)
                {
                    previousStop = null;
                }
                else if (journeyDetail.Mode == ModeType.Walk && !unitIncludesRailReplacementWalk)
                {
                    previousStop = null;
                }
                // otherwise we create a TrainDto to represent the new leg and add it to the collection of legs
                // if the previous stop is null a new stop is created, otherwise the leg begins at the previous stop
                // The various calls pick the appropriate information from the JourneyDetail
                else
                {
                    // Create a trainDto for the leg of the journey
                    StopDto board = CreateOrUpdateBoardStop(previousStop, journeyDetail);
                    StopDto alight = CreateAlightStop(journeyDetail);
                    StopDto origin = null;
                    StopDto destination = null;
                    bool isWalk = false;

                    if ((journeyDetail.Mode == ModeType.Walk && unitIncludesRailReplacementWalk)
                        ||
                        (journeyDetail.Mode == ModeType.Underground) && unitIncludesUnderground)
                    {
                        // Walk/Underground leg doesnt contain a Service Origin or Destination, so set to be the 
                        // same as the board and alight
                        origin = board;
                        destination = alight;
                        isWalk = true;
                    }
                    else
                    {
                        //IR 5851 - the above does not catch all scenarios for rail replacement buses
                        //Set to be same as the board and alight if we have no Naptan as we cannot ctrate 
                        //the Dto's without one.
                        if ( (journeyDetail.Origin.Location.NaPTANs[0].Naptan != "") && (journeyDetail.Destination.Location.NaPTANs[0].Naptan != "") )
                        {
                            origin = CreateOriginStop(journeyDetail);
                            destination = CreateDestinationStop(journeyDetail);
                        }
                        else
                        {
                            origin = board;
                            destination = alight;
                        }
                    } 

                    VehicleFeatureToDtoConvertor vehicleFeaturesConverter = new VehicleFeatureToDtoConvertor(journeyDetail.GetVehicleFeatures());

                    TocDto[] tocs = CreateTocs(journeyDetail);
                    LocationDto[] intermediates = CreateIntermediateStops(journeyDetail);
                    string uid = journeyDetail.Services[0].PrivateId;
                    string retailId = journeyDetail.Services[0].RetailTrainId;
                    string[] fareRouteCodes = journeyDetail.FareRouteCodes;

                    TrainDto train = new TrainDto(uid, retailId, tocs, vehicleFeaturesConverter.SeatingClass, vehicleFeaturesConverter.SleeperClass, vehicleFeaturesConverter.Reservations, vehicleFeaturesConverter.Catering, indicator, origin, board, alight, destination, intermediates, fareRouteCodes);

                    // Flag it as a walk train (this will allow pricing request to use correctly in fare/service validation)
                    train.IsForWalk = isWalk;

                    // Add it to the collection of legs.
                    trains.Add(train);

                    // Additionally, set outward or return dates of the PricingRequestDto if they are not already present
                    // (i.e. they are set to the first appropriate JourneyDetail we find)
                    if (pricingRequest.OutwardDate == null && indicator == ReturnIndicator.Outbound)
                    {
                        pricingRequest.OutwardDate = journeyDetail.LegStart.DepartureDateTime;
                    }
                    else if (pricingRequest.ReturnDate == null && indicator == ReturnIndicator.Return)
                    {
                        pricingRequest.ReturnDate = journeyDetail.LegStart.DepartureDateTime;
                    }

                    // Finally, update the previousStop
                    previousStop = alight;

                }
        }

		/// <summary>
		/// Add a journey index for the public journey 
		/// associated with the request, where relevant
		/// </summary>
		/// <param name="journeyIndex"></param>
		public void AddJourneyIndex(int journeyIndex)
		{
			pricingRequest.JourneyIndex = journeyIndex;
		}

		/// <summary>
		/// Set journey type on the request to InwardSingle if this 
		/// if the request will contain both outward and inward trains.  
		/// </summary>
		/// <param name="isMatchingReturn">isMatchingReturn from PricingUnit</param>
		public void SetJourneyType(bool isMatchingReturn)
		{
			pricingRequest.JourneyType = (isMatchingReturn ? JourneyType.InwardSingle : JourneyType.Return);
		}


		public PricingRequestDto GetPricingRequest()
		{
			// Associate the train legs with the request and return it.
			pricingRequest.Trains = trains;
			return pricingRequest;
		}

		/// <summary>
		/// Create a departure stop (if one isn't passed in), or update a stop with departure information
		/// </summary>
		/// <param name="stop"></param>
		private StopDto CreateOrUpdateBoardStop(StopDto existingStop, PublicJourneyDetail journeyDetail)
		{
			StopDto departureStop;
			if (existingStop == null) 
			{
				departureStop = new StopDto(LocationConvertor.CreateLocationDto(journeyDetail.LegStart.Location));
			} 
			else 
			{
				departureStop = existingStop;
			}
			departureStop.Departure = journeyDetail.LegStart.DepartureDateTime;
			return departureStop;
		}

		/// <summary>
		/// Create an appropriately populated arrival stop for a journey leg
		/// </summary>
		/// <param name="stop"></param>
		private StopDto CreateAlightStop(PublicJourneyDetail journeyDetail)
		{
			StopDto arrivalStop = new StopDto(LocationConvertor.CreateLocationDto(journeyDetail.LegEnd.Location));
			arrivalStop.Arrival = journeyDetail.LegEnd.ArrivalDateTime;
			return arrivalStop;
		}

		/// <summary>
		/// Create an appropriately populated origin stop for a journey leg
		/// </summary>
		/// <param name="journeyDetail"></param>
		/// <returns></returns>
		private StopDto CreateOriginStop(PublicJourneyDetail journeyDetail)
		{
			StopDto originStop = new StopDto(LocationConvertor.CreateLocationDto(journeyDetail.Origin.Location));
			originStop.Departure = journeyDetail.Origin.DepartureDateTime; 
			return originStop;
		}
		
		/// <summary>
		/// Create an appropriately populated destination stop for a journey leg
		/// </summary>
		/// <param name="journeyDetail"></param>
		/// <returns></returns>
		private StopDto CreateDestinationStop(PublicJourneyDetail journeyDetail)
		{
			StopDto destinationStop = new StopDto(LocationConvertor.CreateLocationDto(journeyDetail.Destination.Location));
			destinationStop.Arrival = journeyDetail.Destination.ArrivalDateTime;
			return destinationStop;
		}

		/// <summary>
		/// Build up a list of intermediate locations for a journey leg
		/// </summary>
		/// <param name="journeyDetail"></param>
		/// <returns></returns>
		private LocationDto[] CreateIntermediateStops(PublicJourneyDetail journeyDetail)
		{
			ArrayList locations = new ArrayList();

			foreach (PublicJourneyCallingPoint pjcp in journeyDetail.GetIntermediatesLeg()) 
			{
				locations.Add(LocationConvertor.CreateLocationDto(pjcp.Location));
			}

			return (LocationDto[]) locations.ToArray(typeof(LocationDto));
		}


		/// <summary>
		/// Build up a list of Tocs associated with the journey leg
		/// </summary>
		/// <param name="journeyDetail"></param>
		/// <returns></returns>
		private TocDto[] CreateTocs(PublicJourneyDetail journeyDetail)
		{
			ArrayList tocs = new ArrayList();
			
			foreach(ServiceDetails detail in journeyDetail.Services)
			{
                // Now that we allow TTBO underground legs, don't add the operator code if it
                // is greater than 2 chars, as the fare business objects expect 2 char op code only
                if (!(
                      (journeyDetail.Mode == ModeType.Underground) && (detail.OperatorCode != null) && (detail.OperatorCode.Length > 2)
                      ))
                {
                    tocs.Add(new TocDto(detail.OperatorCode));
                }
			}
			return (TocDto[]) tocs.ToArray(typeof(TocDto));
		}

	}
}
