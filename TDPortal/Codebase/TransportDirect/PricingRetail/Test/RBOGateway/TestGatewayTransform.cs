//********************************************************************************
//NAME         : TestGatewayTransform.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 15/10/2003
//DESCRIPTION  : Implementation of  TestGatewayTransform class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/RBOGateway/TestGatewayTransform.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:34   mturner
//Initial revision.
//
//   Rev 1.4   May 23 2005 14:19:18   rscott
//Updated to fix NUnit Tests
//
//   Rev 1.3   Apr 21 2005 18:36:18   RPhilpott
//Testing.
//
//   Rev 1.2   Mar 22 2005 16:08:58   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.1   Feb 07 2005 16:37:48   RScott
//Assertion changed to Assert
//
//   Rev 1.0   Oct 20 2003 21:41:00   acaunt
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
	/// Test harness for Gateway transform
	/// </summary>
	[TestFixture]
	public class TestGatewayTransform : GatewayTransform
	{
		private IAdditionalData additionalData;
		
		private GatewayTransform transform;
		private static string RAIL_DISCOUNT = "NET";
		private static TransportDirect.UserPortal.PricingRetail.Domain. TicketClass TICKET_CLASS =  TransportDirect.UserPortal.PricingRetail.Domain.TicketClass.All;

		public TestGatewayTransform()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());
			additionalData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];
		}

		[SetUp]
		public void Init()
		{		
			transform = new GatewayTransform();
		}

		[TearDown]
		public void CleanUp()
		{
			transform = null;
		}

		/// <summary>
		///
		/// </summary>
		[Test]
		public void TestMapRequestSingleJourney()
		{
			// Map a return journey
			Discounts discounts = new Discounts(RAIL_DISCOUNT, "", TICKET_CLASS);
			Itinerary itinerary = new Itinerary(TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.TrainNotDov);
			// Use the return portion to calculate for a single journey
			PricingRequestDto request = transform.MapPriceUnitRequest((PricingUnit)itinerary.ReturnUnits[0], discounts);
			// Confirm that we have the correct number of trainDtos
			Assert.AreEqual(2, request.Trains.Count, "Incorrect number of trainDtos in the request");
		}

		/// <summary>
		///
		/// </summary>
		[Test]
		public void TestMapRequestReturnJourney()
		{
			// Map a return journey
			Discounts discounts = new Discounts(RAIL_DISCOUNT, "", TICKET_CLASS);
			Itinerary itinerary = new Itinerary(TestSampleJourneyData.TrainDovNot, TestSampleJourneyData.TrainNotDov);
			// Use the outward portion to calculate for a return journey
			PricingRequestDto request = transform.MapPriceUnitRequest((PricingUnit)itinerary.OutwardUnits[0], discounts);
			// Confirm that we have the correct number of trainDtos
			Assert.AreEqual(4, request.Trains.Count,"Incorrect number of trainDtos in the request");
		}


		///
		/// </summary>
		[Test]
		public void TestMapResponse()
		{
			// Map a PricingResultDto
			PricingResult[] results = transform.MapPriceUnitResponse(TestSampleJourneyData.PricingResultDto);
			PricingResult singles = results[0];
			PricingResult returns = results[1];
			// Confirm that we have the correct number of single and return tickets
			Assert.AreEqual(1, singles.Tickets.Count,"Incorrect number of single tickets");
			Assert.AreEqual(2, returns.Tickets.Count,"Incorrect number of return tickets");
		}
	}
}
