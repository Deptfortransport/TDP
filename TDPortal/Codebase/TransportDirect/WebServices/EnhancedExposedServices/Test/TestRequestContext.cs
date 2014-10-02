// *********************************************** 
// NAME                 : TestRequestContext.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: Test class for the RequestContext object and its creation
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Test/TestRequestContext.cs-arc  $ 
//
//   Rev 1.1   Dec 13 2007 10:20:54   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:24   mturner
//Initial revision.
//
//   Rev 1.6   Jan 20 2006 13:29:18   mtillett
//Move all code requiring HttpContent e.g. for username and partner id lookup to single class. This is so that the test switch is in a seperate place to ensure refactoring later
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.5   Jan 19 2006 16:48:06   mtillett
//Move test for invalid language to different test class
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.4   Jan 11 2006 13:52:02   mtillett
//Change password to send as SendHashed
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Jan 11 2006 09:47:42   mtillett
//Fixup unit test for changes to Context
//
//   Rev 1.2   Jan 11 2006 09:17:14   mtillett
//Move soap exception helper to SoapFault/V1 namespace and fix up references to methods and unit tests
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.1   Jan 05 2006 14:56:28   mtillett
//Update test classes
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.0   Jan 05 2006 14:01:40   mtillett
//Initial revision.

using NUnit.Framework;
using System;
using System.Web.Services.Protocols;
using System.Xml;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Helpers;

namespace TransportDirect.EnhancedExposedServices.Test
{
	/// <summary>
	/// Summary description for TestRequestContext.
	/// </summary>
	[TestFixture]
	public class TestRequestContext
	{
        private TestWebServiceProxyReferenceWse serviceProxy;
		private const string usernamepassword = "EnhancedExposedWebServiceTest";

		#region NUnit Members
		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestInitialisation());

			//username token
			UsernameToken token = new UsernameToken(usernamepassword, usernamepassword, PasswordOption.SendHashed);

			//web service proxy class
			serviceProxy = new TestWebServiceProxyReferenceWse();
//			serviceProxy.Url = "http://localhost/enhancedExposedServices/FindNearest/v1/FindNearestservice.asmx";
			serviceProxy.RequestSoapContext.Security.Tokens.Add( token );
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
	
		[Test]
		public void ValidMessage()
		{
			string transactionId = "1234";
			string english = "en-GB";
			string welsh = "cy-GB";
			try
			{
				//valid request and crequest context created correctly
				string[] data = serviceProxy.RequestContextData(transactionId, english);
				Console.WriteLine(data[0]);
				Console.WriteLine(data[1]);
				Console.WriteLine(data[3]);
				Console.WriteLine(data[4]);
				Console.WriteLine(data[5]);
				Console.WriteLine(data[6]);
				//no test for username
				Assert.IsTrue(data[1] == transactionId);
				//no test for RequestContext.InternalTransactionId;
				Assert.IsTrue(data[3] == english);
				Assert.IsTrue(data[4] == "RequestContextData");
				Assert.IsTrue(data[5] == "101");
				Assert.IsTrue(data[6] == "TransportDirect_EnhancedExposedServices_TestWebService");

				//valid request
				string[] datawelsh = serviceProxy.RequestContextData(transactionId, welsh);
				Assert.IsTrue(datawelsh[3] == welsh);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown for valid SOAP request. " + ex.ToString());
			}
		}
    }
}