// ***********************************************
// NAME 		: JourneyRequestHelper.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 03/01/2006
// DESCRIPTION 	: This class provides common functionality required by the JourneyPlannerService component
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/JourneyRequestHelper.cs-arc  $
//
//   Rev 1.4   Aug 10 2009 16:26:28   mmodi
//Throw error if invalid osgr provided for resolving coordinate location
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.3   Aug 04 2009 14:07:30   mmodi
//Added methods to support Car journey planning, and to support NaPTANs and Coordinates in an EnhancedExposedService journey planner request
//Resolution for 5307: CCN517a Web Service Find a Car Route
//Resolution for 5308: CCN520 NaPTANs in Journey Planner Synchronous web service
//
//   Rev 1.2   Apr 24 2009 10:18:50   mmodi
//Updated to allow Async JP to work with test tool
//
//   Rev 1.1   Dec 13 2007 10:23:54   jfrank
//Updated to WSE 3.0
//
//   Rev 1.0   Nov 08 2007 12:24:32   mturner
//Initial revision.
//
//   Rev 1.9   Jul 12 2006 16:21:40   rphilpott
//Ensure CJP sequence number is > 1 to avoid "force coah" problem, and then select best journey(s) to return to client.
//Resolution for 4126: Not returning best journey to Lauren
//
//   Rev 1.8   Mar 02 2006 16:49:52   mdambrine
//added proxy for the consumerservice + more logging
//
//   Rev 1.7   Feb 21 2006 14:05:28   mdambrine
//error text change
//Resolution for 3589: Journey Planner Exposed Services: Formatting error in error message
//
//   Rev 1.6   Jan 26 2006 14:48:34   mdambrine
//changed the logging of the errors to false
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Jan 23 2006 14:05:38   mdambrine
//Ncover changes
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Jan 20 2006 14:42:04   mdambrine
//changed the consumer to seperate project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 19 2006 17:26:52   mdambrine
//Changes to the async process
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 17 2006 14:33:42   mdambrine
//Addition of the sendresult method + asynchronous journeyplanner functionality
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 13 2006 18:29:12   mdambrine
//In development
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 11 2006 13:39:06   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103


using System;
using System.Net;
using System.Threading;

using Microsoft.Web.Services3.Security.Tokens;
using Microsoft.Web.Services3;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.EnhancedExposedServices.Common;
using TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1;
using TransportDirect.EnhancedExposedServiceConsumers.JourneyResultConsumer.V1;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;

using CommonDTO = TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;
using JPDTO = TransportDirect.EnhancedExposedServices.DataTransfer.JourneyPlanner.V1;
using CarJPDTO = TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1;

using Logger = System.Diagnostics.Trace;
using System.Collections;

namespace TransportDirect.UserPortal.JourneyPlannerService
{

	/// <summary>
	/// This class provides common functionality required by the JourneyPlannerService component
	/// </summary>
	public class JourneyRequestHelper
    {
        #region Private members

        private bool isPublicTransportRequest = true;

		private ExposedServiceContext context;
		private JPDTO.PublicJourneyRequest dtoRequest;
        private CarJPDTO.JourneyRequest dtoCarRequestJourney;
		private ITDJourneyRequest request;		

		private readonly int defaultMinimumSequence = 2;

        #endregion

        #region constructor
        /// <summary>
		/// Constructor for JourneyRequestHelper (Public journey mode)
		/// </summary>
		/// <param name="context">The exposed service context</param>
		/// <param name="request">The actual request</param>
		/// <param name="dtoRequest">The data transformation request</param>
		public JourneyRequestHelper(ExposedServiceContext context,
								   ITDJourneyRequest request,
								   PublicJourneyRequest dtoRequest)
		{
			this.context = context;
			this.request = request;
			this.dtoRequest = dtoRequest;

            this.isPublicTransportRequest = true;
		}

