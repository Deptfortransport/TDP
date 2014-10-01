// *********************************************** 
// NAME                 : TestCjp.cs
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 02/07/2003
// DESCRIPTION  : NUnit test class for Cjp.cs.
// The test harness requires JourneyResult1.xml,
// JourneyResult2.xml and JourneyResult3.xml to
// be the files deserialised.
// ************************************************
// $log$  

// Version      Ref     Author		Date            Description 
// V1.0					kcheung		02/07/2003      Initial Version 


using System;
using NUnit.Framework;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestCjp.
	/// </summary>
	[TestFixture]
	public class TestCjp
	{		
		[Test]
		public void JourneyPlan()
		{
			// Create a new instance of the CJP object
			CjpStub cjpObject = new CjpStub(".\\TestJourneyResult1.xml", 1);

			// Create a dummy request
			JourneyRequest request = new JourneyRequest();

			// Get the first result
			JourneyResult result = (JourneyResult)cjpObject.JourneyPlan(request);

			// Check that the result object is not null
			Assert.IsNotNull( result, "Result object is null." );

			Assert.AreEqual(2, result.messages[0].code);

			// 3 public journeys should exist.
			Assert.AreEqual(3, result.publicJourneys.Length);

			// private journeys should be null.
			Assert.IsNull(result.privateJourneys);

			// Check some of attributes of some legs.
			Assert.AreEqual(false, result.publicJourneys[0].legs[0].validated);
			Assert.AreEqual(result.publicJourneys[0].legs[0].board.stop.NaPTANID,"390022365");
			Assert.AreEqual(result.publicJourneys[0].legs[0].board.stop.name,"Shimpling Street: Halifax Place, C386");
			Assert.AreEqual(new DateTime(2003,7,24,10,27,00),result.publicJourneys[0].legs[0].alight.arriveTime);

			Assert.AreEqual("Mulleys Motorways Ltd",result.publicJourneys[0].legs[0].services[0].operatorName);
			Assert.AreEqual("375",result.publicJourneys[0].legs[0].services[0].serviceNumber);
			Assert.AreEqual("Bury St.Edmunds: Bus Station (8) dep:, Off St.An",
				result.publicJourneys[0].legs[0].services[0].destinationBoard);
			Assert.IsNull(result.publicJourneys[0].legs[0].services[0].direction);

			Assert.AreEqual(ModeType.Bus, result.publicJourneys[0].legs[2].mode);
			Assert.AreEqual(ModeType.Walk, result.publicJourneys[0].legs[3].mode);

			Assert.AreEqual(10, result.publicJourneys[1].legs.Length);
			Assert.AreEqual(ModeType.Bus, result.publicJourneys[1].legs[0].mode);
			Assert.AreEqual(false, result.publicJourneys[1].legs[0].validated);

			Assert.AreEqual(12, result.publicJourneys[2].legs.Length);

				// Create a new instance of the CJP object, loading list of test data files from properties
			cjpObject = new CjpStub(".\\TestProperties.xml");
			result = (JourneyResult)cjpObject.JourneyPlan(request);

			Assert.AreEqual(3, result.messages.Length);
			Assert.AreEqual(516, result.messages[0].code);
			Assert.AreEqual(
				"Code 516 : Error at: {http://217.34.160.179/scripts/xmlplanner.dll}",
				result.messages[0].description);

			// Expect 1 private journey.
			Assert.AreEqual(1, result.privateJourneys.Length);

			// Test some of the attributes of the private journey.
			Assert.AreEqual(false, result.privateJourneys[0].congestion);
			Assert.AreEqual("Start point", result.privateJourneys[0].start.name);
			Assert.AreEqual
				("Finish point", result.privateJourneys[0].finish.name);
			Assert.AreEqual(TurnDirection.Left,
				((DriveSection)result.privateJourneys[0].sections[0]).turnDirection);
			Assert.AreEqual(TurnAngle.Turn,
				((DriveSection)result.privateJourneys[0].sections[0]).turnAngle);
			
			// Test some of the attributes of the public journey
			Assert.AreEqual(ModeType.Walk, result.publicJourneys[0].legs[0].mode);
			Assert.AreEqual(new DateTime(2003,6,17,8,8,0),
				result.publicJourneys[0].legs[0].board.departTime);

			// Get the next journey result - expect to get the first result back
			result = (JourneyResult)cjpObject.JourneyPlan(request);

			Assert.AreEqual(result.messages[0].code, 2);

			// 3 public journeys should exist.
			Assert.AreEqual(3, result.publicJourneys.Length);

			// private journeys should be null.
			Assert.IsNull(result.privateJourneys);

			// Check some of attributes of some legs.
			Assert.AreEqual(false, result.publicJourneys[0].legs[0].validated);
			Assert.AreEqual("390022365",
				result.publicJourneys[0].legs[0].board.stop.NaPTANID);
			Assert.AreEqual("Shimpling Street: Halifax Place, C386",
				result.publicJourneys[0].legs[0].board.stop.name);
			Assert.AreEqual(new DateTime(2003,7,24,10,27,00),
				result.publicJourneys[0].legs[0].alight.arriveTime);

			Assert.AreEqual("Mulleys Motorways Ltd",
				result.publicJourneys[0].legs[0].services[0].operatorName);
			Assert.AreEqual("375",
				result.publicJourneys[0].legs[0].services[0].serviceNumber);
			Assert.AreEqual("Bury St.Edmunds: Bus Station (8) dep:, Off St.An",
				result.publicJourneys[0].legs[0].services[0].destinationBoard);
			Assert.IsNull(result.publicJourneys[0].legs[0].services[0].direction);
		}
	}
}
