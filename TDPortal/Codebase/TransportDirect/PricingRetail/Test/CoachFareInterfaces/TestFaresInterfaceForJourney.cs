//********************************************************************************
//NAME         : TestFaresInterfaceForJourney.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of TestFaresInterfaceForJourney class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFareInterfaces/TestFaresInterfaceForJourney.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:18   mturner
//Initial revision.
//
//   Rev 1.10   Nov 29 2005 12:47:30   mguney
//Outward start date time in the past case test removed.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.9   Oct 31 2005 11:47:40   mguney
//FLOW OF EVENTS section changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.8   Oct 25 2005 15:28:52   mguney
//CoachFaresInterfaceFactory is being initialised directly instead of getting it from the service discovery as the latter uses the test version.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7   Oct 25 2005 11:13:02   mguney
//Initialisation changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.6   Oct 22 2005 15:53:08   mguney
//Factories returned according to the operator code.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Oct 19 2005 17:18:46   mguney
//Outward end datetime check omitted. It is not going to be used for IF3132
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.4   Oct 19 2005 09:40:10   mguney
//Code for TESTing THE FLOW OF EVENTS included.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 18 2005 09:16:38   mguney
//Time in the past case tested.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 15:05:06   mguney
//Creation dare corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 12:27:08   mguney
//SCR associated
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 12:25:22   mguney
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Test class for fare interface for journey based fares. (IF31/32)	
	/// </summary>
	[TestFixture]
	public class TestFaresInterfaceForJourney
	{		
		public TestFaresInterfaceForJourney()
		{			
		}

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

		/// <summary>
		/// Resets the fare request with initial values.
		/// </summary>
		/// <param name="fareRequest">Request</param>
		private void ResetRequest(FareRequest fareRequest)
		{
			TDNaptan origin = new TDNaptan("12345",null);
			TDNaptan destination = new TDNaptan("12346",null);
			DateTime outwardStart = DateTime.Now;
			DateTime outwardEnd = DateTime.Now.AddHours(2);

			fareRequest.OriginNaPTAN = origin;
			fareRequest.DestinationNaPTAN = destination;
			fareRequest.OutwardStartDateTime = outwardStart;
			fareRequest.OutwardEndDateTime = outwardEnd;
		}

		[Test]
		public void TestRequestValidation()
		{			
			//construct a normal request
			FareResult fareResult;
			FareRequest fareRequest = new FareRequest();
			ResetRequest(fareRequest);
			//get the fares interface from the factory
			CoachFaresInterfaceFactory factory = new CoachFaresInterfaceFactory();
				
			IFaresInterface faresInterface = 
				(IFaresInterface)factory.GetFaresInterface(CoachFaresInterfaceType.ForJourney);
			//change the request and test
			fareRequest.OriginNaPTAN = null;
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Origin naptan null case test failed.");
			ResetRequest(fareRequest);

			fareRequest.OriginNaPTAN = new TDNaptan(string.Empty,null);
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Origin naptan empty string case test failed.");
			ResetRequest(fareRequest);

			fareRequest.DestinationNaPTAN = null;
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Destination naptan null case test failed.");
			ResetRequest(fareRequest);

			fareRequest.DestinationNaPTAN = new TDNaptan(string.Empty,null);
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Destination naptan empty string case test failed.");
			ResetRequest(fareRequest);

			fareRequest.OutwardStartDateTime = DateTime.MinValue;
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Outward start date time unset case test failed.");
			ResetRequest(fareRequest);									
			
			#region FLOW OF EVENTS
//			//THE CODE BELOW CAN BE USED TO TEST THE FLOW OF EVENTS WHEN NEEDED. 
//			//BUT IN NORMAL CIRCUMSTANCES THIS IS NOT NEEDED AS THE FUNCTIONALITY USED IS PROVIDED VIA ATKINS DLL.
//			System.Runtime.Remoting.RemotingConfiguration.Configure(
//				@"c:\inetpub\wwwroot\TDRemotingHost\remoting.config");
//			fareRequest.OperatorCode = "SCL";
//			fareRequest.OutwardStartDateTime = new TDDateTime(DateTime.Now.AddHours(1));			
//			fareRequest.OutwardEndDateTime = new TDDateTime(DateTime.Now.AddHours(10));			
//			fareRequest.OriginNaPTAN = new TDNaptan("900067157",null);
//			fareRequest.DestinationNaPTAN = new TDNaptan("900090147",null);
//			fareRequest.CjpRequestInfo = new TransportDirect.UserPortal.JourneyControl.CJPSessionInfo();
//			fareResult = faresInterface.GetCoachFares(fareRequest);
//			Assert.AreEqual(23,fareResult.Fares.Length,"Data expected. No results has returned.");
//			ResetRequest(fareRequest);
			#endregion

		}

		

		
	}
}