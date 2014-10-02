// *********************************************** 
// NAME                 : LoginRegisterLogoutControl.ascx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 06/09/2005
// DESCRIPTION          : TDUserControl class to handle  
//                        visibility of login/register/
//						  logout controls
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LoginRegisterLogoutControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:21:52   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:12   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:56   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Jan 10 2006 15:26:08   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Oct 05 2005 16:43:52   kjosling
//Fix applied for IR 2828
//Resolution for 2828: CCN219 - Global Login Control persistance
//
//   Rev 1.1   Sep 16 2005 17:33:38   Schand
//DN079 UEE
//Changes due to Updated RegisterClientScript function.
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//
//   Rev 1.0   Sep 13 2005 15:44:22   NMoorhouse
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

	/// <summary>
	///	Summary description for LoginRegisterLogoutControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class LoginRegisterLogoutControl : TDUserControl
	{
		protected LoginControl loginControl;
		protected RegisterControl registerControl;
		protected LogoutControl logoutControl;

		/// <summary>
		/// Handler for the load event. Sets up the visibility of the encapsulated controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Hide Login, Register and Logout user controls
			loginControl.Visible = false;
			registerControl.Visible = false;
			logoutControl.Visible = false;

			loginControl.ButtonClicked +=new EventHandler(loginControl_ButtonClicked);
			registerControl.ButtonClicked +=new EventHandler(registerControl_ButtonClicked);

			// Add javascript to the client side
			AddClientForUserNavigation();
		}

		/// <summary>
		/// Handles display of controls for logon option
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Login_Command(object sender, System.EventArgs e)
		{
			loginControl.Visible = true;
			registerControl.Visible = false;
			logoutControl.Visible = false;
		}

		/// <summary>
		/// Handles display of controls for registration option
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Register_Command(object sender, System.EventArgs e)
		{
			loginControl.Visible = false;
			registerControl.Visible = true;
			registerControl.DisplayLogin();
			logoutControl.Visible = false;
		}

		/// <summary>
		/// Handles display of controls for logout option
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void Logout_Command(object sender, System.EventArgs e)
		{
			loginControl.Visible = false;
			registerControl.Visible = false;
			logoutControl.Visible = true;
		}

		/// <summary>
		/// Registers the client side script.
		/// </summary>
		private void AddClientForUserNavigation()
		{
			string javaScriptFileName = "UserNavigationLoginAction";
			string javaScriptDom = ((TDPage)Page).JavascriptDom;
			UserExperienceEnhancementHelper.RegisterClientScript(this.Page, javaScriptFileName);  
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
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		private void loginControl_ButtonClicked(object sender, EventArgs e)
		{
			loginControl.Visible = true;
		}

		private void registerControl_ButtonClicked(object sender, EventArgs e)
		{
			registerControl.Visible = true;
		}
	}

}
