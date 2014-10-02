// *********************************************** 
// NAME                 : TestServiceAndOperationTypesExistsInDB.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 19/01/2006 
// DESCRIPTION  		: Test class for testing service types and operation types exists in DB
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Test/TestServiceAndOperationTypesExistsInDB.cs-arc  $ 
//
//   Rev 1.1   Dec 13 2007 10:20:54   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:24   mturner
//Initial revision.
//
//   Rev 1.3   Jan 30 2006 14:35:50   mtillett
//Fix bug in the unit test
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.2   Jan 23 2006 19:33:36   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 10:58:00   schand
//Corrected test and added some comments
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 19 2006 14:35:08   schand
//Initial revision.

using System;
using NUnit.Framework;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.EnhancedExposedServices.Common;   
using TransportDirect.EnhancedExposedServices.Helpers;

namespace TransportDirect.EnhancedExposedServices.Test
{
	/// <summary>
	/// Test class for testing service types and operation types exists in DB
	/// </summary>
	[TestFixture]
	public class TestServiceAndOperationTypesExistsInDB
	{
		private  const string operation = "OperationType";
		private  const string service   = "ServiceType";


		#region NUnit Members
		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestInitialisation());
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();			
		}

		#endregion

		#region Test Methods
		/// <summary>
		/// This method tests whether all values in service type enum exist in reporting DB
		/// </summary>
		[Test]
		public void TestServiceTypeExistsInDB()
		{   			
			string[] serviceTypes = Enum.GetNames(typeof(EnhancedExposedServiceTypes));
   
			foreach (string serviceType in serviceTypes)
			{
				if (GetDBRowCount(serviceType, service)==0)
					Assert.Fail("The service type enum value {0} does not exists in Reporting DB", serviceType); 
				else
					Console.WriteLine(serviceType); 
			}

		}
		/// <summary>
		/// This method tests whether all values in operation type enum exist in reporting DB
		/// </summary>
		[Test]
		public void TestOperationTypeExistsInDB()
		{
			string[] operationTypes = Enum.GetNames(typeof(EnhancedExposedOperationType));
   
			foreach (string operationType in operationTypes)
			{
				if (GetDBRowCount(operationType, operation)==0)
					Assert.Fail("The service type enum value {0} does not exists in Reporting DB", operationType); 
				else
					Console.WriteLine(operationType); 
			}
		}

		#endregion

		#region Private Methods
		/// <summary>
		/// This method checks whether given enum value exists in the DB  
		/// </summary>
		/// <param name="typeValue">enum value as string</param>
		/// <param name="type">Enum type</param>
		/// <returns>count returned from sql query</returns>
		private static int GetDBRowCount(string typeValue, string enumType)
		{ 			
		   string sqlQuery = string.Empty; 
			SqlHelper sqlHelper = new SqlHelper();
			sqlHelper.ConnOpen(SqlHelperDatabase.ReportDataDB);
			if (enumType == service)				 
				sqlQuery = "SELECT Count(EESTType) FROM  EnhancedExposedServicesType WHERE EESTType =" + "'" + typeValue + "'";
			else
				sqlQuery = "SELECT Count(EEOTType) FROM  EnhancedExposedOperationType WHERE EEOTType =" + "'" + typeValue + "'";



			int recordCount = (int)sqlHelper.GetScalar(sqlQuery);  
			return recordCount;
		}
		#endregion
	
	}
}
