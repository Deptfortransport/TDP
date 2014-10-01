// *********************************************** 
// NAME                 : DepartureBoardAssembler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/12/2005 
// DESCRIPTION  		: Departure Board Assembler class for converting domain types to exposed types.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/DepartureBoard/V1/DepartureBoardAssembler.cs-arc  $ 
//
//   Rev 1.2   Mar 28 2013 15:56:42   rbroddle
//Updated to correct op code translation for EES
//Resolution for 5906: MDV Issue - Stop Events Not Being Displayed in MDV Regions Except London
//
//   Rev 1.1   Jun 15 2010 12:48:46   apatel
//Updated to add new  "Cancelled" attribute to the DBSEvent object
//Resolution for 5554: Departure Board service detail page cancelled train issue
//
//   Rev 1.0   Nov 08 2007 12:22:18   mturner
//Initial revision.
//
//   Rev 1.3   May 10 2007 09:03:14   mturner
//Allowed the 'NaPTANID' string array to be empty when 'CodeType' is NAPTAN.  Should this occur in a request the contents of 'Code' are copied into element 0 of the NaPTANID array. This is at the request of Kizoom.  
//
//   Rev 1.2   May 08 2007 11:02:02   mturner
//Updated to lookup a NaPTANs locality if no locality is providewd for a request with a code type of NaPTAN.
//
//   Rev 1.1   Mar 27 2006 12:53:18   mtillett
//Fix assignment of DepartureTime to DataTransfer type (unit test updated).
//Resolution for 3648: Kizoom mobile - No actual times shown for departures
//
//   Rev 1.0   Jan 27 2006 16:30:44   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.6   Jan 20 2006 16:24:16   schand
//Added more comments and Serialization attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.5   Jan 18 2006 19:42:08   schand
//Added null condition
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.4   Jan 17 2006 14:54:06   schand
//Corrected method name
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.3   Jan 13 2006 15:23:14   schand
//Correct method name to start with Create instead of Get
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 12 2006 20:00:58   schand
//Added more method for translation
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.1   Dec 22 2005 11:32:40   asinclair
//Check in of Sanjeev's Work In Progress code
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.0   Dec 14 2005 15:38:30   schand
//Initial revision.
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements



using System; 
using TransportDirect.EnhancedExposedServices;
using TransportDirect.UserPortal.DepartureBoardService;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;

  

namespace TransportDirect.EnhancedExposedServices.DataTransfer.DepartureBoard.V1
{
	/// <summary>
	/// Departure Board Assembler class for converting domain types to exposed types.
	/// </summary>
	public sealed class DepartureBoardAssembler
	{
		#region Public Methods
		/// <summary>
		/// This method to translate DepartureBoardServiceRequest dto to its internal request  
		/// </summary>
		/// <param name="dto">DepartureBoardServiceRequest as DTO object</param>
		/// <returns>DepartureBoardServiceInternalRequest object</returns>
		public static DepartureBoardServiceInternalRequest CreateDepartureBoardServiceInternalRequest(DepartureBoardServiceRequest dto)
		{
			if (dto==null)
				return null;

			DepartureBoardServiceInternalRequest request = new DepartureBoardServiceInternalRequest();
			request.OriginLocation = CreateDBSLocation(dto.OriginLocation);
			request.DestinationLocation = CreateDBSLocation(dto.DestinationLocation);
			request.ServiceNumber = dto.ServiceNumber;
			request.JourneyTimeInformation = CreateDBSTimeRequest(dto.JourneyTimeInformation);  
			request.RangeType = CreateDBSRangeType(dto.RangeType);  
			request.Range = dto.Range; 
			request.ShowDepartures = dto.ShowDepartures; 
			request.ShowCallingStops = dto.ShowCallingStops;   			
			return request;
		}

