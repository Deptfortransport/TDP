// ************************************************************** 
// NAME			: CostSearchTicket.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 11/01/2005 
// DESCRIPTION	: Implementation of the CostSearchTicket class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/CostSearchTicket.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:44   mturner
//Initial revision.
//
//   Rev 1.13   Jan 18 2006 18:16:32   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.12   Jan 17 2006 17:56:56   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.11   Nov 30 2005 09:36:38   RPhilpott
//Enhance clone method.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.10   Nov 09 2005 12:31:40   build
//Automatically merged from branch for stream2818
//
//   Rev 1.9.1.2   Nov 03 2005 18:23:44   RPhilpott
//Move CoachFareData from CostSearchTicket to base class.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.9.1.1   Oct 28 2005 10:24:48   RWilby
//Added LegNumber property
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.9.1.0   Oct 26 2005 15:31:20   mguney
//UndiscountedCoachFareData and DiscountedCoachFareData properties included.
//Resolution for 2818: DEL 8 Stream: Search by Price
//

using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for CostSearchTicket.
	/// </summary>
	[Serializable]
	public class CostSearchTicket : Ticket
	{
		private int index;
		private int legNumber;
		private Probability probability;

		private float minAdultFare = float.NaN;
		private float minChildFare = float.NaN;

		private CostBasedJourney journeysForTicket;		
		private TravelDate travelDate;

		/// <summary>
		/// default constructor 
		/// </summary>			
		public CostSearchTicket()
		{
		}

		/// <summary>
		/// constructor that sets the probability of this ticket being available
		/// </summary>			
		public CostSearchTicket(Probability probability) 
		{
			this.probability = probability;		

		}

		/// <summary>
		/// constructor that sets the probability and calls a base constructor with full list of parameters
		/// </summary>			
		public CostSearchTicket(String code, Flexibility flexibility, String shortCode, float adultFare, 
			float childFare, float minAdultFare, float minChildFare, float discountedAdultFare, float discountedChildFare, uint minChildAge,
			uint maxChildAge, Probability probability) : base(code, flexibility, shortCode, adultFare, 
			childFare, discountedAdultFare, discountedChildFare, minChildAge,
			maxChildAge)
		{
			this.probability = probability;	
			this.minAdultFare = minAdultFare;
			this.minChildFare = minChildFare;
		}

		/// <summary>
		/// constructor that just calls base constructor 
		/// </summary>			
		public CostSearchTicket(String code, Flexibility flexibility, String shortCode) : base(code, flexibility, shortCode)
		{
		}
		
		/// <summary>
		/// Creates a shallow copy of the current object 
		/// </summary>			
		/// <returns>A shallow copy of the current instance</returns>
		public override object Clone()
		{
			CostSearchTicket clone = (CostSearchTicket) this.MemberwiseClone();
			return clone;
		}

		/// <summary>
		/// read/write property for probability
		/// </summary>			
		public Probability Probability		
		{
			get
			{
				return probability;
			}
			set
			{
				probability = value;
			}
		}			

		/// <summary>
		/// read/write property for CombinedTicketIndex
		/// </summary>	
		public int CombinedTicketIndex
		{
			get
			{
				return index;
			}
			set 
			{
				index = value;
			}
		}

		/// <summary>
		/// read/write property for legNumber
		/// </summary>			
		public int LegNumber		
		{
			get
			{
				return legNumber;
			}
			set
			{
				legNumber = value;
			}
		}

		/// <summary>
		/// read/write property for journeysForTicket
		/// </summary>
		public CostBasedJourney JourneysForTicket
		{
			get
			{
				return journeysForTicket;
			}
			set
			{
				journeysForTicket = value;
			}
		}

		/// <summary>
		/// read/write property for travelDate
		/// </summary>
		public TravelDate TravelDateForTicket
		{
			get
			{
				return travelDate;
			}
			set
			{
				travelDate = value;
			}
		}	

		/// <summary>
		/// property for adult minimum fare [rw]
		/// </summary>
		public float MinimumAdultFare
		{
			get {return minAdultFare;}
			set {minAdultFare = value;}
		}

		/// <summary>
		/// property for child minimum fare [rw]
		/// </summary>
		public float MinimumChildFare
		{
			get {return minChildFare;}
			set {minChildFare = value;}
		}
	}
	
}
