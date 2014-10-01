// *********************************************** 
// NAME                 : TDAccessiblePreferences.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/11/2012
// DESCRIPTION          : Class containing options for accessible journey requests
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TDAccessiblePreferences.cs-arc  $ 
//
//   Rev 1.0   Nov 13 2012 13:14:00   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// TDAccessiblePreferences class containing options for accessible journey requests
    /// </summary>
    [Serializable()]
    public class TDAccessiblePreferences
    {
         #region Private members

        private bool doNotUseUnderground = false;
        private bool requireSpecialAssistance = false;
        private bool requireStepFreeAccess = false;
        private bool requireFewerInterchanges = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TDAccessiblePreferences()
        {
        }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TDAccessiblePreferences(bool doNotUseUnderground, bool requireSpecialAssistance, bool requireStepFreeAccess,
            bool requireFewerInterchanges)
        {
            this.doNotUseUnderground = doNotUseUnderground;
            this.requireSpecialAssistance = requireSpecialAssistance;
            this.requireStepFreeAccess = requireStepFreeAccess;
            this.requireFewerInterchanges = requireFewerInterchanges;
        }
        
        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Accessible
        /// </summary>
        public bool Accessible
        {
            get {
                // if any of the accessible condition is met the accessible preference is selected
                return DoNotUseUnderground || RequireSpecialAssistance || RequireStepFreeAccess || RequireFewerInterchanges;
            }
        }

        /// <summary>
        /// Read/Write. DoNotUseUnderground
        /// </summary>
        public bool DoNotUseUnderground
        {
            get { return doNotUseUnderground; }
            set { doNotUseUnderground = value; }
        }

        /// <summary>
        /// Read/Write. RequireSpecialAssistance
        /// </summary>
        public bool RequireSpecialAssistance
        {
            get { return requireSpecialAssistance; }
            set { requireSpecialAssistance = value; }
        }

        /// <summary>
        /// Read/Write. RequireStepFreeAccess
        /// </summary>
        public bool RequireStepFreeAccess
        {
            get { return requireStepFreeAccess; }
            set { requireStepFreeAccess = value; }
        }

        /// <summary>
        /// Read/Write. RequireFewerInterchanges
        /// </summary>
        public bool RequireFewerInterchanges
        {
            get { return requireFewerInterchanges; }
            set { requireFewerInterchanges = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Populates this TDAccessiblePreferences object using the accessible option string provided (ignores case).
        /// Values can be "u" not underground, "a" assistance, "w" wheelchair, "wa" wheelchair and assistance
        /// </summary>
        /// <param name="accessibleOption"></param>
        public void PopulateAccessiblePreference(string accessibleOption)
        {
            if (!string.IsNullOrEmpty(accessibleOption))
            {
                string accessibleOptions = accessibleOption.ToUpper().Trim();

                switch (accessibleOptions)
                {
                    case "U":
                        doNotUseUnderground = true;
                        break;
                    case "A":
                        requireSpecialAssistance = true;
                        break;
                    case "W":
                        requireStepFreeAccess = true;
                        break;
                    case "WA":
                        requireStepFreeAccess = true;
                        requireSpecialAssistance = true;
                        break;
                    default:
                        doNotUseUnderground = false;
                        requireStepFreeAccess = false;
                        requireSpecialAssistance = false;
                        break;
                }
            }
        }
        
        /// <summary>
        /// Retrieves a string value representing the accessible option of this TDAccessiblePreferences object.
        /// Default is string.Empty if no accessible preference set
        /// </summary>
        /// <returns></returns>
        public string GetAccessiblePreferenceString()
        {
            if (requireStepFreeAccess && requireSpecialAssistance)
                return "WA";
            else if (requireSpecialAssistance)
                return "A";
            else if (requireStepFreeAccess)
                return "W";
            else if (doNotUseUnderground)
                return "U";

            return string.Empty;
        }

        /// <summary>
        /// Returns a deep clone of this TDAccessiblePreferences
        /// </summary>
        /// <returns></returns>
        public TDAccessiblePreferences Clone()
        {
            TDAccessiblePreferences target = new TDAccessiblePreferences();

            target.DoNotUseUnderground = this.doNotUseUnderground;
            target.requireSpecialAssistance = this.requireSpecialAssistance;
            target.requireStepFreeAccess = this.requireStepFreeAccess;
            target.requireFewerInterchanges = this.requireFewerInterchanges;

            return target;
        }

        #endregion
    }
}