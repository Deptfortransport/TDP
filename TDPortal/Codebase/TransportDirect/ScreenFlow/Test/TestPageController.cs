// *********************************************** 
// NAME                 : TestPageController.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : NUnit class to test
// PageController class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Test/TestPageController.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:56   mturner
//Initial revision.
//
//   Rev 1.11   May 23 2005 10:03:02   rscott
//Updated for NUnit Tests
//
//   Rev 1.10   Feb 08 2005 10:18:44   RScott
//Assertion changed to Assert
//
//   Rev 1.9   Jun 29 2004 15:35:32   esevern
//moved TestGetJourneyParameterType to TestPageTransferDataCache
//
//   Rev 1.8   Jun 29 2004 13:37:48   esevern
//added TestGetJourneyParameterType
//
//   Rev 1.7   Sep 16 2003 14:07:14   jcotton
//1.	Added to the PageID enumeration the following PageIDs:
//·	InitialPage
//·	JourneyPlannerInput
//·	JourneyPlannerAmbiguity
//·	JourneySummary
//·	JourneyPlannerLocationMap
//·	JourneyDetails
//·	JourneyMaps
//·	JourneyAdjust
//·	CompareAdjustedJourney
//·	DetailedLegMap
//·	WaitPage
//·	PrintableJourneySummary
//·	PrintableJourneyDetails
//·	PrintableJourneyMaps
//·	PrintableCompareAdjustedJourney
//·	Feedback
//·	Links
//·	Help
//·	GeneralMaps
//·	SiteMap
//2.	Updated PageTransferDetails.xml and PageTransferEvents.xml.
//3.	Corrected "Test Pages" that referenced PageID enumerations no longer used.
//
//   Rev 1.6   Aug 15 2003 14:36:56   passuied
//Update after design change
//
//   Rev 1.5   Jul 25 2003 12:32:42   kcheung
//Changed Urls to TestXXX.aspx
//
//   Rev 1.4   Jul 23 2003 15:27:54   kcheung
//Updated to take into account assembly name change.
//
//   Rev 1.3   Jul 23 2003 13:28:36   kcheung
//Changed $log to $Log

using System;
using NUnit.Framework;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using System.Collections;
using Messages = TransportDirect.UserPortal.ScreenFlow.Messages;

