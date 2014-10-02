// *********************************************** 
// NAME                 : TestJourneyPlannerSynchronousService.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 23/01/2006
// DESCRIPTION  		: Test class for testing JourneyPlannerSynchronousService web service class.
//                        In these tests, the web service class is instantiated locally and not invoked
//                        over HTTP.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/JourneyPlannerSynchronous/V1/Test/TestJourneyPlannerSynchronousService.cs-arc  $
//
//   Rev 1.1   Dec 13 2007 10:13:58   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:08   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:31:44   COwczarek
//Initial revision.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
using NUnit.Framework;
using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Web;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging;
using TransportDirect.EnhancedExposedServices.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.Common;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.Threading;
using TransportDirect.EnhancedExposedServices.JourneyPlanner.V1.Test;

using dataTransfer = TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;

namespace TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1.Test
{

    /// <summary>
    /// Test class for testing JourneyPlannerSynchronousService web service class.
    /// In these tests, the web service class is instantiated locally and not invoked
    /// over HTTP.
    /// </summary>
    [TestFixture]
    public class TestJourneyPlannerSynchronousService
    {

        #region NUnit Members
        [SetUp]
        public void Init() 
        {
            // Initialise the service discovery
            TDServiceDiscovery.Init(new TestJourneyPlannerServiceInitialisation());

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
	
        /// <summary>
        /// Validates code and description elements of SOAP fault
        /// </summary>
        /// <param name="soapex">The SOAP fault</param>
        /// <param name="expectedContent">A substring expected in the description</param>
        /// <param name="exceptionIdentifier">The expected code</param>
        private void CompareSoapExceptionsDetails(SoapException soapex, string expectedContent, TDExceptionIdentifier exceptionIdentifier)
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
            Assert.IsTrue(code.InnerText.Equals(((int)exceptionIdentifier).ToString()));
            Assert.IsTrue(description.InnerText.IndexOf(expectedContent)> -1);
        }

        /// <summary>
        /// Create a dummy request object
        /// </summary>
        /// <returns>dummy request object</returns>
        private dataTransfer.PublicJourneyRequest createPublicJourneyRequest() 
        {
            dataTransfer.PublicJourneyRequest request = new dataTransfer.PublicJourneyRequest();
            request.OutwardDateTime = new System.DateTime(1000);
            request.OriginLocation = new dataTransfer.RequestLocation();
            return request;
        }

        /// <summary>
        /// Test ensures web service can convert a TDException to a SoapException correctly
        /// </summary>
        [Test]
        public void HandleTDException()
        {

            try
            {
                JourneyPlannerSynchronousService journeyPlannerSynchronousService = new JourneyPlannerSynchronousService();
                TestJourneyPlannerSynchronous testJourneyPlannerSynchronous = 
                        (TestJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                testJourneyPlannerSynchronous.TestNumber = 0;
                journeyPlannerSynchronousService.PlanPublicJourney("1234","en-GB",createPublicJourneyRequest());
                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.JourneyPlannerServiceError) > -1);
                CompareSoapExceptionsDetails(soapEx,"Postcode not found, postcode=xyz",TDExceptionIdentifier.JPResolvePostcodeFailed);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
        }	   

        /// <summary>
        /// Test ensures web service can convert a non TDException to a SoapException correctly
        /// </summary>
        [Test]
        public void HandleException()
        {

            try
            {
                JourneyPlannerSynchronousService journeyPlannerSynchronousService = new JourneyPlannerSynchronousService();
                TestJourneyPlannerSynchronous testJourneyPlannerSynchronous = 
                     (TestJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                testJourneyPlannerSynchronous.TestNumber = 1;
                journeyPlannerSynchronousService.PlanPublicJourney("1234","en-GB",createPublicJourneyRequest());
                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.JourneyPlannerServiceError) > -1);
                CompareSoapExceptionsDetails(soapEx,"System.NullReferenceException: Object reference not set to an instance of an object",TDExceptionIdentifier.EESGeneralErrorCode);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
        }	   

        /// <summary>
        /// Test ensures web service can return without raising exceptions
        /// </summary>
        [Test]
        public void ValidResult()
        {
            try
            {
                JourneyPlannerSynchronousService journeyPlannerSynchronousService = new JourneyPlannerSynchronousService();
                TestJourneyPlannerSynchronous testJourneyPlannerSynchronous = 
                      (TestJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                testJourneyPlannerSynchronous.TestNumber = 2;
                dataTransfer.PublicJourneyResult result = journeyPlannerSynchronousService.PlanPublicJourney("1234","en-GB",createPublicJourneyRequest());
                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexecpted exception thrown" + ex.ToString());
            }
        }	   
 
    }
}