// *********************************************** 
// NAME                 : TestMessageValidation.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 05/04/2006
// DESCRIPTION  		: Test class for OpenJourneyPlannerService WSDL validation attributes.
//                        In these tests, the web service is invoked over HTTP (i.e. using the web service proxy
//                        class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/OpenJourneyPlanner/V1/Test/TestMessageValidation.cs-arc  $
//
//   Rev 1.1   Dec 13 2007 10:16:40   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 13:52:16   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:15:22   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
using NUnit.Framework;
using System;
using System.Web.Services.Protocols;
using Microsoft.Web.Services3.Security.Tokens;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Helpers;
using TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.Test;
using System.Xml;
using TransportDirect.Common;

namespace TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1.Test
{
	/// <summary>
    /// Test class for OpenJourneyPlannerService WSDL validation attributes.
    /// In these tests, the web service is invoked over HTTP (i.e. using the web service proxy
    /// class.
	/// </summary>
	[TestFixture]
	public class TestMessageValidation
	{
		private TestOpenJourneyPlannerServiceProxyReference serviceProxy;

		#region NUnit Members
		[SetUp]
		public void Init() 
		{
			// Initialise the service discovery
			TDServiceDiscovery.Init(new TestInitialisation());

			//username token
			UsernameToken token = new UsernameToken(TestInitialisation.UsernamePassword, TestInitialisation.UsernamePassword, PasswordOption.SendHashed);

			//web service proxy class
			serviceProxy = new TestOpenJourneyPlannerServiceProxyReference();
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
        private Request createJourneyRequest() 
        {
            RequestStop[] requestStops1 = new RequestStop[1];
            requestStops1[0] = new RequestStop();
            requestStops1[0].TimeDate = new DateTime(2006,4,29,10,00,0);
            requestStops1[0].NaPTANID = "9100KNGX";
            requestStops1[0].Coordinate = null;

            RequestStop[] requestStops2 = new RequestStop[1];
            requestStops2[0] = new RequestStop();
            requestStops2[0].TimeDate = new DateTime(2006,5,2,15,00,0);
            requestStops2[0].NaPTANID = "9100NWCSTLE";
            requestStops2[0].Coordinate = null;


            RequestPlace requestPlace1 = new RequestPlace();
            requestPlace1.Type = RequestPlaceType.NaPTAN;
            requestPlace1.GivenName = "Kings Cross";
            requestPlace1.Locality = "E0034577";
            requestPlace1.Stops = requestStops1;
            requestPlace1.Coordinate = null;

            RequestPlace requestPlace2 = new RequestPlace();
            requestPlace2.Type = RequestPlaceType.NaPTAN;
            requestPlace2.GivenName = "Newcastle";
            requestPlace2.Locality = "E0057900";
            requestPlace2.Stops = requestStops2;
            requestPlace2.Coordinate = null;

            TransportModes transportModes = new TransportModes();
            transportModes.Include = true;
            transportModes.Modes = new TransportMode[2];
            transportModes.Modes[0] = new TransportMode();
            transportModes.Modes[1] = new TransportMode();
            transportModes.Modes[0].Mode = TransportModeType.Coach;
            transportModes.Modes[1].Mode = TransportModeType.Rail;

            PublicParameters publicParameters = new PublicParameters();

            publicParameters.Algorithm = PublicAlgorithmType.Fastest;
            publicParameters.ExtraCheckInTime = DateTime.MinValue;
            publicParameters.ExtraCheckInTimeSpecified = false;
            publicParameters.InterchangeSpeed = 1;
            publicParameters.IntermediateStops = IntermediateStopsType.All;
            publicParameters.Interval = DateTime.MinValue;
            publicParameters.IntervalSpecified = false;
            publicParameters.MaxWalkDistance = 100;
            publicParameters.NotVias = null;
            publicParameters.RangeType = RangeType.Sequence;
            publicParameters.Sequence = 3;
            publicParameters.SequenceSpecified = true;
            publicParameters.SoftVias = null;
            publicParameters.TrunkPlan = false;
            publicParameters.Vias = null;
            publicParameters.WalkSpeed = 80;

            Request request = new Request();
            request.Depart = true;
            request.Destination = requestPlace1;
            request.ModeFilter = transportModes;
            request.OperatorFilter = null;
            request.Origin = requestPlace2;
            request.PublicParameters = publicParameters;
            request.ServiceFilter = null;

            return request;
        }

        /// <summary>
        /// Invoke web service and test that exception contains expected message text 
        /// </summary>
        /// <param name="request">Journey request parameters</param>
        /// <param name="exceptionMessage">Text expected in exception</param>
        private void CheckRequestInvalidValueForDataType(Request request, string exceptionMessage) 
        {
            try 
            {
                serviceProxy.PlanJourney("A","en-GB", request);
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
            Request request = createJourneyRequest();

            try 
            {
                serviceProxy.PlanJourney(null, "en-GB",request);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,"TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:transactionId");
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
            Request request = createJourneyRequest();
            string longTransactionId = String.Empty;
			longTransactionId.PadLeft(101, 'A');

            try 
            {
                serviceProxy.PlanJourney(longTransactionId, "en-GB", request);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,"TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:transactionId' element has an invalid value according to its data type");
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
            Request request = createJourneyRequest();
            string shortTransactionId = String.Empty;

            try 
            {
                serviceProxy.PlanJourney(shortTransactionId, "en-GB", request);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,"TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:transactionId' element has an invalid value according to its data type");
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
            Request request = createJourneyRequest();

            try 
            {
                serviceProxy.PlanJourney("A", null, request);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,"TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:language'");
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
                serviceProxy.PlanJourney("A","en-GB", null);
                Assert.Fail("Expected exception not thrown");
            } 
            catch (SoapException soapEx)
            {
                Assert.IsTrue(soapEx.Message.IndexOf(EnhancedExposedServicesMessages.WSDLRequestValidationError) > -1);
                CompareSoapExceptionsDetails(soapEx,"TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:request'");
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
            Request request = createJourneyRequest();
            request.PublicParameters.InterchangeSpeed = -4;
            CheckRequestInvalidValueForDataType(
                    request,
                    "TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:InterchangeSpeed' element has an invalid value according to its data type"
            );
        }

        /// <summary>
        /// Test to ensure InterchangeSpeed cannot be greater than upper value limit
        /// </summary>
        [Test]
        public void InterchangeSpeedTooHigh()
        {
            Request request = createJourneyRequest();
            request.PublicParameters.InterchangeSpeed = 4;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:InterchangeSpeed' element has an invalid value according to its data type"
                );
        }

        /// <summary>
        /// Test to ensure WalkSpeed cannot be less than lower value limit
        /// </summary>
        [Test]
        public void WalkingSpeedTooLow()
        {
            Request request = createJourneyRequest();
            request.PublicParameters.WalkSpeed = 39;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:WalkSpeed' element has an invalid value according to its data type"
            );
        }

        /// <summary>
        /// Test to ensure WalkSpeed cannot be greater than upper value limit
        /// </summary>
        [Test]
        public void WalkingSpeedTooHigh()
        {
            Request request = createJourneyRequest();
            request.PublicParameters.WalkSpeed = 121;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:WalkSpeed' element has an invalid value according to its data type"
                );
        }

        /// <summary>
        /// Test to ensure Sequence cannot be less than lower value limit
        /// </summary>
        [Test]
        public void SequenceTooLow()
        {
            Request request = createJourneyRequest();			
            request.PublicParameters.Sequence = 0;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:Sequence' element has an invalid value according to its data type"
                );
        }

        /// <summary>
        /// Test to ensure Sequence cannot be greater than upper value limit
        /// </summary>
        [Test]
        public void SequenceTooHigh()
        {
            Request request = createJourneyRequest();
            request.PublicParameters.Sequence = 6;
            CheckRequestInvalidValueForDataType(
                request,
                "TransportDirect.EnhancedExposedServices.OpenJourneyPlanner.V1:Sequence' element has an invalid value according to its data type"
                );
        }

	}
}
