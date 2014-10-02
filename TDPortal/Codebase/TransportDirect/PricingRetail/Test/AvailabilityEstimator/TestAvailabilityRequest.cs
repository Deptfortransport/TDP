// *********************************************** 
// NAME			: TestAvailabilityRequest.cs
// AUTHOR		: James Broome
// DATE CREATED	: 12/01/2005
// DESCRIPTION	: Test implementation of the AvailabilityRequest class
// ************************************************ 

//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/AvailabilityEstimator/TestAvailabilityRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:16   mturner
//Initial revision.
//
//   Rev 1.2   Apr 28 2005 16:36:22   jbroome
//UnavailableProducts now stored by Outward and Return dates
//Resolution for 2302: PT - Product availability does not handle return products adequately.
//
//   Rev 1.1   Mar 18 2005 14:47:04   jbroome
//Added missing class documentation comments (Code Review)
//
//   Rev 1.0   Feb 08 2005 09:56:16   jbroome
//Initial revision.

using System;

using NUnit.Framework;

using TransportDirect.Common;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.UserPortal.PricingRetail.AvailabilityEstimator
{
	/// <summary>
	/// Test class which tests the valid creation of the AvailabilityRequest class
	/// </summary>
	[TestFixture]
	public class TestAvailabilityRequest
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TestAvailabilityRequest()
		{
		}

		#region Test Methods

		/// <summary>
		/// Test method for creating AvailabilityRequest object with valid input
		/// </summary>
		[Test]
		public void TestValidCreation()
		{
			TDDateTime outTravelDate = new TDDateTime(2005, 01, 01);
			TDDateTime retTravelDate = new TDDateTime(2005, 01, 02);
			
			//Create availabilityrequest object with outward and return dates
			AvailabilityRequest request = new AvailabilityRequest(  TicketTravelMode.Rail, 
																	"9100DONC",
																	"9100KNGX",
																	"SVR",
																	outTravelDate,
																	retTravelDate);
			
			Assert.AreEqual(TicketTravelMode.Rail, request.Mode, "Mode property value not set correctly");
			Assert.AreEqual("9100DONC", request.Origin, "Origin property value not set correctly");
			Assert.AreEqual("9100KNGX", request.Destination, "Destination property value not set correctly");
			Assert.AreEqual("SVR", request.TicketCode, "TicketCode property value not set correctly");
			Assert.AreEqual(outTravelDate, request.OutwardTravelDate, "Outward Travel Date property value not set correctly");
			Assert.AreEqual(retTravelDate, request.ReturnTravelDate, "Return Travel Date property value not set correctly");

			//Create availabilityrequest object with just outward date
			request = new AvailabilityRequest(  TicketTravelMode.Rail, 
				"9100DONC",
				"9100KNGX",
				"SVR",
				outTravelDate);
			
			Assert.AreEqual(TicketTravelMode.Rail, request.Mode, "Mode property value not set correctly");
			Assert.AreEqual("9100DONC", request.Origin, "Origin property value not set correctly");
			Assert.AreEqual("9100KNGX", request.Destination, "Destination property value not set correctly");
			Assert.AreEqual("SVR", request.TicketCode, "TicketCode property value not set correctly");
			Assert.AreEqual(outTravelDate, request.OutwardTravelDate, "Outward Travel Date property value not set correctly");
			Assert.AreEqual(null, request.ReturnTravelDate, "Return Travel Date property set incorrectly");

		}

		#endregion

	}
}
