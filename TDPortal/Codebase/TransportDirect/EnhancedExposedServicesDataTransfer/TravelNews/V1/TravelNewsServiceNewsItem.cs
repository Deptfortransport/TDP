// *********************************************** 
// NAME                 : TravelNewsServiceNewsItem.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/01/2006 
// DESCRIPTION  		: DTO class implementation of TravelNewsItem. For Information refer to TravelNewsItem.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/TravelNews/V1/TravelNewsServiceNewsItem.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:52   mturner
//Initial revision.
//
//   Rev 1.2   Jan 23 2006 19:33:32   schand
//FxCop review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 20 2006 19:38:44   schand
//Added comment and serialisation attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111


using System;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;    
namespace TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1
{
	/// <summary>
	/// DTO class implementation of TravelNewsItem. For Information refer to TravelNewsItem.
	/// </summary>
	[System.Serializable]
	public class TravelNewsServiceNewsItem
	{
		#region Private members
		private string uid;   
		private TravelNewsServiceSeverityLevel severityLevel;
		private string publicTransportOperator;              
		private  string regionalOperator;                            
		private  string modeOfTransport;                      
		private  string regions;                             
		private  string location;                            
		private  string regionsLocation;                     
		private  string incidentType;                        
		private  string headlineText;                        
		private  string detailText;                          
		private  string incidentStatus;                      
		private  OSGridReference gridReference;                                		
		private  DateTime reportedDateTime;                  
		private  DateTime startDateTime;                     
		private  int minutesFromNow;                      
		private  DateTime lastModifiedDateTime;              
		private  DateTime clearedDateTime;                   
		private  DateTime expiryDateTime;
		#endregion

		#region Constructor
		public TravelNewsServiceNewsItem()
		{
			uid = string.Empty;
			publicTransportOperator = string.Empty; 
			regionalOperator = string.Empty; 
			modeOfTransport = string.Empty; 
			regions = string.Empty; 
			location = string.Empty; 
			regionsLocation = string.Empty; 
			incidentType = string.Empty; 
			headlineText = string.Empty; 
			detailText = string.Empty; 
			incidentStatus = string.Empty;
		}
		#endregion

		#region Public properties

		/// <summary>
		/// Read-Write property for 
		/// </summary>
		public string Uid
		{
			get {return uid;}
			set {uid=value;}
		}
		
		/// <summary>
		/// Read-Write property for 
		/// </summary>
		public TravelNewsServiceSeverityLevel SeverityLevel
		{
			get {return severityLevel;}
			set {severityLevel=value;}
		}

		/// <summary>
		/// Read-Write property for PublicTransportOperator
		/// </summary>
		public string PublicTransportOperator
		{
			get {return publicTransportOperator;}
			set {publicTransportOperator=value;}
		}  
		
		/// <summary>
		/// Read-Write property for RegionalOperator
		/// </summary>
		public  string RegionalOperator
		{
			get {return regionalOperator;}
			set {regionalOperator=value;}
		}
                
		/// <summary>
		/// Read-Write property for ModeOfTransport
		/// </summary>
		public  string ModeOfTransport
		{
			get {return modeOfTransport;}
			set {modeOfTransport=value;}
		}          
		
		/// <summary>
		/// Read-Write property for Regions
		/// </summary>
		public  string Regions
		{
			get {return regions;}
			set {regions=value;}
		}                 
		
		/// <summary>
		/// Read-Write property for Location
		/// </summary>
		public  string Location
		{
			get {return location;}
			set {location=value;}
		}                
		
		/// <summary>
		/// Read-Write property for RegionsLocation
		/// </summary>
		public  string RegionsLocation
		{
			get {return regionsLocation;}
			set {regionsLocation=value;}
		}         
		
		/// <summary>
		/// Read-Write property for IncidentType
		/// </summary>
		public  string IncidentType
		{
			get {return incidentType;}
			set {incidentType=value;}
		}            
		
		/// <summary>
		/// Read-Write property for HeadlineText
		/// </summary>
		public  string HeadlineText
		{
			get {return headlineText;}
			set {headlineText=value;}
		}            
		
		/// <summary>
		/// Read-Write property for DetailText
		/// </summary>
		public  string DetailText
		{
			get {return detailText;}
			set {detailText=value;}
		}              
		
		/// <summary>
		/// Read-Write property for IncidentStatus
		/// </summary>
		public  string IncidentStatus
		{
			get {return incidentStatus;}
			set {incidentStatus=value;}
		}          
		
		/// <summary>
		/// Read-Write property for GridReference
		/// </summary>
		public  OSGridReference GridReference
		{
			get {return gridReference;}
			set {gridReference=value;}
		}                    
		              
		/// <summary>
		/// Read-Write property for ReportedDateTime
		/// </summary>
		public  DateTime ReportedDateTime
		{
			get {return reportedDateTime;}
			set {reportedDateTime=value;}
		}      
		
		/// <summary>
		/// Read-Write property for StartDateTime
		/// </summary>
		public  DateTime StartDateTime
		{
			get {return startDateTime;}
			set {startDateTime=value;}
		}         
		
		/// <summary>
		/// Read-Write property for MinutesFromNow
		/// </summary>
		public  int MinutesFromNow
		{
			get {return minutesFromNow;}
			set {minutesFromNow=value;}
		}          
		
		/// <summary>
		/// Read-Write property for LastModifiedDateTime
		/// </summary>
		public  DateTime LastModifiedDateTime
		{
			get {return lastModifiedDateTime;}
			set {lastModifiedDateTime=value;}
		}  
		
		/// <summary>
		/// Read-Write property for ClearedDateTime
		/// </summary>
		public  DateTime ClearedDateTime
		{
			get {return clearedDateTime;}
			set {clearedDateTime=value;}
		}       
		
		/// <summary>
		/// Read-Write property for ExpiryDateTime
		/// </summary>
		public  DateTime ExpiryDateTime
		{
			get {return expiryDateTime;}
			set {expiryDateTime=value;}
		}
		#endregion


	}// END CLASS DEFINITION TravelNewsServiceNewsItem

} // TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1
