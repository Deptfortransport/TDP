// *********************************************** 
// NAME                 : TripInfo.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 31/12/2004
// DESCRIPTION  : Class for Trip Information Request
// ************************************************ 
//
//   Rev 1.0   Dec 31 2004 14:24:10   schand
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// Summary description for TripInfo.
	/// </summary>
	[Serializable]
	public class TripInfo
	{
		private string sMsgID;  
		private string sOriginNaptan;
		private string sDestinationNaptan;
		private string sServiceNumber;
		private DateTime dRequestedTime;
		private int iRange;
		private bool bShowDepartures;
		private bool bShowCallingStops;


		public TripInfo()
		{	
			// initialising parameters
			sMsgID = string.Empty;
			sOriginNaptan = string.Empty ; 
			sDestinationNaptan = string.Empty ;
			sServiceNumber = string.Empty ;
			dRequestedTime = DateTime.MinValue ;
			bShowDepartures = false ;
			bShowCallingStops = false ;
		}


		
		public string MsgID
		{
			get{return (string)sMsgID;}
			set {sMsgID = (string)value;}
		}
  

		public string OriginNaptan
		{
			get{return (string)sOriginNaptan;}
			set{sOriginNaptan = (string)value;}
		}


		public string DestinationNaptan
		{
			get{return (string) sDestinationNaptan;}
			set{sDestinationNaptan = (string)value;}
		}


		public string ServiceNumber
		{
			get{return (string) sServiceNumber;}
			set{sServiceNumber = (string)value;}
		}


		public DateTime RequestedTime
		{
				get{return (DateTime) dRequestedTime;}
				set{dRequestedTime = (DateTime) value;}
		}

		
		public int Range
		{
			get{return (int) iRange ;}
			set{iRange = (int) value;}
		}


		public bool ShowDepartures
		{
			get{return (bool) bShowDepartures;}
			set{bShowDepartures = (bool)value ;}
		}


		public bool ShowCallingStops
		{
			get{return (bool) bShowCallingStops;}
			set{bShowCallingStops = (bool)value;}
		}

		
	}
}
