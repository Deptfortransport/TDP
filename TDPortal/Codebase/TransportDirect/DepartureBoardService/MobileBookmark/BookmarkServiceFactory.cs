// *********************************************** 
// NAME                 : BookmarkServiceFactory.cs
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 04/07/2005
// DESCRIPTION  	    : Service Factory for the Proxy.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/MobileBookmark/BookmarkServiceFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:30   mturner
//Initial revision.
//
//   Rev 1.1   Jul 15 2005 13:39:52   NMoorhouse
//Changes to run Bookmark Service from Remote (App) Server
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.0   Jul 04 2005 18:24:52   NMoorhouse
//Initial revision.
using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DepartureBoardService.MobileBookmark
{
	/// <summary>
	/// Summary description for BookmarkServiceFactory.
	/// </summary>
	public class BookmarkServiceFactory : IServiceFactory
	{
		public BookmarkServiceFactory()
		{
		}

		/// <summary>
		/// Method used to get an instance of the Bookmarking Service Proxy. 
		/// Overriding the Kizoom URL with configurable property.
		/// </summary>
		/// <returns>An instance of OtaBookmarkSenderServiceOverride</returns>
		public object Get()
		{
			OtaBookmarkSenderServiceOverride current = new OtaBookmarkSenderServiceOverride();
			string newUrl = BookmarkHelper.BookmarkServiceUrl;
			if (newUrl != null && newUrl.Length != 0)
				current.Url = newUrl;
			else
			{
				string warningMessage = "Property DepartureBoardService.MobileBookmark.BookmarkServiceUrl not defined";  
				Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Warning, warningMessage)) ;
			}
			return current;
		}
	}
}
