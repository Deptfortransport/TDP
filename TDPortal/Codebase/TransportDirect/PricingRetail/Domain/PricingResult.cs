// *********************************************** 
// NAME			: PricingResult.cs
// AUTHOR		: 
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the PricingResult class
// ************************************************ 
using System;
using System.Collections;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Class representing a PricingResult entity
	/// </summary>
	[Serializable]
	public class PricingResult : ICloneable
	{
		private int minChildAge;
		private int maxChildAge;
		private ArrayList tickets;
		private ArrayList errorResourceIds = new ArrayList();
		private bool noPlacesAvailableForSingles;
		private bool noPlacesAvailableForReturns;
		private bool noThroughFaresAvailable;

		/// <summary>
		/// Create a Pricing Result
		/// </summary>
		public PricingResult(int minChildAge, int maxChildAge, bool noPlacesAvailableForSingles, bool noPlacesAvailableForReturns, bool noThroughFaresAvailable)
		{
			this.minChildAge = minChildAge;
			this.maxChildAge = maxChildAge;
			this.noPlacesAvailableForSingles = noPlacesAvailableForSingles;
			this.noPlacesAvailableForReturns = noPlacesAvailableForReturns;
			this.noThroughFaresAvailable = noThroughFaresAvailable;
			this.tickets = new ArrayList(0);
		}
		/// <summary>
		/// Clone method added so that we can duplicate PricingResult
		/// We make a shallowish copy, this creates a new list of tickets, 
		/// but copies the tickets from the original list to the new list
		///  </summary>
		public object Clone()
		{
			PricingResult clone = new PricingResult(minChildAge, maxChildAge, noPlacesAvailableForSingles, noPlacesAvailableForReturns, noThroughFaresAvailable);
			ArrayList clonedTickets = new ArrayList(tickets);
			clone.tickets = clonedTickets;
			return clone;
		}

		/// <summary>
		/// Read only property for minimum child age
		/// </summary>
		public int MinChildAge
		{
			get { return minChildAge;}
		}

		/// <summary>
		/// Read only property for minimum child age
		/// </summary>
		public int MaxChildAge
		{
			get {return maxChildAge;}
		}

		/// <summary>
		/// Read only property for NoPlacesAvailableForSingles.
		/// Set to true id at least one valid single ticket  
		/// found for the requested service, but no places 
		/// were available on the specified services(s).
		/// </summary>
		public bool NoPlacesAvailableForSingles
		{
			get {return noPlacesAvailableForSingles;}
		}

		/// <summary>
		/// Read only property for NoPlacesAvailableForReturns.
		/// Set to true id at least one valid return ticket  
		/// found for the requested service, but no places 
		/// were available on the specified services(s).
		/// </summary>
		public bool NoPlacesAvailableForReturns
		{
			get {return noPlacesAvailableForReturns;}
		}

		/// <summary>
		/// Read only property for NoThroughFaresAvailable.
		/// Set to true if the call to the RBO returns no fares
		/// as the combination of route and operators planned are not fareable.
		/// </summary>
		public bool NoThroughFaresAvailable
		{
			get {return noThroughFaresAvailable;}
		}

		/// <summary>
		/// Read only property for priced tickets
		///  (contains Ticket objects for time-based, 
		///   but CostSearchTickets for cost-based)
		/// </summary>
		public ArrayList Tickets
		{
			get {return tickets;}
			set {tickets = value;}
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
		///  Sort the tickets using a provided comparer
		/// </summary>
		/// <param name="comparer"></param>
		public void Sort(IComparer comparer)
		{
			tickets.Sort(comparer);
		}
	}
}
