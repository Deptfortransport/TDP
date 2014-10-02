// *********************************************** 
// NAME                 : LogoutControl.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 06/09/2005 
// DESCRIPTION          : TDUserControl class providing  
//                        facility for the user to logout
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LogoutControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Dec 15 2008 09:54:00   apatel
//XHTML compliance changes 
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:21:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:16   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:16:56   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:26:10   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Nov 03 2005 17:08:14   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.0.1.0   Oct 18 2005 14:07:14   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.0   Sep 13 2005 15:44:50   NMoorhouse
//Initial revision.
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.Common;


	/// <summary>
	///	Summary description for LogoutControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class LogoutControl : TDUserControl
	{

		/// <summary>
		/// Handler for the load event. Sets up the controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			logoutTitleLabel.Text = GetResource("LogoutControl.logoutTitelLabel.Text");
			confirmLogoutLabel.Text = GetResource("LogoutControl.confirmLogoutLabel.Text");
			
			confirmCancelButton.Text = GetResource("LogoutControl.confirmCancelButton.Text");
			confirmButton.Text = GetResource("LogoutControl.confirmButton.Text");
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			ExtraWiringEvents();
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraWiringEvents()
		{
			confirmCancelButton.Click += new EventHandler(this.confirmCancelButton_Click);
			confirmButton.Click += new EventHandler(this.confirmButton_Click);
		}
		#endregion

		/// <summary>
		/// Handler for the confirm log out 'ok' button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void confirmButton_Click(object sender, EventArgs e)
		{
			//Clear Session
			Session.Abandon();

			//Redirect to Home
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.SessionAbandon;
		}

		/// <summary>
		/// Handler for the cancel log out process button click
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void confirmCancelButton_Click(object sender, EventArgs e)
		{
			this.Visible = false;
		}

	}
}
