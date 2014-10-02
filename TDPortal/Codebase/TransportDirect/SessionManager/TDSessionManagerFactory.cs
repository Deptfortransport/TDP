// ***********************************************
// NAME 		: TDSessionManagerFactory.cs
// AUTHOR 		: Peter Norell
// DATE CREATED : 18/08/2003
// DESCRIPTION 	: Handles the life-cycle and allocation of TDSessionManager objects
// through the service discovery.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/TDSessionManagerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:42   mturner
//Initial revision.
//
//   Rev 1.2   Mar 29 2004 17:23:34   PNorell
//Fix for IR683
//
//   Rev 1.1   Aug 20 2003 15:51:12   PNorell
//Fixed header

using System;
using System.Collections;
using System.Web;
using System.Web.SessionState;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.SessionManager 
{
	/// <summary>
	/// Summary description for TDSessionManagerFactory.
	/// </summary>
	public class TDSessionManagerFactory : ISessionFactory
	{
		public TDSessionManagerFactory()
		{
		}

		/// <summary>
		/// Keeps track of all the sessions currently in use for this factory
		/// </summary>
		private Hashtable SessionManagers = new Hashtable();

		/// <summary>
		/// Factory method to get an appropriate instance of the ITDSessionManager object.
		/// </summary>
		/// <returns>The correct ITDSessionManager object</returns>
		public object Get()
		{
			// If a session manager already exists for the current session.
			if (SessionManagers.ContainsKey(HttpContext.Current.Session.SessionID))
			{
				return (TDSessionManager) SessionManagers[HttpContext.Current.Session.SessionID];
			}
			else 
			{
				// If not create new session manager.
				TDSessionManager sm = new TDSessionManager(this);
				SessionManagers.Add(HttpContext.Current.Session.SessionID, sm);
				return sm;
			}

		}

		public void Remove()
		{
			// Remove the session manager for the current session
			if (SessionManagers.ContainsKey(HttpContext.Current.Session.SessionID))
			{
				SessionManagers.Remove(HttpContext.Current.Session.SessionID);
			}
		}

	}
}
