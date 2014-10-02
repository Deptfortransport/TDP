//********************************************************************************
//NAME         : RoutePriceSupplier.cs
//AUTHOR       : Russell Wilby
//DATE CREATED : 02/10/2005
//DESCRIPTION  : Implementation of IRoutePriceSupplier interface
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/RoutePriceSupplierFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:56   mturner
//Initial revision.
//
//   Rev 1.1   Nov 07 2005 16:09:58   RWilby
//Updated to check for null coachOperator
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Nov 02 2005 18:00:34   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.RBOGateway;
using TransportDirect.UserPortal.PricingRetail.CoachFares;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Implementation of RoutePriceSupplierFactory.
	/// </summary>
	public class RoutePriceSupplierFactory :IRoutePriceSupplierFactory, IServiceFactory
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public RoutePriceSupplierFactory()
		{}

		/// <summary>
		/// Return an appropriate RoutePriceSupplier for the given mode and operatorCode.
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="operatorCode"></param>
		/// <returns></returns>
		public IRoutePriceSupplier GetSupplier (TicketTravelMode mode, string operatorCode)
		{
			switch (mode) 
			{
				case TicketTravelMode.Rail :
					return new Gateway();
				case TicketTravelMode.Coach :
				{
					//Get an instance of CoachOperatorLookup from ServiceDiscovery
					ICoachOperatorLookup coachOperatorLookup = 
						(ICoachOperatorLookup)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];
					
					//Get coach operator details for operator code
					CoachOperator coachOperator = coachOperatorLookup.GetOperatorDetails(operatorCode);
					
					//Check for null coachOperator
					if (coachOperator == null)
						return null;
						
					//Return appropriate IRoutePriceSupplier for operator interface
					switch (coachOperator.InterfaceType) 
					{
						case CoachFaresInterfaceType.ForJourney :
							return new JourneyCoachFareSupplier();
						case CoachFaresInterfaceType.ForRoute :
							return new RouteCoachFareSupplier();
						default:
							return null;
					}
				}		
				default:
					return null;
			}
		}


		#region IServiceFactory Members
		/// <summary>
		///  Returns the RoutePriceSupplierFactory
		/// </summary>
		/// <returns>The current RoutePriceSupplierFactory</returns>
		public object Get()
		{
			return this;
		}
		#endregion
	}
}
