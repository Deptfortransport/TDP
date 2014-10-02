// *********************************************** 
// NAME                 : IPageTransferDataCache.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : Interface for Page Transfer
// Data Cache.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Interfaces/IPageTransferDataCache.cs-arc  $ 
//
//   Rev 1.1   May 01 2008 17:28:04   mmodi
//Updated with PageGroup methods
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.0   Nov 08 2007 12:47:50   mturner
//Initial revision.
//
//   Rev 1.5   Jul 15 2004 09:50:44   jgeorge
//Removal of JourneyParametersType from screenflow
//
//   Rev 1.4   Jun 29 2004 15:33:32   esevern
//added GetJourneyParameterType from IPageController
//
//   Rev 1.3   Apr 23 2004 14:36:34   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.2   Aug 07 2003 13:54:14   kcheung
//Set CLSComplaint to true
//
//   Rev 1.1   Jul 23 2003 12:28:36   kcheung
//Updated after code review comments

using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.ScreenFlow
{
	/// <summary>
	/// Summary description for IPageTransferDataCache.
	/// </summary>
	public interface IPageTransferDataCache
	{
		/// <summary>
		/// Returns the PageTransferDetails object for the given pageId.
		/// </summary>
		PageTransferDetails GetPageTransferDetails(PageId pageId);

        /// <summary>
        /// Returns a PageGroupDetails array for the selected PageGroup
        /// </summary>
        PageGroupDetails[] GetPageGroupDetails(PageGroup pageGroup);

        /// <summary>
        /// Returns all PageId's for the selected PageGroup
        /// </summary>
        PageId[] GetPageIdsForGroup(PageGroup pageGroup);

		/// <summary>
		/// Returns the PageId for the given transition event.
		/// </summary>
		PageId GetPageEvent(TransitionEvent transitionEvent);

	}
}
