// ****************************************************************
// NAME         : TDJourneyParametersFlightConverter.cs
// AUTHOR       : Andrew Sinclair
// DATE CREATED : 2005-06-07
// DESCRIPTION  : 
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/TDJourneyParametersFlightConverter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:46   mturner
//Initial revision.
//
//   Rev 1.1   Jun 21 2005 14:58:56   asinclair
//Removed bool IsTrunkRequest
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.0   Jun 15 2005 14:20:46   asinclair
//Initial revision.
//
//   Rev 1.0   Jun 15 2005 14:16:14   asinclair
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for TDJourneyParametersFlightConverter.
	/// </summary>
	[Serializable]
	public class TDJourneyParametersFlightConverter : ITDJourneyParameterConverter
	{
		

		public ITDJourneyRequest Convert(TDJourneyParameters parameters, TDDateTime outwardDateTime, TDDateTime returnDateTime)
		{
			TDJourneyParametersFlight jp = (TDJourneyParametersFlight)parameters; 
			
			ITDJourneyRequest tdJourneyRequest = new TDJourneyRequest();

			tdJourneyRequest.OriginLocation = jp.OriginLocation;
			tdJourneyRequest.DestinationLocation = jp.DestinationLocation;
			tdJourneyRequest.IsReturnRequired = (jp.IsReturnRequired) && (returnDateTime != null);
 
			tdJourneyRequest.OutwardDateTime = new TDDateTime[1];
			tdJourneyRequest.OutwardDateTime[0] = outwardDateTime;
			tdJourneyRequest.ReturnDateTime = new TDDateTime[1];
			tdJourneyRequest.ReturnDateTime[0] = returnDateTime;

			tdJourneyRequest.OutwardArriveBefore = jp.OutwardArriveBefore;
			tdJourneyRequest.ReturnArriveBefore = jp.ReturnArriveBefore;
			
			tdJourneyRequest.PublicViaLocations = new TDLocation[1];
			tdJourneyRequest.PublicViaLocations[0] = jp.ViaLocation;
			
			tdJourneyRequest.AlternateLocations = null;

			tdJourneyRequest.OutwardAnyTime = jp.OutwardAnyTime;	
			tdJourneyRequest.ReturnAnyTime  = jp.ReturnAnyTime;

			tdJourneyRequest.UseOnlySpecifiedOperators = jp.OnlyUseSpecifiedOperators;
			tdJourneyRequest.SelectedOperators = jp.SelectedOperators;
			tdJourneyRequest.DirectFlightsOnly = jp.DirectFlightsOnly;
			tdJourneyRequest.PublicAlgorithm = (jp.DirectFlightsOnly ? PublicAlgorithmType.NoChanges : PublicAlgorithmType.Default); 

			// note use of TDDateTime for intervals because TimeSpan not serializable ...
			
			DateTime tempDateTime = DateTime.MinValue + (new TimeSpan(0, jp.ExtraCheckInTime, 0));
			tdJourneyRequest.ExtraCheckinTime = new TDDateTime(tempDateTime);

			tempDateTime = DateTime.MinValue + (new TimeSpan(jp.OutwardStopover, 0, 0));
			tdJourneyRequest.ViaLocationOutwardStopoverTime = new TDDateTime(tempDateTime);

			tempDateTime = DateTime.MinValue + (new TimeSpan(jp.ReturnStopover, 0, 0));
			tdJourneyRequest.ViaLocationReturnStopoverTime = new TDDateTime(tempDateTime);

			// not user-settable for flights, but passed here so defaults from DataServices get used ...
			tdJourneyRequest.InterchangeSpeed = jp.InterchangeSpeed;
			tdJourneyRequest.WalkingSpeed = jp.WalkingSpeed;
			tdJourneyRequest.MaxWalkingTime = jp.MaxWalkingTime;

			tdJourneyRequest.IsTrunkRequest = true;   
			tdJourneyRequest.Modes = new ModeType[] { ModeType.Air };

			return ( tdJourneyRequest);
		}

	}
}
