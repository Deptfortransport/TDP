// *********************************************** 
// NAME			: TestPartnerFeatures.cs
// AUTHOR		: Manuel Dambrine
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Class testing the funcationality of TestPartnerFeatures
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Partners/Test/TestPartnerFeatures.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:48   mturner
//Initial revision.
//
//   Rev 1.2   Oct 07 2005 11:23:12   mdambrine
//FXcop changes
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.1   Oct 05 2005 09:24:44   mdambrine
//Made it work
//
//   Rev 1.0   Oct 04 2005 17:47:00   mdambrine
//Initial revision.
//

//

using System;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using NUnit.Framework;
using System.Data.SqlClient;

namespace TransportDirect.Partners
{
	/// <summary>
	/// Class testing the funcationality of Partners
	/// </summary>
	[TestFixture]
	[System.Runtime.InteropServices.ComVisible(false)]
	public class TestPartnerFeatures
	{
		private Hashtable TableProperties = new Hashtable();

		//PartnerCatalogue partnerCatalogue = new PartnerCatalogue();

		private const string sqlGetAllPartners = "SELECT PartnerId, HostName, PartnerName FROM Partner";	
		private SqlHelperDatabase testDB = SqlHelperDatabase.DefaultDB;

		private ArrayList Partners;	


		/// <summary>
		/// Constructor. Does nothing.
		/// </summary>
		public TestPartnerFeatures()
		{
		}

		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[TestFixtureSetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestPartnerCatalogueInitialisation());
			LoadData();
			LoadFeatures();
			//get the partnercatalogue
			//partnerCatalogue = (PartnerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.PartnerCatalogue];
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
		/// Creates an Partner and checks that the values supplied to the
		/// constructor are returned by the properties.
		/// </summary>
		[Test]
		public void TestContent() 
		{ 
			foreach (object[] currentRow in Partners)
			{
				int partnerId = ((int)currentRow[0]);

				Hashtable PartnerProperties = (Hashtable) TableProperties[partnerId];
				PartnerFeatures partnerFeatures = new PartnerFeatures(partnerId);

				Assert.AreEqual( PartnerProperties["FooterUrl"], partnerFeatures.FooterUrl, "FooterUrl field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsCityToCityEnabled"], partnerFeatures.IsCityToCityEnabled, "IsCityToCityEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["HeaderUrl"], partnerFeatures.HeaderUrl, "HeaderUrl field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsDoorToDoorEnabled"], partnerFeatures.IsDoorToDoorEnabled, "IsDoorToDoorEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsExtendEnabled"], partnerFeatures.IsExtendEnabled, "IsExtendEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsFindACarEnabled"], partnerFeatures.IsFindACarEnabled, "IsFindACarEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsFindACoachEnabled"], partnerFeatures.IsFindACoachEnabled, "IsFindACoachEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsFindAFlightEnabled"], partnerFeatures.IsFindAFlightEnabled, "IsFindAFlightEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsFindATrainEnabled"], partnerFeatures.IsFindATrainEnabled, "IsFindATrainEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsHomeEnabled"], partnerFeatures.IsHomeEnabled, "IsHomeEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");				
				Assert.AreEqual( PartnerProperties["IsLocationMapsEnabled"], partnerFeatures.IsLocationMapsEnabled, "IsLocationMapsEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");				
				Assert.AreEqual( PartnerProperties["IsMapSymbolsEnabled"], partnerFeatures.IsMapSymbolsEnabled, "IsMapSymbolsEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsMobileEnabled"], partnerFeatures.IsMobileEnabled, "IsMobileEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsNetworkMapsEnabled"], partnerFeatures.IsNetworkMapsEnabled, "IsNetworkMapsEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsPrivateTransportEnabled"], partnerFeatures.IsPrivateTransportEnabled, "IsPrivateTransportEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsPublicTransportEnabled"], partnerFeatures.IsPublicTransportEnabled, "IsPublicTransportEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");				
				Assert.AreEqual( PartnerProperties["IsStationAirportEnabled"], partnerFeatures.IsStationAirportEnabled, "IsStationAirportEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsTicketCostEnabled"], partnerFeatures.IsTicketCostEnabled, "IsTicketCostEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");
				Assert.AreEqual( PartnerProperties["IsTrafficMapsEnabled"], partnerFeatures.IsTrafficMapsEnabled, "IsTrafficMapsEnabled field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");			
				Assert.AreEqual( PartnerProperties["PrintableHeaderUrl"], partnerFeatures.PrintableHeaderUrl, "PrintableHeaderUrl field not as expected for Partnerfeatures class with partnerID[" + partnerId + "]");



			}
		}	
	
		private void LoadFeatures() 
		{
			foreach (object[] currentRow in Partners)
			{
				int partnerId = ((int)currentRow[0]);
				Hashtable PartnerProperties = new Hashtable();

				PartnerProperties.Add("HeaderUrl", Properties.Current["PartnerConfiguration.HeaderUrl", partnerId]); 				
				PartnerProperties.Add("PrintableHeaderUrl", Properties.Current["PartnerConfiguration.PrintableHeaderUrl", partnerId]);
				PartnerProperties.Add("FooterUrl", Properties.Current["PartnerConfiguration.FooterUrl", partnerId]);
				PartnerProperties.Add("IsLocationMapsEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsLocationMapsEnabled", partnerId]));
				PartnerProperties.Add("IsNetworkMapsEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsNetworkMapsEnabled", partnerId]));
				PartnerProperties.Add("IsTrafficMapsEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsTrafficMapsEnabled", partnerId]));
				PartnerProperties.Add("IsHomeEnabled" , Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsHomeEnabled", partnerId]));
				PartnerProperties.Add("IsFindATrainEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsFindATrainEnabled", partnerId]));
				PartnerProperties.Add("IsFindAFlightEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsFindAFlightEnabled", partnerId]));
				PartnerProperties.Add("IsFindACoachEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsFindACoachEnabled", partnerId]));
				PartnerProperties.Add("IsFindACarEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsFindACarEnabled", partnerId]));
				PartnerProperties.Add("IsCityToCityEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsCityToCityEnabled", partnerId]));
				PartnerProperties.Add("IsStationAirportEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsStationAirportEnabled", partnerId]));
				PartnerProperties.Add("IsDoorToDoorEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsDoorToDoorEnabled", partnerId]));
				PartnerProperties.Add("IsMobileEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsMobileEnabled", partnerId]));
				PartnerProperties.Add("IsTravelNewsEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsTravelNewsEnabled", partnerId]));
				PartnerProperties.Add("IsDepartureBoardsEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsDepartureBoardsEnabled", partnerId]));
				PartnerProperties.Add("IsPublicTransportEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsPublicTransportEnabled", partnerId]));
				PartnerProperties.Add("IsPrivateTransportEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsPrivateTransportEnabled", partnerId]));
				PartnerProperties.Add("IsExtendEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsExtendEnabled", partnerId]));
				PartnerProperties.Add("IsMapSymbolsEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsMapSymbolsEnabled", partnerId]));
				PartnerProperties.Add("IsTicketCostEnabled", Convert.ToBoolean(Properties.Current["PartnerConfiguration.Function.IsTicketCostEnabled", partnerId]));

				//add the partnerspecific properties to the 
				TableProperties.Add(partnerId, PartnerProperties);
			}
		}

		/// <summary>
		/// Loads the data directly from the database so that it can be compared against
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