		/// <summary>
		/// Static assembler method to convert an array of DBSStopEvent (domain object) to an array DepartureBoardServiceStopInformation (DTO)
		/// </summary>
		/// <param name="domainObject">DBSStopEvent array as domain object</param>
		/// <returns>DepartureBoardServiceStopInformation array as dto object</returns>
		public static DepartureBoardServiceStopInformation[] CreateDepartureBoardServiceStopInformationArrayDT(DBSStopEvent[] domainObject)
		{
			if (domainObject.Length == 0)
				return new DepartureBoardServiceStopInformation[0];
			
			DepartureBoardServiceStopInformation[] result = new DepartureBoardServiceStopInformation[domainObject.Length];
			int resultCount=0;

			foreach (DBSStopEvent dbsTopEvent in domainObject)
			{
				
				if (dbsTopEvent==null)
				{
					result[resultCount] = null;				 
				}
				else
				{
					result[resultCount] = new DepartureBoardServiceStopInformation();
					result[resultCount].Arrival = CreateDepartureBoardServiceInformation(dbsTopEvent.Arrival);  
					result[resultCount].CallingStopStatus = CreateDepartureBoardServiceCallingStopStatus(dbsTopEvent.CallingStopStatus);
					result[resultCount].Departure = CreateDepartureBoardServiceInformation(dbsTopEvent.Departure);
					result[resultCount].Mode  = CreateDepartureBoardServiceType(dbsTopEvent.Mode); 
					result[resultCount].OnwardIntermediates = CreateDepartureBoardServiceInformationArrayDT(dbsTopEvent.OnwardIntermediates);
					result[resultCount].PreviousIntermediates = CreateDepartureBoardServiceInformationArrayDT(dbsTopEvent.PreviousIntermediates);
					result[resultCount].Service	 = CreateDepartureBoardServiceItinerary(dbsTopEvent.Service);    
					result[resultCount].Stop =	CreateDepartureBoardServiceInformation(dbsTopEvent.Stop);				

					// now check if it has train specific details 
					if (dbsTopEvent is TrainStopEvent)
						result[resultCount] = SetTrainDetails(result[resultCount], dbsTopEvent);
				}
				resultCount++;
			}

			return result;
		}

		/// <summary>
		/// Static assembler method to convert an array of TimeRequestType (domain object) to an array DepartureBoardServiceTimeRequestType (DTO)
		/// </summary>
		/// <param name="domainObject">TimeRequestType array as domain object</param>
		/// <returns>DepartureBoardServiceTimeRequestType array as dto object</returns>
		public static DepartureBoardServiceTimeRequestType[] CreateDepartureBoardServiceTimeRequestTypeArrayDT(TimeRequestType[] domainObject)
		{
			if (domainObject.Length ==0)
				return new DepartureBoardServiceTimeRequestType[0];
		    
			DepartureBoardServiceTimeRequestType[] departureBoardServiceTimeRequestType = new DepartureBoardServiceTimeRequestType[domainObject.Length];
			
			int objectCount=0;  
			foreach (TimeRequestType timeRequestType in domainObject)
			{
			   departureBoardServiceTimeRequestType[objectCount] = (DepartureBoardServiceTimeRequestType)Enum.Parse(typeof(DepartureBoardServiceTimeRequestType), timeRequestType.ToString(), true);  
			   objectCount++;
			}
			
			return departureBoardServiceTimeRequestType;
		}

		#endregion
		
		#region Private methods
		/// <summary>
		/// This static method extracts the trains specific details
		/// </summary>
		/// <param name="departureBoardServiceStopInformation">DepartureBoardServiceStopInformation objecy </param>
		/// <param name="dbsStopEvent">DBSStopEvent object</param>
		/// <returns>DepartureBoardServiceStopInformation object</returns>
		private static DepartureBoardServiceStopInformation SetTrainDetails(DepartureBoardServiceStopInformation departureBoardServiceStopInformation, DBSStopEvent dbsStopEvent)
		{
			 if (departureBoardServiceStopInformation == null || dbsStopEvent ==null)
				 return null;

			TrainStopEvent trainStopInformation = (TrainStopEvent)dbsStopEvent;			
			departureBoardServiceStopInformation.HasTrainDetails = true;
			departureBoardServiceStopInformation.CircularRoute = trainStopInformation.CircularRoute;
			departureBoardServiceStopInformation.FalseDestination = trainStopInformation.FalseDestination;
			departureBoardServiceStopInformation.Via = trainStopInformation.Via;
			departureBoardServiceStopInformation.Cancelled = trainStopInformation.Cancelled;
			departureBoardServiceStopInformation.CancellationReason = trainStopInformation.CancellationReason;
			departureBoardServiceStopInformation.LateReason = trainStopInformation.LateReason;
			return departureBoardServiceStopInformation;
		}

		/// <summary>
		/// This static method convert the DepartureBoardServiceLocation dto object to DBSLocationdomain object
		/// </summary>
		/// <param name="location">DepartureBoardServiceLocation object</param>
		/// <returns>DBSLocation object</returns>
		private	static DBSLocation CreateDBSLocation(DepartureBoardServiceLocation location)
		{
			DBSLocation dbsLocation = new DBSLocation();
			if (location == null)
				return null;
			
			dbsLocation.Code =  location.Code;
			dbsLocation.NaptanIds = location.NaptanIds;
			dbsLocation.Type =	 CreateTDCodeType(location.Type);  
			dbsLocation.Valid = location.Valid; 

			
			if (location.Type.ToString() == "NAPTAN")
			{
				// If we have been sent a Code but no NaptanID and the Type = NAPTAN assume
				// that the code is the NAPTAN
				if (location.NaptanIds[0].Length < 1 && location.Code.Length > 1)
				{
						dbsLocation.NaptanIds[0] = location.Code;
				}

				// If we didn't get sent a locality ID and the location is a NaPTAN lookup the NaPTAN
				// to get its locality
				if (location.Locality.Length < 1)
				{
					NaptanCacheEntry nce = NaptanLookup.Get(location.NaptanIds[0], "");
					dbsLocation.Locality = nce.Locality;
				}
				else
				{
					dbsLocation.Locality = location.Locality;
				}
			}
			else
			{
				dbsLocation.Locality = location.Locality;
			}

			
			return dbsLocation; 
		}

