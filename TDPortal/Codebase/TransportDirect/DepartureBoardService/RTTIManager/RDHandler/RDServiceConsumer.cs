// *********************************************** 
// NAME                 : RDServiceConsumer.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 31/12/2004
// DESCRIPTION			: This class is responsible for sending request and reciving response from RTTI server.

// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RDServiceConsumer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:38   mturner
//Initial revision.
//
//   Rev 1.10   Feb 20 2006 15:40:20   build
//Automatically merged from branch for stream0017
//
//   Rev 1.9.1.1   Feb 02 2006 10:56:14   tolomolaiye
//Code review comments
//Resolution for 17: DEL 8.1 Workstream - RTTI
//
//   Rev 1.9.1.0   Jan 30 2006 11:08:28   tolomolaiye
//Changes for RTTI Internal Event
//Resolution for 17: DEL 8.1 Workstream - RTTI
//
//   Rev 1.9   Jul 29 2005 18:35:44   Schand
//Fix for IR2628, IR2629. 
//
//Changes for HandleFirstAndLast Services() function which gets the first and last services iteratively. 
//Resolution for 2628: First Services are not working betweenWAT & FLE. Also unable to find first train (from WTN)
//Resolution for 2629: Unable to find last Train when filtered by arrival station
//
//   Rev 1.8   Jul 26 2005 15:00:38   Schand
//FIx for IR2594. Switching origin and destination for arrival request.
//
//   Rev 1.7   Jul 15 2005 15:49:52   PNorell
//Ensured logging is only done when turned on to save cycles.
//
//   Rev 1.6   Jul 15 2005 15:01:32   jmcallister
//RTTI Test Feed Fix
//
//   Rev 1.5   Mar 17 2005 11:46:52   schand
//Validation error has been fixed for ByRID element..
//
//   Rev 1.4   Mar 08 2005 16:05:04   schand
//CodeReview suggestion has been implemneted.
//
//   Rev 1.3   Mar 02 2005 14:39:16   schand
//Code Review Fix (IR-1928)
//
//   Rev 1.2   Mar 02 2005 10:48:46   schand
//Fixed Code review errors
//
//   Rev 1.1   Mar 01 2005 18:55:50   schand
//Code Review Fix (IR-1928)
//
//   Rev 1.0   Feb 28 2005 16:23:06   passuied
//Initial revision.
//
//   Rev 1.15   Feb 21 2005 14:56:00   schand
//Added method ValidateMsgID. This methid will be used in future when external RTTI server is available. 
//
//   Rev 1.14   Feb 11 2005 17:47:24   schand
//Connection refused retry has been added
//
//   Rev 1.13   Feb 11 2005 13:38:40   schand
//Removed StartMockServer()
//
//   Rev 1.12   Feb 11 2005 11:36:02   schand
//IRDHandler's public interface has been changed.
//i.e. returning DBSResult object instead of boolean.
//UpdateRequested counter is demoted to private 
//
//   Rev 1.11   Feb 02 2005 15:33:12   schand
//applied FxCop rules
//
//   Rev 1.10   Jan 25 2005 19:32:42   schand
//Rename RTTIEventCounters to RTTIEvent
//
//   Rev 1.9   Jan 25 2005 13:38:30   schand
//Applied request event functionality
//
//   Rev 1.8   Jan 21 2005 17:39:24   schand
//Remove Atribute bug fixed 
//
//   Rev 1.7   Jan 21 2005 14:22:36   schand
//Code clean-up and comments has been added

using System;
using System.Xml ;
using System.Data;
using System.Data.SqlClient;
using System.Net.Sockets;  
using System.Globalization;
using TransportDirect.Common;  
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Common.Logging ;  
using TransportDirect.Common.PropertyService.Properties  ; 
using TransportDirect.Common.DatabaseInfrastructure ;   
//using TransportDirect.ReportDataProvider.DatabasePublishers;
using Logger = System.Diagnostics.Trace;


// ****************************
// COMMENTED OUT, MOVED TO DepartureBoardWebService
// ****************************

//namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
//{

//    /// <summary>
//    /// Summary description for RDServiceConsumer.
//    /// This class is mainly responsible for consuming RTTI service from an external source. 
//    /// Three level of information cane be requested: 
//    /// 1. Station level (information for a particular station)
//    /// 2. Trip level  (information between the two station)
//    /// 3. Train level (information about a specifc train )
//    /// This class will also be responsible for logging the count for each request. 
//    /// 
//    /// </summary>	 
	
