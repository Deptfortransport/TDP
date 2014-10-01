// *********************************************** 
// NAME                 : Keys.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 30/12/2004
// DESCRIPTION  : All used keys for Departure Board Service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Keys.cs-arc  $
//
//   Rev 1.1   Jan 04 2012 14:46:48   rbroddle
//Added new "RTTINetworkStreamTimeout" property and code to allow a configurable timeout to be defined for the NetworkStream object used to talk to RTTI. (both reads and writes)
//Resolution for 5777: Station Information page on TDP failed completely with the loss of RTTI.
//
//   Rev 1.0   Nov 08 2007 12:21:06   mturner
//Initial revision.
//
//   Rev 1.4   May 03 2006 15:35:04   COwczarek
//Add RealTimeRequired constant
//Resolution for 4062: Mobile DepartureBoards: Real Time Flag set to true
//
//   Rev 1.3   Jul 15 2005 13:38:54   NMoorhouse
//Additional keys for Mobile Bookmark
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.2   Jul 05 2005 11:02:00   NMoorhouse
//Code merge (Stream2560 -> Trunk)
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.1.1.1   Jul 04 2005 18:12:48   NMoorhouse
//Extra Keys for Bookmarks
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.1.1.0   Jun 23 2005 12:28:26   schand
//Added keys for MobileBookmark functionality.
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.1   Mar 14 2005 15:11:06   schand
//Changes for configurable switch between CJP and RTTI.
//Added TrainInfoFromCJP key.
//
//   Rev 1.0   Feb 28 2005 17:21:06   passuied
//Initial revision.
//
//   Rev 1.12   Feb 24 2005 14:19:54   passuied
//Changes for FxCop
//
//   Rev 1.11   Feb 16 2005 14:54:08   passuied
//Change in interface and behaviour of time Request
//
//possibility to plan in the past within configurable time window
//
//   Rev 1.10   Feb 11 2005 16:46:38   schand
//Added key for DepartureBoardService.RTTIManager.RetryCount, DepartureBoardService.RTTIManager.MilliSecondWaitTime
//
//   Rev 1.9   Jan 28 2005 10:42:34   schand
//Added key for Mocklistner path
//
//   Rev 1.8   Jan 19 2005 16:28:38   schand
//integration of RTTI + SE manager!
//
//   Rev 1.7   Jan 19 2005 14:34:44   schand
//Additional RTTI keys added
//
//   Rev 1.6   Jan 19 2005 10:21:08   schand
//Removed RTTIMockListener key
//
//   Rev 1.5   Jan 19 2005 09:49:30   schand
//Added key for for  mock listener
//
//   Rev 1.4   Jan 14 2005 18:56:32   passuied
//added new property keys.
//
//   Rev 1.3   Jan 11 2005 15:47:18   schand
//Added NaptanPrefix
//
//   Rev 1.2   Jan 11 2005 11:35:36   passuied
//renamed range key to be more generic
//
//   Rev 1.1   Jan 11 2005 11:13:30   passuied
//added new keys
//
//   Rev 1.0   Dec 30 2004 15:09:26   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService
{
	/// <summary>
	/// All used keys for Departure Board Service
	/// </summary>
	public class Keys
	{
		// .NET file trace listener path
		public const string DefaultLogPath		= "DefaultLogPath";

		// DepartureBoard service Generic keys

		/// <summary>
		/// Property key specifying the configurable time window in minutes within which a specific time requested in past
		/// is still considered as a request for today.
		/// </summary>
		public const string PastTimeWindow		= "DepartureBoardService.PastTimeWindow";
		/// <summary>
		/// Property key specifying the default range for results (number of results to return)
		/// </summary>
		public const string DefaultRangeKey		= "DepartureBoardService.StopEventManager.DefaultRange";

		// CJP specific property keys
		public const string CJPTimeOutKey		= "DepartureBoardService.StopEventManager.CJPTimeout";
		public const string DayStartHourKey		= "DepartureBoardService.StopEventManager.DateStartHour";

		// Properties key 
		public const string NaptanPrefix		= "FindA.NaptanPrefix.Rail";
		
		/// <summary>
		/// space delimited string with all TDCodeType to remove (e.g. "IATA Postcode")
		/// </summary>
		public const string CodeTypesToRemoveKey	= "DepartureBoardService.StopEventManager.CodeTypesToRemove";

		/// <summary>
		/// boolean that specifies whether to request all instances of a frequent bus/coach journey
		/// or just the first instance. true if first instance/False if all instances
		/// </summary>
		public const string FirstEventOnlyKey	= "DepartureBoardService.StopEventManager.FirstEventOnly";

		/// <summary>
		/// boolean that specifies whether to request each services in a FirstLastServiceRequest or just one
		/// </summary>
		public const string EachServiceKey	= "DepartureBoardService.StopEventManager.EachService";
		
		/// <summary>
		/// RTTI server address
		/// </summary>
		//public const string RTTIServerAddress = "DepartureBoardService.RTTIManager.SocketServer";
		
		/// <summary>
		/// RTTI Socket Port
		/// </summary>
		//public const string RTTISocketPort  = "DepartureBoardService.RTTIManager.SocketPort";

		
		/// <summary>
		/// The schema location for RTTI xml response.  
		/// </summary>
		public const string RTTISchemaLocation  = "DepartureBoardService.RTTIManager.SchemaLocation";
		

		/// <summary>
		/// The schema namespace RTTI xml response.  
		/// </summary>
		public const string RTTISchemaNameSpace  = "DepartureBoardService.RTTIManager.SchemaNamespace";

		
		/// <summary>
		/// The start hour(time) for the fisrt service of the day.  
		/// </summary>
		public const string RTTIFirstServiceHour  = "DepartureBoardService.RTTIManager.FirstServiceHour";
		

		/// <summary>
		///   The start hour(time) for the last service of the day  
		/// </summary>
		public const string RTTILastServiceHour  = "DepartureBoardService.RTTIManager.LastServiceHour";

		/// <summary>
		///   The time duration for the first service
		/// </summary>
		public const string RTTIFirstServiceDuration  = "DepartureBoardService.RTTIManager.FirstServiceDuration";

		/// <summary>
		///  The time duration for the last service
		/// </summary>
		public const string RTTILastServiceDuartion  = "DepartureBoardService.RTTIManager.LastServiceDuartion";

		/// <summary>
		///   The maximum allowed duration by RTTI
		/// </summary>
		public const string RTTIMaxDurationAllowed  = "DepartureBoardService.RTTIManager.MaxDurationAllowed";

		/// <summary>
		/// The default duartion for the RTTI request   
		/// </summary>
		//public const string RTTIDefaultDuration  = "DepartureBoardService.RTTIManager.DefaultDuration";
		
		
		/// <summary>
		/// The xml request template for station or stop   
		/// </summary>
		//public const string RTTIStationRequestByCRS  = "DepartureBoardService.RTTIManager.RequestTemplate.StationRequestByCRS";
		
		
		/// <summary>
		/// The xml request template for trip or journey 
		/// </summary>
		//public const string RTTITripRequestByCRS  = "DepartureBoardService.RTTIManager.RequestTemplate.TripRequestByCRS";
		
		/// <summary>
		/// The xml request template for train
		/// </summary>
		//public const string RTTITrainRequestByRID  = "DepartureBoardService.RTTIManager.RequestTemplate.TrainRequestByRID";
		
		/// <summary>
		/// The mock listener path if needed to start the mock listener
		/// </summary>
		public const string RTTIMockListnerPath  = "DepartureBoardService.RTTIManager.MockListenerPath";	
		
		/// <summary>
		/// The retry count if connection refused from server.
		/// </summary>
		//public const string RTTIRetryCount  = "DepartureBoardService.RTTIManager.RetryCount";	
		
		/// <summary>
		/// The wait time in millisecond before reteying socket connection to RTTI server
		/// </summary>
		//public const string RTTIRetryMilliSecondWaitTime  = "DepartureBoardService.RTTIManager.MilliSecondWaitTime";

        /// <summary>
        /// The timeout in milliseconds for socket connection network stream to RTTI server. If the read
        /// operation does not complete within the time specified by this property, 
        /// the read operation throws an IOException.
        /// </summary>
        //public const string RTTINetworkStreamTimeout = "DepartureBoardService.RTTIManager.NetworkStreamTimeout";	


		/// <summary>
		/// The config flag indicator for getting Train rsult from CJP or RTTI
		/// </summary>
		public const string GetTrainInfoFromCJP  = "DepartureBoardService.GetTrainInfoFromCJP";
        public const string RealTimeRequired = "DepartureBoardService.RealTimeRequired";
		public const string BookmarkUserName = "DepartureBoardService.MobileBookmark.UserName";		
		public const string BookmarkAccountName = "DepartureBoardService.MobileBookmark.AccountName";
		public const string BookmarkSenderName = "DepartureBoardService.MobileBookmark.BookmarkSenderName";
		public const string BookmarkSubjectHeader = "DepartureBoardService.MobileBookmark.BookmarkSubjectHeader";
		public const string BookmarkBodyMessage = "DepartureBoardService.MobileBookmark.BodyMessage";
		public const string BookmarkServiceUrl = "DepartureBoardService.MobileBookmark.BookmarkServiceUrl";
		public const string UKPhonePrefix = "DepartureBoardService.MobileBookmark.UKPhonePrefix";
		public const string BookmarkHasProxy = "DepartureBoardService.MobileBookmark.BookmarkHasProxy";
		public const string BookmarkProxyURL = "DepartureBoardService.MobileBookmark.BookmarkProxyURL";

		public const int DeviceType_SMS = 1;
		public const int DeviceType_WAPPush = 2;
		public const int DeviceType_Nokia = 3;


		

		
		
	}
}
