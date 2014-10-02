// *********************************************** 
// NAME                 : TestMockStateManagementClasses.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : Defines some mock state
// management classes used for NUnit testing.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Test/TestMockStateManagementClasses.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:54   mturner
//Initial revision.
//
//   Rev 1.3   Jul 25 2003 17:12:02   kcheung
//Updated some of the states.
//
//   Rev 1.2   Jul 23 2003 13:28:30   kcheung
//Changed $log to $Log

using System;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;

namespace TransportDirect.UserPortal.ScreenFlow.State
{
	/// <summary>
	/// Defines some page-specific state classes used for testing.
	/// Each state class simply returns a PageId.
	/// </summary>
	
	public class InitialPageState : TDScreenState
	{
		public InitialPageState(PageId pageId) : base(pageId) { }

		public override PageId DoTransition()
		{
			return PageId.Home;
		}
	}

	public class LoginState : TDScreenState
	{
		public LoginState(PageId pageId) : base(pageId) { }

		public override PageId DoTransition()
		{
			return PageId.Login;
		}
	}

	public class ShowFavouritesState : TDScreenState
	{
		public ShowFavouritesState(PageId pageId) : base(pageId) { }

		public override PageId DoTransition()
		{
			return PageId.ShowFavourites;
		}
	}

	public class BookmarkState : TDScreenState
	{
		public BookmarkState(PageId pageId) : base(pageId) { }

		public override PageId DoTransition()
		{
			// Get the FormShift from the session
			ITDSessionManager sessionManager =
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			if(sessionManager.FormShift[SessionKey.TransitionEvent] ==
				TransitionEvent.GoHome)
			{
				return PageId.Home;
			}
			else
			{
				return PageId.Bookmark;
			}
		}
	}

	public class HomeState : TDScreenState
	{
		public HomeState(PageId pageId) : base(pageId) { }

		public override PageId DoTransition()
		{
			return PageId.Login;
		}
	}
}
