// ***********************************************
// NAME 		: TestCommonAssembler.cs
// AUTHOR 		: Hassan Al-Katib
// DATE CREATED : 26/01/2006
// DESCRIPTION 	: testclass for ther CommonAssembler class
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/Common/V1/Test/TestCommonAssembler.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:18   mturner
//Initial revision.
//
//   Rev 1.4   Feb 24 2006 15:54:54   AViitanen
//Updated following tdexceptionidentifier renumbering.
//
//   Rev 1.3   Feb 02 2006 16:00:44   RWilby
//Added TestCreateTDLocation
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.2   Feb 01 2006 16:25:48   halkatib
//Changes made as part of code review.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//

using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;

using NUnit.Framework;
using journeyPlannerDTO = TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using journeyControl = TransportDirect.UserPortal.JourneyControl;
using cjpInterface = TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1.Test
{
	public class TestCommonAssemblerInitialisation : IServiceInitialisation
	{		

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
			serviceCache.Add( ServiceDiscoveryKey.Cache, new TDCache() );					
			serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new DummyExternalLinkService());		
			serviceCache.Add(ServiceDiscoveryKey.TDMapHandoff, new TestStubTDMapHandoff());			
		}	
	}


	/// <summary>
	/// Summary description for TestCommonAssembler.
	/// </summary>
	[TestFixture]
	public class TestCommonAssembler
	{
		private PublicJourneyRequest publicJourneyRequest;
		//private JourneyRequestHelper helper;
		private TransportDirect.Common.ResourceManager.TDResourceManager rm;
		ITDJourneyResult result;	
		private CompletionStatus completionStatus;
		
		public TestCommonAssembler()
		{
		}

		#region setup
		/// <summary>
		/// Method to initalise the simulated result
		/// </summary>
		[SetUp]		
		public void SetUp()
		{
			result = null;

			// Initialise the service discovery
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestCommonAssemblerInitialisation( ));
			IPropertyProvider property = (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];

			Trace.Listeners.Remove("TDTraceListener");
		
			IEventPublisher[] customPublishers = new IEventPublisher[0];																								
			ArrayList errors = new ArrayList();
				
			try
			{
				Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException e)
			{
				Assert.Fail(e.Message);
			}		
			
			//intialise a dummy request object
			publicJourneyRequest = new PublicJourneyRequest();		
			
		}
		
		
		/// <summary>
		/// Mehtod to initialise the journey result using the specified filename
		/// </summary>
		/// <param name="fileName"></param>
		/// <returns></returns>
		private ITDJourneyResult InitialiseJourneyResult(string fileName)
		{
			TestMockCJPFromFile cjpManager = new TestMockCJPFromFile();

			cjpManager.FilenameOutward = AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName;	

			
			ITDJourneyRequest request =  new TDJourneyRequest();
			request.IsReturnRequired = false;
			ITDJourneyResult initialResult = null;
			initialResult = cjpManager.CallCJP(request);
			
			rm = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.JOURNEY_PLANNER_SERVICE_RM);

			return initialResult;
		}		
		#endregion 

		#region Nunit Tests

		/// <summary>
		/// This tests if the Method uses an array of cjpmessage domain objects to determine the completion status of a single journey result.
		/// </summary>
		[Test]
		public void CallCreateCompletionStatusDT_Inverness_Folkestone()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");

			completionStatus = CommonAssembler.CreateCompletionStatusDT(true, result.CJPMessages, rm);
			
			Console.WriteLine(completionStatus.Status.ToString());
			Assert.AreEqual(completionStatus.Status, StatusType.Success);			
		}

		
		/// <summary>
		/// This tests if the Method uses an array of cjpmessage domain objects to determine the completion status of a single journey result. In this case the test determines whether the code correctly distinguishes user warnings from errors by adding warnings to the result.
		/// </summary>
		[Test]
		public void CallCreateCompletionStatusDT_Inverness_Folkestone_UserWarnings()
		{
			result = InitialiseJourneyResult("Inverness_Folkstone_Xml_Journey_Result.xml");

			result.AddMessageToArray(rm.GetString("JourneyPlannerOutput.JourneyOverlap"),
				"JourneyPlannerOutput.JourneyOverlap",
				0,
				0, 
				ErrorsType.Warning);

			completionStatus = CommonAssembler.CreateCompletionStatusDT(false, result.CJPMessages, rm);
			
			Console.WriteLine(completionStatus.Status.ToString());			
			Console.WriteLine(completionStatus.Messages.Length.ToString());

			Assert.AreEqual(completionStatus.Status, StatusType.Failed);
			Assert.AreEqual(completionStatus.Messages.Length,0);
		}

	
		/// <summary>
		/// This tests if the Method uses an array of cjpmessage domain objects to determine the completion status of a single journey result. In this case the test determines whether the code correctly distinguishes user warnings from errors by supplying a result with errors.
		/// </summary>
		[Test]
		public void CallCreateCompletionStatusDT_Inverness_Folkestone_InvalidJourneyResult()
		{
			result = InitialiseJourneyResult("InvalidJourneyResult.xml");

			completionStatus = CommonAssembler.CreateCompletionStatusDT(false, result.CJPMessages, rm);
			
			Console.WriteLine(completionStatus.Status.ToString());			
			Console.WriteLine(completionStatus.Messages.Length.ToString());
			Console.WriteLine(completionStatus.Messages[0].Text);
			Assert.AreEqual(completionStatus.Messages.Length,1);

		}


		/// <summary>
		/// This tests if the Method returns a null domain object when provided with a null dto object.
		/// </summary>
		[Test]
		public void CallCreateCompletionStatusDT_null()
		{
			completionStatus = CommonAssembler.CreateCompletionStatusDT(true, null, rm);			
			Assert.AreEqual(completionStatus.Messages, null);			
		}


		/// <summary>
		/// This tests if the Method Distinguishes between a Tdexception and any other exception.
		/// </summary>
		[Test]
		public void CallCreateCompletionStatus_Exception()
		{
			NullReferenceException nre = new NullReferenceException("Null reference exception recieved");
			TDException tdex = new TDException("Test TD Exception inside range", true, TDExceptionIdentifier.JPOutwardDateTimeInPast);
			//tdex.Message = "TD Exception recieved";

			CompletionStatus csDTO = new CompletionStatus();
			csDTO = CommonAssembler.CreateCompletionStatusDT(nre);

			Assert.AreEqual(csDTO.Messages[0].Code,0);
			Assert.AreEqual(csDTO.Messages[0].Text,"Null reference exception recieved"); 
			Assert.AreEqual(csDTO.Status,StatusType.Failed);			

			csDTO = new CompletionStatus();
			csDTO = CommonAssembler.CreateCompletionStatusDT(tdex);	
		
			Assert.AreEqual(csDTO.Messages[0].Code,Convert.ToInt32( TDExceptionIdentifier.JPOutwardDateTimeInPast));
			Assert.AreEqual(csDTO.Messages[0].Text,"Test TD Exception inside range"); 
			Assert.AreEqual(csDTO.Status,StatusType.ValidationError);
		}

	
		/// <summary>
		/// This tests if the Method returns a null domain object when provided with a null dto object.
		/// </summary>
		[Test]
		public void CallCreateCompletionStatus_Exception_null()
		{
			CompletionStatus csDTO = new CompletionStatus();
			csDTO = CommonAssembler.CreateCompletionStatusDT(null);
			Assert.AreEqual(csDTO,null);			
		}

	
		/// <summary>
		/// This tests if the method can distinguish between an exposed services exception identifier and a non-exposed services identifier. 
		/// </summary>
		[Test]
		public void CallMapExceptionIdenifierToStatus()
		{
			
			StatusType st = new StatusType();
			st = CommonAssembler.MapExceptionIdentifierToStatus(TDExceptionIdentifier.JPOutwardDateTimeInPast);
			Assert.AreEqual(st,StatusType.ValidationError);

			st = new StatusType();
			st = CommonAssembler.MapExceptionIdentifierToStatus(TDExceptionIdentifier.EESAddTDExpposedServicesEventFailed);
			Assert.AreEqual(st,StatusType.Failed);
		}
	
	
		/// <summary>
		/// Test method for CreateOSGridReferenceDT
		/// </summary>
		[Test]
		public void TestCreateOSGridReferenceDT()
		{
			int easting =1001;
			int northing = 2001;

			OSGridReference osGridRef = CommonAssembler.CreateOSGridReferenceDT(easting, northing);
			Assert.AreEqual(osGridRef.Easting, easting);
			Assert.AreEqual(osGridRef.Northing, northing); 
		}

		/// <summary>
		/// Test method for CreateTDLocation
		/// </summary>
		[Test]
		public void TestCreateTDLocation()
		{
			int easting =1001;
			int northing = 2001;
			
			OSGridReference osGridReference = new OSGridReference();
			osGridReference.Easting = easting;
			osGridReference.Northing = northing;

			TDLocation tdLocation =  CommonAssembler.CreateTDLocation(osGridReference);

			Assert.AreEqual(easting,tdLocation.GridReference.Easting,"Incorrect Easting");
			Assert.AreEqual(northing,tdLocation.GridReference.Northing,"Incorrect Northing"); 
		}

		#endregion
	}
}
