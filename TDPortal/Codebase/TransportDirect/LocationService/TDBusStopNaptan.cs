// *********************************************** 
// NAME			: TDBusStopNaptan.cs
// AUTHOR		: Russell Wilby
// DATE CREATED	: 11/01/06
// DESCRIPTION	: Represents a specific BusStop naptan
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDBusStopNaptan.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:20   mturner
//Initial revision.
//
//   Rev 1.0   Jan 19 2006 09:45:10   RWilby
//Initial revision.
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Represents a specific BusStop naptan
	/// </summary>
	[Serializable()]
	public class TDBusStopNaptan : TDNaptan
	{
		private string smsCode;

		/// <summary>
		/// Default constructor
		/// </summary>
		public TDBusStopNaptan() : base()
		{
		}
		
		/// <summary>
		/// Overloaded constructor
		/// </summary>
		/// <param name="naptan">TDNaptan</param>
		/// <param name="smsCode">SMS code</param>
		public TDBusStopNaptan(TDNaptan naptan,string smsCode) : base( naptan.Naptan, naptan.GridReference, naptan.Name)
		{
			this.smsCode = smsCode;
		}
		
		/// <summary>
		/// Readonly property. SMSCode
		/// </summary>
		public string SMSCode
		{
			get
			{
				return smsCode;
			}
		}
	}
}