		/// <summary>
		/// This static method convert the DepartureBoardServiceLocation dto object to DBSLocationdomain object
		/// </summary>
		/// <param name="timeRequest">DepartureBoardServiceTimeRequest object</param>
		/// <returns>DBSTimeRequest object</returns>
		private static DBSTimeRequest CreateDBSTimeRequest(DepartureBoardServiceTimeRequest timeRequest)
		{
			DBSTimeRequest dbsTimeRequest = new DBSTimeRequest();
			if (timeRequest == null)
				return null;     			
			dbsTimeRequest.Hour =   timeRequest.Hour; 
			dbsTimeRequest.Minute = timeRequest.Minute;
			dbsTimeRequest.Type = (TimeRequestType)Enum.Parse(typeof(TimeRequestType),timeRequest.Type.ToString());     
			return 	dbsTimeRequest;
		}

		/// <summary>
		/// Static assembler method to convert DBSService (domain object) to DepartureBoardServiceItinerary (DTO)		
		/// </summary>
		/// <param name="dbsService">DBSService as domaion object</param>
		/// <returns>DepartureBoardServiceItinerary as dto object</returns>
		private static DepartureBoardServiceItinerary CreateDepartureBoardServiceItinerary(DBSService  dbsService)
		{
			if (dbsService==null)
				return null;
            IOperatorCatalogue operatorCatalogue = OperatorCatalogue.Current;

			DepartureBoardServiceItinerary itinerary = new DepartureBoardServiceItinerary();
			itinerary.OperatorCode = operatorCatalogue.TranslateOperator(dbsService.OperatorCode) ;
			itinerary.OperatorName = dbsService.OperatorName;
			itinerary.ServiceNumber = dbsService.ServiceNumber;   				
			return itinerary;
		}

		/// <summary>
		///	 Static assembler method to convert DBSEvent (domain object) to DepartureBoardServiceInformation (DTO)		
		/// </summary>
		/// <param name="dbsEvent">DBSEvent as domain object</param>
		/// <returns>DepartureBoardServiceInformation as dto object</returns>
		private static DepartureBoardServiceInformation CreateDepartureBoardServiceInformation(DBSEvent dbsEvent)
		{
			if (dbsEvent==null)
				return null;

			DepartureBoardServiceInformation serviceInformation = new DepartureBoardServiceInformation();  			
			serviceInformation.ActivityType = CreateDepartureBoardServiceActivityType(dbsEvent.ActivityType);
			serviceInformation.ArriveTime  =  dbsEvent.ArriveTime;
			serviceInformation.DepartTime  =  dbsEvent.DepartTime;
			serviceInformation.RealTime    =  CreateDepartureBoardServiceRealTime(dbsEvent.RealTime);
			serviceInformation.Stop        =  CreateDepartureBoardServiceStop(dbsEvent.Stop);
            serviceInformation.Cancelled   =  dbsEvent.Cancelled;    
			return serviceInformation;
		}
         		
		/// <summary>
		/// Static assembler method to convert DBSService array (domain object) to DepartureBoardServiceInformation aryray (DTO)		
		/// </summary>
		/// <param name="dbsEvent">DBSEvent array as domain object</param>
		/// <returns>DepartureBoardServiceInformation array as dto object</returns>
		private static DepartureBoardServiceInformation[] CreateDepartureBoardServiceInformationArrayDT(DBSEvent[] dbsEvent)
		{
			if (dbsEvent.Length ==0)
				return new DepartureBoardServiceInformation[0];
			DepartureBoardServiceInformation[] infoResult = new DepartureBoardServiceInformation[dbsEvent.Length];

			int resultCount=0;
			foreach (DBSEvent info in dbsEvent)
			{
				infoResult[resultCount] = CreateDepartureBoardServiceInformation(info);
				resultCount++;
			}
			return infoResult;
		}

