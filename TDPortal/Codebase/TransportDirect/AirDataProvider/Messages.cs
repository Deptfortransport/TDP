// *********************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 22/09/2004
// DESCRIPTION  : Staic class that defines messages
// used by the AirDataProvider
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Messages.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:18:24   mturner
//Initial revision.
//
//   Rev 1.0   Sep 29 2004 12:44:24   jgeorge
//Initial revision.
using System;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Summary description for Messages.
	/// </summary>
	public sealed class Messages
	{
		/// <summary>
		/// Private constructor to ensure no public constructor is created by the compiler
		/// </summary>
		private Messages()
		{ }

		public const string TTBOImportStart = "TTBO update begun for feed [{0}] at time [{1}]";
		public const string TTBOImportFinishAllSuccess = "[{0}] out of [{1}] TTBO updates successful at time [{2}]";
		public const string TTBOImportFinishFailure = "TTBO update failed with error code [{0}]. [{1}] updates attempted, [{2}] updates were successful, [{3}] updates were not attempted at [{4}]";

		public const string TTBOImportUpdateFailed = "Updating business object failed for the following reason: [{0}]";
		public const string TTBOImportCommandSuccess = "TTBO update successful for [{0}]";
		public const string TTBOImportCommandFailure = "TTBO update failed for [{0}]. The error code was [{1}]";

		public const string TTBODuplicateValueInProperty = "A duplicate value [{0}] was found when processing property [{1}]";
		public const string TTBOImportUnexpectedFeedName = "The supplied feedname [{0}] is not supported by this import task";
		public const string TTBOImportNoServerDetails = "There are no servers configured for the supplied feed name [{0}]";
		public const string TTBOImportMissingParametersForServer = "One or both of the properties for a server is missing. The values read from the properties service are [{0}] = [{1}], [{2}] = [{3}]";

	}
}
