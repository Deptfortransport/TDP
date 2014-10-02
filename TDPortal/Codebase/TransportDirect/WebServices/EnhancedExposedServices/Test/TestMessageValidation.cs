// *********************************************** 
// NAME                 : TestMessageValidation.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: Test class for MessageValidation WSDL validation attributes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Test/TestMessageValidation.cs-arc  $ 
//
//   Rev 1.1   Dec 13 2007 10:20:54   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:24   mturner
//Initial revision.
//
//   Rev 1.6   Jan 25 2006 16:57:44   schand
//Modified test methhod to compare the error message
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.5   Jan 23 2006 19:33:36   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.4   Jan 12 2006 18:12:56   mtillett
//Update MessageValidation to ensure that the SOAP response is validated aginast the WSDL in schema folder
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Jan 11 2006 13:52:02   mtillett
//Change password to send as SendHashed
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.2   Jan 05 2006 14:56:28   mtillett
//Update test classes
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.1   Jan 04 2006 15:48:40   mtillett
//NUNIT test files to test the MessageValidation against WSDL schema
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.0   Jan 04 2006 15:48:02   mtillett
//Initial revision.

using NUnit.Framework;
using System;
using System.Web.Services.Protocols;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Helpers;

namespace TransportDirect.EnhancedExposedServices.Test
{
	/// <summary>
	/// Summary description for TestMessageValidation.
	/// </summary>
	[TestFixture]
	public class TestMessageValidation
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
			try
			{
				//valid request with all data passed
				serviceProxy.WSDLValidation("1234", "en-GB", 1, "RG4");
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown for valid SOAP request. " + ex.ToString());
			}
		}

		[Test]
		public void InvalidResponseMessage()
		{
			try
			{
				//valid request with missing param2, to create invalid response
				serviceProxy.WSDLValidation("1234", "en-GB", 1, null);
			}
			catch (SoapException sEx)
			{   				
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLResponseValidationError) >=0);
			}
		}

		[Test]
		public void MissingTransactionId()
		{
			try
			{
				serviceProxy.WSDLValidation(null, "en-GB", 1, null);
			}
			catch (SoapException sEx)
			{   				
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >=0);
			}
		}
		
		[Test]
		public void InvalidLengthTransactionId()
		{
			string longTransactionId = String.Empty;
			longTransactionId.PadLeft(101, 'A');
			try
			{
				serviceProxy.WSDLValidation(longTransactionId, "en-GB", 1, null);
			}
			catch (SoapException sEx)
			{   				
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >=0);
			}
		}	

		[Test]
		public void MissingLanguage()
		{
			try
			{
				serviceProxy.WSDLValidation("A", null, 1, null);
			}
			catch (SoapException sEx)
			{   				
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >=0);
			}
		}
		
		[Test]
		public void InvalidIntRange()
		{
			try
			{
				serviceProxy.WSDLValidation("A", "en-GB", 0, null);
			}
			catch (SoapException sEx)
			{   				
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >=0);
			}
		}

		[Test]
		public void InvalidStringRegularExpression()
		{
			try
			{
				serviceProxy.WSDLValidation("A", "en-GB", 1, "XYZ");
			}
			catch (SoapException sEx)
			{   				
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >=0);
			}
		}
    }
}