//    public class RDServiceConsumer:IRDServiceConsumer
//    {	
//        #region "Class Variables"
//        const int iTiplocCodeLength = 7;   // Tiploc code's max length
//        const int iCRSCodeLength = 3;	   // CRS code's max length 
//        const int iTrainRIDLength = 16;		// Train RTTI ID's max length 
//        string[] arrInterestType = {"arrive", "depart", "both"};	// journey interest type 
//        SocketClient socketClient;	 // variable to interact with RTTI server
//        const string xpathElementForStationReqByCRS= "ByCRS";	// Element name for Station Request when requested by CRS
//        const  string attributeNameForStationReqByCRS= "crs";	// Attribute name for Station Request when requested by CRS
//        const string xpathElementForTripReqByCRS= "ByCRS";		// Element name for Trip Request when requested by CRS
//        const string attributeOriginNameForTripReqByCRS= "main"; // Element name for origin station when requested by CRS
//        const string attributeDestinationNameForTripReqByCRS= "secondary"; // Element name for destination station when requested by CRS
//        const string attributeTimeNameForTripReqByCRS= "time";	   // Attribute name for Trip request when requested by CRS
//        const string attributeInterestTypeNameForTripReqByCRS= "interest"; // Attribute name for interest type when requested by CRS 
//        const string xpathElementForTrain = "ByRID"; // Element name for RID type when requested by train Id
//        const string attributeIdNameForTrain = "rid";  // Attribute name for RID  when requested for train  		
//        const string attributeForDuration = "dur"; // Attribute name for duration
//        const string xpathElementForStationReqByTiploc= "ByTIPLOC"; // Element name for Station Request when requested by Tiploc 
//        const string attributeNameForStationReqByTiploc= "t1";  // Attribute name for station request when requested by Tiploc 
//        string errorDesc = string.Empty; // for holding any error
//        string socketResponse = string.Empty ;   // for holding any response from RTTI server
//        int iDefaultDuration ; // to hold default duration for which information is requested

//        DateTime startDateTime = DateTime.Now;	// start date for journey
//        DateTime finishDateTime = DateTime.Now; // end date for journey
//        bool dataReceived;
		
//        #endregion
		
//        #region "Constructor"
//        /// <summary>
//        /// Class contructor which creates the instance socket client 
//        /// </summary>
//        public RDServiceConsumer()
//        {	
//            try
//            {
//                //Initiate the instance
//                socketClient = new SocketClient();

//                // getting value for Default duration 
//                iDefaultDuration = int.Parse(Properties.Current[Keys.RTTIDefaultDuration].ToString(CultureInfo.InvariantCulture.NumberFormat), CultureInfo.InvariantCulture.NumberFormat);   ;
//            }
//            catch(SocketException sEx)
//            {				
//                // log the error 
//                string errMessage = "SocketClient: Socket exception occured " + sEx.Message ;  
//                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
//                throw new TDException(errMessage, false, TDExceptionIdentifier.RTTISocketCommunicationError );     								
//            }
//            catch(Exception ex)
//            {
//                // log the error 
//                string errMessage = "Error occured in RDServiceConsumer constructor " + " error:  " + ex.Message ;  
//                Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
				
//                throw ex;
//            }
//        }

//        #endregion
		

//        #region "Public Members"
//        /// <summary>
//        /// Summary description for GetArrivalDepartureForStation.
//        /// This method returns the xml response for Station xml request
//        /// </summary>
//        public string GetArrivalDepartureForStation(string msgID, string stationCode)
//        {
//            string xmlRequestTemplate = string.Empty ;			
//            string xmlResponse = string.Empty;
			
 
//            try
//            {	
				
//                // check whether stationcode has been passed or not?				
//                if (! CheckCRSCode(stationCode))
//                {	
//                    // throw an exception 
//                    throw new Exception("GetArrivalDepartureForStation: Invalid CRS Code has been supplied"); 
//                }

//                // get the xml request template
//                xmlRequestTemplate = GetRequestTemplate(TemplateRequestType.StationRequestByCRS);

