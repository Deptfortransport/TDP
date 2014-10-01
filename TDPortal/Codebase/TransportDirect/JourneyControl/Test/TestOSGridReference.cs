// *********************************************** 
// NAME			: TestOSGridReferences.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the TestOSGridReference class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestOSGridReference.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:18   mturner
//Initial revision.
//
//   Rev 1.2   Feb 07 2005 11:15:56   RScott
//Assertion changed to Assert
//
//   Rev 1.1   Sep 05 2003 15:29:02   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.0   Aug 20 2003 17:55:26   AToner
//Initial Revision
using System;
using NUnit.Framework;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for TestOSGridReference.
	/// </summary>
	[TestFixture]
	public class TestOSGridReference
	{
		public TestOSGridReference()
		{
		}

		[Test]
		public void OSGridReference()
		{
			OSGridReference gridReference = new OSGridReference( 1, 2 );

			Assert.AreEqual(1, gridReference.Easting, "Easting 1");
			Assert.AreEqual(2, gridReference.Northing, "Nothing 1");

			gridReference.Easting = 3;
			gridReference.Northing = 4;

			Assert.AreEqual(3, gridReference.Easting, "Easting 2");
			Assert.AreEqual(4, gridReference.Northing, "Nothing 2");
		}
	}
}
