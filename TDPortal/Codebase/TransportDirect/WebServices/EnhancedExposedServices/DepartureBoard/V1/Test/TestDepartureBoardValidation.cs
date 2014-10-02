// *********************************************** 
// NAME                 : TestDepartureBoardValidation..cs
// AUTHOR               : Russell Wilby
// DATE CREATED         : 13/02/2006
// DESCRIPTION  		: Nunit test class Departure Board Web Service using proxy class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/DepartureBoard/V1/Test/TestDepartureBoardValidation.cs-arc  $ 
//
//   Rev 1.1   Dec 12 2007 16:19:42   jfrank
//Updated for WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:51:58   mturner
//Initial revision.
//
//   Rev 1.0   Feb 13 2006 17:35:42   RWilby
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

using System;
using NUnit.Framework;
using System.Web.Services.Protocols;
using System.Xml;
using System.Text; 
using TransportDirect.Common; 
using TransportDirect.Common.Logging;     
using Logger = System.Diagnostics.Trace;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Test;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.EnhancedExposedServices.Helpers;

using dtV1 =  TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1;
using dbV1 = TransportDirect.EnhancedExposedServices.DepartureBoard.V1;

namespace TransportDirect.EnhancedExposedServices.DepartureBoard.V1.Test
{
	/// <summary>
	/// Nunit test class Departure Board Web Service
	/// </summary>
	[TestFixture]
	public class TestDepartureBoardValidation
	{
		private TestDepartureBoardServiceWse serviceProxy;

		private string transactionId = "TestTransactionId";
		private string language = "en-GB";

		#region NUnit Members
		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestInitialisation());

			//username token
			UsernameToken token = new UsernameToken(TestInitialisation.UsernamePassword, TestInitialisation.UsernamePassword, PasswordOption.SendHashed);

			//web service proxy class
			serviceProxy = new TestDepartureBoardServiceWse();
            serviceProxy.RequestSoapContext.Security.Tokens.Add(token);
        }

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			serviceProxy = null;
		}

		#endregion
		
		#region TestMethods
		/// <summary>
		/// Test for missing transaction Id
		/// </summary>
		[Test]
		public void MissingTransactionId()
		{
			try
			{
				serviceProxy.GetDepartureBoardTimeRequestTypes(null, "en-GB");
			}
			catch(SoapException sEx)
			{
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >= 0); 
			}
		}

		/// <summary>
		/// Test for missing language Id
		/// </summary>
		[Test]
		public void MissingLanguage()
		{
			try
			{
				serviceProxy.GetDepartureBoardTimeRequestTypes("A", null);
			}
			catch(SoapException sEx)
			{
				  
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >= 0); 
			}
		}

		/// <summary>
		/// Test Departure Board GetDepartureBoardTimeRequestTypes method
		/// </summary>
		[Test]
		public void TestGetDepartureBoardTimeRequestTypes()
		{
			DepartureBoardServiceTimeRequestType[] timeRequestType = serviceProxy.GetDepartureBoardTimeRequestTypes(transactionId, language);
			
			Assert.IsTrue(timeRequestType.Length == 4, "The DepartureBoardServiceTimeRequestType length should always be 4");
 
			StringBuilder outstring = new StringBuilder(); 
			foreach (DepartureBoardServiceTimeRequestType timeType in timeRequestType)
			{
				outstring.Append(timeType.ToString() +  "  ");
 
			}
			Console.WriteLine(System.Environment.NewLine + outstring); 
  
		}
		#endregion


	
	}
}
