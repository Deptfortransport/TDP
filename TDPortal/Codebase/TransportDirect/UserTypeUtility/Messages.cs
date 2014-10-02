// ************************************************************ 
// NAME                 : Messages
// AUTHOR               : Jonathan George
// DATE CREATED         : 18/08/2003 
// DESCRIPTION			: Messages for the UserTypeUtility
// ************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/UserTypeUtility/Messages.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:51:02   mturner
//Initial revision.
//
//   Rev 1.1   Jul 06 2004 15:46:00   jgeorge
//Updated commenting

using System;

namespace TransportDirect.UserPortal.SessionManager.UserTypeUtility
{
	public class Messages
	{
		public const string Init_DotNETTraceListenerFailed = "Failed to initialise a default .NET trace listener. Reason:[{0}].";
		public const string Init_InitialisationStarted = "Initialisation of Web Log Reader started.";
		public const string Init_Completed = "Initialisation of Web Log Reader completed successfully.";
		public const string Init_TDServiceAddFailed = "Failed to add a TD service to the cache: [{0}].";
		public const string Init_TDTraceListenerFailed = "Failed to initialise the TD Trace Listener class: {0}";
		public const string Init_Usage = "Usage: usertypeutility [/help|/test|/set:username:typecode|/get:username]\n       Valid values for typecode are [{0}].";
		public const string Init_UnknownPropertyKey = "Attempt to validate unknown property key: [{0}].";

		public const string Util_Failed = "Utility failed. Reason:[{0}] Id:[{1}]";
		public const string Util_TestSucceeded = "Utility was run in test mode and succeeded.";
		public const string Util_InvalidArg = "Invalid argument/s passed to utiltity.";
		public const string Util_Completed = "Utility completed successfully.";
		public const string Util_InvalidType = "The value [{0}] is not a valid user type. Use the /help switch to see a list of valid types.";

		public const string User_CurrentValue = "The current user type of user [{0}] is [{1}].";
		public const string User_NotFound = "The user [{0}] was not found or could not be loaded.";
		public const string User_NewValue = "The new user type of user [{0}] is [{1}].";
	}
}

