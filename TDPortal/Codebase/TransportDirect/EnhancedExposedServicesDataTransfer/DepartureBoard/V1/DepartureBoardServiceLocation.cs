// *********************************************** 
// NAME                 : DepartureBoardServiceLocation.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Class for Departure Board Service Location information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardServiceLocation.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:22   mturner
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
	/// Class for Departure Board Service Location information
	/// </summary>
	[System.Serializable]
	public class DepartureBoardServiceLocation
	{
		#region  Private Members		
		private string[] sNaptanIds;
		private string sLocality;
		private string sCode;
		private DepartureBoardServiceCodeType ctType;
		private bool isValid;
		#endregion


		#region Constructor

		/// <summary>
		/// Default constructor to initialise varaible to their default state
		/// </summary>
		public DepartureBoardServiceLocation()
		{
			sLocality = string.Empty;
			sCode = string.Empty;
			sNaptanIds = new string[0];
			ctType = DepartureBoardServiceCodeType.CRS;
			isValid = false;
		}
		#endregion

		#region  Public Properties
		/// <summary>
		/// Read-Write property for Location Naptan Id
		/// </summary>
		public string[] NaptanIds
		{
			get{ return sNaptanIds;}
			set{ sNaptanIds = value;}
		}

		/// <summary>
		/// Read-Write property for Location locatlity name
		/// </summary>
		public string Locality
		{
			get{ return sLocality;}
			set{ sLocality = value;}
		}

		/// <summary>
		/// Read-Write property for Location Code
		/// </summary>
		public string Code
		{
			get{ return sCode;}
			set{ sCode = value;}
		}
	
		/// <summary>
		/// Read-Write property for Location Code Type (CRS, SMS etc)
		/// </summary>
		public  DepartureBoardServiceCodeType Type
		{
			get{ return ctType;}
			set{ ctType = value;}
		}

		/// <summary>
		/// Read-Write property to indicate whether Location instance is valid.
		/// </summary>
		public bool Valid
		{
			get{ return isValid;}
			set{ isValid = value;}
		}
		#endregion

	}
}
