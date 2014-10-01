﻿// *********************************************** 
// NAME             : TDPStopEventRequestManager.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 26 Apr 2011
// DESCRIPTION  	: TDPStopEventRequestManager class to hold multiple stop event requests for a session.
// The class uses the hash property of the request as the key to ensure "identical" requests do not fill
// the historical requests made. This allows the TDPStopEventResultManager to serve the already planned
// stop event result rather than making a duplicate call to the CJP (removes wasted performance overhead).
// This therefore does rely on the caller ensuring proper use of the request hash value.
// ************************************************
// 
                
                
using System;
using System.Collections.Generic;
using TDP.UserPortal.JourneyControl;

namespace TDP.UserPortal.SessionManager
{
    /// <summary>
    /// TDPStopEventRequestManager class to hold multiple stop event requests for a session
    /// </summary>
    [Serializable()]
    public class TDPStopEventRequestManager : ITDPSessionAware
    {
        #region Private members

        // Max number of requests allowed to retain in manager (hard coded value)
        private int maxRequests = 5;

        // Holds TDPJourneyRequests
        private Dictionary<string, ITDPJourneyRequest> requests = null;
        
        // Maintains a history of requests, used to drop the oldest request when maxRequests is exceeded
        private Queue<string> requestQueue = null;

        // Session aware
        private bool isDirty = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public TDPStopEventRequestManager()
        {
            requests = new Dictionary<string, ITDPJourneyRequest>(maxRequests);
            requestQueue = new Queue<string>(maxRequests);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds the TDPJourneyRequest to the collection within this manager.
        /// </summary>
        /// <param name="tdpJourneyRequest"></param>
        public void AddTDPJourneyRequest(ITDPJourneyRequest tdpJourneyRequest)
        {
            if (tdpJourneyRequest != null)
            {
                // Use the request hash property as the key.
                string key = tdpJourneyRequest.JourneyRequestHash;

                // Delete the old(est) request if needed
                if ((requestQueue.Count >= maxRequests) && (!requestQueue.Contains(key)))
                {
                    string oldRequestKey = requestQueue.Peek();

                    requestQueue.Dequeue();
                    requests.Remove(oldRequestKey);
                }

                
                // Check if the request already exists
                if (!requests.ContainsKey(key))
                {
                    // Add this new request and queue to keep track of it
                    requestQueue.Enqueue(key);
                    requests.Add(key, tdpJourneyRequest);
                }
                else
                {
                    // Already exists, update it
                    requests[key] = tdpJourneyRequest;
                }


                // Flag class as changed
                isDirty = true;
            }
        }

        /// <summary>
        /// Retrieves the TDPJourneyRequest from the manager. 
        /// Returns null if it doesnt exist
        /// </summary>
        /// <param name="RequestId"></param>
        public ITDPJourneyRequest GetTDPJourneyRequest(string requestHash)
        {
            if (requests.ContainsKey(requestHash))
            {
                return requests[requestHash];
            }
            
            return null;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. TDPJourneyRequests contained within this manager.
        /// Added for serialization only.
        /// </summary>
        public Dictionary<string, ITDPJourneyRequest> TDPJourneyRequests
        {
            get { return requests; }
            set { requests = value; }
        }

        /// <summary>
        /// Read/Write. Queue to keep track of requests, old requests are removed 
        /// when new requests are added if max capacity is exceeded.
        /// Added for serialization only.
        /// </summary>
        public Queue<string> TDPJourneyRequestsQueue
        {
            get { return requestQueue; }
            set { requestQueue = value; }
        }

        #endregion

        #region ITDPSessionAware methods

        /// <summary>
        /// Gets/Sets if the session aware object considers itself to have changed or not
        /// </summary>
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

        #endregion
    }
}
