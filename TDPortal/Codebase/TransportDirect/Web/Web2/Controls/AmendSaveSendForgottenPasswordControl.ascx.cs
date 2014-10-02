// *********************************************** 
// NAME                 : AmendSaveSendForgottenPasswordControl.ascx.cs 
// AUTHOR               : Kenny Cheung  (Modified by Esther Severn & Halim Ahad)
// DATE CREATED         : 20/08/2003 
// DESCRIPTION			: Forgotten password pane
// for the AmendSaveSend control.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendSaveSendForgottenPasswordControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:19:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:08   mturner
//Initial revision.
//
//   Rev 1.17   Feb 23 2006 19:16:20   build
//Automatically merged from branch for stream3129
//
//   Rev 1.16.1.0   Jan 10 2006 15:23:24   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.16   Nov 03 2005 16:16:56   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.15.1.0   Oct 20 2005 11:23:18   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.15   Apr 28 2004 16:19:58   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.14   Nov 27 2003 11:04:54   passuied
//max length for text boxes	
//
//   Rev 1.13   Nov 25 2003 09:44:20   passuied
//completed character limitation for user registration controls (#138)
//Fixed some coding mistakes
//Resolution for 138: User Registration - Character Limitation
//
//   Rev 1.12   Nov 06 2003 10:37:56   hahad
//Code Review fixes
//
//   Rev 1.11   Oct 30 2003 20:30:38   esevern
//moved label setting out of postback
//
//   Rev 1.10   Oct 29 2003 18:15:04   hahad
//Labels conform to standards
//
//   Rev 1.9   Oct 24 2003 14:37:02   esevern
//added forgotten password functionality
//
//   Rev 1.8   Oct 23 2003 10:32:54   hahad
//Labels now appear on controls
//
//   Rev 1.7   Oct 20 2003 17:49:14   hahad
//Removed Help icon and labels as they are now handled on the master control
//
//   Rev 1.6   Oct 13 2003 11:06:04   hahad
//Added HTML Template
//
//   Rev 1.5   Oct 03 2003 13:34:14   PNorell
//Updated the new exception identifier.
//
//   Rev 1.4   Sep 25 2003 18:32:48   kcheung
//Applied styling to make consistent with Amend Control - this will not be the final version as templates for this has not yet been defined.
//
//   Rev 1.3   Aug 20 2003 17:15:36   kcheung
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
	using System.Web.Mail;
	using TransportDirect.Common.Logging;
	using TransportDirect.Common;
	using Logger = System.Diagnostics.Trace;

	/// <summary>
	///	Forgotten password pane for the AmendSaveSend control.
	/// </summary>
	public partial  class AmendSaveSendForgottenPasswordControl : TDUserControl
	{
		protected TransportDirect.UserPortal.Web.Controls.HelpLabelControl AmendSaveSendForgottenPasswordHelpText;

		/// <summary>
		/// Page Load event - setting of Language Text and Image Url
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			labelEnterEmailMessage.Text = Global.tdResourceManager.GetString
				("AmendSaveSendForgottenPasswordControl.labelEnterEmailMessage",
				TDCultureInfo.CurrentUICulture);

			labelTypeEmailAddressLabel.Text = Global.tdResourceManager.GetString
				("AmendSaveSendForgottenPasswordControl.labelTypeEmailAddressLabel",
				TDCultureInfo.CurrentUICulture);

			buttonSend.Text = GetResource("AmendSaveSendForgottenPasswordControl.buttonSend.Text");
		}


		/// <summary>
		/// Public method for the buttonSend event
		/// </summary>
		/// <returns>ImageButton</returns>
		public TDButton SendButton
		{
			get
			{
				return buttonSend;
			}
		}

		/// <summary>
		/// Checks the validity of the user entered emailaddress, returning 
		/// true if valid, false otherwise.
		/// </summary>
		/// <returns>bool</returns>
		public bool ValidEmailAddress()
		{
			return new EmailAddress(textEmailAddress.Text).IsValid;
		}

		/// <summary>
		/// Public method that reads the Text from textEmailAddress
		/// </summary>
		/// <returns>String</returns>
		public string ForgottonEmailAddress()
		{
		
				return HttpUtility.HtmlEncode(textEmailAddress.Text);
			
		}

		/// <summary>
		/// Creates a CustomEmailEvent, passing the email destinaton (the user's 
		/// registered email address), the body of the email (their forgotten 
		/// password) and the email subject (taken from the properties
		/// service). Assumes that email address validation has been done.
		/// </summary>
		/// <param name="data"></param>
		/// <returns></returns>
		public void SendEmail(string destination, string data, string subject)
		{	
			string msg = "Emailing of user password failed ";
			
			try
				{
					// create custom event
					CustomEmailEvent sendPasswordEvent = new CustomEmailEvent(destination, data, subject);
				
					Logger.Write(sendPasswordEvent);
				}

				//Catch undocumented exception
			catch (NotSupportedException notSupportedException)
			{
				// log exception and re-throw TDException
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, msg);

				throw new TDException(msg, notSupportedException, true, TDExceptionIdentifier.BTCSendPasswordFailed); 
			}

			catch (ArgumentNullException argumentNullException)
			{
				// log exception and re-throw TDException
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, msg);

				throw new TDException(msg, argumentNullException, true, TDExceptionIdentifier.BTCSendPasswordFailed); 
			}

			catch (UriFormatException uriFormatException)
			{
				// log exception and re-throw TDException
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, msg);

				throw new TDException(msg, uriFormatException, true, TDExceptionIdentifier.BTCSendPasswordFailed); 
			}

			catch (TDException tde)
			{
				// log exception and re-throw TDException
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, msg);

				throw new TDException(msg, tde, true, TDExceptionIdentifier.BTCSendPasswordFailed); 
			}

				// to catch undocumented exceptions
			catch (Exception exception) 
			{
				// log exception and re-throw TDException
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, msg);

				throw new TDException(msg, exception, true, TDExceptionIdentifier.BTCSendPasswordFailed); 
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
	}
}
