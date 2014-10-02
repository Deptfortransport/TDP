// *********************************************** 
// NAME                 : DefaultState.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : DefaultState class.  This is
// a simple state management class that returns a
// pageId associated with a transitionEvent.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Classes/DefaultState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:46   mturner
//Initial revision.
//
//   Rev 1.6   Mar 09 2005 15:04:24   tmollart
//Refactored code used to get the Page Id of a transition event into the base class. This class now calls the functionality in the base class.
//
//   Rev 1.5   Apr 23 2004 14:36:44   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.4   Aug 07 2003 13:54:00   kcheung
//Set CLSComplaint to true
//
//   Rev 1.3   Jul 30 2003 18:54:04   geaton
//Reverted back to old OperationalEvent constructor.
//
//   Rev 1.2   Jul 29 2003 18:33:06   geaton
//Swapped OperationalEvent parameter order after change in OperationalEvent constructor.
//
//   Rev 1.1   Jul 23 2003 12:28:12   kcheung
//Updated after code review comments

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.ScreenFlow.State
{
	/// <summary>
	/// Default state management class.
	/// </summary>
	public class DefaultState : TDScreenState
	{
		public DefaultState(PageId pageId) : base(pageId) {}

		/// <summary>
		/// Overrides the abstract method.  Returns a pageId
		/// depending on the TransitionEvent that is currently
		/// held in the FormShift area.
		/// </summary>
		/// <returns>PageId of the next page.</returns>
		public override PageId DoTransition()
		{
			//Call method to get the current Page ID for the transition event.
			return GetPageIdForTransitionEvent();
		}
	}
}
