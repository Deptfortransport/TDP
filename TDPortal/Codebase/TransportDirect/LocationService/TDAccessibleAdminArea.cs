// *********************************************** 
// NAME                 : TDAccessibleAdminArea.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/11/2012 
// DESCRIPTION          : Represents a TD Accessible Administrative Area, 
//                      : indicating if the admin area (and district) have accessible flags
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDAccessibleAdminArea.cs-arc  $ 
//
//   Rev 1.0   Dec 06 2012 09:15:50   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// Represents a TD Accessible Administrative Area, 
    /// indicating if the admin area (and district) have accessible flags
    /// </summary>
    [Serializable()]
    public class TDAccessibleAdminArea
    {
        #region Private Fields
        
        private string administrativeAreaCode = string.Empty;
        private string districtCode = string.Empty;
        private bool stepFreeAccess = false;
        private bool assistanceAvailable = false;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public TDAccessibleAdminArea()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TDAccessibleAdminArea(string administrativeAreaCode, string districtCode, bool stepFreeAccess, bool assistanceAvailable)
        {
            this.administrativeAreaCode = administrativeAreaCode;
            this.districtCode = districtCode;
            this.stepFreeAccess = stepFreeAccess;
            this.assistanceAvailable = assistanceAvailable;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Administrative Area Code
        /// </summary>
        public string AdministrativeAreaCode
        {
            get { return administrativeAreaCode; }
        }

        /// <summary>
        /// District Code
        /// </summary>
        public string DistrictCode
        {
            get { return districtCode; }
        }

        /// <summary>
        /// Whether Step Free Access is available for this Admin Area (and District)
        /// </summary>
        public bool StepFreeAccess
        {
            get { return stepFreeAccess; }
        }

        /// <summary>
        /// Whether Assistance is available for this Admin Area (and District)
        /// </summary>
        public bool AssistanceAvailable
        {
            get { return assistanceAvailable; }
        }

        #endregion
    }
}