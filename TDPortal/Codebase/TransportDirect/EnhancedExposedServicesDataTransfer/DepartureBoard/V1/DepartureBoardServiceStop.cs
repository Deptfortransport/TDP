// *********************************************** 
// NAME                 : DepartureBoardServiceStop.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Departure Board Service Stop Class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceStop.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:24   mturner
//Initial revision.
//
//   Rev 1.0   Jan 27 2006 16:30:50   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 16:24:38   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Dec 14 2005 15:38:38   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements

using System;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Departure Board Service Stop Class
	/// </summary>
	[System.Serializable]
	public class DepartureBoardServiceStop
	{
		#region  Private Members		
		private string sNaptanId;
		private string sName;
		private string sShortCode;
		#endregion        

		#region Constructor

		/// <summary>
		/// Default constructor to initialise varaible to their default state
		/// </summary>
		public DepartureBoardServiceStop()
		{
			sNaptanId = string.Empty;
			sName = string.Empty;
			sShortCode = string.Empty; 
		}
		#endregion


		#region  Public Properties

		/// <summary>
		/// Read-Write property for Stop Naptan Identifier 
		/// </summary>
		public string NaptanId
		{
			get{ return sNaptanId;}
			set{ sNaptanId = value;}
		}

		
		/// <summary>
		/// Read-Write property for Stop Name
		/// </summary>
		public string Name
		{
			get{ return sName;}
			set{ sName = value;}
		}

		
		/// <summary>
		/// Read-Write property for Stop Short Code
		/// </summary>
		public string ShortCode
		{
			get{ return sShortCode;}
			set{ sShortCode = value;}
		}
		#endregion

	}
}
