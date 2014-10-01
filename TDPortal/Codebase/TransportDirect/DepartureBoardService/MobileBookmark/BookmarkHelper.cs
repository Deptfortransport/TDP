// *********************************************** 
// NAME                 : BookmarkHelper.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/06/2005 
// DESCRIPTION  	    : Static bookmark helper class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/MobileBookmark/BookmarkHelper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:30   mturner
//Initial revision.
//
//   Rev 1.3   Jul 15 2005 13:39:50   NMoorhouse
//Changes to run Bookmark Service from Remote (App) Server
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.2   Jul 04 2005 18:24:52   NMoorhouse
//New Properties
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.1   Jun 23 2005 12:45:36   schand
//Updated the properties for MobileBookmark.
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.0   Jun 20 2005 15:34:06   schand
//Initial revision.

using System;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging ;  
using TransportDirect.Common.PropertyService.Properties;    
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DepartureBoardService.MobileBookmark
{
	/// <summary>
	/// Static bookmark helper class.
	/// </summary>
	public sealed class BookmarkHelper
	{
		
		public static string Username
		{
			get{return Properties.Current[Keys.BookmarkUserName];}
  
		}

		public static string Accountname
		{
			get{return Properties.Current[Keys.BookmarkAccountName];}
  
		}

		public static string BookmarkSenderName
		{
			get{return Properties.Current[Keys.BookmarkSenderName];}
  
		}

		public static string BookmarkSubjectHeader
		{
			get{return Properties.Current[Keys.BookmarkSubjectHeader];}
  
		}
        		

		public static string BodyMessage
		{
			get{return Properties.Current[Keys.BookmarkBodyMessage];}
  
		}

		public static string BookmarkServiceUrl
		{
			get{return Properties.Current[Keys.BookmarkServiceUrl];}
		}

		public static string UKPhonePrefix
		{
			get{return Properties.Current[Keys.UKPhonePrefix];}
		} 

		public static string BookmarkHasProxy
		{
			get{return Properties.Current[Keys.BookmarkHasProxy];}
		}

		public static string BookmarkProxyURL
		{
			get{return Properties.Current[Keys.BookmarkProxyURL];}
		}
	}
}
