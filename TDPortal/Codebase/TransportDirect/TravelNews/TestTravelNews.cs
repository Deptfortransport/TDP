// *********************************************** 
// NAME                 : TravelNewsImport.cs 
// AUTHOR               : James Haydock
// DATE CREATED         : 06/10/2003 
// DESCRIPTION  : Test class for travel news functionality
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/TestTravelNews.cs-arc  $
//
//   Rev 1.2   Sep 02 2011 15:13:58   apatel
//Real time car unit tests update
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Sep 01 2011 10:44:00   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.0   Nov 08 2007 12:50:26   mturner
//Initial revision.
//
//   Rev 1.23   Mar 28 2006 16:21:28   AViitanen
//Unit test fixes
//
//   Rev 1.22   Mar 28 2006 11:08:58   build
//Automatically merged from branch for stream0024
//
//   Rev 1.21.1.2   Mar 10 2006 14:58:06   mdambrine
//wrong item was retrieved to do a check on. (testgridresultsfiltered)
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.21.1.1   Mar 10 2006 14:52:08   mdambrine
//Wrong number of items in the array for the test grid results filtered
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.21.1.0   Mar 08 2006 15:45:38   mdambrine
//Fixed the unit test to make them work with the new types of data
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.21   Feb 06 2006 16:51:02   mtillett
//Update data to fix unit tests
//
//   Rev 1.20   Feb 01 2006 14:54:00   mtillett
//Updated with correct XSD and fixed up XML to match. XSD found in the /Codebase/build/DataGatewayApps/Gateway folder
//
//   Rev 1.19   Feb 08 2005 13:41:18   RScott
//Assertion changed to assert
//
//   Rev 1.18   Dec 16 2004 15:25:54   passuied
//Refactoring the TravelNews component
//
//   Rev 1.17   Sep 06 2004 21:08:56   JHaydock
//Major update to travel news
//
//   Rev 1.16   Aug 18 2004 11:59:08   JHaydock
//Updated for new Travel News DB tables & SPs
//
//   Rev 1.15   Jul 20 2004 09:27:04   CHosegood
//Added recent delays combo box option and removed sort by ability on travelnews
//Resolution for 1168: Add 'recent delays' pulldown to travel news and remove the ability to sort headings
//
//   Rev 1.14   Jun 07 2004 14:16:04   CHosegood
//Now all tests pass datafeed name.
//
//   Rev 1.13   May 26 2004 10:22:48   jgeorge
//IR954 fix
//
//   Rev 1.12   Mar 29 2004 19:46:00   geaton
//Unit test refactoring.
//
//   Rev 1.11   Mar 18 2004 16:23:54   CHosegood
//Retrieves test xml file from properties instead of hardcoded
//
//   Rev 1.10   Mar 12 2004 17:44:54   jmorrissey
//Added TestDummyTravelNewsCheck method
//
//   Rev 1.9   Nov 17 2003 15:47:52   JMorrissey
//Fixed all problems with unit test, initialization plus properties.
//
//   Rev 1.8   Oct 28 2003 12:59:50   JHaydock
//Update to use new TravelNews import file and schema. Success code also set to be 0.
//
//   Rev 1.7   Oct 22 2003 12:07:34   JMorrissey
//updated comments
//
//   Rev 1.6   Oct 14 2003 18:10:16   JHaydock
//Update to TravelNews to allow selective display of data
//
//   Rev 1.5   Oct 14 2003 14:44:00   JMorrissey
//removed "Trace.Listeners.Remove("TDTraceListener");" from TearDown method.
//
//   Rev 1.4   Oct 14 2003 14:37:52   JMorrissey
//Added new methods
//
//   Rev 1.3   Oct 13 2003 10:33:06   JMorrissey
//Updated method signatures
//
//   Rev 1.2   Oct 10 2003 16:14:44   JMorrissey
//Added GetTravelHeadlines test
//
//   Rev 1.1   Oct 08 2003 17:20:58   JHaydock
//Completed first cut of travel news import class and test
//
//   Rev 1.0   Oct 06 2003 19:11:42   JHaydock
//Initial Revision

