// *********************************************** 
// NAME                 : TestLocationChoiceComparer.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 01/09/2003 
// DESCRIPTION  : Test for LocationChoiceComparer class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestLocationChoiceComparer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:46   mturner
//Initial revision.
//
//   Rev 1.4   Mar 23 2005 11:55:30   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.3   Feb 07 2005 15:03:20   RScott
//Assertion changed to Assert
//
//   Rev 1.2   Jan 19 2005 12:07:40   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.1   Oct 08 2003 10:44:50   passuied
//implemented detection of ADMINAREA choice to avoid exception when trying to get location details
//
//   Rev 1.0   Sep 05 2003 15:30:10   passuied
//Initial Revision


using System;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Test for LocationChoiceComparer class
	/// </summary>
	[TestFixture]
	public class TestLocationChoiceComparer
	{
		[TestFixtureSetUp]
		public void SetUp()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialisation());
		}

		[TestFixtureTearDown]
		public void TearDown()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
		}

		[Test]
		public void TestCompare()
		{
			string message = "Test failed";
			LocationChoiceComparer tool = new LocationChoiceComparer();

			// Location choice1 score > Location choice2 score
			LocationChoice choice1 = new LocationChoice("description", false, "","", new OSGridReference(), "", 100.00, "", "",false);
			LocationChoice choice2 = new LocationChoice("description", false, "","", new OSGridReference(), "", 99.99, "", "",false);
			int result = tool.Compare(choice1, choice2);
			Assert.IsTrue(result <0, message);

			// Location choice1 score < Location choice2 score
			choice1 = new LocationChoice("description", false, "","", new OSGridReference(), "", 99.99, "", "",false);
			choice2 = new LocationChoice("description", false, "","", new OSGridReference(), "", 100.00, "", "",false);
			result = tool.Compare(choice1, choice2);
			Assert.IsTrue(result >0, message);


			// Location choice1 score = Location choice2 score,
			// Location choice1 description < Location choice2 description
			choice1 = new LocationChoice("description1", false, "","", new OSGridReference(), "", 99.99, "", "",false);
			choice2 = new LocationChoice("description2", false, "","", new OSGridReference(), "", 99.99, "", "",false);
			result = tool.Compare(choice1, choice2);
			Assert.IsTrue(result <0, message);

			// Location choice1 score = Location choice2 score,
			// Location choice1 description > Location choice2 description
			choice1 = new LocationChoice("description2", false, "","", new OSGridReference(), "", 99.99, "", "",false);
			choice2 = new LocationChoice("description1", false, "","", new OSGridReference(), "", 99.99, "", "",false);
			result = tool.Compare(choice1, choice2);
			Assert.IsTrue(result >0, message);

			// Location choice1 score = Location choice2 score,
			// Location choice1 description = Location choice2 description
			choice1 = new LocationChoice("description", false, "","", new OSGridReference(), "", 99.99, "", "",false);
			choice2 = new LocationChoice("description", false, "","", new OSGridReference(), "", 99.99, "", "",false);
			result = tool.Compare(choice1, choice2);
			Assert.IsTrue(result == 0, message);

		}

		[Test]
		public void TestException()
		{
			string obj1 = "hello";
			string obj2 = "hello";
			bool exceptionThrown = false;

			try
			{
				LocationChoiceComparer tool = new LocationChoiceComparer();
				tool.Compare(obj1, obj2);
			}
			catch (TDException)
			{
				exceptionThrown = true;
			}

			Assert.IsTrue(exceptionThrown == true, "Test Failed!");
		}
	}
}
