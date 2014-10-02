//********************************************************************************
//NAME         : TestMockFareInterface.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 22/10/2005
//DESCRIPTION  : Implementation of MockFareInterface class. Returns sample data for both of the coach fare interfaces.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFareInterfaces/TestMockFareInterface.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:18   mturner
//Initial revision.
//
//   Rev 1.4   May 25 2007 16:22:14   build
//Automatically merged from branch for stream4401
//
//   Rev 1.3.1.0   May 09 2007 14:50:36   mmodi
//Added test for new Coach fares
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.3   Jan 03 2006 12:38:40   mguney
//Constructors implemented. CoachFareForRoute or CoachFareForJourney created according to the interface type.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.2   Jan 03 2006 09:32:20   mguney
//GetRouteCoachFares method included.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.1   Dec 06 2005 11:27:54   mguney
//AvailabilityScore changed to Probability.
//Resolution for 3311: IF098 Interface Stub: Journey Restrictions
//
//   Rev 1.0   Dec 01 2005 09:26:40   mguney
//Initial revision.
//
//   Rev 1.6   Nov 29 2005 14:53:00   mguney
//4 new fares included for day return and exclude.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.5   Nov 06 2005 19:35:20   RPhilpott
//NUnit fixes
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Nov 04 2005 16:18:16   RPhilpott
//NUnit fixes.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Nov 01 2005 17:23:48   mguney
//Constant names changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 25 2005 11:10:40   mguney
//Sample data changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 24 2005 15:56:16   mguney
//Associated IR.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 24 2005 15:55:18   mguney
//Initial revision.

using System;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.CoachFares;


namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// MockFareInterface class. Will be used to mock FareInterfaceForJourney and FareInterfaceForRoute.
	/// </summary>
	public class TestMockFareInterface : IFaresInterface
	{
		public const string DISCOUNTCARDTYPENATEXCOACHMONTHLY = "NatEx Coach Card Monthly";
		public const string DISCOUNTCARDTYPENATEXCOACHWEEKLY = "NatEx Coach Card Weekly";
		public const string FARE_TYPE_STANDARD_SINGLE = "Standard Single";
		public const string FARE_TYPE_STANDARD_RETURN = "Standard Return";		

		private FareRequest request = null;
		private CoachFaresInterfaceType interfaceType;		

		/// <summary>
		/// Constructor for setting the interface type.
		/// </summary>
		/// <param name="type"></param>
		public TestMockFareInterface(CoachFaresInterfaceType type)
		{
			interfaceType = type;
		}

		/// <summary>
		/// Returns a CoachFare object using the given information.
		/// </summary>
		/// <param name="adult"></param>
		/// <param name="single"></param>
		/// <param name="fareValue"></param>
		/// <returns></returns>
		private CoachFare GetSampleFare(bool adult,bool single,int fareValue,string fareType,string discountCardType)
		{
			CoachFare fare = (interfaceType == CoachFaresInterfaceType.ForJourney) ? 
				(CoachFare)new CoachFareForJourney() :
				(CoachFare)new CoachFareForRoute();
			
			fare.IsAdult = adult;
			fare.ChildAgeMin = 5;
			fare.ChildAgeMax = 18;
			fare.DiscountCardType = discountCardType;
			fare.Fare = fareValue;

			if	(discountCardType == string.Empty)
			{
				fare.Flexibility = Flexibility.FullyFlexible;
			}
			else
			{
				fare.Flexibility = Flexibility.LimitedFlexibility;
			}

			fare.Availability = Probability.High;
			fare.FareType = fareType;
			fare.PassengerType = "Economy";
			fare.PassengerTypeConditions = string.Empty;
			fare.IsSingle = single;

			fare.OriginNaPTAN = new TDNaptan("9000ORIGIN", new OSGridReference(0, 0), "Origin");
			fare.DestinationNaPTAN = new TDNaptan("9000DESTN", new OSGridReference(0, 0), "Destination");
	
			return fare;
		}		

		#region IFaresInterface Members

		/// <summary>
		/// Mock method for getting the FareResult.
		/// </summary>
		/// <param name="fareRequest">Not used, but exposed via public property so contents can be checked.</param>
		/// <returns>FareResult object.</returns>
		public FareResult GetCoachFares(FareRequest fareRequest)		
		{
			const int NUMBEROFFARES = 16;
			FareResult result = new FareResult();			
			
			CoachFare[] fareList = (interfaceType == CoachFaresInterfaceType.ForJourney) ? 
				(CoachFare[])new CoachFareForJourney[NUMBEROFFARES] :
				(CoachFare[])new CoachFareForRoute[NUMBEROFFARES];

			fareList[0]  = GetSampleFare(true,	false, 585, FARE_TYPE_STANDARD_RETURN,	string.Empty);
			fareList[1]  = GetSampleFare(false,	false, 450, FARE_TYPE_STANDARD_RETURN,	string.Empty);
			fareList[2]  = GetSampleFare(true,	true,  260,	FARE_TYPE_STANDARD_SINGLE,	string.Empty);
			fareList[3]  = GetSampleFare(false,	true,   90,	FARE_TYPE_STANDARD_SINGLE,	string.Empty);			
			
			fareList[4]  = GetSampleFare(true,	false, 570, FARE_TYPE_STANDARD_RETURN,	DISCOUNTCARDTYPENATEXCOACHMONTHLY);
			fareList[5]  = GetSampleFare(false,	false, 430, FARE_TYPE_STANDARD_RETURN,	DISCOUNTCARDTYPENATEXCOACHMONTHLY);
			fareList[6]  = GetSampleFare(true,	true,  240,	FARE_TYPE_STANDARD_SINGLE,	DISCOUNTCARDTYPENATEXCOACHMONTHLY);
			fareList[7]  = GetSampleFare(false,	true,   70,	FARE_TYPE_STANDARD_SINGLE,	DISCOUNTCARDTYPENATEXCOACHMONTHLY);

			fareList[8]  = GetSampleFare(true,	false, 565,	FARE_TYPE_STANDARD_RETURN,	DISCOUNTCARDTYPENATEXCOACHWEEKLY);
			fareList[9]  = GetSampleFare(false,	false, 420, FARE_TYPE_STANDARD_RETURN,	DISCOUNTCARDTYPENATEXCOACHWEEKLY);
			fareList[10] = GetSampleFare(true,	true,  230,	FARE_TYPE_STANDARD_SINGLE,	DISCOUNTCARDTYPENATEXCOACHWEEKLY);
			fareList[11] = GetSampleFare(false,	true,   60,	FARE_TYPE_STANDARD_SINGLE,	DISCOUNTCARDTYPENATEXCOACHWEEKLY);

			fareList[12] = GetSampleFare(true,	false, 565,	TestMockExceptionalFaresLookup.DAYRETURN1, string.Empty);
			fareList[13] = GetSampleFare(true,	false, 565,	TestMockExceptionalFaresLookup.DAYRETURN2, string.Empty);
			fareList[14] = GetSampleFare(true,	false, 565,	TestMockExceptionalFaresLookup.EXCLUDE1, string.Empty);
			fareList[15] = GetSampleFare(true,	false, 565,	TestMockExceptionalFaresLookup.EXCLUDE2, string.Empty);			

			result.Fares = fareList;
			result.ErrorStatus = FareErrorStatus.Success;
			
			this.request = fareRequest;
			
			return result;
		}

		// read-only property exposes the passed-in request for checking ...
		public FareRequest Request
		{
			get { return request; }
		}

		#endregion

		/// <summary>
		/// Mock method for getting the FareResult. Returns the new NX fare ticket types
		/// </summary>
		/// <param name="fareRequest">Not used, but exposed via public property so contents can be checked.</param>
		/// <returns>FareResult object.</returns>
		public FareResult GetNewCoachFares(FareRequest fareRequest)		
		{
			const int NUMBEROFFARES = 4;
			FareResult result = new FareResult();			
			
			CoachFare[] fareList = (interfaceType == CoachFaresInterfaceType.ForJourney) ? 
				(CoachFare[])new CoachFareForJourney[NUMBEROFFARES] :
				(CoachFare[])new CoachFareForRoute[NUMBEROFFARES];

			fareList[0]  = GetSampleFare(true, true, 250, TestMockCoachFaresLookup.FLX, string.Empty);
			fareList[1]  = GetSampleFare(true, true, 250, TestMockCoachFaresLookup.ANR, string.Empty);
			fareList[2]  = GetSampleFare(true, true, 250, TestMockCoachFaresLookup.RNA, string.Empty);
			fareList[3]  = GetSampleFare(true, true, 250, TestMockCoachFaresLookup.RST, string.Empty);

			result.Fares = fareList;
			result.ErrorStatus = FareErrorStatus.Success;
			
			this.request = fareRequest;
			
			return result;
		}
	}
}
