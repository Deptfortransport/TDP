//********************************************************************************
//NAME         : CoachJourneyFare.cs
//AUTHOR       : James Broome
//DATE CREATED : 22/02/2005
//DESCRIPTION  : Implementation of CoachJourneyFare class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/NatExFares/CoachJourneyFare.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:04   mturner
//Initial revision.
//
//   Rev 1.1   Apr 05 2005 14:41:32   jbroome
//Added DiscountCard property
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.0   Mar 23 2005 09:30:22   jbroome
//Initial revision.
//Resolution for 1405: Adjusting Journey causes unexpected results (DEL5.4)

using System;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// CoachJourneyFare class provides a wrapper around a collection 
	/// of CoachPricingUnitFare objects to form a single 'fare' for a 
	/// whole journey (which could be made up of multiple pricing units)
	/// </summary>
	public class CoachJourneyFare
	{
	
		#region Private members
		
		private float totalAmount;
		private bool isSingle;
		private bool isAdult;
		private bool isDiscounted;
		private string discountCard = string.Empty;
		private CoachPricingUnitFare[] pricingUnitFares;
		
		#endregion

		#region Constructor

		/// <summary>
		/// Constructor. Accepts array of CoachPricingUnitFares
		/// which are used to update internal variables.
		/// </summary>
		/// <param name="pricingUnitFares"></param>
		public CoachJourneyFare(CoachPricingUnitFare[] pricingUnitFares)
		{
			this.pricingUnitFares = pricingUnitFares;
			foreach (CoachPricingUnitFare fare in pricingUnitFares)
			{
				this.totalAmount += fare.FareAmount;
				// When grouping CoachPricingUnitFares, can only match a discounted fare 
				// with an undiscounted fare from another pricing unit as discount cards 
				// are different between operators. Therefore, will only ever expect a 
				// maximum of one discounted fare amongst pricingUnitFares. If present, 
				// then CoachJourneyFare is deemed to be a discounted fare.
				if (fare.DiscountCardType.Length != 0)
				{
					this.isDiscounted = true;
					this.discountCard = fare.DiscountCardType;
				}
			}
		}

		#endregion

		#region Public properties and methods

		/// <summary>
		/// Read-only float property.
		/// Represents the combined fare amounts from each
		/// CoachPricingUnitFare.
		/// </summary>
		public float TotalAmount
		{
			get { return totalAmount; }
		}

		/// <summary>
		/// Read/write bool property.
		/// Is the fare a single or return fare?
		/// </summary>
		public bool IsSingle
		{
			get { return isSingle; }
			set { isSingle = value; }
		}

		/// <summary>
		/// Read/write bool property.
		/// Is the fare an adult or child fare?
		/// </summary>
		public bool IsAdult 
		{
			get { return isAdult; }
			set { isAdult = value; }
		}

		/// <summary>
		/// Read/Write bool property.
		/// Is the fare a disounted fare?
		/// </summary>
		public bool IsDiscounted
		{
			get { return isDiscounted; }
			set { isDiscounted = value; }
		}

		/// <summary>
		/// Read-only string property.
		/// Name of the discount card for 
		/// which the fare applies
		/// </summary>
		public string DiscountCard
		{
			get { return discountCard; }
		}

		/// <summary>
		/// Read-only CoachPricingUnitFare array property.
		/// The collection of PricingUnitFare objects which 
		/// cover the whole journey (one for each CJP pricing unit)
		/// </summary>
		public CoachPricingUnitFare[] PricingUnitFares
		{
			get { return pricingUnitFares; }
		}

		#endregion
		
	}
}
  