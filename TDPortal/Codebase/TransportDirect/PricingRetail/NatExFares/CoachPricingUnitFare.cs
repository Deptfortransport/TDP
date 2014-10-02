//********************************************************************************
//NAME         : CoachPricingUnitFare.cs
//AUTHOR       : James Broome
//DATE CREATED : 22/02/2005
//DESCRIPTION  : Implementation of CoachPricingUnitFare class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/NatExFares/CoachPricingUnitFare.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:06   mturner
//Initial revision.
//
//   Rev 1.1   Apr 29 2005 12:01:32   jbroome
//Removed FirstLegIndex and LastLegIndex properties of CoachPricingUnit fare as these are now redundant due to the corresponding properties being removed from the CostSearchTicket class.
//
//   Rev 1.0   Mar 23 2005 09:30:22   jbroome
//Initial revision.
//Resolution for 1405: Adjusting Journey causes unexpected results (DEL5.4)

using System;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Class used to hold coach fare information returned 
	/// from the CJP. One CJP Fare corresponds to one CoachPricingUnitFare. 
	/// </summary>
	public class CoachPricingUnitFare
	{
		#region Private members
		
		private string startLocationNaptan;
		private string endLocationNaptan;
		private float fareAmount;
		private string fareType;
		private string fareTypeConditions;
		private int pricingUnitIndex;
		private string discountCardType;
		private int minChildAge;
		private int maxChildAge;
		private Flexibility flexibility;
		
		#endregion
		
		#region Constructor

		/// <summary>
		/// Constructor. CoachPricingUnit fare objects are created during 
		/// the cost based search process. The parameters used to update 
		/// internal variables come directly from the CJP journey result.
		/// </summary>
		/// <param name="startLocationNaptan">string: NaPTANID from board</param>
		/// <param name="endLocationNaptan">string: NaPTANID from alight</param>
		/// <param name="fareAmount">float: formatted int fare value of CJP Fare</param>
		/// <param name="fareType">string: fareType value of CJP Fare</param>
		/// <param name="fareTypeConditions">string: fareTypeConditions value of CJP Fare</param>
		/// <param name="operatorCode">string: operatorCode of relevant Leg to which Fare applies</param>
		/// <param name="discountCardType">string: discountCardType value of CJP Fare</param>
		/// <param name="minChildAge">int: formatted childAgeRange string value of CJP Fare</param>
		/// <param name="maxChildAge">int: formatted childAgeRange string value of CJP Fare</param>
		/// <param name="flexibility">Flexibility: converted fareRestrictionType value of CJP Fare</param>
		public CoachPricingUnitFare(	string startLocationNaptan, 
										string endLocationNaptan, 
										float fareAmount,	
										string fareType, 
										string fareTypeConditions,
										int pricingUnitIndex,
										string discountCardType,
										int minChildAge,
										int maxChildAge,
										Flexibility flexibility)
		{
			this.startLocationNaptan = startLocationNaptan;
			this.endLocationNaptan = endLocationNaptan;
			this.fareAmount = fareAmount;
			this.fareType = fareType;
			this.fareTypeConditions = fareTypeConditions;	
			this.pricingUnitIndex = pricingUnitIndex;
			this.discountCardType = discountCardType;
			this.minChildAge = minChildAge;
			this.maxChildAge = maxChildAge;
			this.flexibility = flexibility;
		}

		#endregion

		#region Public properties and methods

		/// <summary>
		/// Read-only string property.
		/// NaPTAN of start of journey leg to 
		/// which fare applies.
		/// </summary>
		public string StartLocationNaptan
		{
			get { return startLocationNaptan; }
		}

		/// <summary>
		/// Read-only string property.
		/// NaPTAN of end of journey leg to 
		/// which fare applies.
		/// </summary>
		public string EndLocationNaptan
		{
			get { return endLocationNaptan; }
		}

		/// <summary>
		/// Read-only float property.
		/// Cost of fare.
		/// </summary>
		public float FareAmount
		{
			get { return fareAmount; }
		}

		/// <summary>
		/// Read-only string property.
		/// Type/Name of fare e.g. Economy Return.
		/// </summary>
		public string FareType
		{
			get { return fareType; }
		}

		/// <summary>
		/// Read-only string property.
		/// Conditions applying to fare type.
		/// </summary>
		public string FareTypeConditions
		{
			get { return fareTypeConditions; }
		}

		/// <summary>
		/// Read-only int property.
		/// Index of the pricing unit 
		/// to which the fare applies.		
		/// </summary>
		public int PricingUnitIndex
		{
			get { return pricingUnitIndex; }
		}

		/// <summary>
		/// Read-only string property.
		/// Discount card (if any) which is required
		/// to purchase fare.
		/// </summary>
		public string DiscountCardType
		{
			get { return discountCardType; }
		}

		/// <summary>
		/// Read-only int property.
		/// Minimum age at which a child fare ticket
		/// is required.
		/// </summary>
		public int MinChildFare
		{
			get { return minChildAge; }
		}
		
		/// <summary>
		/// Read-only int property.
		/// Maximum age at which a child fare
		/// ticket can be used.
		/// </summary>
		public int MaxChildFare
		{
			get { return maxChildAge; }
		}

		/// <summary>
		/// Read-only property.
		/// Flexibility restrictions for fare
		/// type e.g. Fully Flexible, No Flexibility.
		/// </summary>
		public Flexibility Flexibility
		{
			get { return flexibility; }
		}

		#endregion

	}
}
