// *********************************************** 
// NAME                 : StopEventConverter.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 12/01/2005
// DESCRIPTION  : Class holding all methods used to build and convert StopEvent requests and results.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/StopEventManager/StopEventConverter.cs-arc  $
//
//   Rev 1.3   Apr 15 2010 10:29:52   mturner
//Updated to prevent the word 'statioon' being removed from the end of station names.
//Resolution for 5512: Do not remove the word station from departure board results
//
//   Rev 1.2   Feb 17 2010 16:42:26   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.1   Mar 12 2008 12:03:44   build
//Manually resolve merge probelms with stream 4542 merge.
//
//   Rev 1.0.1.0   Feb 18 2008 16:08:46   pscott
//IR 5497 - Add new functioinality for FirstToday,FirstTomorrow,LastToday and Last Tomorrow
//
//   Rev 1.0   Nov 08 2007 12:21:44   mturner
//Initial revision.
//
//   Rev 1.3   May 03 2006 15:36:36   COwczarek
//realTime parameter for stop event requests now obtained from property service and not hardcoded
//Resolution for 4062: Mobile DepartureBoards: Real Time Flag set to true
//
//   Rev 1.2   May 04 2005 11:48:16   schand
//Added code to convert station name to TitleCase (mixed case). 
//Also removed the word 'Station'
//
//   Rev 1.1   Mar 14 2005 15:13:22   schand
//Changes for configurable switch between CJP and RTTI.
//Added methof FillMode
//
//   Rev 1.0   Feb 28 2005 16:23:58   passuied
//Initial revision.
//
//   Rev 1.11   Feb 24 2005 14:19:56   passuied
//Changes for FxCop
//
//   Rev 1.10   Feb 16 2005 14:54:18   passuied
//Change in interface and behaviour of time Request
//
//possibility to plan in the past within configurable time window
//
//   Rev 1.9   Feb 11 2005 14:38:56   passuied
//uncommented some code
//
//   Rev 1.8   Feb 11 2005 11:08:14   passuied
//changes to comply to the new cjp
//
//   Rev 1.7   Feb 02 2005 10:19:24   passuied
//removed type from results after design review
//
//   Rev 1.6   Jan 31 2005 11:51:32   passuied
//tidied up
//
//   Rev 1.5   Jan 19 2005 14:01:44   passuied
//added more validation + changed UT to allow destination to be optional
//
//   Rev 1.4   Jan 18 2005 17:36:10   passuied
//changed after update of CjpInterface
//
//   Rev 1.3   Jan 17 2005 14:48:52   passuied
//Latest code with Unit test OK!
//
//   Rev 1.2   Jan 14 2005 20:58:20   passuied
//Changed visibility of method
//
//   Rev 1.1   Jan 14 2005 10:21:36   passuied
//changes in interface
//
//   Rev 1.0   Jan 12 2005 14:49:38   passuied
//Initial revision.

using System;
using System.Globalization;
using System.Text;


