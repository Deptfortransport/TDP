// *********************************************** 
// NAME             : TestCycleJourneyPlannerAssembler.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 22 Sep 2010
// DESCRIPTION  	: Unit tests CycleJourneyPlannerAssembler class
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/Test/TestCycleJourneyPlannerAssembler.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:58   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 
                
                
using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using System.Globalization;
using System.Threading;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.CyclePlannerControl;
using System.Diagnostics;
using TransportDirect.Common.Logging;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;

using CycleJPDTO = TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1;
using CommonDTO = TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;


namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1.Test
{
    [TestFixture]
    public class TestCycleJourneyPlannerAssembler
    {
        #region Initialisation
        class TestCyclePlannerAssemblerInitialisation : IServiceInitialisation
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

                serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory());
                serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
                serviceCache.Add(ServiceDiscoveryKey.DataServices, new TestMockDataServicesFactory());
                serviceCache.Add(ServiceDiscoveryKey.GisQuery, new TestMockGisQuery());
                serviceCache.Add(ServiceDiscoveryKey.Cache, new TDCache());
                serviceCache.Add(ServiceDiscoveryKey.CycleAttributes, new CycleAttributesFactory());
            }

        }
        #endregion

        #region Private Fields
        private CycleJourneyRequest cycleJourneyRequest;
        private CycleJourneyResult cycleJourneyResult;
        private TransportDirect.Common.ResourceManager.TDResourceManager rm;
        ITDCyclePlannerRequest request;
        ITDCyclePlannerResult result;
        #endregion

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
            TDServiceDiscovery.Init(new TestCyclePlannerAssemblerInitialisation());
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
            cycleJourneyRequest = new CycleJourneyRequest();
            cycleJourneyRequest.ResultSettings = CycleJourneyPlannerAssembler.GetDefaultResultSettings();

            RequestLocation origin = new RequestLocation();
            origin.GridReference = new TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference();
            origin.GridReference.Easting = 432950;
            origin.GridReference.Northing = 334120;
            origin.Description = "DE1";
            origin.Type = LocationType.Coordinate;

            RequestLocation destination = new RequestLocation();
            destination.GridReference = new TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference();
            destination.GridReference.Easting = 430571;
            destination.GridReference.Northing = 334539;
            destination.Description = "DE3";
            destination.Type = LocationType.Coordinate;

            cycleJourneyRequest.JourneyRequest = new JourneyRequest();
            cycleJourneyRequest.JourneyRequest.OriginLocation = origin;
            cycleJourneyRequest.JourneyRequest.DestinationLocation = destination;
            cycleJourneyRequest.JourneyRequest.JourneyRequestId = 1234;

            request = CycleJourneyPlannerAssembler.CreateTDCycleJourneyRequest(cycleJourneyRequest);

            request.OriginLocation = ResolveLocation(new OSGridReference(432950, 334120), origin.Description);
            request.DestinationLocation = ResolveLocation(new OSGridReference(430571, 334539), destination.Description);

            

        }

        /// <summary>
        /// Method to initialise the journey result using the specified outward filename
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private ITDCyclePlannerResult InitialiseCycleJourneyResult(string fileName)
        {
            TestMockCyclePlannerFromFile cpManager = new TestMockCyclePlannerFromFile();

            cpManager.FilenameOutward = AppDomain.CurrentDomain.BaseDirectory + "\\" + fileName;

            request.IsReturnRequired = false;

            ITDCyclePlannerResult initialResult = null;
            initialResult = cpManager.CallCyclePlanner(request,"sessionId",0,false,false,"en-GB",string.Empty,true);

            rm = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.JOURNEY_PLANNER_SERVICE_RM);

            return initialResult;
        }

        
        #endregion 


        #region Request
        /// <summary>
        /// This tests if the method creates a tdjourney request domain object using an instansiated journeyrequest dto.
        /// </summary>
        [Test]
        public void CallCreateTDJourneyRequest()
        {
            CycleJPDTO.CycleJourneyRequest cjrDTO = new CycleJourneyRequest();

            cjrDTO.JourneyRequest = new JourneyRequest();
            cjrDTO.JourneyRequest.OutwardArriveBefore = false;
            cjrDTO.JourneyRequest.OutwardDateTime = new DateTime(2010, 09, 25);

            ITDCyclePlannerRequest tdcjr = new TDCyclePlannerRequest();

            tdcjr = CycleJourneyPlannerAssembler.CreateTDCycleJourneyRequest(cjrDTO);

            
            TDDateTime[] testOut = new TDDateTime[] { new DateTime(2010, 09, 25) };

            // locations are not set so should be null
            Assert.AreEqual(tdcjr.OriginLocation, null);
            Assert.AreEqual(tdcjr.DestinationLocation, null);
            Assert.AreEqual(tdcjr.CycleViaLocations, null);

            // RequestPreferences are set to default values
            Assert.AreEqual(tdcjr.UserPreferences.Length.ToString(), Properties.Current["CyclePlanner.TDUserPreference.NumberOfPreferences"]);
            Assert.AreEqual(tdcjr.UserPreferences[0].PreferenceValue, "850");
            Assert.AreEqual(tdcjr.UserPreferences[1].PreferenceValue, "11000");

            // Cycle algorithm is set to default algorithm specified
            Assert.AreEqual(tdcjr.CycleJourneyType, Properties.Current["CyclePlanner.PlannerControl.PenaltyFunction.Default"]);
            Assert.IsNotEmpty(tdcjr.PenaltyFunction);

            Assert.IsFalse(tdcjr.IsReturnRequired);

            // ResultSettings
            Assert.AreEqual(tdcjr.ResultSettings.EastingNorthingSeparator, ',');
            Assert.AreEqual(tdcjr.ResultSettings.PointSeparator, ' ');
            Assert.AreEqual(tdcjr.ResultSettings.IncludeGeometry, false);


        }


        /// <summary>
        /// This tests if the Method returns a null domain object when provided with a null dto object.
        /// </summary>
        [Test]
        public void CallCreateTDJourneyRequest_null()
        {
            ITDCyclePlannerRequest tdcjr = new TDCyclePlannerRequest();
            tdcjr = CycleJourneyPlannerAssembler.CreateTDCycleJourneyRequest(null);
            Assert.AreEqual(tdcjr, null);
        }		
        #endregion

        #region result
        /// <summary>
        /// This tests if the Method converts an ITDourneyRequest object to a PublicJourneyResult object when supplied with a single journey result.
        /// </summary>
        [Test]
        public void CallCreateCycleJourneyResultDT_DE1_DE3()
        {
            result = InitialiseCycleJourneyResult("DE1_DE3_Xml_Cycle_Journey_Result.xml");

            JourneyResult journeyResult = CycleJourneyPlannerAssembler.CreateJourneyResultDT(1234,request,
                                                result,
                                                cycleJourneyRequest.ResultSettings,
                                                rm,
                                                false,
                                                string.Empty, TDExceptionIdentifier.Undefined);

            
            Assert.IsNotNull(journeyResult);
            Assert.IsNotNull(journeyResult.OutwardCycleJourney);

            cycleJourneyResult = CycleJourneyPlannerAssembler.CreateCycleJourneyResultDT(journeyResult);

            Assert.IsNotNull(cycleJourneyResult);

            Assert.AreEqual(cycleJourneyResult.CompletionStatus.Status,CommonDTO.StatusType.Success);

            CycleJourney journey = journeyResult.OutwardCycleJourney;

            Assert.IsNotNull(journey);

            Assert.IsNotNull(journey.Summary);

            Assert.IsNotNull(journey.Details);

            

            // Check Response Location
            Assert.AreEqual(journey.Summary.OriginLocation.Description, "DE1");
            Assert.AreEqual(journey.Summary.DestinationLocation.Description, "DE3");

            // Check Journey Summary
            Assert.AreEqual(journey.Summary.DistanceUnit, CycleJPDTO.DistanceUnit.Miles);
            Assert.Greater( journey.Summary.Distance, 0);
            

            // Check Journey Detail
            Assert.AreEqual(journey.Details.Length, 30);
            Assert.AreEqual(journey.Details[0].InstructionNumber, 1);
            Assert.AreEqual(journey.Details[0].InstructionText, "Starting from DE1");
            Assert.AreEqual(journey.Details[5].IsPath, true);
            Assert.AreEqual(journey.Details[9].IsCycleInfrastructure, true);
            Assert.AreEqual(journey.Details[10].IsRecommendedCycleRoute, true);
            Assert.AreEqual(journey.Details[29].InstructionText, "Arrive at  DE3");
            Assert.AreEqual(journey.Summary.Distance.ToString("F1",TDCultureInfo.CurrentUICulture.NumberFormat), journey.Details[29].CumulativeDistance);
            // Check Instruction Text
        }

        /// <summary>
        /// This tests if the Method converts an ITDCyclePlannerRequest object containing user warnings to a CyclePlannerResult and verifies that the warnings have been passed to the new object.
        /// </summary>
        [Test]
        public void CallCreateCycleJourneyResultDT_DE1_DE3_AddedWarning()
        {
            result = InitialiseCycleJourneyResult("DE1_DE3_Xml_Cycle_Journey_Result.xml");

            result.AddMessageToArray(rm.GetString("JourneyPlannerOutput.JourneyTimeInPast"),
               "JourneyPlannerOutput.JourneyTimeInPast",
               0,
               0,
               ErrorsType.Warning);

            JourneyResult journeyResult = CycleJourneyPlannerAssembler.CreateJourneyResultDT(1234, request,
                                                result,
                                                cycleJourneyRequest.ResultSettings,
                                                rm,
                                                false,
                                                string.Empty, TDExceptionIdentifier.Undefined);



            cycleJourneyResult = CycleJourneyPlannerAssembler.CreateCycleJourneyResultDT(journeyResult);

            Console.WriteLine(cycleJourneyResult.JourneyResult.UserWarnings[0]);
            Assert.AreEqual(cycleJourneyResult.JourneyResult.UserWarnings[0].Text,
                "Some of these journey options start in the past.");
        }


        /// <summary>
        /// This tests if the Method returns a null dto object when provided with a null domain object.
        /// </summary>
        [Test]
        public void CallCreateCycleJourneyResultDT_null()
        {
            result = null;

            JourneyResult journeyResult = CycleJourneyPlannerAssembler.CreateJourneyResultDT(1234, request,
                                               result,
                                               cycleJourneyRequest.ResultSettings,
                                               rm,
                                               false,
                                               string.Empty, TDExceptionIdentifier.Undefined);

            Assert.IsNull(journeyResult);

            cycleJourneyResult = CycleJourneyPlannerAssembler.CreateCycleJourneyResultDT(journeyResult);

            Assert.AreEqual(cycleJourneyResult.CompletionStatus.Status, CommonDTO.StatusType.Failed);
            
        }

        #endregion

        #region Private Helper Methods
        /// <summary>
        /// This method will resolve a coordinate location for Cycle journey planning
        /// </summary>
        /// <param name="osgr"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        private TDLocation ResolveLocation(OSGridReference osgr, string description)
        {
            // Create a new TDLocation and populate the name and coordiante
            TDLocation location = new TDLocation();

            location.Description = description;
            location.GridReference = osgr;
            location.RequestPlaceType = TransportDirect.JourneyPlanning.CJPInterface.RequestPlaceType.Coordinate;

            // Populate the Toids to be used for the cycle journey
            location.PopulateToids();

            // Populate the points which will be same as OSGRs
            location.PopulatePoint(false);
            
            // Location is assumed to be valid
            location.Status = TDLocationStatus.Valid;

            return location;
        }
        #endregion

    }
}
