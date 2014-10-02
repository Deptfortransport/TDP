// *********************************************** 
// NAME			: DisplayableCostSearchTicket.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 08.02.05
// DESCRIPTION	: This class represents a row of 
// table data displayed by the FindFareTicketSelectionControl
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/Adapters/DisplayableCostSearchTicket.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:50   mturner
//Initial revision.
//
//   Rev 1.6   Apr 22 2005 12:28:26   COwczarek
//Add comments
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview

using System;
using System.Collections;
using System.Diagnostics;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// This class represents a row of table 
	/// data displayed by the FindFareTicketSelectionControl
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class DisplayableCostSearchTicket
	{

	    /// <summary>
	    /// The domain class tickets wrapped by this object. For combined tickets
	    /// this collection will hold more than one ticket. For non-combined tickets
	    /// the collection will hold one ticket only.
	    /// </summary>
		private CostSearchTicket[] costSearchTickets;

		/// <summary>
		/// Fare value, formatted for direct output to user. For combined tickets this will be the 
		/// total of all ticket fares.
		/// </summary>
		private string fare;

		/// <summary>
		/// Numeric fare value. For combined tickets this will be the total of all ticket fares.
		/// </summary>
		private float fareValue;

		/// <summary>
		/// Discounted fare value, formatted for direct output to user. For combined tickets this will be the 
		/// total of all discounted ticket fares.
		/// </summary>
		private string discountedFare = string.Empty;

		/// <summary>
		/// Resource id for string that identifies ticket flexibility
		/// </summary>
		private string flexibility;

		/// <summary>
		/// Resource id for string that identifies probable ticket availability
		/// </summary>
		private string probability;

		/// <summary>
		/// If true, indicates that this ticket is an adult ticket, false for a child ticket.
		/// </summary>
		private bool adultTicket;

		/// <summary>
		/// Constuctor. Wraps the supplied non-combined CostSearchTicket in this object. 
		/// </summary>
		/// <param name="processAsAdultTicket">True if the supplied ticket is an adult ticket, false for child ticket</param>
		/// <param name="costSearchTicket">The ticket to be wrapped</param>
		public DisplayableCostSearchTicket(bool processAsAdultTicket, CostSearchTicket costSearchTicket)
		{
            costSearchTickets = new CostSearchTicket[] {costSearchTicket};
			adultTicket = processAsAdultTicket;
			if (processAsAdultTicket)  
			{
				fare = string.Format("{0:N}",costSearchTicket.AdultFare);
				fareValue = costSearchTicket.AdultFare;
				if (!float.IsNaN(costSearchTicket.DiscountedAdultFare))
				{
					discountedFare = string.Format("{0:N}",costSearchTicket.DiscountedAdultFare);
				}
			} 
			else 
			{
				fare = string.Format("{0:N}",costSearchTicket.ChildFare);
				fareValue = costSearchTicket.ChildFare;
				if (!float.IsNaN(costSearchTicket.DiscountedChildFare)) 
				{
					discountedFare = string.Format("{0:N}",costSearchTicket.DiscountedChildFare);
				}
			}
			initFields();
		}

		/// <summary>
		/// Constructor. Wraps the supplied combined CostSearchTicket objects in this object.
		/// </summary>
		/// <param name="processAsAdultTicket">True if the supplied ticket is an adult ticket, false for child ticket</param>
		/// <param name="costSearchTicket">The ticket to be wrapped</param>
		public DisplayableCostSearchTicket(bool processAsAdultTickets, CostSearchTicket[] costSearchTickets)
		{
			this.costSearchTickets = costSearchTickets;
			adultTicket = processAsAdultTickets;

			float totalFare = 0;
			float totalDiscountedFare = 0;

			foreach(CostSearchTicket ticket in costSearchTickets) 
			{
				if (processAsAdultTickets) 
				{
					totalFare += ticket.AdultFare;
					totalDiscountedFare += ticket.DiscountedAdultFare;
				} 
				else 
				{
					totalFare += ticket.ChildFare;
					totalDiscountedFare += ticket.DiscountedChildFare;
				}

			}

			fare = string.Format("{0:N}",totalFare);
			fareValue = totalFare;
			if (!float.IsNaN(totalDiscountedFare)) 
			{
				discountedFare = string.Format("{0:N}",totalDiscountedFare);;
			} 
			initFields();
		}

		#region Properties

		/// <summary>
		/// Read only property. If true, indicates that this ticket is an adult ticket, 
		/// false for a child ticket.
		/// </summary>
		public bool AdultTicket
		{
			get {return adultTicket;}
		}

	    /// <summary>
	    /// Read only property. The domain class tickets wrapped by this object. For combined tickets
	    /// this collection will hold more than one ticket. For non-combined tickets
	    /// the collection will hold one ticket only.
	    /// </summary>
		public CostSearchTicket[] CostSearchTickets
		{
			get {return costSearchTickets;}
		}

		/// <summary>
		/// Read only property giving a string representation of the fare value, 
		/// formatted for direct output to user. For combined tickets this will be the 
		/// total of all ticket fares.
		/// </summary>
		public string Fare
		{
			get {return fare;}
		}

		/// <summary>
		/// Read only property giving the numeric fare value. For combined tickets this will be the total of all ticket fares.
		/// </summary>
		public float FareValue
		{
			get {return fareValue;}
		}

		/// <summary>
		/// Read only property giving a string representation of the discounted fare value, 
		/// formatted for direct output to user. For combined tickets this will be the 
		/// total of all discounted ticket fares.
		/// </summary>
		public string DiscountedFare
		{
			get {return discountedFare;}
		}

		/// <summary>
		/// Read only property giving the resource id for string that identifies ticket flexibility
		/// </summary>
		public string Flexibility
		{
			get {return flexibility;}
		}

		/// <summary>
		/// Read only property giving the resource id for string that identifies probable ticket availability
		/// </summary>
		public string Probability
		{
			get {return probability;}
		}

        /// <summary>
        /// Read only property that returns the 3 letter shortcode of the wrapped ticket. For combined tickets
        /// this will be the short code of the first ticket although all combined tickets should have the
        /// same short code.
        /// </summary>
		public string ShortCode
		{
			get {return costSearchTickets[0].ShortCode;}
		}

		#endregion Properties

		#region Private methods

        /// <summary>
        /// Initialises this objects properties for those that are initialised the same way for both
        /// combined and non-combined tickets.
        /// </summary>
		private void initFields() 
		{
			CostSearchTicket ticket = costSearchTickets[0];
			switch (ticket.Flexibility) 
			{
				case TransportDirect.UserPortal.PricingRetail.Domain.Flexibility.FullyFlexible:
					flexibility = "FindFareTicketSelectionControl.FullyFlexible";
					break;
				case TransportDirect.UserPortal.PricingRetail.Domain.Flexibility.LimitedFlexibility:
					flexibility = "FindFareTicketSelectionControl.LimitedFlexibility";
					break;
				case TransportDirect.UserPortal.PricingRetail.Domain.Flexibility.NoFlexibility:
					flexibility = "FindFareTicketSelectionControl.NoFlexibility";
					break;
			}
			switch (ticket.Probability) {
				case TransportDirect.UserPortal.PricingRetail.Domain.Probability.High:
					probability = "FindFareTicketSelectionControl.HighProbability";
					break;
				case TransportDirect.UserPortal.PricingRetail.Domain.Probability.Medium:
					probability = "FindFareTicketSelectionControl.MediumProbability";
					break;
				case TransportDirect.UserPortal.PricingRetail.Domain.Probability.Low:
					probability = "FindFareTicketSelectionControl.LowProbability";
					break;
                case TransportDirect.UserPortal.PricingRetail.Domain.Probability.None:
                    probability = "FindFareTicketSelectionControl.NoneProbability";
                    break;
            }
		}
		#endregion Private methods

		#region Public methods
		#endregion Public methods

	}
}
