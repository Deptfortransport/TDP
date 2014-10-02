// *********************************************** 
// NAME                 : TestMessageValidation.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 23/01/2006
// DESCRIPTION  		: Test class for JourneyPlannerService WSDL validation attributes.
//                        In these tests, the web service is invoked over HTTP (i.e. using the web service proxy
//                        class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/JourneyPlanner/V1/Test/TestMessageValidation.cs-arc  $
//
//   Rev 1.1   Dec 13 2007 10:07:30   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:06   mturner
//Initial revision.
//
//   Rev 1.2   Feb 22 2006 10:12:54   mdambrine
//fixed the unit tests after namespace change
//
//   Rev 1.1   Feb 02 2006 17:34:16   COwczarek
//Add extra tests
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 27 2006 16:31:08   COwczarek
//Initial revision.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
using NUnit.Framework;
using System;
using System.Web.Services.Protocols;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.Test;
using System.Xml;
using TransportDirect.Common;

namespace TransportDirect.EnhancedExposedServices.JourneyPlanner.V1.Test
{
	/// <summary>
    /// Test class for JourneyPlannerService WSDL validation attributes.
    /// In these tests, the web service is invoked over HTTP (i.e. using the web service proxy
    /// class.
	/// </summary>
	[TestFixture]
	public class TestMessageValidation
	{
		private TestJourneyPlannerServiceProxyReference serviceProxy;

		#region NUnit Members
		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestInitialisation());

			//username token
			UsernameToken token = new UsernameToken(TestInitialisation.UsernamePassword, TestInitialisation.UsernamePassword, PasswordOption.SendHashed);

			//web service proxy class
			serviceProxy = new TestJourneyPlannerServiceProxyReference();
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

        /// <summary>
        /// Create a dummy request object with valid properties
        /// </summary>
        /// <returns>dummy request object</returns>
        private PublicJourneyRequest createPublicJourneyRequest() 
        {
            PublicJourneyRequest request = new PublicJourneyRequest();
            request.OutwardDateTime = new System.DateTime();
            request.OriginLocation = new RequestLocation();
			request.OriginLocation.Type = LocationType.Postcode;
            request.DestinationLocation = new RequestLocation();
			request.DestinationLocation.Type = LocationType.Postcode;
            request.Sequence = 5;
            request.MaxWalkingTime = 5;
            request.InterchangeSpeed = 0;
            request.WalkingSpeed = 40;			
            return request;
        }

