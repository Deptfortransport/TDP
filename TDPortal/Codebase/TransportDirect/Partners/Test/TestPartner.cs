// *********************************************** 
// NAME			: TestPartner.cs
// AUTHOR		: Manuel Dambrine
// DATE CREATED	: 04/10/2005
// DESCRIPTION	: Class testing the funcationality of Partner
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Partners/Test/TestPartner.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:48   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:15:44   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Nov 25 2005 18:13:16   schand
//Test code changes for Password Column
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.2   Oct 13 2005 16:22:36   COwczarek
//Modify call to Partner constructor to pass channel.
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2869: Del8 White Labelling - Request URL Validation
//
//   Rev 1.1   Oct 07 2005 11:23:10   mdambrine
//FXcop changes
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.0   Oct 04 2005 15:33:34   mdambrine
//Initial revision.
//

using System;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using NUnit.Framework;

namespace TransportDirect.Partners
{
	/// <summary>
	/// Class testing the funcationality of Partners
	/// </summary>
	[TestFixture]
	[System.Runtime.InteropServices.ComVisible(false)]
	public class TestPartner
	{
		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestPartnerCatalogueInitialisation());
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() { }

		/// <summary>
		/// Creates an Partner and checks that the values supplied to the
		/// constructor are returned by the properties.
		/// </summary>
		[Test]
		public void TestCreate() 
		{ 
			int id = 5;
			string hostName = "partner";
			string name = "newpartner";
            string channel = "channel";
            string partnerPassword = "uwV6Rgwmsf8WDC5Zh2P7szt91g0OCqFzT74dkFJpSP3jZht1K8zOIeO7IkoU0DrgnLn2evkRQGp9pT2U2jjIKg==";
			Partner testPartner = null;

			try
			{
				testPartner = new Partner(id, hostName, name, channel, partnerPassword);
			}
			catch (Exception e)
			{
				Assert.Fail("An exception occurred when creating the Partner. Exception message follows. " + e.Message);
			}

			Assert.AreEqual(id, testPartner.Id, "ID property differs from expected");
			Assert.AreEqual(hostName, testPartner.HostName, "HostName property differs from expected");
			Assert.AreEqual(name, testPartner.Name, "Name property differs from expected");
		}
						
	}

}
