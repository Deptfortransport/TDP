// *********************************************** 
// NAME			: AvailabilityRequest.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Implementation of the AvailabilityRequest class
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/AvailabilityEstimator/AvailabilityRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:06   mturner
//Initial revision.
//
//   Rev 1.4   May 17 2006 15:01:54   rphilpott
//Add RouteCode to UnavailableProducts table, associated SP's and all classes that use them.
//Resolution for 4084: DD075: Unavailable products - ticket and route codes
//
//   Rev 1.3   May 05 2006 16:17:42   RPhilpott
//Use NLC codes instead of location descriptions for unavailable rail products.
//Resolution for 4080: DD075: Unavailable fare not changed to Low availability
//
//   Rev 1.2   Apr 28 2005 16:35:16   jbroome
//UnavailableProducts now stored by Outward and Return dates
//Resolution for 2302: PT - Product availability does not handle return products adequately.
//
//   Rev 1.1   Mar 18 2005 15:06:00   jbroome
//Added missing class documentation comments and minor updates after code review
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 09:52:34   jbroome
//Initial revision.

using System;

using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Class used in retreiving an availability estimate
	/// for a ticket. Holds parameters used in the availabilty
	/// request.
	/// </summary>
	public class AvailabilityRequest
	{
		
		#region Private members
		
		private TicketTravelMode mode;
		private string origin;
		private string destination;
		private string originGroup;
		private string destinationGroup;
		private string ticketCode;
		private string routeCode;
		private TDDateTime outwardTravelDate;
		private TDDateTime returnTravelDate;
		
		#endregion
        		
		#region Constructor

		/// <summary>
		/// Constructor method for AvailabilityRequest
		/// Used for single rail journeys
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="originGroup"></param>
		/// <param name="destinationGroup"></param>
		/// <param name="ticketCode"></param>
		/// <param name="outwardTravelDate"></param>
		public AvailabilityRequest(TicketTravelMode mode, string origin, 
									string destination, string originGroup, 
									string destinationGroup, string ticketCode, 
									string routeCode, TDDateTime outwardTravelDate)
		{
			this.mode = mode;
			this.origin = origin;
			this.destination = destination;
			this.originGroup = originGroup;
			this.destinationGroup = destinationGroup;
			this.ticketCode = ticketCode;
			this.routeCode = routeCode;
			this.outwardTravelDate = outwardTravelDate;
			this.returnTravelDate = null;
		}

		/// <summary>
		/// Constructor method for AvailabilityRequest
		/// Used for single coach journeys
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="ticketCode"></param>
		/// <param name="outwardTravelDate"></param>
		public AvailabilityRequest(TicketTravelMode mode, string origin, 
			string destination, string ticketCode, 
			TDDateTime outwardTravelDate)
		{
			this.mode = mode;
			this.origin = origin;
			this.destination = destination;
			this.originGroup = origin;
			this.destinationGroup = destination;
			this.ticketCode = ticketCode;
			this.routeCode = string.Empty;
			this.outwardTravelDate = outwardTravelDate;
			this.returnTravelDate = null;
		}

		/// <summary>
		/// Constructor method for AvailabilityRequest
		/// Used for return rail journeys
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="originGroup"></param>
		/// <param name="destinationGroup"></param>
		/// <param name="ticketCode"></param>
		/// <param name="outwardTravelDate"></param>
		/// <param name="returnTravelDate"></param>
		public AvailabilityRequest(TicketTravelMode mode, string origin, 
										string destination, string originGroup, 
										string destinationGroup, string ticketCode, string routeCode,
										TDDateTime outwardTravelDate, TDDateTime returnTravelDate)
		{
			this.mode = mode;
			this.origin = origin;
			this.destination = destination;
			this.originGroup = origin;
			this.destinationGroup = destination;
			this.ticketCode = ticketCode;
			this.routeCode = routeCode;
			this.outwardTravelDate = outwardTravelDate;
			this.returnTravelDate = returnTravelDate;
		}

		/// <summary>
		/// Constructor method for AvailabilityRequest
		/// Used for return coach journeys
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="ticketCode"></param>
		/// <param name="outwardTravelDate"></param>
		/// <param name="returnTravelDate"></param>
		public AvailabilityRequest(TicketTravelMode mode, string origin, 
			string destination, string ticketCode, 
			TDDateTime outwardTravelDate, TDDateTime returnTravelDate)
		{
			this.mode = mode;
			this.origin = origin;
			this.destination = destination;
			this.originGroup = origin;
			this.destinationGroup = destination;
			this.ticketCode = ticketCode;
			this.routeCode = string.Empty;
			this.outwardTravelDate = outwardTravelDate;
			this.returnTravelDate = returnTravelDate;
		}

		#endregion

		#region Public properties and methods
		
		/// <summary>
		/// Read only TicketTravelMode property 
		/// Mode of travel
		/// </summary>
		public TicketTravelMode Mode
		{
			get {return mode;}
		}
		
		/// <summary>
		/// Read only string property 
		/// Origin of route of travel
		/// </summary>
		public string Origin
		{
			get {return origin;}
		}
			
		/// <summary>
		/// Read only string property
		/// Destination of route of travel
		/// </summary>
		public string Destination
		{
			get {return destination;}
		}
		
		/// <summary>
		/// Read only string property 
		/// "Origin Group" of route of travel
		/// NLC code for rail -- same as Origin otherwise
		/// </summary>
		public string OriginGroup
		{
			get {return originGroup;}
		}
			
		/// <summary>
		/// Read only string property
		/// Destination of route of travel
		/// NLC code for coach -- same as Destination otherwise
		/// </summary>
		public string DestinationGroup
		{
			get {return destinationGroup;}
		}

		/// <summary>
		/// Read only string property
		/// Ticket code of ticket 
		/// </summary>
		public string TicketCode
		{
			get {return ticketCode;}
		}
		
		/// <summary>
		/// Read only string property
		/// Route code for rail ticket
		/// </summary>
		public string RouteCode
		{
			get {return routeCode;}
		}
		
		/// <summary>
		/// Read only TDDateTime property 
		/// Date of outward travel for availability request
		/// </summary>
		public TDDateTime OutwardTravelDate
		{
			get {return outwardTravelDate;}
		}

		/// <summary>
		/// Read only TDDateTime property 
		/// Date of return travel for availability request
		/// </summary>
		public TDDateTime ReturnTravelDate
		{
			get {return returnTravelDate;}
		}

		#endregion

	}
}
