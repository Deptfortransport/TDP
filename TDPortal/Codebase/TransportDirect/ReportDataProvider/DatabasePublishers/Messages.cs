// *********************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 6/10/2003 
// DESCRIPTION  : Contains messages that are
// used in the custom database publishers.
// ************************************************ 

using System;

namespace TransportDirect.ReportDataProvider.DatabasePublishers
{
	/// <summary>
	/// Summary description for Messages.
	/// </summary>
	public class Messages
	{
		// TDException messages
		public const string SQLHelperError = "SQL Helper error when excuting stored procedure [{0}]. Message: [{1}]";
		public const string SQLHelperTypeError = "SQL Helper Type error when excuting stored procedure [{0}]. Message: [{1}]";
		public const string SQLHelperInsertFailed = "SQL Helper failed to insert the expected number of rows [{0}] when excuting stored procedure [{1}]. Return code of [{2}] was returned.";
		public const string EventPublishFailure = "Failed to publish event of type [{0}] to database. Reason: [{1}]";
		public const string SqlDateTimeOverflow = "Failure before call to stored procedure [{0}] - The event :{1} has a DateTime that is not compatible with SqlDateTime";
		public const string UnsupportedEventType = "Unsupported event type [{0}] found when publishing to database using database publisher [{1}].";
		public const string ConstructorFailed = "Database Publisher constructor failed with errors: {0}"; 
		public const string FailedRetrievingFieldLengths = "An error occurred when attempting to retrieve field lengths: {0}";
		public const string TruncatedField = "The following fields have been truncated before writing an operational event: [{0}]. The original operational event follows: [{1}]";
		static Messages()
		{}
	}
}
