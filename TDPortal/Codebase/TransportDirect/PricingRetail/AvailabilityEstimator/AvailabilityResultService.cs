// *********************************************** 
// NAME			: AvailabilityResultService.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Implementation of the AvailabilityResultService class
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/AvailabilityEstimator/AvailabilityResultService.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:08   mturner
//Initial revision.
//
//   Rev 1.1   Mar 18 2005 15:06:00   jbroome
//Added missing class documentation comments and minor updates after code review
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 17 2005 14:37:52   jbroome
//Initial revision.
//Resolution for 1923: DEV Code Review : Availability Estimator

using System;
using System.Collections;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Class used to hold details of an external ticket
	/// availability request for a specific service
	/// </summary>
	public class AvailabilityResultService
	{

		# region Private Members

		string origin;
		string destination;
		TDDateTime travelDatetime;
		bool available;

		# endregion

		# region Constructor

		/// <summary>
		/// Constructor method for AvailabilityResultService
		/// </summary>
		/// <param name="origin"></param>
		/// <param name="destination"></param>
		/// <param name="travelDateTime"></param>
		/// <param name="available"></param>
		public AvailabilityResultService(string origin, string destination, 
										TDDateTime travelDateTime, bool available)
		{
			this.origin = origin;
			this.destination = destination;
			this.travelDatetime = travelDateTime;
			this.available = available;	
		}

		# endregion

		# region Public Properties

		/// <summary>
		/// Read only string property 
		/// </summary>
		public string Origin
		{
			get { return origin; }
		}

		/// <summary>
		/// Read only string property 
		/// </summary>
		public string Destination
		{
			get { return destination; }
		}

		/// <summary>
		/// Read only TDDateTime property 
		/// Date and Time of travel for this service
		/// </summary>
		public TDDateTime TravelDatetime
		{
			get {return travelDatetime; }
		}

		/// <summary>
		/// Read only boolean property 
		/// Is the ticket available on this service?
		/// </summary>
		public bool Available
		{
			get { return available; }
		}

		#endregion
	
	}
  }
