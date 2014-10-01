// ******************************************************************** 
// NAME                 : TestCostSearchRunner.cs 
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 23/02/2005
// DESCRIPTION			: Tests CostSearchRunner functionality 
//
// ********************************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/Test/TestCostSearchRunner.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:42   mturner
//Initial revision.
//
//   Rev 1.11   Feb 10 2006 15:09:36   build
//Automatically merged from branch for stream3180
//
//   Rev 1.10.1.0   Dec 12 2005 16:39:34   tmollart
//Modified to use new initialise parameters method on session manager.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.10   Nov 09 2005 12:23:52   build
//Automatically merged from branch for stream2818
//
//   Rev 1.9.1.0   Oct 14 2005 15:08:50   jgeorge
//Updated to use new AsyncCallState and subclasses as part of refactoring of wait page.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.9   Apr 25 2005 17:44:10   jmorrissey
//Update to TestValidateAndRunServicesSynchronous method. Moved some test code from TestCostSearchFacade class but commented it out for now.
//Resolution for 2290: Session data for cost based searching - coach
//
//   Rev 1.8   Mar 30 2005 12:14:34   jmorrissey
//Updated after changes to how the deferred manager is used in CostSearchRunner
//
//   Rev 1.7   Mar 22 2005 17:54:48   jmorrissey
//After initial back end integration
//
//   Rev 1.6   Mar 15 2005 17:34:44   jmorrissey
//Updated after code review
//Resolution for 1929: DEV Code Review: Cost Search Runner
//
//   Rev 1.5   Mar 07 2005 17:13:10   jmorrissey
//Updated to use new travel date structure
//
//   Rev 1.4   Mar 07 2005 10:46:36   jmorrissey
//Fixed FxCop violation.
//Resolution for 1929: DEV Code Review: Cost Search Runner
//
//   Rev 1.3   Mar 03 2005 11:31:42   jmorrissey
//Fixed bug in AdjustSearchDateRange method. Now handles no return dates correctly.
//Resolution for 1929: DEV Code Review: Cost Search Runner
//
//   Rev 1.2   Mar 02 2005 11:22:34   jmorrissey
//Added fully qualified namespace in front of  CostBasedSearchStatus to fix quirky Nant build error.
//Resolution for 1929: DEV Code Review: Cost Search Runner
//
//   Rev 1.1   Mar 01 2005 16:47:16   jmorrissey
//Completed NUnit and Fxcop
//
//   Rev 1.0   Mar 01 2005 16:46:34   jmorrissey
//Initial revision.

using System;
using System.Globalization;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.CostSearch;
using TransportDirect.Common;


namespace TransportDirect.UserPortal.CostSearchRunner
{
	/// <summary>
	/// TestCostSearchRunner is an Nunit class that tests the CostSearchRunner class.
	/// </summary>
	[TestFixture]
	public class TestCostSearchRunner
	{
		private ITDSessionManager dummySessionManager;
		private CostSearchRunner costSearchRunner;		
		private CostSearchParams costSearchParams;
		private CostSearchTicket singleRailTicket;		
		private CostSearchTicket singleCoachTicket;		
		private FindCostBasedPageState pageState;


		/// <summary>
		/// default constructor
		/// </summary>
		public TestCostSearchRunner()
		{
			
		}

		/// <summary>
		/// Initialises TDServiceDiscovery
		/// </summary>
		[TestFixtureSetUp]
		public void InitTest()
		{
			//initialise services
			TDServiceDiscovery.Init(new TestCostSearchRunnerInitialisation());	

			//initialise members
			costSearchRunner = new CostSearchRunner();
			dummySessionManager = TDSessionManager.Current;	
			
		}