        /// <summary>
        /// Constructor for JourneyRequestHelper (Car journey mode)
        /// </summary>
        /// <param name="context">The exposed service context</param>
        /// <param name="request">The actual request</param>
        /// <param name="dtoRequestJourney">The data transformation request for the car journey</param>
        public JourneyRequestHelper(ExposedServiceContext context,
                                   ITDJourneyRequest request,
                                   JourneyRequest dtoCarRequestJourney)
        {
            this.context = context;
            this.request = request;
            this.dtoCarRequestJourney = dtoCarRequestJourney;

            this.isPublicTransportRequest = false;
        }

		#endregion

		#region properties
		/// <summary>
		/// Read-only property to write cleaner code.
		/// </summary>
		public TDResourceManager ResourceManager
		{
			get
			{
				return TDResourceManager.GetResourceManagerFromCache(TDResourceManager.JOURNEY_PLANNER_SERVICE_RM);
			}			
		}		

		/// <summary>
		/// Read-only property to return sequence in original client request,
		/// before it was overridden by our minimum usable value.  
		/// </summary>
		public int OriginalSequence
		{
            get
            {
                if (isPublicTransportRequest)
                {
                    return dtoRequest.Sequence;
                }
                else
                {
                    return 1; // default is always 1 car journey per car journey request
                }
            }
		}

		#endregion

		#region public methods
		/// <summary>
		/// This method performs the necessary processing to convert the location text supplied 
		/// by the External System to actual location data that can be used for journey planning
		/// </summary>
		public void ResolveLocations()
		{
            if (isPublicTransportRequest)
            {
                #region Resolve PublicJourneyRequest

                request.OriginLocation = ResolveLocation(dtoRequest.OriginLocation);
                request.DestinationLocation = ResolveLocation(dtoRequest.DestinationLocation);

                #endregion
            }
            else
            {
                #region Resolve Car RequestJourney

                request.OriginLocation = ResolveLocation(dtoCarRequestJourney.OriginLocation);
                request.DestinationLocation = ResolveLocation(dtoCarRequestJourney.DestinationLocation);
                               
                // If theres a via location
                if (dtoCarRequestJourney.ViaLocationSpecified)
                {
                    request.PrivateViaLocation = ResolveLocation(dtoCarRequestJourney.ViaLocation);
                }

                #endregion
            }
		}

		/// <summary>
		/// This method performs validation on the supplied request object in addition to the schema 
		/// validation already performed on the request
		/// </summary>
		public void Validate()
		{			
			TDDateTime outwardDateTime = (TDDateTime) request.OutwardDateTime[0];
			TDDateTime ReturnDateTime = (TDDateTime) request.ReturnDateTime[0];			
			
			//validate if the time happens in the past
			if (outwardDateTime < TDDateTime.Now)
			{											   
				string validationError = "Outward date time in the past, date time={0}";									

				ThrowError(validationError, 
						   outwardDateTime.ToString(),
						   TDExceptionIdentifier.JPOutwardDateTimeInPast);
			}

			if (request.IsReturnRequired)
            {
                #region Validate return date

                //validate if return date time not specified (if return requested)
				if (ReturnDateTime.GetDateTime() == DateTime.MinValue)
				{
					string validationError = "Return date time not supplied";
					
					ThrowError(validationError, 
							   string.Empty,
							   TDExceptionIdentifier.JPReturnDateTimeNotSupplied);					
				}

				//validate if return date time earlier than or equal to outward date time (if return requested)
				if (ReturnDateTime <= outwardDateTime)
				{
					string validationError = "Return date time earlier than outward date time, outward={0}, return={1}";									
					
					string [] parameters = new string[2];
					parameters[0] = outwardDateTime.ToString();
					parameters[1] = ReturnDateTime.ToString();					

					ThrowError(validationError, 
							   parameters,
							   TDExceptionIdentifier.JPReturnDateTimeEarlierThanOutwardDateTime);
                }

                #endregion
            }
		}

