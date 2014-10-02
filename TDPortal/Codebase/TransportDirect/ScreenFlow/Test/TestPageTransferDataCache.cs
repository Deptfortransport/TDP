// *********************************************** 
// NAME                 : TestPageTransferDataCache.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : NUnit test for the
// PageTransferDataCache class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Test/TestPageTransferDataCache.cs-arc  $ 
//
//   Rev 1.1   May 06 2008 16:05:58   mmodi
//Added PageGroup nunit tests
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.0   Nov 08 2007 12:47:56   mturner
//Initial revision.
//
//   Rev 1.21   Oct 06 2006 13:18:50   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.20.1.0   Aug 07 2006 10:42:34   esevern
//updated to include new find car park pages
//
//   Rev 1.20   Apr 05 2006 15:17:44   mdambrine
//Manual merge from stream0030
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.19   Mar 28 2006 16:17:40   AViitanen
//Unit test fix.
//
//   Rev 1.18   Mar 27 2006 16:07:56   kjosling
//Fixed unit tests. I'll catch up with Marcus and Esther soon!
//
//   Rev 1.17   Mar 15 2006 16:43:36   mtillett
//Fix up unit tests
//
//   Rev 1.16   Mar 09 2006 11:45:26   esevern
//updated TestGetPageTransferDetailsWithGoodData() to include new page entries: HomePageFindAJourneyPlanner and HomePageFindAPlacePlanner
//Resolution for 3586: Additional PageEntryEvents on HomePage
//
//   Rev 1.15   Feb 14 2006 12:47:24   tolomolaiye
//Updated to fix unit test scripts
//
//   Rev 1.14   Feb 06 2006 15:34:26   mtillett
//Fixup unit tests
//
//   Rev 1.13   Feb 02 2006 10:20:28   mtillett
//Correct test for new number of PageId
//
//   Rev 1.12   May 23 2005 10:03:02   rscott
//Updated for NUnit Tests
//
//   Rev 1.11   Feb 07 2005 09:55:38   RScott
//Assertion changed to Assert
//
//   Rev 1.10   Jul 15 2004 09:50:54   jgeorge
//Removal of JourneyParametersType from screenflow
//
//   Rev 1.9   Jun 29 2004 15:35:52   esevern
//moved TestGetJourneyParameterType from  TestPageController
//
//   Rev 1.8   Oct 03 2003 13:38:52   PNorell
//Updated the new exception identifier.
//
//   Rev 1.7   Sep 16 2003 14:07:16   jcotton
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
//   Rev 1.5   Jul 25 2003 12:32:26   kcheung
//Changed Urls to TestXXX.aspx
//
//   Rev 1.4   Jul 23 2003 15:27:44   kcheung
//Updated to take into account assembly name change.
//
//   Rev 1.3   Jul 23 2003 13:28:40   kcheung
//Changed $log to $Log

using System;
using NUnit.Framework;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService;
//using TransportDirect.Common.PropertyService.Providers;
using TransportDirect.Common.ServiceDiscovery;
using System.Collections;
using Messages = TransportDirect.UserPortal.ScreenFlow.Messages;

