// *********************************************** 
// NAME                 : TestPostcodeSyntaxChecker.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Unit tests for PostcodeSyntaxChecker class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/UnitTest/TestPostcodeSyntaxChecker.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:50   mturner
//Initial revision.
//
//   Rev 1.4   Mar 23 2005 11:55:30   jgeorge
//Updates and corrections to address unit test standards and current failures
//
//   Rev 1.3   Feb 07 2005 15:03:20   RScott
//Assertion changed to Assert
//
//   Rev 1.2   Jan 19 2005 12:08:06   passuied
//new Unit tests for code gaz + made sure old UT still work
//
//   Rev 1.1   Apr 27 2004 13:41:54   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.0   Sep 05 2003 15:30:16   passuied
//Initial Revision


using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Unit tests for PostcodeSyntaxChecker class
	/// </summary>
	[TestFixture]
	public class TestPostcodeSyntaxChecker
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
		public void TestContainsPostcode()
		{
			string message = "Test failed";
			// Test with text with no postcode
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.ContainsPostcode("a;lkjdflajsf flkasjfl aflkjslj"), message);
			
			// Test with text with postcode only in 1 word (eg NW118TJ)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.ContainsPostcode("NW118TJ"), message);
			
			// Test with text with postcode only in 2 words (eg NW11 8TJ)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.ContainsPostcode("N1 8TJ"), message);
		
			// Test with text with postcode in 1 word and text (postcode first) 
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.ContainsPostcode("NW111TJ ajsflsajfl lkfajslafj fjasljf"), message);

			// Test with text with postcode in 2 words and text (postcode at end)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.ContainsPostcode("ajsflsajfl lkfajslafj fjasljf NW111TJ "), message);

			// Test with text with postcode in 2 words and text (postcode in middle)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.ContainsPostcode("ajsflsajfl lkfajslafj N111TJ fjasljf "), message);
		}

		[Test]
		public void TestIsPostcode()
		{
			string message = "Test failed";
			// Test with text with no postcode
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPostCode("a;lkjdflajsf flkasjfl aflkjslj"), message);

			// Test with text with postcode and random text
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPostCode("a;lkjdflajsf NW11 8TJ aflkjslj"), message);
			// Test with text with postcode almost correct (1 digit more in the middle)
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPostCode("NW11 88TJ"), message);

			// Test with text with postcode with space at bad place 
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPostCode("NW1 18TJ"), message);

			
			// Test with text with postcode only in 1 word (eg NW118TJ)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPostCode("NW118TJ"), message);

			// Test with text with postcode only in 1 word (short) (eg N18TJ)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPostCode("NW118TJ"), message);
			
			// Test with text with postcode only in 2 words (eg NW11 8TJ)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPostCode("N1 8TJ"), message);

			// Test with text with 2 postcodes
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPostCode("N1 8TJ SW11 3TL"), message);

		}
		[Test]
		public void TestIsPartPostcode()
		{
			string message = "Test failed";
			// Test with text with no part postcode
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("a;lkjdflajsf flkasjfl aflkjslj"), message);

			// Test with text with part postcode and random text
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("a;lkjdflajsf NW11 aflkjslj"), message);
		
			// Test with text with part postcode almost correct (1 digit more in the middle)
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("NW111"), message);

			// Test with text with postcode only in 1 word (short) (eg N18TJ)
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("NW118TJ"), message);
			
			// Test with text with postcode only in 2 words (eg NW11 8TJ)
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("N1 8TJ"), message);

			// Test with text with part postcode and random text
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("asidha sh SW11 alkas;dl o.;87 "), message);

			// Test with text with part postcode - only outcode (AN)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("W1"), message);
			
			// Test with text with part postcode - only outcode (AAN)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("SW1"), message);
			
			// Test with text with part postcode - only outcode (ANN)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("N12"), message);

			// Test with text with part postcode - only outcode (AANN)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("NW12"), message);
			
			// Test with text with part postcode - only outcode (ANA)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("E1R"), message);

			// Test with text with part postcode - only outcode (AANA)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("SW1Y"), message);

			// Test with text with valid outcode and partial incode (N)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("NW12 3"), message);

			// Test with text with valid outcode and partial incode (A)
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("NW12 E"), message);

			// Test with text with valid outcode and partial incode (NA)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("NW12 3R"), message);
			
			// Test with text with valid outcode and partial incode (NN)
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("NW12 33"), message);

			// Test with text with valid outcode and full incode (NAA) i.e. full postcode
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("NW12 3RT"), message);

			// Test with text with valid outcode and partial incode - no space
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("NW123"), message);
		
			// Test with text with valid outcode and partial incode - no space
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("NW123R"), message);

			// Test with text with valid outcode and partial incode - no space
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("SW1Y5"), message);

			// Test with text with valid outcode and partial incode (A)
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("G1 2"), message);

			// Test with text with valid outcode only
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("G12"), message);

			// Test with text with valid outcode and partial incode
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("G1 2E"), message);

			// Test with text with valid outcode and partial incode -incorrect spacing
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("G12 E"), message);

			// Test with text with valid outcode and partial incode, plus white space
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("G12   2"), message);

			// Test with text with valid outcode and partial incode, plus white space
			Assert.AreEqual(true, 
				PostcodeSyntaxChecker.IsPartPostCode("G12   2N"), message);

			// Test with text with full postcode and text which could be outcode
			Assert.AreEqual(false, 
				PostcodeSyntaxChecker.IsPartPostCode("B1 SW11 3TP"), message);
		}

	}
}
