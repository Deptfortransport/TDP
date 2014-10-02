// *********************************************** 
// NAME                 : TestJourneyPlannerSynchronousService.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 03/08/2009
// DESCRIPTION  		: Test class for testing JourneyPlannerSynchronousService web service class.
//                        In these tests, the web service class is instantiated locally and not invoked
//                        over HTTP.
//                      : Tests the PlanPrivateJourney method of the journey planner service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/CarJourneyPlannerSynchronous/V1/Test/TestCarJourneyPlannerSynchronousService.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 15:02:20   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Data;
using System.Configuration;
using System.Xml;
using System.Web;
using System.Web.Security;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.JourneyPlanner.V1.Test;
using TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1.Test;

using carjpDT = TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1;

namespace TransportDirect.EnhancedExposedServices.CarJourneyPlannerSynchronous.V1.Test
{
    /// <summary>
    /// Test class for testing JourneyPlannerSynchronousService web service class.
    /// In these tests, the web service class is instantiated locally and not invoked
    /// over HTTP.
    /// </summary>
    [TestFixture]
    public class TestCarJourneyPlannerSynchronousService
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
        private carjpDT.CarJourneyRequest CreateCarJourneyRequest()
        {
            carjpDT.CarJourneyRequest request = new carjpDT.CarJourneyRequest();

            request.JourneyRequests = new carjpDT.JourneyRequest[1];
            request.JourneyRequests[0] = new carjpDT.JourneyRequest();
            request.JourneyRequests[0].OutwardDateTime = new System.DateTime(1000);
            request.JourneyRequests[0].OriginLocation = new carjpDT.RequestLocation();
            request.JourneyRequests[0].DestinationLocation = new carjpDT.RequestLocation();
                        
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
                CarJourneyPlannerSynchronousService carJourneyPlannerSynchronousService = new CarJourneyPlannerSynchronousService();

                TestJourneyPlannerSynchronous testJourneyPlannerSynchronous = (TestJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                testJourneyPlannerSynchronous.TestNumber = 0;

                carJourneyPlannerSynchronousService.PlanCarJourney("1234", CreateCarJourneyRequest());
                
                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.CarJourneyPlannerServiceError) > -1);

                CompareSoapExceptionsDetails(soapEx, "No journey requests were found in the CarJourneyRequest.", TDExceptionIdentifier.JPMissingJourneyInRequest);
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
                CarJourneyPlannerSynchronousService carJourneyPlannerSynchronousService = new CarJourneyPlannerSynchronousService();

                TestJourneyPlannerSynchronous testJourneyPlannerSynchronous = (TestJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                testJourneyPlannerSynchronous.TestNumber = 1;

                carJourneyPlannerSynchronousService.PlanCarJourney("1234", CreateCarJourneyRequest());

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
                CarJourneyPlannerSynchronousService carJourneyPlannerSynchronousService = new CarJourneyPlannerSynchronousService();

                TestJourneyPlannerSynchronous testJourneyPlannerSynchronous = (TestJourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
                testJourneyPlannerSynchronous.TestNumber = 2;

                carjpDT.CarJourneyResult result = carJourneyPlannerSynchronousService.PlanCarJourney("1234", CreateCarJourneyRequest());

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
