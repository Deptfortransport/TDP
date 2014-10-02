// *********************************************** 
// NAME                 : CityCoachStation
// AUTHOR               : Murat Guney
// DATE CREATED         : 14/02/2006
// DESCRIPTION			: CityCoachStation class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CityCoachStation.cs-arc  $ 
//
//   Rev 1.1   Apr 29 2010 09:34:28   apatel
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
	/// Summary description for CityCoachStation.
	/// </summary>
	[Serializable]
	public class CityCoachStation : CityInterchange
	{
		TDNaptan coachNaptan;

        /// <summary>
        /// Parameterless constructor added for deserialisation
        /// </summary>
        public CityCoachStation()
        {
            coachNaptan = null;
            this.StationType = StationType.Coach;
        }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CityCoachStation(TDNaptan naptan)
		{
			coachNaptan = naptan;
			this.StationType = naptan.StationType;
		}

		/// <summary>
		/// Coach naptan.[r]
		/// </summary>
		public TDNaptan CoachNaptan
		{
			get {return coachNaptan;}			
		}
	}
}
