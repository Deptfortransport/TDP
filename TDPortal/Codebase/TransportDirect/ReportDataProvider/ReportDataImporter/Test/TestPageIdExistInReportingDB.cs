// *********************************************** 
// NAME                 : TestPageIdExistInReportingDB.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 09/02/2006 
// DESCRIPTION  		: Test for missing PageId in Reporting DB
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/ReportDataImporter/Test/TestPageIdExistInReportingDB.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:08   mturner
//Initial revision.
//
//   Rev 1.1   Apr 05 2006 12:59:26   mtillett
//Ensure that the SQL connection is closed
//
//   Rev 1.0   Feb 09 2006 10:44:40   mtillett
//Initial revision.

using System;
using System.Collections;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.ReportDataImporter.Test
{
	public class TestPageIdInitialisation : IServiceInitialisation
	{
		public TestPageIdInitialisation()
		{
			
		}

		public void Populate(Hashtable serviceCache)
		{
			serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory());
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
		}
	}

	/// <summary>
	/// Summary description for TestPageIdExistInReportingDB.
	/// </summary>
	[TestFixture]
	public class TestPageIdExistInReportingDB
	{
		public TestPageIdExistInReportingDB()
		{
		}

		//initialisation in setup method called before every test method
		[SetUp]
		public void Init()
		{	
			TDServiceDiscovery.Init( new TestPageIdInitialisation() );
		}

		//finalisation in TearDown method called after every test method
		[TearDown]
		public void CleanUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();			
		} 

		[Test]
		public void TestPageIdExistsInDB()
		{   			
			string[] pageids = Enum.GetNames(typeof(PageId));
   
			foreach (string pageid in pageids)
			{
				//Empty is purely a marker
				if (pageid != "Empty")
				{
					Console.WriteLine(pageid); 
					if (GetDBRowCount(pageid)==0)
					{
						Assert.Fail("The pageid enum value {0} does not exists in Reporting DB", pageid); 
					}
				}
			}
		}
		/// <summary>
		/// This method checks whether given enum value exists in the DB  
		/// </summary>
		/// <param name="typeValue">enum value as string</param>
		/// <returns>count returned from sql query</returns>
		private int GetDBRowCount(string typeValue)
		{ 			
			string sqlQuery = string.Empty; 
			SqlHelper sqlHelper = new SqlHelper();
			try
			{
				sqlHelper.ConnOpen(SqlHelperDatabase.ReportDataDB);
				sqlQuery = "SELECT Count(PETCode) FROM  PageEntryType WHERE PETCode =" + "'" + typeValue + "'";

				int recordCount = (int)sqlHelper.GetScalar(sqlQuery);  
				return recordCount;
			}
			finally
			{
				sqlHelper.ConnClose();
			}
		}	
	}
}
