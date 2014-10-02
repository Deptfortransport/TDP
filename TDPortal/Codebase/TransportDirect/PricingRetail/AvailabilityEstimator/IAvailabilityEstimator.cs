// *********************************************** 
// NAME			: IAvailabilityEstimator.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Implementation of the IAvailabilityEstimator interface
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/AvailabilityEstimator/IAvailabilityEstimator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:08   mturner
//Initial revision.
//
//   Rev 1.1   Mar 09 2005 14:07:32   jbroome
//Replaced AvailabilityEstimate enum with existing Probability enum
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 09:52:34   jbroome
//Initial revision.

using System;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Interface definition for IAvailabilityEstimator.
	/// </summary>
	public interface IAvailabilityEstimator
	{
		Probability GetAvailabilityEstimate(AvailabilityRequest request);
		void UpdateAvailabilityEstimate(AvailabilityResult result);
	}
}
