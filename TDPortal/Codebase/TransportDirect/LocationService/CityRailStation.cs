// *********************************************** 
// NAME                 : CityRailStation
// AUTHOR               : Murat Guney
// DATE CREATED         : 14/02/2006
// DESCRIPTION			: CityRailStation class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CityRailStation.cs-arc  $ 
//
//   Rev 1.1   Apr 29 2010 09:34:26   apatel
//Updated to enable deserialisation of feedbacks as at present feedback page comes with deserialisation error for journey result.
//Resolution for 5469: Problem with feedback viewer
//
//   Rev 1.0   Nov 08 2007 12:25:04   mturner
//Initial revision.
//
//   Rev 1.0   Feb 15 2006 10:31:26   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for CityRailStation.
	/// </summary>
	[Serializable]
	public class CityRailStation : CityInterchange
	{
		TDNaptan railNaptan;

        /// <summary>
        /// Parameterless constructor to aid in serialisation
        /// </summary>
        public CityRailStation()
        {
            this.StationType = StationType.Rail;
        }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CityRailStation(TDNaptan naptan)
		{
			railNaptan = naptan;
			this.StationType = naptan.StationType;
		}

		/// <summary>
		/// Rail naptan.[r]
		/// </summary>
		public TDNaptan RailNaptan
		{
			get {return railNaptan;}
			set {railNaptan = value;}
		}
	}
}
