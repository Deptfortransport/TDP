//********************************************************************************
//NAME         : TestFaresInterfaceForRoute.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of TestFaresInterfaceForRoute class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFareInterfaces/TestFaresInterfaceForRoute.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:18   mturner
//Initial revision.
//
//   Rev 1.10   Nov 26 2005 11:46:14   mguney
//Date time in the past case test removed as this may cause failure in amend.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.9   Nov 25 2005 12:49:58   mguney
//FLOW OF EVENTS region included.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.8   Nov 01 2005 15:32:34   mguney
//faresInterfaceForRoute unused variable removed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7   Oct 25 2005 15:28:58   mguney
//CoachFaresInterfaceFactory is being initialised directly instead of getting it from the service discovery as the latter uses the test version.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.6   Oct 25 2005 11:13:02   mguney
//Initialisation changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Oct 22 2005 15:53:08   mguney
//Factories returned according to the operator code.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Oct 18 2005 09:16:38   mguney
//Time in the past case tested.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 13 2005 15:05:12   mguney
//Creation dare corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 12 2005 14:31:54   mguney
//Request validation changed to call the actual GetCoachFares method.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 12:27:14   mguney
//SCR associated
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 12:25:26   mguney
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Test class for fare interface for route based fares. (IF98)	
	/// </summary>
	[TestFixture]
	public class TestFaresInterfaceForRoute
	{		
		public TestFaresInterfaceForRoute()
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
			TDNaptan origin = new TDNaptan("369875",null);
			TDNaptan destination = new TDNaptan("369812",null);
			TDDateTime outwardStart = new TDDateTime(DateTime.Now);
			TDDateTime outwardEnd = new TDDateTime(DateTime.Now.AddHours(2));
			string operatorCode = "NX";
			TDDateTime returnStart = new TDDateTime(DateTime.Now);
			TDDateTime returnEnd = new TDDateTime(DateTime.Now.AddHours(2));

			fareRequest.OriginNaPTAN = origin;
			fareRequest.DestinationNaPTAN = destination;
			fareRequest.OutwardStartDateTime = outwardStart;
			fareRequest.OutwardEndDateTime = outwardEnd;
			fareRequest.OperatorCode = operatorCode;
			fareRequest.ReturnStartDateTime = returnStart;
			fareRequest.ReturnEndDateTime = returnEnd;
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
				(IFaresInterface)factory.GetFaresInterface(CoachFaresInterfaceType.ForRoute);
			
			//change the request and test			
			fareRequest.OperatorCode = String.Empty;
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Operator code blank case test failed.");
			ResetRequest(fareRequest);
						
			fareRequest.OriginNaPTAN = null;
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Origin naptan null case test failed.");
			ResetRequest(fareRequest);

			fareRequest.OriginNaPTAN = new TDNaptan(String.Empty,null);
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Origin naptan empty string case test failed.");
			ResetRequest(fareRequest);

			fareRequest.DestinationNaPTAN = null;
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Destination naptan null case test failed.");
			ResetRequest(fareRequest);

			fareRequest.DestinationNaPTAN = new TDNaptan(String.Empty,null);
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Destination naptan empty string case test failed.");
			ResetRequest(fareRequest);

			fareRequest.OutwardStartDateTime = DateTime.MinValue;
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Outward start date time unset case test failed.");
			ResetRequest(fareRequest);

			fareRequest.OutwardEndDateTime = DateTime.MinValue;
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Outward end date time unset case test failed.");
			ResetRequest(fareRequest);

			fareRequest.OutwardStartDateTime = DateTime.MinValue;
			fareRequest.OutwardEndDateTime = DateTime.MinValue;
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Outward start and end dates unset case test failed.");
			ResetRequest(fareRequest);

			fareRequest.OutwardStartDateTime = new TDDateTime(DateTime.Now.AddHours(2));
			fareRequest.OutwardEndDateTime = new TDDateTime(DateTime.Now);
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Outward start date time later than end date time case test failed.");
			ResetRequest(fareRequest);

			fareRequest.ReturnStartDateTime = DateTime.MinValue;			
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Return end date time set, start date time unset case test failed.");
			ResetRequest(fareRequest);

			fareRequest.ReturnEndDateTime = DateTime.MinValue;			
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Return start date time set, end date time unset case test failed.");
			ResetRequest(fareRequest);

			fareRequest.ReturnStartDateTime = new TDDateTime(DateTime.Now.AddHours(2));
			fareRequest.ReturnEndDateTime = new TDDateTime(DateTime.Now);
			fareResult = faresInterface.GetCoachFares(fareRequest);
			Assert.AreEqual(fareResult.ErrorStatus,FareErrorStatus.Error,"Return start date time later than end date time case test failed.");
			ResetRequest(fareRequest);			

			#region FLOW OF EVENTS
//			//THE CODE BELOW CAN BE USED TO TEST THE FLOW OF EVENTS WHEN NEEDED. 
//			//BUT IN NORMAL CIRCUMSTANCES THIS IS NOT NEEDED AS THE FUNCTIONALITY USED IS PROVIDED VIA PROVIDERS' WEB SERVICES.
//			fareRequest.OperatorCode = "O3";
//			fareRequest.OutwardStartDateTime = new TDDateTime(DateTime.Now.AddHours(1));			
//			fareRequest.OutwardEndDateTime = new TDDateTime(DateTime.Now.AddHours(10));			
//			fareRequest.OriginNaPTAN = new TDNaptan("12345",null);
//			fareRequest.DestinationNaPTAN = new TDNaptan("12346",null);
//			fareRequest.CjpRequestInfo = new TransportDirect.UserPortal.JourneyControl.CJPSessionInfo();
//			fareResult = faresInterface.GetCoachFares(fareRequest);
//			Assert.AreEqual(1,fareResult.Fares.Length,"Data expected. No results has returned.");
//			ResetRequest(fareRequest);
			#endregion

		}

		

		

		
	}
}