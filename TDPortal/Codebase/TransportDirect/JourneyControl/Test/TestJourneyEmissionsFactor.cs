// *********************************************** 
// NAME			: TestJourneyEmissionsFactor.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/02/07
// DESCRIPTION	: Class testing the funcationality of JourneyEmissionsFactor
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestJourneyEmissionsFactor.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:14   mturner
//Initial revision.
//
//   Rev 1.0   Feb 27 2007 09:59:12   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
//

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
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestJourneyEmissionsFactor.
	/// </summary>
	[TestFixture]
	[CLSCompliant(false)]
	public class TestJourneyEmissionsFactor
	{
		private const string dataFile1 = @".\Test\testJourneyEmissionsFactor1.xml";
		private const string dataFile2 = @".\Test\testJourneyEmissionsFactor2.xml";

		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init() 
		{ 
			// Clear down temp scripts folder
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new JourneyEmissionsFactorTestInitialisation());

			JourneyEmissionsFactorTestHelper.BackupCurrentData();
			JourneyEmissionsFactorTestHelper.LoadDataFile(dataFile1);
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{ 
			JourneyEmissionsFactorTestHelper.RestoreOriginalData();
		}


		#region Helper methods

		/// <summary>
		/// Helper method. Adds the JourneyEmissionsFactor service to the TDServiceDiscovery cache.
		/// </summary>
		/// <returns>The newly created instance of CarCostCalculator</returns>
		private JourneyEmissionsFactor AddJourneyEmissionsFactor()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.JourneyEmissionsFactor, new JourneyEmissionsFactorFactory());
			return (JourneyEmissionsFactor)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyEmissionsFactor];
		}

		/// <summary>
		/// Adds the mock DataChangeNotification service to the cache
		/// </summary>
		/// <returns></returns>
		private TestMockDataChangeNotification AddDataChangeNotification()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.DataChangeNotification, new TestMockDataChangeNotification());
			return (TestMockDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
		}

		#endregion


		/// <summary>
		/// Checks that data retrieval methods return the correct values after the JourneyEmissionsFactor is created.
		/// </summary>
		[Test]
		public void TestGetDataMethods()
		{
			JourneyEmissionsFactor journeyEmissionsData = AddJourneyEmissionsFactor();

			Assert.AreEqual(0.010, journeyEmissionsData.GetEmissionFactor("AirDefault"), "Emission factor for AirDefault is incorrect");
			Assert.AreEqual(0.020, journeyEmissionsData.GetEmissionFactor("BusDefault"), "Emission factor for BusDefault is incorrect");
			Assert.AreEqual(0.030, journeyEmissionsData.GetEmissionFactor("CoachDefault"), "Emission factor for CoachDefault is incorrect");
			Assert.AreEqual(0.040, journeyEmissionsData.GetEmissionFactor("LightRailDefault"), "Emission factor for LightRailDefault is incorrect");
			Assert.AreEqual(0.050, journeyEmissionsData.GetEmissionFactor("RailDefault"), "Emission factor for RailDefault is incorrect");
			Assert.AreEqual(0.060, journeyEmissionsData.GetEmissionFactor("CarDefault"), "Emission factor for CarDefault is incorrect");
			Assert.AreEqual(0.070, journeyEmissionsData.GetEmissionFactor("FerryDefault"), "Emission factor for FerryDefault is incorrect");

			Assert.AreEqual("LU", journeyEmissionsData.GetLightRailSystemCode("C5"), "LightRailSystemCode for C5 is incorrect");
			Assert.AreEqual("TW", journeyEmissionsData.GetLightRailSystemCode("C3"), "LightRailSystemCode for C3 is incorrect");
		}

		/// <summary>
		/// Checks that data is reloaded successfully when the notification event fires.
		/// </summary>
		[Test]
		public void TestDataReload()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();
			JourneyEmissionsFactor journeyEmissionsData = AddJourneyEmissionsFactor();

			// Check values prior to change
			Assert.AreEqual(0.050, journeyEmissionsData.GetEmissionFactor("RailDefault"), "Emission factor for RailDefault is incorrect");
			Assert.AreEqual("LU", journeyEmissionsData.GetLightRailSystemCode("C5"), "LightRailSystemCode for C5 is incorrect");

			JourneyEmissionsFactorTestHelper.LoadDataFile(dataFile2);

			// Check that data hasn't changed too early
			Assert.AreEqual(0.050, journeyEmissionsData.GetEmissionFactor("RailDefault"), "Data changed too early - Emission factor for RailDefault is incorrect");
			Assert.AreEqual("LU", journeyEmissionsData.GetLightRailSystemCode("C5"), "Data changed too early - LightRailSystemCode for C5 is incorrect");

			// Cause the Changed event to be raised by the notification service
			dataChangeNotification.RaiseChangedEvent("JourneyEmissionsFactor");

			// Check that the data has changed
			Assert.AreEqual(5.050, journeyEmissionsData.GetEmissionFactor("RailDefault"), "Emission factor for RailDefault is incorrect");
			Assert.AreEqual("XY", journeyEmissionsData.GetLightRailSystemCode("C5"), "LightRailSystemCode for C5 is incorrect");
		}
	}

	#region Database helper class

	public sealed class JourneyEmissionsFactorTestHelper
	{
		private const string tempTablePrefix = "tempTestBackup";

		private JourneyEmissionsFactorTestHelper()
		{
		}

		public static void BackupCurrentData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			BackupTable("JourneyEmissionsFactor", helper);
			BackupTable("LightRailSystemCode", helper);
		}

		public static void RestoreOriginalData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			ClearTableDown("JourneyEmissionsFactor", helper);
			ClearTableDown("LightRailSystemCode", helper);

			RestoreFromBackup("JourneyEmissionsFactor", helper);
			RestoreFromBackup("LightRailSystemCode", helper);

			RemoveBackupTable("JourneyEmissionsFactor", helper);
			RemoveBackupTable("LightRailSystemCode", helper);
		}

		/// <summary>
		/// Drops the specified table.
		/// </summary>
		/// <param name="tableName">Name of the table to drop</param>
		/// <param name="connectedHelper"></param>
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
		private static void RestoreFromBackup(string tableName, SqlHelper connectedHelper)
		{
			string tempTableName = tempTablePrefix + tableName;
			ClearTableDown(tableName, connectedHelper);
			connectedHelper.Execute(String.Format(CultureInfo.InvariantCulture, "insert into {0} select * from {1}", tableName, tempTableName));
		}

		/// <summary>
		/// Deletes the contents of the specified helper
		/// </summary>
		/// <param name="tableName">Table to back up. Must not be temporary.</param>
		/// <param name="connectedHelper">An instance of SqlHelper with an open connection</param>
		public static void ClearTableDown(string tableName, SqlHelper connectedHelper)
		{
			connectedHelper.Execute("delete " + tableName);
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

		private static string AttributeValueForDatabase(XmlNode node, string attributeName)
		{
			XmlAttribute attribute;
			attribute = node.Attributes[attributeName];
			if (attribute != null)
				return attribute.Value.Replace("'", "''");
			else
				return string.Empty;
		}

		public static bool LoadDataFile(string dataFile)
		{
			XmlDocument xmlData = new XmlDocument();
			xmlData.Load(dataFile);

			//Data is in a straightforward format:
			//Rootnode is called "JourneyEmissionsFactorTestData"
			//Two types of child node:
			//     <journeyemissionsfactor factortype="" factorvalue="">
			//     <lightrailsystemcode carsize="" fueltype="" consumption="">
			//The following SQL dumps the current tables into xml of this format
			//
			//	select '<journeyemissionsfactor factortype="' + factortype + '" factorvalue="' + cast(factorvalue as int) + '"/>' from journeyemissionsfactor
			//	union
			//	select '<lightrailsystemcode toccode="' + toccode + '" service="' + service + '" mode="' + mode + '" systemcode="' + systemcode + '"/>' from lightrailsystemcode

			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);
			
			ClearTableDown("JourneyEmissionsFactor", helper);
			ClearTableDown("LightRailSystemCode", helper);

			string insertJourneyEmissionsFactor = "insert into journeyemissionsfactor (factortype, factorvalue) values ('{0}', {1})";
			string insertLightRailSystemCode = "insert into lightrailsystemcode (toccode, service, mode, systemcode) values ('{0}', '{1}', '{2}', '{3}')";

			XmlNodeList currNodes;
			currNodes = xmlData.GetElementsByTagName("journeyemissionsfactor");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertJourneyEmissionsFactor, AttributeValueForDatabase(curr, "factortype"), AttributeValueForDatabase(curr, "factorvalue")));

			currNodes = xmlData.GetElementsByTagName("lightrailsystemcode");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertLightRailSystemCode, AttributeValueForDatabase(curr, "toccode"), AttributeValueForDatabase(curr, "service"), AttributeValueForDatabase(curr, "mode"), AttributeValueForDatabase(curr, "systemcode")));

			helper.ConnClose();

			return true;
						
		}

	}

	#endregion

	#region Initialisation class

	/// <summary>
	/// Initialisation class for Car Cost Calculator test
	/// </summary>
	public class JourneyEmissionsFactorTestInitialisation : IServiceInitialisation
	{
		public JourneyEmissionsFactorTestInitialisation()
		{
		}

		// Need to add a file property provider
		// Enable PropertyService
		public void Populate(Hashtable serviceCache)
		{
			ArrayList errors = new ArrayList();

			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			try
			{
				// create custom email publisher
				IEventPublisher[] customPublishers = new IEventPublisher[0];	

				Trace.Listeners.Remove("TDTraceListener");
				// create and add TDTraceListener instance to the listener collection	
				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
			}
			catch (TDException tdEx)
			{
				// create message string
				StringBuilder message = new StringBuilder(100);
				message.Append(tdEx.Message); // prepend with existing exception message

				// append all messages returned by TDTraceListener constructor
				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

				// log message using .NET default trace listener
				Trace.WriteLine(message.ToString() + "ExceptionID:" + tdEx.Identifier.ToString());			

				// rethrow exception - use the initial exception id as the id
				throw new TDException(message.ToString(), tdEx, false, tdEx.Identifier);
			}
		}

	}

	#endregion

}
