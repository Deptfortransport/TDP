// ****************************************************************** 
// NAME			: TestMockCoachRoutesQuotaFaresProvider.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 20/10/2005 
// DESCRIPTION	: Definition of the RoutesQuotaFaresProvider mock test class
// ****************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearch/Test/TestMockCoachRoutesQuotaFaresProvider.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 16:21:54   mturner
//Initial revision.
//
//   Rev 1.1   Nov 07 2005 18:21:12   RWilby
//Updated mock class to fix data bug
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Nov 02 2005 09:32:56   RWilby
//Initial revision.
//Resolution for 2818: DEL 8 Stream: Search by Price
using System;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CostSearch
{
	/// <summary>
	/// Summary description for TestMockCoachRoutesQuotaFaresProvider.
	/// </summary>
	public class TestMockCoachRoutesQuotaFaresProvider : ICoachRoutesQuotaFaresProvider, IServiceFactory
	{
		public TestMockCoachRoutesQuotaFaresProvider()
		{

		}

		/// <summary>
		/// FindRoutesAnsQuotaFares analyses the OperatorList object graph to build up a RouteList object graph to return
		/// </summary>
		/// <param name="originNaPTAN">Origin NaPTAN location</param>
		/// <param name="destinationNaPTAN">Destination NaPTAN location</param>
		/// <returns>RouteList collection</returns>
		public RouteList FindRoutesAndQuotaFares(string originNaPTAN, string destinationNaPTAN)
		{
			RouteList routeList = new RouteList();
			
			//Add Route1 with one leg
			//London direct to Cambridge
			Route route1 = new Route();
			route1.OriginNaPTAN = originNaPTAN;
			route1.DestinationNaPTAN = destinationNaPTAN;
			
			Leg route1Leg1 = new Leg();
			route1Leg1.CoachOperatorCode = "NX";
			route1Leg1.StartNaPTAN = originNaPTAN;
			route1Leg1.EndNaPTAN = destinationNaPTAN;
			route1Leg1.QuotaFareList = new QuotaFareList();
	
			route1.LegList.Add(route1Leg1);
			routeList.Add(route1);

			//Add Route2 with two legs
			//London to Luton with NX
			//Luton to Cambrige with SCL
			Route route2 = new Route();
			const string LutonNaptan = "900051114";
			route2.OriginNaPTAN = originNaPTAN;
			route2.DestinationNaPTAN = destinationNaPTAN;
			
			Leg route2Leg1 = new Leg();
			route2Leg1.CoachOperatorCode = "NX";
			route2Leg1.StartNaPTAN = originNaPTAN;
			route2Leg1.EndNaPTAN = LutonNaptan; //Luton naptan
			route2Leg1.QuotaFareList = new QuotaFareList();

			Leg route2Leg2 = new Leg();
			route2Leg2.CoachOperatorCode = "SCL";
			route2Leg2.StartNaPTAN = LutonNaptan; //Luton naptan
			route2Leg2.EndNaPTAN = destinationNaPTAN;
			route2Leg2.QuotaFareList = new QuotaFareList();

			route2.LegList.Add(route2Leg1);
			route2.LegList.Add(route2Leg2);
			routeList.Add(route2);

			return routeList;
		}

		#region Implementation of IServiceFactory
		/// <summary>
		/// Returns the current CoachRoutesQuotaFareProvider object
		/// </summary>
		/// <returns>CoachRoutesQuotaFareProvider object</returns>
		public object Get()
		{
			return this;
		}
		#endregion
	}
}