		/// <summary>
		/// Sets up for each individual test by initialising journey parameters, page states,
		/// etc
		/// </summary>
		[SetUp]
		public void Init()
		{

			//Initialise FindPageState						
			dummySessionManager.InitialiseJourneyParameters(FindAMode.Fare);

			#region cost search parameters

			// Setup valid cost search parameters
			costSearchParams = new CostSearchParams();		

			// Outward date is one month from now
			TDDateTime outwardDate = DateTime.Now.AddMonths(1);
			costSearchParams.OutwardHour = outwardDate.Hour.ToString(CultureInfo.InvariantCulture);			
			costSearchParams.OutwardDayOfMonth = outwardDate.Day.ToString(CultureInfo.InvariantCulture);
			//ensure month is a 2 digit number
			if (outwardDate.Month.ToString(CultureInfo.InvariantCulture).Length < 2)
			{
				costSearchParams.OutwardMonthYear = "0" + outwardDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + outwardDate.Year.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				costSearchParams.OutwardMonthYear = outwardDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + outwardDate.Year.ToString(CultureInfo.InvariantCulture);
			}

			// return date is 4 hours after the outward time
			TDDateTime returnDate = outwardDate.AddMinutes(240);  			
			costSearchParams.ReturnHour = returnDate.Hour.ToString(CultureInfo.InvariantCulture);			
			costSearchParams.ReturnDayOfMonth = returnDate.Day.ToString(CultureInfo.InvariantCulture);
			//ensure month is a 2 digit number
			if (returnDate.Month.ToString(CultureInfo.InvariantCulture).Length < 2)
			{
				costSearchParams.ReturnMonthYear = "0" + returnDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + returnDate.Year.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				costSearchParams.ReturnMonthYear = returnDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + returnDate.Year.ToString(CultureInfo.InvariantCulture);
			}

			//set up origin location			
			costSearchParams.OriginLocation = new TDLocation();
			costSearchParams.OriginLocation.Status = TDLocationStatus.Valid;
			costSearchParams.OriginLocation.NaPTANs = new TDNaptan[3];
			//coach naptan
			costSearchParams.OriginLocation.NaPTANs[0] = new TDNaptan("9000origin", new OSGridReference(10,10));
			//rail naptan
			costSearchParams.OriginLocation.NaPTANs[1] = new TDNaptan("9100origin", new OSGridReference(100,100));
			//air naptan
			costSearchParams.OriginLocation.NaPTANs[2] = new TDNaptan("9200origin", new OSGridReference(1000,1000));
			costSearchParams.OriginLocation.Locality = "locality";			
			
			//set up destination location
			costSearchParams.DestinationLocation = new TDLocation();
			costSearchParams.DestinationLocation.Status = TDLocationStatus.Valid;
			costSearchParams.DestinationLocation.NaPTANs = new TDNaptan[3];
			//coach naptan
			costSearchParams.DestinationLocation.NaPTANs[0] = new TDNaptan("9000dest", new OSGridReference(20,20));
			//rail naptan
			costSearchParams.DestinationLocation.NaPTANs[1] = new TDNaptan("9100dest", new OSGridReference(200,200));
			//air naptan
			costSearchParams.DestinationLocation.NaPTANs[2] = new TDNaptan("9200dest", new OSGridReference(2000,2000));
			costSearchParams.DestinationLocation.Locality = "locality";			

			costSearchParams.CoachDiscountedCard = string.Empty;			
			costSearchParams.TravelModesParams = new TicketTravelMode[3];
			costSearchParams.TravelModesParams[0] = TicketTravelMode.Coach;
			costSearchParams.TravelModesParams[1] = TicketTravelMode.Air;
			costSearchParams.TravelModesParams[2] = TicketTravelMode.Rail;			
			costSearchParams.OutwardFlexibilityDays = 3;		
			costSearchParams.InwardFlexibilityDays = 3;

			//set up valid CostSearchTickets
			singleRailTicket = new CostSearchTicket("Apex Single /any rail",Flexibility.FullyFlexible,"Apex",27.10f,15.00f,float.NaN,float.NaN,40.00f,5.00f,0,16,Probability.Medium);
			singleRailTicket.CombinedTicketIndex = 1; 
			singleRailTicket.TravelDateForTicket = new TravelDate(1, TDDateTime.Now,TicketTravelMode.Rail,10.00f,50.00f,true);	
			singleRailTicket.TravelDateForTicket.TicketType = TicketType.Single;

			singleCoachTicket = new CostSearchTicket("NX Standard",Flexibility.FullyFlexible,"NXS",50.00f,10.00f,float.NaN,float.NaN,25.00f,7.00f,0,16,Probability.High);
			singleCoachTicket.CombinedTicketIndex = 3;
			singleCoachTicket.TravelDateForTicket = new TravelDate(101, TDDateTime.Now,TicketTravelMode.Coach,10.00f,50.00f,true);
			singleCoachTicket.TravelDateForTicket.TicketType = TicketType.Single;

			#endregion
		}

