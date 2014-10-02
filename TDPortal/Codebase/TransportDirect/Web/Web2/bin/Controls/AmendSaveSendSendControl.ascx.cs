// *********************************************** 
// NAME                 : AmendSaveSendSendControl.ascx.cs 
// AUTHOR               : Kenny Cheung (Modified by Esther Severn & Halim Ahad)
// DATE CREATED         : 20/08/2003 
// DESCRIPTION			: Send pane for the
// AmendSaveSendControl
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendSaveSendSendControl.ascx.cs-arc  $
//
//   Rev 1.4   Oct 26 2010 15:30:56   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.3   Apr 08 2010 13:22:04   apatel
//updated to attach map in Sent to friend emails
//Resolution for 5497: Email via Send to friend doesn't have map attached
//
//   Rev 1.2   Mar 31 2008 13:19:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:16   mturner
//Initial revision.
//
//   Rev 1.29   Feb 23 2006 19:16:22   build
//Automatically merged from branch for stream3129
//
//   Rev 1.28.1.0   Jan 10 2006 15:23:30   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.28   Nov 14 2005 18:48:48   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.27   Nov 03 2005 16:55:20   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.26.1.0   Oct 20 2005 11:23:20   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.26   Mar 18 2005 16:36:52   COwczarek
//Optimisation to not do any work if control is not visible.
//Viewstate disabled for labels and image button urls
//(now initialised for each page request if control visible).
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.25   May 26 2004 17:14:16   COwczarek
//Map image for email attachment is obtained using file path rather than stream
//Resolution for 726: Server error message when sending email to friend (Maps) (DEL 6.0)
//
//   Rev 1.24   Jan 22 2004 16:56:22   asinclair
//Added alt test for OK and Send buttons.  Fix for IR 614
//
//   Rev 1.23   Jan 07 2004 11:40:46   JHaydock
//Changed email subject to "<user email> sent you a message" and email from address to that of the currently logged in user
//
//   Rev 1.22   Nov 27 2003 11:04:58   passuied
//max length for text boxes	
//
//   Rev 1.21   Nov 25 2003 09:44:32   passuied
//completed character limitation for user registration controls (#138)
//Fixed some coding mistakes
//Resolution for 138: User Registration - Character Limitation
//
//   Rev 1.20   Nov 14 2003 13:21:20   hahad
//exception taht thrown when no attachment is present is fixed.  
//
//   Rev 1.19   Nov 06 2003 10:36:04   hahad
//Code Review fixes
//
//   Rev 1.18   Oct 30 2003 19:09:06   esevern
//changes to conform to FXCop
//
//   Rev 1.17   Oct 30 2003 18:09:26   hahad
//No Changes
//
//   Rev 1.16   Oct 29 2003 15:13:52   hahad
//Changed Send Button to ImageButton
//
//   Rev 1.15   Oct 29 2003 10:54:54   hahad
//Catches Undocumented exception and commented code
//
//   Rev 1.14   Oct 27 2003 10:29:48   esevern
//SendEmail takes 2 string args
//
//   Rev 1.13   Oct 24 2003 10:41:24   esevern
//checked in for handover to HA.  Requires urlPath setting
//
//   Rev 1.12   Oct 23 2003 17:19:42   hahad
//Clean up redundant code
//
//   Rev 1.11   Oct 13 2003 11:06:52   hahad
//Added HTML Template
//
//   Rev 1.10   Oct 06 2003 14:20:16   PNorell
//Removed reference to NUnit.
//
//   Rev 1.9   Sep 18 2003 11:25:28   geaton
//Code commented out until EMAILATTACHMENTPUBLISHER is replaced with CUSTOMEMAILPUBLISHER.
//
//   Rev 1.8   Sep 15 2003 16:16:16   hahad
//Changes to Feedback page started
//
//   Rev 1.7   Sep 04 2003 10:56:06   hahad
//EmailAttachmentEvent added with Test Urls
//
//   Rev 1.6   Sep 02 2003 17:36:30   hahad
//EmailAttachmentEvent added
//
//   Rev 1.5   Sep 02 2003 13:59:42   hahad
//Added Get Property to txtEmailAddress
//
//   Rev 1.4   Aug 20 2003 17:20:36   kcheung
//Updated header.
//
//   Rev 1.3   Aug 20 2003 17:19:56   kcheung
//Added header.

namespace TransportDirect.UserPortal.Web.Controls

{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.Common;
	using System.Web.Mail;
	using TransportDirect.Common.Logging;
	using Logger = System.Diagnostics.Trace;
	using System.Collections;
	using System.Diagnostics;
	using System.Net;
	using System.IO;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///	Send pane for the AmendSaveSendControl, takes in an email address and send the recipient  the journey details
	///	and if applicable a map image
	/// </summary>
	public partial  class AmendSaveSendSendControl : TDUserControl
	{
		#region Web Controls
		
		
		#endregion

		/// <summary>
		/// Prerender event handler. Intialises controls with resource manager strings.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if(this.Visible)
			{
				labelTypeEmailAddress.Text = Global.tdResourceManager.GetString
					("AmendSaveSendSendControl.labelTypeEmailAddress", TDCultureInfo.CurrentUICulture);

				labelEnterEmail.Text = Global.tdResourceManager.GetString
					("AmendSaveSendSendControl.labelEnterEmail", TDCultureInfo.CurrentUICulture);

				labelDeliveryNote.Text = Global.tdResourceManager.GetString
					("AmendSaveSendSendControl.labelDeliveryNote", TDCultureInfo.CurrentUICulture);

				buttonSend.Text = GetResource("AmendSaveSendSendControl.buttonSend.Text");
			}

