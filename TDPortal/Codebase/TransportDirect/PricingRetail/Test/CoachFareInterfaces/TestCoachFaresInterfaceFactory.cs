//********************************************************************************
//NAME         : TestCoachFaresInterfaceFactory.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Test class for TestCoachFaresInterfaceFactory class whcich produces FaresInterfaces.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFareInterfaces/TestCoachFaresInterfaceFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:16   mturner
//Initial revision.
//
//   Rev 1.11   Jan 03 2006 12:40:48   mguney
//GetFaresInterface methods changed to use the new TestMockFareInterface constructor.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.10   Dec 01 2005 09:51:24   mguney
//MockFareInterface is replaced with TestMockFareInterface in the related test classes.
//Resolution for 3267: Mock classes should be prefixed with Test.
//
//   Rev 1.9   Nov 03 2005 14:00:28   RPhilpott
//Added extra method override to interface.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.8   Oct 27 2005 14:54:48   mguney
//DB operations removed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7   Oct 27 2005 14:52:36   mguney
//Mock version of the ICoachOperatorLookup is used.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.6   Oct 25 2005 15:25:48   mguney
//CoachFaresInterfaceFactory is being initialised directly instead of getting it from the service discovery as the latter uses the test version.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Oct 25 2005 11:13:02   mguney
//Initialisation changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Oct 24 2005 15:52:12   mguney
//ICoachFaresInterfaceFactory implemented. GetFaresInterface method returns the MockFareInterface.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 22 2005 15:52:54   mguney
//Factories returned according to the operator code.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 15:04:54   mguney
//Creation dare corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 12:26:54   mguney
//SCR associated
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 12:25:12   mguney
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;

using TransportDirect.UserPortal.PricingRetail.CoachFares;


namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Test class for fare interface for route based fares. (IF98)	
	/// </summary>
	[TestFixture]
	public class TestCoachFaresInterfaceFactory : IServiceFactory, ICoachFaresInterfaceFactory
	{	
		
		#region Test Section

		#region Test Initialisation
		[SetUp]
		public void Init()
		{			
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestCoachFaresInterfaceInitialisation());					   						
		}

		[TearDown]
		public void CleanUp()
		{
			
		}

		#endregion

		#region Test methods
		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestInterfaceTypes()
		{			
			IFaresInterface faresInterface;
			const string ROUTE_OPERATOR_CODE = "O1";
			const string JOURNEY_OPERATOR_CODE = "O2";						

			//get the fares interface from the factory
			CoachFaresInterfaceFactory factory = new CoachFaresInterfaceFactory();//(CoachFaresInterfaceFactory)
			//	TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachFaresInterface];									
			faresInterface = factory.GetFaresInterface(JOURNEY_OPERATOR_CODE);
			Assert.IsTrue((faresInterface is FaresInterfaceForJourney),"Wrong object produced.");
			
			faresInterface = factory.GetFaresInterface(ROUTE_OPERATOR_CODE);
			Assert.IsTrue((faresInterface is FaresInterfaceForRoute),"Wrong object produced.");			
		}	
		#endregion

		#endregion
	
		#region IServiceFactory Members

		/// <summary>
		///  Method used by ServiceDiscovery to get an
		///  instance of the CoachFaresInterfaceFactory class.
		/// </summary>
		/// <returns>CoachFaresInterfaceFactory</returns>
		public object Get()
		{
			return this;
		} 

		#endregion

		#region ICoachFaresInterfaceFactory Members

		/// <summary>
		/// Returns the ICoachFaresInterface according to the operator code.
		/// </summary>
		/// <param name="operatorCode">Not used.</param>
		/// <returns>CoachFaresInterface</returns>
		public IFaresInterface GetFaresInterface(string operatorCode)
		{
			//get the interface type according to the provider	
			ICoachOperatorLookup coachOperatorLookup = (ICoachOperatorLookup)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];									
			CoachOperator coachOperator = coachOperatorLookup.GetOperatorDetails(operatorCode);
			return new TestMockFareInterface(coachOperator.InterfaceType);			
		}		

		/// <summary>
		/// Returns the ICoachFaresInterface according to the interface type.
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		public IFaresInterface GetFaresInterface(CoachFaresInterfaceType type)
		{
			return new TestMockFareInterface(type);			
		}		

		#endregion
	}
}
