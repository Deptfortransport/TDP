//********************************************************************************
//NAME         : TestPricingRequestBuilder.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 19/10/2003
//DESCRIPTION  : Implementation of TestPricingRequestBuilder class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/RBOGateway/TestPricingRequestBuilder.cs-arc  $
//
//   Rev 1.1   Apr 09 2010 10:32:02   mmodi
//Updated following change to AddJourneyLeg method
//Resolution for 5500: Fares - RF 019 fares for journeys involving rail replacement bus stops
//
//   Rev 1.0   Nov 08 2007 12:37:34   mturner
//Initial revision.
//
//   Rev 1.6   Aug 19 2005 14:06:30   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.5.1.0   Aug 16 2005 11:20:52   RPhilpott
//Get rid of warnings from deprecated methods.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.5   Apr 17 2005 18:16:58   RPhilpott
//Del 7 unit testing.
//
//   Rev 1.4   Feb 07 2005 16:37:48   RScott
//Assertion changed to Assert
//
//   Rev 1.3   Dec 16 2003 17:24:08   COwczarek
//Add test to verify a walking leg is excluded from the creation of a pricing request object
//Resolution for 376: Pricing of train-walk-train journeys
//
//   Rev 1.2   Oct 22 2003 12:00:20   acaunt
//Updated test for checking railcard
//
//   Rev 1.1   Oct 20 2003 16:47:30   acaunt
//Implemented tests
//
//   Rev 1.0   Oct 20 2003 10:19:06   acaunt
//Initial Revision
using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.AdditionalDataModule;

namespace TransportDirect.UserPortal.PricingRetail.RBOGateway
{
	/// <summary>
	/// Test harness for PricingRequestBuilder
	/// </summary>
	[TestFixture]
	public class TestPricingRequestBuilder
	{
		private IAdditionalData additionalData;

		private static string RAIL_DISCOUNT = "";
		private const string NO_RAILCARD = "   ";
		private static TransportDirect.UserPortal.PricingRetail.Domain. TicketClass TICKET_CLASS =  TransportDirect.UserPortal.PricingRetail.Domain.TicketClass.All;

		PricingRequestBuilder builder;

