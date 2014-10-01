// *********************************************** 
// NAME			: TestExternalLinks.cs
// AUTHOR		: R. Geraghty
// DATE CREATED	: 13/06/05
// DESCRIPTION	: Used for Testing the ExternalLinkService
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ExternalLinkService/Test/TestExternalLinks.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:28   mturner
//Initial revision.
//
//   Rev 1.6   Feb 02 2006 13:01:00   mtillett
//Fix unit test by making path relative
//
//   Rev 1.5   Nov 01 2005 15:12:18   build
//Automatically merged from branch for stream2638
//
//   Rev 1.4.1.0   Sep 23 2005 08:41:20   pcross
//Empty implementation of class to allow an external links service to be loaded by the test session manager. This allows references to the ExternalLinks class to be made (but it can't be used to get any results)
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Aug 19 2005 14:03:28   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.3.1.0   Jul 26 2005 10:08:30   pcross
//Updates for stream2572 (needed update to cope with relationships added to the ExternalLinks table in SQL)
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3   Jun 27 2005 14:29:42   rgeraghty
//Updated documentation comments
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.2   Jun 27 2005 12:33:32   rgeraghty
//Updated with code review comments
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.1   Jun 14 2005 19:00:16   rgeraghty
//Added IR Association
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.0   Jun 14 2005 18:59:50   rgeraghty
//Initial revision.

	
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.ExternalLinkService
{

	/// <summary>
	/// Test Harness for ExternalLinkService and collaborating classes
	/// </summary>
	[TestFixture]
	public class TestExternalLinks
	{

		#region private members

		/// <summary>
		/// Hashtable storing the URL links
		/// </summary>
		private Hashtable links = new Hashtable();

		/// <summary>
		/// Constant - name of file used for setting up initial test data
		/// </summary>
		private readonly string dataFile1 = Directory.GetCurrentDirectory() + @"\ExternalLinkService\ExternalLinksData1.xml";

		/// <summary>
		/// Constant - name of file used for updating test data
		/// </summary>
		private readonly string dataFile2 = Directory.GetCurrentDirectory() + @"\ExternalLinkService\ExternalLinksData2.xml";
		
		#endregion

		#region constructor
		/// <summary>
		/// Constructor. Does nothing.
		/// </summary>
		public TestExternalLinks()
		{
		}

		#endregion

		#region Test setup/teardown
		/// <summary>
		/// Initialises ServiceDiscovery
		/// </summary>
		[TestFixtureSetUp]
		public void Init()
		{
			// Clear down temp scripts folder
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestExternalLinksInitialization());

			ExternalLinksTestHelper.BackupCurrentData();

			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

			// Delete all data that may have come from test data xml files
			ExternalLinksTestHelper.ClearTableDown("ExternalLinks", helper, dataFile1);
			ExternalLinksTestHelper.ClearTableDown("ExternalLinks", helper, dataFile2);
			
			// Load data file
			ExternalLinksTestHelper.LoadDataFile(dataFile1);					
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TestFixtureTearDown]
		public void Teardown() 
		{ 
			ExternalLinksTestHelper.RestoreOriginalData(dataFile1, dataFile2);
		}

		#endregion
		
		#region helper methods
		/// <summary>
		/// Helper method. Adds the ExternalLinksService service to the TDServiceDiscovery cache.
		/// </summary>
		/// <returns>The newly created instance of ExternalLinksService</returns>
		private IExternalLinks AddExternalLinksService()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.ExternalLinkService, new ExternalLinksFactory());
			return (IExternalLinks)TDServiceDiscovery.Current[ServiceDiscoveryKey.ExternalLinkService];
		}

		/// <summary>
		/// Adds the mock DataChangeNotification service to the cache
		/// </summary>
		/// <returns>TestMockDataChangeNotification</returns>
		private TestMockDataChangeNotification AddDataChangeNotification()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());
			return (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
		}
		
		#endregion

		#region Tests
		/// <summary>
		/// Verifies the FindUrl method of the ExternalLinks class
		/// can find a specific url and behaves correctly if the url cannot be found 
		/// </summary>
		[Test]
		public void TestFindUrl()
		{
			
			IExternalLinks externalLinks = AddExternalLinksService();
			// Attempt to find non-existent url
			ExternalLinkDetail nonExistentUrl  = externalLinks.FindUrl("xxx");
			Assert.IsNull(nonExistentUrl, "Searching for non-existent url did not return null as expected");

			foreach (string externalLinkId in links.Keys)
			{
				ExternalLinkDetail linkEld = (ExternalLinkDetail) links[externalLinkId];
				// Check that this url can be found
				Assert.IsTrue(linkEld.IsValid ==true, "Url with id [" + externalLinkId + "] is not valid");
				ExternalLinkDetail eld = externalLinks.FindUrl( linkEld.Url );
				Assert.IsNotNull(eld, "Could not find external link with id [" + externalLinkId + "] in the ExternalLinks");
                
				Assert.AreEqual(linkEld.Url, eld.Url, "URL field not as expected for external link with Id [" + externalLinkId + "]");				
				Assert.AreEqual(linkEld.IsValid, eld.IsValid, "IsValid field not as expected for external link with Id [" + externalLinkId + "]");
								
			}	
		}

		/// <summary>
		/// Checks that data is reloaded successfully when the notification event fires.
		/// </summary>
		[Test]
		public void TestDataReload()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();
			IExternalLinks externalLinks = AddExternalLinksService();

			// Check values prior to change
			ExternalLinkDetail eld = externalLinks.FindUrl("www.channel4.com");			
			Assert.AreEqual("www.channel4.com", eld.Url, "URL field not as expected for external link");				
			Assert.AreEqual(false, eld.IsValid, "isValid field not as expected for external link");				

			ExternalLinkDetail eld2 = externalLinks.FindUrl("www.google.co.uk");			
			Assert.AreEqual("www.google.co.uk", eld2.Url, "URL field not as expected for external link");				
			Assert.AreEqual(false, eld2.IsValid, "isValid field not as expected for external link");				

			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

			// Delete all data that may have come from test data xml files
			ExternalLinksTestHelper.ClearTableDown("ExternalLinks", helper, dataFile1);
			ExternalLinksTestHelper.ClearTableDown("ExternalLinks", helper, dataFile2);

			ExternalLinksTestHelper.LoadDataFile(dataFile2);

			// Check that data hasn't changed too early
			ExternalLinkDetail eld3 = externalLinks.FindUrl("www.google.co.uk");
			Assert.AreEqual(eld2.Url, eld3.Url, "Data changed too early - URL for google has been updated too early");
			Assert.AreEqual(eld2.IsValid, eld3.IsValid, "Data changed too early - isValid value for google has been updated too early");

			ExternalLinkDetail eld4 = externalLinks.FindUrl("www.channel4.com");
			Assert.AreEqual(eld.Url, eld4.Url, "Data changed too early - URL for channel4 has been updated too early");
			Assert.AreEqual(eld.IsValid, eld4.IsValid, "Data changed too early - isValid value for channel4 has been updated too early");
				
			// Cause the Changed event to be raised by the notification service
			dataChangeNotification.RaiseChangedEvent("ExternalLinks");
			
			// Check that the data has changed
			ExternalLinkDetail eld5 = externalLinks.FindUrl("www.google.co.uk");
			Assert.IsNull(eld5,"Entry for Google should no longer exist");

			 //Check that the data has changed
			ExternalLinkDetail eld6 = externalLinks.FindUrl("www.channel4.com");
			Assert.IsNull(eld6,"Entry for Channel4 should no longer exist");
			
			ExternalLinkDetail eld7 = externalLinks.FindUrl("Updatedwww.channel4.com");
			Assert.AreEqual("Updatedwww.channel4.com", eld7.Url, "URL for channel4 has not been updated");				
			Assert.AreEqual(true, eld7.IsValid, "isValid setting for channel has not been updated");				
		}

		/// <summary>
		/// Tests the Load method of External Links which calls a stored procedure
		/// to retrieve all rows in the External Links table
		/// </summary>
		[Test]
		public void TestDataLoad()
		{
			ExternalLinks e = new ExternalLinks();
			e.Load();
		}

		/// <summary>
		/// Tests the AddExternalLink stored procedure - Insert
		/// </summary>
		[Test]
		public void TestInsertAddExternalLinkSP()
		{
			
			int rowsUpdated;
			SqlHelper sqlHelper = new SqlHelper();
			sqlHelper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

			// Used for insert of user surveys into database			
			Hashtable parameters = new Hashtable();	

			//create test data 
			parameters = ExternalLinksTestHelper.CreateTestExternalLinksData("BBC");
			
			//use stored procedure "AddExternalLink" to add external Link to database
			rowsUpdated = sqlHelper.Execute("AddExternalLink",parameters);

			//method returns true if one row has been successfully inserted
			Assert.IsTrue(rowsUpdated == 1,	"InsertTestData failure. Did not insert expected number of rows");
			
			sqlHelper.ConnClose();
		}

		/// <summary>
		/// Tests the AddExternalLink stored procedure - Update
		/// </summary>
		[Test]
		public void TestUpdateAddExternalLinkSP()
		{			
			int rowsUpdated;
			SqlHelper sqlHelper = new SqlHelper();
			sqlHelper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

			// Used for insert of user surveys into database			
			Hashtable parameters = new Hashtable();	

			//create test data 
			parameters = ExternalLinksTestHelper.CreateTestExternalLinksData("GOOGLE");
			rowsUpdated = sqlHelper.Execute("AddExternalLink",parameters);
						
			//method returns true if one row has been successfully updated
			Assert.IsTrue(rowsUpdated == 1,	"UpdateTestData failure. Did not update expected number of rows");
			sqlHelper.ConnClose();
		}
		#endregion

	}

	#region database helper class

	/// <summary>
	/// Test class used for managing External Links table data
	/// </summary>
	public sealed class ExternalLinksTestHelper
	{
		/// <summary>
		/// Constant - name of temporary table used when backing up table data
		/// </summary>
		private const string tempTablePrefix = "tempTestBackup";

		/// <summary>
		/// constructor
		/// </summary>
		private ExternalLinksTestHelper()
		{
		}

		/// <summary>
		/// Backs up the current data in the External Links table - static method
		/// </summary>
		public static void BackupCurrentData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

			BackupTable("ExternalLinks", helper);
		}

		/// <summary>
		/// Restores original data to External Links table - static method
		/// </summary>
		public static void RestoreOriginalData(string dataFile1, string dataFile2)
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

			
			ClearTableDown("ExternalLinks", helper, dataFile1);
			ClearTableDown("ExternalLinks", helper, dataFile2);

			RestoreFromBackup("ExternalLinks", helper, dataFile1, dataFile2);
			RemoveBackupTable("ExternalLinks", helper);
		}

		/// <summary>
		/// Drops the specified table.
		/// </summary>
		/// <param name="tableName">Name of the table to drop</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		private static void DropTable(string tableName, SqlHelper connectedHelper)
		{
			try
			{
				connectedHelper.Execute("drop table " + tableName);
			}
			catch (SqlException s)
			{
				// Allow a sql error with msg code 3701
				if (s.Number != 3701)
					throw;
			}
		}

		/// <summary>
		/// Backs up the specified table.
		/// </summary>
		/// <param name="tableName">Table to back up. Must not be temporary.</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		private static void BackupTable(string tableName, SqlHelper connectedHelper)
		{
			string tempTableName = tempTablePrefix + tableName;
			DropTable(tempTableName, connectedHelper);
			connectedHelper.Execute(String.Format(CultureInfo.InvariantCulture, "select * into {0} from {1}", tempTableName, tableName));
		}

		/// <summary>
		/// Restores the data
		/// </summary>
		/// <param name="tableName">Table to back up. Must not be temporary.</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		private static void RestoreFromBackup(string tableName, SqlHelper connectedHelper, string dataFile1, string dataFile2)
		{
			string tempTableName = tempTablePrefix + tableName;

			ClearTableDown(tableName, connectedHelper, dataFile1);
			ClearTableDown(tableName, connectedHelper, dataFile2);
			
			// Copy contents of temp table back to normal table. Ignore any data that is already there - actually only
			// test data should have been changed and that has just been deleted so this restore should be redundant
			try
			{
				connectedHelper.Execute(String.Format(CultureInfo.InvariantCulture, "insert into {0} select * from {1}", tableName, tempTableName));
			}
			catch (SqlException sqle)
			{
				switch (sqle.Number)
				{
					case 2627:
						// Duplicate insert error - ignore
						break;
					default:
						throw;
				}
			}
		}

		/// <summary>
		/// Deletes the contents of the specified table where the contents will be replaced by the contents
		/// of the test data xml files
		/// </summary>
		/// <param name="tableName">Table to delete from. Must not be temporary.</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		public static void ClearTableDown(string tableName, SqlHelper connectedHelper, string dataFile)
		{
			string deleteExternalLinks = "delete from externallinks where id = '{0}'";
			
			XmlDocument xmlData = new XmlDocument();
			xmlData.Load(dataFile);

			XmlNodeList currNodes;
			currNodes = xmlData.GetElementsByTagName("externallinks");
			foreach (XmlNode curr in currNodes)
				connectedHelper.Execute(String.Format(CultureInfo.InvariantCulture, deleteExternalLinks, AttributeValueForDatabase(curr, "id")));
		}

		/// <summary>
		/// Restores the data
		/// </summary>
		/// <param name="tableName">Table to back up. Must not be temporary.</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		private static void RemoveBackupTable(string tableName, SqlHelper connectedHelper)
		{
			string tempTableName = tempTablePrefix + tableName;
			DropTable(tempTableName, connectedHelper);
		}

		
		
		///<summary>
		/// method creates test external links data
		/// </summary>
		public static Hashtable CreateTestExternalLinksData(string id)
		{

			Hashtable testValues = new Hashtable();

			switch (id)
			{
				case "GOOGLE":

					//add value that is passed to this method - allows surveyEmailed flag to be set to true or false
					testValues.Add("@LinkId", id);
					//add standard test data	
					testValues.Add( "@URL", "UPDATED http://www.google.co.uk");						
					testValues.Add("@TestURL", "Updated testurl for google");	
					testValues.Add("@Description ", "Updated the url link to google website");	
					return testValues;
				
				case "BBC":

					//add value that is passed to this method - allows surveyEmailed flag to be set to true or false
					testValues.Add("@LinkId", id);
					//add standard test data	
					testValues.Add( "@URL", "http://www.bbc.co.uk/news");						
					testValues.Add("@TestURL", "testurl for bbc news");	
					testValues.Add("@Description ", "the url link to guardian website");	
					return testValues;

			}
			return null;

		}


		/// <summary>
		/// Formats the attribute values for inserting in the database
		/// </summary>
		/// <param name="node">XmlNode</param>
		/// <param name="attributeName">attribute name</param>
		/// <returns>formatted string</returns>
		private static string AttributeValueForDatabase(XmlNode node, string attributeName)
		{
			XmlAttribute attribute;
			attribute = node.Attributes[attributeName];
			if (attribute != null)
				return attribute.Value.Replace("'", "''");
			else
				return string.Empty;
		}

		/// <summary>
		/// Static method used to load data into the ExternalLinks table
		/// </summary>
		/// <param name="dataFile">String</param>
		/// <returns>True if data load succesful</returns>
		public static bool LoadDataFile(string dataFile)
		{
			XmlDocument xmlData = new XmlDocument();
			xmlData.Load(dataFile);

			//Data is in a straightforward format:
			//Rootnode is called "ExternalLinksTestData"
			//One type of child node:
			//     <externallinks id="" url="" testurl="" valid="" description ="">
			//The following SQL dumps the current tables into xml of this format
			//
			//	select '<externallinks id="' + id + '" url="' + url + '" testurl="'+ testurl + '" valid="' + cast(valid as bit) + '" description="' + description +  '"/>' from externallinks
			

			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);
			
			string insertExternalLinks = "insert into externallinks (id, url, testurl, valid, description) values ('{0}', '{1}', '{2}','{3}' ,'{4}' )";
			
			XmlNodeList currNodes;
			currNodes = xmlData.GetElementsByTagName("externallinks");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertExternalLinks, AttributeValueForDatabase(curr, "id"), AttributeValueForDatabase(curr, "url"),AttributeValueForDatabase(curr, "testurl") , AttributeValueForDatabase(curr, "valid"),AttributeValueForDatabase(curr, "description")));
			
			helper.ConnClose();

			return true;
						
		}

	}
	#endregion

	#region initialisation class

	/// <summary>
	/// Initialisation class 
	/// </summary>
	public class TestExternalLinksInitialization: IServiceInitialisation
	{

		/// <summary>
		/// Constructor
		/// </summary>
		public TestExternalLinksInitialization()
		{
		}

		/// <summary>
		/// Populates sevice cache with services needed 
		/// </summary>
		/// <param name="serviceCache">Cache to populate.</param>
		public void Populate(Hashtable serviceCache)
		{
			// Add cryptographic scheme
			serviceCache.Add(ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

			// Enable PropertyService					
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			
			// Enable logging service.
			ArrayList errors = new ArrayList();
			try
			{    
				IEventPublisher[] customPublishers = new IEventPublisher[0];

				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException)
			{
				foreach(string error in errors)
				{
					Console.WriteLine(error);
				}
				throw;
			}					

		}
		#endregion
	}


	#region
	/// <summary>
	/// Empty implementation of external links to allow tests that reference external links (but don't actually need data) to run.
	/// This will be loaded into the service cache by the test session manager initialisation.
	/// </summary>
	public class DummyExternalLinkService: IExternalLinks, IServiceFactory
	{
	
		public ExternalLinkDetail FindUrl (string url)
		{
			return null;
		}

		public ExternalLinkDetail this[ string key]
		{
			get
			{
				return null;
			}
		}

		public object Get()
		{
			return this;
		}
	}

	#endregion
}
