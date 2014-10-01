// *********************************************** 
// NAME                 : DepartureBoardServiceInformation.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Class for Departure BoardService Itinerary
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceItinerary.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:20   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:30:48   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 16:24:20   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Dec 14 2005 15:38:34   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Class for Departure BoardService Itinerary
	/// </summary>
	[System.Serializable]
	public class DepartureBoardServiceItinerary
	{
		private string sOperatorCode;
		private string sOperatorName;
		private string sServiceNumber;

		/// <summary>
		/// Default constructor to initialise varaible to their default state
		/// </summary>
		public DepartureBoardServiceItinerary()
		{
			sOperatorCode = string.Empty;
			sOperatorName = string.Empty;
			sServiceNumber = string.Empty;
		}

		/// <summary>
		/// Read-Write property for Departure Board ServiceOperator Code
		/// </summary>
		public string OperatorCode
		{
			get{ return sOperatorCode;}
			set{ sOperatorCode = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Operator Name
		/// </summary>
		public string OperatorName
		{
			get{ return sOperatorName;}
			set{ sOperatorName = value;}
		}

		/// <summary>
		/// Read-Write property for Departure Board Service Number (Train/Bus Service Number)
		/// </summary>
		public string ServiceNumber
		{
			get{ return sServiceNumber;}
			set{ sServiceNumber = value;}
		}
	}
}