//                // now call method to populate xml request template with provided parameter
//                if (! BuildXmlRequestForStation(TemplateRequestType.StationRequestByCRS, msgID, stationCode, ref xmlRequestTemplate, ref errorDesc))
//                {
//                    // throw an exception 
//                    throw new Exception("GetArrivalDepartureForStation: Failed to build xml request for " + TemplateRequestType.StationRequestByCRS.ToString()  + errorDesc); 
//                }

//                // Setting start Time 
//                startDateTime = DateTime.Now;
  
//                if( TDTraceSwitch.TraceVerbose )
//                {
//                    // now send the xml request to RTTI Server
//                    Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty , TDTraceLevel.Verbose  , "RDServiceConsumer:GetArrivalDepartureForStation. Xml sent to RTTI is: " + xmlRequestTemplate)) ;
//                }
//                if (GetXMLResponse(xmlRequestTemplate))
//                {
//                    xmlResponse= socketResponse ; 
//                    dataReceived = true;
//                }
//                else
//                {
//                    xmlResponse = string.Empty; 
//                    dataReceived = false;
//                }
				
//                // Setting finishDateTime 
//                finishDateTime = DateTime.Now; 
				
//                // Call RTTI event request
//                UpdateRequestEvent(startDateTime, finishDateTime, dataReceived );

//                // return the xml response 
//                return xmlResponse;
//            }
//            catch(Exception ex)
//            {
//                // throw back all exceptions
//                throw ex;
//            }
//        }

		
//        /// <summary>
//        /// Summary description for GetArrivalDepartureForTrip.
//        /// This method returns the xml response for trip xml request
//        /// destinationCode is optional parameter. If not supplied or null then   
//        /// </summary>
//        public string GetArrivalDepartureForTrip(
//            string msgID, string originCode, string destinationCode, 
//            DateTime requestedTime , int duration, 
//            bool showDepartures)
//        {
//            string xmlRequestTemplate = string.Empty ;			
//            string xmlResponse = string.Empty;

//            try
//            {
//                if (! CheckCRSCode(originCode))
//                {	
//                    // throw an exception 
//                    throw new Exception("GetArrivalDepartureForTrip: Origin CRS Code is invalid"); 
//                }

//                if (!(destinationCode == null || destinationCode.Length ==0))
//                {
//                    if (! CheckCRSCode(destinationCode))
//                    {
//                        // throw an exception 
//                        throw new Exception("GetArrivalDepartureForTrip: Destination CRS Code is invalid"); 
//                    }
//                }

//                // get the xml request template for trip
//                xmlRequestTemplate = GetRequestTemplate(TemplateRequestType.TripRequestByCRS);
				
//                // now call method to populate xml request template with provided parameter
//                if (! BuildXmlRequestForTrip(msgID, originCode,destinationCode , requestedTime, showDepartures , duration , ref xmlRequestTemplate, ref errorDesc))
//                {
//                    // throw an exception 
//                    throw new Exception("GetArrivalDepartureForTrip: Failed to build xml request for " + TemplateRequestType.StationRequestByCRS.ToString()  + errorDesc); 
//                }
				
//                // Setting start Time 
//                startDateTime = DateTime.Now;
  
//                // now send the xml request to RTTI Server
//                Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty , TDTraceLevel.Verbose  , "RDServiceConsumer:GetArrivalDepartureForTrip. Xml sent to RTTI is: " + xmlRequestTemplate)) ;
//                if (GetXMLResponse(xmlRequestTemplate))
//                {
//                    xmlResponse= socketResponse; 
//                    dataReceived = true;
//                }
//                else
//                {
//                    xmlResponse = string.Empty; 
//                    dataReceived = false;
//                }
				
					
//                // Setting finishDateTime 
//                finishDateTime = DateTime.Now; 

//                // Call RTTI event request
//                UpdateRequestEvent(startDateTime, finishDateTime, dataReceived );

//                // return the xml response 
//                return xmlResponse;

//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }			
//        }

		
//        /// <summary>
//        /// Summary description for GetArrivalDepartureByTrainRID.
//        /// This method returns the xml response for train xml request		
//        /// </summary>
//        public string GetArrivalDepartureByTrainRID(string msgID, string trainRID, DateTime startDate)
//        {
//            string xmlRequestTemplate = string.Empty ;			
//            string xmlResponse = string.Empty;


