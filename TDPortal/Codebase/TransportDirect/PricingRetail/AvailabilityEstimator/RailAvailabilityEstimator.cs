// *********************************************** 
// NAME			: RailAvailabilityEstimator.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Implementation of the RailAvailabilityEstimator class
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/AvailabilityEstimator/RailAvailabilityEstimator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:08   mturner
//Initial revision.
//
//   Rev 1.9   May 17 2006 15:01:54   rphilpott
//Add RouteCode to UnavailableProducts table, associated SP's and all classes that use them.
//Resolution for 4084: DD075: Unavailable products - ticket and route codes
//
//   Rev 1.8   May 05 2006 16:21:04   RPhilpott
//Remove temp unit test fix
//Resolution for 4080: DD075: Unavailable fare not changed to Low availability
//
//   Rev 1.7   May 05 2006 16:17:42   RPhilpott
//Use NLC codes instead of location descriptions for unavailable rail products.
//Resolution for 4080: DD075: Unavailable fare not changed to Low availability
//
//   Rev 1.6   Jan 18 2006 18:16:26   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.5   Apr 28 2005 16:35:16   jbroome
//UnavailableProducts now stored by Outward and Return dates
//Resolution for 2302: PT - Product availability does not handle return products adequately.
//
//   Rev 1.4   Apr 23 2005 11:16:02   jbroome
//Updated UpdateAvailabilityEstimate() after changes to AvailabilityResult class
//
//   Rev 1.3   Mar 18 2005 15:06:02   jbroome
//Added missing class documentation comments and minor updates after code review
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.2   Mar 09 2005 14:07:32   jbroome
//Replaced AvailabilityEstimate enum with existing Probability enum
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.1   Feb 17 2005 14:41:04   jbroome
//Re-written functionality for use with modified AvailabilityResult class
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 09:52:34   jbroome
//Initial revision.

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Class used in obtaining and updating availability estimates 
	/// for Rail travel tickets.
	/// </summary>
	public class RailAvailabilityEstimator : IAvailabilityEstimator
	{
	
		/// <summary>
		/// Constructor
		/// </summary>
		public RailAvailabilityEstimator()
		{

		}

		#region Public Methods

		/// <summary>
		/// GetAvailabilityEstimate returns Probability enumeration
		/// for requested ticket availability
		/// </summary>
		/// <param name="request">AvailablilityRequest </param>
		/// <returns>Probability enum</returns>
		public Probability GetAvailabilityEstimate(AvailabilityRequest request)
		{
			Probability estimate = Probability.None;
			AvailabilityEstimatorDBHelper helper = new AvailabilityEstimatorDBHelper();

			// Firstly check to see if product is know to be unavailable
			bool unavailable = helper.CheckUnavailableProducts(	request.Mode.ToString(), 
																request.OriginGroup, 
																request.DestinationGroup, 
																request.OutwardTravelDate,
																request.ReturnTravelDate,
																request.TicketCode,
																request.RouteCode);

			// Is product known to be unavailable
			if (unavailable)
			{
				// In this case, return Low probability
				return Probability.Low;
			}
			else
			{
				// Get the product profile probability estimate, via the DB helper
				string probability = helper.GetProductProfile(	request.Mode.ToString(), 
																request.Origin, 
																request.Destination, 
																request.TicketCode, 
																request.OutwardTravelDate);
				// Process the results
				if ((probability != null) && (probability.Length != 0))
				{
					estimate = (Probability)Enum.Parse(typeof(Probability), probability, true);					
					return estimate;
				}
				else
				{
					// No matching product profiles were found (should never happen) 
					// or an exception has occurred. Return estimate of None i.e. unknown
					return Probability.None;
				}
			}
		}

		/// <summary>
		/// UpdateAvailabilityEstimate logs history of external availability
		/// result and stores details if ticket is unavailable.
		/// </summary>
		/// <param name="result">AvailabilityResult from external 'real' availability request</param>
		public void UpdateAvailabilityEstimate(AvailabilityResult result)
		{
			AvailabilityEstimatorDBHelper helper = new AvailabilityEstimatorDBHelper();

			// Each journey within the result contains an array of services
			for (int i=0; i < result.JourneyCount; i++)
			{
				// Retrieve services array for each journey
				AvailabilityResultService[] services = result.GetServicesForJourney(i); 	

				//Need to log availability history for each service
				foreach (AvailabilityResultService service in services)
				{
					helper.AddAvailabilityHistory(	result.Mode.ToString(), 
													service.Origin, 
													service.Destination, 
													result.TicketCode,
													service.TravelDatetime,
													service.Available);
				}
			}

			// If we now know product is unavailable then update UnavailableProducts table
			if (!result.Available)
			{
				helper.AddUnavailableProduct(	result.Mode.ToString(), 
												result.OriginGroup,
												result.DestinationGroup,
												result.OutwardDate,		//Only date portion of this datetime will be used
												result.ReturnDate,		//Only date portion of this datetime will be used
												result.TicketCode,
												result.RouteCode);
			}
		}

		#endregion

	}
}
