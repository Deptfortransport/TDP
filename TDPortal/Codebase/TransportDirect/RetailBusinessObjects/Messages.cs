// *********************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION			: Container for messages
// used by classes in the RBO project.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Messages.cs-arc  $
//
//   Rev 1.1   Jan 11 2009 18:03:06   mmodi
//Updated with message indicating an engine was not started because of the allow initialise flag
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:12   mturner
//Initial revision.
//
//   Rev 1.17   Feb 17 2006 14:47:38   aviitanen
//Merge from Del8.0 to 8.1
//
//   Rev 1.16   Nov 17 2005 19:26:12   build
//Fixed build 151
//
//   Rev 1.15   Nov 09 2005 12:31:48   build
//Automatically merged from branch for stream2818
//
//   Rev 1.14.1.1   Nov 02 2005 17:07:10   rhopkins
//Additional messages for tracing logs for multiple fare availability
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.14.1.0   Oct 18 2005 15:44:20   jgeorge
//Removed unnecessary message
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.14   Apr 20 2005 10:32:12   RPhilpott
//Add more thorough and robust error handling.
//Resolution for 2247: PT: error handling by Retail Business Objects
//
//   Rev 1.13   Dec 15 2003 15:19:14   geaton
//Updated exception message to include exception message field.
//
//   Rev 1.12   Dec 11 2003 16:31:04   geaton
//Added additonal exception data when logging engine failure details.
//
//   Rev 1.11   Nov 27 2003 21:46:08   geaton
//Added messages for housekeeping failure.
//
//   Rev 1.10   Nov 27 2003 14:02:34   geaton
//Added error message for use when housekeeping is initiated when housekeeping is already in progress.
//
//   Rev 1.9   Oct 29 2003 19:46:18   geaton
//Added messages.
//
//   Rev 1.8   Oct 28 2003 20:04:56   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.7   Oct 22 2003 09:19:16   geaton
//Added housekeeping support to business objects.
//
//   Rev 1.6   Oct 21 2003 15:22:52   geaton
//Changes to support business object timeout functionality.
//
//   Rev 1.5   Oct 17 2003 13:11:42   geaton
//Added exception messages.
//
//   Rev 1.4   Oct 17 2003 12:03:52   CHosegood
//Added unit test trace
//
//   Rev 1.3   Oct 17 2003 10:15:34   CHosegood
//Added trace messages
//
//   Rev 1.2   Oct 16 2003 10:42:10   geaton
//Added messages to support all pools.
//
//   Rev 1.1   Oct 15 2003 21:34:36   geaton
//Added messages to support exception handling.
//
//   Rev 1.0   Oct 15 2003 14:40:12   geaton
//Initial Revision

