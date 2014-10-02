// *********************************************** 
// NAME			: NPTSAdminDistrict.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 07/08/2006 
// DESCRIPTION	: Class which holds the information for an NPTG Admin and District
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/NPTGAdminDistrict.cs-arc  $
//
//   Rev 1.1   Dec 05 2012 14:10:48   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Nov 08 2007 12:25:14   mturner
//Initial revision.
//
//   Rev 1.0   Aug 08 2006 10:06:50   mmodi
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Stores information about the Admin and District
	/// </summary>
	[Serializable()]
	public class NPTGAdminDistrict
	{
		private readonly string nptgAdminCode;
		private readonly string nptgDistrictCode;

		/// <summary>
		/// Default constructor
		/// </summary>
		public NPTGAdminDistrict()
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
        /// <param name="nptgAdminCode">Admin code</param>
        /// <param name="nptgDistrictCode">District code</param>
		public NPTGAdminDistrict(string nptgAdminCode, string nptgDistrictCode)
		{
			this.nptgAdminCode = nptgAdminCode;
			this.nptgDistrictCode = nptgDistrictCode;
		}

		#region Public properties

		/// <summary>
		/// Readonly property. NPTG Admin Code value.
		/// </summary>
		public string NPTGAdminCode
		{
			get { return nptgAdminCode; }
		}

		/// <summary>
		/// Readonly property. NPTG District Code value.
		/// </summary>
		public string NPTGDistrictCode
		{
			get { return nptgDistrictCode; }
		}

		#endregion

        #region Public methods

        /// <summary>
        /// Returns true if both admin and district are populated
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            return !string.IsNullOrEmpty(nptgAdminCode) && !string.IsNullOrEmpty(nptgDistrictCode);
        }

        #endregion
    }
}
