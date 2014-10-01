// *********************************************** 
// NAME                 : RTTIXmlExtractor.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 04/01/2005
// DESCRIPTION			: This class is responsible for extracting the data from xml response string.
//						  It would fill DBSResult object with the extracted data.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RTTIXmlExtractor.cs-arc  $ 
//
//   Rev 1.3   Jul 01 2010 12:47:26   apatel
//Updated for duplicate tiploc provider
//
//   Rev 1.2   Jun 15 2010 12:48:46   apatel
//Updated to add new  "Cancelled" attribute to the DBSEvent object
//Resolution for 5554: Departure Board service detail page cancelled train issue
//
//   Rev 1.1   Mar 13 2008 15:22:00   rbroddle
//CCN431 Mobile Service Enhancements - Fixed Date for Last type enquiries
//Resolution for 4597: New enhanced exposed service categories
//
//   Rev 1.0   Nov 08 2007 12:21:40   mturner
//Initial revision.
//
//   Rev 1.6   May 03 2006 18:22:54   rwilby
//Changes to make DepartureBoardService have consistent output.
//Resolution for 3934: Mobile Exposed Services: Issues with Departureboards Service
//
//   Rev 1.5   Apr 10 2006 12:13:10   rwilby
//Fix to load data for circular services. See IR3764 comments in code.
//Resolution for 3764: Mobile: Del 8.1: Mobile Departure Boards - Destination not shown for circular services
//
//   Rev 1.4   May 04 2005 11:48:10   schand
//Added code to convert station name to TitleCase (mixed case). 
//Also removed the word 'Station'
//
//   Rev 1.3   Apr 01 2005 16:09:18   schand
//Extracting ShortCode (CRS code). Fix for 5.7, 5.8
//
//   Rev 1.2   Mar 14 2005 15:12:34   schand
//Changes for configurable switch between CJP and RTTI.
//Assigning TrainStopEvent.Mode = Train
//
//   Rev 1.1   Mar 02 2005 14:39:20   schand
//Code Review Fix (IR-1928)
//
//   Rev 1.0   Feb 28 2005 16:23:06   passuied
//Initial revision.
//
//   Rev 1.18   Feb 21 2005 14:23:06   schand
//Removed un-necessary comments.
//
//   Rev 1.17   Feb 16 2005 15:06:38   schand
//Changes made for TimeToday
//
//   Rev 1.16   Feb 16 2005 11:18:48   schand
//Removed unused methods(commented portion) from file.
//
//   Rev 1.15   Feb 15 2005 12:56:40   schand
//Full Tiploc is converted into Tiploc by removing 8th char if any
//
//   Rev 1.14   Feb 02 2005 15:33:30   schand
//applied FxCop rules
//
//   Rev 1.12   Feb 02 2005 12:32:40   schand
//Removed Type = train
//
//   Rev 1.11   Jan 28 2005 11:39:08   schand
//Now extracting both arrival and departure info
//
//   Rev 1.10   Jan 21 2005 14:22:38   schand
//Code clean-up and comments has been added
//
//   Rev 1.9   Jan 20 2005 17:46:14   schand
//With comments