//            try
//            {	
//                // Check if trainRID is valid or not?
//                if (trainRID == null || trainRID.Length  == 0 || trainRID.Length > iTrainRIDLength)
//                {
//                    // throw an exception 
//                    throw new Exception("GetArrivalDepartureByTrainRID: TrainRID is invalid"); 
//                }
				
//                // get the xml request template for trip
//                xmlRequestTemplate = GetRequestTemplate(TemplateRequestType.TrainRequestByRID);

//                // now call method to populate xml request template with provided parameter
//                if (! BuildXmlRequestForTrain(msgID, trainRID, ref xmlRequestTemplate, ref errorDesc))
//                {
//                    // throw an exception 
//                    throw new Exception("GetArrivalDepartureForTrip: Failed to build xml request for " + TemplateRequestType.StationRequestByCRS.ToString()  + errorDesc); 
//                }				
				
//                // Setting start Time 
//                startDateTime = DateTime.Now;

//                // now send the xml request to RTTI Server
//                Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty , TDTraceLevel.Verbose  , "RDServiceConsumer:GetArrivalDepartureByTrainRID. Xml sent to RTTI is: " + xmlRequestTemplate)) ;
//                if (GetXMLResponse(xmlRequestTemplate))
//                {
//                    xmlResponse= socketResponse; 
//                    dataReceived = true;
//                }
//                else
//                {
//                    xmlResponse = string.Empty; 
//                    dataReceived = true;
//                }
					
//                // Setting finishDateTime 
//                finishDateTime = DateTime.Now; 

//                // Call RTTI event request
//                UpdateRequestEvent(startDateTime, finishDateTime, dataReceived );

//                // return the xml response 
//                return xmlResponse;

//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
			
//        }


//        /// <summary>
//        /// Summary description for UpdateRequestEvent.
//        /// This method update the request event 		
//        /// </summary>
//        private void UpdateRequestEvent(DateTime startTime, DateTime finishTime, bool dataReceived )
//        {	
//            try
//            {
//                RTTIEvent rEvent = new RTTIEvent(startTime, finishTime, dataReceived)  ;   				
//                Logger.Write(rEvent);			
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }			
//        }

//        #endregion


		
//        #region "Private Members"
		
//        /// <summary>
//        ///  Gets xml response for the specified xml request 
//        /// </summary>
//        /// <param name="xmlTemplate">xml request</param>
//        /// <returns>REturns if able to get response else false would be returned.</returns>
//        private bool GetXMLResponse(string xmlTemplate)
//        {			
//            string errorDesc=string.Empty ;
//            try
//            {	
//                socketResponse = string.Empty ;
//                SocketClient socketClient = new SocketClient();

//                if (socketClient.SendRequest(xmlTemplate))
//                {
//                    socketResponse  = (string)socketClient.SocketResponse;  
					
//                    if (socketResponse.Length > 0)
//                    {
//                        return true;					
//                    }
//                    else
//                    {
//                        return false;					
//                    }

//                    // For multi thread request, this will ensure that server has returned correct response for a given request. 
//                    //if (ValidateMsgID(msgID, xmlResponse)){}
//                    // modify this method accept one more parameter i.e. msgID and then call ValidateMsgID(msgID,socketResponse)

//                }
//                else
//                    return false ; 
//            }
//            catch(SocketException sEx)
//            {
//                errorDesc = sEx.Message.ToString();  
				
//                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
//                    TDTraceLevel.Error, errorDesc + " "  + "TDException :" + sEx.Message.ToString());
				
//                // log the error 				  
//                Logger.Write(oe) ;

//                return false;     								
//            }
//            catch(TDException  tdEx)
//            {
//                    errorDesc = tdEx.Message.ToString();  				
//                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
//                    TDTraceLevel.Error, errorDesc + " "  + "TDException :" + tdEx.Message.ToString());
//                // log the error 				  
//                Logger.Write(oe) ;
//                return false;
//            }
//        }

		
//        /// <summary>
//        /// This method will return requested xml template from DB. 
//        /// </summary>
//        /// <param name="reqTemplateType">TemplateRequestType type</param>
//        /// <returns>Xml request string</returns>
//        private string GetRequestTemplate(TemplateRequestType reqTemplateType)
//        {	
//            string xmlTemplate = string.Empty;
			
