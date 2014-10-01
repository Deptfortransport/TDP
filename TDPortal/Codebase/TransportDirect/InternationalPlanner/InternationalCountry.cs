// *********************************************** 
// NAME			: InternationalCountry.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Class which contains information about an international country
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalCountry.cs-arc  $
//
//   Rev 1.2   Feb 12 2010 09:40:30   mmodi
//Updated to plan train journeys and save journeys to cache
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 09 2010 09:53:16   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:04:30   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Class which contains information about an international country
    /// </summary>
    [Serializable()]
    public class InternationalCountry
    {
        #region Private members

        private string countryCode;
        private string countryCodeIANA;
        private string adminCodeUIC;
        private double timeZone = 0;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalCountry()
		{			
		}

		#endregion

        #region Public properties

        /// <summary>
        /// Read/Write. The code for this country
        /// </summary>
        public string CountryCode
        {
            get { return countryCode; }
            set { countryCode = value; }
        }

        /// <summary>
        /// Read/Write. The IANA code for this country
        /// </summary>
        public string CountryCodeIANA
        {
            get { return countryCodeIANA; }
            set { countryCodeIANA = value; }
        }

        /// <summary>
        /// Read/Write. The UIC admin code for this country
        /// </summary>
        public string AdminCodeUIC
        {
            get { return adminCodeUIC; }
            set { adminCodeUIC = value; }
        }

        /// <summary>
        /// Read/Write. The Time zone in hours from the UK for this country
        /// </summary>
        public double TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }
        }

        #endregion
    }
}
