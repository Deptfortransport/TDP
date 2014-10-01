// *********************************************** 
// NAME                 : FindNearestAssembler.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: Assmbler class for the FindNearest web services.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/FindNearest/v1/FindNearestAssembler.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:26   mturner
//Initial revision.
//
//   Rev 1.4   Feb 03 2006 10:07:12   RWilby
//Added private constructor
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.3   Jan 19 2006 15:56:20   RWilby
//Corrected spelling mistake
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.2   Jan 19 2006 10:00:26   RWilby
//Completed implementation of class.
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.1   Jan 04 2006 12:08:42   mtillett
//Create stubs for data transfer class
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.0   Jan 04 2006 12:07:34   mtillett
//Initial revision.

using System;

using TransportDirect.UserPortal.LocationService;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;


namespace TransportDirect.EnhancedExposedServices.DataTransfer.FindNearest.V1
{
	/// <summary>
	///  Class responible for the inter-conversion of domain types to EnhancedExposedServices types
	/// </summary>
	public sealed class FindNearestAssembler
	{
		//Declare private constructor as static types should not have constructors
		private FindNearestAssembler(){}

		/// <summary>
		/// Creates a NaptanProximity array from a TDNaptan array
		/// </summary>
		/// <param name="gridReference">Original OSGridReference</param>
		/// <param name="tdNaptans">TDNaptan array</param>
		/// <returns>NaptanProximity array</returns>
		public static NaptanProximity[] CreateNaptanPromimityArrayDT(EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference gridReference,TDNaptan[] tdNaptans )
		{
			NaptanProximity[] naptanProximityArray = new NaptanProximity[tdNaptans.Length];

			for(int i =0;i<tdNaptans.Length;i++)
			{
				//Create NaptanProximity object and set the properties
				NaptanProximity naptanProximity = new NaptanProximity();
				naptanProximity.NaptanId = tdNaptans[i].Naptan;
				naptanProximity.Name = tdNaptans[i].Name;
				naptanProximity.GridReference.Easting = tdNaptans[i].GridReference.Easting;
				naptanProximity.GridReference.Northing = tdNaptans[i].GridReference.Northing;

				//Calculate distance in metres from original grid reference
				naptanProximity.Distance = tdNaptans[i].GridReference.DistanceFrom(
					new UserPortal.LocationService.OSGridReference(gridReference.Easting,gridReference.Northing));
				
				//Retrieve optional data from relevant sub class and set NaptanType
				if(tdNaptans[i] is TDAirportNaptan)
				{
					naptanProximity.IATA = (tdNaptans[i] as TDAirportNaptan).IATA;
					naptanProximity.Type = NaptanType.Airport;
				}
				else if (tdNaptans[i] is TDBusStopNaptan)
				{
					naptanProximity.SMSCode = (tdNaptans[i] as TDBusStopNaptan).SMSCode;
					naptanProximity.Type = NaptanType.BusStop;
				}
				else if (tdNaptans[i] is TDRailNaptan)
				{
					naptanProximity.CRSCode = (tdNaptans[i] as TDRailNaptan).CRSCode;
					naptanProximity.Type = NaptanType.Station;
				}
				
				//Add naptanProximity object to array 
				naptanProximityArray[i] = naptanProximity;
			}

			return  naptanProximityArray;
		}
	}
}