//            switch(reqTemplateType)
//            {
//                case TemplateRequestType.StationRequestByCRS:
//                    // now get the correct xml template for StationByCRS					
//                    xmlTemplate = Properties.Current[Keys.RTTIStationRequestByCRS];  
//                    break;
//                case TemplateRequestType.TripRequestByCRS:				
//                    // now get the correct xml template for TripByCRS
//                    xmlTemplate = Properties.Current[Keys.RTTITripRequestByCRS];
//                    break;								
//                case TemplateRequestType.TrainRequestByRID:
//                    // now get the correct xml template for TrainRequestByRID
//                    xmlTemplate = Properties.Current[Keys.RTTITrainRequestByRID];
//                    break;				
//            }
	
//            return xmlTemplate;

//        }

		
//        /// <summary>
//        /// Build the request xml string 
//        /// </summary>
//        /// <param name="msgID">MessageId for the RTTI request</param>
//        /// <param name="xmldoc">XmlDocument object</param>
//        /// <param name="xmlRequest">Xml request string </param>
//        /// <param name="errDesc">error description would be returned in this variable</param>
//        /// <returns>REturns true if able to build request else false would be returned.</returns>
//        private bool BuildXmlRequest(string msgID, ref XmlDocument xmldoc ,string xmlRequest, ref string errDesc)
//        {
//            try
//            {
//                if (xmlRequest == null || xmlRequest.Length  == 0)
//                {
//                        errDesc = "No XML request template found" ;
//                    return false;
//                }
				
//                if (! CheckMsgID(msgID) )
//                    return false;				
							
//                // loading XML from specified template
//                xmldoc.LoadXml(xmlRequest) ;				
				
//                // point it to root element 
//                XmlElement xmlRoot = xmldoc.DocumentElement ;				
				
//                // Populate the msgID 
//                xmlRoot.Attributes[RTTIUtilities.GetAttributeName(AttributeName.MessageId)].Value  = msgID;
				
//                // Populate current time stamp
//                xmlRoot.Attributes["ts"].Value = ReturnCurrentXmlTimeStamp();

//                return true;
				
//            }
//            catch(XmlException xEx)
//            {
//                errDesc = xEx.Message.ToString();  
//                return false;
//            }
//        }

		
//        /// <summary>
//        /// Build the request xml string for train  
//        /// </summary>
//        /// <param name="msgID">MessageId for the RTTI request</param>
//        /// <param name="trainRID">Train RID </param>
//        /// <param name="startDate">The date on which train starts </param>
//        /// <param name="xmlRequest">Xml request string</param>
//        /// <param name="errDesc">error description would be returned in this variable</param>
//        /// <returns>Returns true if able to build request else false would be returned.</returns>
//        private bool BuildXmlRequestForTrain(string msgID, string trainRID, ref string xmlRequest, ref string errDesc)
//        {	
//            XmlDocument xmldoc = new XmlDocument();
//            string xPathElementName= string.Empty;
//            string attributeName = string.Empty;
//            bool xmlRequestModified= false;
//            int iAttributeCount = 0;

//            try
//            {
//                // build basic xml request structure
//                if (! BuildXmlRequest(msgID, ref xmldoc, xmlRequest, ref errDesc))
//                    return false;				
				
//                // getting xpath element name
//                xPathElementName = xpathElementForTrain ;
				
//                // Getting requested element 
//                XmlNodeList myNodelist =  xmldoc.GetElementsByTagName(xPathElementName); 
//                XmlNode xmlnode ;
//                XmlAttribute xmlAtt;

//                if (myNodelist.Count == 0)
//                {
//                    // Unable to build xml request
//                    errDesc = "Unable to build xml request for element: " + xPathElementName + " for >"  + xmlRequest ;
//                    return false;
//                }

//                for (int i=0; i < myNodelist.Count; i++)
//                {   
//                    xmlnode = myNodelist[i];					
					
//                    iAttributeCount = xmlnode.Attributes.Count;
//                    for (int j=0; j< iAttributeCount; j++  )
//                    {		
//                        xmlAtt = xmlnode.Attributes[j];						 
//                        if (xmlAtt.Name == attributeIdNameForTrain)
//                        {
//                            xmlAtt.Value = trainRID;
//                            xmlRequestModified = true;
//                        }							
//                    }
//                }
				
