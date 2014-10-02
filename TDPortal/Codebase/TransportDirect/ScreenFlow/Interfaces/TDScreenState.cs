// *********************************************** 
// NAME                 : TDScreenState.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : Abstract class.  All page-
// specific state management classes must inherit
// from this class.  
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Interfaces/TDScreenState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:50   mturner
//Initial revision.
//
//   Rev 1.5   Mar 09 2005 15:03:12   tmollart
//Added method GetPageIdForTransitionEvent. This code was lifted from the default state DoTransition method. The code has been refactored to this location in the base class so it can be used for both Default and Specific State processing.
//
//   Rev 1.4   Apr 23 2004 14:36:32   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.3   Aug 15 2003 14:36:52   passuied
//Update after design change
//
//   Rev 1.2   Aug 07 2003 13:54:18   kcheung
//Set CLSComplaint to true
//
//   Rev 1.1   Jul 23 2003 12:28:40   kcheung
//Updated after code review comments

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.ScreenFlow.State
{
	/// <summary>
	/// Abstract class.  All page-
	/// specific state management classes must inherit
	/// from this class.  
	/// </summary>
	public abstract class TDScreenState //: IScreenFlowState // PAssuied : no need for the interface anymore
	{
		protected PageId pageId;

		protected TDScreenState(PageId pageId)
		{
			this.pageId = pageId;
		}

		/// <summary>
		/// Looks in the FormShift area of the session for a TransitionEvent
		/// and returns the Id of the next page to go to based on the TransitionEvent.
		/// </summary>
		/// <returns>Id of the next page.</returns>
		public abstract PageId DoTransition();

		/// <summary>
		/// Gets the page ID of the current transition event.
		/// </summary>
		/// <returns>The Page Id of the current transition event.</returns>
		public PageId GetPageIdForTransitionEvent()
		{
			// Get the session from ServiceDiscovery.
			// Retrive the session from Service Discovery
			ITDSessionManager session = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

			// Get the TransitionEvent in the FormShift
			TransitionEvent transitionEvent = session.FormShift[SessionKey.TransitionEvent];

			try
			{
				if(transitionEvent == TransitionEvent.Default)
				{
					// return the current page 
					return pageId;
				}
				else
				{
					// Get the PageController from Service Discovery
					IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];

					// Get the PageTransferDataCache from the pageController
					IPageTransferDataCache pageTransferDataCache = pageController.PageTransferDataCache;

					// Now, get the pageId associated with the transiton event.
					PageId result = pageTransferDataCache.GetPageEvent(transitionEvent);

					return result;
				}
			}
			catch(ScreenFlowException sfe)
			{
				string message = String.Format(Messages.DefaultState, "PageId:" + pageId);
				
				OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);

				// Propagate the exception back to PageController.
				throw sfe;
			}
			catch(TDException tde)
			{
				string message = String.Format(Messages.DefaultState, "PageId:" + pageId);
				
				OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);

				// Propagate the exception back to PageController.
				throw tde;
			}
			catch(Exception e)
			{
				string message = String.Format(Messages.DefaultState, "PageId:" + pageId);
				
				OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);

				// Propagate the exception back to PageController.
				throw e;
			}
		}
	}
}
