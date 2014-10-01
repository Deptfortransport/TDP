// *********************************************** 
// NAME                 : DepartureBoardService.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 28/02/2005
// DESCRIPTION  : Component implementation of IDepartureBoardService interfance
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DepartureBoardService.cs-arc  $
//
//   Rev 1.4   Feb 17 2010 16:42:20   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.3   Apr 15 2008 11:42:54   mmodi
//Removed unused variable
//
//   Rev 1.2   Apr 11 2008 15:27:38   rbroddle
//Corrected problem with "FirstTomorrow" requests for trains
//Resolution for 4597: New enhanced exposed service categories
//
//   Rev 1.1   Mar 12 2008 11:58:32   build
//Manually resolve merge probelms with stream 4542 merge.
//
//   Rev 1.0.1.2   Mar 11 2008 16:36:24   rbroddle
//Updates for CCN431 Mobile service enhancements
//Resolution for 4597: New enhanced exposed service categories
//
//   Rev 1.0.1.1   Mar 04 2008 16:29:44   pscott
//IR5497
//
//   Rev 1.0.1.0   Feb 18 2008 16:00:44   pscott
//IR 5497 - Add new functioinality for FirstToday,FirstTomorrow,LastToday and Last Tomorrow
//
//   Rev 1.0   Nov 08 2007 12:21:04   mturner
//Initial revision.
//
//   Rev 1.8   May 14 2007 09:45:00   mturner
//Added code to check if a given NaPTAN is Rail (starts with 9100) and sets the request type to Rail when required.
//
//   Rev 1.7   May 03 2007 12:26:40   mturner
//Added code to handle new TDCodeType of NAPTAN and to return DepartureBoardType.BusCoach when it is encountered.
//
//   Rev 1.6   Mar 02 2006 16:22:46   RPhilpott
//Clear compilation warnings. 
//Resolution for 17: DEL 8.1 Workstream - RTTI
//
//   Rev 1.5   Jul 29 2005 18:34:08   Schand
//Fix for IR2628, IR2629. 
//
//HandleFirstAndLast Services() function has been added to get the first and last services iteratively. 
//Resolution for 2628: First Services are not working betweenWAT & FLE. Also unable to find first train (from WTN)
//Resolution for 2629: Unable to find last Train when filtered by arrival station
//
//   Rev 1.4   Jun 27 2005 18:24:10   passuied
//added method in interface and implementation to get the Time request types to display in mobile UI. Retrieved from the DataServices
//Resolution for 2561: Del 8 Stream: Create WAP Prototype pages
//
//   Rev 1.3   Mar 31 2005 19:10:16   schand
//Now returning which code (origin/destination/both) has failed. Fix for 4.4, 4.5
//
//   Rev 1.2   Mar 14 2005 15:09:28   schand
//Changes for configurable switch between CJP and RTTI.
//Added new function GetTrainResultFromCJP method.
//
//   Rev 1.1   Mar 01 2005 10:26:10   passuied
//added/ fixed logging!
//
//   Rev 1.0   Feb 28 2005 17:21:04   passuied
//Initial revision.

using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager;
using TransportDirect.UserPortal.DepartureBoardService.StopEventManager;
using TransportDirect.UserPortal.LocationService;
using Logger = System.Diagnostics.Trace; 

namespace TransportDirect.UserPortal.DepartureBoardService
{
	/// <summary>
	/// Component implementation of IDepartureBoardService interfance
	/// </summary>
	public class DepartureBoardService : MarshalByRefObject, IDepartureBoardService
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
		public DepartureBoardService()
		{
			
		}

        #endregion

        #region Public Methods