		/// <summary>
		/// method gets called automatically at the end of every test method
		/// </summary>
		[TestFixtureTearDown]
		public void CleanUp()
		{			
	
		}

		/// <summary>
		/// Tests ValidateAndRunFares - this will invoke an asynchronous call to the cjp because the search params includes coach mode
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresAsynchronous()
		{
			//call AssembleFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);

			//check that no validation errors have been returned
			Assert.IsTrue(dummySessionManager.ValidationError.ErrorIDs.Length == 0);
			
			//check that stata data is set to InProgress 
			Assert.IsTrue(stateData == AsyncCallStatus.InProgress, "ValidateAndRunFares should return status of InProgress ");
		}			

		/// <summary>
		/// Tests ValidateAndRunFares - this will invoke an synchronous call to the cjp
		/// because the search params do not include coach mode
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresSynchronous()
		{
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			//change coach naptan to a rail one
			costSearchParams.DestinationLocation.NaPTANs[0] = new TDNaptan("9100dest2", new OSGridReference(30,30));
			//change coach naptan to a rail one
			costSearchParams.OriginLocation.NaPTANs[0] = new TDNaptan("9100origin2", new OSGridReference(40,40));

			//ValidateAndRunFares calls CallAssembleFares which attempts to read from deferred storage which is not available from a nunit test, so triggering an error that is handled in CallAssembleFares
			//Therefore, check that ValidateAndRunFares has completed ok upto the point with no validation errors 			
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);		
	
			//check that no validation errors have been returned
			Assert.IsTrue(dummySessionManager.ValidationError.ErrorIDs.Length == 0);

			//check that stata data is set to InProgress 
			Assert.IsTrue(stateData == AsyncCallStatus.InProgress, "ValidateAndRunFares should return status of InProgress ");
			
		}

		/// <summary>
		/// Tests ValidateAndRunServices asynchronously - rail mode
		/// </summary>
		[Test]
		public void TestValidateAndRunServicesAsynchronous()
		{
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			//call ValidateAndRunFares so that dummy session manager is populated with a request 
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
						
			dummySessionManager.AsyncCallState = new CostBasedServicesSearchState();
			//call ValidateAndRunServices, with a dummy ticket (populated in the Init method)
			stateData = costSearchRunner.ValidateAndRunServices(singleRailTicket);
		
			//check that stata data is set to InProgress 
			Assert.IsTrue(stateData == AsyncCallStatus.InProgress, "ValidateAndRunServices should return status of InProgress ");
		
		}

		/// <summary>
		/// Tests ValidateAndRunServices synchronously - coach mode
		/// </summary>
		[Test]
		public void TestValidateAndRunServicesSynchronous()
		{
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();

			//call ValidateAndRunFares so that dummy session manager is populated with a request 
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);

