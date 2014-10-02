// *********************************************** 
// NAME			: AvailabilityResult.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Implementation of the AvailabilityResult class
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/AvailabilityEstimator/AvailabilityResult.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:08   mturner
//Initial revision.
//
//   Rev 1.4   May 17 2006 15:01:54   rphilpott
//Add RouteCode to UnavailableProducts table, associated SP's and all classes that use them.
//Resolution for 4084: DD075: Unavailable products - ticket and route codes
//
//   Rev 1.3   Apr 23 2005 11:15:04   jbroome
//Changes to the class definition
//
//   Rev 1.2   Mar 18 2005 15:06:00   jbroome
//Added missing class documentation comments and minor updates after code review
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.1   Feb 17 2005 14:39:54   jbroome
//Modified class for use with AvailabilityResultService[]
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 09:52:34   jbroome
//Initial revision.

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{

	/// <summary>
	/// Class used in updating estimates for ticket availability.
	/// Holds details of real external availability requests made
	/// that can be used to refine internal estimates.
	/// </summary>
	public class AvailabilityResult
	{
		
		#region Private members

		private TicketTravelMode mode;
		private string originGroup;
		private string destinationGroup;
		private string ticketCode;
		private string routeCode;
		private ArrayList journeyServices;
		private TDDateTime outwardDate;
		private TDDateTime returnDate;
		private bool available;
	
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor method for AvailabilityResult - to be used for rail fares
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="ticketCode"></param>
		/// <param name="travelDate"></param>
		/// <param name="available"></param>
		public AvailabilityResult(TicketTravelMode mode, string originGroup,
									string destinationGroup, string ticketCode, string routeCode,
									TDDateTime outwardDate, TDDateTime returnDate, bool available)
		{
			this.mode = mode;
			this.originGroup = originGroup;	
			this.destinationGroup = destinationGroup;
			this.ticketCode = ticketCode;
			this.routeCode = routeCode;
			this.journeyServices = new ArrayList();
			this.outwardDate = outwardDate;
			this.returnDate = returnDate;
			this.available = available;
		}

		/// <summary>
		/// Constructor method for AvailabilityResult - to be used for coach fares
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="ticketCode"></param>
		/// <param name="travelDate"></param>
		/// <param name="available"></param>
		public AvailabilityResult(TicketTravelMode mode, string originGroup,
			string destinationGroup, string ticketCode, 
			TDDateTime outwardDate, TDDateTime returnDate, bool available)
		{
			this.mode = mode;
			this.originGroup = originGroup;	
			this.destinationGroup = destinationGroup;
			this.ticketCode = ticketCode;
			this.routeCode = string.Empty;
			this.journeyServices = new ArrayList();
			this.outwardDate = outwardDate;
			this.returnDate = returnDate;
			this.available = available;
		}

		#endregion

		#region Public properties and methods

		/// <summary>
		/// Returns AvailabilityResultService array from the private ArrayList
		/// journeyServices. If index not valid, null object returned.
		/// </summary>
		/// <param name="journeyIndex">int index of journeyServices ArrayList</param>
		/// <returns>AvailabilityResultService array</returns>
		public AvailabilityResultService[] GetServicesForJourney(int journeyIndex)
		{
			if ((journeyIndex < JourneyCount)&&(JourneyCount!=0))
			{
				return (AvailabilityResultService[])this.journeyServices[journeyIndex];
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Adds an array of AvailabilityResultService to the private ArrayList
		/// field journeyServices. 
		/// Array only added if not empty and contents are valid.
		/// </summary>
		/// <param name="services">AvailabilityResultService array</param>
		public void AddJourneyServices(AvailabilityResultService[] services)
		{
			if ((services != null) && (services.Length > 0))
			{
				this.journeyServices.Add(services);
			}
		}


		/// <summary>
		/// Ready only int property JourneyCount
		/// Corresponds to the number of AvailabilityResultService 
		/// arrays within the AvailabilityResult
		/// </summary>
		public int JourneyCount
		{
			get { return this.journeyServices.Count; }
		}

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
		public string OriginGroup
		{
			get {return originGroup;}
		}

		/// <summary>
		/// Read only string property 
		/// Destination of route of travel
		/// </summary>
		public string DestinationGroup
		{
			get {return destinationGroup;}
		}

		/// <summary>
		/// Read only string property 
		/// Ticket code for ticket
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
		/// Date of travel for outward journey
		/// </summary>
		public TDDateTime OutwardDate
		{
			get {return outwardDate;}
		}

		/// <summary>
		/// Read only TDDateTime property
		/// Date of travel for return journey
		/// </summary>
		public TDDateTime ReturnDate
		{
			get {return returnDate;}
		}
		
		/// <summary>
		/// Read only boolean property
		/// Is ticket available on dates specified?
		/// </summary>
		public bool Available
		{
			get {return available;}
		}


		#endregion
	
	}
}
