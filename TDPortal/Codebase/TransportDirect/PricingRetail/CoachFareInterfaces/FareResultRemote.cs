//********************************************************************************
//NAME         : FareResultRemote.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of FareResultRemote class. Class for storing external fare result information. (web services)
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/FareResultRemote.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:30   mturner
//Initial revision.
//
//   Rev 1.4   Oct 13 2005 14:53:34   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 12 2005 14:42:52   mguney
//XMLSerialisation attribute added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 12 2005 14:38:22   mguney
//XMLSerialisation attribute added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:46   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:56   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Class for storing external fare result information. (web services)
	/// </summary>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info")]
	public class FareResultRemote
	{
		#region Private variables
		private CoachFareRemoteForRoute[] fares;
		private int[] errorCodeList;
		private string requestId;
		#endregion

		#region Public Properties
		/// <summary>
		/// Array of returned coach fares. [rw]
		/// </summary>
		public CoachFareRemoteForRoute[] Fares
		{
			get {return fares;}
			set {fares = value;}
		}
		
		/// <summary>
		/// Array of returned error codes. [rw]
		/// </summary>
		public int[] ErrorCodeList
		{
			get {return errorCodeList;}
			set {errorCodeList = value;}
		}

		/// <summary>
		/// Request id for reference. [rw]
		/// </summary>
		public string RequestId
		{
			get {return requestId;}
			set {requestId = value;}
		}

		#endregion

	}
}