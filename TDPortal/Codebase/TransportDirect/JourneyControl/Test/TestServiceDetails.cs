// *********************************************** 
// NAME			: TestServiceDetails.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestServiceDetails class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestServiceDetails.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:20   mturner
//Initial revision.
//
//   Rev 1.3   Aug 25 2005 14:41:16   RPhilpott
//Pass Retail Train Id to RVBO in place of UID.
//Resolution for 2710: NRS interface -- retail train id needed
//
//   Rev 1.2   Feb 07 2005 11:16:04   RScott
//Assertion changed to Assert
//
//   Rev 1.1   Oct 15 2003 21:55:38   acaunt
//Destinations added to the leg data
//
//   Rev 1.0   Aug 20 2003 17:55:30   AToner
//Initial Revision
using System;
using NUnit.Framework;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestServiceDetails.
	/// </summary>
	[TestFixture]
	public class TestServiceDetails
	{
		public TestServiceDetails()
		{
		}

		[Test]
		public void ServiceDetails()
		{
			ServiceDetails serviceDetails = new ServiceDetails( "OpCode", "OpName", "ServiceNum", "DestinationBoard",
				"Direction", "PrivateId", "Retail");

			Assert.AreEqual( "OpCode", serviceDetails.OperatorCode, "Operator Code" );
			Assert.AreEqual( "OpName", serviceDetails.OperatorName, "Operator Name" );
			Assert.AreEqual( "ServiceNum", serviceDetails.ServiceNumber, "Service Number" );
			Assert.AreEqual( "DestinationBoard", serviceDetails.DestinationBoard, "Destination Board" );
			Assert.AreEqual( "Direction", serviceDetails.Direction, "Direction" );
			Assert.AreEqual( "PrivateId", serviceDetails.PrivateId, "Private Id" );
			Assert.AreEqual( "Retail", serviceDetails.RetailTrainId, "Retail Train Id" );
		}

	}
}
