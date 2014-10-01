// *********************************************** 
// NAME                 : RTTIUtilities.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 10/01/2005 
// DESCRIPTION  	: This class contains some static helper function which are used by other class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RTTIUtilities.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:40   mturner
//Initial revision.
//
//   Rev 1.2   Mar 02 2005 14:39:18   schand
//Code Review Fix (IR-1928)
//
//   Rev 1.1   Mar 01 2005 18:55:50   schand
//Code Review Fix (IR-1928)
//
//   Rev 1.0   Feb 28 2005 16:23:06   passuied
//Initial revision.
//
//   Rev 1.7   Feb 02 2005 15:33:28   schand
//applied FxCop rules
//
//   Rev 1.6   Jan 28 2005 17:45:14   schand
//Added Dispose method to stop Mock listener
//
//   Rev 1.5   Jan 28 2005 11:38:46   schand
//Added mocklistener path is coming from property
//
//   Rev 1.4   Jan 21 2005 17:38:24   schand
//BuildRTTIError: code fixed for creating error message
//
//   Rev 1.3   Jan 21 2005 14:22:38   schand
//Code clean-up and comments has been added

using System;
using System.Xml;
using System.Xml.Schema ;  
using System.Reflection; 
using System.Configuration;
using System.Collections; 
using TransportDirect.Common.PropertyService.Properties ;   
using TransportDirect.UserPortal.DepartureBoardService ;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.Common.ServiceDiscovery;  
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// RTTIUtilities contains some helper function.
	/// </summary>
	public sealed class RTTIUtilities
	{
		private RTTIUtilities(){}
		
		#region "Public Static Members"
		/// <summary>
		/// Gets the attribute name
		/// </summary>
		/// <param name="attNameType">Attribute Type</param>
		/// <returns>Returns attribute name for the specified type</returns>
		public static string GetAttributeName(AttributeName attNameType)
		{
				string attributeName;
			
				switch(attNameType)
				{
					case AttributeName.ActualTimeOfArrival:
						attributeName = "ata";
						break;
					case AttributeName.ActualTimeOfDeparture:
						attributeName = "atd";
						break;
					case AttributeName.EstimatedTimeOfArrival:
						attributeName = "eta";
						break;
					case AttributeName.EstimatedTimeOfDeparture:
						attributeName = "etd";
						break;
					case AttributeName.OperatorCode:
						attributeName = "toc";
						break;
					case AttributeName.PublicTimeOfArrival :
						attributeName = "pta";
						break;
					case AttributeName.PublicTimeOfDeparture:
						attributeName = "ptd";
						break;
					case AttributeName.ServiceNumber:
						attributeName = "rid";
						break;
					case AttributeName.Cancelled: 
						attributeName = "can";
						break;
					case AttributeName.CancellationCode: 
						attributeName = "cc";
						break;						
					case AttributeName.CircularRoute: 
						attributeName = "cr";
						break;
					case AttributeName.Via : 
						attributeName = "via";
						break;
					case AttributeName.TrainDepartureDelayed: 
						attributeName = "deld";
						break;
					case AttributeName.TrainArrivalDelayed: 
						attributeName = "dela";
						break;
					case AttributeName.OverDueAtDeparture: 
						attributeName = "odetd";
						break;
					case AttributeName.OverDueAtArrival: 
						attributeName = "odeta";
						break;
					case AttributeName.StationAttributeByCRS: 
						attributeName = "crs";
						break;
					case AttributeName.RTTIFullTiploc:
						attributeName = "ftl";
						break;
					case AttributeName.LateRunningCode:
						attributeName = "lrc";
						break;
					case AttributeName.FalseDestination:
						attributeName = "fd";
						break;
					case AttributeName.RTTIErrorCode:
						attributeName = "code";
						break;
					case AttributeName.MessageId:
						attributeName = "msgId";
						break;
					default:
						attributeName = string.Empty;
						break;
				}

				return attributeName.ToString();
			}

	
		
		
		/// <summary>
		/// Gets the attribute value from the xml node 
		/// </summary>
		/// <param name="xmlNode"></param>
		/// <param name="attName">Attribute Type</param>
		/// <returns>Returns attribute value for the specified attribute name</returns>
		public static string GetAttributeValue(XmlNode xmlNode, AttributeName attName)
		{
			string attributeName; 
			XmlAttribute xmlAtt;  
			try
			{	// check for null 	
				if (attName.Equals(null))
					return string.Empty ;	
				if (xmlNode.Equals(null) )
					return string.Empty;

				// get attibute name
				attributeName = GetAttributeName(attName);

				// check for null 
				if (attributeName==null || attributeName.Length  == 0)
					return string.Empty;				

				xmlAtt = xmlNode.Attributes[attributeName];   

				if (xmlAtt == null)	
					return string.Empty ;				
				else
					return xmlAtt.Value.ToString();   				
			}
			catch(XmlException  xEx)
			{	
				// log the error 
				string outMsg;
				outMsg = "GetAttributeValue: Unable to return value for atrribute: " +  attName.ToString() + "ex: " + xEx.Message ; 
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose , outMsg));      
				// empty string back;
				return string.Empty;
			}
		}
		

		/// <summary>
		/// Returns boolean for the specified string. 
		/// </summary>
		/// <param name="valueToCheck"></param>
		/// <returns>If value is "true"  then true would be returned else false</returns>
		public static bool GetBooleanVal(string valueToCheck)
		{
				if (valueToCheck == null || valueToCheck.Length ==0)
					return false;				

				if (valueToCheck.ToLower() == "true"  ) 
					return true;			
				else
					return false;			
		}
		
		
		/// <summary>
		/// Gets the train prefix code from the database 
		/// </summary>
		/// <returns>Returns code prefix</returns>
		public static string GetPrefixForTrain
		{
			get {return Properties.Current[Keys.NaptanPrefix].ToString() ;}
		}

	
		/// <summary>
		/// Build error message and assigned it to DBSResult.
		/// </summary>
		/// <param name="errMessageType">Error Mesaage </param>
		/// <param name="trainResult">DBSResult to which messaged needs to assigned.</param>
		public static void BuildRTTIError(string errMessageType,  DBSResult trainResult)
		{	DBSMessage[] errMessages = new DBSMessage[1];
			DBSMessage errMessage = new DBSMessage(); 
						
			try
			{				
				errMessage.Code = GetErrorCodeForMessage(errMessageType);
				errMessage.Description = errMessageType ;				
				errMessages[0] =  errMessage;
				trainResult.Messages = errMessages ;
			}
			catch
			{		errMessage.Code = 0;
					errMessage.Description = errMessageType;
					errMessages[0] =  errMessage;
					trainResult.Messages = errMessages ;
			}
		}
		
		
		/// <summary>
		/// Returns error code for the error message type
		/// </summary>
		/// <param name="errMessageType">Error Message</param>
		/// <returns>Returns error code</returns>
		public static int GetErrorCodeForMessage(string errMessageType)
		{	int errCode = 0;			
				switch(errMessageType)
				{
					case Messages.RTTIRequestInValid:
						errCode = 200;						
						break;
					case Messages.RTTIRequestNoDataFound: 		
						errCode = 202;													
						break;	
					case Messages.RTTIResponseInValid:
						errCode = 201;						
						break;
					case Messages.RTTISchemaValidationFailed:
						errCode = 203;						
						break;
					case Messages.RTTIUnknownError :		
						errCode = 206;													
						break;
					case Messages.RTTIUnableToExtract :		
						errCode = 207;													
						break;						
					case Messages.RTTIUnableToExtractTripData :		
						errCode = 208;													
						break;	
					case Messages.RTTIUnableToExtractTrainData :		
						errCode = 209;													
						break;						
					default: 
						errCode = 205;
						break;
				}
				return errCode;
			
		}
		
		
		/// <summary>
		/// Gets element name for the specified request template type
		/// </summary>
		/// <param name="requestType">Request template type</param>
		/// <returns>Returns element name</returns>
		public static string GetResponseElement(TemplateRequestType requestType)
		{
			string responseElementName = string.Empty;
			try
			{
				switch(requestType)
				{
					case TemplateRequestType.StationRequestByCRS:
						responseElementName =  RTTIElementName.StationResponse  ;
						break;
					case TemplateRequestType.TripRequestByCRS:
						responseElementName =  RTTIElementName.TripResponse ;
						break;
					case TemplateRequestType.TrainRequestByRID:
						responseElementName =  RTTIElementName.TrainResponse;
						break;
					default:
						responseElementName =  string.Empty;
						break;
				}
				return responseElementName; 
			}
			catch(NullReferenceException nEx)
			{
				// log the error 
				string outMsg;
				outMsg = "GetResponseElement: Unable to return element name for reqType: " +  requestType.ToString() + "ex: " + nEx.Message; 
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, outMsg));      
				// empty string back;
				return string.Empty ;
			}
		}
		

		
		/// <summary>
		/// Gets top level element tag for the specified request template type
		/// </summary>
		/// <param name="requestType">Request template type</param>
		/// <returns>Returns element tag</returns>
		public static string GetResultTagName(TemplateRequestType requestType)
		{
				string sElementTagName = string.Empty;
			try
			{
				switch(requestType)
				{
					case TemplateRequestType.StationRequestByCRS:
						sElementTagName =  RTTIElementName.StationResult;
						break;
					case TemplateRequestType.TripRequestByCRS:
						sElementTagName = RTTIElementName.TripResult;
						break;
					case TemplateRequestType.TrainRequestByRID: 
						sElementTagName = RTTIElementName.TrainResponse ;
						break;
					default: 
						sElementTagName= string.Empty;
						break;
				}

				return sElementTagName;
					
			}
			catch(NullReferenceException  nEx)
			{	
				// log the error 
				string outMsg;
				outMsg = "GetResultTagName: Unable to return Tag name for reqType: " +  requestType.ToString() + "ex: " + nEx.Message; 
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, outMsg));      
				// empty string back;
				return string.Empty;
			}

		}


		/// <summary>
		/// Gets the xml string for the specified request template type
		/// </summary>
		/// <param name="requestType">Request template type</param>
		/// <param name="folderLocation">The file path</param>
		/// <returns>Xml string</returns>
		public static string GetXmlStringForFile(TemplateRequestType requestType, string folderLocation )
		{
			XmlDocument   doc = new XmlDocument(); 

			try
			{	
				switch(requestType)
				{
					case TemplateRequestType.StationRequestByCRS:
						folderLocation += "StationResponseByCRS.xml";
						break;
					case TemplateRequestType.TripRequestByCRS:
						folderLocation +=  "TripResponseByCRS.xml";
						break;
					case TemplateRequestType.TrainRequestByRID:
						folderLocation +=  "TrainResponseByRID.xml";
						break;
					case TemplateRequestType.ErrorResponse:
						folderLocation +=  "RTTIErrorResponse.xml";
						break;
				}
					  
				doc.Load(folderLocation); 
				XmlElement root = doc.DocumentElement ;					
				return root.OuterXml ;
			}
			catch(Exception ex )
			{
				throw ex;
			}
		}		

		#endregion

	}
}
