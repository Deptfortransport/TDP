// *********************************************** 
// NAME			: AvailabilityEstimatorFactory.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Implementation of the AvailabilityEstimatorFactory class
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/AvailabilityEstimator/AvailabilityEstimatorFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:06   mturner
//Initial revision.
//
//   Rev 1.1   Mar 18 2005 15:06:00   jbroome
//Added missing class documentation comments and minor updates after code review
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
	/// Factory class returns the correct AvailabilityEstimator 
	/// class based on the travel mode specifed.
	/// </summary>
	public class AvailabilityEstimatorFactory
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public AvailabilityEstimatorFactory()
		{
		}

		#region Public Methods

		/// <summary>
		/// Returns correct IAvailabilityEstimator implementation based on TicketTravelMode enum
		/// </summary>
		/// <param name="mode">TicketTravel mode enum</param>
		/// <returns>IAvailabilityEstimator</returns>
		public IAvailabilityEstimator GetAvailabilityEstimator(TicketTravelMode mode)
		{
			switch (mode)
			{
				case TicketTravelMode.Rail:
				{
					return new RailAvailabilityEstimator();
				}
				case TicketTravelMode.Coach:
				{
					return new CoachAvailabilityEstimator();
				}
				case TicketTravelMode.Air:
				{
					return new AirAvailabilityEstimator();
				}
				default:
				{
					// Should never happen
					return null;
				}
			}
		}

		#endregion
	}
}