namespace TransportDirect.UserPortal.ScreenFlow_NUnit
{
	/// <summary>
	/// NUnit test class for PageController.
	/// </summary>
	[TestFixture]
	public class TestPageTransferDataCache
	{		
		[SetUp]
		public void SetupDiscoveryService()
		{
			TDServiceDiscovery.ResetServiceDiscoveryForTest();
			TDServiceDiscovery.Init(new TestInitialisation());
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the GetPageTransferDetails method of the PageTransferDataCache
		/// class.
		/// </summary>
		[Test]
		public void TestGetPageTransferDetailsWithGoodData()
		{
			// Point to the mock properties where the all properties are valid.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockGoodProperties());

			// Instantiate a new instance of Page TransferData Cache
			// If validation fails then a ScreenFlowException will be thrown.
			// This should never happen since for this construction, good
			// data is used to initialise the cache.
			PageTransferDataCache pageTransferDataCache =
				new PageTransferDataCache();
			
			// Get all the values of the PageId enumerated type
			PageId[] pageIds = (PageId[])Enum.GetValues(typeof(PageId));

			// Expect 161 values to be returned. (plus 1 for Empty = -1)
			Assert.AreEqual(161, pageIds.Length, "Number of PageIds did not match.");

			PageTransferDetails pageTransferDetails;
			// For each pageId that exists in the enumeration, call
			// the GetPageTransferDetails method to get the PageTransferDetails.

			pageTransferDetails =
				pageTransferDataCache.GetPageTransferDetails(pageIds[0]);
			string message = "Failed testing for " + pageIds[0];

			// -----------------------------------------------------
			// Perform assertions against the PageTransferDetails object
			// returned to ensure that the correct object has been returned.
			Assert.AreEqual(PageId.InitialPage, pageTransferDetails.PageId,message);
			Assert.AreEqual("JourneyPlanning/TestPopulateTDSessionManager.aspx", pageTransferDetails.PageUrl,message);
			Assert.AreEqual(PageId.InitialPage, pageTransferDetails.BookmarkRedirect,message);
			// -----------------------------------------------------

			// Perform assertions against the PageTransferDetails object
			// returned to ensure that the correct object has been returned.
			pageTransferDetails =
				pageTransferDataCache.GetPageTransferDetails(pageIds[1]);
			message = "Failed testing for " + pageIds[1];

			Assert.AreEqual(PageId.JourneyPlannerInput, pageTransferDetails.PageId,message);
			Assert.AreEqual("JourneyPlanning/JourneyPlannerInput.aspx", pageTransferDetails.PageUrl,message);
			Assert.AreEqual(PageId.JourneyPlannerInput, pageTransferDetails.BookmarkRedirect,message);
			// -----------------------------------------------------

			// Perform assertions against the PageTransferDetails object
			// returned to ensure that the correct object has been returned.
			pageTransferDetails =
				pageTransferDataCache.GetPageTransferDetails(pageIds[2]);
			message = "Failed testing for " + pageIds[2];

			Assert.AreEqual(PageId.JourneyPlannerAmbiguity, pageTransferDetails.PageId,message);
			Assert.AreEqual("JourneyPlanning/JourneyPlannerAmbiguity.aspx", pageTransferDetails.PageUrl,message);
			Assert.AreEqual(PageId.JourneyPlannerInput, pageTransferDetails.BookmarkRedirect,message);
			// -----------------------------------------------------

			// Perform assertions against the PageTransferDetails object
			// returned to ensure that the correct object has been returned.
			pageTransferDetails =
				pageTransferDataCache.GetPageTransferDetails(pageIds[3]);
			message = "Failed testing for " + pageIds[3];

			Assert.AreEqual(PageId.JourneySummary, pageTransferDetails.PageId,message);
			Assert.AreEqual("JourneyPlanning/JourneySummary.aspx", pageTransferDetails.PageUrl,message);
			Assert.AreEqual(PageId.JourneyPlannerInput, pageTransferDetails.BookmarkRedirect,message);
			// -----------------------------------------------------

			// Perform assertions against the PageTransferDetails object
			// returned to ensure that the correct object has been returned.
			pageTransferDetails =
				pageTransferDataCache.GetPageTransferDetails(pageIds[4]);
			message = "Failed testing for " + pageIds[4];
			
			Assert.AreEqual(PageId.JourneyPlannerLocationMap, pageTransferDetails.PageId,message);
			Assert.AreEqual("Maps/JourneyPlannerLocationMap.aspx", pageTransferDetails.PageUrl,message);
			Assert.AreEqual(PageId.JourneyPlannerLocationMap, pageTransferDetails.BookmarkRedirect,message);
		}
		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorMissingPageId()
		{
			// Point to mock properties where the filename of the
			// PageTransferDetails xml file is set to a file where
			// a PageId is missing.

			// e.g. The PageId enumeration has 5 PageId values, however,
			// the Xml file will only contain definitions for 4 of these
			// pageIds.

			// Expect a ScreenFlowException to be thrown with a message
			// indicating that the incorrect number of pageIds was found
			// in the Xml file.
			
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesMissingPageIdsInXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test the exception has the correct ID
				Assert.AreEqual(TDExceptionIdentifier.SFMArrayEntryValidationFailed, sfe.Identifier, "Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown,"Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorMissingPageTransitionEvent()
		{		
			// Point to the mock properties object where there are missing
			// transition events in the Xml.  (This means that when the
			// Xml is loaded from the PageTransferDataCache, there will be 
			// a missing TransitionEvent - and hence validation should fail.)
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesMissingTransitionEventsInXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMArrayEntryValidationFailed2, sfe.Identifier, "Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown,"Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorExtraPageId()
		{
			// Point to mock properties where the filename of the
			// PageTransferDetails xml file is set to a file where
			// an extra PageId exists.  This extra pageId should
			// cause the validation routine called by the constructor
			// of PageTransferDataCache to fail and throw a ScreenFlowException.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesExtraPageIdInXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMScreenFlowTableError, sfe.Identifier, "Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown,"Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorExtraTransitionEvent()
		{
			// Point to mock properties where the filename of the
			// PageTransitionEvent xml file is set to a file where
			// an extra TransitionEvent exists.  This should cause
			// the validation routine to fail and throw a ScreenFlowException.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesExtraTransitionEventInXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMScreenFlowNodeError, sfe.Identifier,"Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown, "Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorDuplicatePageId()
		{
			// Point to mock properties where the filename of the
			// PageTransitionEvent xml file is set to a file where
			// a duplicate PageId exists.  This should cause the validation
			// routine of the PageTransferDataCache to fail and cause a
			// ScreenFlowException to be thrown.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesDuplicatePageIdInXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMInvalidNonEmptyArray, sfe.Identifier,"Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown,"Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorDuplicateTransitionEvent()
		{
			// Point to mock properties where the filename of the
			// PageTransitionEvent xml file is set to a file where
			// a duplicate TransitionEvent exists.  This should cause
			// the validation in PageTransferDataCache to fail and throw
			// a ScreenFlowException.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesDuplicateTransitionEventInXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMArrayEntryValidationFailed2, sfe.Identifier,"Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown, "Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorIncorrectPageId()
		{
			// Point to mock properties where the filename of the
			// PageTransitionEvent xml file is set to a file where
			// an incorrect PageId exists.  This should cause the validation
			// routine in PageTransferDataCache to fail and throw a
			// ScreenFlowException.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesIncorrectPageIdInXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMScreenFlowTableError, sfe.Identifier,"Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown, "Exception was not thrown.");
		}

		// --------------------------------------------------------------------
		
		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorIncorrectTransitionEvent()
		{
			// Point to mock properties where the filename of the
			// PageTransitionEvent xml file is set to a file where
			// an incorrect transition event exists. This should cause
			// the validation routine in PageTransferDataCache to fail
			// and throw a ScreenFlowException.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesIncorrectTransitionEventInXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMScreenFlowNodeError, sfe.Identifier, "Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown, "Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorEmptyUrl()
		{
			// Point to mock properties where the filename of the
			// PageTransferDetail xml file is set to a file where
			// an empty Url exists.  This should cause the validation
			// routine in PageTransferDataCache to fail and throw a
			// ScreenFlowException.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesEmptyUrlInXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMPreviousValidationFailed, sfe.Identifier, "Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown, "Exception was not thrown.");
		}

		// --------------------------------------------------------------------
// PAssuied : Removed because no more Invalid State name
//		/// <summary>
//		/// Test the PageTransferDetails constructor with bad data.
//		/// </summary>
//		[Test]
//		public void TestConstructorInvalidState()
//		{
//			// Point to mock properties where the filename of the
//			// PageTransferDetail xml file is set to a file where
//			// an invalid state class name exists.  This should
//			// cause the validation routine in PageTransferDataCache to fail
//			// and throw a ScreenFlowException.
//			TDServiceDiscovery.Current.SetServiceForTest
//				(ServiceDiscoveryKey.PropertyService,
//				new TestMockPropertiesInvalidState());
//
//			bool exceptionThrown = false;
//
//			try
//			{
//				// This should fail.
//				PageTransferDataCache pageTransferDataCache =
//					new PageTransferDataCache();
//			}
//			catch(ScreenFlowException sfe)
//			{
//				exceptionThrown = true;
//
//				// Test that the exception has the correct Id
//				Assertion.AssertEquals("Exception does not have the correct ID",
//					812, sfe.Id);
//			}
//
//			// Test that the exception was thrown.
//			Assertion.AssertEquals
//				("Exception was not thrown.", true, exceptionThrown);
//		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorInvalidBookmarkRedirect()
		{
			// Point to mock properties where the filename of the
			// PageTransferDetail xml file is set to a file where
			// an invalid bookmark redirect page Id exists.  This should
			// cause the validation routine in PageTransferDataCache to fail and
			// throw a ScreenFlowException.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesInvalidBookmarkRedirect());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMScreenFlowTableError, sfe.Identifier, "Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown,"Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		// PAssuied : Removed because no Logon redirect in XSD anymore
//		/// <summary>
//		/// Test the PageTransferDetails constructor with bad data.
//		/// </summary>
//		[Test]
//		public void TestConstructorInvalidLoginRedirect()
//		{
//			// Point to mock properties where the filename of the
//			// PageTransferDetail xml file is set to a file where
//			// an invalid login redirect page Id exists.  This should cause
//			// the validation routine in the PageTransferDataCache to fail and
//			// throw a ScreenFlowException.
//			TDServiceDiscovery.Current.SetServiceForTest
//				(ServiceDiscoveryKey.PropertyService,
//				new TestMockPropertiesInvalidLoginRedirect());
//
//			bool exceptionThrown = false;
//
//			try
//			{
//				// This should fail.
//				PageTransferDataCache pageTransferDataCache =
//					new PageTransferDataCache();
//			}
//			catch(ScreenFlowException sfe)
//			{
//				exceptionThrown = true;
//
//				// Test that the exception has the correct Id
//				Assertion.AssertEquals("Exception does not have the correct ID",
//					803, sfe.Id);
//			}
//
//			// Test that the exception was thrown.
//			Assertion.AssertEquals
//				("Exception was not thrown.", true, exceptionThrown);
//		}
//
//		// --------------------------------------------------------------------
//
//		/// <summary>
//		/// Test the PageTransferDetails constructor with bad data.
//		/// </summary>
//		[Test]
//		public void TestConstructorMissingLoginRedirect1()
//		{
//			// Point to mock properties where the filename of the
//			// PageTransferDetail xml file is set to a file where
//			// the login redirect is an empty string.  This should
//			// cause the validation routine in PageTransferDataCache to fail
//			// and thrown a ScreenFlowException since a valid Login redirect page
//			// Id must exist if LoginRequired is set to TRUE.
//			TDServiceDiscovery.Current.SetServiceForTest
//				(ServiceDiscoveryKey.PropertyService,
//				new TestMockPropertiesMissingLoginRedirect1());
//
//			bool exceptionThrown = false;
//
//			try
//			{
//				// This should fail.
//				PageTransferDataCache pageTransferDataCache =
//					new PageTransferDataCache();
//			}
//			catch(ScreenFlowException sfe)
//			{
//				exceptionThrown = true;
//
//				// Test that the exception has the correct Id
//				Assertion.AssertEquals("Exception does not have the correct ID",
//					810, sfe.Id);
//			}
//
//			// Test that the exception was thrown.
//			Assertion.AssertEquals
//				("Exception was not thrown.", true, exceptionThrown);
//		}
//
//		// --------------------------------------------------------------------
//
//		/// <summary>
//		/// Test the PageTransferDetails constructor with bad data.
//		/// </summary>
//		[Test]
//		public void TestConstructorMissingLoginRedirect2()
//		{
//			// Point to mock properties where the filename of the
//			// PageTransferDetail xml file is set to a file where
//			// athe login redirect attribute is missing from the Xml
//			// file even though the LoginRequired is set to TRUE.  This
//			// should cause the validation routine in PageTransferDataCache
//			// to fail and throw an exception since a valid Login redirect page
//			// Id must exist if LoginRequired is set to TRUE.
//			TDServiceDiscovery.Current.SetServiceForTest
//				(ServiceDiscoveryKey.PropertyService,
//				new TestMockPropertiesMissingLoginRedirect2());
//
//			bool exceptionThrown = false;
//
//			try
//			{
//				// This should fail.
//				PageTransferDataCache pageTransferDataCache =
//					new PageTransferDataCache();
//			}
//			catch(ScreenFlowException sfe)
//			{
//				exceptionThrown = true;
//
//				// Test that the exception has the correct Id
//				Assertion.AssertEquals("Exception does not have the correct ID",
//					810, sfe.Id);
//			}
//
//			// Test that the exception was thrown.
//			Assertion.AssertEquals
//				("Exception was not thrown.", true, exceptionThrown);
//		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorMissingPageTransferDetailsFile()
		{
			// Point to mock properties where the filename of the
			// PageTransferDetail xml file is set to a file where
			// there is a missing set of PageTransferDetails.  This
			// should cause the validation routine in PageTransferDataCache
			// to fail and throw a ScreenFlowException.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesMissingPageTransferDetailsFile());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMInvalidXml, sfe.Identifier, "Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown, "Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorMissingTransitionEventFile()
		{
			// Point to mock properties where the filename of the
			// PageTransferDetail xml file is set to a file where
			// there is a missing set of TransitionEvent details.
			// This should cause the validation routine in the PageTransferDataCache
			// to fail and throw a ScreenFlowException.			
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesMissingTransitionEventFile());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMInvalidXml, sfe.Identifier, "Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown, "Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorPageTransferDetailsInvalidXml()
		{
			// Point to mock properties where the filename of the
			// PageTransferDetail xml file is set to a file where
			// the xml contains missing attributes that are required.
			// This should cause the validation routine to fail and throw
			// a ScreenFlowException.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesPageTransferDetailsInvalidXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMInvalidXml, sfe.Identifier,"Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown, "Exception was not thrown.");
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Test the PageTransferDetails constructor with bad data.
		/// </summary>
		[Test]
		public void TestConstructorPageTransitionEventInvalidXml()
		{
			// Point to mock properties where the filename of the
			// PageTranitionEvent xml file is set to a file where
			// the xml contains missing attributes that are required.
			// This should cause the validation routine to fail and throw
			// a ScreenFlowException.
			TDServiceDiscovery.Current.SetServiceForTest
				(ServiceDiscoveryKey.PropertyService,
				new TestMockPropertiesPageTransitionEventInvalidXml());

			bool exceptionThrown = false;

			try
			{
				// This should fail.
				PageTransferDataCache pageTransferDataCache =
					new PageTransferDataCache();
			}
			catch(ScreenFlowException sfe)
			{
				exceptionThrown = true;

				// Test that the exception has the correct Id
				Assert.AreEqual(TDExceptionIdentifier.SFMInvalidXml, sfe.Identifier, "Exception does not have the correct ID");
			}

			// Test that the exception was thrown.
			Assert.AreEqual(true, exceptionThrown, "Exception was not thrown.");
		}


        // --------------------------------------------------------------------

        /// <summary>
        /// Test the GetPageGroupDetails method of the PageTransferDataCache
        /// class.
        /// </summary>
        [Test]
        public void TestGetPageGroupDetailsWithGoodData()
        {
            // Point to the mock properties where the all properties are valid.
            TDServiceDiscovery.Current.SetServiceForTest
                (ServiceDiscoveryKey.PropertyService,
                new TestMockGoodProperties());

            // Instantiate a new instance of Page TransferData Cache
            // If validation fails then a ScreenFlowException will be thrown.
            // This should never happen since for this construction, good
            // data is used to initialise the cache.
            PageTransferDataCache pageTransferDataCache =
                new PageTransferDataCache();

            // -----------------------------------------------------
            // Test we get group details for the Static group
            PageGroupDetails[] pageGroupDetails = pageTransferDataCache.GetPageGroupDetails(PageGroup.Static);
            Assert.Greater(pageGroupDetails.Length, 0, "Number of Group details returned for Static group was not greater than 0");

            
            // -----------------------------------------------------
            // Test we get group details for the Result group
            pageGroupDetails = pageTransferDataCache.GetPageGroupDetails(PageGroup.Result);
            Assert.Greater(pageGroupDetails.Length, 0, "Number of Group details returned for Result group was not greater than 0");

            // -----------------------------------------------------
            // Test the first page group item returned is Journey Summary
            Assert.AreEqual(PageGroup.Result, pageGroupDetails[0].PageGroup, "Expected first item in group details to be of PageGroup.Result, but it is not");
            Assert.AreEqual(PageId.JourneySummary, pageGroupDetails[0].PageId, "Expected first item in group detail to be JourneySummary, but it is not");
            
            // -----------------------------------------------------
            // Test the page url in the Group detail is the same as that obtained from the Page transfer details
            PageTransferDetails pageTransferDetails = pageTransferDataCache.GetPageTransferDetails(PageId.JourneySummary);
            Assert.AreEqual(pageTransferDetails.PageUrl, pageGroupDetails[0].PageUrl, "Expected first item in group details to have page url for JourneySummary, but it is different");

        }
        // --------------------------------------------------------------------

        /// <summary>
        /// Test the GetPageIdsForGroup method of the PageTransferDataCache
        /// class.
        /// </summary>
        [Test]
        public void TestGetPageIdsForGroup()
        {
            // Point to the mock properties where the all properties are valid.
            TDServiceDiscovery.Current.SetServiceForTest
                (ServiceDiscoveryKey.PropertyService,
                new TestMockGoodProperties());

            // Instantiate a new instance of Page TransferData Cache
            // If validation fails then a ScreenFlowException will be thrown.
            // This should never happen since for this construction, good
            // data is used to initialise the cache.
            PageTransferDataCache pageTransferDataCache =
                new PageTransferDataCache();

            // -----------------------------------------------------
            // Test there are no page ids for the None group, this may be different in the future
            PageId[] pageIds = pageTransferDataCache.GetPageIdsForGroup(PageGroup.None);
            Assert.AreEqual(pageIds.Length, 0, "Number of PageIDs returned for the None group was not 0");

            // -----------------------------------------------------
            // Test we get group details for the Static group
            pageIds = pageTransferDataCache.GetPageIdsForGroup(PageGroup.JourneyInput);
            Assert.Greater(pageIds.Length, 0, "Number of PageIds returned for the JourneyInput group was not greater than 0");

            // -----------------------------------------------------
            // Test the first page id returned is HomePageFindAJourneyPlanner
            Assert.AreEqual(PageId.HomePageFindAJourneyPlanner, pageIds[0], "Expected first item in PageIDs to be HomePageFindAJourneyPlanner, but it is not");

        }
        // --------------------------------------------------------------------

	} // class TestPageTransferDataCache



	/// <summary>
	/// Initialisation class for the Service Discovery.
	/// </summary>
	public class TestInitialisation : IServiceInitialisation
	{
		/// <summary>
		/// This stub is required even though it doesn't do anything.
		/// </summary>
		public void Populate(Hashtable serviceCache){}
	}

}
