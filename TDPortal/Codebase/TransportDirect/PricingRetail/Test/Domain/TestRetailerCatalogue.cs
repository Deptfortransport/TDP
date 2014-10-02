// *********************************************** 
// NAME			: TestRetailerCatalogue.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 03/10/03
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/Domain/TestRetailerCatalogue.cs-arc  $
//
//   Rev 1.1   Feb 02 2009 16:57:50   mmodi
//Updated for Routing Guide
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:37:26   mturner
//Initial revision.
//
//   Rev 1.11   Mar 11 2005 11:40:42   jgeorge
//Updated to test against data in the database rather than hard coded values, and to check new properties

using System;
using System.Collections;
using System.Diagnostics;
using System.Data.SqlClient;

using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.DatabaseInfrastructure;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.DatabaseInfrastructure.Content;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
    /// <summary>
    /// Test Harness for RetailerCatalogue and collaborating classes
    /// </summary>
    [TestFixture]
    public class TestRetailerCatalogue
    {

        private static IList coachRetailerSCLNames = ArrayList.ReadOnly(new ArrayList(new string[]
                {"Go North East"}));
                
        private static IList coachRetailerCLNames = ArrayList.ReadOnly(new ArrayList(new string[]
                {"Go North East","National Express"}));
        
        private static IList coachRetailerFKNames = ArrayList.ReadOnly(new ArrayList(new string[]
                {"Go North East","National Express"}));
                
        private static IList coachRetailerALNames = ArrayList.ReadOnly(new ArrayList(new string[]
                {"National Express"}));
                
        private static IList allRailRetailerNames = ArrayList.ReadOnly(new ArrayList(new string[]
                {"Qjump","Go No Web","Go No Where","Trainline","Train UK"}));
                
		private const string sqlGetAllRetailers = "SELECT RetailerId, Name, WebsiteURL, HandoffURL, PhoneNumber, DisplayURL, IconURL, AllowsMTH, SmallIconUrl FROM Retailers";
		private const string sqlGetMappings = "SELECT OperatorCode, Mode, RetailerId FROM RetailerLookup";
		private SqlHelperDatabase testDB = SqlHelperDatabase.DefaultDB;

		private ArrayList retailers;
		private Hashtable retailerMappings;
		RetailerCatalogue retailerCatalogue;

		/// <summary>
		/// Constructor. Does nothing.
		/// </summary>
        public TestRetailerCatalogue()
        {
        }

		/// <summary>
		/// Initialises ServiceDiscovery
		/// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            TDServiceDiscovery.Init(new TestPricingRetailInitialisation());
			LoadData();
			retailerCatalogue = (RetailerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.RetailerCatalogue];
		}

		/// <summary>
		/// Resets ServiceDiscovery so that the next test can run.
		/// </summary>
        [TestFixtureTearDown]
        public void CleanUp()
        {
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
        }

        /// <summary>
        /// Verify equality and hashcode methods on RetailerKey class
        /// </summary>
        [Test]
        public void TestKeyEquality()
        {
            RetailerKey k1 = new RetailerKey(RetailerKey.IgnoreOperatorCode,ModeType.Rail);
            RetailerKey k2 = new RetailerKey(RetailerKey.IgnoreOperatorCode,ModeType.Rail);
            RetailerKey k3 = new RetailerKey("CL",ModeType.Coach);
            RetailerKey k4 = new RetailerKey("CL",ModeType.Coach);
            RetailerKey k5 = new RetailerKey("AB",ModeType.Coach);
            
            // Additionally check equal objects have same hashcode
            Assert.IsTrue(k1.Equals(k2) && k1.GetHashCode() == k2.GetHashCode());
            Assert.IsTrue(k3.Equals(k4) && k3.GetHashCode() == k4.GetHashCode());
            Assert.IsTrue(k2.Equals(k1));
            
            Assert.IsTrue(!(k4.Equals(k5)));
            Assert.IsTrue(!(k5.Equals(k4)));
            Assert.IsTrue(!(k1.Equals(k5)));
            Assert.IsTrue(!(k1.Equals("xxx")));
            
        }
        
        /// <summary>
        /// Verify hashcode method on RetailerKey class
        /// </summary>
        [Test]
        public void TestKeyHashCode()
        {
            RetailerKey k1 = new RetailerKey(RetailerKey.IgnoreOperatorCode,ModeType.Rail);
            RetailerKey k2 = new RetailerKey(RetailerKey.IgnoreOperatorCode,ModeType.Rail);
            RetailerKey k3 = new RetailerKey("CL",ModeType.Coach);
            RetailerKey k4 = new RetailerKey("CL",ModeType.Coach);
            RetailerKey k5 = new RetailerKey("AB",ModeType.Coach);

            Assert.IsTrue(k1.GetHashCode() == k2.GetHashCode());
            Assert.IsTrue(k3.GetHashCode() == k4.GetHashCode());

            // the following tests rely on the fact that a pefect hash code is produced 
            // by overriden GetHashCode()            
            Assert.IsTrue(k4.GetHashCode() != k5.GetHashCode());
            Assert.IsTrue(k1.GetHashCode() != k5.GetHashCode());
            
        }
        
        /// <summary>
        /// Verifies the FindRetailer method of the RetailerCatalogue class
        /// can find a specific retailer in the catalogue and behaves correctly
        /// if a retailer cannot be found 
        /// </summary>
        [Test]
        public void TestFindRetailer()
        {
			foreach (object[] currentRow in retailers)
			{
				string currentRowId = ((string)currentRow[0]).Trim();

				// Check that this retailer can be found in the catalogue
				Retailer currentRetailer = retailerCatalogue.FindRetailer( currentRowId );
				Assert.IsNotNull( currentRetailer, "Could not find retailer with id [" + currentRowId + "] in the Retailer Catalogue");

				// Check that the values match up
				// 0 = RetailerId (NN)
				// 1 = Name (NN)
				// 2 = WebsiteURL
				// 3 = HandoffURL
				// 4 = PhoneNumber
				// 5 = DisplayURL
				// 6 = IconURL
				// 7 = AllowsMTH (NN)
				// 8 = SmallIconUrl

				Assert.AreEqual( currentRowId, currentRetailer.Id, "Id field not as expected for retailer with Id [" + currentRowId + "]");
				Assert.AreEqual( ((string)currentRow[1]).Trim(), currentRetailer.Name, "Name field not as expected for retailer with Id [" + currentRowId + "]");

				if (currentRow[2] == DBNull.Value)
					Assert.AreEqual( string.Empty, currentRetailer.WebsiteUrl, "WebsiteUrl field not as expected for retailer with Id [" + currentRowId + "]");
				else
					Assert.AreEqual( ((string)currentRow[2]).Trim(), currentRetailer.WebsiteUrl, "WebsiteUrl field not as expected for retailer with Id [" + currentRowId + "]");

				if (currentRow[3] == DBNull.Value)
					Assert.AreEqual( string.Empty, currentRetailer.HandoffUrl, "HandoffURL field not as expected for retailer with Id [" + currentRowId + "]");
				else
					Assert.AreEqual( ((string)currentRow[3]).Trim(), currentRetailer.HandoffUrl, "HandoffURL field not as expected for retailer with Id [" + currentRowId + "]");

				Assert.AreEqual(currentRetailer.HandoffUrl != string.Empty, currentRetailer.isHandoffSupported, "isHandoffSupported field not as expected for retailer with Id [" + currentRowId + "]");

				if (currentRow[4] == DBNull.Value)
					Assert.AreEqual( string.Empty, currentRetailer.PhoneNumber, "PhoneNumber field not as expected for retailer with Id [" + currentRowId + "]");
				else
					Assert.AreEqual( ((string)currentRow[4]).Trim(), currentRetailer.PhoneNumber, "PhoneNumber field not as expected for retailer with Id [" + currentRowId + "]");

				if (currentRow[5] == DBNull.Value)
					Assert.AreEqual( string.Empty, currentRetailer.DisplayUrl, "DisplayURL field not as expected for retailer with Id [" + currentRowId + "]");
				else
					Assert.AreEqual( ((string)currentRow[5]).Trim(), currentRetailer.DisplayUrl, "DisplayURL field not as expected for retailer with Id [" + currentRowId + "]");

                if (currentRow[6] == DBNull.Value)
                    Assert.AreEqual(string.Empty, currentRetailer.IconUrl, "IconURL field not as expected for retailer with Id [" + currentRowId + "]");
                else
                {
                    string iconUrl = ImageUrlHelper.GetAlteredImageUrl(((string)currentRow[6]).Trim());

                    Assert.AreEqual(iconUrl, currentRetailer.IconUrl, "IconURL field not as expected for retailer with Id [" + currentRowId + "]");
                }

				Assert.AreEqual( (string)currentRow[7] == "Y", currentRetailer.AllowsMultipleTicketHandoff, "AllowsMultipleTicketHandoff field not as expected for retailer with Id [" + currentRowId + "]");

                if (currentRow[8] == DBNull.Value)
                    Assert.AreEqual(string.Empty, currentRetailer.SmallIconUrl, "SmallIconUrl field not as expected for retailer with Id [" + currentRowId + "]");
                else
                {
                    string smallIconUrl = ImageUrlHelper.GetAlteredImageUrl(((string)currentRow[8]).Trim());

                    Assert.AreEqual(smallIconUrl, currentRetailer.SmallIconUrl, "SmallIconUrl field not as expected for retailer with Id [" + currentRowId + "]");
                }

			}

            // Attempt to find non-existent retailer
            Retailer noRetailer = retailerCatalogue.FindRetailer("xxx");
            Assert.IsNull(noRetailer, "Searching for non-existant retailer did not return null as expected");

        }

        /// <summary>
        /// Verifies the FindRetailers method of the RetailerCatalogue class
        /// can find retailers for given operator code / mode combinations 
        /// and that it behaves correctly if no retailers are found.
        /// </summary>
        [Test]
        public void TestFindRetailers()
        {
			foreach ( string modeString in retailerMappings.Keys )
			{
				ModeType mode = (ModeType)Enum.Parse( typeof(ModeType), modeString, true );
				Hashtable currentModeHash = (Hashtable)retailerMappings[modeString];
				foreach ( string operatorCode in currentModeHash.Keys)
				{
					ArrayList currentRetailerList = (ArrayList)currentModeHash[operatorCode];

					// Get the retailer list for this mode and operator code from the catalogue
					IList retailers = retailerCatalogue.FindRetailers( operatorCode, mode );

					// Firstly, the IList should contain the same number of entries as the ArrayList
					Assert.AreEqual( currentRetailerList.Count, retailers.Count, "Unexpected number of retailers returned for Mode [" + modeString + "] and Operator Code [" + operatorCode + "]");

					// Now ensure that each item in the list of retailers is in the expected list
					foreach (Retailer r in retailers)
						Assert.IsTrue( currentRetailerList.Contains( r.Id ), "Unexpected retailer [" + r.Id + "] found in retailers list" );

					
				}
			}

			IList list;

            // Find retailers with no operator code and mode not rail
            list = retailerCatalogue.FindRetailers(null,ModeType.Coach);
            Assert.IsNull(list, "no retailers expected");

            // Find retailers with invalid operator code
            list = retailerCatalogue.FindRetailers("XX",ModeType.Coach);
            Assert.IsNull(list, "no retailers expected");

            // Find retailers with invalid mode
            list = retailerCatalogue.FindRetailers("FK",ModeType.Metro);
            Assert.IsNull(list,"no retailers expected");

            // Find retailers with invalid operator code and mode
            list = retailerCatalogue.FindRetailers("XX",ModeType.Metro);
            Assert.IsNull(list, "no retailers expected");
        }

		/// <summary>
		/// Loads the data directly from the database so that it can be compared against
		/// that returned by the retailer catalogue
		/// </summary>
		private void LoadData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(testDB);

			retailers = new ArrayList();
			retailerMappings = new Hashtable();

			SqlDataReader reader = helper.GetReader(sqlGetAllRetailers);

			while (reader.Read())
			{
				object[] currentRow = new object[9];
				reader.GetValues(currentRow);
				retailers.Add(currentRow);
			}

			reader.Close();

			reader = helper.GetReader(sqlGetMappings);

			while (reader.Read())
			{
				object[] currentRow = new object[3];
				reader.GetValues(currentRow);

				if (!retailerMappings.ContainsKey( ((string)currentRow[1]).Trim() ))
					retailerMappings.Add( ((string)currentRow[1]).Trim(), new Hashtable() );

				Hashtable currentModeHash = (Hashtable)retailerMappings[ ((string)currentRow[1]).Trim() ];

				if (!currentModeHash.ContainsKey( ((string)currentRow[0]).Trim() ))
					currentModeHash.Add( ((string)currentRow[0]).Trim(), new ArrayList() );

				ArrayList currentOperatorList = (ArrayList)currentModeHash[ ((string)currentRow[0]).Trim() ];

				currentOperatorList.Add( ((string)currentRow[2]).Trim() );
			}

			reader.Close();

			helper.ConnClose();
		}

	}
}
