// *********************************************** 
// NAME			: Messages.cs
// AUTHOR		: James Broome
// DATE CREATED	: 26/01/2005
// DESCRIPTION	: String constants used througout the console application
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AvailabilityDataMaintenance/Messages.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:52   mturner
//Initial revision.
//
//   Rev 1.2   Mar 21 2005 10:54:00   jbroome
//Minor updates after code review
//
//   Rev 1.1   Feb 17 2005 14:49:36   jbroome
//Removed unnecessary messages
//Resolution for 1923: DEV Code Review : Availability Estimator
//
//   Rev 1.0   Feb 08 2005 10:38:18   jbroome
//Initial revision.

using System;

namespace TransportDirect.UserPortal.AvailabilityDataMaintenance
{
	/// <summary>
	/// Message constants used within project.
	/// </summary>
	public class Messages
	{
		public const string Init_DotNETTraceListenerFailed = "Failed to initialise a default .NET trace listener. Reason:[{0}].";
		public const string Init_InitialisationStarted = "Initialisation of Web Log Reader started.";
		public const string Init_Completed = "Initialisation of Web Log Reader completed successfully.";
		public const string Init_TDServiceAddFailed = "Failed to add a TD service to the cache: [{0}].";
		public const string Init_TDTraceListenerFailed = "Failed to initialise the TD Trace Listener class: {0}";
		public const string Init_UnknownPropertyKey = "Attempt to validate unknown property key: [{0}].";
		public const string Util_Failed = "Utility failed. Reason:[{0}] Id:[{1}]";

		public const string HelpMessage =	"\n/arc_hist	Archives the Availability History data\n" + 
											"/del_unav	Deletes historic Unavailable Products data\n" + 
											"/imp_prof	Imports the Product Profile data from IFxxx\n" +
											"/exp_prof	Exports the Product Profile data";
		public const string NoArgs = "You must include one of the following arguments:\n";
		public const string InvalidArgs = "Invalid argument specified.\n";
		public const string NoRecords = "No records found to archive\n";
       }
}
