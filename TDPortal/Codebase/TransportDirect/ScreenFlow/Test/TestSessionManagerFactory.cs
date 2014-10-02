// *********************************************** 
// NAME                 : TestSessionManagerFactory.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 18/07/2003 
// DESCRIPTION  : NUnit class to allow the
// Service Discovery to create a new Session Manager.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Test/TestSessionManagerFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:56   mturner
//Initial revision.
//
//   Rev 1.1   Jul 23 2003 13:28:42   kcheung
//Changed $log to $Log

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// NUnit class to allow the
	/// Service Discovery to create a new Session Manager..
	/// </summary>
	public class SessionManagerFactory : IServiceFactory
	{
		private ITDSessionManager current;

		public SessionManagerFactory()
		{
			current = new TestMockTDSessionManager();
		}

		public Object Get()
		{
			return current;
		}
	}
}
