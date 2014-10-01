// *********************************************** 
// NAME			: TestEnvironmentalBenefitsCalculator.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 21/09/2009
// DESCRIPTION	: Class testing the funcationality of environmental benefits data and calculator
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnvironmentalBenefits/Test/TestEnvironmentalBenefitsCalculator.cs-arc  $
//
//   Rev 1.1   Oct 19 2009 17:33:30   mmodi
//Updated following tables change
//
//   Rev 1.0   Oct 06 2009 13:57:30   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Xml;

using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.DataServices;

namespace TransportDirect.UserPortal.EnvironmentalBenefits
{
    /// <summary>
    /// Class testing the funcationality of environmental benefits data and calculator
    /// </summary>
    [TestFixture]
    public class TestEnvironmentalBenefitsCalculator
    {
        #region Private members

        private const string dataFile1 = @".\Test\testEnvironmentalBenefitsData1.xml";
        private const string dataFile2 = @".\Test\testEnvironmentalBenefitsData2.xml";

        #endregion

        #region Setup

        /// <summary>
        /// Initialisation in setup method called before every test method
        /// </summary>
        [SetUp]
        public void Init()
        {
            // Clear down temp scripts folder
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new EnvironmentalBenefitsTestInitialisation());

            EnvironmentalBenefitsTestHelper.BackupCurrentData();
            EnvironmentalBenefitsTestHelper.LoadDataFile(dataFile1);
        }

        /// <summary>
        /// Finalisation method called after every test method
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            EnvironmentalBenefitsTestHelper.RestoreOriginalData();
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Helper method. Adds the EnvironmentalBenefitsCalculator service to the TDServiceDiscovery cache.
        /// </summary>
        /// <returns>The newly created instance of EnvironmentalBenefitsCalculator</returns>
        private EnvironmentalBenefitsCalculator AddEnvironmentalBenefitsCalculator()
        {
            TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.EnvironmentalBenefitsCalculator, new EnvironmentalBenefitsCalculatorFactory());
            return (EnvironmentalBenefitsCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.EnvironmentalBenefitsCalculator];
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

        #region Tests

        /// <summary>
        /// Checks that data retrieval methods return the correct values after the EnvironmentalBenefitsCalculator is created.
        /// </summary>
        [Test]
        public void TestGetDataMethods()
        {
            EnvironmentalBenefitsCalculator ebcCalculator = AddEnvironmentalBenefitsCalculator();
            EnvironmentalBenefitsData ebcData = ebcCalculator.EBCData;

            // Calculator returns value as PencePerMetre, so convert the value we are comparing
            double compareValue = 301 / (double)1609;
            
            Assert.AreEqual(compareValue, ebcData.GetRoadCategoryCost(EBCRoadCategory.RoadOther, EBCCountry.England), "EBC RoadCategoryCost value for EBCRoadCategory.RoadOther, EBCCountry.England is incorrect");
        }

        /// <summary>
        /// Checks that data is reloaded successfully when the notification event fires.
        /// </summary>
        [Test]
        public void TestDataReload()
        {
            TestMockDataChangeNotification dataChangeNotification = AddDataChangeNotification();
            EnvironmentalBenefitsCalculator ebcCalculator = AddEnvironmentalBenefitsCalculator();
            EnvironmentalBenefitsData ebcData = ebcCalculator.EBCData;

            ebcCalculator = (EnvironmentalBenefitsCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.EnvironmentalBenefitsCalculator];
            ebcData = ebcCalculator.EBCData;

            // Calculator returns value as PencePerMetre, so convert the value we are comparing
            double compareValue = 301 / (double)1609;

            // Check values prior to change
            Assert.AreEqual(compareValue, ebcData.GetRoadCategoryCost(EBCRoadCategory.RoadOther, EBCCountry.England), "EBC RoadCategoryCost value for EBCRoadCategory.RoadOther, EBCCountry.England is incorrect");

            EnvironmentalBenefitsTestHelper.LoadDataFile(dataFile2);

            ebcCalculator = (EnvironmentalBenefitsCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.EnvironmentalBenefitsCalculator];
            ebcData = ebcCalculator.EBCData;

            // Check that data hasn't changed too early
            Assert.AreEqual(compareValue, ebcData.GetRoadCategoryCost(EBCRoadCategory.RoadOther, EBCCountry.England), "EBC RoadCategoryCost value for EBCRoadCategory.RoadOther, EBCCountry.England is incorrect");

            // Cause the Changed event to be raised by the notification service
            dataChangeNotification.RaiseChangedEvent("EnvironmentalBenefits");

            ebcCalculator = (EnvironmentalBenefitsCalculator)TDServiceDiscovery.Current[ServiceDiscoveryKey.EnvironmentalBenefitsCalculator];
            ebcData = ebcCalculator.EBCData;

            // New value being compared
            compareValue = 33333 / (double)1609;

            // Check that the data has changed
            Assert.AreEqual(compareValue, ebcData.GetRoadCategoryCost(EBCRoadCategory.RoadOther, EBCCountry.England), "EBC RoadCategoryCost value for EBCRoadCategory.RoadOther, EBCCountry.England is incorrect");
        }