        /// <summary>
        /// Invoke web service and test that exception contains expected message text 
        /// </summary>
        /// <param name="request">Journey request parameters</param>
        /// <param name="exceptionMessage">Text expected in exception</param>
        private void CheckRequestInvalidValueForDataType(PublicJourneyRequest request, string exceptionMessage) 
        {
            try 
            {
                serviceProxy.PlanPublicJourney("A","en-GB", request);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,exceptionMessage);
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
        }

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
            Assert.IsTrue(description.InnerText.IndexOf(expectedContent)> -1);
        }

        /// <summary>
        /// Test to ensure missing transaction id is handled
        /// </summary>
		[Test]
		public void MissingTransactionId()
		{
            PublicJourneyRequest request = new PublicJourneyRequest();
            request.OutwardDateTime = new System.DateTime(1000);
            request.OriginLocation = new RequestLocation();

            try 
            {
                serviceProxy.PlanPublicJourney(null, "en-GB",request);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,"TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:transactionId");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
        }

        /// <summary>
        /// Test to ensure transaction id cannot be greater that allowed length
        /// </summary>
        [Test]
        public void TransactionIdTooLong()
		{
            PublicJourneyRequest request = new PublicJourneyRequest();
            string longTransactionId = String.Empty;
			longTransactionId.PadLeft(101, 'A');

            try 
            {
                serviceProxy.PlanPublicJourney(longTransactionId, "en-GB", request);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,"TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:transactionId' element has an invalid value according to its data type");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }        
        
        }	

        /// <summary>
        /// Test to ensure transaction id cannot be zero length
        /// </summary>
        [Test]
        public void TransactionIdTooShort()
        {
            PublicJourneyRequest request = new PublicJourneyRequest();
            string shortTransactionId = String.Empty;

            try 
            {
                serviceProxy.PlanPublicJourney(shortTransactionId, "en-GB", request);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,"TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:transactionId' element has an invalid value according to its data type");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }        
        
        }	

        /// <summary>
        /// Test to ensure missing language is handled
        /// </summary>
        [Test]
        public void MissingLanguage()
		{
            PublicJourneyRequest request = new PublicJourneyRequest();

            try 
            {
                serviceProxy.PlanPublicJourney("A", null, request);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,"TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:language'");
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
            PublicJourneyRequest request = new PublicJourneyRequest();

            try 
            {
                serviceProxy.PlanPublicJourney("A","en-GB", null);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,"TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:request'");
            }
            catch (Exception ex)
            {
                Assert.Fail("Exception thrown of incorrect type. " + ex.ToString());
            }
        
        }

        /// <summary>
        /// Test to ensure InterchangeSpeed cannot be less than lower value limit
        /// </summary>
        [Test]
        public void InterchangeSpeedTooLow()
        {
            PublicJourneyRequest request = createPublicJourneyRequest();
            request.InterchangeSpeed = -4;
            CheckRequestInvalidValueForDataType(
                    request,
                    "TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:InterchangeSpeed' element has an invalid value according to its data type"
            );
        }

        /// <summary>
        /// Test to ensure InterchangeSpeed cannot be greater than upper value limit
        /// </summary>
        [Test]
        public void InterchangeSpeedTooHigh()
        {
            PublicJourneyRequest request = createPublicJourneyRequest();
            request.InterchangeSpeed = 4;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:InterchangeSpeed' element has an invalid value according to its data type"
                );
        }

        /// <summary>
        /// Test to ensure WalkingSpeed cannot be less than lower value limit
        /// </summary>
        [Test]
        public void WalkingSpeedTooLow()
        {
            PublicJourneyRequest request = createPublicJourneyRequest();
            request.WalkingSpeed = 39;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:WalkingSpeed' element has an invalid value according to its data type"
            );
        }

        /// <summary>
        /// Test to ensure WalkingSpeed cannot be greater than upper value limit
        /// </summary>
        [Test]
        public void WalkingSpeedTooHigh()
        {
            PublicJourneyRequest request = createPublicJourneyRequest();
            request.WalkingSpeed = 121;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:WalkingSpeed' element has an invalid value according to its data type"
                );
        }

        /// <summary>
        /// Test to ensure MaxWalkingTime cannot be less than lower value limit
        /// </summary>
        [Test]
        public void MaxWalkingTimeTooLow()
        {
            PublicJourneyRequest request = createPublicJourneyRequest();
            request.MaxWalkingTime = 4;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:MaxWalkingTime' element has an invalid value according to its data type"
                );
        }

        /// <summary>
        /// Test to ensure MaxWalkingTime cannot be greater than upper value limit
        /// </summary>
        [Test]
        public void MaxWalkingTimeTooHigh()
        {
            PublicJourneyRequest request = createPublicJourneyRequest();
            request.MaxWalkingTime = 31;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:MaxWalkingTime' element has an invalid value according to its data type"
                );
        }

        /// <summary>
        /// Test to ensure Sequence cannot be less than lower value limit
        /// </summary>
        [Test]
        public void SequenceTooLow()
        {
            PublicJourneyRequest request = createPublicJourneyRequest();			
            request.Sequence = 0;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:Sequence' element has an invalid value according to its data type"
                );
        }

        /// <summary>
        /// Test to ensure Sequence cannot be greater than upper value limit
        /// </summary>
        [Test]
        public void SequenceTooHigh()
        {
            PublicJourneyRequest request = createPublicJourneyRequest();
            request.Sequence = 6;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.JourneyPlanner.V1:Sequence' element has an invalid value according to its data type"
                );
        }

	}
}