            RegisterJavascript();
		}


		/// <summary>
		/// Read only property, returning the 'buttonSend' image button
		/// </summary>
		/// <returns>ImageButton</ret
		public TDButton SendButton
		{
			get
			{
				return buttonSend;
			}
		}


		/// <summary>
		/// Public method that validates textEmailAddress
		/// </summary>
		/// <returns>Bool, if valid true else false</returns>
		public bool ValidEmailAddress()
		{
			return new EmailAddress(HttpUtility.HtmlEncode(textEmailAddress.Text.Trim())).IsValid;
		}

		/// <summary>
		/// Assumes that email address validation has been done.
		/// </summary>
		/// <param name="data">emailFromAddress, emailSubject, emailBody</param>
		public void SendEmail(string emailFromAddress, string emailSubject, string emailBody)
		{
			//emailToAddress is the send to a friend EmailAdress
			string emailToAddress = HttpUtility.HtmlEncode(textEmailAddress.Text.Trim());
			
			//Exception message
			string msg = "Emailing of journey details failed ";

			//Checks to see if there is an Outward Map Stored in the Session
			if (TDSessionManager.Current.InputPageState.MapUrlOutward != "" || TDSessionManager.Current.InputPageState.MapUrlReturn != "")
			{

                Uri mapFilePath = null;
                //Session contains the URI of the map image. This needs to be converted
                //to a file path by extracting the name of the file from the URI then
                //prepending it with a directory path obtained from configuration data
                if (TDSessionManager.Current.InputPageState.MapUrlOutward != "")
                {
                    mapFilePath = new Uri(TDSessionManager.Current.InputPageState.MapUrlOutward);
                }
                else
                {
                    mapFilePath = new Uri(TDSessionManager.Current.InputPageState.MapUrlReturn);
                }
                
                //Get directory from which map image will be copied
                string mapImageDirectory = Properties.Current["Web.MapImageDirectory"];

                //Combine the directory from config data with filename from URI to get full 
                //file path of image file
                string attachmentFilePath = Path.Combine(mapImageDirectory,Path.GetFileName(mapFilePath.LocalPath));

				//Get name used for mail attachment
				string attachmentName = Global.tdResourceManager.GetString("AmendSaveSendSendControl.attachmentName", TDCultureInfo.CurrentUICulture);

                //Use image file extension as extension for mail attachment
                attachmentName = Path.ChangeExtension(attachmentName,Path.GetExtension(attachmentFilePath));
				
				try
				{	
					//Create Custom Event
					CustomEmailEvent mailattachmentJourneyDetailEvent = 
						new CustomEmailEvent(emailFromAddress, emailToAddress, emailBody, emailSubject, attachmentFilePath, attachmentName);
					//Add to listener
					Logger.Write(mailattachmentJourneyDetailEvent);
				}
				//Catch undocumented exception
				catch (NotSupportedException notSupportedException)
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, msg);

					throw new TDException(msg, notSupportedException, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}
				catch (ArgumentNullException argumentNullException)
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, msg);

					throw new TDException(msg, argumentNullException, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}
				catch (UriFormatException uriFormatException)
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, msg);

					throw new TDException(msg, uriFormatException, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}
				catch (TDException tde)
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, msg);

					throw new TDException(msg, tde, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}
				// to catch undocumented exceptions
				catch (Exception exception) 
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, msg);

					throw new TDException(msg, exception, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}
			}
			else
			{
				try
				{	
					//Create Custom Event
					CustomEmailEvent mailJourneyDetailEvent = 
						new CustomEmailEvent(emailFromAddress, emailToAddress, emailBody, emailSubject);
					//Add to listener
					Logger.Write(mailJourneyDetailEvent);
				}
				//Catch undocumented exception
				catch (NotSupportedException notSupportedException)
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, msg);

					throw new TDException(msg, notSupportedException, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}
				catch (ArgumentNullException argumentNullException)
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, msg);

					throw new TDException(msg, argumentNullException, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}
				catch (UriFormatException uriFormatException)
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, msg);

					throw new TDException(msg, uriFormatException, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}
				catch (TDException tde)
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, msg);

					throw new TDException(msg, tde, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}
				// to catch undocumented exceptions
				catch (Exception exception) 
				{
					// log exception and re-throw TDException
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, msg);

					throw new TDException(msg, exception, true, TDExceptionIdentifier.BTCSendJourneyDetailsFailed); 
				}
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
        }
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion


        #region Private Methods
        private void RegisterJavascript()
        {
            TDPage tdPage = (TDPage)Page;

            if (tdPage.IsJavascriptEnabled)
            {
                string sendButtonScript = string.Format("try{{if(typeof setupEmailImage == 'function'){{ setupEmailImage({0});}}}}catch(Error){{}}",Properties.Current["InteractiveMapping.MapImageResolution"]);

                SendButton.OnClientClick = sendButtonScript;
            }
        }
        #endregion
    }
}
