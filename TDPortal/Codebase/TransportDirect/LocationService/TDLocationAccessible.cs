// *********************************************** 
// NAME                 : TDLocationAccessible.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/11/2012 
// DESCRIPTION          : TDLocationAccessible class containing properties specific to an accessible location
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDLocationAccessible.cs-arc  $ 
//
//   Rev 1.2   Jan 04 2013 15:33:00   mmodi
//Backed out populating locality and gridreference for accessible location
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Dec 18 2012 16:53:36   DLane
//Accessible JP updates
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Dec 06 2012 09:16:20   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// TDLocationAccessible class containing properties specific to an accessible location
    /// </summary>
    [Serializable()]
    public class TDLocationAccessible : TDLocation
    {
        #region Private Fields

        private bool stepFreeAccess = false;
        private bool specialAssistance = false;
        private string operatorCode = string.Empty;
        private string countryCode = string.Empty;
        private double distanceFromSearchOSGR = 0;

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
        public TDLocationAccessible()
            : base()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TDLocationAccessible(
            string name, string naptan, TDStopType tdStopType,
            bool stepFreeAccess, bool specialAssistance, 
            string operatorCode, string countryCode, 
            string adminAreaCode, string districtCode,
            double distanceFromSearchOSGR)
            : base()
        {
            this.stepFreeAccess = stepFreeAccess;
            this.specialAssistance = specialAssistance;
            this.operatorCode = operatorCode;
            this.countryCode = countryCode;
            this.distanceFromSearchOSGR = distanceFromSearchOSGR;

            // Base properties
            Accessible = true;
            AdminDistrict = new NPTGAdminDistrict(adminAreaCode, districtCode);
            Description = name;
            StopType = tdStopType;

            // Accessible location should always a stop, i.e. has a naptan
            NaPTANs = new TDNaptan[1];

            // Add the naptan
            // Deliberately not performing a NaptanLookup to avoid overhead of potential GISQuery performance, 
            // this can be changed in future if necessary: NaptanCacheEntry nce = NaptanLookup.Get(naptan, string.Empty);
            NaPTANs[0] = new TDNaptan(naptan, new OSGridReference(), name);
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Whether this location has Step Free Access
        /// </summary>
        public bool StepFreeAccess
        {
            get { return stepFreeAccess; }
            set { stepFreeAccess = value; }
        }

        /// <summary>
        /// Whether Assistance is available at this location
        /// </summary>
        public bool SpecialAssistance
        {
            get { return specialAssistance; }
            set { specialAssistance = value; }
        }

        /// <summary>
        /// NPTG Operator code for the location
        /// </summary>
        public string OperatorCode
        {
            get { return operatorCode; }
            set { operatorCode = value; }
        }

        /// <summary>
        /// NPTG Country code for the location
        /// </summary>
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        /// <summary>
        /// Distance from search OSGR, default is 0
        /// </summary>
        public double DistanceFromSearchOSGR
        {
            get { return distanceFromSearchOSGR; }
            set { distanceFromSearchOSGR = value; }
        }

        #endregion
    }
}