using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.UserPortal.DepartureBoardService.StopEventManager
{
	/// <summary>
	/// Class holding all methods used to build and convert StopEvent requests and results.
	/// </summary>
	internal abstract class StopEventConverter
	{
		

		#region Static methods for Building the StopEventRequest


		private static string FormatSeqNo(int seqNo)
		{
			return seqNo.ToString("-0000");
		}

		private static void PopulateBaseEventRequest(
			EventRequest targetRequest,
			DBSLocation stopLocation,
            string operatorCode,
            string serviceNumber,
            bool showDepartures,
			bool showCallingStops)
		{
			targetRequest.referenceTransaction = false;
			targetRequest.requestID = SqlHelper.FormatRef(SqlHelper.GetRefNumInt()) + FormatSeqNo(0);
			targetRequest.language = "EN";
			targetRequest.sessionID = string.Empty;
			
			targetRequest.NaPTANIDs = stopLocation.NaptanIds;
			targetRequest.locality = stopLocation.Locality;
			targetRequest.intermediateStops = showCallingStops? IntermediateStopsType.All : IntermediateStopsType.None;

            try
            {
                targetRequest.realTime = Convert.ToBoolean(Properties.Current[Keys.RealTimeRequired].ToString());
            }
            catch(NullReferenceException)
            {
                // not defined in property service
                targetRequest.realTime = false;
            }
            catch(FormatException)
            {
                // invalid value in property service
                targetRequest.realTime = false;
            }
            
			targetRequest.modeFilter = new Modes();
			targetRequest.modeFilter.include = true;

			// Now Fill the mode according to Code Type
			FillMode(targetRequest , stopLocation);

			// service filter if parameter not empty
			if (serviceNumber.Length != 0)
			{
				targetRequest.serviceFilter = new TransportDirect.JourneyPlanning.CJPInterface.ServiceFilter();
				targetRequest.serviceFilter.include = true;
				RequestServiceNumber service = new RequestServiceNumber();
				service.direction = string.Empty; 
				service.serviceNumber = serviceNumber;
                service.operatorCode = operatorCode;
				targetRequest.serviceFilter.services = new RequestServiceNumber[1];
				targetRequest.serviceFilter.services[0] = service;
			}
		}


		/// <summary>
		/// Populate the basic information for a StopEventRequest using the given parameters
		/// </summary>
		/// <param name="targetRequest">request to populate</param>
		/// <param name="stopLocation">location information for the requested stop</param>
		/// <param name="showDepartures">indicates whether to show departures or arrivals</param>
		/// <param name="showCallingStops">indicates whether to show calling stops</param>
		private static void PopulateBaseStopEventRequest( 
			StopEventRequest targetRequest,
			DBSLocation stopLocation,
            string operatorCode,
            string serviceNumber,
			bool showDepartures,
			bool showCallingStops)
		{

            PopulateBaseEventRequest(targetRequest, stopLocation, operatorCode, serviceNumber, showDepartures, showCallingStops);

			targetRequest.arriveDepart = showDepartures? EventArriveDepartType.Depart : EventArriveDepartType.Arrive;
			targetRequest.rangeType = RangeType.Sequence;
			string sSequence = Properties.Current[Keys.DefaultRangeKey];
			int sequence = 5; // default value;
			if (sSequence.Length != 0)
			{
				sequence = Convert.ToInt32(sSequence, CultureInfo.InvariantCulture.NumberFormat);
			}
			else
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Warning,
					string.Format(Messages.MissingProperty, Keys.DefaultRangeKey));
				Logger.Write(oe);
			}
			targetRequest.sequence = sequence;
			
			targetRequest.startTime = DateTime.Now; // Default to be overwritten when dealing with special cases.
			
			
			string sFirst = Properties.Current[Keys.FirstEventOnlyKey];
			if (sFirst.Length != 0)
			{
				bool isFirst = Convert.ToBoolean(sFirst, CultureInfo.InvariantCulture.NumberFormat);
				targetRequest.firstServiceEventOnly = isFirst; 
			}
			else
			{
				targetRequest.firstServiceEventOnly = true; // Default is true
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Warning,
					string.Format(Messages.MissingProperty, Keys.FirstEventOnlyKey));
				Logger.Write(oe);
			}

	
		}


		/// <summary>
		/// Instantiate and populate a new StopEventRequest using the given parameters
		/// </summary>
		/// <param name="stopLocation">location information of the stop</param>
		/// <param name="serviceNumber">optional service number</param>
		/// <param name="showDepartures">indicates whether to show departures or arrivals</param>
		/// <param name="showCallingStops">indicates whether to show calling stops</param>
		/// <returns>new StopEventRequest</returns>
		public static EventRequest BuildStopEventRequest(
			DBSLocation stopLocation,
            string operatorCode,
            string serviceNumber,
			bool showDepartures,
			bool showCallingStops)
		{
			StopEventRequest req = new StopEventRequest();
			PopulateBaseStopEventRequest(req, stopLocation, operatorCode, serviceNumber,  showDepartures, showCallingStops);

			return req;

		}

		/// <summary>
		/// Overloaded Method. Instantiate and populate a new StopEventRequest using the given parameters.
		/// </summary>
		/// <param name="stopLocation">location information of the stop</param>
		/// <param name="serviceNumber">optional service number</param>
		/// <param name="showDepartures">indicates whether to show departures or arrivals</param>
		/// <param name="showCallingStops">indicates whether to show calling stops</param>
		/// <param name="locationFilter">filter location to narrow returned results</param>
		/// <param name="startTime">start time for the request. (Now, last, first, +1h, +2h, +3h)</param>
		/// <param name="range">range of the request. ie. number of results to return</param>
		/// <returns>new StopEventRequest</returns>
		public static EventRequest BuildStopEventRequest(
			DBSLocation stopLocation,
            string operatorCode,
            string serviceNumber,
			bool showDepartures,
			bool showCallingStops,
			DBSLocation locationFilter,
			DBSTimeRequest startTime,
			DBSRangeType rangeType,
			int range)
		{
			EventRequest req = null;


            if (startTime.Type == TimeRequestType.First ||
                    startTime.Type == TimeRequestType.FirstToday ||
                    startTime.Type == TimeRequestType.FirstTomorrow ||
				    startTime.Type == TimeRequestType.Last ||
				    startTime.Type == TimeRequestType.LastToday ||
				    startTime.Type == TimeRequestType.LastTomorrow
                )
			{
				int dayBeginHour = Convert.ToInt32(Properties.Current[Keys.DayStartHourKey], CultureInfo.InvariantCulture.NumberFormat);
				bool eachService = Convert.ToBoolean(Properties.Current[Keys.EachServiceKey], CultureInfo.InvariantCulture.NumberFormat);

				// if Time is First or last need to instanciate an extended FirstLastServiceRequest!
				if (startTime.Type == TimeRequestType.First ||
                    startTime.Type == TimeRequestType.FirstToday ||
                    startTime.Type == TimeRequestType.FirstTomorrow)
				{
					req = new FirstLastServiceRequest();

					FirstLastServiceRequest flReq = req as FirstLastServiceRequest;
					flReq.type = FirstLastServiceRequestType.First;
					flReq.depart = showDepartures; 
					flReq.eachService = eachService;
					
					
					// call base populator
                    PopulateBaseEventRequest(req, stopLocation, operatorCode, serviceNumber, showDepartures, showCallingStops);

					// if first is requested it will be for tomorrow's first service
					flReq.date = DateTime.Today;
					
					// if it's after 3 in the morning (configurable) needs to find first service of tomorrow!
                    if (startTime.Type == TimeRequestType.First )
				    {
                        if ( DateTime.Now.Hour > dayBeginHour)
						    flReq.date = DateTime.Today.AddDays(1);
                    }
                    if (startTime.Type == TimeRequestType.FirstTomorrow)
                    {
                        flReq.date = DateTime.Today.AddDays(1);
                    }
				}
                if (startTime.Type == TimeRequestType.Last ||
                    startTime.Type == TimeRequestType.LastToday ||
                    startTime.Type == TimeRequestType.LastTomorrow)
				{
					req = new FirstLastServiceRequest();
					FirstLastServiceRequest flReq = req as FirstLastServiceRequest;
					flReq.type = FirstLastServiceRequestType.Last;
					flReq.depart = showDepartures;
					flReq.eachService = eachService; 

					// call base populator
					PopulateBaseEventRequest(req, stopLocation, operatorCode, serviceNumber, showDepartures, showCallingStops);
					
					// if last is requested it will be for today's first service
					flReq.date = DateTime.Today;

                    if (startTime.Type == TimeRequestType.Last)
                    {
					// If it's before 3 then, it is considered to be the day before. Then set to yesterday's date.
					if (DateTime.Now.Hour < dayBeginHour)
						flReq.date = DateTime.Today.AddDays(-1);
                    }
                    if (startTime.Type == TimeRequestType.LastTomorrow)
                    {
                        flReq.date = DateTime.Today.AddDays(1);
                    }
				}
			}
				// startTime different from first/last evaluate values
			else
			{
				req = new StopEventRequest();

				StopEventRequest seReq = req as StopEventRequest;
				
				// call base populator
				PopulateBaseStopEventRequest(seReq, stopLocation, operatorCode, serviceNumber, showDepartures, showCallingStops);

				// Populate the rest now

				if (startTime.Type == TimeRequestType.TimeToday || startTime.Type == TimeRequestType.TimeTomorrow)
				{
					
					seReq.startTime = DateTime.Today;

					// if type of time request is TimeTomorrow then start DateTime will be tomorrow
					if (startTime.Type == TimeRequestType.TimeTomorrow)
						seReq.startTime = seReq.startTime.AddDays(1);
					
					seReq.startTime = seReq.startTime.AddHours( startTime.Hour);
					seReq.startTime = seReq.startTime.AddMinutes( startTime.Minute);

				}
					// otherwise take Current Time
				else
				{
					seReq.startTime = DateTime.Now;				
				}

				// range of the request
				if (rangeType == DBSRangeType.Sequence)
				{
					// if RangeType == sequence, set sequence to range
					seReq.rangeType = RangeType.Sequence;
					seReq.sequence = range;
					seReq.interval = DateTime.MinValue;
				}
				else
				{
					// else = interval, convert interval
					seReq.rangeType = RangeType.Interval;
					seReq.sequence = 0;
					seReq.interval = DateTime.Today;
					seReq.interval = seReq.interval.AddMinutes(range); 
				}

				
				
			}

			
			// Now Populate All other filters

			// location filter
			
			// only populate if filter not null, as it's optional
			if (locationFilter != null)
			{
				RequestStopFilter rsFilter = new RequestStopFilter();

				rsFilter.actual = false; // see design for explanation

				// add a requestStop for location filter
				rsFilter.NaPTANIDs = locationFilter.NaptanIds;
			
			

				// filter will be destination if showdepartures true, and origin if showdepartures false
				if (showDepartures)
					req.destinationFilter = rsFilter;
				else
					req.originFilter = rsFilter;
			}
			return req;

			
		}

		#endregion


		#region Static methods for Building the DBSResult

		/// <summary>
		/// Method instantiating and populating a new DBSResult object from a StopEventResult object
		/// </summary>
		/// <param name="result">StopEventResult object</param>
		/// <param name="showCallingStops">indicates whether to display the calling stops or not.</param>
		/// <returns>New populated DBSResult object</returns>
		public static DBSResult BuildDBSResult( StopEventResult result, bool showCallingStops)
		{
			DBSResult formattedResult = new DBSResult();

			// For all StopEvents in StopEventResult, create DBSStopEvents
			if ( result.stopEvents != null)
			{
				formattedResult.StopEvents = new DBSStopEvent[result.stopEvents.Length]; 
				for (int i=0; i< result.stopEvents.Length; i++)
				{
					formattedResult.StopEvents[i] = BuildDBSStopEvent( result.stopEvents[i], showCallingStops);
				}
			}
			else
			{
				formattedResult.StopEvents = null;
			}

			// Check on CJP if msg returned when successful. when messages returned and no stop Events returned
			if ((result.messages == null ||result.messages.Length != 0) && formattedResult.StopEvents == null)
			{
				// Handle Messages from CJP. Create  DBSMessages
				StringBuilder sb = new StringBuilder();

				foreach ( Message msg in result.messages)
				{
					sb.AppendFormat(Messages.CJPMessage, msg.code, msg.description);
				}

				DBSMessage message = new DBSMessage();
				message.Code = (int)DBSMessageIdentifier.CJPReturnedMessages; // see Message Codes spreadsheet
				message.Description = string.Format(Messages.CJPReturnedMessages, sb.ToString());

				formattedResult.Messages = new DBSMessage[]{message};
			}
	
			return formattedResult;

		}

		/// <summary>
		/// Method that instantiates and populates a DBSResult object with only messages in it.
		/// </summary>
		/// <param name="messages">array of messages for populating the DBSResult</param>
		/// <returns>new populated DBSResult</returns>
		public static DBSResult BuildDBSResult( DBSMessage[] messages)
		{
			DBSResult result = new DBSResult();

			result.Messages = messages;

			return result;
		}


		/// <summary>
		/// Method that instantiates and populate a DBSService using a Service object.
		/// </summary>
		/// <param name="service">CJPInterface Service object</param>
		/// <returns>new DBSService object</returns>
		public static DBSService BuildDBSService ( Service service)
		{
			DBSService dsvc = new DBSService();
			dsvc.OperatorCode = service.operatorCode;
			dsvc.OperatorName = service.operatorName;
			dsvc.ServiceNumber = service.serviceNumber;

			return dsvc;
		}

		/// <summary>
		/// Method that instantiates and populate a DBSStop using a Stop object.
		/// </summary>
		/// <param name="stop">CJPInterface Stop object</param>
		/// <returns>new DBSStop</returns>
		public static DBSStop BuildDBSStop ( Stop stop)
		{
			DBSStop dst = new DBSStop();
			dst.Name = DecorateStopName(stop.name);
			dst.NaptanId = stop.NaPTANID;

			return dst;
		}

		/// <summary>
		/// Method that converts the CJPInterface RealTimeType into the DBSRealTimeType
		/// </summary>
		/// <param name="type">CJPInterface RealTimeType object</param>
		/// <returns>converted DBSRealTimeType</returns>
		public static DBSRealTimeType GetDBSRealTimeType( RealTimeType type)
		{
			switch (type)
			{
				case RealTimeType.Estimated:
					return DBSRealTimeType.Estimated;
				case RealTimeType.Recorded:
					return DBSRealTimeType.Recorded;
				default:
					return DBSRealTimeType.Estimated;
			}
		}

		/// <summary>
		/// Method that instantiates and populates a new DBSRealTime object using a RealTime object
		/// </summary>
		/// <param name="realTime">CJPInterface RealTime object</param>
		/// <returns>new DBSRealTime object</returns>
		public static DBSRealTime BuildDBSRealTime ( RealTime realTime, Event anEvent)
		{
			if (realTime != null)
			{
				DBSRealTime drt = new DBSRealTime();

				drt.ArriveTime = realTime.arriveTime;
				drt.DepartTime = realTime.departTime;

				drt.ArriveTimeType = GetDBSRealTimeType( realTime.arriveTimeType);
				drt.DepartTimeType = GetDBSRealTimeType( realTime.departTimeType);

				return drt;			
			}
			else
				return null;
		}

		/// <summary>
		/// Method that instantiates and populates a new DBSEvent object using an Event object.
		/// </summary>
		/// <param name="anEvent">CJPInterface Event object</param>
		/// <returns>new DBSEvent object</returns>
		public static DBSEvent BuildDBSEvent ( Event anEvent)
		{
			if (anEvent != null)
			{
				DBSEvent dbe = new DBSEvent();
				switch (anEvent.activity)
				{
					case ActivityType.Arrive:
						dbe.ActivityType = DBSActivityType.Arrive;
						break;
					case ActivityType.Depart:
						dbe.ActivityType = DBSActivityType.Depart;
						break;
					case ActivityType.ArriveDepart:
						dbe.ActivityType = DBSActivityType.ArriveDepart;
						break;
				}

				dbe.DepartTime = anEvent.departTime;
				dbe.ArriveTime = anEvent.arriveTime;
			
				dbe.RealTime = BuildDBSRealTime( anEvent.realTime, anEvent);
				dbe.Stop = BuildDBSStop (anEvent.stop);

				return dbe;
		
			}
			else
				return null;
		}

		/// <summary>
		/// Returns if stopEvents has calling stops depending on given previous and onward intermediate stops
		/// </summary>
		/// <param name="previous">given previous intermediate stops array</param>
		/// <param name="onward">given previous onward stops array</param>		/// 
		/// <returns></returns>
		public static CallingStopStatus GetCallingStopStatus(Event[] previous, Event[] onward)
		{
			if (previous != null && previous.Length != 0
				|| onward != null && onward.Length !=0)
				return CallingStopStatus.HasCallingStops;
			else
				return CallingStopStatus.NoCallingStops;
		}

		/// <summary>
		/// Method that instantiates and populates a new DBSStopEvent object using a StopEvent object
		/// </summary>
		/// <param name="stopEvent">CJPInterface StopEvent object</param>
		/// <param name="showCallingStops">whether to add calling stops info when available.</param>
		/// <returns></returns>
		public static DBSStopEvent BuildDBSStopEvent ( StopEvent stopEvent, bool showCallingStops)
		{
			DBSStopEvent dse = new DBSStopEvent();

			// Determins if journey has existing calling stops
			dse.CallingStopStatus = GetCallingStopStatus(stopEvent.previousIntermediates, stopEvent.onwardIntermediates); 
			
			// if services array not empty, take first item, otherwise stay null
			dse.Service = (stopEvent.services.Length >0)? BuildDBSService( stopEvent.services[0]): null;

			// Added for CJP train info
			if (stopEvent.mode == ModeType.Rail)
				 dse.Mode = DepartureBoardType.Train ; 			
			else
				dse.Mode = DepartureBoardType.BusCoach ;
 
			// Populate all events
			dse.Departure = BuildDBSEvent( stopEvent.origin);
			dse.Arrival = BuildDBSEvent(stopEvent.destination);
			dse.Stop = BuildDBSEvent( stopEvent.stop);

			if (showCallingStops && stopEvent.previousIntermediates != null && stopEvent.previousIntermediates.Length != 0)
			{
				dse.PreviousIntermediates = new DBSEvent[stopEvent.previousIntermediates.Length];
				for( int i=0; i< stopEvent.previousIntermediates.Length; i++)
				{
					dse.PreviousIntermediates[i] = BuildDBSEvent( stopEvent.previousIntermediates[i]);
				}
			}

			if (showCallingStops && stopEvent.onwardIntermediates != null && stopEvent.onwardIntermediates.Length != 0)
			{
				dse.OnwardIntermediates = new DBSEvent[stopEvent.onwardIntermediates.Length];
				for( int i=0; i< stopEvent.onwardIntermediates.Length; i++)
				{
					dse.OnwardIntermediates[i] = BuildDBSEvent( stopEvent.onwardIntermediates[i]);
				}
			}

			return dse;
		}


		private static void FillMode( EventRequest targetRequest , DBSLocation stopLocation)
		{   bool isTrainResultFromCJP = false;

			try
			{
				isTrainResultFromCJP =   Convert.ToBoolean(Properties.Current[Keys.GetTrainInfoFromCJP].ToString())  ;				
			}
			catch(FormatException)
			{
			  isTrainResultFromCJP = false;
			}
				

			if (isTrainResultFromCJP && stopLocation.Type == LocationService.TDCodeType.CRS )
			{
				targetRequest.modeFilter.modes = new Mode[1];
				targetRequest.modeFilter.modes[0] = new Mode();
				targetRequest.modeFilter.modes[0].mode = ModeType.Rail;				
			}
			else
			{
				targetRequest.modeFilter.modes = new Mode[2];
				targetRequest.modeFilter.modes[0] = new Mode();
				targetRequest.modeFilter.modes[0].mode = ModeType.Bus;
				targetRequest.modeFilter.modes[1] = new Mode();
				targetRequest.modeFilter.modes[1].mode = ModeType.Coach;
			}			
		}

		public static string TitleCase(string givenwords)
		{
			try
			{
				if (givenwords==null)
					return string.Empty;

				System.Globalization.TextInfo textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo  ;   
				return textInfo.ToTitleCase(givenwords.ToLower());  
			}
			catch(ArgumentNullException)
			{
				return givenwords;
			}
		}

        /// <summary>
        /// Method that decorates a stop name by calling the TitleCase method on non empty strings
        /// </summary>
        /// <param name="stop">String representation of a stop name</param>
        /// <returns></returns>
		public static string DecorateStopName(string stop)
		{
			if (stop==null)
            {
				return string.Empty;
            }
			else
            {
				return TitleCase(stop.Trim());
			}
		}
		#endregion

	}
}