			//call ValidateAndRunServices, with a dummy ticket populated in the Init method
			try
			{
				//In a synchronous call the last thing that happens in ValidateAndRunServices is an attempt to write to deferred storage
				//which is not available from a nunit test
				//Therefore we can only check that ValidateAndRunServices has completed ok upto the point with no validation errors 
				//and that the error thrown after that is the one that we would expect 
				dummySessionManager.AsyncCallState = new CostBasedServicesSearchState();
				stateData = costSearchRunner.ValidateAndRunServices(singleCoachTicket);
			}
			catch (TDException ex)
			{	
					
				//check that no validation errors have been returned
				Assert.IsTrue(dummySessionManager.ValidationError.ErrorIDs.Length == 0);

				//check that exception is the one we expect which only occurs when trying to
				//update the deferred session
				AssertStringContains("updating TDSessionSerializer", ex.Message);

				//check that inner exception is also as expected due to deferred storage objects being null
				AssertStringContains("Object reference not set to an instance of an object", ex.InnerException.Message);

				//check that stata data is set to InProgress 
				Assert.IsTrue(stateData == AsyncCallStatus.InProgress, "ValidateAndRunServices should return status of InProgress ");
			}
			
		}
			
		/// <summary>
		/// Tests ValidateAndRunFares with invalid location parameters
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresInvalidLocations()
		{
			//empty Destination and Origin Locations
			costSearchParams.DestinationLocation = new TDLocation();
			costSearchParams.OriginLocation = new TDLocation();			

			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to ValidationError
			Assert.IsTrue(stateData == AsyncCallStatus.ValidationError, "ValidateAndRunFares should return status of ValidationError ");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.OriginLocationInvalid), "Incorrect validation error in session manager");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.DestinationLocationInvalid), "Incorrect validation error in session manager");

		}		

		/// <summary>
		/// Tests ValidateAndRunFares with invalid location parameters
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresOverlappingLocations()
		{
			//assign same naptans to the destination location as the origin location has
			//coach naptan
			costSearchParams.DestinationLocation.NaPTANs[0] = new TDNaptan("9000origin", new OSGridReference(10,10));
			//rail naptan
			costSearchParams.DestinationLocation.NaPTANs[1] = new TDNaptan("9100origin", new OSGridReference(100,100));
			//air naptan
			costSearchParams.DestinationLocation.NaPTANs[2] = new TDNaptan("9200origin", new OSGridReference(1000,1000));
		
			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to ValidationError
			Assert.IsTrue(stateData == AsyncCallStatus.ValidationError, "ValidateAndRunFares should return status of ValidationError ");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.OriginAndDestinationOverlap), "Incorrect validation error in session manager");

		}		

		/// <summary>
		/// Tests ValidateAndRunFares with invalid outward date
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresOutwardDateInvalid()
		{
			//invalid day of month
			costSearchParams.OutwardDayOfMonth = "32";
			
			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to ValidationError
			Assert.IsTrue(stateData == AsyncCallStatus.ValidationError, "ValidateAndRunFares should return status of ValidationError ");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.OutwardDateTimeInvalid), "Incorrect validation error in session manager");

		}		

		/// <summary>
		/// Tests ValidateAndRunFares with invalid return date
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresReturnDateInvalid()
		{
			//invalid day of month
			costSearchParams.ReturnDayOfMonth = "32";
			
			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to ValidationError
			Assert.IsTrue(stateData == AsyncCallStatus.ValidationError, "ValidateAndRunFares should return status of ValidationError ");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.ReturnDateTimeInvalid), "Incorrect validation error in session manager");

		}		

		/// <summary>
		/// Tests ValidateAndRunFares runs ok when no return date is specified
		/// </summary>
		[Test]
		public void TestValidateDatesNoReturnDate()
		{
			//no return date
			costSearchParams.ReturnMonthYear = ReturnType.NoReturn.ToString();
			costSearchParams.ReturnDayOfMonth = string.Empty;
			costSearchParams.InwardFlexibilityDays = 0;
			
			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to InProgress
			Assert.IsTrue(stateData == AsyncCallStatus.InProgress, "ValidateAndRunFares should return status of InProgress ");
		}		

		/// <summary>
		/// Tests ValidateAndRunFares with invalid ouward and invalid return date
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresOutwardAndReturnDatesInvalid()
		{
			//invalid day of month
			costSearchParams.OutwardDayOfMonth = "32";
			costSearchParams.ReturnDayOfMonth = "32";
			
			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to ValidationError
			Assert.IsTrue(stateData == AsyncCallStatus.ValidationError, "ValidateAndRunFares should return status of ValidationError ");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.OutwardAndReturnDateTimeInvalid), "Incorrect validation error in session manager");

		}		

		/// <summary>
		/// Tests ValidateAndRunFares with a return date before the outward date
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresReturnDateBeforeOutwardDate()
		{
			//outward date has been set to be 1 month from now in the Init method
			//set return date to 1 week from now which should through an error
			TDDateTime returnDate = DateTime.Now.AddDays(7);		
			costSearchParams.ReturnHour = returnDate.Hour.ToString(CultureInfo.InvariantCulture);			
			costSearchParams.ReturnDayOfMonth = returnDate.Day.ToString(CultureInfo.InvariantCulture);
			costSearchParams.ReturnMonthYear = returnDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + returnDate.Year.ToString(CultureInfo.InvariantCulture);
			
			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to ValidationError
			Assert.IsTrue(stateData == AsyncCallStatus.ValidationError, "ValidateAndRunFares should return status of ValidationError ");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.ReturnDateTimeNotLaterThanOutwardDateTime), "Incorrect validation error in session manager");

		}		

		/// <summary>
		/// Tests ValidateAndRunFares with a return day set but month/year set to NoReturn
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresReturnMonthMissing()
		{
			//outward date has been set to be 1 month from now in the Init method
			//set return date to 1 week from now which should through an error	
			
			costSearchParams.ReturnMonthYear = ReturnType.NoReturn.ToString();
			
			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to ValidationError
			Assert.IsTrue(stateData == AsyncCallStatus.ValidationError, "ValidateAndRunFares should return status of ValidationError ");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.ReturnMonthMissing), "Incorrect validation error in session manager");
		}		
		
		/// <summary>
		/// Tests ValidateAndRunFares with return monthyear but no return day
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresReturnDayMissing()
		{
			//no day of month specified
			costSearchParams.ReturnDayOfMonth = "";
			
			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to ValidationError
			Assert.IsTrue(stateData == AsyncCallStatus.ValidationError, "ValidateAndRunFares should return status of ValidationError ");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.ReturnDateMissing), "Incorrect validation error in session manager");

		}		

		/// <summary>
		/// Tests ValidateAndRunFares with outward date before today
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresOutwardDateInThePast()
		{
			//outward date 1 day before now
			TimeSpan oneDay = new TimeSpan(1,0,0,0);
			TDDateTime outwardDate = DateTime.Now.Subtract(oneDay);
			costSearchParams.OutwardHour = outwardDate.Hour.ToString(CultureInfo.InvariantCulture);			
			costSearchParams.OutwardDayOfMonth = outwardDate.Day.ToString(CultureInfo.InvariantCulture);
			//ensure month is a 2 digit number
			if (outwardDate.Month.ToString(CultureInfo.InvariantCulture).Length < 2)
			{
				costSearchParams.OutwardMonthYear = "0" + outwardDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + outwardDate.Year.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				costSearchParams.OutwardMonthYear = outwardDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + outwardDate.Year.ToString(CultureInfo.InvariantCulture);
			}
			
			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to ValidationError
			Assert.IsTrue(stateData == AsyncCallStatus.ValidationError, "ValidateAndRunFares should return status of ValidationError ");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.OutwardDateTimeNotLaterThanNow), "Incorrect validation error in session manager");

		}			
	
		/// <summary>
		/// Tests ValidateAndRunFares with outward and return dates before today
		/// </summary>
		[Test]
		public void TestValidateAndRunFaresOutwardAndReturnDatesInThePast()
		{
			//outward date 2 days before now
			TimeSpan twoDays = new TimeSpan(2,0,0,0);
			TDDateTime outwardDate = DateTime.Now.Subtract(twoDays);
			costSearchParams.OutwardHour = outwardDate.Hour.ToString(CultureInfo.InvariantCulture);			
			costSearchParams.OutwardDayOfMonth = outwardDate.Day.ToString(CultureInfo.InvariantCulture);
			//ensure month is a 2 digit number
			if (outwardDate.Month.ToString(CultureInfo.InvariantCulture).Length < 2)
			{
				costSearchParams.OutwardMonthYear = "0" + outwardDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + outwardDate.Year.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				costSearchParams.OutwardMonthYear = outwardDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + outwardDate.Year.ToString(CultureInfo.InvariantCulture);
			}
			
			//return date 1 day before now
			TimeSpan oneDay = new TimeSpan(1,0,0,0);
			TDDateTime returnDate = DateTime.Now.Subtract(oneDay);
			costSearchParams.ReturnHour = returnDate.Hour.ToString(CultureInfo.InvariantCulture);			
			costSearchParams.ReturnDayOfMonth = returnDate.Day.ToString(CultureInfo.InvariantCulture);
			//ensure month is a 2 digit number
			if (returnDate.Month.ToString(CultureInfo.InvariantCulture).Length < 2)
			{
				costSearchParams.ReturnMonthYear = "0" + returnDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + returnDate.Year.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				costSearchParams.ReturnMonthYear = returnDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + returnDate.Year.ToString(CultureInfo.InvariantCulture);
			}

			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);
			
			//check that stata data is set to ValidationError
			Assert.IsTrue(stateData == AsyncCallStatus.ValidationError, "ValidateAndRunFares should return status of ValidationError ");

			//check that validation error is as expected
			Assert.IsTrue(dummySessionManager.ValidationError.Contains(ValidationErrorID.OutwardAndReturnDateTimeNotLaterThanNow), "Incorrect validation error in session manager");
		}		
		
		/// <summary>
		/// Tests AdjustSearchDateRange method used by ValidateAndRunFares
		/// </summary>
		[Test]
		public void TestAdjustSearchDateRange()
		{
			//flexibility is already set to 3 days for both outward and return dates by Init method

			//outward date tomorrow		
			TDDateTime dateToday = DateTime.Today;			
			TDDateTime outwardDate = dateToday.AddDays(1);	
			costSearchParams.OutwardDayOfMonth = outwardDate.Day.ToString(CultureInfo.InvariantCulture);
			//ensure month is a 2 digit number
			if (outwardDate.Month.ToString(CultureInfo.InvariantCulture).Length < 2)
			{
				costSearchParams.OutwardMonthYear = "0" + outwardDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + outwardDate.Year.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				costSearchParams.OutwardMonthYear = outwardDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + outwardDate.Year.ToString(CultureInfo.InvariantCulture);
			}
			
			//return date in 2 days time			
			TDDateTime returnDate = dateToday.AddDays(2);
			costSearchParams.ReturnHour = returnDate.Hour.ToString(CultureInfo.InvariantCulture);			
			costSearchParams.ReturnDayOfMonth = returnDate.Day.ToString(CultureInfo.InvariantCulture);
			//ensure month is a 2 digit number
			if (returnDate.Month.ToString(CultureInfo.InvariantCulture).Length < 2)
			{
				costSearchParams.ReturnMonthYear = "0" + returnDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + returnDate.Year.ToString(CultureInfo.InvariantCulture);
			}
			else
			{
				costSearchParams.ReturnMonthYear = returnDate.Month.ToString(CultureInfo.InvariantCulture) + "/" + returnDate.Year.ToString(CultureInfo.InvariantCulture);
			}

			//call ValidateAndRunFares
			dummySessionManager.AsyncCallState = new CostBasedFaresSearchState();
			AsyncCallStatus stateData = costSearchRunner.ValidateAndRunFares(costSearchParams);

			//check the search dates that were used 			
			pageState = (FindCostBasedPageState)dummySessionManager.FindPageState;
			CostSearchRequest costSearchRequest = pageState.SearchRequest;

			Assert.IsNotNull(costSearchRequest, "pageState.SearchRequest was null after calling ValidateAndRunFares");
			//outward search should be from today thru next 3 days (flexibility cannot go back past today)
			Assert.IsTrue(costSearchRequest.SearchOutwardStartDate.Day == dateToday.Day);
			Assert.IsTrue(costSearchRequest.SearchOutwardStartDate.Month == dateToday.Month);
			Assert.IsTrue(costSearchRequest.SearchOutwardStartDate.Year == dateToday.Year);

			Assert.IsTrue(costSearchRequest.SearchOutwardEndDate.Day == dateToday.AddDays(4).Day);
			Assert.IsTrue(costSearchRequest.SearchOutwardEndDate.Month == dateToday.AddDays(4).Month);
			Assert.IsTrue(costSearchRequest.SearchOutwardEndDate.Year == dateToday.AddDays(4).Year);

			//return search should be from today thru next 5 days (flexibility cannot go back past outward start date)
			Assert.IsTrue(costSearchRequest.SearchReturnStartDate.Day == dateToday.Day);
			Assert.IsTrue(costSearchRequest.SearchReturnStartDate.Month == dateToday.Month);
			Assert.IsTrue(costSearchRequest.SearchReturnStartDate.Year == dateToday.Year);

			Assert.IsTrue(costSearchRequest.SearchReturnEndDate.Day == dateToday.AddDays(5).Day);
			Assert.IsTrue(costSearchRequest.SearchReturnEndDate.Month == dateToday.AddDays(5).Month);
			Assert.IsTrue(costSearchRequest.SearchReturnEndDate.Year == dateToday.AddDays(5).Year);
			
			//check that stata data is set to InProgress
			Assert.IsTrue(stateData == AsyncCallStatus.InProgress, "ValidateAndRunFares should return status of InProgress ");

		}
		
		/// <summary>
		/// tests whether a string contains an expected value.
		/// </summary>
		/// <param name="expected"></param>
		/// <param name="actual"></param>
		private void AssertStringContains( string expected, string actual )
		{
			AssertStringContains( expected, actual, string.Empty );
		}

		/// <summary>
		/// tests whether a string contains an expected value.
		/// </summary>
		/// <param name="expected"></param>
		/// <param name="actual"></param>
		/// <param name="message"></param>
		private void AssertStringContains( string expected, string actual,
			string message )
		{
			if ( actual.IndexOf( expected ) < 0 )
			{
				Assert.Fail(message);
			}
		}

