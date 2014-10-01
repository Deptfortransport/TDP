// *********************************************** 
// NAME                 : OtaBookmarkSenderServiceOverride.cs
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 04/07/2005
// DESCRIPTION  	    : Override of the Kizoom Proxy.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/MobileBookmark/OtaBookmarkSenderServiceOverride.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:32   mturner
//Initial revision.
//
//   Rev 1.2   Jul 18 2005 14:48:24   NMoorhouse
//Changes to support the addition of the interface IOtaBookmarkSenderService
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.1   Jul 15 2005 13:39:52   NMoorhouse
//Changes to run Bookmark Service from Remote (App) Server
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.0   Jul 04 2005 18:24:52   NMoorhouse
//Initial revision.
using System;
using System.Net;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DepartureBoardService.MobileBookmark
{
	/// <summary>
	/// Summary description for OtaBookmarkSenderServiceOverride.
	/// </summary>
	public class OtaBookmarkSenderServiceOverride : OtaBookmarkSenderService, IOtaBookmarkSenderService
	{
		/// <summary>
		/// Used to Override the Bookmark Service Proxy Web Request.
		/// </summary>
		/// <returns>Web Request</returns>
		protected override System.Net.WebRequest GetWebRequest(Uri uri)
		{
			HttpWebRequest wr = (HttpWebRequest)base.GetWebRequest(uri);
			//KeepAlive attribute to False
			wr.KeepAlive=false;

			//Define ProxyServerURL if a Proxy Service is used.
			if( BookmarkHelper.BookmarkHasProxy == "yes")
			{
				string proxyUrl = BookmarkHelper.BookmarkProxyURL;
				if (proxyUrl != null && proxyUrl.Length != 0)
					wr.Proxy = new WebProxy(proxyUrl);
				else
				{
					string errorMessage = "Property DepartureBoardService.MobileBookmark.BookmarkProxyURL not defined";  
					Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, errorMessage)) ;
				}
				
			}
			return wr;
		}

	}
}
