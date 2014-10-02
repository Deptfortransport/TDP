//********************************************************************************
//NAME         : RailAvailabilityResultDto.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-02-24
//DESCRIPTION  : Data Transfer Object to pass back results of an 
//				  NRS/RVBO lookup for an individual train service
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/RailAvailabilityResultDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:52   mturner
//Initial revision.
//
//   Rev 1.2   Nov 23 2005 15:50:14   RPhilpott
//Add retailId for logging.
//Resolution for 3038: DN040: Double reporting/logging of NRS requests
//
//   Rev 1.1   Mar 22 2005 16:08:36   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.0   Mar 01 2005 18:47:42   RPhilpott
//Initial revision.
//

using System;
using System.Text;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Data Transfer Object to pass back results of an 
	/// NRS/RVBO lookup for an individual train service.
	/// </summary>
	[Serializable()]
	public class RailAvailabilityResultDto
	{
		private LocationDto origin;
		private LocationDto destination;
		private TDDateTime departureTime;
		private string trainUid;
		private string retailId;
		private int placesAvailable;
		private int overbookAvailable;

		private string ticketType;
		private string railcardCode;
		private string routeCode;
		private string supplement;

		public RailAvailabilityResultDto(LocationDto origin, LocationDto destination, string ticketType, 
											string railCardCode, string routeCode, string supplement,
											TDDateTime departureTime, string uid, string retailId, int places, int overbook)
		{
			this.origin = origin;
			this.destination = destination;
			this.departureTime = departureTime;
			this.trainUid = uid;
			this.retailId = retailId;
			this.placesAvailable = places;
			this.overbookAvailable = overbook;
			
			this.ticketType = ticketType.PadRight(3, ' ');
			this.railcardCode = railCardCode.PadRight(3, ' ');
			this.routeCode = routeCode.PadRight(5, ' ');
			this.supplement = supplement.PadRight(3, ' ');
		}


		public RailAvailabilityResultDto(string product, int placesAvailable, int overbookAvailable, LocationDto origin, LocationDto destination,
											TDDateTime departureTime, string uid, string retailId)
		{
			this.origin = origin;
			this.destination = destination;
			this.departureTime = departureTime;
			this.trainUid = uid;
			this.retailId = retailId;

			this.placesAvailable = placesAvailable;
			this.overbookAvailable = overbookAvailable;

			this.ticketType = product.Substring(0, 3);
			this.railcardCode = product.Substring(3, 3);
			this.routeCode = product.Substring(6, 5);
			this.supplement = product.Substring(11, 3);


		}

		public string Product
		{
			get { return ticketType + railcardCode + routeCode + supplement; }
		}
			
		public string TicketType
		{
			get { return ticketType; }
		}
			
		public string RailcardCode
		{
			get { return railcardCode; }
		}

		public string RouteCode
		{
			get { return routeCode; }
		}
			
		public string Supplement
		{
			get { return supplement; }
		}

		public LocationDto Origin
		{
			get { return origin; }
		}

		public LocationDto Destination
		{
			get { return destination; }
		}

		public TDDateTime DepartureTime
		{
			get { return departureTime; }
		}

		public string TrainUid
		{
			get { return trainUid; }
		}
		
		public string RetailId
		{
			get { return retailId; }
		}

		public int PlacesAvailable
		{
			get { return placesAvailable; }
		}

		public int OverbookAvailable
		{
			get { return overbookAvailable; }
		}

		public override string ToString()
		{
			StringBuilder sb = new StringBuilder();			
			
			sb.Append(origin.Crs);
			sb.Append(' ');
			sb.Append(destination.Crs);
			sb.Append(' ');
			sb.Append(departureTime.ToString("yyyyMMdd HHmm"));
			sb.Append(' ');
			sb.Append(trainUid);
			sb.Append(' ');
			sb.Append(retailId);
			sb.Append(' ');
			sb.Append(Product);
			sb.Append(' ');
			sb.Append(placesAvailable.ToString());
			sb.Append(' ');
			sb.Append(overbookAvailable.ToString());

			return sb.ToString();
		}
	}
}
