// ***********************************************
// NAME 		: TestJourneyRequestHelper.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 12/01/2006
// DESCRIPTION 	: testclass for ther journeyrequesthelper class
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/Test/TestJourneyRequestHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:40   mturner
//Initial revision.
//
//   Rev 1.10   Jul 12 2006 16:22:26   rphilpott
//Update tests for update sequence number.
//Resolution for 4126: Not returning best journey to Lauren
//
//   Rev 1.9   Feb 20 2006 17:02:08   mdambrine
//added soapactions because the access restriction changes made these tests fail.
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions
//
//   Rev 1.8   Feb 02 2006 14:43:24   mdambrine
//rework on the journey planner enhanced exposed services see CR053_IR_3407 Journey Planner Service Component.doc 
//
//   Rev 1.7   Jan 24 2006 09:50:54   mdambrine
//Changed from a file to a database property provider
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Jan 20 2006 14:42:34   mdambrine
//changed the consumer to seperate project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Jan 19 2006 17:32:58   mdambrine
//Add new test 
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Jan 18 2006 12:06:06   mdambrine
//testing the sendresult methods
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 16 2006 17:10:50   mdambrine
//now referencing the CJPmanager in the journeycontrol project.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 16 2006 14:47:56   mdambrine
//Change in the way the stubCJPmanager works. extra property filename
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 16 2006 14:08:12   mdambrine
//Added aditional tests
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 13 2006 18:26:08   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//


using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Configuration;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;


using Logger = System.Diagnostics.Trace;

using NUnit.Framework;

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.Common;

using TransportDirect.Common.ResourceManager;
using TransportDirect.EnhancedExposedServiceConsumers.JourneyResultConsumer.V1;
using TransportDirect.EnhancedExposedServiceConsumers.JourneyConsumer.V1;

namespace TransportDirect.UserPortal.JourneyPlannerService
{
	
	/// <summary>
	/// Summary description for TestJourneyPlannerSynchronous.
	/// </summary>
	[TestFixture]
	public class TestJourneyRequestHelper
	{		
		private PublicJourneyRequest publicJourneyRequest;
		private ExposedServiceContext context;
		private JourneyRequestHelper helper;
		private ITDJourneyRequest TDJourneyRequest;

		public TestJourneyRequestHelper()
		{
		}
	
		#region setup
		[SetUp]
		public void SetUp()
		{
			// Initialise the service discovery
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestJourneyPlannerSynchronousInitialisation( ));
			IPropertyProvider property = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

			Trace.Listeners.Remove("TDTraceListener");
		
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException)
			{
				Assert.IsTrue(false);
			}

			// Setup Journey Parameters
			// Outward time is one month from now
			DateTime outwardTime = DateTime.Now.AddMonths(1);

			// return time is 5.5 hours after the outward time
			DateTime returnTime = outwardTime.AddSeconds(5.5 * 3600);  
			
			//intialise a dummy request object
			publicJourneyRequest = new PublicJourneyRequest();

			publicJourneyRequest.OutwardDateTime = outwardTime;		
			publicJourneyRequest.ReturnDateTime = returnTime;						
					
			publicJourneyRequest.OriginLocation = new RequestLocation();
			publicJourneyRequest.DestinationLocation = new RequestLocation();
			publicJourneyRequest.OriginLocation.NaPTANs = new Naptan[1];
			publicJourneyRequest.DestinationLocation.NaPTANs = new Naptan[1];

			publicJourneyRequest.OriginLocation.NaPTANs[0] = naptan("naptan1", 0,0);
			publicJourneyRequest.DestinationLocation.NaPTANs[0] = naptan("naptan2", 0,0);

			publicJourneyRequest.OriginLocation.Postcode = "W3 7JW";
			publicJourneyRequest.DestinationLocation.Postcode = "EC1R 3HN";	
	
			publicJourneyRequest.IsReturnRequired = true;
			
