// *********************************************** 
// NAME                 : NaptanProximity.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: NaptanProximity Data Transfer class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/FindNearest/v1/NaptanProximity.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:26   mturner
//Initial revision.
//
//   Rev 1.3   Jan 19 2006 15:56:52   RWilby
//Corrected spelling mistake
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.2   Jan 19 2006 09:54:46   RWilby
//Completed implementation of class.
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.1   Jan 04 2006 12:08:40   mtillett
//Create stubs for data transfer class
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.0   Jan 04 2006 12:07:34   mtillett
//Initial revision.

using System;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.FindNearest.V1
{
	/// <summary>
	/// NaptanProximity Data Transfer class
	/// </summary>
	[Serializable]
	public class NaptanProximity
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public NaptanProximity()
		{
		}
		
		private string naptanId;
		private string name;
		private OSGridReference gridReference = new OSGridReference();
		private int distance;
		private NaptanType type;
		private string smsCode;
		private string crsCode;
		private string iata;

		/// <summary>
		/// Read/Write Property. NaptanId
		/// </summary>
		public string NaptanId
		{
			get{return naptanId;}
			set{naptanId = value;}
		}
	
		/// <summary>
		/// Read/Write Property. Name
		/// </summary>
		public string Name
		{
			get{return name;}
			set{name=value;}
		}
	
		/// <summary>
		/// Read/Write Property. OSGridReference
		/// </summary>
		public OSGridReference GridReference
		{
			get{return gridReference;}
			set{gridReference = value;}
		}
	
		/// <summary>
		/// Read/Write Property. Distance in metres
		/// </summary>
		public int Distance
		{
			get{return distance;}
			set{distance = value;}
		}
	
		/// <summary>
		/// Read/Write Property. NaptanType
		/// </summary>
		public NaptanType Type
		{
			get{return type;}
			set{type = value;}
		}	

		/// <summary>
		/// Read/Write Property. SMSCode Property used by TDBusStopNaptan
		/// </summary>
		public string SMSCode
		{
			get{return smsCode;}
			set{smsCode = value;}
		}	

		/// <summary>
		/// Read/Write Property. CRSCode Property used by TDRailNaptans
		/// </summary>
		public string CRSCode
		{
			get{return crsCode;}
			set{crsCode = value;}
		}
	
		/// <summary>
		/// Read/Write Property. IATA Property used by TDAirportNaptan
		/// </summary>
		public string IATA
		{
			get{return iata;}
			set{iata = value;}
		}	
	}
}
