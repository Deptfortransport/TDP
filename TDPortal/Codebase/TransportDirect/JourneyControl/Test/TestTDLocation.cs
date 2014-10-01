// *********************************************** 
// NAME			: TestTDLocation.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestTDLocation class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestTDLocation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:22   mturner
//Initial revision.
//
//   Rev 1.1   Aug 20 2003 17:55:58   AToner
//Work in progress
using System;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.JourneyPlanning.CJPInterface;
using NUnit.Framework;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestTDLocation.
	/// </summary>
	[TestFixture]
	public class TestTDLocation
	{
		private TDLocation tdl;
		public TestTDLocation()
		{
		}
		/// <summary>
		/// Create a TDLocation object and check the gets against the sets
		/// </summary>
		[Test]
		[SetUp]
		public void TDLoction()
		{
			tdl = new TDLocation();
			tdl.Description = "My House";
			tdl.GridReference= new OSGridReference( 12, 13 );
			tdl.Locality = "Manchester";
			tdl.NapTAN = new string[1];
			tdl.NapTAN[0] = "NapTAN";
			tdl.CodingType = LocationCodingType.NapTAN;
			tdl.Toid = new string[1];
			tdl.Toid[0] = "A Toid";
			tdl.SearchType = SearchType.MainStationAirport;
			tdl.RequestPlaceType = RequestPlaceType.NaPTAN;

			Assertion.AssertEquals( "Description property", tdl.Description, "My House" );
			Assertion.AssertEquals( "Easting property", tdl.GridReference.Easting, 12 );
			Assertion.AssertEquals( "Northing property", tdl.GridReference.Northing, 13 );
			Assertion.AssertEquals( "Locality property", tdl.Locality, "Manchester" );
			Assertion.AssertEquals( "NapTAN property", tdl.NapTAN[0], "NapTAN" );
			Assertion.AssertEquals( "CodingType property", tdl.CodingType, LocationCodingType.NapTAN );
			Assertion.AssertEquals( "Toid property", tdl.Toid[0], "A Toid" );
			Assertion.AssertEquals( "SearchType property", tdl.SearchType, SearchType.MainStationAirport );
			Assertion.AssertEquals( "RequestPlace property", tdl.RequestPlaceType, RequestPlaceType.NaPTAN );
		}
		/// <summary>
		/// Turn the TDLocation object into a CJP RequestPlace object
		/// </summary>
		[Test]
		public void ToRequestPlace()
		{
			RequestPlace requestPlace = tdl.ToRequestPlace();
			//
			// Check the general properties of the RequestPlace match the TDLocation object
			Assertion.AssertEquals( "requestPlace.givenName property", requestPlace.givenName, tdl.Description );
			Assertion.AssertEquals( "requestPlace.locality property", requestPlace.locality, tdl.Locality );
			Assertion.AssertEquals( "requestPlace.type property", requestPlace.type, tdl.RequestPlaceType );
			//
			// Check the co-ordinates
			Assertion.AssertNotNull( "requestPlace.coordinate", requestPlace.coordinate );
			Assertion.AssertEquals( "requestPlace easting", requestPlace.coordinate.easting, tdl.GridReference.Easting );
			Assertion.AssertEquals( "requestPlace.northing", requestPlace.coordinate.northing, tdl.GridReference.Northing);
			//
			// As this TD Location is marked as a NapTAN so we should have a stops (NapTAN)
			// and not have roadPoints (Toid)
			Assertion.AssertNull( "requestPlace.roadPoints", requestPlace.roadPoints );
			Assertion.AssertNotNull( "requestPlace.stops", requestPlace.stops );
			//
			// Check the NapTAN details
			Assertion.AssertEquals( "requestPlace.stops[0].NaPTANID", requestPlace.stops[0].NaPTANID, tdl.NapTAN[0] );
		}
	}
}
