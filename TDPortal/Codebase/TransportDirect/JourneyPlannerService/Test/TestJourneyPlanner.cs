// ***********************************************
// NAME 		: TestJourneyPlanner.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 17/01/2006
// DESCRIPTION 	: testclass for the journey planner class
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/Test/TestJourneyPlanner.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:40   mturner
//Initial revision.
//
//   Rev 1.7   Feb 27 2006 14:34:04   AViitanen
//Post-merge fixes to PlanPublicJourneyInvalidDateAsync and  PlanPublicJourneyInvalidPostCodesAsync.
//
//   Rev 1.6   Feb 02 2006 14:43:22   mdambrine
//rework on the journey planner enhanced exposed services see CR053_IR_3407 Journey Planner Service Component.doc 
//
//   Rev 1.5   Jan 24 2006 09:50:50   mdambrine
//Changed from a file to a database property provider
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Jan 23 2006 14:06:16   mdambrine
//Ncover changes
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 20 2006 18:42:44   mdambrine
//added returnrequired
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 20 2006 14:42:34   mdambrine
//changed the consumer to seperate project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 19 2006 17:32:58   mdambrine
//Add new test 
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103

//Rev 1.0   Jan 19 2006 08:51:56   mdambrine
//Initial revision.
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
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

using Logger = System.Diagnostics.Trace;

using NUnit.Framework;

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.EnhancedExposedServiceConsumers.JourneyResultConsumer.V1;
using TransportDirect.EnhancedExposedServiceConsumers.JourneyConsumer.V1;


namespace TransportDirect.UserPortal.JourneyPlannerService
{
	public class TestJourneyPlannerInitialisation : IServiceInitialisation
	{
        private bool UseRealCJP = Convert.ToBoolean(ConfigurationManager.AppSettings["Nunit.UseDatabase"]);