		/// <summary>
		/// This method submits a request to the CJP through the JourneyControl 
		/// component to commence journey planning
		/// </summary>
		/// <returns>ITDJourneyresult from the CJP</returns>
		public ITDJourneyResult CallCJP()
		{
			// Get a CJP Manager from the service discovery
			ICJPManager cjpManager = (ICJPManager) TDServiceDiscovery.Current[ServiceDiscoveryKey.CjpManager];

			ITDJourneyResult tdJourneyResult = cjpManager.CallCJP(request, 
																  context.InternalTransactionId, 
																  (int) TDUserType.Standard, 
																  false, 
																  false, 
																  context.Language, 
																  false);

			if( TDTraceSwitch.TraceVerbose )
			{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Verbose, 
					"CJPManager has returned - Internal Transaction ID = " + context.InternalTransactionId));
			}

			// Check if any return journey times overlap with outward journey times
			if (tdJourneyResult.IsValid) 
			{
				if (tdJourneyResult.CheckForReturnOverlap(request))
				{        
					tdJourneyResult.AddMessageToArray(ResourceManager.GetString("JourneyPlannerOutput.JourneyOverlap"),
													  "JourneyPlannerOutput.JourneyOverlap",
													  0,
													  0, 
													  ErrorsType.Warning); 
				}

				//check if any journeys returned start in the past 
				if (tdJourneyResult.CheckForJourneyStartInPast(request))
				{        
					tdJourneyResult.AddMessageToArray(ResourceManager.GetString("JourneyPlannerOutput.JourneyTimeInPast"),
													  "JourneyPlannerOutput.JourneyTimeInPast",
													  0,
													  0,
													  ErrorsType.Warning);
				}
			}

