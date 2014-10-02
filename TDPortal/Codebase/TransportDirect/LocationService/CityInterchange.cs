// *********************************************** 
// NAME                 : CityInterchange
// AUTHOR               : Murat Guney
// DATE CREATED         : 14/02/2006
// DESCRIPTION			: Provides a base class for station classes.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CityInterchange.cs-arc  $ 
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
using TransportDirect.JourneyPlanning.CJPInterface;
using System.Xml.Serialization;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for CityInterchange.
	/// </summary>
	[Serializable]
    [XmlInclude(typeof(CityCoachStation))]
    [XmlInclude(typeof(CityRailStation))]
    [XmlInclude(typeof(CityAirport))]
	public class CityInterchange
	{
		#region Private members
		private StationType interchangeStationType;
		#endregion
		
		/// <summary>
		/// Default constructor.
		/// </summary>
		public CityInterchange()
		{
			
		}

		/// <summary>
		/// Station type for the interchange.[rw]
		/// </summary>
		public StationType StationType
		{
			get {return interchangeStationType;}
			set {interchangeStationType = value;}
		}

		/// <summary>
		/// Factory method for creating different city interchange locations according to the station type.
		/// </summary>
		/// <param name="modes"></param>
		/// <param name="useDirect"></param>
		/// <param name="useCombined"></param>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public static CityInterchange CreateCityInterchange(ModeType[] modes,bool useDirect,bool useCombined,
			TDNaptan naptan)
		{
			//create and return city interchange locations according to the station type.
			StationType naptanStationType = naptan.StationType;
			switch (naptanStationType)
			{
				case StationType.Coach:
					return new CityCoachStation(naptan);
					
				case StationType.Rail:
					return new CityRailStation(naptan);
					
				case StationType.Airport:
					return new CityAirport(modes,useDirect,useCombined,naptan);					
			}

			return null;
		}


	}
}
