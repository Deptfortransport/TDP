// *********************************************** 
// NAME                 : TDMobileBookmark.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/06/2005 
// DESCRIPTION  	    : TDBookmark class is mainly responsible for sending the bookmark using Kizoon web service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/MobileBookmark/TDMobileBookmark.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:32   mturner
//Initial revision.
//
//   Rev 1.4   Jul 18 2005 14:48:46   NMoorhouse
//Changes to support the addition of the interface IOtaBookmarkSenderService
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.3   Jul 15 2005 13:39:54   NMoorhouse
//Changes to run Bookmark Service from Remote (App) Server
//Resolution for 2580: Small Mobile/Bookmarks - cannot send a bookmark to a mobile phone
//
//   Rev 1.2   Jul 04 2005 18:25:54   NMoorhouse
//Changes to support Kizoom Proxy
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.1   Jun 23 2005 12:50:34   schand
//Updated the implementation of the interface.
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.0   Jun 20 2005 15:34:08   schand
//Initial revision.

using System;
using System.Collections; 
using TransportDirect.Common;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.Logging ;  
using TransportDirect.Common.PropertyService.Properties;    
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DepartureBoardService.MobileBookmark
{
	/// <summary>
	/// TDBookmark class is mainly responsible for sending the bookmark using Kizoon web service.
	/// </summary>
	/// 
	[Serializable]
	public class TDMobileBookmark  : MarshalByRefObject, ITDMobileBookmark
	{
		#region Private Fields
		string accountName= string.Empty;
		string userName= string.Empty;
		const int bookmarkUrlMaxLength = 255;
		const int bookmarkUrlSMSMaxLength = 160;
		#endregion


		#region Constructor
		public TDMobileBookmark()
		{ 
		}
		#endregion

		#region Public Members
		/// <summary>
		///	 This method sends the bookmark using Kizoom web service. TDException thrown if unsuccessful.
		/// </summary>
		/// <param name="messageRecipient">MSISDN</param>
		/// <param name="bookmarkUrl">The url of the page</param>		
		/// <param name="deviceType">the technology supported on the device</param>
		public void SendBookmark(string messageRecipient, string bookmarkUrl, string deviceType)
		{	
			IOtaBookmarkSenderService service = (IOtaBookmarkSenderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.BookmarkWebService];
			DeliveryMode deliveryMode ;
			
			try
			{
				// validate the input parameter
				if (!ValidateBookmarkParameters(messageRecipient, bookmarkUrl, deviceType))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, Messages.BookmarkInValidRequest)) ;
					throw new TDException(Messages.BookmarkInValidRequest, true, TDExceptionIdentifier.MobileBookmarkInValidRequest);  
				}

				// Remove leading zero and add UK International prefix
				messageRecipient = BookmarkHelper.UKPhonePrefix + messageRecipient.Substring(1);

				// Get the delivery mode
				deliveryMode = GetDeliveryMode(deviceType); 
				
				// account name and username 
				accountName = BookmarkHelper.Accountname;
				userName = BookmarkHelper.Username;
				
				if (!ValidateUserCredentials(userName,accountName))
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, Messages.BookmarkInValidUserName)) ;
					throw new TDException(Messages.BookmarkInValidUserName, true, TDExceptionIdentifier.MobileBookmarkInValidUserName);  
				}


				switch(deliveryMode)
				{
					case DeliveryMode.NokiaOTA:
						SendBookmarkForAdvanceDevice(messageRecipient, bookmarkUrl,  deliveryMode);
						break;
					case DeliveryMode.WapPush:
						SendBookmarkForAdvanceDevice(messageRecipient, bookmarkUrl,  deliveryMode);     	
						break;
					default:						
						SendBookmarkForSimpleDevice(messageRecipient, bookmarkUrl);
						break;
				}

			}
			catch(Exception ex)
			{
				string errorMessage = "Error occured in TDMobileBookmark.SendBookmark() " + " error:  " + ex.Message ;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errorMessage)) ;
				throw new TDException(ex.Message, true, TDExceptionIdentifier.MobileBookmarkSendError);  
			}
		}


		public MobileDeviceTypesDropList[] GetMobileDeviceTypes()
		{	
			DataServices.DataServices ds = (DataServices.DataServices) TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];  
			ArrayList arrlist  = ds.GetList(DataServices.DataServiceType.MobileDeviceTypes);
			MobileDeviceTypesDropList[] mobileDeviceTypeDropList = new MobileDeviceTypesDropList[arrlist.Count];
			
			for (int i=0; i< arrlist.Count; i++)
			{
				MobileDeviceTypesDropList templist = new MobileDeviceTypesDropList(((DataServices.DSDropItem)arrlist[i]).ResourceID, ((DataServices.DSDropItem)arrlist[i]).ItemValue, ((DataServices.DSDropItem)arrlist[i]).IsSelected);  					 
				mobileDeviceTypeDropList[i] = templist;
			}
		    
			return	mobileDeviceTypeDropList;
		}

		#endregion

		#region Private Members
		/// <summary>
		/// This method sends the bookmark to either Nokia phone or WAP Push enabled phone
		/// </summary>
		/// <param name="messageRecipient">MSISDN</param>
		/// <param name="bookmarkUrl">The url of the page</param>		
		/// <param name="deviceType">the technology supported on the device</param>
		private void SendBookmarkForAdvanceDevice(string messageRecipient, string bookmarkUrl,  DeliveryMode deviceType)
		{
			IOtaBookmarkSenderService service = (IOtaBookmarkSenderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.BookmarkWebService];
			string bookmarkSubjectHeader = string.Empty;
 
			bookmarkSubjectHeader = BookmarkHelper.BookmarkSubjectHeader;
  
			if (deviceType == DeliveryMode.NokiaOTA)
			{
				//Now call the web service object to send the bookmark
				service.sendNokiaOtaBookmark(userName, accountName, messageRecipient, bookmarkUrl , bookmarkSubjectHeader); 
				BookmarkLog(bookmarkUrl, messageRecipient, true, DeliveryMode.NokiaOTA.ToString());
                
			}
			else
			{
				//Now call the web service object to send the bookmark
				service.sendWapPushBookmark(userName, accountName, messageRecipient, bookmarkUrl , bookmarkSubjectHeader);
				BookmarkLog(bookmarkUrl, messageRecipient, true, DeliveryMode.WapPush.ToString() );
			}

			
		}

		/// <summary>
		///	 This method sends the bookmark to SMS supported device.
		/// </summary>
		/// <param name="messageRecipient">MSISDN</param>
		/// <param name="bookmarkUrl">The url of the page</param>		
		private void SendBookmarkForSimpleDevice(string messageRecipient, string bookmarkUrl)
		{
			IOtaBookmarkSenderService service = (IOtaBookmarkSenderService)TDServiceDiscovery.Current[ServiceDiscoveryKey.BookmarkWebService];
			string bodyMessage = string.Empty;
			string senderName = string.Empty; 
			
			// building body message
			bodyMessage = BookmarkHelper.BodyMessage;
			if (bodyMessage != null && bodyMessage.Length > 0)			
				bodyMessage = bodyMessage +  bookmarkUrl;

			if (bodyMessage.Length > bookmarkUrlMaxLength)
				bodyMessage = bookmarkUrl;

			// Sender Name is is not available on the current interface (Might be used in future) 
			// senderName = BookmarkHelper.BookmarkSenderName; 
			   
			//Now call the web service object to send the bookmark
			service.sendBookmarkAsPlainText(userName, accountName, messageRecipient, bodyMessage); 
 
			BookmarkLog(bookmarkUrl, messageRecipient, true, DeliveryMode.SMS.ToString());
		}


		/// <summary>
		/// This methods returns the delivery mode 
		/// </summary>
		/// <param name="deviceType"></param>
		/// <returns>DeliveryMode</returns>
		private DeliveryMode GetDeliveryMode(string deviceType)
		{
			
			if (deviceType==null || deviceType.Length < 1 )
				return DeliveryMode.SMS;

			try
			{
				int deviceTypeId = Int16.Parse(deviceType); 
  
				switch(deviceTypeId)
				{
					case Keys.DeviceType_Nokia:
						return DeliveryMode.NokiaOTA;  
					case Keys.DeviceType_WAPPush:		
						return DeliveryMode.WapPush; 
					default:
						return DeliveryMode.SMS; 

				}
			}
			catch(FormatException)
			{
				return DeliveryMode.SMS;
			}
			catch(OverflowException)
			{
				return DeliveryMode.SMS;
			}
		}
		
		/// <summary>
		///	 This method validates the input parameters
		/// </summary>
		/// <param name="messageRecipient">MSISDN</param>
		/// <param name="bookmarkUrl">The url of the page</param>		
		/// <returns>Returns if all parameters are valid else false</returns>
		private bool ValidateBookmarkParameters(string messageRecipient, string bookmarkUrl, string deviceType)
		{
			try
			{
				if (messageRecipient == null || messageRecipient.Length < 1)
					return false;
				
				if (bookmarkUrl ==null || bookmarkUrl.Length < 1) 
					return false;
                			
				switch(GetDeliveryMode(deviceType))
				{
					case DeliveryMode.SMS:
						if (bookmarkUrl.Length > bookmarkUrlSMSMaxLength)
							return false;
						break;
					default:
						if (bookmarkUrl.Length > bookmarkUrlMaxLength)
							return false;
						break;
				}
				return true;

			}
			catch(NullReferenceException)
			{
				return false;
			}
		}
		
		/// <summary>
		/// This method validates the username and its account name 
		/// </summary>
		/// <param name="userName"></param>
		/// <param name="accountName"></param>
		/// <returns></returns>
		private bool ValidateUserCredentials(string userName, string accountName)
		{
			if (userName ==null || userName.Length < 1 )
				return false;
	   
			if (accountName == null  || accountName.Length < 1)
				return false;
			
			// if above are valid then returns true; 
			return true;
		}

		private void BookmarkLog(string bookmarkUrl, string recipient, bool successful, string deliveryType)
		{
			string infoMessage = string.Empty; 
			if (successful)
				infoMessage = String.Format(Messages.BookmarkSentInfo, bookmarkUrl, recipient, deliveryType);  
			else
				infoMessage = String.Format(Messages.BookmarkFailedInfo, bookmarkUrl, recipient, deliveryType);  

			Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, infoMessage)) ;
		}

		#endregion
	}

}