			return tdJourneyResult;

		}

		/// <summary>
		/// This method sends the supplied PublicJourneyResult object to the ExternalSystem
		/// </summary>
		/// <param name="status">The status the result is in</param>
		/// <param name="result">The actual result to pass back</param>
		public void SendResult(CommonDTO.CompletionStatus status,
							   JPDTO.PublicJourneyResult result)
		{
			int partnerId = Convert.ToInt32(context.PartnerId, Thread.CurrentThread.CurrentCulture);

			//get the partners journeyplannerconsumer parameters from the propeties table
			string username = Properties.Current["JourneyPlannerService.JourneyResultConsumer.Username", partnerId];
			string password = Properties.Current["JourneyPlannerService.JourneyResultConsumer.Password", partnerId];
			string uri = Properties.Current["JourneyPlannerService.JourneyResultConsumer.Uri", partnerId];
			string proxy = Properties.Current["JourneyPlannerService.JourneyResultConsumer.Proxy", partnerId];

            // NOTE: Since Del 10, the properties are loaded using an associated Theme. The PartnerId for a property is 
            // ignored. This causes a problem when setting the values above. Not seen as an issue because no one currently
            // uses this Asynchoronous journey planner.
            // If used in future, work is needed to correctly load properties for the correct PartnerId

            // if values are null, try looking for value without partnerId (this will use the Default Theme given 
            // we are not in a TDP website context)
            if (username == null)
                username = Properties.Current["JourneyPlannerService.JourneyResultConsumer.Username"];
            if (password == null)
                password = Properties.Current["JourneyPlannerService.JourneyResultConsumer.Password"];
            if (uri == null)
                uri = Properties.Current["JourneyPlannerService.JourneyResultConsumer.Uri"];
            if (proxy == null)
                proxy = Properties.Current["JourneyPlannerService.JourneyResultConsumer.Proxy"];

			try
			{
				JourneyResultConsumerWse consumerService = new JourneyResultConsumerWse();

				//Create and add the webservice extentions security token
				UsernameToken token = new UsernameToken(username, 
														password, 
														PasswordOption.SendPlainText);
                consumerService.RequestSoapContext.Security.Tokens.Add(token);
                consumerService.Url = uri;	
				consumerService.Proxy = new WebProxy(proxy);

				//call the consumer webservice
				consumerService.ConsumePublicJourneyResult(context.ExternalTransactionId, 
															status,
															result);
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				
				Logger.Write(operationalEvent);								
			}
		}

		/// <summary>
		/// Updates the sequence in the TDJourneyRequest to a minimum value 
		/// specified in the properties.
		/// </summary>
		/// <param name="status">The status the result is in</param>
		/// <param name="result">The actual result to pass back</param>
		public int UpdateRequestSequence()
		{
			if	(request == null)
			{
				return 0;
			}
			
			string minimumSequenceString = Properties.Current["JourneyPlannerService.MinimumSequence"];
				
			int minimumSequence = -1;

			if	(minimumSequenceString != null && minimumSequenceString.Length > 0)
			{
				try 
				{
					minimumSequence = Convert.ToInt32(minimumSequenceString, Thread.CurrentThread.CurrentCulture);
				}
				catch (FormatException)
				{
					minimumSequence = -1;
				}
				catch (OverflowException)
				{
					minimumSequence = -1;
				}
			}
			
			if	(minimumSequence <= 0)
			{
				string message = "No property found for JourneyPlannerService.MinimumSequence - using default " + defaultMinimumSequence;
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, message));
				minimumSequence = defaultMinimumSequence;
			}

			if	(request.Sequence < minimumSequence)
			{
				request.Sequence = minimumSequence;					
			}

			return request.Sequence;
		}


		#region throw errors and events
        /// <summary>
        /// Throws an TD exception error an logs an operational event
        /// </summary>
        /// <param name="errorMessage">Message to log</param>
        /// <param name="parameters">the innerparameter to fill into the message</param>
        /// <param name="identifier">unique identifier to this error</param>
        public void ThrowError(string errorMessage,
                               string parameter,
                               TDExceptionIdentifier identifier)
        {
            string[] parameters = new string[1];
            parameters[0] = parameter;

            ThrowError(errorMessage, parameters, identifier);
        }

		/// <summary>
		/// Throws an TD exception error an logs an operational event
		/// </summary>
		/// <param name="errorMessage">Message to log</param>
		/// <param name="parameters">the innerparameters to fill into the message</param>
		/// <param name="identifier">unique identifier to this error</param>
		public void ThrowError(string errorMessage,
							   string[] parameters,
							   TDExceptionIdentifier identifier)
		{												
			ThrowError(errorMessage, parameters, identifier, null);
		}

		/// <summary>
		/// Throws an TD exception error an logs an operational event
		/// </summary>
		/// <param name="errorMessage">Message to log</param>
		/// <param name="parameters">the innerparameters to fill into the message</param>
		/// <param name="identifier">unique identifier to this error</param>
		/// <param name="cjpMessages">message coming from the cjp</param>
		public void ThrowError(string errorMessage,
							   string[] parameters,
							   TDExceptionIdentifier identifier,
							   CJPMessage[] cjpMessages)
		{												
			//replace the number of parameters
			if (parameters != null && parameters.Length > 0)
			{
				for(int i=0; i<parameters.Length; i++)			
					errorMessage = errorMessage.Replace("{" + i + "}", parameters[i]);			
			}

			Logger.Write(new OperationalEvent(TDEventCategory.Business, 
											  TDTraceLevel.Error, 
											  errorMessage, 
										      this,
											  context.InternalTransactionId));

			if (cjpMessages != null)
				throw new TDException(errorMessage, false, identifier, cjpMessages);
			else
				throw new TDException(errorMessage, false, identifier);

		}		

		/// <summary>
		/// public method to log the end of the request
		/// </summary>
		/// <param name="enhanceExpServiceType">Enhanced Exposed Service Type Request</param>
		/// <param name="externalTransactionId">Reference transaction Id provided by client</param>
		/// <param name="callSucessful">Indicates whether call was sucessfull or not</param>
		public void LogFinishEvent(bool callSuccessful)
		{
			if (context != null)
			{
				Logger.Write(new EnhancedExposedServiceFinishEvent(callSuccessful, context));						
			}
		}
		#endregion

		#endregion

		#region Private methods

        /// <summary>
        /// Resolves and returns a TDLocation for a DataTransfer.JourneyPlanner.V1.RequestLocation
        /// </summary>
        /// <param name="dtoCarRequestJourney"></param>
        /// <returns></returns>
        private TDLocation ResolveLocation(JPDTO.RequestLocation requestLocation)
        {
            // Resolve location based on the type specified
            switch (requestLocation.Type)
            {
                case JPDTO.LocationType.NaPTAN:

                    return ResolveLocation(requestLocation.NaPTANs, string.Empty);

                case JPDTO.LocationType.Postcode:

                    return ResolveLocation(requestLocation.Postcode, string.Empty);

                case JPDTO.LocationType.Coordinate:
                    // Check if the coordiantes for location can be resolved
                    OSGridReference osgr = new OSGridReference(
                        requestLocation.GridReference.Easting,
                        requestLocation.GridReference.Northing);

                    if (!osgr.IsValid)
                    {

                        string validationError = string.Format("Coordinate for location is invalid [{0},{1}]",
                            requestLocation.GridReference.Easting.ToString(), requestLocation.GridReference.Northing.ToString());

                        ThrowError(validationError,
                                   string.Empty,
                                   TDExceptionIdentifier.JPResolveCoordinateFailed);
                    }

                    return ResolveLocation(osgr, string.Empty, true);
            }

            return null;
        }

        /// <summary>
        /// Resolves and returns a TDLocation for a DataTransfer.CarJourneyPlanner.V1.RequestLocation
        /// </summary>
        /// <param name="dtoCarRequestJourney"></param>
        /// <returns></returns>
        private TDLocation ResolveLocation(CarJPDTO.RequestLocation requestLocation)
        {
            // Resolve location based on the type specified
            switch (requestLocation.Type)
            {
                case CarJPDTO.LocationType.Coordinate:
                    // Check if the coordiantes for location can be resolved
                    OSGridReference osgr = new OSGridReference(
                        requestLocation.GridReference.Easting,
                        requestLocation.GridReference.Northing);

                    if (!osgr.IsValid)
                    {

                        string validationError = string.Format("Coordinate for location is invalid [{0},{1}]", 
                            requestLocation.GridReference.Easting.ToString(), requestLocation.GridReference.Northing.ToString());

                        ThrowError(validationError,
                                   string.Empty,
                                   TDExceptionIdentifier.JPResolveCoordinateFailed);
                    }

                    return ResolveLocation(osgr, requestLocation.Description, false);

                case CarJPDTO.LocationType.NaPTAN:

                    return ResolveLocation(requestLocation.NaPTANs, requestLocation.Description);

                case CarJPDTO.LocationType.Postcode:

                    return ResolveLocation(requestLocation.Postcode, requestLocation.Description);
            }

            return null;
        }

		/// <summary>
		/// This method will resolve a postcode location
		/// </summary>
		/// <param name="dtoLocation">The postcode to resolve</param>		
        private TDLocation ResolveLocation(string postcode, string description)
		{
			LocationSearch search = new LocationSearch();
			LocationChoiceList choiceList = new LocationChoiceList();

            choiceList = search.StartSearch(postcode, 
				SearchType.AddressPostCode, 
				false,
				request.WalkingSpeed * request.MaxWalkingTime, 
				context.InternalTransactionId, 
				false);

				
			if (choiceList.Count != 1)					
			{
				//when there are more then one choices or none throw an appropriate exception
				string locationError = "Postcode not found, postcode= {0}";										

				ThrowError(locationError,
                            postcode,
							TDExceptionIdentifier.JPResolvePostcodeFailed);	

				return null;
			}
			else					
			{	
				TDLocation location = new TDLocation();		
				search.GetLocationDetails(ref location, (LocationChoice)choiceList[0]);

                if (!string.IsNullOrEmpty(description))
                {
                    location.Description = description;
                }

				return location;
			}
		}

        /// <summary>
        /// This method will resolve a naptan location
        /// </summary>
        /// <param name="dtoLocation">The postcode to resolve</param>		
        private TDLocation ResolveLocation(CommonDTO.Naptan[] naptans, string description)
        {
            TDLocation location = new TDLocation();
            
            location.Description = description;
            location.RequestPlaceType = TransportDirect.JourneyPlanning.CJPInterface.RequestPlaceType.NaPTAN;
            
            OSGridReference osgr = new OSGridReference();

            // Create array of naptans from string, but convert to upper first
            string[] strNaptans = new string[naptans.Length];
            for (int i = 0; i < naptans.Length; i++)
            {
                strNaptans[i] = naptans[i].NaptanId.Trim().ToUpper();
            }
                        
            try
            {
                // Get the actual Naptans
                location.NaPTANs = PopulateNaptans(strNaptans);

                // Find osgrid to populate the toid and locality, use the first naptan in the list
                foreach (TDNaptan naptan in location.NaPTANs)
                {
                    if (naptan.Naptan == strNaptans[0])
                    {
                        osgr = naptan.GridReference;

                        if (string.IsNullOrEmpty(description))
                        {
                            location.Description = naptan.Name;
                        }
                    }
                }

                // Update location
                location.GridReference = osgr;

                // Populate the Toids to be used for the car journey
                location.PopulateToids();
                location.Locality = PopulateLocality(osgr);

                // Location is assumed to be valid
                location.Status = TDLocationStatus.Valid;
                
            }
            catch (Exception ex)
            {
                ThrowError(ex.Message, new string[0], TDExceptionIdentifier.JPResolveNaPTANFailed);
            }

            return location;
        }

        /// <summary>
        /// This method will resolve a coordinate location for Car or PT journey planning
        /// </summary>
        /// <param name="osgr">The OS Grid Reference</param>
        /// <param name="description">Location description</param>
        /// <param name="populateTOIDs">Flag for populating TOIDs</param>
        /// <returns></returns>
        private TDLocation ResolveLocation(OSGridReference osgr, string description, bool populateTOIDs)
        {
            // Check bounds of coordinates
            int maxEasting = int.Parse(Properties.Current["Coordinate.Validation.Easting.Max"]);
            int maxNorthing = int.Parse(Properties.Current["Coordinate.Validation.Northing.Max"]);

            if (osgr.Easting < 0 || osgr.Easting > maxEasting)
            {
                ThrowError("Coordinate value invalid", new string[0], TDExceptionIdentifier.JPResolveCoordinateFailed);
            }
            else if (osgr.Northing < 0 || osgr.Northing > maxNorthing)
            {
                ThrowError("Coordinate value invalid", new string[0], TDExceptionIdentifier.JPResolveCoordinateFailed);
            }

            // Create a new TDLocation and populate the name and coordiante
            TDLocation location = new TDLocation();

            location.Description = description;
            location.GridReference = osgr;
            location.RequestPlaceType = TransportDirect.JourneyPlanning.CJPInterface.RequestPlaceType.Coordinate;
            
            // Populate the Toids to be used for the car journey
            location.PopulateToids();
            location.Locality = PopulateLocality(osgr);

            // Location is assumed to be valid
            location.Status = TDLocationStatus.Valid;

            return location;
        }

        /// <summary>
        /// Finds the Naptans that are closest to the provided naptan input
        /// </summary>
        /// <param name="naptan"></param>
        /// <returns>TDNaptan[] naptans</returns>
        private TDNaptan[] PopulateNaptans(string[] naptan)
        {
            TDNaptan[] naptans = new TDNaptan[naptan.Length];
            int i = 0;
            try
            {
                foreach (string tempNaptan in naptan)
                {
                    NaptanCacheEntry x = NaptanLookup.Get(tempNaptan.Trim(), "Naptan");

                    if (x.Found)
                    {
                        naptans[i] = new TDNaptan();
                        naptans[i].Naptan = x.Naptan;
                        naptans[i].GridReference = x.OSGR;
                        naptans[i].Name = x.Description;
                        i++;
                    }
                    else
                    {
                        //If any Naptans are not found abort throw a format exception
                        throw (new FormatException("Naptan code not found or invalid Naptan code used: " + tempNaptan));
                    }
                }
            }
            catch // Catch's any errors from NaptanLookup.Get, e.g. where "ABC" is the naptan submitted
            {
                throw (new FormatException("Naptan code not found or invalid Naptan code used"));
            }

            return naptans;
        }

        /// <summary>
        /// Populate locality data into relevant object hierarchy
        /// </summary>
        /// <param name="osgr"></param>
        /// <returns></returns>
        private string PopulateLocality(OSGridReference osgr)
        {
            IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

            return gisQuery.FindNearestLocality(osgr.Easting, osgr.Northing);
        }

		#endregion
	}
} 
