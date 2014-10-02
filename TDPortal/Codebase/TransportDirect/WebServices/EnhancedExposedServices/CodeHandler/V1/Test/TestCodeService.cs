// *********************************************** 
// NAME                 : TestCodeService.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 12/01/2006 
// DESCRIPTION  		: Test class for Code service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/CodeHandler/V1/Test/TestCodeService.cs-arc  $ 
//
//   Rev 1.1   Dec 12 2007 16:16:56   jfrank
//Updated for WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:51:54   mturner
//Initial revision.
//
//   Rev 1.2   Jan 23 2006 19:33:34   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 10:22:30   schand
//Added Nunit test method for language and transaction Id
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 13 2006 15:37:38   schand
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
using TransportDirect.EnhancedExposedServices.Helpers;




namespace TransportDirect.EnhancedExposedServices.CodeHandler.V1.Test
{
	/// <summary>
	/// Test class for Code service
	/// </summary>
	[TestFixture]
	public class TestCodeService
	{
		private TestCodeServiceWse  serviceProxy;
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
			serviceProxy = new TestCodeServiceWse();
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
		/// Test for Code Service FindText method
		/// </summary>
		[Test]
		public void TestFindText()
		{
			// creating the request object 
			CodeServiceRequest request = new CodeServiceRequest();
			CodeServiceModeType[] codeServiceModeTypes = new CodeServiceModeType[2];
			codeServiceModeTypes[0] = CodeServiceModeType.Rail;
			codeServiceModeTypes[0] = CodeServiceModeType.Bus;

			request.PlaceText  = "EUSTON";
			request.ModeTypes  = codeServiceModeTypes ;
			request.Fuzzy  = true;
			
			// calling the service via proxy 
			CodeServiceCodeDetail[] codeServiceCodeDetails = serviceProxy.FindText(transactionId, language, request);
			
			Assert.IsTrue(codeServiceCodeDetails.Length > 0, "There should always be some code item"); 
		    
			Console.WriteLine(string.Format("Total number of code item returned {0}", codeServiceCodeDetails.Length.ToString()));
			StringBuilder outstring = new StringBuilder();
 
			foreach(CodeServiceCodeDetail item in codeServiceCodeDetails)
			{
				Assert.IsTrue(item.Code.Length  > 0, "Code should be present");
				outstring.Append("Code -->"  + item.Code);   
				outstring.Append("Code Description -->"  + item.Description);
				outstring.Append("Code NaptanId -->"  + item.NaptanId );
				outstring.Append("Code Region -->"  + item.Region);				
				outstring.Append(System.Environment.NewLine);   
			}

			Console.WriteLine(outstring);
		}

		
		/// <summary>
		/// Test for Code Service FindCode method
		/// </summary>
		[Test]
		public void TestFindCode()
		{
			string code = "EUS";
			
			// calling the service via proxy 
			CodeServiceCodeDetail[] codeServiceCodeDetails = serviceProxy.FindCode(transactionId, language, code);
			
			Assert.IsTrue(codeServiceCodeDetails.Length > 0, "There should always be some code item"); 
		    
			Console.WriteLine(string.Format("Total number of code item returned {0}", codeServiceCodeDetails.Length.ToString()));
			StringBuilder outstring = new StringBuilder();
 
			foreach(CodeServiceCodeDetail item in codeServiceCodeDetails)
			{
				Assert.IsTrue(item.Code.Length > 0, "Code should be present");
				outstring.Append("Code -->"  + item.Code);   
				outstring.Append("Code Description -->"  + item.Description);
				outstring.Append("Code NaptanId -->"  + item.NaptanId );
				outstring.Append("Code Region -->"  + item.Region);				
				outstring.Append(System.Environment.NewLine);   
			}

			Console.WriteLine(outstring);
		}

		
		/// <summary>
		/// Test for missing transaction Id
		/// </summary>
		[Test]
		public void MissingTransactionId()
		{
			try
			{
				serviceProxy.FindCode(null, "en-GB", null);  
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
				serviceProxy.FindCode("A", null, null);
			}
			catch(SoapException sEx)
			{
				Assert.IsTrue(sEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) >= 0); 
			}
		}
		#endregion
	}
}