using NUnit.Framework;
using System;
using System.Data;
using System.Collections;
using System.IO;
using Logger = System.Diagnostics.Trace;
using System.Resources;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.TravelNews;
using System.Diagnostics;

using TransportDirect.UserPortal.TravelNewsInterface;


namespace TransportDirectNUnit
{
    /// <summary>
    /// Class to initialise services that are used by the tests.
    /// </summary>
    public class TestInitialization : IServiceInitialisation
    {

		public void Populate(Hashtable serviceCache)
        {
            // Enable PropertyService
            serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			
			// Enable logging service.
			ArrayList errors = new ArrayList();
            try
            {    
                IEventPublisher[] customPublishers = new IEventPublisher[0];
                Logger.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
            }
            catch (TDException tdEx)
            {
				foreach(string error in errors)
				{
					Console.WriteLine(error);
				}
				throw tdEx;
            }
        }
    }

    /// <summary>
    /// NUnit test class for test of travel news functionality.
    /// </summary>
    [TestFixture]
    public class TestTravelNews
	{
	
		// Data file paths and names.
		public string dataFileDirectory = Directory.GetCurrentDirectory() + @"\travelnews";
		public string validNewsFilename = "ValidTravelNews.xml";
		public string unavailableNewsFilename = "TravelNewsUnavailable.xml";
        
        /// <summary>
        /// 
        /// </summary>
        [SetUp]
        public void Init()
        {
			// Initialise services.
            TDServiceDiscovery.Init(new TestInitialization());			
		}

        /// <summary>
        /// 
        /// </summary>
		[TearDown]
		public void TearDown() {}

		/// <summary>
		/// Tests TravelNewsHandler indicates that travel news is unavailable
		/// following the load of travel data that indicates this situation.
		/// </summary>
		[Test]
		public void TestTravelNewsUnavailable()
		{
			// Import data file containing unavailable indicator.
			string datafilepath = String.Format("{0}\\{1}", this.dataFileDirectory, this.unavailableNewsFilename);

			try
			{
				TravelNewsImport importer = new TravelNewsImport(Properties.Current["datagateway.airbackend.travelnews.feedname"], null, null, null, null);
				Assert.IsTrue(importer.Run(datafilepath) == 0, "Failed to import unavailable indicator data file: " + datafilepath);

				try
				{
					TravelNewsHandler handler = new TravelNewsHandler();
					bool unavailableIndicators = handler.IsTravelNewsAvaliable;

					Assert.AreEqual(false, unavailableIndicators,
						"Travel news should be indicated as unavailable");
				}
				catch (TDException e2)
				{
					Assert.IsTrue(false,
						"Failure when calling handler to determine if news available: " + e2.Message);
				}
			}
			catch (TDException e1)
			{
				Assert.IsTrue(false,
					"Failure when importing valid travel news unavailable data file: " + datafilepath + " with exception: " + e1.Message);
			}
		}

