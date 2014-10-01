// ***********************************************
// NAME 		: TestDataServices.cs
// AUTHOR 		: Tushar Karsan
// DATE CREATED : 20-Aug-2003
// DESCRIPTION 	: Data Services.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/TestDataServices.cs-arc  $
//
//   Rev 1.2   Oct 14 2008 16:29:18   mturner
//Changes to remove compiler warnings
//
//   Rev 1.1   Mar 10 2008 15:16:06   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:20:48   mturner
//Initial revision.
//
//   Rev 1.22   Feb 09 2006 17:54:32   RWilby
//Updated for ProjectNewkirk
//
//   Rev 1.21   Aug 05 2005 16:00:42   jgeorge
//Updated tests to reduce the ties to matching all data in the dataservicetype enum. Now only one of each type is tested.
//
//   Rev 1.20   May 17 2005 10:56:50   rscott
//Changes for IR1936 - to get unit test working
//
//   Rev 1.19   Feb 24 2005 14:07:16   schand
//Added Lookup_Travelline_TravelNews_Region
//Nunit test is not working for AdultChildDrop as it needs data to be populated. For the time being it has been commented.
//
//   Rev 1.18   Feb 18 2005 14:44:02   tmollart
//Updated drops to match dataservices.
//
//   Rev 1.17   Feb 09 2005 11:53:50   RScott
//New drops added to test
//
//   Rev 1.16   Feb 08 2005 15:34:34   bflenk
//Changed Assertion to Assert
//
//   Rev 1.15   Jan 18 2005 09:04:54   RScott
//Updated to fix NUnit testing
//
//   Rev 1.14   Jun 15 2004 20:38:46   CHosegood
//Moved TestInitialisation into seperate cs file.
//
//   Rev 1.13   Mar 18 2004 20:24:24   CHosegood
//Fixed so it is now possible to run.  THIS BY NO MEANS TESTS THE ENTIRE FUNCTIONALITY OF DATASERVICES!!!
//
//   Rev 1.12   Mar 05 2004 16:53:02   COwczarek
//Replace call to obsolete method to remove compiler warning
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.11   Sep 16 2003 15:37:22   passuied
//latest version
//
//   Rev 1.10   Sep 16 2003 13:38:36   passuied
//make it compile
//
//   Rev 1.9   Sep 16 2003 12:23:08   passuied
//changes for integration in Web with ServiceDiscovery
//
//   Rev 1.8   Sep 02 2003 15:58:38   TKarsan
//Completed, with test harness.
//
//   Rev 1.7   Sep 02 2003 12:41:20   TKarsan
//Work in progress.
//
//   Rev 1.6   Sep 02 2003 11:18:02   TKarsan
//Work in progress
//
//   Rev 1.5   Sep 01 2003 17:42:00   TKarsan
//Work in progress
//
//   Rev 1.4   Aug 28 2003 17:39:44   TKarsan
//Work in progress
//
//   Rev 1.3   Aug 26 2003 16:02:28   TKarsan
//Work in progress
//
//   Rev 1.2   Aug 26 2003 15:27:34   TKarsan
//Work in progress
//
//   Rev 1.1   Aug 26 2003 11:30:54   TKarsan
//Work in progress
//
//   Rev 1.0   Aug 20 2003 16:19:36   TKarsan
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using NUnit.Framework;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Tests the DataServices class
	/// Note that at present, a large amount of functionality from this class is untested.
	/// </summary>
	[TestFixture]
	public class TestDataServices
	{
		/// <summary>
		/// Resource manager used for testing purposes
		/// </summary>
		private TDResourceManager testRM = null; 

		#region Constructor, Init & Teardown

		/// <summary>
		/// Constructor, not used.
		/// </summary>
		public TestDataServices()
		{

		}

		/// <summary>
		/// Initialise Service Discovery, Property and Event Logging Service.
		/// </summary>
		[TestFixtureSetUp]
		public void Init()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialization());
            testRM = new TestResourceManager();
		}

		#endregion

		/// <summary>
		/// Test cache loader and get methods.
		/// </summary>
        [Test]
		[Ignore("ProjectNewkirk")] 
        public void TestLoadAndGet()
        {
			DataServices ds = null;

			try
			{
				ds = new DataServices(testRM);
			}
			catch (Exception e)
			{
				Assert.Fail("Failed trying to create DataServices instance: " + e.Message);
			}

			//lists (type 1)
			try
			{
				IList list = (IList)ds.GetList(DataServiceType.NatExCodes);
				Assert.IsTrue( list.Count > 0, "NatExCodes list returned zero elements");
			}
			catch (Exception e)
			{
				Assert.Fail("Failed trying to retrieve a list for NatExCodes dataset: " + e.Message);
			}

			//hash (type 2) Currently unused so no test

			//dropdowns (type 3)
			DropDownList drop = new DropDownList();
			try
			{
				ds.LoadListControl( DataServiceType.FromToDrop, drop );
				Assert.IsTrue( drop.Items.Count > 0, "FromToDrop returned zero elements");
			}
			catch (Exception e)
			{
				Assert.Fail("Failed trying to populate a dropdown with FromToDrop dataset: " + e.Message);
			}

			//date (type 4)
			TDDateTime englishHoliday = new TDDateTime(2004, 04, 12);
			TDDateTime scottishHoliday = new TDDateTime(2004, 08, 02);
			TDDateTime bothHoliday = new TDDateTime(2004, 12, 25);

			try
			{
				Assert.IsTrue(ds.IsHoliday(DataServiceType.BankHolidays, englishHoliday, DataServiceCountries.EnglandWales), "IsHoliday returned incorrect value for England/Wales only holiday");
				Assert.IsFalse(ds.IsHoliday(DataServiceType.BankHolidays, englishHoliday, DataServiceCountries.Scotland), "IsHoliday returned incorrect value for England/Wales only holiday");
				Assert.IsFalse(ds.IsHoliday(DataServiceType.BankHolidays, scottishHoliday, DataServiceCountries.EnglandWales), "IsHoliday returned incorrect value for Scotland only holiday");
				Assert.IsTrue(ds.IsHoliday(DataServiceType.BankHolidays, scottishHoliday, DataServiceCountries.Scotland), "IsHoliday returned incorrect value for Scotland only holiday");
				Assert.IsTrue(ds.IsHoliday(DataServiceType.BankHolidays, bothHoliday, DataServiceCountries.EnglandWales), "IsHoliday returned incorrect value for England/Wales/Scotland holiday");
				Assert.IsTrue(ds.IsHoliday(DataServiceType.BankHolidays, bothHoliday, DataServiceCountries.Scotland), "IsHoliday returned incorrect value for England/Wales/Scotland holiday");
			}
			catch (Exception e)
			{
				Assert.Fail("Failed trying to test IsHoliday: " + e.Message);
			}


			//Categorised hash (type 5)
			try
			{
				CategorisedHashData data = ds.FindCategorisedHash(DataServiceType.DisplayableRailTickets, "AXS");
				Assert.IsNotNull(data, "Could not find expected value when calling FindCategorisedHash");
			}
			catch (Exception e)
			{
				Assert.Fail("Failed trying to test FindCategorisedHash: " + e.Message);
			}

        }

        /// <summary>
        /// Test DataServiceType.DataServiceTypeEnd is last in the enumeration
        /// </summary>
        [Test]
        public void TestDataServiceType()
        {
            Assert.AreEqual((int) DataServiceType.DataServiceTypeEnd,
                ( (Array) Enum.GetValues( typeof(DataServiceType) ) ).Length - 1, 
                "DataServiceType.DataServiceTypeEnd is not last element!!");
        }
	}

    public class TestResourceManager : TDResourceManager
    {
        public TestResourceManager() : base("")
        {
        }

        new public string GetString(string str) 
        {
            return str;
        }

        new public string GetString(string str, CultureInfo cult)
        {
            return str;
        }
    }
}