        /// <summary>
		/// Trip level request method for DepartureBoard information
		/// </summary>
		/// <param name="token">Authentications token</param>
		/// <param name="originNaptan">naptanId of Origin</param>
		/// <param name="destinationNaptan">naptanId of Destination</param>
		/// <param name="type">type of DepartureBoard requested</param>
		/// <param name="ServiceNumber"> requested service number (optional)</param>
		/// <param name="time"> start time</param>
		/// <param name="range"> max number of returned results</param>
		/// <param name="showDepartures"> show departure times / show arrival times</param>
		/// <param name="showCallingStops">show calling stops if available</param>
		/// <returns>DBS Result object holding DepartureBoard info</returns>
		public DBSResult GetDepartureBoardTrip(
			string token,
			DBSLocation originLocation,
			DBSLocation destinationLocation,
            string operatorCode,
            string serviceNumber,
			DBSTimeRequest time,
			DBSRangeType rangeType,
			int range,
			bool showDepartures,
			bool showCallingStops)
		{

			// Init for logging
			DateTime submitted = DateTime.Now;
		

			// Init for validation
			ArrayList errors = new ArrayList();
			bool valid = true;

			valid = DBSValidation.ValidateTimeRequest(time, errors);
			if (!valid)
				return GetInvalidRequestDBSResult(DBSMessageIdentifier.UserInvalidRequestTime, errors);


			DBSLocation[] origins = new DBSLocation[]{originLocation};
			DBSLocation[] destinations = new DBSLocation[]{destinationLocation};
			DBSValidationType validationType = DBSValidationType.Undefined ;
			valid = DBSValidation.ValidateLocationRequest(ref origins, errors, ref destinations, ref validationType);
			if (!valid && validationType == DBSValidationType.Origin)
				return GetInvalidRequestDBSResult(DBSMessageIdentifier.UserInvalidRequestLocationForOrigin, errors);
			else if (!valid && validationType == DBSValidationType.Destination)
				return GetInvalidRequestDBSResult(DBSMessageIdentifier.UserInvalidRequestLocationForDestination, errors);
			else if (!valid && validationType == DBSValidationType.Both  )
				return GetInvalidRequestDBSResult(DBSMessageIdentifier.UserInvalidRequestLocationForOriginAndDestination, errors);
			else if (!valid && validationType == DBSValidationType.Inconsistent )
				return GetInvalidRequestDBSResult(DBSMessageIdentifier.UserInvalidRequestLocationForInconsistentCodes, errors);

			
			// when valid, call appropriate Provider

			// decide which provider to call
			// if type is Bus/Coach or train result provider= CJP then call CJP
			DepartureBoardType depType = GetDepartureBoardType(origins[0]);
			if ( depType== DepartureBoardType.BusCoach || (depType == DepartureBoardType.Train &&  GetTrainResultFromCJP() ))
			{
				// Call StopEvent Manager
				IStopEventManager sem = (IStopEventManager) TDServiceDiscovery.Current[ServiceDiscoveryKey.StopEventManager];
				DBSResult res = sem.GetDepartureBoardTrip(origins[0], destinations[0], operatorCode, serviceNumber, time, rangeType, range, showDepartures, showCallingStops);

				bool success = (res.Messages == null || res.Messages.Length == 0);
				LogEvent(submitted, ExposedServicesCategory.DepartureBoardServiceStopEvent, success, token);

				return res;
				
			}
				// call RTTI Manager
			else
			{
				DBSResult res;
				IRDHandler rdh = (IRDHandler) TDServiceDiscovery.Current[ServiceDiscoveryKey.RTTIManager];

				// Check if it type last or first 
				if (time.Type == TimeRequestType.First || time.Type == TimeRequestType.Last
                    || time.Type == TimeRequestType.LastToday || time.Type == TimeRequestType.LastTomorrow
                    || time.Type == TimeRequestType.FirstToday || time.Type == TimeRequestType.FirstTomorrow)
				{
					// Handle first and last services differently
					res = HandleFirstAndLastServices(rdh , origins[0], destinations[0], operatorCode, serviceNumber, time, rangeType, range, showDepartures, showCallingStops);
				}
				else
				{
					// Call trip request method!
					res = rdh.GetDepartureBoardTrip(origins[0], destinations[0], operatorCode, serviceNumber, time, rangeType, range, showDepartures, showCallingStops);
				}
				
				bool success = (res.Messages == null || res.Messages.Length == 0);
				LogEvent(submitted, ExposedServicesCategory.DepartureBoardServiceRTTI, success, token);

                if (TDTraceSwitch.TraceVerbose)
                {
                    // Log the call result as xml
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("DBSResult: {0}", ConvertToXML(res))));
                }

