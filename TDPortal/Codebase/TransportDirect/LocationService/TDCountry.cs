// *********************************************** 
// NAME			: TDCountry.cs
// AUTHOR		: Amit Patel
// DATE CREATED	: 08/02/2003 
// DESCRIPTION	: TDCountry class providing country Information of location
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDCountry.cs-arc  $
//
//   Rev 1.2   Mar 27 2013 09:30:10   mmodi
//Added clone method
//Resolution for 5908: Mapping walk leg shows intermediate change count icon in different place to actual start of leg
//
//   Rev 1.1   Feb 10 2010 10:19:54   apatel
//Corrected countryname variable spelling
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 09 2010 10:48:38   apatel
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationService
{
    /// <summary>
    /// Country class
    /// </summary>
    [Serializable]
    public class TDCountry
    {
        #region Private Fields
        private string countryCode = string.Empty;
        private string countryName = string.Empty;
        private string ianaCode = string.Empty;
        private string adminCodeUIC = string.Empty;
        private double timeZone = 0;
        #endregion

        #region Public Properties
        /// <summary>
        /// Read/write - Country code
        /// </summary>
        public string CountryCode 
        { 
            get {return countryCode; }
            set { countryCode = value; }
        }

        /// <summary>
        /// Read/write - Name of the country
        /// </summary>
        public string CountryName
        {
            get { return countryName; }
            set { countryName = value; }
        }

        /// <summary>
        /// Read/write - IANA code for the country
        /// </summary>
        public string IANACode
        {
            get { return ianaCode; }
            set { ianaCode = value; }
        }

        /// <summary>
        /// Read/write - UIC adming code
        /// </summary>
        public string AdminCodeUIC
        {
            get { return adminCodeUIC; }
            set { adminCodeUIC = value; }
        }

        /// <summary>
        /// Read/write - time zone for the country
        /// </summary>
        public double TimeZone
        {
            get { return timeZone; }
            set { timeZone = value; }

        }
        #endregion

        #region Public methods

        /// <summary>
        /// Creates a deep clone of this TDCountry object
        /// </summary>
        /// <returns></returns>
        public TDCountry Clone()
        {
            TDCountry country = new TDCountry();

            country.adminCodeUIC = this.adminCodeUIC;
            country.countryCode = this.countryCode;
            country.countryName = this.countryName;
            country.ianaCode = this.ianaCode;
            country.timeZone = this.timeZone;

            return country;

        }

        #endregion
    }
}
