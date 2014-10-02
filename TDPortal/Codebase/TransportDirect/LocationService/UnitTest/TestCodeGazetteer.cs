// *********************************************** 
// NAME                 : TestCodeGazetteer.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 18/01/2005
// DESCRIPTION  : Test for TDCodeGazetteer class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestCodeGazetteer.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:44   mturner
//Initial revision.
//
//   Rev 1.4   Mar 28 2006 15:52:30   rgreenwood
//Newkirk: removed tests dependent on DataGateway/Manual setup/GazopsWebTest.asmx
//
//   Rev 1.3   Mar 08 2006 14:06:20   pcross
//Newkirk this test to be corrected at a later date
//
//   Rev 1.2   Mar 23 2005 11:55:30   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.1   Jan 19 2005 12:07:28   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.0   Jan 18 2005 17:45:08   passuied
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Test for TDCodeGazetteer class
	/// </summary>
	[TestFixture]
	[Ignore("NEWKIRK: Tests require access to GazopsWebTest.asmx")]
	public class TestCodeGazetteer
	{
		[TestFixtureSetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialisation());
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		[Test]
		public void TestFindCode()
		{
			TDCodeGazetteer cg; 
			TDCodeDetail[] details;
			// FindCode returns 1 CRS code
			cg = new TDCodeGazetteer();
			details = cg.FindCode("EUS");

			Assert.AreEqual (1, details.Length);
			Assert.AreEqual ("EUS", details[0].Code);
			Assert.AreEqual (TDCodeType.CRS, details[0].CodeType);
			Assert.AreEqual ("London Euston train station", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Rail, details[0].ModeType);
			Assert.AreEqual ("9100EUS", details[0].NaptanId);
			
			
			// FindCode returns 1 SMS code
			cg = new TDCodeGazetteer();
			details = cg.FindCode("WSXCRWHS");

			Assert.AreEqual (1, details.Length);
			Assert.AreEqual ("WSXCRWHS", details[0].Code);
			Assert.AreEqual (TDCodeType.SMS, details[0].CodeType);
			Assert.AreEqual ("West Sussex coach station", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Coach, details[0].ModeType);
			Assert.AreEqual ("9000WSX", details[0].NaptanId);

			// FindCode returns 1 Postcode
			cg = new TDCodeGazetteer();
			details = cg.FindCode("NW11 8TJ");

			Assert.AreEqual (1, details.Length);
			Assert.AreEqual ("NW11 8TJ", details[0].Code);
			Assert.AreEqual (TDCodeType.Postcode, details[0].CodeType);
			Assert.AreEqual ("NW11 8TJ", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual (string.Empty, details[0].Locality);
			Assert.AreEqual (string.Empty, details[0].Region);
			Assert.AreEqual (TDModeType.Undefined, details[0].ModeType);
			Assert.AreEqual (string.Empty, details[0].NaptanId);

			// FindCode returns 1 IATA code
			cg = new TDCodeGazetteer();
			details = cg.FindCode("LHR");

			Assert.AreEqual (1, details.Length);
			Assert.AreEqual ("LHR", details[0].Code);
			Assert.AreEqual (TDCodeType.IATA, details[0].CodeType);
			Assert.AreEqual ("London Heathrow airport", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Air, details[0].ModeType);
			Assert.AreEqual ("9200LHR", details[0].NaptanId);

			// FindCode returns many same CRS codes
			cg = new TDCodeGazetteer();
			details = cg.FindCode("KGX");

			Assert.AreEqual (3, details.Length);
			Assert.AreEqual ("KGX", details[0].Code);
			Assert.AreEqual (TDCodeType.CRS, details[0].CodeType);
			Assert.AreEqual ("London Kings Cross train station", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Rail, details[0].ModeType);
			Assert.AreEqual ("9100KGX1", details[0].NaptanId);

			Assert.AreEqual ("KGX", details[1].Code);
			Assert.AreEqual (TDCodeType.CRS, details[1].CodeType);
			Assert.AreEqual ("London Kings Cross train station", details[0].Description);
			Assert.AreEqual (123, details[1].Easting);
			Assert.AreEqual (456, details[1].Northing);
			Assert.AreEqual ("LOCALITY", details[1].Locality);
			Assert.AreEqual ("REGION", details[1].Region);
			Assert.AreEqual (TDModeType.Rail, details[1].ModeType);
			Assert.AreEqual ("9100KGX2", details[1].NaptanId);

			Assert.AreEqual ("KGX", details[2].Code);
			Assert.AreEqual (TDCodeType.CRS, details[2].CodeType);
			Assert.AreEqual ("London Kings Cross train station", details[2].Description);
			Assert.AreEqual (123, details[2].Easting);
			Assert.AreEqual (456, details[2].Northing);
			Assert.AreEqual ("LOCALITY", details[2].Locality);
			Assert.AreEqual ("REGION", details[2].Region);
			Assert.AreEqual (TDModeType.Rail, details[2].ModeType);
			Assert.AreEqual ("9100KGX3", details[2].NaptanId);
			

			// FindCode returns CRS codes + IATA code
			cg = new TDCodeGazetteer();
			details = cg.FindCode("LGW");

			Assert.AreEqual (3, details.Length);
			Assert.AreEqual ("LGW", details[0].Code);
			Assert.AreEqual (TDCodeType.CRS, details[0].CodeType);
			Assert.AreEqual ("Langwathby train station", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Rail, details[0].ModeType);
			Assert.AreEqual ("9100LGW1", details[0].NaptanId);

			Assert.AreEqual ("LGW", details[1].Code);
			Assert.AreEqual (TDCodeType.IATA, details[1].CodeType);
			Assert.AreEqual ("London Gatwick airport", details[1].Description);
			Assert.AreEqual (123, details[1].Easting);
			Assert.AreEqual (456, details[1].Northing);
			Assert.AreEqual ("LOCALITY", details[1].Locality);
			Assert.AreEqual ("REGION", details[1].Region);
			Assert.AreEqual (TDModeType.Air, details[1].ModeType);
			Assert.AreEqual ("9200LGW", details[1].NaptanId);

			Assert.AreEqual ("LGW", details[2].Code);
			Assert.AreEqual (TDCodeType.CRS, details[2].CodeType);
			Assert.AreEqual ("Langwathby train station", details[2].Description);
			Assert.AreEqual (123, details[2].Easting);
			Assert.AreEqual (456, details[2].Northing);
			Assert.AreEqual ("LOCALITY", details[2].Locality);
			Assert.AreEqual ("REGION", details[2].Region);
			Assert.AreEqual (TDModeType.Rail, details[2].ModeType);
			Assert.AreEqual ("9100LGW2", details[2].NaptanId);

			// FindCode returns nothing!
			cg = new TDCodeGazetteer();
			details = cg.FindCode("XXXXXX");

			Assert.AreEqual (0, details.Length);

		}

		[Test]
		[Ignore("ProjectNewkirk")]
		public void TestFindText()
		{
			TDCodeGazetteer cg; 
			TDCodeDetail[] details;
			
			// FindText return multiple CRS code
			cg = new TDCodeGazetteer();
			details = cg.FindText("kings cross", false, new TDModeType[]{TDModeType.Rail});

			Assert.AreEqual (3, details.Length);
			Assert.AreEqual ("KGX", details[0].Code);
			Assert.AreEqual (TDCodeType.CRS, details[0].CodeType);
			Assert.AreEqual ("London Kings Cross train station", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Rail, details[0].ModeType);
			Assert.AreEqual ("9100KGX1", details[0].NaptanId);

			Assert.AreEqual ("KGX", details[1].Code);
			Assert.AreEqual (TDCodeType.CRS, details[1].CodeType);
			Assert.AreEqual ("London Kings Cross train station", details[0].Description);
			Assert.AreEqual (123, details[1].Easting);
			Assert.AreEqual (456, details[1].Northing);
			Assert.AreEqual ("LOCALITY", details[1].Locality);
			Assert.AreEqual ("REGION", details[1].Region);
			Assert.AreEqual (TDModeType.Rail, details[1].ModeType);
			Assert.AreEqual ("9100KGX2", details[1].NaptanId);

			Assert.AreEqual ("KGX", details[2].Code);
			Assert.AreEqual (TDCodeType.CRS, details[2].CodeType);
			Assert.AreEqual ("London Kings Cross train station", details[2].Description);
			Assert.AreEqual (123, details[2].Easting);
			Assert.AreEqual (456, details[2].Northing);
			Assert.AreEqual ("LOCALITY", details[2].Locality);
			Assert.AreEqual ("REGION", details[2].Region);
			Assert.AreEqual (TDModeType.Rail, details[2].ModeType);
			Assert.AreEqual ("9100KGX3", details[2].NaptanId);

			// FindText returns 1 SMS code
			cg = new TDCodeGazetteer();
			details = cg.FindText("west sussex", false, new TDModeType[]{TDModeType.Coach});

			Assert.AreEqual (1, details.Length);
			Assert.AreEqual ("WSXCRWHS", details[0].Code);
			Assert.AreEqual (TDCodeType.SMS, details[0].CodeType);
			Assert.AreEqual ("West Sussex coach station", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Coach, details[0].ModeType);
			Assert.AreEqual ("9000WSX", details[0].NaptanId);

			// FindText returns 1 IATA code
			cg = new TDCodeGazetteer();
			details = cg.FindText("heathrow", false, new TDModeType[]{TDModeType.Air});

			Assert.AreEqual (1, details.Length);
			Assert.AreEqual ("LHR", details[0].Code);
			Assert.AreEqual (TDCodeType.IATA, details[0].CodeType);
			Assert.AreEqual ("London Heathrow airport", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Air, details[0].ModeType);
			Assert.AreEqual ("9200LHR", details[0].NaptanId);

			// FindText returns mix of CRS/IATA/SMS all modetypes selected
			cg = new TDCodeGazetteer();
			details = cg.FindText("gatwick", false, new TDModeType[]{TDModeType.Air, TDModeType.Coach, TDModeType.Bus, TDModeType.Rail});

			Assert.AreEqual (3, details.Length);
			Assert.AreEqual ("GTW", details[0].Code);
			Assert.AreEqual (TDCodeType.CRS, details[0].CodeType);
			Assert.AreEqual ("Gatwick Thameslink station", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Rail, details[0].ModeType);
			Assert.AreEqual ("9100GTWK", details[0].NaptanId);

			Assert.AreEqual ("LGW", details[1].Code);
			Assert.AreEqual (TDCodeType.IATA, details[1].CodeType);
			Assert.AreEqual ("London Gatwick airport", details[1].Description);
			Assert.AreEqual (123, details[1].Easting);
			Assert.AreEqual (456, details[1].Northing);
			Assert.AreEqual ("LOCALITY", details[1].Locality);
			Assert.AreEqual ("REGION", details[1].Region);
			Assert.AreEqual (TDModeType.Air, details[1].ModeType);
			Assert.AreEqual ("9200LGW", details[1].NaptanId);

			Assert.AreEqual ("GTWKGTWK", details[2].Code);
			Assert.AreEqual (TDCodeType.SMS, details[2].CodeType);
			Assert.AreEqual ("Gatwick coach station", details[2].Description);
			Assert.AreEqual (123, details[2].Easting);
			Assert.AreEqual (456, details[2].Northing);
			Assert.AreEqual ("LOCALITY", details[2].Locality);
			Assert.AreEqual ("REGION", details[2].Region);
			Assert.AreEqual (TDModeType.Coach, details[2].ModeType);
			Assert.AreEqual ("9000GTW", details[2].NaptanId);

			// FindText returns mix of CRS/IATA/SMS coach/rail selected
			cg = new TDCodeGazetteer();
			details = cg.FindText("gatwick", false, new TDModeType[]{ TDModeType.Coach, TDModeType.Bus, TDModeType.Rail});

			Assert.AreEqual (2, details.Length);
			Assert.AreEqual ("GTW", details[0].Code);
			Assert.AreEqual (TDCodeType.CRS, details[0].CodeType);
			Assert.AreEqual ("Gatwick Thameslink station", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Rail, details[0].ModeType);
			Assert.AreEqual ("9100GTWK", details[0].NaptanId);

			Assert.AreEqual ("GTWKGTWK", details[1].Code);
			Assert.AreEqual (TDCodeType.SMS, details[1].CodeType);
			Assert.AreEqual ("Gatwick coach station", details[1].Description);
			Assert.AreEqual (123, details[1].Easting);
			Assert.AreEqual (456, details[1].Northing);
			Assert.AreEqual ("LOCALITY", details[1].Locality);
			Assert.AreEqual ("REGION", details[1].Region);
			Assert.AreEqual (TDModeType.Coach, details[1].ModeType);
			Assert.AreEqual ("9000GTW", details[1].NaptanId);

			// FindText returns mix of CRS/IATA/SMS rail only selected
			cg = new TDCodeGazetteer();
			details = cg.FindText("gatwick", false, new TDModeType[]{ TDModeType.Rail});

			Assert.AreEqual (1, details.Length);
			Assert.AreEqual ("GTW", details[0].Code);
			Assert.AreEqual (TDCodeType.CRS, details[0].CodeType);
			Assert.AreEqual ("Gatwick Thameslink station", details[0].Description);
			Assert.AreEqual (123, details[0].Easting);
			Assert.AreEqual (456, details[0].Northing);
			Assert.AreEqual ("LOCALITY", details[0].Locality);
			Assert.AreEqual ("REGION", details[0].Region);
			Assert.AreEqual (TDModeType.Rail, details[0].ModeType);
			Assert.AreEqual ("9100GTWK", details[0].NaptanId);


			// FindText returns mix of CRS/IATA/SMS no modetypes selected
			cg = new TDCodeGazetteer();
			details = cg.FindText("gatwick", false, new TDModeType[0]);
			Assert.AreEqual (0, details.Length);


			// FindText returns nothing
			cg = new TDCodeGazetteer();
			details = cg.FindText("jfsaljf", false, new TDModeType[]{TDModeType.Air, TDModeType.Coach, TDModeType.Bus, TDModeType.Rail});
			Assert.AreEqual (0, details.Length);
		}

		/// <remarks>
		///
		/// Manual setup:
		/// 1) Build the GazopsWeb web service stub: (This will be used instead of the Gazetteer Web service provided on the development server: http://tri-tdp-iis/GazopsWeb/GazopsWeb.asmx
		/// - create a blank solution (called GazopsWeb) in \TDPortal\DEL5\TransportDirect\stubs
		/// - get the latest version of the project TDPortal\DEL5\TransportDirect\Stubs\GazopsWebTest
		/// - add the GazopsWebTest project to the solution created
		/// - build the solution
		/// - enter the following URL into the browser and verify that web methods are displayed:
		/// http://localhost/GazopsWebTest/GazopsWeb.asmx
		/// 
		/// 2) Check that the value of the property with key name locationservice.gazopsweburl
		/// is: http://localhost/GazopsWebTest/GazopsWeb.asmx
		/// 
		/// 3) Ensure that the test XML data files reside in the following location: 
		/// C:\Inetpub\wwwroot\GazopsWebTest\Xml\
		/// (These data files are used by the mock GazopsWeb service to provide output data. ie the data files contain mock output data.)
		/// Within each xml file directory, the file called lookup.xml is used to map requests to the appropriate xml data file that should be used to service that request.
		/// 
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}
	}
}
