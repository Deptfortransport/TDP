// *********************************************** 
// NAME                 : FavouriteJourneyHelper.cs
// AUTHOR               : Peter Norell
// DATE CREATED         : 20/01/2004 
// DESCRIPTION  : 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Code/FavouriteJourneyHelper.cs-arc  $
//
//   Rev 1.6   Dec 11 2012 14:00:34   mmodi
//Save favourite journey accessible options 
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.5   Sep 27 2012 14:45:04   mmodi
//Display favourite journey in new location suggest control
//Resolution for 5852: Gaz - Favourite journeys are not displayed
//
//   Rev 1.4   Mar 14 2011 15:12:02   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.3   Oct 13 2008 16:41:32   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.1   Oct 10 2008 15:51:12   mmodi
//Updated to have avoid time based check
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Sep 02 2008 11:41:02   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:18:48   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:10:46   mturner
//Initial revision.
//
//   Rev 1.7   Feb 23 2006 19:15:58   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.0   Jan 10 2006 15:53:30   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Sep 23 2005 14:34:38   pscott
//Only Retrieve Descriptions to ensure short journeys are not disallowed.
//
//   Rev 1.5   Apr 08 2005 17:08:38   PNorell
//Fix for favourite journes backwards compatibility.
//Resolution for 1991: Del 7  - unable to plan journey if logged in as a user registered pre-del 7
//
//   Rev 1.4   Mar 01 2005 11:41:06   PNorell
//Removed comments and old obsolete code.
//Resolution for 1955: DEV Code Review: Car Costing Journey Favourite
//
//   Rev 1.3   Feb 24 2005 11:37:28   PNorell
//Updated for favourite details.
//
//   Rev 1.2   May 26 2004 09:11:56   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.1   Mar 19 2004 15:57:34   COwczarek
//Fix incorrect journey parameter being set after loading journey. Remove redundant code.
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.0   Jan 21 2004 10:42:42   PNorell
//Initial Revision
using System;using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web
{
	/// <summary>
	/// Summary description for FavouriteJourneyHelper.
	/// </summary>
	public class FavouriteJourneyHelper
	{
		public static void SaveFavouriteJourney(FavouriteJourney fj)
		{
			if (fj == null)
			{
				TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.JourneyPlannerInputDefault;
				return;
			}
			TDJourneyParametersMulti journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
			if (journeyParameters == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("FavouriteJourneyHelper.SaveFavouriteJourney requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}
			
			fj.OriginLocation = journeyParameters.OriginLocation;
			fj.OriginType = journeyParameters.Origin.SearchType;
			fj.DestinationLocation = journeyParameters.DestinationLocation;
			fj.DestinationType = journeyParameters.Destination.SearchType;

			fj.PrivateViaLocation = journeyParameters.PrivateViaLocation;
			fj.PrivateViaType = journeyParameters.PrivateVia.SearchType;
			fj.PublicViaLocation = journeyParameters.PublicViaLocation;
			fj.PublicViaType = journeyParameters.PublicVia.SearchType;
            fj.CycleViaLocation = journeyParameters.CycleViaLocation;
            fj.CycleViaType = journeyParameters.CycleVia.SearchType;

			fj.PublicRequired = journeyParameters.PublicRequired;
			fj.PrivateRequired = journeyParameters.PrivateRequired;
            fj.CycleRequired = journeyParameters.CycleRequired;

			fj.ModeTypes = journeyParameters.PublicModes;

			fj.AvoidTolls = journeyParameters.AvoidTolls;
			fj.AvoidFerries = journeyParameters.AvoidFerries;
            fj.AvoidMotorWays = journeyParameters.AvoidMotorWays;
            fj.BanLimitedAccess = journeyParameters.BanUnknownLimitedAccess;

			fj.AvoidRoad = journeyParameters.AvoidRoadsList;
			fj.UseRoad = journeyParameters.UseRoadsList;

            fj.RequireSpecialAssistance = journeyParameters.RequireSpecialAssistance;
            fj.RequireStepFreeAccess = journeyParameters.RequireStepFreeAccess;
            fj.RequireFewerInterchanges = journeyParameters.RequireFewerInterchanges;

            fj.CycleJourneyType = journeyParameters.CycleJourneyType;
            fj.CycleSpeedText = journeyParameters.CycleSpeedText;
            fj.CycleSpeedUnit = journeyParameters.CycleSpeedUnit;
            fj.CycleAvoidSteepClimbs = journeyParameters.CycleAvoidSteepClimbs;
            fj.CycleAvoidUnlitRoads = journeyParameters.CycleAvoidUnlitRoads;
            fj.CycleAvoidWalkingBike = journeyParameters.CycleAvoidWalkingYourBike;
            fj.CycleAvoidTimeBased = journeyParameters.CycleAvoidTimeBased;
            
			fj.Update();
		}

		public static void LoadFavouriteJourney(FavouriteJourney fj)
		{
			TDJourneyParametersMulti journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
			if (journeyParameters == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("FavouriteJourneyHelper.LoadFavouriteJourney requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}

			// set origin and destination
			
			// First clearAll the searchs and reset the locations
			journeyParameters.Origin.ClearAll();
			journeyParameters.Origin.InputText = fj.OriginLocation.Description; 	
			journeyParameters.Origin.SearchType = fj.OriginType;

            if (fj.OriginLocation != null)
                journeyParameters.OriginLocation = fj.OriginLocation;

			journeyParameters.Destination.ClearAll();
			journeyParameters.Destination.InputText = fj.DestinationLocation.Description;
			journeyParameters.Destination.SearchType = fj.DestinationType;

            if (fj.DestinationLocation != null)
                journeyParameters.DestinationLocation = fj.DestinationLocation;

			// set page details from retrieved profile data
			journeyParameters.PrivateRequired = fj.PrivateRequired;
			journeyParameters.PublicRequired = fj.PublicRequired;
            journeyParameters.CycleRequired = fj.CycleRequired;

			journeyParameters.AvoidTolls = fj.AvoidTolls;
			journeyParameters.AvoidFerries = fj.AvoidFerries;
			journeyParameters.AvoidMotorWays = fj.AvoidMotorWays;
            journeyParameters.BanUnknownLimitedAccess = fj.BanLimitedAccess;

			if( fj.AvoidRoad  != null )
			{
				journeyParameters.AvoidRoadsList = fj.AvoidRoad;
			}
			if( fj.UseRoad != null )
			{
				journeyParameters.UseRoadsList = fj.UseRoad;
			}

			journeyParameters.PrivateVia.ClearAll();
			journeyParameters.PrivateVia.InputText = fj.PrivateViaLocation.Description;
			journeyParameters.PrivateVia.SearchType = fj.PrivateViaType;

            if (fj.PrivateViaLocation != null)
                journeyParameters.PrivateViaLocation = fj.PrivateViaLocation;
	
			journeyParameters.PublicVia.ClearAll();
			journeyParameters.PublicVia.InputText = fj.PublicViaLocation.Description; 
			journeyParameters.PublicVia.SearchType = fj.PublicViaType;

            if (fj.PublicViaLocation != null)
                journeyParameters.PublicViaLocation = fj.PublicViaLocation;

            // A pre Del 10.3 journey may not have a CycleViaLocation
            if (fj.CycleViaLocation != null)
            {
                journeyParameters.CycleVia.ClearAll();
                journeyParameters.CycleVia.InputText = fj.CycleViaLocation.Description;
                journeyParameters.CycleVia.SearchType = fj.CycleViaType;

                journeyParameters.CycleViaLocation = fj.CycleViaLocation;
            }
            else
            {
                // Set to defaults, prevents a previously entered via being retained when loading
                journeyParameters.CycleVia = new LocationSearch();
                journeyParameters.CycleViaLocation = new TDLocation();
                journeyParameters.CycleViaType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
            }

			journeyParameters.PublicModes = fj.ModeTypes;

            journeyParameters.RequireSpecialAssistance = fj.RequireSpecialAssistance;
            journeyParameters.RequireStepFreeAccess = fj.RequireStepFreeAccess;
            journeyParameters.RequireFewerInterchanges = fj.RequireFewerInterchanges;

            journeyParameters.CycleJourneyType = fj.CycleJourneyType;
            journeyParameters.CycleSpeedText = fj.CycleSpeedText;
            journeyParameters.CycleSpeedUnit = fj.CycleSpeedUnit;
            journeyParameters.CycleAvoidSteepClimbs = fj.CycleAvoidSteepClimbs;
            journeyParameters.CycleAvoidUnlitRoads = fj.CycleAvoidUnlitRoads;
            journeyParameters.CycleAvoidWalkingYourBike = fj.CycleAvoidWalkingBike;
            journeyParameters.CycleAvoidTimeBased = fj.CycleAvoidTimeBased;
		}
	}
}