		public void Populate(Hashtable serviceCache)
		{

			// nasty bodge to make date validation checks independent of the 
			//  date format of the user/machine the tests are run on ...
			CultureInfo ci = new CultureInfo("en-GB");
			DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
			dtfi.ShortDatePattern = "dd MM yyyy";
			ci.DateTimeFormat = dtfi;
			Thread.CurrentThread.CurrentCulture = ci;

			serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory() );
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			serviceCache.Add(ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory());	
			serviceCache.Add (ServiceDiscoveryKey.GisQuery, new TestMockGisQuery());
			serviceCache.Add (ServiceDiscoveryKey.GazetteerFactory, new TDGazetteerFactory());					
			serviceCache.Add( ServiceDiscoveryKey.Cache, new TDCache() );		
			serviceCache.Add (ServiceDiscoveryKey.CjpManager, new TestMockCJPFromFile());
			serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new DummyExternalLinkService());		
			serviceCache.Add(ServiceDiscoveryKey.TDMapHandoff, new TestStubTDMapHandoff());
			serviceCache.Add(ServiceDiscoveryKey.JourneyPlannerService, new JourneyPlannerFactory());			
		}
	
	}

	/// <summary>
	/// Summary description for TestJourneyPlanner.
	/// </summary>
	[TestFixture]
	public class TestJourneyPlanner
	{		
		private PublicJourneyRequest publicJourneyRequest;
		private ExposedServiceContext context;
        private bool UseRealCJP = Convert.ToBoolean(ConfigurationManager.AppSettings["Nunit.UseDatabase"]);

		public TestJourneyPlanner()
		{
		}
	
		#region setup
		[SetUp]
		public void SetUp()
		{
			// Initialise the service discovery
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestJourneyPlannerInitialisation( ));
			IPropertyProvider property = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

			Trace.Listeners.Remove("TDTraceListener");
		
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
							
			Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			
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
			context = new ExposedServiceContext("0", 
												"n-unittest", 
												"en-GB", 
												"TransportDirect.EnhancedExposedServices.JourneyPlanner.V1/PlanPublicJourney");
			
			
		}
		
		[TearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}		
		#endregion
		
		#region Tests
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
		public void PlanPublicJourneyValidJourneyAsync()
		{			
			//setup a link to the testtools webservice for checking if the transaction has arrived
			TestJourneyResultConsumer TestService = new TestJourneyResultConsumer();

			//change the url to the location of the testtool if the testtool is not located
			//on this machine			
            TestService.Url = ConfigurationManager.AppSettings["TestTool.NUnitTestService.Uri"];

			//delete the transaction if it is existing in the testtool
			TestService.DeleteTransaction(context.ExternalTransactionId);
		

			TestMockCJPFromFile cjpManager = (TestMockCJPFromFile) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
			
			cjpManager.FilenameOutward =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "ValidJourneyResult.xml";
			cjpManager.FilenameReturn =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "ValidJourneyResultReturn.xml";

			//add two valid postcodes
			publicJourneyRequest.OriginLocation.Postcode = "W3 7JW";
			publicJourneyRequest.DestinationLocation.Postcode = "EC1R 3HN";

			publicJourneyRequest.IsReturnRequired = true;

			// A normal, valid journey
			JourneyPlanner planner = (JourneyPlanner) TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerService];
						
			planner.PlanPublicJourney(context, publicJourneyRequest);
			//check if the result has been recieved 													

			Thread.Sleep(10000);

			//check if a transaction has been logged on the testtool			
			//SoapTransaction TestTransaction = TestService.GetTransaction(context.ExternalTransactionId);			
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
		public void PlanPublicJourneyInValidJourneyAsync()
		{			
			//setup a link to the testtools webservice for checking if the transaction has arrived
			TestJourneyResultConsumer TestService = new TestJourneyResultConsumer();

			//change the url to the location of the testtool if the testtool is not located
			//on this machine			
            TestService.Url = ConfigurationManager.AppSettings["TestTool.NUnitTestService.Uri"];

			//delete the transaction if it is existing in the testtool
			TestService.DeleteTransaction(context.ExternalTransactionId);
		

			TestMockCJPFromFile cjpManager = (TestMockCJPFromFile) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
			
			cjpManager.FilenameOutward =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "InvalidJourneyResult.xml";			

			//add two valid postcodes
			publicJourneyRequest.OriginLocation.Postcode = "W3 7JW";
			publicJourneyRequest.DestinationLocation.Postcode = "EC1R 3HN";

			publicJourneyRequest.IsReturnRequired = false;

			// A normal, valid journey
			JourneyPlanner planner = (JourneyPlanner) TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerService];
						
			planner.PlanPublicJourney(context, publicJourneyRequest);
			//check if the result has been recieved 													

			Thread.Sleep(10000);

			//check if a transaction has been logged on the testtool			
			//SoapTransaction TestTransaction = TestService.GetTransaction(context.ExternalTransactionId);			
			bool isValid = TestService.IsValidResponse(context.ExternalTransactionId);			

			Assert.IsTrue(!isValid, "The wrong response or no response has been sent, look in the test tool for the response");						

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
		public void PlanPublicJourneyInvalidPostCodesAsync()
		{			
			//setup a link to the testtools webservice for checking if the transaction has arrived
			TestJourneyResultConsumer TestService = new TestJourneyResultConsumer();

			//change the url to the location of the testtool if the testtool is not located
			//on this machine
            TestService.Url = ConfigurationManager.AppSettings["TestTool.NUnitTestService.Uri"];

			//delete the transaction if it is existing in the testtool
			TestService.DeleteTransaction(context.ExternalTransactionId);
		

			TestMockCJPFromFile cjpManager = (TestMockCJPFromFile) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
			
			cjpManager.FilenameOutward =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "validJourneyResult.xml";			

			//add an invalid postcodes
			publicJourneyRequest.OriginLocation.Postcode = "W3D 7JW";	
			publicJourneyRequest.DestinationLocation.Postcode = "EC1R 3HN";

			publicJourneyRequest.IsReturnRequired = false;

			// A normal, valid journey
			JourneyPlanner planner = (JourneyPlanner) TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerService];
						
			planner.PlanPublicJourney(context, publicJourneyRequest);
			//check if the result has been recieved 													

			Thread.Sleep(10000);

			//check if a transaction has been logged on the testtool			
			//SoapTransaction TestTransaction = TestService.GetTransaction(context.ExternalTransactionId);			
			bool isValid = TestService.IsValidResponse(context.ExternalTransactionId);	
			
			Assert.IsTrue(!isValid, "The wrong response or no response has been sent, look in the test tool for the response");		

			//check if the soapexception is correct
			string resonseText = TestService.GetTransaction(context.ExternalTransactionId).SoapResponse.SoapMessage;
			Assert.IsTrue(GetInnerXml(resonseText, "Text").StartsWith("Postcode not found"), "the text passed with the error was wrong");
			Assert.IsTrue(GetInnerXml(resonseText, "Code").StartsWith("18001"), "the code passed with the error was wrong");						

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
		public void PlanPublicJourneyInvalidDateAsync()
		{			
			//setup a link to the testtools webservice for checking if the transaction has arrived
			TestJourneyResultConsumer TestService = new TestJourneyResultConsumer();

			//change the url to the location of the testtool if the testtool is not located
			//on this machine
            TestService.Url = ConfigurationManager.AppSettings["TestTool.NUnitTestService.Uri"];

			//delete the transaction if it is existing in the testtool
			TestService.DeleteTransaction(context.ExternalTransactionId);
		

			TestMockCJPFromFile cjpManager = (TestMockCJPFromFile) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
			
			cjpManager.FilenameOutward =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "validJourneyResult.xml";			

			//add valid postcodes
			publicJourneyRequest.OriginLocation.Postcode = "W3 7JW";	
			publicJourneyRequest.DestinationLocation.Postcode = "EC1R 3HN";

			//add invalid date
			publicJourneyRequest.OutwardDateTime = DateTime.Now.AddMinutes(-5);

			publicJourneyRequest.IsReturnRequired = false;

			// A normal, valid journey
			JourneyPlanner planner = (JourneyPlanner) TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerService];
						
			planner.PlanPublicJourney(context, publicJourneyRequest);
			//check if the result has been recieved 													

			Thread.Sleep(10000);

			//check if a transaction has been logged on the testtool			
			//SoapTransaction TestTransaction = TestService.GetTransaction(context.ExternalTransactionId);			
			bool isValid = TestService.IsValidResponse(context.ExternalTransactionId);			

			Assert.IsTrue(!isValid, "The wrong response or no response has been sent, look in the test tool for the response");	
			
			//check if the soapexception is correct
			string resonseText = TestService.GetTransaction(context.ExternalTransactionId).SoapResponse.SoapMessage;
			Assert.IsTrue(GetInnerXml(resonseText, "Text").StartsWith("Outward date time in the past"), "the text passed with the error was wrong");
			Assert.IsTrue(GetInnerXml(resonseText, "Code").StartsWith("18002"), "the code passed with the error was wrong");	

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
		public void PlanPublicJourneyInvalidCJP()
		{			
			//setup a link to the testtools webservice for checking if the transaction has arrived
			TestJourneyResultConsumer TestService = new TestJourneyResultConsumer();

			//change the url to the location of the testtool if the testtool is not located
			//on this machine
            TestService.Url = ConfigurationManager.AppSettings["TestTool.NUnitTestService.Uri"];

			//delete the transaction if it is existing in the testtool
			TestService.DeleteTransaction(context.ExternalTransactionId);
		

			TestMockCJPFromFile cjpManager = (TestMockCJPFromFile) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
			
			cjpManager.FilenameOutward =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "InvalidJourneyResult.xml";				

			//add valid postcodes
			publicJourneyRequest.OriginLocation.Postcode = "W3 7JW";	
			publicJourneyRequest.DestinationLocation.Postcode = "EC1R 3HN";			

			publicJourneyRequest.IsReturnRequired = false;

			// A normal, valid journey
			JourneyPlanner planner = (JourneyPlanner) TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerService];
						
			planner.PlanPublicJourney(context, publicJourneyRequest);
			//check if the result has been recieved 													

			Thread.Sleep(10000);

			//check if a transaction has been logged on the testtool			
			//SoapTransaction TestTransaction = TestService.GetTransaction(context.ExternalTransactionId);			
			bool isValid = TestService.IsValidResponse(context.ExternalTransactionId);			

			Assert.IsTrue(!isValid, "The wrong response or no response has been sent, look in the test tool for the response");	
			
			//check if the soapexception is correct
			string resonseText = TestService.GetTransaction(context.ExternalTransactionId).SoapResponse.SoapMessage;
			Assert.IsTrue(GetInnerXml(resonseText, "Text").StartsWith("Currently unable to obtain journey options using the request parameters supplied"), "the text passed with the error was wrong");
			Assert.IsTrue(GetInnerXml(resonseText, "Code").StartsWith("158"), "the code passed with the error was wrong");	

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

		/// <summary>
		/// Gets the innerxml of an element, use this function if the namespace in the
		/// xml document is unknown. this function does not use the dom document.
		/// </summary>
		/// <param name="soapMessage">Message</param>
		/// <param name="elementName">element to return innerXml</param>
		/// <returns>innerXml of element</returns>
		private string GetInnerXml(string soapMessage,
								   string elementName)
		{
			string startTag = "<" + elementName + ">";
			string endTag = "</" + elementName + ">";

			int startIndex = soapMessage.IndexOf(startTag) + startTag.Length;
			int endIndex = soapMessage.IndexOf(endTag);			
			
			return soapMessage.Substring(startIndex, endIndex - startIndex);
		}
		#endregion
		
	}
}
