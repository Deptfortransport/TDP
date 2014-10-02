// *********************************************** 
// NAME			: TestPartnerCatalogue.cs
// AUTHOR		: Manuel Dambrine
// DATE CREATED	: 03/10/03
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Partners/Test/TestPartnerCatalogue.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:48   mturner
//Initial revision.
//
//   Rev 1.5   Feb 28 2006 10:56:56   AViitanen
//Post-merge fixes.
//
//   Rev 1.4   Feb 23 2006 19:15:44   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.1   Nov 28 2005 14:48:12   schand
//Added test method for GetClearPassword()
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.3.1.0   Nov 25 2005 18:14:14   schand
//Test code changes for Password Column
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.3   Oct 18 2005 17:53:28   mdambrine
//The resourcefile is build with the displayname and not the hostname
//
//   Rev 1.2   Oct 07 2005 11:23:10   mdambrine
//FXcop changes
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.1   Oct 05 2005 16:15:02   mdambrine
//Added a test for validateURL
//
//   Rev 1.0   Oct 04 2005 15:33:34   mdambrine
//Initial revision.

using System;
using System.Collections;
using System.Diagnostics;
using System.Data.SqlClient;

using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Text;
using System.Globalization;


using Logger = System.Diagnostics.Trace;

namespace TransportDirect.Partners
{
    /// <summary>
    /// Test Harness for PartnerCatalogue and collaborating classes
    /// </summary>
    [TestFixture]
	[System.Runtime.InteropServices.ComVisible(false)]
    public class TestPartnerCatalogue
    {
                        
		private const string sqlGetAllPartners = "SELECT PartnerId, HostName, PartnerName, ISNULL(PartnerPassword, '') as PartnerPassword FROM Partner";				
		private SqlHelperDatabase testDB = SqlHelperDatabase.DefaultDB;

		private ArrayList Partners;		
		PartnerCatalogue PartnerCatalogue;

		/// <summary>
		/// Constructor. Does nothing.
		/// </summary>
        public TestPartnerCatalogue()
        {
        }

		/// <summary>
		/// Initialises ServiceDiscovery
		/// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
			
            TDServiceDiscovery.Init(new TestPartnerCatalogueInitialisation());			
			LoadData();
			PartnerCatalogue = (PartnerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.PartnerCatalogue];
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
        /// Verifies the GetPartnerIdFromHostName method of the PartnerCatalogue class
        /// can find a specific Partner HostName in the catalogue and behaves correctly
        /// if a Partner cannot be found 
        /// </summary>
        [Test]
        public void TestGetPartnerIdFromHostName()
        {
			foreach (object[] currentRow in Partners)
			{
				string hostName = ((string)currentRow[1]).Trim().ToLower(new CultureInfo(string.Empty));

				// Check that this Partner can be found in the catalogue
				int partnerId = PartnerCatalogue.GetPartnerIdFromHostName( hostName );
				Assert.IsNotNull( partnerId, "Could not find Partner with hostname [" + hostName + "] in the Partner Catalogue");

				// Check that the values match up
				// 0 = ID (NN)
				// 1 = hostName (NN)
				// 2 = Name			

				Assert.AreEqual( currentRow[0], partnerId, "partnerId field not as expected for Partner with hostname [" + hostName + "]");
				
			}           

        }

		/// <summary>
		/// Verifies the GetPartnerHostName method of the PartnerCatalogue class
		/// can find a specific Partner HostName in the catalogue and behaves correctly
		/// if a Partner cannot be found 
		/// </summary>
		[Test]
		public void TestGetPartnerHostName()
		{
			foreach (object[] currentRow in Partners)
			{
				int partnerId = ((int)currentRow[0]);

				// Check that this Partner can be found in the catalogue
				string hostName = PartnerCatalogue.GetPartnerHostName( partnerId ).ToLower(new CultureInfo(string.Empty));
				Assert.IsNotNull( hostName, "Could not find Partner with hostname [" + partnerId + "] in the Partner Catalogue");

				// Check that the values match up
				// 0 = ID (NN)
				// 1 = hostName (NN)
				// 2 = Name			

				Assert.AreEqual( currentRow[1].ToString().ToLower(new CultureInfo(string.Empty)), hostName, "hostName field not as expected for Partner with ID [" + partnerId + "]");
				
			}           

		}

		/// <summary>
		/// Verifies the ValidateRequestUrl method of the PartnerCatalogue class
		/// checks if correctly checks if a hostname is the same as the channel 
		/// passed through
		/// </summary>
		[Test]
		public void TestValidateRequestUrl()
		{
			foreach (object[] currentRow in Partners)
			{
				int partnerId = ((int)currentRow[0]);
				
				// Check that this Partner can be found in the catalogue
				StringBuilder hostName = new StringBuilder();
				hostName.Append("partner/");
				hostName.Append(PartnerCatalogue.GetPartnerHostName( partnerId ));

				//the hostname passed is the same as the name just retrieved so it should pass
				//else fail
			
				if (PartnerCatalogue.ValidateRequestUrl(partnerId, hostName.ToString()) == false)
					Assert.Fail("The validate request method returned false on a valid string");

				//now test on a invalid hostname
				hostName.Append("wrong");

				if (PartnerCatalogue.ValidateRequestUrl(partnerId, hostName.ToString()) == true)
					Assert.Fail("The validate request method returned false on a valid string");
				
			}           

		}

		/// <summary>
		/// Verify the clear password from the database
		/// </summary>
		[Test]
		public void TestGetClearPassword()
		{
		  // serviceCache.Add (ServiceDiscoveryKey.PartnerCatalogue, new PartnerCatalogueFactory());
		  // PartnerCatalogue partnerCat = (PartnerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.PartnerCatalogue];

			string clearPassword = PartnerCatalogue.GetClearPassword("EnhancedExposedWebServiceTest"); 			

		   Assert.IsTrue(clearPassword == "EnhancedExposedWebServiceTest", "The catalogure returned wrong password");  

		}
       
		/// <summary>
		/// Loads the data directly from the database so that it can be compared against
		/// that returned by the Partner catalogue
		/// </summary>
		private void LoadData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(testDB);
            			
			Partners = new ArrayList();			

			SqlDataReader reader = helper.GetReader(sqlGetAllPartners);

			while (reader.Read())
			{
				object[] currentRow = new object[2];
				reader.GetValues(currentRow);
				Partners.Add(currentRow);
			}
			
			reader.Close();
            		
  
			helper.ConnClose();
		}

	}
}