//                if (iAttributeCount == 0 || (!xmlRequestModified))
//                {
//                    // Unable to build xml request properly
//                    errDesc = "Unable to build xml request for element: " + attributeName + " for >"  + xmlRequest ;
//                    return false;
//                }

//                // getting outer xml of the document element	
//                xmlRequest = xmldoc.OuterXml;			
//                return true;
//            }
//            catch(NullReferenceException nEx)
//            {
//                errorDesc = nEx.Message.ToString();  
//                return false;
//            }
//            catch(TDException tdex)
//            {
//                errorDesc = tdex.Message.ToString();  
//                return false;
//            }

//        }


//        /// <summary>
//        /// Build the request xml string for station
//        /// </summary>
//        /// <param name="reqTemplateType">TemplateRequestType type</param>
//        /// <param name="msgID">MessageId for the RTTI request</param>
//        /// <param name="stationCode"></param>
//        /// <param name="xmlRequest">Xml request string</param>
//        /// <param name="errDesc">error description would be returned in this variable</param>
//        /// <returns>Returns true if able to build request else false would be returned.</returns>
//        private bool BuildXmlRequestForStation(TemplateRequestType reqTemplateType, string msgID, string stationCode, ref string xmlRequest, ref string errDesc )
//        {
//            XmlDocument xmldoc = new XmlDocument();
//            string xPathElementName= string.Empty;
//            string attributeName = string.Empty;
//            bool xmlRequestModified= false;			

//            try
//            {
//                // build basic xml request structure
//                if (! BuildXmlRequest(msgID, ref xmldoc, xmlRequest, ref errDesc))
//                    return false;				

//                // now populate stationCode 			
//                if (reqTemplateType == TemplateRequestType.StationRequestByCRS )
//                {
//                    xPathElementName = xpathElementForStationReqByCRS  ;	
//                    attributeName = attributeNameForStationReqByCRS ;
//                }
//                else
//                {	
//                    xPathElementName = xpathElementForStationReqByTiploc  ;
//                    attributeName = attributeNameForStationReqByTiploc ;
//                }

				
//                XmlNodeList myNodelist =  xmldoc.GetElementsByTagName(xPathElementName); 
//                XmlNode xmlnode ;
//                XmlAttribute xmlAtt;

//                if (myNodelist.Count == 0)
//                {
//                    // Unable to build xml request
//                    errDesc = "Unable to build xml request for element: " + xPathElementName + " for >"  + xmlRequest ;
//                    return false;
//                }
				
//                // iterate through  
//                for (int i=0; i < myNodelist.Count; i++)
//                {   
//                    xmlnode = myNodelist[i];					
					
//                    for (int j=0; j< xmlnode.Attributes.Count; j++  )
//                    {		
//                        if (xmlnode.Attributes[j].Name == attributeName)
//                        {
//                            xmlAtt = xmlnode.Attributes[j];
//                            xmlAtt.Value = stationCode;
//                            xmlRequestModified = true;
//                        }
							
//                    }
//                }				
				
//                if (!xmlRequestModified)
//                {
//                    // Unable to build xml request properly
//                    errDesc = "Unable to build xml request for element: " + attributeName + " for >"  + xmlRequest ;
//                    return false;
//                }

//                // getting outer xml of the document element	
//                xmlRequest = xmldoc.OuterXml;			
//                return true;
//            }
//            catch(NullReferenceException nEx)
//            {
//                errorDesc = nEx.Message.ToString();  
//                return false;
//            }
//            catch(XmlException xEx)
//            {
//                errorDesc = xEx.Message.ToString();  
//                return false;
//            }

//        }

		
//        /// <summary>
//        /// Build the request xml string for trip
//        /// </summary>
//        /// <param name="msgID"><MessageId for the RTTI request</param>
//        /// <param name="originCode">Origin's station code</param>
//        /// <param name="destinationCode">Destination station code</param>
//        /// <param name="requestedTime">Requested time of arrival/departure</param>
//        /// <param name="showDepartures">To indicate to get arrival or departure info </param>
//        /// <param name="duration">Time Window</param>
//        /// <param name="xmlRequest">Xml request string</param>
//        /// <param name="errDesc">Error description would be returned in this variable</param>
//        /// <returns>Returns true if able to build request else false would be returned.</returns>
//        private bool BuildXmlRequestForTrip(string msgID, string originCode, string destinationCode, DateTime requestedTime, bool showDepartures, int duration , ref string xmlRequest, ref string errDesc)
//        {
//            XmlDocument xmldoc = new XmlDocument();
//            string xPathElementName= string.Empty;
//            bool xmlRequestModified = false;
//            int iAttributeCount=0;
//            string main = string.Empty;
//            string secondary = string.Empty; 