		/// <summary>
		/// Tests that TravelNewsHandler is able to identify and return travel headlines.
		/// </summary>
		[Test]
		public void TestTravelHeadlines()
		{
			string datafilepath = String.Format("{0}\\{1}", this.dataFileDirectory, this.validNewsFilename);

			try
			{
				// Import valid data file which includes headlines.
				TravelNewsImport importer = new TravelNewsImport(Properties.Current["datagateway.airbackend.travelnews.feedname"], null, null, null, null);
				Assert.IsTrue(importer.Run(datafilepath) == 0,
					"Failed to import valid travel news data file: " + datafilepath);

				try
				{
					TravelNewsHandler handler = new TravelNewsHandler();
					HeadlineItem[] headlines = handler.GetHeadlines();

					// Find the headline
					bool headlineFound = false;
					foreach (object o in headlines)
					{
						Assert.IsTrue((o is HeadlineItem),
							"TravelHeadlines contained an unexpected type of object");
						headlineFound = headlineFound || ((HeadlineItem)o).HeadlineText == "STAFFORDSHIRE: M6 ROADWORKS BOTH WAYS";
					}

					Assert.IsTrue(headlineFound,
						"Unexpected headlines were returned by travel news handler.");
				}
				catch (TDException e2)
				{
					Assert.IsTrue(false,
						"Failure when getting headlines: " + e2.Message);
				}
			}
			catch (TDException e1)
			{
				Assert.IsTrue(false,
					"Failure when importing valid travel news data file: " + datafilepath + " with exception: " + e1.Message);
			}
		}

        /// <summary>
        /// Tests that TravelNewsImport class throws an exception if the
        /// incorrect datafeed name is passed.
        /// </summary>
        [Test]
        public void TestTravelNewsInvalidFeedname()
        {
            string datafilepath = String.Format("{0}\\{1}", this.dataFileDirectory, this.validNewsFilename);

            try
            {
                // Inavlid datafeed name
                TravelNewsImport importer = new TravelNewsImport(null, null, null, null, null);
                Assert.Fail("Expected invalid datafeed exception");
            } 
            catch (TDException e) 
            {
                Assert.AreEqual(TDExceptionIdentifier.DGUnexpectedFeedName, e.Identifier,
					"Expected invalid datafeed exception but got: " + e.Message);
            }
        }

		/// <summary>
		/// Tests that TravelNewsHandler is able to return the full set of travel news
		/// </summary>
		[Test]
		public void TestGridTravelNewsGridAll()
		{
			string datafilepath = String.Format("{0}\\{1}", this.dataFileDirectory, this.validNewsFilename);

			try
			{
				// Import valid data file which includes headlines.
				TravelNewsImport importer = new TravelNewsImport(Properties.Current["datagateway.airbackend.travelnews.feedname"], null, null, null, null);
				Assert.IsTrue(importer.Run(datafilepath) == 0,
					"Failed to import valid travel news data file: " + datafilepath);

				try
				{
					TravelNewsHandler handler = new TravelNewsHandler();
					TravelNewsState state = new TravelNewsState();
					state.SelectedDate = null;

					TravelNewsItem[] gridData = handler.GetDetails(state);

					Assert.IsTrue(gridData.Length == 9, "Incorrect number of travel news items in array list.");

					// Check that there the first entry in list contains correct data items.
					TravelNewsItem item = gridData[3];

                    Assert.IsTrue(item.Uid == "RTM3715508", 
						"Column UID from first entry invalid");
					Assert.IsTrue(item.SeverityLevel == SeverityLevel.Severe,
						"Column SeverityLevel from first entry invalid");
                    Assert.IsTrue(item.SeverityDescription == "Severe",
						"Column SeverityDescription from first entry invalid");
					Assert.IsTrue(item.PublicTransportOperator == string.Empty,
						"Column PublicTransportOperator from first entry invalid");
					Assert.IsTrue(item.Operator == "-",
						"Column Operator from first entry invalid");
					Assert.IsTrue(item.ModeOfTransport == "Road",
						"Column ModeOfTransport from first entry invalid");
                    Assert.IsTrue(item.Regions == "South East",
						"Column Regions from first entry invalid");
                    Assert.IsTrue(item.Location == "A31 Guildford, Surrey",
						"Column Location from first entry invalid");
                    Assert.IsTrue(item.RegionsLocation == "South East (A31 Guildford, Surrey)",
						"Column RegionsLocation from first entry invalid");
                    Assert.IsTrue(item.IncidentType == "Closures/ blockages",
						"Column IncidentType from first entry invalid");
                    Assert.IsTrue(item.HeadlineText == "A31 : Road closed at Guildford",
						"Column HeadlineText from first entry invalid");
                    Assert.IsTrue(item.DetailText == "Farnham Road closed both ways at the Hogs Back junction in Guildford, because of an accident, a van and a car involved.",
						"Column DetailText from first entry invalid");
					Assert.IsTrue(item.IncidentStatus == "O",
						"Column IncidentStatus from first entry invalid");
                    Assert.IsTrue(item.Easting == 496577,
						"Column Easting from first entry invalid");
                    Assert.IsTrue(item.Northing == 148614,
						"Column Northing from first entry invalid");
                    Assert.IsTrue(item.ReportedDateTime.ToString("dd/MM/yyyy HH:mm:ss") == "27/05/2010 15:08:43",
						"Column ReportedDateTime from first entry invalid");
                    Assert.IsTrue(item.StartDateTime.ToString("dd/MM/yyyy HH:mm:ss") == "27/05/2010 15:08:43",
						"Column StartDateTime from first entry invalid");
                    Assert.IsTrue(item.LastModifiedDateTime.ToString("dd/MM/yyyy HH:mm:ss") == "27/05/2010 15:54:01",
						"Column LastModifiedDateTime from first entry invalid");
                    Assert.IsTrue(item.ClearedDateTime == DateTime.MinValue,
						"Column ClearedDateTime from first entry invalid");
                    Assert.IsTrue(item.ExpiryDateTime.ToString("dd/MM/yyyy HH:mm:ss") == "27/05/2010 16:47:34",
						"Column ExpiryDateTime from first entry invalid");
				}
				catch (TDException e2)
				{
					Assert.IsTrue(false,
						"Failure when getting travel news in array list form: " + e2.Message);
				}
			}
			catch (TDException e1)
			{
				Assert.IsTrue(false,
					"Failure when importing valid travel news data file: " + datafilepath + " with exception: " + e1.Message);
			}
		}

