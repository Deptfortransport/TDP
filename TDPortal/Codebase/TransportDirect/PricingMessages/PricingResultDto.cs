//********************************************************************************
//NAME         : PricingResultDto.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Data Transfer Object for the Pricing Result
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/PricingResultDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:52   mturner
//Initial revision.
//
//   Rev 1.9   Mar 06 2007 13:43:44   build
//Automatically merged from branch for stream4358
//
//   Rev 1.8.1.0   Mar 02 2007 10:59:08   asinclair
//Added new NoThroughFaresAvailable bool
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.8   May 03 2005 12:00:40   RPhilpott
//Fix typo in ctor.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.7   Apr 28 2005 18:05:30   RPhilpott
//Split noPlacesAvaialble flag into singles and returns.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.6   Apr 28 2005 15:50:58   RPhilpott
//Add "NoPlacesAvailable" property to indicate that a time-based fare request has found valid fares but no places are available.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.5   Apr 13 2005 13:59:06   RPhilpott
//Returning of NRS errors.
//Resolution for 2072: PT: NRS error messages.
//
//   Rev 1.4   Mar 22 2005 16:08:36   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.3   Oct 15 2003 20:02:58   CHosegood
//Changed all occurences of DateTime to TDDateTime
//
//   Rev 1.2   Oct 13 2003 13:22:22   CHosegood
//currency now defaults to string.Empty
//
//   Rev 1.1   Oct 08 2003 11:41:02   CHosegood
//Changed access modifiers
//
//   Rev 1.0   Oct 08 2003 11:34:22   CHosegood
//Initial Revision

using System;
using System.Collections;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingMessages
{
    /// <summary>
    /// Data Transfer Object for the Pricing Result.
    /// </summary>
	[CLSCompliant(false)]
	[Serializable]
    public class PricingResultDto
    {
		private TDDateTime outwardDate;
		private TDDateTime returnDate;
		private int minChildAge;
        private int maxChildAge;
        private string currency = String.Empty;
        private ArrayList tickets;
		private JourneyType journeyType;  

		private ArrayList errorResourceIds = new ArrayList();

		private RailAvailabilityResultDto[] rvboResults = new RailAvailabilityResultDto[0];

		private bool noPlacesAvailableForSingles = false;
		private bool noPlacesAvailableForReturns = false;
		private bool noThroughFaresAvailable = false;

		/// <summary>
		/// The date of the outward (or single) journey
		/// </summary>
		public TDDateTime OutwardDate 
		{
			get { return outwardDate; }
			set { outwardDate = value; }
		}

		/// <summary>
		/// The date of the return journey (if any)
		/// </summary>
		public TDDateTime ReturnDate 
		{
			get { return returnDate; }
			set { returnDate = value; }
		}

		/// <summary>
		/// The minimum age for a child before child fares are charged
		/// </summary>
		public int MinChildAge 
		{
			get { return this.minChildAge; }
			set { this.minChildAge = value; }
		}

		/// <summary>
        /// The maximum age for a child after which adult fares are charged
        /// </summary>
        public int MaxChildAge 
        {
            get { return this.maxChildAge; }
            set { this.maxChildAge = value; }
        }

		/// <summary>
		/// The currency code of any monetary fields returned.
		/// </summary>
		public string Currency 
		{
			get { return this.currency; }
			set { this.currency = value; }
		}

		/// <summary>
		/// Return (which implies outward singles too) or InwardSingle
		/// </summary>
		public JourneyType JourneyType 
		{
			get { return this.journeyType; }
			set { this.journeyType = value; }
		}

		/// <summary>
		/// The collection of valid tickets (represented here as TicketDto objects) 
		/// </summary>
		public ArrayList Tickets 
		{
			get { return this.tickets; }
			set { this.tickets = value; }
		}

		/// <summary>
		/// A collection of results of RVBO calls  
		/// applicable to this journey and fare, 
		/// if any. Will only be populated for 
		/// time-based search results (otherwise 
		/// returns an empty array).
		/// </summary>
		public RailAvailabilityResultDto[] RVBOResults 
		{
			get { return rvboResults; } 
			set { rvboResults = value; }
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
		/// Set true if one or more valid single fares 
		/// were found for the request, but there were  
		/// no seats available on the specified service(s).  
		/// </summary>
		public bool NoPlacesAvailableForSingles
		{
			get { return noPlacesAvailableForSingles; }
		}

		/// <summary>
		/// Set true if one or more valid return fares 
		/// were found for the request, but there were  
		/// no seats available on the specified service(s).  
		/// </summary>
		public bool NoPlacesAvailableForReturns
		{
			get { return noPlacesAvailableForReturns; }
		}

		/// <summary>
		/// Set true if the RBO returns no fares as the combination 
		/// of route and operators planned are not fareable.
		/// </summary>
		public bool NoThroughFaresAvailable
		{
			get { return noThroughFaresAvailable; }
		}

		/// <summary>
        /// Constructor setting all values
        /// </summary>
		/// <param name="outwardDate">Outward travel date</param>
		/// <param name="returnDate">Return travel date (null if single only)</param>
		/// <param name="minChildAge">Minimum child age</param>
		/// <param name="maxChildAge">Maximum child age</param>
        /// <param name="currency">Currency used by fares in Tickets</param>
		/// <param name="tickets">ArrayList of Tickets or CostSearchTickets</param>
		/// <param name="journeyType">Return (which implies outward singles too) or InwardSingle</param>
		/// 
		public PricingResultDto(TDDateTime outwardDate, TDDateTime returnDate, int minChildAge, int maxChildAge,
            string currency, ArrayList tickets, JourneyType journeyType, bool noPlacesAvailableForSingles, bool noPlacesAvailableForReturns, bool noThroughFaresAvailable)
        {
			this.outwardDate = outwardDate;
			this.returnDate = returnDate;
			this.minChildAge = minChildAge;
            this.maxChildAge = maxChildAge;
            this.currency = currency;
            this.tickets = tickets;
			this.journeyType = journeyType;
			this.noPlacesAvailableForSingles = noPlacesAvailableForSingles;
			this.noPlacesAvailableForReturns = noPlacesAvailableForReturns;
			this.noThroughFaresAvailable = noThroughFaresAvailable;
		}
    }
}
