// *********************************************** 
// NAME			: InternationalCity.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 03/02/2010
// DESCRIPTION	: Class which contains the information of an international city
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalCity.cs-arc  $
//
//   Rev 1.1   Feb 09 2010 09:53:14   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Feb 04 2010 10:27:58   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Class which contains the information of an international city
    /// </summary>
    [Serializable()]
    public class InternationalCity
    {
        #region Private members

        private string cityID;
        private string cityName;
        private int cityOSGREasting;
        private int cityOSGRNorthing;
        private string cityInfoURL;
        private InternationalCountry cityCountry;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalCity()
		{			
		}

		#endregion

        #region Public properties

        /// <summary>
        /// Read/Write. The City ID to identify an international city
        /// </summary>
        public string CityID
        {
            get { return cityID; }
            set { cityID = value; }
        }

        /// <summary>
        /// Read/Write. The name to be used for display for this international city
        /// </summary>
        public string CityName
        {
            get { return cityName; }
            set { cityName = value; }
        }

        /// <summary>
        /// Read/Write. The OSGR easting for this international city
        /// </summary>
        public int CityOSGREasting
        {
            get { return cityOSGREasting; }
            set { cityOSGREasting = value; }
        }

        /// <summary>
        /// Read/Write. The OSGR northing for this international city
        /// </summary>
        public int CityOSGRNorthing
        {
            get { return cityOSGRNorthing; }
            set { cityOSGRNorthing = value; }
        }

        /// <summary>
        /// Read/Write. The information url for this international stop
        /// </summary>
        public string CityInfoURL
        {
            get { return cityInfoURL; }
            set { cityInfoURL = value; }
        }

        /// <summary>
        /// Read/Write. The country this international city belongs to
        /// </summary>
        public InternationalCountry CityCountry
        {
            get { return cityCountry; }
            set { cityCountry = value; }
        }

        #endregion
    }
}
