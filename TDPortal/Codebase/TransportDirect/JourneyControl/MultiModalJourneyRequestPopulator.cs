//***********************************************
// NAME         : MultiModalJourneyRequestPopulator.cs
// AUTHOR       : Richard Philpott
// DATE CREATED : 2006-02-15
// DESCRIPTION  : Responsible for populating CJP JourneyRequests
//				  for all multi-modal and car-only journeys. 
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/MultiModalJourneyRequestPopulator.cs-arc  $
//
//   Rev 1.4   Dec 05 2012 14:16:22   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.3   Sep 01 2011 10:43:20   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.2   Mar 14 2011 15:11:54   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.1   Feb 02 2009 16:32:24   mmodi
//Populate Routing Guide properties for the request
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.0   Nov 08 2007 12:23:52   mturner
//Initial revision.
//
//   Rev 1.2   Feb 28 2006 14:56:46   RPhilpott
//Comments updated post code review.
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.1   Feb 27 2006 12:17:32   RPhilpott
//Assign to IR 0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.0   Feb 27 2006 12:15:56   RPhilpott
//Initial revision.
//

using System;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Responsible for populating CJP JourneyRequests
	///	for all multi-modal and car-only journeys. 
	/// </summary>
	public class MultiModalJourneyRequestPopulator : JourneyRequestPopulator
	{
		/// <summary>
		/// Constructs a new MultiModalJourneyRequestPopulator
		/// </summary>
		/// <param name="request">Related ITDJourneyRequest</param>
		public MultiModalJourneyRequestPopulator(ITDJourneyRequest request)
		{
			TDRequest = request;
		}

		/// <summary>
		/// Creates the CJPRequest objects needed to call the CJP for the current 
		/// ITDJourneyRequest and returns them encapsulated in an array of CJPCall 
		/// objects.
		/// </summary>
		/// <param name="referenceNumber"></param>
		/// <param name="seqNo"></param>
		/// <param name="sessionId"></param>
		/// <param name="referenceTransaction"></param>
		/// <param name="userType"></param>
		/// <param name="language"></param>
		/// <returns>Array of CJPCall objects</returns>
		public override CJPCall[] PopulateRequests(int referenceNumber, 
													int seqNo,	
													string sessionId,
													bool referenceTransaction, 
													int userType, 
													string language)
		{
			
			CJPCall[] cjpCalls = new CJPCall[TDRequest.IsReturnRequired && TDRequest.IsOutwardRequired ? 2 : 1];

			JourneyRequest request = null;

            if (TDRequest.IsOutwardRequired)
            {
                request = PopulateSingleRequest(TDRequest, false,
                                                    referenceNumber, seqNo++,
                                                    sessionId, referenceTransaction,
                                                    userType, language);

                cjpCalls[0] = new CJPCall(request, false, referenceNumber, sessionId);
            }

			if	(TDRequest.IsReturnRequired)
			{
				request = PopulateSingleRequest(TDRequest, true,
													referenceNumber, seqNo++,
													sessionId, referenceTransaction,
													userType, language);

				cjpCalls[TDRequest.IsOutwardRequired? 1 : 0] = new CJPCall(request, true, referenceNumber, sessionId);
			}

			return cjpCalls;
		}


		/// <summary>
		/// Create a single  fully-populated CJP JourneyRequest object
		/// for a single multimodal request for a specified date.
		/// </summary>
		/// <param name="tdRequest"></param>
		/// <param name="mode"></param>
		/// <param name="inputTime"></param>
		/// <param name="returnJourney"></param>
		/// <param name="referenceNumber"></param>
		/// <param name="seqNo"></param>
		/// <param name="sessionId"></param>
		/// <param name="referenceTransaction"></param>
		/// <param name="userType"></param>
		/// <param name="language"></param>
		/// <returns>Populated CJP JourneyRequest</returns>
		private JourneyRequest PopulateSingleRequest(ITDJourneyRequest tdRequest,
			bool returnJourney,
			int referenceNumber, 
			int seqNo,	
			string sessionId,
			bool referenceTransaction, 
			int userType, 
			string language)
		{
			JourneyRequest cjpRequest = InitialiseNewRequest(referenceNumber, seqNo,
																sessionId, referenceTransaction,
																userType, language);

			bool privateRequired = ModeContains(tdRequest.Modes, ModeType.Car);
			bool publicRequired  = (tdRequest.Modes.Length > 1 || tdRequest.Modes[0] != ModeType.Car);

			cjpRequest.serviceFilter  = CreateServiceFilter (tdRequest.TrainUidFilter,    tdRequest.TrainUidFilterIsInclude);
			cjpRequest.operatorFilter = CreateOperatorFilter(tdRequest.SelectedOperators, tdRequest.UseOnlySpecifiedOperators);

			TDDateTime arriveTime = null;
			TDDateTime departTime = null;

			if  (returnJourney)
			{
				cjpRequest.depart = !tdRequest.ReturnArriveBefore;

				if  (tdRequest.ReturnArriveBefore)
				{
					arriveTime = tdRequest.ReturnDateTime[0];
				}
				else
				{
					departTime = tdRequest.ReturnDateTime[0];
				}

				cjpRequest.origin		= tdRequest.ReturnOriginLocation.ToRequestPlace(departTime, StationType.Undetermined, publicRequired, privateRequired, false);
				cjpRequest.destination	= tdRequest.ReturnDestinationLocation.ToRequestPlace(arriveTime, StationType.Undetermined, publicRequired, privateRequired, false);
			}
			else
			{
				cjpRequest.depart = !tdRequest.OutwardArriveBefore;

				if  (tdRequest.OutwardArriveBefore)
				{
					arriveTime = tdRequest.OutwardDateTime[0];
				}
				else
				{
					departTime = tdRequest.OutwardDateTime[0];
				}

				cjpRequest.origin		= tdRequest.OriginLocation.ToRequestPlace(departTime, StationType.Undetermined, publicRequired, privateRequired, false);
				cjpRequest.destination  = tdRequest.DestinationLocation.ToRequestPlace(arriveTime, StationType.Undetermined, publicRequired, privateRequired, false);
			}

			cjpRequest.parkNRide = false;
			cjpRequest.modeFilter = new Modes();
			cjpRequest.modeFilter.include = true;

			if  (ModeContains(tdRequest.Modes, ModeType.Bus) && !ModeContains(tdRequest.Modes, ModeType.Drt))
			{
				string IsDrtRequired = (string)Properties.Current[JourneyControlConstants.DrtIsRequired];
                    
				if (string.Compare(IsDrtRequired, "Y" , true, CultureInfo.InvariantCulture) == 0 || IsDrtRequired.Length == 0)
				{
					tdRequest.Modes = AddToModeTypeArray(tdRequest.Modes, ModeType.Drt);
				}
			}

			cjpRequest.modeFilter.modes = CreateModeArray(tdRequest.Modes);

			if  (privateRequired)
			{
				cjpRequest.privateParameters = SetPrivateParameters(tdRequest);
			}

			if  (publicRequired)
			{
				cjpRequest.publicParameters = SetPublicParameters(tdRequest, privateRequired);
			}

			return cjpRequest;
		}

    	/// <summary>
		/// Fill the PublicParameters fields for a single multimodal request.
		/// </summary>
		/// <param name="tdRequest"></param>
		/// <returns>Populated CJP PublicParameters object</returns>
		private PublicParameters SetPublicParameters(ITDJourneyRequest tdRequest, bool privateRequired)
		{
			PublicParameters parameters = new PublicParameters();
			
			parameters.algorithm		 = tdRequest.PublicAlgorithm;
			parameters.trunkPlan		 = false;
			parameters.interchangeSpeed  = tdRequest.InterchangeSpeed;
			parameters.walkSpeed		 = tdRequest.WalkingSpeed;
			parameters.intermediateStops = IntermediateStopsType.All;
			parameters.rangeType		 = RangeType.Sequence;

			int publicJourneysRequiredCount = (tdRequest.Sequence == 0) ?
				Int32.Parse(Properties.Current[JourneyControlConstants.NumberOfPublicJourneys])
				: tdRequest.Sequence;
			
			parameters.sequence = (privateRequired ? publicJourneysRequiredCount - 1 : publicJourneysRequiredCount);

            if (tdRequest.MaxWalkingDistance >= 0)
            {
                parameters.maxWalkDistance = tdRequest.MaxWalkingDistance;
            }
            else
            {
                // distance in metres, times in minutes, speeds in metres/min ...
                parameters.maxWalkDistance = tdRequest.WalkingSpeed * tdRequest.MaxWalkingTime;
            }

			if  (tdRequest.PublicViaLocations != null &&  tdRequest.PublicViaLocations.Length > 0)
			{
				ArrayList validViaLocations = new ArrayList();

				for (int i = 0; i < tdRequest.PublicViaLocations.Length; i++)
				{   
					if (tdRequest.PublicViaLocations[i].Status == TDLocationStatus.Valid)
					{
						validViaLocations.Add(tdRequest.PublicViaLocations[i].ToRequestPlace(null, StationType.Undetermined, true, false, true));
					}
				}

				if	(validViaLocations != null && validViaLocations.Count > 0)
				{
					parameters.vias = (RequestPlace[])validViaLocations.ToArray(typeof(RequestPlace));
				}
				else
				{
					parameters.vias = new RequestPlace[0];
				}
			}
			else
			{
				parameters.vias = new RequestPlace[0];
			}

			if  (tdRequest.PublicSoftViaLocations != null &&  tdRequest.PublicSoftViaLocations.Length > 0)
			{
				ArrayList validViaLocations = new ArrayList();

				for (int i = 0; i < tdRequest.PublicSoftViaLocations.Length; i++)
				{   
					if (tdRequest.PublicSoftViaLocations[i].Status == TDLocationStatus.Valid)
					{
						validViaLocations.Add(tdRequest.PublicSoftViaLocations[i].ToRequestPlace(null, StationType.Undetermined, true, false, true));
					}
				}

				if	(validViaLocations != null && validViaLocations.Count > 0)
				{
					parameters.softVias = (RequestPlace[])validViaLocations.ToArray(typeof(RequestPlace));
				}
				else
				{
					parameters.softVias = new RequestPlace[0];
				}
			}
			else
			{
				parameters.softVias = new RequestPlace[0];
			}

			if  (tdRequest.PublicNotViaLocations != null &&  tdRequest.PublicNotViaLocations.Length > 0)
			{
				ArrayList validViaLocations = new ArrayList();
                
				for	(int i = 0; i < tdRequest.PublicNotViaLocations.Length; i++)
				{
					if	(tdRequest.PublicNotViaLocations[i].Status == TDLocationStatus.Valid)
					{
						validViaLocations.Add(tdRequest.PublicNotViaLocations[i].ToRequestPlace(null, StationType.Undetermined, true, false, true));                  
					}
				}

				if	(validViaLocations != null && validViaLocations.Count > 0)
				{
					parameters.notVias = (RequestPlace[])validViaLocations.ToArray(typeof(RequestPlace));
                
				}
				else
				{
					parameters.notVias = new RequestPlace[0];
				}
			}
			else
			{
				parameters.notVias = new RequestPlace[0];
			}

			parameters.extraSequence = 0;
			parameters.extraInterval = DateTime.MinValue;

            // set up Routing guide specific values
            parameters.routingGuideInfluenced = tdRequest.RoutingGuideInfluenced;
            parameters.rejectNonRGCompliantJourneys = tdRequest.RoutingGuideCompliantJourneysOnly;
            parameters.routeCodes = tdRequest.RouteCodes;

            // default filtering to permissive
            parameters.filtering = FilterOptionEnumeration.permissive;

            // set up Accessible journey planning values
            if ((tdRequest.AccessiblePreferences != null) && (tdRequest.AccessiblePreferences.Accessible))
            {
                // set accessible options
                parameters.accessibilityOptions = GetAccessibilityOptions(tdRequest);
                parameters.filtering = FilterOptionEnumeration.strict;

                // set Olympic request flag which  will then ensure the CJP 
                // - uses the single traveline region for accessible journeys
                // - not use the TTBO/Coach planner for air/rail/coach only journeys
                // - not validate the results 
                // - add TTBO service information where available
                parameters.olympicRequest = tdRequest.AccessibleRequest;

                // set dont force coach rule, this overrides the force coach CJP config,
                // i.e. if true, then even if force coach is on in CJP settings do not perform force coach.
                // This is only set for accessible journeys where locations are in london and not coach stops
                parameters.dontForceCoach = tdRequest.DontForceCoach;

                // set awkward overnight, this would remove journeys
                // that breached overnight journey rules, in particular for journeys that arrive for
                // early morning having long waits in the middle of the night.
                // This is not required for TDP accessible journeys.
                parameters.removeAwkwardOvernight = tdRequest.RemoveAwkwardOvernight;
            }

			return parameters;
		}

        /// <summary>
        /// Gets the CJP AccessibilityOptions for the request
        /// </summary>
        /// <param name="sjpJourneyRequest"></param>
        /// <returns></returns>
        private AccessibilityOptions GetAccessibilityOptions(ITDJourneyRequest tdRequest)
        {
            TDAccessiblePreferences accessiblePreferences = tdRequest.AccessiblePreferences;

            // Accessible journey required
            if ((accessiblePreferences != null) && (accessiblePreferences.Accessible))
            {
                AccessibilityOptions accessibilityOptions = new AccessibilityOptions();

                if (accessiblePreferences.DoNotUseUnderground)
                {
                    // Remove Underground from modes
                    // This will have already been done by the journey params convertor when setting the modes for request
                }
                if (accessiblePreferences.RequireStepFreeAccess)
                {
                    accessibilityOptions.wheelchairUse = AccessibilityRequirement.Essential;
                }
                if (accessiblePreferences.RequireSpecialAssistance)
                {
                    accessibilityOptions.assistanceService = AccessibilityRequirement.Essential;
                }

                return accessibilityOptions;
            }

            // Accessible journey options not required
            return null;
        }
	}
}