using System;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	
	public sealed class Messages
	{
		private Messages() 
		{
		}

		// Pool Messages
		public const string Init_PoolCreationFailed = "Failed to create retail business object pool: {0}";
		public const string Pool_SizeLessThanMinimum = "Pool containing engines of id [{0}] does not contain minimum number of engines [{1}]. Only [{2}] engines have been initialised successfully and are available for use by business objects."; 
		public const string Pool_ThreadNotificationFailed = "Pool failed to notify threads waiting on the pool lock monitor: [{0}]";
		public const string Pool_TimeoutReleaseFailed = "A business object release initiated by a timeout failed.";
		public const string Pool_ClientReleaseFailed = "A business object release initiated by a client failed.";
		public const string Pool_ClientReleasedNullObject = "A client attempted to release a null business object.";
		public const string Pool_ClientReleasedTimedOutEngine = "A client released a business object that uses an engine which is no longer allocated to it. Possible reasons: Engine timed out or business object is not being released back into the pool from which it was originally taken.";
		public const string Pool_ConstructionProperty = "Pool constructed using property value [{0}] held in key [{1}].";
		public const string Pool_PropertiesInvalid = "One or more pool properties are invalid: [{0}]";
		public const string Pool_LocalConfigFileNotFound = "Local configuration file [{0}] of pool engine/s is missing. This file must reside in current directory [{1}]. Typically this is the same directory as the engine dll/s.";
		public const string Pool_InvalidInputWhenParsingIni = "Invalid input found when parsing pool engine .ini file.";
		public const string Pool_ValueNotFoundInIni = "Value [{0}] not found in pool engine .ini file.";
		public const string Pool_KeyNotFoundInIni = "Key [{0}] not found in pool engine .ini file.";
		public const string Pool_FailedToReadIni = "Failure when reading pool .ini file [{0}]. Error:[{1}].";
		public const string Pool_LocalFlagIncorrect = "Local data flag in pool's local configuration file is set to the incorrect value. Value must be [{0}].";
		public const string Pool_InitialisationStarted = "Initialisation of pool containing engine/s of object id [{0}] started.";
		public const string Pool_InitialisationCompleted = "Initialisation of pool containing engines of object id [{0}] completed. Pool contains [{1}] engine/s. One engine will be allocated to each business object requested from pool.";
		public const string Pool_TimeoutReleaseStarted = "Release of timed-out pool engine/s of object id [{0}] has started.";
		public const string Pool_TimeoutReleaseCompleted = "Release of [{0}] timed-out pool engine/s of object id [{1}] has completed.";
		public const string Pool_TimeoutCheckingDisabled = "Engine time-out checking is disabled on pool containing objects of id [{0}].";
		public const string Pool_EngineAddFailed = "Failed when adding an engine to the pool.";
		public const string Pool_MaximumPoolSizeExceeded = "Attempted to add an engine to a pool that has already reached it's maximum pool size.";
		public const string Pool_HousekeepingRequested = "A request to initiate housekeeping was made on the pool containing engines of type [{0}].";
		public const string Pool_HousekeepingRequestFailed = "An unsuccessful request to initiate housekeeping was made on the pool containing engines of type [{0}]. Reason: [{1}]";
		public const string Pool_HousekeepingInitiated = "Housekeeping was successfully initated on the pool containing engines of type [{0}].";
		public const string Pool_HousekeepingFailedUpdatingSequence = "After performing housekeeping on an engine dll, an error occurred when attempting to determine the new data sequence number. A restart of the RBO Service is necessary to ensure all engines are using the same data sequence. Errors:[{0}]";
		public const string Pool_GetInstanceFailed = "Error occurred when requesting an business object from the pool containing objects of id [{0}]. Reason:[{1}]";
		public const string Pool_SignalReleaseFailed = "Error when signalling engine release to waiting clients. This may result in deadlock."; 
		public const string Pool_HousekeepingFeedIdInvalid = "Feed id passed in housekeeping request is invalid. Feed id length must be greater than zero.";
		public const string Pool_HousekeepingUpdatefileNotSpecified = "Housekeeping update file path cannot be determined. Reason: [{0}]";
		public const string Pool_HousekeepingUpdatefileNotFound = "Housekeeping update file cannot be found at path [{0}].";
		public const string Pool_HousekeepingTimerFailed = "Housekeeping timer could not be started.";
		public const string Pool_HousekeepingFailed = "Housekeeping failed for feed [{0}] and file [{1}].";
		public const string Pool_HousekeepingInProgress = "At attempt to initiate housekeeping was made while a previous housekeeping request was in progress.";
		public const string Pool_PassedCollectionEnd = "Passed the end of collection when getting next business object.";

		// Engine Messages
		public const string Engine_StopFailed = "Failed to stop an engine. Severity:[{0}] Code:[{1}]";
		public const string Engine_StopFeedback = "Engine was stopped with informational feedback. Severity:[{0}] Code:[{1}]";
		public const string Engine_StartFailed = "Failed to start an engine. Severity:[{0}] Code:[{1}]";
		public const string Engine_StartFeedback = "Engine was started with informational feedback. Severity:[{0}] Code:[{1}]";
        public const string Engine_StartNotDone = "Starting of pool engine [{0}] was not done because allowInitialse flag was false";
		public const string Engine_HousekeepingFailed = "Failed to housekeep an engine. Severity:[{0}] Code:[{1}]";
		public const string Engine_HousekeepingFeedback = "Engine was housekept with informational feedback. Severity:[{0}] Code:[{1}]";
		public const string Engine_RunStarted = "Engine Run started. ObjectId:[{0}] AllocationId:[{1}] SequenceNum:[{2}] Input Header:[{3}] Input Body:[{4}]"; 
		public const string Engine_RunCompleted = "Engine Run completed. ObjectId:[{0}] AllocationId:[{1}] SequenceNum:[{2}] Ouput Header:[{3}] Output Body:[{4}]"; 
		public const string Engine_DllCallFailed = "Call to underlying engine dll failed. Reason:[{0}] ObjectId:[{1}] AllocationId:[{2}] SequenceNum:[{3}] Input Header:[{4}] Input Body:[{5}]. Ensure the engine dll resides in the current directory [{6}].";
		public const string Engine_Timedout = "Engine timed out. ObjectId:[{0}] AllocationId:[{1}]";
		public const string Engine_StopFailedForHousekeep = "Failed to stop an engine to use for housekeeping. Severity:[{0}] Code:[{1}]";
		public const string Engine_StartFailedFollowingHousekeep = "Failed to start an engine following using it to perform housekeeping. Severity:[{0}] Code:[{1}]";
		

		// Business Object Messages
		public const string BusinessObject_EngineTimedOut = "Error calling business object - the engine allocated to the business object has timed out and cannot be used. This was detected because the allocation ids do not match. Business Object's allocation id:[{0}] Engines allocation id:[{1}]";
        public const string BOTicketDetails = "Name[{0}] Class [{1}] Railcard[{2}] Adult Fare[{3}] ChildFare[{4}] Quota[{5}]";
		public const string BOFareDetails = "Fare [{0}]";
		public const string BORailAvailabilityResult = "RailAvailabilityResult [{0}]";
		public const string BOFareAvailability = "FareAvailability - FareKey [{0}] DiscountCard [{1}] OutwardPlacesAvailable [{2}] InwardPlacesAvailable [{3}]";
		public const string BusinessObject_Error	= "Business object error - object = {0}, code = {1}{2}, description = {3}";
		public const string BusinessObject_Warning	= "Business object warning - object = {0}, code = {1}{2}, description = {3}";
       
		// Testing messages (Code that uses these messages will not be part of production build)
		public const string UnitTestMethodStarting = "Starting test [{0}]";
		public const string UnitTestMethodCompleted = "Completed test [{0}]";

		// Property validation messages
		public const string Property_HousekeepingCheckFrequencyInvalid = "Housekeeping check frequency [{0}] specified in key [{1}] is invalid. Value must be greater than zero and be expressed in milliseconds.";
		public const string Property_ValidatorKeyBad = "Retail Business Object Property Validator unable to validate key [{0}]";
		public const string Property_PoolSizeInvalid = "Pool Size [{0}] specified in key [{1}] is invalid. Value must be greater than zero and less than or equal the maxiumum pool size of [{2}]";
		public const string Property_TimeoutDurationInvalid = "Timeout duration [{0}] specified in key [{1}] is invalid. Value must be greater than zero.";
		public const string Property_TimeoutCheckFrequecyInvalid = "Timeout check frequency [{0}] specified in key [{1}] is invalid. Value must be greater than zero.";
		public const string Property_MinimumPoolSizeInvalid = "Minimum pool size [{0}] specified in key [{1}] is invalid. Value must be between 1 and the pool size [{2}].";

		// Validation messages - for housekeeping import
		public const string RBOImport_DuplicateEntryFound = "A duplicate value [{0}] was found when processing property [{1}]";
		public const string RBOImport_MissingParametersForServer = "One or both of the properties for a server is missing. The values read from the properties service are [{0}] = [{1}], [{2}] = [{3}]";
	
		// BO Error codes
		public const string ErrorSeverityCritical	 = "C";
		public const string ErrorSeverityError		 = "E";
		public const string ErrorSeverityWarning	 = "W";
		public const string ErrorSeverityInformation = "I";

	}
}
