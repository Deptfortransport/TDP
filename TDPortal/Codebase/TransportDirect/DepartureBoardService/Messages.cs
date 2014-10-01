// *********************************************** 
// NAME                 : Messages.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 07/01/2005
// DESCRIPTION  : Messages used in DBS
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Messages.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:08   mturner
//Initial revision.
//
//   Rev 1.3   May 03 2007 12:27:44   mturner
//Added new message to report IncompleteNaptanInformation
//
//   Rev 1.2   Jul 29 2005 18:31:38   Schand
//The additional message for HandleFirstAndLast Services.
//Resolution for 2628: First Services are not working betweenWAT & FLE. Also unable to find first train (from WTN)
//Resolution for 2629: Unable to find last Train when filtered by arrival station
//
//   Rev 1.1   Jul 05 2005 11:02:50   NMoorhouse
//Code merge (Stream2560 -> Trunk)
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.0.1.1   Jun 23 2005 12:31:10   schand
//Added additional messages for MobileBookmark functionality.
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.0.1.0   Jun 20 2005 15:32:22   schand
//Added properties for MobileBookmark
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.0   Feb 28 2005 17:21:06   passuied
//Initial revision.
//
//   Rev 1.8   Feb 23 2005 15:18:26   passuied
//added extra message for exception during StopEventCompare
//
//   Rev 1.7   Jan 17 2005 14:48:44   passuied
//Latest code with Unit test OK!
//
//   Rev 1.6   Jan 14 2005 21:00:32   passuied
//Updates during unit tests. Back up before the week end
//
//   Rev 1.5   Jan 14 2005 18:55:26   passuied
//added messages for validation. Back up
//
//   Rev 1.4   Jan 14 2005 15:56:06   schand
//Added RTTI error messages
//
//   Rev 1.3   Jan 14 2005 14:36:52   passuied
//to be updated by pat
//
//   Rev 1.2   Jan 14 2005 10:21:12   passuied
//changes in interface
//
//   Rev 1.1   Jan 11 2005 13:40:36   passuied
//backed up version
//
//   Rev 1.0   Jan 07 2005 16:25:08   passuied
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService
{
	/// <summary>
	/// Messages used in DBS
	/// </summary>
	public class Messages
	{

		// User Request Validation Messages
		public const string RequestBadFormat = "Failed to process the request. The request is invalid. Reasons: {0}";
		public const string IncompleteCrsInfo = "The information provided with submitted Crs code [{0}] was incomplete.";
		public const string IncompleteSmsInfo = "The information provided with submitted Sms code [{0}] was incomplete.";
		public const string IncompleteNaptanInfo = "The information provided with submitted NaPTAN code [{0}] was incomplete.";
		public const string RejectedCodeType = "The submitted code [{0}] of code type [{1}] has been rejected. This code type is not accepted yet.";
		public const string RejectedCodeTypeInFetching = "No accepted codes were left after fetching information from Code service.";
		public const string FailedFetchingInfo = "Failed to fetch needed info for submitted code [{0}]";
		public const string FetchingMissingCode = "Failed to fetch needed info. Empty code";
		public const string FailureConsistencyCheck = "Failure during consistency check for submitted code [{0}] and code [{1}]";
		public const string TimeRequestInvalid = "Having a requested time type = Time, Hour is not within [0-23] AND/OR Minute is not within [0-59]";
		public const string TimeRequestNull = "The submitted DBSTimeRequest object was null";
		public const string EmptyLocationArray = "Internal error. An empty DBSLocation array was passed to DBSValidation class";
		public const string NullLocationRequest = "At least one of the submitted DBSLocation objects was null";
		public const string CodeDetailArgumentException = "Impossible to compare these 2 objects as 2 TDCodeDetails objects. Argument Exception!";
		public const string StopEventArgumentException = "Impossible to compare these 2 objects as 2 DBSStopEvents objects. Argument Exception!";

		// General messages for logging
		public const string MissingProperty = "The property [{0}] was not added to the database. A default value was used instead. ";
		public const string FailedCodeGaz = "The access to the Code Gazetteer failed and has thrown an exception. Reason: {0}";

		// StopEvent Manager messages
		public const string CJPTimeout = "The request to the CJP with requestId[{0}] timed out. ";
		public const string CJPReturnedMessages = "The CJP returned with the following messages:{0}";
		public const string CJPMessage = "CJP Message id=[{0}] - Description=[{1}]";

		public const string CallCJPException = "An exception occurred while calling the CJP for request id[{0}]. Reason:{1}";
		public const string CallCJPNull = "An exception occurred while accessing the CJP for request id[{0}]. The returned result is null";

		// RTTI Manager errors
		public const string RTTIRequestInValid = "InValid RTTI Request Found.";
		public const string RTTIResponseInValid = "InValid RTTI Response Found.";
		public const string RTTIRequestNoDataFound = "No data found for the given RTTI request";
		public const string RTTISchemaValidationFailed = "RTTI Schema validation failed.";
		public const string RTTIServiceUnavailable = "RTTI service is not able to respond to the request.";
		public const string RTTIGeneralError = "RTTI general error occurred.";
		public const string RTTIUnknownError = "Unknown error encountered from RTTI.";
		public const string RTTIUnableToExtract = "Unable to extract RTTI data.";
		public const string RTTIUnableToExtractTripData = "Unable to extract RTTI data for trip/station response.";
		public const string RTTIUnableToExtractTrainData = "Unable to extract RTTI data for train response.";
		public const string RTTIUnableToExtractFirstOrLastServiceData = "Unable to extract RTTI First or Last service data for train response.";


		// bookmark errors
		public const string BookmarkInValidRequest = "Please check the input parameter as the given request has failed to send the bookmark";
		public const string BookmarkInValidUserName = "The username you have specified is not correct";
		public const string BookmarkInValidRule = "Please make sure that given MSISDN is correct and URL length should not exceed the specified limit.";
		public const string BookmarkGeneralError= "General error has occured.";
		public const string BookmarkFailedInfo  = "The following bookmark: {0}  has failed for recipient {1} using mode: {2}";
		public const string BookmarkSentInfo  = "The following bookmark: {0}  has been sent successfully to recipient {1} using mode: {2}";





		


	}
}
