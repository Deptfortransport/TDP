// *********************************************** 
// NAME                 : TestJourneyPlannerService.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 23/01/2006
// DESCRIPTION  		: Test class for testing JourneyPlannerService web service class.
//                        In these tests, the web service class is instantiated locally and not invoked
//                        over HTTP.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/JourneyPlanner/V1/Test/TestJourneyPlannerService.cs-arc  $
//
//   Rev 1.2   Aug 04 2009 14:29:36   mmodi
//Updated for Car journey planner exposed service
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.1   Dec 13 2007 10:07:30   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:04   mturner
//Initial revision.
//
//   Rev 1.1   Mar 15 2006 17:48:48   RWilby
//Fix for batch unit test run on build machine.
//
//   Rev 1.0   Jan 27 2006 16:31:06   COwczarek
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
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Text;
using TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1.Test;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;

using dataTransfer = TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;

namespace TransportDirect.EnhancedExposedServices.JourneyPlanner.V1.Test
{

    public class TestJourneyPlannerServiceInitialisation : IServiceInitialisation
    {		

        private const string DefaultLogFilename = "td.enhancedexposedservices.log";

        public void Populate(Hashtable serviceCache)
        {

            // nasty bodge to make date validation checks independent of the 
            //  date format of the user/machine the tests are run on ...
            CultureInfo ci = new CultureInfo("en-GB");
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = "dd MM yyyy";
            ci.DateTimeFormat = dtfi;
            Thread.CurrentThread.CurrentCulture = ci;

            TextWriterTraceListener logTextListener = null;
            ArrayList errors = new ArrayList();

            try
            {
                // initialise .NET file trace listener for use prior to TDTraceListener
                string logfilePath = ConfigurationManager.AppSettings[EnhancedExposedServicesKey.DefaultLogPath];
                Stream logFile = File.Create(logfilePath + "\\" + DefaultLogFilename);
                logTextListener = new System.Diagnostics.TextWriterTraceListener(logFile);
                Trace.Listeners.Add(logTextListener);
		
                // Add cryptographic scheme
                serviceCache.Add( ServiceDiscoveryKey.Crypto,  new CryptoFactory() );

                // initialise properties service
                serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

                // initialise logging service	
                IEventPublisher[]	customPublishers = new IEventPublisher[0];			
                Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));			

            }
            catch (TDException tdException)
            {	
                // create message string
                StringBuilder message = new StringBuilder(100);
                message.Append(tdException.Message);

                // append error messages, if any
                foreach( string error in errors )
                {
                    message.Append(error);
                    message.Append(" ");	
                }
                string logMessage = "{0} ExceptionID: {1}";
				
                Trace.WriteLine(string.Format(logMessage,  message.ToString(), tdException.Identifier.ToString("D")));		
                throw new TDException(message.ToString(), tdException, false, tdException.Identifier);
            }
            catch (Exception exception)
            {
                Trace.WriteLine(exception.Message);
                throw exception;
            }
            finally
            {
                if( logTextListener != null )
                {
                    logTextListener.Flush();
                    logTextListener.Close();
                    Trace.Listeners.Remove(logTextListener);
                }
            }

            serviceCache.Add(ServiceDiscoveryKey.JourneyPlannerSynchronousService, new TestJourneyPlannerSynchronousFactory());
            serviceCache.Add(ServiceDiscoveryKey.JourneyPlannerService, new TestJourneyPlannerFactory());

            serviceCache.Add(ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory());
            serviceCache.Add(ServiceDiscoveryKey.CarCostCalculator, new CarCostCalculatorFactory());
        }

    }    
    
    /// <summary>
    /// Test class for testing JourneyPlannerService web service class.
    /// In these tests, the web service class is instantiated locally and not invoked
    /// over HTTP.
    /// </summary>
    [TestFixture]
    public class TestJourneyPlannerService
    {

        #region NUnit Members
        [SetUp]
        public void Init() 
        {
			//Fix for batch unit test run on build machine
			TDServiceDiscovery.ResetServiceDiscoveryForTest();

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
                JourneyPlannerService journeyPlannerService = new JourneyPlannerService();
                TestJourneyPlanner testJourneyPlanner = 
                        (TestJourneyPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerService];
                testJourneyPlanner.TestNumber = 0;
                
                journeyPlannerService.PlanPublicJourney("1234","en-GB",createPublicJourneyRequest());
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
                JourneyPlannerService journeyPlannerService = new JourneyPlannerService();
                TestJourneyPlanner testJourneyPlanner = 
                        (TestJourneyPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerService];
                testJourneyPlanner.TestNumber = 1;
                journeyPlannerService.PlanPublicJourney("1234","en-GB",createPublicJourneyRequest());
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
                JourneyPlannerService journeyPlannerService = new JourneyPlannerService();
                TestJourneyPlanner testJourneyPlanner = 
                        (TestJourneyPlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerService];
                testJourneyPlanner.TestNumber = 2;
                journeyPlannerService.PlanPublicJourney("1234","en-GB",createPublicJourneyRequest());
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexecpted exception thrown" + ex.ToString());
            }
        }	   
 
    }
}