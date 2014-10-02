// *********************************************** 
// NAME			: CarParkAccessPoint.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 03/08/2006 
// DESCRIPTION	: Implementation of Access Points for a car park
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CarParkAccessPoint.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:00   mturner
//Initial revision.
//
//   Rev 1.2   Oct 20 2006 15:25:10   mmodi
//Added Default constructor to resolve xmlserialization issue
//Resolution for 4229: Transaction Injector fails with Car Parks
//
//   Rev 1.1   Aug 29 2006 13:43:36   esevern
//Added BarrierInOperation to constructor and properties
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 03 2006 14:04:00   mmodi
//Initial revision.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2


using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// AccessPoints for a car park containing type, easting, northing, and streetname.
	/// </summary>
	[Serializable()]
	public class CarParkAccessPoint
	{
		private readonly string geocodeType;
		private readonly OSGridReference osGridReference;
		private readonly string streetName;
		private readonly string barrierInOperation;

		// constants for string comparison
		public const string BARRIER_Y = "Yes";
		public const string BARRIER_N = "No";


		/// <summary>
		/// Default constructor
		/// </summary>
		public CarParkAccessPoint()
		{
		}

		/// <summary>
		/// Constructor when Street name is not availble
		/// </summary>
		/// <param name="geocodeType">Geocode Type e.g. Map, Entrance, Exit</param>
		/// <param name="osGridReference">OSGridReference value</param>
		public CarParkAccessPoint(string geocodeType, OSGridReference osGridReference)
		{
			this.geocodeType = geocodeType;
			this.osGridReference = osGridReference;
			this.streetName = null;
		}

		/// <summary>
		/// Alternate constructor when Street name is availble
		/// </summary>
		/// <param name="geocodeType">Geocode Type e.g. Map, Entrance, Exit</param>
		/// <param name="osGridReference">OSGridReference value</param>
		/// <param name="streetName">Street name</param>
		/// <param name="barrierInOperation">barrier in operation value</param>
		public CarParkAccessPoint(string geocodeType, OSGridReference osGridReference, 
			string streetName, string barrierInOperation)
		{
			this.geocodeType = geocodeType;
			this.osGridReference = osGridReference;
			this.streetName = streetName;
			this.barrierInOperation = barrierInOperation;
		}

		/// <summary>
		/// Readonly property. GeocodeType value.
		/// </summary>
		public string GeocodeType
		{
			get { return geocodeType; }
		}

		/// <summary>
		/// Readonly property. OSGrid Reference value.
		/// </summary>
		public OSGridReference GridReference
		{
			get { return osGridReference; }
		}

		/// <summary>
		/// Readonly property. Street name value.
		/// </summary>
		public string StreetName
		{
			get { return streetName; }
		}

		/// <summary>
		/// Read only property. Get the barrier in operation indicator
		/// </summary>
		public bool BarrierInOperation
		{
			get 
			{ 
				// compare the strings ignoring case
				if(string.Compare(barrierInOperation, BARRIER_Y, true) == 0) 
					return true;
				else
					return false; 
			}
		}
	}
}
