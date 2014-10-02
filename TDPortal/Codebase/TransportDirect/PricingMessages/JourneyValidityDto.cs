//********************************************************************************
//NAME         : JourneyValidityDto.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-02-24
//DESCRIPTION  : Data Transfer Object to pass back results of  
//				  validity and applicable supplements after restriction 
//                and availability checking for a single rail journey 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/JourneyValidityDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:50   mturner
//Initial revision.
//
//   Rev 1.2   Apr 07 2005 20:53:08   RPhilpott
//Corrections to Supplement and Availability checking.
//
//   Rev 1.1   Mar 22 2005 16:08:34   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.0   Mar 01 2005 18:47:42   RPhilpott
//Initial revision.
//

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Data Transfer Object to pass back results of  
	///	  validity and applicable supplements after restriction 
	///   and availability  checking for a single rail journey 
	/// </summary>
	[CLSCompliant(false)]
	[Serializable()]
	public class JourneyValidityDto
	{
		private int journeyIndex;
		private JourneyValidity validity;
		private ArrayList supplements = new ArrayList();

		public JourneyValidityDto(int journeyIndex, JourneyValidity validity)
		{
			this.journeyIndex = journeyIndex;
			this.validity = validity;
		}

		public JourneyValidityDto(int journeyIndex, JourneyValidity validity, SupplementDto[] supplements)
		{
			this.journeyIndex = journeyIndex;
			this.validity = validity;
			this.supplements.AddRange(supplements);
		}

		/// <summary>
		/// Index of the PublicJourney to which this status applies
		/// </summary>
		public int JourneyIndex
		{
			get { return journeyIndex; }
		}

		/// <summary>
		/// Result of validity checking for this journey for specified fare
		/// </summary>
		public JourneyValidity Validity
		{
			get { return validity; }
		}

		/// <summary>
		/// Array of all applicable supplements
		/// </summary>
		public SupplementDto[] Supplements
		{
			get { return (SupplementDto[])(this.supplements.ToArray(typeof(SupplementDto))); } 
		}

		/// <summary>
		/// Add supplement applicable to this journey
		/// </summary>
		public void AddSupplement(SupplementDto supplement)
		{
			foreach (SupplementDto existingSupplement in supplements)
			{
				if	(existingSupplement.Code.Equals(supplement.Code))
				{
					return;		// already got it
				}
			}

			supplements.Add(supplement);
		}
	}


	/// <summary>
	/// Enumeration of possible journey validity statuses.
	/// </summary>
	public enum JourneyValidity
	{
		ValidFare, 
		MinimumFareApplies,
		NoPlacesAvailable,
		InvalidFare,
	}
}
