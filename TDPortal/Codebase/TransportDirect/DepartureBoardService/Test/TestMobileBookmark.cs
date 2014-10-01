// *********************************************** 
// NAME                 : TestMobileBookmark.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/06/2005 
// DESCRIPTION  	    : Nunit test class for MobileBookmark component
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/Test/TestMobileBookmark.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:48   mturner
//Initial revision.
//
//   Rev 1.3   Mar 02 2006 16:23:22   RPhilpott
//Clear compilation warnings.
//Resolution for 17: DEL 8.1 Workstream - RTTI
//
//   Rev 1.2   Jul 19 2005 09:10:56   NMoorhouse
//Addition of Mock Bookmark Sender Service
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.1   Jul 15 2005 13:41:16   NMoorhouse
//Changes due to bookmark service running from Remote (APP) server
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.0   Jun 23 2005 12:58:54   schand
//Initial revision.

using System;
using System.Reflection; 
using System.Data;
using TransportDirect.Common;
using System.Collections; 
using System.Diagnostics;
using System.IO;
using NUnit.Framework;
using TransportDirect.Common.Logging ;  
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;  
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.DepartureBoardService;   
using TransportDirect.UserPortal.DepartureBoardService.MobileBookmark;
using TransportDirect.UserPortal.AdditionalDataModule; 
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.DatabaseInfrastructure ;

namespace TransportDirect.UserPortal.DepartureBoardService.Test
{
	/// <summary>
	/// Summary description for TestMobileBookmark.
	/// </summary>
	[TestFixture] 
	public class TestMobileBookmark
	{
			TDMobileBookmark bookmark ; 
		
		[TestFixtureSetUp]
		public void Init()
		{
			try
			{

				TDServiceDiscovery.ResetServiceDiscoveryForTest();
				// Initialise services
				TDServiceDiscovery.Init( new  MobileBookmarkTestInitialization() );				
				bookmark = new TDMobileBookmark();   
			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message.ToString());    
			}
		}

		
		[TestFixtureTearDown]
		public void TearDown()
		{   
		}

		/// <summary>
		/// Tests the Sending Bookmark via SMS option
		/// </summary>
		[Test] 
		public void SendBookmarkForSMS()
		{
			string recipient, bookmarkUrl, deviceType;
			recipient = "07733315754";
			bookmarkUrl = "http://localhost/tdmobileUI/MobileHome.aspx?b=lsad893421jkgqewkjdwq8o4123jbkdasjkbdsa8yjhdsajkaskjdasjkdadasdasdasdasdasasd";
			deviceType = Keys.DeviceType_SMS.ToString();  
			
			

			// send SMS bookmark with bookmarkURL < 160
			try
			{
				bookmark.SendBookmark(recipient, bookmarkUrl,  deviceType);    
			}
			catch(TDException)
			{
				Assert.Fail("Fail to send sms"); 
			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);  
			}

