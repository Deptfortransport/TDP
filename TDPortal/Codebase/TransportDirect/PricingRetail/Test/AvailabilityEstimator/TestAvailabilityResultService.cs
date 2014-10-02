// *********************************************** 
// NAME			: TestAvailabilityResultService.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Test implementation of the AvailabilityResultService class
// ************************************************ 

// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/AvailabilityEstimator/TestAvailabilityResultService.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:16   mturner
//Initial revision.
//
//   Rev 1.1   Mar 18 2005 14:47:04   jbroome
//Added missing class documentation comments (Code Review)
//
//   Rev 1.0   Feb 17 2005 14:44:20   jbroome
//Initial revision.
//Resolution for 1923: DEV Code Review : Availability Estimator

using System;

using NUnit.Framework;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Test class which tests the valid creation of the AvailabilityResultService class
	/// </summary>
	[TestFixture]
	public class TestAvailabilityResultService
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TestAvailabilityResultService()
		{
		}

		# region TestMethods

		/// <summary>
		/// Test method for creating AvailabilityResultService object with valid input
		/// </summary>
		[Test]
		public void TestValidCreation()
		{
			TDDateTime travelDatetime = new TDDateTime(2005, 01, 01, 12, 00, 00);

			//Create AvailabilityResultService object
			AvailabilityResultService service = new AvailabilityResultService("TESTORIGIN", "TESTDESTINATION", travelDatetime, true);

			Assert.AreEqual("TESTORIGIN", service.Origin, "Origin property not set correctly");
			Assert.AreEqual("TESTDESTINATION", service.Destination, "Destination property not set correctly");
			Assert.AreEqual(travelDatetime, service.TravelDatetime, "TravelDatetime property not set correctly");
			Assert.AreEqual(true, service.Available, "Available property not set correctly");
		}

		#endregion

	}
}
