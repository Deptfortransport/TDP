// ***********************************************
// NAME 		: TestOpenJourneyPlannerAssembler.cs
// AUTHOR 		: C.M. Owczarek
// DATE CREATED : 31.03.06
// DESCRIPTION 	: Test fixture to test all methods of OpenJourneyPlannerAssembler class
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Test/TestOpenJourneyPlannerAssembler.cs-arc  $
//
//   Rev 1.1   Feb 18 2010 14:16:48   MTurner
//Changed modes used in test as car has been removed.
//Resolution for 5404: EES - Open journey planner throws exception
//
//   Rev 1.0   Nov 08 2007 12:22:48   mturner
//Initial revision.
//
//   Rev 1.2   Apr 10 2006 16:36:10   COwczarek
//Code review rework
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
//   Rev 1.1   Apr 07 2006 14:18:32   COwczarek
//Additional tests for null NaPTAN ids and notes.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//
//   Rev 1.0   Apr 06 2006 16:20:06   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service

using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Threading;
using System.Xml.Serialization;
using System.IO;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1;
using CJPInterface = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1.Test
{

	/// <summary>
	/// Test class to test all methods of OpenJourneyPlannerAssembler class
	/// </summary>
	[TestFixture]
	public class TestOpenJourneyPlannerAssembler
	{

        /// <summary>
        /// Initialisation method called before every test method call
        /// </summary>
		[SetUp]
		public void SetUp()
		{
		}

        /// <summary>
        /// Finalisation method called after every test method call
        /// </summary>
        [TearDown]
        public void TearDown() 
        {
        }
	
        #region CreateTransportModes Tests

        /// <summary>
        /// Test to ensure CreateTransportModes includes correct modes
        /// </summary>
        [Test]
        public void CallCreateTransportModesIncluded()
        {

            TransportModes transportModes = new TransportModes();
            transportModes.Include = true;
            transportModes.Modes = new TransportMode[2];
            transportModes.Modes[0] = new TransportMode();
            transportModes.Modes[0].Mode = TransportModeType.Rail;
            transportModes.Modes[1] = new TransportMode();
            transportModes.Modes[1].Mode = TransportModeType.Air;

            CJPInterface.Modes modes = OpenJourneyPlannerAssembler.CreateTransportModes(transportModes);

            Assert.IsNotNull(modes);
            Assert.IsTrue(modes.include);
            Assert.IsNotNull(modes.modes);
            Assert.AreEqual(modes.modes.Length,2);
            Assert.IsTrue(checkModes(modes.modes, new CJPInterface.ModeType[] 
                    {
                        CJPInterface.ModeType.Rail,
                        CJPInterface.ModeType.Air
                    }
                ));

        }

        /// <summary>
        /// Test to ensure CreateTransportModes includes correct modes when Bus selected
        /// </summary>
        [Test]
        public void CallCreateTransportModesIncludedBus()
        {

            TransportModes transportModes = new TransportModes();
            transportModes.Include = true;
            transportModes.Modes = new TransportMode[1];
            transportModes.Modes[0] = new TransportMode();
            transportModes.Modes[0].Mode = TransportModeType.Bus;

            CJPInterface.Modes modes = OpenJourneyPlannerAssembler.CreateTransportModes(transportModes);

            Assert.IsNotNull(modes);
            Assert.IsTrue(modes.include);
            Assert.IsNotNull(modes.modes);
            Assert.AreEqual(modes.modes.Length,3);
            Assert.IsTrue(checkModes(modes.modes, new CJPInterface.ModeType[] 
                    {
                        CJPInterface.ModeType.Bus,
                        CJPInterface.ModeType.Coach,
                        CJPInterface.ModeType.Ferry
                    }
                ));

        }

        /// <summary>
        /// Test to ensure CreateTransportModes includes correct modes when Coach selected
        /// </summary>
        [Test]
        public void CallCreateTransportModesIncludedCoach()
        {

            TransportModes transportModes = new TransportModes();
            transportModes.Include = true;
            transportModes.Modes = new TransportMode[1];
            transportModes.Modes[0] = new TransportMode();
            transportModes.Modes[0].Mode = TransportModeType.Coach;

            CJPInterface.Modes modes = OpenJourneyPlannerAssembler.CreateTransportModes(transportModes);

            Assert.IsNotNull(modes);
            Assert.IsTrue(modes.include);
            Assert.IsNotNull(modes.modes);
            Assert.AreEqual(modes.modes.Length,3);
            Assert.IsTrue(checkModes(modes.modes, new CJPInterface.ModeType[] 
                    {
                        CJPInterface.ModeType.Bus,
                        CJPInterface.ModeType.Coach,
                        CJPInterface.ModeType.Ferry
                    }
                ));

        }

        /// <summary>
        /// Test to ensure CreateTransportModes includes correct modes when DRT mode selected
        /// </summary>
        [Test]
        public void CallCreateTransportModesIncludedDrt()
        {

            TransportModes transportModes = new TransportModes();
            transportModes.Include = true;
            transportModes.Modes = new TransportMode[1];
            transportModes.Modes[0] = new TransportMode();
            transportModes.Modes[0].Mode = TransportModeType.Drt;

            CJPInterface.Modes modes = OpenJourneyPlannerAssembler.CreateTransportModes(transportModes);

            Assert.IsNotNull(modes);
            Assert.IsTrue(modes.include);
            Assert.IsNotNull(modes.modes);
            Assert.AreEqual(modes.modes.Length,2);
            Assert.IsTrue(checkModes(modes.modes, new CJPInterface.ModeType[] 
                    {
                        CJPInterface.ModeType.Bus,
                        CJPInterface.ModeType.Drt
                    }
                ));

        }

        /// <summary>
        /// Test to ensure CreateTransportModes excludes correct modes
        /// </summary>
        [Test]
        public void CallCreateTransportModesExcluded()
        {

            TransportModes transportModes = new TransportModes();
            transportModes.Include = false;
            transportModes.Modes = new TransportMode[2];
            transportModes.Modes[0] = new TransportMode();
            transportModes.Modes[1] = new TransportMode();
            transportModes.Modes[0].Mode = TransportModeType.Rail;
            transportModes.Modes[1].Mode = TransportModeType.Metro;

            CJPInterface.Modes modes = OpenJourneyPlannerAssembler.CreateTransportModes(transportModes);

            Assert.IsNotNull(modes);
            Assert.IsFalse(modes.include);
            Assert.IsNotNull(modes.modes);
            Assert.AreEqual(modes.modes.Length,2);
            Assert.IsTrue(checkModes(modes.modes, new CJPInterface.ModeType[] 
                    {
                        CJPInterface.ModeType.Rail,
                        CJPInterface.ModeType.Metro
                    }
                ));

        }

        /// <summary>
        /// Test to ensure null is returned from CreateTransportModes if no modes supplied
        /// </summary>
        [Test]
        public void CallCreateTransportModesNoModes()
        {

            TransportModes transportModes = new TransportModes();
            transportModes.Include = true;
            transportModes.Modes = null;

            Assert.IsNull(OpenJourneyPlannerAssembler.CreateTransportModes(transportModes).modes);

        }

        #endregion CreateTransportModes Tests

        #region CreateOperators Tests

        /// <summary>
        /// Test to ensure CreateOperators includes correct operators
        /// </summary>
        [Test]
        public void CallCreateOperators()
        {

            Operators operators = new Operators();
            operators.Include = true;
            operators.OperatorCodes = new OperatorCode[2];
            operators.OperatorCodes[0] = new OperatorCode();
            operators.OperatorCodes[1] = new OperatorCode();
            operators.OperatorCodes[0].Code = "xyz";
            operators.OperatorCodes[1].Code = "abc";

            CJPInterface.Operators returnOperators = OpenJourneyPlannerAssembler.CreateOperators(operators);

            Assert.IsNotNull(returnOperators);
            Assert.IsTrue(returnOperators.include);
            Assert.AreEqual(returnOperators.operatorCodes.Length,2);
            Assert.AreEqual(returnOperators.operatorCodes[0].operatorCode,"xyz");
            Assert.AreEqual(returnOperators.operatorCodes[1].operatorCode,"abc");

        }

        /// <summary>
        /// Test to ensure null is returned from CreateOperators if no operators supplied
        /// </summary>
        [Test]
        public void CallCreateOperatorNoOperators()
        {

            Operators operators = new Operators();
            operators.Include = true;
            operators.OperatorCodes = null;

            Assert.IsNull(OpenJourneyPlannerAssembler.CreateOperators(operators).operatorCodes);

        }

        #endregion CreateOperators Tests

        #region CreateServiceFilter Tests

        /// <summary>
        /// Test to ensure CreateServiceFilter includes correct services
        /// </summary>
        [Test]
        public void CallCreateServiceFilter()
        {

            ServiceFilter serviceFilter = new ServiceFilter();
            serviceFilter.Include = true;
            serviceFilter.Services = new RequestService[2];
            serviceFilter.Services[0] = new RequestService();
            serviceFilter.Services[1] = new RequestService();

            serviceFilter.Services[0].Type = RequestServiceType.RequestServiceNumber;
            serviceFilter.Services[0].OperatorCode = "xyz";
            serviceFilter.Services[0].ServiceNumber = "svc1";
            serviceFilter.Services[1].PrivateID = "id1";

            CJPInterface.ServiceFilter returnServiceFilter = OpenJourneyPlannerAssembler.CreateServiceFilter(serviceFilter);

            Assert.IsNotNull(returnServiceFilter);
            Assert.IsTrue(returnServiceFilter.include);
            Assert.AreEqual(returnServiceFilter.services.Length,2);
            Assert.AreEqual(((CJPInterface.RequestServiceNumber)returnServiceFilter.services[0]).operatorCode,"xyz");
            Assert.AreEqual(((CJPInterface.RequestServiceNumber)returnServiceFilter.services[0]).serviceNumber,"svc1");
            Assert.AreEqual(((CJPInterface.RequestServicePrivate)returnServiceFilter.services[1]).privateID,"id1");

        }

        /// <summary>
        /// Test to ensure null is returned from CreateServiceFilter if no services supplied
        /// </summary>
        [Test]
        public void CallCreateServiceFilterNoFilters()
        {

            ServiceFilter serviceFilter = new ServiceFilter();
            serviceFilter.Include = true;
            serviceFilter.Services = null;

            Assert.IsNull(OpenJourneyPlannerAssembler.CreateServiceFilter(serviceFilter).services);

        }

        #endregion CreateServiceFilter Tests

        #region CreateRequestPlaces Tests

        /// <summary>
        /// Test to ensure CreateRequestPlaces initalises an array of RequestPlace domain objects correctly
        /// </summary>
        [Test]
        public void CallCreateRequestPlaces()
        {

            RequestPlace[] requestPlaces = new RequestPlace[2];

            RequestPlace requestPlace1 = new RequestPlace();
            requestPlace1.Type = RequestPlaceType.Coordinate;
            requestPlace1.GivenName = "abc1";
            requestPlace1.Locality = "xyz1";
            requestPlace1.Stops = null;
            requestPlace1.Coordinate = null;

            RequestStop[] requestStops = new RequestStop[1];
            requestStops[0] = new RequestStop();
            requestStops[0].TimeDate = new DateTime(2006,4,4);
            requestStops[0].NaPTANID = "AB1234";
            requestStops[0].Coordinate = null;

            RequestPlace requestPlace2 = new RequestPlace();
            requestPlace2.Type = RequestPlaceType.Locality;
            requestPlace2.GivenName = "abc2";
            requestPlace2.Locality = "xyz2";
            requestPlace2.Stops = requestStops;
            requestPlace2.Coordinate = null;

            requestPlaces[0] = requestPlace1;
            requestPlaces[1] = requestPlace2;

            CJPInterface.RequestPlace[] returnRequestPlaces = OpenJourneyPlannerAssembler.CreateRequestPlaces(requestPlaces);

            Assert.IsNotNull(returnRequestPlaces);
            Assert.AreEqual(returnRequestPlaces.Length,2);

            Assert.AreEqual(returnRequestPlaces[0].givenName,"abc1");
            Assert.AreEqual(returnRequestPlaces[0].locality,"xyz1");

            Assert.AreEqual(returnRequestPlaces[1].givenName,"abc2");
            Assert.AreEqual(returnRequestPlaces[1].locality,"xyz2");
            Assert.IsNotNull(returnRequestPlaces[1].stops);
            Assert.AreEqual(returnRequestPlaces[1].stops.Length,1);
            Assert.AreEqual(returnRequestPlaces[1].stops[0].NaPTANID,"AB1234");
            Assert.AreEqual(returnRequestPlaces[1].stops[0].timeDate,new DateTime(2006,4,4));

        }

        #endregion CreateRequestPlaces Tests

        #region CreateRequestStop Tests

        /// <summary>
        /// Test to ensure CreateRequestStop initalises domain object correctly
        /// </summary>
        [Test]
        public void CallCreateRequestStop()
        {

            RequestStop requestStop = new RequestStop();

            requestStop.Coordinate = null;
            requestStop.NaPTANID = "AB1234";
            requestStop.TimeDate = new DateTime(2006,4,4);

            CJPInterface.RequestStop returnRequestStop = OpenJourneyPlannerAssembler.CreateRequestStop(requestStop);

            Assert.IsNotNull(returnRequestStop);

            Assert.IsNull(returnRequestStop.coordinate);
            Assert.IsNull(returnRequestStop.seed);
            Assert.AreEqual(returnRequestStop.NaPTANID,"AB1234");
            Assert.AreEqual(returnRequestStop.timeDate, new DateTime(2006,4,4));

        }

        /// <summary>
        /// Test to ensure CreateRequestStop initalises domain object correctly 
        /// (correct handling of null NaPTAN id)
        /// </summary>
        [Test]
        public void CallCreateRequestStopNullNaptanId()
        {

            RequestStop requestStop = new RequestStop();

            requestStop.Coordinate = null;
            requestStop.NaPTANID = null;
            requestStop.TimeDate = new DateTime(2006,4,4);

            CJPInterface.RequestStop returnRequestStop = OpenJourneyPlannerAssembler.CreateRequestStop(requestStop);

            Assert.IsNotNull(returnRequestStop);

            Assert.IsNull(returnRequestStop.coordinate);
            Assert.IsNull(returnRequestStop.seed);
            Assert.AreEqual(returnRequestStop.NaPTANID,string.Empty);
            Assert.AreEqual(returnRequestStop.timeDate, new DateTime(2006,4,4));

        }


        #endregion CreateRequestStop Tests

        #region CreatePublicParameters Tests

        /// <summary>
        /// Test to ensure CreatePublicParameters initalises domain object correctly
        /// </summary>
        [Test]
        public void CallCreatePublicParameters()
        {

            PublicParameters publicParameters = new PublicParameters();

            publicParameters.Algorithm = PublicAlgorithmType.Fastest;
            publicParameters.ExtraCheckInTime = new DateTime(2006,04,04);
            publicParameters.ExtraCheckInTimeSpecified = true;
            publicParameters.InterchangeSpeed = 10;
            publicParameters.IntermediateStops = IntermediateStopsType.All;
            publicParameters.Interval = new DateTime(2006,04,05);
            publicParameters.IntervalSpecified = true;
            publicParameters.MaxWalkDistance = 20;
            publicParameters.NotVias = null;
            publicParameters.RangeType = RangeType.Interval;
            publicParameters.Sequence = 1;
            publicParameters.SequenceSpecified = true;
            publicParameters.SoftVias = null;
            publicParameters.TrunkPlan = true;
            publicParameters.Vias = null;
            publicParameters.WalkSpeed = 30;

            CJPInterface.PublicParameters returnPublicParameters = 
                OpenJourneyPlannerAssembler.CreatePublicParameters(publicParameters);

            Assert.IsNotNull(returnPublicParameters);

            Assert.AreEqual(returnPublicParameters.algorithm, CJPInterface.PublicAlgorithmType.Fastest);
            Assert.AreEqual(returnPublicParameters.extraCheckInTime, new DateTime(2006,04,04));
            Assert.AreEqual(returnPublicParameters.extraInterval,DateTime.MinValue);
            Assert.AreEqual(returnPublicParameters.extraSequence,0);
            Assert.AreEqual(returnPublicParameters.interchangeSpeed,10);
            Assert.AreEqual(returnPublicParameters.intermediateStops,CJPInterface.IntermediateStopsType.All);
            Assert.AreEqual(returnPublicParameters.interval,new DateTime(2006,04,05));
            Assert.AreEqual(returnPublicParameters.maxWalkDistance,20);
            Assert.AreEqual(returnPublicParameters.notVias,null);
            Assert.AreEqual(returnPublicParameters.rangeType,CJPInterface.RangeType.Interval);
            Assert.AreEqual(returnPublicParameters.routingStops,null);
            Assert.AreEqual(returnPublicParameters.sequence,1);
            Assert.AreEqual(returnPublicParameters.softVias,null);
            Assert.AreEqual(returnPublicParameters.trunkPlan,true);
            Assert.AreEqual(returnPublicParameters.vehicleFeatures,null);
            Assert.AreEqual(returnPublicParameters.vias,null);
            Assert.AreEqual(returnPublicParameters.walkSpeed,30);

            // Call following properties purely to get 100% code coverage.

            bool specified;
            specified = publicParameters.SequenceSpecified;
            specified = publicParameters.IntervalSpecified;
            specified = publicParameters.ExtraCheckInTimeSpecified;


        }

        #endregion CreatePublicParameters Tests

        #region CreateCoordinate Tests

        /// <summary>
        /// Test to ensure CreateCoordinate initalises domain object correctly
        /// </summary>
        [Test]
        public void CallCreateCoordinate()
        {

            Coordinate coordinate = new Coordinate();
            coordinate.Easting = 1234;
            coordinate.Northing = 5678;

            CJPInterface.Coordinate returnCoordinate = OpenJourneyPlannerAssembler.CreateCoordinate(coordinate);

            Assert.IsNotNull(returnCoordinate);

            Assert.AreEqual(returnCoordinate.easting,1234);
            Assert.AreEqual(returnCoordinate.northing,5678);

        }

        #endregion CreateCoordinate Tests

        #region CreateJourneyRequest Tests

        /// <summary>
        /// Test to ensure CreateJourneyRequest initalises domain object correctly
        /// </summary>
        [Test]
        public void CallCreateJourneyRequest()
        {

            Request request = new Request();
            request.Depart = true;
            request.Destination = null;
            request.ModeFilter = null;
            request.OperatorFilter = null;
            request.Origin = null;
            request.PublicParameters = null;
            request.ServiceFilter = null;

            CJPInterface.JourneyRequest newJourneyRequest = 
                OpenJourneyPlannerAssembler.CreateJourneyRequest(request,"en-gb","1234");

            Assert.IsNotNull(newJourneyRequest);

            Assert.AreEqual(newJourneyRequest.depart, true);
            Assert.AreEqual(newJourneyRequest.destination, null);
            Assert.AreEqual(newJourneyRequest.language,"en-gb");
            Assert.AreEqual(newJourneyRequest.modeFilter, null);
            Assert.AreEqual(newJourneyRequest.operatorFilter, null);
            Assert.AreEqual(newJourneyRequest.origin, null);
            Assert.AreEqual(newJourneyRequest.parkNRide,false);
            Assert.AreEqual(newJourneyRequest.privateParameters,null);
            Assert.AreEqual(newJourneyRequest.publicParameters, null);
            Assert.AreEqual(newJourneyRequest.referenceTransaction,false);
            Assert.AreEqual(newJourneyRequest.requestID,"1234");
            Assert.AreEqual(newJourneyRequest.serviceFilter, null);
            Assert.AreEqual(newJourneyRequest.sessionID,"1234");
            Assert.AreEqual(newJourneyRequest.userType,0);

        }

        #endregion CreateJourneyRequest Tests

        #region CreateResultDT Tests

        /// <summary>
        /// Test to ensure CreateResultDT initalises data transfer object correctly
        /// </summary>
        [Test]
        public void CallCreateResultDT()
        {

            CJPInterface.JourneyResult journeyResult = new CJPInterface.JourneyResult();
            journeyResult.messages = null;
            journeyResult.parknRideJourneys = null;
            journeyResult.privateJourneys = null;
            journeyResult.publicJourneys = null;

            Result newResult = 
                OpenJourneyPlannerAssembler.CreateResultDT(journeyResult);

            Assert.IsNotNull(newResult);

            Assert.AreEqual(newResult.Messages,null);
            Assert.AreEqual(newResult.PublicJourneys,null);

        }

        #endregion CreateResultDT Tests

        #region CreateMessagesDT Tests

        /// <summary>
        /// Test to ensure CreateMessagesDT initalises an array of data transfer objects correctly
        /// </summary>
        [Test]
        public void CallCreateMessagesDT()
        {

            CJPInterface.Message[] messages = new CJPInterface.Message[3];

            CJPInterface.JourneyWebMessage message1 = new CJPInterface.JourneyWebMessage();

            message1.code = 1;
            message1.description = "journeyweb";
            message1.region = "AB1234";
            message1.subClass = 11;

            CJPInterface.TTBOMessage message2 = new CJPInterface.TTBOMessage();

            message2.code = 2;
            message2.description = "TTBO";
            message2.subCode = 22;

            CJPInterface.Message message3 = new CJPInterface.Message();

            message3.code = 3;
            message3.description = "CJP message";


            messages[0] = message1;
            messages[1] = message2;
            messages[2] = message3;

            Message[] returnMessages = OpenJourneyPlannerAssembler.CreateMessagesDT(messages);

            Assert.IsNotNull(returnMessages);

            Assert.AreEqual(returnMessages.Length,3);

            Assert.AreEqual(returnMessages[0].Type,MessageType.JourneyWebMessage);
            Assert.AreEqual(returnMessages[0].Code,1);
            Assert.AreEqual(returnMessages[0].Description,"journeyweb");
            Assert.AreEqual(returnMessages[0].Region,"AB1234");
            Assert.AreEqual(returnMessages[0].SubClass,11);
            Assert.AreEqual(returnMessages[0].SubCode,0);

            Assert.AreEqual(returnMessages[1].Type,MessageType.RailEngineMessage);
            Assert.AreEqual(returnMessages[1].Code,2);
            Assert.AreEqual(returnMessages[1].Description,"TTBO");
            Assert.AreEqual(returnMessages[1].Region,null);
            Assert.AreEqual(returnMessages[1].SubClass,0);
            Assert.AreEqual(returnMessages[1].SubCode,22);

            Assert.AreEqual(returnMessages[2].Type,MessageType.JourneyPlannerMessage);
            Assert.AreEqual(returnMessages[2].Code,3);
            Assert.AreEqual(returnMessages[2].Description,"CJP message");
            Assert.AreEqual(returnMessages[2].Region,null);
            Assert.AreEqual(returnMessages[2].SubClass,0);
            Assert.AreEqual(returnMessages[2].SubCode,0);

        }

        #endregion CreateMessagesDT Tests

        #region CreatePublicJourneysDT Tests

        /// <summary>
        /// Test to ensure CreatePublicJourneysDT initalises an array of data transfer objects correctly
        /// </summary>
        [Test]
        public void CallCreatePublicJourneysDT()
        {

            CJPInterface.PublicJourney[] publicJourneys = new CJPInterface.PublicJourney[2];

            publicJourneys[0] = new CJPInterface.PublicJourney();
            publicJourneys[0].legs = null;
            publicJourneys[0].routingStops = null;

            publicJourneys[1] = new CJPInterface.PublicJourney();
            publicJourneys[1].legs = null;
            publicJourneys[1].routingStops = null;

            PublicJourney[] newPublicJourneys = 
                OpenJourneyPlannerAssembler.CreatePublicJourneysDT(publicJourneys);

            Assert.IsNotNull(newPublicJourneys);

            Assert.AreEqual(newPublicJourneys.Length,2);
            Assert.AreEqual(newPublicJourneys[0].Legs,null);
            Assert.AreEqual(newPublicJourneys[1].Legs,null);

        }

        #endregion CreatePublicJourneysDT Tests

        #region CallCreateLegsDT Tests

        /// <summary>
        /// Test to ensure CallCreateLegsDT initalises an array of data transfer objects correctly
        /// </summary>
        [Test]
        public void CallCreateLegsDT()
        {

            CJPInterface.Leg[] legs = new CJPInterface.Leg[3];

            CJPInterface.VehicleFeature[] vehicleFeatures = new CJPInterface.VehicleFeature[2];

            CJPInterface.BoolVehicleFeature vehicleFeature1 = new CJPInterface.BoolVehicleFeature();
            vehicleFeature1.id = 111;

            CJPInterface.TextVehicleFeature vehicleFeature2 = new CJPInterface.TextVehicleFeature();
            vehicleFeature2.id = 222;

            vehicleFeatures[0] = vehicleFeature1;
            vehicleFeatures[1] = vehicleFeature2;

            CJPInterface.ContinuousLeg leg1 = new CJPInterface.ContinuousLeg();

            leg1.alight = null;
            leg1.board = null;
            leg1.description = "desc1"; 
            leg1.destination = null;
            leg1.interchangeAllowance = 1;
            leg1.intermediatesA = null;
            leg1.intermediatesB = null;
            leg1.intermediatesC = null;
            leg1.mode = CJPInterface.ModeType.Walk;

            CJPInterface.Note[] notes = new CJPInterface.Note[2];
            notes[0] = new CJPInterface.Note();
            notes[0].message = "note1";
            notes[1] = new CJPInterface.Note();
            notes[1].message = "note2";
            leg1.notes = notes;

            leg1.notServices = null;
            leg1.origin = null;
            leg1.sections = null;
            leg1.services = null;
            leg1.typicalDuration = 2;
            leg1.validated = true;
            leg1.vehicleFeatures = vehicleFeatures;
            leg1.windowOfOpportunity = null;

            CJPInterface.FrequencyLeg leg2 = new CJPInterface.FrequencyLeg();
            leg2.alight = null;
            leg2.board = null;
            leg2.description = "desc2";
            leg2.destination = null;
            leg2.frequency = 11;
            leg2.intermediatesA = null;
            leg2.intermediatesB = null;
            leg2.intermediatesC = null;
            leg2.maxDuration = 22;
            leg2.maxFrequency = 33;
            leg2.minFrequency = 44;
            leg2.mode = CJPInterface.ModeType.Underground;
            leg2.notes = null;
            leg2.notServices = null;
            leg2.origin = null;
            leg2.services = null;
            leg2.typicalDuration = 55;
            leg2.validated = true;
            leg2.vehicleFeatures = null;
            leg2.windowOfOpportunity = null;

            CJPInterface.TimedLeg leg3 = new CJPInterface.TimedLeg();
            leg3.alight = null;
            leg3.board = null;
            leg3.description = "desc3";
            leg3.destination = null;
            leg3.intermediatesA = null;
            leg3.intermediatesB = null;
            leg3.intermediatesC = null;
            leg3.mode = CJPInterface.ModeType.Rail;
            leg3.notes = null;
            leg3.notServices = null;
            leg3.origin = null;
            leg3.services = null;
            leg3.validated = true;
            leg3.vehicleFeatures = null;

            legs[0] = leg1;
            legs[1] = leg2;
            legs[2] = leg3;

            Leg[] newLegs = 
                OpenJourneyPlannerAssembler.CreateLegsDT(legs);

            Assert.IsNotNull(newLegs);

            Assert.AreEqual(newLegs.Length,3);

            Assert.AreEqual(newLegs[0].Alight,null);
            Assert.AreEqual(newLegs[0].Board,null);
            Assert.AreEqual(newLegs[0].Description,"desc1");
            Assert.AreEqual(newLegs[0].Destination,null);
            Assert.AreEqual(newLegs[0].Frequency,0);
            Assert.AreEqual(newLegs[0].IntermediatesA,null);
            Assert.AreEqual(newLegs[0].IntermediatesB,null);
            Assert.AreEqual(newLegs[0].IntermediatesC,null);
            Assert.AreEqual(newLegs[0].MaxDuration,0);
            Assert.AreEqual(newLegs[0].MaxFrequency,0);
            Assert.AreEqual(newLegs[0].MinFrequency,0);
            Assert.AreEqual(newLegs[0].Mode,TransportModeType.Walk);

            Assert.IsNotNull(newLegs[0].Notes);
            Assert.AreEqual(newLegs[0].Notes.Length,2);
            Assert.AreEqual(newLegs[0].Notes[0],"note1");
            Assert.AreEqual(newLegs[0].Notes[1],"note2");

            Assert.AreEqual(newLegs[0].Origin,null);
            Assert.AreEqual(newLegs[0].Services,null);
            Assert.AreEqual(newLegs[0].Type,LegType.ContinuousLeg);
            Assert.AreEqual(newLegs[0].TypicalDuration,2);
            Assert.AreEqual(newLegs[0].Validated,true);
            Assert.AreEqual(newLegs[0].VehicleFeatures.Length,2);
            Assert.AreEqual(newLegs[0].VehicleFeatures[0],111);
            Assert.AreEqual(newLegs[0].VehicleFeatures[1],222);
            Assert.AreEqual(newLegs[0].WindowOfOpportunity,null);

            Assert.AreEqual(newLegs[1].Alight,null);
            Assert.AreEqual(newLegs[1].Board,null);
            Assert.AreEqual(newLegs[1].Description,"desc2");
            Assert.AreEqual(newLegs[1].Destination,null);
            Assert.AreEqual(newLegs[1].Frequency,11);
            Assert.AreEqual(newLegs[1].IntermediatesA,null);
            Assert.AreEqual(newLegs[1].IntermediatesB,null);
            Assert.AreEqual(newLegs[1].IntermediatesC,null);
            Assert.AreEqual(newLegs[1].MaxDuration,22);
            Assert.AreEqual(newLegs[1].MaxFrequency,33);
            Assert.AreEqual(newLegs[1].MinFrequency,44);
            Assert.AreEqual(newLegs[1].Mode,TransportModeType.Underground);
            Assert.AreEqual(newLegs[1].Notes,null);
            Assert.AreEqual(newLegs[1].Origin,null);
            Assert.AreEqual(newLegs[1].Services,null);
            Assert.AreEqual(newLegs[1].Type,LegType.FrequencyLeg);
            Assert.AreEqual(newLegs[1].TypicalDuration,55);
            Assert.AreEqual(newLegs[1].Validated,true);
            Assert.AreEqual(newLegs[1].VehicleFeatures,null);
            Assert.AreEqual(newLegs[1].WindowOfOpportunity,null);

            Assert.AreEqual(newLegs[2].Alight,null);
            Assert.AreEqual(newLegs[2].Board,null);
            Assert.AreEqual(newLegs[2].Description,"desc3");
            Assert.AreEqual(newLegs[2].Destination,null);
            Assert.AreEqual(newLegs[2].Frequency,0);
            Assert.AreEqual(newLegs[2].IntermediatesA,null);
            Assert.AreEqual(newLegs[2].IntermediatesB,null);
            Assert.AreEqual(newLegs[2].IntermediatesC,null);
            Assert.AreEqual(newLegs[2].MaxDuration,0);
            Assert.AreEqual(newLegs[2].MaxFrequency,0);
            Assert.AreEqual(newLegs[2].MinFrequency,0);
            Assert.AreEqual(newLegs[2].Mode,TransportModeType.Rail);
            Assert.AreEqual(newLegs[2].Notes,null);
            Assert.AreEqual(newLegs[2].Origin,null);
            Assert.AreEqual(newLegs[2].Services,null);
            Assert.AreEqual(newLegs[2].Type,LegType.TimedLeg);
            Assert.AreEqual(newLegs[2].TypicalDuration,0);
            Assert.AreEqual(newLegs[2].Validated,true);
            Assert.AreEqual(newLegs[2].VehicleFeatures,null);
            Assert.AreEqual(newLegs[2].WindowOfOpportunity,null);

        }

        #endregion CallCreateLegsDT Tests

        #region CallCreateEventsDT Tests

        /// <summary>
        /// Test to ensure CallCreateEventsDT initalises an array of data transfer objects correctly
        /// </summary>
        [Test]
        public void CallCreateEventsDT()
        {

            CJPInterface.Event[] events = new CJPInterface.Event[2];
            events[0] = new CJPInterface.Event();
            events[0].activity = CJPInterface.ActivityType.Arrive;
            events[0].arriveTime = new DateTime(2006,04,04);
            events[0].confirmedVia = true;
            events[0].departTime = new DateTime(2006,04,02);
            events[0].geometry = null;
            events[0].interchangeTime = 1;
            events[0].links = null;
            events[0].realTime = null;
            events[0].stop = null;

            events[1] = new CJPInterface.Event();
            events[1].activity = CJPInterface.ActivityType.Depart;
            events[1].arriveTime = new DateTime(2006,05,04);
            events[1].confirmedVia = true;
            events[1].departTime = new DateTime(2006,05,02);
            events[1].geometry = null;
            events[1].interchangeTime = 11;
            events[1].links = null;
            events[1].realTime = null;
            events[1].stop = null;

            Event[] newEvents = 
                OpenJourneyPlannerAssembler.CreateEventsDT(events);

            Assert.IsNotNull(newEvents);

            Assert.AreEqual(newEvents.Length,2);
            Assert.AreEqual(newEvents[0].Activity, ActivityType.Arrive);
            Assert.AreEqual(newEvents[0].ArriveTime, new DateTime(2006,04,04) );
            Assert.AreEqual(newEvents[0].ArriveTimeSpecified, true);
            Assert.AreEqual(newEvents[0].ConfirmedVia, true);
            Assert.AreEqual(newEvents[0].DepartTime, DateTime.MinValue);
            Assert.AreEqual(newEvents[0].DepartTimeSpecified, false);
            Assert.AreEqual(newEvents[0].Geometry, null);
            Assert.AreEqual(newEvents[0].Stop, null);

            Assert.AreEqual(newEvents[1].Activity, ActivityType.Depart);
            Assert.AreEqual(newEvents[1].ArriveTime, DateTime.MinValue);
            Assert.AreEqual(newEvents[1].ArriveTimeSpecified, false);
            Assert.AreEqual(newEvents[1].ConfirmedVia, true);
            Assert.AreEqual(newEvents[1].DepartTime, new DateTime(2006,05,02));
            Assert.AreEqual(newEvents[1].DepartTimeSpecified, true);
            Assert.AreEqual(newEvents[1].Geometry, null);
            Assert.AreEqual(newEvents[1].Stop, null);

        }

        #endregion CallCreateEventsDT Tests

        #region CreateCoordinatesDT Tests

        /// <summary>
        /// Test to ensure CreateCoordinatesDT initalises an array of data transfer objects correctly
        /// </summary>
        [Test]
        public void CallCreateCoordinatesDT()
        {

            CJPInterface.Coordinate[] coordinates = new CJPInterface.Coordinate[2];

            coordinates[0] = new CJPInterface.Coordinate();
            coordinates[0].easting = 1234;
            coordinates[0].northing = 5678;

            coordinates[1] = new CJPInterface.Coordinate();
            coordinates[1].easting = 1111;
            coordinates[1].northing = 2222;

            Coordinate[] returnCoordinates = 
                OpenJourneyPlannerAssembler.CreateCoordinatesDT(coordinates);

            Assert.IsNotNull(returnCoordinates);

            Assert.AreEqual(returnCoordinates.Length,2);
            Assert.AreEqual(returnCoordinates[0].Easting, 1234);
            Assert.AreEqual(returnCoordinates[0].Northing, 5678);
            Assert.AreEqual(returnCoordinates[1].Easting, 1111);
            Assert.AreEqual(returnCoordinates[1].Northing, 2222);

        }

        #endregion CreateCoordinatesDT Tests

        #region CreateWindowOfOpportunityDT Tests

        /// <summary>
        /// Test to ensure CreateWindowOfOpportunityDT initalises data transfer object correctly
        /// </summary>
        [Test]
        public void CallCreateWindowOfOpportunityDT()
        {

            CJPInterface.WindowOfOpportunity windowOfOpportunity = new CJPInterface.WindowOfOpportunity();

            windowOfOpportunity.start = new DateTime(2006,4,4);
            windowOfOpportunity.end = new DateTime(2006,4,5);

            WindowOfOpportunity returnWindowOfOpportunity = 
                OpenJourneyPlannerAssembler.CreateWindowOfOpportunityDT(windowOfOpportunity);

            Assert.IsNotNull(returnWindowOfOpportunity);

            Assert.AreEqual(returnWindowOfOpportunity.Start,new DateTime(2006,4,4));
            Assert.AreEqual(returnWindowOfOpportunity.End,new DateTime(2006,4,5));

        }

        #endregion CreateWindowOfOpportunityDT Tests

        #region CallCreateStopDT Tests

        /// <summary>
        /// Test to ensure CreateStopDT initalises data transfer object correctly
        /// </summary>
        [Test]
        public void CallCreateStopDT()
        {

            CJPInterface.Stop stop = new CJPInterface.Stop();

            stop.bay = "123";
            stop.coordinate = null;
            stop.logicalStop = "abc";
            stop.name = "xyz";
            stop.NaPTANID = "AB1234";
            stop.timingPoint = true;

            Stop returnStop = 
                OpenJourneyPlannerAssembler.CreateStopDT(stop);

            Assert.IsNotNull(returnStop);

            Assert.AreEqual(returnStop.Bay,"123");
            Assert.AreEqual(returnStop.Coordinate,null);
            Assert.AreEqual(returnStop.Name,"xyz");
            Assert.AreEqual(returnStop.TimingPoint,true);
            Assert.AreEqual(returnStop.NaPTANID,"AB1234");

        }

        #endregion CallCreateStopDT Tests

        #region CallCreateServicesDT Tests

        /// <summary>
        /// Test to ensure CallCreateServicesDT initalises an array of data transfer objects correctly
        /// </summary>
        [Test]
        public void CallCallCreateServicesDT()
        {

            CJPInterface.Service[] services = new CJPInterface.Service[2];

            services[0] = new CJPInterface.Service();
            services[0].daysOfOperation = new CJPInterface.Days[2] {CJPInterface.Days.Monday,CJPInterface.Days.Friday};
            services[0].destinationBoard = "abc1";
            services[0].direction = "north";
            services[0].expiryDate = new DateTime(2006,4,4);
            services[0].firstDateOfOperation = new DateTime(2006,3,4);
            services[0].openEnded = true;
            services[0].operatorCode = "xyz1";
            services[0].operatorName = "oper1";
            services[0].privateID = "id1";
            services[0].retailId = "retailId1";
            services[0].serviceNumber = "svc1";

            CJPInterface.TimetableLink timetableLink = new CJPInterface.TimetableLink();
            timetableLink.URL = "url1";
            services[0].timetableLink = timetableLink;

            services[1] = new CJPInterface.Service();
            services[1].daysOfOperation = null;
            services[1].destinationBoard = "abc2";
            services[1].direction = "south";
            services[1].expiryDate = new DateTime(2006,5,4);
            services[1].firstDateOfOperation = new DateTime(2006,2,4);
            services[1].openEnded = true;
            services[1].operatorCode = "xyz2";
            services[1].operatorName = "oper2";
            services[1].privateID = "id2";
            services[1].retailId = "retailId2";
            services[1].serviceNumber = "svc2";
            services[1].timetableLink = null;

            Service[] returnServices =
                OpenJourneyPlannerAssembler.CreateServicesDT(services);

            Assert.IsNotNull(returnServices);

            Assert.AreEqual(returnServices.Length,2);

            Assert.IsNotNull(returnServices[0].DaysOfOperation);
            Assert.AreEqual(returnServices[0].DaysOfOperation.Length,2);
            Assert.AreEqual(returnServices[0].DaysOfOperation[0],Days.Monday);
            Assert.AreEqual(returnServices[0].DaysOfOperation[1],Days.Friday);
            Assert.AreEqual(returnServices[0].DestinationBoard,"abc1");
            Assert.AreEqual(returnServices[0].ExpiryDate,new DateTime(2006,4,4));
            Assert.AreEqual(returnServices[0].FirstDateOfOperation,new DateTime(2006,3,4));
            Assert.AreEqual(returnServices[0].OpenEnded,true);
            Assert.AreEqual(returnServices[0].OperatorCode,"xyz1");
            Assert.AreEqual(returnServices[0].OperatorName,"oper1");
            Assert.AreEqual(returnServices[0].PrivateID,"id1");
            Assert.AreEqual(returnServices[0].RetailId,"retailId1");
            Assert.AreEqual(returnServices[0].ServiceNumber,"svc1");
            Assert.AreEqual(returnServices[0].TimetableLinkUrl,"url1");

            Assert.AreEqual(returnServices[1].DaysOfOperation,null);
            Assert.AreEqual(returnServices[1].DestinationBoard,"abc2");
            Assert.AreEqual(returnServices[1].ExpiryDate,new DateTime(2006,5,4));
            Assert.AreEqual(returnServices[1].FirstDateOfOperation,new DateTime(2006,2,4));
            Assert.AreEqual(returnServices[1].OpenEnded,true);
            Assert.AreEqual(returnServices[1].OperatorCode,"xyz2");
            Assert.AreEqual(returnServices[1].OperatorName,"oper2");
            Assert.AreEqual(returnServices[1].PrivateID,"id2");
            Assert.AreEqual(returnServices[1].RetailId,"retailId2");
            Assert.AreEqual(returnServices[1].ServiceNumber,"svc2");
            Assert.AreEqual(returnServices[1].TimetableLinkUrl,null);

        }

        #endregion CallCreateServicesDT Tests

        #region Miscellaneous Tests

        /// <summary>
        /// Use to create template XML request file for exposed services test tool
        /// </summary>
//        [Test]
//        public void createXMLRequestTemplate() 
//        {
//
//            Coordinate coordinate1 = new Coordinate();
//            coordinate1.Easting = 1234;
//            coordinate1.Northing = 5678;
//
//            Coordinate coordinate2 = new Coordinate();
//            coordinate1.Easting = 1234;
//            coordinate1.Northing = 5678;
//
//
//            RequestStop[] requestStops1 = new RequestStop[1];
//            requestStops1[0] = new RequestStop();
//            requestStops1[0].TimeDate = new DateTime(2006,4,29,10,00,0);
//            requestStops1[0].NaPTANID = "9100KNGX";
//            requestStops1[0].Coordinate = coordinate1;
//
//            RequestStop[] requestStops2 = new RequestStop[1];
//            requestStops2[0] = new RequestStop();
//            requestStops2[0].TimeDate = new DateTime(2006,5,2,15,00,0);
//            requestStops2[0].NaPTANID = "9100NWCSTLE";
//            requestStops2[0].Coordinate = coordinate2;
//
//
//            RequestPlace requestPlace1 = new RequestPlace();
//            requestPlace1.Type = RequestPlaceType.NaPTAN;
//            requestPlace1.GivenName = "Kings Cross";
//            requestPlace1.Locality = "E0034577";
//            requestPlace1.Stops = requestStops1;
//            requestPlace1.Coordinate = coordinate1;
//
//            RequestPlace requestPlace2 = new RequestPlace();
//            requestPlace2.Type = RequestPlaceType.NaPTAN;
//            requestPlace2.GivenName = "Newcastle";
//            requestPlace2.Locality = "E0057900";
//            requestPlace2.Stops = requestStops2;
//            requestPlace2.Coordinate = coordinate2;
//
//            TransportModes transportModes = new TransportModes();
//            transportModes.Include = true;
//            transportModes.Modes = new TransportMode[2];
//            transportModes.Modes[0] = new TransportMode();
//            transportModes.Modes[1] = new TransportMode();
//            transportModes.Modes[0].Mode = TransportModeType.Coach;
//            transportModes.Modes[1].Mode = TransportModeType.Rail;
//
//            PublicParameters publicParameters = new PublicParameters();
//
//            publicParameters.Algorithm = PublicAlgorithmType.Fastest;
//            publicParameters.ExtraCheckInTime = DateTime.MinValue;
//            publicParameters.ExtraCheckInTimeSpecified = false;
//            publicParameters.InterchangeSpeed = 1;
//            publicParameters.IntermediateStops = IntermediateStopsType.All;
//            publicParameters.Interval = DateTime.MinValue + new TimeSpan(10,0,0); // 10hr time span
//            publicParameters.IntervalSpecified = true;
//            publicParameters.MaxWalkDistance = 100;
//            publicParameters.NotVias = null;
//            publicParameters.RangeType = RangeType.Sequence;
//            publicParameters.Sequence = 3;
//            publicParameters.SequenceSpecified = true;
//            publicParameters.SoftVias = null;
//            publicParameters.TrunkPlan = false;
//            publicParameters.Vias = null;
//            publicParameters.WalkSpeed = 80;
//
//            Request request = new Request();
//            request.Depart = true;
//            request.Destination = requestPlace1;
//            request.ModeFilter = transportModes;
//            request.OperatorFilter = null;
//            request.Origin = requestPlace2;
//            request.PublicParameters = publicParameters;
//            request.ServiceFilter = null;
//
//            XmlSerializer xs = new XmlSerializer(typeof(Request));
//				
//            FileStream fileStream =
//                new FileStream(@"C:\TDPortal\CodeBase\TransportDirect\Stubs\ExposedServicesTestTool\ExposedServicesTestToolClient\Requests\OpenJourneyPlanner\V1\template.xml",
//                        FileMode.Create, FileAccess.Write, FileShare.Read);
//				
//            xs.Serialize(fileStream,request);
//
//            fileStream.Close();
//
//        }

        /// <summary>
        /// Test to ensure all create methods return null if supplied with null parameter
        /// </summary>
        [Test]
        public void CallCreateWithNull()
        {

            // only call those methods where null case has not already been tested by other tests
            Assert.IsNull(OpenJourneyPlannerAssembler.CreateJourneyRequest(null,"en-gb","1234"));
            Assert.IsNull(OpenJourneyPlannerAssembler.CreateRequestStop(null));
            Assert.IsNull(OpenJourneyPlannerAssembler.CreateCoordinateDT(null));
            Assert.IsNull(OpenJourneyPlannerAssembler.CreateResultDT(null));

        }

        #endregion Miscellaneous Tests
    
        #region helper methods

        /// <summary>
        /// Returns number of occurrences that search item occurs in modeArray
        /// </summary>
        /// <param name="modeArray">Source array</param>
        /// <param name="searchItem">Search item</param>
        /// <returns>number of occurrences that search item occurs in modeArray</returns>
        private int modeCount(CJPInterface.Mode[] modeArray, CJPInterface.ModeType searchItem) 
        {
            int count=0;
            foreach(CJPInterface.Mode mode in modeArray) 
            {
                if (mode.mode == searchItem) count++;
            }
            return count;
        }

        /// <summary>
        /// Returns true if all the modes in actualModes occur in expectedModes array, false otherwise
        /// </summary>
        /// <param name="actualModes">Actual modes</param>
        /// <param name="expectedModes">Expected modes</param>
        /// <returns>true if all the modes in actualModes occur in expectedModes array, false otherwise</returns>
        private bool checkModes(CJPInterface.Mode[] actualModes, CJPInterface.ModeType[] expectedModes) 
        {
            foreach (CJPInterface.ModeType mode in expectedModes) 
            {
                if (modeCount(actualModes,mode) != 1) 
                {
                    return false;
                }
            }
            return true;
        }

        #endregion helper methods
		
	}
}