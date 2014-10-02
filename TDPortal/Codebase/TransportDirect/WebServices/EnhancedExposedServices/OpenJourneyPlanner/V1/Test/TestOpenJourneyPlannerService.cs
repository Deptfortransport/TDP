// *********************************************** 
// NAME                 : TestOpenJourneyPlannerService.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 05.04.2006
// DESCRIPTION  		: Test class for testing OpenJourneyPlannerService web service class.
//                        In these tests, the web service class is instantiated locally and not invoked
//                        over HTTP.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/OpenJourneyPlanner/V1/Test/TestOpenJourneyPlannerService.cs-arc  $
//
//   Rev 1.1   Dec 13 2007 10:16:40   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:18   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:15:24   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
using NUnit.Framework;
using System;
using System.Web.Services.Protocols;
using System.Xml;
using System.Web;
using System.Collections;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Text;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.Helpers;

using dataTransfer = TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1;
using CJPInterface = TransportDirect.JourneyPlanning.CJPInterface;
using CJP = TransportDirect.JourneyPlanning.CJP;

namespace TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1.Test
{

    /// <summary>
    /// Test stub for CJP component.
    /// </summary>
    public class TestCJP : CJP.ICJP
    {

        private int testNumber;

        public CJPInterface.Message UpdateRailData() {
            return null;
        }

        public CJPInterface.Message UpdateAirData() 
        {
            return null;
        }

        public CJPInterface.Message UpdateRoadNetwork() 
        {
            return null;
        }

        public CJPInterface.Message UpdateRoadCongestionData() 
        {
            return null;
        }

        public string GetVersonInfo() 
        {
            return null;
        }

        public string GetVersionInfo() 
        {
            return null;
        }

        /// <summary>
        /// Test method that returns varying results based on value of TestNumber property
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public CJPInterface.CJPResult JourneyPlan(CJPInterface.CJPRequest request)
        {
            switch (testNumber) 
            {
                case 0:
                    // throw a TDException
                    throw new TDException("A TD exception",false,TDExceptionIdentifier.PSMissingProperty);
                case 1:
                    // throw system exception
                    string s = null;
                    s.ToString();
                    return null;
                default:
                    // successful
                    return new CJPInterface.JourneyResult();
            }
        }

        public int TestNumber
        {
            set {testNumber = value;}
            get {return testNumber;}
        }

    }

    /// <summary>
    /// ServiceFactory that returns instances of TestCJP
    /// </summary>
    public class TestCJPFactory : IServiceFactory
    {
        private TestCJP testCJP;

        public TestCJPFactory()
        {
            testCJP = new TestCJP();
        }

        /// <summary>
        ///  Method used by the ServiceDiscovery to get an
        ///  instance of an implementation of ICJP
        /// </summary>
        /// <returns>A new instance of a CJP.</returns>
        public Object Get()
        {
            return testCJP; // return singleton so that state (testNumber) is preserved
        }

    }

    /// <summary>
    /// Intialisation class. Initialises service discovery so that instances of TestCJP are returned.
    /// </summary>
    public class TestOpenJourneyPlannerServiceInitialisation : IServiceInitialisation
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

            serviceCache.Add(ServiceDiscoveryKey.Cjp, new TestCJPFactory());

        }

    }    

    /// <summary>
    /// Test class for testing OpenJourneyPlannerService web service class.
    /// In these tests, the web service class is instantiated locally and not invoked
    /// over HTTP.
    /// </summary>
    [TestFixture]
    public class TestOpenJourneyPlannerService
    {

        #region NUnit Members
        [SetUp]
        public void Init() 
        {
            // Initialise the service discovery
            TDServiceDiscovery.Init(new TestOpenJourneyPlannerServiceInitialisation());

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
        /// Create a dummy request object with valid properties
        /// </summary>
        /// <returns>dummy request object</returns>
        private dataTransfer.Request createJourneyRequest() 
        {
            dataTransfer.Request request = new dataTransfer.Request();
            request.Depart = true;
            request.Destination = null;
            request.ModeFilter = null;
            request.OperatorFilter = null;
            request.Origin = null;
            request.PublicParameters = null;
            request.ServiceFilter = null;

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
                OpenJourneyPlannerService openJourneyPlannerService = new OpenJourneyPlannerService();

                TestCJP cjp = (TestCJP)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cjp];
                cjp.TestNumber = 0;

                openJourneyPlannerService.PlanJourney("1234","en-GB",createJourneyRequest());

                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.OpenJourneyPlannerServiceError) > -1);
                CompareSoapExceptionsDetails(soapEx,"A TD exception",TDExceptionIdentifier.PSMissingProperty);
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
                OpenJourneyPlannerService openJourneyPlannerService = new OpenJourneyPlannerService();

                TestCJP cjp = (TestCJP)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cjp];
                cjp.TestNumber = 1;

                openJourneyPlannerService.PlanJourney("1234","en-GB",createJourneyRequest());

                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.OpenJourneyPlannerServiceError) > -1);
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
                OpenJourneyPlannerService openJourneyPlannerService = new OpenJourneyPlannerService();

                TestCJP cjp = (TestCJP)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cjp];
                cjp.TestNumber = 2;

                dataTransfer.Result result = openJourneyPlannerService.PlanJourney("1234","en-GB",createJourneyRequest());

                Assert.IsNotNull(result);
            }
            catch (Exception ex)
            {
                Assert.Fail("Unexecpted exception thrown" + ex.ToString());
            }
        }	   
 
    }
}