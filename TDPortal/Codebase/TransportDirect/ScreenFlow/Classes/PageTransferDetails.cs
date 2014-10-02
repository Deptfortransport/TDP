// *********************************************** 
// NAME                 : PageTransferDetails.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : A class that holds all the data
// required during page transfers.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Classes/PageTransferDetails.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:47:48   mturner
//Initial revision.
//
//   Rev 1.9   Jul 15 2004 09:50:38   jgeorge
//Removal of JourneyParametersType from screenflow
//
//   Rev 1.8   Jun 29 2004 15:31:12   esevern
//moved conversion of journey parameter type from xml attribute to page transfer details initialisation
//
//   Rev 1.7   Jun 29 2004 13:45:18   esevern
//added JourneyParameterType attribute required for additional safe page redirection processing
//
//   Rev 1.6   May 20 2004 14:48:58   rgreenwood
//Back Button Enhancement:
//Added BookmarkValidJourneyRedirect method which returns PageID for if a page has this optional attribute in the PageTransferDetails.xml file.
//
//   Rev 1.5   Apr 23 2004 14:36:42   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.4   Sep 16 2003 14:29:54   jcotton
//Removed redundant, commented out code.
//
//   Rev 1.3   Aug 15 2003 14:36:52   passuied
//Update after design change
//
//   Rev 1.2   Aug 07 2003 13:53:52   kcheung
//Set CLSComplaint to true
//
//   Rev 1.1   Jul 23 2003 12:28:02   kcheung
//Updated after code review comments

using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.ScreenFlow
{
	/// <summary>
	/// Holds all the data required during page transfers.
	/// </summary>
	public class PageTransferDetails
	{
		private PageId pageId;
		private string pageUrl;
		private PageId bookmarkRedirect;
		private bool boolSpecificStateClass;
		private PageId bookmarkValidJourneyRedirect;


		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="pageId">Id of this page.</param>
		/// <param name="pageUrl">Url of the page.</param>
		/// <param name="bookmarkRedirect">Id of the page to redirect to
		/// if this page cannot be bookmarked.</param>
		/// <param name="stateClassName">Name of the state management class
		/// associated with this page.</param>
		
		public PageTransferDetails(	PageId pageId, string pageUrl, PageId bookmarkRedirect, 
			bool boolsSpecificStateClass, 
			PageId bookmarkValidJourneyRedirect)
		{
			this.pageId			= pageId;
			this.pageUrl		= pageUrl;
			this.bookmarkRedirect = bookmarkRedirect;
			this.boolSpecificStateClass = boolsSpecificStateClass;
			//RGreenwood: Added for Back Button enhancement
			this.bookmarkValidJourneyRedirect = bookmarkValidJourneyRedirect;
		}

		/// <summary>
		/// Read Property. Returns the id of the page.
		/// </summary>
		public PageId PageId
		{
			get { return pageId; }
		}

		/// <summary>
		/// Read Property. Returns the Url of the page.
		/// </summary>
		public string PageUrl
		{
			get { return pageUrl; }
		}

		/// <summary>
		/// Read Property. Returns the Id of the page to redirect to, even
		/// the page is being accessed directly.
		/// </summary>
		public PageId BookmarkRedirect
		{
			get { return bookmarkRedirect; }
		}

		/// <summary>
		/// PAssuied : added 
		/// Read Property.
		/// Returns if the stateClass is specific (otherwise default)
		/// </summary>
		public bool SpecificStateClass
		{
			get { return boolSpecificStateClass; }
		}

		/// <summary>
		/// RGreenwood added:
		/// Read Property for Back Button enhancement.
		/// Returns the PageId of the page to redirect to, if valid journey data is held
		/// </summary>
		public PageId BookmarkValidJourneyRedirect
		{
			get { return bookmarkValidJourneyRedirect; }
		}
	}
}
