// *********************************************** 
// NAME             : ITDPSession.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Mar 2011
// DESCRIPTION  	: ITDPSession interface detailing the indexers 
// that need to be provided by the TDPSession class
// ************************************************
// 
                
using System;

using TDP.Common;

namespace TDP.UserPortal.SessionManager
{
    /// <summary>
    /// Interface for TDPSession
    /// </summary>
    public interface ITDPSession
    {
        /// <summary>
        /// Get/Set Session data.  Read/Write Access.
        /// </summary>
        /// <value>Int32</value>
        int this[IntKey key] { get; set; }

        /// <summary>
        /// Get/Set Session data.  Read/Write Access.
        /// </summary>
        /// <value>String</value>
        string this[StringKey key] { get; set; }

        /// <summary>
        /// Get/Set Session data.  Read/Write Access.
        /// </summary>
        /// <value>Double</value>
        double this[DoubleKey key] { get; set; }

        /// <summary>
        /// Get/Set Session data.  Read/Write Access.
        /// </summary>
        /// <value>DateTime</value>
        DateTime this[DateKey key] { get; set; }

        /// <summary>
        /// Get/Set Session data.  Read/Write Access.
        /// </summary>
        /// <value>bool</value>
        bool this[BoolKey key] { get; set; }

        /// <summary>
        /// Get/Set Session data.  Read/Write Access.
        /// </summary>
        /// <value>PageId</value>
        PageId this[PageIdKey key] { get; set; }

        /// <summary>
        /// Get Session ID.  Read Access.
        /// </summary>
        string SessionID { get; }
    }
}