		/// <summary>
		/// Static assembler method to convert DBSRealTime (domain object) to DepartureBoardServiceRealTime (DTO)		
		/// </summary>
		/// <param name="dbsRealTime">DBSRealTime as domain object</param>
		/// <returns>DepartureBoardServiceRealTime as dto object</returns>
		private static DepartureBoardServiceRealTime CreateDepartureBoardServiceRealTime(DBSRealTime dbsRealTime)
		{
			if (dbsRealTime==null)
				return null;

			DepartureBoardServiceRealTime realTime = new DepartureBoardServiceRealTime();   						
			realTime.ArriveTime = dbsRealTime.ArriveTime;
			realTime.ArriveTimeType = CreateDepartureBoardServiceRealTimeType(dbsRealTime.ArriveTimeType);
			realTime.DepartTime = dbsRealTime.DepartTime;
			realTime.DepartTimeType = CreateDepartureBoardServiceRealTimeType(dbsRealTime.DepartTimeType);	
			return realTime;
		}

		/// <summary>
		/// Static assembler method to convert DBSStop (domain object) to DepartureBoardServiceStop (DTO)		
		/// </summary>
		/// <param name="dbsStop">DBSStop as dto object</param>
		/// <returns>DepartureBoardServiceStop as domain object</returns>
		private static DepartureBoardServiceStop CreateDepartureBoardServiceStop(DBSStop  dbsStop)
		{
			if (dbsStop==null)
				return null;  

			DepartureBoardServiceStop  stop = new DepartureBoardServiceStop();
			stop.Name = dbsStop.Name;
			stop.NaptanId = dbsStop.NaptanId;
			stop.ShortCode = dbsStop.ShortCode; 
			return stop;
		}

		/// <summary>
		///	 Static assembler method to convert DepartureBoardServiceRangeType(DTO) to DBSRangeType (domain object)
		/// </summary>
		/// <param name="rangeType">DepartureBoardServiceRangeType as dto object</param>
		/// <returns>DBSRangeType as domain object</returns>
		private static DBSRangeType CreateDBSRangeType(DepartureBoardServiceRangeType rangeType)
		{
			return (DBSRangeType)Enum.Parse(typeof(DBSRangeType), rangeType.ToString());   
		}        

		/// <summary>
		/// Static assembler method to convert DepartureBoardServiceRangeType(DTO) to DBSRangeType (domain object)
		/// </summary>
		/// <param name="codeType">DepartureBoardServiceCodeType as dto object</param>
		/// <returns>TDCodeType as domain object</returns>
		private static TDCodeType CreateTDCodeType(DepartureBoardServiceCodeType codeType)
		{
			return (TDCodeType)Enum.Parse(typeof(TDCodeType),  codeType.ToString());    
		}

		/// <summary>
		/// Static assembler method to convert DBSRealTimeType (domain object) to DepartureBoardServiceRealTimeType (DTO)
		/// </summary>
		/// <param name="dbsRealTimeType">DBSRealTimeType as domain object</param>
		/// <returns>DepartureBoardServiceRealTimeType as dto object</returns>
		private static DepartureBoardServiceRealTimeType CreateDepartureBoardServiceRealTimeType(DBSRealTimeType dbsRealTimeType)
		{
			return (DepartureBoardServiceRealTimeType)Enum.Parse(typeof(DepartureBoardServiceRealTimeType),  dbsRealTimeType.ToString());    
		}

		/// <summary>
		/// Static assembler method to convert DBSActivityType (domain object) to DepartureBoardServiceActivityType (DTO)
		/// </summary>
		/// <param name="dbsActivityType">DBSActivityType as domain object</param>
		/// <returns>DepartureBoardServiceActivityType as dto object</returns>
		private static DepartureBoardServiceActivityType CreateDepartureBoardServiceActivityType(DBSActivityType dbsActivityType)
		{
			return (DepartureBoardServiceActivityType)Enum.Parse(typeof(DepartureBoardServiceActivityType),  dbsActivityType.ToString());    			            			
		}

		/// <summary>
		/// Static assembler method to convert CallingStopStatus (domain object) to DepartureBoardServiceCallingStopStatus (DTO)
		/// </summary>
		/// <param name="callingStopStatus">CallingStopStatus as domain object</param>
		/// <returns>DepartureBoardServiceCallingStopStatus as dto object</returns>		
		private static DepartureBoardServiceCallingStopStatus CreateDepartureBoardServiceCallingStopStatus(CallingStopStatus  callingStopStatus)
		{
			return (DepartureBoardServiceCallingStopStatus)Enum.Parse(typeof(DepartureBoardServiceCallingStopStatus),  callingStopStatus.ToString());    			            			
		}

		/// <summary>
		/// Static assembler method to convert DepartureBoardType (domain object) to CreateDepartureBoardServiceType (DTO)
		/// </summary>
		/// <param name="departureBoardType">DepartureBoardType as domain object</param>
		/// <returns>CreateDepartureBoardServiceType as dto object</returns>
		private static DepartureBoardServiceType CreateDepartureBoardServiceType(DepartureBoardType  departureBoardType)
		{
			return (DepartureBoardServiceType)Enum.Parse(typeof(DepartureBoardServiceType),  departureBoardType.ToString());    			            			
		}
		
		#endregion
	}
}
