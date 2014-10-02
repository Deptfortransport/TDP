// *********************************************** 
// NAME			: Ticket.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the Ticket class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/Ticket.cs-arc  $
//
//   Rev 1.1   Feb 02 2009 18:48:34   rbroddle
//Added property MatchingNLCreturn
//Resolution for 5221: CCN0492 Return Fares Involving Grouped Stations
//
//   Rev 1.0   Nov 08 2007 12:36:56   mturner
//Initial revision.
//
//   Rev 1.17   Jan 17 2006 17:56:56   RPhilpott
//Code review updates.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.16   Nov 30 2005 09:36:46   RPhilpott
//Enhance clone method.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.15   Nov 09 2005 12:31:34   build
//Automatically merged from branch for stream2818
//

using System;
using System.Collections;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Class representing a Ticket entity
	/// </summary>
	[Serializable]
	public class Ticket  : ICloneable
	{		
		private string code;
		private string shortCode = string.Empty;
		private float adultFare = float.NaN;
		private float childFare = float.NaN;
		private float discountedAdultFare = float.NaN;
		private float discountedChildFare = float.NaN;
		private Flexibility flexibility;
		private uint minChildAge;
		private uint maxChildAge;
		private ArrayList ticketUpgrades = new ArrayList();
		private RailFareData railFareData;
		private CoachFareData coachFareData;
        private bool isMatchingNLCreturn;

		/// <summary>
		/// default constructor
		/// </summary>
		public Ticket ()
		{

		}

		/// <summary>
		/// Create a ticket without fare information
		/// </summary>
		/// <param name="code"></param>		
		/// <param name="flexibility"></param>
		public Ticket(String code, Flexibility flexibility)
		{
			this.code = code;
			this.flexibility = flexibility;
		}

		/// <summary>
		/// Create a ticket without discounted fares and with a short code used to get additional
		/// information on the flexibility
		/// </summary>
		/// <param name="code"></param>
		/// <param name="flexibility"></param>
		/// <param name="shortCode"></param>
		public Ticket(String code, Flexibility flexibility, String shortCode) : this(code, flexibility)
		{
			this.shortCode = shortCode;
		}

		/// <summary>
		/// Create a ticket with fare information
		/// </summary>
		/// <param name="code"></param>
		/// <param name="flexibility"></param>
		/// <param name="shortCode"></param>
		/// <param name="adultFare"></param>
		/// <param name="childFare"></param>
		/// <param name="discountedAdultFare"></param>
		/// <param name="discountedChildFare"></param>
		public Ticket(String code, Flexibility flexibility, String shortCode, float adultFare, 
			float childFare, float discountedAdultFare, float discountedChildFare) : this(code, flexibility, shortCode)
		{
			this.adultFare = adultFare;
			this.childFare = childFare;
			this.discountedAdultFare = discountedAdultFare;
			this.discountedChildFare = discountedChildFare;
		}		

		/// <summary>
		/// Create a ticket with all fields set 
		/// </summary>
		/// <param name="code"></param>
		/// <param name="flexibility"></param>
		/// <param name="shortCode"></param>
		/// <param name="adultFare"></param>
		/// <param name="childFare"></param>
		/// <param name="discountedAdultFare"></param>
		/// <param name="discountedChildFare"></param>
		/// <param name="minChildAge"></param>
		/// <param name="maxChildAge"></param>
		public Ticket(String code, Flexibility flexibility, String shortCode, float adultFare, 
			float childFare, float discountedAdultFare, float discountedChildFare, uint minChildAge,
			uint maxChildAge) : this(code, flexibility,shortCode, adultFare, childFare, discountedAdultFare,discountedChildFare)
		{
			this.minChildAge = minChildAge;
			this.maxChildAge = maxChildAge;			
		}		

		
		/// <summary>
		/// Compares object for equality.
		/// </summary>
		/// <param name="obj">Object to compare against.</param>
		/// <returns>True if object is equal, else false.</returns>
		public override bool Equals(object obj)
		{
			if (obj is Ticket)
			{
				Ticket objTicket = (Ticket)obj;
				//if railfare data has been set, compare the properties which make the ticket unique
				if (objTicket.railFareData !=null && this.railFareData !=null)
				{
					return ((objTicket.railFareData.ShortTicketCode == this.railFareData.ShortTicketCode)
						&& (objTicket.railFareData.RouteCode == this.railFareData.RouteCode)
						&& (objTicket.railFareData.RestrictionCode == this.railFareData.RestrictionCode));
				}
				else if ((objTicket.railFareData == null && this.railFareData != null)
					|| (objTicket.railFareData != null && this.railFareData == null))
				{					
					return false;
				}
				else // when there is no railfaredata compare the ticket Codes - can't use ShortCode here as it is not set for coach tickets
					return (objTicket.Code == this.Code);
			}
			else
				return false;
		}

		/// <summary>
		/// Provides a hashcode for the class so that it can be used as a key in a hashtable
		/// </summary>
		/// <returns>Hash code</returns>
		public override int GetHashCode()
		{
			if (this.railFareData !=null)
			{
				return (this.railFareData.ShortTicketCode.GetHashCode() + this.railFareData.RouteCode.GetHashCode() + this.railFareData.RestrictionCode.GetHashCode());
			}
			else
				return (this.Code.GetHashCode());
		}

		
		/// <summary>
		/// Clone method added so that we can duplicate Ticket objects
		/// This is used when creating the various discounted tickets based on an original undiscounted ticket.
		///  </summary>
		///  <returns>A copy of the current instance</returns>
		public virtual object Clone()
		{
			Ticket clone = new Ticket(code, flexibility, shortCode, adultFare, childFare, discountedAdultFare, discountedChildFare, minChildAge, maxChildAge);

			clone.railFareData	= railFareData;
			clone.coachFareData = coachFareData;
			clone.ticketUpgrades = ticketUpgrades;
			
			return clone;
		}
		
		/// <summary>
		/// Read only property for ticket code
		/// </summary>
		public String Code
		{
			get { return code;}
		}

		/// <summary>
		/// Read only property for three digit short code
		/// </summary>
		public String ShortCode
		{
			get { return shortCode; }
		}

		/// <summary>
		/// property for adult fare
		/// </summary>
		public float AdultFare
		{
			get {return adultFare;}
			set {adultFare = value;}
		}

		/// <summary>
		/// property for child fare
		/// </summary>
		public float ChildFare
		{
			get {return childFare;}
			set {childFare = value;}
		}

		/// <summary>
		///property for discounted adult fare
		/// </summary>
		public float DiscountedAdultFare
		{
			get {return discountedAdultFare;}
			set {discountedAdultFare = value;}
		}

		/// <summary>
		/// property for discounted child fare
		/// </summary>
		public float DiscountedChildFare
		{
			get {return discountedChildFare;}
			set {discountedChildFare = value;}
		}

		/// <summary>
		/// read/write property for flexibility
		/// </summary>
		public Flexibility Flexibility
		{
			get {return flexibility;}
			set {flexibility = value;}
		}

		/// <summary>
		/// read/write property for minChildAge
		/// </summary>
		public uint MinChildAge		
		{
			get
			{
				return minChildAge;
			}
			set
			{
				minChildAge = value;
			}
		}
		/// <summary>
		/// read/write property for maxChildAge
		/// </summary>
		public uint MaxChildAge		
		{
			get
			{
				return maxChildAge;
			}
			set
			{
				maxChildAge = value;
			}
		}
		
		/// <summary>
		/// read/write property for railFareData
		/// </summary>
		public RailFareData TicketRailFareData
		{
			get 
			{
				return railFareData;
			}
			set
			{
				railFareData = value;
			}
		}
		/// <summary>
		/// A collection of supplements applicable 
		/// to this journey and fare, if any.
		/// Will only be populated for time-based
		/// search results (otherwise returns an  
		/// empty array).
		/// </summary>
		public Upgrade[] Upgrades 
		{
			get { return (Upgrade[])(this.ticketUpgrades.ToArray(typeof(Upgrade))); } 
		}

		public void AddUpgrade(Upgrade upgrade)
		{
			foreach (Upgrade existingUpgrade in ticketUpgrades)
			{
				if	(existingUpgrade.Code.Equals(upgrade.Code))
				{
					return;			// already got it
				}
			}

			ticketUpgrades.Add(upgrade);
		}

		/// <summary>
		/// property for CoachFareData. [rw]
		/// </summary>
		public CoachFareData TicketCoachFareData
		{
			get {return coachFareData;}
			set {coachFareData = value;}
		}

        /// <summary>
        /// property for MatchingNLCreturn - used to flag an outbound return rail ticket having origin & dest NLC codes  
        /// that are a reverse match for the NLCs of at least 1 ticket on the equivalent inbound single pricing result. [rw]
        /// </summary>
        public bool MatchingNLCreturn
        {
            get { return isMatchingNLCreturn; }
            set { isMatchingNLCreturn = value; }
        }
	}
}
