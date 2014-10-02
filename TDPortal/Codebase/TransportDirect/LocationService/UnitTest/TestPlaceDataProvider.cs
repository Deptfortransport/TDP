// *********************************************** 
// NAME                 : TestPlaceDataProvider
// AUTHOR               : Jonathan George
// DATE CREATED         : 29/10/2004
// DESCRIPTION  : Unit test for the PlaceDataProvider class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestPlaceDataProvider.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:50   mturner
//Initial revision.
//
//   Rev 1.5   Mar 30 2006 12:17:12   build
//Automatically merged from branch for stream0018
//
//   Rev 1.4.1.0   Feb 16 2006 16:30:10   mguney
//LoadTestData method changed to include the new table columns when inserting.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.4   Mar 23 2005 11:57:12   jgeorge
//Updates for new UseForFareEnquiries field on TDNaptan
//
//   Rev 1.3   Feb 08 2005 15:50:14   RScott
//assertions changed to asserts
//
//   Rev 1.2   Jan 12 2005 13:58:32   RScott
//Updated - Incorrect SQL steps altered ImportantPlace table has no seed value.
//
//   Rev 1.1   Nov 03 2004 10:12:18   jgeorge
//Interim check in
//
//   Rev 1.0   Nov 01 2004 15:47:06   jgeorge
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Unit test for the PlaceDataProvider class
	/// </summary>
	[TestFixture]
	public class TestPlaceDataProvider
	{
		public const string LOCATIONSERVICEPLACEDATAFILE = @".\TestLocationServicePlaceData.xml";

		#region Constructor, setup and teardown

		/// <summary>
		/// Test fixture initialisation. Sets up service discovery
		/// </summary>
		[TestFixtureSetUp]
		public void Init()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialisation());
		}

		/// <summary>
		/// Test fixture clear down. Resets service discovery
		/// </summary>
		[TestFixtureTearDown]
		public void ClearUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		#endregion

		#region Tests

		/// <summary>
		/// Tests that data is correctly loaded from a file
		/// </summary>
		[Test]
		public void TestWithFileLoading()
		{
			IPlaceDataProvider provider = new PlaceDataProvider(new FilePlaceDataLoader(LOCATIONSERVICEPLACEDATAFILE));

			// Verify that a LocationChoiceList is returned for cities
			LocationChoiceList cities = provider.GetPlaces(PlaceType.City);
			Assert.AreEqual(3, cities.Count,
				"Number of LocationChoices not as expected");

			// Check each location
			LocationChoice choice1 = (LocationChoice)cities[0];
			LocationChoice choice2 = (LocationChoice)cities[1];

			Assert.AreEqual("Coventry, West Midlands", choice1.Description,
				"Name of place 1 not as expected");
			Assert.AreEqual("London", choice2.Description,
				"Name of place 2 not as expected");

			Assert.AreEqual("N0073053", choice1.Locality,
				"Locality of place 1 not as expected");
			Assert.AreEqual("N0060403", choice2.Locality,
				"Locality of place 2 not as expected");

			Assert.AreEqual("1", choice1.PicklistValue,
				"Pick list value of place 1 not as expected");
			Assert.AreEqual("2", choice2.PicklistValue,
				"Pick list value of place 2 not as expected");

			// Check the TDLocation for each item
			TDLocation location1 = provider.GetPlace(choice1.PicklistValue);
			TDLocation location2 = provider.GetPlace(choice2.PicklistValue);

			Assert.AreEqual("Coventry, West Midlands", location1.Description,
				"Name of location 1 not as expected");
			Assert.AreEqual("London", location2.Description,
				"Name of location 2 not as expected");

			Assert.AreEqual(2, location1.NaPTANs.Length,
				"Number of naptans of location 1 not as expected");
			Assert.AreEqual(8, location2.NaPTANs.Length,
				"Number of naptans of location 2 not as expected");

			// Check the Naptans
			Assert.AreEqual("9100COVNTRY", location1.NaPTANs[0].Naptan,
				"Location 1, Naptan 0, Naptan not as expected");
			Assert.AreEqual(433200, location1.NaPTANs[0].GridReference.Easting,
				"Location 1, Naptan 0, OSGR.Easting not as expected");
			Assert.AreEqual(278200, location1.NaPTANs[0].GridReference.Northing,
				"Location 1, Naptan 0, OSGR.Northing not as expected");
			Assert.IsTrue(location1.NaPTANs[0].UseForFareEnquiries,
				"Location 1, Naptan 0, UseForFareEnquiries not as expected");

			Assert.AreEqual("9200BHX", location1.NaPTANs[1].Naptan,
				"Location 1, Naptan 1, Naptan not as expected");
			Assert.AreEqual(417950, location1.NaPTANs[1].GridReference.Easting,
				"Location 1, Naptan 1, OSGR.Easting not as expected");
			Assert.AreEqual(283950, location1.NaPTANs[1].GridReference.Northing,
				"Location 1, Naptan 1, OSGR.Northing not as expected");
			Assert.IsFalse(location1.NaPTANs[1].UseForFareEnquiries,
				"Location 1, Naptan 1, UseForFareEnquiries not as expected");

			Assert.AreEqual("9100CHRX", location2.NaPTANs[0].Naptan,
				"Location 2, Naptan 0, Naptan not as expected");
			Assert.AreEqual(530300, location2.NaPTANs[0].GridReference.Easting,
				"Location 2, Naptan 0, OSGR.Easting not as expected");
			Assert.AreEqual(180500, location2.NaPTANs[0].GridReference.Northing,
				"Location 2, Naptan 0, OSGR.Northing not as expected");
			Assert.IsTrue(location2.NaPTANs[0].UseForFareEnquiries,
				"Location 2, Naptan 0, UseForFareEnquiries not as expected");

			Assert.AreEqual("9100WATRLMN", location2.NaPTANs[1].Naptan,
				"Location 2, Naptan 1, Naptan not as expected");
			Assert.AreEqual(531200, location2.NaPTANs[1].GridReference.Easting,
				"Location 2, Naptan 1, OSGR.Easting not as expected");
			Assert.AreEqual(179800, location2.NaPTANs[1].GridReference.Northing,
				"Location 2, Naptan 1, OSGR.Northing not as expected");
			Assert.IsFalse(location2.NaPTANs[1].UseForFareEnquiries,
				"Location 2, Naptan 1, UseForFareEnquiries not as expected");

			Assert.AreEqual("9100VICTRIC", location2.NaPTANs[2].Naptan,
				"Location 2, Naptan 2, Naptan not as expected");
			Assert.AreEqual(528900, location2.NaPTANs[2].GridReference.Easting,
				"Location 2, Naptan 2, OSGR.Easting not as expected");
			Assert.AreEqual(179000, location2.NaPTANs[2].GridReference.Northing,
				"Location 2, Naptan 2, OSGR.Northing not as expected");
			Assert.IsFalse(location2.NaPTANs[2].UseForFareEnquiries,
				"Location 2, Naptan 2, UseForFareEnquiries not as expected");

			Assert.AreEqual("9100EUSTON", location2.NaPTANs[3].Naptan,
				"Location 2, Naptan 3, Naptan not as expected");
			Assert.AreEqual(529500, location2.NaPTANs[3].GridReference.Easting,
				"Location 2, Naptan 3, OSGR.Easting not as expected");
			Assert.AreEqual(182700, location2.NaPTANs[3].GridReference.Northing,
				"Location 2, Naptan 3, OSGR.Northing not as expected");
			Assert.IsFalse(location2.NaPTANs[3].UseForFareEnquiries,
				"Location 2, Naptan 3, UseForFareEnquiries not as expected");

			Assert.AreEqual("9100STPX", location2.NaPTANs[4].Naptan,
				"Location 2, Naptan 4, Naptan not as expected");
			Assert.AreEqual(530100, location2.NaPTANs[4].GridReference.Easting,
				"Location 2, Naptan 4, OSGR.Easting not as expected");
			Assert.AreEqual(182900, location2.NaPTANs[4].GridReference.Northing,
				"Location 2, Naptan 4, OSGR.Northing not as expected");
			Assert.IsFalse(location2.NaPTANs[4].UseForFareEnquiries,
				"Location 2, Naptan 4, UseForFareEnquiries not as expected");

			Assert.AreEqual("9200LHR", location2.NaPTANs[5].Naptan,
				"Location 2, Naptan 5, Naptan not as expected");
			Assert.AreEqual(507728, location2.NaPTANs[5].GridReference.Easting,
				"Location 2, Naptan 5, OSGR.Easting not as expected");
			Assert.AreEqual(176038, location2.NaPTANs[5].GridReference.Northing,
				"Location 2, Naptan 5, OSGR.Northing not as expected");
			Assert.IsTrue(location2.NaPTANs[5].UseForFareEnquiries,
				"Location 2, Naptan 5, UseForFareEnquiries not as expected");

			Assert.AreEqual("9200LGW", location2.NaPTANs[6].Naptan,
				"Location 2, Naptan 6, Naptan not as expected");
			Assert.AreEqual(527406, location2.NaPTANs[6].GridReference.Easting,
				"Location 2, Naptan 6, OSGR.Easting not as expected");
			Assert.AreEqual(141736, location2.NaPTANs[6].GridReference.Northing,
				"Location 2, Naptan 6, OSGR.Northing not as expected");
			Assert.IsTrue(location2.NaPTANs[6].UseForFareEnquiries,
				"Location 2, Naptan 6, UseForFareEnquiries not as expected");

			Assert.AreEqual("9200STN", location2.NaPTANs[7].Naptan,
				"Location 2, Naptan 7, Naptan not as expected");
			Assert.AreEqual(555650, location2.NaPTANs[7].GridReference.Easting,
				"Location 2, Naptan 7, OSGR.Easting not as expected");
			Assert.AreEqual(223750, location2.NaPTANs[7].GridReference.Northing,
				"Location 2, Naptan 7, OSGR.Northing not as expected");
			Assert.IsTrue(location2.NaPTANs[7].UseForFareEnquiries,
				"Location 2, Naptan 7, UseForFareEnquiries not as expected");

		}

		/// <summary>
		/// Tests that data is correctly loaded from the database
		/// </summary>
		[Test]
		public void TestWithDatabaseLoading()
		{			
			LoadTestData(SqlHelperDatabase.PlacesDB);

			try
			{
				IPlaceDataProvider provider = new PlaceDataProvider(new DatabasePlaceDataLoader(SqlHelperDatabase.PlacesDB ));
			
				// Verify that a LocationChoiceList is returned for cities
				LocationChoiceList cities = provider.GetPlaces(PlaceType.City);
				Assert.AreEqual(2, cities.Count, "Number of LocationChoices not as expected");

				// Check each location
				LocationChoice choice1 = (LocationChoice)cities[0];
				LocationChoice choice2 = (LocationChoice)cities[1];

				Assert.AreEqual("Coventry, West Midlands", choice1.Description,
					"Name of place 1 not as expected");
				Assert.AreEqual("London", choice2.Description,
					"Name of place 2 not as expected");

				Assert.AreEqual("N0073053", choice1.Locality,
					"Locality of place 1 not as expected");
				Assert.AreEqual("N0060403", choice2.Locality,
					"Locality of place 2 not as expected");

				Assert.AreEqual("1", choice1.PicklistValue,
					"Pick list value of place 1 not as expected");
				Assert.AreEqual("2", choice2.PicklistValue,
					"Pick list value of place 2 not as expected");

				// Check the TDLocation for each item
				TDLocation location1 = provider.GetPlace(choice1.PicklistValue);
				TDLocation location2 = provider.GetPlace(choice2.PicklistValue);

				Assert.AreEqual("Coventry, West Midlands", location1.Description,
					"Name of location 1 not as expected");
				Assert.AreEqual("London", location2.Description,
					"Name of location 2 not as expected");

				Assert.AreEqual(2, location1.NaPTANs.Length,
					"Number of naptans of location 1 not as expected");
				Assert.AreEqual(8, location2.NaPTANs.Length,
					"Number of naptans of location 2 not as expected");

				// Check the Naptans
				Assert.AreEqual("9100COVNTRY", location1.NaPTANs[0].Naptan,
					"Location 1, Naptan 0, Naptan not as expected");
				Assert.AreEqual(433200, location1.NaPTANs[0].GridReference.Easting,
					"Location 1, Naptan 0, OSGR.Easting not as expected");
				Assert.AreEqual(278200, location1.NaPTANs[0].GridReference.Northing,
					"Location 1, Naptan 0, OSGR.Northing not as expected");
				Assert.IsTrue(location1.NaPTANs[0].UseForFareEnquiries,
					"Location 1, Naptan 0, UseForFareEnquiries not as expected");

				Assert.AreEqual("9200BHX", location1.NaPTANs[1].Naptan,
					"Location 1, Naptan 1, Naptan not as expected");
				Assert.AreEqual(417950, location1.NaPTANs[1].GridReference.Easting,
					"Location 1, Naptan 1, OSGR.Easting not as expected");
				Assert.AreEqual(283950, location1.NaPTANs[1].GridReference.Northing,
					"Location 1, Naptan 1, OSGR.Northing not as expected");
				Assert.IsFalse(location1.NaPTANs[1].UseForFareEnquiries,
					"Location 1, Naptan 1, UseForFareEnquiries not as expected");

				Assert.AreEqual("9100CHRX", location2.NaPTANs[0].Naptan,
					"Location 2, Naptan 0, Naptan not as expected");
				Assert.AreEqual(530300, location2.NaPTANs[0].GridReference.Easting,
					"Location 2, Naptan 0, OSGR.Easting not as expected");
				Assert.AreEqual(180500, location2.NaPTANs[0].GridReference.Northing,
					"Location 2, Naptan 0, OSGR.Northing not as expected");
				Assert.IsTrue(location2.NaPTANs[0].UseForFareEnquiries,
					"Location 2, Naptan 0, UseForFareEnquiries not as expected");

				Assert.AreEqual("9100WATRLMN", location2.NaPTANs[4].Naptan,
					"Location 2, Naptan 4, Naptan not as expected");
				Assert.AreEqual(531200, location2.NaPTANs[4].GridReference.Easting,
					"Location 2, Naptan 4, OSGR.Easting not as expected");
				Assert.AreEqual(179800, location2.NaPTANs[4].GridReference.Northing,
					"Location 2, Naptan 4, OSGR.Northing not as expected");
				Assert.IsFalse(location2.NaPTANs[4].UseForFareEnquiries,
					"Location 2, Naptan 4, UseForFareEnquiries not as expected");

				Assert.AreEqual("9100VICTRIC", location2.NaPTANs[3].Naptan,
					"Location 2, Naptan 3, Naptan not as expected");
				Assert.AreEqual(528900, location2.NaPTANs[3].GridReference.Easting,
					"Location 2, Naptan 3, OSGR.Easting not as expected");
				Assert.AreEqual(179000, location2.NaPTANs[3].GridReference.Northing,
					"Location 2, Naptan 3, OSGR.Northing not as expected");
				Assert.IsFalse(location2.NaPTANs[3].UseForFareEnquiries,
					"Location 2, Naptan 3, UseForFareEnquiries not as expected");

				Assert.AreEqual("9100EUSTON", location2.NaPTANs[1].Naptan,
					"Location 2, Naptan 1, Naptan not as expected");
				Assert.AreEqual(529500, location2.NaPTANs[1].GridReference.Easting,
					"Location 2, Naptan 1, OSGR.Easting not as expected");
				Assert.AreEqual(182700, location2.NaPTANs[1].GridReference.Northing,
					"Location 2, Naptan 1, OSGR.Northing not as expected");
				Assert.IsFalse(location2.NaPTANs[1].UseForFareEnquiries,
					"Location 2, Naptan 4, UseForFareEnquiries not as expected");

				Assert.AreEqual("9100STPX", location2.NaPTANs[2].Naptan,
					"Location 2, Naptan 2, Naptan not as expected");
				Assert.AreEqual(530100, location2.NaPTANs[2].GridReference.Easting,
					"Location 2, Naptan 2, OSGR.Easting not as expected");
				Assert.AreEqual(182900, location2.NaPTANs[2].GridReference.Northing,
					"Location 2, Naptan 2, OSGR.Northing not as expected");
				Assert.IsFalse(location2.NaPTANs[2].UseForFareEnquiries,
					"Location 2, Naptan 2, UseForFareEnquiries not as expected");

				Assert.AreEqual("9200LHR", location2.NaPTANs[6].Naptan,
					"Location 2, Naptan 6, Naptan not as expected");
				Assert.AreEqual(507728, location2.NaPTANs[6].GridReference.Easting,
					"Location 2, Naptan 6, OSGR.Easting not as expected");
				Assert.AreEqual(176038, location2.NaPTANs[6].GridReference.Northing,
					"Location 2, Naptan 6, OSGR.Northing not as expected");
				Assert.IsTrue(location2.NaPTANs[6].UseForFareEnquiries,
					"Location 2, Naptan 6, UseForFareEnquiries not as expected");

				Assert.AreEqual("9200LGW", location2.NaPTANs[5].Naptan,
					"Location 2, Naptan 5, Naptan not as expected");
				Assert.AreEqual(527406, location2.NaPTANs[5].GridReference.Easting,
					"Location 2, Naptan 5, OSGR.Easting not as expected");
				Assert.AreEqual(141736, location2.NaPTANs[5].GridReference.Northing,
					"Location 2, Naptan 5, OSGR.Northing not as expected");
				Assert.IsTrue(location2.NaPTANs[5].UseForFareEnquiries,
					"Location 2, Naptan 5, UseForFareEnquiries not as expected");

				Assert.AreEqual("9200STN", location2.NaPTANs[7].Naptan,
					"Location 2, Naptan 7, Naptan not as expected");
				Assert.AreEqual(555650, location2.NaPTANs[7].GridReference.Easting,
					"Location 2, Naptan 7, OSGR.Easting not as expected");
				Assert.AreEqual(223750, location2.NaPTANs[7].GridReference.Northing,
					"Location 2, Naptan 7, OSGR.Northing not as expected");
				Assert.IsTrue(location2.NaPTANs[7].UseForFareEnquiries,
					"Location 2, Naptan 7, UseForFareEnquiries not as expected");
			}
			finally
			{
				RestoreOriginalData(SqlHelperDatabase.PlacesDB);
			}

		}

		#endregion

		#region Helper methods

		/// <summary>
		/// Loads test data into the database tables.
		/// </summary>
		/// <param name="placesDB">The database to load with data. Tables must already exist.</param>		
		private void LoadTestData(SqlHelperDatabase placesDB)
		{			
			SqlConnection con = new SqlConnection(Properties.Current[placesDB.ToString()]);
			SqlTransaction tran = null;

			try
			{
				con.Open();
				tran = con.BeginTransaction();

				SqlCommand com = new SqlCommand();
				com.Connection = con;
				com.CommandType = CommandType.Text;
				com.Transaction = tran;
				
				// First, backup existing data
				com.CommandText = "select * into testbackupImportantPlace from ImportantPlace";
				com.ExecuteNonQuery();
				com.CommandText = "select * into testbackupNaptanTimeRelationship from NaptanTimeRelationship";
				com.ExecuteNonQuery();

				// Delete old data
				com.CommandText = "delete NaptanTimeRelationship";
				com.ExecuteNonQuery();
				com.CommandText = "delete ImportantPlace";
				com.ExecuteNonQuery();

				string insertImportantPlace = "insert into ImportantPlace values ('{0}', '{1}', '{2}', '{3}')";
				string insertNaptanTimeRelationship = "insert into NaptanTimeRelationship " +
					"(TDNPGID,Naptan,Mode,OSGREasting,OSGRNorthing,UseForFareEnquiries) values " +
					"({0}, '{1}', '{2}', '{3}', '{4}', '{5}')";
					
				// Insert the new data
				com.CommandText = String.Format(insertImportantPlace, "1", "City", "Coventry, West Midlands", "N0073053");
				com.ExecuteNonQuery();

				com.CommandText = String.Format(insertNaptanTimeRelationship, 1, "9100COVNTRY", "Rail", "433200", "278200", "Y");
				com.ExecuteNonQuery();
				com.CommandText = String.Format(insertNaptanTimeRelationship, 1, "9200BHX", "Air", "417950", "283950", "N");
				com.ExecuteNonQuery();

				com.CommandText = String.Format(insertImportantPlace, "2", "City", "London", "N0060403");
				com.ExecuteNonQuery();

				com.CommandText = String.Format(insertNaptanTimeRelationship, 2, "9100CHRX", "Rail", "530300", "180500", "Y");
				com.ExecuteNonQuery();
				com.CommandText = String.Format(insertNaptanTimeRelationship, 2, "9100WATRLMN", "Rail", "531200", "179800", "N");
				com.ExecuteNonQuery();
				com.CommandText = String.Format(insertNaptanTimeRelationship, 2, "9100VICTRIC", "Rail", "528900", "179000", "N");
				com.ExecuteNonQuery();
				com.CommandText = String.Format(insertNaptanTimeRelationship, 2, "9100EUSTON", "Rail", "529500", "182700", "N");
				com.ExecuteNonQuery();
				com.CommandText = String.Format(insertNaptanTimeRelationship, 2, "9100STPX", "Rail", "530100", "182900", "N");
				com.ExecuteNonQuery();
				com.CommandText = String.Format(insertNaptanTimeRelationship, 2, "9200LHR", "Air", "507728", "176038", "Y");
				com.ExecuteNonQuery();
				com.CommandText = String.Format(insertNaptanTimeRelationship, 2, "9200LGW", "Air", "527406", "141736", "Y");
				com.ExecuteNonQuery();
				com.CommandText = String.Format(insertNaptanTimeRelationship, 2, "9200STN", "Air", "555650", "223750", "Y");
				com.ExecuteNonQuery();

			}
			catch ( Exception e )
			{
				try
				{
					if (tran != null)
						tran.Rollback();
					tran = null;
				}
				finally
				{
					throw e;
				}
			}
			finally
			{
				try
				{
					if (tran != null)
						tran.Commit();
					if ((con != null) && (con.State == ConnectionState.Open))
						con.Close();
				}
				finally
				{
					tran = null;
					con = null;
				}
			}
		}

		private void RestoreOriginalData(SqlHelperDatabase placesDB)
		{
			SqlConnection con = new SqlConnection(Properties.Current[placesDB.ToString()]);
			SqlTransaction tran = null;

			try
			{
				con.Open();
				tran = con.BeginTransaction();

                SqlCommand com = new SqlCommand();
				com.Connection = con;
				com.CommandType = CommandType.Text;
				com.Transaction = tran;
				
				// Delete test data
				com.CommandText = "delete NaptanTimeRelationship";
				com.ExecuteNonQuery();
				com.CommandText = "delete ImportantPlace";
				com.ExecuteNonQuery();
				
				// Restore original data
				com.CommandText = "insert into ImportantPlace (TDNPGID, PlaceType, PlaceName, Locality) select TDNPGID, PlaceType, PlaceName, Locality from testbackupImportantPlace";
				com.ExecuteNonQuery();
				com.CommandText = "insert into NaptanTimeRelationship select * from testbackupNaptanTimeRelationship";
				com.ExecuteNonQuery();

				// Drop temp tables
				com.CommandText = "drop table testbackupImportantPlace";
				com.ExecuteNonQuery();
				com.CommandText = "drop table testbackupNaptanTimeRelationship";
				com.ExecuteNonQuery();

			}
			catch ( Exception e )
			{
				if (tran != null)
					tran.Rollback();
				tran = null;
				throw e;
			}
			finally
			{
				if (tran != null)
					tran.Commit();
				if ((con != null) && (con.State == ConnectionState.Open))
					con.Close();
			}
		}

		#endregion
	}

}