namespace TransportDirect.UserPortal.ScreenFlow_NUnit
{
	/// <summary>
	/// NUnit class to test PageController class.
	/// </summary>
	[TestFixture]
	public class TestPageController
	{
		/// <summary>
		/// Sets up the discovery service.
		/// </summary>
		[SetUp]
		public void SetupDiscoveryService()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialisation());
			// Point to the mock properties where the all properties are valid.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService, new TestMockGoodProperties());

			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.SessionManager,
				new SessionManager.SessionManagerFactory());
			}

		/// <summary>
		/// Test the construction of the PageController and check that
		/// no exceptions have been thrown.
		/// </summary>
		[Test]
		public void TestPageControllerConstruction()
		{
			bool noExceptionThrown = true;

			try
			{			
				PageTransferDataCache dataCache = new PageTransferDataCache();
				PageController pageController = new PageController(dataCache);
			}
			catch(ScreenFlowException)
			{
				noExceptionThrown = false;
			}
			catch(Exception)
			{
				noExceptionThrown = false;
			}

			// assert that no exception has been thrown.
			Assert.IsTrue(noExceptionThrown);
		}

		// ----------------------------------------------------------

		/// <summary>
		/// Test the GetPageTransferDetails method to ensure that
		/// the correct PageTransferDetails are returned.
		/// </summary>
		[Test]
		public void TestGetPageTransferDetails()
		{
			PageTransferDataCache dataCache = new PageTransferDataCache();
			PageController pageController = new PageController(dataCache);
			
			PageTransferDetails pageTransferDetails =
				pageController.GetPageTransferDetails(PageId.InitialPage);

			// Test that the pageTransferDetails is correct
			string message = "PageTransferDetails for PageId.InitialPage is incorrect.";

			// Perform assertions against the pageTransferDetails object
			// returned to check that no errors exist.
			Assert.AreEqual
				(PageId.InitialPage, pageTransferDetails.PageId, message);
			Assert.AreEqual
				("JourneyPlanning/TestPopulateTDSessionManager.aspx", pageTransferDetails.PageUrl, message);
			Assert.AreEqual
				(PageId.InitialPage, pageTransferDetails.BookmarkRedirect, message);

			// Added to test that the specificStateClass is returned properly
			Assert.AreEqual
				(false,
				pageTransferDetails.SpecificStateClass, message);

			// Test another pageTransferDetails
			pageTransferDetails =
				pageController.GetPageTransferDetails(PageId.InitialPage);

			message = "PageTransferDetails for PageId.InitialPage is incorrect.";

			// Check that the pageTransferDetails is correct.
			Assert.AreEqual
				(PageId.InitialPage, pageTransferDetails.PageId, message);
			Assert.AreEqual
				("JourneyPlanning/TestPopulateTDSessionManager.aspx", pageTransferDetails.PageUrl, message);
			Assert.AreEqual
				(PageId.InitialPage, pageTransferDetails.BookmarkRedirect, message);


			// Added to test that the specificStateClass is returned properly
			Assert.AreEqual
				(false,
				pageTransferDetails.SpecificStateClass, message);
		}

		// ----------------------------------------------------------

		/// <summary>
		/// Test the GetNextPageId method.
		/// </summary>
		[Test]
		public void TestGetNextPageIdNotTransferred()
		{
			PageTransferDataCache dataCache = new PageTransferDataCache();
			PageController pageController = new PageController(dataCache);

			// Retrive the session from Service Discovery
			ITDSessionManager session = 
				(ITDSessionManager)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

			// Initialise the transferred flag in the session to FALSE 
			session.Session[SessionKey.Transferred] = false;

			PageId query = PageId.InitialPage;

			// The DoTransition() method in the State Management Class
			// for InitialPage returns PageId.InitialPage, so would expect
			// GetNextPageId to return PageId.InitialPage.

			PageId result = pageController.GetNextPageId(query);
			Assert.AreEqual(PageId.InitialPage, result);

			// The GetNextxPageId() method should also write the
			// result to the session.  Test that this has been done.
			string message = "PageId.InitialPage was not written to the session.";
			Assert.AreEqual
				(session.Session[SessionKey.NextPageId], PageId.InitialPage, message);
		}

		// ----------------------------------------------------------

		/// <summary>
		/// Test the GetNextPageId method.
		/// </summary>
		[Test]
		public void TestGetNextPageIdTransferred()
		{
			PageTransferDataCache dataCache = new PageTransferDataCache();
			PageController pageController = new PageController(dataCache);

			// Retrive the session from Service Discovery
			ITDSessionManager session = 
				(ITDSessionManager)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

			// Initialise the transferred flag in the session to TRUE 
			session.Session[SessionKey.Transferred] = true;

			PageId query = PageId.InitialPage;

			// Since the "Transferred" flag in the session is set to TRUE,
			// the GetNextPageId() should simply return the current page.
			PageId result = pageController.GetNextPageId(query);
			Assert.AreEqual(PageId.InitialPage, result);

			// Check that the "transferred" flag in the session has been set to false.
			Assert.AreEqual(false, session.Session[SessionKey.Transferred]);

			// Check that the expected next page has been written to the
			// Session correctly.
			Assert.AreEqual(PageId.InitialPage, result,
				"PageId.InitialPage was not written to the session.");
		}

		// ----------------------------------------------------------

		/// <summary>
		/// Test the GetNextPageId method.
		/// </summary>
		[Test]
		public void TestGetNextPageIdDefaultState1()
		{
			// Retrive the session from Service Discovery
			ITDSessionManager session = 
				(ITDSessionManager)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

			// initialise by clearing the formshift area
			session.FormShift.Clear();

			PageTransferDataCache dataCache = new PageTransferDataCache();
			PageController pageController = new PageController(dataCache);

			// Initialise the transferred flag in the session to FALSE 
			session.Session[SessionKey.Transferred] = false;

			PageId query = PageId.InitialPage;

			// Write the default TransitonEvent to the formShift in the session.
			session.FormShift[SessionKey.TransitionEvent] = TransitionEvent.Default;

			// PageId.InitialPage uses the default state class.  The DoTransition() method
			// of the DefaultState class should always return the current page
			// if the transitionEvent in the formShift is set to Default.
			// Therefore, PageId.InitialPage should be returned.
			PageId result = pageController.GetNextPageId(query);
			Assert.AreEqual(PageId.InitialPage, result);

			// Check that ht "transferred" flag in the session has been set to false.
			// (The flag should be false because the same page was returned.)
			Assert.AreEqual(false, session.Session[SessionKey.Transferred]);

			// Check that the expected next page has been written to the
			// Session correctly.
			Assert.AreEqual(PageId.InitialPage, result,
				"PageId.InitialPage was not written to the session.");

			// cleaup by clearing the FormShift
			session.FormShift.Clear();
		}

		// ----------------------------------------------------------

		/// <summary>
		/// Test the GetNextPageId method.
		/// </summary>
		[Test]
		public void TestGetNextPageIdDefaultState2()
		{
			// Retrive the session from Service Discovery
			ITDSessionManager session = 
				(ITDSessionManager)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
			
			// initialise by clearing the formshift area
			session.FormShift.Clear();
			
			// Make sure that the service discovery creates the pageController,
			// otherwise the DefaultState class will fail since the service
			// has not been started up.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PageController, new PageControllerFactory());
			
			IPageController pageController = (PageController)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];

			// Initialise the transferred flag in the session to FALSE 
			session.Session[SessionKey.Transferred] = false;

			PageId query = PageId.InitialPage;

			// Write the "GoHome" TransitonEvent to the formShift in the session.
			session.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoHome;

			// Since the TransitionEvent is not set to default, the DoTransition
			// method shoud look in the FormShift and return the PageId associated
			// with the TransitionEvent.  In this case, the TransitionEvent 'GoHome'
			// is associated with PageId.Home.
			PageId result = pageController.GetNextPageId(query);
			Assert.AreEqual(PageId.Home, result);

			// Check that ht "transferred" flag in the session has been set to false.
			// (The flag should be false because the same page was returned.)
			Assert.AreEqual(true, session.Session[SessionKey.Transferred]);

			// Check that the expected next page has been written to the
			// Session correctly.
			Assert.AreEqual(PageId.Home, result, 
				"PageId.InitialPage was not written to the session.");

			// clean-up by clearing the FromShift
			session.FormShift.Clear();
		}

		//Manual set up instructions
		//a) In order for these test pages to function the PageID.cs file in the common project 
		//	 must be altered to contain the test pageIds.
		//
		//	 


	}
}
