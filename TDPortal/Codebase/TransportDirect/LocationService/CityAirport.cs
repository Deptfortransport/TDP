// *********************************************** 
// NAME                 : CityAirport
// AUTHOR               : Murat Guney
// DATE CREATED         : 14/02/2006
// DESCRIPTION			: CityAirport class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CityAirport.cs-arc  $ 
//
//   Rev 1.1   Apr 29 2010 09:34:28   apatel
//Updated to enable deserialisation of feedbacks as at present feedback page comes with deserialisation error for journey result.
//Resolution for 5469: Problem with feedback viewer
//
//   Rev 1.0   Nov 08 2007 12:25:02   mturner
//Initial revision.
//
//   Rev 1.0   Feb 15 2006 10:31:26   mguney
//Initial revision.

using System;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for CityAirport.
	/// </summary>
	[Serializable]
	public class CityAirport : CityInterchange
	{

		#region Private members
		TDNaptan airportNaptan;
		ModeType[] combinableModes;
		bool useDirect;
		bool useCombined;
		#endregion

        /// <summary>
        /// Parameterless constructor to aid in serialisation
        /// </summary>
        public CityAirport()
        {
            this.combinableModes = new ModeType[0];
            this.useDirect = false;
            this.useCombined = true;
            this.StationType = StationType.Airport;
        }

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CityAirport(ModeType[] modes,bool useDirect,bool useCombined,TDNaptan naptan)
		{
			this.combinableModes = modes;
			this.useDirect = useDirect;
			this.useCombined = useCombined;
			this.airportNaptan = naptan;
			this.StationType = naptan.StationType;
		}

		/// <summary>
		/// Airport naptan.[r]
		/// </summary>
		public TDNaptan AirportNaptan
		{
			get {return airportNaptan;}			
		}

		/// <summary>
		/// Combinable modes array. [r]
		/// </summary>
		public ModeType[] CombinedModes
		{
			get {return combinableModes;}			
		}

		/// <summary>
		/// Use direct flag. [r]
		/// </summary>
		public bool UseDirect
		{
			get {return useDirect;}
		}

		/// <summary>
		/// Use combined flag. [r]
		/// </summary>
		public bool UseCombined
		{
			get {return useCombined;}
		}

	}
}
