// *********************************************** 
// NAME			: AvailabilityEstimate.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Implementation of the AvailabilityEstimate enumeration
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/AvailabilityEstimator/AvailabilityEstimate.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:06   mturner
//Initial revision.
//
//   Rev 1.0   Feb 08 2005 09:52:34   jbroome
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Enumeration of availability estimates.
	/// </summary>
	public enum AvailabilityEstimate
	{
		None,
		Low,
		Medium,
		High
	}
}
