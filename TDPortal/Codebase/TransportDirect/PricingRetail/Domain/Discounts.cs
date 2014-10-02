// *********************************************** 
// NAME			: Discounts.cs
// AUTHOR		: 
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the Discounts class
// ************************************************ 
using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Class specifying the user selected discounts
	/// </summary>
	[Serializable]
	public class Discounts
	{
		private string railDiscount;
		private string coachDiscount;
		private TicketClass ticketClass;

		/// <summary>
		/// Default constructor provider to allow serialisation.
		/// </summary>
		public Discounts()
		{}

		public Discounts(string railDiscount, string coachDiscount, TicketClass ticketClass)
		{
			this.railDiscount = railDiscount;
			this.coachDiscount = coachDiscount;
			this.ticketClass = ticketClass;
		}

		/// <summary>
		/// Read only property for rail discount card
		/// </summary>
		public string RailDiscount
		{
			get {return railDiscount;}
			set {railDiscount = value;}
		}

		/// <summary>
		/// Read only property for coach discount card
		/// </summary>
		public string CoachDiscount
		{
			get {return coachDiscount;}
			set {coachDiscount = value;}
		}

		/// <summary>
		/// Read only property for ticket class
		/// </summary>
		/// 
		
		public TicketClass TicketClass
		{
			get {return ticketClass;}
			set {ticketClass = value;}
		}
	}
}