			//initialise a dummy context object
			context = new ExposedServiceContext("0", "n-unittest", "en-GB", "TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1/PlanPublicJourney");
			
		}
		
		[TearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}		
		#endregion

		#region TEST ResolveLocation method
		[Test]
		public void ResolveLocationsValidPostcodes()
		{			
			//add two valid postcodes
			publicJourneyRequest.OriginLocation.Postcode = "W3 7JW";
            publicJourneyRequest.OriginLocation.Type = LocationType.Postcode;
			publicJourneyRequest.DestinationLocation.Postcode = "EC1R 3HN";
            publicJourneyRequest.DestinationLocation.Type = LocationType.Postcode;

			helper = SetupHelper(ref TDJourneyRequest);
		
			helper.ResolveLocations();
			
			string message = "test failed because location was not resolved";

			//check if all the locations have been resolved 
			Assert.IsNotNull(TDJourneyRequest.OriginLocation, message);
			Assert.IsNotNull(TDJourneyRequest.DestinationLocation, message);					
			Assert.IsNotNull(TDJourneyRequest.ReturnOriginLocation, message);
			Assert.IsNotNull(TDJourneyRequest.ReturnDestinationLocation, message);		
			
		}

		[Test]
		public void ResolveLocationsInvalidOriginPostcode()
		{			
			publicJourneyRequest.OriginLocation.Postcode = "W3D 7JW";
            publicJourneyRequest.OriginLocation.Type = LocationType.Postcode;

			helper = SetupHelper(ref TDJourneyRequest);

			try
			{
				helper.ResolveLocations();

				Assert.Fail("The postcode should have been unresolvable");
			}
			catch(TDException ex)
			{
				Assert.AreEqual(ex.Identifier, TDExceptionIdentifier.JPResolvePostcodeFailed);
			}
		}

		[Test]
		public void ResolveLocationsInvalidDestinationPostcode()
		{
            publicJourneyRequest.OriginLocation.Postcode = "W3 7JW";
            publicJourneyRequest.OriginLocation.Type = LocationType.Postcode;
            publicJourneyRequest.DestinationLocation.Postcode = "euston,";
            publicJourneyRequest.DestinationLocation.Type = LocationType.Postcode;

			helper = SetupHelper(ref TDJourneyRequest);

			try
			{
				helper.ResolveLocations();

				Assert.Fail("The postcode should have been unresolvable");
			}
			catch(TDException ex)
			{
				Assert.AreEqual(ex.Identifier, TDExceptionIdentifier.JPResolvePostcodeFailed);
			}
		}

        [Test]
        public void ResolveLocationsInvalidCoordinates()
        {
            // origin easting / northing not set
            publicJourneyRequest.OriginLocation.GridReference = new EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference();
            publicJourneyRequest.OriginLocation.Type = LocationType.Coordinate;
            publicJourneyRequest.DestinationLocation.GridReference = new EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference();
            publicJourneyRequest.DestinationLocation.Type = LocationType.Coordinate;
            publicJourneyRequest.DestinationLocation.GridReference.Easting = 599100;
            publicJourneyRequest.DestinationLocation.GridReference.Northing = 226417;

            helper = SetupHelper(ref TDJourneyRequest);

            try
            {
                helper.ResolveLocations();

                Assert.Fail("The coordinate should not have been resolvable 1");
            }
            catch (TDException ex)
            {
                Assert.AreEqual(ex.Identifier, TDExceptionIdentifier.JPResolveCoordinateFailed);
            }

            // origin easting outside boundaries
            publicJourneyRequest.OriginLocation.GridReference.Easting = 900000;
            publicJourneyRequest.OriginLocation.GridReference.Northing = 366985;

            helper = SetupHelper(ref TDJourneyRequest);

            try
            {
                helper.ResolveLocations();

                Assert.Fail("The coordinate should not have been resolvable 2");
            }
            catch (TDException ex)
            {
                Assert.AreEqual(ex.Identifier, TDExceptionIdentifier.JPResolveCoordinateFailed);
            }

            // origin northing outside boundaries
            publicJourneyRequest.OriginLocation.GridReference.Easting = 341335;
            publicJourneyRequest.OriginLocation.GridReference.Northing = 1450000;

            helper = SetupHelper(ref TDJourneyRequest);

            try
            {
                helper.ResolveLocations();

                Assert.Fail("The coordinate should not have been resolvable 3");
            }
            catch (TDException ex)
            {
                Assert.AreEqual(ex.Identifier, TDExceptionIdentifier.JPResolveCoordinateFailed);
            }

            // destination easting / northing not set
            publicJourneyRequest.OriginLocation.GridReference.Northing = 366985;
            publicJourneyRequest.DestinationLocation.GridReference = new EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference();

            helper = SetupHelper(ref TDJourneyRequest);

            try
            {
                helper.ResolveLocations();

                Assert.Fail("The coordinate should not have been resolvable 4");
            }
            catch (TDException ex)
            {
                Assert.AreEqual(ex.Identifier, TDExceptionIdentifier.JPResolveCoordinateFailed);
            }
        }

        [Test]
        public void ResolveLocationsValidCoordinates()
        {
            // origin easting / northing not set
            publicJourneyRequest.OriginLocation.GridReference = new EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference();
            publicJourneyRequest.OriginLocation.Type = LocationType.Coordinate;
            publicJourneyRequest.OriginLocation.GridReference.Easting = 341335;
            publicJourneyRequest.OriginLocation.GridReference.Northing = 366985;
            publicJourneyRequest.DestinationLocation.GridReference = new EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference();
            publicJourneyRequest.DestinationLocation.Type = LocationType.Coordinate;
            publicJourneyRequest.DestinationLocation.GridReference.Easting = 599100;
            publicJourneyRequest.DestinationLocation.GridReference.Northing = 226417;

            helper = SetupHelper(ref TDJourneyRequest);

            helper.ResolveLocations();

            string message = "test failed because coordinate location was not resolved";

            //check if all the locations have been resolved 
            Assert.IsNotNull(TDJourneyRequest.OriginLocation, message);
            Assert.IsNotNull(TDJourneyRequest.DestinationLocation, message);
        }

		#endregion

		#region TEST Validate method
		[Test]
		public void ValidateOutwardDateInPast()
		{			
			publicJourneyRequest.OutwardDateTime = DateTime.Now.AddMinutes(-5);

			helper = SetupHelper(ref TDJourneyRequest);
								
			try
			{
				helper.Validate();

				Assert.Fail("An error should have been thrown");
			}
			catch(TDException ex)
			{
				Assert.AreEqual(ex.Identifier, TDExceptionIdentifier.JPOutwardDateTimeInPast);
			}						
			
		}
		[Test]
		public void ValidateReturnDateNull()
		{			
			publicJourneyRequest.ReturnDateTime = DateTime.MinValue;

			helper = SetupHelper(ref TDJourneyRequest);
								
			try
			{
				helper.Validate();

				Assert.Fail("An error should have been thrown");
			}
			catch(TDException ex)
			{
				Assert.AreEqual(ex.Identifier, TDExceptionIdentifier.JPReturnDateTimeNotSupplied);
			}						
			
		}

		[Test]
		public void ValidateReturnDateBeforeOutwardDate()
		{			
			publicJourneyRequest.ReturnDateTime = DateTime.Now.AddMinutes(10);
			publicJourneyRequest.OutwardDateTime = DateTime.Now.AddMinutes(15);

			helper = SetupHelper(ref TDJourneyRequest);
								
			try
			{
				helper.Validate();

				Assert.Fail("An error should have been thrown");
			}
			catch(TDException ex)
			{
				Assert.AreEqual(ex.Identifier, TDExceptionIdentifier.JPReturnDateTimeEarlierThanOutwardDateTime);
			}						
			
		}		

		[Test]
		public void UpdateSequenceNumber()
		{			
			publicJourneyRequest.ReturnDateTime  = DateTime.Now.AddMinutes(10);
			publicJourneyRequest.OutwardDateTime = DateTime.Now.AddMinutes(15);

			publicJourneyRequest.Sequence = 1;

			helper = SetupHelper(ref TDJourneyRequest);
								
			int newSequence = helper.UpdateRequestSequence();

			//  requires file property provider
			//	Assert.AreEqual(3, newSequence);
			Assert.AreEqual(1, helper.OriginalSequence);

			publicJourneyRequest.Sequence = 4;

			helper = SetupHelper(ref TDJourneyRequest);
								
			newSequence = helper.UpdateRequestSequence();

			//  requires file property provider
			//  Assert.AreEqual(4, newSequence);
			Assert.AreEqual(4, helper.OriginalSequence);
		
		}		

		#endregion

		#region TEST CallCJP method
		[Test]
		public void CallCJPJourneyResultStartInPast()
		{				
			publicJourneyRequest.IsReturnRequired = false;

			TestMockCJPFromFile cjpManager = (TestMockCJPFromFile) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
			
			cjpManager.FilenameOutward =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "JourneyResultStartInPast.xml";			

			helper = SetupHelper(ref TDJourneyRequest);
										
			ITDJourneyResult result = helper.CallCJP();

			Assert.IsNotNull((CJPMessage)result.CJPMessages[0], "there should have been at least one CJP message");

			Assert.IsTrue(((CJPMessage)result.CJPMessages[0]).MessageResourceId == "JourneyPlannerOutput.JourneyTimeInPast", 
							"The correct CJP message could not be found" );
				
						
			
		}
		
		[Test]
		public void CallCJPJourneyCheckForReturnOverlap()
		{				
			publicJourneyRequest.IsReturnRequired = true;

			TestMockCJPFromFile cjpManager = (TestMockCJPFromFile) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
			
			cjpManager.FilenameOutward =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "OverlappingJourneyResult.xml";
			cjpManager.FilenameReturn =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "OverlappingJourneyResultReturn.xml";

			helper = SetupHelper(ref TDJourneyRequest);
										
			ITDJourneyResult result = helper.CallCJP();

			Assert.IsNotNull((CJPMessage)result.CJPMessages[0], "there should have been at least one CJP message");

			Assert.IsTrue(((CJPMessage)result.CJPMessages[0]).MessageResourceId == "JourneyPlannerOutput.JourneyOverlap", 
							"The correct CJP message could not be found" );
				
						
			
		}	
		#endregion

		#region TEST SendResult method
		/// <summary>
		/// NOTE This test methods work togheter with the testtool that is located in this directory:
		/// \TDPortal\CodeBase\TransportDirect\Stubs\ExposedServicesTestTool\ExposedServicesTestToolClient
		/// This needs to be setup on this machine for below tests to run! also the following properties need to 
		/// be in the database for this partnerID:
		/// JourneyPlannerService.JourneyResultConsumer.Password	EnhancedExposedWebServiceTest
		/// JourneyPlannerService.JourneyResultConsumer.Uri			http://localhost/ExposedServicesTestToolClient/Templates/Reveiver.aspx
		/// JourneyPlannerService.JourneyResultConsumer.Username	EnhancedExposedWebServiceTest
		/// </summary>
		[Test]
		public void SendResultValidJourney()
		{				
			helper = SetupHelper(ref TDJourneyRequest);					

			//obtain a status object that can be passed to SendResult
			CompletionStatus status = CommonAssembler.CreateCompletionStatusDT(true, 
																			   null, 
																			   helper.ResourceManager);

			//build up some kind of result
			TestMockCJPFromFile cjpManager = (TestMockCJPFromFile) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];			

			cjpManager.FilenameOutward =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "ValidJourneyResult.xml";		
			cjpManager.FilenameReturn  =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "ValidJourneyResultReturn.xml";		

			TDJourneyResult journeyResult = (TDJourneyResult) cjpManager.CallCJP(TDJourneyRequest);			

			//translation from a domain object to a dto object
			PublicJourneyResult dtoJourneyResult =  JourneyPlannerAssembler.CreatePublicJourneyResultDT(journeyResult,
																										helper.ResourceManager,
																										TDJourneyRequest.OutwardArriveBefore,
																										TDJourneyRequest.ReturnArriveBefore,	
																										TDJourneyRequest.Sequence);


			//setup a link to the testtools webservice for checking if the transaction has arrived
			TestJourneyResultConsumer TestService = new TestJourneyResultConsumer();

			//change the url to the location of the testtool if the testtool is not located
			//on this machine
            TestService.Url = ConfigurationManager.AppSettings["TestTool.NUnitTestService.Uri"];

			//delete the transaction if it is existing in the testtool
			TestService.DeleteTransaction(context.ExternalTransactionId);

			//now call the actual method that needs testing			
			helper.SendResult(status, dtoJourneyResult);								

			//check if a transaction has been logged on the testtool			
			bool isValid = TestService.IsValidResponse(context.ExternalTransactionId);			

			Assert.IsTrue(isValid, "The wrong response or no response has been sent, look in the test tool for the response");

		}

		/// <summary>
		/// NOTE This test methods work togheter with the testtool that is located in this directory:
		/// \TDPortal\CodeBase\TransportDirect\Stubs\ExposedServicesTestTool\ExposedServicesTestToolClient
		/// This needs to be setup on this machine for below tests to run! also the following properties need to 
		/// be in the database for this partnerID:
		/// JourneyPlannerService.JourneyResultConsumer.Password	EnhancedExposedWebServiceTest
		/// JourneyPlannerService.JourneyResultConsumer.Uri			http://localhost/ExposedServicesTestToolClient/Templates/Reveiver.aspx
		/// JourneyPlannerService.JourneyResultConsumer.Username	EnhancedExposedWebServiceTest
		/// </summary>
		[Test]
		public void SendResultFailedJourney()
		{							
			helper = SetupHelper(ref TDJourneyRequest);			

			CJPMessage[] messages = new CJPMessage[1];
			messages[0] = new CJPMessage("test", "test", 2121, 1121);

			//obtain a status object that can be passed to SendResult
			CompletionStatus status = CommonAssembler.CreateCompletionStatusDT(false, 
																			   messages, 
																		       helper.ResourceManager);		

			//setup a link to the testtools webservice for checking if the transaction has arrived			
			TestJourneyResultConsumer TestService = new TestJourneyResultConsumer();

			//change the url to the location of the testtool if the testtool is not located
			//on this machine
            TestService.Url = ConfigurationManager.AppSettings["TestTool.NUnitTestService.Uri"];

			//delete the transaction if it is existing in the testtool
			TestService.DeleteTransaction(context.ExternalTransactionId);

			//now call the actual method that needs testing			
			helper.SendResult(status, null);								

			//check if a transaction has been logged on the testtool			
			bool isValid = TestService.IsValidResponse(context.ExternalTransactionId);			

			Assert.IsTrue(!isValid, "The wrong response or no response has been sent, look in the test tool for the response");

		}

		#endregion

		#region support methods
		public Naptan naptan(string naptanName, int easting, int northing)
		{
			Naptan naptan = new Naptan();

			naptan.Name = naptanName;
			naptan.NaptanId = naptanName;
			naptan.GridReference = new TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference();
			naptan.GridReference.Easting = easting;
			naptan.GridReference.Northing = northing;

			return naptan;
		}

		public JourneyRequestHelper SetupHelper(ref ITDJourneyRequest TDJourneyRequest)
		{
			//Translating the request from data transfer objects passed in by the External System to domain objects
			TDJourneyRequest = JourneyPlannerAssembler.CreateTDJourneyRequest(publicJourneyRequest);
			
			JourneyRequestHelper helper = new JourneyRequestHelper(context, TDJourneyRequest, publicJourneyRequest);

			return helper;
		}
		#endregion

		
	}
}
