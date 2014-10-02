// *********************************************** 
// NAME                 : TestStringHelper.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 04/09/2003 
// DESCRIPTION  : Test for StringHelper class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestStringHelper.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:50   mturner
//Initial revision.
//
//   Rev 1.2   Mar 23 2005 11:55:32   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.1   Feb 07 2005 15:03:20   RScott
//Assertion changed to Assert
//
//   Rev 1.0   Sep 05 2003 15:30:18   passuied
//Initial Revision

using System;
using NUnit.Framework;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Test for StringHelper class
	/// </summary>
	[TestFixture]
	public class TestStringHelper
	{
		[Test]
		public void TestGetMoreFrequentWord()
		{
			string[] words = new string[]
				{
					"bonjour",
					"hello",
					"bonjour",
					"hello",
					"hello",
					"pourquoi",
					"why",
					"shalom",
					"hello",
					"bonjour"
				};

			Assert.AreEqual("hello", StringHelper.GetMoreFrequentWord(words), "TestFailed");
		}
	}
}