using System;
using System.Xml;
using System.Xml.Schema ;  
using System.Collections; 
using TransportDirect.Common.PropertyService.Properties ;   
using TransportDirect.UserPortal.DepartureBoardService ;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.AdditionalDataModule ;
using TransportDirect.Common.ServiceDiscovery;  
using TransportDirect.Common.Logging ; 
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.LocationService;
 

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	
	/// <summary>
	/// This class is responsible for extracting the data from xml response string.
	/// </summary>
	[Serializable()]  
	public class RTTIXmlExtractor
	{	
		#region "Private Variables"
		private IAdditionalData additionalData;
		private LookupTranslation lookUpTranslation = null;
		DateTime dateIndicator = new DateTime() ;		
		bool showDeparture=false;
		bool showCallingStop = false;
		bool requestedStopFound = false;
		string requestedStop = string.Empty; 
		TemplateRequestType requestType;
		DBSEvent departure;
		DBSEvent stop ;
		DBSEvent arrival ;
		int iNoFfResult = -1;		
		TimeRequestType fisrtOrLastService = TimeRequestType.TimeToday  ;
		bool bIsFirstService = false;
		bool bIsLastService = false;
		string calculatedRequestedStopByCRS = string.Empty ;
		int iTiplocLength = 7;

        private IDuplicateTiplocProvider duplicateTiplocProvider;
		#endregion
		
		#region "Public Members"
		/// <summary>
		/// RTTIXmlExtractor contructor which create the instance for additional data lookups.
		/// </summary>		
		public RTTIXmlExtractor()
		{			
			dateIndicator = DateTime.Now ;  
			// creating the instance of AdditionalData and holding it in class variables, so that it can be re-use
			additionalData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];

            duplicateTiplocProvider = (IDuplicateTiplocProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.DuplicateTiplocProvider];
			// initiasing lookup translation 
			lookUpTranslation = new LookupTranslation(); 
		}

		
		/// <summary>
		/// Public property DateIndicator to get/set the requested date & time
		/// </summary>
		public DateTime DateIndicator
		{
			get{return dateIndicator;}
			set{dateIndicator = value;}
		}

		/// <summary>
		///  Public property NoOfResult to get/set the number of result required
		/// </summary>
		public int NoOfResult
		{
			get{return iNoFfResult;}
			set{iNoFfResult=value;}
		}
		
		/// <summary>
		///  Public property ShowDeparture to get/set departure or arrival
		///  If true then show departure information else show arrival information
		/// </summary>
		public bool ShowDeparture
		{
			get{return showDeparture;}
			set{showDeparture = value;}
		}
		
		/// <summary>
		///  Public property RequestedStop to get/set the requested stop.
		/// </summary>
		public string RequestedStop 
		{
			get {return requestedStop;}
			set {requestedStop = value;}
		}

		/// <summary>
		///  Public property ShowCallingStop to get/set the calling points visibility.
		///  If true then show calling points for train information else hide all calling points except the requested stop
		/// </summary>
		public bool ShowCallingStop
		{
			get{return showCallingStop;}
			set {showCallingStop = value;}
		}
		
		/// <summary>
		///  Public property RequestedStop to indicate whether first or last service has been requested or not?
		///  If First then First service needs to be shown , if Last then Last service needs to ve shown else show all available service.
		/// </summary>
		public TimeRequestType FirstOrLastService
		{
			get{return fisrtOrLastService;}
			set {fisrtOrLastService = value;}
		}
				
		/// <summary>
		/// This method extract the data from either Stop/station, trip or train xml response string
		/// </summary>
		/// <param name="reqType">To indicate stop/station info , trip info or train onfo</param>
		/// <param name="xmlFragment">xml response from RTTI server</param>
		/// <param name="trainResult">empty DBSResult object. This object would be filled with extracted data or error message.</param>
		/// <returns>Returns true if managed to ectract data or error message else it returns false</returns>
		public bool  ExtractData(TemplateRequestType requestType, string xmlFragment,  DBSResult trainResult)
		{
			try
			{	// Check if it is first or last service
				if (FirstOrLastService == TimeRequestType.First) bIsFirstService = true;
				if (FirstOrLastService == TimeRequestType.Last) bIsLastService = true;
				
				//Checking request type and calling appropriate method
				if (requestType == TemplateRequestType.TripRequestByCRS || requestType == TemplateRequestType.StationRequestByCRS)
					return ExtractDataForTripOrStation(requestType, xmlFragment,  trainResult);				

				else if (requestType == TemplateRequestType.TrainRequestByRID)
					return ExtractDataForTrain(requestType , xmlFragment,  trainResult);		

					// for any other case return false;
				else	return false;				
			}			
			catch(Exception ex)
			{
				// Build error message 
				RTTIUtilities.BuildRTTIError(Messages.RTTIUnableToExtract ,   trainResult)  ;
				// log the error 
				string errMsg = "Error occured in ExtractData " + ex.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));      
				return false;
			}
			finally
			{	// re-initialising FirstOrLastService
				FirstOrLastService = TimeRequestType.TimeToday  ;
				bIsFirstService = false;
				bIsLastService = false;
			}
		}
				
		#endregion
		
		#region "Private Members"
		/// <summary>
		///  This method extract the data from either Stop/station or trip xml response string. 
		/// </summary>
		/// <param name="reqType">To indicate stop/station info , trip info or train onfo</param>
		/// <param name="xmlFragment">xml response from RTTI server</param>
		/// <param name="trainResult">empty DBSResult object. This object would be filled with extracted data or error message.</param>
		/// <returns>Returns true if managed to ectract data or error message else it returns false</returns>
		private bool ExtractDataForTripOrStation(TemplateRequestType reqType, string xmlFragment,  DBSResult trainResult)
		{				
			TrainStopEvent trainStopEvent = null;	
			XmlDocument doc = new XmlDocument();
			XmlNodeList getRequest = null;			  
			int iResultCount = 0;
			int iLastNodeCount = 0;
			int iInnerNodesCount = 0;
			string fromStation = string.Empty;		
			string toStationName = string.Empty;			
			string requestedStationCode = string.Empty;
			bool extractedFromInfo = false;
			
			try
			{				
				// Initialising all three object 
				departure = null;
				stop = null;
				arrival = null;

				// assigning value to class var
				requestType = reqType;				
				// Creating the instance of XmlDocument to load the xml string						
				if (!LoadXmlString(doc,reqType, xmlFragment, ref getRequest))
				{	// build up the error message and return false							
					//Extract Error messages
					DBSMessage dbsMessage = new DBSMessage(); 
					if (ExtractRTTIError(doc, dbsMessage))
					{
						trainResult.Messages = new DBSMessage[1];
						trainResult.Messages[0] = dbsMessage ;
						return true;
					}
					else
					{
						RTTIUtilities.BuildRTTIError(Messages.RTTIUnableToExtractTripData ,  trainResult);
						return false;
					}					
				}
							
				 //Check the element count
				// now we need to calculate requested stop Id from from property instead fom xml response 
				if (getRequest.Count > 0)
				{	// Getting requested station code					
					if (RequestedStop == null || RequestedStop.Length  == 0 )
					{// getting it from xml response 
						requestedStationCode = RTTIUtilities.GetAttributeValue(getRequest[0], AttributeName.StationAttributeByCRS) ; 	
						// Getting NaptanCode for requested station.
						requestedStationCode = GetNaptanForCRS(requestedStationCode);
					}
					else					
						requestedStationCode = RequestedStop ;					
				}

				// Getting all Result element 
				XmlNodeList nodeList = doc.GetElementsByTagName( RTTIUtilities.GetResultTagName(reqType));				
				
				// Checking if has found any result element 
				if (nodeList.Count == 0 )				
				{	
                    // No services exist in the result, this is not an error but return a message indicating no services
                    RTTIUtilities.BuildRTTIError(Messages.RTTIUnableToExtractTripData, trainResult);
					return true;
				}

				// Now Initialise trainStopEvent array 
				// determine the length of the array
				// length of array is depend on the number of result requested by the caller. 
				int iArrLength = 0; 
				if (NoOfResult > 0 && NoOfResult < nodeList.Count)	iArrLength = NoOfResult;
				else iArrLength = nodeList.Count;
				
				// creating an array of required length
				TrainStopEvent[] arrTrainStopEvent = new TrainStopEvent[iArrLength]; 
				
				// Initialising the iResultCount=0
				iResultCount = 0;

				// Iterate through loop and extract the data 
				foreach (XmlNode node in nodeList)
				{	
					// check the number of result needs to be returned
					if (NoOfResult > 0 && NoOfResult <= iResultCount)
					{	// if the required number of result has been extracted then 
						// do not extract any more information 						
						break;
					}

					// check if we need Last service? If yes then go to last journey available
					if (bIsLastService && iLastNodeCount <  nodeList.Count-1)	
					{	// incrementing the iLastNodeCount till we reached to the last result available
						iLastNodeCount++; 
						continue;
					}
						
					// now Initialaise trainStopEvent object
					trainStopEvent = new TrainStopEvent();
										
					// Only Train request has calling stops 
					trainStopEvent.CallingStopStatus = CallingStopStatus.Unknown ;
					// Now setting mode to train 
					trainStopEvent.Mode = DepartureBoardType.Train ;   
				
					
					// instantiating the object for departure , stop and arrival
					departure = new DBSEvent();
					stop = new DBSEvent();
					arrival = new DBSEvent();					

					// build service object 
					// If unbale to build/extract basic information then return false
					if (! BuildServiceEventInfo(node, trainStopEvent))	return false;
					
					// Initialing inner node count & extractedFromInfo
					iInnerNodesCount  = 0;					
					extractedFromInfo = false;
					
					// Iterate through child nodes i.e From and To element 
					foreach (XmlNode InnerNode in node.ChildNodes)
					{					
						switch(InnerNode.Name)
						{	
							case  RTTIElementName.ResponseFrom : 
								// check if already extracted data for from element (To solve association problem)
								// If there are any association in the result it may have amy From or To element
								// We only going to extract First From element 
								if (extractedFromInfo)break;
								
								//Getting Origion station name
								fromStation = InnerNode.InnerText ;	
								// if fromStation is same as requeted stationName then Departure = null
								if (IsSameAsRequestedStop(requestedStationCode,fromStation ))
								{	// extracting info about arrival/departure info								
									if (! BuildDBSEvent(DBSActivityType.Depart, node, InnerNode, requestedStationCode, false , stop ))
										return false;	
								}
								
								//IR3934: Changes to make DepartureBoardService have consistent output.
								// extracting info about arrival/departure info
								if (! BuildDBSEventForAll(DBSActivityType.ArriveDepart , node, InnerNode, requestedStationCode, false, departure, stop))
									return false;									
								
								// From information has extracted and hence change the flag to false.
								// This will help to avoid extracting info another From element
								extractedFromInfo=true;;
								break;
							case RTTIElementName.ResponseTo:
								
								// If there are any association in the result it may have amy From or To element
								// We only going to extract First From element and Last To element 
								// Extract last TO element info only
								if (iInnerNodesCount !=node.ChildNodes.Count -1) break;
								
								// extracting To or arrival station name
								toStationName = InnerNode.InnerText ;
								// if toStationName is same as requeted stationName then Arrival = null
								if (IsSameAsRequestedStop(requestedStationCode, toStationName))
								{	// extracting info about arrival/departure info								
									if (! BuildDBSEvent(DBSActivityType.Arrive , node, InnerNode, requestedStationCode, true , stop ))
										return false;	
								}
								
								//IR3764 [RW]:Fix to load data for circular services.
								//IR3934: Changes to make DepartureBoardService have consistent output.
								if (! BuildDBSEventForAll(DBSActivityType.ArriveDepart  , node, InnerNode, requestedStationCode, true, arrival, stop))
									return false;									

								// extacting via information, this is a free text
								trainStopEvent.Via = RTTIUtilities.GetAttributeValue(InnerNode , AttributeName.Via); 
								break;
						} // end of switch case 
						// incrementing the inner loop count 
						iInnerNodesCount ++;
					} // for inner loop 
					
					// now add contructed object to 
					// inner loop 
					trainStopEvent.Arrival  = arrival;
					trainStopEvent.Departure  = departure ;
					trainStopEvent.Stop = stop;

					// adding to array
					arrTrainStopEvent[iResultCount] = trainStopEvent; 
					
					// increment the value
					iResultCount++;

					// check if first service, if yes then get out of the loop as we don't need any more information					
					if (bIsFirstService) 
					{
						arrTrainStopEvent = new TrainStopEvent[1];
						arrTrainStopEvent[0] = trainStopEvent;
						break;
					}

					// Check if last service
					if (bIsLastService)
					{	// check if this is the last trainStopEvent
						if (iLastNodeCount  == nodeList.Count -1)
						{
							arrTrainStopEvent = new TrainStopEvent[1];
							arrTrainStopEvent[0] = trainStopEvent;
							break;
						}						
					}								
				} // for outer loop 
				
				// adding result array to result object
				trainResult.StopEvents = arrTrainStopEvent;
				return true;
			}
			catch(Exception ex)
			{	
				// log the error 
				string errMsg = "Error occured in ExtractDataForTripOrStation " + ex.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}
		}

		
		/// <summary>
		///  This method extracts the data from train response string. 
		/// </summary>
		/// <param name="reqType">To indicate stop/station info , trip info or train onfo</param>
		/// <param name="xmlFragment">xml response from RTTI server</param>
		/// <param name="trainResult">empty DBSResult object. This object would be filled with extracted data or error message.</param>
		/// <returns>Returns true if managed to ectract data or error message else it returns false</returns>
		private bool ExtractDataForTrain(TemplateRequestType reqType , string xmlFragment, DBSResult trainResult)
		{
			XmlDocument doc = new XmlDocument();	
			XmlNodeList getRequest = null;
			int iResultCount = 0;
			int iInnerNodesCount = 0;
			bool originExtracted =false;
			bool destinationExtracted= false;
			string interStation = string.Empty ;
			string destinationStation = string.Empty ;
			string originStation = string.Empty ;			
			TrainStopEvent trainStopEvent = null;
			DBSEvent origin = null;
			DBSEvent intermediate = null;
			DBSEvent destination = null;
			DBSEvent previousIntermediates = null;
			DBSEvent onwardsIntermediates = null;			
			ArrayList arrListpreviousStop; 
			ArrayList arrListonwardsStop; 
			  
			try
			{	// Creating the instance of XmlDocument to load the xml string					
				if (!LoadXmlString(doc, reqType, xmlFragment, ref getRequest))
				{	// build up the error message and return false
					//Extract Error messages
					DBSMessage dbsMessage = new DBSMessage(); 
					if (ExtractRTTIError(doc, dbsMessage))
					{	trainResult.Messages = new DBSMessage[1];
						trainResult.Messages[0] = dbsMessage ;
						return true;
					}
					else
					{	RTTIUtilities.BuildRTTIError( Messages.RTTIUnableToExtractTrainData  ,  trainResult);
						return false;
					}				
				} // if 
                
				// Now Initialise trainStopEvent array 
				// generally there would be only one Train result which contains info about calling points
				TrainStopEvent[] arrTrainStopEvent = new TrainStopEvent[getRequest.Count];  

				// Initialising the iResultCount=0
				iResultCount = 0;
				// Iterate through loop and extract the data about arrival, departure
				foreach (XmlNode node in getRequest)
				{						
					// now Initialaise trainStopEvent object
					trainStopEvent = new TrainStopEvent();

					// Initialising array list for previous and onwards intermediates
					arrListpreviousStop = new ArrayList();
					arrListonwardsStop  = new ArrayList();
					
					
					// Only Train request has calling stops 
					trainStopEvent.CallingStopStatus = CallingStopStatus.HasCallingStops  ;
					// Now setting mode to train 
					trainStopEvent.Mode = DepartureBoardType.Train ;  
					
					// Initialising all object to null 
					origin = null;
					intermediate = null;
					destination = null;
					previousIntermediates = null;
					onwardsIntermediates = null;
					
					// build service object 
					// If unbale to build/extract basic information then return false
					if (! BuildServiceEventInfoForTrain(node, trainStopEvent))
						return false;				
					
					iInnerNodesCount  = 0;
					originExtracted = false;
					destinationExtracted = false;
					requestedStopFound = false;
					originStation =string.Empty ;
					interStation = string.Empty;
					destinationStation = string.Empty ;

					// Iterating through result 
					foreach (XmlNode InnerNode in node.ChildNodes)
					{		
						switch(InnerNode.Name)
						{	
							case  RTTIElementName.TrainOrigin : 							
								// check if already extracted data for Orgin element (To solve association problem)
								// If there are any association in the result then it may have many IP or DT element
								// We only going to extract First IP and last DT element 								
								// Check if origin has been extracted or requested stop has been found or not
								if (originExtracted || requestedStopFound)
								{	originExtracted = true;
									break;
								}
								
								// Getting origin  station name.
								originStation = RTTIUtilities.GetAttributeValue(InnerNode, AttributeName.RTTIFullTiploc)  ;	
								// if fromStation is same as requeted stationName then Departure = null
								// all relevant extracted info will be assigned to intermediate stop
								if (IsSameAsRequestedStop(RequestedStop, originStation))
								{	
									intermediate = new DBSEvent(); 
									// if unable extract train info then return false
									if (!ExtractInfoForTrain(DBSActivityType.Depart, InnerNode, intermediate ))
										return false;									

									// adding it to the trainStopEvent
									trainStopEvent.Departure = null;
									trainStopEvent.Stop = intermediate ;
									requestedStopFound = true;
								}
								
								//IR3934: Changes to make DepartureBoardService have consistent output.
								origin = new DBSEvent(); 
								// if unable extract train info then return false
								if (!ExtractInfoForTrain(DBSActivityType.Depart,  InnerNode, origin ))
									return false;

								// adding it to the trainStopEvent
								trainStopEvent.Departure = origin ;	
								

								// mark origin extracted true;
								originExtracted=true;
								break;
							case RTTIElementName.TrainInterStop:
								// extracting intermediate station name
								interStation = RTTIUtilities.GetAttributeValue(InnerNode, AttributeName.RTTIFullTiploc)  ;									
								if (IsSameAsRequestedStop(RequestedStop, interStation) && (!requestedStopFound))
								{
									intermediate = new DBSEvent(); 
									// if unable extract train info then return false
									if (!ExtractInfoForTrain(DBSActivityType.ArriveDepart,  InnerNode, intermediate))
										return false;								

									// adding it to the trainStopEvent
									trainStopEvent.Stop = intermediate ;									
									requestedStopFound = true;
								}
								else
								{	// Check if calling points needs to be extracted or not?
									// if not then continue the iteration till it finds destination or DT element
									if (!ShowCallingStop)							
										break;
									// If requested stop found 
									if (requestedStopFound)
									{	
										onwardsIntermediates = new DBSEvent(); 
										// if unable extract train info then return false
										if (!ExtractInfoForTrain(DBSActivityType.ArriveDepart,  InnerNode, onwardsIntermediates))
											return false;
										
										// add it to arrayList
										arrListonwardsStop.Add(onwardsIntermediates);  
									}
									else
									{	
										previousIntermediates = new DBSEvent(); 
										// if unable extract train info then return false
										if (!ExtractInfoForTrain(DBSActivityType.ArriveDepart,  InnerNode, previousIntermediates))
											return false;
										
										// add it to arrayList
										arrListpreviousStop.Add(previousIntermediates);
									}
									
								} // end else																
								break;

							case RTTIElementName.TrainDestination:
								// check if destination isextracted or not? 
								if (destinationExtracted) break;
								
								// Getting all destination element 
								XmlNodeList destinationNodeList = doc.GetElementsByTagName(RTTIElementName.TrainDestination);
								// check if destination exists
								if (destinationNodeList.Count == 0)
								{
									destinationExtracted = true;
									break;  
								}
								
								// getting last destination element 
								XmlNode destinationNode = (XmlNode) destinationNodeList[destinationNodeList.Count -1];
								
								// getting destinationStation
								destinationStation = RTTIUtilities.GetAttributeValue(destinationNode, AttributeName.RTTIFullTiploc);						
			
								// check if destination is same as origin station name
								if (IsSameAsRequestedStop(RequestedStop, destinationStation))
								{	
									intermediate = new DBSEvent();
									// if unable extract train info then return false
									if (!ExtractInfoForTrain(DBSActivityType.Arrive,  InnerNode, intermediate ))
										return false;
									
									// adding it to the trainStopEvent
									trainStopEvent.Stop = intermediate ;
									requestedStopFound = true;
								}
								
								//IR3764 [RW]:Fix to load data for circular services.
								//IR3934: Changes to make DepartureBoardService have consistent output.
								destination = new DBSEvent(); 
								// if unable to extract train info then return false
								if (!ExtractInfoForTrain(DBSActivityType.Arrive, destinationNode, destination ))
									return false;									

								// adding it to the trainStopEvent
								trainStopEvent.Arrival  = destination ;									
								
								destinationExtracted = true;
								break;
				
							case RTTIElementName.TrainAssociation:
                                break;
								// do not anything for this case 
								// for the time it has being ignored?
						} // switch 
						
						// increment the counter
						iInnerNodesCount++;

					} // for inner loop 
					
					// check how many onwards intermediate stops are available, if > 0 then assigned it to the result object 
					if(arrListonwardsStop.Count > 0  )
					{	
						DBSEvent[] tempOnwards = new DBSEvent[arrListonwardsStop.Count];
						arrListonwardsStop.CopyTo(tempOnwards);
						trainStopEvent.OnwardIntermediates = tempOnwards;
					}
					
					// check how many previous intermediate stops are available, if > 0 then assigned it to the result object
					if (arrListpreviousStop.Count > 0  )
					{	DBSEvent[] tempPrevious = new DBSEvent[arrListpreviousStop.Count];
						arrListpreviousStop.CopyTo(tempPrevious);  
						trainStopEvent.PreviousIntermediates = tempPrevious;  
					}
					
					// assigning it to main result object 
					arrTrainStopEvent[iResultCount] = trainStopEvent;  
					trainResult.StopEvents = arrTrainStopEvent;
					// incrementing the outer loop counter 
					iResultCount++;

					// exiting the loop after 1 iteration  as there is no need to extract more than one train result
					if (iResultCount > 0) break;

			} // for outer loop 
				
				return true;
			}
			catch(Exception ex) 
			{	
				// log the error 
				string errMsg = "Error occured in ExtractData " + ex.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}
		}
		
		
		/// <summary>
		/// Extracts train station's tiploc and convert that into equivalent Naptan Id.
		/// Store train station's common name 
		/// </summary>
		/// <param name="xmlNode">xmlNode containing info about train</param>
		/// <param name="dbsEvent">DBSEvent object to which the info need to be added.</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool GetDBSStopForTrain( XmlNode xmlNode, DBSEvent dbsEvent)
		{
			try
			{				
				DBSStop dbsStop = new DBSStop(); 
				// getting Naptan Id from Tiploc
				dbsStop.NaptanId = GetNaptanIDFromTiploc(RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.RTTIFullTiploc)) ; 
				// getting station name
				dbsStop.Name = GetStationNameFromNaptan(dbsStop.NaptanId);
				// getting Short Code (i.e. CRS in this case)
				dbsStop.ShortCode = GetCRSCodeFromNaptanId(dbsStop.NaptanId); 

				dbsEvent.Stop = dbsStop; 
				return true;
			}
			catch(NullReferenceException )
			{
				return false;
			}
		}
		

		/// <summary>
		/// Extracts train arrival or departure or both information 
		/// </summary>
		/// <param name="activityType">To indicate whether to extract info for arrival or departure or both. </param>
		/// <param name="node">xmlNode containing info about train</param>
		/// <param name="dbsEvent">DBSEvent object to which the info need to be added.</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool ExtractInfoForTrain(DBSActivityType activityType,XmlNode node, DBSEvent dbsEvent)
		{
			try
			{	// checking type and calling appropriate method
				switch(activityType)
				{
					case DBSActivityType.Arrive:
						dbsEvent.ActivityType = DBSActivityType.Arrive ; 
						return ExtractInfoForTrainForArrival(node, dbsEvent);						
					case DBSActivityType.Depart:
						dbsEvent.ActivityType = DBSActivityType.Depart  ;
						return ExtractInfoForTrainForDeparture(node, dbsEvent);						
					case DBSActivityType.ArriveDepart: 
						dbsEvent.ActivityType = DBSActivityType.ArriveDepart;
						if (ExtractInfoForTrainForDeparture(node, dbsEvent))						
							return ExtractInfoForTrainForArrival(node, dbsEvent);
						else
							return false;												
					default:
						return false;					
				}
				
			}
			catch(NullReferenceException nEx)
			{
				// log the error 
				string errMsg = "Error occured in ExtractInfoForTrain " + nEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}
		}
		
		
		
		/// <summary>
		/// Extracts train departure information 
		/// </summary>		
		/// <param name="node">xmlNode containing info about train</param>
		/// <param name="dbsEvent">DBSEvent object to which the info need to be added.</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool ExtractInfoForTrainForDeparture(XmlNode xmlNode , DBSEvent dbsEvent)
		{
			try
			{	
				// Build DBSstop object		
				if (! GetDBSStopForTrain(xmlNode, dbsEvent))
					return false;				
				
				string ptdTime = string.Empty;
				DateTime dateTime = new DateTime();
				string etdTime = string.Empty;
				string atdTime = string.Empty;				
				string trainDelayed =string.Empty ;
				string trainOverdue = string.Empty ;
                bool cancelled = false;
			
				// get ptd
				ptdTime = RTTIUtilities.GetAttributeValue(xmlNode,  AttributeName.PublicTimeOfDeparture);
				dateTime = GetRTTIDateTime(ptdTime);
				dbsEvent.DepartTime = dateTime ;

				// build real time object 				
				TrainRealTime trainRealTime = new TrainRealTime(); 

				// checking if real time is null or not?
				if (dbsEvent.RealTime != null  )
					trainRealTime = (TrainRealTime)dbsEvent.RealTime ;
				
				// Getting estimated departure time
				etdTime = RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.EstimatedTimeOfDeparture);

				// Check  if estimated time is present 
				if (etdTime.Length  != 0)
				{
					trainRealTime.DepartTimeType  = DBSRealTimeType.Estimated; 
					dateTime = GetRTTIDateTime(etdTime);  
					trainRealTime.DepartTime = dateTime;
				}
				
				// Getting actual or recorded departure time 
				atdTime = RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.ActualTimeOfDeparture);
				// Check  if actual time is present 
				if (atdTime.Length  != 0)
				{
					trainRealTime.DepartTimeType  = DBSRealTimeType.Recorded;
					dateTime = GetRTTIDateTime(atdTime);
					trainRealTime.DepartTime = dateTime;  
				}							

				// Get train delayed 
				trainDelayed = RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.TrainDepartureDelayed);
				trainRealTime.Delayed = RTTIUtilities.GetBooleanVal(trainDelayed); 
				
				// Get Train OverDue or not?
				trainOverdue = RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.OverDueAtDeparture);
				trainRealTime.Uncertain = RTTIUtilities.GetBooleanVal(trainOverdue);								
		 
				//Add it to main object
				dbsEvent.RealTime = trainRealTime;	
		
                // Check if the train in cancelled
                if (!bool.TryParse(RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.Cancelled), out cancelled))
                {
                    cancelled = false;
                }
                dbsEvent.Cancelled = cancelled;

				return true;
			}
			catch(NullReferenceException  nEx)
			{	
				// log the error 
				string errMsg = "Error occured in ExtractDataForTrainForDeparture " + nEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}
		}
				
		
		
		/// <summary>
		/// Extracts train arrival information 
		/// </summary>		
		/// <param name="node">xmlNode containing info about train</param>
		/// <param name="dbsEvent">DBSEvent object to which the info need to be added.</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool ExtractInfoForTrainForArrival(XmlNode xmlNode, DBSEvent dbsEvent)
		{
			try
			{
				string ptaTime = string.Empty;
				string etaTime = string.Empty;
				string ataTime = string.Empty;
				string trainDelayed = string.Empty;
				string trainOverdue = string.Empty;
                bool cancelled = false;

				DateTime dateTime = new DateTime(); 
				TrainRealTime trainRealTime = new TrainRealTime(); 
	
				// Build DBSstop object		
				if (! GetDBSStopForTrain(xmlNode, dbsEvent))
					return false;
				
				// if RealTime <> null then get existing one 
				if (dbsEvent.RealTime != null )
					trainRealTime = (TrainRealTime)dbsEvent.RealTime ;
							

				// get public time of arrival 
				ptaTime = RTTIUtilities.GetAttributeValue(xmlNode,  AttributeName.PublicTimeOfArrival);
				dateTime =  GetRTTIDateTime(ptaTime);
				dbsEvent.ArriveTime = dateTime;

				// build real time object 
				// Check which estimated or actual time flag 
				etaTime = RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.EstimatedTimeOfArrival);
				
				// Get estimated time of arrival 
				if (etaTime.Length  !=0)
				{
					trainRealTime.ArriveTimeType  = DBSRealTimeType.Estimated; 
					dateTime = GetRTTIDateTime(etaTime);
					trainRealTime.ArriveTime = dateTime;  
				}
				
				// Get actual time of arrival 
				ataTime = RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.ActualTimeOfArrival);
				if (ataTime.Length != 0) // 
				{
					trainRealTime.ArriveTimeType  = DBSRealTimeType.Recorded;
					dateTime = GetRTTIDateTime(ataTime);
					trainRealTime.ArriveTime = dateTime;  
				}
				
				
				// Get train delayed 
				trainDelayed = RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.TrainArrivalDelayed);
				trainRealTime.Delayed = RTTIUtilities.GetBooleanVal(trainDelayed); 				
				// Get Train OverDue or not?
				trainOverdue = RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.OverDueAtArrival);
				trainRealTime.Uncertain = RTTIUtilities.GetBooleanVal(trainOverdue);							
				
				//Add it to main object
				dbsEvent.RealTime = trainRealTime ;

                // Check if the train in cancelled
                if (!bool.TryParse(RTTIUtilities.GetAttributeValue(xmlNode, AttributeName.Cancelled), out cancelled))
                {
                    cancelled = false;
                }
                dbsEvent.Cancelled = cancelled;
				return true;
			}
			catch(NullReferenceException nEx)
			{	
				// log the error 
				string errMsg = "Error occured in ExtractInfoForTrainForArrival " + nEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}
		}

		

		/// <summary>
		/// This method loads the result in xml document object. 
		/// It will also check if it has result element or not 
		/// </summary>
		/// <param name="xmlDoc"> xmldocument for loading xmlstring </param>
		/// <param name="reqType">request type indicating whether its for Station, trip or Train</param>
		/// <param name="xmlFragment">xml response string </param>
		/// <param name="getRequest">nodelist containing all result element</param>
		/// <returns>Returns true if result element is present in the response else false</returns>
		private bool LoadXmlString(XmlDocument xmlDoc, TemplateRequestType reqType, string xmlFragment, ref XmlNodeList getRequest)
		{
			try
			{
				// loading up the xml string
				xmlDoc.LoadXml(xmlFragment); 

				// Creating the instance of XmlDocument to load the xml string					
				xmlDoc.LoadXml(xmlFragment);
								
				// Get the Top level element name which contains the requested station Name
				getRequest =  xmlDoc.GetElementsByTagName(RTTIUtilities.GetResponseElement(reqType)) ;
				
				// chech result count, if zero then return false 
				if (getRequest.Count == 0)
					return false;
				else
					return true;		
			}
			catch(Exception ex)
			{	// throw exception back
				throw ex;
			}
		}
		
		  
		/// <summary>
		/// This method extracts station id and deparure/arrival info
		/// </summary>
		/// <param name="dbsActivityType">DBSActivityType to indicate arrival, departure or both </param>
		/// <param name="node">XmlNode node contains outer result element </param>
		/// <param name="innerNode">XmlNode Innernode contains outer result element</param>
		/// <param name="requestedStationCode">Requested station code</param>
		/// <param name="previouslyCalculated">To indicate arrival previously calculated</param>
		/// <param name="arrivalOrDeparture">DBSEvent object for arrival/departure</param>
		/// <param name="stop">DBSEvent object for arrival/departure</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool BuildDBSEventForAll(DBSActivityType dbsActivityType, XmlNode node, XmlNode innerNode, string requestedStationCode, bool previouslyCalculated,  DBSEvent arrivalOrDeparture, DBSEvent stop)
		{
			try
			{
				// build arrivalOrDeparture
				arrivalOrDeparture.ActivityType = DBSActivityType.Unavailable ;
				
				// build arrival/departure object, if fails then return false 			
				if (! BuildDBSEvent(DBSActivityType.Unavailable , node, innerNode, requestedStationCode, false, arrivalOrDeparture ))
					return false;											

				// if previouslyCalculated  then check arrival information is populated or not 
				if (previouslyCalculated )					
					return true;
				
				// build stop object, if fails then return false 				
				if (! BuildDBSEvent(dbsActivityType, node, innerNode, requestedStationCode, false, stop ))
					return false;	
				
				return true;
			}
			catch(NullReferenceException )
			{
				return false;
			}
		}

		
		/// <summary>
		/// This method extract station id and deparure/arrival info
		/// </summary>
		/// <param name="dbsActivityType">DBSActivityType to indicate arrival, departure or both</param>
		/// <param name="node">XmlNode node contains outer result element</param>
		/// <param name="InnerNode">XmlNode Innernode contains outer result element</param>
		/// <param name="requestedStationCode">Requested station code</param>
		/// <param name="arrivalCalculated">To indicate arrival previously calculated</param>
		/// <param name="dbsEvent">DBSEvent object for arrival/departure or stop </param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool BuildDBSEvent( DBSActivityType dbsActivityType, XmlNode node, XmlNode InnerNode, string requestedStationCode, bool arrivalCalculated , DBSEvent dbsEvent)
		{
			try
			{				
				// Build stop object		
				DBSStop dbsStop = new DBSStop(); 									
				
				// check activity flag if ArriveDepart then need to use		requestedStationCode	
				if (dbsActivityType == DBSActivityType.ArriveDepart )
					dbsStop.NaptanId = requestedStationCode ;// populate NaptanId  						 
				else
					dbsStop.NaptanId = GetNaptanIDFromTiploc(InnerNode.InnerText.ToString()) ;// populate NaptanId  	
				// getting common station name
				dbsStop.Name = GetStationNameFromNaptan(dbsStop.NaptanId);//string.Empty; // Descriptive Station Name will be populated later										
				// getting Short Code (i.e. CRS in this case)
				dbsStop.ShortCode = GetCRSCodeFromNaptanId(dbsStop.NaptanId); 
				dbsEvent.Stop = dbsStop; 

				// exit the routine if arrival already calculated or activity type is unavailable
				if (dbsActivityType == DBSActivityType.Unavailable || arrivalCalculated )
					return true;				
				
				// Check dbsActivityType to call appropriate method for extracting real time info and departure/arrival info
				switch(dbsActivityType)
				{
					case DBSActivityType.Depart:
						stop.ActivityType = DBSActivityType.Depart ;

						// fill up departure's Real time info as well 
						if (!FillTrainRealTime(dbsActivityType, node, dbsEvent))
							return false;						
						   
						return BuildDBSEventForDeparture(node, InnerNode, requestedStationCode, dbsEvent) ;
						
					case DBSActivityType.Arrive:
						stop.ActivityType = DBSActivityType.Arrive ;
						// fill up arrival's Real time info as well 
						if (!FillTrainRealTime(dbsActivityType, node, dbsEvent))
							return false;						

						return BuildDBSEventForArrival(node, InnerNode, requestedStationCode, false, dbsEvent) ;
						
					case DBSActivityType.ArriveDepart:
						stop.ActivityType = DBSActivityType.ArriveDepart ;
						// fill up departure and arrival's Real time info as well 
						if (!FillTrainRealTime(dbsActivityType, node, arrival))
							return false;

						return BuildDBSEventForArrivalAndDeparture(node, InnerNode, requestedStationCode, dbsEvent) ;
						
					default:
						return false;
				}			
				
			}
			catch(NullReferenceException nEx)
			{
				// log the error 
				string errMsg = "Error occured in BuildDBSEvent " + nEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}

			
		}
		
		
		/// <summary>
		/// This method extract deparure info
		/// </summary>
		/// <param name="node">XmlNode node contains outer result element </param>
		/// <param name="InnerNode">XmlNode Innernode contains outer result element</param>
		/// <param name="requestedStationCode">Requested station code</param>
		/// <param name="stop">DBSEvent object for stop</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool BuildDBSEventForDeparture(XmlNode node, XmlNode InnerNode , string requestedStationCode, DBSEvent stop)
		{
			try
			{	string ptdTime = string.Empty;
				DateTime dateTime = new DateTime();
				string etdTime = string.Empty;
				string atdTime = string.Empty;		
				
				// Check whether departure info is needed or not? 
				// if not needed then return true;
				// now we need extract info for both arrival and departure				
				
				// get public time of departure
				ptdTime = RTTIUtilities.GetAttributeValue(node,  AttributeName.PublicTimeOfDeparture);
				dateTime = GetRTTIDateTime(ptdTime);
				stop.DepartTime = dateTime ;
				
				// build real time object 				
				TrainRealTime trainRealTime = new TrainRealTime(); 

				// checking real time already exists, if yes the use the existing one 
				if (stop.RealTime != null  )
						trainRealTime = (TrainRealTime)stop.RealTime ;				

				// Check which estimated or actual time flag 								
				etdTime = RTTIUtilities.GetAttributeValue(node, AttributeName.EstimatedTimeOfDeparture);
				
				// checking estimated departure time 
				if (etdTime.Length  !=0)
				{
					trainRealTime.DepartTimeType  = DBSRealTimeType.Estimated; 
					dateTime = GetRTTIDateTime(etdTime);  
					trainRealTime.DepartTime = dateTime;
				}
				
				// checking actual departure time 
				atdTime = RTTIUtilities.GetAttributeValue(node, AttributeName.ActualTimeOfDeparture);
				if (atdTime.Length  != 0)
				{
					trainRealTime.DepartTimeType  = DBSRealTimeType.Recorded;
					dateTime = GetRTTIDateTime(atdTime);
					trainRealTime.DepartTime = dateTime;  
				}

				//Add it to main object
				stop.RealTime = trainRealTime;
				return true;
			}
			catch(NullReferenceException  nEx)
			{	
				// log the error 
				string errMsg = "Error occured in BuildDBSEventForDeparture " + nEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}

			
		}

		
		/// <summary>
		/// This method extract arival info
		/// </summary>
		/// <param name="node">XmlNode node contains outer result element </param>
		/// <param name="InnerNode">XmlNode Innernode contains outer result element</param>
		/// <param name="requestedStationCode">Requested station code</param>
		/// <param name="useExisting">To indicate to use existing real object or not</param>
		/// <param name="stop">DBSEvent object for stop</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool BuildDBSEventForArrival(XmlNode node, XmlNode InnerNode,  string requestedStationCode , bool useExisting,  DBSEvent stop)
		{
			try
			{	
				string ptaTime = string.Empty;
				string etaTime = string.Empty;
				string ataTime = string.Empty;				
				DateTime dateTime = new DateTime(); 
				TrainRealTime trainRealTime = new TrainRealTime(); 
				
				// Check whether arrival info is needed or not? 
				// if not needed then return true;
				// now we need extract info for both arrival and departure				
				
				// checking whether to use existing object or not?
				if (useExisting)
					if (stop.RealTime != null )
							trainRealTime = (TrainRealTime)stop.RealTime ;									
				else
					if (stop.RealTime != null )
						trainRealTime = (TrainRealTime)stop.RealTime ;

				// get public time of arrival
				ptaTime = RTTIUtilities.GetAttributeValue(node,  AttributeName.PublicTimeOfArrival);
				dateTime =  GetRTTIDateTime(ptaTime);
				stop.ArriveTime = dateTime;

				// build real time object 
				// Get estimated time of arrival 				
				etaTime = RTTIUtilities.GetAttributeValue(node, AttributeName.EstimatedTimeOfArrival);
				if (etaTime.Length  !=0)
				{
					trainRealTime.ArriveTimeType  = DBSRealTimeType.Estimated; 
					dateTime = GetRTTIDateTime(etaTime);
					trainRealTime.ArriveTime = dateTime;  
				}
				
				// Get actual time of arrival 				
				ataTime = RTTIUtilities.GetAttributeValue(node, AttributeName.ActualTimeOfArrival);
				if (ataTime.Length  !=0) // 
				{
					trainRealTime.ArriveTimeType  = DBSRealTimeType.Recorded;
					dateTime = GetRTTIDateTime(ataTime);
					trainRealTime.ArriveTime = dateTime;  
				}
				
				//Add it to main object
				stop.RealTime = trainRealTime ;

				return true;
			}
			catch(NullReferenceException  nEx)
			{
				// log the error 
				string errMsg = "Error occured in BuildDBSEventForArrival " + nEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}			
		}
		
		
		/// <summary>
		/// This method extract arrival and deparure a info
		/// </summary>
		/// <param name="node">XmlNode node contains outer result element </param>
		/// <param name="InnerNode">XmlNode Innernode contains outer result element</param>
		/// <param name="requestedStationCode">Requested station code</param>		
		/// <param name="stop">DBSEvent object for stop</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>		
		private bool BuildDBSEventForArrivalAndDeparture(XmlNode node, XmlNode InnerNode , string requestedStationCode, DBSEvent stop)
		{
			if (BuildDBSEventForDeparture(node, InnerNode, requestedStationCode, stop))
				if (BuildDBSEventForArrival(node, InnerNode,requestedStationCode , true, stop))
					return true;
				else
					return false;
			else
					return false;
		}
		
		

		/// <summary>
		/// Extracts operator info and service number and trip information like delayed, false destination etc.
		/// </summary>
		/// <param name="node">XmlNode node contains outer result element</param>
		/// <param name="trainStopEvent">TrainStopEvent object</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned </returns>
		private bool BuildServiceEventInfo(XmlNode node, TrainStopEvent trainStopEvent)
		{
			string cancelled= string.Empty;
			string circularRoute = string.Empty;

			try
			{
				DBSService dbsService = new DBSService(); 
				dbsService.OperatorCode = RTTIUtilities.GetAttributeValue(node, AttributeName.OperatorCode); 
				dbsService.OperatorName = GetOperatorName(dbsService.OperatorCode); // Operator Name will be populated later
				dbsService.ServiceNumber =  RTTIUtilities.GetAttributeValue(node, AttributeName.ServiceNumber); 
				trainStopEvent.Service = dbsService;

				// Getting TrainStopEventInfo
				cancelled = RTTIUtilities.GetAttributeValue(node, AttributeName.Cancelled);

				//Getting cancelled information	
				trainStopEvent.Cancelled = RTTIUtilities.GetBooleanVal(cancelled);
				//Getting circular route info
				circularRoute = RTTIUtilities.GetAttributeValue(node, AttributeName.CircularRoute);
				trainStopEvent.CircularRoute =   RTTIUtilities.GetBooleanVal(circularRoute);
				trainStopEvent.FalseDestination = string.Empty; 

				return true;
			}
			catch(NullReferenceException )
			{
				return false;
			}
		}
		
		
		/// <summary>
		/// Extracts operator info and service number and train information.
		/// </summary>
		/// <param name="node">XmlNode node contains outer result element</param>
		/// <param name="trainStopEvent">TrainStopEvent object</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned </returns>
		private bool BuildServiceEventInfoForTrain(XmlNode node, TrainStopEvent trainStopEvent)
		{
			//string cancelled= string.Empty;
			//string circularRoute = string.Empty;
			string operatorCode = string.Empty ;

			try
			{	
				// Getting TrainStopEventInfo
				DBSService dbsService = new DBSService(); 

				operatorCode = RTTIUtilities.GetAttributeValue(node, AttributeName.OperatorCode); 
				if (operatorCode.Length  != 0)
					dbsService.OperatorCode = operatorCode; 
				
				// Get the operator Name
				dbsService.OperatorName = GetOperatorName(operatorCode);

				dbsService.ServiceNumber =  RTTIUtilities.GetAttributeValue(node, AttributeName.ServiceNumber); 
				trainStopEvent.Service = dbsService;				
				
				return true;
			}
			catch(NullReferenceException)
			{	return false;}
			catch(XmlException)
			{return false;}
		}



		/// <summary>
		/// Extracts real time information
		/// </summary>
		/// <param name="dbsActivityType">DBSActivityType to indicate arrival, departure or both  </param>
		/// <param name="node">XmlNode node contains outer result element</param>
		/// <param name="dbsEvent">DBSEvent object</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool FillTrainRealTime(DBSActivityType dbsActivityType, XmlNode node, DBSEvent dbsEvent)
		{	
			try
			{	
				// checking type and calling appropriate object 
				switch(dbsActivityType)
				{
					case DBSActivityType.Depart:
						return FillTrainRealTimeDepartureInfo(dbsActivityType,node, dbsEvent);						
					case DBSActivityType.Arrive :
						return FillTrainRealTimeArrivalInfo(dbsActivityType,node, dbsEvent);												
					case DBSActivityType.ArriveDepart:
						if (FillTrainRealTimeDepartureInfo(dbsActivityType,node, departure))
								return FillTrainRealTimeArrivalInfo(dbsActivityType,node, arrival);						
						else
							return false;
					default: 
						return false;
				}
			}
			catch(NullReferenceException nEx)
			{	// log the error 
				string errMsg = "Error occured in FillTrainRealTime " + nEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}
			
		}


		/// <summary>
		/// Extracts real time information for departute
		/// </summary>
		/// <param name="dbsActivityType">DBSActivityType to indicate arrival, departure or both</param>
		/// <param name="node">XmlNode node contains outer result element</param>
		/// <param name="depDBSEvent">DBSEvent object</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool FillTrainRealTimeDepartureInfo(DBSActivityType dbsActivityType, XmlNode node, DBSEvent depDBSEvent)
		{
			string trainDelayed = string.Empty;
			string trainOverdue = string.Empty;
			TrainRealTime trainRealTime = new TrainRealTime(); 
			try
			{		
				// Check whether departure info is needed or not? 
				// if not needed then return true;				
				// now we need extract info for both arrival and departure				

				// check whether to use existing object or not?
				if ( depDBSEvent.RealTime != null)
					trainRealTime = (TrainRealTime) depDBSEvent.RealTime;				

				// Get train delayed 
				trainDelayed = RTTIUtilities.GetAttributeValue(node, AttributeName.TrainDepartureDelayed);
				trainRealTime.Delayed = RTTIUtilities.GetBooleanVal(trainDelayed); 
				
				// Get Train OverDue or not?
				trainOverdue = RTTIUtilities.GetAttributeValue(node, AttributeName.OverDueAtDeparture);
				trainRealTime.Uncertain = RTTIUtilities.GetBooleanVal(trainOverdue);				
				depDBSEvent.RealTime = trainRealTime; 
				return true;
			}
			catch(NullReferenceException nEx)
			{	// log the error 
				string errMsg = "Error occured in FillTrainRealTimeDepartureInfo " + nEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}
		}

		
		/// <summary>
		/// Extracts real time information for arrival
		/// </summary>
		/// <param name="dbsActivityType">DBSActivityType to indicate arrival, departure or both</param>
		/// <param name="node">XmlNode node contains outer result element</param>
		/// <param name="depDBSEvent">DBSEvent object</param>
		/// <returns>Returns true if managed to extract data sucessfully else false would be returned</returns>
		private bool FillTrainRealTimeArrivalInfo(DBSActivityType dbsActivityType, XmlNode node, DBSEvent arrDBSEvent)
		{	
			string trainDelayed = string.Empty;
			string trainOverdue = string.Empty;
			TrainRealTime trainRealTime = new TrainRealTime(); 
			try
			{						
				// Check whether departure info is needed or not? 
				// if not needed then return true;

				// now we need extract info for both arrival and departure				

				// check whether to use object 
				if (arrDBSEvent.RealTime != null )
					trainRealTime = (TrainRealTime) arrDBSEvent.RealTime;				

				// Get train delayed 
				trainDelayed = RTTIUtilities.GetAttributeValue(node, AttributeName.TrainArrivalDelayed);
				trainRealTime.Delayed = RTTIUtilities.GetBooleanVal(trainDelayed); 				
				// Get Train OverDue or not?
				trainOverdue = RTTIUtilities.GetAttributeValue(node, AttributeName.OverDueAtArrival);
				trainRealTime.Uncertain = RTTIUtilities.GetBooleanVal(trainOverdue);
				arrDBSEvent.RealTime = trainRealTime;

				return true;
			}
			catch(NullReferenceException nEx)
			{	
				// log the error 
				string errMsg = "Error occured in FillTRainRealTimeArrivalInfo " + nEx.Message;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMsg));  
				return false;
			}
		}

		
		/// <summary>
		/// Gets NaptanId from given tiploc code
		/// </summary>
		/// <param name="stationTiploc">station Tiploc code</param>
		/// <returns>Returns NaptanId as string </returns>
		private string GetNaptanIDFromTiploc(string stationTiploc)
		{	
			if (stationTiploc == null || stationTiploc.Length  == 0)
				return stationTiploc ;		
			
			// now checking for full tiploc (8 chars instead of 7 chars)
			// if full tiploc code then get rid of last character.
			if (stationTiploc.Length > iTiplocLength )
				stationTiploc = stationTiploc.Substring(0,iTiplocLength);  
			return RTTIUtilities.GetPrefixForTrain + stationTiploc.Trim(); 
		}
		


		/// <summary>
		/// Gets single Naptan Id from given CRS code
		/// </summary>
		/// <param name="stationCode">Station CRS code</param>
		/// <returns>Returns Naptan Id as string </returns>
		private string GetNaptanForCRS(string stationCode)
		{	string[] naptanIds;
			naptanIds = additionalData.LookupNaptanForCode(stationCode, LookupType.CRS_Code) ;   
			// Check length > 0 and pick up the first one
			if (naptanIds.Length > 0 )						
				return naptanIds[0].ToString() ;					
			else
				return string.Empty ;
			
		}
		
		
		/// <summary>
		/// Check whether station naptan are same or not.
        /// If the station naptan does not match the method checks if station got duplicate tiplocs
        /// asn checks if one of the duplicate matches to the station naptan
		/// </summary>
		/// <param name="requestedStationCode">Requested Station Naptan Id </param>
		/// <param name="stationCode">Station Tiploc code</param>
		/// <returns>Returns true if matched else false would be returned </returns>
		private bool IsSameAsRequestedStop(string requestedStationCode, string stationCode)
		{	
            string stationNaptan = GetNaptanIDFromTiploc(stationCode);
			try
			{

                if (requestedStationCode.ToLower().Trim() == stationNaptan.Trim().ToLower())
                    return true;
                else
                {
                    string[] duplicateTiplocs = null;
                    if (duplicateTiplocProvider.HasDuplicateTiploc(stationNaptan, out duplicateTiplocs))
                    {
                        bool duplicateFound = false;
                        if (duplicateTiplocs != null)
                        {
                            foreach (string duplicate in duplicateTiplocs)
                            {
                                if (duplicate == requestedStationCode)
                                {
                                    duplicateFound = true;
                                    break;
                                }
                            }
                        }

                        return duplicateFound;
                    }
                    else
                        return false;
                }
			}
			catch
			{
				return false;
			}
			
		}
		
		
		/// <summary>
		/// Extracts RTTI error information
		/// </summary>
		/// <param name="xmlDoc">XMLDocument object which contains xml error string </param>
		/// <param name="errMessage">DBSMessage which would be filled with error info. </param>
		/// <returns>Returns true if managed to extract error information else it returns false</returns>
		private bool ExtractRTTIError(XmlDocument xmlDoc, DBSMessage errMessage)
		{	string errCode = string.Empty ;
			int iErrCode = 0;
			string errMsg = string.Empty ;
			string errDescription = string.Empty ;

			try
			{	// Getting all error element 
				XmlNodeList errNodeList = xmlDoc.GetElementsByTagName(RTTIElementName.RTTIError);

				// Checking if any error element present
				if (errNodeList.Count > 0 )
				{	// Get the error code and description
					errCode = RTTIUtilities.GetAttributeValue(errNodeList[0], AttributeName.RTTIErrorCode) ;
					// If empty then fill DBSMessage with custom information
					if (errCode == null || errCode.Length  == 0)
					{
						errMessage.Code =  RTTIUtilities.GetErrorCodeForMessage(Messages.RTTIGeneralError);
						errMessage.Description = Messages.RTTIGeneralError;
					}

					else
					{	
						switch(errCode)
						{	
							case RTTIErrorResponse.InvalidRequest:									
									
								errMsg = Messages.RTTIRequestInValid ; 
								iErrCode = RTTIUtilities.GetErrorCodeForMessage(errMsg);
								break;
							case RTTIErrorResponse.NoDataFound:									
								errMsg = Messages.RTTIRequestNoDataFound ; 
								iErrCode = RTTIUtilities.GetErrorCodeForMessage(errMsg);
								break;
							case RTTIErrorResponse.SchemaFailed:									
								errMsg = Messages.RTTISchemaValidationFailed ; 
								iErrCode = RTTIUtilities.GetErrorCodeForMessage(errMsg);
								break;
							case RTTIErrorResponse.Unavailable:									
								errMsg = Messages.RTTIServiceUnavailable ;
								iErrCode = RTTIUtilities.GetErrorCodeForMessage(errMsg);
								break;
							default:									
								errMsg = Messages.RTTIUnknownError ; 
								iErrCode = RTTIUtilities.GetErrorCodeForMessage(errMsg);
								break;
						}

						errMessage.Code = iErrCode;
						errMessage.Description = errMsg; 
					}

					return true;						
				}
				else
				{	// no error info found 
					return false; 
				}				
			}
			catch(XmlException)
			{return false;}
			catch(NullReferenceException)
			{return false;}

		}

	
		/// <summary>
		/// Gets station name for the given Naptan Id
		/// </summary>
		/// <param name="naptanId">Station Naptan Id</param>
		/// <returns>Return station name for given Naptan Id </returns>
		private string GetStationNameFromNaptan(string naptanId)
		{
			if (naptanId == null || naptanId.Length  == 0)
					return string.Empty ;  
				else
					return StopEventManager.StopEventConverter.DecorateStopName(additionalData.LookupStationNameForNaptan(naptanId).ToString());				
		}
		
		/// <summary>
		/// Gets CRS code for the given Naptan Id
		/// </summary>
		/// <param name="naptanId">Station Naptan Id</param>
		/// <returns>Return CRS code for the given Naptan Id </returns>
		private string GetCRSCodeFromNaptanId(string naptanId)
		{
			if (naptanId == null || naptanId.Length  == 0)
				return string.Empty ;  
			else				
				return additionalData.LookupCrsForNaptan(naptanId).ToString();   
		}

		/// <summary>
		/// Gets Operator name for given operator code
		/// </summary>
		/// <param name="operatorCode">Operator Code</param>
		/// <returns>Returns Operator's code </returns>
		private  string GetOperatorName(string operatorCode)			
		{
			string operatorName = string.Empty ;
			operatorName =  lookUpTranslation.GetOperatorName(operatorCode)	;				
			return operatorName ;
		}

		
				
		/// <summary>
		/// Build DateTime object from given time string
		/// </summary>
		/// <param name="time"></param>
		/// <returns>REturns DateTime from xml time</returns>
		private DateTime GetRTTIDateTime(string time)
		{
			char[] delimitter = new char[]{':'};
            int hour;
            int minute;
            DateTime outDateTime;

			try
			{
				if (time == null || time.Length  == 0)	
				{
					return DateTime.MinValue  ;
				}

				string [] timeSplit = time.Split(delimitter, 2);

                hour = int.Parse(timeSplit[0]);
                minute = int.Parse(timeSplit[1]);
                if (bIsLastService && (hour < int.Parse(Properties.Current[Keys.RTTIFirstServiceHour].ToString())))
                {
                    //We've asked for last service and result is in the early hours therefore must be for day after requested date
                    outDateTime = new DateTime(DateIndicator.Year, DateIndicator.Month, DateIndicator.Day + 1, hour, minute, 0, 0);
                }
                else
                {
                    outDateTime = new DateTime(DateIndicator.Year, DateIndicator.Month, DateIndicator.Day, hour, minute, 0, 0);
                }
				return outDateTime;
				
			}
			catch(NullReferenceException)
			{
				return DateTime.MinValue ;
			}
		}

		
		#endregion

		
	}
}
