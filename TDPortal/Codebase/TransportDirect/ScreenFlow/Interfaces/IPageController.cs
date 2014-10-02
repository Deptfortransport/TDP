// *********************************************** 
// NAME                 : IPageController.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 08/07/2003 
// DESCRIPTION  : Interface for Page Controller.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Interfaces/IPageController.cs-arc  $ 
//
//   Rev 1.1   May 01 2008 17:28:04   mmodi
//Updated with PageGroup methods
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.0   Nov 08 2007 12:47:50   mturner
//Initial revision.
//
//   Rev 1.5   Jun 29 2004 15:33:00   esevern
//moved GetJourneyParameterType to PageTransferDataCache
//
//   Rev 1.4   Jun 29 2004 13:39:10   esevern
//added GetJourneyParameterType 
//
//   Rev 1.3   Apr 23 2004 14:36:36   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.2   Aug 07 2003 13:54:12   kcheung
//Set CLSComplaint to true
//
//   Rev 1.1   Jul 23 2003 12:28:32   kcheung
//Updated after code review comments

using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.ScreenFlow
{
	/// <summary>
	/// Interface for PageController.
	/// </summary>
	public interface IPageController
	{
		/// <summary>
		/// Returns the Id of the next page.
		/// </summary>
		PageId GetNextPageId(PageId pageId);
		
		/// <summary>
		/// Returns the PageTransferDetails object associated with the pageId.
		/// </summary>
		PageTransferDetails GetPageTransferDetails(PageId pageId);
		
		/// <summary>
		/// Returns the Id of the next page by performing a validation
		/// of the current page.
		/// </summary>
		PageId ValidatePageTransition(PageId pageId);

        /// <summary>
        /// Returns a PageGroupDetails array for the selected PageGroup
        /// </summary>
        PageGroupDetails[] GetPageGroupDetails(PageGroup pageGroup);

        /// <summary>
        /// Returns all PageId's for the selected PageGroup
        /// </summary>
        PageId[] GetPageIdsForGroup(PageGroup pageGroup);

		/// <summary>
		/// Read/Write property
		/// </summary>
		IPageTransferDataCache PageTransferDataCache{get; set;}

	}
}

