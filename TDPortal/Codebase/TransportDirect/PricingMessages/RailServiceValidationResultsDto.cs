//********************************************************************************
//NAME         : RailServiceValidationResultsDto.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-02-24
//DESCRIPTION  : Data Transfer Object to pass back results of  
//				  validity after restriction and availability 
//                checking for the collection of supplied journeys 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/RailServiceValidationResultsDto.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:54   mturner
//Initial revision.
//
//   Rev 1.2   Dec 02 2005 12:38:20   RPhilpott
//Add extra flag for handling of "no inventory found" condition.
//Resolution for 3202: DN040: Specific handling of no inventory condition.
//
//   Rev 1.1   Mar 22 2005 16:08:36   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.0   Mar 01 2005 18:47:44   RPhilpott
//Initial revision.

using System;
using System.Collections;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Summary description for RailServiceValidationResultsDto.
	/// </summary>
	[Serializable()]
	[CLSCompliant(false)]
	public class RailServiceValidationResultsDto
	{
		private ArrayList rvboData = new ArrayList();
		private ArrayList outwardValidities = new ArrayList();
		private ArrayList returnValidities  = new ArrayList();
		private ArrayList errorResourceIds  = new ArrayList();

		private bool includesNoInventoryResults = false;

		public RailServiceValidationResultsDto()
		{
		}

		/// <summary>
		/// Array of all outward validities
		/// </summary>
		public JourneyValidityDto[] OutwardValidities
		{
			get { return (JourneyValidityDto[])(this.outwardValidities.ToArray(typeof(JourneyValidityDto))); } 
		}

		/// <summary>
		/// Add outward validity
		/// </summary>
		public void AddOutwardValidity(int journeyIndex, JourneyValidity validity, SupplementDto[] supplements) 
		{
			outwardValidities.Add(new JourneyValidityDto(journeyIndex, validity, supplements));
		}

		/// <summary>
		/// Array of all return validities
		/// </summary>
		public JourneyValidityDto[] ReturnValidities
		{
			get { return (JourneyValidityDto[])(this.returnValidities.ToArray(typeof(JourneyValidityDto))); } 
		}

		/// <summary>
		/// Add return validity
		/// </summary>
		public void AddReturnValidity(int journeyIndex, JourneyValidity validity, SupplementDto[] supplements) 
		{
			returnValidities.Add(new JourneyValidityDto(journeyIndex, validity, supplements));
		}

		/// <summary>
		/// Array of results of all RVBO/NRS calls
		/// </summary>
		public RailAvailabilityResultDto[] RailAvailabilityResults
		{
			get { return (RailAvailabilityResultDto[])(this.rvboData.ToArray(typeof(RailAvailabilityResultDto))); } 
		}

		/// <summary>
		/// Add result of an RVBO/NRS call
		/// </summary>
		public void AddRailAvailabilityResults(ArrayList newResults) 
		{
			rvboData.AddRange(newResults);
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
		/// Indicates if any of the services had "no inventory" response
		///  - imples that further quotas may be added later for this fare
		/// </summary>
		public bool IncludesNoInventoryResults
		{
			get { return includesNoInventoryResults; } 
			set { includesNoInventoryResults = value; } 
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

	}
}
