// *********************************************************** 
// NAME                 : UserFeedbackEventCategory.cs 
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 16/07/2004
// DESCRIPTION  : Defines categories for user feedback events
// *********************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TDPCustomEvents/UserFeedbackEventCategory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:36   mturner
//Initial revision.
//
//   Rev 1.0   Jul 20 2004 15:30:06   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.ReportDataProvider.TDPCustomEvents
{

	/// <summary>
	/// Enumeration containing classifiers for UserFeedbackEvents
	/// </summary>
	public enum UserFeedbackEventCategory : int
	{
		UserFeedbackSiteProblem,
		UserFeedbackIncorrectInformation,
		UserFeedbackSuggestion
		
	}	
}
