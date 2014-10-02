// *********************************************** 
// NAME                 : UserSessionControl.aspx.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 05/09/2005
// DESCRIPTION			: A custom user control to provide
// a single point for logon, register and log-out actions.
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/UserSessionControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:34   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:38   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 16:14:26   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.3   Feb 10 2006 15:04:44   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.4   Jan 09 2006 16:20:38   RGriffith
//Changes made in light of code review comments
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
namespace TransportDirect.UserPortal.Web.Controls
{

	using System;
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///	Summary description for UserSessionControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class UserSessionControl : TDUserControl
	{
		protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl loginLinkButton;
		protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl registerLinkButton;
		protected TransportDirect.UserPortal.Web.Controls.HyperlinkPostbackControl logoutLinkButton;
	
		/// <summary>
		/// Handler for the load event. Sets up the controls.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            loginLinkButton.Text = GetResource("UserSessionControl.loginLinkButton.Text");
			registerLinkButton.Text = GetResource("UserSessionControl.registerLinkButton.Text");
			logoutLinkButton.Text = GetResource("UserSessionControl.logoutLinkButton.Text");
			registerOptionalLiteral.Text = GetResource("UserSessionControl.registerOptionalLiteral.Text");

			loginLinkButton.ToolTipText = GetResource("UserSessionControl.loginLinkButton.TooltipText");
			registerLinkButton.ToolTipText = GetResource("UserSessionControl.registerLinkButton.TooltipText");
			logoutLinkButton.ToolTipText = GetResource("UserSessionControl.logoutLinkButton.TooltipText");

			//Find if user is currently logged on and set display panels accordingly
			if(TDSessionManager.Current.Authenticated)
			{
				LoggedOnPanel.Visible = true;
				LoggedOffPanel.Visible = false;
			}
			else
			{
				LoggedOnPanel.Visible = false;
				LoggedOffPanel.Visible = true;
			}
		}

		public event EventHandler Login;
		public event EventHandler Register;
		public event EventHandler Logout;

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
			this.loginLinkButton.link_Clicked += new EventHandler(this.loginLinkButton_Click);
			this.registerLinkButton.link_Clicked += new EventHandler(this.registerLinkButton_Click);
			this.logoutLinkButton.link_Clicked += new EventHandler(this.logoutLinkButton_Click);
		}
		#endregion

		/// <summary>
		/// Handler for the LoginButton click event. Fires off 'Login' event to listening 
		/// login encapsulating control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void loginLinkButton_Click(object sender, EventArgs e)
		{
			//fire off login event to any listening container class
			if(Login != null)
			{
				Login(this, e);
			}
		
		}

		/// <summary>
		/// Handler for the RegisterButton click event. Fires off 'Register' event to listening 
		/// registering encapsulating control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void registerLinkButton_Click(object sender, EventArgs e)
		{
			//fire off register event to any listening container class
			if(Register != null)
			{
				Register(this, e);
			}
		
		}

		/// <summary>
		/// Handler for the LogoutButton click event. Fires off 'Logout' event to listening 
		/// log out encapsulating control.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void logoutLinkButton_Click(object sender, EventArgs e)
		{
			//fire off logout event to any listening container class
			if(Logout != null)
			{
				Logout(this, e);
			}
		
		}
	}
}
