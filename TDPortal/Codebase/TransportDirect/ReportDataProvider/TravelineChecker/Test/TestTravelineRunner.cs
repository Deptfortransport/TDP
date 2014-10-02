// *********************************************** 
// NAME                 : TestTravelineRunner.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 16/11/2004 
// DESCRIPTION  : Test for TravelineRunner class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TravelineChecker/Test/TestTravelineRunner.cs-arc  $ 
//
//   Rev 1.1   Mar 16 2009 13:49:26   mturner
//Manual merge of stream5215
//
//   Rev 1.0.1.0   Feb 19 2009 15:44:16   mturner
//Changes to implement via proxy server functionality.
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:40:54   mturner
//Initial revision.
//
//   Rev 1.2   May 16 2005 18:41:10   NMoorhouse
//Added SetUp, Initialisation section
//
//   Rev 1.1   Feb 08 2005 13:26:06   RScott
//Assertions changed to Asserts
//
//   Rev 1.0   Nov 17 2004 11:49:10   passuied
//Initial revision.


using System;
using NUnit.Framework;

using TransportDirect.ReportDataProvider.TransactionHelper;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.ReportDataProvider.TravelineChecker.Test
{
	/// <summary>
	/// Test for TravelineRunner class
	/// </summary>
	[TestFixture]
	public class TestTravelineRunner
	{
		[SetUp]
		public void Init()
		{
			// Initialise services.
			TDServiceDiscovery.Init(new TestInitialisation());	
		}

		[Test]
		public void TestRun()
		{
			RequestTravelineParams rtp = new RequestTravelineParams();

			TDLocation origin = new TDLocation();
			origin.Description = "Clapham Junction (Rail)";
			origin.NaPTANs = new TDNaptan[]{ new TDNaptan("9100CLPHMJW", new OSGridReference(0,0))};

			TDLocation destination = new TDLocation();
			destination.Description = "London Victoria (Rail)";
			destination.NaPTANs = new TDNaptan[]{ new TDNaptan("9100VICTRIE", new OSGridReference(0,0))};

            Traveline[] travelines = new Traveline[] { new Traveline("http://157.203.42.230/scripts/xmlplanner.dll", true, "jwproxyserver:8080"), new Traveline("http://157.203.42.228:5000/jweb/nw", true, "jwproxyserver:8080") };

			rtp.OriginLocation = origin;
			rtp.DestinationLocation = destination;
			rtp.Travelines = travelines;

			string result = string.Empty;
			TravelineRunner.Run ( rtp, out result);

			Assert.IsTrue(result.Length != 0);
		}

	}
}
