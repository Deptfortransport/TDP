using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDP.UserPortal.SessionManager;
using TDP.UserPortal.StateServer;

namespace TDP.TestProject
{
    public class MockSessionFactory: ITDPSessionFactory
    {
        #region Private variables

        public static string mockSessionId = "testSession";

        /// <summary>
        /// Keeps track of all the sessions currently in use for this factory
        /// </summary>
        private static Dictionary<string, MockSessionManager> SessionManagers = new Dictionary<string, MockSessionManager>();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public MockSessionFactory()
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
            if (SessionManagers.ContainsKey(mockSessionId))
            {
                return SessionManagers[mockSessionId];
            }
            else
            {
                // If not create new session manager.
                MockSessionManager sm = new MockSessionManager(this);
                SessionManagers.Add(mockSessionId, sm);
                return sm;
            }

        }

        /// <summary>
        /// Removes the session manager for the current session
        /// </summary>
        public void Remove()
        {
            // Remove the session manager for the current session
            if (SessionManagers.ContainsKey(mockSessionId))
            {
                SessionManagers.Remove(mockSessionId);
            }
        }

        public static void ClearSession()
        {
            using (TDPStateServer stateServer = new TDPStateServer())
            {
                stateServer.Delete(mockSessionId ,SessionManagerKey.KeyRequestManager.ID);
                stateServer.Delete(mockSessionId, SessionManagerKey.KeyResultManager.ID);
                stateServer.Delete(mockSessionId, SessionManagerKey.KeyStopEventRequestManager.ID);
                stateServer.Delete(mockSessionId, SessionManagerKey.KeyStopEventResultManager.ID);
                stateServer.Delete(mockSessionId, SessionManagerKey.KeyJourneyViewState.ID);
                stateServer.Delete(mockSessionId, SessionManagerKey.KeyPageState.ID);
                stateServer.Delete(mockSessionId, SessionManagerKey.KeyTravelNewsPageState.ID);
            }
        }

        #endregion
    }
}


