// *********************************************** 
// NAME                 : TestGISQuery.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 03/09/2003 
// DESCRIPTION  : Test for GISQuery class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestGISQuery.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:44   mturner
//Initial revision.
//
//   Rev 1.3   Apr 01 2004 12:09:42   geaton
//Unit test refactoring exercise.
//
//   Rev 1.2   Oct 02 2003 14:41:12   kcheung
//Updated to get working with del 5 build 2 dll
//
//   Rev 1.1   Sep 10 2003 11:09:04   passuied
//Changes after FxCop
//
//   Rev 1.0   Sep 05 2003 15:30:08   passuied
//Initial Revision

using System;
using NUnit.Framework;
using TransportDirect.UserPortal.LocationService;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Presentation.InteractiveMapping;


namespace TransportDirect.UserPortal.LocationService.UnitTest
{
	/// <summary>
	/// Summary description for TestGISQuery.
	/// </summary>
	[TestFixture]
	public class TestGisQuery
	{
		public TestGisQuery()
		{
		}
		[Test]
		public void TestFindNearestITNs()
		{
			GisQuery gis = new GisQuery();

			QuerySchema result = gis.FindNearestITNs(0,0);

			QuerySchema.ITNRow row = (QuerySchema.ITNRow) result.ITN.Rows[0];
			Assertion.AssertEquals("Test Failed","TOID1" , row.toid);
			Assertion.AssertEquals("Test Failed","Name1" , row.name);





		}

		[Test]
		public void TestFindNearestStops()
		{
			GisQuery gis = new GisQuery();

			QuerySchema result = gis.FindNearestStops(0,0,20);
			QuerySchema.StopsRow row = (QuerySchema.StopsRow) result.Stops.Rows[0];
			Assertion.AssertEquals("Test Failed", "ATCOCode1", row.atcocode);
			Assertion.AssertEquals("Test Failed", "NatGazID1", row.natgazid);
			Assertion.AssertEquals("Test Failed", "StopType1", row.stoptype);
			Assertion.AssertEquals("Test Failed", "CommonName1", row.commonname);
			Assertion.AssertEquals("Test Failed", "Street1", row.street);
			Assertion.AssertEquals("Test Failed", "Landmark1", row.landmark);
			Assertion.AssertEquals("Test Failed", 1234, row.X);
			Assertion.AssertEquals("Test Failed", 2345, row.Y);
			

		}

		[Test]
		public void TestNearestStopsAndITNs()
		{
			GisQuery gis = new GisQuery();

			QuerySchema result = gis.FindNearestStopsAndITNs(0, 0, 0);
			
			// Check you can get all ATCOcodes and all TOIDs 
			int i=1;
			foreach (QuerySchema.ITNRow row in result.ITN.Rows)
			{
				Assertion.AssertEquals("Test Failed", "TOID" +i.ToString(), row.toid);
				i++;
			}
			i=1;
			foreach (QuerySchema.StopsRow row in result.Stops.Rows)
			{
				Assertion.AssertEquals("Test Failed", "ATCOCode" +i.ToString(), row.atcocode);
				i++;

			}
															 
		}

		/// <remarks>
		/// Manual setup:
		/// 1) Build the GazopsWeb web service stub: (This will be used instead of the Gazetteer Web service provided on the development server: http://tri-tdp-iis/GazopsWeb/GazopsWeb.asmx
		/// - create a blank solution (called GazopsWeb) in \TDPortal\DEL5\TransportDirect\stubs
		/// - get the latest version of the project TDPortal\DEL5\TransportDirect\Stubs\GazopsWebTest
		/// - add the GazopsWebTest project to the solution created
		/// - build the solution
		/// - enter the following URL into the browser and verify that web methods are displayed:
		/// http://localhost/GazopsWebTest/GazopsWeb.asmx
		/// 
		/// 2) Build the Map class stub: (This will be used instead of the ESRI Map class.)
		/// - get the latest version of the solution TDPortal\DEL5\TransportDirect\stubs\td.interactivemapping\td.interactivemapping.sln
		/// - build the solution
		/// - Note that the mock Map class defines GIS Query data that is returned when clients call the mock GIS Query methods of the class.
		/// 
		/// 3) Change the value of the property with key name locationservice.gazopsweburl
		/// from: http://tri-tdp-iis/GazopsWeb/GazopsWeb.asmx
		/// to: http://localhost/GazopsWebTest/GazopsWeb.asmx
		/// 
		/// 4) Put the dll td.interactivemapping.dll built in step 2) into the directory:
		/// C:\TDPortal\DEL5\build\externals\
		/// 
		/// 5) Ensure that the test XML data files reside in the following location: (These data files are used by the mock GazopsWeb service to provide output data. ie the data files contain mock output data.)
		/// C:\Inetpub\wwwroot\GazopsWebTest\Xml\
		/// Within each xml file directory, the file called lookup.xml is used to map requests to the appropriate xml data file that should be used to service that request.
		/// </remarks>
		[Test]
		[Ignore("Manual setup required")]
		public void ManualSetup()
		{}
	}

	


}
