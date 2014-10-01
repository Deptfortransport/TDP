// *********************************************** 
// NAME                 : RDHandler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 31/12/2004
// DESCRIPTION  : This class is mainly responsible for handling data from RDServiceConsumer class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RDHandler.cs-arc  $ 
//
//   Rev 1.2   Feb 17 2010 16:42:24   RHopkins
//Pass OperatorCode in StopEvent request
//Resolution for 5403: Pass OperatorCode in StopEvents request
//
//   Rev 1.1   Mar 12 2008 12:02:38   build
//Manually resolve merge probelms with stream 4542 merge.
//
//   Rev 1.0.1.1   Mar 11 2008 16:41:50   rbroddle
//Changes for CCN431 Mobile Service Enhancements
//
//   Rev 1.0.1.0   Feb 18 2008 16:05:20   pscott
//IR 5497 - Add new functioinality for FirstToday,FirstTomorrow,LastToday and Last Tomorrow
//
//   Rev 1.0   Nov 08 2007 12:21:38   mturner
//Initial revision.
//
//   Rev 1.8   Jul 29 2005 18:35:12   Schand
//Fix for IR2628, IR2629. 
//
//Changes for HandleFirstAndLast Services() function which gets the first and last services iteratively. 
//Resolution for 2628: First Services are not working betweenWAT & FLE. Also unable to find first train (from WTN)
//Resolution for 2629: Unable to find last Train when filtered by arrival station
//
//   Rev 1.7   Jul 25 2005 18:12:36   Schand
//Fix for IR2617. The current Year, Month and day has been applied to last service request. 
//Resolution for 2506: Mobile Web - "More>" gives erroneous results
//Resolution for 2617: No Arrival/Dep Times returned for LAST train service search using "From" field
//
//   Rev 1.6   Jul 15 2005 15:50:10   PNorell
//Added to only log when logging when turned on.
//
//   Rev 1.5   Jul 15 2005 15:01:22   jmcallister
//RTTI Test Feed Fix
//
//   Rev 1.4   Mar 16 2005 16:55:38   schand
//Fix bug- When last option is selected, it will only last even if it has been changed to other option like today, now or first.
//
//   Rev 1.3   Mar 02 2005 14:39:12   schand
//Code Review Fix (IR-1928)
//
//   Rev 1.2   Mar 01 2005 18:55:50   schand
//Code Review Fix (IR-1928)
//
//   Rev 1.1   Feb 28 2005 17:07:10   passuied
//fixed 29 of feb issue !!!!
//
//   Rev 1.0   Feb 28 2005 16:23:04   passuied
//Initial revision.
//
//   Rev 1.15   Feb 16 2005 14:54:14   passuied
//Change in interface and behaviour of time Request
//
//possibility to plan in the past within configurable time window
//
//   Rev 1.14   Feb 11 2005 17:46:34   schand
//Removed warning xEx
//
//   Rev 1.13   Feb 11 2005 11:35:58   schand
//IRDHandler's public interface has been changed.
//i.e. returning DBSResult object instead of boolean.
//UpdateRequested counter is demoted to private 
//
//   Rev 1.12   Feb 02 2005 15:33:08   schand
//applied FxCop rules
//
//   Rev 1.11   Jan 28 2005 17:45:08   schand
//Added Dispose method to stop Mock listener
//
//   Rev 1.10   Jan 28 2005 11:37:56   schand
//Calculating requested stop for both train and trip
//
//   Rev 1.9   Jan 21 2005 17:40:18   schand
//Bulding DBSmessage before returning false due to bull location
//
//   Rev 1.8   Jan 21 2005 14:22:36   schand
//Code clean-up and comments has been added
//
//   Rev 1.7   Jan 19 2005 16:28:40   schand
//integration of RTTI + SE manager!
//
//   Rev 1.6   Jan 19 2005 14:36:02   schand
//HardCoded values changed to properties key.
//Handled duration for Trip
//
//   Rev 1.5   Jan 18 2005 19:59:14   schand
//NUnit code is working for all methods

