//***********************************************
// NAME         : JourneyRequestPopulator.cs
// AUTHOR       : Richard Philpott
// DATE CREATED : 2006-02-15
// DESCRIPTION  : Abstract base class for hierarchy of classes
//                responsible for populating JourneyRequests
//				  of various types for the CJP.	
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyRequestPopulator.cs-arc  $
//
//   Rev 1.9   Sep 01 2011 10:43:18   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.8   Mar 14 2011 15:11:52   RPhilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.7   Dec 21 2010 14:05:04   apatel
//Code updated to request services for the day of travel starting from 01:00 on the current day to 01:00 the following day for Find a train, Find a flight and City to City requests
//Resolution for 5651: CCN 593 - Show 10 results or show all
//
//   Rev 1.6   Nov 25 2010 11:24:08   MTurner
//Amended logic so that the C2C Interval can be set to numbers 24 or greater. If this happens the window is set to 23:59:59.
//Resolution for 5643: C2C planner hides early morning services.
//
//   Rev 1.5   Nov 24 2010 10:38:38   apatel
//Resolve the issue with SBP planner when planning for today get services from 22:00 yesterday.  For all other dates the period is 00:00 onwards. Make the SBP planner to return journeys planned 00:00 onwards for all days requested
//Resolution for 5644: SBP planner when planning for today you get services from 22:00 yesterday
//
//   Rev 1.4   Feb 02 2009 16:31:34   mmodi
//Populate Routing Guide properties for the request
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.3   Mar 10 2008 15:17:52   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev Devfactory Jan 20 2008 19:00:00 dgath and stsang
//   CCN 0382b - City to City enhancements. Updated to construct the 
//search timespan to be used for city to city journeys. 
//PopulateSingleTrunkRequest amended to accept an useOverrideTimespan flag. 
//   GetTrunkSearchTimes amended to use start time and interval properties from database where   
//   useOverrideTimespan is true.
//
//   Rev 1.1   Nov 15 2007 12:03:20   mturner
//Changed DateTime logic to not use milliseconds for Anytime journeys.  This is to prevent problems with the CJP caused by changes in .Net 2.0
//
//   Rev 1.0   Nov 08 2007 12:23:50   mturner
//Initial revision.
//
//   Rev 1.5   Sep 07 2007 14:39:40   mturner
//Amended for IR-4481 (Journeys in the past) changes allow TrunkRequests to specify a depart or arrive time earlier than the current time for journeys for today.  'Any Time' Requests still default to current time.
//
//   Rev 1.4   Apr 29 2006 13:43:42   mdambrine
//When we are in adjust we pass for trunkplanning we need to pass a sequence in order to avoid an interval in as search type
//Resolution for 3657: DN068: Extend, Replan & Adjust: Adjusting flights
//Resolution for 3726: DN068 Adjust: No adjusted journey option offered from Find A Flight
//
//   Rev 1.3   Apr 08 2006 15:30:12   RPhilpott
//Only populate times in CJP journey request for trunk journeys where necessary.
//
//   Rev 1.2   Feb 28 2006 14:37:10   RPhilpott
//Comments updated, post code review.
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
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;

using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.CJPInterface;

