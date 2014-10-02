using System;
using System.IO;
using System.Collections;

namespace TransportDirect.UserPortal.SessionManager
{
	public enum ValidationErrorID
	{
		OutwardDateTimeInvalid,
		ReturnDateTimeInvalid,
		OutwardDateTimeNotLaterThanNow,
		ReturnDateTimeNotLaterThanNow,
		OutwardAndReturnDateTimeInvalid,
		ReturnDateTimeNotLaterThanOutwardDateTime,
		OutwardAndReturnDateTimeNotLaterThanNow,
		NoModesSelected,
		ReturnMonthMissing,
		ReturnDateMissing,
		ReturnTimeMissing,

		// Origin location error messages
		AmbiguousOriginLocation,
		OriginLocationInvalid,
		OriginLocationHasNoNaptan,
		OriginLocationInvalidAndOtherErrors,
        OriginLocationHasNoPoint,
        OriginLocationNotAccessible,

		// Destination location error messages
		AmbiguousDestinationLocation,
		DestinationLocationInvalid,
		DestinationLocationHasNoNaptan,
		DestinationLocationInvalidAndOtherErrors,
        DestinationLocationHasNoPoint,
        DestinationLocationNotAccessible,

		// Public Via location error messages
		AmbiguousPublicViaLocation,
		PublicViaLocationInvalid,
		PublicViaLocationInvalidAndOtherErrors,
        PublicViaLocationNotAccessible,

		// Private Via location error messages
		AmbiguousPrivateViaLocation,
		PrivateViaLocationInvalid,
		PrivateViaLocationInvalidAndOtherErrors,

        // Cycle Via location error messages
        AmbiguousCycleViaLocation,
        CycleViaLocationInvalid,
        CycleViaLocationInvalidAndOtherErrors,
        CycleViaLocationHasNoPoint,

		// Visit location 1 error messages
		AmbiguousVisitLocation1,
		VisitLocation1Invalid,
		VisitLocation1InvalidAndOtherErrors,

		// Visit location 2 error messages
		AmbiguousVisitLocation2,
		VisitLocation2Invalid,
		VisitLocation2InvalidAndOtherErrors,

		// Common location error messages
		OriginAndDestinationOverlap,
		OriginAndViaOverlap,
		DestinationAndViaOverlap,
        AmbiguousAlternateLocation,
		VisitLocationsOverlap,

		// Find a flight specific
		NoValidRoutes,
		InvalidOperatorSelection,

        // Extend journey specific
        ExtendToStartOutwardInPast,
        ExtendToStartReturnInPast,
        ExtendFromEndOutwardInPast,
        ExtendFromEndReturnInPast,

		//Car Costing
		InvalidFuelConsumption,
		InvalidFuelCost,
        RouteAffectedByClosuresErrors,

		//VisitPlanner Itinerary Manager
		JourneyTimesOverlap,

        // Cycle
        CyclePlannerUnavailable,
        InvalidCycleSpeed,
        DistanceBetweenLocationsTooGreat,
        DistanceBetweenLocationsAndViaTooGreat,
        LocationInInvalidCycleArea,
        LocationPointsNotInSameCycleArea,

        // Environmental benefits
        EnvironmentalBenefitsCalculatorUnavailable,

        //International Planner
        InternationalPlannerUnavailable,
        InternationalPlannerJourneyNotPermitted,
        InternationalPlannerInvalidMode
	}


	/// <summary>
	/// Summary description for ValidationError.
	/// </summary>
	/// 
	[Serializable()]
	public class ValidationError
	{
		private string msgResourceID;
		private ValidationErrorID[] errorIDs;

		private Hashtable messageIDs;

		public Hashtable MessageIDs 
		{
			get { return messageIDs; }
			set { messageIDs = value; }
		}


		public string MsgResourceID
		{
			get { return msgResourceID; }
			set { msgResourceID = value;}
		}
		public ValidationErrorID[] ErrorIDs
		{
			get { return errorIDs; }
			set { errorIDs = value;}
		}

		public ValidationError()
		{
		}

		public bool Contains (ValidationErrorID errorId)
		{
			//only try to do this if there are some errors!
			if (errorIDs != null)
			{
				foreach ( ValidationErrorID id in errorIDs)
				{
					if (errorId == id)
					{
						return true;
					}
				}
				return false;
			}
			else
			{
				return false;
			}
		}

        /// <summary>
        /// Removes the specified error
        /// </summary>
        /// <param name="errorId"></param>
        public void Remove(ValidationErrorID error)
        {
            if (errorIDs != null)
            {
                // Remove the ValidationErrorID
                ArrayList amendedErrorIDs = new ArrayList();
                foreach (ValidationErrorID errorID in errorIDs)
                {
                    if (errorID != error)
                        amendedErrorIDs.Add(errorID);
                }

                errorIDs = (ValidationErrorID[])amendedErrorIDs.ToArray(typeof(ValidationErrorID));

                messageIDs.Remove(error);
            }
        }
		
		public void Initialise()
		{
			errorIDs = null;
			msgResourceID = string.Empty;
			messageIDs.Clear();

		}
	}
}