using System;
using System.Xml ;
using System.Collections; 
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging ;  
using TransportDirect.Common.PropertyService.Properties;    
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using DBWS = TransportDirect.UserPortal.DepartureBoardService.DepartureBoardWebService;
using Logger = System.Diagnostics.Trace;
 

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	
	/// <summary>
	/// Summary description for RDHandler.
	/// This class is mainly responsible for handling data from RDServiceConsumer class.
	/// It will populate the trainStopEvent with information requested
	/// It will format and translate the code to plain English 
	/// It uses TDAdditionalData.LookupStationNameForNaptan() method to do the transaltion from Naptan to station name.
	/// </summary>
	[Serializable()]
	public class RDHandler : IRDHandler
    {
        #region Private variables

        private int messageId = 0;		
		RTTIXmlValidator rttiXmlValidator = null;
		RTTIXmlExtractor rttiXmlExtractor = null;
		string requestedNaptanId = string.Empty;  		
		double firstServiceHour ;  // config 
		double lastServiceHour ; // config 
		int fisrtServiceDuaration ; // config 
		int lastServiceDuaration ;  // config 
		private int iMaxDuration ; // config 
		private int iDefaultResultRange; // config 	
		private string sSchemaLocation = string.Empty ; // config
		private string sNameSpace = string.Empty ; // config		
		private int iDuration = 0; 
		private int iReturnResultCount = -1; 
		private TimeRequestType firstOrLastRequest = TimeRequestType.TimeToday ; // default value   

        #endregion

        #region Constructor

        /// <summary>
		/// Class contructor which also initialises some class variables
		/// </summary>
		public RDHandler()
		{
			if (! Initialise())
			{
				throw new Exception("Unable to create RDHandler class"); 
			}
		}
        
        #endregion
        
        #region Public Members
        
        /// <summary>
		/// Station level request method for DepartureBoard information
		/// </summary>
		/// <param name="stopLocation">DBSlocation type which contains a CRS and collection of Naptan Id for station</param>
		/// <param name="serviceNumber"> RID for train. This is an optional parameter. If value is provided for this param then get the infor for train</param>
		/// <param name="showDepartures"> boolean flag to indicate whether to show departure or arrival. Bydefault is false i.e. show arrival</param>		
		/// <param name="showCallingStops"> boolean flag to indicate whether to show calling points or not</param>				
		/// <returns>DBSResult which consists of Data or Messages </returns>
		public DepartureBoardFacade.DBSResult GetDepartureBoardStop(
            DBSLocation stopLocation, string operatorCode, string serviceNumber, bool showDepartures, 
			bool showCallingStops )			
		{	
			string xmlResponse = string.Empty;
			DateTime reqDateTime = DateTime.MinValue;
			bool bTrainResponse = false;
			TemplateRequestType reqType ;
			
			// creating the instance of DBSResult
			DBSResult trainDbsResult = new DBSResult(); 

			try
			{	// initialise it to blank string 
				xmlResponse = string.Empty;
				
				
				// Check stopLocation
				if (stopLocation == null)
				{	// build error message for null location request
					BuildMessage(ref trainDbsResult, Messages.NullLocationRequest.ToString());  
					return trainDbsResult;
				}
				
				// get the datetime 
				reqDateTime = GetDateTime();
				
				// now check if service number has been provided or not ?
				// call diffrent method
				if (serviceNumber !=null && serviceNumber.Length  != 0)
				{	
					bTrainResponse = true;
					// Getting the RTTI xml response
					xmlResponse = GetXMlResponse(TemplateRequestType.TrainRequestByRID, stopLocation, stopLocation, serviceNumber, reqDateTime, -1,  -1, showDepartures);					
				}
				else
				{
					// Getting the RTTI xml response
					xmlResponse = GetXMlResponse(TemplateRequestType.StationRequestByCRS, stopLocation, stopLocation, serviceNumber, reqDateTime, -1, -1 ,showDepartures);					
				}

				// check xml resposne
				if (xmlResponse == null || xmlResponse.Length  ==0 ) 
				{		RTTIUtilities.BuildRTTIError(Messages.RTTIResponseInValid, trainDbsResult);    
						return trainDbsResult;
				}
				
				// Validate xml response 
				if (!ValidateXmlResponse(xmlResponse, trainDbsResult))
					return trainDbsResult;				
					
				// check whether service or trip data
				if (bTrainResponse)
					reqType = TemplateRequestType.TrainRequestByRID ;				
				else
					reqType = TemplateRequestType.StationRequestByCRS ;  									

				// Get requested NaptanId()
				requestedNaptanId = GetRequestedNaptanId(bTrainResponse, showDepartures, stopLocation, stopLocation) ;

				// extract the data from xml response 
				if (! ExtractDataFromXml(reqType, xmlResponse , showDepartures, showCallingStops, requestedNaptanId,   trainDbsResult))
				{
					BuildMessage(ref trainDbsResult, Messages.RTTIUnableToExtract.ToString() );  
					return trainDbsResult;
				}		
				
				return trainDbsResult;
			}
			catch(Exception ex)
			{	
				// log the error 
				string errMessage = "Error occured in GetDepartureBoardStop " + " error:  " + ex.Message ;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
				// build the error message 
				BuildMessage(ref trainDbsResult, Messages.RTTIUnknownError.ToString() )  ;   
				return trainDbsResult;
			}
			
		}
        
		/// <summary>
		/// Station level request method for DepartureBoard information
		/// </summary>
		/// <param name="originLocation">DBSlocation type which contains a CRS and collection of Naptan Id for station</param>
		/// <param name="destinationLocation">DBSlocation type which contains a CRS and collection of Naptan Id for station</param>
		/// <param name="serviceNumber"> RID for train. This is an optional parameter. If value is provided for this param then get the infor for train</param>
		/// <param name="requestedTime"> DBSTimeRequest contains Hour, Minute & TimeRequestType. </param>
		/// <param name="rangeType"> Sequence or Interval </param>
		/// <param name="range"> number  of response requested or response for an interval </param>
		/// <param name="showDepartures"> boolean flag to indicate whether to show departure or arrival. Bydefault is false i.e. show arrival</param>				
		/// <param name="showCallingStops"> boolean flag to indicate whether to show calling points or not</param>				
		/// <returns>DBSResult which consists of Data or Messages </returns>	
		public DepartureBoardFacade.DBSResult GetDepartureBoardTrip(
							  DBSLocation originLocation, DBSLocation destinationLocation,
                              string operatorCode, string serviceNumber, DepartureBoardFacade.DBSTimeRequest requestedTime, DBSRangeType rangeType, int range, 
							  bool showDepartures, bool showCallingStops)
		{	
			bool bTrainResponse = false;
			string xmlResponse = string.Empty;
			DateTime reqDateTime = DateTime.MinValue ;
			TemplateRequestType reqType ;
			string requestedNaptanId = string.Empty ;
			
			// creating the instance of DBSResult
			DBSResult trainDbsResult = new DBSResult(); 

			try
			{	// initialise it to blank string 
				xmlResponse = string.Empty;

				// initialiasing duration 
				iDuration = -1; ;				
		

				// initiliase max result 
				iReturnResultCount = iDefaultResultRange;
				
				// Check originLocation
				//if (originLocation == null || destinationLocation==null)

				if (originLocation == null)
				{	// build error message for null location request
					BuildMessage(ref trainDbsResult, Messages.NullLocationRequest.ToString());  
					return trainDbsResult;
				}
				
				// now set hour and minute if its first or last service
                if (requestedTime.Type == TimeRequestType.First ||
                    requestedTime.Type == TimeRequestType.FirstToday ||
                    requestedTime.Type == TimeRequestType.FirstTomorrow||
                    requestedTime.Type == TimeRequestType.Last ||
                    requestedTime.Type == TimeRequestType.LastToday ||
                    requestedTime.Type == TimeRequestType.LastTomorrow)
				{
					firstServiceHour = requestedTime.Hour;
					lastServiceHour =  requestedTime.Hour;
				}
				// get the datetime 
				reqDateTime = GetDateTime(requestedTime);


				//DBSRangeType.Interval - time window
				//DBSRangeType.Sequence - no of result 
				if (rangeType == DBSRangeType.Interval )
				{	// check if range is greater than maximum duration allowed					
					if (range > iMaxDuration) iDuration = range  ;
				}
				else
				{
					// if it is sequence then get the number of result to store
					if (range>0)iReturnResultCount = range;
				}
				
				// now check if service number has been provided or not ?
				// call diffrent method
				if (serviceNumber !=null && serviceNumber.Length != 0)
				{	bTrainResponse = true;
					// Getting the RTTI xml response
					xmlResponse = GetXMlResponse(TemplateRequestType.TrainRequestByRID, originLocation, destinationLocation, serviceNumber, reqDateTime, iReturnResultCount, iDuration, showDepartures);
					if( TDTraceSwitch.TraceInfo )
					{
						Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty , TDTraceLevel.Info , "RTTI Response XML Service number provided: " + xmlResponse)) ;
					}
				}

				else
				{	
					// Getting the RTTI xml response
					xmlResponse = GetXMlResponse(TemplateRequestType.TripRequestByCRS  ,originLocation, destinationLocation,serviceNumber ,  reqDateTime, iReturnResultCount, iDuration , showDepartures);					
					if( TDTraceSwitch.TraceInfo )
					{
						Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty , TDTraceLevel.Info , "RTTI Response XML Service number NOT provided: " + xmlResponse)) ;
					}
				}

				//Check response 
				if (xmlResponse ==null ||  xmlResponse.Length  ==0) 				{	
					RTTIUtilities.BuildRTTIError(Messages.RTTIResponseInValid,  trainDbsResult); 	
					return trainDbsResult;				
				}
				
				// Validate xml response 
				if (!ValidateXmlResponse(xmlResponse, trainDbsResult))
					return trainDbsResult;
				
				// check whether service or trip data
				if (bTrainResponse)
					reqType = TemplateRequestType.TrainRequestByRID ;				
				else
					reqType = TemplateRequestType.TripRequestByCRS ;  
				
				// Get requested NaptanId()
				requestedNaptanId = GetRequestedNaptanId(bTrainResponse, showDepartures, originLocation, destinationLocation) ;

				// extract the data from xml response 
				if (! ExtractDataFromXml(reqType, xmlResponse, showDepartures, showCallingStops, requestedNaptanId, trainDbsResult))
				{
					BuildMessage(ref trainDbsResult, Messages.RTTIUnableToExtract.ToString());  
					return trainDbsResult;
				}

				return trainDbsResult;
			}
			catch(Exception ex)
			{	
				// log the error 
				string errMessage = "Error occured in GetDepartureBoardStop " + " error:  " + ex.Message ;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
				// build the error message 
				BuildMessage(ref trainDbsResult, Messages.RTTIUnknownError.ToString())  ;   
				return trainDbsResult;
			}
			
		}
        
		#endregion
        
		#region Private Members
		
		/// <summary>
		/// Initialise class variables from DB.
		/// </summary>
		/// <returns>Returns true if initialisation succeed else false would be returned.</returns>
		private bool InitiliaseConfigParams()
		{
			try
			{
				// config params 
				// following variables needs to come from config file
				// RTTI Schema location 
				sSchemaLocation = Properties.Current[Keys.RTTISchemaLocation];  
				//RTTI Schema NameSpace
				sNameSpace =  Properties.Current[Keys.RTTISchemaNameSpace];   
				
				// Time hour for first service request 		
				firstServiceHour = double.Parse(Properties.Current[Keys.RTTIFirstServiceHour].ToString())  ; 
				// Time hour for last service request 		
				lastServiceHour = double.Parse( Properties.Current[Keys.RTTILastServiceHour].ToString())  ; 
				//Time duration for first service request 		
				fisrtServiceDuaration = int.Parse(Properties.Current[Keys.RTTIFirstServiceDuration].ToString()) ; 
				//Time duration for last service request 		
				lastServiceDuaration = int.Parse(Properties.Current[Keys.RTTILastServiceDuartion].ToString()); 				
				//Maximum Time duration defined by RTTI 
				iMaxDuration = int.Parse(Properties.Current[Keys.RTTIMaxDurationAllowed].ToString()); 
				
				//Default number of result result 
				iDefaultResultRange = int.Parse(Properties.Current[Keys.DefaultRangeKey].ToString()); 
				
				// temporary 
				//if (iDefaultResultRange < 0) iDefaultResultRange = 50;

				return true;
			}
			catch
			{	
				// log the error 
				string errMessage = "Error occured in InitiliaseConfigParams, unable to get the value from properties.current " ;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;					
				return false;
			}
		}

		
		/// <summary>
		/// Initialise class variables and create instance of RDServiceConsumer, RTTIValidator and RTTIExtractor
		/// </summary>
		/// <returns>Returns true if initialisation succeed else false would be returned.</returns>
		private bool Initialise()
		{
			try
			{
				//Initialise all params
				if (!InitiliaseConfigParams())
					return false;				

				// if schema or Namespace is null then log the error and return false .. 
				if (sSchemaLocation== null || sSchemaLocation == null)
				{
					// log the error 
					string errMessage = "Error occured in Initialise, unable to get the value for schema or namepace " ;  
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
					// return false
					return false;				
				}
				
				// creating the instance of xml validator
				rttiXmlValidator = new RTTIXmlValidator(sSchemaLocation, sNameSpace); 
				
				// creating the instance of xml extractor
				rttiXmlExtractor = new RTTIXmlExtractor();
				return true;
			}
			catch(Exception ex)
			{	
				// log the error 
				string errMessage = "Error occured in Initialise " + " error:  " + ex.Message ;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
				
				throw ex ;
			}
		}
		
		
		/// <summary>
		/// Calculates the requested NaptanId from origin and destination.
		/// If destination == null then requested Naptan = origin
		/// </summary>
		/// <param name="trainResponse">boolean flag to indicate whether its for train response</param>
		/// <param name="showDepartures">boolean flag to indicate departure or arrival</param>
		/// <param name="originLocation">Origin</param>
		/// <param name="destinationLocation">Destination</param>
		/// <returns><Resturns Naptan Id of the requested station </returns>
		private string GetRequestedNaptanId(bool trainResponse, bool showDepartures , DBSLocation originLocation, DBSLocation destinationLocation)
		{	string requestedNaptanId = string.Empty ;
			DBSLocation requestedLocation;
			try
			{
				// check if destination is null	or showdeparture == true
				if (showDepartures || destinationLocation == null)
					requestedLocation = originLocation; 					
				else
					requestedLocation = destinationLocation; 
				
				// get requested NaptanID
				if (requestedLocation.NaptanIds.Length == 0 || requestedLocation.NaptanIds[0].ToString() == null )
					requestedNaptanId = string.Empty ;					
				else
					requestedNaptanId = requestedLocation.NaptanIds[0].ToString();	

				return requestedNaptanId;
			}
			catch(NullReferenceException)
			{
				return string.Empty ;
			}
		}

		
		/// <summary>
		/// Gets xml response string from the specified template request
		/// </summary>
		/// <param name="reqType">TemplateRequestType type</param>
		/// <param name="originStation">Origin DBSLocation </param>
		/// <param name="destinationStation">Destination DBSLocation</param>
		/// <param name="serviceNumber">Train RID </param>
		/// <param name="requestedDatetime">Requested DateTime</param>
		/// <param name="duration">Time window</param>
		/// <param name="showDepartures">Boolean flag to indicate departure or arrival</param>
		/// <returns>Xml response string</returns>
        private string GetXMlResponse(TemplateRequestType reqType, DBSLocation originStation, DBSLocation destinationStation, 
            string serviceNumber, DateTime requestedDatetime, int numberOfRows, int duration, bool showDepartures)
        {
            string xmlResponse = string.Empty;
            string destinationCode = string.Empty;

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "DepartureBoardService - Web service call to DepartureBoardWebService started."));
            }

            try
            {
                // Use the DepartureBoardWebService to get the xml response using the NRE EnquiryPorts sockets service.
                // The DBWS sockets call will return xml only, this RDHandler will be responsible for extracting 
                // into a result object
                using (DBWS.DepartureBoards webServiceDepartureBoards = new DBWS.DepartureBoards())
                {
                    // Webservice override url from properties
                    webServiceDepartureBoards.Url = Properties.Current["DepartureBoardService.DepartureBoardWebService.URL"];
                    webServiceDepartureBoards.Timeout = Convert.ToInt32(TransportDirect.Common.PropertyService.Properties.Properties.Current["DepartureBoardService.DepartureBoardWebService.TimeoutMillisecs"]);

                    #region Call web service

                    DBWSConverter dbwsConverter = new DBWSConverter();

                    // Build a request for the web service.
                    // DBWSRequest requires a location for the station board, and optionally a filter location 
                    // to filter services (going to or coming from).
                    // As the RDHandler uses the legacy NRE EnquiryPorts call, this supplies an origin and destination 
                    // location and uses the showDepartures flag to to determine which is the main and filter locations,
                    // and whether its departures/arrivals.
                    // Therefore the showArrivals isn't used but is set anyway, the DBWS for NRE EnquiryPorts call 
                    // will determine departure/arrivals board and will set the main and filter locations correctly. 
                    // Note: See LDBHandler GetDepartureBoardResult for how it handles it's call to the DBWS for NRE LDB web service
                    DBWS.DBWSRequest request = dbwsConverter.BuildDBWSRequest(
                        GetMessageId().ToString(),
                        originStation,
                        destinationStation,
                        showDepartures,
                        !showDepartures,
                        numberOfRows,
                        requestedDatetime,
                        duration,
                        serviceNumber);

                    switch (reqType)
                    {
                        case TemplateRequestType.StationRequestByCRS:
                            Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Verbose,
                                "Calling GetStationBoardSocketXml, Origin Station Code = " + originStation.Code.ToString()));
                            
                            xmlResponse = webServiceDepartureBoards.GetStationBoardSocketXml(request);
                            break;
                        case TemplateRequestType.TripRequestByCRS:
                            if (destinationStation != null && destinationStation.Code.Length != 0)
                                destinationCode = destinationStation.Code;

                            Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Verbose,
                                "Calling GetStationBoardSocketXml, Origin Code = " + originStation.Code.ToString() +
                                "DestinationCode = " + destinationCode +
                                "Requested DateTime = " + requestedDatetime.ToLongTimeString() +
                                " Duration = " + Convert.ToString(duration) +
                                "showDepartures = " + showDepartures.ToString()));

                            xmlResponse = webServiceDepartureBoards.GetStationBoardSocketXml(request);
                            break;
                        case TemplateRequestType.TrainRequestByRID:
                            Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Verbose,
                                "Calling GetServiceDetailSocketXml, serviceNumber = " + serviceNumber +
                               "Requested DateTime = " + requestedDatetime.ToLongTimeString()));

                            xmlResponse = webServiceDepartureBoards.GetServiceDetailSocketXml(request);
                            break;
                        default:
                            xmlResponse = string.Empty;
                            break;
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                // log the error 
                string errMessage = "Error occured in GetXMlResponse " + " error:  " + ex.Message;
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage));

                xmlResponse = string.Empty;
            }

            if (TDTraceSwitch.TraceVerbose)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    "DepartureBoardService - Web service call to DepartureBoardWebService completed."));
            }

            return xmlResponse;
        }
        
		/// <summary>
		/// Validates the given xml response string. In case of error, it will create and assignes error to DBSresult object.
		/// </summary>
		/// <param name="xmlResponse">Xml response string.</param>
		/// <param name="trainDbsResult">DBSResult object</param>
		/// <returns></returns>
		private bool ValidateXmlResponse(string xmlResponse , DBSResult trainDbsResult)
		{
			try
			{	
				// Check Xml Response
				if (xmlResponse==null || xmlResponse.Length  == 0)
				{	
					BuildMessage(ref trainDbsResult, Messages.RTTIResponseInValid );  					
					return false;
				}

				// Validate the xmlResponse
				if (!ValidateRTTISchema(xmlResponse))
				{
					BuildMessage(ref trainDbsResult, Messages.RTTISchemaValidationFailed);  
					// log the error 
					string errMessage = "ValidateXmlResponse: validation fails for " + xmlResponse ;  
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, errMessage)) ;
					return false;
				}

				return true;
			}
			catch(Exception ex)
			{	// Build the Message
				BuildMessage(ref trainDbsResult, Messages.RTTIGeneralError);
				// log the error 
				string errMessage = "Error occured in ValidateXmlResponse " + ex.Message ;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
				// return false
				return false;
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
				string errMessage = "Error occured in BuildMessage " + " error:  " + nEx.Message ;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
				
			}		
		}
		
		
		/// <summary>
		/// Returns current DateTime
		/// </summary>
		/// <returns>Returns current DateTime</returns>
		private DateTime GetDateTime()
		{
			return DateTime.Now; 
		}
		
		
		/// <summary>
		/// Calculates Datetime for RTTI request
		/// </summary>
		/// <param name="dbsTimeRequest">DBSTimeRequest object</param>
		/// <returns>Datetime object</returns>
		private DateTime GetDateTime(DBSTimeRequest dbsTimeRequest)
		{	DateTime datetime = DateTime.MinValue  ;
			int iDays = 1;  
			try
			{
				switch(dbsTimeRequest.Type)
				{	
					case TimeRequestType.Now:
						datetime = DateTime.Now;
						firstOrLastRequest = TimeRequestType.Now ;
						break;
					case TimeRequestType.TimeToday:
						datetime = DateTime.Today;
						datetime = datetime.AddHours(dbsTimeRequest.Hour);
						datetime = datetime.AddMinutes(dbsTimeRequest.Minute);
						firstOrLastRequest = TimeRequestType.TimeToday ;
						break;
					case TimeRequestType.TimeTomorrow:
						datetime = DateTime.Today.AddDays(1);
						datetime = datetime.AddHours(dbsTimeRequest.Hour);
						datetime = datetime.AddMinutes(dbsTimeRequest.Minute);
						firstOrLastRequest = TimeRequestType.TimeTomorrow ;
						break;

					case TimeRequestType.First:
                    case TimeRequestType.FirstToday:
                    case TimeRequestType.FirstTomorrow:

                        datetime = DateTime.Today;
						
						// setting duration 
						iDuration = fisrtServiceDuaration;	 
							
						// indicate its first or last service
						firstOrLastRequest = TimeRequestType.First ; 						
						
						//if (datetime.Hour > (int)firstServiceHour )iDays = 1;							
						if (DateTime.Now.Hour > (int)firstServiceHour)iDays = 1;							
						else iDays = 0 ;

                        if (dbsTimeRequest.Type== TimeRequestType.FirstToday) iDays = 0;
                        if (dbsTimeRequest.Type == TimeRequestType.FirstTomorrow) iDays = 1;

						// now set the sequence duration  to defined duration for 4 hrs and it should starts from 04:00 
						// setting time first 
						datetime = datetime.Add(new TimeSpan(iDays, (int)firstServiceHour,0,0));																		
						break;
                    
                    
                    case TimeRequestType.Last:
                    case TimeRequestType.LastToday:

                        datetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, (int)lastServiceHour, 0, 0, 0);
						// setting duration 
						iDuration = lastServiceDuaration; 

						// indicate its first or last service
						firstOrLastRequest = TimeRequestType.Last ; 
						break;
                    case TimeRequestType.LastTomorrow:

                        datetime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+1, (int)lastServiceHour, 0, 0, 0);
                        // setting duration 
                        iDuration = lastServiceDuaration;

                        // indicate its first or last service
                        firstOrLastRequest = TimeRequestType.Last;
                        break;

                    default:
						datetime = DateTime.MinValue  ;
						break;
				}
                this.rttiXmlExtractor.DateIndicator = datetime;
				return datetime ;
			}
			catch(NullReferenceException)
			{
				return datetime; 
			}
			
		}


		/// <summary>
		/// Extracts data from the specified xml response string and fills up DBSResult object.
		/// </summary>
		/// <param name="reqtype">TemplateRequestType type </param>
		/// <param name="xmlResponse">Xml response string </param>
		/// <param name="showDepartures">Boolean flag to indicate departure or arrival</param>
		/// <param name="showCallingStops">Boolean flag to show calling points or not?</param>
		/// <param name="requestedNaptanID">requested Naptan Id.</param>
		/// <param name="dbsResult">DBSResult object</param>
		/// <returns>Returns true if able t extract data or error message else false would be returned. </returns>
		private bool ExtractDataFromXml(TemplateRequestType reqtype, string xmlResponse, bool showDepartures , bool showCallingStops , string requestedNaptanID,  DBSResult dbsResult)
		{
			try
			{	
				rttiXmlExtractor.ShowDeparture = showDepartures;
				rttiXmlExtractor.RequestedStop = requestedNaptanID; 					
				
				switch(reqtype)
				{
					case TemplateRequestType.TrainRequestByRID:								
						rttiXmlExtractor.ShowCallingStop = showCallingStops;
					break;
					case TemplateRequestType.TripRequestByCRS:
						if (iReturnResultCount > 0 )
							rttiXmlExtractor.NoOfResult = iReturnResultCount; 						
						// check if first or last service has been requested?
						if (firstOrLastRequest == TimeRequestType.First || firstOrLastRequest == TimeRequestType.Last )
							rttiXmlExtractor.FirstOrLastService = firstOrLastRequest ;								
						break;						
					default:						
						break;
				}

				if (!rttiXmlExtractor.ExtractData(reqtype,  xmlResponse, dbsResult))
					return false;				

				return true;

			}
			catch(XmlException)
			{
				return false;
			}
			
		}


		/// <summary>
		/// Validates the given xml response string.
		/// </summary>
		/// <param name="xmlResponse">xml response string</param>
		/// <returns>Returns true if xml string is valid</returns>
		private bool ValidateRTTISchema(string xmlResponse)
		{		  
			try
			{
				return rttiXmlValidator.IsValid(xmlResponse);  

			}
			catch(XmlException)
			{
				return false;
			}
		}

		
		/// <summary>
		/// REturns message Id
		/// </summary>
		/// <returns>REturns message Id</returns>
		private int GetMessageId()
		{
			messageId += 1; 
			return messageId ;
		}
		
	
		
		public void Dispose()
		{
		}
		
		#endregion
	}
}
