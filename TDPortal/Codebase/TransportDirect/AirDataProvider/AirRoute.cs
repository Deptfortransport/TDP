// *********************************************** 
// NAME			: AirRoute.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 12/05/2004
// DESCRIPTION	: Represents a specific air route, eg Heathrow - Glasgow
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/AirRoute.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:22   mturner
//Initial revision.
//
//   Rev 1.3   Jun 09 2004 17:01:22   jgeorge
//Fixed bug in Equals method
//
//   Rev 1.2   May 20 2004 11:32:32   jgeorge
//Added Serializable attribute so class can be used with Session Manager.
//
//   Rev 1.1   May 13 2004 09:28:16   jgeorge
//Modified namespace to TransportDirect.UserPortal.AirDataProvider
//
//   Rev 1.0   May 12 2004 15:59:52   jgeorge
//Initial revision.

using System;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Represents a specific air route, eg Heathrow - Glasgow
	/// </summary>
	[Serializable]
	public class AirRoute
	{
		private string originAirport;
		private string destinationAirport;

		/// <summary>
		/// Constructor - requires the IATA codes of the origin and
		/// destination airports.
		/// </summary>
		/// <param name="originAirport"></param>
		/// <param name="destinationAirport"></param>
		public AirRoute(string originAirport, string destinationAirport)
		{
			this.originAirport = originAirport;
			this.destinationAirport = destinationAirport;
		}
		
		/// <summary>
		/// IATA Code of the origin airport
		/// </summary>
		public string OriginAirport 
		{
			get { return originAirport; }
		}

		/// <summary>
		/// IATA Code of the destination airport
		/// </summary>
		public string DestinationAirport 
		{
			get { return destinationAirport; }
		}

		/// <summary>
		/// Combination of origin and destination airport IATA Codes
		/// </summary>
		public string CompoundName 
		{
			get { return String.Concat(originAirport, destinationAirport); }
		}

		/// <summary>
		/// Overridden Equals method to enable an AirRoute to be used as a key
		/// for a hashtable.
		/// Two routes are equal if their origin and destination airports match
		/// either way round, eg LHR-GLA = GLA-LHR
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public override bool Equals(object obj)
		{
			if (obj is AirRoute)
			{
				AirRoute objar = (AirRoute)obj;
				return ( (objar.OriginAirport == this.OriginAirport) && (objar.DestinationAirport == this.DestinationAirport) ) ||
					( (objar.OriginAirport == this.DestinationAirport) && (objar.DestinationAirport == this.OriginAirport) );
				}
			else
				return false;
		}

		/// <summary>
		/// Overridden GetHashCode method to enable an AirRoute to be used as a key
		/// for a hashtable. HashCode is the sum of the codes for origin and destination
		/// airports, meaning that two routes will have the same hashcode regardless of
		/// which way round their origin and destination airports are.
		/// </summary>
		/// <returns></returns>
		public override int GetHashCode() 
		{
			return originAirport.GetHashCode() + destinationAirport.GetHashCode();
		}

	}
}

