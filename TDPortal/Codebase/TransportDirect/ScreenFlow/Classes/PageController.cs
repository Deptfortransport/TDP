// *********************************************** 
// NAME                 : PageController.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : Implements the IPageController
// interface.  The PageController decides
// which page should be displayed next.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Classes/PageController.cs-arc  $ 
//
//   Rev 1.3   Oct 13 2008 16:46:36   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Aug 22 2008 10:17:02   mmodi
//Updated to check for cycle result
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   May 01 2008 17:07:32   mmodi
//Updated for PageGrouping.xml
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.1   Mar 10 2008 15:26:40   mturner
//Initial Del10 Codebase from Dev Factory
//
//  Rev Devfactory Mar 09 2008 17:04:00 apatel
//  updated validate page transition method for location information 
//
//   Rev 1.0   Nov 08 2007 12:47:48   mturner
//Initial revision.
//
//   Rev 1.22   Mar 14 2006 08:41:40   build
//Automatically merged from branch for stream3353
//
//   Rev 1.21.1.1   Mar 13 2006 16:32:24   tmollart
//Updated after code review.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21.1.0   Dec 20 2005 19:37:26   rhopkins
//Change to enable Extend to work with new TDItineraryManager.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.21   Sep 09 2004 13:34:20   RHopkins
//IR1509 If the User accesses a page out of sequence and is in the middle of creating an Extension then we must check whether they have yet obtained any valid results - if not then delete the incomplete Extension.
//
//   Rev 1.20   Jul 15 2004 09:50:38   jgeorge
//Removal of JourneyParametersType from screenflow
//
//   Rev 1.19   Jun 29 2004 16:45:16   jgeorge
//Ensure correct JourneyParameterType is reinstated when using BookmarkValidJourneyRedirect
//
//   Rev 1.18   Jun 29 2004 15:31:12   esevern
//moved conversion of journey parameter type from xml attribute to page transfer details initialisation
//
//   Rev 1.17   Jun 29 2004 13:45:16   esevern
//added JourneyParameterType attribute required for additional safe page redirection processing
//
//   Rev 1.16   May 26 2004 12:59:32   RHopkins
//BookmarkValidResultsRedirect will now also be used if the Itinerary contains any journeys.
//
//   Rev 1.15   May 20 2004 14:41:00   rgreenwood
//Back Button Enhancement:
//ValidatePageTransition method now checks the page for a BookmarkValidJourneyRedirect attribute, and whether valid Journey Data exists to do the redirect.
//
//   Rev 1.14   Apr 23 2004 14:36:40   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.13   Oct 03 2003 13:38:50   PNorell
//Updated the new exception identifier.
//
//   Rev 1.12   Sep 18 2003 09:57:18   jcotton
//Changes for intitial screenflow integration work
//
//   Rev 1.11   Sep 16 2003 14:24:38   jcotton
//Removed "using System.Reflection"
//
//   Rev 1.10   Sep 16 2003 14:23:14   jcotton
//Deleted redundant and commented out reflection code and reflection exception catches
//
//   Rev 1.9   Aug 15 2003 14:36:50   passuied
//Update after design change
//
//   Rev 1.8   Aug 07 2003 13:54:06   kcheung
//Set CLSComplaint to true
//
//   Rev 1.7   Jul 30 2003 18:55:00   geaton
//Reverted back to old OperationalEvent constructor.
//
//   Rev 1.6   Jul 29 2003 18:33:14   geaton
//Swapped OperationalEvent parameter order after change in OperationalEvent constructor.
//
//   Rev 1.5   Jul 25 2003 17:16:36   kcheung
//Changed the | to || from code review comments.
//
//   Rev 1.4   Jul 23 2003 17:53:40   kcheung
//Attempted to change CLSCompliant stuff - didn't need to in the end.
//
//   Rev 1.3   Jul 23 2003 17:08:14   kcheung
//Removed a variable that was used only in testing.
//
//   Rev 1.2   Jul 23 2003 15:27:32   kcheung
//Updated exceptions after Code Review comments
//
//   Rev 1.1   Jul 23 2003 12:28:18   kcheung
//Updated after code review comments

