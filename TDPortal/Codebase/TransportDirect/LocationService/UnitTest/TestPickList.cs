// *********************************************** 
// NAME                 : TestPickList.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 02/09/2003 
// DESCRIPTION  : Test for PickList class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestPickList.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:48   mturner
//Initial revision.
//
//   Rev 1.2   Mar 23 2005 11:55:30   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.1   Feb 07 2005 15:03:20   RScott
//Assertion changed to Assert
//
//   Rev 1.0   Sep 05 2003 15:30:14   passuied
//Initial Revision


using System;
using NUnit.Framework;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Test for Picklist class
	/// </summary>
	[TestFixture]
	public class TestPickList
	{

		[Test]
		public void TestCurrent()
		{
			PickList pickList = new PickList();

			Assert.AreEqual("<PICKLIST_CRITERIA></PICKLIST_CRITERIA>",
				pickList.Current, "Test has Failed");

			pickList.Add("field", "value");

			Assert.AreEqual("<PICKLIST_CRITERIA><CRITERIA><FIELD>field</FIELD><VALUE>value</VALUE></CRITERIA></PICKLIST_CRITERIA>",
				pickList.Current, "Test has Failed");


			pickList = new PickList(pickList.Current);

			Assert.AreEqual("<PICKLIST_CRITERIA><CRITERIA><FIELD>field</FIELD><VALUE>value</VALUE></CRITERIA></PICKLIST_CRITERIA>",
				pickList.Current, "Test has Failed");

			pickList.Clear();


			Assert.AreEqual("<PICKLIST_CRITERIA></PICKLIST_CRITERIA>",
				pickList.Current, "Test has Failed");
				
		}

		[Test]
		public void TestAdd()
		{
		}

		[Test]
		public void TestRemoveLast()
		{
		}

		[Test]
		public void TestClear()
		{
		}


	}
}
