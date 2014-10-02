// *********************************************** 
// NAME             : TestCycleJourneyPlannerSynchronousService.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: DiscriptionPlaceholder
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/CycleJourneyPlannerSynchronous/V1/Test/TestCycleJourneyPlannerSynchronousService.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:50:38   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Web;
using NUnit.Framework;
using TransportDirect.EnhancedExposedServices.JourneyPlanner.V1.Test;
using TransportDirect.Common.ServiceDiscovery;
using System.Xml;


using cycleJPDT = TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1;
using System.Web.Services.Protocols;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.Common;
using TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1.Test;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.EnhancedExposedServices.Test;

namespace TransportDirect.EnhancedExposedServices.CycleJourneyPlannerSynchronous.V1.Test
{
    /// <summary>
    /// Test class for testing JourneyPlannerSynchronousService web service class.
    /// In these tests, the web service class is instantiated locally and not invoked
    /// over HTTP.
    /// </summary>
    [TestFixture]
    public class TestCycleJourneyPlannerSynchronousService
    {
        
        #region NUnit Members
        [SetUp]
        public void Init()
        {
            // Initialise the service discovery
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
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

        #region Private methods

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
            Assert.IsTrue(description.InnerText.IndexOf(expectedContent) > -1);
        }

        /// <summary>
        /// Create a dummy request object
        /// </summary>
        /// <returns>dummy request object</returns>
        private cycleJPDT.CycleJourneyRequest CreateCycleJourneyRequest()
        {
            cycleJPDT.CycleJourneyRequest request = new cycleJPDT.CycleJourneyRequest();

            request.JourneyRequest = new cycleJPDT.JourneyRequest();
            request.JourneyRequest.OutwardDateTime = new System.DateTime(1000);
            request.JourneyRequest.OriginLocation = new cycleJPDT.RequestLocation();
            request.JourneyRequest.DestinationLocation = new cycleJPDT.RequestLocation();

            return request;
        }

        #endregion

        #region Tests

        /// <summary>
        /// Test ensures web service can convert a TDException to a SoapException correctly
        /// </summary>
        [Test]
        public void HandleTDException()
        {
            try
            {
                CycleJourneyPlannerSynchronousService cycleJourneyPlannerSynchronousService = new CycleJourneyPlannerSynchronousService();

                TestJourneyPlannerSynchronous testJourneyPlannerSynchronous = (TestJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                testJourneyPlannerSynchronous.TestNumber = 0;

                cycleJourneyPlannerSynchronousService.PlanCycleJourney("1234", CreateCycleJourneyRequest());

                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError) > -1);

                CompareSoapExceptionsDetails(soapEx, "No journey requests were found in the CycleJourneyRequest.", TDExceptionIdentifier.JPMissingJourneyInRequest);
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
                CycleJourneyPlannerSynchronousService cycleJourneyPlannerSynchronousService = new CycleJourneyPlannerSynchronousService();

                TestJourneyPlannerSynchronous testJourneyPlannerSynchronous = (TestJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                testJourneyPlannerSynchronous.TestNumber = 1;

                cycleJourneyPlannerSynchronousService.PlanCycleJourney("1234", CreateCycleJourneyRequest());

                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.CarJourneyPlannerServiceError) > -1);
                CompareSoapExceptionsDetails(soapEx, "System.NullReferenceException: Object reference not set to an instance of an object", TDExceptionIdentifier.EESGeneralErrorCode);
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
                CycleJourneyPlannerSynchronousService cycleJourneyPlannerSynchronousService = new CycleJourneyPlannerSynchronousService();

                TestJourneyPlannerSynchronous testJourneyPlannerSynchronous = (TestJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                testJourneyPlannerSynchronous.TestNumber = 2;

                cycleJPDT.CycleJourneyResult result = cycleJourneyPlannerSynchronousService.PlanCycleJourney("1234", CreateCycleJourneyRequest());

                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexecpted exception thrown" + ex.ToString());
            }
        }

        #endregion
    }
}
