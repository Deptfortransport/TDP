//********************************************************************************
//NAME         : TestCoachFare.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Test class for TestCoachFare. Tests CoachFareForJourney and CoachFareForRoute.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFareInterfaces/TestCoachFare.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:16   mturner
//Initial revision.
//
//   Rev 1.7   Dec 06 2005 11:27:54   mguney
//AvailabilityScore changed to Probability.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.6   Oct 31 2005 11:44:34   mguney
//Reference for CJPInterface is changed to FaresProviderInterface.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Oct 25 2005 15:03:24   mguney
//ChildAgeRange check changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Oct 25 2005 11:13:02   mguney
//Initialisation changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 21 2005 10:18:26   mguney
//Flexibility test changed in TestCoachFareForJourneyConstructor.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 15:04:46   mguney
//Creation dare corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 12:26:46   mguney
//SCR associated
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 12:25:08   mguney
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.FaresProviderInterface;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.Domain;


namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Tests CoachFareForJourney and CoachFareForRoute.
	/// </summary>
	[TestFixture]
	public class TestCoachFare
	{
		public TestCoachFare()
		{
			
		}

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestCoachFaresInterfaceInitialisation());					   			
		}

		[TearDown]
		public void CleanUp()
		{
			
		}

		[Test]
		public void TestCoachFareForJourneyConstructor()
		{
			Fare fare = new Fare();
			fare.additionalFees = 10;
			fare.additionalFeesDescription = "test";
			fare.adult = true;
			fare.bookingFee = 20;
			fare.childAgeRange = "0-5";
			fare.discountCardType = "natex";
			fare.fare = 583;
			fare.fareRestrictionType = FareType.LimitedFlexibility;
			fare.fareType = "standard";
			fare.fareTypeConditions = "food inc";
			fare.passengerType = "economy";
			fare.passengerTypeConditions = "good";
			fare.single = true;
			fare.taxes = 12;
			
			CoachFareForJourney coachFare = new CoachFareForJourney(fare, new TDNaptan("12356",null),
				new TDNaptan("45698",null));
			//Compare all the values.
			Assert.AreEqual(fare.additionalFees,coachFare.AdditionalFees,
				"CJPInterface.Fare.additionalFees and CoachFareForJourney. are not equal.");
			Assert.AreEqual(fare.additionalFeesDescription,coachFare.AdditionalFeesDescription,
				"CJPInterface.Fare.additionalFeesDescription and CoachFareForJourney.AdditionalFeesDescription are not equal.");
			Assert.AreEqual(fare.adult,coachFare.IsAdult,
				"CJPInterface.Fare.adult and CoachFareForJourney.IsAdult are not equal.");
			Assert.AreEqual(fare.single,coachFare.IsSingle,
				"CJPInterface.Fare.single and CoachFareForJourney.IsSingle are not equal.");
			Assert.AreEqual(fare.bookingFee,coachFare.BookingFee,
				"CJPInterface.Fare.bookingFee and CoachFareForJourney.BookingFee are not equal.");
			Assert.AreEqual(0,coachFare.ChildAgeMin,
				"CJPInterface.Fare.childAgeRange and CoachFareForJourney.ChildAgeRange are not equal.");
			Assert.AreEqual(5,coachFare.ChildAgeMax,
				"CJPInterface.Fare.childAgeRange and CoachFareForJourney.ChildAgeRange are not equal.");
			Assert.AreEqual(fare.discountCardType,coachFare.DiscountCardType,
				"CJPInterface.Fare.discountCardType and CoachFareForJourney.DiscountCardType are not equal.");
			Assert.AreEqual(fare.fare,coachFare.Fare,
				"CJPInterface.Fare.fare and CoachFareForJourney.Fare are not equal.");
			
			Flexibility flexibility = Flexibility.Unknown;
			switch (fare.fareRestrictionType)
			{
				case FareType.NotFlexible:
					flexibility = Flexibility.NoFlexibility;
					break;
				case FareType.LimitedFlexibility:
					flexibility = Flexibility.LimitedFlexibility;
					break;
				case FareType.Flexible:
					flexibility = Flexibility.FullyFlexible;
					break;
			}

			Assert.AreEqual(flexibility,coachFare.Flexibility,
				"CJPInterface.Fare.fareRestrictionType and CoachFareForJourney.FareRestrictionType are not equal.");
			
			Assert.AreEqual(fare.fareType,coachFare.FareType,
				"CJPInterface.Fare.fareType and CoachFareForJourney.FareType are not equal.");
			Assert.AreEqual(fare.fareTypeConditions,coachFare.FareTypeConditions,
				"CJPInterface.Fare.fareTypeConditions and CoachFareForJourney.FareTypeConditions are not equal.");
			Assert.AreEqual(fare.passengerType,coachFare.PassengerType,
				"CJPInterface.Fare.passengerType and CoachFareForJourney.PassengerType are not equal.");
			Assert.AreEqual(fare.passengerTypeConditions,coachFare.PassengerTypeConditions,
				"CJPInterface.Fare.passengerTypeConditions and CoachFareForJourney. are not equal.");
			Assert.AreEqual(fare.taxes,coachFare.Taxes,
				"CJPInterface.Fare.taxes and CoachFareForJourney.Taxes are not equal.");
		}

		[Test]
		public void TestCoachFareForRouteConstructor()
		{
			//construct the parameter
			CoachFareRemoteForRoute fare = new CoachFareRemoteForRoute();			
			fare.AdvancePurchaseDays = 2;
			fare.Availability = Probability.Low;
			fare.ChangeNaPTANs = new string[] {"12333","56767"};
			fare.ChildAgeMax = 5;
			fare.ChildAgeMin = 0;
			fare.DayRestrictions = new DayRestriction[] {DayRestriction.Monday,DayRestriction.Thursday};
			fare.DestinationNaPTAN = "12345"; 
			fare.DiscountCardType = "natex";
			fare.Fare = 567;
			fare.FareEndDate = DateTime.Now.AddHours(2);
			fare.FareStartDate = DateTime.Now;
			fare.Flexibility = Flexibility.NoFlexibility;
			fare.IsAdult = true;
			fare.IsConcessionaryFare = true;
			fare.IsDayReturn = true;
			fare.IsSingle = true;
			fare.OriginNaPTAN = "23454";
			fare.PassengerType = "normal";
			fare.PassengerTypeConditions = "normal cond";
			fare.RestrictedOperatorCodes = new string[] {"AS","FG"};
			fare.RestrictedServices = new string[] {"asd","ddd3"};			
			fare.TimeRestrictions = new TimeRestriction[] {
				new TimeRestriction(DateTime.Now.AddHours(-2),DateTime.Now.AddHours(-1))};			
			//run the constructor
			CoachFareForRoute coachFare = new CoachFareForRoute(fare);
			//Compare all the values.
			
			Assert.AreEqual(fare.AdvancePurchaseDays,coachFare.AdvancePurchaseDays,
				"CoachFareRemoteForRoute.AdvancePurchaseDays and CoachFareForRoute.AdvancePurchaseDays are not equal.");
			Assert.IsTrue((fare.Availability == coachFare.Availability),
				"CoachFareRemoteForRoute.Availability and CoachFareForRoute.Availability are not equal.");								
			Assert.AreEqual(fare.ChildAgeMax,coachFare.ChildAgeMax,
				"CoachFareRemoteForRoute.ChildAgeMax and CoachFareForRoute.ChildAgeMax are not equal.");
			Assert.AreEqual(fare.ChildAgeMin,coachFare.ChildAgeMin,
				"CoachFareRemoteForRoute.ChildAgeMin and CoachFareForRoute.ChildAgeMin are not equal.");									
			Assert.AreEqual(fare.DestinationNaPTAN,coachFare.DestinationNaPTAN.Naptan,
				"CoachFareRemoteForRoute.DestinationNaPTAN and CoachFareForRoute.DestinationNaPTAN are not equal.");			
			Assert.AreEqual(fare.DiscountCardType,coachFare.DiscountCardType,
				"CoachFareRemoteForRoute.DiscountCardType and CoachFareForRoute.DiscountCardType are not equal.");
			Assert.AreEqual(fare.Fare,coachFare.Fare,
				"CoachFareRemoteForRoute.Fare and CoachFareForRoute.Fare are not equal.");
			
			Assert.IsTrue((fare.FareEndDate.CompareTo(coachFare.FareEndDate.GetDateTime()) == 0),
				"CoachFareRemoteForRoute.FareEndDate and CoachFareForRoute.FareEndDate are not equal.");
			Assert.IsTrue((fare.FareStartDate.CompareTo(coachFare.FareStartDate.GetDateTime()) == 0),
				"CoachFareRemoteForRoute.FareStartDate and CoachFareForRoute.FareStartDate are not equal.");
			
			Assert.IsTrue((fare.Flexibility == coachFare.Flexibility),
				"CoachFareRemoteForRoute.Flexibility and CoachFareForRoute.Flexibility are not equal.");
			Assert.AreEqual(fare.IsAdult,coachFare.IsAdult,
				"CoachFareRemoteForRoute.IsAdult and CoachFareForRoute.IsAdult are not equal.");
			Assert.AreEqual(fare.IsConcessionaryFare,coachFare.IsConcessionaryFare,
				"CoachFareRemoteForRoute.IsConcessionaryFare and CoachFareForRoute.IsConcessionaryFare are not equal.");
			Assert.AreEqual(fare.IsDayReturn,coachFare.IsDayReturn,
				"CoachFareRemoteForRoute.IsDayReturn and CoachFareForRoute.IsDayReturn are not equal.");
			Assert.AreEqual(fare.IsSingle,coachFare.IsSingle,
				"CoachFareRemoteForRoute.IsSingle and CoachFareForRoute.IsSingle are not equal.");
			Assert.AreEqual(fare.OriginNaPTAN,coachFare.OriginNaPTAN.Naptan,
				"CoachFareRemoteForRoute.OriginNaPTAN and CoachFareForRoute.OriginNaPTAN are not equal.");						
			Assert.AreEqual(fare.PassengerType,coachFare.PassengerType,
				"CoachFareRemoteForRoute.PassengerType and CoachFareForRoute.PassengerType are not equal.");
			Assert.AreEqual(fare.PassengerTypeConditions,coachFare.PassengerTypeConditions,
				"CoachFareRemoteForRoute.PassengerTypeConditions and CoachFareForRoute.PassengerTypeConditions are not equal.");			

			for (int i=0;i < fare.RestrictedOperatorCodes.Length;i++)
				Assert.AreEqual(fare.RestrictedOperatorCodes[i],coachFare.RestrictedOperatorCodes[i],
					"CoachFareRemoteForRoute.RestrictedOperatorCodes and CoachFareForRoute.RestrictedOperatorCodes are not equal.");			
			for (int i=0;i < fare.RestrictedServices.Length;i++)
				Assert.AreEqual(fare.RestrictedServices[i],coachFare.RestrictedServices[i],
					"CoachFareRemoteForRoute.RestrictedServices and CoachFareForRoute.RestrictedServices are not equal.");			

			for (int i=0;i < fare.TimeRestrictions.Length;i++)
				Assert.IsTrue(fare.TimeRestrictions[i].Equals(coachFare.TimeRestrictions[i]),
					"CoachFareRemoteForRoute.TimeRestrictions and CoachFareForRoute.TimeRestrictions are not equal.");

			for (int i=0;i < fare.DayRestrictions.Length;i++)
				Assert.IsTrue((fare.DayRestrictions[i] == coachFare.DayRestrictions[i]),
					"CoachFareRemoteForRoute.DayRestrictions and CoachFareForRoute.DayRestrictions are not equal.");
			
			for (int i=0;i < fare.ChangeNaPTANs.Length;i++)
				Assert.AreEqual(fare.ChangeNaPTANs[i],coachFare.ChangeNaPTANs[i].Naptan,
					"CoachFareRemoteForRoute.ChangeNaPTANs and CoachFareForRoute.ChangeNaPTANs are not equal.");				

		}

	}
}
