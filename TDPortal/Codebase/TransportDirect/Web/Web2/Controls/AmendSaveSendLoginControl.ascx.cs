// *********************************************** 
// NAME                 : AmendSaveSendLoginControl.ascx.cs 
// AUTHOR               : Kenny Cheung (Modified by Esther Severn & Halim Ahad)
// DATE CREATED         : 20/08/2003 
// DESCRIPTION			: Login pane
// for the AmendSaveSend control.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendSaveSendLoginControl.ascx.cs-arc  $
//
//   Rev 1.3   Oct 26 2010 14:50:46   RBroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.2   Mar 31 2008 13:19:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:10   mturner
//Initial revision.
//
//   Rev 1.31   Feb 23 2006 19:16:20   build
//Automatically merged from branch for stream3129
//
//   Rev 1.30.1.0   Jan 10 2006 15:23:24   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.30   Mar 18 2005 16:36:48   COwczarek
//Optimisation to not do any work if control is not visible.
//Viewstate disabled for labels and image button urls
//(now initialised for each page request if control visible).
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.29   Sep 04 2004 11:56:54   passuied
//Addition of a second message for FindA mode
//
//   Rev 1.28   Apr 28 2004 16:19:58   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.27   Feb 13 2004 12:13:16   esevern
//DEL5.2 seperation of login and register - removed login/register functions 
//
//   Rev 1.26   Jan 22 2004 16:56:24   asinclair
//Added alt test for OK and Send buttons.  Fix for IR 614
//
//   Rev 1.25   Jan 20 2004 09:49:22   asinclair
//Added summary to forgotpasswordButton_Click event
//
//   Rev 1.24   Jan 20 2004 09:46:16   asinclair
//Added Forgotten password click event to FIx IR 579.  Also Email regular expression validator and alt text for forgotten password button.
//
//   Rev 1.23   Nov 25 2003 18:17:02   passuied
//fix so, on login control extra message is displayed if want to send a message and not logged in
//Resolution for 387: QA: You must be logged in message
//
//   Rev 1.22   Nov 25 2003 10:09:04   passuied
//fix for character limitation #138
//Resolution for 138: User Registration - Character Limitation
//
//   Rev 1.21   Nov 25 2003 09:44:22   passuied
//completed character limitation for user registration controls (#138)
//Fixed some coding mistakes
//Resolution for 138: User Registration - Character Limitation
//
//   Rev 1.20   Nov 13 2003 13:27:20   hahad
//Added custom password Validator
//
//   Rev 1.19   Nov 06 2003 10:37:26   hahad
//Code Review fixes
//
//   Rev 1.18   Oct 30 2003 19:29:36   esevern
//added setting password validator error message
//
//   Rev 1.17   Oct 30 2003 10:54:46   esevern
//added setting email validator text
//
//   Rev 1.16   Oct 29 2003 18:18:10   hahad
//Labels conform to standards
//
//   Rev 1.15   Oct 29 2003 10:57:02   hahad
//Image Buttons now appear properly
//
//   Rev 1.14   Oct 27 2003 10:39:36   hahad
//Input Validation added
//
//   Rev 1.13   Oct 24 2003 10:29:58   hahad
//No changes
//
//   Rev 1.12   Oct 23 2003 12:37:24   hahad
//Removed required field vaildator
//
//   Rev 1.11   Oct 23 2003 10:31:12   hahad
//Removed Login Functioanlity to main Control AmendSaveSendControl
//
//   Rev 1.10   Oct 21 2003 10:22:58   hahad
//Removed Help icons as they now appear on the master control
//
//   Rev 1.9   Oct 20 2003 16:55:50   hahad
//HTML tweaked
//
//   Rev 1.8   Oct 20 2003 13:37:14   esevern
//added forgotten password functionality, changed link button to image button
//
//   Rev 1.7   Oct 12 2003 13:54:42   hahad
//Added Properties for Protected fields
//
//   Rev 1.6   Oct 09 2003 12:50:32   hahad
//HTML Template Applied
//
//   Rev 1.5   Sep 25 2003 18:17:14   kcheung
//Applied some styling to make it consistent with the Amend control - this is not finalized as there is no template for this.
//
//   Rev 1.4   Aug 20 2003 17:16:54   kcheung
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

	using TransportDirect.UserPortal.Web.Controls;
	using Logger = System.Diagnostics.Trace;
	using TransportDirect.Common.Logging;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
	using TransportDirect.UserPortal.Web.Events;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///	Login pane for the AmendSaveSend control.
	/// </summary>
	public partial  class AmendSaveSendLoginControl : TDUserControl
	{
		protected System.Web.UI.WebControls.Label labelLoginRegister;
	
		/// <summary>
		/// Prerender event handler. Intialises controls with resource manager strings.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			if(this.Visible)
			{

				labelLoginRegisterNote.Text = Global.tdResourceManager.GetString
					("AmendSaveSendLoginControl.labelLoginRegisterNote",
					TDCultureInfo.CurrentUICulture);

				if (TDSessionManager.Current.IsFindAMode)
				{
					loginRegisterInstructionsLabel.Text = Global.tdResourceManager.GetString
						("AmendSaveSendLoginControl.loginRegisterInstructionsLabel.FindA",
						TDCultureInfo.CurrentUICulture);
				}
				else
				{
					loginRegisterInstructionsLabel.Text = Global.tdResourceManager.GetString
						("AmendSaveSendLoginControl.loginRegisterInstructionsLabel.JP",
						TDCultureInfo.CurrentUICulture);
				}
						
			}

		}


		/// <summary>
		/// Sets instructional and title labels visible
		/// </summary>
		private void ShowLabels() 
		{
			labelLoginRegisterNote.Visible = true;
			loginRegisterInstructionsLabel.Visible = true;
		}

		/// <summary>
		/// Hides all instructional and title labels 
		/// </summary>
		private void HideLabels() 
		{
			labelLoginRegisterNote.Visible = false;
			loginRegisterInstructionsLabel.Visible = false;
		}

		/// <summary>
		/// Customizes the control and displays more information if the parent requires it
		/// </summary>
		/// <param name="isDefault">default true = no extra messages, false = extra messages</param>
		public void SetControlType (bool isDefault)
		{
			labelLoginRegisterNote.Visible = !isDefault;
		}


		#region Web Form Designer generated code
		/// <summary>
		/// OnInit event
		/// </summary>
		/// <param name="e"></param>
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


