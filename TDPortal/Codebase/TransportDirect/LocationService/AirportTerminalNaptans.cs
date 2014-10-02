// *********************************************** 
// NAME			: AirportTerminalNaptans.cs
// AUTHOR		: Tim Mollart
// DATE CREATED	: 16/02/2007
// DESCRIPTION	: Gets Naptans for airport terminals
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/AirportTerminalNaptans.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:58   mturner
//Initial revision.
//
//   Rev 1.0   Mar 13 2007 14:19:18   tmollart
//Initial revision.


using System;
using System.Collections;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Class to return an array of airport terminal naptans
	/// </summary>
	public class AirportTerminalNaptans
	{

		/// <summary>
		/// Air Data Provider
		/// </summary>
		IAirDataProvider airData;
		
		/// <summary>
		/// Naptan Prefix Length
		/// </summary>
		private const int PREFIX_LENGTH	= 4;

		/// <summary>
		/// IATA code length e.g. LHR, LGW, BHX etc
		/// </summary>
		private const int IATA_LENGTH = 3;

		/// <summary>
		/// Terminal code length
		/// </summary>
		private const int TERMINAL_LENGTH = 1;


		/// <summary>
		/// Constructor
		/// </summary>
		public AirportTerminalNaptans()
		{
			airData = (AirDataProvider.IAirDataProvider) TDServiceDiscovery.Current[ServiceDiscoveryKey.AirDataProvider];
		}

		/// <summary>
		/// Gets terminal naptans
		/// </summary>
		/// <param name="naptans">Array of Naptans to look up</param>
		/// <returns>Array of airport terminal naptans</returns>
		public TDNaptan[] GetTerminalNaptans(TDNaptan[] naptans)
		{
			ArrayList newNaptanList = new ArrayList();

			foreach (TDNaptan naptan in naptans)
			{
				if	(naptan.StationType == StationType.Airport)
				{
					if  (naptan.Naptan.Length == (PREFIX_LENGTH + IATA_LENGTH + TERMINAL_LENGTH)
						&& naptan.Naptan[PREFIX_LENGTH + IATA_LENGTH + TERMINAL_LENGTH - 1] == '0')
					{
						// strip off dummy terminal id so we can handle it like other groups 
						naptan.Naptan = naptan.Naptan.Substring(0, PREFIX_LENGTH + IATA_LENGTH);
					}

					if  (naptan.Naptan.Length == (PREFIX_LENGTH + IATA_LENGTH))
					{
						Airport airport = airData.GetAirport(naptan.Naptan.Substring(PREFIX_LENGTH, IATA_LENGTH));

						if	(airport != null) 
						{
							foreach (string n in airport.Naptans)
							{
								newNaptanList.Add(new TDAirportNaptan(new TDNaptan(n, naptan.GridReference, airport.Name)));
							}							
						}
						
						else
						{
							// if we cannot find airport in our database table, it's because the airport's
							//  been 'thrown away' for having no domestic flights -- in this case assume
							//  it's a little one with just one terminal ...
							newNaptanList.Add(new TDAirportNaptan(new TDNaptan(naptan.Naptan + "1", naptan.GridReference, naptan.Name)));
						}
					}
					else
					{
						newNaptanList.Add(naptan);
					}
				}
				else
				{
					newNaptanList.Add(naptan);
				}
			}

			return (TDNaptan[])(newNaptanList.ToArray(typeof(TDNaptan)));
		}
	}
}
