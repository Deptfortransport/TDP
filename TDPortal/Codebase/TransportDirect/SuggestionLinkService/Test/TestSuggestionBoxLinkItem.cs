// *********************************************** 
// NAME                 : TestLinkTextItem.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 15/08/2005 
// DESCRIPTION			: Unit Test class for SuggestionBoxLinkItem
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/Test/TestSuggestionBoxLinkItem.cs-arc  $
//
//   Rev 1.1   Mar 10 2008 15:28:00   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:50:18   mturner
//Initial revision.
//
//   Rev 1.5   Sep 06 2007 18:17:04   mmodi
//Updated to pass is External link property
//Resolution for 4493: Car journey details: Screen reader improvements
//
//   Rev 1.4   Feb 10 2006 16:20:34   kjosling
//Manually merged for stream 3180
//
//   Rev 1.3   Feb 02 2006 14:46:48   kjosling
//Fixed unit tests
//
//   Rev 1.2   Sep 02 2005 15:34:14   kjosling
//Updated following code review
//
//   Rev 1.1   Aug 31 2005 18:11:58   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 24 2005 16:47:14   kjosling
//Initial revision.

using System;
using NUnit.Framework;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
//using TransportDirect.Web.Support;
using System.Globalization;
using System.Collections;

namespace TransportDirect.UserPortal.SuggestionLinkService.Test
{
	[TestFixture]
	public class TestSuggestionBoxLinkItem
	{
		public const string LINKTEXT1 = "Find other ways of getting from {OriginLocation} to {DestinationLocation} here";
		public const string LINKTEXT2 = "Trouvez d'autres moyens d'obtenir {OriginLocation} {DestinationLocation} d'ici";
		public const string URL1 = "JourneyPlanning/JourneySummary.aspx";
		public const string CULTURE1 = "en-GB";
		public const string CULTURE2 = "fr-FR";

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
		/// Ensures a SuggestionBoxLinkItem object can be created successfully
		/// </summary>
		[Test]
		public void TestObjectCreation()
		{
			LinkTextItem l1 = new LinkTextItem(LINKTEXT1);

			Hashtable linkItems = new Hashtable();
			linkItems.Add(CULTURE1, l1);

			ILinkDetails linkDetails = new InternalLinkDetail(URL1, true);

			SuggestionBoxLinkItem s1 = new SuggestionBoxLinkItem(
				1234,
				1,
				"category1",
				3,
				true,
				linkItems,
				linkDetails,
				false,false,0,1);

			Assert.IsTrue(s1.Category == "category1");
			Assert.IsTrue(s1.CategoryPriority == 1);
			Assert.IsTrue(s1.LinkPriority == 3);
			Assert.IsTrue(s1.SuggestionLinkId == 1234);
		}

		/// <summary>
		/// Tests to see whether the SuggestionBoxLinkItem can correctly retirieve links based
		/// on the current UI culture
		/// </summary>
		[Test]
		public void TestGetLinkText()
		{
			LinkTextItem l1 = new LinkTextItem(LINKTEXT1);
			LinkTextItem l2 = new LinkTextItem(LINKTEXT2);

			Hashtable linkItems = new Hashtable();
			linkItems.Add(CULTURE1, l1);
			linkItems.Add(CULTURE2, l2);

			ILinkDetails linkDetails = new InternalLinkDetail(URL1, true);

			System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(CULTURE1);

			SuggestionBoxLinkItem s1 = new SuggestionBoxLinkItem(
				1234,
				1,
				"category1",
				3,
				true,
				linkItems,
				linkDetails,
				false,false,0,1);

			string result1 = s1.GetLinkText();
			Assert.IsTrue(result1 != String.Empty);
			Console.WriteLine(result1);

			System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(CULTURE2);

			string result2 = s1.GetLinkText();
			Console.WriteLine(result2);

			Assert.IsTrue(result2 != String.Empty);
			Assert.IsTrue(result1 != result2);
		}

