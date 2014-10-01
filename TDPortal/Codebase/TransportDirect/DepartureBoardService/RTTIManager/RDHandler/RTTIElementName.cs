// *********************************************** 
// NAME                 : RTTIElementName.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/01/2005 
// DESCRIPTION  		: This class contains list of RTTI element name 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RTTIElementName.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:38   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:06   passuied
//Initial revision.
//
//   Rev 1.3   Feb 02 2005 15:33:20   schand
//applied FxCop rules
//
//   Rev 1.2   Jan 21 2005 14:22:36   schand
//Code clean-up and comments has been added

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// This class contains list of RTTI element name 
	/// </summary>
	public class RTTIElementName
	{	
		// trip and station element 
		public const string StationResponse = "StationResp" ;
		public const string TripResponse = "TripResp" ;		
		public const string ResponseFrom = "From" ;
		public const string ResponseTo = "To" ;
		public const string StationResult = "TAS" ;
		public const string TripResult = "TAS";
		public const string RTTIError = "Error" ;

		// train element 
		public const string TrainResponse = "TrainResp" ;
		public const string TrainOrigin = "OR" ;
		public const string TrainInterStop = "IP" ;
		public const string TrainDestination = "DT" ;
		public const string TrainAssociation = "AS" ;

	}

}
