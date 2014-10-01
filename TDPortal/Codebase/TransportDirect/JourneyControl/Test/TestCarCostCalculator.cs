// *********************************************** 
// NAME			: TestCarCostCalculator.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 15/12/04
// DESCRIPTION	: Class testing the funcationality of CarCostCalculator
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestCarCostCalculator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:14   mturner
//Initial revision.
//
//   Rev 1.4   Mar 23 2005 13:25:18   rhopkins
//Fix FxCop issues
//
//   Rev 1.3   Mar 04 2005 15:27:44   rhopkins
//Made test file paths relative, so that they should work on the build machine.
//Resolution for 1957: DEV Code Review: CC - Fuel and Running Costs Calculation
//
//   Rev 1.2   Jan 18 2005 15:51:40   rhopkins
//Corrected test so that it runs successfully, whether run individually or as part of the full assembly test.
//
//   Rev 1.1   Jan 06 2005 16:45:44   rhopkins
//Extended testing to correctly test all methods and the data reloading
//
//   Rev 1.0   Dec 17 2004 20:09:08   rhopkins
//Initial revision.
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
	/// Class testing the funcationality of CarCostCalculator
	/// </summary>
	[TestFixture]
	[CLSCompliant(false)]
	public class TestCarCostCalculator
	{
		private const string dataFile1 = @".\Test\testCarCostingData1.xml";
		private const string dataFile2 = @".\Test\testCarCostingData2.xml";

		/// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
		[SetUp]
		public void Init() 
		{ 
			// Clear down temp scripts folder
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new CarCostCalculatorTestInitialisation());

			CarCostCalculatorTestHelper.BackupCurrentData();
			CarCostCalculatorTestHelper.LoadDataFile(dataFile1);
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{ 
			CarCostCalculatorTestHelper.RestoreOriginalData();
		}

		#region Helper methods

		/// <summary>
		/// Helper method. Adds the CarCostCalculator service to the TDServiceDiscovery cache.
		/// </summary>
		/// <returns>The newly created instance of CarCostCalculator</returns>
		private CarCostCalculator AddCarCostCalculator()
		{
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.CarCostCalculator, new CarCostCalculatorFactory());
			return (CarCostCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarCostCalculator];
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
		/// Checks that data retrieval methods return the correct values after the CarCostCalculator is created.
		/// </summary>
		[Test]
		public void TestGetDataMethods()
		{
			CarCostCalculator carCostData = AddCarCostCalculator();

			Assert.AreEqual(5, carCostData.CalcRunningCost("small", "petrol", 5), "Running cost for small petrol is incorrect");
			Assert.AreEqual(10, carCostData.CalcRunningCost("small","diesel", 5), "Running cost for small diesel is incorrect");
			Assert.AreEqual(15, carCostData.CalcRunningCost("medium","petrol", 5), "Running cost for medium petrol is incorrect");
			Assert.AreEqual(20, carCostData.CalcRunningCost("medium","diesel", 5), "Running cost for medium diesel is incorrect");
			Assert.AreEqual(25, carCostData.CalcRunningCost("large","petrol", 5), "Running cost for large petrol is incorrect");
			Assert.AreEqual(30, carCostData.CalcRunningCost("large","diesel", 5), "Running cost for large diesel is incorrect");

			Assert.AreEqual(7, carCostData.GetFuelConsumption("small","petrol"), "Consumption for small petrol is incorrect");
			Assert.AreEqual(8, carCostData.GetFuelConsumption("small","diesel"), "Consumption for small diesel is incorrect");
			Assert.AreEqual(9, carCostData.GetFuelConsumption("medium","petrol"), "Consumption for medium petrol is incorrect");
			Assert.AreEqual(10, carCostData.GetFuelConsumption("medium","diesel"), "Consumption for medium diesel is incorrect");
			Assert.AreEqual(11, carCostData.GetFuelConsumption("large","petrol"), "Consumption for large petrol is incorrect");
			Assert.AreEqual(12, carCostData.GetFuelConsumption("large","diesel"), "Consumption for large diesel is incorrect");

			Assert.AreEqual(85, carCostData.GetFuelCost("petrol"), "Fuel cost for petrol is incorrect");
			Assert.AreEqual(87, carCostData.GetFuelCost("diesel"), "Fuel cost for diesel is incorrect");

		}

		/// <summary>
		/// Checks that data is reloaded successfully when the notification event fires.
		/// </summary>
		[Test]
		public void TestDataReload()
		{
			TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();
			CarCostCalculator carCostData = AddCarCostCalculator();

			// Check values prior to change
			Assert.AreEqual(20, carCostData.CalcRunningCost("medium","diesel", 5), "Running cost for medium diesel is incorrect");
			Assert.AreEqual(10, carCostData.GetFuelConsumption("medium","diesel"), "Consumption for medium diesel is incorrect");
			Assert.AreEqual(87, carCostData.GetFuelCost("diesel"), "Fuel cost for diesel is incorrect");

			CarCostCalculatorTestHelper.LoadDataFile(dataFile2);

			// Check that data hasn't changed too early
			Assert.AreEqual(20, carCostData.CalcRunningCost("medium","diesel", 5), "Data changed too early - Running cost for medium diesel is incorrect");
			Assert.AreEqual(10, carCostData.GetFuelConsumption("medium","diesel"), "Data changed too early - Consumption for medium diesel is incorrect");
			Assert.AreEqual(87, carCostData.GetFuelCost("diesel"), "Data changed too early - Fuel cost for diesel is incorrect");

			// Cause the Changed event to be raised by the notification service
			dataChangeNotification.RaiseChangedEvent("CarCosting");

			// Check that the data has changed
			Assert.AreEqual(70, carCostData.CalcRunningCost("medium","diesel", 5), "Running cost for medium diesel is incorrect");
			Assert.AreEqual(20, carCostData.GetFuelConsumption("medium","diesel"), "Consumption for medium diesel is incorrect");
			Assert.AreEqual(97, carCostData.GetFuelCost("diesel"), "Fuel cost for diesel is incorrect");
		}
	}

	#region Database helper class

	public sealed class CarCostCalculatorTestHelper
	{
		private const string tempTablePrefix = "tempTestBackup";

		private CarCostCalculatorTestHelper()
		{
		}

		public static void BackupCurrentData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			BackupTable("CarCostRunningCost", helper);
			BackupTable("CarCostFuelConsumption", helper);
			BackupTable("CarCostFuelCost", helper);
		}

		public static void RestoreOriginalData()
		{
			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);

			ClearTableDown("CarCostRunningCost", helper);
			ClearTableDown("CarCostFuelConsumption", helper);
			ClearTableDown("CarCostFuelCost", helper);

			RestoreFromBackup("CarCostRunningCost", helper);
			RestoreFromBackup("CarCostFuelConsumption", helper);
			RestoreFromBackup("CarCostFuelCost", helper);

			RemoveBackupTable("CarCostRunningCost", helper);
			RemoveBackupTable("CarCostFuelConsumption", helper);
			RemoveBackupTable("CarCostFuelCost", helper);


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
			//Rootnode is called "CarCostCalculatorTestData"
			//Three types of child node:
			//     <carcostrunningcost carsize="" fueltype="" cost="">
			//     <carcostfuelconsumption carsize="" fueltype="" consumption="">
			//     <carcostfuelcost fueltype="" cost="">
			//The following SQL dumps the current tables into xml of this format
			//
			//	select '<carcostrunningcost carsize="' + carsize + '" fueltype="' + fueltype + '" cost="' + cast(cost as varchar) + '"/>' from carcostrunningcost
			//	union
			//	select '<carcostfuelconsumption carsize="' + carsize + '" fueltype="' + fueltype + '" consumption="' + cast(consumption as varchar) + '"/>' from carcostfuelconsumption
			//	union
			//	select '<carcostfuelcost fueltype="' + fueltype + '" cost="' + cast(cost as varchar) + '"/>' from carcostfuelcost

			SqlHelper helper = new SqlHelper();
			helper.ConnOpen(SqlHelperDatabase.DefaultDB);
			
			ClearTableDown("carcostrunningcost", helper);
			ClearTableDown("carcostfuelconsumption", helper);
			ClearTableDown("carcostfuelcost", helper);

			string insertCarCostRunningCost = "insert into carcostrunningcost (carsize, fueltype, cost) values ('{0}', '{1}', {2})";
			string insertCarCostFuelConsumption = "insert into carcostfuelconsumption (carsize, fueltype, consumption) values ('{0}', '{1}', {2})";
			string insertCarCostFuelCost = "insert into carcostfuelcost (fueltype, cost) values ('{0}', {1})";

			XmlNodeList currNodes;
			currNodes = xmlData.GetElementsByTagName("carcostrunningcost");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertCarCostRunningCost, AttributeValueForDatabase(curr, "carsize"), AttributeValueForDatabase(curr, "fueltype"), AttributeValueForDatabase(curr, "cost")));

			currNodes = xmlData.GetElementsByTagName("carcostfuelconsumption");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertCarCostFuelConsumption, AttributeValueForDatabase(curr, "carsize"), AttributeValueForDatabase(curr, "fueltype"), AttributeValueForDatabase(curr, "consumption")));

			currNodes = xmlData.GetElementsByTagName("carcostfuelcost");
			foreach (XmlNode curr in currNodes)
				helper.Execute(String.Format(CultureInfo.InvariantCulture, insertCarCostFuelCost, AttributeValueForDatabase(curr, "fueltype"), AttributeValueForDatabase(curr, "cost")));

			helper.ConnClose();

			return true;
						
		}

	}

	#endregion

	#region Initialisation class

	/// <summary>
	/// Initialisation class for Car Cost Calculator test
	/// </summary>
	public class CarCostCalculatorTestInitialisation : IServiceInitialisation
	{
		public CarCostCalculatorTestInitialisation()
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