		/// <summary>
		/// Tests that TravelNewsHandler is able to return a filtered set of travel news
		/// </summary>
		[Test]
		public void TestGridTravelNewsGridFiltered()
		{
			string datafilepath = String.Format("{0}\\{1}", this.dataFileDirectory, this.validNewsFilename);

			try
			{
				// Import valid data file which includes headlines.
				TravelNewsImport importer = new TravelNewsImport(Properties.Current["datagateway.airbackend.travelnews.feedname"], null, null, null, null);
				Assert.IsTrue(importer.Run(datafilepath) == 0,
					"Failed to import valid travel news data file: " + datafilepath);

				try
				{
					TravelNewsHandler handler = new TravelNewsHandler();
					TravelNewsState state = new TravelNewsState();
					state.SelectedDate = null;

					// filters out to get all news instead of major ones!
					state.SelectedDelays = DelayType.All;

					//Use state's default filtering
					TravelNewsItem[] gridData = handler.GetDetails(state);

					Assert.IsTrue(gridData.Length == 82, "Incorrect number of travel news items in array list.");

					// Check that there the first entry in list contains correct data items.
					TravelNewsItem item = gridData[3];

                    Assert.IsTrue(item.Uid == "RTM3715508",
                        "Column UID from first entry invalid");
                    Assert.IsTrue(item.SeverityLevel == SeverityLevel.Severe,
                        "Column SeverityLevel from first entry invalid");
                    Assert.IsTrue(item.SeverityDescription == "Severe",
                        "Column SeverityDescription from first entry invalid");
                    Assert.IsTrue(item.PublicTransportOperator == string.Empty,
                        "Column PublicTransportOperator from first entry invalid");
                    Assert.IsTrue(item.Operator == "-",
                        "Column Operator from first entry invalid");
                    Assert.IsTrue(item.ModeOfTransport == "Road",
                        "Column ModeOfTransport from first entry invalid");
                    Assert.IsTrue(item.Regions == "South East",
                        "Column Regions from first entry invalid");
                    Assert.IsTrue(item.Location == "A31 Guildford, Surrey",
                        "Column Location from first entry invalid");
                    Assert.IsTrue(item.RegionsLocation == "South East (A31 Guildford, Surrey)",
                        "Column RegionsLocation from first entry invalid");
                    Assert.IsTrue(item.IncidentType == "Closures/ blockages",
                        "Column IncidentType from first entry invalid");
                    Assert.IsTrue(item.HeadlineText == "A31 : Road closed at Guildford",
                        "Column HeadlineText from first entry invalid");
                    Assert.IsTrue(item.DetailText == "Farnham Road closed both ways at the Hogs Back junction in Guildford, because of an accident, a van and a car involved.",
                        "Column DetailText from first entry invalid");
                    Assert.IsTrue(item.IncidentStatus == "O",
                        "Column IncidentStatus from first entry invalid");
                    Assert.IsTrue(item.Easting == 496577,
                        "Column Easting from first entry invalid");
                    Assert.IsTrue(item.Northing == 148614,
                        "Column Northing from first entry invalid");
                    Assert.IsTrue(item.ReportedDateTime.ToString("dd/MM/yyyy HH:mm:ss") == "27/05/2010 15:08:43",
                        "Column ReportedDateTime from first entry invalid");
                    Assert.IsTrue(item.StartDateTime.ToString("dd/MM/yyyy HH:mm:ss") == "27/05/2010 15:08:43",
                        "Column StartDateTime from first entry invalid");
                    Assert.IsTrue(item.LastModifiedDateTime.ToString("dd/MM/yyyy HH:mm:ss") == "27/05/2010 15:54:01",
                        "Column LastModifiedDateTime from first entry invalid");
                    Assert.IsTrue(item.ClearedDateTime == DateTime.MinValue,
                        "Column ClearedDateTime from first entry invalid");
                    Assert.IsTrue(item.ExpiryDateTime.ToString("dd/MM/yyyy HH:mm:ss") == "27/05/2010 16:47:34",
                        "Column ExpiryDateTime from first entry invalid");
				}
				catch (TDException e2)
				{
					Assert.IsTrue(false,
						"Failure when getting travel news in array list form: " + e2.Message);
				}
			}
			catch (TDException e1)
			{
				Assert.IsTrue(false,
					"Failure when importing valid travel news data file: " + datafilepath + " with exception: " + e1.Message);
			}
		}

