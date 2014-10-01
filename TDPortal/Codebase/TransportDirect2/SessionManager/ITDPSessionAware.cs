// *********************************************** 
// NAME             : ITDPSessionAware.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Mar 2011
// DESCRIPTION  	: Marker interface for things that are aware of session
// ************************************************
// 

namespace TDP.UserPortal.SessionManager
{
    /// <summary>
    /// A marker interface for being session aware.
    /// </summary>
    public interface ITDPSessionAware
    {
        /// <summary>
        /// Gets/Sets if the session aware object considers itself to have changed or not
        /// </summary>
        bool IsDirty { get; set; }
    }
}
