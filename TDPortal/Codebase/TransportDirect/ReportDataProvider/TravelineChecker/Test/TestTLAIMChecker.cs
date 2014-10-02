// *********************************************** 
// NAME                 : TestTLAIMChecker.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 15/11/2004 
// DESCRIPTION  : Test class for TLAIMChecker
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TravelineChecker/Test/TestTLAIMChecker.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:40:52   mturner
//Initial revision.
//
//   Rev 1.3   May 16 2005 18:40:24   NMoorhouse
//Added SetUp, Initialisation sections
//
//   Rev 1.2   Feb 07 2005 10:02:08   RScott
//Assertion changed to Assert
//
//   Rev 1.1   Nov 17 2004 11:27:20   passuied
//used real AIM Url for test...
//
//   Rev 1.0   Nov 15 2004 17:33:06   passuied
//Initial revision.


using System;
using NUnit.Framework;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;


namespace TransportDirect.ReportDataProvider.TravelineChecker.Test
{
	/// <summary>
	/// Test class for TLAIMChecker
	/// </summary>
	[TestFixture]
	public class TestTLAIMChecker
	{
		[SetUp]
		public void Init()
		{
			// Initialise services.
			TDServiceDiscovery.Init(new TestInitialisation());	
		}

		[Test]
		public void TestCheck()
		{
			TLAimChecker checker = new TLAimChecker();

			TDLocation origin = new TDLocation();
			origin.Description = "Clapham Junction (Rail)";
			origin.NaPTANs = new TDNaptan[]{ new TDNaptan("9100CLPHMJW", new OSGridReference(0,0))};

			TDLocation destination = new TDLocation();
			destination.Description = "London Victoria (Rail)";
			destination.NaPTANs = new TDNaptan[]{ new TDNaptan("9100VICTRIE", new OSGridReference(0,0))};
			
			checker.JourneyWebRequest = new TLJourneyWebRequest(origin, destination);

			checker.TravelineUrl = "http://157.203.42.228:5000/jweb/nw";

			Assert.IsTrue(checker.Check() >0 );
		}

		[Test]
		[ExpectedException(typeof(TDException))]
		public void TestBadUrlCheck()
		{
			TLAimChecker checker = new TLAimChecker();

			TDLocation origin = new TDLocation();
			origin.Description = "Clapham Junction (Rail)";
			origin.NaPTANs = new TDNaptan[]{ new TDNaptan("9100CLPHMJW", new OSGridReference(0,0))};

			TDLocation destination = new TDLocation();
			destination.Description = "London Victoria (Rail)";
			destination.NaPTANs = new TDNaptan[]{ new TDNaptan("9100VICTRIE", new OSGridReference(0,0))};
			
			checker.JourneyWebRequest = new TLJourneyWebRequest(origin, destination);

			checker.TravelineUrl = "http://193.168.0.1/scripts/xmlplanner.dll";

			checker.Check();
		}

		[Test]
		[ExpectedException(typeof (TDException))]		
		public void TestNotInitialisedCheck()
		{
			TLAimChecker checker = new TLAimChecker();

			TDLocation origin = new TDLocation();
			origin.Description = "Clapham Junction (Rail)";
			origin.NaPTANs = new TDNaptan[]{ new TDNaptan("9100CLPHMJW", new OSGridReference(0,0))};

			TDLocation destination = new TDLocation();
			destination.Description = "London Victoria (Rail)";
			destination.NaPTANs = new TDNaptan[]{ new TDNaptan("9100VICTRIE", new OSGridReference(0,0))};

			checker.Check();
		}
	}
}