//            try
//            {
//                // build basic xml request structure
//                if (! BuildXmlRequest(msgID, ref xmldoc, xmlRequest, ref errDesc))			
//                    return false;

//                //-----------------------------------------------------------------
//                // Fix for IR2594
//                // main attibute = where passenger is alighting/boarding the train (The location of interest).
//                // if request type = arrive then main = destination and seceondary=origin
//                // else main = origin and seceondary=destination
//                // first making sure that both origin and destination exists
//                if (originCode!=null && originCode.Length > 0 &&  destinationCode!=null && destinationCode.Length >0)
//                {
//                    // if arrival request
//                    if (showDepartures)
//                    {
//                        main =	originCode;	 // for departure origin is point of boarding.
//                        secondary = destinationCode;
//                    }
//                    else
//                    {
//                        main =	destinationCode;	 // for arrival destination is point of alighting.
//                        secondary = originCode;
//                    }
//                }
//                else
//                {	// if only one location present 
//                    main =	originCode;	 
//                    secondary = destinationCode;
//                }
				
//                //------------------------------------------------------------------

				
//                xPathElementName = xpathElementForTripReqByCRS  ;	
//                XmlNodeList myNodelist =  xmldoc.GetElementsByTagName(xPathElementName); 
//                XmlNode xmlnode ;
//                XmlAttribute xmlAtt;

//                if (myNodelist.Count == 0)
//                {
//                    // Unable to build xml request
//                    errDesc = "Unable to build xml request for element: " + xPathElementName + " for >"  + xmlRequest ;
//                    return false;
//                }
				

//                // iterate through xml structure  
//                for (int i=0; i < myNodelist.Count; i++)
//                {   
//                    xmlnode = myNodelist[i];					
//                    iAttributeCount = -1;
//                    for (int j=0; j< xmlnode.Attributes.Count; j++  )
//                    {
//                            iAttributeCount++; 
//                        xmlAtt = xmlnode.Attributes[j];
//                        // building origin attribute 
//                        if( xmlAtt.Name == attributeOriginNameForTripReqByCRS )
//                        {
//                            xmlAtt.Value = main; // originCode;
//                            xmlRequestModified = true;
//                            continue;
//                        }
						
//                        // building destination attribute if value != empty 
//                        if (xmlAtt.Name == attributeDestinationNameForTripReqByCRS)
//                        {
//                            //if (destinationCode==null || destinationCode.Length  == 0)secondary
//                            if (secondary==null || secondary.Length  == 0)
//                            {
//                                xmlnode.Attributes.Remove(xmlAtt);
//                                j--; // decrementing the value as count as gone down
//                            }
//                            else
//                                xmlAtt.Value = secondary; //destinationCode;
							
//                            continue;
//                        }								
						
//                        // building interest attribute 
//                        if (xmlAtt.Name == attributeInterestTypeNameForTripReqByCRS)
//                        {
//                            if (showDepartures)
//                                xmlAtt.Value = arrInterestType[(int)InterestType.Departure] ; 							
//                            else
//                                xmlAtt.Value = arrInterestType[(int)InterestType.Arrival]; 																
							
//                            continue;
//                        }
//                        // building time attribute 							
//                        if (xmlAtt.Name == attributeTimeNameForTripReqByCRS) 
//                        {
//                            if (requestedTime!= DateTime.MinValue && requestedTime.Equals(null))
//                                xmlnode.Attributes.Remove(xmlAtt);  							
//                            else
//                                xmlAtt.Value = ReturnXmlTimeStamp(requestedTime) ; 
								
//                            continue;
//                        }
						
