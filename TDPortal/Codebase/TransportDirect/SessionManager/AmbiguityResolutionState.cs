// *********************************************** 
// NAME                 : AmbiguityResolutionState.ascx.cs 
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 25/03/2004
// DESCRIPTION          : Provides the means to temporarily store those journey parameters
// that can be modified during ambiguity resolution and a means to restore them should 
// the changes need to be backed out.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/AmbiguityResolutionState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:16   mturner
//Initial revision.
//
//   Rev 1.7   Dec 05 2005 17:13:58   jgeorge
//Made IsOpenReturn property of TDJourneyParametersMulti readonly, and derived from ReturnMonthYear.
//Resolution for 3313: Door-to-door: selecting open return on input page does not display return fares on results page
//
//   Rev 1.6   Nov 10 2005 17:53:26   asinclair
//Updated to fix VP issues
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Nov 01 2005 15:12:14   build
//Automatically merged from branch for stream2638
//
//   Rev 1.4.1.1   Oct 27 2005 10:34:00   asinclair
//Updated for VisitPlace1 and VisitPlace2
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4.1.0   Oct 20 2005 09:52:36   asinclair
//Updated for VisitPlanner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Apr 20 2005 12:20:10   Ralavi
//Ensuring that original values are returned for car costing when selecting back button
//
//   Rev 1.3   Jun 23 2004 16:38:00   COwczarek
//Return month year in journey parameters was incorrectly being set to the outward month year.
//Resolution for 1044: Add date validation to extend journey functionality
//
//   Rev 1.2   May 26 2004 08:56:26   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.1   Apr 15 2004 12:04:28   COwczarek
//Save / reinstate journey parameters that indicate journey is return/open return journey.  Ambiguity resolution may create a return date that was not specified by the user on the input page which may need to be backed out
//Resolution for 746: Date validation for return journeys behaves strangely
//
//   Rev 1.0   Mar 26 2004 09:57:12   COwczarek
//Initial revision.
//Resolution for 662: Back Button from the Ambiguity Page does not unconfirm a confirmed location

