// *********************************************** 
// NAME             : TDPSessionFactory.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Mar 2011
// DESCRIPTION  	: TDPSessionFactory handles the life-cycle and allocation of TDPSessionManager objects
// through the service discovery
// ************************************************
// 

using System.Collections;
using System.Web;

namespace TDP.UserPortal.SessionManager
{
    /// <summary>
    /// TDPSessionFactory class
    /// </summary>
    public class TDPSessionFactory : ITDPSessionFactory
    {
        #region Private variables

        /// <summary>
        /// Keeps track of all the sessions currently in use for this factory
        /// </summary>
        private Hashtable SessionManagers = new Hashtable();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TDPSessionFactory()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Factory method to get an appropriate instance of the ITDPSessionManager object.
        /// </summary>
        /// <returns>The correct ITDPSessionManager object</returns>
        public object Get()
        {
            // If a session manager already exists for the current session.
            if (SessionManagers.ContainsKey(HttpContext.Current.Session.SessionID))
            {
                return (TDPSessionManager)SessionManagers[HttpContext.Current.Session.SessionID];
            }
            else
            {
                // If not create new session manager.
                TDPSessionManager sm = new TDPSessionManager(this);
                SessionManagers.Add(HttpContext.Current.Session.SessionID, sm);
                return sm;
            }

        }

        /// <summary>
        /// Removes the session manager for the current session
        /// </summary>
        public void Remove()
        {
            // Remove the session manager for the current session
            if (SessionManagers.ContainsKey(HttpContext.Current.Session.SessionID))
            {
                SessionManagers.Remove(HttpContext.Current.Session.SessionID);
            }
        }

        #endregion
    }
}
