// *********************************************** 
// NAME                 : IOtaBookmarkSenderService.cs
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 18/07/2005 
// DESCRIPTION  	    : The interface class for sending the bookmark using Kizoon web service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/MobileBookmark/IOtaBookmarkSenderService.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:30   mturner
//Initial revision.
//
//   Rev 1.0   Jul 18 2005 14:41:56   NMoorhouse
//Initial revision.
//

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.MobileBookmark
{
	/// <summary>
	/// The interface for Kizoon Web Service Proxy.
	/// </summary>
	public interface IOtaBookmarkSenderService
	{
		void sendWapPushBookmark(string username, string account, string msisdn, string url, string subject);
		void sendNokiaOtaBookmark(string username, string account, string msisdn, string url, string subject);   
		void sendBookmarkAsPlainText(string username, string account, string msisdn, string body);
	}
}