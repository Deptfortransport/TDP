// *********************************************** 
// NAME                 : TestTestWebService.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: Test class for the TestWebService
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/Test/TestTestWebService.cs-arc  $ 
//
//   Rev 1.1   Dec 13 2007 15:04:18   jfrank
//Updated for WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:26   mturner
//Initial revision.
//
//   Rev 1.3   Jan 23 2006 19:33:36   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 20 2006 09:28:04   mtillett
//Remove SetForTest property and add test code to allow unit testing
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.1   Jan 19 2006 17:34:50   mtillett
//Add test for welsh language
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.0   Jan 19 2006 17:30:36   mtillett
//Initial revision.
//


using NUnit.Framework;
using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Web;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Helpers;

namespace TransportDirect.EnhancedExposedServices.Test
{
	/// <summary>
	/// Summary description for TestRequestContext.
	/// </summary>
	[TestFixture]
	public class TestTestWebService
	{

		#region NUnit Members
		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestInitialisation());

			HttpRequest req = new HttpRequest("", "http://www.transportdirect.info/Test/test.asmx", "");
			HttpContext.Current = new HttpContext(req, new HttpResponse(new System.IO.StringWriter()));
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
		public void ValidLanguage()
		{
			TestWebService testwebservice = new TestWebService();
			//invalid language
			string dataen = testwebservice.LanguageTest("1234", "en-GB");
			Assert.IsTrue(dataen == "en-GB");

			string datacy = testwebservice.LanguageTest("1234", "cy-GB");
			Assert.IsTrue(datacy == "cy-GB");
		}
		[Test]
		public void InvalidLanguage()
		{
			try
			{
				TestWebService testwebservice = new TestWebService();
				//invalid language
				testwebservice.LanguageTest("1234", "fr-FR");
			}
			catch (SoapException soapex)
			{
				//create namespace manager to allow XPath query of soap detail node
				XmlNamespaceManager nsmgr = new XmlNamespaceManager(new NameTable());
				nsmgr.AddNamespace("td", "TransportDirect.EnhancedExposedServices.SoapFault.V1");

				XmlNode detail = soapex.Detail;
				XmlNodeList messagelist = detail.SelectNodes("/td:Details/td:Messages/td:Message", nsmgr);
				//check that there is only 1 message
				Assert.IsTrue(messagelist.Count == 1, "Only 1 message expected");

				XmlNode message = messagelist.Item(0);
				XmlNode code = message.SelectSingleNode("td:Code", nsmgr);
				XmlNode description = message.SelectSingleNode("td:Description", nsmgr);
				//test that the correct code and description are included in the message
				Console.WriteLine(code.InnerText);
				Assert.IsTrue(code.InnerText == "15002");
				Console.WriteLine(description.InnerText);
				Assert.IsTrue(description.InnerText == EnhancedExposedServicesMessages.LanguageNotSupported);
			}
			catch (Exception ex)
			{
				Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
			}
		}	
	}
}