        #endregion
    }

    #region Database helper class

    public sealed class EnvironmentalBenefitsTestHelper
    {
        private const string tempTablePrefix = "tempTestBackup";

        private EnvironmentalBenefitsTestHelper()
        {
        }

        public static void BackupCurrentData()
        {
            SqlHelper helper = new SqlHelper();
            helper.ConnOpen(SqlHelperDatabase.DefaultDB);

            BackupTable("EnvBenRoadCategoryCost", helper);
            BackupTable("EnvBenHighValueMotorway", helper);
            BackupTable("EnvBenHighValueMotorwayJunction", helper);
            BackupTable("EnvBenUnknownMotorwayJunction", helper);
        }

        public static void RestoreOriginalData()
        {
            SqlHelper helper = new SqlHelper();
            helper.ConnOpen(SqlHelperDatabase.DefaultDB);

            ClearTableDown("EnvBenRoadCategoryCost", helper);
            ClearTableDown("EnvBenHighValueMotorway", helper);
            ClearTableDown("EnvBenHighValueMotorwayJunction", helper);
            ClearTableDown("EnvBenUnknownMotorwayJunction", helper);

            RestoreFromBackup("EnvBenRoadCategoryCost", helper);
            RestoreFromBackup("EnvBenHighValueMotorway", helper);
            RestoreFromBackup("EnvBenHighValueMotorwayJunction", helper);
            RestoreFromBackup("EnvBenUnknownMotorwayJunction", helper);

            RemoveBackupTable("EnvBenRoadCategoryCost", helper);
            RemoveBackupTable("EnvBenHighValueMotorway", helper);
            RemoveBackupTable("EnvBenHighValueMotorwayJunction", helper);
            RemoveBackupTable("EnvBenUnknownMotorwayJunction", helper);
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

        /// <summary>
        /// Converts value read from an xml file in to a value for a database sql statement
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
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
        /// Loads data from xml files in to the database table
        /// </summary>
        /// <param name="dataFile"></param>
        /// <returns></returns>
        public static bool LoadDataFile(string dataFile)
        {
            XmlDocument xmlData = new XmlDocument();
            xmlData.Load(dataFile);

            //Data is in a straightforward format:
            //Rootnode is called "EnvironmentalBenefitsTestData"
            //Four types of child node:
            //     <EnvBenRoadCategoryCost RoadCategory="" Country="">
            //     <EnvBenHighValueMotorway carsize="" fueltype="" consumption="">
            //     <EnvBenHighValueMotorwayJunction factortype="" factorvalue="">
            //     <EnvBenUnknownMotorwayJunction factortype="" factorvalue="">
            //
            //The following SQL dumps the current tables into xml of this format
            //
            //	select '<EnvBenRoadCategoryCost RoadCategory="' + RoadCategory + '" Country="' + Country + '" PencePerMile="' + cast(PencePerMile as varchar) + '"/>' from EnvBenRoadCategoryCost
            //	union
            //	select '<EnvBenHighValueMotorway MotorwayName="' + MotorwayName + '" DuplicateMotorwayJunction="' + DuplicateMotorwayJunction + '" AllHighValue="' + cast(AllHighValue as varchar) + '"/>' from EnvBenHighValueMotorway
            //  union
            //	select '<EnvBenHighValueMotorwayJunction MotorwayName="' + MotorwayName + '" JunctionStart="' + JunctionStart + '" JunctionEnd="' + JunctionEnd + '" Distance="' + cast(Distance as varchar) + '" Country="' + Country + '"/>' from EnvBenHighValueMotorwayJunction
            //  union
            //	select '<EnvBenUnknownMotorwayJunction MotorwayName="' + MotorwayName + '" JoiningRoad="' + JoiningRoad + '"JoiningJunction="' + JoiningJunction + '" JunctionEntry="' + JunctionEntry + '" JunctionExit="' + JunctionExit + '"/>' from EnvBenUnknownMotorwayJunction
            //

            SqlHelper helper = new SqlHelper();
            helper.ConnOpen(SqlHelperDatabase.DefaultDB);

            ClearTableDown("EnvBenRoadCategoryCost", helper);
            ClearTableDown("EnvBenHighValueMotorway", helper);
            ClearTableDown("EnvBenHighValueMotorwayJunction", helper);
            ClearTableDown("EnvBenUnknownMotorwayJunction", helper);

            string insertEnvBenRoadCategoryCost = "INSERT INTO EnvBenRoadCategoryCost VALUES ('{0}', '{1}', {2})";
            string insertEnvBenHighValueMotorway = "INSERT INTO EnvBenHighValueMotorway VALUES ('{0}', '{1}', {2})";
            string insertEnvBenHighValueMotorwayJunction = "INSERT INTO EnvBenHighValueMotorwayJunction VALUES ('{0}', '{1}', '{2}', {3}, '{4}')";
            string insertEnvBenVirtualMotorwayJunction = "INSERT INTO EnvBenUnknownMotorwayJunction VALUES ('{0}', '{1}', '{2}', '{3}', '{4}')";

            XmlNodeList currNodes;
            currNodes = xmlData.GetElementsByTagName("EnvBenRoadCategoryCost");
            foreach (XmlNode curr in currNodes)
                helper.Execute(String.Format(CultureInfo.InvariantCulture, insertEnvBenRoadCategoryCost, 
                    AttributeValueForDatabase(curr, "RoadCategory"), 
                    AttributeValueForDatabase(curr, "Country"),
                    AttributeValueForDatabase(curr, "PencePerMile")));

            currNodes = xmlData.GetElementsByTagName("EnvBenHighValueMotorway");
            foreach (XmlNode curr in currNodes)
                helper.Execute(String.Format(CultureInfo.InvariantCulture, insertEnvBenHighValueMotorway, 
                    AttributeValueForDatabase(curr, "MotorwayName"),
                    AttributeValueForDatabase(curr, "DuplicateMotorwayJunction"),
                    AttributeValueForDatabase(curr, "AllHighValue")));

            currNodes = xmlData.GetElementsByTagName("EnvBenHighValueMotorwayJunction");
            foreach (XmlNode curr in currNodes)
                helper.Execute(String.Format(CultureInfo.InvariantCulture, insertEnvBenHighValueMotorwayJunction,
                    AttributeValueForDatabase(curr, "MotorwayName"),
                    AttributeValueForDatabase(curr, "JunctionStart"),
                    AttributeValueForDatabase(curr, "JunctionEnd"),
                    AttributeValueForDatabase(curr, "Distance"),
                    AttributeValueForDatabase(curr, "Country")));

            currNodes = xmlData.GetElementsByTagName("EnvBenUnknownMotorwayJunction");
            foreach (XmlNode curr in currNodes)
                helper.Execute(String.Format(CultureInfo.InvariantCulture, insertEnvBenVirtualMotorwayJunction,
                    AttributeValueForDatabase(curr, "MotorwayName"),
                    AttributeValueForDatabase(curr, "JoiningRoad"),
                    AttributeValueForDatabase(curr, "JoiningJunction"),
                    AttributeValueForDatabase(curr, "JunctionEntry"),
                    AttributeValueForDatabase(curr, "JunctionExit")));

            helper.ConnClose();

            return true;

        }
    }

    #endregion

    #region Initialisation class

    /// <summary>
    /// Initialisation class for Environmental Benefits test
    /// </summary>
    public class EnvironmentalBenefitsTestInitialisation : IServiceInitialisation
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public EnvironmentalBenefitsTestInitialisation()
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
                Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
            }
            catch (TDException tdEx)
            {
                // create message string
                StringBuilder message = new StringBuilder(100);
                message.Append(tdEx.Message); // prepend with existing exception message

                // append all messages returned by TDTraceListener constructor
                foreach (string error in errors)
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
