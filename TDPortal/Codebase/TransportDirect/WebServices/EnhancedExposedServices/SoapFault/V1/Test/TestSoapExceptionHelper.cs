// *********************************************** 
// NAME                 : TestSoapExceptionHelper.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 11/01/2006
// DESCRIPTION  		: Test class for the SoapExceptionHelper methods
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/SoapFault/V1/Test/TestSoapExceptionHelper.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 13:52:20   mturner
//Initial revision.
//
//   Rev 1.4   Jan 27 2006 16:28:12   COwczarek
//Modified test of overloaded ThrowClientSoapException method to cater for new signature
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 20 2006 14:07:36   mtillett
//Add new test method for new TDException soapHelper
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.2   Jan 11 2006 11:30:16   mtillett
//Clear down after unit test correctly
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.1   Jan 11 2006 11:17:24   mtillett
//Updates to description
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.0   Jan 11 2006 11:16:30   mtillett
//Initial revision.
//

using NUnit.Framework;
using System;
using System.Xml;
using System.Web;
using System.Web.Services.Protocols;
using TransportDirect.Common;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;      
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.EnhancedExposedServices.SoapFault.V1.Test
{
	/// <summary>
	/// Summary description for TestSoapExceptionHelper.
	/// </summary>
	[TestFixture]
	public class TestSoapExceptionHelper
	{
		private const string RequestUri = "http://www.transportdirect.info/Test/test.asmx";
		private const string TestErrorMessage = "TestErrorMessage";

		private XmlNamespaceManager nsmgr;

		#region NUnit Members
		[SetUp]
		public void Init() 
		{
			HttpRequest req = new HttpRequest("", RequestUri, "");
			HttpContext.Current = new HttpContext(req, new HttpResponse(new System.IO.StringWriter()));

			//create namespace manager to allow XPath query of soap detail node
			nsmgr = new XmlNamespaceManager(new NameTable());
			nsmgr.AddNamespace("td", "TransportDirect.EnhancedExposedServices.SoapFault.V1");
		}

		/// <summary>
		/// Finalisation method called after every test method
		/// </summary>
		[TearDown]
		public void TearDown() 
		{
			HttpContext.Current = null;
		}
		#endregion

		[Test]
		public void ThrowSoapExceptionThreeStringsOverload()
		{
			SoapException soapex = SoapExceptionHelper.ThrowSoapException(TestErrorMessage, "1000", "testdescription");
			Assert.IsTrue(soapex.Message == TestErrorMessage);
			Assert.IsTrue(soapex.Actor == RequestUri);

			//test message status
			Assert.IsTrue(GetStatus(soapex.Detail) == "Failed");
			//test Messages Node
			XmlNodeList messagelist = GetMessages(soapex.Detail);
			Assert.IsTrue(messagelist.Count == 1);
			//test that the correct code and description are included in the message
			XmlNode message = messagelist.Item(0);
			XmlNode code = message.SelectSingleNode("td:Code", nsmgr);
			XmlNode description = message.SelectSingleNode("td:Description", nsmgr);
			Assert.IsTrue(code.InnerText == "1000");
			Assert.IsTrue(description.InnerText == "testdescription");
		}

		[Test]
		public void ThrowSoapExceptionTDExceptionOverload()
		{
			TDException tdex = new TDException("testmessage", new Exception("innertestmessage"), true, TDExceptionIdentifier.Undefined);
			SoapException soapex = SoapExceptionHelper.ThrowSoapException(TestErrorMessage, tdex);
			Assert.IsTrue(soapex.Message == TestErrorMessage);
			Assert.IsTrue(soapex.Actor == RequestUri);

			//test message status
			Assert.IsTrue(GetStatus(soapex.Detail) == "Failed");
			//test Messages Node
			XmlNodeList messagelist = GetMessages(soapex.Detail);
			Assert.IsTrue(messagelist.Count == 1);
			//test that the correct code and description are included in the message
			XmlNode message = messagelist.Item(0);
			XmlNode code = message.SelectSingleNode("td:Code", nsmgr);
			XmlNode description = message.SelectSingleNode("td:Description", nsmgr);
			Assert.IsTrue(code.InnerText == "-1");
			Assert.IsTrue(description.InnerText == "testmessage");
		}
		
		[Test]
		public void ThrowSoapExceptionTDExceptionAdditionalInformationOverload()
		{
			CJPMessage[] cjpmessages = new CJPMessage[2];
			cjpmessages[0] = new CJPMessage( "Message1", "ResourceId", 101, 1);
			cjpmessages[1] = new CJPMessage( "Message2", "ResourceId", 102, 1);
			TDException tdex = new TDException("testmessage", true, TDExceptionIdentifier.JPResolvePostcodeFailed, cjpmessages);
			SoapException soapex = SoapExceptionHelper.ThrowSoapException(TestErrorMessage, tdex);
			Assert.IsTrue(soapex.Message == TestErrorMessage);
			Assert.IsTrue(soapex.Actor == RequestUri);

			//test message status
			Assert.IsTrue(GetStatus(soapex.Detail) == "ValidationError");
			//test Messages Node
			XmlNodeList messagelist = GetMessages(soapex.Detail);
			Assert.IsTrue(messagelist.Count == 2);
			//test that the correct code and description are included in the message
			XmlNode message = messagelist.Item(0);
			XmlNode code = message.SelectSingleNode("td:Code", nsmgr);
			XmlNode description = message.SelectSingleNode("td:Description", nsmgr);
			Assert.IsTrue(code.InnerText == "101");
			Assert.IsTrue(description.InnerText == "Message1");
		}
		
		[Test]
		public void ThrowSoapExceptionExceptionOverload()
		{
			Exception ex = new Exception("testmessageforexception");
			SoapException soapex = SoapExceptionHelper.ThrowSoapException(TestErrorMessage, ex);
			Assert.IsTrue(soapex.Message == TestErrorMessage);
			Assert.IsTrue(soapex.Actor == RequestUri);

			//test message status
			Assert.IsTrue(GetStatus(soapex.Detail) == "Failed");
			//test Messages Node
			XmlNodeList messagelist = GetMessages(soapex.Detail);
			Assert.IsTrue(messagelist.Count == 1);
			//test that the correct code and description are included in the message
			XmlNode message = messagelist.Item(0);
			XmlNode code = message.SelectSingleNode("td:Code", nsmgr);
			XmlNode description = message.SelectSingleNode("td:Description", nsmgr);
			Assert.IsTrue(code.InnerText == "15002");
			Assert.IsTrue(description.InnerText.IndexOf("testmessageforexception") > 0);
		}
		
		[Test]
		public void ThrowSoapExceptionDBSMessageOverload()
		{
			DBSMessage[] dbs = new DBSMessage[2];
			DBSMessage dbs0 = new DBSMessage();
			dbs0.Code = 123;
			dbs0.Description = "123description";
			dbs[0] = dbs0;
			DBSMessage dbs1 = new DBSMessage();
			dbs1.Code = 456;
			dbs1.Description = "456description";
			dbs[1] = dbs1;
			SoapException soapex = SoapExceptionHelper.ThrowSoapException(TestErrorMessage, dbs);
			Assert.IsTrue(soapex.Message == TestErrorMessage);
			Assert.IsTrue(soapex.Actor == RequestUri);

			//test message status
			Assert.IsTrue(GetStatus(soapex.Detail) == "Failed");
			//test Messages Node
			XmlNodeList messagelist = GetMessages(soapex.Detail);
			Assert.IsTrue(messagelist.Count == 2);
			//test that the correct code and description are included in message 1
			XmlNode message1 = messagelist.Item(0);
			XmlNode code1 = message1.SelectSingleNode("td:Code", nsmgr);
			XmlNode description1 = message1.SelectSingleNode("td:Description", nsmgr);
			Assert.IsTrue(code1.InnerText == "123");
			Assert.IsTrue(description1.InnerText == "123description");
			//test that the correct code and description are included in message 2
			XmlNode message2 = messagelist.Item(1);
			XmlNode code2 = message2.SelectSingleNode("td:Code", nsmgr);
			XmlNode description2 = message2.SelectSingleNode("td:Description", nsmgr);
			Assert.IsTrue(code2.InnerText == "456");
			Assert.IsTrue(description2.InnerText == "456description");
		}
		
		[Test]
		public void ThrowClientSoapExceptionThreeStringsOverload()
		{
			SoapException soapex = SoapExceptionHelper.ThrowClientSoapException(TestErrorMessage, "1000", "testdescription");
			Assert.IsTrue(soapex.Message == TestErrorMessage);
			Assert.IsTrue(soapex.Actor == RequestUri);

			//test message status
			Assert.IsTrue(GetStatus(soapex.Detail) == "Failed");
			//test Messages Node
			XmlNodeList messagelist = GetMessages(soapex.Detail);
			Assert.IsTrue(messagelist.Count == 1);
			//test that the correct code and description are included in the message
			XmlNode message = messagelist.Item(0);
			XmlNode code = message.SelectSingleNode("td:Code", nsmgr);
			XmlNode description = message.SelectSingleNode("td:Description", nsmgr);
			Assert.IsTrue(code.InnerText == "1000");
			Assert.IsTrue(description.InnerText == "testdescription");
		}

		[Test]
		public void ThrowClientSoapExceptionExceptionOverload()
		{
            TDException ex = new  TDException("testmessageforexception",false,TDExceptionIdentifier.EESGeneralErrorCode);
            SoapException soapex = SoapExceptionHelper.ThrowClientSoapException(TestErrorMessage, ex);
            Assert.IsTrue(soapex.Message == TestErrorMessage);
			Assert.IsTrue(soapex.Actor == RequestUri);

			//test message status
			Assert.IsTrue(GetStatus(soapex.Detail) == "Failed");
			//test Messages Node
			XmlNodeList messagelist = GetMessages(soapex.Detail);
			Assert.IsTrue(messagelist.Count == 1);
			//test that the correct code and description are included in the message
			XmlNode message = messagelist.Item(0);
			XmlNode code = message.SelectSingleNode("td:Code", nsmgr);
			XmlNode description = message.SelectSingleNode("td:Description", nsmgr);
			Assert.IsTrue(code.InnerText == "15002");
			Assert.IsTrue(description.InnerText.IndexOf("testmessageforexception") > 0);
		}

		#region Private Helpers
		/// <summary>
		/// Gets the Status from the Details fault block
		/// </summary>
		/// <param name="detail"></param>
		/// <returns></returns>
		private string GetStatus(XmlNode detail)
		{
			XmlNode status = detail.SelectSingleNode("/td:Details/td:Status", nsmgr);
			return status.InnerText;
		}
		/// <summary>
		/// Get the Messages list from the Details fault block
		/// </summary>
		/// <param name="detail"></param>
		/// <returns></returns>
		private XmlNodeList GetMessages(XmlNode detail)
		{
			return detail.SelectNodes("/td:Details/td:Messages/td:Message", nsmgr);
		}
		#endregion
	}
}
