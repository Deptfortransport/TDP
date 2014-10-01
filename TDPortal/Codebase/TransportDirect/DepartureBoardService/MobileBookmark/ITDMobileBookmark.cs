// *********************************************** 
// NAME                 : ITDMobileBookmark.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/06/2005 
// DESCRIPTION  	    : The interface class for sending the bookmark using Kizoon web service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/MobileBookmark/ITDMobileBookmark.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:32   mturner
//Initial revision.
//
//   Rev 1.2   Jul 15 2005 13:39:52   NMoorhouse
//Changes to run Bookmark Service from Remote (App) Server
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.1   Jun 23 2005 12:49:00   schand
//Updated the inteface for MobileBookmark. Removed Bookmark type.
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.0   Jun 20 2005 15:34:08   schand
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.MobileBookmark
{
	/// <summary>
	/// The interface for sending the bookmark using Kizoon web service.
	/// </summary>
	public interface ITDMobileBookmark
	{
		void SendBookmark(string messageRecipient, string bookmarkUrl, string deviceType);
		MobileDeviceTypesDropList[] GetMobileDeviceTypes();
	
	}
}
