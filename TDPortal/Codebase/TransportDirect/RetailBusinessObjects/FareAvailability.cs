//********************************************************************************
//NAME         : FareAvailability.cs
//AUTHOR       : Richard Hopkins
//DATE CREATED : 13/10/2005
//DESCRIPTION  : Class that is used to record the availability of each Fare,
//				 either with or without a Discount
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/FareAvailability.cs-arc  $
//
//   Rev 1.3   Oct 04 2012 13:35:08   mmodi
//Added logging for fare availability checks
//Resolution for 5856: Fares - Sleeper fare ticket shown when it is not available
//
//   Rev 1.2   Jun 03 2010 09:30:08   mmodi
//Updated following change to Ticket constructor
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.1   Feb 13 2009 17:14:02   rbroddle
//Amended to use changed ticketDto constructor
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.0   Nov 08 2007 12:46:04   mturner
//Initial revision.
//
//   Rev 1.4   Apr 26 2006 12:15:02   RPhilpott
//Manual merge of stream 35
//
//   Rev 1.3.1.0   Apr 05 2006 17:12:08   RPhilpott
//Obtain names of fare locations from LBO.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.3   Dec 05 2005 18:26:52   RPhilpott
//Changes to ensure that RE GD call is made if connecting TOC's need to be checked post-timetable call.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.2   Nov 24 2005 18:22:50   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.1   Nov 23 2005 15:53:10   RPhilpott
//Fix incorrect availability reporting of inward single and return fares by excluding irrelevant fares from all processing (returns) and from NRS queries (inward singles on outward legs). 
//Resolution for 3101: DN039 - NRS - Single Fares in Return Journeys
//
//   Rev 1.0   Nov 02 2005 16:58:22   rhopkins
//Initial revision.
//

using System;

using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.Common.Logging;
using System.Diagnostics;


namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Availability of a specific Fare, either with or without a Discount
	/// </summary>
	[CLSCompliant(false)]
	public class FareAvailability
	{
		string fareKey;
		bool isWithDiscount;

		private static readonly int SUGDOutputLength = 346;

		BusinessObject sbo  = null;

		FareDataDto fareDto;
		Supplements supplements;
		TicketDto ticket;

		bool outwardBerthsAvailable = true;
		bool outwardSeatsAvailable = true;

		bool inwardBerthsAvailable = true;
		bool inwardSeatsAvailable = true;

		public FareAvailability(SBOPool sboPool, PricingRequestDto request, Fare fare)
		{
			isWithDiscount = (fare.RailcardCode.Trim().Length > 0);

			ticket = new TicketDto(fare.TicketType.Code, fare.Route.Code, fare.AdultFare, fare.ChildFare, fare.AdultFareMinimum, 
									fare.ChildFareMinimum, fare.TicketType.TicketClass, fare.RailcardCode, 
									fare.QuotaControlled, fare.TicketType.Type, fare.RestrictionCode, 
									fare.FareOriginNlc, fare.FareDestinationNlc, fare.Origins, fare.Destinations,
                                    fare.OriginName, fare.DestinationName, fare.FareOriginNlcActual , fare.FareDestinationNlcActual, fare.RawFareString);

			fareDto = new FareDataDto(fare.TicketType.Code, fare.Route.Code, fare.RestrictionCode,
				fare.FareOriginNlc, fare.FareDestinationNlc, fare.RailcardCode, (fare.TicketType.Type == JourneyType.Return), string.Empty,
				fare.Origins, fare.Destinations, false, fare.RawFareString, false, false, false, string.Empty);

			this.fareKey = fareDto.ShortTicketCode.PadRight(3, ' ') + fareDto.RouteCode.PadRight(5, ' ');  // TicketType + RouteCode

            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("Updating supplement data for fare [{0}]", fareKey)));
            }

			SupplementRequest sr = new SupplementRequest(sboPool.InterfaceVersion, request, fareDto, request.Trains, SUGDOutputLength);

			sbo = sboPool.GetInstance();
			supplements = new Supplements(sbo.Process(sr));
			sboPool.Release(ref sbo);
			sbo = null;
		}


		/// <summary>
		/// Gets the key that identifies the Fare
		/// </summary>
		public string FareKey
		{
			get { return fareKey; }
		}

		/// <summary>
		/// True if this records the availability of the fare using the Discount
		/// </summary>
		public bool IsWithDiscount
		{
			get { return isWithDiscount; }
		}

		/// <summary>
		/// Gets data transfer object for Fare data
		/// </summary>
		public FareDataDto FareDto
		{
			get { return fareDto; }
		}

		/// <summary>
		/// Gets the Supplements for this Fare
		/// </summary>
		public Supplements Supplements
		{
			get { return supplements; }
		}

		/// <summary>
		/// Gets the data transfer object for the Ticket for this Fare
		/// </summary>
		public TicketDto Ticket
		{
			get { return ticket; }
		}

		/// <summary>
		/// Get/Set whether Outward seats are available for this Fare
		/// </summary>
		public bool OutwardSeatsAvailable
		{
			get { return outwardSeatsAvailable; }
			set { outwardSeatsAvailable = value; }
		}

		/// <summary>
		/// Get/Set whether Outward berths are available for this Fare
		/// </summary>
		public bool OutwardBerthsAvailable
		{
			get { return outwardBerthsAvailable; }
			set { outwardBerthsAvailable = value; }
		}

		/// <summary>
		/// Get/Set whether Inward seats are available for this Fare
		/// </summary>
		public bool InwardSeatsAvailable
		{
			get { return inwardSeatsAvailable; }
			set { inwardSeatsAvailable = value; }
		}

		/// <summary>
		/// Get/Set whether Inward berths are available for this Fare
		/// </summary>
		public bool InwardBerthsAvailable
		{
			get { return inwardBerthsAvailable; }
			set { inwardBerthsAvailable = value; }
		}
	}
}