using System;
using TransportDirect.UserPortal.LocationService;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
    /// Provides the means to temporarily store those journey parameters
    /// that can be modified during ambiguity resolution and a means to restore them should 
    /// the changes need to be backed out.
	/// </summary>
	/// 
    [Serializable()]
    public class AmbiguityResolutionState
	{

        // Journey parameters held by a TDJourneyParameters object that can be
        // changed during ambiguity resolution

        private LocationSearch originLocationSearch;
        private TDLocation originLocation;
        private LocationSelectControlType originType;

        private LocationSearch destinationLocationSearch;
        private TDLocation destinationLocation;
        private LocationSelectControlType destinationType;

        private LocationSearch privateViaLocationSearch;
        private TDLocation privateViaLocation;
        private LocationSelectControlType privateViaType;

        private LocationSearch publicViaLocationSearch;
        private TDLocation publicViaLocation;
        private LocationSelectControlType publicViaType;

		private LocationSearch visitPlaceSearch1;
		private TDLocation visitPlaceLocation1;
		private LocationSelectControlType visitPlace1Type;
		
		private LocationSearch visitPlaceSearch2;
		private TDLocation visitPlaceLocation2;
		private LocationSelectControlType visitPlace2Type;

        private string outwardHour;
        private string outwardDayOfMonth;
        private string outwardMinute;
        private string outwardMonthYear;

        private string returnHour;
        private string returnDayOfMonth;
        private string returnMinute;
        private string returnMonthYear;

        private int walkingSpeed;
        private int maxWalkingTime;

        private string fuelConsumptionEntered;
        private string fuelCostEntered;
        private bool fuelConsumptionOption;
        private bool fuelCostOption;
        private int fuelConsumptionUnit;
        private TDRoad[] avoidRoadsList; 
        private TDRoad[] useRoadsList;

        private bool isReturnRequired;

        /// <summary>
        /// Constructor
        /// </summary>
		public AmbiguityResolutionState()
		{
        }

        /// <summary>
        /// Sets the journey parameters currently associated with the session to be those
        /// stored by this object, saved by previously calling SaveJourneyParameters()
        /// </summary>
        public void ReinstateJourneyParameters() 
        {
            TDJourneyParametersMulti journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
			if (journeyParameters == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("AmbiguityResolutionState.ReinstateJourneyParameters requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}

            journeyParameters.Origin = originLocationSearch;
            journeyParameters.OriginLocation = originLocation;
            journeyParameters.OriginType = originType;

            journeyParameters.Destination = destinationLocationSearch;
            journeyParameters.DestinationLocation = destinationLocation;
            journeyParameters.DestinationType = destinationType;

            journeyParameters.PrivateVia = privateViaLocationSearch;
            journeyParameters.PrivateViaLocation = privateViaLocation;
            journeyParameters.PrivateViaType = privateViaType;

            journeyParameters.PublicVia = publicViaLocationSearch;
            journeyParameters.PublicViaLocation = publicViaLocation;
            journeyParameters.PublicViaType = publicViaType;

            journeyParameters.OutwardHour = outwardHour;
            journeyParameters.OutwardDayOfMonth = outwardDayOfMonth;
            journeyParameters.OutwardMinute = outwardMinute;
            journeyParameters.OutwardMonthYear = outwardMonthYear;

            journeyParameters.ReturnHour = returnHour;
            journeyParameters.ReturnDayOfMonth = returnDayOfMonth;
            journeyParameters.ReturnMinute = returnMinute;
            journeyParameters.ReturnMonthYear = returnMonthYear;

            journeyParameters.WalkingSpeed = walkingSpeed;
            journeyParameters.MaxWalkingTime = maxWalkingTime;

            journeyParameters.IsReturnRequired = isReturnRequired;

            journeyParameters.FuelConsumptionEntered = fuelConsumptionEntered;
            journeyParameters.FuelCostEntered = fuelCostEntered;
            journeyParameters.FuelConsumptionOption = fuelConsumptionOption;
            journeyParameters.FuelCostOption = fuelCostOption;
            journeyParameters.FuelConsumptionUnit = fuelConsumptionUnit;
            journeyParameters.AvoidRoadsList = avoidRoadsList;
            journeyParameters.UseRoadsList = useRoadsList;

        }

        /// <summary>
        /// Stores (references) of the journey parameters currently associated with the
        /// session so that they may be reinstated at a later time if changes need to be 
        /// backed out
        /// </summary>
        public void SaveJourneyParameters() 
        {
			TDJourneyParametersMulti journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
			if (journeyParameters == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("JourneyPlanRunner requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}

            originLocationSearch = journeyParameters.Origin;
            originLocation = journeyParameters.OriginLocation;
            originType = journeyParameters.OriginType;

            destinationLocationSearch = journeyParameters.Destination;
            destinationLocation = journeyParameters.DestinationLocation;
            destinationType = journeyParameters.DestinationType;

            privateViaLocationSearch = journeyParameters.PrivateVia;
            privateViaLocation = journeyParameters.PrivateViaLocation;
            privateViaType = journeyParameters.PrivateViaType;

            publicViaLocationSearch = journeyParameters.PublicVia;
            publicViaLocation = journeyParameters.PublicViaLocation;
            publicViaType = journeyParameters.PublicViaType;

            outwardHour = journeyParameters.OutwardHour;
            outwardDayOfMonth = journeyParameters.OutwardDayOfMonth;
            outwardMinute = journeyParameters.OutwardMinute;
            outwardMonthYear = journeyParameters.OutwardMonthYear;

            returnHour = journeyParameters.ReturnHour;
            returnDayOfMonth = journeyParameters.ReturnDayOfMonth;
            returnMinute = journeyParameters.ReturnMinute;
            returnMonthYear = journeyParameters.ReturnMonthYear;

            walkingSpeed = journeyParameters.WalkingSpeed;
            maxWalkingTime = journeyParameters.MaxWalkingTime;

            // Save these parameters since validating a date may create a return date
            // that was not specified by the user on the input page
            isReturnRequired = journeyParameters.IsReturnRequired;

            fuelConsumptionEntered = journeyParameters.FuelConsumptionEntered;
            fuelCostEntered = journeyParameters.FuelCostEntered;
            fuelConsumptionOption = journeyParameters.FuelConsumptionOption;
            fuelCostOption = journeyParameters.FuelCostOption;
            fuelConsumptionUnit = journeyParameters.FuelConsumptionUnit;
            avoidRoadsList = journeyParameters.AvoidRoadsList;
            useRoadsList = journeyParameters.UseRoadsList;

        }

		/// <summary>
		/// Stores (references) of the journey parameters for VisitPlanner currently associated with the
		/// session so that they may be reinstated at a later time if changes need to be 
		/// backed out
		/// </summary>
		public void SaveVisitJourneyParameters() 
		{
			TDJourneyParametersVisitPlan journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;
			if (journeyParameters == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("JourneyPlanRunner requires a TDJourneyParametersVisitPlan object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}

			originLocationSearch = journeyParameters.GetLocationSearch(0);
			originLocation = journeyParameters.GetLocation(0);
			originType = journeyParameters.GetLocationType(0);

			visitPlaceSearch1 = journeyParameters.GetLocationSearch(1);
			visitPlaceLocation1 = journeyParameters.GetLocation(1);
			visitPlace1Type = journeyParameters.GetLocationType(1);

			visitPlaceSearch2 = journeyParameters.GetLocationSearch(2);
			visitPlaceLocation2 = journeyParameters.GetLocation(2);
			visitPlace2Type = journeyParameters.GetLocationType(2);

			destinationLocationSearch = journeyParameters.Destination;
			destinationLocation = journeyParameters.DestinationLocation;
			destinationType = journeyParameters.DestinationType;

			outwardHour = journeyParameters.OutwardHour;
			outwardDayOfMonth = journeyParameters.OutwardDayOfMonth;
			outwardMinute = journeyParameters.OutwardMinute;
			outwardMonthYear = journeyParameters.OutwardMonthYear;

			walkingSpeed = journeyParameters.WalkingSpeed;
			maxWalkingTime = journeyParameters.MaxWalkingTime;

		}

		/// <summary>
		/// Used by VisitPlanner to sets the journey parameters currently associated with the 
		/// session to be those stored by this object, saved by previously calling 
		/// SaveVisitJourneyParameters()
		/// </summary>
		public void ReinstateVisitJourneyParameters() 
		{
			TDJourneyParametersVisitPlan journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;
			if (journeyParameters == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("AmbiguityResolutionState.ReinstateJourneyParameters requires a TDJourneyParametersVisitPlan object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);
			}

//			journeyParameters.Origin = originLocationSearch;
//			journeyParameters.OriginLocation = originLocation;
//			journeyParameters.OriginType = originType;
			
			journeyParameters.SetLocationSearch(0, originLocationSearch);
			journeyParameters.SetLocation (0, originLocation);
			journeyParameters.SetLocationType(0 , originType);


			journeyParameters.SetLocationSearch(1, visitPlaceSearch1);
			journeyParameters.SetLocation (1, visitPlaceLocation1);
			journeyParameters.SetLocationType(1, visitPlace1Type);

			journeyParameters.SetLocationSearch(2, visitPlaceSearch2);
			journeyParameters.SetLocation (2, visitPlaceLocation2);
			journeyParameters.SetLocationType(2, visitPlace2Type);


			journeyParameters.Destination = destinationLocationSearch;
			journeyParameters.DestinationLocation = destinationLocation;
			journeyParameters.DestinationType = destinationType;

			journeyParameters.OutwardHour = outwardHour;
			journeyParameters.OutwardDayOfMonth = outwardDayOfMonth;
			journeyParameters.OutwardMinute = outwardMinute;
			journeyParameters.OutwardMonthYear = outwardMonthYear;

			journeyParameters.WalkingSpeed = walkingSpeed;
			journeyParameters.MaxWalkingTime = maxWalkingTime;


		}

    }
}