//                        // building duartion attribute 
//                        if (xmlAtt.Name == attributeForDuration)
//                        {
//                            if (duration > 0)
//                                xmlAtt.Value = duration.ToString(CultureInfo.InvariantCulture) ;							
//                            else
//                                xmlAtt.Value = iDefaultDuration.ToString(CultureInfo.InvariantCulture) ;
							
//                            continue;
//                        }							
//                    }
//                }

//                // check whether it has modified main attribute or not?
//                if (iAttributeCount == 0 ||(!xmlRequestModified))
//                {
//                    // Unable to build xml request properly
//                    errDesc = "Unable to build xml request for element: "  + xmlRequest ;
//                    return false;
//                }

//                // getting outer xml of the document element	
//                xmlRequest = xmldoc.OuterXml;			
//                return true;

//            }
//            catch(NullReferenceException nEx)
//            {
//                errorDesc = nEx.Message.ToString();  
//                return false;
//            }
//            catch(XmlException xEx)
//            {
//                errorDesc = xEx.Message.ToString();  
//                return false;
//            }
//        }		
		
//        /// <summary>
//        /// Check CRS code
//        /// </summary>
//        /// <param name="sCRSCode">Station CRS code.</param>
//        /// <returns>Returns true if valid Tiploc else false would be returned.</returns>
//        private bool CheckCRSCode(string sCRSCode)
//        {
//            if (sCRSCode == null || sCRSCode.Length ==0)
//            {	
//                return false;
//            }

//            if (sCRSCode.Length > iCRSCodeLength )
//            {
//                return false;
//            }

//            return true;
//        }
		
		
//        /// <summary>
//        /// Check MsgId
//        /// </summary>
//        /// <param name="msgID">Message Id for xml request</param>
//        /// <returns></returns>
//        private bool CheckMsgID(string msgID)
//        {
//            if ( msgID == null || msgID.Length ==0)
//                return false;
//            else
//                return true;			
//        }

		
//        /// <summary>
//        /// Gets xml time stamp
//        /// </summary>
//        /// <param name="requestedTime">Requested DateTime</param>
//        /// <returns>Xml time stamp as string </returns>
//        private string ReturnXmlTimeStamp(DateTime requestedTime) 
//        {	
//            return GetXmlTimeStamp(requestedTime); 
//        }

		
//        /// <summary>
//        /// Gets current xml time stamp
//        /// </summary>
//        /// <returns>Cuurent xml time stamp as string </returns>
//        private string ReturnCurrentXmlTimeStamp()
//        {
//            return GetXmlTimeStamp(DateTime.Now); 
//        }
		
//        /// <summary>
//        /// Gets xml time stamp
//        /// </summary>
//        /// <param name="requestedTime">Requested DateTime</param>
//        /// <returns>Xml time stamp as string </returns>
//        private string GetXmlTimeStamp(DateTime time)
//        {	
//            //return time.ToString("yyyy-MM-ddThh:mm:ss.fffff");
//            return time.ToString("s", CultureInfo.CurrentCulture.DateTimeFormat);
//        }

		
//        /// <summary>
//        /// Validates the given msgId with the msgId retreived in response xml string
//        /// </summary>
//        /// <param name="msgID">MessageId assigned to the request </param>
//        /// <param name="xmlResponse">Xml response string</param>
//        /// <returns>REturns true if same else false would be returned</returns>
//        private bool ValidateMsgID(string msgID, string xmlResponse)
//        {
//                XmlDocument xmlDoc = new XmlDocument(); 
//            string extractedMsgID = string.Empty ;
//            try
//            {	// check xml response string 
//                if (xmlResponse == null || xmlResponse.Length  == 0) return false;				

//                // check message Id to compare
//                if (! CheckMsgID(msgID)) return false;				
							
//                // loading XML from specified template
//                xmlDoc.LoadXml(xmlResponse) ;				
				
//                // point it to root element 
//                XmlElement xmlRoot = xmlDoc.DocumentElement ;				
				
//                // Populate the msgID 
//                extractedMsgID = (string)xmlRoot.Attributes[RTTIUtilities.GetAttributeName(AttributeName.MessageId)].Value ;

//                if (extractedMsgID ==null || extractedMsgID.Length ==0) return false;

//                if (extractedMsgID == msgID) return true;
//                else return false;								 
				
//            }
//            catch(Exception)
//            {	// return false for any exception		
//                return false;
//            }

//        }
//        #endregion

//    }
//}