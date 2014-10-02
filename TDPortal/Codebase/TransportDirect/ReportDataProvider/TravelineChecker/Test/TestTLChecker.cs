// *********************************************** 
// NAME                 : TestTLChecker
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 15/11/2004 
// DESCRIPTION  : Test class for TLChecker
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TravelineChecker/Test/TestTLChecker.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:40:52   mturner
//Initial revision.
//
//   Rev 1.2   May 16 2005 18:41:02   NMoorhouse
//Added SetUp, Initialisation section
//
//   Rev 1.1   Feb 08 2005 13:26:06   RScott
//Assertions changed to Asserts
//
//   Rev 1.0   Nov 15 2004 17:33:06   passuied
//Initial revision.


using System;
using NUnit.Framework;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.ReportDataProvider.TravelineChecker.Test
{
	/// <summary>
	/// Summary description for TestTLChecker.
	/// </summary>
	[TestFixture]
	public class TestTLChecker
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
			TLChecker checker = new TLChecker();

			TDLocation origin = new TDLocation();
			origin.Description = "Clapham Junction (Rail)";
			origin.NaPTANs = new TDNaptan[]{ new TDNaptan("9100CLPHMJW", new OSGridReference(0,0))};

			TDLocation destination = new TDLocation();
			destination.Description = "London Victoria (Rail)";
			destination.NaPTANs = new TDNaptan[]{ new TDNaptan("9100VICTRIE", new OSGridReference(0,0))};
			
			checker.JourneyWebRequest = new TLJourneyWebRequest(origin, destination);

			checker.TravelineUrl = "http://157.203.42.230/scripts/xmlplanner.dll";

			Assert.IsTrue (checker.Check() >0 );
		}

		[Test]
		[ExpectedException(typeof(TDException))]
		public void TestBadUrlCheck()
		{
			TLChecker checker = new TLChecker();

			TDLocation origin = new TDLocation();
			origin.Description = "Clapham Junction (Rail)";
			origin.NaPTANs = new TDNaptan[]{ new TDNaptan("9100CLPHMJW", new OSGridReference(0,0))};

			TDLocation destination = new TDLocation();
			destination.Description = "London Victoria (Rail)";
			destination.NaPTANs = new TDNaptan[]{ new TDNaptan("9100VICTRIE", new OSGridReference(0,0))};
			
			checker.JourneyWebRequest = new TLJourneyWebRequest(origin, destination);

			checker.TravelineUrl = "http://193.168.0.1/scripts/xmlplanner.dll";

			Assert.IsTrue (checker.Check() >0 );
		}

		[Test]
		[ExpectedException(typeof (TDException))]
		public void TestNotInitialisedCheck()
		{
			TLChecker checker = new TLChecker();

			TDLocation origin = new TDLocation();
			origin.Description = "Clapham Junction (Rail)";
			origin.NaPTANs = new TDNaptan[]{ new TDNaptan("9100CLPHMJW", new OSGridReference(0,0))};

			TDLocation destination = new TDLocation();
			destination.Description = "London Victoria (Rail)";
			destination.NaPTANs = new TDNaptan[]{ new TDNaptan("9100VICTRIE", new OSGridReference(0,0))};
			

			Assert.IsTrue(checker.Check() >0 );
		}

	}
}