//		This test code used to be in TestCostSearchFacade, until the design change that meant that Coach services
//		are put together in the CostSearchRunner. I have left this here for reference for the time being.
//		In the future, maybe we can adapt it to be used in this test class, although at the moment this is impossible
//		because Coach services are assembled in the AssembleCoachServices method, which cannot access the deferred session
//		manager from NUnit code
//
//
//		/// <summary>
//		/// Tests the AssembleServices method for a single coach ticket
//		/// </summary>
//		[Test]
//		public void TestAssembleServicesCoachSingle()
//		{
//			//call AssembleFares so that we have an existing result
//			existingResult = (CostSearchResult)costSearchFacade.AssembleFares(singleCoachRequest, ref publicJourneyStore);		
//
//			//pick a ticket
//			TravelDate[] travelDates = existingResult.GetAllTravelDates();
//			selectedOutwardTicket = travelDates[0].OutwardTickets[0];				
//
//			//call AssembleServices
//			ICostSearchResult updatedResult = costSearchFacade.AssembleServices(singleCoachRequest, existingResult, selectedOutwardTicket);
//
//			//check result is not null
//			Assert.IsNotNull(updatedResult, "TestAssembleServicesCoachSingle has unexpectedly returned a null CostSearchResult"); 
//
//			//check result has no errors
//			CostSearchError[] errors = updatedResult.GetErrors();
//			Assert.IsTrue(errors.Length == 0, "TestAssembleServicesCoachSingle has unexpectedly returned CostSearchErrors"); 
//
//			//check that a TDJourneyResult exists
//			TDJourneyResult journeyResult = (TDJourneyResult)updatedResult.GetJourneyResultsForTicket(selectedOutwardTicket);
//			Assert.IsNotNull(journeyResult, "TestAssembleServicesCoachSingle has unexpectedly returned a null JourneyResult for the selected ticket"); 
//
//			//check number of public journeys in the result
//			Assert.IsTrue(journeyResult.OutwardPublicJourneyCount > 0, "TestAssembleServicesCoachSingle has not returned any OutwardPublicJourneys for the selected ticket"); 
//			
//			//whilst there are 20 OutwardPublicJourneys in the mock result 
//			//(C:\TDPortal\Journeys\CoachLondonCambridge20050608.xml), only 15 are 'trunk' results
//			Assert.IsTrue(journeyResult.OutwardPublicJourneyCount == 15, "TestAssembleServicesCoachSingle has not returned the expected number of OutwardPublicJourneys for the selected ticket"); 
//			
//			//as this is a single, there should be no return journeys in the result
//			Assert.IsTrue(journeyResult.ReturnPublicJourneyCount == 0, "TestAssembleServicesCoachSingle has unexpectedly returned some ReturnPublicJourneys for the selected ticket"); 
//
//		}
//
//		/// <summary>
//		/// Tests the AssembleServices method with a Return coach ticket
//		/// </summary>
//		[Test]
//		public void TestAssembleServicesCoachReturn()
//		{
//			#region ReturnCoachRequest
//
//			//copy most details from the single coach request
//			returnCoachRequest = singleCoachRequest;
//
//			//return locations
//			returnCoachRequest.ReturnOriginLocation = singleCoachRequest.DestinationLocation;
//			returnCoachRequest.ReturnDestinationLocation = singleCoachRequest.OriginLocation;
//
//			//return on the 15/06/2005
//			TDDateTime fifteenthJune = new TDDateTime(2005,6,15);
//			returnCoachRequest.ReturnDateTime = fifteenthJune;
//			returnCoachRequest.SearchReturnStartDate = fifteenthJune;
//			returnCoachRequest.SearchReturnEndDate = fifteenthJune;
//
//			//flexibility
//			returnCoachRequest.InwardFlexibilityDays = 0;
//
//			#endregion
//
//			//call AssembleFares so that we have an existing result
//			existingResult = (CostSearchResult)costSearchFacade.AssembleFares(returnCoachRequest, ref publicJourneyStore);		
//
//			//pick a Return ticket
//			TravelDate[] travelDates = existingResult.GetAllTravelDates();
//			selectedReturnTicket = travelDates[0].ReturnTickets[0];				
//
//			//call AssembleServices
//			ICostSearchResult updatedResult = costSearchFacade.AssembleServices(returnCoachRequest, existingResult, selectedReturnTicket);
//
//			//check result is not null
//			Assert.IsNotNull(updatedResult, "TestAssembleServicesCoachReturn has unexpectedly returned a null CostSearchResult"); 
//
//			//check result has no errors
//			CostSearchError[] errors = updatedResult.GetErrors();
//			Assert.IsTrue(errors.Length == 0, "TestAssembleServicesCoachReturn has unexpectedly returned CostSearchErrors"); 
//
//			//check that a TDJourneyResult exists
//			TDJourneyResult journeyResult = (TDJourneyResult)updatedResult.GetJourneyResultsForTicket(selectedReturnTicket);
//			Assert.IsNotNull(journeyResult, "TestAssembleServicesCoachReturn has unexpectedly returned a null JourneyResult for the selected ticket"); 
//
//			//check number of outward journeys in the result
//			Assert.IsTrue(journeyResult.OutwardPublicJourneyCount > 0, "TestAssembleServicesCoachReturn has not returned any OutwardPublicJourneys for the selected ticket"); 
//			
//			//there are 20 Public Journeys in the mock outward result (C:\TDPortal\Journeys\CoachLondonCambridge20050608.xml), 
//			//only 15 are 'trunk' results
//			Assert.IsTrue(journeyResult.OutwardPublicJourneyCount == 15, "TestAssembleServicesCoachReturn has not returned the expected number of OutwardPublicJourneys for the selected ticket"); 
//			
//			//check number of return journeys in the result
//			Assert.IsTrue(journeyResult.ReturnPublicJourneyCount > 0, "TestAssembleServicesCoachReturn has not returned any ReturnPublicJourneys for the selected ticket"); 
//
//			//there are 24 Public Journeys in the mock inward result (C:\TDPortal\Journeys\CoachCambridgeLondon20050615.xml)
//			//20 are correct results but 4 others will have been filtered out by the 'CoachFareMergePolicy.IsInwardJourneyValid' check called by  
//			//NatExFaresSupplier.UpdateTravelDateFaresAndTickets method  
//			Assert.IsTrue(journeyResult.ReturnPublicJourneyCount == 20, "TestAssembleServicesCoachReturn has not returned the expected number of ReturnPublicJourneys for the selected ticket"); 
//
//		}

	}
}
