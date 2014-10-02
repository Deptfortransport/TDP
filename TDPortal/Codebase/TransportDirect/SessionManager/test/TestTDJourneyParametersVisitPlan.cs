// *****************************************************
// NAME 		: TestTDJourneyParametersVisitPlan.cs
// AUTHOR 		: Tim Mollart
// DATE CREATED : 27/09/2005
// DESCRIPTION 	: NUnit test for class.
// NOTES		: 
// *****************************************************
//$Log: & 
//

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.SessionManager
{
	
	[TestFixture]
	[CLSCompliant(false)]
	public class TestTDJourneyParametersVisitPlan
	{
		public TestTDJourneyParametersVisitPlan()
		{
		}

		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation() );
		}


		[TearDown]
		public void Dispose()
		{
		}


		/// <summary>
		/// Propeties test.
		/// </summary>
		/// <TestPlan>
		/// Tests all of the properties on the class. Ensures that you can populate
		/// each property and retrieve values from it.
		/// </TestPlan>
		[Test]
		public void TestProperties()
		{

			//Get max segment size. Reduce by 1 as zero based array.
			int maxSegmentSize = int.Parse(Properties.Current["VisitPlan.MaxSegmentSize"]);
			
			//Set up test objects
			TDLocation[] testLocations = new TDLocation[maxSegmentSize];
			TDJourneyParameters.LocationSelectControlType[] testLocationSelectControlTypes = new TDJourneyParameters.LocationSelectControlType[maxSegmentSize];
			LocationSearch[] testLocationSearch = new LocationSearch[maxSegmentSize];
			

			try
			{

				TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();

				// Test Get/Set Properties
				for (int i = 0; i < maxSegmentSize; i++)
				{
					// Set up objects
					testLocations[i] = new TDLocation();
					testLocations[i].Description = "Location" + i.ToString();					
					
					testLocationSelectControlTypes[i] = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.NewLocation);
					
					testLocationSearch[i] = new LocationSearch();
					testLocationSearch[i].InputText = "LocationSearch" + i.ToString();
					

					// Set Objects
					parameters.SetLocation(i, testLocations[i]);
					parameters.SetLocationType(i, testLocationSelectControlTypes[i]);
					parameters.SetLocationSearch(i, testLocationSearch[i]);


					// Get Objects
					Assert.IsTrue( testLocations[i].Equals(parameters.GetLocation(i) ) );
					Assert.IsTrue( testLocationSelectControlTypes[i].Equals(parameters.GetLocationType(i) ) );
					Assert.IsTrue( testLocationSearch[i].Equals( parameters.GetLocationSearch(i) ) ) ;
				}

				// Test Stay Duration
				maxSegmentSize --;
				int[] stayDuration = new int[maxSegmentSize];

				for (int i = 0; i < maxSegmentSize; i ++)
				{
					// Set stay duration
					parameters.SetStayDuration(i, i + 10);

					// Get stay duration
					Assert.IsTrue( parameters.GetStayDuration(i) == i + 10 );
				}

			}

			catch (Exception ex)
			{
				Assert.Fail("Failed: " + ex.Message);
			}

		}


		/// <summary>
		/// Input summary test.
		/// </summary>
		/// <TestPlan>
		/// Tests the output of the InputSummary method against a pre-determined string.
		/// </TestPlan>
		[Test]
		public void TestInputSummary()
		{

			// Define three locations.
			TDLocation location1 = new TDLocation();
			TDLocation location2 = new TDLocation();
			TDLocation location3 = new TDLocation();

			location1.Description = "Location1";
			location2.Description = "Location2";
			location3.Description = "Location3";

			// Create parameters object
			TDJourneyParametersVisitPlan parameters = new TDJourneyParametersVisitPlan();

			// Set locations onto the object
			parameters.SetLocation(0, location1);
			parameters.SetLocation(1, location2);
			parameters.SetLocation(2, location3);

			// Define two stay durations
			parameters.SetStayDuration(0, 10);
			parameters.SetStayDuration(1, 20);
			
			// Set outward date
			parameters.OutwardDayOfMonth = "01";
			parameters.OutwardMonthYear = "01/2006";
			parameters.OutwardHour= "12";
			parameters.OutwardMinute = "00";

			// Set PT options
			parameters.PublicModes = new ModeType[] { ModeType.Rail, ModeType.Coach};

			// Check output is not null
			Assert.IsNotNull(parameters.InputSummary());

		}
	}
}