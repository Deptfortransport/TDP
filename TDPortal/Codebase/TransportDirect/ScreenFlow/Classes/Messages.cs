// *********************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : Contains string messages
// used by the exception handling in ScreenFlow.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Classes/Messages.cs-arc  $ 
//
//   Rev 1.1   May 01 2008 17:07:32   mmodi
//Updated for PageGrouping.xml
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.0   Nov 08 2007 12:47:46   mturner
//Initial revision.
//
//   Rev 1.6   Jul 15 2004 09:50:36   jgeorge
//Removal of JourneyParametersType from screenflow
//
//   Rev 1.5   Jul 09 2004 13:34:48   jgeorge
//IR1143
//
//   Rev 1.4   Jun 29 2004 13:45:16   esevern
//added JourneyParameterType attribute required for additional safe page redirection processing
//
//   Rev 1.3   Apr 23 2004 14:36:38   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.2   Aug 07 2003 13:53:58   kcheung
//Set CLSComplaint to true
//
//   Rev 1.1   Jul 23 2003 12:28:08   kcheung
//Updated after code review comments

using System;

namespace TransportDirect.UserPortal.ScreenFlow
{
	/// <summary>
	/// Messages used when a ScreenFlowException is thrown.
	/// </summary>
	public class Messages
	{
		public const string PageTransferDataCacheConstructor =
			"PageTransferDataCacheConstructor constructor.";

		public const string PageTransferDataCacheProperties =
			"An exception occured while trying to load the properties.";

		public const string PageTransferDataValidationError =
			"Exception when attempting to validate the PageTransferDetails, PageTransitionEvents, and PageGrouping. ";

		public const string PageTransferDataCacheEmptyArray =
			PageTransferDataValidationError + "{0}";

		public const string PageTransferDataCacheInconsistentEnumXml =
			PageTransferDataValidationError + "Inconsistent enumeration type and Xml file. {0}";

		public const string PageTransferDataCacheInvalidEntry =
			PageTransferDataValidationError + "There was an invalid attribute in a PageTransferDetails object. {0}";

		public const string PageTransferDataCacheInvalidXml =
			PageTransferDataValidationError + "The Xml file was invalid. {0}";

		public const string PageTransferDataCachePageIdDoesNotExist =
			PageTransferDataValidationError + "A PageId for a PageTransitionEvent is invalid. {0}";

        public const string PageTransferDataCachePageIdDoesNotExistForGroup =
            PageTransferDataValidationError + "A PageId for a PageGrouping is invalid. {0}";

		public const string PageTransferDataCacheStateClassDoesNotExist =
			PageTransferDataValidationError + "A state management class does not exist. {0}";
			
		public const string EnumConversionFailed =
			PageTransferDataValidationError + "A conversion to an enumeration has failed. The values that may " +
			"have caused the exception are:\n{0}";

		public const string LoadingXmlFailed =
			"An exception occured whilst trying to load the Xml file, possibly because " +
			"an expected PageId or TransitionEvent did not exist in the Xml file. " +
			"The values that may have caused the exception are:\n{0}";

		public const string GetNextPageIdException =
			"An exception occured when attempting to get the pageId of the next page. " +
			"The values that may have caused the exception are:\n{0}";

		public const string ValidatePageTransitionException =
			"An exception occured when attempting to validate a pageId. " +
			"The values that may have caused the exception are:\n{0}";

		public const string DefaultState =
			"An exception occured in the Default State Class." +
			"The values that may have caused the exception are:\n{0}";
	}
}
