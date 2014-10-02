// *********************************************** 
// NAME             : TestMessageValidation.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: DiscriptionPlaceholder
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/CycleJourneyPlannerSynchronous/V1/Test/TestMessageValidation.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:50:38   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Web;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Test;
using Microsoft.Web.Services3.Security.Tokens;
using System.Xml;
using System.Web.Services.Protocols;
using TransportDirect.Common;
using TransportDirect.EnhancedExposedServices.Helpers;

namespace TransportDirect.EnhancedExposedServices.CycleJourneyPlannerSynchronous.V1.Test
{
    /// <summary>
    /// Test class for JourneyPlannerSynchronousService WSDL validation attributes.
    /// In these tests, the web service is invoked over HTTP (i.e. using the web service proxy
    /// class.
    /// </summary>
    [TestFixture]
    public class TestMessageValidation
    {
        private TestCycleJourneyPlannerSyncServiceProxyReference serviceProxy;

        #region NUnit Members
        [SetUp]
        public void Init()
        {
            // Initialise the service discovery
            TDServiceDiscovery.Init(new TestInitialisation());

            //username token
            UsernameToken token = new UsernameToken(TestInitialisation.UsernamePassword, TestInitialisation.UsernamePassword, PasswordOption.SendHashed);


            //web service proxy class
            serviceProxy = new TestCycleJourneyPlannerSyncServiceProxyReference();
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

        #region Private methods

        /// <summary>
        /// Validates code and description elements of SOAP fault
        /// </summary>
        /// <param name="soapex">The SOAP fault</param>
        /// <param name="expectedContent">A substring expected in the description</param>
        private void CompareSoapExceptionsDetails(SoapException soapex, string expectedContent)
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
            Assert.IsTrue(code.InnerText.Equals(((int)TDExceptionIdentifier.EESWSDLRequestValidationFailed).ToString()));
            Assert.IsTrue(description.InnerText.IndexOf(expectedContent) > -1);
        }

        #endregion

        #region Tests

        /// <summary>
        /// Test to ensure missing transaction id is handled
        /// </summary>
        [Test]
        public void MissingTransactionId()
        {
            CycleJourneyRequest request = new CycleJourneyRequest();

            #region PlanCycleJourney
            try
            {
                serviceProxy.PlanCycleJourney(null, request);
                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                // If wsdl catches error then expect this
                bool correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1;

                // If request makes it to the car web service, then expect this error
                if (!correctMessage)
                {
                    correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError) > -1;
                }

                Assert.IsTrue(correctMessage);
                CompareSoapExceptionsDetails(soapEx, "TransactionId");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
            #endregion

            #region GetCycleAlgorithms
            try
            {
                serviceProxy.GetCycleAlgorithms(null);
                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                // If wsdl catches error then expect this
                bool correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1;

                // If request makes it to the car web service, then expect this error
                if (!correctMessage)
                {
                    correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError) > -1;
                }

                Assert.IsTrue(correctMessage);
                CompareSoapExceptionsDetails(soapEx, "TransactionId");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
            #endregion

            #region GetCycleAttributes
            try
            {
                serviceProxy.GetCycleAttributes(null);
                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                // If wsdl catches error then expect this
                bool correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1;

                // If request makes it to the car web service, then expect this error
                if (!correctMessage)
                {
                    correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError) > -1;
                }

                Assert.IsTrue(correctMessage);
                CompareSoapExceptionsDetails(soapEx, "TransactionId");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
            #endregion

            #region GetCycleRequestPreferences
            try
            {
                serviceProxy.GetCycleRequestPreferences(null);
                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                // If wsdl catches error then expect this
                bool correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1;

                // If request makes it to the car web service, then expect this error
                if (!correctMessage)
                {
                    correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError) > -1;
                }

                Assert.IsTrue(correctMessage);
                CompareSoapExceptionsDetails(soapEx, "TransactionId");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
            #endregion
        }

        /// <summary>
        /// Test to ensure transaction id cannot be zero length
        /// </summary>
        [Test]
        public void TransactionIdTooShort()
        {
            CycleJourneyRequest request = new CycleJourneyRequest();
            string shortTransactionId = String.Empty;

            try
            {
                serviceProxy.PlanCycleJourney(shortTransactionId, request);
                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                // If wsdl catches error then expect this
                bool correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1;

                // If request makes it to the car web service, then expect this error
                if (!correctMessage)
                {
                    correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError) > -1;
                }

                Assert.IsTrue(correctMessage);

                CompareSoapExceptionsDetails(soapEx, "TransactionId");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
        }

        /// <summary>
        /// Test to ensure missing journey request is handled
        /// </summary>
        [Test]
        public void MissingRequest()
        {
            try
            {
                serviceProxy.PlanCycleJourney("A", null);
                Assert.Fail("Expected exception not thrown");
            }
            catch (SoapException soapEx)
            {
                // If wsdl catches error then expect this
                bool correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1;

                // If request makes it to the car web service, then expect this error
                if (!correctMessage)
                {
                    correctMessage = soapEx.Message.IndexOf(EnhancedExposedServicesMessages.CycleJourneyPlannerServiceError) > -1;
                }

                Assert.IsTrue(correctMessage);
                CompareSoapExceptionsDetails(soapEx, "Request");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
        }

        #endregion
    }
}
