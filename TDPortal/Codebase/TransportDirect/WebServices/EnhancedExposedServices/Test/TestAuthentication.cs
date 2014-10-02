// *********************************************** 
// NAME                 : TestAuthentication.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 17/02/2006
// DESCRIPTION  		: Test class to test the authentication
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Test/TestAuthentication.cs-arc  $
//
//   Rev 1.1   Dec 13 2007 10:20:54   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:24   mturner
//Initial revision.
//
//   Rev 1.1   Feb 20 2006 16:19:28   mdambrine
//slightly wrong text for the bad password test
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions
//
//   Rev 1.0   Feb 20 2006 14:03:52   mdambrine
//Initial revision.
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions


using NUnit.Framework;
using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Web;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.Common;

namespace TransportDirect.EnhancedExposedServices.Test
{
	/// <summary>
	/// Summary description for TestRequestContext.
	/// </summary>
	[TestFixture]
	public class TestAuthentication
    {
        public const string UsernamePassword = "EnhancedExposedWebServiceTest";
		public const string authorisedParner = "EnhancedExposedPartnerAuthorised";
		public const string notAuthorisedParner = "EnhancedExposedPartnerNotAuthorised";

		public const string notAuthorisedParnerMessage = "Microsoft.Web.Services3.Security.SecurityFault: The security token could not be authenticated or authorized ---> TransportDirect.Common.TDException: Message:Access to this service has been restricted for this user account";
		public const string invalidParnerMessage = "Microsoft.Web.Services3.Security.SecurityFault: The security token could not be authenticated or authorized ---> TransportDirect.Common.TDException: Message:Error trying to lookup the password via the hostname";		
		public const string invalidParnerPasswordMessage = "Microsoft.Web.Services3.Security.SecurityFault: The security token could not be authenticated or authorized ---> System.Exception: WSE563: The computed password digest doesn't match that of the incoming username token";		

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
			HttpContext.Current = null;
		}

		#endregion
	
		[Test]
		public void ValidUserNameAuthorised()
		{
			try
			{
				TestWebServiceProxyReferenceWse serviceProxy = new TestWebServiceProxyReferenceWse();

				//username token
				UsernameToken token = new UsernameToken(authorisedParner, UsernamePassword, PasswordOption.SendHashed);										
				serviceProxy.RequestSoapContext.Security.Tokens.Add( token );	

				string[] context = serviceProxy.RequestContextData("1234", "cy-GB");		
				
				Assert.IsNotNull(context, "the test failed as this object should have been populated");
			}
			catch 
			{
				Assert.Fail("test failed as valid username was rejected");
			}
		}

		[Test]
		public void ValidUserNameNotAuthorised()
		{
			try
			{
				TestWebServiceProxyReferenceWse serviceProxy = new TestWebServiceProxyReferenceWse();

				//username token
				UsernameToken token = new UsernameToken(notAuthorisedParner, UsernamePassword, PasswordOption.SendHashed);										
				serviceProxy.RequestSoapContext.Security.Tokens.Add( token );	

				string[] context = serviceProxy.RequestContextData("1234", "cy-GB");		
				
				Assert.IsNotNull(context, "the test failed as this object should have been populated");
			}
			catch (SoapException soapEx)
            {                
                Assert.AreEqual("FailedAuthentication", soapEx.Code.Name, "The soapexception code does not match");
				Assert.IsTrue(soapEx.Message.StartsWith(notAuthorisedParnerMessage), "This message does not correspond with what was expected");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
		}

		[Test]
		public void InValidUserPasswordAuthorised()
		{
			try
			{
				TestWebServiceProxyReferenceWse serviceProxy = new TestWebServiceProxyReferenceWse();

				//username token
				UsernameToken token = new UsernameToken(authorisedParner, "InvalidPassword", PasswordOption.SendHashed);										
				serviceProxy.RequestSoapContext.Security.Tokens.Add( token );	

				string[] context = serviceProxy.RequestContextData("1234", "cy-GB");		
				
				Assert.Fail("test failed as invalid Password was accepted");
			}
			catch (SoapException soapEx)
			{                
				Assert.AreEqual("FailedAuthentication", soapEx.Code.Name, "The soapexception code does not match");
				Assert.IsTrue(soapEx.Message.StartsWith(invalidParnerPasswordMessage), "This message does not correspond with what was expected");
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}
		}

		[Test]
		public void InvalidUserNameAuthorised()
		{
			try
			{
				TestWebServiceProxyReferenceWse serviceProxy = new TestWebServiceProxyReferenceWse();

				//username token
				UsernameToken token = new UsernameToken("EnhancedExposedWebServiceInvalid", UsernamePassword, PasswordOption.SendHashed);										
				serviceProxy.RequestSoapContext.Security.Tokens.Add( token );	

				string[] context = serviceProxy.RequestContextData("1234", "cy-GB");		
				
				Assert.Fail("test failed as invalid username was accepted");
			}
			catch (SoapException soapEx)
			{                
				Assert.AreEqual("FailedAuthentication", soapEx.Code.Name, "The soapexception code does not match");
				Assert.IsTrue(soapEx.Message.StartsWith(invalidParnerMessage), "This message does not correspond with what was expected");
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}
        }
    }
}