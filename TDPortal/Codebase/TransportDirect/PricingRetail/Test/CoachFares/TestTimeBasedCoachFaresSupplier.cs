//********************************************************************************
//NAME         : TestTimeBasedCoachFaresSupplier.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 22/10/2005
//DESCRIPTION  : Implementation of TestTimeBasedCoachFaresSupplier class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestTimeBasedCoachFaresSupplier.cs-arc  $
//
//   Rev 1.1   Feb 02 2009 16:56:22   mmodi
//Corrected nunit errors
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:37:24   mturner
//Initial revision.
//
//   Rev 1.8   Oct 16 2007 13:53:52   mmodi
//Amended to pass a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.7   Dec 01 2005 09:50:46   mguney
//MockFareInterface is replaced with TestMockFareInterface in the related test classes and failing tests fixed.
//Resolution for 3267: Mock classes should be prefixed with Test.
//
//   Rev 1.6   Nov 29 2005 14:54:18   mguney
//Changes done according to the changes done on the MockFareInterface.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.5   Nov 02 2005 16:42:04   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Nov 01 2005 17:24:54   mguney
//Constant names changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 31 2005 13:59:02   mguney
//Class name changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 29 2005 16:52:54   mguney
//TimeBasedFareSupplier factory is initialised in the Setup.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 28 2005 14:53:52   mguney
//PricingUnit.Price test methods removed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 22 2005 16:05:28   mguney
//Initial revision.


using System;
using NUnit.Framework;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;
using CJP = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Test harness for TimeBasedCoachFaresSupplier
	/// </summary>
	[TestFixture]
	public class TestTimeBasedCoachFaresSupplier
	{				

		#region Constructor and Initialisation

		
		[SetUp]
		public void Init()
		{		
			// Initialise property service etc.
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestPricingRetailInitialisation());
			TDServiceDiscovery.Current.SetServiceForTest(ServiceDiscoveryKey.TimeBasedFareSupplier,new TimeBasedFareSupplierFactory());								
		}

		[TearDown]
		public void CleanUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}
		
		#endregion

		

		#region Test methods

		[Test]
		public void TestPricePricingUnit()
		{
			Ticket[] singleTickets;
			Ticket[] returnTickets;
			PricingUnit pricingUnit;
			Discounts discounts;
			//get the fares interface from the factory
			ITimeBasedFareSupplierFactory factory = (ITimeBasedFareSupplierFactory)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.TimeBasedFareSupplier];
			ITimeBasedFareSupplier supplier = factory.GetSupplier(CJP.ModeType.Coach);	
						
			//***********************TEST WITH MONTHLY DISCOUNTS
			//Apply PricePricingUnit
			discounts = new Discounts(string.Empty,TestMockFareInterface.DISCOUNTCARDTYPENATEXCOACHMONTHLY, 
				TicketClass.All);			
			pricingUnit = new PricingUnit(TestSampleJourneyData.NatExPJDDovVic);
			supplier.PricePricingUnit(pricingUnit,discounts, null, string.Empty);			
			//Transfer tickets from ArrayList to array of tickets
			singleTickets = new Ticket[pricingUnit.SingleFares.Tickets.Count];
			((ArrayList)(pricingUnit.SingleFares.Tickets)).CopyTo(singleTickets);
			returnTickets = new Ticket[pricingUnit.ReturnFares.Tickets.Count];
			((ArrayList)(pricingUnit.ReturnFares.Tickets)).CopyTo(returnTickets);
			//TEST
			Assert.AreEqual(1,singleTickets.Length,"Incorrect single ticket count.");			
			Assert.AreEqual(1,returnTickets.Length,"Incorrect return ticket count.");			
			
			Assert.AreEqual(2.60, Decimal.Round(Convert.ToDecimal(singleTickets[0].AdultFare),2),           "Incorrect single undiscounted adult fare.");
			Assert.AreEqual(2.40, Decimal.Round(Convert.ToDecimal(singleTickets[0].DiscountedAdultFare),2), "Incorrect single monthly discounted adult fare.");
			Assert.AreEqual(0.90, Decimal.Round(Convert.ToDecimal(singleTickets[0].ChildFare),2),           "Incorrect single undiscounted child fare.");
			Assert.AreEqual(0.70, Decimal.Round(Convert.ToDecimal(singleTickets[0].DiscountedChildFare),2), "Incorrect single monthly discounted child fare.");

			Assert.AreEqual(5.85, Decimal.Round(Convert.ToDecimal(returnTickets[0].AdultFare),2),           "Incorrect return undiscounted adult fare.");
			Assert.AreEqual(5.70, Decimal.Round(Convert.ToDecimal(returnTickets[0].DiscountedAdultFare),2), "Incorrect return monthly discounted adult fare.");
			Assert.AreEqual(4.50, Decimal.Round(Convert.ToDecimal(returnTickets[0].ChildFare),2),           "Incorrect return undiscounted child fare.");
			Assert.AreEqual(4.30, Decimal.Round(Convert.ToDecimal(returnTickets[0].DiscountedChildFare),2), "Incorrect return monthly discounted child fare.");

		}

		#endregion

	}
}
