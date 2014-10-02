// *********************************************** 
// NAME                 : TestLinkTextItem.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 15/08/2005 
// DESCRIPTION			: Unit Test class for LinkTextItem
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/Test/TestLinkTextItem.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:16   mturner
//Initial revision.
//
//   Rev 1.3   Feb 02 2006 14:44:56   kjosling
//Fixed unit test
//
//   Rev 1.2   Sep 02 2005 15:33:42   kjosling
//Updated following code review
//
//   Rev 1.1   Aug 31 2005 18:11:48   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 24 2005 16:47:14   kjosling
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;

namespace TransportDirect.UserPortal.SuggestionLinkService.Test
{
	[TestFixture]
	public class TestLinkTextItem
	{
		public const string TEST_STRING_1 = @"Find other ways of getting from {OriginLocation} to {DestinationLocation}";
		public const string TEST_STRING_2 = @"This link has no substitute parameters";
		public const string TEST_STRING_3 = @"This link has invalid syntax {{OriginLocation} DestinationLocation} so please deal with it";
		public const string TEST_STRING_4 = @"This link has an an {unknown} substitution parameter";
		public const string TEST_STRING_5 = @"This link has an an {TestUnhandled} substitution parameter, which is not handled by the LinkTextItem class";

		[SetUp]
		public void Init()
		{		
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestSessionManagerInitialisation());
			TDSessionManager.Current.ItineraryMode = ItineraryManagerMode.ExtendJourney;
			TDSessionManager.Current.ItineraryManager.JourneyRequest = new TestStubJourneyRequest();
		}

		[TearDown]
		public void CleanUp()
		{
		}

		/// <summary>
		/// </summary>
		[Test]
		public void TestValidPlaceholder()
		{
			LinkTextItem l1 = new LinkTextItem(TEST_STRING_1);
			string str1 = l1.GetSubstitutedLinkText();
			Assert.IsTrue(str1.Length > 0 && str1.Length != TEST_STRING_1.Length);
			Console.WriteLine(str1);
		}

		[Test]
		public void TestInvalidPlaceholder()
		{
			bool success = false;
			try
			{
				LinkTextItem l1 = new LinkTextItem(TEST_STRING_4);
			}
			catch(TransportDirect.Common.TDException td)
			{
				success = true;
				Console.WriteLine(td.Message);
			}
			Assert.IsTrue(success);
		}

		[Test]
		public void TestNoPlaceholder()
		{
			LinkTextItem l1 = new LinkTextItem(TEST_STRING_2);
			string str1 = l1.GetSubstitutedLinkText();
			Assert.AreEqual(str1, TEST_STRING_2);
			Console.WriteLine(str1);
		}

		[Test]
		public void TestUnhandledPlaceholder()
		{
			bool success = false;
			LinkTextItem l1 = new LinkTextItem(TEST_STRING_5);
			try
			{
				string str1 = l1.GetSubstitutedLinkText();
			}
			catch(TransportDirect.Common.TDException td)
			{
				success = true;
				Console.WriteLine(td.Message);
			}
			Assert.IsTrue(success);
		}

		[Test]
		public void TestInvalidLinkTextSyntax()
		{
			bool success = false;
			LinkTextItem l1 = new LinkTextItem(TEST_STRING_3);
			try
			{
				string str1 = l1.GetSubstitutedLinkText();
			}
			catch(TransportDirect.Common.TDException td)
			{
				success = true;
				Console.WriteLine(td.Message);
			}
			Assert.IsTrue(success);
		}

	}
}
