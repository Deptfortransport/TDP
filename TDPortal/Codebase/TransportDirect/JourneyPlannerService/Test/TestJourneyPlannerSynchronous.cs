// ***********************************************
// NAME 		: TestJourneyPlannerSynchronous.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 12/01/2006
// DESCRIPTION 	: Test class for the synchronous journey planner service
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/Test/TestJourneyPlannerSynchronous.cs-arc  $
//
//   Rev 1.3   Sep 29 2010 11:27:48   apatel
//EES Web Services for Cycle code changes
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
//
//   Rev 1.2   Sep 08 2009 13:29:18   mmodi
//Updated for a max number of journeys in car request
//Resolution for 5318: Car exposed service - Multiple journey limit property
//
//   Rev 1.1   Aug 04 2009 14:09:14   mmodi
//Updated to test Car journey planning
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 12:24:40   mturner
//Initial revision.
//
//   Rev 1.8   Feb 20 2006 17:02:08   mdambrine
//added soapactions because the access restriction changes made these tests fail.
//Resolution for 19: DEL 8.1 Workstream - Access Restrictions
//
//   Rev 1.7   Jan 23 2006 14:06:16   mdambrine
//Ncover changes
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Jan 20 2006 18:42:54   mdambrine
//added return
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
//   Rev 1.3   Jan 16 2006 17:10:52   mdambrine
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
//   Rev 1.0   Jan 13 2006 18:25:54   mdambrine
//Initial revision.
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
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.JourneyEmissions;

using Logger = System.Diagnostics.Trace;

using NUnit.Framework;

using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.Common;

