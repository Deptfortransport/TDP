// *********************************************** 
// NAME             : TDPUserPreference.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 02 Apr 2011
// DESCRIPTION  	: TDPUserPreference class to hold user preferences to pass to the journey planner
// ************************************************
// 
                
using System;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// TDPUserPreference class to hold user preferences to pass to the journey planner
    /// </summary>
    [Serializable()]
    public class TDPUserPreference
    {
        #region Private members

        private string preferenceKey;
        private string preferenceValue;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TDPUserPreference()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TDPUserPreference(string preferenceKey, string preferenceValue)
        {
            this.preferenceKey = preferenceKey;
            this.preferenceValue = preferenceValue;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. PreferenceKey
        /// </summary>
        public string PreferenceKey
        {
            get { return preferenceKey; }
            set { preferenceKey = value; }
        }

        /// <summary>
        /// Read/Write. PreferenceValue
        /// </summary>
        public string PreferenceValue
        {
            get { return preferenceValue; }
            set { preferenceValue = value; }
        }

        #endregion
    }
}
