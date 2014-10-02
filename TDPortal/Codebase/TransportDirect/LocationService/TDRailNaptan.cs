// *********************************************** 
// NAME			: TDRailNaptan.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 11/01/06
// DESCRIPTION	: Represents a specific Rail naptan
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDRailNaptan.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:24   mturner
//Initial revision.
//
//   Rev 1.1   Feb 02 2006 17:42:50   RWilby
//Corrected bug in overloaded constructor
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.0   Jan 19 2006 09:45:10   RWilby
//Initial revision.
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Represents a specific Rail naptan
	/// </summary>
	[Serializable()]
	public class TDRailNaptan : TDNaptan
	{
		private string crsCode;

		/// <summary>
		/// Default constructor
		/// </summary>
		public TDRailNaptan() : base()
		{
		}

		/// <summary>
		/// Overloaded constructor
		/// </summary>
		/// <param name="naptan">TDNaptan</param>
		/// <param name="crsCode">CRS code</param>
		public TDRailNaptan(TDNaptan naptan, string crsCode) : base( naptan.Naptan, naptan.GridReference, naptan.Name)
		{
			this.crsCode = crsCode;
		}
		
		/// <summary>
		/// Readonly property. CRSCode
		/// </summary>
		public string CRSCode
		{
			get
			{
				return crsCode;
			}
		}
	}
}