// *********************************************** 
// NAME			: InternationalStop.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 26/01/2010
// DESCRIPTION	: Class which contains the information of an international stop
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/InternationalPlanner/InternationalStop.cs-arc  $
//
//   Rev 1.1   Feb 09 2010 09:53:02   mmodi
//Updated
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Jan 29 2010 12:04:40   mmodi
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.InternationalPlanner
{
    /// <summary>
    /// Class which contains the information of an international stop
    /// </summary>
    [Serializable()]
    public class InternationalStop
    {
        #region Private members

        private string stopCode;
        private string stopNaptan;
        private InternationalModeType stopType;
        private string stopName;
        private int stopOSGREasting;
        private int stopOSGRNorthing;
        private string stopTerminal;
        private string stopInfoURL;
        private string stopInfoDesc;
        private string stopDeptURL;
        private string stopDeptDesc;
        private string stopAccessURL;
        private string stopAccessDesc;
        private InternationalCountry stopCountry;

        #endregion

        #region Constructor

        /// <summary>
		/// Constructor
		/// </summary>
        public InternationalStop()
		{			
		}

		#endregion

        #region Public properties

        /// <summary>
        /// Read/Write. The External code to identify an international stop
        /// </summary>
        public string StopCode
        {
            get { return stopCode; }
            set { stopCode = value; }
        }

        /// <summary>
        /// Read/Write. The Internal Naptan code to identify an international stop
        /// </summary>
        public string StopNaptan
        {
            get { return stopNaptan; }
            set { stopNaptan = value; }
        }

        /// <summary>
        /// Read/Write. The mode type of this international stop
        /// </summary>
        public InternationalModeType StopType
        {
            get { return stopType; }
            set { stopType = value; }
        }

        /// <summary>
        /// Read/Write. The name to be used for display for this international stop
        /// </summary>
        public string StopName
        {
            get { return stopName; }
            set { stopName = value; }
        }

        /// <summary>
        /// Read/Write. The OSGR easting for this international stop
        /// </summary>
        public int StopOSGREasting
        {
            get { return stopOSGREasting; }
            set { stopOSGREasting = value; }
        }

        /// <summary>
        /// Read/Write. The OSGR northing for this international stop
        /// </summary>
        public int StopOSGRNorthing
        {
            get { return stopOSGRNorthing; }
            set { stopOSGRNorthing = value; }
        }

        /// <summary>
        /// Read/Write. The terminal number for this international stop
        /// </summary>
        public string StopTerminal
        {
            get { return stopTerminal; }
            set { stopTerminal = value; }
        }

        /// <summary>
        /// Read/Write. The information url for this international stop
        /// </summary>
        public string StopInfoURL
        {
            get { return stopInfoURL; }
            set { stopInfoURL = value; }
        }

        /// <summary>
        /// Read/Write. The information url description to be displayed for this international stop
        /// </summary>
        public string StopInfoDesc
        {
            get { return stopInfoDesc; }
            set { stopInfoDesc = value; }
        }

        /// <summary>
        /// Read/Write. The departures url for this international stop
        /// </summary>
        public string StopDeptURL
        {
            get { return stopDeptURL; }
            set { stopDeptURL = value; }
        }

        /// <summary>
        /// Read/Write. The departures url description to be displayed for this international stop
        /// </summary>
        public string StopDeptDesc
        {
            get { return stopDeptDesc; }
            set { stopDeptDesc = value; }
        }

        /// <summary>
        /// Read/Write. The accessability url for this international stop
        /// </summary>
        public string StopAccessURL
        {
            get { return stopAccessURL; }
            set { stopAccessURL = value; }
        }

        /// <summary>
        /// Read/Write. The accessability url description to be displayed for this international stop
        /// </summary>
        public string StopAccessDesc
        {
            get { return stopAccessDesc; }
            set { stopAccessDesc = value; }
        }

        /// <summary>
        /// Read/Write. The country this international stop belongs to
        /// </summary>
        public InternationalCountry StopCountry
        {
            get { return stopCountry; }
            set { stopCountry = value; }
        }

        #endregion
    }
}