        /// <summary>
        /// Tests that TravelNewsHandler is able to identify and return travel headlines.
        /// </summary>
        [Test]
        public void TestGetTravelNewsByAffectedToids()
        {
            string datafilepath = String.Format("{0}\\{1}", this.dataFileDirectory, this.validNewsFilename);

            try
            {
                // Import valid data file which includes headlines.
                TravelNewsImport importer = new TravelNewsImport(Properties.Current["datagateway.airbackend.travelnews.feedname"], null, null, null, null);
                Assert.IsTrue(importer.Run(datafilepath) == 0,
                    "Failed to import valid travel news data file: " + datafilepath);

                try
                {
                    string[] toidlist = new string[] { "4000000007726447", "4000000007726449" };
                    TravelNewsHandler handler = new TravelNewsHandler();
                    TravelNewsItem[] tnItems = handler.GetTravelNewsByAffectedToids(toidlist);

                    Assert.IsTrue(tnItems.Length == 1,
                        "Unexpected travelnews items were returned by travel news handler.");

                    Assert.AreEqual("RTM3715714", tnItems[0].Uid);
                }
                catch (TDException e2)
                {
                    Assert.IsTrue(false,
                        "Failure when getting headlines: " + e2.Message);
                }
            }
            catch (TDException e1)
            {
                Assert.IsTrue(false,
                    "Failure when importing valid travel news data file: " + datafilepath + " with exception: " + e1.Message);
            }
        }

    }
}