				return res;
			}
		}

		/// <summary>
		/// Stop level request method for DepartureBoard Information
		/// </summary>
		/// <param name="token">Authentication token</param>
		/// <param name="stopNaptan">NaptanId of the stop</param>
		/// <param name="type">type of departure board requested</param>
		/// <param name="serviceNumber"> service number requested (optional)</param>
		/// <param name="showDepartures"> show departure times / arrival times</param>
		/// <param name="showCallingStops"> show calling stops if available</param>
		/// <returns>DBS Result object holding DepartureBoard info</returns>
		public DBSResult GetDepartureBoardStop(
			string token,
			DBSLocation stopLocation,
            string operatorCode,
            string serviceNumber,
            bool showDepartures,
			bool showCallingStops)
		{

			// init for logging
			DateTime submitted = DateTime.Now;

			// init for validation
			ArrayList errors = new ArrayList();
			bool valid = true;

			DBSLocation[] stops = new DBSLocation[]{stopLocation};
			valid = DBSValidation.ValidateLocationRequest(ref stops, errors);
			if (!valid)
			{
				return GetInvalidRequestDBSResult(DBSMessageIdentifier.UserInvalidRequestLocationForOrigin, errors);
			}
			
			// when valid, call appropriate Provider

			// decide which provider to call
			DepartureBoardType depType = GetDepartureBoardType(stops[0]);
			if ( depType == DepartureBoardType.BusCoach || ( depType == DepartureBoardType.Train && GetTrainResultFromCJP()) )
			{
				IStopEventManager sem = (IStopEventManager) TDServiceDiscovery.Current[ServiceDiscoveryKey.StopEventManager];

				DBSResult res = sem.GetDepartureBoardStop(stops[0], operatorCode, serviceNumber, showDepartures, showCallingStops);

				bool success = (res.Messages == null || res.Messages.Length == 0);
				LogEvent(submitted, ExposedServicesCategory.DepartureBoardServiceStopEvent, success, token);

				return res;
			}
			else
			{
				IRDHandler rdh = (IRDHandler) TDServiceDiscovery.Current[ServiceDiscoveryKey.RTTIManager];
				
				// Call stop request method!
                DBSResult res = rdh.GetDepartureBoardStop(stops[0], operatorCode, serviceNumber, showDepartures, showCallingStops);
				
				bool success = (res.Messages == null || res.Messages.Length == 0);
				LogEvent(submitted, ExposedServicesCategory.DepartureBoardServiceRTTI, success, token);

				return res;
			}
		}

		/// <summary>
		/// Use the dataService to return the Time Request Types to display in the UI
		/// </summary>
		/// <returns>an array of TimeRequestType objects sorted in the order to be displayed. The firsts one being the default one.</returns>
		public TimeRequestType[] GetTimeRequestTypesToDisplay()
		{
			DataServices.DataServices  ds = (DataServices.DataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
			ArrayList typeList = ds.GetList(DataServices.DataServiceType.MobileTimeRequestDrop);

			TimeRequestType[] resultList = new TimeRequestType[typeList.Count];
			try
			{
				for (int i =0; i< typeList.Count; i++)
				{
					resultList[i] = (TimeRequestType)Enum.Parse(typeof(TimeRequestType), ((DataServices.DSDropItem)typeList[i]).ItemValue, true);
				}
			}
			catch (SystemException)
			{
				return new TimeRequestType[0];
			}

			return resultList;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Methods that creates a new DBSResult with error messages only following an invalid request
		/// </summary>
		/// <param name="errors">ArrayList of string errors</param>
		/// <returns>a new DBSResult object holding messages.</returns>
		private DBSResult GetInvalidRequestDBSResult( DBSMessageIdentifier id, ArrayList errors)
		{
			DBSResult result = new DBSResult();
			
			DBSMessage message = new DBSMessage();
			message.Code = (int)id; // see Message codes spreadsheet for details of codes.

			StringBuilder sbErrors = new StringBuilder();
			foreach ( string error in errors)
			{
				sbErrors.Append(error);
			}
			message.Description = string.Format(Messages.RequestBadFormat, sbErrors.ToString());

			result.Messages = new DBSMessage[]{message};

			return result;
		}

		/// <summary>
		/// Method returning the type of DepartureBoard to access, according to a given location
		/// We assume that only one location is needed, as the consistency test was called beforehand 
		/// in DBSValidation
		/// </summary>
		/// <param name="stop">given stop to analyse</param>
		/// <returns>which DepartureBoardType to query.</returns>
		private DepartureBoardType GetDepartureBoardType(DBSLocation stop)
		{
			switch (stop.Type)
			{
				case TDCodeType.CRS:
					return DepartureBoardType.Train;
				case TDCodeType.SMS:
					return DepartureBoardType.BusCoach;
				case TDCodeType.NAPTAN:
					if (stop.Code.StartsWith("9100"))
						return DepartureBoardType.Train;
					else
						return DepartureBoardType.BusCoach;
				default:
					return DepartureBoardType.BusCoach;
			}
		}

		/// <summary>
		/// Private method used to log an ExposedServices Event of a specific category
		/// </summary>
		/// <param name="submitted">DateTime submitted</param>
		/// <param name="category">Category of ExposedServicesEvent</param>
		/// <param name="success">success of request</param>
		/// <param name="token">authentication/identification token</param>
		private void LogEvent(DateTime submitted, ExposedServicesCategory category, bool success, string token)
		{
			Logger.Write(new ExposedServicesEvent(token, submitted, category, success));
		}

		/// <summary>
		/// To check to provider for Train Info. If true then CJP else RTTI would be provider.
		/// </summary>
		/// <returns>Returns true if CJP is the provider else false</returns>
		private bool GetTrainResultFromCJP()
		{
			try
			{
				return  Convert.ToBoolean(Properties.Current[Keys.GetTrainInfoFromCJP].ToString())  ;				
			}
			catch(FormatException)
			{
				return false;
			} 
		}
        
		private DBSResult HandleFirstAndLastServices(IRDHandler rdh , DBSLocation originLocation, DBSLocation destinationLocation,
            string operatorCode, string serviceNumber, DepartureBoardFacade.DBSTimeRequest requestedTime, DBSRangeType rangeType, int range, 
			bool showDepartures, bool showCallingStops)
		{
			DBSResult res = new DBSResult();
			double firstServiceHour=0, lastServiceHour=0, currentFirstServiceHour=0, currentLastServiceHour=0 ;
			int firstServiceDuaration=0   ,lastServiceDuaration=0, firstServiceMinute=0, lastServiceMinute=0;
			
			try
			{
				// Time hour for first service request 		
				firstServiceHour = double.Parse(Properties.Current[Keys.RTTIFirstServiceHour].ToString())  ; 
				// Time hour for last service request 		
				lastServiceHour = double.Parse( Properties.Current[Keys.RTTILastServiceHour].ToString())  ; 
				//Time duration for first service request 		
				firstServiceDuaration = int.Parse(Properties.Current[Keys.RTTIFirstServiceDuration].ToString()) ; 
				//Time duration for last service request 		
				lastServiceDuaration = int.Parse(Properties.Current[Keys.RTTILastServiceDuartion].ToString()); 				


				if (requestedTime.Type == TimeRequestType.First ||
                    requestedTime.Type == TimeRequestType.FirstToday ||
                    requestedTime.Type == TimeRequestType.FirstTomorrow)
				{

                    int spanHours = 24;
                    //RB Updated to handle First and FirstToday the same.
                    if (requestedTime.Type == TimeRequestType.First || requestedTime.Type == TimeRequestType.FirstToday)
                    {
                        requestedTime.Hour = (int)firstServiceHour;
                        requestedTime.Minute = firstServiceMinute;

                         spanHours = 24 - requestedTime.Hour + 1;
                    }
                    else if (requestedTime.Type == TimeRequestType.FirstTomorrow)
                    {
                        //HandleFirstTommorrowService
                        spanHours = 24;
                        requestedTime.Hour = (int)firstServiceHour;
                        requestedTime.Minute = firstServiceMinute;
                    }
                    else
                    {
                        //HandleFirstService
                        requestedTime.Hour = (int)firstServiceHour;
                        requestedTime.Minute = firstServiceMinute;
                    }

					currentFirstServiceHour = firstServiceHour;
					TimeSpan firstServiceSpan = new TimeSpan((int)firstServiceHour,firstServiceMinute,0);
					TimeSpan currentServiceSpan = new TimeSpan((int)currentFirstServiceHour,firstServiceMinute,0);
					// Now repeat the request till response is found or firstServiceHour - currentFirstServiceHour <24

                    while (currentServiceSpan.Subtract(firstServiceSpan).TotalHours < spanHours)
                    {
					   requestedTime.Hour =  (int)currentFirstServiceHour;
					   requestedTime.Minute = firstServiceMinute; 					   
					   res = rdh.GetDepartureBoardTrip(originLocation,destinationLocation, operatorCode, serviceNumber, requestedTime, rangeType, range, showDepartures, showCallingStops);

					   if (res==null || res.StopEvents == null)
						   break;
					   
						// now check whether you have result back yet or not?
						if (res.StopEvents.Length > 0)
							return res;
						else
						{
							//currentServiceSpan = new TimeSpan( (int)currentFirstServiceHour,firstServiceDuaration,0); 
							currentServiceSpan = currentServiceSpan.Add(new TimeSpan(0,firstServiceDuaration,0));   
							currentFirstServiceHour =   currentServiceSpan.Hours;
						}
					}
					return res;
				  
				}
                else if (requestedTime.Type == TimeRequestType.Last ||
                    requestedTime.Type == TimeRequestType.LastToday ||
                    requestedTime.Type == TimeRequestType.LastTomorrow)
				{
					//HandleLastService
					//HandleFirstService();
                   requestedTime.Hour = (int)lastServiceHour;
                   requestedTime.Minute = lastServiceMinute;

                    currentLastServiceHour = lastServiceHour;
                    TimeSpan lastServiceSpan = new TimeSpan((int)lastServiceHour, lastServiceMinute, 0);
                    TimeSpan currentServiceSpan = new TimeSpan((int)currentLastServiceHour, lastServiceMinute, 0);
                    DateTime lastServiceTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, (int)lastServiceHour, lastServiceMinute, 0);
                    DateTime currentServiceTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, (int)currentLastServiceHour, lastServiceMinute, 0);

                    //RB Updated to treat Last and LastToday the same
                    if (requestedTime.Type == TimeRequestType.Last || requestedTime.Type == TimeRequestType.LastToday)
                    {
                        requestedTime.Hour = 0;    //start at begining of day
                        requestedTime.Minute = 01;

                        currentLastServiceHour = lastServiceHour;
                        lastServiceSpan = new TimeSpan((int)lastServiceHour, lastServiceMinute, 0);
                        currentServiceSpan = new TimeSpan((int)currentLastServiceHour, lastServiceMinute, 0);
                        lastServiceTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day , (int)lastServiceHour, lastServiceMinute, 0);
                        currentServiceTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day , (int)currentLastServiceHour, lastServiceMinute, 0);

                    }

                    if (requestedTime.Type == TimeRequestType.LastTomorrow)
                    {
                        requestedTime.Hour = 0;    //start at begining of day
                        requestedTime.Minute = 01;

                        currentLastServiceHour = lastServiceHour;
                        lastServiceSpan = new TimeSpan((int)lastServiceHour, lastServiceMinute, 0);
                        currentServiceSpan = new TimeSpan((int)currentLastServiceHour, lastServiceMinute, 0);
                        lastServiceTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1, (int)lastServiceHour, lastServiceMinute, 0);
                        currentServiceTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1, (int)currentLastServiceHour, lastServiceMinute, 0);
                               
                    }
					// Now repeat the request till response is found or 
					// lastServiceHour - currentLastServiceHour < 1 day	 or 
					// till day changes
                    
					while (lastServiceSpan.Subtract(currentServiceSpan).TotalDays < 1 || lastServiceTime.Day != currentServiceTime.Day)
					{
						requestedTime.Hour =  (int)currentLastServiceHour;
						requestedTime.Minute = lastServiceMinute; 					   
						res = rdh.GetDepartureBoardTrip(originLocation,destinationLocation, operatorCode, serviceNumber, requestedTime, rangeType, range, showDepartures, showCallingStops);

						if (res==null || res.StopEvents == null)
							break;
					   
						// now check whether you have result back yet or not?
						if (res.StopEvents.Length > 0)
							return res;
						else
						{							
							currentServiceSpan = currentServiceSpan.Subtract(new TimeSpan(0,lastServiceDuaration,0));   
							currentLastServiceHour =   currentServiceSpan.Hours;
							currentServiceTime.Subtract(currentServiceSpan);  
						}
					}
					
					return res;
				}
				else
				{
					BuildMessage(ref res, Messages.RTTIUnableToExtractFirstOrLastServiceData.ToString());  
					return res;
				}
			}
			catch
			{
				BuildMessage(ref res, Messages.RTTIUnableToExtractFirstOrLastServiceData.ToString());  
				return res;
			}
		}

		/// <summary>
		/// Build error message and create and assignes error to DBSresult object.
		/// </summary>
		/// <param name="dbsResult">DBSResult object</param>
		/// <param name="errorMessageType">Error Message Type</param>
		private void BuildMessage(ref DBSResult dbsResult, string  errorMessageType)
		{
			//DBSResult dbsResult = new DBSResult();  
			try
			{
				// Build Error Message
				//BuildMessage()
				DBSMessage dbMessage = new DBSMessage(); 
				RTTIUtilities.BuildRTTIError(errorMessageType, dbsResult);   
			}
			catch(NullReferenceException nEx)
			{
				// log the error 
				string errMessage = "Error occured in DepartureBoardService.BuildMessage " + " error:  " + nEx.Message ;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
				
			}		
		}

        /// <summary>
        /// Create an XML representtaion of the specified object
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="requestId"></param>
        /// <returns>XML string, prefixed by request id</returns>
        private string ConvertToXML(object obj)
        {
            // Placing in a try catch because we're not confident all objects can be XmlSerialized,
            try
            {
                if (obj != null)
                {
                    XmlSerializer xmls = new XmlSerializer(obj.GetType());
                    StringWriter sw = new StringWriter();
                    xmls.Serialize(sw, obj);
                    sw.Close();

                    return sw.ToString();
                }
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
                    string.Format("Error during XmlSerialize for object: {0} {1} {2} {3}", ex.Message, ex.StackTrace,
                    (ex.InnerException != null) ? ex.InnerException.Message : string.Empty,
                    (ex.InnerException != null) ? ex.InnerException.StackTrace : string.Empty)));
            }

            return string.Empty;
        }

        #endregion
	}
}
