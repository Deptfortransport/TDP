// *********************************************** 
// NAME			: OSGridReference.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the OSGridReference class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/OSGridReference.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:16   mturner
//Initial revision.
//
//   Rev 1.5   Aug 19 2005 14:04:46   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.4.1.0   Jul 22 2005 14:49:04   rgreenwood
//DN62/DD0073 Map Details: Added IsValid property
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.4   Jul 22 2004 10:14:08   RPhilpott
//GetDistance now returns int, not double.
//
//   Rev 1.3   Jul 09 2004 13:09:16   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.2   Oct 11 2003 19:34:38   RPhilpott
//Correct initialisation in default ctors.
//
//   Rev 1.1   Sep 11 2003 16:33:58   jcotton
//Made Class Serializable
//
//   Rev 1.0   Sep 05 2003 15:30:34   passuied
//Initial Revision
//
//   Rev 1.1   Aug 20 2003 17:55:48   AToner
//Work in progress
using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for OSGridReference.
	/// </summary>
	[Serializable()]
	public class OSGridReference
	{
		private int easting;
		private int northing;

		/// <summary>
		/// Default constructor
		/// </summary>
		public OSGridReference()
		{
			this.Easting = -1;
			this.Northing = -1;
		}

		/// <summary>
		/// Alternate constructor
		/// </summary>
		/// <param name="easting">easting value</param>
		/// <param name="northing">northing value</param>
		public OSGridReference( int easting, int northing )
		{
			this.Easting = easting;
			this.Northing = northing;
		}

		/// <summary>
		/// Get/Set property. Easting value.
		/// </summary>
		public int Easting
		{
			get { return easting; }
			set { easting = value; }
		}

		/// <summary>
		/// Get/Set propety. Northing value.
		/// </summary>
		public int Northing
		{
			get { return northing; }
			set { northing = value; }
		}

		/// <summary>
		/// Get Distance in metres between the current instance 
		/// and the supplied other OSGR
		/// </summary>
		/// <param name="osgr">supplied OSGR</param>
		/// <returns>distance in metres</returns>
		public int DistanceFrom( OSGridReference osgr)
		{
			return (int) (Math.Sqrt(
				Math.Pow((this.Easting - osgr.Easting), 2)
				+	Math.Pow((this.Northing - osgr.Northing), 2)));
		}

		/// <summary>
		/// Read-only property that returns true if both the Easting & Northing co-ords are both greater than 0 (valid).
		/// </summary>
		public bool IsValid
		{
			get 
			{
				return (Easting > 0 && Northing > 0);
			}
		}

	}
}
