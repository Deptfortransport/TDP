// *********************************************** 
// NAME			: AirAvailabilityEstimator.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Implementation of the AirAvailabilityEstimator class
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/AvailabilityEstimator/AirAvailabilityEstimator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:06   mturner
//Initial revision.
//
//   Rev 1.2   Mar 18 2005 15:06:00   jbroome
//Added missing class documentation comments and minor updates after code review
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.1   Mar 09 2005 14:07:32   jbroome
//Replaced AvailabilityEstimate enum with existing Probability enum
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 09:52:34   jbroome
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Class used in obtaining and updating availability estimates 
	/// for Air travel tickets.
	/// </summary>
	public class AirAvailabilityEstimator : IAvailabilityEstimator
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public AirAvailabilityEstimator()
		{

		}
	
		#region Public Methods

		/// <summary>
		/// GetAvailabilityEstimate returns Probability enumeration
		/// for requested ticket availability
		/// </summary>
		/// <param name="request">AvailabilityRequest</param>
		/// <returns>Probability enum</returns>
		public Probability GetAvailabilityEstimate(AvailabilityRequest request)
		{
			// ----------------------------------------------------------- 
			// Air ticket availability information is not currently considered.
			// At present this method will always return Probability.None
			// i.e. unkown.
			// This may change in the future, using AvailabilityEstimatorDBHelper  
			// to return estimates from DB, like RailAvailabilityEstimator
			// ----------------------------------------------------------- 
			return Probability.None;
		}

		/// <summary>
		/// UpdateAvailabilityEstimate logs history of external availability
		/// result and stores details if ticket is unavailable.
		/// </summary>
		/// <param name="result">AvailabilityResult from external 'real' availability request</param>
		public void UpdateAvailabilityEstimate(AvailabilityResult result)
		{
			// ----------------------------------------------------------- 
			// As no air ticket information is stored internally, this can 
			// not be updated. Currently, an exception is thrown if this method
			// is called. 
			// ----------------------------------------------------------- 
		
			throw new TDException("Error in UpdateAvailabilityEstimate(). This method call is not permitted for AirAvailabilityEstimator.", 
				false, TDExceptionIdentifier.AEInvalidUpdateAvailabilityEstimateCall);

		}

		#endregion

	}
}