			// send SMS bookmark with bookmarkURL > 160
			bookmarkUrl = "http://localhost/tdmobileUI/MobileHome.aspx?b=lsad893421jkgqewkjdwq8o4123jbkdasjkbdsa8yjhdsajkaskjdasjkdadasdasdasdasdasasdsadasdsadsadsad9843249hafdhrq987hifasdhkda094312hkfe09alafs790fashklasd";
			try
			{
				bookmark.SendBookmark(recipient, bookmarkUrl,  deviceType);
				Assert.Fail("SendBookmark should fail as bookmark length > 160 "); 
			}
			catch (TDException)
			{

			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);  
			}

			// send SMS bookmark with no recipient
			bookmarkUrl = "http://localhost/tdmobileUI/MobileHome.aspx?b=lsad893421jkgqewkjdwq8o4123jbkdasjkbdsa8yjhdsajkaskjdasjkdadasdasdasdasdasasd";
			recipient = "";
			try
			{
				bookmark.SendBookmark(recipient, bookmarkUrl,  deviceType);
				Assert.Fail("SendBookmark should fail as no recipient has been specified");    
			}
			catch (TDException)
			{

			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);  
			}


			// send SMS bookmark with no deviceType
			
			recipient = "07733315754";
			deviceType = "";
			try
			{
				bookmark.SendBookmark(recipient, bookmarkUrl,  deviceType);		
			}	
			catch (TDException)
			{
				Assert.Fail("SendBookmark Failed: With no deviceType should default to sending an SMS");
			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);  
			}
		}

		/// <summary>
		/// Tests the Sending Bookmark via Nokia option
		/// </summary>
		[Test]
		public void SendBookmarkForNokia()
		{
			string recipient, bookmarkUrl, deviceType;
			recipient = "07733315754";
			bookmarkUrl = "http://localhost/tdmobileUI/MobileHome.aspx?b=lsad893421jkgqewkjdwq8o4123jbkdasjkbdsa8yjhdsajkaskjdasjkdadasdasdasdasdasasd";
			deviceType = Keys.DeviceType_Nokia.ToString();  
			
			// send Nokia bookmark with bookmarkURL < 255
			try
			{
				bookmark.SendBookmark(recipient, bookmarkUrl, deviceType);
			}
			catch (TDException)
			{
				Assert.Fail("Fail to send nokia bookmark");    
			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);  
			}

			// send Nokia bookmark with bookmarkURL > 255
			bookmarkUrl = "http://localhost/tdmobileUI/MobileHome.aspx?b=lsad893421jkgqewkjdwq8o4123jbkdasjkbdsa8yjhdsajkaskjdasjkdadasdasdasdasdasasdsadasdsadsadsad9843249hafdhrq987hifasdhkda094312hkfe09alafs790fashklasdsadsadhsadhsadksahdsakdhsakdhsadkhsadkhsadhsabxcxcbifdwqekjfkfakskdsadkjsadgskadsakjdsakjdsakdksad";
			try
			{
				bookmark.SendBookmark(recipient, bookmarkUrl,  deviceType);
				Assert.Fail("SendBookmark should fail as bookmark length > 255 "); 
			}
			catch (TDException)
			{

			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);  
			}

			// send Nokia bookmark with no recipient
			bookmarkUrl = "http://localhost/tdmobileUI/MobileHome.aspx?b=lsad893421jkgqewkjdwq8o4123jbkdasjkbdsa8yjhdsajkaskjdasjkdadasdasdasdasdasasd";
			recipient = "";
			try
			{
				bookmark.SendBookmark(recipient, bookmarkUrl,  deviceType);
				Assert.Fail("SendBookmark should fail as no recipient has been specified");    
			}
			catch (TDException)
			{

			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);  
			}

		}

		/// <summary>
		/// Tests the Sending Bookmark via Wap Push option
		/// </summary>
		[Test]
		public void SendBookmarkForWap()
		{
			string recipient, bookmarkUrl, deviceType;
			recipient = "07733315754";
			bookmarkUrl = "http://localhost/tdmobileUI/MobileHome.aspx?b=lsad893421jkgqewkjdwq8o4123jbkdasjkbdsa8yjhdsajkaskjdasjkdadasdasdasdasdasasd";
			deviceType = Keys.DeviceType_WAPPush.ToString();  
			
			

			// send WAP bookmark with bookmarkURL < 255
			try
			{
				bookmark.SendBookmark(recipient, bookmarkUrl,  deviceType);	
			}
			catch (TDException)
			{
				Assert.Fail("Fail to send wap bookmark");    
			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);  
			}

			// send WAP bookmark with bookmarkURL > 255
			bookmarkUrl = "http://localhost/tdmobileUI/MobileHome.aspx?b=lsad893421jkgqewkjdwq8o4123jbkdasjkbdsa8yjhdsajkaskjdasjkdadasdasdasdasdasasdsadasdsadsadsad9843249hafdhrq987hifasdhkda094312hkfe09alafs790fashklasdsadsadhsadhsadksahdsakdhsakdhsadkhsadkhsadhsabxcxcbifdwqekjfkfakskdsadkjsadgskadsakjdsakjdsakdksad";
			try
			{
				bookmark.SendBookmark(recipient, bookmarkUrl,  deviceType);
				Assert.Fail("SendBookmark should fail as bookmark length > 255 "); 
			}
			catch (TDException)
			{

			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);  
			}


			// send wap bookmark with no recipient
			bookmarkUrl = "http://localhost/tdmobileUI/MobileHome.aspx?b=lsad893421jkgqewkjdwq8o4123jbkdasjkbdsa8yjhdsajkaskjdasjkdadasdasdasdasdasasd";
			recipient = "";
			try
			{
				bookmark.SendBookmark(recipient, bookmarkUrl,  deviceType);
				Assert.Fail("SendBookmark should fail as no recipient has been specified"); 
			}	
			catch (TDException)
			{

			}
			catch(Exception ex)
			{
				Assert.Fail(ex.Message);  
			}

		}
	}

	/// <summary>
	/// Test Initialization Class
	/// </summary>
	public class MobileBookmarkTestInitialization : IServiceInitialisation
	{	
       		
		/// <summary>
		/// Set up of Services and Logging
		/// </summary>
		/// <param name="serviceCache"></param>
		public void Populate(Hashtable serviceCache)
		{
			serviceCache.Add(ServiceDiscoveryKey.Crypto, new CryptoFactory());
			// Enable PropertyService
			serviceCache.Add(ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());
			// adding additionalData				
			serviceCache.Add (ServiceDiscoveryKey.AdditionalData, new AdditionalDataFactory());
 
			// Enable logging service.
			ArrayList errors = new ArrayList();
			try
			{    
				IEventPublisher[] customPublishers = new IEventPublisher[0];
				
				Logger.Listeners.Add(new TDTraceListener(Properties.Current, customPublishers, errors));
			}
			catch (TDException tdEx)
			{
				foreach(string error in errors)
				{
					Console.WriteLine(error);
				}
				throw tdEx;
			}
			// Adding the instance of BookmarkWebService object... need to be before MobileBookmark!
			serviceCache.Add(ServiceDiscoveryKey.BookmarkWebService, new TestMockBookmarkSenderService());
		}
	}
}