		public TestPricingRequestBuilder()
		{
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());
			additionalData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];
		}

		[SetUp]
		public void Init()
		{		
			// The outline of a request
			builder = new PricingRequestBuilder();
			Discounts discounts = new Discounts(RAIL_DISCOUNT, "", TICKET_CLASS);
			builder.CreatePricingRequest(discounts);
		}

		[TearDown]
		public void CleanUp()
		{
			builder = null;
		}

		/// <summary>
		///
		/// </summary>
		[Test]
		public void TestCreateRequest()
		{
			// Check our outline request
			PricingRequestDto request = builder.GetPricingRequest();
			Assert.AreEqual(NO_RAILCARD, request.Railcard, "Railcard not correctly mapped"); // railcard should be mapped to "   "
			Assert.AreEqual(9, request.TicketClass, "Class not correctly mapped");
			Assert.IsNull(request.OutwardDate, "Outward date not set to null");
			Assert.IsNull(request.ReturnDate, "Return date not set to null");
			Assert.AreEqual(1, request.NumberOfAdults, "Incorrect number of adults");
			Assert.AreEqual(1, request.NumberOfChildren, "Incorrect number of children");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestAddTrainLegCheckTrain()
		{
			// Add a outward leg
			builder.AddJourneyLeg(TestSampleJourneyData.TrainPJDDovVic, ReturnIndicator.Outbound, false);
			PricingRequestDto request = builder.GetPricingRequest();
			// Confirm the number of legs
			Assert.AreEqual(1, request.Trains.Count, "Incorrect number of train legs");
			// Confirm the outward and return dates
			Assert.IsNotNull(request.OutwardDate, "Outward date not set");
			Assert.AreEqual(TestSampleJourneyData.TrainPJDDovVic.LegStart.DepartureDateTime, request.OutwardDate, "Outward date not correct");
			Assert.IsNull(request.ReturnDate, "Return date is set. This is incorrect");
			// Check the details of the TrainDTO
			TrainDto train1 = (TrainDto)request.Trains[0];
			Assert.AreEqual(TestSampleJourneyData.TrainPJDDovVic.Services[0].PrivateId, train1.Uid, "Train uid incorrectly set");
			Assert.AreEqual(" ", train1.TrainClass, "Train class incorrectly set");
			Assert.AreEqual(" ", train1.Sleeper, "Sleeper code incorrectly set");
			Assert.AreEqual(ReturnIndicator.Outbound, train1.ReturnIndicator, "ReturnIndicator incorrectly set");
			Assert.AreEqual(TestSampleJourneyData.TrainPJDDovVic.Services[0].OperatorCode, train1.Tocs[0].Code, "Train Toc incorrectly set");
			Assert.AreEqual(TestSampleJourneyData.TrainPJDDovVic.GetIntermediatesLeg().Length, train1.IntermediateStops.Length, "Number of intermediate locations incorrectly set");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestAddTrainLegCheckStops()
		{
			// Add a outward leg
			builder.AddJourneyLeg(TestSampleJourneyData.TrainPJDDovVic, ReturnIndicator.Outbound, false);
			PricingRequestDto request = builder.GetPricingRequest();
			// Confirm the number of legs
			Assert.AreEqual(1, request.Trains.Count, "Incorrect number of train legs");
			// Check the details of the Stops and their locations
			StopDto board = ((TrainDto)request.Trains[0]).Board;
			StopDto alight = ((TrainDto)request.Trains[0]).Alight;
			StopDto destination = ((TrainDto)request.Trains[0]).Destination;
			// The boarding point (note, only the first 4 digits of the nlc code are maintained)	
			Assert.AreEqual(TestSampleJourneyData.TrainPJDDovVic.LegStart.DepartureDateTime, board.Departure, "Boarding departure time incorrectly set");
			Assert.AreEqual(getCRS( TestSampleJourneyData.TrainPJDDovVic.LegStart.Location.NaPTANs[0].Naptan),board.Location.Crs, "Boarding crs wrong");
			Assert.AreEqual(getNLC( TestSampleJourneyData.TrainPJDDovVic.LegStart.Location.NaPTANs[0].Naptan).Substring(0,4),board.Location.Nlc, "Boarding nlc wrong");
			// The alighting point (note, only the first 4 digits of the nlc code are maintained)	
			Assert.AreEqual(TestSampleJourneyData.TrainPJDDovVic.LegEnd.ArrivalDateTime, alight.Arrival, "Alighting arrival time incorrectly set");
			Assert.AreEqual(getCRS( TestSampleJourneyData.TrainPJDDovVic.LegEnd.Location.NaPTANs[0].Naptan),alight.Location.Crs,"Alighting crs wrong");
			Assert.AreEqual(getNLC( TestSampleJourneyData.TrainPJDDovVic.LegEnd.Location.NaPTANs[0].Naptan).Substring(0,4),alight.Location.Nlc, "Alighting nlc wrong");
			// The destination (note, only the first 4 digits of the nlc code are maintained)	
			Assert.AreEqual(TestSampleJourneyData.TrainPJDDovVic.Destination.ArrivalDateTime, destination.Arrival, "Destination arrival time incorrectly set");
			Assert.AreEqual(getCRS( TestSampleJourneyData.TrainPJDDovVic.Destination.Location.NaPTANs[0].Naptan),destination.Location.Crs, "Destination crs wrong");
			Assert.AreEqual(getNLC( TestSampleJourneyData.TrainPJDDovVic.Destination.Location.NaPTANs[0].Naptan).Substring(0,4),destination.Location.Nlc, "Destination nlc wrong");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestAddExtraTrainLeg()
		{
			// Add two outward train legs
			builder.AddJourneyLeg(TestSampleJourneyData.TrainPJDDovVic, ReturnIndicator.Outbound, false);
			builder.AddJourneyLeg(TestSampleJourneyData.TrainPJDStpNot, ReturnIndicator.Outbound, false);
			PricingRequestDto request = builder.GetPricingRequest();
			// Confirm the number of legs
			Assert.AreEqual(2, request.Trains.Count,"Incorrect number of train legs");
			// Confirm that the depart date hasn't been changed by adding the 2nd leg
			Assert.AreEqual(TestSampleJourneyData.TrainPJDDovVic.LegStart.DepartureDateTime, request.OutwardDate, "Outward date not correct");
			// Confirm that the arrival point of the 1st train and the departure point of the 2nd are the same stop
			StopDto board = ((TrainDto)request.Trains[1]).Board;
			StopDto alight = ((TrainDto)request.Trains[0]).Alight;
			Assert.AreEqual(alight, board, "Change over point not represented by the same StopDto");
			// Check the arrival and departure times at the stop
			Assert.AreEqual(TestSampleJourneyData.TrainPJDDovVic.LegEnd.ArrivalDateTime, alight.Arrival, "Change over point arrival time incorrect");
			Assert.AreEqual(TestSampleJourneyData.TrainPJDStpNot.LegStart.DepartureDateTime, board.Departure, "Change over point departure time incorrect");
		}

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestAddUndergroundThenTrainLeg()
		{
			// Add first train leg and then the underground
			builder.AddJourneyLeg(TestSampleJourneyData.TrainPJDDovVic, ReturnIndicator.Outbound, false);
			builder.AddJourneyLeg(TestSampleJourneyData.UnderPJDVicStp, ReturnIndicator.Outbound, false);
			PricingRequestDto request = builder.GetPricingRequest();
			// Confirm the number of legs (should be 1 only for the train)
			Assert.AreEqual(1, request.Trains.Count, "Incorrect number of train legs");
			// Add the second train leg
			builder.AddJourneyLeg(TestSampleJourneyData.TrainPJDStpNot, ReturnIndicator.Outbound, false);
			request = builder.GetPricingRequest();
			// Confirm the number of legs (should be 2 now)
			Assert.AreEqual(2, request.Trains.Count, "Incorrect number of train legs");
			// Confirm that the arrival point of the 1st train and the departure point of the 2nd are the now different stops
			StopDto board = ((TrainDto)request.Trains[1]).Board;
			StopDto alight = ((TrainDto)request.Trains[0]).Alight;
			Assert.IsTrue(!(board == alight),"Change over points have been incorrectly merged");
		}

        /// <summary>
        /// Test that a walking leg is excluded from a pricing request for a
        /// train -> walk -> train journey
        /// </summary>
        [Test]
        public void TestAddWalkThenTrainLeg()
        {
            // Add first train leg and then the walking leg
            builder.AddJourneyLeg(TestSampleJourneyData.TrainPJDDovVic, ReturnIndicator.Outbound, false);
            builder.AddJourneyLeg(TestSampleJourneyData.WalkPJDNot, ReturnIndicator.Outbound, false);
            PricingRequestDto request = builder.GetPricingRequest();
            // Confirm the number of legs (should be 1 only for the train)
            Assert.AreEqual(1, request.Trains.Count, "Incorrect number of train legs");
            // Add the second train leg
            builder.AddJourneyLeg(TestSampleJourneyData.TrainPJDStpNot, ReturnIndicator.Outbound, false);
            request = builder.GetPricingRequest();
            // Confirm the number of legs (should be 2 now)
            Assert.AreEqual(2, request.Trains.Count, "Incorrect number of train legs");
            // Confirm that the arrival point of the 1st train and the departure point of the 2nd are the now different stops
            StopDto board = ((TrainDto)request.Trains[1]).Board;
            StopDto alight = ((TrainDto)request.Trains[0]).Alight;
            Assert.IsTrue(!(board == alight),"Change over points have been incorrectly merged");
        }

		/// <summary>
		/// 
		/// </summary>
		[Test]
		public void TestAddOutwardAndReturnLeg()
		{
			// Add outward and return legs
			builder.AddJourneyLeg(TestSampleJourneyData.TrainPJDDovVic, ReturnIndicator.Outbound, false);
			builder.AddJourneyLeg(TestSampleJourneyData.TrainPJDVicDov, ReturnIndicator.Return, false);
			PricingRequestDto request = builder.GetPricingRequest();
			// Confirm the number of legs
			Assert.AreEqual(2, request.Trains.Count, "Incorrect number of train legs");
			// Check that the outward and return dates are correctly set
			Assert.AreEqual(TestSampleJourneyData.TrainPJDDovVic.LegStart.DepartureDateTime, request.OutwardDate, "Outward date not correct");
			Assert.AreEqual(TestSampleJourneyData.TrainPJDVicDov.LegStart.DepartureDateTime, request.ReturnDate, "Return date not correct");
		}

		private string getCRS(string naptan)
		{
			return additionalData.LookupFromNaPTAN(LookupType.CRS_Code, naptan);
		}

		private string getNLC(string naptan)
		{
			return additionalData.LookupFromNaPTAN(LookupType.NLC_Code, naptan);
		}
	}
}
