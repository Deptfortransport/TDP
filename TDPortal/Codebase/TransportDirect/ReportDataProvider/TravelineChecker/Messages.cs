// *********************************************** 
// NAME                 : Messages.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 15/11/2004 
// DESCRIPTION  : Messages used within component.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TravelineChecker/Messages.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:40:46   mturner
//Initial revision.
//
//   Rev 1.1   Nov 17 2004 11:26:22   passuied
//First working version


using System;

namespace TransportDirect.ReportDataProvider.TravelineChecker
{
	/// <summary>
	/// Messages used within component.
	/// </summary>
	public abstract class Messages
	{
		public const string TravelineCheck_Failed = "Traveline Checker. Some traveline failed. These are : {0}";
		public const string Travelinecheck_Error = "An errror occurred while checking traveline [{0}]. Reason : {1}]";

		public const string TravelineCheck_Success = "All Travelines returned sucessfully.";
		public const string TravelineRequestError = "The client failed to contact the traveline [{0}]. Reason: {1}";


	}
}