using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;
using System.Collections.Generic;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Abstract base class for hierarchy of classes responsible 
	/// for populating JourneyRequests of various types for the CJP.	
	/// </summary>
	public abstract class JourneyRequestPopulator
	{
		private ITDJourneyRequest tdRequest;

		private const int HOURS_IN_DAY = 24;

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
		public abstract CJPCall[] PopulateRequests(int referenceNumber, 
														int seqNo,	
														string sessionId,
														bool referenceTransaction, 
														int userType, 
														string language);

		
        /// <summary>
		/// Scan through an array of ModeTypes to determine 
		/// if a single specified ModeType is in the array
		/// </summary>
		/// <param name="modes">An array of ModeTypes</param>
		/// <param name="mode">ModeType to look for</param>
		/// <returns>True if the specified ModeType is found</returns>
		protected bool ModeContains(ModeType[] modes, ModeType mode )
		{
			foreach (ModeType item in modes)
			{
				if  (item == mode)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// Create an array of Modes from the given array of ModeType
		/// Modes are used within the CJPJourneyRequest Type
		/// </summary>
		/// <param name="modes">An array of type ModeType</param>
		/// <returns>An array of type Mode</returns>
		protected Mode[] CreateModeArray(ModeType[] modes)
		{
            if (modes != null)
			{
                List<ModeType> tmpModes = new List<ModeType>();
                tmpModes.AddRange(modes);
                tmpModes.Sort();

			    List<Mode> result = new List<Mode>();

                for (int i = 0; i < tmpModes.Count; i++)
				{
					result.Add(new Mode());
                    result[i].mode = tmpModes[i];
				}
                                
                return result.ToArray();
			}
			return null;
		}

		/// <summary>
		/// Create an array of Modes from the given ArrayList of ModeType objects
		/// Modes are used within the CJPJourneyRequest Type
		/// </summary>
		/// <param name="modes">An ArrayList of objects of type ModeType</param>
		/// <returns>An array of type Mode</returns>
		protected Mode[] CreateModeArray(ArrayList modes)
		{
			Mode[] result = null;

			if (modes != null)
			{
				result = new Mode[modes.Count];

				for  (int i = 0; i < modes.Count; i++)
				{
					result[i] = new Mode();
					result[i].mode = (ModeType)modes[i];
				}
			}
			return result;
		}


		/// <summary>
		/// Add a ModeType to a ModeType array
		/// </summary>
		/// <param name="modes">An array of type ModeType</param>
		/// <param name="mode">ModeType to add</param>
		/// <returns>An array of type ModeType</returns>
		protected ModeType[] AddToModeTypeArray(ModeType[] modes, ModeType mode)
		{
			ModeType[] result = null;

			if (modes != null)
			{
				result = new ModeType[modes.Length + 1];
                
				result[0] = mode;
				modes.CopyTo(result,1);
			}
			return result;
		}
		
		/// <summary>
		/// Converts ModeType enumeration to StationType enumeration
		/// </summary>
		/// <param name="mode">ModeType</param>
		/// <returns>Corresponding StationType</returns>
		protected StationType ModeToStationType(ModeType mode)
		{
			switch (mode)
			{
				case ModeType.Air:
					return StationType.Airport;

				case ModeType.Coach:
					return StationType.Coach;

				case ModeType.Rail:
					return StationType.Rail;

				default:
					return StationType.Undetermined;
			}
		}

		/// <summary>
		/// Formats a sequence number for use as a request-id
		/// </summary>
		/// <param name="seqNo"></param>
		/// <returns>Formatted string</returns>
		protected string FormatSeqNo(int seqNo)
		{
			return seqNo.ToString("-0000");
		}

		/// <summary>
		/// Instantiates a JourneyRequest and populates some common attributes. 
		/// </summary>
		/// <param name="referenceNumber"></param>
		/// <param name="seqNo"></param>
		/// <param name="sessionId"></param>
		/// <param name="referenceTransaction"></param>
		/// <param name="userType"></param>
		/// <param name="language"></param>
		/// <returns>The new JourneyRequest</returns>
		protected JourneyRequest InitialiseNewRequest(int referenceNumber, int seqNo, string sessionId,
														bool referenceTransaction, int userType, 
														string language)
		{
			JourneyRequest cjpRequest = new JourneyRequest();  

			cjpRequest.requestID = SqlHelper.FormatRef(referenceNumber) + FormatSeqNo(seqNo);
			cjpRequest.referenceTransaction = referenceTransaction;
			cjpRequest.sessionID = sessionId;
			cjpRequest.language = language;
			cjpRequest.userType = userType;

			return cjpRequest;
		}

		/// <summary>
		/// Creates and populates a CJP Service Filter
		/// using the UID details from the TD request.
		/// </summary>
		/// <param name="filter"></param>
		/// <returns>A populated ServiceFilter, or null if none is required</returns>
		protected ServiceFilter CreateServiceFilter(string[] uidFilter, bool uidInclude)
		{
			ServiceFilter cjpFilter = null;

			if (uidFilter !=null && uidFilter.Length > 0)
			{
				cjpFilter = new ServiceFilter();
				cjpFilter.include = uidInclude;

				cjpFilter.services = new RequestServicePrivate[uidFilter.Length];

				for (int i = 0; i < uidFilter.Length ;i++)
				{
					cjpFilter.services[i] = new RequestServicePrivate();
					((RequestServicePrivate)cjpFilter.services[i]).privateID = uidFilter[i].ToString();
				}
			}

			return cjpFilter;
		}


		/// <summary>
		/// Creates and populates a CJP Operators Filter
		/// using the operator details from the TD request.
		/// </summary>
		/// <param name="operatorFilter"></param>
		/// <param name="operatorInclude"></param>
		/// <returns>A populated Operators filter, or null if none is required</returns>
		protected Operators CreateOperatorFilter(string[] operatorFilter, bool operatorInclude)
		{
			Operators cjpOperatorFilter = null;

			if  (operatorFilter != null && operatorFilter.Length > 0)
			{
				cjpOperatorFilter = new Operators();

				cjpOperatorFilter.include = operatorInclude;

				cjpOperatorFilter.operatorCodes = new OperatorCode[operatorFilter.Length];

				for (int i = 0; i < operatorFilter.Length; i++)
				{
					cjpOperatorFilter.operatorCodes[i] = new OperatorCode();
					cjpOperatorFilter.operatorCodes[i].operatorCode = operatorFilter[i];
				}
			}
			
			return cjpOperatorFilter;
		}


		/// <summary>
		/// Create a single  fully-populated CJP JourneyRequest object
		/// for a single mode trunk request for a specified date.
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
		protected JourneyRequest PopulateSingleTrunkRequest(ITDJourneyRequest tdRequest,
			ModeType mode,
			TDDateTime inputDateTime,
			bool returnJourney,
			int referenceNumber, 
			int seqNo,	
			string sessionId,
			bool referenceTransaction, 
			int userType, 
			string language, 
            bool useOverrideTimespan)
		{
			JourneyRequest cjpRequest = InitialiseNewRequest(referenceNumber, seqNo,
				sessionId, referenceTransaction,
				userType, language);

			cjpRequest.serviceFilter  = CreateServiceFilter (tdRequest.TrainUidFilter,    tdRequest.TrainUidFilterIsInclude);
			cjpRequest.operatorFilter = CreateOperatorFilter(tdRequest.SelectedOperators, tdRequest.UseOnlySpecifiedOperators);

			TDDateTime[] searchTimes =  new TDDateTime[] { new TDDateTime(inputDateTime.GetDateTime()), new TDDateTime(inputDateTime.GetDateTime()) };			

			StationType stationType = ModeToStationType(mode);

			if  (returnJourney)
			{
				cjpRequest.depart = !tdRequest.ReturnArriveBefore;

				//if we have a sequence then we know we are in adjust, therefore we do not need to get the full interval
                
				if (tdRequest.Sequence == 0)
				{
					if  (tdRequest.ReturnArriveBefore)
					{
                        // If we're using the OverrideTimespan, then need to ignore the ArriveBefore flag, to ensure the SearchTimes range returned is valid
                        searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.ReturnAnyTime, useOverrideTimespan ? false : true, mode, useOverrideTimespan, returnJourney);
					}
					else
					{
                        searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.ReturnAnyTime, false, mode, useOverrideTimespan, returnJourney);
					}
				}

				cjpRequest.origin = tdRequest.ReturnOriginLocation.ToRequestPlace(cjpRequest.depart ? searchTimes[0] : null, stationType, true, false, false);
				cjpRequest.destination = tdRequest.ReturnDestinationLocation.ToRequestPlace(cjpRequest.depart ? null : searchTimes[1], stationType, true, false, false);
			}
			else
			{
				cjpRequest.depart = !tdRequest.OutwardArriveBefore;

				//if we have a sequence then we know we are in adjust, therefore we do not need to get the full interval
				if (tdRequest.Sequence == 0)
				{
					if  (tdRequest.OutwardArriveBefore)
					{
                        // If we're using the OverrideTimespan, then need to ignore the ArriveBefore flag, to ensure the SearchTimes range returned is valid
                        searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.OutwardAnyTime, useOverrideTimespan ? false : true, mode, useOverrideTimespan, returnJourney);
					}
					else
					{
                        searchTimes = GetTrunkSearchTimes(inputDateTime.GetDateTime(), tdRequest.OutwardAnyTime, false, mode, useOverrideTimespan, returnJourney);
					}
				}				

				cjpRequest.origin = tdRequest.OriginLocation.ToRequestPlace(cjpRequest.depart ? searchTimes[0] : null, stationType, true, false, false);
				cjpRequest.destination = tdRequest.DestinationLocation.ToRequestPlace(cjpRequest.depart ? null : searchTimes[1], stationType, true, false, false);
			}

			cjpRequest.parkNRide = false;

			TimeSpan intervalTimeSpan = searchTimes[1].GetDateTime() - searchTimes[0].GetDateTime();

			cjpRequest.publicParameters = SetPublicParametersForTrunk(tdRequest, intervalTimeSpan, mode, returnJourney, true);

			cjpRequest.modeFilter = new Modes();
			cjpRequest.modeFilter.include = true;

			// single-region coach trunk requests might go to a traveline, which
			//  might not be able to distinguish between buses and coaches, so
			//   we need to pass both modes ...
			
			if  (mode == ModeType.Coach)
			{
				cjpRequest.modeFilter.modes = CreateModeArray(new ModeType[] { ModeType.Coach, ModeType.Bus, ModeType.Ferry } );
			}
			else
			{
				cjpRequest.modeFilter.modes = CreateModeArray(new ModeType[] { mode } );
			}

			return cjpRequest;
		}

		
		/// <summary>
		/// Fill the PublicParameters fields for a single mode trunk request.
		/// </summary>
		/// <param name="tdRequest"></param>
		/// <param name="interval"></param>
		/// <returns>Populated PublicParameters object</returns>
		protected PublicParameters SetPublicParametersForTrunk(ITDJourneyRequest tdRequest, TimeSpan interval, ModeType mode, bool returnJourney, bool includeVias)
		{
			PublicParameters parameters = new PublicParameters();
		
			parameters.trunkPlan = true;

			if	(mode == ModeType.Air)
			{
				parameters.algorithm = tdRequest.DirectFlightsOnly ? PublicAlgorithmType.NoChanges : PublicAlgorithmType.Default;
				parameters.extraCheckInTime = tdRequest.ExtraCheckinTime.GetDateTime();
				parameters.intermediateStops = IntermediateStopsType.None;
			}
			else
			{
				parameters.algorithm = tdRequest.PublicAlgorithm;
				parameters.intermediateStops = IntermediateStopsType.All;
			}
						
			parameters.interchangeSpeed = tdRequest.InterchangeSpeed;
			parameters.walkSpeed = tdRequest.WalkingSpeed;
			parameters.maxWalkDistance = tdRequest.WalkingSpeed * tdRequest.MaxWalkingTime;

			//if we have a sequence then we are in adjust, so we do not need an interval
			if (tdRequest.Sequence == 0)
			{
				parameters.rangeType = RangeType.Interval;
				parameters.interval = DateTime.MinValue + interval;

				parameters.sequence = 0;
				parameters.extraSequence = 0;				
			}
			else
			{
				parameters.rangeType = RangeType.Sequence;
				parameters.interval = DateTime.MinValue;

				parameters.sequence = tdRequest.Sequence;
				parameters.extraSequence = 0;				
			}
			parameters.extraInterval = DateTime.MinValue;

			if	(includeVias)
			{
				if  (tdRequest.PublicViaLocations != null && tdRequest.PublicViaLocations.Length > 0)
				{
					int vias = 0;
					for (int i = 0; i < tdRequest.PublicViaLocations.Length; i++)
					{           
						if	(tdRequest.PublicViaLocations[i].Status == TDLocationStatus.Valid)
						{
							vias++;
						}
					}

					if (vias > 0)
					{
						parameters.vias = new RequestPlace[vias];

						for (int i = 0; i < tdRequest.PublicViaLocations.Length; i++)
						{       
							if (tdRequest.PublicViaLocations[i].Status == TDLocationStatus.Valid)
							{
								parameters.vias[i]
									= tdRequest.PublicViaLocations[i].ToRequestPlace(returnJourney ? tdRequest.ViaLocationReturnStopoverTime
									: tdRequest.ViaLocationOutwardStopoverTime, ModeToStationType(mode), true, false, true);                      
							}
						}
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
					int softVias = 0;

					for (int i = 0; i < tdRequest.PublicSoftViaLocations.Length; i++)
					{           
						if  (tdRequest.PublicSoftViaLocations[i].Status == TDLocationStatus.Valid)
						{
							softVias++;
						}
					}

					if (softVias > 0)
					{
						parameters.softVias = new RequestPlace[softVias];

						for (int i = 0; i < tdRequest.PublicSoftViaLocations.Length; i++)
						{       
							if (tdRequest.PublicSoftViaLocations[i].Status == TDLocationStatus.Valid)
							{
								parameters.softVias[i] = tdRequest.PublicSoftViaLocations[i].ToRequestPlace(null, ModeToStationType(mode), true, false, true);
							}
						}
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
					int notVias = 0;

					for (int i = 0; i < tdRequest.PublicNotViaLocations.Length; i++)
					{           
						if (tdRequest.PublicNotViaLocations[i].Status == TDLocationStatus.Valid)
						{
							notVias++;
						}
					}

					if (notVias > 0)
					{
						parameters.notVias = new RequestPlace[notVias];

						for (int i = 0; i < tdRequest.PublicNotViaLocations.Length; i++)
						{       
							if (tdRequest.PublicNotViaLocations[i].Status == TDLocationStatus.Valid)
							{
								parameters.notVias[i] = tdRequest.PublicNotViaLocations[i].ToRequestPlace(null, ModeToStationType(mode), true, false, true);
							}
						}
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
			}

            // set up Routing guide specific values
            parameters.routingGuideInfluenced = tdRequest.RoutingGuideInfluenced;
            parameters.rejectNonRGCompliantJourneys = tdRequest.RoutingGuideCompliantJourneysOnly;
            parameters.routeCodes = tdRequest.RouteCodes;
            
			return parameters;
		}

		/// <summary>
		/// Calculates the search start time and the search interval for a trunk request of specified mode
		/// </summary>
		/// <param name="dateTime">The date/time specified by the user (may be for out or return)</param>
		/// <param name="anyTime">True if user has not asked for a specific start time</param>
		/// <param name="arriveBefore">True if request is for "arriving by" a specified time</param>
		/// <param name="mode">The transport mode for this request</param>
		/// <returns>Array of precisely two TDDateTime objects, for start and end of range </returns>	
		protected TDDateTime[] GetTrunkSearchTimes(DateTime dateTime, bool anyTime, bool arriveBefore, ModeType mode, bool useOverrideTimespan, bool isReturn)
		{
            DateTime preciseNow = DateTime.Now;

            // If we pass milliseconds to the CJP we get an exception.  We therefore need to create a
            // DateTime that has 0 for milliseconds.
            DateTime now = new DateTime(preciseNow.Year, preciseNow.Month, preciseNow.Day, 
                                        preciseNow.Hour, preciseNow.Minute, preciseNow.Second);
            
			DateTime startDateTime  = dateTime;

			int interval = 0;
			int intervalBefore = 0;
			int intervalAfter  = 0;

			IPropertyProvider propertyProvider = Properties.Current;

            if (mode != ModeType.Car)
            {
                if (arriveBefore)
                {
                    intervalBefore = Int32.Parse(propertyProvider[string.Format(JourneyControlConstants.TrunkSearchInterval3, mode.ToString())]);
                    intervalAfter = Int32.Parse(propertyProvider[string.Format(JourneyControlConstants.TrunkSearchInterval4, mode.ToString())]);
                }
                else
                {
                    intervalBefore = Int32.Parse(propertyProvider[string.Format(JourneyControlConstants.TrunkSearchInterval1, mode.ToString())]);
                    intervalAfter = Int32.Parse(propertyProvider[string.Format(JourneyControlConstants.TrunkSearchInterval2, mode.ToString())]);
                }
            }

			bool isToday = ((dateTime.Year == now.Year) && (dateTime.DayOfYear == now.DayOfYear));


			if  (isToday && anyTime)        // current date, no time specified
			{
				interval = intervalBefore + intervalAfter;

				if  (arriveBefore)
				{
                    startDateTime = now + new TimeSpan(0, interval, 0, 0);
				}
				else
				{
					startDateTime = now;
				}
			}
			else if (isToday)               // current date, specific time
			{
                DateTime testTime = dateTime;

                //Only adjust the outward time 
                if (tdRequest.AdjustTimeWithIntervalBefore)
                {
                    testTime = dateTime - (new TimeSpan(intervalBefore, 0, 0));
                }

				if  (arriveBefore)
				{
					startDateTime = dateTime + (new TimeSpan(intervalAfter, 0, 0));
					interval = intervalBefore + intervalAfter;
				}
				else
				{
					startDateTime = testTime;
					interval = intervalBefore + intervalAfter;
				}
			}
			else if (anyTime)               // future date, no time specified
			{
				interval = HOURS_IN_DAY;

				startDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0);

				if  (arriveBefore)
				{
					startDateTime += new TimeSpan(1, 0, 0, 0);
				}
			}
			else                            // future date, specific time
			{
				interval = intervalBefore + intervalAfter;

				if  (arriveBefore)
				{
					startDateTime = dateTime + new TimeSpan(intervalAfter, 0, 0);
				}
				else
				{
                    if (tdRequest.AdjustTimeWithIntervalBefore)
                    {
                        startDateTime = dateTime - new TimeSpan(intervalBefore, 0, 0);
                    }
                    else
                    {
                        startDateTime = dateTime;
                    }
				}
			}

			TimeSpan intervalTimeSpan;

			if	(interval < 24)
			{
				intervalTimeSpan = new TimeSpan(interval, 0, 0);
			}
			else
			{
				intervalTimeSpan = new TimeSpan(23, 59, 59);
			}

            if (useOverrideTimespan && (tdRequest.FindAMode == FindAPlannerMode.Flight
                    || tdRequest.FindAMode == FindAPlannerMode.Train
                    || tdRequest.FindAMode == FindAPlannerMode.Trunk
                    || tdRequest.FindAMode == FindAPlannerMode.TrunkCostBased
                    || tdRequest.FindAMode == FindAPlannerMode.TrunkStation))
            {
                               
                    #region Override Dates logic
                    // Get starttime from Database
                    string startTime = Properties.Current[string.Format("PublicJourney.{0}.StartTime",tdRequest.FindAMode)]; ;

                    // Use car time if searchtimes is being requested for the car mode
                    if (mode == ModeType.Car)
                    {
                        if (isReturn)
                            startTime = Properties.Current["CityToCity.CarJourney.ReturnTime"];
                        else
                            startTime = Properties.Current["CityToCity.CarJourney.OutwardTime"];
                    }

                    int hours = int.Parse(startTime.Substring(0, 2));
                    int minutes = int.Parse(startTime.Substring(2, 2));

                    DateTime overrideStartDateTime = new DateTime(startDateTime.Year, startDateTime.Month, startDateTime.Day, hours, minutes, 0);
                    DateTime overrideEndDateTime = new DateTime();

                    double intervalHours = double.Parse(Properties.Current[string.Format("PublicJourney.{0}.Interval",tdRequest.FindAMode)]);
                    if (intervalHours < 24)
                    {
                        overrideEndDateTime = overrideStartDateTime.AddHours(intervalHours);
                    }
                    else
                    {
                        overrideEndDateTime = overrideStartDateTime.Add(new TimeSpan(23, 59, 59));
                    }

                    // Because car journeys only need to plan a journey from a time, we can return
                    // the same Start and End times, and ignore the interval set time
                    if (mode == ModeType.Car)
                        overrideEndDateTime = overrideStartDateTime;

                    return new TDDateTime[] { new TDDateTime(overrideStartDateTime), new TDDateTime(overrideEndDateTime) };
                    #endregion
                
            }
            else
            {
                if (!arriveBefore)
                {
                    return new TDDateTime[] { new TDDateTime(startDateTime), new TDDateTime(startDateTime + intervalTimeSpan) };
                }
                else
                {
                    return new TDDateTime[] { new TDDateTime(startDateTime - intervalTimeSpan), new TDDateTime(startDateTime) };
                }
            }
		}

        /// <summary>
        /// Fill the PrivateParameters fields for a single multimodal request.
        /// </summary>
        /// <param name="tdRequest"></param>
        /// <returns>Populated CJP PrivateParameters object</returns>
        protected PrivateParameters SetPrivateParameters(ITDJourneyRequest tdRequest)
        {
            PrivateParameters parameters = new PrivateParameters();

            parameters.flowType = FlowType.Congestion;
            parameters.avoidMotorway = tdRequest.AvoidMotorways;
            parameters.avoidRoads = CreateRoads(tdRequest.AvoidRoads);
            parameters.useRoads = CreateRoads(tdRequest.IncludeRoads);
            parameters.algorithm = tdRequest.PrivateAlgorithm;
            parameters.maxSpeed = tdRequest.DrivingSpeed;
            parameters.avoidFerries = tdRequest.AvoidFerries;
            parameters.avoidToll = tdRequest.AvoidTolls;
            parameters.vehicleType = tdRequest.VehicleType;
            parameters.banMotorway = tdRequest.DoNotUseMotorways;
            parameters.banNamedAccessRestrictions = tdRequest.BanUnknownLimitedAccess;
            parameters.fuelConsumption = decimal.ToInt32(Convert.ToDecimal((tdRequest.FuelConsumption)));
            parameters.fuelPrice = decimal.ToInt32(Convert.ToDecimal((tdRequest.FuelPrice)));

            // Populating the toids to be avoided when planning the journey
            parameters.bannedTOIDs = tdRequest.IsOutwardRequired ? tdRequest.AvoidToidsOutward : tdRequest.AvoidToidsReturn;

            if (tdRequest.PrivateViaLocation != null && tdRequest.PrivateViaLocation.Status == TDLocationStatus.Valid)
            {
                parameters.vias = new RequestPlace[1];
                parameters.vias[0] = tdRequest.PrivateViaLocation.ToRequestPlace(null, StationType.Undetermined, false, true, true);
            }
            else
            {
                parameters.vias = new RequestPlace[0];
            }

            return parameters;
        }

        /// <summary>
        /// Create an array of Roads and populate the contents from an array of strings
        /// </summary>
        /// <param name="Roads">String array of road names</param>
        /// <returns>Array of TransportDirect.JourneyPlanning.CJPInterface.Road</returns>
        private Road[] CreateRoads(string[] stringRoads)
        {
            Road[] roadResult = new Road[stringRoads.Length];

            for (int i = 0; i < stringRoads.Length; i++)
            {
                roadResult[i] = new Road();
                roadResult[i].roadNumber = stringRoads[i];
            }
            return roadResult;
        }

		/// <summary>
		/// Read/write-only - the TDJourneyRequest supllied by the caller
		/// </summary>
		protected ITDJourneyRequest TDRequest
		{
			get { return tdRequest; }
			set { tdRequest = value; }
		}
	}
}