		/// <summary>
		/// Tests to see if the object correctly throws an error when it is asked to produce link
		/// text for a culture that is not supported by this SuggestionBoxLinkItem
		/// </summary>
		[Test]
		public void TestUnsupportedCulture()
		{
			LinkTextItem l1 = new LinkTextItem(LINKTEXT1);

			Hashtable linkItems = new Hashtable();
			linkItems.Add(CULTURE1, l1);

			ILinkDetails linkDetails = new InternalLinkDetail(URL1, true);

			System.Threading.Thread.CurrentThread.CurrentUICulture = new CultureInfo(CULTURE2);

			SuggestionBoxLinkItem s1 = new SuggestionBoxLinkItem(
				1234,
				1,
				"category1",
				3,
				true,
				linkItems,
				linkDetails,
				false,false,0,1);	
		
			string result = s1.GetLinkText();
			Assert.IsTrue(result == String.Empty, "Expected empty string because this culture is not currently supported");
		}

		[Test]
		public void TestIComparableCompliance()
		{
			LinkTextItem l1 = new LinkTextItem(LINKTEXT1);

			Hashtable linkItems = new Hashtable();
			linkItems.Add(CULTURE1, l1);

			ILinkDetails linkDetails = new InternalLinkDetail(URL1, true);

			System.Collections.ArrayList listOfItems = new System.Collections.ArrayList();

			listOfItems.Add(
				new SuggestionBoxLinkItem(1234,3,"category3",1, true,linkItems,linkDetails, false,false,0,1));		
			listOfItems.Add(
				new SuggestionBoxLinkItem(5678,1,"category1",1, true,linkItems,linkDetails, false,false,0,1));		
			listOfItems.Add(
				new SuggestionBoxLinkItem(9012,2,"category2",5, true,linkItems,linkDetails, false,false,0,1));	
			listOfItems.Add(
				new SuggestionBoxLinkItem(3456,2,"category2",2, true,linkItems,linkDetails, false,false,0,1));	
			listOfItems.Add(
				new SuggestionBoxLinkItem(7890,3,"category3",2, true,linkItems,linkDetails, false,false,0,1));	

			listOfItems.Sort();

			for(int i = 0; i < listOfItems.Count; i++)
			{
				SuggestionBoxLinkItem item = (SuggestionBoxLinkItem)listOfItems[i];
				if(i > 0)
				{
					SuggestionBoxLinkItem item2 = (SuggestionBoxLinkItem)listOfItems[i -1];
					Assert.IsTrue(item.CategoryPriority >= item2.CategoryPriority);
					if(item2.Category == item.Category)
					{
						Assert.IsTrue(item.LinkPriority >= item2.LinkPriority);
					}
					Console.WriteLine(item.SuggestionLinkId.ToString() + " " + item.CategoryPriority + " " + item.LinkPriority.ToString());
				}
			}
		}

		[Test]
		public void TestEquals()
		{
			LinkTextItem l1 = new LinkTextItem(LINKTEXT1);

			Hashtable linkItems = new Hashtable();
			linkItems.Add(CULTURE1, l1);

			ILinkDetails linkDetails = new InternalLinkDetail(URL1, true);

			System.Collections.ArrayList listOfItems = new System.Collections.ArrayList();

			SuggestionBoxLinkItem s1 = new SuggestionBoxLinkItem
				(1234,3,"category3",1,true,linkItems,linkDetails, false,false,0,1);

			SuggestionBoxLinkItem s2 = new SuggestionBoxLinkItem
				(9876,1,"category1",2,true,linkItems,linkDetails, false,false,0,1);

			SuggestionBoxLinkItem s3 = new SuggestionBoxLinkItem
				(1234,2,"category2",5,true,linkItems,linkDetails, false,false,0,1);

			listOfItems.Add(s1);
			Assert.IsTrue(listOfItems.Contains(s3));
			Assert.IsFalse(listOfItems.Contains(s2)); 
		}

	}
}
