// *********************************************** 
// NAME                 : TDCodeDetail.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 29/12/2004
// DESCRIPTION  : Output format for returned information. 
// Gives localization information for a particular code-NaptanId association
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDCodeDetail.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:22   mturner
//Initial revision.
//
//   Rev 1.0   Jan 18 2005 17:38:32   passuied
//Initial revision.
//
//   Rev 1.0   Dec 30 2004 14:34:18   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Output format for returned information.
	/// Gives localization information for a particular code-NaptanId association
	/// </summary>
	[Serializable]
	public class TDCodeDetail
	{
		private string sNaptanId;
		private TDCodeType ctCodeType;
		private string sCode;
		private string sDescription;
		private string sLocality;
		private string sRegion;
		private int nEasting;
		private int nNorthing;
		private TDModeType mtModeType;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public TDCodeDetail()
		{
			sNaptanId = string.Empty;
			ctCodeType = TDCodeType.CRS;
			sCode = string.Empty;
			sDescription = string.Empty;
			sLocality = string.Empty;
			sRegion = string.Empty;
			nEasting = -1;
			nNorthing = -1;
			mtModeType = TDModeType.Undefined;
		}

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
		public TDCodeType CodeType
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
		public TDModeType ModeType
		{
			get{ return mtModeType;}
			set{ mtModeType = value;}
		}
	}
}
