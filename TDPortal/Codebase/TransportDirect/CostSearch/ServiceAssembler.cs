// ************************************************************** 
// NAME			: ServiceAssembler.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 18/10/2005 
// DESCRIPTION	: Definition of the ServiceAssembler base class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/ServiceAssembler.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:20   mturner
//Initial revision.
//
//   Rev 1.1   Nov 02 2005 09:34:32   RWilby
//Updated class
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 28 2005 15:31:32   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// FareAssembler base class
	/// </summary>
	public abstract class ServiceAssembler
	{
		#region protected members
		protected const string MINIMUM_FARE_WARNING = "JourneyPlannerOutput.MinimumFareApplies";
		
		//session info object
		protected CJPSessionInfo sessionInfo; 

		//the Supplier to obtain services from		
		protected IPricedServicesSupplier pricedServicesSupplier;	

		//results		
		protected TDJourneyRequest existingJourneyRequest;	
		
		#endregion

		#region private methods
		
		
	
		#endregion

		/// <summary>
		/// ServiceAssembler static method. Returns appropriate concrete class based on mode.
		/// </summary>
		/// <param name="?"></param>
		/// <returns></returns>
		public static ServiceAssembler GetAssembler (TicketTravelMode mode)
		{
			switch (mode) 
			{
				case TicketTravelMode.Rail :
					return new RailServiceAssembler();
				case TicketTravelMode.Coach :
					return new CoachServiceAssembler();		
				default:
					return null;
			}
		}

		
		/// <summary>
		/// Override this method to returns a CostSearchResult with journey information for the selected ticket	
		/// </summary>
		/// <param name="request"></param>
		/// <param name="existingResult"></param>
		/// <param name="ticket"></param>
		/// <returns></returns>
		public abstract ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket ticket);

		/// <summary>
		/// Overloaded version of AssembleServices method. 
		/// Override this method to returns a CostSearchResult with journey information for 2 Singles tickets
		/// </summary>
		/// <param name="request"></param>
		/// <param name="existingResult"></param>
		/// <param name="outwardTicket"></param>
		/// <param name="inwardTicket"></param>
		/// <returns></returns>
		public abstract ICostSearchResult AssembleServices(ICostSearchRequest request, ICostSearchResult existingResult, CostSearchTicket outwardTicket, CostSearchTicket inwardTicket);

	}
}
