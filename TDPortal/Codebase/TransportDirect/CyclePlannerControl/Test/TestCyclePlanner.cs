// *********************************************** 
// NAME                 : TestCyclePlanner.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 10/12/2008
// DESCRIPTION  	    : Nunit class for testing the Cycle Planner service and Gradient Profiler service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/Test/TestCyclePlanner.cs-arc  $ 
//
//   Rev 1.1   Jun 04 2009 14:02:10   mmodi
//Added test for Coordinate convertor
//Resolution for 5293: Cycle Planner - Coordinate convertor (Quest InGrid) to be added as a Web Service
//
//   Rev 1.0   Dec 12 2008 11:25:58   mmodi
//Initial revision.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.CoordinateConvertorProvider;
using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;
using TransportDirect.UserPortal.CyclePlannerService.GradientProfilerWebService;

using CP = TransportDirect.UserPortal.CyclePlannerService.CyclePlannerWebService;
using GP = TransportDirect.UserPortal.CyclePlannerService.GradientProfilerWebService;
using CCP = TransportDirect.UserPortal.CoordinateConvertorProvider.CoordinateConvertorWebService;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.CyclePlannerControl.Test
{
    [TestFixture]
    public class TestCyclePlanner
    {
        /// <summary>
		/// Initialisation in setup method called before every test method
		/// </summary>
        [TestFixtureSetUp]
        public void Init()
        {
            TDServiceDiscovery.ResetServiceDiscoveryForTest();
            TDServiceDiscovery.Init(new TestCyclePlannerInitialization());
        }

        [TestFixtureTearDown]
        public void TearDown()
        {
        }

        /// <summary>
		/// Tests a cycle journey is returned from the Cycle Planner web service
		/// </summary>
        [NUnit.Framework.Test]
        public void TestCyclePlannerService()
        {
            JourneyRequest request = new JourneyRequest();
            
            #region Setup Request

            #region Initialise the request

            request.requestID = "1234567890";
            request.referenceTransaction = true;
            request.sessionID = "qwerty123456789";
            request.language = "en-GB";
            request.userType = 2;

            #endregion

            #region Set locations and date/times, plus any TOIDs

            request.depart = true;
            
            RequestPlace requestPlaceOrigin = new RequestPlace();
            requestPlaceOrigin.givenName = "L1";
            requestPlaceOrigin.timeDate = DateTime.Now;
            requestPlaceOrigin.coordinate = new Coordinate();
            requestPlaceOrigin.coordinate.easting = 334920;
            requestPlaceOrigin.coordinate.northing = 389867;
            requestPlaceOrigin.roadPoints = new ITN[0];
            
            request.origin = requestPlaceOrigin;
            
            RequestPlace requestPlaceDestination = new RequestPlace();
            requestPlaceDestination.givenName = "L2";
            requestPlaceDestination.coordinate = new Coordinate();
            requestPlaceDestination.coordinate.easting = 334330;
            requestPlaceDestination.coordinate.northing = 390524;
            requestPlaceDestination.roadPoints = new ITN[0];

            request.destination = requestPlaceDestination;
           
            #endregion

            #region Set via locations and date/time

            request.vias = new RequestPlace[0];
            
            #endregion

            #region Set user preferences

            ArrayList userPreferences = new ArrayList();

            UserPreference userPreference = new UserPreference();
            userPreference.parameterID = 0;
            userPreference.parameterValue = "850";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 1;
            userPreference.parameterValue = "11000";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 2;
            userPreference.parameterValue = "Congestion";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 3;
            userPreference.parameterValue = "False";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 4;
            userPreference.parameterValue = "Bicycle";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 5;
            userPreference.parameterValue = "19";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 6;
            userPreference.parameterValue = "False";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 7;
            userPreference.parameterValue = "False";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 8;
            userPreference.parameterValue = "False";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 9;
            userPreference.parameterValue = "False";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 10;
            userPreference.parameterValue = "";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 11;
            userPreference.parameterValue = "";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 12;
            userPreference.parameterValue = "False";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 13;
            userPreference.parameterValue = "False";
            userPreferences.Add(userPreference);

            userPreference = new UserPreference();
            userPreference.parameterID = 14;
            userPreference.parameterValue = "False";
            userPreferences.Add(userPreference);
            
            request.userPreferences = (UserPreference[])userPreferences.ToArray(typeof(UserPreference));

            #endregion

            #region Set penalty function

            request.penaltyFunction = @"Call C:\CyclePlanner\Services\RoadInterfaceHostingService\td.cp.CyclePenaltyFunctions.v1.dll, TransportDirect.JourneyPlanning.CyclePenaltyFunctions.Quickest";

            #endregion

            #region Set journey result settings

            JourneyResultSettings resultSettings = new JourneyResultSettings();

            // Get result setting values from properties
            resultSettings.includeToids = true;
            resultSettings.includeGeometry = true;
            resultSettings.includeText = true;
            resultSettings.pointSeparator = char.Parse(" ");
            resultSettings.eastingNorthingSeparator = char.Parse(",");

            request.journeyResultSettings = resultSettings;

            #endregion

            #endregion

            ICyclePlanner cyclePlanner = (ICyclePlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.CyclePlannerFactory];

            CyclePlannerResult result = cyclePlanner.CycleJourneyPlan(request);

            JourneyResult journeyResult = (JourneyResult)result;

            string errorMessage = string.Empty;
            if (result.messages != null)
            {
                foreach (CP.Message message in result.messages)
                {
                    errorMessage = message.code + " " + message.description + "\n";
                }
            }

            Assert.IsNotNull(journeyResult.journeys, "No cycle journeys were in the result object: \n" + errorMessage);

            Assert.GreaterOrEqual(journeyResult.journeys.Length, 1, "0 cycle journeys were returned");
        }

        /// <summary>
        /// Tests a gradient profile is returned from the Cycle Planner Gradient Profile web service
        /// </summary>
        [NUnit.Framework.Test]
        public void TestGradientProfileService()
        {
            GradientProfileRequest request = new GradientProfileRequest();

            #region Setup Request

            #region Initialise the request

            request.requestID = "123456";
            request.referenceTransaction = true;
            request.sessionID = "qwert123456";
            request.language = "en-GB";
            request.userType = 2;

            #endregion

            #region Set up the Polyline groups

            char pointSeperator = char.Parse(" ");
            char eastingNorthingSeparator = char.Parse(",");

            PolylineGroup polylineGroup;
            Polyline polyline;
            ArrayList polylineArray;

            // Create a temp array to hold the PolylineGroups
            ArrayList polylineGroupArray = new ArrayList();
            
            #region Polyline group 1

            polylineGroup = new PolylineGroup();
            polylineGroup.groupID = 1;
            
            // Create a temp array to hold the Polylines
            polylineArray = new ArrayList();

            #region Polylines added to group 1

            polyline = new Polyline();
            polyline.polylineID = 1;
            polyline.interpolateGradient = false;
            polyline.polyline = "334906,389873 334895,389852";

            // Add the web Polyline to our temp array
            polylineArray.Add(polyline);

            #endregion
            
            // All polylines have been set for this group, so add to the web PolylineGroup
            polylineGroup.polylines = (Polyline[])polylineArray.ToArray(typeof(Polyline));
                        
            // Add the web PolylineGroup to our temp array
            polylineGroupArray.Add(polylineGroup);

            #endregion

            #region Polyline group 2

            polylineGroup = new PolylineGroup();
            polylineGroup.groupID = 2;

            // Create a temp array to hold the Polylines
            polylineArray = new ArrayList();

            #region Polylines added to group 2

            polyline = new Polyline();
            polyline.polylineID = 2;
            polyline.interpolateGradient = false;
            polyline.polyline = "334895,389852 334779,389900 334745,389914";

            // Add the web Polyline to our temp array
            polylineArray.Add(polyline);

            polyline = new Polyline();
            polyline.polylineID = 3;
            polyline.interpolateGradient = false;
            polyline.polyline = "334745,389914 334660,389948";

            // Add the web Polyline to our temp array
            polylineArray.Add(polyline);

            #endregion

            // All polylines have been set for this group, so add to the web PolylineGroup
            polylineGroup.polylines = (Polyline[])polylineArray.ToArray(typeof(Polyline));

            // Add the web PolylineGroup to our temp array
            polylineGroupArray.Add(polylineGroup);

            #endregion

            // All polylines have been processed, so add to the request
            request.polylineGroups = (PolylineGroup[])polylineGroupArray.ToArray(typeof(PolylineGroup));

            #endregion

            #region Set settings

            Settings settings = new Settings();

            settings.pointSeparator = pointSeperator;
            settings.eastingNorthingSeparator = eastingNorthingSeparator;
            settings.resolution = 10;

            request.settings = settings;

            #endregion

            #endregion

            ICyclePlanner cyclePlanner = (ICyclePlanner)TDServiceDiscovery.Current[ServiceDiscoveryKey.CyclePlannerFactory];

            GradientProfileResult result = cyclePlanner.GradientProfiler(request);

            string errorMessage = string.Empty;
            if (result.messages != null)
            {
                foreach (GP.Message message in result.messages)
                {
                    errorMessage = message.code + " " + message.description + "\n";
                }
            }

            Assert.IsNotNull(result.gradientProfile, "No gradient profile was in the result object: \n" + errorMessage);

            Assert.GreaterOrEqual(result.gradientProfile.groups.Length, 1, "No gradient profile groups were in the result object");
        }

        /// <summary>
        /// Tests an OSGR coordinate is converted to a Latitude Longitude using the Coordinate Convertor web service
        /// </summary>
        [NUnit.Framework.Test]
        public void TestCoordinateConvertorService()
        {
            #region Setup Request

            CCP.OSGridReference osgr = new CCP.OSGridReference();

            osgr.Easting = 334920;
            osgr.Northing = 389867;

            #endregion

            ICoordinateConvertor coordinateConvertor = (ICoordinateConvertor)TDServiceDiscovery.Current[ServiceDiscoveryKey.CoordinateConvertorFactory];

            CCP.LatitudeLongitude result = coordinateConvertor.GetLatitudeLongitude(osgr);

            Assert.IsNotNull(result, "No latitude longitude returned, result was null.\n");

            bool resultValid = (result.Latitude >= 53.40 && result.Longitude <= -2.98); // These are the values it should be

            Assert.IsTrue(resultValid, string.Format("Latitude longitude result was not valid: [{0},{1}].", result.Latitude.ToString(), result.Longitude.ToString()));
        }
    }

    #region initialisation class

	/// <summary>
	/// Initialisation class 
	/// </summary>
    public class TestCyclePlannerInitialization : IServiceInitialisation
    {
        /// <summary>
        /// Populates sevice cache with services needed 
        /// </summary>
        /// <param name="serviceCache">Cache to populate.</param>
        public void Populate(Hashtable serviceCache)
        {
            // Add cryptographic scheme
            //serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory());

            // Enable PropertyService					
            serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

            // Enable CyclePlannerFactory
            serviceCache.Add(ServiceDiscoveryKey.CyclePlannerFactory, new CyclePlannerFactory());

            // Enable CoordinateConvertorProvider
            serviceCache.Add(ServiceDiscoveryKey.CoordinateConvertorFactory, new CoordinateConvertorFactory());

            // Enable logging service.
            ArrayList errors = new ArrayList();
            try
            {
                IEventPublisher[] customPublishers = new IEventPublisher[0];

                Trace.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
            }
            catch (TDException)
            {
                foreach (string error in errors)
                {
                    Console.WriteLine(error);
                }
                throw;
            }

        }
    }
	#endregion
}