using commonDT = TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using ptJP = TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using carJP = TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1;
using cycleJP = TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1;
using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.CoordinateConvertorProvider;
using TransportDirect.UserPortal.CyclePlannerControl;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.JourneyPlannerService
{

	#region Initialisation
	public class TestJourneyPlannerSynchronousInitialisation : IServiceInitialisation
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

            Trace.Listeners.Remove("TDTraceListener");

            IEventPublisher[] customPublishers = new IEventPublisher[0];
            ArrayList errors = new ArrayList();

            Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));			

			serviceCache.Add(ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory());	
			serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TestMockGisQuery());
			serviceCache.Add(ServiceDiscoveryKey.GazetteerFactory, new TDGazetteerFactory());			
			serviceCache.Add(ServiceDiscoveryKey.CjpManager, new TestMockCJPFromFile());			
			serviceCache.Add(ServiceDiscoveryKey.Cache, new TDCache() );					
			serviceCache.Add(ServiceDiscoveryKey.ExternalLinkService, new DummyExternalLinkService());		
			serviceCache.Add(ServiceDiscoveryKey.TDMapHandoff, new TestStubTDMapHandoff());
			serviceCache.Add(ServiceDiscoveryKey.JourneyPlannerSynchronousService, new JourneyPlannerSynchronousFactory());
            serviceCache.Add(ServiceDiscoveryKey.CarCostCalculator, new CarCostCalculatorFactory());
            serviceCache.Add(ServiceDiscoveryKey.JourneyEmissionsFactor, new JourneyEmissionsFactorFactory());
            serviceCache.Add(ServiceDiscoveryKey.CyclePlannerManager, new CyclePlannerManagerFactory());
            serviceCache.Add(ServiceDiscoveryKey.CyclePlannerFactory, new CyclePlannerFactory());
            serviceCache.Add(ServiceDiscoveryKey.CoordinateConvertorFactory, new CoordinateConvertorFactory());
            serviceCache.Add(ServiceDiscoveryKey.CycleAttributes, new CycleAttributesFactory());
            serviceCache.Add(ServiceDiscoveryKey.GradientProfilerManager, new GradientProfilerManagerFactory());

		}
	
	}
	#endregion

	/// <summary>
	/// Summary description for TestJourneyPlannerSynchronous.
	/// </summary>
	[TestFixture]
	public class TestJourneyPlannerSynchronous
	{		
		private PublicJourneyRequest publicJourneyRequest;
        private CarJourneyRequest carJourneyRequest;
		private ExposedServiceContext context;
        private cycleJP.CycleJourneyRequest cycleJourneyRequest;
        private GradientProfileRequest gradientProfileRequest;


		public TestJourneyPlannerSynchronous()
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
            
			// Setup Journey Parameters
			// Outward time is one month from now
			DateTime outwardTime = DateTime.Now.AddMonths(1);

			// return time is 5.5 hours after the outward time
			DateTime returnTime = outwardTime.AddSeconds(5.5 * 3600);  
			
			//intialise a dummy request object
			publicJourneyRequest = new PublicJourneyRequest();

			publicJourneyRequest.OutwardDateTime = outwardTime;		
			publicJourneyRequest.ReturnDateTime = returnTime;						
					
			publicJourneyRequest.OriginLocation = new ptJP.RequestLocation();
			publicJourneyRequest.DestinationLocation = new ptJP.RequestLocation();
			publicJourneyRequest.OriginLocation.NaPTANs = new Naptan[1];
			publicJourneyRequest.DestinationLocation.NaPTANs = new Naptan[1];

			publicJourneyRequest.OriginLocation.NaPTANs[0] = naptan("naptan1", 0,0);
			publicJourneyRequest.DestinationLocation.NaPTANs[0] = naptan("naptan2", 0,0);
			
			//initialise a dummy context object
			context = new ExposedServiceContext("0", "n-unittest", "en-GB", "TransportDirect.EnhancedExposedServices.JourneyPlannerSynchronous.V1/PlanPublicJourney");

            // initialise a Car journey request object
            carJourneyRequest = new CarJourneyRequest();

            carJourneyRequest.JourneyRequests = new JourneyRequest[1];
            carJourneyRequest.JourneyRequests[0] = new JourneyRequest();
            carJourneyRequest.JourneyRequests[0].OutwardDateTime = outwardTime;
            carJourneyRequest.JourneyRequests[0].ReturnDateTime = returnTime;

            carJourneyRequest.JourneyRequests[0].OriginLocation = new carJP.RequestLocation();
            carJourneyRequest.JourneyRequests[0].DestinationLocation = new carJP.RequestLocation();

            carJourneyRequest.JourneyRequests[0].OriginLocation.Type = carJP.LocationType.Coordinate;
            carJourneyRequest.JourneyRequests[0].DestinationLocation.Type = carJP.LocationType.Coordinate;
            carJourneyRequest.JourneyRequests[0].OriginLocation.GridReference = new commonDT.OSGridReference();
            carJourneyRequest.JourneyRequests[0].DestinationLocation.GridReference = new commonDT.OSGridReference();

            carJourneyRequest.CarParameters = new CarParameters();
            carJourneyRequest.ResultSettings = new TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1.ResultSettings();

            // initialise a Cycle journey request object
            cycleJourneyRequest = new cycleJP.CycleJourneyRequest();
            cycleJourneyRequest.JourneyRequest = new cycleJP.JourneyRequest();

            cycleJourneyRequest.JourneyRequest.OutwardDateTime = outwardTime;
            cycleJourneyRequest.JourneyRequest.OriginLocation = new cycleJP.RequestLocation();
            cycleJourneyRequest.JourneyRequest.DestinationLocation = new cycleJP.RequestLocation();

            cycleJourneyRequest.JourneyRequest.OriginLocation.Type = cycleJP.LocationType.Coordinate;
            cycleJourneyRequest.JourneyRequest.DestinationLocation.Type = cycleJP.LocationType.Coordinate;
            cycleJourneyRequest.JourneyRequest.OriginLocation.GridReference = new commonDT.OSGridReference();
            cycleJourneyRequest.JourneyRequest.DestinationLocation.GridReference = new commonDT.OSGridReference();

            cycleJourneyRequest.CycleParameters = new cycleJP.CycleParameters();
            cycleJourneyRequest.ResultSettings = new cycleJP.ResultSettings();

            // initialise a Gradient Profile request object
            gradientProfileRequest = new GradientProfileRequest();

            gradientProfileRequest.Settings = new TransportDirect.EnhancedExposedServices.DataTransfer.GradientProfile.V1.Settings();

		}		

		[TearDown]
		public void Cleanup()
		{
			Trace.Listeners.Remove("TDTraceListener");
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}
		#endregion

		#region tests
		[Test]
		public void PlanPublicJourneyValidJourney()
		{
		
			TestMockCJPFromFile cjpManager = (TestMockCJPFromFile) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
			
			cjpManager.FilenameOutward =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "ValidJourneyResult.xml";
			cjpManager.FilenameReturn =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "ValidJourneyResultReturn.xml";

			//add two valid postcodes
			publicJourneyRequest.OriginLocation.Postcode = "W3 7JW";
			publicJourneyRequest.DestinationLocation.Postcode = "EC1R 3HN";

			// A normal, valid journey
			JourneyPlannerSynchronous planner = (JourneyPlannerSynchronous) TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];

			publicJourneyRequest.IsReturnRequired = true;
			
			try
			{
				PublicJourneyResult result = planner.PlanPublicJourney(context, publicJourneyRequest);

				Assert.IsNotNull(result, "The result was null, this test Failed");
			}
			catch(Exception ex)
			{
				Assert.Fail("the test failed, with exception: " + ex.Message);
			}

		} 

		[Test]
		public void PlanPublicJourneyInvalidJourney()
		{
					
			TestMockCJPFromFile cjpManager = (TestMockCJPFromFile) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];
						
			cjpManager.FilenameOutward =  AppDomain.CurrentDomain.BaseDirectory + "\\" + "InvalidJourneyResult.xml";			

			//add two valid postcodes
			publicJourneyRequest.IsReturnRequired = false;
			publicJourneyRequest.OriginLocation.Postcode = "W3 7JW";
			publicJourneyRequest.DestinationLocation.Postcode = "EC1R 3HN";

			// A normal, valid journey
			JourneyPlannerSynchronous planner = (JourneyPlannerSynchronous) TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];
			
			try
			{
				PublicJourneyResult result = planner.PlanPublicJourney(context, publicJourneyRequest);
			
			}
			catch(TDException ex)
			{				
				Assert.AreEqual(ex.Identifier, TDExceptionIdentifier.JPCJPErrorsOccured);	
				Assert.IsNotNull(ex.AdditionalInformation, "the cjp messages have not been stored with the error");
			}	

		}

        [Test]
        public void PlanCarJourneyValidJourney()
        {
            // The MockCJP is used to provide the cjp Journey response from a file (instead of actually planning the journey)
            TestMockCJPFromFile cjpManager = (TestMockCJPFromFile)TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];

            cjpManager.FilenameOutward = AppDomain.CurrentDomain.BaseDirectory + "\\" + "ValidCarJourneyResult.xml";
            cjpManager.FilenameReturn = AppDomain.CurrentDomain.BaseDirectory + "\\" + "ValidCarJourneyResultReturn.xml";


            // A normal, valid journey
            JourneyPlannerSynchronous planner = (JourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];

            // Update request parameters
            carJourneyRequest.JourneyRequests[0].IsReturnRequired = false;
            carJourneyRequest.ResultSettings.ResultType = ResultType.Detailed;
            carJourneyRequest.ResultSettings.DistanceUnit = DistanceUnit.Miles;
            carJourneyRequest.ResultSettings.ResultTypeSpecified = true;
            carJourneyRequest.ResultSettings.DistanceUnitSpecified = true;

            // Update location parameters
            carJourneyRequest.JourneyRequests[0].OriginLocation.Description = "Nottingham train station";
            carJourneyRequest.JourneyRequests[0].OriginLocation.GridReference.Easting = 457500;
            carJourneyRequest.JourneyRequests[0].OriginLocation.GridReference.Northing = 339300;

            carJourneyRequest.JourneyRequests[0].DestinationLocation.Description = "Leicester train station";
            carJourneyRequest.JourneyRequests[0].DestinationLocation.GridReference.Easting = 459300;
            carJourneyRequest.JourneyRequests[0].DestinationLocation.GridReference.Northing = 304100;

            try
            {
                // The stub journey should be formatted into response correctly
                CarJourneyResult result = planner.PlanPrivateJourney(context, carJourneyRequest, -1);

                Assert.IsNotNull(result, "The result was null, this test Failed");
                Assert.IsTrue(result.CompletionStatus.Status == StatusType.Success, "The result status was NOT StatusType.Success, journey planning failed");
                Assert.IsNotNull(result.JourneyResults[0].OutwardCarJourney.Details, "The result did not contain a Details object");
                Assert.IsNotNull(result.JourneyResults[0].OutwardCarJourney.Summary, "The result did not contain a Summary object");
            }
            catch (Exception ex)
            {
                Assert.Fail("The test failed, with exception: " + ex.Message);
            }
        }

        [Test]
        public void PlanCarJourneyInvalidJourney()
        {
            // The MockCJP is used to provide the cjp Journey response from a file (instead of actually planning the journey)
            TestMockCJPFromFile cjpManager = (TestMockCJPFromFile)TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];

            cjpManager.FilenameOutward = AppDomain.CurrentDomain.BaseDirectory + "\\" + "InvalidCarJourneyResult.xml";


            // A normal, valid journey
            JourneyPlannerSynchronous planner = (JourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];

            // Update request parameters
            carJourneyRequest.JourneyRequests[0].IsReturnRequired = false;
            carJourneyRequest.ResultSettings.ResultType = ResultType.Detailed;
            carJourneyRequest.ResultSettings.DistanceUnit = DistanceUnit.Miles;
            carJourneyRequest.ResultSettings.ResultTypeSpecified = true;
            carJourneyRequest.ResultSettings.DistanceUnitSpecified = true;

            // Update location parameters
            carJourneyRequest.JourneyRequests[0].OriginLocation.Description = "Nottingham train station";
            carJourneyRequest.JourneyRequests[0].OriginLocation.GridReference.Easting = 457500;
            carJourneyRequest.JourneyRequests[0].OriginLocation.GridReference.Northing = 339300;

            carJourneyRequest.JourneyRequests[0].DestinationLocation.Description = "Leicester train station";
            carJourneyRequest.JourneyRequests[0].DestinationLocation.GridReference.Easting = 459300;
            carJourneyRequest.JourneyRequests[0].DestinationLocation.GridReference.Northing = 304100;

            try
            {
                // The stub journey contains a cjp error, so response should include failure
                CarJourneyResult result = planner.PlanPrivateJourney(context, carJourneyRequest, -1);

                Assert.IsTrue(result.CompletionStatus.Status == StatusType.Failed, "The result status was NOT StatusType.Failed, journey planning should have failed to pass test");
            }
            catch (TDException ex)
            {
                Assert.Fail("The test failed, with exception: " + ex.Message);
            }
        }

        #region Cycle Journey Tests
        [Test]
        public void PlanCycleJourneyValidJourney()
        {
            // A normal, valid journey
            JourneyPlannerSynchronous planner = (JourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];

            // Update request parameters
            cycleJourneyRequest.ResultSettings.IncludeGeometry = true;
            cycleJourneyRequest.ResultSettings.DistanceUnit = TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1.DistanceUnit.Miles;
          

            // Update location parameters
            cycleJourneyRequest.JourneyRequest.OriginLocation.Description = "LE5 0LF";
            cycleJourneyRequest.JourneyRequest.OriginLocation.GridReference.Easting = 461273;
            cycleJourneyRequest.JourneyRequest.OriginLocation.GridReference.Northing = 305895;

            cycleJourneyRequest.JourneyRequest.DestinationLocation.Description = "Leicester train station";
            cycleJourneyRequest.JourneyRequest.DestinationLocation.GridReference.Easting = 459300;
            cycleJourneyRequest.JourneyRequest.DestinationLocation.GridReference.Northing = 304100;

            try
            {
                // The stub journey should be formatted into response correctly
                cycleJP.CycleJourneyResult result = planner.PlanCycleJourney(context, cycleJourneyRequest);

                Assert.IsNotNull(result, "The result was null, this test Failed");
                Assert.IsTrue(result.CompletionStatus.Status == StatusType.Success, "The result status was NOT StatusType.Success, journey planning failed");
                Assert.IsNotNull(result.JourneyResult.OutwardCycleJourney.Details, "The result did not contain a Details object");
                Assert.IsNotNull(result.JourneyResult.OutwardCycleJourney.Summary, "The result did not contain a Summary object");
            }
            catch (Exception ex)
            {
                Assert.Fail("The test failed, with exception: " + ex.Message);
            }
        }

        [Test]
        public void PlanCycleJourneyInvalidJourney()
        {
            
            JourneyPlannerSynchronous planner = (JourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];

            // Update request parameters
            cycleJourneyRequest.ResultSettings.IncludeGeometry = true;
            cycleJourneyRequest.ResultSettings.DistanceUnit = TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1.DistanceUnit.Miles;


            // Update location parameters
            cycleJourneyRequest.JourneyRequest.OriginLocation.Description = "Nottingham train station";
            cycleJourneyRequest.JourneyRequest.OriginLocation.GridReference.Easting = 457500;
            cycleJourneyRequest.JourneyRequest.OriginLocation.GridReference.Northing = 339300;

            // Location coordinates below are invalid so the cycle journey planner returns an error instead
            cycleJourneyRequest.JourneyRequest.DestinationLocation.Description = "Leicester train station";
            cycleJourneyRequest.JourneyRequest.DestinationLocation.GridReference.Easting = 989898;
            cycleJourneyRequest.JourneyRequest.DestinationLocation.GridReference.Northing = 304100;

            try
            {
                cycleJP.CycleJourneyResult result = planner.PlanCycleJourney(context, cycleJourneyRequest);

                Assert.IsTrue(result.CompletionStatus.Status == StatusType.Failed, "The result status was NOT StatusType.Failed, cycle journey planning should have failed to pass test");
            }
            catch (TDException ex)
            {
                Assert.Fail("The test failed, with exception: " + ex.Message);
            }
        }
        #endregion

        #region Gradient Profiler Tests
        [Test]
        public void TestGradientProfiler()
        {
            char pointSeperator = char.Parse(" ");
            char eastingNorthingSeparator = char.Parse(",");

            JourneyPlannerSynchronous planner = (JourneyPlannerSynchronous)TDServiceDiscovery.Current[ServiceDiscoveryKey.JourneyPlannerSynchronousService];

            List<PolylineGroup> polyGroup = new List<PolylineGroup>();

            PolylineGroup polylineGroup;
            List<Polyline> polylineArray;
            Polyline polyline;

            #region Polyline group 1

            polylineGroup = new PolylineGroup();
            polylineGroup.ID = 1;

            // Create a temp array to hold the Polylines
            polylineArray = new List<Polyline>();

            #region Polylines added to group 1

            polyline = new Polyline();
            polyline.ID = 1;
            polyline.InterpolateGradient = false;
            polyline.PolylineGridReferences = "334906,389873 334895,389852";

            // Add the web Polyline to our temp array
            polylineArray.Add(polyline);

            #endregion

            // All polylines have been set for this group, so add to the web PolylineGroup
            polylineGroup.Polylines = polylineArray.ToArray();

            // Add the web PolylineGroup to our temp array
            polyGroup.Add(polylineGroup);

            #endregion

            #region Polyline group 2

            polylineGroup = new PolylineGroup();
            polylineGroup.ID = 2;

            // Create a temp array to hold the Polylines
            polylineArray = new List<Polyline>();

            #region Polylines added to group 2

            polyline = new Polyline();
            polyline.ID= 2;
            polyline.InterpolateGradient = false;
            polyline.PolylineGridReferences = "334895,389852 334779,389900 334745,389914";

            // Add the web Polyline to our temp array
            polylineArray.Add(polyline);

            polyline = new Polyline();
            polyline.ID = 3;
            polyline.InterpolateGradient = false;
            polyline.PolylineGridReferences = "334745,389914 334660,389948";

            // Add the web Polyline to our temp array
            polylineArray.Add(polyline);

            #endregion

            // All polylines have been set for this group, so add to the web PolylineGroup
            polylineGroup.Polylines = polylineArray.ToArray();

            // Add the web PolylineGroup to our temp array
            polyGroup.Add(polylineGroup);


            #endregion

            gradientProfileRequest.PolylineGroups = polyGroup.ToArray();

            gradientProfileRequest.Settings.PointSeperator = pointSeperator;
            gradientProfileRequest.Settings.EastingNorthingSeperator = eastingNorthingSeparator;
            gradientProfileRequest.Settings.Resolution = 10;

            try
            {
                GradientProfileResult result = planner.GetGradientProfile(context, gradientProfileRequest);

                Assert.IsNotNull(result, "The result was null, this test Failed");
                Assert.IsTrue(result.CompletionStatus.Status == StatusType.Success, "The result status was NOT StatusType.Success, journey planning failed");
                Assert.IsNotNull(result.HeightGroups, "The result did not contain a height point object");
            }
            catch (Exception ex)
            {
                Assert.Fail("The test failed, with exception: " + ex.Message);
            }
        }

        #endregion

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
		#endregion

		
	}
}
