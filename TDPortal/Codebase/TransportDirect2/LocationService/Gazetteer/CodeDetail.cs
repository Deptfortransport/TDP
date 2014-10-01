// *********************************************** 
// NAME             : CodeDetail.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 13 Feb 2014
// DESCRIPTION  	: CodeDetail class used to hold Code gazetteer query results
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// CodeDetail class used to hold Code gazetteer query results
    /// </summary>
    [Serializable]
    public class CodeDetail
    {
        #region Private variables

        private string sNaptanId;
		private TDPCodeType ctCodeType;
		private string sCode;
		private string sDescription;
		private string sLocality;
		private string sRegion;
		private int nEasting;
		private int nNorthing;
		private TDPModeType mtModeType;

        #endregion

        #region Constructor

        /// <summary>
		/// Default Constructor
		/// </summary>
        public CodeDetail()
		{
			sNaptanId = string.Empty;
			ctCodeType = TDPCodeType.CRS;
			sCode = string.Empty;
			sDescription = string.Empty;
			sLocality = string.Empty;
			sRegion = string.Empty;
			nEasting = -1;
			nNorthing = -1;
			mtModeType = TDPModeType.Unknown;
		}

        #endregion

        #region Public properties

        /// <summary>
		/// Read-Write Property. NaptanId information
		/// </summary>
		public string NaptanId
		{
			get{ return sNaptanId;}
			set{ sNaptanId = value;}
		}

		/// <summary>
		/// Read-Write Property. CodeType information
		/// </summary>
		public TDPCodeType CodeType
		{
			get{ return ctCodeType;}
			set{ ctCodeType = value;}
		}

		/// <summary>
		/// Read-Write Property. Code information
		/// </summary>
		public string Code
		{
			get{ return sCode;}
			set{ sCode = value;}
		}

		/// <summary>
		/// Read-Write Property. Description information
		/// </summary>
		public string Description
		{
			get{ return sDescription;}
			set{ sDescription = value;}
		}

		/// <summary>
		/// Read-Write Property. Locality information
		/// </summary>
		public string Locality
		{
			get{ return sLocality;}
			set{ sLocality = value;}
		}

		/// <summary>
		/// Read-Write Property. Region information
		/// </summary>
		public string Region
		{
			get{ return sRegion;}
			set{ sRegion = value;}
		}

		/// <summary>
		/// Read-Write Property. Easting coordinate information
		/// </summary>
		public int Easting
		{
			get{ return nEasting;}
			set{ nEasting = value;}
		}

		/// <summary>
		/// Read-Write Property. Northing coordinate information
		/// </summary>
		public int Northing
		{
			get{ return nNorthing;}
			set{ nNorthing = value;}
		}

		/// <summary>
		/// Read-Write Property. ModeType information. What mode of transport this entry applies to!
		/// </summary>
		public TDPModeType ModeType
		{
			get{ return mtModeType;}
			set{ mtModeType = value;}
        }

        #endregion
    }
}