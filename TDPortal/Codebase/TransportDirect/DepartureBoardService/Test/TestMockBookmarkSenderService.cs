// *********************************************** 
// NAME                 : TestMockBookmarkSenderService.cs
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 18/07/2005 
// DESCRIPTION  	    : Nunit test stub for Mobile Bookmark Web Service Proxy
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Test/TestMockBookmarkSenderService.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:48   mturner
//Initial revision.
//
//   Rev 1.0   Jul 18 2005 14:46:56   NMoorhouse
//Initial revision.
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.DepartureBoardService.MobileBookmark
{
	/// <remarks/>
	public class TestMockBookmarkSenderService : IOtaBookmarkSenderService, IServiceFactory
	{
    
		/// <remarks/>
		public TestMockBookmarkSenderService() 
		{

		}
    
		public object Get()
		{
			TestMockBookmarkSenderService current = new TestMockBookmarkSenderService();
			return current;
		}
		
		/// <summary>
		/// Mock method for sending Wap Push Call to Kizoom
		/// </summary>
		/// <param name="username"></param>
		/// <param name="account"></param>
		/// <param name="msisdn"></param>
		/// <param name="url"></param>
		/// <param name="subject"></param>
		public void sendWapPushBookmark(string username, string account, string msisdn, string url, string subject) 
		{
			if (username.Length == 0 ||	account.Length == 0 || msisdn.Length == 0 || url.Length == 0 || subject.Length == 0)
			{
				throw new Exception("Test Failed. Invalid Parameters passed into sendWapPushBookmark");
			}
		}
    
    
		/// <summary>
		/// Mock method for sending Nokia Call to Kizoom
		/// </summary>
		/// <param name="username"></param>
		/// <param name="account"></param>
		/// <param name="msisdn"></param>
		/// <param name="url"></param>
		/// <param name="subject"></param>
		public void sendNokiaOtaBookmark(string username, string account, string msisdn, string url, string subject) 
		{
			if (username.Length == 0 ||	account.Length == 0 || msisdn.Length == 0 || url.Length == 0 || subject.Length == 0)
			{
				throw new Exception("Test Failed. Invalid Parameters passed into sendNokiaOtaBookmark");
			}
		}
    
    
		/// <summary>
		/// Mock method for sending SMS Call to Kizoom
		/// </summary>
		/// <param name="username"></param>
		/// <param name="account"></param>
		/// <param name="msisdn"></param>
		/// <param name="body"></param>
		public void sendBookmarkAsPlainText(string username, string account, string msisdn, string body) 
		{
			if (username.Length == 0 ||	account.Length == 0 || msisdn.Length == 0 || body.Length == 0)
			{
				throw new Exception("Test Failed. Invalid Parameters passed into sendBookmarkAsPlainText");
			}
		}
    
    
	}
}