using System;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.ScreenFlow.State;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.ScreenFlow
{
	/// <summary>
	/// The PageController decides which page should be
	/// displayed next.
	/// </summary>
	public class PageController : IPageController
	{
		private IPageTransferDataCache pageTransferDataCache;

		/// <summary>
		/// Read/Write Property to get/set PageTransferDataCache
		/// </summary>
		public IPageTransferDataCache PageTransferDataCache
		{
			get { return pageTransferDataCache; }
			set { pageTransferDataCache = value; }
		}

		/// <summary>
		/// Constructor for PageController
		/// </summary>
		/// <param name="pageTransferDataCache">
		/// The PageTransferDataCache to use for retrieving PageTransferDetails
		/// and PageIds. (This should be the declared in global.asax and passed
		/// into here.</param>
		public PageController(IPageTransferDataCache pageTransferDataCache)
		{
			this.pageTransferDataCache = pageTransferDataCache;
		}
		
		/// <summary>
		/// Returns the Id of the next page given the current page.
		/// </summary>
		/// <param name="pageId">The current page.</param>
		/// <returns>Next page to be displayed.</returns>
		public PageId GetNextPageId(PageId pageId)
		{		
			try
			{
				PageId resultPageId = PageId.Empty;

				ITDSessionManager session =
					(ITDSessionManager)TDServiceDiscovery.Current
					[ServiceDiscoveryKey.SessionManager];
		
				// Get the "transferred" flag from the session.
				bool transferred = session.Session[SessionKey.Transferred];

				if(transferred)
				{
					// the current pageId should be returned.
					resultPageId = pageId;
				}
				else
				{
					// get the page transfer datails from the cache
					PageTransferDetails pageTransferDetails =
						pageTransferDataCache.GetPageTransferDetails(pageId);

					// PAssuied : inserted instead of part commented out
					// if the page is specific then instantiate a SpecificStateClass, otherwise instantiate default class
					TDScreenState state;
					if (pageTransferDetails.SpecificStateClass)
					{
						state = new SpecificState(pageId);
					}
					else
					{
						state = new DefaultState (pageId);
					}
					// End PAssuied

					resultPageId = state.DoTransition();
				}

				// set the "setTransferredTo" boolean to TRUE if result is 
				// different from the current pageId, otherwise set to FALSE
				bool setTransferredTo = !(pageId == resultPageId);

				// set the "transferred" flag in the session
				session.Session[SessionKey.Transferred] = setTransferredTo;

				// Set the "expected next page-id" in the session to
				// the value that should be returned.
				session.Session[SessionKey.NextPageId] = resultPageId;

				// Return the result
				return resultPageId;
			}

			catch(ScreenFlowException sfe)
			{
				string message = String.Format
					(Messages.GetNextPageIdException, "PageId:" + pageId);

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);
				
				throw new ScreenFlowException
					(message, sfe, true, TDExceptionIdentifier.SFMInvalidGetNextID);
			}
			catch(TDException tde)
			{
				string message = String.Format
					(Messages.GetNextPageIdException, "PageId:" + pageId);

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);
				
				throw new ScreenFlowException
					(message, tde, true, TDExceptionIdentifier.SFMInvalidGetNextID);
			}
			catch(Exception e)
			{
				string message = String.Format
					(Messages.GetNextPageIdException, "PageId:" + pageId);

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);
				
				throw new ScreenFlowException
					(message, e, true, TDExceptionIdentifier.SFMInvalidGetNextID);
			}
		}

		// --------------------------------------------------------------------

		/// <summary>
		/// Returns the PageTransferDetails object associated with the
		/// given pageId.
		/// </summary>
		/// <param name="pageId">Id of the page in which to get the
		/// PageTransferDetails object for.</param>
		/// <returns>The PageTransferDetails object associated
		/// with the pageId.</returns>
		public PageTransferDetails GetPageTransferDetails(PageId pageId)
		{
			return(pageTransferDataCache.GetPageTransferDetails(pageId));
		}

		// --------------------------------------------------------------------

        /// <summary>
        /// Returns the PageTransferDetails object associated with the
        /// given pageId.
        /// </summary>
        /// <param name="pageId">Id of the page in which to get the
        /// PageTransferDetails object for.</param>
        /// <returns>The PageTransferDetails object associated
        /// with the pageId.</returns>
        public PageGroupDetails[] GetPageGroupDetails(PageGroup pageGroup)
        {
            return (pageTransferDataCache.GetPageGroupDetails(pageGroup));
        }

        // --------------------------------------------------------------------

        /// <summary>
        /// Returns all PageId's for the selected PageGroup
        /// </summary>
        public PageId[] GetPageIdsForGroup(PageGroup pageGroup)
        {
            return (pageTransferDataCache.GetPageIdsForGroup(pageGroup));
        }

        // --------------------------------------------------------------------

		/// <summary>
		/// Checks if the current page can be displayed.  (This includes
		/// bookmarking checks and logon requirement checks).
		/// </summary>
		/// <param name="pageId">Id of the current page.</param>
		/// <returns>Id of the page that should be displayed.</returns>
		public PageId ValidatePageTransition(PageId pageId)
		{
			try
			{
				// Get the "expected next page-id" from the session.
				ITDSessionManager session =
					(ITDSessionManager)TDServiceDiscovery.Current
					[ServiceDiscoveryKey.SessionManager];

				TDItineraryManager itineraryManager = TDItineraryManager.Current;

				PageId expectedPageId = PageId.Empty;
				expectedPageId = session.Session[SessionKey.NextPageId];

				// This will hold the pageId to return.
				PageId resultPageId = PageId.Empty;

				if(expectedPageId != pageId || expectedPageId == PageId.Empty)
				{
					// Did not arrive to the current page by an explicit transfer.
					// Determine whether to get the BookmarkRedirect pageId or BookmarkValidJourneyRedirect pageId.
					// resultPageId = pageTransferDataCache.
					// GetPageTransferDetails(pageId).BookmarkRedirect;
					PageTransferDetails pageTransferDetails =
						pageTransferDataCache.GetPageTransferDetails(pageId);

					// Check pageTransferDataCache for a BookmarkValidJourneyRedirect
					if(pageTransferDetails.BookmarkValidJourneyRedirect != PageId.Empty)
					{
						// Check whether valid Journey Results exist
						if (   ((session.JourneyResult != null) && (session.JourneyResult.IsValid) )
							|| (itineraryManager.Length > 0)
                            || ((session.CycleResult != null) && (session.CycleResult.IsValid))
                            )
						{
							if ( ((itineraryManager as ExtendItineraryManager) != null)
								&& (itineraryManager.ExtendInProgress)
								&& ((session.JourneyResult == null) || ( !session.JourneyResult.IsValid ) )
                                && ((session.CycleResult == null) || (!session.CycleResult.IsValid))
                                )
							{
								//Doing an Extension but no valid Journey Results, remove incomplete extension
								if (itineraryManager.ExtendedFromInitialResultsPage && (itineraryManager.Length <= 2))
								{
									// Itinerary only consists of Initial Journey plus this Extension and the User didn't go through the Extend Journey intermediate page
									itineraryManager.ResetToInitialJourney();
								}
								else
								{
									itineraryManager.DeleteLastExtension();
								}
							}

							//Valid Journey Results exist, use BookmarkValidJourneyRedirect
							resultPageId = pageTransferDetails.BookmarkValidJourneyRedirect;
						}
						else
						{
                           
							//No valid Journey Results, use BookmarkRedirect
							resultPageId = pageTransferDetails.BookmarkRedirect;

                            if (pageId == PageId.LocationInformation)
                                resultPageId = pageTransferDetails.PageId;
						}
					}
					else
					{
						//No BookmarkValidJourneyRedirect for this page, use BookmarkRedirect instead
						resultPageId = pageTransferDetails.BookmarkRedirect;
					}

				}
				else
				{
					// expectedPageId matches the current pageId so
					// the result can be set to the current pageId.
					resultPageId = pageId;
				}

				// return the result
				return resultPageId;
			}
			catch(ScreenFlowException sfe)
			{
				string message = String.Format
					(Messages.GetNextPageIdException, "PageId:" + pageId);

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);
				
				throw new ScreenFlowException
					(message, sfe, true, TDExceptionIdentifier.SFMInvalidPageTransition);

			}
			catch(TDException tde)
			{
				string message = String.Format
					(Messages.GetNextPageIdException, "PageId:" + pageId);

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);
				
				throw new ScreenFlowException
					(message, tde, true, TDExceptionIdentifier.SFMInvalidPageTransition);
			}
			catch(Exception e)
			{
				string message = String.Format
					(Messages.ValidatePageTransitionException, "PageId:" + pageId);
				
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);
				
				throw new ScreenFlowException
					(message, e, true, TDExceptionIdentifier.SFMInvalidPageTransition);
			}

		}

	}
}
