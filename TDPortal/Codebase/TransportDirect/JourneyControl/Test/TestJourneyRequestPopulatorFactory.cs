// *********************************************** 
// NAME			: TestJourneyRequestPopulatorFacory.cs
// AUTHOR		: Richard Philpott
// DATE CREATED	: 2006-02-21
// DESCRIPTION	: NUnit tests for JourneyRequestPopulatorfactory 
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestJourneyRequestPopulatorFactory.cs-arc  $
//
//   Rev 1.1   Apr 02 2013 11:18:20   mmodi
//Unit test updates
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.0   Nov 08 2007 12:24:16   mturner
//Initial revision.
//
//   Rev 1.1   Feb 27 2006 12:20:08   RPhilpott
//Assign to IR 0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.0   Feb 27 2006 12:19:28   RPhilpott
//Initial revision.
//

using System;
using System.IO;
using System.Collections;
using System.Diagnostics;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestJourneyControl.
	/// </summary>
	[TestFixture]
	public class TestJourneyRequestPopulatorFactory
	{
		private DateTime outwardDateTime;
		private DateTime returnDateTime;
		private TestJourneyRequestData requestData;

		public TestJourneyRequestPopulatorFactory()
		{
		}

		#region "SetUp and TearDown Methods"

		[TestFixtureSetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestJRPInitialisation());			

			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.PropertyService, new MockProperties());
			
			Trace.Listeners.Remove("TDTraceListener");
		
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			outwardDateTime = DateTime.Now;
			returnDateTime = DateTime.Now + new TimeSpan(0, 1, 0, 0, 0);

			requestData = new TestJourneyRequestData(outwardDateTime, returnDateTime);

		}

		[TestFixtureTearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		#endregion


		[Test]
		public void TestFactory()
		{
			MockProperties props = (MockProperties)Properties.Current;

			props.SetCombinedAir(null);

			// test 1 - multimodal, rail/car/bus/coach
			ITDJourneyRequest request = requestData.InitialiseDefaultRequest(true);
			request.Modes = new ModeType[] {ModeType.Rail, ModeType.Car, ModeType.Bus, ModeType.Coach};
			JourneyRequestPopulator populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			Assert.IsTrue(populator is MultiModalJourneyRequestPopulator);

			// test 2 - multimodal, car only
			request.Modes = new ModeType[] { ModeType.Car };
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			Assert.IsTrue(populator is MultiModalJourneyRequestPopulator);

			// test 3 - multimodal, rail only
			request.Modes = new ModeType[] { ModeType.Rail };
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			Assert.IsTrue(populator is MultiModalJourneyRequestPopulator);

			// test 4 - trunk, rail, coach & air (no CityInterchanges)
			request = requestData.InitialiseDefaultTrunkRequest(true);
			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air };
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
            Assert.IsTrue(populator is CityToCityJourneyRequestPopulator);

			// test 5 - trunk, car only (no CityInterchanges)
			request = requestData.InitialiseDefaultTrunkRequest(true);
			request.Modes = new ModeType[] { ModeType.Car };
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			Assert.IsTrue(populator is MultiModalJourneyRequestPopulator);

			// test 6 - trunk, rail, coach & air, CityInterchanges included
			//          UseCombinedAir property not set 
			request = requestData.InitialiseDefaultCityToCityRequest(true);
			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air };
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
            Assert.IsTrue(populator is CityToCityJourneyRequestPopulator);

			// test 7 - trunk, rail, coach & air, CityInterchanges included
			//          UseCombinedAir property set to 'N' 
			props.SetCombinedAir("N");
			
			request = requestData.InitialiseDefaultCityToCityRequest(true);
			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air };
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
            Assert.IsTrue(populator is CityToCityJourneyRequestPopulator);

			// test 8 - trunk, rail, coach & air, CityInterchanges included
			//          UseCombinedAir property set to 'Y' 
			props.SetCombinedAir("Y");

			request = requestData.InitialiseDefaultCityToCityRequest(true);
			request.Modes = new ModeType[] { ModeType.Rail, ModeType.Coach, ModeType.Air };
			populator = JourneyRequestPopulatorFactory.GetPopulator(request);
			Assert.IsTrue(populator is CityToCityJourneyRequestPopulator);

		}
	}
}
