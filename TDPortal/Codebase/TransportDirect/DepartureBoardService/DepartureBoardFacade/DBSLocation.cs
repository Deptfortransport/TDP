// *********************************************** 
// NAME                 : DBSLocation.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 04/01/2005
// DESCRIPTION  : object holding Location information
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardFacade/DBSLocation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:24   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:21:36   passuied
//Initial revision.
//
//   Rev 1.3   Jan 18 2005 17:36:04   passuied
//changed after update of CjpInterface
//
//   Rev 1.2   Jan 14 2005 10:19:20   passuied
//added Valid, CodeType attributes
//
//   Rev 1.1   Jan 10 2005 15:25:24   passuied
//changes in initialisation
//
//   Rev 1.0   Jan 05 2005 09:59:42   passuied
//Initial revision.

using System;

using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade
{
	/// <summary>
	/// object holding Location information
	/// </summary>
	public class DBSLocation
	{
		private string[] sNaptanIds;
		private string sLocality;
		private string sCode;
		private TDCodeType ctType;
		private bool isValid;

		public DBSLocation()
		{
			sLocality = string.Empty;
			sCode = string.Empty;
			sNaptanIds = new string[0];
			ctType = TDCodeType.CRS;
			isValid = false;
		}

		public string[] NaptanIds
		{
			get{ return sNaptanIds;}
			set{ sNaptanIds = value;}
		}

		public string Locality
		{
			get{ return sLocality;}
			set{ sLocality = value;}
		}

		public string Code
		{
			get{ return sCode;}
			set{ sCode = value;}
		}
	
		public TDCodeType Type
		{
			get{ return ctType;}
			set{ ctType = value;}
		}

		public bool Valid
		{
			get{ return isValid;}
			set{ isValid = value;}
		}

	}
}
