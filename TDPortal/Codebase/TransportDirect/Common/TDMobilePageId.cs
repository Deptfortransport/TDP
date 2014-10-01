// *********************************************** 
// NAME                 : TDMobilePageId.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 03/03/2005 
// DESCRIPTION  		: Enumeration that holds all mobile pages. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TDMobilePageId.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:19:06   mturner
//Initial revision.
//
//   Rev 1.2   Jul 25 2005 15:17:40   Schand
//Added some new pages to enum.
//Resolution for 2618: Del 7 Mobile Bookmark results for now
//
//   Rev 1.1   Mar 08 2005 16:20:54   schand
//Enum page conataining Mobile page type
//
//   Rev 1.0   Mar 07 2005 11:41:02   schand
//Initial revision.

using System;

namespace TransportDirect.Common
{
	/// <summary>
	/// Enumeration that holds all mobile pages.
	/// </summary>
	public enum TDMobilePageId
	{	 MobileUnknown, // For any other page which is not listed here 
		 MobileCodeFinder,
		 MobileHome,
		 MobileLogin,
		 MobileNews,
		 MobileSessionExpired,
		 MobileError,
		 MobileNavigation,
		 MobileNewsDetails,
		 MobileStopPage,
		 MobileTaxiDetails

	}
}
