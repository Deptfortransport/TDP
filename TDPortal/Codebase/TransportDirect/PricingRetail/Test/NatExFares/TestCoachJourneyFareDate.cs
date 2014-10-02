//********************************************************************************
//NAME         : TestCoachJourneyFareDate.cs
//AUTHOR       : James Broome
//DATE CREATED : 22/02/2005
//DESCRIPTION  : Test implementation of CoachJourneyFareDate class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/NatExFares/TestCoachJourneyFareDate.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:30   mturner
//Initial revision.
//
//   Rev 1.0   Mar 23 2005 09:36:56   jbroome
//Initial revision.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier

using System;

using TransportDirect.Common;

using NUnit.Framework;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Test class used to test the creation and public properties/methods
	/// of the CoachJourneyFareDate class.
	/// </summary>
	[TestFixture]	
	public class TestCoachJourneyFareDate
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public TestCoachJourneyFareDate()
		{
		}

		/// <summary>
		/// Tests that a CoachJourneyFareDate can be created successfully
		/// </summary>
		[Test]
		public void TestValidCreation()
		{
			TDDateTime date = new TDDateTime();
			CoachJourneyFareDate fareDate = new CoachJourneyFareDate(date);

			Assert.AreEqual(date, fareDate.Date, "Error in creation of CoachJourneyFareDate with Date");
			Assert.AreEqual(0, fareDate.FareSummary.Length, "Error in creation of CoachJourneyFareDate with FareSummary");

		}

		/// <summary>
		/// Tests the public AddFareSummary method of the CoachJourneyFareDate class
		/// </summary>
		[Test]
		public void TestAddFareSummary()
		{
	
			TDDateTime date = new TDDateTime();
			CoachJourneyFareDate fareDate = new CoachJourneyFareDate(date);
			
			// Create CoachJourneyFareSummary object and add it to FareSummary collection
			CoachJourneyFareSummary summary = new CoachJourneyFareSummary("TEST", "TEST");
			fareDate.AddFareSummary(summary);

			Assert.AreEqual(1, fareDate.FareSummary.Length, "Error in AddFareSummary");


		}
	}
}
